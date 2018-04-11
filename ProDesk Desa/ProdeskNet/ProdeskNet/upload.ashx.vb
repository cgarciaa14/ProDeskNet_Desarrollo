'BBVA-P-423:RQSOL-05 23/11/16 Se modifico parametro de archivos a "files[]" para la carga de documentos a el servidor
Imports System.Web
Imports System.Web.Services

Public Class upload
    Implements System.Web.IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim postedFile As HttpPostedFile = context.Request.Files("files[]")

        Dim savepath As String = ""
        Dim tempPath As String = ""
        tempPath = System.Configuration.ConfigurationManager.AppSettings("FolderPath")
        savepath = context.Server.MapPath(tempPath)
        Dim filename As String = postedFile.FileName


        postedFile.SaveAs((savepath & "\uploads\") + filename)
        context.Response.Write((tempPath & "/uploads/") + filename)
        context.Response.StatusCode = 200
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property


End Class