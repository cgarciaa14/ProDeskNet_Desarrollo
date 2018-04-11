Imports ProdeskNet.BD
'BBV-P-423-RQADM-17 JBB 24/03/2017 Pantalla Consulta Portal Linkedin. 
Public Class clsCuestionarioLinkedin
    Private _strError As String = String.Empty
    Private ID_SOLICITUD As Integer = 0
    Private DISP As Integer = 0
    Private LOC_GEO As Integer = 0
    Private FOTO_PERF As Integer = 0
    Private EMPLEO As Integer = 0

    Sub New()

    End Sub

    Public Property strError() As String
        Get
            Return _strError
        End Get
        Set(value As String)
            _strError = value
        End Set
    End Property

    Public Property _ID_SOLICITUD() As Integer
        Get
            Return ID_SOLICITUD
        End Get
        Set(value As Integer)
            ID_SOLICITUD = value
        End Set
    End Property

    Public Property _DISP() As Integer
        Get
            Return DISP
        End Get
        Set(value As Integer)
            DISP = value
        End Set
    End Property

    Public Property _LOC_GEO() As Integer
        Get
            Return LOC_GEO

        End Get
        Set(value As Integer)

            LOC_GEO = value
        End Set
    End Property

    Public Property _FOTO_PERFIL() As Integer
        Get
            Return FOTO_PERF
        End Get
        Set(value As Integer)
            FOTO_PERF = value
        End Set
    End Property
    Public Property _EMPLEO() As Integer
        Get
            Return EMPLEO
        End Get
        Set(value As Integer)
            EMPLEO = value
        End Set
    End Property

    Public Function insertaDatosLinkedin() As Boolean
        insertaDatosLinkedin = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            BD.AgregaParametro("@id_solicitud", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@disponible", TipoDato.Entero, DISP)
            BD.AgregaParametro("@geografia", TipoDato.Entero, LOC_GEO)
            BD.AgregaParametro("@perfil", TipoDato.Entero, FOTO_PERF)
            BD.AgregaParametro("@empleo", TipoDato.Entero, EMPLEO)
            dsres = BD.EjecutaStoredProcedure("spInsertaDatosLinkedin")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        insertaDatosLinkedin = True
                    Else
                        insertaDatosLinkedin = False
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
    Public Function GetCuestionarioLINK() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_SOLICITUD)

            dsres = BD.EjecutaStoredProcedure("SpGetCuestionarioLinkedin")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                Else
                    Throw New Exception("No se encontraron datos del cuestionario Linkedin")
                End If
            Else
                Throw New Exception("Falla al consultar cuestionario Linkedin")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function

End Class
