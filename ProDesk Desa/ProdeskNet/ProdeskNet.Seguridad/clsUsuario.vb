Imports System.Data
Imports ProdeskNet.BD
Imports System.Text
''RQ-PI7-PD13-2 -13/11/2017- ERODRIGUEZ-Se agrego ejecucion de stored procedure que checa si hay tareas para asignar al usuario
' BUG-PD-311 : ERODRIGUEZ: 18/12/2017: Se corrigieron etiquetas de actividad al cambiar de pantalla
' RQ-PI7-PD13-3: ERODRIGUEZ: 22/12/2017: Se agrego funcion para cerrar sesion
' BUG-PD-327: DJUAREZ: 04/01/2018: Se modifican los estilos de la pagina u orden de los datos
' BUG-PD-329: ERODRIGUEZ: 05/01/2017: Se creo FUNCION AsignaUsuarioSol para la opcion 6 para asignacion de tareas a usuario
' RQ-PI7-PD13-4: ERODRIGUEZ: 10/01/2017: se verifica si el parametro del balanceador esta activo
Public Class clsUsuario
#Region "Variables"
    Private strCveUsuario As String = ""
    Private strCvePassword As String = ""
    Private intStatus As Integer = 0
    Private intVigencia As Integer = 0
    Private intIdUsuario As Integer = 0
    Private strNombre As String = ""
    Private strApeidoPat As String = ""
    Private strApeidoMat As String = ""
    Private strModi As String = ""
    Private strUltIngreso As String = ""
    Private intCveUsua As Integer = 0
    Private strErrorUsuario As String = ""
    Private intPerfil As Integer = 0
    Private strCorreo As String = ""

#End Region
#Region "Propiedad"

    Public Property PDK_USU_CLAVE() As String
        Get
            Return strCveUsuario

        End Get
        Set(ByVal value As String)
            strCveUsuario = value
        End Set
    End Property
    Public Property PDK_ID_PERFIL As Integer
        Get
            Return intPerfil
        End Get
        Set(ByVal value As Integer)
            intPerfil = value
        End Set
    End Property

    Public Property PDK_USU_CONTRASENA() As String
        Get
            Return strCvePassword

        End Get
        Set(ByVal value As String)
            strCvePassword = value
        End Set
    End Property

    Public Property PDK_USU_ACTIVO() As Integer
        Get
            Return intStatus

        End Get
        Set(ByVal value As Integer)
            intStatus = value
        End Set
    End Property

    Public Property PDK_USU_VIGENCIA() As Integer
        Get
            Return intVigencia

        End Get
        Set(ByVal value As Integer)
            intVigencia = value

        End Set
    End Property
    Public Property PDK_ID_USUARIO() As Integer
        Get
            Return intIdUsuario
        End Get
        Set(ByVal value As Integer)
            intIdUsuario = value
        End Set
    End Property
    Public Property PDK_USU_NOMBRE() As String
        Get
            Return strNombre
        End Get
        Set(ByVal value As String)
            strNombre = value
        End Set
    End Property
    Public Property PDK_USU_APE_PAT() As String
        Get
            Return strApeidoPat
        End Get
        Set(ByVal value As String)
            strApeidoPat = value
        End Set
    End Property
    Public Property PDK_USU_APE_MAT() As String
        Get
            Return strApeidoMat
        End Get
        Set(ByVal value As String)
            strApeidoMat = value
        End Set
    End Property
    Public Property PDK_USU_MODIF() As String
        Get
            Return strModi
        End Get
        Set(ByVal value As String)
            strModi = value
        End Set
    End Property
    Private Property PDK_USU_ULTINGRESO() As String
        Get
            Return strUltIngreso
        End Get
        Set(ByVal value As String)
            strUltIngreso = value
        End Set
    End Property
    Private Property PDK_CLAVE_USUARIO() As Integer
        Get
            Return intCveUsua
        End Get
        Set(ByVal value As Integer)
            intCveUsua = value
        End Set
    End Property
    Public Property PDK_USU_CORREO_ELECTRONICO As String
        Get
            Return strCorreo
        End Get
        Set(value As String)
            strCorreo = value

        End Set
    End Property


    Public ReadOnly Property ErrorUsuario() As String
        Get
            Return strErrorUsuario
        End Get
    End Property


