'BBVA-P-423 RQADM-11 GVARGAS 10/03/2017 Antifraude Básico – Carruseles (Celulares, RFC, ID’s, Contador de Teléfonos) 31
'BBVA-P-423 13 Check HB GVARGAS 23/03/2017 Check Carruseles y Visor Documental
'<%--<%BBV-P-423:RQAMD-26 JBEJAR 11/05/2017 SE PROGRAMA  CEDULA ANTIFRAUDE.--
'BUG-PD-50 JBEJAR 22/05/2017 SE AGREGA FUNCION DE GUARDADO DE TIPIFICACION Y CORRECIONES.    
'BUG-PD-75 JBEJAR 07/06/2017 MENSAJE TAREA EXITOSA AL PROCESAR. 
'BUG-PD-103 JBEJAR 17/06/2017 JBEJAR SE CORRIGE EL BOTON DEL VISOR QUE BLOQUEABA EL BOTON PROCESAR. 
'BUG-PD-161  ERODRIGUEZ:18/06/2017:JBEJAR: SE CORRIGE EL RESPONSE REDIRECT. 
'BUG-PD-203: CGARCIIA: 04/09/2017: SE AGREGA NOTIFICACIONES 
'BUG-PD-215: CGARCIA: 26/09/2017: ACTUALIZACION DE CORREOS PARA CASOS AL QUE ENTRA.
'BUG-PD-227: CGARCIA: 05/10/2017: ACTUALIZACION DE EMAILS Y SCORE
'BUG-PD-276: CGARCIA: 27/11/2017: CORRECCION DE MSJ Y REQUEST DE USUARIOS EN EMAILS
'BBV-P-423 RQ-PD-17 6 GVARGAS 16/01/2018 details INE
'BBV-P-423 RQ-PD-17 7 GVARGAS 22/01/2018 Correciones generales
'BBV-P-423 RQ-PD-17 8 GVARGAS 29/01/2018 Correcion detalle y turnar
'BBV-P-423 RQ-PD-17 13 GVARGAS 12/02/2018 Ajustes flujos 4
'BBV-P-423 RQ-PD-17 14 GVARGAS 13/02/2018 Ajustes flujo 5
'BUG-PD-380 GVARGAS 02/03/2018 Correccion urgente instalacion biometrico

Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.BD
Imports ProdeskNet.SN
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.SN.clsCatTareas

