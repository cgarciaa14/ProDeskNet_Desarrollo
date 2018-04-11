
Imports ProdeskNet.BD
Imports System.Text

Public Class clsMenu
#Region "Variable"
    Private strErrObjetoSis As String = ""
    Private intIdMenu As Integer = 0
    Private intMenuObj As Integer = 0
    Private strMenuDescrip As String = ""
    Private strMenuLink As String = ""
    Private intMenuPadre As Integer = 0
    Private intMenuPermiso As Integer = 0
    Private intMenuNivel As Integer = 0
    Private intMenuAcceso As Integer = 0
    Private strMenModi As String = ""
    Private intCveUsuario As Integer = 0

#End Region
#Region "Propiedad"
    Sub New()
    End Sub
    Sub New(ByVal intCveObj As Integer)
        CargaObjetoSis(intCveObj)
    End Sub
    Sub New(ByVal intCveObj As Integer, ByVal intIdMenu As Integer)
        CargaMenuRegistro(intIdMenu)
    End Sub

    Public ReadOnly Property ErrorObjeto() As String
        Get
            Return strErrObjetoSis
        End Get
    End Property
    Public Property PDK_ID_MENU() As Integer
        Get
            Return intIdMenu
        End Get
        Set(ByVal value As Integer)
            intIdMenu = value
        End Set
    End Property
    Public Property PDK_MEN_OBJETO() As Integer
        Get
            Return intMenuObj
        End Get
        Set(ByVal value As Integer)
            intMenuObj = value
        End Set
    End Property
    Public Property PDK_MEN_DESCRIPCION As String
        Get
            Return strMenuDescrip
        End Get
        Set(ByVal value As String)
            strMenuDescrip = value
        End Set
    End Property
    Public Property PDK_MEN_LINK As String
        Get
            Return strMenuLink
        End Get
        Set(ByVal value As String)
            strMenuLink = value
        End Set
    End Property
    Public Property PDK_MEN_ID_PADRE As Integer
        Get
            Return intMenuPadre
        End Get
        Set(ByVal value As Integer)
            intMenuPadre = value
        End Set
    End Property
    Public Property PDK_MEN_TIPO_PERMISO As Integer
        Get
            Return intMenuPermiso
        End Get
        Set(ByVal value As Integer)
            intMenuPermiso = value
        End Set
    End Property
    Public Property PDK_MEN_NIVEL As Integer
        Get
            Return intMenuNivel
        End Get
        Set(ByVal value As Integer)
            intMenuNivel = value
        End Set
    End Property
    Public Property PDK_MEN_ACCESO_DIRECTO As Integer
        Get
            Return intMenuAcceso
        End Get
        Set(ByVal value As Integer)
            intMenuAcceso = value
        End Set
    End Property
    Public Property PDK_MEN_MODIF As String
        Get
            Return strMenModi
        End Get
        Set(ByVal value As String)
            strMenModi = value
        End Set
    End Property
    Public Property PDK_CLAVE_USUARIO As Integer
        Get
            Return intCveUsuario
        End Get
        Set(ByVal value As Integer)
            intCveUsuario = value
        End Set
    End Property
