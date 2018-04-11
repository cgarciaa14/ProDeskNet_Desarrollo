Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Diagnostics
Imports System.Data
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.BD
Imports ProdeskNet.SN
Imports ProdeskNet.Seguridad
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlInputFile
'BBV-P-423: RQADM-19: AVEGA: 10/05/2017 Mesa Especial, Mesa Especiales Saldos Vencidos
'BBV-P-423: RQXLS1: CGARCIA: 23/05/2017 Se agrego la busqueda de alianza por registro
'BUG-PD-74: JBEJAR : 06/06/2017 Se agrega boton imprimir caratula de sancion para generar pdf ademas se agrega  visor documental.
'BUG-PD-80: JBEJAR : 09/06/2017 Se  realiza la operacion de bloquear el boton de procesar cuando la tarea sea exitosa. 
'BUG-PD-103: JEBJAR : 17/06/2017 Cambios al gv. 
'BUG-PD-322: ERODRIGUEZ: 03/01/2017: Se cambio el redireccionamiento al panel de control
'BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit
Partial Class aspx_MesaEspeciales
    Inherits System.Web.UI.Page
    Dim BD As New clsManejaBD
    Dim sol As Integer
    Dim es As New ProdeskNet.Catalogos.clsStatusSolicitud
    Dim dc As New ProdeskNet.Catalogos.clsDatosCliente


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        Me.lblSolicitud.Text = Request.QueryString("sol")

        es.getStatusSol(Request.QueryString("sol"))
        Me.lblStCredito.Text = es.PStCredito
        Me.lblStDocumento.Text = es.PStDocumento

        dc.GetDatosCliente(lblSolicitud.Text)
        lblCliente.Text = dc.propNombreCompleto

        hdPantalla.Value = Request("idPantalla") '108
        hdSolicitud.Value = Request("sol") '1277
        hdusuario.Value = Request("usu") '1


        Dim intEnable As Integer
        intEnable = CInt(Request.QueryString("Enable"))
        If intEnable = 1 Then
            btnProcesar.Attributes.Add("style", "display:none;")
            btnCancelar.Attributes.Add("style", "display:none;")
        End If

        Session.Add("idSol", hdSolicitud.Value)

        If Session("Regresar") Is Nothing Then
            Session("Regresar") = Request.UrlReferrer.LocalPath
        End If

        Session("idSol") = hdSolicitud.Value
        hdRutaEntrada.Value = Session("Regresar")

        If Not IsPostBack Then
            Buscar()
            CargaCombos()
        End If
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');", True)

    End Sub

    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs)

        'SE VALIDA QUE YA SE ENTREGARON LOS DOCUMENTOS OBLIGATORIOS DE LA PANTALLA
        Dim objFlujos1 As New clsSolicitudes(0)
        Dim DS As DataSet
        btnProcesar.Enabled = False 'BUG-PD-80 Bloqueamos el boton procesar para evitar duplicidad en los registros al procesar la tarea.  
        objFlujos1.PDK_ID_SOLICITUD = Me.lblSolicitud.Text
        objFlujos1.PDK_ID_PANTALLA = Request("idPantalla")
        objFlujos1.PDK_CLAVE_USUARIO = Session("IdUsua")

        DS = objFlujos1.ConsultaDocumento(2)



        If objFlujos1.ERROR_SOL <> "" Then
            Master.MensajeError(objFlujos1.ERROR_SOL)
            Exit Sub
        End If

        If ddlTurnar.SelectedValue = 0 Then
            Master.MensajeError("Falta seleccionar la opcion de Turnar")
            btnProcesar.Enabled = True 'BUG-PD-80  habilitamos el boton en caso de caer aqui por reglas de la pantalla. 
            Exit Sub
        Else
            asignaTarea(ddlTurnar.SelectedValue)
        End If


    End Sub

    Protected Sub btnImprimir_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnadelanta_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnVisor_Click(sender As Object, e As EventArgs)
        Dim ds As DataSet
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos

        objCatalogos.Parametro = Me.lblSolicitud.Text
        ds = objCatalogos.Catalogos(7)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Dim Cliente_bbva As String = ""

                Cliente_bbva = ds.Tables(0).Rows(0).Item("CLIENTE_BBVA")

                If Cliente_bbva <> "" Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "&cliente=" + Cliente_bbva.ToString + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)

                End If
            End If
        End If
    End Sub
    Protected Sub btnPrint_Click(sender As Object, e As EventArgs)
        'BUG-PD-74 JBEJAR IMPLEMENTACION DE BOTON IMPRIMIR EN PANTALLA DE ESPECIALES.  
        Dim button As Button = sender
        Dim Fname As String
        Dim crReportDocument As ReportDocument
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim Ds_1 As New DataSet
        Dim Sol = Request("sol")
        Dim strRuta As String = Server.MapPath("..\Reporte\CaratulaDeSancion.rpt")
        Dim objFlujos As New clsMesaPoolCredito() 'clase donde se genera el reporte 
        Dim ds As New DataSet

        objFlujos.PDK_ID_SOLICITUD = Sol
        objFlujos.PDK_CLAVE_USUARIO = Convert.ToInt32(hdusuario.Value)
        objFlujos.CausaRechazo = " rechazo"
        objFlujos.MotivoSancion = "motivo sancion"
        ds = objFlujos.ConsultaSolicitudPool(6) 'opcion para llenar reporte    

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
        Dim strLinl_1 As String = "./Descargapdf.aspx?fname="
        Dim strLink As String = HttpUtility.UrlEncode(Fname)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLinl_1 & strLink & "';", True)
        'Response.Redirect("./Descargapdf.aspx?fname=" & Fname)

    End Sub


    Public Sub RegresaPantalla()
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        'Response.Redirect(Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        RegresaPantalla()
    End Sub

    Public Sub Buscar()
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
        Dim dsDetalle As DataSet
        Dim i As Integer = 0
        Dim j As Integer
        Dim dgc As DataGridColumn
        objCatalogos.Parametro = lblSolicitud.Text
        dsDetalle = objCatalogos.Catalogos(10)

        If dsDetalle.Tables.Count > 0 Then
            If dsDetalle.Tables(0).Rows.Count > 0 Then
                Me.gvMesaEspeciales.DataSource = dsDetalle
                Me.gvMesaEspeciales.DataBind()
                lblAlianza.Text = dsDetalle.Tables(0).Rows(0)("Alianza".ToString())
                gvMesaEspeciales.HeaderRow.Cells(9).Visible = False
                While i < gvMesaEspeciales.Rows.Count
                    gvMesaEspeciales.Rows(i).Cells(9).Visible = False
                    i = i + 1
                End While
            End If
        End If

        Dim dsVehiculos As DataSet
        objCatalogos.Parametro = lblSolicitud.Text
        dsVehiculos = objCatalogos.Catalogos(11)

        If dsVehiculos.Tables.Count > 0 Then
            'Se elimina if  para entrar a la propiedad del empity del gv y mostrar el mensaje. 
            Me.gvMesaAutos.DataSource = dsVehiculos
            Me.gvMesaAutos.DataBind()


        End If


    End Sub

    Protected Sub lbDetalle_Click(sender As Object, e As EventArgs)
        Dim Sol As String = ""
        Sol = sender.CommandArgument.ToString()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & Sol & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub
    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("solicitud")))
        Dim mensaje As String = String.Empty

        Try

            Dim ds_siguienteTarea As DataSet
            ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & Request("idPantalla").ToString)

            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Me.lblSolicitud.Text
            Solicitudes.PDK_CLAVE_USUARIO = Request("usu")
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")

            If idAsignarPantalla <> 0 Then
                'Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla
                If ddlTurnar.SelectedValue = -1 Then
                    Solicitudes.PDK_ID_CAT_RESULTADO = ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")
                ElseIf ddlTurnar.SelectedValue = -2 Then
                    'Dim Solicitudes2 As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
                    'Solicitudes2.PDK_ID_PANTALLA = Request("idPantalla")
                    'Solicitudes2.PDK_ID_SOLICITUD = Val(Request("Sol"))
                    Solicitudes.Estatus = 295
                    Solicitudes.Estatus_Cred = 295
                    Solicitudes.Comentario = ddlTurnar.SelectedItem.Text
                    'Solicitudes2.PDK_CLAVE_USUARIO = Session("IdUsua")
                    Solicitudes.ManejaTarea(6)
                    Master.MsjErrorRedirect("Solicitud Declinada", "../aspx/consultaPanelControl.aspx")
                    Exit Sub
                ElseIf ddlTurnar.SelectedValue = -3 Then
                    Solicitudes.PDK_ID_CAT_RESULTADO = ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO")
                Else
                    Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla
                End If
            End If

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                Master.MsjErrorRedirect(mensaje, "../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If

            dslink = objtarea.SiguienteTarea(Me.lblSolicitud.Text)

            muestrapant = objpantalla.SiguientePantalla(Me.lblSolicitud.Text)
            Dim dc As New clsDatosCliente
            dc.idSolicitud = Me.lblSolicitud.Text
            dc.getDatosSol()

            If muestrapant = 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString & "');", True)
            ElseIf muestrapant = 2 Then
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "&sol=" & Val(Request("Sol")).ToString & "');", True)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "');", True)
                'Response.Redirect("../aspx/consultaPanelControl.aspx")
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub
    Public Sub CargaCombos()
        Dim clsquiz As New clsCuestionarioSolvsID()
        Dim objCombo As New clsParametros
        clsquiz._ID_PANT = CInt(hdPantalla.Value)
        Dim dtsres As New DataSet
        dtsres = clsquiz.getTurnar()
        If dtsres.Tables.Count > 0 And dtsres.Tables(0).Rows.Count > 0 Then
            objCombo.LlenaCombos(dtsres, "TURNAR_NOMBRE", "PDK_ID_TAREAS", ddlTurnar, True, True)
        End If
    End Sub
End Class
