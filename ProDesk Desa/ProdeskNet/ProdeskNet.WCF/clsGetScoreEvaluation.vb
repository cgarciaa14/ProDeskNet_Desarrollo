#Region "TRACKERS"

'AUTOMIK-BUG-435: ERODRIGUEZ: 10/04/18 LLAMADO A SERVICIO getScoreEvaluation

#End Region
Imports System.Text
Imports ProdeskNet.BD
Imports System.Net
Imports System.Data.SqlClient



Public Class clsGetScoreEvaluation

    Private _URL As String = String.Empty
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()

    Public ReadOnly Property urlScore As String
        Get
            Dim urlService = System.Configuration.ConfigurationManager.AppSettings.Item("urlSCORE")

            Dim oldString = urlService.Substring(urlService.IndexOf("@@"), urlService.IndexOf("*") - urlService.IndexOf("@@"))
            Dim newString = System.Configuration.ConfigurationManager.AppSettings.Item(oldString.Replace("@@", ""))
            urlService = urlService.Replace(oldString & "*", newString)

            oldString = urlService.Substring(urlService.IndexOf("@@"), urlService.IndexOf("*") - urlService.IndexOf("@@"))
            newString = System.Configuration.ConfigurationManager.AppSettings.Item(oldString.Replace("@@", ""))
            urlService = urlService.Replace(oldString & "*", newString)

            Return urlService
        End Get
    End Property

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

    Public Class address_
        Public zipCode As String
    End Class

    Public Class genericID
        Public id As String
    End Class

    Public Class genericAmount
        Public amount As Double
    End Class

    Public Class zipCode
        Public zipCode As String
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

    Public Class car_
        Public model As String
        Public brand As String
        Public subBrand As String
        Public isUsed As String
        Public aliance As String
    End Class

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

    Public Class mensajeCod
        Public mensaje As String
        Public code As Integer
        Public path As Integer
    End Class

    Public Function fill_Info_Hermes(ByVal folio As String, Optional ByVal automikRequest As Boolean = 0, Optional headers As WebHeaderCollection = Nothing) As Hermes_Resp
        Dim Hermes_Respuesta As Hermes_Resp = New Hermes_Resp()
        Hermes_Respuesta.valido = False
        Hermes_Respuesta.dictamenFinal = String.Empty
        Hermes_Respuesta.motivoRechazo = String.Empty
        Hermes_Respuesta.score = String.Empty

        Try

            Dim Hermes_DB_Optimo_JSON As String = GetHermesInfoScore(folio, automikRequest)

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            'Dim HermesJSON As String = serializer.Serialize(Hermes)


            Dim restGT As RESTful = New RESTful()
            restGT.automikRequest = automikRequest
            restGT.Uri = _URL

            Dim userID As String
            If Not automikRequest Then
                userID = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Else
                userID = headers("userID")
                restGT.consumerID = headers("consumerID")
            End If

            Dim iv_ticket1 As String
            If Not automikRequest Then
                iv_ticket1 = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Else
                iv_ticket1 = headers("iv_ticket")
            End If
            If Not automikRequest Then
                restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("urlSCORE")
            Else
                restGT.Uri = urlScore
            End If


            'userID = System.Configuration.ConfigurationManager.AppSettings("userID").ToString()
            'userID = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            ''iv_ticket1 = System.Configuration.ConfigurationManager.AppSettings("iv_ticket").ToString()
            'iv_ticket1 = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            'Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
            ''rest.Uri = System.Configuration.ConfigurationManager.AppSettings("getScoreEvaluation").ToString()
            'rest.Uri = "https://150.250.220.36:18500/CbScores/V04/getScoreEvaluation"
            'rest.buscarHeader("error-code")
            restGT.buscarHeader("error-code")

            Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, Hermes_DB_Optimo_JSON)
            'Dim jsonResult As String = rest.ConnectionPost(userID, iv_ticket1, Hermes_DB_Optimo_JSON)
            If (restGT.IsError = False) Then
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

                If restGT.valorHeader <> "" Then
                    Hermes_Respuesta.dictamenFinal = restGT.valorHeader + " " + restGT.MensajeError

                    'Dim Hermes_WS As Hermes_Resp_WS = New Hermes_Resp_WS()
                    Dim Hermes_WS As Hermes_Resp_WS = New Hermes_Resp_WS()
                    Hermes_WS.scoringResults.balance.amount = 0.0
                    Hermes_WS.scoringResults.finalDictum = restGT.valorHeader + " " + restGT.MensajeError
                    Hermes_WS.scoringResults.antifraudDictum = restGT.valorHeader + " " + restGT.MensajeError
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
                    Hermes_Respuesta.dictamenFinal = restGT.MensajeError
                End If

            End If

        Catch ex As Exception
            Hermes_Respuesta.valido = False
        End Try
        Return Hermes_Respuesta
    End Function

    Private Function getHermesInfo(ByVal folio As String, Optional ByVal AUTOMIK As Boolean = 0) As DataSet
        Dim info As DataSet = New DataSet()
        Try

            Dim BD = New clsManejaBD()
            If AUTOMIK Then
                Dim querty As String = "exec get_Hermes_Info_SP @PDK_ID_SECCCERO = '" & folio & "', " & "@AUTOMIK = '" & Convert.ToInt16(AUTOMIK) & "'"
                info = BD.EjecutarQuery("exec get_Hermes_Info_SP @PDK_ID_SECCCERO = '" & folio & "', " & "@AUTOMIK = '" & Convert.ToInt16(AUTOMIK) & "'")
            Else
                info = BD.EjecutarQuery("exec get_Hermes_Info_SP '" & folio & "'")
            End If




            'Dim BD = New clsManejaBD()

            'BD.AbreConexion()

            'BD.AgregaParametro("@PDK_ID_SECCCERO", TipoDato.Cadena, folio)

            'Dim strParamStored As String = String.Empty


            'info = BD.EjecutaStoredProcedure("get_Hermes_Info_SP")
            ''info = BD.EjecutaStoredProcedureCP("get_Hermes_Info_SP", strParamStored)

            'Dim objconex As New ProdeskNet.BD.clsManejaBD()
            'info = objconex.EjecutaStoredProcedure("EXEC get_Hermes_Info_SP" & folio & "")
        Catch ex As Exception

        End Try
        Return info
    End Function

    Private Function GetHermesInfoScore(ByVal folio As String, Optional ByVal AUTOMIK As Boolean = 0) As String
        Dim json As New Hermes_Optimo()
        Dim dts As New DataSet()
        dts = getHermesInfo(folio, AUTOMIK)

        If dts.Tables.Count > 0 Then
            If dts.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To dts.Tables(0).Rows.Count - 1
                    json = New Hermes_Optimo()
                    json.branch.id = dts.Tables(0).Rows(i).Item("branch")
                    json.id = dts.Tables(0).Rows(i).Item("id")

                    json.customer.relationshipGFP = dts.Tables(0).Rows(i).Item("relationshipGFP")
                    json.customer.membershipDate = dts.Tables(0).Rows(i).Item("membershipDate")
                    json.customer.seniorityDate = dts.Tables(0).Rows(i).Item("seniorityDate")
                    json.customer.membershipSeniority = dts.Tables(0).Rows(i).Item("membershipSeniority")
                    json.customer.automaticPaymentIndicator = dts.Tables(0).Rows(i).Item("PaymentIndicator")

                    json.customer.person.extendedData.facebook = dts.Tables(0).Rows(i).Item("facebook")
                    json.customer.person.id = dts.Tables(0).Rows(i).Item("person_id")

                    json.customer.person.lastName = dts.Tables(0).Rows(i).Item("lastname")
                    json.customer.person.mothersLastName = dts.Tables(0).Rows(i).Item("motherslastname")
                    json.customer.person.name = dts.Tables(0).Rows(i).Item("name")
                    json.customer.person.birthDate = dts.Tables(0).Rows(i).Item("birthdate")

                    Dim a1 As address_Optimo = New address_Optimo()
                    a1.streetName = dts.Tables(0).Rows(i).Item("streetName")
                    a1.streetNumber = ""  ''checar si va
                    a1.neighborhood = dts.Tables(0).Rows(i).Item("neighborhood")
                    a1.city = dts.Tables(0).Rows(i).Item("city")
                    a1.state = dts.Tables(0).Rows(i).Item("estado")
                    a1.zipCode = dts.Tables(0).Rows(i).Item("addresses_zp")
                    a1.outdoorNumber = dts.Tables(0).Rows(i).Item("outdoorNumber")

                    If dts.Tables(0).Rows(i).Item("insideNumber") = "" Or IsNothing(dts.Tables(0).Rows(i).Item("insideNumber")) Then
                        a1.insideNumber = " "
                    Else
                        a1.insideNumber = dts.Tables(0).Rows(i).Item("insideNumber")
                    End If

                    a1.startingResidenceDate = dts.Tables(0).Rows(i).Item("residencedate")
                    a1.country.id = dts.Tables(0).Rows(i).Item("country_id")
                    json.customer.person.addresses.Add(a1)

                    json.customer.person.extendedData.rfc = dts.Tables(0).Rows(i).Item("extendedData_rfc")

                    json.customer.person.extendedData.sex = dts.Tables(0).Rows(i).Item("extendedData_sex")
                    json.customer.person.extendedData.homonym = dts.Tables(0).Rows(i).Item("extendedData_homonym")
                    json.customer.person.extendedData.age = dts.Tables(0).Rows(i).Item("extendedData_age")
                    json.customer.person.extendedData.maritalStatus = dts.Tables(0).Rows(i).Item("extendedData_maritalStatus")

                    json.customer.person.economicData.profession = dts.Tables(0).Rows(i).Item("economicData_profession")
                    json.customer.person.economicData.extendedData.schoolingLevel = dts.Tables(0).Rows(i).Item("schoolingLevel")
                    json.customer.person.economicData.extendedData.economicDependants = dts.Tables(0).Rows(i).Item("economicDependants")
                    json.customer.person.economicData.extendedData.occupation = dts.Tables(0).Rows(i).Item("extendedData_occupation")
                    json.customer.person.economicData.extendedData.jobLocation = dts.Tables(0).Rows(i).Item("extendedData_jobLocation")
                    json.customer.person.economicData.extendedData.activityJob = dts.Tables(0).Rows(i).Item("extendedData_activityJob")
                    json.customer.person.economicData.extendedData.employmentSituation = dts.Tables(0).Rows(i).Item("extendedData_employmentSituation")
                    json.customer.person.economicData.extendedData.jobSeniority = dts.Tables(0).Rows(i).Item("extendedData_jobSeniority")
                    json.customer.person.economicData.extendedData.businessActivity = dts.Tables(0).Rows(i).Item("extendedData_businessActivity")

                    json.customer.person.economicData.extendedData.hireDate = dts.Tables(0).Rows(i).Item("extendedData_hireDate")
                    json.customer.person.economicData.extendedData.rentalExpenditure = dts.Tables(0).Rows(i).Item("extendedData_rentalExpenditure")
                    json.customer.person.economicData.extendedData.mortgageExpenditure = dts.Tables(0).Rows(i).Item("mortgageExpenditure")
                    json.customer.person.economicData.extendedData.subgroup = dts.Tables(0).Rows(i).Item("subgroup")

                    json.customer.person.economicData.extendedData.property.commercialValue = dts.Tables(0).Rows(i).Item("commercialValue")
                    json.customer.person.economicData.extendedData.property.realStateAge = dts.Tables(0).Rows(i).Item("realStateAge")
                    json.customer.person.economicData.extendedData.property.conservationStatus = dts.Tables(0).Rows(i).Item("extendedData_property_conservation")

                    json.customer.person.economicData.extendedData.automaticPayment.active = dts.Tables(0).Rows(i).Item("automaticPayment")

                    Dim o As genericAmount = New genericAmount()
                    Dim o_1 As genericAmount = New genericAmount()
                    Dim o_2 As genericAmount = New genericAmount()
                    o.amount = CDbl(dts.Tables(0).Rows(i).Item("othersIncomes_1"))  ''checar conversion
                    o_1.amount = CDbl(dts.Tables(0).Rows(i).Item("othersIncomes_2")) ''checar conversion
                    o_2.amount = CDbl(dts.Tables(0).Rows(i).Item("othersIncomes_3"))  ''checar conversion
                    json.customer.person.economicData.extendedData.othersIncomes.Add(o)  ''warn
                    json.customer.person.economicData.extendedData.othersIncomes.Add(o_1) ''warn
                    json.customer.person.economicData.extendedData.othersIncomes.Add(o_2) ''warn

                    json.customer.person.economicData.extendedData.variableIncomes.amount = CDbl(dts.Tables(0).Rows(i).Item("variableIncomes")) ''checar conversion
                    json.customer.person.economicData.extendedData.fixedIncomes.amount = CDbl(dts.Tables(0).Rows(i).Item("fixedIncomes")) ''checar conversion

                    ''telefono cliente movil obligatorio
                    Dim telephone As telephone_Optimo = New telephone_Optimo()
                    telephone.telephone.telephoneCompany.id = dts.Tables(0).Rows(i).Item("telephoneCompany_id_2")
                    telephone.telephone.number = dts.Tables(0).Rows(i).Item("CUSTOMER_PHONE_NUMBER_M")
                    telephone.telephone.prefix = dts.Tables(0).Rows(i).Item("CUSTOMER_PREFIX_NUMBER_M")
                    telephone.telephone.phoneExtension = dts.Tables(0).Rows(i).Item("CUSTOMER_PHONE_EXTENSION_M")
                    json.customer.person.contactsInformation.Add(telephone) ''warn
                    ''telefono cliente fijo 
                    Dim telephone_1 As telephone_Optimo = New telephone_Optimo()
                    telephone_1.telephone.telephoneCompany.id = dts.Tables(0).Rows(i).Item("telephoneCompany_id")
                    telephone_1.telephone.number = dts.Tables(0).Rows(i).Item("CUSTOMER_PHONE_NUMBER_P")
                    telephone_1.telephone.prefix = dts.Tables(0).Rows(i).Item("CUSTOMER_PREFIX_NUMBER_P")
                    telephone_1.telephone.phoneExtension = dts.Tables(0).Rows(i).Item("CUSTOMER_PHONE_EXTENSION_P")
                    json.customer.person.contactsInformation.Add(telephone_1) ''warn

                    json.customer.person.identityDocument.id = dts.Tables(0).Rows(i).Item("identityDocument_number")
                    json.customer.person.identityDocument.identityType = dts.Tables(0).Rows(i).Item("identityDocument_identityType")

                    json.customer.person.nationality = dts.Tables(0).Rows(i).Item("nationality")
                    json.customer.person.type = dts.Tables(0).Rows(i).Item("person_type")
                    json.customer.person.isCustomer = dts.Tables(0).Rows(i).Item("is_customer")


                    'HARDCODE NECESSARY

                    json.coaccreditedCustomer.relationshipGFP = ""
                    json.coaccreditedCustomer.membershipDate = "          "
                    json.coaccreditedCustomer.seniorityDate = "          "
                    json.coaccreditedCustomer.membershipSeniority = "000"
                    json.coaccreditedCustomer.automaticPaymentIndicator = False

                    json.coaccreditedCustomer.person.lastName = "                    "
                    json.coaccreditedCustomer.person.mothersLastName = "                    "
                    json.coaccreditedCustomer.person.name = "                    "
                    json.coaccreditedCustomer.person.birthDate = "0001-01-01"

                    json.coaccreditedCustomer.person.extendedData.rfc = "          "
                    json.coaccreditedCustomer.person.extendedData.sex = " "

                    json.coaccreditedCustomer.person.extendedData.populationRegisterId = ""   'checar si va a ir

                    json.coaccreditedCustomer.person.extendedData.homonym = "    "
                    json.coaccreditedCustomer.person.extendedData.age = "000"
                    json.coaccreditedCustomer.person.extendedData.maritalStatus = " "

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
                    json.coaccreditedCustomer.person.addresses.Add(ca1)

                    json.coaccreditedCustomer.person.economicData.extendedData.automaticPayment.active = 0  ''checar conversion


                    json.coaccreditedCustomer.person.economicData.extendedData.schoolingLevel = "000"
                    json.coaccreditedCustomer.person.economicData.extendedData.economicDependants = "00"
                    json.coaccreditedCustomer.person.economicData.extendedData.occupation = "                              "
                    json.coaccreditedCustomer.person.economicData.extendedData.jobLocation = "                                                  "
                    json.coaccreditedCustomer.person.economicData.extendedData.activityJob = "   "
                    json.coaccreditedCustomer.person.economicData.extendedData.employmentSituation = "0"
                    json.coaccreditedCustomer.person.economicData.extendedData.jobSeniority = "00"
                    json.coaccreditedCustomer.person.economicData.extendedData.businessActivity = "  "

                    json.coaccreditedCustomer.person.economicData.extendedData.hireDate = "          "
                    json.coaccreditedCustomer.person.economicData.extendedData.rentalExpenditure = "000000000000000"
                    json.coaccreditedCustomer.person.economicData.extendedData.mortgageExpenditure = "000000000000000"
                    json.coaccreditedCustomer.person.economicData.extendedData.subgroup = " "

                    json.coaccreditedCustomer.person.economicData.extendedData.property.commercialValue = "000000000000000"
                    json.coaccreditedCustomer.person.economicData.extendedData.property.realStateAge = "00"
                    json.coaccreditedCustomer.person.economicData.extendedData.property.conservationStatus = "000"

                    json.coaccreditedCustomer.person.economicData.profession = "00"

                    Dim telephonec As telephone_Optimo_coac = New telephone_Optimo_coac()
                    telephonec.telephone.number = "0000000"
                    telephonec.telephone.prefix = "000"
                    telephonec.telephone.phoneExtension = "0000"
                    json.coaccreditedCustomer.person.contactsInformation.Add(telephonec) ''warn

                    telephonec = New telephone_Optimo_coac()
                    telephonec.telephone.number = "0000000"
                    telephonec.telephone.prefix = "000"
                    telephonec.telephone.phoneExtension = "0000"
                    json.coaccreditedCustomer.person.contactsInformation.Add(telephonec) ''warn

                    json.coaccreditedCustomer.person.identityDocument.id = "          "
                    json.coaccreditedCustomer.person.identityDocument.identityType = "0"

                    json.coaccreditedCustomer.person.nationality = "    "
                    json.coaccreditedCustomer.person.type = " "
                    json.coaccreditedCustomer.person.isCustomer = " "

                    json.numberTimesReferenceFoundCoaccredited = ""
                    'HARDCODE NECESSARY FINALLY

                    json.loanBase.subProductCode = dts.Tables(0).Rows(i).Item("subProductCode")
                    json.loanBase.iLoanDetail.loanCar.car.model = dts.Tables(0).Rows(i).Item("model")
                    json.loanBase.iLoanDetail.loanCar.car.brand = dts.Tables(0).Rows(i).Item("brand")
                    json.loanBase.iLoanDetail.loanCar.car.subBrand = dts.Tables(0).Rows(i).Item("subBrand")
                    json.loanBase.iLoanDetail.loanCar.car.isUsed = dts.Tables(0).Rows(i).Item("isUsed")
                    json.loanBase.iLoanDetail.loanCar.car.aliance = dts.Tables(0).Rows(i).Item("aliance")

                    json.loanBase.iLoanDetail.loanCar.termNumber = dts.Tables(0).Rows(i).Item("termNumber")
                    json.loanBase.iLoanDetail.loanCar.invoice.amount.amount = CDbl(dts.Tables(0).Rows(i).Item("invoice_amount")) ''checar esta conversion
                    json.loanBase.iLoanDetail.loanCar.requestAmount.amount = CDbl(dts.Tables(0).Rows(i).Item("requestAmount_amount")) ''checar esta conversion


                    If dts.Tables(0).Rows(i).Item("preapprovedBrand") = "" Or IsNothing(dts.Tables(0).Rows(i).Item("preapprovedBrand")) Then
                        json.loanBase.iLoanDetail.loanCar.preapprovedBrand = " "
                    Else
                        json.loanBase.iLoanDetail.loanCar.preapprovedBrand = dts.Tables(0).Rows(i).Item("preapprovedBrand")
                    End If

                    json.loanBase.iLoanDetail.loanCar.use = dts.Tables(0).Rows(i).Item("use")
                    json.loanBase.iLoanDetail.loanCar.initialAmount.amount = CDbl(dts.Tables(0).Rows(i).Item("initialAmount_amount")) ''checar esta conversion
                    json.loanBase.iLoanDetail.loanCar.percentageInitialAmount = CDbl(dts.Tables(0).Rows(i).Item("percentageInitialAmount"))  ''checar esta conversion

                    json.loanBase.loan.loanInstallments.installmentAmount.amount = CDbl(dts.Tables(0).Rows(i).Item("installmentAmount_amount"))  ''checar conversion
                    json.loanBase.loan.loanProduct.interestRate = CDbl(dts.Tables(0).Rows(i).Item("loanProduct_interestRate"))  ''checar conversion

                    json.loanBase.extendedData.preapprovedAmount.amount = CDbl(dts.Tables(0).Rows(i).Item("preapprovedAmount_amount")) ''checar conversion
                    json.loanBase.extendedData.expiredDebtPayed = CDbl(dts.Tables(0).Rows(i).Item("expiredDebtPayed")) ''checar conversion

                    json.numberTimesReferenceFoundClient = CDbl(dts.Tables(0).Rows(i).Item("numberTimesReferenceFoundClient"))
                    json.moratoriumRates.Add(CDbl(dts.Tables(0).Rows(i).Item("moratoriumRates_1"))) ''checar conversion
                    json.moratoriumRates.Add(CDbl(dts.Tables(0).Rows(i).Item("moratoriumRates_2"))) ''checar conversion
                    json.moratoriumRates.Add(CDbl(dts.Tables(0).Rows(i).Item("moratoriumRates_3"))) ''checar conversion

                    json.originType = dts.Tables(0).Rows(i).Item("originType")
                    json.referenceNumberUG = dts.Tables(0).Rows(i).Item("referenceNumberUG")
                    json.cbScore.loanToValue.amount = CDbl(dts.Tables(0).Rows(i).Item("loanToValue"))  ''checar conversion
                    json.agency.id = CDbl(dts.Tables(0).Rows(i).Item("agency_id")) ''checar conversion
                    json.agency.address.zipCode = dts.Tables(0).Rows(i).Item("agency_zipCode")  ''se modifico
                    json.agency.divisionId = CDbl(dts.Tables(0).Rows(i).Item("divisionId"))   ''checar conversion
                    json.agency.status = CDbl(dts.Tables(0).Rows(i).Item("agency_status"))   ''checar conversion
                Next
            End If
        End If
        Dim HermesJSON As String = ""
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        HermesJSON = serializer.Serialize(json)
        Return HermesJSON
    End Function

    Private Function SaveInfoHermesResponseOptimo(ByVal infoHermes As Hermes_Resp_WS, ByVal folio As String) As String

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

    Public Function getMessage(ByVal mensaje As String, ByVal WSvalido As Boolean, Optional ByVal AUTOMIK As Boolean = 0) As mensajeCod
        Dim msg As mensajeCod = New mensajeCod()

        If (WSvalido) Then
            Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
            Dim cmd As New SqlCommand
            Dim reader As SqlDataReader

            Try
                cmd.CommandText = "get_Message_Output_SP"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@MESSAGE_HERMES", mensaje)
                cmd.Parameters.AddWithValue("@AUTOMIK", AUTOMIK)
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

  
End Class
