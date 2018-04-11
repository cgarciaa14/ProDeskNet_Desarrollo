Imports ProdeskNet.SN
Imports System.Data
Imports ProdeskNet.Catalogos
Imports System.Data.SqlClient
Imports ProdeskNet.BD
Imports ProdeskNet.WCF

'RQADM-38:RHERNANDEZ:05/05/17: Se crea pantalla para la validacion si el cliente esta dado de alta en expediente unico
'CAMBIO URGENTE JRHM
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
'BUG-PD-201 GVARGAS 25/08/2017 Cambio en busqueda de cliente
'BBV-P-423 RQ-PD-17 10 GVARGAS 30/01/2018 Ajustes flujos
'BUG-PD-313: DJUAREZ: 27/12/2017: Se evita el avanzar tareas cuando se presiona F5 cuando esta cargando una pagina.
'BBV-P-423 RQ-PD-17 10 GVARGAS 30/01/2018 Ajustes flujos
'BUG-PD-388: DJUAREZ: 09/03/2018: Se realiza merge

Partial Class aspx_PantBusqClientIncredit
    Inherits System.Web.UI.Page
    Dim StrErr As String = String.Empty
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim idPant As Integer 'CAMBIO LINEA JRHM

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'Validacion de Request
            Dim Validate As New clsValidateData
            Dim Url As String = Validate.ValidateRequest(Request)

            If Url <> String.Empty Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", url, True)
                Exit Sub
            End If
            'Fin validacion de Request

            Dim dsres As New DataSet
            Dim clsdatosclient As New clsCuestionarioIMAX
            Dim objpant As New ProdeskNet.SN.clsPantallas()
            Dim dts As New DataSet()
            idPant = Request("idPantalla") 'JRHM CAMBIO
            clsdatosclient._ID_SOLICITUD = Request("sol")
            dsres = clsdatosclient.GetDatosClient(1)
            If clsdatosclient.StrError <> "" Then
                Master.MensajeError(clsdatosclient.StrError)
                Exit Sub
            End If
            Dim id_client As String = String.Empty
            id_client = ValidaClienteBBVA_(dsres.Tables(0).Rows(0).Item("NOMBRE1").ToString, dsres.Tables(0).Rows(0).Item("NOMBRE2").ToString, dsres.Tables(0).Rows(0).Item("APELLIDO_PATERNO").ToString, dsres.Tables(0).Rows(0).Item("APELLIDO_MATERNO").ToString, dsres.Tables(0).Rows(0).Item("RFC").ToString, dsres.Tables(0).Rows(0).Item("HOMOCLAVE").ToString, dsres.Tables(0).Rows(0).Item("CP").ToString, dsres.Tables(0).Rows(0).Item("FECHA_NAC").ToString)
            If clsdatosclient.StrError <> String.Empty Then
                Master.MensajeError(clsdatosclient.StrError)
                Exit Sub
            End If
            If id_client <> "" Then
                clsdatosclient._ID_INCREDIT = id_client
                dsres = clsdatosclient.GetDatosClient(2)
                If clsdatosclient.StrError <> String.Empty Then
                    Master.MensajeError(clsdatosclient.StrError)
                    Exit Sub
                End If
            End If
            Dim ds_siguienteTarea As DataSet
            ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & idPant.ToString)


            dts = objpant.CargaPantallas(idPant)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Then
                        asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))
                    End If
                End If
            End If
        End If
    End Sub
    Private Function ValidaClienteBBVA(nombre1 As String, nombre2 As String, Apaterno As String, Amaterno As String, rfc As String, homoclave As String, cp As String, fechanac As String) As String
        ValidaClienteBBVA = ""
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        Dim fecha As Date
        fecha = fechanac
        Dim FechaN = fecha.ToString("yyyy-MM-dd")
        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("Customer") + "?$filter=(customerName==" + nombre1 + IIf(nombre2 <> "", " " + nombre2, "") + ",customerLastName==" + Apaterno + IIf(Amaterno <> "", ",customerMotherLastName==" + Amaterno, "") + ",federalTaxpayerRegistry==" + rfc + IIf(homoclave <> "", ",homonimia==" + homoclave, "") + ",postalCode==" + cp + ",birthDate==" + FechaN + ")&$fields=id,person(id,segment)"
        'restGT.consumerID = "10000004"
        Dim respuestaBC As String = restGT.ConnectionGet(userID, iv_ticket1, String.Empty)

        Dim serializerBC As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jresultBC = serializerBC.Deserialize(Of Customer)(respuestaBC)

        ''Error
        Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim alert = srrSerialer.Deserialize(Of msjerr)(respuestaBC)
        If restGT.IsError Then
            If restGT.MensajeError <> "" Then
                StrErr = "Error WS: " + restGT.MensajeError
            Else
                StrErr = "Error de WS - " + alert.message + " Estatus: " + alert.status + "."
            End If

        End If
        Dim Cliente_BBVA As String = jresultBC.Person.id
        If (Cliente_BBVA <> "") Then
            ValidaClienteBBVA = Cliente_BBVA
        End If
        Return ValidaClienteBBVA
    End Function

    Private Function ValidaClienteBBVA_(nombre1 As String, nombre2 As String, Apaterno As String, Amaterno As String, rfc As String, homoclave As String, cp As String, fechanac As String) As String
        Dim cveCliente As String = String.Empty

        Dim listCostumerClass As listCostumerClass = New listCostumerClass()

        Dim solicitante_info As List(Of String) = getSolicitante(Request.QueryString("sol").ToString())
        listCostumerClass.name = solicitante_info(0)
        listCostumerClass.lastName = solicitante_info(1)
        listCostumerClass.mothersLastName = solicitante_info(2)
        listCostumerClass.rfc = solicitante_info(3)
        listCostumerClass.homonimy = solicitante_info(5)

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonBODY As String = String.Empty
        jsonBODY = serializer.Serialize(listCostumerClass)

        Dim rfc_ As String = solicitante_info(3)

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("CostumersWS").ToString()

        Dim jsonResult As String = rest.ConnectionPost(userID, iv_ticket1, jsonBODY)

        If ((rest.IsError = False) And (rest.StatusHTTP = "200")) Then
            Dim customerList As customerList = serializer.Deserialize(Of customerList)(jsonResult)

            For Each person_ As objetos In customerList.customerList
                Dim bandera_RFC As Boolean = False

                For Each Document As identityDocument_ In person_.person.identityDocument
                    If ((Document.type.name = "RFC") And (Document.number.IndexOf(solicitante_info(3).ToUpper()) <> -1)) Then
                        bandera_RFC = True
                    End If
                Next

                If ((bandera_RFC) And (person_.person.id <> "")) Then
                    cveCliente = person_.person.id
                    Exit For
                End If
            Next
        End If

        Return cveCliente
    End Function

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
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("sol"))
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

            dslink = objtarea.SiguienteTarea(Val(Request("sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("sol")))
            Dim dc As New clsDatosCliente
            dc.idSolicitud = Request("sol")
            dc.getDatosSol()

            Dim strLocation As String = String.Empty

            If muestrapant = 0 Then
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usu")).ToString)
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('', '" + strLocation + "');", True)
            ElseIf muestrapant = 2 Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '" + strLocation + "');", True)
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub

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
    End Class
End Class

Public Class Customer
    Public Person As Person = New Person

End Class
Public Class Person
    Public id As String
    Public segment As segment = New segment
End Class
Public Class segment
    Public name As name = New name
End Class
Public Class name
    Public id As String
End Class
Public Class msjerr
    Public message As String
    Public status As String
End Class
