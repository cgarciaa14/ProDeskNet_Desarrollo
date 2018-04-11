'BBV-P-423 RQ-PD-17 3 GVARGAS 08/01/2018 Mejoras carga huella y cambio payload
'BUG-PD-380 GVARGAS 02/03/2018 Correccion urgente instalacion biometrico

Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.Catalogos
Imports ProdeskNet.WCF
Imports ProdeskNet.BD
Imports ProdeskNet.Configurcion

Partial Class aspx_prospectus
    Inherits System.Web.UI.Page

    Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim strError As String = String.Empty

    Private Sub aspx_Prospector_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ' 'Validacion de Request
            ' Dim Validate As New clsValidateData
            ' Dim Url As String = Validate.ValidateRequest(Request)

            ' If Url <> String.Empty Then
                ' ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", Url, True)
                ' Exit Sub
            ' End If
            ' 'Fin validacion de Request

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
        Dim objCancela As New clsSolicitudes(0)
        objCancela.PDK_ID_SOLICITUD = Request("sol").ToString()
        objCancela.Estatus = 42
        objCancela.PDK_CLAVE_USUARIO = Session("IdUsua")

        objCancela.ManejaTarea(3)

        Dim msg As String = "RECHAZO Solicitud no viable por políticas."
        Dim path As String = "../aspx/consultaPanelControl.aspx"
        ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + msg + "', '" + path + "');", True)
    End Sub
End Class
