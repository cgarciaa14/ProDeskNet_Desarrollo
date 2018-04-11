'BUG-PD-197 : ERODRIGUEZ: 23/08/2017: Se realizo validacion para limitar perfiles disponibles.
'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.
Imports ProdeskNet.Seguridad
Imports System.Data

Public Class consultaUsuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"
        If Not IsPostBack Then
            Session("sBandera") = 0
            Session("Nivel") = 0
            Session("PagPdr" & Session("Nivel")) = Me.Page.Request.Url.PathAndQuery
            Session("perfil") = Request("idPerfil")
            If Session("perfil") <> Nothing Then
                LlenarPerfil()
                cmbPerfil.SelectedIndex = cmbPerfil.Items.IndexOf(cmbPerfil.Items.FindByValue(Session("perfil")))
                BuscaDatos()
            Else
                'Llenar combo del perfil
                LlenarPerfil()
                BuscaDatos()

            End If

        End If
    End Sub
    Private Sub LlenarPerfil()
        Dim objCombo As New clsParametros
        Dim objPer As New clsPermisos
        Try
            Dim dtsRed As New DataSet
            If objPer.BuscaPermiso(Session("IdUsua")) Then
                dtsRed = clsPerfil.ObtenTodos()
            Else
                dtsRed = clsPerfil.ObtenPerfiles(1)
            End If


            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_PER_NOMBRE", "PDK_ID_PERFIL", cmbPerfil)
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
   
    Private Sub BuscaDatos()
        Try
            Dim dtsRes As New DataSet
            Dim strOrden As String = ""


            dtsRes = clsUsuario.ObtenerTodo(cmbPerfil.SelectedValue)



            Session("dtsConsulta") = Nothing
            grvConsulta.PageIndex = 0
            grvConsulta.DataSource = Nothing

            If ViewState("strCampos") <> String.Empty Then
                strOrden = ViewState("strCampos") & " " & ViewState("strOrden")
            End If

            If dtsRes.Tables.Count > 0 Then
                If dtsRes.Tables(0).Rows.Count > 0 Then
                    dtsRes.Tables(0).DefaultView.Sort = strOrden.ToUpper ' Se realizo un order al Data Set 
                    Session("dtsConsulta") = dtsRes
                    grvConsulta.DataSource = dtsRes.Tables(0).DefaultView ' Se agrego el DefaultView para que se mostrara ordenado
                Else
                    Master.MensajeError("No se encontró información con los parámetros proporcionados")
                End If
            Else
                Master.MensajeError("No se encontró información para los parámetros proporcionados")
            End If

            grvConsulta.DataBind()
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    'Protected Sub btnBuscar_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
    '    BuscaDatos()
    'End Sub


    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./altaUsuario.aspx", False)
    End Sub

    Protected Sub grvConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand

        If e.CommandName = "catIdUsu" Then
            Session("Nivel") += 1
            Dim path As String = "./manejaUsuario.aspx?idUsu=" & e.CommandArgument
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
        ElseIf e.CommandName = "Sort" Then
            If ViewState("strCampos") = e.CommandArgument Then
                If ViewState("strOrden") = "Asc" Then
                    ViewState("strOrden") = "Desc"
                    Session("sBandera") = 1
                Else
                    ViewState("strOrden") = "Asc"
                    Session("sBandera") = 1
                End If
            Else
                ViewState("strCampos") = e.CommandArgument
                ViewState("strOrden") = "Asc"
                Session("sBandera") = 1
            End If
            BuscaDatos()
            ' Fin Tracker R02-P1-1  JMMM 130212
        End If
    End Sub

    Protected Sub grvConsulta_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
        Dim dts As New DataSet
        Dim strOrden As String = ""

        grvConsulta.PageIndex = e.NewPageIndex
        dts = CType(Session("dtsConsulta"), DataSet)

        If ViewState("strCampos") <> String.Empty Then
            strOrden = ViewState("strCampos") & " " & ViewState("strOrden")
        End If

        dts.Tables(0).DefaultView.Sort = strOrden.ToUpper ' Se realizo un order al Data Set 
        grvConsulta.DataSource = dts.Tables(0).DefaultView
        grvConsulta.DataBind()

    End Sub


    Private Sub cmbPerfil_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPerfil.SelectedIndexChanged
        BuscaDatos()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "script", "  $(document).ready(function () { $('#search').keyup(function () { searchTable($(this).val(), $('[id$=grvConsulta]'));}); });", True)
    End Sub

End Class