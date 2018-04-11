Imports System.Data
Imports ProdeskNet.Catalogos
Imports ProdeskNet.SN
Imports ProdeskNet.Configurcion

Imports ProdeskNet.BD
Imports ProdeskNet.WCF
'Tracker:INC-B-2019:JDRA:Regresar
'BBV-P-423: BUG-PD-52: ERODRIGUEZ: 17/05/2017 Modificaciòn para nos mostrar mensajes y deshabilitar botones validar y procesar
'BBV-P-423: BUG-PD-62: ERODRIGUEZ: 01/06/2017 deshabilitar boton validar-->
'BUG-PD-107: ERODRIGUEZ: 20/06/2017 Se agrego función para obtener la idquote de la tabla cotseguro columna SEG_DS_NUMCOTIZACION; Se valido y guardo en el servcio de cuestionario el cuestionario asociado en caso de estar guardado.
'BUG-PD-224: RHERNANDEZ: 03/10/17: Se agrega redirect de tareas automaticas al ejecutar val negocio
'BUG-PD-235: RHERNANDEZ: 16/10/17: Se bloquea el boton al procesar la tarea, solo en caso de error lo habilita
'BUG-PD-388: DJUAREZ: Modificar URL de respuesta para corregir F5 en tarea manual
Public Class altaCotizacion
    Inherits System.Web.UI.Page
    Dim BD As New ProdeskNet.BD.clsManejaBD
    'Dim js As New ProdeskNet.Principal
    Dim es As New ProdeskNet.Catalogos.clsStatusSolicitud

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Validacion de Request
        Try
            If Request("Enable") = 0 Then
                Dim Validate As New clsValidateData
                Dim Url As String = Validate.ValidateRequest(Request)

                If Url <> String.Empty Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", Url, True)
                    Exit Sub
                End If
            End If
        Catch

        End Try
        'Fin validacion de Request

        Dim dsNom As DataSet
        Dim dsTareaActual As New DataSet
        Dim intEnable As Integer = 0
        Me.lblSolicitud.Text = Request.QueryString("sol")
        hdnIdFolio.Value = Request.QueryString("sol")
        hdnIdPantalla.Value = Request.QueryString("idpantalla")
        hdnUsua.Value = Session("IdUsua")
        If Session("Regresar") Is Nothing Then
            Session("Regresar") = Request.UrlReferrer.LocalPath
            hdRutaEntrada.Value = Request.UrlReferrer.LocalPath
        End If

        hdRutaEntrada.Value = Session("Regresar")

        Try
            dsNom = BD.EjecutarQuery("EXEC sp_GenerarRura " & hdnIdFolio.Value & "," & hdnIdPantalla.Value & "," & hdnUsua.Value & "")
            If dsNom.Tables(0).Rows.Count > 0 AndAlso dsNom.Tables.Count > 0 Then
                hdnResultado.Value = dsNom.Tables(0).Rows(0).Item("RUTA")
                hdnResultado1.Value = dsNom.Tables(1).Rows(0).Item("RUTA2")
                hdnResultado2.Value = dsNom.Tables(2).Rows(0).Item("RUTA3")
            End If
        Catch ex As Exception
            hdnResultado.Value = Session("Regresar")
        End Try

        Try
            intEnable = CInt(Request.QueryString("Enable"))

        Catch ex As Exception
            intEnable = 0
        End Try

        If intEnable = 1 Then
            cmbGuardar.Attributes.Add("style", "display:none;")
            'btnValidar.Attributes.Add("style", "display:none;")
            btn_validar.Visible = False

            btnCancelar.Attributes.Add("style", "display:none;")

        End If
        dsNom = BD.EjecutarQuery("select isnull(NOMBRE1, '') + ' ' + isnull(NOMBRE2, '') + ' ' + isnull(APELLIDO_PATERNO, '') + ' ' + isnull(APELLIDO_MATERNO, '') from pdk_tab_solicitante where pdk_id_secccero = " & Me.lblSolicitud.Text)

        If Not IsDBNull(dsNom) Then
            If dsNom.Tables(0).Rows.Count <= 0 Then
                Me.lblCliente.Text = "USUARIO NO EXISTE"
            Else
                Me.lblCliente.Text = dsNom.Tables(0).Rows(0)(0)
            End If
        End If


        Try
            Dim ds_estatus_cot As New DataSet
            ds_estatus_cot = BD.EjecutarQuery("	select pios.PDK_OPE_STATUS_TAREA from PDK_OPE_SOLICITUD pios inner join (select PDK_ID_SOLICITUD, max(PDK_ID_OPE_SOLICITUD) as maxideopesol from PDK_OPE_SOLICITUD group by PDK_ID_SOLICITUD) groupedtt on pios.PDK_ID_SOLICITUD = groupedtt.PDK_ID_SOLICITUD and pios.PDK_ID_OPE_SOLICITUD = groupedtt.maxideopesol and pios.PDK_ID_SOLICITUD = " & Request.QueryString("sol"))

            'ds_estatus_cot = BD.EjecutarQuery("SELECT  PDK_OPE_STATUS_TAREA FROM PDK_OPE_SOLICITUD where PDK_ID_SOLICITUD = " & Request.QueryString("sol"))
            If Not IsDBNull(ds_estatus_cot) Then
                If (ds_estatus_cot.Tables(0).Rows.Count <= 0) Or (ds_estatus_cot.Tables(0).Rows(0)(0) <> 40) Then
                    hdnEst_Cot.Value = True
                Else : hdnEst_Cot.Value = False
                End If

            End If
        Catch ex As Exception

        End Try

        Try
            If Not IsPostBack Then
                es.getStatusSol(Request.QueryString("sol"))
                Me.lblStCredito.Text = es.PStCredito
                Me.lblStDocumento.Text = es.PStDocumento
                Dim ds As New DataSet
                ds = BD.EjecutarQuery("SELECT NUM_COTIZACION FROM PDK_TAB_DATOS_SOLICITANTE where PDK_ID_SECCCERO = " & Request.QueryString("sol"))
                If Not IsDBNull(ds) Then
                    If ds.Tables(0).Rows(0)(0).ToString <> "" Then
                        Me.hdnCotizacion.Value = ds.Tables(0).Rows(0)(0)
                        txtCotizacion.Value = ds.Tables(0).Rows(0)(0)
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click

        'Response.Redirect("./consultaPantalla.aspx")
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)

    End Sub

    Protected Sub btn_validar_Click(sender As Object, e As EventArgs)
        Dim dsresult As DataSet = New DataSet()

        Dim cot As String = txtCotizacion.Value.ToString()
        Dim sol As String = lblSolicitud.Text.ToString()

        If (cot <> "") Then
            If (txtCotizacion.Value <> "") Then
                Dim coti As Integer
                Dim esentero As Boolean = Int64.TryParse(cot, coti)
                If (esentero) Then
                    If (TieneCuestionario(sol)) Then
                        If GuardaWS() Then
                            If ValidaWS() Then
                                'Dim msj = "Guardo y valido cuestionario"
                            End If
                        End If
                    End If
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "solcot", "fnValidaCot();", True)
                    ''dsresult = BD.EjecutarQuery("EXEC spValidaCot " & cot & "," & sol & "")
                    'btn_validar.Visible = False
                Else
                    'ClientScript.RegisterClientScriptBlock(Me.GetType, "MensajeError", "<script type='text/javascript'> alert('Debes ingresar un numero de cotización valido" + ".');</script>", True)
                End If
            End If

        End If

    End Sub

    Function TieneCuestionario(idsol As Integer) As Boolean
        Dim dsCuestionario As DataSet
        Dim objDatosCuestionario As New clsSolicitudes(0)
        objDatosCuestionario.PDK_ID_SOLICITUD = idsol
        dsCuestionario = objDatosCuestionario.ConsultaCuestionarios(1)

        If (dsCuestionario.Tables(0).Rows.Count > 0) Then
            Session("TABLA") = dsCuestionario
            Return True
        End If
        Return False
    End Function

    Public Class Cuestionario
        Public header As headerWS = New headerWS()
        Public quote As quote = New quote()

    End Class
    Public Class Cuestionario2
        Public header As headerWS = New headerWS()
        Public quote As quote = New quote()
        Public questionnaire As questionnaire = New questionnaire()
    End Class
    Public Class headerWS
        Public aapType As String
        Public dateRequest As String
        Public channel As String
        Public subChannel As String
        Public managementUnit As String
        Public branchOffice As String
        Public user As String
        Public idSession As String
        Public idRequest As String
        Public dateConsumerInvocation As String
    End Class
    Public Class quote
        Public idQuote As String
    End Class
    Public Class Respuesta
        Public question As New List(Of question)
    End Class
    Public Class question
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
        Public type As String
        Public answers As New List(Of answers)
    End Class
    Public Class questionRespuesta
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
        Public answerQuestion As answerQuestion = New answerQuestion()

    End Class
    Public Class catalogItemBase
        Public id As String
        Public name As String
    End Class
    Public Class answerQuestion
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class
    Public Class answers
        Public catalogItemBase As catalogItemBase = New catalogItemBase()
    End Class
    Public Class msjerr
        Public message As String
        Public status As String
    End Class
    Public Class questionnaire
        Public questionRespuesta As New List(Of questionRespuesta)
    End Class

    Public Function GuardaWS() As Boolean
        GuardaWS = False
        Dim Cuestionario As Cuestionario2 = New Cuestionario2()
        Dim mensaje As String

        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()

        Cuestionario.header.aapType = "dasda4544"
        Cuestionario.header.dateRequest = "2016-05-27 12:00:21,854"
        Cuestionario.header.channel = "1"
        Cuestionario.header.subChannel = "2"
        Cuestionario.header.managementUnit = "CONFIN"
        Cuestionario.header.branchOffice = "sucursal"
        Cuestionario.header.user = "usuario"
        Cuestionario.header.idSession = "3232-3232"
        Cuestionario.header.idRequest = "1212-121212-12121-212"
        Cuestionario.header.dateConsumerInvocation = "2016-05-27 12:00:21"

        'Cuestionario.quote.idQuote = "145052"
       
        Cuestionario.quote.idQuote = ObtenIdQuote()

        'Dim Respuestas As New questionRespuesta

        Dim dsTabla As New DataSet
        dsTabla = Session("TABLA")

        Dim ID As String = ""
        Dim PREGUNTA As String = ""
        Dim SI As Integer = Nothing
        Dim NO As Integer = Nothing
        Dim VALOR As String = ""
        Dim RES As Integer = Nothing

        For i = 0 To dsTabla.Tables(0).Rows.Count - 1
            ID = dsTabla.Tables(0).Rows(i).Item("PDK_ID_PREGUNTA").ToString
            PREGUNTA = dsTabla.Tables(0).Rows(i).Item("PDK_PREGUNTA").ToString
            SI = dsTabla.Tables(0).Rows(i).Item("SI").ToString
            NO = dsTabla.Tables(0).Rows(i).Item("NO").ToString
            VALOR = dsTabla.Tables(0).Rows(i).Item("VALOR").ToString
            RES = dsTabla.Tables(0).Rows(i).Item("Res").ToString

            Dim Respuestas As New questionRespuesta
            Respuestas.catalogItemBase.id = ID

            If ID = "CUESVFD02" Or ID = "CUESVFD01" Then
                Respuestas.answerQuestion.catalogItemBase.name = VALOR
            Else
                Respuestas.answerQuestion.catalogItemBase.name = ""
            End If

            Respuestas.answerQuestion.catalogItemBase.id = SI
            Cuestionario.questionnaire.questionRespuesta.Add(Respuestas)
        Next

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

        Dim jsonBODY As String = serializer.Serialize(Cuestionario)

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("createQuestionnaire")

        'restGT.consumerID = "10000004"
        restGT.buscarHeader("ResponseWarningDescription")
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

        Dim jresul2 As Respuesta = serializer.Deserialize(Of Respuesta)(jsonResult)

        Dim alert = serializer.Deserialize(Of msjerr)(jsonResult)

        If restGT.IsError Then
            mensaje = (alert.message & " Estatus: " & alert.status & ".")

            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("sol") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "');", True)
            Exit Function
        End If

        GuardaWS = True

    End Function

    Public Function ValidaWS() As Boolean
        ValidaWS = False

        Dim Cuestionario As Cuestionario = New Cuestionario()
        Dim mensaje As String

        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()

        Cuestionario.header.aapType = "dasda4544"
        Cuestionario.header.dateRequest = "2016-05-27 12:00:21,854"
        Cuestionario.header.channel = "1"
        Cuestionario.header.subChannel = "2"
        Cuestionario.header.managementUnit = "CONFIN"
        Cuestionario.header.branchOffice = "sucursal"
        Cuestionario.header.user = "usuario"
        Cuestionario.header.idSession = "3232-3232"
        Cuestionario.header.idRequest = "1212-121212-12121-212"
        Cuestionario.header.dateConsumerInvocation = "2016-05-27 12:00:21"

        'Cuestionario.quote.idQuote = "145052"
        Cuestionario.quote.idQuote = ObtenIdQuote()


        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

        Dim jsonBODY As String = serializer.Serialize(Cuestionario)

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Dim restGT As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()

        restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("validateQuestionnaire")

        'restGT.consumerID = "10000004"
        restGT.buscarHeader("ResponseWarningDescription")
        Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

        Dim jresul2 As Respuesta = serializer.Deserialize(Of Respuesta)(jsonResult)

        Dim alert = serializer.Deserialize(Of msjerr)(jsonResult)

        If restGT.IsError Then
            mensaje = (alert.message & " Estatus: " & alert.status & ".")

            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("sol") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "');", True)
            Exit Function
        End If


        ValidaWS = True

    End Function

    Public Function ObtenIdQuote() As String

        Dim clsseg As clsSeguros = New clsSeguros
        clsseg._ID_SOLICITUD = Request("sol")
        Dim DatosSeguro As DataSet = clsseg.getDatosSeguro()
       
        If (DatosSeguro.Tables.Count > 0) Then
            If (DatosSeguro.Tables(0).Rows.Count > 0) Then
                Return DatosSeguro.Tables(0).Rows(0).Item("NO_COTSEG").ToString
                Exit Function
            End If
        End If
        

        Return ""
    End Function


    Protected Sub btnproc_Click(sender As Object, e As EventArgs) Handles btnproc.Click
        asignaTarea(0)
    End Sub
    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("sol")))
        Dim mensaje As String = String.Empty
        cmbGuardar.Disabled = True
        Try

            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("sol"))
            Solicitudes.PDK_CLAVE_USUARIO = Request("usu")
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")

            If idAsignarPantalla <> 0 Then
                Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla
            End If

            If mensaje <> "" Then
                Master.MensajeError(mensaje)
                cmbGuardar.Disabled = False
            Else
                dsresult = Solicitudes.ValNegocio(1)
                mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
                Solicitudes.MENSAJE = mensaje
                Solicitudes.ManejaTarea(5)

                If mensaje <> "Tarea Exitosa" And mensaje <> "SE RECHAZO  DOCUMENTO " And mensaje <> "TAREA EXITOSA" Then
                    Throw New Exception(mensaje)
                End If

                dslink = objtarea.SiguienteTarea(Val(Request("sol")))

                muestrapant = objpantalla.SiguientePantalla(Val(Request("sol")))
                Dim dc As New clsDatosCliente
                dc.idSolicitud = Request("sol")
                dc.getDatosSol()

                If muestrapant = 0 Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & "../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString & "');", True)
                ElseIf muestrapant = 2 Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '" & Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona & "&sol=" & Val(Request("Sol")).ToString & "');", True)
                    'Response.Redirect("../aspx/consultaPanelControl.aspx")
                End If
            End If

        Catch ex As Exception
            cmbGuardar.Attributes.Remove("disabled")
            btnRegresar.Attributes.Remove("disabled")
            cmbGuardar.Disabled = False
            Master.MensajeError(mensaje)
        End Try
    End Sub
End Class