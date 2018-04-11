'BBVA-P-423 RQADM-11 GVARGAS 21/03/2017 Antifraude Básico – Carruseles (Celulares, RFC, ID’s, Contador de Teléfonos) 31
'BBVA-P-423 RQADM-13 Check HB GVARGAS 23/03/2017 Check Carruseles y Visor Documental
'BUG-PD-24  GVARGAS  29/03/2017 Bugs Campos Desabilitados en Pre si la SOL se regresa
'BBV-P-423-RQADM-09 JBB 04/04/2017 Correciones  MARRENDO. 
'BUG-PD-26 MAPH 12/04/2017 Correcciones solicitadas por MARRENDONDO.
'BUG-PD-38 GVARGAS 24/04/2017 TAB Check HB sol turnada
'BUG-PD-57 GVARGAS 18/05/2017 Cambios al cancelar tarea
'BBVA-P-423 RQ-CHECKHB-ProDesk GVARGAS 24/07/2017 detalles HIT
'BUG-PD-163 GVARGAS 31/07/2017 Cambios detalles
'BUG-PD-205 RHERNANDEZ:31/08/17: Correccion de lectura de cliente incredit
'BUG-PD-203: CGARCIIA: 04/09/2017: SE AGREGA NOTIFICACIONES 
'BUG-PD-215: CGARCIA: 26/09/2017: ACTUALIZACION DE CORREOS PARA CASOS AL QUE ENTRA.
'BUG-PD-227: CGARCIA: 05/10/2017: ACTUALIZACION DE EMAILS Y SCORE
'BUG-PD-250: CGARCIA: 31/10/2017: LINK DE REPORTES EN LA CONSULTA DE DETALLES DE CELULAR, TELEFONO Y RFC
'BUG-PD-276: CGARCIA: 27/11/2017: CORRECCION DE MSJ Y REQUEST DE USUARIOS EN EMAILS
'BBV-P-423 RQ-PD-17 10 GVARGAS 31/01/2018 Ajustes flujos

Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.BD
Imports ProdeskNet.SN
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Class aspx_CheckHB
    Inherits System.Web.UI.Page
    Dim BD As New ProdeskNet.BD.clsManejaBD
    Dim dc As New ProdeskNet.Catalogos.clsDatosCliente
    Dim es As New ProdeskNet.Catalogos.clsStatusSolicitud
    Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Public ClsEmail As New clsEmailAuto()
    Dim usu As String

    Private Sub aspx_CheckHB_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("Regresar") Is Nothing Then
            Session("Regresar") = Request.UrlReferrer.LocalPath
            hdRutaEntrada.Value = Request.UrlReferrer.LocalPath
        End If

        usu = Val(Request("usuario"))
        If usu = String.Empty Then
            usu = Val(Request("usu"))
        End If

        hdnIdFolio.Value = Request.QueryString("sol")
        hdnIdPantalla.Value = Request.QueryString("idpantalla")
        hdnUsua.Value = Session("IdUsua")
        dc.GetDatosCliente(Request.QueryString("sol"))
        lblSolicitud.Text = Request.QueryString("sol")
        lblCliente.Text = dc.propNombreCompleto

        es.getStatusSol(Request.QueryString("sol"))
        Me.lblStCredito.Text = es.PStCredito
        Me.lblStDocumento.Text = es.PStDocumento

        Dim intEnable As Integer
        intEnable = CInt(Request.QueryString("Enable"))
        If intEnable = 1 Then
            btnProcesarCliente.Attributes.Add("style", "display:none;")
            btnCancelar.Attributes.Add("style", "display:none;")
        End If

        Dim status_list As List(Of String) = StatusCarruseles(Request.QueryString("sol"))

        If (status_list.Count = 0) Then
            statusFalse()
        Else
            If (status_list.Item(0) = "Error") Then
                statusFalse()
            Else
                statusFalse()
                Dim str As String = status_list(status_list.Count - 1)

                Dim hitCarruselCel As Boolean = CarruselCEL(Request("Sol").ToString())
                If (hitCarruselCel) Then
                    'c_c_1.InnerHtml = CarruselCEL_Details(Request("Sol").ToString())
                    c_c_1.InnerHtml = "<span class='tooltip'><label id='c_cel_1'>SI</label><span class='tooltiptext'>Clic para ver detalles</span></span>"
                End If

                Dim hitCarruselTel As List(Of Boolean) = CarruselTEL(Request("Sol").ToString())
                If hitCarruselTel.Count > 0 Then
                    If (hitCarruselTel.Item(0)) Then
                        'c_t_1.InnerHtml = CarruselTEL_details(Request("Sol").ToString(), 1)
                        c_t_1.InnerHtml = "<span class='tooltip'><label id='c_tel_1'>SI</label><span class='tooltiptext'>Clic para ver detalles</span></span>"
                    End If
                    If (hitCarruselTel.Item(1)) Then
                        'c_t_3.InnerHtml = CarruselTEL_details(Request("Sol").ToString(), 2)
                        c_t_3.InnerHtml = "<span class='tooltip'><label id='c_tel_3'>SI</label><span class='tooltiptext'>Clic para ver detalles</span></span>"
                    End If
                    If (hitCarruselTel.Item(2)) Then
                        'c_t_4.InnerHtml = CarruselTEL_details(Request("Sol").ToString(), 3)
                        c_t_4.InnerHtml = "<span class='tooltip'><label id='c_tel_4'>SI</label><span class='tooltiptext'>Clic para ver detalles</span></span>"
                    End If
                End If

                Dim hitCarruselRFC As Boolean = CarruselRFC(Request("Sol").ToString())
                If (hitCarruselRFC) Then
                    'c_r_1.InnerHtml = CarruselRFC_details(Request("Sol").ToString())
                    c_r_1.InnerHtml = "<span class='tooltip'><label id='c_rfc_1'>SI</label><span class='tooltiptext'>Clic para ver detalles</span></span>"
                End If
            End If
        End If
    End Sub

    Public Sub statusFalse()
        c_r_1.InnerHtml = "NO"
        c_r_2.InnerHtml = "NO"
        c_r_3.InnerHtml = "NO"
        c_r_4.InnerHtml = "NO"

        c_c_1.InnerHtml = "NO"
        c_c_2.InnerHtml = "NO"
        c_c_3.InnerHtml = "NO"
        c_c_4.InnerHtml = "NO"

        c_t_1.InnerHtml = "NO"
        c_t_2.InnerHtml = "NO"
        c_t_3.InnerHtml = "NO"
        c_t_4.InnerHtml = "NO"
    End Sub

    Protected Sub btnprocesar_Click(sender As Object, e As EventArgs)
        Dim str1 As String = Me.opc.Value
        If (str1 = "0") Then
            Return
        ElseIf (str1 = "1") Then

            Dim dsresult As DataSet = New DataSet()
            Dim dslink As DataSet = New DataSet()
            Dim muestrapant As Integer = Nothing
            Dim objtarea As New ProdeskNet.SN.clsCatTareas()
            Dim objpantalla As New ProdeskNet.SN.clsPantallas()

            dsresult = DB.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

            Dim mensaje As String = "Tarea Exitosa"

            Dim ds_siguienteTarea As DataSet
            Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
            objCatalogos.Parametro = Request("idPantalla")
            ds_siguienteTarea = objCatalogos.Catalogos(6)

            Dim tarea_norechazo As String = ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO").ToString()
            Dim str As String = Turned(Request("Sol").ToString(), tarea_norechazo)


            If muestrapant = 0 Then
                'Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)

                Dim stringPath As String = "../aspx/" + dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() + "?idPantalla=" + dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() + "&sol=" + Val(Request("Sol")).ToString() + "&usuario=" + Val(Request("usuario")).ToString()
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('', '" + stringPath + "');", True)
            ElseIf muestrapant = 2 Then

                Dim dc As New clsDatosCliente
                dc.idSolicitud = Val(Request("sol"))
                dc.getDatosSol()
                Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                str_ = ""

                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Exitosa.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
            End If

        ElseIf (str1 = "2") Then
            CancelaTarea()
            Dim str As String = Turned(Request("Sol").ToString(), "999")

            Dim dc As New clsDatosCliente
            dc.idSolicitud = Val(Request("sol"))
            dc.getDatosSol()
            'Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
            'str_ = ""

            'ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)

            ClsEmail.OPCION = 17
            ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

            Dim dtConsulta = New DataSet()
            dtConsulta = ClsEmail.ConsultaStatusNotificacion
            If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
                If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                    If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
                        If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
                            Dim strLocation As String = String.Empty
                            strLocation = ("../aspx/ValidaEmails.aspx?idPantalla=86&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=2" & "&usuario=" & usu)
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
            ElseIf (str1 = "3") Then

                Dim ds_siguienteTarea As DataSet
                Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
                objCatalogos.Parametro = Request("idPantalla")
                ds_siguienteTarea = objCatalogos.Catalogos(6)

                Dim tarea_rechazo As String = ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO").ToString()
                Dim str As String = Turned(Request("Sol").ToString(), tarea_rechazo)

                If ds_siguienteTarea.Tables.Count > 0 Then
                    If ds_siguienteTarea.Tables(0).Rows.Count > 0 Then
                        asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))
                    End If
                End If
            End If
    End Sub

    Public Sub CancelaTarea()
        Dim objCancela As New clsSolicitudes(0)
        objCancela.PDK_ID_SOLICITUD = Request("Sol").ToString()
        objCancela.Estatus = 42
        objCancela.PDK_CLAVE_USUARIO = Session("IdUsua")
        objCancela.PDK_ID_PANTALLA = Request("idPantalla").ToString()
        objCancela.Estatus_Cred = 295

        objCancela.ManejaTarea(6)
    End Sub

    Public Class respuesta
        Public cod As String
        Public mensaje As String
    End Class

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub

    Public Sub llenaCombos()
    End Sub

    Protected Sub btnVisor_Click(sender As Object, e As EventArgs)
        Dim ds As DataSet
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos

        Dim str = opc.Value()

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

    Private Function StatusCarruseles(ByVal folio As String) As List(Of String)
        Dim CD As List(Of String) = New List(Of String)
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_StatusCarruseles_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SOLICITUD", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()


            Do While reader.Read()
                Dim str As String = reader(0)
                CD.Add(str)
            Loop
        Catch ex As Exception
            CD.Add("Error")
        End Try
        sqlConnection1.Close()

        Return CD
    End Function

    Private Function CarruselCEL(ByVal folio As String) As String
        Dim respuesta As Boolean = False
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_Carrusel_Cel_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
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

    Private Function CarruselCEL_Details(ByVal folio As String, ByVal intPantalla As String) As String
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Dim msg As ListDetailCel = New ListDetailCel()

        Try
            cmd.CommandText = "get_Carrusel_Cel_Details_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                Dim detailCel As detailCel = New detailCel()

                detailCel.FOLIO = reader("FOLIO").ToString()
                detailCel.MOVIL = reader("MOVIL").ToString()
                detailCel.RFC = reader("RFC").ToString()
                Dim strLiga As String
                strLiga = "ImprimirCreditoSolicitud.aspx?idPantalla=" + intPantalla + "&IdFolio=" + reader("FOLIO").ToString() + "&CVE=5"
                detailCel.LIGA = strLiga

                msg.detalles.Add(detailCel)
            Loop
        Catch ex As Exception
            msg.detalles.Add(New detailCel())
        End Try

        sqlConnection1.Close()

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Respuesta As String = serializer.Serialize(msg)

        Return json_Respuesta
    End Function

    Public Class ListDetailCel
        Public detalles As List(Of detailCel) = New List(Of detailCel)()
    End Class

    Public Class detailCel
        Public FOLIO As String = String.Empty
        Public MOVIL As String = String.Empty
        Public RFC As String = String.Empty
        Public LIGA As String = String.Empty
    End Class

    Private Function CarruselTEL(ByVal folio As String) As List(Of Boolean)
        Dim respuesta As List(Of Boolean) = New List(Of Boolean)
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_Carrusel_Tel_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@folio", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Dim b1 As Boolean = False
            Dim b2 As Boolean = False
            Dim b3 As Boolean = False

            Do While reader.Read()
                If (reader(3) > 4) Then
                    If (reader(1) = 1) Then
                        b1 = True
                    ElseIf (reader(1) = 2) Then
                        b2 = True
                    Else
                        b3 = True
                    End If
                End If
            Loop
            respuesta.Add(b1)
            respuesta.Add(b2)
            respuesta.Add(b3)
        Catch ex As Exception
            respuesta.Add(False)
        End Try

        sqlConnection1.Close()
        Return respuesta
    End Function

    Private Function CarruselTEL_details(ByVal folio As String, ByVal tipo As Integer, ByVal intPantalla As String) As String
        Dim msg As ListDetailTel = New ListDetailTel()
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_Carrusel_Tel_Details_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Parameters.AddWithValue("@TYPE", tipo)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                Dim detailTel As detailTel = New detailTel()

                detailTel.T_TEL = reader("T_TEL")
                detailTel.FOLIO = reader("FOLIO")
                detailTel.TEL = reader("TEL")
                detailTel.TIPO_TEL = reader("TIPO_TEL")
                Dim strLiga As String
                strLiga = "ImprimirCreditoSolicitud.aspx?idPantalla=" + intPantalla + "&IdFolio=" + reader("FOLIO").ToString() + "&CVE=5"
                detailTel.LIGA = strLiga
                msg.detalles.Add(detailTel)
            Loop

        Catch ex As Exception
            msg.detalles.Add(New detailTel())
        End Try

        sqlConnection1.Close()

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Respuesta As String = serializer.Serialize(msg)

        Return json_Respuesta
    End Function

    Public Class ListDetailTel
        Public detalles As List(Of detailTel) = New List(Of detailTel)()
    End Class

    Public Class detailTel
        Public T_TEL As String = String.Empty
        Public FOLIO As String = String.Empty
        Public TEL As String = String.Empty
        Public TIPO_TEL As String = String.Empty
        Public LIGA As String = String.Empty
    End Class

    Private Function CarruselRFC(ByVal folio As String) As String
        Dim respuesta As Boolean = False
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_Carrusel_RFC_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
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

    Private Function CarruselRFC_details(ByVal folio As String, ByVal intPantalla As String) As String
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Dim msg As ListDetailRFC = New ListDetailRFC()

        Try
            cmd.CommandText = "get_Carrusel_RFC_Details_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                Dim detailRFC As detailRFC = New detailRFC()

                detailRFC.FOLIO = reader("FOLIO").ToString()
                detailRFC.RFC = reader("RFC").ToString()
                detailRFC.NOMBRE = reader("NOMBRE").ToString()
                Dim strLiga As String
                strLiga = "ImprimirCreditoSolicitud.aspx?idPantalla=" + intPantalla + "&IdFolio=" + reader("FOLIO").ToString() + "&CVE=5"
                detailRFC.LIGA = strLiga

                msg.detalles.Add(detailRFC)
            Loop
        Catch ex As Exception
            msg.detalles.Add(New detailRFC())
        End Try

        sqlConnection1.Close()

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Respuesta As String = serializer.Serialize(msg)

        Return json_Respuesta
    End Function

    Public Class ListDetailRFC
        Public detalles As List(Of detailRFC) = New List(Of detailRFC)()
    End Class

    Public Class detailRFC
        Public FOLIO As String = String.Empty
        Public RFC As String = String.Empty
        Public NOMBRE As String = String.Empty
        Public LIGA As String = String.Empty
    End Class

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
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                Response.Redirect("../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If

            'BD.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

            If muestrapant = 0 Then
                Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
            ElseIf muestrapant = 2 Then
                'Response.Redirect("../aspx/consultaPanelControl.aspx")
                Dim dc As New clsDatosCliente
                dc.idSolicitud = Val(Request("sol"))
                dc.getDatosSol()
                Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                str_ = ""

                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Exitosa.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                Return
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub

    Private Function Turned(ByVal folio As String, ByVal tarea As String) As String
        Dim respuesta As String = ""
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "set_TURNED_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Parameters.AddWithValue("@TURNED", tarea)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                respuesta = reader(0)
            Loop
        Catch ex As Exception
            respuesta = "FAIL"
        End Try

        sqlConnection1.Close()
        Return respuesta
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function valueTurned(ByVal folio As String, ByVal pantalla As String) As String
        Dim respuesta As String = "0"
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_TURNED_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                respuesta = reader(0)
            Loop
        Catch ex As Exception
            respuesta = "0"
        End Try

        If (respuesta <> "0") Then
            If (respuesta = "999") Then
                respuesta = 2
            Else
                Dim ds_siguienteTarea As DataSet
                Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
                objCatalogos.Parametro = pantalla
                ds_siguienteTarea = objCatalogos.Catalogos(6)

                Dim tarea_rechazo As String = ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO").ToString()
                Dim tarea_norechazo As String = ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO").ToString()

                If (tarea_norechazo = respuesta) Then
                    respuesta = "1"
                ElseIf (tarea_rechazo = respuesta) Then
                    respuesta = "3"
                Else
                    respuesta = "0"
                End If

            End If
        End If

        sqlConnection1.Close()
        Return respuesta
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function getDetails(ByVal id As String, ByVal sol As String, ByVal Pantalla As String) As String
        Dim respuesta As String = String.Empty
        'Dim intPantalla As Integer
        'intPantalla = CInt(Pantalla.ToString)

        Dim cls As aspx_CheckHB = New aspx_CheckHB()

        If (id = "c_cel_1") Then
            respuesta = cls.CarruselCEL_Details(sol, Pantalla)
        ElseIf (id = "c_tel_1") Then
            respuesta = cls.CarruselTEL_details(sol, 1, Pantalla)
        ElseIf (id = "c_tel_3") Then
            respuesta = cls.CarruselTEL_details(sol, 2, Pantalla)
        ElseIf (id = "c_tel_4") Then
            respuesta = cls.CarruselTEL_details(sol, 3, Pantalla)
        ElseIf (id = "c_rfc_1") Then
            respuesta = cls.CarruselRFC_details(sol, Pantalla)
        End If

        Return respuesta
    End Function
End Class