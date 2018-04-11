'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.  
Imports ProdeskNet.Catalogos
Public Class manejaTipoObjeto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("idTipObj")) > 0 Then
                hdnIdRegistro.Value = Request("idTipObj")
                CargaInfo()
            End If
        End If
    End Sub
    Private Sub CargaInfo()
        Try
            Dim intClave As Integer = Val(hdnIdRegistro.Value)
            Dim objTipoObj As New clsTipoObjeto(intClave)

            If objTipoObj.PDK_ID_TIPO_OBJETO > 0 Then

                lblID.Text = objTipoObj.PDK_ID_TIPO_OBJETO
                txtNombre.Text = objTipoObj.PDK_TIP_OBJ_NOMBRE
                txtNomberCod.Text = objTipoObj.PDK_TIP_OBJ_NOMBRE_COD
            Else
                Master.MensajeError("No se localizó información del Tipo de Objeto")
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If Trim(txtNombre.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intCve As Integer = Val(hdnIdRegistro.Value)
                Dim objTipoObj As New clsTipoObjeto(intCve)
                Dim intOpc As Integer = 1

              
                'guardamos la 
                objTipoObj.PDK_TIP_OBJ_NOMBRE = txtNombre.Text.Trim.ToUpper
                objTipoObj.PDK_TIP_OBJ_NOMBRE_COD = txtNomberCod.Text.Trim.ToUpper


                objTipoObj.ActualizaRegistro()


                Master.MensajeError("La información se guardo con éxito")
                hdnIdRegistro.Value = objTipoObj.PDK_ID_TIPO_OBJETO
                CargaInfo()

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim path As String = "./consultaTipoObjeto.aspx"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
    End Sub

End Class