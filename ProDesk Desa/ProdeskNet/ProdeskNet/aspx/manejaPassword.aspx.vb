Imports ProdeskNet.Seguridad
Imports System.Data
Public Class manejaPassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then
            If Session("cveUsuAcc") <> Nothing Then
                hdnIdRegistro.Value = Session("cveUsuAcc")
                lblUsu.Text = Session("cveUsuAcc")
            End If

        End If
    End Sub
    Private Function ValidaDatos() As Boolean
        ValidaDatos = False
        Try
            Dim objUsu As New clsUsuario(Session("cveUsuAcc"))
            Dim objDat As New clsSeguridad

            'faltan datos por capturar
            If Trim$(txtPwd.Text) = "" Or Trim$(txtNvaPwd.Text) = "" Or Trim$(txtCNvaPwd.Text) = "" Then
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
                Exit Function
            End If

            'validamos si es correcto el usuario y la contraseña anterior 
            objUsu = objDat.ValidaUsuario(Trim$(lblUsu.Text), Trim$(txtPwd.Text))
            If Trim$(objDat.ErrorSeguridad) <> "" Then
                Master.MensajeError(objDat.ErrorSeguridad)
                Exit Function
            End If

            'validamos que la longitud de la nueva contraseña no sea menor de 6 caracteres
            If Trim$(txtNvaPwd.Text).Length < 6 Then
                Master.MensajeError("La contraseña debe contener al menos 6 caracteres.")
                Exit Function
            End If

            'validamos que las nuevas contraseñas coincidan
            txtNvaPwd.Text = objDat.EncriptaDesencriptaCadena(Trim$(txtNvaPwd.Text), False)
            txtCNvaPwd.Text = objDat.EncriptaDesencriptaCadena(Trim$(txtCNvaPwd.Text), False)
            If txtNvaPwd.Text <> txtCNvaPwd.Text Then
                Master.MensajeError("La nueva contraseña no coincide con su confirmación.")
                Exit Function
            End If
            ValidaDatos = True
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Function
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("inicio.aspx")
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaDatos() Then
                Dim objUsu As New clsUsuario(Session("cveUsuAcc"))
                If UCase(Trim$(lblUsu.Text)) <> UCase(objUsu.PDK_USU_CLAVE) Then objUsu.CargaUsuario(Trim$(lblUsu.Text))
                Dim MD As New clsSeguridad
                objUsu.PDK_USU_CONTRASENA = txtNvaPwd.Text
                objUsu.PDK_USU_MODIF = Format(Now(), "yyyy-MM-dd")
                Dim dbRes As DataSet = objUsu.ManejaUsuario(4)
                Master.MensajeError("La contraseña se cambió con éxito")
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

End Class