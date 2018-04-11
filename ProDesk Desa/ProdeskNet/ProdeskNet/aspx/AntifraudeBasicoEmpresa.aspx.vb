'BBVA-P-423 GVARGAS 11/05/2017 RQADM-07 Antifraude Básico Cliente, Empresa, Riesgos y Pre-Aprobados 40,76
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
'BUG-PD-313: DJUAREZ: 27/12/2017: Se evita el avanzar tareas cuando se presiona F5 cuando esta cargando una pagina.
Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.BD
Imports ProdeskNet.SN
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports ProdeskNet.WCF

Partial Class aspx_AntifraudeBasicoEmpresa
    Inherits System.Web.UI.Page

    Dim BD As New clsManejaBD
    Dim DB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()

    Private Sub aspx_AntifraudeBasicoEmpresa_Load(sender As Object, e As EventArgs) Handles Me.Load
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

        Dim folio As String = Request("Sol").ToString()

        Dim CteIncredit As String = getCteIncredit(folio)
        Dim mensaje As String = updateAFEmpresa(folio, CteIncredit)

        dsresult = DB.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

        dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

        muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

        Dim strLocation As String = String.Empty

        If muestrapant = 0 Then
            strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
        ElseIf muestrapant = 2 Then
            'Response.Redirect("../aspx/consultaPanelControl.aspx")
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "PopUpLetreroRedirect('Tarea Exitosa.', '../aspx/consultaPanelControl.aspx');", True)
        End If
    End Sub

    Private Function getCteIncredit(ByVal folio As String) As String
        Dim respuesta As String = String.Empty
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_CteIncredit_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                respuesta = reader(0)
            Loop
        Catch ex As Exception
            respuesta = String.Empty
        End Try

        sqlConnection1.Close()
        Return respuesta
    End Function

    Private Function updateAFEmpresa(ByVal folio As String, ByVal CteIncredit As String) As String
        Dim AF_Empresa As AF_Empresa = Empresa(CteIncredit)

        Dim respuesta As String = String.Empty

        If (AF_Empresa.dispercion <> "") Then
            Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
            Dim cmd As New SqlCommand
            Dim reader As SqlDataReader
            Try
                cmd.CommandText = "set_AFEmpresa_SP"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
                cmd.Parameters.AddWithValue("@Cta_BBVA", CteIncredit)
                cmd.Parameters.AddWithValue("@dispersoras", AF_Empresa.dispercion)
                cmd.Parameters.AddWithValue("@empresa_dispersora", AF_Empresa.Empresa_Dispersora)
                cmd.Connection = sqlConnection1
                sqlConnection1.Open()
                reader = cmd.ExecuteReader()

                Do While reader.Read()
                Loop
            Catch ex As Exception
                respuesta = String.Empty
            End Try

            sqlConnection1.Close()
        End If

        Return respuesta
    End Function

    Public Class AF_Empresa
        Public dispercion As String
        Public Empresa_Dispersora As String
    End Class

    Private Function Empresa(ByVal num_cliente As String) As AF_Empresa
        Dim AF_Empresa As AF_Empresa = New AF_Empresa()

        AF_Empresa.Empresa_Dispersora = ""
        AF_Empresa.dispercion = "NO"
        Dim jsonBODY As String = ""
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("Nomina").ToString().Replace("CambiarPorCuenta", num_cliente)

        Dim jsonResult As String = rest.ConnectionGet(userID, iv_ticket1, jsonBODY)
        If (rest.IsError = False) Then
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim dispersionsCompany As dispersionsCompany_ = serializer.Deserialize(Of dispersionsCompany_)(jsonResult)

            If (dispersionsCompany.dispersionsCompany.Count > 0) Then
                AF_Empresa.dispercion = "SI"
                For Each disp As dispersionsCompany In dispersionsCompany.dispersionsCompany
                    AF_Empresa.Empresa_Dispersora = disp.description
                Next
            End If
        End If
        Return AF_Empresa
    End Function

    Public Class dispersionsCompany_
        Public dispersionsCompany As List(Of dispersionsCompany) = New List(Of dispersionsCompany)()
    End Class

    Public Class dispersionsCompany
        Public description As String
    End Class
End Class
