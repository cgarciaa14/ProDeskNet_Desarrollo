'RQ-PD-26 JMENDIETA: 26/02/2018 Se agrega pantalla automatica que redirigira automaticamente a la siguiente tarea. 
Imports System.Data

Partial Class aspx_DespliegaPantalla_1
    Inherits System.Web.UI.Page

    Protected Sub aspx_DespliegaPantalla_1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim dslink As DataSet = New DataSet()
        Dim dsresult As DataSet = New DataSet()
        Dim strLocation As String = String.Empty

        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Solicitudes.BOTON = 64
        Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
        Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
        Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")


        dsresult = Solicitudes.ValNegocio(1)

        dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

        strLocation = String.Format("{0}?idPantalla={1}&pantalla={1}&idFolio={2}&sol={2}&NoSol={2}&solicitud={2}&Enable=0&usuario={3}&usu={3}", dslink.Tables(0).Rows(0).Item("PDK_PANT_MIRROR_LINK").ToString, dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString, Val(Request("Sol")).ToString, Val(Request("usuario")).ToString)

        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)

    End Sub
End Class
