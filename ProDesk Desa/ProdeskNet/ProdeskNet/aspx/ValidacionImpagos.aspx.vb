'BBVA-P-423 RQ-INB226 GVARGAS 04/07/2017 Validación de Impagos (RV02)
'BUG-PD-211 GVARGAS 02/10/2017 Cambios mostrar info
'BBV-P-423 RQ-PI-7-PD-14 GVARGAS 22/11/2017 Detalle Impagos
'BUG-PD-298 GVARGAS 08/12/2017 Cambios NO mostrar info

Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml.Serialization.XmlSerializer
Imports ProdeskNet.BD
Imports ProdeskNet.SN
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos

Partial Class aspx_ValidacionImpagos
    Inherits System.Web.UI.Page

    Dim BD As New clsManejaBD
    Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()

    Private Sub aspx_CarruselCelulares_Load(sender As Object, e As EventArgs) Handles Me.Load
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

        Dim Impagos As ImpagosDetails = GetImpagos(Request("Sol").ToString())

        If (Impagos.Impago) Then
            dsresult = DB.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

            Dim mensaje As String = "Tarea Exitosa"

            Dim ds_siguienteTarea As DataSet
            Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
            objCatalogos.Parametro = Request("idPantalla")
            ds_siguienteTarea = objCatalogos.Catalogos(6)

            Dim tarea_norechazo As String = ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO").ToString()

            If muestrapant = 0 Then
                Dim _str As String = "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() & "&sol=" & Val(Request("Sol")).ToString() & "&usuario=" & Val(Request("usuario")).ToString()
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Exitosa.', '" + _str + "');", True)
            ElseIf muestrapant = 2 Then

                Dim dc As New clsDatosCliente
                dc.idSolicitud = Val(Request("sol"))
                dc.getDatosSol()
                Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                str_ = ""

                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Exitosa.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
            End If
        Else

            Dim ds_siguienteTarea As DataSet
            Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
            objCatalogos.Parametro = Request("idPantalla")
            ds_siguienteTarea = objCatalogos.Catalogos(6)

            Dim tarea_rechazo As String = ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO").ToString()

            If ds_siguienteTarea.Tables.Count > 0 Then
                If ds_siguienteTarea.Tables(0).Rows.Count > 0 Then
                    Dim Impagos_JSON As String = String.Empty

                    If Impagos.ImpagoAmounts.Count > 0 Then
                        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

                        Dim ImpagosDetailsJSON As ImpagosDetailsJSON = New ImpagosDetailsJSON()
                        ImpagosDetailsJSON.ImpagoAmounts = Impagos.ImpagoAmounts
                        Impagos_JSON = serializer.Serialize(ImpagosDetailsJSON)
                    End If

                    asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"), Impagos.message, Impagos_JSON, Impagos.ImpagoAmounts.Count)
                End If
            End If
        End If
    End Sub

    Private Function GetImpagos(ByVal sol As String) As ImpagosDetails
        Dim Impago As ImpagosDetails = New ImpagosDetails()
        Impago.Impago = True
        Impago.totalImpagos = 0

        Try
            Dim jsonBODY As String = String.Empty
            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            Dim cliente As ClienteBBVA = getClientBBVA(sol)

            Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
            Dim monto As String = getMonto(Request.QueryString("sol").ToString())
            rest.Uri = System.Configuration.ConfigurationManager.AppSettings("RiesgoWS").ToString() + "?customerId=" + cliente.cve_cliente + "&requestAmount=" + monto

            Dim jsonResult As String = rest.ConnectionGet(userID, iv_ticket1, jsonBODY)

            If ((rest.IsError = False) And (jsonResult <> "{}")) Then
                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

                Dim riskRegistry As riskRegistry = serializer.Deserialize(Of riskRegistry)(jsonResult)

                Dim totalImpagos As Double = riskRegistry.measures.totalAwardedAmount.amount

                If totalImpagos > 0 Then
                    Impago.Impago = False
                    Impago.totalImpagos = totalImpagos

                    Dim ImpagoAmounts As ImpagoAmounts = New ImpagoAmounts()
                    ImpagoAmounts.Monto = totalImpagos
                    ImpagoAmounts.Detalle = "Recibo 1"
                    Impago.message = "Cliente con impagos en cuenta Bancomer, favor de comunicar al cliente a línea Bancomer."

                    Impago.ImpagoAmounts.Add(ImpagoAmounts)
                End If

                'For Each item As contract In riskRegistry.riskRegistry
                '    Dim PendingPayment As Double = 

                '    If PendingPayment > 0 Then
                '        countDetalle = countDetalle + 1

                '        Impago.Impago = False
                '        Impago.totalImpagos = Impago.totalImpagos + PendingPayment

                '        Dim ImpagoAmounts As ImpagoAmounts = New ImpagoAmounts()
                '        ImpagoAmounts.Monto = PendingPayment
                '        ImpagoAmounts.Detalle = "Recibo " + countDetalle.ToString()
                '        Impago.message = "Cliente con impagos en cuenta Bancomer, favor de comunicar al cliente a línea Bancomer."

                '        Impago.ImpagoAmounts.Add(ImpagoAmounts)
                '    End If
                'Next

                If (Impago.Impago = False) Then
                    Dim XmlSerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(Impago.ImpagoAmounts.GetType)
                    Dim xmlImpagos As StringWriter = New StringWriter()
                    XmlSerializer.Serialize(xmlImpagos, Impago.ImpagoAmounts)

                    Impago.Impago = saveImpagos(sol, xmlImpagos.ToString())
                End If
            ElseIf (rest.IsError) Then
                Impago.Impago = False
            End If
        Catch ex As Exception
            Impago.Impago = False
        End Try

        Return Impago
    End Function

    Public Class ClienteBBVA
        Public cliente As Boolean
        Public cve_cliente As String
    End Class

    Private Function getClientBBVA(ByVal folio As String) As ClienteBBVA
        Dim cliente_BBVA As ClienteBBVA = New ClienteBBVA()
        cliente_BBVA.cliente = False
        cliente_BBVA.cve_cliente = ""

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
                cliente_BBVA.cliente = reader(0)
                cliente_BBVA.cve_cliente = reader(1)
            Loop
        Catch ex As Exception
            cliente_BBVA.cliente = False
        End Try

        sqlConnection1.Close()

        Return cliente_BBVA
    End Function

    Private Function getMonto(ByVal folio_id As String) As String
        Dim cotizacion As String = getCotizacion(folio_id)

        Dim COT As String = ""
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ConexProcotiza").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_Monto_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@COTIZACION", cotizacion)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                COT = reader(0)
            Loop
        Catch ex As Exception
            COT = "ERROR"
        End Try
        sqlConnection1.Close()
        Return COT
    End Function

    Private Function getCotizacion(ByVal folio_id As String) As String
        Dim COT As String = ""
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            Dim id_folio As Integer = Integer.Parse(folio_id)
            cmd.CommandText = "get_cotizacion_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@FOLIO", id_folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                COT = reader(0)
            Loop
        Catch ex As Exception
            COT = "ERROR"
        End Try
        sqlConnection1.Close()
        Return COT
    End Function

    Public Class riskRegistry
        Public riskRegistry As List(Of contract) = New List(Of contract)()
        Public measures As measures = New measures()
    End Class

    Public Class contract
        Public contract As List(Of minContract) = New List(Of minContract)()
    End Class

    Public Class minContract
        Public product As product = New product()
    End Class

    Public Class product
        Public loan As loan = New loan()
    End Class

    Public Class loan
        Public pendingPayment As pendingPayment = New pendingPayment()
    End Class

    Public Class pendingPayment
        Public amount As Double
    End Class

    Public Class measures
        Public totalAwardedAmount As totalAwardedAmount = New totalAwardedAmount()
    End Class

    Public Class totalAwardedAmount
        Public amount As Double
    End Class

    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer, ByVal msg As String, ByVal Impagos_JSON As String, ByVal regs As Integer)
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

            'BD.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

            If muestrapant = 0 Then
                'Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                Dim _str As String = "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() & "&sol=" & Val(Request("Sol")).ToString() & "&usuario=" & Val(Request("usuario")).ToString()
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirectSpecial('" + msg + "', '" + _str + "', '" + Impagos_JSON + "', '" + regs.ToString() + "');", True)
            ElseIf muestrapant = 2 Then
                'Response.Redirect("../aspx/consultaPanelControl.aspx")
                Dim dc As New clsDatosCliente
                dc.idSolicitud = Val(Request("sol"))
                dc.getDatosSol()
                Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                str_ = ""
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirectSpecial('" + msg + "', '../aspx/consultaPanelControl.aspx" + str_ + "', '" + Impagos_JSON + "', '" + regs.ToString() + "');", True)
                Return
            End If

        Catch ex As Exception
            'Master.MensajeError(mensaje)
        End Try
    End Sub

    Public Class ImpagosDetails
        Public Impago As Boolean
        Public totalImpagos As Double
        Public message As String = "RECHAZO Solicitud no viable por políticas."
        Public ImpagoAmounts As List(Of ImpagoAmounts) = New List(Of ImpagoAmounts)
    End Class

    Public Class ImpagosDetailsJSON
        Public ImpagoHeaders As List(Of String) = New List(Of String) From {"Concepto", "Monto"}
        Public ImpagoAmounts As List(Of ImpagoAmounts) = New List(Of ImpagoAmounts)
    End Class

    Public Class ImpagoAmounts
        Public Monto As Double
        Public Detalle As String
    End Class

    Private Function saveImpagos(ByVal idSol As String, ByVal XML_Impagos As String) As Boolean
        Dim save As Boolean = False
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            Dim id_folio As Integer = Integer.Parse(idSol)
            cmd.CommandText = "set_Impagos_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", id_folio)
            cmd.Parameters.AddWithValue("@XML_IMPAGOS", XML_Impagos)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                save = reader("RESULT")
                Dim regs_save As Integer = reader("REGS")
            Loop
        Catch ex As Exception
            save = False
        End Try
        sqlConnection1.Close()

        Return save
    End Function
End Class
