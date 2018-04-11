'BUG-PD-48 JBEJAR 12/05/2017 TAREA AUTOMATICA VALIDA TIPO DE IDENTIFICACION   
'BUG-PD-50 JBEJAR  18/05/2017 CORRECIONES PARA VAIDA MESA ESPECIALES. 
'BUG-PD-64 JBEJAR 26/05/2017 CORRECIONES POR ACTUALIZACION DE CATALOGOS. 
'BUG-PD-159 24/07/2017 SE CAMBIA RESPONSE.
Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.Configurcion
Partial Class aspx_VALIDATI
    Inherits System.Web.UI.Page
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim strError As String = String.Empty
    Public Pantalla As Integer
    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpant As New ProdeskNet.SN.clsPantallas()
            Dim dts As New DataSet()
            Dim Bandera As Integer = 0
            Dim dsParametros As New DataSet
            Dim objTarea As New clsSolicitudes(0)
            Dim ValTareas As Integer = 0
            Dim Solicitud As Integer = 0
            Dim Tipo As String = String.Empty
            Dim objFlujos As New clsSolicitudes(0)
            Dim ds_siguienteTarea As DataSet

            Pantalla = Request("idPantalla")
            Solicitud = Request("Sol")
            objTarea.PDK_ID_SOLICITUD = IIf(Not Request("sol") Is Nothing, Val(Request("Sol")).ToString(), _
                                      IIf(Not Request("NoSolicitud") Is Nothing, Val(Request("Sol")).ToString(), _
                                          IIf(Not Request("IdFolio") Is Nothing, Val(Request("Sol")).ToString(), _
                                              IIf(Not Request("Solicitud") Is Nothing, Val(Request("Sol")).ToString(), "0"))))
            objTarea.PDK_ID_PANTALLA = Request("idPantalla")


            ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & Pantalla)

            'CONSULTA PARAMETROS VERIFICAR  QUE EXISTA EL DOCUMENTO 
            dsParametros = BD.EjecutarQuery("SELECT TIPO_IDENTIFICACION  FROM PDK_TAB_DATOS_PERSONALES   WHERE PDK_ID_SECCCERO=" & Solicitud)

            If dsParametros.Tables.Count > 0 Then
                If dsParametros.Tables(0).Rows.Count > 0 Then
                    Tipo = dsParametros.Tables(0).Rows(0).Item("TIPO_IDENTIFICACION").ToString()

                    If Tipo.ToUpper.Contains("CRED") And Tipo.ToUpper.Contains("ELEC") Then
                        Bandera = 1
                    End If
                    If Tipo.ToUpper.Contains("CED") And Tipo.ToUpper.Contains("PROFESIONAL") Then
                        Bandera = 2
                    End If
                    If Not ((Tipo.ToUpper.Contains("CRED") And Tipo.ToUpper.Contains("ELEC")) Or (Tipo.ToUpper.Contains("CED") And Tipo.ToUpper.Contains("PROFESIONAL"))) Then
                        Bandera = 3
                    End If


                End If
            End If

            dts = objpant.CargaPantallas(Pantalla)


            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Or (Bandera = 1 Or Bandera = 2 Or Bandera = 3) Then

                        If Bandera = 1 Then

                            asignaTarea(101)
                        End If

                        If Bandera = 2 Then

                            asignaTarea(94)

                        End If
                        If Bandera = 3 Then

                            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))


                        End If
                    End If

                End If
            End If
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
                'Dim redirect As String = "../aspx/consultaPanelControl.aspx"
                'Dim script As String = "PopUpLetreroRedirect('" + mensaje + "','" + redirect + "');"
                'ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", script, True)
                Response.Redirect("../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If

            'BD.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

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

