Public Class clsErrores
#Region "Variables"
    Private idError As Integer = 0
    Private fcError As String = ""
#End Region

#Region "Propiedades"
    Public Property id_Error() As Integer
        Get
            Return idError
        End Get
        Set(value As Integer)
            idError = value
        End Set
    End Property
    Public Property fc_Error() As String
        Get
            Return fcError
        End Get
        Set(value As String)
            fcError = value
        End Set
    End Property
#End Region

#Region "Metodos"    
    Public Sub New()        
    End Sub

    Public Sub fnObtenError(idError)
        Dim bd As New ProdeskNet.BD.clsManejaBD
        Dim ds As New DataSet
        ds = bd.EjecutarQuery("select fcMensajeError from tbManejaErrores where fiIdError = " & idError)
        fcError = ds.Tables(0).Rows(0)(0).ToString
    End Sub
#End Region
End Class
