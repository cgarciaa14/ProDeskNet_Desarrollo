'BBV-P-423: RQSOL-03: AVH: 10/11/2016 SE AGREGA CAMPO DEFAULT
'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.
Imports ProdeskNet.Catalogos

Public Class manejaEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("idEmp")) > 0 Then
                hdnIdRegistro.Value = Request("idEmp")
                CargaInfo()
            End If
        End If

    End Sub


    Private Sub CargaInfo()
        Try
            Dim intClave As Integer = Val(hdnIdRegistro.Value)
            Dim objEmpresa As New clsEmpresa(intClave)

            If objEmpresa.PDK_ID_EMPRESA > 0 Then

                lblID.Text = objEmpresa.PDK_ID_EMPRESA
                txtNombreEmpresa.Text = objEmpresa.PDK_EMP_NOMBRE
                chkActivo.Checked = IIf(objEmpresa.PDK_EMP_ACTIVO = 2, True, False)
                chkDefault.Checked = IIf(objEmpresa.PDK_EMP_DEFAULT = 1, True, False)
            Else
                Master.MensajeError("No se localizó información para el cliente")
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If Trim(txtNombreEmpresa.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim path As String = "./consultaEmpresa.aspx"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intCve As Integer = Val(hdnIdRegistro.Value)
                Dim objEmpresa As New clsEmpresa(intCve)
                Dim intOpc As Integer = 2

                If intCve > 0 Then
                    intOpc = 3
                    objEmpresa.PDK_ID_EMPRESA = intCve
                End If

                'guardamos la información
                objEmpresa.PDK_EMP_NOMBRE = txtNombreEmpresa.Text.Trim.ToUpper

                objEmpresa.PDK_EMP_MODIF = Format(Now(), "yyyy-MM-dd")
                objEmpresa.PDK_CLAVE_USUARIO = Session("IdUsua")
                objEmpresa.PDK_EMP_ACTIVO = IIf(chkActivo.Checked = True, 2, 3)
                objEmpresa.PDK_EMP_DEFAULT = IIf(chkDefault.Checked = True, 1, 0)

                objEmpresa.ManejaEmpresa(intOpc)

                If objEmpresa.ErrorEmpresa = "" Then
                    Master.MensajeError("La información se guardo con éxito")
                    hdnIdRegistro.Value = objEmpresa.PDK_ID_EMPRESA
                    CargaInfo()
                Else
                    Master.MensajeError(objEmpresa.ErrorEmpresa.ToString)
                End If

                

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

End Class