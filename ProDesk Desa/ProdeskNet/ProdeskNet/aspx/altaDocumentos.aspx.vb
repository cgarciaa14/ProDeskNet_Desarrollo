Imports ProdeskNet.Catalogos
Imports System.Data

Public Class altaDocumentos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"
        If Not IsPostBack Then

            LlenarCombo(1)
            llenaComboProDoc()
        End If

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

    Private Sub llenaComboProDoc()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim dsProDoc As New DataSet
        dsProDoc = BD.EjecutarQuery("select * from PDK_CAT_DOCPRODOC")    
        ddlIDProdDesk.DataSource = dsProDoc
        ddlIDProdDesk.DataTextField = "PDK_DESC_PRODOC"
        ddlIDProdDesk.DataValueField = "PDK_ID_PRODDOC"
        ddlIDProdDesk.DataBind()
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaDocumentos.aspx")
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
                If Me.lblIdRelacion.Text.Length > 0 Then objDocumentos.PDK_ID_REL_DOC_PER_JUR = Val(Me.lblIdRelacion.Text.Trim)
                If Me.lblIdDocumento.Text.Length > 0 Then objDocumentos.PDK_ID_DOCUMENTOS = Val(Me.lblIdDocumento.Text.Trim)

                objDocumentos.PDK_ID_PER_JURIDICA = Val(ddlPersonalidadJuridica.SelectedValue)
                objDocumentos.PDK_DOC_NOMBRE = Me.txtNombreDocumento.Text.Trim.ToUpper
                objDocumentos.PDK_DOC_MODIF = Format(Now(), "yyyy-MM-dd")
                objDocumentos.PDK_DOC_ACTIVO = IIf(Me.chkActivo.Checked = True, 2, 3)
                objDocumentos.PDK_CLAVE_USUARIO = Session("IdUsua")
                objDocumentos.PDK_CAT_DOCPRODOC = ddlIDProdDesk.SelectedValue

                objDocumentos.Guarda()
                lblIdRelacion.Text = objDocumentos.PDK_ID_REL_DOC_PER_JUR
                lblIdDocumento.Text = objDocumentos.PDK_ID_DOCUMENTOS

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