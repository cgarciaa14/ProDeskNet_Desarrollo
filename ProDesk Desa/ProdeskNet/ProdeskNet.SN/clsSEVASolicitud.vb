Imports ProdeskNet.BD

#Region "Trackers"
'BBV-P-423 RQADM-30 MAPH 17/04/2017 Pantalla Referenciados SEVA 60, clase de almacenamiento y acceso a DAL de solicitudes SEVA
#End Region


Public Class clsSEVASolicitud
#Region "Variables"
    Private _IdSolicitudSEVA As Integer?
    Private _IdPerJuridica As Integer
    Private _Nombre_RazonSocial As String
    Private _SegundoNombre As String
    Private _ApPaterno As String
    Private _ApMaterno As String
    Private _RFC As String
    Private _TelefonoFijo As String
    Private _TelefonoMovil As String
    Private _Email As String
    Private _NombreEjecutivo As String
    Private _UsuarioEjecutivo As Integer
    Private _CR_Sucursal As Integer
    Private _EmailEjecutivo As String
    Private _FechaAlta As Date
    Private _FolioSEVA As String
    Private _Estado As Integer?
#End Region

#Region "Properties"

    Public Property IdSolicitudSEVA() As Integer?
        Get
            Return _IdSolicitudSEVA
        End Get
        Set(ByVal value As Integer?)
            _IdSolicitudSEVA = value
        End Set
    End Property

    Public Property IdPerJuridica() As Integer
        Get
            Return _IdPerJuridica
        End Get
        Set(ByVal value As Integer)
            _IdPerJuridica = value
        End Set
    End Property

    Public Property Nombre_RazonSocial() As String
        Get
            Return _Nombre_RazonSocial
        End Get
        Set(ByVal value As String)
            _Nombre_RazonSocial = value
        End Set
    End Property

    Public Property SegundoNombre() As String
        Get
            Return _SegundoNombre
        End Get
        Set(ByVal value As String)
            _SegundoNombre = value
        End Set
    End Property

    Public Property ApPaterno() As String
        Get
            Return _ApPaterno
        End Get
        Set(ByVal value As String)
            _ApPaterno = value
        End Set
    End Property

    Public Property ApMaterno() As String
        Get
            Return _ApMaterno
        End Get
        Set(ByVal value As String)
            _ApMaterno = value
        End Set
    End Property

    Public Property RFC() As String
        Get
            Return _RFC
        End Get
        Set(ByVal value As String)
            _RFC = value
        End Set
    End Property

    Public Property TelefonoFijo() As String
        Get
            Return _TelefonoFijo
        End Get
        Set(ByVal value As String)
            _TelefonoFijo = value
        End Set
    End Property

    Public Property TelefonoMovil() As String
        Get
            Return _TelefonoMovil
        End Get
        Set(ByVal value As String)
            _TelefonoMovil = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Public Property NombreEjecutivo() As String
        Get
            Return _NombreEjecutivo
        End Get
        Set(ByVal value As String)
            _NombreEjecutivo = value
        End Set
    End Property

    Public Property UsuarioEjecutivo() As Integer
        Get
            Return _UsuarioEjecutivo
        End Get
        Set(ByVal value As Integer)
            _UsuarioEjecutivo = value
        End Set
    End Property

    Public Property CR_Sucursal() As Integer
        Get
            Return _CR_Sucursal
        End Get
        Set(ByVal value As Integer)
            _CR_Sucursal = value
        End Set
    End Property

    Public Property EmailEjecutivo() As String
        Get
            Return _EmailEjecutivo
        End Get
        Set(ByVal value As String)
            _EmailEjecutivo = value
        End Set
    End Property

    Public Property FechaAlta() As Date
        Get
            Return _FechaAlta
        End Get
        Set(ByVal value As Date)
            _FechaAlta = value
        End Set
    End Property

    Public Property FolioSEVA() As String
        Get
            Return _FolioSEVA
        End Get
        Set(ByVal value As String)
            _FolioSEVA = value
        End Set
    End Property

    Public Property Estado() As Integer?
        Get
            If Estado Is Nothing Then
                Return 2
            End If
            Return _Estado
        End Get
        Set(ByVal value As Integer?)
            _Estado = value
        End Set
    End Property

#End Region

End Class


Public Class clsSEVASolicitudes
    Inherits List(Of clsSEVASolicitud)

#Region "Variables"
    Dim dataManager As New clsManejaBD
    Dim tempResult As New DataSet
    Dim _clsManejaDB As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
