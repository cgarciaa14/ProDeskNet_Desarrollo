Imports ProdeskNet.Buro

Public Class consultaBuroCreditoINTL
    Inherits System.Web.UI.Page

    Dim intSolicitud As Integer = 0
    Dim intIdBuro As Integer = 0
    Dim intIdBuroReporte As Integer = 0
    Dim bolBuro As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Try

            If Not IsPostBack Then
                intSolicitud = Request.QueryString("IdSolicitud")
                intSolicitud = 23
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As System.EventArgs) Handles btnConsultar.Click

        Dim objBuro As New clsBuroINTL

        Try
            intSolicitud = Request.QueryString("IdSolicitud")
            intSolicitud = 23
            With objBuro
                .PDK_BURO_SOLICITUD = intSolicitud
                .PDK_BURO_PERSONA = 1
                .PDK_BURO_USUARIO = 1
            End With
            bolBuro = objBuro.PDK_BURO_OBTENBURO

        Catch ex As Exception

        End Try
    End Sub
End Class