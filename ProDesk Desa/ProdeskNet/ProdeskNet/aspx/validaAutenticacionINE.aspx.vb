#Region "Trackers"
'BUG-PD-149:MPUESTO:11/07/2017:Validación automática del estado de la autenticación
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
'BUG-PD-204 GVARGAS 19/09/2017 WS biometrico
'BUG-PD-211 GVARGAS 02/10/2017 Cambios mostrar info
'BUG-PD-221 GVARGAS 03/10/2017 Cambios validacion INE
'BBV-P-423 RQ-PD-17 4 GVARGAS 12/01/2018 Update when validation is OK
'BBV-P-423 RQ-PD-17 7 GVARGAS 19/01/2018 Correcion Path
'BBV-P-423 RQ-PD-17 8 GVARGAS 25/01/2018 Ajuste validaciones Biometrico
'BBV-P-423 RQ-PD-17 9 GVARGAS 30/01/2018 Ajustes flujos
'BBV-P-423 RQ-PD-17 10 GVARGAS 30/01/2018 Ajustes flujos
'BBV-P-423 RQ-PD-17 11 GVARGAS 30/01/2018 Ajustes flujos 2
'BBV-P-423 RQ-PD-17 12 GVARGAS 06/02/2018 Ajustes flujos 3
'BBV-P-423 RQ-PD-17 13 GVARGAS 12/02/2018 Ajustes flujos 4
'BBV-P-423 RQ-PD-17 14 GVARGAS 13/02/2018 Ajustes flujos 5
'BUG-PD-380 GVARGAS 02/03/2018 Correccion urgente instalacion biometrico
#End Region

Imports System.Data
Imports ProdeskNet.BD
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Data.SqlClient
Imports System.IO

Partial Class aspx_validaAutenticacionINE
    Inherits System.Web.UI.Page

#Region "Variables"
    'Dim _idPantalla As Integer
    Dim _mostrarPantalla As Integer = 0
    Dim _clsPantallas As New ProdeskNet.SN.clsPantallas()
    Dim _dtsResult As DataSet
    Dim _clsManejaBD As New clsManejaBD
    Dim _dsResult As DataSet
    Dim _clsCatTareas As New ProdeskNet.SN.clsCatTareas()
    Dim _clsSolicitudes As New clsSolicitudes(0)
#End Region

#Region "Properties"
    Public ReadOnly Property _idSolicitud() As Integer
        Get
            Return IIf(Not Request("sol") Is Nothing, Val(Request("Sol")), _
                                        IIf(Not Request("NoSolicitud") Is Nothing, Val(Request("Sol")), _
                                            IIf(Not Request("IdFolio") Is Nothing, Val(Request("Sol")), 0)))
        End Get
    End Property

    Public ReadOnly Property _idPantalla() As Integer
        Get
            Return IIf(Not Request("idPantalla") Is Nothing, Val(Request("idPantalla")), 146)
        End Get
    End Property

