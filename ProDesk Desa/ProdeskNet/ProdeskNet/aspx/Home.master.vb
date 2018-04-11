
Imports System.Data

#Region "trackers"
' YAM-P-208  egonzalez 14/10/2015 Se agregó la carga del path completo de la aplicación al campo oculto 'fullPath' para tenerse a la mano siempre.
' BBV-P-423: RQSOL-04: AVH: 06/12/2016 Se agregan estilos de Less
' BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
' BUG PD 04 : GVARGAS : 11/01/2017 Cambio en fecha que se muestra en español
' BBV-P-423 RQLOGIN GVARGAS 17/05/2017 Cambios CSS Danamicos
' BUG-PD-311 : ERODRIGUEZ: 18/12/2017: Se corrigieron etiquetas de actividad al cambiar de pantalla
' BUG-PD-318:DJUAREZ:21/12/2017:Se realiza cambio para que cuando se pierda la sesion se redirija a la pantalla de login
' RQ-PI7-PD13-3: ERODRIGUEZ: 21/12/2017: Se oculto Estatus de actividad
' RQ-PI7-PD13-4: ERODRIGUEZ: 10/01/2017: se verifica si el parametro del balanceador esta activo
' BUG-PD-351: ERODRIGUEZ: 31/01/2018: Se valida cuando sea nulo el estatus de activo del usuario.
#End Region

