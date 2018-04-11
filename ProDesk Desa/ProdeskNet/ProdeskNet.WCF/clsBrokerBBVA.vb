'BBV-P-423 20/01/2017: JRHM RQCONYFOR-02: Se crean clases para payloads y respuestas de servicio de impresion de BBVA para tarea "Documentación de Pólizas de Seguros y Desembolso" 
'BUG-PD-81: RHERNANDEZ: 10/06/17: SE AGREGA EMISION DE SEGUROS DE VIDA Y DAÑOS BBVA
'BUG-PD-98: RHERNANDEZ: 20/06/17: SE MODIFICAN PAYLOAD PARA CANCELACION Y SERVICIOS DE BANCOMER EN GENERAL
'BUG-PD-116 RHERNANDEZ: 26/06/17: SE CARGAN CLASES PARA GENERAR PAYLOAD DE GURADADO DE DATOS DEL CUESTIONARIO DE SALUD
'BUG-PD-360: RHERNANDEZ: 19/02/18: SE AGREGA LA LECTURA DE IDQUOTE DE SEGURO DE VIDA PARA EMITIR SEGUROS INDEPENDIENTEMENTE
#Region "Cotizacion de seguro de daños"

#Region "Payload  de Envio para cotizar seguro de daños BBVA"
Public Class CotSegAutoBBVA
    Public header As headerBBVA = New headerBBVA
    Public quote As quoteBBVA = New quoteBBVA
    Public policy As policyBBVA = New policyBBVA
    Public credit As creditBBVA = New creditBBVA
    Public insuranceType As insuranceTypeBBVA = New insuranceTypeBBVA
    Public paymentWay As paymentWayBBVA = New paymentWayBBVA
    Public usageCar As usageCarBBVA = New usageCarBBVA
    Public serviceType As serviceTypeBBVA = New serviceTypeBBVA
    Public productPlan As productPlanBBVA = New productPlanBBVA
    Public circulationArea As circulationAreaBBVA = New circulationAreaBBVA
    Public vehicleFeatures As vehicleFeaturesBBVA = New vehicleFeaturesBBVA
    Public particularData As List(Of particularDataBBVA) = New List(Of particularDataBBVA)
    Public coverageKeys As List(Of coverageKeysBBVA) = New List(Of coverageKeysBBVA)
End Class

Public Class headerBBVA
    Public aapType As String = String.Empty
    Public dateRequest As String = String.Empty
    Public channel As String = String.Empty
    Public subChannel As String = String.Empty
    Public branchOffice As String = String.Empty
    Public managementUnit As String = String.Empty
    Public user As String = String.Empty
    Public idSession As String = String.Empty
    Public idRequest As String = String.Empty
    Public dateConsumerInvocation As String = String.Empty
End Class

Public Class quoteBBVA
    Public idQuote As String
End Class

Public Class policyBBVA
    Public validityPeriod As validityPeriodBBVA = New validityPeriodBBVA
    Public preferredBeneficiary As String = String.Empty
    Public rcUSAIndicator As String = String.Empty
    Public effectiveAdditionaldays As String = String.Empty
    Public invoiceValue As String = String.Empty
End Class

Public Class validityPeriodBBVA
    Public startDate As String = String.Empty
    Public endDate As String = String.Empty
End Class

Public Class creditBBVA
    Public creditPeriod As String = String.Empty
End Class

Public Class insuranceTypeBBVA
    Public catalogItemBase As catalogItemBaseBBVA = New catalogItemBaseBBVA
End Class

Public Class catalogItemBaseBBVA
    Public id As String
    Public name As String
End Class

Public Class usageCarBBVA
    Public catalogItemBase As catalogItemBaseBBVA = New catalogItemBaseBBVA
End Class

Public Class paymentWayBBVA
    Public catalogItemBase As catalogItemBaseBBVA = New catalogItemBaseBBVA
End Class

Public Class productPlanBBVA
    Public catalogItemBase As catalogItemBaseBBVA = New catalogItemBaseBBVA
    Public productCode As String = String.Empty
    Public planReview As String = String.Empty
    Public bouquetCode As String = String.Empty
    Public subPlan As String = String.Empty
