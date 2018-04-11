'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.
Imports ProdeskNet.Catalogos
Imports System.Data

Public Class manejaDocumentos
    Inherits System.Web.UI.Page

    Public intDocumento As Integer
    Public intDocumentoRel As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Me.Title = "PRODESKNET 3.0"
        If Not IsPostBack Then
            Dim BD As New ProdeskNet.BD.clsManejaBD
            Dim dsidpro As New DataSet
            LlenarCombo(1)
            llenaComboProDoc()
            intDocumento = Request.QueryString("IdDoc")
            MuestraDatos(intDocumento)
            dsidpro = BD.EjecutarQuery("select PDK_ID_PRODDOC from PDK_CAT_DOCUMENTOS where PDK_ID_DOCUMENTOS = " & lblID.Text)
            If Not IsDBNull(dsidpro.Tables(0).Rows(0)(0)) = True Then
                ddlIDProdDesk.SelectedValue = dsidpro.Tables(0).Rows(0)(0)
            End If
        End If

    End Sub

    Private Sub llenaComboProDoc()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim dsProDoc As New DataSet
        dsProDoc = BD.EjecutarQuery("select * from PDK_CAT_DOCPRODOC")
        ddlIDProdDesk.DataSource = dsProDoc
        ddlIDProdDesk.DataTextField = "PDK_DESC_PRODOC"
        ddlIDProdDesk.DataValueField = "PDK_ID_PRODDOC"
        ddlIDProdDesk.DataBind()
    End Sub

    Private Sub MuestraDatos(ByVal intDocumento As Integer)

        Dim intPersonalidadJuridica As Integer = 0
        Dim strDocumento As String = String.Empty
        Dim intActivo As Integer = 0
        Dim dsDataset As New DataSet

        Try
            dsDataset = clsDocumentos.ObtenTodos(intDocumento:=intDocumento)
            If dsDataset.Tables.Count > 0 Then
                intPersonalidadJuridica = dsDataset.Tables(0).Rows(0).Item("PDK_ID_PER_JURIDICA").ToString
                ddlPersonalidadJuridica.SelectedIndex = ddlPersonalidadJuridica.Items.IndexOf(ddlPersonalidadJuridica.Items.FindByValue(dsDataset.Tables(0).Rows(0).Item("PDK_ID_PER_JURIDICA").ToString))
                strDocumento = dsDataset.Tables(0).Rows(0).Item("PDK_DOC_NOMBRE").ToString
                Me.txtNombreDocumento.Text = strDocumento
                intActivo = dsDataset.Tables(0).Rows(0).Item("PDK_DOC_ACTIVO").ToString
                Me.chkActivo.Checked = IIf(intActivo = 2, True, False)
                lblID.Text = dsDataset.Tables(0).Rows(0).Item("PDK_ID_DOCUMENTOS").ToString
                intDocumentoRel = dsDataset.Tables(0).Rows(0).Item("PDK_ID_REL_DOC_PER_JUR").ToString

                Me.hdnIdRegistro.Value = intDocumento
                Me.hdnIdRegistroRel.Value = intDocumentoRel
            Else

                Master.MensajeError("No se encontraron registros")
            End If
        Catch ex As Exception
            Master.MensajeError("No se encontraron registros")
        End Try
    End Sub
    Private Sub LlenarCombo(ByVal intCombo As Integer)

        Dim dsDataset As New DataSet

        Try
            dsDataset = clsPersonalidadJuridica.ObtenTodos
            ddlPersonalidadJuridica.DataSource = dsDataset
            ddlPersonalidadJuridica.DataTextField = "PDK_PER_NOMBRE"
            ddlPersonalidadJuridica.DataValueField = "PDK_ID_PER_JURIDICA"
            ddlPersonalidadJuridica.DataBind()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim path As String = "./consultaDocumentos.aspx"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtNombreDocumento.Text.Trim.Length = 0 Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                Dim objDocumentos As New clsDocumentos(0)

                ''guardamos la información
                'If Me.lblIdRelacion.Text.Length > 0 Then objDocumentos.PDK_ID_REL_DOC_PER_JUR = Val(Me.lblIdRelacion.Text.Trim)
                'If Me.lblIdDocumento.Text.Length > 0 Then objDocumentos.PDK_ID_DOCUMENTOS = Val(Me.lblIdDocumento.Text.Trim)

                objDocumentos.PDK_ID_PER_JURIDICA = Val(ddlPersonalidadJuridica.SelectedValue)
                objDocumentos.PDK_DOC_NOMBRE = Me.txtNombreDocumento.Text.Trim.ToUpper
                objDocumentos.PDK_DOC_MODIF = Format(Now(), "yyyy-MM-dd")
                objDocumentos.PDK_DOC_ACTIVO = IIf(Me.chkActivo.Checked = True, 2, 3)
                objDocumentos.PDK_CLAVE_USUARIO = Session("IdUsua")
                objDocumentos.PDK_ID_DOCUMENTOS = CInt(Me.hdnIdRegistro.Value)
                objDocumentos.PDK_ID_REL_DOC_PER_JUR = CInt(Me.hdnIdRegistroRel.Value)
                objDocumentos.PDK_CAT_DOCPRODOC = Me.ddlIDProdDesk.SelectedValue


                objDocumentos.Guarda()
                'lblIdRelacion.Text = objDocumentos.PDK_ID_REL_DOC_PER_JUR
                'lblIdDocumento.Text = objDocumentos.PDK_ID_DOCUMENTOS

            Else

                Master.MensajeError("Todos los campos marcados con * son obligatorios")
                Exit Sub
            End If

            Master.MensajeError("Información almacenada exitosamente")

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

End Class