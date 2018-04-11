'RQADM-20: RHERNANDEZ: 19/05/17: SE CREA CLASE PARA LA CREACION  DE PAYLOADS DE LOS SERVICIOS DE CREACION Y CONTESTACION DE CUESTIONARIOS DE AUTENTICACION E IVR

#Region "Payload Solicitud de Cuestionario"
Public Class ClsCuestionarioAutent
    Public idApp As String
    Public productType As String
    Public numberReference As String
    Public regionalCenter As New regionalCenter
    Public customer As New customer
    Public isForward As String
End Class
Public Class regionalCenter
    Public code As String
End Class
Public Class customer
    Public person As New person
End Class
Public Class person
    Public id As String
    Public extendedData As New extendedData
End Class
Public Class extendedData
    Public rfc As String
End Class
#End Region

#Region "Payload Respuesta Solicitd de Cuestionario"
Public Class ClsResCuestionarioAutent
    Public customer As New customer
    Public numberReference As String
    Public questionnarie As New questionnarie
End Class
Public Class questionnarie
    Public numberQuestion As String
    Public questions As List(Of questions)
End Class
Public Class questions
    Public catalogItemBase As New catalogItemBase
    Public typeAnswer As New typeAnswer
    Public answers As List(Of answers)
    Public help As String
    Public numberOfReplies As String
    Public correctAnswer As String
End Class
Public Class catalogItemBase
    Public id As String
    Public name As String
End Class
Public Class typeAnswer
    Public id As String
End Class
Public Class answers
    Public catalogItemBase As New catalogItemBase
End Class
#End Region

#Region "Payload Envio de Cuestionario de Autenticacion"
Public Class EnvioRespuestaAutent
    Public idApp As String
    Public productType As String
    Public numberReference As String
    Public regionalCenter As New regionalCenter
    Public customer As New customer
    Public isForward As String
    Public questionnarie As New questionnarieanswer
End Class
Public Class questionnarieanswer
    Public numberOfRepliesCustomer As String
    Public questions As New List(Of questionsanswers)
End Class
Public Class questionsanswers
    Public answerCustomer As String
End Class

#End Region

#Region "Payload Res Envio de Respuesta de Autenticacion"
Public Class ResEnvioRespuestaAutent
    Public customer As New customer
    Public numberReference As String
    Public questionnarie As New resquestionnarie
End Class
Public Class resquestionnarie
    Public numberQuestion As String
    Public message As String
End Class
#End Region

