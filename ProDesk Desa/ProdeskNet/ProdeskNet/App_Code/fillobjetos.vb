#Region "trackers"
' YAM-P-208  egonzalez 11/09/2015 Se agregó el caso 'tbObjetos' en el servicio 'newFillgv()'
'BUG-PD-23 29/03/2017 MAPH Cambios para realizar la búsqueda con o sin autocompletar
'--- INC-B-1922 
'--- INC-B-1988:JDRA:se le agrega el ltrim ya que el campo se convirtio a entero 
'INC-B-1974:JDRA:Impresion de contratos.
'INC-B-2019:JDRA:Regresar
'BBV-P-423: RQSOL-03: AVH: 10/11/2016 se mandan valores por default opcion 2 sp_fillPanelControl
'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BUG-PD-01: AMR: 22/12/2016: HTTP Error 404 - Not Found al procesar Precalificación.
'BBV-P-423 RQCONYFOR-01: AVH: 02/01/2017 se agregan opciones en newFillgv 
'BUG-PD-04: GVARGAS: 11/01/2017: Cambios Precalificacion cambio de mansaje y validacion.
'BUG-PD-12: MAGP:20.02.2017: SE CAMBIA MENSAJE DE SALIDA 
'BUG-PD-21  GVARGAS  27/03/2017 Bugs Campos Obligatorios
'BUG-PD-78:MPUESTO:07/06/2017 CORRECCIÓN PREFORMALIZACION
'BUG-PD-99:MPUESTO:15/06/2017:CORRECCION PARA MOSTRAR O ELIMINAR LOS BOTONES DE ELIMINAR EN LAS TABLAS DE FACTURAS
'BUG-PD-123 ERODRIGUEZ 29/06/2017 Se creo la funcion btnInsertar_dos para permitir cancelar al estar equivocados los parametros que se envian, en tipo y orden
'BUG-PD-200 GVARGAS 24/08/2017 Regresa tareas atrapadas
'BUG-PD-205 RHERNANDEZ 31/08/17 Se modifica la consulta de distibuidor(Agencias) del panel de control
'RQADM2-01:MPUESTO:08/09/2017:Mejoras al Panel de Seguimiento.
'BUG-PD-288: Añade validacion consulta de municipio en la opcion case "DELEGA_O_MUNI17"
'BUG-PD-301: CGARCIA: 11/12/2017: SE CREA FILTRO PARA EL LLENADO DE COLONIAS Y ESTADOS DE LA PRECALIFICACION
'RQ-PI7-PD13-3: ERODRIGUEZ: 28/12/2017: Se llama a sp para buscar asignar solicitudes al cancelar
'BUG-PD-329: ERODRIGUEZ: 05/01/2017: Se creo FUNCION AsignaUsuarioSol para la opcion 6 para asignacion de tareas a usuario
'BBV-P-423 RQ-PD-17 8 GVARGAS 25/01/2018 Ajuste validaciones Biometrico
'BUG-PD-365 JMENDIETA 22/02/2018 Se actualiza CLIENTE_INCREDIT como null en la tabla PDK_TAB_DATOS_SOLICITANTE en metodo btnInsertarPanta
'BUG-PC-165: CGARCIA: 05/03/2018: SE CAMBIA CONSULTA DE MUNICIPIO 
'RQ-PD31: DJUAREZ: 08/03/2018: SE CREA POPUP PARA MODIFICAR LA COLONIA CUANDO SE GUARDE LA COLONIA "OTRO"
'BUG-PD-441: DCORNEJO: 09/05/2018: SE CREA VALIDACION PARA VERIFICAR PROBLEMAS DE CONEXION
#End Region

Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports ProdeskNet.Buro
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class fillobjetos
    Inherits System.Web.Services.WebService
    Dim BD As New ProdeskNet.BD.clsManejaBD

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()>
    Public Function llenaddl(ddl As String, depende As String) As ArrayList
        Dim salida As New ArrayList
        Dim ds As New DataSet
        Dim a As Integer = 0
        Dim arraydepende As Array

        arraydepende = depende.Split(",")

        Select Case ddl
            Case "Rechazo"
                ds = BD.EjecutarQuery("select PDK_REC_NOMBRE as selectedvalue, PDK_ID_CAT_RECHAZOS as selectedindex from PDK_CAT_RECHAZOS")
            Case "CondicionNva"
                ds = BD.EjecutarQuery("SELECT RN_CONDICION_REGLADNEGOCIO as selectedvalue, RN_ID_CONDICION as selectedindex FROM RN_CONDICION;")
            Case "RN"
                ds = BD.EjecutarQuery("select RN_NOM_REGLADNEGOCIO as selectedvalue, RN_ID as selectedindex from reglas_negocio;")
            Case "Tarea"
                ds = BD.EjecutarQuery("SELECT PDK_TAR_NOMBRE as selectedvalue, PDK_ID_TAREAS as selectedindex FROM PDK_CAT_TAREAS;")
            Case "Pantalla"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("select PDK_PANT_NOMBRE as selectedvalue, pp.PDK_ID_PANTALLAS as selectedindex from PDK_REL_PANTALLA_TAREA rpt inner join pdk_pantallas pp on rpt.PDK_ID_PANTALLAS = pp.PDK_ID_PANTALLAS where rpt.PDK_ID_TAREAS = " & arraydepende(0))
                Else
                    ds = BD.EjecutarQuery("SELECT ''as selectedvalue, ''as selectedindex;")
                End If
            Case "Seccion"
                ds = BD.EjecutarQuery("select B.PDK_SEC_DAT_NOMBRE AS selectedvalue,A.PDK_ID_SECCION_DATO  AS selectedindex  from PDK_SCORING A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO =B.PDK_ID_SECCION_DATO GROUP BY B.PDK_SEC_DAT_NOMBRE ,A.PDK_ID_SECCION_DATO ")
            Case "seccionDatos"
                ds = BD.EjecutarQuery("SELECT A.PDK_SEC_DAT_NOMBRE as selectedvalue,A.PDK_ID_SECCION_DATO as selectedindex  FROM PDK_SECCION_DATO A INNER JOIN PDK_SECCION B ON A.PDK_ID_SECCION=B.PDK_ID_SECCION   WHERE B.PDK_SEC_CREACION =2 AND A.PDK_SEC_DAT_STATUS=2 AND A.PDK_SEC_DAT_LLAVE=0  AND A.PDK_ID_SECCION <>0")
            Case "matriz"
                ds = BD.EjecutarQuery("SELECT PDK_PAR_SIS_PARAMETRO as  selectedvalue,PDK_ID_PARAMETROS_SISTEMA as selectedindex   FROM PDK_PARAMETROS_SISTEMA WHERE PDK_PAR_SIS_ID_PADRE=19")
            Case "tipoobjeto"
                ds = BD.EjecutarQuery("SELECT PDK_TIP_OBJ_NOMBRE as selectedvalue,PDK_ID_TIPO_OBJETO as selectedindex FROM PDK_TIPO_OBJETO")
            Case "formula"
                ds = BD.EjecutarQuery("SELECT PDK_PAR_SIS_PARAMETRO AS selectedvalue,PDK_ID_PARAMETROS_SISTEMA as selectedindex FROM PDK_PARAMETROS_SISTEMA WHERE PDK_PAR_SIS_ID_PADRE=35")
            Case "Empresa"
                ds = BD.EjecutarQuery("SELECT  A.PDK_EMP_NOMBRE AS selectedvalue, A.PDK_ID_EMPRESA as selectedindex FROM PDK_CAT_EMPRESAS A WHERE A.PDK_EMP_ACTIVO=2")
            Case "Perfil"
                ds = BD.EjecutarQuery("SELECT  A.PDK_PER_NOMBRE AS selectedvalue, A.PDK_ID_PERFIL as selectedindex FROM PDK_PERFIL A where PDK_PER_ACTIVO = 2")
                'RQADM-01:MPUESTO:08/09/2017:Mejoras al Panel de Seguimiento
            Case "Producto1", "Producto", "AddProduct"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("SELECT  A.PDK_PROD_NOMBRE AS selectedvalue, A.PDK_ID_PRODUCTOS  as selectedindex FROM PDK_CAT_PRODUCTOS A INNER JOIN PDK_CAT_EMPRESAS B ON A.PDK_ID_EMPRESA=B.PDK_ID_EMPRESA  INNER JOIN PDK_CAT_MONEDA C ON C.PDK_ID_MONEDA=A.PDK_ID_MONEDA WHERE A.PDK_ID_EMPRESA = " & arraydepende(0) & " AND A.PDK_PROD_ACTIVO=2 ORDER BY A.PDK_ID_PRODUCTOS")
                Else
                    ds = BD.EjecutarQuery("SELECT ''as selectedvalue, ''as selectedindex;")
                End If
                'BUG-PD-301: CGARCIA: 11/12/2017: SE CREA FILTRO PARA EL LLENADO DE COLONIAS Y ESTADOS DE LA PRECALIFICACION
            Case "txtCOLONIA15"
                ds = BD.EjecutarQuery("SELECT DISTINCT C.CPO_DS_COLONIA AS selectedvalue,C.CPO_FL_CP as selectedindex FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE  where C.CPO_FG_STATUS = 2 AND C.CPO_CL_CODPOSTAL =" & arraydepende(0) & " UNION SELECT 'OTRO'[CPO_DS_COLONIA], -1[CPO_FL_CP] ORDER BY [CPO_FL_CP] DESC")
            Case "txtCOLONIA52"
                ds = BD.EjecutarQuery("SELECT DISTINCT C.CPO_DS_COLONIA AS selectedvalue,C.CPO_FL_CP as selectedindex FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE  where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "txtCOLONIA85"
                ds = BD.EjecutarQuery("SELECT C.CPO_DS_COLONIA AS selectedvalue,C.CPO_FL_CP as selectedindex FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE  where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "txtCOLONIA122"
                ds = BD.EjecutarQuery("SELECT C.CPO_DS_COLONIA AS selectedvalue,C.CPO_FL_CP as selectedindex FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE  where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "txtCOLONIA458"
                ds = BD.EjecutarQuery("SELECT C.CPO_DS_COLONIA AS selectedvalue,C.CPO_FL_CP as selectedindex FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE  where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "ProductoPlaz"
                ds = BD.EjecutarQuery("SELECT PDK_PROD_NOMBRE AS selectedvalue,PDK_ID_PRODUCTOS AS selectedindex FROM PDK_CAT_PRODUCTOS where PDK_PROD_ACTIVO = 2")
            Case "Empresa1"
                ds = BD.EjecutarQuery("SELECT  A.PDK_EMP_NOMBRE AS selectedvalue, A.PDK_ID_EMPRESA as selectedindex FROM PDK_CAT_EMPRESAS A WHERE A.PDK_EMP_ACTIVO=2")
            Case "Distribuidor"
                '--- INC-B-1988:JDRA:se le agrega el ltrim ya que el campo se convirtio a entero
                ds = BD.EjecutarQuery("select ltrim(cd.PDK_ID_DISTRIBUIDOR) + '. ' + PDK_DIST_NOMBRE AS selectedvalue,cd.PDK_ID_DISTRIBUIDOR as selectedindex from PDK_CAT_DISTRIBUIDOR cd inner join PDK_REL_USU_DIST rud on cd.PDK_ID_DISTRIBUIDOR = rud.PDK_ID_DISTRIBUIDOR where rud.PDK_ID_USUARIO = " & arraydepende(0) & " and PDK_DIST_ACTIVO = 2 ORDER BY cd.PDK_ID_DISTRIBUIDOR;")
                '--- INC-B-1988:JDRA:se le agrega el ltrim ya que el campo se convirtio a entero
            Case "Persona"
                ds = BD.EjecutarQuery("select  PDK_PER_NOMBRE AS selectedvalue ,PDK_ID_PER_JURIDICA as selectedindex from PDK_CAT_PER_JURIDICA where PDK_PER_ACTIVO = 2;")

            Case "Usuario"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("select distinct usu.PDK_USU_CLAVE AS selectedvalue, bita.PDK_CLAVE_USUARIO as selectedindex from PDK_TAREA_BITACORA bita inner join PDK_USUARIO usu on bita.PDK_CLAVE_USUARIO = usu.PDK_CLAVE_USUARIO where PDK_ID_SOLICITUD = " & arraydepende(0))
                Else
                    ds = BD.EjecutarQuery("SELECT ''as selectedvalue, ''as selectedindex;")
                End If
            Case Else
                ds = BD.EjecutarQuery("SELECT ''as selectedvalue, ''as selectedindex;")

        End Select

        If ds.Tables(0).Rows.Count = 0 Then
            ds = BD.EjecutarQuery("SELECT ''as selectedvalue, ''as selectedindex;")
        End If

        For Each row As DataRow In ds.Tables(0).Rows
            If a = 0 Then
                salida.Add(New ListItem("", ""))
            End If
            salida.Add(New ListItem(row.Item(0).ToString, row.Item(1).ToString))
            a = a + 1
        Next


        Return salida
    End Function
    <WebMethod()>
    Public Function btnImprimirCto(ByVal cadena As String,
                                   ByVal contrato As String,
                                   ByVal cliente As String,
                                   ByVal empresa As String,
                                   ByVal moneda As String,
                                   ByVal toperacion As String,
                                   ByVal pjuridica As String) As String
        Try
            Dim arreglo As Array = Split(cadena, ",")
            Dim dabet As New DataSet
            Dim Dbdat As New DataSet
            Dim arregloPath As String = ""

            Dim objEmpresa As New ProdeskNet.Catalogos.clsEmpresa(1)

            Dim nombreEmpresa As String = objEmpresa.PDK_EMP_NOMBRE

            If nombreEmpresa <> "Incredit" Then
                If pjuridica = 1 Or pjuridica = 2 Then
                    pjuridica = "PF"
                Else
                    pjuridica = "PM"
                End If
            End If

            Dim i As Integer = 0


            If cadena.Length > 0 Then
                dabet = BD.EjecutarQuery("SELECT FMT_FL_CVE, FMT_DS_DESCRIPCION, FMT_DS_MACHOTE, FMT_NO_TIPODOCUMENTO FROM CCTO_MACHOTE WHERE FMT_FL_CVE in(" & cadena & ") ORDER BY FMT_FL_CVE")

                If dabet.Tables.Count > 0 AndAlso dabet.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dabet.Tables(0).Rows.Count - 1
                        Dim strArchivo As String = ""
                        Dim tipoDocumento As Integer = 0
                        Dim objNodo As System.Xml.XmlNode
                        Dim docXML As New XmlDocument
                        Dim tblHash As New Hashtable
                        Dim strCadena As String = ""
                        Dim strLlave As String
                        Dim strValor As String
                        Dim StrDocumento As String = ""


                        strArchivo = dabet.Tables(0).Rows(i).Item("FMT_DS_MACHOTE").ToString
                        tipoDocumento = dabet.Tables(0).Rows(i).Item("FMT_NO_TIPODOCUMENTO").ToString


                        If tipoDocumento = 1 Or tipoDocumento = 4 Then
                            'INC-B-1974:JDRA:Impresion de contratos.
                            If toperacion.Trim = "CA" Then
                                'INC-B-1974:JDRA:Impresion de contratos.
                                Dbdat = BD.EjecutarQuery("EXEC SpLsnetDocumentoContradoCredito '" & contrato & "'," & cliente & "," & empresa & "," & moneda & ",'" & toperacion & "','" & Format(Now(), "yyyy-MM-dd") & "'," & pjuridica)
                            Else
                                Dbdat = BD.EjecutarQuery("EXEC splsnetDocumentoContrato '" & contrato & "'," & cliente & "," & empresa & "," & moneda & ",'" & toperacion & "','" & Format(Now(), "yyyy-MM-dd") & "'," & pjuridica & ",'';")
                            End If

                        End If

                        'Select Case tipoDocumento
                        '    Case 1
                        '        Select Case toperacion
                        '            Case "CA"
                        '                Dbdat = BD.EjecutarQuery("EXEC ProleaseNet.dbo.SpLsnetDocumentoContradoCredito '" & contrato & "'," & cliente & "," & empresa & "," & moneda & ",'" & toperacion & "','" & Format(Now(), "yyyy-MM-dd") & "'," & pjuridica)
                        '            Case Else
                        '                Dbdat = BD.EjecutarQuery("EXEC ProleaseNet.dbo.splsnetDocumentoContrato '" & contrato & "'," & cliente & "," & empresa & "," & moneda & ",'" & toperacion & "','" & Format(Now(), "yyyy-MM-dd") & "'," & pjuridica & ",'';")
                        '        End Select

                        '    Case Else
                        'End Select

                        docXML.LoadXml(Dbdat.GetXml())


                        For Each objNodo In docXML.ChildNodes(0).ChildNodes
                            If Not objNodo.SelectSingleNode("CLAVE") Is Nothing Then
                                strLlave = objNodo.SelectSingleNode("CLAVE").InnerText.Trim
                            Else
                                strLlave = "*"
                            End If

                            If Not objNodo.SelectSingleNode("TEXTO") Is Nothing Then
                                strValor = objNodo.SelectSingleNode("TEXTO").InnerText.Trim
                            Else
                                strValor = objNodo.SelectSingleNode("CLAVE").InnerText.Trim
                            End If
                            tblHash.Add(strLlave, strValor)

                        Next

                        For Each objKey In tblHash.Keys
                            strArchivo = strArchivo.Replace(objKey, tblHash(objKey))
                        Next

                        Dim filename As String = ""
                        Dim Path As String = ""
                        filename = dabet.Tables(0).Rows(i).Item("FMT_FL_CVE") & "_" & StrDocumento & tipoDocumento & "_Contrato " & contrato & ".rtf"
                        filename = filename.Replace("/", "-")
                        Path = Server.MapPath("Documentos\" & filename)
                        Dim sw As System.IO.StreamWriter = New System.IO.StreamWriter(Path)
                        sw.Write(strArchivo)
                        sw.Close()

                        arregloPath += "/Documentos/" & filename & "|"

                        'Dim dataSetIm As New DataSet

                        'dataSetIm = BD.EjecutarQuery("SELECT PAR_CL_VALOR, PAR_DS_DESCRIPCION, PAR_FG_REGDEFAULT FROM ProleaseNet.dbo.CPARAMETRO WHERE PAR_FL_CVE = 76 AND PAR_FG_STATUS = 1 AND PAR_CL_VALOR > 0 AND PAR_CL_VALOR=" & tipoDocumento & "  ORDER BY PAR_FG_REGDEFAULT DESC, PAR_FG_STATUS DESC, PAR_DS_DESCRIPCION ASC")
                        'If dataSetIm.Tables(0).Rows.Count > 0 AndAlso dataSetIm.Tables.Count > 0 Then
                        '    StrDocumento = dataSetIm.Tables(0).Rows(0).Item("PAR_DS_DESCRIPCION").ToString
                        'End If


                        'dataSetIm = BD.EjecutarQuery("SELECT VDC_DS_DOCUMENTO, VDC_NO_CONSEC FROM [ProleaseNet].dbo.KCTO_IMPRESION   WHERE CTO_FL_CVE = '" & contrato & "' AND VDC_CL_DOCUMENTO = " & tipoDocumento)
                        'If dataSetIm.Tables.Count = 0 AndAlso dataSetIm.Tables(0).Rows.Count = 0 Then
                        '    Return "erro: No se ha creado una versión imprimible"
                        '    Exit Function
                        'Else
                        '    Dim filename As String = ""
                        '    Dim Path As String = ""

                        '    With (dataSetIm.Tables(0).Rows(0))
                        '        If Not .Item("VDC_DS_DOCUMENTO") Is Nothing Then
                        '            ''strArchivo = .Item("VDC_DS_DOCUMENTO").ToString
                        '            filename = dabet.Tables(0).Rows(i).Item("FMT_FL_CVE") & "_" & StrDocumento & .Item("VDC_NO_CONSEC").ToString & "_Contrato " & contrato & ".rtf"
                        '            filename = filename.Replace("/", "-")
                        '            Path = Server.MapPath("Documentos\" & filename)                                    
                        '            Dim sw As System.IO.StreamWriter = New System.IO.StreamWriter(Path)
                        '            sw.Write(strArchivo)
                        '            sw.Close()

                        '            arregloPath += "/Documentos/" & filename & "|"


                        '        End If
                        '    End With
                        'End If





                    Next
                End If
            End If

            arregloPath = arregloPath.Substring(0, arregloPath.Length - 1)


            Return arregloPath

            'For Each i In arreglo
            '    Dim idMachote As Integer = arreglo(i)
            '    dabet = BD.EjecutarQuery("SELECT FMT_FL_CVE, FMT_DS_DESCRIPCION, FMT_DS_MACHOTE, FMT_NO_TIPODOCUMENTO FROM CCTO_MACHOTE WHERE FMT_FL_CVE =" & idMachote & " ORDER BY FMT_FL_CVE")
            'Next



        Catch ex As Exception
            Return "Se encontro un error en la transaccion"
        End Try



    End Function
    <WebMethod()>
    Public Function btnInsertarPanta(ByVal cadena As String,
                    ByVal pantalla As String,
                    ByVal solicitud As String,
                    ByVal persona As String,
                    ByVal usuario As String) As String

        Dim countrows As Integer = 0
        Dim ds As DataSet = Nothing
        Dim dataBase As DataSet = Nothing
        Dim dtsGC As DataSet = New DataSet()

        Try

            Dim jsonInfoBasic As String = VerifyIntoBasic(solicitud)

            countrows = BD.ExInsUpd(cadena)

            If countrows > 0 Then
                If pantalla = "PRECALIFICACION" Or pantalla = "COACREDITADO" Then
                    jsonInfoBasic = VerifyIntoBasic(solicitud, jsonInfoBasic)

                    Dim strRFCSolicitante As String = String.Empty
                    Dim strRFCCoacreditado As String = String.Empty
                    Dim valSol As String = ""
                    Dim valCoa As String = ""

                    'BUG-PD-365
                    If pantalla = "PRECALIFICACION" Then
                        Dim objFlujos As New ProdeskNet.Configurcion.clsSolicitudes(0)
                        objFlujos.PDK_ID_SOLICITUD = Val(solicitud)
                        objFlujos.ConsultaSolicitud(7)
                    End If

                    countrows = 0
                    ds = BD.EjecutarQuery("SELECT  ISNULL(A.RFC, '') 'SOLICITANTE', ISNULL(B.RFC,'') 'COACREDITADO' " &
                                " FROM PDK_TAB_SOLICITANTE A " &
                             " LEFT OUTER JOIN PDK_TAB_COACREDITADO_CASO B ON B.PDK_ID_SECCCERO = A.PDK_ID_SECCCERO " &
                             " WHERE A.PDK_ID_SECCCERO = " & solicitud)

                    If ds.Tables.Count > 0 Then

                        strRFCSolicitante = ds.Tables(0).Rows(0).Item(0).ToString
                        strRFCCoacreditado = ds.Tables(0).Rows(0).Item(1).ToString


                        If strRFCSolicitante.Trim.Length > 0 And strRFCCoacreditado.Trim.Length = 0 Then
                            valSol = btnBuro(solicitud, persona, usuario, strRFCSolicitante, 1)
                        ElseIf strRFCCoacreditado.Trim.Length > 0 And strRFCSolicitante.Trim.Length > 0 Then
                            valCoa = btnBuro(solicitud, persona, usuario, strRFCCoacreditado, 2)
                        End If

                        'If strRFCCoacreditado.Trim.Length > 0 Then
                        '    valCoa = btnBuro(solicitud, persona, usuario, strRFCCoacreditado, 2)
                        'End If

                        'Si la consulta se realizo con exito se ejecuta dos veces el spValNegocio

                        If valSol = "Consulta de Buró realizada con éxito" Or valCoa = "Consulta de Buró realizada con éxito" Then
                            'countrows = BD.ExInsUpd(" EXEC spValNegocio  " & solicitud & ",64," & usuario)
                            dataBase = BD.EjecutarQuery(" EXEC spValNegocio " & solicitud & ",64," & usuario)
                            If dataBase.Tables(0).Rows.Count > 0 AndAlso dataBase.Tables.Count > 0 Then
                                Dim strT As String = dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString()
                                'If dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString <> "Las reglas de negocio se corrieron exitosamente" Then
                                If ((strT <> "Las reglas de negocio se corrieron exitosamente") And (strT <> "Tarea Exitosa")) Then
                                    valSol = dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString
                                    Return valSol
                                End If

                            End If

                            ' Si la respuesta de la consulta a buro regresa un mensaje de error muestra el mensaje y sale de la funcion
                            'Obtiene idcte externo
                            Dim objSolicitante As ProdeskNet.SN.clsCteIndeseable = New ProdeskNet.SN.clsCteIndeseable()
                            Dim objGetCustomer As ProdeskNet.WCF.clsGetCustomer = New ProdeskNet.WCF.clsGetCustomer()

                            dtsGC = objSolicitante.ConsultaGetCustomer(solicitud)

                            objGetCustomer.CustomerName = dtsGC.Tables(0).Rows(0).Item("NOMBRE1")
                            objGetCustomer.CustomerLastName = dtsGC.Tables(0).Rows(0).Item("APELLIDO_PATERNO")
                            objGetCustomer.CustomerMotherLastName = dtsGC.Tables(0).Rows(0).Item("APELLIDO_MATERNO")
                            objGetCustomer.FederalTaxpayerRegistry = dtsGC.Tables(0).Rows(0).Item("RFC")
                            objGetCustomer.Homonimia = dtsGC.Tables(0).Rows(0).Item("HOMOCLAVE")
                            objGetCustomer.PostalCode = dtsGC.Tables(0).Rows(0).Item("CP")
                            objGetCustomer.BirthDate = dtsGC.Tables(0).Rows(0).Item("FECHA_NAC")
                            objGetCustomer.GetCustomer(solicitud)

                        Else

                            'Return valSol
                            If valSol <> "" Then
                                Return "ERROR: " & valSol
                            Else
                                Return "ERROR: " & valCoa
                            End If

                            'Exit Function

                        End If

                    End If

                    ' Se cambia de posicion debido a que pasa dos veces el spvalnegocio y siempre pasa a la siguiente tarea aunque el buro no traiga infromacion d@ve.

                    If Not valSol.Contains("ERROR") And Not valSol.Contains("CONDICION") Then
                        dataBase = BD.EjecutarQuery(" EXEC spValNegocio  " & solicitud & ",64," & usuario)
                    End If

                    If dataBase.Tables(0).Rows.Count > 0 AndAlso dataBase.Tables.Count > 0 Then
                        Dim str As String = dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString()
                        'If dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString <> "Las reglas de negocio se corrieron exitosamente" Then
                        If ((str <> "Las reglas de negocio se corrieron exitosamente") And (str <> "Tarea Exitosa")) Then
                            Return dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString
                        Else
                            Return "Registro guardado exitosamente."
                        End If

                    End If

                Else
                    dataBase = BD.EjecutarQuery("select PDK_MENSAJE_TAREA as MENSAJE from tbmensajeTarea a inner join (select pdk_id_secccero, max(idtbmt) maxidtbmt from tbmensajeTarea	group by pdk_id_secccero) b on a.idtbmt = b.maxidtbmt and a.PDK_ID_SECCCERO = b.PDK_ID_SECCCERO where a.pdk_id_secccero = " & solicitud & ";")
                    If dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString.Length > 0 Then
                        Return dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString
                    Else
                        Return "Registro guardado exitosamente."
                    End If
                End If
            Else
                Return "Error al actualizar la informacion en pantalla."
            End If

        Catch ex As Exception
            Return "ERROR: " & ex.Message.ToString
        End Try

    End Function
    <WebMethod()>
    Public Function btnInsUpd(ByVal cadena As String) As String
        Dim countrows As Integer = BD.ExInsUpd(cadena)
        If countrows > 0 Then
            Return "Registro actualizado exitosamente."
        Else
            Return "Existio un error al actualizar la Información."
        End If
    End Function
    <WebMethod()>
    Public Function btnInsertDocumento(ByVal cadena As String) As String
        Dim dataBase As DataSet = Nothing

        Try
            dataBase = BD.EjecutarQuery(cadena)
            Return dataBase.Tables(0).Rows(0)(0).ToString
        Catch ex As Exception
            Return "Error al cargar la informacion" & ex.Message
        End Try

    End Function
    <WebMethod()>
    Public Function btnManejoMensaje(ByVal cadena As String, ByVal cadena2 As String) As String
        Dim dsresult As New DataSet
        Try
            If cadena <> "" Then
                dsresult = BD.EjecutarQuery(cadena)
                If dsresult.Tables.Count > 0 AndAlso dsresult.Tables(0).Rows.Count > 0 Then
                    If dsresult.Tables(0).Rows(0).Item("MENSAJE") <> "" Then
                        Return dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
                    Else
                        'Dim countrows As Integer = BD.ExInsUpd(cadena2)
                        Dim ds2 As New DataSet
                        ds2 = BD.EjecutarQuery(cadena2)
                        If ds2.Tables.Count > 0 AndAlso ds2.Tables(0).Rows.Count > 0 Then
                            If IsDBNull(ds2) Then
                                Return "Tarea Exitosa"
                            Else
                                If IsDBNull(ds2.Tables(0).Rows(0).Item("MENSAJE")) Then
                                    Return "Tarea Exitosa"
1:                              Else
                                    Dim mensaje As String = ds2.Tables(0).Rows(0).Item("MENSAJE")
                                    If mensaje.Contains("Error") Then
                                        Return mensaje
                                    Else
                                        Return "Tarea Exitosa"
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Else
                Dim countrows As Integer = BD.ExInsUpd(cadena2)
                If countrows > 0 Then
                    Return "Tarea Exitosa"
                Else
                    Return "existio un error al insertar el registro."
                End If

            End If

        Catch ex As Exception
            dsresult = BD.EjecutarQuery(cadena)
        End Try

    End Function
    <WebMethod()>
    Public Function fnValida(ByVal cadena As String)
        Dim ds As New DataSet
        ds = BD.EjecutarQuery(cadena)
        If Not IsDBNull(ds) Then
            Return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented)
        End If
    End Function

    'BUG-PD-99:MPUESTO:15/06/2017:
    <WebMethod()>
    Public Function fillMultiTab(ByVal valor As String, ByVal depende As String, ByVal enable As String)
        Dim ds As New DataSet
        Dim arraydepende As Array

        arraydepende = depende.Split(",")

        Select Case valor
            Case "#tbFacturaVehiculo, #tbFacturaAccesorios, #tbFacturaIntangibles, #tbFacturaComer"
                ds = BD.EjecutarQuery("exec sp_fillAllActivos " & arraydepende(0) & IIf(enable = "", "", ", " & enable))
        End Select

        If Not IsDBNull(ds) Then
            Return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented)
        End If
    End Function


    'ERODRIGUEZ: Se duplico la funcion btnInsertar para permitir cancelar al estar equivacados los parametros que se envian en tipo y orden
    <WebMethod()>
    Public Function btnInsertar_dos(
                    ByVal cadena As String,
                    ByVal pantalla As String,
                    ByVal solicitud As String,
                    ByVal persona As String,
                    ByVal usuario As String,
        ByVal cveusuario As String,
        ByVal paswword As String,
        ByVal idpantalla As String,
        ByVal strStatus As String,
        ByVal intbandera As String,
        ByVal strMotivo As String) As String


        Dim countrows As Integer
        Try
            If intbandera = "1" Or intbandera = "2" Then
                Dim ds As DataSet = Nothing
                Dim cad As String = "EXEC sp_ValidacionUsuario " & idpantalla & ",'" & cveusuario & "', '" & paswword & "'," & solicitud & "," & usuario & ", " & strStatus & "," & intbandera & ",'" & strMotivo & "'"
                ds = BD.EjecutarQuery("EXEC sp_ValidacionUsuario " & idpantalla & ",'" & cveusuario & "', '" & paswword & "'," & solicitud & "," & usuario & ", " & strStatus & "," & intbandera & ",'" & strMotivo & "'")
                If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("MENSAJE").ToString = "Registro guardado exitosamente" Then
                        If pantalla <> "DOCUMENTOS" And pantalla <> "ENTREVISTA" Then
                            countrows = BD.ExInsUpd(cadena)
                        Else
                            Return ds.Tables(0).Rows(0).Item("MENSAJE").ToString
                        End If
                        'Se ejecuta sp para buscar solicitudes para asignar al usuario RQ-PI7-PD13-3
                        Dim datainicio As DataSet = ProdeskNet.Seguridad.clsUsuario.AsignaUsuarioSol(usuario)
                    Else
                        Return ds.Tables(0).Rows(0).Item("MENSAJE").ToString
                    End If
                End If
            Else
                countrows = BD.ExInsUpd(cadena)
            End If

        Catch ex As Exception
            Return "Se encontro un error en la transaccion"
        End Try


        If countrows > 0 Then
            Return "Registro guardado exitosamente."
        Else
            Return "Se encontro un error en la transaccion"
        End If

    End Function


    <WebMethod()>
    Public Function btnInsertar(
                    ByVal cadena As String,
                    ByVal pantalla As String,
                    ByVal solicitud As String,
                    ByVal persona As String,
                    ByVal usuario As String,
        ByVal cveusuario As String,
        ByVal paswword As String,
        ByVal idpantalla As String,
        ByVal strStatus As String,
        ByVal intbandera As String,
        ByVal strMotivo As String) As String


        Dim countrows As Integer
        Try
            If intbandera = "1" Or intbandera = "2" Then
                Dim ds As DataSet = Nothing
                ds = BD.EjecutarQuery("EXEC sp_ValidacionUsuario " & idpantalla & "," & cveusuario & ", " & paswword & "," & solicitud & "," & usuario & ", " & strStatus & "," & intbandera & ",'" & strMotivo & "'")
                If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("MENSAJE").ToString = "Registro guardado exitosamente" Then
                        If pantalla <> "DOCUMENTOS" And pantalla <> "ENTREVISTA" Then
                            countrows = BD.ExInsUpd(cadena)
                        Else
                            Return ds.Tables(0).Rows(0).Item("MENSAJE").ToString
                        End If
                        'Se ejecuta sp para buscar solicitudes para asignar al usuario RQ-PI7-PD13-3
                        Dim datainicio As DataSet = ProdeskNet.Seguridad.clsUsuario.AsignaUsuarioSol(usuario)

                    Else
                        Return ds.Tables(0).Rows(0).Item("MENSAJE").ToString
                    End If
                End If
            Else
                countrows = BD.ExInsUpd(cadena)
            End If

        Catch ex As Exception
            Return "Se encontro un error en la transaccion"
        End Try


        If countrows > 0 Then
            Return "Registro guardado exitosamente."
        Else
            Return "Se encontro un error en la transaccion"
        End If

    End Function

    Private Function btnBuro(
                            ByVal intSolicitud As Integer,
                            ByVal intPersona As Integer,
                            ByVal intUsuario As Integer,
                            ByVal strRFC As String,
                            ByVal intTipoPersona As Integer) As String

        Dim objBuro As New clsBuroINTL
        Try

            Dim bolBuro As Boolean = False
            With objBuro
                .PDK_BURO_SOLICITUD = intSolicitud
                .PDK_BURO_PERSONA = 1
                .PDK_BURO_USUARIO = intUsuario
                .PDK_RFC = strRFC
                .pdk_tipo_persona = intTipoPersona
            End With
            bolBuro = objBuro.PDK_BURO_OBTENBURO

            If bolBuro = True Then
                Return "Consulta de Buró realizada con éxito"
            Else
                Return objBuro.mystrMensajeError
            End If
        Catch ex As Exception
            Return "Error al obtener Buró de Crédito"
        Finally
            objBuro = Nothing
        End Try

    End Function

    'BUG-PD-288: Añade validacion consulta de municipio en la opcion case "DELEGA_O_MUNI17"
    <WebMethod()>
    Public Function validaScoring(ByVal cadena As String) As String
        Dim ds As New DataSet
        ds = BD.EjecutarQuery(cadena)
        Return ds.Tables(0).Rows(0)(0).ToString
    End Function
    <WebMethod()>
    Public Function filltxt(ByVal valor As String, ByVal depende As String) As String

        Dim ds As New DataSet
        Dim text As String = ""
        Dim arraydepende As Array

        arraydepende = depende.Split(",")

        Select Case valor
            Case "Valor1"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("select RN_NO_VALOR1 from reglas_negocio where rn_id = " & arraydepende(0) & ";")
                End If
            Case "Valor2"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("select RN_NO_VALOR2 from reglas_negocio where rn_id = " & arraydepende(0) & ";")
                End If
            Case "Condicion1"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("select RN_CONDICION_REGLADNEGOCIO from reglas_negocio rn inner join RN_CONDICION rnc on rn.RN_ID_CONDICION = rnc.RN_ID_CONDICION where rn_id = " & arraydepende(0) & ";")
                End If
                'INC-B-2019:JDRA:Regresar
            Case "tags"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("declare @nombres varchar(max) = ''; exec sp_LlenaDDLSolCli '" & arraydepende(0) & "', @nombres output; select @nombres;")
                End If
            Case "txtDistribuidor"
                '--- INC-B-1988:JDRA:se le agrega el ltrim ya que el campo se convirtio a entero
                ds = BD.EjecutarQuery("declare @dist varchar(max) = ''; select @dist = @dist + ltrim(PDK_DIST_CLAVE) + '. ' + PDK_DIST_NOMBRE + ', ' from PDK_CAT_DISTRIBUIDOR; select @dist;")
                '--- INC-B-1988:JDRA:se le agrega el ltrim ya que el campo se convirtio a entero
            Case "txtUsuAsign"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("exec sp_cargaUsuario '" & arraydepende(0) & "', '" & arraydepende(1) & "';")
                Else
                    ds = BD.EjecutarQuery("exec sp_cargaUsuario '" & arraydepende(0) & "', 0;")
                End If
            Case "XML"
                ds = BD.EjecutarQuery(" EXEC REPORTESXMLPANTALLA " & arraydepende(0) & "," & arraydepende(1) & ",1")
            Case "XSL"
                ds = BD.EjecutarQuery(" EXEC REPORTESXMLPANTALLA " & arraydepende(0) & "," & arraydepende(1) & ",0")
            Case "lblNoSolicitudes"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("select count(distinct pdk_id_solicitud) from PDK_OPE_SOLICITUD where PDK_OPE_USU_ASIGNADO = " & arraydepende(0) & " and PDK_OPE_STATUS_TAREA = 39")
                End If
                'BUG-PD-288: Añade validacion consulta de municipio en la opcion case "DELEGA_O_MUNI17"
            Case "DELEGA_O_MUNI17" 'BUG-PC-165: CGARCIA: 05/03/2018: SE CAMBIA CONSULTA DE MUNICIPIO 
                ds = BD.EjecutarQuery("SELECT TOP 1 M.MUN_DS_MUNICIPIO FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON C.CIU_CL_CIUDAD = CI.CIU_CL_CIUDAD AND C.EFD_CL_CVE = CI.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON C.MUN_CL_CVE=M.MUN_CL_CVE AND C.EFD_CL_CVE=M.EFD_CL_CVE AND C.CIU_CL_CIUDAD = M.CIU_CL_CIUDAD AND M.MUN_FG_STATUS = 2 WHERE C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "ESTADO18"
                ds = BD.EjecutarQuery("SELECT top 1 E.EFD_DS_ENTIDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
                'BUG-PD-301: CGARCIA: 11/12/2017: SE CREA FILTRO PARA EL LLENADO DE COLONIAS Y ESTADOS DE LA PRECALIFICACION
            Case "CIUDAD16"
                ds = BD.EjecutarQuery("SELECT top 1 CI.CIU_NB_CIUDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where CI.CIU_FG_STATUS = 2 AND C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "DELEGA_O_MUNICI54"
                ds = BD.EjecutarQuery("SELECT TOP 1 M.MUN_DS_MUNICIPIO FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON C.CIU_CL_CIUDAD = CI.CIU_CL_CIUDAD AND C.EFD_CL_CVE = CI.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON C.MUN_CL_CVE=M.MUN_CL_CVE AND C.EFD_CL_CVE=M.EFD_CL_CVE AND C.CIU_CL_CIUDAD = M.CIU_CL_CIUDAD AND M.MUN_FG_STATUS = 2 WHERE C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "ESTADO51"
                ds = BD.EjecutarQuery("SELECT top 1 E.EFD_DS_ENTIDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "CIUDAD53"
                ds = BD.EjecutarQuery("SELECT top 1 CI.CIU_NB_CIUDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "DELEGA_O_MUNI87"
                ds = BD.EjecutarQuery("SELECT TOP 1 M.MUN_DS_MUNICIPIO FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON C.CIU_CL_CIUDAD = CI.CIU_CL_CIUDAD AND C.EFD_CL_CVE = CI.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON C.MUN_CL_CVE=M.MUN_CL_CVE AND C.EFD_CL_CVE=M.EFD_CL_CVE AND C.CIU_CL_CIUDAD = M.CIU_CL_CIUDAD AND M.MUN_FG_STATUS = 2 WHERE C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "ESTADO88"
                ds = BD.EjecutarQuery("SELECT top 1 E.EFD_DS_ENTIDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "CIUDAD86"
                ds = BD.EjecutarQuery("SELECT top 1 CI.CIU_NB_CIUDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "DELEGA_O_MUNI124"
                ds = BD.EjecutarQuery("SELECT TOP 1 M.MUN_DS_MUNICIPIO FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON C.CIU_CL_CIUDAD = CI.CIU_CL_CIUDAD AND C.EFD_CL_CVE = CI.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON C.MUN_CL_CVE=M.MUN_CL_CVE AND C.EFD_CL_CVE=M.EFD_CL_CVE AND C.CIU_CL_CIUDAD = M.CIU_CL_CIUDAD AND M.MUN_FG_STATUS = 2 WHERE C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "ESTADO120"
                ds = BD.EjecutarQuery("SELECT top 1 E.EFD_DS_ENTIDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "CIUDAD123"
                ds = BD.EjecutarQuery("SELECT top 1 CI.CIU_NB_CIUDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "DELEGA_O_MUNI459"
                ds = BD.EjecutarQuery("SELECT TOP 1 M.MUN_DS_MUNICIPIO FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON C.CIU_CL_CIUDAD = CI.CIU_CL_CIUDAD AND C.EFD_CL_CVE = CI.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON C.MUN_CL_CVE=M.MUN_CL_CVE AND C.EFD_CL_CVE=M.EFD_CL_CVE AND C.CIU_CL_CIUDAD = M.CIU_CL_CIUDAD AND M.MUN_FG_STATUS = 2 WHERE C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "ESTADO460"
                ds = BD.EjecutarQuery("SELECT top 1 E.EFD_DS_ENTIDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "CIUDAD461"
                ds = BD.EjecutarQuery("SELECT top 1 CI.CIU_NB_CIUDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case Else
                ds = BD.EjecutarQuery("SELECT ''")
        End Select

        If ds.Tables.Count = 0 Then
            ds = BD.EjecutarQuery("SELECT ''")
        Else
            If ds.Tables(0).Rows.Count = 0 Then
                ds = BD.EjecutarQuery("SELECT ''")
            End If
        End If

        Return ds.Tables(0).Rows(0)(0).ToString

    End Function

    Function validaDepende(ByVal arraydepende As Array) As Boolean
        For Each arra In arraydepende
            If arra.ToString = "" Then
                Return False
            End If
        Next
        Return True
    End Function

    <WebMethod()>
    Function fnFillJSON(ByVal valor As String, ByVal depende As String) As Object
        Dim ds As New DataSet
        Dim text As String = ""
        Dim arraydepende As Array

        arraydepende = depende.Split(",")

        Select Case valor
            Case "grvConsulta"

                Dim tam As Integer = arraydepende.Length
                If tam = 1 Then
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl 'usuario', '" & arraydepende(0) & "';")
                ElseIf tam = 4 Then
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl 'usuario', '" & arraydepende(0) & "', '','', " & arraydepende(1) & "," & arraydepende(2) & "," & arraydepende(3) & ";")
                ElseIf tam = 5 Then
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl 'usuario', '" & arraydepende(0) & "', '','', " & arraydepende(1) & "," & arraydepende(2) & "," & arraydepende(3) & ", " & arraydepende(4) & ";")
                ElseIf tam = 7 Then
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl '" & arraydepende(0) & "', '" & arraydepende(1) & "', '" & arraydepende(2) & "', '" & arraydepende(3) & "',  " & arraydepende(4) & "," & arraydepende(5) & "," & arraydepende(6) & ";")
                End If
        End Select

        Return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented)

    End Function

    <WebMethod()>
    Public Function newFillgv(ByVal objeto As String, ByVal dependeObjeto As String, ByVal dependeValor As String) As String
        Dim ds As New DataSet

        Dim arrayObjeto As Array
        Dim arrayValor As Array

        arrayObjeto = dependeObjeto.Split(",")
        arrayValor = dependeValor.Split(",")

        'Dim objetoStr As String = ""
        'Dim ValorStr As String = ""
        'Dim count As Integer = 0

        Select Case objeto
            Case "tbFacturaVehiculo"
                ds = BD.EjecutaStoredProcedureCP("sp_fillTablaActivos", arrayObjeto, arrayValor)
            Case "tbValidarObjetos"
                ds = BD.EjecutaStoredProcedureCP("sp_PantallaValidaDocumentos", arrayObjeto, arrayValor)
            Case "tbObjetos"
                If validaDepende(arrayObjeto) Then
                    ds = BD.EjecutarQuery("exec sp_AsignaReglasNegocio " & arrayObjeto(0))
                Else
                    ds = BD.EjecutarQuery("exec sp_AsignaReglasNegocio 0")
                End If
                'BBV-P-423 RQCONYFOR-01: AVH
            Case "tbFacturaAccesorios"
                ds = BD.EjecutarQuery("exec sp_fillTablaActivosAccesorios " & arrayValor(0) & ", '" & arrayValor(1) & "','" & arrayValor(2) & "'," & arrayValor(3) & ";")
            Case "tbFacturaIntangibles"
                ds = BD.EjecutarQuery("exec sp_fillTablaActivosIntangibles " & arrayValor(0) & ", '" & arrayValor(1) & "','" & arrayValor(2) & "'," & arrayValor(3) & ";")
            Case "tbFacturaComer"
                'ds = BD.EjecutarQuery("exec sp_fillTablaActivosComer " & arrayValor(0) & ", " & arrayValor(1) & ",'" & arrayValor(2) & "'," & arrayValor(3) & ";")
                ds = BD.EjecutaStoredProcedureCP("sp_fillTablaActivosComer", arrayObjeto, arrayValor)


            Case "grvConsulta"

                Dim tam As Integer = arrayObjeto.Length
                If tam = 1 Then
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl 'usuario', '" & arrayValor(0) & "';")
                ElseIf tam = 2 Then
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl 'usuario', '" & arrayValor(0) & "', '','',0,0,0,0,0,0, " & arrayValor(1) & ";") 'AVH
                ElseIf tam = 4 Then
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl 'usuario', '" & arrayValor(0) & "', '','', " & arrayValor(1) & "," & arrayValor(2) & "," & arrayValor(3) & ";")
                ElseIf tam = 5 Then
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl 'usuario', '" & arrayValor(0) & "', '','', " & arrayValor(1) & "," & arrayValor(2) & "," & arrayValor(3) & ", " & arrayValor(4) & ";")
                ElseIf tam = 7 Then
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl '" & arrayValor(0) & "', '" & arrayValor(1) & "', '" & arrayValor(2) & "', '" & arrayValor(3) & "',  " & arrayValor(4) & "," & arrayValor(5) & "," & arrayValor(6) & ";")
                End If

        End Select


        Return ds.Tables(0).Rows(0)(0).ToString

    End Function

    <WebMethod()>
    Public Function fnOcultaObjetos(ByVal pantalla As String, ByVal perfil As String) As String
        Dim ds As New DataSet

        ds = BD.EjecutarQuery("exec spOcultaObjetos '" & pantalla & "', " & perfil)

        Return ds.Tables(0).Rows(0)(0).ToString

    End Function

    <WebMethod()>
    Public Function fillgv(ByVal valor As String, ByVal depende As String) As String

        Dim ds As New DataSet
        Dim text As String = ""
        Dim arraydepende As Array

        arraydepende = depende.Split(",")

        Select Case valor
            Case "dlCliente"
                ds = BD.EjecutarQuery("exec spFillPersona '" & arraydepende(0) & "';")
            Case "tbFacturaVehiculo"
                ds = BD.EjecutarQuery("exec sp_fillTablaActivos " & arraydepende(0) & ", '" & arraydepende(1) & "', '" & arraydepende(2) & "', '" & arraydepende(3) & "', '" & arraydepende(4) & "', " & arraydepende(5))
            Case "tbFacturaAccesorios"
                ds = BD.EjecutarQuery("exec sp_fillTablaActivosAccesorios " & arraydepende(0) & ", '" & arraydepende(1) & "', '" & arraydepende(2) & "', " & arraydepende(3))
            Case "tbFacturaIntangibles"
                ds = BD.EjecutarQuery("exec sp_fillTablaActivosIntangibles " & arraydepende(0) & ", '" & arraydepende(1) & "', '" & arraydepende(2) & "', " & arraydepende(3))
            Case "tbFacturaComer"
                ds = BD.EjecutarQuery("exec sp_fillTablaActivosComer " & arraydepende(0) & ", '" & arraydepende(1) & "', '" & arraydepende(2) & "', " & arraydepende(3))
            Case "tbNvaSol"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("exec sp_addSolicitud " & arraydepende(0))
                End If
            Case "tbValidarObjetos"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("exec sp_PantallaValidaDocumentos " & arraydepende(2) & ", " & arraydepende(1) & ", " & arraydepende(0))
                End If
            Case "tb2"
                ds = BD.EjecutarQuery("declare @cadena varchar(max) = '<tr><th>Id</th><th>Regla de Negocio</th><th>Condicion</th><th>Valor 1</th><th>Valor 2</th><th>Rechazo</th></tr>', @condicion varchar(max) = '', @rechazos varchar(max) = ''; select @condicion = @condicion + '<option value = ' + ltrim(rn_id_condicion) + '>' + RN_CONDICION_REGLADNEGOCIO + '</option>' from RN_CONDICION; select @rechazos = @rechazos + '<option value = ' + ltrim(PDK_ID_CAT_RECHAZOS) + '>' + PDK_REC_NOMBRE + '</option>' from PDK_CAT_RECHAZOS; select @cadena = @cadena + '<tr><td><Label id = ""lblId' + ltrim(RN_ID) + '"" </Label>' + ltrim(RN_ID) + '</td><td><Label id = ""lblRNNom' + ltrim(RN_ID) + '"" onmousedown = ""EditaTXT(this, ' + ltrim(RN_ID) + ')"">' + isnull(RN_NOM_REGLADNEGOCIO, '') + '<Label></td><td><select id = ""ddlRN' + ltrim(RN_ID) + '"" class = ""Text"" onchange = ""btnActualizar(this, ' + ltrim(RN_ID) + ')"">' + replace(@condicion, '<option value = ' + ltrim(rn.RN_ID_CONDICION) + '>' + RN_CONDICION_REGLADNEGOCIO + '</option>', '<option value = ' + ltrim(rn.RN_ID_CONDICION) + '"" selected = ""selected"">' + RN_CONDICION_REGLADNEGOCIO + '</option>') + '</select></td><td><Label id = ""lblVal1' + ltrim(RN_ID) + '"" onmousedown = ""EditaTXT(this, ' + ltrim(RN_ID) + ')"">' + isnull(RN_NO_VALOR1,'0') + '</Label></td><td><Label id = ""lblVal2' + ltrim(RN_ID) + '"" onmousedown = ""EditaTXT(this, ' + ltrim(RN_ID) + ')"">' + isnull(RN_NO_VALOR2,'0') + '</Label></td><td><select id = ""ddlRechazos' + ltrim(RN_ID) + '"" class = ""Text"" onchange = ""btnActualizar(this, ' + ltrim(RN_ID) + ')"">' + replace(@rechazos, '<option value = ' + ltrim(pcr.PDK_ID_CAT_RECHAZOS) + '>' + PDK_REC_NOMBRE + '</option>', '<option value = ' + ltrim(pcr.PDK_ID_CAT_RECHAZOS) + '"" selected = ""selected"">' + PDK_REC_NOMBRE + '</option>') + '</select></td></tr>' from reglas_negocio rn inner join RN_CONDICION rnc on rn.RN_ID_CONDICION = rnc.RN_ID_CONDICION inner join PDK_CAT_RECHAZOS pcr on rn.PDK_ID_CAT_RECHAZOS = pcr.PDK_ID_CAT_RECHAZOS; select @cadena;")
            Case "tbObjetos"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("exec sp_AsignaReglasNegocio " & arraydepende(0))
                Else
                    ds = BD.EjecutarQuery("exec sp_AsignaReglasNegocio 0")
                End If
            Case "TbseccionDat"
                If validaDepende(arraydepende) Then
                    If arraydepende(0) = 36 Then
                        ds = BD.EjecutarQuery("declare @cadena varchar(max)=''; if exists (select * from PDK_SCORING )begin select @cadena='<tr><td><div style=""overflow:auto;""><table  class = ""resulGrid"" style = ""height:150px"">';select @cadena=@cadena+'<tr><td><label id=""txt_'+LTRIM(a.PDK_ID_SECCION_DATO)+ '"" onmousedown=""Operador(id)"" class=""link"">'+b.PDK_SEC_DAT_NOMBRE +'</label></td></tr>' from PDK_SCORING a inner join PDK_SECCION_DATO b on a.PDK_ID_SECCION_DATO=b.PDK_ID_SECCION_DATO group by a.PDK_ID_SECCION_DATO , b.PDK_SEC_DAT_NOMBRE; select @cadena=@cadena+'</table></div></td><td valign=""middle""><input id=""cmbRegresar"" value=""<<"" type=""button"" class=""TextLink"" onclick=""fnBtnBorrar()""  runat=""server""/></td><td  valign=""middle""><table colspan=""3"" width = ""100%"" style = ""height:150px""><tr><td valign=""middle""></td><td valign=""middle""><input id=""suma"" value=""+""  class=""TextLink ""  type=""button"" onclick=""Operadores(id)"" /><input id=""resta"" value=""-""  class=""TextLink"" type=""button"" onclick=""Operadores(id)"" /><input id=""division"" value=""/""  class=""TextLink"" type=""button"" onclick=""Operadores(id)"" /><input id=""multipli"" value=""*""  class=""TextLink"" type=""button"" onclick=""Operadores(id)"" /><input id=""paren1"" value=""(""  class=""TextLink"" type=""button"" onclick=""Operadores(id)"" /><input id=""paren2"" value="")""  class=""TextLink"" type=""button"" onclick=""Operadores(id)""/></td></tr><tr><td></td><td class=""camposFor"">Formula:</td></tr><tr><td valign=""middle"" class=""camposForGran"">PI=(</td><td><table><tr><td colspan=""1"" class=""camposForLetra"">1</td></tr><tr><td valing=""middle"" colspan=""1"">________________________________________________</td></tr><tr><td class=""camposForLetra"">1 + e -<input id=""txtOperacion"" type=""text"" class=""resul"" style=""height:38px; width:300px; font-family: Verdana, Helvetica, Arial, sans-serif; overflow:auto; "" readonly=""readonly"" runat=""server"" /></td></tr></table></td><td valign=""middle"" class=""camposForGran"">)*100</td></tr><tr><td></td><td class=""camposFor"">Nombre:</td></tr><tr><td></td><td><input id=""txtNomFormula"" class=""resul"" onkeypress=""ManejaCar(''A'',1,this.value,this)""  style="" width:310px; font-family: Verdana, Helvetica, Arial, sans-serif;""  type=""text""/></td></tr><tr><td></td><td><input id = ""txtOperanum"" type = ""text"" style = ""display:none;"" /></td><td valign=""middle""><input id=""BttAgregar"" value=""Guardar""   type=""button"" class=""Text"" onClick=""bntGuardar()"" runat=""server""/></td></tr></table></td></tr>';select @cadena=@cadena+'<tr><td colspan = ""6"" style= ""text-align:center"">Formula:<label id=""blbformula"" class=""link"" onmousedown=""fnmostrar()"">+</label><input id=""lblcve"" value=""0"" style=""display:none"" /></td></tr><tr id=""trFormula"" style = ""display:none""><td colspan=""6""><div style = ""width:100%; height:100px; overflow:auto;""><table id=""tb1formula"" style = ""width:100%;""  class = ""resulGrid"" >';select @cadena=@cadena +'<tr><th>Id</th><th>formula</th></tr>'; select @cadena=@cadena +'<tr><td><label id=""lbl_'+ ltrim(PDK_ID_FORMULA) +'"" onmousedown=""fnActualiza(this, '+ ltrim(PDK_ID_FORMULA) + ',''' + PDK_FORMULA_DATOS + ''',''' + replace(replace(replace(replace(replace(replace(PDK_FORMULA_GENERADA,'+','[+]'),'-','[-]'),'*','[*]'),'/','[/]'),'(','[(]'),')','[)]') + ''',''' + PDK_FORMULA_NOMBRE + ''')"">'+ ltrim(PDK_ID_FORMULA)+'</label></td><td><label id=""lbldat_'+ltrim(PDK_ID_FORMULA)+'"" >'+PDK_FORMULA_DATOS+'</label></td></tr>' from PDK_FORMULA WHERE PDK_TIPO_FORMULA=" & arraydepende(0) & "; select @cadena=@cadena+  '</table></div></td></tr>';select @cadena;  end  else  begin select @cadena; end")
                    Else
                        ds = BD.EjecutarQuery("exec sp_GeneraFormulaASP " & arraydepende(0))
                    End If
                Else
                    ds = BD.EjecutarQuery("select '';")
                End If

                'ds = BD.EjecutarQuery("declare @cadena varchar(max)='<tr><td><table>';SELECT @cadena=@cadena + '<tr><td><label id=""txt_' + convert(varchar,PDK_ID_SECCION_DATO) + '"" onmousedown=""Operador(id)"" class=""link"">'+ PDK_SEC_DAT_NOMBRE+'</label></td>' FROM PDK_SECCION_DATO WHERE PDK_ID_SECCION =" & arraydepende(0) & " AND PDK_SEC_DAT_STATUS =2 AND (PDK_ID_TIPO_OBJETO <>1 AND PDK_ID_TIPO_OBJETO <>3) AND PDK_SEC_DAT_LLAVE =0;select @cadena=@cadena+'</td></tr></table></td><td valign=""middle""><input id=""cmbRegresar"" value=""<<"" type=""button"" class=""TextLink"" onclick=""regreso()""  runat=""server""/><td valign=""middle""><input id=""txtOperacion"" type=""text"" style=""height:50px; width:200px; overflow:auto; "" readonly=""readonly"" runat=""server"" /><input id=""txtcve"" type=""text"" style =""display:none"" /></td><td valign=""middle""><input id=""BttAgregar"" value=""Guardar""  type=""button"" class=""Text"" runat=""server""/></tr>';select @cadena;")
            Case "CrearFormula"
                ds = BD.EjecutarQuery("exec sp_fillgv 3;")
            Case "grvseccion"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("declare @cadena varchar(max)=''; if exists (select * from PDK_SCORING )begin select @cadena='<tr><th>Id</th><th>Nombre</th><th>Valor 1</th><th>Valor 2</th><th>Peso</th></tr>';SELECT @cadena=@cadena +'<tr><td><label id=""lbl_'+ltrim(A.PDK_ID_SCORING)+'"">'+ltrim(A.PDK_ID_SCORING)+'</labe></td><td><label id=""lbl_'+ltrim(A.PDK_ID_SCORING)+B.PDK_SEC_DAT_NOMBRE+'"">'+B.PDK_SEC_DAT_NOMBRE+'</label></td><td><label id=""lbl_v1'+ltrim(A.PDK_ID_SCORING)+'"" onmousedown=""EditaTXT(this,' + ltrim(A.PDK_ID_SCORING) + ',' + ltrim(A.PDK_ID_TIPO_OBJETO) + ')"">'+ISNULL(A.PDK_SCORING_VALOR1,'')+'</label></td><td><label id=""lbl_v2'+ltrim(A.PDK_ID_SCORING)+'"" onmousedown=""EditaTXT(this,' + ltrim(A.PDK_ID_SCORING) + ',' + ltrim(A.PDK_ID_TIPO_OBJETO) + ')"">'+A.PDK_SCORING_VALOR2+'</label></td><td><label id=""lbl_pe'+ltrim(A.PDK_ID_SCORING)+'"" onmousedown=""EditaTXT(this, ' + ltrim(A.PDK_ID_SCORING) + ',30)"">'+ltrim(A.PDK_SCORING_PESO)+'</label></td></tr>' FROM PDK_SCORING A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO =B.PDK_ID_SECCION_DATO  WHERE A.PDK_ID_SECCION_DATO= " & arraydepende(0) & ";select @cadena; end else begin select @cadena; end")
                Else
                    ds = BD.EjecutarQuery("select '';")
                End If
                'ds = BD.EjecutarQuery("DECLARE @cadena VARCHAR(MAX)='<tr><th>ID</th><th>Nombre</th><th>Valor 1</th><th>Valor 2</th><th>Peso</th></tr>';SELECT @cadena=@cadena+'<tr><td><label id=""lbl_'+CONVERT(varchar, A.PDK_ID_SECCION_DATO)+'"">'+convert(varchar,A.PDK_ID_SECCION_DATO)+'</label></td><td><label id=""lbl_'+A.PDK_SEC_DAT_NOMBRE+'"">'+PDK_SEC_DAT_NOMBRE+'</label></td><td><label id=""lb_va1'+convert(varchar,A.PDK_ID_SECCION_DATO)+'""></label></td><td><label id=""lbl_va2'+convert(varchar,A.PDK_ID_SECCION_DATO)+'""></label></td><td><label id=""lbl_peso'+convert(varchar,A.PDK_ID_SECCION_DATO)+'""></label></td><td><input id=""cmbinsertar"" value=""Insertar"" type=""button"" class=""TextLink"" onclick=""insertar()""  runat=""server""/></td></tr>'   FROM PDK_SECCION_DATO A INNER JOIN PDK_SECCION B ON A.PDK_ID_SECCION=B.PDK_ID_SECCION   WHERE B.PDK_SEC_CREACION =2 AND A.PDK_SEC_DAT_STATUS=2 AND A.PDK_SEC_DAT_LLAVE=0 AND (A.PDK_ID_TIPO_OBJETO<>1 AND A.PDK_ID_TIPO_OBJETO<>3);select @cadena ")
                ' ds = BD.EjecutarQuery("declare @cadena varchar(max)='<tr><th>Id</th><th>Nombre</th><th>Valor 1</th><th>Valor 2</th><th>Peso</th></tr>';SELECT @cadena=@cadena +'<tr><td><label id=""lbl_'+ltrim(A.PDK_ID_SCORING)+'"">'+ltrim(A.PDK_ID_SCORING)+'</labe></td><td><label id=""lbl_'+ltrim(A.PDK_ID_SCORING)+B.PDK_SEC_DAT_NOMBRE+'"">'+B.PDK_SEC_DAT_NOMBRE+'</label></td><td><label id=""lbl_v1'+ltrim(A.PDK_ID_SCORING)+A.PDK_SCORING_VALOR1+'"" onmousedown=""EditaTXT(this,' + ltrim(A.PDK_ID_SCORING) + ')"">'+A.PDK_SCORING_VALOR1+'</label></td><td><label id=""lbl_v2'+ltrim(A.PDK_ID_SCORING)+A.PDK_SCORING_VALOR2+'"" onmousedown=""EditaTXT(this,' + ltrim(A.PDK_ID_SCORING) + ')"">'+A.PDK_SCORING_VALOR2+'</label></td><td><label id=""lbl_pe'+ltrim(A.PDK_ID_SCORING)+ltrim(A.PDK_SCORING_PESO)+'"" onmousedown=""EditaTXT(this, ' + ltrim(A.PDK_ID_SCORING) + ')"">'+ltrim(A.PDK_SCORING_PESO)+'</label></td></tr>' FROM PDK_SCORING A INNER JOIN PDK_SECCION_DATO B ON A.PDK_ID_SECCION_DATO =B.PDK_ID_SECCION_DATO;select @cadena;")
            Case Is = "grvSeccionPlazo"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("DECLARE @CADENA VARCHAR(MAX)='';IF EXISTS (SELECT * FROM PDK_PRODUCTO_PLAZO) BEGIN  SELECT @CADENA='<tr><th>Id</th><th>Producto</th><th>Nombre Dato</th><th>Valor</th><th>Status</th></tr>'; SELECT @CADENA=@CADENA+'<tr><td><label id=""lbl_'+ltrim(A.PDK_ID_PRODPLAZO)+'"">'+LTRIM(A.PDK_ID_PRODPLAZO)+'</label></td><td><label id=""lbl_'+ltrim(A.PDK_ID_PRODPLAZO)+P.PDK_PROD_NOMBRE+'"">'+P.PDK_PROD_NOMBRE+'</label></td><td><label id=""lbl_'+ltrim(A.PDK_ID_PRODPLAZO)+B.PDK_SEC_DAT_NOMBRE+'"">'+B.PDK_SEC_DAT_NOMBRE+'</label></td><td><label id=""lbl_valor'+ltrim(A.PDK_ID_PRODPLAZO)+'"" onmousedown=""EditaTXT(this,'+ltrim(A.PDK_ID_PRODPLAZO)+','+ltrim(A.PDK_ID_TIPO_OBJETO)+')"">'+A.PDK_PRODPLAZO_VALOR+'</label></td><td><input id=""chek'+ltrim(A.PDK_ID_PRODPLAZO)+'""'+ CASE WHEN A.PDK_PRODPLAZO_STATUS=2 THEN 'checked=""checked""' ELSE '' END + '  type=""checkbox"" onClick=""funchek(this,'+ltrim(A.PDK_ID_PRODPLAZO)+');""/></td></tr>' FROM PDK_PRODUCTO_PLAZO A INNER JOIN PDK_CAT_PRODUCTOS P ON A.PDK_ID_PRODUCTOS=P.PDK_ID_PRODUCTOS  INNER JOIN PDK_SECCION_DATO B ON B.PDK_ID_SECCION_DATO=A.PDK_ID_SECCION_DATO   WHERE A.PDK_ID_PRODUCTOS=" & arraydepende(0) & " AND A.PDK_ID_SECCION_DATO= " & arraydepende(1) & ";   SELECT @CADENA;END ELSE  BEGIN  SELECT @CADENA; END ")
                Else
                    ds = BD.EjecutarQuery("select '';")
                End If

            Case "grvMatriz"
                If validaDepende(arraydepende) Then
                    If arraydepende(0) = 20 Then
                        ds = BD.EjecutarQuery("declare @cadena varchar(max)='<tr><th>Id</th><th>Lim_inf</th><th>Lim_sup</th><th>Decisión</th></tr>'; select @cadena=@cadena + '<tr><td><label id=""lbl_'+LTRIM(A.PDK_ID_MATRIZ_DICSCORING)+'"">'+LTRIM(A.PDK_ID_MATRIZ_DICSCORING)+'</label></td><td><label id=""lblinf_'+LTRIM(A.PDK_ID_MATRIZ_DICSCORING)+'"" onmousedown = ""EditaTXT(this, ' + ltrim(A.PDK_ID_MATRIZ_DICSCORING) + ')"">'+LTRIM(A.PDK_MATRIZ_SCORIN_LIMINF)+'</label></td><td><label id=""lblsup_'+LTRIM(A.PDK_ID_MATRIZ_DICSCORING)+'"" onmousedown = ""EditaTXT(this, ' + ltrim(A.PDK_ID_MATRIZ_DICSCORING) + ')"" >'+LTRIM(A.PDK_MATRIZ_SCORIN_LIMSUP)+'</label></td><td><label id=""lbldec'+LTRIM(A.PDK_ID_MATRIZ_DICSCORING)+'"">'+B.PDK_PAR_SIS_PARAMETRO+'</label></td></tr>' from PDK_MATRIZ_DICTAMENSCORIGN A INNER JOIN PDK_PARAMETROS_SISTEMA B ON A.PDK_MATRIZ_SCORING_DECISION =B.PDK_ID_PARAMETROS_SISTEMA AND B.PDK_PAR_SIS_ID_PADRE=15; SELECT @cadena")
                    ElseIf arraydepende(0) = 21 Then
                        ds = BD.EjecutarQuery("declare @cadena varchar(max)='<tr><th>Id</th><th>Lim_inf</th><th>Lim_sup</th><th>Decisión</th></tr>'; select @cadena=@cadena + '<tr><td><label id=""lbl_'+LTRIM(A.PDK_ID_MATRIZ_BC)+'"">'+LTRIM(A.PDK_ID_MATRIZ_BC)+'</label></td><td><label id=""lblinf_'+LTRIM(A.PDK_ID_MATRIZ_BC)+'"" onmousedown = ""EditaTXT(this, '+ LTRIM(A.PDK_ID_MATRIZ_BC) + ')"" >'+LTRIM(A.PDK_MATRIZ_BC_LIMINF)+'</label></td><td><label id=""lblsup_'+LTRIM(A.PDK_ID_MATRIZ_BC)+'"" onmousedown = ""EditaTXT(this, '+ LTRIM(A.PDK_ID_MATRIZ_BC) + ')"" >'+LTRIM(A.PDK_MATRIZ_BC_LIMSUP)+'</label></td><td><label id=""lbldec'+LTRIM(A.PDK_ID_MATRIZ_BC)+'"">'+B.PDK_PAR_SIS_PARAMETRO+'</label></td></tr>' from PDK_MATRIZ_BCSCORE A INNER JOIN PDK_PARAMETROS_SISTEMA B ON A.PDK_MATRIZ_BC_DECISION =B.PDK_ID_PARAMETROS_SISTEMA AND B.PDK_PAR_SIS_ID_PADRE=15; SELECT @cadena")
                    ElseIf arraydepende(0) = 22 Then
                        ds = BD.EjecutarQuery("declare @cadena varchar(max)='<tr><th>Id</th><th>Lim_inf</th><th>Lim_sup</th><th>Decisión</th></tr>'; select @cadena=@cadena + '<tr><td><label id=""lbl_'+LTRIM(A.PDK_ID_MATRIZ_ICC)+'"">'+LTRIM(A.PDK_ID_MATRIZ_ICC)+'</label></td><td><label id=""lblinf_'+LTRIM(A.PDK_ID_MATRIZ_ICC)+'"" onmousedown = ""EditaTXT(this, '+ LTRIM(A.PDK_ID_MATRIZ_ICC) + ')"" >'+LTRIM(A.PDK_MATRIZ_ICC_LIMINF)+'</label></td><td><label id=""lblsup_'+LTRIM(A.PDK_ID_MATRIZ_ICC)+'"" onmousedown = ""EditaTXT(this, '+ LTRIM(A.PDK_ID_MATRIZ_ICC) + ')"" >'+LTRIM(A.PDK_MATRIZ_ICC_LIMSUP)+'</label></td><td><label id=""lbldec'+LTRIM(A.PDK_ID_MATRIZ_ICC)+'"">'+B.PDK_PAR_SIS_PARAMETRO+'</label></td></tr>' from PDK_MATRIZ_ICC A INNER JOIN PDK_PARAMETROS_SISTEMA B ON A.PDK_MATRIZ_ICC_DECISION =B.PDK_ID_PARAMETROS_SISTEMA AND B.PDK_PAR_SIS_ID_PADRE=15; SELECT @cadena")
                    ElseIf arraydepende(0) = 23 Then
                        ds = BD.EjecutarQuery("declare @cadena varchar(max)='<tr><th>Id</th><th>Lim_inf</th><th>Lim_sup</th><th>Decisión</th></tr>'; select @cadena=@cadena + '<tr><td><label id=""lbl_'+LTRIM(A.PDK_ID_MATRIZ_DICTACAPPAGO)+'"">'+LTRIM(A.PDK_ID_MATRIZ_DICTACAPPAGO)+'</label></td><td><label id=""lblinf_'+LTRIM(A.PDK_ID_MATRIZ_DICTACAPPAGO)+'"" onmousedown = ""EditaTXT(this, ' + LTRIM(A.PDK_ID_MATRIZ_DICTACAPPAGO) + ')"" >'+LTRIM(A.PDK_MATRIZ_DICTACAPPAGO_LIMINF)+'</label></td><td><label id=""lblsup_'+LTRIM(A.PDK_ID_MATRIZ_DICTACAPPAGO)+'"" onmousedown = ""EditaTXT(this, ' + LTRIM(A.PDK_ID_MATRIZ_DICTACAPPAGO) + ')"" >'+LTRIM(A.PDK_MATRIZ_DICTACAPPAGO_LIMSUP)+'</label></td><td><label id=""lbldec'+LTRIM(A.PDK_ID_MATRIZ_DICTACAPPAGO)+'"">'+B.PDK_PAR_SIS_PARAMETRO+'</label></td></tr>' from PDK_MATRIZ_DICTAMENCAPPAGO A INNER JOIN PDK_PARAMETROS_SISTEMA B ON A.PDK_MATRIZ_DICTACAPPAGO_DECISION =B.PDK_ID_PARAMETROS_SISTEMA AND B.PDK_PAR_SIS_ID_PADRE=15; SELECT @cadena")
                    ElseIf arraydepende(0) = 28 Then
                        ds = BD.EjecutarQuery("DECLARE @MIN INT,@MAX INT,@CVE INT,@STA INT, @CADENA VARCHAR(MAX)='<tr><th>Id</th><th>Vivienda</th><th>% Aplicar</th></tr>'; CREATE TABLE #TEM (ID INT IDENTITY,CVE INT,STA INT); INSERT INTO #TEM (CVE,STA) SELECT PDK_ID_PARAMETROS_SISTEMA,PDK_PAR_SIS_STATUS FROM PDK_PARAMETROS_SISTEMA WHERE PDK_PAR_SIS_ID_PADRE=29; SELECT @MIN=MIN(ID) FROM #TEM; SELECT @MAX=MAX(ID) FROM #TEM;WHILE @MIN<=@MAX BEGIN SELECT @CVE=CVE,@STA=STA FROM #TEM WHERE ID=@MIN  IF NOT EXISTS (SELECT * FROM PDK_MATRIZ_TIPOVIVIENDA WHERE PDK_VIVIENDA_NUMERO=@CVE) BEGIN INSERT PDK_MATRIZ_TIPOVIVIENDA VALUES (@CVE,0,0,GETDATE(),1)  END ELSE BEGIN IF @STA <>2 BEGIN DELETE FROM PDK_MATRIZ_TIPOVIVIENDA WHERE PDK_VIVIENDA_NUMERO=@CVE  END END    SET @MIN=@MIN+1 END; DROP TABLE #TEM; SELECT @CADENA=@CADENA + '<tr><td><label id=""lbl_'+ LTRIM(A.PDK_ID_MATRIZ_TIPOVIVIENDA)+'"">'+LTRIM(A.PDK_ID_MATRIZ_TIPOVIVIENDA)+'</label></td><td><label id=""lblvi_'+LTRIM(A.PDK_ID_MATRIZ_TIPOVIVIENDA)+'"">'+B.PDK_PAR_SIS_PARAMETRO+'</label></td><td><label id=""lblpo_'+LTRIM(A.PDK_ID_MATRIZ_TIPOVIVIENDA)+'"" onmousedown = ""EditaTXT(this, ' + LTRIM(A.PDK_ID_MATRIZ_TIPOVIVIENDA) + ')"">'+LTRIM(A.PDK_VIVIENDA_PORCENTAJE)+'</label></td></tr>' FROM PDK_MATRIZ_TIPOVIVIENDA A INNER JOIN PDK_PARAMETROS_SISTEMA B ON A.PDK_VIVIENDA_NUMERO=B.PDK_ID_PARAMETROS_SISTEMA AND B.PDK_PAR_SIS_ID_PADRE=29; SELECT @CADENA;")
                    End If
                Else
                    ds = BD.EjecutarQuery("SELECT '';")
                End If

            Case "tbFormulaRN"
                ds = BD.EjecutarQuery("exec sp_fillgv 3,1;")
            Case "tbPantallaDocumentos"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("exec sp_fillgv 4, " & arraydepende(0) & ";")
                Else
                    ds = BD.EjecutarQuery("exec sp_fillgv 4")
                End If
            Case "tbTareaPerfil"
                If arraydepende.Length = 3 Then
                    If validaDepende(arraydepende) Then
                        ds = BD.EjecutarQuery("exec sp_TareaPerfil " & arraydepende(0) & ", " & arraydepende(1) & ", " & arraydepende(2))
                    Else
                        ds = BD.EjecutarQuery("select '';")
                    End If
                Else
                    If validaDepende(arraydepende) Then
                        ds = BD.EjecutarQuery("exec sp_TareaPerfil " & arraydepende(0) & ", " & arraydepende(1) & ", " & arraydepende(2) & ", " & arraydepende(3) & ", " & arraydepende(4))
                    Else
                        If arraydepende(4) = "" Then
                            ds = BD.EjecutarQuery("exec sp_TareaPerfil " & arraydepende(0) & ", " & arraydepende(1) & ", " & arraydepende(2) & ", " & arraydepende(3))
                        Else
                            ds = BD.EjecutarQuery("select '';")
                        End If
                    End If
                End If
            Case "tbTareas"
                If validaDepende(arraydepende) Then
                    Dim ds_ As New DataSet
                    ds_ = BD.EjecutarQuery("EXEC update_Task_Blocked_SP " + arraydepende(0))
                    ds = BD.EjecutarQuery("exec fillTareas " & arraydepende(0) & ", " & arraydepende(1) & ", " & arraydepende(2))
                End If
            Case "grvConsulta"

                Dim tam As Integer = arraydepende.Length
                If tam = 1 Then
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl 'usuario', '" & arraydepende(0) & "';")
                ElseIf tam = 2 Then
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl 'usuario', '" & arraydepende(0) & "', '','',0,0,0,0,0,0, " & arraydepende(1) & ";") 'AVH
                ElseIf tam = 4 Then
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl 'usuario', '" & arraydepende(0) & "', '','', " & arraydepende(1) & "," & arraydepende(2) & "," & arraydepende(3) & ";")
                ElseIf tam = 5 Then
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl 'usuario', '" & arraydepende(0) & "', '','', " & arraydepende(1) & "," & arraydepende(2) & "," & arraydepende(3) & ", " & arraydepende(4) & ";")
                ElseIf tam = 7 Then
                    'BUG-PD-23 29/03/2017 MAPH Cambios para realizar la búsqueda con o sin autocompletar; ETIQUETA INICIO
                    Dim paramNumber As Integer
                    Dim paramIsNumber As Boolean = Int32.TryParse(arraydepende(2), paramNumber)
                    If Not paramIsNumber Then
                        arraydepende(2) = Replace(arraydepende(2).ToString(), " ", "%")
                    End If
                    'BUG-PD-23 29/03/2017 MAPH Cambios para realizar la búsqueda con o sin autocompletar; ETIQUETA FINAL
                    ds = BD.EjecutarQuery("exec sp_fillPanelControl '" & arraydepende(0) & "', '" & arraydepende(1) & "', '" & arraydepende(2) & "', '" & arraydepende(3) & "',  " & arraydepende(4) & "," & arraydepende(5) & "," & arraydepende(6) & ";")
                End If

            Case "TabEntrevista"
                If validaDepende(arraydepende) Then
                    'ds = BD.EjecutarQuery("DECLARE @CADENA VARCHAR(MAX)='<tr><th>Id Solicitud</th><th>Nombre</th><th>Genera</th></tr>',@pantalla varchar(5);set @pantalla=" & arraydepende(0) & ";SELECT @CADENA=@CADENA+' '+ '<tr><td><label id=""lbl_'+LTRIM(PDK_ID_SECCCERO)+'"">'+LTRIM(PDK_ID_SECCCERO)+'</label></td><td><label id=""lblNom'+LTRIM(PDK_ID_SECCCERO)+'"">'+(NOMBRE1+' '+NOMBRE2+' '+ APELLIDO_PATERNO +' '+APELLIDO_MATERNO) +'</label></td><td><input id=""cmbGenera"" value=""Entrevista""  type=""button"" class=""TextLink"" onclick=""JavaScript:CallWindow(''ImprimirCreditoSolicitud.aspx?idPantalla='+@pantalla+' &IdFolio='+LTRIM(PDK_ID_SECCCERO)+' &CVE=1'')""/></td></tr>' FROM  PDK_TAB_SOLICITANTE WHERE PDK_ID_SECCCERO=" & arraydepende(1) & ";SELECT @CADENA")
                    ds = BD.EjecutarQuery(" EXEC sp_GenerarEntrevista " & arraydepende(1) & "," & arraydepende(0) & "," & arraydepende(2) & "," & arraydepende(3))
                Else
                    ds = BD.EjecutarQuery("select '';")
                End If
            Case "tabNotificacion"
                ds = BD.EjecutarQuery("exec sp_PantallaAutoriza " & arraydepende(0) & "," & arraydepende(1))
                'ds = BD.EjecutarQuery("DECLARE @CADENA VARCHAR(MAX)='',@CONDI INT,@ENABLE INT; SELECT @ENABLE=" & arraydepende(1) & " SELECT @CADENA=@CADENA+'<tr><td class=""campos"">SOLICITUD:</td>'+'<td>'+ltrim(PDK_ID_SECCCERO)+'</td><td class=""campos"">NOMBRE:</td>'+'<td>'+isnull(NOMBRE1,'')+' '+isnull(NOMBRE2,'')+' '+isnull(APELLIDO_PATERNO,'')+' '+isnull(APELLIDO_MATERNO,'')+'</td></tr>'  FROM PDK_TAB_SOLICITANTE WHERE PDK_ID_SECCCERO =" & arraydepende(0) & " IF NOT EXISTS (SELECT * FROM PDK_RESULTADO_NOTIFICA WHERE PDK_ID_SECCCERO=" & arraydepende(0) & " ) BEGIN SELECT @CADENA=@CADENA+'<tr><td class=""campos"">CALIFICACIÓN:</td><td><select id=""ddlcalifica"" class =""Text"" onchange =""Activiar(this)""  style=""width:200px;""><option value=""0""></option>' SELECT @CADENA=@CADENA+'<option value=""'+LTRIM(PDK_ID_PARAMETROS_SISTEMA)+'"">'+PDK_PAR_SIS_PARAMETRO +'</option>' FROM PDK_PARAMETROS_SISTEMA WHERE PDK_PAR_SIS_ID_PADRE=70 SELECT @CADENA=@CADENA+'</select></td><td class=""campos""><label id=""lblcondi"" style=""display:none;"" >CONDICIONADO A:</label></td><td><select id=""ddlcondic""  onchange =""btnGuardaCondi(this)"" class =""Text"" style=""width:200px;display:none;""><option value=""0""></option>' SELECT @CADENA=@CADENA+'<option value=""'+LTRIM(PDK_ID_PARAMETROS_SISTEMA)+'"">'+PDK_PAR_SIS_PARAMETRO+'</option>' FROM PDK_PARAMETROS_SISTEMA WHERE PDK_PAR_SIS_ID_PADRE=74 SELECT @CADENA=@CADENA+'</select></td></tr>' SELECT @CADENA=@CADENA +'<tr><td class=""campos"">OBSERVACION:</td><td colspan=""3""><TEXTAREA id=""txtObserva"" cols=20 rows=10  class=""resul"" onkeypress=""ManejaCar(''A'',1,this.value,this)"" style="" width:410px;"" onblur=""btnguardarObse(this)""  ></TEXTAREA> </td></tr>' END ELSE BEGIN IF OBJECT_ID('TEMPDB..#TEMP') IS NOT NULL DROP TABLE #TEMP  SELECT @CONDI=PDK_RESULNOTI_CALIFICACION  FROM PDK_RESULTADO_NOTIFICA WHERE PDK_ID_SECCCERO =" & arraydepende(0) & "  CREATE TABLE #TEMP (PDK_RESULNOTI_CALIFICACION INT,PDK_RESULNOTI_CONDICIONA INT) INSERT INTO #TEMP (PDK_RESULNOTI_CALIFICACION,PDK_RESULNOTI_CONDICIONA) SELECT PDK_RESULNOTI_CALIFICACION,PDK_RESULNOTI_CONDICIONA   FROM PDK_RESULTADO_NOTIFICA WHERE PDK_ID_SECCCERO=" & arraydepende(0) & " SELECT @CADENA=@CADENA+'<tr><td class=""campos"">CALIFICACIÓN:</td><td><select id=""ddlcalifica"" class =""Text"" onchange =""Activiar(this)"" '+CASE WHEN @ENABLE=1 THEN ' disabled =""disabled""'ELSE '' END +' style=""width:200px;""><option value=""0""></option>' SELECT @CADENA=@CADENA+'<option value=""'+LTRIM(B.PDK_ID_PARAMETROS_SISTEMA)+'""'+ CASE WHEN A.PDK_RESULNOTI_CALIFICACION in(71,72,73) THEN 'selected>' ELSE '>' END +B.PDK_PAR_SIS_PARAMETRO+'</option>' FROM #TEMP A RIGHT OUTER JOIN PDK_PARAMETROS_SISTEMA B ON A.PDK_RESULNOTI_CALIFICACION =B.PDK_ID_PARAMETROS_SISTEMA    WHERE B.PDK_PAR_SIS_ID_PADRE =70  SELECT @CADENA=@CADENA+'</select></td><td class=""campos""><label id=""lblcondi""'+CASE WHEN @CONDI=72 THEN ' style=""display:block;""' ELSE ' style=""display:none;""' END+' >CONDICIONADO A:</label></td><td><select id=""ddlcondic""  onchange =""btnGuardaCondi(this)"" class =""Text""'+ CASE WHEN @CONDI=72 THEN ' style=""width:200px;display:block;""' ELSE ' style=""width:200px;display:none;""' END+ CASE WHEN @ENABLE=1 THEN ' disabled =""disabled""' ELSE '' END+' ><option value=""0""></option>' SELECT @CADENA=@CADENA+'<option value=""'+LTRIM(B.PDK_ID_PARAMETROS_SISTEMA)+'""'+ CASE WHEN A.PDK_RESULNOTI_CONDICIONA in(75,76,77) THEN 'selected>' ELSE '>' END +B.PDK_PAR_SIS_PARAMETRO+'</option>' FROM #TEMP A RIGHT OUTER JOIN PDK_PARAMETROS_SISTEMA B ON A.PDK_RESULNOTI_CONDICIONA =B.PDK_ID_PARAMETROS_SISTEMA     WHERE B.PDK_PAR_SIS_ID_PADRE =74  SELECT @CADENA=@CADENA+ '<tr><td class=""campos"">OBSERVACION:</td><td colspan=""3""><TEXTAREA id=""txtObserva"" cols=20 rows=10  class=""resul"" onkeypress=""ManejaCar(''A'',1,this.value,this)"" style="" width:410px;"" '+CASE WHEN @ENABLE=1 THEN 'disabled =""disabled""' ELSE '' END  +' onblur=""btnguardarObse(this)"">'+PDK_RESULNOTI_OBSERVACION+'</TEXTAREA> </td></tr>'   FROM PDK_RESULTADO_NOTIFICA WHERE PDK_ID_SECCCERO=" & arraydepende(0) & "    END  SELECT @CADENA;   ")
            Case "gvformato"
                ds = BD.EjecutarQuery("exec sp_FormatoCto " & arraydepende(0))
            Case "divValorScore"
                ds = BD.EjecutarQuery("exec sp_SCOREMORAL " & arraydepende(0))
            Case Else
                ds = BD.EjecutarQuery("select '';")
        End Select

        Return ds.Tables(0).Rows(0)(0).ToString

    End Function


    <WebMethod()>
    Public Function urlnueva(ByVal solicitud As Integer) As String
        Dim newurl As String = String.Empty
        Dim sql As New StringBuilder()
        Dim ds As New DataSet()

        Try
            sql.AppendLine("SELECT  E.PDK_PANT_MOSTRAR, ")
            sql.AppendLine("'./' + LOWER(REPLACE(PDK_PANT_LINK, 'PANT_', '')) + '?idPantalla=' + CONVERT(VARCHAR(10), E.PDK_ID_PANTALLAS) ")
            sql.AppendLine("+ '&Sol=' + CONVERT(VARCHAR(10), B.PDK_ID_SOLICITUD)")
            sql.AppendLine("+ '&usuario=' + CONVERT(VARCHAR(10), F.PDK_ID_USUARIO) AS PDK_PANT_LINK")
            sql.AppendLine("FROM PDK_TAB_SECCION_CERO  A")
            sql.AppendLine("INNER JOIN PDK_OPE_SOLICITUD B")
            sql.AppendLine("ON B.PDK_ID_SOLICITUD = A.PDK_ID_SECCCERO")
            sql.AppendLine("AND B.PDK_ID_TAREAS = A.PDK_TAREA_ACTUAL")
            sql.AppendLine("INNER JOIN PDK_CAT_TAREAS C ON C.PDK_ID_TAREAS = B.PDK_ID_TAREAS")
            sql.AppendLine("INNER JOIN PDK_REL_PANTALLA_TAREA D ON D.PDK_ID_TAREAS = C.PDK_ID_TAREAS")
            sql.AppendLine("INNER JOIN PDK_PANTALLAS E ON E.PDK_ID_PANTALLAS = D.PDK_ID_PANTALLAS")
            sql.AppendLine("INNER JOIN PDK_USUARIO F ON F.PDK_ID_USUARIO = B.PDK_OPE_USU_ASIGNADO")
            sql.AppendLine("WHERE B.PDK_OPE_STATUS_TAREA = 40 ")
            sql.AppendLine("AND PDK_ID_SECCCERO = " & solicitud.ToString)

            ds = BD.EjecutarQuery(sql.ToString)

            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Then
                        newurl = ds.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString
                    End If
                End If
            End If

            Return newurl

        Catch ex As Exception
            Return newurl
        End Try

    End Function

    <WebMethod()>
    Public Function Carga_Notas(ByVal solicitud As Integer, ByVal usuario As Integer, ByVal opcion As Integer) As String
        Dim ds As New DataSet()
        Dim lista_solicitudes As String
        lista_solicitudes = BuscaPermiso(usuario)
        If (lista_solicitudes <> "") Then

            Try

                Select Case opcion

                    Case 1  'Busca solicitudes de usuario
                        Dim tmp As DataSet
                        Dim pconsulta As String = "select id id_nota, * from PDK_CAJA_NOTAS_EXT WHERE estatus = 1  and PDK_CAJA_NOTAS_EXT.PDK_ID_SOLICITUD in(" + lista_solicitudes + ") "

                        tmp = BD.EjecutarQuery(pconsulta)
                        If tmp.Tables(0).Rows.Count > 0 Then
                            'El usuario tiene mensajes en la caja de notas
                            pconsulta = "select id id_nota, * from PDK_CAJA_NOTAS_EXT WHERE estatus = 1  and PDK_CAJA_NOTAS_EXT.PDK_ID_SOLICITUD in(" + lista_solicitudes + ") AND PDK_ID_SOLICITUD = " + Convert.ToString(solicitud) + " ORDER BY fe_creacion DESC"
                            ds = BD.EjecutarQuery(pconsulta)
                            If ds.Tables(0).Rows.Count > 0 Then
                                ds.Tables(0).TableName = "cajamensajes"
                                Return ds.GetXml()
                            Else
                                ''la solicitud no tiene mensajes
                                'Dim dss As New DataSet
                                'dss = BD.EjecutarQuery("select PDK_ID_SECCCERO from PDK_TAB_SECCION_CERO WHERE PDK_ID_SECCCERO = " & Text1.Value)

                                'If (dss.Tables.Count > 0) Then
                                '    If (dss.Tables(0).Rows.Count > 0) Then
                                '        If lista_solicitudes.Contains(Convert.ToString(sol).ToString()) Then
                                '            gvMensajes.Visible = False
                                '            gvMensajes.Dispose()
                                '            gvMensajes.DataSource = Nothing
                                '            tmp.Clear()
                                '        Else
                                '            Master.MensajeError("Ingrese una solictud valida")
                                '            textEditor.Visible = False
                                '            btnGuardar.Visible = False
                                '        End If
                                '    Else
                                '        Master.MensajeError("Ingrese una solictud valida")
                                '        textEditor.Visible = False
                                '        btnGuardar.Visible = False
                                '    End If

                                'End If


                            End If
                        Else
                            ''El usuario no tiene mensajes en la caja de notas
                            'If lista_solicitudes.Contains(Convert.ToString(sol).ToString()) Then
                            '    textEditor.Visible = True
                            '    btnGuardar.Visible = True
                            'Else
                            '    Master.MensajeError("Ingrese una solictud valida")
                            '    textEditor.Visible = False
                            '    btnGuardar.Visible = False
                            'End If



                        End If



                    Case 2 'Guardar mensaje
                        Dim pconsulta As String
                        pconsulta = "INSERT INTO PDK_CAJA_NOTAS_EXT (fe_creacion, mensaje,usu_creacion,PDK_ID_SOLICITUD) VALUES (GETDATE(),'" & Session("cveUsuAcc") & ": " & "holas" & "'," & usuario & "," & Convert.ToString(solicitud) & ")"""
                        ds = BD.EjecutarQuery(pconsulta)

                        Return ""
                    Case Else

                        Return ""
                End Select

            Catch ex As Exception
                Return ex.Message
            End Try


        Else
            Return ""
        End If
        Return ""
    End Function



    Function BuscaPermiso(usuario As Int64) As String
        'Dim lista_usuario As New List(Of Int32)
        Dim string_in As String = ""
        Dim dsresult As DataSet
        dsresult = BD.EjecutarQuery("EXEC getUsuariosPerfilDist " & usuario & "," & 1 & "")
        If dsresult.Tables.Count > 0 Then
            If dsresult.Tables(0).Rows.Count > 0 Then
                For index As Integer = 0 To dsresult.Tables(0).Rows.Count - 1
                    'lista_usuario.Add(Convert.ToInt32(dsresult.Tables(0).Rows(index).Item("PDK_ID_USUARIO")))
                    string_in += dsresult.Tables(0).Rows(index).Item("PDK_ID_SECCCERO").ToString + " ,"
                Next
            End If
        Else
            dsresult = BD.EjecutarQuery("EXEC getUsuariosPerfilDist " & usuario & "," & 2 & "")
            If dsresult.Tables.Count > 0 Then
                If dsresult.Tables(0).Rows.Count > 0 Then
                    For index As Integer = 0 To dsresult.Tables(0).Rows.Count - 1
                        'lista_usuario.Add(Convert.ToInt32(dsresult.Tables(0).Rows(index).Item("PDK_ID_USUARIO")))
                        string_in += dsresult.Tables(0).Rows(index).Item("PDK_ID_SECCCERO").ToString + " ,"
                    Next
                End If
            End If

        End If
        If string_in.Length > 0 Then
            string_in = string_in.Remove(string_in.Length - 1)
            Return string_in

        Else : Return ""
        End If

    End Function

    Function VerifyIntoBasic(ByVal sol As Integer, Optional ByVal info_basic As String = "") As String
        Dim jsonRespuesta As String = String.Empty

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_Changes_BasicInfo_SP"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", sol)
            If info_basic <> "" Then
                cmd.Parameters.AddWithValue("@JSON_BASIC_INFO", info_basic)
            End If

            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                jsonRespuesta = reader(0).ToString()
            Loop
        Catch ex As Exception
            jsonRespuesta = ex.Message.ToString()
        End Try

        Return jsonRespuesta
    End Function
End Class