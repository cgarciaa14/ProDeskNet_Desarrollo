Imports ProdeskNet.Catalogos
Imports ProdeskNet.BD
Imports System.Data
Imports ProdeskNet.SN
Imports ProdeskNet.Configurcion
Imports System.Web.UI.HtmlControls.HtmlInputFile
Imports ProdeskNet.Seguridad
Imports System.Data.SqlClient

'BBV-P-423: RQADM-37: JRHM: 17/03/2017 Se crea pantalla "Solicitud vs ID"
'BBV-P-423: RQADM-22: JRHM: 24/03/2017 SE MODIFICO LOGICA DE LA PAGINA AL MOMENTO DE RESPONDER PREGUNTAS
'BUG-PD-25 JRHM 04/04/17 SE MODIFICO APARIENCIA Y FUNCIONALIDAD DE LA TABLA DE VALIDACION DE DOCUMENTOS
'BUG-PD-29 JRHM 12/04/17 Se modifico leyenda de mensaje de declinacion de solicitud.
'BUG-PD-33 JRHM 24/04/17 SE MODIFICO PROBLEMA AL GUARDAR LA RESPUESTA INCORRENTA EN EL LUGAR EQUIVOCADO
'BUG-PD-42:RHERNANDEZ:04/05/17: SE QUIDO UN CASO DE USO DE SELECTED INDEX DE NOMBRE DE ID CONTRA SOLICITUD ES DIFERENTE
'BUG-PD-54: RHERNANDEZ: 22/05/17: SE AGREGA FUNCIONALIDAD PARA TERCERA OPCION EN LA PANTALLA DE ID VS SOLICITUD
'BBV-P-423. RQXLS2: CGARCIA: 30/05/2017 VLIDACIONES DICIONALES A DROPDOWNLIST
'BBV-P-423 RQXLS3: CGARCIA: 09/06/2017 SE AGREGAN VALUDACIONES 
'BUG-PD-88: CGARCIA: 13/06/2017 SE AGREGO VALIDACION PARA LOS DROPDOWNLIST DE CEDULA
'BUG-PD-102: CGARCIA: 16/06/2017 Se AGREGO VALIDACION PARA LAS VENTANAS DE INE Y CEDULA EN CUESTION DE LAS TABLAS DE GESTION DE ARCHIVOS PARA QUE NO SE BORRARAN AL ABRIR LAS VENTANAS ANTERIORES.
'BUG-PD-111: CGARCIA: 22/06/2017: SE AGREGARON VALIDACIONES A LAS TABLAS DE DOCUMENTOS
'BUG-PD-190: JBEJAR: 17/08/2017: SE AGREGA DDL AL IF DE VALIDACION DE PREGUNTAS OBLIGATORIAS.
'BUG-PD-204 GVARGAS 19/09/2017 WS biometrico
'BUG-PD-211 GVARGAS 27/09/2017 Cambios mostrar info
'BUG-PD-400: 14/03/2018: CGARCIA: CORRESCCION VALIDACION ID VS SOLICITUD
'BUG-PD-423: CGARCIA: 23/04/2018: SE MANDA A BACK LA ACTUALIZACION DE STATUS DE LOS DOCUMENTOS.

