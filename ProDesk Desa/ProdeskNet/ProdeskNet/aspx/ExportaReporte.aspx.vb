#Region "TRACKERS"
'BBV-P-423-RQ-INB214:MPUESTO:18/07/2017:Reporte de Proceso de Admision
'BBV-P-423-RQ-INB213:MPUESTO:25/07/2017:Reporte de Aspectos Especiales - Mejoras en codificacion de archivo
#End Region

Imports System.IO

Partial Class aspx_ExportaReporte
    Inherits System.Web.UI.Page

    Public Property ReportResult() As String
        Get
            Return CType(Session("_ReportResult"), String)
        End Get
        Set(ByVal value As String)
            Session("_ReportResult") = value
        End Set
    End Property

    Public Property ReportName() As String
        Get
            Return CType(Session("_ReportName"), String)
        End Get
        Set(ByVal value As String)
            Session("_ReportName") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        GenerateFile()
    End Sub

    Private Sub GenerateFile()
        Response.Clear()
        Response.AddHeader("Content-Disposition", "attachment;filename=" + ReportName + ".xls")
        Response.ContentType = "application/x-msexcel"
        Response.ContentEncoding = Encoding.UTF8
        Response.BinaryWrite(Encoding.UTF8.GetPreamble())
        Response.Write(ReportResult)
        Response.End()
    End Sub



End Class
