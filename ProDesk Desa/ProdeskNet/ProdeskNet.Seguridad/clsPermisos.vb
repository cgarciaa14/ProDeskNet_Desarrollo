'BUG-PD-197 : ERODRIGUEZ: 23/08/2017: Se realizo validacion para limitar menus disponibles.

Imports System.Data
Imports ProdeskNet.BD
Public Class clsPermisos
#Region "Variable"
    Private strErrPermisos As String = ""
    Private intIdPermiso As Integer = 0
    Private intIdMenu As Integer = 0
    Private intIdUsuario As Integer = 0
    Private intIdPerfil As Integer = 0
    Private intStatus As Integer = 0
    Private strModi As String = ""
    Private intCveUsu As Integer = 0
    Private strCveUsu As String = ""
    Private intIdPadre As Integer = 0
    Private intIdUsuarioReg As Integer = 0

#End Region
#Region "Propiedades"
    Sub New()
    End Sub
    Sub New(ByVal intCvePer As Integer, _
            Optional ByVal intCveObj As Integer = 0, _
            Optional ByVal strCveUsu As String = "", _
            Optional ByVal intCvePerf As Integer = 0)
        CargarPermiso(intCvePer, intCveObj, strCveUsu, intCvePerf)

    End Sub

    Public ReadOnly Property ErrorPermisos() As String
        Get
            Return strErrPermisos
        End Get
    End Property
    Public Property PDK_ID_MENU As Integer
        Get
            Return intIdMenu
        End Get
        Set(ByVal value As Integer)
            intIdMenu = value
        End Set
    End Property
    Public Property PDK_ID_PERMISOS As Integer
        Get
            Return intIdPermiso
        End Get
        Set(ByVal value As Integer)
            intIdPermiso = value
        End Set
    End Property
    Public Property PDK_ID_USUARIO As Integer
        Get
            Return intIdUsuario
        End Get
        Set(ByVal value As Integer)
            intIdUsuario = value
        End Set
    End Property

    Public Property PDK_ID_USUARIO_REG As Integer
        Get
            Return intIdUsuarioReg
        End Get
        Set(ByVal value As Integer)
            intIdUsuarioReg = value
        End Set
    End Property
    Public Property PDK_ID_PERFIL As Integer
        Get
            Return intIdPerfil
        End Get
        Set(ByVal value As Integer)
            intIdPerfil = value
        End Set
    End Property
    Public Property PDK_PER_STATUS As Integer
        Get
            Return intStatus
        End Get
        Set(ByVal value As Integer)
            intStatus = value
        End Set
    End Property
    Public Property PDK_PER_MODIF As String
        Get
            Return strModi
        End Get
        Set(ByVal value As String)
            strModi = value
        End Set
    End Property
    Public Property PDK_CLAVE_USUARIO As Integer
        Get
            Return intCveUsu
        End Get
        Set(ByVal value As Integer)
            intCveUsu = value
        End Set
    End Property

    Public Property PDK_USUARIO As String
        Get
            Return strCveUsu
        End Get
        Set(ByVal value As String)
            strCveUsu = value
        End Set
    End Property


    Public Property PDK_ID_PADRE As Integer
        Get
            Return intIdPadre
        End Get
        Set(ByVal value As Integer)
            intIdPadre = value
        End Set
    End Property

