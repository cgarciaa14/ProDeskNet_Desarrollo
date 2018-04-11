'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.  
Imports ProdeskNet.Seguridad

Public Class manejaPerfil
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("idPerfil")) > 0 Then
                hdnIdRegistro.Value = Request("idPerfil")
                CargaInfo()
            End If
        End If

    End Sub


    Private Sub CargaInfo()
        Try
            Dim intClave As Integer = Val(hdnIdRegistro.Value)

            Dim objPerfil As New clsPerfil(intClave)

            If objPerfil.PDK_ID_PERFIL > 0 Then

                lblID.Text = objPerfil.PDK_ID_PERFIL
                txtNombre.Text = objPerfil.PDK_PER_NOMBRE
                txtNivel.Text = objPerfil.PDK_PER_NIVEL
                chkStatus.Checked = IIf(objPerfil.PDK_PER_ACTIVO = 2, True, False)

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
        If Trim(txtNivel.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim path As String = "./consultaPerfil.aspx"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intCve As Integer = Val(hdnIdRegistro.Value)
                Dim objPerfil As New clsPerfil(intCve)
                Dim intOpc As Integer = 1

                If intCve > 0 Then
                    intOpc = 2
                End If
                'Dim dsData As New DataSet
                'dsData = clsPerfil.ValidarNiveles(txtNivel.Text)
                'If dsData.Tables(0).Rows.Count > 0 AndAlso dsData.Tables.Count > 0 Then
                '    Master.MensajeError("El nivel ya existe agregar otro")
                '    Exit Sub
                'End If

                'guardamos la información

                objPerfil.PDK_ID_PERFIL = intCve
                objPerfil.PDK_PER_NOMBRE = txtNombre.Text.Trim.ToUpper
                objPerfil.PDK_PER_ACTIVO = IIf(chkStatus.Checked = True, 2, 3)
                objPerfil.PDK_PER_MODIF = Format(Now(), "yyyy-MM-dd")
                objPerfil.PDK_CLAVE_USUARIO = Session("IdUsua")

                objPerfil.Guarda()


                Master.MensajeError("La información se guardo con éxito")
                hdnIdRegistro.Value = objPerfil.PDK_ID_PERFIL
                CargaInfo()

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub


End Class