Imports System.Data
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Configurcion
Imports ProdeskNet.SN
Imports System.Globalization

'Tracker:INC-B-2019:JDRA:Regresar
'BBV-P-423 RQCONYFOR-01: AVH: 02/01/2017 Se llenan combos
'BBV-P-423 RQCONYFOR-07: AVH: 18/01/2017 SE AGREGA WS createCarLoanPreformalize
'BUG-PD-13  GVARGAS  01/03/2017 Cambio WCF Credenciales
'BUG-PD-16 MPUESTO 06/03/2016 
'   + Obtención de la Descripción del Auto
'   + Adición del -Seleccionar- por defecto al inicio de cada combo
'                  10/03/2016
'   + Inhabilitación de campos de vendedor y agencia si el valor es == 0
'   + Inhabilitación de botón Procesar
'BUG-PD-20 MPUESTO 06/03/2016 
'   + Obtención del catalogo de Carrocerías para el dropdownlist correspondiente
'   + Obtención del precio del producto
'BUG-PD-26 MPUESTO 10/04/2017 Cambio de origen de datos del campo a quien se le paga y cuenta del vendedor
'BUG-PD-27 MPUESTO 19/04/2017 Corrección para mostrar tablas después de error en web service, modificación de la función para seguir buenas prácticas.
'BUG-PD-46: AVEGA: 08/05/2017 Modificaciones del JSON de Preforma
'BUG-PD-78:MPUESTO:07/06/2017:ADICION DE DROPDOWNLIST PARA PROCESO DE TURNAR
'BUG-PD-266: CGARCIA: 08/11/2017: SE CAMBIA LA FECHA FIRMA CONTRATO POR LA FERCHA DE COTIZACION
'RQ-PI7-PD9: CGARCIA: 05/12/2017: Cambio de fecha de contrato a la fecha actual del servidor.
'BUG-PD-294: DJUAREZ: 14/12/2017: Se agrega validacion para validar que siempre se obtenga contrato.
'BUG-PD-341: JMENDIETA: 16/01/2018: En la consulta de solicitud con estatus terminada se oculta la fecha de Firma de Contrato y al dar clic en procesar se actualiza y mostrar un mensaje de que la fecha se actualizara a la actual si es el caso.
'RQ-PC7: CGARCIA: 06/04/2018: CAMBIO DE PAYLOAD DE PREFORMALIZACION
Public Class altaFacturacion
    Inherits System.Web.UI.Page
    Dim BD As New ProdeskNet.BD.clsManejaBD
    Dim dc As New ProdeskNet.Catalogos.clsDatosCliente
    Dim es As New ProdeskNet.Catalogos.clsStatusSolicitud
    Dim _clsCuestionarioSolvsID As clsCuestionarioSolvsID
    Dim _clsSolicitudes As clsSolicitudes
    Dim _dtsResult As New DataSet()
    Dim _clsPantallas As ProdeskNet.SN.clsPantallas
    Dim _clsCatTareas As ProdeskNet.SN.clsCatTareas
    Dim _mostrarPantalla As Integer = 0
    Dim strHoraServidor As DateTime = System.DateTime.Now.ToString("dd/MM/yyyy")

    Sub page_preinit(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreInit
        If Request.QueryString("MP") = 1 Then
            Me.MasterPageFile = "~/aspx/MasterPageVacia.Master"
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim mensaje As String = "Error al procesar la Tarea"

        ' WS(mensaje)
        If Not IsPostBack Then
            Dim dsNom As DataSet
            Dim intEnable As Integer = 0

            Me.lblSolicitud.Text = Request.QueryString("sol").ToString()
            hdnIdFolio.Value = Request.QueryString("sol")
            hdnIdPantalla.Value = Request.QueryString("idpantalla")
            hdnUsua.Value = Session("IdUsua")
            If Session("Regresar") Is Nothing Then
                Session("Regresar") = Request.UrlReferrer.LocalPath
                hdRutaEntrada.Value = Request.UrlReferrer.LocalPath
            End If

            hdRutaEntrada.Value = Session("Regresar")

            es.getStatusSol(Request.QueryString("sol"))
            Me.lblStCredito.Text = es.PStCredito
            Me.lblStDocumento.Text = es.PStDocumento

            Dim dsFac As New DataSet

            dsFac = BD.EjecutarQuery("select isnull(sum(importe),0) from (select pdk_id_secccero, sum(pdk_fd_FAct_importe) as importe from pdk_FacturaActivo group by pdk_id_secccero union select pdk_id_secccero, sum(pdk_fd_FAcc_importe) from pdk_FacturaAccesorios group by pdk_id_secccero union select pdk_id_secccero, isnull(sum(pdk_fd_FInt_importe), 0) from pdk_FacturaIntangibles 	group by pdk_id_secccero union select pdk_id_secccero, isnull(sum(pdk_fd_FCom_importe), 0) from pdk_FacturaComercializacion group by pdk_id_secccero)a where a.pdk_id_secccero = " & Request.QueryString("sol"))

            If Not IsDBNull(dsFac) Then
                If dsFac.Tables(0).Rows.Count() > 0 Then
                    Me.txtSumaFacturas.Value = dsFac.Tables(0).Rows(0)(0)
                End If
            End If

            Try
                dsNom = BD.EjecutarQuery("EXEC sp_GenerarRura " & hdnIdFolio.Value & "," & hdnIdPantalla.Value & "," & hdnUsua.Value & "")
                If dsNom.Tables(0).Rows.Count > 0 AndAlso dsNom.Tables.Count > 0 Then
                    hdnResultado.Value = dsNom.Tables(0).Rows(0).Item("RUTA")
                    hdnResultado1.Value = dsNom.Tables(1).Rows(0).Item("RUTA2")
                    hdnResultado2.Value = dsNom.Tables(2).Rows(0).Item("RUTA3")

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
                'btnGuardarFac.Attributes.Add("style", "display:none;")
                Me.btnProcesar.Attributes.Add("style", "display:none;")
                Me.btnProcesarCliente.Attributes.Add("style", "display:none;")
                'cmbGuardar.Attributes.Add("style", "display:none;")
                ' btnAutorizar.Attributes.Add("style", "display:none;")
                btnCancelar.Attributes.Add("style", "display:none;")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "OcultaElementos", "disableElements();", True)
                Me.feFirma.Attributes.Add("style", "display:none;") 'BUG-PD-341
                Me.feFirma.Attributes.Remove("class") 'BUG-PD-341
                Me.lblFirmaContrato.Attributes.Add("style", "display:none;") 'BUG-PD-341
            End If

            'datos del cliente

            dc.GetDatosCliente(lblSolicitud.Text)

            lblCliente.Text = dc.propNombreCompleto


            llenaCombos()


            'OBTEN DATOS DE COMERCIALIZACION
            Dim objFact As New ProdeskNet.SN.clsFacturacion
            Dim dsFact As DataSet

            Dim printDatosVendedor As Boolean = False
            objFact.ID_SOLICITUD = Me.lblSolicitud.Text
            dsFact = objFact.ObtenDatosFact(1)
            If dsFact.Tables.Count > 0 Then
                If dsFact.Tables(0).Rows.Count > 0 Then
                    printDatosVendedor = True
                    Me.ComVendedor.Value = dsFact.Tables(0).Rows(0).Item("PTJ_MAX_COMISION_VENDEDOR")
                    Me.ComAgencia.Value = dsFact.Tables(0).Rows(0).Item("PTJ_MAX_COMISION_AGENCIA")
                    'BUG-PD-16 MPUESTO 10/03/2016 Deshabilitar campos de vendedor y agencia si el valor es == 0
                    Me.hdnComVendedor.Value = dsFact.Tables(0).Rows(0).Item("PTJ_MAX_COMISION_VENDEDOR")
                    Me.hdnComAgencia.Value = dsFact.Tables(0).Rows(0).Item("PTJ_MAX_COMISION_AGENCIA")
                    'Me.qPaga.Value = dsFact.Tables(0).Rows(0).Item("TITULAR_CUENTA")
                    'Me.CueVendedor.Value = dsFact.Tables(0).Rows(0).Item("TITULAR_CUENTA")
                    'BUG-PD-26 MPUESTO 10/04/2017 Cambio de origen de datos del campo a quien se le paga y cuenta del vendedor
                    If Not dsFact.Tables(0).Rows(0).Item("PTJ_MAX_COMISION_VENDEDOR") Is Nothing Then
                        If Convert.ToDecimal(dsFact.Tables(0).Rows(0).Item("PTJ_MAX_COMISION_VENDEDOR").ToString()) > 0 Then
                            Me.qPaga.Value = dsFact.Tables(0).Rows(0).Item("NOMBRE_VENDEDOR")
                            Me.CueVendedor.Value = dsFact.Tables(0).Rows(0).Item("NO_CUENTA_VENDEDOR")
                        Else
                            printDatosVendedor = False
                        End If
                    Else
                        printDatosVendedor = False
                    End If

                    'BUG-PD-16 MPUESTO 06/03/2016 Obtención de la Descripción del Auto
                    Me.desAuto.Value = dsFact.Tables(0).Rows(0).Item("DESCRIPCION")
                    'BUG-PD-20 MPUESTO 23/03/2016 Obtención del precio del producto
                    Me.txtImporte.Value = dsFact.Tables(0).Rows(0).Item("PRECIO_PRODUCTO")
                    If dsFact.Tables(0).Rows(0).Item("COMISION_AGENCIA") = 1 Then
                        Me.ComVendedor.Disabled = True
                    End If

                    If dsFact.Tables(0).Rows(0).Item("COMISION_VENDEDOR") = 1 Then
                        Me.ComAgencia.Disabled = True
                    End If
                End If
            End If
            If Not printDatosVendedor Then
                Me.lblqPaga.Visible = False
                Me.qPaga.Visible = False
                Me.lblCueVendedor.Visible = False
                Me.CueVendedor.Visible = False
            End If
            poblarComboTurnar()
        End If
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click

        'Response.Redirect("./consultaPantalla.aspx")
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)

    End Sub

    'BUG-PD-16 MPUESTO 06/03/2016 Adición del -Seleccionar- por defecto al inicio de cada combo
    '   + Borrar la sección TEMP cuando se obtenga el catálogo de carrocería
    Public Sub llenaCombos()
        Try
            Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
            Dim objCombo As New clsParametros
            Dim dsPais As DataSet
            Dim dsTrans As DataSet
            Dim dsCarroc As DataSet
            
            dsPais = objCatalogos.Catalogos(1)
            dsTrans = objCatalogos.Catalogos(2)
            objCatalogos.Parametro = 286
            dsCarroc = objCatalogos.Catalogos(3)

            If dsPais.Tables.Count > 0 And dsPais.Tables(0).Rows.Count > 0 Then
                'BUG-PD-16 MPUESTO 06/03/2016 Adición del -Seleccionar- por defecto al inicio de cada combo
                objCombo.LlenaCombos(dsPais, "PAI_DS_NOMBRE_ESPANOL", "PAI_FL_CVE", ddlNumPais, True, True)
            End If
            If dsTrans.Tables.Count > 0 And dsTrans.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dsTrans, "PDK_PAR_SIS_PARAMETRO", "PDK_ID_PARAMETROS_SISTEMA", ddlTransmision, True, True, )
            End If
            'BUG-PD-20 MPUESTO 06/03/2016 Obtención del catalogo de Carrocerías para el dropdownlist correspondiente
            If dsCarroc.Tables.Count > 0 And dsCarroc.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dsCarroc, "PDK_PAR_SIS_PARAMETRO", "PDK_ID_PARAMETROS_SISTEMA", ddlTipoCarroceria, True, True)
            End If

            'RQ-PI7-PD9: CGARCIA: 05/12/2017: Cambio de fecha de contrato a la fecha actual del servidor.
            feFirma.Value = strHoraServidor 'Convert.ToDateTime(dsFechaCot.Tables(0).Rows(0).Item("RESUL")).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)

        Catch ex As Exception
            Master.MensajeError(ex.Message)

        End Try
    End Sub

    Public Function WS(ByRef outputMessage As String) As Boolean
        Dim result As Boolean = True
        Dim PreFormalizacion As PreFormalizacion = New PreFormalizacion
        Dim objFactDETALLE As New ProdeskNet.SN.clsFacturacion
        Dim DSWS As DataSet

        objFactDETALLE.ID_SOLICITUD = Request.QueryString("sol")
        DSWS = objFactDETALLE.ObtenDatosFact(4)

        If DSWS.Tables.Count > 0 Then
            If DSWS.Tables(1).Rows.Count > 0 Then

                PreFormalizacion.entityOffice.bank.id = DSWS.Tables(1).Rows(0).Item("bank.id")
                PreFormalizacion.extendedData.accountIndicator = DSWS.Tables(1).Rows(0).Item("accountIndicator")
                PreFormalizacion.loan.commercialValueAmount.amount = DSWS.Tables(1).Rows(0).Item("commercialValueAmount.amount")
                PreFormalizacion.loan.requestDate = DSWS.Tables(1).Rows(0).Item("requestDate")
                PreFormalizacion.customer.extendedData.regularCustomer = DSWS.Tables(1).Rows(0).Item("regularCustomer")

                If DSWS.Tables(0).Rows.Count > 0 Then
                    For x = 0 To DSWS.Tables(0).Rows.Count - 1
                        PreFormalizacion.customer.extendedData.referenceCustomer.Add(DSWS.Tables(0).Rows(x).Item("referenceCustomer"))
                    Next
                End If
                'PreFormalizacion.customer.extendedData.referenceCustomer.Add("RICARDO TAPIA MARTINEZ                  0155526491358")
                'PreFormalizacion.customer.extendedData.referenceCustomer.Add("CLIENTE 3                               0155538491251")
                'PreFormalizacion.customer.extendedData.referenceCustomer.Add("ALBERTO DEL RIO GARCIA                  0155530216575")

                PreFormalizacion.customer.person.id = DSWS.Tables(1).Rows(0).Item("person.id")
                PreFormalizacion.paymentAnnuity.amount = DSWS.Tables(1).Rows(0).Item("paymentAnnuity.amount")
                'PreFormalizacion.checkDigit1 = DSWS.Tables(1).Rows(0).Item("checkDigit1")
                'PreFormalizacion.creditNumber = DSWS.Tables(1).Rows(0).Item("creditNumber")
                PreFormalizacion.subProductCode = DSWS.Tables(1).Rows(0).Item("subProductCode")
                PreFormalizacion.iLoanDetail.loanCar.agency.intermediateCode = DSWS.Tables(1).Rows(0).Item("intermediateCode")
                PreFormalizacion.iLoanDetail.loanCar.agency.id = DSWS.Tables(1).Rows(0).Item("agency.id")

                PreFormalizacion.iLoanDetail.loanCar.car.brand = DSWS.Tables(1).Rows(0).Item("brand")
                PreFormalizacion.iLoanDetail.loanCar.car.model = DSWS.Tables(1).Rows(0).Item("model")
                PreFormalizacion.iLoanDetail.loanCar.car.descriptionCar = DSWS.Tables(1).Rows(0).Item("descriptionCar")
                PreFormalizacion.iLoanDetail.loanCar.car.engineNumber = DSWS.Tables(1).Rows(0).Item("engineNumber")
                PreFormalizacion.iLoanDetail.loanCar.car.serialNumber = DSWS.Tables(1).Rows(0).Item("serialNumber")
                PreFormalizacion.iLoanDetail.loanCar.car.isUsed = DSWS.Tables(1).Rows(0).Item("isUsed")
                PreFormalizacion.iLoanDetail.loanCar.car.subBrand = DSWS.Tables(1).Rows(0).Item("subBrand")
                PreFormalizacion.iLoanDetail.loanCar.requestAmount.amount = DSWS.Tables(1).Rows(0).Item("requestAmount")

                PreFormalizacion.iLoanDetail.loanCar.invoice.date1 = DSWS.Tables(1).Rows(0).Item("date1")
                PreFormalizacion.iLoanDetail.loanCar.invoice.costCar.amount = DSWS.Tables(1).Rows(0).Item("costCar")
                PreFormalizacion.iLoanDetail.loanCar.invoice.deposit.amount = DSWS.Tables(1).Rows(0).Item("deposit")
                PreFormalizacion.iLoanDetail.loanCar.invoice.invoiceNumber = DSWS.Tables(1).Rows(0).Item("invoiceNumber")
                PreFormalizacion.iLoanDetail.loanCar.invoice.tax = DSWS.Tables(1).Rows(0).Item("tax")

                PreFormalizacion.iLoanDetail.loanCar.subsidy.referenceNumber = DSWS.Tables(1).Rows(0).Item("referenceNumber")
                PreFormalizacion.iLoanDetail.loanCar.subsidy.type = DSWS.Tables(1).Rows(0).Item("Type")
                'PreFormalizacion.iLoanDetail.loanCar.insurance.extendedData.paymentOption.id = DSWS.Tables(1).Rows(0).Item("paymentOption.id")
                'PreFormalizacion.iLoanDetail.loanCar.insurance.extendedData.insuranceType.id = DSWS.Tables(1).Rows(0).Item("insuranceType.id")
                'PreFormalizacion.iLoanDetail.loanCar.insurance.extendedData.annualAmount.amount = DSWS.Tables(1).Rows(0).Item("annualAmount.amount")
                PreFormalizacion.iLoanDetail.loanCar.insurance.extendedData.policy.guaranteeAmount.amount = DSWS.Tables(1).Rows(0).Item("guaranteeAmount.amount")


                Dim lisVida As New listInsurance
                lisVida.policyType.id = DSWS.Tables(1).Rows(0).Item("policyType.id.Vida")
                lisVida.insuranceType.id = DSWS.Tables(1).Rows(0).Item("insuranceType.id.Vida")
                lisVida.initialDate = DSWS.Tables(1).Rows(0).Item("initialDate.Vida")
                lisVida.finalDate = DSWS.Tables(1).Rows(0).Item("finalDate.Vida")
                lisVida.annualAmount.amount = DSWS.Tables(1).Rows(0).Item("amount.Vida")
                PreFormalizacion.iLoanDetail.loanCar.insurance.listInsurance.Add(lisVida)

                Dim lisDaño As New listInsurance
                lisDaño.policyType.id = DSWS.Tables(1).Rows(0).Item("policyType.id.Daño")
                lisDaño.insuranceType.id = DSWS.Tables(1).Rows(0).Item("insuranceType.id.Daño")
                lisDaño.initialDate = DSWS.Tables(1).Rows(0).Item("initialDate.Daño")
                lisDaño.finalDate = DSWS.Tables(1).Rows(0).Item("finalDate.Daño")
                lisDaño.annualAmount.amount = DSWS.Tables(1).Rows(0).Item("amount.Daño")
                PreFormalizacion.iLoanDetail.loanCar.insurance.listInsurance.Add(lisDaño)



                'PreFormalizacion.iLoanDetail.loanCar.insurance.listInsurance = DSWS.Tables(1).Rows(0).Item("policyType")
                'PreFormalizacion.iLoanDetail.loanCar.insurance.extendedData.initialDate = DSWS.Tables(1).Rows(0).Item("initialDate")
                'PreFormalizacion.iLoanDetail.loanCar.insurance.extendedData.finalDate = DSWS.Tables(1).Rows(0).Item("finalDate")
                PreFormalizacion.iLoanDetail.loanCar.insurance.extendedData.receipt = DSWS.Tables(1).Rows(0).Item("receipt")
                PreFormalizacion.iLoanDetail.loanCar.packageId = DSWS.Tables(1).Rows(0).Item("packageId")
                PreFormalizacion.iLoanDetail.loanCar.percentageAmount = DSWS.Tables(1).Rows(0).Item("percentageAmount")
                PreFormalizacion.iLoanDetail.loanCar.customerNumberGuarantee = ""


                Dim listrowVenta As New listDtoRate
                listrowVenta.percentage = DSWS.Tables(1).Rows(0).Item("percentage.Venta")
                'listrow.amount.amount = DSWS.Tables(1).Rows(0).Item("percentage.amount")
                listrowVenta.type.name = DSWS.Tables(1).Rows(0).Item("name.Venta")
                'listrow.type.id = DSWS.Tables( 1).Rows(0).Item("type.id")
                PreFormalizacion.iLoanDetail.loanCar.listDtoRate.Add(listrowVenta)

                Dim listrowSubsidio As New listDtoRate
                listrowSubsidio.percentage = DSWS.Tables(1).Rows(0).Item("percentage.Subsidio")
                listrowSubsidio.amount.amount = DSWS.Tables(1).Rows(0).Item("percentage.amount.Subsidio")
                listrowSubsidio.type.name = DSWS.Tables(1).Rows(0).Item("name.Subsidio")
                'listrow.type.id = DSWS.Tables(1).Rows(0).Item("type.id")
                PreFormalizacion.iLoanDetail.loanCar.listDtoRate.Add(listrowSubsidio)

                Dim listrowDescuento As New listDtoRate
                listrowDescuento.percentage = DSWS.Tables(1).Rows(0).Item("percentage.Descuento")
                listrowDescuento.amount.amount = DSWS.Tables(1).Rows(0).Item("percentage.amount.Descuento")
                listrowDescuento.type.name = DSWS.Tables(1).Rows(0).Item("name.Descuento")
                'listrow.type.id = DSWS.Tables(1).Rows(0).Item("type.id")
                PreFormalizacion.iLoanDetail.loanCar.listDtoRate.Add(listrowDescuento)

                Dim listrowComision As New listDtoRate
                listrowComision.percentage = DSWS.Tables(1).Rows(0).Item("percentage.Comision")
                listrowComision.amount.amount = DSWS.Tables(1).Rows(0).Item("percentage.amount.Comision")
                listrowComision.type.name = DSWS.Tables(1).Rows(0).Item("name.Comision")
                'listrow.type.id = DSWS.Tables(1).Rows(0).Item("type.id")
                PreFormalizacion.iLoanDetail.loanCar.listDtoRate.Add(listrowComision)





                PreFormalizacion.iLoanDetail.loanCar.termNumber = DSWS.Tables(1).Rows(0).Item("termNumber")
                PreFormalizacion.productCode = DSWS.Tables(1).Rows(0).Item("productCode")

                PreFormalizacion.refinancing.termNumber = 0
                Dim rate As New rate()
                rate.percentage = 0
                PreFormalizacion.refinancing.rate.Add(rate)
                rate = New rate()
                rate.percentage = 0
                PreFormalizacion.refinancing.rate.Add(rate)



                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim jsonBODY As String = serializer.Serialize(PreFormalizacion)

                Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
                Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
                restGT.buscarHeader("ResponseWarningDescription")
                restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("PreFormalizacion")


                'restGT.consumerID = "10000004"
                'restGT.buscarHeader("ResponseWarningDescription")
                jsonBODY = Replace(jsonBODY, "date1", "date")
                Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)
                Dim res As Respuesta = serializer.Deserialize(Of Respuesta)(jsonResult)


                'Dim Header As String = restGT.valorHeader

                If restGT.IsError Then
                    Dim obj As Object
                    obj = serializer.Deserialize(Of Object)(jsonResult)
                    If restGT.MensajeError <> "" Then
                        outputMessage = restGT.MensajeError

                    ElseIf (Not obj("message") Is Nothing) Then
                        outputMessage = "WS - " & obj("message")
                        'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & restGT.MensajeError & "', '" & "/ProdeskNet/aspx/altaFacturacion.aspx" & "?idPantalla=13&sol=" & Request.QueryString("sol") & "');", True)
                    End If

                    result = False
                    Return result
                    Exit Function
                End If
                Dim CONTRATO As String = res.loan.contract.id

                If CONTRATO = String.Empty Then
                    Master.MensajeError("WS: Contrato incorrecto intentarlo nuevamente.")
                    result = False
                    Return result
                    Exit Function
                Else
                    Dim objFact As New ProdeskNet.SN.clsFacturacion

                    objFact.ID_SOLICITUD = Request.QueryString("sol")
                    objFact.CONTRATO = CONTRATO
                    objFact.ObtenDatosFact(2)

                    If objFact.ErrFacturacion <> "" Then
                        outputMessage = "Error en Base de Datos"
                        result = False
                    End If
                End If

                
            Else
                outputMessage = "Error en Base de Datos"
                result = False
            End If
        Else
            outputMessage = "Error en Base de Datos"
            result = False
        End If
        Return result
    End Function

    'BUG-PD-78:MPUESTO:07/06/2017:ADICION DE DROPDOWNLIST PARA PROCESO DE TURNAR
    'Este método era el original, se ha reemplazado por btnProcesar_Click que realiza la acción del combo turnar
    Protected Sub btnProcesar_Click_(sender As Object, e As EventArgs)
        Dim objFlujos As New clsSolicitudes(0)
        Dim boton As Integer = 64
        Dim ds As DataSet
        Dim mensaje As String = "Error al procesar la Tarea"

        'BUG-PD-341 INI OBTEN DATO SI LA FECHA DE LA FIRMA DE CONTRATO ES DIFERENTE A LA ACTUAL
        Dim objFact As New ProdeskNet.SN.clsFacturacion
        Dim dsFeFirma As DataSet
        objFact.ID_SOLICITUD = Request.QueryString("sol")
        dsFeFirma = objFact.ObtenDatosFact(8)

        If Not (dsFeFirma Is Nothing) AndAlso dsFeFirma.Tables.Count > 0 AndAlso dsFeFirma.Tables(0).Rows.Count > 0 Then
            Dim val = CType(dsFeFirma.Tables(0).Rows(0).Item(0), Integer)
            If val > 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Notificación", "alert('" & String.Format("La fecha de contrato ha sido actualizada a: {0}", Date.Now.ToString("dd-MM-yyyy")) & "');", True)
            End If
        End If
        'BUG-PD-341 FIN



        If ValidaFact() Then
            If WS(mensaje) Then
                objFlujos.PDK_ID_SOLICITUD = Request.QueryString("sol")
                objFlujos.BOTON = boton
                objFlujos.PDK_CLAVE_USUARIO = Session("IdUsua")

                ds = objFlujos.ValNegocio(1)
                If ds.Tables.Count > 0 And objFlujos.ERROR_SOL = String.Empty Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        mensaje = ds.Tables(0).Rows(0).Item("MENSAJE").ToString
                        If mensaje.ToUpper().Contains("TAREA EXITOSA") Then
                            checkRedirect(mensaje)
                        End If
                    End If
                Else
                    mensaje = IIf(mensaje <> String.Empty, mensaje, "Error al procesar la tarea")
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "');", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/altaFacturacion.aspx" & "?idPantalla=13&sol=" & Request.QueryString("sol") & "');", True)
            End If
        Else
            mensaje = "Es necesario agregar una factura para continuar"
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/altaFacturacion.aspx" & "?idPantalla=13&sol=" & Request.QueryString("sol") & "');", True)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "HabilitaBoton", " btnProcesarCliente_Enable();", True)
            CType(sender, Button).Enabled = True
        End If

    End Sub

    Public Function ValidaFact() As Boolean
        ValidaFact = False


        Dim objFact As New ProdeskNet.SN.clsFacturacion
        Dim ds As DataSet

        objFact.ID_SOLICITUD = Request.QueryString("sol")
        ds = objFact.ObtenDatosFact(3)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                If (ds.Tables(0).Rows(0).Item("FACTURA") > 0) Then
                    ValidaFact = True
                End If
            End If
        End If


    End Function

    Private Sub poblarComboTurnar()
        Dim dtsres As New DataSet
        Dim objCombo As New clsParametros
        _clsCuestionarioSolvsID = New clsCuestionarioSolvsID()
        _clsCuestionarioSolvsID._ID_PANT = Request.QueryString("idpantalla")
        dtsres = _clsCuestionarioSolvsID.getTurnar()
        If dtsres.Tables.Count > 0 And dtsres.Tables(0).Rows.Count > 0 Then
            objCombo.LlenaCombos(dtsres, "TURNAR_NOMBRE", "PDK_ID_TAREAS", ddlSeleccionTurnar, True, True)
        End If
    End Sub


    Public Function ConsultaCot() As Boolean
        'RQ-PI7-PD9: CGARCIA: 05/12/2017: Cambio de fecha de contrato a la fecha actual del servidor asi como la consulta de los dias de vigencia desde base.
        Dim dsFechaCot As New DataSet
        Dim strFec_Cot As DateTime
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
        Dim dsDiasCot As New DataSet
        Dim intDias As Integer
        Dim intDiasVigencia As Integer
        Dim res As Boolean = False

        objCatalogos.Parametro = CInt(hdnIdFolio.Value)
        dsFechaCot = objCatalogos.Catalogos(19)
        dsDiasCot = objCatalogos.Catalogos(21)

        If dsFechaCot.Tables.Count > 0 And dsFechaCot.Tables(0).Rows.Count > 0 Then
            If Not dsFechaCot.Tables(0).Rows(0).Item("RESUL") Is DBNull.Value Then
                strFec_Cot = Convert.ToDateTime(dsFechaCot.Tables(0).Rows(0).Item("RESUL")).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                intDias = (strHoraServidor - strFec_Cot).TotalDays
                If dsDiasCot.Tables.Count > 0 AndAlso dsDiasCot.Tables(0).Rows.Count > 0 Then
                    intDiasVigencia = CInt(dsDiasCot.Tables(0).Rows(0).Item("VALOR"))
                    If (intDias > intDiasVigencia) Then
                        Master.MensajeError("La cotización no es vigente")
                        Return res
                    Else
                        res = True
                        Return res
                    End If
                Else
                    Master.MensajeError("No se obtuvieron los días de vigencia de la cotización.")
                    Return res
                End If

            Else
                Master.MensajeError("No hay datos sobre la cotización.")
                Return res
            End If
        Else
            Master.MensajeError("No hay datos sobre la cotización.")
            Return res
        End If
        Return res
    End Function

    'BUG-PD-78:MPUESTO:07/06/2017:ADICION DE DROPDOWNLIST PARA PROCESO DE TURNAR
    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs)
        Dim _strMessage As String = String.Empty
        

        _dtsResult = New DataSet()
        _dtsResult = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & Request("idPantalla").ToString)


        If ddlSeleccionTurnar.SelectedValue <> 0 Then
            _clsSolicitudes = New clsSolicitudes(Request.QueryString("sol"))
            _clsSolicitudes.PDK_ID_SOLICITUD = Request.QueryString("sol")        'ValNegocio(1) y ManejaTarea(4,3)
            _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")   'ValNegocio(1) y ManejaTarea(4,3)
            _clsSolicitudes.PDK_ID_PANTALLA = Request.QueryString("idpantalla")
            _clsSolicitudes.BOTON = 64
            'RQ-PI7-PD9: CGARCIA: 05/12/2017: Cambio de fecha de contrato a la fecha actual del servidor.
            If ddlSeleccionTurnar.SelectedValue = -1 Then
                'NO_RECHAZO
                If ConsultaCot() = True Then
                    btnProcesar_Click_(sender, e)
                Else
                    Exit Sub
                End If

            ElseIf ddlSeleccionTurnar.SelectedValue = -3 Then
                'RECHAZO
                _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO")
                _dtsResult = _clsSolicitudes.ValNegocio(1)
                _strMessage = _dtsResult.Tables(0).Rows(0).Item("MENSAJE").ToString
                _clsSolicitudes.MENSAJE = _strMessage
                _clsSolicitudes.ManejaTarea(5)
                If _strMessage <> "Tarea Exitosa" Then
                    Master.MsjErrorRedirect(_strMessage, "../aspx/consultaPanelControl.aspx")
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "HabilitaBoton", " btnProcesarCliente_Enable();", True)
                Else
                    checkRedirect(_strMessage)
                End If
            End If
        Else
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('Seleccione una acción a realizar.', '" & "../aspx/altaFacturacion.aspx" & "?idPantalla=13&sol=" & Request.QueryString("sol") & "');", True)
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "ddlTurnarSinSeleccion", "alert('Seleccione una acción a realizar.')", True)
        End If
    End Sub

    Protected Sub checkRedirect(ByVal _strMessage As String)
        _dtsResult = New DataSet()
        _clsCatTareas = New clsCatTareas()
        _clsPantallas = New ProdeskNet.SN.clsPantallas()
        _dtsResult = _clsCatTareas.SiguienteTarea(Request.QueryString("sol"))
        _mostrarPantalla = _clsPantallas.SiguientePantalla(Request.QueryString("sol"))
        'ir a pantalla a la fuerza
        If _mostrarPantalla = 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrePopUpOk", "PopUpLetreroRedirect('" & _strMessage & "', '" & "../aspx/" & _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString & "');", True)
            'Response.Redirect()
        ElseIf _mostrarPantalla = 2 Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrePopUpOk", "PopUpLetreroRedirect('" & _strMessage & "', '" & "../aspx/consultaPanelControl.aspx" & "');", True)
        End If
    End Sub

