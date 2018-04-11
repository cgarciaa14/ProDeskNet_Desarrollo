Imports System.Text

Public Class clsBuroReporteScore
'-------------------------- INICIO PDK_BURO_REPORTE_SCORE-------------------------- 
#Region "Variables"
    Private intPDK_ID_BURO_REPORTE_SCORE As Integer = 0
    Private intPDK_ID_BURO As Integer = 0
    Private strPDK_BUR_REP_SC_NOMBRE As String = String.Empty
    Private strPDK_BUR_REP_SC_COD_SCORE As String = String.Empty
    Private strPDK_BUR_REP_SC_VAL_SCORE As String = String.Empty
    Private strPDK_BUR_REP_SC_COD_RAZON1 As String = String.Empty
    Private strPDK_BUR_REP_SC_COD_RAZON2 As String = String.Empty
    Private strPDK_BUR_REP_SC_COD_RAZON3 As String = String.Empty
    Private strPDK_BUR_REP_SC_COD_ERROR As String = String.Empty
    Private strPDK_BUR_REP_SC_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
#End Region
#Region "Propiedades"
    Public Property PDK_ID_BURO_REPORTE_SCORE() As Integer
        Get
            Return intPDK_ID_BURO_REPORTE_SCORE
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO_REPORTE_SCORE = value
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
    Public Property PDK_BUR_REP_SC_NOMBRE() As String
        Get
            Return strPDK_BUR_REP_SC_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_SC_NOMBRE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_SC_COD_SCORE() As String
        Get
            Return strPDK_BUR_REP_SC_COD_SCORE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_SC_COD_SCORE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_SC_VAL_SCORE() As String
        Get
            Return strPDK_BUR_REP_SC_VAL_SCORE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_SC_VAL_SCORE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_SC_COD_RAZON1() As String
        Get
            Return strPDK_BUR_REP_SC_COD_RAZON1
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_SC_COD_RAZON1 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_SC_COD_RAZON2() As String
        Get
            Return strPDK_BUR_REP_SC_COD_RAZON2
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_SC_COD_RAZON2 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_SC_COD_RAZON3() As String
        Get
            Return strPDK_BUR_REP_SC_COD_RAZON3
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_SC_COD_RAZON3 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_SC_COD_ERROR() As String
        Get
            Return strPDK_BUR_REP_SC_COD_ERROR
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_SC_COD_ERROR = value
        End Set
    End Property
    Public Property PDK_BUR_REP_SC_MODIF() As String
        Get
            Return strPDK_BUR_REP_SC_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_SC_MODIF = value
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
            strSQL.Append(" PDK_ID_BURO_REPORTE_SCORE,")
            strSQL.Append(" PDK_ID_BURO,")
            strSQL.Append(" PDK_BUR_REP_SC_NOMBRE,")
            strSQL.Append(" PDK_BUR_REP_SC_COD_SCORE,")
            strSQL.Append(" PDK_BUR_REP_SC_VAL_SCORE,")
            strSQL.Append(" PDK_BUR_REP_SC_COD_RAZON1,")
            strSQL.Append(" PDK_BUR_REP_SC_COD_RAZON2,")
            strSQL.Append(" PDK_BUR_REP_SC_COD_RAZON3,")
            strSQL.Append(" PDK_BUR_REP_SC_COD_ERROR,")
            strSQL.Append(" PDK_BUR_REP_SC_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_SCORE")
            strSQL.Append(" WHERE PDK_ID_BURO_REPORTE_SCORE = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_BURO_REPORTE_SCORE = .Item("PDK_ID_BURO_REPORTE_SCORE")
                Me.intPDK_ID_BURO = .Item("PDK_ID_BURO")
                Me.strPDK_BUR_REP_SC_NOMBRE = .Item("PDK_BUR_REP_SC_NOMBRE")
                Me.strPDK_BUR_REP_SC_COD_SCORE = .Item("PDK_BUR_REP_SC_COD_SCORE")
                Me.strPDK_BUR_REP_SC_VAL_SCORE = .Item("PDK_BUR_REP_SC_VAL_SCORE")
                Me.strPDK_BUR_REP_SC_COD_RAZON1 = .Item("PDK_BUR_REP_SC_COD_RAZON1")
                Me.strPDK_BUR_REP_SC_COD_RAZON2 = .Item("PDK_BUR_REP_SC_COD_RAZON2")
                Me.strPDK_BUR_REP_SC_COD_RAZON3 = .Item("PDK_BUR_REP_SC_COD_RAZON3")
                Me.strPDK_BUR_REP_SC_COD_ERROR = .Item("PDK_BUR_REP_SC_COD_ERROR")
                Me.strPDK_BUR_REP_SC_MODIF = .Item("PDK_BUR_REP_SC_MODIF")
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
            strSQL.Append(" A.PDK_ID_BURO_REPORTE_SCORE,")
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" A.PDK_BUR_REP_SC_NOMBRE,")
            strSQL.Append(" A.PDK_BUR_REP_SC_COD_SCORE,")
            strSQL.Append(" A.PDK_BUR_REP_SC_VAL_SCORE,")
            strSQL.Append(" A.PDK_BUR_REP_SC_COD_RAZON1,")
            strSQL.Append(" A.PDK_BUR_REP_SC_COD_RAZON2,")
            strSQL.Append(" A.PDK_BUR_REP_SC_COD_RAZON3,")
            strSQL.Append(" A.PDK_BUR_REP_SC_COD_ERROR,")
            strSQL.Append(" A.PDK_BUR_REP_SC_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_SCORE A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_BURO_REPORTE_SCORE")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_BURO_REPORTE_SCORE = 0 Then
                Me.intPDK_ID_BURO_REPORTE_SCORE = BD.ObtenConsecutivo("", "PDK_BURO_REPORTE_SCORE", Nothing)
                strSql = "INSERT INTO PDK_BURO_REPORTE_SCORE " & _
                                "(" & _
