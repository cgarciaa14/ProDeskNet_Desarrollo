Imports System.Text

Public Class clsBuroCreditoMatriz

'-------------------------- INICIO PDK_BURO_CREDITO_MATRIZ-------------------------- 
#Region "Variables"
    Private intPDK_ID_BURO_CREDITO_MATRIZ As Integer = 0
    Private intPDK_ID_BURO As Integer = 0
    Private strPDK_BUR_CRED_SOLICITUD As String = String.Empty
    Private strPDK_BUR_CRED_MAT_ESTATUS As String = String.Empty
    Private strPDK_BUR_CRED_MAT_TIPO As String = String.Empty
    Private strPDK_BUR_CRED_MAT_MONEDA As String = String.Empty
    Private strPDK_BUR_CRED_MAT_MOP_ACT As String = String.Empty
    Private strPDK_BUR_CRED_MAT_MOP_ALTO As String = String.Empty
    Private strPDK_BUR_CRED_MAT_CVE_COMENTARIO As String = String.Empty
    Private strPDK_BUR_CRED_MAT_LIM_CRED As String = String.Empty
    Private strPDK_BUR_CRED_MAT_MAX_LIM_CRED As String = String.Empty
    Private strPDK_BUR_CRED_MAT_SDO_ACTUAL As String = String.Empty
    Private strPDK_BUR_CRED_MAT_MTO_VENC As String = String.Empty
    Private strPDK_BUR_CRED_MAT_MTO_PAG As String = String.Empty
    Private strPDK_BUR_CRED_MAT_FEC_APERTURA As String = String.Empty
    Private strPDK_BUR_CRED_MAT_FEC_CIERRE As String = String.Empty
    Private strPDK_BUR_CRED_MAT_FREC_PAGO As String = String.Empty
    Private strPDK_BUR_CRED_MAT_RESP As String = String.Empty
    Private strPDK_BUR_CRED_MAT_TIPO_CONTRATO As String = String.Empty
    Private strPDK_BUR_CRED_MAT_NOM_OTORGANTE As String = String.Empty
    Private strPDK_BUR_CRED_MAT_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
