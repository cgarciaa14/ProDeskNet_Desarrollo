Imports ProdeskNet.SN
Imports System.Data
Imports ProdeskNet.WCF
'BUG-PD-45:RHERNANDEZ:15/05/17: SE AGREGO PAGINA AUTOMATICA PARA LA CANCELACION DE POLIZAS DE SEGURO DE DAÑOS Y VIDA (ESTE ULTIMO SEGURO AUN NO ESTA IMPLEMENTADO)
'BUG-PD-68:RHERNANDEZ: 02/06/17: SE AGREGA A CODIGO LA OPCION DE CANCELACION DE LA POLIZA DE DAÑOS ORDAS
'BUG-PD-98: RHERNANDEZ: 19/06/17: SE AGREGA CANCELACION DE POLIZAS BANCOMER (COMENTADO DEBIDO A QUE CANCELA COTIZACIONES Y NO POLIZAS)
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
'BUG-PD-193: RHERNANDEZ: 21/08/17 SE TOMAN VARIABLES PARA CANCELAR POLIZAS DESDE EL WEB CONFIG
'BUG-PD-360: RHERNANDEZ: 19/02/18: SE AGREGA LA LECTURA DE IDQUOTE DE SEGURO DE VIDA PARA EMITIR SEGUROS INDEPENDIENTEMENTE
Partial Class aspx_PantCancPolizas
    Inherits System.Web.UI.Page
    Dim StrErr As String = String.Empty
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim idPant As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        idPant = CInt(Request("idPantalla"))

        Dim clssol As New clsTabDatosSolicitante
        Dim res As DataSet

        Dim ds_siguienteTarea As DataSet
        ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & idPant.ToString)

        res = clssol.CargaDatosSolicitante(CInt(Request("sol")))
        If clssol.StrError <> "" Then
            Master.MensajeError(clssol.StrError)
            Exit Sub
        End If

        Dim dts As DataSet
        Dim objpant As New ProdeskNet.SN.clsPantallas()
        dts = objpant.CargaPantallas(idPant)

        If dts.Tables.Count > 0 Then
            If dts.Tables(0).Rows.Count > 0 Then
                If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Then
                    If CancelaPoliza() Then
                        asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
                    Else
                        asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))
                    End If
                End If
            End If
        End If
    End Sub
    Public Function CancelaPoliza() As Boolean
        CancelaPoliza = False
        Dim clsseg As New clsSeguros
        clsseg._ID_SOLICITUD = CInt(Request("sol"))
        clsseg._ID_ENDOSO = "-1"
        clsseg._TIPO_SEG = 3
        If clsseg.InsertCancelacionPoliza() Then
        Else
            Master.MensajeError(clsseg.StrError)
            Exit Function
        End If
        clsseg = New clsSeguros
        clsseg._ID_SOLICITUD = CInt(Request("sol").ToString())
        Dim DatosSeguro As DataSet = clsseg.getDatosSeguro()
        If (DatosSeguro.Tables.Count > 0) Then
            If (DatosSeguro.Tables(0).Rows.Count > 0) Then
                If (DatosSeguro.Tables(0).Rows(0).Item("TIPO_SEG_AUTO").ToString() <> "32") Then
                    If CancelaSeguros(CInt(DatosSeguro.Tables(0).Rows(0).Item("ID_BROKER").ToString()), DatosSeguro.Tables(0).Rows(0).Item("NO_COTSEG").ToString) Then
                    Else
                        Exit Function
                    End If
                End If
                If (DatosSeguro.Tables(0).Rows(0).Item("TIPO_SEG_VIDA").ToString() <> "172") Then
                    If CancelaSeguros(-1, DatosSeguro.Tables(0).Rows(0).Item("NO_COTSEG_VIDA").ToString) Then
                    Else
                        Exit Function
                    End If
                End If
            End If
        End If
        CancelaPoliza = True
    End Function
    Public Function CancelaSeguros(ByVal Id_Seguro As Integer, ByVal nocot As String) As Boolean
        CancelaSeguros = False
        Select Case (Id_Seguro)
            Case -1
                If CancelaBBVA(nocot, 1) Then
                    CancelaSeguros = True
                Else
                    Exit Function
                End If
            Case 2 'ORDAS
                If CancelaORDAS() Then
                    CancelaSeguros = True
                Else
                    Exit Function
                End If
            Case 3 'EIKOS
                If CancelaEIKOS() Then
                    CancelaSeguros = True
                Else
                    Exit Function
                End If
            Case 4 'MARSH
                If CancelaMARSH() Then
                    CancelaSeguros = True
                Else
                    Exit Function
                End If
            Case 5 'BBVA
                If CancelaBBVA(nocot, 2) Then
                    CancelaSeguros = True
                Else
                    Exit Function
                End If
            Case Else
                Dim clsseg As New clsSeguros
                clsseg._ID_SOLICITUD = CInt(Request("sol"))
                clsseg._ID_ENDOSO = "-1"
                clsseg._TIPO_SEG = 3
                If clsseg.InsertCancelacionPoliza() Then
                    CancelaSeguros = True
                Else
                    Master.MensajeError(clsseg.StrError)
                    Exit Function
                End If
        End Select
    End Function
    Public Function CancelaORDAS() As Boolean
        Try
            CancelaORDAS = False
            Dim clsseg As New clsSeguros
            Dim res As DataSet
            clsseg._ID_SOLICITUD = CInt(Request("sol"))
            res = clsseg.GetDatosPoliza()
            If res.Tables.Count > 0 Then
                If res.Tables(0).Rows.Count > 0 Then
                    Dim clscanclOrdas As New CanPolORDAS
                    clscanclOrdas.policy.complement.user.id = System.Configuration.ConfigurationManager.AppSettings("useridOrdas").ToString()
                    clscanclOrdas.policy.complement.user.credentials.accessPassword = System.Configuration.ConfigurationManager.AppSettings("passwordOrdas").ToString()
                    clscanclOrdas.policy.policyId = res.Tables(0).Rows(0).Item("PDK_NO_POLIZA_SEG_AUTO").ToString
                    clscanclOrdas.policy.cancelData.effectiveDate = (Date.Now.Date).ToString("yyyy-MM-dd")
                    clscanclOrdas.policy.cancelData.idCancelReason = "4"
                    clscanclOrdas.policy.cancelData.notes = ""
                    clscanclOrdas.policy.cancelData.param1 = ""
                    clscanclOrdas.policy.cancelData.param2 = ""
                    Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim jsonbody As String = serializer.Serialize(clscanclOrdas)
                    Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                    Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
                    Dim restful As RESTful = New RESTful()
                    restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriCanclORDAS").ToString()

                    Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
                    Dim errormessage As String
                    If (restful.IsError) Then
                        errormessage = restful.MensajeError
                        Throw New Exception(errormessage)
                    Else
                        Dim ress As resCanPolORDAS = serializer.Deserialize(Of resCanPolORDAS)(respuesta)
                        clsseg._ID_ENDOSO = ress.policy.cancelData.numOt.ToString
                        clsseg._TIPO_SEG = 1
                        If clsseg.InsertCancelacionPoliza() Then

                        Else
                            Master.MensajeError(clsseg.StrError)
                            Exit Function
                        End If
                    End If
                End If
            End If
            CancelaORDAS = True
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Function
    Public Function CancelaEIKOS() As Boolean
        Try
            CancelaEIKOS = False
            Dim clsseg As New clsSeguros
            Dim res As DataSet
            clsseg._ID_SOLICITUD = CInt(Request("sol"))
            res = clsseg.GetDatosPoliza()
            If res.Tables.Count > 0 Then
                If res.Tables(0).Rows.Count > 0 Then
                    Dim clscanclEIKOS As New SolCanclPolEIKOS
                    clscanclEIKOS.policy.complement.user.id = System.Configuration.ConfigurationManager.AppSettings("useridEikos").ToString()
                    clscanclEIKOS.policy.complement.user.credentials.accessPassword = System.Configuration.ConfigurationManager.AppSettings("accessPasswordEikos").ToString()
                    clscanclEIKOS.policy.policyId = res.Tables(0).Rows(0).Item("PDK_NO_POLIZA_SEG_AUTO").ToString
                    clscanclEIKOS.policy.typeId = "1"
                    clscanclEIKOS.policy.insurerId = "101"
                    clscanclEIKOS.policy.cancelData.effectiveDate = Date.Now.ToString("yyyy-MM-dd")
                    clscanclEIKOS.policy.cancelData.idCancelReason = "1"
                    Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim jsonbody As String = serializer.Serialize(clscanclEIKOS)
                    Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                    Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
                    Dim restful As RESTful = New RESTful()
                    restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriCanclEIKOS").ToString()

                    Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
                    Dim errormessage As String
                    If (restful.IsError) Then
                        errormessage = restful.MensajeError
                        Throw New Exception(errormessage)
                    Else
                        Dim ress As ResCanclPolMARSH = serializer.Deserialize(Of ResCanclPolMARSH)(respuesta)
                        clsseg._ID_ENDOSO = "-1"
                        clsseg._TIPO_SEG = 1
                        If clsseg.InsertCancelacionPoliza() Then
                            CancelaEIKOS = True
                        Else
                            Master.MensajeError(clsseg.StrError)
                            Exit Function
                        End If
                    End If
                End If
            End If
            CancelaEIKOS = True
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Function
    Public Function CancelaMARSH() As Boolean
        Try
            CancelaMARSH = False
            Dim clsseg As New clsSeguros
            Dim res As DataSet
            clsseg._ID_SOLICITUD = CInt(Request("sol"))
            res = clsseg.GetDatosPoliza()
            If res.Tables.Count > 0 Then
                If res.Tables(0).Rows.Count > 0 Then
                    Dim canclmarsh As New SolCanclPolMARSH
                    canclmarsh.policy.complement.user.id = System.Configuration.ConfigurationManager.AppSettings("useridMarsh").ToString()
                    canclmarsh.policy.complement.user.credentials.accessPassword = System.Configuration.ConfigurationManager.AppSettings("passwordMarsh").ToString()
                    canclmarsh.policy.policyId = res.Tables(0).Rows(0).Item("PDK_NO_POLIZA_SEG_AUTO").ToString
                    canclmarsh.policy.cancelData.effectiveDate = Date.Now.ToString("yyyy-MM-dd")
                    canclmarsh.policy.cancelData.idCancelReason = "01"
                    canclmarsh.policy.cancelData.notes = ""
                    canclmarsh.policy.cancelData.param1 = ""
                    canclmarsh.policy.cancelData.param2 = ""
                    Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim jsonbody As String = serializer.Serialize(canclmarsh)
                    Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                    Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
                    Dim restful As RESTful = New RESTful()
                    restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriCanclMARSH").ToString()

                    Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
                    Dim errormessage As String
                    If (restful.IsError) Then
                        errormessage = restful.MensajeError
                        Throw New Exception(errormessage)
                    Else
                        Dim ress As ResCanclPolMARSH = serializer.Deserialize(Of ResCanclPolMARSH)(respuesta)
                        clsseg._ID_ENDOSO = ress.policy.cancelData.endorsementId.ToString
                        clsseg._TIPO_SEG = 1
                        If clsseg.InsertCancelacionPoliza() Then
                            CancelaMARSH = True
                        Else
                            Master.MensajeError(clsseg.StrError)
                            Exit Function
                        End If
                    End If
                End If
            End If
            CancelaMARSH = True
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Function
    Public Function CancelaBBVA(ByVal idquote As String, ByVal tiposeg As Integer) As Boolean
        Try
            CancelaBBVA = False
            Dim clsseg As New clsSeguros
            Dim res As DataSet
            clsseg._ID_SOLICITUD = CInt(Request("sol"))
            res = clsseg.GetDatosPoliza()
            If res.Tables.Count > 0 Then
                If res.Tables(0).Rows.Count > 0 Then
                    Dim clscancbbva As New cancelquotebbva
                    clscancbbva.header.aapType = System.Configuration.ConfigurationManager.AppSettings("aapTypeBBVA").ToString()
                    clscancbbva.header.dateRequest = DateTime.Now.ToString("dd-MM-yyyy") & " 00:00:00"
                    clscancbbva.header.channel = System.Configuration.ConfigurationManager.AppSettings("channelBBVA").ToString()
                    clscancbbva.header.subChannel = System.Configuration.ConfigurationManager.AppSettings("subChannelBBVA").ToString()
                    clscancbbva.header.branchOffice = System.Configuration.ConfigurationManager.AppSettings("branchOfficeBBVA").ToString()
                    clscancbbva.header.managementUnit = System.Configuration.ConfigurationManager.AppSettings("managementUnitBBVA").ToString()
                    clscancbbva.header.user = System.Configuration.ConfigurationManager.AppSettings("userBBVA").ToString()
                    clscancbbva.header.idSession = "3232-3232"
                    clscancbbva.header.idRequest = "1212-121212-12121-212"
                    clscancbbva.header.dateConsumerInvocation = DateTime.Now.ToString("dd-MM-yyyy") & " 00:00:00"
                    clscancbbva.quote.idQuote = idquote
                    clscancbbva.policyIndicatorLinked = True
                    Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim jsonbody As String = serializer.Serialize(clscancbbva)
                    Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                    Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
                    Dim restful As RESTful = New RESTful()
                    restful.Uri = System.Configuration.ConfigurationManager.AppSettings("uriCanclBBVA").ToString()

                    Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
                    Dim errormessage As String
                    If (restful.IsError) Then
                        errormessage = restful.MensajeError
                        Throw New Exception(errormessage)
                    Else
                        Dim ress As ResCanclPolMARSH = serializer.Deserialize(Of ResCanclPolMARSH)(respuesta)
                        If tiposeg = 2 Then
                            clsseg._ID_ENDOSO = -1
                            clsseg._TIPO_SEG = 1
                            If clsseg.InsertCancelacionPoliza() Then
                                CancelaBBVA = True
                            Else
                                Master.MensajeError(clsseg.StrError)
                                Exit Function
                            End If
                        ElseIf tiposeg = 1 Then
                            clsseg._ID_ENDOSO = -1
                            clsseg._TIPO_SEG = 0
                            If clsseg.InsertCancelacionPoliza() Then
                                CancelaBBVA = True
                            Else
                                Master.MensajeError(clsseg.StrError)
                                Exit Function
                            End If
                        Else
                            clsseg._ID_ENDOSO = -1
                            clsseg._TIPO_SEG = 3
                            If clsseg.InsertCancelacionPoliza() Then
                                CancelaBBVA = True
                            Else
                                Master.MensajeError(clsseg.StrError)
                                Exit Function
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Function

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
            Solicitudes.PDK_ID_SOLICITUD = Request("Sol")
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            Solicitudes.PDK_ID_PANTALLA = idPant
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                Response.Redirect("../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If



            dslink = objtarea.SiguienteTarea(Solicitudes.PDK_ID_SOLICITUD)

            muestrapant = objpantalla.SiguientePantalla(Solicitudes.PDK_ID_SOLICITUD)

            Dim strLocation As String = String.Empty

            If muestrapant = 0 Then
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("IdUsua")).ToString)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            ElseIf muestrapant = 2 Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub
End Class
