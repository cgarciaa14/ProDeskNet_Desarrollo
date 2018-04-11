#Region "Trackers"
'BBV-P-423 - RQADM-25  09/05/2017 erodriguez: Clases para consultar y actualizar en base de datos para Mesa Pool de Crédito 
'BBV-P-423 - BUG-PD-63 26/05/2017 erodriguez se agrego una actualizacion del grid y permitir guardar el usuario asignado
'BUG-PD-74 - JBEJAR    06/06/2017  se agrega la opcion 6 para la caratula de sancion. 
#End Region
Imports System.Text
Imports System.Data

Public Class clsMesaPoolCredito
#Region "Variables"

    Private intPDK_ID_SOLICITUD As Integer = 0
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_OPE_USU_ASIGNADO As Integer = 0
    Private intESTATUS_ATENCION As Integer = 0
    Private intPDK_ID_REL_POOL_CRED As Integer = 0

    Private strNombre As String = ""
    Private strFechaIni As String = ""
    Private strFechaFin As String = ""
    Private strRFC As String = String.Empty

    Private intPDK_ID_PANTALLA As Integer = 0

    Private strErrSolicitud As String = ""
    Private strComentario As String = ""
    Private strNombreUsuario As String = ""
    Private strContraseña As String = ""

    Private strMotivoSancion As String = ""
    Private strCausaRechazo As String = ""



    Private strmensaje As String = String.Empty


#End Region
#Region "Propiedades"


    Public Property PDK_ID_SOLICITUD() As Integer
        Get
            Return intPDK_ID_SOLICITUD
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_SOLICITUD = value
        End Set
    End Property

    Public Property PDK_ID_REL_POOL_CRED() As Integer
        Get
            Return intPDK_ID_REL_POOL_CRED
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_REL_POOL_CRED = value
        End Set
    End Property

    Public Property PDK_CLAVE_USUARIO() As Integer
        Get
            Return intPDK_CLAVE_USUARIO
        End Get
        Set(ByVal value As Integer)
            intPDK_CLAVE_USUARIO = value
        End Set
    End Property
    Public Property PDK_OPE_USU_ASIGNADO() As Integer
        Get
            Return intPDK_OPE_USU_ASIGNADO
        End Get
        Set(ByVal value As Integer)
            intPDK_OPE_USU_ASIGNADO = value
        End Set
    End Property

    Public Property MotivoSancion() As String
        Get
            Return strMotivoSancion
        End Get
        Set(ByVal value As String)
            strMotivoSancion = value
        End Set
    End Property

    Public Property CausaRechazo() As String
        Get
            Return strCausaRechazo
        End Get
        Set(ByVal value As String)
            strCausaRechazo = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return strNombre
        End Get
        Set(ByVal value As String)
            strNombre = value
        End Set
    End Property
    Public Property FechaIni() As String
        Get
            Return strFechaIni
        End Get
        Set(ByVal value As String)
            If value = "" Then
                strFechaIni = value
            Else

                strFechaIni = Format(CDate(value), "yyyyMMdd")
            End If


        End Set
    End Property
    Public Property FechaFin() As String
        Get
            Return strFechaFin
        End Get
        Set(ByVal value As String)
            If value = "" Then
                strFechaFin = value
            Else

                strFechaFin = Format(CDate(value), "yyyyMMdd")
            End If
        End Set
    End Property
    Public Property RFC() As String
        Get
            Return strRFC
        End Get
        Set(ByVal value As String)
            strRFC = value
        End Set
    End Property
    Public Property PDK_ID_PANTALLA() As Integer
        Get
            Return intPDK_ID_PANTALLA
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_PANTALLA = value
        End Set
    End Property
    Public Property ERROR_SOL() As String
        Get
            Return strErrSolicitud
        End Get
        Set(ByVal value As String)
            strErrSolicitud = value
        End Set
    End Property
    Public Property Comentario() As String
        Get
            Return strComentario
        End Get
        Set(ByVal value As String)
            strComentario = value
        End Set
    End Property
    Public Property NombreUsu() As String
        Get
            Return strNombreUsuario
        End Get
        Set(ByVal value As String)
            strNombreUsuario = value
        End Set
    End Property
    Public Property Contraseña() As String
        Get
            Return strContraseña
        End Get
        Set(ByVal value As String)
            strContraseña = value
        End Set
    End Property

    Public Property MENSAJE() As String
        Get
            Return strmensaje
        End Get
        Set(ByVal value As String)
            strmensaje = value
        End Set
    End Property

    Public Property ESTATUS_ATENCION() As Integer
        Get
            Return intESTATUS_ATENCION
        End Get
        Set(ByVal value As Integer)
            intESTATUS_ATENCION = value
        End Set
    End Property

