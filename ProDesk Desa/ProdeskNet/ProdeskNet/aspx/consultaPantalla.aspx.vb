'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports System.IO
Imports System.Data

Public Class consultaPantalla
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"
        Try

            If Not IsPostBack Then
                LlenarEmp()
                LlenarProducto()
                LlenarFlujo()
                BuscarDatos()
            End If

        Catch ex As Exception
        End Try

    End Sub
    Private Sub LlenarEmp()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet
            dtsRed = clsEmpresa.ObtenTodos(1)
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_EMP_NOMBRE", "PDK_ID_EMPRESA", ddlEmpresa)

            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Private Sub LlenarProducto()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet
            dtsRed = clsProductos.ObtenerProductoEmp(ddlEmpresa.SelectedValue, 1)
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_PROD_NOMBRE", "PDK_ID_PRODUCTOS", ddlProducto)
                LlenarFlujo()
            Else
                ddlProducto.Items.Clear()
                ddlFlujo.Items.Clear()

                grvConsulta.DataSource = Nothing
                grvConsulta.DataBind()

            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub
    Private Sub LlenarFlujo()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet
            dtsRed = clsFlujos.ObtenerFlujoProd(ddlProducto.SelectedValue)
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_FLU_NOMBRE", "PDK_ID_FLUJOS", ddlFlujo)
                BuscarDatos()
            Else
                ddlFlujo.Items.Clear()
                grvConsulta.DataSource = Nothing
                grvConsulta.DataBind()

            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub
    Private Sub ddlProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProducto.SelectedIndexChanged
        ddlFlujo.Items.Clear()
        LlenarFlujo()
        grvConsulta.DataSource = Nothing
        grvConsulta.DataBind()
        BuscarDatos()
    End Sub

    Private Sub ddlEmpresa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        LlenarProducto()
    End Sub

    Private Sub BuscarDatos()
        Try
            Dim dtsRes As New DataSet
            Dim strOrden As String = ""

            Dim usuario As Integer = Session("IdUsua")

            dtsRes = clsPantallas.ObtenerPantalla(ddlEmpresa.SelectedValue, ddlProducto.SelectedValue, ddlFlujo.SelectedValue, usuario)

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
        Response.Redirect("./altaPantalla.aspx", False)
    End Sub

    Protected Sub grvConsulta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConsulta.RowCommand

        If e.CommandName = "catPantalla" Then
            Session("Nivel") += 1
            Dim path As String = "./manejaPantalla.aspx?idPantalla=" & e.CommandArgument
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
        ElseIf e.CommandName = "catIdPantalla" Then
            MapPath("..\")
            If e.CommandArgument = "" Then
                Master.MensajeError("El usuario NO tiene los privilegios necesarios.")
                Exit Sub
            End If
            Dim ObjPantalla As New clsPantallas(e.CommandArgument)
            If ObjPantalla.PDK_PANT_STATUS = 2 Then
                Dim dbdateFolio As New DataSet
                Dim dbDabase As New DataSet
                Dim fecha As String
                Dim intSoli As Integer = 0
                fecha = Format(Now(), "yyyy-MM-dd")

                dbDabase = clsPantallaObjeto.ObtenerControles(e.CommandArgument, 3)
                If dbDabase.Tables(0).Rows.Count > 0 AndAlso dbDabase.Tables.Count > 0 Then
                    If dbDabase.Tables(0).Rows(0).Item("FOLIO") <> 0 Then
                        intSoli = dbDabase.Tables(0).Rows(0).Item("FOLIO")
                        dbdateFolio = clsPantallaObjeto.ObtenerControles(e.CommandArgument, 4)
                        If dbdateFolio.Tables.Count > 0 And dbdateFolio.Tables(0).Rows.Count > 0 Then
                            If intSoli <> dbdateFolio.Tables(0).Rows(0).Item("CVE") Then
                                Dim dbDataser1 As New DataSet
                                dbDataser1 = clsPantallas.InsertTabDina("INSERT INTO PDK_TAB_SECCION_CERO(FECHA,CLAVE_USUARIO )VALUES('" & fecha & "'," & Session("IdUsua") & ")")

                            End If
                        End If
                        Session("Regresar") = Nothing
                        Response.Redirect("./Blanco.aspx?idPantalla=" & e.CommandArgument & "&IdFolio=" & intSoli & "")
                    Else
                        Master.MensajeError("No tiene objetos esta pantalla")
                        Exit Sub
                    End If
                End If



            Else
                Master.MensajeError("No se puede mostra la pantalla tiene status INACTIVO ")
            End If




            'Dim fileName As New System.IO.StreamWriter(MapPath("..\aspx\" & ObjPantalla.PDK_PANT_LINK))
            'Dim fileName1 As New System.IO.StreamWriter(MapPath("..\aspx\" & ObjPantalla.PDK_PANT_LINK & ".designer.vb"))
            'Dim fileName2 As New System.IO.StreamWriter(MapPath("..\aspx\" & ObjPantalla.PDK_PANT_LINK & ".vb"))


            'Dim dbDat As New DataSet
            'Dim dbdatSet As New DataSet

            'dbDat = clsPantallas.ObtenerPantaspx(e.CommandArgument)
            'dbdatSet = clsPantallas.ObtenerPantDese(e.CommandArgument)
            'If dbDat.Tables.Count > 0 AndAlso dbDat.Tables(0).Rows.Count > 0 Then
            '    fileName.WriteLine(dbDat.Tables(0).Rows(0).Item("ruta"))
            '    fileName1.WriteLine(dbdatSet.Tables(0).Rows(0).Item("rutas_des"))

            '    dbDat = clsPantallas.ObtenerPantavb(e.CommandArgument)
            '    If dbDat.Tables.Count > 0 AndAlso dbDat.Tables(0).Rows.Count > 0 Then
            '        fileName2.WriteLine(dbDat.Tables(0).Rows(0).Item("rutas_vb"))
            '    End If


            'End If
            'fileName.Close()
            'fileName1.Close()
            'fileName2.Close()
            'Response.Redirect("./" + ObjPantalla.PDK_PANT_LINK)




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
            BuscarDatos()
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

    Protected Sub grvConsulta_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grvConsulta.SelectedIndexChanged
        LlenarFlujo()
    End Sub

    Protected Sub ddlFlujo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFlujo.SelectedIndexChanged
        BuscarDatos()
    End Sub
End Class