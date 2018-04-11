Imports ProdeskNet.Catalogos
Imports System.Data
Partial Class aspx_altaAgencia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then
            LlenaCombo(1)
            llenaddlDistribuidores()
        End If

    End Sub
    Private Sub llenaddlDistribuidores(Optional ByVal idDist As Integer = 0)
        ddlDistribuidor.DataSource = clsEmpresa.obtenDistribuidor(idDist)
        ddlDistribuidor.DataTextField = "PDK_DIST_DISTRIBUIDOR"
        ddlDistribuidor.DataValueField = "PDK_ID_DIST_DISTRIBUIDOR"
        ddlDistribuidor.DataBind()
    End Sub

    Private Sub LlenaCombo(ByVal intCombo As Integer)
        Dim dsDataset As New DataSet
        Dim dsDistribuidor As New DataSet
        Try

            If intCombo = 1 Then
                dsDataset = clsEmpresa.ObtenTodos(1)
                cmdEmpresa.DataSource = dsDataset
                cmdEmpresa.DataTextField = "PDK_EMP_NOMBRE"
                cmdEmpresa.DataValueField = "PDK_ID_EMPRESA"
                cmdEmpresa.DataBind()                
            End If
        Catch ex As Exception
            cmdEmpresa = Nothing
        End Try
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaAgencia.aspx")
    End Sub


    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtNombre.Text.Trim.Length = 0 Then Exit Function
        'If txtClaveDistribuidor.Text.Trim.Length = 0 Then Exit Function

        ValidaCampos = True

    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                Dim objDistribuidor As New clsDistribuidor

                'guardamos la información
                objDistribuidor.PDK_DIST_NOMBRE = txtNombre.Text.Trim.ToUpper
                objDistribuidor.PDK_ID_DIST_DISTRIBUIDOR = ddlDistribuidor.SelectedValue                
                'objDistribuidor.PDK_DIST_CLAVE = txtClaveDistribuidor.Text.Trim
                objDistribuidor.PDK_DIST_ACTIVO = IIf(Me.chkStatus.Checked = True, 2, 3)
                objDistribuidor.PDK_ID_EMPRESA = CInt(Me.cmdEmpresa.SelectedValue.Trim)
                objDistribuidor.PDK_DIST_MODIF = Format(Now(), "yyyy-MM-dd")
                objDistribuidor.PDK_CLAVE_USUARIO = Session("IdUsua")

                objDistribuidor.Guarda()

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
