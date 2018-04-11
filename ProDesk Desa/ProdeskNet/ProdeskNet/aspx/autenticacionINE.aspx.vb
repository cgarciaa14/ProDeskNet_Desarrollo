#Region "TRACKERS"
'BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
'BUG-PD-149:MPUESTO:11/07/2017:Mejoras en la interfaz y decodificacion de imagenes, correcciones de botones
'BUG-PD-204 GVARGAS 19/09/2017 WS biometrico
'BUG-PD-211 GVARGAS 27/09/2017 Cambios mostrar info
'BUG-PD-222 GVARGAS 05/10/2017 Mejoras captura INE
'BUG-PD-229 GVARGAS 09/10/2017 Captura Hand Position & FingerPrint WSQ
'BBV-P-423 RQ-PD-17 2 GVARGAS 02/01/2018 Mejoras carga huella
'BBV-P-423 RQ-PD-17 3 GVARGAS 08/01/2018 Mejoras carga huella y cambio payload
'BBV-P-423 RQ-PD-17 4 GVARGAS 12/01/2018 Update when validation is OK
'BBV-P-423 RQ-PD-17 7 GVARGAS 22/01/2018 Correcciones generales
'BBV-P-423 RQ-PD-17 8 GVARGAS 25/01/2018 Ajuste validaciones Biometrico
'BBV-P-423 RQ-PD-17 9 GVARGAS 30/01/2018 Ajustes flujo
'BBV-P-423 RQ-PD-17 11 GVARGAS 30/01/2018 Ajustes flujo 2
'BBV-P-423 RQ-PD-17 12 GVARGAS 06/02/2018 Ajustes flujo 3
'BBV-P-423 RQ-PD-17 13 GVARGAS 13/02/2018 Ajustes flujo 4
'BBV-P-423 RQ-PD-17 14 GVARGAS 13/02/2018 Ajustes flujo 5
'BBV-P-423 RQ-PD-17 15 GVARGAS 16/02/2018 New reader methotd  
'BBV-P-423 RQ-PD-17 16 GVARGAS 26/02/2018 Cambios correcion AJAX Tool Kit
'BUG-PD-380 GVARGAS 02/03/2018 Correccion urgente instalacion biometrico
#End Region

Imports System.Data
Imports ProdeskNet.Catalogos
Imports System.IO
Imports ProdeskNet.BD
Imports ProdeskNet.Criptografia
Imports ProdeskNet.Criptografia.Entities
Imports System.Web.Services
Imports System.Drawing
Imports System.Drawing.Imaging
Imports ProdeskNet.Configurcion
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports System.Drawing.Bitmap

Partial Class aspx_autenticacionINE
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim _mostrarPantalla As Integer = 0
    Dim _clsPantallas As New ProdeskNet.SN.clsPantallas()
    Dim _dtsResult As New DataSet
    Dim _clsDatosCliente As New clsDatosCliente
    Dim _clsStatusSolicitud As New clsStatusSolicitud
    Dim _numeroIdentificacion As String
    Dim _claveElector As String
    Dim _tipoIdentificacion As String
    Dim _clsManejaBD As New clsManejaBD
    Dim _dsResult As New DataSet
    Dim _clsCatTareas As New ProdeskNet.SN.clsCatTareas()
    Dim _clsSolicitudes As New clsSolicitudes(0)
#End Region

