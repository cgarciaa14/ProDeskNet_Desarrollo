Imports ProdeskNet.Configurcion
Public Class altaSeccion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

        End If

    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaSeccion.aspx")
    End Sub
    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtNombre.Text.Trim.Length = 0 Then Exit Function
        If txtNombreTab.Text.Trim.Length = 0 Then Exit Function
        If txtmostraPantalla.Text.Trim.Length = 0 Then Exit Function


        ValidaCampos = True
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If lblCveSeccion.Text <> "" Then
                Master.MensajeError("Error al intentar guardar")
                Exit Sub
            End If
            If ValidaCampos() Then

                Dim objSeccion As New clsSeccion

                'guardamos la información
                objSeccion.PDK_SEC_NOMBRE = txtNombre.Text.Trim.ToUpper
                objSeccion.PDK_SEC_NOMBRE_TABLA = txtNombreTab.Text.Trim.ToUpper
                objSeccion.PDK_SEC_MODIF = Format(Now(), "yyyy-MM-dd")
                objSeccion.PDK_SEC_CREACION = 1
                objSeccion.PDK_CLAVE_USUARIO = Session("IdUsua")
                objSeccion.PDK_SEC_STATUS = IIf(chkActivo.Checked = True, 2, 3)
                objSeccion.PDK_SEC_TAB_MOSTRAR = txtmostraPantalla.Text.Trim

                objSeccion.Guarda()
                lblCveSeccion.Text = objSeccion.PDK_ID_SECCION

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