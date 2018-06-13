'RQ-PD21: JMENDIETA: 13/02/2018: Se crea el aspx con la fusión de Consulta Imax y Antifraude Basico Riesgo
'RQ-PD21-2: JMENDIETA: 27/02/2018: Se agrega funcionalidad para impagos.
'BUG-PD-384 DCORNEJO 07/03/2018: Modificacion de los objetos para obtener una respuesta mas acertada en Impago y Riesgo-->08/03/18 Se agrega Monto Financiero
'BUG-PD-422 :JMENDDIETA 17/04/2018: Se cambia la estructura de la url para tareas automaticas.
'BUG-PD-387 GVARGAS 21/05/2018 Save Homoclave from WS.

'BUG-PD-423: CGARCIA: 23/04/2018: SE MANDA A BACK LA ACTUALIZACION DE STATUS DE LOS DOCUMENTOS.
Imports ProdeskNet.Catalogos
Imports System.Data
Imports ProdeskNet.SN
Imports ProdeskNet.BD
Imports System.Data.SqlClient
Imports ProdeskNet.Configurcion

Partial Class aspx_ImaxAntifraude
    Inherits System.Web.UI.Page


    Dim usu As String
    Dim clien As New clsDatosCliente
    Dim sol As New clsStatusSolicitud
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Public ClsEmail As New ProdeskNet.SN.clsEmailAuto()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim intEnable As Integer
            Try
                intEnable = CInt(Request.QueryString("Enable"))

            Catch ex As Exception
                intEnable = 0
            End Try
            usu = Val(Request("usuario"))
            If usu = String.Empty Then
                usu = Val(Request("usu"))
            End If

            clien.GetDatosCliente(Request("sol"))
            sol.getStatusSol(Request("sol"))
            hdPantalla.Value = Request("idPantalla")
            hdSolicitud.Value = Request("sol")
            hdusuario.Value = Request("usu")
            Me.lblSolicitud.Text = Request("sol")
            Me.lblCliente.Text = clien.propNombreCompleto
            Me.lblStCredito.Text = sol.PStCredito
            Me.lblStDocumento.Text = sol.PStDocumento

            Session.Add("idSol", hdSolicitud.Value)

            If Session("Regresar") Is Nothing Then
                Session("Regresar") = Request.UrlReferrer.LocalPath
                hdRutaEntrada.Value = Request.UrlReferrer.LocalPath
            End If

            hdRutaEntrada.Value = Session("Regresar")

            Session("idSol") = hdSolicitud.Value
            Dim res As New DataSet
            Dim clssol As New clsTabDatosSolicitante
            res = clssol.CargaDatosSolicitante(CInt(Request("sol")))
            If clssol.StrError <> "" Then
                Master.MensajeError(clssol.StrError)
                Exit Sub
            End If
            lblnocliente.Text = res.Tables(0).Rows(0).Item("CLIENTE_INCREDIT").ToString

            Dim BD As New clsManejaBD
            Dim dsresult As New DataSet
            Try
                dsresult = BD.EjecutarQuery("EXEC sp_GenerarRura  " & hdSolicitud.Value & "," & hdPantalla.Value & "," & hdusuario.Value & "")
                If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
                    hdnResultado.Value = dsresult.Tables(0).Rows(0).Item("RUTA")
                End If

            Catch ex As Exception
                hdnResultado.Value = Session("Regresar")
            End Try

            'Se carga información del cuestionario de IMAX
            Dim clsquiz As New clsCuestionarioIMAX()
            clsquiz._ID_SOLICITUD = Request("sol")
            Dim dsres = clsquiz.GetCuestionarioIMAX()
            If clsquiz.StrError = "" Then
                If Not IsNothing(dsres) Then
                    ddlTipoID.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_TIPO_ID"))
                    ddlfotoyfirma.SelectedValue = CInt(dsres.Tables(0).Rows(0).Item("PDK_FIRMA_Y_FOTO"))
                End If
            Else
                Master.MensajeError("Error: " + clsquiz.StrError)
            End If


            'Tabla de Antifraude Basico Riesgo
            Dim tblAntifraude As New DataTable

            tblAntifraude.Columns.Add("Cta_BBVA", GetType(String))
            tblAntifraude.Columns.Add("Nombre_Cliente", GetType(String)) 'RQ-PD-21-2
            tblAntifraude.Columns.Add("RFC", GetType(String))
            tblAntifraude.Columns.Add("FECHA_NAC", GetType(String))
            tblAntifraude.Columns.Add("Domicilio", GetType(String))
            tblAntifraude.Columns.Add("Riesgo", GetType(String))
            tblAntifraude.Columns.Add("Impago", GetType(String)) 'RQ-PD-21-2

            'Enabled
            If intEnable = 1 Then
                ddlTipoID.Enabled = False
                ddlfotoyfirma.Enabled = False
                lblnocliente.Enabled = False
                btnProcesar.Attributes.Add("style", "display:none;")
                btnCancelar.Attributes.Add("style", "display:none;")

                'Carga información de antifraude
                tblAntifraude = getAntiFraudeRiesgo(Request.QueryString("sol").ToString())

                Dim cve_cliente As ClienteBBVA_ = getClientBBVA(Request.QueryString("sol").ToString())

                lbl_CVEBBVA.Text = String.Format("El número de cliente seleccionado es: {0}", cve_cliente.cve_cliente)
                txtRiesgo.Text = getTotalRisk(Request.QueryString("sol").ToString())
                txtImpago.Text = getTotalImpago(Request.QueryString("sol").ToString()) 'RQ-PD-21-2
            Else

                Dim infoCliente As listadoClientes = clienteBBVA()
                Dim client As List(Of Cliente) = infoCliente.Clientes

                If (infoCliente.valido) Then
                    If (IsPostBack = False) Then
                        If (client.Count > 0) Then

                            Dim client_final_risk As Cliente = New Cliente()
                            Dim client_final_impago As Cliente = New Cliente() 'RQ-PD-21-2
                            Dim client_final_pre As Cliente = New Cliente()
                            Dim client_final_fecha As Cliente = New Cliente()
                            Dim Total_Risk As Decimal = 0.0
                            Dim Temporal_Risk_1 As Decimal = 0.0
                            Dim Temporal_Risk_2 As Decimal = 0.0

                            'RQ-PD-21-2 INI
                            Dim Total_Impago As Decimal = 0.0
                            Dim Temporal_Impago_1 As Decimal = 0.0
                            Dim Temporal_Impago_2 As Decimal = 0.0
                            Dim Riesgo As Decimal = 0.0
                            'RQ-PD-21-2 FIN

                            Dim Temporal_Monto_Pre_1 As Decimal = 0.0
                            Dim Temporal_Monto_Pre_2 As Decimal = 0.0

                            Dim Temporal_Date_1 As Date = Date.Today()
                            Dim Temporal_Date_2 As Date = Date.Today()

                            Dim deleteBan As Integer = 0

                            For Each client_temporal As Cliente In client

                                If deleteBan = 0 Then
                                    deleteHistoricoRiesgo(Request.QueryString("sol").ToString())
                                    deleteBan = 1
                                End If

                                'RQ-PD-21-2
                                tblAntifraude.Rows.Add(client_temporal.Cta_BBVA, client_temporal.Nombre + " " + client_temporal.Ape_Pat + " " + client_temporal.Ape_Mat, client_temporal.RFC, client_temporal.FECHA_NAC, client_temporal.Domicilio, client_temporal.Riesgo, client_temporal.Impago)

                                insertIntoAntiRiesgo(Request.QueryString("sol").ToString(), client_temporal)

                                If (client_temporal.Riesgo <> "") Then
                                    Dim String_Risk As String = Replace(client_temporal.Riesgo.ToString, ",", "")
                                    Total_Risk = Total_Risk + Val(String_Risk)
                                    Temporal_Risk_1 = Val(String_Risk)

                                    If (Temporal_Risk_1 > Temporal_Risk_2) Then
                                        Temporal_Risk_2 = Temporal_Risk_1
                                        client_final_risk = client_temporal
                                    End If
                                End If

                                'RQ-PD-21-2 INI
                                If (client_temporal.Impago <> "") Then
                                    Dim String_Impago As String = Replace(client_temporal.Impago.ToString, ",", "")
                                    Total_Impago = Total_Impago + Val(String_Impago)
                                    Temporal_Impago_1 = Val(String_Impago)

                                    If (Temporal_Impago_1 > Temporal_Impago_2) Then
                                        Temporal_Impago_2 = Temporal_Impago_1
                                        client_final_impago = client_temporal
                                    End If
                                End If
                                'RQ-PD-21-2 FIN

                                If (client_temporal.PreAprovado = "SI") Then
                                    Dim String_Monto As String = Replace(client_temporal.Monto_Pre.ToString, ",", "")
                                    Temporal_Monto_Pre_1 = Val(String_Monto)

                                    If (Temporal_Monto_Pre_1 > Temporal_Monto_Pre_2) Then
                                        Temporal_Monto_Pre_2 = Temporal_Monto_Pre_1
                                        client_final_pre = client_temporal
                                    End If
                                End If

                                If (client_temporal.Fecha_Open_Count <> "") Then
                                    Temporal_Date_1 = Date.Parse(client_temporal.Fecha_Open_Count.ToString())

                                    If (Temporal_Date_1 <= Temporal_Date_2) Then
                                        Temporal_Date_2 = Temporal_Date_1
                                        client_final_fecha = client_temporal
                                    End If
                                End If
                            Next

                            Dim cta_bbva As String = String.Empty
                            If (client_final_risk.Cta_BBVA <> Nothing) Then
                                cta_bbva = client_final_risk.Cta_BBVA
                            ElseIf (client_final_impago.Cta_BBVA <> Nothing) Then 'RQ-PD-21-2 
                                cta_bbva = client_final_impago.Cta_BBVA
                            ElseIf (client_final_pre.Cta_BBVA <> Nothing) Then
                                cta_bbva = client_final_pre.Cta_BBVA
                            ElseIf (client_final_fecha.Cta_BBVA <> Nothing) Then
                                cta_bbva = client_final_fecha.Cta_BBVA
                            End If

                            If (cta_bbva <> "") Then
                                updateCteIncredit(Request.QueryString("sol").ToString(), cta_bbva)
                                lbl_CVEBBVA.Text = "El número de cliente seleccionado es: " + cta_bbva.ToString()
                            Else
                                btnProcesar.Enabled = False
                                grd.EmptyDataText = "Ocurrió un error en WS, intente más tarde."
                            End If

                            Dim monto As String = getMonto(Request.QueryString("sol").ToString())

                            txtMonto.Text = monto.ToString()

                            txtImpago.Text = Total_Impago.ToString() 'RQ-PD-21-2 

                            txtRiesgo.Text = Total_Risk + monto
                        End If
                    End If
                Else
                    btnProcesar.Enabled = False
                    grd.EmptyDataText = "Ocurrió un error en WS, intente más tarde."
                End If

            End If

            grd.DataSource = tblAntifraude
            grd.DataBind()

        End If
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');", True)

    End Sub

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub

    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click

        Try
            Dim objTarea As New clsSolicitudes(0)
            Dim dsValidaTarea As DataSet
            Dim ValTareas As Integer = 0
            objTarea.PDK_ID_SOLICITUD = Request("sol")
            objTarea.PDK_ID_PANTALLA = Request("idPantalla")
            dsValidaTarea = objTarea.ManejaTarea(1)

            If dsValidaTarea.Tables.Count > 0 Then
                If dsValidaTarea.Tables(0).Rows.Count > 0 Then
                    ValTareas = dsValidaTarea.Tables(0).Rows(0).Item("VALIDA_TAREA")
                End If
            End If
            If ValTareas = 1 Then
                btnProcesar.Enabled = False
                If ddlTipoID.SelectedValue <> -1 And ddlfotoyfirma.SelectedValue <> -1 Then
                    Dim clsquiz As New clsCuestionarioIMAX
                    clsquiz._ID_SOLICITUD = hdSolicitud.Value
                    clsquiz._TIPO_ID = ddlTipoID.SelectedValue
                    clsquiz._FOTO_FIRMA_ID = ddlfotoyfirma.SelectedValue
                    If clsquiz.InsertDatosCuestionarioIMAX() Then
                        Dim ds_siguienteTarea As DataSet
                        ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & CInt(Request("idPantalla")).ToString)
                        If ddlfotoyfirma.SelectedValue = 0 Then
                            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"), 0)
                        ElseIf ddlfotoyfirma.SelectedValue = 1 Then
                            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"), 1)
                        End If
                    Else
                        Throw New Exception("Error: Ocurrio un problema al guardar el cuestionario")
                    End If
                Else
                    Throw New Exception("Error: Falta contestar preguntas del cuestionario. *")
                End If
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
            btnProcesar.Enabled = True
        End Try


    End Sub

    Private Function getAntiFraudeRiesgo(ByVal folio_id As String) As DataTable
        Dim dt As DataTable = New DataTable()

        dt.Columns.Add("Cta_BBVA", GetType(String))
        dt.Columns.Add("Nombre_Cliente", GetType(String)) 'RQ-PD-21-2
        dt.Columns.Add("RFC", GetType(String))
        dt.Columns.Add("FECHA_NAC", GetType(String))
        dt.Columns.Add("Domicilio", GetType(String))
        dt.Columns.Add("Riesgo", GetType(String))
        dt.Columns.Add("Impago", GetType(String)) 'RQ-PD-21-2

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_AntiFraudeRiesgo_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio_id)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                Dim CtaBBVA As String = reader(0)
                Dim NOMBRE As String = reader(1)
                Dim AP_PAT As String = reader(2)
                Dim AP_MAT As String = reader(3)
                Dim RFC As String = reader(4)
                Dim FECH_NAC As String = reader(5)
                Dim DOMICILIO As String = reader(6)
                Dim Score As String = reader(7)
                Dim IMPAGO As String = reader(8) 'RQ-PD-21-2
                'RQ-PD-21-2
                dt.Rows.Add(CtaBBVA, NOMBRE + " " + AP_PAT + " " + AP_MAT, RFC, FECH_NAC, DOMICILIO, Score, IMPAGO)
            Loop
        Catch ex As Exception
            dt = New DataTable()
        End Try
        sqlConnection1.Close()
        Return dt
    End Function

    Private Function getClientBBVA(ByVal folio As String) As ClienteBBVA_
        Dim cliente_BBVA As ClienteBBVA_ = New ClienteBBVA_()
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

    Private Function getTotalRisk(ByVal folio As String) As String
        Dim TotalRisk As String = "0.00"

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_TotalRisk_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                TotalRisk = reader(0).ToString()
            Loop
        Catch ex As Exception
            TotalRisk = "0.00"
        End Try

        sqlConnection1.Close()

        Return TotalRisk
    End Function

    Private Function clienteBBVA() As listadoClientes 'As List(Of Cliente)
        Dim valido_ As Boolean = False
        Dim listBBVANumClient As List(Of Cliente) = New List(Of Cliente)

        Dim solicitante_info As List(Of String) = getSolicitante(Request.QueryString("sol").ToString())

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonBODY As String = String.Empty

        If solicitante_info(2).ToString().ToUpper().Contains("Ñ") Then
            Dim listCostumerClass As listCostumerClass_sinEnie = New listCostumerClass_sinEnie()

            listCostumerClass.name = solicitante_info(0)
            listCostumerClass.lastName = solicitante_info(1)
            listCostumerClass.mothersLastName = solicitante_info(2)
            listCostumerClass.rfc = solicitante_info(3)
            listCostumerClass.homonimy = solicitante_info(5)

            jsonBODY = serializer.Serialize(listCostumerClass)
        Else
            Dim listCostumerClass As listCostumerClass = New listCostumerClass()

            listCostumerClass.name = solicitante_info(0)
            listCostumerClass.lastName = solicitante_info(1)
            listCostumerClass.mothersLastName = solicitante_info(2)
            listCostumerClass.rfc = solicitante_info(3)
            listCostumerClass.homonimy = solicitante_info(5)

            jsonBODY = serializer.Serialize(listCostumerClass)
        End If

        Dim rfc_ As String = solicitante_info(3)

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("CostumersWS").ToString()

        Dim jsonResult As String = rest.ConnectionPost(userID, iv_ticket1, jsonBODY)

        If ((rest.IsError = False) And (rest.StatusHTTP = "200")) Then
            valido_ = True

            Dim customerList As customerList = serializer.Deserialize(Of customerList)(jsonResult)

            For Each person_ As objetos In customerList.customerList
                Dim bandera_RFC As Boolean = False
                Dim RFC_byWS As String = String.Empty

                For Each Document As identityDocument_ In person_.person.identityDocument
                    If ((Document.type.name = "RFC") And (Document.number.IndexOf(solicitante_info(3).ToUpper()) <> -1)) Then
                        bandera_RFC = True
                        RFC_byWS = Document.number
                    End If
                Next

                If (bandera_RFC) Then
                    Dim _num As String = person_.person.id

                    Dim client As New Cliente()

                    client.Cta_BBVA = person_.person.id
                    client.Nombre = solicitante_info(0)
                    client.Ape_Pat = solicitante_info(1)
                    client.Ape_Mat = solicitante_info(2)
                    client.RFC = RFC_byWS 'solicitante_info(3)
                    client.FECHA_NAC = solicitante_info(4)
                    'client.Riesgo = Riesgo(person_.person.id)
                    client.Domicilio = person_.person.legalAddress.streetName + " " + person_.person.legalAddress.streetNumber + ", " + person_.person.legalAddress.neighborhood + ", " + person_.person.legalAddress.city
                    client.Fecha_Open_Count = fecha(person_.person.id)
                    Dim PreAprovado As PreAprovado = pre(person_.person.id)
                    client.PreAprovado = PreAprovado.pre
                    client.Monto_Pre = PreAprovado.monto
                    'client.Impago = Impago(person_.person.id) 'RQ-PD-21-2
                    Dim evaluacionRiesgo = Riesgo(person_.person.id)
                    client.Riesgo = evaluacionRiesgo.Riesgo
                    client.Impago = evaluacionRiesgo.Impago
                    client.Homoclave = RFC_byWS.Replace(solicitante_info(3).ToUpper(), "")

                    listBBVANumClient.Add(client)
                End If
            Next
        End If

        Dim ListaClientes As listadoClientes = New listadoClientes()
        ListaClientes.Clientes = listBBVANumClient
        ListaClientes.valido = valido_
        Return ListaClientes
    End Function

    Private Function getSolicitante(ByVal folio_id As String) As List(Of String)
        Dim nombres As List(Of String) = New List(Of String)()
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            Dim id_folio As Integer = Integer.Parse(folio_id)
            cmd.CommandText = "get_Solicitante_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", id_folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                nombres.Add(reader(0))
                nombres.Add(reader(1))
                nombres.Add(reader(2))
                nombres.Add(reader(3))
                nombres.Add(reader(4))
                nombres.Add(reader(5))
            Loop
        Catch ex As Exception
            nombres.Add("")
            nombres.Add("")
            nombres.Add("")
            nombres.Add("")
            nombres.Add("")
            nombres.Add("")
        End Try
        sqlConnection1.Close()
        Return nombres
    End Function

    Private Function Riesgo(ByVal num_cliente As String) As evaluacionRiesgo
        'Dim risk As String = ""
        Dim evaluacionRiesgo As New evaluacionRiesgo
        Dim jsonBODY As String = ""
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        'Dim monto As String = getMonto(Request.QueryString("sol").ToString())
        Dim monto As String = 0
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("RiesgoWS").ToString() + "?customerId=" + num_cliente + "&requestAmount=" + monto

        Dim jsonResult As String = rest.ConnectionGet(userID, iv_ticket1, jsonBODY)

        If ((rest.IsError = False) And (jsonResult <> "{}")) Then
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim riskRegistry As riskRegistry = serializer.Deserialize(Of riskRegistry)(jsonResult)
            evaluacionRiesgo.Riesgo = riskRegistry.potentialRisk.totalPotentialRisk.amount
            evaluacionRiesgo.Impago = riskRegistry.measures.totalAwardedAmount.amount

            set_Total_Creditos_SP(Request.QueryString("sol").ToString(), riskRegistry.riskRegistry.Count)
        End If

        Return evaluacionRiesgo
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

    Private Function set_Total_Creditos_SP(ByVal folio_id As String, ByVal Total As Integer) As String
        Dim COT As String = "OK"
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            Dim id_folio As Integer = Integer.Parse(folio_id)
            cmd.CommandText = "set_Total_Creditos_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", id_folio)
            cmd.Parameters.AddWithValue("@Total_Creditos", Total)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
            Loop
        Catch ex As Exception
            COT = "ERROR"
        End Try
        sqlConnection1.Close()
        Return COT
    End Function

    Private Function fecha(ByVal num_cliente As String) As String
        Dim fecha_ As String = ""
        Dim jsonBODY As String = ""
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("CostumerDateCount").ToString().Replace("CambiarPorCuenta", num_cliente)

        Dim jsonResult As String = rest.ConnectionGet(userID, iv_ticket1, jsonBODY)
        If (rest.IsError = False) Then
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim membershipDate As membershipDate = serializer.Deserialize(Of membershipDate)(jsonResult)
            fecha_ = membershipDate.membershipDate.Substring(0, 10)
        End If
        Return fecha_
    End Function

    Private Function pre(ByVal num_cliente As String) As PreAprovado
        Dim PreAprovado As PreAprovado = New PreAprovado()
        PreAprovado.monto = "0.0"
        PreAprovado.pre = "NO"
        Dim jsonBODY As String = ""
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("Proaprobados").ToString().Replace("CambiarPorCuenta", num_cliente)

        Dim jsonResult As String = rest.ConnectionGet(userID, iv_ticket1, jsonBODY)
        If (rest.IsError = False) Then
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim loanAlternatives As loanAlternatives = serializer.Deserialize(Of loanAlternatives)(jsonResult)

            If (loanAlternatives.loanAlternatives.Count > 0) Then
                Dim monto_1 As Decimal = 0.0
                Dim monto_2 As Decimal = 0.0
                Dim _monto As String = String.Empty

                Dim monto_final As String = String.Empty
                Dim pre_ As String = "NO"

                For Each PreApro As loanAlternatives_ In loanAlternatives.loanAlternatives
                    If (PreApro.loanBase.productCode = "AUTO" Or PreApro.loanBase.productDescription = "AUTO") Then
                        pre_ = "SI"
                        _monto = Replace(PreApro.productAmount.amount.ToString, ",", "")
                        monto_1 = Val(_monto)

                        If (monto_1 > monto_2) Then
                            monto_2 = monto_1
                            monto_final = PreApro.productAmount.amount
                        End If
                    End If
                Next

                PreAprovado.monto = monto_final
                PreAprovado.pre = pre_
            End If

            Dim str As String = ""
        End If
        Return PreAprovado
    End Function

    Private Sub deleteHistoricoRiesgo(ByVal folio_id As String)
        Dim query As StringBuilder = New StringBuilder()
        query.Append("DELETE PDK_TAB_ANTIFRAUDE_RIESGO WHERE PDK_ID_SECCCERO = " + folio_id)

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
            Loop
        Catch

        End Try
        sqlConnection1.Close()
    End Sub

    Private Sub updateCteIncredit(ByVal folio_id As String, ByVal Cte_Incredit As String)
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "update_CteIncredit_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio_id)
            cmd.Parameters.AddWithValue("@CLIENTE_INCREDIT", Cte_Incredit)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
            Loop
        Catch

        End Try
        sqlConnection1.Close()
    End Sub

    Private Sub insertIntoAntiRiesgo(ByVal folio_id As String, ByVal Cliente As Cliente)
        Dim query As StringBuilder = New StringBuilder()
        query.Append("INSERT INTO PDK_TAB_ANTIFRAUDE_RIESGO ( PDK_ID_SECCCERO, Cta_BBVA , Riesgo, Domicilio, Fecha_Open_Count, PreAprovado, Monto_Pre, Impago, Homoclave ) VALUES (")
        query.Append(folio_id + ", ")
        query.Append("'" + Cliente.Cta_BBVA + "', ")
        query.Append("'" + Cliente.Riesgo + "', ")
        query.Append("'" + Cliente.Domicilio + "', ")
        query.Append("'" + Cliente.Fecha_Open_Count + "', ")
        query.Append("'" + Cliente.PreAprovado + "', ")
        query.Append("'" + Cliente.Monto_Pre + "', ")
        query.Append("'" + Cliente.Impago + "', ")
        query.Append("'" + Cliente.Homoclave + "' )")

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
            Loop
        Catch

        End Try
        sqlConnection1.Close()
    End Sub

    'RQ-PD-21-2
    'Private Function Impago(ByVal num_cliente As String) As String
    '    Dim risk As String = ""
    '    Dim jsonBODY As String = ""
    '    Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
    '    Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

    '    Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
    '    Dim monto As String = getMonto(Request.QueryString("sol").ToString())
    '    rest.Uri = System.Configuration.ConfigurationManager.AppSettings("RiesgoWS").ToString() + "?customerId=" + num_cliente + "&requestAmount=" + monto

    '    Dim jsonResult As String = rest.ConnectionGet(userID, iv_ticket1, jsonBODY)

    '    If ((rest.IsError = False) And (jsonResult <> "{}")) Then
    '        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
    '        Dim riskRegistry As riskRegistry = serializer.Deserialize(Of riskRegistry)(jsonResult)
    '        risk = riskRegistry.measures.totalAwardedAmount.amount
    '    End If

    '    Return risk
    'End Function

    'RQ-PD-21-2
    Private Function getTotalImpago(ByVal folio As String) As String
        Dim Impago As Decimal = 0.0
        Dim TotalImpago As String = "0.00"

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_AntiFraudeRiesgo_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                Impago = Impago + reader(8)
            Loop
            TotalImpago = Impago.ToString()
        Catch ex As Exception
            TotalImpago = "0.00"
        End Try

        sqlConnection1.Close()

        Return TotalImpago
    End Function

    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer, ByVal intRechazo As Integer)
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

            If idAsignarPantalla <> 0 Then
                Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla
            End If

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                Master.MsjErrorRedirect(mensaje, "../aspx/consultaPanelControl.aspx")
                Exit Sub
            End If

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))
            Dim dc As New clsDatosCliente
            dc.idSolicitud = Request("sol")
            dc.getDatosSol()


            Dim strLocation As String = String.Empty '

            If muestrapant = 0 Then
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usu=" & Val(Request("usu")).ToString & "');", True) BUG-PD-422
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString) ''BUG-PD-422
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True) 'BUG-PD-422

            ElseIf muestrapant = 2 Then
                If (intRechazo = 0) Then
                    Dim dcc As New ProdeskNet.Catalogos.clsDatosCliente
                    dcc.idSolicitud = Val(Request("sol"))
                    dcc.getDatosSol()
                    ClsEmail.OPCION = 17
                    ClsEmail.TAREA_ACTUAL = Val(Request("idPantalla").ToString)

                    Dim dtConsulta = New DataSet()
                    dtConsulta = ClsEmail.ConsultaStatusNotificacion
                    If (Not IsNothing(dtConsulta) AndAlso dtConsulta.Tables.Count > 0 AndAlso dtConsulta.Tables(0).Rows.Count() > 0) Then
                        If (CInt(dtConsulta.Tables(0).Rows(0).Item("RESULTADO").ToString) = 1) Then
                            If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS").ToString) = 2) Then
                                If (CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_EMAIL").ToString) = 2 Or CInt(CInt(dtConsulta.Tables(1).Rows(0).Item("PDK_STATUS_SMS").ToString) = 2)) Then
                                    Dim strLocation1 As String = String.Empty
                                    strLocation1 = ("../aspx/ValidaEmails.aspx?idPantalla=185&Sol=" & Val(Request("Sol")).ToString & "&mostrarPant=2" & "&usuario=" & usu)
                                    'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation1 & "';", True)
                                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '" + strLocation1 + "');", True)
                                Else
                                    Dim str As String = "?NoSolicitud=" & dcc.idSolicitud & "&Empresa=" & dcc.idempresa & "&Producto=" & dcc.idproducto & "&Persona=" & dcc.idtpersona
                                    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                                End If
                            Else
                                Dim str As String = "?NoSolicitud=" & dcc.idSolicitud & "&Empresa=" & dcc.idempresa & "&Producto=" & dcc.idproducto & "&Persona=" & dcc.idtpersona
                                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                            End If
                        Else
                            Dim str As String = "?NoSolicitud=" & dcc.idSolicitud & "&Empresa=" & dcc.idempresa & "&Producto=" & dcc.idproducto & "&Persona=" & dcc.idtpersona
                            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                        End If
                    Else
                        Dim str As String = "?NoSolicitud=" & dcc.idSolicitud & "&Empresa=" & dcc.idempresa & "&Producto=" & dcc.idproducto & "&Persona=" & dcc.idtpersona
                        ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '../aspx/consultaPanelControl.aspx" + str + "');", True)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "');", True)
                End If
            Else
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "');", True)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "');", True)
                'Response.Redirect("../aspx/consultaPanelControl.aspx")
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function getBack(ByVal id As String, ByVal docType As String, ByVal folio As String, ByVal Check_VAL As String, ByVal Check_REC As String, ByVal Checked As String) As String
        Dim respuesta As String = String.Empty
        Dim clsDoc As clsDocumentos = New clsDocumentos
        Dim dsDatos As DataSet

        clsDoc.id = id
        clsDoc.docType = docType
        clsDoc.folio = folio
        clsDoc.Check_VAL = Check_VAL
        clsDoc.Check_REC = Check_REC
        clsDoc.Checked = Checked

        dsDatos = clsDoc.getActualizaDatosDocumentos(1)

        If (dsDatos.Tables.Count > 1 AndAlso dsDatos.Tables(0).Rows.Count > 0 AndAlso dsDatos.Tables(1).Rows.Count > 0 AndAlso dsDatos.Tables(1).Rows(0).Item("RESULTADO").ToString <> String.Empty) Then
            respuesta = CStr(dsDatos.Tables(1).Rows(0).Item("RESULTADO").ToString)

        End If

        Return respuesta
    End Function

