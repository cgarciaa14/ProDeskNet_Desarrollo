Imports ProdeskNet.Seguridad
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Web.UI.WebControls
Imports ProdeskNet.Buro
Imports System.Data
Imports System.Data.SqlClient

'BBV-P-423: BUG-PD-58: ERODRIGUEZ: 17/05/2017 Caja de mensajes por numero de solicitud
'BBV-P-423: BUG-PD-60: ERODRIGUEZ: 22/05/2017 Mensaje de error al introducir caracteres no validos ó números demasiado grandes en la busqueda por solicitud
'BBV-P-423: BUG-PD-62: ERODRIGUEZ: 24/05/2017 Permite marcar mensajes como leidos y vista de mensajes sin leer
Partial Class aspx_CajaNotas
    Inherits System.Web.UI.Page
    Dim BD As New ProdeskNet.BD.clsManejaBD

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Try
            If Trim$(textEditor.Value) = "" Then
                Throw New Exception("No es posible guardar mensajes en blanco")
            Else
                Dim ds As New DataSet

                'Dim strquery = "INSERT INTO PDK_CAJA_NOTAS (fe_creacion, mensaje,usu_creacion,PDK_ID_SOLICITUD) VALUES (GETDATE(),'" & Session("cveUsuAcc") & ": " & textEditor.Value & "'," & Session("IdUsua") & "," & Session("FolioSol") & ")"
                ds = BD.EjecutarQuery("INSERT INTO PDK_CAJA_NOTAS (fe_creacion, mensaje,usu_creacion,PDK_ID_SOLICITUD) VALUES (GETDATE(),'" & Session("cveUsuAcc") & ": " & textEditor.Value & "'," & Session("IdUsua") & "," & Session("FolioSol") & ")")
                Page_Load(Me, e)
                textEditor.Value = ""
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub btnActualizar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActualizar.Click
        Page_Load(Me, e)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"
        Dim permisos As New DataSet
        Dim mensajes As New DataSet
        Dim tmp As New DataSet

        hdnUsuario.Value = Session("IdUsua")
        hdnFolio.Value = Session("FolioSol")

        Dim sol As Int64

        Dim paramIsNumber As Boolean

        If (hdnFolio.Value <> "") Then
            paramIsNumber = Int64.TryParse(hdnFolio.Value, sol)
        End If


        If (paramIsNumber) Then

            textEditor.Visible = True
            btnGuardar.Visible = True
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "borrarMensaje", "$('.btnBorrar').click(function () { borrarMensaje($(this).attr('dataID')) })", True)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "borrarMensaje", "$('[id$=btnGuardar]').click(function () { $(this).attr('disabled'); })", True)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Actualizar", "$('[id$=btnActualizar]').click(function () { $(this).attr('disabled'); })", True)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "marcarMensajeLeido", "$('.chkLeido').click(function () { marcarMensajeLeido($(this).attr('dataID')) })", True)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "marcarMensajeLeido", "$('[id$=chkLeido]').click(function () { $(this).attr('disabled'); })", True)


            '        If Not IsPostBack Then 'Solo entra la primera vez que carga la página
            'textEditor.Value = "Ingrese su mensaje ..."
            'Else
            Try
                'textEditor.Value = ""
                permisos = BD.EjecutarQuery(" SELECT * " & _
                " FROM PDK_REL_USU_PER A " & _
                " INNER JOIN PDK_PERMISO_CARATULA B " & _
                " 	ON A.PDK_ID_PERFIL = B.PDK_ID_PERFIL " & _
                " INNER JOIN PDK_PERMISO_CARATULA_PESTANA C " & _
                " 	ON B.PDK_FI_ID_PERMISO_CARATULA_PESTANA = C.PDK_FI_ID_PERMISO_CARATULA_PESTANA " & _
                "                 WHERE " & _
                "                 A.PDK_CLAVE_USUARIO = " & Session("IdUsua") & _
                " 	AND C.PDK_FC_NOMBRE_PESTANA = 'cajaNotas'" & _
                "   AND C.PDK_FN_STATUS_PESTANA = 1")

                If permisos.Tables(0).Rows.Count > 0 Then
                    Dim permisoEscritura As New DataSet
                    permisoEscritura = BD.EjecutarQuery(" SELECT * " & _
                    " FROM PDK_REL_USU_PER A " & _
                    " INNER JOIN PDK_PERMISO_CARATULA B " & _
                    " 	ON A.PDK_ID_PERFIL = B.PDK_ID_PERFIL " & _
                    " INNER JOIN PDK_PERMISO_CARATULA_PESTANA C " & _
                    " 	ON B.PDK_FI_ID_PERMISO_CARATULA_PESTANA = C.PDK_FI_ID_PERMISO_CARATULA_PESTANA " & _
                    "                 WHERE " & _
                    "                 A.PDK_CLAVE_USUARIO = " & Session("IdUsua") & _
                    " 	AND C.PDK_FC_NOMBRE_PESTANA = 'botonPublicar'" & _
                    "   AND C.PDK_FN_STATUS_PESTANA = 1")

                    If permisoEscritura.Tables(0).Rows.Count = 0 Then
                        textEditor.Visible = False
                        btnGuardar.Visible = False
                    End If

                    tmp = BD.EjecutarQuery("SELECT * FROM PDK_CAJA_NOTAS WHERE estatus = 1 AND PDK_ID_SOLICITUD =" + Convert.ToString(sol))
                    If tmp.Tables(0).Rows.Count > 0 Then
                        mensajes = BD.EjecutarQuery("DECLARE @HTML AS VARCHAR(MAX) =" & _
                                    " '<tr><th class=""centerBold fecha"">Fecha</th>" & _
                                    "<th class=""centerBold"">Mensaje</th><th class=""centerBold"">Leido</th><th></th></tr>';" & _
                                    " SELECT @HTML = @HTML" & _
                                    " + '<tr><td class=""center fecha"">'" & _
                                    " + CAST(fe_creacion AS VARCHAR)" & _
                                    " + '</td><td class=""left"">'" & _
                                    " + CAST(mensaje AS VARCHAR(MAX))" & _
                                    " + '</td><td class=""center"">'" & _
                                    " + IIF(leido = 1,'<input type=""checkbox"" checked=""checked"" disabled=""disabled"">'," & _
                                    " + '<input class=""btnLeer"" value="" ""  style=""width:1% ; height:60%;"" type=""button"" dataID=""'" & _
                                    " + CAST(id AS VARCHAR) +'"">') " & _
                                    " + '</td><td class=""center"">'" & _
                                    " + CASE WHEN PDK_FN_STATUS_PESTANA IS NOT NULL" & _
                                    " THEN " & _
                                    "'<input class=""btnBorrar"" type=""button"" value=""Borrar"" dataID=""'" & _
                                    " + CAST(id AS VARCHAR) +'""></td> </tr>'" & _
                                    " ELSE " & _
                                    " '</td></tr>'" & _
                                    " END " & _
                                    "FROM PDK_CAJA_NOTAS A" & _
                                    " LEFT JOIN ( SELECT C.PDK_FN_STATUS_PESTANA" & _
                                    "	FROM PDK_REL_USU_PER A" & _
                                    "	INNER JOIN PDK_PERMISO_CARATULA B" & _
                                    "		ON A.PDK_ID_PERFIL = B.PDK_ID_PERFIL" & _
                                    "	INNER JOIN PDK_PERMISO_CARATULA_PESTANA C" & _
                                    "  		ON B.PDK_FI_ID_PERMISO_CARATULA_PESTANA = C.PDK_FI_ID_PERMISO_CARATULA_PESTANA" & _
                                    "	WHERE" & _
                                    "		A.PDK_CLAVE_USUARIO = " & Session("IdUsua") & _
                                    "		AND C.PDK_FC_NOMBRE_PESTANA = 'botonBorrar'" & _
                                    ") B" & _
                                    "	ON B.PDK_FN_STATUS_PESTANA = 1" & _
                                    " WHERE estatus = 1 AND PDK_ID_SOLICITUD = " + Convert.ToString(sol) + " " & _
                                    " ORDER BY id DESC" & _
                                    " SELECT @HTML")

                        messages.InnerHtml = mensajes.Tables(0).Rows(0).Item(0).ToString
                    End If
                Else
                    debug.InnerHtml = "<h1>No tienes permisos suficientes</h1>"
                End If
            Catch ex As Exception
                Master.MensajeError(ex.Message)

            End Try
            'End If
        Else

            textEditor.Visible = False
            btnGuardar.Visible = False
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "borrarMensaje", "$('.btnBorrar').click(function () { borrarMensaje($(this).attr('dataID')) })", True)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "borrarMensaje", "$('[id$=btnGuardar]').click(function () { $(this).attr('disabled'); })", True)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Actualizar", "$('[id$=btnActualizar]').click(function () { $(this).attr('disabled'); })", True)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "marcarMensajeLeido", "$('.chkLeido').click(function () { marcarMensajeLeido($(this).attr('dataID')) })", True)
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "marcarMensajeLeido", "$('[id$=chkLeido]').click(function () { $(this).attr('disabled'); })", True)

            'messages.InnerHtml = " "
            'debug.InnerHtml = "<h1>  </h1>"
            'messages.Dispose()

            Try

                tmp = BD.EjecutarQuery("SELECT * FROM PDK_CAJA_NOTAS WHERE estatus = 1 and leido = 0")
                If tmp.Tables(0).Rows.Count > 0 Then
                    Dim previo As String
                    previo = "DECLARE @HTML AS VARCHAR(MAX) =" & _
                                " '<tr><th class=""center fecha"">Solicitud</th><th class=""center fecha"">Fecha</th>" & _
                                "<th class=""center"">Mensaje</th><th></th></tr>';" & _
                                " SELECT @HTML =  @HTML" & _
                                " + '<tr><td class=""center fecha"">'" & _
                                " + CAST(PDK_ID_SOLICITUD AS VARCHAR)" & _
                                " + '</td><td class=""center fecha"">'" & _
                                " + CAST(fe_creacion AS VARCHAR)" & _
                                " + '</td><td class=""center"">'" & _
                                " + CAST(mensaje AS VARCHAR(MAX))" & _
                                " + '</td></tr>'" & _
                                " FROM PDK_CAJA_NOTAS" & _
                                " WHERE estatus = 1 AND leido = 0" & _
                                " ORDER BY id DESC" & _
                                    " SELECT @HTML"
                    mensajes = BD.EjecutarQuery(previo)

                    messages.InnerHtml = mensajes.Tables(0).Rows(0).Item(0).ToString


                End If
            Catch ex As Exception
                Master.MensajeError(ex.Message)
            End Try


        End If

    End Sub
End Class