#Region "WCF Classes"

    Public Class PreFormalizacion
        Public productCode As String
        Public subProductCode As String
        Public extendedData As extendedData = New extendedData
        Public entityOffice As entityOffice = New entityOffice
        Public iLoanDetail As iLoanDetail = New iLoanDetail
        Public loan As loan = New loan
        Public customer As customer = New customer
        Public paymentAnnuity As paymentAnnuity = New paymentAnnuity
        Public refinancing As refinancing = New refinancing()
        'Public checkDigit1 As String
        'Public creditNumber As String



    End Class
    Public Class extendedData
        Public accountIndicator As String
    End Class
    Public Class entityOffice
        Public bank As bank = New bank
    End Class
    Public Class bank
        Public id As String
    End Class
    Public Class iLoanDetail
        Public loanCar As loanCar = New loanCar
    End Class
    Public Class loanCar
        Public agency As agency = New agency
        Public car As car = New car
        Public packageId As String
        Public requestAmount As requestAmount = New requestAmount
        Public invoice As invoice = New invoice
        Public termNumber As String
        Public subsidy As subsidy = New subsidy
        Public listDtoRate As New List(Of listDtoRate)
        Public insurance As insurance = New insurance
        Public percentageAmount As String
        Public customerNumberGuarantee As String = String.Empty

    End Class
    Public Class agency
        Public intermediateCode As String
        Public id As Integer
    End Class
    Public Class car
        Public brand As String
        Public model As String
        Public descriptionCar As String
        Public engineNumber As String
        Public serialNumber As String
        Public isUsed As Integer
        Public subBrand As Integer
    End Class
    Public Class requestAmount
        Public amount As String
    End Class
    Public Class invoice
        Public costCar As costCar = New costCar
        Public deposit As deposit = New deposit
        Public date1 As String
        Public invoiceNumber As String
        Public tax As String
    End Class
    Public Class costCar
        Public amount As String
    End Class
    Public Class deposit
        Public amount As String
    End Class
    Public Class subsidy
        Public type As String
        Public referenceNumber As String
    End Class
    Public Class listDtoRate
        Public type As typename = New typename
        Public percentage As String
        Public amount As amount = New amount
    End Class
    Public Class typename
        Public name As String
    End Class
    Public Class amount
        Public amount As String
    End Class
    Public Class insurance
        Public listInsurance As New List(Of listInsurance)
        Public extendedData As extendedData3 = New extendedData3
    End Class
    Public Class listInsurance
        Public policyType As policyType = New policyType
        Public insuranceType As insuranceType = New insuranceType
        Public initialDate As String
        Public finalDate As String
        Public annualAmount As annualAmount = New annualAmount
    End Class
    Public Class policyType
        Public id As Integer
    End Class
    Public Class insuranceType
        Public id As String
    End Class
    Public Class annualAmount
        Public amount As String
    End Class
    Public Class extendedData3
        Public policy As policy = New policy
        Public receipt As String
    End Class
    Public Class policy
        Public guaranteeAmount As guaranteeAmount = New guaranteeAmount
    End Class
    Public Class guaranteeAmount
        Public amount As String
    End Class
    Public Class loan
        Public requestDate As String
        Public commercialValueAmount As commercialValueAmount = New commercialValueAmount
    End Class
    Public Class commercialValueAmount
        Public amount As String
    End Class
    Public Class customer
        Public person As Person = New Person
        Public extendedData As extendedData2 = New extendedData2
    End Class
    Public Class person
        Public id As String
    End Class
    Public Class extendedData2
        Public regularCustomer As String
        Public referenceCustomer As New List(Of String)
    End Class
    Public Class paymentAnnuity
        Public amount As String
    End Class
    Public Class refinancing
        Public termNumber As Integer
        Public rate As List(Of rate) = New List(Of rate)
    End Class
    Public Class rate
        Public percentage As Integer
    End Class
 
    Public Class loanRes
        Public contract As contract = New contract
    End Class
    Public Class paymentOption
        Public id As String
    End Class
    Public Class type
        Public name As String
        Public id As String
    End Class
    Public Class Respuesta
        Public loan As loanRes = New loanRes
    End Class
    Public Class contract
        Public id As String
    End Class

#End Region

End Class