#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim regresoINE As String = System.Configuration.ConfigurationManager.AppSettings("regresoINE").ToString()

        Dim query As StringBuilder = New StringBuilder()
        query.Append("EXEC crud_Biometrico_SP " + _idSolicitud.ToString() + ", " + _idPantalla.ToString() + ", 1, 'aa'")
        Dim auth As Boolean = executeQuerys(query)

        If auth Then
            NextTask()
        Else
            query = New StringBuilder()
            query.Append("EXEC crud_Biometrico_SP " + _idSolicitud.ToString() + ", " + _idPantalla.ToString() + ", 9, 'aa'")
            Dim tareaTurnar As Boolean = executeQuerys(query)

            If tareaTurnar Then
                query = New StringBuilder()
                query.Append("EXEC crud_Biometrico_SP " + _idSolicitud.ToString() + ", " + _idPantalla.ToString() + ", 8, 'aa'")
                Dim validaTurnar As Boolean = executeQuerys(query)

                If validaTurnar Then
                    query = New StringBuilder()
                    query.Append("EXEC crud_Biometrico_SP " + _idSolicitud.ToString() + ", " + _idPantalla.ToString() + ", 11, 'aa'")
                    Dim authWithInfo As Boolean = executeQuerys(query)

                    If (authWithInfo = False) Then
                        valida_INE(_idSolicitud.ToString())
                    End If

                    Dim pathturnar As Integer = verifyINEResponse()

                    Dim idAsignarPantalla As Integer = 0
                    Dim sol As Integer = _idSolicitud
                    Dim pantalla As Integer = _idPantalla
                    Dim usu As String = Session("IdUsua").ToString()

                    If pathturnar = 1 Then
                        'turnar celula
                        idAsignarPantalla = 177
                        asignaTarea_(idAsignarPantalla, sol, pantalla, usu)
                    ElseIf pathturnar = 2 Then
                        'turnar a Prospectus
                        query = New StringBuilder()
                        query.Append("EXEC crud_Biometrico_SP " + _idSolicitud.ToString() + ", " + _idPantalla.ToString() + ", 4, 'aa'")
                        idAsignarPantalla = Int32.Parse(executeQuerys(query))

                        asignaTarea_(idAsignarPantalla, sol, pantalla, usu)
                    Else
                        NextTask()
                    End If
                Else
                    NextTask()
                End If
            Else
                query = New StringBuilder()
                query.Append("EXEC crud_Biometrico_SP " + _idSolicitud.ToString() + ", " + _idPantalla.ToString() + ", 6, 'aa'")
                Dim etapa As Integer = Int32.Parse(executeQuerys(query))

                query = New StringBuilder()
                query.Append("EXEC crud_Biometrico_SP " + _idSolicitud.ToString() + ", " + _idPantalla.ToString() + ", 4, 'aa'")
                Dim tareaRechazo As Integer = Int32.Parse(executeQuerys(query))

                query = New StringBuilder()
                query.Append("EXEC crud_Biometrico_SP " + _idSolicitud.ToString() + ", " + tareaRechazo.ToString() + ", 5, 'aa'")
                Dim limite As Boolean = executeQuerys(query)


                If limite Then
                    Dim marcaPreca As marcaPreca = getMarcaPreca(Int64.Parse(Request("Sol").ToString()))

                    query = New StringBuilder()
                    query.Append("EXEC crud_Biometrico_SP " + _idSolicitud.ToString() + ", " + _idPantalla.ToString() + ", 14, 'aa'")
                    Dim etapa_byDatos As Integer = Int32.Parse(executeQuerys(query))

                    If etapa_byDatos = 1 Then
                        If (marcaPreca.id_INE) Then
                            If (valida_INE(Val(Request("Sol")))) Then
                                NextTask()
                            Else
                                asignaTarea()
                            End If
                        Else
                            NextTask()
                        End If
                    Else
                        NextTask()
                    End If
                Else
                    Try
                        Dim marcaPreca As marcaPreca = getMarcaPreca(Int64.Parse(Request("Sol").ToString()))

                        If ((marcaPreca.cambioPreca) And (_idPantalla = 146) And (marcaPreca.id_INE)) Then
                            If (valida_INE(Val(Request("Sol")))) Then
                                NextTask()
                            Else
                                asignaTarea()
                            End If
                        ElseIf ((marcaPreca.fakeAuth) And (marcaPreca.id_INE = False)) Then
                            If ((GetNextTask()) And (marcaPreca.cambioPreca = False) And (_idPantalla = 146)) Then
                                GoToAuthentication()
                            ElseIf ((GetNextTask()) And (_idPantalla <> 146)) Then
                                GoToAuthentication()
                            Else
                                NextTask()
                            End If
                        Else
                            Dim enterINE As enterINE = autorizo_INE(Val(Request("Sol")), _idPantalla.ToString())

                            If (enterINE.path = 0) Then
                                If GetNextTask() Then
                                    GoToAuthentication()
                                Else
                                    NextTask()
                                End If
                            ElseIf (enterINE.path = 1) Then
                                NextTask()
                            Else
                                asignaTarea()
                            End If

                        End If
                    Catch ex As Exception
                        asignaTarea()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Function GetNextTask() As Boolean
        Dim result As Boolean = False

        _dtsResult = New DataSet()
        _clsManejaBD = New clsManejaBD()
        '_clsManejaBD.AgregaParametro("@ID_USUARIO", TipoDato.Entero, Convert.ToInt32(Session("IdUsua")), False)
        _clsManejaBD.AgregaParametro("@PDK_ID_SECCCERO", TipoDato.Entero, Convert.ToInt32(_idSolicitud), False)
        _dtsResult = _clsManejaBD.EjecutaStoredProcedure("get_BiometriaAgencia")

        If Not _dtsResult Is Nothing Then
            If _dtsResult.Tables.Count > 0 Then
                If _dtsResult.Tables(0).Rows.Count > 0 Then
                    result = Convert.ToBoolean(_dtsResult.Tables(0).Rows(0)("VALIDA_BIOMETRICO"))
                End If
            End If
        End If

        If result Then
            _dtsResult = New DataSet()
            _clsManejaBD = New clsManejaBD()
            _clsManejaBD.AgregaParametro("@PDK_ID_SECC_CERO", TipoDato.Entero, _idSolicitud, False)
            _clsManejaBD.AgregaParametro("@PDK_AUTHENTICATION_RESULT", TipoDato.Cadena, "AUTENTICADO", False)
            _dtsResult = _clsManejaBD.EjecutaStoredProcedure("get_HistoryAuthentication")

            result = False

            If Not _dtsResult Is Nothing Then
                If _dtsResult.Tables.Count > 0 Then
                    If _dtsResult.Tables(0).Rows.Count = 0 Then
                        result = True
                    End If
                End If
            End If
        End If
        Return result
    End Function

    Private Sub GoToAuthentication()
        _clsSolicitudes = New clsSolicitudes(_idSolicitud)
        _clsSolicitudes.PDK_ID_SOLICITUD = _idSolicitud
        _clsSolicitudes.BOTON = 64
        _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
        _dtsResult = _clsManejaBD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & _idPantalla.ToString())
        If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO")
        Else
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = 0
        End If
        _clsSolicitudes.ValNegocio(1)
        checkRedirect()
    End Sub

    Private Sub NextTask()
        _clsSolicitudes = New clsSolicitudes(_idSolicitud)
        _clsSolicitudes.PDK_ID_SOLICITUD = _idSolicitud
        _clsSolicitudes.BOTON = 64
        _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")

        _dtsResult = _clsManejaBD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & _idPantalla.ToString())
        If Not _dtsResult Is Nothing And _dtsResult.Tables.Count > 0 And _dtsResult.Tables(0).Rows.Count > 0 Then
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = _dtsResult.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO")
        Else
            _clsSolicitudes.PDK_ID_CAT_RESULTADO = 0
        End If
        _clsSolicitudes.ValNegocio(1)
        checkRedirect()
    End Sub

    Protected Sub checkRedirect()
        _dtsResult = _clsCatTareas.SiguienteTarea(Val(Request("Sol")))
        _mostrarPantalla = _clsPantallas.SiguientePantalla(Val(Request("Sol")))
        Dim strLocation As String = String.Empty
        If _mostrarPantalla = 0 Then
            strLocation = "../aspx/" + _dtsResult.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString() + "?idPantalla=" + _dtsResult.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString() + "&Sol=" + _idSolicitud.ToString() + "&usuario=" + Val(Request("usu")).ToString()
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
        ElseIf _mostrarPantalla = 2 Then
            strLocation = ("../aspx/consultaPanelControl.aspx")
            Dim mensaje As String = "Tarea Exitosa"
            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + mensaje + "', '" + strLocation + "');", True)
        End If
    End Sub

    Private Function autorizo_INE(ByVal folio As String, ByVal idTarea As String) As enterINE
        Dim autoriza As enterINE = New enterINE()

        autoriza.autorizo_INE = False
        autoriza.INE = False

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            'ELSE IF @OPCION = 21 --get_Autoriza_INE_SP     @PDK_ID_SECCCERO BIGINT, @PDK_ID_TAREAS   BIGINT = NULL
            'EXEC crud_Biometrico_SP 3534, 142, 21, ''
            cmd.CommandText = "crud_Biometrico_SP"
            'cmd.CommandText = "crud_Biometrico_SP " + folio + ", " + idTarea + ", 21, ''"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            'cmd.Parameters.AddWithValue("@PDK_ID_TAREAS", idTarea)
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Parameters.AddWithValue("@PDK_ID_TAREAS", idTarea)
            cmd.Parameters.AddWithValue("@OPCION", "21")
            cmd.Parameters.AddWithValue("@XML_INFO", "")
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                If (reader("autorizo_INE").ToString() = "SI") Then
                    autoriza.autorizo_INE = True
                End If
                If (reader("INE").ToString() = "SI") Then
                    autoriza.INE = True
                End If
                autoriza.path = Int32.Parse(reader("PATH").ToString())
            Loop


        Catch ex As Exception
            autoriza.autorizo_INE = False
            autoriza.INE = False
        End Try

        sqlConnection1.Close()
        Return autoriza
    End Function

    Public Class enterINE
        Public autorizo_INE As Boolean
        Public INE As Boolean
        Public path As Integer = -1
    End Class

    Private Function valida_INE(ByVal folio As String) As Boolean
        Dim autoriza As Boolean = False
        Dim validateIdentityCard As validateIdentityCard = info_INE(folio)

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonBODY As String = serializer.Serialize(validateIdentityCard)
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("validateIdentityCard").ToString()
        Dim jsonResult As String = rest.ConnectionPost(userID, iv_ticket1, jsonBODY)

        If (rest.IsError = False) Then
            autoriza = save_INE(folio, jsonResult)
        End If

        Return autoriza
    End Function

    Private Function info_INE(ByVal folio As String) As validateIdentityCard
        Dim validateIdentityCard As validateIdentityCard = New validateIdentityCard()

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            'get_Info_INE_SP @PDK_ID_SECCCERO
            '--EXEC crud_Biometrico_SP 3534, 0, 22, ''
            cmd.CommandText = "crud_Biometrico_SP"
            'cmd.CommandText = "crud_Biometrico_SP " + folio + ", 0, 22, ''"
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Parameters.AddWithValue("@PDK_ID_TAREAS", "0")
            cmd.Parameters.AddWithValue("@OPCION", "22")
            cmd.Parameters.AddWithValue("@XML_INFO", "")
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                validateIdentityCard.identityCard.opticalCharacterRecognition = reader("opticalCharacterRecognition").ToString()
                validateIdentityCard.operationDate = reader("operationDate").ToString()
                validateIdentityCard.channelCode = reader("channelCode").ToString()
                validateIdentityCard.functionCode = reader("functionCode").ToString()
                validateIdentityCard.officeCode = reader("officeCode").ToString()

                Dim userCode As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
                If (userCode.Contains(".")) Then
                    Dim spl As String() = userCode.Split(".")
                    userCode = spl(1)
                End If
                If (userCode.Length > 8) Then
                    userCode = userCode.Substring(userCode.Length - 8)
                End If
                validateIdentityCard.userCode = userCode

                validateIdentityCard.terminalCode = reader("terminalCode").ToString()
                validateIdentityCard.customer.person.name = reader("name").ToString()
                validateIdentityCard.customer.person.lastName = reader("lastName").ToString()
                validateIdentityCard.customer.person.mothersLastName = reader("mothersLastName").ToString()
            Loop

        Catch ex As Exception
            validateIdentityCard = New validateIdentityCard()
        End Try

        sqlConnection1.Close()
        Return validateIdentityCard
    End Function

    Private Function save_INE(ByVal folio As String, ByVal resp_INE As String) As Boolean
        Dim autoriza As Boolean = False

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            'set_Response_INE_SP @PDK_ID_SECCCERO INT, @payloadResponseINE VARCHAR(MAX), @OPCION INT = NULL, @PDK_ID_PANTALLA INT = NULL
            'NULL = 0
            'EXEC crud_Biometrico_SP 3534, 0, 24, '{"ocr":"true","mothersLastName":"true","lastName":"true","name":"true","pawPrint2":"0","codePawPrint2":91,"descripcionPawPrint2":"OCR Vigente","pawPrint7":"10000","codePawPrint7":91,"descripcionPawPrint7":"OCR Vigente","code":91,"descripcion":"OCR Vigente","yearRegistry":"false","numberEmision":"false","codeElector":"false","codeCurp":"false"}'
            'EXEC crud_Biometrico_SP 3534, 142, 24, ''
            cmd.CommandText = "crud_Biometrico_SP"
            'cmd.CommandText = "crud_Biometrico_SP " + folio + ", 0, 24, " + resp_INE
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            'cmd.Parameters.AddWithValue("@payloadResponseINE", resp_INE)
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Parameters.AddWithValue("@PDK_ID_TAREAS", "0")
            cmd.Parameters.AddWithValue("@OPCION", "24")
            cmd.Parameters.AddWithValue("@XML_INFO", resp_INE)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
            Loop
            autoriza = True
        Catch ex As Exception
            autoriza = False
        End Try

        sqlConnection1.Close()
        Return autoriza
    End Function

    Public Class validateIdentityCard
        Public identityCard As identityCard = New identityCard()
        Public operationDate As String
        Public channelCode As String
        Public functionCode As String
        Public officeCode As String
        Public userCode As String
        Public terminalCode As String
        Public pawPrint2 As String = String.Empty
        Public pawPrint7 As String = String.Empty
        Public customer As customer = New customer()
    End Class

    Public Class identityCard
        Public opticalCharacterRecognition As String
    End Class

    Public Class customer
        Public person As person = New person()
    End Class

    Public Class person
        Public name As String
        Public lastName As String
        Public mothersLastName As String
    End Class

    Private Sub asignaTarea()
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "update_Task_Blocked_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SOLICITUD", _idSolicitud)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
            Loop
        Catch ex As Exception

        End Try

        sqlConnection1.Close()

        ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Servicio no disponible.', '../aspx/consultaPanelControl.aspx');", True)
    End Sub

    Private Function getMarcaPreca(ByVal folio As Integer) As marcaPreca
        Dim marcaPreca As marcaPreca = New marcaPreca()
        marcaPreca.cambioPreca = False
        marcaPreca.id_INE = False
        marcaPreca.fakeAuth = False

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_Marca_Preca_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                marcaPreca.cambioPreca = reader("MARCA_PRECA").ToString()
                marcaPreca.id_INE = reader("ID").ToString()
                marcaPreca.fakeAuth = reader("FAKE").ToString()
            Loop

        Catch ex As Exception
            marcaPreca.cambioPreca = False
            marcaPreca.id_INE = False
            marcaPreca.fakeAuth = False
        End Try

        sqlConnection1.Close()
        Return marcaPreca
    End Function

    Public Class marcaPreca
        Public cambioPreca As Boolean
        Public id_INE As Boolean
        Public fakeAuth As Boolean
    End Class

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

    Private Function verifyINEResponse() As Integer
        Dim Query As StringBuilder = New StringBuilder()
        Query.Append("EXEC crud_Biometrico_SP " + _idSolicitud.ToString() + ", " + _idPantalla.ToString() + ", 10, 'aa'")
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim IneResponse As IneResponse = serializer.Deserialize(Of IneResponse)(executeQuerys(Query))

        Dim respuesta As Integer = 0
        Dim porcentMin As Integer = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("codePawPrint").ToString())
        Dim porcentPrintINE As Integer = 0

        If IneResponse.pawPrint2 <> "0" Then
            porcentPrintINE = Convert.ToInt32(IneResponse.pawPrint2)
        Else
            porcentPrintINE = Convert.ToInt32(IneResponse.pawPrint7)
        End If

        Dim fails As Integer = 0
        If IneResponse.name.ToUpper() <> "FALSE" Then
            fails = fails + 1
        End If
        If IneResponse.lastName.ToUpper() <> "FALSE" Then
            fails = fails + 1
        End If
        If IneResponse.mothersLastName.ToUpper() <> "FALSE" Then
            fails = fails + 1
        End If

        Dim huella As Boolean = True
        If (porcentMin > porcentPrintINE) Then
            huella = False
        End If

        If (((fails = 3) And (IneResponse.ocr.ToUpper() = "TRUE") And (huella)) = False) Then
            Dim INE_Inputs As INE_Inputs = New INE_Inputs()
            INE_Inputs.code = IneResponse.code
            INE_Inputs.fails = fails

            Dim huella_ As Integer = 0
            If (huella) Then
                huella_ = 1
            End If

            Dim OCR_ As Integer = 0
            If (IneResponse.ocr.ToUpper() = "TRUE") Then
                OCR_ = 1
            End If

            INE_Inputs.FingerPrint = huella_
            INE_Inputs.OCR = OCR_

            Dim XmlSerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(INE_Inputs.GetType)
            Dim xmlINE As StringWriter = New StringWriter()
            XmlSerializer.Serialize(xmlINE, INE_Inputs)

            Dim Query_ As StringBuilder = New StringBuilder()
            Query_.Append("EXEC crud_Biometrico_SP " + _idSolicitud.ToString() + ", " + _idPantalla.ToString() + ", 12,'" + xmlINE.ToString() + "'")
            respuesta = Int32.Parse(executeQuerys(Query_))

            'If (IneResponse.code = 92) Then
            '    respuesta = 1
            'ElseIf ((fails = 0) And (IneResponse.ocr.ToUpper() = "FALSE") And (huella = False)) Then
            '    respuesta = 2
            'ElseIf ((fails <= 2) And (IneResponse.ocr.ToUpper() = "TRUE") And (huella = False)) Then
            '    respuesta = 1
            'ElseIf ((fails = 0) And (IneResponse.ocr.ToUpper() = "TRUE")) Then
            '    respuesta = 1
            'ElseIf ((IneResponse.ocr.ToUpper() = "TRUE") And (huella)) Then
            '    respuesta = 1
            'ElseIf ((fails > 0) And (IneResponse.ocr.ToUpper() = "FALSE")) Then
            '    respuesta = 1
            'ElseIf ((fails <= 2) And (IneResponse.ocr.ToUpper() = "TRUE") And (huella)) Then
            '    respuesta = 1
            'Else
            '    respuesta = 3
            'End If
        End If

        Return respuesta
    End Function

    Public Class INE_Inputs
        Public fails As Integer
        Public OCR As Integer
        Public FingerPrint As Integer
        Public code As Integer
    End Class

    Public Class IneResponse
        Public ocr As String
        Public mothersLastName As String
        Public lastName As String
        Public name As String
        Public pawPrint2 As String
        Public codePawPrint2 As Integer
        Public descripcionPawPrint2 As String
        Public pawPrint7 As String
        Public codePawPrint7 As Integer
        Public descripcionPawPrint7 As String
        Public code As Integer
        Public descripcion As String
        Public yearRegistry As String
        Public numberEmision As String
        Public codeElector As String
        Public codeCurp As String
    End Class

    Private Sub asignaTarea_(ByVal idAsignarPantalla As Integer, ByVal sol As Integer, ByVal pantalla As Integer, ByVal usu As String)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(sol)
        Dim mensaje As String = String.Empty

        Try
            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = sol
            Solicitudes.PDK_CLAVE_USUARIO = usu
            Solicitudes.PDK_ID_PANTALLA = pantalla
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            dslink = objtarea.SiguienteTarea(Solicitudes.PDK_ID_SOLICITUD)

            muestrapant = objpantalla.SiguientePantalla(Solicitudes.PDK_ID_SOLICITUD)
            Dim strLocation As String = String.Empty
            If muestrapant = 0 Then
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString) 'BUG-PD-125
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('', '" + strLocation + "');", True)
            ElseIf muestrapant = 2 Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea exitosa.', '" + strLocation + "');", True)
            End If

        Catch ex As Exception
            Dim strmessage As String = "Error al procesar la Tarea."
            Dim strLocation As String = ("../aspx/consultaPanelControl.aspx")
            ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('" + strmessage + "', '" + strLocation + "');", True)
        End Try
    End Sub
End Class