End Class

Public Class circulationAreaBBVA
    Public catalogItemBase As catalogItemBaseBBVA = New catalogItemBaseBBVA
End Class

Public Class vehicleFeaturesBBVA
    Public carModel As carModelBBVA = New carModelBBVA
    Public identifierVehicleFeatures As identifierVehicleFeaturesBBVA = New identifierVehicleFeaturesBBVA
    Public originType As New originTypeBBVA
End Class

Public Class carModelBBVA
    Public catalogItemBase As catalogItemBaseBBVA = New catalogItemBaseBBVA
End Class

Public Class identifierVehicleFeaturesBBVA
    Public catalogItemBase As catalogItemBaseBBVA = New catalogItemBaseBBVA
End Class

Public Class originTypeBBVA
    Public catalogItemBase As catalogItemBaseBBVA = New catalogItemBaseBBVA
End Class

Public Class particularDataBBVA
    Public aliasCriterion As String = String.Empty
    Public transformer As transformerBBVA = New transformerBBVA
End Class

Public Class transformerBBVA
    Public catalogItemBase As catalogItemBaseBBVA = New catalogItemBaseBBVA
End Class

Public Class serviceTypeBBVA
    Public catalogItemBase As catalogItemBaseBBVA = New catalogItemBaseBBVA
End Class

Public Class coverageKeysBBVA
    Public catalogItemBase As catalogItemBaseBBVA = New catalogItemBaseBBVA
End Class
#End Region

#Region "Payload respuesta cotizacion seguro de daños BBVA"
Public Class msjerrBBVA
    Public errorId As String
    Public message As String
    Public status As String
End Class
Public Class jsonresponseBBVA
    Public quote As New quoteBBVA
    Public rate As List(Of rateBBVA)
    Public coverages As List(Of coveragesBBVA)
    Public rightPolicy As New rightPolicyBBVA
    Public rcUSARightPolicy As New rcUSARightPolicyBBVA
    Public rightPolicyLocalCurrency As New rightPolicyLocalCurrencyBBVA
    Public rcUSARightPolicyLocalCurrency As New rcUSARightPolicyLocalCurrencyBBVA
End Class
Public Class rateBBVA
    Public paymentWay As New RespaymentWayBBVA
    Public subsequentPaymentsNumber As String
    Public subplan As String
    Public totalPaymentWithTax As New totalPaymentWithTaxBBVA
    Public totalPaymentWithTaxLocalCurrency As New totalPaymentWithTaxLocalCurrencyBBVA
    Public totalPaymentWithoutTax As New totalPaymentWithoutTaxBBVA
    Public totalPaymentWithoutTaxLocalCurrency As New totalPaymentWithoutTaxLocalCurrencyBBVA
    Public firstPayment As New firstPaymentBBVA
    Public firstPaymentLocalCurrency As New firstPaymentLocalCurrencyBBVA
End Class
Public Class RespaymentWayBBVA
    Public id As String
    Public name As String
End Class
Public Class totalPaymentWithTaxBBVA
    Public currency As New currencyBBVA
    Public amount As String
End Class
Public Class currencyBBVA
    Public code As String
End Class
Public Class totalPaymentWithTaxLocalCurrencyBBVA
    Public currency As New currencyBBVA
    Public amount As String
End Class
Public Class totalPaymentWithoutTaxBBVA
    Public currency As New currencyBBVA
    Public amount As String
End Class
Public Class totalPaymentWithoutTaxLocalCurrencyBBVA
    Public currency As New currencyBBVA
    Public amount As String
End Class
Public Class firstPaymentBBVA
    Public currency As New currencyBBVA
    Public amount As String
End Class
Public Class firstPaymentLocalCurrencyBBVA
    Public currency As New currencyBBVA
    Public amount As String
End Class
Public Class coveragesBBVA
    Public catalogItemBase As New catalogItemBase
    Public insuredValue As String
    Public premium As New premiumBBVA
    Public premiumLocalCurrency As New premiumLocalCurrencyBBVA
    Public premiumWithoutTax As New premiumWithoutTaxBBVA
    Public premiumWithoutTaxLocalCurrency As New premiumWithoutTaxLocalCurrencyBBVA
