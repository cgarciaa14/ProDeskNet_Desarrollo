<%@ WebHandler Language="VB" Class="upload" %>

Imports System

Imports System.Web.Services
Imports System.IO
Imports System.Data
Imports ProdeskNet.WCF
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports ProdeskNet.SN
Imports System.IO.Compression


'BBVA-P-423:RQSOL-05 23/11/16 Se modifico parametro de archivos a "files[]" para la carga de documentos a el servidor
'BBVA-P-423:RQCONYFOR-05 JRHM 07/12/16 Se agrego llamado a servicio ingesta documentos
'BBV-P-423: RQ H - CSS JRHM 09/12/2016 Se modificaron clases de estilos para asemejarce a el cotizador
'BBVA-P-423:RQ I -WS ARCHIVING 14/12/16 JRHM Se agrego funcionalidad para regresar dato iddoc generado por servicio ingestadocumentos y version del nuevo documento
'BUG-PD-02: AVH 22/12/16: SE AGREGA IDDOC EN EL NOMBRE DE LA IMAGEN
'BBV-P-423 RQADM-36: AVH: 02/02/2017 Se modifica la carga de archivos
'BUG-PD-13  GVARGAS  01/03/2017 Cambio WCF Credenciales
'BUG-PD-25 JRHM 04/04/17 SE MODIFICO EXCEPCIONES DE CARGA DE ARCHIVOS
'BUG-PD-29 JRHM 10/04/17 SE MODIFICO MAXVALUE DEL SERIALIZE PARA ATRAPAR EL JSON DE INGESTA
'BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN
'BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS PARA ABRIR VISOR TELEPRO
'BUG-PD-299: RHERNANDEZ: 13/12/17: SE CAMBIA LA FORMA EN QUE SE ALMACENA EL ARCHIVO EN DE BASE64 A UN ARCHIVO GZ EN VARBINARY 
Public Class upload
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            Dim BD As New ProdeskNet.BD.clsManejaBD
            Dim postedFile As HttpPostedFile = context.Request.Files("files[]")
            Dim savepath As String = ""
            Dim tempPath As String = ""
            tempPath = System.Configuration.ConfigurationManager.AppSettings("FolderPath")
            savepath = context.Server.MapPath(tempPath)

            'Dim objDoc As New ProdeskNet.consultaPantallaDocumentos
            If postedFile Is Nothing Then
                postedFile = context.Request.Files(0)
            End If
            Dim arreglo As String() = postedFile.FileName.Split(".")

            Dim count As Integer = arreglo.Length
            Dim idsol As String = context.Session("idsol")
            Dim request = context.Request
            Dim reqheader() As String = request.Headers("iddoc").ToString().Replace("yourID", "").Split("_")
            Dim iddoc As String = reqheader(0)
            Dim relpandoc As String = reqheader(1)
            Dim ds As DataSet
            ds = BD.EjecutarQuery("select PDK_VERSION from PDK_REL_VER_DOC_SOL where PDK_ID_SECCERO = " & idsol & " AND PDK_ID_DOCUMENTOS =" & iddoc & ";")
            Dim version As Integer
            If (ds.Tables(0).Rows.Count = 0) Then
                version = 1
            Else
                version = CInt(ds.Tables(0).Rows(0).Item("PDK_VERSION").ToString()) + 1
            End If
        

            Dim nombre As String = ""
        
            For i = 1 To count Step 1
                If i <> count Then
                    nombre = nombre & arreglo(i - 1).ToString
                Else
                    nombre = nombre + "_" + idsol.ToString + "_" + iddoc.ToString + "." + arreglo(i - 1).ToString
                End If
            Next
        
            'Dim nombre As String = arreglo(0).ToString + "_" + idsol.ToString + "." + arreglo(1).ToString
        

            Dim filename As String = nombre
            Dim nombrecompleto As String = (savepath & "\uploads\") + filename
            'Dim filename As String = postedFile.FileName
            Dim returnvalue As String = ""
            postedFile.SaveAs((savepath & "\uploads\") + filename)
            context.Response.Write((tempPath & "/uploads/") + filename)
            
            Dim encodefile As String
            Using binaryfile As FileStream = New FileStream(nombrecompleto, FileMode.Open)
                Dim binread As BinaryReader = New BinaryReader(binaryfile)
                Dim binbytes As Byte() = binread.ReadBytes(CInt(binaryfile.Length))
                returnvalue = Convert.ToBase64String(binbytes)
                binaryfile.Close()
            End Using
            
            Dim savepathzip As String = ""
            Dim tempPathzip As String = ""
            tempPathzip = System.Configuration.ConfigurationManager.AppSettings("FolderPath")
            savepathzip = context.Server.MapPath(tempPathzip)
            savepathzip = savepathzip + "\uploads\" + nombre.ToString.Replace(".pdf", "") & ".gz"
            Using originalFileStream As FileStream = New FileStream(nombrecompleto, FileMode.Open, FileAccess.ReadWrite)
                Using compressedFileStream As FileStream = File.Create(savepathzip)
                    Using compressionStream As New GZipStream(compressedFileStream, CompressionMode.Compress)
                        originalFileStream.CopyTo(compressionStream)
                    End Using
                End Using
            End Using

            Dim files As Byte()
            Using stream = New FileStream(savepathzip, FileMode.Open, FileAccess.Read)
                Using reader = New BinaryReader(stream)
                    files = reader.ReadBytes(CInt(stream.Length))
                End Using
            End Using

            If File.Exists(savepathzip) Then
                File.Delete(savepathzip)
            End If
       
            
            ds = New DataSet
            ds = BD.EjecutarQuery("SELECT PDK_PAR_SIS_STATUS FROM  PDK_PARAMETROS_SISTEMA WHERE PDK_ID_PARAMETROS_SISTEMA=228")
            If (ds.Tables(0).Rows.Count < 1) Then
                Throw New Exception("Falta cargar el parametro sistema para validacion de servicios BBVA")
            ElseIf (ds.Tables(0).Rows(0).Item("PDK_PAR_SIS_STATUS") = "2") Then
                Using binaryfile As FileStream = New FileStream(nombrecompleto, FileMode.Open)
                    Using sha1 As New System.Security.Cryptography.SHA1CryptoServiceProvider()
                        sha1.ComputeHash(binaryfile)
                        encodefile = BitConverter.ToString(sha1.Hash).Replace("-", "").ToLower()
                    End Using
                    binaryfile.Close()
                End Using
                If File.Exists(nombrecompleto) Then
                    File.Delete(nombrecompleto)
                End If
                Dim information As System.IO.FileInfo
                information = My.Computer.FileSystem.GetFileInfo(nombrecompleto)
                Dim extension As String = information.Extension.ToString().Replace(".", "")
                Dim ingestaarchivos As IngestaDocumentos = New IngestaDocumentos()
                ingestaarchivos.repositoryName = "finauto"
                Dim documentfilelist As documentFiles = New documentFiles()
                documentfilelist.name = filename.ToString()
                documentfilelist.size = 1
                documentfilelist.extension = extension.ToString()
                documentfilelist.encodeData = returnvalue
                documentfilelist.extendedData.mapMetadata.f = DateTime.Now().Date.ToString("dd/MM/yyyy")
                documentfilelist.extendedData.mapMetadata.fo = CInt(idsol)
                documentfilelist.extendedData.mapMetadata.no = CInt(iddoc)
                documentfilelist.extendedData.mapMetadata.v = CInt(version)
                documentfilelist.extendedData.sha1N = encodefile
                ingestaarchivos.documentFiles.Add(documentfilelist)
                
               
                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                serializer.MaxJsonLength = Int32.MaxValue
                Dim jsonBODY As String = serializer.Serialize(ingestaarchivos)
        
                Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        
                Dim restful As RESTful = New RESTful()
                restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriIngestadocu").ToString()
                'restful.consumerID = "10000004"
                
                Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonBODY)
                Dim errormessage As String
                If (restful.IsError) Then
                    errormessage = restful.MensajeError
                    Throw New Exception(errormessage)
                Else
                    Dim res As Respuesta = serializer.Deserialize(Of Respuesta)(respuesta)
                    Dim foliodocservice As String = res.documentFiles(0).extendedData.mapMetadata._s3_key
                    context.Response.ContentType = "text/plain"
                    context.Response.Clear()
                    Dim clsseg As New clsSeguros
                    clsseg._ID_SOLICITUD = idsol
                    clsseg._ID_DOC = iddoc
                    clsseg._ID_RELPANDOC = relpandoc
                    clsseg._NOMDOC = foliodocservice
                    clsseg._VER = version
                    clsseg._NOMBRE_ARCHIVO = filename
                    clsseg._DATOS_ARCHIVO = files
                    If clsseg.InsertaDoc() Then
                        context.Response.Write(foliodocservice.ToString() + "|" + version.ToString() + "|" + filename)
                    Else
                        Throw New Exception(clsseg.StrError)
                    End If
                End If
            Else
                Dim clsseg As New clsSeguros
                clsseg._ID_SOLICITUD = idsol
                clsseg._ID_DOC = iddoc
                clsseg._ID_RELPANDOC = relpandoc
                clsseg._NOMDOC = 0
                clsseg._VER = 1
                clsseg._NOMBRE_ARCHIVO = filename
                clsseg._DATOS_ARCHIVO = files
                If clsseg.InsertaDoc() Then
                context.Response.Write("0|1|" + filename + "|" + returnvalue)
                Else
                    Throw New Exception(clsseg.StrError)
                End If
            End If
            
        Catch ex As Exception
            Dim errors As New MsgError
            errors.Message = "Falla Servicio: " + ex.Message
            Dim json As String = JsonConvert.SerializeObject(errors, Formatting.Indented)
            context.Response.Clear()
            context.Response.StatusCode = 500
            context.Response.Write(json)
        End Try
    End Sub
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
End Class
#Region "Ingesta_Documentos"
Public Class IngestaDocumentos
    Public repositoryName As String
    Public documentFiles As New List(Of documentFiles)
End Class
Public Class documentFiles
    Public name As String
    Public size As Integer
    Public extension As String
    Public encodeData As String
    Public extendedData As extendedData = New extendedData()

End Class
Public Class extendedData
    Public mapMetadata As New mapMetadata()
    Public sha1N As String
End Class
Public Class mapMetadata
    Public f As String
    Public fo As Integer
    Public no As Integer
    Public v As Integer
End Class
Public Class Respuesta
    Public documentFiles As New List(Of documentFilesres)
End Class
Public Class documentFilesres
    Public extendedData As extendedDatares = New extendedDatares()
End Class
Public Class extendedDatares
    Public mapMetadata As mapMetadatares = New mapMetadatares()
End Class
Public Class mapMetadatares
    Public _s3_key As String
End Class
Public Class MsgError
    Public Message As String
End Class


#End Region

    

