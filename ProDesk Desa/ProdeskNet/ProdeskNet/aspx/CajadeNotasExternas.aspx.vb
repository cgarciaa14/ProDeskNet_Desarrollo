'BUG-PD-91 : ERODRIGUEZ 12/06/2017 :  Funcionalidad para nueva caja de notas externas
'BBV-P-423 : BUG-PD-126: erodriguez: 30/06/2017 Se realizaron validaciones requeridas
'BBV-P-423 : BUG-PD-140: erodriguez: 05/07/2017 Se realizaron validaciones requeridas se ajusto area de mensajes
'BBV-PD-423 : BUG-PD-148: erodriguez: 11/07/2017 Se realizaron validaciones requeridas para validar visibilidad de mensasjes a usuarios de acuerdo a su perfil y agencia asignada
'BBV-PD-423 : BUG-PD-161: erodriguez: 18/07/2017 Se corrigio que no es visible guardar mensajes de solicitudes nuevas.
'BBV-PD-423 : BUG-PD-168: erodriguez: 21/07/2017 Se valido cuando el usuario no tenga agencia asignada.
'BUG-PD-176 : ERODRIGUEZ: 26/07/2017: Se corrigio para traer lista de solicitudes asignadas a la agencia del usuario.
'BUG-PD-230 : ERODRIGUEZ: 10/10/2017: Se ordenaron notas en orden descendente de fecha
'RQ-PI7-PD2: ERODRIGUEZ: 19/10/2017: Se agrego validacion para abrir caja de notas en pestaña nueva, llevando id de solicitud de pantalla donde se este trabajando.
'BUG-PD-262: ERODRIGUEZ: 07/11/2017: Se agrego validacion para cuando el id de la pantalla no este, en formato correcto.
'BUG-PD-438 : EGONZALEZ : 08/05/2018 : Se elimina la precarga de notas
'RQ-PC9: CGARCIA: NOTAS EXTERNAS EN CADA SOLICITUD
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Data
Imports System.Data.SqlClient

