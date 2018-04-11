'BBV-P-423 RQ-PD-17 9 GVARGAS 30/01/2018 Ajustes flujo
'BBV-P-423 RQ-PD-17 12 GVARGAS 06/02/2018 Ajustes flujos 3

Imports System.Data.SqlClient
Imports System.Data
Imports ProdeskNet.Catalogos

Partial Class aspx_metodoAutenticacion
    Inherits System.Web.UI.Page

    Dim dc As New ProdeskNet.Catalogos.clsDatosCliente
    Dim es As New ProdeskNet.Catalogos.clsStatusSolicitud

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        dc.GetDatosCliente(Request.QueryString("sol"))
        lblSolicitud.Text = Request.QueryString("sol")
        lblCliente.Text = dc.propNombreCompleto

        es.getStatusSol(Request.QueryString("sol"))
        Me.lblStCredito.Text = es.PStCredito
        Me.lblStDocumento.Text = es.PStDocumento
    End Sub

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

    Public Class responseINE
        Public message As String
        Public url As String
    End Class

    <System.Web.Services.WebMethod()> _
    Public Shared Function getInformationINE(ByVal sol As String, ByVal idPant As String, ByVal auth As String, ByVal usu As String) As String
        Dim responseINE As responseINE = New responseINE()

        Dim query As StringBuilder = New StringBuilder()
        Dim aspx As aspx_metodoAutenticacion = New aspx_metodoAutenticacion()

        Dim tarea As Integer = 0

        If auth = "0" Then
            query.Append("EXEC crud_Biometrico_SP " + sol + ", " + idPant + ", 3,''")
            tarea = Int32.Parse(aspx.executeQuerys(query))
        Else
            Dim agenciaBio As Boolean = aspx.getagenciaBio(sol)

            If agenciaBio = False Then
                query.Append("EXEC crud_Biometrico_SP " + sol + ", " + idPant + ", 3,''")
                tarea = Int32.Parse(aspx.executeQuerys(query))
            Else
                query.Append("EXEC crud_Biometrico_SP " + sol + ", " + idPant + ", 1,''")
                Dim authenticado As Boolean = aspx.executeQuerys(query)

                query = New StringBuilder()
                query.Append("EXEC crud_Biometrico_SP " + sol + ", " + idPant + ", 2,''")
                Dim idINE As Boolean = aspx.executeQuerys(query)

                If (authenticado = False) And (idINE) Then
                    query = New StringBuilder()
                    query.Append("EXEC crud_Biometrico_SP " + sol + ", " + idPant + ", 4,''")
                    tarea = Int32.Parse(aspx.executeQuerys(query))
                Else
                    query = New StringBuilder()
                    query.Append("EXEC crud_Biometrico_SP " + sol + ", " + idPant + ", 3,''")
                    tarea = Int32.Parse(aspx.executeQuerys(query))
                End If
            End If
        End If

        responseINE = aspx.asignaTarea_(Int32.Parse(tarea), Int32.Parse(sol), Int32.Parse(idPant), usu)

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Respuesta As String = serializer.Serialize(responseINE)
        Return json_Respuesta
    End Function

    Private Function asignaTarea_(ByVal idAsignarPantalla As Integer, ByVal sol As Integer, ByVal pantalla As Integer, ByVal usu As String) As responseINE
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(sol)
        Dim mensaje As String = String.Empty

        Dim responseINE As responseINE = New responseINE()
        responseINE.message = ""
        responseINE.url = "../aspx/consultaPanelControl.aspx"

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

            responseINE.message = mensaje

            dslink = objtarea.SiguienteTarea(Solicitudes.PDK_ID_SOLICITUD)

            muestrapant = objpantalla.SiguientePantalla(Solicitudes.PDK_ID_SOLICITUD)
            Dim strLocation As String = String.Empty
            If muestrapant = 0 Then
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString) 'BUG-PD-125
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            ElseIf muestrapant = 2 Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            End If

            responseINE.url = strLocation
        Catch ex As Exception
            responseINE.message = "Error al procesar la Tarea."
            responseINE.url = String.Empty
        End Try

        Return responseINE
    End Function

    Private Function getagenciaBio(ByVal PDK_ID_SECCCERO As Integer) As Boolean
        Dim agenciaINE As Boolean = False

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_BiometriaAgencia"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", PDK_ID_SECCCERO)

            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()
            Do While reader.Read()
                agenciaINE = reader(0).ToString()
            Loop
        Catch ex As Exception
            agenciaINE = False
        End Try
        sqlConnection1.Close()

        Return agenciaINE
    End Function

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("sol")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub
End Class
