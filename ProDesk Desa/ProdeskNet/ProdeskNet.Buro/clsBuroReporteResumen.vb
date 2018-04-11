Imports System.Text

Public Class clsBuroReporteResumen
'-------------------------- INICIO PDK_BURO_REPORTE_RESUMEN-------------------------- 
#Region "Variables"
    Private intPDK_ID_BURO_REPORTE_RESUMEN As Integer = 0
    Private intPDK_ID_BURO As Integer = 0
    Private strPDK_BUR_REP_RES_RS_FEC_INGRESO As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_MOP7 As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_MOP6 As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_MOP5 As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_MOP4 As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_MOP3 As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_MOP2 As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_MOP1 As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_MOP0 As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_MOPUR As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_CTAS As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_CTAS_FIJ_HIPO As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_CTAS_REV_AB As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_CTAS_CERRADAS As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_CTAS_NEG As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_CTA_MOP_HIST_NEG As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_CTA_DISP As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_SOL_ULT_6M As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_IND_NVA_DIR As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_MSJ_ALERTA As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_IND_DEC_CONSUMIDOR As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_MONEDA As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_TOT_CRED_REV_AB As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_TOT_LIM_CRED As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_TOT_SDO_ACT_REV_AB As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_TOT_SDO_VEN_REV_AB As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_TOT_PAG_REV_AB As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_PJE_LIM_CRED_UT_REV As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_TOT_CRED_FIJ_HIP As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_TOT_SDO_ACT_FIJ_HIP As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_TOT_SDO_VENC_FIJ_HIP As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_TOT_PAG_FIJ_HIP As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_MOP96 As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_MOP97 As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_MOP99 As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_ANTIGUA As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_SOL_REPORTES As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_FEC_REC_SOL_REPORTE As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_CTA_COBRANZA As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC_COB As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_NUM_SOL_DESP_COB As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_FEC_SOL_MAS_REC_DESP_COB As String = String.Empty
    Private strPDK_BUR_REP_RES_RS_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
