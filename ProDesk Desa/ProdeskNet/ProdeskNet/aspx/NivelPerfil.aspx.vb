
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Configurcion
Imports System.Data

Public Class NivelPerfil
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            hdnIdRegistro.Value = Session("IdUsua")
            nivel()
            BuscarNivel(lblNiveles.SelectedValue)

        End If


    End Sub
    Private Sub BuscarNivel(ByVal intnivel As Integer)
        Try
            Dim res As Boolean

            grvNivel.DataSource = Nothing
            grvNivel.DataBind()

            Dim datSet As New DataSet
            datSet = clsPerfil.BuscarNivel(intnivel)
            If datSet.Tables.Count > 0 AndAlso datSet.Tables(0).Rows.Count > 0 Then
                grvNivel.DataSource = datSet.Tables(0)
                grvNivel.DataBind()

                For intRegis = 0 To grvNivel.Rows.Count - 1
                    Dim check As Object = CType(grvNivel.Rows(intRegis).FindControl("cnkstatus"), CheckBox)
                    res = datSet.Tables(0).Rows(intRegis)(2)
                    check.Checked = res
                Next
            End If


        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub
    Private Sub nivel()
        Dim objCombo As New clsParametros
        Try
            Dim dtsRed As New DataSet
            dtsRed = clsPerfil.niveles
            If dtsRed.Tables.Count > 0 And dtsRed.Tables(0).Rows.Count > 0 Then
                objCombo.LlenaCombos(dtsRed, "PDK_PER_NOMBRE", "PDK_PER_NIVEL", lblNiveles)
            End If
        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try

    End Sub

  
    Private Sub lblNiveles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblNiveles.SelectedIndexChanged
        BuscarNivel(lblNiveles.SelectedValue)
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            Dim i As Integer = 0
            Dim status As Integer = 0
            Dim statusNicel As Integer = 0
            Dim staCheck As New CheckBox

            For i = 0 To grvNivel.Rows.Count - 1

                staCheck = CType(grvNivel.Rows(i).FindControl("cnkstatus"), CheckBox)
                If staCheck.Checked = True Then
                    statusNicel = 1
                Else
                    statusNicel = 0
                End If
                status = clsPerfil.INSERTARNIVEL(grvNivel.Rows(i).Cells(0).Text, lblNiveles.SelectedValue, statusNicel, hdnIdRegistro.Value)

            Next
            If status = 1 Then
                Master.MensajeError("Información almacenada exitosamente")
            Else
                Master.MensajeError("Error almacenar la información")
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
        End Try
    End Sub
End Class