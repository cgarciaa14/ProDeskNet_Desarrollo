Imports System.Text

Public Class clsBuroReporteCuentas
'-------------------------- INICIO PDK_BURO_REPORTE_CUENTAS-------------------------- 
#Region "Variables"
    Private intPDK_ID_BURO_REPORTE_CUENTAS As Integer = 0
    Private intPDK_ID_BURO As Integer = 0
    Private strPDK_BUR_REP_CTA_TL_FEC_ACTUALIZACION As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_REG_IMPUGNADO As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_CVE_OTORGANTE As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_NOM_OTORGANTE As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_TEL_OTORGANTE As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_NUM_CTA_ACTUAL As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_IND_RESP_CTA As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_TIPO_CUENTA As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_MONEDA As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_PROP_VALUACION As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_NUM_PAGOS As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_FREC_PAGOS As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_MTO_PAGAR As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_FEC_APERTURA As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_FEC_ULT_PAGO As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_FEC_ULT_COMPRA As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_FEC_CIERRE_CTA As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_FEC_REPORTE As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_MODO_REPORTAR As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_FEC_CTA_SDO_CERO As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_GARANTIA As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_CRED_MAX As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_SDO_ACTUAL As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_LIM_CRED As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_SDO_VENCIDO As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_NUM_PAG_VEN As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_FOR_PAGO_ACTUAL As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_HISTORICO_PAGOS As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_FEC_REC_HIST_PAGOS As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_FEC_ANT_HIST_PAGOS As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_CVE_OBS As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_TOT_PAG_REP As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP2 As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP3 As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP4 As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP5 As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_SDO_MORA As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_FEC_SDO_MORA As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_MOP_HIST_MORA As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_FEC_INI_FIN_REEST As String = String.Empty
    Private strPDK_BUR_REP_CTA_TL_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
