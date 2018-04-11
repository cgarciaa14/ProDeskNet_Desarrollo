Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Seguridad

Public Class consultaTareaPerfil
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"
        Dim intPerfil As Integer = 0
        Dim intEmpresa As Integer = 0
        Dim intProducto As Integer = 0
        Try
            If Not IsPostBack Then
                intPerfil = Request.QueryString("idPerfil")
                intEmpresa = Request.QueryString("idEmpresa")
                intProducto = Request.QueryString("idProducto")

            End If

        Catch ex As Exception
        End Try

    End Sub

End Class