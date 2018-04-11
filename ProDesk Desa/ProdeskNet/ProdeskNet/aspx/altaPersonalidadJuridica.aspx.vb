Imports ProdeskNet.Catalogos

Public Class altaPersonalidadJuridica
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

        End If

    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaPersonalidadJuridica.aspx")
    End Sub


    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtNombre.Text.Trim.Length = 0 Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                Dim objPersonalidad As New clsPersonalidadJuridica(0)

                'guardamos la información

                objPersonalidad.PDK_PER_NOMBRE = txtNombre.Text.Trim.ToUpper
                objPersonalidad.PDK_PER_ACTIVO = IIf(chkStatus.Checked = True, 1, 0)
                objPersonalidad.PDK_PER_MODIF = Format(Now(), "yyyy-MM-dd")
                objPersonalidad.PDK_CLAVE_USUARIO = Session("IdUsua")

                objPersonalidad.Guarda()

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