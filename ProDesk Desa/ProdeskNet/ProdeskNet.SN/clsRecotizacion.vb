'RQ-PI7-PD9: CGARCIA: 15/11/2017: CREACION DE LA RECOTIZACION
Imports ProdeskNet.BD
Public Class clsRecotizacion
    Private _strError As String
    Private _ID_SOLICITUD As Integer = 0
    Private _opcion As Integer = 0
    Private _pantalla As Integer = 0

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

    Public Property PANTALLA As Integer
        Get
            Return _pantalla
        End Get
        Set(value As Integer)
            _pantalla = value
        End Set
    End Property

    Public Function getRecotiza(ByVal opcion) As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet

            Select opcion
                Case 1
                    If _opcion <> 0 Then BD.AgregaParametro("@opcion", TipoDato.Entero, _opcion)
                    If _ID_SOLICITUD <> 0 Then BD.AgregaParametro("@idSolicitud", TipoDato.Entero, _ID_SOLICITUD)
            End Select

            dsres = BD.EjecutaStoredProcedure("sp_Recotizacion")

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

    Public Function ConsultaTarea() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet

            dsres = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & _pantalla)

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
End Class
