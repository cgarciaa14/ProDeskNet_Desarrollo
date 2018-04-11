
Imports System
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data


Public Class aspx_consultaReportesPDF
    Inherits System.Web.UI.Page

    Public objTable As New WebControls.Table
    Public dsData As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim dsDataset As New DataSet
        Dim strReporte As String = String.Empty
        Dim strTipoReporte As String = String.Empty


        dsDataset = Session("DataReporte")
        strReporte = Request.QueryString("NomReporte").ToString.Trim
        strTipoReporte = Request.QueryString("TipoReporte").ToString.Trim

        If dsDataset Is Nothing Then Exit Sub
        If strReporte.Trim.Length = 0 Then Exit Sub

        If strTipoReporte = "PDF" Then
            GeneraReportePDF(dsDataset, strReporte)
        Else
            GeneraReporteExcel(dsDataset, strReporte)

        End If

    End Sub

    Private Sub GeneraReportePDF(ByVal dsDataSet As DataSet, ByVal strNombreReporte As String)

        Dim doc As Document = New Document(PageSize.A4, 0, 0, 10, 10)
        Dim intTablas As Integer = 0
        Dim i As Integer = 0

        Dim intRegistros As Integer = 0
        Dim intColumnas As Integer = 0

        Dim j As Integer = 0
        Dim k As Integer = 0

        Dim table As Table
        Dim cell As Cell



        Dim titleFont = FontFactory.GetFont("Arial", 9, Font.BOLD)
        Dim subTitleFont = FontFactory.GetFont("Arial", 9, Font.BOLD)
        Dim boldTableFont = FontFactory.GetFont("Arial", 9, Font.BOLD)
        Dim endingMessageFont = FontFactory.GetFont("Arial", 9, Font.BOLDITALIC)
        Dim bodyFont = FontFactory.GetFont("Arial", 6, Font.NORMAL)

        Try

            PdfWriter.GetInstance(doc, New FileStream(Request.PhysicalApplicationPath + "\" & Replace(strNombreReporte.Trim, "", "_") & ".pdf", FileMode.Create))
            doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate())
            doc.Open()

            intTablas = dsDataSet.Tables.Count



            For i = 0 To intTablas - 1

                intColumnas = dsDataSet.Tables(i).Columns.Count

                'Se agrega la primera tabla 
                table = New Table(intColumnas)
                table.BorderWidth = 1
                'table.BorderColor = New Color(0, 0, 255)
                table.Padding = 1
                table.Spacing = 1


                If i = 0 Then
                    cell = New Cell("Reporte :" & strNombreReporte & "        Fecha:" & Format(Now(), "dd-MM-yyyy"))
                Else
                    cell = New Cell(" ")
                End If
                cell.Header = True
                cell.HorizontalAlignment = cell.ALIGN_CENTER
                cell.Colspan = intColumnas
                'cell.BorderColor = New Color(255, 0, 0)
                table.AddCell(cell)


                For k = 0 To intColumnas - 1

                    cell = New Cell(New Phrase(dsDataSet.Tables(i).Columns(k).ColumnName, subTitleFont))
                    cell.BackgroundColor = New Color(192, 192, 192)
                    table.AddCell(cell)
                Next


                intRegistros = dsDataSet.Tables(i).Rows.Count
                For j = 0 To intRegistros - 1

                    For k = 0 To intColumnas - 1

                        cell = New Cell(New Phrase("" & dsDataSet.Tables(i).Rows(j).Item(k).ToString & "", bodyFont))

                        If j Mod 2 = 0 Then
                            cell.BackgroundColor = Color.WHITE
                        Else
                            cell.BackgroundColor = Color.LIGHT_GRAY
                        End If
                        table.AddCell(cell)

                    Next

                Next

                doc.Add(table)
            Next

            doc.Close()
            Response.Redirect("~/" & Replace(strNombreReporte.Trim, "", "_") & ".pdf")
        Catch ex As Exception
        End Try

    End Sub



    Private Sub GeneraReporteExcel(ByVal dsDataset As DataSet, ByVal strReporte As String)

        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form As New HtmlForm

        Try
            ArmaTabla(dsDataset)

            objTable.EnableViewState = False
            pagina.EnableEventValidation = False
            pagina.DesignerInitialize()
            pagina.Controls.Add(form)
            form.Controls.Add(objTable)
            pagina.RenderControl(htw)

            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment;filename=" & strReporte.Trim & ".xls")
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.Default

            'Response.Write(sb.ToString())
            Response.Output.Write(sb.ToString())

            Response.Flush()
            Response.End()

        Catch ex As Exception
        End Try

    End Sub


    Private Sub ArmaTabla(ByVal dsDataSet As DataSet)

        Dim intTablas As Integer = 0
        Dim objTableRow As New TableRow
        Dim objTableCell As New TableCell
        Dim objGrid As New GridView
        Dim i As Integer = 0

        Dim objTable1 As New System.Web.UI.WebControls.Table
        Dim objGenerales As New clsGenerales


        Try
            Table.Rows.Clear()
            If dsDataSet Is Nothing Then Exit Sub
            intTablas = dsDataSet.Tables.Count
            If intTablas > 0 Then
                objGrid.Width = Unit.Pixel(900)
                For i = 0 To intTablas - 1

                    objGrid = New GridView
                    objGrid.AutoGenerateColumns = False
                    objGenerales.GridView_Format_Table(dsDataSet.Tables(i), objGrid)
                    objGrid.DataSource = dsDataSet.Tables(i)
                    objGrid.DataBind()

                    objTableCell = New TableCell
                    objTableRow = New TableRow
                    objTableCell.Controls.Add(objGrid)
                    objTableRow.Cells.Add(objTableCell)
                    Table.Controls.Add(objTableRow)
                Next
            End If
            objTable = Table

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub
End Class