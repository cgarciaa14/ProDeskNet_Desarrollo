Imports System.Web.Script.Serialization
Imports ProdeskNet.WCF.clsCustomerResponse

#Region "Trackers"
'BBV-P-423-RQADM-01:MPUESTO:16/05/2017:SELECCION DE CLIENTES
#End Region

<Serializable>
Public Class clsCustomerRequest
    Implements IDisposable
#Region "Variables"
    Private _name As String
    Private _lastName As String
#End Region

#Region "Properties"
    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Public Property lastName() As String
        Get
            Return _lastName
        End Get
        Set(ByVal value As String)
            _lastName = value
        End Set
    End Property
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                _name = Nothing
                _lastName = Nothing
            End If
        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

#Region "Respuesta ListCustomer"
<Serializable>
Public Class clsCustomerResponse

    Public customerList As List(Of clsCustomerItem)
    Public paginationKey As clsPaginationKey

    Public Class clsCustomerItem

        Public person As New cls_Person
        Public knowYourCustomer As New cls_knowYourCustomer
        Public branch As New cls_branch
        Public operationalResponsabilityCenter As New cls_operationalResponsabilityCenter

        Public Class cls_Person
            Public id As String
            Public identityDocument As New List(Of clsIdentityDocument)
            Public [type] As clsType
            Public name As String
            Public lastName As String
            Public mothersLastName As String
            Public companyInformation As clscompanyInformation
            Public bank As clsBank
            Public [status] As clsStatus
            Public relationBank As clsRelationBank
            Public privileged As clsPrivileged
            Public contract As clsContract
            Public segment As clsSegment
            Public user As clsUser
            Public economicData As clsEconomicData
            Public legalAddress As clsLegalAddress
            Public classification As clsClassification
            Public country As clsCountry

            Public Class clsIdentityDocument
                Public [type] As clsDocType
                Public number As String

                Public Class clsDocType
                    Public id As String
                    Public name As String
                End Class

            End Class

            Public Class clsType
                'Empty Class
            End Class

            Public Class clscompanyInformation
                'Empty Class
            End Class

            Public Class clsBank
                'Empty Class
            End Class

            Public Class clsStatus
                Public id As String
            End Class

            Public Class clsRelationBank
                'Empty Class
            End Class

            Public Class clsPrivileged
                'Empty Class
            End Class

            Public Class clsContract
                'Empty Class
            End Class

            Public Class clsSegment
                'Empty Class
            End Class

            Public Class clsUser
                'Empty Class
            End Class

            Public Class clsEconomicData
                Public job As clsJob
                Public ocupationType As clsOcupationType
                Public profession As clsProfession
                Public sectorType As clsSectorType

                Public Class clsJob

                End Class

                Public Class clsOcupationType

                End Class

                Public Class clsProfession

                End Class

                Public Class clsSectorType
                    Public id As String
                End Class

            End Class

            Public Class clsLegalAddress
                Public [type] As clsType
                Public streetType As clsStreetType
                Public streetName As String
                Public streetNumber As String
                Public typeHouse As clsTypeHouse
                Public door As String
                Public typeSettlement As clsTypeSettlement
                Public neighborhood As String
                Public city As String
                Public state As clsState
                Public country As clsCountry
                Public ownerShipType As clsOwnerShipType

                Public Class clsType

                End Class

                Public Class clsStreetType

                End Class

                Public Class clsTypeHouse

                End Class

                Public Class clsTypeSettlement

                End Class

                Public Class clsState

                End Class

                Public Class clsCountry

                End Class

                Public Class clsOwnerShipType

                End Class

            End Class

            Public Class clsClassification
                'Empty Class
            End Class

            Public Class clsCountry
                'Empty Class
            End Class

        End Class

        Public Class cls_knowYourCustomer
            'Empty Class
        End Class

        Public Class cls_branch
            'Empty Class
        End Class

        Public Class cls_operationalResponsabilityCenter
            'Empty Class
        End Class

    End Class

    Public Class clsPaginationKey
        Public paginationKey As String
    End Class

End Class

Public Class clsCustomerManagement

#Region "Variables"
    Dim jsSerializer As New JavaScriptSerializer()
    Dim jsonBODY As String
    Dim userID As String
    Dim restGT As RESTful
    Dim iv_ticket1 As String
    Dim jsonResult As String
    Dim objClsCustomerResponse As New clsCustomerResponse
    Private _hasError As Boolean
    Private _errorMessage As String
#End Region

