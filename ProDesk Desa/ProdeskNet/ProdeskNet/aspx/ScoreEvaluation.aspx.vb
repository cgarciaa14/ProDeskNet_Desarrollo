'BBV-P-423: AMR: 12/04/2017 RQADM-18 Evaluación
'BUG-PD-67 GVARGAS 05/06/2017 Cambios Hermes
'BUG-PD-72 GVARGAS 07/06/2017 Cambio fecha when fecha value default
'BUG-PD-77 GVARGAS 05/06/2017 Retoques Hermes
'BUG-PD-87 GVARGAS 10/06/2017 Ajustes de hermes
'BUG-PD-127 GVARGAS 10/06/2017 avoid save Hermes
'BUG-PD-139 GVARGAS 04/07/17 Correcion error al regresar tarea
'BBV-P-423 RQACTPRE-01 GVARGAS 03/07/2017 Validar años residencia
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
'BUG-PD-172 GVARGAS 25/07/17 Reglas Elicitadas
'BUG-PD-189 GVARGAS 16/08/17 Add ZipCode & AutomaticPayment
'BUG-PD-196 GVARGAS 30/08/2017 Cambio estatus credito
'BUG-PD-214 GVARGAS 25/09/2017 Cambio Urgentes
'BUG-PD-216 GVARGAS 29/09/2017 Cambios mostrar info
'BUG-PD-203: CGARCIIA: 04/09/2017: SE AGREGA NOTIFICACIONES 
'BUG-PD-215: CGARCIA: 26/09/2017: ACTUALIZACION DE CORREOS PARA CASOS AL QUE ENTRA.
'BUG-PD-220: CGARCIA: 02/10/2017: ACTUALIZACION DE EMAIL.
'BUG-PD-223: CGARCIA: 03/10/2017: ACTUALIZACION DE EMAIL EN SCORING
'BUG-PD-227: CGARCIA: 05/10/2017: ACTUALIZACION DE EMAILS Y SCORE
'BBV-P-423 RQ-PI7-PD8 GVARGAS 18/10/2017 Cambios generales mensajes
'BUG-PD-255 GVARGAS 30/10/2017 Cambio mensaje error Hermes
'BUG-PD-313: DJUAREZ: 27/12/2017: Se evita el avanzar tareas cuando se presiona F5 cuando esta cargando una pagina.
'RQ-PD-23: ERODRIGUEZ: 15/02/2018: Nuevo servicio getScoreEvaluation
'BUG-PD-406: ERODRIGUEZ: 26/03/2018: Corrección en codigo postal de agencia
'BUG-PD-419: ERODRIGUEZ: 13/04/18 cambio a clase de clsGetScoreEvaluation

Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports ProdeskNet.WCF

