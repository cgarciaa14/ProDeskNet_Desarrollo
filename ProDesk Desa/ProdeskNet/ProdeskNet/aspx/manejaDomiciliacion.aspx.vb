'BBV-P-423: RQADM-23: AVH: 28/03/2017 Se crea pantalla
'BBVA-P-423: RQCAINTBAC-02: CGARCIA: 24/04/2017 se crearon eventos para la clave interbancaria y para los folios fiscales 
'BUG-PD-49:MPUESTO:10/05/2017:Adición de redirect dinámico 
'BUG-PD-63:ERODRIGUEZ:26/05/2017: Modificación para permitir mostrar cuando ya se proceso nombre y banco relacionado con cuenta bancaria
'RQCAINTBAC-03: ERODRIGUEZ:05/06/2017 requerimiento de validación de CFDI
'BUG-PD-75: ERODRIGUEZ 06/06/2017 cambios en la validacion
'BUG-PD-79 : ERODRIGUEZ 08/06/2017 : se agrego funcion CargaDatosSolicitanteTodos para la validacion de que el RFC del cleinte coincida con el RFC de la cuenta
'BUG-PD-85 : ERODRIGUEZ 10/06/2017 : se agrego vañidacion para homoclave
'BUG-PD-89 : ERODRIGUEZ 10/06/2017 : se limito el número de cuenta a solo diez digitos y se añadieron 10 ceros al inicio para validar con el servicio
'BUG-PD-91 : ERODRIGUEZ 12/06/2017 : Se cambio el orden de los campos, se limpian los datos de cfdi al cambiar de banco seleccionado, se valido para que Banorte e IXE no validaran cfdi
'BUG-PD-96 : ERODRIGUEZ 15/06/2017 : Se habilitaron guiones automaticos en cfdi y se permitio no validar cfdi para todos los bancos
'BUG-PD-137: ERODRIGUEZ: 03/07/2017: Se cambió validación de clabe interbancaria se validó para permitir 20 números en clabe interbancaria
'BUG-PD-203: CGARCIIA: 04/09/2017: SE AGREGA NOTIFICACIONES 
'BUG-PD-215: CGARCIA: 26/09/2017: ACTUALIZACION DE CORREOS PARA CASOS AL QUE ENTRA.
'BUG-PD-218: CGARCIA: 29/09/2017: ACTUALIZACION DE EMAIL
'BUG-PD-227: CGARCIA: 05/10/2017: ACTUALIZACION DE EMAILS Y SCORE
'BUG-PD-251: ERODRIGUEZ: 27/10/2017: Se pone como default Bancomer como banco seleccionado.	
'BUG-PD-272: MGARCIA: 23/11/2017: Se agrego pantalla DetalleImpagos y su funcionalidad
'BUG-PD-276: CGARCIA: 27/11/2017: CORRECCION DE MSJ Y REQUEST DE USUARIOS EN EMAILS
'BUG-PD-304: ERORIGUEZ: 13/12/2017: Cuando el RFC del servicio no trae homoclave se compara sin homoclave con el rfc registrado.
'RQ-PD18: CGARCIA: 13/12/2017: SE AGREGA NUMERO DE CLIENTE DESDE WS Y BD EN ENCABEZADO DE INFO 
'BUG-PD-307: DCORNEJO: 14/12/2017: CORRECION AL REDIRECCIONAR A VALIDA EMAILS
'BUG-PD-325: CGARCIA: 03/01/2018: SE AGREGA CONSULTA DEL NUMERO DE CLIENTE DEL WS CON SEGUROS BANCOMER
'BUG-PD-328: CGARCIA: 05/01/2017: SE AGREGA COLUMNA PARA GUARDAR EL NUMERO DE CLIENTE QUE AROJA EL WS DE LIGUE DE CUENTA
'BUG-PD-370: ERODRIGUEZ: 26/02/2018: Se cambio validación de RFC para que se compare sin homoclave, Se agrego validacion para no permitir procesar sin número de cliente.
'BUG-PD-386  GVARGAS 14/03/2018  Correccion flujo 
'BUG-PD-417  DCORNEJO 17/04/2018: SE AGREGO LA OPCION DE TURNAR A: COTIZACION ANALISTA

Imports System.Data
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Configurcion
Imports ProdeskNet.SN
Imports ProdeskNet.BD

