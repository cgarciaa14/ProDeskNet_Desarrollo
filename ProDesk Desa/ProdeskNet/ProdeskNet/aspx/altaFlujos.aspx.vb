Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Data

Public Class altaFlujos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then
            LlenaCombo(1) 'Empresa
            ddlEmpresa_SelectedIndexChanged(sender, e)
        End If

    End Sub
    Private Sub LlenaCombo(ByVal intCombo As Integer)
        Dim dsDataset As New DataSet
        Dim intEmpresa As Integer = 0
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
                dsDataset = clsProductos.ObtenerProductoEmp(intEmpresa)
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

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaFlujos.aspx")
    End Sub

    Public Function ValidaDatos() As Boolean
        ValidaDatos = False

        If txtNombreFlujo.Text.Trim.Length <= 0 Then Exit Function
        If txtOrden.Text.Trim.Length <= 0 Then Exit Function
        If Val(txtOrden.Text.Trim) = 0 Then Exit Function
        ValidaDatos = True

    End Function

    Protected Sub btnGuardar_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If lblIdFlujo.Text <> "" Then
                Master.MensajeError("Error al intentar guardar")
                Exit Sub
            End If

            If ValidaDatos() Then

                Dim objFlujo As New clsFlujos

                objFlujo.PDK_FLU_NOMBRE = Me.txtNombreFlujo.Text.Trim.ToUpper
                objFlujo.PDK_FLU_ORDEN = Val(Me.txtOrden.Text.Trim)
                objFlujo.PDK_ID_PRODUCTOS = Val(Me.ddlProducto.SelectedValue.ToString)
                objFlujo.PDK_ID_PER_JURIDICA = Val(Me.ddlPersonalidadJuridica.SelectedValue)
                objFlujo.PDK_FLU_ACTIVO = IIf(chkStatus.Checked = True, 2, 3)
                objFlujo.PDK_FLU_MODIF = Format(Now(), "yyyy-MM-dd")
                objFlujo.PDK_CLAVE_USUARIO = 1
                objFlujo.Guarda()

                lblIdFlujo.Text = objFlujo.PDK_ID_FLUJOS

                Master.MensajeError("Información almacenada exitosamente")
            Else
                Master.MensajeError("Todos los campos marcados con * son obliga torios")
                Exit Sub
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
End Class