Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports ProdeskNet.Buro
Imports Newtonsoft.Json
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Xml
Imports System.Data


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class fillobjetos    
    Inherits System.Web.Services.WebService
    Dim BD As New ProdeskNet.BD.clsManejaBD

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()> _
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
                ds = BD.EjecutarQuery("SELECT  A.PDK_PER_NOMBRE AS selectedvalue, A.PDK_ID_PERFIL as selectedindex FROM PDK_PERFIL A INNER JOIN PDK_PARAMETROS_SISTEMA B ON B.PDK_ID_PARAMETROS_SISTEMA = A.PDK_PER_ACTIVO")
            Case "Producto1"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("SELECT  A.PDK_PROD_NOMBRE AS selectedvalue, A.PDK_ID_PRODUCTOS  as selectedindex FROM PDK_CAT_PRODUCTOS A INNER JOIN PDK_CAT_EMPRESAS B ON A.PDK_ID_EMPRESA=B.PDK_ID_EMPRESA  INNER JOIN PDK_CAT_MONEDA C ON C.PDK_ID_MONEDA=A.PDK_ID_MONEDA WHERE A.PDK_ID_EMPRESA = " & arraydepende(0) & " AND A.PDK_PROD_ACTIVO=2 ORDER BY A.PDK_ID_PRODUCTOS")
                Else
                    ds = BD.EjecutarQuery("SELECT ''as selectedvalue, ''as selectedindex;")
                End If
            Case "txtCOLONIA15"
                ds = BD.EjecutarQuery("SELECT C.CPO_DS_COLONIA AS selectedvalue,C.CPO_FL_CP as selectedindex FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE  where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "txtCOLONIA52"
                ds = BD.EjecutarQuery("SELECT C.CPO_DS_COLONIA AS selectedvalue,C.CPO_FL_CP as selectedindex FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE  where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "txtCOLONIA85"
                ds = BD.EjecutarQuery("SELECT C.CPO_DS_COLONIA AS selectedvalue,C.CPO_FL_CP as selectedindex FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE  where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "txtCOLONIA122"
                ds = BD.EjecutarQuery("SELECT C.CPO_DS_COLONIA AS selectedvalue,C.CPO_FL_CP as selectedindex FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE  where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "txtCOLONIA458"
                ds = BD.EjecutarQuery("SELECT C.CPO_DS_COLONIA AS selectedvalue,C.CPO_FL_CP as selectedindex FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE  where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "ProductoPlaz"
                ds = BD.EjecutarQuery("SELECT PDK_PROD_NOMBRE AS selectedvalue,PDK_ID_PRODUCTOS AS selectedindex FROM PDK_CAT_PRODUCTOS")
            Case "Empresa1"
                ds = BD.EjecutarQuery("SELECT  A.PDK_EMP_NOMBRE AS selectedvalue, A.PDK_ID_EMPRESA as selectedindex FROM PDK_CAT_EMPRESAS A WHERE A.PDK_EMP_ACTIVO=2")
            Case "Distribuidor"
                ds = BD.EjecutarQuery("select PDK_DIST_CLAVE + '. ' + PDK_DIST_NOMBRE AS selectedvalue,cd.PDK_ID_DISTRIBUIDOR as selectedindex from PDK_CAT_DISTRIBUIDOR cd inner join PDK_REL_USU_DIST rud on cd.PDK_ID_DISTRIBUIDOR = rud.PDK_ID_DISTRIBUIDOR where rud.PDK_ID_USUARIO = " & arraydepende(0) & " ORDER BY PDK_DIST_CLAVE;")
            Case "Persona"
                ds = BD.EjecutarQuery("select  PDK_PER_NOMBRE AS selectedvalue ,PDK_ID_PER_JURIDICA as selectedindex from PDK_CAT_PER_JURIDICA;")

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
    <WebMethod()> _
    Public Function btnImprimirCto(ByVal cadena As String, _
                                   ByVal contrato As String, _
                                   ByVal cliente As String, _
                                   ByVal empresa As String, _
                                   ByVal moneda As String, _
                                   ByVal toperacion As String, _
                                   ByVal pjuridica As String) As String
        Try
            Dim arreglo As Array = Split(cadena, ",")
            Dim dabet As New DataSet
            Dim Dbdat As New DataSet
            Dim arregloPath As String = ""


            Dim i As Integer = 0


            If cadena.Length > 0 Then
                dabet = BD.EjecutarQuery("SELECT FMT_FL_CVE, FMT_DS_DESCRIPCION, FMT_DS_MACHOTE, FMT_NO_TIPODOCUMENTO FROM ProleaseNet.dbo.CCTO_MACHOTE WHERE FMT_FL_CVE in(" & cadena & ") ORDER BY FMT_FL_CVE")

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


                        Select Case tipoDocumento
                            Case 1
                                Select Case toperacion
                                    Case "CA"
                                        Dbdat = BD.EjecutarQuery("EXEC ProleaseNet.dbo.SpLsnetDocumentoContradoCredito '" & contrato & "'," & cliente & "," & empresa & "," & moneda & ",'" & toperacion & "','" & Format(Now(), "yyyy-MM-dd") & "'," & pjuridica)
                                    Case Else
                                        Dbdat = BD.EjecutarQuery("EXEC ProleaseNet.dbo.splsnetDocumentoContrato '" & contrato & "'," & cliente & "," & empresa & "," & moneda & ",'" & toperacion & "','" & Format(Now(), "yyyy-MM-dd") & "'," & pjuridica)
                                End Select

                            Case Else

                        End Select

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
    <WebMethod()> _
    Public Function btnInsertarPanta(ByVal cadena As String, _
                    ByVal pantalla As String, _
                    ByVal solicitud As String, _
                    ByVal persona As String, _
                    ByVal usuario As String) As String

        Dim countrows As Integer = 0
        Dim ds As DataSet = Nothing
        Dim dataBase As DataSet = Nothing

        Try

            countrows = BD.ExInsUpd(cadena)

            If countrows > 0 Then
                If pantalla = "PRECALIFICACION" Or pantalla = "COACREDITADO" Then
                    Dim strRFCSolicitante As String = String.Empty
                    Dim strRFCCoacreditado As String = String.Empty
                    Dim valSol As String = ""
                    Dim valCoa As String = ""

                    countrows = 0
                    ds = BD.EjecutarQuery("SELECT  ISNULL(A.RFC, '') 'SOLICITANTE', ISNULL(B.RFC,'') 'COACREDITADO' " & _
                                " FROM PDK_TAB_SOLICITANTE A " & _
                             " LEFT OUTER JOIN PDK_TAB_COACREDITADO_CASO B ON B.PDK_ID_SECCCERO = A.PDK_ID_SECCCERO " & _
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
                                If dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString <> "" Then
                                    valSol = dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString
                                End If

                            End If

                            ' Si la respuesta de la consulta a buro regresa un mensaje de error muestra el mensaje y sale de la funcion
                        Else

                            'Return valSol
                            Return "ERROR:" & valSol
                            'Exit Function

                        End If

                    End If

                    ' Se cambia de posicion debido a que pasa dos veces el spvalnegocio y siempre pasa a la siguiente tarea aunque el buro no traiga infromacion d@ve.

                    dataBase = BD.EjecutarQuery(" EXEC spValNegocio  " & solicitud & ",64," & usuario)
                    If dataBase.Tables(0).Rows.Count > 0 AndAlso dataBase.Tables.Count > 0 Then
                        If dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString <> "" Then
                            Return dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString
                        Else
                            Return "Registro guardado exitosamente."
                        End If

                    End If

                End If
            Else
                Return "Error al actualizar la informacion en pantalla."
            End If

        Catch ex As Exception
            Return "ERROR: " & ex.Message.ToString
        End Try

    End Function
    <WebMethod()> _
    Public Function btnInsUpd(ByVal cadena As String) As String
        Dim countrows As Integer = BD.ExInsUpd(cadena)
        If countrows > 0 Then
            Return "Registro actualizado exitosamente."
        Else
            Return "Existio un error al actualizar la Información."
        End If
    End Function
    <WebMethod()> _
    Public Function btnInsertDocumento(ByVal cadena As String) As String
        Dim dataBase As DataSet = Nothing
        dataBase = BD.EjecutarQuery(cadena)
        If dataBase.Tables(0).Rows.Count > 0 AndAlso dataBase.Tables.Count > 0 Then
            If dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString <> "" Then
                Return dataBase.Tables(0).Rows(0).Item("MENSAJE").ToString
            Else
                Return "Registro guardado exitosamente."
            End If
        End If


    End Function
    <WebMethod()> _
    Public Function btnManejoMensaje(ByVal cadena As String, ByVal cadena2 As String) As String
        Dim dsresult As New DataSet

        If cadena <> "" Then
            dsresult = BD.EjecutarQuery(cadena)
            If dsresult.Tables.Count > 0 AndAlso dsresult.Tables(0).Rows.Count > 0 Then
                If dsresult.Tables(0).Rows(0).Item("MENSAJE") <> "" Then
                    Return dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
                Else
                    Dim countrows As Integer = BD.ExInsUpd(cadena2)
                    If countrows > 0 Then
                        Return "el registro se actualizo correctamente"
                    Else
                        Return "existio un error al insertar el registro."
                    End If
                    'dsresult = BD.EjecutarQuery(cadena2)
                    ''Return "Registro actualizado correctamente"

                End If
            End If
        Else
            Dim countrows As Integer = BD.ExInsUpd(cadena2)
            If countrows > 0 Then
                Return "el registro se actualizo correctamente"
            Else
                Return "existio un error al insertar el registro."
            End If

        End If


    End Function
    <WebMethod()> _
    Public Function fnValida(ByVal cadena As String)
        Dim ds As New DataSet
        ds = BD.EjecutarQuery(cadena)
        If Not IsDBNull(ds) Then
            Return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented)


        End If
    End Function

    <WebMethod()> _
    Public Function fillMultiTab(ByVal valor As String, ByVal depende As String)
        Dim ds As New DataSet
        Dim arraydepende As Array

        arraydepende = depende.Split(",")

        Select Case valor
            Case "#tbFacturaVehiculo, #tbFacturaAccesorios, #tbFacturaIntangibles, #tbFacturaComer"
                ds = BD.EjecutarQuery("exec sp_fillAllActivos " & arraydepende(0))
        End Select

        If Not IsDBNull(ds) Then
            Return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented)
        End If

    End Function

    <WebMethod()> _
    Public Function btnInsertar( _
                    ByVal cadena As String, _
                    ByVal pantalla As String, _
                    ByVal solicitud As String, _
                    ByVal persona As String, _
                    ByVal usuario As String, _
        ByVal cveusuario As String, _
        ByVal paswword As String, _
        ByVal idpantalla As String, _
        ByVal strStatus As String, _
        ByVal intbandera As String, _
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

    Private Function btnBuro( _
                            ByVal intSolicitud As Integer, _
                            ByVal intPersona As Integer, _
                            ByVal intUsuario As Integer,
                            ByVal strRFC As String, _
                            ByVal intTipoPersona As Integer) As String

        Try

            Dim objBuro As New clsBuroINTL
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
        End Try

    End Function
    <WebMethod()> _
    Public Function validaScoring(ByVal cadena As String) As String
        Dim ds As New DataSet
        ds = BD.EjecutarQuery(cadena)
        Return ds.Tables(0).Rows(0)(0).ToString
    End Function
    <WebMethod()> _
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
            Case "tags"
                If validaDepende(arraydepende) Then
                    ds = BD.EjecutarQuery("declare @usuario varchar(50) = 'ADMINTEL', @nombres varchar(max) = ''; select @nombres = @nombres + ltrim(PDK_ID_SECCCERO) + '. ' + isnull(NOMBRE1, '') + ' ' + isnull(NOMBRE2, '') + ' ' + isnull([APELLIDO_PATERNO], '') + ' ' + isnull(APELLIDO_MATERNO, '') + ', '  from PDK_TAB_SOLICITANTE tsol where PDK_ID_SECCCERO = 1; select replace(substring(@nombres, 0, len(@nombres)), '  ', ' ');")
                End If
            Case "txtDistribuidor"
                ds = BD.EjecutarQuery("declare @dist varchar(max) = ''; select @dist = @dist + PDK_DIST_CLAVE + '. ' + PDK_DIST_NOMBRE + ', ' from PDK_CAT_DISTRIBUIDOR; select @dist;")
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

            Case "DELEGA_O_MUNI17"
                ds = BD.EjecutarQuery("SELECT top 1 M.MUN_DS_MUNICIPIO FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "ESTADO18"
                ds = BD.EjecutarQuery("SELECT top 1 E.EFD_DS_ENTIDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "CIUDAD16"
                ds = BD.EjecutarQuery("SELECT top 1 CI.CIU_NB_CIUDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "DELEGA_O_MUNICI54"
                ds = BD.EjecutarQuery("SELECT top 1 M.MUN_DS_MUNICIPIO FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "ESTADO51"
                ds = BD.EjecutarQuery("SELECT top 1 E.EFD_DS_ENTIDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "CIUDAD53"
                ds = BD.EjecutarQuery("SELECT top 1 CI.CIU_NB_CIUDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "DELEGA_O_MUNI87"
                ds = BD.EjecutarQuery("SELECT top 1 M.MUN_DS_MUNICIPIO FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "ESTADO88"
                ds = BD.EjecutarQuery("SELECT top 1 E.EFD_DS_ENTIDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "CIUDAD86"
                ds = BD.EjecutarQuery("SELECT top 1 CI.CIU_NB_CIUDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "DELEGA_O_MUNI124"
                ds = BD.EjecutarQuery("SELECT top 1 M.MUN_DS_MUNICIPIO FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "ESTADO120"
                ds = BD.EjecutarQuery("SELECT top 1 E.EFD_DS_ENTIDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "CIUDAD123"
                ds = BD.EjecutarQuery("SELECT top 1 CI.CIU_NB_CIUDAD FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
            Case "DELEGA_0_MUNI459"
                ds = BD.EjecutarQuery("SELECT top 1 M.MUN_DS_MUNICIPIO FROM PDK_CAT_CODPOSTAL  C INNER JOIN  PDK_CAT_EFEDERATIVA  E ON C.EFD_CL_CVE =E.EFD_CL_CVE INNER JOIN  PDK_CAT_CIUDAD CI ON CI.CIU_CL_CIUDAD=C.CIU_CL_CIUDAD AND CI.EFD_CL_CVE=E.EFD_CL_CVE INNER JOIN  PDK_CAT_MUNICIPIO  M ON M.MUN_CL_CVE=C.MUN_CL_CVE AND M.EFD_CL_CVE=C.EFD_CL_CVE where C.CPO_CL_CODPOSTAL =" & arraydepende(0) & "")
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

    <WebMethod()> _
    Public Function fillgv(ByVal valor As String, ByVal depende As String) As String

        Dim ds As New DataSet
        Dim text As String = ""
        Dim arraydepende As Array

        arraydepende = depende.Split(",")

        Select Case valor
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
                        ds = BD.EjecutarQuery("declare @cadena varchar(max)=''; if exists (select * from PDK_SCORING )begin select @cadena='<tr><td><div style=""overflow:auto;""><table  class = ""resulGrid"" style = ""height:150px"">';select @cadena=@cadena+'<tr><td><label id=""txt_'+LTRIM(a.PDK_ID_SECCION_DATO)+ '"" onmousedown=""Operador(id)"" class=""link"">'+b.PDK_SEC_DAT_NOMBRE +'</label></td></tr>' from PDK_SCORING a inner join PDK_SECCION_DATO b on a.PDK_ID_SECCION_DATO=b.PDK_ID_SECCION_DATO group by a.PDK_ID_SECCION_DATO , b.PDK_SEC_DAT_NOMBRE; select @cadena=@cadena+'</table></div></td><td valign=""middle""><input id=""cmbRegresar"" value=""<<"" type=""button"" class=""TextLink"" onclick=""fnBtnBorrar()""  runat=""server""/></td><td  valign=""middle""><table colspan=""3"" width = ""100%"" style = ""height:150px""><tr><td valign=""middle""><input id=""suma"" value=""+""  class=""TextLink ""  type=""button"" onclick=""Operadores(id)"" /><input id=""resta"" value=""-""  class=""TextLink"" type=""button"" onclick=""Operadores(id)"" /><input id=""division"" value=""/""  class=""TextLink"" type=""button"" onclick=""Operadores(id)"" /><input id=""multipli"" value=""*""  class=""TextLink"" type=""button"" onclick=""Operadores(id)"" /><input id=""paren1"" value=""(""  class=""TextLink"" type=""button"" onclick=""Operadores(id)"" /><input id=""paren2"" value="")""  class=""TextLink"" type=""button"" onclick=""Operadores(id)""/></td></tr><tr><td class=""camposFor"">Formula:</td></tr><tr><td><input id=""txtOperacion"" type=""text"" class=""resul"" style=""height:43px; width:300px; font-family: Verdana, Helvetica, Arial, sans-serif; overflow:auto; "" readonly=""readonly"" runat=""server"" /></td></tr><tr><td class=""camposFor"">Nombre:</td></tr><tr><td><input id=""txtNomFormula"" class=""resul"" onkeypress=""ManejaCar(''A'',1,this.value,this)""  style="" width:310px; font-family: Verdana, Helvetica, Arial, sans-serif;""  type=""text""/></td></tr><tr><td><input id = ""txtOperanum"" type = ""text"" style = ""display:none;"" /></td><td><input id=""BttAgregar"" value=""Guardar""   type=""button"" class=""Text"" onClick=""bntGuardar()"" runat=""server""/></td></tr></table></td></tr>';select @cadena=@cadena+'<tr><td colspan = ""6"" style= ""text-align:center"">Formula:<label id=""blbformula"" class=""link"" onmousedown=""fnmostrar()"">+</label><input id=""lblcve"" value=""0"" style=""display:none"" /></td></tr><tr id=""trFormula"" style = ""display:none""><td colspan=""6""><div style = ""width:100%; height:100px; overflow:auto;""><table id=""tb1formula"" style = ""width:100%;""  class = ""resulGrid"" >';select @cadena=@cadena +'<tr><th>Id</th><th>formula</th></tr>'; select @cadena=@cadena +'<tr><td><label id=""lbl_'+ ltrim(PDK_ID_FORMULA) +'"" onmousedown=""fnActualiza(this, '+ ltrim(PDK_ID_FORMULA) + ',''' + PDK_FORMULA_DATOS + ''',''' + replace(replace(replace(replace(replace(replace(PDK_FORMULA_GENERADA,'+','[+]'),'-','[-]'),'*','[*]'),'/','[/]'),'(','[(]'),')','[)]') + ''',''' + PDK_FORMULA_NOMBRE + ''')"">'+ ltrim(PDK_ID_FORMULA)+'</label></td><td><label id=""lbldat_'+ltrim(PDK_ID_FORMULA)+'"" >'+PDK_FORMULA_DATOS+'</label></td></tr>' from PDK_FORMULA WHERE PDK_TIPO_FORMULA=" & arraydepende(0) & "; select @cadena=@cadena+  '</table></div></td></tr>';select @cadena;  end  else  begin select @cadena; end")

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
                    'ds = BD.EjecutarQuery("declare @proceso int = " & arraydepende(1) & ", @solicitud int = " & arraydepende(0) & "; declare @nombreCliente varchar(100), @cadenatareas varchar(max); select @nombreCliente = ltrim(APELLIDO_PATERNO) + ' ' + ltrim(APELLIDO_MATERNO) + ' ' + ltrim(NOMBRE1) + ' ' + ltrim(NOMBRE2)from PDK_TAB_SOLICITANTE where PDK_ID_SECCCERO = @solicitud; set @cadenatareas = '<tr><th>Sol: ' + ltrim(@solicitud) + '</th><th colspan = ""2"">Nombre: ' + isnull(@nombreCliente, '') + '</th><th style = ""text-align: right;""><b><a onmousedown = ""ocultaVentanaFast($(''#Principal''), $(''#dvTareas''));"" class = ""link"" style = ""color:#FFFFFF"">X</a></b></th></tr><tr><th>TAREA</th><th>STATUS</th><th>FECHA INICIO</th><th>FECHA FIN</th></tr>'; if OBJECT_ID('tempdb..##tempURL') is not null drop table ##tempURL; select PDK_ID_SOLICITUD, PDK_ID_OPE_SOLICITUD, tar.PDK_ID_TAREAS, PDK_ID_PROCESOS, PDK_PANT_MOSTRAR, case when PDK_OPE_STATUS_TAREA in (39, 40) then CASE WHEN PDK_PANT_DOCUMENTOS in(26,68) THEN './consultaPantallaDocumentos.aspx?pantalla=' + CONVERT(VARCHAR,pant.PDK_ID_PANTALLAS)  + '&solicitud=' + convert(VARCHAR,sol.PDK_ID_SOLICITUD) + '&usu=' + ltrim(isnull(PDK_OPE_USU_ASIGNADO, 1)) when PDK_PANT_DOCUMENTOS = 44 THEN './consultaPantallaEntrevista.aspx?idPantalla=' + CONVERT(VARCHAR,pant.PDK_ID_PANTALLAS)  + '&IdFolio=' + convert(VARCHAR,sol.PDK_ID_SOLICITUD) + '&CVE=1&Enable=0' WHEN PDK_PANT_DOCUMENTOS = 69 THEN './altaNotificacion.aspx?idPantalla=' + CONVERT(VARCHAR,pant.PDK_ID_PANTALLAS)  + '&IdFolio=' + convert(VARCHAR,sol.PDK_ID_SOLICITUD) + '&CVE=1&Enable=0' ELSE './Blanco.aspx' + '?idFolio=' + CONVERT(VARCHAR,sol.PDK_ID_SOLICITUD) + '&idPantalla=' +  CONVERT(VARCHAR,pant.PDK_ID_PANTALLAS) END when PDK_OPE_STATUS_TAREA in (41, 42) then CASE WHEN PDK_PANT_DOCUMENTOS in(26,68) THEN './consultaPantallaDocumentos.aspx?pantalla=' + CONVERT(VARCHAR,pant.PDK_ID_PANTALLAS)  + '&solicitud=' + convert(VARCHAR,sol.PDK_ID_SOLICITUD) + '&usu=' + ltrim(isnull(PDK_OPE_USU_ASIGNADO, 1)) + '&Enable=1' when PDK_PANT_DOCUMENTOS = 44 THEN './consultaPantallaEntrevista.aspx?idPantalla=' + CONVERT(VARCHAR,pant.PDK_ID_PANTALLAS)  + '&IdFolio=' + convert(VARCHAR,sol.PDK_ID_SOLICITUD) + '&CVE=1&Enable=1' WHEN PDK_PANT_DOCUMENTOS = 69 THEN './altaNotificacion.aspx?idPantalla=' + CONVERT(VARCHAR,pant.PDK_ID_PANTALLAS)  + '&IdFolio=' + convert(VARCHAR,sol.PDK_ID_SOLICITUD) + '&CVE=1&Enable=1'  ELSE './Blanco.aspx' + '?idFolio=' + CONVERT(VARCHAR,sol.PDK_ID_SOLICITUD) + '&idPantalla=' +  CONVERT(VARCHAR,pant.PDK_ID_PANTALLAS) + '&Enable=1' END end url into ##tempURL from PDK_CAT_TAREAS tar left outer join PDK_OPE_SOLICITUD sol on tar.PDK_ID_TAREAS = sol.PDK_ID_TAREAS left outer join PDK_REL_PANTALLA_TAREA panttar on tar.PDK_ID_TAREAS = panttar.PDK_ID_TAREAS left outer join PDK_PANTALLAS pant on pant.PDK_ID_PANTALLAS = panttar.PDK_ID_PANTALLAS where PDK_ID_PROCESOS = @proceso and sol.PDK_ID_SOLICITUD = @solicitud and PDK_PANT_MOSTRAR = 2 order by PDK_ID_SOLICITUD, PDK_ID_OPE_SOLICITUD; select @cadenatareas = @cadenatareas + '<tr><td>' + PDK_TAR_NOMBRE + '</td><td><input type = ""image"" ' + 	case when PDK_OPE_STATUS_TAREA = 39 then 'src = ""../App_Themes/Imagenes/newfiles.jpg"" onmousedown = ""window.location.href = ''' + url + '''""/>' when PDK_OPE_STATUS_TAREA = 40 then 'src = ""../App_Themes/Imagenes/process.jpg"" onmousedown = ""window.location.href = ''' + url + '''""/></td>' when PDK_OPE_STATUS_TAREA = 41 then 'src = ""../App_Themes/Imagenes/ok.jpg"" onmousedown = ""window.location.href = ''' + url + '''""/></td>' when PDK_OPE_STATUS_TAREA = 42 then 'src = ""../App_Themes/Imagenes/cancel2.png"" onmousedown = ""window.location.href = ''' + url + '''""/></td>' else '/><td></td>' end + '<td>' + convert(varchar(10), PDK_OPE_FECHA_INICIO, 120) + ' ' + convert(varchar(5), PDK_OPE_FECHA_INICIO, 114) + '</td><td>' + convert(varchar(10), isnull(PDK_OPE_FECHA_FINAL, ''), 120) + ' ' + convert(varchar(5), isnull(PDK_OPE_FECHA_FINAL, ''), 114) + '</td></tr>' from PDK_CAT_TAREAS tar left outer join PDK_OPE_SOLICITUD sol on tar.PDK_ID_TAREAS = sol.PDK_ID_TAREAS left outer join ##tempURL turl on tar.PDK_ID_TAREAS = turl.PDK_ID_TAREAS and tar.PDK_ID_PROCESOS = turl.PDK_ID_PROCESOS and sol.PDK_ID_SOLICITUD = turl.PDK_ID_SOLICITUD and sol.PDK_ID_TAREAS = turl.PDK_ID_TAREAS and sol.PDK_ID_OPE_SOLICITUD = turl.PDK_ID_OPE_SOLICITUD where tar.PDK_ID_PROCESOS = @proceso and sol.PDK_ID_SOLICITUD = @solicitud and turl.PDK_PANT_MOSTRAR = 2 order by sol.PDK_ID_SOLICITUD, sol.PDK_ID_OPE_SOLICITUD; select @cadenatareas;")
                    ds = BD.EjecutarQuery("exec fillTareas " & arraydepende(0) & ", " & arraydepende(1) & ", " & arraydepende(2))
                End If
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

End Class