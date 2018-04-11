'-- RQMSG  JBEJAR 31/05/2017   webservices sms e email automaticos.
'-- BUG-PD-65 JBEJAR 10/06/2017 Genera email y sms dinamicamente. 
'--BUG-PD-111: CGARCIA: 22/06/2017: Se implementan nuevas plantillas para los correos electronicos
'BUG-PD-122: CGARCIA: 28/06/2017: SE IMPLENETO FUNCIONALIDAD PARA EN CASO DE QUE NO SE TENGA INFO EN HERMES_RESP
'BUG-PD-133: CGARCIA: 03/07/2017: SE CAMBIO LA TAREA QUE VALIDA LA ENTRADA A FUN_CORREO_AGENCIA
'BUG-PD-170: JBEJAR:02/08/2017: SE AGREGAN DATOS DINAMICOS PARA LOS CORREOS Y SMS EN LA TAREA AUTOMATICA AL IGUAL QUE LAS CONFIGURACIONES.
'BUG-PD-203 CGARCIA: 04/09/2017 CASOS PARA LAS NOTIFICACIONES 
'BUG-PD-215: CGARCIA: 26/09/2017: ACTUALIZACION DE CORREOS PARA CASOS AL QUE ENTRA.
'BUG-PD-218: CGARCIA: 29/09/2017: ACTUALIZACION DE EMAIL
'BUG-PD-220: CGARCIA: 02/10/2017: ACTUALIZA EMAIL.
'BUG-PD-223: CGARCIA: 03/10/2017: ACTUALIZACION DE EMAIL EN SCORING
'BUG-PD-227: CGARCIA: 06/10/2017: ACTALIZA EMAIL.
'BUG-PD-232: JBEJAR:  12/10/2017: CORRECION AL MOMENTO DE RECHAZAR SOLICITUD.- CAMBIO DE PLANTILLA.   
'BUG-PD-244: CGARCIA: 20/10/2017: ACTUALIZACION A OPCION DE SCORE ACEPTADO
'RQ-PI7-PD16 JBEJAR:  23/11/2017: SE MODIFICA MENSAJE PARA CASOS NO PROCESABLES.  
'BUG-PD-276: CGARCIA: 23/11/2017: CAMBIO SOBRE TAREA 97 EN MSJ HERMES OPCION 2
'RQ-PD21: JMENDIETA: 13/02/2018: Se agrega la pantalla 185 para llamar a la funcion fn_Declinados_BN_Carruseles
Imports ProdeskNet.WCF
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlInputFile
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.SN
Imports ProdeskNet.BD
Imports System.Collections
Imports System
Partial Class aspx_ValidaEmails
    Inherits System.Web.UI.Page
#Region "Variables"
    Public BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Public strError As String = String.Empty
    Public Pantalla As Integer
    Public objpant As New ProdeskNet.SN.clsPantallas()
    Public ds_siguienteTarea As DataSet
    Public ds_anteriorTarea As DataSet
    Public solicitud As Integer
    Public valorTarea As Integer
    Public datosSol As DataSet
    Public conf As DataSet
    Public ClsEmail As New clsEmailAuto()
    Public lista As New ArrayList
    Public Mensaje As String
    Public folio As New ArrayList
    Public fecha As New ArrayList
    Dim monto As New ArrayList
    Dim enganche As New ArrayList
    Dim vigencia As New ArrayList
    Dim msj As New ArrayList

    Public mostrarPantalla As Integer
    Public usuario As Integer

    Dim objtarea As New ProdeskNet.SN.clsCatTareas()
    Dim dslink As DataSet = New DataSet()

    Public gestorEventos As New clsGestorEventos()
