'BBV-P-423: RQADM-14:AVH: 17/03/2017 CALCULO DE INGRESOS
'BBV-P-423: RQADM-22: JRHM: 24/03/17 SE MODIFICA FUNCIONALIDAD DE RECHAZO	   
'BUG-PD-22: JBB: 27/03/2017 CORRECIONES  EN EL METODO LIMPIAR CAMPOS. 
'BBV-P-423-RQADM-09 JBB 04/04/2017 CORRECIONES CALCULO DE INGRESOS. 
'BBVA-P-423: RQCONYFOR-07_2: AVH: 11/04/2017 modificaciones JBEJAR
'BUG-PD-37 JBB: 24/04/2017 Se hacen correciones para guardar todos los campos solicitados y se corrigen los decimales en asalariados. 
'BUG-PD-55 JBEJAR 25/05/2017 Se hacen correciones para guardar  se corrige visor y se agregan validaciones. 
'BUG-PD-64 JBEJAR 26/05/2017 Se hacen validaciones. 
'BUG-PD-71 JBEJAR 05/06/2017 correciones calculo de ingresos 
'BUG-PD-74 JBEJAR 07/06/2017 Se agregan 35 perscepciones. En la pantalla calculo de ingresos. 
'BUG-PD-80 JBEJAR:  09/06/2017 Todas las percepciones nullas se iran a 0 pero se valida que el total no sea 0 minimo debe ir una percepcion con valor.
'BUG-PD-94 JBEJAR: 15/06/2017 Se oculta. el boton de guardar al procesar la tarea.
'BUG-PD-97 JBEJAR: 22/06/2017 Correciones calculo de ingresos y cambio de formulas. 
'BUG-PD-108 JBEJAR: 29/06/2017 Rework calculo de ingresos.
'BUG-PD-125 JBEJAR: 01/07/2017 se agrega validacion de cdfi en calculo de ingresos. 
'BUG-PD-124 ERODRIGUEZ 06/07/2017 Se agregó enlace a reporte.
'BUG-PD-153 JBEJAR 07/07/2017 Se elimina validacion de tipifacion al guardar. 
'BUG-PD-156 JBEJAR 17/07/2017 Correcion datos duplicados al guardar.  
'BUG-PD-203: CGARCIIA: 04/09/2017: SE AGREGA NOTIFICACIONES 
'BUG-PD-215: CGARCIA: 26/09/2017: ACTUALIZACION DE CORREOS PARA CASOS AL QUE ENTRA.
'BUG-PD-218: CGARCIA: 29/09/2017: ACTUALIZACION DE EMAIL
'BUG-PD-227: CGARCIA: 05/10/2017: ACTUALIZACION DE EMAILS Y SCORE
'BUG-PD-237: RHERNANDEZ: 17/10/17: SE CORRIGE MENSAJE AL SELECCIONAR PROCESAR EN LAS OPCIONES DE TURNAR
'BUG-PD-276: CGARCIA: 27/11/2017: CORRECCION DE MSJ Y REQUEST DE USUARIOS EN EMAILS
'BBV-P-423 RQ-PD-17 10 GVARGAS 31/01/2018 Ajustes flujos
'BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit

