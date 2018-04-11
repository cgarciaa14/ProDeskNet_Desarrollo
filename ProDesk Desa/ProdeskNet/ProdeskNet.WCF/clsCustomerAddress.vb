Imports System.Web.Script.Serialization
Imports ProdeskNet.BD

#Region "Trackers"
'BBV-P-423-RQADM-01:MPUESTO:16/05/2017:SELECCION DE CLIENTES
#End Region
<Serializable>
Public Class clsCustomerAddressResponse
    Private _customer As clsCustomerAd
    Private _ineValidationStatus As String
    Private _adressDetail As List(Of clsAddressDetail)


    Public Property customer() As clsCustomerAd
        Get
            Return _customer
        End Get
        Set(ByVal value As clsCustomerAd)
            _customer = value
        End Set
    End Property

    Public Property ineValidationStatus() As String
        Get
            Return _ineValidationStatus
        End Get
        Set(ByVal value As String)
            _ineValidationStatus = value
        End Set
    End Property

    Public Property adressDetail() As List(Of clsAddressDetail)
        Get
            Return _adressDetail
        End Get
        Set(ByVal value As List(Of clsAddressDetail))
            _adressDetail = value
        End Set
    End Property

    Public Class clsCustomerAd
        Private _id As String


        Public Property id() As String
            Get
                Return _id
            End Get
            Set(ByVal value As String)
                _id = value
            End Set
        End Property

    End Class

    Public Class clsAddressDetail
        Private _telephoneNumbers As List(Of String)
        Private _address As clsAddressAd
        Private _homeRegistrationDate As String
        Private _numberAccountsAssociated As String
        Private addressKeyRelationship As String
        Private dateResidencyHome As String


        Public Property telephoneNumbers() As List(Of String)
            Get
                Return _telephoneNumbers
            End Get
            Set(ByVal value As List(Of String))
                _telephoneNumbers = value
            End Set
        End Property

        Public Property address() As clsAddressAd
            Get
                Return _address
            End Get
            Set(ByVal value As clsAddressAd)
                _address = value
            End Set
        End Property

        Public Property homeRegistrationDate() As String
            Get
                Return _homeRegistrationDate
            End Get
            Set(ByVal value As String)
                _homeRegistrationDate = value
            End Set
        End Property

        Public Property numberAccountsAssociated() As String
            Get
                Return _numberAccountsAssociated
            End Get
            Set(ByVal value As String)
                _numberAccountsAssociated = value
            End Set
        End Property

        Public Property _addressKeyRelationship() As String
            Get
                Return addressKeyRelationship
            End Get
            Set(ByVal value As String)
                addressKeyRelationship = value
            End Set
        End Property

        Public Property _dateResidencyHome() As String
            Get
                Return dateResidencyHome
            End Get
            Set(ByVal value As String)
                dateResidencyHome = value
            End Set
        End Property


        Public Class clsAddressAd
            Private _streetName As String
            Private _streetNumber As String
            Private _typeHouse As clsTypeHouse
            Private _door As String
            Private _neighborhood As String
            Private _zipCode As String
            Private _state As clsState
            Private _country As clsCountry


            Public Property streetName() As String
                Get
                    Return _streetName
                End Get
                Set(ByVal value As String)
                    _streetName = value
                End Set
            End Property

            Public Property streetNumber() As String
                Get
                    Return _streetNumber
                End Get
                Set(ByVal value As String)
                    _streetNumber = value
                End Set
            End Property

            Public Property typeHouse() As clsTypeHouse
                Get
                    Return _typeHouse
                End Get
                Set(ByVal value As clsTypeHouse)
                    _typeHouse = value
                End Set
            End Property

            Public Property door() As String
                Get
                    Return _door
                End Get
                Set(ByVal value As String)
                    _door = value
                End Set
            End Property

            Public Property neighborhood() As String
                Get
                    Return _neighborhood
                End Get
                Set(ByVal value As String)
                    _neighborhood = value
                End Set
            End Property

            Public Property zipCode() As String
                Get
                    Return _zipCode
                End Get
                Set(ByVal value As String)
                    _zipCode = value
                End Set
            End Property

            Public Property state() As clsState
                Get
                    Return _state
                End Get
                Set(ByVal value As clsState)
                    _state = value
                End Set
            End Property

            Public Property country() As clsCountry
                Get
                    Return _country
                End Get
                Set(ByVal value As clsCountry)
                    _country = value
                End Set
            End Property


            Public Class clsTypeHouse
                Private _name As String

                Public Property name() As String
                    Get
                        Return _name
                    End Get
                    Set(ByVal value As String)
                        _name = value
                    End Set
                End Property
            End Class

            Public Class clsState

                Private _id As String
                Private _name As String


                Public Property id() As String
                    Get
                        Return _id
                    End Get
                    Set(ByVal value As String)
                        _id = value
                    End Set
                End Property

                Public Property name() As String
                    Get
                        Return _name
                    End Get
                    Set(ByVal value As String)
                        _name = value
                    End Set
                End Property

            End Class

            Public Class clsCountry

                Private _id As String
                Private _name As String


                Public Property id() As String
                    Get
                        Return _id
                    End Get
                    Set(ByVal value As String)
                        _id = value
                    End Set
                End Property

                Public Property name() As String
                    Get
                        Return _name
                    End Get
                    Set(ByVal value As String)
                        _name = value
                    End Set
                End Property

            End Class
        End Class
    End Class

