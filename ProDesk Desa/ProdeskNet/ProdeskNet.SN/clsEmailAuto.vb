'-- RQMSG  JBEJAR 26/05/2017  obtiene tarea anterior. 
'-- BUG-PD-65 JBEJAR 30/05/2017 Se agregan mejoras para  el web services de emails y sms   automaticos. 
'--BUG-PD-203 CGARCIA 04/09/2017 SE AGREGAN PARAMETROS DE CONSULTA 
Imports ProdeskNet.BD
Public Class clsEmailAuto
    Private _strError As String
    Private _ID_SOLICITUD As Integer = 0
    Private _TAREA_ANTERIOR As Integer = 0
    Private _opcion As Integer = 0
    Private _tarea_actual As Integer = 0

    Public Property TAREA_ANTERIOR As Integer
        Get
            Return _TAREA_ANTERIOR
        End Get
        Set(ByVal value As Integer)
            _TAREA_ANTERIOR = value
        End Set
    End Property

    Public Property strError As String
        Get
            Return _strError
        End Get
        Set(ByVal value As String)
            _strError = value
        End Set
    End Property

    Public Property ID_SOLICITUD As Integer
        Get
            Return _ID_SOLICITUD
        End Get
        Set(ByVal value As Integer)
            _ID_SOLICITUD = value
        End Set
    End Property

    Public Property OPCION As Integer
        Get
            Return _opcion
        End Get
        Set(value As Integer)
            _opcion = value
        End Set
    End Property

    Public Property TAREA_ACTUAL As Integer
        Get
            Return _tarea_actual
        End Get
        Set(value As Integer)
            _tarea_actual = value
        End Set
    End Property


    Public Function GetEmail() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_SOLICITUD)
            dsres = BD.EjecutaStoredProcedure("SpGetEmail")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                Else
                    Throw New Exception("No se encontraron datos del caso consultado")
                End If
            Else
                Throw New Exception("Falla al consultar datos")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function
    Public Function GetWS() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@IdTarea", TipoDato.Entero, TAREA_ANTERIOR)
            dsres = BD.EjecutaStoredProcedure("SpGetWSMS")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                Else
                    Throw New Exception("No se encontraron datos del caso consultado")
                End If
            Else
                Throw New Exception("Falla al consultar datos")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function
    Public Function Consultasolicitud() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@opcion", TipoDato.Entero, OPCION)
            BD.AgregaParametro("@parametro", TipoDato.Entero, ID_SOLICITUD)
            dsres = BD.EjecutaStoredProcedure("spCatalogos")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                Else
                    Throw New Exception("No se encontraron datos del caso consultado")
                End If
            Else
                Throw New Exception("Falla al consultar datos")
            End If

        Catch ex As Exception
            _strError = ex.Message.ToString()
        End Try
    End Function
    Public Function ConsultaStatusNotificacion() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@opcion", TipoDato.Entero, OPCION)
            BD.AgregaParametro("@parametro", TipoDato.Entero, _tarea_actual)
            dsres = BD.EjecutaStoredProcedure("spCatalogos")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                Else
                    Throw New Exception("No se encontraron datos del caso consultado")
                End If
            Else
                Throw New Exception("Falla al consultar datos")
            End If

        Catch ex As Exception
            _strError = ex.Message.ToString()
        End Try
    End Function
End Class
