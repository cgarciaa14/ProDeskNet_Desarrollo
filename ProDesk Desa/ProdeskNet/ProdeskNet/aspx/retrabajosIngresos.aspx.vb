#Region "trackers"
'BBV-P-423-RQADM-32:MPUESTO:20/04/2017:Retrabajos Ingresos 29 Creación de flujo alterno
'BUG-PD-49:MPUESTO:12/05/2017:Corrección Retrabajos Ingresos
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
#End Region

Imports System.Data
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports ProdeskNet.BD

Partial Class aspx_retrabajosIngresos
    Inherits System.Web.UI.Page
    Dim _clsPantallas As New ProdeskNet.SN.clsPantallas()
    Dim _dtsResult As New DataSet()
    Dim _clsCatTareas As New ProdeskNet.SN.clsCatTareas()
    Dim _clsSolicitudes As New clsSolicitudes(0)
    Dim _clsAlianzas As New clsAlianzas
    Dim _idPantalla As Integer = 102
    Dim _idSolicitud As Integer
    Dim _mostrarPantalla As Integer = 0
    Dim _clsManejaBD As New clsManejaBD()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            _idPantalla = Convert.ToInt32(Val(Request("idPantalla")).ToString())
            _idSolicitud = Convert.ToInt32(Request.QueryString("sol"))
            _dtsResult = _clsPantallas.CargaPantallas(_idPantalla)
            If _dtsResult.Tables.Count > 0 Then
                If _dtsResult.Tables(0).Rows.Count > 0 Then
                    _mostrarPantalla = Convert.ToInt32(_dtsResult.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR"))
                    If _mostrarPantalla = 0 Then
                        btnProcesar_Click(sender, e)
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs)
        _clsAlianzas.ObtenerAlianzas(idSolicitud:=_idSolicitud)
        _dtsResult = _clsManejaBD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & _idPantalla.ToString())
        If _clsAlianzas.Count > 0 Then
            _clsSolicitudes = New clsSolicitudes(_idSolicitud)
            _clsSolicitudes.PDK_ID_SOLICITUD = _idSolicitud
            _clsSolicitudes.BOTON = 64
            _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            If (_clsAlianzas(0).ES_MULTIMARCA And _clsAlianzas(0).MONTO_FINANCIAMIENTO > 300000.0) Or ((Not _clsAlianzas(0).ES_MULTIMARCA) And _clsAlianzas(0).MONTO_FINANCIAMIENTO > 350000.0) Then
                If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
                    _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")
                Else
                    _clsSolicitudes.PDK_ID_CAT_RESULTADO = 103
                End If
            Else
                If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
                    _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO")
                Else
                    _clsSolicitudes.PDK_ID_CAT_RESULTADO = 107
                End If
            End If
            _clsSolicitudes.ValNegocio(1)
        End If
        checkRedirect()
    End Sub

    Protected Sub checkRedirect()
        _dtsResult = _clsCatTareas.SiguienteTarea(Val(Request("Sol")))
        _mostrarPantalla = _clsPantallas.SiguientePantalla(Val(Request("Sol")))
        'ir a pantalla a la fuerza
        Dim strLocation As String = String.Empty

        If _mostrarPantalla = 0 Then
            strLocation = ("../aspx/" & _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
        ElseIf _mostrarPantalla = 2 Then
            strLocation = ("../aspx/consultaPanelControl.aspx")
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
        End If
    End Sub
End Class