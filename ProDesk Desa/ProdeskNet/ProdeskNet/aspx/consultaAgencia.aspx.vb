'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Seguridad
Imports System.Data
Partial Class aspx_consultaAgencia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then
            Session("sBandera") = 0
            Session("Nivel") = 0
            Session("PagPdr" & Session("Nivel")) = Me.Page.Request.Url.PathAndQuery
            LlenarEmpresa()
            BuscaDatos()
        End If

        Master.EjecutaJS("$(document).ready(function () {$('#search').keyup(function () {searchTable($(this).val(), $('[id$=grvConsulta]'));});});")

    End Sub
    Public Sub LlenarEmpresa()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet

            dtsRed = clsEmpresa.ObtenTodos(1)
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_EMP_NOMBRE", "PDK_ID_EMPRESA", ddlempresa)
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Sub BuscaDatos()
        Try
            Dim dtsRes As New DataSet
            Dim strOrden As String = ""


            dtsRes = clsDistribuidor.ObtenTodos(ddlempresa.SelectedValue, 0)

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


    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./altaAgencia.aspx", False)
    End Sub

    Protected Sub grvConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand

        If e.CommandName = "catDisId" Then
            Session("Nivel") += 1
            Dim path As String = "./manejaAgencia.aspx?idDist=" & e.CommandArgument
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
        End If
    End Sub

    'Protected Sub grvConsulta_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvConsulta.PageIndexChanging
    '    Dim dts As New DataSet
    '    Dim strOrden As String = ""

    '    grvConsulta.PageIndex = e.NewPageIndex
    '    dts = CType(Session("dtsConsulta"), DataSet)

    '    If ViewState("strCampos") <> String.Empty Then
    '        strOrden = ViewState("strCampos") & " " & ViewState("strOrden")
    '    End If

    '    dts.Tables(0).DefaultView.Sort = strOrden.ToUpper ' Se realizo un order al Data Set 
    '    grvConsulta.DataSource = dts.Tables(0).DefaultView
    '    grvConsulta.DataBind()

    'End Sub

    Private Sub ddlempresa_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlempresa.SelectedIndexChanged
        BuscaDatos()
    End Sub

End Class
