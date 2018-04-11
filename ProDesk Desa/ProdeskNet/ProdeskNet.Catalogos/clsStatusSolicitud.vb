'tracker:INC-B-1816:JDRA:Status Invertidos
Public Class clsStatusSolicitud
    Dim BD As New ProdeskNet.BD.clsManejaBD
#Region "variables"
    Private StDocumento As String
    Private StCredito As String
#End Region
#Region "Propiedades"
    Public Property PStDocumento As String
        Get
            Return StDocumento
        End Get
        Set(value As String)
            StDocumento = value
        End Set
    End Property
    Public Property PStCredito As String
        Get
            Return StCredito
        End Get
        Set(value As String)
            StCredito = value
        End Set
    End Property
#End Region
#Region "Metodos"
    Public Sub getStatusSol(ByVal solicitud As Integer)
        Dim ds As New DataSet
        ds = BD.EjecutarQuery("select b.PDK_PAR_SIS_PARAMETRO StatusDocumento, c.PDK_PAR_SIS_PARAMETRO StatusCredito from PDK_TAB_SECCION_CERO a inner join pdk_parametros_sistema b on a.PDK_STATUS_DOCUMENTOS = b.PDK_ID_PARAMETROS_SISTEMA inner join pdk_parametros_sistema c on a.PDK_STATUS_CREDITO = c.PDK_ID_PARAMETROS_SISTEMA where pdk_id_secccero = " & solicitud)
        With ds.Tables(0).Rows(0)
            'tracker:INC-B-1816:JDRA:Status Invertidos
            Me.StCredito = .Item("StatusCredito")
            Me.StDocumento = .Item("StatusDocumento")
            'tracker:INC-B-1816:JDRA:Status Invertidos
        End With
    End Sub
#End Region

End Class
