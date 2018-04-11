Imports System.Data
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Seguridad
Public Class altaProducto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then
            LlenarEmpresa()
            LlenarMoneda()
        End If

    End Sub
    Public Sub LlenarEmpresa()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet

            dtsRed = clsEmpresa.ObtenTodos(1)
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_EMP_NOMBRE", "PDK_ID_EMPRESA", cmbEmpresa)
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Public Sub LlenarMoneda()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet

            dtsRed = clsMoneda.ObtenTodos(1)
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_MON_NOMBRE", "PDK_ID_MONEDA", cmbMoneda)
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaProducto.aspx")
    End Sub
    Public Function ValidaDatos() As Boolean
        ValidaDatos = False
        If txtNombreProd.Text.Length = 0 Then Exit Function

        ValidaDatos = True
    End Function
    Protected Sub btnGuardar_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If lblCveProd.Text <> "" Then
                Master.MensajeError("Error al intentar guardar")
                Exit Sub
            End If

            If ValidaDatos() Then
                Dim objProducto As New clsProductos
                objProducto.PDK_PROD_NOMBRE = txtNombreProd.Text.Trim.ToUpper
                objProducto.PDK_ID_EMPRESA = cmbEmpresa.SelectedValue
                objProducto.PDK_ID_MONEDA = cmbMoneda.SelectedValue
                objProducto.PDK_PROD_MODIF = Format(Now(), "yyyy-MM-dd")
                objProducto.PDK_CLAVE_USUARIO = Session("IdUsua")
                objProducto.PDK_PROD_ACTIVO = IIf(chkActivo.Checked = True, 2, 3)
                objProducto.Guarda()
                lblCveProd.Text = objProducto.PDK_ID_PRODUCTOS
                If objProducto.ErrorProducto <> "" Then
                    Master.MensajeError("Error al guardar PDK_ID_PRODUCTO")
                Else
                    Master.MensajeError("Información almacenada exitosamente")
                End If
            Else
                Master.MensajeError("Todos los campos marcados con * son obliga torios")
                Exit Sub

            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

End Class