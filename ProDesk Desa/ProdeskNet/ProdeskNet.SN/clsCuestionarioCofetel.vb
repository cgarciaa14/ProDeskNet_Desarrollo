Imports ProdeskNet.BD
'BBV-P-423-RQADM-10 JBEJAR 17/04/2017 Pantalla Consulta cofetel. 
Public Class clsCuestionarioCofetel
    Private _strError As String = String.Empty
    Private _ID_SOLICITUD As Integer = 0
    Private _EXISTE_TEL_FIJO_SOL As Integer = 0
    Private _NUMERO_TEL_MOVIL As Integer = 0
    Private _NOMBRE_COMPANIA As String = ""
    Private _EXISTE_TEL_MOVILF As Integer = 0
    Private _EXISTE_TEL_FIJOF As Integer = 0
    Private _NOMBRE_COMPANIA1 As String = ""
    Private _EXISTE_TEL_EMPRESA As Integer = 0
    Private _EXISTE_TEL_REFERENCIA1 As Integer = 0
    Private _EXISTE_TEL_REFERENCIA2 As Integer = 0
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
    Public Property EXISTE_TEL_FIJO_SOL As Integer
        Get
            Return _EXISTE_TEL_FIJO_SOL
        End Get
        Set(value As Integer)
            _EXISTE_TEL_FIJO_SOL = value
        End Set
    End Property
    Public Property NUMERO_TEL_MOVIL As Integer
        Get
            Return _NUMERO_TEL_MOVIL
        End Get
        Set(value As Integer)
            _NUMERO_TEL_MOVIL = value
        End Set
    End Property
    Public Property NOMBRE_COMPANIA As String
        Get
            Return _NOMBRE_COMPANIA
        End Get
        Set(value As String)
            _NOMBRE_COMPANIA = value
        End Set
    End Property
    Public Property EXISTE_TEL_MOVILF As Integer
        Get
            Return _EXISTE_TEL_MOVILF
        End Get
        Set(value As Integer)
            _EXISTE_TEL_MOVILF = value
        End Set
    End Property
    Public Property EXISTE_TEL_FIJOF As Integer
        Get
            Return _EXISTE_TEL_FIJOF
        End Get
        Set(value As Integer)
            _EXISTE_TEL_FIJOF = value
        End Set
    End Property
    Public Property NOMBRE_COMPANIA1 As String
        Get
            Return _NOMBRE_COMPANIA1
        End Get
        Set(value As String)
            _NOMBRE_COMPANIA1 = value
        End Set
    End Property
    Public Property EXISTE_TEL_EMPRESA As Integer
        Get
            Return _EXISTE_TEL_EMPRESA
        End Get
        Set(value As Integer)
            _EXISTE_TEL_EMPRESA = value
        End Set
    End Property
    Public Property EXISTE_TEL_REFERENCIA1 As Integer
        Get
            Return _EXISTE_TEL_REFERENCIA1
        End Get
        Set(value As Integer)
            _EXISTE_TEL_REFERENCIA1 = value
        End Set
    End Property
    Public Property EXISTE_TEL_REFERENCIA2 As Integer
        Get
            Return _EXISTE_TEL_REFERENCIA2
        End Get
        Set(value As Integer)
            _EXISTE_TEL_REFERENCIA2 = value
        End Set
    End Property

    Public Function insertaDatosCofetel() As Boolean
        insertaDatosCofetel = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            BD.AgregaParametro("@id_solicitud", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@existtf", TipoDato.Entero, EXISTE_TEL_FIJO_SOL)
            BD.AgregaParametro("@numerotelm", TipoDato.Entero, NUMERO_TEL_MOVIL)
            BD.AgregaParametro("@nombrecom", TipoDato.Cadena, NOMBRE_COMPANIA)
            BD.AgregaParametro("@existtmf", TipoDato.Entero, EXISTE_TEL_MOVILF)
            BD.AgregaParametro("@existtff", TipoDato.Entero, EXISTE_TEL_FIJOF)
            BD.AgregaParametro("@nombrecom1", TipoDato.Cadena, NOMBRE_COMPANIA1)
            BD.AgregaParametro("@telempresa", TipoDato.Entero, EXISTE_TEL_EMPRESA)
            BD.AgregaParametro("@telref1", TipoDato.Entero, EXISTE_TEL_REFERENCIA1)
            BD.AgregaParametro("@telref2", TipoDato.Entero, EXISTE_TEL_REFERENCIA2)
            dsres = BD.EjecutaStoredProcedure("spInsertaDatosCofetel")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        insertaDatosCofetel = True
                    Else
                        insertaDatosCofetel = False
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
    Public Function GetCuestionarioCofetel() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_SOLICITUD)

            dsres = BD.EjecutaStoredProcedure("SpGetCuestionarioCofetel")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                Else
                    Throw New Exception("No se encontraron datos del cuestionario Cofetel")
                End If
            Else
                Throw New Exception("Falla al consultar cuestionario Cofetel")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function
End Class
