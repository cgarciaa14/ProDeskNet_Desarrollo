'BBV-P-423 20/01/2017: JRHM RQCONYFOR-02: Se crean clases para payloads y respuestas de servicios de emision e impresion de EIKOS para tarea "Documentación de Pólizas de Seguros y Desembolso" (NOTA: Payload y respuesta de servicio emision aun no aplicados a solucion)
'BUG-PD-33 24/04/17 JRHM SE AGREGO LA CLASE PARA GENERAR EL PAYLOAD DE EMISION DE SEGURO EIKOS
'BUG-PD-45: RHERNANDEZ: 15/05/17 SE AGREGARON LAS CLASES ENCARGADAS DE LOS PAYLOADS DE CANCELACION DE POLIZAS
'BUG-PD-128: RHERNANDEZ: 01/07/17: SE AGREGA PAYLOAD PARA GESTION DE ERRORES DE EIKOS
#Region "Payload Emision Seguro EIKOS"
Public Class clsBrokerEIKOS
    Public policy As policyEIKOS = New policyEIKOS()
End Class
Public Class policyEIKOS
    Public iPolicy As iPolicyEIKOS = New iPolicyEIKOS()
    Public iComplement As iComplementEIKOS = New iComplementEIKOS()
    Public complement As complementEIKOS = New complementEIKOS()
    Public agencyNumber As String
    Public fee As feeEIKOS = New feeEIKOS()
    Public contractor As contractorEIKOS = New contractorEIKOS()
    Public validityPeriod As validityPeriodEIKOS = New validityPeriodEIKOS()
End Class
Public Class iPolicyEIKOS
    Public vehiclePolicy As vehiclePolicyEIKOS = New vehiclePolicyEIKOS()
End Class
Public Class vehiclePolicyEIKOS
    Public idQuote As String
    Public car As carEIKOS = New carEIKOS()
End Class
Public Class carEIKOS
    Public key As String
    Public model As String
    Public serialNumber As String
    Public engineNumber As String
    Public plates As String
    Public repuve As String
    Public location As String
End Class
Public Class iComplementEIKOS
    Public EmissionComplementEikos As EmissionComplementEikos = New EmissionComplementEikos()
End Class
Public Class EmissionComplementEikos
    Public employeeId As String
    Public creditId As String
    Public pol_exp As String
End Class
Public Class complementEIKOS
    Public user As userEIKOS = New userEIKOS()
End Class
Public Class userEIKOS
    Public id As String
    Public credentials As credentialsEIKOS = New credentialsEIKOS()
End Class
Public Class credentialsEIKOS
    Public accessPassword As String
End Class
Public Class feeEIKOS
    Public amount As Double
End Class
Public Class contractorEIKOS
    Public name As String
    Public middleName As String
    Public lastName As String
    Public mothersLastName As String
    Public birthDate As String
    Public legalAddress As legalAddressEIKOS = New legalAddressEIKOS()
    Public extendedData As extendedDataEIKOS = New extendedDataEIKOS()
End Class
Public Class legalAddressEIKOS
    Public streetName As String
    Public externalNumber As String
    Public door As String
    Public neighborhood As String
    Public zipCode As String
    Public stateId As String
    Public county As Integer
    Public country As countryEIKOS = New countryEIKOS()
End Class
Public Class countryEIKOS
    Public name As String
End Class
Public Class extendedDataEIKOS
    Public rfc As String
    Public fiscalSituation As fiscalSituationEIKOS = New fiscalSituationEIKOS()
    Public gender As String
    Public maritalStatus As String
    Public curp As String
    Public occupation As String
    Public email As String
    Public countryOrigin As countryOriginEIKOS = New countryOriginEIKOS()
    Public homePhone As homePhoneEIKOS = New homePhoneEIKOS()
    Public mobilePhone As mobilePhoneEIKOS = New mobilePhoneEIKOS()
End Class
Public Class fiscalSituationEIKOS
    Public relationName As Integer
End Class
Public Class countryOriginEIKOS
    Public name As String
End Class
Public Class homePhoneEIKOS
    Public countryCode As String
    Public areaCode As String
    Public telephoneNumber As String
    Public phoneExtension As String
End Class
Public Class mobilePhoneEIKOS
    Public countryCode As String
    Public areaCode As String
    Public telephoneNumber As String
    Public phoneExtension As String
End Class
Public Class validityPeriodEIKOS
    Public startDate As String
    Public endDate As String