End Class
Public Class premiumBBVA
    Public currency As New currencyBBVA
    Public amount As String
End Class
Public Class premiumLocalCurrencyBBVA
    Public currency As New currencyBBVA
    Public amount As String
End Class
Public Class premiumWithoutTaxBBVA
    Public currency As New currencyBBVA
End Class
Public Class premiumWithoutTaxLocalCurrencyBBVA
    Public currency As New currencyBBVA
End Class
Public Class rightPolicyBBVA
    Public currency As New currencyBBVA
    Public amount As String
End Class
Public Class rcUSARightPolicyBBVA
    Public currency As New currencyBBVA
End Class
Public Class rightPolicyLocalCurrencyBBVA
    Public currency As New currencyBBVA
    Public amount As String
End Class
Public Class rcUSARightPolicyLocalCurrencyBBVA
    Public currency As New currencyBBVA
End Class
#End Region

#End Region


#Region "Guardado de datos del cliente"

#Region "Payload para el guardado de datos del cliente para cotizar el seguro de vida"
Public Class SaveClientBBVA
    Public header As New CQheaderBBVA
    Public quote As New SCquoteBBVA
    Public holder As New SCholderBBVA
    Public clientType As New SCclientTypeBBVA
    Public contractingHolderIndicator As Boolean
End Class
Public Class SCheaderBBVA
    Public aapType As String
    Public dateRequest As String
    Public channel As String
    Public subChannel As String
    Public managementUnit As String
    Public branchOffice As String
    Public user As String
    Public idSession As String
    Public idRequest As String
    Public dateConsumerInvocation As String
End Class
Public Class SCquoteBBVA
    Public idQuote As String
End Class
Public Class SCholderBBVA
    Public email As String
    Public rfc As String
    Public activity As New SCactivityBBVA
    Public mainAddress As New SCmainAddressBBVA
    Public correspondenceAddress As New SCcorrespondenceAddressBBVA
    Public legalAddress As New SClegalAddressBBVA
    Public nacionality As New SCnacionalityBBVA
    Public mainContactData As New SCmainContactDataBBVA
    Public correspondenceContactData As New SCcorrespondenceContactDataBBVA
    Public fiscalContactData As New SCfiscalContactDataBBVA
    Public physicalPersonalityData As New SCphysicalPersonalityDataBBVA
    Public deliveryInformation As New SCdeliveryInformationBBVA
End Class
Public Class SCactivityBBVA
    Public catalogItemBase As New SCcatalogItemBaseBBVA
End Class
Public Class SCcatalogItemBaseBBVA
    Public id As String
    Public name As String
End Class
Public Class SCcatalogItemBaseBBVA2
    Public name As String
    Public id As String
End Class
Public Class SCmainAddressBBVA
    Public zipCode As String
    Public streetName As String
    Public outdoorNumber As String
    Public door As String
    Public suburb As New SCsuburbBBVA
End Class
Public Class SCsuburbBBVA
    Public neighborhood As New SCneighborhoodBBVA2
End Class
Public Class SCneighborhoodBBVA
    Public catalogItemBase As New SCcatalogItemBaseBBVA
End Class
Public Class SCneighborhoodBBVA2
    Public catalogItemBase As New SCcatalogItemBaseBBVA2
End Class
Public Class SCcorrespondenceAddressBBVA
    Public zipCode As String
    Public streetName As String
    Public outdoorNumber As String
    Public door As String
    Public suburb As New SCsuburbBBVA
End Class
Public Class SClegalAddressBBVA
    Public zipCode As String
    Public streetName As String
    Public outdoorNumber As String
    Public door As String
    Public suburb As New SCsuburbBBVA
End Class
Public Class SCnacionalityBBVA
    Public catalogItemBase As New SCcatalogItemBaseBBVA
End Class
Public Class SCmainContactDataBBVA
    Public cellphone As New SCcellphoneBBVA
    Public phone As New SCSCphoneBBVA
    Public officePhone As New SCofficePhoneBBVA
