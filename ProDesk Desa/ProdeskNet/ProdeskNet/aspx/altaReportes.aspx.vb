Imports ProdeskNet.Catalogos

Public Class altaReportes

    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

        End If

    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaReportes.aspx")
    End Sub


    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtNombre.Text.Trim.Length = 0 Then Exit Function
        If txtProcedimiento.Text.Trim.Length = 0 Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                Dim objProc As New clsReportes(0)

                'guardamos la información
                objProc.PDK_ID_REPORTES = IIf(Me.lblID.Text.Trim.Length > 0, Val(Me.lblID.Text.Trim), 0)
                objProc.PDK_REP_NOMBRE_REPORTE = txtNombre.Text.Trim
                objProc.PDK_REP_NOMBRE_PROCEDIMIENTO = txtProcedimiento.Text.Trim
                objProc.PDK_CLAVE_USUARIO = Session("IdUsua")
                objProc.PDK_REP_MODIF = Format(Now(), "yyyy-MM-dd")

                objProc.Guarda()

                Me.lblID.Text = objProc.PDK_ID_REPORTES

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