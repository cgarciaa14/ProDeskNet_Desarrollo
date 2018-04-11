#Region "TRACKERS"
'BBV-P-423-RQ-INB214:MPUESTO:18/07/2017:Reporte de Proceso de Admision
'BBV-P-423-RQ-INB213:MPUESTO:25/07/2017:Reporte de Aspectos Especiales - Mejoras en interfaz
#End Region

Imports ProdeskNet.BD
Imports System.Data.SqlClient
Imports System.Data
Imports ProdeskNet.Seguridad
Imports System.IO

Partial Class aspx_reportes
    Inherits System.Web.UI.Page

    Dim _clsDataBase As clsManejaBD
    Dim _storeCommand As SqlCommand
    Dim _connection As SqlConnection
    Dim _dsResult As DataSet
    Dim _filters As List(Of String)
    Dim _strList As New List(Of String)

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LLenaComboReportes()
            btnConsultarCliente.Visible = False
            btnExportar.Visible = False
            btnLimpiar.Visible = False
        End If
    End Sub

    Protected Sub LLenaComboReportes()
        Dim objCombo As New clsParametros
        _dsResult = New DataSet()
        _clsDataBase = New clsManejaBD()
        _dsResult = _clsDataBase.EjecutarQuery("get_Reportes")
        If _dsResult.Tables.Count > 0 And _dsResult.Tables(0).Rows.Count > 0 Then
            objCombo.LlenaCombos(_dsResult, "PDK_REP_NOMBRE_REPORTE", "PDK_REP_NOMBRE_PROCEDIMIENTO", ddlSeleccionReporte, True, True)
        End If
    End Sub

    Protected Sub ddlSeleccionReporte_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlSeleccionReporte.SelectedIndex > 0 Then
            btnConsultarCliente.Visible = True
            GetParams()
        Else
            CleanParams()
        End If
    End Sub

    Private Sub GetParams()
        Dim Counter As Integer = 0
        Dim strRow As String = String.Empty
        Dim myCommand As New SqlCommand
        myCommand.Connection = New SqlConnection
        myCommand.Connection.ConnectionString = ConfigurationManager.AppSettings("Conexion")
        myCommand.CommandText = ddlSeleccionReporte.SelectedValue.ToString()
        myCommand.CommandType = CommandType.StoredProcedure
        myCommand.Connection.Open()
        SqlCommandBuilder.DeriveParameters(myCommand)
        myCommand.Connection.Close()

        _filters = New List(Of String)
        For Each param As SqlParameter In myCommand.Parameters
            If param.Direction = ParameterDirection.Input Then
                Counter += 1
                strRow += "<td>"
                strRow += param.ParameterName.Replace("_", " ").Replace("@", "")
                strRow += "</td><td>"
                strRow += "<input type='text' id='filter_" & param.ParameterName.Replace("@", "") & "' "
                Select Case param.DbType
                    Case DbType.Date
                        strRow += " class='txt3BBVA dateClass inputParams' style='width:250px !important' />"
                    Case DbType.Int16, DbType.Int32, DbType.Int64
                        strRow += " class='txt3BBVA intClass inputParams' style='width:250px !important' onkeypress='return ValCarac(event,7);'/>"
                    Case DbType.String, DbType.StringFixedLength, DbType.AnsiString, DbType.AnsiStringFixedLength
                        strRow += IIf(param.ParameterName.Replace("_", " ").Replace("@", "").Contains("RFC"), _
                            " class='txt3BBVA textClass inputParams' style='width:250px !important' onkeypress='return ValCarac(event, 22);' />", _
                            " class='txt3BBVA textClass inputParams' style='width:250px !important' onkeypress='return ValCarac(event, 25);' />")
                End Select
                strRow += "</td>"
                strRow += "</td><td>"
                If Counter = 3 Then
                    Counter = 0
                    _filters.Add(strRow)
                    strRow = String.Empty
                End If
            End If
        Next
        If Counter = 1 Then
            strRow += "<td></td><td></td>"
        ElseIf Counter = 2 Then
            strRow += "<td></td><td></td><td></td><td></td>"
        End If
        If Counter > 0 Then
            _filters.Add(strRow)
        End If
        repFiltros.DataSource = _filters
        repFiltros.DataBind()
        btnConsultarCliente.Visible = True
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs)
        Dim paramList As String()
        If hdnParams.Value <> String.Empty Then
            hdnParams.Value = hdnParams.Value.ToString().Replace("filter_", "")
            paramList = hdnParams.Value.ToString().Split("|")
        Else
            paramList = Nothing
        End If
        Dim _dataSet As New DataSet
        Dim _commandResult As New SqlCommand
        Dim _dataAdapter As New SqlDataAdapter
        _commandResult.Connection = New SqlConnection
        _commandResult.Connection.ConnectionString = ConfigurationManager.AppSettings("Conexion")
        _commandResult.CommandText = ddlSeleccionReporte.SelectedValue.ToString()
        _commandResult.CommandType = CommandType.StoredProcedure

        If Not paramList Is Nothing Then
            For Each strItem In paramList
                _commandResult.Parameters.AddWithValue(strItem.Split("=")(0), strItem.Split("=")(1))
            Next
        End If
        _dataAdapter.SelectCommand = _commandResult
        _commandResult.Connection.Open()
        _dataAdapter.Fill(_dataSet)
        _commandResult.Connection.Close()

        Dim _strRow As String = String.Empty
        _strList = New List(Of String)
        If Not _dataSet Is Nothing AndAlso _dataSet.Tables.Count > 0 AndAlso _dataSet.Tables(0).Rows.Count > 0 Then
            _strRow += "<thead>"
            _strRow += "<tr>"
            For _ColumnIndex = 0 To _dataSet.Tables(0).Columns.Count - 1
                _strRow += "<th>" + _dataSet.Tables(0).Columns(_ColumnIndex).ColumnName.Replace("_", " ") + "</th>"
            Next
            _strRow += "</tr>"
            _strRow += "</thead>"
            _strList.Add(_strRow)

            _strRow = "<tbody>"
            For RowIndex = 0 To _dataSet.Tables(0).Rows.Count - 1
                _strRow = "<tr>"
                For _ColumnIndex = 0 To _dataSet.Tables(0).Columns.Count - 1
                    _strRow += "<td>" + _dataSet.Tables(0).Rows(RowIndex)(_ColumnIndex).ToString() + "</td>"
                Next
                _strRow += "</tr>"
                _strList.Add(_strRow)
                _strRow = String.Empty
            Next
            _strList(_strList.Count - 1) = _strList(_strList.Count - 1) + "</tbody>"
            btnExportar.Visible = True
            btnLimpiar.Visible = True
        Else
            _strList.Add("<th style='width: 100% !important'> - No se encontraron datos para la búsqueda actual - </th>")
            btnExportar.Visible = False
            btnLimpiar.Visible = False
        End If

        repResult.DataSource = _strList
        repResult.DataBind()
    End Sub

    Private Sub CleanParams()
        repFiltros.DataSource = Nothing
        repFiltros.DataBind()
        btnConsultarCliente.Visible = False
    End Sub

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs)
        Dim _StringWriter As New StringWriter()
        Dim _HtmlTextWriter As New HtmlTextWriter(_StringWriter)
        divResult.RenderControl(_HtmlTextWriter)
        Session("_ReportResult") = _StringWriter.ToString()
        Session("_ReportName") = ddlSeleccionReporte.SelectedItem.ToString().Replace(" ", "_") + DateTime.Now.ToString().Replace("/", "").Replace(" ", "_").Replace(":", "").Replace(".", "")
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "OpenNewPopUpWindow", "window.open('../aspx/ExportaReporte.aspx','_newtab');", True)
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        _strList = New List(Of String)
        _strList.Add("<th style='height: 100px !important; width: 100% !important;'> - No se encontraron datos para la búsqueda actual - </th>")
        btnExportar.Visible = False
        btnLimpiar.Visible = False
        repResult.DataSource = _strList
        repResult.DataBind()
    End Sub
End Class
