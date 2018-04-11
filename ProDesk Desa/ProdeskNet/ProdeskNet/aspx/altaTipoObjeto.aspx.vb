Imports ProdeskNet.Catalogos
Public Class altaTipoObjeto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

        End If

    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaTipoObjeto.aspx")
    End Sub
    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtNombre.Text.Trim.Length = 0 Then Exit Function


        ValidaCampos = True
    End Function
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try

            If lblCveObj.Text <> "" Then
                Master.MensajeError("Error al intentar guardar")
                Exit Sub

            End If
            If ValidaCampos() Then

                Dim objTipoObj As New clsTipoObjeto

                'guardamos la información
                objTipoObj.PDK_TIP_OBJ_NOMBRE = txtNombre.Text.Trim.ToUpper
                objTipoObj.PDK_TIP_OBJ_NOMBRE_COD = txtnombreCod.Text.Trim.ToUpper

                objTipoObj.Guarda()
                lblCveObj.Text = objTipoObj.PDK_ID_TIPO_OBJETO
                If objTipoObj.ErroTipoObjeto <> "" Then
                    Master.MensajeError(" Error al guardar PDK_ID_TIPO_OBJETO")
                Else
                    Master.MensajeError("Información almacenada exitosamente")
                End If

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
                Exit Sub
            End If



        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

End Class