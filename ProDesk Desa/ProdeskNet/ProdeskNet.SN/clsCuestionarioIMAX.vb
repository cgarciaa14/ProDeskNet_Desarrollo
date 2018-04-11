Imports ProdeskNet.BD
'RQADM-38:RHERNANDEZ:05/05/17: Se crea clase para la operacion de la tarea de Consulta IMAX
Public Class clsCuestionarioIMAX
    Private _strError As String = String.Empty
    Private ID_SOLICITUD As Integer = 0
    Private ID_INCREDIT As String = String.Empty
    Private TIPO_ID As Integer = 0
    Private FOTO_FIRMA_ID As Integer = 0
    Public Property StrError() As String
        Get
            Return _strError
        End Get
        Set(value As String)
            _strError = value
        End Set
    End Property
    Public Property _ID_SOLICITUD() As Integer
        Get
            Return ID_SOLICITUD
        End Get
        Set(value As Integer)
            ID_SOLICITUD = value
        End Set
    End Property
    Public Property _ID_INCREDIT() As String
        Get
            Return ID_INCREDIT
        End Get
        Set(value As String)
            ID_INCREDIT = value
        End Set
    End Property
    Public Property _TIPO_ID() As Integer
        Get
            Return TIPO_ID
        End Get
        Set(value As Integer)
            TIPO_ID = value
        End Set
    End Property
    Public Property _FOTO_FIRMA_ID() As Integer
        Get
            Return FOTO_FIRMA_ID
        End Get
        Set(value As Integer)
            FOTO_FIRMA_ID = value
        End Set
    End Property
    Public Function GetDatosClient(ByVal opc As Integer) As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_SOLICITUD)
            If ID_INCREDIT <> String.Empty Then
                BD.AgregaParametro("@IdClientIncredit", TipoDato.Cadena, ID_INCREDIT)
            End If
            BD.AgregaParametro("@opcion", TipoDato.Entero, opc)

            dsres = BD.EjecutaStoredProcedure("Sp_GetDatosSol")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                End If
            Else
                Throw New Exception("Falla al consultar datos del solicitante")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function

    Public Function InsertDatosCuestionarioIMAX() As Boolean
        InsertDatosCuestionarioIMAX = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            BD.AgregaParametro("@ID_SOLICITUD", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@TIPOID", TipoDato.Entero, TIPO_ID)
            BD.AgregaParametro("@FIRMA_FOTO_ID", TipoDato.Entero, FOTO_FIRMA_ID)
            dsres = BD.EjecutaStoredProcedure("Sp_Insert_ConsultaIMAX")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        InsertDatosCuestionarioIMAX = True
                    Else
                        InsertDatosCuestionarioIMAX = False
                        Throw New Exception(dsres.Tables(0).Rows(0).Item("RESULT").ToString())
                    End If
                Else
                    Throw New Exception("Falla al guardar Cuestionario")
                End If
            Else
                Throw New Exception("Falla al guardar Cuestionario")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return False
        End Try
    End Function
    Public Function GetCuestionarioIMAX() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@ID_SOLICITUD", TipoDato.Entero, ID_SOLICITUD)

            dsres = BD.EjecutaStoredProcedure("Sp_Get_ConsultaIMAX")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                End If
            Else
                Throw New Exception("Falla al consultar cuestionario IMAX")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function
End Class