End Class
Public Class SCcellphoneBBVA
    Public phoneExtension As String
    Public telephoneNumber As String
End Class
Public Class SCSCphoneBBVA
    Public phoneExtension As String
    Public telephoneNumber As String
End Class
Public Class SCofficePhoneBBVA
    Public phoneExtension As String
    Public telephoneNumber As String
End Class
Public Class SCcorrespondenceContactDataBBVA
    Public cellphone As New SCcellphoneBBVA
    Public phone As New SCSCphoneBBVA
    Public officePhone As New SCofficePhoneBBVA
End Class
Public Class SCfiscalContactDataBBVA
    Public cellphone As New SCcellphoneBBVA
    Public phone As New SCSCphoneBBVA
    Public officePhone As New SCofficePhoneBBVA
End Class
Public Class SCphysicalPersonalityDataBBVA
    Public name As String
    Public lastName As String
    Public birthDate As String
    Public sex As New SCsexBBVA
    Public mothersLastName As String
    Public curp As String
    Public civilStatus As New SCcivilStatusBBVA
    Public occupation As New SCoccupationBBVA
    Public fiscalPersonality As New SCfiscalPersonalityBBVA
End Class
Public Class SCsexBBVA
    Public catalogItemBase As New SCcatalogItemBaseBBVA
End Class
Public Class SCcivilStatusBBVA
    Public catalogItemBase As New SCcatalogItemBaseBBVA
End Class
Public Class SCoccupationBBVA
    Public catalogItemBase As New SCcatalogItemBaseBBVA
End Class
Public Class SCfiscalPersonalityBBVA
    Public catalogItemBase As New SCcatalogItemBaseBBVA2
End Class
Public Class SCdeliveryInformationBBVA
    Public referenceStreets As String
    Public deliveryInstructions As String
    Public deliveryTimeStart As String
    Public deliveryTimeEnd As String
End Class
Public Class SCclientTypeBBVA
    Public catalogItemBase As New SCcatalogItemBaseBBVA
End Class

#End Region

#Region "Payload de respuesta para el guardado de datos de cliente"

#End Region

#End Region


#Region "Guardado de Vehiculo"

#Region "Payload para el guardado de datos del vehiculo para cotizar seguro de vida"

Public Class CreateCarDataBBVA
    Public header As New CQheaderBBVA
    Public quote As New SCquoteBBVA
    Public carData As New CDcarDataBBVA
End Class
Public Class CDcarDataBBVA
    Public car As New CDcarBBVA
    Public experienceYears As String
    Public credit As New CDcreditBBVA
End Class
Public Class CDcarBBVA
    Public serialNumber As String
    Public engineNumber As String
End Class

Public Class CDcreditBBVA
    Public creditNumber As String
End Class
#End Region

#Region "Payload de respuesta del guardado de datos del vehiculo para cotizacion del seguro de vida"

#End Region

#End Region


#Region "Cotizacion Seguro de Vida"

#Region "Payload para la cotizacion del seguro de vida BBVA"
Public Class getratelifequoteBBVA
    Public technicalInformation As technicalInformation = New technicalInformation
    Public productPlan As productPlan = New productPlan
    Public validityPeriod As validityPeriod = New validityPeriod
    Public rateQuote As New List(Of rateQuote)
    Public particularData As New List(Of particularData)
    Public insuredList As New List(Of insuredList)
    Public region As String = String.Empty
End Class
Public Class technicalInformation
    Public dateRequest As String = String.Empty
    Public technicalChannel As String = String.Empty
    Public technicalSubChannel As String = String.Empty
    Public branchOffice As String = String.Empty
    Public managementUnit As String = String.Empty
    Public user As String = String.Empty
    Public technicalIdSession As String = String.Empty
    Public idRequest As String = String.Empty
    Public dateConsumerInvocation As String = String.Empty
End Class
Public Class productPlan
    Public productCode As String = String.Empty
    Public planReview As String = String.Empty
    Public planCode As planCode = New planCode
    Public bouquetCode As String = String.Empty
End Class
Public Class planCode
    Public id As String = String.Empty
End Class
Public Class validityPeriod
    Public startDate As String = String.Empty
    Public endDate As String = String.Empty
    Public type As type = New type
