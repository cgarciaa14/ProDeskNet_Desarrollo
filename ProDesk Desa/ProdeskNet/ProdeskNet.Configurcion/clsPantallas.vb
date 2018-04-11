Imports System.Text
Imports System.Configuration


Public Class clsPantallas


    '-------------------------- INICIO PDK_PANTALLAS-------------------------- 
#Region "trakers"
    ' Se agregó en la función ObtenerlosTabs el caso 7 el cuál devuelve toda la info de la tabla PDK_TAB_SECCION_CERO para así obtner los datos de la solicitud y pder saber información como qué tipo de persona es.
    ' YAM-P-208  egonzalez 12/10/2015 En la función ObtenerlosTabs se modificó el query del caso 1 para obtener la cotización sin importar la persona física que se maneje
    ' BBV-P-423: RQSOL-04: AVH: 06/12/2016 Se remplaza el nombre d ela Base de Datos
    ' BBV-P-423 RQCONYFOR-07: AVH: 18/01/2017 SE OBTIENE NombreBD DESDE WEB.CONFIG
    ' BBV-P-423: BUG-PD-60: ERODRIGUEZ: 22/05/2017 Mensaje de error al introducir números demasiado grandes en la busqueda por solicitud se cambio el parametro de entrada para ObtenerlosTabs de Integer a int64
#End Region


#Region "Variables"
    Private intPDK_ID_PANTALLAS As Integer = 0
    Private strPDK_PANT_NOMBRE As String = String.Empty
    Private intPDK_PANT_ORDEN As Integer = 0
    Private intPDK_PANT_STATUS As Integer = 0
    Private strPDK_PANT_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
    Private strPDK_PANT_LINK As String = String.Empty
    Private intPDK_ID_TAREAS As Integer = 0
    Private intPDK_PANT_MOSTRAR As Integer = 0
    Private intPDK_PANT_DOCUMENTOS As Integer = 0
    Private arrPantallasObjeto() As clsPantallaObjeto


