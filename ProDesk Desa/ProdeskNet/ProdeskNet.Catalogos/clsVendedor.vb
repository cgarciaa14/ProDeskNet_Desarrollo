#Region "Trackers"
'BBV-P-423 - RQADM-31 22/03/2017 MAPH Cambio de Agencia
#End Region

Public Class clsVendedor
    Public Sub New()

    End Sub

#Region "Variables"
    Private _IdVendedor As Integer
    Private _Nombre As String
#End Region

#Region "Propiedades"
    Public Property IdVendedor() As Integer
        Get
            Return _IdVendedor
        End Get
        Set(ByVal value As Integer)
            _IdVendedor = value
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

Public Class clsVendedores
    Inherits List(Of clsVendedor)
#Region "Variables"
    Dim dataManager As New ProdeskNet.BD.clsManejaBD
    Dim tempResult As New DataSet
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Devuelve los vendedores de una agencia
    ''' </summary>
    ''' <param name="idAgencia">Identificador de la agencia</param>
    ''' <param name="idVendedor">Identificador del vendedor</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function obtenerVendedor(ByVal idAgencia As Integer, Optional idVendedor As Integer? = Nothing, Optional EstatusVendedor As Integer? = Nothing, Optional EstatusPerfil As Integer? = Nothing) As clsVendedores
        dataManager.AgregaParametro("@PDK_ID_SECCCERO", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(idAgencia))
        If Not idVendedor Is Nothing Then
            dataManager.AgregaParametro("@ID_VENDEDOR", ProdeskNet.BD.TipoDato.Entero, Convert.ToInt32(idVendedor))
        End If
        dataManager.AgregaParametro("@ESTATUS_VENDEDOR", ProdeskNet.BD.TipoDato.Entero, IIf(EstatusVendedor Is Nothing, 2, Convert.ToInt32(EstatusVendedor)))
        dataManager.AgregaParametro("@ESTATUS_PERFIL", ProdeskNet.BD.TipoDato.Entero, IIf(EstatusPerfil Is Nothing, 2, Convert.ToInt32(EstatusPerfil)))

        tempResult = dataManager.EjecutaStoredProcedure("getVendedor")
        If Not tempResult Is Nothing And tempResult.Tables.Count > 0 Then
            For Each registro As DataRow In tempResult.Tables(0).Rows
                Me.Add(New clsVendedor() With {.IdVendedor = registro("ID_VENDEDOR"), _
                                              .Nombre = registro("NOMBRE")})
            Next
        End If
        Return Me
    End Function
#End Region
End Class