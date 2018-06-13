
'BUG-PD-199: RHERNANDEZ: 24/08/17 SE CREA PANTALLA PARA DE VISOR TELEPRO PARA VER LOS DOCUMENTOS DE PDK_REL_PAN_DOC_SOL 
'BUG-PD-299: RHERNANDEZ: 13/12/17: SE CAMBIA LA FORMA EN QUE SE ALMACENA EL ARCHIVO EN DE BASE64 A UN ARCHIVO GZ EN VARBINARY 
'BUG-PD-418: RHERNANDEZ: 12/04/18: SE ELIMINA FUNCIONALIDAD DE CERRAR LA PESTANIA AL ABRIR DOCUMENTOS SIN DATOS BINARIOS
Imports ProdeskNet.SN
Imports System.Data
Imports System.IO
Imports System.IO.Compression

Partial Class aspx_VisorTele
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Try
                Dim savepath As String = ""
                Dim tempPath As String = ""
                Dim iddocsol As String = Request("folio")
                Dim bytes As Byte()
                Dim fileName As String
                Dim clsseguros = New clsSeguros
                clsseguros._ID_DOC_SOL = iddocsol
                Dim dat As New DataSet
                dat = clsseguros.GetDatosArchivo()
                If clsseguros.StrError <> "" Then
                    Throw New Exception(clsseguros.StrError)
                End If
                If dat.Tables(0).Rows(0).Item("ARCHIVO_BINARIO").ToString() = "" Then
                    Throw New Exception("No existe informacion del archivo")
                End If
                bytes = DirectCast(dat.Tables(0).Rows(0).Item("ARCHIVO_BINARIO"), Byte()).ToArray()
                fileName = dat.Tables(0).Rows(0).Item("NOMBRE_ARCHIVO").ToString

                tempPath = System.Configuration.ConfigurationManager.AppSettings("FolderPath")
                savepath = Context.Server.MapPath(tempPath)
                savepath = savepath + "\temp\" + fileName.ToString.Replace(".pdf", "") & ".gz"

                File.WriteAllBytes(savepath, bytes)
                Dim savepathpdf As String = ""
                Dim tempPathpdf As String = ""
                tempPathpdf = System.Configuration.ConfigurationManager.AppSettings("FolderPath")
                savepathpdf = Context.Server.MapPath(tempPathpdf)
                savepathpdf = savepathpdf + "\temp\" + fileName

                Using originalFileStream As FileStream = New FileStream(savepath, FileMode.Open, FileAccess.ReadWrite)
                    Dim currentFileName As String = fileName.ToString.Replace(".pdf", "") & ".gz"

                    Using decompressedFileStream As FileStream = File.Create(savepathpdf)
                        Using decompressionStream As GZipStream = New GZipStream(originalFileStream, CompressionMode.Decompress)
                            decompressionStream.CopyTo(decompressedFileStream)
                        End Using
                    End Using
                End Using

                Dim files As Byte()
                Using stream = New FileStream(savepathpdf, FileMode.Open, FileAccess.Read)
                    Using reader = New BinaryReader(stream)
                        files = reader.ReadBytes(CInt(stream.Length))
                    End Using
                End Using
                If File.Exists(savepath) Then
                    File.Delete(savepath)
                End If
                If File.Exists(savepathpdf) Then
                    File.Delete(savepathpdf)
                End If

                Context.Response.Buffer = True
                Context.Response.Charset = ""
                If Context.Request.QueryString("download") = "1" Then
                    Context.Response.AppendHeader("Content-Disposition", Convert.ToString("attachment; filename=") & fileName)
                End If
                Context.Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Context.Response.ContentType = "application/pdf"
                Context.Response.BinaryWrite(files)
                Context.Response.Flush()
            Catch ex As Exception
                Response.Write("<script>alert('" + ex.Message + "');</script>")
            End Try
        End If
    End Sub
End Class
