Imports ProdeskNet.Catalogos

Public Class altaResultados
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

        End If

    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaResultados.aspx")
    End Sub


    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtDescripcion.Text.Trim.Length = 0 Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                Dim objResultados As New clsResultados(0)

                'guardamos la información
                objResultados.PDK_ID_CAT_RESULTADO = IIf(lblId.Text.Trim.Length > 0, Val(lblId.Text), 0)
                objResultados.PDK_RES_NOMBRE = txtDescripcion.Text.Trim.ToUpper
                objResultados.PDK_RES_MODIF = Format(Now(), "yyyy-MM-dd")
                objResultados.PDK_CLAVE_USUARIO = Session("IdUsua")
                objResultados.PDK_RES_ACTIVO = IIf(Me.chkActivo.Checked = True, 2, 3)

                objResultados.Guarda()

                lblId.Text = objResultados.PDK_ID_CAT_RESULTADO

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