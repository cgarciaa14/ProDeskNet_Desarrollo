'BUG-PD-313: DJUAREZ: 27/12/2017: Se evita el avanzar tareas cuando se presiona F5 cuando esta cargando una pagina.
'BUG-PD-388: DJUAREZ: Modificar URL de respuesta para corregir F5 en tarea manual

Imports System.Data.SqlClient
Imports System.Web.UI

Public Class clsValidateData
    Public Class Respon_BD
        Public Message As String = String.Empty
        Public Path As String = String.Empty
    End Class
    Public Function ValidaTarea(ByVal Solicitud As Integer, ByVal Tarea As Integer) As Respon_BD
        Dim Response_BD As New Respon_BD

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader

        Try
            cmd.CommandText = "sp_ConsultaTareaActual"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@PDK_ID_SOLICITUD", Solicitud.ToString())
            cmd.Parameters.AddWithValue("@PDK_ID_TAREAS", Tarea.ToString())

            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                Response_BD.Message = reader(0).ToString()
                Response_BD.Path = reader(1).ToString()
            Loop
        Catch ex As Exception
            Response_BD.Message = "Error: " + ex.Message
        End Try

        Return Response_BD
    End Function
    Public Function ValidateRequest(ByVal Request As System.Web.HttpRequest) As String
        Dim TareaActual As String = Request.Params("idPantalla")
        Dim Solicitud As String = Request.Params("sol")
        Dim DocumentData As New DocumentDataOfCar
        Dim url As String = String.Empty

        If TareaActual IsNot Nothing And Solicitud IsNot Nothing Then
            If Not Request.Url.Query.Contains("&Load=1") Then

                Dim ResponDB As Respon_BD = ValidaTarea(Solicitud, TareaActual)

                If ResponDB.Message <> "OK" Then
                    If ResponDB.Path = "../aspx/consultaPanelControl.aspx" Then
                        url = "PopUpLetreroRedirectValidateRequest('" + ResponDB.Message + "', '" + ResponDB.Path + "');"
                    Else
                        url = "PopUpLetreroRedirectValidateRequest('" + ResponDB.Message + "', '" + ResponDB.Path + "&usuario=" + Val(Request("usuario")).ToString() + "&Load=1" + "');"
                    End If
                End If

            End If
        End If
        Return url
    End Function
End Class
