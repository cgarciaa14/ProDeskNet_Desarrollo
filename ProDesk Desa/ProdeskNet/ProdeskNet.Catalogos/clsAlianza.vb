#Region "trackers"
'BBV-P-423-RQADM-32:MPUESTO:20/04/2017:Retrabajos Ingresos 29 Creación de flujo alterno
'BUG-PD-44:MPUESTO:10/05/2017:Corrección de nombre de Stored Procedure
'BUG-PD-49:MPUESTO:12/05/2017:Corrección Retrabajos Ingresos
#End Region

Imports ProdeskNet.BD

Public Class clsAlianza

#Region "Variables"
    Private _PDK_TAREA_ACTUAL As Integer
    Private _ID_COTIZACION As Integer
    Private _MONTO_FINANCIAMIENTO As Decimal
    Private _PRECIO_PRODUCTO As Decimal
    Private _MONTO_ACCESORIOS As Decimal
    Private _MONTO_SEGURO As Decimal
    Private _ENGANCHE As Decimal
    Private _MONTO_SUBSIDIO As Decimal
    Private _ID_ALIANZA As Integer
    Private _ALIANZA As String
    Private _ES_MULTIMARCA As Boolean
#End Region

#Region "Properties"
    Public Property PDK_TAREA_ACTUAL() As Integer
        Get
            Return _PDK_TAREA_ACTUAL
        End Get
        Set(ByVal value As Integer)
            _PDK_TAREA_ACTUAL = value
        End Set
    End Property

    Public Property ID_COTIZACION() As Integer
        Get
            Return _ID_COTIZACION
        End Get
        Set(ByVal value As Integer)
            _ID_COTIZACION = value
        End Set
    End Property

    Public Property MONTO_FINANCIAMIENTO() As Decimal
        Get
            Return _MONTO_FINANCIAMIENTO
        End Get
        Set(ByVal value As Decimal)
            _MONTO_FINANCIAMIENTO = value
        End Set
    End Property

    Public Property PRECIO_PRODUCTO() As Decimal
        Get
            Return _PRECIO_PRODUCTO
        End Get
        Set(ByVal value As Decimal)
            _PRECIO_PRODUCTO = value
        End Set
    End Property

    Public Property MONTO_ACCESORIOS() As Decimal
        Get
            Return _MONTO_ACCESORIOS
        End Get
        Set(ByVal value As Decimal)
            _MONTO_ACCESORIOS = value
        End Set
    End Property

    Public Property MONTO_SEGURO() As Decimal
        Get
            Return _MONTO_SEGURO
        End Get
        Set(ByVal value As Decimal)
            _MONTO_SEGURO = value
        End Set
    End Property

    Public Property ENGANCHE() As Decimal
        Get
            Return _ENGANCHE
        End Get
        Set(ByVal value As Decimal)
            _ENGANCHE = value
        End Set
    End Property

    Public Property MONTO_SUBSIDIO() As Decimal
        Get
            Return _MONTO_SUBSIDIO
        End Get
        Set(ByVal value As Decimal)
            _MONTO_SUBSIDIO = value
        End Set
    End Property

    Public Property ID_ALIANZA() As Integer
        Get
            Return _ID_ALIANZA
        End Get
        Set(ByVal value As Integer)
            _ID_ALIANZA = value
        End Set
    End Property

    Public Property ALIANZA() As String
        Get
            Return _ALIANZA
        End Get
        Set(ByVal value As String)
            _ALIANZA = value
        End Set
    End Property

    Public Property ES_MULTIMARCA() As Boolean
        Get
            Return _ES_MULTIMARCA
        End Get
        Set(ByVal value As Boolean)
            _ES_MULTIMARCA = value
        End Set
    End Property

#End Region

End Class

Public Class clsAlianzas
    Inherits List(Of clsAlianza)

#Region "Variables"
    Dim dataManager As New clsManejaBD
    Dim tempResult As New DataSet
#End Region

    Public Sub ObtenerAlianzas(Optional idSolicitud As Integer? = Nothing, Optional idTareaActual As Integer? = Nothing)
        dataManager = New clsManejaBD()

        If Not idSolicitud Is Nothing Then
            dataManager.AgregaParametro("@PDK_ID_SECCCERO", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(idSolicitud))
        End If
        If Not idTareaActual Is Nothing Then
            dataManager.AgregaParametro("@PDK_TAREA_ACTUAL", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(idTareaActual))
        End If

        tempResult = dataManager.EjecutaStoredProcedure("getDatosRetrabajoIngreso")

        If Not tempResult.Tables Is Nothing And tempResult.Tables.Count > 0 Then
            For Each itemRow As DataRow In tempResult.Tables(0).Rows
                Me.Add(New clsAlianza With { _
                        .PDK_TAREA_ACTUAL = Convert.ToInt32(itemRow("PDK_TAREA_ACTUAL").ToString()), _
                        .ID_COTIZACION = Convert.ToInt32(itemRow("ID_COTIZACION").ToString()), _
                        .MONTO_FINANCIAMIENTO = Convert.ToDecimal(itemRow("MONTO_FINANCIAMIENTO").ToString()), _
                        .PRECIO_PRODUCTO = Convert.ToDecimal(itemRow("PRECIO_PRODUCTO").ToString()), _
                        .MONTO_ACCESORIOS = Convert.ToDecimal(itemRow("MONTO_ACCESORIOS").ToString()), _
                        .MONTO_SEGURO = Convert.ToDecimal(itemRow("MONTO_SEGURO").ToString()), _
                        .ENGANCHE = Convert.ToDecimal(itemRow("ENGANCHE").ToString()), _
                        .MONTO_SUBSIDIO = Convert.ToDecimal(itemRow("MONTO_SUBSIDIO").ToString()), _
                        .ID_ALIANZA = Convert.ToInt32(itemRow("ID_ALIANZA").ToString()), _
                        .ALIANZA = itemRow("ALIANZA").ToString(), _
                        .ES_MULTIMARCA = Convert.ToBoolean(itemRow("ES_MULTIMARCA").ToString()) _
                      })
            Next
        End If

    End Sub
End Class