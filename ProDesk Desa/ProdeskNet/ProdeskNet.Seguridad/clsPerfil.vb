'BUG-PD-197 : ERODRIGUEZ: 23/08/2017: Se realizo validacion para limitar perfiles disponibles.
'BUG-PD-267 : RIGLESIAS: 10/11/2017 : se ordeno alfabeticamente la tabla A.PDK_PER_NOMBRE y A.PDK_PER_NOMBRE

Imports System.Data
Imports System.Text

Public Class clsPerfil
    '-------------------------- INICIO PDK_PERFIL-------------------------- 
#Region "Variables"
    Private intPDK_ID_PERFIL As Integer = 0
    Private strPDK_PER_NOMBRE As String = String.Empty
    Private strPDK_PER_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_PER_ACTIVO As Integer = 0
    Private strPDK_PER_STATUS As String = String.Empty
    Private intPDK_PER_NIVEL As Integer = 0
#End Region
#Region "Propiedades"
    Public Property PDK_ID_PERFIL() As Integer
        Get
            Return intPDK_ID_PERFIL
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_PERFIL = value
        End Set
    End Property
    Public Property PDK_PER_NOMBRE() As String
        Get
            Return strPDK_PER_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_PER_NOMBRE = value
        End Set
    End Property
    Public Property PDK_PER_MODIF() As String
        Get
            Return strPDK_PER_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_PER_MODIF = value
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
    Public Property PDK_PER_ACTIVO() As Integer
        Get
            Return intPDK_PER_ACTIVO
        End Get
        Set(ByVal value As Integer)
            intPDK_PER_ACTIVO = value
        End Set
    End Property

    Public Property PDK_PER_STATUS() As String
        Get
            Return strPDK_PER_STATUS
        End Get
        Set(ByVal value As String)
            strPDK_PER_STATUS = value
        End Set
    End Property
    Public Property PDK_PER_NIVEL() As Integer
        Get
            Return intPDK_PER_NIVEL
        End Get
        Set(ByVal value As Integer)
            intPDK_PER_NIVEL = value
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
            strSQL.Append(" A.PDK_ID_PERFIL,")
            strSQL.Append(" A.PDK_PER_NOMBRE,")
            strSQL.Append(" A.PDK_PER_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_PER_ACTIVO,")
            strSQL.Append(" B.PDK_PAR_SIS_PARAMETRO AS 'PDK_PAR_STATUS', ")
            strSQL.Append(" A.PDK_PER_NIVEL ")
            strSQL.Append(" FROM PDK_PERFIL A")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA B ON B.PDK_ID_PARAMETROS_SISTEMA = A.PDK_PER_ACTIVO ")
            strSQL.Append(" WHERE PDK_ID_PERFIL = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_PERFIL = .Item("PDK_ID_PERFIL")
                Me.strPDK_PER_NOMBRE = .Item("PDK_PER_NOMBRE")
                Me.strPDK_PER_MODIF = .Item("PDK_PER_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_PER_ACTIVO = .Item("PDK_PER_ACTIVO")
                Me.strPDK_PER_STATUS = .Item("PDK_PAR_STATUS")
                Me.intPDK_PER_NIVEL = .Item("PDK_PER_NIVEL")

            End With
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try
    End Sub
    Public Shared Function BuscarNivel(ByVal intnivel As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.AppendLine("IF OBJECT_ID('TEMPDB..#TEMPORAL') IS NOT NULL")
            strSQL.AppendLine("DROP TABLE #TEMPORAL;")
            strSQL.AppendLine("CREATE TABLE #TEMPORAL (PDK_ID_PERFIL INT,PDK_PER_NOMBRE VARCHAR(200),PDK_NIVELPER_STATUS INT,PDK_NIVELPER_NUMNIVEL INT)")
            strSQL.AppendLine("INSERT INTO #TEMPORAL(PDK_ID_PERFIL,PDK_PER_NOMBRE,PDK_NIVELPER_STATUS,PDK_NIVELPER_NUMNIVEL)")
            strSQL.AppendLine("SELECT A.PDK_ID_PERFIL ,A.PDK_PER_NOMBRE ,0,0  FROM PDK_PERFIL A WHERE PDK_PER_ACTIVO = 2 ")
            strSQL.AppendLine("UPDATE #TEMPORAL SET PDK_NIVELPER_STATUS=B.PDK_NIVELPER_STATUS,PDK_NIVELPER_NUMNIVEL=B.PDK_NIVELPER_NUMNIVEL")
            strSQL.AppendLine("FROM #TEMPORAL A INNER JOIN PDK_REL_NIVEL_PERFIL B ON A.PDK_ID_PERFIL=B.PDK_ID_PERFIL")
            strSQL.AppendLine("WHERE B.PDK_NIVELPER_NUMNIVEL = " & intnivel & "")
            strSQL.AppendLine("SELECT * FROM #TEMPORAL")
            BuscarNivel = BD.EjecutarQuery(strSQL.ToString)
            Return BuscarNivel
        Catch ex As Exception
            objException = New Exception("Error no se encuentra información")
            Throw objException
        End Try

    End Function
    Public Shared Function ObtenerNivel() As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append("SELECT COUNT(PDK_PER_NIVEL)+1 as NIVEL FROM PDK_PERFIL ")
            ObtenerNivel = BD.EjecutarQuery(strSQL.ToString)
            Return ObtenerNivel
        Catch ex As Exception
            objException = New Exception("Error no se encuentra información")
            Throw objException
        End Try
    End Function
    Public Shared Function ValidarNiveles(ByVal intNivel As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append("SELECT PDK_PER_NOMBRE  FROM PDK_PERFIL WHERE PDK_PER_NIVEL=" & intNivel)
            ValidarNiveles = BD.EjecutarQuery(strSQL.ToString)
            Return ValidarNiveles
        Catch ex As Exception
            objException = New Exception("Error no se encuentra información")
            Throw objException
        End Try
    End Function
    Public Shared Function niveles() As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append("SELECT PDK_PER_NIVEL ,PDK_PER_NOMBRE  FROM PDK_PERFIL WHERE PDK_PER_ACTIVO = 2")
            niveles = BD.EjecutarQuery(strSQL.ToString)
            Return niveles
        Catch ex As Exception
            objException = New Exception("Error no se encuentra información")
            Throw objException
        End Try
    End Function

    Public Shared Function ObtenTodos() As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_PERFIL,")
            strSQL.Append(" A.PDK_PER_NOMBRE,")
            strSQL.Append(" A.PDK_PER_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_PER_ACTIVO,")
            strSQL.Append(" B.PDK_PAR_SIS_PARAMETRO AS 'PDK_PAR_STATUS' ")
            strSQL.Append(" FROM PDK_PERFIL A")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA B ON B.PDK_ID_PARAMETROS_SISTEMA = A.PDK_PER_ACTIVO ORDER BY A.PDK_PER_NOMBRE ASC")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_PERFIL")
            Throw objException
        End Try
    End Function
    Public Shared Function ObtenPerfiles(opc As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        If opc = 1 Then
            Try
                strSQL.Append(" SELECT ")
                strSQL.Append(" A.PDK_ID_PERFIL,")
                strSQL.Append(" A.PDK_PER_NOMBRE,")
                strSQL.Append(" A.PDK_PER_MODIF,")
                strSQL.Append(" A.PDK_CLAVE_USUARIO,")
                strSQL.Append(" A.PDK_PER_ACTIVO,")
                strSQL.Append(" B.PDK_PAR_SIS_PARAMETRO AS 'PDK_PAR_STATUS' ")
                strSQL.Append(" FROM PDK_PERFIL A")
                strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA B ON B.PDK_ID_PARAMETROS_SISTEMA = A.PDK_PER_ACTIVO AND A.PDK_ID_PERFIL <> 71 ORDER BY A.PDK_PER_NOMBRE ASC")
                Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
                Return ds
            Catch ex As Exception
                objException = New Exception("Error al buscar los registros de PDK_PERFIL")
                Throw objException
            End Try
        Else
            Try
                strSQL.Append(" SELECT ")
                strSQL.Append(" A.PDK_ID_PERFIL,")
                strSQL.Append(" A.PDK_PER_NOMBRE,")
                strSQL.Append(" A.PDK_PER_MODIF,")
                strSQL.Append(" A.PDK_CLAVE_USUARIO,")
                strSQL.Append(" A.PDK_PER_ACTIVO,")
                strSQL.Append(" B.PDK_PAR_SIS_PARAMETRO AS 'PDK_PAR_STATUS' ")
                strSQL.Append(" FROM PDK_PERFIL A")
                strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA B ON B.PDK_ID_PARAMETROS_SISTEMA = A.PDK_PER_ACTIVO AND A.PDK_ID_PERFIL <> 71")
                Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
                Return ds
            Catch ex As Exception
                objException = New Exception("Error al buscar los registros de PDK_PERFIL")
                Throw objException
            End Try
        End If

    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_PERFIL = 0 Then
                Me.intPDK_ID_PERFIL = BD.ObtenConsecutivo("PDK_ID_PERFIL", "PDK_PERFIL", Nothing)
                strSql = "INSERT INTO PDK_PERFIL " & _
                                "(" & _
                        "PDK_ID_PERFIL,PDK_PER_NOMBRE,PDK_PER_MODIF,PDK_CLAVE_USUARIO,PDK_PER_ACTIVO,PDK_PER_NIVEL)" & _
                        " VALUES ( " & intPDK_ID_PERFIL & ", '" & strPDK_PER_NOMBRE & "','" & strPDK_PER_MODIF & "', " & intPDK_CLAVE_USUARIO & ",  " & intPDK_PER_ACTIVO & ", " & intPDK_PER_NIVEL & " " & _
                        ")"
            Else
                ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_PERFIL ")
        End Try
    End Sub
    Public Shared Function INSERTARNIVEL(ByVal INTPERFIL As Integer, ByVal INTNIVEL As Integer, ByVal INTSTATUS As Integer, ByVal INTUSUARIO As Integer) As Integer
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "IF EXISTS (SELECT * FROM PDK_REL_NIVEL_PERFIL WHERE PDK_ID_PERFIL=" & INTPERFIL & " AND PDK_NIVELPER_NUMNIVEL=" & INTNIVEL & ") BEGIN UPDATE PDK_REL_NIVEL_PERFIL SET PDK_NIVELPER_STATUS=" & INTSTATUS & " ,PDK_CLAVE_USUARIO=" & INTUSUARIO & " ,PDK_NIVELPER_MODIF= GETDATE() WHERE PDK_ID_PERFIL=" & INTPERFIL & " AND PDK_NIVELPER_NUMNIVEL=" & INTNIVEL & "  END ELSE BEGIN INSERT INTO PDK_REL_NIVEL_PERFIL (PDK_ID_PERFIL,PDK_NIVELPER_NUMNIVEL,PDK_NIVELPER_STATUS,PDK_CLAVE_USUARIO,PDK_NIVELPER_MODIF) VALUES (" & INTPERFIL & "," & INTNIVEL & "," & INTSTATUS & "," & INTUSUARIO & ",GETDATE())  END"
            BD.EjecutarQuery(strSql.ToString)
            INSERTARNIVEL = 1
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_NIVEL ")
        End Try

    End Function
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_PERFIL " & _
               "SET " & _
                " PDK_PER_NOMBRE = '" & strPDK_PER_NOMBRE & "'," & _
                " PDK_PER_MODIF = '" & strPDK_PER_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
                " PDK_PER_ACTIVO = " & intPDK_PER_ACTIVO & " " & _
                " WHERE PDK_ID_PERFIL=  " & intPDK_ID_PERFIL
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_PERFIL")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_PERFIL-------------------------- 

End Class
