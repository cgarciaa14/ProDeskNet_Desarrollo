Imports ProdeskNet.Catalogos

Public Class manejaReportes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("idReporte")) > 0 Then
                hdnIdRegistro.Value = Request("idReporte")
                CargaInfo()
            End If
        End If

    End Sub


    Private Sub CargaInfo()
        Try
            Dim intClave As Integer = Val(hdnIdRegistro.Value)

            Dim objReportes As New clsReportes(intClave)

            If objReportes.PDK_ID_REPORTES > 0 Then

                lblID.Text = objReportes.PDK_ID_REPORTES
                txtNombre.Text = objReportes.PDK_REP_NOMBRE_REPORTE
                txtProcedimiento.Text = objReportes.PDK_REP_NOMBRE_PROCEDIMIENTO
                chkActivo.Checked = IIf(objReportes.PDK_REP_ACTIVO = 2, True, False)

            Else
                Master.MensajeError("No se localizó información para el cl-iente")
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If Trim(txtNombre.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaReportes.aspx")
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intCve As Integer = Val(hdnIdRegistro.Value)
                Dim objRep As New clsReportes(intCve)
                Dim intOpc As Integer = 1

                If intCve > 0 Then
                    intOpc = 2
                End If

                'guardamos la información

                objRep.PDK_ID_REPORTES = intCve
                objRep.PDK_REP_NOMBRE_REPORTE = txtNombre.Text.Trim
                objRep.PDK_REP_NOMBRE_PROCEDIMIENTO = txtProcedimiento.Text.Trim

                objRep.PDK_REP_MODIF = Format(Now(), "yyyy-MM-dd")
                objRep.PDK_CLAVE_USUARIO = Session("IdUsua")
                objRep.PDK_REP_ACTIVO = IIf(chkActivo.Checked = True, 2, 3)

                objRep.Guarda()


                Master.MensajeError("La información se guardo con éxito")
                hdnIdRegistro.Value = objRep.PDK_ID_REPORTES
                CargaInfo()

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub



End Class