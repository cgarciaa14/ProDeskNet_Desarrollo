Imports ProdeskNet.SN
Imports System.Data
'RQADM-38:RHERNANDEZ:05/05/17: Se crea la pantalla para validar si es cliente incredit con caso de rechazo y no rechazo-
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
'BBV-P-423 RQ-PD-17 10 GVARGAS 30/01/2018 Ajustes flujos
'BUG-PD-386 GVARGAS 12/03/2018 Se cambia idPant

Partial Class aspx_PantValidaClient
    Inherits System.Web.UI.Page
    Dim StrErr As String = String.Empty
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim idPant As Integer = Int32.Parse(Request("idPantalla").ToString())

        Dim clssol As New clsTabDatosSolicitante
        Dim res As DataSet

        Dim ds_siguienteTarea As DataSet
        ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" + idPant.ToString())

        res = clssol.CargaDatosSolicitante(CInt(Request("sol")))
        If clssol.StrError <> "" Then
            Master.MensajeError(clssol.StrError)
            Exit Sub
        End If
        Dim noclientebbva As String
        noclientebbva = res.Tables(0).Rows(0).Item("CLIENTE_INCREDIT").ToString()

        Dim dts As DataSet
        Dim objpant As New ProdeskNet.SN.clsPantallas()
        dts = objpant.CargaPantallas(idPant)

        If dts.Tables.Count > 0 Then
            If dts.Tables(0).Rows.Count > 0 Then
                If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Then
                    If noclientebbva <> "" Then
                        asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"), idPant)
                    Else
                        asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"), idPant)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer, ByVal idPant As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Try

            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Request("Sol")
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            Solicitudes.PDK_ID_PANTALLA = idPant
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                Response.Redirect("../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If



            dslink = objtarea.SiguienteTarea(Solicitudes.PDK_ID_SOLICITUD)

            muestrapant = objpantalla.SiguientePantalla(Solicitudes.PDK_ID_SOLICITUD)

            Dim strLocation As String = String.Empty

            If muestrapant = 0 Then
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                'Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('', '" + strLocation + "');", True)
            ElseIf muestrapant = 2 Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '" + strLocation + "');", True)
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub
End Class
