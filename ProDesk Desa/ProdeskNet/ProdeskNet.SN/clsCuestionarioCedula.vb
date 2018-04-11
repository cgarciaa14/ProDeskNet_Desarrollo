'BBV-P-423-RQADM-09 JBB 04/04/2017 Pantalla Consulta cedula. 
Imports ProdeskNet.BD
Public Class clsCuestionarioCedula

    Private _strError As String = String.Empty
    Private _ID_SOLICITUD As Integer = 0
    Private _CEDULA As Integer = 0
    Private _NUMERO As Integer = 0
    Private _NOMBRE As Integer = 0

    Public Property strError As String
        Get
            Return _strError

        End Get
        Set(value As String)
            _strError = value
        End Set
    End Property
    Public Property ID_SOLICITUD As Integer
        Get
            Return _ID_SOLICITUD
        End Get
        Set(value As Integer)
            _ID_SOLICITUD = value
        End Set
    End Property
    Public Property CEDULA As Integer
        Get
            Return _CEDULA
        End Get
        Set(value As Integer)
            _CEDULA = value
        End Set
    End Property

    Public Property NUMERO As Integer
        Get
            Return _NUMERO
        End Get
        Set(value As Integer)
            _NUMERO = value
        End Set
    End Property
    Public Property NOMBRE As Integer
        Get
            Return _NOMBRE
        End Get
        Set(value As Integer)
            _NOMBRE = value
        End Set
    End Property
    Public Function insertaDatosCedula() As Boolean
        insertaDatosCedula = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            BD.AgregaParametro("@id_solicitud", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@cedula", TipoDato.Entero, CEDULA)
            BD.AgregaParametro("@numeroced", TipoDato.Entero, NUMERO)
            BD.AgregaParametro("@nombrelic", TipoDato.Entero, NOMBRE)

            dsres = BD.EjecutaStoredProcedure("spInsertaDatosCedula")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        insertaDatosCedula = True
                    Else
                        insertaDatosCedula = False
                        Throw New Exception(dsres.Tables(0).Rows(0).Item("RESULT").ToString())
                    End If
                Else
                    Throw New Exception("Falla al guardar Cuestionario")
                End If
            Else
                Throw New Exception("Falla al guardar Cuestionario")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return False
        End Try
    End Function
    Public Function GetCuestionarioCedula() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_SOLICITUD)

            dsres = BD.EjecutaStoredProcedure("SpGetCuestionarioCedula")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                Else
                    Throw New Exception("No se encontraron datos del cuestionario Facebook")
                End If
            Else
                Throw New Exception("Falla al consultar cuestionario Facebook")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function
End Class
