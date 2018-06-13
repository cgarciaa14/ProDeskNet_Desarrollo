'BUG-PD-254: ERODRIGUEZ: 27/10/2017: Se creo ventana para el historial de caja de notas externas e internas.
'BUG-PD-268: RIGLESIAS:  10/11/2017: Se creo metodo para filtro de  busqueda de solicitud y boton de limpiar 
'BUG-PD-438 : EGONZALEZ : 08/05/2018 : Se elimina la precarga de notas
Imports ProdeskNet.Seguridad
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Data
Imports System.Data.SqlClient


Partial Class aspx_HistorialNotas
    Inherits System.Web.UI.Page
    Dim BD As New ProdeskNet.BD.clsManejaBD


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim dsresult As DataSet
        If IsPostBack Then
            dsresult = BD.EjecutarQuery("EXEC getHistorialNotas " & 1)
            If dsresult.Tables.Count > 0 Then
                If dsresult.Tables(0).Rows.Count > 0 Then
                    Session("dtsConsultaG") = dsresult
                    GridViewGral.DataSource = dsresult
                    GridViewGral.DataBind()
                End If
            End If
        End If



        'If ID.Value <> "" Then
        '    Dim number As Int64
        'End If
        'Dim result As Boolean = Int64.TryParse(tx.Value, number)

        'If result Then
        'End If


    End Sub

    Protected Sub GridViewGral_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        If CType(Session("dtsConsultaG"), DataSet) Is Nothing Then
            Dim nusu As Int64
            Dim resultusu As Boolean = Int64.TryParse(txtIdnota.Value, nusu)
            If (resultusu) Then
                ' BuscaMensajes(2, BuscaPermiso(nusu))
                'Page_Load(Me, e)
            End If
            Page_Load(Me, e)

        End If

        GridViewGral.PageIndex = e.NewPageIndex
        GridViewGral.DataSource = CType(Session("dtsConsultaG"), DataSet)
        GridViewGral.DataBind()

        'Page_Load(Me, e)
    End Sub

   
    Protected Sub btnBuscaNota_Click(sender As Object, e As EventArgs)
        Try
            Dim number As Int64
            Dim result As Boolean = Int16.TryParse(txtIdnota.Value, number)
            If result Then
                Dim Idnota As String = txtIdnota.Value.ToString()
                If IsNumeric(Idnota) Then
                    If Idnota <> "" Then
                        Dim dsresult As DataSet
                        dsresult = BD.EjecutarQuery("EXEC getHistorialNotas " & 2 & "," & Idnota)
                        If (dsresult.Tables.Count > 0 AndAlso dsresult.Tables(0).Rows.Count > 0) Then
                            GridViewGral.DataSource = dsresult
                            GridViewGral.DataBind()
                        Else
                            Throw New Exception("No existe esta solicitud")
                        End If
                    Else
                        Throw New Exception("Debe existir una solicitud ")
                    End If
                Else
                    Throw New Exception("Formato incorrecto")

                End If
            Else
                Throw New Exception("Formato incorrecto")
            End If



        Catch ex As Exception


            Master.MensajeError(ex.Message)
        End Try

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        txtIdnota.Value = String.Empty

    End Sub
End Class
