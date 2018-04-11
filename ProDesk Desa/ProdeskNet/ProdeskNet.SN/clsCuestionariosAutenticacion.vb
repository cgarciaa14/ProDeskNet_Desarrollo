Imports ProdeskNet.BD
'RQADM-20: RHERNANDEZ: 19/05/17: SE CREA CLASE PARA EL MANEJO DE LOS CUESTIONARIOS DE AUTENTICACION E IVR
Public Class clsCuestionariosAutenticacion
    Private _strError As String = String.Empty
    Private PDK_ID_SECCERO As Integer = 0
    Private PDK_ID_PREGUNTA As String = String.Empty
    Private PDK_PREGUNTA As String = String.Empty
    Private PDK_ID_RESPUESTA As String = String.Empty
    Private PDK_RESPUESTA As String = String.Empty
    Private PDK_CLAVE_USUARIO As Integer = 0
    Private PDK_TIPO_PREGUNTA As String = String.Empty
    Private PDK_NO_RESPUESTAS As Integer = 0
    Private PDK_RESPUESTA_CORRECTA As Integer = 0
    Private PDK_AYUDA_PREGUNTA As String = String.Empty
    Private OPC As Integer = 0
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
    Public Property _PDK_ID_SECCERO() As Integer
        Get
            Return PDK_ID_SECCERO
        End Get
        Set(value As Integer)
            PDK_ID_SECCERO = value
        End Set
    End Property
    Public Property _PDK_ID_PREGUNTA() As String
        Get
            Return PDK_ID_PREGUNTA
        End Get
        Set(value As String)
            PDK_ID_PREGUNTA = value
        End Set
    End Property
    Public Property _PDK_PREGUNTA() As String
        Get
            Return PDK_PREGUNTA
        End Get
        Set(value As String)
            PDK_PREGUNTA = value
        End Set
    End Property
    Public Property _PDK_ID_RESPUESTA() As String
        Get
            Return PDK_ID_RESPUESTA
        End Get
        Set(value As String)
            PDK_ID_RESPUESTA = value
        End Set
    End Property
    Public Property _PDK_RESPUESTA() As String
        Get
            Return PDK_RESPUESTA
        End Get
        Set(value As String)
            PDK_RESPUESTA = value
        End Set
    End Property
    Public Property _PDK_CLAVE_USUARIO() As Integer
        Get
            Return PDK_CLAVE_USUARIO
        End Get
        Set(value As Integer)
            PDK_CLAVE_USUARIO = value
        End Set
    End Property
    Public Property _PDK_TIPO_PREGUNTA() As String
        Get
            Return PDK_TIPO_PREGUNTA
        End Get
        Set(value As String)
            PDK_TIPO_PREGUNTA = value
        End Set
    End Property
    Public Property _PDK_NO_RESPUESTAS() As Integer
        Get
            Return PDK_NO_RESPUESTAS
        End Get
        Set(value As Integer)
            PDK_NO_RESPUESTAS = value
        End Set
    End Property
    Public Property _PDK_RESPUESTA_CORRECTA() As Integer
        Get
            Return PDK_RESPUESTA_CORRECTA
        End Get
        Set(value As Integer)
            PDK_RESPUESTA_CORRECTA = value
        End Set
    End Property
    Public Property _PDK_AYUDA_PREGUNTA As String
        Get
            Return PDK_AYUDA_PREGUNTA
        End Get
        Set(value As String)
            PDK_AYUDA_PREGUNTA=value 
        End Set
    End Property
    Public Property _OPC() As Integer
        Get
            Return OPC
        End Get
        Set(value As Integer)
            OPC = value
        End Set
    End Property

    Public Function ManejaCuestionarioAut() As DataSet
        Try
            Dim dsres As DataSet = New DataSet
            Dim BD As New clsManejaBD
            Select Case OPC
                Case 0
                    BD.AgregaParametro("@PDK_ID_SECCERO", TipoDato.Entero, PDK_ID_SECCERO)
                    BD.AgregaParametro("@OPC", TipoDato.Entero, OPC)
                Case 1
                    BD.AgregaParametro("@PDK_ID_SECCERO", TipoDato.Entero, PDK_ID_SECCERO)
                    BD.AgregaParametro("@PDK_ID_PREGUNTA", TipoDato.Cadena, PDK_ID_PREGUNTA)
                    BD.AgregaParametro("@PDK_PREGUNTA", TipoDato.Cadena, PDK_PREGUNTA)
                    BD.AgregaParametro("@PDK_ID_RESPUESTA", TipoDato.Cadena, PDK_ID_RESPUESTA)
                    BD.AgregaParametro("@PDK_RESPUESTA", TipoDato.Cadena, PDK_RESPUESTA)
                    BD.AgregaParametro("@PDK_CLAVE_USUARIO", TipoDato.Entero, PDK_CLAVE_USUARIO)
                    BD.AgregaParametro("@PDK_TIPO_PREGUNTA", TipoDato.Cadena, PDK_TIPO_PREGUNTA)
                    BD.AgregaParametro("@PDK_NO_RESPUESTAS", TipoDato.Entero, PDK_NO_RESPUESTAS)
                    BD.AgregaParametro("@PDK_RESPUESTA_CORRECTA", TipoDato.Entero, PDK_RESPUESTA_CORRECTA)
                    BD.AgregaParametro("@PDK_AYUDA_PREGUNTA", TipoDato.Cadena, PDK_AYUDA_PREGUNTA)
                    BD.AgregaParametro("@OPC", TipoDato.Entero, OPC)
                Case 2
                    BD.AgregaParametro("@PDK_ID_SECCERO", TipoDato.Entero, PDK_ID_SECCERO)
                    BD.AgregaParametro("@OPC", TipoDato.Entero, OPC)
            End Select

            dsres = BD.EjecutaStoredProcedure("Sp_Cuest_Autenticacion")
            If (BD.ErrorBD) <> "" Then
                Throw New Exception(BD.ErrorBD)
            End If
            Return dsres
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function

End Class