End Class


Public Class clsCustomerAddressManagement

#Region "Variables"
    Dim jsSerializer As New JavaScriptSerializer()
    Dim jsonBODY As String
    Dim userID As String
    Dim restGT As RESTful
    Dim iv_ticket1 As String
    Dim jsonResult As String
    Dim objClsCustomerAddressResponse As New clsCustomerAddressResponse
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

    Private ReadOnly Property uriCustomersAddresses() As String
        Get
            Dim _uriCustomersAddresses As String
            _uriCustomersAddresses = System.Configuration.ConfigurationManager.AppSettings.Item("uriCustomersAddresses")
            '"http://@@ipWebServices_01/customers/@@versionCustomerAddresses/@@CustomerId/customersAddresses/?operationType=@@paramOperationType"
            _uriCustomersAddresses = _uriCustomersAddresses.Replace("@@ipWebServices_01", System.Configuration.ConfigurationManager.AppSettings.Item("ipWebServices_01"))
            _uriCustomersAddresses = _uriCustomersAddresses.Replace("@@versionCustomerAddresses", System.Configuration.ConfigurationManager.AppSettings.Item("versionCustomerAddresses"))
            _uriCustomersAddresses = _uriCustomersAddresses.Replace("@@paramOperationType", System.Configuration.ConfigurationManager.AppSettings.Item("paramOperationType"))
            Return _uriCustomersAddresses
        End Get
    End Property
#End Region

    Public Function getCustomerAddress(ByVal idCustomer As String, ByRef errorWebService As Boolean) As clsCustomerAddressResponse
        objClsCustomerAddressResponse = New clsCustomerAddressResponse()
        Try
            restGT = New RESTful()
            userID = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            iv_ticket1 = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            restGT.Uri = uriCustomersAddresses
            restGT.Uri = uriCustomersAddresses.Replace("@@CustomerId", idCustomer)
            restGT.buscarHeader("ResponseWarningDescription")
            jsonResult = restGT.ConnectionGet(userID, iv_ticket1, String.Empty)
            errorWebService = restGT.IsError
            objClsCustomerAddressResponse = jsSerializer.Deserialize(Of clsCustomerAddressResponse)(jsonResult)
        Catch ex As Exception
            errorWebService = True
        End Try
        Return objClsCustomerAddressResponse
    End Function
End Class
