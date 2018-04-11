#Region "TRACKER"
'RQCAINTBAC-03: erodriguez: 26/05/17  se creo la clase para consulta del servicio TXEMDC
#End Region

Public Class ClsCFDI
    Private _strerror As String = String.Empty
    Private _transmitterTaxpayer As String = String.Empty
    Private _receiverTaxpayer As String = String.Empty
    Private _totalRevenues As String = String.Empty
    Private _referenceFiscalNumber As String = String.Empty

    Private _estatus As String = String.Empty

    Private _data As String = String.Empty
    Private _extension As String = String.Empty

    Public ReadOnly Property Strerror As String
        Get
            Return _strerror
        End Get
    End Property
    'estatus de la validacion
    Public Property estatus As String
        Get
            Return _estatus
        End Get
        Set(value As String)
            _estatus = value
        End Set
    End Property

    Public Property transmitterTaxpayer As String
        Get
            Return _transmitterTaxpayer
        End Get
        Set(value As String)
            _transmitterTaxpayer = value
        End Set
    End Property

    Public Property receiverTaxpayer As String
        Get
            Return _receiverTaxpayer
        End Get
        Set(value As String)
            _receiverTaxpayer = value
        End Set
    End Property

    Public Property totalRevenues As String
        Get
            Return _totalRevenues
        End Get
        Set(value As String)
            _totalRevenues = value
        End Set
    End Property

    Public Property referenceFiscalNumber As String
        Get
            Return _referenceFiscalNumber
        End Get
        Set(value As String)
            _referenceFiscalNumber = value
        End Set
    End Property
    Public Property data As String
        Get
            Return _data
        End Get
        Set(value As String)
            _data = value
        End Set
    End Property

    Public Property extension As String
        Get
            Return _extension
        End Get
        Set(value As String)
            _extension = value
        End Set
    End Property

#Region "cfdiclass"
    Public Class cfdiClass
        Public document As documentdata = New documentdata()
        Public transmitterTaxpayer As String = String.Empty
        Public receiverTaxpayer As String = String.Empty
        Public totalRevenues As String = String.Empty
        Public referenceFiscalNumber As String
    End Class
    Public Class documentdata
        Public data As String = String.Empty
        Public extension As String = String.Empty
    End Class

    Public Class msjerr
        Public message As String
        Public status As String
    End Class

    ''Respuesta
    Public Class jResult
        Public document As documentdata = New documentdata()
        Public validationResponse As ValidationResponse = New ValidationResponse()
    End Class

    Public Class ValidationResponse
        Public id As String = String.Empty
        Public name As String = String.Empty
    End Class
#End Region
    Sub New()
    End Sub

    Public Function ValidaCFDI() As Boolean
        Try
            ValidaCFDI = False
            Dim objpros As New cfdiClass()
            Dim resp As jResult

            objpros.document.data = _data
            objpros.document.extension = _extension
            objpros.receiverTaxpayer = _receiverTaxpayer
            objpros.referenceFiscalNumber = _referenceFiscalNumber
            objpros.totalRevenues = _totalRevenues
            objpros.transmitterTaxpayer = _transmitterTaxpayer

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonBODY As String = serializer.Serialize(objpros)

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restGT As RESTful = New RESTful()
            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("verificaCFDI")
            'restGT.consumerID = "10000004" '"10000024"

            'restGT.buscarHeader("ResponseWarningDescription")

            Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

            'Dim str As String = restGT.valorHeader

            Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()


            If restGT.IsError Then
                '_strerror = IIf(restGT.StatusHTTP = 500, "Error al consultar Servicio Web: ", "Mensaje del Servicio Web: ") & restGT.MensajeError & ". Estatus:" & restGT.StatusHTTP
                _strerror = IIf(restGT.StatusHTTP = 500, "Error al consultar Servicio Web: ", "Mensaje del Servicio Web: ") & "No disponible" & ". Estatus:" & restGT.StatusHTTP
                Exit Function
            Else
                resp = srrSerialer.Deserialize(Of jResult)(jsonResult)

                estatus = resp.validationResponse.name

                If (resp.validationResponse.id = 0 And resp.validationResponse.name = "VALIDACION EXITOSA") Then
                    ValidaCFDI = True
                End If

            End If
        Catch ex As Exception
            Return _strerror = ex.Message
        End Try
    End Function


End Class
