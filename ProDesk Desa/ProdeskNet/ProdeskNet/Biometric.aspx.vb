#Region "TRACKERS"
'BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
'BUG-PD-229 GVARGAS 09/10/2017 Captura Hand Position & FingerPrint WSQ
#End Region

Imports ProdeskNet.Criptografia.Entities
Imports System.Data
Imports ProdeskNet.BD
Imports ProdeskNet.Criptografia
Imports System.Web.Script.Serialization
Imports System.IO

Partial Class Biometric
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim _Result As String = String.Empty
        If Not Request("EncryptedData") Is Nothing And Not Request("publicKey") Is Nothing Then
            _Result = postBiometricData(Request("EncryptedData"), Request("publicKey"))
            Response.Clear()
            Response.ContentType = "text/plain"
            Using _streamWriter As New StreamWriter(Response.OutputStream, Encoding.UTF8)
                _streamWriter.Write(_Result)
            End Using
            Response.Flush()
            Response.End()
        End If
    End Sub

    Protected Function postBiometricData(ByVal EncryptedData As String, ByVal publicKey As String) As String
        Dim _result As String = "NO_DB_CONNECTION"
        Dim _FingerprintParams As FingerprintParams
        Dim _DecryptedString As String
        Dim _DataSet As New DataSet()
        Dim _clsManejaBD As New clsManejaBD()

        Try
            _DecryptedString = New Descifrado().DecryptString(EncryptedData, publicKey)

            _FingerprintParams = New JavaScriptSerializer().Deserialize(Of FingerprintParams)(_DecryptedString)
            _FingerprintParams.FingerprintImage = New Cifrado().EncryptString(_FingerprintParams.FingerprintImage, _FingerprintParams.Key)
            _FingerprintParams.FingerprintImageWSQ = New Cifrado().EncryptString(_FingerprintParams.FingerprintImageWSQ, _FingerprintParams.Key)

            Dim hand As Integer = Int32.Parse(_FingerprintParams.hand)

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
        Return _result
    End Function

End Class