'BBV-P-423 RQCONYFOR-07: AVH: 18/01/2017 SE CREA VENTANA DE DESEMBOLSO
'BUG-PD-13  GVARGAS  01/03/2017 Cambio WCF Credenciales
'BBVA-P-423: RQCONYFOR-07_2: AVH: 11/04/2017 modificaciones WS
'BUG-PD-35: AVH: 25/04/2017 Modificaciones en el Reporte
'BUG-PD-46: AVEGA: 08/05/2017 Modificaciones del JSON de Forma	 
'BUG-PD-86: AVEGA: 10/06/2017 Se bloquea boton Procesar
'BUG-PD-88: CGARCIA: 13/06/2017 Se agregaron las opciones para turnar la tarea 
'BUG-PD-95: CGARCIA: 15/06/2017: Se agrego validacion de no entrar al WebService en caso de que la tarea fuera No procesable
'BUG-PD-103: JBEJAR : 17/06/2017 Correciones solicitadas por AVEGA.
'BUG-PD-98: RHERNANDEZ: 20/06/17 SE AGREGA EMISION DE POLIZAS DE BBVA DESPUES DEL DESEMBOLSO
'BUG-PD-116:RHERNANDEZ: 26/06/17 SE QUITA EMISION DE POLIZAS BBVA PARA QUE SE CONVIERTA EN UNA PAGINA INDEPENDIENTE
'BUG-PD-128: RHERNANDEZ: 05/07/17: se agrega usu para tareas en request para el avance de tareas.
'BUG-PD-46: AVEGA: 17/07/2017 SE QUITA MX. DEL NOMBRE DEL USUARIO PARA MANDARLO AL WS DESEMBOLSO
'BUG-PD-198: CGARCIA: 23/08/2017: SE ELABORO MERGE ENTRE TEST Y DESARROLLO
'BBVA-P-423 RQ-PI7-PD1 GVARGAS 31/10/2017 Mejoras CI Precalificaciòn & Preforma
'BUG-PD-259: RIGLESIAS 09/11/2017 ACTUALIZACIÓN A TERA AUTOMATICA RECHOZO Y NO RECHAZO CONTRASTE DOCUMENTAL 
'BUG-PD-264: RHERNANDEZ: 10/11/17: SE MODIFICO AVANCE DE LA TAREA DEBIDO A QUE YA NO ABRIRA LA TAREA HIBRIDA DE EMISION POLIZAS BBVA
'BUG-PC-282: RHERNANDEZ: 01/12/17: SE MODIFICO LA PANTALLA DE DESEMBOLSO PARA ADAPTARSE DEPENDIENDO SI ES AUTOMATICA O NO Y SI YA FUE EMITIDA EXTERNAMENTE DEJAR AVANZAR NORMALMENTE
'RQ-PC7: CGARCIA: 09/04/2018: CAMBIO DE ESTRUCTURA DE WS
Imports ProdeskNet.SN
Imports System.Diagnostics
Imports System.Data
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.BD
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports ProdeskNet.WCF
Imports System.Data.SqlClient
Imports System.Net
Imports System.IO
Imports ProdeskNet.Seguridad

