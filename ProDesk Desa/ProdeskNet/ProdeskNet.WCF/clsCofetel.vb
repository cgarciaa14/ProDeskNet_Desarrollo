#Region "TRACKERS"
'BBV-P-423-RQADM-10 JBEJAR 19/04/2017 Pantalla Consulta cofetel para obtener datos para el web service. 
'RQ-PI7-PD5-JBEJAR: 16/10/2017 Se agregan clases de ws a clscofetel , se agrega la funcion para guardar el log del ws.  
#End Region
Imports ProdeskNet.BD
Public Class clsCofetel
#Region "Variables Privadas"
    Private _strError As String = String.Empty
    Private _ID_SOLICITUD As Integer = 0
    Private _Json As String = String.Empty
    Private _Json_Respuesta = String.Empty
#End Region

#Region "Propiedades Publicas"
    Public Property Json_Respuesta As String
        Get
            Return _Json_Respuesta
        End Get
        Set(value As String)
            _Json_Respuesta = value
        End Set
    End Property
    Public Property Json As String
        Get
            Return _Json
        End Get
        Set(ByVal value As String)
            _Json = value
        End Set
    End Property
    Public Property strErrror As String
        Get
            Return _strError
        End Get
        Set(value As String)
            _strError = value
        End Set
    End Property
    Public Property ID_SOLICITUD As Integer
        Get
            Return _ID_SOLICITUD
        End Get
        Set(value As Integer)
            _ID_SOLICITUD = value
        End Set
    End Property
#End Region
#Region "Metodos y funciones cofetel"
    Public Function GetTelSol() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_SOLICITUD)

            dsres = BD.EjecutaStoredProcedure("SpGetCofetelTel")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                Else
                    Throw New Exception("No se encontraron datos del cuestionario telefono")
                End If
            Else
                Throw New Exception("Falla al consultar cuestionario telefono cofetel")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function

    Public Function InsertaWS() As Boolean
        Try
            Dim _BD As New clsManejaBD
            Dim _dataresult As New DataSet
            InsertaWS = False
            _BD.AgregaParametro("@id_solicitud", TipoDato.Entero, ID_SOLICITUD)
            _BD.AgregaParametro("@json", TipoDato.Cadena, Json)
            _BD.AgregaParametro("@respuesta", TipoDato.Cadena, Json_Respuesta)
            _dataresult = _BD.EjecutaStoredProcedure("spInsertaDatosWSCofetel")
            If _dataresult.Tables.Count > 0 Then
                If _dataresult.Tables(0).Rows.Count > 0 Then
                    If (_dataresult.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        InsertaWS = True
                    Else
                        InsertaWS = False
                        Throw New Exception(_dataresult.Tables(0).Rows(0).Item("RESULT").ToString())
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
#End Region
#Region "Clases servicio cofetel"
    Public Class telephones
        Public telephones As List(Of telephone1) = New List(Of telephone1)
    End Class

    Public Class telephone1
        Public telephone As telephone = New telephone()
    End Class
    Public Class telephone
        Public telephoneNumber As String
        Public type As String

    End Class
    Public Class telephones1
        Public telephones As List(Of telephone11) = New List(Of telephone11)
        Public warningMessage As String
    End Class
    Public Class telephone11
        Public telephone As telephone2 = New telephone2()
        Public warningMessage As String

    End Class
    Public Class telephone2
        Public telephoneNumber As String
        Public type As String
    End Class

#End Region
End Class

