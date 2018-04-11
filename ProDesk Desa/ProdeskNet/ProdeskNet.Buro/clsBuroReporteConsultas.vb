Imports System.Text

Public Class clsBuroReporteConsultas

    '-------------------------- INICIO PDK_BURO_REPORTE_CONSULTAS-------------------------- 
#Region "Variables"
    Private intPDK_ID_BURO_REPORTE_CONSUTLAS As Integer = 0
    Private intPDK_ID_BURO As Integer = 0
    Private strPDK_BUR_REP_CON_IQ_FEC_CONSULTA As String = String.Empty
    Private strPDK_BUR_REP_CON_IQ_IDENTIF_BURO As String = String.Empty
    Private strPDK_BUR_REP_CON_IQ_CVE_OTORGANTE As String = String.Empty
    Private strPDK_BUR_REP_CON_NOM_OTORGANTE As String = String.Empty
    Private strPDK_BUR_REP_CON_TEL_OTORGANTE As String = String.Empty
    Private strPDK_BUR_REP_CON_TIPO_CONTRATO As String = String.Empty
    Private strPDK_BUR_REP_CON_MONEDA As String = String.Empty
    Private strPDK_BUR_REP_CON_MTO_CONTRATO As String = String.Empty
    Private strPDK_BUR_REP_CON_IDEN_RESP As String = String.Empty
    Private strPDK_BUR_REP_CON_IDEN_CONS_NUEVO As String = String.Empty
    Private strPDK_BUR_REP_CON_RESULT_FINAL As String = String.Empty
    Private strPDK_BUR_REP_CON_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
