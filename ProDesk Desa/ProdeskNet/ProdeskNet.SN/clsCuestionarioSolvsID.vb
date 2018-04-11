Imports ProdeskNet.BD
'BBV-P-423: RQADM-37: JRHM: 17/03/2017 Se crea clase para "Solicitud vs ID"
'BBV-P-423: RQADM-22: JRHM: 24/03/17 SE MODIFICA FUNCIONALIDAD DE CONSULTA DE CUESTIONARIO"
'BBV-P-423: RQXLS3: CGARCIA 09/06/2017 SE AGREGAN PARAMETROS
Public Class clsCuestionarioSolvsID
    Private _strError As String = String.Empty
    Private ID_SOLICITUD As Integer = 0
    Private TIPO_ID As String = String.Empty
    Private ID_VIG As Integer = -1
    Private VAL_ADIC As Integer = -1
    Private NOM_VS_ID As Integer = -1
    Private FIRMA_ID As Integer = -1
    Private LOCK_RIGHT As Integer = -1
    Private ID_DOC As Integer = 0
    Private ID_PANT As Integer = 0
    Private ID_USER As Integer = 0
    Private ACTION As Integer = 0
    Private MOT_RECH As Integer = 0
    Private ID_EXISTE_CEDULA As Integer = -1
    Private ID_NUM_CEDULA As Integer = -1
    Private ID_LICENCIATURA As Integer = -1
    Private ID_ESTATUS_INE As Integer = -1
    Private ID_EXISTE_INE As Integer = -1
    Private ID_VERSION_INE As Integer = -1
    Private ID_MICA_PROC As Integer = -1


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
    Public Property _TIPO_ID() As String
        Get
            Return TIPO_ID
        End Get
        Set(value As String)
            TIPO_ID = value
        End Set
    End Property
    Public Property _ID_VIG() As Integer
        Get
            Return ID_VIG
        End Get
        Set(value As Integer)
            ID_VIG = value
        End Set
    End Property
    Public Property _VAL_ADIC() As Integer
        Get
            Return VAL_ADIC
        End Get
        Set(value As Integer)
            VAL_ADIC = value
        End Set
    End Property
    Public Property _NOM_VS_ID() As Integer
        Get
            Return NOM_VS_ID
        End Get
        Set(value As Integer)
            NOM_VS_ID = value
        End Set
    End Property
    Public Property _FIRMA_ID() As Integer
        Get
            Return FIRMA_ID
        End Get
        Set(value As Integer)
            FIRMA_ID = value
        End Set
    End Property
    Public Property _LOCK_RIGHT() As Integer
        Get
            Return LOCK_RIGHT
        End Get
        Set(value As Integer)
            LOCK_RIGHT = value
        End Set
    End Property
    Public Property _ID_DOC() As Integer
        Get
            Return ID_DOC
        End Get
        Set(value As Integer)
            ID_DOC = value
        End Set
    End Property
    Public Property _ID_PANT() As Integer
        Get
            Return ID_PANT
        End Get
        Set(value As Integer)
            ID_PANT = value
        End Set
    End Property
    Public Property _ID_USER() As Integer
        Get
            Return ID_USER
        End Get
        Set(value As Integer)
            ID_USER = value
        End Set
    End Property
    Public Property _ACTION() As Integer
        Get
            Return ACTION
        End Get
        Set(value As Integer)
            ACTION = value
        End Set
    End Property
    Public Property _MOT_RECH() As Integer
        Get
            Return MOT_RECH
        End Get
        Set(value As Integer)
            MOT_RECH = value
        End Set
    End Property
    Public Property _ID_EXISTE_CEDULA() As Integer
        Get
            Return ID_EXISTE_CEDULA
        End Get
        Set(value As Integer)
            ID_EXISTE_CEDULA = value
        End Set
    End Property
    Public Property _ID_NUM_CEDULA() As Integer
        Get
            Return ID_NUM_CEDULA
        End Get
        Set(value As Integer)
            ID_NUM_CEDULA = value
        End Set
    End Property
    Public Property _ID_LICENCIATURA() As Integer
        Get
            Return ID_LICENCIATURA
        End Get
        Set(value As Integer)
            ID_LICENCIATURA = value
        End Set
    End Property
    Public Property _ID_ESTATUS_INE() As Integer
        Get
            Return ID_ESTATUS_INE
        End Get
        Set(value As Integer)
            ID_ESTATUS_INE = value
        End Set
    End Property
    Public Property _ID_EXISTE_INE() As Integer
        Get
            Return ID_EXISTE_INE
        End Get
        Set(value As Integer)
            ID_EXISTE_INE = value
        End Set
    End Property
    Public Property _ID_VERSION_INE() As Integer
        Get
            Return ID_VERSION_INE
        End Get
        Set(value As Integer)
            ID_VERSION_INE = value
        End Set
    End Property
    Public Property _ID_MICA_PROC() As Integer
        Get
            Return ID_MICA_PROC
        End Get
        Set(value As Integer)
            ID_MICA_PROC = value
        End Set
    End Property

    Public Function InsertCuestionarioIDvsSol() As Boolean
        InsertCuestionarioIDvsSol = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            BD.AgregaParametro("@id_solicitud", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@Tipo_ID", TipoDato.Cadena, TIPO_ID)
            BD.AgregaParametro("@ID_Vig", TipoDato.Entero, ID_VIG)
            BD.AgregaParametro("@Val_Adic", TipoDato.Entero, VAL_ADIC)
            BD.AgregaParametro("@NomID_Vs_Sol", TipoDato.Entero, NOM_VS_ID)
            BD.AgregaParametro("@Firma_ID", TipoDato.Entero, FIRMA_ID)
            BD.AgregaParametro("@Lock_Right", TipoDato.Entero, LOCK_RIGHT)
            BD.AgregaParametro("@Existe_Cedula", TipoDato.Entero, ID_EXISTE_CEDULA)
            BD.AgregaParametro("@Numero_Cedula", TipoDato.Entero, ID_NUM_CEDULA)
            BD.AgregaParametro("@Lincenciatura", TipoDato.Entero, ID_LICENCIATURA)
            BD.AgregaParametro("@Estatus_INE", TipoDato.Entero, ID_ESTATUS_INE)
            BD.AgregaParametro("@Existe_INE", TipoDato.Entero, ID_EXISTE_INE)
            BD.AgregaParametro("@Version_INE", TipoDato.Entero, ID_VERSION_INE)
            BD.AgregaParametro("@Mica_Procesable", TipoDato.Entero, ID_MICA_PROC)

            dsres = BD.EjecutaStoredProcedure("spInsertaCuestionarioIDvsSol")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        InsertCuestionarioIDvsSol = True
                    Else
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
    Public Function GetCuestionarioIDvsSol() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_SOLICITUD)

            dsres = BD.EjecutaStoredProcedure("SpGetCuestionarioIDvsSol")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                End If
            Else
                Throw New Exception("Falla al consultar cuestionario ID vs Solicitud")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function
    Public Function ValidaID() As Boolean
        ValidaID = False
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@IdDoc", TipoDato.Entero, ID_DOC)
            BD.AgregaParametro("@IdPantalla", TipoDato.Entero, ID_PANT)
            BD.AgregaParametro("@IdUsuario", TipoDato.Entero, ID_USER)
            BD.AgregaParametro("@Valido", TipoDato.Entero, ACTION)
            BD.AgregaParametro("@IdRech", TipoDato.Entero, MOT_RECH)

            dsres = BD.EjecutaStoredProcedure("Sp_ValidaYRechazoDoc")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(1).Rows.Count > 0) Then
                    If (dsres.Tables(1).Rows(0).Item("RESULT").ToString = "OK") Then
                        ValidaID = True
                    Else
                        Throw New Exception("Falla al guardar rechazar o validar documento")
                    End If
                Else
                    Throw New Exception("Falla al guardar rechazar o validar documento")
                End If
            Else
                Throw New Exception("Falla al guardar rechazar o validar documento")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return False
        End Try
    End Function
    Public Function getTurnar() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@Id_pantalla", TipoDato.Entero, ID_PANT)

            dsres = BD.EjecutaStoredProcedure("Sp_Get_Turnar")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                Else
                    Throw New Exception("Pantalla no tiene opciones turnar configuradas")
                End If
            Else
                Throw New Exception("Falla al obtener las opciones a turnar")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function

End Class