Partial Class aspx_manejaDesembolso
    Inherits System.Web.UI.Page

    Dim dc As New ProdeskNet.Catalogos.clsDatosCliente
    Dim es As New ProdeskNet.Catalogos.clsStatusSolicitud
    Dim usuario As String = ""
    Dim _clsCuestionarioSolvsID As clsCuestionarioSolvsID
    Dim msg As String = String.Empty




    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        If Session("Regresar") Is Nothing Then
            Session("Regresar") = Request.UrlReferrer.LocalPath
        End If

        hdRutaEntrada.Value = Session("Regresar")
        hdPantalla.Value = Request("idPantalla")
        hdSolicitud.Value = Request("sol")
        hdusuario.Value = Request("usuario")
        usuario = Replace(Session("cveUsuAcc"), "MX.", "")



        'WS()

        If Not IsPostBack Then
            Dim intEnable As Integer = 0

            Try
                poblarComboTurnar()
                intEnable = CInt(Request.QueryString("Enable"))
                Me.lblSolicitud.Text = Request.QueryString("sol")
                es.getStatusSol(Request.QueryString("sol"))
                Me.lblStCredito.Text = es.PStCredito
                Me.lblStDocumento.Text = es.PStDocumento

                dc.GetDatosCliente(lblSolicitud.Text)
                lblCliente.Text = dc.propNombreCompleto

            Catch ex As Exception
                intEnable = 0
            End Try

            If intEnable = 1 Then
                Me.btnProcesar.Attributes.Add("style", "display:none;")
                btnCancelar.Attributes.Add("style", "display:none;")
                Me.btnProcesarCliente.Attributes.Add("style", "display:none;")
            Else
                Me.btnImprimir.Attributes.Add("style", "display:none;")
            End If

            Dim objDes As New ProdeskNet.SN.clsDesembolso
            Dim dsDes As DataSet
            objDes.Solicitud = Request("sol")
            dsDes = objDes.Desembolso(1)


            Me.gvCupon.DataSource = dsDes.Tables(0)
            Me.gvCupon.DataBind()

            Me.gvCupon2.DataSource = dsDes.Tables(1)
            Me.gvCupon2.DataBind()

            Me.gvAgencia.DataSource = dsDes.Tables(2)
            Me.gvAgencia.DataBind()

            Me.gvDetalle.DataSource = dsDes.Tables(3)
            Me.gvDetalle.DataBind()


            Dim objpant As New ProdeskNet.SN.clsPantallas()
            Dim dts As New DataSet()

            dts = objpant.CargaPantallas(Val(Request("idPantalla")).ToString)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Then
                        divcuerpodesem.Visible = False
                        divbotones.Visible = False
                        btnProcesar_Click(btnProcesar, Nothing)
                    Else
                        divcuerpodesem.Visible = True
                        divbotones.Visible = True
                    End If
                End If
            End If
        End If

    End Sub


    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs)
        Dim objpant As New ProdeskNet.SN.clsPantallas()
        Dim dts As New DataSet()

        dts = objpant.CargaPantallas(Val(Request("idPantalla")).ToString)

        If dts.Tables.Count > 0 Then
            If dts.Tables(0).Rows.Count > 0 Then
                If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Then
                    Dim isci As Boolean = True
                    'getIsCI(Request("sol").ToString()) 'valida compra intelijente 

                    If isci Then
                        Dim msg2 As String = "OK" 'validCIWS(Request("sol").ToString())

                        If msg2 = "OK" Then
                            If WS() Then
                                btnAsigna_Click(True, msg)
                            Else
                                '   regresar tarea
                                btnAsigna_Click(False, msg)

                            End If
                        Else
                            'regresar tarea
                            btnAsigna_Click(False, msg2)

                        End If
                    Else
                        If WS() Then
                            btnAsigna_Click(True, msg)
                        Else
                            'regresar tarea
                            btnAsigna_Click(False, msg)

                        End If
                    End If
                Else
                    If ddlTurnar.SelectedValue = -1 Then

                        Dim isci As Boolean = True
                        'getIsCI(Request("sol").ToString()) 'valida compra intelijente 

                        If isci Then
                            Dim msg2 As String = "OK" 'validCIWS(Request("sol").ToString())

                            If msg2 = "OK" Then
                                If WS() Then
                                    btnAsigna_Click(True, msg)
                                Else
                                    '   regresar tarea
                                    Master.MensajeError(msg)

                                End If
                            Else
                                'regresar tarea
                                Master.MensajeError(msg2)

                            End If
                        Else
                            If WS() Then
                                btnAsigna_Click(True, msg)
                            Else
                                'regresar tarea
                                Master.MensajeError(msg)

                            End If
                        End If
                    ElseIf ddlTurnar.SelectedValue = -3 Then
                        btnAsigna_Click(False, msg)
                    Else
                        Master.MensajeError("Es necesario elegir una opción para turnar la solicitud")
                    End If
                End If
            End If
        End If



     


        ' ''Else
        ' ''btnAsigna_Click()
        ' '' End If
        ' ''If WS() Then

        'Dim objFlujos As New clsSolicitudes(0)
        'Dim boton As Integer = 64
        'Dim ds As DataSet
        'Dim mensaje As String = "Error al procesar la Tarea"

        ' btnProcesar.Enabled = False

        'Dim dc As New clsDatosCliente
        'dc.idSolicitud = Request("sol")
        'dc.getDatosSol()

        'objFlujos.PDK_ID_SOLICITUD = Request("sol")
        'objFlujos.BOTON = boton
        'objFlujos.PDK_CLAVE_USUARIO = Session("IdUsua")

        'ds = objFlujos.ValNegocio(1)

        'If ds.Tables.Count > 0 Then
        '    If ds.Tables(0).Rows.Count > 0 Then
        '        mensaje = ds.Tables(0).Rows(0).Item("MENSAJE").ToString
        '    End If
        'End If


        'If objFlujos.ERROR_SOL <> "" Then
        '    Master.MensajeError(objFlujos.ERROR_SOL)
        '    Exit Sub
        'Else
        '    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "');", True)
        'End If
        'End If

    End Sub

    Protected Sub btnImprimir_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnadelanta_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        'Response.Redirect("./consultaPantalla.aspx")
        RegresaPantalla()

    End Sub
    Public Sub RegresaPantalla()
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & Request("sol") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub
    Public Function WS() As Boolean

        Try


            WS = False

            Dim createCarLoanFormalize As createCarLoanFormalize = New createCarLoanFormalize
            Dim objDesembolso As New ProdeskNet.SN.clsFacturacion
            Dim DSWS As DataSet

            objDesembolso.ID_SOLICITUD = Request.QueryString("sol")
            DSWS = objDesembolso.ObtenDatosFact(7)

            If DSWS.Tables.Count > 0 Then
                If DSWS.Tables(0).Rows.Count > 0 Then
                    createCarLoanFormalize.entityOffice.bank.id = DSWS.Tables(0).Rows(0).Item("bank.id").ToString
                    createCarLoanFormalize.entityOffice.id = DSWS.Tables(0).Rows(0).Item("id").ToString
                    createCarLoanFormalize.checkDigit1 = DSWS.Tables(0).Rows(0).Item("checkDigit1").ToString
                    createCarLoanFormalize.creditNumber = DSWS.Tables(0).Rows(0).Item("creditNumber").ToString
                    'Dim row As New loanAmortizations
                    'row.extendedData.totalAnnualCost.amount = DSWS.Tables(0).Rows(0).Item("totalAnnualCost.amount")
                    'createCarLoanFormalize.loan.amortizationSchedule.loanAmortizations.Add(row)
                    createCarLoanFormalize.initialAmount.amount = DSWS.Tables(0).Rows(0).Item("initialAmount.amount").ToString

                    createCarLoanFormalize.iLoanDetail.loanCar.car.color = DSWS.Tables(0).Rows(0).Item("color").ToString
                    createCarLoanFormalize.iLoanDetail.loanCar.car.registrationNumber = DSWS.Tables(0).Rows(0).Item("registrationNumber").ToString
                    createCarLoanFormalize.iLoanDetail.loanCar.car.publicVehicleRegistration = DSWS.Tables(0).Rows(0).Item("publicVehicleRegistration").ToString

                    createCarLoanFormalize.iLoanDetail.loanCar.agency.id = "0"
                    createCarLoanFormalize.iLoanDetail.loanCar.agency.intermediateCode = ""

                    'createCarLoanFormalize.iLoanDetail.loanCar.loanAuthorization.formalizeAuthorization = DSWS.Tables(0).Rows(0).Item("formalizeAuthorization").ToString
                    'createCarLoanFormalize.iLoanDetail.loanCar.loanAuthorization.approverUser = usuario.ToString
                    'createCarLoanFormalize.iLoanDetail.loanCar.loanAuthorization.observations = DSWS.Tables(0).Rows(0).Item("observations").ToString

                    createCarLoanFormalize.iLoanDetail.loanCar.totalAnnualCost = DSWS.Tables(0).Rows(0).Item("totalAnnualCost.amount").ToString

                    'createCarLoanFormalize.iLoanDetail.loanCar.insurance.extendedData.policy.initialAmount.amount = DSWS.Tables(0).Rows(0).Item("initialAmount.amount2")
                    createCarLoanFormalize.iLoanDetail.loanCar.insurance.extendedData.policy.policyId = DSWS.Tables(0).Rows(0).Item("policyId").ToString

                    Dim listDaño As New listInsurance
                    listDaño.policy.initialAmount.amount = DSWS.Tables(0).Rows(0).Item("SeguroDaño").ToString
                    createCarLoanFormalize.iLoanDetail.loanCar.insurance.listInsurance.Add(listDaño)

                    Dim listVida As New listInsurance
                    listVida.policy.initialAmount.amount = DSWS.Tables(0).Rows(0).Item("SeguroVida").ToString
                    createCarLoanFormalize.iLoanDetail.loanCar.insurance.listInsurance.Add(listVida)



                    'createCarLoanFormalize.iLoanDetail.loanCar.insurance.extendedData.insuranceType.name = DSWS.Tables(0).Rows(0).Item("name")

                    createCarLoanFormalize.iLoanDetail.loanCar.invoice.invoiceNumber = DSWS.Tables(0).Rows(0).Item("invoiceNumber").ToString
                    'createCarLoanFormalize.iLoanDetail.loanCar.listDtoRate.Add("")
                    createCarLoanFormalize.iLoanDetail.loanCar.givenInvoiceCar = DSWS.Tables(0).Rows(0).Item("givenInvoiceCar").ToString
                    createCarLoanFormalize.iLoanDetail.loanCar.numberAddressAssociated = DSWS.Tables(0).Rows(0).Item("numberAddressAssociated").ToString

                    createCarLoanFormalize.startLoanDate = DSWS.Tables(0).Rows(0).Item("startLoanDate").ToString

                    createCarLoanFormalize.extendedData.cbScore.referenceNumber = DSWS.Tables(0).Rows(0).Item("cbScore").ToString

                    createCarLoanFormalize.renewal.renewalIndicator = ""
                    createCarLoanFormalize.renewal.previousLoanNumber = ""
                    createCarLoanFormalize.renewal.startDateSecondPhase = ""

                    Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim jsonBODY As String = serializer.Serialize(createCarLoanFormalize)

                    Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                    Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
                    Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
                    restGT.buscarHeader("ResponseWarningDescription")
                    restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("createCarLoanFormalize")


                    'restGT.consumerID = "10000004"
                    restGT.buscarHeader("ResponseWarningDescription")
                    jsonBODY = Replace(jsonBODY, "date1", "date")

                    Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
                    Dim dsTerm As DataSet
                    objCatalogos.Parametro = Request("sol")
                    objCatalogos.ID_USUARIO = Session("IdUsua")
                    dsTerm = objCatalogos.Catalogos(9)

                    Dim LogicalTerm As String, AccountTerm As String

                    LogicalTerm = dsTerm.Tables(0).Rows(0).Item("LogicalTerm")
                    AccountTerm = dsTerm.Tables(0).Rows(0).Item("AccountTerm")

                    'Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY, LogicalTerm, AccountTerm)
                    Dim Header As String = restGT.valorHeader

                    If restGT.IsError Then
                        If restGT.MensajeError <> "" Then
                            If restGT.MensajeError = "PROPUESTA YA ESTA FO RMALIZAD" Then
                                WS = True
                            Else
                                'Master.MensajeError("Error de WS - " & restGT.MensajeError)
                                msg = "Error de WS - " + restGT.MensajeError.ToString()
                            End If
                        End If
                        Exit Function
                    End If
                Else
                    'Master.MensajeError("No se encontro información")
                    msg = "No se encontro información"
                    Exit Function

                End If
            Else
                'Master.MensajeError("No se encontro información")
                msg = "No se encontro información"
                Exit Function
            End If

            WS = True

        Catch ex As Exception
            ' Master.MensajeError(ex.Message)
            msg = ex.Message.ToString()
            Return False
        End Try
    End Function

    Public Class createCarLoanFormalize
        Public entityOffice As entityOffice = New entityOffice
        Public checkDigit1 As String
        Public creditNumber As String
        Public startLoanDate As String

        Public iLoanDetail As iLoanDetail = New iLoanDetail
        'Public loan As loan = New loan
        Public initialAmount As initialAmount = New initialAmount
        Public extendedData As extendedData = New extendedData
        Public renewal As renewal = New renewal
    End Class
    Public Class renewal
        Public renewalIndicator As String
        Public previousLoanNumber As String
        Public startDateSecondPhase As String
    End Class
    Public Class entityOffice
        Public bank As bank = New bank
        Public id As String
    End Class
    Public Class bank
        Public id As String
    End Class
    Public Class loan
        Public amortizationSchedule As amortizationSchedule = New amortizationSchedule
    End Class
    Public Class amortizationSchedule
        Public loanAmortizations As New List(Of loanAmortizations)
    End Class
    Public Class loanAmortizations
        Public extendedData As extendedData = New extendedData
    End Class
    Public Class extendedData
        'Public totalAnnualCost As totalAnnualCost = New totalAnnualCost
        Public cbScore As cbScore = New cbScore
    End Class
    Public Class totalAnnualCost
        Public amount As String
    End Class
    Public Class initialAmount
        Public amount As String
    End Class
    Public Class iLoanDetail
        Public loanCar As loanCar = New loanCar

    End Class
    Public Class cbScore
        Public referenceNumber As String
    End Class
    Public Class loanCar
        Public givenInvoiceCar As String
        Public car As car = New car
        Public agency As agency = New agency
        Public insurance As insurance = New insurance
        Public invoice As invoice = New invoice
        'Public listDtoRate As New List(Of String)

        Public numberAddressAssociated As String
        'Public loanAuthorization As loanAuthorization = New loanAuthorization
        Public totalAnnualCost As String
    End Class

    Public Class agency
        Public id As String
        Public intermediateCode As String
    End Class

    Public Class car
        Public color As String
        Public registrationNumber As String
        Public publicVehicleRegistration As String
    End Class
    Public Class loanAuthorization
        Public formalizeAuthorization As String
        Public approverUser As String
        Public observations As String
    End Class
    Public Class insurance
        Public extendedData As extendedData2 = New extendedData2
        Public listInsurance As New List(Of listInsurance)
    End Class
    Public Class extendedData2
        Public policy As policy = New policy
        'Public insuranceType As insuranceType = New insuranceType
    End Class
    Public Class policy
        Public policyId As String
        'Public initialAmount As initialAmount2 = New initialAmount2

    End Class
    Public Class policylist
        Public initialAmount As initialAmount2 = New initialAmount2
    End Class
    Public Class listInsurance
        Public policy As policylist = New policylist
    End Class
    Public Class insuranceType
        Public name As String
    End Class
    Public Class initialAmount2
        Public amount As String
    End Class
    Public Class invoice
        Public invoiceNumber As String
    End Class

    Protected Sub btnImprimir_Click1(sender As Object, e As EventArgs)

        Dim Fname As String
        Dim crReportDocument As ReportDocument
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim strRuta As String = Server.MapPath("..\Reporte\Desembolso.rpt")
        Dim dtsDatos As New DataSet
        Dim dtsTabla As New DataTable
        Dim id_Solicitud As Integer = Request("sol")


        crReportDocument = New ReportDocument
        crReportDocument.Load(strRuta)

        Fname = Server.MapPath("Prodesknet_" & id_Solicitud & "_" & Format$(Now(), "dd-MM-yyyy HH-mm-ss") & ".pdf")
        Fname = Replace(Fname, "\aspx\", "\Docs\")


        Dim objDes As New ProdeskNet.SN.clsDesembolso
        Dim dsDes As DataSet
        objDes.Solicitud = Request("sol")
        dsDes = objDes.Desembolso(1)




        dtsDatos.Tables.Add(dsDes.Tables(4).Copy())
        dtsDatos.Tables(0).TableName = "Datos"

        dtsDatos.Tables.Add(dsDes.Tables(2).Copy())
        dtsDatos.Tables(1).TableName = "Agencia"

        dtsDatos.Tables.Add(dsDes.Tables(3).Copy())
        dtsDatos.Tables(2).TableName = "Detalle"

        dtsDatos.Tables.Add(dsDes.Tables(5).Copy())
        dtsDatos.Tables(3).TableName = "Desembolso"

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

        Response.Redirect("./Descargapdf.aspx?fname=" & Fname)

    End Sub
    Private Sub poblarComboTurnar()
        Dim dtsres As New DataSet
        Dim objCombo As New clsParametros
        _clsCuestionarioSolvsID = New clsCuestionarioSolvsID()
        _clsCuestionarioSolvsID._ID_PANT = Request.QueryString("idPantalla")
        dtsres = _clsCuestionarioSolvsID.getTurnar()
        If dtsres.Tables.Count > 0 And dtsres.Tables(0).Rows.Count > 0 Then
            objCombo.LlenaCombos(dtsres, "TURNAR_NOMBRE", "PDK_ID_TAREAS", ddlTurnar, True, True)
        End If
    End Sub

    Private Sub btnAsigna_Click(ByVal Tareas As Boolean, ByVal msg As String)
        Dim _dtsResult As DataSet
        Dim _clsManejaBD As New clsManejaBD
        Dim _idPantalla As Integer = Request("idPantalla")
        _dtsResult = _clsManejaBD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & _idPantalla.ToString())



        If Tareas = True Then
            asignaTarea(_dtsResult.Tables(0).Rows(0)("PDK_ID_TAREAS_NORECHAZO").ToString, msg)
        ElseIf Tareas = False Then
            asignaTarea(_dtsResult.Tables(0).Rows(0)("PDK_ID_TAREAS_RECHAZO").ToString, msg)
        Else
            Master.MensajeError("Es necesario elegir una opción para turnar la solicitud")
        End If
    End Sub

    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer, ByVal msg As String)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("solicitud")))
        Dim mensaje As String = String.Empty
        Try

            Dim usu As Integer = Request("usuario")

            If usu = 0 Then
                usu = Request("usu")
            End If

            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("sol"))
            Solicitudes.PDK_CLAVE_USUARIO = usu
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")

            If idAsignarPantalla <> 0 Then
                Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla
            End If

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "TAREA EXITOSA" And mensaje <> "SE RECHAZO  DOCUMENTO " And mensaje <> "Tarea Exitosa" Then
                Throw New Exception(mensaje)
            End If

            dslink = objtarea.SiguienteTarea(Val(Request("sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("sol")))
            Dim dc As New clsDatosCliente
            dc.idSolicitud = Request("sol")
            dc.getDatosSol()

            If muestrapant = 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usu")).ToString & "&usu=" & Val(Request("usu")).ToString & "');", True)
            ElseIf muestrapant = 2 Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & msg & "', '" & "../aspx/consultaPanelControl.aspx" & "');", True)
                'Response.Redirect("../aspx/consultaPanelControl.aspx")
            End If
        Catch ex As Exception
            Master.MensajeError(mensaje)
            btnProcesar.Enabled = False
        End Try
    End Sub

    Private Function validCIWS(ByVal IdSol As String) As String
        Dim msg_CI As String = String.Empty

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("getDocumentDataOfCar").ToString().Replace("Cambiar_NUM_CREDIT", getNumCI(IdSol))
        Dim jsonResult As String = rest.ConnectionGet(userID, iv_ticket1, String.Empty)

        'If ((rest.IsError = False) And (jsonResult <> "{}")) Then
        '    Dim DocumentDataOfCar As ProdeskNet.WCF.DocumentDataOfCar = serializer.Deserialize(Of ProdeskNet.WCF.DocumentDataOfCar)(jsonResult)

        '    Dim contractName_2 As String = System.Configuration.ConfigurationManager.AppSettings("contractName_2").ToString()
        '    Dim contractName_WS As String = DocumentDataOfCar.loan.contract.status.name

        '    If (contractName_WS = contractName_2) Then
        '        msg_CI = "OK"
        '    Else
        '        msg_CI = "El contrato debe estar en estado " + contractName_2
        '    End If
        'Else
        '    msg_CI = "Error Al consultar WS, favor de intentarlo nuevamente"
        'End If
        Return msg_CI
    End Function

    Private Function getNumCI(ByVal IdSol As String) As String
        Dim NumCliente As String = String.Empty

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_Num_Contract_CI_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", IdSol)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                NumCliente = reader(0).ToString()
            Loop

        Catch ex As Exception
            NumCliente = "NOT_FOUND"
        End Try

        sqlConnection1.Close()

        Return NumCliente
    End Function

    Private Function getIsCI(ByVal IdSol As String) As Boolean
        Dim CI As Boolean = False

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_InfoCI_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", IdSol)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                CI = reader(0).ToString()
            Loop

        Catch ex As Exception
            CI = False
        End Try

        sqlConnection1.Close()

        Return CI
    End Function
End Class