Partial Class aspx_ScoreEvaluation
    Inherits System.Web.UI.Page

    Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim strError As String = String.Empty
    Public ClsEmail As New ProdeskNet.SN.clsEmailAuto()


    Private Sub aspx_ScoreEvaluation_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'Validacion de Request
            Dim Validate As New clsValidateData
            Dim Url As String = Validate.ValidateRequest(Request)

            If Url <> String.Empty Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "", True)
                Exit Sub
            End If
            'Fin validacion de Request

            Dim objpant As New ProdeskNet.SN.clsPantallas()
            Dim dts As New DataSet()

            dts = objpant.CargaPantallas(Val(Request("idPantalla")).ToString)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Then
                        btnprocesar_Click(btnprocesar, Nothing)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnprocesar_Click(sender As Object, e As EventArgs) Handles btnprocesar.Click
        Dim url As String = "PopUpLetreroRedirectSpecial('Ocurrio un error favor de intentarlo nuevamente.', '../aspx/consultaPanelControl.aspx');"

        Try
            'Dim Hermes As Hermes_Resp = fill_Info_Hermes(Val(Request("Sol").ToString()))
            Dim fillhermes As New clsGetScoreEvaluation
            Dim Hermes As clsGetScoreEvaluation.Hermes_Resp = fillhermes.fill_Info_Hermes(Val(Request("Sol").ToString()))
            'Dim msg As mensajeCod = getMessage(Hermes.dictamenFinal, Hermes.valido)
            Dim msg As clsGetScoreEvaluation.mensajeCod = fillhermes.getMessage(Hermes.dictamenFinal, Hermes.valido)

            If (msg.path = 0) Then
                url = asignaTarea(msg.mensaje)
            ElseIf (msg.path = 1) Then
                url = CancelaTarea(msg.code, msg.mensaje)
            Else
                url = NextStep(CDbl(Val(Hermes.score)), msg.mensaje)
            End If
        Catch ex As Exception
            url = asignaTarea("Servicio no disponible: " + ex.Message.ToString())
        End Try

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", url, True)
    End Sub

    Private Function fill_Info_Hermes(ByVal folio As String) As Hermes_Resp
        Dim Hermes_Respuesta As Hermes_Resp = New Hermes_Resp()
        Hermes_Respuesta.valido = False
        Hermes_Respuesta.dictamenFinal = String.Empty
        Hermes_Respuesta.motivoRechazo = String.Empty
        Hermes_Respuesta.score = String.Empty

        Try

            Dim Hermes_DB_Optimo_JSON As String = getInfoJsonHermesOptimo(folio)

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            'Dim HermesJSON As String = serializer.Serialize(Hermes)

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
            rest.Uri = System.Configuration.ConfigurationManager.AppSettings("getScoreEvaluation").ToString()
            rest.buscarHeader("error-code")

            Dim jsonResult As String = rest.ConnectionPost(userID, iv_ticket1, Hermes_DB_Optimo_JSON)
            If (rest.IsError = False) Then
                'Dim Hermes_WS As Hermes_Resp_WS = serializer.Deserialize(Of Hermes_Resp_WS)(jsonResult)
                Dim Hermes_WS As Hermes_Resp_WS = serializer.Deserialize(Of Hermes_Resp_WS)(jsonResult)
                Hermes_Respuesta.valido = True
                Hermes_Respuesta.score = Hermes_WS.scoringResults.maximumLimit
                Hermes_Respuesta.dictamenFinal = Hermes_WS.scoringResults.finalDictum
                Hermes_Respuesta.motivoRechazo = Hermes_WS.scoringResults.antifraudDictum

                Hermes_WS.scoringResults.payloadEnvio = Hermes_DB_Optimo_JSON.ToString()
                Hermes_WS.scoringResults.payloadRecibido = jsonResult.ToString()

                'Hermes_Respuesta.msg = rest.valorHeader

                'Dim save As String = SaveInfoHermesResponse(Hermes_WS, folio)
                Dim save As String = SaveInfoHermesResponseOptimo(Hermes_WS, folio)
            Else
                Hermes_Respuesta.valido = False

                If rest.valorHeader <> "" Then
                    Hermes_Respuesta.dictamenFinal = rest.valorHeader + " " + rest.MensajeError

                    'Dim Hermes_WS As Hermes_Resp_WS = New Hermes_Resp_WS()
                    Dim Hermes_WS As Hermes_Resp_WS = New Hermes_Resp_WS()
                    Hermes_WS.scoringResults.balance.amount = 0.0
                    Hermes_WS.scoringResults.finalDictum = rest.valorHeader + " " + rest.MensajeError
                    Hermes_WS.scoringResults.antifraudDictum = rest.valorHeader + " " + rest.MensajeError
                    Hermes_WS.scoringResults.antifraud = ""
                    Hermes_WS.scoringResults.paymentCapacity = ""
                    Hermes_WS.scoringResults.reference = ""
                    Hermes_WS.scoringResults.score = ""
                    Hermes_WS.scoringResults.rejectionPolicy = 0
                    Hermes_WS.scoringResults.maximumLimit = 0.0

                    Hermes_WS.scoringResults.payloadEnvio = Hermes_DB_Optimo_JSON.ToString()
                    Hermes_WS.scoringResults.payloadRecibido = jsonResult.ToString()
                    Dim save As String = SaveInfoHermesResponseOptimo(Hermes_WS, folio)
                    If save = "SI" Then
                        Hermes_Respuesta.valido = True
                    End If
                Else
                    Hermes_Respuesta.dictamenFinal = rest.MensajeError
                End If

            End If

        Catch ex As Exception
            Hermes_Respuesta.valido = False
        End Try
        Return Hermes_Respuesta
    End Function

    Public Class Hermes_Optimo
        Public branch As genericID = New genericID()
        Public id As String
        Public customer As customer_Optimo = New customer_Optimo()
        Public coaccreditedCustomer As customer_Optimo_coac = New customer_Optimo_coac()
        Public loanBase As loanBase_Optimo = New loanBase_Optimo()
        Public numberTimesReferenceFoundClient As Double
        Public numberTimesReferenceFoundCoaccredited As String
        Public moratoriumRates As List(Of Double) = New List(Of Double)()
        Public originType As String
        Public referenceNumberUG As String
        Public cbScore As cbScore_Optimo = New cbScore_Optimo()
        Public agency As agency_Optimo = New agency_Optimo()
    End Class
    Public Class agency_Optimo
        Public id As Double
        Public address As address_ = New address_()
        Public divisionId As Double
        Public status As Double
    End Class
    Public Class cbScore_Optimo
        Public loanToValue As genericAmount = New genericAmount()
    End Class
    Public Class loanBase_Optimo
        Public subProductCode As String
        Public iLoanDetail As iLoanDetail_Optimo = New iLoanDetail_Optimo()
        Public loan As loan_Optimo = New loan_Optimo()
        Public extendedData As extendedData_2_Optimo = New extendedData_2_Optimo()
    End Class
    Public Class extendedData_2_Optimo
        Public preapprovedAmount As genericAmount = New genericAmount()
        Public expiredDebtPayed As Double
    End Class
    Public Class loan_Optimo
        Public loanInstallments As loanInstallments_Optimo = New loanInstallments_Optimo()
        Public loanProduct As loanProduct_Optimo = New loanProduct_Optimo()
    End Class
    Public Class loanProduct_Optimo
        Public interestRate As Double
    End Class
    Public Class loanInstallments_Optimo
        Public installmentAmount As genericAmount = New genericAmount()
    End Class
    Public Class iLoanDetail_Optimo
        Public loanCar As loanCar_Optimo = New loanCar_Optimo()
    End Class
    Public Class loanCar_Optimo
        Public car As car_ = New car_()
        Public termNumber As String
        Public invoice As invoice_Optimo = New invoice_Optimo()
        Public requestAmount As genericAmount = New genericAmount()
        Public preapprovedBrand As String
        Public use As String
        Public initialAmount As genericAmount = New genericAmount()
        Public percentageInitialAmount As Double
    End Class
    Public Class invoice_Optimo
        Public amount As genericAmount = New genericAmount()
    End Class

    Public Class customer_Optimo
        Public person As person_Optimo = New person_Optimo()
        'no esta en bd
        Public relationshipGFP As String
        Public membershipDate As String
        Public seniorityDate As String
        Public membershipSeniority As String
        Public automaticPaymentIndicator As String
    End Class
    Public Class customer_Optimo_coac
        'Inherits customer_Optimo
        Public person As person_Optimo_coac = New person_Optimo_coac()
        Public relationshipGFP As String
        Public membershipDate As String
        Public seniorityDate As String
        Public membershipSeniority As String
        Public automaticPaymentIndicator As Boolean
    End Class

    Public Class person_Optimo
        Public extendedData As extended_DataOptimo = New extended_DataOptimo()
        Public id As String
        Public lastName As String
        Public mothersLastName As String
        Public name As String
        'Public addresses As List(Of zipCode) = New List(Of zipCode)()
        Public addresses As List(Of address_Optimo) = New List(Of address_Optimo)()
        Public birthDate As String
        Public economicData As economicData_Optimo = New economicData_Optimo()
        Public contactsInformation As List(Of telephone_Optimo) = New List(Of telephone_Optimo)()
        Public identityDocument As identityDocument_ = New identityDocument_()
        Public nationality As String
        Public isCustomer As String
        Public type As String

    End Class
    Public Class person_Optimo_coac
        'Inherits person_Optimo
        Public extendedData As extended_DataOptimo_coac = New extended_DataOptimo_coac()
        'Public id As String
        Public lastName As String
        Public mothersLastName As String
        Public name As String
        'Public addresses As List(Of zipCode) = New List(Of zipCode)()
        Public addresses As List(Of address_Optimo_coac) = New List(Of address_Optimo_coac)()
        Public birthDate As String
        Public economicData As economicData_Optimo_coac = New economicData_Optimo_coac()
        Public contactsInformation As List(Of telephone_Optimo_coac) = New List(Of telephone_Optimo_coac)()
        Public identityDocument As identityDocument_ = New identityDocument_()
        Public nationality As String
        Public isCustomer As String
        Public type As String
    End Class

    Public Class extended_DataOptimo
        Public rfc As String
        Public sex As String
        ''Public populationRegisterId As String  '''checar si va
        Public homonym As String
        Public facebook As Integer
        Public age As String
        Public maritalStatus As String
    End Class
    Public Class extended_DataOptimo_coac
        Public rfc As String
        Public sex As String
        Public populationRegisterId As String  '''checar si va
        Public homonym As String
        'Public facebook As String
        Public age As String
        Public maritalStatus As String
    End Class
    Public Class economicData_Optimo
        Public extendedData As extendedData_1_Optimo = New extendedData_1_Optimo()
        'nueva propiedad
        Public profession As String
    End Class
    Public Class economicData_Optimo_coac
        Public extendedData As extendedData_1_Optimo_coac = New extendedData_1_Optimo_coac()
        'nueva propiedad
        Public profession As String
    End Class

    Public Class extendedData_1_Optimo
        Public automaticPayment As automaticPayment_ = New automaticPayment_()
        Public othersIncomes As List(Of genericAmount) = New List(Of genericAmount)()  ''esta clase es opcional para coacreditado
        Public variableIncomes As genericAmount = New genericAmount() ''esta clase es opcional para coacreditado
        Public fixedIncomes As genericAmount = New genericAmount() ''esta clase es opcional para coacreditado
        'propiedad nueva
        Public schoolingLevel As String
        Public economicDependants As String
        Public [property] As Propert_y_ = New Propert_y_()
        Public occupation As String
        Public jobLocation As String
        Public activityJob As String
        Public employmentSituation As String
        Public jobSeniority As String
        Public hireDate As String
        Public businessActivity As String
        Public subgroup As String
        Public rentalExpenditure As String
        Public mortgageExpenditure As String
        'No esta en bd
    End Class
    Public Class extendedData_1_Optimo_coac
        Public automaticPayment As automaticPayment_coac = New automaticPayment_coac()
        'propiedad nueva
        Public schoolingLevel As String
        Public economicDependants As String
        Public [property] As Propert_y_ = New Propert_y_()
        Public occupation As String
        Public jobLocation As String
        Public activityJob As String
        Public employmentSituation As String
        Public jobSeniority As String
        Public hireDate As String
        Public businessActivity As String
        Public subgroup As String
        Public rentalExpenditure As String
        Public mortgageExpenditure As String
        'No esta en bd
    End Class
    Public Class telephone_Optimo
        Public telephone As telephone_v_Optimo = New telephone_v_Optimo()
    End Class
    Public Class telephone_Optimo_coac
        Public telephone As telephone_v_Optimo_coac = New telephone_v_Optimo_coac()
    End Class
    Public Class telephone_v_Optimo
        Public telephoneCompany As genericID = New genericID() ''esta clase es opcional para coacreditado
        'nuevo campo'
        Public prefix As String
        Public number As String
        'no se tiene en bd

        Public phoneExtension As String
    End Class
    Public Class telephone_v_Optimo_coac
        'Public telephoneCompany As genericID = New genericID() ''esta clase es opcional para coacreditado
        'nuevo campo'
        Public prefix As String
        Public number As String
        'no se tiene en bd

        Public phoneExtension As String
    End Class
    Public Class address_Optimo
        Public streetName As String
        Public streetNumber As String  ''checar si va
        Public neighborhood As String
        Public city As String
        Public state As String
        Public zipCode As String
        Public outdoorNumber As String
        Public insideNumber As String
        Public startingResidenceDate As String
        Public country As country_ = New country_()

        ''Public nationality As String
        'faltan mapear a bd


    End Class
    Public Class address_Optimo_coac
        Public streetName As String
        Public streetNumber As String  ''checar si va
        Public neighborhood As String
        Public door As String
        Public city As String
        Public state As String
        Public zipCode As String
        Public county As String
        Public outdoorNumber As String
        Public insideNumber As String
        Public startingResidenceDate As String
        Public country As country_ = New country_()

        ''Public nationality As String
        'faltan mapear a bd


    End Class
    'faltan mapear a bd
    Public Class country_
        Public id As String
    End Class
    'No esta en bd
    Public Class Propert_y_
        Public commercialValue As String
        Public realStateAge As String
        Public conservationStatus As String
    End Class
    Public Class identityDocument_
        Public id As String
        Public identityType As String
    End Class

    Public Class agency_
        Public id As Integer
        Public address As address_ = New address_()
        Public divisionId As Integer
        Public status As Integer
    End Class

    Public Class address_
        Public zipCode As String
    End Class

    Public Class cbScore_
        Public loanToValue As genericAmount = New genericAmount()
    End Class

    Public Class genericID
        Public id As String
    End Class

    Public Class genericAmount
        Public amount As Double
    End Class

    Public Class customer_
        Public person As person_ = New person_()
    End Class

    Public Class person_
        Public extendedData As extendedData_ = New extendedData_()
        Public id As String
        Public economicData As economicData_ = New economicData_()
        Public contactsInformation As List(Of telephone_) = New List(Of telephone_)()

        Public addresses As List(Of zipCode) = New List(Of zipCode)()
    End Class

    Public Class zipCode
        Public zipCode As String
    End Class

    Public Class extendedData_
        Public facebook As Integer
    End Class

    Public Class economicData_
        Public extendedData As extendedData_1 = New extendedData_1()
    End Class

    Public Class extendedData_1
        Public automaticPayment As automaticPayment_ '= New automaticPayment_()
        Public othersIncomes As List(Of genericAmount) = New List(Of genericAmount)()
        Public variableIncomes As genericAmount = New genericAmount()
        Public fixedIncomes As genericAmount = New genericAmount()
    End Class

    Public Class automaticPayment_
        Public active As Integer
    End Class
    Public Class automaticPayment_coac
        Public active As Integer
    End Class

    Class automaticPayment_Fill
        'Inherits automaticPayment_

        Public active As Integer
    End Class


    Public Class telephone_
        Public telephone As telephone_v = New telephone_v()
    End Class

    Public Class telephone_v
        Public telephoneCompany As genericID = New genericID()
    End Class

    Public Class loanBase_
        Public subProductCode As String
        Public iLoanDetail As iLoanDetail_ = New iLoanDetail_()
        Public loan As loan_ = New loan_()
        Public extendedData As extendedData_2 = New extendedData_2()
    End Class

    Public Class extendedData_2
        Public preapprovedAmount As genericAmount = New genericAmount()
        Public expiredDebtPayed As Integer
    End Class

    Public Class loan_
        Public loanInstallments As loanInstallments_ = New loanInstallments_()
        Public loanProduct As loanProduct_ = New loanProduct_()
    End Class

    Public Class loanProduct_
        Public interestRate As Double
    End Class

    Public Class loanInstallments_
        Public installmentAmount As genericAmount = New genericAmount()
    End Class

    Public Class iLoanDetail_
        Public loanCar As loanCar_ = New loanCar_()
    End Class

    Public Class loanCar_
        Public car As car_ = New car_()
        Public termNumber As String
        Public invoice As invoice_ = New invoice_()
        Public requestAmount As genericAmount = New genericAmount()
        Public preapprovedBrand As String
        Public use As String
        Public initialAmount As genericAmount = New genericAmount()
        Public percentageInitialAmount As String
    End Class

    Public Class car_
        Public model As String
        Public brand As String
        Public subBrand As String
        Public isUsed As String
        Public aliance As String
    End Class

    Public Class invoice_
        Public amount As genericAmount = New genericAmount()
    End Class


