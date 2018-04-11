#Region "TRACKERS"
'BUG-PD-22 JBB 30/03/2017 Se tarea automatica valida calculo de ingresos. 
'BBV-P-423:RQAMD-26 JBEJAR 11/05/2017 CORRECION AL RECUPERAR EL ID DE LA PANTALLA AUTOMATICA.
'BUG-PD-49:MPUESTO:12/10/2017:CORRECCIÓN DE LA OBTENCIÓN DEL PARAMETRO IDPANTALLA Y 
'BUG-PD-159 24/07/2017 SE CAMBIA RESPONSE.
'BUG-PD-203: CGARCIIA: 04/09/2017: SE AGREGA NOTIFICACIONES 
'BUG-PD-215: CGARCIA: 26/09/2017: ACTUALIZACION DE CORREOS PARA CASOS AL QUE ENTRA.
'BUG-PD-227: CGARCIA: 05/10/2017: ACTUALIZACION DE EMAILS Y SCORE
'BUG-PD-232: JBEJAR: 12/10/2017:  SE MODIFICA   LA CONDICION QUE DISPARA EL EVENTO  DE EMAILS.  
'BUG-PD-276: CGARCIA: 27/11/2017: CORRECCION DE MSJ Y REQUEST DE USUARIOS EN EMAILS
'BUG-PD-297: JBEJAR: 08/12/2017:  TAREA-PERFIL  SE AGREGA DOCUMENTO 127 COMPROBANTE OPCIONAL. 
'BUG-PD-331: DJUAREZ: 09/01/2017: Regreso al panel sin filtrar
'BUG-PD-348 GVARGAS 25/01/2017 Correcion regreso Emails
#End Region
Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.Configurcion
Partial Class aspx_ValidaCI
    Inherits System.Web.UI.Page
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim strError As String = String.Empty
    Public Pantalla As Integer
    Public ClsEmail As New ProdeskNet.SN.clsEmailAuto()
    Dim usu As String
    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpant As New ProdeskNet.SN.clsPantallas()
            Dim dts As New DataSet()
            Dim Bandera As Integer = 1
            Dim dsParametros As New DataSet
            Dim objTarea As New clsSolicitudes(0)
            Dim ValTareas As Integer = 0
            Dim Documento As String = ""
            Dim MontoD As String = ""
            Dim Fecha As Date = DateTime.Now.ToString()
            Dim objFlujos As New clsSolicitudes(0)
            Dim ds_siguienteTarea As DataSet

            usu = Val(Request("usuario"))
            If usu = String.Empty Then
                usu = Val(Request("usu"))
            End If

            Pantalla = Convert.ToInt32(Val(Request("idPantalla")).ToString())
            objTarea.PDK_ID_SOLICITUD = Request("Sol")
            objTarea.PDK_ID_PANTALLA = Pantalla

            ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & Pantalla.ToString())

            'CONSULTA PARAMETROS VERIFICAR  QUE EXISTA EL DOCUMENTO 5 COMPROBANTE CALCULO DE INGRESOS 127 COMPROBANTE OPCIONAL. 
            dsParametros = BD.EjecutarQuery("SELECT * FROM PDK_REL_PAN_DOC_SOL WHERE PDK_ID_SECCCERO=" & objTarea.PDK_ID_SOLICITUD & "AND PDK_ID_DOCUMENTOS IN(5,127)")


            If dsParametros.Tables.Count > 0 Then
                If dsParametros.Tables(0).Rows.Count > 0 Then
                    Documento = Val(dsParametros.Tables(0).Rows(0).Item("PDK_ID_DOC_SOLICITUD").ToString())
                    If Documento <> "" Then
                        Bandera = 0
                    End If
                End If
            End If

            dts = objpant.CargaPantallas(Pantalla)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Or (Bandera = 1) Then
                        If Bandera = 1 Then

                            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"), 1)

                        Else

                            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"), 0)

                        End If

                    End If
                End If
            End If
        End If
    End Sub
    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer, ByVal intRechazo As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Try
            'SE LLENAN LOS PARAMETROS PARA EJECUTAR METODOS DE LA CLASE clsSolicitudes
            Solicitudes.BOTON = 64                                  'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_SOLICITUD = Request("Sol")      'PARAMETRO PARA ValNegocio() Y ManejaTarea()
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")          'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_PANTALLA = Pantalla    'PARAMETRO PARA ManejaTarea()
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla    'PARAMETRO PARA ManejaTarea()

            dsresult = Solicitudes.ValNegocio(1)                    'SE ADELANTA LA TAREA ESPECIFICADA(RECHAZO O NORECHAZO) Y OBTENEMOS MENSAJE
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje  ' SE ASIGNA MENSAJE COMO PARAMETRO PARA ManejaTarea()
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                'Dim redirect As String = "../aspx/consultaPanelControl.aspx"
                'Dim script As String = "PopUpLetreroRedirect('" + mensaje + "','" + redirect + "');"
                'ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", script, True)
                Response.Redirect("../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If

            'BD.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

            dslink = objtarea.SiguienteTarea(Solicitudes.PDK_ID_SOLICITUD)

            muestrapant = objpantalla.SiguientePantalla(Solicitudes.PDK_ID_SOLICITUD)


            Dim strLocation As String = String.Empty

            If muestrapant = 0 Then
                'strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                Dim dc As New ProdeskNet.Catalogos.clsDatosCliente
                dc.idSolicitud = Val(Request("sol"))
                dc.getDatosSol()
                ClsEmail.OPCION = 17
                ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

                Dim dtConsulta = New DataSet()
                dtConsulta = ClsEmail.ConsultaStatusNotificacion
                If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
                    If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                        If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
                            If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
                                Dim strLocation1 As String = String.Empty
                                strLocation1 = ("../aspx/ValidaEmails.aspx?idPantalla=103&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=0" & "&usuario=" & usu)
                                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation1 & "';", True)
                                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '" + strLocation1 + "');", True)
                            Else
                                'Dim str As String = String.Empty
                                'ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                            End If
                        Else
                            'Dim str As String = String.Empty
                            'ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                            strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                        End If
                    Else
                        'Dim str As String = String.Empty
                        'ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                        strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                    End If
                Else
                    'strLocation = ("../aspx/consultaPanelControl.aspx")
                    'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                    strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                End If
            ElseIf muestrapant = 2 Then
                If (intRechazo = 1) Then
                    Dim dc As New ProdeskNet.Catalogos.clsDatosCliente
                    dc.idSolicitud = Val(Request("sol"))
                    dc.getDatosSol()
                    ClsEmail.OPCION = 17
                    ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

                    Dim dtConsulta = New DataSet()
                    dtConsulta = ClsEmail.ConsultaStatusNotificacion
                    If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
                        If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                            If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
                                If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
                                    Dim strLocation1 As String = String.Empty
                                    strLocation1 = ("../aspx/ValidaEmails.aspx?idPantalla=103&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=2" & "&usuario=" & Val(Request("usuario")).ToString)
                                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation1 & "';", True)
                                Else
                                    Dim str As String = String.Empty
                                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                                End If
                            Else
                                Dim str As String = String.Empty
                                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                            End If
                        Else
                            Dim str As String = String.Empty
                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                        End If
                    Else
                        strLocation = ("../aspx/consultaPanelControl.aspx")
                        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                    End If
                Else
                    strLocation = ("../aspx/consultaPanelControl.aspx")
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                End If
                'strLocation = ("../aspx/consultaPanelControl.aspx")
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub
End Class

