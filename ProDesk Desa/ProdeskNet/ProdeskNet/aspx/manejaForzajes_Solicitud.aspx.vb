#Region "trackers"
'BBV-P-423 RQADM-36: AVH: 31/01/2017  Se crea pantalla para forzajes en Admision
'BUG-PD-14 JBB 2017/03/06 Se Agrega los 7 días  para precalificación 
'BUG-PD-17 JRHM 16/03/17 Correccion a busqueda por fecha y rfc
'BUG PD-30 CGARCIA 10/04/2017 Se agrego un evento más para que de inicio no se seleccione ningun trámite 
'BUG-PD-26 MAPH 07/04/17 : Cambios solicitados por JBB Redireccionamiento dinamico 
'BUG-PD-37 JBB 24/04/17  : Correciones en la pantalla de forzajes    
'BUG-PD-41 JBB 27/04/17  : Se elimina codigo inesesario en el redirect  cuando la pantalla se muestra 2 
'BUG-PD-62 ERODRIGUEZ 05/06/2017 : Se agrego paginacion para grid
'BUG-PD-86: AVEGA: 10/06/2017 Se agrega cambio de estatus
'BUG-PD-125: JBEJAR 29/06/2017 Se agrega validacion al boton procesar. 
'RQ-INB203INB204INB209 JBEJAR: 18/07/2017 correciones redirect
'RQ-INB219 JBEJAR 28/07/2017 Requerimiento mostrar las solicitudes que fueron forzadas y en que forzaje fueron
'BUG-PD-179:JBEJAR:10/08/2017  Se implementa log de forzajes. 
'BUG-PD-219:JBEJAR:03/10/2017  mejora de log forzajes.  
'BUG-PD-281:JBEJAR:28/11/2017  Se elimina valnegocio para evitar avanzar dos veces.  
'BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit
'BUG-PD-367 GVARGAS 22/02/2018 Correcion redirect forzar
#End Region


Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Data
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlInputFile
Imports ProdeskNet.BD