End Class
Public Class type
    Public id As String = String.Empty
    Public name As String = String.Empty
End Class
Public Class rateQuote
    Public paymentWay As paymentWay = New paymentWay
End Class
Public Class paymentWay
    Public id As String = String.Empty
    Public name As String = String.Empty
End Class
Public Class particularData
    Public aliasCriterion As String = String.Empty
    Public transformer As transformer = New transformer
    Public peopleNumber As String = String.Empty
End Class
Public Class transformer
    Public id As String = String.Empty
    Public name As String = String.Empty
End Class
Public Class insuredList
    Public coverages As New List(Of coverages)
End Class
Public Class coverages
    Public catalogItemBase As catalogItemBases = New catalogItemBases
    Public peopleNumber As String = String.Empty
End Class
Public Class catalogItemBases
    Public id As String = String.Empty
End Class

Public Class msjerr
    Public message As String
    Public status As String
End Class

#End Region

#Region "Payload de respuesta de la cotizacion del seguro de vida"
Public Class clscreateQuoteRespuesta
    Public id As String = String.Empty
    Public insuredList As New List(Of insuredListRespuesta)
    Public rateQuote As New List(Of rateQuoteRespuesta)
End Class
Public Class insuredListRespuesta
    Public id As String = String.Empty
    Public holderIndicator As String = String.Empty
    Public relationshipCode As String = String.Empty
    Public coverages As New List(Of coveragesRespuesta)
End Class
Public Class coveragesRespuesta
    Public catalogItemBase As catalogItemBaseRespuesta = New catalogItemBaseRespuesta
    Public premium As premium = New premium
    Public premiumLocalCurrency As premiumLocalCurrency = New premiumLocalCurrency
    Public premiumWithoutTax As premiumWithoutTax = New premiumWithoutTax
End Class
Public Class catalogItemBaseRespuesta
    Public id As String = String.Empty
    Public name As String = String.Empty
End Class
Public Class premium
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class currency
    Public code As String = String.Empty
End Class
Public Class premiumLocalCurrency
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class premiumWithoutTax
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class rateQuoteRespuesta
    Public paymentWay As paymentWayRespuesta = New paymentWayRespuesta
    Public subsequentPaymentsNumber As String = String.Empty
    Public totalPaymentWithoutTax As totalPaymentWithoutTax = New totalPaymentWithoutTax
    Public totalPaymentWithoutTaxLocalCurrency As totalPaymentWithoutTaxLocalCurrency = New totalPaymentWithoutTaxLocalCurrency
    Public firstPayment As firstPayment = New firstPayment
    Public firstPaymentLocalCurrency As firstPaymentLocalCurrency = New firstPaymentLocalCurrency
    Public fractionalPaymentFee As fractionalPaymentFee = New fractionalPaymentFee
    Public fractionalPaymentFeeLocal As fractionalPaymentFeeLocal = New fractionalPaymentFeeLocal
    Public taxLocalCurrency As taxLocalCurrency = New taxLocalCurrency
    Public rightPolicy As rightPolicy = New rightPolicy
    Public rightPolicyLocalCurrency As rightPolicyLocalCurrency = New rightPolicyLocalCurrency
    Public subsequentPayment As subsequentPayment = New subsequentPayment
    Public subsequentPaymentLocal As subsequentPaymentLocal = New subsequentPaymentLocal
    Public paymentWithoutTax As paymentWithoutTax = New paymentWithoutTax
    Public paymentWithoutTaxLocal As paymentWithoutTaxLocal = New paymentWithoutTaxLocal
    Public relatedPolicyFee As relatedPolicyFee = New relatedPolicyFee
    Public relatedPolicyFeeLocal As relatedPolicyFeeLocal = New relatedPolicyFeeLocal
    Public discount As discount = New discount
End Class
Public Class paymentWayRespuesta
    Public id As String = String.Empty
    Public name As String = String.Empty