#End Region
#Region "Propiedades"
    Public Property PDK_ID_BURO_CREDITO_MATRIZ() As Integer
        Get
            Return intPDK_ID_BURO_CREDITO_MATRIZ
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO_CREDITO_MATRIZ = value
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
    Public Property PDK_BUR_CRED_SOLICITUD() As String
        Get
            Return strPDK_BUR_CRED_SOLICITUD
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_SOLICITUD = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_ESTATUS() As String
        Get
            Return strPDK_BUR_CRED_MAT_ESTATUS
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_ESTATUS = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_TIPO() As String
        Get
            Return strPDK_BUR_CRED_MAT_TIPO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_TIPO = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_MONEDA() As String
        Get
            Return strPDK_BUR_CRED_MAT_MONEDA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_MONEDA = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_MOP_ACT() As String
        Get
            Return strPDK_BUR_CRED_MAT_MOP_ACT
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_MOP_ACT = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_MOP_ALTO() As String
        Get
            Return strPDK_BUR_CRED_MAT_MOP_ALTO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_MOP_ALTO = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_CVE_COMENTARIO() As String
        Get
            Return strPDK_BUR_CRED_MAT_CVE_COMENTARIO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_CVE_COMENTARIO = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_LIM_CRED() As String
        Get
            Return strPDK_BUR_CRED_MAT_LIM_CRED
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_LIM_CRED = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_MAX_LIM_CRED() As String
        Get
            Return strPDK_BUR_CRED_MAT_MAX_LIM_CRED
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_MAX_LIM_CRED = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_SDO_ACTUAL() As String
        Get
            Return strPDK_BUR_CRED_MAT_SDO_ACTUAL
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_SDO_ACTUAL = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_MTO_VENC() As String
        Get
            Return strPDK_BUR_CRED_MAT_MTO_VENC
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_MTO_VENC = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_MTO_PAG() As String
        Get
            Return strPDK_BUR_CRED_MAT_MTO_PAG
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_MTO_PAG = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_FEC_APERTURA() As String
        Get
            Return strPDK_BUR_CRED_MAT_FEC_APERTURA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_FEC_APERTURA = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_FEC_CIERRE() As String
        Get
            Return strPDK_BUR_CRED_MAT_FEC_CIERRE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_FEC_CIERRE = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_FREC_PAGO() As String
        Get
            Return strPDK_BUR_CRED_MAT_FREC_PAGO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_FREC_PAGO = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_RESP() As String
        Get
            Return strPDK_BUR_CRED_MAT_RESP
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_RESP = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_TIPO_CONTRATO() As String
        Get
            Return strPDK_BUR_CRED_MAT_TIPO_CONTRATO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_TIPO_CONTRATO = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_NOM_OTORGANTE() As String
        Get
            Return strPDK_BUR_CRED_MAT_NOM_OTORGANTE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_NOM_OTORGANTE = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MAT_MODIF() As String
        Get
            Return strPDK_BUR_CRED_MAT_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MAT_MODIF = value
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
            strSQL.Append(" PDK_ID_BURO_CREDITO_MATRIZ,")
            strSQL.Append(" PDK_ID_BURO,")
            strSQL.Append(" PDK_BUR_CRED_SOLICITUD,")
            strSQL.Append(" PDK_BUR_CRED_MAT_ESTATUS,")
            strSQL.Append(" PDK_BUR_CRED_MAT_TIPO,")
            strSQL.Append(" PDK_BUR_CRED_MAT_MONEDA,")
            strSQL.Append(" PDK_BUR_CRED_MAT_MOP_ACT,")
            strSQL.Append(" PDK_BUR_CRED_MAT_MOP_ALTO,")
            strSQL.Append(" PDK_BUR_CRED_MAT_CVE_COMENTARIO,")
            strSQL.Append(" PDK_BUR_CRED_MAT_LIM_CRED,")
            strSQL.Append(" PDK_BUR_CRED_MAT_MAX_LIM_CRED,")
            strSQL.Append(" PDK_BUR_CRED_MAT_SDO_ACTUAL,")
            strSQL.Append(" PDK_BUR_CRED_MAT_MTO_VENC,")
            strSQL.Append(" PDK_BUR_CRED_MAT_MTO_PAG,")
            strSQL.Append(" PDK_BUR_CRED_MAT_FEC_APERTURA,")
            strSQL.Append(" PDK_BUR_CRED_MAT_FEC_CIERRE,")
            strSQL.Append(" PDK_BUR_CRED_MAT_FREC_PAGO,")
            strSQL.Append(" PDK_BUR_CRED_MAT_RESP,")
            strSQL.Append(" PDK_BUR_CRED_MAT_TIPO_CONTRATO,")
            strSQL.Append(" PDK_BUR_CRED_MAT_NOM_OTORGANTE,")
            strSQL.Append(" PDK_BUR_CRED_MAT_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_CREDITO_MATRIZ")
            strSQL.Append(" WHERE PDK_ID_BURO_CREDITO_MATRIZ = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_BURO_CREDITO_MATRIZ = .Item("PDK_ID_BURO_CREDITO_MATRIZ")
                Me.intPDK_ID_BURO = .Item("PDK_ID_BURO")
                Me.strPDK_BUR_CRED_SOLICITUD = .Item("PDK_BUR_CRED_SOLICITUD")
                Me.strPDK_BUR_CRED_MAT_ESTATUS = .Item("PDK_BUR_CRED_MAT_ESTATUS")
                Me.strPDK_BUR_CRED_MAT_TIPO = .Item("PDK_BUR_CRED_MAT_TIPO")
                Me.strPDK_BUR_CRED_MAT_MONEDA = .Item("PDK_BUR_CRED_MAT_MONEDA")
                Me.strPDK_BUR_CRED_MAT_MOP_ACT = .Item("PDK_BUR_CRED_MAT_MOP_ACT")
                Me.strPDK_BUR_CRED_MAT_MOP_ALTO = .Item("PDK_BUR_CRED_MAT_MOP_ALTO")
                Me.strPDK_BUR_CRED_MAT_CVE_COMENTARIO = .Item("PDK_BUR_CRED_MAT_CVE_COMENTARIO")
                Me.strPDK_BUR_CRED_MAT_LIM_CRED = .Item("PDK_BUR_CRED_MAT_LIM_CRED")
                Me.strPDK_BUR_CRED_MAT_MAX_LIM_CRED = .Item("PDK_BUR_CRED_MAT_MAX_LIM_CRED")
                Me.strPDK_BUR_CRED_MAT_SDO_ACTUAL = .Item("PDK_BUR_CRED_MAT_SDO_ACTUAL")
                Me.strPDK_BUR_CRED_MAT_MTO_VENC = .Item("PDK_BUR_CRED_MAT_MTO_VENC")
                Me.strPDK_BUR_CRED_MAT_MTO_PAG = .Item("PDK_BUR_CRED_MAT_MTO_PAG")
                Me.strPDK_BUR_CRED_MAT_FEC_APERTURA = .Item("PDK_BUR_CRED_MAT_FEC_APERTURA")
                Me.strPDK_BUR_CRED_MAT_FEC_CIERRE = .Item("PDK_BUR_CRED_MAT_FEC_CIERRE")
                Me.strPDK_BUR_CRED_MAT_FREC_PAGO = .Item("PDK_BUR_CRED_MAT_FREC_PAGO")
                Me.strPDK_BUR_CRED_MAT_RESP = .Item("PDK_BUR_CRED_MAT_RESP")
                Me.strPDK_BUR_CRED_MAT_TIPO_CONTRATO = .Item("PDK_BUR_CRED_MAT_TIPO_CONTRATO")
                Me.strPDK_BUR_CRED_MAT_NOM_OTORGANTE = .Item("PDK_BUR_CRED_MAT_NOM_OTORGANTE")
                Me.strPDK_BUR_CRED_MAT_MODIF = .Item("PDK_BUR_CRED_MAT_MODIF")
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
            strSQL.Append(" A.PDK_ID_BURO_CREDITO_MATRIZ,")
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" A.PDK_BUR_CRED_SOLICITUD,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_ESTATUS,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_TIPO,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_MONEDA,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_MOP_ACT,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_MOP_ALTO,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_CVE_COMENTARIO,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_LIM_CRED,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_MAX_LIM_CRED,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_SDO_ACTUAL,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_MTO_VENC,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_MTO_PAG,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_FEC_APERTURA,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_FEC_CIERRE,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_FREC_PAGO,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_RESP,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_TIPO_CONTRATO,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_NOM_OTORGANTE,")
            strSQL.Append(" A.PDK_BUR_CRED_MAT_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_CREDITO_MATRIZ A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_BURO_CREDITO_MATRIZ")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_BURO_CREDITO_MATRIZ = 0 Then
                Me.intPDK_ID_BURO_CREDITO_MATRIZ = BD.ObtenConsecutivo("", "PDK_BURO_CREDITO_MATRIZ", Nothing)
                strSql = "INSERT INTO PDK_BURO_CREDITO_MATRIZ " & _
                                "(" & _