#End Region


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Pantalla = Convert.ToInt32(Val(Request("idPantalla")).ToString())
        mostrarPantalla = Convert.ToInt32(Val(Request("mostrarPant")).ToString())
        usuario = Convert.ToInt32(Val(Request("usuario")).ToString())
        solicitud = Request("Sol")
        BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, solicitud) ' donde esta configurada valida email
        ds_anteriorTarea = BD.EjecutaStoredProcedure("SpGetTareaAnteriorReplica") 'Obtenemos la tarea anterior  de la automatica. 
        valorTarea = ds_anteriorTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS") 'Obtenemos  valor de la tarea anterior a la automatica   
        ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & Pantalla.ToString()) 'Obtenemos tarea rechazo y no rechazo de la tarea automatica.
        ClsEmail.ID_SOLICITUD = solicitud 'Le enviamos a la clase los parametros . de solicitud 
        ClsEmail.TAREA_ANTERIOR = valorTarea ' Le enviamos a la clase los parametros de valortarea que es la tarea anterior. 
        conf = ClsEmail.GetWS 'Aqui obtenemos los datos de configuracion del pay load.
        datosSol = ClsEmail.GetEmail 'Aqui obtenemos los datos del cliente numero email compania 

        If (Not IsNothing(datosSol) AndAlso datosSol.Tables.Count > 0 AndAlso datosSol.Tables(0).Rows.Count() > 0 AndAlso
            datosSol.Tables(1).Rows.Count() > 0) Then



            If (Pantalla = 103) Then
                Fun_ComprobantesIngresosBCOM_(solicitud)
                Dim strLocation As String = String.Empty
                If (mostrarPantalla = 0) Then
                    dslink = objtarea.SiguienteTarea(Val(Request("Sol")))
                    Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString, False)
                ElseIf (mostrarPantalla = 2) Then
                    strLocation = ("../aspx/consultaPanelControl.aspx")
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                End If
            ElseIf (Pantalla = 97) Then 'tarea de scoring

                Dim dtConsulta As New DataSet()
                Dim intStatusProceso As Integer
                Dim intProcesable As Integer

                ClsEmail.OPCION = 16
                dtConsulta = ClsEmail.Consultasolicitud
                'intStatusProceso = 1 APROBADA O ACEPTADA
                'intStatusProceso = 0 RECHAZADA
                intStatusProceso = CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString)


                If (intStatusProceso = 1) Then
                    fn_Aprobados(solicitud)
                    Dim intTipoSMS As Integer
                    intTipoSMS = CInt(datosSol.Tables(0).Rows(0).Item("TIPO_SMS").ToString)
                    Select Case intTipoSMS
                        Case 1
                            Fun_AprobadoSMS_Honda_Acura(solicitud)
                        Case 2
                            Fun_AprobadoSMS_Suzuki(solicitud)
                        Case 3
                            Fun_AprobadoSMS_Motos(solicitud)
                        Case 4
                            Fun_AprobadoSMS_(solicitud)
                    End Select
                    Dim strLocation As String = String.Empty
                    If (mostrarPantalla = 0) Then
                        dslink = objtarea.SiguienteTarea(Val(Request("Sol")))
                        Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString, False)
                    ElseIf (mostrarPantalla = 2) Then
                        strLocation = ("../aspx/consultaPanelControl.aspx")
                        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                    End If

                ElseIf (intStatusProceso = 2) Then
                    'BUG-PD-276: CGARCIA: 23/11/2017: CAMBIO SOBRE TAREA 97 EN MSJ HERMES OPCION 2
                    If (mostrarPantalla = 0) Then
                        dslink = objtarea.SiguienteTarea(Val(Request("Sol")))
                        Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString, False)
                    ElseIf (mostrarPantalla = 2) Then
                        Dim strLocation As String = String.Empty
                        strLocation = ("../aspx/consultaPanelControl.aspx")
                        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                    End If

                Else
                    'PROCESO EN 40 O EN 42
                    intProcesable = CInt(dtConsulta.Tables(0).Rows(0).Item("STATUS_PROCESO").ToString)
                    'If (intProcesable = 42) Then
                    '    Dim strLocation As String = String.Empty
                    '    strLocation = ("../aspx/consultaPanelControl.aspx")
                    '    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                    'Else
                    fn_Rechazo_Modelo(solicitud)
                    Dim strLocation As String = String.Empty
                    strLocation = ("../aspx/consultaPanelControl.aspx")
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                    'End If
                End If
                'RQ-PD21 pantalla 185
            ElseIf (Pantalla = 69 Or Pantalla = 122 Or Pantalla = 70 Or Pantalla = 83 Or Pantalla = 86 Or Pantalla = 87 Or Pantalla = 112 Or Pantalla = 124 Or Pantalla = 185) Then
                fn_Declinados_BN_Carruseles(solicitud)

                Dim strLocation As String = String.Empty
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)

            ElseIf (Pantalla = 92) Then
                fn_Domiciliado(solicitud)
                Dim strLocation As String = String.Empty
                If (mostrarPantalla = 0) Then
                    dslink = objtarea.SiguienteTarea(Val(Request("Sol")))
                    Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString, False)
                ElseIf (mostrarPantalla = 2) Then
                    strLocation = ("../aspx/consultaPanelControl.aspx")
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                End If
            ElseIf (Pantalla = 74 Or Pantalla = 80 Or Pantalla = 105 Or Pantalla = 106 Or Pantalla = 89) Then
                fn_No_Procesable(solicitud)
                Dim strLocation As String = String.Empty
                If (mostrarPantalla = 0) Then
                    dslink = objtarea.SiguienteTarea(Val(Request("Sol")))
                    Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString, False)
                ElseIf (mostrarPantalla = 2) Then
                    strLocation = ("../aspx/consultaPanelControl.aspx")
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                End If
            End If
        Else
            Dim strLocation As String = String.Empty
            If (mostrarPantalla = 0) Then
                dslink = objtarea.SiguienteTarea(Val(Request("Sol")))
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")))
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                'Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString, False)
            ElseIf (mostrarPantalla = 2) Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            End If
            Exit Sub
        End If
    End Sub

    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Try
            'SE LLENAN LOS PARAMETROS PARA EJECUTAR METODOS DE LA CLASE clsSolicitudes
            Solicitudes.BOTON = 64                                  'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_SOLICITUD = Request("sol")      'PARAMETRO PARA ValNegocio() Y ManejaTarea()
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")          'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_PANTALLA = Pantalla    'PARAMETRO PARA ManejaTarea()
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla    'PARAMETRO PARA ManejaTarea()

            dsresult = Solicitudes.ValNegocio(1)                    'SE ADELANTA LA TAREA ESPECIFICADA(RECHAZO O NORECHAZO) Y OBTENEMOS MENSAJE
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje  ' SE ASIGNA MENSAJE COMO PARAMETRO PARA ManejaTarea()
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                Response.Redirect("../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If

            dslink = objtarea.SiguienteTarea(Solicitudes.PDK_ID_SOLICITUD)

            muestrapant = objpantalla.SiguientePantalla(Solicitudes.PDK_ID_SOLICITUD)

            Dim strLocation As String = String.Empty

            If muestrapant = 0 Then
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)

            ElseIf muestrapant = 2 Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub

    Private Sub Fun_AprobadoBCOM_(ByVal idSolicitud As Integer)

        Dim Email As String = String.Empty
        Dim Tel As String = String.Empty
        Dim Compania As String = String.Empty

        Dim nom As String = "Aprobado" & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next

        Dim folio_C As String
        folio_C = datosSol.Tables(0).Rows(0).Item("FOLIO").ToString 'preguntar de donde se obtendra el folio
        Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
        Dim longitud_F As Integer
        longitud_F = Len(folio_S) 'longitud de caracteres 
        For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud_F > valor1 Then
                folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        Dim monto1 As String = datosSol.Tables(0).Rows(0).Item("MONTO").ToString  'Monto de la solicitud preguntar de donde se obtendra el dato.
        Dim monto1_S As Char() = monto1.ToCharArray()
        Dim longitud_monto As Integer = Len(monto1_S) 'longitud de la fecha.
        For valor3 As Integer = 0 To 12 Step +1
            If longitud_monto > valor3 Then
                monto.Add(monto1_S(valor3)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                monto.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        Dim mes As String = datosSol.Tables(0).Rows(0).Item("VALOR_PLAZO").ToString 'meses del credito   preguntar donde se obtiene esta informacion
        Dim engan As String = datosSol.Tables(0).Rows(0).Item("PTJ_ENGANCHE").ToString 'enganche del auto.  preguntar el enganche del auto
        Dim pagoMen As String = datosSol.Tables(0).Rows(0).Item("PAGO_PERIODO").ToString 'pago mensual.  del auto.
        Dim union As String = mes & engan & pagoMen 'Unimos los campos como lo requerimos .
        Dim union_S As Char() = union.ToCharArray()
        Dim longitud_union As Integer = Len(union_S) 'Longitud de la union de campos. 
        For valor4 As Integer = 0 To 19 Step +1
            If longitud_union > valor4 Then
                enganche.Add(union_S(valor4)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                enganche.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        Dim Numero As String = "5553029050" 'numero de bancomer. 
        Dim cat As String = datosSol.Tables(0).Rows(0).Item("CAT").ToString 'cat  de la solicitud. 
        If cat = 0 Then
            Master.MensajeError("El cat es 0 Favor de Recalcularlo.")
            asignaTarea(valorTarea)
        End If
        Dim tasa As String = datosSol.Tables(0).Rows(0).Item("TASA_INTERES").ToString 'tasa fija  de la solicitud   para autos nuevos. 
        Dim fechavi1 As String = Format(Date.Now(), "ddMMyyyy")
        Dim fechavi2 As String = Format(Date.Now().AddYears(1), "ddMMyyyy") ' sumamos un año  a la fecha actual.
        Dim fechacal As String = Format(Date.Now(), "ddMMyyyy") ' fecha del calculo .  
        Dim datos_des As String = "30.9920.80"  'Se desconoce el origen de estos datos.  
        Dim unionF As String = Numero & cat & tasa & fechavi1 & fechavi2 & fechacal & datos_des & fechavi1 & fechavi2 & fechacal 'union de los campos requeridos 
        Dim unionF_S As Char() = unionF.ToCharArray() 'arreglo de char     
        Dim longitud_unionF As Integer = Len(unionF_S) 'Longitud de la union  
        For valor5 As Integer = 0 To 77 Step +1
            If longitud_unionF > valor5 Then
                vigencia.Add(unionF_S(valor5))  'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                vigencia.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim folio_Cadena As String = String.Join("", folio.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())
        Dim monto_Cadena As String = String.Join("", monto.ToArray())
        Dim enganche_Cadena As String = String.Join("", enganche.ToArray())
        Dim vigencia_Cadena As String = String.Join("", vigencia.ToArray())
        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena & monto_Cadena & enganche_Cadena & vigencia_Cadena 'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000000978" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "KZTW0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 
        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                End If
            Else
                Fun_AprobadoBCOM_Prueba(idSolicitud, Mensaje)
                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If
    End Sub

    Private Sub Fun_AutentificacionBCOM_(ByVal idSolicitud As Integer)
        Dim gestorSMS As New clsaproSMS
        Dim fechasms As String = Format(Date.Now(), "MMM-yyyy-dd H:mm")

        gestorSMS.dtoReceiver.smsReceiver.company = "MOVISTAR" 'Datos hardcore de prueba. la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("COMPANY_MOVIL")
        gestorSMS.dtoReceiver.smsReceiver.phoneNumber = "5520593771" 'Datos hardcore de prueba.  la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("TELEFONO_MOVIL")
        gestorSMS.eventCode = "0000000985" 'Event code recuperado de la tabla para mensajes 
        gestorSMS.message = fechasms & " " & "Para continuar con el tramite de tu credito automotriz llama al 5553029050 y proporciona el codigo 123456 al asesor telefonico BBVA." 'recuperado de la tabla para mensajes 
        gestorSMS.messageTemplate = "KZ2C0101" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverReference = "D0075225" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverType = "informado" 'recuperado de la tabla para mensajes 

        Dim serializer1 As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody1 As String = serializer1.Serialize(gestorSMS) 'Se serializa el contenido de la clase gestorSMS  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) 'user id 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket para ocupar los servicios 
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") ' recuperado del web.config. uri
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody1) 'respuesta del webservice 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody1

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                End If
            Else
                Fun_AutentificacionBCOM_Prueba(idSolicitud)
                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If

    End Sub

    Private Sub Fun_NP_Autentificacion_VozBCOM_(ByVal idSolicitud As Integer)

        Dim Email As String = String.Empty
        Dim Tel As String = String.Empty
        Dim Compania As String = String.Empty


        Dim nom As String = datosSol.Tables(0).Rows(0).Item("FOLIO").ToString & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next

        Dim folio_C As String
        folio_C = datosSol.Tables(0).Rows(0).Item("FOLIO").ToString 'preguntar de donde se obtendra el folio
        Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
        Dim longitud_F As Integer
        longitud_F = Len(folio_S) 'longitud de caracteres 
        For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud_F > valor1 Then
                folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        Dim msj1 As String = "Datos Erroneos de Solicitud"  'Monto de la solicitud preguntar de donde se obtendra el dato.
        Dim msj1_S As Char() = msj1.ToCharArray()
        Dim longitud_monto As Integer = Len(msj1_S) 'longitud de la fecha.
        For valor3 As Integer = 0 To 199 Step +1
            If longitud_monto > valor3 Then
                msj.Add(msj1_S(valor3)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                msj.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        Dim Numero As String = "5553029080" 'numero de bancomer. 

        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim folio_Cadena As String = String.Join("", folio.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())
        Dim monto_Cadena As String = String.Join("", msj.ToArray())

        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena & monto_Cadena & Numero 'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000000975" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "KZTS0101" 'se recupera del store."
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                End If
            Else
                Fun_NP_Autentificacion_VozBCOM_Prueba(idSolicitud, Mensaje)
                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If
    End Sub
    Private Sub Fun_NP_BCOM_(ByVal idSolicitud As Integer)

        Dim Email As String = String.Empty
        Dim Tel As String = String.Empty
        Dim Compania As String = String.Empty

        If datosSol.Tables(0).Rows.Count > 0 Then
            Dim nom As String = datosSol.Tables(0).Rows(0).Item("FOLIO").ToString & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
            'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
            Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
            Dim longitud As Integer
            longitud = Len(caracteres) 'longitud de caracteres 
            For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
                If longitud > valor Then
                    lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
                Else
                    lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
                End If
            Next

            Dim folio_C As String
            folio_C = datosSol.Tables(0).Rows(0).Item("FOLIO").ToString 'preguntar de donde se obtendra el folio
            Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
            Dim longitud_F As Integer
            longitud_F = Len(folio_S) 'longitud de caracteres 
            For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
                If longitud_F > valor1 Then
                    folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
                Else
                    folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
                End If
            Next

            Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
            Dim date1_S As Char() = date1.ToCharArray()
            Dim longitud_Fecha As Integer
            longitud_Fecha = Len(date1_S) 'longitud de  fecha 
            For valor2 As Integer = 0 To 119 Step +1
                If longitud_Fecha > valor2 Then
                    fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
                Else
                    fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
                End If
            Next

            Dim msj1 As String = "Datos Erroneos de Solicitud"  'Monto de la solicitud preguntar de donde se obtendra el dato.
            Dim msj1_S As Char() = msj1.ToCharArray()
            Dim longitud_monto As Integer = Len(msj1_S) 'longitud de la fecha.
            For valor3 As Integer = 0 To 199 Step +1
                If longitud_monto > valor3 Then
                    msj.Add(msj1_S(valor3)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
                Else
                    msj.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
                End If
            Next

            Dim Numero As String = "5553029080" 'numero de bancomer. 

            'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
            Dim lista_Cadena As String = String.Join("", lista.ToArray())
            Dim folio_Cadena As String = String.Join("", folio.ToArray())
            Dim fecha_Cadena As String = String.Join("", fecha.ToArray())
            Dim monto_Cadena As String = String.Join("", msj.ToArray())

            'Terminamos conversion de array a cadenas.
            Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena & monto_Cadena & Numero 'Formamos mensaje como es requerido por el WS posicional de mal.
            gestorEventos.dtoReceiver.emailReceiver.address = datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
            'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
            gestorEventos.eventCode = "0000000975" 'EVENT CODE 
            gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
            gestorEventos.messageTemplate = "KZTS0101" 'se recupera del store."
            gestorEventos.receiverReference = "D0075225" ' se recupera el store.
            gestorEventos.receiverType = "informado"  'Se recupera del store. 

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
            Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
            Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

            Dim objRespuesta As New clsGuardarRespuesta()
            objRespuesta.ID_SOLICITUD = solicitud
            objRespuesta.JSON = jsonbody
            If restGT.MensajeError <> "" Then

                objRespuesta.RESPUESTA = restGT.MensajeError
            Else
                objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
            End If

            If objRespuesta.insertaDatosRespuesta() Then

                If restGT.IsError Then
                    If restGT.MensajeError <> "" Then
                        asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                    End If
                Else
                    Fun_NP_BCOM_Prueba(idSolicitud, Mensaje)
                    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
                End If
            End If
        End If

    End Sub
    Private Sub Fun_NP_ID_NoLegibleBCOM_(ByVal idSolictus As Integer)

        Dim Email As String = String.Empty
        Dim Tel As String = String.Empty
        Dim Compania As String = String.Empty

        If datosSol.Tables(0).Rows.Count > 0 Then
            Dim nom As String = datosSol.Tables(0).Rows(0).Item("FOLIO").ToString & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
            'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
            Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
            Dim longitud As Integer
            longitud = Len(caracteres) 'longitud de caracteres 
            For valor As Integer = 0 To 123 Step +1 'for para recorrer 123 veces el tamaño defenido para el asunto 
                If longitud > valor Then
                    lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
                Else
                    lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
                End If
            Next

            Dim folio_C As String

            folio_C = datosSol.Tables(0).Rows(0).Item("FOLIO").ToString  'preguntar de donde se obtendra el folio
            Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
            Dim longitud_F As Integer
            longitud_F = Len(folio_S) 'longitud de caracteres 
            For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
                If longitud_F > valor1 Then
                    folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
                Else
                    folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
                End If
            Next
            Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm").ToString & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
            Dim date1_S As Char() = date1.ToCharArray()
            Dim longitud_Fecha As Integer
            longitud_Fecha = Len(date1_S) 'longitud de  fecha 
            For valor2 As Integer = 0 To 115 Step +1
                If longitud_Fecha > valor2 Then
                    fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
                Else
                    fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
                End If
            Next

            'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
            Dim lista_Cadena As String = String.Join("", lista.ToArray())
            Dim folio_Cadena As String = String.Join("", folio.ToArray())
            Dim fecha_Cadena As String = String.Join("", fecha.ToArray())

            'Terminamos conversion de array a cadenas.
            Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena 'Formamos mensaje como es requerido por el WS posicional de mal.
            gestorEventos.dtoReceiver.emailReceiver.address = datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
            'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
            gestorEventos.eventCode = "0000000973" 'EVENT CODE 
            gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
            gestorEventos.messageTemplate = "KZTP0101" 'se recupera del store.
            gestorEventos.receiverReference = "D0075225" ' se recupera el store.
            gestorEventos.receiverType = "informado"  'Se recupera del store. 

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
            Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
            Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

            Dim objRespuesta As New clsGuardarRespuesta()
            objRespuesta.ID_SOLICITUD = solicitud
            objRespuesta.JSON = jsonbody
            If restGT.MensajeError <> "" Then

                objRespuesta.RESPUESTA = restGT.MensajeError
            Else
                objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
            End If

            If objRespuesta.insertaDatosRespuesta() Then

                If restGT.IsError Then
                    If restGT.MensajeError <> "" Then
                        asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                    End If
                Else
                    Fun_NP_ID_NoLegibleBCOM_Prueba(solicitud)
                    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
                End If

            End If
        End If


    End Sub
    Private Sub Fun_NP_ID_NoValidaBCOM_(ByVal idSolicitud As Integer)
        Dim Email As String = String.Empty
        Dim Tel As String = String.Empty
        Dim Compania As String = String.Empty


        Dim nom As String = datosSol.Tables(0).Rows(0).Item("FOLIO").ToString & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next

        Dim folio_C As String
        folio_C = datosSol.Tables(0).Rows(0).Item("FOLIO").ToString 'preguntar de donde se obtendra el folio
        Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
        Dim longitud_F As Integer
        longitud_F = Len(folio_S) 'longitud de caracteres 
        For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud_F > valor1 Then
                folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim folio_Cadena As String = String.Join("", folio.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())

        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena 'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000000974" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "KZTQ0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody
        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                End If
            Else
                Fun_NP_ID_NoValidaBCOM_Prueba(idSolicitud)
                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If

        End If
    End Sub
    Private Sub Fun_RechazoBCOM_(ByVal idSolicitud As Integer)
        Dim msj As New ArrayList
        Dim Email As String = String.Empty
        Dim Tel As String = String.Empty
        Dim Compania As String = String.Empty


        Dim nom As String = "RECHAZO" & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next

        Dim folio_C As String
        folio_C = datosSol.Tables(0).Rows(0).Item("FOLIO").ToString 'preguntar de donde se obtendra el folio
        Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
        Dim longitud_F As Integer
        longitud_F = Len(folio_S) 'longitud de caracteres 
        For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud_F > valor1 Then
                folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        Dim Numero As String = "5553029080" 'numero de bancomer. 

        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim folio_Cadena As String = String.Join("", folio.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())

        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena & Numero 'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = "jbejar@telepro.com.mx" ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000000977" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "KZTV0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If objRespuesta.insertaDatosRespuesta() Then


            If restGT.MensajeError <> "" Then

                objRespuesta.RESPUESTA = restGT.MensajeError
            Else
                objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
            End If

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then

                    asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                End If
            Else
                Fun_RechazoBCOM_Prueba(idSolicitud, Mensaje)
                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If
    End Sub
    Private Sub Fun_ValidacionTelefonica_(ByVal idSolicitud As Integer)
        Dim Email As String = String.Empty
        Dim Tel As String = String.Empty
        Dim Compania As String = String.Empty


        Dim nom As String = "Validacion Telefonica" & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next

        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())

        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & fecha_Cadena 'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = "jbejar@telepro.com.mx" ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000000983" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "KZTZ0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 
        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If objRespuesta.insertaDatosRespuesta() Then


            If restGT.MensajeError <> "" Then

                objRespuesta.RESPUESTA = restGT.MensajeError
            Else
                objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
            End If

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then

                    asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                End If
            Else
                Fun_ValidacionTelefonica_Prueba(idSolicitud, Mensaje)
                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If
    End Sub
    Private Sub Fun_NP_ID_NoLegibleBCOM_Prueba(ByVal idSolictud As Integer)

        Dim Email As String = String.Empty
        Dim Tel As String = String.Empty
        Dim Compania As String = String.Empty

        If datosSol.Tables(0).Rows.Count > 0 Then


            'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
            Dim lista_Cadena As String = String.Join("", lista.ToArray())
            Dim folio_Cadena As String = String.Join("", folio.ToArray())
            Dim fecha_Cadena As String = String.Join("", fecha.ToArray())

            'Terminamos conversion de array a cadenas.
            Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena 'Formamos mensaje como es requerido por el WS posicional de mal.
            gestorEventos.dtoReceiver.emailReceiver.address = ConfigurationManager.AppSettings("CorreoPrueba").ToString ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
            'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
            gestorEventos.eventCode = "0000000973" 'EVENT CODE 
            gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
            gestorEventos.messageTemplate = "KZTP0101" 'se recupera del store.
            gestorEventos.receiverReference = "D0075225" ' se recupera el store.
            gestorEventos.receiverType = "informado"  'Se recupera del store. 

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
            Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
            Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

            Dim objRespuesta As New clsGuardarRespuesta()
            objRespuesta.ID_SOLICITUD = solicitud
            objRespuesta.JSON = jsonbody
            If restGT.MensajeError <> "" Then

                objRespuesta.RESPUESTA = restGT.MensajeError & " " & "Prueba"
            Else
                objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente" & " " & "Prueba"
            End If

            objRespuesta.insertaDatosRespuesta()

        End If
    End Sub
    Private Sub Fun_NP_ID_NoValidaBCOM_Prueba(ByVal idSolicitud As Integer)
        Dim Email As String = String.Empty
        Dim Tel As String = String.Empty
        Dim Compania As String = String.Empty

        If datosSol.Tables(0).Rows.Count > 0 Then
            'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
            Dim lista_Cadena As String = String.Join("", lista.ToArray())
            Dim folio_Cadena As String = String.Join("", folio.ToArray())
            Dim fecha_Cadena As String = String.Join("", fecha.ToArray())

            'Terminamos conversion de array a cadenas.
            Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena 'Formamos mensaje como es requerido por el WS posicional de mal.
            gestorEventos.dtoReceiver.emailReceiver.address = ConfigurationManager.AppSettings("CorreoPrueba").ToString  ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
            'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
            gestorEventos.eventCode = "0000000974" 'EVENT CODE 
            gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
            gestorEventos.messageTemplate = "KZTQ0101" 'se recupera del store.
            gestorEventos.receiverReference = "D0075225" ' se recupera el store.
            gestorEventos.receiverType = "informado"  'Se recupera del store. 

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
            Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
            Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 
            Dim objRespuesta As New clsGuardarRespuesta()
            objRespuesta.ID_SOLICITUD = solicitud
            objRespuesta.JSON = jsonbody
            If restGT.MensajeError <> "" Then

                objRespuesta.RESPUESTA = restGT.MensajeError & " " & "Prueba"
            Else
                objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente" & " " & "Prueba"
            End If

            objRespuesta.insertaDatosRespuesta()
        End If

    End Sub
    Private Sub Fun_NP_BCOM_Prueba(ByVal idSolicitud As Integer, ByVal Mensaje As String)

        Dim mensaje1 As String = Mensaje

        'Terminamos conversion de array a cadenas.
        Mensaje = mensaje1   'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = ConfigurationManager.AppSettings("CorreoPrueba").ToString ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000000975" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "KZTS0101" 'se recupera del store."
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody
        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError & " " & "Prueba"
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente" & " " & "Prueba"
        End If

        objRespuesta.insertaDatosRespuesta()

    End Sub
    Private Sub Fun_RechazoBCOM_Prueba(ByVal idSolicitud As Integer, ByVal Mensaje As String)

        Dim mensaje1 As String = Mensaje
        'Terminamos conversion de array a cadenas.
        Mensaje = mensaje1  'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = ConfigurationManager.AppSettings("CorreoPrueba").ToString ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000000977" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "KZTV0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody
        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError & " " & "Prueba"
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente" & " " & "Prueba"
        End If

        objRespuesta.insertaDatosRespuesta()

    End Sub
    Private Sub Fun_AprobadoBCOM_Prueba(ByVal idSolicitud As Integer, ByVal Mensaje As String)

        Dim mensaje1 As String = Mensaje
        'Terminamos conversion de array a cadenas.
        Mensaje = mensaje1   'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = ConfigurationManager.AppSettings("CorreoPrueba").ToString ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000000978" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "KZTW0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 
        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError & " " & "Prueba"
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente" & " " & "Prueba"
        End If

        objRespuesta.insertaDatosRespuesta()

    End Sub
    Private Sub Fun_NP_Autentificacion_VozBCOM_Prueba(ByVal idSolicitud As Integer, ByVal Mensaje As String)

        Dim mensaje1 As String = Mensaje

        Mensaje = mensaje1 'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = ConfigurationManager.AppSettings("CorreoPrueba").ToString  ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000000975" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "KZTS0101" 'se recupera del store."
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError & "Prueba"
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente" & "Prueba"
        End If

        objRespuesta.insertaDatosRespuesta()
    End Sub
    Private Sub Fun_ValidacionTelefonica_Prueba(ByVal idSolicitud As Integer, ByVal Mensaje As String)

        Dim mensaje1 As String = Mensaje

        Mensaje = mensaje1 'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = ConfigurationManager.AppSettings("CorreoPrueba").ToString  ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000000983" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "KZTZ0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError & "Prueba"
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente" & "Prueba"
        End If

        objRespuesta.insertaDatosRespuesta()
    End Sub
    Private Sub Fun_AutentificacionBCOM_Prueba(ByVal idSolicitud As Integer)
        Dim gestorSMS As New clsaproSMS
        Dim fechasms As String = Format(Date.Now(), "MMM-yyyy-dd H:mm")
        Dim mensaje1 As String = Mensaje

        Mensaje = mensaje1 'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorSMS.dtoReceiver.smsReceiver.company = ConfigurationManager.AppSettings("COMPANIA").ToString  'Datos hardcore de prueba. la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("COMPANY_MOVIL")
        gestorSMS.dtoReceiver.smsReceiver.phoneNumber = ConfigurationManager.AppSettings("CELULAR").ToString 'Datos hardcore de prueba.  la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("TELEFONO_MOVIL") 5561227876 numero rene Telcel 
        gestorSMS.eventCode = "0000000985" 'Event code recuperado de la tabla para mensajes 
        gestorSMS.message = fechasms & " " & "Para continuar con el tramite de tu credito automotriz llama al 5553029050 y proporciona el codigo 123456 al asesor telefonico BBVA." 'recuperado de la tabla para mensajes 
        gestorSMS.messageTemplate = "KZ2C0101" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverReference = "D0075225" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverType = "informado"
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody1 As String = serializer.Serialize(gestorSMS) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody1) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody1

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError & "Prueba"
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente" & "Prueba"
        End If

        objRespuesta.insertaDatosRespuesta()
    End Sub


    ''' <summary>
    ''' APROBADOS POS-02 (Correo Agencia)
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    Private Sub fn_Aprobados(ByVal idSolicitud As Integer)
        Dim msj1 As New ArrayList
        Dim msj2 As New ArrayList

        Dim nom As String = "Estimado(a):" & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next
        Dim folio_C As String
        folio_C = CStr(solicitud) '"10000006789" 'preguntar de donde se obtendra el folio
        Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
        Dim longitud_F As Integer
        longitud_F = Len(folio_S) 'longitud de caracteres 
        For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud_F > valor1 Then
                folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If

        Next
        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj1 As DataSet
        'dtMsj1 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        'If dtMsj1.Tables(0).Rows.Count = 0 Then
        '    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
        '    Exit Sub
        'End If
        Dim msj1_1 As String = "APROBADO" 'dtMsj1.Tables(0).Rows(0).Item("finalDictum") '"Solicitud rechazada por capacidad de pago, rechazo por buro."  'Monto de la solicitud preguntar de donde se obtendra el dato.
        Dim msj1_1_S As Char() = msj1_1.ToCharArray()
        Dim longitud_msj1 As Integer = Len(msj1_1_S) 'longitud de la fecha.
        For valor3 As Integer = 0 To 199 Step +1
            If longitud_msj1 > valor3 Then
                msj1.Add(msj1_1_S(valor3)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                msj1.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj2 As DataSet
        'dtMsj2 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        Dim msj2_2 As String = "Por: $" & datosSol.Tables(0).Rows(0).Item("MONTO").ToString & " " & "en moneda nacional a" & " " & datosSol.Tables(0).Rows(0).Item("VALOR_PLAZO".ToString) & " " &
            "meses y con un enganche del" & " " & datosSol.Tables(0).Rows(0).Item("PTJ_ENGANCHE").ToString & "%" 'dtMsj2.Tables(0).Rows(0).Item("finalDictum") '"Se rechaza solicitud por capacidad en buro de credito, motor de capacidad se puede ofrecer una oferta menor"
        Dim msj2_2_S As Char() = msj2_2.ToCharArray()
        Dim longitud_msj2 As Integer = Len(msj2_2_S)
        For valor4 As Integer = 0 To 299 Step +1
            If longitud_msj2 > valor4 Then
                msj2.Add(msj2_2_S(valor4))
            Else
                msj2.Add(" ")
            End If
        Next

        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim folio_Cadena As String = String.Join("", folio.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())
        Dim msj1_Cadena As String = String.Join("", msj1.ToArray())
        Dim msj2_Cadena As String = String.Join("", msj2.ToArray())

        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena & msj1_Cadena & msj2_Cadena
        'Formamos mensaje como es requerido por el WS posicional de mal.
        Dim strEmail As String
        If (Not IsNothing(datosSol) AndAlso datosSol.Tables.Count > 0 AndAlso datosSol.Tables(1).Rows.Count() > 0 AndAlso datosSol.Tables(1).Rows(0).Item("EMAIL_ENVIO") <> String.Empty) Then
            strEmail = datosSol.Tables(1).Rows(0).Item("EMAIL_ENVIO")
        Else
            Dim strLocation As String = String.Empty
            If (mostrarPantalla = 0) Then
                dslink = objtarea.SiguienteTarea(Val(Request("Sol")))
                Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString, False)
                Exit Sub
            ElseIf (mostrarPantalla = 2) Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                Exit Sub
            End If
        End If
        gestorEventos.dtoReceiver.emailReceiver.address = strEmail.ToString() '"g.martinez.quiroz@accenture.com" ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000001093" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "AGAU0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    'asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                    Exit Sub
                End If
            Else
                fn_Replica_envio(idSolicitud, Mensaje)
                'asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If

    End Sub

    ''' <summary>
    ''' APROBADOS POS-02 REPLICA (Correo Agencia)
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    ''' <param name="Mensaje"></param>
    Private Sub fn_Replica_envio(ByVal idSolicitud As Integer, ByVal Mensaje As String)

        Dim mensaje1 As String = Mensaje

        Mensaje = mensaje1 'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = ConfigurationManager.AppSettings("CorreoPrueba").ToString  ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000001093" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "AGAU0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError & "Prueba"
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente" & "Prueba"
        End If

        objRespuesta.insertaDatosRespuesta()
    End Sub

    ''' <summary>
    ''' DECLANDOS POR BASES NEGATIVAS O CARRUSELES POS-03 (Correo Agencia) 
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    Private Sub fn_Declinados_BN_Carruseles(ByVal idSolicitud As Integer)
        Dim msj1 As New ArrayList
        Dim msj2 As New ArrayList

        Dim nom As String = "Estimado(a):" & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next
        Dim folio_C As String
        folio_C = CStr(solicitud) '"10000006789" 'preguntar de donde se obtendra el folio
        Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
        Dim longitud_F As Integer
        longitud_F = Len(folio_S) 'longitud de caracteres 
        For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud_F > valor1 Then
                folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If

        Next
        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj1 As DataSet
        'dtMsj1 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        'If dtMsj1.Tables(0).Rows.Count = 0 Then
        '    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
        '    Exit Sub
        'End If
        Dim msj1_1 As String = "Declinado" 'dtMsj1.Tables(0).Rows(0).Item("finalDictum") '"Solicitud rechazada por capacidad de pago, rechazo por buro."  'Monto de la solicitud preguntar de donde se obtendra el dato.
        Dim msj1_1_S As Char() = msj1_1.ToCharArray()
        Dim longitud_msj1 As Integer = Len(msj1_1_S) 'longitud de la fecha.
        For valor3 As Integer = 0 To 199 Step +1
            If longitud_msj1 > valor3 Then
                msj1.Add(msj1_1_S(valor3)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                msj1.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj2 As DataSet
        'dtMsj2 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        Dim msj2_2 As String = "Incongruencia de datos." 'dtMsj2.Tables(0).Rows(0).Item("finalDictum") '"Se rechaza solicitud por capacidad en buro de credito, motor de capacidad se puede ofrecer una oferta menor"
        Dim msj2_2_S As Char() = msj2_2.ToCharArray()
        Dim longitud_msj2 As Integer = Len(msj2_2_S)
        For valor4 As Integer = 0 To 299 Step +1
            If longitud_msj2 > valor4 Then
                msj2.Add(msj2_2_S(valor4))
            Else
                msj2.Add(" ")
            End If
        Next

        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim folio_Cadena As String = String.Join("", folio.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())
        Dim msj1_Cadena As String = String.Join("", msj1.ToArray())
        Dim msj2_Cadena As String = String.Join("", msj2.ToArray())

        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena & msj1_Cadena & msj2_Cadena 'Formamos mensaje como es requerido por el WS posicional de mal.
        Dim strEmail As String
        If (Not IsNothing(datosSol) AndAlso datosSol.Tables.Count > 0 AndAlso datosSol.Tables(1).Rows.Count() > 0 AndAlso datosSol.Tables(1).Rows(0).Item("EMAIL_ENVIO") <> String.Empty) Then
            strEmail = datosSol.Tables(1).Rows(0).Item("EMAIL_ENVIO")
        Else
            Exit Sub
        End If
        gestorEventos.dtoReceiver.emailReceiver.address = strEmail.ToString() '"g.martinez.quiroz@accenture.com" ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000001093" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "AGAU0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    'asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                    Exit Sub
                End If
            Else
                fn_Replica_envio(idSolicitud, Mensaje)
                'asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO")) ' si  no a tarea  rechazo  
            End If
        End If

    End Sub

    ''' <summary>
    ''' MENSAJES LIGUE DE CUENTA POS-05 (Correo Agencia)
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    Private Sub fn_Domiciliado(ByVal idSolicitud As Integer)
        Dim msj1 As New ArrayList
        Dim msj2 As New ArrayList

        Dim nom As String = "Estimado(a):" & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next
        Dim folio_C As String
        folio_C = CStr(solicitud) '"10000006789" 'preguntar de donde se obtendra el folio
        Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
        Dim longitud_F As Integer
        longitud_F = Len(folio_S) 'longitud de caracteres 
        For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud_F > valor1 Then
                folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If

        Next
        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj1 As DataSet
        'dtMsj1 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        'If dtMsj1.Tables(0).Rows.Count = 0 Then
        '    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
        '    Exit Sub
        'End If
        Dim msj1_1 As String = "Cuenta Ligada" 'dtMsj1.Tables(0).Rows(0).Item("finalDictum") '"Solicitud rechazada por capacidad de pago, rechazo por buro."  'Monto de la solicitud preguntar de donde se obtendra el dato.
        Dim msj1_1_S As Char() = msj1_1.ToCharArray()
        Dim longitud_msj1 As Integer = Len(msj1_1_S) 'longitud de la fecha.
        For valor3 As Integer = 0 To 199 Step +1
            If longitud_msj1 > valor3 Then
                msj1.Add(msj1_1_S(valor3)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                msj1.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj2 As DataSet
        'dtMsj2 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        Dim msj2_2 As String = " " 'dtMsj2.Tables(0).Rows(0).Item("finalDictum") '"Se rechaza solicitud por capacidad en buro de credito, motor de capacidad se puede ofrecer una oferta menor"
        Dim msj2_2_S As Char() = msj2_2.ToCharArray()
        Dim longitud_msj2 As Integer = Len(msj2_2_S)
        For valor4 As Integer = 0 To 299 Step +1
            If longitud_msj2 > valor4 Then
                msj2.Add(msj2_2_S(valor4))
            Else
                msj2.Add(" ")
            End If
        Next

        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim folio_Cadena As String = String.Join("", folio.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())
        Dim msj1_Cadena As String = String.Join("", msj1.ToArray())
        Dim msj2_Cadena As String = String.Join("", msj2.ToArray())

        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena & msj1_Cadena & msj2_Cadena 'Formamos mensaje como es requerido por el WS posicional de mal.
        Dim strEmail As String
        If (Not IsNothing(datosSol) AndAlso datosSol.Tables.Count > 0 AndAlso datosSol.Tables(1).Rows.Count() > 0 AndAlso datosSol.Tables(1).Rows(0).Item("EMAIL_ENVIO") <> String.Empty) Then
            strEmail = datosSol.Tables(1).Rows(0).Item("EMAIL_ENVIO")
        Else
            Dim strLocation As String = String.Empty
            If (mostrarPantalla = 0) Then
                dslink = objtarea.SiguienteTarea(Val(Request("Sol")))
                Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString, False)
                Exit Sub
            ElseIf (mostrarPantalla = 2) Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                Exit Sub
            End If
        End If
        gestorEventos.dtoReceiver.emailReceiver.address = strEmail.ToString() '"g.martinez.quiroz@accenture.com" ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000001093" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "AGAU0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    'cambio urgente
                    Exit Sub 'asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) 'en caso de error volvemos a la tarea anterior.  
                End If
            Else
                fn_Replica_envio(idSolicitud, Mensaje)
                'asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If

    End Sub

    ''' <summary>
    ''' NO PROCESABLE POS-06 (Correo Agencia)
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    Private Sub fn_No_Procesable(ByVal idSolicitud As Integer)
        Dim msj1 As New ArrayList
        Dim msj2 As New ArrayList

        Dim nom As String = "Estimado(a):" & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next
        Dim folio_C As String
        folio_C = CStr(solicitud) '"10000006789" 'preguntar de donde se obtendra el folio
        Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
        Dim longitud_F As Integer
        longitud_F = Len(folio_S) 'longitud de caracteres 
        For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud_F > valor1 Then
                folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If

        Next
        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj1 As DataSet
        'dtMsj1 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        'If dtMsj1.Tables(0).Rows.Count = 0 Then
        '    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
        '    Exit Sub
        'End If
        Dim msj1_1 As String = "No Procesable" 'dtMsj1.Tables(0).Rows(0).Item("finalDictum") '"Solicitud rechazada por capacidad de pago, rechazo por buro."  'Monto de la solicitud preguntar de donde se obtendra el dato.
        Dim msj1_1_S As Char() = msj1_1.ToCharArray()
        Dim longitud_msj1 As Integer = Len(msj1_1_S) 'longitud de la fecha.
        For valor3 As Integer = 0 To 199 Step +1
            If longitud_msj1 > valor3 Then
                msj1.Add(msj1_1_S(valor3)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                msj1.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj2 As DataSet
        'dtMsj2 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        Dim msj2_2 As String = "Favor de validar caja de notas." 'dtMsj2.Tables(0).Rows(0).Item("finalDictum") '"Se rechaza solicitud por capacidad en buro de credito, motor de capacidad se puede ofrecer una oferta menor"
        Dim msj2_2_S As Char() = msj2_2.ToCharArray()
        Dim longitud_msj2 As Integer = Len(msj2_2_S)
        For valor4 As Integer = 0 To 299 Step +1
            If longitud_msj2 > valor4 Then
                msj2.Add(msj2_2_S(valor4))
            Else
                msj2.Add(" ")
            End If
        Next

        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim folio_Cadena As String = String.Join("", folio.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())
        Dim msj1_Cadena As String = String.Join("", msj1.ToArray())
        Dim msj2_Cadena As String = String.Join("", msj2.ToArray())

        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena & msj1_Cadena & msj2_Cadena 'Formamos mensaje como es requerido por el WS posicional de mal.
        Dim strEmail As String
        If (Not IsNothing(datosSol) AndAlso datosSol.Tables.Count > 0 AndAlso datosSol.Tables(1).Rows.Count() > 0 AndAlso datosSol.Tables(1).Rows(0).Item("EMAIL_ENVIO") <> String.Empty) Then
            strEmail = datosSol.Tables(1).Rows(0).Item("EMAIL_ENVIO")
        Else
            Dim strLocation As String = String.Empty
            If (mostrarPantalla = 0) Then
                dslink = objtarea.SiguienteTarea(Val(Request("Sol")))
                Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString, False)
                Exit Sub
            ElseIf (mostrarPantalla = 2) Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                Exit Sub
            End If
        End If
        gestorEventos.dtoReceiver.emailReceiver.address = strEmail.ToString() '"g.martinez.quiroz@accenture.com" ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000001093" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "AGAU0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    'asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                    Exit Sub
                End If
            Else
                fn_Replica_envio(idSolicitud, Mensaje)
                If (Pantalla = 80 And Pantalla = 106) Then
                    'asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO")) ' si  no a tarea no rechazo  
                End If

            End If
        End If

    End Sub

    ''' <summary>
    ''' POSTAL NP PROCESABLE ID NO LEGIBLE POS-07 (Correo Agencia)
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    Private Sub fn_No_Procesable_ID_Ilegible(ByVal idSolicitud As Integer)
        Dim msj1 As New ArrayList
        Dim msj2 As New ArrayList

        Dim nom As String = "Estimado(a):" & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next
        Dim folio_C As String
        folio_C = CStr(solicitud) '"10000006789" 'preguntar de donde se obtendra el folio
        Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
        Dim longitud_F As Integer
        longitud_F = Len(folio_S) 'longitud de caracteres 
        For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud_F > valor1 Then
                folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If

        Next
        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj1 As DataSet
        'dtMsj1 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        'If dtMsj1.Tables(0).Rows.Count = 0 Then
        '    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
        '    Exit Sub
        'End If
        Dim msj1_1 As String = "No Procesable" 'dtMsj1.Tables(0).Rows(0).Item("finalDictum") '"Solicitud rechazada por capacidad de pago, rechazo por buro."  'Monto de la solicitud preguntar de donde se obtendra el dato.
        Dim msj1_1_S As Char() = msj1_1.ToCharArray()
        Dim longitud_msj1 As Integer = Len(msj1_1_S) 'longitud de la fecha.
        For valor3 As Integer = 0 To 199 Step +1
            If longitud_msj1 > valor3 Then
                msj1.Add(msj1_1_S(valor3)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                msj1.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj2 As DataSet
        'dtMsj2 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        Dim msj2_2 As String = "Identificación Oficial No Legible." 'dtMsj2.Tables(0).Rows(0).Item("finalDictum") '"Se rechaza solicitud por capacidad en buro de credito, motor de capacidad se puede ofrecer una oferta menor"
        Dim msj2_2_S As Char() = msj2_2.ToCharArray()
        Dim longitud_msj2 As Integer = Len(msj2_2_S)
        For valor4 As Integer = 0 To 299 Step +1
            If longitud_msj2 > valor4 Then
                msj2.Add(msj2_2_S(valor4))
            Else
                msj2.Add(" ")
            End If
        Next

        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim folio_Cadena As String = String.Join("", folio.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())
        Dim msj1_Cadena As String = String.Join("", msj1.ToArray())
        Dim msj2_Cadena As String = String.Join("", msj2.ToArray())

        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena & msj1_Cadena & msj2_Cadena 'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = "g.martinez.quiroz@accenture.com" ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000001093" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "AGAU0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                End If
            Else
                fn_Replica_envio(idSolicitud, Mensaje)
                'asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If

    End Sub

    ''' <summary>
    '''  POSTAL NO PROCESABLE ID NO VALIDA POS-08 (Correo Agencia)
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    Private Sub fn_No_Procesable_ID_No_Valida(ByVal idSolicitud As Integer)
        Dim msj1 As New ArrayList
        Dim msj2 As New ArrayList

        Dim nom As String = "Estimado(a):" & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next
        Dim folio_C As String
        folio_C = CStr(solicitud) '"10000006789" 'preguntar de donde se obtendra el folio
        Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
        Dim longitud_F As Integer
        longitud_F = Len(folio_S) 'longitud de caracteres 
        For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud_F > valor1 Then
                folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj1 As DataSet
        'dtMsj1 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        'If dtMsj1.Tables(0).Rows.Count = 0 Then
        '    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
        '    Exit Sub
        'End If
        Dim msj1_1 As String = "No Procesable" 'dtMsj1.Tables(0).Rows(0).Item("finalDictum") '"Solicitud rechazada por capacidad de pago, rechazo por buro."  'Monto de la solicitud preguntar de donde se obtendra el dato.
        Dim msj1_1_S As Char() = msj1_1.ToCharArray()
        Dim longitud_msj1 As Integer = Len(msj1_1_S) 'longitud de la fecha.
        For valor3 As Integer = 0 To 199 Step +1
            If longitud_msj1 > valor3 Then
                msj1.Add(msj1_1_S(valor3)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                msj1.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj2 As DataSet
        'dtMsj2 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        Dim msj2_2 As String = "Identificación Oficial No valida." 'dtMsj2.Tables(0).Rows(0).Item("finalDictum") '"Se rechaza solicitud por capacidad en buro de credito, motor de capacidad se puede ofrecer una oferta menor"
        Dim msj2_2_S As Char() = msj2_2.ToCharArray()
        Dim longitud_msj2 As Integer = Len(msj2_2_S)
        For valor4 As Integer = 0 To 299 Step +1
            If longitud_msj2 > valor4 Then
                msj2.Add(msj2_2_S(valor4))
            Else
                msj2.Add(" ")
            End If
        Next

        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim folio_Cadena As String = String.Join("", folio.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())
        Dim msj1_Cadena As String = String.Join("", msj1.ToArray())
        Dim msj2_Cadena As String = String.Join("", msj2.ToArray())

        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena & msj1_Cadena & msj2_Cadena 'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = "g.martinez.quiroz@accenture.com" ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000001093" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "AGAU0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                End If
            Else
                fn_Replica_envio(idSolicitud, Mensaje)
                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If

    End Sub

    ''' <summary>
    ''' NO PROCESABLE, SOLICITUD ILEGIBLE. (Correo Agencia)
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    Private Sub fn_No_Procesable_Solicitud_Ilegible(ByVal idSolicitud As Integer)
        Dim msj1 As New ArrayList
        Dim msj2 As New ArrayList

        Dim nom As String = "Estimado(a):" & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next
        Dim folio_C As String
        folio_C = CStr(solicitud) '"10000006789" 'preguntar de donde se obtendra el folio
        Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
        Dim longitud_F As Integer
        longitud_F = Len(folio_S) 'longitud de caracteres 
        For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud_F > valor1 Then
                folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj1 As DataSet
        'dtMsj1 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        'If dtMsj1.Tables(0).Rows.Count = 0 Then
        '    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
        '    Exit Sub
        'End If
        Dim msj1_1 As String = "No Procesable" 'dtMsj1.Tables(0).Rows(0).Item("finalDictum") '"Solicitud rechazada por capacidad de pago, rechazo por buro."  'Monto de la solicitud preguntar de donde se obtendra el dato.
        Dim msj1_1_S As Char() = msj1_1.ToCharArray()
        Dim longitud_msj1 As Integer = Len(msj1_1_S) 'longitud de la fecha.
        For valor3 As Integer = 0 To 199 Step +1
            If longitud_msj1 > valor3 Then
                msj1.Add(msj1_1_S(valor3)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                msj1.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj2 As DataSet
        'dtMsj2 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        Dim msj2_2 As String = "Solicitud de crédito No Legible." 'dtMsj2.Tables(0).Rows(0).Item("finalDictum") '"Se rechaza solicitud por capacidad en buro de credito, motor de capacidad se puede ofrecer una oferta menor"
        Dim msj2_2_S As Char() = msj2_2.ToCharArray()
        Dim longitud_msj2 As Integer = Len(msj2_2_S)
        For valor4 As Integer = 0 To 299 Step +1
            If longitud_msj2 > valor4 Then
                msj2.Add(msj2_2_S(valor4))
            Else
                msj2.Add(" ")
            End If
        Next

        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim folio_Cadena As String = String.Join("", folio.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())
        Dim msj1_Cadena As String = String.Join("", msj1.ToArray())
        Dim msj2_Cadena As String = String.Join("", msj2.ToArray())

        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena & msj1_Cadena & msj2_Cadena 'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = "g.martinez.quiroz@accenture.com" ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000001093" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "AGAU0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                End If
            Else
                fn_Replica_envio(idSolicitud, Mensaje)
                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If

    End Sub

    ''' <summary>
    ''' RECHAZO DE MODELO POS-15 (Correo Agencia)
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    Private Sub fn_Rechazo_Modelo(ByVal idSolicitud As Integer)
        Dim msj1 As New ArrayList
        Dim msj2 As New ArrayList

        Dim nom As String = "Estimado(a):" & " " & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next
        Dim folio_C As String
        folio_C = CStr(solicitud) '"10000006789" 'preguntar de donde se obtendra el folio
        Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
        Dim longitud_F As Integer
        longitud_F = Len(folio_S) 'longitud de caracteres 
        For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud_F > valor1 Then
                folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj1 As DataSet
        'dtMsj1 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        'If dtMsj1.Tables(0).Rows.Count = 0 Then
        '    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
        '    Exit Sub
        'End If
        Dim msj1_1 As String = "Rechazo" 'dtMsj1.Tables(0).Rows(0).Item("finalDictum") '"Solicitud rechazada por capacidad de pago, rechazo por buro."  'Monto de la solicitud preguntar de donde se obtendra el dato.
        Dim msj1_1_S As Char() = msj1_1.ToCharArray()
        Dim longitud_msj1 As Integer = Len(msj1_1_S) 'longitud de la fecha.
        For valor3 As Integer = 0 To 199 Step +1
            If longitud_msj1 > valor3 Then
                msj1.Add(msj1_1_S(valor3)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                msj1.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        'Dim dtMsj2 As DataSet
        'dtMsj2 = BD.EjecutarQuery("SELECT A.finalDictum FROM PDK_TAB_HERMES_RESPONSE A WHERE A.PDK_ID_SECCCERO =" & solicitud.ToString())
        Dim msj2_2 As String = "La solicitud de crédito ha sido rechazada. Para mayor información consulta caja de notas." 'dtMsj2.Tables(0).Rows(0).Item("finalDictum") '"Se rechaza solicitud por capacidad en buro de credito, motor de capacidad se puede ofrecer una oferta menor"
        Dim msj2_2_S As Char() = msj2_2.ToCharArray()
        Dim longitud_msj2 As Integer = Len(msj2_2_S)
        For valor4 As Integer = 0 To 299 Step +1
            If longitud_msj2 > valor4 Then
                msj2.Add(msj2_2_S(valor4))
            Else
                msj2.Add(" ")
            End If
        Next

        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim folio_Cadena As String = String.Join("", folio.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())
        Dim msj1_Cadena As String = String.Join("", msj1.ToArray())
        Dim msj2_Cadena As String = String.Join("", msj2.ToArray())

        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena & msj1_Cadena & msj2_Cadena 'Formamos mensaje como es requerido por el WS posicional de mal.
        Dim strEmail As String
        If (Not IsNothing(datosSol) AndAlso datosSol.Tables.Count > 0 AndAlso datosSol.Tables(1).Rows.Count() > 0 AndAlso datosSol.Tables(1).Rows(0).Item("EMAIL_ENVIO") <> String.Empty) Then
            strEmail = datosSol.Tables(1).Rows(0).Item("EMAIL_ENVIO")
        Else
            Dim strLocation As String = String.Empty
            If (mostrarPantalla = 0) Then
                dslink = objtarea.SiguienteTarea(Val(Request("Sol")))
                Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString, False)
                Exit Sub
            ElseIf (mostrarPantalla = 2) Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                Exit Sub
            End If
        End If
        gestorEventos.dtoReceiver.emailReceiver.address = strEmail.ToString() '"g.martinez.quiroz@accenture.com" ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000001093" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "AGAU0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    'asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                    Exit Sub
                End If
            Else
                fn_Replica_envio(idSolicitud, Mensaje)
                'asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If

    End Sub

    ''' <summary>
    ''' SMS APROBADA SMS-16
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    ''' <param name="Mensaje"></param>
    Private Sub Fun_AprobadoSMS_Prueba(ByVal idSolicitud As Integer, ByVal Mensaje As String)

        Dim mensaje1 As String = Mensaje

        Dim gestorSMS As New clsaproSMS
        Dim fechasms As String = Format(Date.Now(), "MMM-yyyy-dd H:mm")

        Mensaje = mensaje1 'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorSMS.dtoReceiver.smsReceiver.company = ConfigurationManager.AppSettings("COMPANIA").ToString  'Datos hardcore de prueba. la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("COMPANY_MOVIL")
        gestorSMS.dtoReceiver.smsReceiver.phoneNumber = ConfigurationManager.AppSettings("CELULAR").ToString 'Datos hardcore de prueba.  la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("TELEFONO_MOVIL") 5561227876 numero rene Telcel 
        gestorSMS.eventCode = "0000000984" 'Event code recuperado de la tabla para mensajes 
        gestorSMS.message = Mensaje
        gestorSMS.messageTemplate = "KZ2C0101" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverReference = "D0075225" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverType = "informado" 'recuperado de la tabla para mensajes 

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody1 As String = serializer.Serialize(gestorSMS) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody1) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody1

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError & "Prueba"
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente" & "Prueba"
        End If

        objRespuesta.insertaDatosRespuesta()
    End Sub

    ''' <summary>
    ''' SMS APROBADO 
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    Private Sub Fun_AprobadoSMS_(ByVal idSolicitud As Integer)
        Dim gestorSMS As New clsaproSMS
        Dim fechasms As String = Format(Date.Now(), "MMM-yyyy-dd H:mm")

        gestorSMS.dtoReceiver.smsReceiver.company = datosSol.Tables(0).Rows(0).Item("COMPANY_MOVIL").ToString '"MOVISTAR" 'Datos hardcore de prueba. la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("COMPANY_MOVIL")
        gestorSMS.dtoReceiver.smsReceiver.phoneNumber = datosSol.Tables(0).Rows(0).Item("TELEFONO_MOVIL") '"5547315526" 'Datos hardcore de prueba.  la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("TELEFONO_MOVIL") 5561227876 numero rene Telcel 
        gestorSMS.eventCode = "0000000984" 'Event code recuperado de la tabla para mensajes 
        gestorSMS.message = fechasms & " " & "Tu solicitud" & " " & solicitud & " " &
            "de Credito Auto Consumer Finance fue autorizada!. Si no reconoces esta solicitud comunicate al 10547088" 'recuperado de la tabla para mensajes 
        gestorSMS.messageTemplate = "KZ2C0101" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverReference = "D0075225" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverType = "informado" 'recuperado de la tabla para mensajes 

        Dim serializer1 As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody1 As String = serializer1.Serialize(gestorSMS) 'Se serializa el contenido de la clase gestorSMS  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) 'user id 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket para ocupar los servicios 
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") ' recuperado del web.config. uri
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody1) 'respuesta del webservice 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody1

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    'asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                    Exit Sub
                End If
            Else
                Fun_AprobadoSMS_Prueba(idSolicitud, Mensaje)
                'asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If

    End Sub

    ''' <summary>
    ''' APROBADO SMS HONDA SM-17
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    Private Sub Fun_AprobadoSMS_Honda_Acura(ByVal idSolicitud As Integer)
        Dim gestorSMS As New clsaproSMS
        Dim fechasms As String = Format(Date.Now(), "MMM-yyyy-dd H:mm")
        Dim MensajeSMS As String

        MensajeSMS = fechasms & " " & "Tu solicitud" & " " & solicitud & " " &
            "de Credito Auto con Honda fue autorizada!. Si no reconoces esta solicitud comunicate al 10547089" 'recuperado de la tabla para mensajes 
        gestorSMS.dtoReceiver.smsReceiver.company = datosSol.Tables(0).Rows(0).Item("COMPANY_MOVIL").ToString '"MOVISTAR" 'Datos hardcore de prueba. la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("COMPANY_MOVIL")
        gestorSMS.dtoReceiver.smsReceiver.phoneNumber = datosSol.Tables(0).Rows(0).Item("TELEFONO_MOVIL") '"5547315526" 'Datos hardcore de prueba.  la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("TELEFONO_MOVIL") 5561227876 numero rene Telcel 
        gestorSMS.eventCode = "0000000984" 'Event code recuperado de la tabla para mensajes 
        gestorSMS.message = MensajeSMS
        gestorSMS.messageTemplate = "KZ2C0101" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverReference = "D0075225" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverType = "informado" 'recuperado de la tabla para mensajes 

        Dim serializer1 As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody1 As String = serializer1.Serialize(gestorSMS) 'Se serializa el contenido de la clase gestorSMS  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) 'user id 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket para ocupar los servicios 
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") ' recuperado del web.config. uri
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody1) 'respuesta del webservice 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody1

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    'asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                    Exit Sub
                End If
            Else
                Fun_AprobadoSMS_Prueba(idSolicitud, MensajeSMS)
                'asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If
    End Sub

    ''' <summary>
    '''  APROBADO SMS SUZUKI
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    Private Sub Fun_AprobadoSMS_Suzuki(ByVal idSolicitud As Integer)
        Dim gestorSMS As New clsaproSMS
        Dim fechasms As String = Format(Date.Now(), "MMM-yyyy-dd H:mm")
        Dim MensajeSMS As String
        MensajeSMS = fechasms & " " & "Tu solicitud" & " " & solicitud & " " &
            "de Credito Auto SukiCredit fue autorizada!. Si no reconoces esta solicitud comunicate al 80002693" 'recuperado de la tabla para mensajes 

        gestorSMS.dtoReceiver.smsReceiver.company = datosSol.Tables(0).Rows(0).Item("COMPANY_MOVIL").ToString '"MOVISTAR" 'Datos hardcore de prueba. la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("COMPANY_MOVIL")
        gestorSMS.dtoReceiver.smsReceiver.phoneNumber = datosSol.Tables(0).Rows(0).Item("TELEFONO_MOVIL") '"5547315526" 'Datos hardcore de prueba.  la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("TELEFONO_MOVIL") 5561227876 numero rene Telcel 
        gestorSMS.eventCode = "0000000984" 'Event code recuperado de la tabla para mensajes 
        gestorSMS.message = MensajeSMS
        gestorSMS.messageTemplate = "KZ2C0101" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverReference = "D0075225" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverType = "informado" 'recuperado de la tabla para mensajes 

        Dim serializer1 As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody1 As String = serializer1.Serialize(gestorSMS) 'Se serializa el contenido de la clase gestorSMS  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) 'user id 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket para ocupar los servicios 
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") ' recuperado del web.config. uri
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody1) 'respuesta del webservice 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody1

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    'asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                    Exit Sub
                End If
            Else
                Fun_AprobadoSMS_Prueba(idSolicitud, MensajeSMS)
                'asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If

    End Sub

    ''' <summary>
    ''' APROBADO SMS MOTOS
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    Private Sub Fun_AprobadoSMS_Motos(ByVal idSolicitud As Integer)
        Dim gestorSMS As New clsaproSMS
        Dim fechasms As String = Format(Date.Now(), "MMM-yyyy-dd H:mm")
        Dim MensajeSMS As String

        MensajeSMS = fechasms & " " & "Tu solicitud" & " " & solicitud & " " &
            "de Credito de Motos Consumer Finance fue autorizada!. Si no reconoces esta solicitud comunicate al 41959263" 'recuperado de la tabla para mensajes 
        gestorSMS.dtoReceiver.smsReceiver.company = datosSol.Tables(0).Rows(0).Item("COMPANY_MOVIL").ToString '"MOVISTAR" 'Datos hardcore de prueba. la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("COMPANY_MOVIL")
        gestorSMS.dtoReceiver.smsReceiver.phoneNumber = datosSol.Tables(0).Rows(0).Item("TELEFONO_MOVIL") '"5547315526" 'Datos hardcore de prueba.  la linea de abajo comentada es  la manera dinamica para recuperar estos datos de la solicitud 
        'datosSol.Tables(0).Rows(0).Item("TELEFONO_MOVIL") 5561227876 numero rene Telcel 
        gestorSMS.eventCode = "0000000984" 'Event code recuperado de la tabla para mensajes 
        gestorSMS.message = MensajeSMS
        gestorSMS.messageTemplate = "KZ2C0101" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverReference = "D0075225" 'recuperado de la tabla para mensajes 
        gestorSMS.receiverType = "informado" 'recuperado de la tabla para mensajes 

        Dim serializer1 As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody1 As String = serializer1.Serialize(gestorSMS) 'Se serializa el contenido de la clase gestorSMS  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) 'user id 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket para ocupar los servicios 
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") ' recuperado del web.config. uri
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody1) 'respuesta del webservice 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody1

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    'asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                    Exit Sub
                End If
            Else
                Fun_AprobadoSMS_Prueba(idSolicitud, MensajeSMS)
                'asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If

    End Sub

    ''' <summary>
    ''' COMPROBANTES UNGRESOS APROVADOS
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    Private Sub Fun_ComprobantesIngresosBCOM_(ByVal idSolicitud As Integer)


        Dim nom As String = datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'Nombre mas mensaje de configuracion dependiendo pantalla anterior.
        'cadena = "Aprobado BCOM Luis Antonio Maya Cardenas                                                                                                      10000001234       2017-01-11 16:49:000Luis Antonio Maya Cardenas                                                                          250,000      0113.995,730        555302905020.9913.9911012017110120181102201730.9920.80111120171111201801012017"
        Dim caracteres As Char() = nom.ToCharArray() 'arreglo de caracteres de nom
        Dim longitud As Integer
        longitud = Len(caracteres) 'longitud de caracteres 
        For valor As Integer = 0 To 141 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud > valor Then
                lista.Add(caracteres(valor)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                lista.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload posicional.
            End If
        Next

        Dim folio_C As String
        folio_C = datosSol.Tables(0).Rows(0).Item("FOLIO").ToString 'preguntar de donde se obtendra el folio
        Dim folio_S As Char() = folio_C.ToCharArray() 'arreglo de caracteres de folio
        Dim longitud_F As Integer
        longitud_F = Len(folio_S) 'longitud de caracteres 
        For valor1 As Integer = 0 To 17 Step +1 'for para recorrer 141 veces el tamaño defenido para el asunto 
            If longitud_F > valor1 Then
                folio.Add(folio_S(valor1)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                folio.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next

        Dim date1 As String = Format(Date.Now(), "yyyy-MM-dd H:mm:ss").ToString & "0" & datosSol.Tables(0).Rows(0).Item("NOMBRE").ToString 'fecha como es requerida en el WS DEL MAL 
        Dim date1_S As Char() = date1.ToCharArray()
        Dim longitud_Fecha As Integer
        longitud_Fecha = Len(date1_S) 'longitud de  fecha 
        For valor2 As Integer = 0 To 119 Step +1
            If longitud_Fecha > valor2 Then
                fecha.Add(date1_S(valor2)) 'mientras la longitud sea mayor a el valor en el ciclo agregaremos el arreglo de char al arraylist
            Else
                fecha.Add(" ") 'cuando valor supere a la longitud del arreglo de char agregamos espacios en blanco para completar la longitud requerida en el payload
            End If
        Next
        ''''''''''''''''''''''''''''''''''''''''''''''''''''-----------------------------------------------------
        Dim Numero As String = "5553029050"  'Monto de la solicitud preguntar de donde se obtendra el dato.        
        'Convertimos Arrays a cadenas para enviar el webservice del mal D: 
        Dim lista_Cadena As String = String.Join("", lista.ToArray())
        Dim folio_Cadena As String = String.Join("", folio.ToArray())
        Dim fecha_Cadena As String = String.Join("", fecha.ToArray())

        'Terminamos conversion de array a cadenas.
        Mensaje = lista_Cadena & folio_Cadena & fecha_Cadena & Numero 'Formamos mensaje como es requerido por el WS posicional de mal.
        Dim strEmail As String
        If (Not IsNothing(datosSol) AndAlso datosSol.Tables.Count > 0 AndAlso datosSol.Tables(1).Rows.Count() > 0 AndAlso datosSol.Tables(1).Rows(0).Item("EMAIL_ENVIO") <> String.Empty) Then
            strEmail = datosSol.Tables(1).Rows(0).Item("EMAIL_ENVIO")
        Else
            Dim strLocation As String = String.Empty
            If (mostrarPantalla = 0) Then
                dslink = objtarea.SiguienteTarea(Val(Request("Sol")))
                Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString, False)
                Exit Sub
            ElseIf (mostrarPantalla = 2) Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                Exit Sub
            End If
        End If
        gestorEventos.dtoReceiver.emailReceiver.address = strEmail.ToString() ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA.  "g.martinez.quiroz@accenture.com"
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000000979" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "KZTX0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente"
        End If

        If objRespuesta.insertaDatosRespuesta() Then

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    'asignaTarea(valorTarea) 'en caso de error volvemos a la tarea anterior.  
                    Exit Sub
                End If
            Else
                Fun_ComprobantesIngresosBCOM_Prueba(idSolicitud, Mensaje)
                'asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' si  no a tarea no rechazo  
            End If
        End If
    End Sub

    ''' <summary>
    ''' COMPROBANTES INGRESOS
    ''' </summary>
    ''' <param name="idSolicitud"></param>
    ''' <param name="Mensaje"></param>
    Private Sub Fun_ComprobantesIngresosBCOM_Prueba(ByVal idSolicitud As Integer, ByVal Mensaje As String)

        ''''''''''''''''''''''''''''''''''''''''''''''''''''-----------------------------------------------------
        Dim Mensaje1 As String = Mensaje
        'Terminamos conversion de array a cadenas.
        Mensaje = Mensaje1  'Formamos mensaje como es requerido por el WS posicional de mal.
        gestorEventos.dtoReceiver.emailReceiver.address = ConfigurationManager.AppSettings("CorreoPrueba").ToString ' Correo HARDCORE PARA PRUEBAS CAMBIAR POR LINEA COMENTADA. 
        'datosSol.Tables(0).Rows(0).Item("CORREO_ELECTRONICO") 'Manera dinamica para obtener el correo. 
        gestorEventos.eventCode = "0000000979" 'EVENT CODE 
        gestorEventos.message = Mensaje 'MENSAJE POSICIONAL como lo requiere el WS para dar todo OK.
        gestorEventos.messageTemplate = "KZTX0101" 'se recupera del store.
        gestorEventos.receiverReference = "D0075225" ' se recupera el store.
        gestorEventos.receiverType = "informado"  'Se recupera del store. 

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonbody As String = serializer.Serialize(gestorEventos) 'serializamos  

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String) ' user ID 
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String) 'iv_ticket  
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("AprobadoBCOM") 'Recuperamos del  webconfig. 
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonbody) ' resultado del web service respuesta 

        Dim objRespuesta As New clsGuardarRespuesta()
        objRespuesta.ID_SOLICITUD = solicitud
        objRespuesta.JSON = jsonbody

        If restGT.MensajeError <> "" Then

            objRespuesta.RESPUESTA = restGT.MensajeError & "Prueba"
        Else
            objRespuesta.RESPUESTA = "Proceso Ejecutado correctamente" & "Prueba"
        End If

        objRespuesta.insertaDatosRespuesta()

    End Sub




End Class