Public Class aspx_Home
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'Me.LessCSS.InnerText = "@import url(../CSSDinamics/honda.less); "

        If (Session("agencia") IsNot Nothing) Then
            Dim alianza As String = Session("agencia").ToString()
            If (alianza = "A") Then
                Me.LessCSS.InnerText = "@import url(../CSSDinamics/acura.less); "
            ElseIf (alianza = "H") Then
                Me.LessCSS.InnerText = "@import url(../CSSDinamics/honda.less); "
            ElseIf (alianza = "J") Then
                Me.LessCSS.InnerText = "@import url(../CSSDinamics/jaguar.less); "
            ElseIf (alianza = "S") Then
                Me.LessCSS.InnerText = "@import url(../CSSDinamics/suzuki.less); "
            End If
        End If

        If IsNothing(Session("cveUsuAcc")) Then
            FormsAuthentication.SignOut()
            Response.Redirect("~/Login.aspx", True)
        End If
        Dim strCveUsu As String = Session("cveUsuAcc")
        Dim objUsu As New ProdeskNet.Seguridad.clsUsuario(strCveUsu)

        lblNombreCom.Text = Session("strNombre")
        lblNomUsu.Text = Session("cveUsuAcc")
        hdidUsuario.Value = objUsu.PDK_ID_USUARIO
        hdPerfilUsuario.Value = objUsu.PDK_ID_PERFIL

        Dim strFecha As String = UCase(Now().ToString("dd/MMM/yyyy"))
        lblFechaSistema.Text = strFecha.Replace("JAN", "ENE").Replace("APR", "ABR").Replace("AUG", "AGO").Replace("DEC", "DIC")
        lblMenu.Text = CargaMenu(0, 5, Session("cveUsuAcc"))

        '------- NOMBRE DEL PERFIL-------------------------------
        Dim DatSet As DataSet = ProdeskNet.Seguridad.clsUsuario.ObtenerNombreperfi(objUsu.PDK_ID_PERFIL)
        If DatSet.Tables.Count > 0 AndAlso DatSet.Tables(0).Rows.Count > 0 Then
            lblNomUsuAcc.Text = DatSet.Tables(0).Rows(0).Item("PDK_PER_NOMBRE").ToString




        End If

        'Dim paramSol As Integer = Request.Params("IdFolio")
        'If paramSol = 0 Then
        '    paramSol = Request.Params("Solicitud")
        'End If
        Dim DatSet_bal As DataSet = ProdeskNet.Seguridad.clsUsuario.ObtieneBalanceador()
        If DatSet_bal.Tables(0).Rows(0).Item("balanceo") = 0 Then
            hdBalanceo.Value = False
        ElseIf DatSet_bal.Tables(0).Rows(0).Item("balanceo") = 1 Then
            hdBalanceo.Value = True
        End If


        Dim fillobj As New fillobjetos

        fullPath.Value = Server.MapPath("~\")

        hdACNombre.Value = fillobj.filltxt("tags", Me.lblNomUsu.Text)
        hdACDistribuidor.Value = fillobj.filltxt("txtDistribuidor", "")
        If IsPostBack Then
            If hdBalanceo.Value = "True" Then
                Dim DatSet_VU As DataSet = ProdeskNet.Seguridad.clsUsuario.obtenerValidadUsuario("", 3, objUsu.PDK_ID_USUARIO)
                If Not IsNothing(DatSet_VU) AndAlso DatSet_VU.Tables.Count > 0 AndAlso DatSet_VU.Tables(0).Rows.Count() > 0 Then
                    If DatSet_VU.Tables(0).Rows(0).Item("ESTATUS_ACTIVO") = 0 Then
                        hdEstatus_Activo.Value = False
                    ElseIf DatSet_VU.Tables(0).Rows(0).Item("ESTATUS_ACTIVO") = 1 Then
                        hdEstatus_Activo.Value = True
                    End If

                Else
                    hdEstatus_Activo.Value = False
                End If


                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Actividad", "ActualizaActividad()", True)
            End If
        End If
        If Not IsPostBack Then

            'If paramSol <> 0 Then
            '    'Me.lblSol.Text = paramSol
            '    'Me.lblSol2.Text = paramSol
            '    'fillddlUsuario(paramSol)
            '    fillNotAn(paramSol)
            'End If

            'fillAreaSol(Session("IdUsua"))
            If hdBalanceo.Value = "True" Then
                Dim DatSet_VU As DataSet = ProdeskNet.Seguridad.clsUsuario.obtenerValidadUsuario("", 3, objUsu.PDK_ID_USUARIO)
                If Not IsNothing(DatSet_VU) AndAlso DatSet_VU.Tables.Count > 0 AndAlso DatSet_VU.Tables(0).Rows.Count() > 0 Then
                    If DatSet_VU.Tables(0).Rows(0).Item("ESTATUS_ACTIVO") = 0 Then
                        hdEstatus_Activo.Value = False
                    ElseIf DatSet_VU.Tables(0).Rows(0).Item("ESTATUS_ACTIVO") = 1 Then
                        hdEstatus_Activo.Value = True
                    End If

                Else
                    hdEstatus_Activo.Value = False
                End If
             

                If (hdEstatus_Activo.Value = "") Then
                    Dim datainicio As DataSet = ProdeskNet.Seguridad.clsUsuario.InicioSesionEstatus(objUsu.PDK_ID_USUARIO)

                End If
            End If

            'llenalista()

            Dim BD As New ProdeskNet.BD.clsManejaBD
            gvStatusCol.DataSource = BD.EjecutarQuery("select fcDescripcionEstatus Descripcion, fcColor Color from PDK_STATUSPROCESO")
            gvStatusCol.DataBind()
            For a As Integer = 0 To gvStatusCol.Rows.Count - 1
                Select Case gvStatusCol.Rows(a).Cells(1).Text
                    Case "Blanco"
                        gvStatusCol.Rows(a).Cells(1).BackColor = Drawing.Color.White
                    Case "Amarillo"
                        gvStatusCol.Rows(a).Cells(1).BackColor = Drawing.Color.Yellow
                    Case "Verde"
                        gvStatusCol.Rows(a).Cells(1).BackColor = Drawing.Color.Green
                    Case "Rojo"
                        gvStatusCol.Rows(a).Cells(1).BackColor = Drawing.Color.Red
                    Case "Azul"
                        gvStatusCol.Rows(a).Cells(1).BackColor = Drawing.ColorTranslator.FromHtml("#0087FF")

                End Select
            Next
        End If

    End Sub

    'Public Sub fillNotAn(ByVal solicitud As String)

    '    Dim BD As New ProdeskNet.BD.clsManejaBD
    '    Dim ds As New DataSet
    '    ds = BD.EjecutarQuery("select PDK_USU_NOMBRE + ' ' + PDK_USU_APE_PAT + ' ' + PDK_USU_APE_MAT 'Alta', fcNoficacionAC 'Notificacion', fdFechaNotificacionAC 'Fecha' from PDK_NOTOBS_ANALISIS_CREDITO ac inner join PDK_USUARIO pu on ac.fcUsuarioAltaAC = pu.PDK_ID_USUARIO where PDK_ID_SECCCERO = " & solicitud)
    '    gvNotAn.DataSource = ds
    '    gvNotAn.DataBind()

    'End Sub

    'Public Sub fillAreaSol(ByVal usuario As String)
    '    Dim BD As New ProdeskNet.BD.clsManejaBD
    '    Dim ds As New DataSet
    '    Dim dslabel As New DataSet
    '    ds = BD.EjecutarQuery("if OBJECT_ID('PDK_TAB_SOLICITANTE')is not null select ltrim(isnull(opsol.PDK_ID_SOLICITUD, 0)) Solicitud, isnull(APELLIDO_PATERNO, '') + ' ' + isnull(APELLIDO_MATERNO, '') + ' ' + isnull(NOMBRE1, '') + ' ' + isnull(NOMBRE2, '') as Nombre, isnull(PDK_PAR_SIS_PARAMETRO, 'NO DEFINIDO') as Estatus from (select PDK_ID_SOLICITUD, max(PDK_ID_TAREAS)PDK_ID_TAREAS from PDK_OPE_SOLICITUD group by PDK_ID_SOLICITUD) opsol2 inner join PDK_OPE_SOLICITUD opsol on opsol.PDK_ID_SOLICITUD = opsol2.PDK_ID_SOLICITUD and opsol.PDK_ID_TAREAS = opsol2.PDK_ID_TAREAS left outer join PDK_TAB_SOLICITANTE tsol on opsol.PDK_ID_SOLICITUD = tsol.PDK_ID_SECCCERO left outer join PDK_PARAMETROS_SISTEMA psis on opsol.PDK_OPE_STATUS_TAREA = psis.PDK_ID_PARAMETROS_SISTEMA and PDK_PAR_SIS_ID_PADRE = 38 where PDK_OPE_USU_ASIGNADO = " & usuario & ";")
    '    dslabel = BD.EjecutarQuery("select count(distinct pdk_id_solicitud) from PDK_OPE_SOLICITUD where PDK_OPE_USU_ASIGNADO = " & usuario & " and PDK_OPE_STATUS_TAREA = 39")
    '    Me.tbAreaSol.DataSource = ds
    '    'Me.lblNoSolicitudes.Text = dslabel.Tables(0).Rows(0)(0).ToString()
    '    Me.tbAreaSol.DataBind()
    'End Sub

    'Public Sub fillddlUsuario(ByVal solicitud As Integer)
    '    Dim dsUsu As New DataSet
    '    Dim BD As New ProdeskNet.BD.clsManejaBD
    '    dsUsu = BD.EjecutarQuery("select distinct usu.PDK_USU_CLAVE AS selectedvalue, bita.PDK_CLAVE_USUARIO as selectedindex from PDK_TAREA_BITACORA bita inner join PDK_USUARIO usu on bita.PDK_CLAVE_USUARIO = usu.PDK_CLAVE_USUARIO where PDK_ID_SOLICITUD = " & solicitud)
    '    Me.ddlUsuario.DataSource = dsUsu
    '    Me.ddlUsuario.DataTextField = "selectedvalue"
    '    Me.ddlUsuario.DataValueField = "selectedindex"
    '    Me.ddlUsuario.DataBind()
    '    Me.ddlUsuario2.DataSource = dsUsu
    '    Me.ddlUsuario2.DataTextField = "selectedvalue"
    '    Me.ddlUsuario2.DataValueField = "selectedindex"
    '    Me.ddlUsuario2.DataBind()
    'End Sub

    Public Event refrescar_click As EventHandler

    'Public ReadOnly Property boton() As Button
    '    Get
    '        Return Me.btnBuscarCliente
    '    End Get
    'End Property

    Public Sub llenalista()
        Dim strCveUsu As String = Session("cveUsuAcc")
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim ulist As New HtmlGenericControl("ul")
        ulist = Me.lista

        Dim dslist As New DataSet
        dslist = BD.EjecutarQuery("select PDK_ID_SECCCERO, 'Sol ' + ltrim(PDK_ID_SECCCERO) + ': ' + fcNotificacion from PDK_NOTIFICACIONES noti inner join PDK_USUARIO usu on noti.fcUsuarioNotificacion = usu.PDK_ID_USUARIO where PDK_USU_CLAVE = '" & strCveUsu & "'; ")
        For Each A As DataRow In dslist.Tables(0).Rows
            Dim item1 As New HtmlGenericControl("li")
            item1.ID = A.Item(0)
            item1.InnerText = A.Item(1)
            ulist.Controls.Add(item1)
        Next
    End Sub

    'Protected Sub btn_Buscar_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarCliente.Click
    '    RaiseEvent refrescar_click(sender, e)
    'End Sub

    Public Sub MensajeError(ByVal strError As String)
        If Trim(strError) <> "" Then
            Master.MensajeError(strError)
        End If
    End Sub

    Public Sub Script(ByVal strScript As String)
        If Trim(strScript) <> "" Then
            Master.Script(strScript)
        End If
    End Sub
    Public Sub EjecutaJS(ByVal script As String)
        If Trim(script) <> "" Then
            Master.EjecutaJS(script)

        End If
    End Sub


    Public Sub RegistraControl(ByVal Ctrl As Control)
        If Not IsNothing(Ctrl) Then
            Master.RegistraControl(Ctrl)
        End If
    End Sub

    Private Function CargaMenu(ByVal intPadre As Integer, ByVal intTipoObj As Integer, ByVal strUsu As String) As String
        CargaMenu = ""
        Try
            Dim dtsRes As New DataSet
            Dim intHij As Integer = 0
            Dim intRow As Integer = 0
            Dim intObj As Integer = 0
            Dim intNiv As Integer = 0
            Dim intAccDir As Integer = 0
            Dim strTexto As String = ""
            Dim strLink As String = ""
            Dim strPaso As String = ""
            Dim strHijo As String = ""
            Dim intHijoCarga As Integer = 0
            Dim objmenu As New ProdeskNet.Seguridad.clsMenu
            Dim objPerm As New ProdeskNet.Seguridad.clsPermisos
            Dim objUsu As New ProdeskNet.Seguridad.clsUsuario(strUsu)

            objmenu.PDK_MEN_ID_PADRE = intPadre
            objmenu.PDK_MEN_OBJETO = intTipoObj
            dtsRes = objmenu.ManejaMenu(1)

            If Trim(objmenu.ErrorObjeto) = "" Then
                If dtsRes.Tables.Count > 0 Then
                    intRow = dtsRes.Tables(0).Rows.Count
                    If intRow > 0 Then
                        For intHij = 0 To intRow - 1
                            'cargamos los menus y submenus
                            'intObj = dtsRes.Tables(0).Rows(intHij).Item("PDK_MEN_OBJETO")
                            intObj = dtsRes.Tables(0).Rows(intHij).Item("PDK_ID_MENU")
                            intNiv = dtsRes.Tables(0).Rows(intHij).Item("PDK_MEN_NIVEL")
                            intAccDir = dtsRes.Tables(0).Rows(intHij).Item("PDK_MEN_ACCESO_DIRECTO")
                            strTexto = dtsRes.Tables(0).Rows(intHij).Item("PDK_MEN_DESCRIPCION")
                            strLink = dtsRes.Tables(0).Rows(intHij).Item("PDK_MEN_LINK")

                            objPerm = New ProdeskNet.Seguridad.clsPermisos

                            'revisamos si es seguridad por perfil o por usuario

                            objPerm.CargarPermiso(0, intObj, strUsu) ' por usurio


                            If objPerm.ErrorPermisos = "" Then
                                If objPerm.PDK_PER_STATUS = 2 Then
                                    'OBTENEMOS LOS HIJOS
                                    strHijo = CargaMenu(intObj, 6, strUsu)

                                    'CARGAMOS LOS OBJETOS
                                    If intTipoObj = 5 Then
                                        If Trim(strHijo) <> "" Then
                                            strPaso += Chr(10) & _
                                                       "<table border=|0| cellspacing=|0|>" & Chr(10) & _
                                                       "    <tr class=|menu|>" & Chr(10) & _
                                                       "        <td valign=|top|><label onmouseover=|timer1=setTimeout(function(){MuestraMenu('ID" & intObj & "');}, 250);| onmouseout=|clearTimeout(timer1);|>" & strTexto & "</label></td>" & Chr(10) & _
                                                       "    </tr>" & Chr(10) & _
                                                       "</table>" & Chr(10) & strHijo
                                        End If

                                    Else
                                        If intHijoCarga = 0 Then
                                            strPaso = "<div id=|ID" & intPadre & "| >" & Chr(10) & _
                                                      "    <table class = |allGv| border=|0| cellspacing=|0|>" & Chr(10)
                                            intHijoCarga = 1
                                        End If

                                        If Trim$(strHijo) = "" Then
                                            strPaso += "        <tr class=|submenu| id = | MenuID" & intPadre & "| style= |display:none;|>" & Chr(10) & _
                                                       "            <td style=|width:250px;| valign=|top| onclick=|CargaPantalla('" & strLink & "');|>" & strTexto & "</td>" & Chr(10) & _
                                                       "        </tr>" & Chr(10)
                                        Else
                                            strPaso += "        <tr class=|menu|>" & Chr(10) & _
                                                       "            <td valign=|top| onclick=|MuestraMenu('ID" & intObj & "');|>" & UCase(strTexto) & Chr(10) & _
                                                       strHijo & Chr(10) & _
                                                       "            </td>" & Chr(10) & _
                                                       "        </tr>" & Chr(10)
                                        End If
                                    End If
                                End If

                                If intHij = intRow - 1 And intHijoCarga > 0 Then
                                    strPaso += "    </table>" & Chr(10) & _
                                               "</div>" & Chr(10)
                                End If

                                If intAccDir = 1 Then
                                    lblAccesoDir.Text &= "<span class='linkAccesos' onclick=" & """" & _
                                                             "CargaPantalla('" & strLink & "');" & """ " & _
                                                             "style='CURSOR:hand'>" & _
                                                             IIf(Trim(lblAccesoDir.Text) = "", "", "| ") & UCase(strTexto) & _
                                                             " </span>"
                                End If
                            End If
                        Next
                    End If
                End If
            End If

            CargaMenu = Replace(strPaso, "|", """")

        Catch ex As Exception

        End Try
    End Function
    Private Function Sangria(ByVal intNiv As Integer) As String
        Sangria = ""
        Dim intR As Integer = 0
        For intR = 2 To intNiv
            Sangria += "&nbsp;&nbsp;&nbsp;&nbsp;"
        Next
    End Function

    Public Sub MsjErrorRedirect(ByVal strError As String, ByVal strURL As String)
        If Trim(strError) <> "" Then
            Master.MsjErrorRedirect(strError, strURL)
        End If
    End Sub
    'MsjErrorRedirect

    Protected Sub ButtonHideSalir_Click(sender As Object, e As EventArgs)
        If (hdidUsuario.Value > 0) Then
            Dim datacierre As DataSet = ProdeskNet.Seguridad.clsUsuario.CierraSesionEstatus(hdidUsuario.Value)
        End If
    End Sub
End Class