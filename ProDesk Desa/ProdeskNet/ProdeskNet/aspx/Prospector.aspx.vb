'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BUG-PD-01: AMR: 22/12/2016: HTTP Error 404 - Not Found al procesar Precalificación.
'BUG-PD-11: GAPM 16.02.17: modificacion por amata
'BBV-P-423: ERV: 20/04/2017 RQADM-04 Validación prospector
'BUG-PD-67  GVARGAS 01/06/2017 Regresa si ocurre error sin borrar OPE_SOLICITUD-->
'BUG-PD-87  GVARGAS 15/06/2017 Regresa si ocurre un error logico
'BUG-PD-129  GVARGAS 29/06/2017 Correcion PEPs y Ñs
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
'BUG-PD-216 GVARGAS 29/09/2017 Cambios mostrar info
'BUG-PD-239 GVARGAS 17/10/2017 Camnio tamaño apellidos
'BUG-PD-285 GVARGAS 30/11/2017 Prospector cambios urgentes
'BUG-PD-313: DJUAREZ: 27/12/2017: Se evita el avanzar tareas cuando se presiona F5 cuando esta cargando una pagina.
'RQ-PD-23: ERODRIGUEZ: 23/02/2018: Cambio en datos enviados a servicio prospector para nuevo servicio getScoreEvaluation se quito if else, para ir siempre sin CLIENTE_INCREDIT
Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.Catalogos
Imports ProdeskNet.WCF
Imports ProdeskNet.BD

