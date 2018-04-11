Imports System.Text

Public Class clsTipoObjeto
    '-------------------------- INICIO PDK_TIPO_OBJETO-------------------------- 
#Region "Variables"
    Private intPDK_ID_TIPO_OBJETO As Integer = 0
    Private strPDK_TIP_OBJ_NOMBRE As String = String.Empty
    Private strPDK_TIP_OBJ_NOMBRE_COD As String = String.Empty
    Private strErrorTipoObj As String = ""
#End Region
#Region "Propiedades"
    Public Property PDK_ID_TIPO_OBJETO() As Integer
        Get
            Return intPDK_ID_TIPO_OBJETO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_TIPO_OBJETO = value
        End Set
    End Property
    Public Property PDK_TIP_OBJ_NOMBRE() As String
        Get
            Return strPDK_TIP_OBJ_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_TIP_OBJ_NOMBRE = value
        End Set
    End Property
    Public Property PDK_TIP_OBJ_NOMBRE_COD() As String
        Get
            Return strPDK_TIP_OBJ_NOMBRE_COD
        End Get
        Set(ByVal value As String)
            strPDK_TIP_OBJ_NOMBRE_COD = value
        End Set
    End Property
    Public ReadOnly Property ErroTipoObjeto As String
        Get
            Return strErrorTipoObj
        End Get
    End Property
#End Region
#Region "Metodos"
    Sub New()
    End Sub
    Public Sub New(ByVal intRegistro As Integer)
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intRegistro = 0 Then Exit Sub
            strSQL.Append(" SELECT ")
            strSQL.Append(" PDK_ID_TIPO_OBJETO,")
            strSQL.Append(" PDK_TIP_OBJ_NOMBRE,")
            strSQL.Append(" PDK_TIP_OBJ_NOMBRE_COD")
            strSQL.Append(" FROM PDK_TIPO_OBJETO")
            strSQL.Append(" WHERE PDK_ID_TIPO_OBJETO = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_TIPO_OBJETO = .Item("PDK_ID_TIPO_OBJETO")
                Me.strPDK_TIP_OBJ_NOMBRE = .Item("PDK_TIP_OBJ_NOMBRE")
                Me.strPDK_TIP_OBJ_NOMBRE_COD = .Item("PDK_TIP_OBJ_NOMBRE_COD")
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
            strSQL.Append(" A.PDK_ID_TIPO_OBJETO,")
            strSQL.Append(" A.PDK_TIP_OBJ_NOMBRE,")
            strSQL.Append(" A.PDK_TIP_OBJ_NOMBRE_COD")
            strSQL.Append(" FROM PDK_TIPO_OBJETO A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_TIPO_OBJETO")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_TIPO_OBJETO = 0 Then
                Me.intPDK_ID_TIPO_OBJETO = BD.ObtenConsecutivo("PDK_ID_TIPO_OBJETO", "PDK_TIPO_OBJETO", Nothing)
                strSql = "INSERT INTO PDK_TIPO_OBJETO " & _
                                "(" & _
"PDK_ID_TIPO_OBJETO,PDK_TIP_OBJ_NOMBRE,PDK_TIP_OBJ_NOMBRE_COD)" & _
" VALUES ( " & intPDK_ID_TIPO_OBJETO & ", '" & strPDK_TIP_OBJ_NOMBRE & "','" & strPDK_TIP_OBJ_NOMBRE_COD & "'" & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
            PDK_ID_TIPO_OBJETO = Me.intPDK_ID_TIPO_OBJETO
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_TIPO_OBJETO ")
        End Try
    End Sub
    Public Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_TIPO_OBJETO " & _
               "SET " & _
                " PDK_TIP_OBJ_NOMBRE = '" & strPDK_TIP_OBJ_NOMBRE & "'," & _
                " PDK_TIP_OBJ_NOMBRE_COD = '" & strPDK_TIP_OBJ_NOMBRE_COD & "'" & _
             " WHERE PDK_ID_TIPO_OBJETO=  " & intPDK_ID_TIPO_OBJETO
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_TIPO_OBJETO")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_TIPO_OBJETO-------------------------- 

End Class