#End Region
#Region "Propiedades"
    Public Property PDK_ID_BURO_REPORTE_CONSUTLAS() As Integer
        Get
            Return intPDK_ID_BURO_REPORTE_CONSUTLAS
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO_REPORTE_CONSUTLAS = value
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
    Public Property PDK_BUR_REP_CON_IQ_FEC_CONSULTA() As String
        Get
            Return strPDK_BUR_REP_CON_IQ_FEC_CONSULTA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CON_IQ_FEC_CONSULTA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CON_IQ_IDENTIF_BURO() As String
        Get
            Return strPDK_BUR_REP_CON_IQ_IDENTIF_BURO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CON_IQ_IDENTIF_BURO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CON_IQ_CVE_OTORGANTE() As String
        Get
            Return strPDK_BUR_REP_CON_IQ_CVE_OTORGANTE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CON_IQ_CVE_OTORGANTE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CON_NOM_OTORGANTE() As String
        Get
            Return strPDK_BUR_REP_CON_NOM_OTORGANTE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CON_NOM_OTORGANTE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CON_TEL_OTORGANTE() As String
        Get
            Return strPDK_BUR_REP_CON_TEL_OTORGANTE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CON_TEL_OTORGANTE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CON_TIPO_CONTRATO() As String
        Get
            Return strPDK_BUR_REP_CON_TIPO_CONTRATO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CON_TIPO_CONTRATO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CON_MONEDA() As String
        Get
            Return strPDK_BUR_REP_CON_MONEDA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CON_MONEDA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CON_MTO_CONTRATO() As String
        Get
            Return strPDK_BUR_REP_CON_MTO_CONTRATO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CON_MTO_CONTRATO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CON_IDEN_RESP() As String
        Get
            Return strPDK_BUR_REP_CON_IDEN_RESP
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CON_IDEN_RESP = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CON_IDEN_CONS_NUEVO() As String
        Get
            Return strPDK_BUR_REP_CON_IDEN_CONS_NUEVO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CON_IDEN_CONS_NUEVO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CON_RESULT_FINAL() As String
        Get
            Return strPDK_BUR_REP_CON_RESULT_FINAL
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CON_RESULT_FINAL = value
        End Set
    End Property
    Public Property PDK_BUR_REP_CON_MODIF() As String
        Get
            Return strPDK_BUR_REP_CON_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_CON_MODIF = value
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
            strSQL.Append(" PDK_ID_BURO_REPORTE_CONSUTLAS,")
            strSQL.Append(" PDK_ID_BURO,")
            strSQL.Append(" PDK_BUR_REP_CON_IQ_FEC_CONSULTA,")
            strSQL.Append(" PDK_BUR_REP_CON_IQ_IDENTIF_BURO,")
            strSQL.Append(" PDK_BUR_REP_CON_IQ_CVE_OTORGANTE,")
            strSQL.Append(" PDK_BUR_REP_CON_NOM_OTORGANTE,")
            strSQL.Append(" PDK_BUR_REP_CON_TEL_OTORGANTE,")
            strSQL.Append(" PDK_BUR_REP_CON_TIPO_CONTRATO,")
            strSQL.Append(" PDK_BUR_REP_CON_MONEDA,")
            strSQL.Append(" PDK_BUR_REP_CON_MTO_CONTRATO,")
            strSQL.Append(" PDK_BUR_REP_CON_IDEN_RESP,")
            strSQL.Append(" PDK_BUR_REP_CON_IDEN_CONS_NUEVO,")
            strSQL.Append(" PDK_BUR_REP_CON_RESULT_FINAL,")
            strSQL.Append(" PDK_BUR_REP_CON_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_CONSULTAS")
            strSQL.Append(" WHERE PDK_ID_BURO_REPORTE_CONSUTLAS = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_BURO_REPORTE_CONSUTLAS = .Item("PDK_ID_BURO_REPORTE_CONSUTLAS")
                Me.intPDK_ID_BURO = .Item("PDK_ID_BURO")
                Me.strPDK_BUR_REP_CON_IQ_FEC_CONSULTA = .Item("PDK_BUR_REP_CON_IQ_FEC_CONSULTA")
                Me.strPDK_BUR_REP_CON_IQ_IDENTIF_BURO = .Item("PDK_BUR_REP_CON_IQ_IDENTIF_BURO")
                Me.strPDK_BUR_REP_CON_IQ_CVE_OTORGANTE = .Item("PDK_BUR_REP_CON_IQ_CVE_OTORGANTE")
                Me.strPDK_BUR_REP_CON_NOM_OTORGANTE = .Item("PDK_BUR_REP_CON_NOM_OTORGANTE")
                Me.strPDK_BUR_REP_CON_TEL_OTORGANTE = .Item("PDK_BUR_REP_CON_TEL_OTORGANTE")
                Me.strPDK_BUR_REP_CON_TIPO_CONTRATO = .Item("PDK_BUR_REP_CON_TIPO_CONTRATO")
                Me.strPDK_BUR_REP_CON_MONEDA = .Item("PDK_BUR_REP_CON_MONEDA")
                Me.strPDK_BUR_REP_CON_MTO_CONTRATO = .Item("PDK_BUR_REP_CON_MTO_CONTRATO")
                Me.strPDK_BUR_REP_CON_IDEN_RESP = .Item("PDK_BUR_REP_CON_IDEN_RESP")
                Me.strPDK_BUR_REP_CON_IDEN_CONS_NUEVO = .Item("PDK_BUR_REP_CON_IDEN_CONS_NUEVO")
                Me.strPDK_BUR_REP_CON_RESULT_FINAL = .Item("PDK_BUR_REP_CON_RESULT_FINAL")
                Me.strPDK_BUR_REP_CON_MODIF = .Item("PDK_BUR_REP_CON_MODIF")
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
            strSQL.Append(" A.PDK_ID_BURO_REPORTE_CONSUTLAS,")
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" A.PDK_BUR_REP_CON_IQ_FEC_CONSULTA,")
            strSQL.Append(" A.PDK_BUR_REP_CON_IQ_IDENTIF_BURO,")
            strSQL.Append(" A.PDK_BUR_REP_CON_IQ_CVE_OTORGANTE,")
            strSQL.Append(" A.PDK_BUR_REP_CON_NOM_OTORGANTE,")
            strSQL.Append(" A.PDK_BUR_REP_CON_TEL_OTORGANTE,")
            strSQL.Append(" A.PDK_BUR_REP_CON_TIPO_CONTRATO,")
            strSQL.Append(" A.PDK_BUR_REP_CON_MONEDA,")
            strSQL.Append(" A.PDK_BUR_REP_CON_MTO_CONTRATO,")
            strSQL.Append(" A.PDK_BUR_REP_CON_IDEN_RESP,")
            strSQL.Append(" A.PDK_BUR_REP_CON_IDEN_CONS_NUEVO,")
            strSQL.Append(" A.PDK_BUR_REP_CON_RESULT_FINAL,")
            strSQL.Append(" A.PDK_BUR_REP_CON_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_CONSULTAS A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_BURO_REPORTE_CONSULTAS")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_BURO_REPORTE_CONSUTLAS = 0 Then
                Me.intPDK_ID_BURO_REPORTE_CONSUTLAS = BD.ObtenConsecutivo("", "PDK_BURO_REPORTE_CONSULTAS", Nothing)
                strSql = "INSERT INTO PDK_BURO_REPORTE_CONSULTAS " & _
                                "(" & _