#End Region
#Region "Propiedades"
    Public Property PDK_ID_BURO_REPORTE_RESUMEN() As Integer
        Get
            Return intPDK_ID_BURO_REPORTE_RESUMEN
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO_REPORTE_RESUMEN = value
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
    Public Property PDK_BUR_REP_RES_RS_FEC_INGRESO() As String
        Get
            Return strPDK_BUR_REP_RES_RS_FEC_INGRESO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_FEC_INGRESO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_MOP7() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_MOP7
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_MOP7 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_MOP6() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_MOP6
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_MOP6 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_MOP5() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_MOP5
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_MOP5 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_MOP4() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_MOP4
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_MOP4 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_MOP3() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_MOP3
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_MOP3 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_MOP2() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_MOP2
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_MOP2 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_MOP1() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_MOP1
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_MOP1 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_MOP0() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_MOP0
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_MOP0 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_MOPUR() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_MOPUR
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_MOPUR = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_CTAS() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_CTAS
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_CTAS = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_CTAS_FIJ_HIPO() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_CTAS_FIJ_HIPO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_CTAS_FIJ_HIPO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_CTAS_REV_AB() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_CTAS_REV_AB
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_CTAS_REV_AB = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_CTAS_CERRADAS() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_CTAS_CERRADAS
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_CTAS_CERRADAS = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_CTAS_NEG() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_CTAS_NEG
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_CTAS_NEG = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_CTA_MOP_HIST_NEG() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_CTA_MOP_HIST_NEG
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_CTA_MOP_HIST_NEG = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_CTA_DISP() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_CTA_DISP
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_CTA_DISP = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_SOL_ULT_6M() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_SOL_ULT_6M
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_SOL_ULT_6M = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_IND_NVA_DIR() As String
        Get
            Return strPDK_BUR_REP_RES_RS_IND_NVA_DIR
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_IND_NVA_DIR = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_MSJ_ALERTA() As String
        Get
            Return strPDK_BUR_REP_RES_RS_MSJ_ALERTA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_MSJ_ALERTA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_IND_DEC_CONSUMIDOR() As String
        Get
            Return strPDK_BUR_REP_RES_RS_IND_DEC_CONSUMIDOR
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_IND_DEC_CONSUMIDOR = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_MONEDA() As String
        Get
            Return strPDK_BUR_REP_RES_RS_MONEDA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_MONEDA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_TOT_CRED_REV_AB() As String
        Get
            Return strPDK_BUR_REP_RES_RS_TOT_CRED_REV_AB
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_TOT_CRED_REV_AB = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_TOT_LIM_CRED() As String
        Get
            Return strPDK_BUR_REP_RES_RS_TOT_LIM_CRED
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_TOT_LIM_CRED = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_TOT_SDO_ACT_REV_AB() As String
        Get
            Return strPDK_BUR_REP_RES_RS_TOT_SDO_ACT_REV_AB
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_TOT_SDO_ACT_REV_AB = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_TOT_SDO_VEN_REV_AB() As String
        Get
            Return strPDK_BUR_REP_RES_RS_TOT_SDO_VEN_REV_AB
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_TOT_SDO_VEN_REV_AB = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_TOT_PAG_REV_AB() As String
        Get
            Return strPDK_BUR_REP_RES_RS_TOT_PAG_REV_AB
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_TOT_PAG_REV_AB = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_PJE_LIM_CRED_UT_REV() As String
        Get
            Return strPDK_BUR_REP_RES_RS_PJE_LIM_CRED_UT_REV
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_PJE_LIM_CRED_UT_REV = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_TOT_CRED_FIJ_HIP() As String
        Get
            Return strPDK_BUR_REP_RES_RS_TOT_CRED_FIJ_HIP
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_TOT_CRED_FIJ_HIP = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_TOT_SDO_ACT_FIJ_HIP() As String
        Get
            Return strPDK_BUR_REP_RES_RS_TOT_SDO_ACT_FIJ_HIP
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_TOT_SDO_ACT_FIJ_HIP = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_TOT_SDO_VENC_FIJ_HIP() As String
        Get
            Return strPDK_BUR_REP_RES_RS_TOT_SDO_VENC_FIJ_HIP
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_TOT_SDO_VENC_FIJ_HIP = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_TOT_PAG_FIJ_HIP() As String
        Get
            Return strPDK_BUR_REP_RES_RS_TOT_PAG_FIJ_HIP
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_TOT_PAG_FIJ_HIP = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_MOP96() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_MOP96
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_MOP96 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_MOP97() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_MOP97
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_MOP97 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_MOP99() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_MOP99
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_MOP99 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_FEC_CTA_MAS_ANTIGUA() As String
        Get
            Return strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_ANTIGUA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_ANTIGUA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC() As String
        Get
            Return strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_SOL_REPORTES() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_SOL_REPORTES
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_SOL_REPORTES = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_FEC_REC_SOL_REPORTE() As String
        Get
            Return strPDK_BUR_REP_RES_RS_FEC_REC_SOL_REPORTE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_FEC_REC_SOL_REPORTE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_CTA_COBRANZA() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_CTA_COBRANZA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_CTA_COBRANZA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC_COB() As String
        Get
            Return strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC_COB
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC_COB = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_NUM_SOL_DESP_COB() As String
        Get
            Return strPDK_BUR_REP_RES_RS_NUM_SOL_DESP_COB
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_NUM_SOL_DESP_COB = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_FEC_SOL_MAS_REC_DESP_COB() As String
        Get
            Return strPDK_BUR_REP_RES_RS_FEC_SOL_MAS_REC_DESP_COB
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_FEC_SOL_MAS_REC_DESP_COB = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RES_RS_MODIF() As String
        Get
            Return strPDK_BUR_REP_RES_RS_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RES_RS_MODIF = value
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
            strSQL.Append(" PDK_ID_BURO_REPORTE_RESUMEN,")
            strSQL.Append(" PDK_ID_BURO,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_FEC_INGRESO,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_MOP7,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_MOP6,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_MOP5,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_MOP4,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_MOP3,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_MOP2,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_MOP1,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_MOP0,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_MOPUR,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_CTAS,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_CTAS_FIJ_HIPO,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_CTAS_REV_AB,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_CTAS_CERRADAS,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_CTAS_NEG,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_CTA_MOP_HIST_NEG,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_CTA_DISP,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_SOL_ULT_6M,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_IND_NVA_DIR,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_MSJ_ALERTA,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_IND_DEC_CONSUMIDOR,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_MONEDA,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_TOT_CRED_REV_AB,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_TOT_LIM_CRED,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_TOT_SDO_ACT_REV_AB,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_TOT_SDO_VEN_REV_AB,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_TOT_PAG_REV_AB,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_PJE_LIM_CRED_UT_REV,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_TOT_CRED_FIJ_HIP,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_TOT_SDO_ACT_FIJ_HIP,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_TOT_SDO_VENC_FIJ_HIP,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_TOT_PAG_FIJ_HIP,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_MOP96,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_MOP97,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_MOP99,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_FEC_CTA_MAS_ANTIGUA,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_SOL_REPORTES,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_FEC_REC_SOL_REPORTE,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_CTA_COBRANZA,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC_COB,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_NUM_SOL_DESP_COB,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_FEC_SOL_MAS_REC_DESP_COB,")
            strSQL.Append(" PDK_BUR_REP_RES_RS_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_RESUMEN")
            strSQL.Append(" WHERE PDK_ID_BURO_REPORTE_RESUMEN = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_BURO_REPORTE_RESUMEN = .Item("PDK_ID_BURO_REPORTE_RESUMEN")
                Me.intPDK_ID_BURO = .Item("PDK_ID_BURO")
                Me.strPDK_BUR_REP_RES_RS_FEC_INGRESO = .Item("PDK_BUR_REP_RES_RS_FEC_INGRESO")
                Me.strPDK_BUR_REP_RES_RS_NUM_MOP7 = .Item("PDK_BUR_REP_RES_RS_NUM_MOP7")
                Me.strPDK_BUR_REP_RES_RS_NUM_MOP6 = .Item("PDK_BUR_REP_RES_RS_NUM_MOP6")
                Me.strPDK_BUR_REP_RES_RS_NUM_MOP5 = .Item("PDK_BUR_REP_RES_RS_NUM_MOP5")
                Me.strPDK_BUR_REP_RES_RS_NUM_MOP4 = .Item("PDK_BUR_REP_RES_RS_NUM_MOP4")
                Me.strPDK_BUR_REP_RES_RS_NUM_MOP3 = .Item("PDK_BUR_REP_RES_RS_NUM_MOP3")
                Me.strPDK_BUR_REP_RES_RS_NUM_MOP2 = .Item("PDK_BUR_REP_RES_RS_NUM_MOP2")
                Me.strPDK_BUR_REP_RES_RS_NUM_MOP1 = .Item("PDK_BUR_REP_RES_RS_NUM_MOP1")
                Me.strPDK_BUR_REP_RES_RS_NUM_MOP0 = .Item("PDK_BUR_REP_RES_RS_NUM_MOP0")
                Me.strPDK_BUR_REP_RES_RS_NUM_MOPUR = .Item("PDK_BUR_REP_RES_RS_NUM_MOPUR")
                Me.strPDK_BUR_REP_RES_RS_NUM_CTAS = .Item("PDK_BUR_REP_RES_RS_NUM_CTAS")
                Me.strPDK_BUR_REP_RES_RS_NUM_CTAS_FIJ_HIPO = .Item("PDK_BUR_REP_RES_RS_NUM_CTAS_FIJ_HIPO")
                Me.strPDK_BUR_REP_RES_RS_NUM_CTAS_REV_AB = .Item("PDK_BUR_REP_RES_RS_NUM_CTAS_REV_AB")
                Me.strPDK_BUR_REP_RES_RS_NUM_CTAS_CERRADAS = .Item("PDK_BUR_REP_RES_RS_NUM_CTAS_CERRADAS")
                Me.strPDK_BUR_REP_RES_RS_NUM_CTAS_NEG = .Item("PDK_BUR_REP_RES_RS_NUM_CTAS_NEG")
                Me.strPDK_BUR_REP_RES_RS_NUM_CTA_MOP_HIST_NEG = .Item("PDK_BUR_REP_RES_RS_NUM_CTA_MOP_HIST_NEG")
                Me.strPDK_BUR_REP_RES_RS_NUM_CTA_DISP = .Item("PDK_BUR_REP_RES_RS_NUM_CTA_DISP")
                Me.strPDK_BUR_REP_RES_RS_NUM_SOL_ULT_6M = .Item("PDK_BUR_REP_RES_RS_NUM_SOL_ULT_6M")
                Me.strPDK_BUR_REP_RES_RS_IND_NVA_DIR = .Item("PDK_BUR_REP_RES_RS_IND_NVA_DIR")
                Me.strPDK_BUR_REP_RES_RS_MSJ_ALERTA = .Item("PDK_BUR_REP_RES_RS_MSJ_ALERTA")
                Me.strPDK_BUR_REP_RES_RS_IND_DEC_CONSUMIDOR = .Item("PDK_BUR_REP_RES_RS_IND_DEC_CONSUMIDOR")
                Me.strPDK_BUR_REP_RES_RS_MONEDA = .Item("PDK_BUR_REP_RES_RS_MONEDA")
                Me.strPDK_BUR_REP_RES_RS_TOT_CRED_REV_AB = .Item("PDK_BUR_REP_RES_RS_TOT_CRED_REV_AB")
                Me.strPDK_BUR_REP_RES_RS_TOT_LIM_CRED = .Item("PDK_BUR_REP_RES_RS_TOT_LIM_CRED")
                Me.strPDK_BUR_REP_RES_RS_TOT_SDO_ACT_REV_AB = .Item("PDK_BUR_REP_RES_RS_TOT_SDO_ACT_REV_AB")
                Me.strPDK_BUR_REP_RES_RS_TOT_SDO_VEN_REV_AB = .Item("PDK_BUR_REP_RES_RS_TOT_SDO_VEN_REV_AB")
                Me.strPDK_BUR_REP_RES_RS_TOT_PAG_REV_AB = .Item("PDK_BUR_REP_RES_RS_TOT_PAG_REV_AB")
                Me.strPDK_BUR_REP_RES_RS_PJE_LIM_CRED_UT_REV = .Item("PDK_BUR_REP_RES_RS_PJE_LIM_CRED_UT_REV")
                Me.strPDK_BUR_REP_RES_RS_TOT_CRED_FIJ_HIP = .Item("PDK_BUR_REP_RES_RS_TOT_CRED_FIJ_HIP")
                Me.strPDK_BUR_REP_RES_RS_TOT_SDO_ACT_FIJ_HIP = .Item("PDK_BUR_REP_RES_RS_TOT_SDO_ACT_FIJ_HIP")
                Me.strPDK_BUR_REP_RES_RS_TOT_SDO_VENC_FIJ_HIP = .Item("PDK_BUR_REP_RES_RS_TOT_SDO_VENC_FIJ_HIP")
                Me.strPDK_BUR_REP_RES_RS_TOT_PAG_FIJ_HIP = .Item("PDK_BUR_REP_RES_RS_TOT_PAG_FIJ_HIP")
                Me.strPDK_BUR_REP_RES_RS_NUM_MOP96 = .Item("PDK_BUR_REP_RES_RS_NUM_MOP96")
                Me.strPDK_BUR_REP_RES_RS_NUM_MOP97 = .Item("PDK_BUR_REP_RES_RS_NUM_MOP97")
                Me.strPDK_BUR_REP_RES_RS_NUM_MOP99 = .Item("PDK_BUR_REP_RES_RS_NUM_MOP99")
                Me.strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_ANTIGUA = .Item("PDK_BUR_REP_RES_RS_FEC_CTA_MAS_ANTIGUA")
                Me.strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC = .Item("PDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC")
                Me.strPDK_BUR_REP_RES_RS_NUM_SOL_REPORTES = .Item("PDK_BUR_REP_RES_RS_NUM_SOL_REPORTES")
                Me.strPDK_BUR_REP_RES_RS_FEC_REC_SOL_REPORTE = .Item("PDK_BUR_REP_RES_RS_FEC_REC_SOL_REPORTE")
                Me.strPDK_BUR_REP_RES_RS_NUM_CTA_COBRANZA = .Item("PDK_BUR_REP_RES_RS_NUM_CTA_COBRANZA")
                Me.strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC_COB = .Item("PDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC_COB")
                Me.strPDK_BUR_REP_RES_RS_NUM_SOL_DESP_COB = .Item("PDK_BUR_REP_RES_RS_NUM_SOL_DESP_COB")
                Me.strPDK_BUR_REP_RES_RS_FEC_SOL_MAS_REC_DESP_COB = .Item("PDK_BUR_REP_RES_RS_FEC_SOL_MAS_REC_DESP_COB")
                Me.strPDK_BUR_REP_RES_RS_MODIF = .Item("PDK_BUR_REP_RES_RS_MODIF")
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
            strSQL.Append(" A.PDK_ID_BURO_REPORTE_RESUMEN,")
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_FEC_INGRESO,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_MOP7,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_MOP6,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_MOP5,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_MOP4,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_MOP3,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_MOP2,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_MOP1,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_MOP0,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_MOPUR,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_CTAS,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_CTAS_FIJ_HIPO,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_CTAS_REV_AB,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_CTAS_CERRADAS,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_CTAS_NEG,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_CTA_MOP_HIST_NEG,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_CTA_DISP,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_SOL_ULT_6M,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_IND_NVA_DIR,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_MSJ_ALERTA,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_IND_DEC_CONSUMIDOR,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_MONEDA,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_TOT_CRED_REV_AB,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_TOT_LIM_CRED,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_TOT_SDO_ACT_REV_AB,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_TOT_SDO_VEN_REV_AB,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_TOT_PAG_REV_AB,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_PJE_LIM_CRED_UT_REV,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_TOT_CRED_FIJ_HIP,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_TOT_SDO_ACT_FIJ_HIP,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_TOT_SDO_VENC_FIJ_HIP,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_TOT_PAG_FIJ_HIP,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_MOP96,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_MOP97,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_MOP99,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_FEC_CTA_MAS_ANTIGUA,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_SOL_REPORTES,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_FEC_REC_SOL_REPORTE,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_CTA_COBRANZA,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC_COB,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_NUM_SOL_DESP_COB,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_FEC_SOL_MAS_REC_DESP_COB,")
            strSQL.Append(" A.PDK_BUR_REP_RES_RS_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_RESUMEN A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_BURO_REPORTE_RESUMEN")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_BURO_REPORTE_RESUMEN = 0 Then
                Me.intPDK_ID_BURO_REPORTE_RESUMEN = BD.ObtenConsecutivo("", "PDK_BURO_REPORTE_RESUMEN", Nothing)
                strSql = "INSERT INTO PDK_BURO_REPORTE_RESUMEN " & _
                                "(" & _
"PDK_ID_BURO_REPORTE_RESUMEN,PDK_ID_BURO,PDK_BUR_REP_RES_RS_FEC_INGRESO,PDK_BUR_REP_RES_RS_NUM_MOP7,PDK_BUR_REP_RES_RS_NUM_MOP6,PDK_BUR_REP_RES_RS_NUM_MOP5,PDK_BUR_REP_RES_RS_NUM_MOP4,PDK_BUR_REP_RES_RS_NUM_MOP3,PDK_BUR_REP_RES_RS_NUM_MOP2,PDK_BUR_REP_RES_RS_NUM_MOP1,PDK_BUR_REP_RES_RS_NUM_MOP0,PDK_BUR_REP_RES_RS_NUM_MOPUR,PDK_BUR_REP_RES_RS_NUM_CTAS,PDK_BUR_REP_RES_RS_NUM_CTAS_FIJ_HIPO,PDK_BUR_REP_RES_RS_NUM_CTAS_REV_AB,PDK_BUR_REP_RES_RS_NUM_CTAS_CERRADAS,PDK_BUR_REP_RES_RS_NUM_CTAS_NEG,PDK_BUR_REP_RES_RS_NUM_CTA_MOP_HIST_NEG,PDK_BUR_REP_RES_RS_NUM_CTA_DISP,PDK_BUR_REP_RES_RS_NUM_SOL_ULT_6M,PDK_BUR_REP_RES_RS_IND_NVA_DIR,PDK_BUR_REP_RES_RS_MSJ_ALERTA,PDK_BUR_REP_RES_RS_IND_DEC_CONSUMIDOR,PDK_BUR_REP_RES_RS_MONEDA,PDK_BUR_REP_RES_RS_TOT_CRED_REV_AB,PDK_BUR_REP_RES_RS_TOT_LIM_CRED,PDK_BUR_REP_RES_RS_TOT_SDO_ACT_REV_AB,PDK_BUR_REP_RES_RS_TOT_SDO_VEN_REV_AB,PDK_BUR_REP_RES_RS_TOT_PAG_REV_AB,PDK_BUR_REP_RES_RS_PJE_LIM_CRED_UT_REV,PDK_BUR_REP_RES_RS_TOT_CRED_FIJ_HIP,PDK_BUR_REP_RES_RS_TOT_SDO_ACT_FIJ_HIP,PDK_BUR_REP_RES_RS_TOT_SDO_VENC_FIJ_HIP,PDK_BUR_REP_RES_RS_TOT_PAG_FIJ_HIP,PDK_BUR_REP_RES_RS_NUM_MOP96,PDK_BUR_REP_RES_RS_NUM_MOP97,PDK_BUR_REP_RES_RS_NUM_MOP99,PDK_BUR_REP_RES_RS_FEC_CTA_MAS_ANTIGUA,PDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC,PDK_BUR_REP_RES_RS_NUM_SOL_REPORTES,PDK_BUR_REP_RES_RS_FEC_REC_SOL_REPORTE,PDK_BUR_REP_RES_RS_NUM_CTA_COBRANZA,PDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC_COB,PDK_BUR_REP_RES_RS_NUM_SOL_DESP_COB,PDK_BUR_REP_RES_RS_FEC_SOL_MAS_REC_DESP_COB,PDK_BUR_REP_RES_RS_MODIF,PDK_CLAVE_USUARIO,)" & _
" VALUES ( " & intPDK_ID_BURO_REPORTE_RESUMEN & ",  " & intPDK_ID_BURO & ", '" & strPDK_BUR_REP_RES_RS_FEC_INGRESO & "','" & strPDK_BUR_REP_RES_RS_NUM_MOP7 & "','" & strPDK_BUR_REP_RES_RS_NUM_MOP6 & "','" & strPDK_BUR_REP_RES_RS_NUM_MOP5 & "','" & strPDK_BUR_REP_RES_RS_NUM_MOP4 & "','" & strPDK_BUR_REP_RES_RS_NUM_MOP3 & "','" & strPDK_BUR_REP_RES_RS_NUM_MOP2 & "','" & strPDK_BUR_REP_RES_RS_NUM_MOP1 & "','" & strPDK_BUR_REP_RES_RS_NUM_MOP0 & "','" & strPDK_BUR_REP_RES_RS_NUM_MOPUR & "','" & strPDK_BUR_REP_RES_RS_NUM_CTAS & "','" & strPDK_BUR_REP_RES_RS_NUM_CTAS_FIJ_HIPO & "','" & strPDK_BUR_REP_RES_RS_NUM_CTAS_REV_AB & "','" & strPDK_BUR_REP_RES_RS_NUM_CTAS_CERRADAS & "','" & strPDK_BUR_REP_RES_RS_NUM_CTAS_NEG & "','" & strPDK_BUR_REP_RES_RS_NUM_CTA_MOP_HIST_NEG & "','" & strPDK_BUR_REP_RES_RS_NUM_CTA_DISP & "','" & strPDK_BUR_REP_RES_RS_NUM_SOL_ULT_6M & "','" & strPDK_BUR_REP_RES_RS_IND_NVA_DIR & "','" & strPDK_BUR_REP_RES_RS_MSJ_ALERTA & "','" & strPDK_BUR_REP_RES_RS_IND_DEC_CONSUMIDOR & "','" & strPDK_BUR_REP_RES_RS_MONEDA & "','" & strPDK_BUR_REP_RES_RS_TOT_CRED_REV_AB & "','" & strPDK_BUR_REP_RES_RS_TOT_LIM_CRED & "','" & strPDK_BUR_REP_RES_RS_TOT_SDO_ACT_REV_AB & "','" & strPDK_BUR_REP_RES_RS_TOT_SDO_VEN_REV_AB & "','" & strPDK_BUR_REP_RES_RS_TOT_PAG_REV_AB & "','" & strPDK_BUR_REP_RES_RS_PJE_LIM_CRED_UT_REV & "','" & strPDK_BUR_REP_RES_RS_TOT_CRED_FIJ_HIP & "','" & strPDK_BUR_REP_RES_RS_TOT_SDO_ACT_FIJ_HIP & "','" & strPDK_BUR_REP_RES_RS_TOT_SDO_VENC_FIJ_HIP & "','" & strPDK_BUR_REP_RES_RS_TOT_PAG_FIJ_HIP & "','" & strPDK_BUR_REP_RES_RS_NUM_MOP96 & "','" & strPDK_BUR_REP_RES_RS_NUM_MOP97 & "','" & strPDK_BUR_REP_RES_RS_NUM_MOP99 & "','" & strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_ANTIGUA & "','" & strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC & "','" & strPDK_BUR_REP_RES_RS_NUM_SOL_REPORTES & "','" & strPDK_BUR_REP_RES_RS_FEC_REC_SOL_REPORTE & "','" & strPDK_BUR_REP_RES_RS_NUM_CTA_COBRANZA & "','" & strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC_COB & "','" & strPDK_BUR_REP_RES_RS_NUM_SOL_DESP_COB & "','" & strPDK_BUR_REP_RES_RS_FEC_SOL_MAS_REC_DESP_COB & "','" & strPDK_BUR_REP_RES_RS_MODIF & "','" & strPDK_CLAVE_USUARIO & "', " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_BURO_REPORTE_RESUMEN ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_BURO_REPORTE_RESUMEN " & _
               "SET " & _
             " PDK_ID_BURO_REPORTE_RESUMEN = " & intPDK_ID_BURO_REPORTE_RESUMEN & ", " & _
             " PDK_ID_BURO = " & intPDK_ID_BURO & ", " & _
                " PDK_BUR_REP_RES_RS_FEC_INGRESO = '" & strPDK_BUR_REP_RES_RS_FEC_INGRESO & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_MOP7 = '" & strPDK_BUR_REP_RES_RS_NUM_MOP7 & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_MOP6 = '" & strPDK_BUR_REP_RES_RS_NUM_MOP6 & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_MOP5 = '" & strPDK_BUR_REP_RES_RS_NUM_MOP5 & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_MOP4 = '" & strPDK_BUR_REP_RES_RS_NUM_MOP4 & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_MOP3 = '" & strPDK_BUR_REP_RES_RS_NUM_MOP3 & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_MOP2 = '" & strPDK_BUR_REP_RES_RS_NUM_MOP2 & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_MOP1 = '" & strPDK_BUR_REP_RES_RS_NUM_MOP1 & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_MOP0 = '" & strPDK_BUR_REP_RES_RS_NUM_MOP0 & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_MOPUR = '" & strPDK_BUR_REP_RES_RS_NUM_MOPUR & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_CTAS = '" & strPDK_BUR_REP_RES_RS_NUM_CTAS & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_CTAS_FIJ_HIPO = '" & strPDK_BUR_REP_RES_RS_NUM_CTAS_FIJ_HIPO & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_CTAS_REV_AB = '" & strPDK_BUR_REP_RES_RS_NUM_CTAS_REV_AB & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_CTAS_CERRADAS = '" & strPDK_BUR_REP_RES_RS_NUM_CTAS_CERRADAS & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_CTAS_NEG = '" & strPDK_BUR_REP_RES_RS_NUM_CTAS_NEG & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_CTA_MOP_HIST_NEG = '" & strPDK_BUR_REP_RES_RS_NUM_CTA_MOP_HIST_NEG & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_CTA_DISP = '" & strPDK_BUR_REP_RES_RS_NUM_CTA_DISP & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_SOL_ULT_6M = '" & strPDK_BUR_REP_RES_RS_NUM_SOL_ULT_6M & "'," & _
                " PDK_BUR_REP_RES_RS_IND_NVA_DIR = '" & strPDK_BUR_REP_RES_RS_IND_NVA_DIR & "'," & _
                " PDK_BUR_REP_RES_RS_MSJ_ALERTA = '" & strPDK_BUR_REP_RES_RS_MSJ_ALERTA & "'," & _
                " PDK_BUR_REP_RES_RS_IND_DEC_CONSUMIDOR = '" & strPDK_BUR_REP_RES_RS_IND_DEC_CONSUMIDOR & "'," & _
                " PDK_BUR_REP_RES_RS_MONEDA = '" & strPDK_BUR_REP_RES_RS_MONEDA & "'," & _
                " PDK_BUR_REP_RES_RS_TOT_CRED_REV_AB = '" & strPDK_BUR_REP_RES_RS_TOT_CRED_REV_AB & "'," & _
                " PDK_BUR_REP_RES_RS_TOT_LIM_CRED = '" & strPDK_BUR_REP_RES_RS_TOT_LIM_CRED & "'," & _
                " PDK_BUR_REP_RES_RS_TOT_SDO_ACT_REV_AB = '" & strPDK_BUR_REP_RES_RS_TOT_SDO_ACT_REV_AB & "'," & _
                " PDK_BUR_REP_RES_RS_TOT_SDO_VEN_REV_AB = '" & strPDK_BUR_REP_RES_RS_TOT_SDO_VEN_REV_AB & "'," & _
                " PDK_BUR_REP_RES_RS_TOT_PAG_REV_AB = '" & strPDK_BUR_REP_RES_RS_TOT_PAG_REV_AB & "'," & _
                " PDK_BUR_REP_RES_RS_PJE_LIM_CRED_UT_REV = '" & strPDK_BUR_REP_RES_RS_PJE_LIM_CRED_UT_REV & "'," & _
                " PDK_BUR_REP_RES_RS_TOT_CRED_FIJ_HIP = '" & strPDK_BUR_REP_RES_RS_TOT_CRED_FIJ_HIP & "'," & _
                " PDK_BUR_REP_RES_RS_TOT_SDO_ACT_FIJ_HIP = '" & strPDK_BUR_REP_RES_RS_TOT_SDO_ACT_FIJ_HIP & "'," & _
                " PDK_BUR_REP_RES_RS_TOT_SDO_VENC_FIJ_HIP = '" & strPDK_BUR_REP_RES_RS_TOT_SDO_VENC_FIJ_HIP & "'," & _
                " PDK_BUR_REP_RES_RS_TOT_PAG_FIJ_HIP = '" & strPDK_BUR_REP_RES_RS_TOT_PAG_FIJ_HIP & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_MOP96 = '" & strPDK_BUR_REP_RES_RS_NUM_MOP96 & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_MOP97 = '" & strPDK_BUR_REP_RES_RS_NUM_MOP97 & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_MOP99 = '" & strPDK_BUR_REP_RES_RS_NUM_MOP99 & "'," & _
                " PDK_BUR_REP_RES_RS_FEC_CTA_MAS_ANTIGUA = '" & strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_ANTIGUA & "'," & _
                " PDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC = '" & strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_SOL_REPORTES = '" & strPDK_BUR_REP_RES_RS_NUM_SOL_REPORTES & "'," & _
                " PDK_BUR_REP_RES_RS_FEC_REC_SOL_REPORTE = '" & strPDK_BUR_REP_RES_RS_FEC_REC_SOL_REPORTE & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_CTA_COBRANZA = '" & strPDK_BUR_REP_RES_RS_NUM_CTA_COBRANZA & "'," & _
                " PDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC_COB = '" & strPDK_BUR_REP_RES_RS_FEC_CTA_MAS_REC_COB & "'," & _
                " PDK_BUR_REP_RES_RS_NUM_SOL_DESP_COB = '" & strPDK_BUR_REP_RES_RS_NUM_SOL_DESP_COB & "'," & _
                " PDK_BUR_REP_RES_RS_FEC_SOL_MAS_REC_DESP_COB = '" & strPDK_BUR_REP_RES_RS_FEC_SOL_MAS_REC_DESP_COB & "'," & _
                " PDK_BUR_REP_RES_RS_MODIF = '" & strPDK_BUR_REP_RES_RS_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "'," & _
             " WHERE PDK_ID_BURO_REPORTE_RESUMEN=  " & intPDK_ID_BURO_REPORTE_RESUMEN
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_BURO_REPORTE_RESUMEN")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_BURO_REPORTE_RESUMEN-------------------------- 

End Class
