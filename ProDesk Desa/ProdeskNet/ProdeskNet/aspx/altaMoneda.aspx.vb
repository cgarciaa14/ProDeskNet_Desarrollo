Imports ProdeskNet.Catalogos

Public Class altaMoneda
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

        End If

    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaMoneda.aspx")
    End Sub


    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtNombreMoneda.Text.Trim.Length = 0 Then Exit Function
        If txtTipoCambio.Text.Trim.Length = 0 Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                Dim objMoneda As New clsMoneda(0)

                'guardamos la información
                objMoneda.PDK_MON_NOMBRE = txtNombreMoneda.Text.Trim.ToUpper
                objMoneda.PDK_MON_MODIF = Format(Now(), "yyyy-MM-dd")
                objMoneda.PDK_CLAVE_USUARIO = Session("IdUsua")
                objMoneda.PDK_MON_TIPO_CAMBIO = CDbl(txtTipoCambio.Text.Trim)
                objMoneda.PDK_MON_ACTIVO = IIf(chkActivo.Checked = True, 2, 3)
                objMoneda.Guarda()

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
                Exit Sub
            End If

            Master.MensajeError("Información almacenada exitosamente")

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
End Class