#End Region
#Region "Propiedades"
    Public Property PDK_ID_BURO_REPORTE_CUENTAS() As Integer
        Get
            Return intPDK_ID_BURO_REPORTE_CUENTAS
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO_REPORTE_CUENTAS = value
        End Set
    End Property
    Public Property PDK_ID_BURO() As Integer
        Get
            Return intPDK_ID_BURO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_FEC_ACTUALIZACION() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_FEC_ACTUALIZACION
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_FEC_ACTUALIZACION = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_REG_IMPUGNADO() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_REG_IMPUGNADO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_REG_IMPUGNADO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_CVE_OTORGANTE() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_CVE_OTORGANTE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_CVE_OTORGANTE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_NOM_OTORGANTE() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_NOM_OTORGANTE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_NOM_OTORGANTE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_TEL_OTORGANTE() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_TEL_OTORGANTE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_TEL_OTORGANTE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_NUM_CTA_ACTUAL() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_NUM_CTA_ACTUAL
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_NUM_CTA_ACTUAL = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_IND_RESP_CTA() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_IND_RESP_CTA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_IND_RESP_CTA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_TIPO_CUENTA() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_TIPO_CUENTA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_TIPO_CUENTA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_MONEDA() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_MONEDA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_MONEDA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_PROP_VALUACION() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_PROP_VALUACION
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_PROP_VALUACION = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_NUM_PAGOS() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_NUM_PAGOS
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_NUM_PAGOS = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_FREC_PAGOS() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_FREC_PAGOS
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_FREC_PAGOS = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_MTO_PAGAR() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_MTO_PAGAR
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_MTO_PAGAR = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_FEC_APERTURA() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_FEC_APERTURA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_FEC_APERTURA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_FEC_ULT_PAGO() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_FEC_ULT_PAGO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_FEC_ULT_PAGO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_FEC_ULT_COMPRA() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_FEC_ULT_COMPRA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_FEC_ULT_COMPRA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_FEC_CIERRE_CTA() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_FEC_CIERRE_CTA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_FEC_CIERRE_CTA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_FEC_REPORTE() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_FEC_REPORTE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_FEC_REPORTE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_MODO_REPORTAR() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_MODO_REPORTAR
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_MODO_REPORTAR = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_FEC_CTA_SDO_CERO() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_FEC_CTA_SDO_CERO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_FEC_CTA_SDO_CERO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_GARANTIA() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_GARANTIA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_GARANTIA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_CRED_MAX() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_CRED_MAX
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_CRED_MAX = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_SDO_ACTUAL() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_SDO_ACTUAL
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_SDO_ACTUAL = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_LIM_CRED() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_LIM_CRED
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_LIM_CRED = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_SDO_VENCIDO() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_SDO_VENCIDO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_SDO_VENCIDO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_NUM_PAG_VEN() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_NUM_PAG_VEN
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_NUM_PAG_VEN = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_FOR_PAGO_ACTUAL() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_FOR_PAGO_ACTUAL
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_FOR_PAGO_ACTUAL = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_HISTORICO_PAGOS() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_HISTORICO_PAGOS
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_HISTORICO_PAGOS = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_FEC_REC_HIST_PAGOS() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_FEC_REC_HIST_PAGOS
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_FEC_REC_HIST_PAGOS = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_FEC_ANT_HIST_PAGOS() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_FEC_ANT_HIST_PAGOS
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_FEC_ANT_HIST_PAGOS = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_CVE_OBS() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_CVE_OBS
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_CVE_OBS = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_TOT_PAG_REP() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_TOT_PAG_REP
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_TOT_PAG_REP = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_TOT_PAG_MOP2() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP2
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP2 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_TOT_PAG_MOP3() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP3
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP3 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_TOT_PAG_MOP4() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP4
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP4 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_TOT_PAG_MOP5() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP5
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP5 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_SDO_MORA() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_SDO_MORA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_SDO_MORA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_FEC_SDO_MORA() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_FEC_SDO_MORA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_FEC_SDO_MORA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_MOP_HIST_MORA() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_MOP_HIST_MORA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_MOP_HIST_MORA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_FEC_INI_FIN_REEST() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_FEC_INI_FIN_REEST
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_FEC_INI_FIN_REEST = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CTA_TL_MODIF() As String
        Get
            Return strPDK_BUR_REP_CTA_TL_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CTA_TL_MODIF = value
        End Set
    End Property
    Public Property PDK_CLAVE_USUARIO() As String
        Get
            Return strPDK_CLAVE_USUARIO
        End Get
        Set(ByVal value As String)
            strPDK_CLAVE_USUARIO = value
        End Set
    End Property
