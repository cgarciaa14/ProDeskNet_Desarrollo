Imports ProdeskNet.Catalogos
Imports System.Data

Public Class altaDistribuidora
    Inherits System.Web.UI.Page
    Dim objCombo As New ProdeskNet.Seguridad.clsParametros

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then
            LlenaCombo(1)
            llenaPlaza()
        End If

    End Sub


    Private Sub LlenaCombo(ByVal intCombo As Integer)
        Dim dsDataset As New DataSet
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
    Public Sub llenaPlaza()
        Try
            Dim objEmpresa As New clsEmpresa(0)
            objEmpresa.PDK_ID_EMPRESA = cmdEmpresa.SelectedValue
            objEmpresa.getPlaza()
            objCombo.LlenaCombos(objEmpresa.dsPlaza, "PDK_PLAZA_NOMBRE", "PDK_ID_PLAZA", ddlPlaza)
            Master.MensajeError("Se debe de seleccionar una empresa")            

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaDistribuidor.aspx")
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
                'objDistribuidor.PDK_DIST_NOMBRE = txtNombre.Text.Trim.ToUpper
                objDistribuidor.PDK_DIST_DISTRIBUIDOR = txtNombre.Text.Trim.ToUpper
                'objDistribuidor.PDK_DIST_CLAVE = txtClaveDistribuidor.Text.Trim
                'objDistribuidor.PDK_DIST_ACTIVO = IIf(Me.chkStatus.Checked = True, 2, 3)
                objDistribuidor.PDK_FN_DIST_DISTRIBUIDOR_ACTIVO = Me.chkStatus.Checked
                'objDistribuidor.PDK_ID_EMPRESA = CInt(Me.cmdEmpresa.SelectedValue.Trim)
                objDistribuidor.PDK_ID_PLAZA = cmdEmpresa.SelectedValue
                objDistribuidor.PDK_DIST_MODIF = Format(Now(), "yyyy-MM-dd")
                objDistribuidor.PDK_CLAVE_USUARIO = Session("IdUsua")

                objDistribuidor.GuardaDistribuidor()

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