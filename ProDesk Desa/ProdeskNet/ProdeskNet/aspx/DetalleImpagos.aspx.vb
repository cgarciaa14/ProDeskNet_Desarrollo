Imports System.Data
Imports ProdeskNet.BD
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports System.Data.SqlClient
Imports System.Web.HttpUtility
'--BUG-PD-272: MGARCIA: 23/11/2017: Se agrego pantalla DetalleImpagos y su funcionalidad
'--BUG-PD-290: MGARCIA: 05/12/2017: Detalle Impagos en Menu--



Partial Class aspx_DetalleImpagos
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        GrdDetalleImpagos.DataSource = New DataTable()
        GrdDetalleImpagos.DataBind()
           

    End Sub

    Protected Sub btnBuscaDetalle_Click(sender As Object, e As EventArgs)
        Dim idsol As Integer = txtDetalle.Value

        Dim ds_siguienteTarea As DataSet
        Dim objCatalogos As New ProdeskNet.SN.clsCatalogos
        objCatalogos.Parametro = Request("idPantalla")
        ds_siguienteTarea = objCatalogos.Catalogos_Sol(20, idsol)
        If ds_siguienteTarea.Tables.Count > 0 Then
            If ds_siguienteTarea.Tables(0).Rows.Count > 0 Then
                Session("dtsConsultaG") = ds_siguienteTarea.Tables(0)
                GrdDetalleImpagos.DataSource = ds_siguienteTarea.Tables(0)
                GrdDetalleImpagos.DataBind()
                txtTotalMonto.Text = ds_siguienteTarea.Tables(1).Rows(0).Item("Total").ToString
            Else
                GrdDetalleImpagos.DataSource = New DataTable()
                GrdDetalleImpagos.DataBind()
            End If
        End If

    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        txtDetalle.Value = String.Empty
        txtTotalMonto.Text = String.Empty

    End Sub
End Class
