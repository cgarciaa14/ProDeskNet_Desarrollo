#Region "Trackers"
'BBV-P-423 - RQADM-31 22/03/2017 MAPH Cambio de Agencia
'BUG-PD-36  25/04/2017 MAPH Validación del repeater en el front, inclusión de multiselect en el repeater, maxlength a 9 de número de solicitud.
'           27/04/2017 MAPH Cambio de maxlength a 10 de Número de solicitud y validaciones para no rebasar la capacidad de un entero en .NET y SQL SERVER
'BUG-PD-119:  26/06/2017: ERODRIGUEZ Se agregaron validaciones y campos necesarios para guardar un historial de agencias cambiadas por solicitud
#End Region

Imports ProdeskNet.Catalogos
Imports System.Data

Partial Class aspx_manejaCambioAgencia
    Inherits System.Web.UI.Page

    Dim _objDistribuidor As clsDistribuidor
    Dim _clsAgencia As New clsAgencias
    Dim _clsVendedor As New clsVendedores
    Dim _dtsCambioAgencia As New DataSet
    Dim _listaSolicitud As New List(Of Integer)
    Dim listaSolictud As New List(Of clsAgencias.CambiaAgencia)

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Llena_ComboAgencia()
            btnBuscar_Click(sender, e)
        End If

    End Sub

    Private Sub Llena_ComboAgencia()
        _clsAgencia = _clsAgencia.obtenerAgencia()
        ddlAgencia.Items.Add(New ListItem("< SELECCIONAR >", 0))
        For Each agencia As clsAgencia In _clsAgencia
            ddlAgencia.Items.Add(New ListItem(agencia.Nombre, agencia.IdAgencia))
        Next
        ddlVendedor.Items.Add(New ListItem("< SELECCIONAR >", 0))
    End Sub

    Private Sub Llena_ComboVendedor()
        ddlVendedor.Items.Clear()
        _clsVendedor = _clsVendedor.obtenerVendedor(ddlAgencia.SelectedValue)
        ddlVendedor.Items.Add(New ListItem("< SELECCIONAR >", 0))
        For Each vendedor As clsVendedor In _clsVendedor
            ddlVendedor.Items.Add(New ListItem(vendedor.Nombre, vendedor.IdVendedor))
        Next
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs)
        Dim NoSolicitud As Integer? = Nothing
        If Convert.ToInt64(IIf(tbxNumeroSolicitud.Text <> String.Empty, tbxNumeroSolicitud.Text, "0")) > Int32.MaxValue Then
            ChangeVisibility(False)
        Else
            If tbxNumeroSolicitud.Text <> String.Empty Then
                NoSolicitud = Convert.ToInt32(tbxNumeroSolicitud.Text)
            End If
            _dtsCambioAgencia = _clsAgencia.obtenerTablaAgenciasVendedor( _
                NoSolicitud:=IIf(tbxNumeroSolicitud.Text = String.Empty, Nothing, NoSolicitud), _
                NombreSolicitante:=IIf(tbxNombreSolicitante.Text = String.Empty, Nothing, tbxNombreSolicitante.Text.Replace(" ", "%")), _
                RFCSolicitante:=IIf(tbxRFCSolicitante.Text = String.Empty, Nothing, tbxRFCSolicitante.Text))
            repAgenciaVendedor.DataSource = Nothing
            repAgenciaVendedor.DataBind()
            If Not _dtsCambioAgencia Is Nothing And _dtsCambioAgencia.Tables.Count > 0 And _dtsCambioAgencia.Tables(0).Rows.Count > 0 Then
                repAgenciaVendedor.DataSource = _dtsCambioAgencia
                repAgenciaVendedor.DataBind()
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
        divTableHeader.Visible = divStatus
    End Sub

    Protected Sub ddlAgencia_SelectedIndexChanged(sender As Object, e As EventArgs)
        Llena_ComboVendedor()
        Me.updDropDowns.Update()
    End Sub

    Protected Sub btnCambiar_Click(sender As Object, e As EventArgs)
        _listaSolicitud.Clear()
        listaSolictud.Clear()
        Dim hdnAgenciaAnt As HiddenField
        For index As Integer = 0 To Me.repAgenciaVendedor.Items.Count - 1
            Dim chk As CheckBox = CType(repAgenciaVendedor.Items(index).FindControl("chkOption"), CheckBox)
            If chk.Checked Then
                Dim item As New clsAgencias.CambiaAgencia
                Dim hdnIdGrilla As HiddenField = CType(repAgenciaVendedor.Items(index).FindControl("hdnId"), HiddenField)
                Dim hdnIdCotGrilla As HiddenField = CType(repAgenciaVendedor.Items(index).FindControl("hdnIdCot"), HiddenField)
                hdnAgenciaAnt = CType(repAgenciaVendedor.Items(index).FindControl("hdIdAgAnt"), HiddenField)
                _listaSolicitud.Add(Convert.ToInt32(hdnIdGrilla.Value))
                If (hdnIdGrilla.Value <> "") Then
                    item.solictud = hdnIdGrilla.Value
                End If
                If (hdnIdCotGrilla.Value <> "") Then
                    item.cotizacion = hdnIdCotGrilla.Value
                End If
                If (hdnAgenciaAnt.Value <> "") Then
                    item.agencia = hdnAgenciaAnt.Value
                End If

                listaSolictud.Add(item)
            End If
        Next
    
        If _listaSolicitud.Count > 0 Then
            If listaSolictud.Count > 0 Then
                _clsAgencia.guardarCambioAgenciaSolicitud(listaSolictud, Convert.ToInt32(ddlAgencia.SelectedValue))
            End If
            _clsAgencia.cambiarAgenciaSolicitud(_listaSolicitud, Convert.ToInt32(ddlAgencia.SelectedValue), Convert.ToInt32(ddlVendedor.SelectedValue))
            btnBuscar_Click(sender, e)
        End If
    End Sub
End Class