"PDK_ID_BURO_CREDITO_MATRIZ,PDK_ID_BURO,PDK_BUR_CRED_SOLICITUD,PDK_BUR_CRED_MAT_ESTATUS,PDK_BUR_CRED_MAT_TIPO,PDK_BUR_CRED_MAT_MONEDA,PDK_BUR_CRED_MAT_MOP_ACT,PDK_BUR_CRED_MAT_MOP_ALTO,PDK_BUR_CRED_MAT_CVE_COMENTARIO,PDK_BUR_CRED_MAT_LIM_CRED,PDK_BUR_CRED_MAT_MAX_LIM_CRED,PDK_BUR_CRED_MAT_SDO_ACTUAL,PDK_BUR_CRED_MAT_MTO_VENC,PDK_BUR_CRED_MAT_MTO_PAG,PDK_BUR_CRED_MAT_FEC_APERTURA,PDK_BUR_CRED_MAT_FEC_CIERRE,PDK_BUR_CRED_MAT_FREC_PAGO,PDK_BUR_CRED_MAT_RESP,PDK_BUR_CRED_MAT_TIPO_CONTRATO,PDK_BUR_CRED_MAT_NOM_OTORGANTE,PDK_BUR_CRED_MAT_MODIF,PDK_CLAVE_USUARIO,)" & _
" VALUES ( " & intPDK_ID_BURO_CREDITO_MATRIZ & ",  " & intPDK_ID_BURO & ", '" & strPDK_BUR_CRED_SOLICITUD & "','" & strPDK_BUR_CRED_MAT_ESTATUS & "','" & strPDK_BUR_CRED_MAT_TIPO & "','" & strPDK_BUR_CRED_MAT_MONEDA & "','" & strPDK_BUR_CRED_MAT_MOP_ACT & "','" & strPDK_BUR_CRED_MAT_MOP_ALTO & "','" & strPDK_BUR_CRED_MAT_CVE_COMENTARIO & "','" & strPDK_BUR_CRED_MAT_LIM_CRED & "','" & strPDK_BUR_CRED_MAT_MAX_LIM_CRED & "','" & strPDK_BUR_CRED_MAT_SDO_ACTUAL & "','" & strPDK_BUR_CRED_MAT_MTO_VENC & "','" & strPDK_BUR_CRED_MAT_MTO_PAG & "','" & strPDK_BUR_CRED_MAT_FEC_APERTURA & "','" & strPDK_BUR_CRED_MAT_FEC_CIERRE & "','" & strPDK_BUR_CRED_MAT_FREC_PAGO & "','" & strPDK_BUR_CRED_MAT_RESP & "','" & strPDK_BUR_CRED_MAT_TIPO_CONTRATO & "','" & strPDK_BUR_CRED_MAT_NOM_OTORGANTE & "','" & strPDK_BUR_CRED_MAT_MODIF & "','" & strPDK_CLAVE_USUARIO & "', " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_BURO_CREDITO_MATRIZ ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_BURO_CREDITO_MATRIZ " & _
               "SET " & _
             " PDK_ID_BURO_CREDITO_MATRIZ = " & intPDK_ID_BURO_CREDITO_MATRIZ & ", " & _
             " PDK_ID_BURO = " & intPDK_ID_BURO & ", " & _
                " PDK_BUR_CRED_SOLICITUD = '" & strPDK_BUR_CRED_SOLICITUD & "'," & _
                " PDK_BUR_CRED_MAT_ESTATUS = '" & strPDK_BUR_CRED_MAT_ESTATUS & "'," & _
                " PDK_BUR_CRED_MAT_TIPO = '" & strPDK_BUR_CRED_MAT_TIPO & "'," & _
                " PDK_BUR_CRED_MAT_MONEDA = '" & strPDK_BUR_CRED_MAT_MONEDA & "'," & _
                " PDK_BUR_CRED_MAT_MOP_ACT = '" & strPDK_BUR_CRED_MAT_MOP_ACT & "'," & _
                " PDK_BUR_CRED_MAT_MOP_ALTO = '" & strPDK_BUR_CRED_MAT_MOP_ALTO & "'," & _
                " PDK_BUR_CRED_MAT_CVE_COMENTARIO = '" & strPDK_BUR_CRED_MAT_CVE_COMENTARIO & "'," & _
                " PDK_BUR_CRED_MAT_LIM_CRED = '" & strPDK_BUR_CRED_MAT_LIM_CRED & "'," & _
                " PDK_BUR_CRED_MAT_MAX_LIM_CRED = '" & strPDK_BUR_CRED_MAT_MAX_LIM_CRED & "'," & _
                " PDK_BUR_CRED_MAT_SDO_ACTUAL = '" & strPDK_BUR_CRED_MAT_SDO_ACTUAL & "'," & _
                " PDK_BUR_CRED_MAT_MTO_VENC = '" & strPDK_BUR_CRED_MAT_MTO_VENC & "'," & _
                " PDK_BUR_CRED_MAT_MTO_PAG = '" & strPDK_BUR_CRED_MAT_MTO_PAG & "'," & _
                " PDK_BUR_CRED_MAT_FEC_APERTURA = '" & strPDK_BUR_CRED_MAT_FEC_APERTURA & "'," & _
                " PDK_BUR_CRED_MAT_FEC_CIERRE = '" & strPDK_BUR_CRED_MAT_FEC_CIERRE & "'," & _
                " PDK_BUR_CRED_MAT_FREC_PAGO = '" & strPDK_BUR_CRED_MAT_FREC_PAGO & "'," & _
                " PDK_BUR_CRED_MAT_RESP = '" & strPDK_BUR_CRED_MAT_RESP & "'," & _
                " PDK_BUR_CRED_MAT_TIPO_CONTRATO = '" & strPDK_BUR_CRED_MAT_TIPO_CONTRATO & "'," & _
                " PDK_BUR_CRED_MAT_NOM_OTORGANTE = '" & strPDK_BUR_CRED_MAT_NOM_OTORGANTE & "'," & _
                " PDK_BUR_CRED_MAT_MODIF = '" & strPDK_BUR_CRED_MAT_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "'," & _
             " WHERE PDK_ID_BURO_CREDITO_MATRIZ=  " & intPDK_ID_BURO_CREDITO_MATRIZ
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_BURO_CREDITO_MATRIZ")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_BURO_CREDITO_MATRIZ-------------------------- 


End Class