Imports System.Data
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Configurcion
Imports ProdeskNet.SN
Imports System.Globalization
Imports ProdeskNet.BD
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Partial Class aspx_CalculoDeIngresos
    Inherits System.Web.UI.Page
    Dim Maximo As Integer = 35 'Maximo de percepciones   
    Dim es As New ProdeskNet.Catalogos.clsStatusSolicitud
    Dim dc As New ProdeskNet.Catalogos.clsDatosCliente
    Public ClsEmail As New clsEmailAuto()
    Dim usu As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.lblSolicitud.Text = Request.QueryString("sol")

        es.getStatusSol(Request.QueryString("sol"))
        Me.lblStCredito.Text = es.PStCredito
        Me.lblStDocumento.Text = es.PStDocumento

        dc.GetDatosCliente(lblSolicitud.Text)
        lblCliente.Text = dc.propNombreCompleto

        hdPantalla.Value = Request("idPantalla") 'BUG-PD-97 SE AGREGAN VARIABLES PARA OCUPAR EL APARTADO DE DOCUMENTOS.   
        hdSolicitud.Value = Request("sol")
        Session("idsol") = hdSolicitud.Value
        hdusuario.Value = Request("usu")

        Dim intEnable As Integer
        intEnable = CInt(Request.QueryString("Enable"))
        If intEnable = 1 Then
            btnProcesarCliente.Attributes.Add("style", "display:none;")
            Button1.Attributes.Add("style", "display:none;")
        End If
        If Session("Regresar") Is Nothing Then
            Session("Regresar") = Request.UrlReferrer.LocalPath
        End If

        usu = Val(Request("usuario"))
        If usu = String.Empty Then
            usu = Val(Request("usu"))
        End If

        If Not IsPostBack Then
            CargaCombos()
            ddlTipoActividad_SelectedIndexChanged(sender, e)

            Dim objBuscaCalculo As New clsCalculoIngreso()
            Dim dsDatos As DataSet
            objBuscaCalculo.ID_SOLICITUD = lblSolicitud.Text
            dsDatos = objBuscaCalculo.CalculoIngreso(1)

            If dsDatos.Tables.Count > 1 Then
                If dsDatos.Tables(0).Rows.Count > 0 Then
                    Me.ddlTipoActividad.SelectedValue = dsDatos.Tables(0).Rows(0).Item("PDK_TIPO_ACTIVIDAD_ECONOMICA").ToString
                    Me.ddlTipoReciboAsalariado.SelectedValue = dsDatos.Tables(0).Rows(0).Item("PDK_TIPO_RECIBO_ASALARIADO").ToString
                    Me.ddlTipoReciboNoAsalariado.SelectedValue = dsDatos.Tables(0).Rows(0).Item("PDK_TIPO_RECIBO_NOASALARIADO").ToString
                    Me.ddlRecibosAsalariado.SelectedValue = dsDatos.Tables(0).Rows(0).Item("PDK_RECIBOS_ASALARIADOS").ToString
                    Me.ddlRecibosNoAsalariado.SelectedValue = dsDatos.Tables(0).Rows(0).Item("PDK_RECIBOS_NOASALARIADOS").ToString
                    Me.ddlPeriodoPago.SelectedValue = dsDatos.Tables(0).Rows(0).Item("PDK_PERIODO_PAGO").ToString
                    Me.lblTotalIngresosAsalariados.Text = dsDatos.Tables(0).Rows(0).Item("PDK_INGRESO_ASALARIADO").ToString
                    Me.lblTotalIngresosNoAsalariados.Text = dsDatos.Tables(0).Rows(0).Item("PDK_INGRESO_NOASALARIADO").ToString
                    Me.txtTotalIngresos.Text = dsDatos.Tables(0).Rows(0).Item("PDK_TOTAL_INGRESOS").ToString
                    Me.ddlTipificaciones.SelectedValue = dsDatos.Tables(0).Rows(0).Item("PDK_TIPIFICACION").ToString
                    ddlTipoActividad_SelectedIndexChanged(sender, e)


                    If dsDatos.Tables(1).Rows.Count > 0 Then
                        Dim Actividad As Integer
                        Actividad = dsDatos.Tables(1).Rows(0).Item("PDK_TIPO_ACTIVIDAD_ECONOMICA")

                        If Actividad = 248 Then 'asalariado
                            grvAsalariados.DataSource = dsDatos.Tables(1)
                            grvAsalariados.DataBind()
                        Else
                            grvNoAsalariados.DataSource = dsDatos.Tables(1)
                            grvNoAsalariados.DataBind()
                        End If
                    End If

                    If dsDatos.Tables.Count = 3 Then
                        grvNoAsalariados.DataSource = dsDatos.Tables(2)
                        grvNoAsalariados.DataBind()
                    End If
                    ddlPeriodoPago_SelectedIndexChanged(sender, e)
                End If
            End If
        End If


        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
        Dim dsCalendario As DataSet

        objCatalogos.Parametro = lblSolicitud.Text
        dsCalendario = objCatalogos.Catalogos(4)

        If dsCalendario.Tables.Count > 0 Then
            If dsCalendario.Tables(0).Rows.Count > 0 Then
                Dim DiasPeriodo As Integer = dsCalendario.Tables(0).Rows(0).Item("VALOR")

                'If Me.txtDiasPeriodoPago.Text = "" Then
                '    Me.txtDiasPeriodoPago.Text = 0
                'End If

                'If Me.txtDiasPeriodoPago.Text > DiasPeriodo Then
                '    Master.MensajeError("Maximo " + DiasPeriodo.ToString + " días")
                '    Me.txtDiasPeriodoPago.Text = ""
                '    Me.txtDiasPeriodoPago.Focus()
                '    Exit Sub
                'End If
            End If
        End If


        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');", True) 'BUG-PD-97 VALIDAR DOCUMENTO 



    End Sub

    Public Sub CargaCombos()
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
        Dim objCombo As New clsParametros

        ''TIPO DE ACTIVIDAD ECONOMICA
        Dim dsTipoActividadEconomica As DataSet
        objCatalogos.Parametro = 247
        dsTipoActividadEconomica = objCatalogos.Catalogos(3)

        If dsTipoActividadEconomica.Tables.Count > 0 And dsTipoActividadEconomica.Tables(0).Rows.Count > 0 Then
            objCombo.LlenaCombos(dsTipoActividadEconomica, "PDK_PAR_SIS_PARAMETRO", "PDK_ID_PARAMETROS_SISTEMA", ddlTipoActividad, True, True)
        End If

        ''TIPO DE RECIBO ASALARIADO
        Dim dsTipoReciboAsalariado As DataSet
        objCatalogos.Parametro = 251
        dsTipoReciboAsalariado = objCatalogos.Catalogos(3)

        If dsTipoReciboAsalariado.Tables.Count > 0 And dsTipoReciboAsalariado.Tables(0).Rows.Count > 0 Then
            objCombo.LlenaCombos(dsTipoReciboAsalariado, "PDK_PAR_SIS_PARAMETRO", "PDK_ID_PARAMETROS_SISTEMA", ddlTipoReciboAsalariado, True, True)
        End If

        ''TIPO DE RECIBO NO ASALARIADO
        Dim dsTipoReciboNoAsalariado As DataSet
        objCatalogos.Parametro = 254
        dsTipoReciboNoAsalariado = objCatalogos.Catalogos(3)

        If dsTipoReciboNoAsalariado.Tables.Count > 0 And dsTipoReciboNoAsalariado.Tables(0).Rows.Count > 0 Then
            objCombo.LlenaCombos(dsTipoReciboNoAsalariado, "PDK_PAR_SIS_PARAMETRO", "PDK_ID_PARAMETROS_SISTEMA", ddlTipoReciboNoAsalariado, True, True)
        End If

        ''PERIODO DE PAGO
        Dim dsPeriodoPago As DataSet
        objCatalogos.Parametro = 257
        dsPeriodoPago = objCatalogos.Catalogos(3)

        If dsPeriodoPago.Tables.Count > 0 And dsPeriodoPago.Tables(0).Rows.Count > 0 Then
            objCombo.LlenaCombos(dsPeriodoPago, "PDK_PAR_SIS_PARAMETRO", "PDK_ID_PARAMETROS_SISTEMA", ddlPeriodoPago, True)
        End If



    End Sub

    Protected Sub ddlTipoTipificacion_SelectedIndexChanged(sender As Object, e As EventArgs)



        If Me.ddlTipificaciones.SelectedValue <> 0 Then 'Inabilita o habilita boton procesar dependiendo de las tipificaciones JBEJAR 

            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable();", True)

        Else
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable1();", True)

        End If


    End Sub
    Protected Sub btnValidar1_Click(sender As Object, e As EventArgs)
        btnValidar1.Enabled = False

        Dim mensaje As String = String.Empty
        hdnResultado1.Value = 0
        'consultar el servicio
        Dim objmdec As New ProdeskNet.WCF.ClsCFDI
        'objmdec.transmitterTaxpayer = "MSE071130LB2"
        'objmdec.receiverTaxpayer = "MARJ6908135D8"
        'objmdec.totalRevenues = "0000000030277.180000"
        'objmdec.referenceFiscalNumber = "352C0C29-D960-40A8-AA1D-0ECC8525676F"

        objmdec.transmitterTaxpayer = txtRFCE1.Value
        objmdec.receiverTaxpayer = txtRFCR1.Value
        objmdec.totalRevenues = txtTotal1.Value
        'objmdec.referenceFiscalNumber = txtFolioFiscal.Text
        objmdec.referenceFiscalNumber = txtFolioFiscal1.Value
        'mensaje = "Validando"
        'If  Then
        '    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('" & mensaje & "CFDI" & "')", True)
        '    'Master.MensajeError(mensaje)
        '    hdnResultado1.Value = 1


        'Else
        objmdec.ValidaCFDI()
        mensaje = objmdec.estatus + " " + objmdec.Strerror
        'mensaje = "Servivio no disponible"
        'btnProcesarCliente.Disabled = True

        Master.MensajeError(mensaje)

        btnValidar1.Enabled = True
        limpiar_cfdi()
        'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Alert", "alert('" & mensaje & "')", True)

        'End If



    End Sub
    Public Sub limpiar_cfdi()
        txtRFCE1.Value = ""
        txtRFCR1.Value = ""
        txtFolioFiscal1.Value = ""
        txtTotal1.Value = ""
    End Sub
    'Public Function validadatoscfdi() As Boolean
    '    Dim estado As Boolean = False

    '    If txtFolioFiscal1.Value = String.Empty Then
    '        'If txtFolioFiscal.Text = String.Empty Then
    '        Master.MensajeError("Se tiene que capturar el folio fiscal")
    '        'ElseIf (Len(txtFolioFiscal.Text) < 32) Then
    '    ElseIf (Len(txtFolioFiscal1.Value) < 32) Then
    '        Master.MensajeError("El Folio fiscal capturado contiene formato invalido")
    '    ElseIf (txtTotal1.Value = String.Empty) Then
    '        Master.MensajeError("Falta introducir el total de la cantidad")
    '    ElseIf (txtRFCE1.Value = String.Empty) Then
    '        Master.MensajeError("Falta introducir el RFC del emisor")
    '    ElseIf (txtRFCR1.Value = String.Empty) Then
    '        Master.MensajeError("Falta introducir el RFC del receptor")

    '    Else
    '        estado = True
    '    End If

    '    Return estado
    'End Function

    Protected Sub ddlTipoActividad_SelectedIndexChanged(sender As Object, e As EventArgs)



        If Me.ddlTipoActividad.SelectedValue = 248 Or Me.ddlTipoActividad.SelectedValue = 250 Then
            Me.ddlTipoReciboAsalariado.Enabled = True
            Me.ddlPeriodoPago.Enabled = True
            Me.ddlRecibosAsalariado.Enabled = True
            Me.Div2.Visible = True
            Me.lblAsalariados.Visible = True
        Else
            Me.ddlTipoReciboAsalariado.Enabled = False
            Me.ddlPeriodoPago.Enabled = False
            Me.ddlRecibosAsalariado.Enabled = False

            Me.ddlTipoReciboAsalariado.SelectedValue = 0
            Me.ddlPeriodoPago.SelectedValue = 0
            Me.ddlRecibosAsalariado.SelectedValue = 0
            Me.Div2.Visible = False
            Me.lblAsalariados.Visible = False
        End If

        If Me.ddlTipoActividad.SelectedValue = 249 Or Me.ddlTipoActividad.SelectedValue = 250 Then
            Me.ddlTipoReciboNoAsalariado.Enabled = True
            Me.ddlRecibosNoAsalariado.Enabled = True
            Me.Div3.Visible = True
            Me.lblNoAsalariados.Visible = True
        Else
            Me.ddlTipoReciboNoAsalariado.Enabled = False
            Me.ddlRecibosNoAsalariado.Enabled = False

            Me.ddlTipoReciboNoAsalariado.SelectedValue = 0
            Me.ddlRecibosNoAsalariado.SelectedValue = 0
            Me.Div3.Visible = False
            Me.lblNoAsalariados.Visible = False

        End If

        ddlRecibosAsalariado_SelectedIndexChanged(sender, e)
        ddlRecibosNoAsalariado_SelectedIndexChanged(sender, e)
    End Sub
    Private Sub ResetRowID(dt As DataTable)
        Dim rowNumber As Integer = 1
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                row(0) = rowNumber
                rowNumber += 1
            Next
        End If
    End Sub
    Protected Sub grvAsalariados_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvAsalariados.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dt As DataTable = DirectCast(ViewState("Asalariados"), DataTable)
            Dim imgbtn As ImageButton = DirectCast(e.Row.FindControl("ImageButton1"), ImageButton)
            If imgbtn IsNot Nothing Then
                If dt.Rows.Count > 1 Then
                    If e.Row.RowIndex = dt.Rows.Count - 1 Then
                        imgbtn.Visible = True
                    End If
                Else
                    imgbtn.Visible = True
                End If
            End If
        End If
    End Sub
    Protected Sub grvNoAsalariados_RowCreated(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dt As DataTable = DirectCast(ViewState("NoAsalariados"), DataTable)
            Dim imgbtn As ImageButton = DirectCast(e.Row.FindControl("ImageButton1"), ImageButton)
            If imgbtn IsNot Nothing Then
                If dt.Rows.Count > 1 Then
                    If e.Row.RowIndex = dt.Rows.Count - 1 Then
                        imgbtn.Visible = True
                    End If
                Else
                    imgbtn.Visible = True
                End If
            End If
        End If
    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As EventArgs)
        ''Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)

        Try

            Dim lb As ImageButton = DirectCast(sender, ImageButton)
            Dim gvRow As GridViewRow = DirectCast(lb.NamingContainer, GridViewRow)
            Dim rowID As Integer = gvRow.RowIndex
            Dim Filtro As String() = Split(sender.CommandArgument.ToString(), "|")


            If ViewState("Asalariados") IsNot Nothing Then

                Dim dt As DataTable = DirectCast(ViewState("Asalariados"), DataTable)
                'If dt.Rows.Count > 1 Then
                'If gvRow.RowIndex < dt.Rows.Count - 1 Then
                'Remove the Selected Row data and reset row number  
                dt.Rows.Remove(dt.Rows(rowID))
                ResetRowID(dt)
                'End If
                'End If


                ViewState("Asalariados") = dt

                grvAsalariados.DataSource = dt
                grvAsalariados.DataBind()

                'Dim objAdicionales As New clsVersiones
                'objAdicionales.CargaSession(Val(Session("cveAcceso")))
                'objAdicionales.IDVersion = lblidversion.Text

                If Filtro(0) <> "" Then
                    'objAdicionales.Plazo = Filtro(0)
                    'objAdicionales.UsuReg = objAdicionales.UserNameAcceso
                    'objAdicionales.ManejaVersion(7)
                End If
                'BUG-PC-24 14/12/2016 MAUT Se agrega Try Catch y mensaje de guardado
                'MensajeError("Registro guardado exitosamente")

            End If

            'Set Previous Data on Postbacks  
            SetPreviousDataAsalariados()
            CalculaNoAsalariado()
        Catch ex As Exception
            'MensajeError(ex.Message)
        End Try

    End Sub
    Protected Sub ImageButton1_Click1(sender As Object, e As ImageClickEventArgs)
        Try

            Dim lb As ImageButton = DirectCast(sender, ImageButton)
            Dim gvRow As GridViewRow = DirectCast(lb.NamingContainer, GridViewRow)
            Dim rowID As Integer = gvRow.RowIndex
            Dim Filtro As String() = Split(sender.CommandArgument.ToString(), "|")


            If ViewState("NoAsalariados") IsNot Nothing Then

                Dim dt As DataTable = DirectCast(ViewState("NoAsalariados"), DataTable)
                'If dt.Rows.Count > 1 Then
                '    If gvRow.RowIndex < dt.Rows.Count - 1 Then
                'Remove the Selected Row data and reset row number  
                dt.Rows.Remove(dt.Rows(rowID))
                ResetRowID(dt)
                'End If
                '    End If


                ViewState("NoAsalariados") = dt

                grvNoAsalariados.DataSource = dt
                grvNoAsalariados.DataBind()

                'Dim objAdicionales As New clsVersiones
                'objAdicionales.CargaSession(Val(Session("cveAcceso")))
                'objAdicionales.IDVersion = lblidversion.Text

                If Filtro(0) <> "" Then
                    'objAdicionales.Plazo = Filtro(0)
                    'objAdicionales.UsuReg = objAdicionales.UserNameAcceso
                    'objAdicionales.ManejaVersion(7)
                End If
                'BUG-PC-24 14/12/2016 MAUT Se agrega Try Catch y mensaje de guardado
                'MensajeError("Registro guardado exitosamente")

            End If

            'Set Previous Data on Postbacks  
            SetPreviousDataNoAsalariados()
            CalculaNoAsalariado()

        Catch ex As Exception
            'MensajeError(ex.Message)
        End Try
    End Sub
    Protected Sub ddlRecibosAsalariado_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim Recibos As Integer = ddlRecibosAsalariado.SelectedValue

        If Recibos > 0 Then
            Me.divAgregarAsalariados.Visible = True
            Me.grvAsalariados.Visible = True

            llenaAsalariados()

            For x = 1 To grvAsalariados.Columns.Count - 1
                If x > Me.ddlRecibosAsalariado.SelectedValue Then
                    Me.grvAsalariados.Columns(x).Visible = False
                Else
                    Me.grvAsalariados.Columns(x).Visible = True
                End If

            Next

            Me.grvAsalariados.Columns(16).Visible = True

            TotalAsalariado()
        Else
            Me.divAgregarAsalariados.Visible = False
            Me.grvAsalariados.Visible = False

        End If
    End Sub
    Protected Sub ddlRecibosNoAsalariado_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim Recibos As Integer = ddlRecibosNoAsalariado.SelectedValue

        If Recibos > 0 Then
            Me.divAgregarNoAsalariados.Visible = True
            Me.grvNoAsalariados.Visible = True

            llenaNoAsalariados()

            For x = 1 To grvNoAsalariados.Columns.Count - 1
                If x > Me.ddlRecibosNoAsalariado.SelectedValue Then
                    Me.grvNoAsalariados.Columns(x).Visible = False
                Else
                    Me.grvNoAsalariados.Columns(x).Visible = True
                End If

            Next

            Me.grvNoAsalariados.Columns(7).Visible = True

            TotalNoAsalariado()
        Else
            Me.divAgregarNoAsalariados.Visible = False
            Me.grvNoAsalariados.Visible = False

        End If



    End Sub

    Private Sub llenaAsalariados()
        SetInitialRowAsalariados()
    End Sub

    Private Sub llenaNoAsalariados()
        SetInitialRowNoAsalariados()
    End Sub

    Private Sub SetInitialRowAsalariados()

        Dim dt As New DataTable()
        Dim dr As DataRow = Nothing

        dt.Columns.Add(New DataColumn("RowNumber", GetType(String)))
        dt.Columns.Add(New DataColumn("Column1", GetType(String)))
        dt.Columns.Add(New DataColumn("Column2", GetType(String)))
        dt.Columns.Add(New DataColumn("Column3", GetType(String)))
        dt.Columns.Add(New DataColumn("Column4", GetType(String)))
        dt.Columns.Add(New DataColumn("Column5", GetType(String)))
        dt.Columns.Add(New DataColumn("Column6", GetType(String)))
        dt.Columns.Add(New DataColumn("Column7", GetType(String)))
        dt.Columns.Add(New DataColumn("Column8", GetType(String)))
        dt.Columns.Add(New DataColumn("Column9", GetType(String)))
        dt.Columns.Add(New DataColumn("Column10", GetType(String)))
        dt.Columns.Add(New DataColumn("Column11", GetType(String)))
        dt.Columns.Add(New DataColumn("Column12", GetType(String)))
        dt.Columns.Add(New DataColumn("Column13", GetType(String)))
        dt.Columns.Add(New DataColumn("Column14", GetType(String)))
        dt.Columns.Add(New DataColumn("Column15", GetType(String)))
        dr = dt.NewRow()
        dr("RowNumber") = 1
        dr("Column1") = String.Empty
        dt.Rows.Add(dr)

        'Store the DataTable in ViewState for future reference   
        ViewState("Asalariados") = dt

        'Bind the Gridview   
        grvAsalariados.DataSource = dt
        grvAsalariados.DataBind()

    End Sub
    Private Sub SetInitialRowNoAsalariados()

        Dim dt As New DataTable()
        Dim dr As DataRow = Nothing

        dt.Columns.Add(New DataColumn("RowNumber", GetType(String)))
        dt.Columns.Add(New DataColumn("Column1", GetType(String)))
        dt.Columns.Add(New DataColumn("Column2", GetType(String)))
        dt.Columns.Add(New DataColumn("Column3", GetType(String)))
        dt.Columns.Add(New DataColumn("Column4", GetType(String)))
        dt.Columns.Add(New DataColumn("Column5", GetType(String)))

        dr = dt.NewRow()
        dr("RowNumber") = 1
        dr("Column1") = String.Empty
        dt.Rows.Add(dr)

        'Store the DataTable in ViewState for future reference   
        ViewState("NoAsalariados") = dt

        'Bind the Gridview   
        grvNoAsalariados.DataSource = dt
        grvNoAsalariados.DataBind()

    End Sub

    Protected Sub cmdAgregaAsalariados_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdAgregaAsalariados.Click
        Dim count As Integer = 0


        For Each row As GridViewRow In grvAsalariados.Rows

            Dim boxEnganche As TextBox = TryCast(row.FindControl("txtEnganche"), TextBox)
            Dim boxBalloon As TextBox = TryCast(row.FindControl("txtBalloon"), TextBox)

            Dim boxComprobante1 As TextBox = TryCast(row.FindControl("txtComprobante1"), TextBox)
            Dim boxComprobante2 As TextBox = TryCast(row.FindControl("txtComprobante2"), TextBox)
            Dim boxComprobante3 As TextBox = TryCast(row.FindControl("txtComprobante3"), TextBox)
            Dim boxComprobante4 As TextBox = TryCast(row.FindControl("txtComprobante4"), TextBox)
            Dim boxComprobante5 As TextBox = TryCast(row.FindControl("txtComprobante5"), TextBox)
            Dim boxComprobante6 As TextBox = TryCast(row.FindControl("txtComprobante6"), TextBox)
            Dim boxComprobante7 As TextBox = TryCast(row.FindControl("txtComprobante7"), TextBox)
            Dim boxComprobante8 As TextBox = TryCast(row.FindControl("txtComprobante8"), TextBox)
            Dim boxComprobante9 As TextBox = TryCast(row.FindControl("txtComprobante9"), TextBox)
            Dim boxComprobante10 As TextBox = TryCast(row.FindControl("txtComprobante10"), TextBox)
            Dim boxComprobante11 As TextBox = TryCast(row.FindControl("txtComprobante11"), TextBox)
            Dim boxComprobante12 As TextBox = TryCast(row.FindControl("txtComprobante12"), TextBox)
            Dim boxComprobante13 As TextBox = TryCast(row.FindControl("txtComprobante13"), TextBox)
            Dim boxComprobante14 As TextBox = TryCast(row.FindControl("txtComprobante14"), TextBox)
            Dim boxComprobante15 As TextBox = TryCast(row.FindControl("txtComprobante15"), TextBox)
        Next

        If count > 0 Then
            'LimpiaError()
            'MensajeError("Todos los campos marcados con * son obligatorios.")
            Exit Sub
        Else
            AddNewRowToGridAsalariados()
        End If

    End Sub

    Protected Sub cmdAgregaNoAsalariados_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdAgregaNoAsalariados.Click
        Dim count As Integer = 0


        For Each row As GridViewRow In grvNoAsalariados.Rows

            Dim boxEnganche As TextBox = TryCast(row.FindControl("txtEnganche"), TextBox)
            Dim boxBalloon As TextBox = TryCast(row.FindControl("txtBalloon"), TextBox)

            Dim boxComprobante1 As TextBox = TryCast(row.FindControl("txtComprobante1"), TextBox)
            Dim boxComprobante2 As TextBox = TryCast(row.FindControl("txtComprobante2"), TextBox)
            Dim boxComprobante3 As TextBox = TryCast(row.FindControl("txtComprobante3"), TextBox)
            Dim boxComprobante4 As TextBox = TryCast(row.FindControl("txtComprobante4"), TextBox)
            Dim boxComprobante5 As TextBox = TryCast(row.FindControl("txtComprobante5"), TextBox)
        Next

        If count > 0 Then
            'LimpiaError()
            'MensajeError("Todos los campos marcados con * son obligatorios.") Validamos en frente. 
            Exit Sub
        Else
            AddNewRowToGridNoAsalariados()
        End If

    End Sub

    Private Sub AddNewRowToGridAsalariados()

        If ViewState("Asalariados") IsNot Nothing Then

            Dim dtCurrentTable As DataTable = DirectCast(ViewState("Asalariados"), DataTable)
            Dim drCurrentRow As DataRow = Nothing

            If dtCurrentTable.Rows.Count > 0 And dtCurrentTable.Rows.Count <= Maximo Then
                drCurrentRow = dtCurrentTable.NewRow()
                drCurrentRow("RowNumber") = dtCurrentTable.Rows.Count + 1

                'add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow)

                'Store the current data to ViewState for future reference   
                ViewState("Asalariados") = dtCurrentTable


                For i As Integer = 0 To dtCurrentTable.Rows.Count - 2


                    Dim boxComprobante1 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(2).FindControl("txtComprobante1"), TextBox)
                    dtCurrentTable.Rows(i)("Column1") = IIf(boxComprobante1.Text = "", 0, boxComprobante1.Text)

                    Dim boxComprobante2 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(3).FindControl("txtComprobante2"), TextBox)
                    dtCurrentTable.Rows(i)("Column2") = IIf(boxComprobante2.Text = "", 0, boxComprobante2.Text)

                    Dim boxComprobante3 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(4).FindControl("txtComprobante3"), TextBox)
                    dtCurrentTable.Rows(i)("Column3") = IIf(boxComprobante3.Text = "", 0, boxComprobante3.Text)

                    Dim boxComprobante4 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(5).FindControl("txtComprobante4"), TextBox)
                    dtCurrentTable.Rows(i)("Column4") = IIf(boxComprobante4.Text = "", 0, boxComprobante4.Text)

                    Dim boxComprobante5 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(6).FindControl("txtComprobante5"), TextBox)
                    dtCurrentTable.Rows(i)("Column5") = IIf(boxComprobante5.Text = "", 0, boxComprobante5.Text)

                    Dim boxComprobante6 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(7).FindControl("txtComprobante6"), TextBox)
                    dtCurrentTable.Rows(i)("Column6") = IIf(boxComprobante6.Text = "", 0, boxComprobante6.Text)

                    Dim boxComprobante7 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(8).FindControl("txtComprobante7"), TextBox)
                    dtCurrentTable.Rows(i)("Column7") = IIf(boxComprobante7.Text = "", 0, boxComprobante7.Text)

                    Dim boxComprobante8 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(9).FindControl("txtComprobante8"), TextBox)
                    dtCurrentTable.Rows(i)("Column8") = IIf(boxComprobante8.Text = "", 0, boxComprobante8.Text)

                    Dim boxComprobante9 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(10).FindControl("txtComprobante9"), TextBox)
                    dtCurrentTable.Rows(i)("Column9") = IIf(boxComprobante9.Text = "", 0, boxComprobante9.Text)

                    Dim boxComprobante10 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(6).FindControl("txtComprobante10"), TextBox)
                    dtCurrentTable.Rows(i)("Column10") = IIf(boxComprobante10.Text = "", 0, boxComprobante10.Text)

                    Dim boxComprobante11 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(2).FindControl("txtComprobante11"), TextBox)
                    dtCurrentTable.Rows(i)("Column11") = IIf(boxComprobante11.Text = "", 0, boxComprobante11.Text)

                    Dim boxComprobante12 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(3).FindControl("txtComprobante12"), TextBox)
                    dtCurrentTable.Rows(i)("Column12") = IIf(boxComprobante12.Text = "", 0, boxComprobante12.Text)

                    Dim boxComprobante13 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(4).FindControl("txtComprobante13"), TextBox)
                    dtCurrentTable.Rows(i)("Column13") = IIf(boxComprobante13.Text = "", 0, boxComprobante13.Text)

                    Dim boxComprobante14 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(5).FindControl("txtComprobante14"), TextBox)
                    dtCurrentTable.Rows(i)("Column14") = IIf(boxComprobante14.Text = "", 0, boxComprobante14.Text)

                    Dim boxComprobante15 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(6).FindControl("txtComprobante15"), TextBox)
                    dtCurrentTable.Rows(i)("Column15") = IIf(boxComprobante15.Text = "", 0, boxComprobante15.Text)

                Next

                If dtCurrentTable.Rows.Count > Maximo Then
                    dtCurrentTable.Rows(Maximo).Delete()
                    Master.MensajeError("Solo puede capturar " + Maximo.ToString + " Percepciones")
                End If



                'Rebind the Grid with the current data to reflect changes   
                grvAsalariados.DataSource = dtCurrentTable
                grvAsalariados.DataBind()


            Else
                'Master.MensajeError("Solo puede capturar " + Maximo.ToString + " Percepciones")
            End If
        Else

            Response.Write("ViewState is null")
        End If
        'Set Previous Data on Postbacks   
        SetPreviousDataAsalariados()
        TotalAsalariado()
    End Sub

    Private Sub AddNewRowToGridNoAsalariados()

        If ViewState("NoAsalariados") IsNot Nothing Then

            Dim dtCurrentTable As DataTable = DirectCast(ViewState("NoAsalariados"), DataTable)
            Dim drCurrentRow As DataRow = Nothing

            If dtCurrentTable.Rows.Count > 0 And dtCurrentTable.Rows.Count <= Maximo Then
                drCurrentRow = dtCurrentTable.NewRow()
                drCurrentRow("RowNumber") = dtCurrentTable.Rows.Count + 1

                'add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow)

                'Store the current data to ViewState for future reference   
                ViewState("NoAsalariados") = dtCurrentTable


                For i As Integer = 0 To dtCurrentTable.Rows.Count - 2


                    Dim boxComprobante1 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(2).FindControl("txtComprobante1"), TextBox)
                    dtCurrentTable.Rows(i)("Column1") = IIf(boxComprobante1.Text = "", 0, boxComprobante1.Text)

                    Dim boxComprobante2 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(3).FindControl("txtComprobante2"), TextBox)
                    dtCurrentTable.Rows(i)("Column2") = IIf(boxComprobante2.Text = "", 0, boxComprobante2.Text)

                    Dim boxComprobante3 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(4).FindControl("txtComprobante3"), TextBox)
                    dtCurrentTable.Rows(i)("Column3") = IIf(boxComprobante3.Text = "", 0, boxComprobante3.Text)

                    Dim boxComprobante4 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(5).FindControl("txtComprobante4"), TextBox)
                    dtCurrentTable.Rows(i)("Column4") = IIf(boxComprobante4.Text = "", 0, boxComprobante4.Text)

                    Dim boxComprobante5 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(6).FindControl("txtComprobante5"), TextBox)
                    dtCurrentTable.Rows(i)("Column5") = IIf(boxComprobante5.Text = "", 0, boxComprobante5.Text)


                Next

                If dtCurrentTable.Rows.Count > Maximo Then
                    dtCurrentTable.Rows(Maximo).Delete()
                    Master.MensajeError("Solo puede capturar " + Maximo.ToString + " Percepciones")
                End If

                'Rebind the Grid with the current data to reflect changes   
                grvNoAsalariados.DataSource = dtCurrentTable
                grvNoAsalariados.DataBind()
            Else
                'Master.MensajeError("Solo puede capturar 31 Percepciones")
            End If
        Else

            Response.Write("NoAsalariados is null")
        End If
        'Set Previous Data on Postbacks   
        SetPreviousDataNoAsalariados()
        TotalNoAsalariado()
    End Sub


    Private Sub SetPreviousDataAsalariados()

        Dim rowIndex As Integer = 0
        If ViewState("Asalariados") IsNot Nothing Then

            Dim dt As DataTable = DirectCast(ViewState("Asalariados"), DataTable)
            If dt.Rows.Count > 0 Then

                For i As Integer = 0 To dt.Rows.Count - 1


                    Dim boxComprobante1 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(1).FindControl("txtComprobante1"), TextBox)
                    Dim boxComprobante2 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(2).FindControl("txtComprobante2"), TextBox)
                    Dim boxComprobante3 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(3).FindControl("txtComprobante3"), TextBox)
                    Dim boxComprobante4 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(4).FindControl("txtComprobante4"), TextBox)
                    Dim boxComprobante5 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(5).FindControl("txtComprobante5"), TextBox)
                    Dim boxComprobante6 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(6).FindControl("txtComprobante6"), TextBox)
                    Dim boxComprobante7 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(7).FindControl("txtComprobante7"), TextBox)
                    Dim boxComprobante8 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(8).FindControl("txtComprobante8"), TextBox)
                    Dim boxComprobante9 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(9).FindControl("txtComprobante9"), TextBox)
                    Dim boxComprobante10 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(10).FindControl("txtComprobante10"), TextBox)
                    Dim boxComprobante11 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(11).FindControl("txtComprobante11"), TextBox)
                    Dim boxComprobante12 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(12).FindControl("txtComprobante12"), TextBox)
                    Dim boxComprobante13 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(13).FindControl("txtComprobante13"), TextBox)
                    Dim boxComprobante14 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(14).FindControl("txtComprobante14"), TextBox)
                    Dim boxComprobante15 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(15).FindControl("txtComprobante15"), TextBox)

                    If i < dt.Rows.Count - 1 Or i = Maximo Then

                        boxComprobante1.Text = dt.Rows(i)("Column1").ToString()
                        'boxComprobante1.Enabled = False

                        boxComprobante2.Text = dt.Rows(i)("Column2").ToString()
                        'boxComprobante2.Enabled = False

                        boxComprobante3.Text = dt.Rows(i)("Column3").ToString()
                        'boxComprobante3.Enabled = False

                        boxComprobante4.Text = dt.Rows(i)("Column4").ToString()
                        'boxComprobante4.Enabled = False

                        boxComprobante5.Text = dt.Rows(i)("Column5").ToString()


                        boxComprobante6.Text = dt.Rows(i)("Column6").ToString()


                        boxComprobante7.Text = dt.Rows(i)("Column7").ToString()


                        boxComprobante8.Text = dt.Rows(i)("Column8").ToString()


                        boxComprobante9.Text = dt.Rows(i)("Column9").ToString()


                        boxComprobante10.Text = dt.Rows(i)("Column10").ToString()


                        boxComprobante11.Text = dt.Rows(i)("Column11").ToString()


                        boxComprobante12.Text = dt.Rows(i)("Column12").ToString()


                        boxComprobante13.Text = dt.Rows(i)("Column13").ToString()


                        boxComprobante14.Text = dt.Rows(i)("Column14").ToString()


                        boxComprobante15.Text = dt.Rows(i)("Column15").ToString()

                    End If

                    rowIndex += 1
                Next
            End If
        End If
    End Sub

    Private Sub SetPreviousDataNoAsalariados()

        Dim rowIndex As Integer = 0
        If ViewState("NoAsalariados") IsNot Nothing Then

            Dim dt As DataTable = DirectCast(ViewState("NoAsalariados"), DataTable)
            If dt.Rows.Count > 0 Then

                For i As Integer = 0 To dt.Rows.Count - 1


                    Dim boxComprobante1 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(1).FindControl("txtComprobante1"), TextBox)
                    Dim boxComprobante2 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(2).FindControl("txtComprobante2"), TextBox)
                    Dim boxComprobante3 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(3).FindControl("txtComprobante3"), TextBox)
                    Dim boxComprobante4 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(4).FindControl("txtComprobante4"), TextBox)
                    Dim boxComprobante5 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(5).FindControl("txtComprobante5"), TextBox)


                    If i < dt.Rows.Count - 1 Or i = Maximo Then

                        boxComprobante1.Text = dt.Rows(i)("Column1").ToString()
                        'boxComprobante1.Enabled = False

                        boxComprobante2.Text = dt.Rows(i)("Column2").ToString()
                        'boxComprobante2.Enabled = False

                        boxComprobante3.Text = dt.Rows(i)("Column3").ToString()
                        'boxComprobante3.Enabled = False

                        boxComprobante4.Text = dt.Rows(i)("Column4").ToString()
                        'boxComprobante4.Enabled = False

                        boxComprobante5.Text = dt.Rows(i)("Column5").ToString()
                        'boxComprobante5.Enabled = False


                    End If

                    rowIndex += 1
                Next
            End If
        End If
    End Sub

    Public Sub TotalNoAsalariado()

        Try
            Dim footerRow As GridViewRow = grvNoAsalariados.FooterRow
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("NoAsalariados"), DataTable)
            Dim ds As New DataSet


            If Not dtCurrentTable Is Nothing Then
                ds.Tables.Add(dtCurrentTable)


                Dim Total As Decimal
                Dim TotalPercepcionNoAsalariado As Decimal
                Dim Menor As Decimal = 0
                Dim Mayor As Decimal = 0
                Dim Regla As Integer = 0

                footerRow.Cells(0).Text = "TOTAL: "

                'For i = 0 To grvNoAsalariados.Rows.Count - 1
                '    Dim boxTotal As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(1).FindControl("txtComprobante1"), TextBox)

                '    Total = Total + CDbl(IIf(boxTotal.Text = "", 0, boxTotal.Text))
                'Next

                Total = 0
                TotalPercepcionNoAsalariado = 0
                For i = 0 To ds.Tables(0).Columns.Count - 1
                    If i > 0 Then
                        For x = 0 To ds.Tables(0).Rows.Count - 1
                            Total = Total + CDbl(IIf(ds.Tables(0).Rows(x).Item(i).ToString = "", 0, ds.Tables(0).Rows(x).Item(i).ToString))
                        Next
                        footerRow.Cells(i).Text = CDbl(Total)
                        TotalPercepcionNoAsalariado = TotalPercepcionNoAsalariado + Total
                        If i <= ddlRecibosNoAsalariado.SelectedValue Then 'Mientras la seleccion de los recibos sea menor a i se evalua los campos del total de caso contrario se evaluarian los datos que vienen por defeault en 0.  BUG-PD-97 
                            If Total = 0 Then
                                Regla = 3
                            End If
                        End If


                        If Menor = 0 Or (Total > 0 And Total < Menor) Then
                            Menor = Total
                        End If

                        If Mayor = 0 Or (Total > 0 And Total > Mayor) Then
                            Mayor = Total
                        End If

                        Total = 0
                    End If
                Next

                Me.lblTotalPercepcionesNoAsalariado.Text = TotalPercepcionNoAsalariado
                Me.lblMayor.Text = Mayor
                Me.lblMenor.Text = Menor
                Me.lblRegla.Text = Regla

                'footerRow.Cells(1).Text = Format(Total, "##,##0.00")
            End If
        Catch ex As Exception
            SetInitialRowNoAsalariados()
        End Try




    End Sub

    Public Sub TotalAsalariado()
        Try
            Dim footerRow As GridViewRow = grvAsalariados.FooterRow
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("Asalariados"), DataTable)
            Dim ds As New DataSet

            ds.Tables.Add(dtCurrentTable)


            Dim Total As Decimal
            Dim TotalPercepcionAsalariado As Decimal

            footerRow.Cells(0).Text = "TOTAL: "

            'For i = 0 To grvAsalariados.Rows.Count - 1
            '    Dim boxTotal As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(1).FindControl("txtComprobante1"), TextBox)

            '    Total = Total + CDbl(IIf(boxTotal.Text = "", 0, boxTotal.Text))
            'Next

            Total = 0
            TotalPercepcionAsalariado = 0
            For i = 0 To ds.Tables(0).Columns.Count - 1
                If i > 0 Then
                    For x = 0 To ds.Tables(0).Rows.Count - 1
                        Total = Total + CDbl(IIf(ds.Tables(0).Rows(x).Item(i).ToString = "", 0, ds.Tables(0).Rows(x).Item(i).ToString)).ToString("F")
                    Next
                    footerRow.Cells(i).Text = CDbl(Total)
                    TotalPercepcionAsalariado = TotalPercepcionAsalariado + Total
                    Total = 0
                End If
            Next

            Me.lblTotalPercepcionesAsalariado.Text = TotalPercepcionAsalariado
        Catch ex As Exception
            SetInitialRowAsalariados()
        End Try



        'footerRow.Cells(1).Text = Format(Total, "##,##0.00")
    End Sub

    Protected Sub btnCalcularIngresos_Click(sender As Object, e As EventArgs)


        Me.lblTotalIngresosAsalariados.Text = ""
        Me.lblTotalIngresosNoAsalariados.Text = ""




        Dim BanderaAsalariado As Integer = 0
        Dim BanderaNoAsalariado As Integer = 0

        If ddlTipoActividad.SelectedValue <> 0 Then

            ''''''''''ASALARIADOS
            If ddlTipoReciboAsalariado.Enabled = True Then
                If ddlTipoReciboAsalariado.SelectedValue <> 0 Then
                    If ddlRecibosAsalariado.SelectedValue <> 0 Then
                        ActualizaDatosAsalariado()
                        CalculaAsalariado()
                    Else
                        Master.MensajeError("Debe seleccionar Recibos Asalariado para Calcular el Ingreso")
                        Exit Sub
                    End If

                Else
                    Master.MensajeError("Debe seleccionar Tipo de Recibo Asalariado para Calcular el Ingreso")
                    Exit Sub
                End If
            End If


            ''''''''''NO ASALARIADOS
            If ddlTipoReciboNoAsalariado.Enabled = True Then
                If ddlTipoReciboNoAsalariado.SelectedValue <> 0 Then
                    If ddlRecibosNoAsalariado.SelectedValue <> 0 Then
                        ActualizaDatosNoAsalariado()
                        CalculaNoAsalariado()
                    Else
                        Master.MensajeError("Debe seleccionar Recibos No Asalariado para Calcular el Ingreso")
                        Exit Sub
                    End If

                Else
                    Master.MensajeError("Debe seleccionar Tipo de Recibo No Asalariado para Calcular el Ingreso")
                    Exit Sub
                End If
            End If

        Else
            Master.MensajeError("Debe seleccionar Tipo de Actividad Económica")
            Exit Sub
        End If

        If Me.lblTotalIngresosAsalariados.Text = "" Then
            Me.lblTotalIngresosAsalariados.Text = 0
        End If

        If Me.lblTotalIngresosNoAsalariados.Text = "" Then
            Me.lblTotalIngresosNoAsalariados.Text = 0
        End If

        Me.txtTotalIngresos.Text = Convert.ToDouble(Me.lblTotalIngresosAsalariados.Text, CultureInfo.InvariantCulture) + Convert.ToDouble(lblTotalIngresosNoAsalariados.Text, CultureInfo.InvariantCulture)



        If txtTotalIngresos.Text <> "" And txtTotalIngresos.Text = "0" Then

            Master.MensajeError("Es necesario tener al menos un valor en los comprobantes.")

        Else
            Master.MensajeError("Cálculo realizado correctamente")
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable();", True)
        End If

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)

        Dim BanderaAsalariado As Integer = 0
        Dim BanderaNoAsalariado As Integer = 0



        Dim objFlujos As New clsCalculoIngreso()
        objFlujos.ID_SOLICITUD = lblSolicitud.Text
        objFlujos.CalculoIngreso(4)

        'If Me.ddlTipificaciones.SelectedValue = 0 Then
        '    Master.MensajeError("Debe Seleccionar una Tipificación")
        '    Exit Sub
        'End If


        If ddlTipoActividad.SelectedValue <> 0 Then

            ''''''''''ASALARIADOS
            If ddlTipoReciboAsalariado.Enabled = True Then
                If ddlTipoReciboAsalariado.SelectedValue <> 0 Then
                    If ddlRecibosAsalariado.SelectedValue <> 0 Then

                        ActualizaDatosAsalariado()
                        GuardaDatosAsalariado()
                    Else
                        Master.MensajeError("Debe seleccionar Recibos Asalariado para Calcular el Ingreso")
                        Exit Sub

                    End If


                Else
                    Master.MensajeError("Debe seleccionar Tipo de Recibo Asalariado para Calcular el Ingreso")
                    Exit Sub
                End If
            End If


            ''''''''''NO ASALARIADOS
            If ddlTipoReciboNoAsalariado.Enabled = True Then
                If ddlTipoReciboNoAsalariado.SelectedValue <> 0 Then
                    If ddlRecibosNoAsalariado.SelectedValue <> 0 Then

                        ActualizaDatosNoAsalariado()
                        GuardaDatosNoAsalariado()

                    Else
                        Master.MensajeError("Debe seleccionar Recibos No Asalariado para Calcular el Ingreso")
                        Exit Sub
                    End If

                Else
                    Master.MensajeError("Debe seleccionar Tipo de Recibo No Asalariado para Calcular el Ingreso")
                    Exit Sub
                End If

            End If

        Else
            Master.MensajeError("Debe seleccionar Tipo de Actividad Económica")
            Exit Sub
        End If

        If Me.lblTotalIngresosAsalariados.Text = "" Then
            Me.lblTotalIngresosAsalariados.Text = 0
        End If

        If Me.lblTotalIngresosNoAsalariados.Text = "" Then
            Me.lblTotalIngresosNoAsalariados.Text = 0
        End If

        'Me.txtTotalIngresos.Text = Format(CDbl(Me.lblTotalIngresosAsalariados.Text) + CDbl(lblTotalIngresosNoAsalariados.Text), "##,##0.00")

        Master.MensajeError("Información guardada correctamente")



        If txtTotalIngresos.Text <> "" Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable();", True)


        End If

    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        Me.ddlTipoActividad.SelectedValue = 0
        'Me.txtDiasPeriodoPago.Text = 0
        ddlTipoActividad_SelectedIndexChanged(sender, e)
        Me.lblMayor.Text = 0
        Me.lblMenor.Text = 0
        Me.lblTotalPercepcionesAsalariado.Text = 0
        Me.lblTotalPercepcionesNoAsalariado.Text = 0
        Me.lblTotalIngresosNoAsalariados.Text = 0
        Me.lblTotalIngresosAsalariados.Text = 0
        Me.lblRegla.Text = 0
        Me.txtTotalIngresos.Text = ""
        Me.ddlTipificaciones.SelectedValue = 0
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable1();", True)
    End Sub

    Protected Sub btnAgregarPercepcionNoAsalariado_Click(sender As Object, e As EventArgs)

        Dim count As Integer = 0


        For Each row As GridViewRow In grvNoAsalariados.Rows

            Dim boxComprobante1 As TextBox = TryCast(row.FindControl("txtComprobante1"), TextBox)
            Dim boxComprobante2 As TextBox = TryCast(row.FindControl("txtComprobante2"), TextBox)
            Dim boxComprobante3 As TextBox = TryCast(row.FindControl("txtComprobante3"), TextBox)
            Dim boxComprobante4 As TextBox = TryCast(row.FindControl("txtComprobante4"), TextBox)
            Dim boxComprobante5 As TextBox = TryCast(row.FindControl("txtComprobante5"), TextBox)


            Dim Comprobante1 As Double = IIf(boxComprobante1.Text = "", 0, boxComprobante1.Text)
            Dim Comprobante2 As Double = IIf(boxComprobante2.Text = "", 0, boxComprobante2.Text)
            Dim Comprobante3 As Double = IIf(boxComprobante3.Text = "", 0, boxComprobante3.Text)
            Dim Comprobante4 As Double = IIf(boxComprobante4.Text = "", 0, boxComprobante4.Text)
            Dim Comprobante5 As Double = IIf(boxComprobante5.Text = "", 0, boxComprobante5.Text)

            If Comprobante1 = 0 And Comprobante2 = 0 And Comprobante3 = 0 And Comprobante4 = 0 And Comprobante5 = 0 Then
                count = 0
            End If


        Next

        If count > 0 Then
            'LimpiaError()
            Master.MensajeError("No se pueden ingresar todos los comprobantes sin valor")
            Exit Sub
        Else
            AddNewRowToGridNoAsalariados()
        End If

    End Sub

    Protected Sub btnAgregarPercepcionAsalariados_Click(sender As Object, e As EventArgs)

        Dim count As Integer = 0


        For Each row As GridViewRow In grvAsalariados.Rows

            Dim boxComprobante1 As TextBox = TryCast(row.FindControl("txtComprobante1"), TextBox)
            Dim boxComprobante2 As TextBox = TryCast(row.FindControl("txtComprobante2"), TextBox)
            Dim boxComprobante3 As TextBox = TryCast(row.FindControl("txtComprobante3"), TextBox)
            Dim boxComprobante4 As TextBox = TryCast(row.FindControl("txtComprobante4"), TextBox)
            Dim boxComprobante5 As TextBox = TryCast(row.FindControl("txtComprobante5"), TextBox)
            Dim boxComprobante6 As TextBox = TryCast(row.FindControl("txtComprobante6"), TextBox)
            Dim boxComprobante7 As TextBox = TryCast(row.FindControl("txtComprobante7"), TextBox)
            Dim boxComprobante8 As TextBox = TryCast(row.FindControl("txtComprobante8"), TextBox)
            Dim boxComprobante9 As TextBox = TryCast(row.FindControl("txtComprobante9"), TextBox)
            Dim boxComprobante10 As TextBox = TryCast(row.FindControl("txtComprobante10"), TextBox)
            Dim boxComprobante11 As TextBox = TryCast(row.FindControl("txtComprobante11"), TextBox)
            Dim boxComprobante12 As TextBox = TryCast(row.FindControl("txtComprobante12"), TextBox)
            Dim boxComprobante13 As TextBox = TryCast(row.FindControl("txtComprobante13"), TextBox)
            Dim boxComprobante14 As TextBox = TryCast(row.FindControl("txtComprobante14"), TextBox)
            Dim boxComprobante15 As TextBox = TryCast(row.FindControl("txtComprobante15"), TextBox)

            Dim Comprobante1 As Double = IIf(boxComprobante1.Text = "", 0, boxComprobante1.Text)
            Dim Comprobante2 As Double = IIf(boxComprobante2.Text = "", 0, boxComprobante2.Text)
            Dim Comprobante3 As Double = IIf(boxComprobante3.Text = "", 0, boxComprobante3.Text)
            Dim Comprobante4 As Double = IIf(boxComprobante4.Text = "", 0, boxComprobante4.Text)
            Dim Comprobante5 As Double = IIf(boxComprobante5.Text = "", 0, boxComprobante5.Text)
            Dim Comprobante6 As Double = IIf(boxComprobante6.Text = "", 0, boxComprobante6.Text)
            Dim Comprobante7 As Double = IIf(boxComprobante7.Text = "", 0, boxComprobante7.Text)
            Dim Comprobante8 As Double = IIf(boxComprobante8.Text = "", 0, boxComprobante8.Text)
            Dim Comprobante9 As Double = IIf(boxComprobante9.Text = "", 0, boxComprobante9.Text)
            Dim Comprobante10 As Double = IIf(boxComprobante10.Text = "", 0, boxComprobante10.Text)
            Dim Comprobante11 As Double = IIf(boxComprobante11.Text = "", 0, boxComprobante11.Text)
            Dim Comprobante12 As Double = IIf(boxComprobante12.Text = "", 0, boxComprobante12.Text)
            Dim Comprobante13 As Double = IIf(boxComprobante13.Text = "", 0, boxComprobante13.Text)
            Dim Comprobante14 As Double = IIf(boxComprobante14.Text = "", 0, boxComprobante14.Text)
            Dim Comprobante15 As Double = IIf(boxComprobante15.Text = "", 0, boxComprobante15.Text)

            If Comprobante1 = 0 And Comprobante2 = 0 And Comprobante3 = 0 And Comprobante4 = 0 And Comprobante5 = 0 Then
                'count = count + 1
                count = 0
            End If


        Next

        If count > 0 Then
            'LimpiaError()
            Master.MensajeError("No se pueden ingresar todos los comprobantes sin valor")
            Exit Sub
        Else
            AddNewRowToGridAsalariados()
        End If

    End Sub

    Public Sub CalculaAsalariado()

        lblTotalIngresosAsalariados.Text = 0

        'If Val(txtDiasPeriodoPago.Text) = 0 Then
        '    Master.MensajeError("Los Días que Comprende el Periódo de Pago debe ser Mayor a 0")
        '    txtDiasPeriodoPago.Focus()
        '    Exit Sub
        'End If

        If ddlTipoReciboAsalariado.SelectedValue = 252 Then

            If ddlPeriodoPago.SelectedValue = 0 Then
                Master.MensajeError("Debe seleccionar Periodo De Pago")
                Exit Sub
            End If

            TotalAsalariado()
            Me.lblTotalIngresosAsalariados.Text = CDbl(((lblTotalPercepcionesAsalariado.Text / ddlRecibosAsalariado.SelectedValue) / Me.lblDiasPeriodo.Text) * 30.4).ToString("F")

        Else

            TotalAsalariado()
            Me.lblTotalIngresosAsalariados.Text = CDbl((lblTotalPercepcionesAsalariado.Text / ddlRecibosAsalariado.SelectedValue) * 1.25).ToString("F") 'BUG-PD-97  SE MULTIPLICA POR 1.25 EN EL TIPO DE RECIBO ASALARIADO "ESTADO DE CUENTA"

        End If


    End Sub
    Public Sub CalculaNoAsalariado()

        'If txtDiasPeriodoPago.Text = 0 Then
        '    Master.MensajeError("Los Días que Comprende el Periódo de Pago debe ser Mayor a 0")
        '    txtDiasPeriodoPago.Focus()
        '    Exit Sub
        'End If
        lblTotalIngresosNoAsalariados.Text = 0



        TotalNoAsalariado()
        Dim resultado As Decimal
        resultado = (lblMenor.Text / lblMayor.Text) ' Comprobamos el resultado para verificar la relgla de 0.75 

		 If ddlTipoReciboNoAsalariado.SelectedValue = 256 Then

            Me.lblTotalIngresosNoAsalariados.Text = CDbl((lblTotalPercepcionesNoAsalariado.Text / ddlRecibosNoAsalariado.SelectedValue)).ToString("F")
            Exit Sub
        End If	
        If lblRegla.Text <> 3 Then 'Si la regla es diferenta a 3 en este caso la regla 3 es un monto de un comprobante  en 0 
            If (lblMenor.Text / lblMayor.Text) < 0.75 Then
                Me.lblTotalIngresosNoAsalariados.Text = (CDbl(lblMenor.Text)).ToString("F")
            Else
                Me.lblTotalIngresosNoAsalariados.Text = CDbl((lblTotalPercepcionesNoAsalariado.Text / ddlRecibosNoAsalariado.SelectedValue)).ToString("F")
            End If
        Else
            Me.lblTotalIngresosNoAsalariados.Text = CDbl((lblTotalPercepcionesNoAsalariado.Text / ddlRecibosNoAsalariado.SelectedValue)).ToString("F")
			Exit Sub
        End If




    End Sub
    Public Sub GuardaDatosAsalariado()
        Dim objFlujos As New clsCalculoIngreso()

        'TotalAsalariado()
        'TotalNoAsalariado()
        If Me.lblTotalIngresosAsalariados.Text = "" Then
            Me.lblTotalIngresosAsalariados.Text = 0
        End If
        If Me.lblTotalIngresosNoAsalariados.Text = "" Then
            Me.lblTotalIngresosNoAsalariados.Text = 0
        End If
        If Me.txtTotalIngresos.Text = "" Then
            Me.txtTotalIngresos.Text = 0
        End If


        objFlujos.ID_SOLICITUD = lblSolicitud.Text
        objFlujos.TipoActividad = Me.ddlTipoActividad.SelectedValue
        objFlujos.PeriodoPago = Me.ddlPeriodoPago.SelectedValue
        'objFlujos.DiasPerPago = IIf(Me.txtDiasPeriodoPago.Text = "", 0, Me.txtDiasPeriodoPago.Text)

        objFlujos.IngresoAsalariado = Convert.ToDouble(Me.lblTotalIngresosAsalariados.Text)
        objFlujos.IngresoNoAsalariado = Convert.ToDouble(Me.lblTotalIngresosNoAsalariados.Text)

        objFlujos.TipoReciboAsalariado = Me.ddlTipoReciboAsalariado.SelectedValue
        objFlujos.TipoReciboNoAsalariado = Me.ddlTipoReciboNoAsalariado.SelectedValue

        objFlujos.RecibosAsalariado = Me.ddlRecibosAsalariado.SelectedValue
        objFlujos.RecibosNoAsalariado = Me.ddlRecibosNoAsalariado.SelectedValue
        objFlujos.Tipificacion = Me.ddlTipificaciones.SelectedValue
        objFlujos.TOTAL = Convert.ToDouble(txtTotalIngresos.Text)
        objFlujos.Usuario = 1

        objFlujos.CalculoIngreso(2)



        Dim dtCurrentTableAsalariado As DataTable = DirectCast(ViewState("Asalariados"), DataTable)
        Dim dsAsalariado As New DataSet

        Dim Comprobante1 As Double
        Dim Comprobante2 As Double
        Dim Comprobante3 As Double
        Dim Comprobante4 As Double
        Dim Comprobante5 As Double
        Dim Comprobante6 As Double
        Dim Comprobante7 As Double
        Dim Comprobante8 As Double
        Dim Comprobante9 As Double
        Dim Comprobante10 As Double
        Dim Comprobante11 As Double
        Dim Comprobante12 As Double
        Dim Comprobante13 As Double
        Dim Comprobante14 As Double
        Dim Comprobante15 As Double
        Dim NumPercepcion As Integer = 0

        If Not dtCurrentTableAsalariado Is Nothing Then
            dsAsalariado.Tables.Add(dtCurrentTableAsalariado)

            If dsAsalariado.Tables.Count > 0 Then
                If dsAsalariado.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsAsalariado.Tables(0).Rows.Count - 1
                        NumPercepcion = dsAsalariado.Tables(0).Rows(i).Item("RowNumber").ToString
                        Comprobante1 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column1").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column1").ToString))
                        Comprobante2 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column2").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column2").ToString))
                        Comprobante3 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column3").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column3").ToString))
                        Comprobante4 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column4").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column4").ToString))
                        Comprobante5 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column5").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column5").ToString))
                        Comprobante6 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column6").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column6").ToString))
                        Comprobante7 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column7").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column7").ToString))
                        Comprobante8 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column8").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column8").ToString))
                        Comprobante9 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column9").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column9").ToString))
                        Comprobante10 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column10").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column10").ToString))
                        Comprobante11 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column11").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column11").ToString))
                        Comprobante12 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column12").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column12").ToString))
                        Comprobante13 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column13").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column13").ToString))
                        Comprobante14 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column14").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column14").ToString))
                        Comprobante15 = CDbl(IIf((dsAsalariado.Tables(0).Rows(i).Item("column15").ToString) = "", 0, dsAsalariado.Tables(0).Rows(i).Item("column15").ToString))

                        If Comprobante1 > 0 Or Comprobante2 > 0 Or Comprobante3 > 0 Or Comprobante4 > 0 Or Comprobante5 > 0 Or Comprobante6 > 0 Or Comprobante7 > 0 Or Comprobante8 > 0 Or Comprobante9 > 0 Or Comprobante10 > 0 Or Comprobante11 > 0 Or Comprobante12 > 0 Or Comprobante13 > 0 Or Comprobante14 > 0 Or Comprobante15 > 0 Then
                            objFlujos.ID_SOLICITUD = lblSolicitud.Text
                            objFlujos.TipoActividad = 248
                            objFlujos.Comprobante1 = Comprobante1
                            objFlujos.Comprobante2 = Comprobante2
                            objFlujos.Comprobante3 = Comprobante3
                            objFlujos.Comprobante4 = Comprobante4
                            objFlujos.Comprobante5 = Comprobante5
                            objFlujos.Comprobante6 = Comprobante6
                            objFlujos.Comprobante7 = Comprobante7
                            objFlujos.Comprobante8 = Comprobante8
                            objFlujos.Comprobante9 = Comprobante9
                            objFlujos.Comprobante10 = Comprobante10
                            objFlujos.Comprobante11 = Comprobante11
                            objFlujos.Comprobante12 = Comprobante12
                            objFlujos.Comprobante13 = Comprobante13
                            objFlujos.Comprobante14 = Comprobante14
                            objFlujos.Comprobante15 = Comprobante15

                            objFlujos.Percepcion = NumPercepcion


                            objFlujos.Usuario = 1

                            objFlujos.CalculoIngreso(3)

                        End If
                    Next
                End If
            End If
        End If


    End Sub

    Public Sub GuardaDatosNoAsalariado()

        Dim objFlujos As New clsCalculoIngreso()

        If Me.lblTotalIngresosAsalariados.Text = "" Then
            Me.lblTotalIngresosAsalariados.Text = 0
        End If
        If Me.lblTotalIngresosNoAsalariados.Text = "" Then
            Me.lblTotalIngresosNoAsalariados.Text = 0
        End If
        If Me.txtTotalIngresos.Text = "" Then
            Me.txtTotalIngresos.Text = 0
        End If

        objFlujos.ID_SOLICITUD = lblSolicitud.Text
        objFlujos.TipoActividad = Me.ddlTipoActividad.SelectedValue
        objFlujos.PeriodoPago = Me.ddlPeriodoPago.SelectedValue
        'objFlujos.DiasPerPago = IIf(Me.txtDiasPeriodoPago.Text = "", 0, Me.txtDiasPeriodoPago.Text)

        objFlujos.IngresoAsalariado = Convert.ToDouble(Me.lblTotalIngresosAsalariados.Text)
        objFlujos.IngresoNoAsalariado = Convert.ToDouble(Me.lblTotalIngresosNoAsalariados.Text)

        objFlujos.TipoReciboAsalariado = Me.ddlTipoReciboAsalariado.SelectedValue
        objFlujos.TipoReciboNoAsalariado = Me.ddlTipoReciboNoAsalariado.SelectedValue

        objFlujos.RecibosAsalariado = Me.ddlRecibosAsalariado.SelectedValue
        objFlujos.RecibosNoAsalariado = Me.ddlRecibosNoAsalariado.SelectedValue
        objFlujos.Tipificacion = Me.ddlTipificaciones.SelectedValue
        objFlujos.TOTAL = Convert.ToDouble(txtTotalIngresos.Text, CultureInfo.InvariantCulture)
        objFlujos.Usuario = 1

        objFlujos.CalculoIngreso(2)


        Dim dtCurrentTableNoAsalariado As DataTable = DirectCast(ViewState("NoAsalariados"), DataTable)
        Dim dsNoAsalariado As New DataSet

        Dim Comprobante1 As Double
        Dim Comprobante2 As Double
        Dim Comprobante3 As Double
        Dim Comprobante4 As Double
        Dim Comprobante5 As Double
        Dim NumPercepcion As Integer = 0

        If Not dtCurrentTableNoAsalariado Is Nothing Then
            dsNoAsalariado.Tables.Add(dtCurrentTableNoAsalariado)

            If dsNoAsalariado.Tables.Count > 0 Then
                If dsNoAsalariado.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsNoAsalariado.Tables(0).Rows.Count - 1
                        NumPercepcion = dsNoAsalariado.Tables(0).Rows(i).Item("RowNumber").ToString
                        Comprobante1 = CDbl(IIf((dsNoAsalariado.Tables(0).Rows(i).Item("column1").ToString) = "", 0, dsNoAsalariado.Tables(0).Rows(i).Item("column1").ToString))
                        Comprobante2 = CDbl(IIf((dsNoAsalariado.Tables(0).Rows(i).Item("column2").ToString) = "", 0, dsNoAsalariado.Tables(0).Rows(i).Item("column2").ToString))
                        Comprobante3 = CDbl(IIf((dsNoAsalariado.Tables(0).Rows(i).Item("column3").ToString) = "", 0, dsNoAsalariado.Tables(0).Rows(i).Item("column3").ToString))
                        Comprobante4 = CDbl(IIf((dsNoAsalariado.Tables(0).Rows(i).Item("column4").ToString) = "", 0, dsNoAsalariado.Tables(0).Rows(i).Item("column4").ToString))
                        Comprobante5 = CDbl(IIf((dsNoAsalariado.Tables(0).Rows(i).Item("column5").ToString) = "", 0, dsNoAsalariado.Tables(0).Rows(i).Item("column5").ToString))

                        If Comprobante1 > 0 Or Comprobante2 > 0 Or Comprobante3 > 0 Or Comprobante4 > 0 Or Comprobante5 > 0 Then
                            objFlujos.ID_SOLICITUD = lblSolicitud.Text
                            objFlujos.TipoActividad = 249
                            objFlujos.Comprobante1 = Comprobante1
                            objFlujos.Comprobante2 = Comprobante2
                            objFlujos.Comprobante3 = Comprobante3
                            objFlujos.Comprobante4 = Comprobante4
                            objFlujos.Comprobante5 = Comprobante5

                            objFlujos.Percepcion = NumPercepcion


                            objFlujos.Usuario = 1

                            objFlujos.CalculoIngreso(3)

                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Protected Sub btnVisor_Click(sender As Object, e As EventArgs)


        Dim ds As DataSet
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos

        objCatalogos.Parametro = Me.lblSolicitud.Text
        ds = objCatalogos.Catalogos(7)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Dim Cliente_bbva As String = String.Empty 'BUG-PD-97  EL TIPO DE DATO ES UNA CADENA ANTERIORMENTE ESTA DEFINIDO COMO INT.    

                Cliente_bbva = ds.Tables(0).Rows(0).Item("CLIENTE_BBVA")

                If Cliente_bbva <> "" Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "&cliente=" + Cliente_bbva.ToString + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)

                End If
            End If
        End If



    End Sub

    Protected Sub ddlPeriodoPago_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlPeriodoPago.SelectedValue > 0 Then
            Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
            Dim ds As DataSet
            objCatalogos.Parametro = ddlPeriodoPago.SelectedValue
            ds = objCatalogos.Catalogos(5)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.lblDiasPeriodo.Text = ds.Tables(0).Rows(0).Item("PDK_PAR_SIS_VALOR_TEXTO").ToString
                End If
            End If

        End If
    End Sub
    Public Sub ActualizaDatosAsalariado()

        If ViewState("Asalariados") IsNot Nothing Then
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("Asalariados"), DataTable)
            Dim drCurrentRow As DataRow = Nothing

            If dtCurrentTable.Rows.Count > 0 And dtCurrentTable.Rows.Count <= Maximo Then
                drCurrentRow = dtCurrentTable.NewRow()
                'drCurrentRow("RowNumber") = dtCurrentTable.Rows.Count + 1

                'add new row to DataTable   
                'dtCurrentTable.Rows.Add(drCurrentRow)

                'Store the current data to ViewState for future reference   
                ViewState("Asalariados") = dtCurrentTable


                For i As Integer = 0 To dtCurrentTable.Rows.Count - 1


                    Dim boxComprobante1 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(2).FindControl("txtComprobante1"), TextBox)
                    dtCurrentTable.Rows(i)("Column1") = IIf(boxComprobante1.Text = "", 0, boxComprobante1.Text)

                    Dim boxComprobante2 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(3).FindControl("txtComprobante2"), TextBox)
                    dtCurrentTable.Rows(i)("Column2") = IIf(boxComprobante2.Text = "", 0, boxComprobante2.Text)

                    Dim boxComprobante3 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(4).FindControl("txtComprobante3"), TextBox)
                    dtCurrentTable.Rows(i)("Column3") = IIf(boxComprobante3.Text = "", 0, boxComprobante3.Text)

                    Dim boxComprobante4 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(5).FindControl("txtComprobante4"), TextBox)
                    dtCurrentTable.Rows(i)("Column4") = IIf(boxComprobante4.Text = "", 0, boxComprobante4.Text)

                    Dim boxComprobante5 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(6).FindControl("txtComprobante5"), TextBox)
                    dtCurrentTable.Rows(i)("Column5") = IIf(boxComprobante5.Text = "", 0, boxComprobante5.Text)

                    Dim boxComprobante6 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(2).FindControl("txtComprobante6"), TextBox)
                    dtCurrentTable.Rows(i)("Column6") = IIf(boxComprobante6.Text = "", 0, boxComprobante6.Text)

                    Dim boxComprobante7 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(3).FindControl("txtComprobante7"), TextBox)
                    dtCurrentTable.Rows(i)("Column7") = IIf(boxComprobante7.Text = "", 0, boxComprobante7.Text)

                    Dim boxComprobante8 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(4).FindControl("txtComprobante8"), TextBox)
                    dtCurrentTable.Rows(i)("Column8") = IIf(boxComprobante8.Text = "", 0, boxComprobante8.Text)

                    Dim boxComprobante9 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(5).FindControl("txtComprobante9"), TextBox)
                    dtCurrentTable.Rows(i)("Column9") = IIf(boxComprobante9.Text = "", 0, boxComprobante9.Text)

                    Dim boxComprobante10 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(6).FindControl("txtComprobante10"), TextBox)
                    dtCurrentTable.Rows(i)("Column10") = IIf(boxComprobante10.Text = "", 0, boxComprobante10.Text)

                    Dim boxComprobante11 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(2).FindControl("txtComprobante11"), TextBox)
                    dtCurrentTable.Rows(i)("Column11") = IIf(boxComprobante11.Text = "", 0, boxComprobante11.Text)

                    Dim boxComprobante12 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(3).FindControl("txtComprobante12"), TextBox)
                    dtCurrentTable.Rows(i)("Column12") = IIf(boxComprobante12.Text = "", 0, boxComprobante12.Text)

                    Dim boxComprobante13 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(4).FindControl("txtComprobante13"), TextBox)
                    dtCurrentTable.Rows(i)("Column13") = IIf(boxComprobante13.Text = "", 0, boxComprobante13.Text)

                    Dim boxComprobante14 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(5).FindControl("txtComprobante14"), TextBox)
                    dtCurrentTable.Rows(i)("Column14") = IIf(boxComprobante14.Text = "", 0, boxComprobante14.Text)

                    Dim boxComprobante15 As TextBox = DirectCast(grvAsalariados.Rows(i).Cells(6).FindControl("txtComprobante15"), TextBox)
                    dtCurrentTable.Rows(i)("Column15") = IIf(boxComprobante15.Text = "", 0, boxComprobante15.Text)


                Next

                'Rebind the Grid with the current data to reflect changes   
                grvAsalariados.DataSource = dtCurrentTable
                grvAsalariados.DataBind()
            End If
        End If
    End Sub

    Public Sub ActualizaDatosNoAsalariado()
        If ViewState("NoAsalariados") IsNot Nothing Then

            Dim dtCurrentTable As DataTable = DirectCast(ViewState("NoAsalariados"), DataTable)
            Dim drCurrentRow As DataRow = Nothing

            If dtCurrentTable.Rows.Count > 0 And dtCurrentTable.Rows.Count <= Maximo Then
                drCurrentRow = dtCurrentTable.NewRow()
                'drCurrentRow("RowNumber") = dtCurrentTable.Rows.Count + 1

                'add new row to DataTable   
                'dtCurrentTable.Rows.Add(drCurrentRow)

                'Store the current data to ViewState for future reference   
                ViewState("NoAsalariados") = dtCurrentTable


                For i As Integer = 0 To dtCurrentTable.Rows.Count - 1


                    Dim boxComprobante1 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(2).FindControl("txtComprobante1"), TextBox)
                    dtCurrentTable.Rows(i)("Column1") = IIf(boxComprobante1.Text = "", 0, boxComprobante1.Text)

                    Dim boxComprobante2 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(3).FindControl("txtComprobante2"), TextBox)
                    dtCurrentTable.Rows(i)("Column2") = IIf(boxComprobante2.Text = "", 0, boxComprobante2.Text)

                    Dim boxComprobante3 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(4).FindControl("txtComprobante3"), TextBox)
                    dtCurrentTable.Rows(i)("Column3") = IIf(boxComprobante3.Text = "", 0, boxComprobante3.Text)

                    Dim boxComprobante4 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(5).FindControl("txtComprobante4"), TextBox)
                    dtCurrentTable.Rows(i)("Column4") = IIf(boxComprobante4.Text = "", 0, boxComprobante4.Text)

                    Dim boxComprobante5 As TextBox = DirectCast(grvNoAsalariados.Rows(i).Cells(6).FindControl("txtComprobante5"), TextBox)
                    dtCurrentTable.Rows(i)("Column5") = IIf(boxComprobante5.Text = "", 0, boxComprobante5.Text)


                Next



                'Rebind the Grid with the current data to reflect changes   
                grvNoAsalariados.DataSource = dtCurrentTable
                grvNoAsalariados.DataBind()
            End If
        End If
    End Sub


    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs)

        Dim objFlujos As New clsCalculoIngreso()
        objFlujos.ID_SOLICITUD = lblSolicitud.Text

        objFlujos.CalculoIngreso(4)

        If Me.ddlTipificaciones.SelectedValue = 0 Then
            Master.MensajeError("Debe Seleccionar una Tipificación")
            Exit Sub
        End If

        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty



        Dim objpant As New ProdeskNet.SN.clsPantallas()
        Dim ds_siguienteTarea As DataSet
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos

        Try
            objCatalogos.Parametro = Request("idPantalla")
            ds_siguienteTarea = objCatalogos.Catalogos(6)

            If ds_siguienteTarea.Tables.Count > 0 Then
                If ds_siguienteTarea.Tables(0).Rows.Count > 0 Then
                    If Me.ddlTipificaciones.SelectedValue = 2 Or Me.ddlTipificaciones.SelectedValue = 3 Then
                        Dim clssolrech As New clsCuestionarioSolvsID
                        clssolrech._ID_SOLICITUD = Val(Request("Sol"))
                        clssolrech._ID_DOC = 5 'Comprobante de ingresos
                        clssolrech._ID_PANT = 74
                        clssolrech._ID_USER = Session("IdUsua")
                        clssolrech._ACTION = 1
                        clssolrech._MOT_RECH = IIf(ddlTipificaciones.SelectedValue = 2, 274, 275)
                        If ddlRecibosAsalariado.SelectedValue <> 0 Then
                            ActualizaDatosAsalariado()
                            GuardaDatosAsalariado()
                        End If
                        If ddlRecibosNoAsalariado.SelectedValue <> 0 Then
                            ActualizaDatosNoAsalariado()
                            GuardaDatosNoAsalariado()
                        End If
                        If Not clssolrech.ValidaID() Then
                            Throw New Exception(clssolrech.StrError)
                        End If

                        asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))
                    ElseIf Me.ddlTipificaciones.SelectedValue = 1 Then
                        If ddlTipoActividad.SelectedValue <> 0 Then
                            ''''''''''ASALARIADOS
                            If ddlTipoReciboAsalariado.Enabled = True Then
                                If ddlTipoReciboAsalariado.SelectedValue <> 0 Then
                                    If ddlRecibosAsalariado.SelectedValue <> 0 Then
                                        ActualizaDatosAsalariado()
                                        GuardaDatosAsalariado()
                                    Else
                                        Master.MensajeError("Debe seleccionar Recibos Asalariado para Calcular el Ingreso")
                                        Exit Sub
                                    End If

                                Else
                                    Master.MensajeError("Debe seleccionar Tipo de Recibo Asalariado para Calcular el Ingreso")
                                    Exit Sub
                                End If
                            End If



                            ''''''''''NO ASALARIADOS
                            If ddlTipoReciboNoAsalariado.Enabled = True Then
                                If ddlTipoReciboNoAsalariado.SelectedValue <> 0 Then
                                    If ddlRecibosNoAsalariado.SelectedValue <> 0 Then
                                        ActualizaDatosNoAsalariado()
                                        GuardaDatosNoAsalariado()
                                    Else
                                        Master.MensajeError("Debe seleccionar Recibos No Asalariado para Calcular el Ingreso")
                                        Exit Sub
                                    End If

                                Else
                                    Master.MensajeError("Debe seleccionar Tipo de Recibo No Asalariado para Calcular el Ingreso")
                                    Exit Sub
                                End If

                            End If

                        Else
                            Master.MensajeError("Debe seleccionar Tipo de Actividad Económica")
                            Exit Sub
                        End If
                        asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))

                    End If

                End If
            End If

            If Me.ddlTipificaciones.SelectedValue >= 4 Then
                Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
                Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
                Solicitudes.Estatus = 47
                Solicitudes.Comentario = ddlTipificaciones.SelectedItem.Text
                Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
                Solicitudes.ManejaTarea(6)

                If ddlRecibosAsalariado.SelectedValue <> 0 Then
                    ActualizaDatosAsalariado()
                    GuardaDatosAsalariado()
                End If
                If ddlRecibosNoAsalariado.SelectedValue <> 0 Then
                    ActualizaDatosNoAsalariado()
                    GuardaDatosNoAsalariado()

                End If

                Master.MsjErrorRedirect("Solicitud Declinada", "../aspx/consultaPanelControl.aspx") 'BUG-PD-97 

            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable();", True)
            Master.MensajeError(mensaje)
        End Try


    End Sub
    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Dim ds_validardocumento As DataSet
        Dim BD As New clsManejaBD
        Dim Mensaje1 As String
        BD.AgregaParametro("@FOLIO", TipoDato.Entero, hdSolicitud.Value)
        BD.AgregaParametro("@BANDERA", TipoDato.Entero, 2)
        BD.AgregaParametro("@PANTALLA", TipoDato.Entero, hdPantalla.Value)


        ds_validardocumento = BD.EjecutaStoredProcedure("sp_validarEntrevista")
        Mensaje1 = ds_validardocumento.Tables(0).Rows(0).Item("MENSAJE")
        If Mensaje1 <> "" Then
            Master.MensajeError(Mensaje1)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable();", True)
            Exit Sub
        Else

        End If
        Try        
            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                Response.Redirect("../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If

            'BD.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

            If muestrapant = 0 Then
                'Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                If (ddlTipificaciones.SelectedValue = 2 Or ddlTipificaciones.SelectedValue = 3) Then
                    ClsEmail.OPCION = 17
                    ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

                    Dim dtConsulta = New DataSet()
                    dtConsulta = ClsEmail.ConsultaStatusNotificacion
                    If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
                        If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                            If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
                                If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
                                    Dim strLocation As String = String.Empty
                                    strLocation = ("../aspx/ValidaEmails.aspx?idPantalla=80&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=0" & "&usuario=" & usu)
                                    'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '" + strLocation + "');", True)
                                Else
                                    Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                                End If
                            Else
                                Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                            End If
                        Else

                        End If
                    Else
                        'Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString, False)
                        Dim strPath As String = "../aspx/" + dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() + "?idPantalla=" + dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() + "&sol=" + Val(Request("Sol")).ToString() + "&usuario=" + Val(Request("usuario")).ToString()
                        ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('', '" + strPath + "');", True)
                    End If
                Else
                    'Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString, False)
                    Dim strPath As String = "../aspx/" + dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() + "?idPantalla=" + dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() + "&sol=" + Val(Request("Sol")).ToString() + "&usuario=" + Val(Request("usuario")).ToString()
                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('', '" + strPath + "');", True)
                End If

            ElseIf muestrapant = 2 Then

                If (ddlTipificaciones.SelectedValue = 2 Or ddlTipificaciones.SelectedValue = 3) Then
                    ClsEmail.OPCION = 17
                    ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

                    Dim dtConsulta = New DataSet()
                    dtConsulta = ClsEmail.ConsultaStatusNotificacion
                    If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
                        If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                            If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
                                If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
                                    Dim strLocation As String = String.Empty
                                    strLocation = ("../aspx/ValidaEmails.aspx?idPantalla=80&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=2" & "&usuario=" & usu)
                                    'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '" + strLocation + "');", True)
                                Else
                                    Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                                End If
                            Else
                                Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                            End If
                        Else
                            Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                        End If
                    Else
                        Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                        ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                    End If
                Else
                    'Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx');", True)  'RHERNANDEZ
                End If
            End If
        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub
    Public Sub RegresaPantalla()
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & Request("sol") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        RegresaPantalla()
    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs)

        Dim button As Button = sender
        'Dim row As GridViewRow = button.NamingContainer
        'Dim index As Integer = row.RowIndex
        Try

        
        Dim Fname As String
        Dim crReportDocument As ReportDocument
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim Ds_1 As New DataSet


        'Dim _Chk As CheckBox = CType(sender, CheckBox)
        Dim Sol = Convert.ToInt32(Me.lblSolicitud.Text)



        Dim strRuta As String = Server.MapPath("..\Reporte\CalculoIngresos.rpt")

        Dim objFlujos As New clsCalculoIngreso()
        Dim ds As New DataSet

        objFlujos.ID_SOLICITUD = Sol
        objFlujos.Usuario = Convert.ToInt32(hdusuario.Value)

        ds = objFlujos.CalculoIngreso(5)

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



        ds.Tables(0).TableName = "PDK_CALCULO_INGRESOS"
        ds.Tables(1).TableName = "PDK_MATRIZ_INGRESOS_A"
        ds.Tables(2).TableName = "PDK_MATRIZ_INGRESOS_NA"
        'Ds_1.Tables.Add(ds.Tables(0).Copy())
        'Ds_1.Tables.Add(ds.Tables(1).Copy())
        'Ds_1.Tables.Add(ds.Tables(2).Copy())
        'Ds_1.Tables(0).TableName = "PDK_CALCULO_INGRESOS"
        'Ds_1.Tables(1).TableName = "PDK_MATRIZ_INGRESOS_A"
        'Ds_1.Tables(2).TableName = "PDK_MATRIZ_INGRESOS_NA"


        crReportDocument.SetDataSource(ds)

        crReportDocument.Export()

        Response.ClearContent()
        Response.ClearHeaders()
        Response.ContentType = "application/pdf"
            Response.Redirect("./Descargapdf.aspx?fname=" & Fname)
        Response.End()


        crReportDocument.Close()
        crReportDocument.Dispose()


        



        'Response.Redirect("./Descargapdf.aspx?fname=" & Fname)

        Catch ex As Exception
            Dim mensaje As String = ex.Message
        End Try
    End Sub

End Class
