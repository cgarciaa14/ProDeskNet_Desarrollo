'BUG-PD-47  GVARGAS  23/05/2017 Create Cliente
'BUG-PD-51  GVARGAS  26/05/2017 Update Cliente
'BUG-PD-72  GVARGAS  07/06/2017 Cambio fecha when fecha value default
'BUG-PD-127  GVARGAS  07/06/2017 set Persona juridica by REST
'BUG-PD-142  GVARGAS  12/07/2017 New Cambio Sitvivi, +ingresos
'BUG-PD-164  GVARGAS  18/07/2017 Cambio actualizacion direccion
'BUG-PD-143 GVARGAS 25/07/2017 PopUpRedirect Nuevo
'BUG-PD-195 GVARGAS 22/08/2017 Modiify message when Cliente Indeseable
'BUG-PD-313: DJUAREZ: 27/12/2017: Se evita el avanzar tareas cuando se presiona F5 cuando esta cargando una pagina.
'BUG-PD-373 GVARGAS 01/03/2018 Cancelar tareas Create Costumer Status Credito rechazo por politicas
'BUG-PD-414 GVARGAS 24/04/2018 New Fields Update

Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.BD
Imports ProdeskNet.SN
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.WCF

Partial Class aspx_CreateCustomer
    Inherits System.Web.UI.Page

    Dim BD As New clsManejaBD
    Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()

    Private Sub aspx_CreateCustomer_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'Validacion de Request
            Dim Validate As New clsValidateData
            Dim Url As String = Validate.ValidateRequest(Request)

            If Url <> String.Empty Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", Url, True)
                Exit Sub
            End If
            'Fin validacion de Request

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

        Dim cliente As ClienteBBVA = getClientBBVA(Request("Sol").ToString())
        Dim alta_modificacion As AltaClienteBBVA = New AltaClienteBBVA()

        If (cliente.cliente) Then
            alta_modificacion = ModificacionCliente(Request("Sol").ToString(), cliente.cve_cliente)
        Else
            alta_modificacion = AltaCliente(Request("Sol").ToString())
        End If

        If (alta_modificacion.alta) Then
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
                Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
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
                    If alta_modificacion.mensajeAlta.Contains("PEE1117") Then
                        Dim url As String = CancelaTarea(350, "AVISO CLIENTE EN BASE INDESEABLE")
                        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", url, True)
                    Else
                        asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"), alta_modificacion.mensajeAlta)
                    End If
                End If
            End If
        End If
    End Sub

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

    Public Class ClienteBBVA
        Public cliente As Boolean
        Public cve_cliente As String
    End Class

    Public Class createCustomer
        Public nombre As String '= "KARINA"
        Public priape As String '= "VILLORDO"
        Public segape As String '= "AXA"
        Public rfc As String '= "VIAK900812"
        Public homonim As String '= "XYZ"
        Public fecnaci As String '= "1990-08-12"
        Public codpost As String '= "01000"
        Public numinte As String '= ""
        Public pecnaci As String '= "MEX"
        Public fecingr As String '= "2016-12-30"
        Public codpais As String '= "MEX"
        Public estado As String '= "DF"
        Public ocuphab As String '= " "
        Public sexo As String '= "F"
        Public calle As String '= "AV SANTA ANA"
        Public numexte As String '= "327"
        Public ecalle2 As String '= " "
        Public dfecre As String '= "1991-08-12"
        Public nacion As String '= "MEX"
        Public escolar As String '= " "
        Public centrab As String '= " "
        Public edocivi As String '= "B"
        Public ecalle1 As String '= " "
        Public delmuni As String '= "ALVARO OBREGON"
        Public entnaci As String '= "DF"
        Public colonia As String '= "SAN ANGEL"
        Public puesto As String '= " "
        Public curp As String '= ""
        Public numescr As String '= " "
        Public perjuri As String '= "F32"
        Public cveiden As String '= "P"
        Public numiden As String '= "35712436"
        Public fvenide As String '= "2019-12-30"
        Public ingrcta As Integer '= 0
        Public gtosfij As Integer '= 0
        Public domirec As String '= "N"
        Public actieco As String '= "2T"
        Public situlab As String '= "4"
        Public ingrvar As Integer '= 0
        Public sitvivi As Integer '= 1
        Public actogir As String '= " "
        Public sdomedi As Integer '= 0
        Public ingrnom As Integer '= 0
        Public antempr As Integer '= 0
        Public profesi As String '= "G"
        Public depecon As Integer '= 2
        Public gtosalq As Integer '= 0
        Public ingrotr As Integer '= 0
        Public domnomi As String '= "N"
        Public ingrren As Integer '= 0
        Public apoder1 As String '= " "
        Public valvivi As Integer '= 1000000
        Public gtoship As Integer '= 0
        Public gtospre As Integer '= 1
    End Class

    Public Class createContactInformation
        Public email As String '= "correo3@BBVA.COM"
        Public customerId As String '= "D0077843"
        Public channel As String '= "21"
        Public telephone As List(Of telephone) = New List(Of telephone)()
    End Class

    Public Class telephone
        Public telephoneNumber As Int64 '= 2414173864
        Public cellphoneCompany As cellphoneCompany '= New cellphoneCompany()
        Public typeTelephone As String '= "FIXED"
        Public portabilityIndicator As String '= "P"
    End Class

    Public Class cellphoneCompany
        Public id As String '= "01"
    End Class

    Private Function getInfoCliente(ByVal folio As String) As createCustomer
        Dim alta As createCustomer = New createCustomer()

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_Info_AltaBBVA_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idsolicitud", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                alta.nombre = reader("nombre")
                alta.priape = reader("priape")
                alta.segape = reader("segape")
                alta.rfc = reader("rfc")
                alta.homonim = reader("homonim")
                alta.fecnaci = reader("fecnaci")
                alta.codpost = reader("codpost")
                alta.numinte = reader("numinte")
                alta.pecnaci = reader("pecnaci")
                alta.fecingr = reader("fecingr")
                alta.codpais = reader("codpais")
                alta.estado = reader("estado")
                alta.ocuphab = reader("ocuphab")
                alta.sexo = reader("sexo")
                alta.calle = reader("calle")
                alta.numexte = reader("numexte")
                alta.ecalle2 = reader("ecalle2")
                alta.dfecre = reader("dfecre")
                alta.nacion = reader("nacion")
                alta.escolar = reader("escolar")
                alta.centrab = reader("centrab")
                alta.edocivi = reader("edocivi")
                alta.ecalle1 = reader("ecalle1")
                alta.delmuni = reader("delmuni")
                alta.entnaci = reader("entnaci")
                alta.colonia = reader("colonia")
                alta.puesto = reader("puesto")
                alta.curp = reader("curp")
                alta.numescr = reader("numescr")
                alta.perjuri = reader("perjuri")
                alta.cveiden = reader("cveiden")
                alta.numiden = reader("numiden")
                alta.fvenide = reader("fvenide")
                alta.ingrcta = reader("ingrcta")
                alta.gtosfij = reader("gtosfij")
                alta.domirec = reader("domirec")
                alta.actieco = reader("actieco")
                alta.situlab = reader("situlab")
                alta.ingrvar = reader("ingrvar")
                alta.sitvivi = reader("sitvivi")
                alta.actogir = reader("actogir")
                alta.sdomedi = reader("sdomedi")
                alta.ingrnom = reader("ingrnom")
                alta.antempr = reader("antempr")
                alta.profesi = reader("profesi")
                alta.depecon = reader("depecon")
                alta.gtosalq = reader("gtosalq")
                alta.ingrotr = reader("ingrotr")
                alta.domnomi = reader("domnomi")
                alta.ingrren = reader("ingrren")
                alta.apoder1 = reader("apoder1")
                alta.valvivi = reader("valvivi")
                alta.gtoship = reader("gtoship")
                alta.gtospre = reader("gtospre")
                'alta.gtospre = reader("Ant_Lab()
            Loop
        Catch ex As Exception
            'alta = False
        End Try

        sqlConnection1.Close()
        Return alta
    End Function

    Private Function getInfoCliente_By_REST(ByVal Cva_BBVA As String) As ClienteBBVAInfo
        'Dim updateCustomer As updateCustomer = New updateCustomer()
        Dim GetInfoCliente As ClienteBBVAInfo = New ClienteBBVAInfo()

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonBODY As String = String.Empty
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        'Cva_BBVA = "D0075225"
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("getCustomer").ToString().Replace("CambiarPorCuenta", Cva_BBVA)

        Dim jsonResult As String = rest.ConnectionGet(userID, iv_ticket1, jsonBODY)

        If ((rest.IsError = False) And (jsonResult <> "{}")) Then
            'updateCustomer = serializer.Deserialize(Of updateCustomer)(jsonResult)
            GetInfoCliente = serializer.Deserialize(Of ClienteBBVAInfo)(jsonResult)
        End If

        Return GetInfoCliente
    End Function

    Private Function getInfoContactCliente(ByVal folio As String, ByVal costumerID As String) As createContactInformation
        Dim createContactInformation As createContactInformation = New createContactInformation()

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_InfoContact_AltaBBVA_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                createContactInformation.email = reader("MAIL")
                createContactInformation.customerId = costumerID
                createContactInformation.channel = "21"

                Dim telephoneMovil As telephone = New telephone()
                telephoneMovil.telephoneNumber = reader("MOVIL")
                telephoneMovil.cellphoneCompany = New cellphoneCompany()
                'telephoneMovil.cellphoneCompany.id = "01"
                telephoneMovil.cellphoneCompany.id = reader("COMPANYMOVIL")
                telephoneMovil.typeTelephone = "MOVIL"
                telephoneMovil.portabilityIndicator = "P"

                Dim telephoneFijo As telephone = New telephone()
                telephoneFijo.telephoneNumber = reader("FIJO")
                'telephoneFijo.cellphoneCompany.id = reader("COMPANYFIJO")
                'telephoneFijo.cellphoneCompany.id = "01"
                telephoneFijo.typeTelephone = "FIXED"
                telephoneFijo.portabilityIndicator = "P"

                createContactInformation.telephone.Add(telephoneMovil)
                createContactInformation.telephone.Add(telephoneFijo)
            Loop
        Catch ex As Exception
            'createContactInformation = False
        End Try

        sqlConnection1.Close()
        Return createContactInformation
    End Function

    Private Function getInfoContactCliente_By_REST(ByVal Cve_BBVA As String) As createContactInformation
        Dim createContactInformation As createContactInformation = New createContactInformation()

        Dim jsonBODY As String = String.Empty
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        'Cve_BBVA = "D0075225"
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("getContactInformation").ToString().Replace("CambiarPorCuenta", Cve_BBVA)

        Dim jsonResult As String = rest.ConnectionGet(userID, iv_ticket1, jsonBODY)

        If ((rest.IsError = False) And (jsonResult <> "{}")) Then
            'Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            'Dim riskRegistry As riskRegistry = serializer.Deserialize(Of riskRegistry)(jsonResult)
            'risk = riskRegistry.potentialRisk.totalPotentialRisk.amount
        End If

        Return createContactInformation
    End Function

    Private Function AltaCliente(ByVal folio As String) As AltaClienteBBVA
        Dim alta As AltaClienteBBVA = New AltaClienteBBVA()
        Dim newCtaBBVA As String = String.Empty
        Dim createCustomer As createCustomer = getInfoCliente(folio)

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonBODY As String = serializer.Serialize(createCustomer)

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("createCustomer").ToString()
        rest.buscarHeader("error-code")

        Dim jsonResult As String = rest.ConnectionPost(userID, iv_ticket1, jsonBODY)

        If (rest.IsError = False) Then
            Dim NewClienteBBVA As NewClienteBBVA = serializer.Deserialize(Of NewClienteBBVA)(jsonResult)
            newCtaBBVA = NewClienteBBVA.numclie

            If (newCtaBBVA <> "") Then
                alta.alta = True
                updateCteIncredit(folio, newCtaBBVA)

                Dim createContactInformation As createContactInformation = getInfoContactCliente(folio, newCtaBBVA)

                rest.Reset()
                rest.Uri = System.Configuration.ConfigurationManager.AppSettings("createContactInformation").ToString()
                rest.buscarHeader("ResponseWarningCode")
                jsonBODY = serializer.Serialize(createContactInformation)
                jsonBODY = jsonBODY.Replace("null", "{}")

                jsonResult = rest.ConnectionPost(userID, iv_ticket1, jsonBODY)

                If (rest.IsError = False) Then
                    Dim valorHeader = rest.valorHeader
                Else
                    setMensajeError(folio, newCtaBBVA, rest.MensajeError)
                    'alta.alta = False
                    'alta.mensajeAlta = rest.MensajeError
                End If
            End If
        Else
            If (rest.valorHeader.Contains("PEE1117")) Then
                alta.mensajeAlta = rest.valorHeader + " " + rest.MensajeError
            Else
                alta.mensajeAlta = rest.MensajeError
            End If

            setMensajeError(folio, "NO CREATED", alta.mensajeAlta)
            alta.alta = False
        End If

        Return alta
    End Function

    Public Class NewClienteBBVA
        Public numclie As String
    End Class

    Public Class AltaClienteBBVA
        Public alta As Boolean = False
        Public mensajeAlta As String = String.Empty
    End Class

    Private Function ModificacionCliente(ByVal folio As String, ByVal cve_bbva As String) As AltaClienteBBVA
        Dim alta As AltaClienteBBVA = New AltaClienteBBVA()
        Try
            Dim createCustomer As createCustomer = getInfoCliente(folio)
            Dim createContactInformation As createContactInformation = getInfoContactCliente(folio, cve_bbva)
            Dim createCustomer_By_REST As ClienteBBVAInfo = getInfoCliente_By_REST(cve_bbva)
            Dim updateCustomer As updateCustomer = MixInfoCliente(createCustomer, createCustomer_By_REST)

            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim updateCustomerJSON As String = serializer.Serialize(updateCustomer)
            Dim ContactInformationJSON As String = serializer.Serialize(createContactInformation)
            ContactInformationJSON = ContactInformationJSON.Replace("null", "{}")

            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

            rest.Uri = System.Configuration.ConfigurationManager.AppSettings("updateCustomer").ToString()
            Dim jsonResult As String = rest.ConnectionPut(userID, iv_ticket1, updateCustomerJSON)

            If (rest.IsError = False) Then
                'Dim NewClienteBBVA As NewClienteBBVA = serializer.Deserialize(Of NewClienteBBVA)(jsonResult)
                'newCtaBBVA = NewClienteBBVA.numclie

                alta.alta = True

                rest.Reset()
                rest.Uri = System.Configuration.ConfigurationManager.AppSettings("updateContactInformation").ToString()
                'rest.buscarHeader("ResponseWarningCode")

                jsonResult = rest.ConnectionPut(userID, iv_ticket1, ContactInformationJSON)

                If (rest.IsError) Then
                    setMensajeError(folio, cve_bbva, rest.MensajeError)
                    alta.mensajeAlta = rest.MensajeError
                End If
            Else
                alta.alta = False
                alta.mensajeAlta = rest.MensajeError
            End If
        Catch
            alta.alta = False
            alta.mensajeAlta = "Error en la consulta a Servicios."
            setMensajeError(folio, cve_bbva, "Error en la consulta a Servicios.")
        End Try

        Return alta
    End Function

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

    Private Sub setMensajeError(ByVal folio_id As String, ByVal Cta_BBVA As String, ByVal MensajeError As String)
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "set_ErrorAltaMod_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio_id)
            cmd.Parameters.AddWithValue("@Cta_BBVA", Cta_BBVA)
            cmd.Parameters.AddWithValue("@MensajeError", MensajeError)

            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
            Loop
        Catch

        End Try
        sqlConnection1.Close()
    End Sub

    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer, ByVal msg As String)
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
                Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
            ElseIf muestrapant = 2 Then
                'Response.Redirect("../aspx/consultaPanelControl.aspx")
                Dim dc As New clsDatosCliente
                dc.idSolicitud = Val(Request("sol"))
                dc.getDatosSol()
                Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
                str_ = ""
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + msg + "', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
                Return
            End If

        Catch ex As Exception
            'Master.MensajeError(mensaje)
        End Try
    End Sub

    ' Public Class createCustomerNew
    'Public person As persona = New persona()
    'End Class

    '    Public Class persona

    'End Class

    Public Class updateCustomer
        Public id As String
        Public person As persona1 = New persona1()
    End Class

    Public Class persona1
        Public extendedData As extendedData1 = New extendedData1()
        Public identityDocument As List(Of identityDocument1) = New List(Of identityDocument1)()
        Public legalAddress As legalAddress1 = New legalAddress1()
        Public economicData As economicData1 = New economicData1()
        Public country As genericID = New genericID()
    End Class

    Public Class extendedData1
        Public legalGroup As String
        Public maritalStatus As genericID = New genericID()
        Public birthPlace As birthPlaceInfo = New birthPlaceInfo()
    End Class

    Public Class birthPlaceInfo
        Public state As genericID = New genericID()
        Public country As genericID = New genericID()
    End Class

    Public Class identityDocument1
        Public id As String
        Public number As String
        Public expiryDate As String
    End Class

    Public Class legalAddress1
        Public streetName As String
        Public streetNumber As String
        Public zipCode As String
        Public neighborhood As String
        Public city As String
        Public state As genericID = New genericID()
        Public startingResidenceDate As String
        Public country As genericID = New genericID()
        Public ownerShipType As genericID = New genericID()
    End Class

    Public Class genericID
        Public id As String
    End Class

    Public Class economicData1
        Public extendedData As extendedData2 = New extendedData2()
        Public profession As genericID = New genericID()

    End Class

    Public Class extendedData2
        Public iOwnerShip As iOwnerShip1 = New iOwnerShip1()
        Public hireDate As String
        Public bussinessActivity As bussinessActivity1 = New bussinessActivity1()
        Public employmentSituation As String
        Public economicDependants As String
        Public jobSeniority As String

        Public averageIncome As genericMOUNT = New genericMOUNT()
        Public payrollPensionIncome As genericMOUNT = New genericMOUNT()
        Public accountIncome As genericMOUNT = New genericMOUNT()
        Public variableIncome As genericMOUNT = New genericMOUNT()
        Public othersIncome As List(Of genericMOUNT) = New List(Of genericMOUNT)()
        Public mortageExpenditure As genericMOUNT = New genericMOUNT()
        Public fixedExpenditure As genericMOUNT = New genericMOUNT()
        Public rentalExpenditure As genericMOUNT = New genericMOUNT()
        Public loanExpenditure As genericMOUNT = New genericMOUNT()
        Public domiciledPaymentIndicator As String
        Public domiciledReceiptsIndicator As String
        Public economicSpin As economicSpin1 = New economicSpin1()
        Public schoolingLevel As String
    End Class

    Public Class iOwnerShip1
        Public propertyData As propertyData1 = New propertyData1()
    End Class

    Public Class propertyData1
        Public deedNumber As String
        Public attorneyInFact As String
        Public value As genericMOUNT = New genericMOUNT()
    End Class

    Public Class genericMOUNT
        Public amount As String
    End Class

    Public Class bussinessActivity1
        Public name As String
    End Class

    Public Class economicSpin1
        Public name As String
    End Class
    '----------------------------------------------------------------------------------------------------------
    '----------------------------------------------------------------------------------------------------------
    '----------------------------------------------------------------------------------------------------------
    '----------------------------------------------------------------------------------------------------------
    '----------------------------------------------------------------------------------------------------------
    '----------------------------------------------------------------------------------------------------------
    '----------------------------------------------------------------------------------------------------------
    Public Class ClienteBBVAInfo
        Public person As person_ = New person_()
        Public knowYourCustomer As knowYourCustomer_ = New knowYourCustomer_()
        Public membershipDate As String
        Public branch As branch_ = New branch_()
        Public operationalResponsabilityCenter As genericID = New genericID()
        Public salesPracticeStatus As salesPracticeStatus_ = New salesPracticeStatus_()
        Public relationshipGFP As Boolean
        Public customerInformationResult As customerInformationResult_ = New customerInformationResult_()
    End Class

    Public Class person_
        Public id As String
        Public identityDocument As List(Of identityDocument_) = New List(Of identityDocument_)()
        Public type As type_ = New type_()
        Public name As String
        Public lastName As String
        Public mothersLastName As String
        Public companyInformation As companyInformation_ = New companyInformation_()
        Public bank As bank_ = New bank_()
        Public status As status_ = New status_()
        Public relationBank As relationBank_ = New relationBank_()
        Public privileged As genericID = New genericID()
        Public birthDate As String
        Public contract As contract_ = New contract_()
        Public segment As segment_ = New segment_()
        Public user As user_ = New user_()
        Public economicData As economicData_ = New economicData_() 'Continue Here
        Public legalAddress As legalAddress_ = New legalAddress_()
        Public extendedData As extendedData_ = New extendedData_()
        Public classification As classification_ = New classification_()
        Public country As country_ = New country_()
        Public contactsInformation As List(Of contactsInformation_) = New List(Of contactsInformation_)()
    End Class

    Public Class identityDocument_
        Public id As String
        Public type As type_ = New type_()
        Public number As String
        Public digitId As String
        Public secId As String
        Public expiryDate As String
    End Class

    Public Class economicData_
        Public job As genericID = New genericID()
        Public ocupationType As genericName = New genericName()
        Public profession As genericName = New genericName()
        Public sectorType As genericName = New genericName()
        Public extendedData As extendedData_2 = New extendedData_2()
    End Class

    Public Class extendedData_2
        Public bussinessActivity As knowYourCustomer_ = New knowYourCustomer_()
        Public hireDate As String
        Public economicDependants As Integer
        Public jobSeniority As Integer
        Public payrollPensionIncome As genericMOUNT_Int = New genericMOUNT_Int()
        Public accountIncome As genericMOUNT_Int = New genericMOUNT_Int()
        Public variableIncome As genericMOUNT_Int = New genericMOUNT_Int()
        Public othersIncome As List(Of genericMOUNT_Int) = New List(Of genericMOUNT_Int)()
        Public mortageExpenditure As genericMOUNT_Int = New genericMOUNT_Int()
        Public fixedExpenditure As genericMOUNT_Int = New genericMOUNT_Int()
        Public rentalExpenditure As genericMOUNT_Int = New genericMOUNT_Int()
        Public loanExpenditure As genericMOUNT_Int = New genericMOUNT_Int()
        Public averageIncome As genericMOUNT_Int = New genericMOUNT_Int()
        Public employmentSituation As String
        Public iOwnerShip As iOwnerShip_ = New iOwnerShip_()
        Public economicSpin As genericName = New genericName()
    End Class

    Public Class genericMOUNT_Int
        Public amount As Double
    End Class

    Public Class iOwnerShip_
        Public propertyData As propertyData_ = New propertyData_()
    End Class

    Public Class propertyData_
        Public value As genericMOUNT_Int = New genericMOUNT_Int()
    End Class

    'Public Class value_1
    '    Public amount As genericMOUNT_Int = New genericMOUNT_Int()
    'End Class

    Public Class genericName
        Public name As String
    End Class

    Public Class legalAddress_
        Public id As String
        Public type As genericID = New genericID()
        Public startingResidenceDate As String
        Public streetType As knowYourCustomer_ = New knowYourCustomer_()
        Public streetName As String
        Public streetNumber As String
        Public typeHouse As knowYourCustomer_ = New knowYourCustomer_()
        Public typeSettlement As knowYourCustomer_ = New knowYourCustomer_()
        Public neighborhood As String
        'Public additionalInformation  [null, null],
        Public city As String
        Public zipCode As String
        Public state As genericIdName = New genericIdName()
        Public country As genericIdName = New genericIdName()
        Public ownerShipType As genericIdName = New genericIdName()
    End Class

    Public Class extendedData_
        Public maritalStatus As genericIdName = New genericIdName()
        Public sex As genericIdName = New genericIdName()
        Public prefix As genericID = New genericID()
        Public birthPlace As birthPlace_ = New birthPlace_()
        Public legalGroup As String
    End Class

    Public Class birthPlace_
        Public state As genericID = New genericID()
        Public country As genericID = New genericID()
    End Class

    Public Class genericIdName
        Public id As String
        Public name As String
    End Class

    Public Class classification_
        Public globalSegmentation As globalSegmentation_ = New globalSegmentation_()
    End Class

    Public Class globalSegmentation_
        Public name As genericID = New genericID()
        Public value_12 As value_12 = New value_12()
    End Class

    Public Class value_12
    End Class

    Public Class country_
        Public id As String
        Public name As String
    End Class

    Public Class contactsInformation_
        Public id As String
        Public type As type_ = New type_()
        Public name As String
        Public phoneCode As String
    End Class

    Public Class type_
        Public id As String
        Public name As String
    End Class

    Public Class companyInformation_
        Public legalForm As legalForm_ = New legalForm_()
    End Class

    Public Class legalForm_
    End Class

    Public Class bank_
    End Class

    Public Class status_
        Public id As String
        Public name As String
    End Class

    Public Class relationBank_
        Public id As String
        Public name As String
    End Class

    Public Class contract_
        Public product As product_ = New product_()
    End Class

    Public Class product_
        Public card As card_ = New card_()
    End Class

    Public Class card_
        Public cardBase As knowYourCustomer_ = New knowYourCustomer_()
        Public productBase As knowYourCustomer_ = New knowYourCustomer_()
        Public card As knowYourCustomer_ = New knowYourCustomer_()
    End Class

    Public Class segment_
        Public name As genericID = New genericID()
        Public value As value_ = New value_()
    End Class

    Public Class value_
        Public id As String
        Public name As String
    End Class

    Public Class user_
        Public idTerminal As String
        Public modifyHour As String
        Public id As String
    End Class

    Public Class knowYourCustomer_
    End Class

    Public Class branch_
        Public name As String
    End Class

    Public Class salesPracticeStatus_
    End Class

    Public Class customerInformationResult_
        Public reserveIndicator As String
        Public customerIndicator As String
    End Class

    Private Function MixInfoCliente(ByVal createCustomer As createCustomer, ByVal createCustomer_By_REST As ClienteBBVAInfo) As updateCustomer
        Dim createCustomerNewField As createCustomerNewField = getInfoClienteNewFields(Request("Sol").ToString())

        Dim sit_lab_R As String = createCustomer_By_REST.person.economicData.extendedData.employmentSituation
        Dim sit_lab_P As String = createCustomer.situlab.ToString()

        Dim ant_lab As String = getInfoCliente_Extra(Request("Sol").ToString())
        Dim ant_lab_date As String = String.Empty

        If (ant_lab <> "") Then
            If (ant_lab = "0") Then
                ant_lab = "1"
            End If
            Dim thisYear As Date = Date.Today
            Dim anio As Integer = Year(thisYear)

            ant_lab_date = (anio - Int32.Parse(ant_lab)).ToString() + "-12-31"
        End If

        If ((sit_lab_R <> sit_lab_P) Or (sit_lab_R = sit_lab_P)) Then
            createCustomer_By_REST.person.economicData.extendedData.employmentSituation = createCustomer.situlab.ToString()
            createCustomer_By_REST.person.economicData.extendedData.payrollPensionIncome.amount = createCustomer.ingrnom.ToString()
            createCustomer_By_REST.person.economicData.extendedData.variableIncome.amount = createCustomer.ingrvar.ToString()

            If createCustomer.situlab.ToString() = "3" Then
                createCustomer_By_REST.person.economicData.extendedData.payrollPensionIncome.amount = "0.00"
            ElseIf createCustomer.situlab.ToString() = "4" Then
                createCustomer_By_REST.person.economicData.extendedData.payrollPensionIncome.amount = "0.00"
                ant_lab = "0"
                ant_lab_date = "0001-01-01"
                'ElseIf createCustomer.situlab.ToString() = "5" Then
                'createCustomer_By_REST.person.economicData.extendedData.payrollPensionIncome.amount = "0.00"
                'createCustomer_By_REST.person.economicData.extendedData.accountIncome.amount = createCustomer.ingrcta.ToString()
            End If
        End If

        Dim sit_viv_R As String

        If createCustomer_By_REST.person.legalAddress.ownerShipType.id Is Nothing Then
            sit_viv_R = "0"
            createCustomer_By_REST.person.legalAddress.ownerShipType.id = "1"
        Else
            sit_viv_R = createCustomer_By_REST.person.legalAddress.ownerShipType.id.ToString()
        End If

        Dim sit_viv_P As String = createCustomer.sitvivi.ToString()

        If (sit_viv_R <> sit_viv_P) Then
            createCustomer_By_REST.person.legalAddress.ownerShipType.id = createCustomer.sitvivi.ToString()

            createCustomer_By_REST.person.economicData.extendedData.mortageExpenditure.amount = 0.0
            createCustomer_By_REST.person.economicData.extendedData.rentalExpenditure.amount = 0.0

            If createCustomer.sitvivi.ToString() = "2" Then
                createCustomer_By_REST.person.economicData.extendedData.mortageExpenditure.amount = createCustomer.gtoship
                createCustomer_By_REST.person.economicData.extendedData.rentalExpenditure.amount = 0.0
            ElseIf createCustomer.sitvivi.ToString() = "3" Then
                createCustomer_By_REST.person.economicData.extendedData.mortageExpenditure.amount = 0.0
                createCustomer_By_REST.person.economicData.extendedData.rentalExpenditure.amount = createCustomer.gtosalq
            End If

        End If


        Dim updateCustomer As updateCustomer = New updateCustomer()

        updateCustomer.id = createCustomer_By_REST.person.id
        updateCustomer.person.country.id = createCustomerNewField.PAIS_NACIMIENTO
        updateCustomer.person.extendedData.legalGroup = createCustomer_By_REST.person.extendedData.legalGroup 'createCustomer.perjuri

        updateCustomer.person.extendedData.birthPlace.state.id = createCustomer.entnaci
        updateCustomer.person.extendedData.birthPlace.country.id = createCustomer.pecnaci
        updateCustomer.person.extendedData.maritalStatus.id = createCustomer.edocivi

        Dim identityDocument As identityDocument1 = New identityDocument1()
        identityDocument.id = createCustomer.cveiden
        identityDocument.number = createCustomer.numiden
        identityDocument.expiryDate = createCustomer.fvenide
        updateCustomer.person.identityDocument.Add(identityDocument)

        'HERE
        identityDocument = New identityDocument1()
        identityDocument.id = createCustomerNewField.ID_DOCUMENTO
        identityDocument.number = createCustomer.curp
        identityDocument.expiryDate = createCustomerNewField.VENCIMIENTO_DOCUMENTO
        updateCustomer.person.identityDocument.Add(identityDocument)


        updateCustomer.person.legalAddress.streetName = createCustomer_By_REST.person.legalAddress.streetName 'createCustomer.calle
        updateCustomer.person.legalAddress.streetNumber = createCustomer_By_REST.person.legalAddress.streetNumber 'createCustomer.numexte
        updateCustomer.person.legalAddress.zipCode = createCustomer_By_REST.person.legalAddress.zipCode 'createCustomer.codpost
        updateCustomer.person.legalAddress.neighborhood = createCustomer_By_REST.person.legalAddress.neighborhood 'createCustomer.colonia
        updateCustomer.person.legalAddress.city = createCustomer_By_REST.person.legalAddress.city 'createCustomer.delmuni
        updateCustomer.person.legalAddress.state.id = createCustomer_By_REST.person.legalAddress.state.id 'createCustomer.estado

        Dim fecha = getInfoCliente_Extra_Anios(Request("Sol").ToString())

        'If (createCustomer_By_REST.person.legalAddress.startingResidenceDate.Substring(0, 10) = "0001-01-01") Then
        '    fecha = getInfoCliente_Extra_Anios(Request("Sol").ToString())
        'Else
        '    fecha = createCustomer_By_REST.person.legalAddress.startingResidenceDate.Substring(0, 10)
        'End If

        updateCustomer.person.legalAddress.startingResidenceDate = fecha

        updateCustomer.person.legalAddress.country.id = createCustomer.codpais
        updateCustomer.person.legalAddress.ownerShipType.id = createCustomer_By_REST.person.legalAddress.ownerShipType.id  'VERIFICAR CON BBVA

        updateCustomer.person.economicData.extendedData.iOwnerShip.propertyData.deedNumber = ""
        updateCustomer.person.economicData.extendedData.iOwnerShip.propertyData.attorneyInFact = ""
        updateCustomer.person.economicData.extendedData.iOwnerShip.propertyData.value.amount = createCustomer_By_REST.person.economicData.extendedData.iOwnerShip.propertyData.value.amount.ToString()

        updateCustomer.person.economicData.extendedData.hireDate = ant_lab_date
        updateCustomer.person.economicData.extendedData.bussinessActivity.name = createCustomer.actieco '"1A" 'definir

        updateCustomer.person.economicData.extendedData.employmentSituation = createCustomer_By_REST.person.economicData.extendedData.employmentSituation 'createCustomer.situlab
        updateCustomer.person.economicData.extendedData.economicDependants = createCustomer.depecon
        updateCustomer.person.economicData.extendedData.jobSeniority = ant_lab 'createCustomer_By_REST.person.economicData.extendedData.jobSeniority 'ant_lab 'VERIFICAR POR QUE NO SE ENVIA

        updateCustomer.person.economicData.extendedData.schoolingLevel = createCustomer.escolar

        updateCustomer.person.economicData.extendedData.averageIncome.amount = createCustomer_By_REST.person.economicData.extendedData.averageIncome.amount
        updateCustomer.person.economicData.extendedData.payrollPensionIncome.amount = createCustomer_By_REST.person.economicData.extendedData.payrollPensionIncome.amount 'createCustomer.ingrcta 'fijos 'VERIFICAR POR QUE NO SE ENVIA 
        updateCustomer.person.economicData.extendedData.accountIncome.amount = createCustomer.ingrcta 'createCustomer_By_REST.person.economicData.extendedData.accountIncome.amount
        updateCustomer.person.economicData.extendedData.variableIncome.amount = createCustomer_By_REST.person.economicData.extendedData.variableIncome.amount 'createCustomer.ingrvar 'variables

        For Each other_inc As genericMOUNT_Int In createCustomer_By_REST.person.economicData.extendedData.othersIncome
            Dim other_ As genericMOUNT = New genericMOUNT()
            other_.amount = other_inc.amount.ToString()
            updateCustomer.person.economicData.extendedData.othersIncome.Add(other_)
        Next

        'updateCustomer.person.economicData.extendedData.othersIncome.Add(other_2.ToString())

        updateCustomer.person.economicData.extendedData.mortageExpenditure.amount = createCustomer_By_REST.person.economicData.extendedData.mortageExpenditure.amount
        updateCustomer.person.economicData.extendedData.fixedExpenditure.amount = createCustomer_By_REST.person.economicData.extendedData.fixedExpenditure.amount
        updateCustomer.person.economicData.extendedData.rentalExpenditure.amount = createCustomer_By_REST.person.economicData.extendedData.rentalExpenditure.amount
        updateCustomer.person.economicData.extendedData.loanExpenditure.amount = createCustomer_By_REST.person.economicData.extendedData.loanExpenditure.amount

        updateCustomer.person.economicData.extendedData.domiciledPaymentIndicator = "N" ' "" Verificar
        updateCustomer.person.economicData.extendedData.domiciledReceiptsIndicator = "N" ' "" Verificar

        updateCustomer.person.economicData.extendedData.economicSpin.name = createCustomer_By_REST.person.economicData.extendedData.economicSpin.name

        updateCustomer.person.economicData.profession.id = createCustomer.profesi

        Return updateCustomer
    End Function

    Private Function getInfoCliente_Extra(ByVal folio As String) As String
        Dim ant_lab As String = String.Empty

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_Info_AltaBBVA_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idsolicitud", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                ant_lab = reader("Ant_Lab")
            Loop
        Catch ex As Exception
            'alta = False
        End Try

        sqlConnection1.Close()
        Return ant_lab
    End Function

    Private Function getInfoCliente_Extra_Anios(ByVal folio As String) As String
        Dim Anios_Residir As Integer = 0

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_Info_AltaBBVA_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idsolicitud", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                Anios_Residir = reader("Anios_Residir")
            Loop
        Catch ex As Exception
            'alta = False
        End Try

        sqlConnection1.Close()

        Dim thisYear As Date = Date.Today
        Dim anio As Integer = Year(thisYear)

        Dim strAño As String = (anio - Anios_Residir).ToString() + "-12-31"
        Return strAño
    End Function

    Private Function CancelaTarea(ByVal status_credito As Integer, ByVal message As String) As String
        Dim path As String = String.Empty

        Dim objCancela As New clsSolicitudes(0)
        objCancela.PDK_ID_SOLICITUD = Request("Sol").ToString()
        objCancela.Estatus = 42
        objCancela.PDK_CLAVE_USUARIO = Session("IdUsua")
        objCancela.PDK_ID_PANTALLA = Request("idPantalla").ToString()
        objCancela.Estatus_Cred = status_credito
        objCancela.ManejaTarea(6)

        Dim dc As New clsDatosCliente
        dc.idSolicitud = Val(Request("sol"))
        dc.getDatosSol()

        Dim str As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
        path = "PopUpLetreroRedirect('" + message + "', '../aspx/consultaPanelControl.aspx" + str + "');"

        Return path
    End Function

    Private Function getInfoClienteNewFields(ByVal folio As String) As createCustomerNewField
        Dim alta As createCustomerNewField = New createCustomerNewField()

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "get_Info_AltaBBVA_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idsolicitud", folio)
            cmd.Parameters.AddWithValue("@EXTENDED", 1)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                alta.ID_DOCUMENTO = reader("ID_DOCUMENTO")
                alta.VENCIMIENTO_DOCUMENTO = reader("VENCIMIENTO_DOCUMENTO")
                alta.PAIS_NACIMIENTO = reader("PAIS_NACIMIENTO")
            Loop
        Catch ex As Exception
        End Try

        sqlConnection1.Close()
        Return alta
    End Function

    Public Class createCustomerNewField
        Public ID_DOCUMENTO As String
        Public VENCIMIENTO_DOCUMENTO As String
        Public PAIS_NACIMIENTO As String
    End Class
End Class
