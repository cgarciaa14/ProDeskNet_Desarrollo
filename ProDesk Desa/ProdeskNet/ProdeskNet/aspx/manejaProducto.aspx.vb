'BBV-P-423: RQSOL-03: AVH: 10/11/2016 SE AGREGA CAMPO DEFAULT

Imports System.Data
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Seguridad

Public Class manejaProducto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Val(Request("idProd")) > 0 Then
                hdnIdRegistro.Value = Request("idProd")
                ObtenerProducto()

            End If
            LlenarEmpresa()
            LlenarMoneda()
        End If
    End Sub
    Private Sub ObtenerProducto()
        Dim intCveProd As Integer = Val(hdnIdRegistro.Value)
        Dim dbProducto As New DataSet

        Try
            Dim objProducto As New clsProductos(intCveProd)
            If objProducto.PDK_ID_PRODUCTOS > 0 Then
                lblID.Text = objProducto.PDK_ID_PRODUCTOS
                txtNombreProd.Text = objProducto.PDK_PROD_NOMBRE
                If objProducto.PDK_ID_EMPRESA > 0 Then
                    LlenarEmpresa()
                    cmbEmpresa.SelectedIndex = cmbEmpresa.Items.IndexOf(cmbEmpresa.Items.FindByValue(objProducto.PDK_ID_EMPRESA.ToString.Trim))
                End If
                If objProducto.PDK_ID_MONEDA > 0 Then
                    LlenarMoneda()
                    cmbMoneda.SelectedIndex = cmbMoneda.Items.IndexOf(cmbMoneda.Items.FindByValue(objProducto.PDK_ID_MONEDA.ToString.Trim))

                End If
                chkStatus.Checked = IIf(objProducto.PDK_PROD_ACTIVO = 2, True, False)
                chkDefault.Checked = IIf(objProducto.PDK_PROD_DEFAULT = 1, True, False)
            End If

        Catch ex As Exception

        End Try

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
        Response.Redirect("./consultaProducto.aspx?Idemp=" & cmbEmpresa.SelectedValue)
    End Sub
    Public Function ValidaDatos() As Boolean
        ValidaDatos = False
        If txtNombreProd.Text.Length = 0 Then Exit Function

        ValidaDatos = True
    End Function
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaDatos() Then
                Dim intCvePro As Integer = Val(hdnIdRegistro.Value)
                Dim objPros As New clsProductos

                Dim intOpc As Integer = 2

                If intCvePro > 0 Then
                    intOpc = 3
                    objPros.PDK_ID_PRODUCTOS = intCvePro
                End If

                'objPros.PDK_ID_PRODUCTOS = intCvePro
                objPros.PDK_PROD_NOMBRE = txtNombreProd.Text.Trim
                objPros.PDK_ID_EMPRESA = cmbEmpresa.SelectedValue
                objPros.PDK_ID_MONEDA = cmbMoneda.SelectedValue
                objPros.PDK_PROD_MODIF = Format(Now(), "yyyy-MM-dd")
                objPros.PDK_PROD_ACTIVO = IIf(chkStatus.Checked = True, 2, 3)
                objPros.PDK_PROD_DEFAULT = IIf(chkDefault.Checked = True, 1, 0)
                objPros.ManejaProducto(intOpc)
                hdnIdRegistro.Value = objPros.PDK_ID_PRODUCTOS
                lblID.Text = Val(hdnIdRegistro.Value)

                Master.MensajeError("Información almacenada exitosamente")

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")

            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

End Class