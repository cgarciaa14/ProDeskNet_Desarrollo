Imports ProdeskNet.Catalogos
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Configurcion
Imports System.Data

Public Class SeccionDatos

#Region "trackers"
    ' BBV-P-423  RQSOL-02  gvargas   15/12/2016 Se moficaron los metodos "bthGuardar_Click" y "grvConsulta_RowCommand" para guardar en la base de datos el input ToolTip.
    '                                           Se modifico el metodo "LimpiarDatos()", ahora limpia el input para "ToolTip".
#End Region

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("catIdSeccio")) > 0 Then
                hdnIdRegistro.Value = Request("catIdSeccio")
                lblCveSeccion.Text = Request("catIdSeccio")
                Dim dbjSecc As New clsSeccion(Request("catIdSeccio"))
                If dbjSecc.PDK_SEC_CREACION = 2 Then
                    cmbLlave.Enabled = False
                End If
                CargarTipoObj()
                CargarCampos()
                CargarLlaves()
                CargaDatos()
            End If
        End If
    End Sub
    Private Sub CargarTipoObj()
        Try
            Dim dbDta As New DataSet
            Dim objCombo As New clsParametros

            dbDta = clsTipoObjeto.ObtenTodos()
            If dbDta.Tables.Count > 0 AndAlso dbDta.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dbDta, "PDK_TIP_OBJ_NOMBRE", "PDK_ID_TIPO_OBJETO", cmbObjeto)
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Private Sub CargarCampos()
        Try
            Dim dbDta As New DataSet
            Dim objCombo As New clsParametros

            dbDta = objCombo.ObtenerParametro(60)
            If dbDta.Tables.Count > 0 AndAlso dbDta.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dbDta, "PDK_PAR_SIS_PARAMETRO", "PDK_ID_PARAMETROS_SISTEMA", cmbCampo)
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub
    Private Sub CargarLlaves()
        Try
            Dim dbLlave As New DataSet
            Dim objCombo As New clsParametros

            dbLlave = clsSeccionDato.ObtenerLlaves()
            If dbLlave.Tables.Count > 0 AndAlso dbLlave.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dbLlave, "PDK_NOMBRE_TABLA", "PDK_ID_LLAVE", cmbLlave)
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Private Sub CargaDatos()
        Try
            Dim dtsRes As New DataSet
            Dim strOrden As String = ""

            Dim intClave As Integer = Val(hdnIdRegistro.Value)
            dtsRes = clsSeccionDato.ObtenTodos(intClave)


            Session("dtsConsulta") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing

            If ViewState("strCampos") <> String.Empty Then
                strOrden = ViewState("strCampos") & " " & ViewState("strOrden")
            End If

            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    dtsRes.Tables(0).DefaultView.Sort = strOrden.ToUpper ' Se realizo un order al Data Set 
                    Session("dtsConsulta") = dtsRes
                    grvConsulta.DataSource = dtsRes.Tables(0).DefaultView ' Se agrego el DefaultView para que se mostrara ordenado
                Else
                    Master.MensajeError("No se encontró información con los parámetros proporcionados")
                End If
            Else
                Master.MensajeError("No se encontró información para los parámetros proporcionados")
            End If

            grvConsulta.DataBind()
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub

    Private Sub grvConsulta_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        Dim dts As New DataSet
        Dim strOrden As String = ""

        grvConsulta.PageIndex = e.NewPageIndex
        dts = CType(Session("dtsConsulta"), DataSet)

        If ViewState("strCampos") <> String.Empty Then
            strOrden = ViewState("strCampos") & " " & ViewState("strOrden")
        End If

        dts.Tables(0).DefaultView.Sort = strOrden.ToUpper ' Se realizo un order al Data Set 
        grvConsulta.DataSource = dts.Tables(0).DefaultView
        grvConsulta.DataBind()
    End Sub
    Protected Sub grvConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand

        If e.CommandName = "catIDSeccDato" Then
            Try
                Dim intClave As Integer = Val(hdnIdRegistro.Value)
                Dim objSeccDatos As New clsSeccionDato(e.CommandArgument)

                If objSeccDatos.PDK_ID_SECCION_DATO > 0 Then
                    lblCveDatos.Text = objSeccDatos.PDK_ID_SECCION_DATO
                    lblCveSeccion.Text = objSeccDatos.PDK_ID_SECCION
                    txtnombre.Text = objSeccDatos.PDK_SEC_DAT_NOMBRE
                    txtlongu.Text = objSeccDatos.PDK_SEC_DAT_LONGUITUD
                    txtMostrar.Text = objSeccDatos.PDK_SEC_MOSTRA_PANT
                    txtTool.Text = objSeccDatos.PDK_TOOLTIP
                    'chkLlave.Checked = IIf(objSeccDatos.PDK_SEC_DAT_LLAVE = 1, True, False)                   
                    chkStatus.Checked = IIf(objSeccDatos.PDK_SEC_DAT_STATUS = 2, True, False)
                    'CargarLlaves()
                    cmbLlave.SelectedIndex = cmbLlave.Items.IndexOf(cmbLlave.Items.FindByValue(objSeccDatos.PDK_SEC_DAT_LLAVE))
                    If objSeccDatos.PDK_ID_TIPO_OBJETO > 0 Then
                        CargarTipoObj()
                        cmbObjeto.SelectedIndex = cmbObjeto.Items.IndexOf(cmbObjeto.Items.FindByValue(objSeccDatos.PDK_ID_TIPO_OBJETO))
                    End If
                    If objSeccDatos.PDK_SEC_TIPO_CAMPO_OBJETO > 0 Then
                        CargarCampos()
                        cmbCampo.SelectedIndex = cmbCampo.Items.IndexOf(cmbCampo.Items.FindByValue(objSeccDatos.PDK_SEC_TIPO_CAMPO_OBJETO))
                    End If

                    If cmbLlave.SelectedValue > 0 Then
                        txtnombre.Enabled = False
                    Else
                        txtnombre.Enabled = True
                    End If

                Else
                    Master.MensajeError("No se localizó información del Tipo de Objeto")
                End If


            Catch ex As Exception
                Master.MensajeError(ex.Message)
            End Try

        ElseIf e.CommandName = "Sort" Then
            If ViewState("strCampos") = e.CommandArgument Then
                If ViewState("strOrden") = "Asc" Then
                    ViewState("strOrden") = "Desc"
                    Session("sBandera") = 1
                Else
                    ViewState("strOrden") = "Asc"
                    Session("sBandera") = 1
                End If
            Else
                ViewState("strCampos") = e.CommandArgument
                ViewState("strOrden") = "Asc"
                Session("sBandera") = 1
            End If
        End If
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaSeccion.aspx")
    End Sub
    Private Function ValidaCampos() As Boolean
        ValidaCampos = False
        If Trim(txtnombre.Text).Length = 0 Then Exit Function
        If Trim(txtlongu.Text).Length = 0 Then Exit Function
        If Trim(txtMostrar.Text).Length = 0 Then Exit Function
        ValidaCampos = True
    End Function
    Protected Sub bthGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim dbDaset As New DataSet
                Dim objDatoSecc As New clsSeccionDato
                Dim objSecc As New clsSeccion(Val(hdnIdRegistro.Value))
                Dim intClave As Integer = Val(hdnIdRegistro.Value)
                Dim objTipo As New clsTipoObjeto(cmbObjeto.SelectedValue)
                objDatoSecc.PDK_SEC_DAT_NOMBRE = clsParametros.ReplaceAll(txtnombre.Text.Trim.ToUpper, " ", "_")
                objDatoSecc.PDK_SEC_DAT_LONGUITUD = txtlongu.Text.Trim.ToUpper
                objDatoSecc.PDK_SEC_DAT_MODIF = Format(Now(), "yyyy-MM-dd HH:mm:ss")
                objDatoSecc.PDK_SEC_DAT_STATUS = IIf(chkStatus.Checked = True, 2, 3)
                objDatoSecc.PDK_SEC_MOSTRA_PANT = txtMostrar.Text.Trim
                objDatoSecc.PDK_TOOLTIP = txtTool.Text.Trim
                'objDatoSecc.PDK_SEC_DAT_LLAVE = IIf(chkLlave.Checked = True, 1, 0)
                objDatoSecc.PDK_SEC_DAT_LLAVE = cmbLlave.SelectedValue
                objDatoSecc.PDK_ID_TIPO_OBJETO = cmbObjeto.SelectedValue
                objDatoSecc.PDK_SEC_TIPO_CAMPO_OBJETO = cmbCampo.SelectedValue
                objDatoSecc.PDK_CLAVE_USUARIO = Session("IdUsua")
                objDatoSecc.PDK_ID_SECCION = intClave

                If lblCveDatos.Text = "" Then
                    dbDaset = clsSeccionDato.ValidadDatos(clsParametros.ReplaceAll(txtnombre.Text.Trim.ToUpper, " ", "_"), Val(hdnIdRegistro.Value))
                    If dbDaset.Tables.Count > 0 AndAlso dbDaset.Tables(0).Rows.Count > 0 Then
                        Master.MensajeError("El nombre ya existe en la tabla ")
                        LimpiarDatos()
                        Exit Sub
                    End If

                    objDatoSecc.Guarda()
                    lblCveDatos.Text = objDatoSecc.PDK_ID_SECCION_DATO

                    If objSecc.PDK_SEC_CREACION = 2 Then
                        If chkStatus.Checked = True Then
                            objDatoSecc.AgregarTablaCam(intClave, objSecc.PDK_SEC_NOMBRE_TABLA, objTipo.PDK_TIP_OBJ_NOMBRE_COD)
                        End If
                    End If

                    If objDatoSecc.ErrorSeccion <> "" Then
                        Master.MensajeError(" Error al guardar PDK_ID_SECCION_DATO")
                    Else
                        Master.MensajeError("Información almacenada exitosamente")
                        LimpiarDatos()
                        CargarTipoObj()
                        CargarCampos()
                    End If
                Else
                    objDatoSecc.PDK_ID_SECCION_DATO = lblCveDatos.Text
                    dbDaset = clsSeccionDato.ValidadDatos(clsParametros.ReplaceAll(txtnombre.Text.Trim.ToUpper, " ", "_"), Val(hdnIdRegistro.Value), lblCveDatos.Text, 1)
                    If dbDaset.Tables.Count > 0 AndAlso dbDaset.Tables(0).Rows.Count > 0 Then
                        Master.MensajeError("El nombre ya existe en la tabla ")
                        LimpiarDatos()
                        Exit Sub
                    End If

                    objDatoSecc.ActualizaRegistro()
                    Master.MensajeError("La información se guardo con éxito")
                    LimpiarDatos()
                End If
                CargaDatos()
            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Public Sub LimpiarDatos()
        txtlongu.Text = ""
        txtnombre.Text = ""
        lblCveDatos.Text = ""
        txtMostrar.Text = ""
        txtTool.Text = ""
        chkStatus.Checked = False
        'chkLlave.Checked = False
        txtnombre.Enabled = True
        CargarLlaves()

    End Sub

    Private Sub cmbLlave_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLlave.SelectedIndexChanged
        Try
            Dim dbData As New DataSet
            If cmbLlave.SelectedValue > 0 Then
                dbData = clsSeccionDato.ObtenerLlaves(cmbLlave.SelectedValue)
                If dbData.Tables.Count > 0 AndAlso dbData.Tables(0).Rows.Count > 0 Then
                    txtnombre.Text = dbData.Tables(0).Rows(0).Item("PDK_NOMBRE_CAMPO").ToString
                    txtlongu.Text = 0
                    txtnombre.Enabled = False
                    cmbObjeto.SelectedIndex = cmbObjeto.Items.IndexOf(cmbObjeto.Items.FindByValue(2))
                End If

            Else
                txtnombre.Text = ""
                txtlongu.Text = ""
                txtnombre.Enabled = True
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
End Class