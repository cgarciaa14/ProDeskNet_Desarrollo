'BBVA-P-423:RQ03 AVH: 15/12/2016 Se crea ventana para cuestionario de salud
'BUG-PD-07: AVH: 27/01/2017 Al capturar Edad y Peso se llena el grid con esos valores en las preguntas correspondientes
'BUG-PD-09: AVH: 14/02/2017 SE REMPLAZA CancelaTarwea POR ModificaTarea()
'BUG-PD-13  GVARGAS  01/03/2017 Cambio WCF Credenciales
'BUG-PD-19: JBB 10/03/2017 Modificaciones para entrevista salud
'BUG-PD-32: AVH: 18/04/2017 Se agregan WS con la secuencia 1 getQuestionnaire, 2 createQuestionnaire, 3 validateQuestionnaire
'BUG-PD-35: AVH: 25/04/2017 Modificaciones pantalla												   
'BUG-PD-40: erodriguez: 04/05/2017: Modificacion para validar INVALIDO_EMISION
'BUG-PD-61 JBEJAR 25/05/2017 CORRECIONES EN LA MODIFICACION  
'BUG-PD-98: RHERNANDEZ: 20/06/17: SE CONSULTA IDQUOTE DE COTIZACION EN CASO DE QUE TENGA SEGUROS BBVA
'BUG-PD-156: JBEJAR 17/07/2017: VALIDACION AL NO ESTAR DISPONIBLE EL SERVICIO. 
'BUG-PD-161: ERODRIGUEZ:18/07/2017:JBEJAR: SE BLOQUEA BOTON PROCESAR PARA TAREA EXITOSA. 
'BUG-PD-158: RHERNANDEZ: 17/07/17: SE CAMBIA COTIZACION DEFAULT PARA SIEMPRE GENERAR UNA ENTREVISTA DE SALUD

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Diagnostics
Imports System.Data
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.BD
Imports ProdeskNet.SN

