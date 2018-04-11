#Region "Trackers"
'BBV-P-423 - RQADM-31 22/03/2017 MAPH Cambio de Agencia
'BUG-PD-119:  26/06/2017: ERODRIGUEZ Se agrego clase y función necesarios para guardar un historial de agencias cambiadas por solicitud
#End Region

Imports ProdeskNet.BD

Public Class clsAgencia
    Public Sub New()
    End Sub

#Region "Variables"
    Private _IdAgencia As Integer
    Private _Nombre As String
#End Region

#Region "Propiedades"
    Public Property IdAgencia() As Integer
        Get
            Return _IdAgencia
        End Get
        Set(ByVal value As Integer)
            _IdAgencia = value
        End Set
    End Property
    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(ByVal value As String)
            _Nombre = value
        End Set
    End Property
#End Region

End Class

Public Class clsAgencias
    Inherits List(Of clsAgencia)

    Public Class CambiaAgencia
        Public solictud As Integer
        Public cotizacion As Integer
        Public agencia As Integer
    End Class


#Region "Variables"
    Dim dataManager As New clsManejaBD
    Dim tempResult As New DataSet
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Devuelve las agencias dependiendo de los parámetros seleccionados.
    ''' </summary>
    ''' <param name="idAgencia">Id de agencia opcional</param>
    ''' <param name="Estatus">Estado de la agencia opcional, si es nulo entonces obtiene todos los activos con Estatus=2</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function obtenerAgencia(Optional idAgencia As Integer? = Nothing, Optional Estatus As Integer? = Nothing) As clsAgencias
        dataManager = New clsManejaBD()
        dataManager.AgregaParametro("@ESTATUS", ProdeskNet.BD.TipoDato.Entero, IIf(Estatus Is Nothing, 2, Convert.ToInt32(Estatus)))
        If Not idAgencia Is Nothing Then
            dataManager.AgregaParametro("@PDK_ID_SECCCERO", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(idAgencia))
        End If
        tempResult = dataManager.EjecutaStoredProcedure("getAgencia")
        If Not tempResult Is Nothing And tempResult.Tables.Count > 0 Then
            For Each registro As DataRow In tempResult.Tables(0).Rows
                Me.Add(New clsAgencia() With {.IdAgencia = registro("ID_AGENCIA"), _
                                              .Nombre = registro("Nombre")})
            Next
        End If
        Return Me
    End Function

    ''' <summary>
    ''' Devuelve un DataSet dependiendo de los parámetros especificados:
    ''' </summary>
    ''' <param name="NoSolicitud">Id de la solicitud</param>
    ''' <param name="NombreSolicitante">Nombre de la persona o razón social</param>
    ''' <param name="RFCSolicitante">RFC del solicitante o razón social</param>
    ''' <returns>un DataSet con la siguiente estructura ordenada de Columnas: [ID_SOLICITUD][NOMBRE][RFC][AGENCIA]</returns>
    ''' <remarks></remarks>
    Public Function obtenerTablaAgenciasVendedor(Optional NoSolicitud As Integer? = Nothing, Optional NombreSolicitante As String = Nothing, Optional RFCSolicitante As String = Nothing, Optional NombreAgencia As String = Nothing) As DataSet
        dataManager = New clsManejaBD()
        If Not NoSolicitud Is Nothing Then
            dataManager.AgregaParametro("@PDK_ID_SECCCERO", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(NoSolicitud))
        End If
        If Not NombreSolicitante Is Nothing Then
            dataManager.AgregaParametro("@NOMBRE_SOLICITANTE", ProdeskNet.BD.TipoDato.Cadena, Convert.ToString(NombreSolicitante))
        End If
        If Not RFCSolicitante Is Nothing Then
            dataManager.AgregaParametro("@RFC_SOLICITANTE", ProdeskNet.BD.TipoDato.Cadena, Convert.ToString(RFCSolicitante))
        End If
        If Not RFCSolicitante Is Nothing Then
            dataManager.AgregaParametro("@NOMBRE_AGENCIA", ProdeskNet.BD.TipoDato.Cadena, Convert.ToString(NombreAgencia))
        End If
        tempResult = dataManager.EjecutaStoredProcedure("getDatosCambioAgencia")
        Return tempResult
    End Function

    ''' <summary>
    ''' Actualiza las solicitudes para efectuar cambio de agencia y vendedor
    ''' </summary>
    ''' <param name="NoSolicitudes">Arreglo de enteros</param>
    ''' <returns>Boleano indicando el estado de la actualización</returns>
    ''' <remarks></remarks>
    Public Function cambiarAgenciaSolicitud(ByVal NoSolicitudes As List(Of Integer), ByVal IdNuevaAgencia As Integer, ByVal IdNuevoVendedor As Integer) As Boolean
        Try
            dataManager = New clsManejaBD()
            For Each listElement As Integer In NoSolicitudes
                'NoSolicitudes.ForEach(Function(x) tblNoSolicitudes.Rows.Add(x))
                dataManager.AgregaParametro("@ID_SOLICITUD", ProdeskNet.BD.TipoDato.Entero, listElement)
                dataManager.AgregaParametro("@ID_NUEVA_AGENCIA", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(IdNuevaAgencia))
                dataManager.AgregaParametro("@ID_NUEVO_VENDEDOR", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(IdNuevoVendedor))
                dataManager.EjecutaStoredProcedure("sp_ActualizaAgencia")
            Next
            Return True
        Catch ex As Exception

        End Try
        Return False
    End Function

    ''' <summary>
    ''' Guarda historial de agencias guardadas
    ''' </summary>
    ''' <param name="NoSolicitudes">Arreglo de enteros</param>
    ''' <returns>Boleano indicando el estado de la actualización</returns>
    ''' <remarks></remarks>
    Public Function guardarCambioAgenciaSolicitud(ByVal NoSolicitudes As List(Of clsAgencias.CambiaAgencia), ByVal IdNuevaAgencia As Integer) As Boolean
        Try
            dataManager = New clsManejaBD()
            For Each listElement As clsAgencias.CambiaAgencia In NoSolicitudes
                'NoSolicitudes.ForEach(Function(x) tblNoSolicitudes.Rows.Add(x))
                dataManager.AgregaParametro("@ID_SOLICITUD", ProdeskNet.BD.TipoDato.Entero, listElement.solictud)
                dataManager.AgregaParametro("@ID_COTIZACION", ProdeskNet.BD.TipoDato.Entero, listElement.cotizacion)
                dataManager.AgregaParametro("@ID_AGENCIA_ANT", ProdeskNet.BD.TipoDato.Entero, listElement.agencia)
                dataManager.AgregaParametro("@ID_NUEVA_AGENCIA", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(IdNuevaAgencia))
                dataManager.EjecutaStoredProcedure("sp_GuardaCambiaAgencia")
            Next
            Return True
        Catch ex As Exception

        End Try
        Return False
    End Function

#End Region
End Class
