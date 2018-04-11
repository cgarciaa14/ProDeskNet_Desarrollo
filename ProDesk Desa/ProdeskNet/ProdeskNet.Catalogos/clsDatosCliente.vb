Imports ProdeskNet.BD

Public Class clsDatosCliente
    Dim BD As New ProdeskNet.BD.clsManejaBD

#Region "trackers"
    'INC-B-2019:JDRA:Regresar.
    'BUG-PD-16: 09/03/2017 MAPH: Corrección en la recuperación del nombre del cliente.
    'BUG-PD-17 
    'BBV-P-423:RQAMD-26 JBEJAR 11/05/2017 SE PROGRAMA  CEDULA ANTIFRAUDE.
    'BUG-PD-247 JBEJAR 27/10/2017  SE AGREGA CORREO ELECTRONICO Y VISOR DOCUMENTAL. 
    'RQ-PD18: CGARCIA: 13/12/2017: SE AGREGA NUMERO DE CLIENTE DESDE WS Y BD EN ENCABEZADO DE INFO 
#End Region

#Region "Variables"
    Private Nombre1 As String
    Private Nombre2 As String
    Private apellidopaterno As String
    Private apellidomaterno As String
    Private nombreCompleto As String
    Private solicitud As Integer
    Private empresa As Integer
    Private producto As Integer
    Private tpersona As Integer
    Private RFC As String
    Private _Correo As String = String.Empty
    Private _strError As String
    Private _NumCliente As String

#End Region
#Region "Propiedades"
    Public Property idSolicitud() As Integer
        Get
            Return solicitud
        End Get
        Set(value As Integer)
            solicitud = value
        End Set
    End Property
    Public Property idempresa() As Integer
        Get
            Return empresa
        End Get
        Set(value As Integer)
            empresa = value
        End Set
    End Property
    Public Property idproducto() As Integer
        Get
            Return producto
        End Get
        Set(value As Integer)
            producto = value
        End Set
    End Property
    Public Property idtpersona() As Integer
        Get
            Return tpersona
        End Get
        Set(value As Integer)
            solicitud = tpersona
        End Set
    End Property
    Public Property PropNombre1() As String
        Get
            Return Nombre1
        End Get
        Set(value As String)
            Nombre1 = value
        End Set
    End Property
    Public Property propNombre2() As String
        Get
            Return Nombre2
        End Get
        Set(value As String)
            Nombre2 = value
        End Set
    End Property
    Public Property propapellidopaterno() As String
        Get
            Return apellidopaterno
        End Get
        Set(value As String)
            apellidopaterno = value
        End Set
    End Property
    Public Property propapellidomaterno() As String
        Get
            Return apellidomaterno
        End Get
        Set(value As String)
            apellidomaterno = value
        End Set
    End Property
    Public Property propNombreCompleto() As String
        Get
            Return nombreCompleto
        End Get
        Set(value As String)
            nombreCompleto = value
        End Set
    End Property
    Public Property propRFC() As String
        Get
            Return RFC
        End Get
        Set(value As String)
            RFC = value
        End Set
    End Property
    Public Property Correo As String
        Get
            Return _Correo
        End Get
        Set(ByVal value As String)
            _Correo = value
        End Set
    End Property

    Public Property PropNumCliente As String
        Get
            Return _NumCliente
        End Get
        Set(value As String)
            _NumCliente = value
        End Set
    End Property

#End Region

#Region "Metodos"
    Public Sub GetDatosCliente(ByVal solicitud As Integer)
        Dim ds As New DataSet
        Try
            ds = BD.EjecutarQuery("declare @sol int = " & solicitud & "; if (SELECT PDK_ID_PER_JURIDICA	FROM PDK_TAB_SECCION_CERO where PDK_ID_SECCCERO = @sol) in (1,2)  begin select NOMBRE1, 	NOMBRE2, 	APELLIDO_PATERNO, 	APELLIDO_MATERNO,RFC from dbo.PDK_TAB_SOLICITANTE	where PDK_ID_SECCCERO = @sol end else if(SELECT PDK_ID_PER_JURIDICA FROM PDK_TAB_SECCION_CERO where PDK_ID_SECCCERO = @sol) = 3 begin select RAZON_SOCIAL NOMBRE1, '' NOMBRE2, '' APELLIDO_PATERNO, '' APELLIDO_MATERNO from dbo.PDK_TAB_DATOS_MORALES where PDK_ID_SECCCERO = @sol end else begin 	select 'Desconocido' end")
            With ds.Tables(0).Rows(0)
                Me.Nombre1 = .Item("NOMBRE1")
                'BUG-PD-16: MAPH Adición del IIf para corrección en la recuperación del nombre del cliente.
                Me.Nombre2 = IIf(IsDBNull(.Item("NOMBRE2")), "", .Item("NOMBRE2"))
                Me.apellidopaterno = .Item("APELLIDO_PATERNO")
                'BUG-PD-16: MAPH Adición del IIf para corrección en la recuperación del nombre del cliente.
                Me.apellidomaterno = IIf(IsDBNull(.Item("APELLIDO_MATERNO")), "", .Item("APELLIDO_MATERNO"))
                Me.RFC = .Item("RFC")
            End With
            nombreCompleto = Nombre1 & " " & Nombre2 & " " & apellidopaterno & " " & apellidomaterno

        Catch ex As Exception
            nombreCompleto = String.Empty
        End Try
    End Sub

    Public Sub getDatosSol()
        Dim ds As New DataSet
        ds = BD.EjecutarQuery("select * from pdk_tab_seccion_cero where pdk_id_secccero = " & solicitud)
        With ds.Tables(0).Rows(0)
            Me.empresa = .Item("pdk_id_empresa")
            Me.producto = .Item("pdk_id_producto")
            Me.tpersona = .Item("pdk_id_per_juridica")
        End With
    End Sub

    Public Function GetEmailCliente(ByVal sol As Integer) As String
        Dim _datasetresult As New DataSet

        _datasetresult = BD.EjecutarQuery("SELECT  ISNULL(CORREO_ELECTRONICO,'Sin Correo Electrónico') AS CORREO FROM PDK_TAB_SOLICITANTE WHERE PDK_ID_SECCCERO =" & sol)

        If (Not IsNothing(_datasetresult) AndAlso _datasetresult.Tables.Count() > 0 AndAlso _datasetresult.Tables(0).Rows.Count() > 0) Then

            Correo = _datasetresult.Tables(0).Rows(0).Item("CORREO")
        End If

        Return Correo

    End Function

    'RQ-PD18: CGARCIA: 13/12/2017: SE AGREGA NUMERO DE CLIENTE DESDE WS Y BD EN ENCABEZADO DE INFO 
    Public Sub getNumCliente(ByVal opcion As Integer, ByVal solicitud As Integer)
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet

            BD.AgregaParametro("@opcion", TipoDato.Entero, opcion)
            BD.AgregaParametro("@parametro", TipoDato.Entero, solicitud)
            dsres = BD.EjecutaStoredProcedure("spCatalogos")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Me._NumCliente = dsres.Tables(0).Rows(0).Item("CLIENTE_BBVA").ToString
                Else
                    Throw New Exception("No se encontraron datos del caso consultado")
                End If
            Else
                Throw New Exception("Falla al consultar datos")
            End If

        Catch ex As Exception
            _strError = ex.Message.ToString()
            Me._NumCliente = _strError
        End Try
    End Sub

#End Region

End Class
