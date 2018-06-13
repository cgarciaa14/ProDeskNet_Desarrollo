'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BUG-PD-01: AMR: 22/12/2016: HTTP Error 404 - Not Found al procesar Precalificación.
'BUG-PD-11: GAPM 16.02.17: modificacion por amata
'BBV-P-423: ERV: 20/04/2017 RQADM-04 Validación prospector
'BUG-PD-67  GVARGAS 01/06/2017 Regresa si ocurre error sin borrar OPE_SOLICITUD-->
'BUG-PD-87  GVARGAS 15/06/2017 Regresa si ocurre un error logico
'BUG-PD-129  GVARGAS 29/06/2017 Correcion PEPs y Ñs
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
'BUG-PD-216 GVARGAS 29/09/2017 Cambios mostrar info
'BUG-PD-239 GVARGAS 17/10/2017 Camnio tamaño apellidos
'BUG-PD-285 GVARGAS 30/11/2017 Prospector cambios urgentes
'BUG-PD-313: DJUAREZ: 27/12/2017: Se evita el avanzar tareas cuando se presiona F5 cuando esta cargando una pagina.
'RQ-PD-23: ERODRIGUEZ: 23/02/2018: Cambio en datos enviados a servicio prospector para nuevo servicio getScoreEvaluation se quito if else, para ir siempre sin CLIENTE_INCREDIT
'AUTOMIK-TASK-428 GVARGAS 26/03/2018 Cambio funcion Prospector

Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.Catalogos
Imports ProdeskNet.WCF
Imports ProdeskNet.BD

Partial Class aspx_Prospector
    Inherits System.Web.UI.Page

    Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim strError As String = String.Empty

    Private Sub aspx_Prospector_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'Validacion de Request
            Dim Validate As New clsValidateData
            Dim Url As String = Validate.ValidateRequest(Request)

            If Url <> String.Empty Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", Url, True)
                Exit Sub
            End If
            'Fin validacion de Request

            Dim objpant As New ProdeskNet.SN.clsPantallas()
            Dim dts As New DataSet()

            dts = objpant.CargaPantallas(Val(Request("idPantalla")).ToString)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Then
                        btnprocesar_Click(btnprocesar, Nothing)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnprocesar_Click(sender As Object, e As EventArgs) Handles btnprocesar.Click
        Dim ProspectorClass As ProdeskNet.WCF.clsProspectus = New ProdeskNet.WCF.clsProspectus()

        Dim nSol As String = Request("Sol").ToString()
        Dim Usuario As String = Request("usuario").ToString()

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Dim Uri As String = System.Configuration.ConfigurationManager.AppSettings.Item("url") + System.Configuration.ConfigurationManager.AppSettings.Item("Prospectus")

        Dim responsePropectorString As String = ProspectorClass.GetProspector(nSol, userID, iv_ticket1, Uri, Usuario)

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

        Dim ResponseProspector As ResponseProspector = serializer.Deserialize(Of ResponseProspector)(responsePropectorString)

        Dim objseccero As ProdeskNet.SN.clsTabSeccionCero = New ProdeskNet.SN.clsTabSeccionCero()

        If ResponseProspector.Path = 0 Then 'regresar por fallo
            asignaTarea("1")
            Master.MsjErrorRedirect(ResponseProspector.Message, "./consultaPanelControl.aspx")
        ElseIf ResponseProspector.Path = 1 Then 'cancelar por prospector
            objseccero.ActSeccionCeroProsp(nSol, Usuario, 232) 'No viable
            Master.MsjErrorRedirect(ResponseProspector.Message, "./consultaPanelControl.aspx")
        Else 'prospector valido
            Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
            Dim objtarea As ProdeskNet.SN.clsCatTareas = New ProdeskNet.SN.clsCatTareas()
            Dim objpantalla As ProdeskNet.SN.clsPantallas = New ProdeskNet.SN.clsPantallas()

            Dim dsresult As DataSet = DB.EjecutarQuery("exec spvalNegocio " + nSol + ",64," + Usuario)

            Dim Message_ As String = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString()

            objseccero.ActSeccionCeroProsp(nSol, Usuario, 231) 'Viable

            Dim dslink As DataSet = objtarea.SiguienteTarea(nSol)

            Dim muestrapant As Integer = objpantalla.SiguientePantalla(nSol)

            Dim strLocation As String = String.Empty

            If muestrapant = 0 Then
                strLocation = "../aspx/" + dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() + "?idPantalla=" + dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() + "&sol=" + nSol + "&usuario=" + Usuario
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            ElseIf muestrapant = 2 Then
                Master.MsjErrorRedirect(Message_, "./consultaPanelControl.aspx")
            End If
        End If
    End Sub

    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Dim ipPantallaRechazo As Integer
        Try

            Dim ds_siguienteTarea As DataSet
            Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
            objCatalogos.Parametro = Request("idPantalla")
            ds_siguienteTarea = objCatalogos.Catalogos(6)

            If ds_siguienteTarea.Tables.Count > 0 Then
                If ds_siguienteTarea.Tables(0).Rows.Count > 0 Then
                    ipPantallaRechazo = Int32.Parse(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))
                End If
            End If



            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
            Solicitudes.PDK_ID_CAT_RESULTADO = ipPantallaRechazo

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                Master.MsjErrorRedirect(mensaje, "./consultaPanelControl.aspx")
                Exit Sub
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub

    Public Class ResponseProspector
        Public Code As String
        Public Message As String
        Public Path As Integer
    End Class
End Class
