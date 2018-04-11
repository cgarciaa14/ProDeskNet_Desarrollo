Imports System.Text
Imports ProdeskNet.BD

#Region "TRACKERS"

'BUG-PD-295: CCHAVEZ: 06/12/2017:Div Ordenar Consulta  Tareas.
'BUG-PD-346: DCORNEJO: 23/01/2018: Ordenar Tareas alfabeticamentre de los combos para modificar una tarea.
'RQ-PD25: DCORNEJO: 22/02/2018: OBTENTAREAS Y OBTENSOLICITUD PARA MAPEO TAREAS

#End Region
Public Class clsTareas

    '-------------------------- INICIO PDK_CAT_TAREAS-------------------------- 
#Region "Variables"
    Private intPDK_ID_TAREAS As Integer = 0
    Private intPDK_ID_PROCESOS As Integer = 0
    Private strPDK_TAR_NOMBRE As String = String.Empty
    Private strPDK_TAR_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_TAR_ACTIVO As Integer = 0
    Private intPDK_TAR_ORDEN As Integer = 0
    Private intPDK_TAR_PARALEL As Integer = 0
    Private intPDK_TAR_PADRE As Integer = 0

    Private intPDK_ID_TAREAS_RECHAZO As Integer = 0
    Private intPDK_ID_TAREAS_NORECHAZO As Integer = 0


    Private intPDK_ID_REL_TAR_PERFIL As Integer = 0
    Private intPDK_ID_PERFIL As Integer = 0
    Private intPDK_TAR_DICTAME As Integer = 0

    Private arrPantallas() As clsPantallas

