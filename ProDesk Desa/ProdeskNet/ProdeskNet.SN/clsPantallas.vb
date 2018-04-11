'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BBV-P-423: AMR: 12/04/2017 RQADM-18 Evaluación

Imports System.Text
Public Class clsPantallas

    Private _strError As String = String.Empty
    Private _IDPANTALLAS As Integer = 0
    Private _PANTNOMBRE As String = String.Empty
    Private _PANTORDEN As Integer = 0
    Private _PANTSTATUS As Integer = 0
    Private _PANTMODIF As String = String.Empty
    Private _CLAVEUSUARIO As String = String.Empty
    Private _PANTLINK As String = String.Empty
    Private _PANTMOSTRAR As Integer = 0
    Private _PANTDOCUMENTOS As Integer = 0

    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property

    Public ReadOnly Property IDPANTALLAS As Integer
        Get
            Return _IDPANTALLAS
        End Get
    End Property

    Public Property PANTNOMBRE As String
        Get
            Return _PANTNOMBRE
        End Get
        Set(value As String)
            _PANTNOMBRE = value
        End Set
    End Property

    Public Property PANTORDEN As Integer
        Get
            Return _PANTORDEN
        End Get
        Set(value As Integer)
            _PANTORDEN = value
        End Set
    End Property

    Public Property PANTSTATUS As Integer
        Get
            Return _PANTSTATUS
        End Get
        Set(value As Integer)
            _PANTSTATUS = value
        End Set
    End Property

    Public Property PANTMODIF As String
        Get
            Return _PANTMODIF
        End Get
        Set(value As String)
            _PANTMODIF = value
        End Set
    End Property

    Public Property CLAVEUSUARIO As String
        Get
            Return _CLAVEUSUARIO
        End Get
        Set(value As String)
            _CLAVEUSUARIO = value
        End Set
    End Property

    Public Property PANTLINK As String
        Get
            Return _PANTLINK
        End Get
        Set(value As String)
            _PANTLINK = value
        End Set
    End Property

    Public Property PANTMOSTRAR As Integer
        Get
            Return _PANTMOSTRAR
        End Get
        Set(value As Integer)
            _PANTMOSTRAR = value
        End Set
    End Property

    Public Property PANTDOCUMENTOS As Integer
        Get
            Return _PANTDOCUMENTOS
        End Get
        Set(value As Integer)
            _PANTDOCUMENTOS = value
        End Set
    End Property

    Sub New()

    End Sub

    Public Function CargaPantallas(ByVal intidpant As Integer) As DataSet

        Dim dts As DataSet = New DataSet()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder

        Try

            sql.AppendLine("SELECT PDK_ID_PANTALLAS,  PDK_PANT_NOMBRE, PDK_PANT_ORDEN,  PDK_PANT_STATUS, PDK_PANT_MODIF,")
            sql.AppendLine("PDK_CLAVE_USUARIO, PDK_PANT_LINK, PDK_PANT_MOSTRAR, PDK_PANT_DOCUMENTOS ")
            sql.AppendLine("FROM PDK_PANTALLAS")
            sql.AppendLine("WHERE PDK_ID_PANTALLAS = " & intidpant.ToString)

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

    Public Function SiguientePantalla(ByVal intSol As Integer) As Integer
        Dim dts As DataSet = New DataSet()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder
        Dim cmuestra As Integer = Nothing

        Try

            sql.AppendLine("SELECT C.PDK_PANT_MOSTRAR")
            sql.AppendLine("FROM PDK_TAB_SECCION_CERO A")
            sql.AppendLine("INNER JOIN PDK_REL_PANTALLA_TAREA B")
            sql.AppendLine("ON B.PDK_ID_TAREAS = A.PDK_TAREA_ACTUAL")
            sql.AppendLine("INNER JOIN PDK_PANTALLAS C")
            sql.AppendLine("ON C.PDK_ID_PANTALLAS = B.PDK_ID_PANTALLAS")
            sql.AppendLine("INNER JOIN PDK_OPE_SOLICITUD D")
            sql.AppendLine("ON D.PDK_ID_SOLICITUD =  A.PDK_ID_SECCCERO")
            sql.AppendLine("AND D.PDK_ID_TAREAS = A.PDK_TAREA_ACTUAL")
            sql.AppendLine("WHERE D.PDK_OPE_STATUS_TAREA = 40")
            sql.AppendLine("AND PDK_ID_SECCCERO = " & intSol.ToString)

            dts = BD.EjecutarQuery(sql.ToString)

            If dts.Tables().Count > 0 Then
                If dts.Tables(0).Rows().Count > 0 Then
                    cmuestra = dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR")
                Else
                    _strError = "No existe información."
                    Exit Function
                End If
            Else
                _strError = "No existe información."
                Exit Function
            End If
            Return cmuestra
        Catch ex As Exception
            _strError = "Error al consultar la información."
        End Try
    End Function

    Public Function PantallaAnterior(ByVal intSol As Integer) As DataSet
        Dim dts As DataSet = New DataSet()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder

        Try
            sql.AppendLine("DECLARE @actual TINYINT")
            sql.AppendLine("DECLARE @anterior TINYINT")
            sql.AppendLine("DECLARE @mostrar TINYINT = NULL")
            sql.AppendLine("DECLARE @control TINYINT = 0")
            sql.AppendLine("")
            sql.AppendLine("SELECT	@actual = B.PDK_ID_TAREAS")
            sql.AppendLine("FROM	PDK_CAT_TAREAS A")
            sql.AppendLine("INNER JOIN PDK_OPE_SOLICITUD B")
            sql.AppendLine("ON B.PDK_ID_TAREAS = A.PDK_ID_TAREAS")
            sql.AppendLine("WHERE	B.PDK_OPE_STATUS_TAREA = 40")
            sql.AppendLine("AND		PDK_ID_SOLICITUD = " & intSol.ToString)
            sql.AppendLine("")
            sql.AppendLine("WHILE @mostrar IS NULL")
            sql.AppendLine("BEGIN")
            sql.AppendLine("SELECT	@mostrar = PDK_PANT_MOSTRAR,")
            sql.AppendLine("@anterior = PDK_ID_TAREAS_RECHAZO")
            sql.AppendLine("FROM	PDK_CAT_TAREAS A")
            sql.AppendLine("INNER JOIN PDK_REL_PANTALLA_TAREA B ")
            sql.AppendLine("ON B.PDK_ID_TAREAS = A.PDK_ID_TAREAS")
            sql.AppendLine("INNER JOIN PDK_PANTALLAS C")
            sql.AppendLine("ON C.PDK_ID_PANTALLAS = B.PDK_ID_PANTALLAS")
            sql.AppendLine("WHERE A.PDK_ID_TAREAS = @actual")
            sql.AppendLine("")
            sql.AppendLine("IF @mostrar <> 2")
            sql.AppendLine("BEGIN")
            sql.AppendLine("SET @mostrar = NULL")
            sql.AppendLine("SET @actual = @anterior")
            sql.AppendLine("END")
            sql.AppendLine("")
            sql.AppendLine("SET @control = @control + 1")
            sql.AppendLine("IF @control > (SELECT COUNT(*) FROM PDK_CAT_TAREAS)")
            sql.AppendLine("BEGIN")
            sql.AppendLine("SET @actual = 0")
            sql.AppendLine("BREAK")
            sql.AppendLine("END")
            sql.AppendLine("END")
            sql.AppendLine("SELECT @actual AS PDK_ID_TAREAS, @mostrar AS PDK_PANT_MOSTRAR")

            dts = BD.EjecutarQuery(sql.ToString)

            If dts.Tables().Count > 0 Then
                If dts.Tables(0).Rows().Count > 0 Then
                    Return dts
                Else
                    _strError = "No existe información."
                    Exit Function
                End If
            Else
                _strError = "No existe información."
                Exit Function
            End If

        Catch ex As Exception
            _strError = "Error al consultar la información."
        End Try
    End Function

End Class
