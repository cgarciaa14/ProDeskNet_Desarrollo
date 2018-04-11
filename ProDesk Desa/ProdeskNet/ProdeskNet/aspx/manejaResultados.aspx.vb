Imports ProdeskNet.Catalogos

Public Class manejaResultados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("idRes")) > 0 Then
                hdnIdRegistro.Value = Request("idRes")
                CargaInfo()
            End If
        End If

    End Sub


    Private Sub CargaInfo()
        Try
            Dim intClave As Integer = Val(hdnIdRegistro.Value)

            Dim objResultados As New clsResultados(intClave)

            If objResultados.PDK_ID_CAT_RESULTADO > 0 Then

                lblIdRechazo.Text = objResultados.PDK_ID_CAT_RESULTADO
                txtDescripcion.Text = objResultados.PDK_RES_NOMBRE
                chkActivo.Checked = IIf(objResultados.PDK_RES_ACTIVO = 2, True, False)

            Else
                Master.MensajeError("No se localizó información del catálogo de resultados")
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If Trim(txtDescripcion.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaResultados.aspx")
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intCve As Integer = Val(hdnIdRegistro.Value)
                Dim objResultados As New clsResultados(intCve)

                'guardamos la información
                objResultados.PDK_ID_CAT_RESULTADO = intCve
                objResultados.PDK_RES_NOMBRE = txtDescripcion.Text.Trim.ToUpper
                objResultados.PDK_RES_MODIF = Format(Now(), "yyyy-MM-dd")
                objResultados.PDK_CLAVE_USUARIO = Session("IdUsua")
                objResultados.PDK_RES_ACTIVO = IIf(chkActivo.Checked = True, 2, 3)


                objResultados.Guarda()


                Master.MensajeError("La información se guardo con éxito")

                hdnIdRegistro.Value = objResultados.PDK_ID_CAT_RESULTADO
                CargaInfo()

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub


End Class