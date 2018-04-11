'RQADM2-03 RHERNANDEZ: 14/09/17: SE CREA PANTALLA PARA LA IMPRESION DEL COMPROBANTE DE DESEMBOLSO
'BUG-PD-264: RHERNANDEZ: 10/11/17: SE MODIFICO AVANCE DE LA TAREA DEBIDO A QUE ABRIRA LA TAREA HIBRIDA DE EMISION POLIZAS BBVA
'BUG-PD-296: DCORNEJO: 07/12/17: CORRECION AL PROCESAR COMPROBANTE DE DESEMBOLSO
Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports ProdeskNet.Catalogos
Imports ProdeskNet.BD

Partial Class aspx_CuponDesembolso
    Inherits System.Web.UI.Page
    Dim sol As New ProdeskNet.Catalogos.clsStatusSolicitud
    Dim clien As New ProdeskNet.Catalogos.clsDatosCliente
    Dim BD As New clsManejaBD
    Protected Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim Fname As String
        Dim crReportDocument As ReportDocument
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
        Dim crExportOptions As ExportOptions
        Dim strRuta As String = Server.MapPath("..\Reporte\Desembolso.rpt")
        Dim dtsDatos As New DataSet
        Dim dtsTabla As New DataTable
        Dim id_Solicitud As Integer = Request("sol")


        crReportDocument = New ReportDocument
        crReportDocument.Load(strRuta)

        Fname = Server.MapPath("Prodesknet_" & id_Solicitud & "_" & Format$(Now(), "dd-MM-yyyy HH-mm-ss") & ".pdf")
        Fname = Replace(Fname, "\aspx\", "\Docs\")


        Dim objDes As New ProdeskNet.SN.clsDesembolso
        Dim dsDes As DataSet
        objDes.Solicitud = Request("sol")
        dsDes = objDes.Desembolso(1)




        dtsDatos.Tables.Add(dsDes.Tables(4).Copy())
        dtsDatos.Tables(0).TableName = "Datos"

        dtsDatos.Tables.Add(dsDes.Tables(2).Copy())
        dtsDatos.Tables(1).TableName = "Agencia"

        dtsDatos.Tables.Add(dsDes.Tables(3).Copy())
        dtsDatos.Tables(2).TableName = "Detalle"

        dtsDatos.Tables.Add(dsDes.Tables(5).Copy())
        dtsDatos.Tables(3).TableName = "Desembolso"

        System.IO.File.Delete(Fname)

        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        crDiskFileDestinationOptions.DiskFileName = Fname
        crExportOptions = crReportDocument.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat

        End With


        crReportDocument.SetDataSource(dtsDatos)

        crReportDocument.Export()
        crReportDocument.Close()
        crReportDocument.Dispose()

        Response.Redirect("./Descargapdf.aspx?fname=" & Fname)
    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            clien.GetDatosCliente(Request("solicitud"))
            sol.getStatusSol(Request("solicitud"))


            Dim dsresult As New DataSet
            Dim dsresulta As New DataSet
            Dim enable As Integer = Request("Enable")
            hdPantalla.Value = Request("pantalla")
            hdSolicitud.Value = Request("solicitud")
            hdusuario.Value = Request("usu")
            Me.lblSolicitud.Text = Request("solicitud")
            Me.lblCliente.Text = clien.propNombreCompleto
            Me.lblStCredito.Text = sol.PStCredito
            Me.lblStDocumento.Text = sol.PStDocumento
            If Session("Regresar") Is Nothing Then
                Session("Regresar") = Request.UrlReferrer.LocalPath
            End If

            hdRutaEntrada.Value = Session("Regresar")
            Try
                dsresult = BD.EjecutarQuery("EXEC sp_GenerarRura  " & hdSolicitud.Value & "," & hdPantalla.Value & "," & hdusuario.Value & "")
                If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
                    hdnResultado.Value = dsresult.Tables(0).Rows(0).Item("RUTA")
                    hdnResultado1.Value = dsresult.Tables(1).Rows(0).Item("RUTA2")
                    hdnResultado2.Value = dsresult.Tables(2).Rows(0).Item("RUTA3")
                End If

            Catch ex As Exception
                hdnResultado.Value = Session("Regresar")
            End Try
            If enable = 1 Then
                cmbguardar1.Disabled = True
            End If
        End If
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & Request("sol") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub

    Protected Sub btnproc_Click(sender As Object, e As EventArgs)
        asignaTarea(0)
    End Sub
    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("sol")))
        Dim mensaje As String = String.Empty
        Try

            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("sol"))
            Solicitudes.PDK_CLAVE_USUARIO = Request("usu")
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")

            If idAsignarPantalla <> 0 Then
                Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla
            End If

            If mensaje <> "" Then
                Master.MensajeError(mensaje)
                cmbguardar1.Disabled = False
            Else
                dsresult = Solicitudes.ValNegocio(1)
                mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
                Solicitudes.MENSAJE = mensaje
                Solicitudes.ManejaTarea(5)

                If mensaje <> "Tarea Exitosa" And mensaje <> "SE RECHAZO  DOCUMENTO " And mensaje <> "TAREA EXITOSA" Then
                    Throw New Exception(mensaje)
                End If

                dslink = objtarea.SiguienteTarea(Val(Request("sol")))

                muestrapant = objpantalla.SiguientePantalla(Val(Request("sol")))
                Dim dc As New clsDatosCliente
                dc.idSolicitud = Request("sol")
                dc.getDatosSol()

                If CInt(dslink.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR").ToString) = 0 Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje.ToString() & "', '../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usu")).ToString & "&usu=" & Val(Request("usu")).ToString & "');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "');", True)
                End If
            End If

        Catch ex As Exception
            cmbguardar1.Attributes.Remove("disabled")
            btnRegresar.Attributes.Remove("disabled")
            cmbguardar1.Disabled = False
            Master.MensajeError(mensaje)
        End Try
    End Sub
End Class
