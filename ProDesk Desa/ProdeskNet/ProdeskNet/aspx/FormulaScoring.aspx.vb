Public Class FormulaScoring
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hdnIdRegistro.Value = Session("IdUsua")
        End If
    End Sub

End Class