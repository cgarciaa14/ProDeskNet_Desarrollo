'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BBVA-P-423 RQADM-12 GVARGAS 14/02/2017 Carrusel Empresas
'BUG-PD-13  GVARGAS  28/02/2017 Mensaje cancela
'BUG-PD-24  GVARGAS  29/03/2017 Bugs Campos Desabilitados en Pre si la SOL se regresa
'BBVA-P-423 RQSOLBCOM-01 GVARGAS 16/04/2017 Corrige al cancelar la tarea
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
'BUG-PD-203: CGARCIIA: 04/09/2017: SE AGREGA NOTIFICACIONES 
'BUG-PD-215: CGARCIA: 26/09/2017: ACTUALIZACION DE CORREOS PARA CASOS AL QUE ENTRA.
'BUG-PD-227: CGARCIA: 05/10/2017: ACTUALIZACION DE EMAILS Y SCORE
'BUG-PD-276: CGARCIA: 27/11/2017: CORRECCION DE MSJ Y REQUEST DE USUARIOS EN EMAILS
'BUG-PD-313: DJUAREZ: 27/12/2017: Se evita el avanzar tareas cuando se presiona F5 cuando esta cargando una pagina.
Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.BD
Imports ProdeskNet.SN
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.WCF

Partial Class aspx_EmpresasNegativas
    Inherits System.Web.UI.Page
    Dim BD As New clsManejaBD
    Public ClsEmail As New clsEmailAuto()
    Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim usu As String

    Private Sub aspx_EmpresasNegativas_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'Validacion de Request
            Dim Validate As New clsValidateData
            Dim Url As String = Validate.ValidateRequest(Request)

            If Url <> String.Empty Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", Url, True)
                Exit Sub
            End If
            'Fin validacion de Request

            Dim objpant As New ProdeskNet.SN.clsPantallas()
            Dim dts As New DataSet()

            dts = objpant.CargaPantallas(Val(Request("idPantalla")).ToString)

            usu = Val(Request("usuario"))
            If usu = String.Empty Then
                usu = Val(Request("usu"))
            End If

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Then
                        btnprocesar_Click(btnprocesar, Nothing)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnprocesar_Click(sender As Object, e As EventArgs) Handles btnprocesar.Click
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()

        Dim hitCarrusel As Boolean = CarruselEmpresa(Request("Sol").ToString())

        If (hitCarrusel) Then
            CancelaTarea()

            Dim dc As New clsDatosCliente
            dc.idSolicitud = Val(Request("sol"))
            dc.getDatosSol()
            'Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)

            ClsEmail.OPCION = 17
            ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

            Dim dtConsulta = New DataSet()
            dtConsulta = ClsEmail.ConsultaStatusNotificacion
            If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
                If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                    If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
                        If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
                            Dim strLocation As String = String.Empty
                            strLocation = ("../aspx/ValidaEmails.aspx?idPantalla=70&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=2" & "&usuario=" & usu)
                            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '" + strLocation + "');", True)
                        Else
                            Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                        End If
                    Else
                        Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                        ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                    End If
                Else
                    Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                End If
            Else
                Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
            End If
            'Return
        Else
            'dsresult = DB.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

            'dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            'muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

            'If muestrapant = 0 Then
            '    Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
            'ElseIf muestrapant = 2 Then
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "PopUpLetreroRedirect('Tarea Exitosa.', '../aspx/consultaPanelControl.aspx');", True)
            'End If

            dsresult = DB.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

            Dim mensaje As String = "Tarea Exitosa"
            Dim strLocation As String = String.Empty

            If muestrapant = 0 Then
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & usu)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            ElseIf muestrapant = 2 Then

                Dim dc As New clsDatosCliente
                dc.idSolicitud = Val(Request("sol"))
                dc.getDatosSol()
                Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona

                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Exitosa.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
            End If
        End If
    End Sub

    Private Function CarruselEmpresa(ByVal folio As String) As String
        Dim respuesta As Boolean = False
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_HIT_BNEMPRESA_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@folio", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                respuesta = reader(0)
            Loop
        Catch ex As Exception
            respuesta = False
        End Try

        sqlConnection1.Close()
        Return respuesta
    End Function

    Public Sub CancelaTarea()
        Dim objCancela As New clsSolicitudes(0)
        objCancela.PDK_ID_SOLICITUD = Request("Sol").ToString()
        objCancela.Estatus = 42
        objCancela.PDK_CLAVE_USUARIO = Session("IdUsua")
        objCancela.PDK_ID_PANTALLA = Request("idPantalla").ToString()

        objCancela.ManejaTarea(6)

    End Sub
End Class