End Class
Public Class totalPaymentWithoutTax
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class totalPaymentWithoutTaxLocalCurrency
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class firstPayment
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class firstPaymentLocalCurrency
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class fractionalPaymentFee
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class fractionalPaymentFeeLocal
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class taxLocalCurrency
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class rightPolicy
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class rightPolicyLocalCurrency
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class subsequentPayment
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class subsequentPaymentLocal
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class paymentWithoutTax
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class paymentWithoutTaxLocal
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class relatedPolicyFee
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class relatedPolicyFeeLocal
    Public amount As String = String.Empty
    Public currency As currency = New currency
End Class
Public Class discount
    Public maximum As String = String.Empty
End Class

#End Region

#End Region


#Region "Formalizacion"

#Region "Payload de formalizacion de cotizaciones de daño y de vida"
Public Class createquoteBBVA
    Public header As New CQheaderBBVA
    Public quote As New SCquoteBBVA
End Class
Public Class CQheaderBBVA
    Public aapType As String
    Public dateRequest As String
    Public channel As String
    Public subChannel As String
    Public branchOffice As String
    Public managementUnit As String
    Public user As String
    Public idSession As String
    Public idRequest As String
    Public dateConsumerInvocation As String
End Class
#End Region

#Region "Respuesta de formalizacion de cotizaciones de vida y de daños"
Public Class rescreatequoteBBVA
    Public messageInfo As New resmessageInfoBBVA
End Class
#End Region

#End Region


#Region "Emision"

#Region "Payload de Emision de seguros BBVA (Daños y vida)"
Public Class createPolicy
    Public header As New CQheaderBBVA
    Public quote As New SCquoteBBVA
End Class
#End Region

#Region "Respuesta de payload de emision de seguros BBVA (Daños y Vida)"
Public Class rescreatePolicy
    Public mainPolicy As New resmainPolicyBBVA
    Public associatedPolicy As New resassociatedPolicyBBVA
    Public messageInfo As New resmessageInfoBBVA
End Class
Public Class resmainPolicyBBVA
    Public idProduct As String
    Public idPolicy As String
End Class
Public Class resassociatedPolicyBBVA
    Public idProduct As String
    Public idPolicy As String
End Class
Public Class resmessageInfoBBVA
    Public messageId As String
    Public message As String
    Public traceId As String
    Public extraInfo As String
End Class
#End Region

#End Region


#Region "Impresion"

#Region "Payload Servicio impresion seguro de vida BBVA"
Public Class clsBrokerBBVA
    Public header As header = New header
    Public quote As quote = New quote
End Class
Public Class header
    Public aapType As String
    Public dateRequest As String
    Public channel As String
    Public subChannel As String
    Public branchOffice As String
    Public managementUnit As String
    Public user As String
    Public idSession As String
    Public idRequest As String
    Public dateConsumerInvocation As String
End Class
Public Class quote
    Public idQuote As String
End Class
#End Region

#Region "Response Servicio impresion seguro de vida BBVA"
Public Class clsResImpBrokerBBVA
    Public urlPolicy As String
End Class
#End Region

#End Region


#Region "Cancelacion"

#Region "Payload de Cancelacion de las polizas de vida o daños"
Public Class cancelquotebbva
    Public header As New CQheaderBBVA
    Public quote As New SCquoteBBVA
    Public policyIndicatorLinked As Boolean
End Class
#End Region

#Region "Payload respuesta de cancelacion de poliza de vida y daños"
Public Class rescancelquotebbva
    Public message As String
    Public extraInfo As String
    Public trazaId As String
    Public errorId As String
End Class
#End Region

#End Region



#Region "Consultas de colonias por CP"
Public Class envioCol
    Public header As New CQheaderBBVA
    Public zipCode As String
End Class

Public Class resenvioCol
    Public iCatalogItem As New iCatalogItemBBVA
    Public messageInfo As New messageInfoBBVA
    Public catalog As New catalogBBVA
    Public pagination As New paginationBBVA
End Class
Public Class iCatalogItemBBVA
    Public suburb As List(Of suburbBBVA)
End Class
Public Class suburbBBVA
    Public neighborhood As New neighborhoodBBVA
    Public county As New countyBBVA
    Public city As New cityBBVA
    Public state As New stateBBVA
End Class
Public Class neighborhoodBBVA
    Public id As String
    Public name As String
