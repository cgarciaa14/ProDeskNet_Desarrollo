'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BBV-P-423: AMR: 12/04/2017 RQADM-18 Evaluación

Imports System.Text

Public Class clsActualizaBuro
    Private _strError As String = String.Empty
    Private _IDSECCCERO As Integer = 0
    Private _SCCODSCORE As String = String.Empty
    Private _SCVALSCORE As String = String.Empty

    Private _DICTAMENFINAL As String = String.Empty
    Private _BCSCORE As String = String.Empty
    Private _ICC As String = String.Empty

    Public Property IDSECCCERO As Integer
        Get
            Return _IDSECCCERO
        End Get
        Set(value As Integer)
            _IDSECCCERO = value
        End Set
    End Property

    Public Property SCCODSCORE As String
        Get
            Return _SCCODSCORE
        End Get
        Set(value As String)
            _SCCODSCORE = value
        End Set
    End Property

    Public Property SCVALSCORE As String
        Get
            Return _SCVALSCORE
        End Get
        Set(value As String)
            _SCVALSCORE = value
        End Set
    End Property

    Public Property DICTAMENFINAL As String
        Get
            Return _DICTAMENFINAL
        End Get
        Set(value As String)
            _DICTAMENFINAL = value
        End Set
    End Property

    Public Property BCSCORE As String
        Get
            Return _BCSCORE
        End Get
        Set(value As String)
            _BCSCORE = value
        End Set
    End Property

    Public Property ICC As String
        Get
            Return _ICC
        End Get
        Set(value As String)
            _ICC = value
        End Set
    End Property

    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property

    Sub New()

    End Sub

    Public Function actREPORTE_SCORE(ByVal intsol As Integer, ByVal stricc As String, ByVal strbcc As String, ByVal intusr As Integer) As Integer

        Dim reg As Integer = 0
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder

        sql.AppendLine("UPDATE  PDK_BURO_REPORTE_SCORE SET PDK_BUR_REP_SC_VAL_SCORE = " & "'" & stricc & "', PDK_CLAVE_USUARIO = " & intusr.ToString & ", PDK_BUR_REP_SC_MODIF = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "'")
        sql.AppendLine(" WHERE PDK_ID_SECCCERO = " & intsol.ToString)
        sql.AppendLine(" AND PDK_BUR_REP_SC_COD_SCORE = '004'")
        sql.AppendLine("")
        sql.AppendLine("UPDATE  PDK_BURO_REPORTE_SCORE SET PDK_BUR_REP_SC_VAL_SCORE = " & "'" & strbcc & "', PDK_CLAVE_USUARIO = " & intusr.ToString & ", PDK_BUR_REP_SC_MODIF = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "'")
        sql.AppendLine(" WHERE PDK_ID_SECCCERO = " & intsol.ToString)
        sql.AppendLine(" AND PDK_BUR_REP_SC_COD_SCORE = '007'")

        reg = BD.ExInsUpd(sql.ToString)

        Return reg

    End Function

    Public Function actDICTAMEN_FINAL(ByVal intsol As Integer, ByVal stricc As String, ByVal strbcc As String, ByVal intusr As Integer) As Integer

        Dim reg As Integer = 0
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder

        sql.AppendLine("UPDATE  PDK_TAB_DICTAMEN_FINAL SET BC_SCORE = " & "'" & strbcc & "'" & ", ICC = " & "'" & stricc & "', PDK_CLAVE_USUARIO = " & intusr.ToString & ", PDK_FECHA_MODIF = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "'")
        sql.AppendLine("WHERE PDK_ID_SECCCERO = " & intsol.ToString)

        reg = BD.ExInsUpd(sql.ToString)

        Return reg

    End Function

    Public Function actScore(ByVal intsol As Integer, dbScore As Double) As Integer
        Try
            Dim reg As Integer = 0
            Dim BD As New ProdeskNet.BD.clsManejaBD
            Dim sql As New StringBuilder

            sql.AppendLine("UPDATE PDK_TAB_DICTAMEN_FINAL SET PDK_CAPACIDAD_PAGO = " & dbScore.ToString)
            sql.AppendLine("WHERE PDK_ID_SECCCERO = " & intsol.ToString)

            reg = BD.ExInsUpd(sql.ToString)

            sql.Clear()
            sql.AppendLine("UPDATE PDK_TAB_DATOS_CREDITO SET CAPACIDAD =  " & dbScore.ToString)
            sql.AppendLine("WHERE PDK_ID_SECCCERO = " & intsol.ToString)

            reg = reg + BD.ExInsUpd(sql.ToString)
            Return reg
        Catch ex As Exception
            _strError = ex.Message
            Return Nothing
        End Try
    End Function
End Class
