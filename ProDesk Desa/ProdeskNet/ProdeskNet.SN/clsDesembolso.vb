Imports System.Text
Imports System.Data
Public Class clsDesembolso
#Region "Trackers"
    'BBV-P-423 RQCONYFOR-07_2: AVH: 11/04/2017: Se crea clase

#End Region
#Region "Variables"
    Private intparametro As Integer = 0
    Private intID_SOLICITUD As Integer = 0
    Private intUsuario As Integer = 0
    Private strErrDesembolso As String = ""
#End Region
#Region "Propiedades"
    Public Property Parametro() As Integer
        Get
            Return intparametro
        End Get
        Set(ByVal value As Integer)
            intparametro = value
        End Set
    End Property
    Public Property Solicitud() As Integer
        Get
            Return intID_SOLICITUD
        End Get
        Set(ByVal value As Integer)
            intID_SOLICITUD = value
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
    Public Property ErrorDesembolso() As String
        Get
            Return strErrDesembolso
        End Get
        Set(ByVal value As String)
            strErrDesembolso = value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Function Desembolso(ByVal intOper) As DataSet
        Dim BD As New ProdeskNet.BD.clsManejaBD

        Select Case intOper
            Case 1
                BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                If intID_SOLICITUD > 0 Then BD.AgregaParametro("@IDSOLICITUD", ProdeskNet.BD.TipoDato.Entero, intID_SOLICITUD)
            Case 2
                BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                If intID_SOLICITUD > 0 Then BD.AgregaParametro("@IDSOLICITUD", ProdeskNet.BD.TipoDato.Entero, intID_SOLICITUD)
                If intUsuario > 0 Then BD.AgregaParametro("@PDK_CLAVE_USUARIO", ProdeskNet.BD.TipoDato.Entero, intUsuario)

        End Select
        Desembolso = BD.EjecutaStoredProcedure("spDesembolso")
        If (BD.ErrorBD) <> "" Then
            strErrDesembolso = BD.ErrorBD
        End If

    End Function
#End Region
End Class
