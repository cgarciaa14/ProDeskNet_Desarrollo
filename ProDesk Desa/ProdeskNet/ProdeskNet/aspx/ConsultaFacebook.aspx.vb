﻿#Region "trackers"
'--BBV-P-423--RQADM-16 JBB 21/03/2017 Pantalla Consulta Portal Facebook.   
'--BUG-PD-37 JBB 24/04/17  : Correciones redes solicitudes.
'--BBV-P-423-RQADM-10 JBEJAR 19/04/2017 Pantalla Consulta se toma la ruta del portal del webconfig.
'--BUG-PD-64 JBEJAR 26/05/2017 Simplificacion de las preguntas.   
'--BUG-PD-247 JBEJAR 27/10/2017 SE AGREGA CORREO ELECTRONICO Y VISOR DOCUMENTAL.
#End Region
Imports System.Data
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlInputFile
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.SN
Imports ProdeskNet.BD
Partial Class aspx_ConsultaFacebook
    Inherits System.Web.UI.Page
    Public idPantalla As Integer = 90
    Dim clien As New clsDatosCliente
    Dim sol As New clsStatusSolicitud
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim intEnable As Integer
        Dim _corrreo As String
        clien.GetDatosCliente(Request("sol"))
        _corrreo = clien.GetEmailCliente(Request("sol"))
        sol.getStatusSol(Request("sol"))
        hdPantalla.Value = Request("idPantalla")
        hdSolicitud.Value = Request("sol")
        Session("idsol") = hdSolicitud.Value
        hdusuario.Value = Request("usu")
        Me.lblSolicitud.Text = Request("sol")
        Me.lblCliente.Text = clien.propNombreCompleto
        Me.lblStCredito.Text = sol.PStCredito
        Me.lblStDocumento.Text = sol.PStDocumento
        Me.LblSCorreo.Text = _corrreo.ToString()
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
            btnFacebook.Enabled = False
            btnProcesar.Attributes.Add("style", "display:none;")
            btnLimpiar.Attributes.Add("style", "display:none;")
            btnCancelar.Attributes.Add("style", "display:none;")
            ddlRedSocial.Enabled = False
            ddlGeoloca.Enabled = False
            ddlPerfil.Enabled = False
            ddlPublicaciones.Enabled = False
            ddlEmpleo.Enabled = False
            TextBox1.Enabled = False
            Dim clsifai As New clsCuestionarioFacebook()
            clsifai._ID_SOLICITUD = Request("sol")
            Dim dsres = clsifai.GetCuestionarioFACE
            If clsifai.StrError = "" Then
                ddlRedSocial.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_PORTAL_DISP"))
                'ddlGeoloca.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_LOC_GEO"))
                'If Not IsDBNull(dsres.Tables(0).Rows(0).Item("PDK_ANIO_APERTURA")) Then
                'hfFeIni.Value = CDate(dsres.Tables(0).Rows(0).Item("PDK_ANIO_APERTURA"))
                'Else
                '    hfFeIni.Value = Nothing
                'End If
                ddlPerfil.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_FOTO_PERF"))
                'ddlPublicaciones.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_PUBLI_REC"))
                'ddlEmpleo.SelectedValue = 1
                'TextBox1.Text = CDbl(dsres.Tables(0).Rows(0).Item("PDK_AMIGOS"))
            Else
                Master.MensajeError("Error: " + clsifai.StrError)
            End If
        End If

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');", True)

    End Sub

    Protected Sub btnFacebook_click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirFB", "window.open('" + ConfigurationManager.AppSettings("PaginaFacebook").ToString + "','popupifai','width=1800,height=1000,left=-10,top=0,resizable');", True)
    End Sub

    Protected Sub ddlRedSocial_select_index(sender As Object, e As EventArgs)
        If ddlRedSocial.SelectedValue = 0 Or ddlRedSocial.SelectedValue = 2 Then
            ddlGeoloca.Enabled = False
            ddlPublicaciones.Enabled = False
            ddlPerfil.Enabled = False
            ddlEmpleo.Enabled = False
            TextBox1.Enabled = False
            btnProcesar.Enabled = True
            ddlGeoloca.SelectedValue = -1
            ddlPerfil.SelectedValue = -1
            ddlPublicaciones.SelectedValue = -1
            TextBox1.Text = ""
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "desf", "$('#feIni').attr('disabled', true);", True)
        Else
            ddlGeoloca.Enabled = True
            ddlPublicaciones.Enabled = True
            ddlPerfil.Enabled = True
            ddlPerfil.Enabled = True
            ddlEmpleo.Enabled = True
            TextBox1.Enabled = True
            TextBox1.Text = ""
            btnProcesar.Enabled = True
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "deshf", "$('#feIni').attr('disabled', false);", True)
        End If
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        ddlRedSocial.SelectedValue = -1
        ddlGeoloca.SelectedValue = -1
        ddlPerfil.SelectedValue = -1
        ddlPublicaciones.SelectedValue = -1
        TextBox1.Text = ""


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
            Dim clscuestionarioF As New clsCuestionarioFacebook
            Dim DATEINI As Date? = Date.MinValue
            DateTime.TryParse(hfFeIni.Value.ToString(), DATEINI)
            'DATEINI = IIf(DATEINI = Date.MinValue, Nothing, DATEINI)
            'Dim DATEINI As Date = DateTime.Parse(hfFeIni.Value.ToString)
            Dim ds As New DataSet
            clscuestionarioF._ID_SOLICITUD = hdSolicitud.Value
            clscuestionarioF._DISP = ddlRedSocial.SelectedValue
            If DATEINI = Date.MinValue Then
                clscuestionarioF._ANIO_APERTURA = String.Empty
            Else
                clscuestionarioF._ANIO_APERTURA = "1999/99/99"
            End If
            clscuestionarioF._LOC_GEO = 1
            clscuestionarioF._FOTO_PER = ddlPerfil.SelectedValue
            If TextBox1.Text = String.Empty Then
                clscuestionarioF._AMIGOS = 1
            Else
                clscuestionarioF._AMIGOS = 1
            End If
            clscuestionarioF._PUBLI_REC = 1
            clscuestionarioF._EMPLEO = 1
            If clscuestionarioF.insertaDatosFacebook() Then

                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Avanza_Tarea", "btnManejoMensaje('exec sp_validarEntrevista " + hdSolicitud.Value.ToString + ",2," + idPantalla.ToString + "', 'exec spValNegocio " + hdSolicitud.Value.ToString + ",64," + hdusuario.Value.ToString + "; select dbo.fnGetMensajeTarea (" + hdSolicitud.Value.ToString + ", " + idPantalla.ToString + ")')", True)
            Else
                Throw New Exception("Error: Ocurrio un problema al guardar el cuestionario")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
            btnProcesar.Enabled = True
        End Try

    End Sub

    Protected Sub btnVisor_Click(sender As Object, e As EventArgs)
        Dim ds As DataSet
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable();", True)

        objCatalogos.Parametro = Me.lblSolicitud.Text

        ds = objCatalogos.Catalogos(7)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Dim Cliente_bbva As String = ""

                Cliente_bbva = ds.Tables(0).Rows(0).Item("CLIENTE_BBVA")

                If Cliente_bbva <> "" Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "&cliente=" + Cliente_bbva.ToString + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)

                End If
            End If
        End If
    End Sub


End Class
