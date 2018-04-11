'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BUG-PD-26 MAPH 12/04/2017 Cambios solicitados por AMATA
'BUG-PD-37 JBB 26/04/2017 Se obtiene el pdk_pant_mostrar de siguiente tara para forzajes de admision 
'RQ-PD-26 JMENDIETA: 26/02/2018 En el query del metodo SiguienteTarea se agrega el campo PDK_PANT_MIRROR_LINK

Imports System.Text


Public Class clsCatTareas

    Private _strError As String = String.Empty
    Private _IDSolicitud As Integer = 0

    Sub New()

    End Sub

    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property

    Public Property IDSolicitud As Integer
        Get
            Return _IDSolicitud
        End Get
        Set(value As Integer)
            _IDSolicitud = value
        End Set
    End Property

    Public Function SiguienteTarea(ByVal intSol As Integer) As DataSet
        Dim dts As DataSet = New DataSet()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder

        Try
            sql.AppendLine("SELECT A.PDK_ID_PANTALLAS, LOWER(SUBSTRING(PDK_PANT_LINK,6,LEN(PDK_PANT_LINK))) AS PDK_PANT_LINK,PDK_PANT_MOSTRAR,PDK_PANT_MIRROR_LINK")
            sql.AppendLine("FROM PDK_PANTALLAS A")
            sql.AppendLine("INNER JOIN PDK_REL_PANTALLA_TAREA B ON B.PDK_ID_PANTALLAS = A.PDK_ID_PANTALLAS")
            sql.AppendLine("INNER JOIN PDK_CAT_TAREAS C ON C.PDK_ID_TAREAS = B.PDK_ID_TAREAS")
            sql.AppendLine("INNER JOIN (SELECT	PDK_ID_TAREAS ")
            sql.AppendLine("FROM	PDK_OPE_SOLICITUD ")
            sql.AppendLine("WHERE PDK_ID_OPE_SOLICITUD =(SELECT	MAX(PDK_ID_OPE_SOLICITUD)  AS PDK_ID_OPE_SOLICITUD")
            sql.AppendLine("FROM	PDK_OPE_SOLICITUD ")
            sql.AppendLine("WHERE PDK_ID_SOLICITUD = " & intSol.ToString & ")")
            sql.AppendLine("AND PDK_ID_SOLICITUD = " & intSol.ToString & ") D ")
            sql.AppendLine("ON D.PDK_ID_TAREAS = C.PDK_ID_TAREAS")

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
            _strError = "Error al cargar la información."
        End Try
    End Function

    Public Function RegresaTarea(ByVal intSol As Integer, ByVal intusr As Integer) As Integer
        Dim reg As Integer = 0
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder

        sql.AppendLine("DELETE Z")
        sql.AppendLine("FROM PDK_OPE_SOLICITUD Z")
        sql.AppendLine("LEFT JOIN(")
        sql.AppendLine("SELECT B.PDK_ID_OPE_SOLICITUD, B.PDK_ID_SOLICITUD, A.PDK_TAREA_ACTUAL, PDK_ID_TAREAS_RECHAZO, B.PDK_OPE_STATUS_TAREA")
        sql.AppendLine("FROM PDK_TAB_SECCION_CERO A")
        sql.AppendLine("INNER JOIN PDK_OPE_SOLICITUD B")
        sql.AppendLine("ON B.PDK_ID_SOLICITUD = A.PDK_ID_SECCCERO ")
        sql.AppendLine("AND B.PDK_ID_TAREAS = A.PDK_TAREA_ACTUAL")
        sql.AppendLine("INNER JOIN PDK_CAT_TAREAS C ON C.PDK_ID_TAREAS = B.PDK_ID_TAREAS")
        sql.AppendLine("WHERE B.PDK_OPE_STATUS_TAREA = 40")
        sql.AppendLine("AND PDK_ID_SECCCERO = " & intSol.ToString & ") X")
        sql.AppendLine("ON X.PDK_ID_SOLICITUD = Z.PDK_ID_SOLICITUD")
        sql.AppendLine("AND X.PDK_ID_TAREAS_RECHAZO = Z.PDK_ID_TAREAS")
        sql.AppendLine("WHERE Z.PDK_ID_SOLICITUD = " & intSol.ToString)
        sql.AppendLine("AND X.PDK_ID_SOLICITUD IS NULL")
        sql.AppendLine("AND Z.PDK_ID_TAREAS <> 1")
        reg = BD.ExInsUpd(sql.ToString)

        If reg > 0 Then
            reg = 0
            sql.Clear()
        Else
            _strError = "Error al actualizar la información"
            Exit Function
        End If


        sql.AppendLine("UPDATE A SET PDK_OPE_STATUS_TAREA = 40, PDK_OPE_STATUS_PROCESO = 40")
        sql.AppendLine("FROM PDK_OPE_SOLICITUD A")
        sql.AppendLine("INNER JOIN(")
        sql.AppendLine("SELECT MAX(PDK_ID_OPE_SOLICITUD) AS PDK_ID_OPE_SOLICITUD")
        sql.AppendLine("FROM PDK_OPE_SOLICITUD WHERE PDK_ID_SOLICITUD = " & intSol.ToString & ") B")
        sql.AppendLine("ON B.PDK_ID_OPE_SOLICITUD = A.PDK_ID_OPE_SOLICITUD")
        reg = BD.ExInsUpd(sql.ToString)

        If reg = 0 Then
            _strError = "Error al actualizar la información"
            Exit Function
        End If

        Return reg
    End Function

End Class