Imports System
Imports System.Web
Imports Microsoft.AspNet.SignalR

Namespace SignalRChat
    Public Class ChatHub
        Inherits Hub
        Public Sub send(name As String, message As String)
            Clients.All.broadcastMessage(name, message)
        End Sub
    End Class
End Namespace