#End Region
#Region "Propiedades"

    Public Property pantallas As clsPantallas()
        Get
            Return arrPantallas
        End Get
        Set(value As clsPantallas())
            arrPantallas = value
        End Set
    End Property



    Public Property PDK_ID_TAREAS() As Integer
        Get
            Return intPDK_ID_TAREAS
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_TAREAS = value
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
    Public Property PDK_TAR_NOMBRE() As String
        Get
            Return strPDK_TAR_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_TAR_NOMBRE = value
        End Set
    End Property
    Public Property PDK_TAR_MODIF() As String
        Get
            Return strPDK_TAR_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_TAR_MODIF = value
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
    Public Property PDK_TAR_ACTIVO() As Integer
        Get
            Return intPDK_TAR_ACTIVO
        End Get
        Set(ByVal value As Integer)
            intPDK_TAR_ACTIVO = value
        End Set
    End Property
    Public Property PDK_TAR_ORDEN() As Integer
        Get
            Return intPDK_TAR_ORDEN
        End Get
        Set(ByVal value As Integer)
            intPDK_TAR_ORDEN = value
        End Set
    End Property
    Public Property PDK_TAR_PARALEL() As Integer
        Get
            Return intPDK_TAR_PARALEL
        End Get
        Set(ByVal value As Integer)
            intPDK_TAR_PARALEL = value
        End Set
    End Property
    Public Property PDK_TAR_PADRE() As Integer
        Get
            Return intPDK_TAR_PADRE
        End Get
        Set(ByVal value As Integer)
            intPDK_TAR_PADRE = value
        End Set
    End Property

    Public Property PDK_ID_TAREAS_RECHAZO() As Integer
        Get
            Return intPDK_ID_TAREAS_RECHAZO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_TAREAS_RECHAZO = value
        End Set
    End Property

    Public Property PDK_ID_TAREAS_NORECHAZO() As Integer
        Get
            Return intPDK_ID_TAREAS_NORECHAZO

        End Get
        Set(ByVal value As Integer)
            intPDK_ID_TAREAS_NORECHAZO = value
        End Set
    End Property
    Public Property PDK_TAR_DICTAME() As Integer
        Get
            Return intPDK_TAR_DICTAME

        End Get
        Set(ByVal value As Integer)
            intPDK_TAR_DICTAME = value
        End Set
    End Property


    Public Property PDK_ID_REL_TAR_PERFIL() As Integer
        Get
            Return intPDK_ID_REL_TAR_PERFIL
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_REL_TAR_PERFIL = value
        End Set
    End Property

    Public Property PDK_ID_PERFIL() As Integer
        Get
            Return intPDK_ID_PERFIL

        End Get
        Set(ByVal value As Integer)
            intPDK_ID_PERFIL = value
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
            strSQL.Append(" A.PDK_ID_TAREAS,")
            strSQL.Append(" A.PDK_ID_PROCESOS,")
            strSQL.Append(" A.PDK_TAR_NOMBRE,")
            strSQL.Append(" A.PDK_TAR_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_TAR_ACTIVO,")
            strSQL.Append(" A.PDK_TAR_ORDEN,")
            strSQL.Append(" A.PDK_TAR_PARALEL,")
            strSQL.Append(" A.PDK_TAR_PADRE, A.PDK_ID_TAREAS_RECHAZO , A.PDK_ID_TAREAS_NORECHAZO,A.PDK_TAR_DICTAME,B.PDK_ID_PERFIL,B.PDK_ID_REL_TAR_PERFIL")
            strSQL.Append(" FROM PDK_CAT_TAREAS A INNER JOIN PDK_REL_TAR_PERFIL B  ON A.PDK_ID_TAREAS=B.PDK_ID_TAREAS ")
            strSQL.Append(" WHERE A.PDK_ID_TAREAS = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_TAREAS = .Item("PDK_ID_TAREAS")
                Me.intPDK_ID_PROCESOS = .Item("PDK_ID_PROCESOS")
                Me.strPDK_TAR_NOMBRE = .Item("PDK_TAR_NOMBRE")
                Me.strPDK_TAR_MODIF = .Item("PDK_TAR_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_TAR_ACTIVO = .Item("PDK_TAR_ACTIVO")
                Me.intPDK_TAR_ORDEN = .Item("PDK_TAR_ORDEN")
                Me.intPDK_TAR_PARALEL = .Item("PDK_TAR_PARALEL")
                Me.intPDK_TAR_PADRE = .Item("PDK_TAR_PADRE")
                Me.intPDK_ID_TAREAS_RECHAZO = .Item("PDK_ID_TAREAS_RECHAZO")
                Me.intPDK_ID_TAREAS_NORECHAZO = .Item("PDK_ID_TAREAS_NORECHAZO")
                Me.intPDK_TAR_DICTAME = .Item("PDK_TAR_DICTAME")
                Me.intPDK_ID_PERFIL = .Item("PDK_ID_PERFIL")
                Me.intPDK_ID_REL_TAR_PERFIL = .Item("PDK_ID_REL_TAR_PERFIL")

            End With
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try
    End Sub
    Public Shared Function ObtenerPerfil() As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.AppendLine("SELECT PDK_ID_PERFIL,PDK_PER_NOMBRE  FROM PDK_PERFIL ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_PERFIL")
            Throw objException
        End Try
    End Function

    Public Shared Function ObtenerDictame() As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.AppendLine("SELECT id,DICTAMENFINAL  FROM PDK_MATRIZ_DICTAMENFINAL")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_PERFIL")
            Throw objException
        End Try
    End Function

    Public Shared Function ObtenTodos(Optional ByVal intProceso As Integer = 0, Optional ByVal intPerfil As Integer = 0, Optional ByVal intflujo As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_TAREAS,")
            strSQL.Append(" A.PDK_ID_PROCESOS,")
            strSQL.Append(" A.PDK_TAR_NOMBRE,")
            strSQL.Append(" A.PDK_TAR_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_TAR_ACTIVO,")
            strSQL.Append(" A.PDK_TAR_ORDEN,")
            strSQL.Append(" A.PDK_TAR_PARALEL,")
            strSQL.Append(" A.PDK_TAR_PADRE,")
            strSQL.Append(" B.PDK_PROC_NOMBRE,")
            strSQL.Append(" C.PDK_PAR_SIS_PARAMETRO,")
            strSQL.Append(" D.PDK_PAR_SIS_PARAMETRO AS 'PDK_PAR_SIS_PARAMETRO_PARALEL',")
            strSQL.Append(" CASE WHEN A.PDK_TAR_PADRE = 0 THEN 'NINGUNO' ELSE E.PDK_TAR_NOMBRE END AS 'PDK_TAR_PADRE_NOMBRE',")
            strSQL.Append(" A.PDK_ID_TAREAS_RECHAZO , A.PDK_ID_TAREAS_NORECHAZO, ")

            strSQL.Append(" ISNULL(F.PDK_ID_REL_TAR_PERFIL,0) PDK_ID_REL_TAR_PERFIL , ISNULL(F.PDK_ID_PERFIL,0) PDK_ID_PERFIL, ")
            strSQL.Append(" ISNULL(F.PDK_REL_TAR_PER_STATUS,3) PDK_REL_TAR_PER_STATUS ")

            strSQL.Append(" FROM PDK_CAT_TAREAS A ")
            strSQL.Append(" INNER JOIN PDK_CAT_PROCESOS B ON B.PDK_ID_PROCESOS = A.PDK_ID_PROCESOS ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA C ON C.PDK_ID_PARAMETROS_SISTEMA = A.PDK_TAR_ACTIVO ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA D ON D.PDK_ID_PARAMETROS_SISTEMA = A.PDK_TAR_PARALEL")
            strSQL.Append(" LEFT OUTER JOIN PDK_CAT_TAREAS E ON E.PDK_ID_TAREAS= A.PDK_TAR_PADRE")

            strSQL.Append(" LEFT OUTER JOIN PDK_REL_TAR_PERFIL F ON F.PDK_ID_TAREAS = A.PDK_ID_TAREAS AND (" & intPerfil & " IS NULL OR F.PDK_ID_PERFIL = " & intPerfil & ")")
            strSQL.Append("  WHERE  B.PDK_ID_FLUJOS=" & intflujo & "")


            If intProceso > 0 Then
                strSQL.Append(" AND A.PDK_ID_PROCESOS = " & intProceso)
            End If

            strSQL.Append(" ORDER BY A.PDK_TAR_ORDEN ")


            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_TAREAS")
            Throw objException
        End Try
    End Function

    Public Shared Function ConsultaTareas(Optional ByVal intProceso As Integer = 0, Optional ByVal intPerfil As Integer = 0, Optional ByVal intflujo As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_TAREAS,")
            strSQL.Append(" A.PDK_ID_PROCESOS,")
            strSQL.Append(" A.PDK_TAR_NOMBRE,")
            strSQL.Append(" A.PDK_TAR_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_TAR_ACTIVO,")
            strSQL.Append(" A.PDK_TAR_ORDEN,")
            strSQL.Append(" A.PDK_TAR_PARALEL,")
            strSQL.Append(" A.PDK_TAR_PADRE,")
            strSQL.Append(" B.PDK_PROC_NOMBRE,")
            strSQL.Append(" C.PDK_PAR_SIS_PARAMETRO,")
            strSQL.Append(" D.PDK_PAR_SIS_PARAMETRO AS 'PDK_PAR_SIS_PARAMETRO_PARALEL',")
            strSQL.Append(" CASE WHEN A.PDK_TAR_PADRE = 0 THEN 'NINGUNO' ELSE E.PDK_TAR_NOMBRE END AS 'PDK_TAR_PADRE_NOMBRE',")
            strSQL.Append(" A.PDK_ID_TAREAS_RECHAZO , A.PDK_ID_TAREAS_NORECHAZO, ")

            strSQL.Append(" ISNULL(F.PDK_ID_REL_TAR_PERFIL,0) PDK_ID_REL_TAR_PERFIL , ISNULL(F.PDK_ID_PERFIL,0) PDK_ID_PERFIL, ")
            strSQL.Append(" ISNULL(F.PDK_REL_TAR_PER_STATUS,3) PDK_REL_TAR_PER_STATUS ")

            strSQL.Append(" FROM PDK_CAT_TAREAS A ")
            strSQL.Append(" INNER JOIN PDK_CAT_PROCESOS B ON B.PDK_ID_PROCESOS = A.PDK_ID_PROCESOS ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA C ON C.PDK_ID_PARAMETROS_SISTEMA = A.PDK_TAR_ACTIVO ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA D ON D.PDK_ID_PARAMETROS_SISTEMA = A.PDK_TAR_PARALEL")
            strSQL.Append(" LEFT OUTER JOIN PDK_CAT_TAREAS E ON E.PDK_ID_TAREAS= A.PDK_TAR_PADRE")

            strSQL.Append(" LEFT OUTER JOIN PDK_REL_TAR_PERFIL F ON F.PDK_ID_TAREAS = A.PDK_ID_TAREAS AND (" & intPerfil & " IS NULL OR F.PDK_ID_PERFIL = " & intPerfil & ")")
            strSQL.Append("  WHERE  B.PDK_ID_FLUJOS=" & intflujo & "")


            If intProceso > 0 Then
                strSQL.Append(" AND A.PDK_ID_PROCESOS = " & intProceso)
            End If

            strSQL.Append(" ORDER BY A.PDK_TAR_NOMBRE ASC ")


            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_TAREAS")
            Throw objException
        End Try
    End Function

    Public Sub Guarda()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Try
            If intPDK_ID_TAREAS = 0 Then
                Me.intPDK_ID_TAREAS = BD.ObtenConsecutivo("PDK_ID_TAREAS", "PDK_CAT_TAREAS", Nothing)
                strSql = "INSERT INTO PDK_CAT_TAREAS " & _
                            "(" & _
                            "PDK_ID_TAREAS,PDK_ID_PROCESOS,PDK_TAR_NOMBRE,PDK_TAR_MODIF,PDK_CLAVE_USUARIO,PDK_TAR_ACTIVO,PDK_TAR_ORDEN,PDK_TAR_PARALEL,PDK_TAR_PADRE, PDK_ID_TAREAS_RECHAZO , PDK_ID_TAREAS_NORECHAZO,PDK_TAR_DICTAME)" & _
                            " VALUES ( " & intPDK_ID_TAREAS & ",  " & intPDK_ID_PROCESOS & ", '" & strPDK_TAR_NOMBRE & "','" & strPDK_TAR_MODIF & "', " & intPDK_CLAVE_USUARIO & ",  " & intPDK_TAR_ACTIVO & ",  " & intPDK_TAR_ORDEN & ",  " & intPDK_TAR_PARALEL & ",  " & intPDK_TAR_PADRE & ", " & intPDK_ID_TAREAS_RECHAZO & "," & intPDK_ID_TAREAS_NORECHAZO & "," & intPDK_TAR_DICTAME & " " & _
                            ")"
            Else
                ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
            PDK_ID_TAREAS = Me.intPDK_ID_TAREAS
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_CAT_TAREAS ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_CAT_TAREAS " & _
               "SET " & _
                " PDK_ID_PROCESOS = " & intPDK_ID_PROCESOS & ", " & _
                " PDK_TAR_NOMBRE = '" & strPDK_TAR_NOMBRE & "'," & _
                " PDK_TAR_MODIF = '" & strPDK_TAR_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
                " PDK_TAR_ACTIVO = " & intPDK_TAR_ACTIVO & ", " & _
                " PDK_TAR_ORDEN = " & intPDK_TAR_ORDEN & ", " & _
                " PDK_TAR_PARALEL = " & intPDK_TAR_PARALEL & ", " & _
                " PDK_TAR_PADRE = " & intPDK_TAR_PADRE & ", " & _
                " PDK_ID_TAREAS_RECHAZO = " & intPDK_ID_TAREAS_RECHAZO & "," & _
                " PDK_ID_TAREAS_NORECHAZO = " & intPDK_ID_TAREAS_NORECHAZO & "," & _
                " PDK_TAR_DICTAME = " & intPDK_TAR_DICTAME & " " & _
                " WHERE PDK_ID_TAREAS=  " & intPDK_ID_TAREAS
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_TAREAS")
        End Try
    End Sub


    Public Sub GuardaTareaPerfil()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Try

            If intPDK_ID_REL_TAR_PERFIL = 0 Then
                strSql = "INSERT INTO PDK_REL_TAR_PERFIL " & _
                        "(" & _
                        "PDK_ID_TAREAS,PDK_ID_PERFIL,PDK_REL_TAR_PER_MODIF,PDK_CLAVE_USUARIO,PDK_REL_TAR_PER_STATUS)" & _
                        " VALUES ( " & intPDK_ID_TAREAS & ",  " & intPDK_ID_PERFIL & ", '" & strPDK_TAR_MODIF & "', " & intPDK_CLAVE_USUARIO & ",  " & intPDK_TAR_ACTIVO & " " & _
                        ")"
            Else
                ActualizaRegistroPerfil()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_REL_TAR_PERFIL ")
        End Try
    End Sub

    Private Sub ActualizaRegistroPerfil()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_REL_TAR_PERFIL " & _
                           "SET " & _
            " PDK_ID_PERFIL = " & intPDK_ID_PERFIL & ", " & _
            " PDK_REL_TAR_PER_MODIF = '" & strPDK_TAR_MODIF & "'," & _
            " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
            " PDK_REL_TAR_PER_STATUS = " & intPDK_TAR_ACTIVO & " " & _
            " WHERE PDK_ID_REL_TAR_PERFIL=  " & intPDK_ID_REL_TAR_PERFIL

            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_TAREAS")
        End Try
    End Sub

    Public Shared Function ObtenTarea(Optional ByVal intProceso As Integer = 0, Optional ByVal intPerfil As Integer = 0, Optional ByVal intflujo As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.AppendLine("SELECT COUNT (*) AS CONTADOR, A.PDK_ID_TAREAS, B.PDK_PANT_NOMBRE, B.PDK_ID_PANTALLAS,B.PDK_PANT_MOSTRAR,")
            strSQL.AppendLine("SUM(CASE WHEN A.PDK_OPE_STATUS_PROCESO = '40' THEN 1 ELSE 0 END) PROCESO,")
            strSQL.AppendLine("SUM(CASE WHEN A.PDK_OPE_STATUS_PROCESO  = '41' THEN 1 ELSE 0 END) TERMINADA,")
            strSQL.AppendLine("SUM(CASE WHEN A.PDK_OPE_STATUS_PROCESO  = '42' THEN 1 ELSE 0 END) CANCELADA,")
            strSQL.AppendLine("SUM(CASE WHEN A.PDK_OPE_STATUS_PROCESO  = '118' THEN 1 ELSE 0 END) ACTIVO")
            strSQL.AppendLine("FROM PDK_OPE_SOLICITUD A")
            strSQL.AppendLine("INNER JOIN PDK_PANTALLAS B ON B.PDK_ID_PANTALLAS = A.PDK_ID_TAREAS")
            strSQL.AppendLine("INNER JOIN PDK_CAT_TAREAS C ON C.PDK_ID_TAREAS = A.PDK_ID_TAREAS")
            strSQL.AppendLine("LEFT OUTER JOIN PDK_CAT_PROCESOS D ON D.PDK_ID_PROCESOS = C.PDK_ID_PROCESOS")
            strSQL.AppendLine("WHERE D.PDK_ID_FLUJOS=" & intflujo & "")
            If intProceso > 0 Then
                strSQL.Append(" AND C.PDK_ID_PROCESOS = " & intProceso)
            End If
            strSQL.AppendLine("GROUP BY A.PDK_ID_TAREAS, B.PDK_PANT_NOMBRE, B.PDK_ID_PANTALLAS,B.PDK_PANT_MOSTRAR")
            strSQL.Append(" ORDER BY A.PDK_ID_TAREAS")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds

        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_ID_TAREAS")
            Throw objException
        End Try
    End Function

    Shared Function ObtenSolicitud(Optional ByVal intTarea As Integer = 0, Optional ByVal intStatus As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intTarea > 0 Then
                strSQL.AppendLine("SELECT A.PDK_ID_SOLICITUD,")
                strSQL.AppendLine("A.PDK_ID_TAREAS,")
                strSQL.AppendLine("A.PDK_OPE_MODIF, ")
                strSQL.AppendLine("A.PDK_OPE_STATUS_TAREA, ")
                strSQL.AppendLine("A.PDK_OPE_STATUS_PROCESO,")
                strSQL.AppendLine("A.PDK_OPE_FECHA_INICIO,")
                strSQL.AppendLine("B.PDK_TAR_NOMBRE,")
                strSQL.AppendLine("C.PDK_USU_CLAVE,")
                strSQL.AppendLine("C.PDK_USU_NOMBRE")
                strSQL.AppendLine("FROM PDK_OPE_SOLICITUD A")
                strSQL.AppendLine("INNER JOIN PDK_CAT_TAREAS B ON B.PDK_ID_TAREAS = A.PDK_ID_TAREAS")
                strSQL.AppendLine("INNER JOIN PDK_USUARIO C ON C.PDK_ID_USUARIO = A.PDK_CLAVE_USUARIO")
                strSQL.AppendLine("WHERE A.PDK_ID_TAREAS=" & intTarea & "")
                strSQL.AppendLine("AND A.PDK_OPE_STATUS_PROCESO = " & intStatus)
                strSQL.Append("  ORDER BY A.PDK_ID_SOLICITUD ASC ")
                Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
                Return ds
            End If

        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_ID_TAREAS")
            Throw objException
        End Try
    End Function
#End Region
    '-------------------------- FIN PDK_CAT_TAREAS-------------------------- 
End Class
