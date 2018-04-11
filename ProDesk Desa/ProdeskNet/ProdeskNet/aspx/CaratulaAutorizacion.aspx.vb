Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Seguridad
Imports System.Data

'YAM-P-208  egonzalez 12/08/2015 Se hizo la implementación para agregar el iframe de coacreditado en la edición de solicitud
'YAM-P-208  egonzalez 12/08/2015 Se añadió el parámetro "OcultaSoli" en la carga del iFrame para indicar que se deben eliminar los datos del solicitante en la parte del "coacreditado"
'YAM-P-208  egonzalez 12/08/2015 Se cambió la carga de la vista de la vista del coacreditado para mostrar menos información ya que la validación exige muchos campos más
'YAM-P-208  egonzalez 12/10/2015 Se agregó una validación para saber si se trata de una persona física o moral y así poder mostrar los datos adecuadamente
'BBV-P-423: RQSOL-04: AVH: 06/12/2016 Editar el Estado de la Tarea
'BBV-P-423: BUG-PD-58: ERODRIGUEZ: 17/05/2017 Caja de mensajes por numero de solicitud
'BBV-P-423: BUG-PD-60: ERODRIGUEZ: 22/05/2017 Mensaje de error al introducir caracteres no validos ó números demasiado grandes en la busqueda por solicitud
'BBV-P-423: BUG-PD-62: ERODRIGUEZ: 30/05/2017 Permite marcar mensajes como leidos y vista de mensajes sin leer

