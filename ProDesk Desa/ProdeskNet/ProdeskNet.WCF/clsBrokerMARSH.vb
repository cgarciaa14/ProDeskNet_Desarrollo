'BBV-P-423 20/01/2017: JRHM RQCONYFOR-02: Se crean clases para payloads y respuestas de servicios de emision e impresion de MARSH para tarea "Documentación de Pólizas de Seguros y Desembolso" (NOTA: Payload y respuesta de servicio emision aun no aplicados a solucion)
'BUG-PD-33 24/04/17 JRHM SE AGREGO LA CLASE PARA GENERAR EL PAYLOAD DE EMISION DE SEGURO MARSH
'BUG-PD-45: RHERNANDEZ: 15/05/17 SE AGREGARON LAS CLASES ENCARGADAS DE LOS PAYLOADS DE CANCELACION DE POLIZAS
#Region "Payload Emision de Seguro MARSH"
Public Class clsBrokerMARSH
    Public policy As policyMARSH = New policyMARSH()
End Class
Public Class policyMARSH
    Public iPolicy As iPolicyMARSH = New iPolicyMARSH()
    Public iComplement As iComplementMARSH = New iComplementMARSH()
    Public complement As complementMARSH = New complementMARSH()
    Public contractor As contractorMARSH = New contractorMARSH()
    Public preferredBeneficiary As String
End Class
Public Class iPolicyMARSH
    Public vehiclePolicy As vehiclePolicyMARSH = New vehiclePolicyMARSH()
End Class
Public Class vehiclePolicyMARSH
    Public car As carMARSH = New carMARSH()
    Public idQuote As String
    Public credit As creditMARSH = New creditMARSH()
    Public invoiceNumber As String
End Class
Public Class carMARSH
    Public serialNumber As String
    Public engineNumber As String
    Public cylinders As String
    Public chassis As String
    Public capacity As String
End Class
Public Class creditMARSH
    Public idFinancingTerm As String
    Public validityPeriod As validityPeriodMARSH = New validityPeriodMARSH()
End Class
Public Class validityPeriodMARSH
    Public startDate As String
    Public endDate As String
End Class
Public Class iComplementMARSH
    Public EmissionComplementMarsh As EmissionComplementMarsh = New EmissionComplementMarsh()
End Class
Public Class EmissionComplementMarsh
    Public contractNumber As String
End Class
Public Class complementMARSH
    Public user As userMARSH = New userMARSH()
End Class
Public Class userMARSH
    Public id As String
    Public credentials As credentialsMARSH = New credentialsMARSH()
End Class
Public Class credentialsMARSH
    Public accessPassword As String
End Class
Public Class contractorMARSH
    Public name As String
    Public lastName As String
    Public mothersLastName As String
    Public extendedData As extendedDataMARSH = New extendedDataMARSH()
    Public legalAddress As legalAddressMARSH = New legalAddressMARSH()
    Public birthDate As String
End Class
Public Class extendedDataMARSH
    Public homePhone As homePhoneMARSH = New homePhoneMARSH
    Public rfc As String
    Public email As String
    Public fiscalSituation As fiscalSituationMARSH = New fiscalSituationMARSH()
    Public gender As String
    Public maritalStatus As String
End Class
Public Class homePhoneMARSH
    Public telephoneNumber As String
End Class
Public Class fiscalSituationMARSH
    Public relationName As String
End Class
Public Class legalAddressMARSH
    Public zipCode As String
    Public streetName As String
    Public externalNumber As String
    Public city As String
    Public state As String
    Public neighborhood As String
End Class
#End Region

#Region "Respuesta Emision de Seguro MARSH"
Public Class clsResEmiBrokerMARSH
    Public policy As RespolicyMARSH = New RespolicyMARSH()
End Class
Public Class RespolicyMARSH
    Public iComplement As ResiComplementMARSH = New ResiComplementMARSH()
    Public complement As RescomplementMARSH = New RescomplementMARSH()
    Public fee As ResfeeMARSH = New ResfeeMARSH()
    Public contractor As RescontractorMARSH = New RescontractorMARSH()
    Public validityPeriod As ResvalidityPeriodMARSH = New ResvalidityPeriodMARSH()
    Public requestDate As String
    Public shippingCosts As ResshippingCostsMARSH = New ResshippingCostsMARSH()
    Public tax As RestaxMARSH = New RestaxMARSH()
    Public policyId As String
    Public realPremium As ResrealPremiumMARSH = New ResrealPremiumMARSH()
    Public totalPremium As RestotalPremiumMARSH = New RestotalPremiumMARSH()
    Public lateFee As ReslateFeeMARSH = New ReslateFeeMARSH()
    Public receipts As List(Of ResreceiptsMARSH)
    Public typeId As String
    Public serviceType As String
    Public totalNumberReceipts As String
    Public insurerId As String
    Public insuredAmount As ResinsuredAmountMARSH = New ResinsuredAmountMARSH()
    Public currency As RescurrencyMARSH = New RescurrencyMARSH()
    Public cancelData As RescancelDataMARSH = New RescancelDataMARSH()
    Public printingData As ResprintingDataMARSH = New ResprintingDataMARSH()
End Class
Public Class ResiComplementMARSH
    Public EmissionComplementMarsh As ResEmissionComplementMarsh = New ResEmissionComplementMarsh()
End Class
Public Class ResEmissionComplementMarsh
    Public [error] As ReserrorMARSH = New ReserrorMARSH() 'ver como dejarla como error
    Public endorsement As String
    Public subsection As String
End Class
Public Class ReserrorMARSH
    Public errorId As String
    Public description As String
