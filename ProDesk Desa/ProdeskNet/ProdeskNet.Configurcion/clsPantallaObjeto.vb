Imports System.Text

Public Class clsPantallaObjeto

    '-------------------------- INICIO PDK_REL_PANTALLA_OBJETO-------------------------- 

#Region "Trackers"
    ' YAM-P-208  egonzalez 01/09/2015 Se implemento la función "tareaActual()", la cuál recive como parámetro el id de la solicitud para la búsqueda de la tarea activa en la que se encuentra el flujo
    ' BBV-P-423  RQSOL-02  gvargas   15/12/2016 Se modifico el metodo "ObtenerControles()" cuando el atributo "intBandera" es 0, agregando al Query el campo "PDK_TOOLTIP"
    ' BBV-P-423 JRHM:  RQ J-REPORTE SOLICITUD DE CREDITO 30/12/16.- Se creo metodo ObtenDatosImpSolicitudCredito para la consulta de datos para la impresion de el reporte de solicitud de credito
    ' BUG PD 10 GVARGAS 21/02/2017 Orden alfabetico combos
    ' BUG-PD-274 GVARGAS 23/11/2017 Cambio fechas
    ' RQ-PD31: DJUAREZ: 08/03/2018: SE CREA POPUP PARA MODIFICAR LA COLONIA CUANDO SE GUARDE LA COLONIA "OTRO"
#End Region


#Region "Variables"
    Private intPDK_ID_REL_PANTALLA_OBJETO As Integer = 0
    Private strPDK_REL_PANT_OBJ_NOMBRE As String = String.Empty
    Private intPDK_REL_PANT_OBJ_TAMANO As Integer = 0
    Private intPDK_REL_PANT_OBJ_ORDEN As Integer = 0
    Private intPDK_REL_PANT_OBJ_STATUS As Integer = 0
    Private strPDK_REL_PANT_OBJ_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_ID_SECCION_DATO As Integer = 0
    'Private intPDK_ID_TIPO_OBJETO As Integer = 0
    'Private intPDK_ID_SECCION As Integer = 0
    Private intPDK_ID_PANTALLAS As Integer = 0
    Private intPDK_REL_PANT_OBJ_MOSTRAR As Integer = 0

