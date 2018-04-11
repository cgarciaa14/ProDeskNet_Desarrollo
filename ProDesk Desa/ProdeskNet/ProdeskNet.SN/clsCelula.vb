'BUG-PD-50 JBEJAR 19/05/2017 SE GUARDA EL TIPO DE TIPIFICACION DE CELULA ANTIFRAUDE.
Imports ProdeskNet.BD
Public Class clsCelula
    Private _strError As String
    Private _ID_SOLICITUD As Integer = 0
    Private _PANTALLA As Integer = 0
    Private _TIPIFICACION As Integer = 0
    Public Property strError As String
        Get
            Return _strError
        End Get
        Set(ByVal value As String)
            _strError = value
        End Set
    End Property

    Public Property ID_SOLICITUD As Integer
        Get
            Return _ID_SOLICITUD
        End Get
        Set(ByVal value As Integer)
            _ID_SOLICITUD = value
        End Set
    End Property

    Public Property PANTALLA As Integer
        Get
            Return _PANTALLA
        End Get
        Set(value As Integer)
            _PANTALLA = value

        End Set
    End Property
    Public Property TIPIFICACION As Integer
        Get
            Return _TIPIFICACION
        End Get
        Set(value As Integer)
            _TIPIFICACION = value
        End Set
    End Property
    Public Function insertaDatosCelula() As Boolean
        insertaDatosCelula = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            BD.AgregaParametro("@id_solicitud", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@pantalla", TipoDato.Entero, PANTALLA)
            BD.AgregaParametro("@tipificacion", TipoDato.Entero, TIPIFICACION)
            dsres = BD.EjecutaStoredProcedure("spInsertaCelula")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        insertaDatosCelula = True
                    Else
                        insertaDatosCelula = False
                        Throw New Exception(dsres.Tables(0).Rows(0).Item("RESULT").ToString())
                    End If
                Else
                    Throw New Exception("Falla al guardar Datos")
                End If
            Else
                Throw New Exception("Falla al guardar Cuestionario")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return False
        End Try
    End Function
    Public Function GetCelula() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@idSolicitud", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@pantalla", TipoDato.Entero, PANTALLA)
            dsres = BD.EjecutaStoredProcedure("SpGetCelula")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                Else
                    Throw New Exception("No se encontraron datos del caso consultado")
                End If
            Else
                Throw New Exception("Falla al consultar datos")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function
End Class
