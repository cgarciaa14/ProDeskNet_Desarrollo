'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Data

Public Class consultaFlujos
    Inherits System.Web.UI.Page

    Public intProducto As Integer = 0
    Public intEmpresa As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"
        Try

            If Not IsPostBack Then
                LlenaCombo(1)
                LlenaCombo(2, ddlEmpresa.SelectedValue)

                'If intPerfil > 0 Then
                '    ddlPerfil.SelectedIndex = ddlPerfil.Items.IndexOf(ddlPerfil.Items.FindByValue(intPerfil.ToString.Trim))
                'End If
                LlenaDatos()

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub LlenaDatos()
        Dim dtsRes As New DataSet
        Dim objFlujos As New clsFlujos(0)
        Dim strOrden As String = ""

        Try
            intEmpresa = 0
            intProducto = 0

            intEmpresa = Val(ddlEmpresa.SelectedValue.ToString)
            objFlujos.PDK_ID_PRODUCTOS = Val(ddlProducto.SelectedValue.ToString)
            dtsRes = objFlujos.ObtenFlujos()

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

    Private Sub LlenaCombo(ByVal intCombo As Integer, Optional ByVal intEmpresa As Integer = 0)
        Dim dsDataset As New DataSet
        Dim i As Integer = 0
        Try
            If intCombo = 1 Then
                dsDataset = clsEmpresa.ObtenTodos(1)
                ddlEmpresa.DataSource = dsDataset
                ddlEmpresa.DataTextField = "PDK_EMP_NOMBRE"
                ddlEmpresa.DataValueField = "PDK_ID_EMPRESA"
                ddlEmpresa.DataBind()

            ElseIf intCombo = 2 Then 'Llena Producto
                dsDataset = clsProductos.ObtenerProductoEmp(intEmpresa, 1)
                ddlProducto.DataSource = dsDataset
                ddlProducto.DataTextField = "PDK_PROD_NOMBRE"
                ddlProducto.DataValueField = "PDK_ID_PRODUCTOS"
                ddlProducto.DataBind()
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub ddlProducto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProducto.SelectedIndexChanged
        Try
            LlenaDatos()
        Catch ex As Exception
            grvConsulta = Nothing
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./altaFlujos.aspx", False)
    End Sub


    Protected Sub grvConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand

        If e.CommandName = "catIdFlujos" Then
            Session("Nivel") += 1
            Dim path As String = "./manejaFlujos.aspx?idFlujo=" & e.CommandArgument
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

            LlenaDatos()

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

    Private Sub ddlEmpresa_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        Try
            LlenaCombo(2, ddlEmpresa.SelectedValue)
            LlenaDatos()
        Catch ex As Exception
            grvConsulta = Nothing
        End Try
    End Sub
End Class