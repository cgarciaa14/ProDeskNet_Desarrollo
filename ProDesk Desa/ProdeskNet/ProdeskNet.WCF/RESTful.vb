'BBVA-P-412 RQ06: AMR: 20/09/2016 Administración de Planes de Financiamiento: Promociones
'BBVA-P-412 RQ WSA. WS GetLoan Products (RQ06, RQ09): AMR: 25/10/2016 WS GetLoan Products
'BBVA-P-412 RQWSE: GVARGAS: 27/10/2016 Cambios en la clase
'BBVA-P-423:RQCONYFOR-05 JRHM 07/12/16 Se agrego estos objetos para la consulta de servicios bbva
'BBVA-P-423:RQSOL-01: AMR:14/12/2016 Precalificación Brechas (31, 49, 75)
'BUG-PD-13  GVARGAS  01/03/2017  Cambios consumerID
'BUG-PD-18  GVARGAS  07/03/2017  Reset consumerID
'BUG-PC-85 GVARGAS 05/07/2017 consumerID_Extranet
'BUG-PD-172 GVARGAS 27/07/2017 Cambio Codigo error cuando Falla WS
'BUG-PD-175 GVARGAS 26/07/2017 Cambio para Ñs
'BUG-PD-192 GVARGAS 21/08/2017 ByPass SSL

'V 1.0.1 Agregados metodos GET, POST, PUT, PATCH Y DELETE
'V 2.0.1 Validacion de errores segun HTTP Status
'V 2.0.2 Agregado StatusHTTP propiedad
'V 3.0.1 Agregados parametros para intentos de conexion para obtener un TSEC, Actualizados los metodos de conexion permitiendo 5 intentos,
'        agregado metodo Reset() que permite reutilizar la clase para mas de 1 conexion a un servicio REST, agregado un metodo buscarHeader() que permite leer valores
'        contenidos en los headers de respuesta, modificados los metodos GetTsec() y Connection() permitiendo leer el bosy de respuesta en caso de error
'V 3.0.2 Mejorado el Catch de errores de Red y logicos para los metodos GetTsec() y Connection(), agregada la opcion de no usar TSEC para el consumo de Servicios REST
'BBVA-P-423: RQCONYFOR-07_2: AVH: 11/04/2017 Se modifica JSON 

Imports System
Imports System.Net
Imports System.IO
Imports System.Threading.Thread
Imports System.Text
Imports System.Net.Security

