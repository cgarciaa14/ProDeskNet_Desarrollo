Imports ProdeskNet.Catalogos
Imports ProdeskNet.WCF
Imports System.Data
Imports ProdeskNet.SN
Imports ProdeskNet.BD
'BUG-PD-116: RHERNANDEZ: 26/06/17: SE CREA PAMNTALLA QUE SE ENCARGARA DE LA EMISION DE POLIZAS DE DAÑO Y VIDA PERO SOLO DE BBVA
'BUG-PD-128: RHERNANDEZ: 01/07/17: SE MODIFICA EL USO DE BOTONES AL CARGAR LA PAGINA
'BUG-PD-193: RHERNANDEZ: 21/08/17: SE TOMAN VARIABLES DE LLAMADO DE SERVICIOS DE EMISION DE WEB CONFIG
'BUG-PD-246: RHERNANDEZ: 25/10/17:  SE CORIGE PROBLEMA AL REGRESAR AL PANEL RECUPERANDO CORRECTAMENTE EL NUMERO DE SOLICITUD 
'BUG-PD-264: RHERNANDEZ: 10/11/17: SE CORRIGE PROBLEMA QUE AL FALLAR EL SERVICIO TE PERMITA REGRESAR O PROCESAR LA TAREA NUEVAMENTE
'BUG-PD-330: RHERNANDEZ: 05/01/17: SE VALIDA QUE LA SOLICITUD CUENTE CON DATOS DE POLIZA DE CASO CONTRARIO NO DEJA FINALIZAR LA TAREA
'BUG-PD-360: RHERNANDEZ: 19/02/18: SE AGREGA LA LECTURA DE IDQUOTE DE SEGURO DE VIDA PARA EMITIR SEGUROS INDEPENDIENTEMENTE
Partial Class aspx_PantallaEmiteBBVA
    Inherits System.Web.UI.Page
    Dim BD As New clsManejaBD
    Dim clien As New clsDatosCliente
    Dim sol As New clsStatusSolicitud
    Protected Sub btnproc_Click(sender As Object, e As EventArgs)
        If emiteBBVA() Then
            asignaTarea(0)
        Else
            cmbguardar1.Disabled = False
            btnRegresar.Enabled = True
        End If
    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub
    Public Function emiteBBVA() As Boolean
        Try
            emiteBBVA = False
            Dim clsseg As clsSeguros = New clsSeguros
            clsseg._ID_SOLICITUD = CInt(Request("sol").ToString())
            Dim DatosSeguro As DataSet = clsseg.getDatosSeguro()
            If (DatosSeguro.Tables.Count > 0) Then
                If (DatosSeguro.Tables(0).Rows.Count > 0) Then
                    If (DatosSeguro.Tables(0).Rows(0).Item("TIPO_SEG_AUTO").ToString() <> "32") And (CInt(DatosSeguro.Tables(0).Rows(0).Item("ID_BROKER").ToString()) = 5 Or CInt(DatosSeguro.Tables(0).Rows(0).Item("ID_BROKER").ToString()) = 9) Then
                        If emitepoliza(DatosSeguro.Tables(0).Rows(0).Item("NO_COTSEG").ToString) Then
                            emiteBBVA = True
                        Else
                            emiteBBVA = False
                            Exit Function
                        End If
                    End If
                    If DatosSeguro.Tables(0).Rows(0).Item("TIPO_SEG_VIDA").ToString() <> "172" Then
                        If emitepoliza(DatosSeguro.Tables(0).Rows(0).Item("NO_COTSEG_VIDA").ToString) Then
                            emiteBBVA = True
                        Else
                            emiteBBVA = False
                            Exit Function
                        End If
                    End If
                    If DatosSeguro.Tables(0).Rows(0).Item("TIPO_SEG_VIDA").ToString() = "172" And DatosSeguro.Tables(0).Rows(0).Item("TIPO_SEG_AUTO").ToString() = "32" Then
                        emiteBBVA = True
                    End If
                Else
                    Throw New Exception("Solicitud no cuenta con datos de polizas")
                End If
            Else
                Throw New Exception("Solicitud no cuenta con datos de polizas")
            End If



        Catch ex As Exception
            cmbguardar1.Attributes.Remove("disabled")
            btnRegresar.Attributes.Remove("disabled")
            emiteBBVA = False
            Master.MensajeError(ex.Message)
        End Try
    End Function
    Public Function emitepoliza(ByVal id_cot As String) As Boolean
        Try
            emitepoliza = False
            Dim clemitseg As New createquoteBBVA
            clemitseg.header.aapType = System.Configuration.ConfigurationManager.AppSettings("aapTypeBBVA").ToString()
            clemitseg.header.dateRequest = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")
            clemitseg.header.channel = System.Configuration.ConfigurationManager.AppSettings("channelBBVA").ToString()
            clemitseg.header.subChannel = System.Configuration.ConfigurationManager.AppSettings("subChannelBBVA").ToString()
            clemitseg.header.branchOffice = System.Configuration.ConfigurationManager.AppSettings("branchOfficeBBVA").ToString()
            clemitseg.header.managementUnit = System.Configuration.ConfigurationManager.AppSettings("managementUnitBBVA").ToString()
            clemitseg.header.user = System.Configuration.ConfigurationManager.AppSettings("userBBVA").ToString()
            clemitseg.header.idSession = "3232-3232"
            clemitseg.header.idRequest = "1212-121212-12121-212"
            clemitseg.header.dateConsumerInvocation = Date.Now.ToString("dd-MM-yyyy hh:mm:ss")

            clemitseg.quote.idQuote = id_cot

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            serializer.MaxJsonLength = Int32.MaxValue
            Dim jsonbody As String = serializer.Serialize(clemitseg)
            Dim userid As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim idticket As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim restful As RESTful = New RESTful()
            restful.Uri = System.Configuration.ConfigurationManager.AppSettings("createPolicyBBVA").ToString()

            Dim respuesta As String = restful.ConnectionPost(userid, idticket, jsonbody)
            Dim res As rescreatePolicy = serializer.Deserialize(Of rescreatePolicy)(respuesta)
            If (restful.IsError) Or res.messageInfo.messageId <> "" Then
                If res.messageInfo.messageId <> "20382" Then
                    If Not IsNothing(res.messageInfo.message) Then
                        Throw New Exception(res.messageInfo.message.ToString.Replace(vbLf, ", "))
                    Else
                        Dim errores As msjerr = serializer.Deserialize(Of msjerr)(respuesta)
                        If errores.message.ToString <> Nothing Then
                            Throw New Exception(errores.message.ToString.Replace(vbLf, ", "))
                        Else
                            Throw New Exception(restful.MensajeError.ToString.Replace(vbLf, ", "))
                        End If
                    End If
                Else
                    emitepoliza = True
                End If
            Else
                emitepoliza = True
            End If
        Catch ex As Exception
            emitepoliza = False
            cmbguardar1.Attributes.Remove("disabled")
            btnRegresar.Attributes.Remove("disabled")
            Master.MensajeError("Error WS: " + ex.Message)
        End Try
    End Function
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

                If muestrapant = 0 Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '../aspx/consultaPanelControl.aspx');", True)
                ElseIf muestrapant = 2 Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("Sol") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "&sol=" & Val(Request("Sol")).ToString & "');", True)
                    'Response.Redirect("../aspx/consultaPanelControl.aspx")
                End If
            End If

        Catch ex As Exception
            cmbguardar1.Attributes.Remove("disabled")
            btnRegresar.Attributes.Remove("disabled")
            cmbguardar1.Disabled = False
            Master.MensajeError(mensaje)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        clien.GetDatosCliente(Request("sol"))
        sol.getStatusSol(Request("sol"))

        Dim intEnable As Integer
        Dim dsresult As New DataSet
        Dim dsresulta As New DataSet
        hdPantalla.Value = Request("idPantalla")
        hdSolicitud.Value = Request("sol")
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
                    hdnResultado.Value = (dsresulta.Tables(0).Rows(0).Item("RUTA") & "?sol=" & hdSolicitud.Value & "&IdPantalla=" & pantallas & "&usuario=" & hdusuario.Value)
                    hdnResultado2.Value = (dsresulta.Tables(0).Rows(0).Item("RUTA") & "?sol=" & hdSolicitud.Value & "&IdPantalla=" & pantallas & "&usuario=" & hdusuario.Value)
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

        If intEnable = 1 Then
            cmbguardar1.Attributes.Add("style", "display:none;")
        Else
            If Not IsPostBack Then
                btnproc_Click(btnproc, Nothing)
            End If
        End If


    End Sub
End Class
Public Class msjerr
    Public message As String
    Public status As String
End Class
