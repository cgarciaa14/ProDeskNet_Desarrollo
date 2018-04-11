'BBVA-P-423 RQ-PI7-PD1 GVARGAS 23/10/2017 Mejoras CI Precalificaciòn & Preforma

Public Class DocumentDataOfCar
    Public creditNumber As String
    Public loan As loan_DDC = New loan_DDC()
    Public iLoanDetail As iLoanDetail_DDC = New iLoanDetail_DDC()
    Public customer As customer_DDC = New customer_DDC()
    Public loanTransaction As loanTransaction_DDC = New loanTransaction_DDC()
    Public paymentAnnuity As amount_DDC = New amount_DDC()
    Public earlyCancellationCommission As amount_DDC = New amount_DDC()
    Public collectionCommision As amount_DDC = New amount_DDC()
    Public ivaCollectionCommision As amount_DDC = New amount_DDC()

    Public Class loanTransaction_DDC
        Public payment As payment_DDC = New payment_DDC()
    End Class

    Public Class iLoanDetail_DDC
        Public loanCar As loanCar_DDC = New loanCar_DDC()
    End Class

    Public Class loanCar_DDC
        Public measure As measure_DDC = New measure_DDC()
        Public car As car_DDC = New car_DDC()
        Public invoice As invoice_DDC = New invoice_DDC()
        Public agency As agency_DDC = New agency_DDC()
        Public listDtoRate As List(Of extendedData_DDC) = New List(Of extendedData_DDC)()
        Public insurance As insurance_DDC = New insurance_DDC_2()
        Public insuranceAccessories As insuranceAccessories_DDC = New insuranceAccessories_DDC()
    End Class

    Public Class insuranceAccessories_DDC
        Public extendedData As extendedData_DDC = New extendedData_DDC()
    End Class

    Public Class agency_DDC
        Public id As Integer
        Public name As String
        Public rfc As String
    End Class

    Public Class invoice_DDC
        Public invoiceNumber As String
    End Class

    Public Class car_DDC
        Public brand As String
        Public model As String
        Public year As String
        Public engineNumber As String
        Public serialNumber As String
        Public newCar As String
    End Class

    Public Class measure_DDC
        Public total As Double
        Public totalPendingPayAmount As amount_DDC = New amount_DDC()
        Public totalNonPayment As amount_DDC = New amount_DDC()
    End Class

    Public Class loan_DDC
        Public pendingAmount As amount_DDC = New amount_DDC()
        Public awardedAmount As amount_DDC = New amount_DDC()
        Public loanProduct As loanProduct_DDC = New loanProduct_DDC()
        Public contract As contract_DDC = New contract_DDC()
        Public installments As installments_DDC = New installments_DDC()
        Public amortizationSchedule As amortizationSchedule_DDC = New amortizationSchedule_DDC()
        Public overdueLoan As overdueLoan_DDC = New overdueLoan_DDC()
    End Class

    Public Class amortizationSchedule_DDC
        Public loanAmortizations As List(Of loanAmortizations_DDC) = New List(Of loanAmortizations_DDC)
    End Class

    Public Class loanAmortizations_DDC
        Public capitalTotalAmount As amount_DDC = New amount_DDC()
        Public interestTotalAmount As amount_DDC = New amount_DDC()
        Public detailedAmortizationPayments As detailedAmortizationPayments_DDC = New detailedAmortizationPayments_DDC()
        Public extendedData As extendedData_DDC = New extendedData_DDC_4()
    End Class

    Public Class detailedAmortizationPayments_DDC
        Public loanAmortizationPayments As List(Of loanAmortizationPayments_DDC) = New List(Of loanAmortizationPayments_DDC)()
    End Class

    Public Class loanAmortizationPayments_DDC
        Public paymentDetail As paymentDetail_DDC = New paymentDetail_DDC()
    End Class

    Public Class paymentDetail_DDC
        Public loanPaymentDetails As List(Of loanPaymentDetails_DDC) = New List(Of loanPaymentDetails_DDC)()
    End Class

    Public Class loanPaymentDetails_DDC
        Public interestIva As amount_DDC = New amount_DDC()
    End Class

    Public Class overdueLoan_DDC
        Public capitalAmount As List(Of amount_DDC) = New List(Of amount_DDC)()
        Public ivaInterestAmount As List(Of amount_DDC) = New List(Of amount_DDC)()
        Public commissionAmount As List(Of amount_DDC) = New List(Of amount_DDC)()
        Public outstandingReceipts As List(Of Integer) = New List(Of Integer)()
    End Class

    Public Class installments_DDC
        Public installmentAmount As amount_DDC = New amount_DDC()
        Public installmentDate As String
        Public extendedData As extendedData_DDC = New extendedData_DDC_3()
        Public pendingAmount As amount_DDC = New amount_DDC()
        Public capitalAmounts As List(Of amount_DDC) = New List(Of amount_DDC)()
    End Class

    Public Class contract_DDC
        Public status As status_DDC = New status_DDC()
    End Class

    Public Class status_DDC
        Public name As String
    End Class

    Public Class loanProduct_DDC
        Public insurance As insurance_DDC = New insurance_DDC_1()
    End Class

    Public Class insurance_DDC
    End Class

    Public Class insurance_DDC_1
        Inherits insurance_DDC
        Public extendedData As extendedData_DDC = New extendedData_DDC_1()
        Public listInsurance As List(Of extendedData_DDC) = New List(Of extendedData_DDC)()
    End Class

    Public Class insurance_DDC_2
        Inherits insurance_DDC
        Public extendedData As extendedData_DDC = New extendedData_DDC_5()
    End Class

    Public Class amount_DDC
        Public amount As String
    End Class

    Public Class payment_DDC
        Public lastPaymentDate As String
    End Class

    Public Class customer_DDC
        Public person As New person_DDC
    End Class

    Public Class person_DDC
        Public name As String
        Public lastName As String
        Public mothersLastName As String
        Public legalAddress As legalAddress_DDC = New legalAddress_DDC()
        Public extendedData As extendedData_DDC = New extendedData_DDC_2()
    End Class

    Public Class legalAddress_DDC
        Public streetName As String
        Public streetNumber As String
        Public additionalInformation As String
        Public zipCode As String
        Public county As String
    End Class

    Public Class extendedData_DDC
    End Class

    Public Class extendedData_DDC_1
        Inherits extendedData_DDC
        Public insurer As String
        Public initialDate As String
        Public finalDate As String
        Public annualAmount As amount_DDC = New amount_DDC()
        Public pendingMonthlyPayments As List(Of Integer) = New List(Of Integer)()
    End Class

    Public Class extendedData_DDC_2
        Inherits extendedData_DDC
        Public rfc As String
    End Class

    Public Class extendedData_DDC_3
        Inherits extendedData_DDC
        Public statusDate As String
    End Class

    Public Class extendedData_DDC_4
        Inherits extendedData_DDC
        Public totalAnnualCost As amount_DDC = New amount_DDC()
        Public totalValueAddedTax As value_DDC = New value_DDC()
        Public totalDeferralBalance As amount_DDC = New amount_DDC()
    End Class

    Public Class extendedData_DDC_5
        Inherits extendedData_DDC

        Public policy As policy_DDC = New policy_DDC()
        Public outstandingBalanceInsurance As amount_DDC = New amount_DDC()
        Public insurer As String
        Public initialDate As String
        Public finalDate As String
        Public annualAmount As amount_DDC = New amount_DDC()
        Public interestRate As amount_DDC = New amount_DDC()
        Public totalOutstandingBalanceInsurance As amount_DDC = New amount_DDC()
        Public iva As amount_DDC = New amount_DDC()
    End Class

    Public Class policy_DDC
        Public policyId As String
    End Class

    Public Class value_DDC
        Public value As amount_DDC = New amount_DDC()
    End Class
End Class