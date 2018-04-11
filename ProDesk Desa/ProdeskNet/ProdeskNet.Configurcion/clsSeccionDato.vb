Imports System.Text


Public Class clsSeccionDato

#Region "trackers"
    ' BBV-P-423  RQSOL-02  gvargas   06/09/2016 Se agrego el atributo "strPDK_TOOLTIP" son su metodo GetSet.
    '                                           Se modificaron los metodos "ActualizaRegistro()" y "Guarda()" para permitir guardar y actualizar los Tooltips.
#End Region

    '-------------------------- INICIO PDK_SECCION_DATO-------------------------- 
#Region "Variables"
    Private intPDK_ID_SECCION_DATO As Integer = 0
    Private intPDK_ID_SECCION As Integer = 0
    Private strPDK_SEC_DAT_NOMBRE As String = String.Empty
    Private intPDK_ID_TIPO_OBJETO As Integer = 0
    Private strPDK_SEC_DAT_LONGUITUD As String = String.Empty
    Private intPDK_SEC_DAT_STATUS As Integer = 0
    Private strPDK_SEC_DAT_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_SEC_DAT_LLAVE As Integer = 0
    Private intPDK_SEC_TIPO_CAMPO_OBJETO As Integer = 0
    Private strPDK_SEC_MOSTRA_PANT As String = String.Empty
    Private strPDK_TOOLTIP As String = String.Empty
    Private strErrorSeccion As String = ""
