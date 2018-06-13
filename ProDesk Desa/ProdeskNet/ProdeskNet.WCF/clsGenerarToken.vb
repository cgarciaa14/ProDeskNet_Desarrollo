'RQ-PD34: JMENDIETA: 03/05/2018: Se genera la clase que consumira el servicio para generar un TOKEN para poder visualizar los documento.

Public Class clsGenerarToken

    Private _strError As String
    Public Property StrError() As String
        Get
            Return _strError
        End Get
        Set(ByVal value As String)
            _strError = value
        End Set
    End Property


    Public Class RequestToken
        Public aplicationName As String = String.Empty
        Public listReferenceNumber As New List(Of String)
        Public features As New List(Of features)
    End Class

    Public Class features
        Public id As String = String.Empty
        Public name As String = String.Empty
    End Class

    Public Class ResponseToken
        Public listDocuments As New List(Of listDocuments)
    End Class

    Public Class listDocuments
        Public document As New document
        Public href As String
    End Class

    Public Class document
        Public data As String
        Public extendedData As New extendedData
    End Class

    Public Class extendedData
        Public errorStorage As New errorStorage
    End Class

    Public Class errorStorage
        Public errorCode As String = String.Empty
        Public errorMessage As String = String.Empty
    End Class

    Public Function ObtenerToken() As String

        Dim token As String = String.Empty
        Try
            Dim jsonRequest As New RequestToken
            Dim features As New features

            jsonRequest.aplicationName = "token"

            features.id = "usuario"
            features.name = "FA"
            jsonRequest.features.Add(features)
            jsonRequest.listReferenceNumber.Add("0")


            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonbody As String = serializer.Serialize(jsonRequest)
            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("TokenDocument").ToString()

            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)


            If (restful.IsError) Then
                Throw New Exception(restful.MensajeError)
            Else
                Dim res As ResponseToken = serializer.Deserialize(Of ResponseToken)(respuesta)

                If (Not IsNothing(res.listDocuments)) AndAlso res.listDocuments.Count > 0 Then
                    If Not (String.IsNullOrEmpty(res.listDocuments(0).href)) Then
                        token = res.listDocuments(0).href.ToString()
                    Else
                        Throw New Exception(res.listDocuments(0).document.extendedData.errorStorage.errorMessage)
                    End If
                Else
                    Throw New Exception("ERROR AL GENERAR TOKEN")
                End If

            End If

        Catch ex As Exception
            _strError = ex.Message
        End Try

        Return token

    End Function

End Class
