#Region "Trackers"
'BBV-P-423-RQADM01:MPUESTO:16/05/2017:Seleccion de Clientes
'BUG-PD-69:MPUESTO:01/06/2017:Correcciones Seleccion de Clientes
#End Region

Imports System.Web.Script.Serialization
Imports ProdeskNet.WCF
Imports ProdeskNet.SN
Imports System.Data
Imports ProdeskNet.BD
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos

Partial Class aspx_SeleccionCliente
    Inherits System.Web.UI.Page
#Region "Variables"
    'Objeto para invocar el servicio web listCustomers
    Dim objClsCustomerManagement As clsCustomerManagement
    'Objeto para almacenar la respuesta del servicio web listCustomers
    Dim objCustomerAddressManagement As clsCustomerAddressManagement
    'Objeto para poblar el repeater
    Dim objAddrCustFullJoinList As List(Of clsAddressCustomer_FullJoin)
    'Objeto para obtener los datos de un cliente de la base de datos
    Dim objClsSolicitantes As clsSolicitantes
    'Objeto para almacenar los identificadores de la lista de uniones
    Dim listaSolStr As List(Of String)
    Dim _idSolicitud As Integer
    Dim _dtsResult As DataSet
    Dim _idPantalla As Integer = 113
    Dim _clsManejaBD As clsManejaBD
    Dim _mostrarPantalla As Integer = 0
    Dim _clsSolicitudes As clsSolicitudes
    Dim _clsCatTareas As ProdeskNet.SN.clsCatTareas
    Dim _clsPantallas As ProdeskNet.SN.clsPantallas
    Dim _cliente As New clsDatosCliente
    Dim _solicitud As New clsStatusSolicitud
    Dim _errorWebservice As Boolean
