#Region "TRACKERS"
'BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
#End Region

Imports System.IO
Imports ProdeskNet.Criptografia.Entities
Imports System.Web.Script.Serialization
Imports ProdeskNet.Criptografia

Partial Class aspx_ManageFingerprintFile
    Inherits System.Web.UI.Page

    Public Property FingerprintParams() As FingerprintParams
        Get
            Return CType(Session("_FingerprintParams"), FingerprintParams)
        End Get
        Set(ByVal value As FingerprintParams)
            Session("_FingerprintParams") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (Request("Input") Is Nothing) Then
            GenerateFile()
        Else

        End If
    End Sub

    Private Sub GenerateFile()
        Dim _fileContent As String = New JavaScriptSerializer().Serialize(FingerprintParams)
        _fileContent = New Cifrado().EncryptString(_fileContent, FingerprintParams.Key)
        Response.ContentType = "text/plain"
        Response.AddHeader("content-disposition", "attachment;filename=" & FingerprintParams.Key & ".fng")
        Response.Clear()
        Using _streamWriter As New StreamWriter(Response.OutputStream, Encoding.UTF8)
            _streamWriter.Write(_fileContent)
        End Using
        Response.Flush()
        Response.End()
    End Sub


End Class