End Class
Public Class RescomplementMARSH
    Public user As ResuserMARSH = New ResuserMARSH()
    Public errorInfo As ReserrorInfoMARSH = New ReserrorInfoMARSH()
End Class
Public Class ResuserMARSH
    Public credentials As RescredentialsMARSH = New RescredentialsMARSH()
End Class
Public Class RescredentialsMARSH
End Class
Public Class ReserrorInfoMARSH
End Class
Public Class ResfeeMARSH
End Class
Public Class RescontractorMARSH
    Public legalAddress As ReslegalAddressMARSH = New ReslegalAddressMARSH()
    Public extendedData As ResextendedDataMARSH = New ResextendedDataMARSH()
    Public nationality As ResnationalityMARSH = New ResnationalityMARSH()
End Class
Public Class ReslegalAddressMARSH
End Class
Public Class ResnationalityMARSH
End Class
Public Class ResextendedDataMARSH
    Public countryOrigin As RescountryOriginMARSH = New RescountryOriginMARSH()
    Public homePhone As ReshomePhoneMARSH = New ReshomePhoneMARSH()
    Public mobilePhone As ResmobilePhoneMARSH = New ResmobilePhoneMARSH()
    Public fiscalSituation As ResfiscalSituationMARSH = New ResfiscalSituationMARSH()
    Public bussinessData As ResbussinessDataMARSH = New ResbussinessDataMARSH()
End Class
Public Class RescountryOriginMARSH
End Class
Public Class ReshomePhoneMARSH
End Class
Public Class ResmobilePhoneMARSH
End Class
Public Class ResfiscalSituationMARSH
End Class
Public Class ResbussinessDataMARSH
End Class
Public Class ResvalidityPeriodMARSH
    Public startDate As String
    Public endDate As String
End Class
Public Class ResshippingCostsMARSH
    Public amount As String
End Class
Public Class RestaxMARSH
    Public value As ResvalueMARSH = New ResvalueMARSH()
End Class
Public Class ResvalueMARSH
    Public amount As String
End Class
Public Class ResrealPremiumMARSH
    Public amount As String
End Class
Public Class RestotalPremiumMARSH
    Public amount As String
End Class
Public Class ReslateFeeMARSH
    Public amount As String
End Class
Public Class ResreceiptsMARSH
    Public validityPeriod As ResvalidityPeriodMARSH = New ResvalidityPeriodMARSH()
    Public shippingCosts As ResshippingCostsMARSH = New ResshippingCostsMARSH()
    Public tax As RestaxMARSH = New RestaxMARSH()
    Public realPremium As ResrealPremiumMARSH = New ResrealPremiumMARSH()
    Public totalPremium As RestotalPremiumMARSH = New RestotalPremiumMARSH()
    Public lateFee As ReslateFeeMARSH = New ReslateFeeMARSH()
    Public serialNumber As String
End Class
Public Class ResinsuredAmountMARSH
End Class
Public Class RescurrencyMARSH
End Class
Public Class RescancelDataMARSH
End Class
Public Class ResprintingDataMARSH
End Class
#End Region

#Region "Payload Impresion de Seguro MARSH"
Public Class clsImpBrokerMARSH
    Public policy As policyMARSH2 = New policyMARSH2
End Class
Public Class policyMARSH2
    Public complement As complementMARSH = New complementMARSH
    Public agencyNumber As String
    Public policyId As String
    Public printingData As printingData = New printingData
End Class

Public Class printingData
    Public subsection As String
    Public endorsement As String
    Public requestDate As String
    Public reference1 As String
    Public reference2 As String
End Class
#End Region

#Region "Respuesta Impresion de Seguro MARSH"
Public Class clsResImpBrokerMARSH
    Public policy As policyMARSH4 = New policyMARSH4
End Class
Public Class policyMARSH4
    Public printingData As printingDataMARSH = New printingDataMARSH
End Class
Public Class printingDataMARSH
    Public policyCode As String
End Class
#End Region

#Region "Payload Cancelacion de Poliza MARSH"
Public Class SolCanclPolMARSH
    Public policy As New policyCanclMARSH
End Class
Public Class policyCanclMARSH
    Public complement As New complementMARSH
    Public policyId As String
    Public cancelData As New cancelDataMARSH
End Class
Public Class cancelDataMARSH
    Public effectiveDate As String
    Public idCancelReason As String
    Public notes As String
    Public param1 As String
    Public param2 As String
End Class
#End Region

#Region "Payload Respuesta Cancelacion de poliza MARSH"
Public Class ResCanclPolMARSH
    Public policy As New RespolicyCanclMARSH
End Class
Public Class RespolicyCanclMARSH
    Public complement As New RescomplementMARSH
    Public fee As New ResfeeMARSH
    Public contractor As New RescontractorMARSH
    Public validityPeriod As New ResvalidityPeriodMARSH
    Public shippingCosts As New ResshippingCostsMARSH
    Public tax As New RestaxMARSH
    Public realPremium As New ResrealPremiumMARSH
    Public totalPremium As New RestotalPremiumMARSH
    Public lateFee As New ReslateFeeMARSH
    Public insuredAmount As New ResinsuredAmountMARSH
    Public currency As New RescurrencyMARSH
    Public cancelData As New ResCanclcancelDataMARSH
    Public printingData As New ResprintingDataMARSH
End Class
Public Class ResCanclcancelDataMARSH
    Public [error] As New ReserrorMARSH
    Public registerDate As String
    Public endorsementId As String
    Public reference1 As String
    Public reference2 As String
End Class
#End Region