#End Region
#Region "Metodo"

    Public Sub CargaObjetoSis(Optional ByVal strCveMenu As Integer = 0)
        strErrObjetoSis = ""
        Dim dtsRes As New DataSet
        If strCveMenu > 0 Then
            Try
                LimpiarPropiedades()
                intMenuObj = strCveMenu
                dtsRes = ManejaMenu(1)
                intMenuObj = 0
                If dtsRes.Tables.Count > 0 AndAlso dtsRes.Tables(0).Rows.Count > 0 Then
                    With dtsRes.Tables(0).Rows(0)
                        Me.intIdMenu = .Item("PDK_ID_MENU").ToString.Trim
                        Me.intMenuObj = .Item("PDK_MEN_OBJETO").ToString.Trim
                        Me.strMenuDescrip = .Item("PDK_MEN_DESCRIPCION").ToString.Trim
                        Me.strMenuLink = .Item("PDK_MEN_LINK").ToString.Trim
                        Me.intMenuPadre = .Item("PDK_MEN_ID_PADRE").ToString.Trim
                        Me.intMenuPermiso = .Item("PDK_MEN_TIPO_PERMISO").ToString.Trim
                        Me.intMenuNivel = .Item("PDK_MEN_NIVEL").ToString.Trim
                        Me.intMenuAcceso = .Item("PDK_MEN_ACCESO_DIRECTO").ToString.Trim
                        Me.strMenModi = .Item("PDK_MEN_MODIF").ToString.Trim
                        Me.intCveUsuario = .Item("PDK_CLAVE_USUARIO").ToString.Trim
                    End With
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub
    Public Sub CargaMenuRegistro(Optional ByVal intCveMenu As Integer = 0)
        strErrObjetoSis = ""
        Dim dtsRes As New DataSet
        If intCveMenu > 0 Then
            Try
                LimpiarPropiedades()
                intIdMenu = intCveMenu
                dtsRes = ManejaMenu(2)
                If dtsRes.Tables.Count > 0 AndAlso dtsRes.Tables(0).Rows.Count > 0 Then
                    With dtsRes.Tables(0).Rows(0)
                        Me.intIdMenu = .Item("PDK_ID_MENU").ToString.Trim
                        Me.intMenuObj = .Item("PDK_MEN_OBJETO").ToString.Trim
                        Me.strMenuDescrip = .Item("PDK_MEN_DESCRIPCION").ToString.Trim
                        Me.strMenuLink = .Item("PDK_MEN_LINK").ToString.Trim
                        Me.intMenuPadre = .Item("PDK_MEN_ID_PADRE").ToString.Trim
                        Me.intMenuPermiso = .Item("PDK_MEN_TIPO_PERMISO").ToString.Trim
                        Me.intMenuNivel = .Item("PDK_MEN_NIVEL").ToString.Trim
                        Me.intMenuAcceso = .Item("PDK_MEN_ACCESO_DIRECTO").ToString.Trim
                        Me.strMenModi = .Item("PDK_MEN_MODIF").ToString.Trim
                        Me.intCveUsuario = .Item("PDK_CLAVE_USUARIO").ToString.Trim
                    End With
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub
    Public Sub LimpiarPropiedades()
        strErrObjetoSis = ""
        intIdMenu = 0
        intMenuObj = 0
        strMenuDescrip = ""
        strMenuLink = ""
        intMenuPadre = 0
        intMenuPermiso = 0
        intMenuNivel = 0
        intMenuAcceso = 0
        strMenModi = ""
        intCveUsuario = 0

    End Sub
    Public Function ManejaMenu(ByVal intAcci As Integer) As DataSet
        strErrObjetoSis = ""
        ManejaMenu = New DataSet
        Dim ManejaDB As New clsManejaBD
        Try
            Select Case intAcci
                Case 1
                    ManejaMenu = ObtenerMenu(intMenuObj, strErrObjetoSis)
                    Return ManejaMenu
                Case 2
                    ManejaMenu = ObtenMenuRegistro(intIdMenu, strErrObjetoSis)
                Case 3
                Case 4

            End Select
        Catch ex As Exception

        End Try

    End Function
    Public Function ObtenerMenu(ByVal intCveOjet As Integer, ByRef strsErr As String) As DataSet
        ObtenerMenu = New DataSet
        Dim strSQL As String = ""
        Dim ManejaBD As New clsManejaBD

        Try
            strErrObjetoSis = ManejaBD.ErrorBD
            If Trim(strErrObjetoSis) <> "" Then Exit Function
            strSQL = " SELECT A.PDK_ID_MENU,A.PDK_MEN_OBJETO,A.PDK_MEN_DESCRIPCION,ISNULL(A.PDK_MEN_LINK,'') AS PDK_MEN_LINK," & _
                    " A.PDK_MEN_ID_PADRE,A.PDK_MEN_TIPO_PERMISO,A.PDK_MEN_NIVEL,A.PDK_MEN_ACCESO_DIRECTO,A.PDK_MEN_MODIF, " & _
                    " A.PDK_CLAVE_USUARIO " & _
                    " FROM PDK_MENU A INNER JOIN  PDK_PARAMETROS_SISTEMA C ON (A.PDK_MEN_OBJETO =C.PDK_ID_PARAMETROS_SISTEMA)  " & _
                    " WHERE  A.PDK_MEN_OBJETO=" & intCveOjet & " "

            If intMenuPadre > 0 Then
                strSQL &= " AND A.PDK_MEN_ID_PADRE = " & intMenuPadre
            End If

            ObtenerMenu = ManejaBD.EjecutarQuery(strSQL)
            Return ObtenerMenu


        Catch ex As Exception
            Throw New Exception("Error al obtener el Menu")
        End Try

    End Function
    Public Function ObtenMenuRegistro(ByVal intIdMenu As Integer, ByRef strsErr As String) As DataSet
        ObtenMenuRegistro = New DataSet
        Dim strSQL As String = ""
        Dim ManejaBD As New clsManejaBD

        Try
            strErrObjetoSis = ManejaBD.ErrorBD
            If Trim(strErrObjetoSis) <> "" Then Exit Function
            strSQL = " SELECT A.PDK_ID_MENU,A.PDK_MEN_OBJETO,A.PDK_MEN_DESCRIPCION,ISNULL(A.PDK_MEN_LINK,'') AS PDK_MEN_LINK," & _
                    " A.PDK_MEN_ID_PADRE,A.PDK_MEN_TIPO_PERMISO,A.PDK_MEN_NIVEL,A.PDK_MEN_ACCESO_DIRECTO,A.PDK_MEN_MODIF, " & _
                    " A.PDK_CLAVE_USUARIO " & _
                    " FROM PDK_MENU A INNER JOIN  PDK_PARAMETROS_SISTEMA C ON (A.PDK_MEN_OBJETO =C.PDK_ID_PARAMETROS_SISTEMA)  " & _
                    " WHERE A.PDK_ID_MENU=" & intIdMenu & " "

            If intMenuPadre > 0 Then
                strSQL &= " AND A.PDK_MEN_ID_PADRE = " & intMenuPadre
            End If

            ObtenMenuRegistro = ManejaBD.EjecutarQuery(strSQL)
            Return ObtenMenuRegistro
        Catch ex As Exception
            Throw New Exception("Error al obtener el Menu")
        End Try

    End Function
    Public Shared Function ObtenTodos() As DataSet

        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim ManejaBD As New clsManejaBD

        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_MENU,")
            strSQL.Append(" A.PDK_MEN_OBJETO,")
            strSQL.Append(" A.PDK_MEN_DESCRIPCION,")
            strSQL.Append(" A.PDK_MEN_LINK,")
            strSQL.Append(" A.PDK_MEN_ID_PADRE,")
            strSQL.Append(" A.PDK_MEN_TIPO_PERMISO,")
            strSQL.Append(" A.PDK_MEN_NIVEL,")
            strSQL.Append(" A.PDK_MEN_ACCESO_DIRECTO,")
            strSQL.Append(" A.PDK_MEN_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO")
            strSQL.Append(" FROM PDK_MENU A ")
            Dim ds As DataSet = ManejaBD.EjecutarQuery(strSQL.ToString)
            Return ds

        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_MENU")
            Throw objException
        End Try

    End Function
    Public Shared Function ObtenTodosOrdenadosPadre() As DataSet

        Dim objException As Exception = Nothing
        Dim strSQl As String = String.Empty
        Dim ManejaBD As New clsManejaBD

        Try


            strSQl = "CREATE TABLE #PDK_MENU"
            strSQl &= " (		"
            strSQl &= " ID_MENU INTEGER IDENTITY (1,1),"
            strSQl &= " PDK_ID_MENU INTEGER, "
            strSQl &= " PDK_MEN_OBJETO INTEGER, "
            strSQl &= " PDK_MEN_DESCRIPCION VARCHAR(200), "
            strSQl &= " PDK_MEN_LINK VARCHAR(200), "
            strSQl &= " PDK_MEN_ID_PADRE INTEGER, "
            strSQl &= " PDK_PAR_SIS_PARAMETRO VARCHAR(200),"
            strSQl &= " PDK_PAD_DESCRIPCION  VARCHAR(200)"
            strSQl &= " )"

            strSQl &= " CREATE TABLE #PDK_PADRE"
            strSQl &= " ("
            strSQl &= " PDK_ID_PADRE INTEGER IDENTITY(1,1),"
            strSQl &= " PDK_ID_MENU INTEGER, "
            strSQl &= " PDK_PAD_OBJETO INTEGER, "
            strSQl &= " PDK_PAD_DESCRIPCION VARCHAR(200)"
            strSQl &= " )"

            strSQl &= " INSERT INTO #PDK_PADRE"
            strSQl &= " SELECT PDK_ID_MENU, PDK_MEN_OBJETO, PDK_MEN_DESCRIPCION FROM PDK_MENU WHERE PDK_MEN_OBJETO = 5 " & _
                " ORDER BY PDK_ID_MENU"


            strSQl &= " DECLARE @CONTADOR INT"
            strSQl &= " DECLARE @CONTADOR_REGISTROS INT"
            strSQl &= " DECLARE @PDK_ID_MENU INT"

            strSQl &= " SET @CONTADOR = 1"
            strSQl &= " SET @CONTADOR_REGISTROS = 0 "

            strSQl &= " SELECT @CONTADOR_REGISTROS = COUNT(*) FROM #PDK_PADRE "

            strSQl &= " WHILE @CONTADOR <= @CONTADOR_REGISTROS"
            strSQl &= " BEGIN"
            strSQl &= "     SELECT @PDK_ID_MENU = PDK_ID_MENU FROM #PDK_PADRE WHERE PDK_ID_PADRE = @CONTADOR "
            strSQl &= "     INSERT INTO #PDK_MENU "
            strSQl &= "     SELECT A.PDK_ID_MENU , "
            strSQl &= "         A.PDK_MEN_OBJETO,"
            strSQl &= "         A.PDK_MEN_DESCRIPCION,"
            strSQl &= "         A.PDK_MEN_LINK,"
            strSQl &= "         A.PDK_MEN_ID_PADRE,"
            strSQl &= "         B.PDK_PAR_SIS_PARAMETRO,"
            strSQl &= "         C.PDK_MEN_DESCRIPCION "
            strSQl &= "     FROM PDK_MENU A"
            strSQl &= "     INNER JOIN PDK_PARAMETROS_SISTEMA B ON B.PDK_ID_PARAMETROS_SISTEMA = A.PDK_MEN_OBJETO "
            strSQl &= "     LEFT OUTER JOIN PDK_MENU C ON C.PDK_ID_MENU = A.PDK_MEN_ID_PADRE "
            strSQl &= "     WHERE A.PDK_ID_MENU = @PDK_ID_MENU OR A.PDK_MEN_ID_PADRE = @PDK_ID_MENU"

            strSQl &= " SET @CONTADOR = @CONTADOR + 1"
            strSQl &= " END"


            strSQl &= " SELECT 	PDK_ID_MENU,"
            strSQl &= " PDK_MEN_OBJETO,"
            strSQl &= " PDK_MEN_DESCRIPCION,"
            strSQl &= " ISNULL(PDK_MEN_LINK,'NINGUNO') PDK_MEN_LINK,"
            strSQl &= " PDK_MEN_ID_PADRE,"
            strSQl &= " PDK_PAR_SIS_PARAMETRO,"
            strSQl &= " ISNULL(PDK_PAD_DESCRIPCION,'NINGUNO') PDK_PAD_DESCRIPCION "
            strSQl &= " FROM #PDK_MENU "

            'strSQl &= " DROP TABLE #PDK_MENU"
            'strSQl &= " DROP TABLE #PDK_PADRE "

            Dim ds As DataSet = ManejaBD.EjecutarQuery(strSQl.ToString)
            Return ds

        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_MENU")
            Throw objException
        End Try

    End Function
    Public Shared Function ObtenPadres() As DataSet

        Dim objException As Exception = Nothing
        Dim strSQl As String = String.Empty
        Dim ManejaBD As New clsManejaBD

        Try

            strSQl = String.Empty
            strSQl &= " SELECT PDK_ID_MENU, " & _
                " PDK_MEN_OBJETO, " & _
                " PDK_MEN_DESCRIPCION " & _
                " FROM PDK_MENU WHERE PDK_MEN_OBJETO = 5 ORDER BY PDK_ID_MENU"

            Dim ds As DataSet = ManejaBD.EjecutarQuery(strSQl.ToString)
            Return ds

        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_MENU")
            Throw objException
        End Try

    End Function

    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD

        Try
            If intIdMenu = 0 Then
                Me.intIdMenu = BD.ObtenConsecutivo("PDK_ID_MENU", "PDK_MENU", Nothing)
                strSql = "INSERT INTO PDK_MENU " & _
                        "(" & _
                        "PDK_ID_MENU,PDK_MEN_OBJETO,PDK_MEN_DESCRIPCION,PDK_MEN_LINK,PDK_MEN_ID_PADRE,PDK_MEN_TIPO_PERMISO,PDK_MEN_NIVEL,PDK_MEN_ACCESO_DIRECTO,PDK_MEN_MODIF,PDK_CLAVE_USUARIO)" & _
                        " VALUES ( " & intIdMenu & ",  " & intMenuObj & ", '" & strMenuDescrip & "','" & strMenuLink & "', " & intMenuPadre & ",  " & intMenuPermiso & ",  " & intMenuNivel & ",  " & intMenuAcceso & ", '" & strMenModi & "', " & intCveUsuario & " " & _
                        ")"

            Else
                ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_MENU ")
        End Try
    End Sub

    Private Sub ActualizaRegistro()

        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_MENU " & _
               "SET " & _
                " PDK_MEN_OBJETO = " & intMenuObj & ", " & _
                " PDK_MEN_DESCRIPCION = '" & strMenuDescrip & "'," & _
                " PDK_MEN_LINK = '" & strMenuLink & "'," & _
                " PDK_MEN_ID_PADRE = " & intMenuPadre & ", " & _
                " PDK_MEN_TIPO_PERMISO = " & intMenuPermiso & ", " & _
                " PDK_MEN_NIVEL = " & intMenuNivel & ", " & _
                " PDK_MEN_ACCESO_DIRECTO = " & intMenuAcceso & ", " & _
                " PDK_MEN_MODIF = '" & strMenModi & "'," & _
                " PDK_CLAVE_USUARIO = " & intCveUsuario & " " & _
                " WHERE PDK_ID_MENU=  " & intIdMenu

            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_MENU")
        End Try
    End Sub


#End Region

End Class