Partial Class aspx_ConsultaEntrevistaSalud
    Inherits System.Web.UI.Page
    Dim BD As New clsManejaBD
    Dim sol As Integer
    Dim es As New ProdeskNet.Catalogos.clsStatusSolicitud
    Dim dc As New ProdeskNet.Catalogos.clsDatosCliente

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        Me.lblSolicitud2.Text = (Request("sol"))

        es.getStatusSol(Request("sol"))
        Me.lblStCredito.Text = es.PStCredito
        Me.lblStDocumento.Text = es.PStDocumento

        dc.GetDatosCliente(lblSolicitud2.Text)
        lblCliente.Text = dc.propNombreCompleto

        Dim dsresult As New DataSet
        Dim dsParametros As New DataSet
        Dim Bandera As Integer = 0

        Dim Fecha As Date = DateTime.Now.ToString() '("dd/MM/yyyy")

        hdPantalla.Value = Request("idPantalla")
        hdSolicitud.Value = Request("sol")
        hdusuario.Value = Request("usu")

        Dim objTarea As New clsSolicitudes(0)
        Dim dsValidaTarea As DataSet
        Dim ValTareas As Integer = 0
        objTarea.PDK_ID_SOLICITUD = Request("sol")
        objTarea.PDK_ID_PANTALLA = Request("idPantalla")
        dsValidaTarea = objTarea.ManejaTarea(1)

        If dsValidaTarea.Tables.Count > 0 Then
            If dsValidaTarea.Tables(0).Rows.Count > 0 Then
                ValTareas = dsValidaTarea.Tables(0).Rows(0).Item("VALIDA_TAREA")
            End If
        End If
        Dim intEnable As Integer
        intEnable = CInt(Request.QueryString("Enable"))
        If intEnable = 1 Then
            btnProcesarCliente.Attributes.Add("style", "display:none;")
            'btnAutorizar.Attributes.Add("style", "display:none;")
            btnCancelar.Attributes.Add("style", "display:none;")
        Else
            btnImprimir.Attributes.Add("style", "display:none;")
        End If
        If Session("Regresar") Is Nothing Then
            Session("Regresar") = Request.UrlReferrer.LocalPath
        End If

        hdRutaEntrada.Value = Session("Regresar")

        If Not IsPostBack Then
            Session("Tabla") = Nothing
            Buscar()

            'Dim MontoMax As Decimal
            'Dim EdadMeses As Integer
            'dsParametros = BD.EjecutarQuery("SELECT PDK_ID_PARAMETROS_SISTEMA,PDK_PAR_SIS_VALOR_TEXTO FROM PDK_PARAMETROS_SISTEMA WHERE PDK_PAR_SIS_ID_PADRE=233 ORDER BY 1")

            'If dsParametros.Tables.Count > 0 Then
            '    If dsParametros.Tables(0).Rows.Count > 0 Then
            '        MontoMax = Val(dsParametros.Tables(0).Rows(0).Item("PDK_PAR_SIS_VALOR_TEXTO").ToString)
            '        EdadMeses = Val(dsParametros.Tables(0).Rows(1).Item("PDK_PAR_SIS_VALOR_TEXTO").ToString)

            '        If Me.txtMonto.Text > MontoMax Then
            '            Bandera = 1
            '        End If

            '        Fecha = DateAdd("m", Val(Me.txtPlazo.Text), Fecha)
            '        Dim DifMeses As Integer
            '        Dim FechaN As Date = Me.txtFeNac.Text
            '        DifMeses = DateDiff(DateInterval.Month, FechaN, Fecha)

            '        If DifMeses > EdadMeses Then
            '            Bandera = 1
            '        End If

            '    End If
            'End If


            'Dim objpant As New ProdeskNet.SN.clsPantallas()
            'Dim dts As New DataSet()

            'dts = objpant.CargaPantallas(Val(Request("idPantalla")).ToString)

            'If dts.Tables.Count > 0 Then
            '    If dts.Tables(0).Rows.Count > 0 Then
            '        If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Or (Bandera = 0 And ValTareas = 1) Then
            '            btnadelanta_Click(btnProcesar, Nothing)
            '        End If
            '    End If
            'End If
            If Session("Tabla") Is Nothing Then
                If Not WSGetCuestionario() Then
                    Exit Sub
                End If
            End If
            LlenaGrid()
        End If
        dsresult = BD.EjecutarQuery("select A.PDK_PANT_NOMBRE,B.PDK_PAR_SIS_PARAMETRO,A.PDK_PANT_DOCUMENTOS from PDK_PANTALLAS A INNER JOIN PDK_PARAMETROS_SISTEMA B ON A.PDK_PANT_DOCUMENTOS=B.PDK_ID_PARAMETROS_SISTEMA AND B.PDK_PAR_SIS_ID_PADRE=24 where A.PDK_ID_PANTALLAS = " & hdPantalla.Value)
        If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
            hdNomPantalla.Value = dsresult.Tables(0).Rows(0).Item("PDK_PAR_SIS_PARAMETRO").ToString
            hntipoPantalla.Value = dsresult.Tables(0).Rows(0).Item("PDK_PANT_DOCUMENTOS").ToString
        End If

        sol = Request("sol")
        lblSolicitud.Text = sol
        LlenaGrid2()
    End Sub

    Public Sub Buscar()
        Dim dsCuestionario As DataSet
        Dim dsDatosCliente As DataSet
        Dim objFlujos As New clsSolicitudes(0)

        objFlujos.PDK_ID_SOLICITUD = Request("sol")
        dsCuestionario = objFlujos.ConsultaCuestionarios(1)
        dsDatosCliente = objFlujos.ConsultaCuestionarios(2)

        If dsCuestionario.Tables.Count > 0 Then
            If dsCuestionario.Tables(0).Rows.Count > 0 Then
                Session("TABLA") = dsCuestionario
                gvCuestionario.DataSource = dsCuestionario
                gvCuestionario.DataBind()
                LlenaGrid()
            End If
        End If


        If dsDatosCliente.Tables.Count > 0 Then
            If dsDatosCliente.Tables(0).Rows.Count > 0 Then
                Me.txtNombre.Text = dsDatosCliente.Tables(0).Rows(0).Item("NOMBRE").ToString
                Me.txtRFC.Text = dsDatosCliente.Tables(0).Rows(0).Item("RFC").ToString
                Me.txtOcupacion.Text = dsDatosCliente.Tables(0).Rows(0).Item("OCUPACION").ToString
                Me.txtSexo.Text = dsDatosCliente.Tables(0).Rows(0).Item("SEXO").ToString
                Me.txtFeNac.Text = dsDatosCliente.Tables(0).Rows(0).Item("DIA_NACIMIENTO").ToString + "-" + dsDatosCliente.Tables(0).Rows(0).Item("MES_NACIMIENTO").ToString + "-" + dsDatosCliente.Tables(0).Rows(0).Item("ANO_NACIMIENTO").ToString
                Me.txtEstadoCivil.Text = dsDatosCliente.Tables(0).Rows(0).Item("ESTADO_CIVIL").ToString
                Me.txtRegimen.Text = ""
                Me.txtMonto.Text = dsDatosCliente.Tables(0).Rows(0).Item("MONTO").ToString
                Me.txtPlazo.Text = dsDatosCliente.Tables(0).Rows(0).Item("PLAZO").ToString
                Me.txtDomicilio.Text = dsDatosCliente.Tables(0).Rows(0).Item("DOMICILIO").ToString
                Me.txtTel.Text = dsDatosCliente.Tables(0).Rows(0).Item("TELEFONO").ToString
                Me.txtColonia.Text = dsDatosCliente.Tables(0).Rows(0).Item("COLONIA").ToString
                Me.txtCiudad.Text = dsDatosCliente.Tables(0).Rows(0).Item("CIUDAD").ToString
                Me.txtEstado.Text = dsDatosCliente.Tables(0).Rows(0).Item("ESTADO").ToString
                Me.txtCP.Text = dsDatosCliente.Tables(0).Rows(0).Item("CP").ToString
                Me.txtPeso.Text = dsDatosCliente.Tables(0).Rows(0).Item("PESO").ToString
                Me.txtEstatura.Text = dsDatosCliente.Tables(0).Rows(0).Item("ESTATURA").ToString

            End If
        End If

        'gv.DataSource = dsCuestionario
        'gv.DataBind()

    End Sub
    Public Sub LlenaGrid()

        Dim ID As String = ""
        Dim PREGUNTA As String = ""
        Dim SI As Integer = Nothing
        Dim BANDERA_SI As String = ""
        Dim BANDERA_NO As String = ""
        Dim NO As Integer = Nothing
        Dim VALOR As String = ""
        Dim RES As Integer = Nothing

        Dim _ChkSi As New RadioButton
        Dim _ChkNo As New RadioButton

        _ChkSi.Checked = True

        Dim dsTabla As New DataSet
        dsTabla = Session("Tabla")

        If Not dsTabla Is Nothing Then
            If dsTabla.Tables.Count > 0 Then
                For i = 0 To dsTabla.Tables(0).Rows.Count - 1
                    ID = dsTabla.Tables(0).Rows(i).Item("PDK_ID_PREGUNTA").ToString
                    PREGUNTA = dsTabla.Tables(0).Rows(i).Item("PDK_PREGUNTA").ToString
                    BANDERA_SI = dsTabla.Tables(0).Rows(i).Item("SI").ToString
                    BANDERA_NO = dsTabla.Tables(0).Rows(i).Item("NO").ToString
                    VALOR = dsTabla.Tables(0).Rows(i).Item("VALOR").ToString
                    RES = dsTabla.Tables(0).Rows(i).Item("Res").ToString

                    If RES > 1 Then
                        Me.gvCuestionario.Rows(i).Cells(4).Style.Add("display", "NONE")
                    Else
                        Me.gvCuestionario.Rows(i).Cells(3).Style.Add("display", "NONE")
                        Me.gvCuestionario.Rows(i).Cells(2).Style.Add("display", "NONE")
                    End If

                    If BANDERA_SI <> "" Then
                        SI = Val(BANDERA_SI)
                        Me.gvCuestionario.Rows(i).Cells(2).Enabled = False
                        Me.gvCuestionario.Rows(i).Cells(3).Enabled = False
                        Me.gvCuestionario.Rows(i).Cells(4).Enabled = False
                    End If

                    If BANDERA_NO <> "" Then
                        NO = Val(BANDERA_NO)
                    End If



                    If RES = 2 And SI = 1 Then
                        Dim chec As CheckBox = gvCuestionario.Rows(i).Cells(2).FindControl("rbSI")
                        chec.Checked = True
                    End If

                    If RES = 2 And NO = 1 Then
                        Dim chec As CheckBox = gvCuestionario.Rows(i).Cells(3).FindControl("rbNO")
                        chec.Checked = True
                    End If

                    If RES = 1 Then
                        Dim tex As TextBox = gvCuestionario.Rows(i).Cells(4).FindControl("txtValor")
                        tex.Text = VALOR

                        If ID = "CUESVFD02" Then
                            Me.gvCuestionario.Rows(i).Cells(4).Enabled = False
                            tex.Text = Me.txtEstatura.Text
                        End If
                        If ID = "CUESVFD01" Then
                            Me.gvCuestionario.Rows(i).Cells(4).Enabled = False
                            tex.Text = Me.txtPeso.Text
                        End If
                    End If



                Next
            End If
        End If
    End Sub
    Public Sub LlenaGrid2()

        Dim ID As String = ""
        Dim PREGUNTA As String = ""
        Dim SI As Integer = Nothing
        Dim BANDERA_SI As String = ""
        Dim BANDERA_NO As String = ""
        Dim NO As Integer = Nothing
        Dim VALOR As String = ""
        Dim RES As Integer = Nothing

        Dim _ChkSi As New RadioButton
        Dim _ChkNo As New RadioButton

        _ChkSi.Checked = True

        Dim dsTabla As New DataSet
        dsTabla = Session("Tabla")

        If Not dsTabla Is Nothing Then
            If dsTabla.Tables.Count > 0 Then
                For i = 0 To dsTabla.Tables(0).Rows.Count - 1
                    ID = dsTabla.Tables(0).Rows(i).Item("PDK_ID_PREGUNTA").ToString
                    PREGUNTA = dsTabla.Tables(0).Rows(i).Item("PDK_PREGUNTA").ToString
                    BANDERA_SI = dsTabla.Tables(0).Rows(i).Item("SI").ToString
                    BANDERA_NO = dsTabla.Tables(0).Rows(i).Item("NO").ToString
                    VALOR = dsTabla.Tables(0).Rows(i).Item("VALOR").ToString
                    RES = dsTabla.Tables(0).Rows(i).Item("Res").ToString

                    If RES = 1 Then
                        Dim tex As TextBox = gvCuestionario.Rows(i).Cells(4).FindControl("txtValor")
                        tex.Text = VALOR

                        If ID = "CUESVFD02" Then
                            Me.gvCuestionario.Rows(i).Cells(4).Enabled = False
                            tex.Text = Me.txtEstatura.Text
                        End If
                        If ID = "CUESVFD01" Then
                            Me.gvCuestionario.Rows(i).Cells(4).Enabled = False
                            tex.Text = Me.txtPeso.Text
                        End If
                    End If



                Next
            End If
        End If
    End Sub

    Protected Sub btnImprimir_Click(sender As Object, e As EventArgs)

        Dim IDCot = 1
        Dim Fname As String
        Dim crReportDocument As ReportDocument
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim strRuta As String = Server.MapPath("..\Reporte\CuestionarioSalud.rpt")
        Dim dtsDatos As New DataSet
        Dim dtsTabla As New DataTable

        Dim dtsFiltros As New DataSet
        Dim dtsCargIni As New DataSet
        Dim dsDatosCliente As DataSet
        Dim objFlujos As New clsSolicitudes(0)

        If ValidaRespuestas() Then


            crReportDocument = New ReportDocument
            crReportDocument.Load(strRuta)

            'RQ06
            Fname = Server.MapPath("Prodesknet_" & IDCot & "_" & Format$(Now(), "dd-MM-yyyy HH-mm-ss") & ".pdf")
            Fname = Replace(Fname, "\aspx\", "\Docs\")

            System.IO.File.Delete(Fname)

            crDiskFileDestinationOptions = New DiskFileDestinationOptions
            crDiskFileDestinationOptions.DiskFileName = Fname
            crExportOptions = crReportDocument.ExportOptions
            With crExportOptions
                .DestinationOptions = crDiskFileDestinationOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat

            End With



            objFlujos.PDK_ID_SOLICITUD = Val(sol)
            dsDatosCliente = objFlujos.ConsultaCuestionarios(2)
            dsDatosCliente.Tables(0).TableName = "DatosCliente"

            dsDatosCliente.Tables(0).Rows(0).Item("PESO") = Me.txtPeso.Text
            dsDatosCliente.Tables(0).Rows(0).Item("ESTATURA") = Me.txtEstatura.Text
            'dsDatosCliente.Table(0).Rows(0)("Periodo_compuesto") = periodo



            dtsDatos.Tables.Add(dsDatosCliente.Tables(0).Copy())
            dtsDatos.Tables(0).TableName = "DatosCliente"

            If ValidaRespuestas() Then
                Dim dsTabla As New DataSet
                dsTabla = Session("TABLA")

                dtsDatos.Tables.Add(dsTabla.Tables(0).Copy())


                dtsDatos.Tables(1).TableName = "Cuestionario"
            End If




            crReportDocument.SetDataSource(dtsDatos)

            crReportDocument.Export()
            crReportDocument.Close()
            crReportDocument.Dispose()


            'Dim psi As New ProcessStartInfo()
            'psi.UseShellExecute = True
            'psi.FileName = Fname
            'Process.Start(psi)

            'Response.Clear()
            'Response.ClearContent()
            'Response.ClearHeaders()
            'Response.AddHeader("Content-Disposition", "attachment; filename=" + Fname)
            'Response.ContentType = "application/pdf"
            'Response.TransmitFile(Fname)

            Response.Redirect("./Descargapdf.aspx?fname=" & Fname)
        End If

        'Response.TransmitFile(Fname)
        'Response.Flush()
        'Response.Close()
        'Response.Clear()
        'Response.ClearContent()
    End Sub



    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        'Response.Redirect("./consultaPantalla.aspx")
        RegresaPantalla()

    End Sub
    Public Sub RegresaPantalla()
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & Request("sol") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub


    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs)



        If ValidaRespuestas() Then

            Dim IMC As Decimal = 0
            Dim DM As Integer = 0
            Dim HAS As Integer = 0
            Dim OTRA As Integer = 0
            Dim Bandera As String = ""
            Dim mensaje As String = "Error al procesar la Tarea"


            Dim boton As Integer = 64
            Dim objFlujos As New clsSolicitudes(0)
            Dim ds As DataSet


            Dim dc As New clsDatosCliente
            dc.idSolicitud = Request("sol")
            dc.getDatosSol()

            CalculaIMC(IMC)


            If GuardaPreguntas(DM, HAS, OTRA) Then
                If GuardaWS() Then
                    If ValidaWS() Then

                        If DM = 1 And HAS = 1 And IMC < 30 Then
                            Bandera = "ACEPTADA"
                        ElseIf DM = 1 And HAS = 0 And IMC <= 35 Then
                            Bandera = "ACEPTADA"
                        ElseIf DM = 0 And HAS = 1 And IMC <= 35 Then
                            Bandera = "ACEPTADA"
                        ElseIf DM = 0 And HAS = 0 Then 'And IMC <= 35 Then
                            Bandera = "ACEPTADA"
                        Else
                            Bandera = "RECHAZADA"
                        End If


                        If Bandera = "RECHAZADA" Or OTRA = 1 Then
                            'BUG-PD-09: AVH
                            ModificaTarea()
                            'mensaje = "inválido para emisión"
                            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "');", True)
                            'Exit Sub
                        End If

                        objFlujos.PDK_ID_SOLICITUD = lblSolicitud.Text
                        objFlujos.BOTON = boton
                        objFlujos.PDK_CLAVE_USUARIO = Session("IdUsua")

                        ds = objFlujos.ValNegocio(1)
                        If ds.Tables.Count > 0 Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                mensaje = ds.Tables(0).Rows(0).Item("MENSAJE").ToString
                            End If
                        End If


                        If objFlujos.ERROR_SOL <> "" Then
                            Master.MensajeError(objFlujos.ERROR_SOL)
                            Exit Sub
                        Else
                            btnProcesarCliente.Disabled = True
                            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "');", True)
                        End If
                    End If
                End If
            End If
        End If

    End Sub
    Public Function ValidaRespuestas() As Boolean
        ValidaRespuestas = False


        Dim _ChkSi As New RadioButton
        Dim _ChkNo As New RadioButton
        Dim _tex As New TextBox

        Dim idPregunta As String
        Dim Pregunta As String
        Dim Respuestas As Integer
        Dim CajaTexto As String = ""

        'If Me.txtPeso.Text = "" Then
        '    Me.txtPeso.Focus()
        '    Master.MensajeError("El campo Peso es Obligatorio")
        '    btnProcesar.Enabled = True
        '    Exit Function
        'End If
        'If Me.txtEstatura.Text = "" Then
        '    Me.txtEstatura.Focus()
        '    Master.MensajeError("El campo Estatura es Obligatorio")
        '    Exit Function
        'End If

        Dim dsTabla As New DataSet
        dsTabla = Session("TABLA")


        Dim dt As New DataTable()
        'columnas
        dt.TableName = "RESULTADO"
        dt.Columns.Add("PDK_ID_PREGUNTA")
        dt.Columns.Add("PDK_PREGUNTA")
        dt.Columns.Add("SI")
        dt.Columns.Add("NO")
        dt.Columns.Add("VALOR")
        dt.Columns.Add("Res")



        For i As Integer = 0 To gvCuestionario.Rows.Count - 1
            _ChkSi = gvCuestionario.Rows(i).Cells(2).FindControl("rbSI")
            _ChkNo = gvCuestionario.Rows(i).Cells(3).FindControl("rbNO")
            idPregunta = gvCuestionario.Rows(i).Cells(0).Text
            Pregunta = gvCuestionario.Rows(i).Cells(1).Text.ToString
            Respuestas = gvCuestionario.Rows(i).Cells(5).Text
            CajaTexto = ""

            If Respuestas = 2 Then
                If _ChkSi.Checked = False And _ChkNo.Checked = False Then

                    Master.MensajeError("Debe responder la pregunta: " + (idPregunta).ToString)
                    Exit Function
                End If
            Else
                _tex = gvCuestionario.Rows(i).Cells(4).FindControl("txtValor")
                If _tex.Text = "" Then
                    Master.MensajeError("Debe responder la pregunta: " + (idPregunta).ToString)
                    Exit Function
                Else
                    CajaTexto = _tex.Text
                End If

            End If

            Dim row As DataRow = dt.NewRow()
            row("PDK_ID_PREGUNTA") = idPregunta
            row("PDK_PREGUNTA") = Pregunta
            row("SI") = IIf(_ChkSi.Checked = True, 1, 0)
            row("NO") = IIf(_ChkNo.Checked = True, 1, 0)
            row("VALOR") = CajaTexto
            row("Res") = Respuestas
            dt.Rows.Add(row)

            dsTabla.Tables(0).Rows(i).Item("SI") = IIf(_ChkSi.Checked = True, 1, 0)
            dsTabla.Tables(0).Rows(i).Item("NO") = IIf(_ChkNo.Checked = True, 1, 0)
            dsTabla.Tables(0).Rows(i).Item("VALOR") = CajaTexto

        Next

        ValidaRespuestas = True
        Session("TABLA") = Nothing
        Session("TABLA") = dsTabla

    End Function

    Public Function GuardaPreguntas(ByRef DM As Integer, ByRef HAS As Integer, ByRef OTRA As Integer) As Boolean
        GuardaPreguntas = False

        Dim dsTabla As New DataSet
        dsTabla = Session("TABLA")

        Dim ID As String = ""
        Dim PREGUNTA As String = ""
        Dim SI As Integer = Nothing
        Dim NO As Integer = Nothing
        Dim VALOR As String = ""
        Dim RES As Integer = Nothing
        Dim INVALIDO_EMISION As Integer = 0
        Dim objDatosCli As New clsSolicitudes(0)
        Dim objFlujos As New clsSolicitudes(0)

        objDatosCli.PDK_ID_SOLICITUD = Me.lblSolicitud.Text
        objDatosCli.PDK_CLAVE_USUARIO = Session("IdUsua")
        objDatosCli.PESO = txtPeso.Text
        objDatosCli.ESTATURA = txtEstatura.Text
        objDatosCli.ConsultaCuestionarios(4)




        For i = 0 To dsTabla.Tables(0).Rows.Count - 1
            ID = dsTabla.Tables(0).Rows(i).Item("PDK_ID_PREGUNTA").ToString
            PREGUNTA = dsTabla.Tables(0).Rows(i).Item("PDK_PREGUNTA").ToString
            SI = dsTabla.Tables(0).Rows(i).Item("SI").ToString
            NO = dsTabla.Tables(0).Rows(i).Item("NO").ToString
            VALOR = dsTabla.Tables(0).Rows(i).Item("VALOR").ToString
            RES = dsTabla.Tables(0).Rows(i).Item("Res").ToString

            If ID = "CUESVFP01" And SI = 1 Then
                DM = 1
            End If

            If ID = "CUESVFP07" And SI = 1 Then
                HAS = 1
            End If

            If (ID <> "CUESVFP07" Or ID = "CUESVFP01") And SI = 1 Then
                OTRA = 1
            End If
            'AQUI VA EL CODIGO PARA EL NUEVO CAMPO RECHAZO
            If (SI = 1) Then
                INVALIDO_EMISION = SI
            End If

            objFlujos.PDK_ID_SOLICITUD = Me.lblSolicitud.Text
            objFlujos.PDK_CLAVE_USUARIO = Session("IdUsua")
            objFlujos.ID_PREGUNTA = ID
            objFlujos.PREGUNTA = PREGUNTA
            objFlujos.SI = SI
            objFlujos.NO = NO
            objFlujos.VALOR = VALOR
            objFlujos.RES = RES
            objFlujos.INVALIDO_EMISION = INVALIDO_EMISION

            objFlujos.ConsultaCuestionarios(3)
            If objFlujos.ERROR_SOL <> "" Then
                Master.MensajeError(objFlujos.ERROR_SOL)
                Exit Function
            End If
        Next

        GuardaPreguntas = True
        If (INVALIDO_EMISION = 1) Then
            objFlujos.ConsultaCuestionarios(5)
        End If
    End Function
    Public Sub CalculaIMC(ByRef IMC As Decimal)
        Dim EstaturaM As Decimal = (Val(Me.txtEstatura.Text))

        IMC = Val(Me.txtPeso.Text) / (EstaturaM * EstaturaM)

    End Sub

    Public Sub ModificaTarea()
        'BUG-PD-09: AVH
        Dim objModifica As New clsSolicitudes(0)
        objModifica.PDK_ID_SOLICITUD = lblSolicitud.Text
        objModifica.Estatus = 237
        objModifica.PDK_CLAVE_USUARIO = Session("IdUsua")
        objModifica.ManejaTarea(4)
    End Sub
    Public Function WSGetCuestionario() As Boolean

        Try
            WSGetCuestionario = False
            Dim Cuestionario As Cuestionario = New Cuestionario()
            Dim mensaje As String

            Dim dc As New clsDatosCliente
            dc.idSolicitud = Request("sol")
            dc.getDatosSol()

            Cuestionario.header.aapType = "45555F6"
            Cuestionario.header.dateRequest = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
            Cuestionario.header.channel = "8"
            Cuestionario.header.subChannel = "8"
            Cuestionario.header.managementUnit = "0001"
            Cuestionario.header.branchOffice = "CONSUMER FINANCE"
            Cuestionario.header.user = "CARLOS"
            Cuestionario.header.idSession = "3232-3232"
            Cuestionario.header.idRequest = "1212-121212-12121-212"
            Cuestionario.header.dateConsumerInvocation = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")

            Dim idquote As String = ""
            Dim clsseg As clsSeguros = New clsSeguros
            clsseg._ID_SOLICITUD = CInt(Request("sol"))
            Dim DatosSeguro As DataSet = clsseg.getDatosSeguro()
            If (DatosSeguro.Tables.Count > 0) Then
                If (DatosSeguro.Tables(0).Rows.Count > 0) Then
                    idquote = System.Web.HttpContext.Current.Session.Item("QuoteSalud").ToString  'DatosSeguro.Tables(0).Rows(0).Item("NO_COTSEG").ToString
                End If
            End If
            Cuestionario.quote.idQuote = IIf(idquote <> "", idquote, System.Web.HttpContext.Current.Session.Item("QuoteSalud").ToString)


            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

            Dim jsonBODY As String = serializer.Serialize(Cuestionario)

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("Cuestionario") + "getQuestionnaire/"

            'restGT.consumerID = "10000004"
            restGT.buscarHeader("ResponseWarningDescription")
            Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

            Dim jresul2 As Respuesta = serializer.Deserialize(Of Respuesta)(jsonResult)

            Dim alert = serializer.Deserialize(Of msjerr)(jsonResult)

            If restGT.IsError Then
                mensaje = (alert.message & " Estatus: " & alert.status & ".")
                btnProcesarCliente.Attributes.Add("style", "display:none;")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("sol") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "');", True)
                Exit Function
            End If

            Dim Pregunta As String
            Dim idPregunta As String

            Dim dt As New DataTable()
            'columnas
            dt.TableName = "RESULTADO"
            dt.Columns.Add("PDK_ID_PREGUNTA")
            dt.Columns.Add("PDK_PREGUNTA")
            dt.Columns.Add("SI")
            dt.Columns.Add("NO")
            dt.Columns.Add("VALOR")
            dt.Columns.Add("Res")


            For i = 0 To jresul2.question.Count - 1
                Pregunta = jresul2.question(i).catalogItemBase.name
                idPregunta = jresul2.question(i).catalogItemBase.id

                If jresul2.question(i).answers.Count > 1 Then

                End If

                Dim row As DataRow = dt.NewRow()
                row("PDK_ID_PREGUNTA") = idPregunta
                row("PDK_PREGUNTA") = Pregunta
                row("Res") = jresul2.question(i).answers.Count
                dt.Rows.Add(row)
            Next

            Dim dstable As New DataSet

            dstable.Tables.Add(dt)
            Session("TABLA") = dstable


            gvCuestionario.DataSource = dstable
            gvCuestionario.DataBind()

            For i = 0 To jresul2.question.Count - 1
                If jresul2.question(i).answers.Count > 1 Then
                    Me.gvCuestionario.Rows(i).Cells(4).Style.Add("display", "NONE")
                Else
                    Me.gvCuestionario.Rows(i).Cells(3).Style.Add("display", "NONE")
                    Me.gvCuestionario.Rows(i).Cells(2).Style.Add("display", "NONE")

                    Dim idClave As String, preguntan As String
                    idClave = gvCuestionario.Rows(i).Cells(0).Text
                    preguntan = gvCuestionario.Rows(i).Cells(1).Text

                    If idClave = "CUESVFD02" Or idClave = "CUESVFD01" Then
                        Me.gvCuestionario.Rows(i).Cells(4).Enabled = False
                    End If


                End If
            Next

            WSGetCuestionario = True
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Function
    Public Class Cuestionario
        Public header As headerWS = New headerWS()
        Public quote As quote = New quote()

    End Class
    Public Class Cuestionario2
        Public header As headerWS = New headerWS()
        Public quote As quote = New quote()
        Public questionnaire As questionnaire = New questionnaire()
    End Class
    Public Class headerWS
        Public aapType As String
        Public dateRequest As String
        Public channel As String
        Public subChannel As String
        Public managementUnit As String
        Public branchOffice As String
        Public user As String
        Public idSession As String
        Public idRequest As String
        Public dateConsumerInvocation As String
    End Class
    Public Class quote
        Public idQuote As String
    End Class
    Public Class Respuesta
        Public question As New List(Of question)
    End Class
    Public Class question
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
        Public type As String
        Public answers As New List(Of answers)
    End Class
    Public Class questionRespuesta
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
        Public answerQuestion As answerQuestion = New answerQuestion()

    End Class
    Public Class catalogItemBase
        Public id As String
        Public name As String
    End Class
    Public Class answerQuestion
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class
    Public Class answers
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class
    Public Class msjerr
        Public message As String
        Public status As String
    End Class
    Public Class questionnaire
        Public questionRespuesta As New List(Of questionRespuesta)
    End Class

    Protected Sub btnadelanta_Click(sender As Object, e As EventArgs) Handles btnadelanta.Click
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()

        Try

            dsresult = BD.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

            If muestrapant = 0 Then
                Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
            ElseIf muestrapant = 2 Then
                Response.Redirect("../aspx/consultaPanelControl.aspx")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Function GuardaWS() As Boolean
        GuardaWS = False
        Dim Cuestionario As Cuestionario2 = New Cuestionario2()
        Dim mensaje As String

        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()

        Cuestionario.header.aapType = "45555F6"
        Cuestionario.header.dateRequest = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
        Cuestionario.header.channel = "8"
        Cuestionario.header.subChannel = "8"
        Cuestionario.header.managementUnit = "0001"
        Cuestionario.header.branchOffice = "CONSUMER FINANCE"
        Cuestionario.header.user = "usuario"
        Cuestionario.header.idSession = "3232-3232"
        Cuestionario.header.idRequest = "1212-121212-12121-212"
        Cuestionario.header.dateConsumerInvocation = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
        Dim idquote As String = ""
        Dim clsseg As clsSeguros = New clsSeguros
        clsseg._ID_SOLICITUD = CInt(Request("sol"))
        Dim DatosSeguro As DataSet = clsseg.getDatosSeguro()
        If (DatosSeguro.Tables.Count > 0) Then
            If (DatosSeguro.Tables(0).Rows.Count > 0) Then
                idquote = "145052" 'DatosSeguro.Tables(0).Rows(0).Item("NO_COTSEG").ToString
            End If
        End If
        Cuestionario.quote.idQuote = IIf(idquote <> "", idquote, "145052")

        'Dim Respuestas As New questionRespuesta

        Dim dsTabla As New DataSet
        dsTabla = Session("TABLA")

        Dim ID As String = ""
        Dim PREGUNTA As String = ""
        Dim SI As Integer = Nothing
        Dim NO As Integer = Nothing
        Dim VALOR As String = ""
        Dim RES As Integer = Nothing

        For i = 0 To dsTabla.Tables(0).Rows.Count - 1
            ID = dsTabla.Tables(0).Rows(i).Item("PDK_ID_PREGUNTA").ToString
            PREGUNTA = dsTabla.Tables(0).Rows(i).Item("PDK_PREGUNTA").ToString
            SI = dsTabla.Tables(0).Rows(i).Item("SI").ToString
            NO = dsTabla.Tables(0).Rows(i).Item("NO").ToString
            VALOR = dsTabla.Tables(0).Rows(i).Item("VALOR").ToString
            RES = dsTabla.Tables(0).Rows(i).Item("Res").ToString

            Dim Respuestas As New questionRespuesta
            Respuestas.catalogItemBase.id = ID

            If ID = "CUESVFD02" Or ID = "CUESVFD01" Then
                If ID = "CUESVFD02" Then
                    Respuestas.answerQuestion.catalogItemBase.name = VALOR.Replace(".", "")
                Else
                    Respuestas.answerQuestion.catalogItemBase.name = VALOR

                End If

                Respuestas.answerQuestion.catalogItemBase.id = "001"
            Else
                Respuestas.answerQuestion.catalogItemBase.name = ""
                Respuestas.answerQuestion.catalogItemBase.id = SI
            End If

            Cuestionario.questionnaire.questionRespuesta.Add(Respuestas)
        Next

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

        Dim jsonBODY As String = serializer.Serialize(Cuestionario)

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("createQuestionnaire")

        'restGT.consumerID = "10000004"
        restGT.buscarHeader("ResponseWarningDescription")
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

        Dim jresul2 As Respuesta = serializer.Deserialize(Of Respuesta)(jsonResult)

        Dim alert = serializer.Deserialize(Of msjerr)(jsonResult)

        If restGT.IsError Then
            mensaje = (alert.message & " Estatus: " & alert.status & ".")

            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("sol") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "');", True)
            Exit Function
        End If

        GuardaWS = True

    End Function

    Public Function ValidaWS() As Boolean
        ValidaWS = False

        Dim Cuestionario As Cuestionario = New Cuestionario()
        Dim mensaje As String

        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()

        Cuestionario.header.aapType = "45555F6"
        Cuestionario.header.dateRequest = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
        Cuestionario.header.channel = "8"
        Cuestionario.header.subChannel = "8"
        Cuestionario.header.managementUnit = "0001"
        Cuestionario.header.branchOffice = "CONSUMER FINANCE"
        Cuestionario.header.user = "CARLOS"
        Cuestionario.header.idSession = "3232-3232"
        Cuestionario.header.idRequest = "1212-121212-12121-212"
        Cuestionario.header.dateConsumerInvocation = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")

        Dim idquote As String = ""
        Dim clsseg As clsSeguros = New clsSeguros
        clsseg._ID_SOLICITUD = CInt(Request("sol"))
        Dim DatosSeguro As DataSet = clsseg.getDatosSeguro()
        If (DatosSeguro.Tables.Count > 0) Then
            If (DatosSeguro.Tables(0).Rows.Count > 0) Then
                idquote = "145052" 'DatosSeguro.Tables(0).Rows(0).Item("NO_COTSEG").ToString
            End If
        End If
        Cuestionario.quote.idQuote = IIf(idquote <> "", idquote, "145052")


        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

        Dim jsonBODY As String = serializer.Serialize(Cuestionario)

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("validateQuestionnaire")

        'restGT.consumerID = "10000004"
        restGT.buscarHeader("ResponseWarningDescription")
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

        Dim jresul2 As Respuesta = serializer.Deserialize(Of Respuesta)(jsonResult)

        Dim alert = serializer.Deserialize(Of msjerr)(jsonResult)

        If restGT.IsError Then
            mensaje = (alert.message & " Estatus: " & alert.status & ".")

            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("sol") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "');", True)
            Exit Function
        End If


        ValidaWS = True

    End Function
End Class
