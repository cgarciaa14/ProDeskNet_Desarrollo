'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Seguridad
Imports System.Data

#Region "Trackers"
' BBV-P-423  RQSOL-02  gvargas  15/12/2016 Se modifico el metodo "validaTarea()", permitiendo conocer el perfil del usuario y validadando si es o no perfil "Administrador" para la edicion de tareas
#End Region

Public Class manejaPantalla

    Inherits System.Web.UI.Page
    Dim intPagMax As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("idPantalla")) > 0 Then
                hdnIdRegistro.Value = Request("idPantalla")
                Dim usu As Integer = Session("IdUsua")
                validaTarea(hdnIdRegistro.Value, usu)
                ObtenerPantalla()
                LlenarSeccion()
                BuscarSeccDta()
                Dim flujo As Integer = ddlFlujo.SelectedValue
                dvdocumentos.Attributes.Add("style", "display:none")
                fillgvDocumentos(flujo)
                obtenerOculta()
            End If
        End If
    End Sub

    Private Sub validaTarea(ByVal idpantalla As Integer, ByVal idusu As Integer)
        Dim ds As New DataSet
        Dim ds1 As New DataSet
        Dim bddatos As New ProdeskNet.BD.clsManejaBD
        Dim strSql As String = String.Empty

        'Caqmbio de Query para validar permisos
        'ds = bddatos.EjecutarQuery("select PDK_TAR_PASIGNACION, PDK_TAR_PMODIFICACION from PDK_CAT_TAREAS where PDK_ID_TAREAS = " & idpantalla & " and PDK_CLAVE_USUARIO = " & idusu)
        'strSql = "SELECT X.PDK_ID_PERFIL" & _
        '        "FROM  ( SELECT D.PDK_ID_PERFIL, D.PDK_PER_NOMBRE" & _
        '               "FROM PDK_USUARIO A" & _
        '                  "INNER JOIN PDK_REL_USU_PER C ON A.PDK_ID_USUARIO = C.PDK_ID_USUARIO " & _
        '                 "INNER JOIN PDK_PERFIL D ON D.PDK_ID_PERFIL = C.PDK_ID_PERFIL" & _
        '            "WHERE A.PDK_CLAVE_USUARIO = " & idusu & " ) X"
        ds1 = bddatos.EjecutarQuery("SELECT X.PDK_ID_PERFIL FROM  ( SELECT D.PDK_ID_PERFIL, D.PDK_PER_NOMBRE FROM PDK_USUARIO A INNER JOIN PDK_REL_USU_PER C ON A.PDK_ID_USUARIO = C.PDK_ID_USUARIO INNER JOIN PDK_PERFIL D ON D.PDK_ID_PERFIL = C.PDK_ID_PERFIL WHERE A.PDK_CLAVE_USUARIO = " & idusu & " ) X")

        Dim bandera As Integer
        If ds1.Tables.Count > 0 AndAlso ds1.Tables(0).Rows.Count > 0 Then
            With ds1.Tables(0).Rows(0)
                bandera = Convert.ToInt32(.Item("PDK_ID_PERFIL").ToString)
            End With
        End If

        If bandera = 71 Then
            ds = bddatos.EjecutarQuery("select PDK_TAR_PASIGNACION, PDK_TAR_PMODIFICACION from PDK_CAT_TAREAS where PDK_ID_TAREAS = " & idpantalla)
        Else
            ds = bddatos.EjecutarQuery("select PDK_TAR_PASIGNACION, PDK_TAR_PMODIFICACION from PDK_CAT_TAREAS where PDK_ID_TAREAS = " & idpantalla & " and PDK_CLAVE_USUARIO = " & idusu)
        End If



        If IsDBNull(ds.Tables(0).Rows(0)(0)) Then
            Master.MensajeError("No tiene permisos para ejecutar esta tarea.")

        ElseIf ds.Tables(0).Rows(0)(0) = False Then
            btnGuardar.Enabled = False
            btnGuardar1.Enabled = False
            btnDocumentos.Enabled = False

        End If

    End Sub
    Private Sub obtenerOculta()
        If ddlDocumento.SelectedValue = 26 Or ddlDocumento.SelectedValue = 68 Then
            dvdocumentos.Attributes.Add("style", "display:block")
            dvidpantalla.Attributes.Add("style", "display:none")
        Else
            dvdocumentos.Attributes.Add("style", "display:none")
            dvidpantalla.Attributes.Add("style", "display:block")
        End If
    End Sub

    Private Sub fillgvDocumentos(flujo)
        Dim BDatos As New ProdeskNet.BD.clsManejaBD
        Dim dsTP As New DataSet
        Dim dsDocumentos As New DataSet
        Dim i As Integer
        Dim res As Boolean

        gvDocumentos.DataSource = Nothing
        gvDocumentos.DataBind()


        dsTP = BDatos.EjecutarQuery("select PDK_PER_NOMBRE from PDK_CAT_FLUJOS f inner join PDK_CAT_PER_JURIDICA pj on f.PDK_ID_PER_JURIDICA = pj.PDK_ID_PER_JURIDICA where PDK_ID_FLUJOS = " & flujo & ";")
        dsDocumentos = BDatos.EjecutarQuery("select cd.PDK_ID_DOCUMENTOS, PDK_DOC_NOMBRE,isnull(p.PDK_PAR_SIS_PARAMETRO,'VACIO') as PDK_PAR_SIS_PARAMETRO, ISNULL(p.PDK_ID_PARAMETROS_SISTEMA,109) AS PDK_ID_PARAMETROS_SISTEMA from PDK_CAT_DOCUMENTOS cd left outer join PDK_REL_PAN_DOC doc on cd.PDK_ID_DOCUMENTOS=doc.PDK_ID_DOCUMENTOS and PDK_ID_PANTALLAS = " & Request("idPantalla") & " left outer join PDK_REL_DOC_PER_JUR rcdpf on cd.PDK_ID_DOCUMENTOS = rcdpf.PDK_ID_DOCUMENTOS left join PDK_PARAMETROS_SISTEMA p on p.PDK_ID_PARAMETROS_SISTEMA=doc.PDK_REL_ACT_OBLIGATORIO and PDK_PAR_SIS_ID_PADRE=106 inner join PDK_CAT_FLUJOS f on rcdpf.PDK_ID_PER_JURIDICA = f.PDK_ID_PER_JURIDICA where PDK_ID_FLUJOS = " & flujo)
        'dsDocumentos = BDatos.EjecutarQuery("create table #tem (PDK_ID_DOCUMENTOS INT,PDK_DOC_NOMBRE VARCHAR(50),PDK_DOC_ACTIVO VARCHAR(10)) ; INSERT INTO #tem(PDK_ID_DOCUMENTOS ,PDK_DOC_NOMBRE ,PDK_DOC_ACTIVO) select cd.PDK_ID_DOCUMENTOS, PDK_DOC_NOMBRE,PDK_DOC_ACTIVO from PDK_CAT_DOCUMENTOS cd inner join PDK_REL_DOC_PER_JUR rcdpf on cd.PDK_ID_DOCUMENTOS = rcdpf.PDK_ID_DOCUMENTOS inner join PDK_CAT_FLUJOS f on rcdpf.PDK_ID_PER_JURIDICA = f.PDK_ID_PER_JURIDICA where PDK_ID_FLUJOS = " & flujo & "; update #tem set PDK_DOC_ACTIVO='false' ; SELECT * FROM #tem; DROP TABLE #tem")

        lblTpersona.Text = dsTP.Tables(0).Rows(0)(0).ToString

        gvDocumentos.DataSource = dsDocumentos.Tables(0)
        gvDocumentos.DataBind()

        'For intRegis = 0 To gvDocumentos.Rows.Count - 1
        '    Dim check As Object = CType(gvDocumentos.Rows(intRegis).FindControl("chkSeccDta"), CheckBox)
        '    res = dsDocumentos.Tables(0).Rows(intRegis)(3)
        '    check.Checked = res

        'Next

    End Sub
    Private Sub ObtenerDtaSeccion(ByVal intCve As Integer)
        Dim datReg As New DataSet
        Dim strOrden As String = ""

        Try
            grvObjetoPant.DataSource = Nothing
            grvObjetoPant.DataBind()

            datReg = clsPantallaObjeto.ObtenObjPan(intCve)

            If datReg.Tables.Count > 0 AndAlso datReg.Tables(0).Rows.Count > 0 Then
                Session("dtsConsulta") = Nothing
                grvObjetoPant.PageIndex = 0
                grvObjetoPant.DataSource = Nothing

                If ViewState("strCampos") <> String.Empty Then
                    strOrden = ViewState("strCampos") & " " & ViewState("strOrden")
                End If

                If datReg.Tables.Count > 0 Then
                    If datReg.Tables(0).Rows.Count > 0 Then
                        datReg.Tables(0).DefaultView.Sort = strOrden.ToUpper ' Se realizo un order al Data Set 
                        Session("dtsConsulta") = datReg
                        grvObjetoPant.DataSource = datReg.Tables(0).DefaultView ' Se agrego el DefaultView para que se mostrara ordenado
                    Else
                        Master.MensajeError("No se encontró información con los parámetros proporcionados")
                    End If
                Else
                    Master.MensajeError("No se encontró información para los parámetros proporcionados")
                End If
                grvObjetoPant.DataBind()

            End If


        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub


    Private Sub ObtenerPantalla()
        Dim intId As Integer = Val(hdnIdRegistro.Value)
        Dim bdatReg As New DataSet

        Try
            bdatReg = clsPantallas.ObteInfoPantalla(intId)

            If bdatReg.Tables.Count > 0 AndAlso bdatReg.Tables(0).Rows.Count > 0 Then
                With bdatReg.Tables(0).Rows(0)
                    lblPantalla.Text = .Item("PDK_ID_PANTALLAS").ToString
                    txtNombre.Text = .Item("PDK_PANT_NOMBRE").ToString.Trim
                    txtaspx.Text = .Item("PDK_PANT_LINK").ToString.Trim
                    chkStatus.Checked = IIf(.Item("PDK_PANT_STATUS") = 2, True, False)
                    txtOrden.Text = .Item("PDK_PANT_ORDEN").ToString.Trim
                    chkMostrar.Checked = IIf(.Item("PDK_PANT_MOSTRAR") = 2, True, False)
                    If .Item("PDK_ID_EMPRESA") > 0 Then
                        LlenarEmp()
                        ddlEmpresa.SelectedIndex = ddlEmpresa.Items.IndexOf(ddlEmpresa.Items.FindByValue(.Item("PDK_ID_EMPRESA")))

                    End If
                    If .Item("PDK_ID_PRODUCTOS") > 0 Then
                        LlenarProducto()
                        ddlProducto.SelectedIndex = ddlProducto.Items.IndexOf(ddlProducto.Items.FindByValue(.Item("PDK_ID_PRODUCTOS")))

                    End If
                    If .Item("PDK_ID_FLUJOS") > 0 Then
                        LlenarFlujo()
                        ddlFlujo.SelectedIndex = ddlFlujo.Items.IndexOf(ddlFlujo.Items.FindByValue(.Item("PDK_ID_FLUJOS")))

                    End If
                    If .Item("PDK_ID_PROCESOS") > 0 Then
                        Llenarproceso()
                        ddlProceso.SelectedIndex = ddlProceso.Items.IndexOf(ddlProceso.Items.FindByValue(.Item("PDK_ID_PROCESOS")))

                    End If
                    If .Item("PDK_ID_TAREAS") > 0 Then
                        LlenarTarea()
                        ddlTarea.SelectedIndex = ddlTarea.Items.IndexOf(ddlTarea.Items.FindByValue(.Item("PDK_ID_TAREAS")))

                    End If
                    If .Item("PDK_PANT_DOCUMENTOS") > 0 Then
                        LlenarDocumento()
                        ddlDocumento.SelectedIndex = ddlDocumento.Items.IndexOf(ddlDocumento.Items.FindByValue(.Item("PDK_PANT_DOCUMENTOS")))


                    End If

                End With
        ObtenerDtaSeccion(intId)
            End If


        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
    Private Sub LlenarDocumento()
        Dim objCombo As New clsParametros
        Dim dbtsRe As New DataSet

        Try
            dbtsRe = objCombo.ObtenerParametro(24)
            If dbtsRe.Tables.Count > 0 AndAlso dbtsRe.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dbtsRe, "PDK_PAR_SIS_PARAMETRO", "PDK_ID_PARAMETROS_SISTEMA", ddlDocumento)

            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
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
    Private Sub Llenarproceso()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet
            dtsRed = clsProcesos.ObtenTodos(ddlFlujo.SelectedValue, 1)
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_PROC_NOMBRE", "PDK_ID_PROCESOS", ddlProceso)
                LlenarTarea()
            Else
                ddlProceso.Items.Clear()
                ddlTarea.Items.Clear()
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub
    Private Sub LlenarTarea()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet
            dtsRed = clsTareas.ObtenTodos(ddlProceso.SelectedValue, 0, ddlFlujo.SelectedValue)
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_TAR_NOMBRE", "PDK_ID_TAREAS", ddlTarea)
            Else
                ddlTarea.Items.Clear()
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub
    Private Sub LlenarSeccion()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet
            dtsRed = clsSeccion.obtenerSeccStatus
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_SEC_NOMBRE_TABLA", "PDK_ID_SECCION", ddlSeccion)
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
                ddlProceso.Items.Clear()
                ddlTarea.Items.Clear()
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
                Llenarproceso()
            Else
                ddlFlujo.Items.Clear()
                ddlProceso.Items.Clear()
                ddlTarea.Items.Clear()
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub
    Private Sub ddlProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProducto.SelectedIndexChanged
        ddlFlujo.Items.Clear()
        ddlProceso.Items.Clear()
        ddlTarea.Items.Clear()
        LlenarFlujo()
        Llenarproceso()
        LlenarTarea()
    End Sub

    Private Sub ddlEmpresa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEmpresa.SelectedIndexChanged
        ddlProducto.Items.Clear()
        LlenarProducto()
    End Sub

    Private Sub ddlFlujo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFlujo.SelectedIndexChanged
        ddlProceso.Items.Clear()
        ddlTarea.Items.Clear()
        Llenarproceso()
        LlenarTarea()
    End Sub

    Private Sub ddlProceso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProceso.SelectedIndexChanged
        ddlTarea.Items.Clear()
        LlenarTarea()
    End Sub

    Private Sub ddlSeccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSeccion.SelectedIndexChanged
        BuscarSeccDta()
    End Sub
    Private Sub BuscarSeccDta()
        Try
            Dim dtsRes As New DataSet
            Dim strOrden As String = ""

            grdSeccDat.DataSource = Nothing
            grdSeccDat.DataBind()


            dtsRes = clsSeccionDato.ObtenerSeccDta(ddlSeccion.SelectedValue)
            grdSeccDat.DataSource = dtsRes.Tables(0)
            grdSeccDat.DataBind()

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub


    Private Sub ordenarTodos()
        Dim etiqueta As Label
        Dim fila As GridViewRow
        Dim max As Integer = 1

        If grdSeccDat Is Nothing OrElse grdSeccDat.Rows.Count = 0 Then Exit Sub

        'Ordena los registros que se seleccionarán de 1 en adelante en forma ascendente


        For Each fila In grdSeccDat.Rows
            If CType(fila.FindControl("chkSeccDta"), CheckBox).Checked Then
                etiqueta = CType(fila.FindControl("lblOrden"), Label)
                etiqueta.Text = max
                max += 1
            End If
        Next
    End Sub
    Protected Sub chkTodoMostrar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fila As GridViewRow
        For Each fila In grdSeccDat.Rows
            If CType(grdSeccDat.HeaderRow.FindControl("chkTodoMostrar"), CheckBox).Checked = True Then
                CType(fila.FindControl("chkMostrar"), CheckBox).Checked = True
            Else
                CType(fila.FindControl("chkMostrar"), CheckBox).Checked = False
            End If
        Next

    End Sub
    Private Sub revisarMostrar()
        Dim fila As GridViewRow
        Dim todos As Boolean = True

        If grdSeccDat Is Nothing OrElse grdSeccDat.Rows.Count = 0 Then Exit Sub

        For Each fila In grdSeccDat.Rows
            If Not CType(fila.FindControl("chkMostrar"), CheckBox).Checked Then
                todos = False
                Exit For
            End If
        Next

        If todos Then
            CType(grdSeccDat.HeaderRow.FindControl("chkTodoMostrar"), CheckBox).Checked = True
        Else
            CType(grdSeccDat.HeaderRow.FindControl("chkTodoMostrar"), CheckBox).Checked = False
        End If
    End Sub
    Protected Sub chkTodos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fila As GridViewRow
        Dim etiqueta As Label
        For Each fila In grdSeccDat.Rows
            If CType(grdSeccDat.HeaderRow.FindControl("chkTodos"), CheckBox).Checked = True Then
                CType(fila.FindControl("chkSeccDta"), CheckBox).Checked = True
            Else
                CType(fila.FindControl("chkSeccDta"), CheckBox).Checked = False
                etiqueta = CType(fila.FindControl("lblOrden"), Label)
                etiqueta.Text = ""
            End If
        Next
        ordenarTodos()
        revisarSeleccion()
    End Sub
    Protected Sub chkSeccDta_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        OrdenarSeccionDta(sender)
    End Sub

    Private Sub OrdenarSeccionDta(ByVal sender As Object)
        Dim check As CheckBox = CType(sender, CheckBox)
        Dim etiqueta As Label
        Dim fila As GridViewRow
        Dim max As Integer = 0

        If grdSeccDat Is Nothing OrElse grdSeccDat.Rows.Count = 0 Then Exit Sub

        If check.Checked Then
            'Obtiene el máximo valor del orden los datos  seleccionados
            For Each fila In grdSeccDat.Rows
                If CType(fila.FindControl("chkSeccDta"), CheckBox).Checked Then
                    etiqueta = CType(fila.FindControl("lblOrden"), Label)
                    If etiqueta.Text.Length > 0 AndAlso CType(etiqueta.Text, Integer) > max Then
                        max = CType(etiqueta.Text, Integer)
                    End If
                End If
            Next
            max += 1
            'Busca la fila que fue seleccionada por el usuario y escribe el orden correspondiente en la etiqueta
            For Each fila In grdSeccDat.Rows
                If CType(fila.FindControl("chkSeccDta"), CheckBox).ClientID = check.ClientID Then
                    etiqueta = CType(fila.FindControl("lblOrden"), Label)
                    etiqueta.Text = max
                    Exit Sub
                End If
            Next
        Else
            '*******************************
            'igonzalez  09 jun 2009  se valida si es el desglose mayor

            'Busca la fila que fue seleccionada para tomar el valor del orden
            For Each fila In grdSeccDat.Rows
                If CType(fila.FindControl("chkSeccDta"), CheckBox).ClientID = check.ClientID Then
                    etiqueta = CType(fila.FindControl("lblOrden"), Label)
                    If etiqueta.Text.Length > 0 Then
                        max = CType(etiqueta.Text, Integer)
                        etiqueta.Text = String.Empty
                    End If
                    Exit For
                End If
            Next

            intPagMax = max

            'Resta 1 al orden de las filas que están marcadas y que son mayores a la fila seleccionada
            For Each fila In grdSeccDat.Rows
                If CType(fila.FindControl("chkSeccDta"), CheckBox).Checked Then
                    etiqueta = CType(fila.FindControl("lblOrden"), Label)
                    If etiqueta.Text.Length > 0 AndAlso CType(etiqueta.Text, Integer) > max Then
                        etiqueta.Text = CType(etiqueta.Text, Integer) - 1

                        If CType(etiqueta.Text, Integer) > intPagMax Then
                            intPagMax = CType(etiqueta.Text, Integer)
                        End If
                    End If
                End If
            Next
            '*******************************
        End If
    End Sub
    Private Sub revisarSeleccion()
        Dim fila As GridViewRow
        Dim todos As Boolean = True

        If grdSeccDat Is Nothing OrElse grdSeccDat.Rows.Count = 0 Then Exit Sub

        For Each fila In grdSeccDat.Rows
            If Not CType(fila.FindControl("chkSeccDta"), CheckBox).Checked Then
                todos = False
                Exit For
            End If
        Next

        If todos Then
            CType(grdSeccDat.HeaderRow.FindControl("chkTodos"), CheckBox).Checked = True
        Else
            CType(grdSeccDat.HeaderRow.FindControl("chkTodos"), CheckBox).Checked = False
        End If
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim path As String = "./consultaPantalla.aspx"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
    End Sub
    Public Function ValidarCampo() As Boolean
        ValidarCampo = False
        If txtNombre.Text.Trim.Length = 0 Then Exit Function
        If txtaspx.Text.Trim.Length = 0 Then Exit Function
        If txtOrden.Text.Trim.Length = 0 Then Exit Function

        ValidarCampo = True

    End Function
    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim objPantalla As New clsPantallas
        Try
            If ValidarCampo() Then

                Dim dbDatset As New DataSet
                dbDatset = clsPantallas.ObtenerValidacion(ddlTarea.SelectedValue, lblPantalla.Text.Trim)
                If dbDatset.Tables(0).Rows.Count > 0 AndAlso dbDatset.Tables.Count > 0 Then
                    Master.MensajeError("Tarea asignada a otra pantalla")
                    Exit Sub
                End If

                objPantalla.PDK_ID_PANTALLAS = lblPantalla.Text.Trim
                objPantalla.PDK_PANT_NOMBRE = txtNombre.Text.Trim.ToUpper
                objPantalla.PDK_PANT_LINK = txtaspx.Text.Trim.ToUpper
                objPantalla.PDK_PANT_ORDEN = txtOrden.Text.Trim
                objPantalla.PDK_PANT_STATUS = IIf(chkStatus.Checked = True, 2, 3)
                objPantalla.PDK_PANT_MODIF = Format(Now(), "yyyy-MM-dd")
                objPantalla.PDK_CLAVE_USUARIO = Session("IdUsua")
                objPantalla.PDK_ID_TAREAS = ddlTarea.SelectedValue
                objPantalla.PDK_PANT_MOSTRAR = IIf(chkMostrar.Checked = True, 2, 3)
                objPantalla.PDK_PANT_DOCUMENTOS = ddlDocumento.SelectedValue
                objPantalla.ActualizaRegistro()


                Master.MensajeError("Información almacenada exitosamente")
                ObtenerDtaSeccion(lblPantalla.Text.Trim)

            Else
                Master.MensajeError("Todos los campos marcados con * son obliga torios")
                Exit Sub

            End If

        Catch ex As Exception
            Master.MensajeError("Error al guardar la información")
        End Try
    End Sub

    Private Sub btnGuardar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar1.Click
        Try
            Dim objSeccObjPant As New clsPantallaObjeto
            Dim intCvePanta As Integer = Val(hdnIdRegistro.Value)
            Dim intRegis As Integer = 0
            Dim intCol As Integer = 0
            Dim strOrde As New Label
            Dim check As New CheckBox


            For intRegis = 0 To grdSeccDat.Rows.Count - 1

                objSeccObjPant.PDK_REL_PANT_OBJ_NOMBRE = Replace((grdSeccDat.Rows(intRegis).Cells(1).Text), "&#209;", "Ñ")
                objSeccObjPant.PDK_REL_PANT_OBJ_TAMANO = grdSeccDat.Rows(intRegis).Cells(4).Text
                strOrde = CType(grdSeccDat.Rows(intRegis).FindControl("lblOrden"), Label)
                If strOrde.Text <> "" Then
                    objSeccObjPant.PDK_REL_PANT_OBJ_ORDEN = strOrde.Text
                Else
                    objSeccObjPant.PDK_REL_PANT_OBJ_ORDEN = 0
                End If
                objSeccObjPant.PDK_REL_PANT_OBJ_MOSTRAR = 1

                objSeccObjPant.PDK_REL_PANT_OBJ_STATUS = IIf(chkStatus.Checked = True, 2, 3)
                objSeccObjPant.PDK_REL_PANT_OBJ_MODIF = Format(Now(), "yyyy-MM-dd")
                objSeccObjPant.PDK_CLAVE_USUARIO = Session("IdUsua")
                'objSeccObjPant.PDK_ID_TIPO_OBJETO = grdSeccDat.Rows(intRegis).Cells(2).Text
                'objSeccObjPant.PDK_ID_SECCION = ddlSeccion.SelectedValue
                objSeccObjPant.PDK_ID_SECCION_DATO = grdSeccDat.Rows(intRegis).Cells(0).Text
                objSeccObjPant.PDK_ID_PANTALLAS = intCvePanta
                objSeccObjPant.ActualizaRegistro()
                objSeccObjPant.PDK_ID_REL_PANTALLA_OBJETO = 0
            Next
            Master.MensajeError("Información almacenada exitosamente")
            ObtenerDtaSeccion(intCvePanta)

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Sub grvObjetoPant_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grvObjetoPant.PageIndexChanging
        Dim dts As New DataSet
        Dim strOrden As String = ""

        grvObjetoPant.PageIndex = e.NewPageIndex
        dts = CType(Session("dtsConsulta"), DataSet)

        If ViewState("strCampos") <> String.Empty Then
            strOrden = ViewState("strCampos") & " " & ViewState("strOrden")
        End If

        dts.Tables(0).DefaultView.Sort = strOrden.ToUpper ' Se realizo un order al Data Set 
        grvObjetoPant.DataSource = dts.Tables(0).DefaultView
        grvObjetoPant.DataBind()
    End Sub

    Private Sub ddlDocumento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDocumento.SelectedIndexChanged
        If ddlDocumento.SelectedValue = 26 Or ddlDocumento.SelectedValue = 68 Then
            dvdocumentos.Attributes.Add("style", "display:block")
            dvidpantalla.Attributes.Add("style", "display:none")
        Else
            dvdocumentos.Attributes.Add("style", "display:none")
            dvidpantalla.Attributes.Add("style", "display:block")
        End If

    End Sub

    Protected Sub btnDocumentos_clkBtnDocumentos(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDocumentos.Click
        Dim BDatos As New ProdeskNet.BD.clsManejaBD
        Dim stat As Integer

        Try

            For intRegis = 0 To gvDocumentos.Rows.Count - 1
                Dim iddocumento As Integer = gvDocumentos.Rows(intRegis).Cells(0).Text
                Dim check As Object = CType(gvDocumentos.Rows(intRegis).FindControl("chkSeccDta"), CheckBox)
                Dim country As Integer = CType(gvDocumentos.Rows(intRegis).FindControl("ddlobliga"), DropDownList).SelectedValue
                'If (check.Checked = True) Then

                If country = 107 Or CInt(country) = 108 Then
                    stat = 1
                Else
                    stat = 0
                End If
                BDatos.EjecutarQuery("if exists (select * from PDK_REL_PAN_DOC where PDK_CLAVE_USUARIO = " & Session("IdUsua") & " and PDK_ID_DOCUMENTOS = " & iddocumento & " and PDK_ID_PANTALLAS = " & Request("idPantalla") & ") begin update PDK_REL_PAN_DOC set PDK_REL_ACT_OBLIGATORIO = " & CInt(country) & ",PDK_REL_ACT_STATUS= " & stat & " where PDK_CLAVE_USUARIO = " & Session("IdUsua") & " and PDK_ID_DOCUMENTOS = " & iddocumento & " and PDK_ID_PANTALLAS = " & Request("idPantalla") & " End else begin insert PDK_REL_PAN_DOC (PDK_REL_ACT_MODIF, PDK_CLAVE_USUARIO, PDK_ID_DOCUMENTOS, PDK_ID_PANTALLAS, PDK_REL_ACT_STATUS,PDK_REL_ACT_OBLIGATORIO) values(GETDATE()," & Session("IdUsua") & ", " & iddocumento & ", " & Request("idPantalla") & ", " & stat & "," & CInt(country) & ") end")

                Master.MensajeError("Información almacenada exitosamente")
            Next
            'Request("idPantalla")
            'Session("IdUsua")

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub

    Protected Sub ButtElimivar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtElimivar.Click
        Try
            Dim eliminar As New clsPantallaObjeto
            Dim intCvePanta As Integer = Val(hdnIdRegistro.Value)
            eliminar.EliminarPantObj(hdnIdRegistro.Value, ddlSeccion.SelectedValue)
            Master.MensajeError("Información elimino exitosamente")
            ObtenerDtaSeccion(intCvePanta)

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Sub gvDocumentos_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDocumentos.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then

            Dim BDatos As New ProdeskNet.BD.clsManejaBD

            Dim ddlobliga As DropDownList = CType(e.Row.FindControl("ddlobliga"), DropDownList)

            ddlobliga.DataSource = BDatos.EjecutarQuery("SELECT PDK_ID_PARAMETROS_SISTEMA,PDK_PAR_SIS_PARAMETRO FROM PDK_PARAMETROS_SISTEMA WHERE PDK_PAR_SIS_ID_PADRE=106")
            ddlobliga.DataTextField = "PDK_PAR_SIS_PARAMETRO"
            ddlobliga.DataValueField = "PDK_ID_PARAMETROS_SISTEMA"
            ddlobliga.DataBind()

            Dim country As String = CType(e.Row.FindControl("lbloblig"), Label).Text

            'ddlobliga.SelectedValue = CInt(country)
            ddlobliga.SelectedIndex = ddlobliga.Items.IndexOf(ddlobliga.Items.FindByValue(country))

        End If


    End Sub
End Class