Partial Class aspx_CelulaAntifraude
    Inherits System.Web.UI.Page
    Dim clien As New clsDatosCliente
    Dim sol As New clsStatusSolicitud
    Dim BD As New clsManejaBD
    Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Public ClsEmail As New clsEmailAuto()
    Dim usu As String


    Private Sub aspx_CelulaAntifraude_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (Request("idPantalla").ToString() = 177) Then
            btnProcess.Visible = True
            btnProcesarCliente.Visible = False
            ddlTurnar.Visible = True
            If Request("Enable").ToString() = 1 Then
                ddlTurnar.Enabled = False
            End If
            ddlTipificaciones.Visible = False
            lblTextTip.Text = "Turnar a:"
        Else
            btnProcess.Visible = False
            btnProcesarCliente.Visible = True
            ddlTurnar.Visible = False
            ddlTipificaciones.Visible = True
            lblTextTip.Text = "Tipificaciones *"
        End If

        Dim objFlujos As New clsSolicitudes(0)
        Dim clscel As New clsCelula()
        Dim ds As DataSet
        If Session("Regresar") Is Nothing Then
            Session("Regresar") = Request.UrlReferrer.LocalPath
            hdRutaEntrada.Value = Request.UrlReferrer.LocalPath
        End If

        usu = Val(Request("usuario"))
        If usu = String.Empty Then
            usu = Val(Request("usu"))
        End If

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
        objFlujos.PDK_ID_SOLICITUD = Request("sol")
        Session.Add("idSol", hdSolicitud.Value)
        ds = objFlujos.ConsultaSolicitud(5)
        Me.gvCelula.DataSource = ds
        Me.gvCelula.DataBind()
        Dim intEnable As Integer
        intEnable = CInt(Request.QueryString("Enable"))
        If intEnable = 1 Then

            clscel.ID_SOLICITUD = hdSolicitud.Value
            clscel.PANTALLA = hdPantalla.Value
            Dim dres = clscel.GetCelula
            btnProcesarCliente.Attributes.Add("style", "display:none;")
            If clscel.strError = "" Then
                ddlTipificaciones.Enabled = False
                ddlTipificaciones.SelectedValue = CInt(dres.Tables(0).Rows(0).Item("PDK_TIPIFICACION"))
                btnProcesarCliente.Attributes.Add("style", "display:none;")

            End If
        End If
    End Sub
    Protected Sub btnVisor_Click(sender As Object, e As EventArgs)
        Dim ds As DataSet
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable();", True)

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
    Protected Sub ddlTipoTipificacion_SelectedIndexChanged(sender As Object, e As EventArgs)



        If Me.ddlTipificaciones.SelectedValue <> 0 Then

            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable();", True)

        Else
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable1();", True)

        End If


    End Sub

    'Protected Sub ddlTurnar_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    If Me.ddlTurnar.SelectedValue <> 0 Then
    '        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable();", True)
    '    Else
    '        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable1();", True)
    '    End If
    'End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub

    Protected Sub btnprocesar_Click(sender As Object, e As EventArgs)
        'Dim turnar As Integer = Me.ddlTurnar.SelectedValue
        'If (turnar <> 0) Then
        '    turnarINE(turnar)
        '    Exit Sub
        'End If


        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim mensaje As String = String.Empty
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim objFlujos As New clsSolicitudes(0)

        Dim clscelula As New clsCelula()

        clscelula.ID_SOLICITUD = hdSolicitud.Value
        clscelula.PANTALLA = hdPantalla.Value
        clscelula.TIPIFICACION = ddlTipificaciones.SelectedValue
        If clscelula.insertaDatosCelula Then
            If Me.ddlTipificaciones.SelectedValue = 1 Then

                dsresult = DB.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

                mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString

                dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

                muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

                If muestrapant = 0 Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Mensaje", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString & "');", True)
                ElseIf muestrapant = 2 Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Mensaje", "PopUpLetreroRedirect('" & mensaje & "','" & "../aspx/consultaPanelControl.aspx');", True)
                End If
            ElseIf Me.ddlTipificaciones.SelectedValue = 2 Then

                objFlujos.PDK_ID_SOLICITUD = Request("sol")
                objFlujos.PDK_CLAVE_USUARIO = Request("usu")
                objFlujos.ManejaTarea(3)
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Mensaje", "PopUpLetreroRedirect('Tarea Rechazada','" & "../aspx/consultaPanelControl.aspx');", True)

                Dim dc As New clsDatosCliente
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
                                Dim strLocation As String = String.Empty
                                strLocation = ("../aspx/ValidaEmails.aspx?idPantalla=87&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=2" & "&usuario=" & usu)
                                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Rechazada.', '" + strLocation + "');", True)
                            Else
                                Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Rechazada.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                            End If
                        Else
                            Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Rechazada.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                        End If
                    Else
                        Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                        ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Rechazada.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                    End If
                Else
                    Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Rechazada.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                End If
            ElseIf Me.ddlTipificaciones.SelectedValue = 0 Then

                Master.MensajeError("Es necesario elegir una tipificación")

            End If
        End If


    End Sub

    Protected Sub turnarINE(ByVal turnarOpc As Integer)
        If (turnarOpc = 1) Then
            Dim _clsSolicitudes As clsSolicitudes = New clsSolicitudes(Request("Sol").ToString())
            _clsSolicitudes.PDK_ID_SOLICITUD = Request("Sol").ToString()
            _clsSolicitudes.BOTON = 64
            _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = turnarOpc
            _clsSolicitudes.ValNegocio(1)
            checkRedirect()
        Else
            Dim objCancela As New clsSolicitudes(0)
            objCancela.PDK_ID_SOLICITUD = Request("sol").ToString()
            objCancela.Estatus = 42
            objCancela.PDK_CLAVE_USUARIO = Session("IdUsua")

            objCancela.ManejaTarea(3)

            Dim msg As String = "RECHAZO Solicitud no viable por políticas."
            Dim path As String = "../aspx/consultaPanelControl.aspx"
            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + msg + "', '" + path + "');", True)
        End If
    End Sub

    Protected Sub checkRedirect()
        Dim _clsCatTareas As clsCatTareas = New clsCatTareas()

        Dim _clsPantallas As ProdeskNet.SN.clsPantallas = New ProdeskNet.SN.clsPantallas()
        Dim _dtsResult As DataSet = _clsCatTareas.SiguienteTarea(Val(Request("Sol")))
        Dim _mostrarPantalla As String = _clsPantallas.SiguientePantalla(Val(Request("Sol")))
        Dim strLocation As String = String.Empty
        If _mostrarPantalla = 0 Then
            strLocation = ("../aspx/" & _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() & "?idPantalla=" & _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() & "&sol=" & Request("sol").ToString())
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
        ElseIf _mostrarPantalla = 2 Then
            strLocation = ("../aspx/consultaPanelControl.aspx")
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
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
            'get_Response_INE_SP @PDK_ID_SECCCERO INT, @OPCION INT
            'EXEC crud_Biometrico_SP 3534, 0, 23, ''
            'EXEC crud_Biometrico_SP 3534, 1, 23, ''
            cmd.CommandText = "crud_Biometrico_SP"
            'cmd.CommandText = "crud_Biometrico_SP " + folio + ", " + opc.ToString() + ", 23, ''"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Parameters.AddWithValue("@PDK_ID_TAREAS", opc)
            cmd.Parameters.AddWithValue("@OPCION", "23")
            cmd.Parameters.AddWithValue("@XML_INFO", "")
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

    <System.Web.Services.WebMethod()> _
    Public Shared Function btnProcessINE(ByVal turnarOpc As Integer, ByVal sol As String, ByVal usr As Integer) As String
        Dim msgPath As msgPath = New msgPath()
        msgPath.path = "../aspx/consultaPanelControl.aspx"

        If (turnarOpc = 1) Then
            Dim _clsSolicitudes As clsSolicitudes = New clsSolicitudes(sol)
            _clsSolicitudes.PDK_ID_SOLICITUD = sol
            _clsSolicitudes.BOTON = 64
            _clsSolicitudes.PDK_CLAVE_USUARIO = usr
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = turnarOpc
            _clsSolicitudes.ValNegocio(1)

            msgPath.msg = "Tarea Exitosa."

            Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
            Dim cmd As New SqlCommand
            Dim reader As SqlDataReader
            Try
                cmd.CommandText = "exec_SP"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@query", "DELETE FROM PDK_HISTORY_AUTHENTICATION WHERE PDK_ID_SECC_CERO = " + sol)

                cmd.Connection = sqlConnection1
                sqlConnection1.Open()
                reader = cmd.ExecuteReader()
                Do While reader.Read()
                Loop
            Catch ex As Exception
            End Try
            sqlConnection1.Close()
        ElseIf (turnarOpc = 2) Then
            Dim objCancela As New clsSolicitudes(0)
            objCancela.PDK_ID_SOLICITUD = sol
            objCancela.Estatus = 42
            objCancela.PDK_CLAVE_USUARIO = usr

            objCancela.ManejaTarea(3)

            msgPath.msg = "RECHAZO Solicitud no viable por políticas."
        Else
            Dim query As StringBuilder = New StringBuilder()
            query.Append("EXEC crud_Biometrico_SP " + sol.ToString() + ", 177, 19, ''")

            Dim tarea As Integer = 0
            Dim link As String = String.Empty
            Dim usuario As String = String.Empty

            Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
            Dim cmd As New SqlCommand
            Dim reader As SqlDataReader
            Try
                cmd.CommandText = "exec_SP"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@query", query.ToString())

                cmd.Connection = sqlConnection1
                sqlConnection1.Open()
                reader = cmd.ExecuteReader()
                Do While reader.Read()
                    tarea = Int32.Parse(reader("TASK").ToString())
                    link = reader("LINK").ToString()
                    usuario = reader("USR").ToString()
                    msgPath.msg = reader("MSG").ToString()
                Loop
            Catch ex As Exception
            End Try
            sqlConnection1.Close()

            Dim _clsSolicitudes As clsSolicitudes = New clsSolicitudes(sol)
            _clsSolicitudes.PDK_ID_SOLICITUD = sol
            _clsSolicitudes.BOTON = 64
            _clsSolicitudes.PDK_CLAVE_USUARIO = usr
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = tarea
            _clsSolicitudes.ValNegocio(1)

            msgPath.path = "../aspx/" + link + "?idPantalla=" + tarea.ToString() + "&sol=" + sol + "&usuario=" + usuario
        End If

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Respuesta As String = serializer.Serialize(msgPath)
        Return json_Respuesta
    End Function

    Public Class msgPath
        Public path As String
        Public msg As String
    End Class

    <System.Web.Services.WebMethod()> _
    Public Shared Function OpenVisor(ByVal folio As Integer) As String
        Dim strVisor As String = "../Comparador.aspx"
        Dim ds As DataSet
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos

        'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "btnEnabled", "btnProcesarCliente_Enable();", True)

        objCatalogos.Parametro = folio 'Me.lblSolicitud.Text
        ds = objCatalogos.Catalogos(7)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Dim Cliente_bbva As String = ""

                Cliente_bbva = ds.Tables(0).Rows(0).Item("CLIENTE_BBVA")

                If Cliente_bbva <> "" Then
                    'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "&cliente=" + Cliente_bbva.ToString + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)
                    strVisor = strVisor + "?folio=" + folio.ToString() + "&cliente=" + Cliente_bbva.ToString + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable"
                Else
                    'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)
                    strVisor = strVisor + "?folio=" + folio.ToString() + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable"
                End If
            End If
        End If
        Return strVisor
    End Function
End Class
