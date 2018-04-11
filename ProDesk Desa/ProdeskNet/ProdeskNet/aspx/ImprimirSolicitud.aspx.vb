Imports ProdeskNet.Seguridad
Imports ProdeskNet.Catalogos
Imports ProdeskNet.Configurcion
Imports System.Xml
Imports System.Xml.Xsl


Public Class aspx_ImprimirSolicitud
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try

                hdnPantalla.Value = Request("idPantalla")
                hdnFolio.Value = Request("IdFolio")
                hdnUsuar.Value = Request("IdUsu")

                'hdnXML.Value = Session("xml")
                'hdnXSL.Value = Session("xsl")
                ''If Session("entra") = Nothing Then
                ''    Session("entra") = 2
                ''    txtentrada.Value = 2  IdUsu
                ''End If

                Dim strXML As String = ""
                Dim strXSL As String = ""
                Session("xml") = Nothing
                Session("xsl") = Nothing

                Select Case Request("CVE")
                    Case 0
                        strXML = clsPantallaObjeto.ObtenerPantaXlm(hdnPantalla.Value, hdnFolio.Value, 1)
                        strXSL = clsPantallaObjeto.ObtenerPantaXlm(hdnPantalla.Value, hdnFolio.Value, 2)
                        If strXML <> "" Then
                            hdnXML.Value = strXML
                        End If
                        If strXSL <> "" Then
                            hdnXSL.Value = strXSL
                        End If

                    Case 1
                        'strXSL = clsPantallaObjeto.ObtenerEntreReporte(hdnPantalla.Value, hdnFolio.Value, hdnUsuar.Value, 1, 1)
                        'If strXSL <> "" Then
                        '    hdnXML.Value = ""
                        '    hdnXSL.Value = strXSL
                        'End If

                End Select



                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "script", "fnTransformaXSL();", True)
                'If Not IsPostBack Then
                'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "script", "reFresh();", True)

                'End If

                ''If solictud > 0 Then
                ''    Dim data As New DataSet
                ''    Dim docXML As New XmlDocument

                ''    strxml = clsPantallaObjeto.ObtenerPantaXlm(pantalla, solictud)
                ''    docXML.LoadXml(strxml)
                ''    xml1.Document = docXML



                ''End If

            Catch ex As Exception

            End Try
            ''Else
            ''    ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "script", "fnForzap();", True)
        End If
    End Sub

End Class