Imports ProdeskNet.Seguridad
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Data

Public Class altaPantalla
    Inherits System.Web.UI.Page
    Dim intPagMax As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"
        Try

            If Not IsPostBack Then
                LlenarEmp()
                LlenarProducto()
                LlenarFlujo()
                Llenarproceso()
                LlenarTarea()
                LlenarSeccion()
                LlenarDocumento()
                BuscarSeccDta()

            End If

        Catch ex As Exception
        End Try
    End Sub
    Private Sub LlenarEmp()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet
            dtsRed = clsEmpresa.ObtenTodos(1)
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_EMP_NOMBRE", "PDK_ID_EMPRESA", ddlEmpresa)
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Private Sub LlenarDocumento()
        Dim objCombo As New clsParametros
        Dim dbtsRe As New DataSet

        Try
            dbtsRe = objCombo.ObtenerParametro(24)
            If dbtsRe.Tables.Count > 0 AndAlso dbtsRe.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dbtsRe, "PDK_PAR_SIS_PARAMETRO", "PDK_ID_PARAMETROS_SISTEMA", ddldocumento)

            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Private Sub Llenarproceso()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet
            dtsRed = clsProcesos.ObtenTodos(ddlFlujo.SelectedValue, 1)
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_PROC_NOMBRE", "PDK_ID_PROCESOS", ddlProceso)
                LlenarTarea()
            Else
                ddlProceso.Items.Clear()
                ddlTarea.Items.Clear()
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub
    Private Sub LlenarTarea()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet
            dtsRed = clsTareas.ObtenTodos(ddlProceso.SelectedValue, 0, ddlFlujo.SelectedValue)
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_TAR_NOMBRE", "PDK_ID_TAREAS", ddlTarea)
            Else
                ddlTarea.Items.Clear()
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub
    Private Sub LlenarSeccion()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet
            dtsRed = clsSeccion.obtenerSeccStatus
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_SEC_NOMBRE_TABLA", "PDK_ID_SECCION", ddlSeccion)
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Private Sub LlenarProducto()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet
            dtsRed = clsProductos.ObtenerProductoEmp(ddlEmpresa.SelectedValue, 1)
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_PROD_NOMBRE", "PDK_ID_PRODUCTOS", ddlProducto)
                LlenarFlujo()
            Else
                ddlProducto.Items.Clear()
                ddlFlujo.Items.Clear()
                ddlProceso.Items.Clear()
                ddlTarea.Items.Clear()
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub
    Private Sub LlenarFlujo()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet
            dtsRed = clsFlujos.ObtenerFlujoProd(ddlProducto.SelectedValue)
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_FLU_NOMBRE", "PDK_ID_FLUJOS", ddlFlujo)
                Llenarproceso()
            Else
                ddlFlujo.Items.Clear()
                ddlProceso.Items.Clear()
                ddlTarea.Items.Clear()
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub
    Private Sub ddlProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProducto.SelectedIndexChanged
        ddlFlujo.Items.Clear()
        ddlProceso.Items.Clear()
        ddlTarea.Items.Clear()
        LlenarFlujo()
        Llenarproceso()
        LlenarTarea()
    End Sub

    Private Sub ddlEmpresa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        ddlProducto.Items.Clear()
        LlenarProducto()
    End Sub

    Private Sub ddlFlujo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFlujo.SelectedIndexChanged
        ddlProceso.Items.Clear()
        ddlTarea.Items.Clear()
        Llenarproceso()
        LlenarTarea()
    End Sub

    Private Sub ddlProceso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProceso.SelectedIndexChanged
        ddlTarea.Items.Clear()
        LlenarTarea()
    End Sub

    Private Sub ddlSeccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSeccion.SelectedIndexChanged
        BuscarSeccDta()
    End Sub
    Private Sub BuscarSeccDta()
        Try
            Dim dtsRes As New DataSet
            Dim strOrden As String = ""

            grdSeccDat.DataSource = Nothing
            grdSeccDat.DataBind()

            dtsRes = clsSeccionDato.ObtenerSeccDta(ddlSeccion.SelectedValue)
            grdSeccDat.DataSource = dtsRes.Tables(0)
            grdSeccDat.DataBind()

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Protected Sub chkTodoMostrar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fila As GridViewRow
        For Each fila In grdSeccDat.Rows
            If CType(grdSeccDat.HeaderRow.FindControl("chkTodoMostrar"), CheckBox).Checked = True Then
                CType(fila.FindControl("chkMostrar"), CheckBox).Checked = True
            Else
                CType(fila.FindControl("chkMostrar"), CheckBox).Checked = False
            End If
        Next
      
    End Sub
    Private Sub ordenarTodos()
        Dim etiqueta As Label
        Dim fila As GridViewRow
        Dim max As Integer = 1

        If grdSeccDat Is Nothing OrElse grdSeccDat.Rows.Count = 0 Then Exit Sub

        'Ordena los registros que se seleccionarán de 1 en adelante en forma ascendente


        For Each fila In grdSeccDat.Rows
            If CType(fila.FindControl("chkSeccDta"), CheckBox).Checked Then
                etiqueta = CType(fila.FindControl("lblOrden"), Label)
                etiqueta.Text = max
                max += 1
            End If
        Next
    End Sub
    Protected Sub chkTodos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fila As GridViewRow
        Dim etiqueta As Label
            For Each fila In grdSeccDat.Rows
                If CType(grdSeccDat.HeaderRow.FindControl("chkTodos"), CheckBox).Checked = True Then
                    CType(fila.FindControl("chkSeccDta"), CheckBox).Checked = True
                Else
                CType(fila.FindControl("chkSeccDta"), CheckBox).Checked = False
                etiqueta = CType(fila.FindControl("lblOrden"), Label)
                etiqueta.Text = ""
            End If
        Next
        ordenarTodos()
        revisarSeleccion()
    End Sub
    Protected Sub chkSeccDta_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        OrdenarSeccionDta(sender)
    End Sub
    Private Sub OrdenarSeccionDta(ByVal sender As Object)
        Dim check As CheckBox = CType(sender, CheckBox)
        Dim etiqueta As Label
        Dim fila As GridViewRow
        Dim max As Integer = 0

        If grdSeccDat Is Nothing OrElse grdSeccDat.Rows.Count = 0 Then Exit Sub

        If check.Checked Then
            'Obtiene el máximo valor del orden de los datos seleccionados
            For Each fila In grdSeccDat.Rows
                If CType(fila.FindControl("chkSeccDta"), CheckBox).Checked Then
                    etiqueta = CType(fila.FindControl("lblOrden"), Label)
                    If etiqueta.Text.Length > 0 AndAlso CType(etiqueta.Text, Integer) > max Then
                        max = CType(etiqueta.Text, Integer)
                    End If
                End If
            Next
            max += 1
            'Busca la fila que fue seleccionada por el usuario y escribe el orden correspondiente en la etiqueta
            For Each fila In grdSeccDat.Rows
                If CType(fila.FindControl("chkSeccDta"), CheckBox).ClientID = check.ClientID Then
                    etiqueta = CType(fila.FindControl("lblOrden"), Label)
                    etiqueta.Text = max
                    Exit Sub
                End If
            Next
        Else
            '*******************************
            'igonzalez  09 jun 2009  se valida si es el desglose mayor

            'Busca la fila que fue seleccionada para tomar el valor del orden
            For Each fila In grdSeccDat.Rows
                If CType(fila.FindControl("chkSeccDta"), CheckBox).ClientID = check.ClientID Then
                    etiqueta = CType(fila.FindControl("lblOrden"), Label)
                    If etiqueta.Text.Length > 0 Then
                        max = CType(etiqueta.Text, Integer)
                        etiqueta.Text = String.Empty
                    End If
                    Exit For
                End If
            Next

            intPagMax = max

            'Resta 1 al orden de las filas que están marcadas y que son mayores a la fila seleccionada
            For Each fila In grdSeccDat.Rows
                If CType(fila.FindControl("chkSeccDta"), CheckBox).Checked Then
                    etiqueta = CType(fila.FindControl("lblOrden"), Label)
                    If etiqueta.Text.Length > 0 AndAlso CType(etiqueta.Text, Integer) > max Then
                        etiqueta.Text = CType(etiqueta.Text, Integer) - 1

                        If CType(etiqueta.Text, Integer) > intPagMax Then
                            intPagMax = CType(etiqueta.Text, Integer)
                        End If
                    End If
                End If
            Next
            '*******************************
        End If
    End Sub
    Private Sub revisarSeleccion()
        Dim fila As GridViewRow
        Dim todos As Boolean = True

        If grdSeccDat Is Nothing OrElse grdSeccDat.Rows.Count = 0 Then Exit Sub

        For Each fila In grdSeccDat.Rows
            If Not CType(fila.FindControl("chkSeccDta"), CheckBox).Checked Then
                todos = False
                Exit For
            End If
        Next

        If todos Then
            CType(grdSeccDat.HeaderRow.FindControl("chkTodos"), CheckBox).Checked = True
        Else
            CType(grdSeccDat.HeaderRow.FindControl("chkTodos"), CheckBox).Checked = False
        End If
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaPantalla.aspx")
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If lblidPantalla.Text <> "" Then
                Master.MensajeError("Error al intentar guardar")
                Exit Sub
            End If

            Dim dbDatset As New DataSet
            dbDatset = clsPantallas.ObtenerValidaTarea(ddlTarea.SelectedValue)
            If dbDatset.Tables(0).Rows.Count > 0 AndAlso dbDatset.Tables.Count > 0 Then
                Master.MensajeError("Tarea asignada a otra pantalla")
                Exit Sub
            End If

            If ValidarCampo() Then
                Dim objPantalla As New clsPantallas

                objPantalla.PDK_PANT_NOMBRE = txtNombre.Text.Trim.ToUpper
                objPantalla.PDK_PANT_LINK = txtaspx.Text.Trim.ToUpper
                objPantalla.PDK_PANT_ORDEN = txtOrden.Text.Trim
                objPantalla.PDK_PANT_STATUS = IIf(chkStatus.Checked = True, 2, 3)
                objPantalla.PDK_PANT_MODIF = Format(Now(), "yyyy-MM-dd")
                objPantalla.PDK_CLAVE_USUARIO = Session("IdUsua")
                objPantalla.PDK_ID_TAREAS = ddlTarea.SelectedValue
                objPantalla.PDK_PANT_DOCUMENTOS = ddldocumento.SelectedValue
                objPantalla.Guarda()
                lblidPantalla.Text = objPantalla.PDK_ID_PANTALLAS

                Master.MensajeError("Información almacenada exitosamente")

            Else
                Master.MensajeError("Todos los campos marcados con * son obliga torios")
                Exit Sub

            End If


        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Public Function ValidarCampo() As Boolean
        ValidarCampo = False
        If txtNombre.Text.Trim.Length = 0 Then Exit Function
        If txtaspx.Text.Trim.Length = 0 Then Exit Function
        If txtOrden.Text.Trim.Length = 0 Then Exit Function
        ValidarCampo = True

    End Function

    Protected Sub btnGuardar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar1.Click
        Try
            If ValidaSeccion() Then
                Dim objSeccObjPant As New clsPantallaObjeto
                Dim intRegis As Integer = 0
                Dim intCol As Integer = 0
                Dim strOrde As New Label
                Dim check As New CheckBox

                For intRegis = 0 To grdSeccDat.Rows.Count - 1

                    objSeccObjPant.PDK_REL_PANT_OBJ_NOMBRE = grdSeccDat.Rows(intRegis).Cells(1).Text
                    objSeccObjPant.PDK_REL_PANT_OBJ_TAMANO = grdSeccDat.Rows(intRegis).Cells(4).Text
                    strOrde = CType(grdSeccDat.Rows(intRegis).FindControl("lblOrden"), Label)
                    If strOrde.Text <> "" Then
                        objSeccObjPant.PDK_REL_PANT_OBJ_ORDEN = strOrde.Text
                    Else
                        objSeccObjPant.PDK_REL_PANT_OBJ_ORDEN = 0
                    End If
                    check = CType(grdSeccDat.Rows(intRegis).FindControl("chkMostrar"), CheckBox)
                    If check.Checked = True Then
                        objSeccObjPant.PDK_REL_PANT_OBJ_MOSTRAR = 1
                    Else
                        objSeccObjPant.PDK_REL_PANT_OBJ_MOSTRAR = 0
                    End If

                    objSeccObjPant.PDK_REL_PANT_OBJ_STATUS = IIf(chkStatus.Checked = True, 2, 3)
                    objSeccObjPant.PDK_REL_PANT_OBJ_MODIF = Format(Now(), "yyyy-MM-dd")
                    objSeccObjPant.PDK_CLAVE_USUARIO = Session("IdUsua")
                    ' objSeccObjPant.PDK_ID_TIPO_OBJETO = grdSeccDat.Rows(intRegis).Cells(2).Text
                    ' objSeccObjPant.PDK_ID_SECCION = ddlSeccion.SelectedValue
                    objSeccObjPant.PDK_ID_SECCION_DATO = grdSeccDat.Rows(intRegis).Cells(0).Text
                    objSeccObjPant.PDK_ID_PANTALLAS = lblidPantalla.Text
                    objSeccObjPant.Guarda()
                    objSeccObjPant.PDK_ID_REL_PANTALLA_OBJETO = 0
                Next
                Master.MensajeError("Información almacenada exitosamente")
            Else
                Master.MensajeError("Debe de guardar la pantalla")
                Exit Sub
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Public Function ValidaSeccion() As Boolean
        ValidaSeccion = False
        If lblidPantalla.Text.Trim.Length = 0 Then Exit Function
        ValidaSeccion = True
    End Function

  

End Class