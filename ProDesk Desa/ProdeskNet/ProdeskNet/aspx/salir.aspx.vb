Public Class aspx_salir
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FormsAuthentication.SignOut()
        Session("cveAcceso") = Nothing
        Response.Redirect("~/Login.aspx")
    End Sub
End Class