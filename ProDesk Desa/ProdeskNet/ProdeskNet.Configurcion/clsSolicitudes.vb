'BBV-P-423: RQSOL-03: AVH: 10/11/2016 se crea funcion ConsultaSolicitud
'BBV-P-423: RQSOL-04: AVH: 06/12/2016 se crea funcion ManejaTarea
'BBVA-P-423:RQ03 AVH: 15/12/2016 Se agregan opciones para Cuestionario de Salud
'BBV-P-423 RQADM-36: AVH: 31/01/2017 Se agrega opcion a ConsultaCuestionarios
'BUG-PD-09: AVH:14/02/2017 SE Agrega opcion 4 ManejaTarea
'BUG-PD-14 JBB:06/02/2017 Se agrega formato a la fecha como se necesita en el store.
'BUG-PD-19 JBB:10/03/2017 Se agrega  parametro para el store de sp_val_negocio y asi seguir con el flujo y modificaciones.
'BBV-P-423: RQADM-14: AVH: 17/03/2017 TAREA DE AVH 80 Cálculo de Ingresos
'BBV-P-423 RQADM-22: JRHM: 28/03/2017: SE MODIFICA MANEJATAREAS OPCION 6 PARA ACEPTAR UN NUEVO PARAMETRO QUE ES ESTATUS DE CREDITO
'BUG-PD-40: erodriguez: 04/05/2017: Modificacion a la clase para leer y actualizar el nuevo campo INVALIDO_EMISION
'BBV-P-423:RQAMD-26 JBEJAR 11/05/2017 SE PROGRAMA  CEDULA ANTIFRAUDE.
'BBVA-P-423 RQADM-28 17/05/2017 CGARCIA agrega nuevo caso 6 para el módulo de consulta 
'BBVA-BUG-PD-112: 28/06/2017: CGARCIA: SE AGREGARON DOS VARIABLES DE FILTRO PARA EL CASO 6
'RQ-INB219 JBEJAR 28/07/2017 Requerimiento mostrar las solicitudes que fueron forzadas y en que forzaje fueron se agrega nueva propiedad para tener el control de las solicitudes que fueron forzadas.
'BUG-PD-179:JBEJAR:10/08/2017 Se agregan variables,propiedades y nuevo metodo para guardar en el LOG de forzajes.  
'BUG-PD-365 JMENDIETA 22/02/2018 Se agrega opcion 8 en el metodo ConsultaSolicitud
Imports System.Text
Imports System.Data
Imports ProdeskNet.BD

Public Class clsSolicitudes
    '-------------------------- INICIO PDK_OPE_SOLICITUD-------------------------- 
#Region "Variables"

    Private intPDK_ID_OPE_SOLICITUD As Integer = 0
    Private intPDK_ID_SOLICITUD As Integer = 0
    Private intPDK_ID_CAT_RESULTADO As Integer = 0
    Private intPDK_ID_CAT_RECHAZOS As Integer = 0
    Private intPDK_ID_TAREAS As Integer = 0
    Private intPDK_OPE_STATUS As Integer = 0
    Private strPDK_OPE_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_OPE_USU_ASIGNADO As Integer = 0

    Private strNombre As String = ""
    Private strFechaIni As String = ""
    Private strFechaFin As String = ""
    Private strRFC As String = String.Empty

    Private intPDK_ID_PANTALLA As Integer = 0

    Private strErrSolicitud As String = ""
    Private strComentario As String = ""
    Private strNombreUsuario As String = ""
    Private strContraseña As String = ""

    Private intBOTON As Integer = 0
    Private strID_Pregunta As String = ""
    Private strPregunta As String = ""
    Private intSi As Integer = 0
    Private intNo As Integer = 0
    Private strValor As String = ""
    Private intRes As Integer = 0
    Private dblPeso As Double = 0
    Private dblEstatura As Double = 0
    Private intINVALIDO_EMISION As Integer = 0
    Private intStatus As Integer
    Private intStatus_cred As Integer
    Private strmensaje As String = String.Empty
    Private strAsesor As String = String.Empty
    Private IntId_Estatus As Integer

    Private intDesdeSol As Integer = 0
    Private intHastaSol As Integer = 0
    Private _Forzaje As Integer
    'BUG-PD-179 JBEJAR 
    Private _Tipo_Forzaje As String = String.Empty
    Private _Tarea_Anterior As Integer = 0
    Private _Tarea_Siguiente As Integer = 0
    Private _Usuario_Forza As Integer = 0
    Private _strError As String = String.Empty
    '-----------------------------------------------



