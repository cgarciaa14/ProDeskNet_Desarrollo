Imports ProdeskNet.Seguridad
Imports System.Data

Public Class altaPerfil
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then
            Dim dataset As New DataSet
            dataset = clsPerfil.ObtenerNivel()
            If dataset.Tables.Count > 0 AndAlso dataset.Tables(0).Rows.Count > 0 Then
                txtNivel.Text = dataset.Tables(0).Rows(0).Item("NIVEL").ToString
            End If


        End If

    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaPerfil.aspx")
    End Sub


    Private Function ValidaCampos() As Boolean
        ValidaCampos = False
    
        If txtNombre.Text.Trim.Length = 0 Then Exit Function
        If txtNivel.Text.Trim.Length = 0 Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                Dim objPerfil As New clsPerfil(0)
                'Dim dsData As New DataSet
                'dsData = clsPerfil.ValidarNiveles(txtNivel.Text)
                'If dsData.Tables(0).Rows.Count > 0 AndAlso dsData.Tables.Count > 0 Then
                '    Master.MensajeError("El nivel ya existe agregar otro")
                '    Exit Sub

                'End If

                'guardamos la información
                objPerfil.PDK_PER_NOMBRE = txtNombre.Text.Trim.ToUpper
                objPerfil.PDK_PER_ACTIVO = IIf(chkPerfil.Checked = True, 2, 3)
                objPerfil.PDK_PER_MODIF = Format(Now(), "yyyy-MM-dd")
                objPerfil.PDK_PER_NIVEL = txtNivel.Text.Trim
                objPerfil.PDK_CLAVE_USUARIO = Session("IdUsua")

                objPerfil.Guarda()

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