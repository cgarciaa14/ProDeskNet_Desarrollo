'BUG-PD-50 JBEJAR 18/05/2017 CONFIGURACIONES ESPECIALES 
'BUG-PD-74 JBEJAR 07/06/2017 MENSAJE TAREA EXITOSA. 
Imports ProdeskNet.SN
Imports System.Data
Partial Class aspx_CONFIGESPEC
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                ConsultaDatos()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ConsultaDatos()

        Try
            Dim objconfi As New clsconfes

            Dim dtes = objconfi.GetConfigEspe()
            If objconfi.strError = "" Then
                Me.lblIdConfiguracion.Text = CInt(dtes.Tables(0).Rows(0).Item("PDK_ID_SECCCERO"))
                Me.txtnumeve.Text = CInt(dtes.Tables(0).Rows(0).Item("PDK_NUM_VEHICULOS"))
                Me.txtPeriodo.Text = CInt(dtes.Tables(0).Rows(0).Item("PDK_PERIODO_AUTORIZACION"))
                Me.TextAli.Text = CInt(dtes.Tables(0).Rows(0).Item("PDK_MONTO_ALIANZA"))
                Me.TextMulti.Text = CInt(dtes.Tables(0).Rows(0).Item("PDK_MONTO_MULTIMARCA"))
                Me.txtRiesgo.Text = CInt(dtes.Tables(0).Rows(0).Item("PDK_RIESGO"))
            End If

        Catch ex As Exception

        End Try


    End Sub
    Private Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click


        Try
            Dim objclesp As New clsconfes
            Dim id As Integer = 1
           
            objclesp.ID_CONFIGUR = id
            objclesp.NUM_VEHI = Me.txtnumeve.Text
            objclesp.PERIODO_AUTO = Me.txtPeriodo.Text
            objclesp.MONTO_ALIA = Me.TextAli.Text
            objclesp.MONTO_MULTI = Me.TextMulti.Text
            objclesp.RIESGO = Me.txtRiesgo.Text
            If objclesp.insertaDatosConfigespe Then
                'Me.lblIdConfiguracion.Text = objBuro.PDK_ID_BURO_CONFIGURACION
                Master.MensajeError("Tarea Exitosa.")

            Else
                Master.MensajeError("Los campos son requeridos")
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
