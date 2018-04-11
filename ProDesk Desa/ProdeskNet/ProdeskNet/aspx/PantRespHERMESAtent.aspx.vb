Imports ProdeskNet.SN
Imports System.Data
Imports System.Data.SqlClient

'BUG-PD-145: RHERNANDEZ: 10/07/17 SE CREA TAREA AUTOMATICA PARA CONSULTA SI LA RESPUESTA DE SCORING ES NECESARIA LA AUTENTICACION VIA CUESTIONARIO
'BUG-PD-158: RHERNANDEZ: 17/07/17: SE CAMBIAN RESPONSE REDIRECT A SCRIPTMANAGER
'BUG-PD-332: JBEJAR: 11/01/2018: SE IMPLEMENTA LOG DE TAREAS AUTOMATICAS.       
'BBV-P-423 RQ-PD-17 12 GVARGAS 06/02/2018 Ajustes flujo 3
'BUG-PD-388: DJUAREZ 09/03/2018 Se realiza merge
Partial Class aspx_PantRespHERMESAtent
    Inherits System.Web.UI.Page
    Dim StrErr As String = String.Empty
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim ds_tarea_log As DataSet
    Dim _Parametro As Integer
    Dim solicitud As Integer = 0
    Dim _mensaje_ingreso As String = "Inicio_Tarea"
    Dim objespeciales As New clsEspeciales()
    Public Pantalla As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim clssol As New clsTabDatosSolicitante
        Dim res As DataSet
        Pantalla = Request("idPantalla")
        Dim _sol As Integer = Request("sol")
        Dim ds_siguienteTarea As DataSet
        ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & Request("idPantalla"))

        res = clssol.CargaRespuestaScoring(Request("sol"))
        If clssol.StrError <> "" Then
            Master.MensajeError(clssol.StrError)
            Exit Sub
        End If
        Dim MsgScoring As String
        MsgScoring = res.Tables(0).Rows(0).Item("finalDictum").ToString

        Dim dts As DataSet
        Dim objpant As New ProdeskNet.SN.clsPantallas()
        dts = objpant.CargaPantallas(Request("idPantalla"))

        ds_tarea_log = objespeciales.ConsultaParametro()

        If (Not IsNothing(ds_tarea_log) AndAlso ds_tarea_log.Tables.Count > 0 AndAlso ds_tarea_log.Tables(0).Rows.Count() > 0) Then 'BUG-PD-332

            _Parametro = ds_tarea_log.Tables(0).Rows(0).Item("PARAMETRO")

            If _Parametro = 2 Then
                objespeciales.Tarea_Actual = Pantalla
                objespeciales.ID_SOLICITUD() = _sol
                objespeciales.Mensaje() = _mensaje_ingreso
                objespeciales.InsertLog()

            End If

        End If 'BUG-PD-332


        If dts.Tables.Count > 0 Then
            If dts.Tables(0).Rows.Count > 0 Then
                If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Then
                    If MsgScoring = "ACEPTADO CON AUTENTIFICACION" Then
                        Dim query As StringBuilder = New StringBuilder()
                        query.Append("EXEC crud_Biometrico_SP " + _sol.ToString() + ", " + Pantalla.ToString() + ", 13, ''")
                        Dim tarea As Integer = Int32.Parse(executeQuerys(query))

                        If tarea <> 0 Then
                            asignaTarea(tarea)
                        Else
                            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
                        End If
                    Else
                        asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))
                    End If
                End If
            End If
        End If

    End Sub

    Private Function executeQuerys(ByVal query As StringBuilder) As String
        Dim respuestaQuery As String = String.Empty

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "exec_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@query", query.ToString())

            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()
            Do While reader.Read()
                respuestaQuery = reader(0).ToString()
            Loop
        Catch ex As Exception
        End Try
        sqlConnection1.Close()

        Return respuestaQuery
    End Function

    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim _sol As Integer = Request("sol")
        Dim mensaje As String = String.Empty
        Try

            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Request("Sol")
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            Solicitudes.PDK_ID_PANTALLA = Pantalla
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '../aspx/consultaPanelControl.aspx');", True)
                Exit Sub
            End If



            dslink = objtarea.SiguienteTarea(Solicitudes.PDK_ID_SOLICITUD)

            muestrapant = objpantalla.SiguientePantalla(Solicitudes.PDK_ID_SOLICITUD)
            Dim strLocation As String = String.Empty

            If (Not IsNothing(ds_tarea_log) AndAlso ds_tarea_log.Tables.Count > 0 AndAlso ds_tarea_log.Tables(0).Rows.Count() > 0) Then 'BUG-PD-332

                _Parametro = ds_tarea_log.Tables(0).Rows(0).Item("PARAMETRO")

                If _Parametro = 2 Then
                    objespeciales.Tarea_Actual = Pantalla
                    objespeciales.ID_SOLICITUD = _sol
                    objespeciales.Mensaje = mensaje
                    objespeciales.Tarea_siguiente = idAsignarPantalla
                    objespeciales.InsertLog()

                End If

            End If 'BUG-PD-332

            If muestrapant = 0 Then
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString) 'BUG-PD-125
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