End Class
#End Region

#Region "Respuesta Emision Seguro EIKOS"
Public Class clsResEmiBrokerEIKOS
    Public policy As RespolicyEIKOS = New RespolicyEIKOS()
End Class
Public Class RespolicyEIKOS
    Public policy As ResipolicyEIKOS = New ResipolicyEIKOS()
    Public complement As RescomplementEIKOS = New RescomplementEIKOS()
    Public fee As ResfeeEIKOS = New ResfeeEIKOS()
    Public contractor As RescontractorEIKOS = New RescontractorEIKOS()
    Public validityPeriod As ResvalidityPeriodEIKOS = New ResvalidityPeriodEIKOS()
    Public shippingCosts As ResshippingCostsEIKOS = New ResshippingCostsEIKOS()
    Public tax As RestaxEIKOS = New RestaxEIKOS()
    Public policyId As String
    Public realPremium As ResrealPremiumEIKOS = New ResrealPremiumEIKOS()
    Public totalPremium As RestotalPremiumEIKOS = New RestotalPremiumEIKOS()
    Public lateFee As ReslateFeeEIKOS = New ReslateFeeEIKOS()
    Public insuredAmount As ResinsuredAmountEIKOS = New ResinsuredAmountEIKOS()
    Public currency As RescurrencyEIKOS = New RescurrencyEIKOS()
    Public cancelData As RescancelDataEIKOS = New RescancelDataEIKOS()
    Public printingData As ResprintingDataEIKOS = New ResprintingDataEIKOS()
    Public urlPolicy As String
End Class
Public Class ResipolicyEIKOS
    Public vehiclePolicy As ResvehiclePolicyEIKOS = New ResvehiclePolicyEIKOS()
End Class
Public Class ResvehiclePolicyEIKOS
    Public idQuote As String
    Public car As RescarEIKOS = New RescarEIKOS()
    Public credit As RescreditEIKOS = New RescreditEIKOS()
    Public accessorySum As ResaccessorySumEIKOS = New ResaccessorySumEIKOS()
    Public driver As ResdriverEIKOS = New ResdriverEIKOS()
End Class
Public Class RescarEIKOS
    Public brand As ResbrandEIKOS = New ResbrandEIKOS()
    Public unitPrice As ResunitPriceEIKOS = New ResunitPriceEIKOS()
End Class
Public Class ResbrandEIKOS
End Class
Public Class ResunitPriceEIKOS
End Class
Public Class RescreditEIKOS
    Public validityPeriod As ResvalidityPeriodEIKOS = New ResvalidityPeriodEIKOS()
End Class
Public Class ResvalidityPeriodEIKOS
    Public startDate As String
    Public endDate As String
End Class
Public Class ResaccessorySumEIKOS
End Class
Public Class ResdriverEIKOS
    Public legalAddress As ReslegalAddressEIKOS = New ReslegalAddressEIKOS()
    Public extendedData As ResextendedDataEIKOS = New ResextendedDataEIKOS()
    Public nationality As ResnationalityEIKOS = New ResnationalityEIKOS()
End Class
Public Class ResnationalityEIKOS
End Class
Public Class ReslegalAddressEIKOS
End Class
Public Class ResextendedDataEIKOS
    Public countryOrigin As RescountryOriginEIKOS = New RescountryOriginEIKOS()
    Public homePhone As ReshomePhoneEIKOS = New ReshomePhoneEIKOS()
    Public mobilePhone As ResmobilePhoneEIKOS = New ResmobilePhoneEIKOS()
    Public fiscalSituation As ResfiscalSituationEIKOS = New ResfiscalSituationEIKOS()
    Public bussinessData As ResbussinessDataEIKOS = New ResbussinessDataEIKOS()
End Class
Public Class RescountryOriginEIKOS
End Class
Public Class ReshomePhoneEIKOS
End Class
Public Class ResmobilePhoneEIKOS
End Class
Public Class ResfiscalSituationEIKOS
End Class
Public Class ResbussinessDataEIKOS
End Class
Public Class RescomplementEIKOS
    Public user As ResuserEIKOS = New ResuserEIKOS
    Public errorInfo As ReserrorInfoEIKOS = New ReserrorInfoEIKOS()
End Class
Public Class ResuserEIKOS
    Public credentials As RescredentialsEIKOS = New RescredentialsEIKOS()
