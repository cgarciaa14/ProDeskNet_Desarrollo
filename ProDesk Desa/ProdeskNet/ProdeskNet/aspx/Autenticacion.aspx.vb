Imports ProdeskNet.BD
Imports System.Data
Imports ProdeskNet.Catalogos
Imports ProdeskNet.WCF
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Configurcion
Imports ProdeskNet.SN
'RQADM-20: RHERNANDEZ: 19/05/17: SE CREA CODIGO BACK PARA LA CARGA Y CONTESTACION DE CUESTIONARIOS DE AUTENTICACION E IVR
'BUG-PD-128: RHERNANDEZ: 05/07/17: SE MODIFICA CR Y NUMERO DE REFERENCIA
'BUG-PD-174: RHERNANDEZ: 28/07/17 SE MODIFICA GANERACION DE NUMERO DE REFERENCIA Y SE CORRIGE PROBLEMA AL ALMACENAR EL CUESTIONARIO  
'BUG-PD-246: RHERNANDEZ: 25/10/17: SE CAMBIA ESTATUS AL APROBAR LA AUTENTICACION VIA CUESTIONARIO
'BUG-PD-390: RHERNANDEZ: 09/03/2018 SE CAMBIA IDAPP DE ResponderCuestionarioWS() PARA GENERAR CUESTIONARIOS ILIMITADOS
Partial Class aspx_Autenticacion
    Inherits System.Web.UI.Page
    Dim clien As New clsDatosCliente
    Dim sol As New clsStatusSolicitud
    Dim dtanswer As New DataSet
    Dim dtquiz As New DataSet
    Dim idPant As Integer
    Dim strerror As String
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim intEnable As Integer
            Try
                intEnable = CInt(Request.QueryString("Enable"))
            Catch ex As Exception
                intEnable = 0
            End Try
            idPant = Request("idPantalla")
            Dim dsresult As New DataSet
            Dim BD As New clsManejaBD
            clien.GetDatosCliente(Request("sol"))
            sol.getStatusSol(Request("sol"))
            hdPantalla.Value = Request("idPantalla")
            hdSolicitud.Value = Request("sol")
            Session("idsol") = hdSolicitud.Value
            hdusuario.Value = Request("usu")
            Me.lblSolicitud.Text = Request("sol")
            Me.lblCliente.Text = clien.propNombreCompleto
            Me.lblStCredito.Text = sol.PStCredito
            Me.lblStDocumento.Text = sol.PStDocumento

            Session.Add("idSol", hdSolicitud.Value)

            If Session("Regresar") Is Nothing Then
                Session("Regresar") = Request.UrlReferrer.LocalPath
            End If

            hdRutaEntrada.Value = Session("Regresar")


            Session("idSol") = hdSolicitud.Value

            dsresult = BD.EjecutarQuery("select A.PDK_PANT_NOMBRE,B.PDK_PAR_SIS_PARAMETRO,A.PDK_PANT_DOCUMENTOS from PDK_PANTALLAS A INNER JOIN PDK_PARAMETROS_SISTEMA B ON A.PDK_PANT_DOCUMENTOS=B.PDK_ID_PARAMETROS_SISTEMA AND B.PDK_PAR_SIS_ID_PADRE=24 where A.PDK_ID_PANTALLAS = " & hdPantalla.Value)
            If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
                Me.lblNomPantalla.Text = dsresult.Tables(0).Rows(0).Item("PDK_PANT_NOMBRE").ToString
            End If

            Dim dsresult2 As New DataSet
            Try
                dsresult2 = BD.EjecutarQuery("EXEC sp_GenerarRura  " & hdSolicitud.Value & "," & hdPantalla.Value & "," & hdusuario.Value & "")
                If dsresult2.Tables(0).Rows.Count > 0 AndAlso dsresult2.Tables.Count > 0 Then
                    hdnResultado.Value = dsresult2.Tables(0).Rows(0).Item("RUTA")
                    hdnResultado1.Value = dsresult2.Tables(1).Rows(0).Item("RUTA2")
                    hdnResultado2.Value = dsresult2.Tables(2).Rows(0).Item("RUTA3")
                End If

            Catch ex As Exception
                hdnResultado.Value = Session("Regresar")
            End Try
            Select Case CInt(Request("idPantalla"))
                Case 114
                    If intEnable = 0 Then
                        pantallaIVR(CInt(intEnable))
                    Else
                        CuestionarioAut.Visible = False
                        ConsultaCuestionario(1)
                        ddlOKIVR.Enabled = False
                        cmbguardar1.Attributes.Add("style", "display:none;")
                        btnCancelar.Attributes.Add("style", "display:none;")
                    End If
                Case 115
                    If intEnable = 0 Then
                        pantallaCuestionario(CInt(intEnable))
                    Else
                        CuestionarioIVR.Visible = False
                        ConsultaCuestionario(2)
                        cmbguardar1.Attributes.Add("style", "display:none;")
                        btnCancelar.Attributes.Add("style", "display:none;")
                    End If
            End Select
        End If
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
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

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                Master.MsjErrorRedirect(mensaje, "../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If

            dslink = objtarea.SiguienteTarea(Val(Request("sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("sol")))
            Dim dc As New clsDatosCliente
            dc.idSolicitud = Request("sol")
            dc.getDatosSol()

            If muestrapant = 0 Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString & "');", True)
            ElseIf muestrapant = 2 Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("sol") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "&sol=" & Val(Request("Sol")).ToString & "');", True)
                'Response.Redirect("../aspx/consultaPanelControl.aspx")
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
            cmbguardar1.Disabled = False
        End Try
    End Sub
    Public Sub pantallaIVR(ByVal intEnable As Integer)
        CuestionarioAut.Visible = False
        ConsultaCuestionario(1)
    End Sub
    Public Sub pantallaCuestionario(ByVal intEnable As Integer)
        CuestionarioIVR.Visible = False
        crearCuestionarioWS()
    End Sub
    Public Sub crearCuestionarioWS()
        Try
            Dim clsquiz As New ClsCuestionarioAutent()
            Dim no_cliente As String
            Dim rfc As String
            Dim clsguadaCuest As New clsCuestionariosAutenticacion()
            clsguadaCuest._PDK_ID_SECCERO = Val(Request("sol"))
            clsguadaCuest._OPC = 0
            Dim ds As New DataSet
            ds = clsguadaCuest.ManejaCuestionarioAut()
            If Not IsNothing(ds) Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count Then
                        no_cliente = ds.Tables(0).Rows(0).Item("CLIENTE_INCREDIT").ToString()
                        rfc = ds.Tables(0).Rows(0).Item("RFC").ToString()
                    Else
                        Throw New Exception("No cuenta con datos de cliente bbva para crear cuestionario")
                    End If
                Else
                    Throw New Exception("No cuenta con datos de cliente bbva para crear cuestionario")
                End If
            Else
                Throw New Exception("No cuenta con datos de cliente bbva para crear cuestionario")
            End If
            clsquiz.idApp = "CA"
            clsquiz.productType = "AUT"
            clsquiz.numberReference = System.Configuration.ConfigurationManager.AppSettings.Item("Entidad") & System.Configuration.ConfigurationManager.AppSettings.Item("CR") & "0" & formatsendws(Val(Request("Sol")).ToString, 10, 0)  '"0074001009606391151"
            clsquiz.regionalCenter.code = System.Configuration.ConfigurationManager.AppSettings.Item("CR") '"0010"
            clsquiz.customer.person.id = no_cliente '"D0041908" 
            clsquiz.customer.person.extendedData.rfc = rfc  '"AIZM660730FE5" 
            clsquiz.isForward = "true" ' RHERNANDEZ
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonBODY As String = serializer.Serialize(clsquiz)

            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriAutentQuiz").ToString()
            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonBODY)
            Dim errormessage As String
            If (restful.IsError) Then
                errormessage = restful.MensajeError
                cmbguardar1.Attributes.Add("style", "display:none;")
                Session("Enable") = 1
                ConsultaCuestionario(2)
                Throw New Exception(errormessage)
            Else
                Dim res As ClsResCuestionarioAutent = serializer.Deserialize(Of ClsResCuestionarioAutent)(respuesta)

                dtquiz.Tables.Add("PDK_CUEST_AUTENTICACION")
                dtquiz.Tables(0).Columns.Add("PDK_ID_SECCERO")
                dtquiz.Tables(0).Columns.Add("PDK_ID_PREGUNTA")
                dtquiz.Tables(0).Columns.Add("PDK_PREGUNTA")
                dtquiz.Tables(0).Columns.Add("PDK_ID_RESPUESTA")
                dtquiz.Tables(0).Columns.Add("PDK_RESPUESTA")
                dtquiz.Tables(0).Columns.Add("PDK_CLAVE_USUARIO")
                dtquiz.Tables(0).Columns.Add("PDK_TIPO_PREGUNTA")
                dtquiz.Tables(0).Columns.Add("PDK_NO_RESPUESTAS")
                dtquiz.Tables(0).Columns.Add("PDK_RESPUESTA_CORRECTA")
                dtquiz.Tables(0).Columns.Add("PDK_AYUDA_PREGUNTA")


                dtanswer.Tables.Add("PDK_CUEST_RESP")
                dtanswer.Tables(0).Columns.Add("PDK_ID_PREGUNTA")
                dtanswer.Tables(0).Columns.Add("PDK_PREGUNTA")
                dtanswer.Tables(0).Columns.Add("PDK_ID_RESPUESTA")
                dtanswer.Tables(0).Columns.Add("PDK_RESPUESTA")
                For i As Integer = 0 To CInt(res.questionnarie.numberQuestion) - 1
                    Dim rwq As DataRow = dtquiz.Tables(0).NewRow()
                    rwq("PDK_ID_SECCERO") = Request("sol").ToString()
                    rwq("PDK_ID_PREGUNTA") = res.questionnarie.questions(i).catalogItemBase.id.ToString()
                    rwq("PDK_PREGUNTA") = res.questionnarie.questions(i).catalogItemBase.name.ToString()
                    rwq("PDK_TIPO_PREGUNTA") = res.questionnarie.questions(i).typeAnswer.id.ToString()
                    rwq("PDK_NO_RESPUESTAS") = res.questionnarie.questions(i).numberOfReplies.ToString()
                    rwq("PDK_RESPUESTA_CORRECTA") = res.questionnarie.questions(i).correctAnswer.ToString()
                    rwq("PDK_AYUDA_PREGUNTA") = res.questionnarie.questions(i).help.ToString()
                    Dim no_respuestas As String = 1
                    For j As Integer = 0 To CInt(res.questionnarie.questions(i).answers.Count()) - 1
                        Dim rwa As DataRow = dtanswer.Tables(0).NewRow
                        If res.questionnarie.questions(i).answers(j).catalogItemBase.name <> "N/A" Then
                            rwa("PDK_ID_PREGUNTA") = res.questionnarie.questions(i).catalogItemBase.id.ToString()
                            rwa("PDK_PREGUNTA") = res.questionnarie.questions(i).catalogItemBase.name.ToString()
                            rwa("PDK_ID_RESPUESTA") = no_respuestas.ToString()
                            rwa("PDK_RESPUESTA") = res.questionnarie.questions(i).answers(j).catalogItemBase.name.ToString()
                            dtanswer.Tables(0).Rows.Add(rwa)
                            no_respuestas += 1
                        End If
                    Next
                    dtquiz.Tables(0).Rows.Add(rwq)
                Next
                gvCuestionario.DataSource = dtquiz
                gvCuestionario.DataBind()
                gvCuestionario.Columns(3).Visible = False
                gvCuestionario.Columns(4).Visible = False
                gvCuestionario.Columns(5).Visible = False
                gvCuestionario.Columns(6).Visible = False
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Function formatsendws(valor As String, lng As Integer, isDec As Integer, Optional ispercent As Integer = 0) As String
        Dim strresult As String = String.Empty
        Dim Pos As Integer = 0

        Select Case ispercent
            Case 0
                If isDec = 1 Then
                    Pos = InStr(valor, ".")
                    If Pos > 0 Then
                        strresult = ((valor).Substring(0, Pos) & (valor).Substring(Pos, 2)).Replace(".", "")
                    Else
                        strresult = valor & "00"
                    End If
                    strresult = New String("0"c, lng - Len(strresult)) & strresult
                Else
                    strresult = New String("0"c, lng - Len(valor)) & valor
                End If
            Case 1
                Pos = InStr(valor, ".")
                If Pos > 0 Then
                    Dim strdec As String = valor.Substring(Pos, valor.Length - Pos)
                    If strdec.Length < 4 Then
                        strdec = strdec & New String("0"c, 4 - Len(strdec))
                    End If

                    Dim strent As String = valor.Substring(0, Pos - 1)
                    strresult = New String("0"c, lng - Len(strent & strdec)) & (strent & strdec)
                Else
                    strresult = valor & "0000"
                    strresult = New String("0"c, lng - Len(strresult)) & strresult
                End If
        End Select

        Return strresult
    End Function

    Protected Sub gvCuestionario_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As DropDownList
            Dim gvrow As GridViewRow = CType(e.Row.Cells(2).NamingContainer, GridViewRow)
            ddl = CType(e.Row.FindControl("ddlRespuesta"), DropDownList)
            Dim dsres As New DataSet
            Dim query = (From num In dtanswer.Tables(0)
                 Where num.Item("PDK_ID_PREGUNTA") = e.Row.Cells(0).Text
                 Select num).CopyToDataTable
            dsres.Tables.Add(query)
            Dim objCombo As New clsParametros
            ddl.ToolTip = e.Row.Cells(6).Text
            If CInt(Request("Enable")) = 0 Then
                If CInt(Session("Enable")) = 1 Then
                    objCombo.LlenaCombos(dsres, "PDK_RESPUESTA", "PDK_ID_RESPUESTA", ddl)
                    ddl.Enabled = False
                Else
                    objCombo.LlenaCombos(dsres, "PDK_RESPUESTA", "PDK_ID_RESPUESTA", ddl, True)
                End If
            Else
                objCombo.LlenaCombos(dsres, "PDK_RESPUESTA", "PDK_ID_RESPUESTA", ddl)
                ddl.Enabled = False
            End If
        End If
    End Sub
    Public Function ValidaCuestionario() As Boolean
        Try
            strerror = ""
            ValidaCuestionario = False
            Dim dtfinal As New DataTable
            dtfinal.TableName = "PDK_CUEST_AUTENTICACION"
            dtfinal.Columns.Add("PDK_ID_SECCERO")
            dtfinal.Columns.Add("PDK_ID_PREGUNTA")
            dtfinal.Columns.Add("PDK_PREGUNTA")
            dtfinal.Columns.Add("PDK_ID_RESPUESTA")
            dtfinal.Columns.Add("PDK_RESPUESTA")
            dtfinal.Columns.Add("PDK_CLAVE_USUARIO")
            dtfinal.Columns.Add("PDK_TIPO_PREGUNTA")
            dtfinal.Columns.Add("PDK_NO_RESPUESTAS")
            dtfinal.Columns.Add("PDK_RESPUESTA_CORRECTA")
            dtfinal.Columns.Add("PDK_AYUDA_PREGUNTA")

            Dim pregsinresp As Integer = 0
            For i As Integer = 0 To gvCuestionario.Rows.Count - 1
                Dim rwq As DataRow = dtfinal.NewRow()
                rwq("PDK_ID_SECCERO") = Request("sol").ToString()
                rwq("PDK_ID_PREGUNTA") = gvCuestionario.Rows(i).Cells(0).Text.ToString()
                rwq("PDK_PREGUNTA") = gvCuestionario.Rows(i).Cells(1).Text.ToString()
                Dim ddl As DropDownList
                ddl = CType(gvCuestionario.Rows(i).FindControl("ddlRespuesta"), DropDownList)
                rwq("PDK_ID_RESPUESTA") = ddl.SelectedValue.ToString()
                rwq("PDK_RESPUESTA") = ddl.SelectedItem.Text.ToString()
                rwq("PDK_CLAVE_USUARIO") = Request("usu").ToString().ToString()
                rwq("PDK_TIPO_PREGUNTA") = gvCuestionario.Rows(i).Cells(3).Text.ToString()
                rwq("PDK_NO_RESPUESTAS") = gvCuestionario.Rows(i).Cells(4).Text.ToString()
                rwq("PDK_RESPUESTA_CORRECTA") = gvCuestionario.Rows(i).Cells(5).Text.ToString()
                rwq("PDK_AYUDA_PREGUNTA") = gvCuestionario.Rows(i).Cells(6).Text.ToString()
                dtfinal.Rows.Add(rwq)

                If ddl.SelectedValue = 0 Then
                    pregsinresp += 1
                End If
            Next
            If pregsinresp = 0 Then
                ValidaCuestionario = True
                dtquiz = New DataSet
                dtquiz.Tables.Add(dtfinal)
            Else
                ValidaCuestionario = False
                Throw New Exception("Error: Faltan responder " + pregsinresp.ToString + IIf(pregsinresp = 1, " pregunta", " preguntas"))
            End If
        Catch ex As Exception
            strerror = ex.Message
        End Try
    End Function

    Protected Sub btnproc_Click(sender As Object, e As EventArgs) Handles btnproc.Click
        Try
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
            If ValTareas = 1 Then
                Select Case CInt(Request("idPantalla"))
                    Case 114
                        Dim ds_siguienteTarea As DataSet
                        ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & CInt(Request("idPantalla")).ToString)
                        If GuardaCuestionario(1) Then
                            If ddlOKIVR.SelectedValue = 0 Then
                                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))
                            ElseIf ddlOKIVR.SelectedValue = 1 Then
                                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
                            Else
                                Throw New Exception("Debes contestar todas las preguntas para continuar.")
                            End If
                        Else
                            Throw New Exception(strerror)
                        End If

                    Case 115
                        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
                        If ValidaCuestionario() Then
                            Dim resquiz As Boolean = ResponderCuestionarioWS() 'RHERNANDEZ CAMBIO URGENTE
                            If resquiz Then
                                Dim ds_siguienteTarea As DataSet
                                ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & CInt(Request("idPantalla")).ToString)
                                Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
                                Solicitudes.Estatus = 231
                                Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
                                Solicitudes.ManejaTarea(4)
                                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
                            ElseIf resquiz = False And strerror = "" Then
                                Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
                                Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
                                Solicitudes.Estatus = 104
                                Solicitudes.Estatus_Cred = 104
                                Solicitudes.Comentario = strerror
                                Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
                                Solicitudes.ManejaTarea(6)

                                Master.MsjErrorRedirect("Solicitud Rechazada", "../aspx/consultaPanelControl.aspx")
                            Else
                                If strerror.Contains("RECHAZADA") Then
                                    Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
                                    Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
                                    Solicitudes.Estatus = 104
                                    Solicitudes.Estatus_Cred = 104
                                    Solicitudes.Comentario = strerror
                                    Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
                                    Solicitudes.ManejaTarea(6)
                                    Master.MsjErrorRedirect(strerror, "../aspx/consultaPanelControl.aspx")
                                ElseIf strerror.Contains("APROBADA") Then
                                    Dim ds_siguienteTarea As DataSet
                                    ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & CInt(Request("idPantalla")).ToString)
                                    Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
                                    Solicitudes.Estatus = 231
                                    Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
                                    Solicitudes.ManejaTarea(4)
                                    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
                                Else
                                    Throw New Exception(strerror)
                                End If
                            End If
                        Else
                            Throw New Exception(strerror)
                        End If
                End Select
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
            strerror = ""
            cmbguardar1.Disabled = False
        End Try
    End Sub
    Public Function GuardaCuestionario(ByVal opcion As Integer) As Boolean
        GuardaCuestionario = False
        strerror = ""
        Try
            Select Case opcion
                Case 1
                    Dim clsguadaCuest As New clsCuestionariosAutenticacion()
                    clsguadaCuest._PDK_ID_SECCERO = Request("sol").ToString
                    clsguadaCuest._PDK_ID_PREGUNTA = 0
                    clsguadaCuest._PDK_PREGUNTA = "¿Autenticación IVR exitosa?"
                    clsguadaCuest._PDK_ID_RESPUESTA = ddlOKIVR.SelectedValue.ToString
                    clsguadaCuest._PDK_RESPUESTA = ddlOKIVR.SelectedItem.Text
                    clsguadaCuest._PDK_CLAVE_USUARIO = Session("IdUsua").ToString
                    clsguadaCuest._PDK_TIPO_PREGUNTA = "IVR"
                    clsguadaCuest._PDK_NO_RESPUESTAS = 2
                    clsguadaCuest._PDK_RESPUESTA_CORRECTA = 1
                    clsguadaCuest._OPC = 1
                    Dim dsres As DataSet = clsguadaCuest.ManejaCuestionarioAut()
                    If dsres.Tables.Count > 0 Then
                        If dsres.Tables(0).Rows.Count > 0 Then
                            If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                                GuardaCuestionario = True
                            Else
                                Throw New Exception(dsres.Tables(0).Rows(0).Item("RESULT").ToString())
                            End If
                        End If
                    End If
                Case 2
                    For i As Integer = 0 To dtquiz.Tables(0).Rows.Count - 1
                        Dim clsguadaCuest As New clsCuestionariosAutenticacion
                        clsguadaCuest._PDK_ID_SECCERO = Request("sol").ToString()
                        clsguadaCuest._PDK_ID_PREGUNTA = dtquiz.Tables(0).Rows(i).Item("PDK_ID_PREGUNTA").ToString()
                        clsguadaCuest._PDK_PREGUNTA = dtquiz.Tables(0).Rows(i).Item("PDK_PREGUNTA").ToString()
                        clsguadaCuest._PDK_ID_RESPUESTA = dtquiz.Tables(0).Rows(i).Item("PDK_ID_RESPUESTA").ToString()
                        clsguadaCuest._PDK_RESPUESTA = dtquiz.Tables(0).Rows(i).Item("PDK_RESPUESTA").ToString()
                        clsguadaCuest._PDK_CLAVE_USUARIO = Session("IdUsua").ToString()
                        clsguadaCuest._PDK_TIPO_PREGUNTA = dtquiz.Tables(0).Rows(i).Item("PDK_TIPO_PREGUNTA").ToString()
                        clsguadaCuest._PDK_NO_RESPUESTAS = dtquiz.Tables(0).Rows(i).Item("PDK_NO_RESPUESTAS").ToString()
                        clsguadaCuest._PDK_RESPUESTA_CORRECTA = dtquiz.Tables(0).Rows(i).Item("PDK_RESPUESTA_CORRECTA").ToString()
                        clsguadaCuest._PDK_AYUDA_PREGUNTA = dtquiz.Tables(0).Rows(i).Item("PDK_AYUDA_PREGUNTA").ToString()
                        clsguadaCuest._OPC = 1
                        Dim dsres As DataSet = clsguadaCuest.ManejaCuestionarioAut()
                        If dsres.Tables.Count > 0 Then
                            If dsres.Tables(0).Rows.Count > 0 Then
                                If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                                    GuardaCuestionario = True
                                Else
                                    GuardaCuestionario = False
                                    Throw New Exception(dsres.Tables(0).Rows(0).Item("RESULT").ToString())
                                    Exit For
                                End If
                            End If
                        End If
                    Next
            End Select
        Catch ex As Exception
            strerror = ex.Message
            Return False
        End Try
    End Function
    Public Function ResponderCuestionarioWS() As Boolean
        Try
            strerror = ""
            Dim no_cliente As String
            Dim rfc As String
            Dim clsguadaCuest As New clsCuestionariosAutenticacion()
            clsguadaCuest._PDK_ID_SECCERO = Val(Request("sol"))
            clsguadaCuest._OPC = 0
            Dim ds As New DataSet
            ds = clsguadaCuest.ManejaCuestionarioAut()
            If Not IsNothing(ds) Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count Then
                        no_cliente = ds.Tables(0).Rows(0).Item("CLIENTE_INCREDIT").ToString()
                        rfc = ds.Tables(0).Rows(0).Item("RFC").ToString()
                    Else
                        Throw New Exception("No cuenta con datos de cliente bbva para crear cuestionario")
                    End If
                Else
                    Throw New Exception("No cuenta con datos de cliente bbva para crear cuestionario")
                End If
            Else
                Throw New Exception("No cuenta con datos de cliente bbva para crear cuestionario")
            End If
            ResponderCuestionarioWS = False
            Dim clsanswerquiz As New EnvioRespuestaAutent
            clsanswerquiz.idApp = "CA" 'EK CAMBIO BUG-PD-390
            clsanswerquiz.productType = "CON"
            clsanswerquiz.numberReference = System.Configuration.ConfigurationManager.AppSettings.Item("Entidad") & System.Configuration.ConfigurationManager.AppSettings.Item("CR") & "0" & formatsendws(Val(Request("Sol")).ToString, 10, 0) '"0074001009606391151"  ' RHERNANDEZ CAMBIO URGENTE
            clsanswerquiz.regionalCenter.code = System.Configuration.ConfigurationManager.AppSettings.Item("CR") '"0010"
            clsanswerquiz.customer.person.id = no_cliente '"D0041908"
            clsanswerquiz.customer.person.extendedData.rfc = rfc '"AIZM660730FE5"
            clsanswerquiz.isForward = "false"
            clsanswerquiz.questionnarie.numberOfRepliesCustomer = dtquiz.Tables(0).Rows.Count.ToString("D3")
            For i As Integer = 0 To dtquiz.Tables(0).Rows.Count - 1
                Dim answers As New questionsanswers
                answers.answerCustomer = dtquiz.Tables(0).Rows(i).Item("PDK_ID_PREGUNTA").ToString + dtquiz.Tables(0).Rows(i).Item("PDK_TIPO_PREGUNTA").ToString + CInt(dtquiz.Tables(0).Rows(i).Item("PDK_NO_RESPUESTAS")).ToString("D3") + CInt(dtquiz.Tables(0).Rows(i).Item("PDK_RESPUESTA_CORRECTA")).ToString("D3") + CInt(dtquiz.Tables(0).Rows(i).Item("PDK_ID_RESPUESTA")).ToString("D3")
                clsanswerquiz.questionnarie.questions.Add(answers)
            Next
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonBODY As String = serializer.Serialize(clsanswerquiz)

            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriRespAutentQuiz").ToString()
            restful.buscarHeader("ResponseWarningCode")
            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonBODY)
            Dim errormessage As String
            If (restful.IsError) Then
                If GuardaCuestionario(2) Then
                    errormessage = restful.MensajeError
                    Throw New Exception(errormessage)
                Else
                    Throw New Exception(strerror)
                End If
            Else
                Dim res As ResEnvioRespuestaAutent = serializer.Deserialize(Of ResEnvioRespuestaAutent)(respuesta)
                If GuardaCuestionario(2) Then
                    If restful.valorHeader = "CSA0058" Or restful.valorHeader = "CSA0059" Then
                        ResponderCuestionarioWS = True
                    ElseIf restful.valorHeader = "CSE0589" Or restful.valorHeader = "CSE0591" Or restful.valorHeader = "CSA0062" Then
                        strerror = restful.MensajeError
                        ResponderCuestionarioWS = False
                    End If
                Else
                    Throw New Exception(strerror)
                End If
            End If
        Catch ex As Exception
            strerror = ex.Message
            ResponderCuestionarioWS = False
        End Try
    End Function
    Public Sub ConsultaCuestionario(ByVal intopc As Integer)
        Try
            Select Case intopc
                Case 1 'IVR
                    Dim clsguadaCuest As New clsCuestionariosAutenticacion
                    clsguadaCuest._PDK_ID_SECCERO = CInt(Request("sol").ToString)
                    clsguadaCuest._OPC = 2
                    Dim dsres As New DataSet
                    dsres = clsguadaCuest.ManejaCuestionarioAut()
                    If Not IsNothing(dsres) Then
                        If dsres.Tables.Count > 0 Then
                            If dsres.Tables(0).Rows.Count > 0 Then
                                For i As Integer = 0 To dsres.Tables(0).Rows.Count - 1
                                    If dsres.Tables(0).Rows(i).Item("PDK_TIPO_PREGUNTA").ToString = "IVR" Then
                                        ddlOKIVR.SelectedValue = dsres.Tables(0).Rows(i).Item("PDK_ID_RESPUESTA").ToString
                                    End If
                                Next
                            End If
                        End If
                    End If
                Case 2 'Cuestionario
                    dtquiz.Tables.Add("PDK_CUEST_AUTENTICACION")
                    dtquiz.Tables(0).Columns.Add("PDK_ID_SECCERO")
                    dtquiz.Tables(0).Columns.Add("PDK_ID_PREGUNTA")
                    dtquiz.Tables(0).Columns.Add("PDK_PREGUNTA")
                    dtquiz.Tables(0).Columns.Add("PDK_ID_RESPUESTA")
                    dtquiz.Tables(0).Columns.Add("PDK_RESPUESTA")
                    dtquiz.Tables(0).Columns.Add("PDK_CLAVE_USUARIO")
                    dtquiz.Tables(0).Columns.Add("PDK_TIPO_PREGUNTA")
                    dtquiz.Tables(0).Columns.Add("PDK_NO_RESPUESTAS")
                    dtquiz.Tables(0).Columns.Add("PDK_RESPUESTA_CORRECTA")
                    dtquiz.Tables(0).Columns.Add("PDK_AYUDA_PREGUNTA")

                    dtanswer.Tables.Add("PDK_CUEST_RESP")
                    dtanswer.Tables(0).Columns.Add("PDK_ID_PREGUNTA")
                    dtanswer.Tables(0).Columns.Add("PDK_PREGUNTA")
                    dtanswer.Tables(0).Columns.Add("PDK_ID_RESPUESTA")
                    dtanswer.Tables(0).Columns.Add("PDK_RESPUESTA")
                    Dim clsguadaCuest As New clsCuestionariosAutenticacion
                    clsguadaCuest._PDK_ID_SECCERO = CInt(Request("sol").ToString)
                    clsguadaCuest._OPC = 2
                    Dim dsres As New DataSet
                    dsres = clsguadaCuest.ManejaCuestionarioAut()
                    If Not IsNothing(dsres) Then
                        If dsres.Tables.Count > 0 Then
                            If dsres.Tables(0).Rows.Count > 0 Then
                                For i As Integer = 0 To dsres.Tables(0).Rows.Count - 1
                                    Dim rwq As DataRow = dtquiz.Tables(0).NewRow()
                                    If dsres.Tables(0).Rows(i).Item("PDK_TIPO_PREGUNTA").ToString <> "IVR" Then
                                        rwq("PDK_ID_SECCERO") = CInt(Request("sol")).ToString()
                                        rwq("PDK_ID_PREGUNTA") = CInt(dsres.Tables(0).Rows(i).Item("PDK_ID_PREGUNTA")).ToString("D5")
                                        rwq("PDK_PREGUNTA") = dsres.Tables(0).Rows(i).Item("PDK_PREGUNTA").ToString()
                                        rwq("PDK_TIPO_PREGUNTA") = dsres.Tables(0).Rows(i).Item("PDK_TIPO_PREGUNTA").ToString()
                                        rwq("PDK_NO_RESPUESTAS") = dsres.Tables(0).Rows(i).Item("PDK_NO_RESPUESTAS").ToString()
                                        rwq("PDK_RESPUESTA_CORRECTA") = dsres.Tables(0).Rows(i).Item("PDK_RESPUESTA_CORRECTA").ToString()
                                        rwq("PDK_AYUDA_PREGUNTA") = dsres.Tables(0).Rows(i).Item("PDK_AYUDA_PREGUNTA").ToString()
                                        Dim rwa As DataRow = dtanswer.Tables(0).NewRow
                                        rwa("PDK_ID_PREGUNTA") = CInt(dsres.Tables(0).Rows(i).Item("PDK_ID_PREGUNTA")).ToString("D5")
                                        rwa("PDK_PREGUNTA") = dsres.Tables(0).Rows(i).Item("PDK_PREGUNTA").ToString()
                                        rwa("PDK_ID_RESPUESTA") = dsres.Tables(0).Rows(i).Item("PDK_ID_RESPUESTA").ToString()
                                        rwa("PDK_RESPUESTA") = dsres.Tables(0).Rows(i).Item("PDK_RESPUESTA").ToString()
                                        dtanswer.Tables(0).Rows.Add(rwa)
                                        dtquiz.Tables(0).Rows.Add(rwq)
                                    End If
                                Next
                            End If
                        End If
                    End If
                    gvCuestionario.DataSource = dtquiz
                    gvCuestionario.DataBind()
                    gvCuestionario.Columns(3).Visible = False
                    gvCuestionario.Columns(4).Visible = False
                    gvCuestionario.Columns(5).Visible = False
                    gvCuestionario.Columns(6).Visible = False
            End Select
        Catch ex As Exception
            Master.MensajeError(strerror)
        End Try
    End Sub
End Class
