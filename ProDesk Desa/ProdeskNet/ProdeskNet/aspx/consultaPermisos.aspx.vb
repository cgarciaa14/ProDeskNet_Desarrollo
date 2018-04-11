'BUG-PD-197 : ERODRIGUEZ: 23/08/2017: Se realizo validacion para limitar perfiles disponibles.

Imports ProdeskNet.Seguridad
Imports ProdeskNet.Catalogos
Imports System.Data

Public Class consultaPermisos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"
        Dim intPerfil As Integer = 0
        Try
            If Not IsPostBack Then
                intPerfil = Request.QueryString("idPerfil")
                LlenaCombo(1)
                LlenaCombo(2)

                If intPerfil > 0 Then
                    ddlPerfil.SelectedIndex = ddlPerfil.Items.IndexOf(ddlPerfil.Items.FindByValue(intPerfil.ToString.Trim))
                End If
                CargaArbol()
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub LlenaCombo(ByVal intCombo As Integer)
        Dim dsDataset As New DataSet
        Dim i As Integer = 0
        Dim objPer As New clsPermisos
        Try

            If intCombo = 1 Then 'Perfiles

                If objPer.BuscaPermiso(Session("IdUsua")) Then
                    dsDataset = clsPerfil.ObtenTodos
                Else
                    dsDataset = clsPerfil.ObtenPerfiles(1)
                End If

                ddlPerfil.DataSource = dsDataset
                ddlPerfil.DataTextField = "PDK_PER_NOMBRE"
                ddlPerfil.DataValueField = "PDK_ID_PERFIL"
                ddlPerfil.DataBind()
            End If

            If intCombo = 2 Then 'Llena combo Padres
                dsDataset = clsMenu.ObtenPadres()
                ddlMenu.Items.Add(New ListItem("", "0"))
                For i = 0 To dsDataset.Tables(0).Rows.Count - 1
                    ddlMenu.Items.Add(New ListItem(dsDataset.Tables(0).Rows(i).Item("PDK_MEN_DESCRIPCION").ToString, dsDataset.Tables(0).Rows(i).Item("PDK_ID_MENU").ToString))
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub CargaArbol()
        Dim objNodo As New TreeNode
        'Dim objMenu As New Object 'SNSeg_Protracker.clsPermisos
        Dim objMenu As New clsPermisos

        Dim dtsMenu As New DataSet
        Dim dtsPermiso As New DataSet
        Dim objRow As DataRow

        trvObj.Nodes.Clear()
        If (Val(ddlMenu.SelectedValue)) > 0 Then
            objMenu.PDK_ID_MENU = Val(ddlMenu.SelectedValue)
            'objMenu.PDK_ID_MENU = 0
        Else
            objMenu.PDK_ID_MENU = 0
        End If
        objMenu.PDK_ID_USUARIO_REG = Session("IdUsua")
        objMenu.PDK_ID_PERFIL = Val(ddlPerfil.SelectedValue)
        objMenu.PDK_ID_PADRE = -1
        dtsMenu = objMenu.ManejaPermisos(2)

        If dtsMenu.Tables.Count > 0 Then
            For Each objRow In dtsMenu.Tables(0).Rows

                objNodo = New TreeNode
                objNodo.Value = objRow.Item("PDK_ID_MENU") & "|" & objRow.Item("PDK_ID_PERMISOS")
                CargaHijosArbol(objNodo)

                If objRow.Item("PDK_PER_STATUS") = 2 Then
                    objNodo.ImageUrl = "~/APP_THEMES/Imagenes/ok.png"
                Else
                    objNodo.ImageUrl = "~/APP_THEMES/Imagenes/cross.png"
                End If

                objNodo.Text = objRow.Item("PDK_MEN_DESCRIPCION")
                objNodo.Expanded = True
                trvObj.Nodes.Add(objNodo)

            Next
        End If
    End Sub


    Private Sub CargaHijosArbol(ByRef objNodo As TreeNode)
        Try
            Dim dtsRes As New DataSet
            Dim objRow As DataRow
            Dim strPaso As String() = Split(objNodo.Value, "|")
            Dim intObj As Integer = Val(strPaso(0))
            Dim intObjPermiso As Integer = Val(strPaso(1))
            Dim clsObj As New clsPermisos 'Object 'SNSeg_Protracker.clsPermisos()
            Dim objNodoHijo As TreeNode

            'clsObj.PDK_ID_MENU = intObj
            clsObj.PDK_ID_PERFIL = Val(ddlPerfil.SelectedValue)
            clsObj.PDK_ID_USUARIO_REG = Session("IdUsua")
            'dtsRes = clsObj.ManejaPermisos(5)
            clsObj.PDK_ID_PADRE = intObj
            dtsRes = clsObj.ManejaPermisos(2)

            If Trim(clsObj.ErrorPermisos) = "" Then
                If dtsRes.Tables.Count > 0 Then
                    For Each objRow In dtsRes.Tables(0).Rows
                        objNodoHijo = New TreeNode
                        objNodoHijo.Value = objRow.Item("PDK_ID_MENU") & "|" & objRow.Item("PDK_ID_PERMISOS") & "|" & intObj & "|" & intObjPermiso
                        objNodoHijo.Text = "&nbsp " & objRow.Item("PDK_MEN_DESCRIPCION")
                        objNodoHijo.Checked = IIf(objRow.Item("PDK_PER_STATUS") = 2, True, False)

                        objNodo.ChildNodes.Add(objNodoHijo)
                    Next
                End If
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub trvObj_TreeNodeCheckChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles trvObj.TreeNodeCheckChanged

        Dim strPaso As String() = Split(e.Node.Value, "|")
        Dim intObj As Integer = Val(strPaso(0))
        Dim intPerm As Integer = Val(strPaso(1))
        Dim intPadre As Integer = Val(strPaso(2))
        Dim intPadrePermiso As Integer = Val(strPaso(3))
        Dim intStatus As Integer = IIf(e.Node.Checked, 2, 3)
        Dim bolActualizaPadre As Boolean = False
        Dim bolGuardaPadre As Boolean = False
        Dim intRegistros As Integer = 0
        Dim intRegistrosStatus As Integer = 0
        Dim dtsRes As New DataSet
        Dim dtsResPadre As New DataSet

        'Dim objPerm As New Object 'SNSeg_Protracker.clsPermisos(intPerm)
        Dim objPerm As New clsPermisos


        objPerm.PDK_ID_USUARIO_REG = Session("IdUsua")
        objPerm.PDK_PER_STATUS = intStatus
        If intPerm = 0 Then
            '" VALUES (" & intMenu & ",  " & intCveUsu & ",  " & intPerfil & ",  " & intStatus & ", '" & strModi & "', " & intCveUsu & ",  " & _
            objPerm.PDK_ID_MENU = intObj
            objPerm.PDK_ID_PERFIL = Val(ddlPerfil.SelectedValue)
            objPerm.PDK_ID_USUARIO = 0
            objPerm.PDK_PER_STATUS = intStatus
            objPerm.PDK_PER_MODIF = Format(Now(), "yyyy-MM-dd")
            objPerm.PDK_CLAVE_USUARIO = 1
            'Guardar el Nuevo Perfil
            objPerm.ManejaPermisos(3)

        Else
            'Actualiza el estatus del Perfil
            objPerm.PDK_ID_PERMISOS = intPerm

            objPerm.PDK_ID_MENU = intObj
            objPerm.PDK_ID_PERFIL = Val(ddlPerfil.SelectedValue)
            objPerm.PDK_ID_USUARIO = 0
            objPerm.PDK_PER_STATUS = intStatus
            objPerm.PDK_PER_MODIF = Format(Now(), "yyyy-MM-dd")
            objPerm.PDK_CLAVE_USUARIO = 1
            objPerm.ManejaPermisos(4)

        End If


        'Existe el Padre
        objPerm.PDK_ID_PERFIL = Val(ddlPerfil.SelectedValue)
        objPerm.PDK_ID_MENU = intPadre
        objPerm.PDK_ID_PERMISOS = intPadrePermiso
        dtsResPadre = objPerm.ManejaPermisos(2)
        If Val(dtsResPadre.Tables(0).Rows(0).Item("PDK_ID_PERMISOS").ToString) = 0 Then
            bolGuardaPadre = True
        End If

        objPerm.PDK_ID_PERFIL = Val(ddlPerfil.SelectedValue)
        objPerm.PDK_ID_PADRE = intPadre
        objPerm.PDK_ID_MENU = 0
        dtsRes = objPerm.ManejaPermisos(2)
        intRegistrosStatus = dtsRes.Tables(0).Select("PDK_PER_STATUS=2").Count


        If bolGuardaPadre = True Then
            objPerm.PDK_ID_MENU = intPadre
            objPerm.PDK_ID_PERFIL = Val(ddlPerfil.SelectedValue)
            objPerm.PDK_ID_USUARIO = 0
            objPerm.PDK_PER_STATUS = intStatus
            objPerm.PDK_PER_MODIF = Format(Now(), "yyyy-MM-dd")
            objPerm.PDK_CLAVE_USUARIO = 1
            objPerm.ManejaPermisos(3)
        Else
            If intRegistrosStatus > 0 Then
                objPerm.PDK_ID_PERMISOS = intPadrePermiso
                objPerm.PDK_ID_MENU = intPadre
                objPerm.PDK_ID_PERFIL = Val(ddlPerfil.SelectedValue)
                objPerm.PDK_ID_USUARIO = 0
                objPerm.PDK_PER_STATUS = 2
                objPerm.PDK_PER_MODIF = Format(Now(), "yyyy-MM-dd")
                objPerm.PDK_CLAVE_USUARIO = 1
                objPerm.ManejaPermisos(4)
            Else
                objPerm.PDK_ID_PERMISOS = intPadrePermiso
                objPerm.PDK_ID_MENU = intPadre
                objPerm.PDK_ID_PERFIL = Val(ddlPerfil.SelectedValue)
                objPerm.PDK_ID_USUARIO = 0
                objPerm.PDK_PER_STATUS = 3
                objPerm.PDK_PER_MODIF = Format(Now(), "yyyy-MM-dd")
                objPerm.PDK_CLAVE_USUARIO = 1
                objPerm.ManejaPermisos(4)
            End If
        End If




        CargaArbol()

        Response.Redirect("./consultaPermisos.aspx?idPerfil=" & ddlPerfil.SelectedValue & "")
    End Sub

    Private Sub ddlPerfil_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPerfil.SelectedIndexChanged
        CargaArbol()
    End Sub

    Private Sub ddlMenu_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlMenu.SelectedIndexChanged
        CargaArbol()
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargaArbol()
    End Sub

   
End Class