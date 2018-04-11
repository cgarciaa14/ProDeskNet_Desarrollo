Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.Configurcion
'BUG-PD-312: DCORNEJO: 26/02/18: SE CREA PANTALLA DESPLIEGUE TAREA PARA SALTAR A OTRA TAREA SIN PASAR POR EL PANEL
Partial Class aspx_DespliegaPantalla3
    Inherits System.Web.UI.Page
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim strError As String = String.Empty

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Dim strLocat As String = String.Empty
        Try

            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")

            dsresult = Solicitudes.ValNegocio(1)

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            Dim strLocation As String = String.Empty

            If muestrapant = 0 Then
                strLocation = String.Format("{0}?idPantalla={1}&pantalla={1}&idFolio={2}&sol={2}&NoSol={2}&solicitud={2}&Enable=0&usuario={3}&usu={3}", dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString, dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString, Val(Request("Sol")).ToString, Val(Request("usuario")).ToString)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
            Throw New Exception(ex.Message)
        End Try
    End Sub

End Class
