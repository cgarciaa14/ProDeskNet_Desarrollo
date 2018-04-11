#Region "trackers"
'BBV-P-423: RQSOL-04: AVH: 06/12/2016 SE CREA VENTANA FORZAJES
#End Region

Imports System
Imports System.Data
Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlInputFile

Partial Class aspx_ConsultaDocumento
    Inherits System.Web.UI.UserControl
    Public Property ValorAGuardar As String
    Public Property ValorVentana As String


    Private intPantalla As Integer = 0
    Private intSolicitud As Integer = 0


    Public Property Pantalla() As Integer
        Get
            Return intPantalla
        End Get
        Set(ByVal value As Integer)
            intPantalla = value
        End Set
    End Property
    Public Property NoSolicitud() As Integer
        Get
            Return intSolicitud
        End Get
        Set(ByVal value As Integer)
            intSolicitud = value
        End Set
    End Property

    Protected Sub page_load(sender As Object, e As System.EventArgs) Handles Me.Load


        Dim objFlujos As New clsSolicitudes(0)

        Dim dsDoc As DataSet


        If intSolicitud = 0 Then
            intSolicitud = Context.Session("idsol")
        End If

        objFlujos.PDK_ID_SOLICITUD = intSolicitud
        objFlujos.PDK_ID_PANTALLA = ValorVentana
        objFlujos.PDK_CLAVE_USUARIO = Session("IdUsua")

        dsDoc = objFlujos.ConsultaDocumento(1)


        Me.gridprueba.DataSource = dsDoc
        Me.gridprueba.DataBind()







    End Sub

    Public Function ConsultaDoc(ByVal intOper As Integer) As Boolean
        Dim objFlujos As New clsSolicitudes(0)

        Dim dsDoc As DataSet = New DataSet()
        If intOper = 1 Then
            

            objFlujos.PDK_ID_SOLICITUD = intSolicitud
            objFlujos.PDK_ID_PANTALLA = intPantalla
            objFlujos.PDK_CLAVE_USUARIO = Session("IdUsua")

            dsDoc = objFlujos.ConsultaDocumento(1)

            Me.gridprueba.DataSource = dsDoc.Tables(0)
            Me.gridprueba.DataBind()
        Else

        End If
        Return True

    End Function


End Class
