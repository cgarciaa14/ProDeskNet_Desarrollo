Imports ProdeskNet.Seguridad
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Web.UI.WebControls
Imports ProdeskNet.Buro
Imports System.Data
Imports System.Data.SqlClient

' YAM-P-208  egonzalez 11/08/2015 Se agregó implementó un tipo de dato existente 'txtCustMayGde' para su validación en los formularios
' YAM-P-208  egonzalez 13/08/2015 Se Cambiaron los títulos para diferenciar entre 'Solicitante' y 'Coacreditado' en la sección de 'Actualizar solicitud'
' YAM-P-208  egonzalez 03/09/2015 Se implementó el parámetro "OcultaSoli" para saber si se debe ocultar o no los datos de un solicitante.

Partial Class aspx_VerificaDatSol
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
                Dim miTabla As New Table
                Dim rowCnt As Integer
                Dim rowCtr As Integer
                Dim cellCtr As Integer
                Dim cellCnt As Integer
                Dim strNombre As String = ""
                Dim intContador As Integer = 0



                If Session("Regresar") Is Nothing Then
                    Session("Regresar") = Request.UrlReferrer.LocalPath
                    hdRutaEntrada.Value = Request.UrlReferrer.LocalPath
                End If
                hdRutaEntrada.Value = Session("Regresar")

                If Val(Request("idPantalla")) > 0 Then


                    hdnIdRegistro.Value = Request("idPantalla")
                    hdnUsuario.Value = Session("IdUsua")
                    hdnFolio.Value = Request("IdFolio")
                    hdnOcultaSoli.Value = Request("OcultaSoli")

                    Try
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
                        If dbData.Tables(0).Rows(0).Item("PDK_PANT_NOMBRE") = "SOLICITUD SOLICITANTE" Then
                            lbltitulo.Text = "Solicitante"
                        ElseIf dbData.Tables(0).Rows(0).Item("PDK_PANT_NOMBRE") = "SOLICITUD" Then
                            lbltitulo.Text = "Coacreditado"
                        Else
                            lbltitulo.Text = dbData.Tables(0).Rows(0).Item("PDK_PANT_NOMBRE")
                        End If
                        strPantalla = dbData.Tables(0).Rows(0).Item("PDK_PANT_NOMBRE")
                        hdnPantalla.Value = strPantalla
                    End If

                    dbDabase = clsPantallaObjeto.ObtenerDatosControl(hdnIdRegistro.Value, 4, 0, hdnFolio.Value)
                    If dbDabase.Tables.Count > 0 AndAlso dbDabase.Tables(0).Rows.Count > 0 Then
                        'Dim mitable As New Table
                        For i = 0 To dbDabase.Tables(0).Rows.Count - 1
                            Dim mitable As New Table
                            mitable.ID = dbDabase.Tables(0).Rows(i).Item("VALIDA") + "_" + dbDabase.Tables(0).Rows(i).Item("PDK_SEC_NOMBRE_TABLA")
                            mitable.Caption = "<font size=3>" + dbDabase.Tables(0).Rows(i).Item("PDK_SEC_NOMBRE") + "</font>"
                            mitable.CaptionAlign = TableCaptionAlign.Top
                            mitable.CssClass = "bold"
                            mitable.Font.Bold = True
                            'mitable.BorderStyle = BorderStyle.Ridge
                            'mitable.BorderColor = Drawing.Color.DarkGray
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
                                For cellCtr = 1 To cellCnt
                                    Dim tCell As New TableCell()
                                    'creamos(Label)
                                    If cellCtr = 1 Or cellCtr = 3 Then
                                        Dim lbl As New Label
                                        lbl.ID = "lbl" + dbData.Tables(0).Rows(rowCtr).Item("PDK_ID_SECCION_DATO").ToString + "|" + dbData.Tables(0).Rows(rowCtr).Item("PDK_REL_PANT_OBJ_NOMBRE")
                                        lbl.Text = dbData.Tables(0).Rows(rowCtr).Item("MOSTRAR")
                                        lbl.SkinID = "lblGeneral"
                                        lbl.Width = "230"
                                        arregloLabel(rowCtr) = lbl
                                        tCell.Text = "Row " & rowCtr & ", Cell " & cellCtr
                                        tCell.Controls.Add(lbl)
                                        tRow.Cells.Add(tCell)

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
                                            dropList.SkinID = "cmbGeneral"
                                            dropList.CssClass = "Text"
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
                                                dropList.Enabled = True
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
                                            txtgrande.Attributes.Add("class", "Text")
                                            txtgrande.Rows = "3"
                                            txtgrande.Cols = "1"
                                            If dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtOBSERVACION428" Then
                                                txtgrande.Attributes.Add("style", "width:500px")
                                                txtgrande.Disabled = True
                                            Else
                                                txtgrande.Attributes.Add("style", "width:100%")

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
                                                        If dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtNUM_SOLICITUD418" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtSTATUS420" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtNUMERO_CLIENTE434" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtSTATUS_DOC446" Or dbData.Tables(0).Rows(rowCtr).Item("COLUMNA") = "txtSTATUS_CREDITO445" Then
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
                                                    Dim clspo As New clsPantallaObjeto
                                                    Dim resp As Boolean = clspo.fnMuestraValidaDatos(hdnUsuario.Value)
                                                    'If hdnIdRegistro.Value = 6 And resp = True Then         'Para la pantalla de Solicitud se habilitan todos los campos
                                                    '    nvoText.Enabled = "True"
                                                    'Else
                                                    '    nvoText.Enabled = "false"
                                                    'End If


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
                                                    nvoText.Text = 0
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
                                            tRow.Cells.Add(tCell)


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

                    'If hdnPantalla.Value = "SOLICITUD" Then
                    '    btnImprimir.Attributes.Add("OnClick", "JavaScript:CallWindow('ImprimirCreditoSolicitud.aspx?idPantalla=" & hdnIdRegistro.Value & "&IdFolio=" & hdnFolio.Value & "&CVE=0" & "');")
                    'ElseIf hdnPantalla.Value = "SOLICITUD SOLICITANTE" Then
                    '    btnImprimir.Attributes.Add("OnClick", "JavaScript:CallWindow('ImprimirCreditoSolicitud.aspx?idPantalla=" & hdnIdRegistro.Value & "&IdFolio=" & hdnFolio.Value & "&CVE=2" & "');")
                    'ElseIf hdnPantalla.Value = "DICTAMEN PRECALIFICACION" Then
                    '    btnImprimir.Attributes.Add("OnClick", "JavaScript:CallWindow('ImprimirCreditoSolicitud.aspx?idPantalla=" & hdnIdRegistro.Value & "&IdFolio=" & hdnFolio.Value & "&CVE=3" & "');")
                    'ElseIf hdnPantalla.Value = "PRECALIFICACION PERSONAS MORALES" Then
                    '    btnImprimir.Attributes.Add("OnClick", "JavaScript:CallWindow('ImprimirCreditoSolicitud.aspx?idPantalla=" & hdnIdRegistro.Value & "&IdFolio=" & hdnFolio.Value & "&CVE=4" & "');")

                    'Else
                    '    btnImprimir.Attributes.Add("OnClick", "JavaScript:CallWindow('ImprimirSolicitud.aspx?idPantalla=" & hdnIdRegistro.Value & "&IdFolio=" & hdnFolio.Value & "&IdUsu=" & hdnUsuario.Value & "&CVE=0" & "');")

                    'End If

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

End Class
