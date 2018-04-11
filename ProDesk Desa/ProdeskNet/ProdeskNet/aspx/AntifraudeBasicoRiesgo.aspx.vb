'BBVA-P-423 GVARGAS 05/05/2017 RQADM-07 Antifraude Básico Cliente, Empresa, Riesgos y Pre-Aprobados 40,76
'BUG-PD-72 GVARGAS 08/06/2017 Cambios Riesgos
'BBVA-P-423 RQ-INB225 GVARGAS 07/07/2017 Pantalla antifraude riesgos - ProDesk
'BUG-PD-154 GVARGAS 14/07/2017 Cambios Riesgos select Clien
'BUG-PD-157 GVARGAS 17/07/2017 Cambios cuando riesgo falla
'BUG-PD-160 GVARGAS 18/07/2017 Cambios Riesgo by RFC
'BUG-PD-164 GVARGAS 18/07/2017 Cambios Pre & correcion ortografica
'BUG-PD-165 GVARGAS 19/07/2017 Cambio response.redirect
'BBVA-P-423 RQ-INB226 GVARGAS 04/07/2017 Validación de Impagos (RV02) - ProDesk Get riesgo total
'RQ-PD24 DJUAREZ 19/02/2018 Se agrega la columna impagos a antifraude y se une en una columna el cliente
'RQ-PD21-2: JMENDIETA: 27/02/2018: Se guarda la información de impagos.
'BUG-PD-384 DCORNEJO 07/03/2018: Modificacion de los objetos para obtener una respuesta mas acertada en Impago y Riesgo-->08/03/18 Se agrega Monto Financiero

Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.BD
Imports ProdeskNet.SN
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Class aspx_AntifraudeBasicoRiesgo
    Inherits System.Web.UI.Page
    Dim BD As New ProdeskNet.BD.clsManejaBD
    Dim dc As New ProdeskNet.Catalogos.clsDatosCliente
    Dim es As New ProdeskNet.Catalogos.clsStatusSolicitud
    Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()

    Private Sub aspx_AntifraudeBasicoRiesgo_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("Regresar") Is Nothing Then
            Session("Regresar") = Request.UrlReferrer.LocalPath
            hdRutaEntrada.Value = Request.UrlReferrer.LocalPath
        End If

        hdnIdFolio.Value = Request.QueryString("sol")
        hdnIdPantalla.Value = Request.QueryString("idpantalla")
        hdnUsua.Value = Session("IdUsua")
        dc.GetDatosCliente(Request.QueryString("sol"))
        lblSolicitud.Text = Request.QueryString("sol")
        lblCliente.Text = dc.propNombreCompleto

        es.getStatusSol(Request.QueryString("sol"))
        Me.lblStCredito.Text = es.PStCredito
        Me.lblStDocumento.Text = es.PStDocumento

        Dim table As New DataTable

        table.Columns.Add("Cta_BBVA", GetType(String))
        table.Columns.Add("Nombre_Cliente", GetType(String))
        table.Columns.Add("RFC", GetType(String))
        table.Columns.Add("FECHA_NAC", GetType(String))
        table.Columns.Add("Domicilio", GetType(String))
        table.Columns.Add("Riesgo", GetType(String))
        table.Columns.Add("Impago", GetType(String))

        Dim intEnable As Integer
        intEnable = CInt(Request.QueryString("Enable"))
        If intEnable = 1 Then
            btnProcesarCliente.Attributes.Add("style", "display:none;")
            btnCancelar.Attributes.Add("style", "display:none;")

            table = getAntiFraudeRiesgo(Request.QueryString("sol"))

            Dim cve_cliente As ClienteBBVA_ = getClientBBVA(Request.QueryString("sol").ToString())

            lbl_CVEBBVA.Text = "El número de cliente seleccionado es: " + cve_cliente.cve_cliente
            txtRiesgo.Text = getTotalRisk(Request.QueryString("sol").ToString())
            txtImpago.Text = getTotalImpago(Request.QueryString("sol").ToString())
        Else
            Dim infoCliente As listadoClientes = clienteBBVA()
            Dim client As List(Of Cliente) = infoCliente.Clientes 'se va a sustituir por la pantalla de seleccion de cliente MPUESTO

            If (infoCliente.valido) Then
                If (IsPostBack = False) Then
                    If (client.Count > 0) Then

                        Dim client_final_risk As Cliente = New Cliente()
                        Dim client_final_impago As Cliente = New Cliente()
                        Dim client_final_pre As Cliente = New Cliente()
                        Dim client_final_fecha As Cliente = New Cliente()
                        Dim Total_Risk As Decimal = 0.0
                        Dim Temporal_Risk_1 As Decimal = 0.0
                        Dim Temporal_Risk_2 As Decimal = 0.0

                        Dim Total_Impago As Decimal = 0.0
                        Dim Temporal_Impago_1 As Decimal = 0.0
                        Dim Temporal_Impago_2 As Decimal = 0.0

                        Dim Riesgo As Decimal = 0.0

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

                            table.Rows.Add(client_temporal.Cta_BBVA, client_temporal.Nombre + " " + client_temporal.Ape_Pat + " " + client_temporal.Ape_Mat, client_temporal.RFC, client_temporal.FECHA_NAC, client_temporal.Domicilio, client_temporal.Riesgo, client_temporal.Impago)

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

                            If (client_temporal.Impago <> "") Then
                                Dim String_Impago As String = Replace(client_temporal.Impago.ToString, ",", "")
                                Total_Impago = Total_Impago + Val(String_Impago)
                                Temporal_Impago_1 = Val(String_Impago)

                                If (Temporal_Impago_1 > Temporal_Impago_2) Then
                                    Temporal_Impago_2 = Temporal_Impago_1
                                    client_final_impago = client_temporal
                                End If
                            End If

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
                        ElseIf (client_final_impago.Cta_BBVA <> Nothing) Then
                            cta_bbva = client_final_impago.Cta_BBVA
                            'Me.lblSolicitud.Text = Total_Risk.ToString()
                        ElseIf (client_final_pre.Cta_BBVA <> Nothing) Then
                            cta_bbva = client_final_pre.Cta_BBVA
                        ElseIf (client_final_fecha.Cta_BBVA <> Nothing) Then
                            cta_bbva = client_final_fecha.Cta_BBVA
                        End If

                        'If ((intEnable = 0) And (cta_bbva <> "")) Then
                        If (cta_bbva <> "") Then
                            updateCteIncredit(Request.QueryString("sol").ToString(), cta_bbva)
                            lbl_CVEBBVA.Text = "El número de cliente seleccionado es: " + cta_bbva.ToString()
                        Else
                            btnProcesarCliente.Disabled = True
                            grd.EmptyDataText = "Ocurrió un error en WS, intente más tarde."
                        End If

                        Dim monto As String = getMonto(Request.QueryString("sol").ToString())

                        txtMonto.Text = monto.ToString()

                        txtImpago.Text = Total_Impago.ToString()

                        txtRiesgo.Text = Total_Risk + monto

                        'grd.DataSource = table
                        'grd.DataBind()
                    Else
                    End If
                End If
            Else
                btnProcesarCliente.Disabled = True
                grd.EmptyDataText = "Ocurrió un error en WS, intente más tarde."
            End If
        End If

        grd.DataSource = table
        grd.DataBind()
        'repClientes.DataSource = table
        'repClientes.DataBind()
    End Sub

    Protected Sub btnprocesar_Click(sender As Object, e As EventArgs)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()

        dsresult = DB.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

        dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

        muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

        Dim mensaje As String = "Tarea Exitosa"

        Dim ds_siguienteTarea As DataSet
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
        objCatalogos.Parametro = Request("idPantalla")
        ds_siguienteTarea = objCatalogos.Catalogos(6)

        Dim tarea_norechazo As String = ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO").ToString()

        Dim strLocation As String = String.Empty 'CAMBIO URGENTE 04/08/2017 GVARGAS

        If muestrapant = 0 Then
            'Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
            strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString) 'CAMBIO URGENTE 04/08/2017 GVARGAS
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True) 'CAMBIO URGENTE 04/08/2017 GVARGAS

        ElseIf muestrapant = 2 Then

            Dim dc As New clsDatosCliente
            dc.idSolicitud = Val(Request("sol"))
            dc.getDatosSol()
            Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
            str_ = ""

            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Exitosa.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
        End If
    End Sub

    Public Sub CancelaTarea()
        Dim objCancela As New clsSolicitudes(0)
        objCancela.PDK_ID_SOLICITUD = Request("Sol").ToString()
        objCancela.Estatus = 42
        objCancela.PDK_CLAVE_USUARIO = Session("IdUsua")

        objCancela.ManejaTarea(6)
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub

    Protected Sub btnVisor_Click(sender As Object, e As EventArgs)
        Dim ds As DataSet
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos

        Dim str = opc.Value()

        objCatalogos.Parametro = Me.lblSolicitud.Text
        ds = objCatalogos.Catalogos(7)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Dim Cliente_bbva As String = ""

                Cliente_bbva = ds.Tables(0).Rows(0).Item("CLIENTE_BBVA")

                If Cliente_bbva > 0 Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "&cliente=" + Cliente_bbva.ToString + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?folio=" + Me.lblSolicitud.Text + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)

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

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

            If muestrapant = 0 Then
                Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
            ElseIf muestrapant = 2 Then
                Dim dc As New clsDatosCliente
                dc.idSolicitud = Val(Request("sol"))
                dc.getDatosSol()
                Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                str_ = ""

                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Exitosa.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                Return
            End If

        Catch ex As Exception
            'Master.MensajeError(mensaje)
        End Try
    End Sub

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
                    'client.Impago = Impago(person_.person.id)
                    Dim evaluacionRiesgo = Riesgo(person_.person.id)
                    client.Riesgo = evaluacionRiesgo.Riesgo
                    client.Impago = evaluacionRiesgo.Impago
                    listBBVANumClient.Add(client)
                End If
            Next
        End If

        Dim ListaClientes As listadoClientes = New listadoClientes()
        ListaClientes.Clientes = listBBVANumClient
        ListaClientes.valido = valido_
        Return ListaClientes
    End Function

    Private Function Riesgo(ByVal num_cliente As String) As evaluacionRiesgo
        'Dim risk As String = ""
        'Dim impago As String = ""
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

    'End Function

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

    Public Class listCostumerClass
        Public name As String
        Public lastName As String
        Public mothersLastName As String
        Public rfc As String
        Public homonimy As String
    End Class

    Public Class listCostumerClass_sinEnie
        Public name As String
        Public lastName As String
        Public mothersLastName As String
        Public rfc As String
        Public homonimy As String
    End Class

    Public Class customerList
        Public customerList As List(Of objetos) = New List(Of objetos)
    End Class

    Public Class membershipDate
        Public membershipDate As String
    End Class

    Public Class objetos
        Public person As person = New person()
    End Class

    Public Class person
        Public id As String
        Public identityDocument As List(Of identityDocument_) = New List(Of identityDocument_)()
        Public legalAddress As legalAddress = New legalAddress()
    End Class

    Public Class identityDocument_
        Public type As type_ = New type_()
        Public number As String = String.Empty
    End Class

    Public Class type_
        Public id As String
        Public name As String
    End Class

    Public Class legalAddress
        Public streetName As String
        Public streetNumber As String
        Public neighborhood As String
        Public city As String
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
    End Class

    Public Class listadoClientes
        Public Clientes As List(Of Cliente) = New List(Of Cliente)()
        Public valido As Boolean
    End Class

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

    Private Function getAntiFraudeRiesgo(ByVal folio_id As String) As DataTable
        Dim dt As DataTable = New DataTable()

        dt.Columns.Add("Cta_BBVA", GetType(String))
        dt.Columns.Add("Nombre_Cliente", GetType(String))
        dt.Columns.Add("RFC", GetType(String))
        dt.Columns.Add("FECHA_NAC", GetType(String))
        dt.Columns.Add("Domicilio", GetType(String))
        dt.Columns.Add("Riesgo", GetType(String))
        dt.Columns.Add("Impago", GetType(String))

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
                Dim IMPAGO As String = reader(8)
                dt.Rows.Add(CtaBBVA, NOMBRE + " " + AP_PAT + " " + AP_MAT, RFC, FECH_NAC, DOMICILIO, Score, IMPAGO)
            Loop
        Catch ex As Exception
            dt = New DataTable()
        End Try
        sqlConnection1.Close()
        Return dt
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

    Public Class riskRegistry
        Public riskRegistry As List(Of contract) = New List(Of contract)()
        Public potentialRisk As potentialRisk = New potentialRisk()
        Public measures As measures = New measures()
    End Class

    Public Class contract

    End Class

    Public Class potentialRisk
        Public totalPotentialRisk As totalPotentialRisk = New totalPotentialRisk()
    End Class

    Public Class totalPotentialRisk
        Public amount As String
    End Class

    Public Class measures
        Public totalAwardedAmount As totalAwardedAmount = New totalAwardedAmount()
    End Class

    Public Class totalAwardedAmount
        Public amount As Double
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
        query.Append("INSERT INTO PDK_TAB_ANTIFRAUDE_RIESGO ( PDK_ID_SECCCERO, Cta_BBVA , Riesgo, Domicilio, Fecha_Open_Count, PreAprovado, Monto_Pre, Impago ) VALUES (")
        query.Append(folio_id + ", ")
        query.Append("'" + Cliente.Cta_BBVA + "', ")
        query.Append("'" + Cliente.Riesgo + "', ")
        query.Append("'" + Cliente.Domicilio + "', ")
        query.Append("'" + Cliente.Fecha_Open_Count + "', ")
        query.Append("'" + Cliente.PreAprovado + "', ")
        query.Append("'" + Cliente.Monto_Pre + "', ")
        query.Append("'" + Cliente.Impago + "' )") 'RQ-PD21-2

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

    Public Class ClienteBBVA_
        Public cliente As Boolean
        Public cve_cliente As String
    End Class

    Public Class evaluacionRiesgo
        Property Riesgo As String = String.Empty
        Property Impago As String = String.Empty
    End Class
End Class


