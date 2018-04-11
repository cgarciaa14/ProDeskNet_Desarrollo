Imports System.Data
Public Class SignalR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim ds As New DataSet
        ds = BD.EjecutarQuery("select pdk_usu_nombre from pdk_usuario where pdk_id_usuario = " & Session("IdUsua"))

        hdUsuario.Value = ds.Tables(0).Rows(0)(0)
    End Sub

End Class