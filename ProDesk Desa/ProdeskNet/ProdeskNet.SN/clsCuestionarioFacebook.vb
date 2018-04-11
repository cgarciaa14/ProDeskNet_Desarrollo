Imports ProdeskNet.BD
'BBV-P-423-RQADM-16 JBB 21/03/2017 Pantalla Consulta Portal Facebook. 
'BUG-PD-37 JBB 24/04/17  : Correciones redes solicitudes. 

Public Class clsCuestionarioFacebook
    Private _strError As String = String.Empty
    Private ID_SOLICITUD As Integer = 0
    Private DISP As Integer = 0
    Private ANIO_APERTURA As String = ""
    Private LOC_GEO As Integer = 0
    Private FOTO_PERF As Integer = 0
    Private AMIGOS As Integer = 0
    Private PUBLI_REC As Integer = 0
    Private EMPLEO As Integer = 0

    Sub New()

    End Sub


    Public Property StrError() As String
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

    Public Property _ANIO_APERTURA() As String
        Get
            Return ANIO_APERTURA
        End Get
        Set(value As String)

            ANIO_APERTURA = value
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

    Public Property _FOTO_PER() As Integer
        Get
            Return FOTO_PERF
        End Get
        Set(value As Integer)

            FOTO_PERF = value

        End Set
    End Property

    Public Property _AMIGOS() As Integer
        Get
            Return AMIGOS
        End Get
        Set(value As Integer)

            AMIGOS = value

        End Set
    End Property
    Public Property _PUBLI_REC() As Integer
        Get
            Return PUBLI_REC
        End Get
        Set(value As Integer)

            PUBLI_REC = value

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

    Public Function insertaDatosFacebook() As Boolean
        insertaDatosFacebook = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            BD.AgregaParametro("@id_solicitud", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@disponible", TipoDato.Entero, DISP)
            If ANIO_APERTURA <> String.Empty Then
                BD.AgregaParametro("@apertura", TipoDato.Cadena, ANIO_APERTURA)
            End If
            BD.AgregaParametro("@geografia", TipoDato.Entero, LOC_GEO)
            BD.AgregaParametro("@perfil", TipoDato.Entero, FOTO_PERF)
            BD.AgregaParametro("@amigos", TipoDato.Entero, AMIGOS)
            BD.AgregaParametro("@publicaciones", TipoDato.Entero, PUBLI_REC)
            BD.AgregaParametro("@empleo", TipoDato.Entero, EMPLEO)
            dsres = BD.EjecutaStoredProcedure("spInsertaDatosFacebook")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        insertaDatosFacebook = True
                    Else
                        insertaDatosFacebook = False
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
    Public Function GetCuestionarioFACE() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_SOLICITUD)

            dsres = BD.EjecutaStoredProcedure("SpGetCuestionarioFacebook")
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
