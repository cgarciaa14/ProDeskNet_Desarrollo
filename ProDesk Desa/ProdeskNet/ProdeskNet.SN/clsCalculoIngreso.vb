Imports System.Text
Imports System.Data

Public Class clsCalculoIngreso


#Region "Trackers"
    'BBV-P-423:RQADM-14: AVH: 17/03/2017 TAREA DE AVH 80 Cálculo de Ingresos
    'BBV-P-423-RQADM-09 JBB 04/04/2017 CORRECIONES CALCULO DE INGRESOS.
    'BUG-PD-55 JBEJAR 29/05/2017 CORRECIONES AL GUARDAR EL TOTAL CALCULO DE INGRESOS. 
    'BUG-PD-108 JBEJAR 28/06/2017 SE AGREGAN PROPIEDADES PARA GUARDAR LAS NUEVAS PERSEPCIONES. 
	'BUG-PD-124 ERODRIGUEZ 06/07/2017 Se agregó opción consulta de para reporte.
#End Region
#Region "Variables"

    Private intID_SOLICITUD As Integer = 0
    Private intTipoActividad As Integer = 0
    Private intPeriodoPago As Integer = 0
    Private intDiasPerPago As Integer = 0
    Private _Tipificacion As Integer = 0
    Private _TOTAL As Double = 0
    Private dbIngresoAsalariado As Double = 0
    Private dbIngresoNoAsalariado As Double = 0

    Private intTipoReciboAsalariado As Integer = 0
    Private intTipoReciboNoAsalariado As Integer = 0

    Private intRecibosAsalariado As Integer = 0
    Private intRecibosNoAsalariado As Integer = 0

    Private intUsuario As Integer = 0

    Private intNoPercepcion As Integer = 0
    Private dbCompronate1 As Double = 0
    Private dbCompronate2 As Double = 0
    Private dbCompronate3 As Double = 0
    Private dbCompronate4 As Double = 0
    Private dbCompronate5 As Double = 0
    Private _dbComprobante6 As Double = 0 'BUG-PD-108
    Private _dbComprobante7 As Double = 0
    Private _dbComprobante8 As Double = 0
    Private _dbComprobante9 As Double = 0
    Private _dbComprobante10 As Double = 0
    Private _dbComprobante11 As Double = 0
    Private _dbComprobante12 As Double = 0
    Private _dbComprobante13 As Double = 0
    Private _dbComprobante14 As Double = 0
    Private _dbComprobante15 As Double = 0 'BUG-PD-108
    Private strErrCalculoIngreso As String = ""
    Private strContrato As String = ""

#End Region

