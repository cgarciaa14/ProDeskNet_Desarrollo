Imports ProdeskNet.Catalogos

Public Class altaEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

        End If

    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaEmpresa.aspx")
    End Sub


    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtNombreEmpresa.Text.Trim.Length = 0 Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                Dim objEmpresa As New clsEmpresa(0)

                'guardamos la información
                objEmpresa.PDK_EMP_NOMBRE = txtNombreEmpresa.Text.Trim.ToUpper
                objEmpresa.PDK_EMP_MODIF = Format(Now(), "yyyy-MM-dd")
                objEmpresa.PDK_CLAVE_USUARIO = Session("IdUsua")
                objEmpresa.PDK_EMP_ACTIVO = IIf(chkActivo.Checked = True, 2, 3)

                objEmpresa.Guarda()

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