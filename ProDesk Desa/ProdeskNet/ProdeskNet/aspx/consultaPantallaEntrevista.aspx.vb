Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports System.Data

#Region "trackers"
'INC-B-2019:JDRA:Regresar
' YAM-P-208  egonzalez 26/08/2015 Se agregó la lectura del campo 'PDK_PAR_SIS_VALOR_NUMERO' de la tabla 'PDK_PARAMETROS_SISTEMA'
'    y se asignó al campo oculto hdnValPorcentaje para la validación de porcentaje mínimo acertado
#End Region

Public Class consultaPantallaEntrevista
    Inherits System.Web.UI.Page
    Public intEnable As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim dbset As New DataSet
            HdnPantalla.Value = Request("idPantalla")
            hdnFolio.Value = Request("IdFolio")
            hdnIdUsuario.Value = Session("IdUsua")
            hdHabilitar.Value = Request("Enable")
            If Session("Regresar") Is Nothing Then
                Session("Regresar") = Request.UrlReferrer.LocalPath
                hdRutaEntrada.Value = Request.UrlReferrer.LocalPath
            End If

            hdRutaEntrada.Value = Session("Regresar")

            Try
                dbset = clsPantallaObjeto.OBTENERRUTA(hdnFolio.Value, HdnPantalla.Value, hdnIdUsuario.Value)

                If dbset.Tables(0).Rows.Count > 0 AndAlso dbset.Tables.Count > 0 Then
                    hdnResultado.Value = dbset.Tables(0).Rows(0).Item("RUTA")
                End If
            Catch ex As Exception
                hdnResultado.Value = Session("Regresar")
            End Try



            Try
                intEnable = CInt(Request.QueryString("Enable"))
            Catch ex As Exception
                intEnable = 0
            End Try
            If intEnable = 1 Then
                cmbguardar1.Attributes.Add("style", "display:none;")
                'btnAutorizar.Attributes.Add("style", "display:none;")
                btnCancelar.Attributes.Add("style", "display:none;")
            Else
                btnImprimir.Enabled = False
            End If
            dbset = clsPantallaObjeto.ObtenPersonaJuridica(hdnFolio.Value)
            If dbset.Tables.Count > 0 AndAlso dbset.Tables(0).Rows.Count > 0 Then
                If dbset.Tables(0).Rows(0).Item("PDK_ID_PER_JURIDICA") = 3 Then
                    txtCambioCot.Attributes.Add("style", "display:none;")
                End If
            End If


            dbset = clsPantallaObjeto.OntenerPantallaEntrevista(HdnPantalla.Value)
            If dbset.Tables.Count > 0 AndAlso dbset.Tables(0).Rows.Count > 0 Then
                lblNomPantalla.Text = dbset.Tables(0).Rows(0).Item("PDK_PANT_NOMBRE").ToString
                hdnNomPantalla.Value = dbset.Tables(0).Rows(0).Item("PDK_PAR_SIS_PARAMETRO").ToString
                hdnValPorcentaje.Value = dbset.Tables(0).Rows(0).Item("PDK_PAR_SIS_VALOR_NUMERO").ToString
            End If

            ' btnImprimir.Attributes.Add("OnClick", "JavaScript:CallWindow('ImprimirSolicitud.aspx?idPantalla=" & HdnPantalla.Value & "&IdFolio=" & hdnFolio.Value & "&IdUsu=" & hdnIdUsuario.Value & "&CVE=1" & "');")
            btnImprimir.Attributes.Add("OnClick", "JavaScript:CallWindow('ImprimirCreditoSolicitud.aspx?idPantalla=" & HdnPantalla.Value & "&IdFolio=" & hdnFolio.Value & "&IdUsu=" & hdnIdUsuario.Value & "&CVE=1" & "');")

        Else
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "script", "buscarEntre();", True)
        End If

    End Sub


    Private Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("IdFolio")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)
    End Sub

End Class