'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.  
Imports ProdeskNet.Catalogos

Public Class manejaRechazos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("idRech")) > 0 Then
                hdnIdRegistro.Value = Request("idRech")
                CargaInfo()
            End If

            Dim objRechazos As New clsRechazos
            objRechazos.returnRechazo()            
            ddlcondicion.DataSource = objRechazos.CondicionRechazo.Tables(0)
            ddlcondicion.DataValueField = "id"
            ddlcondicion.DataTextField = "value"
            ddlcondicion.DataBind()

        End If

    End Sub


    Private Sub CargaInfo()
        Try
            Dim intClave As Integer = Val(hdnIdRegistro.Value)

            Dim objRechazos As New clsRechazos(intClave)

            If objRechazos.PDK_ID_CAT_RECHAZOS > 0 Then

                lblIdRechazo.Text = objRechazos.PDK_ID_CAT_RECHAZOS
                txtDescripcion.Text = objRechazos.PDK_REC_NOMBRE
                chkActivo.Checked = IIf(objRechazos.PDK_REC_ACTIVO = 2, True, False)

            Else
                Master.MensajeError("No se localizó información del catálogo de rechazos")
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If Trim(txtDescripcion.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim path As String = "./consultaRechazos.aspx"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intCve As Integer = Val(hdnIdRegistro.Value)
                Dim objRechazos As New clsRechazos(intCve)

                'guardamos la información
                objRechazos.PDK_ID_CAT_RECHAZOS = intCve
                objRechazos.PDK_REC_NOMBRE = txtDescripcion.Text.Trim.ToUpper
                objRechazos.PDK_REC_MODIF = Format(Now(), "yyyy-MM-dd")
                objRechazos.PDK_CLAVE_USUARIO = Session("IdUsua")
                objRechazos.PDK_REC_ACTIVO = IIf(chkActivo.Checked = True, 2, 3)
                objRechazos.rechcond = ddlcondicion.SelectedValue

                objRechazos.Guarda()


                Master.MensajeError("La información se guardo con éxito")

                hdnIdRegistro.Value = objRechazos.PDK_ID_CAT_RECHAZOS
                CargaInfo()

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub


End Class