#Region "Propiedades"

    Public Property ID_SOLICITUD() As Integer
        Get
            Return intID_SOLICITUD
        End Get
        Set(ByVal value As Integer)
            intID_SOLICITUD = value
        End Set
    End Property

    Public Property ErrCalculoIngreso() As String
        Get
            Return strErrCalculoIngreso
        End Get
        Set(ByVal value As String)
            strErrCalculoIngreso = value
        End Set
    End Property
    Public Property TipoActividad() As Integer
        Get
            Return intTipoActividad
        End Get
        Set(ByVal value As Integer)
            intTipoActividad = value
        End Set
    End Property
    Public Property PeriodoPago() As Integer
        Get
            Return intPeriodoPago
        End Get
        Set(ByVal value As Integer)
            intPeriodoPago = value
        End Set
    End Property
    Public Property DiasPerPago() As Integer
        Get
            Return intDiasPerPago
        End Get
        Set(ByVal value As Integer)
            intDiasPerPago = value
        End Set
    End Property
    Public Property IngresoAsalariado() As Double
        Get
            Return dbIngresoAsalariado
        End Get
        Set(ByVal value As Double)
            dbIngresoAsalariado = value
        End Set
    End Property
    Public Property IngresoNoAsalariado() As Double
        Get
            Return dbIngresoNoAsalariado
        End Get
        Set(ByVal value As Double)
            dbIngresoNoAsalariado = value
        End Set
    End Property
    Public Property TipoReciboAsalariado() As Integer
        Get
            Return intTipoReciboAsalariado
        End Get
        Set(ByVal value As Integer)
            intTipoReciboAsalariado = value
        End Set
    End Property
    Public Property TipoReciboNoAsalariado() As Integer
        Get
            Return intTipoReciboNoAsalariado
        End Get
        Set(ByVal value As Integer)
            intTipoReciboNoAsalariado = value
        End Set
    End Property

    Public Property RecibosAsalariado() As Integer
        Get
            Return intRecibosAsalariado
        End Get
        Set(ByVal value As Integer)
            intRecibosAsalariado = value
        End Set
    End Property
    Public Property RecibosNoAsalariado() As Integer
        Get
            Return intRecibosNoAsalariado
        End Get
        Set(ByVal value As Integer)
            intRecibosNoAsalariado = value
        End Set
    End Property

    Public Property Usuario() As Integer
        Get
            Return intUsuario
        End Get
        Set(ByVal value As Integer)
            intUsuario = value
        End Set
    End Property

    Public Property Comprobante1() As Double
        Get
            Return dbCompronate1
        End Get
        Set(ByVal value As Double)
            dbCompronate1 = value
        End Set
    End Property
    Public Property Comprobante2() As Double
        Get
            Return dbCompronate2
        End Get
        Set(ByVal value As Double)
            dbCompronate2 = value
        End Set
    End Property
    Public Property Comprobante3() As Double
        Get
            Return dbCompronate3
        End Get
        Set(ByVal value As Double)
            dbCompronate3 = value
        End Set
    End Property
    Public Property Comprobante4() As Double
        Get
            Return dbCompronate4
        End Get
        Set(ByVal value As Double)
            dbCompronate4 = value
        End Set
    End Property
    Public Property Comprobante5() As Double
        Get
            Return dbCompronate5
        End Get
        Set(ByVal value As Double)
            dbCompronate5 = value
        End Set
    End Property
    
    Public Property Comprobante6 As Double
        Get
            Return _dbComprobante6
        End Get
        Set(value As Double)
            _dbComprobante6 = value
        End Set
    End Property
    Public Property Comprobante7 As Double
        Get
            Return _dbComprobante7
        End Get
        Set(value As Double)
            _dbComprobante7 = value
        End Set
    End Property
    Public Property Comprobante8 As Double
        Get
            Return _dbComprobante8
        End Get
        Set(value As Double)
            _dbComprobante8 = value
        End Set
    End Property
    Public Property Comprobante9 As Double
        Get
            Return _dbComprobante9
        End Get
        Set(value As Double)
            _dbComprobante9 = value
        End Set
    End Property
    Public Property Comprobante10 As Double
        Get
            Return _dbComprobante10
        End Get
        Set(value As Double)
            _dbComprobante10 = value
        End Set
    End Property
    Public Property Comprobante11 As Double
        Get
            Return _dbComprobante11
        End Get
        Set(value As Double)
            _dbComprobante11 = value
        End Set
    End Property
    Public Property Comprobante12 As Double
        Get
            Return _dbComprobante12
        End Get
        Set(value As Double)
            _dbComprobante12 = value
        End Set
    End Property
    Public Property Comprobante13 As Double
        Get
            Return _dbComprobante13
        End Get
        Set(value As Double)
            _dbComprobante13 = value
        End Set
    End Property
    Public Property Comprobante14 As Double
        Get
            Return _dbComprobante14
        End Get
        Set(value As Double)
            _dbComprobante14 = value
        End Set
    End Property
    Public Property Comprobante15 As Double
        Get
            Return _dbComprobante15
        End Get
        Set(value As Double)
            _dbComprobante15 = value
        End Set
    End Property
    Public Property Percepcion() As Integer
        Get
            Return intNoPercepcion
        End Get
        Set(ByVal value As Integer)
            intNoPercepcion = value
        End Set
    End Property

    Public Property Tipificacion As Integer
        Get
            Return _Tipificacion
        End Get
        Set(value As Integer)
            _Tipificacion = value
        End Set
    End Property
    Public Property TOTAL As Double
        Get
            Return _TOTAL
        End Get
        Set(value As Double)
            _TOTAL = value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Function CalculoIngreso(ByVal intOper) As DataSet
        Dim BD As New ProdeskNet.BD.clsManejaBD

        Select Case intOper
            Case 1
                BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                If intID_SOLICITUD > 0 Then BD.AgregaParametro("@IDSOLICITUD", ProdeskNet.BD.TipoDato.Entero, intID_SOLICITUD)
            Case 2
                BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                If intID_SOLICITUD > 0 Then BD.AgregaParametro("@IDSOLICITUD", ProdeskNet.BD.TipoDato.Entero, intID_SOLICITUD)
                If intTipoActividad > 0 Then BD.AgregaParametro("@TipoActividad", ProdeskNet.BD.TipoDato.Entero, intTipoActividad)
                If intPeriodoPago > 0 Then BD.AgregaParametro("@PeriodoPago", ProdeskNet.BD.TipoDato.Entero, intPeriodoPago)
                If intDiasPerPago > 0 Then BD.AgregaParametro("@DiasPerPago", ProdeskNet.BD.TipoDato.Entero, intDiasPerPago)
                If dbIngresoAsalariado > 0 Then BD.AgregaParametro("@IngresoAsalariado", ProdeskNet.BD.TipoDato.Flotante, dbIngresoAsalariado)
                If dbIngresoNoAsalariado > 0 Then BD.AgregaParametro("@IngresoNoAsalariado", ProdeskNet.BD.TipoDato.Flotante, dbIngresoNoAsalariado)
                If intTipoReciboAsalariado > 0 Then BD.AgregaParametro("@TipoReciboAsalariado", ProdeskNet.BD.TipoDato.Entero, intTipoReciboAsalariado)
                If intTipoReciboNoAsalariado > 0 Then BD.AgregaParametro("@TipoReciboNoAsalariado", ProdeskNet.BD.TipoDato.Entero, intTipoReciboNoAsalariado)
                If intRecibosAsalariado > 0 Then BD.AgregaParametro("@RecibosAsalariado", ProdeskNet.BD.TipoDato.Entero, intRecibosAsalariado)
                If intRecibosNoAsalariado > 0 Then BD.AgregaParametro("@RecibosNoAsalariado", ProdeskNet.BD.TipoDato.Entero, intRecibosNoAsalariado)
                If Tipificacion > 0 Then BD.AgregaParametro("@Tipificacion", ProdeskNet.BD.TipoDato.Entero, Tipificacion)
                If TOTAL > 0 Then BD.AgregaParametro("@Total", ProdeskNet.BD.TipoDato.Flotante, TOTAL)
                If intUsuario > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intUsuario)
            Case 3
                BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                If intID_SOLICITUD > 0 Then BD.AgregaParametro("@IDSOLICITUD", ProdeskNet.BD.TipoDato.Entero, intID_SOLICITUD)
                If intTipoActividad > 0 Then BD.AgregaParametro("@TipoActividad", ProdeskNet.BD.TipoDato.Entero, intTipoActividad)
                If intNoPercepcion > 0 Then BD.AgregaParametro("@Percepcion", ProdeskNet.BD.TipoDato.Entero, intNoPercepcion)
                If dbCompronate1 > 0 Then BD.AgregaParametro("@Comprobante1", ProdeskNet.BD.TipoDato.Flotante, dbCompronate1)
                If dbCompronate2 > 0 Then BD.AgregaParametro("@Comprobante2", ProdeskNet.BD.TipoDato.Flotante, dbCompronate2)
                If dbCompronate3 > 0 Then BD.AgregaParametro("@Comprobante3", ProdeskNet.BD.TipoDato.Flotante, dbCompronate3)
                If dbCompronate4 > 0 Then BD.AgregaParametro("@Comprobante4", ProdeskNet.BD.TipoDato.Flotante, dbCompronate4)
                If dbCompronate5 > 0 Then BD.AgregaParametro("@Comprobante5", ProdeskNet.BD.TipoDato.Flotante, dbCompronate5)
                If _dbComprobante6 > 0 Then BD.AgregaParametro("@Comprobante6", ProdeskNet.BD.TipoDato.Flotante, _dbComprobante6)
                If _dbComprobante7 > 0 Then BD.AgregaParametro("@Comprobante7", ProdeskNet.BD.TipoDato.Flotante, _dbComprobante7)
                If _dbComprobante8 > 0 Then BD.AgregaParametro("@Comprobante8", ProdeskNet.BD.TipoDato.Flotante, _dbComprobante8)
                If _dbComprobante9 > 0 Then BD.AgregaParametro("@Comprobante9", ProdeskNet.BD.TipoDato.Flotante, _dbComprobante9)
                If _dbComprobante10 > 0 Then BD.AgregaParametro("@Comprobante10", ProdeskNet.BD.TipoDato.Flotante, _dbComprobante10)
                If _dbComprobante11 > 0 Then BD.AgregaParametro("@Comprobante11", ProdeskNet.BD.TipoDato.Flotante, _dbComprobante11)
                If _dbComprobante12 > 0 Then BD.AgregaParametro("@Comprobante12", ProdeskNet.BD.TipoDato.Flotante, _dbComprobante12)
                If _dbComprobante13 > 0 Then BD.AgregaParametro("@Comprobante13", ProdeskNet.BD.TipoDato.Flotante, _dbComprobante13)
                If _dbComprobante14 > 0 Then BD.AgregaParametro("@Comprobante14", ProdeskNet.BD.TipoDato.Flotante, _dbComprobante14)
                If _dbComprobante15 > 0 Then BD.AgregaParametro("@Comprobante15", ProdeskNet.BD.TipoDato.Flotante, _dbComprobante15)
                If intUsuario > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intUsuario)
            Case 4
                BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                If intID_SOLICITUD > 0 Then BD.AgregaParametro("@IDSOLICITUD", ProdeskNet.BD.TipoDato.Entero, intID_SOLICITUD)
                If intUsuario > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intUsuario)
            Case 5
                BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                If intID_SOLICITUD > 0 Then BD.AgregaParametro("@IDSOLICITUD", ProdeskNet.BD.TipoDato.Entero, intID_SOLICITUD)
                If intUsuario > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intUsuario)

        End Select
        CalculoIngreso = BD.EjecutaStoredProcedure("sp_CalculoIngresos")
        If (BD.ErrorBD) <> "" Then
            strErrCalculoIngreso = BD.ErrorBD
        End If
    End Function
#End Region
End Class
