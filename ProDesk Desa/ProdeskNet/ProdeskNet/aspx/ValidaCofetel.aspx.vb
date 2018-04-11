#Region "TRACKERS "
'BBV-P-423-RQADM-10 JBEJAR 19/04/2017 Pantalla tarea automtica cofetel. 
'BUG-PD-50 JBEJAR 16/05/2017  Correciones se le la respues del webservice y se mandan los dos.
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.     
'RQ-PI-7-PD5: JBEJAR:  13/10/2017: REFACTORIZACION DE CODIGO  VALIDA COFETEL  SE ELIMINA EL ENVIO DEL TELEFONO FIJO AL WS Y SE AGREGAN REGLAS DE NEGOCIO.  
'BUG-PD-395: DJUAREZ: 13/03/2018: Se agrega el bloqueo de F5 a la tarea automatica
#End Region
Imports ProdeskNet.WCF
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlInputFile
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.SN
Imports ProdeskNet.BD

Partial Class aspx_ValidaCofetel
    Inherits System.Web.UI.Page
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim strError As String = String.Empty
    Public Pantalla As Integer
    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Validacion de Request
        Dim Validate As New clsValidateData
        Dim Url As String = Validate.ValidateRequest(Request)

        If Url <> String.Empty Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", Url, True)
            Exit Sub
        End If
        'Fin validacion de Request

        Dim dsres As New DataSet
        Dim ds_siguienteTarea As DataSet
        Dim telefonos As clsCofetel.telephones = New clsCofetel.telephones()
        Dim telefono As clsCofetel.telephone1 = New clsCofetel.telephone1()
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonBODY As String = String.Empty
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        Dim jsonResult As String = String.Empty
        Dim res As clsCofetel.telephones1 = New clsCofetel.telephones1()
        Dim str As String = String.Empty
        Dim str1 As String = String.Empty
        Dim objtelefonos As New clsCofetel()
        Dim objguardar As New clsCofetel()

        Pantalla = Request("idPantalla")
        objtelefonos.ID_SOLICITUD = Request("sol")
        dsres = objtelefonos.GetTelSol
        ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS='" & Pantalla & "'")

        If Not IsNothing(dsres) AndAlso dsres.Tables.Count > 0 AndAlso dsres.Tables(0).Rows.Count() > 0 Then

            telefono.telephone.telephoneNumber = dsres.Tables(0).Rows(0).Item("TELEFONO_MOVIL").ToString()
            telefono.telephone.type = "MOVIL"
            telefonos.telephones.Add(telefono)


            jsonBODY = serializer.Serialize(telefonos)
            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("Cofetel")
            jsonResult = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)
            res = serializer.Deserialize(Of clsCofetel.telephones1)(jsonResult)
            objguardar.ID_SOLICITUD = Request("sol")
            objguardar.Json = jsonBODY
            objguardar.Json_Respuesta = jsonResult


            If objguardar.InsertaWS() Then
                If restGT.IsError = True Then
                    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO")) 'COFETEL TAREA MANUAL.
                Else

                    If restGT.MensajeError = "" Then
                        str = res.telephones(0).warningMessage.Split("-")(1)
                        If str.Contains("OK") Or str.Contains("MOVIL") Or str.Contains("FIJO") Then
                            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) 'FLUJO NORMAL. 
                        Else
                            asignaTarea(135)
                        End If

                    End If
                End If
            End If
        Else
            objguardar.ID_SOLICITUD = Request("sol")
            objguardar.Json = "SIN INFORMACIÓN DE TELEFONOS EN LA SOLICITUD"
            objguardar.Json_Respuesta = "SIN INFORMACIÓN DE TELEFONOS EN LA SOLICITUD"
            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))
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
        Dim strLocation As String = String.Empty

        Try
            'SE LLENAN LOS PARAMETROS PARA EJECUTAR METODOS DE LA CLASE clsSolicitudes
            Solicitudes.BOTON = 64                                  'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_SOLICITUD = Request("sol")      'PARAMETRO PARA ValNegocio() Y ManejaTarea()
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")          'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_PANTALLA = Pantalla    'PARAMETRO PARA ManejaTarea()
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla    'PARAMETRO PARA ManejaTarea()

            dsresult = Solicitudes.ValNegocio(1)                    'SE ADELANTA LA TAREA ESPECIFICADA(RECHAZO O NORECHAZO) Y OBTENEMOS MENSAJE
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje  ' SE ASIGNA MENSAJE COMO PARAMETRO PARA ManejaTarea()
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                Exit Sub
            End If

            dslink = objtarea.SiguienteTarea(Solicitudes.PDK_ID_SOLICITUD)

            muestrapant = objpantalla.SiguientePantalla(Solicitudes.PDK_ID_SOLICITUD)

            If muestrapant = 0 Then
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            ElseIf muestrapant = 2 Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub

End Class
