
Imports Microsoft.VisualBasic
Imports System.Data
Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.IO


Public Class ClienteVPN
#Region "Variables"
    Private _Stm As Stream
    Private _IPHost As String
    Private _Puerto As String
#End Region

#Region "Eventos"
    Public Event CierraConexion()
    Public Event Respuesta(ByVal XML As String, ByRef respuesta As Boolean)
#End Region
#Region "Propiedades"
    Public Property IPHost() As String
        Get
            IPHost = _IPHost
        End Get
        Set(ByVal value As String)
            _IPHost = value
        End Set
    End Property
    Public Property Puerto()
        Get
            Puerto = _Puerto
        End Get
        Set(ByVal value)
            _Puerto = value
        End Set
    End Property
#End Region
#Region "Metodos"
    Public Sub Conectar()
        Dim tcpClt As TcpClient
        Dim tcpThd As Thread
        Try
            tcpClt = New TcpClient()
            tcpClt.Connect(IPHost, Puerto)
            _Stm = tcpClt.GetStream()
            _Stm.ReadTimeout = 10000
            tcpThd = New Thread(AddressOf leerSocket)
            tcpThd.Start()
        Catch ex As Exception
            Exit Sub
        End Try

    End Sub
    Public Sub Consulta(ByVal XML As String)
        Try
            Dim BufferEcritura() As Byte
            BufferEcritura = Encoding.ASCII.GetBytes(XML)

            Dim tamm As Integer = BufferEcritura.Length + 1

            ReDim Preserve BufferEcritura(BufferEcritura.Length)
            BufferEcritura(BufferEcritura.Length - 1) = 19

            If Not (_Stm Is Nothing) Then
                _Stm.Write(BufferEcritura, 0, BufferEcritura.Length)
            End If
        Catch ex As Exception
            XML = "Error"
            Exit Sub
        End Try

    End Sub
#End Region
#Region "Funciones Privadas"
    Private Sub leerSocket()
        Dim BufferLectura() As Byte
        Dim texto As String = ""
        Dim resp As Boolean = False
        Dim n As Integer

        While True
            Try

                BufferLectura = New Byte(20000) {}

                n = _Stm.Read(BufferLectura, 0, BufferLectura.Length)

                If n = 0 Then
                    RaiseEvent Respuesta(texto, resp)
                    RaiseEvent CierraConexion()
                    RaiseEvent CierraConexion()
                    Exit Sub
                End If


                If BufferLectura(0) = 0 Then
                    RaiseEvent Respuesta(texto, resp)
                Else
                    For i As Integer = 0 To BufferLectura.Length - 1
                        If BufferLectura(i) = 0 Then
                            ReDim Preserve BufferLectura(i - 1)
                            Exit For
                        End If
                    Next

                    texto = texto.ToString + Encoding.ASCII.GetString(BufferLectura).ToString

                    RaiseEvent CierraConexion()

                    If BufferLectura(BufferLectura.Length - 4) = 50 And BufferLectura(BufferLectura.Length - 3) = 42 And BufferLectura(BufferLectura.Length - 2) = 42 And BufferLectura(BufferLectura.Length - 1) = 19 Then
                        RaiseEvent Respuesta(texto, resp)
                        Exit While
                    End If

                End If

            Catch ex As Exception

                RaiseEvent Respuesta(texto, resp)
                Exit While
            End Try
        End While
        RaiseEvent CierraConexion()
    End Sub
#End Region

End Class
