#Region "Trackers"
'BBV-P-423 - RQADM-31 24/03/2017 MAPH Mensajes de Red
'BUG-PD-43 28/04/2017 MPUESTO Solución a los siguientes temas:
'           * Validaciones de la búsqueda para no rebasar el rango del Int32.
'           * Corrección de la URL de visor documental
'BUG-PD-44:MPUESTO:08/05/2017:Corrección de los siguientes puntos:
'           * Modificación de interfaz para mostrar en naranja las solicitudes que han sido rechazadas
'           * Concordancia de solicitudes en Panel de Seguimiento y consulta de Mensajes de Red
'BBV-P-423 - RQADM-25  09/05/2017 erodriguez: Se agrego funcionalidad para Mesa Pool de Crédito
'BUG-PD-57 GVARGAS 18/05/2017 Cambios al cancelar tarea
'BBVA-P-423 - RQXLS1 23/05/2017 CGARCIA Se agrego la consulta de alianza por registro
'BUG-PD-155: ERODRIGUEZ 15/07/2017 Se realizo validación, si es tarea automatica no se envia a mesa pool de credito
'BUG-PD-217: ERODRIGUEZ:    02/10/2017:Se agrego guardar en bitacora turnados de mensajes de red.
'BUG-PD-231:MPUESTO:11/10/2017:Corrección del orden de turnar, mensaje y búsqueda para reflejar cambios en solicitudes turnadas.
#End Region

Imports ProdeskNet.Catalogos
Imports System.Data
Imports ProdeskNet.SN
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Configurcion