Partial Class aspx_CajadeNotasExternas
    Inherits System.Web.UI.Page
    Dim BD As New ProdeskNet.BD.clsManejaBD

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"
        Dim dbdata As New DataSet
        lblNomUsuario.Text = Session("cveUsuAcc")

        'Si la url trae id_solicitd se ejecuta la busqueda
        If Not IsNothing(Request.UrlReferrer) Then
            Dim paramSol = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)

            'validamos si viene de la pantalla reportes________________________________
            Dim strURl_Entrada As String = "reportes.aspx"

            For index As Integer = 0 To Request.UrlReferrer.Segments.Count - 1 Step 1
                If CStr(Request.UrlReferrer.Segments(index).ToString) = strURl_Entrada Then
                    Dim strSol As String = Request("sol")
                    Text1.Value = strSol
                    BuscaMensajes(1, strSol)
                End If
            Next
            '_____________________________________________________________________________
            If Request.UrlReferrer.Query.Contains("sol") Then
                Dim idsol As Integer = Integer.Parse(paramSol.Item("sol").ToString())
                Text1.Value = idsol
                lblNomUsuario.Text = Session("cveUsuAcc")
                btnBuscaCliente.Visible = False

                If Request.UrlReferrer.Query.Contains("idPantalla") Then

                    Try
                        Dim idPant As Integer = Integer.Parse(paramSol.Item("idPantalla").ToString())
                        Dim Tarea As String = ObtenTarea(idPant)
                        LabelTarea.Text = "Tarea: " + Tarea
                    Catch
                        If Request.UrlReferrer.Query.Contains("pantalla") Then
                            Dim idPant As Integer = Integer.Parse(paramSol.Item("pantalla").ToString())
                            Dim Tarea As String = ObtenTarea(idPant)
                            LabelTarea.Text = "Tarea: " + Tarea
                        End If
                    End Try

                End If

               

                'If Request.UrlReferrer.Query.Contains("usu") Then
                '    Dim usuario As Integer = Integer.Parse(paramSol.Item("usu").ToString())
                '    txtusu.Value = usuario

                'End If

            End If
        End If




        'busqueda de solicitud
        If Text1.Value <> "" Then
            Dim number As Int64
            Dim result As Boolean = Int64.TryParse(Text1.Value, number)

            If result Then
                'dbdata = clsPantallas.ObtenerlosTabs(number, 6)
                'If dbdata.Tables.Count > 0 AndAlso dbdata.Tables(0).Rows.Count > 0 Then
                '    hdnFolio.Value = dbdata.Tables(0).Rows(0).Item("PDK_ID_SECCCERO").ToString
                '    Session("FolioSol") = hdnFolio.Value
                'End If
                If Not IsPostBack Then
                    lblNomUsuario.Text = Session("cveUsuAcc")
                    hdnUsuario.Value = Session("IdUsua")
                    Dim nusu As Int64
                    Dim resultusu As Boolean = Int64.TryParse(hdnUsuario.Value, nusu)
                    If (resultusu) Then

                        BuscaMensajes(1, BuscaPermiso(nusu, Text1.Value))
                    End If
                Else
                    'Master.EjecutaJS("$(""#tabs"").tabs(); $(""[id$='inSolicitud']"").autocomplete({ source: $('[id$=""hdACNombre""]').val().split(',') }); $(""#divsol"").css(""display"", ""none"");fnOcultaObjetos(document.location.href.match(/[^\/]+$/)[0], $('[id$=""hdPerfilUsuario""]').val());")
                End If

            Else
                hdnUsuario.Value = Session("IdUsua")
                Dim nusu As Int64
                Dim resultusu As Boolean = Int64.TryParse(hdnUsuario.Value, nusu)
                If (resultusu) Then

                    BuscaMensajes(2, BuscaPermiso(nusu, Text1.Value))
                End If


                Master.MensajeError("Debe introducir un numero")
            End If
        Else
            If Not IsPostBack Then
                hdnUsuario.Value = Session("IdUsua")
                Dim nusu As Int64
                Dim resultusu As Boolean = Int64.TryParse(hdnUsuario.Value, nusu)
                If (resultusu) Then

                    BuscaMensajes(2, BuscaPermiso(nusu))

                End If
            End If

        End If

    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Try
            If Trim$(textEditor.Value) = "" Then
                Throw New Exception("No es posible guardar mensajes en blanco")
            Else
                If Text1.Value <> "" Then
                    Dim number As Int64
                    Dim result As Boolean = Int64.TryParse(Text1.Value, number)

                    If result Then
                        Try
                            Dim dss As New DataSet
                            dss = BD.EjecutarQuery("select PDK_ID_SECCCERO from PDK_TAB_SECCION_CERO WHERE PDK_ID_SECCCERO = " & Text1.Value)

                            If (dss.Tables.Count > 0) Then
                                If (dss.Tables(0).Rows.Count > 0) Then

                                    Dim solicitudes As String = BuscaPermiso(Session("IdUsua"), Text1.Value)
                                    If solicitudes.Contains(Text1.Value) Then
                                        Dim ds As New DataSet
                                        Dim tareag As String = ""
                                        If (LabelTarea.Text.Length > 6) Then
                                            tareag = LabelTarea.Text.Substring(7)
                                        End If

                                        'Dim strquery = "INSERT INTO PDK_CAJA_NOTAS_EXT (fe_creacion, mensaje,usu_creacion,PDK_ID_SOLICITUD,Tarea,Usuario) VALUES (GETDATE(),'" & Session("cveUsuAcc") & ": " & textEditor.Value & "'," & Session("IdUsua") & "," & Text1.Value & ",'" & LabelTarea.Text.Substring(7) & "','" & ObtenNombre(Session("IdUsua").ToString()) & "')"
                                        ds = BD.EjecutarQuery("INSERT INTO PDK_CAJA_NOTAS_EXT (fe_creacion, mensaje,usu_creacion,PDK_ID_SOLICITUD,Tarea,Usuario) VALUES (GETDATE(),'" & Session("cveUsuAcc") & ": " & textEditor.Value & "'," & Session("IdUsua") & "," & Text1.Value & ",'" & tareag & "','" & ObtenNombre(Session("IdUsua").ToString()) & "')")
                                        'Page_Load(Me, e)
                                        BuscaMensajes(1, BuscaPermiso(Session("IdUsua"), Text1.Value))
                                        textEditor.Value = ""
                                    Else
                                        Throw New Exception("La solicitud proporcionada no es valida")
                                    End If
                                Else
                                    Throw New Exception("La solicitud proporcionada no es valida")
                                End If
                            Else

                                Throw New Exception("La solicitud proporcionada no es valida")
                            End If
                        Catch ex As Exception
                            Master.MensajeError(ex.Message)
                            BuscaMensajes(2, BuscaPermiso(Session("IdUsua")))
                        End Try
                    Else

                        Throw New Exception("La solicitud proporcionada debe ser numerica")
                    End If
                Else
                    Throw New Exception("Proporcione una solicitud")
                End If
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
            If Text1.Value <> "" Then
                Dim number As Int64
                Dim result As Boolean = Int64.TryParse(Text1.Value, number)

                If result Then
                    BuscaMensajes(1, BuscaPermiso(Session("IdUsua")))
                End If
            Else
                BuscaMensajes(2, BuscaPermiso(Session("IdUsua")))
            End If

        End Try
    End Sub

    Protected Sub btnBuscaCliente_Click(sender As Object, e As EventArgs)
        Dim sol As Int64
        Dim nusu As Int64
        Dim resultusu As Boolean = Int64.TryParse(hdnUsuario.Value, nusu)
        Dim paramIsNumber As Boolean = False

        If (Text1.Value <> "") Then
            paramIsNumber = Int64.TryParse(Text1.Value, sol)
        End If
        If paramIsNumber Then
            If (resultusu) Then
                BuscaMensajes(1, BuscaPermiso(nusu, Text1.Value))
            End If
        Else
            If (resultusu) Then
                BuscaMensajes(2, BuscaPermiso(nusu, Text1.Value))
            End If
            End If
    End Sub

    Protected Sub GridViewGral_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        If CType(Session("dtsConsultaG"), DataSet) Is Nothing Then
            Dim nusu As Int64
            Dim resultusu As Boolean = Int64.TryParse(hdnUsuario.Value, nusu)
            If (resultusu) Then
                BuscaMensajes(2, BuscaPermiso(nusu, Text1.Value))
                'Page_Load(Me, e)
            End If
        End If

        GridViewGral.PageIndex = e.NewPageIndex
        GridViewGral.DataSource = CType(Session("dtsConsultaG"), DataSet)
        GridViewGral.DataBind()
        'Page_Load(Me, e)
    End Sub

    Protected Sub gvMensajes_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        If CType(Session("dtsConsulta"), DataSet) Is Nothing Then
            Dim nusu As Int64
            Dim resultusu As Boolean = Int64.TryParse(hdnUsuario.Value, nusu)
            If (resultusu) Then
                BuscaMensajes(1, BuscaPermiso(nusu, Text1.Value))
            End If
            'Page_Load(Me, e)
        End If

        gvMensajes.PageIndex = e.NewPageIndex
        gvMensajes.DataSource = CType(Session("dtsConsulta"), DataSet)
        gvMensajes.DataBind()
        'Page_Load(Me, e)
    End Sub

    Public Sub BuscaMensajes(opc As Integer, lista_solicitudes As String)

        If (lista_solicitudes <> "") Then
            Dim permisos As New DataSet
            Dim mensajes As New DataSet
            Dim tmp As New DataSet


            hdnUsuario.Value = Session("IdUsua")
            'hdnFolio.Value = Session("FolioSol")

            Dim sol As Int64

            Dim paramIsNumber As Boolean

            If (Text1.Value <> "") Then
                paramIsNumber = Int64.TryParse(Text1.Value, sol)
            End If

            If (paramIsNumber And opc = 1) Then
                hdnFolio.Value = Text1.Value
                GridViewGral.Dispose()
                GridViewGral.DataSource = Nothing
                GridViewGral.DataBind()
                GridViewGral.Visible = False

                gvMensajes.Visible = True

                textEditor.Visible = True
                btnGuardar.Visible = True

                Try
                    'Dim pconsulta As String = "select id id_nota, * from PDK_CAJA_NOTAS_EXT WHERE estatus = 1  and PDK_CAJA_NOTAS_EXT.PDK_ID_SOLICITUD in(" + lista_solicitudes + ") AND PDK_ID_SOLICITUD = " + Convert.ToString(sol)
                    Dim pconsulta As String = "select A.id id_nota, A.fe_creacion, A.fe_borrado, A.mensaje, A.estatus, A.usu_creacion, A.usu_borrado, A.PDK_ID_SOLICITUD, A.leido , A.Tarea ,B.PDK_USU_NOMBRE +' '+ B.PDK_USU_APE_PAT +' '+ B.PDK_USU_APE_MAT from PDK_CAJA_NOTAS_EXT A INNER JOIN PDK_USUARIO B on B.PDK_ID_USUARIO = A.usu_creacion WHERE estatus = 1  and A.PDK_ID_SOLICITUD in(" + lista_solicitudes + ") "

                    tmp = BD.EjecutarQuery(pconsulta)

                    If tmp.Tables(0).Rows.Count > 0 Then
                        'El usuario tiene mensajes en la caja de notas
                        'pconsulta = "select id id_nota, * from PDK_CAJA_NOTAS_EXT WHERE estatus = 1  and PDK_CAJA_NOTAS_EXT.PDK_ID_SOLICITUD in(" + lista_solicitudes + ") AND PDK_ID_SOLICITUD = " + Convert.ToString(sol) + " ORDER BY fe_creacion DESC"
                        pconsulta = "select A.id id_nota, A.fe_creacion, A.fe_borrado, A.mensaje, A.estatus, A.usu_creacion, A.usu_borrado, A.PDK_ID_SOLICITUD, A.leido , A.Tarea ,B.PDK_USU_NOMBRE +' '+ B.PDK_USU_APE_PAT +' '+ B.PDK_USU_APE_MAT as Usuario from PDK_CAJA_NOTAS_EXT A INNER JOIN PDK_USUARIO B on B.PDK_ID_USUARIO = A.usu_creacion WHERE estatus = 1  and A.PDK_ID_SOLICITUD in(" + lista_solicitudes + ") AND A.PDK_ID_SOLICITUD = " + Convert.ToString(sol) + " ORDER BY A.fe_creacion DESC"
                        tmp = BD.EjecutarQuery(pconsulta)
                        If tmp.Tables(0).Rows.Count > 0 Then
                            Session("dtsConsulta") = tmp
                            gvMensajes.DataSource = tmp
                            gvMensajes.DataBind()
                        Else
                            'la solicitud no tiene mensajes
                            Dim dss As New DataSet
                            dss = BD.EjecutarQuery("select PDK_ID_SECCCERO from PDK_TAB_SECCION_CERO WHERE PDK_ID_SECCCERO = " & Text1.Value)

                            If (dss.Tables.Count > 0) Then
                                If (dss.Tables(0).Rows.Count > 0) Then
                                    If lista_solicitudes.Contains(Convert.ToString(sol).ToString()) Then
                                        gvMensajes.Visible = False
                                        gvMensajes.Dispose()
                                        gvMensajes.DataSource = Nothing
                                        tmp.Clear()
                                    Else
                                        Master.MensajeError("Ingrese una solictud valida")
                                        textEditor.Visible = False
                                        btnGuardar.Visible = False
                                    End If
                                Else
                                    Master.MensajeError("Ingrese una solictud valida")
                                    textEditor.Visible = False
                                    btnGuardar.Visible = False
                                End If

                            End If

                           
                        End If
                    Else
                        'El usuario no tiene mensajes en la caja de notas
                        If lista_solicitudes.Contains(Convert.ToString(sol).ToString()) Then
                            textEditor.Visible = True
                            btnGuardar.Visible = True
                        Else
                            Master.MensajeError("Ingrese una solictud valida")
                            textEditor.Visible = False
                            btnGuardar.Visible = False
                        End If



                        End If


                Catch ex As Exception
                    Master.MensajeError(ex.Message)
                    'textEditor.Visible = True
                    'btnGuardar.Visible = True

                    Try
                        gvMensajes.Visible = False
                        gvMensajes.Dispose()
                        gvMensajes.DataSource = Nothing
                        tmp.Clear()
                        GridViewGral.Visible = True
                        tmp = BD.EjecutarQuery("SELECT A.id id_nota, A.fe_creacion, A.fe_borrado, A.mensaje, A.estatus, A.usu_creacion, A.usu_borrado, A.PDK_ID_SOLICITUD, A.leido , A.Tarea ,B.PDK_USU_NOMBRE +' '+ B.PDK_USU_APE_PAT +' '+ B.PDK_USU_APE_MAT as Usuario FROM PDK_CAJA_NOTAS_EXT A INNER JOIN PDK_USUARIO B on B.PDK_ID_USUARIO = A.usu_creacion WHERE estatus = 1 and leido = 0 and A.PDK_ID_SOLICITUD in(" + lista_solicitudes + ") ORDER BY fe_creacion DESC")
                        Session("dtsConsultaG") = tmp
                        GridViewGral.DataSource = tmp
                        GridViewGral.DataBind()

                    Catch ex1 As Exception
                        gvMensajes.Visible = False
                        gvMensajes.Dispose()
                        gvMensajes.DataSource = Nothing
                        Master.MensajeError(ex.Message)
                    End Try

                End Try
                'End If
            Else
                textEditor.Visible = False
                btnGuardar.Visible = False

                Try
                    gvMensajes.Visible = False
                    gvMensajes.Dispose()
                    gvMensajes.DataSource = Nothing
                    tmp.Clear()
                    GridViewGral.Visible = True
                    'tmp = BD.EjecutarQuery("SELECT A.id id_nota, A.fe_creacion, A.fe_borrado, A.mensaje, A.estatus, A.usu_creacion, A.usu_borrado, A.PDK_ID_SOLICITUD, A.leido , A.Tarea ,B.PDK_USU_NOMBRE +' '+ B.PDK_USU_APE_PAT +' '+ B.PDK_USU_APE_MAT as Usuario FROM PDK_CAJA_NOTAS_EXT A INNER JOIN PDK_USUARIO B on B.PDK_ID_USUARIO = A.usu_creacion WHERE estatus = 1 and leido = 0 and A.PDK_ID_SOLICITUD in(" + lista_solicitudes + ") ORDER BY fe_creacion DESC")
                    'Session("dtsConsultaG") = tmp
                    'GridViewGral.DataSource = tmp
                    'GridViewGral.DataBind()

                Catch ex As Exception
                    gvMensajes.Visible = False
                    gvMensajes.Dispose()
                    gvMensajes.DataSource = Nothing
                    Master.MensajeError(ex.Message)
                End Try
            End If

        Else
            If Text1.Value <> "" Then
            Master.MensajeError("El usuario debe tener solicitudes relacionadas")
        End If

        End If
    End Sub

    Protected Sub btnActualizar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActualizar.Click
        Page_Load(Me, e)
    End Sub

    Function BuscaPermiso(usuario As Int64, Optional ByVal Solicitud As Integer = 0) As String
        'Dim lista_usuario As New List(Of Int32)
        Dim string_in As String = ""
        Dim dsresult As DataSet
        dsresult = BD.EjecutarQuery("EXEC getUsuariosPerfilDist " & usuario & "," & 1 & "," & Solicitud & "")
        If dsresult.Tables.Count > 0 Then
            If dsresult.Tables(0).Rows.Count > 0 Then
                For index As Integer = 0 To dsresult.Tables(0).Rows.Count - 1
                    'lista_usuario.Add(Convert.ToInt32(dsresult.Tables(0).Rows(index).Item("PDK_ID_USUARIO")))
                    string_in += dsresult.Tables(0).Rows(index).Item("PDK_ID_SECCCERO").ToString + " ,"
                Next
            End If
        Else
            dsresult = BD.EjecutarQuery("EXEC getUsuariosPerfilDist " & usuario & "," & 2 & "," & Solicitud & "")
            If dsresult.Tables.Count > 0 Then
                If dsresult.Tables(0).Rows.Count > 0 Then
                    For index As Integer = 0 To dsresult.Tables(0).Rows.Count - 1
                        'lista_usuario.Add(Convert.ToInt32(dsresult.Tables(0).Rows(index).Item("PDK_ID_USUARIO")))
                        string_in += dsresult.Tables(0).Rows(index).Item("PDK_ID_SECCCERO").ToString + " ,"
                    Next
                End If
            End If

        End If
        If string_in.Length > 0 Then
            string_in = string_in.Remove(string_in.Length - 1)
            Return string_in

        Else : Return ""
        End If
        
    End Function

    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        Dim nusu As Int64
        Dim resultusu As Boolean = Int64.TryParse(hdnUsuario.Value, nusu)

        If (resultusu) Then
            BuscaMensajes(2, BuscaPermiso(nusu, Text1.Value))
        End If
        Text1.Value = ""
    End Sub

    Function ObtenTarea(idPantalla As Integer) As String
        Dim tmp As New DataSet

        Dim pconsulta As String = "select A.PDK_TAR_NOMBRE from PDK_CAT_TAREAS A INNER JOIN PDK_REL_PANTALLA_TAREA B ON A.PDK_ID_TAREAS = B.PDK_ID_TAREAS INNER JOIN PDK_PANTALLAS C ON C.PDK_ID_PANTALLAS = B.PDK_ID_PANTALLAS WHERE C.PDK_ID_PANTALLAS = " + idPantalla.ToString()
        tmp = BD.EjecutarQuery(pconsulta)
        If Not IsNothing(tmp.Tables) Then
            If tmp.Tables(0).Rows.Count > 0 Then
                Return tmp.Tables(0).Rows(0).Item("PDK_TAR_NOMBRE").ToString()
            Else
                Return ""
            End If
        Else
            Return ""
        End If

    End Function

    Function ObtenNombre(idUsuario As String) As String
        Dim tmp As New DataSet

        Dim pconsulta As String = "select PDK_USU_NOMBRE from PDK_USUARIO where PDK_ID_USUARIO = " + idUsuario
        tmp = BD.EjecutarQuery(pconsulta)
        If Not IsNothing(tmp.Tables) Then
            If tmp.Tables(0).Rows.Count > 0 Then
                Return tmp.Tables(0).Rows(0).Item("PDK_USU_NOMBRE").ToString()
            Else
                Return ""
            End If
        Else
            Return ""
        End If

    End Function

End Class
