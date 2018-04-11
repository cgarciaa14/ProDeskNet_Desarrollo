Imports ProdeskNet.BD
'BBV-P-423: RQADM-15: JRHM: 17/03/17 SE CREA PANTALLA PARA TAREA DE "CONSULTA INGRESOS EN PORTAL DE TRANSPARENCIA" 
'BBV-P-423: RQADM-22: JRHM: 24/03/17 SE MODIFICA LA CONSULTA DE CUESTIONARIO
Public Class clsCuestionarioIFAI
    Private _strError As String = String.Empty
    Private ID_SOLICITUD As Integer = 0
    Private IFAI_DISP As Integer = 0
    Private EXIST_SOL As Integer = 0
    Private EXIST_PUESTO As Integer = 0
    Private EXIST_REL_PUE_SOL As Integer = 0
    Private MONTO_REG As Double = 0

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
    Public Property _IFAI_DISP() As Integer
        Get
            Return IFAI_DISP
        End Get
        Set(value As Integer)
            IFAI_DISP = value
        End Set
    End Property
    Public Property _EXIST_SOL() As Integer
        Get
            Return EXIST_SOL
        End Get
        Set(value As Integer)
            EXIST_SOL = value
        End Set
    End Property
    Public Property _EXIST_PUESTO() As Integer
        Get
            Return EXIST_PUESTO
        End Get
        Set(value As Integer)
            EXIST_PUESTO = value
        End Set
    End Property
    Public Property _EXIST_REL_PUE_SOL() As Integer
        Get
            Return EXIST_REL_PUE_SOL
        End Get
        Set(value As Integer)
            EXIST_REL_PUE_SOL = value
        End Set
    End Property
    Public Property _MONTO_REG() As Double
        Get
            Return MONTO_REG
        End Get
        Set(value As Double)
            MONTO_REG = value
        End Set
    End Property

    Public Function InsertDatosSeguroSolicitud() As Boolean
        InsertDatosSeguroSolicitud = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            BD.AgregaParametro("@id_solicitud", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@IFAI_Disp", TipoDato.Entero, IFAI_DISP)
            BD.AgregaParametro("@Exist_sol", TipoDato.Entero, EXIST_SOL)
            BD.AgregaParametro("@Exist_puesto", TipoDato.Entero, EXIST_PUESTO)
            BD.AgregaParametro("@Exist_rel_sol_puesto", TipoDato.Entero, EXIST_REL_PUE_SOL)
            BD.AgregaParametro("@Monto_sol", TipoDato.Flotante, MONTO_REG)
            dsres = BD.EjecutaStoredProcedure("spInsertaDatosIFAI")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        InsertDatosSeguroSolicitud = True
                    Else
                        InsertDatosSeguroSolicitud = False
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
    Public Function GetCuestionarioIFAI() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_SOLICITUD)

            dsres = BD.EjecutaStoredProcedure("SpGetCuestionarioIFAI")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                End If
            Else
                Throw New Exception("Falla al consultar cuestionario IFAI")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function

End Class