#End Region
#Region "Metodos"
    Sub New()
    End Sub
    Sub New(ByVal strCveUsu As String)
        CargaUsuario(strCveUsu)
    End Sub

    Public Function ObtenerUsuario(ByVal strUsuario As String, ByRef strError As String) As DataSet
        Dim strSql As String = ""
        Dim ManejaBD As New clsManejaBD
        Try
            strSql = "SELECT 	A.PDK_ID_USUARIO,A.PDK_USU_NOMBRE,A.PDK_USU_APE_PAT,A.PDK_USU_APE_MAT,A.PDK_USU_ACTIVO,A.PDK_USU_MODIF,A.PDK_USU_CLAVE,A.PDK_USU_CONTRASENA,A.PDK_USU_ULTINGRESO,A.PDK_USU_VIGENCIA,A.PDK_CLAVE_USUARIO, D.PDK_ID_PERFIL,A.PDK_USU_CORREO_ELECTRONICO,A.ESTATUS_ACTIVO " & _
                      " FROM PDK_USUARIO A INNER JOIN PDK_REL_USU_PER C ON A.PDK_ID_USUARIO=C.PDK_ID_USUARIO  INNER JOIN PDK_PERFIL D ON D.PDK_ID_PERFIL =C.PDK_ID_PERFIL WHERE PDK_USU_CLAVE='" & strUsuario & "' "
            ObtenerUsuario = ManejaBD.EjecutarQuery(strSql)
            If ObtenerUsuario.Tables.Count > 0 AndAlso ObtenerUsuario.Tables(0).Rows.Count > 0 Then
                With ObtenerUsuario.Tables(0).Rows(0)
                    Me.strCveUsuario = .Item("PDK_USU_CLAVE").ToString.Trim
                    Me.strCvePassword = .Item("PDK_USU_CONTRASENA").ToString.Trim
                    Me.intStatus = .Item("PDK_USU_ACTIVO").ToString.Trim
                    Me.intVigencia = .Item("PDK_USU_VIGENCIA").ToString.Trim
                    Me.intIdUsuario = .Item("PDK_ID_USUARIO").ToString.Trim
                    Me.strNombre = .Item("PDK_USU_NOMBRE").ToString.Trim
                    Me.strApeidoPat = .Item("PDK_USU_APE_PAT").ToString.Trim
                    Me.strApeidoMat = .Item("PDK_USU_APE_MAT").ToString.Trim
                    Me.strModi = .Item("PDK_USU_MODIF").ToString.Trim
                    Me.strUltIngreso = .Item("PDK_USU_ULTINGRESO").ToString.Trim
                    Me.intCveUsua = .Item("PDK_CLAVE_USUARIO").ToString.Trim
                    Me.intPerfil = .Item("PDK_ID_PERFIL").ToString.Trim
                    Me.strCorreo = .Item("PDK_USU_CORREO_ELECTRONICO").ToString.Trim
                End With

                Return ObtenerUsuario
            End If

        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try

    End Function
    Public Shared Function obtenerValidadUsuario(ByVal strValidad As String, ByVal intBandera As Integer, ByVal intIdusuario As Integer) As DataSet
        Dim strSql As New StringBuilder
        Dim BD As New clsManejaBD
        Try
            If intBandera = 1 Then
                strSql.AppendLine("SELECT * FROM PDK_USUARIO WHERE PDK_USU_CLAVE='" & strValidad & "'")
            ElseIf intBandera = 2 Then
                strSql.AppendLine("SELECT * FROM PDK_USUARIO WHERE PDK_USU_CLAVE='" & strValidad & "' and PDK_ID_USUARIO<>" & intIdusuario)
            ElseIf intBandera = 3 Then
                strSql.AppendLine("SELECT * FROM PDK_USUARIO WHERE PDK_ID_USUARIO =" & intIdusuario)
            End If

            obtenerValidadUsuario = BD.EjecutarQuery(strSql.ToString)
            Return obtenerValidadUsuario
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros ")
        End Try

    End Function
    Public Shared Function ObtenerDistriModif(ByVal CVE As Integer) As DataSet
        Dim strSql As New StringBuilder
        Dim BD As New clsManejaBD
        Try
            strSql.AppendLine("CREATE TABLE #TEMPORAL (PDK_ID_DISTRIBUIDOR INT,PDK_DIST_CLAVE VARCHAR(20), PDK_DIST_NOMBRE VARCHAR(200),PDK_REL_USU_DIST_ACTIVO INT)")
            strSql.AppendLine("INSERT INTO #TEMPORAL(PDK_ID_DISTRIBUIDOR ,PDK_DIST_CLAVE , PDK_DIST_NOMBRE ,PDK_REL_USU_DIST_ACTIVO)")
            strSql.AppendLine("SELECT PDK_ID_DISTRIBUIDOR ,PDK_DIST_CLAVE ,PDK_DIST_NOMBRE,0  FROM PDK_CAT_DISTRIBUIDOR")
            strSql.AppendLine("UPDATE #TEMPORAL SET PDK_REL_USU_DIST_ACTIVO = 2")
            strSql.AppendLine("FROM #TEMPORAL A   WHERE A.PDK_ID_DISTRIBUIDOR IN(SELECT PDK_ID_DISTRIBUIDOR  FROM PDK_REL_USU_DIST WHERE PDK_ID_USUARIO=" & CVE & ")")
            strSql.AppendLine("SELECT * FROM #TEMPORAL ORDER BY PDK_REL_USU_DIST_ACTIVO DESC")
            strSql.AppendLine("DROP TABLE #TEMPORAL")


            ObtenerDistriModif = BD.EjecutarQuery(strSql.ToString)
            Return ObtenerDistriModif
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros ")
        End Try

    End Function
    Public Shared Function ObtenerNombreperfi(ByVal intperfil As Integer) As DataSet
        Dim strSql As String = ""
        Dim BD As New clsManejaBD
        Try
            strSql = "SELECT PDK_PER_NOMBRE FROM PDK_PERFIL WHERE PDK_ID_PERFIL=" & intperfil & " AND PDK_PER_ACTIVO=2"
            ObtenerNombreperfi = BD.EjecutarQuery(strSql)
            Return ObtenerNombreperfi
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros ")
        End Try

    End Function
    Public Shared Function ObtenerDistribudir() As DataSet
        Dim strSql As String = ""
        Dim BD As New clsManejaBD
        Try
            strSql = "SELECT PDK_ID_DISTRIBUIDOR,PDK_DIST_CLAVE ,PDK_DIST_NOMBRE FROM PDK_CAT_DISTRIBUIDOR"
            ObtenerDistribudir = BD.EjecutarQuery(strSql)
            Return ObtenerDistribudir
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros ")
        End Try
    End Function
    Public Shared Function GuardarDistribuidora(ByVal cve As Integer, ByVal cveusu As Integer, ByVal intStatusDistri As Integer, ByVal intusuarios As Integer) As Integer
        Dim strSql As New StringBuilder
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim intConsecutivo As Integer = 0
        Dim cveDistri As Integer = 0
        Try
            If cve > 0 Then
                If intStatusDistri = 2 Then
                    cveDistri = BD.ObtenConsecutivo("PDK_ID_REL_USU_DIST", "PDK_REL_USU_DIST", Nothing)
                    If cveDistri > 0 Then
                        strSql.AppendLine("IF  NOT  EXISTS   (SELECT * FROM PDK_REL_USU_DIST WHERE PDK_ID_DISTRIBUIDOR= " & cve & " AND PDK_ID_USUARIO= " & cveusu & " )")
                        strSql.AppendLine("BEGIN")
                        strSql.AppendLine("INSERT INTO PDK_REL_USU_DIST (PDK_ID_REL_USU_DIST ,PDK_ID_USUARIO,  PDK_REL_USU_DIST_ACTIVO,PDK_REL_USU_DIST_MODIF,PDK_CLAVE_USUARIO,PDK_ID_DISTRIBUIDOR)")
                        strSql.AppendLine("VALUES(" & cveDistri & "," & cveusu & "," & intStatusDistri & ", " & Format(Now(), "yyyy-MM-dd") & ", " & intusuarios & " , " & cve & ")  ")
                        strSql.AppendLine("END")
                        strSql.AppendLine("ELSE")
                        strSql.AppendLine("BEGIN")
                        strSql.AppendLine("UPDATE PDK_REL_USU_DIST SET PDK_REL_USU_DIST_ACTIVO=" & intStatusDistri & ",PDK_CLAVE_USUARIO=" & intusuarios & ",PDK_REL_USU_DIST_MODIF=GETDATE() WHERE PDK_ID_DISTRIBUIDOR=" & cve & " AND PDK_ID_USUARIO=" & cveusu & " ")
                        strSql.AppendLine("END")
                    End If
                Else
                    strSql.AppendLine("DELETE PDK_REL_USU_DIST WHERE PDK_ID_DISTRIBUIDOR=" & cve & " AND PDK_ID_USUARIO=" & cveusu & " ")
                End If
                BD.EjecutarQuery(strSql.ToString)
                GuardarDistribuidora = 1
                Return GuardarDistribuidora
            End If

        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_ID_USUARIO ")
        End Try

    End Function
    Public Sub Guardar()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim intConsecutivo As Integer = 0
        Try
            If intIdUsuario = 0 Then
                Me.intIdUsuario = BD.ObtenConsecutivo("PDK_ID_USUARIO", "PDK_USUARIO", Nothing)
                strSql = "INSERT INTO PDK_USUARIO " & _
                                "(" & _
                        "PDK_ID_USUARIO,PDK_USU_NOMBRE,PDK_USU_APE_PAT,PDK_USU_APE_MAT,PDK_USU_CLAVE,PDK_USU_CONTRASENA,PDK_USU_MODIF,PDK_USU_ULTINGRESO,PDK_USU_VIGENCIA,PDK_CLAVE_USUARIO,PDK_USU_ACTIVO,PDK_USU_CORREO_ELECTRONICO)" & _
                        " VALUES ( " & intIdUsuario & ", '" & PDK_USU_NOMBRE & "','" & PDK_USU_APE_PAT & "', '" & PDK_USU_APE_MAT & "','" & PDK_USU_CLAVE & "','" & PDK_USU_CONTRASENA & "','" & PDK_USU_MODIF & "' " & _
                                  ",'" & PDK_USU_MODIF & "'," & PDK_USU_VIGENCIA & "," & intIdUsuario & "," & PDK_USU_ACTIVO & ",'" & PDK_USU_CORREO_ELECTRONICO & "')"

            End If
            BD.EjecutarQuery(strSql)
            Me.PDK_ID_USUARIO = Me.intIdUsuario

            strSql = String.Empty
            strSql = " INSERT INTO PDK_REL_USU_PER  (PDK_ID_USUARIO, PDK_ID_PERFIL, PDK_REL_MODIF,  PDK_CLAVE_USUARIO)" & _
                    " VALUES (" & intIdUsuario & "," & PDK_ID_PERFIL & ",'" & PDK_USU_MODIF & "'," & intIdUsuario & ")"
            BD.EjecutarQuery(strSql)


        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_ID_USUARIO ")
        End Try

    End Sub
    Public Function ActualizarContra(ByVal intuSUARIO As Integer) As Integer
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_USUARIO " & _
               "SET " & _
                " PDK_USU_CONTRASENA= '" & PDK_USU_CONTRASENA & "', PDK_USU_ULTINGRESO = GETDATE() " & _
                " WHERE PDK_ID_USUARIO=  " & intuSUARIO
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_PERFIL")
        End Try

    End Function
    Public Function Actualizar(Optional ByVal intIdUsuario As Integer = 0) As Integer
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_USUARIO " & _
               "SET " & _
                " PDK_USU_NOMBRE = '" & PDK_USU_NOMBRE & "'," & _
                " PDK_USU_APE_PAT= '" & PDK_USU_APE_PAT & "', " & _
                "PDK_USU_APE_MAT= '" & PDK_USU_APE_MAT & "', " & _
                " PDK_USU_MODIF ='" & PDK_USU_MODIF & "'," & _
                " PDK_USU_ACTIVO = " & PDK_USU_ACTIVO & ", " & _
                " PDK_USU_CLAVE ='" & PDK_USU_CLAVE & "', " & _
                " PDK_USU_CORREO_ELECTRONICO= '" & PDK_USU_CORREO_ELECTRONICO & "' " & _
                " WHERE PDK_ID_USUARIO=  " & intIdUsuario
            BD.EjecutarQuery(strSql)

            strSql = String.Empty
            strSql = "UPDATE PDK_REL_USU_PER " & _
                   "SET " & _
                 " PDK_ID_PERFIL = '" & PDK_ID_PERFIL & "'," & _
                 " PDK_REL_MODIF = '" & PDK_USU_MODIF & "'" & _
                 " WHERE  PDK_ID_USUARIO=  " & intIdUsuario
            BD.EjecutarQuery(strSql)

            Actualizar = 1

        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_PERFIL")
        End Try
    End Function
    Public Shared Function ObtenerTodo(Optional ByVal intperfil As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        ObtenerTodo = New DataSet
        Dim strSql As String = ""
        Dim ManejaBD As New clsManejaBD
        Try
            strSql = "SELECT A.PDK_ID_USUARIO,A.PDK_USU_NOMBRE,A.PDK_USU_APE_MAT,A.PDK_USU_APE_PAT,A.PDK_USU_CLAVE,A.PDK_USU_CONTRASENA,A.PDK_USU_MODIF,A.PDK_USU_ULTINGRESO,A.PDK_USU_VIGENCIA,B.PDK_PAR_SIS_PARAMETRO,A.PDK_USU_CORREO_ELECTRONICO " & _
                      "FROM PDK_USUARIO A INNER JOIN PDK_PARAMETROS_SISTEMA B ON  B.PDK_ID_PARAMETROS_SISTEMA=A.PDK_USU_ACTIVO " & _
                       "INNER JOIN PDK_REL_USU_PER C ON A.PDK_ID_USUARIO=C.PDK_ID_USUARIO  INNER JOIN PDK_PERFIL D ON D.PDK_ID_PERFIL =C.PDK_ID_PERFIL "

            If intperfil > 0 Then
                strSql &= " WHERE C.PDK_ID_PERFIL = " & intperfil & " ORDER BY  A.PDK_USU_NOMBRE ASC"
            End If
            ObtenerTodo = ManejaBD.EjecutarQuery(strSql)
            Return ObtenerTodo
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros PDK_USU_CLAVE")
            Throw objException
        End Try

    End Function
    Public Shared Function ObtenerUsuarioCve(ByVal intIdusua As Integer) As DataSet
        ObtenerUsuarioCve = New DataSet
        Dim objException As Exception = Nothing
        Dim strSql As String = ""
        Dim ManejaBD As New clsManejaBD
        Try
            strSql = "SELECT A.PDK_ID_USUARIO,A.PDK_USU_NOMBRE,A.PDK_USU_APE_MAT,A.PDK_USU_APE_PAT,A.PDK_USU_CLAVE,A.PDK_USU_CONTRASENA,A.PDK_USU_MODIF,A.PDK_USU_ULTINGRESO,A.PDK_USU_VIGENCIA,A.PDK_USU_ACTIVO,B.PDK_PAR_SIS_PARAMETRO,D.PDK_PER_NOMBRE,D.PDK_ID_PERFIL,A.PDK_USU_CLAVE, A.PDK_USU_CORREO_ELECTRONICO " & _
                     " FROM PDK_USUARIO A INNER JOIN PDK_PARAMETROS_SISTEMA B ON  B.PDK_ID_PARAMETROS_SISTEMA=A.PDK_USU_ACTIVO  INNER JOIN PDK_REL_USU_PER C ON A.PDK_ID_USUARIO=C.PDK_ID_USUARIO " & _
                     " INNER JOIN PDK_PERFIL D ON D.PDK_ID_PERFIL =C.PDK_ID_PERFIL WHERE(A.PDK_ID_USUARIO =" & intIdusua & ")"
            ObtenerUsuarioCve = ManejaBD.EjecutarQuery(strSql)
            Return ObtenerUsuarioCve
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros PDK_ID_USUARIO")
            Throw objException
        End Try

    End Function

    Public Sub CargaUsuario(ByVal strCveUsu As String)
        strErrorUsuario = ""
        Dim dtsResul As DataSet = Nothing
        Dim strSql As String = ""

        If Trim(strCveUsu) <> "" Then
            Try
                LimpiarPropiedad()
                strCveUsuario = strCveUsu
                dtsResul = ManejaUsuario(1)
            Catch ex As Exception
                strErrorUsuario = ex.Message
            End Try

        End If
    End Sub

    Public Function ManejaUsuario(ByVal intAccion As Integer) As DataSet
        ManejaUsuario = New DataSet
        strErrorUsuario = ""
        Dim ManejaBD As New clsManejaBD
        Try
            Select Case intAccion
                Case 1
                    ObtenerUsuario(strCveUsuario, strErrorUsuario)
                Case 2
                    Guardar()
                Case 3
                    Actualizar(PDK_ID_USUARIO)
                Case 4
                    ActualizarContra(PDK_ID_USUARIO)

            End Select

        Catch ex As Exception
            strErrorUsuario = ex.Message
        End Try

    End Function


    Public Sub LimpiarPropiedad()
        strCveUsuario = ""
        strCvePassword = ""
        intStatus = 0
        intVigencia = 0
        intIdUsuario = 0
        strNombre = ""
        strApeidoPat = ""
        strApeidoMat = ""
        strModi = ""
        strUltIngreso = ""
        intCveUsua = 0
        strErrorUsuario = ""
        strCorreo = ""

    End Sub

    Public Shared Function InicioSesionEstatus(ByVal USRINT As Integer) As DataSet
        Dim ManejaBD As New clsManejaBD
        '' 13/11/2017- ERODRIGUEZ-Aqui va el codigo que ejecuta el stored procedure que checa si hay tareas para asignar al usuario y actualiz el estatus a 1
        Dim dsresult As DataSet
        Dim cadena As String = "EXEC spManejaTareaUsuario " & 0 & ", " & USRINT & ", " & 3 & ", " & 0 & ", " & 0 & ""
        dsresult = ManejaBD.EjecutarQuery("EXEC spManejaTareaUsuario " & 0 & ", " & USRINT & ", " & 3 & ", " & 0 & ", " & 0 & "")
        Return dsresult
        ''fin
    End Function

    Public Shared Function CierraSesionEstatus(ByVal USRINT As Integer) As DataSet
        Dim ManejaBD As New clsManejaBD
        '' 22/12/2017- ERODRIGUEZ-Aqui va el codigo que ejecuta el stored procedure que actuakiza el usuario a inactivo y pasa sus tareas asignadas al usuario virtual.
        Dim dsresult As DataSet
        Dim cadena As String = "EXEC spManejaTareaUsuario " & 0 & ", " & USRINT & ", " & 5 & ", " & 0 & ", " & 0 & ""
        dsresult = ManejaBD.EjecutarQuery("EXEC spManejaTareaUsuario " & 0 & ", " & USRINT & ", " & 5 & ", " & 0 & ", " & 0 & "")
        Return dsresult
        ''fin
    End Function

    Public Shared Function AsignaUsuarioSol(ByVal USRINT As Integer) As DataSet
        Dim ManejaBD As New clsManejaBD
        '' 05/01/2017- ERODRIGUEZ-Aqui va el codigo que ejecuta el stored procedure que checa si hay tareas para asignar al usuario
        Dim dsresult As DataSet
        Dim cadena As String = "EXEC spManejaTareaUsuario " & 0 & ", " & USRINT & ", " & 6 & ", " & 0 & ", " & 0 & ""
        dsresult = ManejaBD.EjecutarQuery("EXEC spManejaTareaUsuario " & 0 & ", " & USRINT & ", " & 6 & ", " & 0 & ", " & 0 & "")
        Return dsresult
        ''fin
    End Function

    Public Shared Function ObtieneBalanceador() As DataSet
        Dim ManejaBD As New clsManejaBD
        Dim dsresult As DataSet
        'Dim cadena As String = "SELECT PDK_PAR_SIS_VALOR_NUMERO as  balanceo FROM PDK_PARAMETROS_SISTEMA WHERE PDK_ID_PARAMETROS_SISTEMA = 418"
        dsresult = ManejaBD.EjecutarQuery("SELECT PDK_PAR_SIS_VALOR_NUMERO as  balanceo FROM PDK_PARAMETROS_SISTEMA WHERE PDK_ID_PARAMETROS_SISTEMA = 418")
        Return dsresult
    End Function

#End Region




End Class
