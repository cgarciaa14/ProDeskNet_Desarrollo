Imports ProdeskNet.Seguridad
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Web.UI.WebControls
Imports ProdeskNet.Buro
Imports System.Data
Imports System.Data.SqlClient
'<%--BBV-P-423: RQSOL-04: AVH: 06/12/2016 FORZAJES--%>
Partial Class aspx_manejaTareas
    Inherits System.Web.UI.Page
    Dim BD As New ProdeskNet.BD.clsManejaBD

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"
        'Response.Redirect("http://www.microsoft.com")
        Dim ds As New DataSet
        Dim permisos As New DataSet
        Dim tmp As New DataSet

        Try

            If Request("idFolio") > 0 Then

                permisos = BD.EjecutarQuery("SELECT * FROM PDK_REL_USU_PER WHERE PDK_ID_PERFIL IN (SELECT PDK_ID_PERFIL FROM PDK_PERMISO_CARATULA WHERE PDK_FI_ID_PERMISO_CARATULA_PESTANA = 6) AND PDK_CLAVE_USUARIO = " & Session("IdUsua"))
                If permisos.Tables(0).Rows.Count > 0 Then

                    hdnFolio.Value = Request("idFolio")
                    hdnUsuario.Value = Session("IdUsua")

                    ds = BD.EjecutarQuery("EXEC sp_fillTareas " & Request("idFolio") & "," & Session("IdUsua"))
                    pantalla.InnerHtml = ds.Tables(0).Rows(0).Item(0).ToString

                    tmp = BD.EjecutarQuery("SELECT TOP 1 PDK_ID_TAREAS FROM PDK_OPE_SOLICITUD WHERE PDK_ID_SOLICITUD = " & Request("idFolio") & " ORDER BY PDK_ID_OPE_SOLICITUD DESC")
                    tareaActual.Value = tmp.Tables(0).Rows(0).Item(0).ToString

                Else
                    debug.InnerHtml = "<h1>No tienes permisos suficientes</h1>"
                End If

            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)

        End Try

    End Sub

End Class
