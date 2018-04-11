
Partial Class aspx_MasterPageVacia
    Inherits System.Web.UI.MasterPage

    Public Sub MensajeError(ByVal strError As String)
        If Trim(strError) <> "" Then
            Master.MensajeError(strError)
        End If
    End Sub

End Class

