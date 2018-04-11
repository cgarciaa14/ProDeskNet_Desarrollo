Imports System.Data
Imports ProdeskNet.Catalogos

Partial Class aspx_caratulaFacturacion

    Inherits System.Web.UI.Page
    Dim BD As New ProdeskNet.BD.clsManejaBD
    Dim dc As New ProdeskNet.Catalogos.clsDatosCliente
    Dim es As New ProdeskNet.Catalogos.clsStatusSolicitud

    Sub page_preinit(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreInit
        'If Request.QueryString("MP") = 1 Then
        '    Me.MasterPageFile = "~/aspx/MasterPageVacia.Master"
        'End If        
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim dsNom As DataSet
            Dim intEnable As Integer = 0
            Me.lblSolicitud.Text = Request.QueryString("sol")
            hdnIdFolio.Value = Request.QueryString("sol")
            hdnIdPantalla.Value = Request.QueryString("idpantalla")
            hdnUsua.Value = Session("IdUsua")
            'If Session("Regresar") Is Nothing Then
            '    Session("Regresar") = Request.UrlReferrer.LocalPath
            '    hdRutaEntrada.Value = Request.UrlReferrer.LocalPath
            'End If

            'hdRutaEntrada.Value = Session("Regresar")

            Dim statusTarea As New DataSet

            statusTarea = BD.EjecutarQuery("SELECT CASE WHEN PDK_OPE_STATUS_TAREA = 41 THEN 'TERMINADA' Else 'ACTIVA' End ,* FROM PDK_OPE_SOLICITUD WHERE PDK_ID_SOLICITUD = " & Request("sol") & " AND PDK_ID_TAREAS = 13")

            If statusTarea.Tables(0).Rows.Count() > 0 Then
                Me.statusTarea.Value = statusTarea.Tables(0).Rows(0)(0)
            Else
                Me.statusTarea.Value = "INEXISTENTE"
            End If

            es.getStatusSol(Request.QueryString("sol"))
            Me.lblStCredito.Text = es.PStCredito
            Me.lblStDocumento.Text = es.PStDocumento

            Dim dsFac As New DataSet

            dsFac = BD.EjecutarQuery("select isnull(sum(importe),0) from (select pdk_id_secccero, sum(pdk_fd_FAct_importe) as importe from pdk_FacturaActivo group by pdk_id_secccero union select pdk_id_secccero, sum(pdk_fd_FAcc_importe) from pdk_FacturaAccesorios group by pdk_id_secccero union select pdk_id_secccero, isnull(sum(pdk_fd_FInt_importe), 0) from pdk_FacturaIntangibles 	group by pdk_id_secccero union select pdk_id_secccero, isnull(sum(pdk_fd_FCom_importe), 0) from pdk_FacturaComercializacion group by pdk_id_secccero)a where a.pdk_id_secccero = " & Request.QueryString("sol"))

            If Not IsDBNull(dsFac) Then
                If dsFac.Tables(0).Rows.Count() > 0 Then
                    Me.txtSumaFacturas.Value = dsFac.Tables(0).Rows(0)(0)
                End If
            End If

            Try
                dsNom = BD.EjecutarQuery("EXEC sp_GenerarRura " & hdnIdFolio.Value & "," & hdnIdPantalla.Value & "," & hdnUsua.Value & "")
                If dsNom.Tables(0).Rows.Count > 0 AndAlso dsNom.Tables.Count > 0 Then
                    hdnResultado.Value = dsNom.Tables(0).Rows(0).Item("RUTA")
                    hdnResultado1.Value = dsNom.Tables(1).Rows(0).Item("RUTA2")
                    hdnResultado2.Value = dsNom.Tables(2).Rows(0).Item("RUTA3")

                End If
            Catch ex As Exception
                hdnResultado.Value = Session("Regresar")
            End Try

            Try
                intEnable = CInt(Request.QueryString("Enable"))

            Catch ex As Exception
                intEnable = 0
            End Try

            dc.GetDatosCliente(lblSolicitud.Text)

            lblCliente.Text = dc.propNombreCompleto

        End If
    End Sub

End Class