'BBV-P-423: AMR: 12/04/2017 RQADM-18 Evaluación
Public Class clsScoreEvaluation

    Private _strerror As String = String.Empty

    Public ReadOnly Property StrError As String
        Get
            Return _strerror
        End Get
    End Property


    Public Class JSON
        Public quote As quote = New quote()
    End Class

    Public Class quote
        Public branch As branch = New branch()
        Public id As String = Nothing 'String.Empty
        Public customer As customer = New customer()
        Public coaccreditedCustomer As coaccreditedCustomer = New coaccreditedCustomer()
        Public loan As loan = New loan()
        Public scoringResults As String = Nothing 'String.Empty
        Public numberTimesReferenceFoundClient As Integer = 0
        Public numberTimesReferenceFoundCoaccredited As String = Nothing 'String.Empty
        Public moratoriumRates As New List(Of Double)
    End Class

    Public Class branch
        Public id As String = Nothing 'String.Empty
    End Class

    Public Class customer
        Public person As person = New person()
        Public economicData As economicData = New economicData()
        Public customerInformationResult As String = Nothing 'String.Empty
        Public relationshipGFP As String = Nothing 'String.Empty
    End Class

    Public Class person
        Public extendedDataper As extendedDataper '= New extendedDataper() ''Reemplazar con "extendedData"
        Public id As String = Nothing 'String.Empty
        Public lastName As String = Nothing 'String.Empty
        Public mothersLastName As String = Nothing 'String.Empty
        Public name As String = Nothing 'String.Empty
        Public addresses As String = Nothing 'String.Empty
        Public birthDate As String = Nothing 'String.Empty
        Public economicData As economicData = New economicData()
        Public contactsInformation As List(Of contactsInformation)

    End Class

    Public Class coaccreditedCustomer
        Public person As person = New person()
        Public economicData As economicData = New economicData()
        Public customerInformationResult As String = Nothing 'String.Empty
        Public relationshipGFP As String = Nothing 'String.Empty
    End Class

    Public Class contactsInformation
        Public telephone As telephone = New telephone()
    End Class

    Public Class telephone
        Public telephoneCompany As telephoneCompany = New telephoneCompany()
    End Class

    Public Class telephoneCompany
        Public id As String = Nothing 'String.Empty
    End Class

    Public Class economicData
        Public fixedIncomes As String = Nothing 'String.Empty
        Public variableIncomes As String = Nothing 'String.Empty
        Public otherPayments As String = Nothing 'String.Empty
        Public extendedData As extendedData = New extendedData()
    End Class

    Public Class loan
        Public subProductCode As String = Nothing 'String.Empty
        Public iLoanDetail As iLoanDetail = New iLoanDetail()
        Public subloan As subloan = New subloan() ''Reemplazar con "loan"
        Public extendedDataloan As extendedDataloan = New extendedDataloan() ''Reemplazar con "extendedData"
    End Class

    Public Class iLoanDetail
        Public loanCar As loanCar = New loanCar()
    End Class

    Public Class loanCar
        Public car As car = New car()
        Public termNumber As String = Nothing 'String.Empty
        Public invoice As invoice = New invoice()
        Public requestAmount As requestAmount = New requestAmount()
        Public preapprovedBrand As String = Nothing 'String.Empty
        Public use As String = Nothing 'String.Empty
        Public initialAmount As initialAmount = New initialAmount()
        Public percentageInitialAmount As String = Nothing 'String.Empty
    End Class

    Public Class car
        Public model As String = Nothing 'String.Empty
        Public brand As String = Nothing 'String.Empty
        Public subBrand As String = Nothing 'String.Empty
        Public isUsed As String = Nothing 'String.Empty
        Public aliance As String = Nothing 'String.Empty
    End Class

    Public Class invoice
        Public amount As amount = New amount()
    End Class

    Public Class amount
        Public amount As Double? ' = 0.00
        Public currency As String = Nothing 'String.Empty
    End Class


    Public Class requestAmount
        Public amount As Double?
        Public currency As String = Nothing 'String.Empty
    End Class

    Public Class initialAmount
        Public amount As Double?
        Public currency As String = Nothing 'String.Empty
    End Class

    Public Class subloan
        Public loanInstallments As loanInstallments = New loanInstallments()
        Public loanProduct As loanProduct = New loanProduct()
    End Class

    Public Class loanInstallments
        Public installmentAmount As installmentAmount = New installmentAmount()
    End Class

    Public Class installmentAmount
        Public amount As Double?
        Public currency As String = Nothing 'String.Empty
    End Class

    Public Class loanProduct
        Public interestRate As Double = 0.00
    End Class

    Public Class extendedDataper
        Public rfc As String = Nothing 'String.Empty
        Public sex As String = Nothing 'String.Empty
        Public populationRegisterId As String = Nothing 'String.Empty
        Public homonym As String = Nothing 'String.Empty
        Public facebook As Boolean = False
    End Class

    Public Class extendedData
        Public automaticPayment As automaticPayment
        Public othersIncomes As New List(Of othersIncomes)
        Public variableIncomes As variableIncomes '= New variableIncomes()
        Public fixedIncomes As fixedIncomes '= New fixedIncomes()
    End Class

    Public Class automaticPayment
        Public active As Integer?
    End Class

    Public Class othersIncomes
        Public amount As Double?
        Public currency As String = Nothing 'String.Empty
    End Class

    Public Class variableIncomes
        Public amount As Double?
        Public currency As String = Nothing 'String.Empty
    End Class

    Public Class fixedIncomes
        Public amount As Double?
        Public currency As String = Nothing 'String.Empty
    End Class

    Public Class extendedDataloan
        Public preapprovedAmount As preapprovedAmount = New preapprovedAmount()
        Public expiredDebtPayed As Integer = 0
    End Class

    Public Class preapprovedAmount
        Public amount As Double?
        Public currency As String = Nothing 'String.Empty
    End Class

    Public Class msjerr
        Public message As String
        Public status As String
    End Class

    Sub New()

    End Sub

    Public Function GetScore() As Double
        Dim score As Double = 0.0
        Dim json As New JSON()

        json.quote.branch.id = "2167"
        json.quote.id = "0764574961"
        Dim extdatper As extendedDataper = New extendedDataper
        extdatper.facebook = True

        'json.quote.customer.person.extendedDataper.rfc = "null"
        'json.quote.customer.person.extendedDataper.sex = "null"
        'json.quote.customer.person.extendedDataper.populationRegisterId = "null"
        json.quote.customer.person.extendedDataper = extdatper
        json.quote.customer.person.id = "J0819866"
        'json.quote.customer.person.lastName = "null"
        'json.quote.customer.person.mothersLastName = "null"
        'json.quote.customer.person.name = "null"
        'json.quote.customer.person.addresses = "null"
        'json.quote.customer.person.birthDate = "null"
        'json.quote.customer.person.economicData.fixedIncomes = "null"
        'json.quote.customer.person.economicData.variableIncomes = "null"
        'json.quote.customer.person.economicData.otherPayments = "null"

        Dim act As automaticPayment = New automaticPayment()
        act.active = 0
        json.quote.customer.person.economicData.extendedData.automaticPayment = act

        Dim ls1 As othersIncomes = New othersIncomes()
        'ls1.amount = "null"
        'ls1.currency = "null"
        json.quote.customer.person.economicData.extendedData.othersIncomes.Add(ls1)

        Dim ls2 As othersIncomes = New othersIncomes()
        ls2.amount = 35000
        'ls2.currency = "null"
        json.quote.customer.person.economicData.extendedData.othersIncomes.Add(ls2)

        Dim ls3 As othersIncomes = New othersIncomes()
        ls3.amount = 20000
        'ls3.currency = "null"
        json.quote.customer.person.economicData.extendedData.othersIncomes.Add(ls3)

        Dim var As variableIncomes = New variableIncomes()
        var.amount = 30000
        json.quote.customer.person.economicData.extendedData.variableIncomes = var

        Dim fix As fixedIncomes = New fixedIncomes()
        fix.amount = 25000
        json.quote.customer.person.economicData.extendedData.fixedIncomes = fix

        Dim ls As New List(Of contactsInformation)
        Dim lstel As contactsInformation = New contactsInformation()
        lstel.telephone.telephoneCompany.id = "S"
        ls.Add(lstel)
        json.quote.customer.person.contactsInformation = ls

        'json.quote.customer.economicData.fixedIncomes = "null"
        'json.quote.customer.economicData.variableIncomes = "null"
        'json.quote.customer.economicData.otherPayments = "null"
        'json.quote.customer.economicData.extendedData.automaticPayment = "null"


        Dim ls4 As othersIncomes = New othersIncomes()
        ls4.amount = 40000
        'ls4.currency = "null"
        json.quote.customer.economicData.extendedData.othersIncomes.Add(ls4)

        'json.quote.customer.customerInformationResult = "null"
        'json.quote.customer.relationshipGFP = "null"

        Dim ls5 As othersIncomes = New othersIncomes()
        json.quote.coaccreditedCustomer.person.economicData.extendedData.othersIncomes.Add(ls5)

        Dim ls6 As othersIncomes = New othersIncomes()
        ls6.amount = 0
        json.quote.coaccreditedCustomer.person.economicData.extendedData.othersIncomes.Add(ls6)

        Dim ls7 As othersIncomes = New othersIncomes()
        ls7.amount = 0
        json.quote.coaccreditedCustomer.person.economicData.extendedData.othersIncomes.Add(ls7)

        Dim ls8 As othersIncomes = New othersIncomes()
        ls8.amount = 0
        json.quote.coaccreditedCustomer.person.economicData.extendedData.othersIncomes.Add(ls8)

        Dim varcoa As variableIncomes = New variableIncomes()
        varcoa.amount = 0
        json.quote.coaccreditedCustomer.person.economicData.extendedData.variableIncomes = varcoa

        Dim fixcoa As fixedIncomes = New fixedIncomes()
        fixcoa.amount = 0
        json.quote.coaccreditedCustomer.person.economicData.extendedData.fixedIncomes = fixcoa

        Dim ls9 As othersIncomes = New othersIncomes()
        ls9.amount = 0
        json.quote.coaccreditedCustomer.economicData.extendedData.othersIncomes.Add(ls9)

        json.quote.loan.subProductCode = "CZ04"
        json.quote.loan.iLoanDetail.loanCar.car.model = "2017"
        json.quote.loan.iLoanDetail.loanCar.car.brand = "008"
        json.quote.loan.iLoanDetail.loanCar.car.subBrand = "0014"
        json.quote.loan.iLoanDetail.loanCar.car.isUsed = "N"
        json.quote.loan.iLoanDetail.loanCar.car.aliance = "01"
        json.quote.loan.iLoanDetail.loanCar.termNumber = "48"
        json.quote.loan.iLoanDetail.loanCar.invoice.amount.amount = 175000
        'json.quote.loan.iLoanDetail.loanCar.invoice.amount.currency = "null"
        json.quote.loan.iLoanDetail.loanCar.requestAmount.amount = 100000
        'json.quote.loan.iLoanDetail.loanCar.requestAmount.currency = "null"
        'json.quote.loan.iLoanDetail.loanCar.preapprovedBrand = "null"
        json.quote.loan.iLoanDetail.loanCar.use = "02"
        json.quote.loan.iLoanDetail.loanCar.initialAmount.amount = 50000
        'json.quote.loan.iLoanDetail.loanCar.initialAmount.currency = "null"
        json.quote.loan.iLoanDetail.loanCar.percentageInitialAmount = "40"
        json.quote.loan.subloan.loanInstallments.installmentAmount.amount = 8000
        'json.quote.loan.subloan.loanInstallments.installmentAmount.currency = "null"
        json.quote.loan.subloan.loanProduct.interestRate = 5.2
        json.quote.loan.extendedDataloan.preapprovedAmount.amount = 90000
        'json.quote.loan.extendedDataloan.preapprovedAmount.currency = "null"
        json.quote.loan.extendedDataloan.expiredDebtPayed = 0
        'json.quote.scoringResults = "null"
        json.quote.numberTimesReferenceFoundClient = 2
        'json.quote.numberTimesReferenceFoundCoaccredited = "null"


        json.quote.moratoriumRates.Add(3.0)
        json.quote.moratoriumRates.Add(2.0)
        json.quote.moratoriumRates.Add(1.0)

        Dim restGT As RESTful = New RESTful()
        Dim jsonResult As String
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

        Dim jsonBODY As String = serializer.Serialize(json)
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        jsonBODY = Replace(jsonBODY, "extendedDataloan", "extendedData")
        jsonBODY = Replace(jsonBODY, "subloan", "loan")
        jsonBODY = Replace(jsonBODY, "extendedDataper", "extendedData")

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("urlScore")
        restGT.buscarHeader("ResponseWarningDescription")
        jsonResult = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

        Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim alert = srrSerialer.Deserialize(Of msjerr)(jsonResult)

        If restGT.IsError Then
            If restGT.MensajeError <> "" Then
                _strerror = "Servicio Web: " & restGT.MensajeError
                Return Nothing
                Exit Function
            Else
                If Not alert.message = Nothing Then
                    _strerror = "Servicio Web: " & alert.message
                    Return Nothing
                    Exit Function
                End If
            End If
        End If

        score = 250000.0

        Return score
    End Function

End Class
