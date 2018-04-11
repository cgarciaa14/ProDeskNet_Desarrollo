Imports ProdeskNet.Catalogos
Imports System.Data
Imports ProdeskNet.Seguridad
Imports ProdeskNet.SN

'BBV-P-423 RQADM-30 MAPH 17/04/2017 Pantalla Referenciados SEVA 60
Partial Class aspx_altaSEVA
    Inherits System.Web.UI.Page

    Dim dtsres As New DataSet
    Dim objCombo As New clsParametros
    Dim _SEVAsolicitudes As clsSEVASolicitudes
    Dim _SEVAsolicitud As clsSEVASolicitud

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        LlenarComboPersonalidadJuridica()

    End Sub

    Private Sub LlenarComboPersonalidadJuridica()
        Try
            dtsres = clsPersonalidadJuridica.ObtenTodos
            ddlPersonalidadJuridica.DataSource = dtsres

            If dtsres.Tables.Count > 0 And dtsres.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsres, "PDK_PER_NOMBRE", "PDK_ID_PER_JURIDICA", ddlPersonalidadJuridica, True, True)
            End If

        Catch ex As Exception
        End Try
    End Sub


    Protected Sub btnAspAltaSEVA_Click(sender As Object, e As EventArgs)
        _SEVAsolicitudes = New clsSEVASolicitudes()
        _SEVAsolicitud = New clsSEVASolicitud With {.IdPerJuridica = ddlPersonalidadJuridica.SelectedValue, _
                                                    .Nombre_RazonSocial = txtNombre.Value, _
                                                    .SegundoNombre = txtSegundoNombre.Value, _
                                                    .ApPaterno = txtApPaterno.Value, _
                                                    .ApMaterno = txtApMaterno.Value, _
                                                    .RFC = txtRFC.Value, _
                                                    .TelefonoFijo = txtTelParticular.Value, _
                                                    .TelefonoMovil = txtTelMovil.Value, _
                                                    .Email = txtEmail.Value, _
                                                    .NombreEjecutivo = txtNombreCompletoEjec.Value, _
                                                    .UsuarioEjecutivo = txtUsuarioCorpoEjec.Value, _
                                                    .CR_Sucursal = txtSucursalEjec.Value, _
                                                    .EmailEjecutivo = txtEmailEjec.Value _
                                                   }
        _SEVAsolicitudes.GeneraSolicitudSEVA(_SEVAsolicitud)
        If _SEVAsolicitud.FolioSEVA <> String.Empty Then
            txtFolioSEVA.Value = _SEVAsolicitud.FolioSEVA
        End If
    End Sub
End Class
