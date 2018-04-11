'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.
Imports ProdeskNet.Configurcion
Imports System.Data

Public Class consultaSeccion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then
            Session("sBandera") = 0
            Session("Nivel") = 0
            Session("PagPdr" & Session("Nivel")) = Me.Page.Request.Url.PathAndQuery

            BuscaDatos()
        End If
    End Sub
    Private Sub BuscaDatos()
        Try
            Dim dtsRes As New DataSet
            Dim strOrden As String = ""
            Dim intRen As Integer


            dtsRes = clsSeccion.ObtenTodos()


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

            For intRen = 0 To grvConsulta.Rows.Count - 1
                If grvConsulta.Rows.Item(intRen).Cells(3).Text = 2 Then
                    grvConsulta.Rows.Item(intRen).Cells(5).Enabled = False
                End If
            Next

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Response.Redirect("./altaSeccion.aspx", False)
    End Sub

    Protected Sub grvConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand
        Try
            If e.CommandName = "catIdSeccio" Then
                Session("Nivel") += 1
                Dim path As String = "./manejaSeccion.aspx?catIdSeccio=" & e.CommandArgument
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
            ElseIf e.CommandName = "catidSeccDatos" Then
                Session("Nivel") += 1
                Dim path As String = "./SeccionDatos.aspx?catIdSeccio=" & e.CommandArgument
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
            ElseIf e.CommandName = "catIdSeccTable" Then
                Dim dbRest As New DataSet
                Dim dbjSecc As New clsSeccion(e.CommandArgument)
                dbRest = clsSeccionDato.ObtenTodos(e.CommandArgument)

                If dbRest.Tables.Count > 0 AndAlso dbRest.Tables(0).Rows.Count > 0 Then
                    If dbjSecc.PDK_SEC_CREACION <> 2 Then
                        Dim dbResDa As DataSet = clsSeccionDato.CrearTable(e.CommandArgument, Session("IdUsua"), 1)
                        If dbResDa.Tables.Count > 0 AndAlso dbResDa.Tables(0).Rows.Count > 0 Then
                            Master.MensajeError("Se creo con  éxito")
                            BuscaDatos()
                        Else
                            Master.MensajeError("Error al crear la tabla")

                        End If

                    Else
                        Master.MensajeError("Error ya existe la tabla")

                    End If
                Else
                    Master.MensajeError("No se encontró información con los parámetros proporcionados")

                End If
            ElseIf e.CommandName = "catIdSeccElimina" Then
                Dim dbReDat As New DataSet
                Dim dbjSecc As New clsSeccion(e.CommandArgument)
                Dim dbjSeccDat As New clsSeccionDato
                Dim intBandera As Integer = 0
                Dim intExtObjPant As Integer = clsPantallaObjeto.ObtenerSeccObj(e.CommandArgument)

                If intExtObjPant = 0 Then
                    If dbjSecc.PDK_SEC_CREACION <> 1 Then
                        dbReDat = clsSeccionDato.DropTable(e.CommandArgument, dbjSecc.PDK_SEC_NOMBRE_TABLA, Session("IdUsua"))
                        If dbReDat.Tables.Count > 0 AndAlso dbReDat.Tables(0).Rows.Count > 0 Then
                            Master.MensajeError(dbReDat.Tables(0).Rows(0).Item("@SQL").ToString)
                            BuscaDatos()
                        End If
                    Else
                        Master.MensajeError("No se puede borrar la tabla porque no existe")
                    End If
                Else

                    Master.MensajeError("No se puede borrar la tabla esta en uso")

                End If


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

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
        
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


        For intRen = 0 To grvConsulta.Rows.Count - 1
            If grvConsulta.Rows.Item(intRen).Cells(3).Text = 2 Then
                grvConsulta.Rows.Item(intRen).Cells(5).Enabled = False
            End If
        Next
    End Sub


End Class