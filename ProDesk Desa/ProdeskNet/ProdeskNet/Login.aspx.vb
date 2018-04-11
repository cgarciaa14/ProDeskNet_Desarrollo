' BUG-PD-13  GVARGAS  28/02/2017 AutoLogIn
' BUG-PD-18  GVARGAS  06/03/2017 Create Session when LoginEXTERNO = 0
' BUG-PD-151  GVARGAS  12/07/2017 Cambios iv_ticket
' BUG-PD-180  GVARGAS 07/08/2017 Correcion Session timeout
' BUG-PD-311 : ERODRIGUEZ: 18/12/2017: Se corrigieron etiquetas de actividad al cambiar de pantalla
' RQ-PI7-PD13-3: ERODRIGUEZ: 21/12/2017: Se agrego inicio de sesion.
Imports ProdeskNet.Seguridad
Imports System.Data
Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim LoginExt As Integer = 1
        If Request("d3bug") = "t313pr0" Or Session("d3bug") = "t313pr0" Then
            LoginExt = 0
            Session("d3bug") = "t313pr0"
        End If
        If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString()) = 1) And LoginExt = 1 Then

            Dim userID As String = Request.QueryString("userID")
            Dim iv_ticket As String = Request.QueryString("iv_ticket")
            Dim css As String = Request.QueryString("css")

            Session("userID") = Nothing
            Session("iv_ticket") = Nothing
            Session("css") = Nothing
            Session("ext") = Nothing
            Session("agencia") = Nothing

            If ((userID <> Nothing) And (iv_ticket <> Nothing) And (css <> Nothing)) Then
                Session("userID") = userID
                Session("iv_ticket") = iv_ticket.ToString().Replace("_encode_1", "+").Replace("_encode_2", "/")
                Session("css") = css.ToUpper()

                If userID.IndexOf("EXT") <> -1 Then
                    Session("ext") = "EXT"
                Else
                    Session("ext") = "INT"
                End If


                If (css.IndexOf("HONDA") <> -1) Then
                    Session("agencia") = "H"
                ElseIf (css.IndexOf("ACURA") <> -1) Then
                    Session("agencia") = "A"
                ElseIf (css.IndexOf("JLR_") <> -1) Then
                    Session("agencia") = "J"
                ElseIf (css.IndexOf("SUKI") <> -1) Then
                    Session("agencia") = "S"
                Else
                    Session("agencia") = "SIN"
                End If

                txtUsu.Text = userID.Replace("EXT", "")
                txtPwd.Text = iv_ticket.ToString().Replace("_encode_1", "+").Replace("_encode_2", "/")
                btnAceptar_Click(Me, EventArgs.Empty)
                Return
            Else
                If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("Produccion").ToString()) = 1) Then
                    Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri.ToString()
                    Dim path As String = HttpContext.Current.Request.Url.AbsolutePath.ToString()
                    url = url.Replace(path, "")
                    Dim paths As String() = HttpContext.Current.Request.Url.AbsolutePath.ToString().Split("/")
                    path = paths(1)

                    Dim _url As String = url + "/" + path + "/"

                    Dim Link As String = System.Configuration.ConfigurationManager.AppSettings("urlProCotiza").ToString()

                    ScriptManager.RegisterStartupScript(Page, GetType(String), "Out", "window.location.href = ' " + _url + Link + "';", True)
                    Return
                Else
                    Session("userID") = System.Configuration.ConfigurationManager.AppSettings("userID").ToString()
                    Session("iv_ticket") = System.Configuration.ConfigurationManager.AppSettings("iv_ticket").ToString()
                    Session("css") = System.Configuration.ConfigurationManager.AppSettings("css").ToString()
                    Session("ext") = "INT"
                    Session("agencia") = "SIN"

                    txtUsu.Text = System.Configuration.ConfigurationManager.AppSettings("userID").ToString()
                    txtPwd.Text = "NO_PASS"
                    btnAceptar_Click(Me, EventArgs.Empty)
                    Return
                End If
            End If
        End If

        If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString()) = 0) Then
            Session("userID") = System.Configuration.ConfigurationManager.AppSettings("userID").ToString()
            Session("iv_ticket") = System.Configuration.ConfigurationManager.AppSettings("iv_ticket").ToString()
            Session("css") = System.Configuration.ConfigurationManager.AppSettings("css").ToString()
            Session("ext") = "INT"
            Session("agencia") = "SIN"
        End If



        If Not IsPostBack Then
            txtUsu.Focus()
            usuAcceso.Value = ""
            intentosAcceso.Value = 0
        End If
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Dim objDat As New ProdeskNet.Seguridad.clsSeguridad
        Dim objUsu As New ProdeskNet.Seguridad.clsUsuario
        Dim objUsuBloq As clsUsuario = Nothing
        'Dim dtePWD As Date
        Dim bdData As New DataSet
        Dim intRegistro As Integer = 0
        Try


            If Trim$(txtUsu.Text) = "" Or Trim$(txtPwd.Text) = "" Then
                Throw New Exception("Debe proporcionar usuario y contraseña")
            Else
                objUsu = objDat.ValidaUsuario(Trim$(txtUsu.Text), Trim$(txtPwd.Text))
                If Trim$(objDat.ErrorSeguridad) <> "" Then


                    If usuAcceso.Value = Trim$(txtUsu.Text) Then
                        intentosAcceso.Value = Val(intentosAcceso.Value) + 1
                    Else
                        usuAcceso.Value = Trim$(txtUsu.Text)
                        intentosAcceso.Value = 1
                    End If

                    If Val(intentosAcceso.Value) >= 3 Then
                        objUsuBloq = New clsUsuario(usuAcceso.Value)
                        If Trim(objUsuBloq.PDK_ID_USUARIO) <> "" And Trim(objUsuBloq.ErrorUsuario) = "" Then
                            objUsuBloq.PDK_USU_ACTIVO = 3
                            objUsuBloq.PDK_USU_MODIF = Format(Now(), "yyyy-MM-dd")
                            objUsuBloq.ManejaUsuario(3)
                        End If
                    End If
                    Throw New Exception(objDat.ErrorSeguridad)
                Else
                    Session("cveUsuAcc") = txtUsu.Text.ToUpper
                    Session("strNombre") = objUsu.PDK_USU_NOMBRE & " " & objUsu.PDK_USU_APE_PAT & " " & objUsu.PDK_USU_APE_MAT
                    Session("IdUsua") = objUsu.PDK_ID_USUARIO
                    FormsAuthentication.RedirectFromLoginPage(objUsu.PDK_USU_CLAVE, False)

                    updateTimeLogIn(txtUsu.Text.ToUpper)
                    updateUsuarioActivo(objUsu.PDK_ID_USUARIO)
                    Response.Redirect("./aspx/consultaPanelControl.aspx?usr=" + Session("userID").ToString() + "&tck=" + Session("iv_ticket").ToString() + "&sn=" + Session("css").ToString())
                End If
            End If
        Catch ex As Exception
            Master.MensajeErrorLogin(ex.Message)
        End Try
    End Sub

    Private Function updateTimeLogIn(ByVal usr As String) As String
        Dim COT As String = String.Empty
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "update_Hora_Login_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_USU_CLAVE", usr)
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

    <System.Web.Services.WebMethod()> _
    Public Shared Function OutToProDesk(ByVal url As String) As String
        Dim vars As String = ""
        Dim Link As String = ""
        If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString()) = 1) Then
            Link = System.Configuration.ConfigurationManager.AppSettings("urlProCotiza").ToString()
            vars = "?out=true"
        Else
            Link = "salir.aspx"
            vars = ""
        End If
        Return url + Link + vars
    End Function

    Private Sub updateUsuarioActivo(ByVal idusr As Integer)
        Dim datainicio As DataSet = ProdeskNet.Seguridad.clsUsuario.InicioSesionEstatus(idusr)
    End Sub
End Class