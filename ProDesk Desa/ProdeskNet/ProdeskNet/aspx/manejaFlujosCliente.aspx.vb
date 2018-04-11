'BBVA-PD-BUG-PD-112: 28/06/2017: CGARCIA: SE REPLICO LA PANTALLA DE MANEJA FLUJO SISTEMA, CON ALGUNOS CAMBIOS 
'BBVA: BUG-PD-194: 22/08/2017: CGARCIA: SE QUITARON CONTROLES QUE NO REQUIERE FLUJOS DE CLIENTES

Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Data


Partial Class aspx_manejaFlujosCliente
    Inherits System.Web.UI.Page    

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Val(Request("idFlujo")) > 0 Then
                hdnIdRegistro.Value = Request("idFlujo")
                LlenaCombo(1)
                LlenaCombo(2)
                LlenaCombo(3)
                LlenaCombo(4)
                LlenaCombo(5)
                LlenaCombo(6)
                LlenaCombo(7)
                LlenaCombo(8)
                LlenaCombo(9)
                ObtenFlujo()
                ObtenProceso()
            End If
        End If
    End Sub

    Private Sub LlenaCombo(ByVal intCombo As Integer)
        Dim dsDataset As New DataSet
        Dim intEmpresa As Integer = 0
        Dim i As Integer = 0

        Try
            If intCombo = 1 Then
                dsDataset = clsEmpresa.ObtenTodos(1)
                ddlEmpresa.DataSource = dsDataset
                ddlEmpresa.DataTextField = "PDK_EMP_NOMBRE"
                ddlEmpresa.DataValueField = "PDK_ID_EMPRESA"
                ddlEmpresa.DataBind()
            End If

            If intCombo = 2 Then
                intEmpresa = Val(ddlEmpresa.SelectedValue.ToString)
                dsDataset = clsProductos.ObtenerProductoEmp(intEmpresa, 1)
                ddlProducto.DataSource = dsDataset
                ddlProducto.DataTextField = "PDK_PROD_NOMBRE"
                ddlProducto.DataValueField = "PDK_ID_PRODUCTOS"
                ddlProducto.DataBind()
            End If

            If intCombo = 3 Then
                dsDataset = clsPersonalidadJuridica.ObtenTodos
                ddlPersonalidadJuridica.DataSource = dsDataset
                ddlPersonalidadJuridica.DataTextField = "PDK_PER_NOMBRE"
                ddlPersonalidadJuridica.DataValueField = "PDK_ID_PER_JURIDICA"
                ddlPersonalidadJuridica.DataBind()
            End If

            If intCombo = 4 Then
                dsDataset = clsProcesos.ObtenTodos(Val(hdnIdRegistro.Value))
                ddlProcesoTarea.DataSource = dsDataset
                ddlProcesoTarea.DataTextField = "PDK_PROC_NOMBRE"
                ddlProcesoTarea.DataValueField = "PDK_ID_PROCESOS"
                ddlProcesoTarea.DataBind()
            End If

            If intCombo = 5 Then
                ddlProcesoPadre.Items.Clear()
                dsDataset = clsProcesos.ObtenTodos(hdnIdRegistro.Value)

                ddlProcesoPadre.Items.Add(New ListItem("NINGUNO", "0"))
                For i = 0 To dsDataset.Tables(0).Rows.Count - 1
                    ddlProcesoPadre.Items.Add(New ListItem(dsDataset.Tables(0).Rows(i).Item(2).ToString, dsDataset.Tables(0).Rows(i).Item(0).ToString))
                Next
            End If

            If intCombo = 6 Then
                ddlTareaPadre.Items.Clear()

                dsDataset = clsTareas.ObtenTodos(Session("IdProceso"), 0, hdnIdRegistro.Value)
                ddlTareaPadre.DataSource = dsDataset
                ddlTareaPadre.Items.Add(New ListItem("NINGUNO", "0"))
                For i = 0 To dsDataset.Tables(0).Rows.Count - 1
                    ddlTareaPadre.Items.Add(New ListItem(dsDataset.Tables(0).Rows(i).Item(2).ToString, dsDataset.Tables(0).Rows(i).Item(0).ToString))
                Next
            End If

            If intCombo = 7 Then
                ddlTareaRechazo.Items.Clear()

                dsDataset = clsTareas.ObtenTodos(0, 0, hdnIdRegistro.Value)
                ddlTareaRechazo.DataSource = dsDataset
                ddlTareaRechazo.Items.Add(New ListItem("NINGUNO", "0"))
                For i = 0 To dsDataset.Tables(0).Rows.Count - 1
                    ddlTareaRechazo.Items.Add(New ListItem(dsDataset.Tables(0).Rows(i).Item(2).ToString, dsDataset.Tables(0).Rows(i).Item(0).ToString))
                Next
            End If

            If intCombo = 8 Then
                ddlTareaNoRechazo.Items.Clear()

                dsDataset = clsTareas.ObtenTodos(0, 0, hdnIdRegistro.Value)
                ddlTareaNoRechazo.DataSource = dsDataset
                ddlTareaNoRechazo.Items.Add(New ListItem("NINGUNO", "0"))
                For i = 0 To dsDataset.Tables(0).Rows.Count - 1
                    ddlTareaNoRechazo.Items.Add(New ListItem(dsDataset.Tables(0).Rows(i).Item(2).ToString, dsDataset.Tables(0).Rows(i).Item(0).ToString))
                Next
            End If

            If intCombo = 9 Then
                ddlperdilTarea.Items.Clear()
                dsDataset = clsTareas.ObtenerPerfil()
                ddlperdilTarea.DataSource = dsDataset
                ddlperdilTarea.DataTextField = "PDK_PER_NOMBRE"
                ddlperdilTarea.DataValueField = "PDK_ID_PERFIL"
                ddlperdilTarea.DataBind()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ObtenFlujo()
        Dim intCveFlujo As Integer = Val(hdnIdRegistro.Value)
        Dim dbProducto As New DataSet

        Try
            Dim objFlujo As New clsFlujos(intCveFlujo)

            If objFlujo.PDK_ID_FLUJOS > 0 Then
                lblIdFlujo.Text = objFlujo.PDK_ID_FLUJOS
                ddlEmpresa.SelectedIndex = ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(1))
                LlenaCombo(2)
                LlenaCombo(3)
                ddlProducto.SelectedIndex = ddlProducto.Items.IndexOf(ddlProducto.Items.FindByValue(objFlujo.PDK_ID_PRODUCTOS.ToString))
                ddlPersonalidadJuridica.SelectedIndex = ddlPersonalidadJuridica.Items.IndexOf(ddlPersonalidadJuridica.Items.FindByValue(objFlujo.PDK_ID_PER_JURIDICA.ToString))
                chkStatus.Checked = IIf(objFlujo.PDK_FLU_ACTIVO = 2, True, False)
                Me.txtNombreFlujo.Text = objFlujo.PDK_FLU_NOMBRE
                Me.txtOrden.Text = objFlujo.PDK_FLU_ORDEN

            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bthRegresar.Click
        Dim path As String = "./consultaFlujosCliente.aspx?idProducto=" & Me.ddlProducto.SelectedValue
        'Response.Redirect("./consultaFlujosCliente.aspx?idProducto=" & Me.ddlProducto.SelectedValue)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
    End Sub

    Public Function ValidaDatos() As Boolean
        ValidaDatos = False

        If txtNombreFlujo.Text.Trim.Length <= 0 Then Exit Function
        If txtOrden.Text.Trim.Length <= 0 Then Exit Function
        If Val(txtOrden.Text.Trim) = 0 Then Exit Function
        ValidaDatos = True
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim objFlujo As New clsFlujos

        Try
            If ValidaDatos() Then

                Dim intCvePro As Integer = Val(hdnIdRegistro.Value)
                'Dim objPros As New clsProductos
                objFlujo.PDK_ID_FLUJOS = intCvePro
                objFlujo.PDK_CLAVE_USUARIO = Session("IdUsua")
                objFlujo.PDK_ID_PER_JURIDICA = Val(ddlPersonalidadJuridica.SelectedValue)
                objFlujo.PDK_ID_PRODUCTOS = Val(ddlProducto.SelectedValue)
                objFlujo.PDK_FLU_ORDEN = Val(txtOrden.Text.Trim)
                objFlujo.PDK_FLU_NOMBRE = Me.txtNombreFlujo.Text.Trim.ToUpper
                objFlujo.PDK_FLU_MODIF = Format(Now(), "yyyy-MM-dd")
                objFlujo.PDK_FLU_ACTIVO = IIf(Me.chkStatus.Checked = True, 2, 3)

                objFlujo.Guarda()
                hdnIdRegistro.Value = objFlujo.PDK_ID_FLUJOS
                ObtenFlujo()
            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")

            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        Try
            LlenaCombo(2)
            LlenaCombo(3)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAltaProcesos_Click(sender As Object, e As System.EventArgs) Handles btnAltaProcesos.Click

        Try
            Call mpeAltaProcesos.Show()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnGuardarProcesos_Click(sender As Object, e As System.EventArgs) Handles btnGuardarProcesos.Click

        Dim objProcesos As New clsProcesos

        Try

            objProcesos.PDK_CLAVE_USUARIO = Session("IdUsua")
            objProcesos.PDK_ID_FLUJOS = Val(hdnIdRegistro.Value)
            objProcesos.PDK_PROC_ACTIVO = IIf(chkActivoProc.Checked = True, 2, 3)
            objProcesos.PDK_PROC_MODIF = Format(Now(), "yyyy-MM-dd")
            objProcesos.PDK_PROC_NOMBRE = Me.txtProcesoNombre.Text.Trim.ToUpper
            objProcesos.PDK_PROC_ORDEN = Val(Me.txtOrdenProc.Text.Trim)
            objProcesos.PDK_PROC_PADRE = Val(Me.ddlProcesoPadre.SelectedValue)
            objProcesos.PDK_PROC_PARALLEL = IIf(Me.chkParallelProc.Checked = True, 2, 3)


            If Me.hdnIdProcesos.Value.Trim.Length > 0 Then
                objProcesos.PDK_ID_PROCESOS = Val(Me.hdnIdProcesos.Value)
            End If

            objProcesos.Guarda()
            ObtenProceso()
            LimpiaMPEProcesos()

            LlenaCombo(4)
            LlenaCombo(5)
            LlenaCombo(6)

            Master.MensajeError("Proceso guardado con éxito")
        Catch ex As Exception
            Master.MensajeError("Error al guardar el proceso")
        End Try
    End Sub

    Public Sub LimpiaMPEProcesos()
        Try
            Me.txtProcesoNombre.Text = ""
            Me.txtOrdenProc.Text = ""
            Me.chkParallelProc.Checked = False
            Me.chkActivoProc.Checked = False
            Me.hdnIdProcesos.Value = 0
            ddlProcesoPadre.SelectedIndex = ddlProcesoPadre.Items.IndexOf(ddlProcesoPadre.Items.FindByValue("0"))

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ObtenProceso()

        Dim intFlujo As Integer = 0
        Dim dsDataset As New DataSet
        Dim objProceso As New clsProcesos(0)
        Try

            intFlujo = hdnIdRegistro.Value

            dsDataset = clsProcesos.ObtenTodos(intFlujo)

            If dsDataset.Tables(0).Rows.Count > 0 Then
                DirectCast(AccProcesos.FindControl("grdProcesos"), GridView).DataSource = dsDataset.Tables(0)
                DirectCast(AccProcesos.FindControl("grdProcesos"), GridView).DataBind()
                LblAct.Text = "Procesos (" & dsDataset.Tables(0).Rows.Count & ")"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAltaTareas_Click(sender As Object, e As System.EventArgs) Handles btnAltaTareas.Click
        Try

            Me.hdnIdTarea.Value = 0
            Me.hdnIdTareaPerfil.Value = 0
            LimpiaMPETareas()
            Call Me.mpeAltaTareas.Show()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnGuardaTareas_Click(sender As Object, e As System.EventArgs) Handles btnGuardaTareas.Click
        Dim intProceso As Integer = 0
        Dim objTareas As New clsTareas()

        Try
            intProceso = Val(ddlProcesoTarea.SelectedValue)
            objTareas.PDK_CLAVE_USUARIO = Session("IdUsua")
            objTareas.PDK_ID_PROCESOS = intProceso
            objTareas.PDK_TAR_ACTIVO = IIf(Me.chkTareaActivo.Checked = True, 2, 3)
            objTareas.PDK_TAR_MODIF = Format(Now(), "yyyy-MM-dd")
            objTareas.PDK_TAR_NOMBRE = Me.txtTareaNombre.Text.Trim.ToUpper
            objTareas.PDK_TAR_ORDEN = Val(Me.txtTareaOrden.Text.Trim)
            objTareas.PDK_TAR_PADRE = Val(Me.ddlTareaPadre.SelectedValue)
            objTareas.PDK_TAR_PARALEL = IIf(Me.chkTareaParallel.Checked = True, 2, 3)
            objTareas.PDK_ID_TAREAS_RECHAZO = Val(ddlTareaRechazo.SelectedValue)
            objTareas.PDK_ID_TAREAS_NORECHAZO = Val(ddlTareaNoRechazo.SelectedValue)
            'objTareas.PDK_TAR_DICTAME = Val(ddldictamen.SelectedValue)
            objTareas.PDK_ID_PERFIL = Val(ddlperdilTarea.SelectedValue)

            If Me.hdnIdTarea.Value.Trim.Length > 0 Then
                objTareas.PDK_ID_TAREAS = Val(Me.hdnIdTarea.Value)
                objTareas.PDK_ID_REL_TAR_PERFIL = Val(Me.hdnIdTareaPerfil.Value)
            End If

            objTareas.Guarda()
            objTareas.GuardaTareaPerfil()


            LimpiaMPETareas()

            ObtenTareas(Session("IdProceso"))
            LlenaCombo(6)
            LlenaCombo(7)
            LlenaCombo(8)


            Master.MensajeError("Tarea guardada con éxito")

        Catch ex As Exception
            Master.MensajeError("Error al guardar la tarea")
        End Try
    End Sub

    Public Sub LimpiaMPETareas()
        Try
            ddlProcesoTarea.SelectedValue = 0
            ddlTareaPadre.SelectedValue = 0

            Me.hdnIdTarea.Value = 0
            Me.txtTareaNombre.Text = String.Empty
            Me.txtTareaOrden.Text = String.Empty
            Me.chkTareaParallel.Checked = False
            Me.chkTareaActivo.Checked = False

            ddlProcesoTarea.SelectedIndex = ddlProcesoTarea.Items.IndexOf(ddlProcesoTarea.Items.FindByValue(Session("IdProceso").ToString))
            ddlTareaPadre.SelectedIndex = ddlTareaPadre.Items.IndexOf(ddlTareaPadre.Items.FindByValue("0"))
            ddlTareaRechazo.SelectedIndex = ddlTareaRechazo.Items.IndexOf(ddlTareaRechazo.Items.FindByValue("0"))
            ddlTareaNoRechazo.SelectedIndex = ddlTareaNoRechazo.Items.IndexOf(ddlTareaNoRechazo.Items.FindByValue("0"))

        Catch ex As Exception

            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Sub ObtenTareas(Optional ByVal intProceso As Integer = 0)

        Dim objTareas As New clsTareas
        Dim dsDataset As New DataSet

        Try
            DirectCast(AccTareas.FindControl("grdTareas"), GridView).DataSource = Nothing
            DirectCast(AccTareas.FindControl("grdTareas"), GridView).DataBind()
            lblTareas.Text = "Tareas "


            dsDataset = clsTareas.ObtenTodos(intProceso, 0, hdnIdRegistro.Value)
            If dsDataset.Tables(0).Rows.Count > 0 Then
                DirectCast(AccTareas.FindControl("grdTareas"), GridView).DataSource = dsDataset.Tables(0)
                DirectCast(AccTareas.FindControl("grdTareas"), GridView).DataBind()
                lblTareas.Text = "Tareas (" & dsDataset.Tables(0).Rows.Count & ")"
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Public Sub lkbProc_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim btn = sender
        Dim args = btn.CommandArgument.ToString
        Dim intProceso As Integer = 0
        Dim selectedRow As GridViewRow
        Dim intIndex As Integer = 0
        Dim i As Integer = 0

        Try
            intProceso = Val(args)

            For i = 0 To Me.grdProcesos.Rows.Count - 1
                Me.grdProcesos.Rows(i).BackColor = Drawing.Color.White
            Next

            Session("IdProceso") = intProceso
            ddlProcesoTarea.SelectedIndex = ddlProcesoTarea.Items.IndexOf(ddlProcesoTarea.Items.FindByValue(Session("IdProceso").ToString))

            selectedRow = CType((CType(sender, LinkButton)).Parent.Parent, GridViewRow)
            selectedRow.RowState = DataControlRowState.Selected
            selectedRow.BackColor = Drawing.Color.LightBlue
            selectedRow.RowState = DataControlRowState.Normal


            ObtenTareas(intProceso)
            LlenaCombo(6)



        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Public Sub btnMod_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn = sender
        Dim args = btn.CommandArgument.ToString
        Dim intTarea As Integer = 0
        Dim intRelPerfil As Integer = 0
        intTarea = Val(args)

        Dim objTarea As New clsTareas(intTarea)

        Dim intProcesoTarea As Integer = Val(Session("IdProceso"))
        Dim intTareaPadre As Integer = 0
        Dim intTareaRechazo As Integer = 0
        Dim intTareaNoRechazo As Integer = 0
        Dim intTareaPerfil As Integer = 0
        Dim intTareaDictame As Integer = 0


        Try
            LimpiaMPETareas()

            Me.hdnIdTarea.Value = intTarea
            Me.hdnIdTareaPerfil.Value = objTarea.PDK_ID_REL_TAR_PERFIL
            Me.txtTareaNombre.Text = objTarea.PDK_TAR_NOMBRE
            Me.txtTareaOrden.Text = objTarea.PDK_TAR_ORDEN
            Me.chkTareaParallel.Checked = IIf(objTarea.PDK_TAR_PARALEL = 2, True, False)
            Me.chkTareaActivo.Checked = IIf(objTarea.PDK_TAR_ACTIVO = 2, True, False)


            intTareaPadre = objTarea.PDK_TAR_PADRE
            intTareaRechazo = objTarea.PDK_ID_TAREAS_RECHAZO
            intTareaNoRechazo = objTarea.PDK_ID_TAREAS_NORECHAZO
            intTareaDictame = objTarea.PDK_TAR_DICTAME
            intTareaPerfil = objTarea.PDK_ID_PERFIL



            ddlProcesoTarea.SelectedIndex = ddlProcesoTarea.Items.IndexOf(ddlProcesoTarea.Items.FindByValue(Session("IdProceso").ToString))
            ddlTareaPadre.SelectedIndex = ddlTareaPadre.Items.IndexOf(ddlTareaPadre.Items.FindByValue(intTareaPadre.ToString))
            ddlTareaRechazo.SelectedIndex = ddlTareaRechazo.Items.IndexOf(ddlTareaRechazo.Items.FindByValue(intTareaRechazo.ToString))
            ddlTareaNoRechazo.SelectedIndex = ddlTareaNoRechazo.Items.IndexOf(ddlTareaNoRechazo.Items.FindByValue(intTareaNoRechazo.ToString))
            'ddldictamen.SelectedIndex = ddldictamen.Items.IndexOf(ddldictamen.Items.FindByValue(intTareaDictame.ToString))
            ddlperdilTarea.SelectedIndex = ddlperdilTarea.Items.IndexOf(ddlperdilTarea.Items.FindByValue(intTareaPerfil))



            Call Me.mpeAltaTareas.Show()

        Catch ex As Exception

        End Try

    End Sub

    Public Sub btnPant_Click(ByVal sender As Object, ByVal e As EventArgs)


        Dim btn = sender
        Dim args = btn.CommandArgument.ToString
        Dim intTarea As Integer = 0
        Dim dsDataset As New DataSet

        intTarea = Val(args)

        Dim objTarea As New clsTareas(intTarea)

        Try

            Me.hdnIdTareaPantalla.Value = intTarea
            dsDataset = clsPantallas.ObtenerValidaTarea(intTarea)

            grvPantallas.DataSource = dsDataset
            grvPantallas.DataBind()

            Call Me.mpePantallasAsociadas.Show()

        Catch ex As Exception

        End Try

    End Sub
    
    Public Sub btnModP_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim btn = sender
        Dim args = btn.CommandArgument.ToString
        Dim intProceso As Integer = 0
        intProceso = Val(args)

        Dim objProceso As New clsProcesos(intProceso)

        Try
            LimpiaMPEProcesos()

            Me.hdnIdProcesos.Value = intProceso
            Me.txtProcesoNombre.Text = objProceso.PDK_PROC_NOMBRE
            Me.txtOrdenProc.Text = objProceso.PDK_PROC_ORDEN
            Me.chkParallelProc.Checked = IIf(objProceso.PDK_PROC_PARALLEL = 2, True, False)
            Me.chkActivoProc.Checked = IIf(objProceso.PDK_PROC_ACTIVO = 2, True, False)

            ddlProcesoPadre.SelectedIndex = ddlProcesoPadre.Items.IndexOf(ddlProcesoPadre.Items.FindByValue(objProceso.PDK_PROC_PADRE.ToString))

            Call Me.mpeAltaProcesos.Show()

        Catch ex As Exception

        End Try
    End Sub

  
End Class