End Class
Public Class countyBBVA
    Public id As String
    Public name As String
End Class
Public Class cityBBVA
    Public id As String
    Public name As String
End Class
Public Class stateBBVA
    Public id As String
    Public name As String
End Class
Public Class messageInfoBBVA
End Class
Public Class catalogBBVA
End Class
Public Class paginationBBVA
End Class
#End Region



#Region "Payload Consulta de polizas"
#Region "Payload de Consulta de polizas"
Public Class getpolicybbva
    Public header As New CQheaderBBVA
    Public quote As New quoteBBVA
End Class
#End Region
#Region "Payload de respuesta Consulta de polizas"
Public Class resgetpolicybbva
    Public quote As New resquoteBBVA
    Public mainPolicy As New resmainPolicyBBVA2
    Public associatedPolicy As New resassociatedPolicyBBVA2
    Public messageInfo As New resmessageInfoBBVA
End Class
Public Class resquoteBBVA
    Public idQuote As String
    Public status As String
    Public dateQuote As String
End Class
Public Class resmainPolicyBBVA2
    Public idProduct As String
    Public idPolicy As String
    Public validityPeriod As New resvalidityPeriodBBVA
    Public totalPremiumLocalCurrency As New restotalPremiumLocalCurrencyBBVA
    Public totalPremium As New restotalPremiumBBVA
End Class
Public Class resvalidityPeriodBBVA
    Public startDate As String
    Public endDate As String
End Class
Public Class restotalPremiumLocalCurrencyBBVA
    Public currency As New rescurrencyBBVA2
    Public amount As String
End Class
Public Class rescurrencyBBVA2
    Public code As String
End Class
Public Class restotalPremiumBBVA
    Public currency As New rescurrencyBBVA2
    Public amount As String
End Class
Public Class resassociatedPolicyBBVA2
    Public idProduct As String
    Public idPolicy As String
    Public validityPeriod As New resvalidityPeriodBBVA
    Public totalPremiumLocalCurrency As New restotalPremiumLocalCurrencyBBVA
    Public totalPremium As New restotalPremiumBBVA
End Class
#End Region
#End Region


#Region "Guardar Cuestionario de Salud"

#Region "Payload de Envio"
Public Class Cuestionario2
    Public header As headerWSQ2 = New headerWSQ2()
    Public quote As quoteQ2 = New quoteQ2()
    Public questionnaire As questionnaireQ2 = New questionnaireQ2()
End Class
Public Class headerWSQ2
    Public aapType As String
    Public dateRequest As String
    Public channel As String
    Public subChannel As String
    Public managementUnit As String
    Public branchOffice As String
    Public user As String
    Public idSession As String
    Public idRequest As String
    Public dateConsumerInvocation As String
End Class
Public Class quoteQ2
    Public idQuote As String
End Class
Public Class RespuestaQ2
    Public question As New List(Of questionQ2)
End Class
Public Class questionQ2
    Public catalogItemBase As catalogItemBaseQ2 = New catalogItemBaseQ2()
    Public type As String
    Public answers As New List(Of answersQ2)
End Class
Public Class questionRespuestaQ2
    Public catalogItemBase As catalogItemBaseQ2 = New catalogItemBaseQ2()
    Public answerQuestion As answerQuestionQ2 = New answerQuestionQ2()
End Class
Public Class catalogItemBaseQ2
    Public id As String
    Public name As String
End Class
Public Class answerQuestionQ2
    Public catalogItemBase As catalogItemBaseQ2 = New catalogItemBaseQ2()
End Class
Public Class answersQ2
    Public catalogItemBase As catalogItemBaseQ2 = New catalogItemBaseQ2()
End Class
Public Class msjerrQ2
    Public message As String
    Public status As String
End Class
Public Class questionnaireQ2
    Public question As New List(Of questionRespuestaQ2)
End Class
#End Region

#End Region


#Region "Valida cuestiomnario"
#Region "Payload"
Public Class Cuestionario
    Public header As headerWSQ2 = New headerWSQ2()
    Public quote As quoteQ2 = New quoteQ2()
End Class
#End Region
#End Region
