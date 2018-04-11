'BBV-P-423-RQADM-08 JBEJAR 04/04/2017 Pantalla Consulta INE. 
'BUG-PD-55 JBEJAR 22/05/2017 correcion al consultar documentos.
'BUG-PD-74 JBEJAR 06/06/2017 MENSAJE TAREA EXITOSA AL PROCESAR. 
Imports System.Data
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlInputFile
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.SN
Imports ProdeskNet.BD

Partial Class aspx_ConsultaINE
    Inherits System.Web.UI.Page
    Public idPantalla As Integer = 101
    Dim clien As New clsDatosCliente
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim sol As New clsStatusSolicitud
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim intEnable As Integer
        clien.GetDatosCliente(Request("sol"))
        sol.getStatusSol(Request("sol"))
        hdPantalla.Value = Request("idPantalla")
        hdSolicitud.Value = Request("sol")
        Session("idsol") = hdSolicitud.Value
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


        intEnable = CInt(Request.QueryString("Enable"))



        If intEnable = 1 Then
            btnLinkedin.Enabled = False
            btnProcesar.Attributes.Add("style", "display:none;")
            btnLimpiar.Attributes.Add("style", "display:none;")
            btnCancelar.Attributes.Add("style", "display:none;")
            ddlRedSocial.Enabled = False
            ddlGeoloca.Enabled = False

            Dim clsifai As New clsCuestionarioINE()
            clsifai.ID_SOLICITUD = Request("sol")
            Dim dsres = clsifai.GetCuestionarioINE
            If clsifai.strError = "" Then
                ddlRedSocial.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_EXISTE_INE"))
                ddlGeoloca.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_ESTATUS_INE"))
               

            Else
                Master.MensajeError("Error: " + clsifai.StrError)
            End If
        End If

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');", True)

    End Sub

    Protected Sub btnLinkedin_click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirFB", "window.open('" + ConfigurationManager.AppSettings("PaginaIne").ToString + "','popupifai','width=1800,height=1000,left=-10,top=0,resizable');", True)
    End Sub

    Protected Sub ddlRedSocial_select_index(sender As Object, e As EventArgs)
        If ddlRedSocial.SelectedValue = 0 Or ddlRedSocial.SelectedValue = 2 Or ddlRedSocial.SelectedValue = 3 Then
            ddlGeoloca.Enabled = False


            btnProcesar.Enabled = True
            ddlGeoloca.SelectedValue = -1


        Else
            ddlGeoloca.Enabled = True
            ddlGeoloca.SelectedValue = -1

            btnProcesar.Enabled = True

        End If
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        ddlRedSocial.SelectedValue = -1
        ddlGeoloca.SelectedValue = -1



    End Sub
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub
    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs)
        Dim ds_siguienteTarea As DataSet
        Dim ds_validardocumento As DataSet
        Dim BD As New clsManejaBD
        Dim Mensaje As String
      

        btnProcesar.Enabled = False
        Dim clscuestionarioF As New clsCuestionarioINE
        Dim ds As New DataSet
        Dim Bandera As Integer = 0
        clscuestionarioF.ID_SOLICITUD = hdSolicitud.Value
        clscuestionarioF.EXISTE_INE = ddlRedSocial.SelectedValue
        clscuestionarioF.ESTATUS_INE = ddlGeoloca.SelectedValue
        BD.AgregaParametro("@FOLIO", TipoDato.Entero, hdSolicitud.Value)
        BD.AgregaParametro("@BANDERA", TipoDato.Entero, 2)
        BD.AgregaParametro("@PANTALLA", TipoDato.Entero, idPantalla)

        If clscuestionarioF.insertaDatosINE() Then
            ds_validardocumento = BD.EjecutaStoredProcedure("sp_validarEntrevista")
            Mensaje = ds_validardocumento.Tables(0).Rows(0).Item("MENSAJE")
            If Mensaje <> "" Then
                Master.MensajeError(Mensaje)
                btnProcesar.Enabled = True
            Else
               
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Desabilitar", "document.getElementById(" & cmbguardar1C.ClientID & ").attr('disabled'", True)

                ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & hdPantalla.Value)

                If ddlGeoloca.SelectedValue <> 1 Then

                    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))
                Else
                    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
                End If

            End If


            '    ScriptManager.RegisterStartupScript(Me, Page.GetType, "Avanza_Tarea", "btnManejoMensaje('exec sp_validarEntrevista " + hdSolicitud.Value.ToString + ",2," + idPantalla.ToString + "')", True)

        Else
            Throw New Exception("Error: Ocurrio un problema al guardar el cuestionario")
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
        Try
            'SE LLENAN LOS PARAMETROS PARA EJECUTAR METODOS DE LA CLASE clsSolicitudes
            Solicitudes.BOTON = 64                                  'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))      'PARAMETRO PARA ValNegocio() Y ManejaTarea()
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")          'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")     'PARAMETRO PARA ManejaTarea()
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla    'PARAMETRO PARA ManejaTarea()

            dsresult = Solicitudes.ValNegocio(1)                    'SE ADELANTA LA TAREA ESPECIFICADA(RECHAZO O NORECHAZO) Y OBTENEMOS MENSAJE
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje  ' SE ASIGNA MENSAJE COMO PARAMETRO PARA ManejaTarea()
            Solicitudes.ManejaTarea(5)
            If mensaje <> "Tarea Exitosa" Then
                'Dim redirect As String = "../aspx/consultaPanelControl.aspx"
                'Dim script As String = "PopUpLetreroRedirect('" + mensaje + "','" + redirect + "');"
                'ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", script, True)
                Response.Redirect("../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If

            'BD.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))
            'BUG-PD-74 JBEJAR MENSAJE TAREA EXITOSA  AL PROCESAR LA TAREA 
            If muestrapant = 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Mensaje", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString & "');", True)
            ElseIf muestrapant = 2 Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Mensaje", "PopUpLetreroRedirect('" & mensaje & "','" & "../aspx/consultaPanelControl.aspx');", True)
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub
End Class
