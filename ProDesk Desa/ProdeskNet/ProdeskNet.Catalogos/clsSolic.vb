#Region "Trackers"
'BBV-P-423 - RQADM-31 22/03/2017 MAPH Mensajes de Red
'BBV-P-423 - RQXLS1: 23/05/2017 CGARCIA Se agrego la variable para la consulta de alianza 
'RQ-PD33: DJUAREZ: 03/05/2018: Se crea nueva pantalla para visualizar biometría
'BUG-PD-449 GVARGAS 23/05/2018 Cambio metodo obtenerSolicitudes
#End Region

Imports ProdeskNet.BD
Imports System.Globalization

Public Class clsSolic

#Region "Variables"
    Private _NumeroSolicitud As Integer
    Private _NombreCliente As String
    Private _RFC As String
    Private _NombreAgencia As String
    Private _NombreCanal As String
    Private _NombreMarca As String
    Private _NombreSubmarca As String
    Private _MontoSolicitado As String
    Private _TareaActual As String
    Private _StatusTareaActual As String
    Private _Alianza As String
    Private _Mensaje As String
    Private _PantallaAut As String
    Private _PantallaError As String
    Private _NumError As String
#End Region

#Region "Properties"
    Public Property NumeroSolicitud() As Integer
        Get
            Return _NumeroSolicitud
        End Get
        Set(ByVal value As Integer)
            _NumeroSolicitud = value
        End Set
    End Property

    Public Property NombreCliente() As String
        Get
            Return _NombreCliente
        End Get
        Set(ByVal value As String)
            _NombreCliente = value
        End Set
    End Property

    Public Property RFC() As String
        Get
            Return _RFC
        End Get
        Set(ByVal value As String)
            _RFC = value
        End Set
    End Property

    Public Property NombreAgencia() As String
        Get
            Return _NombreAgencia
        End Get
        Set(ByVal value As String)
            _NombreAgencia = value
        End Set
    End Property

    Public Property NombreCanal() As String
        Get
            Return _NombreCanal
        End Get
        Set(ByVal value As String)
            _NombreCanal = value
        End Set
    End Property

    Public Property NombreMarca() As String
        Get
            Return _NombreMarca
        End Get
        Set(ByVal value As String)
            _NombreMarca = value
        End Set
    End Property

    Public Property NombreSubmarca() As String
        Get
            Return _NombreSubmarca
        End Get
        Set(ByVal value As String)
            _NombreSubmarca = value
        End Set
    End Property

    Public Property MontoSolicitado() As String
        Get
            Return _MontoSolicitado
        End Get
        Set(ByVal value As String)
            _MontoSolicitado = value
        End Set
    End Property

    Public Property TareaActual() As String
        Get
            Return _TareaActual
        End Get
        Set(ByVal value As String)
            _TareaActual = value
        End Set
    End Property

    Public Property StatusTareaActual() As String
        Get
            Return _StatusTareaActual
        End Get
        Set(ByVal value As String)
            _StatusTareaActual = value
        End Set
    End Property

    Public Property Alianza() As String
        Get
            Return _Alianza
        End Get        
        Set(value As String)
            _Alianza = value
        End Set
    End Property

    Public Property Mensaje() As String
        Get
            Return _Mensaje
        End Get
        Set(value As String)
            _Mensaje = value
        End Set
    End Property

    Public Property PantallaAut() As String
        Get
            Return _PantallaAut
        End Get
        Set(value As String)
            _PantallaAut = value
        End Set
    End Property

    Public Property PantallaError() As String
        Get
            Return _PantallaError
        End Get
        Set(value As String)
            _PantallaError = value
        End Set
    End Property

    Public Property NumError() As String
        Get
            Return _NumError
        End Get
        Set(value As String)
            _NumError = value
        End Set
    End Property
#End Region

End Class

Public Class clsSolictds
    Inherits List(Of clsSolic)

#Region "Variables"
    Dim dataManager As New clsManejaBD
    Dim tempResult As New DataSet