#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        _idSolicitud = Convert.ToInt64(Request.QueryString("sol"))
        _cliente.GetDatosCliente(_idSolicitud)
        _solicitud.getStatusSol(_idSolicitud)
        Me.lblCliente.Text = _cliente.propNombreCompleto
        Me.lblStCredito.Text = _solicitud.PStCredito
        Me.lblStDocumento.Text = _solicitud.PStDocumento
        Me.lblSolicitud.Text = _idSolicitud.ToString()
        _idPantalla = Convert.ToInt32(Val(Request("idPantalla")).ToString())
        _clsSolicitudes = New clsSolicitudes(_idSolicitud)
        _dtsResult = New DataSet()
        _clsManejaBD = New clsManejaBD()
        _dtsResult = _clsManejaBD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & _idPantalla.ToString())
        FindCustomers()
    End Sub

    Protected Sub FindCustomers()
        _errorWebservice = False
        Dim counter As Integer = 0
        objClsSolicitantes = New clsSolicitantes()
        objClsCustomerManagement = New clsCustomerManagement()
        objCustomerAddressManagement = New clsCustomerAddressManagement()
        objClsSolicitantes = objClsSolicitantes.getSolicitante(_idSolicitud)
        objAddrCustFullJoinList = New List(Of clsAddressCustomer_FullJoin)
        If objClsSolicitantes.Count > 0 Then
            For Each clsSolicitanteItem As clsSolicitante In objClsSolicitantes
                Using objClsCustomerRequest As New clsCustomerRequest() With { _
                    .name = clsSolicitanteItem.NAME, _
                    .lastName = clsSolicitanteItem.LAST_NAME
                }
                    Dim objCustomers = objClsCustomerManagement.getCustomers(objClsCustomerRequest, _errorWebservice)
                    If _errorWebservice Then
                        Exit For
                    End If
                    If Not objCustomers Is Nothing Then
                        If Not objCustomers.customerList Is Nothing Then
                            If objCustomers.customerList.Count > 0 Then
                                For Each objCustomerItem As clsCustomerResponse.clsCustomerItem In objCustomers.customerList
                                    Dim objCustomerAddresses = objCustomerAddressManagement.getCustomerAddress(objCustomerItem.person.id, _errorWebservice)
                                    If _errorWebservice Then
                                        Exit For
                                    End If
                                    If Not objCustomerAddresses Is Nothing Then
                                        If Not objCustomerAddresses.adressDetail Is Nothing Then
                                            For Each objAddressDetail As clsCustomerAddressResponse.clsAddressDetail In objCustomerAddresses.adressDetail
                                                objAddrCustFullJoinList.Add(New clsAddressCustomer_FullJoin() With {.clsCustomerItemObj = objCustomerItem, .Birthday = clsSolicitanteItem.BIRTHDAY, .clsAddressDetail = objAddressDetail, .Identifier = counter, .RFC = clsSolicitanteItem.RFC})
                                                counter += 1
                                            Next
                                        End If
                                    End If
                                Next
                            Else
                                ChangeVisibility(False)
                            End If
                        Else
                            ChangeVisibility(False)
                        End If
                    Else
                        ChangeVisibility(False)
                    End If
                    objClsCustomerRequest.Dispose()
                End Using
            Next
        End If
        If Not _errorWebservice Then
            objAddrCustFullJoinList = objAddrCustFullJoinList.Where(Function(x) x.Address <> String.Empty).ToList()
            If objAddrCustFullJoinList.Count > 0 Then
                repCustomerAddress.DataSource = objAddrCustFullJoinList
                repCustomerAddress.DataBind()
                ChangeVisibility(True)
            Else
                ChangeVisibility(False)
                ProcesaTarea(AvanzarTareaA.Rechazo)
            End If
        Else
            ChangeVisibility(False)
            ProcesaTarea(AvanzarTareaA.ManualAnterior)
        End If
    End Sub

    ''' <summary>
    ''' Cambia la visibilidad de los div del repeater para mostrar resultados u ocultarlo y mostrar el mensaje "Sin resultados"
    ''' </summary>
    ''' <param name="divStatus">Visibilidad del grid de resultados</param>
    ''' <remarks></remarks>
    Private Sub ChangeVisibility(ByVal divStatus As Boolean)
        divEmptyTableResult.Visible = Not divStatus
        divTableResult.Visible = divStatus
    End Sub

    Private Sub ProcesaTarea(ByVal SiguienteTarea As AvanzarTareaA)
        Dim withError As Boolean = False
        Select Case SiguienteTarea
            Case AvanzarTareaA.NoRechazo
                If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
                    _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")
                Else
                    _clsSolicitudes.PDK_ID_CAT_RESULTADO = 112
                End If
            Case AvanzarTareaA.Rechazo
                If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
                    _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO")
                Else
                    _clsSolicitudes.PDK_ID_CAT_RESULTADO = 99
                End If
            Case AvanzarTareaA.ManualAnterior
                _clsSolicitudes.PDK_ID_CAT_RESULTADO = ObtenerTareaManualAnterior()
                withError = True
        End Select
        _clsSolicitudes.ValNegocio(1)
        checkRedirect(withError)
    End Sub

    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs)
        listaSolStr = hdnArrayIds.Value.Split(",").ToList()
        For Each strItem As String In listaSolStr
            Dim CustomerItem = objAddrCustFullJoinList.Find(Function(x) x.Identifier = Convert.ToInt32(strItem))
            objClsSolicitantes.insertaDireccionAlter(_idSolicitud, New clsSolicitante() With {
                                                     .CUSTOMER_ID = CustomerItem.ClientID, _
                                                     .NAME = CustomerItem.Name, _
                                                     .LAST_NAME = CustomerItem.LastName, _
                                                     .MOTHERS_LAST_NAME = CustomerItem.MothersLastName, _
                                                    .RFC = CustomerItem.RFC, _
                                                    .BIRTHDAY = CustomerItem.Birthday, _
                                                    .ADDRESS = CustomerItem.Address _
                                                    })
        Next
        ProcesaTarea(AvanzarTareaA.NoRechazo)
    End Sub

    Protected Sub checkRedirect(ByVal withError As Boolean)
        _clsCatTareas = New clsCatTareas()
        _clsPantallas = New ProdeskNet.SN.clsPantallas()
        _dtsResult = _clsCatTareas.SiguienteTarea(Val(Request("Sol")))
        _mostrarPantalla = _clsPantallas.SiguientePantalla(Val(Request("Sol")))
        If _mostrarPantalla = 2 Or withError Then
            Response.Redirect("../aspx/consultaPanelControl.aspx")
        ElseIf _mostrarPantalla = 0 Then
            Response.Redirect("../aspx/" & _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
        End If
    End Sub

    Enum AvanzarTareaA
        Rechazo
        NoRechazo
        ManualAnterior
    End Enum

    Private Function ObtenerTareaManualAnterior() As Integer
        Dim result = 0
        Dim querysin = "SELECT TOP 1 TAREAS.PDK_ID_TAREAS FROM PDK_OPE_SOLICITUD OPE_SOL " & _
                            "INNER JOIN PDK_CAT_TAREAS TAREAS " & _
                                "ON OPE_SOL.PDK_ID_TAREAS = TAREAS.PDK_ID_TAREAS " & _
                            "INNER JOIN PDK_REL_PANTALLA_TAREA REL_PANT_TAR " & _
                                "ON TAREAS.PDK_ID_TAREAS = REL_PANT_TAR.PDK_ID_TAREAS " & _
                            "INNER JOIN PDK_PANTALLAS PANTALLAS " & _
                                "ON REL_PANT_TAR.PDK_ID_PANTALLAS = PANTALLAS.PDK_ID_PANTALLAS " & _
                            "WHERE OPE_SOL.PDK_ID_SOLICITUD = @@IdSolicitud And PANTALLAS.PDK_PANT_MOSTRAR = 2 " & _
                                "ORDER BY 1 DESC"
        querysin = querysin.Replace("@@IdSolicitud", _idSolicitud)
        _dtsResult = _clsManejaBD.EjecutarQuery(querysin)
        If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
            result = Convert.ToInt32(_dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS").ToString())
        End If
        Return result
    End Function

End Class
