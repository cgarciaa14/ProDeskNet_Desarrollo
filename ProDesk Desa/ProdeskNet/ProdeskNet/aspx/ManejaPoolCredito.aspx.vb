#Region "Trackers"
'BBV-P-423 - RQADM-25 05/05/2017 erodriguez Creacion de aspx Mesa Pool Credito
'BBVA-P-423 - RQXLS1 23/05/2017 CGARCIA creacion de consulta de alianzas por registro
'BBV-P-423 - BUG-PD-63 26/05/2017 erodriguez se agrego una actualizacion del grid y permitis guardar el usuario asignado
'BBV-P-423 - BUG-PD-79: 09/06/2017 erodriguez: se agrego validacion para ocultar boton imprimir cuando no tenga usuario asignado
'BBV-P-423 - BUG-PD-126: erodriguez: 30/06/2017 Se realizaron validaciones requeridas para ordenar los resultados del grid
'BBV-P-423 - BUG-PD-155- ERODRIGUEZ 17/07/2017: ERODRIGUEZ: Se modifico el monto solicitado.
#End Region
'Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Diagnostics
Imports ProdeskNet.BD
Imports ProdeskNet.SN
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports System.Data
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlInputFile

Partial Class aspx_ManejaPoolCredito
    Inherits System.Web.UI.Page
    Public Pantalla As Integer = 96
    Dim IntBandera As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("Regresar") Is Nothing Then
            Session("Regresar") = Request.UrlReferrer.LocalPath
        End If

        hdPantalla.Value = Pantalla

        hdusuario.Value = Session("IdUsua")

        If Not IsPostBack Then
            'Buscar(1)
        End If
        ValidaCampos()

    End Sub

    Public Function ValidaCampos() As Boolean
        ValidaCampos = False

        If (hfFeIni.Value <> "") Or (hfFeFin.Value <> "") Then
            If hfFeIni.Value = "" Then

                Master.MensajeError("Para consultar por rango de Fechas, Fecha Inicio no valida")
                Me.tbFechaInicio.Visible = True
                Me.tbFechaFin.Visible = True
                Me.txtFechaIni.Visible = False
                Me.txtFechaFin.Visible = False
                Exit Function
            Else
                Dim DATEINI As Date = DateTime.Parse(hfFeIni.Value.ToString)
                Me.txtFechaIni.Text = DATEINI.ToString("dd/MM/yyyy")
                Me.tbFechaInicio.Visible = False
                Me.txtFechaIni.Visible = True

            End If
            If hfFeFin.Value = "" Then
                Master.MensajeError("Para consultar por rango de Fechas, Fecha Fin no valida")
                Me.tbFechaInicio.Visible = True
                Me.tbFechaFin.Visible = True
                Me.txtFechaIni.Visible = False
                Me.txtFechaFin.Visible = False
                Exit Function
            Else
                Dim DATEFIN As Date = DateTime.Parse(hfFeFin.Value.ToString)
                Me.txtFechaFin.Text = DATEFIN.ToString("dd/MM/yyyy")
                Me.tbFechaFin.Visible = False
                Me.txtFechaFin.Visible = True
            End If
            If hfFeIni.Value > hfFeFin.Value Then
                Master.MensajeError("La Fecha Inicio no puede ser Mayor que la Fecha Fin")
                Me.tbFechaInicio.Visible = True
                Me.tbFechaFin.Visible = True
                Me.txtFechaIni.Visible = False
                Me.txtFechaFin.Visible = False
                Exit Function
            End If

        Else
            Me.txtFechaIni.Visible = False
            Me.txtFechaFin.Visible = False
        End If


        ValidaCampos = True
    End Function

    Public Sub LimpiaCampos()
        Me.txtNombre.Value = Nothing
        Me.txtNoSol.Value = Nothing
        Me.txtRFC.Value = Nothing
        Me.hfFeIni.Value = Nothing
        Me.hfFeFin.Value = Nothing
        'Me.txtComentarios.Text = Nothing

        'gridprueba.DataSource = Nothing
        'gridprueba.DataBind()

        Buscar(1)

    End Sub

    Public Sub Buscar(IntBandera As Integer)
        Dim objFlujos As New clsMesaPoolCredito()
        Dim ds As DataSet
        Me.IntBandera = IntBandera
        If ValidaCampos() Then
            objFlujos.PDK_CLAVE_USUARIO = Convert.ToInt32(hdusuario.Value)
            objFlujos.PDK_ID_SOLICITUD = Val(txtNoSol.Value)
            objFlujos.Nombre = txtNombre.Value
            objFlujos.RFC = txtRFC.Value

            If hfFeFin.Value <> "" And hfFeIni.Value <> "" Then
                objFlujos.FechaIni = hfFeIni.Value
                objFlujos.FechaFin = hfFeFin.Value
            End If
            objFlujos.PDK_ID_PANTALLA = Pantalla

            ds = objFlujos.ConsultaSolicitudPool(IntBandera)



            'Session("dtsConsultaG") = ds
            Me.gvMesaPoolCredito.DataSource = ds.Tables(0)
            Me.gvMesaPoolCredito.DataBind()

            cargarcheckbox(ds)


        End If
    End Sub
    Sub cargarcheckbox(ds As DataSet)
        'CODIGO PARA EL CHECKBOX ESTATUS ATENCION
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To gvMesaPoolCredito.Rows.Count - 1
                    'If ds.Tables(0).Rows(i).Item("ESTATUS_ATENCION") = 0 Or ds.Tables(0).Rows(i).Item("ESTATUS_ATENCION") = 2 Then
                    For h = 0 To ds.Tables(0).Rows.Count - 1
                        If Convert.ToInt16(ds.Tables(0).Rows(h).Item("PDK_ID_REL_COT_POOL").ToString()) = Convert.ToInt16(gvMesaPoolCredito.Rows(i).Cells(12).Text) Then
                            If ds.Tables(0).Rows(i).Item("ESTATUS_ATENCION") = 0 Or ds.Tables(0).Rows(i).Item("ESTATUS_ATENCION") = 2 Then

                                DirectCast(gvMesaPoolCredito.Rows(i).Cells(9).FindControl("chkSelecciona"), CheckBox).Checked = False
                                If ds.Tables(0).Rows(i).Item("ESTATUS_ATENCION") = 2 Then
                                    DirectCast(gvMesaPoolCredito.Rows(i).Cells(11).FindControl("btnPrint"), Button).Visible = True
                                Else
                                    DirectCast(gvMesaPoolCredito.Rows(i).Cells(11).FindControl("btnPrint"), Button).Visible = False
                                End If

                            Else
                                DirectCast(gvMesaPoolCredito.Rows(i).Cells(9).FindControl("chkSelecciona"), CheckBox).Checked = True
                                If ds.Tables(0).Rows(i).Item("ESTATUS_ATENCION") = 1 Then
                                    DirectCast(gvMesaPoolCredito.Rows(i).Cells(11).FindControl("btnPrint"), Button).Visible = True
                                End If
                            End If

                        End If

                    Next

                Next
            End If
        End If
        'FIN DE CODIGO

    End Sub


    Protected Sub btnLimpiar_Click1(sender As Object, e As EventArgs)
        LimpiaCampos()
        'ValidaCampos()
        'Buscar()
        Context.Session("idsol") = Nothing
        Me.txtFechaIni.Visible = False
        Me.txtFechaFin.Visible = False

        Me.tbFechaInicio.Visible = True
        Me.tbFechaFin.Visible = True
        Dim _Chk As New CheckBox
        'For i = 0 To gvMesaPoolCredito.Rows.Count - 1
        '    _Chk = gvMesaPoolCredito.Rows(i).Cells(8).FindControl("chkSelecciona")
        '    _Chk.Checked = False
        'Next
        txtNoSol.Value = Nothing
        tbValidarObjetos.Visible = False
    End Sub

    Protected Sub chkSelecciona_CheckedChanged(sender As Object, e As EventArgs)

        Dim checkbox As CheckBox = sender
        Dim row As GridViewRow = checkbox.NamingContainer
        Dim index As Integer = row.RowIndex


        'Dim _Chk As CheckBox = CType(sender, CheckBox)
        Dim Sol As Integer = Convert.ToInt32(gvMesaPoolCredito.Rows(index).Cells(0).Text)
        Dim id_pool As Integer = Convert.ToInt32(gvMesaPoolCredito.Rows(index).Cells(12).Text)
        Dim objFlujos As New clsMesaPoolCredito()
        Dim ds As New DataSet


        For i = 0 To gvMesaPoolCredito.Rows.Count - 1

            Dim lab As Label = New Label()
            'Sol = Convert.ToInt16(gvMesaPoolCredito.Rows(i).Cells(0).Text)

            If id_pool = Convert.ToInt16(gvMesaPoolCredito.Rows(i).Cells(12).Text) Then
                If checkbox.Checked = True Then
                    'Sol = Convert.ToInt16(gvMesaPoolCredito.Rows(i).Cells(0).Text)
                    Context.Session("idsol") = Sol

                    objFlujos.PDK_ID_SOLICITUD = Sol
                    objFlujos.PDK_CLAVE_USUARIO = hdusuario.Value
                    objFlujos.ESTATUS_ATENCION = 1
                    lab = gvMesaPoolCredito.Rows(i).Cells(9).FindControl("ID_PDK_ID_REL_COT_POOL")
                    objFlujos.PDK_ID_REL_POOL_CRED = Convert.ToInt16(lab.Text)
                    objFlujos.PDK_CLAVE_USUARIO = Convert.ToInt32(hdusuario.Value)
                    ds = objFlujos.ConsultaSolicitudPool(4)
                    'Me.txtNoSol.Text = Sol
                    'Buscar()
                    'tbValidarObjetos.Visible = True
                    'Exit For
                Else
                    'Sol = Convert.ToInt16(gvMesaPoolCredito.Rows(i).Cells(0).Text)
                    Context.Session("idsol") = Sol

                    objFlujos.PDK_ID_SOLICITUD = Sol

                    objFlujos.ESTATUS_ATENCION = 2
                    lab = gvMesaPoolCredito.Rows(i).Cells(9).FindControl("ID_PDK_ID_REL_COT_POOL")
                    objFlujos.PDK_ID_REL_POOL_CRED = Convert.ToInt16(lab.Text)
                    objFlujos.PDK_CLAVE_USUARIO = Convert.ToInt32(hdusuario.Value)
                    ds = objFlujos.ConsultaSolicitudPool(4)
                    'tbValidarObjetos.Visible = False
                End If

            End If

        Next
        Buscar(1)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        'Me.mpoForzaje.Hide()
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs)

        Buscar(2)
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        'Dim dc As New clsDatosCliente
        'dc.idSolicitud = Request("solicitud")
        'dc.getDatosSol()
        Response.Redirect(Session("Regresar"))
    End Sub

    Protected Sub Button1_Click1(sender As Object, e As EventArgs)


        'lab = gvMesaPoolCredito.Rows(i).Cells(9).FindControl("ID_PDK_ID_REL_COT_POOL")
        'objFlujos.PDK_ID_REL_POOL_CRED = Convert.ToInt16(lab.Text)
        'objFlujos.PDK_CLAVE_USUARIO = Convert.ToInt32(hdusuario.Value)
        'ds = objFlujos.ConsultaSolicitudPool(4)

        'If ValidaRespuestas() Then


        '    crReportDocument = New ReportDocument
        '    crReportDocument.Load(strRuta)

        '    'RQ06
        '    Fname = Server.MapPath("Prodesknet_" & IDCot & "_" & Format$(Now(), "dd-MM-yyyy HH-mm-ss") & ".pdf")
        '    Fname = Replace(Fname, "\aspx\", "\Docs\")

        '    System.IO.File.Delete(Fname)

        '    crDiskFileDestinationOptions = New DiskFileDestinationOptions
        '    crDiskFileDestinationOptions.DiskFileName = Fname
        '    crExportOptions = crReportDocument.ExportOptions
        '    With crExportOptions
        '        .DestinationOptions = crDiskFileDestinationOptions
        '        .ExportDestinationType = ExportDestinationType.DiskFile
        '        .ExportFormatType = ExportFormatType.PortableDocFormat

        '    End With



        '    objFlujos.PDK_ID_SOLICITUD = Val(sol)
        '    dsDatosCliente = objFlujos.ConsultaCuestionarios(2)
        '    dsDatosCliente.Tables(0).TableName = "DatosCliente"

        '    dsDatosCliente.Tables(0).Rows(0).Item("PESO") = Me.txtPeso.Text
        '    dsDatosCliente.Tables(0).Rows(0).Item("ESTATURA") = Me.txtEstatura.Text



        '    dtsDatos.Tables.Add(dsDatosCliente.Tables(0).Copy())
        '    dtsDatos.Tables(0).TableName = "DatosCliente"

        '    If ValidaRespuestas() Then
        '        Dim dsTabla As New DataSet
        '        dsTabla = Session("TABLA")

        '        dtsDatos.Tables.Add(dsTabla.Tables(0).Copy())


        '        dtsDatos.Tables(1).TableName = "Cuestionario"
        '    End If




        '    crReportDocument.SetDataSource(dtsDatos)

        '    crReportDocument.Export()
        '    crReportDocument.Close()
        '    crReportDocument.Dispose()



        '    Response.Redirect("./Descargapdf.aspx?fname=" & Fname)
        'End If

    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs)

        Dim button As Button = sender
        Dim row As GridViewRow = button.NamingContainer
        Dim index As Integer = row.RowIndex

        Dim Fname As String
        Dim crReportDocument As ReportDocument
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim Ds_1 As New DataSet


        'Dim _Chk As CheckBox = CType(sender, CheckBox)
        Dim Sol = Convert.ToInt32(gvMesaPoolCredito.Rows(index).Cells(0).Text)



        Dim strRuta As String = Server.MapPath("..\Reporte\CaratulaDeSancion.rpt")

        Dim objFlujos As New clsMesaPoolCredito()
        Dim ds As New DataSet

        objFlujos.PDK_ID_SOLICITUD = Sol
        objFlujos.PDK_CLAVE_USUARIO = Convert.ToInt32(hdusuario.Value)
        objFlujos.CausaRechazo = " rechazo"
        objFlujos.MotivoSancion = "motivo sancion"
        ds = objFlujos.ConsultaSolicitudPool(5)

        crReportDocument = New ReportDocument
        crReportDocument.Load(strRuta)


        Fname = Server.MapPath("Prodesknet_" & Sol & "_" & Format$(Now(), "dd-MM-yyyy HH-mm-ss") & ".pdf")
        Fname = Replace(Fname, "\aspx\", "\Docs\")

        System.IO.File.Delete(Fname)

        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        crDiskFileDestinationOptions.DiskFileName = Fname
        crExportOptions = crReportDocument.ExportOptions
        Dim CrFormatTypeOptions As PdfRtfWordFormatOptions
        CrFormatTypeOptions = New PdfRtfWordFormatOptions()
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
            .ExportFormatOptions = CrFormatTypeOptions

        End With





        Ds_1.Tables.Add(ds.Tables(0).Copy())
        Ds_1.Tables(0).TableName = "DatosCaratula"


        crReportDocument.SetDataSource(Ds_1)

        crReportDocument.Export()
        crReportDocument.Close()
        crReportDocument.Dispose()



        Response.Redirect("./Descargapdf.aspx?fname=" & Fname)

    End Sub

    'Protected Sub gvMesaPoolCredito_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
    '    'If CType(Session("dtsConsultaG"), DataSet) Is Nothing Then
    '    '    Buscar(IntBandera)
    '    '    'Page_Load(Me, e)
    '    'End If

    '    'gvMesaPoolCredito.PageIndex = e.NewPageIndex
    '    ''Buscar(IntBandera)
    '    'gvMesaPoolCredito.DataSource = CType(Session("dtsConsultaG"), DataSet)
    '    'gvMesaPoolCredito.DataBind()
    '    ''If e.NewPageIndex = 0 Then
    '    ''    cargarcheckbox(CType(Session("dtsConsultaG"), DataSet))
    '    ''End If



    'End Sub
End Class
