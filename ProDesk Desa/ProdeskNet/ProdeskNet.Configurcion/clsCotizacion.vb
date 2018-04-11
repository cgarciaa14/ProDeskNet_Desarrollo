Imports System.Text
Imports System.Data
Public Class clsCotizacion

    Dim BD As New ProdeskNet.BD.clsManejaBD    

#Region "variables"

    Private intId_Cotizacion As Integer = 0
    Private intId_Producto As Integer = 0
    Private strProducto_Descripcion As String = String.Empty
    Private intId_Paquete As Integer = 0
    Private strPaquete_Nombre As String = String.Empty
    Private strMensajeError As String = String.Empty

#End Region

#Region "propiedades"
    Public Property Id_Cotizacion() As Integer
        Get
            Return intId_Cotizacion
        End Get
        Set(value As Integer)
            intId_Cotizacion = value
        End Set
    End Property

    Public Property Id_Producto() As Integer
        Get
            Return intId_Producto
        End Get
        Set(value As Integer)
            intId_Producto = value
        End Set
    End Property

    Public ReadOnly Property Producto_Descripcion() As String
        Get
            Return strProducto_Descripcion
        End Get
    End Property

    Public Property Id_Paquete() As Integer
        Get
            Return intId_Paquete
        End Get
        Set(value As Integer)
            intId_Paquete = value
        End Set
    End Property

    Public ReadOnly Property Paquete_Nombre() As String
        Get
            Return strPaquete_Nombre
        End Get
    End Property


#End Region

#Region "metodos"
    Public Sub getCotizacion()

        Try
            Dim dsCotizacion As New DataSet
            Dim strSQL As New StringBuilder

            strSQL.Append(" select * ")
            strSQL.Append(" from cotizaciones ")
            strSQL.Append(" where id_cotizacion = " & Id_Cotizacion)
            dsCotizacion = BD.EjecutarQuery(strSQL.ToString, 1)


            With dsCotizacion.Tables(0).Rows(0)
                intId_Producto = .Item("id_producto")
                intId_Paquete = .Item("id_paquete")
            End With
        Catch ex As Exception
            strMensajeError = ex.Message
        End Try

    End Sub

    Public Sub getProducto()

        Try
            Dim dsProducto As New DataSet
            Dim strSQL As New StringBuilder

            strSQL.Append(" select * ")
            strSQL.Append(" from productos ")
            strSQL.Append(" where id_producto = " & Id_Producto)
            dsProducto = BD.EjecutarQuery(strSQL.ToString, 1)

            With dsProducto.Tables(0).Rows(0)
                strProducto_Descripcion = .Item("descripcion")
            End With
        Catch ex As Exception
            strMensajeError = ex.Message
        End Try

    End Sub

    Public Sub getPaquete()

        Try
            Dim dsPaquete As New DataSet
            Dim strSQL As New StringBuilder

            strSQL.Append(" select * ")
            strSQL.Append(" from paquetes ")
            strSQL.Append(" where id_paquete = " & Id_Paquete)
            dsPaquete = BD.EjecutarQuery(strSQL.ToString, 1)

            With dsPaquete.Tables(0).Rows(0)
                strPaquete_Nombre = .Item("nombre")
            End With
        Catch ex As Exception
            strMensajeError = ex.Message
        End Try        

    End Sub

#End Region

End Class
