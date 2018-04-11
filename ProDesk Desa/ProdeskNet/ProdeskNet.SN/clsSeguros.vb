Imports ProdeskNet.BD
Imports System.Text
Imports System.Configuration
Imports System.Data.SqlClient

'BBV-P-423 20/01/2017: JRHM RQCONYFOR-02: Se crea clase nueva para la consultas e inserccion de datos de seguro para la tarea de "Documentación de Pólizas de Seguros y Desembolso" 
'BUG-PD-17 JRHM 16/03/17 Se cambia como se realizaba la consulta de GetFinCredito
'BUG-PD-33 JRHM 24/04/17 SE CREO NUEVA FUNCION PARA LA CONSULTA DE LOS DATOS QUE REQUERIRAN LOS BROKERS PARA REALIZAR LA EMISION
'BUG-PD-45: RHERNANDEZ: 15/05/17 SE AGREGO METODO PARA ACTIALIZAR LOS DATOS DE LA POLIZA EN CASO DE QUE PASE POR UNA CANCELACION
'BUG-PD-98: RHERNANDEZ: 20/06/17: SE AGREGA PDK_ID_QUOTELIFE A GUARDADO DE DATOS DE POLIZAS PARA SU FUTURA CANCELACION
'BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN
'BUG-PD-199: RHERNANDEZ: 24/08/17 SE AGREGA METODO PARA CONSULTA DE DATOS BASE64 DE VISOR DE DOCUMENTOS TELEPRO
'BUG-PD-299: RHERNANDEZ: 13/12/17: SE CAMBIA LA FORMA EN QUE SE ALMACENA EL ARCHIVO EN DE BASE64 A UN ARCHIVO GZ EN VARBINARY 
'BUG-PD-312: RHERNANDEZ: 18/12/17: sE CONFIGURA LA OPCION NO PROCESABLE PARA LA PANTALLA DE INS CHECK DOCUMENTAL
Public Class clsSeguros
    Private _strError As String = String.Empty
    Private ID_SOLICITUD As Integer = 0
    Private NOMSEGDANIOS As String = String.Empty
    Private NUMSEGDANIOS As String = String.Empty
    Private VIGSEGDANIOS As String = String.Empty
    Private NOMSEGVIDA As String = String.Empty
    Private NUMSEGVIDA As String = String.Empty
    Private VIGSEGVIDA As String = String.Empty
    Private ID_DOC As Integer = 0
    Private ID_RELPANDOC As Integer = 0
    Private NOMDOC As String = String.Empty
    Private VER As Integer = 0
    Private TIPO_SEG As Integer = -1
    Private ID_ENDOSO As String = String.Empty
    Private PDK_ID_QUOTELIFE As String = String.Empty
    Private NOMBRE_ARCHIVO As String = String.Empty
    Private DATOS_ARCHIVO As Byte()
    Private ID_DOC_SOL As String = String.Empty
    Private ID_PANTALLA As Integer = 0

    Sub New()

    End Sub
    Public Property StrError() As String
        Get
            Return _strError
        End Get
        Set(value As String)
            _strError = value
        End Set
    End Property
    Public Property _ID_SOLICITUD() As Integer
        Get
            Return ID_SOLICITUD
        End Get
        Set(value As Integer)
            ID_SOLICITUD = value
        End Set
    End Property
    Public Property _NOMSEGDANIOS() As String
        Get
            Return NOMSEGDANIOS
        End Get
        Set(value As String)
            NOMSEGDANIOS = value
        End Set
    End Property
    Public Property _NUMSEGDANIOS() As String
        Get
            Return NUMSEGDANIOS
        End Get
        Set(value As String)
            NUMSEGDANIOS = value
        End Set
    End Property
    Public Property _VIGSEGDANIOS() As String
        Get
            Return VIGSEGDANIOS
        End Get
        Set(value As String)
            VIGSEGDANIOS = value
        End Set
    End Property
    Public Property _NOMSEGVIDA() As String
        Get
            Return NOMSEGVIDA
        End Get
        Set(value As String)
            NOMSEGVIDA = value
        End Set
    End Property
    Public Property _NUMSEGVIDA() As String
        Get
            Return NUMSEGVIDA
        End Get
        Set(value As String)
            NUMSEGVIDA = value
        End Set
    End Property
    Public Property _VIGSEGVIDA() As String
        Get
            Return VIGSEGVIDA
        End Get
        Set(value As String)
            VIGSEGVIDA = value
        End Set
    End Property
    Public Property _ID_DOC() As Integer
        Get
            Return ID_DOC
        End Get
        Set(value As Integer)
            ID_DOC = value
        End Set
    End Property
    Public Property _ID_RELPANDOC() As Integer
        Get
            Return ID_RELPANDOC
        End Get
        Set(value As Integer)
            ID_RELPANDOC = value
        End Set
    End Property
    Public Property _NOMDOC() As String
        Get
            Return NOMDOC
        End Get
        Set(value As String)
            NOMDOC = value
        End Set
    End Property
    Public Property _VER() As Integer
        Get
            Return VER
        End Get
        Set(value As Integer)
            VER = value
        End Set
    End Property
    Public Property _TIPO_SEG() As Integer
        Get
            Return TIPO_SEG
        End Get
        Set(value As Integer)
            TIPO_SEG = value
        End Set
    End Property
    Public Property _ID_ENDOSO() As Integer
        Get
            Return ID_ENDOSO
        End Get
        Set(value As Integer)
            ID_ENDOSO = value
        End Set
    End Property
    Public Property _PDK_ID_QUOTELIFE() As String
        Get
            Return PDK_ID_QUOTELIFE
        End Get
        Set(value As String)
            PDK_ID_QUOTELIFE = value
        End Set
    End Property
    Public Property _NOMBRE_ARCHIVO As String
        Get
            Return NOMBRE_ARCHIVO
        End Get
        Set(value As String)
            NOMBRE_ARCHIVO = value
        End Set
    End Property
    Public Property _DATOS_ARCHIVO As Byte()
        Get
            Return DATOS_ARCHIVO
        End Get
        Set(value As Byte())
            DATOS_ARCHIVO = value
        End Set
    End Property
    Public Property _ID_DOC_SOL As String
        Get
            Return ID_DOC_SOL
        End Get
        Set(value As String)
            ID_DOC_SOL = value
        End Set
    End Property
    Public Property _ID_PANTALLA() As Integer
        Get
            Return ID_PANTALLA
        End Get
        Set(value As Integer)
            ID_PANTALLA = value
        End Set
    End Property

    Public Function getDatosSeguro() As DataSet
        Dim BD As New clsManejaBD
        BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_SOLICITUD)
        getDatosSeguro = BD.EjecutaStoredProcedure("getDatosSeguro")
        If (BD.ErrorBD) <> "" Then
            _strError = BD.ErrorBD
        End If
        Return getDatosSeguro
    End Function
    Public Function InsertDatosSeguroSolicitud() As Boolean
        InsertDatosSeguroSolicitud = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            BD.AgregaParametro("@ID_SOLICITUD", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@NOMSEGDANIOS", TipoDato.Cadena, NOMSEGDANIOS)
            BD.AgregaParametro("@NUMSEGDANIOS", TipoDato.Cadena, NUMSEGDANIOS)
            BD.AgregaParametro("@VIGSEGDANIOS", TipoDato.Cadena, VIGSEGDANIOS)
            BD.AgregaParametro("@NOMSEGVIDA", TipoDato.Cadena, NOMSEGVIDA)
            BD.AgregaParametro("@NUMSEGVIDA", TipoDato.Cadena, NUMSEGVIDA)
            BD.AgregaParametro("@VIGSEGVIDA", TipoDato.Cadena, VIGSEGVIDA)
            If PDK_ID_QUOTELIFE <> String.Empty Then
                BD.AgregaParametro("@PDK_ID_QUOTELIFE", TipoDato.Cadena, PDK_ID_QUOTELIFE)
            End If
            dsres = BD.EjecutaStoredProcedure("spInsertaSegurosSolicitud")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        InsertDatosSeguroSolicitud = True
                    Else
                        InsertDatosSeguroSolicitud = False
                        Throw New Exception(dsres.Tables(0).Rows(0).Item("RESULT").ToString())
                    End If
                Else
                    Throw New Exception("Carga de Datos de Seguro Fallida")
                End If
            Else
                Throw New Exception("Carga de Datos de Seguro Fallida")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return False
        End Try
    End Function
    Public Function InsertaDoc() As Boolean
        InsertaDoc = False
        Dim sql As New StringBuilder
        Try

            Dim sqlCon As SqlConnection = New SqlConnection(ConfigurationManager.AppSettings("Conexion").ToString)


            Using (sqlCon)

                Dim sqlComm As New SqlCommand

                sqlComm.Connection = sqlCon

                sqlComm.CommandText = "sp_insertaDOC"
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.AddWithValue("sol", ID_SOLICITUD)
                sqlComm.Parameters.AddWithValue("doc", ID_DOC)
                sqlComm.Parameters.AddWithValue("relpandoc", ID_RELPANDOC)
                sqlComm.Parameters.AddWithValue("ruta", NOMDOC)
                sqlComm.Parameters.AddWithValue("version", VER)
                sqlComm.Parameters.AddWithValue("nombre_archivo", NOMBRE_ARCHIVO)
                sqlComm.Parameters.AddWithValue("archivo_base", DATOS_ARCHIVO)

                sqlCon.Open()

                sqlComm.ExecuteNonQuery()

            End Using

            InsertaDoc = True
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return False
        End Try

    End Function

    Public Function GetFinCredito() As DataSet
        Dim dts As New DataSet
        Dim BD As New clsManejaBD
        Dim sql As New StringBuilder
        Dim Conex As String = ConfigurationManager.AppSettings("ConexProcotiza").ToString
        Dim Datos As String() = Split(Conex, ";")
        Dim NombreBD As String = Replace(Datos(1), "Initial Catalog=", "")

        sql.Append("SELECT C.FEC_PAGO, CASE WHEN B.ID_TIPO_SEGURO IN (75,76,77,78,188) THEN 1 ELSE 0 END AS ISMULTIANUAL FROM PDK_TAB_SECCION_CERO A INNER JOIN " + NombreBD + "..COTIZACIONES B ON A.ID_COTIZACION=B.ID_COTIZACION INNER JOIN ")
        sql.Append(NombreBD + "..TABLAAMORTIZA C ON B.ID_COTIZACION=C.ID_COTIZACION WHERE A.PDK_ID_SECCCERO=" + ID_SOLICITUD.ToString + " AND C.NO_PAGO=B.VALOR_PLAZO")
        Try
            dts = BD.EjecutarQuery(sql.ToString)
            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    GetFinCredito = dts
                Else
                    StrError = "Problema al consultar fecha final del credito"
                    GetFinCredito = Nothing
                End If
            Else
                StrError = "Problema al consultar fecha final del credito"
                GetFinCredito = Nothing
            End If
        Catch ex As Exception
            StrError = "Problema al consultar fecha final del credito"
            GetFinCredito = Nothing
        End Try
    End Function
    Public Function getDatosBroker(ByVal opcion As Integer) As DataSet
        Dim BD As New clsManejaBD
        BD.AgregaParametro("@ID_SOLICITUD", TipoDato.Entero, ID_SOLICITUD)
        BD.AgregaParametro("@ID_BROKER", TipoDato.Entero, opcion)
        getDatosBroker = BD.EjecutaStoredProcedure("SP_GETDATOS_BROKER")
        If (BD.ErrorBD) <> "" Then
            _strError = BD.ErrorBD
        End If
    End Function
    Public Function GetDatosPoliza() As DataSet
        Dim BD As New clsManejaBD
        BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_SOLICITUD)
        GetDatosPoliza = BD.EjecutaStoredProcedure("SpGetDatosPoliza")
        If (BD.ErrorBD) <> "" Then
            _strError = BD.ErrorBD
        End If
    End Function
    Public Function InsertCancelacionPoliza() As Boolean
        InsertCancelacionPoliza = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            BD.AgregaParametro("@ID_SOLICITUD", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@ID_ENDOSO", TipoDato.Cadena, ID_ENDOSO)
            BD.AgregaParametro("@TIPOSEGURO", TipoDato.Entero, TIPO_SEG)
            dsres = BD.EjecutaStoredProcedure("Sp_CancelaPolizas")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        InsertCancelacionPoliza = True
                    Else
                        InsertCancelacionPoliza = False
                        Throw New Exception(dsres.Tables(0).Rows(0).Item("RESULT").ToString())
                    End If
                Else
                    Throw New Exception("Carga de Datos de cancelacion de Seguro Fallida")
                End If
            Else
                Throw New Exception("Carga de Datos de cancelacion de Seguro Fallida")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return False
        End Try
    End Function

    Public Function GetDatosArchivo() As DataSet
        Dim dts As New DataSet
        Dim BD As New clsManejaBD
        Dim sql As New StringBuilder


        sql.Append("SELECT * FROM PDK_REL_PAN_DOC_SOL WHERE PDK_ID_DOC_SOLICITUD =" + ID_DOC_SOL)

        Try
            dts = BD.EjecutarQuery(sql.ToString)
            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    GetDatosArchivo = dts
                Else
                    StrError = "Problema al consultar datos del archivo consultado"
                    GetDatosArchivo = Nothing
                End If
            Else
                StrError = "Problema al consultar datos del archivo consultado"
                GetDatosArchivo = Nothing
            End If
        Catch ex As Exception
            StrError = "Problema al consultar datos del archivo consultado"
            GetDatosArchivo = Nothing
        End Try
    End Function
    Public Function ObtenDocumentosopc() As DataSet
        Dim dts As New DataSet
        Dim BD As New clsManejaBD
        Dim sql As New StringBuilder


        'sql.Append("SELECT B.PDK_ID_DOCUMENTOS,B.PDK_DOC_NOMBRE FROM PDK_REL_PAN_DOC A INNER JOIN PDK_CAT_DOCUMENTOS B ON A.PDK_ID_DOCUMENTOS=B.PDK_ID_DOCUMENTOS RIGHT OUTER JOIN PDK_DOCUM_NO_PROCESABLE C ON A.PDK_ID_PANTALLAS=C.PDK_ID_PANTALLAS AND A.PDK_ID_DOCUMENTOS<>C.PDK_ID_DOCUMENTOS  AND C.PDK_ID_SECCCERO=" + ID_SOLICITUD.ToString + " WHERE  A.PDK_REL_ACT_OBLIGATORIO=108 AND A.PDK_ID_PANTALLAS=" + ID_PANTALLA.ToString)
        sql.Append("SELECT B.PDK_ID_DOCUMENTOS,B.PDK_DOC_NOMBRE FROM PDK_REL_PAN_DOC A INNER JOIN PDK_CAT_DOCUMENTOS B ON A.PDK_ID_DOCUMENTOS=B.PDK_ID_DOCUMENTOS WHERE A.PDK_REL_ACT_OBLIGATORIO=108 AND A.PDK_ID_PANTALLAS=" + ID_PANTALLA.ToString + " and a.PDK_ID_DOCUMENTOS not in(SELECT PDK_ID_DOCUMENTOS FROM PDK_DOCUM_NO_PROCESABLE C WHERE C.PDK_ID_SECCCERO=" + ID_SOLICITUD.ToString + " AND C.PDK_ID_PANTALLAS=" + ID_PANTALLA.ToString + ")")
        Try
            dts = BD.EjecutarQuery(sql.ToString)
            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    ObtenDocumentosopc = dts
                Else
                    StrError = "Esta pantalla no cuenta con documentos opcionales"
                    ObtenDocumentosopc = Nothing
                End If
            Else
                StrError = "Esta pantalla no cuenta con documentos opcionales"
                ObtenDocumentosopc = Nothing
            End If
        Catch ex As Exception
            StrError = "Esta pantalla no cuenta con documentos opcionales"
            ObtenDocumentosopc = Nothing
        End Try
    End Function
    Public Function Inserta_No_Proc() As Boolean
        Inserta_No_Proc = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            Dim opc As Integer = 1
            BD.AgregaParametro("@OPCION", TipoDato.Entero, opc.ToString())
            BD.AgregaParametro("@ID_PANTALLAS", TipoDato.Entero, ID_PANTALLA)
            BD.AgregaParametro("@ID_SOLICITUD", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@ID_DOCUMENTO", TipoDato.Entero, ID_DOC)
           
            dsres = BD.EjecutaStoredProcedure("sp_PDK_DOCUM_NO_PROCESABLE")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    Inserta_No_Proc = True
                Else
                    Throw New Exception("Error al guardar documentos no procesables")
                End If
            Else
                Throw New Exception("Error al guardar documentos no procesables")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return False
        End Try
    End Function

End Class
