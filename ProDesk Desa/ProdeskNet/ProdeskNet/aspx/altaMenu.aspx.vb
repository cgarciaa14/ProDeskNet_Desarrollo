Imports ProdeskNet.Seguridad
Imports ProdeskNet.Catalogos
Imports System.Data

Public Class altaMenu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then
            LlenaCombo(1)
            LlenaCombo(2)
            ddlTipoObjeto_SelectedIndexChanged(sender, e)
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
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaMenu.aspx")
    End Sub


    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtDescripcion.Text.Trim.Length = 0 Then Exit Function

        ValidaCampos = True

    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                Dim objMenu As New clsMenu(0)

                'guardamos la información
                objMenu.PDK_MEN_OBJETO = ddlTipoObjeto.SelectedValue
                objMenu.PDK_MEN_DESCRIPCION = Me.txtDescripcion.Text.Trim
                objMenu.PDK_MEN_ACCESO_DIRECTO = 0
                objMenu.PDK_MEN_ID_PADRE = IIf(ddlTipoObjeto.SelectedValue = 5, 0, Val(Me.ddlPadre.SelectedValue))
                objMenu.PDK_MEN_LINK = IIf(ddlTipoObjeto.SelectedValue = 5, 0, Me.txtLink.Text.Trim)
                objMenu.PDK_MEN_MODIF = Format(Now(), "yyyy-MM-dd")
                objMenu.PDK_MEN_TIPO_PERMISO = 10
                objMenu.PDK_MEN_NIVEL = IIf(ddlTipoObjeto.SelectedValue = 5, 1, 2)
                objMenu.PDK_CLAVE_USUARIO = Session("IdUsua")

                objMenu.Guarda()

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
                Exit Sub
            End If

            Master.MensajeError("Información almacenada exitosamente")

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