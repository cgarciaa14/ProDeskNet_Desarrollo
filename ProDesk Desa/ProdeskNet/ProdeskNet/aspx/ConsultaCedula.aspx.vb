'BBV-P-423-RQADM-09 JBB 04/04/2017 Pantalla Consulta cedula. 
'BBVA BUG-PD-41 JBB 26/04/2017 Correciones de pantalla cedula profesional al guardar  en caso de tener no o pagina disponible
'BBV-P-423-RQADM-10 JBEJAR 19/04/2017 Pantalla Consulta se toma la ruta del portal del webconfig
'BUG-PD-64 JBEJAR 26/05/2017 SE CORRIGEN EL COMPORTAMIENTO DE LOS DDL AL SELLECIONAR NO O PAGINA NO DISPONIBLE.
Imports System.Data
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlInputFile
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.SN
Imports ProdeskNet.BD
Partial Class aspx_ConsultaCedula
    Inherits System.Web.UI.Page
    Public idPantalla As Integer = 94
    Dim clien As New clsDatosCliente
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
            ddlPerfil.Enabled = False
            Dim clsifai As New clsCuestionarioCedula()
            clsifai.ID_SOLICITUD = Request("sol")
            Dim dsres = clsifai.GetCuestionarioCedula
            If clsifai.strError = "" Then
                ddlRedSocial.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_EXISTE_CEDULA"))
                ddlGeoloca.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_NUMERO_CEDULA"))
                ddlPerfil.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_NOMBRE_LIC"))


            Else
                Master.MensajeError("Error: " + clsifai.StrError)
            End If
        End If

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');", True)

    End Sub

    Protected Sub btnLinkedin_click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirFB", "window.open('" + ConfigurationManager.AppSettings("PaginaCedula").ToString + "','popupifai','width=1800,height=1000,left=-10,top=0,resizable');", True)
    End Sub

    Protected Sub ddlRedSocial_select_index(sender As Object, e As EventArgs)
        If ddlRedSocial.SelectedValue = 0 Or ddlRedSocial.SelectedValue = 2 Then
            ddlGeoloca.Enabled = False
            ddlPerfil.Enabled = False
            ddlPerfil.Enabled = False

            btnProcesar.Enabled = True
            ddlGeoloca.SelectedValue = -1
            ddlPerfil.SelectedValue = -1

        Else
            ddlGeoloca.Enabled = True
            ddlGeoloca.SelectedValue = -1
            ddlPerfil.SelectedValue = -1
            ddlPerfil.Enabled = True
            ddlPerfil.Enabled = True
            btnProcesar.Enabled = True

        End If
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        ddlRedSocial.SelectedValue = -1
        ddlGeoloca.SelectedValue = -1
        ddlPerfil.SelectedValue = -1


    End Sub
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub
    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs)
        Try
            btnProcesar.Enabled = False
            Dim clscuestionarioF As New clsCuestionarioCedula
            Dim ds As New DataSet
            clscuestionarioF.ID_SOLICITUD = hdSolicitud.Value
            clscuestionarioF.CEDULA = ddlRedSocial.SelectedValue
            clscuestionarioF.NUMERO = ddlGeoloca.SelectedValue
            clscuestionarioF.NOMBRE = ddlPerfil.SelectedValue
            If clscuestionarioF.insertaDatosCedula Then
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Avanza_Tarea", "btnManejoMensaje('exec sp_validarEntrevista " + hdSolicitud.Value.ToString + ",2," + idPantalla.ToString + "', 'exec spValNegocio " + hdSolicitud.Value.ToString + ",64," + hdusuario.Value.ToString + "; select dbo.fnGetMensajeTarea (" + hdSolicitud.Value.ToString + ", " + idPantalla.ToString + ")')", True)
            Else
                Throw New Exception("Error: Ocurrio un problema al guardar el cuestionario")
            End If
            btnProcesar.Enabled = True
        Catch ex As Exception
            Master.MensajeError(ex.Message)
            btnProcesar.Enabled = True
        End Try

    End Sub

End Class

