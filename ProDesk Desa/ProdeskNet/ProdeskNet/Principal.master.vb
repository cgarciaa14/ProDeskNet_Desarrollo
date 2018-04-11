Imports System.Data
'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)

Public Class Principal
    Inherits System.Web.UI.MasterPage

    Public Sub MensajeError(ByVal strError As String)
        If Trim(strError) <> "" Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetrero('" & strError.Replace("'", """") & "');", True)
        End If
    End Sub

    Public Sub MensajeErrorLogin(ByVal strError As String)
        If Trim(strError) <> "" Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroLogin('" & strError & "')", True)
        End If
    End Sub

    Public Sub Script(ByVal strScript As String)
        If Trim(strScript) <> "" Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, GetType(String), "RegScript", "<script>" & strScript & "</script>", True)
        End If

    End Sub
    Public Sub RegistraControl(ByVal Ctrl As Control)
        ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(Ctrl)
    End Sub

    Public Sub EjecutaJS(ByVal script As String)
        If Len(script) > 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "EjecutaJava", script, True)
        End If
    End Sub

    Public Sub MsjErrorRedirect(ByVal strError As String, ByVal strURL As String)
        If Trim(strError) <> "" Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & strError.Replace("'", """") & "', '" & strURL.Replace("'", """") & "');", True)
        End If
    End Sub
End Class