'BBV-P-423: RQSOL-03: AVH: 10/11/2016 SE AGREGA CAMPO DEFAULT
'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.  
Imports ProdeskNet.Catalogos

Public Class manejaPersonalidadJuridica
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("idPer")) > 0 Then
                hdnIdRegistro.Value = Request("idPer")
                CargaInfo()
            End If
        End If

    End Sub


    Private Sub CargaInfo()
        Try
            Dim intClave As Integer = Val(hdnIdRegistro.Value)

            Dim objPersonalidad As New clsPersonalidadJuridica(intClave)

            If objPersonalidad.PDK_ID_PER_JURIDICA > 0 Then

                lblID.Text = objPersonalidad.PDK_ID_PER_JURIDICA
                txtNombre.Text = objPersonalidad.PDK_PER_NOMBRE
                chkStatus.Checked = IIf(objPersonalidad.PDK_PER_ACTIVO = 2, True, False)
                chkDefault.Checked = IIf(objPersonalidad.PDK_PER_DEFAULT = 1, True, False)

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
        Dim path As String = "./consultaPersonalidadJuridica.aspx"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intCve As Integer = Val(hdnIdRegistro.Value)
                Dim objPersonalidad As New clsPersonalidadJuridica(intCve)

                Dim intOpc As Integer = 2

                If intCve > 0 Then
                    intOpc = 3
                    objPersonalidad.PDK_ID_PER_JURIDICA = intCve
                End If

                'guardamos la información
                objPersonalidad.PDK_ID_PER_JURIDICA = intCve
                objPersonalidad.PDK_PER_NOMBRE = txtNombre.Text.Trim.ToUpper
                objPersonalidad.PDK_PER_ACTIVO = IIf(chkStatus.Checked = True, 2, 3)
                objPersonalidad.PDK_PER_MODIF = Format(Now(), "yyyy-MM-dd")
                objPersonalidad.PDK_CLAVE_USUARIO = Session("IdUsua")
                objPersonalidad.PDK_PER_DEFAULT = IIf(chkDefault.Checked = True, 1, 0)

                objPersonalidad.ManejaPerJuridica(intOpc)


                Master.MensajeError("La información se guardo con éxito")
                hdnIdRegistro.Value = objPersonalidad.PDK_ID_PER_JURIDICA
                lblID.Text = Val(hdnIdRegistro.Value)


            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub


End Class