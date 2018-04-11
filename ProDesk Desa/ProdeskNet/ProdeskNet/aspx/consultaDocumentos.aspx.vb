'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.
Imports ProdeskNet.Catalogos
Imports System.Data

Public Class consultaDocumentos
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then
            Session("sBandera") = 0
            Session("Nivel") = 0
            Session("PagPdr" & Session("Nivel")) = Me.Page.Request.Url.PathAndQuery


            LlenaCombo(1)            
            BuscaDatos()
        End If

    End Sub

    Private Sub LlenaCombo(ByVal intCombo As Integer)
        Dim dsDataset As New DataSet

        Try

            dsDataset = clsPersonalidadJuridica.ObtenTodos
            ddlPersonalidadJur.DataSource = dsDataset
            ddlPersonalidadJur.DataTextField = "PDK_PER_NOMBRE"
            ddlPersonalidadJur.DataValueField = "PDK_ID_PER_JURIDICA"
            ddlPersonalidadJur.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BuscaDatos()
        Try
            Dim dtsRes As New DataSet
            Dim strOrden As String = ""
            Dim intPersonalidad As Integer = 0


            intPersonalidad = ddlPersonalidadJur.SelectedValue
            If intPersonalidad > 0 Then

                dtsRes = clsDocumentos.ObtenTodos(intPerJuridica:=intPersonalidad)

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
            Else
                grvConsulta.DataSource = Nothing
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub


    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./altaDocumentos.aspx", False)
    End Sub

    Protected Sub grvConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand

        If e.CommandName = "catRelDoc" Then
            Session("Nivel") += 1
            Dim path As String = "./manejaDocumentos.aspx?IdDoc=" & e.CommandArgument
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

    Private Sub ddlPersonalidadJur_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPersonalidadJur.SelectedIndexChanged

        Try
            BuscaDatos()
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub grvConsulta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvConsulta.RowDataBound
    '    Dim objeto As New Object
    '    objeto = e.Row.Cells(4).FindControl("ddlDescProdoc")
    '    If Not IsNothing(objeto) Then
    '        If objeto.GetType.ToString = "System.Web.UI.WebControls.DropDownList" Then
    '            llenaComboProDoc(objeto)
    '        End If
    '    End If
    'End Sub
End Class