Partial Class aspx_manejaDomiciliacion
    Inherits System.Web.UI.Page
    Dim es As New ProdeskNet.Catalogos.clsStatusSolicitud
    Dim dc As New ProdeskNet.Catalogos.clsDatosCliente
    Dim BD As New clsManejaBD
    Public ClsEmail As New clsEmailAuto()
    Dim usu As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.lblSolicitud.Text = Request.QueryString("sol")

        Dim intNumCliente As Integer = CInt(Request.QueryString("sol"))

        es.getStatusSol(Request.QueryString("sol"))
        Me.lblStCredito.Text = es.PStCredito
        Me.lblStDocumento.Text = es.PStDocumento

        dc.GetDatosCliente(lblSolicitud.Text)
        lblCliente.Text = dc.propNombreCompleto
        'RQ-PD18: CGARCIA: 13/12/2017: SE AGREGA NUMERO DE CLIENTE DESDE WS Y BD EN ENCABEZADO DE INFO 
        dc.getNumCliente(7, intNumCliente)
        lblNumCliente.Text = dc.PropNumCliente

        Dim dsresult As New DataSet
        Dim dsresulta As New DataSet
        hdPantalla.Value = Request("idPantalla")
        hdSolicitud.Value = Request("sol")
        hdusuario.Value = Request("usu")

        usu = Val(Request("usuario"))
        If usu = String.Empty Then
            usu = Val(Request("usu"))
        End If

        Dim intEnable As Integer
        intEnable = CInt(Request.QueryString("Enable"))
        ViewState("vwsIntEnable") = intEnable
        If intEnable = 1 Then

            btnProcesarCliente.Attributes.Add("style", "display:none;")
            btnCancelar.Attributes.Add("style", "display:none;")

        End If
        If Session("Regresar") Is Nothing Then
            Session("Regresar") = Request.UrlReferrer.LocalPath
        End If

        'btnProcesarCliente.Disabled = True
        hdRutaEntrada.Value = Session("Regresar")

        Try
            dsresulta = BD.EjecutarQuery("get_Path_Next_Tarea  " & hdPantalla.Value)
            If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
                Dim Mostrar As String
                Dim pantallas As String
                Mostrar = dsresulta.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR").ToString
                pantallas = dsresulta.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString
                If Mostrar = 2 Then

                    hdnResultado.Value = dsresult.Tables(0).Rows(0).Item("RUTA")
                Else
                    hdnResultado.Value = (dsresulta.Tables(0).Rows(0).Item("RUTA") & "?sol=" & hdSolicitud.Value & "&IdPantalla=" & pantallas & "&usuario=" & hdusuario.Value)
                    hdnResultado2.Value = (dsresulta.Tables(0).Rows(0).Item("RUTA") & "?sol=" & hdSolicitud.Value & "&IdPantalla=" & pantallas & "&usuario=" & hdusuario.Value)
                End If
            End If

        Catch ex As Exception
            hdnResultado.Value = Session("Regresar")
        End Try

        If Not IsPostBack Then
            If (Request("pantalla").ToString = "92") And intEnable = 0 Then
                'divActualizaColonia.Attributes.Add("style", "display:''")
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
                CargaCombos(0)
                ddlTipoCuenta_SelectedIndexChanged(sender, e)
                hdnResultado1.Value = 0
                hdnResultado2.Value = 0
                hdnResultado3.Value = 0
                CargaDatos()

            End If
        End If
        ValidaDigitosBancarios()
        'lblNumCliente.Text = ""
        If lblNumCliente.Text = "" And lblNumCliente.Text.Length < 1 Then
            hdnResultado3.Value = 0
        Else
            hdnResultado3.Value = 1
        End If
    End Sub
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        RegresaPantalla()
    End Sub
    Public Sub RegresaPantalla()
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        'Response.Redirect(Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub

    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs)
        Dim msj As String = ""
        Dim intValueBanco_1 = "0" + Format(12, "0#")
        Dim intValueBanco_2 = "0" + Format(17, "0#")

        'para Banorte e Ixe
        Dim intValueBanco_3 = "0" + Format(72, "0#")
        Dim intValueBanco_4 = "0" + Format(32, "0#")

        If ddlBanco.SelectedValue = intValueBanco_3 Or ddlBanco.SelectedValue = intValueBanco_4 Then
                hdnResultado1.Value = 1
                ValidaDigitosBancarios()
            End If

            If ddlBanco.SelectedValue = intValueBanco_1 Or ddlBanco.SelectedValue = intValueBanco_2 Then
                If (ddlTipoCuenta.SelectedValue = 284) Then
                    If (txtNumeroTarjeta.Text <> "") Then
                        hdnResultado2.Value = 1
                    Else
                        hdnResultado2.Value = 0
                    End If

                End If
                If (ddlTipoCuenta.SelectedValue = 283) Then
                    If (LblRFC.Text <> "") Then
                        If (HiddenRFCCompleto.Value = "") Then
                            HiddenRFCCompleto.Value = ObtenRFCcte(1)
                        End If

                        Dim rfcg_len As Integer = Len(HiddenRFCCompleto.Value)
                        Dim rfcs_len As Integer = Len(LblRFC.Text)

                        'If rfcs_len = 10 Then  'BUG-PD-304: ERORIGUEZ: 13/12/2017: Cuando el RFC del servicio no trae homoclave se compara sin homoclave con el rfc registrado.
                        '    If rfcg_len >= 10 Then
                        '        HiddenRFCCompleto.Value = HiddenRFCCompleto.Value.ToString().Substring(0, 10)
                        '        rfcg_len = Len(HiddenRFCCompleto.Value)
                        '    End If
                        'End If



                        If (rfcg_len >= 10 And rfcs_len >= 10) Then
                            LblRFC.Text = LblRFC.Text.Substring(0, 10)
                            HiddenRFCCompleto.Value = HiddenRFCCompleto.Value.Substring(0, 10)
                            If (rfcg_len = rfcs_len) Then
                                If (LblRFC.Text = HiddenRFCCompleto.Value.ToString()) Then
                                    hdnResultado2.Value = 1
                                Else
                                    msj = ", no coincide RFC"
                                End If
                            Else
                                If (rfcs_len > rfcg_len) Then
                                    Dim new_rfcgs As String = Left(LblRFC.Text, rfcs_len - 3)
                                    If (HiddenRFCCompleto.Value = new_rfcgs) Then
                                        hdnResultado2.Value = 1
                                    Else
                                        msj = ", no coincide RFC"
                                    End If

                                End If
                            End If

                        End If

                    End If
                End If
            End If

            'Modificacion temporal para permitir que ninguna banco requiera CFDI, favor de borrar cuando esto, ya no sea requerido.
            hdnResultado1.Value = 1
        'Fin de validacion temporal
        If (hdnResultado3.Value = 1) Then
            If (hdnResultado1.Value = 1 And hdnResultado2.Value = 1) Then


                If ddlTipoCuenta.SelectedValue <> 0 Then
                    If ddlBanco.SelectedValue <> 0 Then
                    Else
                        Master.MensajeError("Debe seleccionar algun Banco")
                        btnProcesarCliente.Disabled = False
                    End If

                    If ValidaCombo() Then
                        btnProcesarCliente.Attributes.Add("Disabled", True)
                        If (hdnResultado1.Value = 1 And hdnResultado2.Value = 1) Then
                            GuardaDatos()
                            btnProcesarCliente.Disabled = True
                        Else
                            If (hdnResultado1.Value = 0) Then
                                Master.MensajeError("Debe de proporcionar los datos para validar CFDI")
                            End If
                            If (hdnResultado2.Value = 0) Then
                                Master.MensajeError("Debe de proporcionar los datos de la cuenta" + msj)
                            End If

                            btnProcesarCliente.Disabled = False
                        End If
                    End If
                Else
                    Master.MensajeError("Debe seleccionar Tipo de Cuenta")
                    btnProcesarCliente.Disabled = False
                End If

            ElseIf (hdnResultado1.Value = 0) Then
                Master.MensajeError("Debe de llenar y validar los datos de CFDI")
                btnProcesarCliente.Disabled = False
            ElseIf (hdnResultado2.Value = 0 AndAlso ddlTurnar.SelectedValue = 0) Then 'BUG-PD - 417
                Master.MensajeError("Debe de verificar los datos bancarios" + msj)
                btnProcesarCliente.Disabled = False
            ElseIf (ddlTurnar.SelectedValue = 88) Then 'BUG-PD - 417
                asignaTarea(ddlTurnar.SelectedValue)
            End If
        ElseIf (hdnResultado3.Value = 0) Then
            Master.MensajeError("Número de cliente no encontrado" + msj)
            btnProcesarCliente.Disabled = False
        End If

    End Sub

    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("sol")))
        Dim mensaje As String = String.Empty
        Try

            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("sol"))
            Solicitudes.PDK_CLAVE_USUARIO = Request("usu")
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")

            If idAsignarPantalla <> 0 Then
                Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla
            End If

            If mensaje <> "" Then
                Master.MensajeError(mensaje)

            Else
                dsresult = Solicitudes.ValNegocio(1)
                mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
                Solicitudes.MENSAJE = mensaje
                Solicitudes.ManejaTarea(5)

                If mensaje <> "Tarea Exitosa" And mensaje <> "SE RECHAZO  DOCUMENTO " And mensaje <> "TAREA EXITOSA" Then
                    Throw New Exception(mensaje)
                End If

                dslink = objtarea.SiguienteTarea(Val(Request("sol")))

                muestrapant = objpantalla.SiguientePantalla(Val(Request("sol")))
                Dim dc As New clsDatosCliente
                dc.idSolicitud = Request("sol")
                dc.getDatosSol()

                If muestrapant = 0 Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString & "');", True)
                ElseIf muestrapant = 2 Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "&sol=" & Val(Request("Sol")).ToString & "');", True)
                    'Response.Redirect("../aspx/consultaPanelControl.aspx")
                End If
            End If

        Catch ex As Exception

            btnRegresar.Attributes.Remove("disabled")

            Master.MensajeError(mensaje)
        End Try
    End Sub

    Public Sub CargaCombos(opc As Integer)
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
        Dim objCombo As New clsParametros
        Dim dsBanco As New DataSet
        ''BANCO


        ''TIPO DE CUENTA
        Dim dsTipoCuenta As DataSet

        If (opc = 0) Then
            objCatalogos.Parametro = 544
            dsBanco = objCatalogos.Catalogos(8)

            If dsBanco.Tables.Count > 0 And dsBanco.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dsBanco, "PDK_SCORING_VALOR1", "PDK_SCORING_VALOR2", ddlBanco, True, True)
            End If


            objCatalogos.Parametro = 282
            dsTipoCuenta = objCatalogos.Catalogos(3)
            ddlTipoCuenta.Items.Clear()
            If dsTipoCuenta.Tables.Count > 0 And dsTipoCuenta.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dsTipoCuenta, "PDK_PAR_SIS_PARAMETRO", "PDK_ID_PARAMETROS_SISTEMA", ddlTipoCuenta, True, True)
            End If
        ElseIf (opc = 1) Then
            objCatalogos.Parametro = 282
            dsTipoCuenta = objCatalogos.Catalogos(13)
            ddlTipoCuenta.Items.Clear()
            If dsTipoCuenta.Tables.Count > 0 And dsTipoCuenta.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dsTipoCuenta, "PDK_PAR_SIS_PARAMETRO", "PDK_ID_PARAMETROS_SISTEMA", ddlTipoCuenta, True, True)
            End If
        ElseIf (opc = 2) Then
            objCatalogos.Parametro = 282
            dsTipoCuenta = objCatalogos.Catalogos(14)
            ddlTipoCuenta.Items.Clear()
            If dsTipoCuenta.Tables.Count > 0 And dsTipoCuenta.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dsTipoCuenta, "PDK_PAR_SIS_PARAMETRO", "PDK_ID_PARAMETROS_SISTEMA", ddlTipoCuenta, True, True)
            End If
        End If

    End Sub

    Protected Sub ddlTipoCuenta_SelectedIndexChanged(sender As Object, e As EventArgs)

        If ddlTipoCuenta.SelectedValue = 283 Then
            Me.txtNumeroCuenta.Enabled = True
            Me.txtNumeroTarjeta.Enabled = False
            Me.txtClaveInterbancaria.Enabled = False

        ElseIf ddlTipoCuenta.SelectedValue = 284 Then
            Me.txtNumeroCuenta.Enabled = False
            Me.txtNumeroTarjeta.Enabled = True
            Me.txtClaveInterbancaria.Enabled = False

        ElseIf ddlTipoCuenta.SelectedValue = 285 Then
            Me.txtNumeroCuenta.Enabled = False
            Me.txtNumeroTarjeta.Enabled = False
            Me.txtClaveInterbancaria.Enabled = True

        ElseIf ddlTipoCuenta.SelectedValue = 0 Then
            Me.txtNumeroCuenta.Enabled = False
            Me.txtNumeroTarjeta.Enabled = False
            Me.txtClaveInterbancaria.Enabled = False

        End If

        LimpiaCampos()

        Dim intValueBanco_1 = "0" + Format(12, "0#")
        Dim intValueBanco_2 = "0" + Format(17, "0#")

        If ddlBanco.SelectedValue = intValueBanco_1 And (ddlTipoCuenta.SelectedValue = 283) Then
            Me.btnLigarCuenta.Visible = True
            trTitular.Visible = True
            lblTitular.Visible = True
            trRFC.Visible = True
            Lbl1.Visible = True
            lblNumClienteWS.Visible = True
            txtNumClienteWS.Visible = True
        ElseIf ddlBanco.SelectedValue = intValueBanco_2 And (ddlTipoCuenta.SelectedValue = 283) Then
            Me.btnLigarCuenta.Visible = True
            trTitular.Visible = True
            lblTitular.Visible = True
            trRFC.Visible = True
            Lbl1.Visible = True
            lblNumClienteWS.Visible = True
            txtNumClienteWS.Visible = True
        Else
            Me.btnLigarCuenta.Visible = False
            trTitular.Visible = False
            lblTitular.Visible = False
            trRFC.Visible = False
            Lbl1.Visible = False
            lblNumClienteWS.Visible = False
            txtNumClienteWS.Visible = False
        End If


        'ddlBanco_SelectedIndexChanged(sender, e)

    End Sub
    Public Sub LimpiaCampos()
        Me.txtClaveInterbancaria.Text = ""
        Me.txtNumeroCuenta.Text = ""
        Me.txtNumeroTarjeta.Text = ""
    End Sub
    Public Function ValidaCombo() As Boolean
        ValidaCombo = False

        If ddlTipoCuenta.SelectedValue = 283 Then
            If txtNumeroCuenta.Text = "" Then
                Master.MensajeError("El campo Número de Cuenta está vacío")
                Me.txtNumeroCuenta.Focus()
                Exit Function
            ElseIf (Len(txtNumeroCuenta.Text) <> 10) Then
                Master.MensajeError("El número de digitos de la cuenta no es correcto")
                Me.txtNumeroCuenta.Focus()
                Exit Function
            End If
        ElseIf ddlTipoCuenta.SelectedValue = 284 Then
            If txtNumeroTarjeta.Text = "" Then
                Master.MensajeError("El campo Tarjeta de Débito está vacío")
                txtNumeroTarjeta.Focus()
                Exit Function
            End If
        ElseIf ddlTipoCuenta.SelectedValue = 285 Then
            If txtClaveInterbancaria.Text = "" Then
                Master.MensajeError("El campo Clave Interbancaria está vacío ")
                txtClaveInterbancaria.Focus()
                Exit Function
            ElseIf (Len(txtClaveInterbancaria.Text) < 4) Then
                Master.MensajeError("El número de digitos de la clave interbancaria no es correcto")
                txtClaveInterbancaria.Focus()
                Exit Function
            End If
        End If

        hdnResultado2.Value = 1
        ValidaCombo = True
    End Function

    Public Sub GuardaDatos()
        'BUG-PD-328: CGARCIA: 05/01/2017: SE AGREGA COLUMNA PARA GUARDAR EL NUMERO DE CLIENTE QUE AROJA EL WS DE LIGUE DE CUENTA
        Dim objDom As New clsDomiciliacion
        objDom.Solicitud = Me.lblSolicitud.Text
        'objDom.FolioFiscal = Me.txtFolioFiscal.Text
        objDom.FolioFiscal = Me.txtFolioFiscal.Value
        objDom.TipoCuenta = ddlTipoCuenta.SelectedValue
        objDom.Banco = ddlBanco.SelectedValue
        objDom.Titular = lblTitularCuenta.Text
        objDom.Clabe = txtClaveInterbancaria.Text
        objDom.NumeroTarjeta = txtNumeroTarjeta.Text
        objDom.NumeroCuenta = txtNumeroCuenta.Text
        objDom.Usuario = Session("IdUsua")
        objDom.NumClienteWS = txtNumClienteWS.Text
        objDom.Domiciliacion(2)

        Dim objFlujos As New clsSolicitudes(0)
        Dim boton As Integer = 64
        Dim ds As DataSet
        Dim mensaje As String = "Error al procesar la Tarea"

        objFlujos.PDK_ID_SOLICITUD = lblSolicitud.Text
        objFlujos.BOTON = boton
        objFlujos.PDK_CLAVE_USUARIO = Session("IdUsua")

        ds = objFlujos.ValNegocio(1)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                mensaje = ds.Tables(0).Rows(0).Item("MENSAJE").ToString

            End If
        End If

        If objFlujos.ERROR_SOL <> "" Then
            Master.MensajeError(objFlujos.ERROR_SOL)
            Exit Sub
        Else

            'BUG-PD-49:MPUESTO:10/05/2017:Adición de redirect dinámico utilizando requerimiento de selección de clientes
            checkRedirect(mensaje)
        End If
    End Sub

    'BUG-PD-49:MPUESTO:10/05/2017:Adición de redirect dinámico utilizando requerimiento de selección de clientes
    Protected Sub checkRedirect(mensaje As String)
        Dim _dtsResult As New DataSet()
        Dim _clsCatTareas As New ProdeskNet.SN.clsCatTareas()
        Dim _clsPantallas As New ProdeskNet.SN.clsPantallas()
        Dim _mostrarPantalla As Integer = 0
        _dtsResult = _clsCatTareas.SiguienteTarea(Val(Request("Sol")))
        _mostrarPantalla = _clsPantallas.SiguientePantalla(Val(Request("Sol")))

        If _mostrarPantalla = 0 Then
            'Response.Redirect("../aspx/" & _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
            Dim dc As New clsDatosCliente
            dc.idSolicitud = Val(Request("sol"))
            dc.getDatosSol()
            ClsEmail.OPCION = 17
            ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

            Dim dtConsulta = New DataSet()
            dtConsulta = ClsEmail.ConsultaStatusNotificacion
            If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
                If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                    If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
                        If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
                            Dim strLocation As String = String.Empty
                            strLocation = ("../aspx/ValidaEmails.aspx?idPantalla=92&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=" & _mostrarPantalla.ToString() & "&usuario=" & usu)
                            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '" + strLocation + "');", True)
                        Else
                            Dim str As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                        End If
                    Else
                        Dim str As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                        ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                    End If
                Else
                    Dim str As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                End If
            Else
                Dim str As String = "../aspx" & _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & usu
                ScriptManager.RegisterStartupScript(Page, GetType(String), "RedireccionaPagina", "window.location = '" & str & "';", True)
                'Response.Redirect("../aspx/" & _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
            End If
        ElseIf _mostrarPantalla = 2 Then
            Dim str_ As String = ""
            'ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" & mensaje & "', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
            'ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Exitosa.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
            'Response.Redirect("../aspx/consultaPanelControl.aspx")
            Dim dc As New clsDatosCliente
            dc.idSolicitud = Val(Request("sol"))
            dc.getDatosSol()
            ClsEmail.OPCION = 17
            ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

            Dim dtConsulta = New DataSet()
            dtConsulta = ClsEmail.ConsultaStatusNotificacion
            If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
                If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                    If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
                        If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
                            Dim strLocation As String = String.Empty
                            strLocation = ("../aspx/ValidaEmails.aspx?idPantalla=92&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=2" & "&usuario=" & usu)
                            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '" + strLocation + "');", True)
                        Else
                            Dim str As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                        End If
                    Else
                        Dim str As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                        ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                    End If
                Else
                    Dim str As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                End If
            Else
                Dim str As String = "../aspx" & _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & usu
                ScriptManager.RegisterStartupScript(Page, GetType(String), "RedireccionaPagina", "window.location = '" & str & "';", True)
                'Response.Redirect("../aspx/" & _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
            End If
        End If
    End Sub

    Protected Sub btnLigarCuenta_Click(sender As Object, e As EventArgs)
        Me.lblTitularCuenta.Text = ""
        Dim NumeroCuentas As String = ""
        If ValidaCombo() Then

            If ddlTipoCuenta.SelectedValue = 283 Then
                If (Len(txtNumeroCuenta.Text) = 10) Then
                    NumeroCuentas = "0000000000" + txtNumeroCuenta.Text
                End If

            ElseIf ddlTipoCuenta.SelectedValue = 284 Then
                NumeroCuentas = txtNumeroTarjeta.Text
            End If


            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
            rest.buscarHeader("ResponseWarningDescription")

            rest.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("Customer") + "?$filter=(accountNumber==" + NumeroCuentas + ")"
            Dim respuesta As String = rest.ConnectionGet(userID, iv_ticket1, String.Empty)

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim jresul2 As Customer = serializer.Deserialize(Of Customer)(respuesta)



            Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim alert = srrSerialer.Deserialize(Of msjerr)(respuesta)

            If rest.IsError Then
                If rest.MensajeError <> "" Then
                    Master.MensajeError("Error WS - " & rest.MensajeError)
                Else
                    Master.MensajeError("Error WS - " & alert.message & " Estatus: " & alert.status & "." & "Mensaje:" & rest.MensajeError)
                End If
                Exit Sub
            Else
                If rest.valorHeader <> "" And jresul2.Person.id Is Nothing Then
                    Master.MensajeError("Error WS - " & rest.valorHeader)
                Else
                    Me.lblTitularCuenta.Text = jresul2.Person.name + " " + jresul2.Person.lastName + " " + jresul2.Person.mothersLastName
                    Me.LblRFC.Text = jresul2.Person.identityDocument(1).number.ToString()
                    'RQ-PD18: CGARCIA: 13/12/2017: SE AGREGA NUMERO DE CLIENTE DESDE WS Y BD EN ENCABEZADO DE INFO 
                    Me.txtNumClienteWS.Text = jresul2.Person.id.ToString
                    trRFC.Visible = True
                    LblRFC.Visible = True
                    Master.MensajeError(" Validación realizada correctamente ")

                End If
            End If
        End If

    End Sub

    Protected Sub ddlBanco_SelectedIndexChanged(sender As Object, e As EventArgs)
        'CargaCombos()
        'para Bancomer o BBVA Bancomer
        Dim intValueBanco_1 = "0" + Format(12, "0#")
        Dim intValueBanco_2 = "0" + Format(17, "0#")
        'para Banorte e Ixe
        Dim intValueBanco_3 = "0" + Format(72, "0#")
        Dim intValueBanco_4 = "0" + Format(32, "0#")

        hdnResultado2.Value = 0
        hdnResultado1.Value = 0

        LimpiaDatosCFDI()
        If ddlBanco.SelectedValue = intValueBanco_3 Or ddlBanco.SelectedValue = intValueBanco_4 Then
            hdnResultado1.Value = 1
        End If

        If ddlBanco.SelectedValue = intValueBanco_1 Or ddlBanco.SelectedValue = intValueBanco_2 Then
            hdnResultado2.Value = 1
            hdnResultado1.Value = 1
            CargaCombos(1)

        Else
            CargaCombos(2)

        End If

        If ddlBanco.SelectedValue = intValueBanco_1 And (ddlTipoCuenta.SelectedValue = 283) Then
            Me.btnLigarCuenta.Visible = True
            trTitular.Visible = True
            lblTitular.Visible = True
            trRFC.Visible = True
            Lbl1.Visible = True
            lblNumClienteWS.Visible = True
            txtNumClienteWS.Visible = True
        ElseIf ddlBanco.SelectedValue = intValueBanco_2 And (ddlTipoCuenta.SelectedValue = 283) Then
            Me.btnLigarCuenta.Visible = True
            trTitular.Visible = True
            lblTitular.Visible = True
            trRFC.Visible = True
            Lbl1.Visible = True
            lblNumClienteWS.Visible = True
            txtNumClienteWS.Visible = True
        Else
            Me.btnLigarCuenta.Visible = False
            trTitular.Visible = False
            lblTitular.Visible = False
            trRFC.Visible = False
            Lbl1.Visible = False
            lblNumClienteWS.Visible = False
            txtNumClienteWS.Visible = False
        End If


        If ddlTipoCuenta.SelectedValue = 283 Then
            Me.txtNumeroCuenta.Enabled = True
            Me.txtNumeroTarjeta.Enabled = False
            Me.txtClaveInterbancaria.Enabled = False

        ElseIf ddlTipoCuenta.SelectedValue = 284 Then
            Me.txtNumeroCuenta.Enabled = False
            Me.txtNumeroTarjeta.Enabled = True
            Me.txtClaveInterbancaria.Enabled = False

        ElseIf ddlTipoCuenta.SelectedValue = 285 Then
            Me.txtNumeroCuenta.Enabled = False
            Me.txtNumeroTarjeta.Enabled = False
            Me.txtClaveInterbancaria.Enabled = True

        ElseIf ddlTipoCuenta.SelectedValue = 0 Then
            Me.txtNumeroCuenta.Enabled = False
            Me.txtNumeroTarjeta.Enabled = False
            Me.txtClaveInterbancaria.Enabled = False

        End If


    End Sub


    Public Class Customer
        Public Person As Person
    End Class
    Public Class Person
        Public id As String
        Public name As String
        Public lastName As String
        Public mothersLastName As String
        Public identityDocument As List(Of _identityDocument) = New List(Of _identityDocument)
    End Class
    Public Class msjerr
        Public message As String
        Public status As String
    End Class
    Public Class _identityDocument
        'Public lista As List(Of docstype)
        Public number As String
    End Class
    'Public Class docstype
    '    Public number As String
    'End Class

    Protected Sub btnVisor_Click(sender As Object, e As EventArgs)
        Dim ds As DataSet
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos

        objCatalogos.Parametro = Me.lblSolicitud.Text
        ds = objCatalogos.Catalogos(7)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                'Dim Cliente_bbva As Integer = 0
                Dim Cliente_bbva As String = ""
                Cliente_bbva = ds.Tables(0).Rows(0).Item("CLIENTE_BBVA")

                If Cliente_bbva <> "" Then
                    'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?pantalla=" + Request("idPantalla") + "&folio=" + Me.lblSolicitud.Text + "&cliente=" + Cliente_bbva.ToString + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "&cliente=" + Cliente_bbva.ToString + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)
                Else
                    'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?pantalla=" + Request("idPantalla") + "&folio=" + Me.lblSolicitud.Text + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)

                End If
            End If
        End If
    End Sub
    Protected Sub btnDetalleImpagos_Click(sender As Object, e As EventArgs)
        'Dim ds As DataSet
        'Dim objCatalogos As New ProdeskNet.SN.clsCatalogos

        'objCatalogos.Parametro = Me.lblSolicitud.Text
        'ds = objCatalogos.Catalogos(19)

        ' If ds.Tables.Count > 0 Then
        'If ds.Tables(0).Rows.Count > 0 Then
        'Dim Cliente_bbva As Integer = 0
        ' Dim Cliente_bbva As String = ""
        ' Cliente_bbva = ds.Tables(0).Rows(0).Item("CLIENTE_BBVA")


        'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?pantalla=" + Request("idPantalla") + "&folio=" + Me.lblSolicitud.Text + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../aspx/DetalleImpagos.aspx?folio=" + Me.lblSolicitud.Text + "','popupVisor','width=1200,height=500,left=-10,top=0,resizable');", True)


        ' End If
        ' End If
    End Sub
    Public Sub CargaDatos()
        Dim objDom As New clsDomiciliacion
        Dim ds As DataSet

        Dim intValueBanco_1 = "0" + Format(12, "0#")
        Dim intValueBanco_2 = "0" + Format(17, "0#")

        Dim intValueBanco_3 = "0" + Format(72, "0#")
        Dim intValueBanco_4 = "0" + Format(32, "0#")

        objDom.Solicitud = Me.lblSolicitud.Text
        ds = objDom.Domiciliacion(1)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Me.txtFolioFiscal.Value = ds.Tables(0).Rows(0).Item("FOLIO_FISCAL").ToString
                'Me.txtFolioFiscal.Text = ds.Tables(0).Rows(0).Item("FOLIO_FISCAL").ToString
                If (ds.Tables(0).Rows.Count > 0 And ddlBanco.Items.Count > 0) Then
                    Dim banco As String = CInt(ds.Tables(0).Rows(0).Item("BANCO")).ToString("D3")
                    ViewState("vwsBanco") = banco
                    ddlBanco.SelectedValue = banco
                    If CInt(ViewState("vwsIntEnable")) = 1 OrElse LblRFC.Text <> String.Empty Then
                        Dim intValueBanco_1_1 = "0" + Format(12, "0#")
                        Dim intValueBanco_2_2 = "0" + Format(17, "0#")
                        If (banco = intValueBanco_1_1 Or banco = intValueBanco_2_2) Then
                            VerificaEnable()
                        End If
                    End If
                Else
                    ddlBanco.SelectedValue = intValueBanco_1
                    ddlBanco_SelectedIndexChanged(Nothing, Nothing)
                End If
                If (ds.Tables(0).Rows.Count > 0) Then

                    If ddlBanco.SelectedValue = intValueBanco_3 Or ddlBanco.SelectedValue = intValueBanco_4 Then
                        hdnResultado1.Value = 1
                    End If

                    If ddlBanco.SelectedValue = intValueBanco_1 Or ddlBanco.SelectedValue = intValueBanco_2 Then
                        hdnResultado1.Value = 1
                        CargaCombos(1)

                    Else
                        CargaCombos(2)


                    End If


                    ddlTipoCuenta.SelectedValue = ds.Tables(0).Rows(0).Item("PDK_TIPO_CUENTA").ToString
                End If
                lblTitularCuenta.Text = ds.Tables(0).Rows(0).Item("TITULAR_CUENTA").ToString
                trTitular.Visible = True
                lblTitular.Visible = True
                trRFC.Visible = True
                Lbl1.Visible = True
                txtClaveInterbancaria.Text = ds.Tables(0).Rows(0).Item("CLABE").ToString
                txtNumeroTarjeta.Text = ds.Tables(0).Rows(0).Item("NUMERO_TARJETA").ToString
                txtNumeroCuenta.Text = ds.Tables(0).Rows(0).Item("NUMERO_CUENTA").ToString
                HiddenRFCCompleto.Value = ds.Tables(0).Rows(0).Item("RFC").ToString
                If (Not IsNothing(ds.Tables(0).Rows(0).Item("HOMOCLAVE")) Or Not IsDBNull(ds.Tables(0).Rows(0).Item("HOMOCLAVE"))) Then
                    If (ds.Tables(0).Rows(0).Item("HOMOCLAVE").ToString <> "") Then
                        HiddenRFCCompleto.Value = ds.Tables(0).Rows(0).Item("RFC").ToString + ds.Tables(0).Rows(0).Item("HOMOCLAVE").ToString()
                    End If
                End If

                If ddlBanco.SelectedValue = intValueBanco_1 Or ddlBanco.SelectedValue = intValueBanco_2 Then
                    If (ddlTipoCuenta.SelectedValue = 284) Then
                        If (txtNumeroTarjeta.Text <> "") Then
                            hdnResultado1.Value = 1
                            hdnResultado2.Value = 1
                        Else
                            hdnResultado2.Value = 0
                        End If
                    ElseIf (ddlTipoCuenta.SelectedValue = 283) Then
                        If (txtNumeroCuenta.Text <> "") Then
                            'btnValidar_Click(btnValidar, Nothing)
                            btnLigarCuenta_Click(btnLigarCuenta, Nothing) 'BUG-PD-328: CGARCIA: 05/01/2017: SE AGREGA COLUMNA PARA GUARDAR EL NUMERO DE CLIENTE QUE AROJA EL WS DE LIGUE DE CUENTA
                            If CInt(ViewState("vwsIntEnable")) = 1 OrElse LblRFC.Text <> String.Empty Then
                                If (CStr(ViewState("vwsBanco")) = intValueBanco_1 Or CStr(ViewState("vwsBanco")) = intValueBanco_2) Then
                                    VerificaEnable()
                                End If
                            End If
                        End If
                    End If
                End If
            Else
                If (ddlBanco.Items.Count > 0) Then
                    ddlBanco.SelectedValue = intValueBanco_1
                    ddlBanco_SelectedIndexChanged(Nothing, Nothing)
                End If
            End If
        Else
            If (ddlBanco.Items.Count > 0) Then
                ddlBanco.SelectedValue = intValueBanco_1
                ddlBanco_SelectedIndexChanged(Nothing, Nothing)
            End If
        End If
    End Sub

    Public Sub ValidaDigitosBancarios()
        Dim strCahDigitos As String
        Dim ValueBanco As Integer
        Dim ValueBancoModificado As String
        Dim Resp As String
        hdnResultado2.Value = 0
        Dim nclabe As Int64 = 0
        If (txtClaveInterbancaria.Text <> "" And txtClaveInterbancaria.Text.Length > 3) Then

            Dim cadena As String = Trim(txtClaveInterbancaria.Text).Substring(3)
            Dim result As Boolean = Int64.TryParse(cadena, nclabe)

            If (result) Then


                Valida_entrada()
                Resp = Valida_entrada()
                If Resp <> String.Empty Then
                    strCahDigitos = Resp
                Else
                    strCahDigitos = hiddenCach.Value
                End If
                'strCahDigitos = hiddenCach.Value
                ValueBanco = ddlBanco.SelectedValue
                If Len(ValueBanco.ToString) = 1 Then
                    ValueBancoModificado = "00" + CStr(ValueBanco)
                    If ValueBancoModificado <> "000" Then
                        If strCahDigitos <> String.Empty Then
                            If strCahDigitos <> ValueBancoModificado Then
                                Master.MensajeError("La CLABE no pertenece al banco seleccionado")
                                txtClaveInterbancaria.Focus()
                                hdnResultado2.Value = 0
                            Else
                                hdnResultado2.Value = 1
                            End If
                        End If
                    End If
                ElseIf Len(ValueBanco.ToString) = 2 Then
                    ValueBancoModificado = "0" + CStr(ValueBanco)
                    If strCahDigitos <> String.Empty Then
                        If strCahDigitos <> ValueBancoModificado Then
                            Master.MensajeError("La CLABE no pertenece al banco seleccionado")
                            txtClaveInterbancaria.Focus()
                            hdnResultado2.Value = 0
                        Else
                            hdnResultado2.Value = 1
                        End If
                    End If
                ElseIf Len(ValueBanco.ToString) = 3 Then
                    If strCahDigitos <> String.Empty Then
                        If strCahDigitos <> ValueBanco Then
                            Master.MensajeError("La CLABE no pertenece al banco seleccionado")
                            txtClaveInterbancaria.Focus()
                            hdnResultado2.Value = 0
                        Else
                            hdnResultado2.Value = 1
                        End If
                    End If
                End If
            Else
                txtClaveInterbancaria.Text = ""
            End If

        End If

    End Sub

    Protected Sub btnValidar_Click(sender As Object, e As EventArgs)
        Dim mensaje As String = String.Empty
        hdnResultado1.Value = 0
        If (validadatoscfdi()) Then

            'consultar el servicio
            Dim objmdec As New ProdeskNet.WCF.ClsCFDI
            'objmdec.transmitterTaxpayer = "MSE071130LB2"
            'objmdec.receiverTaxpayer = "MARJ6908135D8"
            'objmdec.totalRevenues = "0000000030277.180000"
            'objmdec.referenceFiscalNumber = "352C0C29-D960-40A8-AA1D-0ECC8525676F"

            objmdec.transmitterTaxpayer = txtRFCE.Value
            objmdec.receiverTaxpayer = txtRFCR.Value
            objmdec.totalRevenues = txtTotal.Value
            'objmdec.referenceFiscalNumber = txtFolioFiscal.Text
            objmdec.referenceFiscalNumber = txtFolioFiscal.Value
            mensaje = "Validando"
            If objmdec.ValidaCFDI Then
                mensaje = objmdec.estatus + " CFDI"
                Master.MensajeError(mensaje)
                hdnResultado1.Value = 1


            Else
                mensaje = objmdec.estatus + " " + objmdec.Strerror
                'mensaje = "Servivio no disponible"
                'btnProcesarCliente.Disabled = True
                Master.MensajeError(mensaje)
                hdnResultado1.Value = 0

            End If
        End If


    End Sub

    Public Function validadatoscfdi() As Boolean
        Dim estado As Boolean = False

        If txtFolioFiscal.Value = String.Empty Then
            'If txtFolioFiscal.Text = String.Empty Then
            Master.MensajeError("Se tiene que capturar el folio fiscal")
            'ElseIf (Len(txtFolioFiscal.Text) < 32) Then
        ElseIf (Len(txtFolioFiscal.Value) < 32) Then
            Master.MensajeError("El Folio fiscal capturado contiene formato invalido")
        ElseIf (txtTotal.Value = String.Empty) Then
            Master.MensajeError("Falta introducir el total de la cantidad")
        ElseIf (txtRFCE.Value = String.Empty) Then
            Master.MensajeError("Falta introducir el RFC del emisor")
        ElseIf (txtRFCR.Value = String.Empty) Then
            Master.MensajeError("Falta introducir el RFC del receptor")

        Else
            estado = True
        End If

        Return estado
    End Function

    Public Function Valida_entrada() As String
        Dim str1 As String
        Dim resultado As String

        If txtClaveInterbancaria.Text <> String.Empty Then
            str1 = txtClaveInterbancaria.Text.Substring(0, 3)
        Else
            str1 = String.Empty
        End If
        resultado = str1
        Return resultado
    End Function
    Public Function ObtenRFCcte(opc As Integer) As String
        Dim rfccompleto As String = ""
        Dim objDom As New clsSolicitante
        Dim dsts As DataSet
        If (opc = 1) Then
            dsts = objDom.CargaDatosSolicitanteTodos(Me.lblSolicitud.Text)
            If dsts.Tables.Count > 0 Then
                If dsts.Tables(0).Rows.Count > 0 Then
                    HiddenRFC.Value = dsts.Tables(0).Rows(0).Item("RFC").ToString
                    HiddenHomoclave.Value = dsts.Tables(0).Rows(0).Item("HOMOCLAVE").ToString
                    If (Not IsNothing(HiddenHomoclave.Value) Or Not IsDBNull(HiddenHomoclave.Value)) Then
                        rfccompleto = HiddenRFC.Value.ToString() + HiddenHomoclave.Value.ToString()
                    Else
                        rfccompleto = HiddenRFC.Value.ToString()
                    End If
                Else : HiddenRFCCompleto.Value = ""

                End If
            End If


        End If

        Return rfccompleto
    End Function
    Public Sub LimpiaDatosCFDI()
        'txtFolioFiscal.Text = ""
        txtFolioFiscal.Value = ""
        txtTotal.Value = ""
        txtRFCE.Value = ""
        txtRFCR.Value = ""

    End Sub


    Public Sub VerificaEnable()
        txtNumClienteWS.Visible = True
        lblNumClienteWS.Visible = True
    End Sub

    Protected Sub ddlTurnar_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
End Class