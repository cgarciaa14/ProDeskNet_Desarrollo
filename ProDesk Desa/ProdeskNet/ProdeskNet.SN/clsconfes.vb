Imports ProdeskNet.BD
'BUG-PD-50 JBEJAR 13/05/2017 CLASE PARA GUARDAR DATOS.
Public Class clsconfes
    Private _strError As String = String.Empty
    Private _ID_CONFIGUR As Integer = 1
    Private _NUM_VEHI As Integer = 0
    Private _PERIODO_AUTO As Integer = 0
    Private _MONTO_ALIA As Integer = 0
    Private _MONTO_MULTI As Integer = 0
    Private _RIESGO As Integer = 0
    Public Property strError As String
        Get
            Return _strError
        End Get
        Set(value As String)
            _strError = value
        End Set
    End Property

    Public Property ID_CONFIGUR As Integer
        Get
            Return _ID_CONFIGUR
        End Get
        Set(value As Integer)
            _ID_CONFIGUR = value
        End Set
    End Property

    Public Property NUM_VEHI As Integer
        Get
            Return _NUM_VEHI
        End Get
        Set(value As Integer)
            _NUM_VEHI = value
        End Set
    End Property
    Public Property PERIODO_AUTO As Integer
        Get
            Return _PERIODO_AUTO
        End Get
        Set(value As Integer)
            _PERIODO_AUTO = value
        End Set
    End Property
    Public Property MONTO_ALIA As Integer
        Get
            Return _MONTO_ALIA
        End Get
        Set(value As Integer)
            _MONTO_ALIA = value
        End Set
    End Property
    Public Property MONTO_MULTI As Integer
        Get
            Return _MONTO_MULTI
        End Get
        Set(value As Integer)
            _MONTO_MULTI = value
        End Set
    End Property

    Public Property RIESGO As Integer
        Get
            Return _RIESGO
        End Get
        Set(value As Integer)
            _RIESGO = value
        End Set
    End Property

    Public Function insertaDatosConfigespe() As Boolean
        insertaDatosConfigespe = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            BD.AgregaParametro("@id_solicitud", TipoDato.Entero, ID_CONFIGUR)
            BD.AgregaParametro("@numvehi", TipoDato.Entero, NUM_VEHI)
            BD.AgregaParametro("@periodoauto", TipoDato.Entero, PERIODO_AUTO)
            BD.AgregaParametro("@montoali", TipoDato.Entero, MONTO_ALIA)
            BD.AgregaParametro("@montomulti", TipoDato.Entero, MONTO_MULTI)
            BD.AgregaParametro("@riesgo", TipoDato.Entero, RIESGO)
            dsres = BD.EjecutaStoredProcedure("spInsertaDatosConfigEs")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        insertaDatosConfigespe = True
                    Else
                        insertaDatosConfigespe = False
                        Throw New Exception(dsres.Tables(0).Rows(0).Item("RESULT").ToString())
                    End If
                Else
                    Throw New Exception("Falla al guardar")
                End If
            Else
                Throw New Exception("Falla al guardar ")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return False
        End Try
    End Function
    Public Function GetConfigEspe() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_CONFIGUR)

            dsres = BD.EjecutaStoredProcedure("SpGetConfigEspe")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                Else
                    Throw New Exception("No se encontraron datos de Configuración especiales")
                End If
            Else
                Throw New Exception("Falla al consultar datos Configuración especiales.")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function
End Class
