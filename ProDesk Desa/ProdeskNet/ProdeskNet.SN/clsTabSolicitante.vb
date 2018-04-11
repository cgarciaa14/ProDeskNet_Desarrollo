'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)

Imports System.Text

Public Class clsTabSolicitante

    Private _strError As String = String.Empty
    Private _IDSOLICITANTE As Integer = 0
    Private _IDSECCCERO As Integer = 0
    Private _NOMBRE1 As String = String.Empty
    Private _NOMBRE2 As String = String.Empty
    Private _PATERNO As String = String.Empty
    Private _MATERNO As String = String.Empty
    Private _RFC As String = String.Empty
    Private _TELPARTI As Integer = 0
    Private _TELMOVIL As Integer = 0
    Private _EMAIL As String = String.Empty
    Private _EDAD As Integer = 0
    Private _FECHAMOD As String = String.Empty
    Private _CVEUSUARIO As Integer = 0
    Private _VISITARBURO As String = String.Empty
    Private _ANIONAC As Integer = 0
    Private _MESNAC As Integer = 0
    Private _DIANAC As Integer = 0
    Private _HOMOCLAVE As String = String.Empty


    Sub New()
    End Sub

    Public ReadOnly Property ErrorSolicitante
        Get
            Return _strError
        End Get
    End Property

    Public ReadOnly Property IDSOLICITANTE As Integer
        Get
            Return _IDSOLICITANTE
        End Get
    End Property

    Public Property IDSECCCERO As Integer
        Get
            Return _IDSECCCERO
        End Get
        Set(value As Integer)
            _IDSECCCERO = value
        End Set
    End Property

    Public Property NOMBRE1 As String
        Get
            Return _NOMBRE1
        End Get
        Set(value As String)
            _NOMBRE1 = value
        End Set
    End Property

    Public Property NOMBRE2 As String
        Get
            Return _NOMBRE2
        End Get
        Set(value As String)
            _NOMBRE2 = value
        End Set
    End Property

    Public Property PATERNO As String
        Get
            Return _PATERNO
        End Get
        Set(value As String)
            _PATERNO = value
        End Set
    End Property

    Public Property MATERNO As String
        Get
            Return _MATERNO
        End Get
        Set(value As String)
            _MATERNO = value
        End Set
    End Property

    Public Property RFC As String
        Get
            Return _RFC
        End Get
        Set(value As String)
            _RFC = value
        End Set
    End Property

    Public Property TELPARTI As Integer
        Get
            Return _TELPARTI
        End Get
        Set(value As Integer)
            _TELPARTI = value
        End Set
    End Property

    Public Property TELMOVIL As Integer
        Get
            Return _TELMOVIL
        End Get
        Set(value As Integer)
            _TELMOVIL = value
        End Set
    End Property

    Public Property EMAIL As String
        Get
            Return _EMAIL
        End Get
        Set(value As String)
            _EMAIL = value
        End Set
    End Property

    Public Property EDAD As Integer
        Get
            Return _EDAD
        End Get
        Set(value As Integer)
            _EDAD = value
        End Set
    End Property

    Public Property FECHAMOD As String
        Get
            Return _FECHAMOD
        End Get
        Set(value As String)
            _FECHAMOD = value
        End Set
    End Property

    Public Property CVEUSUARIO As Integer
        Get
            Return _CVEUSUARIO
        End Get
        Set(value As Integer)
            _CVEUSUARIO = value
        End Set
    End Property

    Public Property VISITARBURO As String
        Get
            Return _VISITARBURO
        End Get
        Set(value As String)
            _VISITARBURO = value
        End Set
    End Property

    Public Property ANIONAC As Integer
        Get
            Return _ANIONAC
        End Get
        Set(value As Integer)
            _ANIONAC = value
        End Set
    End Property

    Public Property MESNAC As Integer
        Get
            Return _MESNAC
        End Get
        Set(value As Integer)
            _MESNAC = value
        End Set
    End Property

    Public Property DIANAC As Integer
        Get
            Return _DIANAC
        End Get
        Set(value As Integer)
            _DIANAC = value
        End Set
    End Property

    Public Property HOMOCLAVE As String
        Get
            Return _HOMOCLAVE
        End Get
        Set(value As String)
            _HOMOCLAVE = value
        End Set
    End Property

    Public Function CargaSolicitante(ByVal intSol As Integer) As DataSet

        Dim dts As DataSet = New DataSet()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder

        Try

            sql.AppendLine("SELECT PDK_ID_SOLICITANTE, PDK_ID_SECCCERO, NOMBRE1, NOMBRE2,APELLIDO_PATERNO,APELLIDO_MATERNO,")
            sql.AppendLine("RFC, TELEFONO_PARTI, TELEFONO_MOVIL, CORREO_ELECTRONICO, EDAD, PDK_FECHA_MODIF, PDK_CLAVE_USUARIO,")
            sql.AppendLine("VISITAR_BURO, ANO_NACIMIENTO, MES_NACIMIENTO, DIA_NACIMIENTO, HOMOCLAVE")
            sql.AppendLine("FROM PDK_TAB_SOLICITANTE")
            sql.AppendLine("WHERE PDK_ID_SECCCERO = " & intSol.ToString)

            dts = BD.EjecutarQuery(sql.ToString)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    Return dts
                Else
                    _strError = "Error al consultar la información."
                    Return Nothing
                End If
            Else
                _strError = "Error al consultar la información."
                Return Nothing
            End If

        Catch ex As Exception
            _strError = "Exception: Error al cargar la información."
            Return Nothing
        End Try
    End Function
End Class
