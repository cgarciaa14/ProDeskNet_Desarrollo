'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.  
Imports ProdeskNet.Configurcion
Public Class manejaSeccion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("catIdSeccio")) > 0 Then
                hdnIdRegistro.Value = Request("catIdSeccio")
                CargarDatos()
            End If
        End If
    End Sub
    Private Sub CargarDatos()
        Try
            Dim intCveSeccion As Integer = Val(hdnIdRegistro.Value)
            Dim objSeccion As New clsSeccion(intCveSeccion)
            If objSeccion.PDK_ID_SECCION > 0 Then
                lblCveSeccion.Text = objSeccion.PDK_ID_SECCION
                txtNombre.Text = objSeccion.PDK_SEC_NOMBRE
                txtNombreTab.Text = objSeccion.PDK_SEC_NOMBRE_TABLA
                chkActivo.Checked = IIf(objSeccion.PDK_SEC_STATUS = 2, True, False)
                txtmostraPantalla.Text = objSeccion.PDK_SEC_TAB_MOSTRAR
                hndcreada.Value = objSeccion.PDK_SEC_CREACION
                If objSeccion.PDK_SEC_CREACION = 2 Then
                    txtNombre.Enabled = False
                    txtNombreTab.Enabled = False
                    chkActivo.Enabled = False
                Else
                    txtNombre.Enabled = True
                    txtNombreTab.Enabled = True
                    chkActivo.Enabled = True
                End If
            Else
                Master.MensajeError("No se localizó información de la Sección")
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim path As String = "./consultaSeccion.aspx"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
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
            If ValidaCampos() Then
                Dim intCveSeccion As Integer = Val(hdnIdRegistro.Value)
                Dim objSeccion As New clsSeccion
                objSeccion.PDK_ID_SECCION = intCveSeccion
                objSeccion.PDK_SEC_NOMBRE = txtNombre.Text.Trim.ToUpper
                objSeccion.PDK_SEC_NOMBRE_TABLA = txtNombreTab.Text.Trim.ToUpper
                objSeccion.PDK_SEC_STATUS = IIf(chkActivo.Checked = True, 2, 3)
                objSeccion.PDK_SEC_MODIF = Format(Now(), "yyyy-MM-dd")
                objSeccion.PDK_CLAVE_USUARIO = Session("IdUsua")
                If hndcreada.Value <> 2 Then
                    objSeccion.PDK_SEC_CREACION = 1
                Else
                    objSeccion.PDK_SEC_CREACION = 2
                End If

                objSeccion.PDK_SEC_TAB_MOSTRAR = txtmostraPantalla.Text.Trim

                objSeccion.ActualizaRegistro()
                Master.MensajeError("Información almacenada exitosamente")
                hdnIdRegistro.Value = objSeccion.PDK_ID_SECCION
                CargarDatos()
            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

End Class