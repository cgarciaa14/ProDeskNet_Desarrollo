Imports ProdeskNet.BD
Imports System.Data
Imports ProdeskNet.Catalogos
Imports ProdeskNet.SN
Imports ProdeskNet.Seguridad

#Region "trackers"
'INC-B-2019:JDRA:Regresar.
'BUG-PD-233: RHERNANDEZ: 12/10/2017: SE QUITA OPCION DE DAR DOBLE CLICK EN PANTALLA DE CONTRATOS
'BUG-PD-417: DCORNEJO: 17/04/2018: SE AGREGA LA OPCION DE TURNAR EN ADJUNTAR DOCUMENTOS PARA DESEMBOLSO
#End Region

Public Class ImprimirContrato
    Inherits System.Web.UI.Page
    Dim BD As New clsManejaBD
    Dim es As New ProdeskNet.Catalogos.clsStatusSolicitud

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim intEnable As Integer
            Dim dataset As New DataSet
            Dim dsresulta As New DataSet
            Dim dsresult As New DataSet
            hdnIdFolio.Value = Request("IdFolio")
            hdnIdPantalla.Value = Request("idPantalla")
            hdnUsua.Value = Session("IdUsua")
            hdPantalla.Value = Request("pantalla")
            dataset = BD.EjecutarQuery("SELECT NOMBRE_SOLICI FROM PDK_TAB_DATOS_SOLICITANTE WHERE PDK_ID_SECCCERO=" & hdnIdFolio.Value)
            If dataset.Tables.Count > 0 AndAlso dataset.Tables(0).Rows.Count > 0 Then
                lblSoli.Text = hdnIdFolio.Value
                lblNombre.Text = dataset.Tables(0).Rows(0).Item("NOMBRE_SOLICI").ToString
            End If

            es.getStatusSol(Request("IdFolio"))
            Me.ldlstatusCre.Text = es.PStCredito
            Me.lblstatusDoc.Text = es.PStDocumento

            hdnEnable.Value = Request("Enable")
            If Session("Regresar") Is Nothing Then
                Session("Regresar") = Request.UrlReferrer.LocalPath
                hdRutaEntrada.Value = Request.UrlReferrer.LocalPath
            End If

            hdRutaEntrada.Value = Session("Regresar")

            Try
                dataset = BD.EjecutarQuery("EXEC sp_GenerarRura " & hdnIdFolio.Value & "," & hdnIdPantalla.Value & "," & hdnUsua.Value & "")
                If dataset.Tables(0).Rows.Count > 0 AndAlso dataset.Tables.Count > 0 Then
                    hdnResultado.Value = dataset.Tables(0).Rows(0).Item("RUTA")
                    hdnResultado1.Value = dataset.Tables(1).Rows(0).Item("RUTA2")
                    hdnResultado2.Value = dataset.Tables(2).Rows(0).Item("RUTA3")
                End If
            Catch ex As Exception
                hdnResultado.Value = Session("Regresar")
            End Try

            Try
                dsresulta = BD.EjecutarQuery("get_Path_Next_Tarea  " & hdPantalla.Value)
                If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
                    Dim Mostrar As String
                    Dim pantallas As String
                    Mostrar = dsresulta.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR").ToString
                    pantallas = dsresulta.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString
                    If Mostrar = 2 Then

                        hdnResultado.Value = dsresult.Tables(0).Rows(0).Item("RUTA")
                    Else
                        hdnResultado.Value = (dsresulta.Tables(0).Rows(0).Item("RUTA") & "?sol=" & hdnIdFolio.Value & "&IdPantalla=" & pantallas & "&usuario=" & hdnUsua.Value)
                        hdnResultado2.Value = (dsresulta.Tables(0).Rows(0).Item("RUTA") & "?sol=" & hdnIdFolio.Value & "&IdPantalla=" & pantallas & "&usuario=" & hdnUsua.Value)
                    End If
                End If

            Catch ex As Exception
                hdnResultado.Value = Session("Regresar")
            End Try

            Try
                intEnable = CInt(Request.QueryString("Enable"))

            Catch ex As Exception
                intEnable = 0
            End Try

            If (Request("Pantalla").ToString = "14") And intEnable = 0 Then
                'divActualizaColonia.Attributes.Add("style", "display:''")
                lblturnar.Visible = True
                ddlTurnar.Visible = True
                Dim clsquiz As New clsCuestionarioSolvsID()
                Dim objCombo As New clsParametros
                clsquiz._ID_PANT = CInt(hdPantalla.Value)
                Dim dtsres As New DataSet
                dtsres = clsquiz.getTurnar()
                If dtsres.Tables.Count > 0 Then
                    If dtsres.Tables(0).Rows.Count > 0 Then
                        objCombo.LlenaCombos(dtsres, "TURNAR_NOMBRE", "PDK_ID_TAREAS", ddlTurnar, True, True)
                    End If
                End If
            End If

            If intEnable = 1 Then
                cmbguardar.Attributes.Add("style", "display:none;")
                'btnAutorizar.Attributes.Add("style", "display:none;")
                btnCancelar.Attributes.Add("style", "display:none;")

            End If
        End If
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click

        'Response.Redirect("./consultaPantalla.aspx")
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("IdFolio")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)

    End Sub

    Protected Sub btnproc_Click(sender As Object, e As EventArgs)
        If ddlTurnar.Visible = False Then
            asignaTarea(0)
        Else
            asignaTarea(ddlTurnar.SelectedValue)
        End If

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
                cmbguardar.Disabled = False
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

                If muestrapant = 0 Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString & "');", True)
                ElseIf muestrapant = 2 Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "&sol=" & Val(Request("Sol")).ToString & "');", True)
                    'Response.Redirect("../aspx/consultaPanelControl.aspx")
                End If
            End If

        Catch ex As Exception
            cmbguardar.Attributes.Remove("disabled")
            btnRegresar.Attributes.Remove("disabled")
            cmbguardar.Disabled = False
            Master.MensajeError(mensaje)
        End Try
    End Sub

    'Private Sub cmbImprimir_Click(sender As Object, e As System.EventArgs) Handles cmbImprimir.Click
    '    ''If chekCto.Checked = False And chekPagare.Checked = False And chekAnexoA.Checked = False And chekAnexoB.Checked = False And chekAnexoC.Checked = False Then
    '    ''    Master.MensajeError("Debes de selecionar cualquier opción")
    '    ''    Exit Sub
    '    ''End If

    '    Try
    '        Dim tblHash As New Hashtable
    '        Dim strCadena As String = ""
    '        Dim strLlave As String = ""
    '        Dim strValor As String = ""
    '        'Dim dRenglon As New DataRow
    '        ''Dim i As Integer
    '        ''Dim tipoDocumento As Integer
    '        Dim strTablaCaracteristica As New StringBuilder
    '        Dim datSet As New DataSet
    '        datSet = BD.EjecutarQuery("SELECT FMT_FL_CVE, FMT_DS_DESCRIPCION, FMT_DS_MACHOTE, FMT_NO_TIPODOCUMENTO FROM CCTO_MACHOTE WHERE FMT_FL_CVE = 117 ORDER BY FMT_FL_CVE")
    '        If datSet.Tables.Count > 0 AndAlso datSet.Tables(0).Rows.Count > 0 Then

    '        End If




    '    Catch ex As Exception

    '    End Try



    'End Sub
    Protected Sub ddlTurnar_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
End Class