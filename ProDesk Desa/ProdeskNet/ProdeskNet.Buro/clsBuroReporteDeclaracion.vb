Imports System.Text
Public Class clsBuroReporteDeclaracion
'-------------------------- INICIO PDK_BURO_REPORTE_DECLARACION-------------------------- 
#Region "Variables"
    Private intPDK_ID_BURO_REPORTE_DECLARACION As Integer = 0
    Private intPDK_ID_BURO As Integer = 0
    Private strPDK_BUR_REP_DEC_TIPO_SEGUIMIENTO As String = String.Empty
    Private strPDK_BUR_REP_DEC_DECLARACION_CONSUMIDOR As String = String.Empty
    Private strPDK_BUR_REP_DEC_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
#End Region
#Region "Propiedades"
    Public Property PDK_ID_BURO_REPORTE_DECLARACION() As Integer
        Get
            Return intPDK_ID_BURO_REPORTE_DECLARACION
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO_REPORTE_DECLARACION = value
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
    Public Property PDK_BUR_REP_DEC_TIPO_SEGUIMIENTO() As String
        Get
            Return strPDK_BUR_REP_DEC_TIPO_SEGUIMIENTO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DEC_TIPO_SEGUIMIENTO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DEC_DECLARACION_CONSUMIDOR() As String
        Get
            Return strPDK_BUR_REP_DEC_DECLARACION_CONSUMIDOR
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DEC_DECLARACION_CONSUMIDOR = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DEC_MODIF() As String
        Get
            Return strPDK_BUR_REP_DEC_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DEC_MODIF = value
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
            strSQL.Append(" PDK_ID_BURO_REPORTE_DECLARACION,")
            strSQL.Append(" PDK_ID_BURO,")
            strSQL.Append(" PDK_BUR_REP_DEC_TIPO_SEGUIMIENTO,")
            strSQL.Append(" PDK_BUR_REP_DEC_DECLARACION_CONSUMIDOR,")
            strSQL.Append(" PDK_BUR_REP_DEC_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_DECLARACION")
            strSQL.Append(" WHERE PDK_ID_BURO_REPORTE_DECLARACION = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_BURO_REPORTE_DECLARACION = .Item("PDK_ID_BURO_REPORTE_DECLARACION")
                Me.intPDK_ID_BURO = .Item("PDK_ID_BURO")
                Me.strPDK_BUR_REP_DEC_TIPO_SEGUIMIENTO = .Item("PDK_BUR_REP_DEC_TIPO_SEGUIMIENTO")
                Me.strPDK_BUR_REP_DEC_DECLARACION_CONSUMIDOR = .Item("PDK_BUR_REP_DEC_DECLARACION_CONSUMIDOR")
                Me.strPDK_BUR_REP_DEC_MODIF = .Item("PDK_BUR_REP_DEC_MODIF")
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
            strSQL.Append(" A.PDK_ID_BURO_REPORTE_DECLARACION,")
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" A.PDK_BUR_REP_DEC_TIPO_SEGUIMIENTO,")
            strSQL.Append(" A.PDK_BUR_REP_DEC_DECLARACION_CONSUMIDOR,")
            strSQL.Append(" A.PDK_BUR_REP_DEC_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_DECLARACION A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_BURO_REPORTE_DECLARACION")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_BURO_REPORTE_DECLARACION = 0 Then
                Me.intPDK_ID_BURO_REPORTE_DECLARACION = BD.ObtenConsecutivo("", "PDK_BURO_REPORTE_DECLARACION", Nothing)
                strSql = "INSERT INTO PDK_BURO_REPORTE_DECLARACION " & _
                                "(" & _
"PDK_ID_BURO_REPORTE_DECLARACION,PDK_ID_BURO,PDK_BUR_REP_DEC_TIPO_SEGUIMIENTO,PDK_BUR_REP_DEC_DECLARACION_CONSUMIDOR,PDK_BUR_REP_DEC_MODIF,PDK_CLAVE_USUARIO,)" & _
" VALUES ( " & intPDK_ID_BURO_REPORTE_DECLARACION & ",  " & intPDK_ID_BURO & ", '" & strPDK_BUR_REP_DEC_TIPO_SEGUIMIENTO & "','" & strPDK_BUR_REP_DEC_DECLARACION_CONSUMIDOR & "','" & strPDK_BUR_REP_DEC_MODIF & "','" & strPDK_CLAVE_USUARIO & "', " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_BURO_REPORTE_DECLARACION ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_BURO_REPORTE_DECLARACION " & _
               "SET " & _
             " PDK_ID_BURO_REPORTE_DECLARACION = " & intPDK_ID_BURO_REPORTE_DECLARACION & ", " & _
             " PDK_ID_BURO = " & intPDK_ID_BURO & ", " & _
                " PDK_BUR_REP_DEC_TIPO_SEGUIMIENTO = '" & strPDK_BUR_REP_DEC_TIPO_SEGUIMIENTO & "'," & _
                " PDK_BUR_REP_DEC_DECLARACION_CONSUMIDOR = '" & strPDK_BUR_REP_DEC_DECLARACION_CONSUMIDOR & "'," & _
                " PDK_BUR_REP_DEC_MODIF = '" & strPDK_BUR_REP_DEC_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "'," & _
             " WHERE PDK_ID_BURO_REPORTE_DECLARACION=  " & intPDK_ID_BURO_REPORTE_DECLARACION
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_BURO_REPORTE_DECLARACION")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_BURO_REPORTE_DECLARACION-------------------------- 

End Class
