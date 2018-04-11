#Region "TRACKERS"
'BBVA-P-423 RQADM-28 17/05/2017 CGARCIA nuevo formulario de perfilamiento de consultas
'BBVA BUG-PD-84 10/06/2017 CAGRCIA CABIOS EN VALIDACION DE BUSQUEDA
'BBVA-BUG-PD-112: CGARCIA: 28/06/2017: CAMBIO EL FLUJO DE BUSQUED, ASÍ COMO CONTROLES PARA MOSTRAR LA INFORMACION
'BUG-PD-162:MPUESTO:24/07/2017:REEMPLAZO DE PAGINA ANTERIOR POR LA ACTUAL, CORRECCIONES DEL TMO DEL MODULO Y TMO ACUMULADO
'BUG-PD-185:JBEJAR:07/08/2017:CORRECIONES EN LA PAGINA AL MOMENTO DE REALIZAR  BUSQUEDA Y VALIDACION A LA BUSQUEDAD POR FECHAS
#End Region

Imports ProdeskNet.BD
Imports System.Data

Partial Class aspx_PerfilamientoDeConsultas
    Inherits System.Web.UI.Page

    Dim _clsManejaBD As clsManejaBD
    Dim _dtsResult As New DataSet()
    Dim objDateAndTime As DateTime

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btnLimpiar_Click(sender, e)
        End If
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs)
        _clsManejaBD = New clsManejaBD()
        If Not String.IsNullOrEmpty(tbxNumeroSolicitud.Text) Then
            _clsManejaBD.AgregaParametro("@No_Solicitud", TipoDato.Entero, Convert.ToInt32(tbxNumeroSolicitud.Text))
        End If
        If Not String.IsNullOrEmpty(tbxNombreSolicitante.Text) Then
            _clsManejaBD.AgregaParametro("@Nombre_del_Cliente", TipoDato.Cadena, tbxNombreSolicitante.Text)
        End If
        If Not String.IsNullOrEmpty(tbxRFCSolicitante.Text) Then
            _clsManejaBD.AgregaParametro("@RFC", TipoDato.Cadena, tbxRFCSolicitante.Text)
        End If
        If Not String.IsNullOrEmpty(txtFechaInicio.Value) Then
           
            _clsManejaBD.AgregaParametro("@Fecha_inicio", TipoDato.Fecha, txtFechaInicio.Value)
            'Else
            '    _clsManejaBD.AgregaParametro("@Fecha_inicio", TipoDato.Fecha, DateTime.Now.AddDays(-15).Date)
        End If
        If Not String.IsNullOrEmpty(txtFechaFin.Value) Then
            _clsManejaBD.AgregaParametro("@Fecha_final", TipoDato.Fecha, txtFechaFin.Value)
            'Else
            '    _clsManejaBD.AgregaParametro("@Fecha_final", TipoDato.Fecha, DateTime.Now.Date)
        End If
        If String.IsNullOrEmpty(tbxAsesor.Text) Then
            _clsManejaBD.AgregaParametro("@Asesor", TipoDato.Cadena, tbxAsesor.Text)
        End If
        _dtsResult = _clsManejaBD.EjecutaStoredProcedure("get_reporteAdmision")
        DoDataBind(_dtsResult)
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        tbxNumeroSolicitud.Text = ""
        tbxNombreSolicitante.Text = ""
        tbxRFCSolicitante.Text = ""
        txtFechaInicio.Value = ""
        txtFechaFin.Value = ""
        tbxAsesor.Text = ""
        _clsManejaBD = New clsManejaBD()
        _clsManejaBD.AgregaParametro("@Fecha_inicio", TipoDato.Fecha, DateTime.Now.AddDays(-15).Date)
        _clsManejaBD.AgregaParametro("@Fecha_final", TipoDato.Fecha, DateTime.Now.Date)
        _dtsResult = _clsManejaBD.EjecutaStoredProcedure("get_reporteAdmision")
        DoDataBind(_dtsResult)
    End Sub

    Public Sub DoDataBind(ByVal _dataSet As DataSet)
        If Not _dataSet Is Nothing AndAlso _dataSet.Tables.Count > 0 AndAlso _dataSet.Tables(0).Rows.Count > 0 Then
            repResult.DataSource = _dataSet
            repResult.DataBind()
            divEmptyTableResult.Visible = False
            divTableResult.Visible = True
        Else
            divEmptyTableResult.Visible = True
            divTableResult.Visible = False
        End If
    End Sub

    Public Function CutText(ByVal str As Object) As String
        Dim text = CType(str, String)
        If text.Length > 20 Then
            text = text.Substring(0, 20) + "..."
        End If
        Return text
    End Function

    ''' <summary>
    ''' Devuelve una parte de una variable DateTime dependiendo del parámetro de entrada
    ''' </summary>
    ''' <param name="dateTimeObj">El objeto DateTime o string del cual se extraerá sólo la fecha o la hora </param>
    ''' <returns>Cadena con la fecha en formato DD/MM/YYYY o cadena con la hora en formato HH:MM:SS</returns>
    ''' <remarks></remarks>
    Public Function GetPartOfTime(ByVal dateTimeObj As Object, ByVal isTheDate As Boolean) As String
        Dim dateParts As String()
        dateParts = dateTimeObj.ToString().Split(" ")
        Return IIf(isTheDate, (dateParts(0) + "/" + dateParts(1) + "/" + dateParts(2)), (dateParts(3)))
    End Function

End Class