#End Region


#Region "Metodos"

    Public Function ConsultaSolicitudPool(ByVal intOper As Integer) As DataSet
        Dim MB As New ProdeskNet.BD.clsManejaBD
        Try

            strErrSolicitud = ""

            Select Case intOper
                Case 1 'CONSULTA
                    MB.AgregaParametro("@OPCION", ProdeskNet.BD.TipoDato.Entero, intOper)
                    MB.AgregaParametro("@PDK_CVE_USR", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                Case 2
                    MB.AgregaParametro("@OPCION", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then MB.AgregaParametro("@PDK_ID_SECCCERO", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If strNombre <> "" Then MB.AgregaParametro("@NOM_CTE", ProdeskNet.BD.TipoDato.Cadena, strNombre)
                    If strFechaIni <> "" Then MB.AgregaParametro("@FECHA_INI", ProdeskNet.BD.TipoDato.Cadena, strFechaIni)
                    If strFechaFin <> "" Then MB.AgregaParametro("@FECHA_FIN", ProdeskNet.BD.TipoDato.Cadena, strFechaFin)
                    If strRFC <> "" Then MB.AgregaParametro("@RFC", ProdeskNet.BD.TipoDato.Cadena, strRFC)
                    MB.AgregaParametro("@PDK_CVE_USR", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                Case 3
                    MB.AgregaParametro("@OPCION", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then MB.AgregaParametro("@PDK_ID_SECCCERO", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    If intPDK_ID_PANTALLA > 0 Then MB.AgregaParametro("@TIPO_PANTALLA", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PANTALLA)
                Case 4
                    MB.AgregaParametro("@OPCION", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then MB.AgregaParametro("@PDK_ID_SECCCERO", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    MB.AgregaParametro("@PDK_ID_REL_POOL_CRED", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_REL_POOL_CRED)
                    MB.AgregaParametro("@PDK_CVE_USR", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                    MB.AgregaParametro("@STAT_ATENCION", ProdeskNet.BD.TipoDato.Entero, intESTATUS_ATENCION)
                Case 5
                    If strMotivoSancion <> "" Then MB.AgregaParametro("@MOTIVO_SANCION", ProdeskNet.BD.TipoDato.Cadena, strMotivoSancion)
                    If strCausaRechazo <> "" Then MB.AgregaParametro("@CAUSA_RECHAZO", ProdeskNet.BD.TipoDato.Cadena, strCausaRechazo)
                    MB.AgregaParametro("@OPCION", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then MB.AgregaParametro("@PDK_ID_SECCCERO", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    MB.AgregaParametro("@PDK_CVE_USR", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                Case 6
                    If strMotivoSancion <> "" Then MB.AgregaParametro("@MOTIVO_SANCION", ProdeskNet.BD.TipoDato.Cadena, strMotivoSancion)
                    If strCausaRechazo <> "" Then MB.AgregaParametro("@CAUSA_RECHAZO", ProdeskNet.BD.TipoDato.Cadena, strCausaRechazo)
                    MB.AgregaParametro("@OPCION", ProdeskNet.BD.TipoDato.Entero, intOper)
                    If intPDK_ID_SOLICITUD > 0 Then MB.AgregaParametro("@PDK_ID_SECCCERO", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_SOLICITUD)
                    MB.AgregaParametro("@PDK_CVE_USR", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
            End Select

            ConsultaSolicitudPool = MB.EjecutaStoredProcedure("getPoolCredito")
            If (MB.ErrorBD) <> "" Then
                strErrSolicitud = MB.ErrorBD
            End If
        Catch ex As Exception
            strErrSolicitud = ex.Message + " " + strErrSolicitud
        End Try
    End Function

#End Region

End Class