Partial Class aspx_PantallaCuestionarioSolvsID
    Inherits System.Web.UI.Page
    Public idPantalla As Integer
    Dim clien As New clsDatosCliente
    Dim sol As New clsStatusSolicitud
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim intEnable As Integer
        Try
            intEnable = CInt(Request.QueryString("Enable"))
        Catch ex As Exception
            intEnable = 0
        End Try

        idPantalla = CInt(Request("idPantalla").ToString)
        If Not IsPostBack Then
            LlenaCombo(1)
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

            Dim BD As New clsManejaBD
            Dim dsresult As New DataSet
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

            Dim clsquiz As New clsCuestionarioSolvsID()
            clsquiz._ID_SOLICITUD = Request("sol")
            Dim dsres = clsquiz.GetCuestionarioIDvsSol()
            If clsquiz.StrError = "" Then
                If Not IsNothing(dsres) Then
                    ddlTipoID.SelectedValue = CStr(dsres.Tables(0).Rows(0).Item("PDK_TIPO_ID"))
                    If ddlTipoID.SelectedValue = "E" Then
                        ddlNomvsSol.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_NOMBREID_VS_SOL"))
                        ddlIDVig.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_ID_VIGENTE"))
                        ddlFirmaID.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_FIRMA_ID"))
                        ddlGeoloca.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_NUMERO_CEDULA"))
                        ddlCedula.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_EXISTE_CEDULA"))
                        ddlPerfil.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_LICENCIATURA"))
                        'ddlRightLock.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_CANDADO_OK"))
                        'ddlValAdic.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_VALIDACION_ADICIONAL"))
                        ddlFirmaID_SelectedIndexChanged(ddlFirmaID, Nothing)
                        ValidaCombo(2)
                    ElseIf ddlTipoID.SelectedValue = "I" Then
                        ddlNomvsSol.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_NOMBREID_VS_SOL"))
                        ddlIDVig.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_ID_VIGENTE"))
                        ddlFirmaID.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_FIRMA_ID"))
                        If ddlIDVig.SelectedValue = 0 Then
                            ddlEstatusINE.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_ESTATUS_INE"))
                        End If
                        'ddlRightLock.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_CANDADO_OK"))
                        'ddlValAdic.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_VALIDACION_ADICIONAL"))
                        ddlTipoINE.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_VERSION_INE"))
                        If (ddlTipoINE.SelectedValue = 1 Or ddlTipoINE.SelectedValue = 2) Then
                            ddlProcesable.SelectedValue = -1
                            ddlProcesable.Enabled = False
                        Else
                            ddlProcesable.Enabled = True
                            ddlProcesable.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_MICA_PROCESABLE"))
                        End If

                        ddlExisteINE.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_EXISTE_INE"))
                        ddlFirmaID_SelectedIndexChanged(ddlFirmaID, Nothing)
                        ValidaCombo(2)
                        'ddlProcesable.Enabled = True
                    Else
                        ddlNomvsSol.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_NOMBREID_VS_SOL"))
                        ddlIDVig.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_ID_VIGENTE"))
                        ddlFirmaID.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_FIRMA_ID"))
                        'ddlRightLock.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_CANDADO_OK"))
                        'ddlValAdic.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_VALIDACION_ADICIONAL"))
                        ddlFirmaID_SelectedIndexChanged(ddlFirmaID, Nothing)
                        ValidaCombo(1)
                        hdBandera.Value = CInt(0)
                        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                        'ddlRightLock.Enabled = False
                        'ddlValAdic.Enabled = False
                    End If


                    
                End If
                ddlFirmaID_SelectedIndexChanged(ddlFirmaID, Nothing)
            Else
                Master.MensajeError("Error: " + clsquiz.StrError)
            End If

            '   CARGO SECCION TURNAR
            clsquiz._ID_PANT = idPantalla
            Dim dtsres As New DataSet
            Dim objCombo As New clsParametros
            dtsres = clsquiz.getTurnar()
            If dtsres.Tables.Count > 0 And dtsres.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsres, "TURNAR_NOMBRE", "PDK_ID_TAREAS", ddlTurnar, True, True)
            End If
        End If
        If intEnable = 1 Then
            cmbguardar1.Attributes.Add("style", "display:none;")
            btnCancelar.Attributes.Add("style", "display:none;")
            lblturnar.Attributes.Add("style", "display:none;")
            ddlTurnar.Attributes.Add("style", "display:none;")
            ddlTipoID.Enabled = False
            ddlIDVig.Enabled = False
            ddlNomvsSol.Enabled = False
            'BUG-PD-400: 14/03/2018: CGARCIA: CORRESCCION VALIDACION ID VS SOLICITUD
            ddlExisteINE.Enabled = False
            ddlTipoINE.Enabled = False
            ddlProcesable.Enabled = False
            'ddlRightLock.Enabled = False
            ddlFirmaID.Enabled = False
            'ddlValAdic.Enabled = False
        End If

        'ValidaCombo(1)

        ' ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');", True)
    End Sub
    Protected Sub cmbguardar1_Click(sender As Object, e As EventArgs)
        Try
            Dim objTarea As New clsSolicitudes(0)
            Dim dsValidaTarea As DataSet
            Dim ValTareas As Integer = 0
            Dim BD As New clsManejaBD
            Dim ds_validardocumento As DataSet
            Dim Mensaje As String
            objTarea.PDK_ID_SOLICITUD = Request("sol")
            objTarea.PDK_ID_PANTALLA = Request("idPantalla")
            dsValidaTarea = objTarea.ManejaTarea(1)

            
            If (ddlTipoID.SelectedValue = "E" Or ddlTipoID.SelectedValue = "I") Then
                BD.AgregaParametro("@FOLIO", TipoDato.Entero, hdSolicitud.Value)
                BD.AgregaParametro("@BANDERA", TipoDato.Entero, 2)
                BD.AgregaParametro("@PANTALLA", TipoDato.Entero, idPantalla)
                BD.AgregaParametro("@BANDERA_VALI_ID", TipoDato.Entero, 1)
                ds_validardocumento = BD.EjecutaStoredProcedure("sp_validarEntrevista")
                Mensaje = ds_validardocumento.Tables(0).Rows(0).Item("MENSAJE")
            Else
                BD.AgregaParametro("@FOLIO", TipoDato.Entero, hdSolicitud.Value)
                BD.AgregaParametro("@BANDERA", TipoDato.Entero, 2)
                BD.AgregaParametro("@PANTALLA", TipoDato.Entero, idPantalla)
                ds_validardocumento = BD.EjecutaStoredProcedure("sp_validarEntrevista")
                Mensaje = ds_validardocumento.Tables(0).Rows(0).Item("MENSAJE")
            End If

            

            If Mensaje <> "" Then
                Master.MensajeError(Mensaje)
                If (ddlTipoID.SelectedValue = "E" Or ddlTipoID.SelectedValue = "I" Or ddlTipoID.SelectedValue = "-1") Then
                    hdBandera.Value = CInt(1)
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                Else
                    hdBandera.Value = CInt(0)
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                End If
                cmbguardar1.Enabled = True
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Desabilitar", "document.getElementById(" & cmbguardar1.ClientID & ").attr('disabled'", True)
                Exit Sub
            End If

            If dsValidaTarea.Tables.Count > 0 Then
                If dsValidaTarea.Tables(0).Rows.Count > 0 Then
                    ValTareas = dsValidaTarea.Tables(0).Rows(0).Item("VALIDA_TAREA")
                End If
            End If
            If ValTareas = 1 Then
                If ddlTurnar.SelectedValue <> 0 Then
                    cmbguardar1.Enabled = False
                    If ddlTipoID.SelectedValue = "E" Then 'En caso de seleccionar cédula profecional se guarda lo correspondiente a ella 
                        Select Case ddlCedula.SelectedValue
                            Case 0
                                If ddlNomvsSol.SelectedValue <> -1 And ddlIDVig.SelectedValue <> -1 And
                                    ddlFirmaID.SelectedValue <> -1 And ddlCedula.SelectedValue <> -1 Then
                                    Dim clsquiz As New clsCuestionarioSolvsID
                                    clsquiz._ID_SOLICITUD = hdSolicitud.Value
                                    clsquiz._TIPO_ID = ddlTipoID.SelectedValue
                                    clsquiz._NOM_VS_ID = ddlNomvsSol.SelectedValue
                                    clsquiz._ID_VIG = ddlIDVig.SelectedValue
                                    clsquiz._FIRMA_ID = ddlFirmaID.SelectedValue
                                    clsquiz._ID_NUM_CEDULA = ddlGeoloca.SelectedValue
                                    clsquiz._ID_EXISTE_CEDULA = ddlCedula.SelectedValue
                                    If ddlCedula.SelectedValue = 1 Then
                                        clsquiz._ID_LICENCIATURA = ddlPerfil.SelectedValue
                                    End If
                                    If clsquiz.InsertCuestionarioIDvsSol() Then
                                        If ddlTurnar.SelectedValue = -1 Then
                                            asignaTarea(0)
                                        ElseIf ddlTurnar.SelectedValue = -2 Then
                                            Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
                                            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
                                            Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
                                            Solicitudes.Estatus = 295
                                            Solicitudes.Estatus_Cred = 295
                                            Solicitudes.Comentario = ddlTurnar.SelectedItem.Text
                                            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
                                            Solicitudes.ManejaTarea(6)

                                            Master.MsjErrorRedirect("Solicitud Declinada", "../aspx/consultaPanelControl.aspx")
                                        Else
                                            asignaTarea(ddlTurnar.SelectedValue)
                                        End If

                                    Else
                                        hdBandera.Value = CInt(1)
                                        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                                        Throw New Exception("Error: Ocurrio un problema al guardar el cuestionario")
                                    End If
                                Else
                                    hdBandera.Value = CInt(1)
                                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                                    Throw New Exception("Error: Falta contestar preguntas del cuestionario. *")
                                End If
                            Case 1
                                If ddlNomvsSol.SelectedValue <> -1 And ddlIDVig.SelectedValue <> -1 And
                                    ddlFirmaID.SelectedValue <> -1 And ddlGeoloca.SelectedValue <> -1 And
                                    ddlCedula.SelectedValue <> -1 And ddlPerfil.SelectedValue <> -1 Then
                                    Dim clsquiz As New clsCuestionarioSolvsID
                                    clsquiz._ID_SOLICITUD = hdSolicitud.Value
                                    clsquiz._TIPO_ID = ddlTipoID.SelectedValue
                                    clsquiz._NOM_VS_ID = ddlNomvsSol.SelectedValue
                                    clsquiz._ID_VIG = ddlIDVig.SelectedValue
                                    clsquiz._FIRMA_ID = ddlFirmaID.SelectedValue
                                    clsquiz._ID_NUM_CEDULA = ddlGeoloca.SelectedValue
                                    clsquiz._ID_EXISTE_CEDULA = ddlCedula.SelectedValue
                                    If ddlCedula.SelectedValue = 1 Then
                                        clsquiz._ID_LICENCIATURA = ddlPerfil.SelectedValue
                                    End If
                                    If clsquiz.InsertCuestionarioIDvsSol() Then
                                        If ddlTurnar.SelectedValue = -1 Then
                                            asignaTarea(0)
                                        ElseIf ddlTurnar.SelectedValue = -2 Then
                                            Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
                                            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
                                            Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
                                            Solicitudes.Estatus = 295
                                            Solicitudes.Estatus_Cred = 295
                                            Solicitudes.Comentario = ddlTurnar.SelectedItem.Text
                                            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
                                            Solicitudes.ManejaTarea(6)

                                            Master.MsjErrorRedirect("Solicitud Declinada", "../aspx/consultaPanelControl.aspx")
                                        Else
                                            asignaTarea(ddlTurnar.SelectedValue)
                                        End If

                                    Else
                                        hdBandera.Value = CInt(1)
                                        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                                        Throw New Exception("Error: Ocurrio un problema al guardar el cuestionario")
                                    End If
                                Else
                                    hdBandera.Value = CInt(1)
                                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                                    Throw New Exception("Error: Falta contestar preguntas del cuestionario. *")
                                End If
                            Case -1

                                hdBandera.Value = CInt(1)
                                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                                Throw New Exception("Error: Falta contestar preguntas del cuestionario. *")
                        End Select
                        
                    ElseIf ddlTipoID.SelectedValue = "I" Then
                        If ddlNomvsSol.SelectedValue <> -1 And ddlIDVig.SelectedValue <> -1 And
                           ddlFirmaID.SelectedValue <> -1 And ddlTipoINE.SelectedValue <> -1 And ddlExisteINE.SelectedValue <> -1 Then
                            Dim clsquiz As New clsCuestionarioSolvsID
                            clsquiz._ID_SOLICITUD = hdSolicitud.Value
                            clsquiz._TIPO_ID = ddlTipoID.SelectedValue
                            clsquiz._NOM_VS_ID = ddlNomvsSol.SelectedValue
                            clsquiz._ID_VIG = ddlIDVig.SelectedValue
                            If ddlIDVig.SelectedValue = 0 Then
                                clsquiz._ID_ESTATUS_INE = ddlEstatusINE.SelectedValue
                            End If
                            clsquiz._FIRMA_ID = ddlFirmaID.SelectedValue
                            'clsquiz._VAL_ADIC = ddlValAdic.SelectedValue
                            'clsquiz._LOCK_RIGHT = ddlRightLock.SelectedValue
                            clsquiz._ID_VERSION_INE = ddlTipoINE.SelectedValue
                            If ddlTipoINE.SelectedValue = 1 Then
                                clsquiz._ID_MICA_PROC = ddlProcesable.SelectedValue
                            End If
                            clsquiz._ID_EXISTE_INE = ddlExisteINE.SelectedValue
                            If clsquiz.InsertCuestionarioIDvsSol() Then
                                If ddlTurnar.SelectedValue = -1 Then
                                    asignaTarea(0)
                                ElseIf ddlTurnar.SelectedValue = -2 Then
                                    Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
                                    Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
                                    Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
                                    Solicitudes.Estatus = 295
                                    Solicitudes.Estatus_Cred = 295
                                    Solicitudes.Comentario = ddlTurnar.SelectedItem.Text
                                    Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
                                    Solicitudes.ManejaTarea(6)


                                    Master.MsjErrorRedirect("Solicitud Declinada", "../aspx/consultaPanelControl.aspx")
                                Else
                                    asignaTarea(ddlTurnar.SelectedValue)
                                End If

                            Else
                                hdBandera.Value = CInt(1)
                                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                                Throw New Exception("Error: Ocurrio un problema al guardar el cuestionario")
                            End If
                        Else
                            hdBandera.Value = CInt(1)
                            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                            Throw New Exception("Error: Falta contestar preguntas del cuestionario. *")
                        End If
                    Else
                        If ddlTipoID.SelectedValue <> "-1" And ddlNomvsSol.SelectedValue <> -1 And ddlIDVig.SelectedValue <> -1 And
                            ddlFirmaID.SelectedValue <> -1 Then
                            Dim clsquiz As New clsCuestionarioSolvsID
                            clsquiz._ID_SOLICITUD = hdSolicitud.Value
                            clsquiz._TIPO_ID = ddlTipoID.SelectedValue
                            clsquiz._NOM_VS_ID = ddlNomvsSol.SelectedValue
                            clsquiz._ID_VIG = ddlIDVig.SelectedValue
                            clsquiz._FIRMA_ID = ddlFirmaID.SelectedValue
                            If clsquiz.InsertCuestionarioIDvsSol() Then
                                If ddlTurnar.SelectedValue = -1 Then
                                    asignaTarea(0)
                                ElseIf ddlTurnar.SelectedValue = -2 Then
                                    Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
                                    Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
                                    Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
                                    Solicitudes.Estatus = 295
                                    Solicitudes.Estatus_Cred = 295
                                    Solicitudes.Comentario = ddlTurnar.SelectedItem.Text
                                    Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
                                    Solicitudes.ManejaTarea(6)


                                    Master.MsjErrorRedirect("Solicitud Declinada", "../aspx/consultaPanelControl.aspx")
                                Else
                                    asignaTarea(ddlTurnar.SelectedValue)
                                End If

                            Else
                                hdBandera.Value = CInt(1)
                                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                                Throw New Exception("Error: Ocurrio un problema al guardar el cuestionario")
                            End If
                        Else
                            hdBandera.Value = CInt(1)
                            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                            Throw New Exception("Error: Falta contestar preguntas del cuestionario. *")
                        End If
                    End If
                Else
                    hdBandera.Value = CInt(1)
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                    Throw New Exception("Error: Debe Seleccionar una opcion a turnar")
                End If
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
            cmbguardar1.Enabled = True
        End Try
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub

    Protected Sub ddlNomvsSol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNomvsSol.SelectedIndexChanged
        Dim clscuestionarioID As New clsCuestionarioSolvsID
        clscuestionarioID._ID_SOLICITUD = hdSolicitud.Value
        clscuestionarioID._ID_PANT = hdPantalla.Value
        clscuestionarioID._ID_USER = hdusuario.Value
        clscuestionarioID._ID_DOC = 90
        If ddlNomvsSol.SelectedValue = -1 And ddlIDVig.SelectedValue = -1 And ddlFirmaID.SelectedValue = -1 Then
            clscuestionarioID._ACTION = -1
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = -1 And (ddlIDVig.SelectedValue = 1 Or ddlIDVig.SelectedValue = 0) And
            (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1 Or ddlFirmaID.SelectedValue = 2) Then
            clscuestionarioID._ACTION = -1
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1 Or ddlNomvsSol.SelectedValue = 2) And
            ddlIDVig.SelectedValue = -1 And (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1 Or ddlFirmaID.SelectedValue = 2) Then
            clscuestionarioID._ACTION = -1
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1 Or ddlNomvsSol.SelectedValue = 2) And
            (ddlIDVig.SelectedValue = 1 Or ddlIDVig.SelectedValue = 0) And ddlFirmaID.SelectedValue = -1 Then
            clscuestionarioID._ACTION = -1
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1) And ddlIDVig.SelectedValue = 1 And
            (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1) Then
            clscuestionarioID._ACTION = 0
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1) And ddlIDVig.SelectedValue = 1 And
            ddlFirmaID.SelectedValue = 2 Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 344
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1) And ddlIDVig.SelectedValue = 0 And
            (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1) Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 272
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = 2 And ddlIDVig.SelectedValue = 1 And
            (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1) Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 271
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1) And ddlIDVig.SelectedValue = 0 And ddlFirmaID.SelectedValue = 2 Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 346
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = 2 And ddlIDVig.SelectedValue = 0 And (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1) Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 339
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = 2 And ddlIDVig.SelectedValue = 1 And ddlFirmaID.SelectedValue = 2 Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 345
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = 2 And ddlIDVig.SelectedValue = 0 And ddlFirmaID.SelectedValue = 2 Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 347
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        End If
        If (ddlTipoID.SelectedValue = "E" Or ddlTipoID.SelectedValue = "I" Or ddlTipoID.SelectedValue = "-1") Then
            hdBandera.Value = CInt(1)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
        Else
            hdBandera.Value = CInt(0)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
        End If
    End Sub

    Protected Sub ddlIDVig_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlIDVig.SelectedIndexChanged
        Dim clscuestionarioID As New clsCuestionarioSolvsID
        clscuestionarioID._ID_SOLICITUD = hdSolicitud.Value
        clscuestionarioID._ID_PANT = hdPantalla.Value
        clscuestionarioID._ID_USER = hdusuario.Value
        clscuestionarioID._ID_DOC = 90
        If ddlNomvsSol.SelectedValue = -1 And ddlIDVig.SelectedValue = -1 And ddlFirmaID.SelectedValue = -1 Then
            clscuestionarioID._ACTION = -1
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = -1 And (ddlIDVig.SelectedValue = 1 Or ddlIDVig.SelectedValue = 0) And
            (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1 Or ddlFirmaID.SelectedValue = 2) Then
            clscuestionarioID._ACTION = -1
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1 Or ddlNomvsSol.SelectedValue = 2) And
            ddlIDVig.SelectedValue = -1 And (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1 Or ddlFirmaID.SelectedValue = 2) Then
            clscuestionarioID._ACTION = -1
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1 Or ddlNomvsSol.SelectedValue = 2) And
            (ddlIDVig.SelectedValue = 1 Or ddlIDVig.SelectedValue = 0) And ddlFirmaID.SelectedValue = -1 Then
            clscuestionarioID._ACTION = -1
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1) And ddlIDVig.SelectedValue = 1 And
            (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1) Then
            clscuestionarioID._ACTION = 0
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1) And ddlIDVig.SelectedValue = 1 And
            ddlFirmaID.SelectedValue = 2 Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 344
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1) And ddlIDVig.SelectedValue = 0 And
            (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1) Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 272
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = 2 And ddlIDVig.SelectedValue = 1 And
            (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1) Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 271
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1) And ddlIDVig.SelectedValue = 0 And ddlFirmaID.SelectedValue = 2 Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 346
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = 2 And ddlIDVig.SelectedValue = 0 And (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1) Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 339
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = 2 And ddlIDVig.SelectedValue = 1 And ddlFirmaID.SelectedValue = 2 Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 345
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = 2 And ddlIDVig.SelectedValue = 0 And ddlFirmaID.SelectedValue = 2 Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 347
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        End If
        If (ddlTipoID.SelectedValue = "E" Or ddlTipoID.SelectedValue = "I" Or ddlTipoID.SelectedValue = "-1") Then
            hdBandera.Value = CInt(1)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
        Else
            hdBandera.Value = CInt(0)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
        End If
        ValidaCombo(1)
    End Sub

    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Try

            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")

            If idAsignarPantalla <> 0 Then
                Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla
            End If

            dsresult = Solicitudes.ValNegocio(1)
            'mensaje = IIf(dsresult.Tables(0).Rows(0).Item("MENSAJE") Is Nothing Or dsresult.Tables(0).Rows(0).Item("MENSAJE") = String.Empty, dsresult.Tables(1).Rows(0).Item("MENSAJE").ToString, dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString)
            If dsresult.Tables(0).Rows(0).Item("MENSAJE") Is Nothing Or dsresult.Tables(0).Rows(0).Item("MENSAJE") = String.Empty Then
                mensaje = dsresult.Tables(1).Rows(0).Item("MENSAJE").ToString
            Else
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            End If
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" And mensaje <> "SE RECHAZO  DOCUMENTO" And mensaje.Contains("Falta el documento consulta identificacion") Then

                Throw New Exception(mensaje)
                'Master.MsjErrorRedirect(mensaje, "../aspx/consultaPanelControl.aspx")
                'Exit Sub
            End If

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))
            Dim dc As New clsDatosCliente
            dc.idSolicitud = Request("sol")
            dc.getDatosSol()

            If muestrapant = 0 Then
                Dim strAux As String = "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usu")).ToString & "');"
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Mensaje", strAux, True)
            ElseIf muestrapant = 2 Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Mensaje", "PopUpLetreroRedirect('" & mensaje & "','" & "../aspx/consultaPanelControl.aspx');", True)
            End If



        Catch ex As Exception
            Master.MensajeError(mensaje)

        End Try
    End Sub

    Protected Sub ddlTipoID_SelectedIndexChanged(sender As Object, e As EventArgs)
        
        ValidaCombo(2)

    End Sub

    Protected Sub ddlFirmaID_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim clscuestionarioID As New clsCuestionarioSolvsID
        clscuestionarioID._ID_SOLICITUD = hdSolicitud.Value
        clscuestionarioID._ID_PANT = hdPantalla.Value
        clscuestionarioID._ID_USER = hdusuario.Value
        clscuestionarioID._ID_DOC = 90
        If ddlNomvsSol.SelectedValue = -1 And ddlIDVig.SelectedValue = -1 And ddlFirmaID.SelectedValue = -1 Then
            clscuestionarioID._ACTION = -1
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = -1 And (ddlIDVig.SelectedValue = 1 Or ddlIDVig.SelectedValue = 0) And
            (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1 Or ddlFirmaID.SelectedValue = 2) Then
            clscuestionarioID._ACTION = -1
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1 Or ddlNomvsSol.SelectedValue = 2) And
            ddlIDVig.SelectedValue = -1 And (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1 Or ddlFirmaID.SelectedValue = 2) Then
            clscuestionarioID._ACTION = -1
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1 Or ddlNomvsSol.SelectedValue = 2) And
            (ddlIDVig.SelectedValue = 1 Or ddlIDVig.SelectedValue = 0) And ddlFirmaID.SelectedValue = -1 Then
            clscuestionarioID._ACTION = -1
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1) And ddlIDVig.SelectedValue = 1 And
            (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1) Then
            clscuestionarioID._ACTION = 0
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1) And ddlIDVig.SelectedValue = 1 And
            ddlFirmaID.SelectedValue = 2 Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 344
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1) And ddlIDVig.SelectedValue = 0 And
            (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1) Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 272
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = 2 And ddlIDVig.SelectedValue = 1 And
            (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1) Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 271
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf (ddlNomvsSol.SelectedValue = 0 Or ddlNomvsSol.SelectedValue = 1) And ddlIDVig.SelectedValue = 0 And ddlFirmaID.SelectedValue = 2 Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 346
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = 2 And ddlIDVig.SelectedValue = 0 And (ddlFirmaID.SelectedValue = 0 Or ddlFirmaID.SelectedValue = 1) Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 339
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = 2 And ddlIDVig.SelectedValue = 1 And ddlFirmaID.SelectedValue = 2 Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 345
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        ElseIf ddlNomvsSol.SelectedValue = 2 And ddlIDVig.SelectedValue = 0 And ddlFirmaID.SelectedValue = 2 Then
            clscuestionarioID._ACTION = 1
            clscuestionarioID._MOT_RECH = 347
            If Not clscuestionarioID.ValidaID() Then
                Master.MensajeError(clscuestionarioID.StrError)
            End If
        End If
        If (ddlTipoID.SelectedValue = "E" Or ddlTipoID.SelectedValue = "I" Or ddlTipoID.SelectedValue = "-1") Then
            hdBandera.Value = CInt(1)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
        Else
            hdBandera.Value = CInt(0)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
        End If
    End Sub

    Protected Sub ddlTipoINE_TextChanged(sender As Object, e As EventArgs)
        If (ddlTipoID.SelectedValue = "E" Or ddlTipoID.SelectedValue = "I" Or ddlTipoID.SelectedValue = "-1") Then
            hdBandera.Value = CInt(1)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
        Else
            hdBandera.Value = CInt(0)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
        End If
        ValidaCombo(3)

       
    End Sub

    Protected Sub ddlCedula_SelectedIndexChanged(sender As Object, e As EventArgs)
        If (ddlTipoID.SelectedValue = "E" Or ddlTipoID.SelectedValue = "I" Or ddlTipoID.SelectedValue = "-1") Then
            hdBandera.Value = CInt(1)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
        Else
            hdBandera.Value = CInt(0)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
        End If
        ValidaCombo(4)

    End Sub

    Private Sub LlenaCombo(ByVal intCombo As Integer)
        If intCombo = 1 Then
            'Carga combo  de tipo de identificacion
            Dim ds As DataSet
            Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())

            Dim queryString As String = "SELECT '-1' AS ID, '<SELECCIONAR>' AS NAME UNION SELECT PDK_SCORING_VALOR2 AS ID, PDK_SCORING_VALOR1 AS NAME FROM PDK_SCORING WHERE PDK_ID_SECCION_DATO = 541 ORDER BY NAME ASC"
            Dim aadpter As SqlDataAdapter = New SqlDataAdapter(queryString, sqlConnection1)
            Dim customer As DataSet = New DataSet
            aadpter.Fill(customer, "customer")

            ds = customer

            ddlTipoID.DataSource = ds
            ddlTipoID.DataTextField = "NAME"
            ddlTipoID.DataValueField = "ID"
            ddlTipoID.DataBind()
        End If
    End Sub

    Private Sub ValidaCombo(ByVal intValida As Integer)
        If intValida = 1 Then
            If ddlIDVig.SelectedValue = 0 And ddlTipoID.SelectedValue = "I" Then
                tdEstatusINE.Visible = True
                trExisteINE.Visible = True
                trTipoINE.Visible = True
            ElseIf ddlIDVig.SelectedValue = 1 And ddlTipoID.SelectedValue = "I" Then
                trTipoINE.Visible = True
                tdEstatusINE.Visible = False
                trExisteINE.Visible = True
            Else
                tdEstatusINE.Visible = False
                trExisteINE.Visible = False
                ' trTipoINE.Visible = False
            End If
        ElseIf intValida = 2 Then
            If ddlTipoID.SelectedValue = "I" Then
                hdBandera.Value = CInt(1)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                divPortalNominal.Visible = True
                divPortalCedula.Visible = False
                tdExisteCedula.Visible = False
                trDatosCedula.Visible = False
                'ddlValAdic.Enabled = True
                'ddlRightLock.Enabled = True
                trTipoINE.Visible = True
                trExisteINE.Visible = True
                If ddlIDVig.SelectedValue = 0 Then
                    tdEstatusINE.Visible = True
                Else
                    tdEstatusINE.Visible = False
                End If
                
            ElseIf ddlTipoID.SelectedValue = "E" Then
                hdBandera.Value = CInt(1)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                divPortalNominal.Visible = False
                divPortalCedula.Visible = True
                tdExisteCedula.Visible = True
                trDatosCedula.Visible = True
                'ddlValAdic.Enabled = False
                'ddlRightLock.Enabled = False
                'ddlValAdic.SelectedValue = -1
                'ddlRightLock.SelectedValue = -1
                trTipoINE.Visible = False
                trExisteINE.Visible = False
                tdEstatusINE.Visible = False
                If ddlCedula.SelectedValue = 0 Or ddlCedula.SelectedValue = 2 Then
                    ddlPerfil.SelectedValue = -1
                    ddlPerfil.Enabled = False
                    ddlGeoloca.SelectedValue = -1
                    ddlGeoloca.Enabled = False
                ElseIf ddlCedula.SelectedValue = 1 Then
                    ddlPerfil.Enabled = True
                    ddlGeoloca.Enabled = True
                End If
            ElseIf ddlTipoID.SelectedValue = "-1" Then
                hdBandera.Value = CInt(0)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                'ddlValAdic.Enabled = True
                'ddlValAdic.SelectedValue = -1
                'ddlRightLock.Enabled = True
                'ddlRightLock.SelectedValue = -1
                ddlTipoINE.SelectedValue = -1
                trTipoINE.Visible = False
                trExisteINE.Visible = False
                tdExisteCedula.Visible = False
                trDatosCedula.Visible = False
                divPortalCedula.Visible = False
                divPortalNominal.Visible = False
                ddlIDVig.SelectedValue = -1
                ddlNomvsSol.SelectedValue = -1
                ddlFirmaID.SelectedValue = -1
            Else
                hdBandera.Value = CInt(0)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
                divPortalCedula.Visible = False
                divPortalNominal.Visible = False
                tdExisteCedula.Visible = False
                trDatosCedula.Visible = False
                ddlNomvsSol.Enabled = True
                ddlFirmaID.Enabled = True
                'ddlValAdic.Enabled = False
                'ddlRightLock.Enabled = False
                ddlTipoINE.SelectedValue = -1
                trTipoINE.Visible = False
                tdEstatusINE.Visible = False
                tdExisteCedula.Visible = False
                trExisteINE.Visible = False
            End If
        ElseIf intValida = 3 Then
        If ddlTipoINE.SelectedValue = 0 Then
            ddlProcesable.Enabled = True
            ddlNomvsSol.Enabled = True
            ddlFirmaID.Enabled = True
        ElseIf ddlTipoINE.SelectedValue = -1 Then
            ddlProcesable.Enabled = True
            ddlNomvsSol.Enabled = True
            ddlFirmaID.Enabled = True
        Else
            ddlProcesable.SelectedValue = -1
            ddlProcesable.Enabled = False
            ddlNomvsSol.Enabled = True
            ddlFirmaID.Enabled = True
        End If
        ElseIf intValida = 4 Then
        If ddlCedula.SelectedValue = 0 Or ddlCedula.SelectedValue = 2 Then
            ddlGeoloca.Enabled = False
            ddlPerfil.Enabled = False
            ddlPerfil.Enabled = False
            ddlGeoloca.SelectedValue = -1
            ddlPerfil.SelectedValue = -1
        Else
            ddlGeoloca.Enabled = True
                'ddlGeoloca.SelectedValue = -1
            ddlPerfil.SelectedValue = -1
            ddlPerfil.Enabled = True
            ddlPerfil.Enabled = True
        End If
        End If
    End Sub

    Protected Sub btnLinkedin_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirFB", "window.open('" + ConfigurationManager.AppSettings("PaginaIne").ToString + "','popupifai','width=1800,height=1000,left=-10,top=0,resizable');", True)
        hdBandera.Value = CInt(1)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
    End Sub

    Protected Sub btnLinkCel_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirFB", "window.open('" + ConfigurationManager.AppSettings("PaginaCedula").ToString + "','popupifai','width=1800,height=1000,left=-10,top=0,resizable');", True)
        hdBandera.Value = CInt(1)
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
    End Sub

    Protected Sub btnVisorDocumental_Click(sender As Object, e As EventArgs)
        Dim strRuta As String = "../Comparador.aspx?folio=2131&id_doc=72979"

        If ddlTipoID.SelectedValue = "E" Or ddlTipoID.SelectedValue = "I" Then
            hdBandera.Value = CInt(1)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)
        Else
            hdBandera.Value = CInt(0)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload2('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');", True)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)        
        End If



        
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function detailsINE(ByVal folio As String, ByVal opc As Integer) As String
        Dim msg_CI As respuesta = New respuesta()
        msg_CI.cod = "0"
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_Response_INE_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Parameters.AddWithValue("@OPCION", opc)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                msg_CI.mensaje = reader(0).ToString()
            Loop

        Catch ex As Exception
            msg_CI.mensaje = "ERROR"
        End Try

        sqlConnection1.Close()
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Respuesta As String = serializer.Serialize(msg_CI)
        Return json_Respuesta
    End Function

    Public Class respuesta
        Public cod As String
        Public mensaje As String
    End Class

    'BUG-PD-423: CGARCIA: 23/04/2018: SE MANDA A BACK LA ACTUALIZACION DE STATUS DE LOS DOCUMENTOS.
    <System.Web.Services.WebMethod()> _
    Public Shared Function getBack(ByVal id As String, ByVal docType As String, ByVal folio As String, ByVal Check_VAL As String, ByVal Check_REC As String, ByVal Checked As String) As String
        Dim respuesta As String = String.Empty
        Dim clsDoc As clsDocumentos = New clsDocumentos
        Dim dsDatos As DataSet

        clsDoc.id = id
        clsDoc.docType = docType
        clsDoc.folio = folio
        clsDoc.Check_VAL = Check_VAL
        clsDoc.Check_REC = Check_REC
        clsDoc.Checked = Checked

        dsDatos = clsDoc.getActualizaDatosDocumentos(1)

        If (dsDatos.Tables.Count > 1 AndAlso dsDatos.Tables(0).Rows.Count > 0 AndAlso dsDatos.Tables(1).Rows.Count > 0 AndAlso dsDatos.Tables(1).Rows(0).Item("RESULTADO").ToString <> String.Empty) Then
            respuesta = CStr(dsDatos.Tables(1).Rows(0).Item("RESULTADO").ToString)

        End If

        Return respuesta
    End Function
End Class
