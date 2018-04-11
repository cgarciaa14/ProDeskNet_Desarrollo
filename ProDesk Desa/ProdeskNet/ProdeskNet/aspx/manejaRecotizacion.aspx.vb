'RQ-PI7-PD9: CGARCIA: 15/11/2017: CREACION DE LA RECOTIZACION
Imports ProdeskNet.WCF
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlInputFile
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.SN
Imports ProdeskNet.BD
Imports System.Collections
Imports System

Partial Class aspx_manejaRecotizacion
    Inherits System.Web.UI.Page

#Region "Variables"
    Public BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Public strError As String = String.Empty
    Public Pantalla As Integer
    Public solicitud As Integer
    Public dsDatos As DataSet
    Public ds_siguienteTarea As DataSet
    Public ds_anteriorTarea As DataSet
    Public valorTarea As Integer
    Public clsRec As New clsRecotizacion()
#End Region

    Protected Sub aspx_manejaRecotizacion_Load(sender As Object, e As EventArgs) Handles Me.Load
        Pantalla = Convert.ToInt32(Val(Request("idPantalla")).ToString())
        solicitud = CInt(Val(Request("sol")))

        clsRec.OPCION = 1
        clsRec.ID_SOLICITUD = solicitud
        clsRec.PANTALLA = Pantalla

        dsDatos = clsRec.getRecotiza(1)

        If (Not IsNothing(dsDatos) AndAlso dsDatos.Tables.Count > 0 AndAlso dsDatos.Tables(0).Rows.Count() > 0 AndAlso
            dsDatos.Tables(1).Rows.Count > 0) Then
            ds_siguienteTarea = clsRec.ConsultaTarea
            If (Not IsNothing(ds_siguienteTarea) AndAlso ds_siguienteTarea.Tables.Count > 0 AndAlso ds_siguienteTarea.Tables(0).Rows.Count() > 0) Then
                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
            Else
                Master.MensajeError("Error al consultar los datos.")
                Exit Sub
            End If
        Else
            Master.MensajeError("No es posible hacer la re-cotización")
            Exit Sub
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
            Solicitudes.PDK_ID_SOLICITUD = solicitud
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
