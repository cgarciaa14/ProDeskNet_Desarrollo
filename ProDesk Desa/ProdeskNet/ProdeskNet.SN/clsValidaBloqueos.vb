Imports ProdeskNet.BD
'BBV-P-423: 04/01/17: JRHM RQCONYFOR-06 Se creo nueva clase para la validacion para validar si es necesario validar si la agencia esta bloqueada
Public Class clsValidaBloqueos
    Private _ID_SOLICITUD As Integer = 0
    Private StrErrorValidacion As String = ""
    Sub New()
    End Sub

    Public Property ID_SOLICITUD As Integer
        Get
            Return _ID_SOLICITUD
        End Get
        Set(ByVal value As Integer)
            _ID_SOLICITUD = value
        End Set
    End Property
    Public Property Cad_Error As String
        Get
            Return StrErrorValidacion
        End Get
        Set(ByVal value As String)
            StrErrorValidacion = value
        End Set
    End Property



    Public Function Validacion_Agencia(ByVal idsol As Integer) As DataSet

        Dim dsres As DataSet = New DataSet
        Dim BD As New clsManejaBD
        BD.AgregaParametro("@idsolicitud", TipoDato.Entero, idsol)
        Validacion_Agencia = BD.EjecutaStoredProcedure("spValidaBloqueoAgencia")
        If (BD.ErrorBD) <> "" Then
            StrErrorValidacion = BD.ErrorBD
        End If
        Return Validacion_Agencia
    End Function

End Class