#End Region
#Region "Metodo"
    Public Sub CargarPermiso(Optional ByVal intPerm As Integer = 0, _
                            Optional ByVal intObj As Integer = 0, _
                            Optional ByVal strUsu As String = "", _
                            Optional ByVal intPerf As Integer = 0)
        Dim dtsRes As New DataSet
        Dim blnCarga As Boolean = False

        If intPerm > 0 Then blnCarga = True
        If intObj > 0 Then blnCarga = True
        If Trim(strUsu) <> "" Then blnCarga = True
        If intPerf > 0 Then blnCarga = True

        If blnCarga Then
            Try
                LimpiaPropiedades()
                intIdPermiso = intPerm
                intIdMenu = intObj
                strCveUsu = strUsu
                intIdPerfil = intPerf
                dtsRes = ManejaPermisos(1)

                LimpiaPropiedades()

                If Trim$(strErrPermisos) = "" Then
                    If dtsRes.Tables(0).Rows.Count > 0 Then
                        intIdPermiso = dtsRes.Tables(0).Rows(0).Item("PDK_ID_PERMISOS")
                        intIdMenu = dtsRes.Tables(0).Rows(0).Item("PDK_ID_MENU")
                        intCveUsu = dtsRes.Tables(0).Rows(0).Item("PDK_ID_USUARIO")
                        intIdPerfil = dtsRes.Tables(0).Rows(0).Item("PDK_ID_PERFIL")
                        intStatus = dtsRes.Tables(0).Rows(0).Item("PDK_PER_STATUS")
                    Else
                        strErrPermisos = "No se encontró información para poder cargar permiso"
                    End If
                End If
            Catch ex As Exception
                strErrPermisos = ex.Message
            End Try
        End If

    End Sub
    Public Function ManejaPermisos(ByVal intAcc As Integer) As DataSet
        ManejaPermisos = New DataSet
        strErrPermisos = ""
        Try
            Select Case intAcc
                Case 1 'Busca
                    ManejaPermisos = ObtenerPermiso(strCveUsu, intIdPermiso, intIdMenu, strErrPermisos)
                    Return ManejaPermisos
                Case 2 'Busca
                    ManejaPermisos = EstablecePermiso(strCveUsu, intIdPermiso, intIdMenu, strErrPermisos, intIdPerfil, intIdPadre)
                Case 3 ' Guarda
                    ManejaPermisos = GuardaPermiso(strCveUsu, intIdPermiso, intIdMenu, strErrPermisos, intIdPerfil, intIdPadre)
                Case 4 ' Actualiza
                    ManejaPermisos = ActualizaRegistro()

            End Select
        Catch ex As Exception

        End Try
    End Function

    Public Function GuardaPermiso( _
                                ByVal strUsuario As String, _
                                ByVal intPermiso As Integer, _
                                ByVal intMenu As Integer, _
                                ByRef strErro As String, _
                                Optional ByVal intPerfil As Integer = 0,
                                Optional ByVal intPadre As Integer = 0) As DataSet
        Try

            Dim strSql As String = String.Empty
            Dim strFechaOp As String = String.Empty
            Dim objException As Exception = Nothing
            Dim BD As New ProdeskNet.BD.clsManejaBD
            Try
                If intPermiso = 0 Then
                    'Me.strPDK_ID_PARAMETROS_SISTEMA = BD.ObtenConsecutivo("PDK_ID_PARAMETROS_SISTEMA", "PDK_PARAMETROS_SISTEMA", Nothing)
                    strSql = "INSERT INTO PDK_PERMISOS " & _
                        "(" & _
                            "PDK_ID_MENU, PDK_ID_USUARIO, PDK_ID_PERFIL,PDK_PER_STATUS,PDK_PER_MODIF,PDK_CLAVE_USUARIO)" & _
                             " VALUES (" & intMenu & ",  " & intCveUsu & ",  " & intPerfil & ",  " & intStatus & ", '" & strModi & "', " & intCveUsu & "  " & _
                        ")"
                Else
                    ActualizaRegistro()
                    Return Nothing
                    Exit Function
                End If
                BD.EjecutarQuery(strSql)
            Catch ex As Exception
                Throw New Exception("Error al guardar PDK_PERMISOS")
            End Try

        Catch ex As Exception

        End Try
        GuardaPermiso = Nothing
    End Function


    Public Function ActualizaRegistro() As DataSet

        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_PERMISOS " & _
            "SET " & _
            " PDK_ID_MENU = " & intIdMenu & ", " & _
            " PDK_ID_USUARIO = " & intIdUsuario & ", " & _
            " PDK_ID_PERFIL = " & intIdPerfil & ", " & _
            " PDK_PER_STATUS = " & intStatus & ", " & _
            " PDK_PER_MODIF = '" & strModi & "'," & _
            " PDK_CLAVE_USUARIO = " & intCveUsu & " " & _
            " WHERE PDK_ID_PERMISOS=  " & intIdPermiso

            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_PERMISOS ")
        End Try

        Return Nothing

    End Function


    Public Function EstablecePermiso( _
                                ByVal strUsuario As String, _
                                ByVal intpermiso As Integer, _
                                ByVal intMenu As Integer, _
                                ByRef strErro As String, _
                                Optional ByVal intPerfil As Integer = 0,
                                Optional ByVal intPadre As Integer = 0) As DataSet

        EstablecePermiso = New DataSet
        Dim strSQL As String = ""
        Dim ManejaBD As New clsManejaBD
        Try
            If BuscaPermiso(intIdUsuarioReg) Then
                strSQL = "CREATE TABLE #PERMISOS ("
                strSQL &= " PDK_ID_MENU INT,"
                strSQL &= " PDK_MEN_ID_PADRE INT,"
                strSQL &= " PDK_MEN_OBJETO INT,"
                strSQL &= " PDK_MEN_DESCRIPCION VARCHAR(200),"
                strSQL &= " PDK_ID_PERMISOS INT NULL, "
                strSQL &= " PDK_ID_PERFIL INT NULL,"
                strSQL &= " PDK_PER_STATUS INT NULL	"
                strSQL &= ")"
                strSQL &= ""
                strSQL &= "INSERT INTO #PERMISOS ("
                strSQL &= " PDK_ID_MENU,"
                strSQL &= " PDK_MEN_ID_PADRE,"
                strSQL &= " PDK_MEN_OBJETO,"
                strSQL &= " PDK_MEN_DESCRIPCION"
                strSQL &= ")"
                strSQL &= ""
                strSQL &= "SELECT "
                strSQL &= " PDK_ID_MENU,"
                strSQL &= " PDK_MEN_ID_PADRE,"
                strSQL &= " PDK_MEN_OBJETO,"
                strSQL &= " PDK_MEN_DESCRIPCION"
                strSQL &= " FROM PDK_MENU WHERE 1=1 "
            Else
                strSQL = "CREATE TABLE #PERMISOS ("
                strSQL &= " PDK_ID_MENU INT,"
                strSQL &= " PDK_MEN_ID_PADRE INT,"
                strSQL &= " PDK_MEN_OBJETO INT,"
                strSQL &= " PDK_MEN_DESCRIPCION VARCHAR(200),"
                strSQL &= " PDK_ID_PERMISOS INT NULL, "
                strSQL &= " PDK_ID_PERFIL INT NULL,"
                strSQL &= " PDK_PER_STATUS INT NULL	"
                strSQL &= ")"
                strSQL &= ""
                strSQL &= "INSERT INTO #PERMISOS ("
                strSQL &= " PDK_ID_MENU,"
                strSQL &= " PDK_MEN_ID_PADRE,"
                strSQL &= " PDK_MEN_OBJETO,"
                strSQL &= " PDK_MEN_DESCRIPCION"
                strSQL &= ")"
                strSQL &= ""
                strSQL &= "SELECT "
                strSQL &= " PDK_ID_MENU,"
                strSQL &= " PDK_MEN_ID_PADRE,"
                strSQL &= " PDK_MEN_OBJETO,"
                strSQL &= " PDK_MEN_DESCRIPCION"
                strSQL &= " FROM PDK_MENU WHERE 1=1 AND PDK_ID_MENU NOT IN (4,7,8,9,10,14,19,20,21,22,23,25,26,27,28,29,30,31,32,34,35,36,44,45,46,47)" 'SE OCOULTAN ALGUNOS MENUS
            End If


            If intPadre > 0 Then
                strSQL &= "AND PDK_MEN_ID_PADRE = " & intPadre & ""
            End If

            If intPadre < 0 Then
                strSQL &= "AND PDK_MEN_ID_PADRE = 0"
            End If

            If intMenu > 0 Then
                strSQL &= " AND PDK_ID_MENU = " & intMenu
            End If
            strSQL &= " ORDER BY PDK_ID_MENU "


            strSQL &= "UPDATE #PERMISOS"
            strSQL &= " SET PDK_ID_PERMISOS = 0, PDK_PER_STATUS = 3, PDK_ID_PERFIL = " & intPerfil & ""

            strSQL &= " UPDATE #PERMISOS"
            strSQL &= "  SET PDK_ID_PERMISOS = PDK_PERMISOS.PDK_ID_PERMISOS, "
            strSQL &= "     PDK_PER_STATUS = PDK_PERMISOS.PDK_PER_STATUS"
            strSQL &= "  FROM #PERMISOS "
            strSQL &= "  INNER JOIN PDK_PERMISOS ON "
            strSQL &= "   PDK_PERMISOS.PDK_ID_MENU = #PERMISOS.PDK_ID_MENU WHERE 1=1"

            If intPerfil > 0 Then
                strSQL &= " AND PDK_PERMISOS.PDK_ID_PERFIL = " & intPerfil & ""
            End If

            strSQL &= "SELECT PDK_ID_MENU, PDK_MEN_ID_PADRE, PDK_MEN_OBJETO, PDK_MEN_DESCRIPCION, PDK_ID_PERMISOS, PDK_ID_PERFIL, PDK_PER_STATUS FROM #PERMISOS WHERE 1=1 "

            If intMenu > 0 Then
                strSQL &= " AND (" & intMenu & " IS NULL OR PDK_ID_MENU=" & intMenu & ") "
            End If

            If intPerfil > 0 Then
                strSQL &= " AND PDK_ID_PERFIL = " & intPerfil & " "
            End If

            EstablecePermiso = ManejaBD.EjecutarQuery(strSQL)
            Return EstablecePermiso
        Catch ex As Exception

        End Try

    End Function



    Public Function ObtenerPermiso( _
                                    ByVal strUsuario As String, _
                                    ByVal intpermiso As Integer, _
                                    ByVal intMenu As Integer, _
                                    ByRef strErro As String, _
                                    Optional ByVal intPerfil As Integer = 0) As DataSet

        ObtenerPermiso = New DataSet
        Dim strSQL As String = ""
        Dim ManejaBD As New clsManejaBD
        Try
            strSQL = "SELECT A.PDK_ID_PERMISOS, A.PDK_ID_MENU, A.PDK_ID_USUARIO, A.PDK_ID_PERFIL, A.PDK_PER_STATUS, " & _
                     " A.PDK_PER_MODIF, A.PDK_CLAVE_USUARIO " & _
                     " FROM PDK_PERMISOS A " & _
                     " INNER JOIN PDK_PERFIL C ON C.PDK_ID_PERFIL = A.PDK_ID_PERFIL  AND C.PDK_PER_ACTIVO = 2" & _
                     " INNER JOIN PDK_REL_USU_PER E ON E.PDK_ID_PERFIL = C.PDK_ID_PERFIL " & _
                     " INNER JOIN PDK_USUARIO B ON B.PDK_ID_USUARIO = E.PDK_ID_USUARIO" & _
                    " WHERE 1=1 "

            If intMenu > 0 Then
                strSQL &= " AND (" & intMenu & " IS NULL OR A.PDK_ID_MENU=" & intMenu & ") "
            End If
            If strUsuario.Trim.Length > 0 Then
                strSQL &= " AND B.PDK_USU_CLAVE='" & strUsuario & "'"
            End If

            If intPerfil > 0 Then
                strSQL &= " AND C.PDK_ID_PERFIL = " & intPerfil & " "
            End If

            ObtenerPermiso = ManejaBD.EjecutarQuery(strSQL)
            Return ObtenerPermiso
        Catch ex As Exception

        End Try

    End Function
    Private Sub LimpiaPropiedades()
        strErrPermisos = ""
        intIdPermiso = 0
        intIdMenu = 0
        intIdUsuario = 0
        intIdPerfil = 0
        intStatus = 0
        strModi = ""
        intCveUsu = 0
    End Sub
    Function BuscaPermiso(usuario As Int64) As Boolean
        If (usuario > 0) Then

            Dim IdPerfil As Integer = 0
            Dim DSPerfil As DataSet
            Dim strSQL As String = ""
            Dim ManejaBD As New clsManejaBD
            Try
                strSQL = "SELECT A.PDK_ID_PERFIL " & _
                         " FROM PDK_PERFIL A " & _
                         " INNER JOIN PDK_REL_USU_PER E ON E.PDK_ID_PERFIL = A.PDK_ID_PERFIL " & _
                         " INNER JOIN PDK_USUARIO B ON B.PDK_ID_USUARIO = E.PDK_ID_USUARIO" & _
                        " where A.PDK_PER_ACTIVO = 2 AND B.PDK_ID_USUARIO = " & usuario & " "

                DSPerfil = ManejaBD.EjecutarQuery(strSQL)

                If DSPerfil.Tables.Count > 0 Then
                    If DSPerfil.Tables.Item(0).Rows.Count > 0 Then
                        IdPerfil = DSPerfil.Tables.Item(0).Rows(0).Item("PDK_ID_PERFIL")
                    End If
                End If
            Catch ex As Exception
            End Try

            If IdPerfil = 71 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function
#End Region

End Class
