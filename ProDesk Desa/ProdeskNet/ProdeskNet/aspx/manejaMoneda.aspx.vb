'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.
Imports ProdeskNet.Catalogos

Public Class manejaMoneda
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("idMon")) > 0 Then
                hdnIdRegistro.Value = Request("idMon")
                CargaInfo()
            End If
        End If

    End Sub


    Private Sub CargaInfo()
        Try
            Dim intClave As Integer = Val(hdnIdRegistro.Value)

            Dim objMoneda As New clsMoneda(intClave)

            If objMoneda.PDK_ID_MONEDA > 0 Then

                lblID.Text = objMoneda.PDK_ID_MONEDA
                txtNombreMoneda.Text = objMoneda.PDK_MON_NOMBRE
                txtTipoCambio.Text = objMoneda.PDK_MON_TIPO_CAMBIO
                chkActivo.Checked = IIf(objMoneda.PDK_MON_ACTIVO = 2, True, False)


            Else
                Master.MensajeError("No se localizó información para el cliente")
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub

    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If Trim(txtNombreMoneda.Text) = "" Then Exit Function
        If Trim(txtTipoCambio.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim path As String = "./consultaMoneda.aspx"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intCve As Integer = Val(hdnIdRegistro.Value)
                Dim objMoneda As New clsMoneda(intCve)
                Dim intOpc As Integer = 1

                If intCve > 0 Then
                    intOpc = 2
                End If

                'guardamos la información
                objMoneda.PDK_ID_MONEDA = intCve
                objMoneda.PDK_MON_NOMBRE = txtNombreMoneda.Text.Trim.ToUpper
                objMoneda.PDK_MON_TIPO_CAMBIO = CDbl(txtTipoCambio.Text)
                objMoneda.PDK_MON_MODIF = Format(Now(), "yyyy-MM-dd")
                objMoneda.PDK_CLAVE_USUARIO = Session("IdUsua")
                objMoneda.PDK_MON_ACTIVO = IIf(chkActivo.Checked = True, 2, 3)


                objMoneda.Guarda()


                Master.MensajeError("La información se guardo con éxito")
                hdnIdRegistro.Value = objMoneda.PDK_ID_MONEDA
                CargaInfo()

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub


End Class