Partial Class aspx_Prospector
    Inherits System.Web.UI.Page

    Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim strError As String = String.Empty

    Private Sub aspx_Prospector_Load(sender As Object, e As EventArgs) Handles Me.Load
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
        Dim dsDatosSolicitante As New DataSet()
        Dim dsseccero As New DataSet()
        Dim dsSolicitante As New DataSet()
        Dim dsresult As New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim dsAgencia As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing

        Dim objDatosSolicitante As New ProdeskNet.SN.clsTabDatosSolicitante()
        Dim objseccero As New ProdeskNet.SN.clsTabSeccionCero()
        Dim objSolicitante As New ProdeskNet.SN.clsCteIndeseable()
        Dim objpros As New ProdeskNet.WCF.clsProspectus
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim objAgencia As New ProdeskNet.SN.clsValidaProspector()
        Dim result As Integer = Nothing
        Dim VALIDA_PROSPECTOR As Boolean = False

        Try
            dsAgencia = objAgencia.Validacion_Alianza(Val(Request("Sol")))

            If dsAgencia.Tables(0).Rows(0).Item("VALIDA_PROSPECTOR") = 0 Then
                VALIDA_PROSPECTOR = False
            Else
                VALIDA_PROSPECTOR = True
            End If
        Catch
        End Try



        Try
            'Codigo anterior a valida prospector
            dsDatosSolicitante = objDatosSolicitante.CargaDatosSolicitante(Val(Request("Sol")))
            dsseccero = objseccero.CargaDatosCotiza(Val(Request("Sol")))
            dsSolicitante = objSolicitante.ConsultaGetCustomer(Val(Request("Sol")))


            If dsDatosSolicitante.Tables.Count > 0 Then
                If dsDatosSolicitante.Tables(0).Rows.Count > 0 Then
                    'Cambio de prospector para nuevo servicio getScoreEvaluation se quito if else
                    'If dsDatosSolicitante.Tables(0).Rows(0).Item("CLIENTE_INCREDIT").ToString.Length > 0 Then
                    '    'Cliente BBVA
                    '    objpros.ProductCode = System.Configuration.ConfigurationManager.AppSettings.Item("ProductCode")
                    '    objpros.ReferenceNumber = System.Configuration.ConfigurationManager.AppSettings.Item("Entidad") & System.Configuration.ConfigurationManager.AppSettings.Item("CR") & formatsendws(Val(Request("Sol")).ToString, 10, 0)
                    '    objpros.Amount = Replace(dsseccero.Tables(0).Rows(0).Item("LOANTOVALUE"), ",", ".")
                    '    objpros.Id = dsDatosSolicitante.Tables(0).Rows(0).Item("CLIENTE_INCREDIT").ToString
                    '    objpros.Name = ""
                    '    objpros.LastName = ""
                    '    objpros.MothersLastName = ""
                    '    objpros.Rfc = ""
                    '    objpros.Sex = ""
                    '    objpros.StreetName = ""
                    '    objpros.Neightborthood = ""
                    '    objpros.City = ""
                    '    objpros.State = ""
                    '    objpros.ZipCode = ""
                    'Else
                    objpros.ProductCode = System.Configuration.ConfigurationManager.AppSettings.Item("ProductCode")
                    objpros.ReferenceNumber = System.Configuration.ConfigurationManager.AppSettings.Item("Entidad") & System.Configuration.ConfigurationManager.AppSettings.Item("CR") & formatsendws(Val(Request("Sol")).ToString, 10, 0)
                    objpros.Amount = Replace(dsseccero.Tables(0).Rows(0).Item("LOANTOVALUE"), ",", ".")
                    objpros.Id = ""

                    Dim Param As Params_BD = New Params_BD()
                    Param.Name = "PDK_ID_SECCCERO"
                    Param.Value = Request("Sol").ToString()

                    Dim Params As List(Of Params_BD) = New List(Of Params_BD)()
                    Params.Add(Param)

                    Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
                    Dim Prospector_JSON As String = get_JSON_BySP("get_Info_Prospector_sp", Params)
                    Dim Prospector As Prospector = serializer.Deserialize(Of Prospector)(Prospector_JSON)

                    objpros.Name = Prospector.InformationProspector(0).NOMBRE1
                    objpros.LastName = Prospector.InformationProspector(0).APELLIDO_PATERNO
                    objpros.MothersLastName = Prospector.InformationProspector(0).APELLIDO_MATERNO
                    objpros.Rfc = Prospector.InformationProspector(0).RFC
                    objpros.Sex = Prospector.InformationProspector(0).SEXO
                    objpros.StreetName = Prospector.InformationProspector(0).CALLE
                    objpros.Neightborthood = Prospector.InformationProspector(0).COLONIA
                    objpros.City = Prospector.InformationProspector(0).CIUDAD
                    objpros.State = Prospector.InformationProspector(0).EFD_CL_BNC
                    objpros.ZipCode = Prospector.InformationProspector(0).CP
                    'End If  'Cambio de prospector para nuevo servicio getScoreEvaluation

                    If objpros.GetProspector() Then
                        Dim bcc As String = objpros.Bcc
                        Dim icc As String = objpros.Icc
                        Dim status As String = objpros.Status

                        If ((bcc Is Nothing) Or (bcc = "")) Then
                            bcc = "-9.00"
                        End If

                        If ((icc Is Nothing) Or (icc = "")) Then
                            icc = "-3.00"
                        End If

                        Dim objupd As New ProdeskNet.SN.clsActualizaBuro()

                        Dim r1 As Integer = objupd.actREPORTE_SCORE(Val(Request("Sol")), icc, bcc, Val(Request("usuario")).ToString)
                        Dim r2 As Integer = objupd.actDICTAMEN_FINAL(Val(Request("Sol")), icc, bcc, Val(Request("usuario")).ToString)

                        If r1 > 0 And r2 > 0 Then


                            If status = "APROBADO" Or VALIDA_PROSPECTOR = False Then
                                dsresult = DB.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)
                                strError = dsresult.Tables(0).Rows(0).Item("MENSAJE")
                                If strError.Contains("ERROR") Then
                                    Throw New System.Exception(strError)
                                End If

                                objseccero.ActSeccionCeroProsp(Val(Request("Sol")), Val(Request("usuario")).ToString, 231) 'Viable
                            Else
                                objseccero.ActSeccionCeroProsp(Val(Request("Sol")), Val(Request("usuario")).ToString, 232) 'No viable
                                strError = "No viable"
                                Master.MsjErrorRedirect(strError, "./consultaPanelControl.aspx")
                                Exit Sub
                            End If

                            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

                            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

                            Dim strLocation As String = String.Empty

                            If muestrapant = 0 Then
                                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
                            ElseIf muestrapant = 2 Then
                                Master.MsjErrorRedirect(strError, "./consultaPanelControl.aspx")
                            End If
                        End If
                    Else
                        strError = objpros.Strerror
                        'result = objtarea.RegresaTarea(Val(Request("Sol")), Val(Request("usuario")))
                        'result = objseccero.ActSeccionCero(Val(Request("Sol")), Val(Request("usuario")), 238)
                        asignaTarea("1")

                        Master.MsjErrorRedirect(strError, "./consultaPanelControl.aspx")
                    End If
                End If
            End If

        Catch ex As Exception
            'Master.MsjErrorRedirect(ex.Message, "./consultaPanelControl.aspx")
            'strError = "Error al procesar la tarea"
            asignaTarea("1")
            Master.MsjErrorRedirect(ex.Message, "./consultaPanelControl.aspx")
        End Try
    End Sub

    Private Function formatsendws(valor As String, lng As Integer, isDec As Integer, Optional ispercent As Integer = 0) As String
        Dim strresult As String = String.Empty
        Dim Pos As Integer = 0

        Select Case ispercent
            Case 0
                If isDec = 1 Then
                    Pos = InStr(valor, ".")
                    If Pos > 0 Then
                        strresult = ((valor).Substring(0, Pos) & (valor).Substring(Pos, 2)).Replace(".", "")
                    Else
                        strresult = valor & "00"
                    End If
                    strresult = New String("0"c, lng - Len(strresult)) & strresult
                Else
                    strresult = New String("0"c, lng - Len(valor)) & valor
                End If
            Case 1
                Pos = InStr(valor, ".")
                If Pos > 0 Then
                    Dim strdec As String = valor.Substring(Pos, valor.Length - Pos)
                    If strdec.Length < 4 Then
                        strdec = strdec & New String("0"c, 4 - Len(strdec))
                    End If

                    Dim strent As String = valor.Substring(0, Pos - 1)
                    strresult = New String("0"c, lng - Len(strent & strdec)) & (strent & strdec)
                Else
                    strresult = valor & "0000"
                    strresult = New String("0"c, lng - Len(strresult)) & strresult
                End If
        End Select

        Return strresult
    End Function

    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Dim ipPantallaRechazo As Integer
        Try

            Dim ds_siguienteTarea As DataSet
            Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
            objCatalogos.Parametro = Request("idPantalla")
            ds_siguienteTarea = objCatalogos.Catalogos(6)

            If ds_siguienteTarea.Tables.Count > 0 Then
                If ds_siguienteTarea.Tables(0).Rows.Count > 0 Then
                    ipPantallaRechazo = Int32.Parse(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))
                End If
            End If



            Solicitudes.BOTON = 64
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")
            Solicitudes.PDK_ID_CAT_RESULTADO = ipPantallaRechazo

            dsresult = Solicitudes.ValNegocio(1)
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                'Response.Redirect("../aspx/consultaPanelControl.aspx")
                Master.MsjErrorRedirect(mensaje, "./consultaPanelControl.aspx")
                Exit Sub
            End If

            'BD.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

            'dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            'muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

            'If muestrapant = 0 Then
            '    Response.Redirect("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
            'ElseIf muestrapant = 2 Then
            '    'Response.Redirect("../aspx/consultaPanelControl.aspx")
            '    Dim dc As New clsDatosCliente
            '    dc.idSolicitud = Val(Request("sol"))
            '    dc.getDatosSol()
            '    Dim str_ As String = "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona
            '    str_ = ""

            '    ScriptManager.RegisterStartupScript(Page, GetType(String), "disp_confirm", "PopUpLetreroRedirect('Tarea Exitosa.', '../aspx/consultaPanelControl.aspx" + str_ + "');", True)
            '    Return
            'End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub

    Public Class Params_BD
        Public Name As String
        Public Value As String
    End Class

    Public Class Prospector
        Public Message As String
        Public InformationProspector As List(Of InformationProspector) = New List(Of InformationProspector)()
    End Class

    Public Class InformationProspector
        Public NOMBRE1 As String
        Public APELLIDO_PATERNO As String
        Public APELLIDO_MATERNO As String
        Public RFC As String
        Public FECHA_NAC As String
        Public HOMOCLAVE As String
        Public CP As String
        Public COLONIA As String
        Public CALLE As String
        Public CIUDAD As String
        Public ESTADO As String
        Public SEXO As String
        Public RFC2 As String
        Public EFD_CL_BNC As String
        Public DELEGA_O_MUNI As String
    End Class

    Private Function get_JSON_BySP(ByVal SP_Name As String, ByVal Params As List(Of Params_BD)) As String
        Dim JSON_Result_Str As String = String.Empty

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = SP_Name.ToString()
            cmd.CommandType = CommandType.StoredProcedure

            For Each Param As Params_BD In Params
                cmd.Parameters.AddWithValue("@" + Param.Name, Param.Value.ToString())
            Next

            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                JSON_Result_Str = reader(0).ToString()
            Loop
        Catch ex As Exception
            JSON_Result_Str = ex.ToString().Replace(vbCr, "").Replace(vbLf, "").Replace("'", "")
        End Try

        Return JSON_Result_Str
    End Function
End Class
