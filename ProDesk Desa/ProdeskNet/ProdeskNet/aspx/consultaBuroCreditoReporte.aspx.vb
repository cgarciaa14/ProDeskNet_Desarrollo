Imports ProdeskNet.Buro
Imports System.Xml
Imports System.Xml.Xsl

#Region "Trackers"
'Tracker INC-B-2114:JDRA:Cambio en el area de SC
#End Region

Public Class aspx_consultaBuroCreditoReporte
    Inherits System.Web.UI.Page

    Public strHTML As String = String.Empty
    Public intSolicitud As Integer = 0
    Public strRFC As String = String.Empty
    Public intBuro As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim PRDSK_CVE_SOLICITUD As String = String.Empty
        Dim PRDSK_CVE_REPORTEBURO As String = String.Empty
        Dim PRDSK_ID_BURO As String = String.Empty

        Dim strXML As String = String.Empty

        Try
            If Not IsPostBack Then
                'If Len(Session("USUARIO_SYS")) = 0 Then
                '    Response.Redirect("../../../Login.aspx")
                '    Exit Sub
                'End If
                intSolicitud = CInt(Request.QueryString("IdSolicitud"))
                intBuro = CInt(Request.QueryString("IdBuro"))

                strXML = GeneraXMLBuro( _
                            intSolicitud, _
                            "0", _
                            Session("IdUsua"))

                Dim docXML As New XmlDocument
                docXML.LoadXml(strXML)
                Dim docXSL As New XslTransform
                docXSL.Load(Server.MapPath("REPORTE_BURO.xsl"))
                Xml1.Document = docXML
                Xml1.Transform = docXSL

                ''strHTML = strXML
            End If
        Catch ex As Exception
            strHTML = ""
        End Try
    End Sub

    Private Function GeneraXMLBuro( _
                ByVal PRDSK_CVE_SOLICITUD As String, _
                ByVal PRDSK_CVE_PERSONA As String, _
                ByVal USUARIO_SYS As String) As String

        Dim dsDataset As New Data.DataSet
        Dim BURO As New clsBuroBitacora(0)
        Dim BCString As String = String.Empty
        Dim PNCampos As Integer = 0
        Dim PACampos As Integer = 0
        Dim PECampos As Integer = 0
        Dim TDCampos As Integer = 0
        Dim IQ_N As Integer = 0
        Dim IQCampos As Integer = 0
        Dim RSCampos As Integer = 0
        Dim HICampos As Integer = 0
        Dim SCCampos As Integer = 0

        Dim x As Integer = 0
        Dim y As Integer = 0

        Dim strXML As String = String.Empty

        Try

            'Aqui se manda llamar el proceso de buró de crédito

            Dim bolRespuesta As Boolean = False
            Dim strResultado As String = String.Empty
            Dim PRDSKBURO As New clsBuroReporte(intSolicitud, intBuro)

            'dsDataset = PRDSKBURO.PDK_BUR_RESPUESTA
            'strResultado = dsDataset.Tables(0).Rows(0).Item("RESPUESTABURO").ToString.Trim
            strResultado = PRDSKBURO.PDK_BUR_REP_RESPUESTA
            '**************************************************

            If strResultado.Trim.Length < 10 Then
                strXML &= "No se encontró reporte crediticio"
            Else
                BCString = strResultado

                BCString = Replace(BCString, vbCrLf, "")
                BCString = Replace(BCString, "&", "Y")

                Dim INTL, Paterno, Direccion(4), Empresa(4), Trades(100), Consultas(100), Resumen(100), Hawk(100), Comentario, Score(100)
                Dim PN_Campos, PA_Campos, PE_Campos, TD_Campos, IQ_Campos, RS_Campos, HI_Campos, SC_Campos
                Dim PN(100), PA(50, 100), PE(50, 100), TD(100, 100), IQ(100, 100), RS(50, 100), HI(50, 100), apunta, campo, longitud, Dir_N, Emp_N, TD_N, RS_N, Hawk_N, SC_N
                Dim SC(50, 100)

                INTL = Left(BCString, 49)

                apunta = 50

                If Mid(BCString, apunta, 2) = "PN" Then
                    apunta = apunta + 2
                    longitud = CInt(Mid(BCString, apunta, 2))
                    apunta = apunta + 2
                    Paterno = Mid(BCString, apunta, longitud)

                    apunta = apunta + longitud

                    campo = Mid(BCString, apunta, 2)

                    Do While campo <> "PA"
                        apunta = apunta + 2
                        longitud = CInt(Mid(BCString, apunta, 2))
                        apunta = apunta + 2
                        PN(CInt(campo)) = Mid(BCString, apunta, longitud)
                        apunta = apunta + longitud
                        PNCampos = CInt(campo)
                        campo = Mid(BCString, apunta, 2)
                    Loop
                End If


                Dir_N = 1
                If Mid(BCString, apunta, 2) = "PA" Then
                    apunta = apunta + 2
                    longitud = CInt(Mid(BCString, apunta, 2))
                    apunta = apunta + 2
                    Direccion(Dir_N) = Mid(BCString, apunta, longitud)
                    apunta = apunta + longitud

                    campo = Mid(BCString, apunta, 2)

                    Do While campo <> "PE"
                        apunta = apunta + 2
                        longitud = CInt(Mid(BCString, apunta, 2))
                        apunta = apunta + 2

                        PA(Dir_N, CInt(campo)) = Mid(BCString, apunta, longitud)
                        apunta = apunta + longitud

                        PACampos = CInt(campo)
                        campo = Mid(BCString, apunta, 2)
                        If campo = "PA" Then
                            Dir_N = Dir_N + 1
                            apunta = apunta + 2
                            longitud = CInt(Mid(BCString, apunta, 2))
                            apunta = apunta + 2
                            Direccion(Dir_N) = Mid(BCString, apunta, longitud)
                            apunta = apunta + longitud
                            campo = Mid(BCString, apunta, 2)
                        End If
                        'Control si solo hay una trama
                        If campo = "PE" Or campo = "TL" Or campo = "ES" Or campo = "HI" Or campo = "HR" Or campo = "RS" Or campo = "IQ" Then Exit Do
                    Loop
                End If


                Emp_N = 1
                If Mid(BCString, apunta, 2) = "PE" Then
                    apunta = apunta + 2
                    longitud = CInt(Mid(BCString, apunta, 2))
                    apunta = apunta + 2
                    Empresa(Emp_N) = Mid(BCString, apunta, longitud)
                    apunta = apunta + longitud

                    campo = Mid(BCString, apunta, 2)

                    Do While campo <> "TL"
                        apunta = apunta + 2
                        longitud = CInt(Mid(BCString, apunta, 2))
                        apunta = apunta + 2

                        PE(Emp_N, CInt(campo)) = Mid(BCString, apunta, longitud)
                        apunta = apunta + longitud

                        PECampos = CInt(campo)
                        campo = Mid(BCString, apunta, 2)
                        If campo = "PE" Then
                            Emp_N = Emp_N + 1
                            apunta = apunta + 2
                            longitud = CInt(Mid(BCString, apunta, 2))
                            apunta = apunta + 2
                            Empresa(Emp_N) = Mid(BCString, apunta, longitud)
                            apunta = apunta + longitud
                            campo = Mid(BCString, apunta, 2)
                        End If
                        'Control si solo hay una trama
                        If campo = "TL" Or campo = "IQ" Or campo = "ES" Or campo = "HI" Or campo = "HR" Or campo = "RS" Then Exit Do
                    Loop
                End If

                TD_N = 1
                Try
                    If Mid(BCString, apunta, 2) = "TL" Then
                        apunta = apunta + 2
                        longitud = CInt(Mid(BCString, apunta, 2))
                        apunta = apunta + 2
                        Trades(TD_N) = Mid(BCString, apunta, longitud)
                        apunta = apunta + longitud

                        campo = Mid(BCString, apunta, 2)

                        Do While campo <> "IQ"
                            apunta = apunta + 2
                            longitud = CInt(Mid(BCString, apunta, 2))
                            apunta = apunta + 2

                            TD(TD_N, CInt(campo)) = Mid(BCString, apunta, longitud)
                            apunta = apunta + longitud

                            TDCampos = CInt(campo)
                            campo = Mid(BCString, apunta, 2)
                            If campo = "TL" Then
                                TD_N = TD_N + 1
                                apunta = apunta + 2
                                longitud = CInt(Mid(BCString, apunta, 2))
                                apunta = apunta + 2
                                Trades(TD_N) = Mid(BCString, apunta, longitud)
                                apunta = apunta + longitud
                                campo = Mid(BCString, apunta, 2)
                            End If
                            'Control si solo hay una trama
                            If campo = "IQ" Or campo = "ES" Or campo = "HI" Or campo = "HR" Or campo = "RS" Then Exit Do
                        Loop
                    End If
                Catch ex As Exception
                End Try


                IQ_N = 1
                If Mid(BCString, apunta, 2) = "IQ" Then
                    apunta = apunta + 2
                    longitud = CInt(Mid(BCString, apunta, 2))
                    apunta = apunta + 2
                    Consultas(IQ_N) = Mid(BCString, apunta, longitud)
                    apunta = apunta + longitud

                    campo = Mid(BCString, apunta, 2)
                    Try
                        Do While campo <> "RS"
                            apunta = apunta + 2
                            longitud = CInt(Mid(BCString, apunta, 2))
                            apunta = apunta + 2

                            IQ(IQ_N, CInt(campo)) = Mid(BCString, apunta, longitud)
                            apunta = apunta + longitud

                            IQCampos = CInt(campo)
                            campo = Mid(BCString, apunta, 2)
                            If campo = "IQ" Then
                                IQ_N = IQ_N + 1
                                apunta = apunta + 2
                                longitud = CInt(Mid(BCString, apunta, 2))
                                apunta = apunta + 2
                                Consultas(IQ_N) = Mid(BCString, apunta, longitud)
                                apunta = apunta + longitud
                                campo = Mid(BCString, apunta, 2)
                            End If
                            'Control si solo hay una trama
                            If campo = "RS" Or campo = "ES" Or campo = "HI" Or campo = "HR" Then Exit Do
                        Loop
                    Catch ex As Exception
                    End Try
                End If


                RS_N = 1
                If Mid(BCString, apunta, 2) = "RS" Then
                    apunta = apunta + 2
                    longitud = CInt(Mid(BCString, apunta, 2))
                    apunta = apunta + 2
                    Resumen(RS_N) = Mid(BCString, apunta, longitud)
                    apunta = apunta + longitud

                    campo = Mid(BCString, apunta, 2)
                    Do While (campo <> "HI" Or campo <> "HR" Or campo <> "CR" Or campo <> "SC" Or campo <> "ES") AndAlso Len(campo) > 0
                        Try
                            apunta = apunta + 2
                            longitud = CInt(Mid(BCString, apunta, 2))

                            apunta = apunta + 2

                            'strXML &=campo)
                            RS(RS_N, CInt(campo)) = Mid(BCString, apunta, longitud)
                            apunta = apunta + longitud

                            RSCampos = CInt(campo)
                            campo = Mid(BCString, apunta, 2)
                            If campo = "RS" Then
                                RS_N = RS_N + 1
                                apunta = apunta + 2
                                longitud = CInt(Mid(BCString, apunta, 2))
                                apunta = apunta + 2
                                Resumen(RS_N) = Mid(BCString, apunta, longitud)
                                apunta = apunta + longitud
                                campo = Mid(BCString, apunta, 2)
                            End If
                            'Control si solo hay una trama
                            If campo = "ES" Or campo = "HI" Or campo = "HR" Or campo = "BS" Or campo = "CR" Or campo = "SC" Then Exit Do
                        Catch ex As Exception
                        End Try
                    Loop
                End If

                'response.End()
                Hawk_N = 1
                If Mid(BCString, apunta, 2) = "HI" Or Mid(BCString, apunta, 2) = "HR" Then
                    apunta = apunta + 2
                    longitud = CInt(Mid(BCString, apunta, 2))
                    apunta = apunta + 2
                    Hawk(Hawk_N) = Mid(BCString, apunta, longitud)
                    apunta = apunta + longitud

                    campo = Mid(BCString, apunta, 2)

                    Do While campo <> "CR" Or campo <> "SC" Or campo <> "ES"
                        apunta = apunta + 2
                        longitud = CInt(Mid(BCString, apunta, 2))
                        apunta = apunta + 2

                        HI(Hawk_N, CInt(campo)) = Mid(BCString, apunta, longitud)
                        apunta = apunta + longitud

                        HICampos = CInt(campo)
                        campo = Mid(BCString, apunta, 2)
                        If campo = "HI" Or campo = "HR" Then
                            Hawk_N = Hawk_N + 1
                            apunta = apunta + 2
                            longitud = CInt(Mid(BCString, apunta, 2))
                            apunta = apunta + 2
                            Hawk(Hawk_N) = Mid(BCString, apunta, longitud)
                            apunta = apunta + longitud
                            campo = Mid(BCString, apunta, 2)
                        End If
                        'Control si solo hay una trama
                        If campo = "ES" Or campo = "BS" Or campo = "CR" Or campo = "SC" Then Exit Do
                    Loop
                End If


                If Mid(BCString, apunta, 2) = "CR" Then
                    apunta = apunta + 2 + 2 ' El comentario es CR04 siempre con 0000 como campo declaracion sin longitud
                    longitud = CInt(Mid(BCString, apunta, 4)) 'Mas largo que 2 bytes la longitud del comentario
                    apunta = apunta + 4 + 4
                    Comentario = Mid(BCString, apunta, longitud)
                    Comentario = Replace(Comentario, "&", "")
                    apunta = apunta + longitud
                    campo = Mid(BCString, apunta, 2)
                End If



                SC_N = 1
                If Mid(BCString, apunta, 2) = "SC" Then
                    apunta = apunta + 2
                    longitud = CInt(Mid(BCString, apunta, 2))
                    apunta = apunta + 2
                    Score(SC_N) = Mid(BCString, apunta, longitud)
                    apunta = apunta + longitud

                    campo = Mid(BCString, apunta, 2)
                    Do While (campo <> "HI" Or campo <> "HR" Or campo <> "CR" Or campo <> "ES") And Len(campo) > 0
                        Try
                            apunta = apunta + 2
                            longitud = CInt(Mid(BCString, apunta, 2))

                            apunta = apunta + 2

                            SC(SC_N, CInt(campo)) = Mid(BCString, apunta, longitud)
                            apunta = apunta + longitud

                            SCCampos = CInt(campo)
                            campo = Mid(BCString, apunta, 2)
                            If campo = "SC" Then
                                SC_N = SC_N + 1
                                apunta = apunta + 2
                                longitud = CInt(Mid(BCString, apunta, 2))
                                apunta = apunta + 2
                                Score(SC_N) = Mid(BCString, apunta, longitud)
                                apunta = apunta + longitud
                                campo = Mid(BCString, apunta, 2)
                            End If
                            'Control si solo hay una trama
                            If campo = "ES" Or campo = "HI" Or campo = "HR" Or campo = "BS" Or campo = "CR" Then Exit Do
                        Catch ex As Exception
                        End Try
                    Loop
                End If

                'FALTA BC SCORE BS


                '-- OUTPUT DEL BURO DE CREDITO EN XML -->>>>>>>>>
                strXML = ""
                strXML &= "<?xml version=""1.0"" encoding=""windows-1252"" ?>" & vbCrLf
                strXML &= "<?xml-stylesheet type=""text/xsl"" href=""REPORTE_BURO.xsl""?>" & vbCrLf
                strXML &= "<Reporte>" & vbCrLf
                If Mid(INTL, 48, 1) = "1" Then strXML &= "<Clave>1</Clave>" 'Encontrado
                strXML &= "<PN>" & vbCrLf
                strXML &= "<Paterno>"
                strXML &= Paterno & "</Paterno>" & vbCrLf
                For x = 0 To PNCampos
                    If x < 10 Then
                        strXML &= "<C0" & x & ">" & PN(x) & "</C0" & x & ">" & vbCrLf
                    Else
                        strXML &= "<C" & x & ">" & PN(x) & "</C" & x & ">" & vbCrLf
                    End If
                Next
                strXML &= "</PN>" & vbCrLf

                For x = 1 To Dir_N
                    strXML &= "<PA>" & vbCrLf
                    strXML &= "<Direccion>"
                    strXML &= Direccion(x) & "</Direccion>" & vbCrLf
                    For y = 1 To (PACampos + 1)
                        If y < 10 Then
                            strXML &= "<C0" & y & ">" & PA(x, y) & "</C0" & y & ">" & vbCrLf
                        Else
                            strXML &= "<C" & y & ">" & PA(x, y) & "</C" & y & ">" & vbCrLf
                        End If
                    Next
                    strXML &= "</PA>" & vbCrLf
                Next

                For x = 1 To Emp_N
                    strXML &= "<PE>" & vbCrLf
                    strXML &= "<Empresa>"
                    strXML &= Empresa(x) & "</Empresa>" & vbCrLf
                    For y = 0 To PECampos
                        If y < 10 Then
                            strXML &= "<C0" & y & ">" & PE(x, y) & "</C0" & y & ">" & vbCrLf
                        Else
                            strXML &= "<C" & y & ">" & PE(x, y) & "</C" & y & ">" & vbCrLf
                        End If
                    Next
                    strXML &= "</PE>" & vbCrLf
                Next
                TDCampos = 30
                For x = 1 To TD_N
                    strXML &= "<TD>" & vbCrLf
                    strXML &= "<FechaAct>"
                    strXML &= Trades(x) & "</FechaAct>" & vbCrLf
                    For y = 1 To TDCampos
                        If y < 10 Then
                            strXML &= "<C0" & y & ">" & TD(x, y) & "</C0" & y & ">" & vbCrLf
                        Else
                            strXML &= "<C" & y & ">" & TD(x, y) & "</C" & y & ">" & vbCrLf
                        End If
                    Next
                    strXML &= "</TD>" & vbCrLf
                Next

                For x = 1 To IQ_N
                    strXML &= "<IQ>" & vbCrLf
                    strXML &= "<Consulta>"
                    strXML &= Consultas(x) & "</Consulta>" & vbCrLf
                    For y = 1 To IQCampos
                        If y < 10 Then
                            strXML &= "<C0" & y & ">" & IQ(x, y) & "</C0" & y & ">" & vbCrLf
                        Else
                            strXML &= "<C" & y & ">" & IQ(x, y) & "</C" & y & ">" & vbCrLf
                        End If
                    Next
                    strXML &= "</IQ>" & vbCrLf
                Next

                For x = 1 To RS_N
                    strXML &= "<RS>" & vbCrLf
                    strXML &= "<Fecha>"
                    strXML &= Resumen(x) & "</Fecha>" & vbCrLf
                    For y = 1 To RSCampos
                        If y < 10 Then
                            strXML &= "<C0" & y & ">" & RS(x, y) & "</C0" & y & ">" & vbCrLf
                        Else
                            strXML &= "<C" & y & ">" & RS(x, y) & "</C" & y & ">" & vbCrLf
                        End If
                    Next
                    strXML &= "</RS>" & vbCrLf
                Next

                For x = 1 To Hawk_N
                    strXML &= "<HIR>" & vbCrLf
                    strXML &= "<Fecha>"
                    strXML &= Hawk(x) & "</Fecha>" & vbCrLf
                    For y = 1 To HICampos
                        If y < 10 Then
                            strXML &= "<C0" & y & ">" & HI(x, y) & "</C0" & y & ">" & vbCrLf
                        Else
                            strXML &= "<C" & y & ">" & HI(x, y) & "</C" & y & ">" & vbCrLf
                        End If
                    Next
                    strXML &= "</HIR>" & vbCrLf
                Next


                For x = 1 To SC_N
                    strXML &= "<SC>" & vbCrLf
                    strXML &= "<Fecha>"
                    strXML &= Score(x) & "</Fecha>" & vbCrLf
                    For y = 0 To SCCampos
                        If y < 10 Then
                            If y = 0 Then                                
                                If SC(x, y) = "007" Then
                                    strXML &= "<C0" & y & ">BC SCORE</C0" & y & ">" & vbCrLf
                                ElseIf SC(x, y) = "004" Then
                                    strXML &= "<C0" & y & ">Índice de capacidad crediticia</C0" & y & ">" & vbCrLf
                                End If
                            End If
                            strXML &= "<C0" & y & ">" & SC(x, y) & "</C0" & y & ">" & vbCrLf
                        Else
                            strXML &= "<C" & y & ">" & SC(x, y) & "</C" & y & ">" & vbCrLf
                        End If
                    Next
                    strXML &= "</SC>" & vbCrLf
                Next

                strXML &= "<COMENTARIO>" & Comentario & "</COMENTARIO>" & vbCrLf
                strXML &= "</Reporte>" & vbCrLf
            End If

        Catch ex As Exception
            strXML &= "No se encontró reporte crediticio"
        End Try

        Return strXML
    End Function

End Class