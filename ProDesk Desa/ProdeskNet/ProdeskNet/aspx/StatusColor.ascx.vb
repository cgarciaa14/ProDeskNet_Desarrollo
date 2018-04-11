'BBV-P-423: RQSOL-03: AVH: 10/11/2016 SE CREA GRID DE COLORES
'BBV-P-423: RQ H - CSS JRHM 09/12/2016 Se cambio el color blanco del grid por un tono gris por presentacion de pagina de seguimiento
'BUG PD-30 CGARCIA 10/04/2017 Se cambio la paleta de colores del estatus del sistema 
Imports System.Data
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos

Partial Class aspx_StatusColor
    Inherits System.Web.UI.UserControl
    Dim OPCION As Integer = 1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objFlujos As New clsSolicitudes(0)
        Dim dsSolicitudes As DataSet
        Dim dsMenu As DataSet
        Dim dsColor As DataSet
        dsMenu = objFlujos.ConsultaSolicitud(1)
        OPCION = dsMenu.Tables(0).Rows(0).Item("PDK_PAR_SIS_STATUS").ToString
        dsColor = objFlujos.ConsultaSolicitud(2)
        If dsColor.Tables.Count > 0 Then
            If dsColor.Tables(0).Rows.Count > 0 Then
                Me.gvColor.DataSource = dsColor
                Me.gvColor.DataBind()
            End If
        End If
        If OPCION = 2 Then
            For a As Integer = 0 To gvColor.Rows.Count - 1
                Select Case gvColor.Rows(a).Cells(1).Text
                    Case "0"
                        Me.gvColor.Rows(1).Cells(0).Visible = False
                        Select Case gvColor.Rows(a).Cells(1).Text
                            Case "Amarillo"
                                gvColor.Rows(1).Cells(1).BackColor = Drawing.ColorTranslator.FromHtml("#B5E5F9")
                                gvColor.Rows(1).Cells(1).ForeColor = Drawing.Color.Black
                                Select Case gvColor.Rows(a).Cells(2).Text
                                    Case "Verde"
                                        gvColor.Rows(1).Cells(2).BackColor = Drawing.ColorTranslator.FromHtml("#52BCEC")
                                        gvColor.Rows(1).Cells(2).ForeColor = Drawing.Color.Black
                                        Select Case gvColor.Rows(a).Cells(3).Text
                                            Case "Rojo"
                                                gvColor.Rows(1).Cells(3).BackColor = Drawing.ColorTranslator.FromHtml("#F6891E")
                                                gvColor.Rows(1).Cells(3).ForeColor = Drawing.Color.Black
                                                Select Case gvColor.Rows(a).Cells(4).Text
                                                    Case "Azul"
                                                        gvColor.Rows(1).Cells(4).BackColor = Drawing.ColorTranslator.FromHtml("#006EC1")
                                                        gvColor.Rows(1).Cells(4).ForeColor = Drawing.Color.Black
                                                End Select
                                        End Select
                                End Select
                        End Select
                End Select
            Next
            gvColor.HeaderRow.Visible = False
        ElseIf OPCION = 3 Then
            For a As Integer = 0 To gvColor.Rows.Count - 1
                Select Case gvColor.Rows(a).Cells(0).Text
                    Case "0"
                        Me.gvColor.Rows(1).Cells(0).Visible = False
                        Select Case gvColor.Rows(a).Cells(1).Text
                            Case "Amarillo"
                                gvColor.Rows(1).Cells(1).BackColor = Drawing.ColorTranslator.FromHtml("#B5E5F9")
                                gvColor.Rows(1).Cells(1).ForeColor = Drawing.Color.Black
                                Select Case gvColor.Rows(a).Cells(2).Text
                                    Case "Verde"
                                        gvColor.Rows(1).Cells(2).BackColor = Drawing.ColorTranslator.FromHtml("#52BCEC")
                                        gvColor.Rows(1).Cells(2).ForeColor = Drawing.Color.Black
                                        Select Case gvColor.Rows(a).Cells(3).Text
                                            Case "Rojo"
                                                gvColor.Rows(1).Cells(3).BackColor = Drawing.ColorTranslator.FromHtml("#F6891E")
                                                gvColor.Rows(1).Cells(3).ForeColor = Drawing.Color.Black
                                                Select Case gvColor.Rows(a).Cells(4).Text
                                                    Case "Azul"
                                                        gvColor.Rows(1).Cells(4).BackColor = Drawing.ColorTranslator.FromHtml("#006EC1")
                                                        gvColor.Rows(1).Cells(4).ForeColor = Drawing.Color.Black
                                                End Select
                                        End Select
                                End Select
                        End Select
                End Select
            Next
            gvColor.Rows(0).Visible = False
            gvColor.HeaderRow.Visible = False

        End If
    End Sub
    Protected Sub gvColor_RowCreated(sender As Object, e As GridViewRowEventArgs)
        If OPCION = 2 Then
            e.Row.Cells(1).Visible = False
        End If
    End Sub
End Class
