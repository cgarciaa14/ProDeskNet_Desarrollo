'BBV-P-423:  AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BUG-PD-01: AMR: 22/12/2016: HTTP Error 404 - Not Found al procesar Precalificación.
'BUG-PD-13  GVARGAS  01/03/2017 Cambio WCF Credenciales
'BUG-PD-113  GVARGAS  21/06/2017 Modificacion servicio
'BUG-PD-110: AVEGA: 23/06/2017 CORRECCION MARREDONDO
'BUG-PD-150 GVARGAS 11/07/2017 Correcion Redirect

Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.Configurcion

Partial Class aspx_BusquedaCliente
    Inherits System.Web.UI.Page

    Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim strError As String = String.Empty

    Private Sub aspx_BusquedaCliente_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpant As New ProdeskNet.SN.clsPantallas()
            Dim dts As New DataSet()

            dts = objpant.CargaPantallas(Val(Request("idPantalla")).ToString)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Then
                        btnprocesar_Click(btnprocesar, Nothing)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnprocesar_Click(sender As Object, e As EventArgs) Handles btnprocesar.Click
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim objseccero As New ProdeskNet.SN.clsTabSeccionCero()
        Dim result As Integer = Nothing

        Try
            Dim ds_siguienteTarea As DataSet
            Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
            objCatalogos.Parametro = Request("idPantalla")
            ds_siguienteTarea = objCatalogos.Catalogos(6)

            Dim tarea_norechazo As String = ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO").ToString()
            Dim tarea_rechazo As String = ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO").ToString()

            Dim resultado As Integer = ConsultaWS()

            If resultado = 2 Then
                dsresult = DB.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

                dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

                muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

                Dim msg As String = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString()

                If muestrapant = 0 Then
                    'Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrePopUpOk", "PopUpLetreroRedirect('" & msg & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString & "');", True)
                ElseIf muestrapant = 2 Then
                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + msg + "', '../aspx/consultaPanelControl.aspx');", True)
                End If
            ElseIf (resultado = 1) Then
                CancelaTarea()
                strError = "RECHAZO Solicitud no viable por políticas."
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + strError + "', '../aspx/consultaPanelControl.aspx');", True)
            Else
                strError = "Error al procesar la tarea: "
                asignaTarea(Int32.Parse(tarea_rechazo))
                Master.MsjErrorRedirect(strError, "../aspx/consultaPanelControl.aspx")
            End If
        Catch ex As Exception
            Dim ds_siguienteTarea As DataSet
            Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
            objCatalogos.Parametro = Request("idPantalla")
            ds_siguienteTarea = objCatalogos.Catalogos(6)

            Dim tarea_norechazo As String = ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO").ToString()
            Dim tarea_rechazo As String = ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO").ToString()

            strError = "Error al procesar la tarea: "
            asignaTarea(Int32.Parse(tarea_rechazo))
            Master.MsjErrorRedirect(strError + ex.Message.ToString(), "../aspx/consultaPanelControl.aspx")
        End Try
    End Sub

    Private Function ConsultaWS() As Integer
        Dim resultado As Integer = 0

        Dim objCteIndeseable As ProdeskNet.SN.clsCteIndeseable = New ProdeskNet.SN.clsCteIndeseable
        Dim strEsIndeseable As String = String.Empty

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        Dim queryString As StringBuilder = New StringBuilder()

        queryString.Append(System.Configuration.ConfigurationManager.AppSettings.Item("Customer").ToString())
        queryString.Append("?$filter=(clientNumber==" + getClientBBVA(Val(Request("Sol"))))
        queryString.Append(")&&$fields=customerInformationResult(customerIndicator)")

        restGT.Uri = queryString.ToString()

        restGT.buscarHeader("ResponseWarningDescription")

        Dim respuestaBC As String = restGT.ConnectionGet(userID, iv_ticket1, String.Empty)

        Dim serializerBC As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jresultBC = serializerBC.Deserialize(Of Customer)(respuestaBC)

        Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim alert = srrSerialer.Deserialize(Of msjerr)(respuestaBC)
        Dim Header As String = restGT.valorHeader

        If Not restGT.IsError Then
            If restGT.StatusHTTP = "200" Then
                If (jresultBC.customerInformationResult IsNot Nothing) Then
                    strEsIndeseable = objCteIndeseable.CatalogoCteIndeseable(jresultBC.customerInformationResult.customerIndicator.ToString())
                    If objCteIndeseable.StrError = "" Then
                        If strEsIndeseable = jresultBC.customerInformationResult.customerIndicator.ToString() Then
                            resultado = 1
                        Else
                            resultado = 2
                        End If
                    End If
                End If
            End If
        Else
            resultado = 0
        End If

        Return resultado
    End Function

    Public Class Customer
        Public customerInformationResult As customerInformationResult
    End Class

    Public Class Person
        Public id As String
        Public segment As segment
    End Class
    Public Class segment
        Public name As name
    End Class

    Public Class name
        Public id As String
    End Class

    Public Class msjerr
        Public message As String
        Public status As String
    End Class

    Public Class customerInformationResult
        Public customerIndicator As String
    End Class

    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Try

            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                Response.Redirect("../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If
        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub

    Public Sub CancelaTarea()
        Dim objCancela As New clsSolicitudes(0)
        objCancela.PDK_ID_SOLICITUD = Request("Sol").ToString()
        objCancela.Estatus = 42
        objCancela.PDK_CLAVE_USUARIO = Session("IdUsua")
        objCancela.PDK_ID_PANTALLA = Request("idPantalla").ToString()
        objCancela.Estatus_Cred = 295

        objCancela.ManejaTarea(6)
    End Sub

    Private Function getClientBBVA(ByVal folio As String) As String
        Dim cliente_BBVA As String = String.Empty

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_Cliente_BBVA"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                cliente_BBVA = reader(1)
            Loop
        Catch ex As Exception
            cliente_BBVA = String.Empty
        End Try

        sqlConnection1.Close()

        Return cliente_BBVA
    End Function
End Class
