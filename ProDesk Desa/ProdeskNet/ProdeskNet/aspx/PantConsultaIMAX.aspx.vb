Imports ProdeskNet.BD
Imports System.Data
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports ProdeskNet.SN
'RQADM-38:RHERNANDEZ:05/05/17: Se crea pantalla para la tarea de Consulta IMAX
'BUG-PD-54: RHERNANDEZ: 22/05/17: SE CAMBIA FUNCIONALIDAD PARA QUE EN VEZ DE QUE EL FLUJO DE LA TAREA CAMBIE EN BASE AL DOCUMENTO SEA EN BASE A UNA PREGUNTA
'BUG-PD-203: CGARCIIA: 04/09/2017: SE AGREGA NOTIFICACIONES 
'BUG-PD-215: CGARCIA: 26/09/2017: ACTUALIZACION DE CORREOS PARA CASOS AL QUE ENTRA.
'BUG-PD-227: CGARCIA: 05/10/2017: ACTUALIZACION DE EMAILS Y SCORE
'BUG-PD-237: RHERNANDEZ: 23/10/17: SE COTTIGE PROBLEMA AL DAR CLICK EN PROCESAR SIN HABER RESPONDIDO NINGUNA PREGUNTA
'BUG-PD-276: CGARCIA: 27/11/2017: CORRECCION DE MSJ Y REQUEST DE USUARIOS EN EMAILS
'BUG-PD-322: ERODRIGUEZ: 03/01/2017: Se cambio el redireccionamiento al panel de control
Partial Class aspx_PantConsultaIMAX
    Inherits System.Web.UI.Page
    Public idPantalla As Integer = 112
    Dim clien As New clsDatosCliente
    Dim sol As New clsStatusSolicitud
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Public ClsEmail As New ProdeskNet.SN.clsEmailAuto()
    Dim usu As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim intEnable As Integer
            Try
                intEnable = CInt(Request.QueryString("Enable"))

            Catch ex As Exception
                intEnable = 0
            End Try
            usu = Val(Request("usuario"))
            If usu = String.Empty Then
                usu = Val(Request("usu"))
            End If

            clien.GetDatosCliente(Request("sol"))
            sol.getStatusSol(Request("sol"))
            hdPantalla.Value = Request("idPantalla")
            hdSolicitud.Value = Request("sol")
            hdusuario.Value = Request("usu")
            Me.lblSolicitud.Text = Request("sol")
            Me.lblCliente.Text = clien.propNombreCompleto
            Me.lblStCredito.Text = sol.PStCredito
            Me.lblStDocumento.Text = sol.PStDocumento

            Session.Add("idSol", hdSolicitud.Value)

            If Session("Regresar") Is Nothing Then
                Session("Regresar") = Request.UrlReferrer.LocalPath
                hdRutaEntrada.Value = Request.UrlReferrer.LocalPath
            End If

            hdRutaEntrada.Value = Session("Regresar")

            Session("idSol") = hdSolicitud.Value
            Dim res As New DataSet
            Dim clssol As New clsTabDatosSolicitante
            res = clssol.CargaDatosSolicitante(CInt(Request("sol")))
            If clssol.StrError <> "" Then
                Master.MensajeError(clssol.StrError)
                Exit Sub
            End If
            lblnocliente.Text = res.Tables(0).Rows(0).Item("CLIENTE_INCREDIT").ToString

            Dim BD As New clsManejaBD
            Dim dsresult As New DataSet
            Try
                dsresult = BD.EjecutarQuery("EXEC sp_GenerarRura  " & hdSolicitud.Value & "," & hdPantalla.Value & "," & hdusuario.Value & "")
                If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
                    hdnResultado.Value = dsresult.Tables(0).Rows(0).Item("RUTA")
                End If

            Catch ex As Exception
                hdnResultado.Value = Session("Regresar")
            End Try

            If intEnable = 1 Then
                ddlTipoID.Enabled = False
                ddlfotoyfirma.Enabled = False
                lblnocliente.Enabled = False
                cmbguardar1.Attributes.Add("style", "display:none;")
                btnCancelar.Attributes.Add("style", "display:none;")
            End If

            Dim clsquiz As New clsCuestionarioIMAX()
            clsquiz._ID_SOLICITUD = Request("sol")
            Dim dsres = clsquiz.GetCuestionarioIMAX()
            If clsquiz.StrError = "" Then
                If Not IsNothing(dsres) Then
                    ddlTipoID.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_TIPO_ID"))
                    ddlfotoyfirma.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_FIRMA_Y_FOTO"))
                End If
            Else
                Master.MensajeError("Error: " + clsquiz.StrError)
            End If

        End If







        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');", True)

    End Sub

    

    Protected Sub cmbguardar1_Click(sender As Object, e As EventArgs) Handles cmbguardar1.Click
        Try
            Dim objTarea As New clsSolicitudes(0)
            Dim dsValidaTarea As DataSet
            Dim ValTareas As Integer = 0
            objTarea.PDK_ID_SOLICITUD = Request("sol")
            objTarea.PDK_ID_PANTALLA = Request("idPantalla")
            dsValidaTarea = objTarea.ManejaTarea(1)

            If dsValidaTarea.Tables.Count > 0 Then
                If dsValidaTarea.Tables(0).Rows.Count > 0 Then
                    ValTareas = dsValidaTarea.Tables(0).Rows(0).Item("VALIDA_TAREA")
                End If
            End If
            If ValTareas = 1 Then
                cmbguardar1.Enabled = False
                If ddlTipoID.SelectedValue <> -1 And ddlfotoyfirma.SelectedValue <> -1 Then
                    Dim clsquiz As New clsCuestionarioIMAX
                    clsquiz._ID_SOLICITUD = hdSolicitud.Value
                    clsquiz._TIPO_ID = ddlTipoID.SelectedValue
                    clsquiz._FOTO_FIRMA_ID = ddlfotoyfirma.SelectedValue
                    If clsquiz.InsertDatosCuestionarioIMAX() Then
                        Dim ds_siguienteTarea As DataSet
                        ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & CInt(Request("idPantalla")).ToString)
                        If ddlfotoyfirma.SelectedValue = 0 Then
                            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"), 0)
                        ElseIf ddlfotoyfirma.SelectedValue = 1 Then
                            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"), 1)
                        End If
                    Else
                        Throw New Exception("Error: Ocurrio un problema al guardar el cuestionario")
                    End If
                Else
                    Throw New Exception("Error: Falta contestar preguntas del cuestionario. *")
                End If
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
            cmbguardar1.Enabled = True
        End Try

    End Sub
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub
    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer, ByVal intRechazo As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Try

            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")

            If idAsignarPantalla <> 0 Then
                Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla
            End If

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                Master.MsjErrorRedirect(mensaje, "../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))
            Dim dc As New clsDatosCliente
            dc.idSolicitud = Request("sol")
            dc.getDatosSol()

            If muestrapant = 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usu=" & Val(Request("usu")).ToString & "');", True)
            ElseIf muestrapant = 2 Then
                If (intRechazo = 0) Then
                    Dim dcc As New ProdeskNet.Catalogos.clsDatosCliente
                    dcc.idSolicitud = Val(Request("sol"))
                    dcc.getDatosSol()
                    ClsEmail.OPCION = 17
                    ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

                    Dim dtConsulta = New DataSet()
                    dtConsulta = ClsEmail.ConsultaStatusNotificacion
                    If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
                        If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                            If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
                                If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
                                    Dim strLocation1 As String = String.Empty
                                    strLocation1 = ("../aspx/ValidaEmails.aspx?idPantalla=112&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=2" & "&usuario=" & usu)
                                    'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation1 & "';", True)
                                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '" + strLocation1 + "');", True)
                                Else
                                    Dim str As String = "?NoSolicitud=" & dcc.idSolicitud & "&Empresa=" & dcc.idempresa & "&Producto=" & dcc.idproducto & "&Persona=" & dcc.idtpersona
                                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                                End If
                            Else
                                Dim str As String = "?NoSolicitud=" & dcc.idSolicitud & "&Empresa=" & dcc.idempresa & "&Producto=" & dcc.idproducto & "&Persona=" & dcc.idtpersona
                                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                            End If
                        Else
                            Dim str As String = "?NoSolicitud=" & dcc.idSolicitud & "&Empresa=" & dcc.idempresa & "&Producto=" & dcc.idproducto & "&Persona=" & dcc.idtpersona
                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                        End If
                    Else
                        Dim str As String = "?NoSolicitud=" & dcc.idSolicitud & "&Empresa=" & dcc.idempresa & "&Producto=" & dcc.idproducto & "&Persona=" & dcc.idtpersona
                        ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                    End If
                Else
                    Dim strLocation1 As String
                    strLocation1 = ("../aspx/consultaPanelControl.aspx")
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation1 & "';", True)
                End If
                Else
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "');", True)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "');", True)
                    'Response.Redirect("../aspx/consultaPanelControl.aspx")
                End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub
End Class