End Class
Public Class RescredentialsEIKOS
End Class
Public Class ReserrorInfoEIKOS
    Public errorId As String
    Public description As String
End Class
Public Class ResfeeEIKOS
End Class
Public Class RescontractorEIKOS
    Public legalAddress As ReslegalAddressEIKOS = New ReslegalAddressEIKOS()
    Public extendedData As ResextendedDataEIKOS = New ResextendedDataEIKOS()
    Public nationality As ResnationalityEIKOS = New ResnationalityEIKOS()
End Class
Public Class ResshippingCostsEIKOS
End Class
Public Class RestaxEIKOS
    Public tax As String
End Class
Public Class ResrealPremiumEIKOS
End Class
Public Class RestotalPremiumEIKOS
End Class
Public Class ReslateFeeEIKOS
End Class
Public Class ResinsuredAmountEIKOS
End Class
Public Class RescurrencyEIKOS
End Class
Public Class RescancelDataEIKOS
    Public [error] As ReserrorEIKOS = New ReserrorEIKOS()
End Class
Public Class ReserrorEIKOS
End Class
Public Class ResprintingDataEIKOS
End Class
#End Region

#Region "Payload Impresion Seguro EIKOS"
Public Class clsImpBrokerEIKOS
    Public policy As policyEIKOS2 = New policyEIKOS2
End Class
Public Class policyEIKOS2
    Public complement As complementEIKOS = New complementEIKOS
    Public agencyNumber As String
    Public policyId As String
End Class
#End Region

#Region "Respuesta Impresion Seguro EIKOS"
Public Class clsResImpBrokerEIKOS
    Public policy As policyEIKOS4 = New policyEIKOS4
End Class
Public Class policyEIKOS4
    Public urlPolicy As String
End Class
#End Region

#Region "Payload Cancelacion de Poliza EIKOS"
Public Class SolCanclPolEIKOS
    Public policy As New SolCanclpolicyEIKOS
End Class
Public Class SolCanclpolicyEIKOS
    Public complement As New SolCanclcomplementEIKOS
    Public policyId As String
    Public typeId As String
    Public insurerId As String
    Public cancelData As New SolCanclcancelDataEIKOS
End Class
Public Class SolCanclcomplementEIKOS
    Public user As New userEIKOS
End Class
Public Class SolCanclcancelDataEIKOS
    Public effectiveDate As String
    Public idCancelReason As String
End Class
#End Region

#Region "Payload Respuesta Cancelacion de poliza EIKOS"
Public Class ResCanclPolEIKOS
    Public policy As New ResCanclpolicyEIKOS
End Class
Public Class ResCanclpolicyEIKOS
    Public complement As New ResCanclcomplementEIKOS
    Public fee As New ReslateFeeEIKOS
    Public contractor As New ResCanclcontractorEIKOS
    Public validityPeriod As New validityPeriodEIKOS
    Public shippingCosts As New shippingCostsEIKOS
    Public tax As New taxEIKOS
    Public realPremium As New ResrealPremiumEIKOS
    Public totalPremium As New RestotalPremiumEIKOS
    Public lateFee As New ReslateFeeEIKOS
    Public insuredAmount As New ResinsuredAmountEIKOS
    Public currency As New RescurrencyEIKOS
    Public cancelData As New RescancelDataEIKOS
    Public printingData As New ResprintingDataEIKOS
End Class
Public Class ResCanclcomplementEIKOS
    Public user As New userEIKOS
    Public errorInfo As New ResCanclerrorInfoEIKOS
End Class
Public Class ResCanclcontractorEIKOS
    Public legalAddress As New legalAddressEIKOS
    Public extendedData As New ResCanclextendedDataEIKOS
    Public nationality As New nationalityEIKOS
End Class
Public Class ResCanclextendedDataEIKOS
    Public countryOrigin As New countryOriginEIKOS
    Public homePhone As New homePhoneEIKOS
    Public mobilePhone As New mobilePhoneEIKOS
    Public fiscalSituation As New fiscalSituationEIKOS
    Public bussinessData As New bussinessDataEIKOS
End Class
Public Class ResCanclerrorInfoEIKOS
    Public errorId As String
    Public description As String
End Class
Public Class bussinessDataEIKOS
End Class
Public Class nationalityEIKOS
End Class
Public Class shippingCostsEIKOS
End Class
Public Class taxEIKOS
    Public value As New valueEIKOS
End Class
Public Class valueEIKOS
End Class
#End Region