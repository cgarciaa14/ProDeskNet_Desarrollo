'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BBVA-P-423 RQADM-12 GVARGAS 14/02/2017 Carrusel RFC
'BUG-PD-13  GVARGAS  28/02/2017 Mensaje cancela
'BUG-PD-24  GVARGAS  29/03/2017 Bugs Campos Desabilitados en Pre si la SOL se regresa
'BUG-PD-26  MAPH  12/04/2017 CAMBIOS SOLICITADOS POR GVARGAS
'BBVA-P-423 RQSOLBCOM-01 GVARGAS 16/04/2017 Corrige al cancelar la tarea
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
'BUG-PD-313: DJUAREZ: 27/12/2017: Se evita el avanzar tareas cuando se presiona F5 cuando esta cargando una pagina.
Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.BD
Imports ProdeskNet.SN
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.WCF

Partial Class aspx_CaidaLibre
    Inherits System.Web.UI.Page
    Dim BD As New clsManejaBD

    Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Private Sub aspx_CaidaLibre_Load(sender As Object, e As EventArgs) Handles Me.Load
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

        Dim hitCarrusel As Boolean = CarruselRFC(Request("Sol").ToString())

        If (hitCarrusel) Then
            CancelaTarea()

            Dim dc As New clsDatosCliente
            dc.idSolicitud = Val(Request("sol"))
            dc.getDatosSol()
            Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "PopUpLetreroRedirect('RECHAZO Solicitud no viable por políticas.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
            Return
        Else
            dsresult = DB.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

            Dim strLocation As String = String.Empty

            If muestrapant = 0 Then
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            ElseIf muestrapant = 2 Then
                'Response.Redirect("../aspx/consultaPanelControl.aspx")
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "PopUpLetreroRedirect('Tarea Exitosa.', '../aspx/consultaPanelControl.aspx');", True)
            End If
        End If
    End Sub

    Private Function CarruselRFC(ByVal folio As String) As String
        Dim respuesta As Boolean = False
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try

            cmd.CommandText = "get_HIT_BNRFC_SP"
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
