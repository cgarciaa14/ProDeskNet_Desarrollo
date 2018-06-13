Imports ProdeskNet.Seguridad
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Web.UI.WebControls
Imports ProdeskNet.Buro
Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.WCF
Imports System.IO

#Region "trackers"
'INC-B-2019:JDRA:Regresar
' YAM-P-208  egonzalez 11/08/2015 Se agregó implementó un tipo de dato existente 'txtCustMayGde' para su validación en los formularios
' YAM-P-208  egonzalez 12/08/2015 Se implementó una división en la sección "Referencias personales" para distinguir los campos entre los 2 posibles referidos.
' YAM-P-208  egonzalez 01/09/2015 Se implemento la llamada a la función "tareaActual" y se guarda el valor "PDK_ID_TAREAS" en el campo oculto "hdnTareaActual"
' YAM-P-208  egonzalez 08/10/2015 Se evitó el envío del formulrio para así prevenir la recarga de la página derivando en más errores al imprimir.
' BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
' BBV-P-423  RQSOL-02  gvargas  15/12/2016 se agregaron web methods para consulta de informacion mediante callbacks
'                                          se agregaron clases para el manejo de la informacion en pantalla, metodos de negocio
' BBV-P-423 JRHM:  RQ J-REPORTE SOLICITUD DE CREDITO 30/12/16.- Se agrego opcion para la impresion de el reporte de la tarea solicitud de credito.
' BUG-PD-03: GVARGAS: 05/01/2017: Correcciones pantalla Precalificacion.
' BUG-PD-04: GVARGAS: 11/01/2017: Consulta a BDD Procotiza si existe num_cotizacion.
' BUG-PD-05: GVARGAS: 24/01/2017: Correcciones Solititud de Credito.
' BUG-PD-06: GVARGAS: 26/01/2017: Correcciones pantalla Solicitud de Credito & Precalificacion.
' BUG-PD-08: GVARGAS: 09/02/2017: Correcciones pantalla Precalificacion Homoclave no Obligatoria.
' BUG-PD-09: GVARGAS: 15/02/2017: Revision general Bugs.
' BUG-PD-10: GVARGAS: 20/02/2017: Revision general Bugs 10.
' BUG-PD-13  GVARGAS  27/02/2017  focus doble val.
' BUG-PD-18  GVARGAS  07/03/2017  Table Pep skip row.
' BUG-PD-21  GVARGAS  27/03/2017 Bugs Campos Obligatorios
' BUG-PD-28  GVARGAS  10/04/2017 Cambio Label PEP
' BBV-P-423  RQADM-02 GVARGAS  12/04/2017 Actualización de Datos en Solicitud 38,39
' BUG-PD-39  erodriguez 26/04/2017 Cambios de usabilidad y estilos
' BUG-PD-67  GVARGAS 01/06/2017 Cambios ciudades-->
' BUG-PD-115 GVARGAS 23/06/2017 Cambios default 0-->
' BBV-P-423 RQACTPRE-01 GVARGAS 03/07/2017 Validar años residencia-->
' BBVA-P-423 RQ-MN2-1 GVARGAS 07/09/2017 CI Precalificaciòn
' BBVA-P-423 RQ-PI7-PD6 Quitar Obligatoriedad telefono fijo
' BUG-PD-234 12/10/2017 GVARGAS Cambio urgente Blanco
' BBVA-P-423 RQ-PI7-PD1 GVARGAS 23/10/2017 Mejoras CI Precalificaciòn & Preforma
' BUG-PD-393 GVARGAS 12/03/2018 Validar campos obligatorios sin ERROR
' BUG-PD-412 DJUAREZ 05/04/2018 Validar que la antiguedad de trabajo se mayor o igual a 14 años
' BUG-PD-449 GVARGAS 23/05/2018 New save method

#End Region

