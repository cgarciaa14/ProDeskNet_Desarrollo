'BBVA-P-423:RQCONYFOR-05 JRHM 23/11/16 se cambio el texto de lblNomPantalla para mostrar el nombre de la pantalla y no el nombre de la tarea a realizar.
'BBV-P-423: RQ H - CSS JRHM 09/12/2016 Se modificaron clases de estilos para asemejarce a el cotizador
'BBV-P-423: 04/01/17: JRHM RQCONYFOR-06 Se modifico el metodo jsonValidaAgencia para llamar metodo de clsValidaBloqueos
'BBV-P-423 20/01/2017: JRHM RQCONYFOR-02: Se crearon nuevos metodos para la tarea de "Documentación de Pólizas de Seguros y Desembolso"
'BUG-PD-13  GVARGAS  01/03/2017 Cambio WCF Credenciales
'BUG-PD-17 JRHM 16/03/17 Se modifica como avanza la tarea el webmethod de guarda datos de seguro 
'BBV-P-423: RQADM-22: JRHM: 24/03/17 CORRIGE PROBLEMA AL GUARDAR CUESTIONARIO DE BROKERS EN EL SERVIDOR
'BUG-PD-22 JBB 30/03/2017 Se obtiene la siguiente tarea para poder redireccionar de manera correcta si es automatica.
'BUG-PD-25 JRHM 04/04/17 SE MODIFICA PANTALLA PARA LA SOLUCION DE ERRORES EN LA VALIDACION DE DOCUMENTOS, AGENCIA BLOQUEADA Y TAREAS AUTOMATICAS
'BUG-PD-33 JRHM 24/04/17 SE AGREGA FUNCIONALIDAD PARA EMISION DE POLIZAS DE SEGURO Y OPCIONES DE TURNAR PARA TAREAS DE CHECK DOCUMENTAL E INS CHECK DOCUMENTAL
'BUG-PD-42:RHERNANDEZ:04/05/17:SE CORRIGE ERROR AL IMPRIMIR POLIZA DE SEGURO DE EIKOS
'BUG-PD-45:RHERNANDEZ:08/05/17: SE ARREGLO PROBLEMA CON SERVICIO DE INGESTA DOCUMENTOS PARA LA CARGA DE POLIZAS AL SERVICIO DE INGESTA SE MODIFICARON MENSAJES Y SE AGREGO DEFINICION DE PROXY PARA CORRECTA EMISION DE EIKOS EN LADO DEL SERVIDOR
'BUG-PD-68: RHERNANDEZ: 02/06:17: SE MODIFICA EL CODIGO BACK PARA PPODER EMITIR EN ORDAS E IMPRIMIR SU RESPECTIVA POLIZA DE SEGURO DE DAÑOS.
'BUG-PD-81: RHERNANDEZ: 10/06/17: SE AGREGA EMISION DE SEGUROS DE VIDA Y DAÑOS BBVA
'BUG-PD-98: RHERNANDEZ: 20/06/17: SE MODIFICA JS PARA BLOQUEAR BOTONES UPLOAD
'BUG-PD-116: RHERNANDEZ: 26/06/17: SE AGREGA LA CONTESTACION DE LA ENTREVISTA DE SALUD PARA LOS SEGUROS DE VIDA QUE SON EMITIDOS POR BBVA
'BUG-PD-128: RHERNANDEZ: 01/07/17: SE SOLUCIONAN PROBLEMAS DE EMISION CON BROKERS 
'RQ-INB206: ERODRIGUEZ  10/07/2017 Se agrego consulta para Monto aprobado
'BUG-PD-152: ERODRIGUEZ: 12/07/2017 Se valido Monto a financiar para cuando traiga nulos.
'BBV-P-423:RQ-INB215: AVEGA: 24/07/2017 SE AGREGA BOTON IMPRIMIR + Cambio Urgente JRHM
'BUG-PD-174: RHERNANDEZ: 28/07/17 SE CAMBIAN IDS DE AGENCIAS DE LA EMISION E IMPRESION DE POLIZAS DE SEGURO
'BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN
'BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS DE POLIZAS PARA EL VISOR DE TELEPRO
'BUG-PD-208: RHERNANDEZ: 11/09/17: SE MODIFICA LECTURA DE MENSAJE DE AGENCIAS BLOQUEADAS PARA CAPTAR SOLO UNA PARTE DEL MENSAJE  QUE DEVUELVE
'BUG-PD-203: CGARCIIA: 04/09/2017: SE AGREGA NOTIFICACIONES 
'BUG-PD-215: CGARCIA: 26/09/2017: ACTUALIZACION DE CORREOS PARA CASOS AL QUE ENTRA.
'BUG-PD-213: RHERNANDEZ: 29/09/2017: SE CORRIGE PROBLEMA REDIRECT AL NO ENVIAR MENSAJE
'BUG-PD-218: CGARCIA: 29/09/2017: ACTUALIZACION DE EMAIL
'BUG-PD-227: CGARCIA: 05/10/2017: ACTUALIZACION DE EMAILS Y SCORE
'BUG-PD-235: RHERNANDEZ: 16/10/2017: En el flujo de emision bbva se evalua si el telefono fijo va vacio y en numero interior se limita la longitud a 10 caracteres
'BUG-PD-236: CGARCIA: 17/10/2017: SE AGREGA OPCION DE PANTALLA 105 PARA QUE EL ENVIO DE EMAIL SE HAGA EFECTIVO
'BUG-PD-244: CGARCIA: 23/10/2017: SE AGREGA VALIDACION DE PANTALLA 105 DE CHECK DOCUMENTAL REPLICA
'BUG-PD-246: RHERNANDEZ: 25/10/17: SE TOMA UN VEHICULO DEFAULT PARA LA SIMULACION DE SEGURO DE VIDA BBVA
'BUG-PD-259: RIGLESIAS: 09/11/17: CONCATENACIÓN DE Request("usu") EN EL METODO asignaTarea
'BUG-PD-271: RIGLESIAS: 27/11/2017: SE AGREGO OPCIÓN DE NO PROECESBALE EN LAS OPCIONES  DE TURNAR 
'BUG-PD-275: RHERNANDEZ: 27/11/17: SE TRUNCA  LA OCUPACION DE LOS DATOS QUE SE LE ENVIA AL SERVICIO DE EMISION DE EIKOS
'BUG-PD-276: CGARCIA: 27/11/2017: CORRECCION DE MSJ Y REQUEST DE USUARIOS EN EMAILS
'BUG-PD-299: RHERNANDEZ: 13/12/17: SE CAMBIA LA FORMA EN QUE SE ALMACENA EL ARCHIVO EN DE BASE64 A UN ARCHIVO GZ EN VARBINARY 
'BUG-PD-312: RHERNANDEZ: 18/12/17: sE CONFIGURA LA OPCION NO PROCESABLE PARA LA PANTALLA DE INS CHECK DOCUMENTAL
'BUG-PD-322: ERODRIGUEZ: 03/01/2017: Se cambio el redireccionamiento al panel de control		   
'BUG-PD-320: REHERNANDEZ: 26/12/17: Se valida si la poliza de seguros de danios marsh ya ha sido emitida.
'BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit
'BUG-PD-388: DJUAREZ: Modificar URL de respuesta para corregir F5 en tarea manual  
'BUG-PD-360: RHERNANDEZ: 19/02/18: SE AGREGA LA LECTURA DE IDQUOTE DE SEGURO DE VIDA PARA EMITIR SEGUROS INDEPENDIENTEMENTE
'BUG-PD-363: JMENDIETA: 20/02/18: Campo adicional servicio re-cotización BBVA en particular data se agrega el Numero de Crédito
'BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit
'BUG-PD-371: JMENDIETA: 26/02/18: Campo adicional servicio re-cotización BBVA en particular data se agrega el Tipo de Unidad
'RQ-PD30: JMENDIETA: 05/03/2018: Para el tipo de producto 1(AUTO) la alianza sera VINCF003 y el plan 046, para producto 2(Moto) alianza sera VINCF004 y el plan 047
'BUG-PD-374: RHERNANDEZ: 27/02/17: SE CORRIGE CONTROL DE EXCEPCIONES AL NO ENCONTRAR COLONIA VALIDA EN EMISION DE SEGUROS BBVA
'BUG-PD-385 RHERNANDEZ: 07/03/2018: SE QUITA VALIDACION CORRECTA DE MENSAJE 03062 EN EMISION MARSH
'BUG-PD-403: RHERNANDEZ: 20/03/2018: Se modifica proceso de emision de marsh encuanto a fecha fin que es la fecha inicial mas el plazo y mas 16 dias
'RQ-PD31: DJUAREZ: 08/03/2018: SE CREA POPUP PARA MODIFICAR LA COLONIA CUANDO SE GUARDE LA COLONIA "OTRO"
'BUG-PD-417: DCORNEJO: 17/04/2018: SE AGREGA LA OPCION DE TURNAR EN ADJUNTAR DOCUMENTOS PARA DESEMBOLSO
'BUG-PD-432: JMENDIETA 02/05/2018: Al recotizar vida se envian la fecha inicio y fin de la tabla de amortizacion.
'BUG-PD-435: DCORNEJO 08/05/2018: Modificacion en validaciones en tnproc_Click (ROBERTO HERNANDEZ)
'BUG-PD-448: JMENDIETA: 18/05/2018: Cuando el broker sea Marsh se omite validacion de que la fecha de vencimiento del seguro de daños no sea menor a la fecha de termino del contrato.
'BUG-PD-447: JMENDIETA: 23/05/2018: Se agrega validación para el parametro SERVICIO INGESTA DOCUMENTOS que indica si se consume el servicio de ingesta o no. Para impresion Marsh de envia el id agencia.
'RQ-PD35-2: 29/05/2018: CGARCIA: SE AGREGA OPCION PARA PANTALLA 88
Imports ProdeskNet.BD
Imports ProdeskNet.Catalogos
Imports System.Data
Imports ProdeskNet.WCF
Imports ProdeskNet.SN
Imports System.Data.SqlClient
Imports System.Net
Imports System.IO
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Seguridad
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Diagnostics
Imports System.IO.Compression

#Region "trackers"
'INC-B-2019:JDRA:Regresar
#End Region

