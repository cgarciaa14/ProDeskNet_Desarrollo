#Region "Trackers"
'BUG-PD-217: ERODRIGUEZ: 02/10/2017:  Bitacora turnados de mensajes de red.
#End Region
Imports System.Text
Imports System.Data

Public Class clsMensajesRedBitacora
#Region "Variables"

    Private intIdTurnado As Integer = 0
    Private intIdTareaActual As Integer = 0
    Private intTareaTurnado As Integer = 0
    Private dateFechaTurnado As Date
    Private intIdUsuario As String = ""
    Private intEstatus As Integer = 0
    Private intIdSolicitado As Integer = 0

    Private strmensaje As String = String.Empty
    Private strErrSolicitud As String = String.Empty


#End Region
#Region "Propiedades"


    Public Property ID_TURNADO() As Integer
        Get
            Return intIdTurnado
        End Get
        Set(ByVal value As Integer)
            intIdTurnado = value
        End Set
    End Property

    Public Property ID_TAREA_ACTUAL() As Integer
        Get
            Return intIdTareaActual
        End Get
        Set(ByVal value As Integer)
            intIdTareaActual = value
        End Set
    End Property

    Public Property ID_TAREA_TURNADO() As Integer
        Get
            Return intTareaTurnado
        End Get
        Set(ByVal value As Integer)
            intTareaTurnado = value
        End Set
    End Property
    Public Property FECHA_TURNADO() As Date
        Get
            Return dateFechaTurnado
        End Get
        Set(ByVal value As Date)
            dateFechaTurnado = value
        End Set
    End Property

    Public Property ID_USUARIO() As Integer
        Get
            Return intIdUsuario
        End Get
        Set(ByVal value As Integer)
            intIdUsuario = value
        End Set
    End Property

    Public Property ESTATUS() As Integer
        Get
            Return intEstatus
        End Get
        Set(ByVal value As Integer)
            intEstatus = value
        End Set
    End Property

    Public Property MENSAJES() As String
        Get
            Return strmensaje
        End Get
        Set(ByVal value As String)
            strmensaje = value
        End Set
    End Property


    Public Property ID_SOLICITUD As Integer
        Get
            Return intIdSolicitado
        End Get
        Set(ByVal value As Integer)
            intIdSolicitado = value
        End Set
    End Property

#End Region


#Region "Metodos"

    Public Function ManejaBitacoraMsjRed(ByVal intOper As Integer) As DataSet
        Dim MB As New ProdeskNet.BD.clsManejaBD
        Try

            strErrSolicitud = ""

            Select Case intOper
                Case 1 'CONSULTA
                    MB.AgregaParametro("@OPCION", ProdeskNet.BD.TipoDato.Entero, intOper)
                    MB.AgregaParametro("@Id_Solicitud", ProdeskNet.BD.TipoDato.Entero, intIdSolicitado)
                Case 2 'Inserta
                    MB.AgregaParametro("@OPCION", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intIdSolicitado > 0 Then MB.AgregaParametro("@Id_Solicitud", ProdeskNet.BD.TipoDato.Entero, intIdSolicitado)
                    If intTareaTurnado > 0 Then MB.AgregaParametro("@Id_Tarea_Turnado", ProdeskNet.BD.TipoDato.Entero, intTareaTurnado)
                    If intIdUsuario > 0 Then MB.AgregaParametro("@Id_Usuario", ProdeskNet.BD.TipoDato.Entero, intIdUsuario)
                    If intEstatus >= 0 Then MB.AgregaParametro("@Estatus", ProdeskNet.BD.TipoDato.Entero, intEstatus)
                Case 3 'Confirma guardado
                    MB.AgregaParametro("@OPCION", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intIdTurnado > 0 Then MB.AgregaParametro("@id_turnado", ProdeskNet.BD.TipoDato.Entero, intIdTurnado)
            End Select

            ManejaBitacoraMsjRed = MB.EjecutaStoredProcedure("sp_ManejaBitacoraMsjRed")

            If (MB.ErrorBD) <> "" Then
                strErrSolicitud = MB.ErrorBD
            End If
        Catch ex As Exception
            strErrSolicitud = ex.Message + " " + strErrSolicitud
        End Try
    End Function

#End Region
End Class
