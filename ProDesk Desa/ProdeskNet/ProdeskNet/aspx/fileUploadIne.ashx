<%@ WebHandler Language="VB" Class="fileUploadIne" %>

'BBV-P-423 RQ-PD-17 1 GVARGAS 22/12/2017 Mejoras carga huella
'BBV-P-423 RQ-PD-17 2 GVARGAS 02/01/2018 Mejoras carga huella

Imports System
Imports System.Web
Imports System.IO
Imports ProdeskNet.Criptografia
Imports ProdeskNet.Criptografia.Entities
Imports System.Web.Script.Serialization
Imports System.Data
Imports ProdeskNet.BD

Public Class fileUploadIne : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            If (context.Request.QueryString("upload") <> Nothing) Then
                Dim postedFile = context.Request.Files(0)
                Dim fileName As String = postedFile.FileName
                Dim reader As StreamReader = New StreamReader(postedFile.InputStream)
                Dim file As String = reader.ReadLine
                
                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                Dim responseFinger As responseFinger = serializer.Deserialize(Of responseFinger)(file)
                
                Dim _DecryptedString As String = New Descifrado().DecryptString(responseFinger.EncryptedData, responseFinger.publicKey)
                                
                Dim _FingerprintParams As FingerprintParams
               
                _FingerprintParams = New JavaScriptSerializer().Deserialize(Of FingerprintParams)(_DecryptedString)
                _FingerprintParams.FingerprintImage = New Cifrado().EncryptString(_FingerprintParams.FingerprintImage, _FingerprintParams.Key)
                _FingerprintParams.FingerprintImageWSQ = New Cifrado().EncryptString(_FingerprintParams.FingerprintImageWSQ, _FingerprintParams.Key)

                Dim hand As Integer = Int32.Parse(_FingerprintParams.hand)
                
                Dim _result As String = "NO_DB_CONNECTION"
                Dim _DataSet As New DataSet()
                Dim _clsManejaBD As New clsManejaBD()

                Try
                    _clsManejaBD.AgregaParametro("@PDK_ID_SECC_CERO", TipoDato.Entero, _FingerprintParams.NoSolicitud, False)
                    _clsManejaBD.AgregaParametro("@PDK_ID_PANTALLA", TipoDato.Entero, _FingerprintParams.IdPantalla, False)
                    _clsManejaBD.AgregaParametro("@PDK_ID_HISTORY_AUTHENTICATION", TipoDato.Entero, _FingerprintParams.IdAutenticacion, False)
                    _clsManejaBD.AgregaParametro("@PDK_FINGERPRINT", TipoDato.Cadena, _FingerprintParams.FingerprintImage, False)
                    _clsManejaBD.AgregaParametro("@PDK_GUID_KEY", TipoDato.Cadena, _FingerprintParams.Key, False)
                    _clsManejaBD.AgregaParametro("@PDK_FINGERPRINT_WSQ", TipoDato.Cadena, _FingerprintParams.FingerprintImageWSQ, False)
                    _clsManejaBD.AgregaParametro("@PDK_FINGERPRINT_POSITION", TipoDato.Entero, hand, False)
                    
                    _DataSet = _clsManejaBD.EjecutaStoredProcedure("spManejaDatosHuella")

                    If Not _DataSet Is Nothing And _DataSet.Tables.Count > 0 And _DataSet.Tables(0).Rows.Count > 0 Then
                        _result = _DataSet.Tables(0).Rows(0)("MENSAJE").ToString()
                    End If
                Catch ex As Exception
                    _result = ex.Message
                End Try              

            End If
        Catch ex As Exception

        End Try
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    
    Public Class responseFinger
        Public publicKey As String 
        Public EncryptedData As String 
    End Class
End Class