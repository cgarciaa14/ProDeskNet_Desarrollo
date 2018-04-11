Imports System.Text

Public Class clsProcesos

    '-------------------------- INICIO PDK_CAT_PROCESOS-------------------------- 
#Region "Variables"
    Private intPDK_ID_PROCESOS As Integer = 0
    Private intPDK_ID_FLUJOS As Integer = 0
    Private strPDK_PROC_NOMBRE As String = String.Empty
    Private strPDK_PROC_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
    Private intPDK_PROC_ACTIVO As Integer = 0
    Private intPDK_PROC_ORDEN As Integer = 0
    Private intPDK_PROC_PADRE As Integer = 0
    Private intPDK_PROC_PARALLEL As Integer = 0


    Private arrTareas() As clsTareas

#End Region
#Region "Propiedades"

    Public Property tareas() As clsTareas()
        Get
            Return arrTareas
        End Get
        Set(value As clsTareas())
            arrTareas = value
        End Set
    End Property





    Public Property PDK_ID_PROCESOS() As Integer
        Get
            Return intPDK_ID_PROCESOS
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_PROCESOS = value
        End Set
    End Property
    Public Property PDK_ID_FLUJOS() As Integer
        Get
            Return intPDK_ID_FLUJOS
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_FLUJOS = value
        End Set
    End Property
    Public Property PDK_PROC_NOMBRE() As String
        Get
            Return strPDK_PROC_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_PROC_NOMBRE = value
        End Set
    End Property
    Public Property PDK_PROC_MODIF() As String
        Get
            Return strPDK_PROC_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_PROC_MODIF = value
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
    Public Property PDK_PROC_ACTIVO() As Integer
        Get
            Return intPDK_PROC_ACTIVO
        End Get
        Set(ByVal value As Integer)
            intPDK_PROC_ACTIVO = value
        End Set
    End Property
    Public Property PDK_PROC_ORDEN() As Integer
        Get
            Return intPDK_PROC_ORDEN
        End Get
        Set(ByVal value As Integer)
            intPDK_PROC_ORDEN = value
        End Set
    End Property
    Public Property PDK_PROC_PADRE() As Integer
        Get
            Return intPDK_PROC_PADRE
        End Get
        Set(ByVal value As Integer)
            intPDK_PROC_PADRE = value
        End Set
    End Property
    Public Property PDK_PROC_PARALLEL() As Integer
        Get
            Return intPDK_PROC_PARALLEL
        End Get
        Set(ByVal value As Integer)
            intPDK_PROC_PARALLEL = value
        End Set
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
            strSQL.Append(" PDK_ID_PROCESOS,")
            strSQL.Append(" PDK_ID_FLUJOS,")
            strSQL.Append(" PDK_PROC_NOMBRE,")
            strSQL.Append(" PDK_PROC_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" PDK_PROC_ACTIVO,")
            strSQL.Append(" PDK_PROC_ORDEN,")
            strSQL.Append(" PDK_PROC_PADRE,")
            strSQL.Append(" PDK_PROC_PARALLEL")
            strSQL.Append(" FROM PDK_CAT_PROCESOS")
            strSQL.Append(" WHERE PDK_ID_PROCESOS = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_PROCESOS = .Item("PDK_ID_PROCESOS")
                Me.intPDK_ID_FLUJOS = .Item("PDK_ID_FLUJOS")
                Me.strPDK_PROC_NOMBRE = .Item("PDK_PROC_NOMBRE")
                Me.strPDK_PROC_MODIF = .Item("PDK_PROC_MODIF")
                Me.strPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_PROC_ACTIVO = .Item("PDK_PROC_ACTIVO")
                Me.intPDK_PROC_ORDEN = .Item("PDK_PROC_ORDEN")
                Me.intPDK_PROC_PADRE = .Item("PDK_PROC_PADRE")
                Me.intPDK_PROC_PARALLEL = .Item("PDK_PROC_PARALLEL")
            End With
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try
    End Sub
    Public Shared Function ObtenTodos(Optional ByVal intFlujo As Integer = 0, Optional ByVal intBandera As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_PROCESOS,")
            strSQL.Append(" A.PDK_ID_FLUJOS,")
            strSQL.Append(" A.PDK_PROC_NOMBRE,")
            strSQL.Append(" A.PDK_PROC_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_PROC_ACTIVO,")
            strSQL.Append(" A.PDK_PROC_ORDEN,")
            strSQL.Append(" A.PDK_PROC_PADRE,")
            strSQL.Append(" A.PDK_PROC_PARALLEL,")
            strSQL.Append(" C.PDK_PAR_SIS_PARAMETRO,")
            strSQL.Append(" D.PDK_PAR_SIS_PARAMETRO AS 'PDK_PAR_SIS_PARAMETRO_PARALEL',")
            strSQL.Append(" CASE WHEN A.PDK_PROC_PADRE = 0 THEN 'NINGUNO' ELSE E.PDK_PROC_NOMBRE END AS 'PDK_PROC_PADRE_NOMBRE'")

            strSQL.Append(" FROM PDK_CAT_PROCESOS A ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA C ON C.PDK_ID_PARAMETROS_SISTEMA = A.PDK_PROC_ACTIVO ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA D ON D.PDK_ID_PARAMETROS_SISTEMA = A.PDK_PROC_PARALLEL")
            strSQL.Append(" LEFT OUTER JOIN PDK_CAT_PROCESOS E ON E.PDK_ID_PROCESOS = A.PDK_PROC_PADRE")

            If intFlujo > 0 Then
                strSQL.Append(" WHERE A.PDK_ID_FLUJOS= " & intFlujo & " ")
            End If
            If intBandera > 0 Then
                strSQL.Append(" AND A.PDK_PROC_ACTIVO= 2 ")
            End If

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_PROCESOS")
            Throw objException
        End Try
    End Function

    Public Sub Guarda()

        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Try
            If intPDK_ID_PROCESOS = 0 Then
                Me.intPDK_ID_PROCESOS = BD.ObtenConsecutivo("PDK_ID_PROCESOS", "PDK_CAT_PROCESOS", Nothing)
                strSql = "INSERT INTO PDK_CAT_PROCESOS " & _
                        "(" & _
                        "PDK_ID_PROCESOS,PDK_ID_FLUJOS,PDK_PROC_NOMBRE,PDK_PROC_MODIF,PDK_CLAVE_USUARIO,PDK_PROC_ACTIVO,PDK_PROC_ORDEN,PDK_PROC_PADRE,PDK_PROC_PARALLEL)" & _
                        " VALUES ( " & intPDK_ID_PROCESOS & ",  " & intPDK_ID_FLUJOS & ", '" & strPDK_PROC_NOMBRE & "','" & strPDK_PROC_MODIF & "','" & strPDK_CLAVE_USUARIO & "', " & intPDK_PROC_ACTIVO & ",  " & intPDK_PROC_ORDEN & ",  " & intPDK_PROC_PADRE & ",  " & intPDK_PROC_PARALLEL & " " & _
                        ")"
            Else
                ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
            PDK_ID_PROCESOS = Me.intPDK_ID_PROCESOS
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_CAT_PROCESOS ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_CAT_PROCESOS " & _
               "SET " & _
             " PDK_ID_FLUJOS = " & intPDK_ID_FLUJOS & ", " & _
            " PDK_PROC_NOMBRE = '" & strPDK_PROC_NOMBRE & "'," & _
            " PDK_PROC_MODIF = '" & strPDK_PROC_MODIF & "'," & _
            " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "'," & _
             " PDK_PROC_ACTIVO = " & intPDK_PROC_ACTIVO & ", " & _
             " PDK_PROC_ORDEN = " & intPDK_PROC_ORDEN & ", " & _
             " PDK_PROC_PADRE = " & intPDK_PROC_PADRE & ", " & _
             " PDK_PROC_PARALLEL = " & intPDK_PROC_PARALLEL & " " & _
             " WHERE PDK_ID_PROCESOS=  " & intPDK_ID_PROCESOS
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_PROCESOS")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_CAT_PROCESOS-------------------------- 


End Class
