Imports System.Data.SqlClient
Imports ProdeskNet.BD
Imports ProdeskNet.WCF
Imports ProdeskNet.SN

'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BUG-PD-01: AMR: 22/12/2016: HTTP Error 404 - Not Found al procesar Precalificación.
'BUG-PD-11:GAPM: 16.02.2017 modificacion de amata
'BUG-PD-13  GVARGAS  01/03/2017 Cambio WCF Credenciales
'AUTOMIK-TASK-428 GVARGAS 26/03/2018 Create Class Function
'AUTOMIK-TASK-435 ERODRIGUEZ 16/04/2018 Validacion para Automik en r1, se permite guardar cuando falle servicio prospector.

Public Class clsProspectus
    Private Class jResult
        Public creditCapacityIndex As creditCapacityIndex = New creditCapacityIndex()
        Public status As String = String.Empty
        Public value As String = String.Empty
    End Class

    Private Class creditCapacityIndex
        Public value As String
    End Class

    Public Function GetProspector(ByVal Folio As String, ByVal userID As String, ByVal iv_ticket1 As String, ByVal Uri As String, Optional ByVal usu As String = "-1") As String 'ResponseProspector
        Dim automik As Integer = 0
        If usu = "-1" Then
            usu = "1"
            automik = 1
        End If

        Dim Response As ResponseProspector = New ResponseProspector()
        Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()

        Try
            Dim Params As List(Of Params_BD) = New List(Of Params_BD)()
            Dim Param As Params_BD = New Params_BD()

            Param.Name = "PDK_ID_SECCCERO"
            Param.Value = Folio
            Params.Add(Param)

            Param = New Params_BD()
            Param.Name = "AUTOMIK"
            Param.Value = Folio
            Params.Add(Param)

            Dim VALIDA_PROSPECTOR As Boolean = Int32.Parse(get_JSON_BySP("getAgenciaBySol", Params))

            Params = New List(Of Params_BD)()
            Param = New Params_BD()
            Param.Name = "PDK_ID_SECCCERO"
            Param.Value = Folio
            Params.Add(Param)

            Dim jsonBODY As String = get_JSON_BySP("get_Info_Prospector_sp", Params)

            Dim restGT As RESTful = New RESTful()
            restGT.Uri = Uri

            If automik = 1 Then
                restGT.automikRequest = True
            End If

            Dim jsonResult As String = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)


            Params = New List(Of Params_BD)()
            Param = New Params_BD()
            Param.Name = "PDK_ID_SECCCERO"
            Param.Value = Folio
            Params.Add(Param)

            Param = New Params_BD()
            Param.Name = "PAYLOAD_ENVIO"
            Param.Value = jsonBODY
            Params.Add(Param)

            If restGT.IsError Then
                Response.Message = IIf(restGT.StatusHTTP = 500, "Error al consultar Servicio Web: ", "Mensaje del Servicio Web: ") & restGT.MensajeError & ". Estatus:" & restGT.StatusHTTP
                Response.Code = "ERROR"
                Response.Path = 0

                Param = New Params_BD()
                Param.Name = "PAYLOAD_RESPUESTA"
                Param.Value = restGT.MensajeError + "; " + jsonResult
                Params.Add(Param)

                Dim saved As String = get_JSON_BySP("get_Info_Prospector_sp", Params)
            Else
                Dim resp As jResult = srrSerialer.Deserialize(Of jResult)(jsonResult)

                Dim icc_ As String = resp.creditCapacityIndex.value
                Dim bcc_ As String = resp.value
                Dim state_ As String = resp.status

                If ((bcc_ Is Nothing) Or (bcc_ = "")) Then
                    bcc_ = "-9.00"
                End If

                If ((icc_ Is Nothing) Or (icc_ = "")) Then
                    icc_ = "-3.00"
                End If

                Dim objupd As New ProdeskNet.SN.clsActualizaBuro()

                Dim r1 As Integer = objupd.actREPORTE_SCORE(Folio, icc_, bcc_, usu)
                Dim r2 As Integer = objupd.actDICTAMEN_FINAL(Folio, icc_, bcc_, usu)
                If automik Then 'checar esta validacion que se hizo para automik por no haber registro en la tabla PDK_BURO_REPORTE_SCORE
                    If r1 <= 0 Then
                        r1 = 1
                    End If
                End If

                If r1 > 0 And r2 > 0 Then
                    If state_ = "APROBADO" Or VALIDA_PROSPECTOR = False Then
                        Response.Message = state_
                        Response.Code = "OK"
                        Response.Path = 2
                    Else
                        Response.Message = "No viable"
                        Response.Code = "ERROR"
                        Response.Path = 1
                    End If
                Else
                    Response.Message = "Error al actualizar información."
                    Response.Code = "ERROR"
                    Response.Path = 0
                End If

                Param = New Params_BD()
                Param.Name = "PAYLOAD_RESPUESTA"
                Param.Value = jsonResult
                Params.Add(Param)

                Dim saved As String = get_JSON_BySP("get_Info_Prospector_sp", Params)
            End If
        Catch ex As Exception
            Response.Message = ex.Message.ToString().Replace(vbCr, "").Replace(vbLf, "").Replace("'", "")
            Response.Code = "ERROR"
            Response.Path = 0
        End Try

        Return srrSerialer.Serialize(Response)
    End Function

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

    Private Class Params_BD
        Public Name As String
        Public Value As String
    End Class

    Public Class ResponseProspector
        Public Code As String
        Public Message As String
        Public Path As Integer
    End Class
End Class
