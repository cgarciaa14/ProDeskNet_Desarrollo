'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Catalogos
Imports System.Data

Public Class manejaMenu
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("idMen")) > 0 Then
                hdnIdRegistro.Value = Request("idMen")
                LlenaCombo(1)
                LlenaCombo(2)

                CargaInfo()
                ddlTipoObjeto_SelectedIndexChanged(sender, e)
            End If
        End If

    End Sub

    Private Sub LlenaCombo(ByVal intCombo As Integer)
        Dim dsDataset As New DataSet
        Dim i As Integer = 0
        Try

            If intCombo = 1 Then 'Tipo de Objeto
                dsDataset = clsParametrosSistema.ObtenTodos(4)
                ddlTipoObjeto.DataSource = dsDataset
                ddlTipoObjeto.DataTextField = "PDK_PAR_SIS_PARAMETRO"
                ddlTipoObjeto.DataValueField = "PDK_ID_PARAMETROS_SISTEMA"
                ddlTipoObjeto.DataBind()
            End If

            If intCombo = 2 Then 'Llena combo Padres
                dsDataset = clsMenu.ObtenPadres()
                ddlPadre.DataSource = dsDataset
                ddlPadre.DataTextField = "PDK_MEN_DESCRIPCION"
                ddlPadre.DataValueField = "PDK_ID_MENU"
                ddlPadre.DataBind()

            End If
        Catch ex As Exception
            ddlTipoObjeto = Nothing
        End Try
    End Sub


    Private Sub CargaInfo()
        Try
            Dim intClave As Integer = Val(hdnIdRegistro.Value)
            Dim objMenu As New clsMenu(0, intClave)
            If objMenu.PDK_ID_MENU > 0 Then
                lblID.Text = objMenu.PDK_ID_MENU
                ddlTipoObjeto.SelectedIndex = ddlPadre.Items.IndexOf(ddlPadre.Items.FindByValue(objMenu.PDK_MEN_OBJETO.ToString.Trim))
                ddlPadre.SelectedIndex = ddlPadre.Items.IndexOf(ddlPadre.Items.FindByValue(objMenu.PDK_MEN_ID_PADRE.ToString.Trim))
                txtDescripcion.Text = objMenu.PDK_MEN_DESCRIPCION
                txtLink.Text = objMenu.PDK_MEN_LINK
                chkAcc.Checked = False
            Else
                Master.MensajeError("No se localizó información")
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If ddlTipoObjeto.SelectedValue = 6 Then
            If Trim(txtDescripcion.Text) = "" Then Exit Function
        Else
            If Trim(txtDescripcion.Text) = "" Then Exit Function
            If Trim(txtLink.Text) = "" Then Exit Function
        End If
        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim path As String = "./consultaMenu.aspx"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intCve As Integer = Val(hdnIdRegistro.Value)
                Dim objMenu As New clsMenu(intCve)
                Dim intOpc As Integer = 1

                If intCve > 0 Then
                    intOpc = 2
                End If

                'guardamos la información
                objMenu.PDK_ID_MENU = intCve
                objMenu.PDK_MEN_OBJETO = ddlTipoObjeto.SelectedValue
                objMenu.PDK_MEN_DESCRIPCION = Me.txtDescripcion.Text.Trim
                objMenu.PDK_MEN_ACCESO_DIRECTO = 0
                objMenu.PDK_MEN_ID_PADRE = Me.ddlPadre.SelectedValue
                objMenu.PDK_MEN_LINK = Me.txtLink.Text.Trim
                objMenu.PDK_MEN_MODIF = Format(Now(), "yyyy-MM-dd")
                objMenu.PDK_MEN_TIPO_PERMISO = 10
                objMenu.PDK_MEN_NIVEL = IIf(ddlTipoObjeto.SelectedValue = 5, 1, 2)
                objMenu.PDK_CLAVE_USUARIO = Session("IdUsua")

                objMenu.Guarda()


                Master.MensajeError("La información se guardo con éxito")
                hdnIdRegistro.Value = objMenu.PDK_ID_MENU
                CargaInfo()

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub


    Private Sub ddlTipoObjeto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTipoObjeto.SelectedIndexChanged
        Try

            If ddlTipoObjeto.SelectedValue = 5 Then
                Me.trPadre.Visible = False
                Me.trAcc.Visible = False
                Me.trLink.Visible = False
            Else
                Me.trPadre.Visible = True
                Me.trAcc.Visible = True
                Me.trLink.Visible = True
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class