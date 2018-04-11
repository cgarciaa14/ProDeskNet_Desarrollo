Imports CrystalDecisions.CrystalReports.Engine
Imports System.Drawing
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports CrystalDecisions.Shared
Imports System.Data
#Region "Trackers"
' BBV-P-423 JRHM:  RQ J-REPORTE SOLICITUD DE CREDITO 30/12/16.- Se agrego opcion 5 para la impresion de reporte solicitud de credito y validacion por web
'service para validar si elel cliente es cliente BBVA
'BUG-PD-13  GVARGAS  01/03/2017 Cambio WCF Credenciales
'BUG-PD-140:ERODRIGUEZ: 05/07/2017: Se agrego nueva tabla por cambio de origen de monto fijo y variable
'BUG-PD-171:ERODRIGUEZ: 27/07/2017: Se agregaron parametros al reporte para cambiar colores y logo segun alianza
#End Region


Public Class aspx_ImprimirCreditoSolicitud
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Try
                Dim Dat As New DataSet
                Dim crReportDocument As ReportDocument
                Dim crExportOptions As New ExportOptions
                Dim crDiskFileDestinationOptions As DiskFileDestinationOptions
                Dim Fname As String
                Select Case Request("CVE")
                    Case 0
                        crReportDocument = New ReportDocument
                        crReportDocument.Load(Server.MapPath("../Reporte/Solicitud.rpt"))
                        'Dat = clsPantallaObjeto.ObtenerPantCrys(Request("IdFolio"), 0)
                        Dat = clsPantallaObjeto.ObtenDatosImpSolicitud(Request("IdFolio"))
                        If Dat.Tables.Count > 0 Then
                            Dat.Tables(0).TableName = "SOLICITUD"
                            Dat.Tables(1).TableName = "DATOS_PERSONAL"
                            Dat.Tables(2).TableName = "EMPLEO"
                            Dat.Tables(3).TableName = "REFERENCIA_PERSONALES"
                            Dat.Tables(4).TableName = "CARGO_DIRECTO"
                            Dat.Tables(5).TableName = "COACREDITADO"
                            Dat.Tables(6).TableName = "EMPLEO_COACREDITADO"
                            Dat.Tables(7).TableName = "DISTRIBUIDOR"
                            Dat.Tables(8).TableName = "REFERENCIA_CREDITO"
                            Dat.Tables(9).TableName = "PERFIL_ECONOMICO"
                            Dat.Tables(10).TableName = "PERFIL_ECONOMICO_COA"
                            Dat.Tables(11).TableName = "REFERENCIA_CREDITO_COA"
                            Dat.Tables(12).TableName = "CARGO_DIRECTO_COA"
                            Dat.Tables(13).TableName = "DATOS_SOLICITANTE"

                            'Dim report As New ReportDocument
                            'report = New Solicitud
                            ''report.Load(Server.MapPath("../Reporte/Solicitud.rpt"))
                            'report.SetDataSource(Dat)
                            ''report.SetDataSource(Dat.Tables("DATOS_PERSONAL"))
                            'visor.ReportSource = report
                            Fname = Server.MapPath("../Documentos/SolicitudExportado" & Format$(Now(), "dd-MM-yyyy") & ".pdf")
                            crDiskFileDestinationOptions = New DiskFileDestinationOptions
                            crDiskFileDestinationOptions.DiskFileName = Fname
                            crExportOptions = crReportDocument.ExportOptions
                            With crExportOptions
                                .DestinationOptions = crDiskFileDestinationOptions
                                .ExportDestinationType = ExportDestinationType.DiskFile
                                .ExportFormatType = ExportFormatType.PortableDocFormat
                            End With
                            crReportDocument.SetDataSource(Dat)
                            crReportDocument.Export()
                            Response.ClearContent()
                            Response.ClearHeaders()
                            Response.ContentType = "application/pdf"
                            Response.WriteFile(Fname)
                            Response.End()

                        End If



                    Case 1
                        crReportDocument = New ReportDocument
                        crReportDocument.Load(Server.MapPath("../Reporte/EntrevistaForma.rpt"))
                        'Dat = clsPantallaObjeto.ObtenerPantCrys(Request("IdFolio"))
                        Dat = clsPantallaObjeto.ObtenerEntreReporte(Request("idPantalla"), Request("IdFolio"), Request("IdUsu"), 1, 1)
                        If Dat.Tables.Count > 0 Then
                            Dat.Tables(0).TableName = "ENCABEZADAT"
                            Dat.Tables(1).TableName = "PREGUNTAS"
                            'Dat.Tables(2).TableName = "EMPLEO"
                            'Dat.Tables(3).TableName = "REFERENCIA_PERSONALES"
                            'Dat.Tables(4).TableName = "CARGO_DIRECTO"
                            'Dat.Tables(5).TableName = "COACREDITADO"
                            'Dat.Tables(6).TableName = "EMPLEO_COACREDITADO"
                            'Dat.Tables(7).TableName = "DISTRIBUIDOR"
                            'Dat.Tables(8).TableName = "REFERENCIA_CREDITO"
                            'Dat.Tables(9).TableName = "PERFIL_ECONOMICO"
                            'Dat.Tables(10).TableName = "PERFIL_ECONOMICO_COA"
                            'Dat.Tables(11).TableName = "REFERENCIA_CREDITO_COA"
                            'Dat.Tables(12).TableName = "CARGO_DIRECTO_COA"

                            'Dim report As New ReportDocument
                            'report = New Solicitud
                            ''report.Load(Server.MapPath("../Reporte/Solicitud.rpt"))
                            'report.SetDataSource(Dat)
                            ''report.SetDataSource(Dat.Tables("DATOS_PERSONAL"))
                            'visor.ReportSource = report
                            Fname = Server.MapPath("../Documentos/EntrevistaExportado_" & Request("IdFolio") & "_" & Format$(Now(), "dd-MM-yyyy") & ".pdf")
                            crDiskFileDestinationOptions = New DiskFileDestinationOptions
                            crDiskFileDestinationOptions.DiskFileName = Fname
                            crExportOptions = crReportDocument.ExportOptions
                            With crExportOptions
                                .DestinationOptions = crDiskFileDestinationOptions
                                .ExportDestinationType = ExportDestinationType.DiskFile
                                .ExportFormatType = ExportFormatType.PortableDocFormat
                            End With
                            crReportDocument.SetDataSource(Dat)
                            crReportDocument.Export()
                            Response.ClearContent()
                            Response.ClearHeaders()
                            Response.ContentType = "application/pdf"
                            Response.WriteFile(Fname)

                            ''System.Diagnostics.Process.Start(Fname)
                            Response.End()

                        End If
                    Case 2
                        'crReportDocument = New ReportDocument
                        'crReportDocument.Load(Server.MapPath("../Reporte/SolicitudSol.rpt"))
                        'Dat = clsPantallaObjeto.ObtenerPantCrys(Request("IdFolio"), 1)
                        'If Dat.Tables.Count > 0 Then
                        '    Dat.Tables(0).TableName = "SOLICITUD"
                        '    Dat.Tables(1).TableName = "DATOS_PERSONAL"
                        '    Dat.Tables(2).TableName = "EMPLEO"
                        '    Dat.Tables(3).TableName = "REFERENCIA_PERSONALES"
                        '    Dat.Tables(4).TableName = "CARGO_DIRECTO"
                        '    Dat.Tables(5).TableName = "DISTRIBUIDOR"
                        '    Dat.Tables(6).TableName = "REFERENCIA_CREDITO"
                        '    Dat.Tables(7).TableName = "PERFIL_ECONOMICO"
                        '    Dat.Tables(8).TableName = "DATOS_SOLICITANTE"

                        '    'Dim report As New ReportDocument
                        '    'report = New Solicitud
                        '    ''report.Load(Server.MapPath("../Reporte/Solicitud.rpt"))
                        '    'report.SetDataSource(Dat)
                        '    ''report.SetDataSource(Dat.Tables("DATOS_PERSONAL"))
                        '    'visor.ReportSource = report
                        '    Fname = Server.MapPath("../Documentos/SolicitudExportado" & Format$(Now(), "dd-MM-yyyy") & ".pdf")
                        '    crDiskFileDestinationOptions = New DiskFileDestinationOptions
                        '    crDiskFileDestinationOptions.DiskFileName = Fname
                        '    crExportOptions = crReportDocument.ExportOptions
                        '    With crExportOptions
                        '        .DestinationOptions = crDiskFileDestinationOptions
                        '        .ExportDestinationType = ExportDestinationType.DiskFile
                        '        .ExportFormatType = ExportFormatType.PortableDocFormat
                        '    End With
                        '    crReportDocument.SetDataSource(Dat)
                        '    crReportDocument.Export()
                        '    Response.ClearContent()
                        '    Response.ClearHeaders()
                        '    Response.ContentType = "application/pdf"
                        '    Response.WriteFile(Fname)
                        '    Response.End()

                        'End If
                        crReportDocument = New ReportDocument
                        crReportDocument.Load(Server.MapPath("../Reporte/Solicitud.rpt"))
                        'Dat = clsPantallaObjeto.ObtenerPantCrys(Request("IdFolio"), 0)
                        Dat = clsPantallaObjeto.ObtenDatosImpSolicitud(Request("IdFolio"))
                        If Dat.Tables.Count > 0 Then
                            Dat.Tables(0).TableName = "SOLICITUD"
                            Dat.Tables(1).TableName = "DATOS_PERSONAL"
                            Dat.Tables(2).TableName = "EMPLEO"
                            Dat.Tables(3).TableName = "REFERENCIA_PERSONALES"
                            Dat.Tables(4).TableName = "CARGO_DIRECTO"
                            Dat.Tables(5).TableName = "COACREDITADO"
                            Dat.Tables(6).TableName = "EMPLEO_COACREDITADO"
                            Dat.Tables(7).TableName = "DISTRIBUIDOR"
                            Dat.Tables(8).TableName = "REFERENCIA_CREDITO"
                            Dat.Tables(9).TableName = "PERFIL_ECONOMICO"
                            Dat.Tables(10).TableName = "PERFIL_ECONOMICO_COA"
                            Dat.Tables(11).TableName = "REFERENCIA_CREDITO_COA"
                            Dat.Tables(12).TableName = "CARGO_DIRECTO_COA"
                            Dat.Tables(13).TableName = "DATOS_SOLICITANTE"

                            'Dim report As New ReportDocument
                            'report = New Solicitud
                            ''report.Load(Server.MapPath("../Reporte/Solicitud.rpt"))
                            'report.SetDataSource(Dat)
                            ''report.SetDataSource(Dat.Tables("DATOS_PERSONAL"))
                            'visor.ReportSource = report
                            Fname = Server.MapPath("../Documentos/SolicitudExportado" & Format$(Now(), "dd-MM-yyyy") & ".pdf")
                            crDiskFileDestinationOptions = New DiskFileDestinationOptions
                            crDiskFileDestinationOptions.DiskFileName = Fname
                            crExportOptions = crReportDocument.ExportOptions
                            With crExportOptions
                                .DestinationOptions = crDiskFileDestinationOptions
                                .ExportDestinationType = ExportDestinationType.DiskFile
                                .ExportFormatType = ExportFormatType.PortableDocFormat
                            End With
                            crReportDocument.SetDataSource(Dat)
                            crReportDocument.Export()
                            Response.ClearContent()
                            Response.ClearHeaders()
                            Response.ContentType = "application/pdf"
                            Response.WriteFile(Fname)
                            Response.End()

                        End If
                    Case 3
                        crReportDocument = New ReportDocument
                        crReportDocument.Load(Server.MapPath("../Reporte/ContratoMarco.rpt"))
                        Dat = clsPantallaObjeto.reporteCONTRATOS(Request("IdFolio"))
                        If Dat.Tables.Count > 0 Then
                            Dat.Tables(0).TableName = "Reporte"

                            'Dim report As New ReportDocument
                            'report = New Solicitud
                            ''report.Load(Server.MapPath("../Reporte/Solicitud.rpt"))
                            'report.SetDataSource(Dat)
                            ''report.SetDataSource(Dat.Tables("DATOS_PERSONAL"))
                            'visor.ReportSource = report
                            Fname = Server.MapPath("../Documentos/Marco" & Format$(Now(), "dd-MM-yyyy") & ".pdf")
                            crDiskFileDestinationOptions = New DiskFileDestinationOptions
                            crDiskFileDestinationOptions.DiskFileName = Fname
                            crExportOptions = crReportDocument.ExportOptions
                            With crExportOptions
                                .DestinationOptions = crDiskFileDestinationOptions
                                .ExportDestinationType = ExportDestinationType.DiskFile
                                .ExportFormatType = ExportFormatType.PortableDocFormat
                            End With
                            crReportDocument.SetDataSource(Dat)
                            crReportDocument.Export()
                            Response.ClearContent()
                            Response.ClearHeaders()
                            Response.ContentType = "application/pdf"
                            Response.WriteFile(Fname)
                            Response.End()
                        End If
                    Case 4
                        crReportDocument = New ReportDocument
                        crReportDocument.Load(Server.MapPath("../Reporte/SolicitudPer.rpt"))
                        Dat = clsPantallaObjeto.ReporteSolicitudPer(Request("IdFolio"), 1)
                        If Dat.Tables.Count > 0 Then
                            Dat.Tables(0).TableName = "CARGO"
                            Dat.Tables(1).TableName = "COACREDITADO"
                            Dat.Tables(2).TableName = "EMPLEO_COACREDITADO"
                            Dat.Tables(3).TableName = "DISTRIBUIDOR"
                            Dat.Tables(4).TableName = "REFERENCIA_CREDITO_COA"
                            Dat.Tables(5).TableName = "DATOS_SOLICITANTE"
                            Dat.Tables(6).TableName = "DATOS_MORALES"
                            Dat.Tables(7).TableName = "REFERENCIAS_COMER"
                            Dat.Tables(8).TableName = "INFORMACION"


                            'Dim report As New ReportDocument
                            'report = New Solicitud
                            ''report.Load(Server.MapPath("../Reporte/Solicitud.rpt"))
                            'report.SetDataSource(Dat)
                            ''report.SetDataSource(Dat.Tables("DATOS_PERSONAL"))
                            'visor.ReportSource = report
                            Fname = Server.MapPath("../Documentos/SolicitudExportado" & Format$(Now(), "dd-MM-yyyy") & ".pdf")
                            crDiskFileDestinationOptions = New DiskFileDestinationOptions
                            crDiskFileDestinationOptions.DiskFileName = Fname
                            crExportOptions = crReportDocument.ExportOptions
                            With crExportOptions
                                .DestinationOptions = crDiskFileDestinationOptions
                                .ExportDestinationType = ExportDestinationType.DiskFile
                                .ExportFormatType = ExportFormatType.PortableDocFormat
                            End With
                            crReportDocument.SetDataSource(Dat)
                            crReportDocument.Export()
                            Response.ClearContent()
                            Response.ClearHeaders()
                            Response.ContentType = "application/pdf"
                            Response.WriteFile(Fname)
                            Response.End()

                        End If
                    Case 5
                        crReportDocument = New ReportDocument
                        crReportDocument.Load(Server.MapPath("../Reporte/SolicitudCredito.rpt"))


                        Dat = clsPantallaObjeto.ObtenDatosImpSolicitudCredito(Request("IdFolio"))
                        If Dat.Tables.Count > 0 Then
                            Dat.Tables(0).TableName = "PDK_TAB_DATOS_SOLICITANTE"
                            Dat.Tables(1).TableName = "PDK_TAB_RESIDENTES_EXTRANJEROS"
                            Dat.Tables(2).TableName = "PDK_TAB_PEPS"
                            Dat.Tables(3).TableName = "PDK_TAB_LUGAR_NACIMIENTO"
                            Dat.Tables(4).TableName = "PDK_TAB_FIEL"
                            Dat.Tables(5).TableName = "PDK_TAB_IDENTIFICACION_FISCAL"
                            Dat.Tables(6).TableName = "PDK_TAB_CONDUCTOR_RECURRENTE"
                            Dat.Tables(7).TableName = "PDK_TAB_REFERE_CONOCIDO"
                            Dat.Tables(8).TableName = "PDK_TAB_REFERE_PERSONALES"
                            Dat.Tables(9).TableName = "PDK_TAB_REFERE_FAMILIAR"
                            Dat.Tables(10).TableName = "PDK_TAB_EMPLEO"
                            Dat.Tables(11).TableName = "PDK_TAB_SOLICITANTE"
                            Dat.Tables(12).TableName = "PDK_TAB_DATOS_PERSONALES"
                            Dat.Tables(13).TableName = "PDK_ASESOR"
                            Dat.Tables(14).TableName = "PDK_VENDEDOR"
                            Dat.Tables(15).TableName = "PDK_AGENCIA"
                            Dat.Tables(16).TableName = "PDK_TAB_PERFIL_ECONOMICO"
                            Dat.Tables(17).TableName = "PDK_PRODUCTO"
                            Dat.Tables(18).TableName = "PDK_COTIZACION"

                            'validacion si es cliente bbva
                            Dim EsCliente As String = "0"
                            If (Dat.Tables(11).Rows.Count >= 0 And Dat.Tables(12).Rows.Count >= 0) Then
                                If (Dat.Tables(11).Rows(0).Item("NOMBRE1").ToString <> "" And Dat.Tables(11).Rows(0).Item("NOMBRE2").ToString <> "" And Dat.Tables(11).Rows(0).Item("APELLIDO_PATERNO").ToString <> "" And Dat.Tables(11).Rows(0).Item("APELLIDO_MATERNO").ToString <> "" And Dat.Tables(11).Rows(0).Item("RFC").ToString <> "" And Dat.Tables(11).Rows(0).Item("HOMOCLAVE").ToString <> "" And Dat.Tables(12).Rows(0).Item("CP").ToString <> "" And Dat.Tables(12).Rows(0).Item("FECHA_NACIMIENTO").ToString <> "") Then
                                    EsCliente = IIf(ValidaClienteBBVA(Dat.Tables(11).Rows(0).Item("NOMBRE1").ToString, Dat.Tables(11).Rows(0).Item("NOMBRE2").ToString, Dat.Tables(11).Rows(0).Item("APELLIDO_PATERNO").ToString, Dat.Tables(11).Rows(0).Item("APELLIDO_MATERNO").ToString, Dat.Tables(11).Rows(0).Item("RFC").ToString, Dat.Tables(11).Rows(0).Item("HOMOCLAVE").ToString, Dat.Tables(12).Rows(0).Item("CP").ToString, Dat.Tables(12).Rows(0).Item("FECHA_NACIMIENTO").ToString) = True, "1", "0")
                                End If
                            End If


                            Fname = Server.MapPath("../Documentos/SolicitudExportado" & Format$(Now(), "dd-MM-yyyy") & ".pdf")
                            crDiskFileDestinationOptions = New DiskFileDestinationOptions
                            crDiskFileDestinationOptions.DiskFileName = Fname
                            crExportOptions = crReportDocument.ExportOptions
                            With crExportOptions
                                .DestinationOptions = crDiskFileDestinationOptions
                                .ExportDestinationType = ExportDestinationType.DiskFile
                                .ExportFormatType = ExportFormatType.PortableDocFormat
                            End With
                            crReportDocument.SetDataSource(Dat)

                            Dim IMG As String = ""
                            If Dat.Tables(15).Rows.Count > 0 Then
                                IMG = Dat.Tables(15).Rows(0).Item("IMG_REP").ToString

                            End If

                            If IMG <> "" Then
                                crReportDocument.SetParameterValue("PicturePath", IMG)
                            Else
                                crReportDocument.SetParameterValue("PicturePath", "")
                            End If


                            crReportDocument.SetParameterValue("EsBBVA", EsCliente)
                            crReportDocument.Export()
                            Response.ClearContent()
                            Response.ClearHeaders()
                            Response.ContentType = "application/pdf"
                            Response.WriteFile(Fname)
                            Response.End()

                        End If

                End Select

            Catch ex As Exception
                Dim cade As String = ex.Message
            End Try

        End If
    End Sub
    Private Function ValidaClienteBBVA(nombre1 As String, nombre2 As String, Apaterno As String, Amaterno As String, rfc As String, homoclave As String, cp As String, fechanac As String) As Boolean
        ValidaClienteBBVA = False

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        Dim fecha As Date
        fecha = fechanac
        Dim FechaN = fecha.ToString("yyyy-MM-dd")
        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("Customer") + "?$filter=(customerName==" + nombre1 + " " + nombre2 + ",customerLastName==" + Apaterno + ",customerMotherLastName==" + Amaterno + ",federalTaxpayerRegistry==" + rfc + ",homonimia==" + homoclave + ",postalCode==" + cp + ",birthDate==" + FechaN + ")&$fields=id,person(id,segment)"
        'restGT.consumerID = "10000004"
        Dim respuestaBC As String = restGT.ConnectionGet(userID, iv_ticket1, String.Empty)

        Dim serializerBC As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jresultBC = serializerBC.Deserialize(Of Customer)(respuestaBC)

        ''Error
        Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim alert = srrSerialer.Deserialize(Of msjerr)(respuestaBC)
        If restGT.IsError Then
            If restGT.MensajeError <> "" Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "MensajeError", "<script type='text/javascript'> alert('" + restGT.MensajeError + "');</script>", True)
                'MsgBox("Error de WS - " & restGT.MensajeError)
            Else
                ClientScript.RegisterClientScriptBlock(Me.GetType, "MensajeError", "<script type='text/javascript'> alert('Error de WS - " + alert.message + " Estatus: " + alert.status + ".');</script>", True)
                'MsgBox("Error de WS - " & alert.message & " Estatus: " & alert.status & ".")
            End If

        End If
        Dim Cliente_BBVA As String = jresultBC.Person.id
        If (Cliente_BBVA <> "") Then
            ValidaClienteBBVA = True
        End If
        Return ValidaClienteBBVA
    End Function
End Class
Public Class Customer
    Public Person As Person = New Person

End Class
Public Class Person
    Public id As String
    Public segment As segment = New segment
End Class
Public Class segment
    Public name As name = New name
End Class
Public Class name
    Public id As String
End Class
Public Class msjerr
    Public message As String
    Public status As String
End Class