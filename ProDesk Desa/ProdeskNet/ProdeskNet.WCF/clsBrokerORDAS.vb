'BBV-P-423 20/01/2017: JRHM RQCONYFOR-02: Se crean clases para payloads y respuestas de servicios de emision e impresion de ORDAS para tarea "Documentación de Pólizas de Seguros y Desembolso" (NOTA: Payload y respuesta de servicio emision aun no aplicados a solucion)
'BUG-PD-68: RHERNANDEZ:  02/06/17: SE CREARON PAYLOADS DE EMISION Y CANCELACION DE SEGURO DE DAÑOS ORDAS
#Region "Payload Emision de Seguro ORDAS"
Public Class clsBrokerEmiORDAS
    Public policy As policyORDAS = New policyORDAS()
End Class
Public Class policyORDAS
    Public complement As complementORDAS = New complementORDAS
    Public iPolicy As iPolicyORDAS = New iPolicyORDAS
    Public idPack As Integer
    Public idProduct As Integer
    Public idTerm As Integer
    Public agencyNumber As Integer
    Public insurerId As Integer
    Public insuredAmount As insuredAmountORDAS = New insuredAmountORDAS
    Public idAdditionalPaymentWay As String
    Public idAdditionalTerm As String
    Public idAdditionalPack As Integer
    Public numberFinancialContract As String
    Public idPaymentWay As String
    Public preferredBeneficiary As String
    Public contractor As contractorORDAS = New contractorORDAS
    Public validityPeriod As validityPeriodORDAS2 = New validityPeriodORDAS2
    Public currency As currencyORDAS = New currencyORDAS
End Class
Public Class complementORDAS
    Public user As userORDAS = New userORDAS
End Class
Public Class userORDAS
    Public id As String
    Public credentials As credentialsORDAS = New credentialsORDAS
End Class
Public Class credentialsORDAS
    Public accessPassword As String
End Class
Public Class iPolicyORDAS
    Public vehiclePolicy As vehiclePolicyORDAS = New vehiclePolicyORDAS
End Class
Public Class vehiclePolicyORDAS
    Public idVehicle As String
    Public idUse As Integer
    Public accessoryDescription As String
    Public invoiceNumber As String
    Public credit As creditORDAS = New creditORDAS
    Public car As carORDAS = New carORDAS
    Public accessorySum As accessorySumORDAS = New accessorySumORDAS
    Public driver As driverORDAS = New driverORDAS
End Class
Public Class creditORDAS
    Public idFinancingTerm As Integer
    Public validityPeriod As validityPeriodORDAS = New validityPeriodORDAS
End Class
Public Class validityPeriodORDAS
    Public startDate As String
    Public endDate As String
End Class
Public Class carORDAS
    Public serialNumber As String
    Public engineNumber As String
    Public repuve As String
    Public plates As String
End Class
Public Class accessorySumORDAS
    Public amount As Double
End Class
Public Class driverORDAS
    Public legalAddress As New driverlegalAddressORDAS
    Public name As String
    Public lastName As String
    Public mothersLastName As String
    Public extendedData As extendedDataORDAS = New extendedDataORDAS
End Class
Public Class driverlegalAddressORDAS
    Public zipCode As String
End Class
Public Class extendedDataORDAS
    Public age As String
    Public rfc As String
    Public gender As String
End Class
Public Class insuredAmountORDAS
    Public amount As Double
End Class
Public Class contractorORDAS
    Public name As String
    Public lastName As String
    Public mothersLastName As String
    Public birthDate As String
    Public extendedData As extendedDataORDAS2 = New extendedDataORDAS2
    Public legalAddress As legalAddressORDAS = New legalAddressORDAS
End Class
Public Class extendedDataORDAS2
    Public rfc As String
    Public email As String
    Public gender As String
    Public maritalStatus As String
    Public fiscalSituation As fiscalSituationORDAS = New fiscalSituationORDAS
    Public bussinessData As bussinessDataORDAS = New bussinessDataORDAS
    Public homePhone As homePhoneORDAS = New homePhoneORDAS
    Public mobilePhone As mobilePhoneORDAS = New mobilePhoneORDAS
End Class
Public Class fiscalSituationORDAS
    Public relationName As Integer
End Class
Public Class bussinessDataORDAS
    Public name As String
    Public constitutionDate As String
End Class
Public Class homePhoneORDAS
    Public telephoneNumber As String
End Class
Public Class mobilePhoneORDAS
    Public telephoneNumber As String
End Class
Public Class legalAddressORDAS
    Public zipCode As String
    Public neighborhood As String
    Public additionalAddress As String
    Public externalNumber As String
    Public door As String
    Public county As String
    Public state As String
    Public stateId As String
End Class
Public Class validityPeriodORDAS2
    Public startDate As String
    Public endDate As String
End Class
Public Class currencyORDAS
    Public id As String
End Class
#End Region

#Region "Respuesta Emision de Seguro ORDAS"
Public Class clsResEmiBrokerORDAS
    Public policy As New respolicyORDAS
