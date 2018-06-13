'-- RQ-INB203INB204INB209 JBEJAR: 18/07/2017  SPRINT 15 DOCUMENTOS ADICIONALES AGENCIA. 
'-- BUG-PD-177 JBEJAR: 26/07/2017 MEJORAS SE AGREGAN TODOS LOS DOCUMENTOS LIGADAS  A LA SOLICITUD Y CAMBIOS ESTETICOS AL DIV DE DOCUMENTOS. 
'-- BUG-PD-449 GVARGAS 23/05/2018 Cambios llamado solicitudes.

Imports ProdeskNet.Catalogos
Imports System.Data
Imports ProdeskNet.SN
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Configurcion
Imports System.Web.UI.HtmlControls.HtmlInputFile
Partial Class aspx_DocumentAdic
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
    Dim _idPantalla As Integer = 145
    Dim _clsCuestionarioSolvsID As New clsCuestionarioSolvsID()
    Dim _dtsResult As New DataSet
    Dim _clsCatTareas As New clsCatTareas()
    Dim _clsMesaPoolCredito As New clsMesaPoolCredito()

    Protected Sub DocumentoAdic_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            divdocumentos.Visible = False
            btnBuscar_Click(sender, e)

        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs)

        divdocumentos.Visible = False

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
                                                           RFCCliente:=_RFCCliente, _
                                                           Opcion:=3)
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

    End Sub
    Protected Sub chkSelecciona_CheckedChanged(sender As Object, e As EventArgs)
        divTableResult.Style.Add("height", "100px")

        hdPantalla.Value = _idPantalla

        hdusuario.Value = Session("IdUsua")

        Dim _strMessage As String = String.Empty
        _listaSolicitud.Clear()
        For index As Integer = 0 To Me.repSolicitudes.Items.Count - 1
            Dim chk As CheckBox = CType(repSolicitudes.Items(index).FindControl("chkOption"), CheckBox)
            If chk.Checked = True Then
                Dim hdnIdGrilla As HiddenField = CType(repSolicitudes.Items(index).FindControl("hdnId"), HiddenField)
                tbValidarObjetos.Visible = True
                _listaSolicitud.Add(Convert.ToInt32(hdnIdGrilla.Value))
                Exit For
            End If
        Next

        If _listaSolicitud.Count > 0 Then
            For Each _intSolicitud As Integer In _listaSolicitud
                _clsSolicitudes = New clsSolicitudes(_intSolicitud)
                _clsSolicitudes.PDK_ID_SOLICITUD = _intSolicitud
                _clsSolicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")   '
                _clsSolictds = _clsSolictds.obtenerSolicitudes(NoSolicitud:=_intSolicitud, _
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
                    hdSolicitud.Value = _intSolicitud
                    Context.Session("idsol") = _intSolicitud
                    CType(repSolicitudes.Items(0).FindControl("chkOption"), CheckBox).Checked = True
                    divdocumentos.Visible = True
                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "CargaGrid", "fillUpload('tbValidarObjetos','hdPantalla,hdSolicitud,hdusuario','per1','')", True)

                Else
                    ChangeVisibility(False)
                End If
            Next
            If _strMessage = String.Empty Then

            Else
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "TurnarError", "PopUpLetrero('Error :" & _strMessage & "');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "GrillaSinSeleccion", "alert('¡Debe seleccionar un registro!')", True)
        End If



    End Sub
    Protected Sub btnLimpiarResultados_Click(sender As Object, e As EventArgs)
        tbxNombreCliente.Text = ""
        tbxNumeroSolicitud.Text = ""
        tbxRFCCliente.Text = ""
        txtFechaInicio.Value = ""
        txtFechaFin.Value = ""
        btnBuscar_Click(sender, e)
        Context.Session("idsol") = Nothing
        divdocumentos.Visible = False
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
