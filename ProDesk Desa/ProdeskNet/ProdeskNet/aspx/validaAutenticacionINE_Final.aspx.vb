#Region "Trackers"
'BUG-PD-211 GVARGAS 27/09/2017 Cambios mostrar info
'BUG-PD-221 GVARGAS 03/10/2017 Cambios validacion INE
'BBV-P-423 RQ-PD-17 5 GVARGAS 12/01/2018 Update when validation is OK
'BBV-P-423 RQ-PD-17 8 GVARGAS 12/01/2018 Update when validation is OK
'BBV-P-423 RQ-PD-17 10 GVARGAS 31/01/2018 Ajustes flujos
#End Region

Imports System.Data
Imports ProdeskNet.BD
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Data.SqlClient

Partial Class aspx_validaAutenticacionINE_Final
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim _mostrarPantalla As Integer = 0
    Dim _clsPantallas As New ProdeskNet.SN.clsPantallas()
    Dim _dtsResult As New DataSet
    Dim _clsManejaBD As New clsManejaBD
    Dim _dsResult As New DataSet
    Dim _clsCatTareas As New ProdeskNet.SN.clsCatTareas()
    Dim _clsSolicitudes As New clsSolicitudes(0)
#End Region

#Region "Properties"
    Public ReadOnly Property _idSolicitud() As Integer
        Get
            Return IIf(Not Request("sol") Is Nothing, Val(Request("Sol")), _
                                        IIf(Not Request("NoSolicitud") Is Nothing, Val(Request("Sol")), _
                                            IIf(Not Request("IdFolio") Is Nothing, Val(Request("Sol")), 0)))
        End Get
    End Property

    Public ReadOnly Property _idPantalla() As Integer
        Get
            Return IIf(Not Request("idPantalla") Is Nothing, Val(Request("idPantalla")), 146)
        End Get
    End Property

#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim enterINE As enterINE = autorizo_INE(Val(Request("Sol")))

        If (enterINE.autehntifyINE) Then
            NextTask()
        Else
            GoToFB()
        End If
    End Sub

    Private Function GetNextTask() As Boolean
        Dim result As Boolean = False

        _dtsResult = New DataSet()
        _clsManejaBD = New clsManejaBD()
        '_clsManejaBD.AgregaParametro("@ID_USUARIO", TipoDato.Entero, Convert.ToInt32(Session("IdUsua")), False)
        _clsManejaBD.AgregaParametro("@PDK_ID_SECCCERO", TipoDato.Entero, Convert.ToInt32(_idSolicitud), False)
        _dtsResult = _clsManejaBD.EjecutaStoredProcedure("get_BiometriaAgencia")

        If Not _dtsResult Is Nothing Then
            If _dtsResult.Tables.Count > 0 Then
                If _dtsResult.Tables(0).Rows.Count > 0 Then
                    result = Convert.ToBoolean(_dtsResult.Tables(0).Rows(0)("VALIDA_BIOMETRICO"))
                End If
            End If
        End If

        If result Then
            _dtsResult = New DataSet()
            _clsManejaBD = New clsManejaBD()
            _clsManejaBD.AgregaParametro("@PDK_ID_SECC_CERO", TipoDato.Entero, _idSolicitud, False)
            _clsManejaBD.AgregaParametro("@PDK_AUTHENTICATION_RESULT", TipoDato.Cadena, "AUTENTICADO", False)
            _dtsResult = _clsManejaBD.EjecutaStoredProcedure("get_HistoryAuthentication")

            result = False

            If Not _dtsResult Is Nothing Then
                If _dtsResult.Tables.Count > 0 Then
                    If _dtsResult.Tables(0).Rows.Count = 0 Then
                        result = True
                    End If
                End If
            End If
        End If
        Return result
    End Function

    Private Sub GoToFB()
        _clsSolicitudes = New clsSolicitudes(_idSolicitud)
        _clsSolicitudes.PDK_ID_SOLICITUD = _idSolicitud
        _clsSolicitudes.BOTON = 64
        _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
        _dtsResult = _clsManejaBD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & _idPantalla.ToString())
        If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO")
        Else
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = 0
        End If
        _clsSolicitudes.ValNegocio(1)
        checkRedirect()
    End Sub

    Private Sub NextTask()
        _clsSolicitudes = New clsSolicitudes(_idSolicitud)
        _clsSolicitudes.PDK_ID_SOLICITUD = _idSolicitud
        _clsSolicitudes.BOTON = 64
        _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")

        _dtsResult = _clsManejaBD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & _idPantalla.ToString())
        If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")
        Else
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = 0
        End If
        _clsSolicitudes.ValNegocio(1)
        checkRedirect()
    End Sub

    Protected Sub checkRedirect()
        _dtsResult = _clsCatTareas.SiguienteTarea(Val(Request("Sol")))
        _mostrarPantalla = _clsPantallas.SiguientePantalla(Val(Request("Sol")))
        Dim strLocation As String = String.Empty
        If _mostrarPantalla = 0 Then
            strLocation = ("../aspx/" & _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() & "?idPantalla=" & _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() & "&sol=" & _idSolicitud.ToString())
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('', '" + strLocation + "');", True)
        ElseIf _mostrarPantalla = 2 Then
            strLocation = ("../aspx/consultaPanelControl.aspx")
            Dim mensaje As String = "Tarea Exitosa"
            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '" + strLocation + "');", True)
        End If
    End Sub

    Private Function autorizo_INE(ByVal folio As String) As enterINE
        Dim autoriza As enterINE = New enterINE()

        autoriza.autehntifyINE = False

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_FB_Path_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                autoriza.autehntifyINE = reader("AVOID_FB").ToString()
            Loop
        Catch ex As Exception
            autoriza.autehntifyINE = False
        End Try

        sqlConnection1.Close()
        Return autoriza
    End Function

    Public Class enterINE
        Public autehntifyINE As Boolean
    End Class
End Class