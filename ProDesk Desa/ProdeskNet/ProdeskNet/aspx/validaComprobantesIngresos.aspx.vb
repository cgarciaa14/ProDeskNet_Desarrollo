#Region "trackers"
'BUG-PD-109 MPUESTO 20/06/2017 TAREA AUTOMATICA DE VALIDACION DE INGRESOS COMPROBABLES VS INFORMADOS
'BUG-PD-117:MPUESTO:26/06/2017:CORRECCIONES DE TAREA AUTOMÁTICA.
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
'BUG-PD-247 JBEJAR 27/10/2017 SE CAMBIA RESPONSE.
'BUG-PD-396 GVARGAS 13/03/2018 URL add IdUsua
#End Region

Imports System.Data
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports ProdeskNet.BD

Partial Class aspx_validaComprobantesIngresos
    Inherits System.Web.UI.Page
    Dim _clsPantallas As New ProdeskNet.SN.clsPantallas()
    Dim _dtsResult As New DataSet()
    Dim _dtsIngresos As New DataSet()
    Dim _clsCatTareas As New ProdeskNet.SN.clsCatTareas()
    Dim _clsSolicitudes As New clsSolicitudes(0)
    Dim _idPantalla As Integer 'PONER UN ID POR DEFECTO
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
        Dim _rechazo As Boolean = False
        _clsManejaBD = New clsManejaBD()
        _clsManejaBD.AgregaParametro("@PDK_ID_SECC_CERO", TipoDato.Entero, _idSolicitud, False)
        _dtsIngresos = _clsManejaBD.EjecutaStoredProcedure("get_IngresosValidacion")

        _dtsResult = _clsManejaBD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & _idPantalla.ToString())
        _clsSolicitudes = New clsSolicitudes(_idSolicitud)
        _clsSolicitudes.PDK_ID_SOLICITUD = _idSolicitud
        _clsSolicitudes.BOTON = 64
        _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
        If Not _dtsIngresos Is Nothing And _dtsIngresos.Tables.Count > 1 And _dtsIngresos.Tables(0).Rows.Count > 0 And _dtsIngresos.Tables(1).Rows.Count > 0 _
        And Convert.ToDecimal(_dtsIngresos.Tables("RESULTADO").Rows(0)("SUMATORIA_INGRESOS_COMP")) >= Convert.ToDecimal(_dtsIngresos.Tables("RESULTADO1").Rows(0)("SUMATORIA_INGRESOS_DECL")) Then
            'RESULTADO1  INGRESOS_COMPROBABLES
            'RESULTADO   INGRESOS_DECLARADOS
            If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
                _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")
            Else
                _clsSolicitudes.PDK_ID_CAT_RESULTADO = 107
            End If
        Else
            If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
                _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO")
            Else
                _clsSolicitudes.PDK_ID_CAT_RESULTADO = 97
            End If
        End If
        _clsSolicitudes.ValNegocio(1)
        checkRedirect()
    End Sub

    Protected Sub checkRedirect(Optional ByVal mostrarPantalla As Integer = -1)
        _dtsResult = _clsCatTareas.SiguienteTarea(Val(Request("Sol")))
        _mostrarPantalla = IIf(mostrarPantalla = -1, _clsPantallas.SiguientePantalla(Val(Request("Sol"))), mostrarPantalla)
        'ir a pantalla a la fuerza

        Dim strLocation As String = String.Empty

        If _mostrarPantalla = 0 Then
            strLocation = ("../aspx/" & _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() & "?idPantalla=" & _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() & "&sol=" & _idSolicitud.ToString() & "&usuario=" & Session("IdUsua").ToString())
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "PopUpLetreroRedirect('', '" + strLocation + "');", True)
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            'Response.Redirect(strLocation, False)
        ElseIf _mostrarPantalla = 2 Then
            strLocation = "../aspx/consultaPanelControl.aspx"
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "PopUpLetreroRedirect('', '" + strLocation + "');", True)
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            'Response.Redirect(strLocation, False)
        End If

    End Sub
End Class