#Region "Internal Class"
    Public Class ClienteBBVA_
        Public cliente As Boolean
        Public cve_cliente As String
    End Class

    Public Class listadoClientes
        Public Clientes As List(Of Cliente) = New List(Of Cliente)()
        Public valido As Boolean
    End Class

    Public Class Cliente
        Public Cta_BBVA As String
        Public Nombre As String
        Public Ape_Pat As String
        Public Ape_Mat As String
        Public RFC As String
        Public FECHA_NAC As String
        Public Riesgo As String
        Public Domicilio As String
        Public Fecha_Open_Count As String
        Public PreAprovado As String
        Public Monto_Pre As String
        Public Impago As String
        Public Homoclave As String
    End Class

    Public Class listCostumerClass_sinEnie
        Public name As String
        Public lastName As String
        Public mothersLastName As String
        Public rfc As String
        Public homonimy As String
    End Class

    Public Class listCostumerClass
        Public name As String
        Public lastName As String
        Public mothersLastName As String
        Public rfc As String
        Public homonimy As String
    End Class

    Public Class customerList
        Public customerList As List(Of objetos) = New List(Of objetos)
    End Class

    Public Class objetos
        Public person As person = New person()
    End Class

    Public Class identityDocument_
        Public type As type_ = New type_()
        Public number As String = String.Empty
    End Class

    Public Class type_
        Public id As String
        Public name As String
    End Class

    Public Class person
        Public id As String
        Public identityDocument As List(Of identityDocument_) = New List(Of identityDocument_)()
        Public legalAddress As legalAddress = New legalAddress()
    End Class

    Public Class legalAddress
        Public streetName As String
        Public streetNumber As String
        Public neighborhood As String
        Public city As String
    End Class

    Public Class riskRegistry
        Public riskRegistry As List(Of contract) = New List(Of contract)()
        Public potentialRisk As potentialRisk = New potentialRisk()
        Public measures As New measures()
    End Class

    Public Class contract

    End Class

    Public Class potentialRisk
        Public totalPotentialRisk As totalPotentialRisk = New totalPotentialRisk()
    End Class

    Public Class totalPotentialRisk
        Public amount As String
    End Class

    Public Class membershipDate
        Public membershipDate As String
    End Class

    Public Class PreAprovado
        Public pre As String
        Public monto As String
    End Class

    Public Class loanAlternatives
        Public loanAlternatives As List(Of loanAlternatives_) = New List(Of loanAlternatives_)()
    End Class

    Public Class loanAlternatives_
        Public productAmount As productAmount = New productAmount()
        Public loanBase As loanBase = New loanBase()
    End Class

    Public Class productAmount
        Public amount As String
    End Class

    Public Class loanBase
        Public productDescription As String
        Public productCode As String
    End Class

    Public Class measures
        Public totalAwardedAmount As totalAwardedAmount = New totalAwardedAmount()
    End Class

    Public Class totalAwardedAmount
        Public amount As Double
    End Class

    Public Class evaluacionRiesgo
        Property Riesgo As String = String.Empty
        Property Impago As String = String.Empty
    End Class
#End Region

End Class