Public Class RESTful
    Private _Uri As String = String.Empty
    Private _IsError As Boolean = False
    Private _MensajeError As String = String.Empty
    Private _StatusHTTP As String = String.Empty
    Private _bodyHTML As String = String.Empty
    Private _userID As String = String.Empty
    Private _iv_ticket As String = String.Empty
    Private _tsec As String = String.Empty
    Private _counterConnection As Integer = 0
    Private _Interval As Integer = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("Interval").ToString())
    Private _Intents As Integer = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("Intents").ToString())
    Private _uriGRANTINGTICKET As String = System.Configuration.ConfigurationManager.AppSettings("uriGRANTINGTICKET").ToString()
    Private _consumerID As String = System.Configuration.ConfigurationManager.AppSettings("consumerID").ToString()
    Private _buscarHeader As Boolean = False
    Private _Header As String = String.Empty
    Private _valorHeader As String = String.Empty
    Private _SinTSEC As Boolean = False
    Private _logicalTerm As String = String.Empty
    Private _accountTerm As String = String.Empty

    Public Property Uri() As String
        Get
            Return Me._Uri
        End Get
        Set(ByVal value As String)
            Me._Uri = value
        End Set
    End Property

    Public ReadOnly Property IsError() As Boolean
        Get
            Return Me._IsError
        End Get
    End Property

    Public ReadOnly Property MensajeError() As String
        Get
            Return Me._MensajeError
        End Get
    End Property

    Public ReadOnly Property StatusHTTP() As String
        Get
            Return Me._StatusHTTP
        End Get
    End Property

    Public ReadOnly Property consumerID() As String
        Get
            Return Me._consumerID
        End Get
    End Property

    Public ReadOnly Property valorHeader() As String
        Get
            Return Me._valorHeader
        End Get
    End Property

    Public Property LogicalTerm() As String
        Get
            Return Me._logicalTerm
        End Get
        Set(ByVal value As String)
            Me._logicalTerm = value
        End Set
    End Property
    Public Property AccountTerm() As String
        Get
            Return Me._accountTerm
        End Get
        Set(ByVal value As String)
            Me._accountTerm = value
        End Set
    End Property

    Private Function Connection(ByVal verbo As String) As String
        Dim json As String
        Try
            Try
                If (Me._SinTSEC = False) Then
                    'While ((Me._tsec = String.Empty) And (Me._counterConnection < 5))
                    Me.GetTsec()
                    'End While
                End If

                If (Me._tsec = String.Empty) Then
                    Dim str_msg As String = String.Empty
                    If (Me._MensajeError.ToUpper.Contains("TICKET")) Then
                        str_msg = "Favor de cerrar sesión e iniciar nuevamente"
                    Else
                        str_msg = "Se ha intentado conectar al servicio pero no esta disponible GT"
                    End If

                    Return "{ 'message' : '" + str_msg + "', 'status' : '0'  }"
                End If

                ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications

                Dim request As HttpWebRequest = CType(WebRequest.Create(Me._Uri), HttpWebRequest)

                Dim encoding As New UTF8Encoding()
                Dim bite As Byte() = encoding.GetBytes(Me._bodyHTML)

                request.Method = verbo
                'request.ContentType = "application/json;charset=UTF-8"
                request.ContentType = "application/json"
                If (Me._SinTSEC = False) Then
                    request.Headers.Add("tsec", Me._tsec)
                End If

                Dim Uri_Formalize As String = System.Configuration.ConfigurationManager.AppSettings.Item("createCarLoanFormalize")

                If Uri_Formalize = Me._Uri Then
                    request.Headers.Add("onsite-logicalTerm", Me._logicalTerm)
                    request.Headers.Add("onsite-accountTerm", Me._accountTerm)
                End If



                If (verbo <> "GET") Then
                    request.ContentLength = bite.Length
                    Dim newStream As Stream = request.GetRequestStream()
                    newStream.Write(bite, 0, bite.Length)
                    newStream.Close()
                End If

                Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
                Me._StatusHTTP = response.StatusCode
                json = reader.ReadToEnd()
                If (Me._buscarHeader) Then
                    Me._valorHeader = response.Headers(Me._Header)
                End If
            Catch ex As WebException
                Dim status As String = CType(ex.Response, HttpWebResponse).StatusCode

                json = "{""message"" : """ + ex.Message + """, ""status"" : """ + status + """}"
                Dim reader As StreamReader = New StreamReader(CType(ex.Response, HttpWebResponse).GetResponseStream())
                Dim jsonError As String = reader.ReadToEnd()
                Dim bodyError As bodyError = New bodyError()
                bodyError = bodyError.Deserializar(jsonError)
                Me._MensajeError = bodyError.error_message

                Me._IsError = True
                Me._StatusHTTP = status
                If (Me._buscarHeader) Then
                    Me._valorHeader = bodyError.error_code
                End If

            End Try
        Catch ex As Exception
            json = "{ ""message"" : """ + ex.Message + """, ""status"" : ""0""  }"
            Me._MensajeError = ex.Message
            Me._IsError = True
            Me._StatusHTTP = "0"
        End Try
        Return json
    End Function

    Public Function ConnectionGet(ByVal userID As String, ByVal iv_ticket As String, ByVal json As String) As String
        Me._userID = userID
        Me._iv_ticket = iv_ticket
        Me._bodyHTML = json
        Dim jsonRespond As String = Connection("GET")
        Return jsonRespond
    End Function

    Public Function ConnectionPost(ByVal userID As String, ByVal iv_ticket As String, ByVal json As String, Optional LogicalTerm As String = "", Optional accountTerm As String = "") As String
        Me._userID = userID
        Me._iv_ticket = iv_ticket
        Me._bodyHTML = json

        If LogicalTerm <> "" And accountTerm <> "" Then
            Me._logicalTerm = LogicalTerm
            Me._accountTerm = accountTerm
        End If

        Dim jsonRespond As String = Connection("POST")
        Return jsonRespond
    End Function

    Public Function ConnectionPut(ByVal userID As String, ByVal iv_ticket As String, ByVal json As String) As String
        Me._userID = userID
        Me._iv_ticket = iv_ticket
        Me._bodyHTML = json
        Dim jsonRespond As String = Connection("PUT")
        Return jsonRespond
    End Function

    Public Function ConnectionDelete(ByVal userID As String, ByVal iv_ticket As String, ByVal json As String) As String
        Me._userID = userID
        Me._iv_ticket = iv_ticket
        Me._bodyHTML = json
        Dim jsonRespond As String = Connection("DELETE")
        Return jsonRespond
    End Function

    Public Function ConnectionPatch(ByVal userID As String, ByVal iv_ticket As String, ByVal json As String) As String
        Me._userID = userID
        Me._iv_ticket = iv_ticket
        Me._bodyHTML = json
        Dim jsonRespond As String = Connection("PATCH")
        Return jsonRespond
    End Function

    Public Sub GetTsec()

        Dim header As headerTsec = New headerTsec()
        header.authentication.userID = Me._userID

        'If (Me._userID = System.Configuration.ConfigurationManager.AppSettings("GENERIC_userID").ToString()) Then
        If Me._userID.IndexOf("EXT") <> -1 Then
            header.authentication.consumerID = System.Configuration.ConfigurationManager.AppSettings("GENERIC_consumerID").ToString()
        Else
            header.authentication.consumerID = Me._consumerID
        End If
        Me._consumerID = header.authentication.consumerID.ToString

        header.authentication.authenticationType = "00"

        Dim authenticationDataBody As authenticationDataBody = New authenticationDataBody()
        authenticationDataBody.idAuthenticationData = "iv_ticketService"
        authenticationDataBody.authenticationData.Add(Me._iv_ticket)

        header.authentication.authenticationData.Add(authenticationDataBody)
        header.backendUserRequest.userId = ""
        header.backendUserRequest.dialogId = ""
        header.backendUserRequest.accessCode = ""

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonBODY As String = serializer.Serialize(header)

        Try
            Try
                ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications

                Dim request As HttpWebRequest = CType(WebRequest.Create(Me._uriGRANTINGTICKET), HttpWebRequest)

                Dim encoding As New UTF8Encoding()
                Dim bite As Byte() = encoding.GetBytes(jsonBODY)

                request.Method = "POST"
                request.ContentType = "application/json"
                request.ContentLength = bite.Length
                Dim newStream As Stream = request.GetRequestStream()
                newStream.Write(bite, 0, bite.Length)
                newStream.Close()

                Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
                Me._tsec = response.Headers("tsec")
                Me._counterConnection = 0

            Catch ex As WebException
                'Me._counterConnection = Me._counterConnection + 1
                'If (Me._counterConnection <= Me._Intents) Then
                Dim status As String = CType(ex.Response, HttpWebResponse).StatusCode

                Dim reader As StreamReader = New StreamReader(CType(ex.Response, HttpWebResponse).GetResponseStream())
                Dim jsonError As String = reader.ReadToEnd()
                Dim bodyError As bodyError = New bodyError()
                bodyError = bodyError.Deserializar(jsonError)
                Me._MensajeError = bodyError.error_message

                Me._IsError = True
                Me._StatusHTTP = status
                'End If
                'Threading.Thread.Sleep(Me._Interval)
            End Try
        Catch ex As Exception
            'Me._counterConnection = Me._counterConnection + 1
            'If (Me._counterConnection <= Me._Intents) Then
            Me._MensajeError = ex.Message
            Me._IsError = True
            Me._StatusHTTP = "0"
            'End If
            'Threading.Thread.Sleep(Me._Interval)
        End Try
        Return
    End Sub

    Public Sub Reset()
        Me._Uri = String.Empty
        Me._IsError = False
        Me._MensajeError = String.Empty
        Me._StatusHTTP = String.Empty
        Me._bodyHTML = String.Empty
        Me._userID = String.Empty
        Me._iv_ticket = String.Empty
        Me._tsec = String.Empty
        Me._counterConnection = 0
        Me._Interval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("Interval").ToString())
        Me._Intents = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("Intents").ToString())
        Me._uriGRANTINGTICKET = System.Configuration.ConfigurationManager.AppSettings("uriGRANTINGTICKET").ToString()
        Me._consumerID = System.Configuration.ConfigurationManager.AppSettings("consumerID").ToString()
        Me._buscarHeader = False
        Me._Header = String.Empty
        Me._valorHeader = String.Empty
        Me._SinTSEC = False
    End Sub

    Public Sub buscarHeader(ByVal Header As String)
        Me._buscarHeader = True
        Me._Header = Header
    End Sub

    Public Sub SinTSEC()
        Me._SinTSEC = True
    End Sub

    Public Function AcceptAllCertifications() As Boolean
        Return True
    End Function
End Class


Public Class headerTsec
    Public authentication As authenticationBody = New authenticationBody()
    Public backendUserRequest As backendUserRequestBody = New backendUserRequestBody()
End Class

Public Class authenticationBody
    Public userID As String
    Public consumerID As String
    Public authenticationType As String
    Public authenticationData As New List(Of authenticationDataBody)
End Class

Public Class backendUserRequestBody
    Public userId As String
    Public accessCode As String
    Public dialogId As String
End Class

Public Class authenticationDataBody
    Public idAuthenticationData As String
    Public authenticationData As New List(Of String)
End Class

Public Class bodyError
    Public version As String
    Public severity As String
    Public http_status As String
    Public error_code As String
    Public error_message As String
    Public system_error_code As String
    Public system_error_description As String
    Public system_error_cause As String

    Public Function replaceVars(ByVal json As String) As String
        json = json.Replace("http-status", "http_status")
        json = json.Replace("error-code", "error_code")
        json = json.Replace("error-message", "error_message")
        json = json.Replace("system-error-code", "system_error_code")
        json = json.Replace("system-error-description", "system_error_description")
        json = json.Replace("system-error-cause", "system_error_cause")
        Return json
    End Function

    Public Function Deserializar(ByVal json As String) As bodyError
        Dim bodyError As bodyError = New bodyError()
        json = bodyError.replaceVars(json)
        Dim serializerError As New System.Web.Script.Serialization.JavaScriptSerializer()
        Return serializerError.Deserialize(Of bodyError)(json)
    End Function
End Class