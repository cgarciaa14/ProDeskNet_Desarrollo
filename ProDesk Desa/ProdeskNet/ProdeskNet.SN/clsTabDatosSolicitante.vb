'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BUG-PD-145: RHERNANDEZ: 10/07/17 SE CREA METODO CargaRespuestaScoring PARA OBTENER DATOS DE LA TABLA PDK_TAB_HERMES_RESPONSE
Imports System.Text
Public Class clsTabDatosSolicitante

    Private _strError As String = String.Empty
    Private _PDK_ID_DATOS_SOLICITANTE As Integer = 0
    Private _PDK_ID_SECCCERO As Integer = 0
    Private _NUM_SOLICITUD As Integer = 0
    Private _NOMBRE_SOLICI As String = String.Empty
    Private _STATUS As String = String.Empty
    Private _PDK_FECHA_MODIF As String = String.Empty
    Private _PDK_CLAVE_USUARIO As Integer = 0
    Private _CLIENTE_INCREDIT As String = String.Empty
    Private _NUMERO_CLIENTE As Integer = 0
    Private _FECHA_DIA As String = String.Empty
    Private _NUM_COTIZACION As Integer = 0
    Private _STATUS_CREDITO As String = String.Empty
    Private _STATUS_DOC As String = String.Empty
    Private _COTIZACION As Integer = 0

    Sub New()
    End Sub

    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property

    Public ReadOnly Property PDK_ID_DATOS_SOLICITANTE As Integer
        Get
            Return _PDK_ID_DATOS_SOLICITANTE
        End Get
    End Property

    Public Property PDK_ID_SECCCERO As Integer
        Get
            Return _PDK_ID_SECCCERO
        End Get
        Set(value As Integer)
            _PDK_ID_SECCCERO = value
        End Set
    End Property

    Public Property NUM_SOLICITUD As Integer
        Get
            Return _NUM_SOLICITUD
        End Get
        Set(value As Integer)
            _NUM_SOLICITUD = value
        End Set
    End Property

    Public Property NOMBRE_SOLICI As String
        Get
            Return _NOMBRE_SOLICI
        End Get
        Set(value As String)
            _NOMBRE_SOLICI = value
        End Set
    End Property

    Public Property STATUS As String
        Get
            Return _STATUS
        End Get
        Set(value As String)
            _STATUS = value
        End Set
    End Property

    Public Property PDK_FECHA_MODIF As String
        Get
            Return _PDK_FECHA_MODIF
        End Get
        Set(value As String)
            _PDK_FECHA_MODIF = value
        End Set
    End Property

    Public Property PDK_CLAVE_USUARIO As Integer
        Get
            Return _PDK_CLAVE_USUARIO
        End Get
        Set(value As Integer)
            _PDK_CLAVE_USUARIO = value
        End Set
    End Property

    Public Property CLIENTE_INCREDIT As String
        Get
            Return _CLIENTE_INCREDIT
        End Get
        Set(value As String)
            _CLIENTE_INCREDIT = value
        End Set
    End Property

    Public Property NUMERO_CLIENTE As Integer
        Get
            Return _NUMERO_CLIENTE
        End Get
        Set(value As Integer)
            _NUMERO_CLIENTE = value
        End Set
    End Property

    Public Property FECHA_DIA As String
        Get
            Return _FECHA_DIA
        End Get
        Set(value As String)
            _FECHA_DIA = value
        End Set
    End Property

    Public Property NUM_COTIZACION As Integer
        Get
            Return _NUM_COTIZACION
        End Get
        Set(value As Integer)
            _NUM_COTIZACION = value
        End Set
    End Property

    Public Property STATUS_CREDITO As String
        Get
            Return _STATUS_CREDITO
        End Get
        Set(value As String)
            _STATUS_CREDITO = value
        End Set
    End Property

    Public Property STATUS_DOC As String
        Get
            Return _STATUS_DOC
        End Get
        Set(value As String)
            _STATUS_DOC = value
        End Set
    End Property

    Public Property COTIZACION As Integer
        Get
            Return _COTIZACION
        End Get
        Set(value As Integer)
            _COTIZACION = value
        End Set
    End Property

    Public Function CargaDatosSolicitante(ByVal intSol As Integer) As DataSet

        Dim dts As DataSet = New DataSet()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder

        Try

            sql.AppendLine("SELECT PDK_ID_DATOS_SOLICITANTE, PDK_ID_SECCCERO, NUM_SOLICITUD, NOMBRE_SOLICI, [STATUS],")
            sql.AppendLine("PDK_FECHA_MODIF, PDK_CLAVE_USUARIO, CLIENTE_INCREDIT, NUMERO_CLIENTE, FECHA_DIA,")
            sql.AppendLine("NUM_COTIZACION, STATUS_CREDITO, STATUS_DOC, COTIZACION")
            sql.AppendLine("FROM PDK_TAB_DATOS_SOLICITANTE")
            sql.AppendLine("WHERE PDK_ID_SECCCERO = " & intSol.ToString)

            dts = BD.EjecutarQuery(sql.ToString)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    Return dts
                Else
                    _strError = "Error al consultar la información."
                    Return Nothing
                End If
            Else
                _strError = "Error al consultar la información."
                Return Nothing
            End If

        Catch ex As Exception
            _strError = "Exception: Error al cargar la información."
            Return Nothing
        End Try
    End Function


    Public Function CargaRespuestaScoring(ByVal intSol As Integer) As DataSet

        Dim dts As DataSet = New DataSet()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder

        Try

            sql.AppendLine("SELECT PDK_ID_SECCCERO, balance, paymentCapacity, score, reference,")
            sql.AppendLine("antifraud, antifraudDictum, maximumLimit, finalDictum, rejectionPolicy, PDK_EXCEP_VAL")
            sql.AppendLine("FROM PDK_TAB_HERMES_RESPONSE")
            sql.AppendLine("WHERE PDK_ID_SECCCERO = " & intSol.ToString)

            dts = BD.EjecutarQuery(sql.ToString)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    Return dts
                Else
                    _strError = "Error al consultar la información."
                    Return Nothing
                End If
            Else
                _strError = "Error al consultar la información."
                Return Nothing
            End If

        Catch ex As Exception
            _strError = "Exception: Error al cargar la información."
            Return Nothing
        End Try
    End Function


    Public Function CargaDatosCotiza()

    End Function

End Class
