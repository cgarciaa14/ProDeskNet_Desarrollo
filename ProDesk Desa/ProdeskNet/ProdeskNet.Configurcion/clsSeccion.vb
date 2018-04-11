Imports System.Text

Public Class clsSeccion
    '-------------------------- INICIO PDK_SECCION-------------------------- 
#Region "Variables"
    Private intPDK_ID_SECCION As Integer = 0
    Private strPDK_SEC_NOMBRE As String = String.Empty
    Private strPDK_SEC_NOMBRE_TABLA As String = String.Empty
    Private strPDK_SEC_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_SEC_CREACION As Integer = 0
    Private intPDK_SEC_STATUS As Integer = 0
    Private strPDK_SEC_TAB_MOSTRAR As String = String.Empty
    Private strErrorSeccion As String = ""
#End Region
#Region "Propiedades"
    Public Property PDK_ID_SECCION() As Integer
        Get
            Return intPDK_ID_SECCION
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_SECCION = value
        End Set
    End Property
    Public Property PDK_SEC_NOMBRE() As String
        Get
            Return strPDK_SEC_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_SEC_NOMBRE = value
        End Set
    End Property
    Public Property PDK_SEC_NOMBRE_TABLA() As String
        Get
            Return strPDK_SEC_NOMBRE_TABLA
        End Get
        Set(ByVal value As String)
            strPDK_SEC_NOMBRE_TABLA = value
        End Set
    End Property
    Public Property PDK_SEC_MODIF() As String
        Get
            Return strPDK_SEC_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_SEC_MODIF = value
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
    Public Property PDK_SEC_CREACION() As Integer
        Get
            Return intPDK_SEC_CREACION
        End Get
        Set(ByVal value As Integer)
            intPDK_SEC_CREACION = value
        End Set
    End Property
    Public Property PDK_SEC_STATUS() As Integer
        Get
            Return intPDK_SEC_STATUS
        End Get
        Set(ByVal value As Integer)
            intPDK_SEC_STATUS = value
        End Set
    End Property
    Public Property PDK_SEC_TAB_MOSTRAR() As String
        Get
            Return strPDK_SEC_TAB_MOSTRAR
        End Get
        Set(value As String)
            strPDK_SEC_TAB_MOSTRAR = value
        End Set
    End Property
    Public ReadOnly Property ErrorSeccion() As String
        Get
            Return strErrorSeccion
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
            strSQL.Append(" PDK_ID_SECCION,")
            strSQL.Append(" PDK_SEC_NOMBRE,")
            strSQL.Append(" PDK_SEC_NOMBRE_TABLA,")
            strSQL.Append(" PDK_SEC_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" PDK_SEC_CREACION,")
            strSQL.Append(" PDK_SEC_STATUS, ")
            strSQL.Append(" PDK_SEC_TAB_MOSTRAR ")
            strSQL.Append(" FROM PDK_SECCION")
            strSQL.Append(" WHERE PDK_ID_SECCION = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_SECCION = .Item("PDK_ID_SECCION")
                Me.strPDK_SEC_NOMBRE = .Item("PDK_SEC_NOMBRE")
                Me.strPDK_SEC_NOMBRE_TABLA = .Item("PDK_SEC_NOMBRE_TABLA")
                Me.strPDK_SEC_MODIF = .Item("PDK_SEC_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_SEC_CREACION = .Item("PDK_SEC_CREACION")
                Me.intPDK_SEC_STATUS = .Item("PDK_SEC_STATUS")
                Me.strPDK_SEC_TAB_MOSTRAR = .Item("PDK_SEC_TAB_MOSTRAR")

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
            strSQL.Append(" A.PDK_ID_SECCION,")
            strSQL.Append(" A.PDK_SEC_NOMBRE,")
            strSQL.Append(" A.PDK_SEC_NOMBRE_TABLA,")
            strSQL.Append(" A.PDK_SEC_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_SEC_CREACION,")
            strSQL.Append(" A.PDK_SEC_STATUS,")
            strSQL.Append(" A.PDK_SEC_TAB_MOSTRAR ")
            strSQL.Append(" FROM PDK_SECCION A WHERE A.PDK_ID_SECCION<>0 ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_SECCION")
            Throw objException
        End Try
    End Function
    Public Shared Function obtenerSeccStatus() As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_SECCION,")
            strSQL.Append(" A.PDK_SEC_NOMBRE,")
            strSQL.Append(" A.PDK_SEC_NOMBRE_TABLA,")
            strSQL.Append(" A.PDK_SEC_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_SEC_CREACION,")
            strSQL.Append(" A.PDK_SEC_STATUS,")
            strSQL.Append(" A.PDK_SEC_TAB_MOSTRAR")
            strSQL.Append(" FROM PDK_SECCION A WHERE A.PDK_ID_SECCION<>0 AND A.PDK_SEC_CREACION=2 AND A.PDK_SEC_STATUS=2")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_SECCION")
            Throw objException
        End Try

    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_SECCION = 0 Then
                Me.intPDK_ID_SECCION = BD.ObtenConsecutivo("PDK_ID_SECCION", "PDK_SECCION", Nothing)
                strSql = "INSERT INTO PDK_SECCION " & _
                                "(" & _
"PDK_ID_SECCION,PDK_SEC_NOMBRE,PDK_SEC_NOMBRE_TABLA,PDK_SEC_MODIF,PDK_CLAVE_USUARIO,PDK_SEC_CREACION,PDK_SEC_STATUS,PDK_SEC_TAB_MOSTRAR)" & _
" VALUES ( " & intPDK_ID_SECCION & ", '" & strPDK_SEC_NOMBRE & "','" & strPDK_SEC_NOMBRE_TABLA & "','" & strPDK_SEC_MODIF & "', " & intPDK_CLAVE_USUARIO & ",  " & intPDK_SEC_CREACION & ",  " & intPDK_SEC_STATUS & ",'" & strPDK_SEC_TAB_MOSTRAR & "'  " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
            PDK_ID_SECCION = Me.intPDK_ID_SECCION
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_SECCION ")
        End Try
    End Sub
    Public Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_SECCION " & _
               "SET " & _
               " PDK_SEC_NOMBRE = '" & strPDK_SEC_NOMBRE & "'," & _
                " PDK_SEC_NOMBRE_TABLA = '" & strPDK_SEC_NOMBRE_TABLA & "'," & _
                " PDK_SEC_MODIF = '" & strPDK_SEC_MODIF & "'," & _
             " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
             " PDK_SEC_CREACION = " & intPDK_SEC_CREACION & ", " & _
             " PDK_SEC_STATUS = " & intPDK_SEC_STATUS & ", " & _
             " PDK_SEC_TAB_MOSTRAR = '" & strPDK_SEC_TAB_MOSTRAR & "' " & _
             " WHERE PDK_ID_SECCION=  " & intPDK_ID_SECCION
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_SECCION")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_SECCION-------------------------- 

End Class
