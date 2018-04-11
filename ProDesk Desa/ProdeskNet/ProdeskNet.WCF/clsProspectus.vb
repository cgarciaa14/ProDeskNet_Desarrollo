'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BUG-PD-01: AMR: 22/12/2016: HTTP Error 404 - Not Found al procesar Precalificación.
'BUG-PD-11:GAPM: 16.02.2017 modificacion de amata
'BUG-PD-13  GVARGAS  01/03/2017 Cambio WCF Credenciales

Public Class clsProspectus

    Private _strerror As String = String.Empty
    Private _productCode As String = String.Empty
    Private _referenceNumber As String = String.Empty
    Private _amount As String = String.Empty
    Private _id As String = String.Empty
    Private _name As String = String.Empty
    Private _lastName As String = String.Empty
    Private _mothersLastName As String = String.Empty
    Private _rfc As String = String.Empty
    Private _sex As String = String.Empty
    Private _streetName As String = String.Empty
    Private _neightborthood As String = String.Empty
    Private _city As String = String.Empty
    Private _state As String = String.Empty
    Private _zipCode As String = String.Empty

    Private _bcc As String = String.Empty
    Private _icc As String = String.Empty
    Private _status As String = String.Empty


    Public ReadOnly Property Strerror As String
        Get
            Return _strerror
        End Get
    End Property

    Public Property ProductCode As String
        Get
            Return _productCode
        End Get
        Set(value As String)
            _productCode = value
        End Set
    End Property

    Public Property ReferenceNumber As String
        Get
            Return _referenceNumber
        End Get
        Set(value As String)
            _referenceNumber = value
        End Set
    End Property

    Public Property Amount As String
        Get
            Return _amount
        End Get
        Set(value As String)
            _amount = value
        End Set
    End Property

    Public Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
        End Set
    End Property

    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Public Property LastName As String
        Get
            Return _lastName
        End Get
        Set(value As String)
            _lastName = value
        End Set
    End Property

    Public Property MothersLastName As String
        Get
            Return _mothersLastName
        End Get
        Set(value As String)
            _mothersLastName = value
        End Set
    End Property

    Public Property Rfc As String
        Get
            Return _rfc
        End Get
        Set(value As String)
            _rfc = value
        End Set
    End Property

    Public Property Sex As String
        Get
            Return _sex
        End Get
        Set(value As String)
            _sex = value
        End Set
    End Property

    Public Property StreetName As String
        Get
            Return _streetName
        End Get
        Set(value As String)
            _streetName = value
        End Set
    End Property

    Public Property Neightborthood As String
        Get
            Return _neightborthood
        End Get
        Set(value As String)
            _neightborthood = value
        End Set
    End Property

    Public Property City As String
        Get
            Return _city
        End Get
        Set(value As String)
            _city = value
        End Set
    End Property

    Public Property State As String
        Get
            Return _state
        End Get
        Set(value As String)
            _state = value
        End Set
    End Property

    Public Property ZipCode As String
        Get
            Return _zipCode
        End Get
        Set(value As String)
            _zipCode = value
        End Set
    End Property

    Public Property Bcc As String
        Get
            Return _bcc
        End Get
        Set(value As String)
            _bcc = value
        End Set
    End Property

    Public Property Icc As String
        Get
            Return _icc
        End Get
        Set(value As String)
            _icc = value
        End Set
    End Property

    Public Property Status As String
        Get
            Return _status
        End Get
        Set(value As String)
            _status = value
        End Set
    End Property

    Public Class cbScore
        Public productCode As String = String.Empty
        Public referenceNumber As String = String.Empty
        Public loanToValue As loanToValue = New loanToValue
        Public person As person = New person
    End Class

    Public Class loanToValue
        Public amount As String = String.Empty
    End Class

    Public Class person
        Public id As String = String.Empty
        Public name As String = String.Empty
        Public lastName As String = String.Empty
        Public mothersLastName As String = String.Empty
        Public extendedData As extendedData = New extendedData
        Public addresses As List(Of addresses) = New List(Of addresses)
    End Class

    Public Class extendedData
        Public rfc As String = String.Empty
        Public sex As String = String.Empty
    End Class

    Public Class addresses
        Public streetName As String = String.Empty
        Public neightborthood As String = String.Empty
        Public city As String = String.Empty
        Public state As String = String.Empty
        Public zipCode As String = String.Empty
    End Class

    Public Class msjerr
        Public message As String
        Public status As String
    End Class

    ''Respuesta
    Public Class jResult
        Public creditCapacityIndex As creditCapacityIndex = New creditCapacityIndex()
        Public status As String = String.Empty
        Public value As String = String.Empty
    End Class
    Public Class creditCapacityIndex
        Public value As String
    End Class

    Sub New()
    End Sub

    Public Function GetProspector() As Boolean
        Try
            GetProspector = False
            Dim objpros As New cbScore()
            Dim resp As jResult

            objpros.productCode = _productCode
            objpros.referenceNumber = _referenceNumber
            objpros.loanToValue.amount = _amount

            objpros.person.id = _id
            objpros.person.name = _name
            objpros.person.lastName = _lastName
            objpros.person.mothersLastName = _mothersLastName

            objpros.person.extendedData.rfc = _rfc
            objpros.person.extendedData.sex = _sex

            Dim lsAddress As addresses = New addresses()
            lsAddress.streetName = _streetName
            lsAddress.neightborthood = _neightborthood
            lsAddress.city = _city
            lsAddress.state = _state
            lsAddress.zipCode = _zipCode
            objpros.person.addresses.Add(lsAddress)

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonBODY As String = serializer.Serialize(objpros)

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restGT As RESTful = New RESTful()
            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("url") & System.Configuration.ConfigurationManager.AppSettings.Item("Prospectus")
            'restGT.consumerID = "10000004" '"10000024"

            restGT.buscarHeader("ResponseWarningDescription")

            Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

            Dim str As String = restGT.valorHeader

            Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()


            If restGT.IsError Then
                _strerror = IIf(restGT.StatusHTTP = 500, "Error al consultar Servicio Web: ", "Mensaje del Servicio Web: ") & restGT.MensajeError & ". Estatus:" & restGT.StatusHTTP
                Exit Function
            Else
                resp = srrSerialer.Deserialize(Of jResult)(jsonResult)
                If resp.status = "APROBADO" Then
                    _icc = resp.creditCapacityIndex.value
                    _bcc = resp.value
                    _state = resp.status
                    _status = resp.status
                Else
                    _icc = resp.creditCapacityIndex.value
                    _bcc = resp.value
                    _state = resp.status
                    _status = resp.status
                End If
                GetProspector = True
            End If

        Catch ex As Exception
            Return _strerror = ex.Message
        End Try
    End Function

End Class
