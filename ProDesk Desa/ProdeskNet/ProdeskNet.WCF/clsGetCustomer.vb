'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BUG-PD-13  GVARGAS  01/03/2017 Cambio WCF Credenciales

Public Class clsGetCustomer

    Private _strerror As String = String.Empty
    Private _userID As String = String.Empty
    Private _iv_ticket1 As String = String.Empty
    Private _customerName As String = String.Empty
    Private _customerLastName As String = String.Empty
    Private _customerMotherLastName As String = String.Empty
    Private _federalTaxpayerRegistry As String = String.Empty
    Private _homonimia As String = String.Empty
    Private _postalCode As String = String.Empty
    Private _birthDate As String = String.Empty


    Public ReadOnly Property Strerror As String
        Get
            Return _strerror
        End Get
    End Property

    Public Property UserID As String
        Get
            Return _userID
        End Get
        Set(value As String)
            _userID = value
        End Set
    End Property

    Public Property Iv_ticket1 As String
        Get
            Return _iv_ticket1
        End Get
        Set(value As String)
            _iv_ticket1 = value
        End Set
    End Property

    Public Property CustomerName As String
        Get
            Return _customerName
        End Get
        Set(value As String)
            _customerName = value
        End Set
    End Property

    Public Property CustomerLastName As String
        Get
            Return _customerLastName
        End Get
        Set(value As String)
            _customerLastName = value
        End Set
    End Property

    Public Property CustomerMotherLastName As String
        Get
            Return _customerMotherLastName
        End Get
        Set(value As String)
            _customerMotherLastName = value
        End Set
    End Property

    Public Property FederalTaxpayerRegistry As String
        Get
            Return _federalTaxpayerRegistry
        End Get
        Set(value As String)
            _federalTaxpayerRegistry = value
        End Set
    End Property

    Public Property Homonimia As String
        Get
            Return _homonimia
        End Get
        Set(value As String)
            _homonimia = value
        End Set
    End Property

    Public Property PostalCode As String
        Get
            Return _postalCode
        End Get
        Set(value As String)
            _postalCode = value
        End Set
    End Property

    Public Property BirthDate As String
        Get
            Return _birthDate
        End Get
        Set(value As String)
            _birthDate = value
        End Set
    End Property

    Sub New()
    End Sub

    Public Function GetCustomer(ByVal intsol As Integer)
        Try

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restGT As WCF.RESTful = New WCF.RESTful()

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("Customer") & "?$filter=(customerName==" & _customerName &
                ",customerLastName==" & CustomerLastName & ",customerMotherLastName==" & _customerMotherLastName &
                ",federalTaxpayerRegistry==" & _federalTaxpayerRegistry & ",homonimia==" & _homonimia & ",postalCode==" & _postalCode &
                ",birthDate==" & _birthDate & ")&$fields=id,person(id,segment)"

            'restGT.consumerID = "10000004"
            Dim respuestaBC As String = restGT.ConnectionGet(userID, iv_ticket1, String.Empty)

            Dim serializerBC As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jresultBC = serializerBC.Deserialize(Of Customer)(respuestaBC)

            ''Error
            Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim alert = srrSerialer.Deserialize(Of msjerr)(respuestaBC)

            If Not restGT.IsError Then
                Dim BD As New ProdeskNet.BD.clsManejaBD
                Dim reg As Integer = BD.ExInsUpd("UPDATE PDK_TAB_DATOS_SOLICITANTE SET CLIENTE_INCREDIT = '" & jresultBC.Person.id.ToString & "' WHERE PDK_ID_SECCCERO = " & intsol)
            End If

        Catch ex As Exception
            _strerror = ex.Message
        End Try

    End Function


    Public Class Customer
        Public Person As Person
    End Class
    Public Class Person
        Public id As String
        Public segment As segment
    End Class
    Public Class segment
        Public name As name
    End Class

    Public Class name
        Public id As String
    End Class
    Public Class msjerr
        Public message As String
        Public status As String
    End Class

End Class