#Region "hermesdb"
    Public Class genericHO
        Public value As String
        Public lenght As Integer
        Public Type_value As String
    End Class

    Public Class branch
        Inherits genericHO
    End Class
    Public Class id_HDB
        Inherits genericHO
    End Class
    Public Class facebook
        Inherits genericHO
    End Class
    Public Class person_id
        Inherits genericHO
    End Class
    Public Class automaticPayment
        Inherits genericHO
    End Class
    Public Class PaymentIndicator
        Inherits genericHO
    End Class
    Public Class othersIncomes_1
        Inherits genericHO
    End Class
    Public Class othersIncomes_2
        Inherits genericHO
    End Class
    Public Class othersIncomes_3
        Inherits genericHO
    End Class
    Public Class variableIncomes
        Inherits genericHO
    End Class
    Public Class fixedIncomes
        Inherits genericHO
    End Class
    Public Class telephoneCompany_id
        Inherits genericHO
    End Class
    Public Class subProductCode
        Inherits genericHO
    End Class
    Public Class model
        Inherits genericHO
    End Class
    Public Class brand
        Inherits genericHO
    End Class
    Public Class subBrand
        Inherits genericHO
    End Class
    Public Class isUsed
        Inherits genericHO
    End Class
    Public Class aliance
        Inherits genericHO
    End Class
    Public Class termNumber
        Inherits genericHO
    End Class
    Public Class invoice_amount
        Inherits genericHO
    End Class
    Public Class requestAmount_amount
        Inherits genericHO
    End Class
    Public Class preapprovedBrand
        Inherits genericHO
    End Class
    Public Class use
        Inherits genericHO
    End Class
    Public Class initialAmount_amount
        Inherits genericHO
    End Class
    Public Class percentageInitialAmount
        Inherits genericHO
    End Class
    Public Class installmentAmount_amount
        Inherits genericHO
    End Class
    Public Class loanProduct_interestRate
        Inherits genericHO
    End Class
    Public Class preapprovedAmount_amount
        Inherits genericHO
    End Class
    Public Class expiredDebtPayed
        Inherits genericHO
    End Class
    Public Class numberTimesReferenceFoundClient
        Inherits genericHO
    End Class
    Public Class moratoriumRates_1
        Inherits genericHO
    End Class
    Public Class moratoriumRates_2
        Inherits genericHO
    End Class
    Public Class moratoriumRates_3
        Inherits genericHO
    End Class
    Public Class originType
        Inherits genericHO
    End Class
    Public Class referenceNumberUG
        Inherits genericHO
    End Class
    Public Class loanToValue
        Inherits genericHO
    End Class
    Public Class agency_id
        Inherits genericHO
    End Class
    Public Class agency_zipCode
        Inherits genericHO
    End Class
    Public Class divisionId
        Inherits genericHO
    End Class
    Public Class agency_status
        Inherits genericHO
    End Class
    Public Class addresses_zp
        Inherits genericHO
    End Class
    Public Class directi_zp
        Inherits genericHO
    End Class
    Public Class streetName
        Inherits genericHO
    End Class
    Public Class neighborhood
        Inherits genericHO
    End Class
    Public Class insideNumber
        Inherits genericHO
    End Class
    Public Class outdoorNumber
        Inherits genericHO
    End Class
    Public Class state
        Inherits genericHO
    End Class
    Public Class nationality
        Inherits genericHO
    End Class
    Public Class contactinfo_telephone
        Inherits genericHO
    End Class
    Public Class economicDependants
        Inherits genericHO
    End Class
    Public Class extendedData_occupation
        Inherits genericHO
    End Class
    Public Class extendedData_jobLocation
        Inherits genericHO
    End Class
    Public Class extendedData_activityJob
        Inherits genericHO
    End Class
    Public Class extendedData_employmentSituation
        Inherits genericHO
    End Class
    Public Class extendedData_jobSeniority
        Inherits genericHO
    End Class
    Public Class extendedData_businessActivity
        Inherits genericHO
    End Class
    Public Class person_type
        Inherits genericHO
    End Class
    Public Class extendedData_rentalExpenditure
        Inherits genericHO
    End Class
    Public Class extendedData_rfc
        Inherits genericHO
    End Class
    Public Class extendedData_age
        Inherits genericHO
    End Class
    Public Class extendedData_homonym
        Inherits genericHO
    End Class
    Public Class extendedData_sex
        Inherits genericHO
    End Class
    Public Class extendedData_maritalStatus
        Inherits genericHO
    End Class
    Public Class lastName
        Inherits genericHO
    End Class
    Public Class mothersLastName
        Inherits genericHO
    End Class
    Public Class name
        Inherits genericHO
    End Class
    Public Class addresscity
        Inherits genericHO
    End Class
    Public Class startingResidenceDate
        Inherits genericHO
    End Class
    Public Class country_id
        Inherits genericHO
    End Class
    Public Class birth_date
        Inherits genericHO
    End Class
    Public Class schoolingLevel
        Inherits genericHO
    End Class
    Public Class commercialValue
        Inherits genericHO
    End Class
    Public Class realStateAge
        Inherits genericHO
    End Class
    Public Class conservationStatus
        Inherits genericHO
    End Class
    Public Class HireData
        Inherits genericHO
    End Class
    Public Class mortgageExpenditure
        Inherits genericHO
    End Class
    Public Class profession
        Inherits genericHO
    End Class
    Public Class c_phone_number
        Inherits genericHO
    End Class
    Public Class c_phone_prefix
        Inherits genericHO
    End Class
    Public Class c_phone_ext
        Inherits genericHO
    End Class
    Public Class identityType
        Inherits genericHO
    End Class
    Public Class identityNumber
        Inherits genericHO
    End Class
    Public Class isCustomer
        Inherits genericHO
    End Class
    Public Class relationshipGFP
        Inherits genericHO
    End Class
    Public Class membershipdate
        Inherits genericHO
    End Class
    Public Class senioritydate
        Inherits genericHO
    End Class
    Public Class membershipseniority
        Inherits genericHO
    End Class
    Public Class subgroup
        Inherits genericHO
    End Class

