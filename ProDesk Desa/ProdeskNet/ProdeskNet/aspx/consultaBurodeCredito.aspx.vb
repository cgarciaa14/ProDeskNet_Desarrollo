Imports ProdeskNet.Buro

Public Class consultaBuroCredito
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                ConsultaDatos()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ConsultaDatos()
        Try
            Dim objBuro As New clsBuro(1)

            Me.lblIdConfiguracion.Text = objBuro.PDK_ID_BURO_CONFIGURACION
            Me.txtFrecuencia.Text = objBuro.PDK_CONF_FRECUENCIA
            Me.txtPFContrasena.Text = objBuro.PDK_CONF_PF_CONTRASENA
            Me.txtPFPuerto.Text = objBuro.PDK_CONF_PF_PORT
            Me.txtPFServidor.Text = objBuro.PDK_CONF_PF_SERVER
            Me.txtPFUsuario.Text = objBuro.PDK_CONF_PF_USUARIO
            Me.txtPMContrasena.Text = objBuro.PDK_CONF_PM_CONTRASENA
            Me.txtPMPuerto.Text = objBuro.PDK_CONF_PM_PORT
            Me.txtPMServidor.Text = objBuro.PDK_CONF_PM_SERVER
            Me.txtPMUsuario.Text = objBuro.PDK_CONF_PM_USUARIO
        Catch ex As Exception
        End Try
    End Sub
    Private Function ValidaInformacion() As Boolean
        Try

            ValidaInformacion = False

            If Len(Me.txtFrecuencia.Text.Trim) = 0 Then Exit Function
            If Len(Me.txtPFContrasena.Text.Trim) = 0 Then Exit Function
            If Len(Me.txtPFPuerto.Text.Trim) = 0 Then Exit Function
            If Len(Me.txtPFServidor.Text.Trim) = 0 Then Exit Function
            If Len(Me.txtPFUsuario.Text.Trim) = 0 Then Exit Function
            If Len(Me.txtPMContrasena.Text.Trim) = 0 Then Exit Function
            If Len(Me.txtPMPuerto.Text.Trim) = 0 Then Exit Function
            If Len(Me.txtPMServidor.Text.Trim) = 0 Then Exit Function
            If Len(Me.txtPMUsuario.Text.Trim) = 0 Then Exit Function

            ValidaInformacion = True
        Catch ex As Exception

        End Try
    End Function
    Private Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        Dim objBuro As New clsBuro(0)

        Try

            If ValidaInformacion() Then
                If Me.lblIdConfiguracion.Text.Trim.Length = 0 Then Me.lblIdConfiguracion.Text = 0

                objBuro.PDK_ID_BURO_CONFIGURACION = Me.lblIdConfiguracion.Text.Trim
                objBuro.PDK_CONF_FRECUENCIA = Me.txtFrecuencia.Text.Trim
                objBuro.PDK_CONF_PF_CONTRASENA = Me.txtPFContrasena.Text.Trim
                objBuro.PDK_CONF_PF_PORT = Me.txtPFPuerto.Text.Trim
                objBuro.PDK_CONF_PF_SERVER = Me.txtPFServidor.Text.Trim
                objBuro.PDK_CONF_PF_USUARIO = Me.txtPFUsuario.Text.Trim
                objBuro.PDK_CONF_PM_CONTRASENA = Me.txtPMContrasena.Text.Trim
                objBuro.PDK_CONF_PM_PORT = Me.txtPMPuerto.Text.Trim
                objBuro.PDK_CONF_PM_SERVER = Me.txtPMServidor.Text.Trim
                objBuro.PDK_CONF_PM_USUARIO = Me.txtPMUsuario.Text.Trim

                objBuro.Guarda()

                ConsultaDatos()
                'Me.lblIdConfiguracion.Text = objBuro.PDK_ID_BURO_CONFIGURACION
                Master.MensajeError("El registro fue guardado correctamente")

            Else
                Master.MensajeError("Los campos son requeridos")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class