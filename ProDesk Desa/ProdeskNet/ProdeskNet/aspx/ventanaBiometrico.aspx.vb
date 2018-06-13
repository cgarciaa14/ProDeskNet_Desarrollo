'RQ-PD33: DJUAREZ: 03/05/2018: Se crea nueva pantalla para visualizar biometría

Imports ProdeskNet.Catalogos
Imports ProdeskNet.BD
Imports System.Data
Imports System.Data.SqlClient

Partial Class aspx_ventanaBiometrico
    Inherits System.Web.UI.Page
    Dim _clsSolictds As New clsSolictds
    Dim _NoSolicitud As Integer? = Nothing
    Dim _Opcion As Integer
    Dim _SolAutenticar As Integer? = Nothing
    Dim _Pantalla As Integer = 69

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hdnUsua.Value = Session("IdUsua")
            btnBuscar_Click(sender, e)
        End If
    End Sub
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs)
        divEmptyTableResult.Visible = False
        If tbxNumeroSolicitud.Text <> String.Empty Then
            _NoSolicitud = Convert.ToInt32(tbxNumeroSolicitud.Text)
        Else
            _NoSolicitud = 0
        End If
        _Opcion = 25
        _clsSolictds = _clsSolictds.obtenerSolicitudesBiometrico(NoSolicitud:=_NoSolicitud, _
                                                           Opcion:=_Opcion)
        repSolicitudes.DataSource = Nothing
        repSolicitudes.DataBind()
        If Not _clsSolictds Is Nothing And _clsSolictds.Count > 0 Then
            repSolicitudes.DataSource = _clsSolictds
            repSolicitudes.DataBind()
            ChangeVisibility(True)
        Else
            ChangeVisibility(False)
        End If
    End Sub

    Private Sub ChangeVisibility(ByVal divStatus As Boolean)
        divEmptyTableResult.Visible = Not divStatus
        'divTableResult.Visible = divStatus
        divTurnar.Visible = divStatus
    End Sub

    Protected Sub btnAutenticar_Click(sender As Object, e As EventArgs)
        If txtSolAutenticar.Text <> String.Empty Then
            _SolAutenticar = Convert.ToInt16(txtSolAutenticar.Text)
        End If
        If AutGenerator(_SolAutenticar, _Pantalla) Then
            If UpdAutenticacion(_SolAutenticar, _Pantalla, "AUTENTICADO") Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AutenticadaOK", "PopUpLetrero('La(s) solicitud(es) han sido autenticadas');", True)
                If (txtCompleto.Text = "COMPLETO") Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "refresh", "window.setTimeout('window.location.reload(true);',5000);", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AutenticadaError", "PopUpLetrero('Error al autenticar la(s) solicitud(es)');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AutenticadaError", "PopUpLetrero('Error al autenticar la(s) solicitud(es)');", True)
        End If

    End Sub

    Private Function AutGenerator(ByVal Solicitud As String, ByVal IdPant As String) As Boolean
        Dim Result As Boolean = False
        Dim _dsResult As DataSet = New DataSet
        Dim _clsManejaBD As clsManejaBD = New clsManejaBD()
        Try
            _clsManejaBD.AgregaParametro("@PDK_ID_SECC_CERO", TipoDato.Entero, Solicitud, False)
            _clsManejaBD.AgregaParametro("@PDK_ID_PANTALLA", TipoDato.Entero, IdPant, False)
            _dsResult = _clsManejaBD.EjecutaStoredProcedure("spManejaDatosHuella")
            If Not _dsResult Is Nothing And _dsResult.Tables.Count > 0 And _dsResult.Tables(0).Rows.Count > 0 Then
                If _dsResult.Tables(0).Rows(0)("MENSAJE").ToString() = String.Empty Then
                    Result = True
                Else
                    Result = False
                End If
            Else
                Result = False
            End If
        Catch ex As Exception
            Result = False
        End Try
        Return Result
    End Function

    Private Function UpdAutenticacion(ByVal Solicitud As String, ByVal IdPant As String, ByVal Message As String) As Boolean
        Dim Result As Boolean = False

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "update_Spent_Attempts_INE_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", Solicitud)
            cmd.Parameters.AddWithValue("@PDK_ID_PANTALLA", IdPant)
            If (Message <> "") Then
                cmd.Parameters.AddWithValue("@MESSAGE", Message)
            End If
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
            Loop
            Result = True
        Catch ex As Exception
            Result = False
        End Try

        sqlConnection1.Close()

        Return Result
    End Function
End Class
