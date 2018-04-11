#Region "Tracker"
'BUG-PD-107: ERODRIGUEZ: 19/06/2017 Se deshabiltaron texbox, drop downlist que haga cambio en el usuario a excepción de el checkbox de activo e inactivo y asignar agencias.
#End Region
Imports System.Data
Imports ProdeskNet.Seguridad
Public Class manejaUsuario
    Inherits System.Web.UI.Page
    Dim BD As New ProdeskNet.BD.clsManejaBD
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("idUsu")) > 0 Then
                Dim dataset As New DataSet

                hdnIdRegistro.Value = Request("idUsu")
                hdnResetiar.Value = "123456"
                dataset = BD.EjecutarQuery("EXEC procEncripta " & hdnResetiar.Value)
                If dataset.Tables.Count > 0 AndAlso dataset.Tables(0).Rows.Count > 0 Then
                    hdhEncrip.Value = dataset.Tables(0).Rows(0).Item("RESULTADO").ToString
                End If
                ObtenerUsuario()
                LlenarDistribuir()

            End If
        End If

        'Se deshabiltaron texbox, drop downlist y boton que haga cambio en el usuario a exepcion de el checkbox de activo e inavtivo y asignar agencias.
        cmbPerfil.Enabled = False
        txtNombre.Enabled = False
        txtPaterno.Enabled = False
        txtMaterno.Enabled = False
        txtUsuario.Enabled = False
        txtcorreo.Enabled = False


    End Sub
    Private Sub LlenarDistribuir()
        Dim dataDistri As New DataSet
        Dim i As Integer = 0
        Dim chen As New CheckBox
        Try
            grvDistribu.DataSource = Nothing
            grvDistribu.DataBind()


            dataDistri = clsUsuario.ObtenerDistriModif(hdnIdRegistro.Value)
            If dataDistri.Tables.Count > 0 AndAlso dataDistri.Tables(0).Rows.Count > 0 Then
                grvDistribu.DataSource = dataDistri.Tables(0)
                grvDistribu.DataBind()

                For i = 0 To grvDistribu.Rows.Count - 1
                    If dataDistri.Tables(0).Rows(i).Item("PDK_REL_USU_DIST_ACTIVO") = 2 Then
                        CType(grvDistribu.Rows(i).FindControl("chkDistribuir"), CheckBox).Checked = True
                    Else
                        CType(grvDistribu.Rows(i).FindControl("chkDistribuir"), CheckBox).Checked = False
                    End If

                Next

            End If


        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Private Sub ObtenerUsuario()
        Dim intCveUsu As Integer = Val(hdnIdRegistro.Value)
        Dim dbData As New DataSet
        Try
            dbData = clsUsuario.ObtenerUsuarioCve(intCveUsu)

            If dbData.Tables.Count > 0 AndAlso dbData.Tables(0).Rows.Count > 0 Then
                With dbData.Tables(0).Rows(0)
                    lblID.Text = .Item("PDK_ID_USUARIO").ToString.Trim
                    txtNombre.Text = .Item("PDK_USU_NOMBRE").ToString.Trim
                    txtPaterno.Text = .Item("PDK_USU_APE_PAT").ToString.Trim
                    txtMaterno.Text = .Item("PDK_USU_APE_MAT").ToString.Trim
                    txtcorreo.Text = .Item("PDK_USU_CORREO_ELECTRONICO").ToString.Trim
                    txtUsuario.Text = .Item("PDK_USU_CLAVE").ToString.Trim
                    If .Item("PDK_ID_PERFIL") > 0 Then
                        LlenarPerfil()
                        cmbPerfil.SelectedIndex = cmbPerfil.Items.IndexOf(cmbPerfil.Items.FindByValue(.Item("PDK_ID_PERFIL")))

                    End If
                    chkStatus.Checked = IIf(.Item("PDK_USU_ACTIVO") = 2, True, False)
                End With
            Else
                Master.MensajeError("No se localizó información del Usuario")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Private Sub LlenarPerfil()
        Try
            Dim dtsRed As New DataSet
            Dim objCombo As New clsParametros
            dtsRed = clsPerfil.ObtenTodos()
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_PER_NOMBRE", "PDK_ID_PERFIL", cmbPerfil)
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaUsuario.aspx?idPerfil=" & cmbPerfil.SelectedValue)
    End Sub
    Protected Sub btnGuardar_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidarCam() Then
                Dim intCveUsu As Integer = Val(hdnIdRegistro.Value)
                Dim objUsu As New clsUsuario
                Dim i As Integer = 0

                Dim dataset As New DataSet
                dataset = clsUsuario.obtenerValidadUsuario(txtUsuario.Text.Trim, 2, intCveUsu)
                If dataset.Tables(0).Rows.Count > 0 AndAlso dataset.Tables.Count > 0 Then
                    Master.MensajeError("El Usuario ya existe")
                    Exit Sub
                End If

                objUsu.PDK_ID_USUARIO = intCveUsu
                objUsu.PDK_USU_NOMBRE = txtNombre.Text.Trim.ToUpper
                objUsu.PDK_USU_APE_PAT = txtPaterno.Text.Trim.ToUpper
                objUsu.PDK_USU_APE_MAT = txtMaterno.Text.Trim.ToUpper
                objUsu.PDK_USU_ACTIVO = IIf(chkStatus.Checked = True, 2, 3)
                objUsu.PDK_ID_PERFIL = cmbPerfil.SelectedValue
                objUsu.PDK_USU_MODIF = Format(Now(), "yyyy-MM-dd")
                objUsu.PDK_USU_CORREO_ELECTRONICO = txtcorreo.Text.Trim
                objUsu.PDK_USU_CLAVE = txtUsuario.Text.Trim.ToUpper

                Dim MD As New clsSeguridad
                objUsu.ManejaUsuario(3)
                Master.MensajeError("La información se guardo con éxito")
                hdnIdRegistro.Value = objUsu.PDK_ID_USUARIO
                For i = 0 To grvDistribu.Rows.Count - 1
                    Dim chek As New CheckBox
                    Dim num As Integer = 0
                    chek = CType(grvDistribu.Rows(i).FindControl("chkDistribuir"), CheckBox)

                    If chek.Checked = True Then
                        num = 2
                    Else
                        num = 3
                    End If
                    num = clsUsuario.GuardarDistribuidora(grvDistribu.Rows(i).Cells(0).Text(), hdnIdRegistro.Value, num, Session("IdUsua"))


                Next
                LlenarDistribuir()
                ObtenerUsuario()
            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Public Function ValidarCam() As Boolean
        ValidarCam = False

        If txtNombre.Text.Trim.Length = 0 Then Exit Function

        ValidarCam = True



    End Function

    Protected Sub chkTodos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fila As GridViewRow
        For Each fila In grvDistribu.Rows
            If CType(grvDistribu.HeaderRow.FindControl("chkTodos"), CheckBox).Checked = True Then
                CType(fila.FindControl("chkDistribuir"), CheckBox).Checked = True
            Else
                CType(fila.FindControl("chkDistribuir"), CheckBox).Checked = False

            End If
        Next

    End Sub

End Class