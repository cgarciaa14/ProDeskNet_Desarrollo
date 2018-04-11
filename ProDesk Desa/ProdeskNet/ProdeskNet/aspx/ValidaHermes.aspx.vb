'BUG-PD-103 JBEJAR 17/06/2017  TAREA AUTOMATICA VALIDA RESPUESTA HERMES. 
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
Imports ProdeskNet.WCF
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlInputFile
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.SN
Imports ProdeskNet.BD

Partial Class aspx_ValidaHermes
    Inherits System.Web.UI.Page
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim strError As String = String.Empty
    Public Pantalla As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim objpant As New ProdeskNet.SN.clsPantallas()
        Dim ds_siguienteTarea As DataSet
        Dim ds_Hermes As DataSet
        Dim solicitud As Integer
        Dim Dictamen As String = String.Empty
        Dim Idpolicy As String = String.Empty

        solicitud = Request("Sol") 'Recuperamos el id de la solicitud de la url 
        Pantalla = Request("IdPantalla") 'Recuperamos el id de la pantalla de la url 
        ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & Pantalla.ToString()) 'SIGUIENTE TAREA 
        ds_Hermes = BD.EjecutarQuery("SELECT finalDictum AS FINAL , rejectionPolicy AS IDPOLICY  FROM PDK_TAB_HERMES_RESPONSE WHERE PDK_ID_SECCCERO =" & solicitud.ToString()) 'OBTENEMOS LOS DATOS A EVALUAR DE LA TABLA DE HERMES. 
        If ds_Hermes.Tables(0).Rows.Count > 0 Then  'Validamos 
            Dictamen = ds_Hermes.Tables(0).Rows(0).Item("FINAL") 'Valor del dictame
            Idpolicy = ds_Hermes.Tables(0).Rows(0).Item("IDPOLICY") 'Valor del idpolicy 
        End If
        If Dictamen.Contains("RECHAZO POR POLITICA") And Idpolicy.Contains("8") Then 'BUG-PD-103 SE VALIDA  LAS CONDICIONES PARA ENTRAR  A LA TAREA RECHAZO.   
            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))
        Else

            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")) ' EN CASO CONTRARIO VAMOS A LA TAREA NO RECHAZO ESTAS VIENEN DE LA CONFIGURACION DEL FLUJO

        End If
    End Sub

    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Try
            'SE LLENAN LOS PARAMETROS PARA EJECUTAR METODOS DE LA CLASE clsSolicitudes
            Solicitudes.BOTON = 64                                  'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_SOLICITUD = Request("Sol")      'PARAMETRO PARA ValNegocio() Y ManejaTarea()
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")          'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_PANTALLA = Pantalla    'PARAMETRO PARA ManejaTarea()
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla    'PARAMETRO PARA ManejaTarea()

            dsresult = Solicitudes.ValNegocio(1)                    'SE ADELANTA LA TAREA ESPECIFICADA(RECHAZO O NORECHAZO) Y OBTENEMOS MENSAJE
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje  ' SE ASIGNA MENSAJE COMO PARAMETRO PARA ManejaTarea()
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                Response.Redirect("../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If

            dslink = objtarea.SiguienteTarea(Solicitudes.PDK_ID_SOLICITUD)

            muestrapant = objpantalla.SiguientePantalla(Solicitudes.PDK_ID_SOLICITUD)

            Dim strLocation As String = String.Empty

            If muestrapant = 0 Then
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            ElseIf muestrapant = 2 Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub
End Class
