Imports ProdeskNet.Catalogos
Imports System.Data

Public Class altaParametrosSistema
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Title = "PRODESKNET 3.0"

        If Not IsPostBack Then
            LlenaCombo(1)
        End If

    End Sub


    Private Sub LlenaCombo(ByVal intCombo As Integer)
        Dim dsDataset As New DataSet
        Dim i As Integer = 0
        Try

            If intCombo = 1 Then
                dsDataset = clsParametrosSistema.ObtenTodos(-1)
                ddlPadre.Items.Add(New ListItem("NINGUNO", "0"))
                For i = 0 To dsDataset.Tables(0).Rows.Count - 1
                    ddlPadre.Items.Add(New ListItem(dsDataset.Tables(0).Rows(i).Item("PDK_PAR_SIS_PARAMETRO").ToString, dsDataset.Tables(0).Rows(i).Item("PDK_ID_PARAMETROS_SISTEMA").ToString))
                Next
            End If
        Catch ex As Exception
            ddlPadre = Nothing
        End Try
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("./consultaParametrosSistema.aspx")
    End Sub


    Private Function ValidaCampos() As Boolean
        ValidaCampos = False

        If txtNombre.Text.Trim.Length = 0 Then Exit Function

        ValidaCampos = True

    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If ValidaCampos() Then

                Dim objPar As New clsParametrosSistema(0)

                'guardamos la información
                objPar.PDK_PAR_SIS_STATUS = IIf(chkStatus.Checked = True, 2, 3)
                objPar.PDK_PAR_SIS_PARAMETRO = txtNombre.Text.Trim.ToUpper
                objPar.PDK_PAR_SIS_VALOR_TEXTO = txtValorTexto.Text.Trim.ToUpper
                objPar.PDK_PAR_SIS_VALOR_FECHA = txtValorFecha.Text.Trim
                objPar.PDK_PAR_SIS_VALOR_NUMERO = txtValorNumero.Text.Trim
                objPar.PDK_PAR_SIS_ID_PADRE = CInt(ddlPadre.SelectedValue.ToString)
                objPar.PDK_PAR_SIS_MODIF = Format(Now(), "yyyy-MM-dd")
                objPar.PDK_CLAVE_USUARIO = Session("IdUsua")

                objPar.Guarda()

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