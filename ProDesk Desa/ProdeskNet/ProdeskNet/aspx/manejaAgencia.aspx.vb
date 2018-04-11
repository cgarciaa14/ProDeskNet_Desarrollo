'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.
Imports ProdeskNet.Catalogos
Imports System.Data

Partial Class aspx_manejaAgencia
    Inherits System.Web.UI.Page

    Dim objEmpresa As New clsEmpresa(0)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("idDist")) > 0 Then

                hdnIdRegistro.Value = Request("idDist")
                CargaInfo()
                llenaddlDistribuidores()
                objEmpresa.PDK_ID_DISTRIBUIDOR = Request("idDist")
                objEmpresa.getDistribuidorActual()
                ddlDistribuidor.SelectedValue = objEmpresa.PDK_ID_DIST_DISTRIBUIDOR

            End If
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


    Private Sub CargaInfo()
        Try
            Dim intClave As Integer = Val(hdnIdRegistro.Value)

            'Dim objDistribuidor As New clsDistribuidor(intClave)
            Dim objDistribuidor As New clsDistribuidor

            objDistribuidor.PDK_ID_DISTRIBUIDOR = intClave

            objDistribuidor.obtenAgencia()

            objDistribuidor.ObtenDistribuidores()

            If objDistribuidor.PDK_ID_DISTRIBUIDOR > 0 Then

                lblID.Text = objDistribuidor.PDK_ID_DISTRIBUIDOR
                txtNombre.Text = objDistribuidor.PDK_DIST_NOMBRE
                'txtClaveDistribuidor.Text = objDistribuidor.PDK_DIST_CLAVE
                chkStatus.Checked = IIf(objDistribuidor.PDK_DIST_ACTIVO = 2, True, False)

                If objDistribuidor.PDK_ID_EMPRESA > 0 Then
                    LlenaCombo(1)
                    cmdEmpresa.SelectedValue = objDistribuidor.PDK_ID_EMPRESA
                End If

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
        'If Trim(txtClaveDistribuidor.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim path As String = "./consultaAgencia.aspx"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intCve As Integer = Val(hdnIdRegistro.Value)
                Dim objDistribuidor As New clsDistribuidor
                Dim intOpc As Integer = 1

                If intCve > 0 Then
                    intOpc = 2
                End If

                'guardamos la información
                objDistribuidor.PDK_ID_DISTRIBUIDOR = intCve
                objDistribuidor.PDK_DIST_NOMBRE = txtNombre.Text.Trim.ToUpper
                'objDistribuidor.PDK_DIST_CLAVE = txtClaveDistribuidor.Text.Trim.ToUpper
                objDistribuidor.PDK_ID_DIST_DISTRIBUIDOR = ddlDistribuidor.SelectedValue
                objDistribuidor.PDK_DIST_ACTIVO = IIf(chkStatus.Checked = True, 2, 3)
                objDistribuidor.PDK_ID_EMPRESA = CInt(Me.cmdEmpresa.SelectedValue.ToString)
                objDistribuidor.PDK_DIST_MODIF = Format(Now(), "yyyy-MM-dd")
                objDistribuidor.PDK_CLAVE_USUARIO = Session("IdUsua")
                objDistribuidor.PDK_DIST_CLAVE = intCve

                objDistribuidor.Guarda()


                Master.MensajeError("La información se guardo con éxito")
                hdnIdRegistro.Value = objDistribuidor.PDK_ID_DISTRIBUIDOR
                CargaInfo()

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

End Class