Partial Class aspx_mensajesRed
    Inherits System.Web.UI.Page
    Dim _clsSolictds As New clsSolictds
    Dim _clsSolicitudes As New clsSolicitudes(0)
    Dim _listaSolicitud As New List(Of Integer)
    Dim _NoSolicitud As Integer? = Nothing
    Dim _FechaInicio As String
    Dim _FechaFin As String
    Dim _FechaGenerica() As String
    Dim _NombreCliente As String
    Dim _RFCCliente As String
    Dim _objCatalogos As New ProdeskNet.SN.clsCatalogos
    Dim _idPantalla As Integer = 95
    Dim _clsCuestionarioSolvsID As New clsCuestionarioSolvsID()
    Dim _dtsResult As New DataSet
    Dim _clsCatTareas As New clsCatTareas()
    Dim _clsMesaPoolCredito As New clsMesaPoolCredito()

    Protected Sub mensajesRed_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btnBuscar_Click(sender, e)
            poblarComboTurnar()
        End If

    End Sub
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs)
        If Convert.ToInt64(IIf(tbxNumeroSolicitud.Text <> String.Empty, tbxNumeroSolicitud.Text, "0")) > Int32.MaxValue Then
            ChangeVisibility(False)
        Else
            If tbxNumeroSolicitud.Text <> String.Empty Then
                _NoSolicitud = Convert.ToInt32(tbxNumeroSolicitud.Text)
            Else
                _NoSolicitud = 0
            End If
            _FechaInicio = ""
            _FechaFin = ""
            If txtFechaInicio.Value.ToString() <> String.Empty Then
                _FechaGenerica = txtFechaInicio.Value.ToString().Split("/")
                _FechaInicio = _FechaGenerica(2) + "/" + _FechaGenerica(1) + "/" + _FechaGenerica(0)
            End If
            If txtFechaFin.Value.ToString() <> String.Empty Then
                _FechaGenerica = txtFechaFin.Value.ToString().Split("/")
                _FechaFin = _FechaGenerica(2) + "/" + _FechaGenerica(1) + "/" + _FechaGenerica(0)
            End If
            _NombreCliente = tbxNombreCliente.Text.Replace(" ", "%")
            _RFCCliente = tbxRFCCliente.Text.Replace(" ", "%")
            _clsSolictds = _clsSolictds.obtenerSolicitudes(NoSolicitud:=_NoSolicitud, _
                                                           FechaInicio:=_FechaInicio, _
                                                           FechaFin:=_FechaFin, _
                                                           NombreCliente:=_NombreCliente, _
                                                           RFCCliente:=_RFCCliente)
            repSolicitudes.DataSource = Nothing
            repSolicitudes.DataBind()
            If Not _clsSolictds Is Nothing And _clsSolictds.Count > 0 Then
                repSolicitudes.DataSource = _clsSolictds
                repSolicitudes.DataBind()
                ChangeVisibility(True)
            Else
                ChangeVisibility(False)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Cambia la visibilidad de los div del repeater para mostrar resultados u ocultarlo y mostrar el mensaje "Sin resultados"
    ''' </summary>
    ''' <param name="divStatus">Visibilidad del grid de resultados</param>
    ''' <remarks></remarks>
    Private Sub ChangeVisibility(ByVal divStatus As Boolean)
        divEmptyTableResult.Visible = Not divStatus
        divTableResult.Visible = divStatus
        divTurnar.Visible = divStatus
    End Sub


    Protected Sub btnLimpiarResultados_Click(sender As Object, e As EventArgs)
        tbxNombreCliente.Text = ""
        tbxNumeroSolicitud.Text = ""
        tbxRFCCliente.Text = ""
        txtFechaInicio.Value = ""
        txtFechaFin.Value = ""
        btnBuscar_Click(sender, e)
    End Sub

    Protected Sub btnVisorDocumental_Click(sender As Object, e As EventArgs)
        Dim ds As DataSet

        _objCatalogos.Parametro = Me.hdnNumeroSolicitud.Value.ToString()
        ds = _objCatalogos.Catalogos(7)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Dim Cliente_bbva As String
                Cliente_bbva = ds.Tables(0).Rows(0).Item("CLIENTE_BBVA")
                'BUG -PD - 43
                If Cliente_bbva <> String.Empty And Cliente_bbva <> "0" Then
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?&folio=" + Me.hdnNumeroSolicitud.Value.ToString() + "&cliente=" + Cliente_bbva.ToString + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbrirVisor", "window.open('../Comparador.aspx?&folio=" + Me.hdnNumeroSolicitud.Value.ToString() + "','popupVisor','width=1800,height=1000,left=-10,top=0,resizable');", True)

                End If
            End If
        End If
    End Sub

    Private Sub poblarComboTurnar()
        Dim dtsres As New DataSet
        Dim objCombo As New clsParametros
        _clsCuestionarioSolvsID._ID_PANT = _idPantalla
        dtsres = _clsCuestionarioSolvsID.getTurnar()
        If dtsres.Tables.Count > 0 And dtsres.Tables(0).Rows.Count > 0 Then
            objCombo.LlenaCombos(dtsres, "TURNAR_NOMBRE", "PDK_ID_TAREAS", ddlSeleccionTurnar, True, True)
        End If

    End Sub

    ''' <summary>
    ''' Este método le da poder a esta pantalla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGuardaTurnar_Click(sender As Object, e As EventArgs)
        Dim _strMessage As String = String.Empty
        Dim ListaIdBit As New List(Of Integer)
        _listaSolicitud.Clear()

        If ddlSeleccionTurnar.SelectedValue <> 0 Then


            For index As Integer = 0 To Me.repSolicitudes.Items.Count - 1
                Dim chk As CheckBox = CType(repSolicitudes.Items(index).FindControl("chkOption"), CheckBox)
                If chk.Checked Then

                    Dim hdnIdGrilla As HiddenField = CType(repSolicitudes.Items(index).FindControl("hdnId"), HiddenField)
                    _listaSolicitud.Add(Convert.ToInt32(hdnIdGrilla.Value))
                End If
            Next
            If _listaSolicitud.Count > 0 Then

                For Each _intSolicitud As Integer In _listaSolicitud
                    _clsSolicitudes = New clsSolicitudes(_intSolicitud)
                    _clsSolicitudes.PDK_ID_SOLICITUD = _intSolicitud        'ValNegocio(1) y ManejaTarea(4,3)
                    _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")   'ValNegocio(1) y ManejaTarea(4,3)

                    If ddlSeleccionTurnar.SelectedValue < 0 Then
                        _clsSolicitudes.ManejaTarea(3)
                    Else
                        'BUG-PD-217  Se guardan datos en la bitacora
                        Dim dtsBit As New DataSet
                        Dim _clsMsjRedBit As clsMensajesRedBitacora
                        _clsMsjRedBit = New clsMensajesRedBitacora()
                        _clsMsjRedBit.ID_SOLICITUD = _intSolicitud
                        _clsMsjRedBit.ID_TAREA_TURNADO = ddlSeleccionTurnar.SelectedValue
                        _clsMsjRedBit.ID_USUARIO = Session("IdUsua")
                        _clsMsjRedBit.ESTATUS = 0
                        dtsBit = _clsMsjRedBit.ManejaBitacoraMsjRed(2)
                        If Not IsDBNull(dtsBit.Tables(0).Rows(0).Item("id_turnado")) Then
                            ListaIdBit.Add(dtsBit.Tables(0).Rows(0).Item("id_turnado"))
                        End If

                        'BUG-PD-217  Se guardan datos en la bitacora

                        If ddlSeleccionTurnar.SelectedValue = 96 Then 'CUANDO SE TRATA DE POOL CREDITO
                            Try
                                _clsMesaPoolCredito = New clsMesaPoolCredito()
                                _clsMesaPoolCredito.PDK_ID_SOLICITUD = _intSolicitud
                                _clsMesaPoolCredito.PDK_ID_PANTALLA = ddlSeleccionTurnar.SelectedValue
                                _dtsResult = _clsMesaPoolCredito.ConsultaSolicitudPool(3)
                                If _dtsResult.Tables.Count > 0 Then
                                    If _dtsResult.Tables(0).Rows.Count > 0 Then
                                        If _dtsResult.Tables(0).Rows(0).Item("MENSAJE").ToString() <> "TAREA EXITOSA" Then
                                            _strMessage = IIf(_strMessage = String.Empty, _intSolicitud.ToString(), ", " & _intSolicitud.ToString())
                                        End If
                                    End If
                                End If
                            Catch ex As Exception
                                _strMessage = IIf(_strMessage = String.Empty, _intSolicitud.ToString(), ", " & _intSolicitud.ToString())
                            End Try
                        Else

                            Try
                                _clsSolicitudes.BOTON = 64                                                  'ValNegocio(1)
                                _clsSolicitudes.PDK_ID_CAT_RESULTADO = ddlSeleccionTurnar.SelectedValue     'ValNegocio(1)
                                _dtsResult = _clsSolicitudes.ValNegocio(1)

                                If _dtsResult.Tables(0).Rows(0).Item("MENSAJE").ToString() <> "Tarea Exitosa" Then
                                    _strMessage = IIf(_strMessage = String.Empty, _intSolicitud.ToString(), ", " & _intSolicitud.ToString())
                                Else
                                    _clsSolicitudes.Estatus = 231    'ManejaTarea(4)
                                    _clsSolicitudes.ManejaTarea(4)
                                End If
                            Catch ex As Exception
                                _strMessage = IIf(_strMessage = String.Empty, _intSolicitud.ToString(), ", " & _intSolicitud.ToString())
                            End Try
                        End If
                    End If
                Next
                If _strMessage = String.Empty Then

                    'BUG-PD-217  Se confirman datos en la bitacora
                    If ListaIdBit.Count > 0 Then
                        For Each _intTurn As Integer In ListaIdBit
                            Dim _clsMsjRedBit As clsMensajesRedBitacora
                            _clsMsjRedBit = New clsMensajesRedBitacora()
                            _clsMsjRedBit.ID_TURNADO = _intTurn
                            _clsMsjRedBit.ManejaBitacoraMsjRed(3)
                        Next
                    End If

                    'BUG-PD-217  Se confirman datos en la bitacora

                    'BUG-PD-217  Se confirma estatus de guardado
                    'BUG-PD-231:MPUESTO:11/10/2017:Inversión de las siguientes dos lineas.
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "TurnarOK", "PopUpLetrero('La(s) tarea(s) han sido turnada(s) a otra mesa');", True)
                    btnBuscar_Click(sender, e)
                Else
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "TurnarError", "PopUpLetrero('Error al turnar la(S) solicitud(es): " & _strMessage & "');", True)

                End If

            Else
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "GrillaSinSeleccion", "alert('¡Debe seleccionar un registro!')", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "ddlTurnarSinSeleccion", "alert('Seleccione una acción a realizar.')", True)
        End If

    End Sub

    Public Function CutText(ByVal str As Object) As String
        Dim text = CType(str, String)
        If text.Length > 20 Then
            text = text.Substring(0, 20) + "..."
        End If
        Return text
    End Function

    Public Function GetStyle(ByVal str As Object) As String
        Dim Text As String = ""
        If str.ToString().ToUpper().Contains("CANCEL") Then
            Text = "style = 'color:#F6891E'"
        End If
        Return Text
    End Function

End Class
