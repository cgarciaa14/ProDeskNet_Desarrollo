'BUG-PD-277: 29/11/2017: RIGLESIAS: SE AGREGO False A  Response PARA EVITAR QUE LA PAGINA MUERA 
'BUG-PD-309: 28/12/2017: DJUAREZ: Se cambio redirect para evitar que la pagina deje de funcionar y se pierdan estilos
'BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos.  
Imports ProdeskNet.Catalogos
Imports System.Data

Public Class manejoParametrosSistema
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then

            If Val(Request("idPar")) > 0 Then
                hdnIdRegistro.Value = Request("idPar")
                LlenaCombo(1)
                CargaInfo()
            End If

        End If

    End Sub


    Private Sub CargaInfo()
        Try
            Dim intClave As Integer = Val(hdnIdRegistro.Value)
            Dim objParametro As New clsParametrosSistema(intClave)

            If objParametro.PDK_ID_PARAMETROS_SISTEMA > 0 Then

                lblID.Text = objParametro.PDK_ID_PARAMETROS_SISTEMA
                txtNombre.Text = objParametro.PDK_PAR_SIS_PARAMETRO
                txtValorFecha.Text = objParametro.PDK_PAR_SIS_VALOR_FECHA
                txtValorNumero.Text = objParametro.PDK_PAR_SIS_VALOR_NUMERO
                txtValorTexto.Text = objParametro.PDK_PAR_SIS_VALOR_TEXTO
                chkStatus.Checked = IIf(objParametro.PDK_PAR_SIS_STATUS = 2, True, False)
                ddlPadre.SelectedIndex = ddlPadre.Items.IndexOf(ddlPadre.Items.FindByValue(objParametro.PDK_PAR_SIS_ID_PADRE.ToString.Trim))
            Else
                Master.MensajeError("No se localizó información para el cl-iente")
            End If



        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub


    Private Sub LlenaCombo(ByVal intCombo As Integer)
        Dim dsDataset As New DataSet
        Dim i As Integer = 0
        Try

            If intCombo = 1 Then
                dsDataset = clsParametrosSistema.ObtenTodos(-1)
                ddlPadre.Items.Add(New ListItem("NINGUNO", "0"))
                For i = 0 To dsDataset.Tables(0).Rows.Count - 1
                    ddlPadre.Items.Add(New ListItem(dsDataset.Tables(0).Rows(i).Item("PDK_PAR_SIS_PARAMETRO").ToString.Trim, dsDataset.Tables(0).Rows(i).Item("PDK_ID_PARAMETROS_SISTEMA").ToString.Trim))
                Next


            End If
        Catch ex As Exception
            ddlPadre = Nothing
        End Try
    End Sub



    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If Trim(txtNombre.Text) = "" Then Exit Function

        ValidaCampos = True
    End Function

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Dim path As String = "./consultaParametrosSistema.aspx"
        ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & path & "';", True)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then
                Dim intCve As Integer = Val(hdnIdRegistro.Value)
                Dim objParametro As New clsParametrosSistema(intCve)
                Dim intOpc As Integer = 1

                If intCve > 0 Then
                    intOpc = 2
                End If

                'guardamos la información
                With objParametro
                    .PDK_ID_PARAMETROS_SISTEMA = intCve
                    .PDK_PAR_SIS_PARAMETRO = txtNombre.Text.Trim.ToUpper
                    .PDK_PAR_SIS_STATUS = IIf(chkStatus.Checked = True, 2, 3)
                    .PDK_PAR_SIS_MODIF = Format(Now(), "yyyy-MM-dd")
                    .PDK_CLAVE_USUARIO = Session("IdUsua")
                    .PDK_PAR_SIS_VALOR_FECHA = Me.txtValorFecha.Text
                    .PDK_PAR_SIS_VALOR_TEXTO = Me.txtValorTexto.Text
                    .PDK_PAR_SIS_VALOR_NUMERO = Me.txtValorNumero.Text
                    .PDK_PAR_SIS_ID_PADRE = CInt(Me.ddlPadre.SelectedValue.ToString)
                    .Guarda()
                End With

                Master.MensajeError("La información se guardo con éxito")
                hdnIdRegistro.Value = objParametro.PDK_ID_PARAMETROS_SISTEMA
                CargaInfo()

            Else
                Master.MensajeError("Todos los campos marcados con * son obligatorios")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub


End Class