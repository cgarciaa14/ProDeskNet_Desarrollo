'BUG-PD-07: AVH: 27/01/2017 Se crea ventana en Blanco para hacer la descarga de archivos
'BBV-P-423:RQ-INB215: AVEGA: 24/07/2017 SE AGREGA END al IMPRIMIR
'BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit

Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports CrystalDecisions.Shared
Imports ProdeskNet.Configurcion

Partial Class aspx_Descargapdf
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Fname As String
        
        Dim valor As String = HttpUtility.UrlDecode(Request("fname").ToString)

            Fname = valor

        Response.Clear()
        Response.ClearContent()
        Response.ClearHeaders()
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Fname)
        Response.ContentType = "application/pdf"
        Response.TransmitFile(Fname)
        Response.End()

        
    End Sub
End Class
