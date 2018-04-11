Imports System.Text

Public Class clsBuroBitacora

'-------------------------- INICIO PDK_BURO_BITACORA-------------------------- 
#Region "Variables"
    Private intPDK_ID_BURO As Integer = 0
    Private strPDK_BUR_RESPUESTA As String = String.Empty
    Private strPDK_BUR_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
#End Region
#Region "Propiedades"
    Public Property PDK_ID_BURO() As Integer
        Get
            Return intPDK_ID_BURO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO = value
        End Set
    End Property
    Public Property PDK_BUR_RESPUESTA() As String
        Get
            Return strPDK_BUR_RESPUESTA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_RESPUESTA = value
        End Set
    End Property
    Public Property PDK_BUR_MODIF() As String
        Get
            Return strPDK_BUR_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_MODIF = value
        End Set
    End Property
    Public Property PDK_CLAVE_USUARIO() As Integer
        Get
            Return intPDK_CLAVE_USUARIO
        End Get
        Set(ByVal value As Integer)
            intPDK_CLAVE_USUARIO = value
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
            strSQL.Append(" PDK_ID_BURO,")
            strSQL.Append(" PDK_BUR_RESPUESTA,")
            strSQL.Append(" PDK_BUR_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_BITACORA")
            strSQL.Append(" WHERE PDK_ID_BURO = " & intRegistro)

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_BURO = .Item("PDK_ID_BURO")
                Me.strPDK_BUR_RESPUESTA = .Item("PDK_BUR_RESPUESTA")
                Me.strPDK_BUR_MODIF = .Item("PDK_BUR_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
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
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" A.PDK_BUR_RESPUESTA,")
            strSQL.Append(" A.PDK_BUR_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_BITACORA A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_BURO_BITACORA")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_BURO = 0 Then
                Me.intPDK_ID_BURO = BD.ObtenConsecutivo("", "PDK_BURO_BITACORA", Nothing)
                strSql = "INSERT INTO PDK_BURO_BITACORA " & _
                                "(" & _
                    "PDK_ID_BURO,PDK_BUR_RESPUESTA,PDK_BUR_MODIF,PDK_CLAVE_USUARIO,)" & _
                    " VALUES ( " & intPDK_ID_BURO & ", '" & strPDK_BUR_RESPUESTA & "','" & strPDK_BUR_MODIF & "', " & intPDK_CLAVE_USUARIO & ",  " & _
                    ")"
            Else
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_BURO_BITACORA ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_BURO_BITACORA " & _
               "SET " & _
                " PDK_BUR_RESPUESTA = '" & strPDK_BUR_RESPUESTA & "'," & _
                " PDK_BUR_MODIF = '" & strPDK_BUR_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
                " WHERE PDK_ID_BURO=  " & intPDK_ID_BURO

            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_BURO_BITACORA")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_BURO_BITACORA-------------------------- 

End Class