#End Region
#Region "Propiedades"

    Public Property PDK_ID_OPE_SOLICITUD() As Integer
        Get
            Return intPDK_ID_OPE_SOLICITUD
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_OPE_SOLICITUD = value
        End Set
    End Property

    Public Property PDK_ID_SOLICITUD() As Integer
        Get
            Return intPDK_ID_SOLICITUD
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_SOLICITUD = value
        End Set
    End Property
    Public Property PDK_ID_CAT_RESULTADO() As Integer
        Get
            Return intPDK_ID_CAT_RESULTADO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_CAT_RESULTADO = value
        End Set
    End Property
    Public Property PDK_ID_CAT_RECHAZOS() As Integer
        Get
            Return intPDK_ID_CAT_RECHAZOS
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_CAT_RECHAZOS = value
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
    Public Property PDK_OPE_STATUS() As Integer
        Get
            Return intPDK_OPE_STATUS
        End Get
        Set(ByVal value As Integer)
            intPDK_OPE_STATUS = value
        End Set
    End Property
    Public Property PDK_OPE_MODIF() As String
        Get
            Return strPDK_OPE_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_OPE_MODIF = value
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
    Public Property PDK_OPE_USU_ASIGNADO() As Integer
        Get
            Return intPDK_OPE_USU_ASIGNADO
        End Get
        Set(ByVal value As Integer)
            intPDK_OPE_USU_ASIGNADO = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return strNombre
        End Get
        Set(ByVal value As String)
            strNombre = value
        End Set
    End Property
    Public Property FechaIni() As String
        Get
            Return strFechaIni
        End Get
        Set(ByVal value As String)
            If value = "" Then
                strFechaIni = value
            Else

                strFechaIni = Format(CDate(value), "yyyyMMdd")
            End If


        End Set
    End Property
    Public Property FechaFin() As String
        Get
            Return strFechaFin
        End Get
        Set(ByVal value As String)
            If value = "" Then
                strFechaFin = value
            Else

                strFechaFin = Format(CDate(value), "yyyyMMdd")
            End If
        End Set
    End Property
    Public Property RFC() As String
        Get
            Return strRFC
        End Get
        Set(ByVal value As String)
            strRFC = value
        End Set
    End Property
    Public Property PDK_ID_PANTALLA() As Integer
        Get
            Return intPDK_ID_PANTALLA
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_PANTALLA = value
        End Set
    End Property
    Public Property ERROR_SOL() As String
        Get
            Return strErrSolicitud
        End Get
        Set(ByVal value As String)
            strErrSolicitud = value
        End Set
    End Property
    Public Property Comentario() As String
        Get
            Return strComentario
        End Get
        Set(ByVal value As String)
            strComentario = value
        End Set
    End Property
    Public Property NombreUsu() As String
        Get
            Return strNombreUsuario
        End Get
        Set(ByVal value As String)
            strNombreUsuario = value
        End Set
    End Property
    Public Property Contraseña() As String
        Get
            Return strContraseña
        End Get
        Set(ByVal value As String)
            strContraseña = value
        End Set
    End Property
    Public Property BOTON() As Integer
        Get
            Return intBOTON
        End Get
        Set(ByVal value As Integer)
            intBOTON = value
        End Set
    End Property
    Public Property ID_PREGUNTA() As String
        Get
            Return strID_Pregunta
        End Get
        Set(ByVal value As String)
            strID_Pregunta = value
        End Set
    End Property
    Public Property PREGUNTA() As String
        Get
            Return strPregunta
        End Get
        Set(ByVal value As String)
            strPregunta = value
        End Set
    End Property
    Public Property SI() As Integer
        Get
            Return intSi
        End Get
        Set(ByVal value As Integer)
            intSi = value
        End Set
    End Property
    Public Property NO() As Integer
        Get
            Return intNo
        End Get
        Set(ByVal value As Integer)
            intNo = value
        End Set
    End Property
    Public Property VALOR() As String
        Get
            Return strValor
        End Get
        Set(ByVal value As String)
            strValor = value
        End Set
    End Property
    Public Property RES() As Integer
        Get
            Return intRes
        End Get
        Set(ByVal value As Integer)
            intRes = value
        End Set
    End Property
    Public Property PESO() As Double
        Get
            Return dblPeso
        End Get
        Set(ByVal value As Double)
            dblPeso = value
        End Set
    End Property
    Public Property ESTATURA() As Double
        Get
            Return dblEstatura
        End Get
        Set(ByVal value As Double)
            dblEstatura = value
        End Set
    End Property
    Public Property INVALIDO_EMISION() As Integer
        Get
            Return intINVALIDO_EMISION
        End Get
        Set(ByVal value As Integer)
            intINVALIDO_EMISION = value
        End Set
    End Property
    Public Property Estatus() As Integer
        Get
            Return intStatus
        End Get
        Set(ByVal value As Integer)
            intStatus = value
        End Set
    End Property
    Public Property Estatus_Cred As Integer
        Get
            Return intStatus_cred
        End Get
        Set(ByVal value As Integer)
            intStatus_cred = value
        End Set
    End Property
    Public Property MENSAJE() As String
        Get
            Return strmensaje
        End Get
        Set(ByVal value As String)
            strmensaje = value
        End Set
    End Property

    Public Property ASESOR() As String
        Get
            Return strAsesor
        End Get
        Set(value As String)
            strAsesor = value
        End Set
    End Property

    Public Property ID_ESTATUS() As Integer
        Get
            Return IntId_Estatus
        End Get
        Set(value As Integer)
            IntId_Estatus = value
        End Set
    End Property

    Public Property INT_DESDE_SOL As Integer
        Get
            Return intDesdeSol
        End Get
        Set(value As Integer)
            intDesdeSol = value
        End Set
    End Property

    Public Property INT_HASTA_SOL As Integer
        Get
            Return intHastaSol
        End Get
        Set(value As Integer)
            intHastaSol = value
        End Set
    End Property


    Public Property Forzaje As String
        Get
            Return _Forzaje
        End Get
        Set(ByVal value As String)
            _Forzaje = value
        End Set
    End Property
    Public Property Tipo_Forzaje As String
        Get
            Return _Tipo_Forzaje
        End Get
        Set(ByVal value As String)
            _Tipo_Forzaje = value
        End Set
    End Property

    Public Property Tarea_Anterior As Integer
        Get
            Return _Tarea_Anterior
        End Get
        Set(value As Integer)
            _Tarea_Anterior = value
        End Set
    End Property

    Public Property Tarea_Siguiente As Integer
        Get
            Return _Tarea_Siguiente
        End Get
        Set(value As Integer)
            _Tarea_Siguiente = value
        End Set
    End Property

    Public Property Usuario_Forza As Integer
        Get
            Return _Usuario_Forza
        End Get
        Set(value As Integer)
            _Usuario_Forza = value
        End Set
    End Property

    Public Property StrError() As String
        Get
            Return _strError
        End Get
        Set(value As String)
            _strError = value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Sub New(ByVal intRegistro As Integer)
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intRegistro = 0 Then Exit Sub
            strSQL.Append(" SELECT ")
            strSQL.Append(" PDK_ID_OPE_SOLICITUD,")
            strSQL.Append(" PDK_ID_SOLICITUD,")
            strSQL.Append(" PDK_ID_CAT_RESULTADO,")
            strSQL.Append(" PDK_ID_CAT_RECHAZOS,")
            strSQL.Append(" PDK_ID_TAREAS,")
            strSQL.Append(" PDK_OPE_STATUS,")
            strSQL.Append(" PDK_OPE_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" PDK_OPE_USU_ASIGNADO")
            strSQL.Append(" FROM PDK_OPE_SOLICITUD")
            strSQL.Append(" WHERE PDK_ID_SOLICITUD = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)

            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If

            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_OPE_SOLICITUD = .Item("PDK_ID_OPE_SOLICITUD")
                Me.intPDK_ID_SOLICITUD = .Item("PDK_ID_SOLICITUD")
                Me.intPDK_ID_CAT_RESULTADO = .Item("PDK_ID_CAT_RESULTADO")
                Me.intPDK_ID_CAT_RECHAZOS = .Item("PDK_ID_CAT_RECHAZOS")
                Me.intPDK_ID_TAREAS = .Item("PDK_ID_TAREAS")
                Me.intPDK_OPE_STATUS = .Item("PDK_OPE_STATUS")
                Me.strPDK_OPE_MODIF = .Item("PDK_OPE_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_OPE_USU_ASIGNADO = .Item("PDK_OPE_USU_ASIGNADO")
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
            strSQL.Append(" A.PDK_ID_OPE_SOLICITUD,")
            strSQL.Append(" A.PDK_ID_SOLICITUD,")
            strSQL.Append(" A.PDK_ID_CAT_RESULTADO,")
            strSQL.Append(" A.PDK_ID_CAT_RECHAZOS,")
            strSQL.Append(" A.PDK_ID_TAREAS,")
            strSQL.Append(" A.PDK_OPE_STATUS,")
            strSQL.Append(" A.PDK_OPE_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_OPE_USU_ASIGNADO,")
            strSQL.Append(" FROM PDK_OPE_SOLICITUD A ")

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_OPE_SOLICITUD")
            Throw objException
        End Try
    End Function


    Public Function ObtenStatusSolicitud(solid) As DataSet

        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD

        If solid > 0 Then
            intPDK_ID_SOLICITUD = solid
        End If

        Try
            strSQL.Append(" SELECT A.PDK_ID_OPE_SOLICITUD 'PDK_ID_OPE_SOLICITUD' , ")
            strSQL.Append(" A.PDK_ID_SOLICITUD, ")
            strSQL.Append(" A.PDK_ID_CAT_RESULTADO, ")
            strSQL.Append(" A.PDK_ID_CAT_RECHAZOS, ")
            strSQL.Append(" A.PDK_ID_TAREAS, ")
            strSQL.Append(" A.PDK_OPE_STATUS, ")
            strSQL.Append(" CONVERT(VARCHAR(10),A.PDK_OPE_MODIF,120) 'PDK_OPE_MODIF',")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_OPE_USU_ASIGNADO,")
            strSQL.Append(" ISNULL(I.PDK_USU_NOMBRE,'') + ' ' + ISNULL(I.PDK_USU_APE_PAT,'') + ' ' + ISNULL(I.PDK_USU_APE_MAT,'') 'USUARIO',")
            strSQL.Append(" ISNULL(J.PDK_USU_NOMBRE,'') + ' ' + ISNULL(J.PDK_USU_APE_PAT,'') + ' ' + ISNULL(J.PDK_USU_APE_MAT,'') 'ASESOR',")
            strSQL.Append(" F.PDK_FLU_NOMBRE,")
            strSQL.Append(" E.PDK_PROC_NOMBRE,")
            strSQL.Append(" D.PDK_TAR_NOMBRE,")
            strSQL.Append(" H.PDK_ID_PANTALLAS,")

            'strSQL.Append(" './Blanco.aspx' + '?idFolio=' + CONVERT(VARCHAR,a.PDK_ID_SOLICITUD) + '&idPantalla=' +  CONVERT(VARCHAR,H.PDK_ID_PANTALLAS) 'PDK_PANT_LINK',")
            strSQL.Append(" CASE WHEN PDK_PANT_DOCUMENTOS = 26 THEN './consultaPantallaDocumentos.aspx?pantalla=' + CONVERT(VARCHAR,H.PDK_ID_PANTALLAS)  + '&solicitud=' + convert(VARCHAR,a.PDK_ID_SOLICITUD) ")
            strSQL.Append(" WHEN  PDK_PANT_DOCUMENTOS = 44 THEN './consultaPantallaEntrevista.aspx?idPantalla=' + CONVERT(VARCHAR,H.PDK_ID_PANTALLAS)  + '&IdFolio=' + convert(VARCHAR,a.PDK_ID_SOLICITUD)+ '&CVE=1' ")
            strSQL.Append(" ELSE './Blanco.aspx' + '?idFolio=' + CONVERT(VARCHAR,a.PDK_ID_SOLICITUD) + '&idPantalla=' +  CONVERT(VARCHAR,H.PDK_ID_PANTALLAS) END 'PDK_PANT_LINK',")

            strSQL.Append(" 'IN CREDIT' 'PDK_EMP_NOMBRE',")
            strSQL.Append(" ISNULL(L.NOMBRE1,'') + ' ' + ISNULL(L.NOMBRE2,'') + ' ' + ISNULL(L.APELLIDO_PATERNO,'') + ' ' + ISNULL(L.APELLIDO_MATERNO,'') 'PDK_NOMBRE_CLIENTE',")
            strSQL.Append(" K.PDK_PROD_NOMBRE,")
            strSQL.Append(" CASE WHEN A.PDK_ID_CAT_RECHAZOS > 0 THEN 3 ELSE 1 END 'PDK_OPE_IMG_STATUS', ")
            strSQL.Append(" ISNULL(ZZ.PDK_ID_BURO_REPORTE,'') 'PDK_IMG_BURO_SOL', ")
            strSQL.Append(" ISNULL(WW.PDK_ID_BURO_REPORTE,'') 'PDK_IMG_BURO_COA' ")


            strSQL.Append(" FROM PDK_OPE_SOLICITUD A")
            strSQL.Append("     LEFT OUTER JOIN PDK_CAT_RESULTADO B ON A.PDK_ID_CAT_RESULTADO  = B.PDK_ID_CAT_RESULTADO ")
            strSQL.Append("     LEFT OUTER JOIN PDK_CAT_RECHAZOS C ON A.PDK_ID_CAT_RECHAZOS  = C.PDK_ID_CAT_RECHAZOS ")
            strSQL.Append("     LEFT OUTER  JOIN PDK_CAT_TAREAS D ON D.PDK_ID_TAREAS = A.PDK_ID_TAREAS ")
            strSQL.Append("     LEFT OUTER JOIN PDK_CAT_PROCESOS E ON E.PDK_ID_PROCESOS = D.PDK_ID_PROCESOS ")
            strSQL.Append("     LEFT OUTER JOIN PDK_CAT_FLUJOS F ON F.PDK_ID_FLUJOS = E.PDK_ID_FLUJOS ")
            strSQL.Append("     LEFT OUTER JOIN PDK_REL_PANTALLA_TAREA G ON G.PDK_ID_TAREAS = A.PDK_ID_TAREAS ")
            strSQL.Append("     INNER JOIN PDK_PANTALLAS H ON H.PDK_ID_PANTALLAS = G.PDK_ID_PANTALLAS AND H.PDK_PANT_STATUS = 2")
            strSQL.Append("     LEFT OUTER JOIN PDK_USUARIO I ON I.PDK_ID_USUARIO = A.PDK_CLAVE_USUARIO")
            strSQL.Append("     LEFT OUTER JOIN PDK_USUARIO J ON J.PDK_ID_USUARIO = A.PDK_OPE_USU_ASIGNADO")
            strSQL.Append("     LEFT OUTER JOIN PDK_CAT_PRODUCTOS K ON K.PDK_ID_PRODUCTOS = F.PDK_ID_PRODUCTOS ")
            strSQL.Append("     LEFT OUTER JOIN PDK_TAB_SOLICITANTE L ON L.PDK_ID_SECCCERO  = A.PDK_ID_SOLICITUD")

            'strSQL.Append("     LEFT OUTER JOIN (SELECT ISNULL(COUNT(*),0) 'PDK_BURO_IMG_STATUS', ISNULL(PDK_ID_SECCCERO,0) AS PDK_ID_SECCCERO FROM PDK_BURO_REPORTE GROUP BY PDK_ID_SECCCERO ) ZZ ON  ZZ.PDK_ID_SECCCERO = A.PDK_ID_SOLICITUD")
            strSQL.Append("     LEFT OUTER JOIN (SELECT YY.PDK_ID_SECCCERO, MIN(YY.PDK_ID_BURO_REPORTE) 'PDK_ID_BURO_REPORTE' FROM PDK_BURO_REPORTE YY	INNER JOIN PDK_BURO_BITACORA XX ON XX.PDK_ID_BURO = YY.PDK_ID_BURO GROUP BY YY.PDK_ID_SECCCERO) ZZ ON  ZZ.PDK_ID_SECCCERO = A.PDK_ID_SOLICITUD ")
            strSQL.Append("     LEFT OUTER JOIN (SELECT YY.PDK_ID_SECCCERO, MAX(YY.PDK_ID_BURO_REPORTE) 'PDK_ID_BURO_REPORTE' FROM PDK_BURO_REPORTE YY 	INNER JOIN PDK_BURO_BITACORA XX ON XX.PDK_ID_BURO = YY.PDK_ID_BURO GROUP BY YY.PDK_ID_SECCCERO ) WW ON  WW.PDK_ID_SECCCERO = A.PDK_ID_SOLICITUD AND WW.PDK_ID_BURO_REPORTE <> ZZ.PDK_ID_BURO_REPORTE  ")




            strSQL.Append(" WHERE 1=1")

            If intPDK_ID_SOLICITUD > 0 Then
                strSQL.Append(" AND A.PDK_ID_SOLICITUD = " & intPDK_ID_SOLICITUD)
            End If

            If intPDK_OPE_USU_ASIGNADO > 0 Then
                strSQL.Append(" AND A.PDK_OPE_USU_ASIGNADO = " & intPDK_OPE_USU_ASIGNADO)
            End If

            If intPDK_CLAVE_USUARIO > 0 Then
                strSQL.Append(" AND A.PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO)
            End If

            strSQL.Append(" AND A.PDK_ID_OPE_SOLICITUD = (SELECT MAX(PDK_ID_OPE_SOLICITUD) FROM PDK_OPE_SOLICITUD WHERE PDK_OPE_SOLICITUD.PDK_ID_SOLICITUD = A.PDK_ID_SOLICITUD)")

            'strSQL.Append(" GROUP BY A.PDK_ID_SOLICITUD,  A.PDK_ID_CAT_RESULTADO,  A.PDK_ID_CAT_RECHAZOS,  A.PDK_ID_TAREAS,  A.PDK_OPE_STATUS,  CONVERT(VARCHAR(10),A.PDK_OPE_MODIF,120), A.PDK_CLAVE_USUARIO, A.PDK_OPE_USU_ASIGNADO, F.PDK_FLU_NOMBRE, E.PDK_PROC_NOMBRE, D.PDK_TAR_NOMBRE, H.PDK_ID_PANTALLAS, I.PDK_USU_NOMBRE, I.PDK_USU_APE_PAT, I.PDK_USU_APE_MAT, J.PDK_USU_NOMBRE, J.PDK_USU_APE_PAT, J.PDK_USU_APE_MAT, H.PDK_PANT_LINK, K.PDK_PROD_NOMBRE  ")

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds

        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_OPE_SOLICITUD")
            Throw objException
        End Try
    End Function


    Public Function ObtenSolicitudFlujo() As DataSet

        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD

        Try
            strSQL.Append(" SELECT A.PDK_ID_OPE_SOLICITUD 'PDK_ID_OPE_SOLICITUD' , ")
            strSQL.Append(" A.PDK_ID_SOLICITUD, ")
            strSQL.Append(" A.PDK_ID_CAT_RESULTADO, ")
            strSQL.Append(" A.PDK_ID_CAT_RECHAZOS, ")
            strSQL.Append(" A.PDK_ID_TAREAS, ")
            strSQL.Append(" A.PDK_OPE_STATUS, ")
            strSQL.Append(" CONVERT(VARCHAR(10),A.PDK_OPE_MODIF,120) 'PDK_OPE_MODIF',")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_OPE_USU_ASIGNADO,")
            strSQL.Append(" ISNULL(I.PDK_USU_NOMBRE,'') + ' ' + ISNULL(I.PDK_USU_APE_PAT,'') + ' ' + ISNULL(I.PDK_USU_APE_MAT,'') 'USUARIO',")
            strSQL.Append(" ISNULL(J.PDK_USU_NOMBRE,'') + ' ' + ISNULL(J.PDK_USU_APE_PAT,'') + ' ' + ISNULL(J.PDK_USU_APE_MAT,'') 'ASESOR',")
            strSQL.Append(" F.PDK_FLU_NOMBRE,")
            strSQL.Append(" E.PDK_PROC_NOMBRE,")
            strSQL.Append(" D.PDK_TAR_NOMBRE,")
            strSQL.Append(" H.PDK_ID_PANTALLAS,")

            strSQL.Append(" CASE WHEN PDK_PANT_DOCUMENTOS = 26 THEN './consultaPantallaDocumentos.aspx?pantalla=' + CONVERT(VARCHAR,H.PDK_ID_PANTALLAS)  + '&solicitud=' + convert(VARCHAR,a.PDK_ID_SOLICITUD) + '&Enable=1' ")
            strSQL.Append(" ELSE './Blanco.aspx' + '?idFolio=' + CONVERT(VARCHAR,a.PDK_ID_SOLICITUD) + '&idPantalla=' +  CONVERT(VARCHAR,H.PDK_ID_PANTALLAS) + '&Enable=1' END 'PDK_PANT_LINK',")

            strSQL.Append(" 'IN CREDIT' 'PDK_EMP_NOMBRE',")
            strSQL.Append(" ISNULL(L.NOMBRE1,'') + ' ' + ISNULL(L.NOMBRE2,'') + ' ' + ISNULL(L.APELLIDO_PATERNO,'') + ' ' + ISNULL(L.APELLIDO_MATERNO,'') 'PDK_NOMBRE_CLIENTE',")
            strSQL.Append(" K.PDK_PROD_NOMBRE,")
            strSQL.Append(" CASE WHEN A.PDK_ID_CAT_RECHAZOS > 0 THEN 3 ELSE 1 END 'PDK_OPE_IMG_STATUS',PP.PDK_PAR_SIS_PARAMETRO AS STATUSTAREA ")
            strSQL.Append(" FROM PDK_OPE_SOLICITUD A")
            strSQL.Append("     LEFT OUTER JOIN PDK_CAT_RESULTADO B ON A.PDK_ID_CAT_RESULTADO  = B.PDK_ID_CAT_RESULTADO ")
            strSQL.Append("     LEFT OUTER JOIN PDK_CAT_RECHAZOS C ON A.PDK_ID_CAT_RECHAZOS  = C.PDK_ID_CAT_RECHAZOS ")
            strSQL.Append("     LEFT OUTER  JOIN PDK_CAT_TAREAS D ON D.PDK_ID_TAREAS = A.PDK_ID_TAREAS ")
            strSQL.Append("     LEFT OUTER JOIN PDK_CAT_PROCESOS E ON E.PDK_ID_PROCESOS = D.PDK_ID_PROCESOS ")
            strSQL.Append("     LEFT OUTER JOIN PDK_CAT_FLUJOS F ON F.PDK_ID_FLUJOS = E.PDK_ID_FLUJOS ")
            strSQL.Append("     LEFT OUTER JOIN PDK_REL_PANTALLA_TAREA G ON G.PDK_ID_TAREAS = A.PDK_ID_TAREAS ")
            strSQL.Append("     INNER JOIN PDK_PANTALLAS H ON H.PDK_ID_PANTALLAS = G.PDK_ID_PANTALLAS AND H.PDK_PANT_STATUS = 2 AND H.PDK_PANT_MOSTRAR =2")
            strSQL.Append("     LEFT OUTER JOIN PDK_USUARIO I ON I.PDK_ID_USUARIO = A.PDK_CLAVE_USUARIO")
            strSQL.Append("     LEFT OUTER JOIN PDK_USUARIO J ON J.PDK_ID_USUARIO = A.PDK_OPE_USU_ASIGNADO")
            strSQL.Append("     LEFT OUTER JOIN PDK_CAT_PRODUCTOS K ON K.PDK_ID_PRODUCTOS = F.PDK_ID_PRODUCTOS ")
            strSQL.Append("     INNER JOIN PDK_TAB_SOLICITANTE L ON L.PDK_ID_SECCCERO  = A.PDK_ID_SOLICITUD")
            strSQL.Append("     INNER JOIN PDK_PARAMETROS_SISTEMA PP ON A.PDK_OPE_STATUS_TAREA=PP.PDK_ID_PARAMETROS_SISTEMA AND PP.PDK_PAR_SIS_ID_PADRE=38")
            strSQL.Append(" WHERE 1=1")

            If intPDK_ID_SOLICITUD > 0 Then
                strSQL.Append(" AND A.PDK_ID_SOLICITUD = " & intPDK_ID_SOLICITUD)
            End If

            If intPDK_OPE_USU_ASIGNADO > 0 Then
                strSQL.Append(" AND A.PDK_OPE_USU_ASIGNADO = " & intPDK_OPE_USU_ASIGNADO)
            End If

            If intPDK_CLAVE_USUARIO > 0 Then
                strSQL.Append(" AND A.PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO)
            End If

            strSQL.Append(" ORDER BY A.PDK_OPE_MODIF")

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_OPE_SOLICITUD")
            Throw objException
        End Try
    End Function

    Public Function ObtenStatusSolicitudRechazo() As DataSet

        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD

        Try

            strSQL.Append("  SELECT A.PDK_ID_SOLICITUD 'PDK_ID_SOLICITUD', B.PDK_REC_NOMBRE 'DESCRIPCION' ")
            strSQL.Append("  FROM PDK_OPE_SOLICITUD A ")
            strSQL.Append("  INNER JOIN PDK_CAT_RECHAZOS B ON B.PDK_ID_CAT_RECHAZOS = A.PDK_ID_CAT_RECHAZOS  ")
            strSQL.Append("  WHERE A.PDK_ID_CAT_RECHAZOS > 0 ")

            If intPDK_ID_SOLICITUD > 0 Then
                strSQL.Append(" AND A.PDK_ID_SOLICITUD = " & intPDK_ID_SOLICITUD)
            End If

            strSQL.Append("  ORDER BY A.PDK_OPE_MODIF DESC ")

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_OPE_SOLICITUD")
            Throw objException
        End Try

    End Function

    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_OPE_SOLICITUD = 0 Then
                strSql = "INSERT INTO PDK_OPE_SOLICITUD " & _
                    "(" & _
                    "PDK_ID_OPE_SOLICITUD,PDK_ID_SOLICITUD,PDK_ID_CAT_RESULTADO,PDK_ID_CAT_RECHAZOS,PDK_ID_TAREAS,PDK_OPE_STATUS,PDK_OPE_MODIF,PDK_CLAVE_USUARIO,PDK_OPE_USU_ASIGNADO,)" & _
                    " VALUES ( " & intPDK_ID_SOLICITUD & ",  " & intPDK_ID_CAT_RESULTADO & ",  " & intPDK_ID_CAT_RECHAZOS & ",  " & intPDK_ID_TAREAS & ",  " & intPDK_OPE_STATUS & ", '" & strPDK_OPE_MODIF & "', " & intPDK_CLAVE_USUARIO & ",  " & intPDK_OPE_USU_ASIGNADO & " " & _
                    ")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_OPE_SOLICITUD ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD

        Try
            strSql = "UPDATE PDK_OPE_SOLICITUD " & _
               "SET " & _
             " PDK_ID_SOLICITUD = " & intPDK_ID_SOLICITUD & ", " & _
             " PDK_ID_CAT_RESULTADO = " & intPDK_ID_CAT_RESULTADO & ", " & _
             " PDK_ID_CAT_RECHAZOS = " & intPDK_ID_CAT_RECHAZOS & ", " & _
             " PDK_ID_TAREAS = " & intPDK_ID_TAREAS & ", " & _
             " PDK_OPE_STATUS = " & intPDK_OPE_STATUS & ", " & _
             " PDK_OPE_MODIF = '" & strPDK_OPE_MODIF & "'," & _
             " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
             " PDK_OPE_USU_ASIGNADO = " & intPDK_OPE_USU_ASIGNADO & ", " & _
             " WHERE PDK_ID_OPE_SOLICITUD=  " & intPDK_ID_OPE_SOLICITUD
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_OPE_SOLICITUD")
        End Try
    End Sub

    Public Function ConsultaSolicitud(ByVal intOper As Integer) As DataSet
        Dim MB As New ProdeskNet.BD.clsManejaBD
        Try

            strErrSolicitud = ""

            Select Case intOper
                Case 1 'CONSULTA
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                Case 2
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                Case 3

                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then MB.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If strNombre <> "" Then MB.AgregaParametro("@Nombre", ProdeskNet.BD.TipoDato.Cadena, strNombre)
                    If strFechaIni <> "" Then MB.AgregaParametro("@FechaIni", ProdeskNet.BD.TipoDato.Cadena, strFechaIni)
                    If strFechaFin <> "" Then MB.AgregaParametro("@FechaFin", ProdeskNet.BD.TipoDato.Cadena, strFechaFin)
                    If strRFC <> "" Then MB.AgregaParametro("@RFC", ProdeskNet.BD.TipoDato.Cadena, strRFC)
                Case 4
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then MB.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If strNombre <> "" Then MB.AgregaParametro("@Nombre", ProdeskNet.BD.TipoDato.Cadena, strNombre)
                    If strFechaIni <> "" Then MB.AgregaParametro("@FechaIni", ProdeskNet.BD.TipoDato.Cadena, strFechaIni)
                    If strFechaFin <> "" Then MB.AgregaParametro("@FechaFin", ProdeskNet.BD.TipoDato.Cadena, strFechaFin)
                    If strRFC <> "" Then MB.AgregaParametro("@RFC", ProdeskNet.BD.TipoDato.Cadena, strRFC)
                Case 5
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then MB.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                Case 6
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then MB.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If strNombre <> "" Then MB.AgregaParametro("@Nombre", ProdeskNet.BD.TipoDato.Cadena, strNombre)
                    If strFechaIni <> "" Then MB.AgregaParametro("@FechaIni", ProdeskNet.BD.TipoDato.Cadena, strFechaIni)
                    If strFechaFin <> "" Then MB.AgregaParametro("@FechaFin", ProdeskNet.BD.TipoDato.Cadena, strFechaFin)
                    If strRFC <> "" Then MB.AgregaParametro("@RFC", ProdeskNet.BD.TipoDato.Cadena, strRFC)
                    If strAsesor <> "" Then MB.AgregaParametro("@Asesor", ProdeskNet.BD.TipoDato.Cadena, strAsesor)
                    If IntId_Estatus <> 0 Then MB.AgregaParametro("@Id_Estatus", ProdeskNet.BD.TipoDato.Cadena, IntId_Estatus)
                    If intDesdeSol <> 0 Then MB.AgregaParametro("@Desde_Solicitud", ProdeskNet.BD.TipoDato.Entero, intDesdeSol)
                    If intHastaSol <> 0 Then MB.AgregaParametro("@Hasta_Solicitud", ProdeskNet.BD.TipoDato.Entero, intHastaSol)
                    'BUG-PD-365
                Case 7
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then MB.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
            End Select

            ConsultaSolicitud = MB.EjecutaStoredProcedure("Consulta_Solicitud")

        Catch ex As Exception
            strErrSolicitud = ex.Message
        End Try
    End Function
    Public Function ConsultaDocumento(ByVal intOper As Integer) As DataSet
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strErrSolicitud = ""

            Select Case intOper
                Case 1
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_ID_PANTALLA > 0 Then BD.AgregaParametro("@Pantalla", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PANTALLA)
                    If intPDK_CLAVE_USUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)

                Case 2
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_ID_PANTALLA > 0 Then BD.AgregaParametro("@Pantalla", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PANTALLA)
                    If intPDK_CLAVE_USUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)

                Case 3
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_ID_PANTALLA > 0 Then BD.AgregaParametro("@Pantalla", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PANTALLA)
                    If intPDK_CLAVE_USUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                    If strComentario <> "" Then BD.AgregaParametro("@coment", ProdeskNet.BD.TipoDato.Cadena, strComentario)


            End Select

            ConsultaDocumento = BD.EjecutaStoredProcedure("Consulta_Documento")

            If (BD.ErrorBD) <> "" Then
                strErrSolicitud = BD.ErrorBD
            End If

        Catch ex As Exception
            strErrSolicitud = ex.Message
        End Try
    End Function

    Public Function ManejaTarea(ByVal intOper) As DataSet
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try


            Select Case intOper
                Case 1
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_ID_PANTALLA > 0 Then BD.AgregaParametro("@Pantalla", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PANTALLA)

                Case 2
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_ID_PANTALLA > 0 Then BD.AgregaParametro("@Pantalla", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PANTALLA)
                    If intPDK_CLAVE_USUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                    If strNombreUsuario <> "" Then BD.AgregaParametro("@nomUsuario", ProdeskNet.BD.TipoDato.Cadena, strNombreUsuario)
                    If strContraseña <> "" Then BD.AgregaParametro("@contraseña", ProdeskNet.BD.TipoDato.Cadena, strContraseña)
                    If _Forzaje > 0 Then BD.AgregaParametro("@FORZAJE", ProdeskNet.BD.TipoDato.Entero, Forzaje)
                Case 3
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_CLAVE_USUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                    If intStatus > 0 Then BD.AgregaParametro("@status", ProdeskNet.BD.TipoDato.Entero, intStatus)
                Case 4 'BUG-PD-09: AVH
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_CLAVE_USUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                    If intStatus > 0 Then BD.AgregaParametro("@status", ProdeskNet.BD.TipoDato.Entero, intStatus)
                Case 5
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_ID_PANTALLA > 0 Then BD.AgregaParametro("@Pantalla", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PANTALLA)
                    If intPDK_ID_CAT_RESULTADO > 0 Then BD.AgregaParametro("@CVETARNREC", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_CAT_RESULTADO)

                Case 6
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_ID_PANTALLA > 0 Then BD.AgregaParametro("@Pantalla", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PANTALLA)
                    If intPDK_CLAVE_USUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                    If strComentario <> "" Then BD.AgregaParametro("@MENSAJE", ProdeskNet.BD.TipoDato.Cadena, strComentario)
                    If intStatus > 0 Then BD.AgregaParametro("@status", ProdeskNet.BD.TipoDato.Entero, intStatus)
                    If intStatus_cred > 0 Then BD.AgregaParametro("@status_cred", ProdeskNet.BD.TipoDato.Entero, intStatus_cred)
            End Select

            ManejaTarea = BD.EjecutaStoredProcedure("sp_ManejaTarea")

            If (BD.ErrorBD) <> "" Then
                strErrSolicitud = BD.ErrorBD
            End If

        Catch ex As Exception
            strErrSolicitud = ex.Message
        End Try

    End Function

    Public Function ConsultaCuestionarios(ByVal intOper) As DataSet
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try

            Select Case intOper
                Case 1
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_CLAVE_USUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                Case 2
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_CLAVE_USUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                Case 3
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_CLAVE_USUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                    BD.AgregaParametro("@ID_PREGUNTA", ProdeskNet.BD.TipoDato.Cadena, strID_Pregunta)
                    BD.AgregaParametro("@PREGUNTA", ProdeskNet.BD.TipoDato.Cadena, strPregunta)
                    BD.AgregaParametro("@SI", ProdeskNet.BD.TipoDato.Entero, intSi)
                    BD.AgregaParametro("@NO", ProdeskNet.BD.TipoDato.Entero, intNo)
                    BD.AgregaParametro("@VALOR", ProdeskNet.BD.TipoDato.Cadena, strValor)
                    BD.AgregaParametro("@RES", ProdeskNet.BD.TipoDato.Entero, intRes)
                    BD.AgregaParametro("@INVALIDO_EMISION", ProdeskNet.BD.TipoDato.Entero, intINVALIDO_EMISION)
                Case 4
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_CLAVE_USUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                    BD.AgregaParametro("@PESO", ProdeskNet.BD.TipoDato.Flotante, dblPeso)
                    BD.AgregaParametro("@ESTATURA", ProdeskNet.BD.TipoDato.Flotante, dblEstatura)

                Case 5
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@Sol", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    BD.AgregaParametro("@INVALIDO_EMISION", ProdeskNet.BD.TipoDato.Entero, intINVALIDO_EMISION)
            End Select

            ConsultaCuestionarios = BD.EjecutaStoredProcedure("sp_ManejaCuestionario")

            If (BD.ErrorBD) <> "" Then
                strErrSolicitud = BD.ErrorBD
            End If

        Catch ex As Exception
            strErrSolicitud = ex.Message
        End Try
    End Function

    Public Function ValNegocio(ByVal intOper) As DataSet

        Dim BD As New ProdeskNet.BD.clsManejaBD

        Select Case intOper
            Case 1

                If intPDK_ID_SOLICITUD > 0 Then BD.AgregaParametro("@IDSOLICITUD", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                If intBOTON > 0 Then BD.AgregaParametro("@BOTON", ProdeskNet.BD.TipoDato.Entero, intBOTON)
                If intPDK_CLAVE_USUARIO > 0 Then BD.AgregaParametro("@usuario", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                If intPDK_ID_CAT_RESULTADO > 0 Then BD.AgregaParametro("@BANDERA", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_CAT_RESULTADO)
        End Select


        ValNegocio = BD.EjecutaStoredProcedure("spValNegocio")
        If (BD.ErrorBD) <> "" Then
            strErrSolicitud = BD.ErrorBD
        End If

    End Function
    'Bug-pd-179:JBEJAR 
    Public Function insertaForzaje() As Boolean
        insertaForzaje = False
        Dim dsres As DataSet = New DataSet
        Dim BD As New clsManejaBD
        Try
            BD.AgregaParametro("@id_solicitud", TipoDato.Entero, intPDK_ID_SOLICITUD)
            BD.AgregaParametro("@tipo_forzaje", TipoDato.Cadena, Tipo_Forzaje)
            BD.AgregaParametro("@tarea_anterior", TipoDato.Entero, Tarea_Anterior)
            BD.AgregaParametro("@tarea_siguiente", TipoDato.Entero, Tarea_Siguiente)
            BD.AgregaParametro("@usuario_for", TipoDato.Entero, Usuario_Forza)
            dsres = BD.EjecutaStoredProcedure("spInsertaForzaje")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        insertaForzaje = True
                    Else
                        insertaForzaje = False
                        Throw New Exception(dsres.Tables(0).Rows(0).Item("RESULT").ToString())
                    End If
                Else
                    Throw New Exception("Falla al guardar forzaje")
                End If
            Else
                Throw New Exception("Falla al guardar forzaje")
            End If
        Catch ex As Exception

        End Try
    End Function

#End Region
    '-------------------------- FIN PDK_OPE_SOLICITUD-------------------------- 

End Class
