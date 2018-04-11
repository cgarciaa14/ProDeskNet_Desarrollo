Imports System.Data
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlInputFile
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.SN
Imports ProdeskNet.BD
'BBV-P-423: RQADM-15: JRHM: 17/03/17 SE CREA PANTALLA PARA TAREA DE "CONSULTA INGRESOS EN PORTAL DE TRANSPARENCIA" 
'BBV-P-423: RQADM-22: JRHM: 24/03/17 SE MODIFICA FUNCIONALIDAD DE LA PAGINA DEACUERDO A LAS ESPECIFICACIONES DEL USUARIO 
Partial Class aspx_ConsultaPortalTransparecia
    Inherits System.Web.UI.Page
    Public idPantalla As Integer = 78
    Dim clien As New clsDatosCliente
    Dim sol As New clsStatusSolicitud
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim intEnable As Integer
        Try
            intEnable = CInt(Request.QueryString("Enable"))

        Catch ex As Exception
            intEnable = 0
        End Try

        If Not IsPostBack Then
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

            Dim clsifai As New clsCuestionarioIFAI()
            clsifai._ID_SOLICITUD = Request("sol")
            Dim dsres = clsifai.GetCuestionarioIFAI()
            If clsifai.StrError = "" Then
                If Not IsNothing(dsres) Then
                    ddlIFAIisAvaible.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_PORTAL_DISP"))
                    If intEnable = 0 Then
                        If ddlIFAIisAvaible.SelectedValue = 0 Then
                            ddlExistSolIFAI.Enabled = False
                            ddlExistJobIFAI.Enabled = False
                            ddlPerJobIFAI.Enabled = False
                            txtMontoReg.Enabled = False
                            ddlExistSolIFAI.SelectedValue = 0
                            ddlExistJobIFAI.SelectedValue = 0
                            ddlPerJobIFAI.SelectedValue = 0
                            txtMontoReg.Text = "0"
                        Else
                            ddlExistSolIFAI.Enabled = True
                            ddlExistJobIFAI.Enabled = False
                            ddlPerJobIFAI.Enabled = False
                            txtMontoReg.Enabled = False
                            ddlExistSolIFAI.SelectedValue = -1
                            ddlExistJobIFAI.SelectedValue = -1
                            ddlPerJobIFAI.SelectedValue = -1
                            txtMontoReg.Text = "0"
                        End If
                    End If
                    ddlExistSolIFAI.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_EXIST_SOL"))
                    If intEnable = 0 Then
                        If ddlExistSolIFAI.SelectedValue = 0 Then
                            ddlExistJobIFAI.Enabled = False
                            ddlPerJobIFAI.Enabled = False
                            txtMontoReg.Enabled = False
                            ddlExistSolIFAI.SelectedValue = 0
                            ddlExistJobIFAI.SelectedValue = 0
                            ddlPerJobIFAI.SelectedValue = 0
                            txtMontoReg.Text = "0"
                        Else
                            ddlExistJobIFAI.Enabled = True
                            ddlPerJobIFAI.Enabled = True
                            txtMontoReg.Enabled = True
                            txtMontoReg.Text = ""
                        End If
                    End If
                    ddlExistJobIFAI.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_EXIST_PUESTO"))
                    ddlPerJobIFAI.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_PERSONA_PUESTO"))
                    txtMontoReg.Text = CDbl(dsres.Tables(0).Rows(0).Item("PDK_MONTO_REG"))
                Else
                    ddlExistSolIFAI.Enabled = False
                    ddlExistJobIFAI.Enabled = False
                    ddlPerJobIFAI.Enabled = False
                    txtMontoReg.Enabled = False
                End If
            Else
                Master.MensajeError("Error: " + clsifai.StrError)
            End If
        End If

        If intEnable = 1 Then
            btnlinkIFAI.Enabled = False
            cmbguardar1.Attributes.Add("style", "display:none;")
            btnLimpiar.Attributes.Add("style", "display:none;")
            btnCancelar.Attributes.Add("style", "display:none;")
            ddlIFAIisAvaible.Enabled = False
            ddlExistSolIFAI.Enabled = False
            ddlExistJobIFAI.Enabled = False
            ddlPerJobIFAI.Enabled = False
            txtMontoReg.Enabled = False
        End If



        
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');", True)

    End Sub

    Protected Sub btnlinkIFAI_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirIFAI", "window.open('" + ConfigurationManager.AppSettings("PaginaIFAI").ToString+ "','popupifai','width=1800,height=1000,left=-10,top=0,resizable');", True)
    End Sub

    Protected Sub ddlIFAIisAvaible_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlIFAIisAvaible.SelectedIndexChanged
        If ddlIFAIisAvaible.SelectedValue = 0 Then
            ddlExistSolIFAI.Enabled = False
            ddlExistJobIFAI.Enabled = False
            ddlPerJobIFAI.Enabled = False
            txtMontoReg.Enabled = False
            ddlExistSolIFAI.SelectedValue = 0
            ddlExistJobIFAI.SelectedValue = 0
            ddlPerJobIFAI.SelectedValue = 0
            txtMontoReg.Text = "0"
        ElseIf ddlIFAIisAvaible.SelectedValue = -1 Then
            ddlExistSolIFAI.Enabled = False
            ddlExistJobIFAI.Enabled = False
            ddlPerJobIFAI.Enabled = False
            txtMontoReg.Enabled = False
            ddlExistSolIFAI.SelectedValue = -1
            ddlExistJobIFAI.SelectedValue = -1
            ddlPerJobIFAI.SelectedValue = -1
            txtMontoReg.Text = ""
        Else
            ddlExistSolIFAI.Enabled = True
            ddlExistJobIFAI.Enabled = False
            ddlPerJobIFAI.Enabled = False
            txtMontoReg.Enabled = False
            ddlExistSolIFAI.SelectedValue = -1
            ddlExistJobIFAI.SelectedValue = -1
            ddlPerJobIFAI.SelectedValue = -1
            txtMontoReg.Text = ""
        End If
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        ddlIFAIisAvaible.SelectedValue = -1
        ddlExistSolIFAI.SelectedValue = -1
        ddlExistJobIFAI.SelectedValue = -1
        ddlPerJobIFAI.SelectedValue = -1
        txtMontoReg.Text = ""
        ddlIFAIisAvaible.Enabled = True
        ddlExistSolIFAI.Enabled = False
        ddlExistJobIFAI.Enabled = False
        ddlPerJobIFAI.Enabled = False
        txtMontoReg.Enabled = False
    End Sub
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub
    Protected Sub cmbguardar1_Click(sender As Object, e As EventArgs)
        Try
            cmbguardar1.Enabled = False
            If ddlIFAIisAvaible.SelectedValue <> -1 And ddlExistSolIFAI.SelectedValue <> -1 And ddlPerJobIFAI.SelectedValue <> -1 And ddlExistJobIFAI.SelectedValue <> -1 And txtMontoReg.Text <> "" Then
                If IsNumeric(txtMontoReg.Text) Then
                    Dim clsquiz As New clsCuestionarioIFAI
                    clsquiz._ID_SOLICITUD = hdSolicitud.Value
                    clsquiz._IFAI_DISP = ddlIFAIisAvaible.SelectedValue
                    clsquiz._EXIST_SOL = ddlExistSolIFAI.SelectedValue
                    clsquiz._EXIST_PUESTO = ddlExistJobIFAI.SelectedValue
                    clsquiz._EXIST_REL_PUE_SOL = ddlPerJobIFAI.SelectedValue
                    If clsquiz.InsertDatosSeguroSolicitud() Then
                        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Avanza_Tarea", "document.getElementById('ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1').disabled = 'disabled';btnManejoMensaje('exec sp_validarEntrevista " + hdSolicitud.Value.ToString + ",2," + idPantalla.ToString + "', 'exec spValNegocio " + hdSolicitud.Value.ToString + ",64," + hdusuario.Value.ToString + "; select dbo.fnGetMensajeTarea (" + hdSolicitud.Value.ToString + ", " + idPantalla.ToString + ")'); document.getElementById('ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1').disabled = 'disabled';", True)
                    Else
                        Throw New Exception("Error: Ocurrio un problema al guardar el cuestionario")
                    End If
                Else
                    Throw New Exception("Error: Monto agregado no valido.")
                End If
            Else
                Throw New Exception("Error: Falta contestar preguntas del cuestionario. *")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
            cmbguardar1.Enabled = True
        End Try
    End Sub

    Protected Sub ddlExistSolIFAI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistSolIFAI.SelectedIndexChanged
        If ddlExistSolIFAI.SelectedValue = 0 Then
            ddlExistJobIFAI.Enabled = False
            ddlPerJobIFAI.Enabled = False
            txtMontoReg.Enabled = False
            ddlExistJobIFAI.SelectedValue = 0
            ddlPerJobIFAI.SelectedValue = 0
            txtMontoReg.Text = "0"
        ElseIf ddlExistSolIFAI.SelectedValue = -1 Then
            ddlExistJobIFAI.Enabled = False
            ddlPerJobIFAI.Enabled = False
            txtMontoReg.Enabled = False
            ddlExistJobIFAI.SelectedValue = -1
            ddlPerJobIFAI.SelectedValue = -1
            txtMontoReg.Text = ""
        Else
            ddlExistJobIFAI.Enabled = True
            ddlPerJobIFAI.Enabled = True
            txtMontoReg.Enabled = True
            txtMontoReg.Text = ""
        End If
    End Sub
End Class
