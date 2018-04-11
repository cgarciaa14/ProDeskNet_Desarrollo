Imports ProdeskNet.BD
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Seguridad
Imports System.Data

#Region "Tracker"
'Tracker:INC-B-2019:JDRA:Regresar.
#End Region

Public Class altaNotificacion
    Inherits System.Web.UI.Page
    Dim BD As New clsManejaBD
    'BD.clsManejaBD
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim intEnable As Integer
                Dim dsresult As New DataSet

                hdnIdPantalla.Value = Request("idPantalla")
                hdnIdFolio.Value = Request("IdFolio")
                hdnUsua.Value = Session("IdUsua")
                hdnEnable.Value = Request("Enable")
                If Session("Regresar") Is Nothing Then
                    Session("Regresar") = Request.UrlReferrer.LocalPath
                    hdRutaEntrada.Value = Request.UrlReferrer.LocalPath
                End If

                hdRutaEntrada.Value = Session("Regresar")

                Try
                    dsresult = BD.EjecutarQuery("EXEC sp_GenerarRura " & hdnIdFolio.Value & "," & hdnIdPantalla.Value & "," & hdnUsua.Value & "")
                    If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
                        hdnResultado.Value = dsresult.Tables(0).Rows(0).Item("RUTA")
                        hdnResultado1.Value = dsresult.Tables(1).Rows(0).Item("RUTA2")
                        hdnResultado2.Value = dsresult.Tables(2).Rows(0).Item("RUTA3")
                    End If
                Catch ex As Exception
                    hdnResultado.Value = Session("Regresar")
                End Try

                Try
                    intEnable = CInt(Request.QueryString("Enable"))

                Catch ex As Exception
                    intEnable = 0
                End Try
                btnBuscar(hdnIdFolio.Value)

                If intEnable = 1 Then
                    btnGuardarCom.Attributes.Add("style", "display:none;")
                    btnAutorizar.Attributes.Add("style", "display:none;")
                    btnCancelar.Attributes.Add("style", "display:none;")

                End If
                dsresult = BD.EjecutarQuery("SELECT PDK_ID_PER_JURIDICA FROM PDK_TAB_SECCION_CERO WHERE PDK_ID_SECCCERO=" & hdnIdFolio.Value & "")
                If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
                    hdnIdTipoJuridica.Value = dsresult.Tables(0).Rows(0).Item("PDK_ID_PER_JURIDICA")
                End If

                dsresult = BD.EjecutarQuery("SELECT A.PDK_PANT_NOMBRE,B.PDK_PAR_SIS_PARAMETRO   FROM PDK_PANTALLAS A INNER JOIN PDK_PARAMETROS_SISTEMA B ON A.PDK_PANT_DOCUMENTOS=B.PDK_ID_PARAMETROS_SISTEMA AND B.PDK_PAR_SIS_ID_PADRE=24   WHERE PDK_ID_PANTALLAS = " & hdnIdPantalla.Value & "")
                If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
                    hdNomPantalla.Value = dsresult.Tables(0).Rows(0).Item("PDK_PAR_SIS_PARAMETRO").ToString
                    lblNomNotificacion.Text = dsresult.Tables(0).Rows(0).Item("PDK_PANT_NOMBRE").ToString
                End If

            Catch ex As Exception
                Master.MensajeError(ex.Message)
            End Try

        Else
            Dim dsresult As New DataSet
            Try
                dsresult = BD.EjecutarQuery("EXEC sp_GenerarRura " & hdnIdFolio.Value & "," & hdnIdPantalla.Value & "," & hdnUsua.Value & "")
                If dsresult.Tables(0).Rows.Count > 0 AndAlso dsresult.Tables.Count > 0 Then
                    hdnResultado.Value = dsresult.Tables(0).Rows(0).Item("RUTA")
                    hdnResultado1.Value = dsresult.Tables(1).Rows(0).Item("RUTA2")
                    hdnResultado2.Value = dsresult.Tables(2).Rows(0).Item("RUTA3")
                End If
            Catch ex As Exception
                hdnResultado.Value = Session("Regresar")
            End Try
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "script", " $(document).ready(function () {$(""#tabs"").tabs();  fillgv('tabNotificacion', 'hdnIdFolio,hdnEnable'); }); ", True)

        End If
    End Sub
    Public Sub LimpiarSolicitud()
        lblnomCom.Text = ""
        lblRFC.Text = ""
        lbltelpar.Text = ""
        lbltelmov.Text = ""
        lblcorreoe.Text = ""
        lbledad.Text = ""
        lblcp.Text = ""
        lblcolonia.Text = ""
        lbldelega.Text = ""
        lblestado.Text = ""
        lblciudad.Text = ""
        lbldomi.Text = ""
        lblcrup.Text = ""
        lblsexo.Text = ""
        lblestadocivil.Text = ""
        lblpropiedadasu.Text = ""
        lblvive.Text = ""
        lblnaciona.Text = ""
        lblfechana.Text = ""
        lblpuesto.Text = ""
        lbldepartamen.Text = ""
        lbltelemp.Text = ""
        lblextemp.Text = ""
        lblanoantiguedad.Text = ""
        lblsuelmensu.Text = ""
        lblDomEmp.Text = ""
        lblcpemp.Text = ""
        lblcoloniemp.Text = ""
        lbldelegaemp.Text = ""
        lblestadoemp.Text = ""
        lblciudademp.Text = ""
        lblnombreComcoa.Text = ""
        lblrfccoa.Text = ""
        lbltelemovicoa.Text = ""
        lbltelparcoa.Text = ""
        lblcorroelecoa.Text = ""
        lblcpcoa.Text = ""
        lblcoloniacoa.Text = ""
        lbldelegacoa.Text = ""
        lblestadocoa.Text = ""
        lblciudadcoa.Text = ""
        lblcrpcoa.Text = ""
        lblsexocoa.Text = ""
        lblfechanacimicoa.Text = ""
        lblnacionalicoa.Text = ""
        lblestadocivicoa.Text = ""
        lblvivencoa.Text = ""
        lblpropiedadcoa.Text = ""
        lbledadcoa.Text = ""
        lbldomicicoa.Text = ""
        lblRFC.Text = ""
        lblcompania.Text = ""
    End Sub
    Private Sub btnBuscar(ByVal intsol As Integer)
        Dim dtaset As New DataSet
        Try
            grvCaratula.DataSource = Nothing
            grvCaratula.DataBind()
            lblNumCotiza.Text = ""
            LimpiarSolicitud()
            'grvDocumento.DataSource = Nothing
            'grvDocumento.DataBind()



            '''''-------------primer tabs  -----------------
            dtaset = clsPantallas.ObtenerlosTabs(intsol, 1)
            If dtaset.Tables.Count > 0 AndAlso dtaset.Tables(0).Rows.Count > 0 Then
                lblNumCotiza.Text = dtaset.Tables(0).Rows(0).Item("COTIZACION")
                grvCaratula.DataSource = dtaset.Tables(1)
                grvCaratula.DataBind()
            End If
            dtaset = clsPantallas.ObtenerlosTabs(intsol, 2)
            If dtaset.Tables.Count > 0 AndAlso dtaset.Tables(0).Rows.Count > 0 Then
                lblbc.Text = dtaset.Tables(0).Rows(0).Item("BC_SCORE")
                ''lblDecibc.Text = dtaset.Tables(0).Rows(0).Item("DECISION_BC")
                lblicc.Text = dtaset.Tables(0).Rows(0).Item("ICC")
                ''lblDeciicc.Text = dtaset.Tables(0).Rows(0).Item("DECISION_ICC")

                If Not dtaset.Tables(0).Rows(0).Item("ICC") Is DBNull.Value Then
                    lblicc.Text = dtaset.Tables(0).Rows(0).Item("ICC")
                End If

                ''lblDeciicc.Text = dtaset.Tables(0).Rows(0).Item("DECISION_ICC")

                If Not dtaset.Tables(0).Rows(0).Item("BC_SCORE_COA") Is DBNull.Value Then
                    lblbc_coa.Text = dtaset.Tables(0).Rows(0).Item("BC_SCORE_COA")

                End If
                If Not dtaset.Tables(0).Rows(0).Item("ICC_COA") Is DBNull.Value Then
                    lblicc_coa.Text = dtaset.Tables(0).Rows(0).Item("ICC_COA")

                End If

                If Not dtaset.Tables(0).Rows(0).Item("PDK_CAPACIDAD_PAGO") Is DBNull.Value Then
                    lblration.Text = dtaset.Tables(0).Rows(0).Item("PDK_CAPACIDAD_PAGO")
                End If

                'lblcappago.Text = dtaset.Tables(0).Rows(0).Item("PDK_DICTAMENCAPPAGO")
                lblresulscore.Text = dtaset.Tables(0).Rows(0).Item("RESULTADO_SCORE")
                ''lbldeciScore.Text = dtaset.Tables(0).Rows(0).Item("DECISION_SCORE")
                'lbldicburo.Text = dtaset.Tables(0).Rows(0).Item("DICTAME_BURO")
                lbldicPre.Text = dtaset.Tables(0).Rows(0).Item("DICTAMENFINAL")
                lblcondi1.Text = dtaset.Tables(0).Rows(0).Item("CONDICION1")
                lblCondi2.Text = dtaset.Tables(0).Rows(0).Item("CONDICION2")
                lblCondi3.Text = dtaset.Tables(0).Rows(0).Item("CONDICION3")
                lblCondi4.Text = dtaset.Tables(0).Rows(0).Item("CONDICION4")
            Else
                limpiarBuro()
            End If
            dtaset = clsPantallas.ObtenerlosTabs(intsol, 3)
            If dtaset.Tables(0).Rows.Count > 0 AndAlso dtaset.Tables.Count > 0 Then
                lblnomCom.Text = dtaset.Tables(0).Rows(0).Item("NOMBRE1").ToString + " " + dtaset.Tables(0).Rows(0).Item("NOMBRE2").ToString + " " + dtaset.Tables(0).Rows(0).Item("APELLIDO_PATERNO").ToString + " " + dtaset.Tables(0).Rows(0).Item("APELLIDO_MATERNO").ToString
                lblRFC.Text = dtaset.Tables(0).Rows(0).Item("RFC").ToString
                lbltelpar.Text = dtaset.Tables(0).Rows(0).Item("TELEFONO_PARTI").ToString
                lbltelmov.Text = dtaset.Tables(0).Rows(0).Item("TELEFONO_MOVIL").ToString
                lblcorreoe.Text = dtaset.Tables(0).Rows(0).Item("CORREO_ELECTRONICO").ToString
                lbledad.Text = dtaset.Tables(0).Rows(0).Item("EDAD").ToString
                If dtaset.Tables(1).Rows.Count > 0 AndAlso dtaset.Tables.Count > 0 Then
                    lblcp.Text = dtaset.Tables(1).Rows(0).Item("CP").ToString
                    lblcolonia.Text = dtaset.Tables(1).Rows(0).Item("COLONIA").ToString
                    lbldelega.Text = dtaset.Tables(1).Rows(0).Item("DELEGA_O_MUNI").ToString
                    lblestado.Text = dtaset.Tables(1).Rows(0).Item("ESTADO").ToString
                    lblciudad.Text = dtaset.Tables(1).Rows(0).Item("CIUDAD").ToString
                    lbldomi.Text = dtaset.Tables(1).Rows(0).Item("CALLE").ToString + " " + dtaset.Tables(1).Rows(0).Item("NUM_EXT").ToString + " " + dtaset.Tables(1).Rows(0).Item("NUM_INT").ToString
                    lblcrup.Text = dtaset.Tables(1).Rows(0).Item("CURP").ToString
                    lblsexo.Text = dtaset.Tables(1).Rows(0).Item("SEXO").ToString
                    lblestadocivil.Text = dtaset.Tables(1).Rows(0).Item("ESTADO_CIVIL").ToString
                    lblpropiedadasu.Text = dtaset.Tables(1).Rows(0).Item("PROPIEDAD_NOMB").ToString
                    lblvive.Text = dtaset.Tables(1).Rows(0).Item("VIVEN_EN").ToString
                    lblnaciona.Text = dtaset.Tables(1).Rows(0).Item("NACIONALIDAD").ToString
                    lblfechana.Text = Format(dtaset.Tables(1).Rows(0).Item("FECHA_NACIMIENTO"), "dd-MM-yyyy").ToString
                End If
                If dtaset.Tables(2).Rows.Count > 0 AndAlso dtaset.Tables.Count > 0 Then
                    lblcompania.Text = dtaset.Tables(2).Rows(0).Item("COMPANIA").ToString
                    lblpuesto.Text = dtaset.Tables(2).Rows(0).Item("PUESTO").ToString
                    lbldepartamen.Text = dtaset.Tables(2).Rows(0).Item("DEPARTAMENTO").ToString
                    lbltelemp.Text = dtaset.Tables(2).Rows(0).Item("TELEFONO").ToString
                    lblextemp.Text = dtaset.Tables(2).Rows(0).Item("EXT").ToString
                    lblanoantiguedad.Text = dtaset.Tables(2).Rows(0).Item("ANOS_ANTIGUEDAD").ToString
                    lblsuelmensu.Text = dtaset.Tables(2).Rows(0).Item("SUELDO_MENSIAL").ToString
                    lblDomEmp.Text = dtaset.Tables(2).Rows(0).Item("CALLE").ToString + " " + dtaset.Tables(2).Rows(0).Item("NUM_EXT").ToString + " " + dtaset.Tables(2).Rows(0).Item("NUM_INT").ToString
                    lblcpemp.Text = dtaset.Tables(2).Rows(0).Item("CP").ToString
                    lblcoloniemp.Text = dtaset.Tables(2).Rows(0).Item("COLONIA").ToString
                    lbldelegaemp.Text = dtaset.Tables(2).Rows(0).Item("DELEGA_O_MUNICI").ToString
                    lblestadoemp.Text = dtaset.Tables(2).Rows(0).Item("ESTADO").ToString
                    lblciudademp.Text = dtaset.Tables(2).Rows(0).Item("CIUDAD").ToString
                End If
                If dtaset.Tables(5).Rows.Count > 0 AndAlso dtaset.Tables.Count > 0 Then
                    lblnombreComcoa.Text = dtaset.Tables(5).Rows(0).Item("NOMBRE1").ToString + " " + dtaset.Tables(5).Rows(0).Item("NOMBRE2").ToString + " " + dtaset.Tables(5).Rows(0).Item("APELLIDO_PATERNO").ToString + " " + dtaset.Tables(5).Rows(0).Item("APELLIDO_MATERNO").ToString
                    lblrfccoa.Text = dtaset.Tables(5).Rows(0).Item("RFC").ToString
                    lbltelemovicoa.Text = dtaset.Tables(5).Rows(0).Item("TELEFONO_MOVIL").ToString
                    lbltelparcoa.Text = dtaset.Tables(5).Rows(0).Item("TELEFONO_PARTICULAR").ToString
                    lblcorroelecoa.Text = dtaset.Tables(5).Rows(0).Item("CORREO_ELECTRONICO").ToString
                    lblcpcoa.Text = dtaset.Tables(5).Rows(0).Item("CP").ToString
                    lblcoloniacoa.Text = dtaset.Tables(5).Rows(0).Item("COLONIA").ToString
                    lbldelegacoa.Text = dtaset.Tables(5).Rows(0).Item("DELEGA_O_MUNI").ToString
                    lblestadocoa.Text = dtaset.Tables(5).Rows(0).Item("ESTADO").ToString
                    lblciudadcoa.Text = dtaset.Tables(5).Rows(0).Item("CIUDAD").ToString
                    lbldomicicoa.Text = dtaset.Tables(5).Rows(0).Item("CALLE").ToString + " " + dtaset.Tables(5).Rows(0).Item("NUM_EXT").ToString + " " + dtaset.Tables(5).Rows(0).Item("NUM_INT").ToString
                    lblcrpcoa.Text = dtaset.Tables(5).Rows(0).Item("CURP").ToString
                    lblsexocoa.Text = dtaset.Tables(5).Rows(0).Item("SEXO").ToString
                    lblfechanacimicoa.Text = Format(dtaset.Tables(5).Rows(0).Item("FECHA_NACIMIENTO"), "dd-MM-yyyy")
                    lblnacionalicoa.Text = dtaset.Tables(5).Rows(0).Item("NACIONALIDAD").ToString
                    lblestadocivicoa.Text = dtaset.Tables(5).Rows(0).Item("ESTADO_CIVIL").ToString
                    lblvivencoa.Text = dtaset.Tables(5).Rows(0).Item("VIVEN_EN_COA").ToString
                    lblpropiedadcoa.Text = dtaset.Tables(5).Rows(0).Item("PROPIEDAD_NOMBRE").ToString
                    lbledadcoa.Text = dtaset.Tables(5).Rows(0).Item("EDAD_COAC").ToString
                End If
            End If
            dtaset = clsPantallas.ObtenerlosTabs(intsol, 4)
            If dtaset.Tables(0).Rows.Count > 0 AndAlso dtaset.Tables.Count > 0 Then
                imagSolicita.Attributes.Add("onClick", "JavaScript:CallWindow('consultaBuroCreditoReporte.aspx?IdSolicitud=" & intsol & "&IdBuro=" & dtaset.Tables(0).Rows(0).Item("PDK_ID_BURO").ToString & "');  window.location.replace('altaNotificacion.aspx?idPantalla=" & hdnIdPantalla.Value & "&IdFolio=" & hdnIdFolio.Value & "&CVE=1&Enable=" & hdnEnable.Value & "')")
                imgCoacre.Attributes.Add("onClick", "JavaScript:CallWindow('consultaBuroCreditoReporte.aspx?IdSolicitud=" & intsol & "&IdBuro=" & dtaset.Tables(0).Rows(0).Item("PDK_ID_BURO").ToString & "');  window.location.replace('altaNotificacion.aspx?idPantalla=" & hdnIdPantalla.Value & "&IdFolio=" & hdnIdFolio.Value & "&CVE=1&Enable=" & hdnEnable.Value & "')")

            End If
            dtaset = clsPantallas.ObtenerlosTabs(intsol, 5)
            If dtaset.Tables(0).Rows.Count > 0 AndAlso dtaset.Tables.Count > 0 Then
                grvDocumento.DataSource = dtaset.Tables(0)
                grvDocumento.DataBind()
            End If

            Master.EjecutaJS("$('[id$=tabs]').tabs();")


        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub
    Public Sub limpiarBuro()
        lblbc.Text = ""
        'lblDecibc.Text = ""
        lblicc.Text = ""
        'lblDeciicc.Text = ""
        lblbc_coa.Text = ""
        'lbldecbc_coa.Text = ""
        lblicc_coa.Text = ""
        'lbldeciicc_coa.Text = ""
        lblration.Text = ""
        'lblcappago.Text = ""
        lblresulscore.Text = ""
        'lbldeciScore.Text = ""
        'lbldicburo.Text = ""
        lbldicPre.Text = ""
        lblcondi1.Text = ""
        lblCondi2.Text = ""
        lblCondi3.Text = ""
        lblCondi4.Text = ""
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click

        'Response.Redirect("./consultaPantalla.aspx")
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("IdFolio")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & dc.idSolicitud & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)

    End Sub
  
End Class