#Region "Properties"
    Public Property hasWSError() As Boolean
        Get
            Return _hasError
        End Get
        Set(ByVal value As Boolean)
            _hasError = value
        End Set
    End Property

    Public Property WSErrorMessage() As String
        Get
            Return _errorMessage
        End Get
        Set(ByVal value As String)
            _errorMessage = value
        End Set
    End Property

    Private ReadOnly Property uriListCustomers() As String
        Get
            Dim _uriListCustomers As String
            _uriListCustomers = System.Configuration.ConfigurationManager.AppSettings.Item("uriListCustomers")
            _uriListCustomers = _uriListCustomers.Replace("@@ipWebServices_01", System.Configuration.ConfigurationManager.AppSettings.Item("ipWebServices_01"))
            _uriListCustomers = _uriListCustomers.Replace("@@versionListCustomers", System.Configuration.ConfigurationManager.AppSettings.Item("versionListCustomers"))
            Return _uriListCustomers
        End Get
    End Property
#End Region

    Public Function getCustomers(ByVal objClsCustomerRequest As clsCustomerRequest, ByRef errorWebService As Boolean) As clsCustomerResponse
        objClsCustomerResponse = New clsCustomerResponse()
        Try
            restGT = New RESTful()
            jsonBODY = jsSerializer.Serialize(objClsCustomerRequest)
            userID = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            iv_ticket1 = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            restGT.Uri = uriListCustomers
            restGT.buscarHeader("ResponseWarningDescription")
            jsonResult = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)
            errorWebService = restGT.IsError
            objClsCustomerResponse = jsSerializer.Deserialize(Of clsCustomerResponse)(jsonResult)
        Catch ex As Exception
            errorWebService = True
        End Try
        Return objClsCustomerResponse
    End Function

End Class

Public Class clsAddressCustomer_FullJoin
#Region "Simple Variables"
    Private _Identifier As Integer
    Private _Name As String
    Private _LastName As String
    Private _MothersLastName As String
    Private _ClientID As String
    Private _Address As String
    Private _RFC As String
    Private _Birthday As Date
#End Region

#Region "Simple Properties"
    Public Property Identifier() As Integer
        Get
            Return _Identifier
        End Get
        Set(ByVal value As Integer)
            _Identifier = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return _LastName
        End Get
        Set(ByVal value As String)
            _LastName = value
        End Set
    End Property

    Public Property MothersLastName() As String
        Get
            Return _MothersLastName
        End Get
        Set(ByVal value As String)
            _MothersLastName = value
        End Set
    End Property

    Public ReadOnly Property FullName As String
        Get
            Return IIf(_Name <> String.Empty, _Name & " ", "") & _
                IIf(_LastName <> String.Empty, _LastName & " ", "") & _
                IIf(_MothersLastName <> String.Empty, _MothersLastName, "")
        End Get
    End Property

    Public Property ClientID() As String
        Get
            Return _ClientID
        End Get
        Set(ByVal value As String)
            _ClientID = value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal value As String)
            _Address = value
        End Set
    End Property

    Public Property RFC() As String
        Get
            Return _RFC
        End Get
        Set(ByVal value As String)
            _RFC = value
        End Set
    End Property

    Public Property Birthday() As Date
        Get
            Return _Birthday
        End Get
        Set(ByVal value As Date)
            _Birthday = value
        End Set
    End Property

#End Region

#Region "Composite Variables"
    Private _clsCustomerItemObj As clsCustomerResponse.clsCustomerItem
    Private _clsAddressDetail As clsCustomerAddressResponse.clsAddressDetail
#End Region

#Region "Composite Properties"
    Public Property clsCustomerItemObj() As clsCustomerResponse.clsCustomerItem
        Get
            Return _clsCustomerItemObj
        End Get
        Set(ByVal value As clsCustomerResponse.clsCustomerItem)
            _clsCustomerItemObj = value

            _Name = value.person.name
            _LastName = value.person.lastName
            _MothersLastName = value.person.mothersLastName
            _ClientID = value.person.id
        End Set
    End Property
    Public Property clsAddressDetail() As clsCustomerAddressResponse.clsAddressDetail
        Get
            Return _clsAddressDetail
        End Get
        Set(ByVal value As clsCustomerAddressResponse.clsAddressDetail)
            _clsAddressDetail = value

            _Address = IIf(value.address.streetName <> String.Empty, value.address.streetName & " ", "") & _
                        IIf(value.address.streetNumber <> String.Empty, "No. " & value.address.streetNumber & " ", "") & _
                        IIf(value.address.neighborhood <> String.Empty, value.address.neighborhood & " ", "") & _
                        IIf(value.address.zipCode <> String.Empty, " C.P. " & value.address.zipCode & " ", "") & _
                        IIf(value.address.state.name <> String.Empty, value.address.state.name & " ", "") & _
                        IIf(value.address.country.name <> String.Empty, value.address.country.name, "")
        End Set
    End Property
#End Region

End Class

#End Region
