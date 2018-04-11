
Public Class clsGenerales


    Public Sub GridView_Format(ByVal ds As DataSet, ByRef gridView As GridView)

        Dim dc As DataColumn
        Dim intColumnas As Integer = 0

        If ds.Tables.Count > 0 Then
            'Building all the columns in the table. 
            For Each dc In ds.Tables(0).Columns

                Dim bField As New BoundField
                bField.DataField = dc.ColumnName
                bField.HeaderText = dc.ColumnName

                bField.HeaderStyle.CssClass = "encabezados"
                bField.ItemStyle.CssClass = "resulGrid"


                If dc.DataType.Name.ToString.ToUpper = "INT32" Then
                    bField.DataFormatString = "{0:#########}"
                    bField.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                ElseIf dc.DataType.Name.ToString.ToUpper = "DECIMAL" Then
                    bField.DataFormatString = "{0:###,###,##0.00}"
                    bField.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                Else
                    bField.ItemStyle.HorizontalAlign = HorizontalAlign.Left
                End If
                'Add the newly created bound field to the GridView. 
                gridView.Columns.Add(bField)

                intColumnas = intColumnas + 1
            Next
        End If

    End Sub


    Public Sub GridView_Format_Table(ByVal ds As DataTable, ByRef gridView As GridView)

        Dim dc As DataColumn
        Dim intColumnas As Integer = 0

        If ds.Columns.Count > 0 Then
            'Building all the columns in the table. 
            For Each dc In ds.Columns

                Dim bField As New BoundField
                bField.DataField = dc.ColumnName
                bField.HeaderText = dc.ColumnName

                bField.HeaderStyle.CssClass = "encabezados"
                bField.ItemStyle.CssClass = "resulGrid"

                If ds.Columns(intColumnas).DataType.Name.ToString.ToUpper = "INT32" Then
                    bField.DataFormatString = "{0:#########}"
                    bField.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                ElseIf ds.Columns(intColumnas).DataType.Name.ToString.ToUpper = "DECIMAL" Then
                    bField.DataFormatString = "{0:###,###,##0.00}"
                    bField.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                Else
                    bField.ItemStyle.HorizontalAlign = HorizontalAlign.Left
                End If
                'Add the newly created bound field to the GridView. 
                gridView.Columns.Add(bField)

                intColumnas = intColumnas + 1
            Next
        End If
    End Sub


End Class