End Class
Public Class respolicyORDAS
    Public complement As New rescomplementORDAS
    Public fee As New resfeeORDAS
    Public contractor As New rescontractorORDAS
    Public validityPeriod As New resvalidityPeriodORDAS
    Public shippingCosts As New resshippingCostsORDAS
    Public tax As New restaxORDAS
    Public realPremium As New resrealPremiumORDAS
    Public totalPremium As New restotalPremiumORDAS
    Public lateFee As New reslateFeeORDAS
    Public receipts As List(Of resreceiptsORDAS)
    Public insuredAmount As New resinsuredAmountORDAS
    Public currency As New rescurrencyORDAS
    Public warrantyExtension As List(Of reswarrantyExtensionORDAS)
    Public cancelData As New rescancelDataORDAS
    Public printingData As New resprintingDataORDAS
End Class
Public Class rescomplementORDAS
    Public user As New resuserORDAS
    Public errorInfo As New reserrorInfoORDAS
End Class
Public Class resuserORDAS
    Public credentials As New rescredentialsORDAS
End Class
Public Class reserrorInfoORDAS
    Public description As String
End Class
Public Class rescredentialsORDAS
End Class
Public Class resfeeORDAS
End Class
Public Class rescontractorORDAS
    Public legalAddress As New reslegalAddressORDAS
    Public extendedData As New resextendedDataORDAS
    Public nationality As New resnationalityORDAS
End Class
Public Class reslegalAddressORDAS
End Class
Public Class resextendedDataORDAS
    Public countryOrigin As New rescountryOriginORDAS
    Public homePhone As New reshomePhoneORDAS
    Public mobilePhone As New resmobilePhoneORDAS
    Public fiscalSituation As New resfiscalSituationORDAS
    Public bussinessData As New resbussinessDataORDAS
End Class
Public Class rescountryOriginORDAS
End Class
Public Class reshomePhoneORDAS
End Class
Public Class resmobilePhoneORDAS
End Class
Public Class resfiscalSituationORDAS
End Class
Public Class resbussinessDataORDAS
End Class
Public Class resnationalityORDAS
End Class
Public Class resvalidityPeriodORDAS
End Class
Public Class resshippingCostsORDAS
    Public amount As String
End Class
Public Class restaxORDAS
    Public value As New resvalueORDAS
End Class
Public Class resvalueORDAS
    Public amount As String
End Class
Public Class resrealPremiumORDAS
    Public amount As String
End Class
Public Class restotalPremiumORDAS
    Public amount As String
End Class
Public Class reslateFeeORDAS
    Public amount As String
End Class
Public Class resreceiptsORDAS
    Public validityPeriod As New resvalidityPeriodORDAS
    Public shippingCosts As New resshippingCostsORDAS
    Public tax As New restaxORDAS2
    Public realPremium As New resrealPremiumORDAS
    Public totalPremium As New restotalPremiumORDAS
    Public lateFee As New resfeeORDAS
    Public serialNumber As String
    Public idRequest As String
    Public insurerId As String
    Public idPack As String
    Public totalNumberReceipts As String
    Public idPolicy As String
End Class
Public Class restaxORDAS2
    Public value As New resvalueORDAS
End Class
Public Class resinsuredAmountORDAS
End Class
Public Class rescurrencyORDAS
End Class
Public Class reswarrantyExtensionORDAS
End Class
Public Class rescancelDataORDAS
End Class
Public Class resprintingDataORDAS
End Class
#End Region

#Region "Payload Impresion de Seguro ORDAS"
Public Class clsImpBrokerORDAS
    Public policy As policyORDAS2 = New policyORDAS2
End Class
Public Class policyORDAS2
    Public complement As complementORDAS = New complementORDAS()
    Public policyId As String
End Class
#End Region

#Region "Respuesta Impresion de Seguro ORDAS"
Public Class clsResImpBrokerORDAS
    Public policy As policyORDAS4 = New policyORDAS4
End Class
Public Class policyORDAS4
    Public urlPolicy As String
End Class

#End Region

#Region "Payload Cancelacion de Seguro de danos ORDAS"
Public Class CanPolORDAS
    Public policy As New CancpolicyORDAS
End Class
Public Class CancpolicyORDAS
    Public complement As New complementORDAS
    Public policyId As String
    Public cancelData As New cancelDataOrdas
End Class
Public Class cancelDataOrdas
    Public effectiveDate As String
    Public idCancelReason As String
    Public notes As String
    Public param1 As String
    Public param2 As String
End Class

#End Region

#Region "Payload Respuesta Cancelacion de Seguro de danos ORDAS"
Public Class resCanPolORDAS
    Public policy As New resCanclORDAS
End Class
Public Class resCanclORDAS
    Public complement As New rescomplementORDAS
    Public fee As New resfeeORDAS
    Public contractor As New rescontractorORDAS
    Public validityPeriod As New resvalidityPeriodORDAS
    Public shippingCosts As New resshippingCostsORDAS
    Public tax As New restaxORDAS2
    Public realPremium As New resrealPremiumORDAS
    Public totalPremium As New restotalPremiumORDAS
    Public lateFee As New reslateFeeORDAS
    Public insuredAmount As New resinsuredAmountORDAS
    Public currency As New rescurrencyORDAS
    Public cancelData As New rescancelDataORDAS2
    Public printingData As New resprintingDataORDAS
End Class
Public Class rescancelDataORDAS2
    Public [error] As New reserrorORDAS
    Public dateRequest As String
    Public idRequest As String
    Public numOt As String
End Class
Public Class reserrorORDAS
    Public description As String
End Class
#End Region