Partial Class aspx_manejaForzajes_Solicitud
    Inherits System.Web.UI.Page
    Public Pantalla As Integer = 77
    Public BD As New clsManejaBD


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("Regresar") Is Nothing Then
            Session("Regresar") = Request.UrlReferrer.LocalPath
        End If

        hdPantalla.Value = Pantalla

        hdusuario.Value = Session("IdUsua")

        If Not IsPostBack Then
            'Buscar()
        End If
        ValidaCampos()

    End Sub
    Public Sub LimpiaCampos()
        Me.txtNombre.Text = Nothing
        Me.txtNoSol.Text = Nothing
        Me.txtRFC.Text = Nothing
        Me.hfFeIni.Value = Nothing
        Me.hfFeFin.Value = Nothing
        Me.txtComentarios.Text = Nothing

        'gridprueba.DataSource = Nothing
        'gridprueba.DataBind()

        Buscar(0)

    End Sub
    Public Sub Buscar(IntBandera As Integer)
        Dim objFlujos As New clsSolicitudes(0)
        Dim ds As DataSet

        If ValidaCampos() Then
            objFlujos.PDK_ID_SOLICITUD = Val(txtNoSol.Text)
            objFlujos.Nombre = txtNombre.Text
            objFlujos.RFC = txtRFC.Text

            If hfFeFin.Value <> "" And hfFeIni.Value <> "" Then
                objFlujos.FechaIni = hfFeIni.Value
                objFlujos.FechaFin = hfFeFin.Value
            End If
            objFlujos.PDK_ID_PANTALLA = Pantalla

            ds = objFlujos.ConsultaSolicitud(4)
            Session("dtsConsulta") = ds
            Me.gvForzajes.DataSource = ds
            Me.gvForzajes.DataBind()



            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows.Count = 1 Then
                        'Me.dvUC.Style.Add("display", "block")



                        Context.Session("idsol") = Val(txtNoSol.Text)
                        hdSolicitud.Value = Val(txtNoSol.Text)

                        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload('tbValidarObjetos','hdPantalla,hdSolicitud,hdusuario','per1','')", True)

                        objFlujos.PDK_ID_SOLICITUD = Val(txtNoSol.Text)
                        ds = objFlujos.ConsultaSolicitud(4)
                        Me.gvForzajes.DataSource = ds
                        Me.gvForzajes.DataBind()

                        Dim dsComentarios As DataSet
                        objFlujos.PDK_ID_SOLICITUD = Val(txtNoSol.Text)
                        dsComentarios = objFlujos.ConsultaDocumento(1)
                        'Me.txtComentarios.Text = dsComentarios.Tables(0).Rows(0).Item("PDK_COMENT_FORZAJES")
                        Me.txtNoSol.Text = ds.Tables(0).Rows(0).Item("FolioSolicitud")
                        If IntBandera = 1 Then
                            DirectCast(gvForzajes.Rows(0).Cells(7).FindControl("chkSelecciona"), CheckBox).Checked = True
                        ElseIf IntBandera = 0 Then


                        End If




                        cargaGV()

                    End If
                Else
                    'gridprueba.DataSource = Nothing
                    'gridprueba.DataBind()
                    'Me.btnForzar.Visible = True
                End If
            Else
                'gridprueba.DataSource = Nothing
                'gridprueba.DataBind()
            End If
        End If
    End Sub
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs)
        Buscar(0)
    End Sub
    Protected Sub chkSelecciona_CheckedChanged(sender As Object, e As EventArgs)
        Dim _Chk As New CheckBox
        Dim Sol As Integer
        Dim objFlujos As New clsSolicitudes(0)
        Dim ds As DataSet

        For i = 0 To gvForzajes.Rows.Count - 1
            _Chk = gvForzajes.Rows(i).Cells(7).FindControl("chkSelecciona")

            If _Chk.Checked = True Then
                Sol = Convert.ToInt16(gvForzajes.Rows(i).Cells(0).Text)
                Context.Session("idsol") = Sol
                Me.txtNoSol.Text = Sol
                Buscar(1)
                tbValidarObjetos.Visible = True
                Exit For
            Else
                tbValidarObjetos.Visible = False
            End If
        Next
    End Sub
    Public Sub cargaGV()

        Dim objFlujos As New clsSolicitudes(0)
        Dim dsDoc As DataSet

        'Me.Div2.Visible = True

        objFlujos.PDK_ID_SOLICITUD = Me.txtNoSol.Text
        objFlujos.PDK_ID_PANTALLA = Pantalla
        objFlujos.PDK_CLAVE_USUARIO = Session("IdUsua")

        dsDoc = objFlujos.ConsultaDocumento(1)


        'Me.gridprueba.DataSource = dsDoc
        'Me.gridprueba.DataBind()
    End Sub

    Protected Sub btnAdelantar_Click(sender As Object, e As EventArgs)

        Try
            btnAdelantarCliente.Disabled = True 'BUG-PD-125 
            If txtNoSol.Text = "" Then
                'Me.mpoForzaje.Hide()
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "hideDivCancela();", True)
                Master.MensajeError("Falta seleccionar una solicitud")
                btnAdelantarCliente.Disabled = False
                Exit Sub
            End If

            'SE VALIDA QUE YA SE ENTREGARON LOS DOCUMENTOS OBLIGATORIOS DE LA PANTALLA
            Dim objFlujos1 As New clsSolicitudes(0)
            Dim DS As DataSet

            objFlujos1.PDK_ID_SOLICITUD = Me.txtNoSol.Text
            objFlujos1.PDK_ID_PANTALLA = Pantalla
            objFlujos1.PDK_CLAVE_USUARIO = Session("IdUsua")

            DS = objFlujos1.ConsultaDocumento(2)

            If objFlujos1.ERROR_SOL <> "" Then
                'pup.HidePopupWindow()
                'Me.mpoForzaje.Hide()
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "hideDivCancela();", True)
                Master.MensajeError(objFlujos1.ERROR_SOL)
                btnAdelantarCliente.Disabled = False
                Buscar(0)
                Exit Sub
            End If


            Dim objFlujos2 As New clsSolicitudes(0)
            Dim DS2 As DataSet

            objFlujos2.PDK_ID_SOLICITUD = Me.txtNoSol.Text
            objFlujos2.PDK_ID_PANTALLA = Pantalla
            objFlujos2.PDK_CLAVE_USUARIO = Session("IdUsua")
            objFlujos2.NombreUsu = txtusua.Text
            objFlujos2.Contraseña = txtpass.Text
            objFlujos2.Forzaje = 1
            DS2 = objFlujos2.ManejaTarea(2)


            If objFlujos2.ERROR_SOL <> "" Then
                'pup.HidePopupWindow()
                'Me.mpoForzaje.Hide()
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "hideDivCancela();", True)
                Master.MensajeError(objFlujos2.ERROR_SOL)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload('tbValidarObjetos','hdPantalla,hdSolicitud,hdusuario','per1','')", True)
                btnAdelantarCliente.Disabled = False 'BUG-PD-125 
                Exit Sub
            End If



            Dim objFlujos3 As New clsSolicitudes(0)
            objFlujos3.PDK_ID_PANTALLA = Pantalla
            objFlujos3.Comentario = txtComentarios.Text
            objFlujos3.PDK_ID_SOLICITUD = Me.txtNoSol.Text
            objFlujos3.ConsultaDocumento(3)

           

            'Master.MensajeError("PROCESO EJECUTADO CORRECTAMENTE")

            'Me.mpoForzaje.Hide()
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "hideDivCancela();", True)
           
        Catch ex As Exception
            Master.MensajeError(ex.Message)
            btnAdelantarCliente.Disabled = False 'BUG-PD-125 
        End Try

        Inserta_Forzaje()
    End Sub
    Public Sub Inserta_Forzaje() 'Log de forzajes  
        Dim objForzaje As New ProdeskNet.SN.clsForzaje()
        Dim dstareaAnterior As New DataSet
        Dim dslinkForzaje As New DataSet
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()

        dstareaAnterior = BD.EjecutarQuery("exec SpGetTareaAnteriorReplica " & hdSolicitud.Value) 'Consultamos tarea anterior del store para guardar en el log.
        dslinkForzaje = objtarea.SiguienteTarea(hdSolicitud.Value) 'Siguiente tarea 
        objForzaje.Id_Solicitud = hdSolicitud.Value 'Solicitud que se forzara 
        objForzaje.Tipo_Forzaje = "Admision" 'Tipo de forzaje
        objForzaje.Tarea_Anterior = dstareaAnterior.Tables(0).Rows(0).Item("PDK_ID_TAREAS") 'Valor tarea anterior 
        objForzaje.Tarea_Siguiente = dslinkForzaje.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString 'Valor tarea siguiente.  
        objForzaje.Usuario_Forza = hdusuario.Value 'Usuario que forzo la tarea. 
        objForzaje.insertaForzaje() 'funcion que guarda los resultados en el sp   

        redirect()

    End Sub

    Protected Sub redirect()

        Dim dsresult As New DataSet
        Dim dsresulta As New DataSet
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim strLocation As String = String.Empty


        dslink = objtarea.SiguienteTarea(hdSolicitud.Value)

        muestrapant = dslink.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR")

        ModificaTarea()



        If muestrapant = 0 Then
            'CAMBIO URGENTE 
            strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & hdSolicitud.Value & "&usuario=" & hdusuario.Value)
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "PopUpLetreroRedirect('PROCESO EJECUTADO CORRECTAMENTE', '" + strLocation + "');", True)
            'Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & hdSolicitud.Value & "&usuario=" & hdusuario.Value)

        ElseIf muestrapant = 2 Then
            strLocation = ("../aspx/consultaPanelControl.aspx")
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "PopUpLetreroRedirect('PROCESO EJECUTADO CORRECTAMENTE', '" + strLocation + "');", True)
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
        End If


    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        'Me.mpoForzaje.Hide()
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "hideDivCancela();", True)
    End Sub

    Protected Sub btnLimpiar_Click1(sender As Object, e As EventArgs)
        LimpiaCampos()
        ValidaCampos()
        'Buscar()
        Context.Session("idsol") = Nothing
        Me.txtFechaIni.Visible = False
        Me.txtFechaFin.Visible = False

        Me.tbFechaInicio.Visible = True
        Me.tbFechaFin.Visible = True
        Dim _Chk As New CheckBox
        For i = 0 To gvForzajes.Rows.Count - 1
            _Chk = gvForzajes.Rows(i).Cells(7).FindControl("chkSelecciona")
            _Chk.Checked = False
        Next
        txtNoSol.Text = Nothing
        tbValidarObjetos.Visible = False
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
    Protected Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Dim bandera As Integer
        Dim _Chk As New CheckBox
        If Not IsPostBack Then
            For i = 0 To gvForzajes.Rows.Count - 1
                _Chk = gvForzajes.Rows(i).Cells(7).FindControl("chkSelecciona")
                If _Chk.Checked = True Then
                    _Chk.Checked = False
                Else
                    _Chk.Checked = False
                End If
            Next
            bandera = 1
            ViewState("vwsBandera") = bandera
            tbValidarObjetos.Visible = False
        Else
            bandera = CInt(ViewState("vwsBandera").ToString())
            If bandera = 1 Then
                For i = 0 To gvForzajes.Rows.Count - 1
                    _Chk = gvForzajes.Rows(i).Cells(7).FindControl("chkSelecciona")
                    If _Chk.Checked = True Then
                        _Chk.Checked = False
                    Else
                        _Chk.Checked = False
                    End If
                Next
                bandera = bandera + 1
                ViewState("vwsBandera") = bandera
            End If
        End If
    End Sub

    Protected Sub gvForzajes_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvForzajes.PageIndexChanging

        If CType(Session("dtsConsulta"), DataSet) Is Nothing Then
            Buscar(0)
        End If

        gvForzajes.PageIndex = e.NewPageIndex
        gvForzajes.DataSource = CType(Session("dtsConsulta"), DataSet)
        gvForzajes.DataBind()
    End Sub

    Public Sub ModificaTarea()
        Dim objModifica As New clsSolicitudes(0)
        objModifica.PDK_ID_SOLICITUD = hdSolicitud.Value
        objModifica.Estatus = 231
        objModifica.PDK_CLAVE_USUARIO = Session("IdUsua")
        objModifica.ManejaTarea(4)
    End Sub

End Class
