Imports System.Data
Imports ProdeskNet.SN

'BBV-P-423 RQADM-30 MAPH 17/04/2017 Pantalla Referenciados SEVA 60, asignación automática de folios SEVA a las solicitudes correspondientes
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
Partial Class aspx_asignacionSEVA
    Inherits System.Web.UI.Page

    Dim _clsPantallas As New ProdeskNet.SN.clsPantallas()
    Dim _dtsResult As New DataSet()
    Dim _solicCounter As Integer
    Dim _clsCatTareas As New ProdeskNet.SN.clsCatTareas()
    Dim _clsSEVASolicitudes As New clsSEVASolicitudes()
    Dim _muestraPantalla As Integer = 0

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            _dtsResult = _clsPantallas.CargaPantallas(Val(Request("idPantalla")).ToString())
            If _dtsResult.Tables.Count > 0 Then
                If _dtsResult.Tables(0).Rows.Count > 0 Then
                    _muestraPantalla = Convert.ToInt32(_dtsResult.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR"))
                    If _muestraPantalla = 0 Then
                        btnprocesar_Click(sender, e)
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub btnprocesar_Click(sender As Object, e As EventArgs)
        Dim PDK_ID_SECC_CERO As String = IIf(Not Request("sol") Is Nothing, Val(Request("Sol")).ToString(), _
                                      IIf(Not Request("NoSolicitud") Is Nothing, Val(Request("Sol")).ToString(), _
                                          IIf(Not Request("IdFolio") Is Nothing, Val(Request("Sol")).ToString(), "0")))

        _solicCounter = _clsSEVASolicitudes.AsignaSolicitudSEVA(Val(PDK_ID_SECC_CERO))
        _dtsResult = _clsCatTareas.SiguienteTarea(Convert.ToInt32(PDK_ID_SECC_CERO))
        _muestraPantalla = _clsPantallas.SiguientePantalla(PDK_ID_SECC_CERO)

        Dim strLocation As String = String.Empty

        If _muestraPantalla = 0 Then
            strLocation = ("../aspx/" & _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "PopUpLetreroRedirect('Tarea Exitosa.', '../aspx/consultaPanelControl.aspx');", True)
        End If
    End Sub

End Class