"PDK_ID_BURO_REPORTE_SCORE,PDK_ID_BURO,PDK_BUR_REP_SC_NOMBRE,PDK_BUR_REP_SC_COD_SCORE,PDK_BUR_REP_SC_VAL_SCORE,PDK_BUR_REP_SC_COD_RAZON1,PDK_BUR_REP_SC_COD_RAZON2,PDK_BUR_REP_SC_COD_RAZON3,PDK_BUR_REP_SC_COD_ERROR,PDK_BUR_REP_SC_MODIF,PDK_CLAVE_USUARIO,)" & _
" VALUES ( " & intPDK_ID_BURO_REPORTE_SCORE & ",  " & intPDK_ID_BURO & ", '" & strPDK_BUR_REP_SC_NOMBRE & "','" & strPDK_BUR_REP_SC_COD_SCORE & "','" & strPDK_BUR_REP_SC_VAL_SCORE & "','" & strPDK_BUR_REP_SC_COD_RAZON1 & "','" & strPDK_BUR_REP_SC_COD_RAZON2 & "','" & strPDK_BUR_REP_SC_COD_RAZON3 & "','" & strPDK_BUR_REP_SC_COD_ERROR & "','" & strPDK_BUR_REP_SC_MODIF & "','" & strPDK_CLAVE_USUARIO & "', " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_BURO_REPORTE_SCORE ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_BURO_REPORTE_SCORE " & _
               "SET " & _
             " PDK_ID_BURO_REPORTE_SCORE = " & intPDK_ID_BURO_REPORTE_SCORE & ", " & _
             " PDK_ID_BURO = " & intPDK_ID_BURO & ", " & _
                " PDK_BUR_REP_SC_NOMBRE = '" & strPDK_BUR_REP_SC_NOMBRE & "'," & _
                " PDK_BUR_REP_SC_COD_SCORE = '" & strPDK_BUR_REP_SC_COD_SCORE & "'," & _
                " PDK_BUR_REP_SC_VAL_SCORE = '" & strPDK_BUR_REP_SC_VAL_SCORE & "'," & _
                " PDK_BUR_REP_SC_COD_RAZON1 = '" & strPDK_BUR_REP_SC_COD_RAZON1 & "'," & _
                " PDK_BUR_REP_SC_COD_RAZON2 = '" & strPDK_BUR_REP_SC_COD_RAZON2 & "'," & _
                " PDK_BUR_REP_SC_COD_RAZON3 = '" & strPDK_BUR_REP_SC_COD_RAZON3 & "'," & _
                " PDK_BUR_REP_SC_COD_ERROR = '" & strPDK_BUR_REP_SC_COD_ERROR & "'," & _
                " PDK_BUR_REP_SC_MODIF = '" & strPDK_BUR_REP_SC_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "'," & _
             " WHERE PDK_ID_BURO_REPORTE_SCORE=  " & intPDK_ID_BURO_REPORTE_SCORE
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_BURO_REPORTE_SCORE")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_BURO_REPORTE_SCORE-------------------------- 

End Class