#End Region

    ''' <summary>
    ''' Inserta en base de datos los elementos mínimos para un solicitante en la tabla de solicitudes SEVA
    ''' </summary>
    ''' <param name="datosSolicitud">Datos de la solicitud mínimos para realizar alta en la tabla SEVA</param>
    ''' <remarks>Asigna un folio SEVA a la solicitud que se envía como parámetro</remarks>
    Public Sub GeneraSolicitudSEVA(ByRef datosSolicitud As clsSEVASolicitud)
        dataManager = New clsManejaBD()
        tempResult = New DataSet()
        dataManager.AgregaParametro("@PDK_ID_PER_JURIDICA", ProdeskNet.BD.TipoDato.Entero, datosSolicitud.IdPerJuridica)
        dataManager.AgregaParametro("@PDK_NOMBRE_RAZON_SOC", ProdeskNet.BD.TipoDato.Cadena, datosSolicitud.Nombre_RazonSocial)
        dataManager.AgregaParametro("@PDK_SEGUNDO_NOMBRE", ProdeskNet.BD.TipoDato.Cadena, datosSolicitud.SegundoNombre)
        dataManager.AgregaParametro("@PDK_AP_PATERNO", ProdeskNet.BD.TipoDato.Cadena, datosSolicitud.ApPaterno)
        dataManager.AgregaParametro("@PDK_AP_MATERNO", ProdeskNet.BD.TipoDato.Cadena, datosSolicitud.ApMaterno)
        dataManager.AgregaParametro("@PDK_RFC", ProdeskNet.BD.TipoDato.Cadena, datosSolicitud.RFC)
        dataManager.AgregaParametro("@PDK_TELEFONO_FIJO", ProdeskNet.BD.TipoDato.Cadena, datosSolicitud.TelefonoFijo)
        dataManager.AgregaParametro("@PDK_TELEFONO_MOVIL", ProdeskNet.BD.TipoDato.Cadena, datosSolicitud.TelefonoMovil)
        dataManager.AgregaParametro("@PDK_EMAIL", ProdeskNet.BD.TipoDato.Cadena, datosSolicitud.Email)
        dataManager.AgregaParametro("@PDK_NOMBRE_EJECUTIVO", ProdeskNet.BD.TipoDato.Cadena, datosSolicitud.NombreEjecutivo)
        dataManager.AgregaParametro("@PDK_USUARIO_EJECUTIVO", ProdeskNet.BD.TipoDato.Entero, datosSolicitud.UsuarioEjecutivo)
        dataManager.AgregaParametro("@PDK_CR_SUCURSAL_EJECUTIVO", ProdeskNet.BD.TipoDato.Entero, datosSolicitud.CR_Sucursal)
        dataManager.AgregaParametro("@PDK_EMAIL_EJECUTIVO", ProdeskNet.BD.TipoDato.Cadena, datosSolicitud.EmailEjecutivo)
        dataManager.AgregaParametro("@PDK_ID_STATUS", ProdeskNet.BD.TipoDato.Cadena, datosSolicitud.Estado)

        tempResult = dataManager.EjecutaStoredProcedure("sp_insertaSolSEVA")

        If Not tempResult.Tables Is Nothing And tempResult.Tables.Count > 0 Then
            If Not tempResult.Tables(0).Rows Is Nothing And tempResult.Tables(0).Rows.Count > 0 Then
                If Not tempResult.Tables(0).Rows(0)("FOLIO_SEVA") Is Nothing Then
                    datosSolicitud.FolioSEVA = tempResult.Tables(0).Rows(0)("FOLIO_SEVA").ToString()
                End If
            End If
        End If

    End Sub

    Public Function AsignaSolicitudSEVA(ByVal idSolicitud As String) As Integer
        Dim result As Integer = 0
        dataManager = New clsManejaBD()
        tempResult = New DataSet()
        dataManager.AgregaParametro("@ID_SOLICITUD", ProdeskNet.BD.TipoDato.Entero, idSolicitud)
        tempResult = dataManager.EjecutaStoredProcedure("sp_asignaSolSEVA")
        If Not tempResult.Tables Is Nothing And tempResult.Tables.Count > 0 Then
            If Not tempResult.Tables(0).Rows Is Nothing And tempResult.Tables(0).Rows.Count > 0 Then
                result = Convert.ToInt32(tempResult.Tables(0).Rows(0)("SOLICITUDES_POR_RFC").ToString())
            End If
        End If
        Return result
    End Function
End Class