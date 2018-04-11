Imports System.Data
Imports ProdeskNet.Seguridad

'BUG-PD-70:MAPH:29/08/2017:SEGUNDA VERSION DEL PANEL DE SEGUIMIENTO
'RQADM2-01:MPUESTO:11/09/2017:Mejoras de Panel de Seguimiento
'BUG-PD-226:MPUESTO:04/11/2017:Correcciones de Panel de Seguimiento
'BUG-PD-241:MPUESTO:18/10/2017:Mejoras para mostrar historial de solicitudes de acuerdo a nuevo campo de perfil
'BUG-PD-318:DJUAREZ:21/12/2017:Se realiza cambio para que cuando se pierda la sesion se redirija a la pantalla de login
'BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit
'BUG-PD-369 GVARGAS 23/02/2018 Correccion panel avoid Ajax Tool Kit Paginar
'BUG-PD-375:MPUESTO:28/02/2018:Omisión de Stored Procedure que revive las solicitudes en tareas automaticas, este proceso se pasa al SP.
Partial Class aspx_consultaPanelSeguimiento
    Inherits System.Web.UI.Page
    Dim _dtsResult As DataSet
    Dim _clsManejaBD As New ProdeskNet.BD.clsManejaBD
    Dim objCombo As New clsParametros


    Public Property idUsuario() As String
        Get
            If Session("IdUsua") = Nothing Then
                Return String.Empty
            Else
                Return Session("IdUsua").ToString()
            End If
        End Get
        Set(value As String)
            Session("IdUsua") = value
        End Set
    End Property

    Public Property Paginator() As Integer
        Get
            Return IIf(Convert.ToInt32(Session("Paginator")) <= 0, 1, Convert.ToInt32(Session("Paginator")))
        End Get
        Set(value As Integer)
            Session("Paginator") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadOptions()
            LoadParams()
        End If
        LoadPanel()
    End Sub

    Private Sub LoadPanel()
        If hdnPostbackSearch.Value = "Busqueda" Then
            ddlPaginador.SelectedValue = 1
        End If

        _clsManejaBD = New ProdeskNet.BD.clsManejaBD()
        _clsManejaBD.AgregaParametro("@ID_EMPRESA", ProdeskNet.BD.TipoDato.Entero, ddlCompany.SelectedValue, False)
        _clsManejaBD.AgregaParametro("@ID_PRODUCTO", ProdeskNet.BD.TipoDato.Entero, ddlProduct.SelectedValue, False)
        _clsManejaBD.AgregaParametro("@ID_TIPO_PERSONA", ProdeskNet.BD.TipoDato.Entero, ddlLegalPersonality.SelectedValue, False)
        _clsManejaBD.AgregaParametro("@ID_USUARIO", ProdeskNet.BD.TipoDato.Entero, idUsuario, False)
        If hdnSearchRequestNumber.Value.ToString() <> String.Empty Then
            _clsManejaBD.AgregaParametro("@NO_SOLICITUD", ProdeskNet.BD.TipoDato.Entero, hdnSearchRequestNumber.Value.ToString(), False)
        End If
        If hdnSearchClient.Value.ToString() <> String.Empty Then
            _clsManejaBD.AgregaParametro("@NOMBRE_CLIENTE", ProdeskNet.BD.TipoDato.Cadena, hdnSearchClient.Value.ToString(), False)
        End If
        If hdnSearchInitialDate.Value.ToString() <> String.Empty Then
            _clsManejaBD.AgregaParametro("@FECHA_INICIO", ProdeskNet.BD.TipoDato.Fecha, hdnSearchInitialDate.Value.ToString(), False)
        End If
        If hdnSearchFinalDate.Value.ToString() <> String.Empty Then
            _clsManejaBD.AgregaParametro("@FECHA_FIN", ProdeskNet.BD.TipoDato.Fecha, hdnSearchFinalDate.Value.ToString(), False)
        End If
        If hdnSearchDealer.Value.ToString() <> String.Empty Then
            _clsManejaBD.AgregaParametro("@DISTRIBUIDOR", ProdeskNet.BD.TipoDato.Cadena, hdnSearchDealer.Value.ToString(), False)
        End If
        If hdnSearchProdeskUser.Value.ToString() <> String.Empty Then
            _clsManejaBD.AgregaParametro("@USUARIO_ASIGNADO", ProdeskNet.BD.TipoDato.Cadena, hdnSearchProdeskUser.Value.ToString(), False)
        End If
        If (ddlPaginador.Items.Count > 0) Then
            _clsManejaBD.AgregaParametro("@PAGINADOR", ProdeskNet.BD.TipoDato.Entero, ddlPaginador.SelectedValue, False)
        End If

        repSolicitudes.DataSource = Nothing
        repSolicitudes.DataBind()

        _dtsResult = _clsManejaBD.EjecutaStoredProcedure("get_panelControl")

        If Not _dtsResult Is Nothing AndAlso _dtsResult.Tables.Count > 0 Then
            repSolicitudes.DataSource = _dtsResult.Tables(0)
            repSolicitudes.DataBind()

            If Not IsPostBack Or _dtsResult.Tables(1).Rows.Count <> ddlPaginador.Items.Count Then
                Dim _dataSet As New DataSet()
                _dataSet.Tables.Add(_dtsResult.Tables(1).Copy())
                objCombo.LlenaCombos(_dataSet, "ID_PAGINATOR", "ID_PAGINATOR", ddlPaginador, False, True)
            End If
        End If

    End Sub

    Private Sub LoadOptions()
        'dropdowns en cascada EMPRESA -> PRODUCTO -> PERSONALIDAD JURÍDICA
        _dtsResult = New DataSet()
        _clsManejaBD = New ProdeskNet.BD.clsManejaBD()
        _dtsResult = _clsManejaBD.EjecutaStoredProcedure("get_EmpresasPanelControl")
        If Not _dtsResult Is Nothing AndAlso _dtsResult.Tables.Count > 0 AndAlso _dtsResult.Tables(0).Rows.Count > 0 Then
            objCombo.LlenaCombos(_dtsResult, "PDK_EMP_NOMBRE", "PDK_ID_EMPRESA", ddlCompany, False, True)
            ddlCompany.SelectedIndex = 0

            objCombo.LlenaCombos(_dtsResult, "PDK_EMP_NOMBRE", "PDK_ID_EMPRESA", ddlAddCompany, True, True)
            ddlAddProduct.Items.Clear()
            ddlAddProduct.Items.Add("0")
            ddlAddLegalPersonality.Items.Clear()
            _dtsResult = New DataSet()
            _clsManejaBD = New ProdeskNet.BD.clsManejaBD()
            _clsManejaBD.AgregaParametro("@ID_EMPRESA", ProdeskNet.BD.TipoDato.Entero, ddlCompany.SelectedValue)
            _dtsResult = _clsManejaBD.EjecutaStoredProcedure("get_ProductosPanelControl")
            If Not _dtsResult Is Nothing AndAlso _dtsResult.Tables.Count > 0 AndAlso _dtsResult.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(_dtsResult, "PDK_PROD_NOMBRE", "PDK_ID_PRODUCTOS", ddlProduct, False, True)
                ddlProduct.SelectedIndex = 0

                _dtsResult = New DataSet()
                _clsManejaBD = New ProdeskNet.BD.clsManejaBD()
                _clsManejaBD.AgregaParametro("@ID_EMPRESA", ProdeskNet.BD.TipoDato.Entero, ddlCompany.SelectedValue)
                _clsManejaBD.AgregaParametro("@ID_PRODUCTO", ProdeskNet.BD.TipoDato.Entero, ddlProduct.SelectedValue)
                _dtsResult = _clsManejaBD.EjecutaStoredProcedure("get_PersonalidadJuridicaPanelControl")
                If Not _dtsResult Is Nothing AndAlso _dtsResult.Tables.Count > 0 AndAlso _dtsResult.Tables(0).Rows.Count > 0 Then
                    objCombo.LlenaCombos(_dtsResult, "PDK_PER_NOMBRE", "PDK_ID_PER_JURIDICA", ddlLegalPersonality, False, True)
                    ddlLegalPersonality.SelectedIndex = 0

                    objCombo.LlenaCombos(_dtsResult, "PDK_PER_NOMBRE", "PDK_ID_PER_JURIDICA", ddlAddLegalPersonality, True, True)
                    ddlAddLegalPersonality.SelectedIndex = 0
                End If
            End If
        End If
        _dtsResult = New DataSet()
        _clsManejaBD = New ProdeskNet.BD.clsManejaBD()
        _clsManejaBD.AgregaParametro("@ID_USUARIO", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(Session("IdUsua")))
        _dtsResult = _clsManejaBD.EjecutaStoredProcedure("get_AgenciasPanelControl")
        If Not _dtsResult Is Nothing AndAlso _dtsResult.Tables.Count > 0 AndAlso _dtsResult.Tables(0).Rows.Count > 0 Then
            objCombo.LlenaCombos(_dtsResult, "PDK_DIST_NOMBRE_COMPUESTO", "PDK_ID_DISTRIBUIDOR", ddlAddDealer, False, True)
            ddlAddDealer.Items.Insert(0, "0")
            ddlAddDealer.SelectedIndex = 0
        End If


    End Sub

    Public Function CutText(ByVal str As Object) As String
        Dim text = CType(str, String)
        If text.Length > 20 Then
            text = text.Substring(0, 20) + "..."
        End If
        Return text
    End Function

    Public Function GetStyle(ByVal colorNumber As Object) As String
        Dim hexaColor As String = "style='background:"
        Select Case colorNumber.ToString()
            Case "1" 'SECCION EN PROCESO
                hexaColor += "#B5E5F9 !important' class='link'"
            Case "2" 'SECCION TERMINADA
                hexaColor += "#52BCEC !important' class='link'"
            Case "3" 'PROCESO ACTIVADO
                hexaColor += "#006EC1 !important' class='link'"
            Case "4" 'PROCESO CANCELADO
                hexaColor += "#F6891E !important' class='link'"
            Case Else
                hexaColor += "#FFFFFF !important' "
        End Select
        hexaColor += ""
        Return hexaColor
    End Function

    Public Function GetEvent(ByVal taskStatus As Object, ByVal taskNumber As Object, ByVal colNumber As String) As String
        Dim strEvent As String = "onmousedown='"
        Select Case Convert.ToInt32(taskStatus)
            Case 1, 2, 3, 4
                strEvent += "btnFindTasksClient_click(" & taskNumber.ToString() & "," & colNumber & ")'"
            Case Else
                strEvent = ""
        End Select
        strEvent += ""
        Return strEvent
    End Function

    Public Function GetImage(ByVal taskStatus As Object) As String
        Dim imgSource As String = "src='../App_Themes/Imagenes/"
        Dim intStatus As Integer
        intStatus = Convert.ToInt32(taskStatus)
        Select Case intStatus
            Case 40
                imgSource += "process.png'"
            Case 41, 118
                imgSource += "ok.png'"
            Case 42
                imgSource += "cancel.png'"
        End Select
        Return imgSource
    End Function

    Public Function GetImageEvent(ByVal taskNumber As Object, ByVal taskLink As Object, ByVal taskStatus As Object, screenNumber As Object) As String
        Dim imgEvent As String
        imgEvent = "window.location.href = ""./" & taskLink.ToString().Replace("PANT_", "")
        imgEvent += "?idPantalla=" & screenNumber.ToString()
        imgEvent += "&pantalla=" & screenNumber.ToString()
        imgEvent += "&idFolio=" & taskNumber.ToString()
        imgEvent += "&sol=" & taskNumber.ToString()
        imgEvent += "&NoSol=" & taskNumber.ToString()
        imgEvent += "&solicitud=" & taskNumber.ToString()
        imgEvent += "&Enable=" & IIf(Convert.ToInt32(taskStatus) = 40, "0", "1")
        imgEvent += "&usuario=" & idUsuario
        imgEvent += "&usu=" & idUsuario & """"
        Return imgEvent
    End Function

    Protected Sub btnFindTasks_Click(sender As Object, e As EventArgs)
        Dim taskValue As Integer
        Dim colNumberValue As Integer

        taskValue = Convert.ToInt32(hdnTask.Value)
        colNumberValue = Convert.ToInt32(hdnColNumber.Value)

        'BUG-PD-226:MPUESTO:11/10/2017:Implementación del regreso de tareas
        _dtsResult = New DataSet()
        _clsManejaBD = New ProdeskNet.BD.clsManejaBD()
        _clsManejaBD.AgregaParametro("@PDK_ID_SOLICITUD", ProdeskNet.BD.TipoDato.Entero, taskValue, False)
        '_dtsResult = _clsManejaBD.EjecutaStoredProcedure("update_Task_Blocked_SP")

        _dtsResult = New DataSet()
        _clsManejaBD = New ProdeskNet.BD.clsManejaBD()
        _clsManejaBD.AgregaParametro("@NO_SOLICITUD", ProdeskNet.BD.TipoDato.Entero, taskValue, False)
        _clsManejaBD.AgregaParametro("@COL_NUMBER", ProdeskNet.BD.TipoDato.Entero, colNumberValue, False)
        _clsManejaBD.AgregaParametro("@ID_USUARIO", ProdeskNet.BD.TipoDato.Entero, idUsuario, False)
        _dtsResult = _clsManejaBD.EjecutaStoredProcedure("get_PanelTareas")
        repTareas.DataSource = _dtsResult.Tables(0)
        repTareas.DataBind()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "MuestraTareas", "ShowTasks();", True)
    End Sub

    Protected Sub btnHideModals_Click(sender As Object, e As EventArgs)
        'mpuTasks.Hide()
        'mpuShowSearch.Hide()
        'mpuAddRequest.Hide()
    End Sub

    Protected Sub btnShowTasks_Click(sender As Object, e As EventArgs)
        'mpuTasks.Show()
    End Sub

    Protected Sub btnClean_Click(sender As Object, e As EventArgs)
        hdnSearchRequestNumber.Value = String.Empty
        hdnSearchClient.Value = String.Empty
        hdnSearchDealer.Value = String.Empty
        hdnSearchProdeskUser.Value = String.Empty
        hdnSearchInitialDate.Value = String.Empty
        hdnSearchFinalDate.Value = String.Empty
        ddlPaginador.Items.Clear()
        LoadPanel()
    End Sub

    Private Sub LoadParams()
        If Not Request("NoSolicitud") Is Nothing Then
            hdnSearchRequestNumber.Value = Convert.ToInt32(Request("NoSolicitud"))
        End If
        If Not Request("Empresa") Is Nothing Then
            ddlCompany.SelectedValue = Convert.ToInt32(Request("Empresa"))
        End If
        If Not Request("Producto") Is Nothing Then
            ddlProduct.SelectedValue = Convert.ToInt32(Request("Producto"))
        End If
        If Not Request("Persona") Is Nothing Then
            ddlLegalPersonality.SelectedValue = Convert.ToInt32(Request("Persona"))
        End If
    End Sub

    Protected Sub ddlPaginador_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Paginator = ddlPaginador.SelectedValue
    End Sub

    Protected Sub btnExcecuteAddRequest_Click(sender As Object, e As EventArgs)
        Dim redirectUrl As String = String.Empty
        Dim mensaje As String = String.Empty
        Dim nombreTarea As String = String.Empty

        'mpuAddRequest.Hide()

        _clsManejaBD = New ProdeskNet.BD.clsManejaBD()
        _dtsResult = New DataSet()
        _clsManejaBD.AgregaParametro("@Distribuidor", ProdeskNet.BD.TipoDato.Entero, hdnAddDealer.Value, False)
        _clsManejaBD.AgregaParametro("@Producto", ProdeskNet.BD.TipoDato.Entero, hdnAddProduct.Value, False)
        _clsManejaBD.AgregaParametro("@TPersona", ProdeskNet.BD.TipoDato.Entero, hdnAddLegalPersonality.Value, False)
        _clsManejaBD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, idUsuario, False)
        _clsManejaBD.AgregaParametro("@empresa", ProdeskNet.BD.TipoDato.Entero, hdnAddCompany.Value, False)
        _dtsResult = _clsManejaBD.EjecutaStoredProcedure("sp_insertaSol")
        If Not _dtsResult Is Nothing AndAlso _dtsResult.Tables.Count > 0 AndAlso _dtsResult.Tables(0).Rows.Count > 0 Then
            redirectUrl = _dtsResult.Tables(0).Rows(0)("RUTA").ToString()
            nombreTarea = _dtsResult.Tables(0).Rows(0)("NOMBRE_TAREA").ToString()
        End If
        If redirectUrl <> String.Empty Then
            mensaje = "En breve será redirigido a la pantalla de " & nombreTarea & "."
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreRedireccionamiento", "PopUpLetreroRedirect('" & mensaje & "', '" & redirectUrl & "');", True)
        Else
            mensaje = "Error al crear la solicitud, por favor recargue la página."
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetrero('" & mensaje & "', '" & redirectUrl & "');", True)
        End If
    End Sub

    Protected Sub btnFindRequests_Click(sender As Object, e As EventArgs)
        hdnPostbackSearch.Value = String.Empty
    End Sub

    Protected Sub btnFinNewAJAX_Click(sender As Object, e As EventArgs)
        LoadPanel()
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function addPaginador(ByVal id_Paginador As String) As String
        System.Web.HttpContext.Current.Session.Item("Paginator") = id_Paginador
        Return id_Paginador
    End Function
End Class
