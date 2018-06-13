'BUG-PD-407  JBEJAR:17/04/2018 REFACTORIZACION DE PRECALIFICACION SE  SE SEPARA DE BLANCO ASPX.

Imports ProdeskNet.Seguridad
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Web.UI.WebControls
Imports ProdeskNet.Buro
Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.WCF

Partial Class aspx_Precalificacion
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


                    '/*Query nombre de pantalla*/
                    dbData = clsPantallaObjeto.ObtenerControles(hdnIdRegistro.Value, 1)
                    If dbData.Tables(0).Rows.Count > 0 AndAlso dbData.Tables.Count > 0 Then
                        lbltitulo.Text = dbData.Tables(0).Rows(0).Item("PDK_PANT_NOMBRE")
                        strPantalla = dbData.Tables(0).Rows(0).Item("PDK_PANT_NOMBRE")
                        hdnPantalla.Value = strPantalla
                        'pantallaBTSP.InnerText = strPantalla
                    End If

                    '/*Query Obtención de tablas para secciones*/
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
                                    mitable.Caption = "<div class=bold>" + "<div class=inner>" + dbDabase.Tables(0).Rows(i).Item("PDK_SEC_NOMBRE") + "</div>"

                                End If
                            Else
                                Dim strNameSection As String = dbDabase.Tables(0).Rows(i).Item("PDK_SEC_NOMBRE")
                                If dbDabase.Tables(0).Rows(i).Item("PDK_SEC_NOMBRE") = "DATOS PERSONALES NO MOSTRAR" Then
                                    strNameSection = "Buro de Crédito"
                                End If
                                mitable.Caption = "<div class=bold>" + "<div class=inner>" + strNameSection + "</div>" + "</div>"

                            End If

                            mitable.CssClass = "boldtable"
                            mitable.HorizontalAlign = HorizontalAlign.Center
                            If intConta = 0 Then
                                If dbDabase.Tables(0).Rows(i).Item("VALIDA") = "U" Then
                                    intConta = 1
                                End If
                            End If
                            '/*Se obtine el nombre de la tabla de la seccion a pintar*/
                            dbData = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 2, dbDabase.Tables(0).Rows(i).Item("PDK_ID_SECCION"))
                            If dbData.Tables.Count > 0 AndAlso dbData.Tables(0).Rows.Count > 0 Then
                                strNombre = dbData.Tables(0).Rows(0).Item("PDK_SEC_NOMBRE_TABLA").ToString.Trim
                            End If
                            dbData = Nothing
                            '/*Se obtienen los campos para pintar los formularios*/
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
                                        Dim strLabel As String = dbData.Tables(0).Rows(rowCtr).Item("MOSTRAR").ToString()

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
                                                    dropList.DataSource = dbdataset.Tables(0)
                                                    dropList.DataValueField = "CPO_FL_CP"
                                                    dropList.DataTextField = "CPO_DS_COLONIA"
                                                    dropList.DataBind()
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
                                                    dropList.DataSource = dbdataset.Tables(0)
                                                    dropList.DataValueField = "PDK_ID_SCORING"
                                                    dropList.DataTextField = "PDK_SCORING_VALOR1"
                                                    dropList.DataBind()
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
                                                radio.DataSource = dbdataset1.Tables(0)
                                                radio.DataValueField = "PDK_ID_SCORING"
                                                radio.DataTextField = "PDK_SCORING_VALOR1"
                                                radio.DataBind()
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
                                            ElseIf dbData.Tables(0).Rows(rowCtr).Item("CONVE") = "ALFANUMERICO" Then
                                                nvoText.SkinID = "txtAlfanumerico"
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
                                                            nvoText.Enabled = True
                                                        End If

                                                        If dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtIMPORTE_RENTA436" Then
                                                            hdnValiRenta.Value = ""
                                                        ElseIf dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtIMPORTE_RENTA437" Then
                                                            hdnValiRenta1.Value = ""
                                                        End If


                                                    End If
                                                Else
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
                                                nvoText.Enabled = "false"
                                            End If
                                            Dim TAMAÑO As Integer = dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_TAMANO")
                                            nvoText.MaxLength = TAMAÑO
                                            arregloTextBoxs(rowCtr) = nvoText
                                            tCell.Text = "Row " & rowCtr & ", Cell " & cellCtr
                                            tCell.Controls.Add(nvoText)

                                            If dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "FUNCION_PEPS_OTRA_" Then

                                                tRowTmp.Cells.Add(tCell)
                                                mitable.Rows.Add(tRowTmp)
                                                cellCtr = 0
                                            ElseIf (dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE") = "FUNCION_REL_PEPS_") Then
                                                tRowTmp.Cells.Add(tCell)
                                                mitable.Rows.Add(tRowTmp)
                                                cellCtr = 0
                                            Else
                                                tRow.Cells.Add(tCell)
                                            End If

                                        End If
                                        If cellCtr <> 4 Then
                                            rowCtr = rowCtr + 1
                                        End If

                                        If rowCtr = rowCnt Then
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
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "Notificación", "alert('" & ex.Message & "');", True)
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
            Dim cls As aspx_Precalificacion = New aspx_Precalificacion()
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
            Dim cls As aspx_Precalificacion = New aspx_Precalificacion()
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
            Dim cls As aspx_Precalificacion = New aspx_Precalificacion()
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
            Dim cls As aspx_Precalificacion = New aspx_Precalificacion()
            Dim queryToExec As List(Of Querys) = cls.dinamicQuery(Tablas, procesar)

            For Each query As Querys In queryToExec
                Dim cmd As New SqlCommand
                Dim reader As SqlDataReader
                cmd.CommandText = "exec_SP"
                cmd.CommandType = CommandType.StoredProcedure
                If query.validQuery Then
                    cmd.Parameters.AddWithValue("@query", query.query.Replace("'NULL'", "NULL"))
                    cmd.Connection = sqlConnection1
                    sqlConnection1.Open()
                    reader = cmd.ExecuteReader()

                    Do While reader.Read()
                        COT = "1"
                    Loop
                    sqlConnection1.Close()
                End If
            Next

            If (procesar = "0") Then
                COT = "Información Guardada Correctamente"
            ElseIf (procesar = "1") Then
                Dim continuar As String = "SI"
                For Each query As Querys In queryToExec
                    If (query.validQuery = False) Then
                        continuar = "NO"
                        Exit For
                    End If
                Next
                If (continuar = "SI") Then
                    For Each query As Querys In queryToExec
                        Dim cmd As New SqlCommand
                        Dim reader As SqlDataReader
                        cmd.CommandText = "exec_SP"
                        cmd.CommandType = CommandType.StoredProcedure
                        If query.validQuery Then
                            cmd.Parameters.AddWithValue("@query", query.query.Replace("'NULL'", "NULL"))
                            cmd.Connection = sqlConnection1
                            sqlConnection1.Open()
                            reader = cmd.ExecuteReader()

                            Do While reader.Read()
                                COT = "1"
                            Loop
                            sqlConnection1.Close()
                        End If
                    Next
                    COT = "OK"
                Else
                    COT = "Todos los campos con * son obligatorios"
                End If
            End If
        Catch ex As Exception
            COT = "Hubo un error al procesar la información."
        End Try
        sqlConnection1.Close()
        Return COT
    End Function

    Private Function dinamicQuery(ByVal Tablas As List(Of jsonfields), ByVal procesar As String) As List(Of Querys)
        Dim querys As List(Of Querys) = New List(Of Querys)
        Dim i As Integer = 0
        Dim condiciones As condicionesBLL = New condicionesBLL()

        For Each tabla As jsonfields In Tablas
            Dim typeQuery As String = (tabla.tableName.Replace("ctl00_ctl00_cphCuerpo_cphPantallas_", "")).Substring(0, 1)
            Dim nombreTabla As String = tabla.tableName.Replace("ctl00_ctl00_cphCuerpo_cphPantallas_U_", "").Replace("ctl00_ctl00_cphCuerpo_cphPantallas_I_", "")
            Dim LQuery As Querys = New Querys()
            Dim strQuery As StringBuilder = New StringBuilder()
            Dim Lastcampo As fields = tabla.field.Item(tabla.field.Count - 1)
            Dim coma As String = ", "
            Dim evaluate As deniedCauses = New deniedCauses()
            Dim validQuery As Boolean = True
            If (procesar = "1") Then
                evaluate = validBll(tabla, condiciones)
                condiciones = evaluate.condiciones
                validQuery = evaluate.isValid
            End If

            If (typeQuery = "I") Then
                Dim strQueryFields As StringBuilder = New StringBuilder()
                Dim strQueryValues As StringBuilder = New StringBuilder()
                strQuery.Append("INSERT INTO " + nombreTabla + "  ")
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
                    'strQueryFields.Append(campo.nameField + coma)
                    strQueryFields.Append(str + coma)
                    strQueryValues.Append("'" + campo.valueField + "'" + coma)

                Next
                'strQuery.Append("(PDK_ID_SECCCERO, " + replaceNumbers(strQueryFields.ToString().Replace("ctl00_ctl00_cphCuerpo_cphPantallas_txt", "").Replace("ctl00_ctl00_cphCuerpo_cphPantallas_ddltxt", "")))
                strQuery.Append("(PDK_ID_SECCCERO, " + strQueryFields.ToString())
                strQuery.Append(") VALUES ('" + tabla.Folio + "', " + strQueryValues.ToString() + ")")
            ElseIf (typeQuery = "U") Then
                strQuery.Append("UPDATE " + nombreTabla + " SET ")
                Dim strQuerySettings As StringBuilder = New StringBuilder()

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

                    'If (campo.valueField <> "") Then
                    strQuerySettings.Append(replaceNumbers(campo.nameField.Replace("ctl00_ctl00_cphCuerpo_cphPantallas_txt", "").Replace("ctl00_ctl00_cphCuerpo_cphPantallas_ddltxt", "")) + " = '" + campo.valueField + "'" + coma + " ")
                    'End If
                Next
                strQuery.Append(strQuerySettings.ToString())
                strQuery.Append("WHERE PDK_ID_SECCCERO = " + tabla.Folio)
            End If


            LQuery.typeQuery = typeQuery.ToString()
            LQuery.query = strQuery.ToString()
            LQuery.validQuery = validQuery
            'LQuery.evaluateBLL = evaluate.listDeniedCausess
            querys.Add(LQuery)
            i = i + 1
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
        Public query As String
        Public typeQuery As String
        Public validQuery As Boolean
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
                    Dim cls As aspx_Precalificacion = New aspx_Precalificacion()
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
End Class
