Imports System.Text
Imports System.Data

Public Class clsFacturacion
#Region "Trackers"
    'BBV-P-423 RQCONYFOR-01: AVH: 02/01/2017 Se crea Clase
    'BBV-P-423 RQCONYFOR-07: AVH: 18/01/2017 SE AGREGAN OPCIONES 3,4,6,7 EN ObtenDatosFact
    'BUG-PD-341: JMENDIETA: 16/01/2018: En el método ObtenDatosFact se la opcion numero 8.
#End Region
#Region "Variables"

    Private intID_SOLICITUD As Integer = 0
    Private strErrFacturacion As String = ""
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
    Public Property CONTRATO() As String
        Get
            Return strContrato
        End Get
        Set(ByVal value As String)
            strContrato = value
        End Set
    End Property
    Public Property ErrFacturacion() As String
        Get
            Return strErrFacturacion
        End Get
        Set(ByVal value As String)
            strErrFacturacion = value
        End Set
    End Property
#End Region

#Region "Metodos"
    'BUG-PD-341
    Public Function ObtenDatosFact(ByVal intOper) As DataSet
        Dim BD As New ProdeskNet.BD.clsManejaBD

        Select Case intOper
            Case 1
                BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                If intID_SOLICITUD > 0 Then BD.AgregaParametro("@IDSOLICITUD", ProdeskNet.BD.TipoDato.Entero, intID_SOLICITUD)
            Case 2
                BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                If intID_SOLICITUD > 0 Then BD.AgregaParametro("@IDSOLICITUD", ProdeskNet.BD.TipoDato.Entero, intID_SOLICITUD)
                If strContrato <> "" Then BD.AgregaParametro("@CTO", ProdeskNet.BD.TipoDato.Cadena, strContrato)
            Case 3, 4, 6, 7, 8
                BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                If intID_SOLICITUD > 0 Then BD.AgregaParametro("@IDSOLICITUD", ProdeskNet.BD.TipoDato.Entero, intID_SOLICITUD)



        End Select
        ObtenDatosFact = BD.EjecutaStoredProcedure("spFacturacion")
        If (BD.ErrorBD) <> "" Then
            strErrFacturacion = BD.ErrorBD
        End If
    End Function
#End Region


End Class