#End Region

    ''' <summary>
    ''' Devuelve el mismo objeto dependiendo de los parametros proporcionados
    ''' </summary>
    ''' <param name="NoSolicitud">Número de solicitud</param>
    ''' <param name="FechaInicio">Fecha de inicio de la solicitud, puede ser mayor a esta fecha y menor a la siguiente</param>
    ''' <param name="FechaFin">Fecha final de la solicitud, puede ser menor a esta fecha y mayor a la anterior</param>
    ''' <param name="NombreCliente">Nombre de solicitante</param>
    ''' <param name="RFCCliente">RFC del solicitante</param>
    ''' <param name="Estatus">Estado de la solicitud, siempre está en 2 para obtener sólo activas</param>
    ''' <returns>Regresa el mismo objeto poblado con elementos clsSolic</returns>
    ''' <remarks></remarks>
    Public Function obtenerSolicitudes(ByVal NoSolicitud As Integer, _
                                        ByVal FechaInicio As String, _
                                        ByVal FechaFin As String, _
                                        ByVal NombreCliente As String, _
                                        ByVal RFCCliente As String, _
                                        Optional Opcion As Integer = 0, _
                                        Optional Estatus As Integer = 2) As clsSolictds
        dataManager = New clsManejaBD()

        If NoSolicitud > 0 Then
            dataManager.AgregaParametro("@PDK_ID_SECCCERO", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(NoSolicitud))
        End If
        If FechaInicio <> String.Empty Then
            dataManager.AgregaParametro("@FECHA_INICIO", ProdeskNet.BD.TipoDato.Cadena, Convert.ToString(FechaInicio))
        End If
        If FechaFin <> String.Empty Then
            dataManager.AgregaParametro("@FECHA_FIN", ProdeskNet.BD.TipoDato.Cadena, Convert.ToString(FechaFin))
        End If
        If NombreCliente <> String.Empty Then
            dataManager.AgregaParametro("@NOMBRE_CLIENTE", ProdeskNet.BD.TipoDato.Cadena, Convert.ToString(NombreCliente))
        End If
        If RFCCliente <> String.Empty Then
            dataManager.AgregaParametro("@RFC_CLIENTE", ProdeskNet.BD.TipoDato.Cadena, Convert.ToString(RFCCliente))
        End If
        If Estatus <> 0 Then
            dataManager.AgregaParametro("@ESTATUS", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(Estatus))
        End If
        If Opcion <> 0 Then
            dataManager.AgregaParametro("@OPCION", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(Opcion))
        End If

        tempResult = dataManager.EjecutaStoredProcedure("getSolicitudes")   
        If Not tempResult.Tables Is Nothing And tempResult.Tables.Count > 0 Then
            For Each itemRow As DataRow In tempResult.Tables(0).Rows
                Me.Add(New clsSolic With { _
                .NumeroSolicitud = Convert.ToInt32(itemRow("NUMERO_SOLICITUD").ToString()), _
                .NombreCliente = itemRow("NOMBRE_CLIENTE").ToString(), _
                .RFC = itemRow("RFC_CLIENTE").ToString(), _
                .NombreAgencia = itemRow("NOMBRE_AGENCIA").ToString(), _
                .Alianza = itemRow("ALIANZA").ToString(), _
                .NombreCanal = itemRow("NOMBRE_CANAL").ToString(), _
                .NombreMarca = itemRow("NOMBRE_MARCA").ToString(), _
                .NombreSubmarca = itemRow("NOMBRE_SUBMARCA").ToString(), _
                .MontoSolicitado = itemRow("MONTO_SOLICITADO").ToString(), _
                .TareaActual = itemRow("TAREA_ACTUAL").ToString(), _
                .StatusTareaActual = itemRow("STATUS_TAREA").ToString() _
                })

            Next
        Else
            Return New clsSolictds
        End If
        Return Me
    End Function

    Public Function obtenerSolicitudesBiometrico(ByVal NoSolicitud As Integer, _
                                                 ByVal Opcion As String) As clsSolictds
        dataManager = New clsManejaBD()

        dataManager.AgregaParametro("@PDK_ID_SECCCERO", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(NoSolicitud))
        dataManager.AgregaParametro("@OPCION", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(Opcion))
        tempResult = dataManager.EjecutaStoredProcedure("crud_Biometrico_SP")
        If Not tempResult.Tables Is Nothing And tempResult.Tables.Count > 0 Then
            For Each itemRow As DataRow In tempResult.Tables(0).Rows
                Me.Add(New clsSolic With { _
                .NumeroSolicitud = Convert.ToInt32(itemRow("PDK_ID_SECCCERO").ToString()), _
                .Mensaje = itemRow("STATUS").ToString(), _
                .PantallaAut = itemRow("PANT_AUT").ToString(), _
                .PantallaError = itemRow("PANT_QUE").ToString(), _
                .NumError = itemRow("NUM_QUE").ToString() _
                })

            Next
        Else
            Return New clsSolictds
        End If
        Return Me
    End Function
   
End Class