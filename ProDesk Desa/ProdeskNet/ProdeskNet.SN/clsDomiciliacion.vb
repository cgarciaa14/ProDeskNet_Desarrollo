Imports System.Text
Imports System.Data
Public Class clsDomiciliacion
#Region "Trackers"
    'BBV-P-423 RQADM-23: AVH: 28/03/2017 Se crea Clase
    'BUG-PD-328: CGARCIA: 05/01/2017: SE AGREGA COLUMNA PARA GUARDAR EL NUMERO DE CLIENTE QUE AROJA EL WS DE LIGUE DE CUENTA
#End Region
#Region "Variables"
    Private intparametro As Integer = 0
    Private intID_SOLICITUD As Integer = 0
    Private strFOLIO_FISCA As String = ""
    Private intPDK_TIPO_CUENTA As Integer = 0
    Private intBANCO As Integer = 0
    Private strTITULAR_CUENTA As String = ""
    Private strCLABE As String = ""
    Private strNUMERO_TARJETA As String = ""
    Private strNUMERO_CUENTA As String = ""
    Private intUsuario As Integer = 0
    Private strErrDomiciliacion As String = ""
    Private strNumClienteWS As String = ""
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
    Public Property FolioFiscal() As String
        Get
            Return strFOLIO_FISCA
        End Get
        Set(ByVal value As String)
            strFOLIO_FISCA = value
        End Set
    End Property
    Public Property TipoCuenta() As Integer
        Get
            Return intPDK_TIPO_CUENTA
        End Get
        Set(ByVal value As Integer)
            intPDK_TIPO_CUENTA = value
        End Set
    End Property
    Public Property Banco() As Integer
        Get
            Return intBANCO
        End Get
        Set(ByVal value As Integer)
            intBANCO = value
        End Set
    End Property
    Public Property Titular() As String
        Get
            Return strTITULAR_CUENTA
        End Get
        Set(ByVal value As String)
            strTITULAR_CUENTA = value
        End Set
    End Property
    Public Property Clabe() As String
        Get
            Return strCLABE
        End Get
        Set(ByVal value As String)
            strCLABE = value
        End Set
    End Property
    Public Property NumeroTarjeta() As String
        Get
            Return strNUMERO_TARJETA
        End Get
        Set(ByVal value As String)
            strNUMERO_TARJETA = value
        End Set
    End Property
    Public Property NumeroCuenta() As String
        Get
            Return strNUMERO_CUENTA
        End Get
        Set(ByVal value As String)
            strNUMERO_CUENTA = value
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
    Public Property ErrorDomiciliacion() As String
        Get
            Return strErrDomiciliacion
        End Get
        Set(ByVal value As String)
            strErrDomiciliacion = value
        End Set
    End Property
    Public Property NumClienteWS() As String
        Get
            Return strNumClienteWS
        End Get
        Set(value As String)
            strNumClienteWS = value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Function Domiciliacion(ByVal intOper) As DataSet
        Dim BD As New ProdeskNet.BD.clsManejaBD

        Select Case intOper
            Case 1
                BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                If intID_SOLICITUD > 0 Then BD.AgregaParametro("@IDSOLICITUD", ProdeskNet.BD.TipoDato.Entero, intID_SOLICITUD)
            Case 2
                BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                If intID_SOLICITUD > 0 Then BD.AgregaParametro("@IDSOLICITUD", ProdeskNet.BD.TipoDato.Entero, intID_SOLICITUD)
                If strFOLIO_FISCA <> "" Then BD.AgregaParametro("@FOLIO_FISCAL", ProdeskNet.BD.TipoDato.Cadena, strFOLIO_FISCA)
                If intPDK_TIPO_CUENTA > 0 Then BD.AgregaParametro("@PDK_TIPO_CUENTA", ProdeskNet.BD.TipoDato.Entero, intPDK_TIPO_CUENTA)
                If intBANCO > 0 Then BD.AgregaParametro("@BANCO", ProdeskNet.BD.TipoDato.Entero, intBANCO)
                If strTITULAR_CUENTA <> "" Then BD.AgregaParametro("@TITULAR_CUENTA", ProdeskNet.BD.TipoDato.Cadena, strTITULAR_CUENTA)
                If strCLABE <> "" Then BD.AgregaParametro("@CLABE", ProdeskNet.BD.TipoDato.Cadena, strCLABE)
                If strNUMERO_TARJETA <> "" Then BD.AgregaParametro("@NUMERO_TARJETA", ProdeskNet.BD.TipoDato.Cadena, strNUMERO_TARJETA)
                If strNUMERO_CUENTA <> "" Then BD.AgregaParametro("@NUMERO_CUENTA", ProdeskNet.BD.TipoDato.Cadena, strNUMERO_CUENTA)
                If intUsuario > 0 Then BD.AgregaParametro("@PDK_CLAVE_USUARIO", ProdeskNet.BD.TipoDato.Entero, intUsuario)
                If strNumClienteWS <> "" Then BD.AgregaParametro("@NUMERO_CLIENTE_WS", ProdeskNet.BD.TipoDato.Cadena, strNumClienteWS)
        End Select
        Domiciliacion = BD.EjecutaStoredProcedure("spDomiciliacion")
        If (BD.ErrorBD) <> "" Then
            strErrDomiciliacion = BD.ErrorBD
        End If

    End Function
#End Region

End Class