#Region "Properties"
    Public ReadOnly Property BiometricWebPage() As String
        Get
            Return IIf(Not ConfigurationManager.AppSettings("BiometricPageURL").ToString() = String.Empty, _
                       (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) & _
                        ResolveUrl("~/") & _
                        ConfigurationManager.AppSettings("BiometricPageURL").ToString()) _
                    , ConfigurationManager.AppSettings("ExternalBiometricPageURL").ToString())
        End Get
    End Property

    Public Property StatusModal() As Boolean
        Get
            Return Convert.ToBoolean(Session("_StatusModal"))
        End Get
        Set(ByVal value As Boolean)
            Session("_StatusModal") = value
        End Set
    End Property

    Public ReadOnly Property _idPantalla() As Integer
        Get
            Return IIf(Not Request("idPantalla") Is Nothing, Request("idPantalla"), 0)
        End Get
    End Property

    Public ReadOnly Property _idSolicitud() As Integer
        Get
            Return IIf(Not Request("sol") Is Nothing, Val(Request("Sol")), _
                                         IIf(Not Request("NoSolicitud") Is Nothing, Val(Request("Sol")), _
                                             IIf(Not Request("IdFolio") Is Nothing, Val(Request("Sol")), 0)))
        End Get
    End Property

    Public Property FingerprintParams() As FingerprintParams
        Get
            Return CType(Session("_FingerprintParams"), FingerprintParams)
        End Get
        Set(ByVal value As FingerprintParams)
            Session("_FingerprintParams") = value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            StatusModal = False
            SetVisibilityTable(False)
            If LoadUserData() Then
                Me.btnGeneraArchivo.Enabled = True
            Else
                Me.btnGeneraArchivo.Enabled = True
            End If
        End If
    End Sub

    Private Function LoadUserData() As Boolean
        Dim result As Boolean

        _clsDatosCliente = New clsDatosCliente()
        _clsDatosCliente.GetDatosCliente(_idSolicitud)
        _clsStatusSolicitud.getStatusSol(_idSolicitud)
        lblSolicitud.Text = _idSolicitud
        lblCliente.Text = _clsDatosCliente.propNombreCompleto
        lblStCredito.Text = _clsStatusSolicitud.PStCredito
        lblStDocumento.Text = _clsStatusSolicitud.PStDocumento

        _dtsResult = New DataSet()
        _clsManejaBD = New clsManejaBD()
        _clsManejaBD.AgregaParametro("@PDK_ID_SECCCERO", TipoDato.Entero, _idSolicitud, False)
        _dtsResult = _clsManejaBD.EjecutaStoredProcedure("get_DatosINEsolicitante")
        If Not _dtsResult Is Nothing Then
            If _dtsResult.Tables.Count > 0 Then
                If _dtsResult.Tables(0).Rows.Count > 0 Then
                    _numeroIdentificacion = IIf(_dtsResult.Tables(0).Rows(0)("N_IDENTIFICACION") Is Nothing, String.Empty, _dtsResult.Tables(0).Rows(0)("N_IDENTIFICACION").ToString())
                    _claveElector = IIf(_dtsResult.Tables(0).Rows(0)("CLAVE_ELECTOR") Is Nothing, String.Empty, _dtsResult.Tables(0).Rows(0)("CLAVE_ELECTOR").ToString())
                    _tipoIdentificacion = IIf(_dtsResult.Tables(0).Rows(0)("TIPO_IDENTIFICACION") Is Nothing, String.Empty, _dtsResult.Tables(0).Rows(0)("TIPO_IDENTIFICACION").ToString())
                    _claveElector = IIf(_dtsResult.Tables(0).Rows(0)("CLAVE_ELECTOR") Is Nothing, String.Empty, _dtsResult.Tables(0).Rows(0)("CLAVE_ELECTOR").ToString())
                End If
            End If
        End If
        tbxTipoIdentificacion.Text = _tipoIdentificacion
        tbxNoIdentificacion.Text = _numeroIdentificacion
        tbxClaveElector.Text = _claveElector

        result = (_numeroIdentificacion <> String.Empty)
        Return result
    End Function

    Private Sub RejectTask()
        _dtsResult = New DataSet()
        _clsSolicitudes = New clsSolicitudes(_idSolicitud)
        _clsSolicitudes.PDK_ID_SOLICITUD = _idSolicitud
        _clsSolicitudes.BOTON = 64
        _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
        _dtsResult = _clsManejaBD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & _idPantalla.ToString())
        If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO")
        Else
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = 0
        End If
        _clsSolicitudes.ValNegocio(1)
        checkRedirect()
    End Sub

    Private Sub CancelTask()
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim ipPantallaRechazo As Integer = 419
        Try
            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
            Solicitudes.PDK_ID_CAT_RESULTADO = ipPantallaRechazo

            dsresult = Solicitudes.ValNegocio(1)
            Dim mensaje As String = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje = "Tarea Exitosa" Then
                checkRedirect()
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Error al generar tarea.', '');", True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Error al generar tarea.', '');", True)
        End Try
    End Sub

    Private Sub asignTask(ByVal tarea As Integer, ByVal msg As String)
        '_dtsResult = New DataSet()
        _clsSolicitudes = New clsSolicitudes(_idSolicitud)
        '_clsManejaBD = New clsManejaBD()
        _clsSolicitudes.PDK_ID_SOLICITUD = _idSolicitud
        _clsSolicitudes.BOTON = 64
        _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
        _clsSolicitudes.PDK_ID_CAT_RESULTADO = tarea
        '_dtsResult = _clsManejaBD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & _idPantalla.ToString())
        'If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
        '    _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")
        'Else
        '    _clsSolicitudes.PDK_ID_CAT_RESULTADO = 0
        'End If
        _clsSolicitudes.ValNegocio(1)
        'checkRedirect()

        _dtsResult = _clsCatTareas.SiguienteTarea(Val(Request("Sol")))
        _mostrarPantalla = _clsPantallas.SiguientePantalla(Val(Request("Sol")))
        If _mostrarPantalla = 0 Then
            Dim strPath As String = "../aspx/" + _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() + "?idPantalla=" + _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() + "&Sol=" + _idSolicitud.ToString() + "&usuario=" + Val(Request("usu")).ToString()
            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + msg + "', '" + strPath + "');", True)
        ElseIf _mostrarPantalla = 2 Then
            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + msg + "', '../aspx/consultaPanelControl.aspx');", True)
        End If
    End Sub

    Private Sub NextTask(Optional ByVal message As String = "Tarea Exitosa.")
        _dtsResult = New DataSet()
        _clsSolicitudes = New clsSolicitudes(_idSolicitud)
        _clsManejaBD = New clsManejaBD()
        _clsSolicitudes.PDK_ID_SOLICITUD = _idSolicitud
        _clsSolicitudes.BOTON = 64
        _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
        _dtsResult = _clsManejaBD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & _idPantalla.ToString())
        If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")
        Else
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = 0
        End If
        _clsSolicitudes.ValNegocio(1)
        checkRedirect(message)
    End Sub

    Protected Sub checkRedirect(Optional ByVal message As String = "Tarea Exitosa.")
        _dtsResult = _clsCatTareas.SiguienteTarea(Val(Request("Sol")))
        _mostrarPantalla = _clsPantallas.SiguientePantalla(Val(Request("Sol")))
        If _mostrarPantalla = 0 Then
            Dim strPath As String = "../aspx/" + _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() + "?idPantalla=" + _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() + "&Sol=" + _idSolicitud.ToString() + "&usuario=" + Val(Request("usu")).ToString()
            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + message + "', '" + strPath + "');", True)
        ElseIf _mostrarPantalla = 2 Then
            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + message + "', '../aspx/consultaPanelControl.aspx');", True)
        End If
    End Sub

    Private Sub SetVisibilityTable(ByVal ShowResults As Boolean)
        divNoAuthentication.Visible = Not ShowResults
        divResultAuthentication.Visible = ShowResults
    End Sub

    Protected Sub btnGeneraArchivo_Click(sender As Object, e As EventArgs)
        _dsResult = New DataSet
        FingerprintParams = New FingerprintParams() With { _
                .IdUsuario = Session("IdUsua"), _
                .IdPantalla = Convert.ToInt32(Val(Request("idPantalla")).ToString()),
                .NoSolicitud = IIf(Not Request("sol") Is Nothing, Val(Request("Sol")).ToString(), _
                                 IIf(Not Request("NoSolicitud") Is Nothing, Val(Request("Sol")).ToString(), _
                                     IIf(Not Request("IdFolio") Is Nothing, Val(Request("Sol")).ToString(), "0"))), _
                .Key = Guid.NewGuid().ToString(), _
                .WebPage = BiometricWebPage
            }
        _clsManejaBD = New clsManejaBD()
        _clsManejaBD.AgregaParametro("@PDK_ID_SECC_CERO", TipoDato.Entero, FingerprintParams.NoSolicitud, False)
        _clsManejaBD.AgregaParametro("@PDK_ID_PANTALLA", TipoDato.Entero, FingerprintParams.IdPantalla, False)
        _clsManejaBD.AgregaParametro("@PDK_GUID_KEY", TipoDato.Cadena, FingerprintParams.Key, False)
        _dsResult = _clsManejaBD.EjecutaStoredProcedure("spManejaDatosHuella")

        If Not _dsResult Is Nothing And _dsResult.Tables.Count > 0 And _dsResult.Tables(0).Rows.Count > 0 Then
            If _dsResult.Tables(0).Rows(0)("MENSAJE").ToString() = String.Empty Then
                FingerprintParams.IdAutenticacion = Convert.ToInt64(_dsResult.Tables(0).Rows(0)("RESULTADO").ToString())
                StatusModal = True
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "OpenNewPopUpWindow", "window.open('../aspx/ManageFingerprintFile.aspx','_newtab');", True)
                lblEsperaHuellaTitulo.Text = "En espera de información del lector"
                lblPopUpEsperaHuella.Text = "Verifique la descarga del archivo: " & FingerprintParams.Key & ".fng     Al terminar el procesamiento de la huella por el lector, por favor adjunte el archivo que genera el lector."
            Else
                Dim _message = _dsResult.Tables(0).Rows(0)("MENSAJE").ToString()
                RejectTask()
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & _message & "', '" & "/ProdeskNet/aspx/consultaPanelControl.aspx?NoSolicitud=" & FingerprintParams.NoSolicitud.ToString() & "');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('Error de comunicaciones con la base de datos. Intentelo de nuevo más tarde.', '" & "/ProdeskNet/aspx/autenticacionINE.aspx" & "?idPantalla=" & _idPantalla & "&sol=" & _idSolicitud & "&Enable=" & Request("Enable") & "&usu=" & Request("Enable") & "');", True)
        End If

        SetModalVisivility()
    End Sub

    Protected Sub btnConsultaHuella_Click(sender As Object, e As EventArgs)
        _dsResult = New DataSet
        _clsManejaBD = New clsManejaBD()
        _clsManejaBD.AgregaParametro("@PDK_ID_SECC_CERO", TipoDato.Entero, FingerprintParams.NoSolicitud, False)
        _clsManejaBD.AgregaParametro("@PDK_ID_PANTALLA", TipoDato.Entero, FingerprintParams.IdPantalla, False)
        _clsManejaBD.AgregaParametro("@PDK_GUID_KEY", TipoDato.Cadena, FingerprintParams.Key, False)
        _dsResult = _clsManejaBD.EjecutaStoredProcedure("get_HistoryAuthentication")

        If Not _dsResult Is Nothing And _dsResult.Tables.Count > 0 And _dsResult.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(_dsResult.Tables(0).Rows(0)("PDK_FINGERPRINT")) Then
                'mpuEsperaHuella.Hide()
                btnGeneraArchivoCliente.Enabled = False
                StatusModal = False
                Dim _FingerprintStr = New Descifrado().DecryptString(_dsResult.Tables(0).Rows(0)("PDK_FINGERPRINT").ToString(), _dsResult.Tables(0).Rows(0)("PDK_GUID_KEY"))
                imgFingerprint.Src = "data:image/bmp;base64," & _FingerprintStr

            End If
        End If
        lblPopUpEsperaHuella.Text = "   No existe información asociada, procese el archivo " & FingerprintParams.Key & ".fng y obtenga la imagen dactilar con el lector. Si ya ha procesado el archivo espere unos segundos y presione el botón Continuar"
        SetModalVisivility()
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        _clsDatosCliente = New clsDatosCliente
        _clsDatosCliente.idSolicitud = _idSolicitud
        _clsDatosCliente.getDatosSol()
        'Response.Redirect("consultaPanelControl.aspx" & "?NoSolicitud=" & _clsDatosCliente.idSolicitud & "&Empresa=" & _clsDatosCliente.idempresa & "&Producto=" & _clsDatosCliente.idproducto & "&Persona=" & _clsDatosCliente.idtpersona)
        Dim strPath As String = "consultaPanelControl.aspx?NoSolicitud=" + _clsDatosCliente.idSolicitud.ToString() + "&Empresa=" + _clsDatosCliente.idempresa.ToString() + "&Producto=" + _clsDatosCliente.idproducto.ToString() + "&Persona=" + _clsDatosCliente.idtpersona.ToString()
        'ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea exitosa.', '../aspx/consultaPanelControl.aspx');", True)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" + strPath + "';", True)
    End Sub

    Protected Sub SetModalVisivility()
        btnGeneraArchivoCliente.Enabled = True
        btnGeneraArchivo.Enabled = True
        If StatusModal Then
            'mpuEsperaHuella.Show()
        Else
            'mpuEsperaHuella.Hide()
            btnProcesar.Visible = True
        End If
    End Sub

    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs)
        Dim marcaPreca As marcaPreca = getMarcaPreca(Val(Request("Sol")))

        If ((marcaPreca.fakeAuth) And (marcaPreca.id_INE = False)) Then
            cancelFinger(_idSolicitud, _idPantalla)
            cancelFinger(_idSolicitud, _idPantalla, "OK")
            NextTask()
        Else
            Dim respuesta As respuesta = valida_INE(Val(Request("Sol")))

            If (respuesta.valido) Then
                NextTask()
            Else
                If (respuesta.ruta = "0") Then 'sin servicio
                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Servicio no disponible, intentar más tarde.', '');", True)
                Else
                    Dim _nSol_ As Integer = Int32.Parse(Request("Sol").ToString())
                    Dim _idPant_ As Integer = Int32.Parse(Request("idPantalla").ToString())
                    Dim queryNew As StringBuilder = New StringBuilder()
                    queryNew.Append("EXEC crud_Biometrico_SP " + _nSol_.ToString() + ", " + _idPant_.ToString() + ", 17, ''")
                    Dim validINEhere As Integer = Int32.Parse(executeQuerys(queryNew))

                    If (validINEhere = 1) Then
                        Dim tarea_ As Integer = 0
                        Dim nSol As Integer = Int32.Parse(Request("Sol").ToString())
                        Dim idPant As Integer = Int32.Parse(Request("idPantalla").ToString())

                        Dim opc As Integer = 18

                        If (maxIntentos(Request("Sol").ToString(), Request("idPantalla").ToString())) Then
                            opc = 16
                        End If

                        Dim pathCelula As pathCelula = getPathCelula(nSol, idPant, opc)

                        If pathCelula.path = idPant Then
                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('No se logró leer la huella del solicitante.', '');", True)
                        Else
                            asignTask(pathCelula.path, pathCelula.message)
                        End If
                    Else
                        If (respuesta.ruta = "1") Then 'Tarea regreso
                            Dim nSol As Integer = Int32.Parse(Request("Sol").ToString())
                            Dim idPant As Integer = Int32.Parse(Request("idPantalla").ToString())

                            Dim query As StringBuilder = New StringBuilder()
                            query.Append("EXEC crud_Biometrico_SP " + nSol.ToString() + ", " + idPant.ToString() + ", 7, 'aa'")
                            Dim tarea As Integer = Int32.Parse(executeQuerys(query))

                            asignTask(tarea, "Verifique que el Nombre del Cliente sea Correcto.")
                        ElseIf respuesta.ruta = 2 Then 'Si no siguiente tarea
                            NextTask()
                        Else
                            If (maxIntentos(Request("Sol").ToString(), Request("idPantalla").ToString())) Then
                                NextTask("Fallo Autenticación Biométrica, Solicitud en proceso de reconsideración automática.")
                            Else
                                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('No se logró leer la huella del solicitante.', '');", True)
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Public Class validateIdentityCard
        Public identityCard As identityCard = New identityCard()
        Public operationDate As String
        Public channelCode As String
        Public functionCode As String
        Public officeCode As String
        Public userCode As String
        Public terminalCode As String
        Public pawPrint2 As String = String.Empty
        Public pawPrint7 As String = String.Empty
        Public customer As customer = New customer()
    End Class

    Public Class identityCard
        Public opticalCharacterRecognition As String
    End Class

    Public Class customer
        Public person As person = New person()
    End Class

    Public Class person
        Public name As String
        Public lastName As String
        Public mothersLastName As String
    End Class

    Public Class IneResponse
        Public ocr As String
        Public mothersLastName As String
        Public lastName As String
        Public name As String
        Public pawPrint2 As String
        Public codePawPrint2 As Integer
        Public descripcionPawPrint2 As String
        Public pawPrint7 As String
        Public codePawPrint7 As Integer
        Public descripcionPawPrint7 As String
        Public code As Integer
        Public descripcion As String
        Public yearRegistry As String
        Public numberEmision As String
        Public codeElector As String
        Public codeCurp As String
    End Class

    Private Function valida_INE(ByVal folio As String) As respuesta
        Dim autoriza As respuesta = New respuesta()
        autoriza.valido = False
        autoriza.ruta = 0

        Dim validateIdentityCard As validateIdentityCard = info_INE(folio)

        Dim imgFingerprint_ As FingerInfo = getIMG(_idPantalla, _idSolicitud)

        If imgFingerprint_.Hand = "IZQ" Then
            validateIdentityCard.pawPrint2 = imgFingerprint_.FingerprintStr 'mano Izquierda
        Else
            validateIdentityCard.pawPrint7 = imgFingerprint_.FingerprintStr 'mano Derecha
        End If

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        serializer.MaxJsonLength = Int32.MaxValue
        Dim jsonBODY As String = serializer.Serialize(validateIdentityCard)
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("validateIdentityCard").ToString()
        Dim jsonResult As String = rest.ConnectionPost(userID, iv_ticket1, jsonBODY)

        If (rest.IsError = False) Then
            Dim IneResponse As IneResponse = serializer.Deserialize(Of IneResponse)(jsonResult)
            Dim porcent As Integer = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("codePawPrint").ToString())

            autoriza.valido = save_INE(folio, jsonResult)
            autoriza.ruta = -1

            Dim porcentPrint As Integer = 0

            If imgFingerprint_.Hand = "IZQ" Then
                porcentPrint = Convert.ToInt32(IneResponse.pawPrint2)
            Else
                porcentPrint = Convert.ToInt32(IneResponse.pawPrint7)
            End If

            If autoriza.valido Then
                cancelFinger(_idSolicitud, _idPantalla)
                autoriza = getPagh(IneResponse, porcent, porcentPrint, autoriza.valido)
            Else
                autoriza.ruta = 0
            End If
        Else
            autoriza.valido = False
            autoriza.ruta = 0
        End If

        Return autoriza
    End Function

    Private Function info_INE(ByVal folio As String) As validateIdentityCard
        Dim validateIdentityCard As validateIdentityCard = New validateIdentityCard()

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            'get_Info_INE_SP @PDK_ID_SECCCERO
            '--EXEC crud_Biometrico_SP 3534, 0, 22, ''
            cmd.CommandText = "crud_Biometrico_SP"
            'cmd.CommandText = "crud_Biometrico_SP " + folio + ", 0, 22, ''"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Parameters.AddWithValue("@PDK_ID_TAREAS", "0")
            cmd.Parameters.AddWithValue("@OPCION", "22")
            cmd.Parameters.AddWithValue("@XML_INFO", "")
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                validateIdentityCard.identityCard.opticalCharacterRecognition = reader("opticalCharacterRecognition").ToString()
                validateIdentityCard.operationDate = reader("operationDate").ToString()
                validateIdentityCard.channelCode = reader("channelCode").ToString()
                validateIdentityCard.functionCode = reader("functionCode").ToString()
                validateIdentityCard.officeCode = reader("officeCode").ToString()

                Dim userCode As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                If (userCode.Contains(".")) Then
                    Dim spl As String() = userCode.Split(".")
                    userCode = spl(1)
                End If
                If (userCode.Length > 8) Then
                    userCode = userCode.Substring(userCode.Length - 8)
                End If
                validateIdentityCard.userCode = userCode

                validateIdentityCard.terminalCode = reader("terminalCode").ToString()
                validateIdentityCard.customer.person.name = reader("name").ToString()
                validateIdentityCard.customer.person.lastName = reader("lastName").ToString()
                validateIdentityCard.customer.person.mothersLastName = reader("mothersLastName").ToString()
            Loop

        Catch ex As Exception
            validateIdentityCard = New validateIdentityCard()
        End Try

        sqlConnection1.Close()
        Return validateIdentityCard
    End Function

    Private Function save_INE(ByVal folio As String, ByVal resp_INE As String) As Boolean
        Dim autoriza As Boolean = False

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            'set_Response_INE_SP @PDK_ID_SECCCERO INT, @payloadResponseINE VARCHAR(MAX), @OPCION INT = NULL, @PDK_ID_PANTALLA INT = NULL
            'NULL = 0
            'EXEC crud_Biometrico_SP 3534, 0, 24, '{"ocr":"true","mothersLastName":"true","lastName":"true","name":"true","pawPrint2":"0","codePawPrint2":91,"descripcionPawPrint2":"OCR Vigente","pawPrint7":"10000","codePawPrint7":91,"descripcionPawPrint7":"OCR Vigente","code":91,"descripcion":"OCR Vigente","yearRegistry":"false","numberEmision":"false","codeElector":"false","codeCurp":"false"}'
            'EXEC crud_Biometrico_SP 3534, 142, 24, ''
            cmd.CommandText = "crud_Biometrico_SP"
            'cmd.CommandText = "crud_Biometrico_SP " + folio + ", 0, 24, " + resp_INE
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Parameters.AddWithValue("@PDK_ID_TAREAS", "0")
            cmd.Parameters.AddWithValue("@OPCION", "24")
            cmd.Parameters.AddWithValue("@XML_INFO", resp_INE)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
            Loop
            autoriza = True
        Catch ex As Exception
            autoriza = False
        End Try

        sqlConnection1.Close()
        Return autoriza
    End Function

    Private Sub cancelFinger(ByRef NoSolicitud As Integer, ByVal IdPantalla As Integer, Optional ByVal message As String = "")
        Dim autoriza As Boolean = False

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "update_Spent_Attempts_INE_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", NoSolicitud.ToString())
            cmd.Parameters.AddWithValue("@PDK_ID_PANTALLA", IdPantalla.ToString())
            If (message <> "") Then
                cmd.Parameters.AddWithValue("@MESSAGE", message)
            End If
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
            Loop
            autoriza = True
        Catch ex As Exception
            autoriza = False
        End Try

        sqlConnection1.Close()
    End Sub

    Public Class respuesta
        Public valido As Boolean
        Public ruta As Integer
    End Class

    Public Sub CancelaTarea()
        Dim objCancela As New clsSolicitudes(0)
        objCancela.PDK_ID_SOLICITUD = Request("Sol").ToString()
        objCancela.Estatus = 42
        objCancela.PDK_CLAVE_USUARIO = Session("IdUsua")
        objCancela.PDK_ID_PANTALLA = Request("idPantalla").ToString()
        objCancela.Estatus_Cred = 295

        objCancela.ManejaTarea(6)
    End Sub

    Private Sub BeforeTask()
        _dtsResult = New DataSet()
        _clsSolicitudes = New clsSolicitudes(_idSolicitud)
        _clsManejaBD = New clsManejaBD()
        _clsSolicitudes.PDK_ID_SOLICITUD = _idSolicitud
        _clsSolicitudes.BOTON = 64
        _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
        _dtsResult = _clsManejaBD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & _idPantalla.ToString())
        If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO")
        Else
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = 0
        End If
        _clsSolicitudes.ValNegocio(1)
        checkRedirect()
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function FNGenerator(ByVal idPant As String, ByVal nSol As String, ByVal url As String, ByVal Browser As String) As String
        Dim FingerprintParams As FingerprintParams = New FingerprintParams()
        FingerprintParams.IdUsuario = CType(System.Web.HttpContext.Current.Session.Item("IdUsua"), String)
        FingerprintParams.IdPantalla = idPant
        FingerprintParams.NoSolicitud = nSol
        FingerprintParams.WebPage = url
        FingerprintParams.Browser = Browser

        Dim _dsResult As DataSet = New DataSet
        Dim _clsManejaBD As clsManejaBD = New clsManejaBD()
        _clsManejaBD.AgregaParametro("@PDK_ID_SECC_CERO", TipoDato.Entero, FingerprintParams.NoSolicitud, False)
        _clsManejaBD.AgregaParametro("@PDK_ID_PANTALLA", TipoDato.Entero, FingerprintParams.IdPantalla, False)
        '_clsManejaBD.AgregaParametro("@PDK_GUID_KEY", TipoDato.Cadena, FingerprintParams.Key, False)
        _dsResult = _clsManejaBD.EjecutaStoredProcedure("spManejaDatosHuella")

        Dim msgCode As String = String.Empty
        Dim msgTitle As String = String.Empty
        Dim msg As String = String.Empty
        Dim responseFNG As responseFNG = New responseFNG()

        If Not _dsResult Is Nothing And _dsResult.Tables.Count > 0 And _dsResult.Tables(0).Rows.Count > 0 Then
            If _dsResult.Tables(0).Rows(0)("MENSAJE").ToString() = String.Empty Then
                FingerprintParams.IdAutenticacion = Convert.ToInt64(_dsResult.Tables(0).Rows(0)("RESULTADO").ToString())
                FingerprintParams.Key = _dsResult.Tables(0).Rows(0)("GUID").ToString()

                msgCode = "OK"
                msgTitle = "En espera de información del lector"
                msg = "Verifique la descarga del archivo: " & FingerprintParams.Key & ".fng     Al terminar el procesamiento de la huella por el lector presione el botón Cerrar."

                responseFNG.PublicKey = FingerprintParams.Key

                Dim _fileContent As String = New JavaScriptSerializer().Serialize(FingerprintParams)
                _fileContent = New Cifrado().EncryptString(_fileContent, FingerprintParams.Key)

                responseFNG.informationFNG = _fileContent
            Else
                msgCode = "EF"
                msg = _dsResult.Tables(0).Rows(0)("MENSAJE").ToString()
            End If
        Else
            msgCode = "EC"
            msg = "Error de comunicaciones con la base de datos. Intentelo de nuevo más tarde."
        End If

        Dim datesFNG As datesFNG = New datesFNG()

        datesFNG.name = FingerprintParams.Key
        datesFNG.information = New JavaScriptSerializer().Serialize(responseFNG)
        datesFNG.messageCode = msgCode
        datesFNG.messageTitle = msgTitle
        datesFNG.message = msg

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Respuesta As String = serializer.Serialize(datesFNG)
        Return json_Respuesta
    End Function

    Public Class datesFNG
        Public name As String
        Public information As String
        Public messageCode As String
        Public messageTitle As String
        Public message As String
    End Class

    Public Class responseFNG
        Public PublicKey As String
        Public informationFNG As String
    End Class

    <System.Web.Services.WebMethod()> _
    Public Shared Function getInformationINE(ByVal nSol As String, ByVal idPant As String, ByVal key As String) As String
        Dim _dsResult As DataSet = New DataSet
        Dim _clsManejaBD As clsManejaBD = New clsManejaBD()
        _clsManejaBD.AgregaParametro("@PDK_ID_SECC_CERO", TipoDato.Entero, Int32.Parse(nSol), False)
        _clsManejaBD.AgregaParametro("@PDK_ID_PANTALLA", TipoDato.Entero, Int32.Parse(idPant), False)
        _clsManejaBD.AgregaParametro("@PDK_GUID_KEY", TipoDato.Cadena, key, False)
        _dsResult = _clsManejaBD.EjecutaStoredProcedure("get_HistoryAuthentication")

        Dim opStatus As String = String.Empty
        Dim _FingerprintStr As String = String.Empty
        Dim msg As String = String.Empty

        If Not _dsResult Is Nothing And _dsResult.Tables.Count > 0 And _dsResult.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(_dsResult.Tables(0).Rows(0)("PDK_FINGERPRINT")) Then
                _FingerprintStr = New Descifrado().DecryptString(_dsResult.Tables(0).Rows(0)("PDK_FINGERPRINT").ToString(), _dsResult.Tables(0).Rows(0)("PDK_GUID_KEY"))
                _FingerprintStr = "data:image/bmp;base64," + _FingerprintStr
                opStatus = "OK"
            End If
        End If

        msg = "No existe información asociada, procese el archivo " + key + ".fng y obtenga la imagen dactilar con el lector. Si ya ha procesado el archivo espere unos segundos y presione el botón Continuar"

        Dim FingerprintINE As FingerprintINE = New FingerprintINE()
        FingerprintINE.opStatus = opStatus
        FingerprintINE.Fingerprint = _FingerprintStr
        FingerprintINE.message = msg

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Respuesta As String = serializer.Serialize(FingerprintINE)
        Return json_Respuesta
    End Function

    Public Class FingerprintINE
        Public opStatus As String
        Public Fingerprint As String
        Public message As String
    End Class

    Private Function getIMG(ByVal idPant As String, ByVal nSol As String) As FingerInfo
        Dim FingerInfo As FingerInfo = New FingerInfo()
        Dim _guid As String = String.Empty

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            'set_Response_INE_SP @PDK_ID_SECCCERO INT, @payloadResponseINE VARCHAR(MAX), @OPCION INT = NULL, @PDK_ID_PANTALLA INT = NULL
            'NULL = 0
            'EXEC crud_Biometrico_SP 3534, 0, 24, '{"ocr":"true","mothersLastName":"true","lastName":"true","name":"true","pawPrint2":"0","codePawPrint2":91,"descripcionPawPrint2":"OCR Vigente","pawPrint7":"10000","codePawPrint7":91,"descripcionPawPrint7":"OCR Vigente","code":91,"descripcion":"OCR Vigente","yearRegistry":"false","numberEmision":"false","codeElector":"false","codeCurp":"false"}'
            'EXEC crud_Biometrico_SP 3534, 142, 24, ''
            cmd.CommandText = "crud_Biometrico_SP"
            'cmd.CommandText = "crud_Biometrico_SP " + nSol + ", " + idPant + ", 24, 'N/A'"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", nSol)
            cmd.Parameters.AddWithValue("@PDK_ID_TAREAS", idPant)
            cmd.Parameters.AddWithValue("@OPCION", "24")
            cmd.Parameters.AddWithValue("@XML_INFO", "N/A")
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                FingerInfo.FingerprintStr = reader("PDK_FINGERPRINT").ToString()
                _guid = reader("PDK_GUID_KEY").ToString()
                FingerInfo.Hand = reader("PDK_FINGERPRINT_POSITION").ToString()
            Loop

            'FingerInfo.FingerprintStr = New Descifrado().DecryptString(FingerInfo.FingerprintStr, _guid)
        Catch ex As Exception
            FingerInfo.FingerprintStr = "N/A"
        End Try

        sqlConnection1.Close()

        Return FingerInfo
    End Function

    Public Class FingerInfo
        Public FingerprintStr As String
        Public Hand As String
    End Class

    Private Function maxIntentos(ByVal nSol As String, ByVal idPant As String) As Boolean
        Dim _response As Boolean = True

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "spManejaDatosHuella"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECC_CERO", nSol.ToString())
            cmd.Parameters.AddWithValue("@PDK_ID_PANTALLA", idPant.ToString())

            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                If (Int32.Parse(reader("RESULTADO").ToString()) <> -1) Then
                    _response = False
                End If
            Loop
        Catch ex As Exception
            _response = True
        End Try

        sqlConnection1.Close()

        Return _response
    End Function

    Private Function getPagh(ByVal ineResponse As IneResponse, ByVal porcentMin As Integer, ByVal porcentPrintINE As Integer, ByVal saveBD As Boolean) As respuesta
        Dim respuesta As respuesta = New respuesta()
        respuesta.valido = saveBD
        respuesta.ruta = 1732

        If (saveBD) Then
            Dim fails As Integer = 0
            If ineResponse.name.ToUpper() <> "FALSE" Then
                fails = fails + 1
            End If
            If ineResponse.lastName.ToUpper() <> "FALSE" Then
                fails = fails + 1
            End If
            If ineResponse.mothersLastName.ToUpper() <> "FALSE" Then
                fails = fails + 1
            End If

            If (porcentMin > porcentPrintINE) Then
                respuesta.valido = False
            End If

            If (((fails = 3) And (ineResponse.ocr.ToUpper() = "TRUE") And (respuesta.valido)) = False) Then
                If (ineResponse.code = 92) Then
                    respuesta.ruta = 3
                ElseIf ((fails <= 2) And (ineResponse.ocr.ToUpper() = "TRUE") And (respuesta.valido = False)) Then
                    respuesta.ruta = 1
                ElseIf ((fails = 0) And (ineResponse.ocr.ToUpper() = "TRUE")) Then
                    respuesta.ruta = 1
                ElseIf ((fails = 3) And (ineResponse.ocr.ToUpper() = "TRUE") And (respuesta.valido = False)) Then
                    respuesta.ruta = 3
                ElseIf ((fails > 0) And (ineResponse.ocr.ToUpper() = "FALSE")) Then
                    respuesta.ruta = 2
                ElseIf ((fails <= 2) And (ineResponse.ocr.ToUpper() = "TRUE") And (respuesta.valido)) Then
                    respuesta.ruta = 1
                Else
                    respuesta.ruta = 2
                End If

                respuesta.valido = False
            Else
                cancelFinger(_idSolicitud, _idPantalla, "OK")
            End If
        Else
            respuesta.ruta = 0
        End If

        Return respuesta
    End Function

    Private Function getMarcaPreca(ByVal folio As Integer) As marcaPreca
        Dim marcaPreca As marcaPreca = New marcaPreca()
        marcaPreca.cambioPreca = False
        marcaPreca.id_INE = False

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_Marca_Preca_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                marcaPreca.cambioPreca = reader("MARCA_PRECA").ToString()
                marcaPreca.id_INE = reader("ID").ToString()
                marcaPreca.fakeAuth = reader("FAKE").ToString()
            Loop

        Catch ex As Exception
            marcaPreca.cambioPreca = False
            marcaPreca.id_INE = False
        End Try

        sqlConnection1.Close()
        Return marcaPreca
    End Function

    Public Class marcaPreca
        Public cambioPreca As Boolean
        Public id_INE As Boolean
        Public fakeAuth As Boolean
    End Class

    Private Function executeQuerys(ByVal query As StringBuilder) As String
        Dim respuestaQuery As String = String.Empty

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "exec_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@query", query.ToString())

            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()
            Do While reader.Read()
                respuestaQuery = reader(0).ToString()
            Loop
        Catch ex As Exception
        End Try
        sqlConnection1.Close()

        Return respuestaQuery
    End Function

    Private Function getPathCelula(ByVal folio As Integer, ByVal IDpant As Integer, Optional ByVal opc As Integer = 16) As pathCelula
        Dim pathCelula As pathCelula = New pathCelula()

        Dim _query As StringBuilder = New StringBuilder()
        _query.Append("EXEC crud_Biometrico_SP " + folio.ToString() + ", " + IDpant.ToString() + ", 15, ''")
        Dim pantalla_valida As Integer = Int32.Parse(executeQuerys(_query))

        If pantalla_valida = 1 Then
            pathCelula = verifyINEResponse(opc)
        End If

        Return pathCelula
    End Function

    Public Class pathCelula
        Public path As Integer = 0
        Public celula As Boolean = False
        Public message As String = String.Empty
    End Class

    Private Function verifyINEResponse(Optional ByVal opc As Integer = 16) As pathCelula
        Dim Query As StringBuilder = New StringBuilder()
        Query.Append("EXEC crud_Biometrico_SP " + _idSolicitud.ToString() + ", " + _idPantalla.ToString() + ", 10, 'aa'")
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim IneResponse As IneResponse = serializer.Deserialize(Of IneResponse)(executeQuerys(Query))

        Dim respuesta As pathCelula = New pathCelula()
        Dim porcentMin As Integer = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("codePawPrint").ToString())
        Dim porcentPrintINE As Integer = 0

        If IneResponse.pawPrint2 <> "0" Then
            porcentPrintINE = Convert.ToInt32(IneResponse.pawPrint2)
        Else
            porcentPrintINE = Convert.ToInt32(IneResponse.pawPrint7)
        End If

        Dim fails As Integer = 0
        If IneResponse.name.ToUpper() <> "FALSE" Then
            fails = fails + 1
        End If
        If IneResponse.lastName.ToUpper() <> "FALSE" Then
            fails = fails + 1
        End If
        If IneResponse.mothersLastName.ToUpper() <> "FALSE" Then
            fails = fails + 1
        End If

        Dim huella As Boolean = True
        If (porcentMin > porcentPrintINE) Then
            huella = False
        End If

        If (((fails = 3) And (IneResponse.ocr.ToUpper() = "TRUE") And (huella)) = False) Then
            Dim INE_Inputs As INE_Inputs = New INE_Inputs()
            INE_Inputs.code = IneResponse.code
            INE_Inputs.fails = fails

            Dim huella_ As Integer = 0
            If (huella) Then
                huella_ = 1
            End If

            Dim OCR_ As Integer = 0
            If (IneResponse.ocr.ToUpper() = "TRUE") Then
                OCR_ = 1
            End If

            INE_Inputs.FingerPrint = huella_
            INE_Inputs.OCR = OCR_

            Dim XmlSerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(INE_Inputs.GetType)
            Dim xmlINE As StringWriter = New StringWriter()
            XmlSerializer.Serialize(xmlINE, INE_Inputs)

            Dim Query_ As StringBuilder = New StringBuilder()
            Query_.Append("EXEC crud_Biometrico_SP " + _idSolicitud.ToString() + ", " + _idPantalla.ToString() + ", " + opc.ToString() + ",'" + xmlINE.ToString() + "'")
            Dim jsonEvalINE As String = executeQuerys(Query_)

            Dim INE_EVAL As INE_EVAL = serializer.Deserialize(Of INE_EVAL)(jsonEvalINE)
            respuesta.path = Int32.Parse(INE_EVAL.PATH)
            respuesta.message = INE_EVAL.MSG
            respuesta.celula = True
        End If

        Return respuesta
    End Function

    Public Class INE_Inputs
        Public fails As Integer
        Public OCR As Integer
        Public FingerPrint As Integer
        Public code As Integer
    End Class

    Public Class INE_EVAL
        Public PATH As String
        Public MSG As String
    End Class

    <System.Web.Services.WebMethod()> _
    Public Shared Function saveFinger(ByVal WQS As String, ByVal IMG As String, ByVal idPant As String, ByVal nSol As String) As String
        Dim respuestaQuery As String = String.Empty

        Dim INE_Inputs As saveFingernew = New saveFingernew()
        INE_Inputs.WSQ = WQS
        INE_Inputs.IMG = IMG

        Dim XmlSerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(INE_Inputs.GetType)
        Dim xmlINE As StringWriter = New StringWriter()
        XmlSerializer.Serialize(xmlINE, INE_Inputs)

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "crud_Biometrico_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", nSol)
            cmd.Parameters.AddWithValue("@PDK_ID_TAREAS", idPant)
            cmd.Parameters.AddWithValue("@OPCION", "20")
            cmd.Parameters.AddWithValue("@XML_INFO", xmlINE.ToString())

            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()
            Do While reader.Read()
                respuestaQuery = reader(0).ToString()
            Loop
        Catch ex As Exception
        End Try
        sqlConnection1.Close()

        Return respuestaQuery
    End Function

    Public Class saveFingernew
        Public IMG As String = String.Empty
        Public WSQ As String = String.Empty
    End Class
End Class