Public Class CaratulaAutorizacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim dbdata As New DataSet
            Dim Solicitud = Split(inSolicitud.Value, ".")
            Session("FolioSol") = ""
            If inSolicitud.Value <> "" Then
                Dim number As Int64
                Dim result As Boolean = Int64.TryParse(Solicitud(0), number)

                If result Then

                    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "script", "finOculta();", True)
                    dbdata = clsPantallas.ObtenerlosTabs(Solicitud(0), 6)
                    If dbdata.Tables.Count > 0 AndAlso dbdata.Tables(0).Rows.Count > 0 Then
                        lblsolicitud.Text = dbdata.Tables(0).Rows(0).Item("PDK_ID_SECCCERO").ToString
                        hdnFolio.Value = dbdata.Tables(0).Rows(0).Item("PDK_ID_SECCCERO").ToString
                        Session("FolioSol") = hdnFolio.Value
                        lblnombresol.Text = dbdata.Tables(0).Rows(0).Item("NOMBRECOM").ToString
                        lblfechasol.Text = dbdata.Tables(0).Rows(0).Item("FECHA").ToString
                    Else
                        lblsolicitud.Text = ""
                        lblnombresol.Text = ""
                        lblfechasol.Text = ""
                    End If
                End If

            End If

            If Not IsPostBack Then
                lblNomUsuario.Text = Session("cveUsuAcc")
            Else
                Master.EjecutaJS("$(""#tabs"").tabs(); $(""[id$='inSolicitud']"").autocomplete({ source: $('[id$=""hdACNombre""]').val().split(',') }); $(""#divsol"").css(""display"", ""none"");fnOcultaObjetos(document.location.href.match(/[^\/]+$/)[0], $('[id$=""hdPerfilUsuario""]').val());")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub

    Private Sub btnBuscarCliente_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscarCliente.Click
        'Dim corta


        Dim dtaset As New DataSet
        Dim tmpData As New DataSet
        Dim corta = Split(inSolicitud.Value, ".")
        Dim number As Int64
        Dim result As Boolean = Int64.TryParse(corta(0), number)

        If inSolicitud.Value <> "" And result Then



            Try
                grvCaratula.DataSource = Nothing
                grvCaratula.DataBind()
                lblNumCotiza.Text = ""
                'LimpiarSolicitud()
                grvDocumento.DataSource = Nothing
                grvDocumento.DataBind()


                '''''-------------primer tabs  -----------------
                dtaset = clsPantallas.ObtenerlosTabs(corta(0), 1)
                If dtaset.Tables.Count > 0 AndAlso dtaset.Tables(0).Rows.Count > 0 Then
                    If (Convert.ToString(dtaset.Tables(0).Rows(0).Item("COTIZACION")) <> "") Then
                        lblNumCotiza.Text = dtaset.Tables(0).Rows(0).Item("COTIZACION")
                        Dim objCotiza As New clsCotizacion
                        objCotiza.Id_Cotizacion = lblNumCotiza.Text
                        objCotiza.getCotizacion()
                        objCotiza.getProducto()
                        objCotiza.getPaquete()
                        lblUnidad.Text = objCotiza.Producto_Descripcion.ToString
                        lblPaquete.Text = objCotiza.Paquete_Nombre.ToString
                        'grvCaratula.DataSource = dtaset.Tables(1)
                        'grvCaratula.DataBind()
                    End If
                End If
                dtaset = clsPantallas.ObtenerlosTabs(corta(0), 2)
                If dtaset.Tables.Count > 0 AndAlso dtaset.Tables(0).Rows.Count > 0 Then
                    lblbc.Text = dtaset.Tables(0).Rows(0).Item("BC_SCORE").ToString
                    ''lblDecibc.Text = dtaset.Tables(0).Rows(0).Item("DECISION_BC")
                    If Not dtaset.Tables(0).Rows(0).Item("ICC") Is DBNull.Value Then
                        lblicc.Text = dtaset.Tables(0).Rows(0).Item("ICC").ToString
                    End If

                    ''lblDeciicc.Text = dtaset.Tables(0).Rows(0).Item("DECISION_ICC")


                    If Not dtaset.Tables(0).Rows(0).Item("BC_SCORE_COA") Is DBNull.Value Then
                        lblbc_coa.Text = dtaset.Tables(0).Rows(0).Item("BC_SCORE_COA")

                    End If
                    If Not dtaset.Tables(0).Rows(0).Item("ICC_COA") Is DBNull.Value Then
                        lblicc_coa.Text = dtaset.Tables(0).Rows(0).Item("ICC_COA")

                    End If
                    'If dtaset.Tables(0).Rows(0).Item("DECISION_BC_COA") <> "" Then
                    '    ''lbldecbc_coa.Text = dtaset.Tables(0).Rows(0).Item("DECISION_BC_COA")
                    'End If


                    'If dtaset.Tables(0).Rows(0).Item("DECISION_COA") <> "" Then
                    '    ''lbldeciicc_coa.Text = dtaset.Tables(0).Rows(0).Item("DECISION_COA")
                    'End If

                    If Not dtaset.Tables(0).Rows(0).Item("PDK_CAPACIDAD_PAGO") Is DBNull.Value Then
                        lblCapacidadPago.Text = dtaset.Tables(0).Rows(0).Item("PDK_CAPACIDAD_PAGO")
                        lblration.Text = dtaset.Tables(0).Rows(0).Item("RATION")
                    End If

                    'lblcappago.Text = dtaset.Tables(0).Rows(0).Item("PDK_DICTAMENCAPPAGO")
                    lblresulscore.Text = dtaset.Tables(0).Rows(0).Item("RESULTADO_SCORE").ToString
                    ''lbldeciScore.Text = dtaset.Tables(0).Rows(0).Item("DECISION_SCORE")
                    'lbldicburo.Text = dtaset.Tables(0).Rows(0).Item("DICTAME_BURO")
                    lbldicPre.Text = dtaset.Tables(0).Rows(0).Item("DICTAMENFINAL").ToString
                    lblcondi1.Text = dtaset.Tables(0).Rows(0).Item("CONDICION1").ToString
                    lblCondi2.Text = dtaset.Tables(0).Rows(0).Item("CONDICION2").ToString
                    lblCondi3.Text = dtaset.Tables(0).Rows(0).Item("CONDICION3").ToString
                    lblCondi4.Text = dtaset.Tables(0).Rows(0).Item("CONDICION4").ToString
                Else
                    limpiarBuro()
                End If
                dtaset = clsPantallas.ObtenerlosTabs(corta(0), 3)

                'JDRA Se quita la pestaña

                'If dtaset.Tables(0).Rows.Count > 0 AndAlso dtaset.Tables.Count > 0 Then
                '    lblnomCom.Text = dtaset.Tables(0).Rows(0).Item("NOMBRE1").ToString + " " + dtaset.Tables(0).Rows(0).Item("NOMBRE2").ToString + " " + dtaset.Tables(0).Rows(0).Item("APELLIDO_PATERNO").ToString + " " + dtaset.Tables(0).Rows(0).Item("APELLIDO_MATERNO").ToString
                '    lblRFC.Text = dtaset.Tables(0).Rows(0).Item("RFC").ToString
                '    lbltelpar.Text = dtaset.Tables(0).Rows(0).Item("TELEFONO_PARTI").ToString
                '    lbltelmov.Text = dtaset.Tables(0).Rows(0).Item("TELEFONO_MOVIL").ToString
                '    lblcorreoe.Text = dtaset.Tables(0).Rows(0).Item("CORREO_ELECTRONICO").ToString
                '    lbledad.Text = dtaset.Tables(0).Rows(0).Item("EDAD").ToString
                '    If dtaset.Tables(1).Rows.Count > 0 AndAlso dtaset.Tables.Count > 0 Then
                '        lblcp.Text = dtaset.Tables(1).Rows(0).Item("CP").ToString
                '        lblcolonia.Text = dtaset.Tables(1).Rows(0).Item("COLONIA").ToString
                '        lbldelega.Text = dtaset.Tables(1).Rows(0).Item("DELEGA_O_MUNI").ToString
                '        lblestado.Text = dtaset.Tables(1).Rows(0).Item("ESTADO").ToString
                '        lblciudad.Text = dtaset.Tables(1).Rows(0).Item("CIUDAD").ToString
                '        lbldomi.Text = dtaset.Tables(1).Rows(0).Item("CALLE").ToString + " " + dtaset.Tables(1).Rows(0).Item("NUM_EXT").ToString + " " + dtaset.Tables(1).Rows(0).Item("NUM_INT").ToString
                '        lblcrup.Text = dtaset.Tables(1).Rows(0).Item("CURP").ToString
                '        lblsexo.Text = dtaset.Tables(1).Rows(0).Item("SEXO").ToString
                '        lblestadocivil.Text = dtaset.Tables(1).Rows(0).Item("ESTADO_CIVIL").ToString
                '        lblpropiedadasu.Text = dtaset.Tables(1).Rows(0).Item("PROPIEDAD_NOMB").ToString
                '        lblvive.Text = dtaset.Tables(1).Rows(0).Item("VIVEN_EN").ToString
                '        lblnaciona.Text = dtaset.Tables(1).Rows(0).Item("NACIONALIDAD").ToString
                '        lblfechana.Text = Format(dtaset.Tables(1).Rows(0).Item("FECHA_NACIMIENTO"), "dd-MM-yyyy").ToString
                '    End If
                '    If dtaset.Tables(2).Rows.Count > 0 AndAlso dtaset.Tables.Count > 0 Then
                '        lblcompania.Text = dtaset.Tables(2).Rows(0).Item("COMPANIA").ToString
                '        lblpuesto.Text = dtaset.Tables(2).Rows(0).Item("PUESTO").ToString
                '        lbldepartamen.Text = dtaset.Tables(2).Rows(0).Item("DEPARTAMENTO").ToString
                '        lbltelemp.Text = dtaset.Tables(2).Rows(0).Item("TELEFONO").ToString
                '        lblextemp.Text = dtaset.Tables(2).Rows(0).Item("EXT").ToString
                '        lblanoantiguedad.Text = dtaset.Tables(2).Rows(0).Item("ANOS_ANTIGUEDAD").ToString
                '        lblsuelmensu.Text = dtaset.Tables(2).Rows(0).Item("SUELDO_MENSIAL").ToString
                '        lblDomEmp.Text = dtaset.Tables(2).Rows(0).Item("CALLE").ToString + " " + dtaset.Tables(2).Rows(0).Item("NUM_EXT").ToString + " " + dtaset.Tables(2).Rows(0).Item("NUM_INT").ToString
                '        lblcpemp.Text = dtaset.Tables(2).Rows(0).Item("CP").ToString
                '        lblcoloniemp.Text = dtaset.Tables(2).Rows(0).Item("COLONIA").ToString
                '        lbldelegaemp.Text = dtaset.Tables(2).Rows(0).Item("DELEGA_O_MUNICI").ToString
                '        lblestadoemp.Text = dtaset.Tables(2).Rows(0).Item("ESTADO").ToString
                '        lblciudademp.Text = dtaset.Tables(2).Rows(0).Item("CIUDAD").ToString
                '    End If
                '    If dtaset.Tables(5).Rows.Count > 0 AndAlso dtaset.Tables.Count > 0 Then
                '        lblnombreComcoa.Text = dtaset.Tables(5).Rows(0).Item("NOMBRE1").ToString + " " + dtaset.Tables(5).Rows(0).Item("NOMBRE2").ToString + " " + dtaset.Tables(5).Rows(0).Item("APELLIDO_PATERNO").ToString + " " + dtaset.Tables(5).Rows(0).Item("APELLIDO_MATERNO").ToString
                '        lblrfccoa.Text = dtaset.Tables(5).Rows(0).Item("RFC").ToString
                '        lbltelemovicoa.Text = dtaset.Tables(5).Rows(0).Item("TELEFONO_MOVIL").ToString
                '        lbltelparcoa.Text = dtaset.Tables(5).Rows(0).Item("TELEFONO_PARTICULAR").ToString
                '        lblcorroelecoa.Text = dtaset.Tables(5).Rows(0).Item("CORREO_ELECTRONICO").ToString
                '        lblcpcoa.Text = dtaset.Tables(5).Rows(0).Item("CP").ToString
                '        lblcoloniacoa.Text = dtaset.Tables(5).Rows(0).Item("COLONIA").ToString
                '        lbldelegacoa.Text = dtaset.Tables(5).Rows(0).Item("DELEGA_O_MUNI").ToString
                '        lblestadocoa.Text = dtaset.Tables(5).Rows(0).Item("ESTADO").ToString
                '        lblciudadcoa.Text = dtaset.Tables(5).Rows(0).Item("CIUDAD").ToString
                '        lbldomicicoa.Text = dtaset.Tables(5).Rows(0).Item("CALLE").ToString + " " + dtaset.Tables(5).Rows(0).Item("NUM_EXT").ToString + " " + dtaset.Tables(5).Rows(0).Item("NUM_INT").ToString
                '        lblcrpcoa.Text = dtaset.Tables(5).Rows(0).Item("CURP").ToString
                '        lblsexocoa.Text = dtaset.Tables(5).Rows(0).Item("SEXO").ToString

                '        lblfechanacimicoa.Text = Format(dtaset.Tables(5).Rows(0).Item("FECHA_NACIMIENTO").ToString, "dd-MM-yyyy")                
                '    lblnacionalicoa.Text = dtaset.Tables(5).Rows(0).Item("NACIONALIDAD").ToString
                '    lblestadocivicoa.Text = dtaset.Tables(5).Rows(0).Item("ESTADO_CIVIL").ToString
                '    lblvivencoa.Text = dtaset.Tables(5).Rows(0).Item("VIVEN_EN_COA").ToString
                '    lblpropiedadcoa.Text = dtaset.Tables(5).Rows(0).Item("PROPIEDAD_NOMBRE").ToString
                '    lbledadcoa.Text = dtaset.Tables(5).Rows(0).Item("EDAD_COAC").ToString
                'End If
                'End If
                dtaset = clsPantallas.ObtenerlosTabs(corta(0), 4)
                tmpData = clsPantallas.ObtenerlosTabs(corta(0), 7)
                If dtaset.Tables(0).Rows(0).Item("Persona") = "Cliente" And dtaset.Tables(0).Rows(0).Item("idBuro") <> 0 Then
                    imagSolicita.Attributes.Add("onClick", "JavaScript:CallWindow('consultaBuroCreditoReporte.aspx?IdSolicitud=" & corta(0) & "&IdBuro=" & dtaset.Tables(0).Rows(0).Item("idBuro").ToString & "');")
                End If

                ifsol2.Attributes("src") = ""

                If dtaset.Tables(0).Rows.Count > 1 Then
                    If dtaset.Tables(0).Rows(1).Item("Persona") = "Coacreditado" And dtaset.Tables(0).Rows(1).Item("idBuro") <> 0 Then
                        imgCoacre.Attributes.Add("onClick", "JavaScript:CallWindow('consultaBuroCreditoReporte.aspx?IdSolicitud=" & corta(0) & "&IdBuro=" & dtaset.Tables(0).Rows(1).Item("idBuro").ToString & "');")
                        ifsol2.Attributes("src") = "VerificaDatSol.aspx?idFolio=" & corta(0) & "&idPantalla=5&Enable=0&OcultaSoli=1"
                    End If
                End If

                If dtaset.Tables(0).Rows(0).Item("idBuro") > 0 Then
                    ifSol.Attributes("src") = "VerificaDatSol.aspx?idFolio=" & corta(0) & "&idPantalla=7&Enable=0"
                    ' ifsol2.Attributes("src") = "VerificaDatSol.aspx?idFolio=" & corta(0) & "&idPantalla=6&Enable=0"
                Else
                    If tmpData.Tables(0).Rows(0).Item("PDK_ID_PER_JURIDICA") = 3 Then
                        ifSol.Attributes("src") = "VerificaDatSol.aspx?idFolio=" & corta(0) & "&idPantalla=15&Enable=0"
                        ifsol2.Attributes("src") = ""
                    Else
                        ifSol.Attributes("src") = "VerificaDatSol.aspx?idFolio=" & corta(0) & "&idPantalla=6&Enable=0"
                    End If
                End If

                ifFact.Attributes("src") = "caratulaFacturacion.aspx?idPantalla=13&sol=" & corta(0)

                'If dtaset.Tables(0).Rows.Count > 1 AndAlso dtaset.Tables.Count > 1 Then
                '    imagSolicita.Attributes.Add("onClick", "JavaScript:CallWindow('consultaBuroCreditoReporte.aspx?IdSolicitud=" & corta(0) & "&IdBuro=" & dtaset.Tables(0).Rows(0).Item("PDK_ID_BURO").ToString & "');")
                '    imgCoacre.Attributes.Add("onClick", "JavaScript:CallWindow('consultaBuroCreditoReporte.aspx?IdSolicitud=" & corta(0) & "&IdBuro=" & dtaset.Tables(dtaset.Tables.Count - 1).Rows(0).Item("PDK_ID_BURO").ToString & "');")
                'ElseIf dtaset.Tables(0).Rows.Count > 0 AndAlso dtaset.Tables.Count > 0 Then
                '    imagSolicita.Attributes.Add("onClick", "JavaScript:CallWindow('consultaBuroCreditoReporte.aspx?IdSolicitud=" & corta(0) & "&IdBuro=" & dtaset.Tables(0).Rows(0).Item("PDK_ID_BURO").ToString & "');")
                'End If
                dtaset = clsPantallas.ObtenerlosTabs(corta(0), 5)
                If dtaset.Tables(0).Rows.Count > 0 AndAlso dtaset.Tables.Count > 0 Then
                    grvDocumento.DataSource = dtaset.Tables(0)
                    grvDocumento.DataBind()
                End If

                MTareas.Attributes("src") = "manejaTareas.aspx?idFolio=" & corta(0)

                Dim stringurl As String = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath

                'ifFact.Attributes("src") = "altaFacturacion.aspx?idPantalla=13&sol=" & corta(0) & "&Enable=1&MP=1"

            Catch ex As Exception
                Master.MensajeError("Debe introducir un número de solicitud válido")
                'Master.MensajeError(ex.Message)
            End Try
        Else
            grvDocumento.DataSource = Nothing
            Master.MensajeError("Debe introducir un número de solicitud válido")
        End If
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

End Class