#End Region
#Region "Propiedades"
    Public Property PDK_ID_SECCION_DATO() As Integer
        Get
            Return intPDK_ID_SECCION_DATO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_SECCION_DATO = value
        End Set
    End Property
    Public Property PDK_ID_SECCION() As Integer
        Get
            Return intPDK_ID_SECCION
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_SECCION = value
        End Set
    End Property
    Public Property PDK_SEC_DAT_NOMBRE() As String
        Get
            Return strPDK_SEC_DAT_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_SEC_DAT_NOMBRE = value
        End Set
    End Property
    Public Property PDK_ID_TIPO_OBJETO() As Integer
        Get
            Return intPDK_ID_TIPO_OBJETO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_TIPO_OBJETO = value
        End Set
    End Property
    Public Property PDK_SEC_DAT_LONGUITUD() As String
        Get
            Return strPDK_SEC_DAT_LONGUITUD
        End Get
        Set(ByVal value As String)
            strPDK_SEC_DAT_LONGUITUD = value
        End Set
    End Property
    Public Property PDK_SEC_DAT_STATUS() As Integer
        Get
            Return intPDK_SEC_DAT_STATUS
        End Get
        Set(ByVal value As Integer)
            intPDK_SEC_DAT_STATUS = value
        End Set
    End Property
    Public Property PDK_SEC_DAT_MODIF() As String
        Get
            Return strPDK_SEC_DAT_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_SEC_DAT_MODIF = value
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
    Public Property PDK_SEC_DAT_LLAVE() As Integer
        Get
            Return intPDK_SEC_DAT_LLAVE
        End Get
        Set(ByVal value As Integer)
            intPDK_SEC_DAT_LLAVE = value
        End Set
    End Property
    Public Property PDK_SEC_TIPO_CAMPO_OBJETO() As Integer
        Get
            Return intPDK_SEC_TIPO_CAMPO_OBJETO
        End Get
        Set(ByVal value As Integer)
            intPDK_SEC_TIPO_CAMPO_OBJETO = value
        End Set
    End Property
    Public Property PDK_SEC_MOSTRA_PANT() As String
        Get
            Return strPDK_SEC_MOSTRA_PANT
        End Get
        Set(value As String)
            strPDK_SEC_MOSTRA_PANT = value
        End Set
    End Property
    Public Property PDK_TOOLTIP() As String
        Get
            Return strPDK_TOOLTIP
        End Get
        Set(value As String)
            strPDK_TOOLTIP = value
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
            strSQL.Append(" PDK_ID_SECCION_DATO,")
            strSQL.Append(" PDK_ID_SECCION,")
            strSQL.Append(" PDK_SEC_DAT_NOMBRE,")
            strSQL.Append(" PDK_ID_TIPO_OBJETO,")
            strSQL.Append(" PDK_SEC_DAT_LONGUITUD,")
            strSQL.Append(" PDK_SEC_DAT_STATUS,")
            strSQL.Append(" PDK_SEC_DAT_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append("PDK_SEC_DAT_LLAVE,")
            strSQL.Append("PDK_SEC_TIPO_CAMPO_OBJETO,")
            strSQL.Append(" PDK_SEC_MOSTRA_PANT,")
            strSQL.Append(" CASE WHEN PDK_TOOLTIP IS NULL THEN '' ELSE PDK_TOOLTIP END AS PDK_TOOLTIP")
            strSQL.Append(" FROM PDK_SECCION_DATO")
            strSQL.Append(" WHERE PDK_ID_SECCION_DATO = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_SECCION_DATO = .Item("PDK_ID_SECCION_DATO")
                Me.intPDK_ID_SECCION = .Item("PDK_ID_SECCION")
                Me.strPDK_SEC_DAT_NOMBRE = .Item("PDK_SEC_DAT_NOMBRE")
                Me.intPDK_ID_TIPO_OBJETO = .Item("PDK_ID_TIPO_OBJETO")
                Me.strPDK_SEC_DAT_LONGUITUD = .Item("PDK_SEC_DAT_LONGUITUD")
                Me.intPDK_SEC_DAT_STATUS = .Item("PDK_SEC_DAT_STATUS")
                Me.strPDK_SEC_DAT_MODIF = .Item("PDK_SEC_DAT_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_SEC_DAT_LLAVE = .Item("PDK_SEC_DAT_LLAVE")
                Me.intPDK_SEC_TIPO_CAMPO_OBJETO = .Item("PDK_SEC_TIPO_CAMPO_OBJETO")
                Me.strPDK_SEC_MOSTRA_PANT = .Item("PDK_SEC_MOSTRA_PANT")
                Me.strPDK_TOOLTIP = .Item("PDK_TOOLTIP")
            End With
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try
    End Sub
    Public Shared Function CrearTable(ByVal intSeccion As Integer, ByVal intUsuario As Integer, ByVal intBandera As Integer) As DataSet
        CrearTable = New DataSet
        Dim MB As New ProdeskNet.BD.clsManejaBD
        Dim objException As Exception = Nothing
        Dim strError As String = ""
        Try
            MB.AgregaParametro("@Seccion", ProdeskNet.BD.TipoDato.Entero, Trim(intSeccion))
            MB.AgregaParametro("@Usuario", ProdeskNet.BD.TipoDato.Entero, Trim(intUsuario))
            MB.AgregaParametro("@Bandera", ProdeskNet.BD.TipoDato.Entero, Trim(intBandera))
            If MB.ErrorBD <> "" Then Exit Function
            CrearTable = MB.EjecutaStoredProcedure("spCrearTabla")
            Return CrearTable
        Catch ex As Exception
            objException = New Exception("Error al crear la tabla")
            Throw objException
        End Try

    End Function
    Public Sub AgregarTablaCam(ByVal intIdSecc As Integer, ByVal strNombreTabla As String, ByVal strObjetos As String)
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If strObjetos = "VARCHAR" Then
                strSQL.Append("  ALTER TABLE " & strNombreTabla & " ADD " & strPDK_SEC_DAT_NOMBRE & " " & strObjetos & "(" & strPDK_SEC_DAT_LONGUITUD & ")")
            Else
                strSQL.Append("  ALTER TABLE " & strNombreTabla & " ADD " & strPDK_SEC_DAT_NOMBRE & " " & strObjetos & "   ")
            End If
            BD.EjecutarQuery(strSQL.ToString)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_SECCION_DATO")
        End Try


    End Sub
    Public Shared Function DropTable(ByVal intIdSeccion As Integer, ByVal strNombreTab As String, ByVal intCveUsu As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        DropTable = New DataSet

        Try

            strSQL.Append(" DECLARE @TABLA VARCHAR(200),@LLAVE INT,@MINIMO INT ,@MAXIMO INT,@SQL VARCHAR(2000),@CONTA INT ")
            strSQL.Append(" SELECT @TABLA=PDK_SEC_NOMBRE_TABLA  FROM PDK_SECCION  WHERE PDK_ID_SECCION=" & intIdSeccion & "")
            strSQL.Append(" SELECT @LLAVE=A.PDK_ID_LLAVE  FROM PDK_LLAVES A WHERE A.PDK_NOMBRE_TABLA =@TABLA ")
            strSQL.Append(" CREATE TABLE #TEM(ID INT IDENTITY,PDK_SEC_NOMBRE_TABLA VARCHAR(200)) ")
            strSQL.Append(" INSERT INTO #TEM(PDK_SEC_NOMBRE_TABLA) ")
            strSQL.Append(" SELECT B.PDK_SEC_NOMBRE_TABLA  FROM PDK_SECCION_DATO A  ")
            strSQL.Append(" INNER JOIN PDK_SECCION B ON A.PDK_ID_SECCION =B.PDK_ID_SECCION  ")
            strSQL.Append(" WHERE  a.PDK_SEC_DAT_LLAVE=@LLAVE AND B.PDK_SEC_NOMBRE_TABLA<>@TABLA AND B.PDK_SEC_NOMBRE_TABLA IN(SELECT PDK_NOMBRE_TABLA FROM PDK_LLAVES WHERE PDK_LLAVE_STATUS=2 ) ")
            strSQL.Append(" SET @CONTA=1 ")
            strSQL.Append(" SELECT @SQL='NO SE PUEDE BORRA LA TABLA PORQUE TIENE RELACION: ' ")
            strSQL.Append(" SELECT @MINIMO=MIN(ID) FROM #TEM ")
            strSQL.Append(" SELECT @MAXIMO=MAX(ID) FROM #TEM ")
            strSQL.Append(" WHILE @MINIMO<=@MAXIMO  ")
            strSQL.Append(" BEGIN ")
            strSQL.Append(" SELECT @TABLA=PDK_SEC_NOMBRE_TABLA FROM #TEM WHERE ID=@MINIMO ")
            strSQL.Append(" IF @CONTA =@MAXIMO ")
            strSQL.Append(" BEGIN ")
            strSQL.Append(" SELECT  @SQL=@SQL+@TABLA ")
            strSQL.Append(" End ")
            strSQL.Append(" ELSE ")
            strSQL.Append(" BEGIN ")
            strSQL.Append(" SELECT  @SQL=@SQL+@TABLA+',' ")
            strSQL.Append(" End")
            strSQL.Append(" SELECT @CONTA=@CONTA+1 ")
            strSQL.Append(" SET @MINIMO=@MINIMO+1 ")
            strSQL.Append("  End")
            strSQL.Append(" IF EXISTS ( SELECT B.PDK_SEC_NOMBRE_TABLA  FROM PDK_SECCION_DATO A ")
            strSQL.Append("  INNER JOIN PDK_SECCION B ON A.PDK_ID_SECCION =B.PDK_ID_SECCION  ")
            strSQL.Append("  WHERE  a.PDK_SEC_DAT_LLAVE=@LLAVE AND B.PDK_SEC_NOMBRE_TABLA<>@TABLA AND B.PDK_SEC_NOMBRE_TABLA IN(SELECT PDK_NOMBRE_TABLA FROM PDK_LLAVES WHERE PDK_LLAVE_STATUS=2 ))")
            strSQL.Append(" BEGIN ")
            strSQL.Append(" SELECT @SQL AS  '@SQL' ")
            strSQL.Append(" END ")
            strSQL.Append(" ELSE ")
            strSQL.Append(" BEGIN ")
            strSQL.Append("  DROP TABLE " & strNombreTab & " ")
            strSQL.Append("  UPDATE PDK_LLAVES SET PDK_LLAVE_STATUS=3 WHERE PDK_NOMBRE_TABLA='" & strNombreTab & "'")
            strSQL.Append("  UPDATE PDK_SECCION SET PDK_SEC_CREACION=1,PDK_CLAVE_USUARIO=" & intCveUsu & " WHERE PDK_ID_SECCION =" & intIdSeccion & "")
            strSQL.Append("  SELECT @SQL ='La tabla se borro con éxito' ")
            strSQL.Append("  SELECT @SQL AS  '@SQL' ")
            strSQL.Append(" END ")
            strSQL.Append("  DROP TABLE #TEM ")
            DropTable = BD.EjecutarQuery(strSQL.ToString)
            If DropTable.Tables.Count > 0 AndAlso DropTable.Tables(0).Rows.Count > 0 Then
                Return DropTable
            End If


        Catch ex As Exception
            objException = New Exception("Error no se puede eliminar la tabla")
            Throw objException
        End Try

    End Function
    Public Shared Function ObtenerSeccDta(ByVal intCveSeccio As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append("SELECT A.PDK_ID_SECCION_DATO,A.PDK_SEC_DAT_NOMBRE,B.PDK_TIP_OBJ_NOMBRE_COD,A.PDK_SEC_DAT_LONGUITUD,A.PDK_ID_TIPO_OBJETO,A.PDK_SEC_MOSTRA_PANT")
            strSQL.Append(" FROM PDK_SECCION_DATO A INNER JOIN PDK_TIPO_OBJETO B ON A.PDK_ID_TIPO_OBJETO=B.PDK_ID_TIPO_OBJETO ")
            strSQL.Append(" WHERE A.PDK_ID_SECCION =" & intCveSeccio & " AND PDK_SEC_DAT_STATUS=2 ORDER BY A.PDK_SEC_DAT_LLAVE DESC,A.PDK_ID_SECCION_DATO ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_SECCION_DATO")
            Throw objException
        End Try

    End Function
    Public Shared Function ObtenTodos(ByVal intCveSeccin As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_SECCION_DATO,")
            strSQL.Append(" A.PDK_ID_SECCION,")
            strSQL.Append(" A.PDK_SEC_DAT_NOMBRE,")
            strSQL.Append(" A.PDK_ID_TIPO_OBJETO,")
            strSQL.Append(" D.PDK_TIP_OBJ_NOMBRE,")
            strSQL.Append(" A.PDK_SEC_DAT_LONGUITUD,")
            strSQL.Append(" A.PDK_SEC_DAT_STATUS,")
            strSQL.Append(" A.PDK_SEC_DAT_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" B.PDK_PAR_SIS_PARAMETRO,")
            strSQL.Append(" A.PDK_SEC_DAT_LLAVE,")
            strSQL.Append(" A.PDK_SEC_MOSTRA_PANT ")
            strSQL.Append(" FROM PDK_SECCION_DATO A INNER JOIN PDK_PARAMETROS_SISTEMA B ON  B.PDK_ID_PARAMETROS_SISTEMA=A.PDK_SEC_DAT_STATUS  ")
            strSQL.Append(" INNER JOIN PDK_SECCION C ON A.PDK_ID_SECCION=C.PDK_ID_SECCION")
            strSQL.Append(" INNER JOIN PDK_TIPO_OBJETO D ON D.PDK_ID_TIPO_OBJETO=A.PDK_ID_TIPO_OBJETO")
            strSQL.Append(" WHERE A.PDK_ID_SECCION=" & intCveSeccin & "  AND A.PDK_SEC_DAT_LLAVE=0")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_SECCION_DATO")
            Throw objException
        End Try
    End Function
    Public Shared Function ValidadDatos(ByVal strValida As String, ByVal intSecc As Integer, Optional ByVal intseccDat As Integer = 0, Optional ByVal intBandera As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intBandera = 0 Then
                strSQL.AppendLine("SELECT * FROM PDK_SECCION_DATO WHERE PDK_ID_SECCION =" & intSecc & " AND  PDK_SEC_DAT_NOMBRE='" & strValida & "' ")
            Else
                strSQL.AppendLine("SELECT * FROM PDK_SECCION_DATO WHERE PDK_ID_SECCION=" & intSecc & " AND PDK_ID_SECCION_DATO <>" & intseccDat & " AND PDK_SEC_DAT_NOMBRE ='" & strValida & "' ")
            End If

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_SECCION_DATO")
            Throw objException
        End Try

    End Function

    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_SECCION_DATO = 0 Then
                Me.intPDK_ID_SECCION_DATO = BD.ObtenConsecutivo("PDK_ID_SECCION_DATO", "PDK_SECCION_DATO", Nothing)
                strSql = "INSERT INTO PDK_SECCION_DATO (" & _
"PDK_ID_SECCION_DATO,PDK_ID_SECCION,PDK_SEC_DAT_NOMBRE,PDK_ID_TIPO_OBJETO,PDK_SEC_DAT_LONGUITUD,PDK_SEC_DAT_STATUS,PDK_SEC_DAT_MODIF,PDK_CLAVE_USUARIO,PDK_SEC_DAT_LLAVE,PDK_SEC_TIPO_CAMPO_OBJETO,PDK_SEC_MOSTRA_PANT,PDK_TOOLTIP)" & _
" VALUES ( " & intPDK_ID_SECCION_DATO & ",  " & intPDK_ID_SECCION & ", '" & strPDK_SEC_DAT_NOMBRE & "', " & intPDK_ID_TIPO_OBJETO & ", '" & strPDK_SEC_DAT_LONGUITUD & "', " & intPDK_SEC_DAT_STATUS & ", '" & strPDK_SEC_DAT_MODIF & "', " & intPDK_CLAVE_USUARIO & ", " & intPDK_SEC_DAT_LLAVE & ", " & intPDK_SEC_TIPO_CAMPO_OBJETO & ", '" & strPDK_SEC_MOSTRA_PANT & "' , " & _
" PDK_TOOLTIP = CASE WHEN '" & strPDK_TOOLTIP & "' = '' THEN NULL ELSE '" & strPDK_TOOLTIP & "' END" & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_SECCION_DATO ")
        End Try
    End Sub
    Public Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_SECCION_DATO " & _
               "SET " & _
                " PDK_SEC_DAT_NOMBRE = '" & strPDK_SEC_DAT_NOMBRE & "'," & _
             " PDK_ID_TIPO_OBJETO = " & intPDK_ID_TIPO_OBJETO & ", " & _
                " PDK_SEC_DAT_LONGUITUD = '" & strPDK_SEC_DAT_LONGUITUD & "'," & _
             " PDK_SEC_DAT_STATUS = " & intPDK_SEC_DAT_STATUS & ", " & _
                " PDK_SEC_DAT_MODIF = '" & strPDK_SEC_DAT_MODIF & "'," & _
             " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
             "PDK_SEC_DAT_LLAVE=" & intPDK_SEC_DAT_LLAVE & ", " & _
             "PDK_SEC_TIPO_CAMPO_OBJETO=" & intPDK_SEC_TIPO_CAMPO_OBJETO & ", " & _
                         " PDK_SEC_MOSTRA_PANT ='" & strPDK_SEC_MOSTRA_PANT & "', " & _
                         " PDK_TOOLTIP = CASE WHEN '" & strPDK_TOOLTIP & "' = '' THEN NULL ELSE '" & strPDK_TOOLTIP & "' END" & _
            " WHERE PDK_ID_SECCION_DATO=  " & intPDK_ID_SECCION_DATO
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_SECCION_DATO")
        End Try
    End Sub
    Public Shared Function ObtenerLlaves(Optional ByVal intCveLlave As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" PDK_ID_LLAVE,")
            strSQL.Append(" PDK_NOMBRE_TABLA,")
            strSQL.Append(" PDK_NOMBRE_CAMPO,")
            strSQL.Append(" PDK_LLAVE_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO ")
            strSQL.Append(" FROM PDK_LLAVES WHERE PDK_ID_LLAVE<>1 AND PDK_LLAVE_STATUS=2")

            If intCveLlave > 0 Then
                strSQL.Append(" AND PDK_ID_LLAVE=" & intCveLlave & "")
            End If
            strSQL.Append(" ORDER BY PDK_ID_LLAVE")

            ObtenerLlaves = BD.EjecutarQuery(strSQL.ToString)
            Return ObtenerLlaves
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_SECCION_DATO")
            Throw objException
        End Try

    End Function
#End Region
    '-------------------------- FIN PDK_SECCION_DATO-------------------------- 


End Class