#End Region
    Public Class HermesDB_Optimo
        Public branch As branch = New branch With {.lenght = 4, .Type_value = "str"}

        Public id_HDB As id_HDB = New id_HDB With {.lenght = 10, .Type_value = "str"}
        Public facebook As facebook = New facebook With {.lenght = 1, .Type_value = "num"}
        Public person_id As person_id = New person_id With {.lenght = 8, .Type_value = "str"}

        Public rfc As extendedData_rfc = New extendedData_rfc With {.lenght = 10, .Type_value = "str"}
        Public sex As extendedData_sex = New extendedData_sex With {.lenght = 1, .Type_value = "str"}
        Public homo As extendedData_homonym = New extendedData_homonym With {.lenght = 4, .Type_value = "str"}
        Public age As extendedData_age = New extendedData_age With {.lenght = 3, .Type_value = "num"}
        Public maritalstatus As extendedData_maritalStatus = New extendedData_maritalStatus With {.lenght = 1, .Type_value = "str"}

        Public lastname As lastName = New lastName With {.lenght = 20, .Type_value = "str"}
        Public mothersLastName As mothersLastName = New mothersLastName With {.lenght = 20, .Type_value = "str"}
        Public Name As name = New name With {.lenght = 20, .Type_value = "str"}
        Public birthdate As birth_date = New birth_date With {.lenght = 10, .Type_value = "str"}
        Public schoolingLevel As schoolingLevel = New schoolingLevel With {.lenght = 3, .Type_value = "num"}

        Public automaticPayment As automaticPayment = New automaticPayment With {.lenght = 1, .Type_value = "num"} ''checar tipo de datos
        Public PaymentIndicator As PaymentIndicator = New PaymentIndicator With {.lenght = 5, .Type_value = "str"}
        Public othersIncomes_1 As othersIncomes_1 = New othersIncomes_1 With {.lenght = 18, .Type_value = "num_15_02"}
        Public othersIncomes_2 As othersIncomes_2 = New othersIncomes_2 With {.lenght = 18, .Type_value = "num_15_02"}
        Public othersIncomes_3 As othersIncomes_3 = New othersIncomes_3 With {.lenght = 18, .Type_value = "num_15_02"}
        Public variableIncomes As variableIncomes = New variableIncomes With {.lenght = 18, .Type_value = "num_15_02"}
        Public fixedIncomes As fixedIncomes = New fixedIncomes With {.lenght = 18, .Type_value = "num_15_02"}

        Public subProductCode As subProductCode = New subProductCode With {.lenght = 4, .Type_value = "str"}

        Public model As model = New model With {.lenght = 4, .Type_value = "str"}
        Public brand As brand = New brand With {.lenght = 3, .Type_value = "num"}  ''se cambio de acuerdo al json de prueba
        Public subBrand As subBrand = New subBrand With {.lenght = 2, .Type_value = "num"} ''se cambio de acuerdo al json de prueba
        Public isUsed As isUsed = New isUsed With {.lenght = 1, .Type_value = "str"}
        Public aliance As aliance = New aliance With {.lenght = 2, .Type_value = "str"}

        Public termNumber As termNumber = New termNumber With {.lenght = 2, .Type_value = "str"}
        Public invoice_amount As invoice_amount = New invoice_amount With {.lenght = 18, .Type_value = "num_15_02"}
        Public requestAmount_amount As requestAmount_amount = New requestAmount_amount With {.lenght = 18, .Type_value = "num_15_02"}

        Public preapprovedBrand As preapprovedBrand = New preapprovedBrand With {.lenght = 2, .Type_value = "str"}
        Public use As use = New use With {.lenght = 1, .Type_value = "num"}
        Public initialAmount_amount As initialAmount_amount = New initialAmount_amount With {.lenght = 18, .Type_value = "num_15_02"}
        Public percentageInitialAmount As percentageInitialAmount = New percentageInitialAmount With {.lenght = 8, .Type_value = "num_05_02"}

        Public installmentAmount_amount As installmentAmount_amount = New installmentAmount_amount With {.lenght = 18, .Type_value = "num_15_02"}
        Public loanProduct_interestRate As loanProduct_interestRate = New loanProduct_interestRate With {.lenght = 7, .Type_value = "num_04_02"}

        Public preapprovedAmount_amount As preapprovedAmount_amount = New preapprovedAmount_amount With {.lenght = 18, .Type_value = "num_15_02"}

        Public expiredDebtPayed As expiredDebtPayed = New expiredDebtPayed With {.lenght = 1, .Type_value = "num"}

        Public numberTimesReferenceFoundClient As numberTimesReferenceFoundClient = New numberTimesReferenceFoundClient With {.lenght = 2, .Type_value = "num"}
        Public moratoriumRates_1 As moratoriumRates_1 = New moratoriumRates_1 With {.lenght = 16, .Type_value = "num_08_07"}
        Public moratoriumRates_2 As moratoriumRates_2 = New moratoriumRates_2 With {.lenght = 16, .Type_value = "num_08_07"}
        Public moratoriumRates_3 As moratoriumRates_3 = New moratoriumRates_3 With {.lenght = 16, .Type_value = "num_08_07"}
        Public originType As originType = New originType With {.lenght = 3, .Type_value = "str"}
        Public referenceNumberUG As referenceNumberUG = New referenceNumberUG With {.lenght = 20, .Type_value = "str"}

        Public loanToValue As loanToValue = New loanToValue With {.lenght = 9, .Type_value = "num"}

        Public agency_id As agency_id = New agency_id With {.lenght = 10, .Type_value = "num"}

        Public agency_zipCode As agency_zipCode = New agency_zipCode With {.lenght = 5, .Type_value = "str"}
        Public divisionId As divisionId = New divisionId With {.lenght = 10, .Type_value = "str"}
        Public agency_status As agency_status = New agency_status With {.lenght = 1, .Type_value = "num"}

        Public addresses_zp As addresses_zp = New addresses_zp With {.lenght = 5, .Type_value = "str"}

        'Campos nuevos
        Public streetName As streetName = New streetName With {.lenght = 50, .Type_value = "str"}
        Public neighborhood As neighborhood = New neighborhood With {.lenght = 30, .Type_value = "str"}
        Public insideNumber As insideNumber = New insideNumber With {.lenght = 8, .Type_value = "str"}
        Public outdoorNumber As outdoorNumber = New outdoorNumber With {.lenght = 8, .Type_value = "str"}
        Public directi_zp As directi_zp = New directi_zp With {.lenght = 5, .Type_value = "str"}

        Public address_city As addresscity = New addresscity With {.lenght = 30, .Type_value = "str"}
        Public startingResidence_Date As startingResidenceDate = New startingResidenceDate With {.lenght = 10, .Type_value = "str"}
        Public address_countryid As country_id = New country_id With {.lenght = 4, .Type_value = "str"}

        Public commercialValue As commercialValue = New commercialValue With {.lenght = 15, .Type_value = "str"}
        Public realStateAge As realStateAge = New realStateAge With {.lenght = 2, .Type_value = "num"}
        Public conservationStatus As conservationStatus = New conservationStatus With {.lenght = 3, .Type_value = "num"}

        Public subgroup As subgroup = New subgroup With {.lenght = 1, .Type_value = "str"}

        Public state As state = New state With {.lenght = 2, .Type_value = "str"}
        Public nationality As nationality = New nationality With {.lenght = 4, .Type_value = "str"}

        Public telephoneCompany_id As telephoneCompany_id = New telephoneCompany_id With {.lenght = 2, .Type_value = "num"}
        Public telephoneNumber As c_phone_number = New c_phone_number With {.lenght = 7, .Type_value = "str"}
        'Public contactinfo_telephone As contactinfo_telephone = New contactinfo_telephone With {.lenght = 7, .Type_value = "str"}
        Public contactinfo_prefix As c_phone_prefix = New c_phone_prefix With {.lenght = 3, .Type_value = "str"}
        Public contactinfo_ext As c_phone_ext = New c_phone_ext With {.lenght = 4, .Type_value = "str"}

        Public telephoneCompany1_id As telephoneCompany_id = New telephoneCompany_id With {.lenght = 2, .Type_value = "num"}
        Public telephoneNumber1 As c_phone_number = New c_phone_number With {.lenght = 7, .Type_value = "str"}
        Public contactinfo_prefix1 As c_phone_prefix = New c_phone_prefix With {.lenght = 3, .Type_value = "str"}
        Public contactinfo_ext1 As c_phone_ext = New c_phone_ext With {.lenght = 4, .Type_value = "str"}

        Public telephoneCompany2_id As telephoneCompany_id = New telephoneCompany_id With {.lenght = 2, .Type_value = "num"}
        Public telephoneNumber2 As c_phone_number = New c_phone_number With {.lenght = 7, .Type_value = "str"}
        Public contactinfo_prefix2 As c_phone_prefix = New c_phone_prefix With {.lenght = 3, .Type_value = "str"}
        Public contactinfo_ext2 As c_phone_ext = New c_phone_ext With {.lenght = 4, .Type_value = "str"}

        Public telephoneCompany3_id As telephoneCompany_id = New telephoneCompany_id With {.lenght = 2, .Type_value = "num"}
        Public telephoneNumber3 As c_phone_number = New c_phone_number With {.lenght = 7, .Type_value = "str"}
        Public contactinfo_prefix3 As c_phone_prefix = New c_phone_prefix With {.lenght = 3, .Type_value = "str"}
        Public contactinfo_ext3 As c_phone_ext = New c_phone_ext With {.lenght = 4, .Type_value = "str"}

        Public identity_type As identityType = New identityType With {.lenght = 1, .Type_value = "str"}
        Public indentity_number As identityNumber = New identityNumber With {.lenght = 10, .Type_value = "str"}

        Public isCustomer As isCustomer = New isCustomer With {.lenght = 1, .Type_value = "str"}
        Public relationshipGFP As relationshipGFP = New relationshipGFP With {.lenght = 1, .Type_value = "str"}
        Public membershipdate As membershipdate = New membershipdate With {.lenght = 10, .Type_value = "str"}
        Public seniorityDate As senioritydate = New senioritydate With {.lenght = 10, .Type_value = "str"}
        Public membershipSeniority As membershipseniority = New membershipseniority With {.lenght = 3, .Type_value = "str"}

        Public economicDependants As economicDependants = New economicDependants With {.lenght = 2, .Type_value = "num"}
        Public extendedData_occupation As extendedData_occupation = New extendedData_occupation With {.lenght = 30, .Type_value = "str"}
        Public extendedData_jobLocation As extendedData_jobLocation = New extendedData_jobLocation With {.lenght = 50, .Type_value = "str"}
        Public extendedData_activityJob As extendedData_activityJob = New extendedData_activityJob With {.lenght = 3, .Type_value = "str"}
        Public extendedData_employmenteSituation As extendedData_employmentSituation = New extendedData_employmentSituation With {.lenght = 1, .Type_value = "str"}
        Public extendedData_jobSeniority As extendedData_jobSeniority = New extendedData_jobSeniority With {.lenght = 2, .Type_value = "num"}

        Public HireData As HireData = New HireData With {.lenght = 10, .Type_value = "str"}

        Public economicData_profession As profession = New profession With {.lenght = 2, .Type_value = "str"}

        Public extendedData_businessActivity As extendedData_businessActivity = New extendedData_businessActivity With {.lenght = 2, .Type_value = "str"}
        Public person_type As person_type = New person_type With {.lenght = 1, .Type_value = "str"}
        Public extendedData_rentalExpenditure As extendedData_rentalExpenditure = New extendedData_rentalExpenditure With {.lenght = 15, .Type_value = "num"}
        Public mortgageExpenditure As mortgageExpenditure = New mortgageExpenditure With {.lenght = 15, .Type_value = "str"}

        Public mensaje_ As String

        Public newquery As Integer


        'Campos nuevos


    End Class
    Private Function Fill_D_O(newquery As String, elemento As genericHO) As String
        ''funcion para completar ceros o espacios en blanco segun sea el tipo, regresa un string
        If (newquery = 0) Then
            If Not IsNothing(elemento.value) And elemento.value <> "" Then
                Select Case elemento.Type_value
                    Case "str"
                        'If IsNothing(elemento.value) Then
                        '    elemento.value = " "
                        'End If
                        elemento.value = elemento.value.Trim()
                        Dim l_v As Integer = 0
                        l_v = elemento.value.Length()
                        If l_v < elemento.lenght Then
                            For i As Integer = 0 To (elemento.lenght - l_v) - 1
                                elemento.value = elemento.value + " "
                            Next
                        ElseIf l_v > elemento.lenght Then
                            elemento.value = elemento.value.Substring(0, elemento.lenght)
                        End If
                    Case "num"
                        'If IsNothing(elemento.value) Then
                        '    elemento.value = "0"
                        'End If
                        elemento.value = elemento.value.Trim()
                        Dim l_v As Integer = 0
                        l_v = elemento.value.Length()
                        If l_v < elemento.lenght Then
                            For i As Integer = 0 To (elemento.lenght - l_v) - 1
                                elemento.value = "0" + elemento.value
                            Next
                        ElseIf l_v > elemento.lenght Then
                            elemento.value = elemento.value.Substring(l_v - elemento.lenght)
                        End If
                    Case Else
                        If elemento.Type_value.Contains("num") Then
                            'If IsNothing(elemento.value) Then
                            '    elemento.value = "0"
                            'End If
                            Dim left_l As Integer = 0
                            Dim right_l As Integer = 0

                            If Not elemento.value.Contains(".") Then
                                elemento.value = elemento.value + "."
                            End If

                            left_l = Convert.ToInt16(elemento.Type_value.Substring(4, 2))
                            right_l = Convert.ToInt16(elemento.Type_value.Substring(7, 2))
                            elemento.value = elemento.value.Trim()
                            Dim l_v_t As Integer = 0
                            Dim l_v As Integer = 0
                            l_v_t = elemento.value.Length()
                            l_v = (elemento.value.IndexOf("."))
                            If l_v <= left_l - 1 Then
                                For i As Integer = 0 To (left_l - l_v) - 1
                                    elemento.value = "0" + elemento.value
                                Next
                            End If
                            l_v_t = elemento.value.Length()
                            l_v = (elemento.value.IndexOf(".")) + 1
                            If l_v_t <= elemento.lenght - 1 Then
                                For i As Integer = l_v_t To elemento.lenght - 1
                                    elemento.value = elemento.value + "0"
                                Next
                            End If
                        End If
                End Select
            End If
        End If

        Return elemento.value
    End Function

    Private Function getInfoJsonHermesOptimo(ByVal folio As String) As String
        '***
        Dim HermesJSON As String = ""
        Try

            Dim Hermes_DB As HermesDB_Optimo = getInfoHermesDBOptimo(folio)
            Dim Hermes As Hermes_Optimo = New Hermes_Optimo()

            If Hermes_DB.mensaje_ = "Ok" Then

                Dim nq As Integer = Hermes_DB.newquery
                Hermes.branch.id = Fill_D_O(nq, Hermes_DB.branch)
                Hermes.id = Fill_D_O(nq, Hermes_DB.id_HDB)

                Hermes.customer.relationshipGFP = Fill_D_O(nq, Hermes_DB.relationshipGFP)
                Hermes.customer.membershipDate = Fill_D_O(nq, Hermes_DB.membershipdate)
                Hermes.customer.seniorityDate = Fill_D_O(nq, Hermes_DB.seniorityDate)
                Hermes.customer.membershipSeniority = Fill_D_O(nq, Hermes_DB.membershipSeniority)
                Hermes.customer.automaticPaymentIndicator = Fill_D_O(nq, Hermes_DB.PaymentIndicator)

                Hermes.customer.person.extendedData.facebook = Fill_D_O(nq, Hermes_DB.facebook)
                Hermes.customer.person.extendedData.facebook = Hermes_DB.facebook.value
                Hermes.customer.person.id = Fill_D_O(nq, Hermes_DB.person_id)

                Hermes.customer.person.lastName = Fill_D_O(nq, Hermes_DB.lastname)
                Hermes.customer.person.mothersLastName = Fill_D_O(nq, Hermes_DB.mothersLastName)
                Hermes.customer.person.name = Fill_D_O(nq, Hermes_DB.Name)
                Hermes.customer.person.birthDate = Fill_D_O(nq, Hermes_DB.birthdate)

                Dim a1 As address_Optimo = New address_Optimo()
                a1.streetName = Fill_D_O(nq, Hermes_DB.streetName)
                a1.streetNumber = ""  ''checar si va
                a1.neighborhood = Fill_D_O(nq, Hermes_DB.neighborhood)
                a1.city = Fill_D_O(nq, Hermes_DB.address_city)
                a1.state = Fill_D_O(nq, Hermes_DB.state)
                a1.zipCode = Fill_D_O(nq, Hermes_DB.addresses_zp)
                a1.outdoorNumber = Fill_D_O(nq, Hermes_DB.outdoorNumber)
                a1.insideNumber = Fill_D_O(nq, Hermes_DB.insideNumber)
                a1.startingResidenceDate = Fill_D_O(nq, Hermes_DB.startingResidence_Date)
                a1.country.id = Fill_D_O(nq, Hermes_DB.address_countryid)
                Hermes.customer.person.addresses.Add(a1)

                Hermes.customer.person.extendedData.rfc = Fill_D_O(nq, Hermes_DB.rfc)
                'Hermes.customer.person.extendedData.rfc = FHermes_DB.rfc.value
                Hermes.customer.person.extendedData.sex = Fill_D_O(nq, Hermes_DB.sex)
                Hermes.customer.person.extendedData.homonym = Fill_D_O(nq, Hermes_DB.homo)
                Hermes.customer.person.extendedData.age = Fill_D_O(nq, Hermes_DB.age)
                Hermes.customer.person.extendedData.maritalStatus = Fill_D_O(nq, Hermes_DB.maritalstatus)

                Hermes.customer.person.economicData.profession = Fill_D_O(nq, Hermes_DB.economicData_profession)
                Hermes.customer.person.economicData.extendedData.schoolingLevel = Fill_D_O(nq, Hermes_DB.schoolingLevel)
                Hermes.customer.person.economicData.extendedData.economicDependants = Fill_D_O(nq, Hermes_DB.economicDependants)
                Hermes.customer.person.economicData.extendedData.occupation = Fill_D_O(nq, Hermes_DB.extendedData_occupation)
                Hermes.customer.person.economicData.extendedData.jobLocation = Fill_D_O(nq, Hermes_DB.extendedData_jobLocation)
                Hermes.customer.person.economicData.extendedData.activityJob = Fill_D_O(nq, Hermes_DB.extendedData_activityJob)
                Hermes.customer.person.economicData.extendedData.employmentSituation = Fill_D_O(nq, Hermes_DB.extendedData_employmenteSituation)
                Hermes.customer.person.economicData.extendedData.jobSeniority = Fill_D_O(nq, Hermes_DB.extendedData_jobSeniority)
                Hermes.customer.person.economicData.extendedData.businessActivity = Fill_D_O(nq, Hermes_DB.extendedData_businessActivity)

                Hermes.customer.person.economicData.extendedData.hireDate = Fill_D_O(nq, Hermes_DB.HireData)
                Hermes.customer.person.economicData.extendedData.rentalExpenditure = Fill_D_O(nq, Hermes_DB.extendedData_rentalExpenditure)
                Hermes.customer.person.economicData.extendedData.mortgageExpenditure = Fill_D_O(nq, Hermes_DB.mortgageExpenditure)
                Hermes.customer.person.economicData.extendedData.subgroup = Fill_D_O(nq, Hermes_DB.subgroup)

                Hermes.customer.person.economicData.extendedData.property.commercialValue = Fill_D_O(nq, Hermes_DB.commercialValue)
                Hermes.customer.person.economicData.extendedData.property.realStateAge = Fill_D_O(nq, Hermes_DB.realStateAge)
                Hermes.customer.person.economicData.extendedData.property.conservationStatus = Fill_D_O(nq, Hermes_DB.conservationStatus)

                'Dim active As automaticPayment_Fill = New automaticPayment_Fill()
                'active.active = Convert.ToInt16(Fill_D_O(nq,Hermes_DB.automaticPayment))
                'Hermes.customer.person.economicData.extendedData.automaticPayment = active
                Hermes.customer.person.economicData.extendedData.automaticPayment.active = Fill_D_O(nq, Hermes_DB.automaticPayment)  ''checar conversion

                Dim o As genericAmount = New genericAmount()
                Dim o_1 As genericAmount = New genericAmount()
                Dim o_2 As genericAmount = New genericAmount()
                o.amount = CDbl(Fill_D_O(nq, Hermes_DB.othersIncomes_1))  ''checar conversion
                o_1.amount = CDbl(Fill_D_O(nq, Hermes_DB.othersIncomes_2)) ''checar conversion
                o_2.amount = CDbl(Fill_D_O(nq, Hermes_DB.othersIncomes_3))  ''checar conversion
                Hermes.customer.person.economicData.extendedData.othersIncomes.Add(o)  '''warn
                Hermes.customer.person.economicData.extendedData.othersIncomes.Add(o_1) ''warn
                Hermes.customer.person.economicData.extendedData.othersIncomes.Add(o_2) '''warn

                Hermes.customer.person.economicData.extendedData.variableIncomes.amount = CDbl(Fill_D_O(nq, Hermes_DB.variableIncomes)) ''checar conversion
                Hermes.customer.person.economicData.extendedData.fixedIncomes.amount = CDbl(Fill_D_O(nq, Hermes_DB.fixedIncomes)) ''checar conversion

                ''telefono cliente movil obligatorio
                Dim telephone As telephone_Optimo = New telephone_Optimo()
                telephone.telephone.telephoneCompany.id = Fill_D_O(nq, Hermes_DB.telephoneCompany2_id)
                telephone.telephone.number = Fill_D_O(nq, Hermes_DB.telephoneNumber)
                telephone.telephone.prefix = Fill_D_O(nq, Hermes_DB.contactinfo_prefix)
                telephone.telephone.phoneExtension = Fill_D_O(nq, Hermes_DB.contactinfo_ext)
                Hermes.customer.person.contactsInformation.Add(telephone) ''warn
                ''segundo telefono para servicio
                Dim telephone_1 As telephone_Optimo = New telephone_Optimo()
                telephone_1.telephone.telephoneCompany.id = Fill_D_O(nq, Hermes_DB.telephoneCompany2_id)
                telephone_1.telephone.number = Fill_D_O(nq, Hermes_DB.telephoneNumber)
                telephone_1.telephone.prefix = Fill_D_O(nq, Hermes_DB.contactinfo_prefix)
                telephone_1.telephone.phoneExtension = Fill_D_O(nq, Hermes_DB.contactinfo_ext)
                Hermes.customer.person.contactsInformation.Add(telephone_1) ''warn

                Hermes.customer.person.identityDocument.id = Fill_D_O(nq, Hermes_DB.indentity_number)
                Hermes.customer.person.identityDocument.identityType = Fill_D_O(nq, Hermes_DB.identity_type)

                Hermes.customer.person.nationality = Fill_D_O(nq, Hermes_DB.nationality)
                Hermes.customer.person.type = Fill_D_O(nq, Hermes_DB.person_type)
                Hermes.customer.person.isCustomer = Fill_D_O(nq, Hermes_DB.isCustomer)


                Dim zipCode As zipCode = New zipCode()
                zipCode.zipCode = Fill_D_O(nq, Hermes_DB.addresses_zp)
                'Hermes.customer.person.addresses. = zipCode.zipCode  ''warn

                'HARDCODE NECESSARY

                Hermes.coaccreditedCustomer.relationshipGFP = ""
                Hermes.coaccreditedCustomer.membershipDate = "          "
                Hermes.coaccreditedCustomer.seniorityDate = "          "
                Hermes.coaccreditedCustomer.membershipSeniority = "000"
                Hermes.coaccreditedCustomer.automaticPaymentIndicator = False

                'Hermes.coaccreditedCustomer.person.extendedData.facebook = Fill_D_O(nq,Hermes_DB.facebook)
                'Hermes.coaccreditedCustomer.person.id = Fill_D_O(nq,Hermes_DB.person_id)

                Hermes.coaccreditedCustomer.person.lastName = "                    "
                Hermes.coaccreditedCustomer.person.mothersLastName = "                    "
                Hermes.coaccreditedCustomer.person.name = "                    "
                Hermes.coaccreditedCustomer.person.birthDate = "0001-01-01"

                Hermes.coaccreditedCustomer.person.extendedData.rfc = "          "
                Hermes.coaccreditedCustomer.person.extendedData.sex = " "

                Hermes.coaccreditedCustomer.person.extendedData.populationRegisterId = ""   'checar si va a ir

                Hermes.coaccreditedCustomer.person.extendedData.homonym = "    "
                Hermes.coaccreditedCustomer.person.extendedData.age = "000"
                Hermes.coaccreditedCustomer.person.extendedData.maritalStatus = " "

                Dim ca1 As address_Optimo_coac = New address_Optimo_coac()
                ca1.streetName = "                                                  "
                ca1.streetNumber = ""
                ca1.neighborhood = "                              "
                ca1.door = ""
                ca1.city = "                              "
                ca1.state = "  "
                ca1.zipCode = "00000"
                ca1.county = ""
                ca1.outdoorNumber = "        "
                ca1.insideNumber = "        "
                ca1.startingResidenceDate = "          "
                ca1.country.id = "    "
                Hermes.coaccreditedCustomer.person.addresses.Add(ca1)

                'Dim active_ As automaticPayment_Fill = New automaticPayment_Fill()
                'active_.active = "0"
                Hermes.coaccreditedCustomer.person.economicData.extendedData.automaticPayment.active = 0  ''checar conversion


                Hermes.coaccreditedCustomer.person.economicData.extendedData.schoolingLevel = "000"
                Hermes.coaccreditedCustomer.person.economicData.extendedData.economicDependants = "00"
                Hermes.coaccreditedCustomer.person.economicData.extendedData.occupation = "                              "
                Hermes.coaccreditedCustomer.person.economicData.extendedData.jobLocation = "                                                  "
                Hermes.coaccreditedCustomer.person.economicData.extendedData.activityJob = "   "
                Hermes.coaccreditedCustomer.person.economicData.extendedData.employmentSituation = "0"
                Hermes.coaccreditedCustomer.person.economicData.extendedData.jobSeniority = "00"
                Hermes.coaccreditedCustomer.person.economicData.extendedData.businessActivity = "  "

                Hermes.coaccreditedCustomer.person.economicData.extendedData.hireDate = "          "
                Hermes.coaccreditedCustomer.person.economicData.extendedData.rentalExpenditure = "000000000000000"
                Hermes.coaccreditedCustomer.person.economicData.extendedData.mortgageExpenditure = "000000000000000"
                ''Hermes.coaccreditedCustomer.person.economicData.extendedData.subgroup = Fill_D_O(nq,Hermes_DB.subgroup)
                Hermes.coaccreditedCustomer.person.economicData.extendedData.subgroup = " "

                Hermes.coaccreditedCustomer.person.economicData.extendedData.property.commercialValue = "000000000000000"
                Hermes.coaccreditedCustomer.person.economicData.extendedData.property.realStateAge = "00"
                Hermes.coaccreditedCustomer.person.economicData.extendedData.property.conservationStatus = "000"

                Hermes.coaccreditedCustomer.person.economicData.profession = "00"

                Dim telephonec As telephone_Optimo_coac = New telephone_Optimo_coac()
                'telephonec.telephone.telephoneCompany.id = "0"   ''checar campo
                telephonec.telephone.number = "0000000"
                telephonec.telephone.prefix = "000"
                telephonec.telephone.phoneExtension = "0000"
                Hermes.coaccreditedCustomer.person.contactsInformation.Add(telephonec) ''warn

                telephonec = New telephone_Optimo_coac()
                'telephonec.telephone.telephoneCompany.id = "0"   ''checar campo
                telephonec.telephone.number = "0000000"
                telephonec.telephone.prefix = "000"
                telephonec.telephone.phoneExtension = "0000"
                Hermes.coaccreditedCustomer.person.contactsInformation.Add(telephonec) ''warn

                Hermes.coaccreditedCustomer.person.identityDocument.id = "          "
                Hermes.coaccreditedCustomer.person.identityDocument.identityType = "0"

                Hermes.coaccreditedCustomer.person.nationality = "    "
                Hermes.coaccreditedCustomer.person.type = " "
                Hermes.coaccreditedCustomer.person.isCustomer = " "


                'Hermes.coaccreditedCustomer.person.extendedData.facebook = False   ''warn
                'Hermes.coaccreditedCustomer.person.id = ""  ''warn



                'Dim o_ As genericAmount = New genericAmount()
                'o_.amount = 0           ''warn
                'Hermes.coaccreditedCustomer.person.economicData.extendedData.othersIncomes.Add(o_)
                'Hermes.coaccreditedCustomer.person.economicData.extendedData.othersIncomes.Add(o_)
                'Hermes.coaccreditedCustomer.person.economicData.extendedData.othersIncomes.Add(o_)
                'Hermes.coaccreditedCustomer.person.economicData.extendedData.variableIncomes.amount = 0
                'Hermes.coaccreditedCustomer.person.economicData.extendedData.fixedIncomes.amount = 0

                'Dim t As telephone_Optimo = New telephone_Optimo()
                't.telephone.telephoneCompany.id = ""
                'Hermes.coaccreditedCustomer.person.contactsInformation.Add(t)

                Dim zipCode_ As zipCode = New zipCode()
                'zipCode_.zipCode = "00000"
                zipCode_.zipCode = "00000"
                'Hermes.coaccreditedCustomer.person.addresses.Add(zipCode_)
                Hermes.numberTimesReferenceFoundCoaccredited = ""
                'HARDCODE NECESSARY FINALLY

                Hermes.loanBase.subProductCode = Fill_D_O(nq, Hermes_DB.subProductCode)
                Hermes.loanBase.iLoanDetail.loanCar.car.model = Fill_D_O(nq, Hermes_DB.model)
                Hermes.loanBase.iLoanDetail.loanCar.car.brand = Fill_D_O(nq, Hermes_DB.brand)
                Hermes.loanBase.iLoanDetail.loanCar.car.subBrand = Fill_D_O(nq, Hermes_DB.subBrand)
                Hermes.loanBase.iLoanDetail.loanCar.car.isUsed = Fill_D_O(nq, Hermes_DB.isUsed)
                Hermes.loanBase.iLoanDetail.loanCar.car.aliance = Fill_D_O(nq, Hermes_DB.aliance)

                Hermes.loanBase.iLoanDetail.loanCar.termNumber = Fill_D_O(nq, Hermes_DB.termNumber)
                Hermes.loanBase.iLoanDetail.loanCar.invoice.amount.amount = CDbl(Fill_D_O(nq, Hermes_DB.invoice_amount)) ''checar esta conversion
                Hermes.loanBase.iLoanDetail.loanCar.requestAmount.amount = CDbl(Fill_D_O(nq, Hermes_DB.requestAmount_amount)) ''checar esta conversion

                Hermes.loanBase.iLoanDetail.loanCar.preapprovedBrand = Fill_D_O(nq, Hermes_DB.preapprovedBrand)
                Hermes.loanBase.iLoanDetail.loanCar.use = Fill_D_O(nq, Hermes_DB.use)
                Hermes.loanBase.iLoanDetail.loanCar.initialAmount.amount = CDbl(Fill_D_O(nq, Hermes_DB.initialAmount_amount)) ''checar esta conversion
                Hermes.loanBase.iLoanDetail.loanCar.percentageInitialAmount = CDbl(Fill_D_O(nq, Hermes_DB.percentageInitialAmount))  ''checar esta conversion

                Hermes.loanBase.loan.loanInstallments.installmentAmount.amount = CDbl(Fill_D_O(nq, Hermes_DB.installmentAmount_amount))  ''checar conversion
                Hermes.loanBase.loan.loanProduct.interestRate = CDbl(Fill_D_O(nq, Hermes_DB.loanProduct_interestRate))  ''checar conversion

                Hermes.loanBase.extendedData.preapprovedAmount.amount = CDbl(Fill_D_O(nq, Hermes_DB.preapprovedAmount_amount)) ''checar conversion
                Hermes.loanBase.extendedData.expiredDebtPayed = CDbl(Fill_D_O(nq, Hermes_DB.expiredDebtPayed)) ''checar conversion

                Hermes.numberTimesReferenceFoundClient = CDbl(Fill_D_O(nq, Hermes_DB.numberTimesReferenceFoundClient)) ''checar conversion
                Hermes.moratoriumRates.Add(CDbl(Fill_D_O(nq, Hermes_DB.moratoriumRates_1))) ''checar conversion
                Hermes.moratoriumRates.Add(CDbl(Fill_D_O(nq, Hermes_DB.moratoriumRates_2))) ''checar conversion
                Hermes.moratoriumRates.Add(CDbl(Fill_D_O(nq, Hermes_DB.moratoriumRates_3))) ''checar conversion

                Hermes.originType = Fill_D_O(nq, Hermes_DB.originType)
                Hermes.referenceNumberUG = Fill_D_O(nq, Hermes_DB.referenceNumberUG)
                Hermes.cbScore.loanToValue.amount = CDbl(Fill_D_O(nq, Hermes_DB.loanToValue))  ''checar conversion
                Hermes.agency.id = CDbl(Fill_D_O(nq, Hermes_DB.agency_id)) ''checar conversion
                Hermes.agency.address.zipCode = Fill_D_O(nq, Hermes_DB.agency_zipCode)
                Hermes.agency.divisionId = CDbl(Fill_D_O(nq, Hermes_DB.divisionId))   ''checar conversion
                Hermes.agency.status = CDbl(Fill_D_O(nq, Hermes_DB.agency_status))   ''checar conversion


                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                HermesJSON = serializer.Serialize(Hermes)

            End If
        Catch ex As Exception
            Dim msj As String = ex.Message
        End Try

        '**
        Return HermesJSON
    End Function

    Private Function getInfoHermesDBOptimo(ByVal folio As String) As HermesDB_Optimo
        Dim Hermes As HermesDB_Optimo = New HermesDB_Optimo()

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_Hermes_Info_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()

                If (Not IsNothing(reader("newquery"))) Then
                    Hermes.newquery = reader("newquery")
                Else
                    Hermes.newquery = 0
                End If



                Hermes.branch.value = reader("branch")
                Hermes.id_HDB.value = reader("id")
                Hermes.facebook.value = reader("facebook")
                Hermes.person_id.value = reader("person_id")

                Hermes.rfc.value = reader("extendedData_rfc")
                Hermes.age.value = reader("extendedData_age")

                If reader("extendedData_homonym") = "" Or IsNothing(reader("extendedData_homonym")) Then
                    Hermes.homo.value = " "
                Else
                    Hermes.homo.value = reader("extendedData_homonym")
                End If


                Hermes.sex.value = reader("extendedData_sex")
                Hermes.maritalstatus.value = reader("extendedData_maritalStatus")

                Hermes.Name.value = reader("name")
                Hermes.lastname.value = reader("lastname")
                Hermes.mothersLastName.value = reader("motherslastname")
                Hermes.birthdate.value = reader("birthdate")

                Hermes.automaticPayment.value = reader("automaticPayment")
                Hermes.PaymentIndicator.value = reader("PaymentIndicator")
                Hermes.othersIncomes_1.value = reader("othersIncomes_1")
                Hermes.othersIncomes_2.value = reader("othersIncomes_2")
                Hermes.othersIncomes_3.value = reader("othersIncomes_3")
                Hermes.variableIncomes.value = reader("variableIncomes")
                Hermes.fixedIncomes.value = reader("fixedIncomes")
                ''TELEFONO MOVIL CTE
                Hermes.telephoneCompany_id.value = reader("telephoneCompany_id_2")
                Hermes.contactinfo_prefix.value = reader("CUSTOMER_PREFIX_NUMBER_M")
                Hermes.contactinfo_ext.value = reader("CUSTOMER_PHONE_EXTENSION_M")
                Hermes.telephoneNumber.value = reader("CUSTOMER_PHONE_NUMBER_M")
                ''TELEFONO FIJO CTE
                Hermes.telephoneCompany1_id.value = reader("telephoneCompany_id")
                Hermes.contactinfo_prefix1.value = reader("CUSTOMER_PREFIX_NUMBER_P")
                Hermes.contactinfo_ext1.value = reader("CUSTOMER_PHONE_EXTENSION_P")
                Hermes.telephoneNumber1.value = reader("CUSTOMER_PHONE_NUMBER_P")
                ''TELEFONO REFRENCIA 1
                Hermes.telephoneCompany2_id.value = reader("telephoneCompany_id_2")
                Hermes.contactinfo_prefix2.value = reader("contactinfo_1_telephone_PREFIX")
                Hermes.contactinfo_ext2.value = reader("contactinfo_1_telephone_EXTENSION")
                Hermes.telephoneNumber2.value = reader("contactinfo_1_telephone_NUMBER")
                ''TELEFONO REFRENCIA 2
                Hermes.telephoneCompany2_id.value = reader("telephoneCompany_id_2")
                Hermes.contactinfo_prefix2.value = reader("contactinfo_2_telephone_PREFIX")
                Hermes.contactinfo_ext2.value = reader("contactinfo_2_telephone_EXTENSION")
                Hermes.telephoneNumber2.value = reader("contactinfo_2_telephone_NUMBER")

                Hermes.subProductCode.value = reader("subProductCode")

                Hermes.model.value = reader("model")
                Hermes.brand.value = reader("brand")
                Hermes.subBrand.value = reader("subBrand")
                Hermes.isUsed.value = reader("isUsed")
                Hermes.aliance.value = reader("aliance")

                Hermes.termNumber.value = reader("termNumber")
                Hermes.invoice_amount.value = reader("invoice_amount")
                Hermes.requestAmount_amount.value = reader("requestAmount_amount")

                If reader("preapprovedBrand") = "" Or IsNothing(reader("preapprovedBrand")) Then
                    Hermes.preapprovedBrand.value = " "
                Else
                    Hermes.preapprovedBrand.value = reader("preapprovedBrand")
                End If

                Hermes.use.value = reader("use")
                Hermes.initialAmount_amount.value = reader("initialAmount_amount")
                Hermes.percentageInitialAmount.value = reader("percentageInitialAmount")

                Hermes.installmentAmount_amount.value = reader("installmentAmount_amount")
                Hermes.loanProduct_interestRate.value = reader("loanProduct_interestRate")

                Hermes.preapprovedAmount_amount.value = reader("preapprovedAmount_amount")

                Hermes.expiredDebtPayed.value = reader("expiredDebtPayed")

                Hermes.numberTimesReferenceFoundClient.value = reader("numberTimesReferenceFoundClient")
                Hermes.moratoriumRates_1.value = CDbl(Val(reader("moratoriumRates_1")))
                Hermes.moratoriumRates_2.value = CDbl(Val(reader("moratoriumRates_2")))
                Hermes.moratoriumRates_3.value = CDbl(Val(reader("moratoriumRates_3")))
                Hermes.originType.value = reader("originType")
                Hermes.referenceNumberUG.value = reader("referenceNumberUG")

                Hermes.loanToValue.value = reader("loanToValue")

                Hermes.agency_id.value = reader("agency_id")
                Hermes.agency_zipCode.value = reader("agency_zipCode") 'Codigo postal de la agencia

                Hermes.divisionId.value = reader("divisionId")
                Hermes.agency_status.value = reader("agency_status")
                Hermes.addresses_zp.value = reader("addresses_zp") 'cpclient

                'Nuevos campos
                Hermes.streetName.value = reader("streetName")
                Hermes.neighborhood.value = reader("neighborhood")
                Hermes.outdoorNumber.value = reader("outdoorNumber")

                If reader("insideNumber") = "" Or IsNothing(reader("insideNumber")) Then
                    Hermes.insideNumber.value = " "
                Else
                    Hermes.insideNumber.value = reader("insideNumber")
                End If

                Hermes.address_countryid.value = reader("country_id")
                Hermes.addresses_zp.value = reader("zipcode_directi")
                Hermes.startingResidence_Date.value = reader("residencedate")

                Hermes.address_city.value = reader("city")

                Hermes.directi_zp.value = reader("zipcode_directi") 'DIRECTI
                Hermes.state.value = reader("estado")
                Hermes.nationality.value = reader("nationality")
                'Dim HCI_tel_value As List(Of contactinfo_telephone) = New List(Of contactinfo_telephone)(2)
                'HCI_tel_value(0).value = reader("CUSTOMER_PHONE_NUMBER_P")
                'HCI_tel_value(1).value = reader("CUSTOMER_PHONE_NUMBER_M")
                Hermes.schoolingLevel.value = reader("schoolingLevel")
                Hermes.economicDependants.value = reader("economicDependants")
                Hermes.extendedData_occupation.value = reader("extendedData_occupation")
                Hermes.extendedData_jobLocation.value = reader("extendedData_jobLocation")
                Hermes.extendedData_activityJob.value = reader("extendedData_activityJob")
                Hermes.extendedData_employmenteSituation.value = reader("extendedData_employmentSituation")
                Hermes.extendedData_jobSeniority.value = reader("extendedData_jobSeniority")
                Hermes.HireData.value = reader("extendedData_hireDate")
                Hermes.extendedData_businessActivity.value = reader("extendedData_businessActivity")
                Hermes.person_type.value = reader("person_type")
                Hermes.extendedData_rentalExpenditure.value = reader("extendedData_rentalExpenditure")

                Hermes.commercialValue.value = reader("commercialValue")
                Hermes.realStateAge.value = reader("realStateAge")

                Hermes.subgroup.value = reader("subgroup")

                Hermes.mortgageExpenditure.value = reader("mortgageExpenditure")
                Hermes.conservationStatus.value = reader("extendedData_property_conservation")
                Hermes.extendedData_employmenteSituation.value = reader("extendedData_employmentSituation")
                Hermes.economicData_profession.value = reader("economicData_profession")
                Hermes.identity_type.value = reader("identityDocument_identityType")
                Hermes.indentity_number.value = reader("identityDocument_number")
                Hermes.isCustomer.value = reader("is_customer")
                Hermes.relationshipGFP.value = reader("relationshipGFP")
                Hermes.membershipdate.value = reader("membershipDate")
                Hermes.seniorityDate.value = reader("seniorityDate")
                Hermes.membershipSeniority.value = reader("membershipSeniority")
                'Hermes.co .birthdate.value = reader("coac_birthDate")
                Hermes.mensaje_ = "Ok"
                'Nuevos campos
            Loop
        Catch ex As Exception
            Hermes.mensaje_ = "Error al traer los datos de la BD"
            'alta = False
        End Try

        sqlConnection1.Close()
        Return Hermes
    End Function

    Public Class Hermes_Resp
        Public valido As Boolean
        Public dictamenFinal As String
        Public motivoRechazo As String
        Public score As String
    End Class

    Public Class Hermes_Resp_WS
        Public scoringResults As scoringResults = New scoringResults
    End Class

    Public Class scoringResults
        Public balance As genericAmount = New genericAmount()
        Public paymentCapacity As String
        Public score As String
        Public reference As String
        Public antifraud As String
        Public antifraudDictum As String
        Public maximumLimit As Double
        Public finalDictum As String
        Public rejectionPolicy As Integer
        Public payloadEnvio As String
        Public payloadRecibido As String
    End Class

    Public Class scoringResultsOptimo
        Inherits scoringResults
        Public internalBuro As String
    End Class
    Private Function SaveInfoHermesResponseOptimo(ByVal infoHermes As Hermes_Resp_WS, ByVal folio As String) As String
        Dim Hermes As HermesDB_Optimo = New HermesDB_Optimo()
        Dim save As String = "NO"

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "set_Hermes_Response_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Parameters.AddWithValue("@balance", infoHermes.scoringResults.balance.amount.ToString())
            cmd.Parameters.AddWithValue("@paymentCapacity", infoHermes.scoringResults.paymentCapacity.ToString())
            cmd.Parameters.AddWithValue("@score", infoHermes.scoringResults.score.ToString())
            cmd.Parameters.AddWithValue("@reference", infoHermes.scoringResults.reference.ToString())
            cmd.Parameters.AddWithValue("@antifraud", infoHermes.scoringResults.antifraud.ToString())
            cmd.Parameters.AddWithValue("@antifraudDictum", infoHermes.scoringResults.antifraudDictum.ToString())
            cmd.Parameters.AddWithValue("@maximumLimit", infoHermes.scoringResults.maximumLimit.ToString())
            cmd.Parameters.AddWithValue("@finalDictum", infoHermes.scoringResults.finalDictum.ToString())
            cmd.Parameters.AddWithValue("@rejectionPolicy", infoHermes.scoringResults.rejectionPolicy.ToString())

            cmd.Parameters.AddWithValue("@payloadEnvio", infoHermes.scoringResults.payloadEnvio.ToString())
            cmd.Parameters.AddWithValue("@payloadRecepcion", infoHermes.scoringResults.payloadRecibido.ToString())

            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
            Loop
            save = "SI"
        Catch ex As Exception
            save = "NO"
        End Try

        sqlConnection1.Close()
        Return save
    End Function


    Private Function getMessage(ByVal mensaje As String, ByVal WSvalido As Boolean) As mensajeCod
        Dim msg As mensajeCod = New mensajeCod()

        If (WSvalido) Then
            Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
            Dim cmd As New SqlCommand
            Dim reader As SqlDataReader

            Try
                cmd.CommandText = "get_Message_Output_SP"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@MESSAGE_HERMES", mensaje)
                cmd.Connection = sqlConnection1
                sqlConnection1.Open()
                reader = cmd.ExecuteReader()

                Do While reader.Read()
                    msg.mensaje = reader("MSG").ToString()
                    msg.code = Int32.Parse(reader("STATUS"))
                    msg.path = Int32.Parse(reader("PATH"))
                Loop
            Catch ex As Exception
                msg.mensaje = ex.ToString().Replace(vbCr, "").Replace(vbLf, "").Replace("'", "")
                msg.code = 0
                msg.path = 0
            End Try
        Else
            msg.mensaje = mensaje
            msg.code = 0
            msg.path = 0
        End If

        Return msg
    End Function

    Public Class mensajeCod
        Public mensaje As String
        Public code As Integer
        Public path As Integer
    End Class

    Private Function CancelaTarea(ByVal status_credito As Integer, ByVal message As String) As String
        Dim path As String = String.Empty

        Dim objCancela As New clsSolicitudes(0)
        objCancela.PDK_ID_SOLICITUD = Request("Sol").ToString()
        objCancela.Estatus = 42
        objCancela.PDK_CLAVE_USUARIO = Session("IdUsua")
        objCancela.PDK_ID_PANTALLA = Request("idPantalla").ToString()
        objCancela.Estatus_Cred = status_credito
        objCancela.ManejaTarea(6)

        Dim dc As New clsDatosCliente
        dc.idSolicitud = Val(Request("sol"))
        dc.getDatosSol()
        ClsEmail.OPCION = 17
        ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

        Dim str As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
        path = "PopUpLetreroRedirectSpecial('" + message + "', '../aspx/consultaPanelControl.aspx" + str + "');"

        Dim dtConsulta = New DataSet()
        dtConsulta = ClsEmail.ConsultaStatusNotificacion
        If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
            If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
                    If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
                        Dim strLocation As String = "../aspx/ValidaEmails.aspx?idPantalla=" + Request("idPantalla").ToString() + "&Sol=" + Request("Sol").ToString() + "&mostrarPant=2&usuario=" + Request("usuario").ToString()
                        path = "PopUpLetreroRedirectSpecial('" + message + "', '" + strLocation + "');"
                    End If
                End If
            End If
        End If

        Return path
    End Function

    Private Function asignaTarea(ByVal msg As String) As String
        Dim path As String = String.Empty
        Dim idAsignarPantalla As Integer

        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Try
            Dim ds_siguienteTarea As DataSet
            Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
            objCatalogos.Parametro = Request("idPantalla")
            ds_siguienteTarea = objCatalogos.Catalogos(6)

            If ds_siguienteTarea.Tables.Count > 0 Then
                If ds_siguienteTarea.Tables(0).Rows.Count > 0 Then
                    idAsignarPantalla = Int32.Parse(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))
                End If
            End If

            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                path = "PopUpLetreroRedirectSpecial('" + mensaje + "', '../aspx/consultaPanelControl.aspx');"
            Else
                dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

                muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

                If muestrapant = 0 Then
                    Dim strLocation As String = "../aspx/" + dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() + "?idPantalla=" + dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() + "&sol=" + Val(Request("Sol")).ToString() + "&usuario=" + Val(Request("usuario")).ToString()
                    path = "window.location = '" + strLocation + "';"
                ElseIf muestrapant = 2 Then
                    path = "PopUpLetreroRedirectSpecial('" + msg + "', '../aspx/consultaPanelControl.aspx');"
                End If
            End If
        Catch ex As Exception
            path = "PopUpLetreroRedirectSpecial('Error: " + ex.ToString() + "', '../aspx/consultaPanelControl.aspx');"
        End Try

        Return path
    End Function

    Private Function NextStep(ByVal dbScore As Double, ByVal message As String) As String
        Dim path As String = String.Empty

        Dim dslink As DataSet = New DataSet()
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim muestrapant As Integer = Nothing
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()

        Dim r As Integer
        Dim objupd As New ProdeskNet.SN.clsActualizaBuro()
        r = objupd.actScore(Val(Request("Sol")), dbScore)

        If r >= 2 And objupd.StrError = "" Then
            Dim dsresult As DataSet = New DataSet()
            dsresult = DB.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)
            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))
            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

            Dim mensaje As String = "Tarea Exitosa"
            Dim strLocation As String = String.Empty

            If muestrapant = 0 Then
                'Dim strLocation As String = ("../aspx/" + dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() + "?idPantalla=" + dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() + "&sol=" + Val(Request("Sol")).ToString() + "&usuario=" + Val(Request("usuario")).ToString())
                strLocation = "../aspx/ValidaEmails.aspx?idPantalla=" + Request("idPantalla").ToString() + "&Sol=" + Request("Sol").ToString() + "&mostrarPant=" + muestrapant.ToString() + "&usuario=" + Request("usuario").ToString()
                path = "PopUpLetreroRedirectSpecial('" + message + "', '" + strLocation + "');"
            ElseIf muestrapant = 2 Then
                strLocation = "../aspx/ValidaEmails.aspx?idPantalla=" + Request("idPantalla").ToString() + "&Sol=" + Request("Sol").ToString() + "&mostrarPant=" + muestrapant.ToString() + "&usuario=" + Request("usuario").ToString()
                path = "PopUpLetreroRedirectSpecial('" + message + "', '" + strLocation + "');"
                'path = "PopUpLetreroRedirectSpecial('" + message + "', '../aspx/consultaPanelControl.aspx');"
            End If

        Else
            path = asignaTarea("No se pudo actualizar la información.")
        End If

        Return path
    End Function
End Class