Partial Class aspx_Blanco
    Inherits System.Web.UI.Page
    Public arregloTextBoxs(50) As TextBox
    Public arregloCombos(50) As DropDownList
    Public arregloLabel(50) As Label
    Public arregloTable(50) As Table
    Public arregloUpdate(50) As HiddenField
    Public arregloTextTarea(50) As HtmlTextArea
    Public arregloCheck(50) As CheckBox
    Public arregloRadio(50) As RadioButtonList
    Public strPantalla As String = String.Empty
    Public intEnable As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"
        Try

            Session("entra") = Nothing

            If Not IsPostBack Then
                arregloTextBoxs(50) = New TextBox
                arregloCombos(50) = New DropDownList
                arregloLabel(50) = New Label
                arregloTable(50) = New Table
                arregloUpdate(50) = New HiddenField
                arregloTextTarea(50) = New HtmlTextArea
                arregloCheck(50) = New CheckBox
                arregloRadio(50) = New RadioButtonList
                Dim intConta As Integer = 0
                Dim dbData As New DataSet
                Dim dbDabase As New DataSet
                Dim dbTareaActual As New DataSet
                Dim miTabla As New Table
                Dim rowCnt As Integer
                Dim rowCtr As Integer
                Dim cellCtr As Integer
                Dim cellCnt As Integer
                Dim strNombre As String = ""
                Dim intContador As Integer = 0
                Dim Counternx As Integer = 0
                Dim Counterfx As Integer = 0



                If Session("Regresar") Is Nothing Then
                    Session("Regresar") = Request.UrlReferrer.LocalPath
                    hdRutaEntrada.Value = Request.UrlReferrer.LocalPath
                End If
                hdRutaEntrada.Value = Session("Regresar")

                If Val(Request("idPantalla")) > 0 Then


                    hdnIdRegistro.Value = Request("idPantalla")
                    hdnUsuario.Value = Session("IdUsua")
                    hdnFolio.Value = Request("IdFolio")

                    Try
                        dbTareaActual = clsPantallaObjeto.tareaActual(hdnFolio.Value)
                        hdnTareaActual.Value = dbTareaActual.Tables(0).Rows(0).Item("PDK_ID_TAREAS")
                        dbData = clsPantallaObjeto.OBTENERRUTA(hdnFolio.Value, hdnIdRegistro.Value, hdnUsuario.Value)
                        If dbData.Tables(0).Rows.Count > 0 AndAlso dbData.Tables.Count > 0 Then
                            If Request("idPantalla") = 1 Or Request("idPantalla") = 5 Then
                                hdnResultado.Value = dbData.Tables(0).Rows(0).Item("RUTA")
                                hdnResultado1.Value = dbData.Tables(1).Rows(0).Item("RUTA2")
                                hdnResultado2.Value = dbData.Tables(2).Rows(0).Item("RUTA3")

                            Else
                                hdnResultado.Value = dbData.Tables(0).Rows(0).Item("RUTA")
                            End If

                        End If
                    Catch ex As Exception
                        hdnResultado.Value = Session("Regresar")
                    End Try



                    dbData = clsPantallaObjeto.ObtenerControles(hdnIdRegistro.Value, 1)
                    If dbData.Tables(0).Rows.Count > 0 AndAlso dbData.Tables.Count > 0 Then
                        lbltitulo.Text = dbData.Tables(0).Rows(0).Item("PDK_PANT_NOMBRE")
                        strPantalla = dbData.Tables(0).Rows(0).Item("PDK_PANT_NOMBRE")
                        hdnPantalla.Value = strPantalla
                    End If

                    dbDabase = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 4, 0, hdnFolio.Value)
                    If (Request("idPantalla") = 7) Then
                        Dim dt As DataTable = dbDabase.Tables(0)
                        Dim loopRows As Integer() = New Integer() {1, 12, 13, 8, 2, 3, 4, 14, 5, 9, 6, 7, 15, 16}
                        '{1, 11, 12, 2, 3, 4, 13, 5, 8, 6, 7, 14, 15}
                        Dim dtSorted As DataTable = New DataTable()
                        dtSorted.TableName = "REGISTRO1"
                        dtSorted.Columns.Add("ID")
                        dtSorted.Columns.Add("PDK_SEC_NOMBRE_TABLA")
                        dtSorted.Columns.Add("PDK_SEC_NOMBRE")
                        dtSorted.Columns.Add("ORDEN")
                        dtSorted.Columns.Add("PDK_ID_SECCION")
                        dtSorted.Columns.Add("VALIDA")
                        Dim indiceI As Integer = -1
                        For indice As Integer = 0 To loopRows.Length - 1
                            indiceI = loopRows(indice) - 1
                            Dim dr As DataRow = dtSorted.NewRow
                            dr("ID") = indice + 1
                            dr("PDK_SEC_NOMBRE_TABLA") = dt.Rows(indiceI).Item("PDK_SEC_NOMBRE_TABLA")
                            dr("PDK_SEC_NOMBRE") = dt.Rows(indiceI).Item("PDK_SEC_NOMBRE")
                            dr("ORDEN") = dt.Rows(indiceI).Item("ORDEN")
                            dr("PDK_ID_SECCION") = dt.Rows(indiceI).Item("PDK_ID_SECCION")
                            dr("VALIDA") = dt.Rows(indiceI).Item("VALIDA")
                            dtSorted.Rows.Add(dr)
                        Next
                        dbDabase.Tables.Clear()
                        dbDabase.Tables.Add(dtSorted)
                    End If



                    If dbDabase.Tables.Count > 0 AndAlso dbDabase.Tables(0).Rows.Count > 0 Then
                        'Dim mitable As New Table
                        For i = 0 To dbDabase.Tables(0).Rows.Count - 1
                            Dim mitable As New Table
                            mitable.ID = dbDabase.Tables(0).Rows(i).Item("VALIDA") + "_" + dbDabase.Tables(0).Rows(i).Item("PDK_SEC_NOMBRE_TABLA")
                            'title of section
                            If (Request("idPantalla") = 7) Then
                                If ((dbDabase.Tables(0).Rows(i).Item("PDK_SEC_NOMBRE") <> "DATOS PERSONALES NO MOSTRAR") And (dbDabase.Tables(0).Rows(i).Item("PDK_SEC_NOMBRE") <> "Capacidad de Pago")) Then
                                    'mitable.Caption = "<font size=2>" + dbDabase.Tables(0).Rows(i).Item("PDK_SEC_NOMBRE") + "</font>"
                                    mitable.Caption = "<div class=bold>" + "<div class=inner>" + dbDabase.Tables(0).Rows(i).Item("PDK_SEC_NOMBRE") + "</div>"
                                    'mitable.CaptionAlign = TableCaptionAlign.Right


                                    'mitable.Font.Bold = True
                                End If
                            Else
                                Dim strNameSection As String = dbDabase.Tables(0).Rows(i).Item("PDK_SEC_NOMBRE")
                                If dbDabase.Tables(0).Rows(i).Item("PDK_SEC_NOMBRE") = "DATOS PERSONALES NO MOSTRAR" Then
                                    strNameSection = "Buro de Crédito"
                                End If
                                'mitable.Caption = "<font size=2>" + strNameSection + "</font>"
                                mitable.Caption = "<div class=bold>" + "<div class=inner>" + strNameSection + "</div>" + "</div>"
                                'mitable.CaptionAlign = TableCaptionAlign.Right


                                'mitable.Font.Bold = True
                            End If

                            'mitable.BorderStyle = BorderStyle.Ridge
                            'mitable.BorderColor = Drawing.Color.DarkGray
                            mitable.CssClass = "boldtable"
                            mitable.HorizontalAlign = HorizontalAlign.Center
                            If intConta = 0 Then
                                If dbDabase.Tables(0).Rows(i).Item("VALIDA") = "U" Then
                                    ''Button1.Value = "Actualizar"
                                    intConta = 1
                                End If
                            End If
                            dbData = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 2, dbDabase.Tables(0).Rows(i).Item("PDK_ID_SECCION"))
                            If dbData.Tables.Count > 0 AndAlso dbData.Tables(0).Rows.Count > 0 Then
                                strNombre = dbData.Tables(0).Rows(0).Item("PDK_SEC_NOMBRE_TABLA").ToString.Trim
                            End If
                            dbData = Nothing
                            dbData = clsPantallaObjeto.ObtenerControles(hdnIdRegistro.Value, 0, dbDabase.Tables(0).Rows(i).Item("PDK_ID_SECCION"), hdnFolio.Value)
                            rowCnt = CInt(dbData.Tables(0).Rows.Count)
                            cellCnt = CInt(4)
                            For rowCtr = 0 To rowCnt - 1
                                Dim tRow As New TableRow()
                                Dim tRowTmp As New TableRow()
                                Dim tRowTmp2 As New TableRow()
                                For cellCtr = 1 To cellCnt
                                    Dim tCell As New TableCell()
                                    Dim asterisk As String = ""
                                    'creamos(Label)
                                    If cellCtr = 1 Or cellCtr = 3 Then
                                        Dim lbl As New Label
                                        lbl.ID = "lbl" + dbData.Tables(0).Rows(rowCtr).Item("PDK_ID_SECCION_DATO").ToString + "|" + dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE")

                                        Dim strLabel As String = dbData.Tables(0).Rows(rowCtr).Item("MOSTRAR").ToString().ToLower()
                                        Dim words As String() = strLabel.Split(New Char() {" "c})
                                        Dim word As String
                                        strLabel = String.Empty
                                        For Each word In words
                                            If ((Request("idPantalla") = 7)) Then
                                                If (word = "*") Then
                                                    word = " "
                                                End If
                                            End If
                                            If (word <> "la") And (word <> "de") And (word <> "del") And (word <> "o") And (word <> "para") And (word <> "con") And (word <> "que") And (word <> "es") And (word <> "ha") And (word <> "sido") And (word <> "a") And (word <> "un") And (word <> "el") And (word <> "") Then
                                                word = word.Substring(0, 1).ToUpper() + word.Substring(1)
                                            End If
                                            If word = "Pep?" Then
                                                word = word.ToUpper()
                                            End If
                                            If word = "Pep" Then
                                                word = word.ToUpper()
                                            End If
                                            If word = "Rfc" Then
                                                word = word.ToUpper()
                                            End If
                                            If word = "Cp" Then
                                                word = word.ToUpper()
                                            End If
                                            If word = "Fiel" Then
                                                word = word.ToUpper()
                                            End If
                                            If word = "Curp" Then
                                                word = word.ToUpper()
                                            End If
                                            If word = "(meses)" Then
                                                word = "(Meses)"
                                            End If
                                            If word = "(años)" Then
                                                word = "(Años)"
                                            End If
                                            If word = "(arrendador) *" Then
                                                word = "(Arrendador)"
                                            End If
                                            If word = "(conocido)" Then
                                                word = "(Conocido)"
                                            End If
                                            If word = "(familiar)" Then
                                                word = "(Familiar)"
                                            End If
                                            If word = "¿qué" Then
                                                word = "¿Qué"
                                            End If
                                            If word = "¿usted" Then
                                                word = "¿Usted"
                                            End If
                                            If word = "¿cuál" Then
                                                word = "¿Cuál"
                                            End If
                                            If word = "Telefono1" Then
                                                word = "Telefono (Arrendador)"
                                            End If
                                            If word = "Nombre1" Then
                                                word = "Nombre Completo (Arrendador)"
                                            End If
                                            If word = "Comicilio" Then
                                                word = "Domicilio"
                                            End If
                                            strLabel = strLabel + word + " "
                                        Next
                                        'COD Tool TIP
                                        If ((Request("idPantalla") = 7) Or (Request("idPantalla") = 1)) Then
                                            asterisk = " *"
                                            If (strLabel = "Folio ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Tipo de Credito ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Homoclave ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Nacionalidad ") Then
                                                If (Request("idPantalla") = 1) Then
                                                    asterisk = "*"
                                                Else
                                                    asterisk = ""
                                                End If
                                            ElseIf (strLabel = "CURP ") Then
                                                asterisk = "*"
                                            ElseIf (strLabel = "Telefono Particular ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Telefono Movil * ") Then
                                                'strLabel = "Teléfono Móvil * "
                                                asterisk = ""
                                            ElseIf (strLabel = "Correo Electronico ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Regimen Conyugal ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Situacion Laboral ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "FIEL ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Numero de Identificacion Fiscal ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Pais de Asignacion ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Pais de Domicilio ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Ext ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Correo Electronico ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Delegacion o Municipio ") Then
                                                asterisk = ""
                                            ElseIf ((strLabel = "Ciudad ") And (strNombre <> "PDK_ID_LUGAR_NACIMIENTO")) Then
                                                If (Request("idPantalla") = 1) Then
                                                    asterisk = "*"
                                                Else
                                                    asterisk = "*"
                                                End If
                                            ElseIf ((strLabel = "Estado ") And (strNombre <> "PDK_ID_LUGAR_NACIMIENTO")) Then
                                                If (Request("idPantalla") = 1) Then
                                                    asterisk = "*"
                                                Else
                                                    asterisk = "*"
                                                End If
                                            ElseIf (strLabel = "Nombre de la Empresa del Empleo Anterior ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Teléfono de la Empresa Empleo Anterior ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Ext de la Empresa Empleo Anterior ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Antigüedad Laborar (Años) Empleo Anterior ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Antigüedad Laborar (Meses) Empleo Anterior ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Nombre (Arrendador) ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Telefono (Arrendador) ") Then
                                                asterisk = "*"
                                            ElseIf (strLabel = "Direccion (Arrendador) ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "¿Qué Función Desempeño o ha Desempeñado? ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Solicitante es Conductor Recurrente ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Número de Cotización * ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Apellido Paterno * ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Apellido Materno * ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Año Nacimiento * ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Mes Nacimiento * ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Día Nacimiento * ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "RFC * ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Homoclave * ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Delegación o Municipio * ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Tipo de Vivienda * ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Ingresos Fijos * ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Segundo Nombre ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Ingresos Variables ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Num Int ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Apellido Materno ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Renovación Ci ") Then
                                                strLabel = "Renovación CI"
                                                asterisk = ""
                                            ElseIf (strLabel = "Núm. Contrato ") Then
                                                asterisk = ""
                                            ElseIf (strLabel = "Autoriza Captura Huella ") Then
                                                asterisk = ""
                                            End If
                                        End If

                                        strLabel = strLabel.Replace("Telefono", "Teléfono").Replace("Movil", "Móvil").Replace("Operacion", "Operación").Replace("Credito", "Crédito")
                                        strLabel = strLabel.Replace("Pais", "País").Replace("Direccion", "Dirección").Replace("Numero", "Número").Replace("Electronico", "Electrónico")
                                        strLabel = strLabel.Replace("Identificacion", "Identificación").Replace("Domiciliacion", "Domiciliación").Replace("Situacion", "Situación")
                                        strLabel = strLabel.Replace("Asignacion", "Asignación")

                                        strLabel = strLabel.Replace("Laborar", "Laboral").Replace("Compañia", "Compañía")

                                        If (Request("idPantalla") = 1) Then
                                            If (strLabel.Contains("CURP")) Then
                                                strLabel = "CURP"
                                                asterisk = ""
                                            End If

                                            If (strLabel.Contains("Homoclave")) Then
                                                strLabel = "Homoclave"
                                                asterisk = ""
                                            End If

                                            If (strLabel.Contains("Teléfono Móvil")) Then
                                                strLabel = "Teléfono Móvil "
                                                asterisk = "*"
                                            End If

                                            If (strLabel.Contains("Teléfono Fijo")) Then
                                                strLabel = "Teléfono Fijo"
                                                asterisk = ""
                                            End If

                                            If (strLabel.Contains("Compañía Telefónica Teléfono Particular")) Then
                                                strLabel = "Compañía Fijo"
                                                asterisk = ""
                                            End If
                                            If (strLabel.Contains("Apellido Materno")) Then
                                                strLabel = "Apellido Materno "
                                                asterisk = ""
                                            End If
                                            If (strLabel.Contains("Situación Laboral")) Then
                                                strLabel = "Situación Laboral "
                                                asterisk = "*"
                                            End If
                                        End If


                                        If (Request("idPantalla") = 7) Then
                                            If (strLabel.Contains("Ingresos Variables")) Then
                                                strLabel = "Ingresos Variables *"
                                            End If

                                            If (strLabel.Contains("Situación Laboral")) Then
                                                strLabel = "Situación Laboral "
                                                asterisk = "*"
                                            End If

                                            If (strLabel.Contains("Clave de Elector")) Then
                                                strLabel = "Clave de Elector"
                                                asterisk = ""
                                                'asterisk = "<span style='color:blue'>***</span>"
                                            End If
                                            If (strLabel.Contains("Conyugal")) Then
                                                strLabel = "Régimen Conyugal"
                                                asterisk = ""
                                            End If
                                            If (strLabel.Contains("Homoclave")) Then
                                                strLabel = "Homoclave"
                                                asterisk = ""
                                            End If
                                            If (strLabel.Contains("CURP")) Then
                                                strLabel = "CURP"
                                                asterisk = ""
                                            End If

                                            If (strLabel.Contains("Teléfono Fijo")) Then
                                                strLabel = "Teléfono Particular "
                                                asterisk = ""
                                            End If

                                            If (strLabel.Contains("Compañía Telefónica Teléfono Particular")) Then
                                                strLabel = "Compañía Fijo"
                                                asterisk = ""
                                            End If



                                            If (strLabel.Contains("Tipo de Contrato Laboral")) Then
                                                strLabel = "Tipo de Contrato Laboral "
                                                asterisk = ""
                                            End If

                                            If (strLabel.Contains("Apellido Materno")) Then
                                                strLabel = "Apellido Materno "
                                                asterisk = ""
                                            End If

                                            If (strLabel.Contains("Fake 1")) Then
                                                strLabel = "Fake 1"
                                                asterisk = ""
                                            End If

                                            If (strLabel.Contains("Fake 2")) Then
                                                strLabel = "Fake 2"
                                                asterisk = ""
                                            End If

                                            strLabel = strLabel.Replace("¿Usted es o ha sido Una Persona Políticamente Expuesta?", "¿Usted es o ha sido una persona políticamente expuesta?")
                                            strLabel = strLabel.Replace("¿tiene Relación con Alguien que es o que ha sido PEP?", "¿Tiene relación con alguien que es o que ha sido PEP?")
                                            strLabel = strLabel.Replace("¿Qué Función Desempeño o ha Desempeñado?", "¿Qué función desempeña o ha desempeñado?")
                                            strLabel = strLabel.Replace("¿Qué Parentesco Tiene con el PEP?", "¿Qué parentesco tiene con el PEP? ")
                                            strLabel = strLabel.Replace("¿Cuál es el Nombre de la PEP con la que Tiene Parentesco?", "¿Cuál es el nombre de la PEP con la que tiene parentesco?")
                                            strLabel = strLabel.Replace("¿Qué Función Desempeño o ha Desempeñado la PEP con la que Tiene Parentesco?", "¿Qué función desempeñó o ha desempeñado la PEP con la que tiene parentesco?")
                                            strLabel = strLabel.Replace("¿Usted Actúa a Nombre de un Tercero?", "¿Usted actúa a nombre de un tercero?")
                                            strLabel = strLabel.Replace("Especifique Nombre", "Especifique Nombre")
                                        End If

                                        If Not DBNull.Value.Equals(dbData.Tables(0).Rows(rowCtr).Item("TOOLTIP")) Then
                                            Dim tool = "<span class=""tooltip"">"
                                            Dim toolText = "<span class=""tooltiptext"">"
                                            Dim toolEnd = "</span></span>"
                                            lbl.Text = tool + strLabel + asterisk + toolText + dbData.Tables(0).Rows(rowCtr).Item("TOOLTIP") + toolEnd
                                        Else
                                            lbl.Text = strLabel + asterisk
                                        End If
                                        'END COD Tool TIP
                                        lbl.Text = "<div Align = Right>" + lbl.Text + "</div>"
                                        lbl.SkinID = "lblGeneral"
                                        lbl.Width = "230"
                                        arregloLabel(rowCtr) = lbl
                                        tCell.Text = "Row " & rowCtr & ", Cell " & cellCtr
                                        tCell.Controls.Add(lbl)

                                        If ((dbData.Tables(0).Rows(rowCtr).Item("MOSTRAR") = "Otro (Especifique)") And (Counternx = 10)) Then
                                            Counternx = 1

                                            tRowTmp.Cells.Add(tCell)
                                            mitable.Rows.Add(tRowTmp)
                                        ElseIf ((dbData.Tables(0).Rows(rowCtr).Item("MOSTRAR") = "¿Qué función desempeño o ha desempeñado la PEP con la que tiene parentesco?") And (Counterfx = 10)) Then
                                            Counterfx = 1

                                            tRowTmp.Cells.Add(tCell)
                                            mitable.Rows.Add(tRowTmp)
                                        Else
                                            tRow.Cells.Add(tCell)
                                        End If

                                        'tRow.Cells.Add(tCell)

                                    Else
                                        'creamos TextBox DropDownList
                                        Dim dropList As New DropDownList
                                        Dim nvoText As New TextBox
                                        Dim txtgrande As New HtmlTextArea
                                        Dim chk As New CheckBox
                                        Dim radio As New RadioButtonList

                                        If dbData.Tables(0).Rows(rowCtr).Item("TIPO") = "DROPDOWNLIST" Then
                                            Dim dbdataset As New DataSet
                                            dropList.ID = "ddl" + dbData.Tables(0).Rows(rowCtr).Item("COLUMNA")
                                            dropList.Attributes.Add("onchange", "getval(this);")
                                            dropList.SkinID = "cmbGeneral"
                                            dropList.CssClass = "select2BBVA"
                                            dropList.Width = "176"
                                            'dropList.Items.Add("")
                                            'dropList.Items.Add("PROPIA")



                                            If dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE").ToString = "COLONIA" Then
                                                If hdnCp.Value <> "" Then
                                                    dbdataset = clsPantallaObjeto.ObtenerCpCol(hdnCp.Value)
                                                    hdnCp.Value = ""
                                                ElseIf hdnCp1.Value <> "" Then
                                                    dbdataset = clsPantallaObjeto.ObtenerCpCol(hdnCp1.Value)
                                                    hdnCp1.Value = ""
                                                ElseIf hdnCp2.Value <> "" Then
                                                    dbdataset = clsPantallaObjeto.ObtenerCpCol(hdnCp2.Value)
                                                    hdnCp2.Value = ""
                                                ElseIf hdnCp3.Value <> "" Then
                                                    dbdataset = clsPantallaObjeto.ObtenerCpCol(hdnCp3.Value)
                                                    hdnCp3.Value = ""
                                                ElseIf hdnCp4.Value <> "" Then
                                                    dbdataset = clsPantallaObjeto.ObtenerCpCol(hdnCp4.Value)
                                                    hdnCp4.Value = ""

                                                End If


                                                If dbdataset.Tables.Count > 0 AndAlso dbdataset.Tables(0).Rows.Count > 0 Then
                                                    ''    ''Dim dsDataset As New DataSet
                                                    ''    ''dsDataset = clsPantallas.InsertTabDina(dbdataset.Tables(0).Rows(0).Item("SELE"))
                                                    ''    ''If dsDataset.Tables(0).Rows.Count > 0 AndAlso dsDataset.Tables.Count > 0 Then
                                                    dropList.DataSource = dbdataset.Tables(0)
                                                    dropList.DataValueField = "CPO_FL_CP"
                                                    dropList.DataTextField = "CPO_DS_COLONIA"
                                                    dropList.DataBind()
                                                    ''    'End If
                                                End If
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE").ToString = "PLAZO" Then
                                                dbdataset = clsPantallaObjeto.ObtenerControles(hdnIdRegistro.Value, 5, dbDabase.Tables(0).Rows(i).Item("PDK_ID_SECCION"), 0, dbData.Tables(0).Rows(rowCtr).Item("PDK_ID_SECCION_DATO"))
                                                If dbdataset.Tables.Count > 0 AndAlso dbdataset.Tables(0).Rows.Count > 0 Then
                                                    dropList.DataSource = dbdataset.Tables(0)
                                                    dropList.DataValueField = "PDK_ID_PRODPLAZO"
                                                    dropList.DataTextField = "PDK_PRODPLAZO_VALOR"
                                                    dropList.DataBind()

                                                End If
                                            Else
                                                dbdataset = clsPantallaObjeto.ObtenerControles(hdnIdRegistro.Value, 2, dbDabase.Tables(0).Rows(i).Item("PDK_ID_SECCION"), 0, dbData.Tables(0).Rows(rowCtr).Item("PDK_ID_SECCION_DATO"))
                                                If dbdataset.Tables.Count > 0 AndAlso dbdataset.Tables(0).Rows.Count > 0 Then
                                                    ''    ''Dim dsDataset As New DataSet
                                                    ''    ''dsDataset = clsPantallas.InsertTabDina(dbdataset.Tables(0).Rows(0).Item("SELE"))
                                                    ''    ''If dsDataset.Tables(0).Rows.Count > 0 AndAlso dsDataset.Tables.Count > 0 Then
                                                    dropList.DataSource = dbdataset.Tables(0)
                                                    dropList.DataValueField = "PDK_ID_SCORING"
                                                    dropList.DataTextField = "PDK_SCORING_VALOR1"
                                                    dropList.DataBind()
                                                    ''    'End If
                                                End If

                                            End If



                                            If dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "N" And dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "0" Then
                                                dropList.SelectedIndex = dropList.Items.IndexOf(dropList.Items.FindByText(dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")))
                                                dropList.Enabled = False
                                                If dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtANO_NACIMIENTO412" Then
                                                    hdnano.Value = dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")
                                                ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtMES_NACIMIENTO413" Then
                                                    hdnmes.Value = dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")
                                                ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtDIA_NACIMIENTO414" Then
                                                    hdndia.Value = dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")
                                                ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtANO_NACIMIENTO415" Then
                                                    hdnanocoa.Value = dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")
                                                ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtMES_NACIMIENTO416" Then
                                                    hdnmescoa.Value = dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")
                                                ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtDIA_NACIMIENTO417" Then
                                                    hdndiacoa.Value = dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")
                                                End If
                                            End If


                                            If dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtANO_NACIMIENTO412" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtMES_NACIMIENTO413" Then
                                                dropList.Attributes.Add("onchange", "llenaddlDias();CalcularRFC();calculaEdad();")
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtDIA_NACIMIENTO414" Then
                                                dropList.Attributes.Add("onchange", "CalcularRFC();calculaEdad();")
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtANO_NACIMIENTO415" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtMES_NACIMIENTO416" Then
                                                dropList.Attributes.Add("onchange", "llenaddlDiascoa();CalcularRFCcoa();calculaEdadcoa();")
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtDIA_NACIMIENTO417" Then
                                                dropList.Attributes.Add("onchange", "CalcularRFCcoa();calculaEdadcoa();")
                                            End If



                                            arregloCombos(rowCtr) = dropList
                                            tCell.Text = "Row " & rowCtr & ", Cell " & cellCtr
                                            tCell.Controls.Add(dropList)
                                            tRow.Cells.Add(tCell)

                                            'AgregarControles(lbl, nvoText, dropList, 1)

                                        ElseIf dbData.Tables(0).Rows(rowCtr).Item("TIPO") = "TEXTBOXGRA" Then
                                            txtgrande.ID = dbData.Tables(0).Rows(rowCtr).Item("COLUMNA")
                                            txtgrande.Attributes.Add("class", "txtBBVA")
                                            txtgrande.Rows = "3"
                                            txtgrande.Cols = "1"
                                            If dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtOBSERVACION428" Then
                                                txtgrande.Attributes.Add("style", "width:500px")
                                                txtgrande.Disabled = True
                                            Else
                                                txtgrande.Attributes.Add("style", "width:176px")
                                            End If
                                            If dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "N" Then
                                                txtgrande.Value = dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")
                                                txtgrande.Disabled = True

                                            End If

                                            nvoText.Enabled = "true"
                                            arregloTextTarea(rowCtr) = txtgrande
                                            tCell.Text = "Row " & rowCtr & ", Cell " & cellCtr
                                            tCell.Controls.Add(txtgrande)
                                            tRow.Cells.Add(tCell)

                                        ElseIf dbData.Tables(0).Rows(rowCtr).Item("TIPO") = "CHECKBOX" Then
                                            chk.ID = dbData.Tables(0).Rows(rowCtr).Item("COLUMNA")
                                            If dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "N" Then
                                                'chk.Enabled = False
                                                chk.Checked = True
                                            End If
                                            arregloCheck(rowCtr) = chk
                                            tCell.Text = "Row " & rowCtr & ", Cell " & cellCtr
                                            tCell.Controls.Add(chk)
                                            tRow.Cells.Add(tCell)
                                        ElseIf dbData.Tables(0).Rows(rowCtr).Item("TIPO") = "RADIOBUTTON" Then
                                            Dim dbdataset1 As New DataSet
                                            radio.ID = dbData.Tables(0).Rows(rowCtr).Item("COLUMNA")

                                            dbdataset1 = clsPantallaObjeto.ObtenerControles(hdnIdRegistro.Value, 2, dbDabase.Tables(0).Rows(i).Item("PDK_ID_SECCION"), 0, dbData.Tables(0).Rows(rowCtr).Item("PDK_ID_SECCION_DATO"))
                                            If dbdataset1.Tables.Count > 0 AndAlso dbdataset1.Tables(0).Rows.Count > 0 Then
                                                ''    ''Dim dsDataset As New DataSet
                                                ''    ''dsDataset = clsPantallas.InsertTabDina(dbdataset.Tables(0).Rows(0).Item("SELE"))
                                                ''    ''If dsDataset.Tables(0).Rows.Count > 0 AndAlso dsDataset.Tables.Count > 0 Then
                                                radio.DataSource = dbdataset1.Tables(0)
                                                radio.DataValueField = "PDK_ID_SCORING"
                                                radio.DataTextField = "PDK_SCORING_VALOR1"
                                                radio.DataBind()
                                                ''    'End If
                                            End If
                                            If dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "N" Then
                                                radio.SelectedIndex = radio.Items.IndexOf(radio.Items.FindByText(dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")))

                                                If intEnable = 1 Then
                                                    radio.Enabled = False
                                                End If

                                            End If
                                            radio.RepeatDirection = RepeatDirection.Horizontal
                                            radio.Width = "176"
                                            arregloRadio(rowCtr) = radio
                                            tCell.Text = "Row " & rowCtr & ", Cell " & cellCtr
                                            tCell.Controls.Add(radio)
                                            tRow.Cells.Add(tCell)


                                        Else
                                            nvoText.ID = dbData.Tables(0).Rows(rowCtr).Item("COLUMNA")
                                            If dbData.Tables(0).Rows(rowCtr).Item("TIPO") = "TEXTBOXGRA" Then
                                                nvoText.Width = "400"
                                                nvoText.Height = "100"
                                            Else
                                                nvoText.Width = "400"
                                            End If


                                            If dbData.Tables(0).Rows(rowCtr).Item("CONVE") = "ENTERO" Then
                                                nvoText.SkinID = "txtNumerosGden"
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("CONVE") = "FECHA" Then
                                                nvoText.SkinID = "txtAlfaMayGde1"
                                                nvoText.Text = Format(Now(), "yyyy-MM-dd")
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("CONVE") = "NUMERICO" Then
                                                nvoText.SkinID = "txtNumerosGden"
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("CONVE") = "MONEDA" Then
                                                nvoText.SkinID = "txtMontosN"
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("CONVE") = "TEXTO" Then
                                                If dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "NUM_EXT" Or dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "NUM_INT" Or dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "CURP" Or dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "CALLE" Or dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "RFC" Or dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "HOMOCLAVE" Or dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "SECCION" Then
                                                    nvoText.SkinID = "txtAlfaMayGde1"
                                                ElseIf dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "CORREO_ELECTRONICO" Or dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "CORREO_ELECTRONICO1" Or dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "CORREO_ELECTRONICO2" Then
                                                    nvoText.SkinID = "txtMailGde"
                                                ElseIf dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "COMPANIA" Then
                                                    nvoText.SkinID = "txtCustMayGde"
                                                Else
                                                    nvoText.SkinID = "txtAlfaMayGden"
                                                End If



                                            End If

                                            If dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtCP14" Then
                                                nvoText.Attributes.Add("onblur", "llenado(this)")
                                                If dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "0" And dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "N" Then
                                                    hdnCp.Value = dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")
                                                End If
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtCP50" Then
                                                nvoText.Attributes.Add("onblur", "llenadoEmp(this)")
                                                If dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "0" And dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "N" Then
                                                    hdnCp1.Value = dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")
                                                End If
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtCP84" Then
                                                nvoText.Attributes.Add("onblur", "llenadoCoa(this)")
                                                If dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "0" And dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "N" Then
                                                    hdnCp2.Value = dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")
                                                End If
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtCP121" Then
                                                nvoText.Attributes.Add("onblur", "llenarcoaemp(this)")
                                                If dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "0" And dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "N" Then
                                                    hdnCp3.Value = dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")
                                                End If
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtCP457" Then
                                                nvoText.Attributes.Add("onblur", "llenarpersonamoral(this)")
                                                If dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "0" And dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "N" Then
                                                    hdnCp4.Value = dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")
                                                End If
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtNOMBRE14" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtNOMBRE25" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtAPELLIDO_PATERNO6" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtAPELLIDO_MATERNO7" Then
                                                nvoText.Attributes.Add("onblur", "CalcularRFC();ConcatenarNom();")
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtNOMBRE176" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtNOMBRE277" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtAPELLIDO_PATERNO78" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtAPELLIDO_MATERNO79" Then
                                                nvoText.Attributes.Add("onblur", "CalcularRFCcoa();")
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtCORREO_ELECTRONICO11" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtCORREO_ELECTRONICO268" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtCORREO_ELECTRONICO164" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtCORREO_ELECTRONICO59" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA").ToString = "txtCORREO_ELECTRONICO83" Then
                                                'nvoText.Attributes.Add("onblur", "regCorreo(this)")
                                                nvoText.Attributes.Add("onblur", "validarcorreo(this)")


                                            End If


                                            If dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE").ToString = strNombre Then
                                                Dim dbDat As New DataSet
                                                dbDat = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 3)
                                                If dbDat.Tables.Count > 0 AndAlso dbDat.Tables(0).Rows.Count > 0 Then
                                                    nvoText.Text = dbDat.Tables(0).Rows(0).Item("numero")
                                                End If
                                                nvoText.Enabled = False
                                            Else
                                                nvoText.Enabled = True
                                            End If

                                            If dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") <> "N" Then
                                                nvoText.Text = dbData.Tables(0).Rows(rowCtr).Item("REGISTRO")

                                                If dbData.Tables(0).Rows(rowCtr).Item("CVEPANTALLA") = 1 Then
                                                    If intEnable = 1 Then
                                                        nvoText.Enabled = "false"
                                                    Else
                                                        If dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtNUM_SOLICITUD418" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtSTATUS420" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtNUMERO_CLIENTE434" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtSTATUS_DOC446" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtSTATUS_CREDITO445" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtNOMBRE_SOLICI419" Then
                                                            'AMR Se agrega el campo Nombre solicitante para que se inactive.
                                                            'D@vE_® se quita para que no inactive el campo de la cotización.
                                                            'Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtNUM_COTIZACION441" 
                                                            nvoText.Enabled = "false"

                                                        ElseIf dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "RFC" Then
                                                            nvoText.Enabled = "false"
                                                        End If
                                                    End If

                                                ElseIf dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") = "0" Or dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") = "0.00" Then
                                                    nvoText.Enabled = "true"
                                                Else
                                                    nvoText.Enabled = "false"
                                                End If
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("CONVE") = "NUMERICO" Or dbData.Tables(0).Rows(rowCtr).Item("CONVE") = "ENTERO" Or dbData.Tables(0).Rows(rowCtr).Item("CONVE") = "MONEDA" Then
                                                If dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtEDAD13" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtEDAD_COAC105" Then
                                                    nvoText.Text = 0
                                                    nvoText.Enabled = "false"
                                                ElseIf dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "IMPORTE_RENTA" Then
                                                    If dbData.Tables(0).Rows(rowCtr).Item("ACTIVA") = "2" Then
                                                        nvoText.Enabled = True
                                                        If dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtIMPORTE_RENTA436" Then
                                                            hdnValiRenta.Value = "IMPORTE_RENTA436"
                                                        ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtIMPORTE_RENTA437" Then
                                                            hdnValiRenta1.Value = "IMPORTE_RENTA437"
                                                        End If


                                                    Else
                                                        If dbData.Tables(0).Rows(rowCtr).Item("CVEPANTALLA") = 15 Then
                                                            nvoText.Enabled = True
                                                        Else
                                                            'Se comenta para que el importe de la renta este activo D@ver 20140228
                                                            'nvoText.Enabled = False
                                                            nvoText.Enabled = True
                                                        End If

                                                        If dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtIMPORTE_RENTA436" Then
                                                            hdnValiRenta.Value = ""
                                                        ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtIMPORTE_RENTA437" Then
                                                            hdnValiRenta1.Value = ""
                                                        End If


                                                    End If
                                                Else
                                                    'nvoText.Text = 0
                                                    nvoText.Enabled = "true"
                                                End If


                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("REGISTRO") = "N" Then
                                                If dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtNOMBRE_SOLICI419" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtNUM_SOLICITUD418" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtSTATUS420" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtNUM_COTIZACION441" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtNUMERO_CLIENTE434" Then
                                                    nvoText.Enabled = "false"
                                                Else
                                                    nvoText.Enabled = "true"
                                                End If

                                            End If
                                            If dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "DELEGA_O_MUNI" Or dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "ESTADO" Or dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "CIUDAD" Or dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "DELEGA_O_MUNICI" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtNUMERO_CLIENTE434" Then
                                                'Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtNUM_COTIZACION441"
                                                nvoText.Enabled = "false"
                                            End If
                                            Dim TAMAÑO As Integer = dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_TAMANO")
                                            'If dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "RFC" Then
                                            '    nvoText.MaxLength = 10
                                            'Else
                                            nvoText.MaxLength = TAMAÑO
                                            'End If
                                            'nvoText.Attributes.Add("maxlength", dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_TAMANO"))

                                            'nvoText.Width = "100"
                                            arregloTextBoxs(rowCtr) = nvoText
                                            tCell.Text = "Row " & rowCtr & ", Cell " & cellCtr
                                            tCell.Controls.Add(nvoText)

                                            'If dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "TELEFONO_CEL" Then
                                            If dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "FUNCION_PEPS_OTRA_" Then

                                                tRowTmp.Cells.Add(tCell)
                                                mitable.Rows.Add(tRowTmp)
                                                cellCtr = 0
                                                'Dim tCellTmp As New TableCell()
                                                'tCellTmp.Text = "<font size=3>REFERENCIAS (CONOCIDOS/FAMILIARES)</font>"
                                                'tRowTmp2.Cells.Add(tCellTmp)
                                                'mitable.Rows.Add(tRowTmp2)


                                                'ElseIf dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "NOMBRE22222" Then
                                                'tRowTmp.Cells.Add(tCell)
                                                'mitable.Rows.Add(tRowTmp)
                                                'Dim tCellTmp As New TableCell()
                                                'tCellTmp.Text = "<font size=3>REFERENCIAS (FAMILIAR)</font>"
                                                'tRowTmp2.Cells.Add(tCellTmp)
                                                'mitable.Rows.Add(tRowTmp2)
                                            ElseIf (dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "FUNCION_REL_PEPS_") Then
                                                tRowTmp.Cells.Add(tCell)
                                                mitable.Rows.Add(tRowTmp)
                                                cellCtr = 0
                                            Else
                                                tRow.Cells.Add(tCell)
                                            End If


                                            'AgregarControles(lbl, nvoText, dropList)
                                        End If
                                        If cellCtr <> 4 Then
                                            rowCtr = rowCtr + 1
                                        End If

                                        If rowCtr = rowCnt Then
                                            'mitable.Rows.Add(tRow)
                                            Exit For
                                        End If

                                    End If
                                Next


                                mitable.Rows.Add(tRow)
                                arregloTable(i) = mitable


                            Next
                            pantalla.Controls.Add(mitable)

                        Next

                    End If

                    If hdnPantalla.Value = "SOLICITUD" Then
                        btnImprimir.Attributes.Add("OnClick", "JavaScript:CallWindow('ImprimirCreditoSolicitud.aspx?idPantalla=" & hdnIdRegistro.Value & "&IdFolio=" & hdnFolio.Value & "&CVE=0" & "'); return false;")
                    ElseIf hdnPantalla.Value = "SOLICITUD SOLICITANTE" Then
                        btnImprimir.Attributes.Add("OnClick", "JavaScript:CallWindow('ImprimirCreditoSolicitud.aspx?idPantalla=" & hdnIdRegistro.Value & "&IdFolio=" & hdnFolio.Value & "&CVE=2" & "'); return false;")
                    ElseIf hdnPantalla.Value = "DICTAMEN PRECALIFICACION" Then
                        btnImprimir.Attributes.Add("OnClick", "JavaScript:CallWindow('ImprimirCreditoSolicitud.aspx?idPantalla=" & hdnIdRegistro.Value & "&IdFolio=" & hdnFolio.Value & "&CVE=3" & "'); return false;")
                    ElseIf hdnPantalla.Value = "PRECALIFICACION PERSONAS MORALES" Then
                        btnImprimir.Attributes.Add("OnClick", "JavaScript:CallWindow('ImprimirCreditoSolicitud.aspx?idPantalla=" & hdnIdRegistro.Value & "&IdFolio=" & hdnFolio.Value & "&CVE=4" & "'); return false;")
                    ElseIf hdnPantalla.Value = "Solicitud de Credito" Then
                        btnImprimir.Attributes.Add("OnClick", "JavaScript:CallWindow('ImprimirCreditoSolicitud.aspx?idPantalla=" & hdnIdRegistro.Value & "&IdFolio=" & hdnFolio.Value & "&CVE=5" & "'); return false;")

                    Else
                        btnImprimir.Attributes.Add("OnClick", "JavaScript:CallWindow('ImprimirCreditoSolicitud.aspx?idPantalla=" & hdnIdRegistro.Value & "&IdFolio=" & hdnFolio.Value & "&IdUsu=" & hdnUsuario.Value & "&CVE=0" & "'); return false;")

                    End If

                End If
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)

        End Try

    End Sub

    Private Sub AgregarControles(ByVal inLabe As Label, ByVal inTxt As TextBox, ByVal inDropList As DropDownList, Optional ByVal intBandera As Integer = 0)
        Try

            pantalla.Controls.Add(inLabe)
            pantalla.Controls.Add(New LiteralControl())
            If intBandera = 1 Then
                pantalla.Controls.Add(inDropList)
                pantalla.Controls.Add(New LiteralControl(""))

            Else
                pantalla.Controls.Add(inTxt)

            End If



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        'Response.Redirect("./consultaPantalla.aspx")
        Dim dc As New clsDatosCliente
        dc.idSolicitud = Request("IdFolio")
        dc.getDatosSol()
        Response.Redirect(Session("Regresar") & "?NoSolicitud=" & Request("IdFolio") & "&Empresa=" & dc.idempresa & "&Producto=" & dc.idproducto & "&Persona=" & dc.idtpersona)

    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim dbDataset As New DataSet
        Dim dbDabase As New DataSet
        Dim fecha As String
        Dim vart As String = ""
        Dim strNombre As String = ""
        Dim NUM As Integer = 0
        Dim intSoli As Integer = 0

        Try

            fecha = Format(Now(), "yyyy-MM-dd")

            dbDabase = clsPantallaObjeto.ObtenerControles(hdnIdRegistro.Value, 3)
            If dbDabase.Tables(0).Rows.Count > 0 AndAlso dbDabase.Tables.Count > 0 Then
                intSoli = dbDabase.Tables(0).Rows(0).Item("FOLIO")
                dbDataset = clsPantallaObjeto.ObtenerControles(hdnIdRegistro.Value, 4)
                If dbDataset.Tables.Count > 0 And dbDataset.Tables(0).Rows.Count > 0 Then
                    If intSoli <> dbDataset.Tables(0).Rows(0).Item("CVE") Then
                        Dim dbDataser1 As New DataSet
                        dbDataser1 = clsPantallas.InsertTabDina("INSERT INTO PDK_TAB_SECCION_CERO(PDK_ID_SECCCERO ,FECHA,CLAVE_USUARIO )VALUES(" & intSoli & ",'" & fecha & "'," & Session("IdUsua") & ")")
                    End If
                End If
            End If

            dbDabase = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 4)
            If dbDabase.Tables.Count > 0 AndAlso dbDabase.Tables(0).Rows.Count > 0 Then
                For NUM = 0 To dbDabase.Tables(0).Rows.Count - 1
                    dbDataset = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 2, dbDabase.Tables(0).Rows(NUM).Item("PDK_ID_SECCION"))
                    If dbDataset.Tables(0).Rows.Count > 0 AndAlso dbDataset.Tables.Count > 0 Then
                        strNombre = dbDataset.Tables(0).Rows(0).Item("PDK_SEC_NOMBRE_TABLA").ToString.Trim
                    End If
                    dbDataset = Nothing
                    dbDataset = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 1, dbDabase.Tables(0).Rows(NUM).Item("PDK_ID_SECCION"))
                    If dbDataset.Tables(0).Rows.Count > 0 AndAlso dbDataset.Tables.Count > 0 Then
                        For i = 0 To dbDataset.Tables(0).Rows.Count - 1
                            If dbDataset.Tables(0).Rows(i).Item("TEXTO") = "TextBox" Then
                                If dbDataset.Tables(0).Rows(i).Item("PDK_REL_PANT_OBJ_NOMBRE") <> strNombre Then
                                    If dbDataset.Tables(0).Rows(i).Item("CONVE") = "TEXTO" Then
                                        vart &= "'" & arregloTextBoxs(i).Text.ToUpper & "'" & ","
                                    Else
                                        vart &= arregloTextBoxs(i).Text & ","

                                    End If

                                End If

                            Else
                                vart &= arregloCombos(i).SelectedValue & ","

                            End If

                        Next
                    End If
                    dbDataset = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 0, dbDabase.Tables(0).Rows(NUM).Item("PDK_ID_SECCION"))
                    If dbDataset.Tables(0).Rows.Count > 0 AndAlso dbDataset.Tables.Count > 0 Then
                        Dim dbDataser1 As New DataSet
                        dbDataser1 = clsPantallas.InsertTabDina(dbDataset.Tables(0).Rows(0).Item("CAMPOS") & "VALUES(" & vart & "'" & fecha & "'" & "," & Session("IdUsua") & "," & intSoli & ")")

                    End If

                Next

                Master.MensajeError("Información almacenada exitosamente")
                'LimpiarDatos()

            End If


            'dbDataset = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 2)
            'If dbDataset.Tables(0).Rows.Count > 0 AndAlso dbDataset.Tables.Count > 0 Then
            '    strNombre = dbDataset.Tables(0).Rows(0).Item("PDK_SEC_NOMBRE_TABLA").ToString.Trim
            'End If

            'dbDataset = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 1)
            'If dbDataset.Tables(0).Rows.Count > 0 AndAlso dbDataset.Tables.Count > 0 Then
            '    For i = 0 To dbDataset.Tables(0).Rows.Count - 1
            '        If dbDataset.Tables(0).Rows(i).Item("TEXTO") = "TextBox" Then
            '            If dbDataset.Tables(0).Rows(i).Item("PDK_REL_PANT_OBJ_NOMBRE") <> strNombre Then
            '                If dbDataset.Tables(0).Rows(i).Item("CONVE") = "TEXTO" Then
            '                    vart &= "'" & arregloTextBoxs(i).Text.ToUpper & "'" & ","
            '                Else
            '                    vart &= arregloTextBoxs(i).Text & ","

            '                End If

            '            End If

            '        Else
            '            vart &= arregloCombos(i).SelectedValue & ","

            '        End If

            '    Next
            'End If
            'dbDataset = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value)
            'If dbDataset.Tables(0).Rows.Count > 0 AndAlso dbDataset.Tables.Count > 0 Then
            '    Dim dbDataser1 As New DataSet
            '    dbDataser1 = clsPantallas.InsertTabDina(dbDataset.Tables(0).Rows(0).Item("CAMPOS") & "VALUES(" & vart & "'" & fecha & "'" & "," & Session("IdUsua") & "," & 1 & ")")
            '    Master.MensajeError("Información almacenada exitosamente")
            '    LimpiarDatos()
            'End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarDatos()
        Dim dbDataset As New DataSet
        Dim strNombre As String = ""





        dbDataset = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 2)
        If dbDataset.Tables.Count > 0 AndAlso dbDataset.Tables(0).Rows.Count > 0 Then
            strNombre = dbDataset.Tables(0).Rows(0).Item("PDK_SEC_NOMBRE_TABLA").ToString.Trim
        End If
        dbDataset = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 1)
        If dbDataset.Tables(0).Rows.Count > 0 AndAlso dbDataset.Tables.Count > 0 Then
            For i = 0 To dbDataset.Tables(0).Rows.Count - 1
                If dbDataset.Tables(0).Rows(i).Item("TEXTO") = "TextBox" Then
                    If dbDataset.Tables(0).Rows(i).Item("PDK_REL_PANT_OBJ_NOMBRE").ToString = strNombre Then
                        Dim dbDat As New DataSet
                        dbDat = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 3)
                        If dbDat.Tables.Count > 0 AndAlso dbDat.Tables(0).Rows.Count > 0 Then
                            arregloTextBoxs(i).Text = dbDat.Tables(0).Rows(0).Item("numero")
                        End If

                    Else
                        arregloTextBoxs(i).Text = ""
                    End If

                End If

            Next
        End If


    End Sub

    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImprimir.Click
        Try

            If hdnPantalla.Value = "SOLICITUD" Or hdnPantalla.Value = "SOLICITUD SOLICITANTE" Or hdnPantalla.Value = "DICTAMEN PRECALIFICACION" Or hdnPantalla.Value = "PRECALIFICACION PERSONAS MORALES" Then


                Response.Redirect("./Blanco.aspx?idFolio=" & hdnFolio.Value & "&idPantalla=" & hdnIdRegistro.Value & "&Enable=1")

            Else
                'Dim strXML As String = ""
                'Dim strXSL As String = ""
                'Session("xml") = Nothing
                'Session("xsl") = Nothing

                'strXML = clsPantallaObjeto.ObtenerPantaXlm(hdnIdRegistro.Value, hdnFolio.Value, 1)
                'strXSL = clsPantallaObjeto.ObtenerPantaXlm(hdnIdRegistro.Value, hdnFolio.Value, 2)
                'If strXML <> "" Then
                '    Session("xml") = strXML
                'End If
                'If strXSL <> "" Then
                '    Session("xsl") = strXSL
                'End If

                'If strXML <> "" AndAlso strXSL <> "" Then
                '    'Response.Redirect("./ImprimirSolicitud.aspx?idPantalla=" & hdnIdRegistro.Value & "&IdFolio=" & hdnFolio.Value & "")


                'End 

                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "script", "__doPostBack('', '');", True)

            End If



        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try






        ''Response.Redirect("./ImprimirSolicitud.aspx?idPantalla=" & hdnIdRegistro.Value & "&IdFolio=" & hdnFolio.Value & "")
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function jsonDestinoUnidad(ByVal folio_id As String) As String
        Dim salida As String = ""
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ConexProcotiza").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            Dim cls As aspx_Blanco = New aspx_Blanco()
            Dim cotizacion As String = cls.getCotizacion(folio_id)
            Dim id_cot As Integer = Integer.Parse(cotizacion)
            cmd.CommandText = "get_destinoUnidad_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@FOLIO", id_cot)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                salida = reader(0)
            Loop
        Catch ex As Exception
            salida = "ERROR"
        End Try
        sqlConnection1.Close()
        Return salida
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function jsonHondaAcura(ByVal folio_id As String) As String
        Dim salida As String = ""
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ConexProcotiza").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            Dim cls As aspx_Blanco = New aspx_Blanco()
            Dim cotizacion As String = cls.getCotizacion(folio_id)
            Dim id_folio As Integer = Integer.Parse(cotizacion)
            cmd.CommandText = "get_hondaAcura_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@FOLIO", id_folio)
            cmd.Parameters.AddWithValue("@FOLIO_PRD", folio_id)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Dim str = ""
            Do While reader.Read()
                If (reader(1)) Then
                    str = "1"
                Else
                    str = "0"
                End If
                salida = reader(0).ToString() + ";" + str
            Loop
        Catch ex As Exception
            salida = "ERROR"
        End Try
        sqlConnection1.Close()
        Return salida
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function jsonTipoPersona(ByVal folio_id As String) As String
        Dim TP As String = ""
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            'get_compraInteligente_SP
            Dim id_folio As Integer = Integer.Parse(folio_id)
            cmd.CommandText = "get_tipoPersona_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@FOLIO", id_folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                TP = reader(0)
                TP = TP + "&$&" + reader(1)
            Loop
        Catch ex As Exception
            TP = "ERROR"
        End Try
        sqlConnection1.Close()
        Return TP
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function jsonCompraInteligente(ByVal folio_id As String) As String
        Dim CI As String = ""
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ConexProcotiza").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            Dim cls As aspx_Blanco = New aspx_Blanco()
            Dim cotizacion As String = cls.getCotizacion(folio_id)
            Dim id_folio As Integer = Integer.Parse(cotizacion)
            cmd.CommandText = "get_compraInteligente_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@FOLIO", id_folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                CI = reader(1)
            Loop
        Catch ex As Exception
            CI = "ERROR"
        End Try
        sqlConnection1.Close()
        Return CI
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function jsonCiudades(ByVal city_id As String) As String
        Dim cError As ciudades = New ciudades()
        Dim ciudades As New List(Of ciudades)
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        If city_id = "0" Then
            cError.id = 0
            cError.c = "ERROR VALOR INVALIDO"
            ciudades.Add(cError)
        Else
            Try
                'EXEC paises_GetSP 23
                Dim id_pais As Integer = Integer.Parse(city_id)

                If id_pais < 1540 Then
                    id_pais = id_pais - 1510

                    If id_pais = 24 Then
                        id_pais = 25
                    End If
                Else
                    id_pais = id_pais - 9392

                    If id_pais = 3 Then
                        id_pais = 4
                    ElseIf id_pais = 4 Then
                        id_pais = 5
                    ElseIf id_pais = 5 Then
                        id_pais = 6
                    ElseIf id_pais = 6 Then
                        id_pais = 10
                    ElseIf id_pais = 7 Then
                        id_pais = 12
                    ElseIf id_pais = 8 Then
                        id_pais = 11
                    ElseIf id_pais = 9 Then
                        id_pais = 13
                    ElseIf id_pais = 10 Then
                        id_pais = 14
                    ElseIf id_pais = 11 Then
                        id_pais = 15
                    ElseIf id_pais = 12 Then
                        id_pais = 16
                    ElseIf id_pais = 13 Then
                        id_pais = 17
                    ElseIf id_pais = 14 Then
                        id_pais = 18
                    ElseIf id_pais = 15 Then
                        id_pais = 20
                    ElseIf id_pais = 16 Then
                        id_pais = 21
                    ElseIf id_pais = 17 Then
                        id_pais = 22
                    ElseIf id_pais = 18 Then
                        id_pais = 24
                    ElseIf id_pais = 19 Then
                        id_pais = 26
                    ElseIf id_pais = 20 Then
                        id_pais = 27
                    ElseIf id_pais = 21 Then
                        id_pais = 28
                    ElseIf id_pais = 22 Then
                        id_pais = 30
                    ElseIf id_pais = 23 Then
                        id_pais = 33

                        Dim c As ciudades = New ciudades()
                        c.id = "0"
                        c.c = "N/A"
                        ciudades.Add(c)

                        Dim serializer_ As New System.Web.Script.Serialization.JavaScriptSerializer()
                        Dim json_Ciudades_ As String = serializer_.Serialize(ciudades)
                        Return json_Ciudades_

                    ElseIf id_pais = 24 Then
                        id_pais = 31
                    ElseIf id_pais = 25 Then
                        id_pais = 32
                    End If
                End If

                cmd.CommandText = "get_ciudades_SP"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@CIUDAD", id_pais)
                cmd.Connection = sqlConnection1
                sqlConnection1.Open()
                reader = cmd.ExecuteReader()

                Do While reader.Read()
                    Dim c As ciudades = New ciudades()
                    c.id = reader(0) 'Is possible access to name Field
                    c.c = reader(1)
                    ciudades.Add(c)
                Loop
            Catch ex As Exception
                cError.id = 0
                cError.c = "ERROR CONEXION"
                ciudades.Add(cError)
            End Try
            sqlConnection1.Close()
        End If

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Ciudades As String = serializer.Serialize(ciudades)
        Return json_Ciudades
    End Function

    Private Function getCotizacion(ByVal folio_id As String) As String
        Dim COT As String = ""
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            Dim id_folio As Integer = Integer.Parse(folio_id)
            cmd.CommandText = "get_cotizacion_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@FOLIO", id_folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                COT = reader(0)
            Loop
        Catch ex As Exception
            COT = "ERROR"
        End Try
        sqlConnection1.Close()
        Return COT
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function getCity(ByVal folio_id As String) As String
        Dim CD As String = ""
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            Dim id_folio As Integer = Integer.Parse(folio_id)
            cmd.CommandText = "get_ciudad_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@FOLIO", id_folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                CD = reader(0)
            Loop
        Catch ex As Exception
            CD = "ERROR"
        End Try
        sqlConnection1.Close()
        Return CD
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function Proccess(ByVal Tablas As List(Of jsonfields)) As String
        Dim COT As String = ""
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Try
            Dim procesar As String = Tablas(0).Finalizar
            Dim cls As aspx_Blanco = New aspx_Blanco()
            Dim queryToExec As List(Of Querys) = cls.dinamicQuery(Tablas, procesar)

            Dim XmlSerializer As System.Xml.Serialization.XmlSerializer = New System.Xml.Serialization.XmlSerializer(queryToExec.GetType)
            Dim xmlINE As StringWriter = New StringWriter()
            XmlSerializer.Serialize(xmlINE, queryToExec)

            Dim cmd As New SqlCommand
            Dim reader As SqlDataReader
            cmd.CommandText = "set_PrecaSolicitud_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", Tablas(0).Folio.ToString())
            cmd.Parameters.AddWithValue("@XML_INFO", xmlINE.ToString())
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                COT = "1"
            Loop
            sqlConnection1.Close()

            If (procesar = "0") Then
                COT = "Información Guardada Correctamente"
            ElseIf (procesar = "1") Then
                COT = "OK"

                For Each query As Querys In queryToExec
                    If (query.validQuery = False) Then
                        COT = "Todos los campos con * son obligatorios"
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            COT = "Hubo un error al procesar la información."
            sqlConnection1.Close()
        End Try

        Return COT
    End Function

    Private Function dinamicQuery(ByVal Tablas As List(Of jsonfields), ByVal procesar As String) As List(Of Querys)
        Dim querys As List(Of Querys) = New List(Of Querys)
        Dim condiciones As condicionesBLL = New condicionesBLL()

        For Each tabla As jsonfields In Tablas
            Dim nombreTabla As String = tabla.tableName.Replace("ctl00_ctl00_cphCuerpo_cphPantallas_U_", "").Replace("ctl00_ctl00_cphCuerpo_cphPantallas_I_", "")
            Dim LQuery As Querys = New Querys()
            Dim strQuery_insert As StringBuilder = New StringBuilder()
            Dim strQuery_update As StringBuilder = New StringBuilder()
            Dim Lastcampo As fields = tabla.field.Item(tabla.field.Count - 1)
            Dim coma As String = ", "
            Dim evaluate As deniedCauses = New deniedCauses()
            Dim validQuery As Boolean = True
            If (procesar = "1") Then
                evaluate = validBll(tabla, condiciones)
                condiciones = evaluate.condiciones
                validQuery = evaluate.isValid
            End If


            Dim strQueryFields As StringBuilder = New StringBuilder()
            Dim strQueryValues As StringBuilder = New StringBuilder()
            strQuery_insert.Append("INSERT INTO " + nombreTabla + "  ")
            For Each campo As fields In tabla.field
                If (campo.Equals(Lastcampo)) Then
                    coma = ""
                End If
                If (campo.valueField = "on") Then 'AVOID CHECKBOX
                    campo.valueField = "True"
                ElseIf (campo.valueField = "off") Then
                    campo.valueField = "False"
                ElseIf (campo.valueField = "") Then
                    campo.valueField = "NULL"
                ElseIf (campo.valueField.ToUpper() = "ERROR") Then
                    campo.valueField = "NULL"
                End If

                Dim str = replaceNumbers(campo.nameField.Replace("ctl00_ctl00_cphCuerpo_cphPantallas_txt", "").Replace("ctl00_ctl00_cphCuerpo_cphPantallas_ddltxt", ""))
                strQueryFields.Append(str + coma)
                strQueryValues.Append("'" + campo.valueField + "'" + coma)

            Next

            strQuery_insert.Append("(PDK_ID_SECCCERO, " + strQueryFields.ToString())
            strQuery_insert.Append(") VALUES ('" + tabla.Folio + "', " + strQueryValues.ToString() + ")")

            strQuery_update.Append("UPDATE " + nombreTabla + " SET ")
            Dim strQuerySettings As StringBuilder = New StringBuilder()

            coma = ", "

            For Each campo As fields In tabla.field
                If (campo.Equals(Lastcampo)) Then
                    coma = ""
                End If
                If (campo.valueField = "on") Then 'AVOID CHECKBOX
                    campo.valueField = "True"
                ElseIf (campo.valueField = "off") Then
                    campo.valueField = "False"
                ElseIf (campo.valueField = "") Then
                    campo.valueField = "NULL"
                ElseIf (campo.valueField.ToUpper() = "ERROR") Then
                    campo.valueField = "NULL"
                End If

                strQuerySettings.Append(replaceNumbers(campo.nameField.Replace("ctl00_ctl00_cphCuerpo_cphPantallas_txt", "").Replace("ctl00_ctl00_cphCuerpo_cphPantallas_ddltxt", "")) + " = '" + campo.valueField + "'" + coma + " ")
            Next
            strQuery_update.Append(strQuerySettings.ToString())
            strQuery_update.Append("WHERE PDK_ID_SECCCERO = " + tabla.Folio)

            LQuery.query_insert = strQuery_insert.ToString().Replace("'NULL'", "NULL")
            LQuery.query_update = strQuery_update.ToString().Replace("'NULL'", "NULL")
            LQuery.validQuery = validQuery
            LQuery.nameTable = nombreTabla
            querys.Add(LQuery)
        Next

        Return querys
    End Function

    Private Function validBll(ByVal tabla As jsonfields, ByVal condiciones As condicionesBLL) As deniedCauses
        Dim infoTable As deniedCauses = New deniedCauses()
        Dim cause As String = String.Empty
        Dim tableName As String = tabla.tableName.Replace("ctl00_ctl00_cphCuerpo_cphPantallas_U_", "").Replace("ctl00_ctl00_cphCuerpo_cphPantallas_I_", "")
        infoTable.isValid = True
        Dim obligatoryF As Boolean = obligatoryFields(tabla)
        Dim strNombreTablaFront As String = ""


        If (tableName = "PDK_TAB_DATOS_SOLICITANTE") Then
            strNombreTablaFront = ""
            For Each tablaFields As fields In tabla.field
                'infoTable.isValid = False
            Next
        ElseIf (tableName = "PDK_TAB_DATOS_SOLICITANTE") Then
            For Each tablaFields As fields In tabla.field
            Next
        ElseIf (tableName = "PDK_TAB_SOLICITANTE") Then
            For Each tablaFields As fields In tabla.field
            Next
        ElseIf (tableName = "PDK_TAB_DATOS_PERSONALES") Then
            For Each tablaFields As fields In tabla.field
            Next
        ElseIf (tableName = "PDK_TAB_LUGAR_NACIMIENTO") Then
            For Each tablaFields As fields In tabla.field
            Next
        ElseIf (tableName = "PDK_TAB_FIEL") Then
            For Each tablaFields As fields In tabla.field
            Next
        ElseIf (tableName = "PDK_TAB_IDENTIFICACION_FISCAL") Then
            For Each tablaFields As fields In tabla.field
            Next
        ElseIf (tableName = "PDK_TAB_RESIDENTES_EXTRANJEROS") Then
            For Each tablaFields As fields In tabla.field
            Next
        ElseIf (tableName = "PDK_TAB_EMPLEO") Then
            For Each tablaFields As fields In tabla.field
            Next
        ElseIf (tableName = "PDK_TAB_REFERE_PERSONALES") Then
            For Each tablaFields As fields In tabla.field
            Next
        ElseIf (tableName = "PDK_TAB_REFERE_CONOCIDO") Then
            For Each tablaFields As fields In tabla.field
            Next
        ElseIf (tableName = "PDK_TAB_REFERE_FAMILIAR") Then
            For Each tablaFields As fields In tabla.field
            Next
        ElseIf (tableName = "PDK_TAB_PEPS") Then
            For Each tablaFields As fields In tabla.field
            Next
        ElseIf (tableName = "PDK_TAB_CONDUCTOR_RECURRENTE") Then
            For Each tablaFields As fields In tabla.field
            Next
        End If

        If (obligatoryF = False) Then
            infoTable.isValid = False
            strNombreTablaFront = "Los campos con * son obligatorios"
            infoTable.listDeniedCauses.Add(strNombreTablaFront)
        End If

        Return infoTable
    End Function

    Private Function obligatoryFields(ByVal tabla As jsonfields) As Boolean
        Dim obligatoryF As Boolean = True
        Dim str As String = ""
        For Each tablaFields As fields In tabla.field
            Dim valorCampo As String = tablaFields.valueField
            Dim textField As String = tablaFields.textField
            Dim index As Integer = textField.IndexOf("*")
            If (textField = "Tipo de Identificacion *") Then
                If (valorCampo <> "Credencial de Elector") Then
                    str = "-"
                End If
            End If
            If (textField = "Clave de Elector *") Then
                If (str = "-") Then
                    valorCampo = str
                    str = ""
                End If
            End If

            If (textField = "-¿Tiene relación con alguien que es o que ha sido PEP? *") Then
                If valorCampo = "NO" Then
                    Exit For
                End If
            End If
            If ((valorCampo = "") And (index <> -1)) Then
                obligatoryF = False
                Exit For
            End If
        Next
        Return obligatoryF
    End Function



    Private Function replaceNumbers(ByVal toReplace As String) As String
        ' If ((toReplace <> "NOMBRE14") And (toReplace <> "NOMBRE25") And (toReplace <> "NOMBRE161") And (toReplace And "TELEFONO163")) Then 'AVOID CHECKBOX
        If (toReplace = "NOMBRE14") Then
            toReplace = toReplace.Replace("4", "")
        ElseIf (toReplace = "NOMBRE25") Then
            toReplace = toReplace.Replace("5", "")
        ElseIf (toReplace = "NOMBRE161") Then
            toReplace = toReplace.Replace("61", "")
        ElseIf (toReplace = "TELEFONO163") Then
            toReplace = toReplace.Replace("63", "")
        ElseIf (toReplace = "FAKE_1587") Then
            toReplace = toReplace.Replace("587", "")
        ElseIf (toReplace = "FAKE_2588") Then
            toReplace = toReplace.Replace("588", "")
        Else
            toReplace = toReplace.Replace("1", "")
            toReplace = toReplace.Replace("2", "")
            toReplace = toReplace.Replace("3", "")
            toReplace = toReplace.Replace("4", "")
            toReplace = toReplace.Replace("5", "")
            toReplace = toReplace.Replace("6", "")
            toReplace = toReplace.Replace("7", "")
            toReplace = toReplace.Replace("8", "")
            toReplace = toReplace.Replace("9", "")
            toReplace = toReplace.Replace("0", "")
        End If
        Return toReplace
    End Function


    <System.Web.Services.WebMethod()> _
    Public Shared Function nextTarea(ByVal exec As String) As String
        Dim COT As String = ""
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Try

            Dim cmd As New SqlCommand
            Dim reader As SqlDataReader
            cmd.CommandText = "exec_SP"
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@query", "spValNegocio " + exec)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                COT = reader(0)
            Loop

            reader.Close()

            If COT = "Tarea Exitosa" Then
                Dim _cmd As New SqlCommand
                Dim _reader As SqlDataReader
                _cmd.CommandText = "get_Path_Next_Tarea"
                _cmd.CommandType = CommandType.StoredProcedure

                _cmd.Parameters.AddWithValue("@ID_PANTALLA", "7")
                _cmd.Connection = sqlConnection1
                _reader = _cmd.ExecuteReader()

                Dim path As nextPATH = New nextPATH()
                Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

                Do While _reader.Read()
                    path.mensaje = "Tarea Exitosa."
                    path.mostrar = _reader(0).ToString()
                    path.id_pantalla = _reader(1).ToString()
                    path.link = _reader(2).ToString()
                    COT = serializer.Serialize(path)
                Loop
            End If

            sqlConnection1.Close()
        Catch ex As Exception
            COT = "Error al procesar la solicitud."
        End Try
        sqlConnection1.Close()
        Return COT
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function validaCot(ByVal num_cot As String, ByVal folio As String) As String
        Dim resp As respuesta = New respuesta()
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_AgenciaCotizacion_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Parameters.AddWithValue("@NUM_COTIZACION", num_cot)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Dim salida As List(Of String) = New List(Of String)

            Do While reader.Read()
                salida.Add(reader(0)) 'Agencia ProDesk
                salida.Add(reader(1)) 'Agencia ProCotiza
                salida.Add(reader(2)) 'Cotizacion Procotiza
            Loop

            Dim salTamano As Integer = salida.Count()

            If (salTamano > 0) Then
                If (salida(0) <> salida(1)) Then
                    resp.cod = "0"
                    resp.mensaje = "La agencia de la cotización no corresponde"
                ElseIf (salida(2) = "") Then
                    resp.cod = "0"
                    resp.mensaje = "La cotizacion no existe"
                Else
                    resp.cod = "1"
                    resp.mensaje = "OK"
                End If
            Else
                resp.cod = "0"
                resp.mensaje = "La cotizacion no existe"
            End If
        Catch ex As Exception
            resp.cod = "0"
            resp.mensaje = "Error de conexion: " + ex.ToString()
        End Try

        sqlConnection1.Close()
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Respuesta As String = serializer.Serialize(resp)
        Return json_Respuesta
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function limiteUpdate(ByVal folio As String, ByVal tarea As String) As Integer
        Dim limite As Integer = 0
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_LIMITE_UPDATES_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SOLICITUD", folio)
            cmd.Parameters.AddWithValue("@PDK_ID_TAREAS", tarea)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                limite = Integer.Parse(reader(0))
            Loop

        Catch ex As Exception
            limite = 0
        End Try

        sqlConnection1.Close()
        Return limite
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function InfoCI(ByVal folio As String) As String
        Dim msg_CI As List(Of String) = New List(Of String)
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_InfoCI_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                msg_CI.Add("OK")
                msg_CI.Add(reader(0).ToString())
                msg_CI.Add(reader(1).ToString())
            Loop

        Catch ex As Exception
            msg_CI = New List(Of String)
            msg_CI.Add("Error de conexion: " + ex.Message.ToString())
        End Try

        sqlConnection1.Close()
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Respuesta As String = serializer.Serialize(msg_CI)
        Return json_Respuesta
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function validCI(ByVal NContrato As String, ByVal Nombre As String) As String
        Dim msg_CI As respuesta = New respuesta()
        msg_CI.cod = "0"
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_Detail_Contract_CI_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@CTO_FL_CVE", NContrato)
            cmd.Parameters.AddWithValue("@NOMBRE", Nombre)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                msg_CI.mensaje = reader(0).ToString()
            Loop

        Catch ex As Exception
            msg_CI.mensaje = "Error de conexion: " + ex.Message.ToString()
        End Try

        sqlConnection1.Close()
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Respuesta As String = serializer.Serialize(msg_CI)
        Return json_Respuesta
    End Function

    Public Class ciudades
        Public id As Integer
        Public c As String
    End Class

    Public Class Querys
        Public query_insert As String
        Public query_update As String
        Public validQuery As Boolean
        Public nameTable As String
        Public evaluateBLL As List(Of String) = New List(Of String)
    End Class

    Public Class jsonTablas
        Public Tablas As List(Of jsonfields)
    End Class

    Public Class jsonfields
        Public tableName As String
        Public Folio As String
        Public Finalizar As String
        Public field As List(Of fields)
    End Class

    Public Class fields
        Public nameField As String
        Public valueField As String
        Public textField As String
        Public tipo As String
    End Class

    Public Class deniedCauses
        Public isValid As Boolean
        Public listDeniedCauses As List(Of String) = New List(Of String)
        Public condiciones As condicionesBLL = New condicionesBLL()
    End Class

    Public Class condicionesBLL
        Dim mexican As Boolean = False
        Dim CompraInteligent As Boolean = False
        Dim HondaAcura As Boolean = False
        Dim rentada As Boolean = False
        Dim ife_ine As Boolean = False
        Dim relacionPEP As Boolean = False
        Dim casado As Boolean = False
    End Class

    Public Class respuesta
        Public cod As String
        Public mensaje As String
    End Class

    Public Class nextPATH
        Public mensaje As String
        Public mostrar As String
        Public id_pantalla As String
        Public link As String
    End Class

    <System.Web.Services.WebMethod()> _
    Public Shared Function validCIWS(ByVal NContrato As String, ByVal Etapa As Integer, ByVal Nombre As String) As String
        Dim msg_CI As respuesta = New respuesta()
        msg_CI.mensaje = ""
        msg_CI.cod = "0"

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("getDocumentDataOfCar").ToString().Replace("Cambiar_NUM_CREDIT", NContrato)
        Dim jsonResult As String = rest.ConnectionGet(userID, iv_ticket1, String.Empty)

        If ((rest.IsError = False) And (jsonResult <> "{}")) Then
            Dim DocumentDataOfCar As ProdeskNet.WCF.DocumentDataOfCar = serializer.Deserialize(Of ProdeskNet.WCF.DocumentDataOfCar)(jsonResult)

            Dim contractName_1 As String = System.Configuration.ConfigurationManager.AppSettings("contractName_1").ToString()
            Dim contractName_2 As String = System.Configuration.ConfigurationManager.AppSettings("contractName_2").ToString()
            Dim contractName_WS As String = DocumentDataOfCar.loan.contract.status.name

            If (Etapa = 0) Then
                If (contractName_WS = contractName_2) Then
                    msg_CI.mensaje = "OK"
                ElseIf (contractName_WS = contractName_1) Then
                    Dim cls As aspx_Blanco = New aspx_Blanco()
                    Dim ultimoRecibo As Boolean = cls.getLastRecibo(NContrato, Nombre)

                    Dim impagos As Integer = Int32.Parse(DocumentDataOfCar.iLoanDetail.loanCar.measure.totalNonPayment.amount)
                    Dim fecha As String = DocumentDataOfCar.loan.installments.installmentDate
                    Dim fecha_ As DateTime = DateTime.Parse(fecha)
                    Dim fechaToday As DateTime = DateTime.Now

                    If ultimoRecibo Then
                        If (impagos = 0) Then
                            msg_CI.mensaje = "OK"

                            If fecha_.Date < fechaToday.Date Then
                                msg_CI.mensaje = "La fecha del contrato es menor a la fecha actual"
                            End If
                        Else
                            msg_CI.mensaje = "El contrato tiene impagos"
                        End If
                    Else
                        If (impagos = 0) Then
                            msg_CI.mensaje = "OK"
                        Else
                            msg_CI.mensaje = "El contrato tiene impagos"
                        End If
                    End If
                Else
                    msg_CI.mensaje = "El contrato se encuentra en estado: " + contractName_WS
                End If
            Else
                If (contractName_WS = contractName_2) Then
                    msg_CI.mensaje = "OK"
                Else
                    msg_CI.mensaje = "El contrato debe estar en estado " + contractName_2
                End If
            End If
        Else
            msg_CI.mensaje = "Error Al consultar WS, favor de intentarlo nuevamente"
            msg_CI.cod = "0"
        End If

        Dim json_Respuesta As String = serializer.Serialize(msg_CI)
        Return json_Respuesta
    End Function

    Private Function getLastRecibo(ByVal NContrato As String, ByVal Nombre As String) As Boolean
        Dim lastRecibo As Boolean = False

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_Detail_Contract_CI_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@CTO_FL_CVE", NContrato)
            cmd.Parameters.AddWithValue("@NOMBRE", Nombre)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                If reader(0).ToString() = "OK" Then
                    lastRecibo = reader(1).ToString()
                End If
            Loop

        Catch ex As Exception
            lastRecibo = False
        End Try

        sqlConnection1.Close()

        Return lastRecibo
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function ParamAntiguedadLaboral(ByVal param As String, ByVal opcion As String) As String
        Dim msg_Param As List(Of String) = New List(Of String)
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "spCatalogos"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@opcion", opcion)
            cmd.Parameters.AddWithValue("@parametro", param)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                msg_Param.Add("OK")
                msg_Param.Add(reader(2).ToString())
            Loop

        Catch ex As Exception
            msg_Param = New List(Of String)
            msg_Param.Add("Error de conexion: " + ex.Message.ToString())
        End Try

        sqlConnection1.Close()
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim json_Respuesta As String = serializer.Serialize(msg_Param)
        Return json_Respuesta
    End Function
End Class
