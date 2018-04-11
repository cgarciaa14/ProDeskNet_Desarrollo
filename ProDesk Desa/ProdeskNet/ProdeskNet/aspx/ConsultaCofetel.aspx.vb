'BBV-P-423-RQADM-10 JBEJAR 19/04/2017 Pantalla Consulta cofetel. 
'BUG-PD-71 JBEJAR 05/06/2017 se agrega redirect dinamico para tareas automaticas.
Imports System.Data
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlInputFile
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.SN
Imports ProdeskNet.BD
Partial Class aspx_ConsultaCedula
    Inherits System.Web.UI.Page
    Public idPantalla As Integer = 99
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
        Dim dsresulta As New DataSet

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
                hdnResultado1.Value = dsresult.Tables(1).Rows(0).Item("RUTA2")
                hdnResultado2.Value = dsresult.Tables(2).Rows(0).Item("RUTA3")
            End If

        Catch ex As Exception
            hdnResultado.Value = Session("Regresar")
        End Try

        Try
            dsresulta = BD.EjecutarQuery("get_Path_Next_Tarea  " & hdPantalla.Value)
            If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
                Dim Mostrar As String
                Dim pantallas As String
                Mostrar = dsresulta.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR").ToString
                pantallas = dsresulta.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString
                If Mostrar = 2 Then

                    hdnResultado.Value = dsresult.Tables(0).Rows(0).Item("RUTA")
                Else
                    hdnResultado.Value = (dsresulta.Tables(0).Rows(0).Item("RUTA") & "?sol=" & hdSolicitud.Value & "&IdPantalla=" & pantallas & "&usuario=" & hdusuario.Value)
                    hdnResultado2.Value = (dsresulta.Tables(0).Rows(0).Item("RUTA") & "?sol=" & hdSolicitud.Value & "&IdPantalla=" & pantallas & "&usuario=" & hdusuario.Value)
                End If
            End If

            intEnable = CInt(Request.QueryString("Enable"))

        Catch ex As Exception
            hdnResultado.Value = Session("Regresar")
        End Try

        If intEnable = 1 Then
            btnCofetel.Enabled = False
            btnQuien.Enabled = False
            btnProcesar.Attributes.Add("style", "display:none;")
            btnLimpiar.Attributes.Add("style", "display:none;")
            btnCancelar.Attributes.Add("style", "display:none;")
            TextBox2.Enabled = False
            TextBox1.Enabled = False
            ddlRedSocial.Enabled = False
            ddlGeoloca.Enabled = False
            ddlExitelm.Enabled = False
            ddlTelfm.Enabled = False
            ddlexitem.Enabled = False
            ddltelref1.Enabled = False
            ddltelref2.Enabled = False
            btnCofetel.Enabled = False

            Dim clsifai As New clsCuestionarioCofetel()
            clsifai.ID_SOLICITUD = Request("sol")
            Dim dsres = clsifai.GetCuestionarioCofetel
            If clsifai.strError = "" Then
                ddlRedSocial.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_EXISTE_TEL_FIJO_SOL"))
                ddlGeoloca.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_NUMERO_TEL_MOVIL"))
                TextBox1.Text = dsres.Tables(0).Rows(0).Item("PDK_NOMBRE_COMPANIA").ToString()
                ddlExitelm.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_EXISTE_TEL_MOVILF"))
                ddlTelfm.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_EXISTE_TEL_FIJOF"))
                TextBox2.Text = dsres.Tables(0).Rows(0).Item("PDK_NOMBRE_COMPANIA1").ToString()
                ddlexitem.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_EXISTE_TEL_EMPRESA"))
                ddltelref1.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_EXISTE_TEL_REFERENCIA1"))
                ddltelref2.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_EXISTE_TEL_REFERENCIA2"))



            Else
                Master.MensajeError("Error: " + clsifai.strError)
            End If
        End If

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');", True)

    End Sub
    ''Protected Sub Bloqueo()

    ''    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable();", True)
    ''    ddlRedSocial.SelectedValue = -1
    ''    ddlGeoloca.SelectedValue = -1
    ''    TextBox1.Text = ""
    ''    TextBox2.Text = ""
    ''    ddlExitelm.SelectedValue = -1
    ''    ddlTelfm.SelectedValue = -1
    ''    ddlexitem.SelectedValue = -1
    ''    ddltelref1.SelectedValue = -1
    ''    ddltelref2.SelectedValue = -1
    ''    ddlGeoloca.Enabled = False
    ''    btnProcesar.Enabled = False
    ''    ddlExitelm.Enabled = False
    ''    TextBox1.Enabled = False
    ''    ddlTelfm.Enabled = False
    ''    TextBox2.Enabled = False
    ''    ddlRedSocial.Enabled = False
    ''    ddlGeoloca.Enabled = False
    ''    ddlExitelm.Enabled = False
    ''    ddlTelfm.Enabled = False
    ''    ddlexitem.Enabled = False
    ''    ddltelref1.Enabled = False
    ''    ddltelref2.Enabled = False
    ''    btnCofetel.Enabled = False
    ''    btnQuien.Enabled = False
    ''    btnProcesar.Attributes.Add("style", "display:none;")
    ''    btnLimpiar.Attributes.Add("style", "display:none;")
    ''    btnCancelar.Attributes.Add("style", "display:none;")

    ''End Sub

    Protected Sub btnQuien_click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirFQ", "window.open('" + ConfigurationManager.AppSettings("PaginaQuien").ToString + "','popupifai','width=1800,height=1000,left=-10,top=0,resizable');", True)
    End Sub
    Protected Sub btnCofetel_click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirFC", "window.open('" + ConfigurationManager.AppSettings("PaginaCofetel").ToString + "','popupifai1','width=1800,height=1000,left=-10,top=0,resizable');", True)
    End Sub

    Protected Sub ddlRedSocial_select_index(sender As Object, e As EventArgs)
        If ddlRedSocial.SelectedValue = 0 Or ddlRedSocial.SelectedValue = 2 Then
            ddlGeoloca.Enabled = False
            btnProcesar.Enabled = True
            ddlGeoloca.SelectedValue = -1
            TextBox1.Enabled = False
            TextBox1.Text = ""
        Else
            ddlGeoloca.Enabled = True
            btnProcesar.Enabled = True
            ddlExitelm.Enabled = True
            TextBox1.Enabled = True
        End If
    End Sub
    Protected Sub ddlExitelm_select_index(sender As Object, e As EventArgs)
        If ddlExitelm.SelectedValue = 0 Or ddlExitelm.SelectedValue = 2 Then
            ddlTelfm.Enabled = False
            ddlTelfm.SelectedValue = -1
            TextBox2.Enabled = False
            TextBox2.Text = ""
        Else
            ddlTelfm.Enabled = True
            TextBox2.Enabled = True
        End If
    End Sub
    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        ddlRedSocial.SelectedValue = -1
        ddlGeoloca.SelectedValue = -1
        TextBox1.Text = ""
        TextBox2.Text = ""
        ddlExitelm.SelectedValue = -1
        ddlTelfm.SelectedValue = -1
        ddlexitem.SelectedValue = -1
        ddltelref1.SelectedValue = -1
        ddltelref2.SelectedValue = -1
        ddlGeoloca.Enabled = True
        btnProcesar.Enabled = True
        ddlExitelm.Enabled = True
        TextBox1.Enabled = True
        ddlTelfm.Enabled = True
        TextBox2.Enabled = True

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
            Dim clscuestionarioF As New clsCuestionarioCofetel
            Dim ds As New DataSet
            clscuestionarioF.ID_SOLICITUD = hdSolicitud.Value
            clscuestionarioF.EXISTE_TEL_FIJO_SOL = ddlRedSocial.SelectedValue
            clscuestionarioF.NUMERO_TEL_MOVIL = ddlGeoloca.SelectedValue
            clscuestionarioF.NOMBRE_COMPANIA = TextBox1.Text
            clscuestionarioF.EXISTE_TEL_MOVILF = ddlExitelm.SelectedValue
            clscuestionarioF.EXISTE_TEL_FIJOF = ddlTelfm.SelectedValue
            clscuestionarioF.NOMBRE_COMPANIA1 = TextBox2.Text
            clscuestionarioF.EXISTE_TEL_EMPRESA = ddlexitem.SelectedValue
            clscuestionarioF.EXISTE_TEL_REFERENCIA1 = ddltelref1.SelectedValue
            clscuestionarioF.EXISTE_TEL_REFERENCIA2 = ddltelref2.SelectedValue
            If clscuestionarioF.insertaDatosCofetel Then
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


