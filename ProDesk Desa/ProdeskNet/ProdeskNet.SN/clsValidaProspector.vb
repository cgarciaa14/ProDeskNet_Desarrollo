'BBV-P-423 RQADM-04 ERV 20/04/2017 Se creo la clase para VALIDA_PROSPECTOR
Imports ProdeskNet.BD

Public Class clsValidaProspector
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



    Public Function Validacion_Alianza(ByVal idsol As Integer) As DataSet

        Dim dsres As DataSet = New DataSet
        Dim BD As New clsManejaBD
        BD.AgregaParametro("@PDK_ID_SECCCERO", TipoDato.Entero, idsol)
        Validacion_Alianza = BD.EjecutaStoredProcedure("getAgenciaBySol")
        If (BD.ErrorBD) <> "" Then
            StrErrorValidacion = BD.ErrorBD
        End If
        Return Validacion_Alianza
    End Function

End Class