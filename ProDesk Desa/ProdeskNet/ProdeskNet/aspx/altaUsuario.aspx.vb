Imports ProdeskNet.Seguridad
Imports System.Data

Public Class altaUsuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then
            ' CargarCombo(1)
            LlenarPerfil()
            LlenarDistribuidora()
        End If
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaUsuario.aspx")
    End Sub
    Private Sub LlenarDistribuidora()
        Dim dataDistri As New DataSet

        Try
            grvDistribu.DataSource = Nothing
            grvDistribu.DataBind()


            dataDistri = clsUsuario.ObtenerDistribudir()
            If dataDistri.Tables.Count > 0 AndAlso dataDistri.Tables(0).Rows.Count > 0 Then
                grvDistribu.DataSource = dataDistri.Tables(0)
                grvDistribu.DataBind()


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
    Private Function CargarCombo(ByVal intObj As Integer) As Boolean
        CargarCombo = False
        Dim dtsRes As New DataSet
        Dim objCombo As New clsParametros

        Try
            Select Case intObj
                Case 1
                    'Combo de status
                    dtsRes = objCombo.ObtenInfoParametros(1)
                    If Trim$(objCombo.ErrorParametro) = "" Then
                        'objCombo.LlenaCombos(dtsRes, "PDK_PAR_SIS_PARAMETRO", "PDK_ID_PARAMETROS_SISTEMA", cmbStatus)
                        If Trim$(objCombo.ErrorParametro) <> "" Then
                            Master.MensajeError(objCombo.ErrorParametro)
                            Exit Function
                        End If
                    Else
                        Master.MensajeError(objCombo.ErrorParametro)
                        Exit Function
                    End If
            End Select
            CargarCombo = True
        Catch ex As Exception

        End Try

    End Function
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            Dim dtsRes As New DataSet
            Dim i As Integer = 0
            If lblCveUsu.Text <> "" Then
                Master.MensajeError("Error al intentar guardar")
                Exit Sub
            End If

            If ValidarCam() Then

                Dim dataset As New DataSet
                dataset = clsUsuario.obtenerValidadUsuario(txtUsuario.Text.Trim, 1, 0)
                If dataset.Tables(0).Rows.Count > 0 AndAlso dataset.Tables.Count > 0 Then
                    Master.MensajeError("El Usuario ya existe")
                    Exit Sub
                End If

                Dim objUsuario As New clsUsuario
                'guardamos la información
                objUsuario.PDK_USU_NOMBRE = txtNombre.Text.Trim.ToUpper
                objUsuario.PDK_USU_APE_PAT = txtPaterno.Text.Trim.ToUpper
                objUsuario.PDK_USU_APE_MAT = txtMaterno.Text.Trim.ToUpper
                objUsuario.PDK_USU_CLAVE = txtUsuario.Text.Trim.ToUpper
                objUsuario.PDK_USU_ACTIVO = IIf(chkActivo.Checked = True, 2, 3)
                objUsuario.PDK_USU_MODIF = Format(Now(), "yyyy-MM-dd")
                objUsuario.PDK_USU_VIGENCIA = 30
                objUsuario.PDK_USU_CORREO_ELECTRONICO = txtcorreo.Text.Trim
                objUsuario.PDK_ID_PERFIL = cmbPerfil.SelectedValue
                Dim MD As New clsSeguridad
                objUsuario.PDK_USU_CONTRASENA = MD.EncriptaDesencriptaCadena(txtContraseña.Text, False)


                objUsuario.ManejaUsuario(2)
                lblCveUsu.Text = objUsuario.PDK_ID_USUARIO

                For i = 0 To grvDistribu.Rows.Count - 1
                    Dim chek As New CheckBox
                    Dim num As Integer = 0
                    chek = CType(grvDistribu.Rows(i).FindControl("chkDistribuir"), CheckBox)

                    If chek.Checked = True Then
                        num = clsUsuario.GuardarDistribuidora(grvDistribu.Rows(i).Cells(0).Text(), lblCveUsu.Text, 2, Session("IdUsua"))
                    End If


                Next

                If objUsuario.ErrorUsuario <> "" Then

                    Master.MensajeError(" Error al guardar PDK_ID_USUARIO")
                Else
                    Master.MensajeError("Información almacenada exitosamente")

                End If
            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
                Exit Sub
            End If


        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Public Function ValidarCam() As Boolean
        ValidarCam = False

        If txtUsuario.Text.Trim.Length = 0 Then Exit Function
        If txtContraseña.Text.Trim.Length = 0 Then Exit Function
        If txtNombre.Text.Trim.Length = 0 Then Exit Function

    



        ValidarCam = True



    End Function
    'Protected Sub chkTodos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim fila As GridViewRow
    '    For Each fila In grvDistribu.Rows
    '        If CType(grvDistribu.HeaderRow.FindControl("chkTodos"), CheckBox).Checked = True Then
    '            CType(fila.FindControl("chkDistribuir"), CheckBox).Checked = True
    '        Else
    '            CType(fila.FindControl("chkDistribuir"), CheckBox).Checked = False

    '        End If
    '    Next

    'End Sub

   
End Class