#End Region
#Region "Metodos"
    Public Sub New(ByVal intRegistro As Integer)
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intRegistro = 0 Then Exit Sub
            strSQL.Append(" SELECT ")
            strSQL.Append(" PDK_ID_BURO_REPORTE_CUENTAS,")
            strSQL.Append(" PDK_ID_BURO,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_FEC_ACTUALIZACION,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_REG_IMPUGNADO,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_CVE_OTORGANTE,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_NOM_OTORGANTE,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_TEL_OTORGANTE,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_NUM_CTA_ACTUAL,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_IND_RESP_CTA,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_TIPO_CUENTA,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_MONEDA,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_PROP_VALUACION,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_NUM_PAGOS,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_FREC_PAGOS,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_MTO_PAGAR,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_FEC_APERTURA,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_FEC_ULT_PAGO,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_FEC_ULT_COMPRA,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_FEC_CIERRE_CTA,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_FEC_REPORTE,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_MODO_REPORTAR,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_FEC_CTA_SDO_CERO,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_GARANTIA,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_CRED_MAX,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_SDO_ACTUAL,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_LIM_CRED,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_SDO_VENCIDO,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_NUM_PAG_VEN,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_FOR_PAGO_ACTUAL,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_HISTORICO_PAGOS,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_FEC_REC_HIST_PAGOS,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_FEC_ANT_HIST_PAGOS,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_CVE_OBS,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_TOT_PAG_REP,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_TOT_PAG_MOP2,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_TOT_PAG_MOP3,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_TOT_PAG_MOP4,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_TOT_PAG_MOP5,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_SDO_MORA,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_FEC_SDO_MORA,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_MOP_HIST_MORA,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_FEC_INI_FIN_REEST,")
            strSQL.Append(" PDK_BUR_REP_CTA_TL_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_CUENTAS")
            strSQL.Append(" WHERE PDK_ID_BURO_REPORTE_CUENTAS = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_BURO_REPORTE_CUENTAS = .Item("PDK_ID_BURO_REPORTE_CUENTAS")
                Me.intPDK_ID_BURO = .Item("PDK_ID_BURO")
                Me.strPDK_BUR_REP_CTA_TL_FEC_ACTUALIZACION = .Item("PDK_BUR_REP_CTA_TL_FEC_ACTUALIZACION")
                Me.strPDK_BUR_REP_CTA_TL_REG_IMPUGNADO = .Item("PDK_BUR_REP_CTA_TL_REG_IMPUGNADO")
                Me.strPDK_BUR_REP_CTA_TL_CVE_OTORGANTE = .Item("PDK_BUR_REP_CTA_TL_CVE_OTORGANTE")
                Me.strPDK_BUR_REP_CTA_TL_NOM_OTORGANTE = .Item("PDK_BUR_REP_CTA_TL_NOM_OTORGANTE")
                Me.strPDK_BUR_REP_CTA_TL_TEL_OTORGANTE = .Item("PDK_BUR_REP_CTA_TL_TEL_OTORGANTE")
                Me.strPDK_BUR_REP_CTA_TL_NUM_CTA_ACTUAL = .Item("PDK_BUR_REP_CTA_TL_NUM_CTA_ACTUAL")
                Me.strPDK_BUR_REP_CTA_TL_IND_RESP_CTA = .Item("PDK_BUR_REP_CTA_TL_IND_RESP_CTA")
                Me.strPDK_BUR_REP_CTA_TL_TIPO_CUENTA = .Item("PDK_BUR_REP_CTA_TL_TIPO_CUENTA")
                Me.strPDK_BUR_REP_CTA_TL_MONEDA = .Item("PDK_BUR_REP_CTA_TL_MONEDA")
                Me.strPDK_BUR_REP_CTA_TL_PROP_VALUACION = .Item("PDK_BUR_REP_CTA_TL_PROP_VALUACION")
                Me.strPDK_BUR_REP_CTA_TL_NUM_PAGOS = .Item("PDK_BUR_REP_CTA_TL_NUM_PAGOS")
                Me.strPDK_BUR_REP_CTA_TL_FREC_PAGOS = .Item("PDK_BUR_REP_CTA_TL_FREC_PAGOS")
                Me.strPDK_BUR_REP_CTA_TL_MTO_PAGAR = .Item("PDK_BUR_REP_CTA_TL_MTO_PAGAR")
                Me.strPDK_BUR_REP_CTA_TL_FEC_APERTURA = .Item("PDK_BUR_REP_CTA_TL_FEC_APERTURA")
                Me.strPDK_BUR_REP_CTA_TL_FEC_ULT_PAGO = .Item("PDK_BUR_REP_CTA_TL_FEC_ULT_PAGO")
                Me.strPDK_BUR_REP_CTA_TL_FEC_ULT_COMPRA = .Item("PDK_BUR_REP_CTA_TL_FEC_ULT_COMPRA")
                Me.strPDK_BUR_REP_CTA_TL_FEC_CIERRE_CTA = .Item("PDK_BUR_REP_CTA_TL_FEC_CIERRE_CTA")
                Me.strPDK_BUR_REP_CTA_TL_FEC_REPORTE = .Item("PDK_BUR_REP_CTA_TL_FEC_REPORTE")
                Me.strPDK_BUR_REP_CTA_TL_MODO_REPORTAR = .Item("PDK_BUR_REP_CTA_TL_MODO_REPORTAR")
                Me.strPDK_BUR_REP_CTA_TL_FEC_CTA_SDO_CERO = .Item("PDK_BUR_REP_CTA_TL_FEC_CTA_SDO_CERO")
                Me.strPDK_BUR_REP_CTA_TL_GARANTIA = .Item("PDK_BUR_REP_CTA_TL_GARANTIA")
                Me.strPDK_BUR_REP_CTA_TL_CRED_MAX = .Item("PDK_BUR_REP_CTA_TL_CRED_MAX")
                Me.strPDK_BUR_REP_CTA_TL_SDO_ACTUAL = .Item("PDK_BUR_REP_CTA_TL_SDO_ACTUAL")
                Me.strPDK_BUR_REP_CTA_TL_LIM_CRED = .Item("PDK_BUR_REP_CTA_TL_LIM_CRED")
                Me.strPDK_BUR_REP_CTA_TL_SDO_VENCIDO = .Item("PDK_BUR_REP_CTA_TL_SDO_VENCIDO")
                Me.strPDK_BUR_REP_CTA_TL_NUM_PAG_VEN = .Item("PDK_BUR_REP_CTA_TL_NUM_PAG_VEN")
                Me.strPDK_BUR_REP_CTA_TL_FOR_PAGO_ACTUAL = .Item("PDK_BUR_REP_CTA_TL_FOR_PAGO_ACTUAL")
                Me.strPDK_BUR_REP_CTA_TL_HISTORICO_PAGOS = .Item("PDK_BUR_REP_CTA_TL_HISTORICO_PAGOS")
                Me.strPDK_BUR_REP_CTA_TL_FEC_REC_HIST_PAGOS = .Item("PDK_BUR_REP_CTA_TL_FEC_REC_HIST_PAGOS")
                Me.strPDK_BUR_REP_CTA_TL_FEC_ANT_HIST_PAGOS = .Item("PDK_BUR_REP_CTA_TL_FEC_ANT_HIST_PAGOS")
                Me.strPDK_BUR_REP_CTA_TL_CVE_OBS = .Item("PDK_BUR_REP_CTA_TL_CVE_OBS")
                Me.strPDK_BUR_REP_CTA_TL_TOT_PAG_REP = .Item("PDK_BUR_REP_CTA_TL_TOT_PAG_REP")
                Me.strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP2 = .Item("PDK_BUR_REP_CTA_TL_TOT_PAG_MOP2")
                Me.strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP3 = .Item("PDK_BUR_REP_CTA_TL_TOT_PAG_MOP3")
                Me.strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP4 = .Item("PDK_BUR_REP_CTA_TL_TOT_PAG_MOP4")
                Me.strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP5 = .Item("PDK_BUR_REP_CTA_TL_TOT_PAG_MOP5")
                Me.strPDK_BUR_REP_CTA_TL_SDO_MORA = .Item("PDK_BUR_REP_CTA_TL_SDO_MORA")
                Me.strPDK_BUR_REP_CTA_TL_FEC_SDO_MORA = .Item("PDK_BUR_REP_CTA_TL_FEC_SDO_MORA")
                Me.strPDK_BUR_REP_CTA_TL_MOP_HIST_MORA = .Item("PDK_BUR_REP_CTA_TL_MOP_HIST_MORA")
                Me.strPDK_BUR_REP_CTA_TL_FEC_INI_FIN_REEST = .Item("PDK_BUR_REP_CTA_TL_FEC_INI_FIN_REEST")
                Me.strPDK_BUR_REP_CTA_TL_MODIF = .Item("PDK_BUR_REP_CTA_TL_MODIF")
                Me.strPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
            End With
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try
    End Sub
    Public Shared Function ObtenTodos() As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_BURO_REPORTE_CUENTAS,")
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_FEC_ACTUALIZACION,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_REG_IMPUGNADO,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_CVE_OTORGANTE,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_NOM_OTORGANTE,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_TEL_OTORGANTE,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_NUM_CTA_ACTUAL,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_IND_RESP_CTA,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_TIPO_CUENTA,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_MONEDA,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_PROP_VALUACION,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_NUM_PAGOS,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_FREC_PAGOS,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_MTO_PAGAR,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_FEC_APERTURA,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_FEC_ULT_PAGO,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_FEC_ULT_COMPRA,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_FEC_CIERRE_CTA,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_FEC_REPORTE,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_MODO_REPORTAR,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_FEC_CTA_SDO_CERO,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_GARANTIA,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_CRED_MAX,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_SDO_ACTUAL,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_LIM_CRED,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_SDO_VENCIDO,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_NUM_PAG_VEN,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_FOR_PAGO_ACTUAL,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_HISTORICO_PAGOS,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_FEC_REC_HIST_PAGOS,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_FEC_ANT_HIST_PAGOS,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_CVE_OBS,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_TOT_PAG_REP,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_TOT_PAG_MOP2,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_TOT_PAG_MOP3,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_TOT_PAG_MOP4,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_TOT_PAG_MOP5,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_SDO_MORA,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_FEC_SDO_MORA,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_MOP_HIST_MORA,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_FEC_INI_FIN_REEST,")
            strSQL.Append(" A.PDK_BUR_REP_CTA_TL_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_CUENTAS A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_BURO_REPORTE_CUENTAS")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_BURO_REPORTE_CUENTAS = 0 Then
                Me.intPDK_ID_BURO_REPORTE_CUENTAS = BD.ObtenConsecutivo("", "PDK_BURO_REPORTE_CUENTAS", Nothing)
                strSql = "INSERT INTO PDK_BURO_REPORTE_CUENTAS " & _
                                "(" & _
"PDK_ID_BURO_REPORTE_CUENTAS,PDK_ID_BURO,PDK_BUR_REP_CTA_TL_FEC_ACTUALIZACION,PDK_BUR_REP_CTA_TL_REG_IMPUGNADO,PDK_BUR_REP_CTA_TL_CVE_OTORGANTE,PDK_BUR_REP_CTA_TL_NOM_OTORGANTE,PDK_BUR_REP_CTA_TL_TEL_OTORGANTE,PDK_BUR_REP_CTA_TL_NUM_CTA_ACTUAL,PDK_BUR_REP_CTA_TL_IND_RESP_CTA,PDK_BUR_REP_CTA_TL_TIPO_CUENTA,PDK_BUR_REP_CTA_TL_MONEDA,PDK_BUR_REP_CTA_TL_PROP_VALUACION,PDK_BUR_REP_CTA_TL_NUM_PAGOS,PDK_BUR_REP_CTA_TL_FREC_PAGOS,PDK_BUR_REP_CTA_TL_MTO_PAGAR,PDK_BUR_REP_CTA_TL_FEC_APERTURA,PDK_BUR_REP_CTA_TL_FEC_ULT_PAGO,PDK_BUR_REP_CTA_TL_FEC_ULT_COMPRA,PDK_BUR_REP_CTA_TL_FEC_CIERRE_CTA,PDK_BUR_REP_CTA_TL_FEC_REPORTE,PDK_BUR_REP_CTA_TL_MODO_REPORTAR,PDK_BUR_REP_CTA_TL_FEC_CTA_SDO_CERO,PDK_BUR_REP_CTA_TL_GARANTIA,PDK_BUR_REP_CTA_TL_CRED_MAX,PDK_BUR_REP_CTA_TL_SDO_ACTUAL,PDK_BUR_REP_CTA_TL_LIM_CRED,PDK_BUR_REP_CTA_TL_SDO_VENCIDO,PDK_BUR_REP_CTA_TL_NUM_PAG_VEN,PDK_BUR_REP_CTA_TL_FOR_PAGO_ACTUAL,PDK_BUR_REP_CTA_TL_HISTORICO_PAGOS,PDK_BUR_REP_CTA_TL_FEC_REC_HIST_PAGOS,PDK_BUR_REP_CTA_TL_FEC_ANT_HIST_PAGOS,PDK_BUR_REP_CTA_TL_CVE_OBS,PDK_BUR_REP_CTA_TL_TOT_PAG_REP,PDK_BUR_REP_CTA_TL_TOT_PAG_MOP2,PDK_BUR_REP_CTA_TL_TOT_PAG_MOP3,PDK_BUR_REP_CTA_TL_TOT_PAG_MOP4,PDK_BUR_REP_CTA_TL_TOT_PAG_MOP5,PDK_BUR_REP_CTA_TL_SDO_MORA,PDK_BUR_REP_CTA_TL_FEC_SDO_MORA,PDK_BUR_REP_CTA_TL_MOP_HIST_MORA,PDK_BUR_REP_CTA_TL_FEC_INI_FIN_REEST,PDK_BUR_REP_CTA_TL_MODIF,PDK_CLAVE_USUARIO,)" & _
" VALUES ( " & intPDK_ID_BURO_REPORTE_CUENTAS & ",  " & intPDK_ID_BURO & ", '" & strPDK_BUR_REP_CTA_TL_FEC_ACTUALIZACION & "','" & strPDK_BUR_REP_CTA_TL_REG_IMPUGNADO & "','" & strPDK_BUR_REP_CTA_TL_CVE_OTORGANTE & "','" & strPDK_BUR_REP_CTA_TL_NOM_OTORGANTE & "','" & strPDK_BUR_REP_CTA_TL_TEL_OTORGANTE & "','" & strPDK_BUR_REP_CTA_TL_NUM_CTA_ACTUAL & "','" & strPDK_BUR_REP_CTA_TL_IND_RESP_CTA & "','" & strPDK_BUR_REP_CTA_TL_TIPO_CUENTA & "','" & strPDK_BUR_REP_CTA_TL_MONEDA & "','" & strPDK_BUR_REP_CTA_TL_PROP_VALUACION & "','" & strPDK_BUR_REP_CTA_TL_NUM_PAGOS & "','" & strPDK_BUR_REP_CTA_TL_FREC_PAGOS & "','" & strPDK_BUR_REP_CTA_TL_MTO_PAGAR & "','" & strPDK_BUR_REP_CTA_TL_FEC_APERTURA & "','" & strPDK_BUR_REP_CTA_TL_FEC_ULT_PAGO & "','" & strPDK_BUR_REP_CTA_TL_FEC_ULT_COMPRA & "','" & strPDK_BUR_REP_CTA_TL_FEC_CIERRE_CTA & "','" & strPDK_BUR_REP_CTA_TL_FEC_REPORTE & "','" & strPDK_BUR_REP_CTA_TL_MODO_REPORTAR & "','" & strPDK_BUR_REP_CTA_TL_FEC_CTA_SDO_CERO & "','" & strPDK_BUR_REP_CTA_TL_GARANTIA & "','" & strPDK_BUR_REP_CTA_TL_CRED_MAX & "','" & strPDK_BUR_REP_CTA_TL_SDO_ACTUAL & "','" & strPDK_BUR_REP_CTA_TL_LIM_CRED & "','" & strPDK_BUR_REP_CTA_TL_SDO_VENCIDO & "','" & strPDK_BUR_REP_CTA_TL_NUM_PAG_VEN & "','" & strPDK_BUR_REP_CTA_TL_FOR_PAGO_ACTUAL & "','" & strPDK_BUR_REP_CTA_TL_HISTORICO_PAGOS & "','" & strPDK_BUR_REP_CTA_TL_FEC_REC_HIST_PAGOS & "','" & strPDK_BUR_REP_CTA_TL_FEC_ANT_HIST_PAGOS & "','" & strPDK_BUR_REP_CTA_TL_CVE_OBS & "','" & strPDK_BUR_REP_CTA_TL_TOT_PAG_REP & "','" & strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP2 & "','" & strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP3 & "','" & strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP4 & "','" & strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP5 & "','" & strPDK_BUR_REP_CTA_TL_SDO_MORA & "','" & strPDK_BUR_REP_CTA_TL_FEC_SDO_MORA & "','" & strPDK_BUR_REP_CTA_TL_MOP_HIST_MORA & "','" & strPDK_BUR_REP_CTA_TL_FEC_INI_FIN_REEST & "','" & strPDK_BUR_REP_CTA_TL_MODIF & "','" & strPDK_CLAVE_USUARIO & "', " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_BURO_REPORTE_CUENTAS ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_BURO_REPORTE_CUENTAS " & _
               "SET " & _
             " PDK_ID_BURO_REPORTE_CUENTAS = " & intPDK_ID_BURO_REPORTE_CUENTAS & ", " & _
             " PDK_ID_BURO = " & intPDK_ID_BURO & ", " & _
                " PDK_BUR_REP_CTA_TL_FEC_ACTUALIZACION = '" & strPDK_BUR_REP_CTA_TL_FEC_ACTUALIZACION & "'," & _
                " PDK_BUR_REP_CTA_TL_REG_IMPUGNADO = '" & strPDK_BUR_REP_CTA_TL_REG_IMPUGNADO & "'," & _
                " PDK_BUR_REP_CTA_TL_CVE_OTORGANTE = '" & strPDK_BUR_REP_CTA_TL_CVE_OTORGANTE & "'," & _
                " PDK_BUR_REP_CTA_TL_NOM_OTORGANTE = '" & strPDK_BUR_REP_CTA_TL_NOM_OTORGANTE & "'," & _
                " PDK_BUR_REP_CTA_TL_TEL_OTORGANTE = '" & strPDK_BUR_REP_CTA_TL_TEL_OTORGANTE & "'," & _
                " PDK_BUR_REP_CTA_TL_NUM_CTA_ACTUAL = '" & strPDK_BUR_REP_CTA_TL_NUM_CTA_ACTUAL & "'," & _
                " PDK_BUR_REP_CTA_TL_IND_RESP_CTA = '" & strPDK_BUR_REP_CTA_TL_IND_RESP_CTA & "'," & _
                " PDK_BUR_REP_CTA_TL_TIPO_CUENTA = '" & strPDK_BUR_REP_CTA_TL_TIPO_CUENTA & "'," & _
                " PDK_BUR_REP_CTA_TL_MONEDA = '" & strPDK_BUR_REP_CTA_TL_MONEDA & "'," & _
                " PDK_BUR_REP_CTA_TL_PROP_VALUACION = '" & strPDK_BUR_REP_CTA_TL_PROP_VALUACION & "'," & _
                " PDK_BUR_REP_CTA_TL_NUM_PAGOS = '" & strPDK_BUR_REP_CTA_TL_NUM_PAGOS & "'," & _
                " PDK_BUR_REP_CTA_TL_FREC_PAGOS = '" & strPDK_BUR_REP_CTA_TL_FREC_PAGOS & "'," & _
                " PDK_BUR_REP_CTA_TL_MTO_PAGAR = '" & strPDK_BUR_REP_CTA_TL_MTO_PAGAR & "'," & _
                " PDK_BUR_REP_CTA_TL_FEC_APERTURA = '" & strPDK_BUR_REP_CTA_TL_FEC_APERTURA & "'," & _
                " PDK_BUR_REP_CTA_TL_FEC_ULT_PAGO = '" & strPDK_BUR_REP_CTA_TL_FEC_ULT_PAGO & "'," & _
                " PDK_BUR_REP_CTA_TL_FEC_ULT_COMPRA = '" & strPDK_BUR_REP_CTA_TL_FEC_ULT_COMPRA & "'," & _
                " PDK_BUR_REP_CTA_TL_FEC_CIERRE_CTA = '" & strPDK_BUR_REP_CTA_TL_FEC_CIERRE_CTA & "'," & _
                " PDK_BUR_REP_CTA_TL_FEC_REPORTE = '" & strPDK_BUR_REP_CTA_TL_FEC_REPORTE & "'," & _
                " PDK_BUR_REP_CTA_TL_MODO_REPORTAR = '" & strPDK_BUR_REP_CTA_TL_MODO_REPORTAR & "'," & _
                " PDK_BUR_REP_CTA_TL_FEC_CTA_SDO_CERO = '" & strPDK_BUR_REP_CTA_TL_FEC_CTA_SDO_CERO & "'," & _
                " PDK_BUR_REP_CTA_TL_GARANTIA = '" & strPDK_BUR_REP_CTA_TL_GARANTIA & "'," & _
                " PDK_BUR_REP_CTA_TL_CRED_MAX = '" & strPDK_BUR_REP_CTA_TL_CRED_MAX & "'," & _
                " PDK_BUR_REP_CTA_TL_SDO_ACTUAL = '" & strPDK_BUR_REP_CTA_TL_SDO_ACTUAL & "'," & _
                " PDK_BUR_REP_CTA_TL_LIM_CRED = '" & strPDK_BUR_REP_CTA_TL_LIM_CRED & "'," & _
                " PDK_BUR_REP_CTA_TL_SDO_VENCIDO = '" & strPDK_BUR_REP_CTA_TL_SDO_VENCIDO & "'," & _
                " PDK_BUR_REP_CTA_TL_NUM_PAG_VEN = '" & strPDK_BUR_REP_CTA_TL_NUM_PAG_VEN & "'," & _
                " PDK_BUR_REP_CTA_TL_FOR_PAGO_ACTUAL = '" & strPDK_BUR_REP_CTA_TL_FOR_PAGO_ACTUAL & "'," & _
                " PDK_BUR_REP_CTA_TL_HISTORICO_PAGOS = '" & strPDK_BUR_REP_CTA_TL_HISTORICO_PAGOS & "'," & _
                " PDK_BUR_REP_CTA_TL_FEC_REC_HIST_PAGOS = '" & strPDK_BUR_REP_CTA_TL_FEC_REC_HIST_PAGOS & "'," & _
                " PDK_BUR_REP_CTA_TL_FEC_ANT_HIST_PAGOS = '" & strPDK_BUR_REP_CTA_TL_FEC_ANT_HIST_PAGOS & "'," & _
                " PDK_BUR_REP_CTA_TL_CVE_OBS = '" & strPDK_BUR_REP_CTA_TL_CVE_OBS & "'," & _
                " PDK_BUR_REP_CTA_TL_TOT_PAG_REP = '" & strPDK_BUR_REP_CTA_TL_TOT_PAG_REP & "'," & _
                " PDK_BUR_REP_CTA_TL_TOT_PAG_MOP2 = '" & strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP2 & "'," & _
                " PDK_BUR_REP_CTA_TL_TOT_PAG_MOP3 = '" & strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP3 & "'," & _
                " PDK_BUR_REP_CTA_TL_TOT_PAG_MOP4 = '" & strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP4 & "'," & _
                " PDK_BUR_REP_CTA_TL_TOT_PAG_MOP5 = '" & strPDK_BUR_REP_CTA_TL_TOT_PAG_MOP5 & "'," & _
                " PDK_BUR_REP_CTA_TL_SDO_MORA = '" & strPDK_BUR_REP_CTA_TL_SDO_MORA & "'," & _
                " PDK_BUR_REP_CTA_TL_FEC_SDO_MORA = '" & strPDK_BUR_REP_CTA_TL_FEC_SDO_MORA & "'," & _
                " PDK_BUR_REP_CTA_TL_MOP_HIST_MORA = '" & strPDK_BUR_REP_CTA_TL_MOP_HIST_MORA & "'," & _
                " PDK_BUR_REP_CTA_TL_FEC_INI_FIN_REEST = '" & strPDK_BUR_REP_CTA_TL_FEC_INI_FIN_REEST & "'," & _
                " PDK_BUR_REP_CTA_TL_MODIF = '" & strPDK_BUR_REP_CTA_TL_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "'," & _
             " WHERE PDK_ID_BURO_REPORTE_CUENTAS=  " & intPDK_ID_BURO_REPORTE_CUENTAS
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_BURO_REPORTE_CUENTAS")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_BURO_REPORTE_CUENTAS-------------------------- 


End Class