#End Region
#Region "Propiedades"
    Public Property pantallasObjeto() As clsPantallaObjeto()
        Get
            Return arrPantallasObjeto
        End Get
        Set(value As clsPantallaObjeto())
            arrPantallasObjeto = value
        End Set
    End Property


    Public Property PDK_ID_PANTALLAS() As Integer
        Get
            Return intPDK_ID_PANTALLAS
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_PANTALLAS = value
        End Set
    End Property
    Public Property PDK_PANT_NOMBRE() As String
        Get
            Return strPDK_PANT_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_PANT_NOMBRE = value
        End Set
    End Property
    Public Property PDK_PANT_ORDEN() As Integer
        Get
            Return intPDK_PANT_ORDEN
        End Get
        Set(ByVal value As Integer)
            intPDK_PANT_ORDEN = value
        End Set
    End Property
    Public Property PDK_PANT_STATUS() As Integer
        Get
            Return intPDK_PANT_STATUS
        End Get
        Set(ByVal value As Integer)
            intPDK_PANT_STATUS = value
        End Set
    End Property
    Public Property PDK_PANT_MODIF() As String
        Get
            Return strPDK_PANT_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_PANT_MODIF = value
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

    Public Property PDK_PANT_LINK() As String
        Get
            Return strPDK_PANT_LINK

        End Get
        Set(ByVal value As String)
            strPDK_PANT_LINK = value

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
    Public Property PDK_PANT_DOCUMENTOS() As Integer
        Get
            Return intPDK_PANT_DOCUMENTOS
        End Get
        Set(ByVal value As Integer)
            intPDK_PANT_DOCUMENTOS = value

        End Set
    End Property
    Public Property PDK_PANT_MOSTRAR() As Integer
        Get
            Return intPDK_PANT_MOSTRAR
        End Get
        Set(ByVal value As Integer)
            intPDK_PANT_MOSTRAR = value

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
            strSQL.Append(" PDK_ID_PANTALLAS,")
            strSQL.Append(" PDK_PANT_NOMBRE,")
            strSQL.Append(" PDK_PANT_ORDEN,")
            strSQL.Append(" PDK_PANT_STATUS,")
            strSQL.Append(" PDK_PANT_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" PDK_PANT_LINK,")
            strSQL.Append(" PDK_PANT_MOSTRAR")
            strSQL.Append(" FROM PDK_PANTALLAS ")
            strSQL.Append(" WHERE PDK_ID_PANTALLAS = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_PANTALLAS = .Item("PDK_ID_PANTALLAS")
                Me.strPDK_PANT_NOMBRE = .Item("PDK_PANT_NOMBRE")
                Me.intPDK_PANT_ORDEN = .Item("PDK_PANT_ORDEN")
                Me.intPDK_PANT_STATUS = .Item("PDK_PANT_STATUS")
                Me.strPDK_PANT_MODIF = .Item("PDK_PANT_MODIF")
                Me.strPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.strPDK_PANT_LINK = .Item("PDK_PANT_LINK")
                Me.intPDK_PANT_MOSTRAR = .Item("PDK_PANT_MOSTRAR")
            End With
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try
    End Sub
    Public Shared Function ObtenerPantalla(ByVal intEmp As Integer, ByVal intProduto As Integer, ByVal intFlujo As Integer, ByVal usuario As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            'strSQL.Append(" SELECT case when PDK_TAR_PCONSULTA = 1 then ltrim(A.PDK_ID_PANTALLAS) else '' end PDK_ID_PANTALLAS, A.PDK_PANT_NOMBRE,A.PDK_PANT_LINK ,D.PDK_PROC_NOMBRE,C.PDK_TAR_NOMBRE,A.PDK_PANT_ORDEN,S.PDK_PAR_SIS_PARAMETRO ")
            strSQL.Append(" SELECT ltrim(PP.PDK_ID_PANTALLAS) PDK_ID_PANTALLAS, ISNULL(PP.PDK_PANT_NOMBRE, 'SIN PANTALLA') PDK_PANT_NOMBRE, ISNULL(PP.PDK_PANT_LINK, 'SIN PANTALLA') PDK_PANT_LINK ,PDK_PROC_NOMBRE, CPT.PDK_TAR_NOMBRE, PP.PDK_PANT_ORDEN, PPS.PDK_PAR_SIS_PARAMETRO ") 'D@ver no se puede actualizar
            strSQL.Append(" FROM PDK_CAT_PRODUCTOS PCPROD")
            strSQL.Append(" INNER JOIN PDK_CAT_FLUJOS PCF")
            strSQL.Append(" ON PCPROD.PDK_ID_PRODUCTOS = PCF.PDK_ID_PRODUCTOS")
            strSQL.Append(" INNER JOIN PDK_CAT_PROCESOS PCP")
            strSQL.Append(" ON PCF.PDK_ID_FLUJOS = PCP.PDK_ID_FLUJOS")
            strSQL.Append(" INNER JOIN PDK_CAT_TAREAS CPT")
            strSQL.Append(" ON PCP.PDK_ID_PROCESOS = CPT.PDK_ID_PROCESOS")
            strSQL.Append(" LEFT OUTER JOIN PDK_REL_PANTALLA_TAREA PRPT")
            strSQL.Append(" ON CPT.PDK_ID_TAREAS = PRPT.PDK_ID_TAREAS")
            strSQL.Append(" LEFT OUTER JOIN PDK_PANTALLAS PP")
            strSQL.Append(" ON PRPT.PDK_ID_PANTALLAS = PP.PDK_ID_PANTALLAS")
            strSQL.Append(" LEFT OUTER JOIN PDK_PARAMETROS_SISTEMA PPS")
            strSQL.Append(" ON PP.PDK_PANT_STATUS = PPS.PDK_ID_PARAMETROS_SISTEMA ")
            strSQL.Append(" WHERE PDK_ID_EMPRESA=" & intEmp)
            strSQL.Append(" AND PCPROD.PDK_ID_PRODUCTOS=" & intProduto)
            strSQL.Append(" AND PCF.PDK_ID_FLUJOS= " & intFlujo)
            strSQL.Append(" AND ISNULL(PP.pdk_clave_usuario, 1) = " & usuario)

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds

        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_PANTALLAS")
            Throw objException
        End Try
    End Function
    Public Shared Function ObteInfoPantalla(ByVal intIdPantalla As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT A.PDK_ID_PANTALLAS,A.PDK_PANT_NOMBRE,A.PDK_PANT_LINK,A.PDK_PANT_ORDEN,A.PDK_PANT_STATUS,A.PDK_PANT_MOSTRAR,T.PDK_ID_TAREAS,T.PDK_TAR_NOMBRE,P.PDK_ID_PROCESOS,P.PDK_PROC_NOMBRE, ")
            strSQL.Append(" F.PDK_ID_FLUJOS, F.PDK_FLU_NOMBRE, PRO.PDK_ID_PRODUCTOS, PRO.PDK_PROD_NOMBRE, E.PDK_ID_EMPRESA, E.PDK_EMP_NOMBRE,S.PDK_PAR_SIS_PARAMETRO,A.PDK_PANT_DOCUMENTOS  ")
            strSQL.Append(" FROM PDK_PANTALLAS A INNER JOIN PDK_REL_PANTALLA_TAREA B ON A.PDK_ID_PANTALLAS=B.PDK_ID_PANTALLAS ")
            strSQL.Append(" INNER JOIN PDK_CAT_TAREAS T ON T.PDK_ID_TAREAS =B.PDK_ID_TAREAS ")
            strSQL.Append(" INNER JOIN PDK_CAT_PROCESOS P ON P.PDK_ID_PROCESOS=T.PDK_ID_PROCESOS ")
            strSQL.Append(" INNER JOIN PDK_CAT_FLUJOS F ON F.PDK_ID_FLUJOS =P.PDK_ID_FLUJOS  ")
            strSQL.Append(" INNER JOIN PDK_CAT_PRODUCTOS PRO ON PRO.PDK_ID_PRODUCTOS=F.PDK_ID_PRODUCTOS ")
            strSQL.Append(" INNER JOIN PDK_CAT_EMPRESAS E ON E.PDK_ID_EMPRESA=PRO.PDK_ID_EMPRESA ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA  S ON S.PDK_ID_PARAMETROS_SISTEMA=A.PDK_PANT_DOCUMENTOS AND S.PDK_PAR_SIS_ID_PADRE =24 ")
            strSQL.Append(" WHERE A.PDK_ID_PANTALLAS = " & intIdPantalla & "")
            ObteInfoPantalla = BD.EjecutarQuery(strSQL.ToString)
            Return ObteInfoPantalla
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_PANTALLAS")
            Throw objException
        End Try

    End Function

    Public Shared Function ObtenerPantavb(ByVal intcve As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD

        Try
            strSQL.Append(" declare @var varchar(max), @encabezado varchar(max),@nombrePant varchar(200),@nombreASPX varchar(200),@nombreTab varchar(max),@tab varchar(200)")
            strSQL.Append(" set @nombreTab=''")
            strSQL.Append(" SELECT @tab=C.PDK_SEC_NOMBRE_TABLA FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN")
            strSQL.Append(" PDK_SECCION C ON B.PDK_ID_SECCION=C.PDK_ID_SECCION  WHERE A.PDK_ID_PANTALLAS=" & intcve & " GROUP BY C.PDK_SEC_NOMBRE_TABLA")
            strSQL.Append(" SELECT  @nombreTab='DIM VAR AS STRING'+CHAR(13)")
            strSQL.Append(" SELECT   @nombreTab=@nombreTab+'DIM DATA AS NEW DataSet'+CHAR(13)")
            strSQL.Append(" SELECT   @nombreTab=@nombreTab+'Dim Fdate As Date'+CHAR(13)")
            strSQL.Append(" SELECT   @nombreTab=@nombreTab+'Try'+CHAR(13)")
            strSQL.Append(" SELECT   @nombreTab=@nombreTab+'Fdate = Format(Now(), ""yyyy-MM-dd"")'+CHAR(13)")
            strSQL.Append(" select @nombreTab=@nombreTab+'VAR=""INSERT INTO'+' '+ @tab+'('")
            strSQL.Append("  SELECT @nombreTab=@nombreTab+A.PDK_REL_PANT_OBJ_NOMBRE+','  FROM PDK_REL_PANTALLA_OBJETO A ")
            strSQL.Append("  WHERE A.PDK_ID_PANTALLAS=" & intcve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND A.PDK_REL_PANT_OBJ_NOMBRE<>REPLACE(@tab,'TAB','ID') ORDER BY A.PDK_ID_REL_PANTALLA_OBJETO ,A.PDK_REL_PANT_OBJ_ORDEN ")
            strSQL.Append("  SELECT @nombreTab=@nombreTab+'PDK_FECHA_MODIF,'+'PDK_CLAVE_USUARIO,'+'PDK_ID_SECCCERO) VALUES(' ")
            strSQL.Append("  SELECT @nombreTab =@nombreTab +'"" & '+'txt_' + A.PDK_REL_PANT_OBJ_NOMBRE+ CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO)+'.SelectedValue'+' & ""'+',' ")
            strSQL.Append(" FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO WHERE A.PDK_ID_PANTALLAS=" & intcve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND A.PDK_REL_PANT_OBJ_NOMBRE<>REPLACE(@tab,'TAB','ID') AND  B.PDK_SEC_DAT_LLAVE<>0 ORDER BY A.PDK_ID_REL_PANTALLA_OBJETO ,A.PDK_REL_PANT_OBJ_ORDEN")
            strSQL.Append(" SELECT @nombreTab =@nombreTab + CASE WHEN B.PDK_ID_TIPO_OBJETO=1 OR  B.PDK_ID_TIPO_OBJETO=3 THEN '''"" & '+'txt_' + A.PDK_REL_PANT_OBJ_NOMBRE+ CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO)+'.Text.Trim.ToUpper'+' & ""'''+','")
            strSQL.Append("  ELSE  '"" & '+'txt_' + A.PDK_REL_PANT_OBJ_NOMBRE+ CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO)+'.Text.Trim.ToUpper'+' & ""'+','")
            strSQL.Append("  END FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO WHERE A.PDK_ID_PANTALLAS=" & intcve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND A.PDK_REL_PANT_OBJ_NOMBRE<>REPLACE(@tab,'TAB','ID') AND  B.PDK_SEC_DAT_LLAVE=0 ORDER BY A.PDK_ID_REL_PANTALLA_OBJETO ,A.PDK_REL_PANT_OBJ_ORDEN")
            strSQL.Append(" SELECT @nombreTab=@nombreTab+'''"" &  Fdate & ""'',"" & Session(""IdUsua"") & "",1)""'+CHAR(13)")
            strSQL.Append(" SELECT @nombreTab =@nombreTab+' DATA= clsPantallas.InsertTabDina(VAR)' +CHAR(13)")

            strSQL.Append("  SELECT @nombreTab =@nombreTab+'Master.MensajeError(""Información almacenada exitosamente"")'+CHAR(13)")
            strSQL.Append(" SELECT @nombreTab =@nombreTab+'  Catch ex As Exception'+CHAR(13)")
            strSQL.Append(" SELECT @nombreTab =@nombreTab+'Master.MensajeError(ex.Message)'+CHAR(13)")
            strSQL.Append(" SELECT @nombreTab =@nombreTab+'End Try'+CHAR(13)")
            strSQL.AppendLine("SELECT @nombrePant=replace(PDK_PANT_LINK,'.ASPX',''),@nombreASPX=REPLACE(PDK_PANT_NOMBRE,' ','_') FROM PDK_PANTALLAS WHERE PDK_ID_PANTALLAS =" & intcve & "")
            strSQL.AppendLine("select @encabezado='Imports ProdeskNet.Configurcion'+CHAR(13)")
            strSQL.AppendLine("select @encabezado=@encabezado+'Partial Class'+' '+@nombrePant + CHAR(13)")
            strSQL.AppendLine(" select @encabezado=@encabezado+' '+'Inherits System.Web.UI.Page'+CHAR(13)")
            strSQL.AppendLine(" SELECT @encabezado=@encabezado+' '+'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load'+CHAR(13) ")
            strSQL.AppendLine("  SELECT @encabezado=@encabezado+' '+' Me.Title = ""PRODESKNET 3.0""'+CHAR(13) ")
            strSQL.AppendLine("SELECT @encabezado=@encabezado+' '+' Dim dsDataset As New DataSet'+CHAR(13)   ")
            strSQL.AppendLine("SELECT @encabezado=@encabezado+' '+' If Not IsPostBack Then'+CHAR(13)")

            strSQL.AppendLine(" DECLARE @MINIMO INT,@MAXIMO INT,@NOMCAM VARCHAR(200),@NOMTAB VARCHAR(200),@SECCINT AS INT,@Load varchar(max),@NOMTXT VARCHAR(200),@NOMBRE VARCHAR(200)")
            strSQL.AppendLine("CREATE TABLE #TEMPORAL (ID INT IDENTITY,PDK_REL_PANT_OBJ_NOMBRE VARCHAR(200),PDK_REL_PANT_OBJ_NOMBRETXT VARCHAR(200)) ")
            strSQL.AppendLine("INSERT INTO  #TEMPORAL (PDK_REL_PANT_OBJ_NOMBRE,PDK_REL_PANT_OBJ_NOMBRETXT)")
            strSQL.AppendLine("SELECT  A.PDK_REL_PANT_OBJ_NOMBRE,'txt_' + A.PDK_REL_PANT_OBJ_NOMBRE+ CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO)")
            strSQL.AppendLine("FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO WHERE A.PDK_ID_PANTALLAS=" & intcve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND A.PDK_REL_PANT_OBJ_NOMBRE<>REPLACE(@tab,'TAB','ID') AND  B.PDK_SEC_DAT_LLAVE<>0 ORDER BY A.PDK_ID_REL_PANTALLA_OBJETO ,A.PDK_REL_PANT_OBJ_ORDEN")
            strSQL.AppendLine("SELECT @MINIMO=MIN(ID) FROM  #TEMPORAL")
            strSQL.AppendLine("SELECT @MAXIMO =MAX(ID)  FROM  #TEMPORAL")
            strSQL.AppendLine("SELECT @Load=''")
            strSQL.AppendLine("WHILE @MINIMO <=@MAXIMO ")
            strSQL.AppendLine("  BEGIN")
            strSQL.AppendLine("SELECT  @Load=@Load+'dsDataset =clsPantallas.InsertTabDina(""SELECT '")
            strSQL.AppendLine("SELECT @NOMCAM=PDK_REL_PANT_OBJ_NOMBRE,@NOMTXT=PDK_REL_PANT_OBJ_NOMBRETXT FROM  #TEMPORAL WHERE ID=@MINIMO")
            strSQL.AppendLine("SELECT @NOMTAB=PDK_NOMBRE_TABLA FROM PDK_LLAVES WHERE PDK_NOMBRE_CAMPO=@NOMCAM")
            strSQL.AppendLine("SELECT @SECCINT=PDK_ID_SECCION FROM PDK_SECCION WHERE PDK_SEC_NOMBRE_TABLA=@NOMTAB")
            strSQL.AppendLine("SELECT @NOMBRE=PDK_SEC_DAT_NOMBRE  FROM PDK_SECCION_DATO WHERE PDK_ID_SECCION =@SECCINT AND PDK_SEC_DAT_NOMBRE LIKE 'NOMBRE'")
            strSQL.AppendLine("SELECT @Load=@Load+PDK_SEC_DAT_NOMBRE+',' FROM PDK_SECCION_DATO WHERE PDK_ID_SECCION =@SECCINT")
            strSQL.AppendLine("SELECT  @Load= SUBSTRING(@Load, 0, LEN(@Load)) + ' ' + 'FROM'+' '+@NOMTAB+'"")'+CHAR(13)")
            strSQL.AppendLine(" SELECT @Load=@Load+@NOMTXT+'.DataSource=dsDataset'+CHAR(13)")
            strSQL.AppendLine("SELECT @Load=@Load+@NOMTXT+'.DataTextField =""'+@NOMBRE+'""'+CHAR(13)")
            strSQL.AppendLine("SELECT @Load=@Load+@NOMTXT+'.DataValueField =""'+@NOMCAM+'""'+CHAR(13)")
            strSQL.AppendLine("SELECT @Load=@Load+@NOMTXT+'.DataBind()'+CHAR(13)")
            strSQL.AppendLine("SET @MINIMO =@MINIMO +1")
            strSQL.AppendLine("END  ")
            strSQL.AppendLine("DROP TABLE #TEMPORAL")
            strSQL.AppendLine("SELECT @encabezado=@encabezado+' '+@Load+CHAR(13)")
            strSQL.AppendLine("SELECT @encabezado=@encabezado+' '+'End If'+CHAR(13)")
            strSQL.AppendLine("  SELECT @encabezado=@encabezado+'End Sub'+CHAR(13)")
            strSQL.AppendLine(" SELECT  @encabezado=@encabezado+'Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click' +CHAR(13)")
            strSQL.AppendLine(" SELECT  @encabezado=@encabezado+'Response.Redirect(""./consultaPantalla.aspx"")'+CHAR(13) ")
            strSQL.AppendLine(" SELECT @encabezado=@encabezado+'End Sub'+CHAR(13)")
            strSQL.AppendLine("SELECT @encabezado=@encabezado+' Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click'+CHAR(13)")
            strSQL.AppendLine("    SELECT @encabezado=@encabezado+ @nombreTab +CHAR(13) ")
            strSQL.AppendLine(" SELECT @encabezado=@encabezado+ 'End Sub'+CHAR(13)")
            strSQL.AppendLine(" select @encabezado=@encabezado+''+' End Class'+ CHAR(13)")
            strSQL.AppendLine("select @encabezado as rutas_vb")

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al obtener los objetos")
            Throw objException
        End Try


    End Function
    Public Shared Function ObtenerPantDese(ByVal incve As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try

            strSQL.AppendLine("declare @des varchar(max),@nombrePant varchar(200) ,@cerra varchar(max),@tab varchar(200) ")
            strSQL.AppendLine("set @des=''")
            strSQL.AppendLine("SELECT @tab=C.PDK_SEC_NOMBRE_TABLA FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN")
            strSQL.AppendLine("PDK_SECCION C ON B.PDK_ID_SECCION=C.PDK_ID_SECCION  WHERE A.PDK_ID_PANTALLAS=" & incve & " GROUP BY C.PDK_SEC_NOMBRE_TABLA")
            strSQL.AppendLine("SELECT @nombrePant=replace(PDK_PANT_LINK,'.ASPX','') FROM PDK_PANTALLAS WHERE PDK_ID_PANTALLAS =" & incve & "")
            strSQL.AppendLine("select @des='Option Strict On'+CHAR(13)")
            strSQL.AppendLine("select  @des=@des+'Option Explicit On'+CHAR(13) ")
            strSQL.AppendLine("select @des=@des+'Partial Public Class '+' '+@nombrePant+CHAR(13)")
            strSQL.AppendLine("SELECT @des =@des + CASE WHEN   A.PDK_REL_PANT_OBJ_NOMBRE=REPLACE(@tab,'TAB','ID')   THEN 'Protected WithEvents txt_'+A.PDK_REL_PANT_OBJ_NOMBRE+ CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO) +' '+' As Global.System.Web.UI.WebControls.TextBox'+CHAR(13)")
            strSQL.AppendLine("ELSE 'Protected WithEvents txt_'+A.PDK_REL_PANT_OBJ_NOMBRE+ CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO) +' '+' As Global.System.Web.UI.WebControls.DropDownList'+CHAR(13)")
            strSQL.AppendLine("END FROM PDK_REL_PANTALLA_OBJETO A   INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO")
            strSQL.AppendLine("WHERE A.PDK_ID_PANTALLAS=" & incve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND B.PDK_SEC_DAT_LLAVE<>0  ORDER BY A.PDK_ID_REL_PANTALLA_OBJETO ,A.PDK_REL_PANT_OBJ_ORDEN")
            strSQL.AppendLine("SELECT @des =@des + 'Protected WithEvents txt_'+A.PDK_REL_PANT_OBJ_NOMBRE+ CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO) +' '+' As Global.System.Web.UI.WebControls.TextBox'+CHAR(13)")
            strSQL.AppendLine("FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO ")
            strSQL.AppendLine("WHERE A.PDK_ID_PANTALLAS=" & incve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND B.PDK_SEC_DAT_LLAVE=0 ORDER BY A.PDK_ID_REL_PANTALLA_OBJETO ,A.PDK_REL_PANT_OBJ_ORDEN")
            strSQL.AppendLine("select @cerra ='Protected WithEvents btnRegresar As Global.System.Web.UI.WebControls.Button'+CHAR(13)")
            strSQL.AppendLine("select @cerra =@cerra+'Protected WithEvents btnGuardar As Global.System.Web.UI.WebControls.Button'+CHAR(13)")
            strSQL.AppendLine("select @cerra=@cerra+'Public Shadows ReadOnly Property Master() As ProdeskNet.Home'+CHAR(13)")
            strSQL.AppendLine("select @cerra=@cerra+' Get'+CHAR(13)")
            strSQL.AppendLine("select @cerra=@cerra+'Return CType(MyBase.Master, ProdeskNet.Home)'+CHAR (13)")
            strSQL.AppendLine("select @cerra=@cerra+'End Get'+ CHAR(13)")
            strSQL.AppendLine("select @cerra=@cerra+'End Property'+CHAR(13)")
            strSQL.AppendLine("select @cerra=@cerra+'End Class'+CHAR(13)")
            strSQL.AppendLine("select @des+@cerra as rutas_des")

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al obtener los objetos")
            Throw objException
        End Try

    End Function
    Public Shared Function ObtenerPantaspx(ByVal intcve As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            'strSQL.AppendLine(" declare @var varchar(max), @encabezado varchar(max),@nombrePant varchar(200),@nombreASPX varchar(200),@nombreCla varchar(200),@tab varchar(200)")
            'strSQL.AppendLine(" SELECT @tab=C.PDK_SEC_NOMBRE_TABLA FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN")
            'strSQL.AppendLine("  PDK_SECCION C ON B.PDK_ID_SECCION=C.PDK_ID_SECCION  WHERE A.PDK_ID_PANTALLAS=" & intcve & " GROUP BY C.PDK_SEC_NOMBRE_TABLA")
            'strSQL.AppendLine(" SELECT @nombrePant=PDK_PANT_LINK,@nombreASPX=REPLACE(PDK_PANT_NOMBRE,' ','_'),@nombreCla=replace(PDK_PANT_LINK,'.ASPX','') FROM PDK_PANTALLAS WHERE PDK_ID_PANTALLAS =" & intcve & "")
            'strSQL.AppendLine(" set @encabezado = '<%@ Page Title=""'+@nombreASPX+'"" Language=""vb"" AutoEventWireup=""false""  MasterPageFile=""~/aspx/Home.Master"" CodeBehind=""'+ @nombrePant+'.vb'+'"" Inherits=""ProdeskNet.'+ @nombreCla +'""  %>")
            'strSQL.AppendLine(" <%@ Register Assembly=""AjaxControlToolkit"" Namespace =""AjaxControlToolkit"" TagPrefix =""cc1"" %> ")
            'strSQL.AppendLine("<%@ MasterType VirtualPath=""~/aspx/Home.Master"" %>")
            'strSQL.AppendLine(" <asp:Content ID=""Content1"" ContentPlaceHolderID =""cphPantallas"" runat=""server"" > ")
            'strSQL.AppendLine("<div class=""divFiltrosConsul"">")
            'strSQL.AppendLine("<table>")
            'strSQL.AppendLine("<tr><td class=""tituloConsul"">'+@nombreASPX+'</td>")
            'strSQL.AppendLine("</tr></table></div>")
            'strSQL.AppendLine("<div class=""divAdminCatCuerpo"">'")
            'strSQL.AppendLine(" set @var = ''")
            'strSQL.AppendLine(" SELECT @var = @var + CASE WHEN   A.PDK_REL_PANT_OBJ_NOMBRE=REPLACE(@tab,'TAB','ID')   THEN     '<tr> <td class=""campos"">'+A.PDK_REL_PANT_OBJ_NOMBRE+':</td> <td><asp:TextBox ID = ""txt_' + A.PDK_REL_PANT_OBJ_NOMBRE + CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO) + '"" runat = ""server"" SkinID=""txtNumerosGde"">")
            'strSQL.AppendLine(" </asp:TextBox></td></tr>' + char(10) ELSE '<tr> <td class=""campos"">'+A.PDK_REL_PANT_OBJ_NOMBRE+':</td> <td><asp:DropDownList ID = ""txt_' + A.PDK_REL_PANT_OBJ_NOMBRE + CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO) + '"" runat = ""server""  CssClass=""Text"" SkinID=""cmbGeneral"" >")
            'strSQL.AppendLine(" </asp:DropDownList></td></tr>' + char(10)  END FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO")
            'strSQL.AppendLine(" WHERE a.PDK_ID_PANTALLAS=" & intcve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND B.PDK_SEC_DAT_LLAVE<>0   ORDER BY A.PDK_ID_REL_PANTALLA_OBJETO ,A.PDK_REL_PANT_OBJ_ORDEN ")
            'strSQL.AppendLine(" SELECT @var = @var + CASE WHEN B.PDK_ID_TIPO_OBJETO=2   THEN '<tr> <td class=""campos"">'+A.PDK_REL_PANT_OBJ_NOMBRE+':</td> <td><asp:TextBox ID = ""txt_' + A.PDK_REL_PANT_OBJ_NOMBRE + CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO) + '"" runat = ""server"" SkinID=""txtNumerosGde"">")
            'strSQL.AppendLine(" </asp:TextBox></td></tr>' + char(10) ELSE '<tr> <td class=""campos"">'+A.PDK_REL_PANT_OBJ_NOMBRE+':</td> <td><asp:TextBox ID = ""txt_' + A.PDK_REL_PANT_OBJ_NOMBRE + CONVERT(VARCHAR(50),A.PDK_ID_SECCION_DATO) + '"" runat = ""server"" >")
            'strSQL.AppendLine(" </asp:TextBox></td></tr>' + char(10) END FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO ")
            'strSQL.AppendLine(" WHERE a.PDK_ID_PANTALLAS=" & intcve & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0 AND B.PDK_SEC_DAT_LLAVE=0 ORDER BY A.PDK_ID_REL_PANTALLA_OBJETO ,A.PDK_REL_PANT_OBJ_ORDEN ")
            'strSQL.AppendLine("select @encabezado +'<table>' + @var +'</table>'+'</div>'+'<div class=""divAdminCatPie"">'+'<table>'+'<tr>'+'<td align=""right"" valign=""middle"">'+' <asp:Button runat=""server"" ID=""btnRegresar"" text=""Regresar"" SkinID=""btnGeneral"" />'+'<asp:Button runat=""server"" ID=""btnGuardar"" text=""Guardar"" SkinID=""btnGeneral"" />'+'</td>'+'</tr>'+'</table>'+'</div>'+'</asp:Content>' as ruta")

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al obtener los objetos")
            Throw objException
        End Try

    End Function
    Public Shared Function ObtenTodos() As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_PANTALLAS,")
            strSQL.Append(" A.PDK_PANT_NOMBRE,")
            strSQL.Append(" A.PDK_PANT_ORDEN,")
            strSQL.Append(" A.PDK_PANT_STATUS,")
            strSQL.Append(" A.PDK_PANT_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_PANT_LINK,")
            strSQL.Append(" A.PDK_PANT_MOSTRAR")
            strSQL.Append(" FROM PDK_PANTALLAS A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_PANTALLAS")
            Throw objException
        End Try
    End Function
    Public Shared Function InsertTabDina(ByVal str As String) As DataSet
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            InsertTabDina = BD.EjecutarQuery(str.ToString)
            Return InsertTabDina
        Catch ex As Exception
            objException = New Exception("Error al insertar Datos")
            Throw objException
        End Try

    End Function
    Public Sub Guarda()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Try
            If intPDK_ID_PANTALLAS = 0 Then
                Me.intPDK_ID_PANTALLAS = BD.ObtenConsecutivo("PDK_ID_PANTALLAS", "PDK_PANTALLAS", Nothing)
                strSql = "INSERT INTO PDK_PANTALLAS " & _
                                "(" & _
"PDK_ID_PANTALLAS,PDK_PANT_NOMBRE,PDK_PANT_ORDEN,PDK_PANT_STATUS,PDK_PANT_MODIF,PDK_CLAVE_USUARIO,PDK_PANT_LINK,PDK_PANT_MOSTRAR,PDK_PANT_DOCUMENTOS)" & _
" VALUES ( " & intPDK_ID_PANTALLAS & ", '" & strPDK_PANT_NOMBRE & "', " & intPDK_PANT_ORDEN & ",  " & intPDK_PANT_STATUS & ", '" & strPDK_PANT_MODIF & "','" & strPDK_CLAVE_USUARIO & "','" & strPDK_PANT_LINK & "'," & intPDK_PANT_MOSTRAR & ", " & intPDK_PANT_DOCUMENTOS & " " & _
")   INSERT INTO PDK_REL_PANTALLA_TAREA ( " & _
"PDK_REL_PANT_TAR_ORDEN,PDK_REL_PANT_TAR_STATUS,PDK_REL_PANT_TAR_MODIF,PDK_CLAVE_USUARIO,PDK_ID_TAREAS,PDK_ID_PANTALLAS)" & _
 "VALUES (" & intPDK_PANT_ORDEN & "," & intPDK_PANT_STATUS & ",'" & strPDK_PANT_MODIF & "'," & strPDK_CLAVE_USUARIO & "," & intPDK_ID_TAREAS & "," & intPDK_ID_PANTALLAS & ")"
                'Else
                '    ActualizaRegistro()
                '    Exit Sub
            End If
            BD.EjecutarQuery(strSql)
            PDK_ID_PANTALLAS = Me.intPDK_ID_PANTALLAS


        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_PANTALLAS ")
        End Try
    End Sub
    Public Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_PANTALLAS " & _
               "SET " & _
                " PDK_PANT_ORDEN = " & intPDK_PANT_ORDEN & ", " & _
                " PDK_PANT_STATUS = " & intPDK_PANT_STATUS & ", " & _
                " PDK_PANT_MODIF = '" & strPDK_PANT_MODIF & "'," & _
                " PDK_PANT_MOSTRAR= " & intPDK_PANT_MOSTRAR & "," & _
                " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "'," & _
                " PDK_PANT_NOMBRE= '" & strPDK_PANT_NOMBRE & "'," & _
                " PDK_PANT_DOCUMENTOS= " & intPDK_PANT_DOCUMENTOS & " " & _
                "  WHERE PDK_ID_PANTALLAS=  " & intPDK_ID_PANTALLAS & " " & _
                "  UPDATE PDK_REL_PANTALLA_TAREA  " & _
                "  SET PDK_ID_TAREAS=" & intPDK_ID_TAREAS & " " & _
                "  WHERE PDK_ID_PANTALLAS= " & intPDK_ID_PANTALLAS & " " & _
                "   UPDATE PDK_REL_PANTALLA_OBJETO  SET PDK_REL_PANT_OBJ_STATUS=" & intPDK_PANT_STATUS & " WHERE PDK_ID_PANTALLAS=" & intPDK_ID_PANTALLAS & "  " & _
                "   AND PDK_ID_SECCION_DATO IN(SELECT A.PDK_ID_SECCION_DATO  FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN  PDK_SECCION_DATO B  ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN PDK_SECCION C ON B.PDK_ID_SECCION =C.PDK_ID_SECCION  WHERE C.PDK_SEC_CREACION=2) "

            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_PANTALLAS")
        End Try
    End Sub
    Public Shared Function ObtenerValidaTarea(ByVal intTarea As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.AppendLine("SELECT A.PDK_ID_PANTALLAS,B.PDK_ID_TAREAS, A.PDK_PANT_NOMBRE FROM PDK_PANTALLAS A INNER JOIN PDK_REL_PANTALLA_TAREA B ON A.PDK_ID_PANTALLAS=B.PDK_ID_PANTALLAS ")
            strSQL.AppendLine("WHERE B.PDK_ID_TAREAS = " & intTarea & " AND A.PDK_PANT_STATUS=2")
            ObtenerValidaTarea = BD.EjecutarQuery(strSQL.ToString)
            Return ObtenerValidaTarea


        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try

    End Function

    Public Shared Function ObtenerValidacion(ByVal intTarea As Integer, ByVal intPantalla As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.AppendLine("SELECT A.PDK_ID_PANTALLAS,B.PDK_ID_TAREAS FROM PDK_PANTALLAS A INNER JOIN PDK_REL_PANTALLA_TAREA B ON A.PDK_ID_PANTALLAS=B.PDK_ID_PANTALLAS ")
            strSQL.AppendLine("WHERE B.PDK_ID_TAREAS = " & intTarea & " AND A.PDK_ID_PANTALLAS <>" & intPantalla & "")
            ObtenerValidacion = BD.EjecutarQuery(strSQL.ToString)
            Return ObtenerValidacion


        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try

    End Function
    Public Shared Function ObtenerlosTabs(ByVal idSoli As Int64, ByVal intBandera As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD

        'RQCONYFOR-07: AVH:
        Dim Conex As String = ConfigurationManager.AppSettings("ConexProcotiza").ToString
        Dim Datos As String() = Split(Conex, ";")
        Dim NombreBD As String = Replace(Datos(1), "Initial Catalog=", "")

        Try
            If intBandera = 1 Then
                strSQL.AppendLine("DECLARE @COTIZACION AS INT;SELECT @COTIZACION=isnull(ID_COTIZACION, 0) FROM PDK_TAB_SECCION_CERO  WHERE PDK_ID_SECCCERO =" & idSoli & ";")
                strSQL.AppendLine("SELECT @cotizacion as Cotizacion")
                strSQL.AppendLine("SELECT M.NO_PAGO [No.],")
                strSQL.AppendLine("m.INSOLUTO_PROD [Saldo Capital],")
                strSQL.AppendLine("m.CAPITAL [Amortizacion Capital],")
                strSQL.AppendLine("m.INTERES_PROD Interes,")
                strSQL.AppendLine("m.IVA_PROD IVA,")
                strSQL.AppendLine("M.PAGO_PROD + m.IVA_PROD [Pago Mensual],")
                strSQL.AppendLine("m.SEG_PROD [Pago Mensual Seguro],")
                strSQL.AppendLine("M.PAGO_PROD  + m.IVA_PROD + m.SEG_PROD [Pago Mensual Total]")
                strSQL.AppendLine("FROM " + NombreBD + "..TABLAAMORTIZA M ")
                strSQL.AppendLine("inner join " + NombreBD + "..COTIZACIONES RPCC on m.ID_COTIZACION = RPCC.ID_COTIZACION")
                strSQL.AppendLine("inner join PDK_TAB_SECCION_CERO PTSC on RPCC.ID_COTIZACION = PTSC.ID_COTIZACION")
                strSQL.AppendLine("WHERE PDK_ID_SECCCERO = " & idSoli)
                strSQL.AppendLine("AND M.TIPO_TABLA=1 ")
                strSQL.AppendLine("ORDER BY M.NO_PAGO;")
            ElseIf intBandera = 2 Then
                strSQL.AppendLine("DECLARE @ID AS INT;")
                strSQL.AppendLine("SELECT @ID=MAX(PDK_ID_DICTAMEN_FINAL)  FROM PDK_TAB_DICTAMEN_FINAL	WHERE PDK_ID_SECCCERO=" & idSoli & "")
                strSQL.AppendLine("SELECT * FROM PDK_TAB_DICTAMEN_FINAL  WHERE PDK_ID_DICTAMEN_FINAL =@ID AND PDK_ID_SECCCERO=" & idSoli & " ")
            ElseIf intBandera = 3 Then
                strSQL.AppendLine("DECLARE @PANTALLA INT,@TABLA VARCHAR(200),@CADENA VARCHAR(MAX)='',@MINI INT ,@MAXI INT,@SECCION INT,@FOLIO INT,@SQLTAB VARCHAR(MAX)='',@CADENA1 VARCHAR(MAX)='',@TAB1 VARCHAR(200)=''")
                strSQL.AppendLine("CREATE TABLE #TEMPORAL (ID INT IDENTITY , PDK_SEC_NOMBRE_TABLA VARCHAR(200),PDK_ID_SECCION INT) ")
                strSQL.AppendLine("SELECT @TAB1=PDK_SEC_NOMBRE_TABLA FROM PDK_SECCION WHERE PDK_ID_SECCION=1")
                strSQL.AppendLine("SELECT @PANTALLA=A.PDK_ID_PANTALLAS  FROM PDK_PANTALLAS A INNER JOIN PDK_REL_PANTALLA_TAREA B ON A.PDK_ID_PANTALLAS=B.PDK_ID_PANTALLAS ")
                strSQL.AppendLine("INNER JOIN PDK_CAT_TAREAS C ON C.PDK_ID_TAREAS =B.PDK_ID_TAREAS  WHERE C.PDK_TAR_NOMBRE ='SOLICITUD'")
                strSQL.AppendLine("SELECT @FOLIO=" & idSoli & "")
                strSQL.AppendLine("INSERT INTO  #TEMPORAL (PDK_SEC_NOMBRE_TABLA,PDK_ID_SECCION)")
                strSQL.AppendLine("SELECT C.PDK_SEC_NOMBRE_TABLA,C.PDK_ID_SECCION FROM PDK_REL_PANTALLA_OBJETO A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO=B.PDK_ID_SECCION_DATO INNER JOIN")
                strSQL.AppendLine("PDK_SECCION C ON B.PDK_ID_SECCION=C.PDK_ID_SECCION  WHERE A.PDK_ID_PANTALLAS=@PANTALLA AND A.PDK_REL_PANT_OBJ_ORDEN<>0 GROUP BY C.PDK_ID_SECCION,C.PDK_SEC_NOMBRE_TABLA")
                strSQL.AppendLine("SELECT @CADENA1=@CADENA1+'DECLARE @FECHA DATE  SELECT @FECHA=LTRIM(ANO_NACIMIENTO)+ ''-'' +dbo.fnConvIntVarcharDosCar(MES_NACIMIENTO)+''-''+dbo.fnConvIntVarcharDosCar(DIA_NACIMIENTO) FROM '+ @TAB1+' WHERE PDK_ID_SECCCERO='+LTRIM(@FOLIO) ")
                strSQL.AppendLine("SELECT @CADENA1=@CADENA1+' UPDATE PDK_TAB_DATOS_PERSONALES SET  FECHA_NACIMIENTO=@FECHA  WHERE PDK_ID_SECCCERO='+LTRIM(@FOLIO)")
                strSQL.AppendLine("SELECT @CADENA1=@CADENA1+' UPDATE PDK_TAB_COACREDITADO_CASO SET FECHA_NACIMIENTO= case when A.ANO_NACIMIENTO != 0 and A.MES_NACIMIENTO != 0 and DIA_NACIMIENTO != 0 then A.ANO_NACIMIENTO +''-''+ dbo.fnConvIntVarcharDosCar(A.MES_NACIMIENTO) +''-''+ dbo.fnConvIntVarcharDosCar(DIA_NACIMIENTO) else null end FROM PDK_TAB_COACREDITADO_CASO A  WHERE PDK_ID_SECCCERO = '+LTRIM(@FOLIO)")
                strSQL.AppendLine("EXEC(@CADENA1)")
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
                strSQL.AppendLine("exec (@SQLTAB)")
                strSQL.AppendLine("SET @MINI=@MINI+1")
                strSQL.AppendLine("End")
                strSQL.AppendLine("DROP TABLE #TEMPORAL")
            ElseIf intBandera = 4 Then
                strSQL.AppendLine("declare @solicitud int = " & idSoli & "; SELECT isnull(max(PDK_ID_BURO), 0) idBuro, 'Cliente' as Persona FROM PDK_BURO_REPORTE WHERE PDK_ID_SECCCERO=@solicitud and PDK_BUR_CRED_PERSONA = 0 union  SELECT isnull(max(PDK_ID_BURO), 0), 'Coacreditado' FROM PDK_BURO_REPORTE WHERE PDK_ID_SECCCERO=@solicitud and PDK_BUR_CRED_PERSONA != 0")
            ElseIf intBandera = 5 Then
                ' strSQL.AppendLine("SELECT A.PDK_ID_DOCUMENTOS,B.PDK_DOC_NOMBRE,A.PDK_LNK_IMAGEN AS URL,A.PDK_LNK_IMAGEN    FROM PDK_REL_PAN_DOC_SOL A INNER JOIN PDK_CAT_DOCUMENTOS B ON A.PDK_ID_DOCUMENTOS=B.PDK_ID_DOCUMENTOS INNER JOIN PDK_REL_PAN_DOC D ON A.PDK_ID_REL_PAN_DOC=D.PDK_ID_REL_PAN_DOC WHERE A.PDK_ID_SECCCERO=" & idSoli & " AND A.PDK_ST_VALIDADO=1 AND D.PDK_REL_ACT_STATUS=1")
                strSQL.AppendLine("SELECT A.PDK_ID_DOCUMENTOS,B.PDK_DOC_NOMBRE,A.PDK_LNK_IMAGEN AS URL,A.PDK_LNK_IMAGEN,CASE WHEN A.PDK_ST_ENTREGADO=1 THEN 'SI' ELSE 'NO' END AS PDK_ST_ENTREGADO ,CASE WHEN A.PDK_ST_VALIDADO=1 THEN 'SI' ELSE 'NO' END AS PDK_ST_VALIDADO , CASE WHEN ISNULL(A.PDK_ST_RECHAZADO,0)=1 THEN 'SI' ELSE 'NO' END AS PDK_ST_RECHAZADO")
                strSQL.AppendLine(" FROM PDK_REL_PAN_DOC_SOL A INNER JOIN PDK_CAT_DOCUMENTOS B ON A.PDK_ID_DOCUMENTOS=B.PDK_ID_DOCUMENTOS INNER JOIN PDK_REL_PAN_DOC D ON A.PDK_ID_REL_PAN_DOC=D.PDK_ID_REL_PAN_DOC ")
                strSQL.AppendLine("WHERE A.PDK_ID_SECCCERO=" & idSoli & "  AND D.PDK_REL_ACT_STATUS=1")
            ElseIf intBandera = 6 Then
                strSQL.AppendLine("SELECT A.PDK_ID_SECCCERO,CONVERT(VARCHAR(20),A.FECHA,103)AS FECHA,S.NOMBRE1+' '+S.NOMBRE2+' '+APELLIDO_PATERNO+' '+S.APELLIDO_MATERNO as NOMBRECOM FROM PDK_TAB_SECCION_CERO A INNER JOIN PDK_TAB_SOLICITANTE S ON A.PDK_ID_SECCCERO=S.PDK_ID_SECCCERO  WHERE A.PDK_ID_SECCCERO =" & idSoli & "")
            ElseIf intBandera = 7 Then
                strSQL.AppendLine("SELECT * FROM PDK_TAB_SECCION_CERO WHERE PDK_ID_SECCCERO = " & idSoli & "")

            End If


            ObtenerlosTabs = BD.EjecutarQuery(strSQL.ToString)
            Return ObtenerlosTabs
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de SOLICITUD")
            Throw objException
        End Try
    End Function
    Public Shared Function Obtenerlistaobjeto(ByVal cvePantalla As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.AppendLine("SELECT A.PDK_ID_REL_PANTALLA_OBJETO,A.PDK_REL_PANT_OBJ_NOMBRE,A.PDK_REL_PANT_OBJ_ORDEN,A.PDK_ID_PANTALLAS,B.PDK_ID_SECCION_DATO  FROM PDK_REL_PANTALLA_OBJETO A")
            strSQL.AppendLine("INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO =B.PDK_ID_SECCION_DATO")
            strSQL.AppendLine("WHERE A.PDK_ID_PANTALLAS=" & cvePantalla & " AND A.PDK_REL_PANT_OBJ_ORDEN<>0")
            Obtenerlistaobjeto = BD.EjecutarQuery(strSQL.ToString)
            Return Obtenerlistaobjeto


        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REL_PANTALLA_OBJETO")
            Throw objException
        End Try
    End Function
#End Region
    '-------------------------- FIN PDK_PANTALLAS-------------------------- 


End Class