Public Class consultaPantallaDocumentos
    Inherits System.Web.UI.Page
    Public ClsEmail As New clsEmailAuto()
    Dim BD As New clsManejaBD
    Dim clien As New clsDatosCliente
    Dim sol As New clsStatusSolicitud
    Dim clsCat As New clsCatalogos
    Dim usu As String
    Private Const Marsh As Short = 4 'BUG-PD-448

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Validacion de Request
        Try
            If Request("Enable") = 0 Then
                Dim Validate As New clsValidateData
                Dim Url As String = Validate.ValidateRequest(Request)

                If Url <> String.Empty Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", Url, True)
                    Exit Sub
                End If
            End If
        Catch

        End Try
        'Fin validacion de Request

        clien.GetDatosCliente(Request("solicitud"))
        sol.getStatusSol(Request("solicitud"))

        Dim intEnable As Integer
        Dim dsresult As New DataSet

        hdPantalla.Value = Request("pantalla")
        hdSolicitud.Value = Request("solicitud")
        hdusuario.Value = Request("usu")
        Me.lblSolicitud.Text = Request("solicitud")
        Me.lblCliente.Text = clien.propNombreCompleto
        Me.lblStCredito.Text = sol.PStCredito
        Me.lblStDocumento.Text = sol.PStDocumento

        usu = Val(Request("usuario"))
        If usu = String.Empty Then
            usu = Val(Request("usu"))
        End If

        ConsultaCapacidadPago()

        If hdPantalla.Value = 9 Then
            Me.btnImprimir.Visible = True
        Else
            Me.btnImprimir.Visible = False

        End If

        Session.Add("idSol", hdSolicitud.Value)

        If Session("Regresar") Is Nothing Then
            Session("Regresar") = Request.UrlReferrer.LocalPath
        End If

        hdRutaEntrada.Value = Session("Regresar")
        Try
            dsresult = BD.EjecutarQuery("EXEC sp_GenerarRura  " & hdSolicitud.Value & "," & hdPantalla.Value & "," & hdusuario.Value & "")
            If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
                hdnResultado.Value = dsresult.Tables(0).Rows(0).Item("RUTA")
                hdnResultado1.Value = dsresult.Tables(1).Rows(0).Item("RUTA2")
                hdnResultado2.Value = dsresult.Tables(2).Rows(0).Item("RUTA3")
            End If

        Catch ex As Exception
            hdnResultado.Value = Session("Regresar")
        End Try

        Try
            dsresult = BD.EjecutarQuery("get_Path_Next_Tarea  " & hdPantalla.Value)
            If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
                Dim Mostrar As String
                Dim pantallas As String
                Mostrar = dsresult.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR").ToString
                pantallas = dsresult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString
                If Mostrar = 2 Then

                    hdnResultado.Value = dsresult.Tables(0).Rows(0).Item("RUTA")
                Else
                    hdnResultado.Value = (dsresult.Tables(0).Rows(0).Item("RUTA") & "?sol=" & hdSolicitud.Value & "&IdPantalla=" & pantallas & "&usuario=" & hdusuario.Value)
                    hdnResultado2.Value = (dsresult.Tables(0).Rows(0).Item("RUTA") & "?sol=" & hdSolicitud.Value & "&IdPantalla=" & pantallas & "&usuario=" & hdusuario.Value)
                End If
            End If

        Catch ex As Exception
            hdnResultado.Value = Session("Regresar")
        End Try
        Try
            intEnable = CInt(Request.QueryString("Enable"))

        Catch ex As Exception
            intEnable = 0
        End Try

        If intEnable = 1 Then
            cmbguardar1.Attributes.Add("style", "display:none;")
            'btnAutorizar.Attributes.Add("style", "display:none;")
            btnCancelar.Attributes.Add("style", "display:none;")

        End If
        dsresult = BD.EjecutarQuery("select A.PDK_PANT_NOMBRE,B.PDK_PAR_SIS_PARAMETRO,A.PDK_PANT_DOCUMENTOS from PDK_PANTALLAS A INNER JOIN PDK_PARAMETROS_SISTEMA B ON A.PDK_PANT_DOCUMENTOS=B.PDK_ID_PARAMETROS_SISTEMA AND B.PDK_PAR_SIS_ID_PADRE=24 where A.PDK_ID_PANTALLAS = " & hdPantalla.Value)
        If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
            hdNomPantalla.Value = dsresult.Tables(0).Rows(0).Item("PDK_PAR_SIS_PARAMETRO").ToString
            hntipoPantalla.Value = dsresult.Tables(0).Rows(0).Item("PDK_PANT_DOCUMENTOS").ToString
        End If

        Me.lblNomPantalla.Text = dsresult.Tables(0).Rows(0).Item("PDK_PANT_NOMBRE").ToString

        If Not IsPostBack Then
            If Request("pantalla").ToString = "63" And intEnable = 0 Then
                Session("RutapolDaños") = ""
                Session("RutapolVida") = ""
                lblturnar.Visible = True
                ddlTurnar.Visible = True
                Dim clsquiz As New clsCuestionarioSolvsID()
                Dim objCombo As New clsParametros
                clsquiz._ID_PANT = CInt(hdPantalla.Value)
                Dim dtsres As New DataSet
                dtsres = clsquiz.getTurnar()
                If dtsres.Tables.Count > 0 Then
                    If dtsres.Tables(0).Rows.Count > 0 Then
                        objCombo.LlenaCombos(dtsres, "TURNAR_NOMBRE", "PDK_ID_TAREAS", ddlTurnar, True, True)
                    End If
                End If

                DatosBrokers.Attributes.Add("style", "display:''")
                Dim clsseg As New clsSeguros
                Dim res As DataSet
                clsseg._ID_SOLICITUD = CInt(hdSolicitud.Value)
                res = clsseg.GetDatosPoliza()
                If res.Tables.Count > 0 Then
                    If res.Tables(0).Rows.Count > 0 Then
                        If txtNomSegDanios.Text <> "" Then
                            txtNomSegDanios.Text = res.Tables(0).Rows(0).Item("PDK_NOM_SEG_AUTO")
                        End If
                        If txtNumSegDanios.Text <> "" Then
                            txtNumSegDanios.Text = res.Tables(0).Rows(0).Item("PDK_NO_POLIZA_SEG_AUTO")
                        End If
                        If txtVigSegDanios.Text <> "" Then
                            txtVigSegDanios.Text = CDate(res.Tables(0).Rows(0).Item("PDK_VIG_SEGAUTO")).ToString("dd/MM/yyyy")
                        End If
                        If txtNomSegVida.Text <> "" Then
                            txtNomSegVida.Text = res.Tables(0).Rows(0).Item("PDK_NOM_SEG_VIDA")
                        End If
                        If txtNumSegVida.Text <> "" Then
                            txtNumSegVida.Text = res.Tables(0).Rows(0).Item("PDK_NO_POLIZA_SEG_VIDA")
                        End If
                        If txtVigSegVida.Text <> "" Then
                            txtVigSegVida.Text = CDate(res.Tables(0).Rows(0).Item("PDK_VIG_SEG_VIDA")).ToString("dd/MM/yyyy")
                        End If
                    End If
                End If
                ProcesoSeguros(0)
            ElseIf Request("pantalla").ToString = "63" And intEnable = 1 Then
                DatosBrokers.Attributes.Add("style", "display:''")
                Dim clsseg As New clsSeguros
                Dim res As DataSet
                clsseg._ID_SOLICITUD = CInt(hdSolicitud.Value)
                res = clsseg.GetDatosPoliza()
                If res.Tables.Count > 0 Then
                    If res.Tables(0).Rows.Count > 0 Then
                        txtNomSegDanios.Text = res.Tables(0).Rows(0).Item("PDK_NOM_SEG_AUTO")
                        txtNumSegDanios.Text = res.Tables(0).Rows(0).Item("PDK_NO_POLIZA_SEG_AUTO")
                        If res.Tables(0).Rows(0).Item("PDK_VIG_SEGAUTO").ToString <> "" Then
                            txtVigSegDanios.Text = CDate(res.Tables(0).Rows(0).Item("PDK_VIG_SEGAUTO")).ToString("dd/MM/yyyy")
                        End If
                        txtNomSegVida.Text = res.Tables(0).Rows(0).Item("PDK_NOM_SEG_VIDA")
                        txtNumSegVida.Text = res.Tables(0).Rows(0).Item("PDK_NO_POLIZA_SEG_VIDA")
                        If res.Tables(0).Rows(0).Item("PDK_VIG_SEG_VIDA").ToString <> "" Then
                            txtVigSegVida.Text = CDate(res.Tables(0).Rows(0).Item("PDK_VIG_SEG_VIDA")).ToString("dd/MM/yyyy")
                        End If
                        txtNomSegDanios.Enabled = False
                        txtNumSegDanios.Enabled = False
                        txtVigSegDanios.Enabled = False
                        txtNomSegVida.Enabled = False
                        txtNumSegVida.Enabled = False
                        txtVigSegVida.Enabled = False
                    End If
                End If
            ElseIf (Request("pantalla").ToString = "74" Or Request("pantalla").ToString = "89" Or Request("pantalla").ToString = "9" Or Request("pantalla").ToString = "8") And intEnable = 0 Then
                divActualizaColonia.Attributes.Add("style", "display:''")
                lblturnar.Visible = True
                ddlTurnar.Visible = True
                Dim clsquiz As New clsCuestionarioSolvsID()
                Dim objCombo As New clsParametros
                clsquiz._ID_PANT = CInt(hdPantalla.Value)
                Dim dtsres As New DataSet
                dtsres = clsquiz.getTurnar()
                If dtsres.Tables.Count > 0 Then
                    If dtsres.Tables(0).Rows.Count > 0 Then
                        objCombo.LlenaCombos(dtsres, "TURNAR_NOMBRE", "PDK_ID_TAREAS", ddlTurnar, True, True)
                    End If
                End If
            ElseIf Request("pantalla").ToString = "88" And intEnable = 0 Then 'RQ-PD35-2: 29/05/2018: CGARCIA: SE AGREGA OPCION PARA PANTALLA 88
                lblturnar.Visible = True
                ddlTurnar.Visible = True
                Dim clsquiz As New clsCuestionarioSolvsID()
                Dim objCombo As New clsParametros
                clsquiz._ID_PANT = CInt(hdPantalla.Value)
                Dim dtsres As New DataSet
                dtsres = clsquiz.getTurnar()
                If dtsres.Tables.Count > 0 Then
                    If dtsres.Tables(0).Rows.Count > 0 Then
                        objCombo.LlenaCombos(dtsres, "TURNAR_NOMBRE", "PDK_ID_TAREAS", ddlTurnar, True, True)
                    End If
                End If
            End If
        End If

    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click

        'Response.Redirect("./consultaPantalla.aspx")
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("solicitud")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)

    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function jsonValidaAgencia(ByVal id_sol As String) As String
        Try
            Dim dsres As DataSet
            Dim BD As New clsManejaBD
            Dim objVal As clsValidaBloqueos = New clsValidaBloqueos
            dsres = objVal.Validacion_Agencia(CInt(id_sol.ToString()))

            Dim id_agencia As Integer = 0
            Dim Valida_Agencia As Integer = 3
            Dim StatusAgencia As String

            If (dsres.Tables(0).Rows.Count >= 0) Then
                id_agencia = IIf(dsres.Tables(0).Rows(0).Item("Agencia").ToString = "", 0, dsres.Tables(0).Rows(0).Item("Agencia"))
                Valida_Agencia = dsres.Tables(0).Rows(0).Item("ValidaAgenciaBloqueada")
                If (Valida_Agencia = 0) Then
                    Throw New Exception("Falta cargar el parametro sistema para validacion de servicios BBVA")
                ElseIf (Valida_Agencia = 3) Then
                    StatusAgencia = "AGENCIA NO BLOQUEADA"
                    Return StatusAgencia
                Else
                    If (id_agencia = 0) Then
                        Throw New Exception("La solucitud no tiene una Agencia ligada")
                    End If
                    Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                    Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

                    Dim restful As RESTful = New RESTful()
                    restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriValidaAgencia").ToString() + id_agencia.ToString()
                    'restful.consumerID = "10000004"
                    restful.buscarHeader("ResponseWarningDescription")
                    Dim respuesta As String = restful.ConnectionGet(userid, idticket, "")

                    Dim errormessage As String
                    If (restful.IsError) Then
                        errormessage = restful.MensajeError
                        Throw New Exception("Falla en Servicio Web: " + errormessage)
                    Else
                        Dim arr = restful.valorHeader.ToString().Substring(0, 20)
                        StatusAgencia = arr(0).ToString
                        Return StatusAgencia
                    End If
                End If
            Else
                Throw New Exception("BD no tiene valores para procesar tarea")
            End If
        Catch ex As Exception
            Return "ERROR: " + ex.Message.ToString()
        End Try
    End Function

    'Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
    '    Dim BD As New ProdeskNet.BD.clsManejaBD
    '    Dim dsresult As New DataSet
    '    Dim solicitud As String = Request("solicitud")

    '    Try

    '        Try
    '            dsresult = BD.EjecutarQuery("EXEC sp_GenerarRura  " & hdSolicitud.Value & "," & hdPantalla.Value & "," & hdusuario.Value & "")
    '            If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
    '                hdnResultado.Value = dsresult.Tables(0).Rows(0).Item("RUTA")
    '            End If


    '        Catch ex As Exception
    '            hdnResultado.Value = Session("Regresar")
    '        End Try

    '        dsresult = Nothing

    '        dsresult = BD.EjecutarQuery("exec spvalNegocio " & solicitud.ToString & ",64," & hdusuario.Value)

    '        Master.MensajeError("Registro actualizado correctamente")
    '        Response.Redirect(hdnResultado.Value, False)
    '    Catch ex As Exception
    '        Master.MensajeError(ex.Message)
    '    End Try
    'End Sub

    'Public Sub fillgvValidaDocumentos(ByVal solicitud As String, ByVal pantalla As String)
    '    gvValidaDocumentos.DataSource = BD.EjecutarQuery("declare @solicitud int = " & solicitud & ", @pantalla int = " & pantalla & "; if (select count(*) from PDK_REL_PAN_DOC_SOL where PDK_ID_SECCCERO = @solicitud) > 0 begin	select isnull(doc.PDK_ID_REL_PAN_DOC, 0) as PDK_ID_REL_PAN_DOC, cd.PDK_ID_DOCUMENTOS, PDK_DOC_NOMBRE, isnull(PDK_LNK_IMAGEN, '')PDK_LNK_IMAGEN, isnull(PDK_REL_ACT_STATUS, 0) as PDK_DOC_ACTIVO, isnull(dsol.PDK_ST_ENTREGADO, 0) PDK_ST_ENTREGADO, isnull(dsol.PDK_ST_VALIDADO, 0) PDK_ST_VALIDADO from PDK_CAT_DOCUMENTOS cd left outer join PDK_REL_PAN_DOC doc on cd.PDK_ID_DOCUMENTOS=doc.PDK_ID_DOCUMENTOS and doc.PDK_ID_PANTALLAS = @pantalla left outer join (select a.PKD_ID_DOCUMENTOS, PDK_LNK_IMAGEN, PDK_ST_ENTREGADO, PDK_ST_VALIDADO from PDK_REL_PAN_DOC_SOL a inner join (select max(PDK_ID_DOC_SOLICITUD)PDK_ID_DOC_SOLICITUD, PDK_ID_SECCCERO, PKD_ID_DOCUMENTOS, PDK_ID_REL_PAN_DOC from PDK_REL_PAN_DOC_SOL group by PDK_ID_SECCCERO, PKD_ID_DOCUMENTOS, PDK_ID_REL_PAN_DOC)b on a.PDK_ID_DOC_SOLICITUD = b.PDK_ID_DOC_SOLICITUD and a.PKD_ID_DOCUMENTOS = b.PKD_ID_DOCUMENTOS and a.PDK_ID_DOC_SOLICITUD = b.PDK_ID_DOC_SOLICITUD and a.PDK_ID_REL_PAN_DOC = b.PDK_ID_REL_PAN_DOC where a.PDK_ID_SECCCERO = @solicitud) dsol on doc.PDK_ID_DOCUMENTOS = dsol.PKD_ID_DOCUMENTOS where PDK_REL_ACT_STATUS = 1 end else begin select isnull(doc.PDK_ID_REL_PAN_DOC, 0) as PDK_ID_REL_PAN_DOC, cd.PDK_ID_DOCUMENTOS, PDK_DOC_NOMBRE, ''PDK_LNK_IMAGEN, isnull(PDK_REL_ACT_STATUS, 0) as PDK_DOC_ACTIVO, convert(bit, 0) PDK_ST_ENTREGADO, convert(bit, 0) PDK_ST_VALIDADO from PDK_CAT_DOCUMENTOS cd left outer join PDK_REL_PAN_DOC doc on cd.PDK_ID_DOCUMENTOS=doc.PDK_ID_DOCUMENTOS and doc.PDK_ID_PANTALLAS = @pantalla	where PDK_REL_ACT_STATUS = 1 end")
    '    gvValidaDocumentos.DataBind()
    'End Sub
    Public Function ImprimirSeguros(ByVal Id_Seguro As Integer, Optional ByVal id_poliza As String = "", Optional ByVal id_agencia As String = "") As Boolean
        ImprimirSeguros = False
        Select Case (Id_Seguro)
            Case -1 'Vida BBVA
                If ImprimirBBVA(id_poliza, 1) Then
                    ImprimirSeguros = True
                Else
                    Exit Function
                End If
            Case 2 'ORDAS
                If ImprimirORDAS(id_poliza) Then
                    ImprimirSeguros = True
                Else
                    Exit Function
                End If
            Case 3 'EIKOS
                If ImprimirEIKOS(id_agencia, id_poliza) Then 'CAMBIOS URGENTES RHERNANDES 
                    ImprimirSeguros = True
                Else
                    Exit Function
                End If
            Case 4 'MARSH
                If ImprimirMARSH(id_agencia, id_poliza) Then 'CAMBIOS URGENTES RHERNANDES 
                    ImprimirSeguros = True
                Else
                    Exit Function
                End If
            Case 5, 9 'SEGURO_BBVA
                If ImprimirBBVA(id_poliza, 0) Then
                    ImprimirSeguros = True
                Else
                    Exit Function
                End If
            Case Else
                ImprimirSeguros = True
        End Select
    End Function
    Public Function ImprimirORDAS(ByVal policyid As String) As Boolean
        Try
            ImprimirORDAS = False
            Dim ServImpORDAS As clsImpBrokerORDAS = New clsImpBrokerORDAS()
            ServImpORDAS.policy.complement.user.id = System.Configuration.ConfigurationManager.AppSettings("useridOrdas").ToString()
            ServImpORDAS.policy.complement.user.credentials.accessPassword = System.Configuration.ConfigurationManager.AppSettings("passwordOrdas").ToString()
            ServImpORDAS.policy.policyId = txtNumSegDanios.Text.ToString
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonbody As String = serializer.Serialize(ServImpORDAS)
            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriImpORDAS").ToString()
            'restful.consumerID = "10000004"
            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
            Dim errormessage As String
            If (restful.IsError) Then
                errormessage = restful.MensajeError
                Throw New Exception(errormessage)
            Else
                Dim res As clsResImpBrokerORDAS = serializer.Deserialize(Of clsResImpBrokerORDAS)(respuesta)
                'If GuardarPDFURL(res.policy.urlPolicy, 2) Then}
                Dim s As String = "window.open('" & res.policy.urlPolicy.ToString & "', '_blank');" 'Cambio Urgente JRHM
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "redirectpolicyordas", s, True) 'Cambio Urgente JRHM
                ImprimirORDAS = True
                'Else
                '    Throw New Exception("Problema al Guardar PDF en servidor")
                '    ImprimirORDAS = False
                'End If
            End If
        Catch ex As Exception
            Master.MensajeError("Falla Servicio: " + ex.Message)
            ImprimirORDAS = False
        End Try
    End Function
    Public Function ImprimirEIKOS(ByVal agencyNumber As String, ByVal policyid As String) As Boolean
        Try
            ImprimirEIKOS = False
            Dim ServImpEIKOS As clsImpBrokerEIKOS = New clsImpBrokerEIKOS()
            ServImpEIKOS.policy.complement.user.id = System.Configuration.ConfigurationManager.AppSettings("useridEikos").ToString()
            ServImpEIKOS.policy.complement.user.credentials.accessPassword = System.Configuration.ConfigurationManager.AppSettings("accessPasswordEikos").ToString()
            ServImpEIKOS.policy.agencyNumber = agencyNumber
            ServImpEIKOS.policy.policyId = txtNumSegDanios.Text.ToString
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonbody As String = serializer.Serialize(ServImpEIKOS)
            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriImpEIKOS").ToString()
            'restful.consumerID = "10000004"
            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
            Dim errormessage As String
            If (restful.IsError) Then
                errormessage = restful.MensajeError
                Throw New Exception(errormessage)
            Else
                Dim res As clsResImpBrokerEIKOS = serializer.Deserialize(Of clsResImpBrokerEIKOS)(respuesta)
                'If GuardarPDFURL(res.policy.urlPolicy, 3) Then
                Dim s As String = "window.open('" & res.policy.urlPolicy.ToString & "', '_blank');" 'Cambio Urgente JRHM
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "redirectpolicyeikos", s, True) 'Cambio Urgente JRHM
                ImprimirEIKOS = True
                'Else
                '    Throw New Exception("Problema al Guardar PDF en servidor")
                '    ImprimirEIKOS = False
                'End If
            End If
        Catch ex As Exception
            Master.MensajeError("Falla Servicio: " + ex.Message)
            ImprimirEIKOS = False
        End Try
    End Function
    Public Function ImprimirMARSH(ByVal agencyNumber As String, policyid As String) As Boolean
        Try
            ImprimirMARSH = False
            Dim ServImpMARSH As clsImpBrokerMARSH = New clsImpBrokerMARSH()
            ServImpMARSH.policy.complement.user.id = System.Configuration.ConfigurationManager.AppSettings("useridMarsh").ToString()
            ServImpMARSH.policy.complement.user.credentials.accessPassword = System.Configuration.ConfigurationManager.AppSettings("passwordMarsh").ToString()
            ServImpMARSH.policy.agencyNumber = agencyNumber
            ServImpMARSH.policy.policyId = txtNumSegDanios.Text.ToString
            ServImpMARSH.policy.printingData.subsection = "1"
            ServImpMARSH.policy.printingData.endorsement = "0"
            ServImpMARSH.policy.printingData.requestDate = (Date.Now.Date).ToString("yyyy-MM-dd")
            ServImpMARSH.policy.printingData.reference1 = ""
            ServImpMARSH.policy.printingData.reference2 = ""
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonbody As String = serializer.Serialize(ServImpMARSH)
            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriImpMARSH").ToString()
            'restful.consumerID = "10000004"
            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
            Dim errormessage As String
            If (restful.IsError) Then
                errormessage = restful.MensajeError
                Throw New Exception(errormessage)
            Else
                Dim res As clsResImpBrokerMARSH = serializer.Deserialize(Of clsResImpBrokerMARSH)(respuesta)
                If GuardarPDKB64(res.policy.printingData.policyCode, policyid, 1) Then
                    ImprimirMARSH = True
                Else
                    Throw New Exception("Problema al Guardar PDF en servidor")
                    ImprimirMARSH = False
                End If

            End If
        Catch ex As Exception
            Master.MensajeError("Falla Servicio: " + ex.Message)
            ImprimirMARSH = False
        End Try
    End Function
    Public Function ImprimirBBVA(ByVal id_cotizacion As String, Optional ByVal segvida As Integer = 0) As Boolean
        Try
            ImprimirBBVA = False
            Dim clsimpBBVA As clsBrokerBBVA = New clsBrokerBBVA()
            clsimpBBVA.header.aapType = System.Configuration.ConfigurationManager.AppSettings("aapTypeBBVA").ToString()
            clsimpBBVA.header.dateRequest = Date.Now.ToString("yyyy-MM-dd hh:mm:ss")
            clsimpBBVA.header.channel = System.Configuration.ConfigurationManager.AppSettings("channelBBVA").ToString()
            clsimpBBVA.header.subChannel = System.Configuration.ConfigurationManager.AppSettings("subChannelBBVA").ToString()
            clsimpBBVA.header.branchOffice = System.Configuration.ConfigurationManager.AppSettings("branchOfficeBBVA").ToString()
            clsimpBBVA.header.managementUnit = System.Configuration.ConfigurationManager.AppSettings("managementUnitBBVA").ToString()
            clsimpBBVA.header.user = System.Configuration.ConfigurationManager.AppSettings("userBBVA").ToString()
            clsimpBBVA.header.idSession = "3232-3232"
            clsimpBBVA.header.idRequest = "1212-121212-12121-212"
            clsimpBBVA.header.dateConsumerInvocation = Date.Now.ToString("yyyy-MM-dd hh:mm:ss")
            clsimpBBVA.quote.idQuote = id_cotizacion
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonbody As String = serializer.Serialize(clsimpBBVA)
            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriImpBBVA").ToString()
            'restful.consumerID = "10000004"
            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
            Dim errormessage As String
            If (restful.IsError) Then
                errormessage = restful.MensajeError
                Throw New Exception(errormessage)
            Else
                Dim res As clsResImpBrokerBBVA = serializer.Deserialize(Of clsResImpBrokerBBVA)(respuesta)
                If segvida = 0 Then
                    If GuardarPDKB64(res.urlPolicy, id_cotizacion, 2) Then
                        ImprimirBBVA = True
                    Else
                        Throw New Exception("Error: Problema al Guardar PDF en servidor")
                        ImprimirBBVA = False
                    End If
                Else
                    If GuardarPDKB64(res.urlPolicy, id_cotizacion, 3) Then
                        ImprimirBBVA = True
                    Else
                        Throw New Exception("Error: Problema al Guardar PDF en servidor")
                        ImprimirBBVA = False
                    End If
                End If
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
            ImprimirBBVA = False
        End Try
    End Function
    Public Function GuardarPDFURL(ByVal URL As String, ByVal broker As Integer) As Boolean
        Try
            GuardarPDFURL = False
            Dim NombreArch As String = String.Empty
            Dim filereader As New WebClient()
            Dim proxy As New WebProxy

            proxy = New WebProxy("150.100.210.40:8080", True)

            filereader.Proxy = proxy
            Dim savepath As String = ""


            savepath = Context.Server.MapPath("../")

            NombreArch = URL.Substring(URL.LastIndexOf("/") + 1)

            If Not (System.IO.File.Exists(savepath + "\PDF_Brokers\" + NombreArch)) Then
                filereader.DownloadFile(URL, savepath + "\PDF_Brokers\" + NombreArch)
            End If
            If GuardaringestaBBVA(savepath + "\PDF_Brokers\" + NombreArch, NombreArch, 74) Then
                GuardarPDFURL = True
                Session("RutapolDaños") = savepath + "\PDF_Brokers\" + NombreArch
                btnImpAuto.Visible = True
            Else
                Throw New Exception("Error al consumir servicio ingesta de documentos")
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
            GuardarPDFURL = False
        End Try
    End Function
    Public Function GuardarPDKB64(ByVal cadena As String, ByVal idpoliza As String, ByVal tiposeg As Integer) As Boolean
        Try
            GuardarPDKB64 = False
            Select Case tiposeg
                Case 1 'Seguro de Auto MARSH
                    Dim bytes() As Byte = Convert.FromBase64String(cadena)
                    Dim savepath As String = ""
                    Dim NombreArch As String = String.Empty

                    'savepath = Context.Server.MapPath("/ProdeskNet")
                    savepath = Context.Server.MapPath("../")
                    NombreArch = idpoliza + ".pdf"
                    If Not (System.IO.File.Exists(savepath + "\PDF_Brokers\" + NombreArch)) Then
                        Dim fs As New FileStream(savepath + "\PDF_Brokers\" + NombreArch, FileMode.CreateNew)
                        fs.Write(bytes, 0, bytes.Length)
                        fs.Close()
                    End If
                    If GuardaringestaBBVA(savepath + "\PDF_Brokers\" + NombreArch, NombreArch, 74) Then
                        GuardarPDKB64 = True
                        Session("RutapolDaños") = savepath + "\PDF_Brokers\" + NombreArch
                        btnImpAuto.Visible = True
                    Else
                        Throw New Exception("Error al consumir servicio ingesta de documentos")
                    End If
                Case 2 'Seguro de Daños BBVA
                    Dim bytes() As Byte = Convert.FromBase64String(cadena)
                    Dim savepath As String = ""
                    Dim NombreArch As String = String.Empty

                    'savepath = Context.Server.MapPath("/ProdeskNet")
                    savepath = Context.Server.MapPath("../")
                    NombreArch = idpoliza + ".pdf"
                    If Not (System.IO.File.Exists(savepath + "\PDF_Brokers\" + NombreArch)) Then
                        Dim fs As New FileStream(savepath + "\PDF_Brokers\" + NombreArch, FileMode.CreateNew)
                        fs.Write(bytes, 0, bytes.Length)
                        fs.Close()
                    End If
                    If GuardaringestaBBVA(savepath + "\PDF_Brokers\" + NombreArch, NombreArch, 74) Then
                        GuardarPDKB64 = True
                        Session("RutapolDaños") = savepath + "\PDF_Brokers\" + NombreArch
                        btnImpAuto.Visible = True
                    Else
                        Throw New Exception("Error al consumir servicio ingesta de documentos")
                    End If
                Case 3 'Seguro de Vida BBVA
                    Dim bytes() As Byte = Convert.FromBase64String(cadena)
                    Dim savepath As String = ""
                    Dim NombreArch As String = String.Empty

                    'savepath = Context.Server.MapPath("/ProdeskNet")
                    savepath = Context.Server.MapPath("../")
                    NombreArch = idpoliza + "_vida.pdf"
                    If Not (System.IO.File.Exists(savepath + "\PDF_Brokers\" + NombreArch)) Then
                        Dim fs As New FileStream(savepath + "\PDF_Brokers\" + NombreArch, FileMode.CreateNew)
                        fs.Write(bytes, 0, bytes.Length)
                        fs.Close()
                    End If
                    If GuardaringestaBBVA(savepath + "\PDF_Brokers\" + NombreArch, NombreArch, 105) Then
                        GuardarPDKB64 = True
                        Session("RutapolVida") = savepath + "\PDF_Brokers\" + NombreArch
                        btnImpVida.Visible = True
                    Else
                        Throw New Exception("Error al consumir servicio ingesta de documentos")
                    End If
            End Select
        Catch ex As Exception
            Master.MensajeError(ex.Message)
            GuardarPDKB64 = False
        End Try
    End Function
    Public Function GuardaringestaBBVA(ByVal Ruta As String, ByVal filename As String, ByVal id_doc As Integer) As Boolean
        Try

            'BUG-PD-447 INI
            Dim ds As DataSet
            Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
            Dim almacenaEnIngesta As Boolean = True
            Dim foliodocservice As String = "0"

            objCatalogos.Parametro = Me.lblSolicitud.Text
            objCatalogos.Parametro = 228
            ds = objCatalogos.Catalogos(5)

            If String.IsNullOrEmpty(objCatalogos.ErrorCatalogos) AndAlso (Not IsNothing(ds)) AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                almacenaEnIngesta = IIf(CInt(ds.Tables(0).Rows(0).Item("PDK_PAR_SIS_STATUS").ToString()) = 2, True, False)
            End If
            'BUG-PD-447 FIN

            Dim idsol As String = Context.Session("idsol")

            Dim savepath As String = ""
            Dim tempPath As String = ""
            tempPath = System.Configuration.ConfigurationManager.AppSettings("FolderPath")
            savepath = Context.Server.MapPath(tempPath)

            Dim returnvalue As String = ""
            Dim encodefile As String = ""
            Dim nombre As String = ""
            Dim arreglo As String() = filename.Split(".")
            Dim count As Integer = arreglo.Length
            For i = 1 To count Step 1
                If i <> count Then
                    nombre = nombre & arreglo(i - 1).ToString
                Else
                    nombre = nombre + "_" + idsol.ToString + "_" + id_doc.ToString + "." + arreglo(i - 1).ToString
                End If
            Next


            ds = BD.EjecutarQuery("select PDK_VERSION from PDK_REL_VER_DOC_SOL where PDK_ID_SECCERO = " & idsol & " AND PDK_ID_DOCUMENTOS =" & id_doc & ";")
            Dim version As Integer
            If (ds.Tables(0).Rows.Count = 0) Then
                version = 1
            Else
                version = CInt(ds.Tables(0).Rows(0).Item("PDK_VERSION").ToString()) + 1
            End If
            Using binaryfile As FileStream = New FileStream(Ruta, FileMode.Open)
                Dim binread As BinaryReader = New BinaryReader(binaryfile)
                Dim binbytes As Byte() = binread.ReadBytes(CInt(binaryfile.Length))
                returnvalue = Convert.ToBase64String(binbytes)
                binaryfile.Close()
            End Using

            Dim savepathzip As String = ""
            Dim tempPathzip As String = ""
            tempPathzip = System.Configuration.ConfigurationManager.AppSettings("FolderPath")
            savepathzip = Context.Server.MapPath("../")
            savepathzip = savepathzip + "\uploads\" + nombre.ToString.Replace(".pdf", "") & ".gz"
            Using originalFileStream As FileStream = New FileStream(Ruta, FileMode.Open, FileAccess.ReadWrite)
                Using compressedFileStream As FileStream = File.Create(savepathzip)
                    Using compressionStream As New GZipStream(compressedFileStream, CompressionMode.Compress)
                        originalFileStream.CopyTo(compressionStream)
                    End Using
                End Using
            End Using

            Dim files As Byte()
            Using stream = New FileStream(savepathzip, FileMode.Open, FileAccess.Read)
                Using reader = New BinaryReader(stream)
                    files = reader.ReadBytes(CInt(stream.Length))
                End Using
            End Using

            If File.Exists(savepathzip) Then
                File.Delete(savepathzip)
            End If

            'BUG-PD-447
            If almacenaEnIngesta Then
                Using binaryfile As FileStream = New FileStream(Ruta, FileMode.Open)
                    Using sha1 As New System.Security.Cryptography.SHA1CryptoServiceProvider()
                        sha1.ComputeHash(binaryfile)
                        encodefile = BitConverter.ToString(sha1.Hash).Replace("-", "").ToLower()
                    End Using
                    binaryfile.Close()
                End Using

                Dim information As System.IO.FileInfo
                information = My.Computer.FileSystem.GetFileInfo(Ruta)
                Dim extension As String = information.Extension.ToString().Replace(".", "")
                Dim ingestaarchivos As IngestaDocumentos = New IngestaDocumentos()
                ingestaarchivos.repositoryName = "finauto"
                Dim documentfilelist As documentFiles = New documentFiles()
                documentfilelist.name = nombre.ToString()
                documentfilelist.size = 1
                documentfilelist.extension = extension.ToString()
                documentfilelist.encodeData = returnvalue
                documentfilelist.extendedData.mapMetadata.f = DateTime.Now().Date.ToString("dd/MM/yyyy")
                documentfilelist.extendedData.mapMetadata.fo = CInt(idsol)
                documentfilelist.extendedData.mapMetadata.no = CInt(id_doc)
                documentfilelist.extendedData.mapMetadata.v = CInt(version)
                documentfilelist.extendedData.sha1N = encodefile
                ingestaarchivos.documentFiles.Add(documentfilelist)


                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                serializer.MaxJsonLength = Int32.MaxValue
                Dim jsonBODY As String = serializer.Serialize(ingestaarchivos)

                Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

                Dim restful As RESTful = New RESTful()
                restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriIngestadocu").ToString()
                'restful.consumerID = "10000004"
                Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonBODY)
                If (restful.IsError) Then
                    Throw New Exception(restful.MensajeError)
                End If

                Dim res As Respuesta = serializer.Deserialize(Of Respuesta)(respuesta)
                foliodocservice = res.documentFiles(0).extendedData.mapMetadata._s3_key

            End If

            Dim dsrelpandoc As DataSet
            Dim relpandoc As String = String.Empty
            dsrelpandoc = BD.EjecutarQuery("select PDK_ID_REL_PAN_DOC from PDK_REL_PAN_DOC where PDK_ID_DOCUMENTOS=" + id_doc.ToString + "and PDK_ID_PANTALLAS=" + Request("pantalla").ToString + ";")
            If (dsrelpandoc.Tables.Count > 0 And dsrelpandoc.Tables(0).Rows.Count >= 0) Then
                relpandoc = dsrelpandoc.Tables(0).Rows(0).Item("PDK_ID_REL_PAN_DOC").ToString()
            End If
            'If System.IO.File.Exists(Ruta) = True Then
            '    System.IO.File.Delete(Ruta)
            'End If
            Dim clsseg As New clsSeguros
            clsseg._ID_SOLICITUD = idsol
            clsseg._ID_DOC = id_doc
            clsseg._ID_RELPANDOC = relpandoc
            clsseg._NOMDOC = foliodocservice
            clsseg._VER = version
            clsseg._NOMBRE_ARCHIVO = filename
            clsseg._DATOS_ARCHIVO = files
            If clsseg.InsertaDoc() Then
                GuardaringestaBBVA = True
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CheckDoc", "fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');", True)
            Else
                Throw New Exception(clsseg.StrError)
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
            GuardaringestaBBVA = False
        End Try
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function jsonGuardaDatosSeguro(ByVal id_sol As String, ByVal id_usu As String, ByVal nomsegdanios As String, ByVal numsegdanios As String, ByVal vigsegdanios As String, ByVal nomsegvida As String, ByVal numsegvida As String, ByVal vigsegvida As String, ByVal quotevida As String) As String
        Try
            If (nomsegdanios <> "" And numsegdanios <> "" And nomsegvida <> "" And numsegvida <> "" And vigsegdanios <> "" And vigsegvida <> "") Then
                Dim seguros As New clsSeguros
                seguros._ID_SOLICITUD = CInt(id_sol)
                seguros._NOMSEGDANIOS = nomsegdanios
                seguros._NUMSEGDANIOS = numsegdanios
                seguros._NOMSEGVIDA = nomsegvida
                seguros._NUMSEGVIDA = numsegvida
                If quotevida <> "" Then
                    seguros._PDK_ID_QUOTELIFE = quotevida
                End If
                vigsegvida = FormatoDate(vigsegvida)
                Dim datevigsegvida As Date = vigsegvida
                seguros._VIGSEGVIDA = datevigsegvida.ToString("yyyy/MM/dd")
                Dim datefincredito As Date
                Dim isMulti As Integer = 0
                Dim clsdatsol As clsSeguros = New clsSeguros
                Dim strres As New DataSet
                clsdatsol._ID_SOLICITUD = id_sol
                strres = clsdatsol.GetFinCredito()
                If clsdatsol.StrError = "" Then
                    datefincredito = strres.Tables(0).Rows(0).Item("FEC_PAGO").ToString()
                    isMulti = CInt(strres.Tables(0).Rows(0).Item("ISMULTIANUAL").ToString())
                Else
                    Throw New Exception(clsdatsol.StrError)
                End If
                vigsegdanios = FormatoDate(vigsegdanios)
                Dim fechadanios As Date = vigsegdanios

                'BUG-PD-448
                Dim dsDatosSeguro As DataSet = seguros.getDatosSeguro()
                Dim idBroker As Short = 0

                If (Not IsNothing(dsDatosSeguro)) AndAlso dsDatosSeguro.Tables.Count > 0 AndAlso dsDatosSeguro.Tables(0).Rows.Count > 0 Then
                    idBroker = CShort(dsDatosSeguro.Tables(0).Rows(0).Item("ID_BROKER").ToString())
                End If

                If idBroker <> Marsh Then
                    If isMulti = 1 AndAlso (DateDiff("d", fechadanios, datefincredito) > 2) Then
                        Throw New Exception("Fecha vigencia de seguro de daños vence antes de la fecha fin del credito")
                    End If
                End If

                seguros._VIGSEGDANIOS = fechadanios.ToString("yyyy/MM/dd")
                'BUG-PD-448

                If seguros.InsertDatosSeguroSolicitud() Then
                    Return "OK"
                Else
                    Throw New Exception(seguros.StrError.ToString())
                End If
            Else
                Throw New Exception("Todos los campos marcados con * son obligatorios")
            End If
        Catch ex As Exception
            Return "ERROR: " + ex.Message.ToString()
        End Try
    End Function
    Public Shared Function FormatoDate(ByVal fecha As String) As String
        If fecha <> "" Then
            Dim fechastring As String() = fecha.Split("/")
            FormatoDate = fechastring(2).ToString + "-" + fechastring(1).ToString + "-" + fechastring(0).ToString
        Else
            Return ""
        End If
    End Function
    Public Sub ProcesoSeguros(ByVal opcion As Integer)
        Dim clsseg As clsSeguros = New clsSeguros
        clsseg._ID_SOLICITUD = CInt(Request("solicitud").ToString())
        Dim DatosSeguro As DataSet = clsseg.getDatosSeguro()
        If (DatosSeguro.Tables.Count > 0) Then
            If (DatosSeguro.Tables(0).Rows.Count > 0) Then
                If (DatosSeguro.Tables(0).Rows(0).Item("TIPO_SEG_AUTO").ToString() <> "32") Then
                    txtNomSegDanios.Text = DatosSeguro.Tables(0).Rows(0).Item("NOM_ASEGURADORA").ToString()
                    txtNomSegDanios.Enabled = False

                    Dim idbroker As Integer = CInt(DatosSeguro.Tables(0).Rows(0).Item("ID_BROKER").ToString())
                    If idbroker = 2 Or idbroker = 3 Or idbroker = 4 Or idbroker = 5 Or idbroker = 9 Then
                        txtNumSegDanios.Enabled = False
                    End If
                    Dim tienevida As Integer = IIf(DatosSeguro.Tables(0).Rows(0).Item("TIPO_SEG_VIDA").ToString() <> "172", 1, 0)
                    Select Case opcion
                        Case 0
                            If EmitirSeguros(CInt(DatosSeguro.Tables(0).Rows(0).Item("ID_BROKER").ToString()), DatosSeguro.Tables(0).Rows(0).Item("NO_COTSEG").ToString, DatosSeguro.Tables(0).Rows(0).Item("ID_AGENCIA").ToString) Then
                            Else
                                btnEmisionDanRetry.Visible = True
                                btnImpresionDanRetry.Visible = False
                                btnEmisionVidRetry.Visible = False
                                btnImpresionVidRetry.Visible = False
                                Exit Sub
                            End If
                            If ImprimirSeguros(CInt(DatosSeguro.Tables(0).Rows(0).Item("ID_BROKER").ToString()), DatosSeguro.Tables(0).Rows(0).Item("NO_COTSEG").ToString, DatosSeguro.Tables(0).Rows(0).Item("ID_AGENCIA").ToString) Then
                            Else
                                btnEmisionDanRetry.Visible = False
                                btnImpresionDanRetry.Visible = True
                                btnEmisionVidRetry.Visible = False
                                btnImpresionVidRetry.Visible = False
                                Exit Sub
                            End If
                        Case 1
                            If ImprimirSeguros(CInt(DatosSeguro.Tables(0).Rows(0).Item("ID_BROKER").ToString()), DatosSeguro.Tables(0).Rows(0).Item("NO_COTSEG").ToString, DatosSeguro.Tables(0).Rows(0).Item("ID_AGENCIA").ToString) Then
                            Else
                                btnEmisionDanRetry.Visible = False
                                btnImpresionDanRetry.Visible = True
                                btnEmisionVidRetry.Visible = False
                                btnImpresionVidRetry.Visible = False
                                Exit Sub
                            End If
                    End Select

                End If
                If (DatosSeguro.Tables(0).Rows(0).Item("TIPO_SEG_VIDA").ToString() <> "172") Then
                    txtNomSegVida.Text = DatosSeguro.Tables(0).Rows(0).Item("NOM_ASEGURADORA_VIDA").ToString()
                    Dim idquotelife = DatosSeguro.Tables(0).Rows(0).Item("NO_COTSEG_VIDA").ToString
                    If IsNothing(idquotelife) Or idquotelife = "" Then
                        Master.MensajeError("Cotización no valida para seguro de vida, por favor ligue una nueva cotización.")
                        Exit Sub
                    End If
                    txtNomSegVida.Enabled = False
                    txtNumSegVida.Enabled = False
                    Select Case opcion
                        Case 0
                            If EmitirSeguros(-1, idquotelife.ToString, "") Then
                            Else
                                btnEmisionDanRetry.Visible = False
                                btnImpresionDanRetry.Visible = False
                                btnEmisionVidRetry.Visible = True
                                btnImpresionVidRetry.Visible = False
                                Exit Sub
                            End If
                            If ImprimirSeguros(-1, idquotelife.ToString, "") Then
                            Else
                                btnEmisionDanRetry.Visible = False
                                btnImpresionDanRetry.Visible = False
                                btnEmisionVidRetry.Visible = False
                                btnImpresionVidRetry.Visible = True
                                Exit Sub
                            End If
                        Case 1
                            If EmitirSeguros(-1, idquotelife.ToString, "") Then
                            Else
                                btnEmisionDanRetry.Visible = False
                                btnImpresionDanRetry.Visible = False
                                btnEmisionVidRetry.Visible = True
                                btnImpresionVidRetry.Visible = False
                                Exit Sub
                            End If
                            If ImprimirSeguros(-1, idquotelife.ToString, "") Then
                            Else
                                btnEmisionDanRetry.Visible = False
                                btnImpresionDanRetry.Visible = False
                                btnEmisionVidRetry.Visible = False
                                btnImpresionVidRetry.Visible = True
                                Exit Sub
                            End If
                        Case 2
                            If EmitirSeguros(-1, idquotelife.ToString, "") Then
                            Else
                                btnEmisionDanRetry.Visible = False
                                btnImpresionDanRetry.Visible = False
                                btnEmisionVidRetry.Visible = True
                                btnImpresionVidRetry.Visible = False
                                Exit Sub
                            End If
                            If ImprimirSeguros(-1, idquotelife.ToString, "") Then
                            Else
                                btnEmisionDanRetry.Visible = False
                                btnImpresionDanRetry.Visible = False
                                btnEmisionVidRetry.Visible = False
                                btnImpresionVidRetry.Visible = True
                                Exit Sub
                            End If
                        Case 3
                            If ImprimirSeguros(-1, idquotelife.ToString, "") Then
                            Else
                                btnEmisionDanRetry.Visible = False
                                btnImpresionDanRetry.Visible = False
                                btnEmisionVidRetry.Visible = False
                                btnImpresionVidRetry.Visible = True
                                Exit Sub
                            End If
                    End Select
                End If
            Else
                Master.MensajeError("No existen datos para la emision e impresion de seguros")
            End If
        Else
            Master.MensajeError("No existen datos para la emision e impresion de seguros")
            Exit Sub
        End If
    End Sub
    Public Function EmitirSeguros(ByVal Id_Broker As Integer, Optional ByVal Idquote As String = "", Optional ByVal id_agencia As String = "") As Boolean
        EmitirSeguros = False
        Select Case (Id_Broker)
            Case -1 'BBVA VIDA
                If EmitirVidaBBVA(Idquote) Then
                    EmitirSeguros = True
                Else
                    Exit Function
                End If
            Case 2 'ORDAS
                If EmitirORDAS(Idquote) Then
                    EmitirSeguros = True
                Else
                    Exit Function
                End If
            Case 3 'EIKOS
                If EmitirEIKOS(Idquote) Then
                    EmitirSeguros = True
                Else
                    Exit Function
                End If
            Case 4 'MARSH
                If EmitirMARSH(Idquote) Then
                    EmitirSeguros = True
                Else
                    Exit Function
                End If
            Case 5, 9

                If EmitirBBVA(Idquote, 0) Then
                    EmitirSeguros = True
                Else
                    Exit Function
                End If

            Case Else
                EmitirSeguros = True
        End Select
    End Function
    Public Function EmitirORDAS(ByVal policyid As String) As Boolean
        Try
            EmitirORDAS = False
            Dim clsdatos As New clsSeguros
            clsdatos._ID_SOLICITUD = hdSolicitud.Value
            Dim dsres As New DataSet
            dsres = clsdatos.getDatosBroker(2)
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Dim clsORDAS As New clsBrokerEmiORDAS
                    clsORDAS.policy.complement.user.id = System.Configuration.ConfigurationManager.AppSettings("useridOrdas").ToString()
                    clsORDAS.policy.complement.user.credentials.accessPassword = System.Configuration.ConfigurationManager.AppSettings("passwordOrdas").ToString()
                    clsORDAS.policy.iPolicy.vehiclePolicy.idVehicle = dsres.Tables(0).Rows(0).Item("idVehicle").ToString()
                    clsORDAS.policy.iPolicy.vehiclePolicy.idUse = dsres.Tables(0).Rows(0).Item("idUse").ToString
                    clsORDAS.policy.iPolicy.vehiclePolicy.accessoryDescription = "CON BASE A FACTURA"
                    clsORDAS.policy.iPolicy.vehiclePolicy.invoiceNumber = dsres.Tables(0).Rows(0).Item("invoiceNumber").ToString()
                    clsORDAS.policy.iPolicy.vehiclePolicy.credit.idFinancingTerm = dsres.Tables(0).Rows(0).Item("idFinancingTerm").ToString()
                    clsORDAS.policy.iPolicy.vehiclePolicy.credit.validityPeriod.startDate = DateTime.Now.ToString("yyyy-MM-dd")
                    clsORDAS.policy.iPolicy.vehiclePolicy.credit.validityPeriod.endDate = DateTime.Now.AddMonths(CInt(dsres.Tables(0).Rows(0).Item("idFinancingTerm"))).ToString("yyyy-MM-dd").ToString()
                    clsORDAS.policy.iPolicy.vehiclePolicy.car.serialNumber = dsres.Tables(0).Rows(0).Item("serialNumber").ToString()
                    clsORDAS.policy.iPolicy.vehiclePolicy.car.engineNumber = dsres.Tables(0).Rows(0).Item("engineNumber").ToString()
                    clsORDAS.policy.iPolicy.vehiclePolicy.car.repuve = ""
                    clsORDAS.policy.iPolicy.vehiclePolicy.car.plates = ""
                    clsORDAS.policy.iPolicy.vehiclePolicy.accessorySum.amount = IIf(dsres.Tables(0).Rows(0).Item("accessorySum_amount").ToString = "", "0", dsres.Tables(0).Rows(0).Item("accessorySum_amount").ToString)
                    Dim nombreconductorrecurrente As String() = dsres.Tables(0).Rows(0).Item("AllnameCR").ToString.Split(" ")
                    Dim totnombre As Integer = nombreconductorrecurrente.Count
                    Dim nomcte As String = ""
                    Dim apellpcte As String = ""
                    Dim apellmcte As String = ""
                    If totnombre - 3 > 0 Then
                        For i As Integer = 0 To totnombre - 3
                            If nombreconductorrecurrente(i).ToString <> "" Then
                                If nomcte.ToString <> "" Then
                                    nomcte = nomcte + " "
                                End If
                                nomcte = nomcte + nombreconductorrecurrente(i)
                            End If
                        Next
                        apellpcte = nombreconductorrecurrente(totnombre - 2)
                        apellmcte = nombreconductorrecurrente(totnombre - 1)
                    ElseIf totnombre - 3 = 0 Then
                        nomcte = nombreconductorrecurrente(0)
                        apellpcte = nombreconductorrecurrente(1)
                        apellmcte = nombreconductorrecurrente(2)
                    ElseIf totnombre = 1 Then
                        nomcte = nombreconductorrecurrente(0)
                        apellpcte = nombreconductorrecurrente(1)
                    Else
                        nomcte = nombreconductorrecurrente(0)
                    End If
                    clsORDAS.policy.iPolicy.vehiclePolicy.driver.legalAddress.zipCode = dsres.Tables(0).Rows(0).Item("CPCR").ToString()
                    clsORDAS.policy.iPolicy.vehiclePolicy.driver.name = nomcte
                    clsORDAS.policy.iPolicy.vehiclePolicy.driver.lastName = apellpcte
                    clsORDAS.policy.iPolicy.vehiclePolicy.driver.mothersLastName = apellmcte
                    clsORDAS.policy.iPolicy.vehiclePolicy.driver.extendedData.age = dsres.Tables(0).Rows(0).Item("ageCR").ToString()
                    clsORDAS.policy.iPolicy.vehiclePolicy.driver.extendedData.rfc = dsres.Tables(0).Rows(0).Item("rfcCR").ToString()
                    clsORDAS.policy.iPolicy.vehiclePolicy.driver.extendedData.gender = dsres.Tables(0).Rows(0).Item("genderCR").ToString()
                    clsORDAS.policy.idPack = dsres.Tables(0).Rows(0).Item("idPack").ToString()
                    clsORDAS.policy.idProduct = dsres.Tables(0).Rows(0).Item("idProduct").ToString()
                    clsORDAS.policy.idTerm = dsres.Tables(0).Rows(0).Item("idTerm").ToString()
                    clsORDAS.policy.agencyNumber = dsres.Tables(0).Rows(0).Item("agencyid").ToString()
                    clsORDAS.policy.insurerId = dsres.Tables(0).Rows(0).Item("insurerId").ToString()
                    clsORDAS.policy.insuredAmount.amount = dsres.Tables(0).Rows(0).Item("amount").ToString()
                    clsORDAS.policy.idAdditionalPaymentWay = "-1"
                    clsORDAS.policy.idAdditionalTerm = "0"
                    clsORDAS.policy.idAdditionalPack = "0"
                    clsORDAS.policy.numberFinancialContract = dsres.Tables(0).Rows(0).Item("numberFinancialContract").ToString()
                    clsORDAS.policy.idPaymentWay = "9"
                    clsORDAS.policy.preferredBeneficiary = "3"
                    clsORDAS.policy.contractor.name = dsres.Tables(0).Rows(0).Item("name").ToString()
                    clsORDAS.policy.contractor.lastName = dsres.Tables(0).Rows(0).Item("lastName").ToString()
                    clsORDAS.policy.contractor.mothersLastName = dsres.Tables(0).Rows(0).Item("mothersLastName").ToString()
                    clsORDAS.policy.contractor.birthDate = dsres.Tables(0).Rows(0).Item("birthDate").ToString()
                    clsORDAS.policy.contractor.extendedData.rfc = dsres.Tables(0).Rows(0).Item("rfc").ToString()
                    clsORDAS.policy.contractor.extendedData.email = dsres.Tables(0).Rows(0).Item("email").ToString()
                    clsORDAS.policy.contractor.extendedData.gender = dsres.Tables(0).Rows(0).Item("gender").ToString()
                    clsORDAS.policy.contractor.extendedData.maritalStatus = dsres.Tables(0).Rows(0).Item("maritalStatus").ToString()
                    clsORDAS.policy.contractor.extendedData.fiscalSituation.relationName = dsres.Tables(0).Rows(0).Item("relationName").ToString()
                    clsORDAS.policy.contractor.extendedData.bussinessData.name = ""
                    clsORDAS.policy.contractor.extendedData.bussinessData.constitutionDate = ""
                    clsORDAS.policy.contractor.extendedData.homePhone.telephoneNumber = dsres.Tables(0).Rows(0).Item("homePhone").ToString()
                    clsORDAS.policy.contractor.extendedData.mobilePhone.telephoneNumber = dsres.Tables(0).Rows(0).Item("mobilePhone").ToString()
                    clsORDAS.policy.contractor.legalAddress.zipCode = dsres.Tables(0).Rows(0).Item("zipCode").ToString()
                    clsORDAS.policy.contractor.legalAddress.neighborhood = dsres.Tables(0).Rows(0).Item("neighborhood").ToString()
                    clsORDAS.policy.contractor.legalAddress.additionalAddress = dsres.Tables(0).Rows(0).Item("additionalAddress").ToString()
                    clsORDAS.policy.contractor.legalAddress.externalNumber = dsres.Tables(0).Rows(0).Item("externalNumber").ToString()
                    clsORDAS.policy.contractor.legalAddress.door = dsres.Tables(0).Rows(0).Item("door").ToString
                    clsORDAS.policy.contractor.legalAddress.county = dsres.Tables(0).Rows(0).Item("countyname").ToString
                    clsORDAS.policy.contractor.legalAddress.state = dsres.Tables(0).Rows(0).Item("state").ToString()
                    clsORDAS.policy.contractor.legalAddress.stateId = dsres.Tables(0).Rows(0).Item("stateId").ToString()
                    clsORDAS.policy.validityPeriod.startDate = DateTime.Now.ToString("yyyy-MM-dd").ToString()
                    clsORDAS.policy.validityPeriod.endDate = DateTime.Now.AddMonths(CInt(dsres.Tables(0).Rows(0).Item("idFinancingTerm"))).ToString("yyyy-MM-dd").ToString()
                    clsORDAS.policy.currency.id = "1"

                    Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                    serializer.MaxJsonLength = Int32.MaxValue
                    Dim jsonbody As String = serializer.Serialize(clsORDAS)
                    Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                    Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
                    Dim restful As RESTful = New RESTful()
                    restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriEmiORDAS").ToString()

                    Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
                    Dim errormessage As String
                    If (restful.IsError) Then
                        If restful.StatusHTTP <> "200" Then
                            errormessage = restful.MensajeError
                            Throw New Exception(errormessage)
                        Else
                            Dim res As clsResEmiBrokerORDAS = serializer.Deserialize(Of clsResEmiBrokerORDAS)(respuesta)
                            If res.policy.complement.errorInfo.description.Contains("GO::") Then
                                Throw New Exception(res.policy.complement.errorInfo.description.ToString)
                            End If
                            Dim idpoliza As String = ""


                            Dim fechafin As Date = dsres.Tables(0).Rows(0).Item("endDate")
                            txtVigSegDanios.Text = fechafin.ToString("dd/MM/yyyy")
                            For i As Integer = 0 To res.policy.receipts.Count - 1
                                If res.policy.receipts(i).idPolicy.ToString <> "" Then
                                    idpoliza = res.policy.receipts(i).idPolicy.ToString
                                    Exit For
                                End If
                            Next
                            txtNumSegDanios.Text = idpoliza
                        End If


                    Else
                        Dim res As clsResEmiBrokerORDAS = serializer.Deserialize(Of clsResEmiBrokerORDAS)(respuesta)
                        If res.policy.complement.errorInfo.description.Contains("GO::") Then
                            Throw New Exception(res.policy.complement.errorInfo.description.ToString)
                        End If
                        Dim idpoliza As String = ""


                        Dim fechafin As Date = dsres.Tables(0).Rows(0).Item("endDate")
                        txtVigSegDanios.Text = fechafin.ToString("dd/MM/yyyy")
                        For i As Integer = 0 To res.policy.receipts.Count - 1
                            If res.policy.receipts(i).idPolicy.ToString <> "" Then
                                idpoliza = res.policy.receipts(i).idPolicy.ToString
                                Exit For
                            End If
                        Next
                        txtNumSegDanios.Text = idpoliza
                    End If

                End If
            End If
            EmitirORDAS = True
        Catch ex As Exception
            EmitirORDAS = False
            Master.MensajeError("Error WS: " & ex.Message.ToString.Replace(vbLf, ", "))
        End Try
    End Function
    Public Function EmitirEIKOS(ByVal policyid As String) As Boolean
        Try
            EmitirEIKOS = False
            Dim clsdatos As New clsSeguros
            clsdatos._ID_SOLICITUD = hdSolicitud.Value
            Dim dsres As New DataSet
            dsres = clsdatos.getDatosBroker(3)
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Dim clsseg As clsSeguros = New clsSeguros
                    Dim tiposeg As String = ""
                    clsseg._ID_SOLICITUD = CInt(Request("solicitud").ToString())
                    Dim DatosSeguro As DataSet = clsseg.getDatosSeguro()
                    If (DatosSeguro.Tables.Count > 0) Then
                        If (DatosSeguro.Tables(0).Rows.Count > 0) Then
                            tiposeg = DatosSeguro.Tables(0).Rows(0).Item("TIPO_SEG_AUTO").ToString()
                        End If
                    End If
                    Dim plazo As Integer = IIf(tiposeg = "30" Or tiposeg = "31", "12", dsres.Tables(0).Rows(0).Item("ValorPlazo").ToString)

                    Dim clsemiEIKOS As New clsBrokerEIKOS
                    clsemiEIKOS.policy.iPolicy.vehiclePolicy.idQuote = policyid.ToString

                    clsemiEIKOS.policy.iPolicy.vehiclePolicy.car.key = dsres.Tables(0).Rows(0).Item("key").ToString
                    clsemiEIKOS.policy.iPolicy.vehiclePolicy.car.model = dsres.Tables(0).Rows(0).Item("model").ToString
                    clsemiEIKOS.policy.iPolicy.vehiclePolicy.car.serialNumber = dsres.Tables(0).Rows(0).Item("serialNumber").ToString
                    clsemiEIKOS.policy.iPolicy.vehiclePolicy.car.engineNumber = dsres.Tables(0).Rows(0).Item("engineNumber").ToString
                    clsemiEIKOS.policy.iPolicy.vehiclePolicy.car.plates = ""
                    clsemiEIKOS.policy.iPolicy.vehiclePolicy.car.repuve = ""
                    clsemiEIKOS.policy.iPolicy.vehiclePolicy.car.location = dsres.Tables(0).Rows(0).Item("location").ToString

                    clsemiEIKOS.policy.iComplement.EmissionComplementEikos.employeeId = Request("usu").ToString
                    clsemiEIKOS.policy.iComplement.EmissionComplementEikos.creditId = dsres.Tables(0).Rows(0).Item("creditId").ToString
                    clsemiEIKOS.policy.iComplement.EmissionComplementEikos.pol_exp = "0"

                    clsemiEIKOS.policy.complement.user.id = System.Configuration.ConfigurationManager.AppSettings("useridEikos").ToString()
                    clsemiEIKOS.policy.complement.user.credentials.accessPassword = System.Configuration.ConfigurationManager.AppSettings("accessPasswordEikos").ToString()

                    clsemiEIKOS.policy.agencyNumber = dsres.Tables(0).Rows(0).Item("agencyid").ToString
                    clsemiEIKOS.policy.fee.amount = dsres.Tables(0).Rows(0).Item("amount").ToString
                    clsemiEIKOS.policy.contractor.name = dsres.Tables(0).Rows(0).Item("name").ToString
                    clsemiEIKOS.policy.contractor.middleName = dsres.Tables(0).Rows(0).Item("middleName").ToString
                    clsemiEIKOS.policy.contractor.lastName = dsres.Tables(0).Rows(0).Item("lastName").ToString
                    clsemiEIKOS.policy.contractor.mothersLastName = dsres.Tables(0).Rows(0).Item("motherLastName").ToString
                    clsemiEIKOS.policy.contractor.birthDate = dsres.Tables(0).Rows(0).Item("birthDate").ToString
                    clsemiEIKOS.policy.contractor.legalAddress.streetName = dsres.Tables(0).Rows(0).Item("streetName").ToString
                    clsemiEIKOS.policy.contractor.legalAddress.externalNumber = dsres.Tables(0).Rows(0).Item("externalNumber").ToString
                    clsemiEIKOS.policy.contractor.legalAddress.door = dsres.Tables(0).Rows(0).Item("door").ToString
                    clsemiEIKOS.policy.contractor.legalAddress.neighborhood = dsres.Tables(0).Rows(0).Item("neighborhood").ToString
                    clsemiEIKOS.policy.contractor.legalAddress.zipCode = dsres.Tables(0).Rows(0).Item("zipCode").ToString
                    clsemiEIKOS.policy.contractor.legalAddress.stateId = dsres.Tables(0).Rows(0).Item("stateId").ToString
                    clsemiEIKOS.policy.contractor.legalAddress.county = dsres.Tables(0).Rows(0).Item("countyname").ToString
                    clsemiEIKOS.policy.contractor.legalAddress.country.name = "1"
                    clsemiEIKOS.policy.contractor.extendedData.rfc = dsres.Tables(0).Rows(0).Item("rfc").ToString
                    clsemiEIKOS.policy.contractor.extendedData.fiscalSituation.relationName = dsres.Tables(0).Rows(0).Item("relationName").ToString
                    clsemiEIKOS.policy.contractor.extendedData.gender = dsres.Tables(0).Rows(0).Item("gender").ToString
                    clsemiEIKOS.policy.contractor.extendedData.maritalStatus = dsres.Tables(0).Rows(0).Item("maritalStatus").ToString
                    clsemiEIKOS.policy.contractor.extendedData.curp = dsres.Tables(0).Rows(0).Item("curp").ToString
                    Dim ocupacion As String = String.Empty
                    If (dsres.Tables(0).Rows(0).Item("occupation").ToString.Length <= 29) Then
                        ocupacion = dsres.Tables(0).Rows(0).Item("occupation").ToString()
                    Else
                        ocupacion = dsres.Tables(0).Rows(0).Item("occupation").ToString.Substring(0, 29)
                    End If
                    clsemiEIKOS.policy.contractor.extendedData.occupation = ocupacion
                    clsemiEIKOS.policy.contractor.extendedData.email = dsres.Tables(0).Rows(0).Item("email").ToString
                    clsemiEIKOS.policy.contractor.extendedData.countryOrigin.name = dsres.Tables(0).Rows(0).Item("countryOriginName").ToString
                    clsemiEIKOS.policy.contractor.extendedData.homePhone.countryCode = "52"
                    clsemiEIKOS.policy.contractor.extendedData.homePhone.areaCode = dsres.Tables(0).Rows(0).Item("homePhoneAreaCode").ToString
                    clsemiEIKOS.policy.contractor.extendedData.homePhone.telephoneNumber = dsres.Tables(0).Rows(0).Item("homePhoneTelephoneNumber").ToString
                    clsemiEIKOS.policy.contractor.extendedData.homePhone.phoneExtension = ""
                    clsemiEIKOS.policy.contractor.extendedData.mobilePhone.countryCode = "52"
                    clsemiEIKOS.policy.contractor.extendedData.mobilePhone.areaCode = dsres.Tables(0).Rows(0).Item("mobilePhoneAreaCode").ToString
                    clsemiEIKOS.policy.contractor.extendedData.mobilePhone.telephoneNumber = dsres.Tables(0).Rows(0).Item("mobilePhoneTelephoneNumber").ToString
                    clsemiEIKOS.policy.contractor.extendedData.mobilePhone.phoneExtension = ""
                    clsemiEIKOS.policy.validityPeriod.startDate = dsres.Tables(0).Rows(0).Item("startDate").ToString
                    Dim startdate As Date = dsres.Tables(0).Rows(0).Item("startDate").ToString
                    clsemiEIKOS.policy.validityPeriod.endDate = startdate.AddMonths(plazo).ToString("yyyy-MM-dd")


                    Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                    serializer.MaxJsonLength = Int32.MaxValue
                    Dim jsonbody As String = serializer.Serialize(clsemiEIKOS)
                    Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                    Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
                    Dim restful As RESTful = New RESTful()
                    restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriEmiEIKOS").ToString()

                    Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
                    Dim errormessage As String
                    If (restful.IsError) Then
                        errormessage = restful.MensajeError
                        Throw New Exception(errormessage)
                    Else
                        Dim res As clsResEmiBrokerEIKOS = serializer.Deserialize(Of clsResEmiBrokerEIKOS)(respuesta)
                        If Not IsNothing(res.policy.complement.errorInfo.errorId) Then
                            Throw New Exception(res.policy.complement.errorInfo.description.ToString.Replace(vbLf, ", "))
                        End If
                        txtNumSegDanios.Text = res.policy.policyId.ToString
                        Dim fechafin As Date = res.policy.validityPeriod.endDate.ToString
                        txtVigSegDanios.Text = fechafin.ToString("dd/MM/yyyy")
                    End If
                End If
            End If
            EmitirEIKOS = True
        Catch ex As Exception
            EmitirEIKOS = False
            Master.MensajeError("Error WS: " & ex.Message.ToString.Replace(vbLf, ", "))
        End Try
    End Function
    Public Function EmitirMARSH(ByVal policyid As String) As Boolean
        Try
            EmitirMARSH = False = False
            Dim clsdatos As New clsSeguros
            clsdatos._ID_SOLICITUD = hdSolicitud.Value
            Dim dsres As New DataSet
            dsres = clsdatos.getDatosBroker(4)
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Dim clsemiMARSH As clsBrokerMARSH = New clsBrokerMARSH

                    clsemiMARSH.policy.iPolicy.vehiclePolicy.car.serialNumber = dsres.Tables(0).Rows(0).Item("serialNumber").ToString
                    clsemiMARSH.policy.iPolicy.vehiclePolicy.car.engineNumber = dsres.Tables(0).Rows(0).Item("engineNumber").ToString
                    clsemiMARSH.policy.iPolicy.vehiclePolicy.car.cylinders = dsres.Tables(0).Rows(0).Item("cylinders").ToString
                    clsemiMARSH.policy.iPolicy.vehiclePolicy.car.chassis = dsres.Tables(0).Rows(0).Item("chassis").ToString
                    clsemiMARSH.policy.iPolicy.vehiclePolicy.car.capacity = "0"
                    clsemiMARSH.policy.iPolicy.vehiclePolicy.idQuote = policyid.ToString
                    clsemiMARSH.policy.iPolicy.vehiclePolicy.credit.idFinancingTerm = dsres.Tables(0).Rows(0).Item("idFinancingTerm").ToString
                    clsemiMARSH.policy.iPolicy.vehiclePolicy.credit.validityPeriod.startDate = dsres.Tables(0).Rows(0).Item("startDate").ToString
                    Dim FechafinMarsh As Date = dsres.Tables(0).Rows(0).Item("startDate").ToString()
                    Dim val_plazo As Integer = IIf(dsres.Tables(0).Rows(0).Item("idFinancingTerm").ToString() = "", 0, CInt(dsres.Tables(0).Rows(0).Item("idFinancingTerm").ToString()))
                    FechafinMarsh = DateAdd(DateInterval.Month, val_plazo, FechafinMarsh)
                    'FechafinMarsh = DateAdd(DateInterval.Day, 16, FechafinMarsh)
                    clsemiMARSH.policy.iPolicy.vehiclePolicy.credit.validityPeriod.endDate = FechafinMarsh.Date.ToString("yyyy-MM-dd")

                    clsemiMARSH.policy.iPolicy.vehiclePolicy.invoiceNumber = dsres.Tables(0).Rows(0).Item("invoiceNumber").ToString

                    clsemiMARSH.policy.iComplement.EmissionComplementMarsh.contractNumber = dsres.Tables(0).Rows(0).Item("contractNumber").ToString

                    clsemiMARSH.policy.complement.user.id = System.Configuration.ConfigurationManager.AppSettings("useridMarsh").ToString()
                    clsemiMARSH.policy.complement.user.credentials.accessPassword = System.Configuration.ConfigurationManager.AppSettings("passwordMarsh").ToString()
                    clsemiMARSH.policy.contractor.name = dsres.Tables(0).Rows(0).Item("name").ToString
                    clsemiMARSH.policy.contractor.lastName = dsres.Tables(0).Rows(0).Item("lastName").ToString
                    clsemiMARSH.policy.contractor.mothersLastName = dsres.Tables(0).Rows(0).Item("mothersLastName").ToString
                    clsemiMARSH.policy.contractor.extendedData.homePhone.telephoneNumber = dsres.Tables(0).Rows(0).Item("telephoneNumber").ToString
                    clsemiMARSH.policy.contractor.extendedData.rfc = dsres.Tables(0).Rows(0).Item("rfc").ToString
                    clsemiMARSH.policy.contractor.extendedData.email = dsres.Tables(0).Rows(0).Item("email").ToString
                    clsemiMARSH.policy.contractor.extendedData.fiscalSituation.relationName = dsres.Tables(0).Rows(0).Item("relationName").ToString
                    clsemiMARSH.policy.contractor.extendedData.gender = dsres.Tables(0).Rows(0).Item("gender").ToString
                    clsemiMARSH.policy.contractor.extendedData.maritalStatus = dsres.Tables(0).Rows(0).Item("maritalStatus").ToString
                    clsemiMARSH.policy.contractor.legalAddress.zipCode = dsres.Tables(0).Rows(0).Item("zipCode").ToString
                    clsemiMARSH.policy.contractor.legalAddress.streetName = dsres.Tables(0).Rows(0).Item("streetName").ToString
                    clsemiMARSH.policy.contractor.legalAddress.externalNumber = dsres.Tables(0).Rows(0).Item("externalNumber").ToString
                    clsemiMARSH.policy.contractor.legalAddress.city = dsres.Tables(0).Rows(0).Item("city").ToString
                    clsemiMARSH.policy.contractor.legalAddress.state = dsres.Tables(0).Rows(0).Item("state").ToString
                    clsemiMARSH.policy.contractor.legalAddress.neighborhood = dsres.Tables(0).Rows(0).Item("neighborhood").ToString
                    clsemiMARSH.policy.contractor.birthDate = dsres.Tables(0).Rows(0).Item("birthDate").ToString
                    clsemiMARSH.policy.preferredBeneficiary = "BBVA BANCOMER S.A."
                    Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                    serializer.MaxJsonLength = Int32.MaxValue
                    Dim jsonbody As String = serializer.Serialize(clsemiMARSH)
                    Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                    Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
                    Dim restful As RESTful = New RESTful()
                    restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriEmiMARSH").ToString()

                    Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
                    Dim errormessage As String
                    If (restful.IsError) Then
                        errormessage = restful.MensajeError
                        Throw New Exception(errormessage)
                    Else
                        Dim res As clsResEmiBrokerMARSH = serializer.Deserialize(Of clsResEmiBrokerMARSH)(respuesta)
                        If res.policy.iComplement.EmissionComplementMarsh.error.errorId.ToString <> "0" Then 'AndAlso res.policy.iComplement.EmissionComplementMarsh.error.errorId.ToString <> "03062"
                            Throw New Exception(res.policy.iComplement.EmissionComplementMarsh.error.errorId.ToString & ": " & res.policy.iComplement.EmissionComplementMarsh.error.description)
                        End If
                        txtNumSegDanios.Text = res.policy.policyId.ToString
                        Dim fechafin As Date = res.policy.validityPeriod.endDate.ToString
                        txtVigSegDanios.Text = fechafin.ToString("dd/MM/yyyy")
                    End If
                End If
            End If
            EmitirMARSH = True
        Catch ex As Exception
            EmitirMARSH = False
            Master.MensajeError("Error WS: " & ex.Message.ToString.Replace(vbLf, ", "))
        End Try
    End Function
    Public Function EmitirBBVA(ByVal id_poliza As String, ByVal tienevida As Integer) As Boolean
        Try
            EmitirBBVA = False
            Dim clsdatos As New clsSeguros
            clsdatos._ID_SOLICITUD = hdSolicitud.Value
            Dim dsres As New DataSet
            dsres = clsdatos.getDatosBroker(5)
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    If SaveClientData(id_poliza, dsres) Then
                        If SaveCarData(id_poliza, dsres) Then
                            If FormalizeSeg(id_poliza) Then
                                If CreatePolicyBBVA(id_poliza, dsres, 1) Then
                                    EmitirBBVA = True
                                Else
                                    Exit Function
                                End If
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Else
                    Throw New Exception("No se cuenta con datos para emitir")
                End If
            Else
                Throw New Exception("No se cuenta con datos para emitir")
            End If
        Catch ex As Exception
            EmitirBBVA = False
            Master.MensajeError("Error WS: " & ex.Message.ToString.Replace(vbLf, ", "))
        End Try
    End Function
    Public Function SaveClientData(ByVal id_poliza As String, ByVal dsdat As DataSet) As Boolean
        Try
            SaveClientData = False
            Dim clsClientSav As New SaveClientBBVA
            Dim numero_interno As String
            If dsdat.Tables(0).Rows(0).Item("internalNumber").ToString.Length < 10 Then
                If dsdat.Tables(0).Rows(0).Item("internalNumber").ToString = "" Then
                    numero_interno = " "
                Else
                    numero_interno = dsdat.Tables(0).Rows(0).Item("internalNumber").ToString
                End If
            Else
                numero_interno = dsdat.Tables(0).Rows(0).Item("internalNumber").ToString.Substring(0, 9)
            End If
            Dim idcol As String = ""
            Try
                idcol = GetIdCol(dsdat.Tables(0).Rows(0).Item("zipCode").ToString, dsdat.Tables(0).Rows(0).Item("neighborhood").ToString)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
            clsClientSav.header.aapType = System.Configuration.ConfigurationManager.AppSettings("aapTypeBBVA").ToString()
            clsClientSav.header.dateRequest = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
            clsClientSav.header.channel = System.Configuration.ConfigurationManager.AppSettings("channelBBVA").ToString()
            clsClientSav.header.subChannel = System.Configuration.ConfigurationManager.AppSettings("subChannelBBVA").ToString()
            clsClientSav.header.branchOffice = System.Configuration.ConfigurationManager.AppSettings("branchOfficeBBVA").ToString()
            clsClientSav.header.managementUnit = System.Configuration.ConfigurationManager.AppSettings("managementUnitBBVA").ToString()
            clsClientSav.header.user = System.Configuration.ConfigurationManager.AppSettings("userBBVA").ToString()
            clsClientSav.header.idSession = "3232-3232" 'idsol,id_usuario,id_cot
            clsClientSav.header.idRequest = "1212-121212-12121-212" 'idtarea
            clsClientSav.header.dateConsumerInvocation = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")

            clsClientSav.quote.idQuote = id_poliza

            clsClientSav.holder.email = dsdat.Tables(0).Rows(0).Item("email").ToString
            clsClientSav.holder.rfc = dsdat.Tables(0).Rows(0).Item("rfc").ToString
            clsClientSav.holder.activity.catalogItemBase.id = "707"
            clsClientSav.holder.activity.catalogItemBase.name = "ASOCIACION CIVIL"

            clsClientSav.holder.mainAddress.zipCode = dsdat.Tables(0).Rows(0).Item("zipCode").ToString
            clsClientSav.holder.mainAddress.streetName = dsdat.Tables(0).Rows(0).Item("streetName").ToString
            clsClientSav.holder.mainAddress.outdoorNumber = dsdat.Tables(0).Rows(0).Item("externalNumber").ToString
            clsClientSav.holder.mainAddress.door = numero_interno
            clsClientSav.holder.mainAddress.suburb.neighborhood.catalogItemBase.name = dsdat.Tables(0).Rows(0).Item("neighborhood").ToString
            clsClientSav.holder.mainAddress.suburb.neighborhood.catalogItemBase.id = idcol

            clsClientSav.holder.correspondenceAddress.zipCode = dsdat.Tables(0).Rows(0).Item("zipCode").ToString
            clsClientSav.holder.correspondenceAddress.streetName = dsdat.Tables(0).Rows(0).Item("streetName").ToString
            clsClientSav.holder.correspondenceAddress.outdoorNumber = dsdat.Tables(0).Rows(0).Item("externalNumber").ToString
            clsClientSav.holder.correspondenceAddress.door = numero_interno
            clsClientSav.holder.correspondenceAddress.suburb.neighborhood.catalogItemBase.name = dsdat.Tables(0).Rows(0).Item("neighborhood").ToString
            clsClientSav.holder.correspondenceAddress.suburb.neighborhood.catalogItemBase.id = idcol

            clsClientSav.holder.legalAddress.zipCode = dsdat.Tables(0).Rows(0).Item("zipCode").ToString
            clsClientSav.holder.legalAddress.streetName = dsdat.Tables(0).Rows(0).Item("streetName").ToString
            clsClientSav.holder.legalAddress.outdoorNumber = dsdat.Tables(0).Rows(0).Item("externalNumber").ToString
            clsClientSav.holder.legalAddress.door = numero_interno
            clsClientSav.holder.legalAddress.suburb.neighborhood.catalogItemBase.name = dsdat.Tables(0).Rows(0).Item("neighborhood").ToString
            clsClientSav.holder.legalAddress.suburb.neighborhood.catalogItemBase.id = idcol

            clsClientSav.holder.nacionality.catalogItemBase.id = "052"
            clsClientSav.holder.nacionality.catalogItemBase.name = "MEXICO"

            clsClientSav.holder.mainContactData.cellphone.phoneExtension = "1"
            clsClientSav.holder.mainContactData.cellphone.telephoneNumber = dsdat.Tables(0).Rows(0).Item("MobileNumber").ToString

            clsClientSav.holder.mainContactData.phone.phoneExtension = "1"
            clsClientSav.holder.mainContactData.phone.telephoneNumber = IIf(dsdat.Tables(0).Rows(0).Item("telephoneNumber").ToString = "", " ", dsdat.Tables(0).Rows(0).Item("telephoneNumber").ToString)

            clsClientSav.holder.mainContactData.officePhone.phoneExtension = IIf(dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString = "" Or dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString = "0", 1, dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString)
            clsClientSav.holder.mainContactData.officePhone.telephoneNumber = dsdat.Tables(0).Rows(0).Item("OfficeNumber").ToString

            clsClientSav.holder.correspondenceContactData.cellphone.phoneExtension = "1"
            clsClientSav.holder.correspondenceContactData.cellphone.telephoneNumber = dsdat.Tables(0).Rows(0).Item("MobileNumber").ToString

            clsClientSav.holder.correspondenceContactData.phone.phoneExtension = "1"
            clsClientSav.holder.correspondenceContactData.phone.telephoneNumber = IIf(dsdat.Tables(0).Rows(0).Item("telephoneNumber").ToString = "", " ", dsdat.Tables(0).Rows(0).Item("telephoneNumber").ToString)

            clsClientSav.holder.correspondenceContactData.officePhone.phoneExtension = IIf(dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString = "" Or dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString = "0", 1, dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString)
            clsClientSav.holder.correspondenceContactData.officePhone.telephoneNumber = dsdat.Tables(0).Rows(0).Item("OfficeNumber").ToString

            clsClientSav.holder.fiscalContactData.cellphone.phoneExtension = "1"
            clsClientSav.holder.fiscalContactData.cellphone.telephoneNumber = dsdat.Tables(0).Rows(0).Item("MobileNumber").ToString

            clsClientSav.holder.fiscalContactData.phone.phoneExtension = "1"
            clsClientSav.holder.fiscalContactData.phone.telephoneNumber = IIf(dsdat.Tables(0).Rows(0).Item("telephoneNumber").ToString = "", " ", dsdat.Tables(0).Rows(0).Item("telephoneNumber").ToString)

            clsClientSav.holder.fiscalContactData.officePhone.phoneExtension = IIf(dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString = "" Or dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString = "0", 1, dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString)
            clsClientSav.holder.fiscalContactData.officePhone.telephoneNumber = dsdat.Tables(0).Rows(0).Item("OfficeNumber").ToString

            clsClientSav.holder.physicalPersonalityData.name = dsdat.Tables(0).Rows(0).Item("name").ToString
            clsClientSav.holder.physicalPersonalityData.lastName = dsdat.Tables(0).Rows(0).Item("lastName").ToString
            clsClientSav.holder.physicalPersonalityData.birthDate = dsdat.Tables(0).Rows(0).Item("birthDate").ToString
            clsClientSav.holder.physicalPersonalityData.sex.catalogItemBase.id = dsdat.Tables(0).Rows(0).Item("gender").ToString
            clsClientSav.holder.physicalPersonalityData.sex.catalogItemBase.name = IIf(dsdat.Tables(0).Rows(0).Item("gender").ToString = "F", "FEMENINO", "MASCULINO")
            clsClientSav.holder.physicalPersonalityData.mothersLastName = dsdat.Tables(0).Rows(0).Item("mothersLastName").ToString
            clsClientSav.holder.physicalPersonalityData.curp = dsdat.Tables(0).Rows(0).Item("CURP").ToString
            clsClientSav.holder.physicalPersonalityData.civilStatus.catalogItemBase.id = dsdat.Tables(0).Rows(0).Item("maritalStatus").ToString
            clsClientSav.holder.physicalPersonalityData.civilStatus.catalogItemBase.name = dsdat.Tables(0).Rows(0).Item("maritalStatusname").ToString
            clsClientSav.holder.physicalPersonalityData.occupation.catalogItemBase.id = "446"
            clsClientSav.holder.physicalPersonalityData.occupation.catalogItemBase.name = "DOMADOR DE ANIMALES"
            clsClientSav.holder.physicalPersonalityData.fiscalPersonality.catalogItemBase.name = dsdat.Tables(0).Rows(0).Item("relationNam").ToString
            clsClientSav.holder.physicalPersonalityData.fiscalPersonality.catalogItemBase.id = dsdat.Tables(0).Rows(0).Item("relationName").ToString


            clsClientSav.holder.deliveryInformation.referenceStreets = " "
            clsClientSav.holder.deliveryInformation.deliveryInstructions = " "
            clsClientSav.holder.deliveryInformation.deliveryTimeStart = Date.Now.ToString("yyyy-MM-dd hh:mm:ss")
            clsClientSav.holder.deliveryInformation.deliveryTimeEnd = Date.Now.ToString("yyyy-MM-dd hh:mm:ss")

            clsClientSav.clientType.catalogItemBase.id = "P"
            clsClientSav.clientType.catalogItemBase.name = "PERSONA"

            clsClientSav.contractingHolderIndicator = True

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonbody As String = serializer.Serialize(clsClientSav)
            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("createCustomerDataBBVA").ToString()

            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
            Dim errormessage As String
            If (restful.IsError) Then
                If txtNumSegDanios.Text = "" Then
                    Dim res As New msjerrBBVA
                    Try
                        res = serializer.Deserialize(Of msjerrBBVA)(restful.MensajeError)
                    Catch ex As Exception
                        Throw New Exception(restful.MensajeError.ToString)
                    End Try
                    If (res.errorId.ToString = "20326" Or res.errorId.ToString = "20310") And Not IsNothing(res.errorId.ToString) Then
                        SaveClientData = True
                    Else
                        errormessage = res.message.ToString.Replace(vbLf, ", ")
                        Throw New Exception(errormessage)
                    End If
                Else
                    SaveClientData = True
                End If
            Else
                SaveClientData = True
            End If
        Catch ex As Exception
            SaveClientData = False
            Master.MensajeError("Error WS: " & ex.Message.ToString.Replace(vbLf, ", "))
        End Try
    End Function
    Public Function SaveClientDataLife(ByVal id_poliza As String, ByVal dsdat As DataSet) As Boolean
        Try
            SaveClientDataLife = False
            Dim clsClientSav As New SaveClientBBVA
            Dim numero_interno As String
            If dsdat.Tables(0).Rows(0).Item("internalNumber").ToString.Length < 10 Then
                If dsdat.Tables(0).Rows(0).Item("internalNumber").ToString = "" Then
                    numero_interno = " "
                Else
                    numero_interno = dsdat.Tables(0).Rows(0).Item("internalNumber").ToString
                End If
            Else
                numero_interno = dsdat.Tables(0).Rows(0).Item("internalNumber").ToString.Substring(0, 9)
            End If
            Dim idcol As String = ""
            Try
                idcol = GetIdCol(dsdat.Tables(0).Rows(0).Item("zipCode").ToString, dsdat.Tables(0).Rows(0).Item("neighborhood").ToString)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
            clsClientSav.header.aapType = System.Configuration.ConfigurationManager.AppSettings("aapTypeBBVA").ToString()
            clsClientSav.header.dateRequest = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
            clsClientSav.header.channel = System.Configuration.ConfigurationManager.AppSettings("channelBBVA").ToString()
            clsClientSav.header.subChannel = System.Configuration.ConfigurationManager.AppSettings("subChannelBBVA").ToString()
            clsClientSav.header.branchOffice = System.Configuration.ConfigurationManager.AppSettings("branchOfficeBBVA").ToString()
            clsClientSav.header.managementUnit = "VINCF002"
            clsClientSav.header.user = System.Configuration.ConfigurationManager.AppSettings("userBBVA").ToString()
            clsClientSav.header.idSession = "3232-3232" 'idsol,id_usuario,id_cot
            clsClientSav.header.idRequest = "1212-121212-12121-212" 'idtarea
            clsClientSav.header.dateConsumerInvocation = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")

            clsClientSav.quote.idQuote = id_poliza

            clsClientSav.holder.email = dsdat.Tables(0).Rows(0).Item("email").ToString
            clsClientSav.holder.rfc = dsdat.Tables(0).Rows(0).Item("rfc").ToString
            clsClientSav.holder.activity.catalogItemBase.id = "707"
            clsClientSav.holder.activity.catalogItemBase.name = "ASOCIACION CIVIL"

            clsClientSav.holder.mainAddress.zipCode = dsdat.Tables(0).Rows(0).Item("zipCode").ToString
            clsClientSav.holder.mainAddress.streetName = dsdat.Tables(0).Rows(0).Item("streetName").ToString
            clsClientSav.holder.mainAddress.outdoorNumber = dsdat.Tables(0).Rows(0).Item("externalNumber").ToString
            clsClientSav.holder.mainAddress.door = numero_interno
            clsClientSav.holder.mainAddress.suburb.neighborhood.catalogItemBase.name = dsdat.Tables(0).Rows(0).Item("neighborhood").ToString
            clsClientSav.holder.mainAddress.suburb.neighborhood.catalogItemBase.id = idcol

            clsClientSav.holder.correspondenceAddress.zipCode = dsdat.Tables(0).Rows(0).Item("zipCode").ToString
            clsClientSav.holder.correspondenceAddress.streetName = dsdat.Tables(0).Rows(0).Item("streetName").ToString
            clsClientSav.holder.correspondenceAddress.outdoorNumber = dsdat.Tables(0).Rows(0).Item("externalNumber").ToString
            clsClientSav.holder.correspondenceAddress.door = numero_interno
            clsClientSav.holder.correspondenceAddress.suburb.neighborhood.catalogItemBase.name = dsdat.Tables(0).Rows(0).Item("neighborhood").ToString
            clsClientSav.holder.correspondenceAddress.suburb.neighborhood.catalogItemBase.id = idcol

            clsClientSav.holder.legalAddress.zipCode = dsdat.Tables(0).Rows(0).Item("zipCode").ToString
            clsClientSav.holder.legalAddress.streetName = dsdat.Tables(0).Rows(0).Item("streetName").ToString
            clsClientSav.holder.legalAddress.outdoorNumber = dsdat.Tables(0).Rows(0).Item("externalNumber").ToString
            clsClientSav.holder.legalAddress.door = numero_interno
            clsClientSav.holder.legalAddress.suburb.neighborhood.catalogItemBase.name = dsdat.Tables(0).Rows(0).Item("neighborhood").ToString
            clsClientSav.holder.legalAddress.suburb.neighborhood.catalogItemBase.id = idcol

            clsClientSav.holder.nacionality.catalogItemBase.id = "052"
            clsClientSav.holder.nacionality.catalogItemBase.name = "MEXICO"

            clsClientSav.holder.mainContactData.cellphone.phoneExtension = "1"
            clsClientSav.holder.mainContactData.cellphone.telephoneNumber = dsdat.Tables(0).Rows(0).Item("MobileNumber").ToString

            clsClientSav.holder.mainContactData.phone.phoneExtension = "1"
            clsClientSav.holder.mainContactData.phone.telephoneNumber = IIf(dsdat.Tables(0).Rows(0).Item("telephoneNumber").ToString = "", " ", dsdat.Tables(0).Rows(0).Item("telephoneNumber").ToString)

            clsClientSav.holder.mainContactData.officePhone.phoneExtension = IIf(dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString = "" Or dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString = "0", 1, dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString)
            clsClientSav.holder.mainContactData.officePhone.telephoneNumber = dsdat.Tables(0).Rows(0).Item("OfficeNumber").ToString

            clsClientSav.holder.correspondenceContactData.cellphone.phoneExtension = "1"
            clsClientSav.holder.correspondenceContactData.cellphone.telephoneNumber = dsdat.Tables(0).Rows(0).Item("MobileNumber").ToString

            clsClientSav.holder.correspondenceContactData.phone.phoneExtension = "1"
            clsClientSav.holder.correspondenceContactData.phone.telephoneNumber = IIf(dsdat.Tables(0).Rows(0).Item("telephoneNumber").ToString = "", " ", dsdat.Tables(0).Rows(0).Item("telephoneNumber").ToString)

            clsClientSav.holder.correspondenceContactData.officePhone.phoneExtension = IIf(dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString = "" Or dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString = "0", 1, dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString)
            clsClientSav.holder.correspondenceContactData.officePhone.telephoneNumber = dsdat.Tables(0).Rows(0).Item("OfficeNumber").ToString

            clsClientSav.holder.fiscalContactData.cellphone.phoneExtension = "1"
            clsClientSav.holder.fiscalContactData.cellphone.telephoneNumber = dsdat.Tables(0).Rows(0).Item("MobileNumber").ToString

            clsClientSav.holder.fiscalContactData.phone.phoneExtension = "1"
            clsClientSav.holder.fiscalContactData.phone.telephoneNumber = IIf(dsdat.Tables(0).Rows(0).Item("telephoneNumber").ToString = "", " ", dsdat.Tables(0).Rows(0).Item("telephoneNumber").ToString)

            clsClientSav.holder.fiscalContactData.officePhone.phoneExtension = IIf(dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString = "" Or dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString = "0", 1, dsdat.Tables(0).Rows(0).Item("extOfficeNumber").ToString)
            clsClientSav.holder.fiscalContactData.officePhone.telephoneNumber = dsdat.Tables(0).Rows(0).Item("OfficeNumber").ToString

            clsClientSav.holder.physicalPersonalityData.name = dsdat.Tables(0).Rows(0).Item("name").ToString
            clsClientSav.holder.physicalPersonalityData.lastName = dsdat.Tables(0).Rows(0).Item("lastName").ToString
            clsClientSav.holder.physicalPersonalityData.birthDate = dsdat.Tables(0).Rows(0).Item("birthDate").ToString
            clsClientSav.holder.physicalPersonalityData.sex.catalogItemBase.id = dsdat.Tables(0).Rows(0).Item("gender").ToString
            clsClientSav.holder.physicalPersonalityData.sex.catalogItemBase.name = IIf(dsdat.Tables(0).Rows(0).Item("gender").ToString = "F", "FEMENINO", "MASCULINO")
            clsClientSav.holder.physicalPersonalityData.mothersLastName = dsdat.Tables(0).Rows(0).Item("mothersLastName").ToString
            clsClientSav.holder.physicalPersonalityData.curp = dsdat.Tables(0).Rows(0).Item("CURP").ToString
            clsClientSav.holder.physicalPersonalityData.civilStatus.catalogItemBase.id = dsdat.Tables(0).Rows(0).Item("maritalStatus").ToString
            clsClientSav.holder.physicalPersonalityData.civilStatus.catalogItemBase.name = dsdat.Tables(0).Rows(0).Item("maritalStatusname").ToString
            clsClientSav.holder.physicalPersonalityData.occupation.catalogItemBase.id = "446"
            clsClientSav.holder.physicalPersonalityData.occupation.catalogItemBase.name = "DOMADOR DE ANIMALES"
            clsClientSav.holder.physicalPersonalityData.fiscalPersonality.catalogItemBase.name = dsdat.Tables(0).Rows(0).Item("relationNam").ToString
            clsClientSav.holder.physicalPersonalityData.fiscalPersonality.catalogItemBase.id = dsdat.Tables(0).Rows(0).Item("relationName").ToString


            clsClientSav.holder.deliveryInformation.referenceStreets = " "
            clsClientSav.holder.deliveryInformation.deliveryInstructions = " "
            clsClientSav.holder.deliveryInformation.deliveryTimeStart = Date.Now.ToString("yyyy-MM-dd hh:mm:ss")
            clsClientSav.holder.deliveryInformation.deliveryTimeEnd = Date.Now.ToString("yyyy-MM-dd hh:mm:ss")

            clsClientSav.clientType.catalogItemBase.id = "P"
            clsClientSav.clientType.catalogItemBase.name = "PERSONA"

            clsClientSav.contractingHolderIndicator = True

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonbody As String = serializer.Serialize(clsClientSav)
            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("createCustomerDataBBVA").ToString()

            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
            Dim errormessage As String
            If (restful.IsError) Then
                If txtNumSegDanios.Text = "" Then
                    Dim res As New msjerrBBVA
                    Try
                        res = serializer.Deserialize(Of msjerrBBVA)(restful.MensajeError)
                    Catch ex As Exception
                        Throw New Exception(restful.MensajeError.ToString)
                    End Try
                    If (res.errorId.ToString = "20326" Or res.errorId.ToString = "20310") And Not IsNothing(res.errorId.ToString) Then
                        SaveClientDataLife = True
                    Else
                        errormessage = res.message.ToString.Replace(vbLf, ", ")
                        Throw New Exception(errormessage)
                    End If
                Else
                    SaveClientDataLife = True
                End If
            Else
                SaveClientDataLife = True
            End If
        Catch ex As Exception
            SaveClientDataLife = False
            Master.MensajeError("Error WS: " & ex.Message.ToString.Replace(vbLf, ", "))
        End Try
    End Function
    Public Function SaveCarData(ByVal id_policy As String, ByVal dsres As DataSet) As Boolean
        Try
            SaveCarData = False
            Dim clscardata As New CreateCarDataBBVA
            clscardata.header.aapType = System.Configuration.ConfigurationManager.AppSettings("aapTypeBBVA").ToString()
            clscardata.header.dateRequest = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
            clscardata.header.channel = System.Configuration.ConfigurationManager.AppSettings("channelBBVA").ToString()
            clscardata.header.subChannel = System.Configuration.ConfigurationManager.AppSettings("subChannelBBVA").ToString()
            clscardata.header.managementUnit = System.Configuration.ConfigurationManager.AppSettings("branchOfficeBBVA").ToString()
            clscardata.header.branchOffice = System.Configuration.ConfigurationManager.AppSettings("managementUnitBBVA").ToString()
            clscardata.header.user = System.Configuration.ConfigurationManager.AppSettings("userBBVA").ToString()
            clscardata.header.idSession = "3232-3232"
            clscardata.header.idRequest = "1212-121212-12121-212"
            clscardata.header.dateConsumerInvocation = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")

            clscardata.quote.idQuote = id_policy

            clscardata.carData.car.serialNumber = dsres.Tables(0).Rows(0).Item("serialNumber").ToString
            clscardata.carData.car.engineNumber = dsres.Tables(0).Rows(0).Item("engineNumber").ToString
            clscardata.carData.experienceYears = "1"
            clscardata.carData.credit.creditNumber = dsres.Tables(0).Rows(0).Item("numberFinancialContract").ToString
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonbody As String = serializer.Serialize(clscardata)
            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("createCarDataBBVA").ToString()

            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
            Dim errormessage As String
            If (restful.IsError) Then
                If txtNumSegDanios.Text = "" Then
                    Dim res As msjerrBBVA = serializer.Deserialize(Of msjerrBBVA)(restful.MensajeError)
                    errormessage = res.message.ToString.Replace(vbLf, ", ")
                    Throw New Exception(errormessage)
                Else
                    SaveCarData = True
                End If
            Else
                Dim res As msjerrBBVA = serializer.Deserialize(Of msjerrBBVA)(respuesta)
                If Not IsNothing(res.errorId) Then
                    If res.errorId.ToString = "20326" Then
                        SaveCarData = True
                    Else
                        Throw New Exception(res.message.ToString.Replace(vbLf, ", "))
                    End If
                Else
                    SaveCarData = True
                End If
            End If
        Catch ex As Exception
            SaveCarData = False
            Master.MensajeError("Error WS: " & ex.Message.ToString.Replace(vbLf, ", "))
        End Try
    End Function
    Public Function FormalizeSeg(ByVal id_poliza As String) As Boolean
        Try
            FormalizeSeg = False
            Dim clsfromseg As New createquoteBBVA
            clsfromseg.header.aapType = System.Configuration.ConfigurationManager.AppSettings("aapTypeBBVA").ToString()
            clsfromseg.header.dateRequest = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
            clsfromseg.header.channel = System.Configuration.ConfigurationManager.AppSettings("channelBBVA").ToString()
            clsfromseg.header.subChannel = System.Configuration.ConfigurationManager.AppSettings("subChannelBBVA").ToString()
            clsfromseg.header.branchOffice = System.Configuration.ConfigurationManager.AppSettings("branchOfficeBBVA").ToString()
            clsfromseg.header.managementUnit = System.Configuration.ConfigurationManager.AppSettings("managementUnitBBVA").ToString()
            clsfromseg.header.user = System.Configuration.ConfigurationManager.AppSettings("userBBVA").ToString()
            clsfromseg.header.idSession = "3232-3232"
            clsfromseg.header.idRequest = "1212-121212-12121-212"
            clsfromseg.header.dateConsumerInvocation = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")

            clsfromseg.quote.idQuote = id_poliza
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonbody As String = serializer.Serialize(clsfromseg)
            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String).ToString
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String).ToString
            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("createQuoteBBVA").ToString()

            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
            Dim errormessage As String
            If (restful.IsError) Then
                If txtNumSegDanios.Text = "" Then
                    Dim res As msjerrBBVA = serializer.Deserialize(Of msjerrBBVA)(restful.MensajeError)
                    errormessage = res.message.ToString.Replace(vbLf, ", ")
                    Throw New Exception(errormessage)
                Else
                    FormalizeSeg = True
                End If
            Else
                Dim res As msjerrBBVA = serializer.Deserialize(Of msjerrBBVA)(respuesta)
                If Not IsNothing(res.message) Then
                    If res.errorId.ToString = "20341" Then
                        FormalizeSeg = True
                    Else
                        Throw New Exception(res.message.ToString.Replace(vbLf, ", "))
                    End If
                End If
                FormalizeSeg = True
            End If

        Catch ex As Exception
            FormalizeSeg = False
            Master.MensajeError("Error WS: " & ex.Message)
        End Try
    End Function
    Public Function CreatePolicyBBVA(ByVal id_poliza As String, ByVal dsres As DataSet, ByVal tiposeg As Integer) As Boolean
        Try
            CreatePolicyBBVA = False
            Dim clsfromseg As New createquoteBBVA
            clsfromseg.header.aapType = System.Configuration.ConfigurationManager.AppSettings("aapTypeBBVA").ToString()
            clsfromseg.header.dateRequest = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
            clsfromseg.header.channel = System.Configuration.ConfigurationManager.AppSettings("channelBBVA").ToString()
            clsfromseg.header.subChannel = System.Configuration.ConfigurationManager.AppSettings("subChannelBBVA").ToString()
            clsfromseg.header.branchOffice = System.Configuration.ConfigurationManager.AppSettings("branchOfficeBBVA").ToString()
            clsfromseg.header.managementUnit = System.Configuration.ConfigurationManager.AppSettings("managementUnitBBVA").ToString()
            clsfromseg.header.user = System.Configuration.ConfigurationManager.AppSettings("userBBVA").ToString()
            clsfromseg.header.idSession = "3232-3232"
            clsfromseg.header.idRequest = "1212-121212-12121-212"
            clsfromseg.header.dateConsumerInvocation = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")

            clsfromseg.quote.idQuote = id_poliza
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonbody As String = serializer.Serialize(clsfromseg)
            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String).ToString
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String).ToString
            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("GetPolicy").ToString()

            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
            If (restful.IsError) Then
                Throw New Exception(restful.MensajeError.ToString.Replace(vbLf, ", "))
            Else
                Dim res = serializer.Deserialize(Of resgetpolicybbva)(respuesta)
                Dim fechafin As Date = dsres.Tables(0).Rows(0).Item("endDate").ToString
                Dim seguros As New clsSeguros
                If tiposeg = 1 Then
                    seguros._ID_SOLICITUD = hdSolicitud.Value
                    seguros._NOMSEGDANIOS = txtNomSegDanios.Text
                    seguros._NUMSEGDANIOS = res.mainPolicy.idPolicy.ToString
                    txtNumSegDanios.Text = res.mainPolicy.idPolicy.ToString
                    seguros._VIGSEGDANIOS = fechafin.ToString("yyyy/MM/dd")
                    txtVigSegDanios.Text = fechafin.ToString("dd/MM/yyyy")
                ElseIf tiposeg = 2 Then
                    seguros._ID_SOLICITUD = hdSolicitud.Value
                    seguros._NOMSEGVIDA = txtNomSegVida.Text
                    seguros._NUMSEGVIDA = res.mainPolicy.idPolicy.ToString
                    txtNumSegVida.Text = res.mainPolicy.idPolicy.ToString
                    seguros._VIGSEGVIDA = fechafin.ToString("yyyy/MM/dd")
                    txtVigSegVida.Text = fechafin.ToString("dd/MM/yyyy")
                    seguros._PDK_ID_QUOTELIFE = idquotelife.Value
                End If
                If seguros.InsertDatosSeguroSolicitud() Then
                Else
                    Throw New Exception(seguros.StrError)
                End If
                CreatePolicyBBVA = True
            End If

        Catch ex As Exception
            CreatePolicyBBVA = False
            Master.MensajeError("Error WS: " & ex.Message.ToString.Replace(vbLf, ", "))
        End Try
    End Function
    Public Function EmitirVidaBBVA(ByVal idBrokerdanos As String) As Boolean
        Try
            EmitirVidaBBVA = False
            Dim clsdatos As New clsSeguros
            clsdatos._ID_SOLICITUD = hdSolicitud.Value
            Dim dsres As New DataSet
            dsres = clsdatos.getDatosBroker(5)
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    If SaveClientDataLife(idBrokerdanos, dsres) Then
                        If Cotizasegvidabbva(idBrokerdanos, dsres) Then
                            If FormalizeSeg(idBrokerdanos) Then
                                If CreatePolicyBBVA(idBrokerdanos, dsres, 2) Then
                                    EmitirVidaBBVA = True
                                Else
                                    Exit Function
                                End If
                            Else
                                Exit Function
                            End If
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                Else
                    Throw New Exception("No se cuenta con datos para emitir")
                End If
            Else
                Throw New Exception("No se cuenta con datos para emitir")
            End If
        Catch ex As Exception
            EmitirVidaBBVA = False
            Master.MensajeError("Error WS: " & ex.Message.ToString.Replace(vbLf, ", "))
        End Try
    End Function

#Region "Ingesta_Documentos Seguros"
    Public Class IngestaDocumentos
        Public repositoryName As String
        Public documentFiles As New List(Of documentFiles)
    End Class
    Public Class documentFiles
        Public name As String
        Public size As Integer
        Public extension As String
        Public encodeData As String
        Public extendedData As extendedData = New extendedData()

    End Class
    Public Class extendedData
        Public mapMetadata As New mapMetadata()
        Public sha1N As String
    End Class
    Public Class mapMetadata
        Public f As String
        Public fo As Integer
        Public no As Integer
        Public v As Integer
    End Class
    Public Class Respuesta
        Public documentFiles As New List(Of documentFilesres)
    End Class
    Public Class documentFilesres
        Public extendedData As extendedDatares = New extendedDatares()
    End Class
    Public Class extendedDatares
        Public mapMetadata As mapMetadatares = New mapMetadatares()
    End Class
    Public Class mapMetadatares
        Public _s3_key As String
    End Class
#End Region

    Protected Sub btnEmisionDanRetry_Click(sender As Object, e As EventArgs) Handles btnEmisionDanRetry.Click
        ProcesoSeguros(0)
        btnEmisionDanRetry.Enabled = True
    End Sub

    Protected Sub btnImpresionDanRetry_Click(sender As Object, e As EventArgs) Handles btnImpresionDanRetry.Click
        ProcesoSeguros(1)
        btnImpresionDanRetry.Enabled = True
    End Sub

    Protected Sub btnEmisionVidRetry_Click(sender As Object, e As EventArgs) Handles btnEmisionVidRetry.Click
        ProcesoSeguros(2)
        btnEmisionVidRetry.Enabled = True
    End Sub

    Protected Sub btnImpresionVidRetry_Click(sender As Object, e As EventArgs) Handles btnImpresionVidRetry.Click
        ProcesoSeguros(3)
        btnImpresionVidRetry.Enabled = True
    End Sub
    Protected Sub btnproc_Click(sender As Object, e As EventArgs)
        cmbguardar1.Disabled = True
        Dim dsConsulta As New DataSet
        clsCat.Parametro = Val(Request("Sol")).ToString
        dsConsulta = clsCat.Catalogos(18)

        If ddlTurnar.Visible = False Then
            'If (Not IsNothing(dsConsulta) AndAlso dsConsulta.Tables.Count > 0 AndAlso dsConsulta.Tables(0).Rows.Count > 0) Then
            '    If hdPantalla.Value = 74 Or hdPantalla.Value = 89 Or hdPantalla.Value = 105 Then
            '        Dim doc1 As Boolean = False
            '        Dim doc2 As Boolean = False
            '        Dim doc3 As Boolean = False

            '        doc1 = dsConsulta.Tables(0).Rows(0).Item("VALIDADO")
            '        If dsConsulta.Tables(0).Rows.Count > 1 Then
            '            doc2 = dsConsulta.Tables(0).Rows(1).Item("VALIDADO")
            '        End If
            '        If dsConsulta.Tables(0).Rows.Count > 2 Then
            '            doc3 = dsConsulta.Tables(0).Rows(2).Item("VALIDADO")
            '        End If

            '        If (doc1 = False Or doc2 = False Or doc3 = False) Then

            '            ClsEmail.OPCION = 17
            '            ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

            '            Dim dtConsulta = New DataSet()
            '            dtConsulta = ClsEmail.ConsultaStatusNotificacion
            '            If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
            '                If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
            '                    If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
            '                        If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
            '                            Dim strLocation As String = String.Empty
            '                            strLocation = ("../aspx/ValidaEmails.aspx?idPantalla=" & Val(Request("pantalla")) & "&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=2" & "&usuario=" & usu & "&usu=" & usu)
            '                            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            '                        Else
            '                            Dim strLocation As String
            '                            strLocation = ("../aspx/consultaPanelControl.aspx")
            '                            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            '                        End If
            '                    Else
            '                        Dim strLocation As String
            '                        strLocation = ("../aspx/consultaPanelControl.aspx")
            '                        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            '                    End If
            '                Else
            '                    Dim strLocation As String
            '                    strLocation = ("../aspx/consultaPanelControl.aspx")
            '                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            '                End If
            '            Else

            '            End If
            '        Else
            '            Dim strLocation As String
            '            strLocation = ("../aspx/consultaPanelControl.aspx")
            '            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            '        End If
            '    Else
            asignaTarea(0)
            '    End If
            'Else
            '    Dim strLocation As String
            '    strLocation = ("../aspx/consultaPanelControl.aspx")
            '    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            'End If
        Else
            If ddlTurnar.SelectedValue = 0 Then
                If hdPantalla.Value = "63" Then
                    Master.MensajeError("Debe seleccionar una tipificacion.")
                    cmbguardar1.Attributes.Remove("disabled")
                Else
                    asignaTarea(0)
                End If
            Else
                If hdPantalla.Value = "63" Or hdPantalla.Value = "89" Then
                    Dim ds_siguienteTarea As DataSet
                    ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & hdPantalla.Value.ToString)

                    If ddlTurnar.SelectedValue = -1 Then
                        asignaTarea(0)
                    ElseIf ddlTurnar.SelectedValue = -2 Then
                        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
                        Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
                        Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
                        Solicitudes.Estatus = 295
                        Solicitudes.Estatus_Cred = 295
                        Solicitudes.Comentario = ddlTurnar.SelectedItem.Text
                        Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
                        Solicitudes.ManejaTarea(6)
                        Master.MsjErrorRedirect("Solicitud Declinada", "../aspx/consultaPanelControl.aspx")
                    ElseIf ddlTurnar.SelectedValue = -3 Then
                        If hdPantalla.Value = "89" Then
                            If Not Guarda_No_procesable() Then
                                cmbguardar1.Disabled = False
                                Exit Sub
                            End If
                        End If
                        asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))
                    Else
                        asignaTarea(ddlTurnar.SelectedValue)
                    End If
                Else
                    asignaTarea(ddlTurnar.SelectedValue)
                End If
            End If
        End If
    End Sub
    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("solicitud")))
        Dim mensaje As String = String.Empty
        Try

            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("solicitud"))
            Solicitudes.PDK_CLAVE_USUARIO = Request("usu")
            Solicitudes.PDK_ID_PANTALLA = Request("pantalla")

            If idAsignarPantalla <> 0 Then
                Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla
            End If
            If ddlTurnar.Visible = True Then
                If ddlTurnar.SelectedValue = -1 Or (Request("pantalla") = "8" AndAlso ddlTurnar.SelectedValue = 0) Then
                    BD.AgregaParametro("@FOLIO", TipoDato.Entero, hdSolicitud.Value)
                    BD.AgregaParametro("@BANDERA", TipoDato.Entero, 2)
                    BD.AgregaParametro("@PANTALLA", TipoDato.Entero, Request("pantalla"))
                    Dim ds_validardocumento As New DataSet
                    ds_validardocumento = BD.EjecutaStoredProcedure("sp_validarEntrevista")
                    mensaje = ds_validardocumento.Tables(0).Rows(0).Item("MENSAJE")
                End If
            Else
                BD.AgregaParametro("@FOLIO", TipoDato.Entero, hdSolicitud.Value)
                BD.AgregaParametro("@BANDERA", TipoDato.Entero, 2)
                BD.AgregaParametro("@PANTALLA", TipoDato.Entero, Request("pantalla"))
                Dim ds_validardocumento As New DataSet
                ds_validardocumento = BD.EjecutaStoredProcedure("sp_validarEntrevista")
                mensaje = ds_validardocumento.Tables(0).Rows(0).Item("MENSAJE")
            End If

            If mensaje <> "" Then
                Master.MensajeError(mensaje)
                cmbguardar1.Disabled = False
            Else
                dsresult = Solicitudes.ValNegocio(1)
                mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
                Solicitudes.MENSAJE = mensaje
                Solicitudes.ManejaTarea(5)

                If mensaje <> "Tarea Exitosa" And mensaje <> "SE RECHAZO  DOCUMENTO " Then
                    Throw New Exception(mensaje)
                End If

                dslink = objtarea.SiguienteTarea(Val(Request("solicitud")))

                muestrapant = objpantalla.SiguientePantalla(Val(Request("solicitud")))
                Dim dc As New clsDatosCliente
                dc.idSolicitud = Request("solicitud")
                dc.getDatosSol()


                'Else
                'esta parte va fuera del else
                If muestrapant = 0 Then
                    'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString & "');", True)
                    If (Request("pantalla") = 74 Or Request("pantalla") = 105 Or Request("pantalla") = 89) Then
                        If (mensaje = "SE RECHAZO  DOCUMENTO ") Then

                            ClsEmail.OPCION = 17
                            ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

                            Dim dtConsulta = New DataSet()
                            dtConsulta = ClsEmail.ConsultaStatusNotificacion
                            If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
                                If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                                    If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
                                        If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
                                            Dim strLocation As String = String.Empty
                                            strLocation = ("../aspx/ValidaEmails.aspx?idPantalla=" & Val(Request("pantalla")) & "&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=2" & "&usuario=" & usu & "&usu=" & usu)
                                            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '" + strLocation + "');", True)
                                        Else
                                            Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                                        End If
                                    Else
                                        Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                                        ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                                    End If
                                Else
                                    Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                                End If
                            Else
                                If muestrapant = 0 Then
                                    'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & usu & "&usu=" & usu & "');", True)
                                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & usu & "&usu=" & usu & "');", True)
                                ElseIf muestrapant = 2 Then
                                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "');", True)
                                    'Response.Redirect("../aspx/consultaPanelControl.aspx")
                                End If
                            End If
                            If muestrapant = 0 Then
                                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & usu & "&usu=" & usu & "');", True)
                            ElseIf muestrapant = 2 Then
                                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "');", True)
                                'Response.Redirect("../aspx/consultaPanelControl.aspx")
                            End If
                        Else
                            If muestrapant = 0 Then
                                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & usu & "&usu=" & usu & "');", True)
                            ElseIf muestrapant = 2 Then
                                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "');", True)
                                'Response.Redirect("../aspx/consultaPanelControl.aspx")
                            End If
                        End If
                    Else
                        If muestrapant = 0 Then
                            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & usu & "&usu=" & usu & "');", True)
                        ElseIf muestrapant = 2 Then
                            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "');", True)
                            'Response.Redirect("../aspx/consultaPanelControl.aspx")
                        End If
                    End If
                ElseIf muestrapant = 2 Then
                    'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "&sol=" & Val(Request("Sol")).ToString & "');", True)
                    'Response.Redirect("../aspx/consultaPanelControl.aspx")
                    If (Request("pantalla") = 74 Or Request("pantalla") = 105 Or Request("pantalla") = 89) Then
                        If (mensaje = "SE RECHAZO  DOCUMENTO ") Then

                            ClsEmail.OPCION = 17
                            ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

                            Dim dtConsulta = New DataSet()
                            dtConsulta = ClsEmail.ConsultaStatusNotificacion
                            If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
                                If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                                    If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
                                        If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
                                            Dim strLocation As String = String.Empty
                                            strLocation = ("../aspx/ValidaEmails.aspx?idPantalla=" & Val(Request("pantalla")) & "&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=2" & "&usuario=" & usu & "&usu=" & usu)
                                            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '" + strLocation + "');", True)
                                        Else
                                            Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                                        End If
                                    Else
                                        Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                                        ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                                    End If
                                Else
                                    Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                                End If
                            Else
                                If muestrapant = 0 Then
                                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & usu & "&usu=" & usu & "');", True)
                                ElseIf muestrapant = 2 Then
                                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "');", True)
                                    'Response.Redirect("../aspx/consultaPanelControl.aspx")
                                End If
                            End If
                            If muestrapant = 0 Then
                                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & usu & "&usu=" & usu & "');", True)
                            ElseIf muestrapant = 2 Then
                                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "');", True)
                                'Response.Redirect("../aspx/consultaPanelControl.aspx")
                            End If
                        Else
                            If muestrapant = 0 Then
                                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & usu & "&usu=" & usu & "');", True)
                            ElseIf muestrapant = 2 Then
                                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "&sol=" & Val(Request("Sol")).ToString & "');", True)
                                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "');", True)
                                'Response.Redirect("../aspx/consultaPanelControl.aspx")
                            End If
                        End If
                    Else
                        If muestrapant = 0 Then
                            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & usu & "&usu=" & usu & "');", True)
                        ElseIf muestrapant = 2 Then
                            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "');", True)
                            'Response.Redirect("../aspx/consultaPanelControl.aspx")
                        End If
                    End If
                End If
                'End If


            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
            cmbguardar1.Disabled = False
        End Try
    End Sub
    Public Function GetIdCol(ByVal CP As String, ByVal Name As String) As String
        Try
            Dim clscol As New envioCol
            clscol.header.aapType = System.Configuration.ConfigurationManager.AppSettings("aapTypeBBVA").ToString()
            clscol.header.dateRequest = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
            clscol.header.channel = System.Configuration.ConfigurationManager.AppSettings("channelBBVA").ToString()
            clscol.header.subChannel = System.Configuration.ConfigurationManager.AppSettings("subChannelBBVA").ToString()
            clscol.header.branchOffice = System.Configuration.ConfigurationManager.AppSettings("branchOfficeBBVA").ToString()
            clscol.header.managementUnit = System.Configuration.ConfigurationManager.AppSettings("managementUnitBBVA").ToString()
            clscol.header.user = System.Configuration.ConfigurationManager.AppSettings("userBBVA").ToString()
            clscol.header.idSession = "3232-3232"
            clscol.header.idRequest = "1212-121212-12121-212"
            clscol.header.dateConsumerInvocation = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")

            clscol.zipCode = CP

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonbody As String = serializer.Serialize(clscol)
            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("GetColony").ToString()

            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
            Dim errormessage As String
            If (restful.IsError) Then
                errormessage = restful.MensajeError
                Throw New Exception(errormessage)
            Else
                Dim res As resenvioCol = serializer.Deserialize(Of resenvioCol)(respuesta)
                If res.iCatalogItem.suburb.Count > 0 Then
                    For i As Integer = 0 To res.iCatalogItem.suburb.Count - 1
                        If Eliminar_Acentos(res.iCatalogItem.suburb(i).neighborhood.name.ToUpper.ToString) = Name.ToUpper.ToString Then
                            GetIdCol = res.iCatalogItem.suburb(i).neighborhood.id.ToString
                            Exit For
                        End If
                    Next
                    If GetIdCol = "" Then
                        'Throw New Exception("Direccion del cliente no valida")
                        GetIdCol = "99999"
                    End If
                Else
                    'Throw New Exception("Problema al guardar datos del cliente")
                    GetIdCol = "99999"
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString.Replace(vbLf, ", "))
        End Try
    End Function
    Public Function Eliminar_Acentos(ByVal accentedStr As String) As String
        Dim tempBytes As Byte()
        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr)
        Return System.Text.Encoding.UTF8.GetString(tempBytes)
    End Function
    Public Function Cotizasegautobbva(ByVal dsres As DataSet) As Boolean
        Try
            Cotizasegautobbva = False
            Dim json As New CotSegAutoBBVA
            Dim restGT As RESTful = New RESTful()
            Dim jsonResult As String
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim clsseg As New clsSeguros
            clsseg._ID_SOLICITUD = Request("solicitud").ToString
            Dim dsprod As DataSet
            Dim id_ext As String = ""
            dsprod = clsseg.getDatosBroker(-1)
            If dsprod.Tables.Count > 0 Then
                If dsprod.Tables(0).Rows.Count > 0 Then
                    id_ext = dsprod.Tables(0).Rows(0).Item("ID_EXTERNO").ToString
                Else
                    Throw New Exception("Vehiculo no apto para cotizar seguro de vida BBVA")
                End If
            Else
                Throw New Exception("Vehiculo no apto para cotizar seguro de vida BBVA")
            End If
            json.header.aapType = System.Configuration.ConfigurationManager.AppSettings("aapTypeBBVA").ToString()
            json.header.dateRequest = DateTime.Now.ToString("dd-MM-yyyy") & " 00:00:00" ' "06-08-2016 00:00:00"
            json.header.channel = System.Configuration.ConfigurationManager.AppSettings("channelBBVA").ToString()
            json.header.subChannel = System.Configuration.ConfigurationManager.AppSettings("subChannelBBVA").ToString()
            json.header.branchOffice = System.Configuration.ConfigurationManager.AppSettings("branchOfficeBBVA").ToString()
            json.header.managementUnit = IIf(dsres.Tables(0).Rows(0).Item("vehicletype").ToString = "1", System.Configuration.ConfigurationManager.AppSettings("managementUnitBBVA").ToString(), "0011") 'CAMBIO URGENTE RHERNANDEZ PARA MOTOS
            json.header.user = System.Configuration.ConfigurationManager.AppSettings("userBBVA").ToString()
            json.header.idSession = "3232-3232"
            json.header.idRequest = "1212-121212-12121-212"
            json.header.dateConsumerInvocation = DateTime.Now.ToString("dd-MM-yyyy") & " 00:00:00" ' "06-08-016 00:00:00"

            json.quote.idQuote = ""

            json.policy.validityPeriod.startDate = DateTime.Now.ToString("yyyy-MM-dd") '"2016-02-01"
            json.policy.validityPeriod.endDate = DateTime.Now.AddMonths(CInt(dsres.Tables(0).Rows(0).Item("idFinancingTerm"))).ToString("yyyy-MM-dd") '"2017-02-01"

            json.policy.preferredBeneficiary = ""
            json.policy.rcUSAIndicator = "N"
            json.policy.effectiveAdditionaldays = ""
            json.policy.invoiceValue = dsres.Tables(0).Rows(0).Item("precio").ToString.Replace(",", "")

            json.credit.creditPeriod = dsres.Tables(0).Rows(0).Item("idFinancingTerm").ToString

            json.insuranceType.catalogItemBase.id = "RL"
            json.insuranceType.catalogItemBase.name = "REGALADO (LIBRE)"

            json.usageCar.catalogItemBase.id = "6"
            json.usageCar.catalogItemBase.name = "PARTICULAR"

            json.paymentWay.catalogItemBase.id = "A"
            json.paymentWay.catalogItemBase.name = "ANUAL"

            json.productPlan.catalogItemBase.id = IIf(dsres.Tables(0).Rows(0).Item("vehicletype").ToString = "1", "005", "014") 'CAMBIO URGENTE RHERNANDEZ PARA MOTOS
            json.productPlan.productCode = "2002"
            json.productPlan.planReview = IIf(dsres.Tables(0).Rows(0).Item("vehicletype").ToString = "1", "008", "007") 'CAMBIO URGENTE RHERNANDEZ PARA MOTOS
            json.productPlan.bouquetCode = "AUAR"
            json.productPlan.subPlan = ""

            json.circulationArea.catalogItemBase.id = "001"
            json.circulationArea.catalogItemBase.name = "AGUASCALIENTES"

            json.vehicleFeatures.carModel.catalogItemBase.name = dsprod.Tables(0).Rows(0).Item("ANIO_MODELO").ToString
            json.vehicleFeatures.identifierVehicleFeatures.catalogItemBase.id = id_ext
            json.vehicleFeatures.identifierVehicleFeatures.catalogItemBase.name = dsprod.Tables(0).Rows(0).Item("DESCRIPCION2").ToString
            json.vehicleFeatures.originType.catalogItemBase.id = "N"
            json.vehicleFeatures.originType.catalogItemBase.name = "NACIONAL"

            Dim data3 As particularDataBBVA = New particularDataBBVA()
            data3.aliasCriterion = "AFSAE"
            data3.transformer.catalogItemBase.id = "001"
            data3.transformer.catalogItemBase.name = "0"
            json.particularData.Add(data3)


            json.serviceType.catalogItemBase.id = "PAR"
            json.serviceType.catalogItemBase.name = "PARTICULAR"


            Dim jsonBODY As String = serializer.Serialize(json)

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("getRateCarQuoteBBVA")
            jsonResult = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)
            If (restGT.IsError) Then
                Dim res As msjerrBBVA = serializer.Deserialize(Of msjerrBBVA)(restGT.MensajeError)
                If IsNothing(res.message) Then
                    Throw New Exception(restGT.MensajeError.Replace(vbLf, ", "))
                Else
                    Throw New Exception(res.message.ToString.Replace(vbLf, ", "))
                End If

            Else
                Dim res As jsonresponseBBVA = serializer.Deserialize(Of jsonresponseBBVA)(jsonResult)
                idquotelife.Value = res.quote.idQuote.ToString
                Cotizasegautobbva = True
            End If

        Catch ex As Exception
            Cotizasegautobbva = False
            Master.MensajeError("Error WS: " + ex.Message.ToString.Replace(vbLf, ", "))
        End Try
    End Function
    Public Function Cotizasegvidabbva(ByVal idquotedanos As String, ByVal dsres As DataSet) As Boolean
        Try
            Cotizasegvidabbva = False
            Dim json As New getratelifequoteBBVA
            Dim plazo As Integer = dsres.Tables(0).Rows(0).Item("idFinancingTerm")
            Dim monto As Double = dsres.Tables(0).Rows(0).Item("Credit")
            Dim restGT As RESTful = New RESTful()
            Dim jsonResult As String
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim fecha_inicio As String = DateTime.Now.ToString("yyyy-MM-dd")
            json.technicalInformation.dateRequest = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")
            json.technicalInformation.technicalChannel = "8"
            json.technicalInformation.technicalSubChannel = "8"
            json.technicalInformation.branchOffice = "CONSUMER FINANCE"
            json.technicalInformation.managementUnit = IIf(dsres.Tables(0).Rows(0).Item("vehicletype").ToString().Trim() = "2", "VINCF006", "VINCF005")  'RQ-PD30: 2->MOTOCICLETAS:VINCF004 autos-> VINCF003 "VINCF002"
            json.technicalInformation.user = "CARLOS"
            json.technicalInformation.technicalIdSession = "3232-3232"
            json.technicalInformation.idRequest = "1212-121212-12121-212"
            json.technicalInformation.dateConsumerInvocation = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")

            json.productPlan.productCode = "4044"
            json.productPlan.planReview = "001"
            json.productPlan.planCode.id = IIf(dsres.Tables(0).Rows(0).Item("vehicletype").ToString().Trim() = "2", "044", "041") 'RQ-PD30: 2->MOTOCICLETAS:047 autos-> 046 "041"

            json.productPlan.bouquetCode = "VGPU"

            json.validityPeriod.startDate = dsres.Tables(0).Rows(0).Item("startDate") 'BUG-PD-432 DateTime.Now.ToString("yyyy-MM-dd") '
            json.validityPeriod.endDate = dsres.Tables(0).Rows(0).Item("endDate") 'BUG-PD-432 DateTime.Now.AddMonths(plazo).ToString("yyyy-MM-dd")
            json.validityPeriod.type.id = "L"
            json.validityPeriod.type.name = "LIBRE"

            Dim ratequote As New rateQuote
            ratequote.paymentWay.id = "U"
            ratequote.paymentWay.name = "PAGO UNICO"
            json.rateQuote.Add(ratequote)

            Dim particulardata As New particularData
            particulardata.aliasCriterion = "VFPL"
            particulardata.transformer.id = plazo.ToString("D3")
            particulardata.transformer.name = plazo.ToString()
            particulardata.peopleNumber = "1"

            json.particularData.Add(particulardata)

            particulardata = New particularData
            particulardata.aliasCriterion = "VFSA"
            particulardata.transformer.id = "001"
            particulardata.transformer.name = monto.ToString()
            particulardata.peopleNumber = "1"

            json.particularData.Add(particulardata)
            Dim objFlujos As New clsSolicitudes(0)
            objFlujos.PDK_ID_SOLICITUD = Request("solicitud")

            Dim dsCuestionario = New DataSet

            dsCuestionario = objFlujos.ConsultaCuestionarios(1)


            Dim ID As String = ""
            Dim PREGUNTA As String = ""
            Dim SI As Integer = Nothing
            Dim NO As Integer = Nothing
            Dim VALOR As String = ""
            Dim RES As Integer = Nothing

            For i = 0 To dsCuestionario.Tables(0).Rows.Count - 1
                particulardata = New particularData
                ID = dsCuestionario.Tables(0).Rows(i).Item("PDK_ID_PREGUNTA").ToString
                PREGUNTA = dsCuestionario.Tables(0).Rows(i).Item("PDK_PREGUNTA").ToString
                SI = dsCuestionario.Tables(0).Rows(i).Item("SI").ToString
                NO = dsCuestionario.Tables(0).Rows(i).Item("NO").ToString
                VALOR = dsCuestionario.Tables(0).Rows(i).Item("VALOR").ToString
                RES = dsCuestionario.Tables(0).Rows(i).Item("Res").ToString

                particulardata.aliasCriterion = ID

                If ID = "CUESVFD02" Or ID = "CUESVFD01" Then
                    If ID = "CUESVFD02" Then
                        particulardata.transformer.name = VALOR.Replace(".", "")
                    Else
                        particulardata.transformer.name = VALOR

                    End If

                    particulardata.transformer.id = "001"
                Else
                    particulardata.transformer.name = ""
                    particulardata.transformer.id = IIf(SI = 0, "NO", "SI")
                End If
                particulardata.peopleNumber = "1"
                json.particularData.Add(particulardata)
            Next

            'BUG-PD-363
            particulardata = New particularData
            particulardata.aliasCriterion = "CREDVINC"
            particulardata.transformer.id = "001"
            particulardata.transformer.name = dsres.Tables(0).Rows(0).Item("numberFinancialContract")
            particulardata.peopleNumber = "1"
            json.particularData.Add(particulardata)

            'BUG-PD-371
            particulardata = New particularData
            particulardata.aliasCriterion = "TIPOVEH"
            particulardata.transformer.id = "001"
            particulardata.transformer.name = IIf(dsres.Tables(0).Rows(0).Item("vehicletype").ToString().Trim() = "2", "M", "A") 'En caso de Autos/Camiones mandar una "A" y en caso de Motos una "M"
            particulardata.peopleNumber = "1"
            json.particularData.Add(particulardata)

            Dim insuredlist As New insuredList
            Dim coverages As New coverages
            coverages.catalogItemBase.id = "0024"
            coverages.peopleNumber = "1"
            insuredlist.coverages.Add(coverages)
            json.insuredList.Add(insuredlist)
            json.region = "CF AUTO"







            Dim jsonBODY As String = serializer.Serialize(json)

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("urlBBVAV") + "/" + idquotedanos
            restGT.buscarHeader("ResponseWarningDescription")
            jsonResult = restGT.ConnectionPut(userID, iv_ticket1, jsonBODY)

            Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim alert = srrSerialer.Deserialize(Of msjerr)(jsonResult)

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    Throw New Exception(restGT.MensajeError)
                Else
                    If Not alert.message = Nothing Then
                        Throw New Exception(alert.message)
                    End If
                End If
            Else
                Cotizasegvidabbva = True
            End If
        Catch ex As Exception
            Cotizasegvidabbva = False
            Master.MensajeError("Error WS: " + ex.Message.ToString.Replace(vbLf, ", "))
        End Try
    End Function


    Protected Sub btnImpAuto_Click(sender As Object, e As EventArgs)
        Dim ruta As String = Session("RutapolDaños").ToString
        If ruta <> "" Then
            Response.Redirect("./Descargapdf.aspx?fname=" & ruta)
        Else
            Master.MensajeError("No se puede cargar poliza de vida.")
        End If
    End Sub

    Protected Sub btnImpVida_Click(sender As Object, e As EventArgs)
        Dim ruta As String = Session("RutapolVida").ToString
        If ruta <> "" Then
            Response.Redirect("./Descargapdf.aspx?fname=" & ruta)
        Else
            Master.MensajeError("No se puede cargar poliza de vida.")
        End If
    End Sub
    Public Function GuardaCuestionarioSalud(ByVal idquote As String) As Boolean
        Try
            GuardaCuestionarioSalud = False
            Dim dsCuestionario As DataSet
            Dim objFlujos As New clsSolicitudes(0)
            Dim mensaje As String
            objFlujos.PDK_ID_SOLICITUD = Request("solicitud")
            dsCuestionario = objFlujos.ConsultaCuestionarios(1)

            Dim Cuestionario As Cuestionario2 = New Cuestionario2()
            Cuestionario.header.aapType = System.Configuration.ConfigurationManager.AppSettings("aapTypeBBVA").ToString()
            Cuestionario.header.dateRequest = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
            Cuestionario.header.channel = System.Configuration.ConfigurationManager.AppSettings("channelBBVA").ToString()
            Cuestionario.header.subChannel = System.Configuration.ConfigurationManager.AppSettings("subChannelBBVA").ToString()
            Cuestionario.header.managementUnit = System.Configuration.ConfigurationManager.AppSettings("managementUnitBBVA").ToString()
            Cuestionario.header.branchOffice = System.Configuration.ConfigurationManager.AppSettings("branchOfficeBBVA").ToString()
            Cuestionario.header.user = System.Configuration.ConfigurationManager.AppSettings("userBBVA").ToString()
            Cuestionario.header.idSession = "3232-3232"
            Cuestionario.header.idRequest = "1212-121212-12121-212"
            Cuestionario.header.dateConsumerInvocation = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
            Cuestionario.quote.idQuote = idquote

            Dim ID As String = ""
            Dim PREGUNTA As String = ""
            Dim SI As Integer = Nothing
            Dim NO As Integer = Nothing
            Dim VALOR As String = ""
            Dim RES As Integer = Nothing

            For i = 0 To dsCuestionario.Tables(0).Rows.Count - 1
                ID = dsCuestionario.Tables(0).Rows(i).Item("PDK_ID_PREGUNTA").ToString
                PREGUNTA = dsCuestionario.Tables(0).Rows(i).Item("PDK_PREGUNTA").ToString
                SI = dsCuestionario.Tables(0).Rows(i).Item("SI").ToString
                NO = dsCuestionario.Tables(0).Rows(i).Item("NO").ToString
                VALOR = dsCuestionario.Tables(0).Rows(i).Item("VALOR").ToString
                RES = dsCuestionario.Tables(0).Rows(i).Item("Res").ToString

                Dim Respuestas As New questionRespuestaQ2
                Respuestas.catalogItemBase.id = ID

                If ID = "CUESVFD02" Or ID = "CUESVFD01" Then
                    If ID = "CUESVFD02" Then
                        Respuestas.answerQuestion.catalogItemBase.name = VALOR.Replace(".", "")
                    Else
                        Respuestas.answerQuestion.catalogItemBase.name = VALOR

                    End If

                    Respuestas.answerQuestion.catalogItemBase.id = "001"
                Else
                    Respuestas.answerQuestion.catalogItemBase.name = ""
                    Respuestas.answerQuestion.catalogItemBase.id = IIf(SI = 0, "NO", "SI")
                End If

                Cuestionario.questionnaire.question.Add(Respuestas)
            Next

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

            Dim jsonBODY As String = serializer.Serialize(Cuestionario)
            jsonBODY.Replace(",""name"": """, "")
            jsonBODY.Replace(", ""name"": null", "")
            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("createQuestionnaire")

            'restGT.consumerID = "10000004"
            restGT.buscarHeader("ResponseWarningDescription")
            Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

            Dim jresul2 As Respuesta = serializer.Deserialize(Of Respuesta)(jsonResult)

            Dim alert = serializer.Deserialize(Of msjerrQ2)(jsonResult)

            If restGT.IsError Then
                mensaje = (alert.message & " Estatus: " & alert.status & ".")
                Throw New Exception(mensaje)
            End If
            GuardaCuestionarioSalud = True
        Catch ex As Exception
            GuardaCuestionarioSalud = False
            Master.MensajeError(ex.Message)
        End Try
    End Function
    Public Function ValidaWS(ByVal idquote As String) As Boolean
        ValidaWS = False

        Dim Cuestionario As Cuestionario = New Cuestionario()
        Dim mensaje As String

        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()

        Cuestionario.header.aapType = System.Configuration.ConfigurationManager.AppSettings("aapTypeBBVA").ToString()
        Cuestionario.header.dateRequest = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
        Cuestionario.header.channel = System.Configuration.ConfigurationManager.AppSettings("channelBBVA").ToString()
        Cuestionario.header.subChannel = System.Configuration.ConfigurationManager.AppSettings("subChannelBBVA").ToString()
        Cuestionario.header.managementUnit = System.Configuration.ConfigurationManager.AppSettings("managementUnitBBVA").ToString()
        Cuestionario.header.branchOffice = System.Configuration.ConfigurationManager.AppSettings("branchOfficeBBVA").ToString()
        Cuestionario.header.user = System.Configuration.ConfigurationManager.AppSettings("userBBVA").ToString()
        Cuestionario.header.idSession = "3232-3232"
        Cuestionario.header.idRequest = "1212-121212-12121-212"
        Cuestionario.header.dateConsumerInvocation = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")

        Cuestionario.quote.idQuote = idquote


        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

        Dim jsonBODY As String = serializer.Serialize(Cuestionario)

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("validateQuestionnaire")

        'restGT.consumerID = "10000004"
        restGT.buscarHeader("ResponseWarningDescription")
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

        Dim jresul2 As Respuesta = serializer.Deserialize(Of Respuesta)(jsonResult)

        Dim alert = serializer.Deserialize(Of msjerr2)(jsonResult)

        If restGT.IsError Then
            mensaje = (alert.message & " Estatus: " & alert.status & ".")

            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("sol") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "');", True)
            Exit Function
        End If


        ValidaWS = True

    End Function
    Protected Sub ConsultaCapacidadPago()
        Dim dsresult As DataSet
        dsresult = BD.EjecutarQuery("EXEC getMontosCotSol " & hdSolicitud.Value & "," & 3 & "")
        If dsresult.Tables.Count > 0 Then
            If dsresult.Tables(0).Rows.Count > 0 Then
                If Convert.ToDouble(dsresult.Tables(0).Rows(0).Item("Mto_Financiar")) > 0.0 Then
                    LblCS.Text = Convert.ToDouble(dsresult.Tables(0).Rows(0).Item("Mto_Financiar")).ToString("N2")
                End If
            End If
        End If

    End Sub

    Protected Sub btnImprimir_Click(sender As Object, e As EventArgs)
        Dim Fname As String
        Dim crReportDocument As ReportDocument
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim strRuta As String = Server.MapPath("..\Reporte\DetalleCredito.rpt")
        Dim dtsDatos As New DataSet
        Dim dtsTabla As New DataTable
        Dim id_Solicitud As Integer = Request("sol")


        crReportDocument = New ReportDocument
        crReportDocument.Load(strRuta)

        Fname = Server.MapPath("Detalle_" & id_Solicitud & "_" & Format$(Now(), "dd-MM-yyyy HH-mm-ss") & ".pdf")
        Fname = Replace(Fname, "\aspx\", "\Docs\")



        Dim dsDes As DataSet
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
        objCatalogos.Parametro = Me.lblSolicitud.Text
        dsDes = objCatalogos.Catalogos(15)


        dtsDatos.Tables.Add(dsDes.Tables(0).Copy())
        dtsDatos.Tables(0).TableName = "Detalle"

        dtsDatos.Tables.Add(dsDes.Tables(1).Copy())
        dtsDatos.Tables(1).TableName = "Detalle2"

        dtsDatos.Tables.Add(dsDes.Tables(2).Copy())
        dtsDatos.Tables(2).TableName = "Detalle3"




        crReportDocument.SetParameterValue("PicturePath", "")

        System.IO.File.Delete(Fname)

        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        crDiskFileDestinationOptions.DiskFileName = Fname
        crExportOptions = crReportDocument.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat

        End With


        crReportDocument.SetDataSource(dtsDatos)

        crReportDocument.Export()
        crReportDocument.Close()
        crReportDocument.Dispose()

        Dim strLinl_1 As String = "./Descargapdf.aspx?fname="
        Dim strLink As String = HttpUtility.UrlEncode(Fname)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLinl_1 & strLink & "';", True)

        'Response.Redirect("./Descargapdf.aspx?fname=" & Fname)


    End Sub

    Protected Sub ddlTurnar_SelectedIndexChanged(sender As Object, e As EventArgs)
        If hdPantalla.Value = "89" And ddlTurnar.SelectedValue = -3 Then
            divDocumentos_opcionales.Visible = True
            Dim clsdoc As clsSeguros = New clsSeguros
            clsdoc._ID_PANTALLA = CInt(hdPantalla.Value)
            clsdoc._ID_SOLICITUD = CInt(Request("sol"))
            Dim ds As New DataSet
            ds = clsdoc.ObtenDocumentosopc()
            If clsdoc.StrError <> "" Then
                Master.MensajeError(clsdoc.StrError)
            Else
                gvDocumentos_opc.DataSource = ds
                gvDocumentos_opc.DataBind()

            End If
        Else
            divDocumentos_opcionales.Visible = False
        End If
    End Sub
    Public Function Guarda_No_procesable() As Boolean
        Guarda_No_procesable = False
        Dim cont As Integer = 0
        Dim _Chk As New CheckBox
        For i = 0 To gvDocumentos_opc.Rows.Count - 1
            If gvDocumentos_opc.Rows(i).RowType = DataControlRowType.DataRow Then
                _Chk = gvDocumentos_opc.Rows(i).Cells(2).FindControl("chkDocopc")

                If _Chk.Checked = True Then
                    cont += 1
                End If
            End If
        Next

        If cont = 0 Then
            Master.MensajeError("Debes de seleccionar al menos un documento opcional.")
            Exit Function
        Else
            Dim clsdoc As clsSeguros = New clsSeguros
            Dim id_doc As New Label
            clsdoc._ID_PANTALLA = CInt(hdPantalla.Value)
            clsdoc._ID_SOLICITUD = CInt(Request("sol"))
            For i = 0 To gvDocumentos_opc.Rows.Count - 1
                If gvDocumentos_opc.Rows(i).RowType = DataControlRowType.DataRow Then
                    _Chk = gvDocumentos_opc.Rows(i).Cells(2).FindControl("chkDocopc")
                    If _Chk.Checked = True Then
                        id_doc = gvDocumentos_opc.Rows(i).Cells(0).FindControl("lblid_doc")
                        clsdoc._ID_DOC = CInt(id_doc.Text.ToString)
                        If Not clsdoc.Inserta_No_Proc() Then
                            Master.MensajeError(clsdoc.StrError)
                            Exit Function
                        End If
                    End If
                End If
            Next
        End If
        Guarda_No_procesable = True
    End Function
End Class

Public Class msjerr2
    Public message As String
    Public status As String
End Class