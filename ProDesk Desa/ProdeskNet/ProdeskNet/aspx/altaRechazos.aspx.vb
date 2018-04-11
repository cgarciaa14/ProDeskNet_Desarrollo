Imports ProdeskNet.Catalogos

Public Class altaRechazos
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then
            Dim objRechazos As New clsRechazos
            objRechazos.returnRechazo()
            ddlcondicion.DataSource = objRechazos.CondicionRechazo.Tables(0)
            ddlcondicion.DataValueField = "id"
            ddlcondicion.DataTextField = "value"
            ddlcondicion.DataBind()
        End If

    End Sub


    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaRechazos.aspx")
    End Sub


    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtDescripcion.Text.Trim.Length = 0 Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                Dim objRechazos As New clsRechazos(0)

                'guardamos la información
                objRechazos.PDK_REC_NOMBRE = txtDescripcion.Text.Trim.ToUpper
                objRechazos.PDK_REC_MODIF = Format(Now(), "yyyy-MM-dd")
                objRechazos.PDK_CLAVE_USUARIO = Session("IdUsua")
                objRechazos.PDK_REC_ACTIVO = IIf(Me.chkActivo.Checked = True, 2, 3)
                objRechazos.rechcond = ddlcondicion.SelectedValue
                objRechazos.Guarda()

                lblIdRechazo.Text = objRechazos.PDK_ID_CAT_RECHAZOS

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