Imports System.Text
Imports System.Data
Public Class clsCatalogos
#Region "Trackers"
    'BBV-P-423 RQCONYFOR-01: AVH: 02/01/2017 Se crea Clase
    'BBV-P-423:RQADM-14: AVH: 17/03/2017 TAREA DE AVH 80 Cálculo de Ingresos
    'BBV-P-423 RQCONYFOR-07_2: AVH: 11/04/2017: Se agrega Variable intUSUARIO
    'BUG-PD-272: MGARCIA: 23/11/2017: Se agrego pantalla DetalleImpagos y su funcionalidad
#End Region
#Region "Variables"
    Private intID_SOLICITUD As Integer = 0
    Private intparametro As Integer = 0
    Private strErrCatalogos As String = ""
    Private intUSUARIO As Integer = 0
#End Region
#Region "Propiedades"

    Public Property ErrorCatalogos() As String
        Get
            Return strErrCatalogos
        End Get
        Set(ByVal value As String)
            strErrCatalogos = value
        End Set
    End Property
    Public Property Parametro() As Integer
        Get
            Return intparametro
        End Get
        Set(ByVal value As Integer)
            intparametro = value
        End Set
    End Property
    Public Property ID_USUARIO As Integer
        Get
            Return intUSUARIO
        End Get
        Set(value As Integer)
            intUSUARIO = value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Function Catalogos(ByVal intOper) As DataSet
        Dim BD As New ProdeskNet.BD.clsManejaBD

        BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
        If intparametro > 0 Then BD.AgregaParametro("@parametro", ProdeskNet.BD.TipoDato.Entero, intparametro)
        If intUSUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intUSUARIO)

        Catalogos = BD.EjecutaStoredProcedure("spCatalogos")
        If (BD.ErrorBD) <> "" Then
            strErrCatalogos = BD.ErrorBD
        End If

    End Function

    Public Function Catalogos_Sol(ByVal intOper, ByVal intparametro) As DataSet
        Dim BD As New ProdeskNet.BD.clsManejaBD

        BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
        If intparametro > 0 Then BD.AgregaParametro("@parametro", ProdeskNet.BD.TipoDato.Entero, intparametro)
        If intUSUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intUSUARIO)

        Catalogos_Sol = BD.EjecutaStoredProcedure("spCatalogos")
        If (BD.ErrorBD) <> "" Then
            strErrCatalogos = BD.ErrorBD
        End If

    End Function
#End Region
End Class