#End Region
#Region "Propiedades"
    Public Property PDK_ID_REL_PANTALLA_OBJETO() As Integer
        Get
            Return intPDK_ID_REL_PANTALLA_OBJETO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_REL_PANTALLA_OBJETO = value
        End Set
    End Property
    Public Property PDK_REL_PANT_OBJ_NOMBRE() As String
        Get
            Return strPDK_REL_PANT_OBJ_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_REL_PANT_OBJ_NOMBRE = value
        End Set
    End Property
    Public Property PDK_REL_PANT_OBJ_TAMANO() As Integer
        Get
            Return intPDK_REL_PANT_OBJ_TAMANO
        End Get
        Set(ByVal value As Integer)
            intPDK_REL_PANT_OBJ_TAMANO = value
        End Set
    End Property
    Public Property PDK_REL_PANT_OBJ_ORDEN() As Integer
        Get
            Return intPDK_REL_PANT_OBJ_ORDEN
        End Get
        Set(ByVal value As Integer)
            intPDK_REL_PANT_OBJ_ORDEN = value
        End Set
    End Property
    Public Property PDK_REL_PANT_OBJ_STATUS() As Integer
        Get
            Return intPDK_REL_PANT_OBJ_STATUS
        End Get
        Set(ByVal value As Integer)
            intPDK_REL_PANT_OBJ_STATUS = value
        End Set
    End Property
    Public Property PDK_REL_PANT_OBJ_MODIF() As String
        Get
            Return strPDK_REL_PANT_OBJ_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_REL_PANT_OBJ_MODIF = value
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
    Public Property PDK_ID_SECCION_DATO() As Integer
        Get
            Return intPDK_ID_SECCION_DATO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_SECCION_DATO = value
        End Set
    End Property
    Public Property PDK_REL_PANT_OBJ_MOSTRAR() As Integer
        Get
            Return intPDK_REL_PANT_OBJ_MOSTRAR

        End Get
        Set(ByVal value As Integer)
            intPDK_REL_PANT_OBJ_MOSTRAR = value

        End Set
    End Property
    'Public Property PDK_ID_TIPO_OBJETO() As Integer
    '    Get
    '        Return intPDK_ID_TIPO_OBJETO
    '    End Get
    '    Set(ByVal value As Integer)
    '        intPDK_ID_TIPO_OBJETO = value
    '    End Set
    'End Property
    'Public Property PDK_ID_SECCION() As Integer
    '    Get
    '        Return intPDK_ID_SECCION
    '    End Get
    '    Set(ByVal value As Integer)
    '        intPDK_ID_SECCION = value
    '    End Set
    'End Property
    Public Property PDK_ID_PANTALLAS() As Integer
        Get
            Return intPDK_ID_PANTALLAS
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_PANTALLAS = value
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
            strSQL.Append(" PDK_ID_REL_PANTALLA_OBJETO,")
            strSQL.Append(" PDK_REL_PANT_OBJ_NOMBRE,")
            strSQL.Append(" PDK_REL_PANT_OBJ_TAMANO,")
            strSQL.Append(" PDK_REL_PANT_OBJ_ORDEN,")
            strSQL.Append(" PDK_REL_PANT_OBJ_STATUS,")
            strSQL.Append(" PDK_REL_PANT_OBJ_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" PDK_ID_SECCION_DATO,")
            'strSQL.Append(" PDK_ID_TIPO_OBJETO,")
            'strSQL.Append(" PDK_ID_SECCION, ")
            strSQL.Append(" PDK_ID_PANTALLAS ")
            strSQL.Append(" FROM PDK_REL_PANTALLA_OBJETO")
            strSQL.Append(" WHERE PDK_ID_REL_PANTALLA_OBJETO = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_REL_PANTALLA_OBJETO = .Item("PDK_ID_REL_PANTALLA_OBJETO")
                Me.strPDK_REL_PANT_OBJ_NOMBRE = .Item("PDK_REL_PANT_OBJ_NOMBRE")
                Me.intPDK_REL_PANT_OBJ_TAMANO = .Item("PDK_REL_PANT_OBJ_TAMANO")
                Me.intPDK_REL_PANT_OBJ_ORDEN = .Item("PDK_REL_PANT_OBJ_ORDEN")
                Me.intPDK_REL_PANT_OBJ_STATUS = .Item("PDK_REL_PANT_OBJ_STATUS")
                Me.strPDK_REL_PANT_OBJ_MODIF = .Item("PDK_REL_PANT_OBJ_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_ID_SECCION_DATO = .Item("PDK_ID_SECCION_DATO")
                'Me.intPDK_ID_TIPO_OBJETO = .Item("PDK_ID_TIPO_OBJETO")
                'Me.intPDK_ID_SECCION = .Item("PDK_ID_SECCION")
                Me.intPDK_ID_PANTALLAS = .Item("PDK_ID_PANTALLAS")

            End With
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try
    End Sub
    Public Shared Function reporteCONTRATOS(ByVal INTfOLIO As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.AppendLine("select A.NOMBRE_SOLICI, ISNULL(B.NOMBRE1+''+B.NOMBRE2+''+B.APELLIDO_PATERNO+''+B.APELLIDO_MATERNO,'') AS COACREDITADO from PDK_TAB_DATOS_SOLICITANTE A LEFT JOIN  PDK_TAB_COACREDITADO_CASO B ON A.PDK_ID_SECCCERO=B.PDK_ID_SECCCERO ")
            strSQL.AppendLine("WHERE A.PDK_ID_SECCCERO=" & INTfOLIO & " ")

            reporteCONTRATOS = BD.EjecutarQuery(strSQL.ToString)
            Return reporteCONTRATOS
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try
    End Function
    Public Shared Function ReporteSolicitudPer(ByVal intFolio As Integer, ByVal intBandera As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.AppendLine("DECLARE @PANTALLA INT,@TABLA VARCHAR(200),@CADENA VARCHAR(MAX)='',@MINI INT ,@MAXI INT,@SECCION INT,@FOLIO INT,@SQLTAB VARCHAR(MAX)='',@CADENA1 VARCHAR(MAX)='',@TAB1 VARCHAR(200)=''")
            strSQL.AppendLine("CREATE TABLE #TEMPORAL (ID INT IDENTITY , PDK_SEC_NOMBRE_TABLA VARCHAR(200),PDK_ID_SECCION INT)")
            strSQL.AppendLine("SELECT @PANTALLA=A.PDK_ID_PANTALLAS  FROM PDK_PANTALLAS A INNER JOIN PDK_REL_PANTALLA_TAREA B ON A.PDK_ID_PANTALLAS=B.PDK_ID_PANTALLAS ")
            strSQL.AppendLine("INNER JOIN PDK_CAT_TAREAS C ON C.PDK_ID_TAREAS =B.PDK_ID_TAREAS  WHERE A.PDK_PANT_DOCUMENTOS=116 ")
            strSQL.AppendLine("INSERT INTO  #TEMPORAL (PDK_SEC_NOMBRE_TABLA,PDK_ID_SECCION)")
            strSQL.AppendLine("SELECT C.PDK_SEC_NOMBRE_TABLA,C.PDK_ID_SECCION FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN")
            strSQL.AppendLine("PDK_SECCION C ON B.PDK_ID_SECCION=C.PDK_ID_SECCION  WHERE A.PDK_ID_PANTALLAS=@PANTALLA AND A.PDK_REL_PANT_OBJ_ORDEN<>0 GROUP BY C.PDK_ID_SECCION,C.PDK_SEC_NOMBRE_TABLA ")
            strSQL.AppendLine("SET  @FOLIO= " & intFolio & "")
            strSQL.AppendLine("SELECT @MINI=MIN(ID) FROM #TEMPORAL")
            strSQL.AppendLine("SELECT @MAXI=MAX(ID) FROM #TEMPORAL")
            strSQL.AppendLine("WHILE @MINI<=@MAXI")
            strSQL.AppendLine("BEGIN")
            strSQL.AppendLine("SELECT @CADENA=''")
            strSQL.AppendLine("SELECT @TABLA=PDK_SEC_NOMBRE_TABLA,@SECCION=PDK_ID_SECCION FROM #TEMPORAL WHERE ID=@MINI")
            strSQL.AppendLine("SELECT  @CADENA = @CADENA + PDK_SEC_DAT_NOMBRE + ', '")
            strSQL.AppendLine("FROM PDK_SECCION_DATO ")
            strSQL.AppendLine("WHERE  PDK_ID_SECCION = @SECCION AND PDK_SEC_DAT_STATUS=2 AND PDK_SEC_DAT_LLAVE=0 ")
            strSQL.AppendLine("SELECT @SQLTAB= 'SELECT PDK_ID_SECCCERO,' + SUBSTRING(@CADENA, 0, LEN(@CADENA)) + ' ' + ' FROM ' + @TABLA +' '+'WHERE PDK_ID_SECCCERO='+LTRIM(@FOLIO) ")
            strSQL.AppendLine("FROM PDK_SECCION ")
            strSQL.AppendLine("WHERE PDK_ID_SECCION  = @SECCION ")
            strSQL.AppendLine("exec (@SQLTAB) ")
            strSQL.AppendLine("SET @MINI=@MINI+1")
            strSQL.AppendLine("End")
            strSQL.AppendLine("DROP TABLE #TEMPORAL")


            ReporteSolicitudPer = BD.EjecutarQuery(strSQL.ToString)
            Return ReporteSolicitudPer
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try

    End Function

    Public Shared Function ObtenerPantCrys(ByVal innFolio As Integer, ByVal inbBandera As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.AppendLine("DECLARE @PANTALLA INT,@TABLA VARCHAR(200),@CADENA VARCHAR(MAX)='',@MINI INT ,@MAXI INT,@SECCION INT,@FOLIO INT,@SQLTAB VARCHAR(MAX)='',@CADENA1 VARCHAR(MAX)='',@TAB1 VARCHAR(200)=''")
            strSQL.AppendLine("CREATE TABLE #TEMPORAL (ID INT IDENTITY , PDK_SEC_NOMBRE_TABLA VARCHAR(200),PDK_ID_SECCION INT) ")
            strSQL.AppendLine(" SELECT @TAB1=PDK_SEC_NOMBRE_TABLA FROM PDK_SECCION WHERE PDK_ID_SECCION=1")
            If inbBandera = 0 Then
                strSQL.AppendLine("SELECT @PANTALLA=A.PDK_ID_PANTALLAS  FROM PDK_PANTALLAS A INNER JOIN PDK_REL_PANTALLA_TAREA B ON A.PDK_ID_PANTALLAS=B.PDK_ID_PANTALLAS ")
                strSQL.AppendLine("INNER JOIN PDK_CAT_TAREAS C ON C.PDK_ID_TAREAS =B.PDK_ID_TAREAS  WHERE C.PDK_TAR_NOMBRE ='SOLICITUD'")
            Else
                strSQL.AppendLine("SELECT @PANTALLA=A.PDK_ID_PANTALLAS  FROM PDK_PANTALLAS A INNER JOIN PDK_REL_PANTALLA_TAREA B ON A.PDK_ID_PANTALLAS=B.PDK_ID_PANTALLAS ")
                strSQL.AppendLine("INNER JOIN PDK_CAT_TAREAS C ON C.PDK_ID_TAREAS =B.PDK_ID_TAREAS  WHERE A.PDK_PANT_DOCUMENTOS=110")
            End If

            strSQL.AppendLine("SELECT @FOLIO=" & innFolio & "")
            strSQL.AppendLine("INSERT INTO  #TEMPORAL (PDK_SEC_NOMBRE_TABLA,PDK_ID_SECCION)")
            strSQL.AppendLine("SELECT C.PDK_SEC_NOMBRE_TABLA,C.PDK_ID_SECCION FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN")
            strSQL.AppendLine("PDK_SECCION C ON B.PDK_ID_SECCION=C.PDK_ID_SECCION  WHERE A.PDK_ID_PANTALLAS=@PANTALLA AND A.PDK_REL_PANT_OBJ_ORDEN<>0 GROUP BY C.PDK_ID_SECCION,C.PDK_SEC_NOMBRE_TABLA")
            strSQL.AppendLine("SELECT @CADENA1=@CADENA1+'DECLARE @FECHA DATE  SELECT @FECHA=LTRIM(ANO_NACIMIENTO)+'+'''-'''+'+LTRIM(MES_NACIMIENTO)+'+'''-'''+'+LTRIM(DIA_NACIMIENTO) FROM '+ @TAB1+' WHERE PDK_ID_SECCCERO='+LTRIM(@FOLIO)")
            strSQL.AppendLine("SELECT @CADENA1=@CADENA1+' UPDATE PDK_TAB_DATOS_PERSONALES SET  FECHA_NACIMIENTO=@FECHA  WHERE PDK_ID_SECCCERO='+LTRIM(@FOLIO)")
            strSQL.AppendLine("SELECT @CADENA1=@CADENA1+' UPDATE PDK_TAB_COACREDITADO_CASO SET FECHA_NACIMIENTO=LTRIM(A.ANO_NACIMIENTO)+'+'''-'''+'+LTRIM(A.MES_NACIMIENTO)+'+'''-'''+'+LTRIM(A.DIA_NACIMIENTO) FROM PDK_TAB_COACREDITADO_CASO A WHERE PDK_ID_SECCCERO='+LTRIM(@FOLIO)")
            strSQL.AppendLine(" EXEC(@CADENA1);")
            strSQL.AppendLine("SELECT @MINI=MIN(ID) FROM #TEMPORAL")
            strSQL.AppendLine(" SELECT @MAXI=MAX(ID) FROM #TEMPORAL")
            strSQL.AppendLine("WHILE @MINI<=@MAXI")
            strSQL.AppendLine("BEGIN")
            strSQL.AppendLine("SELECT @CADENA=''")
            strSQL.AppendLine("SELECT @TABLA=PDK_SEC_NOMBRE_TABLA,@SECCION=PDK_ID_SECCION FROM #TEMPORAL WHERE ID=@MINI")
            strSQL.AppendLine("SELECT  @CADENA = @CADENA + PDK_SEC_DAT_NOMBRE + ', '")
            strSQL.AppendLine("FROM PDK_SECCION_DATO")
            strSQL.AppendLine("WHERE  PDK_ID_SECCION = @SECCION AND PDK_SEC_DAT_STATUS=2 AND PDK_SEC_DAT_LLAVE=0 ")
            strSQL.AppendLine("SELECT @SQLTAB= 'SELECT PDK_ID_SECCCERO,' + SUBSTRING(@CADENA, 0, LEN(@CADENA)) + ' ' + ' FROM ' + @TABLA +' '+'WHERE PDK_ID_SECCCERO='+LTRIM(@FOLIO) ")
            strSQL.AppendLine("FROM PDK_SECCION ")
            strSQL.AppendLine("WHERE PDK_ID_SECCION  = @SECCION ")
            strSQL.AppendLine("exec (@SQLTAB)")
            strSQL.AppendLine("SET @MINI=@MINI+1")
            strSQL.AppendLine("End")
            strSQL.AppendLine("DROP TABLE #TEMPORAL")

            ObtenerPantCrys = BD.EjecutarQuery(strSQL.ToString)
            Return ObtenerPantCrys

        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try



    End Function
    Public Shared Function ObtenerEntreReporte(ByVal intpantalla As Integer, ByVal intsolicitud As Integer, ByVal intusua As Integer, ByVal intclabe As Integer, ByVal inthabili As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim Dat As New DataSet
        Dim strError As String = ""
        ObtenerEntreReporte = Nothing
        Try
            Dim objSD As New ProdeskNet.BD.clsManejaBD
            With objSD
                .AgregaParametro("@FOLIO", ProdeskNet.BD.TipoDato.Entero, Trim(intsolicitud))
                .AgregaParametro("@PANTALLA", ProdeskNet.BD.TipoDato.Entero, Trim(intpantalla))
                .AgregaParametro("@USUARIO", ProdeskNet.BD.TipoDato.Entero, Trim(intusua))
                .AgregaParametro("@xml", ProdeskNet.BD.TipoDato.Entero, Trim(intclabe))
                .AgregaParametro("@xml2", ProdeskNet.BD.TipoDato.Entero, Trim(inthabili))

                If objSD.ErrorBD <> "" Then Exit Function
                ObtenerEntreReporte = objSD.EjecutaStoredProcedure("sp_GenerarEntrevista")

                'If Dat.Tables(0).Rows.Count > 0 AndAlso Dat.Tables.Count > 0 Then
                '    ObtenerEntreReporte = Dat.Tables(0).Rows(0)(0).ToString

                'End If
                Return ObtenerEntreReporte

            End With


        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try

    End Function
    Public Shared Function ObtenerPantaXlm(ByVal pantalla As Integer, ByVal solicitud As Integer, ByVal intBandera As Integer) As String
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim Dat As New DataSet
        Dim strError As String = ""
        ObtenerPantaXlm = Nothing
        Try
            Dim objSD As New ProdeskNet.BD.clsManejaBD
            With objSD
                .AgregaParametro("@PANTA", ProdeskNet.BD.TipoDato.Entero, Trim(pantalla))
                .AgregaParametro("@SOLICITUD", ProdeskNet.BD.TipoDato.Entero, Trim(solicitud))
                .AgregaParametro("@XMIL", ProdeskNet.BD.TipoDato.Entero, Trim(intBandera))

                If objSD.ErrorBD <> "" Then Exit Function
                Dat = objSD.EjecutaStoredProcedure("REPORTESXMLPANTALLA")

                If Dat.Tables(0).Rows.Count > 0 AndAlso Dat.Tables.Count > 0 Then
                    ObtenerPantaXlm = Dat.Tables(0).Rows(0).Item("PANTALLA")
                End If
                Return ObtenerPantaXlm

            End With


        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try

    End Function

    Public Shared Function OntenerPantallaEntrevista(ByVal intpantalla As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append("select A.PDK_PANT_NOMBRE,B.PDK_PAR_SIS_PARAMETRO,B.PDK_PAR_SIS_VALOR_NUMERO from PDK_PANTALLAS A INNER JOIN PDK_PARAMETROS_SISTEMA B ON A.PDK_PANT_DOCUMENTOS=B.PDK_ID_PARAMETROS_SISTEMA AND B.PDK_PAR_SIS_ID_PADRE=24  where A.PDK_ID_PANTALLAS =" & intpantalla & "")
            OntenerPantallaEntrevista = BD.EjecutarQuery(strSQL.ToString)
            Return OntenerPantallaEntrevista
        Catch ex As Exception
            objException = New Exception("Error al buscar nombre de la pantalla")
            Throw objException
        End Try
    End Function
    Public Shared Function ObtenPersonaJuridica(ByVal intSolicitud As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append("SELECT PDK_ID_PER_JURIDICA FROM PDK_TAB_SECCION_CERO WHERE PDK_ID_SECCCERO=" & intSolicitud & "")
            ObtenPersonaJuridica = BD.EjecutarQuery(strSQL.ToString)
            Return ObtenPersonaJuridica
        Catch ex As Exception
            objException = New Exception("Error al buscar nombre de la pantalla")
            Throw objException
        End Try

    End Function
    Public Shared Function ObtenerObjPantaTarea(ByVal intPantalla As Integer, ByVal intTarea As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT D.PDK_SEC_NOMBRE_TABLA, A.PDK_REL_PANT_OBJ_NOMBRE,A.PDK_REL_PANT_OBJ_TAMANO,A.PDK_REL_PANT_OBJ_ORDEN ")
            strSQL.Append(" FROM PDK_REL_PANTALLA_OBJETO A  INNER JOIN PDK_PANTALLAS C ON C.PDK_ID_PANTALLAS=A.PDK_ID_PANTALLAS  ")
            strSQL.Append("  INNER JOIN PDK_REL_PANTALLA_TAREA Y ON Y.PDK_ID_PANTALLAS=C.PDK_ID_PANTALLAS ")
            strSQL.Append(" INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN PDK_SECCION D ON D.PDK_ID_SECCION=B.PDK_ID_SECCION ")
            strSQL.Append(" WHERE A.PDK_ID_PANTALLAS = " & intPantalla & " AND A.PDK_REL_PANT_OBJ_STATUS=2 AND Y.PDK_ID_TAREAS=" & intTarea & " and PDK_REL_PANT_OBJ_MOSTRAR = 1")
            ObtenerObjPantaTarea = BD.EjecutarQuery(strSQL.ToString)
            Return ObtenerObjPantaTarea
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try

    End Function

    Public Shared Function ObtenObjPan(ByVal intCve As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT D.PDK_SEC_NOMBRE_TABLA, A.PDK_REL_PANT_OBJ_NOMBRE,A.PDK_REL_PANT_OBJ_TAMANO,A.PDK_REL_PANT_OBJ_ORDEN ")
            strSQL.Append(" FROM PDK_REL_PANTALLA_OBJETO A  INNER JOIN PDK_PANTALLAS C ON C.PDK_ID_PANTALLAS=A.PDK_ID_PANTALLAS  ")
            strSQL.Append(" INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN PDK_SECCION D ON D.PDK_ID_SECCION=B.PDK_ID_SECCION ")
            strSQL.Append(" WHERE A.PDK_ID_PANTALLAS = " & intCve & " AND A.PDK_REL_PANT_OBJ_STATUS=2 and PDK_REL_PANT_OBJ_MOSTRAR = 1")
            ObtenObjPan = BD.EjecutarQuery(strSQL.ToString)
            Return ObtenObjPan
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try

    End Function
    Public Shared Function ObtenerSeccObj(ByVal intIdSecc As Integer) As Integer
        Dim objException As Exception = Nothing
        Dim strSQL As String
        Dim BD As New ProdeskNet.BD.clsManejaBD
        ObtenerSeccObj = 0
        Try
            strSQL = "SELECT COUNT(A.PDK_ID_REL_PANTALLA_OBJETO) AS PDK_ID_REL_PANTALLA_OBJETO FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO " & _
                      " WHERE B.PDK_ID_SECCION= " & intIdSecc & " AND A.PDK_REL_PANT_OBJ_STATUS=2 "

            Dim ds As DataSet = BD.EjecutarQuery(strSQL)

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                ObtenerSeccObj = ds.Tables(0).Rows(0).Item("PDK_ID_REL_PANTALLA_OBJETO").ToString.Trim
                Return ObtenerSeccObj
            End If
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try
    End Function
    Public Shared Function ObtenerDatosControl(ByVal intCve As Integer, Optional ByVal intBandera As Integer = 0, Optional ByVal intCveSecc As Integer = 0, Optional ByVal intFolio As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intBandera = 0 Then
                strSQL.AppendLine("DECLARE @tab varchar(200),@nombreTab varchar(max) ,@INSER AS VARCHAR(200),@NUM INT,@MINI INT,@MAXI INT,@VALU VARCHAR(MAX),@NOMCAM VARCHAR(200),@CAMP VARCHAR(50),@TEXT VARCHAR(200),@CONVEN VARCHAR(200)")
                strSQL.AppendLine("SELECT @tab=C.PDK_SEC_NOMBRE_TABLA FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN")
                strSQL.AppendLine("PDK_SECCION C ON B.PDK_ID_SECCION=C.PDK_ID_SECCION  WHERE A.PDK_ID_PANTALLAS=" & intCve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND B.PDK_ID_SECCION=" & intCveSecc & "  GROUP BY C.PDK_SEC_NOMBRE_TABLA ")
                strSQL.AppendLine("set @nombreTab =''")
                strSQL.AppendLine("SET @INSER='INSERT INTO'")
                strSQL.AppendLine("select @nombreTab=@nombreTab+@INSER+' '+ @tab+'('")
                strSQL.AppendLine("SELECT @nombreTab=@nombreTab+REPLACE(A.PDK_REL_PANT_OBJ_NOMBRE,'&#209;','Ñ')+','  FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO =B.PDK_ID_SECCION_DATO ")
                strSQL.AppendLine("WHERE A.PDK_ID_PANTALLAS=" & intCve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND A.PDK_REL_PANT_OBJ_NOMBRE<>REPLACE(@tab,'TAB','ID') AND  B.PDK_ID_SECCION=" & intCveSecc & " ORDER BY A.PDK_ID_REL_PANTALLA_OBJETO ,A.PDK_REL_PANT_OBJ_ORDEN ")
                strSQL.AppendLine("SELECT @nombreTab=@nombreTab+'PDK_FECHA_MODIF,'+'PDK_CLAVE_USUARIO,'+'PDK_ID_SECCCERO)'")
                strSQL.AppendLine("SELECT @nombreTab AS 'CAMPOS'")

            ElseIf intBandera = 1 Then
                strSQL.AppendLine("DECLARE @tab varchar(200)")
                strSQL.AppendLine("SELECT @tab=C.PDK_SEC_NOMBRE_TABLA FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN")
                strSQL.AppendLine(" PDK_SECCION C ON B.PDK_ID_SECCION=C.PDK_ID_SECCION  WHERE A.PDK_ID_PANTALLAS=" & intCve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND B.PDK_ID_SECCION=" & intCveSecc & " GROUP BY C.PDK_SEC_NOMBRE_TABLA  ")
                strSQL.AppendLine("SELECT  CASE WHEN   B.PDK_SEC_DAT_LLAVE<>0 AND A.PDK_REL_PANT_OBJ_NOMBRE<>REPLACE(@tab,'TAB','ID')  THEN   'DropDownList'     ELSE  'TextBox'")
                strSQL.AppendLine("END AS TEXTO, C.PDK_TIP_OBJ_NOMBRE AS  CONVE,")
                strSQL.AppendLine("A.PDK_REL_PANT_OBJ_NOMBRE,A.PDK_REL_PANT_OBJ_NOMBRE+CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO)as LABE,'txt_'+A.PDK_REL_PANT_OBJ_NOMBRE + CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO)as COLUMNA,A.PDK_ID_SECCION_DATO,A.PDK_REL_PANT_OBJ_TAMANO FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO")
                strSQL.AppendLine("INNER JOIN PDK_TIPO_OBJETO C ON C.PDK_ID_TIPO_OBJETO=B.PDK_ID_TIPO_OBJETO")
                strSQL.AppendLine("WHERE a.PDK_ID_PANTALLAS=" & intCve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND B.PDK_ID_SECCION=" & intCveSecc & "  ORDER BY A.PDK_ID_REL_PANTALLA_OBJETO ,A.PDK_REL_PANT_OBJ_ORDEN")

            ElseIf intBandera = 2 Then
                strSQL.AppendLine("SELECT REPLACE(C.PDK_SEC_NOMBRE_TABLA,'TAB','ID')  AS PDK_SEC_NOMBRE_TABLA  FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN")
                strSQL.AppendLine("PDK_SECCION C ON B.PDK_ID_SECCION=C.PDK_ID_SECCION  WHERE A.PDK_ID_PANTALLAS=" & intCve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND  B.PDK_ID_SECCION=" & intCveSecc & " AND PDK_SEC_STATUS = 2 GROUP BY C.PDK_SEC_NOMBRE_TABLA  ")
            ElseIf intBandera = 3 Then
                strSQL.AppendLine("DECLARE @tab varchar(200),@COMPO VARCHAR(200),@sQry VARCHAR(MAX)")
                strSQL.AppendLine("SELECT @tab=C.PDK_SEC_NOMBRE_TABLA FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN")
                strSQL.AppendLine("PDK_SECCION C ON B.PDK_ID_SECCION=C.PDK_ID_SECCION  WHERE A.PDK_ID_PANTALLAS=" & intCve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 GROUP BY C.PDK_SEC_NOMBRE_TABLA ")
                strSQL.AppendLine("SELECT @COMPO= A.PDK_REL_PANT_OBJ_NOMBRE")
                strSQL.AppendLine("FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO WHERE A.PDK_ID_PANTALLAS=" & intCve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND A.PDK_REL_PANT_OBJ_NOMBRE=REPLACE(@tab,'TAB','ID') AND  B.PDK_SEC_DAT_LLAVE<>0   ORDER BY A.PDK_ID_REL_PANTALLA_OBJETO ,A.PDK_REL_PANT_OBJ_ORDEN")
                strSQL.AppendLine("SELECT @sQry =''")
                strSQL.AppendLine("SELECT @sQry =@sQry+' SELECT isnull(MAX('+@COMPO+'),0)+ 1 AS numero'+' FROM '+@tab")
                strSQL.AppendLine("EXEC (@sQry)")
            ElseIf intBandera = 4 Then
                strSQL.AppendLine("DECLARE @MINIMO INT ,@MAXIMO INT ,@NOMBRE VARCHAR(200),@FOLIO INT,@SQL VARCHAR(MAX),@UP VARCHAR(5),@INSE VARCHAR(5)")
                strSQL.AppendLine("CREATE TABLE #TEMPORAL (ID INT IDENTITY,PDK_SEC_NOMBRE_TABLA VARCHAR(200),PDK_SEC_NOMBRE VARCHAR(200),ORDEN INT,PDK_ID_SECCION INT,VALIDA VARCHAR(5))")
                strSQL.AppendLine("INSERT INTO #TEMPORAL (PDK_SEC_NOMBRE_TABLA ,PDK_SEC_NOMBRE ,ORDEN ,PDK_ID_SECCION)")
                strSQL.AppendLine("select DISTINCT C.PDK_SEC_NOMBRE_TABLA,CASE WHEN C.PDK_SEC_TAB_MOSTRAR<>'' THEN C.PDK_SEC_TAB_MOSTRAR ELSE C.PDK_SEC_NOMBRE END PDK_SEC_NOMBRE, MIN(A.PDK_ID_REL_PANTALLA_OBJETO) AS ORDEN,C.PDK_ID_SECCION from PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO ")
                strSQL.AppendLine(" INNER JOIN PDK_SECCION C ON B.PDK_ID_SECCION =C.PDK_ID_SECCION where A.PDK_ID_PANTALLAS = " & intCve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 and c.PDK_SEC_STATUS = 2 GROUP BY C.PDK_SEC_NOMBRE_TABLA,C.PDK_ID_SECCION ,CASE WHEN  C.PDK_SEC_TAB_MOSTRAR<>'' THEN C.PDK_SEC_TAB_MOSTRAR ELSE C.PDK_SEC_NOMBRE END ORDER BY ORDEN")
                strSQL.AppendLine("SET @FOLIO =" & intFolio & " SET @UP ='U' SET @INSE='I'")
                strSQL.AppendLine("declare @PersJur int; select @PersJur = PDK_ID_PER_JURIDICA from PDK_TAB_SECCION_CERO where PDK_ID_SECCCERO = @FOLIO; if @PersJur = 3 begin update #TEMPORAL set PDK_SEC_NOMBRE = replace(PDK_SEC_NOMBRE, 'OBLIGADO SOLIDARIO', 'REPRESENTANTE LEGAL') end")
                strSQL.AppendLine("SELECT @MINIMO=MIN(ID) FROM  #TEMPORAL")
                strSQL.AppendLine("SELECT @MAXIMO =MAX(ID) FROM  #TEMPORAL")
                strSQL.AppendLine("WHILE @MINIMO<=@MAXIMO")
                strSQL.AppendLine("BEGIN")
                strSQL.AppendLine("SELECT @NOMBRE=PDK_SEC_NOMBRE_TABLA FROM  #TEMPORAL WHERE ID=@MINIMO")
                strSQL.AppendLine("SELECT @SQL='IF EXISTS (SELECT * FROM'+' '+@NOMBRE+' '+'WHERE PDK_ID_SECCCERO='+CONVERT(VARCHAR,@FOLIO)+')'")
                strSQL.AppendLine("SELECT @SQL=@SQL+' '+'BEGIN'")
                strSQL.AppendLine(" SELECT @SQL=@SQL+' '+'UPDATE #TEMPORAL SET VALIDA='+''''+@UP+''''+' WHERE ID='+CONVERT(VARCHAR,@MINIMO)")
                strSQL.AppendLine(" SELECT @SQL=@SQL+' '+'END'")
                strSQL.AppendLine("SELECT @SQL=@SQL+' '+'ELSE'")
                strSQL.AppendLine("SELECT @SQL=@SQL+' '+'BEGIN'")
                strSQL.AppendLine("SELECT @SQL=@SQL+' '+'UPDATE #TEMPORAL SET VALIDA='+''''+@INSE+''''+' WHERE ID='+CONVERT(VARCHAR,@MINIMO)")
                strSQL.AppendLine("SELECT @SQL=@SQL+' '+'END'")
                strSQL.AppendLine("EXEC(@SQL)")
                strSQL.AppendLine(" SET @MINIMO =@MINIMO +1")
                strSQL.AppendLine("END")
                strSQL.AppendLine(" SELECT * FROM #TEMPORAL")
                strSQL.AppendLine("  DROP TABLE #TEMPORAL")
            End If


            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try


    End Function

    Public Shared Function ObtenerControles(ByVal cvePantalla As Integer, Optional ByVal intBandera As Integer = 0, Optional ByVal intCveSeccio As Integer = 0, Optional ByVal intFolio As Integer = 0, Optional ByVal intSeccion As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intBandera = 0 Then
                strSQL.AppendLine("DECLARE @tab varchar(200),@MINI INT,@MAXI INT,@NOMBRE VARCHAR(200),@SQL VARCHAR(MAX),@FOLIO INT,@FECHA VARCHAR(200),@TIPO VARCHAR(50),@CLAVESECCIO INT,@REGIS VARCHAR(200),@VALORRATI INT,@VIVE VARCHAR(50)")
                strSQL.AppendLine("SELECT @tab=C.PDK_SEC_NOMBRE_TABLA FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN")
                strSQL.AppendLine(" PDK_SECCION C ON B.PDK_ID_SECCION=C.PDK_ID_SECCION  WHERE A.PDK_ID_PANTALLAS=" & cvePantalla & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND B.PDK_ID_SECCION=" & intCveSeccio & " GROUP BY C.PDK_SEC_NOMBRE_TABLA  ")
                strSQL.AppendLine("CREATE TABLE #TEMPORAL(ID INT IDENTITY ,TEXTO VARCHAR(200),CONVE VARCHAR(200),PDK_REL_PANT_OBJ_NOMBRE VARCHAR(200),LABE VARCHAR(200),COLUMNA VARCHAR(200) ,PDK_ID_SECCION_DATO INT,PDK_REL_PANT_OBJ_TAMANO INT,REGISTRO VARCHAR(200),TIPO VARCHAR(50),MOSTRAR VARCHAR(MAX), TOOLTIP VARCHAR(250), CVEPANTALLA INT,ACTIVA INT)")
                strSQL.AppendLine("INSERT INTO #TEMPORAL(TEXTO ,CONVE,PDK_REL_PANT_OBJ_NOMBRE ,LABE ,COLUMNA  ,PDK_ID_SECCION_DATO ,PDK_REL_PANT_OBJ_TAMANO,TIPO,MOSTRAR,TOOLTIP,CVEPANTALLA,ACTIVA )")
                strSQL.AppendLine("SELECT  CASE WHEN   B.PDK_SEC_DAT_LLAVE<>0 AND A.PDK_REL_PANT_OBJ_NOMBRE<>REPLACE(@tab,'TAB','ID')  THEN   'DropDownList'     ELSE  'TextBox'")
                strSQL.AppendLine("END AS TEXTO, C.PDK_TIP_OBJ_NOMBRE AS  CONVE,")
                strSQL.AppendLine("REPLACE(A.PDK_REL_PANT_OBJ_NOMBRE,'&#209;','Ñ')AS PDK_REL_PANT_OBJ_NOMBRE,A.PDK_REL_PANT_OBJ_NOMBRE+CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO)as LABE,'txt'+A.PDK_REL_PANT_OBJ_NOMBRE + CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO)as COLUMNA,A.PDK_ID_SECCION_DATO,A.PDK_REL_PANT_OBJ_TAMANO,P.PDK_PAR_SIS_PARAMETRO,CASE WHEN PDK_SEC_MOSTRA_PANT<>'' THEN PDK_SEC_MOSTRA_PANT ELSE REPLACE(REPLACE(REPLACE(REPLACE(A.PDK_REL_PANT_OBJ_NOMBRE,'ANOS','AÑOS'),'MENSIAL','MENSUAL'),'ANO_','AÑOS_'),'_',' ') END,B.PDK_TOOLTIP, " & cvePantalla & ",1 FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO")
                strSQL.AppendLine("INNER JOIN PDK_TIPO_OBJETO C ON C.PDK_ID_TIPO_OBJETO=B.PDK_ID_TIPO_OBJETO")
                strSQL.AppendLine("INNER JOIN PDK_PARAMETROS_SISTEMA P ON B.PDK_SEC_TIPO_CAMPO_OBJETO=P.PDK_ID_PARAMETROS_SISTEMA AND P.PDK_PAR_SIS_ID_PADRE=60")
                'D@vE_® valida que el dato este activo and PDK_SEC_DAT_STATUS = 2
                strSQL.AppendLine("WHERE a.PDK_ID_PANTALLAS=" & cvePantalla & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND B.PDK_ID_SECCION=" & intCveSeccio & " AND A.PDK_REL_PANT_OBJ_NOMBRE<>REPLACE(@tab,'TAB','ID') and PDK_SEC_DAT_STATUS = 2 and PDK_REL_PANT_OBJ_MOSTRAR = 1 ORDER BY  A.PDK_REL_PANT_OBJ_ORDEN,A.PDK_ID_REL_PANTALLA_OBJETO ")
                'D@vE_® valida que el dato este activo and PDK_SEC_DAT_STATUS = 2
                strSQL.AppendLine("SET @FOLIO=" & intFolio & "")
                strSQL.AppendLine("SELECT @MINI=MIN(ID) FROM #TEMPORAL")
                strSQL.AppendLine("SELECT @MAXI=MAX(ID) FROM #TEMPORAL")
                strSQL.AppendLine("WHILE @MINI<=@MAXI")
                strSQL.AppendLine("BEGIN")
                strSQL.AppendLine("SELECT @NOMBRE=PDK_REL_PANT_OBJ_NOMBRE,@FECHA=CONVE,@TIPO=TIPO,@CLAVESECCIO =PDK_ID_SECCION_DATO FROM #TEMPORAL WHERE ID=@MINI")
                strSQL.AppendLine("IF @FECHA='FECHA'")
                strSQL.AppendLine("BEGIN ")
                'strSQL.AppendLine("SELECT @SQL='DECLARE @NOMREG VARCHAR(25) '") 'Cambio para fecha
                strSQL.AppendLine("IF @NOMBRE = 'PDK_FECHA_MODIF' BEGIN SELECT @SQL = 'DECLARE @NOMREG DATE ' END ELSE	BEGIN SELECT @SQL = 'DECLARE @NOMREG VARCHAR(25) ' END ")
                strSQL.AppendLine(" SELECT @SQL=@SQL+' '+'SELECT top 1'+' '+'@NOMREG='+CONVERT(VARCHAR,@NOMBRE)+' '+'FROM'+' '+@tab+' '+'WHERE PDK_ID_SECCCERO='+CONVERT(VARCHAR,@FOLIO)+' '+'ORDER BY'+' '+ REPLACE(@tab,'TAB','ID')+' '+' DESC'")
                strSQL.AppendLine("  SELECT @SQL = @SQL+' '+'UPDATE #TEMPORAL SET REGISTRO=@NOMREG WHERE ID='+CONVERT(VARCHAR,@MINI)")
                strSQL.AppendLine("END")
                strSQL.AppendLine("ELSE")
                strSQL.AppendLine("BEGIN")
                strSQL.AppendLine("SELECT @SQL='DECLARE @NOMREG VARCHAR(200) '")
                strSQL.AppendLine("SELECT @SQL=@SQL+' '+'SELECT top 1'+' '+'@NOMREG='+@NOMBRE+' '+'FROM'+' '+@tab+' '+'WHERE PDK_ID_SECCCERO='+CONVERT(VARCHAR,@FOLIO)+' '+'ORDER BY'+' '+ REPLACE(@tab,'TAB','ID')+' '+' DESC'")
                strSQL.AppendLine("SELECT @SQL = @SQL+' '+'UPDATE #TEMPORAL SET REGISTRO=@NOMREG WHERE ID='+CONVERT(VARCHAR,@MINI)")
                strSQL.AppendLine("END")
                strSQL.AppendLine("EXEC(@SQL)")
                strSQL.AppendLine("IF @TIPO='RADIOBUTTON'")
                strSQL.AppendLine("BEGIN")
                strSQL.AppendLine("SELECT @REGIS=REGISTRO FROM #TEMPORAL WHERE PDK_ID_SECCION_DATO=@CLAVESECCIO")
                strSQL.AppendLine("IF @REGIS IS NULL")
                strSQL.AppendLine("BEGIN")
                strSQL.AppendLine("SELECT @VALORRATI=MIN(PDK_ID_SCORING) FROM PDK_SCORING WHERE PDK_ID_SECCION_DATO=@CLAVESECCIO")
                strSQL.AppendLine("UPDATE #TEMPORAL SET REGISTRO=@VALORRATI WHERE PDK_ID_SECCION_DATO=@CLAVESECCIO")
                strSQL.AppendLine("END")
                strSQL.AppendLine("UPDATE #TEMPORAL SET REGISTRO=S.PDK_SCORING_VALOR1")
                strSQL.AppendLine("FROM #TEMPORAL A INNER JOIN PDK_SCORING S ON A.REGISTRO= CONVERT(VARCHAR(200), S.PDK_ID_SCORING)")
                strSQL.AppendLine("END")
                strSQL.AppendLine("SELECT @VIVE=REGISTRO FROM  #TEMPORAL WHERE PDK_ID_SECCION_DATO=@CLAVESECCIO")
                strSQL.AppendLine("IF @VIVE='RENTADA'")
                strSQL.AppendLine("BEGIN")
                strSQL.AppendLine("UPDATE #TEMPORAL  SET ACTIVA=2 FROM #TEMPORAL WHERE PDK_REL_PANT_OBJ_NOMBRE='IMPORTE_RENTA'")
                strSQL.AppendLine("END")
                strSQL.AppendLine("SET @MINI=@MINI+1")
                strSQL.AppendLine("END")
                strSQL.AppendLine("UPDATE #TEMPORAL  SET REGISTRO='N'  FROM #TEMPORAL A   WHERE ISNULL(A.REGISTRO,'')=' ' ")
                strSQL.AppendLine("SELECT * FROM #TEMPORAL")
                strSQL.AppendLine("DROP TABLE #TEMPORAL")

            ElseIf intBandera = 1 Then
                strSQL.AppendLine("SELECT PDK_PANT_NOMBRE FROM PDK_PANTALLAS WHERE PDK_ID_PANTALLAS =" & cvePantalla & " ")
            ElseIf intBandera = 2 Then
                'strSQL.AppendLine("SELECT PDK_ID_SCORING ,PDK_SCORING_VALOR1  FROM PDK_SCORING WHERE PDK_ID_SECCION_DATO = " & intSeccion & " ORDER BY PDK_SCORING_VALOR1 ASC ")
                strSQL.AppendLine("SELECT PDK_ID_SCORING ,PDK_SCORING_VALOR1  FROM PDK_SCORING WHERE PDK_ID_SECCION_DATO=" & intSeccion & "")

            ElseIf intBandera = 3 Then
                strSQL.AppendLine("DECLARE @MINI INT,@MAXI INT,@CVECERO INT,@CONSU VARCHAR(MAX)")
                strSQL.AppendLine("CREATE TABLE #TEMPORAL(ID INT IDENTITY ,PDK_ID_SECCCERO INT) ")
                strSQL.AppendLine("INSERT INTO #TEMPORAL (PDK_ID_SECCCERO)")
                strSQL.AppendLine("SELECT ISNULL(MAX(PDK_ID_SECCCERO),1)  FROM PDK_TAB_SECCION_CERO")
                strSQL.AppendLine("SELECT @MINI=MIN(ID) FROM #TEMPORAL")
                strSQL.AppendLine("SELECT @MAXI=MAX(ID) FROM #TEMPORAL")
                strSQL.AppendLine("SET @CONSU=''")
                strSQL.AppendLine("WHILE @MINI<=@MAXI")
                strSQL.AppendLine("BEGIN")
                strSQL.AppendLine("SELECT @CVECERO=PDK_ID_SECCCERO FROM #TEMPORAL WHERE ID=@MINI")
                strSQL.AppendLine("SET @CONSU='CREATE TABLE #TEM(COLUM INT)'")
                strSQL.AppendLine("SET @CONSU=@CONSU+'DECLARE @NUM AS INT SET @NUM='+CONVERT(VARCHAR(20),@CVECERO)")
                strSQL.AppendLine("select @CONSU=@CONSU+' '+'IF EXISTS( SELECT * FROM'+' '+ C.PDK_SEC_NOMBRE_TABLA+' '+'WHERE PDK_ID_SECCCERO'+'=@NUM)'+' '+'BEGIN'+' '+'SELECT'+' @NUM=@NUM+1'+' '+'INSERT INTO #TEM(COLUM)VALUES(@NUM)'+' END'+' '+' ELSE BEGIN INSERT INTO #TEM(COLUM)VALUES(@NUM) '+'END' from PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO ")
                strSQL.AppendLine("INNER JOIN PDK_SECCION C ON B.PDK_ID_SECCION =C.PDK_ID_SECCION where A.PDK_ID_PANTALLAS = " & cvePantalla & " GROUP BY C.PDK_SEC_NOMBRE_TABLA")
                strSQL.AppendLine("SELECT   @CONSU=@CONSU+' '+'SELECT ISNULL(MAX(COLUM),0) AS FOLIO FROM #TEM'")
                strSQL.AppendLine("SELECT  @CONSU=@CONSU+' '+'DROP TABLE #TEM'")
                strSQL.AppendLine(" exec(@CONSU)")
                strSQL.AppendLine("SET @MINI =@MINI+1")
                strSQL.AppendLine("End")
                strSQL.AppendLine("DROP TABLE #TEMPORAL")
            ElseIf intBandera = 4 Then
                strSQL.AppendLine("SELECT ISNULL(MAX(PDK_ID_SECCCERO),1) AS CVE FROM dbo.PDK_TAB_SECCION_CERO")
            ElseIf intBandera = 5 Then
                strSQL.AppendLine("SELECT A.PDK_ID_PRODPLAZO,A.PDK_PRODPLAZO_VALOR FROM PDK_PRODUCTO_PLAZO A ")
                strSQL.AppendLine("INNER JOIN PDK_CAT_PRODUCTOS P ON A.PDK_ID_PRODUCTOS=P.PDK_ID_PRODUCTOS ")
                strSQL.AppendLine("INNER JOIN PDK_CAT_FLUJOS F ON F.PDK_ID_PRODUCTOS=P.PDK_ID_PRODUCTOS ")
                strSQL.AppendLine("INNER JOIN PDK_CAT_PROCESOS PC ON PC.PDK_ID_FLUJOS=F.PDK_ID_FLUJOS ")
                strSQL.AppendLine("INNER JOIN PDK_CAT_TAREAS T ON T.PDK_ID_PROCESOS=PC.PDK_ID_PROCESOS ")
                strSQL.AppendLine("INNER JOIN PDK_REL_PANTALLA_TAREA TP ON TP.PDK_ID_TAREAS=T.PDK_ID_TAREAS ")
                strSQL.AppendLine("INNER JOIN PDK_REL_PANTALLA_OBJETO PO ON PO.PDK_ID_PANTALLAS=TP.PDK_ID_PANTALLAS ")
                strSQL.AppendLine("WHERE TP.PDK_ID_PANTALLAS = " & cvePantalla & " And PO.PDK_ID_SECCION_DATO = " & intSeccion & " And PDK_PRODPLAZO_STATUS=2 and PDK_REL_PANT_OBJ_MOSTRAR = 1")

            End If



            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try

    End Function
    Public Shared Function ObtenerCpCol(ByVal intcp As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.AppendLine("SELECT  C.CPO_FL_CP,C.CPO_DS_COLONIA FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE  INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE ")
            strSQL.AppendLine("INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE  where C.CPO_CL_CODPOSTAL =" & intcp & "")
            strSQL.AppendLine("UNION SELECT '-1'[CPO_FL_CP], 'OTRO'[CPO_DS_COLONIA] ORDER BY [CPO_FL_CP] DESC")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try
    End Function
    Public Shared Function OBTENERRUTA(ByVal intFolio As Integer, ByVal intPantalla As Integer, ByVal intUsuario As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As String
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL = "EXEC sp_GenerarRura " & intFolio & "," & intPantalla & "," & intUsuario & ""
            OBTENERRUTA = BD.EjecutarQuery(strSQL)
            Return OBTENERRUTA
        Catch ex As Exception
            objException = New Exception("Error la ruta")
            Throw objException
        End Try
    End Function


    Public Shared Function ObtenTodos() As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_REL_PANTALLA_OBJETO,")
            strSQL.Append(" A.PDK_REL_PANT_OBJ_NOMBRE,")
            strSQL.Append(" A.PDK_REL_PANT_OBJ_TAMANO,")
            strSQL.Append(" A.PDK_REL_PANT_OBJ_ORDEN,")
            strSQL.Append(" A.PDK_REL_PANT_OBJ_STATUS,")
            strSQL.Append(" A.PDK_REL_PANT_OBJ_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_ID_SECCION_DATO,")
            'strSQL.Append(" A.PDK_ID_TIPO_OBJETO,")
            'strSQL.Append(" A.PDK_ID_SECCION,")
            strSQL.Append(" A.PDK_ID_PANTALLAS ")
            strSQL.Append(" FROM PDK_REL_PANTALLA_OBJETO A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Try
            If intPDK_ID_REL_PANTALLA_OBJETO = 0 Then
                Me.intPDK_ID_REL_PANTALLA_OBJETO = BD.ObtenConsecutivo("PDK_ID_REL_PANTALLA_OBJETO", "PDK_REL_PANTALLA_OBJETO", Nothing)
                strSql = "IF NOT EXISTS (SELECT * FROM PDK_REL_PANTALLA_OBJETO WHERE PDK_ID_SECCION_DATO=" & intPDK_ID_SECCION_DATO & " AND PDK_ID_PANTALLAS=" & intPDK_ID_PANTALLAS & ") BEGIN " & _
                    " INSERT INTO PDK_REL_PANTALLA_OBJETO " & _
                                "(" & _
"PDK_ID_REL_PANTALLA_OBJETO,PDK_REL_PANT_OBJ_NOMBRE,PDK_REL_PANT_OBJ_TAMANO,PDK_REL_PANT_OBJ_ORDEN,PDK_REL_PANT_OBJ_STATUS,PDK_REL_PANT_OBJ_MODIF,PDK_CLAVE_USUARIO,PDK_ID_SECCION_DATO,PDK_ID_PANTALLAS,PDK_REL_PANT_OBJ_MOSTRAR )" & _
" VALUES ( " & intPDK_ID_REL_PANTALLA_OBJETO & ", '" & strPDK_REL_PANT_OBJ_NOMBRE & "', " & intPDK_REL_PANT_OBJ_TAMANO & ",  " & intPDK_REL_PANT_OBJ_ORDEN & ",  " & intPDK_REL_PANT_OBJ_STATUS & ", '" & strPDK_REL_PANT_OBJ_MODIF & "', " & intPDK_CLAVE_USUARIO & ", " & _
 " " & intPDK_ID_SECCION_DATO & "," & intPDK_ID_PANTALLAS & "," & intPDK_REL_PANT_OBJ_MOSTRAR & "   " & _
") END"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
            'PDK_ID_REL_PANTALLA_OBJETO = Me.intPDK_ID_REL_PANTALLA_OBJETO
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_REL_PANTALLA_OBJETO ")
        End Try
    End Sub
    Public Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            Me.intPDK_ID_REL_PANTALLA_OBJETO = BD.ObtenConsecutivo("PDK_ID_REL_PANTALLA_OBJETO", "PDK_REL_PANTALLA_OBJETO", Nothing)
            strSql = "IF EXISTS (SELECT * FROM PDK_REL_PANTALLA_OBJETO WHERE PDK_ID_SECCION_DATO=" & intPDK_ID_SECCION_DATO & "  AND PDK_ID_PANTALLAS=" & intPDK_ID_PANTALLAS & ") BEGIN " & _
               " UPDATE PDK_REL_PANTALLA_OBJETO " & _
               "SET " & _
               " PDK_REL_PANT_OBJ_ORDEN = " & intPDK_REL_PANT_OBJ_ORDEN & ", " & _
               " PDK_REL_PANT_OBJ_MODIF = '" & strPDK_REL_PANT_OBJ_MODIF & "'," & _
             " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & "," & _
             " PDK_REL_PANT_OBJ_MOSTRAR = " & intPDK_REL_PANT_OBJ_MOSTRAR & ", " & _
             " PDK_REL_PANT_OBJ_STATUS= " & intPDK_REL_PANT_OBJ_STATUS & " " & _
             " WHERE PDK_ID_SECCION_DATO=  " & intPDK_ID_SECCION_DATO & " AND PDK_ID_PANTALLAS=" & intPDK_ID_PANTALLAS & " " & _
             " END " & _
             " ELSE " & _
             " BEGIN " & _
             " INSERT INTO PDK_REL_PANTALLA_OBJETO " & _
                                "(" & _
"PDK_ID_REL_PANTALLA_OBJETO,PDK_REL_PANT_OBJ_NOMBRE,PDK_REL_PANT_OBJ_TAMANO,PDK_REL_PANT_OBJ_ORDEN,PDK_REL_PANT_OBJ_STATUS,PDK_REL_PANT_OBJ_MODIF,PDK_CLAVE_USUARIO,PDK_ID_SECCION_DATO,PDK_ID_PANTALLAS,PDK_REL_PANT_OBJ_MOSTRAR )" & _
" VALUES ( " & intPDK_ID_REL_PANTALLA_OBJETO & ", '" & strPDK_REL_PANT_OBJ_NOMBRE & "', " & intPDK_REL_PANT_OBJ_TAMANO & ",  " & intPDK_REL_PANT_OBJ_ORDEN & ",  " & intPDK_REL_PANT_OBJ_STATUS & ", '" & strPDK_REL_PANT_OBJ_MODIF & "', " & intPDK_CLAVE_USUARIO & ", " & _
 " " & intPDK_ID_SECCION_DATO & "," & intPDK_ID_PANTALLAS & " ," & intPDK_REL_PANT_OBJ_MOSTRAR & " " & _
") END "


            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_REL_PANTALLA_OBJETO")
        End Try
    End Sub
    Public Sub EliminarPantObj(ByVal intPantalla As Integer, ByVal intSeccion As Integer)
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "DELETE FROM  PDK_REL_PANTALLA_OBJETO WHERE PDK_ID_PANTALLAS =" & intPantalla & " AND PDK_ID_SECCION_DATO IN(SELECT A.PDK_ID_SECCION_DATO FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO WHERE B.PDK_ID_SECCION=" & intSeccion & ")"
            BD.EjecutarQuery(strSql)

        Catch ex As Exception
            Throw New Exception("Error al eliminar PDK_REL_PANTALLA_OBJETO")
        End Try
    End Sub

    Public Function fnMuestraValidaDatos(ByVal usuario) As Boolean
        Dim bd As New ProdeskNet.BD.clsManejaBD
        If bd.EjecutarQuery("select * from PDK_USUARIO a inner join PDK_REL_USU_PEr b on a.PDK_ID_USUARIO = b.PDK_ID_USUARIO inner join PDK_PERMISOS c on b.PDK_ID_PERFIL = c.PDK_ID_PERFIL where a.PDK_ID_USUARIO = " & usuario & " and PDK_ID_MENU = 46").Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function ObtenDatosImpSolicitud(intsolicitud As Integer) As DataSet

        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim Dat As New DataSet
        Dim strError As String = ""
        ObtenDatosImpSolicitud = Nothing

        Try
            Dim objSD As New ProdeskNet.BD.clsManejaBD

            With objSD
                .AgregaParametro("@innFolio", ProdeskNet.BD.TipoDato.Entero, Trim(intsolicitud))


                If objSD.ErrorBD <> "" Then Exit Function
                ObtenDatosImpSolicitud = objSD.EjecutaStoredProcedure("spImprimeSolicitud")

                Return ObtenDatosImpSolicitud

            End With

        Catch ex As Exception
            objException = New Exception("Error al recuperar información de la Solicitud")
            Throw objException
        End Try
    End Function
    Public Shared Function ObtenDatosImpSolicitudCredito(intsolicitud As Integer) As DataSet

        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim Dat As New DataSet
        Dim strError As String = ""
        ObtenDatosImpSolicitudCredito = Nothing

        Try
            Dim objSD As New ProdeskNet.BD.clsManejaBD

            With objSD
                .AgregaParametro("@idsolicitud", ProdeskNet.BD.TipoDato.Entero, Trim(intsolicitud))


                If objSD.ErrorBD <> "" Then Exit Function
                ObtenDatosImpSolicitudCredito = objSD.EjecutaStoredProcedure("spImprimeSolicitudCredito")

                Return ObtenDatosImpSolicitudCredito

            End With

        Catch ex As Exception
            objException = New Exception("Error al recuperar información de la Solicitud")
            Throw objException
        End Try
    End Function


    Public Shared Function tareaActual(ByVal solicitud) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT")
            strSQL.Append(" PDK_OPE_SOLICITUD.PDK_ID_OPE_SOLICITUD ")
            strSQL.Append(" ,PDK_OPE_SOLICITUD.PDK_ID_TAREAS ")
            strSQL.Append(" ,PDK_CAT_TAREAS.PDK_TAR_NOMBRE ")
            strSQL.Append(" ,PDK_OPE_SOLICITUD.PDK_OPE_USU_ASIGNADO ")
            strSQL.Append(" ,PDK_USUARIO.PDK_USU_NOMBRE + ' ' + PDK_USUARIO.PDK_USU_APE_PAT + ' ' + PDK_USUARIO.PDK_USU_APE_MAT USU_ASIGNADO_ACTUAL ")
            strSQL.Append(" ,PDK_USUARIO.PDK_USU_CLAVE ")
            strSQL.Append(" FROM PDK_OPE_SOLICITUD ")
            strSQL.Append(" INNER JOIN PDK_CAT_TAREAS ")
            strSQL.Append(" ON PDK_CAT_TAREAS.PDK_ID_TAREAS = PDK_OPE_SOLICITUD.PDK_ID_TAREAS ")
            strSQL.Append(" INNER JOIN PDK_USUARIO ")
            strSQL.Append(" ON PDK_USUARIO.PDK_ID_USUARIO = PDK_OPE_SOLICITUD.PDK_OPE_USU_ASIGNADO ")
            strSQL.Append(" WHERE PDK_OPE_STATUS_TAREA = 40")
            strSQL.Append(" AND PDK_OPE_SOLICITUD.PDK_ID_SOLICITUD = " & solicitud & " ")
            strSQL.Append(" ORDER BY PDK_OPE_SOLICITUD.PDK_ID_OPE_SOLICITUD DESC ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar el estado de la tarea actual.")
            Throw objException
        End Try
    End Function

#End Region
    '-------------------------- FIN PDK_REL_PANTALLA_OBJETO-------------------------- 

End Class
