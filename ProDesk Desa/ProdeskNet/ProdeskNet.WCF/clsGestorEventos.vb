'-- RQMSG  JBEJAR 31/05/2017 clase para el manejo de webservices sms e email automaticos.
'-- BUG-PD-170 JBEJAR 18/08/2017 Se  guarda la respuesta del wss y los parametros enviados.
Imports ProdeskNet.BD
#Region "PAY LOAD APROBADO BCOM"
Public Class clsGestorEventos
    Public dtoReceiver As DtoReceiver = New DtoReceiver()
    Public Property eventCode As String
    Public Property message As String
    Public Property messageTemplate As String
    Public Property receiverReference As String
    Public Property receiverType As String

End Class
Public Class EmailReceiver
    Public Property address As String
End Class

Public Class DtoReceiver
    Public emailReceiver As EmailReceiver = New EmailReceiver()
End Class
#End Region

#Region "APROBADO SMS"
Public Class clsaproSMS
    Public dtoReceiver As DtoReceiver1 = New DtoReceiver1()
    Public Property eventCode As String
    Public Property message As String
    Public Property messageTemplate As String
    Public Property receiverReference As String
    Public Property receiverType As String
End Class

Public Class DtoReceiver1
    Public smsReceiver As smsReceiver = New smsReceiver()
End Class

Public Class smsReceiver
    Public company As String
    Public phoneNumber As String
End Class

#End Region

#Region "GUARDAR_RESPUESTA"
Public Class clsGuardarRespuesta
    Private _ID_SOLICITUD As Integer = 0
    Private _JSON As String = String.Empty
    Private _RESPUESTA As String = String.Empty
    Private _strError As String = String.Empty

    Public Property ID_SOLICITUD As Integer
        Get
            Return _ID_SOLICITUD
        End Get
        Set(ByVal value As Integer)
            _ID_SOLICITUD = value
        End Set
    End Property

    Public Property JSON As String
        Get
            Return _JSON
        End Get
        Set(value As String)
            _JSON = value
        End Set
    End Property

    Public Property RESPUESTA As String
        Get
            Return _RESPUESTA
        End Get
        Set(value As String)
            _RESPUESTA = value
        End Set
    End Property

    Public Function insertaDatosRespuesta() As Boolean
        insertaDatosRespuesta = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            BD.AgregaParametro("@id_solicitud", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@json", TipoDato.Cadena, JSON)
            BD.AgregaParametro("@respuesta", TipoDato.Cadena, RESPUESTA)
            dsres = BD.EjecutaStoredProcedure("spInsertaDatosRespuesta")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        insertaDatosRespuesta = True
                    Else
                        insertaDatosRespuesta = False
                        Throw New Exception(dsres.Tables(0).Rows(0).Item("RESULT").ToString())
                    End If
                Else
                    Throw New Exception("Falla al guardar Datos")
                End If
            Else
                Throw New Exception("Falla al guardar Datos")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return False
        End Try
    End Function
End Class
#End Region