"PDK_ID_BURO_REPORTE_CONSUTLAS,PDK_ID_BURO,PDK_BUR_REP_CON_IQ_FEC_CONSULTA,PDK_BUR_REP_CON_IQ_IDENTIF_BURO,PDK_BUR_REP_CON_IQ_CVE_OTORGANTE,PDK_BUR_REP_CON_NOM_OTORGANTE,PDK_BUR_REP_CON_TEL_OTORGANTE,PDK_BUR_REP_CON_TIPO_CONTRATO,PDK_BUR_REP_CON_MONEDA,PDK_BUR_REP_CON_MTO_CONTRATO,PDK_BUR_REP_CON_IDEN_RESP,PDK_BUR_REP_CON_IDEN_CONS_NUEVO,PDK_BUR_REP_CON_RESULT_FINAL,PDK_BUR_REP_CON_MODIF,PDK_CLAVE_USUARIO,)" & _
" VALUES ( " & intPDK_ID_BURO_REPORTE_CONSUTLAS & ",  " & intPDK_ID_BURO & ", '" & strPDK_BUR_REP_CON_IQ_FEC_CONSULTA & "','" & strPDK_BUR_REP_CON_IQ_IDENTIF_BURO & "','" & strPDK_BUR_REP_CON_IQ_CVE_OTORGANTE & "','" & strPDK_BUR_REP_CON_NOM_OTORGANTE & "','" & strPDK_BUR_REP_CON_TEL_OTORGANTE & "','" & strPDK_BUR_REP_CON_TIPO_CONTRATO & "','" & strPDK_BUR_REP_CON_MONEDA & "','" & strPDK_BUR_REP_CON_MTO_CONTRATO & "','" & strPDK_BUR_REP_CON_IDEN_RESP & "','" & strPDK_BUR_REP_CON_IDEN_CONS_NUEVO & "','" & strPDK_BUR_REP_CON_RESULT_FINAL & "','" & strPDK_BUR_REP_CON_MODIF & "','" & strPDK_CLAVE_USUARIO & "', " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_BURO_REPORTE_CONSULTAS ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_BURO_REPORTE_CONSULTAS " & _
               "SET " & _
             " PDK_ID_BURO_REPORTE_CONSUTLAS = " & intPDK_ID_BURO_REPORTE_CONSUTLAS & ", " & _
             " PDK_ID_BURO = " & intPDK_ID_BURO & ", " & _
                " PDK_BUR_REP_CON_IQ_FEC_CONSULTA = '" & strPDK_BUR_REP_CON_IQ_FEC_CONSULTA & "'," & _
                " PDK_BUR_REP_CON_IQ_IDENTIF_BURO = '" & strPDK_BUR_REP_CON_IQ_IDENTIF_BURO & "'," & _
                " PDK_BUR_REP_CON_IQ_CVE_OTORGANTE = '" & strPDK_BUR_REP_CON_IQ_CVE_OTORGANTE & "'," & _
                " PDK_BUR_REP_CON_NOM_OTORGANTE = '" & strPDK_BUR_REP_CON_NOM_OTORGANTE & "'," & _
                " PDK_BUR_REP_CON_TEL_OTORGANTE = '" & strPDK_BUR_REP_CON_TEL_OTORGANTE & "'," & _
                " PDK_BUR_REP_CON_TIPO_CONTRATO = '" & strPDK_BUR_REP_CON_TIPO_CONTRATO & "'," & _
                " PDK_BUR_REP_CON_MONEDA = '" & strPDK_BUR_REP_CON_MONEDA & "'," & _
                " PDK_BUR_REP_CON_MTO_CONTRATO = '" & strPDK_BUR_REP_CON_MTO_CONTRATO & "'," & _
                " PDK_BUR_REP_CON_IDEN_RESP = '" & strPDK_BUR_REP_CON_IDEN_RESP & "'," & _
                " PDK_BUR_REP_CON_IDEN_CONS_NUEVO = '" & strPDK_BUR_REP_CON_IDEN_CONS_NUEVO & "'," & _
                " PDK_BUR_REP_CON_RESULT_FINAL = '" & strPDK_BUR_REP_CON_RESULT_FINAL & "'," & _
                " PDK_BUR_REP_CON_MODIF = '" & strPDK_BUR_REP_CON_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "'," & _
             " WHERE PDK_ID_BURO_REPORTE_CONSUTLAS=  " & intPDK_ID_BURO_REPORTE_CONSUTLAS
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_BURO_REPORTE_CONSULTAS")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_BURO_REPORTE_CONSULTAS-------------------------- 

End Class
