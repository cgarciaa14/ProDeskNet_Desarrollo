Imports Microsoft.VisualBasic
Imports System.Data
Imports ProdeskNet.BD
Imports System.Web.UI.WebControls
'Trackers
'BUG-PD-16 MPUESTO 08/03/2016 Corrección del elemento inicial de los dropdown 

Public Class clsParametros
#Region "Variable"
    Private intIdParametro As Integer = 0
    Private intParaPadre As Integer = 0
    Private strParaNombre As String = ""
    Private strParaValorText As String = ""
    Private strParaValorFecha As String = ""
    Private intParaOrden As Integer = 0
    Private intParaStatus As Integer = 0
    Private strParaModi As String = ""
    Private intCveUsu As Integer = 0
    Private intParaValorNumero As Integer = 0
    Private strErrorParam As String = ""
#End Region
#Region "Propiedades"
    Sub New()
    End Sub

    Sub New(ByVal intCveParam As Integer)
        CargaParametro(intCveParam)
    End Sub
    Public ReadOnly Property ErrorParametro() As String
        Get
            Return strErrorParam
        End Get
    End Property
    Public Property PDK_ID_PARAMETROS_SISTEMA As Integer
        Get
            Return intIdParametro
        End Get
        Set(ByVal value As Integer)
            intIdParametro = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_ID_PADRE As Integer
        Get
            Return intParaPadre
        End Get
        Set(ByVal value As Integer)
            intParaPadre = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_PARAMETR As String
        Get
            Return strParaNombre
        End Get
        Set(ByVal value As String)
            strParaNombre = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_VALOR_TEXTO As String
        Get
            Return strParaValorText
        End Get
        Set(ByVal value As String)
            strParaValorText = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_VALOR_FECHA As String
        Get
            Return strParaValorFecha
        End Get
        Set(ByVal value As String)
            strParaValorFecha = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_ORDEN As Integer
        Get
            Return intParaOrden
        End Get
        Set(ByVal value As Integer)
            intParaOrden = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_STATUS As Integer
        Get
            Return intParaStatus
        End Get
        Set(ByVal value As Integer)
            intParaStatus = value

        End Set
    End Property
    Public Property PDK_PAR_SIS_MODIF As String
        Get
            Return strParaModi
        End Get
        Set(ByVal value As String)
            strParaModi = value
        End Set
    End Property
    Public Property PDK_CLAVE_USUARIO As Integer
        Get
            Return intCveUsu
        End Get
        Set(ByVal value As Integer)
            intCveUsu = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_VALOR_NUMERO As Integer
        Get
            Return intParaValorNumero
        End Get
        Set(ByVal value As Integer)
            intParaValorNumero = value
        End Set
    End Property


#End Region
#Region "Metodos"
    Public Sub CargaParametro(ByVal intcvepara As Integer)
        strErrorParam = ""
        Dim dtsResul As New DataSet

        If intcvepara > 0 Then
            Try
                LimpiarParametros()
                intIdParametro = intcvepara
                dtsResul = ManejaParametros(1)
                If dtsResul.Tables.Count > 0 AndAlso dtsResul.Tables(0).Rows.Count > 0 Then
                    With dtsResul.Tables(0).Rows(0)
                        Me.intIdParametro = .Item("PDK_ID_PARAMETROS_SISTEMA").ToString.Trim
                        Me.intParaPadre = .Item("PDK_PAR_SIS_ID_PADRE").ToString.Trim
                        Me.strParaNombre = .Item("PDK_PAR_SIS_PARAMETRO").ToString.Trim
                        Me.strParaValorText = .Item("PDK_PAR_SIS_VALOR_TEXTO").ToString.Trim
                        Me.strParaValorFecha = .Item("PDK_PAR_SIS_VALOR_FECHA").ToString.Trim
                        Me.intParaOrden = .Item("PDK_PAR_SIS_ORDEN").ToString.Trim
                        Me.intParaStatus = .Item("PDK_PAR_SIS_STATUS").ToString.Trim
                        Me.strParaModi = .Item("PDK_PAR_SIS_MODIF").ToString.Trim
                        Me.intCveUsu = .Item("PDK_CLAVE_USUARIO").ToString.Trim
                        Me.intParaValorNumero = .Item("PDK_PAR_SIS_VALOR_NUMERO").ToString.Trim
                    End With

                End If

            Catch ex As Exception
                strErrorParam = ex.Message
            End Try
        End If

    End Sub
    Private Sub LimpiarParametros()
        intIdParametro = 0
        intParaPadre = 0
        strParaNombre = ""
        strParaValorText = ""
        strParaValorFecha = ""
        intParaOrden = 0
        intParaStatus = 0
        strParaModi = ""
        intCveUsu = 0
        intParaValorNumero = ""
        strErrorParam = ""

    End Sub
    Public Function ObtenerParametro(ByVal intParaPadre As Integer) As DataSet
        ObtenerParametro = New DataSet
        Dim strSQL As String = ""
        Dim ManejaBD As New clsManejaBD
        Try
            strSQL = "SELECT A.PDK_ID_PARAMETROS_SISTEMA,A.PDK_PAR_SIS_ID_PADRE,A.PDK_PAR_SIS_PARAMETRO,A.PDK_PAR_SIS_VALOR_TEXTO,A.PDK_PAR_SIS_VALOR_FECHA,A.PDK_PAR_SIS_ORDEN,A.PDK_PAR_SIS_STATUS " & _
                      ",A.PDK_PAR_SIS_MODIF,A.PDK_CLAVE_USUARIO,A.PDK_PAR_SIS_VALOR_NUMERO FROM PDK_PARAMETROS_SISTEMA A WHERE  A.PDK_PAR_SIS_ID_PADRE=" & intParaPadre & ""
            ObtenerParametro = ManejaBD.EjecutarQuery(strSQL)
            Return ObtenerParametro

        Catch ex As Exception
            Throw New Exception("Error al buscar los registros parametros")
        End Try


    End Function
    Public Shared Function ReplaceAll(ByRef texto, ByVal buscar, ByVal remplaza)
        Dim idx = texto.ToString().IndexOf(buscar)
        While (idx <> -1)
            texto = texto.ToString().Replace(buscar, remplaza)
            idx = texto.ToString().IndexOf(buscar, idx)
        End While
        Return texto
    End Function
    Public Function ManejaParametros(ByVal intAccion As Integer) As DataSet
        ManejaParametros = New DataSet
        Try
            Select Case intAccion
                Case 1
                    ManejaParametros = ObtenerParametro(intParaPadre)
                    Return ManejaParametros
                Case 2
                Case 3
                Case 4

            End Select
        Catch ex As Exception
            strErrorParam = ex.Message
        End Try
    End Function
    Public Function ObtenInfoParametros(ByVal intPadre As Integer, _
                                        Optional ByVal intStatus As Integer = 0, _
                                        Optional ByVal dblValNum As Double = 0) As DataSet
        ObtenInfoParametros = New DataSet
        strErrorParam = ""
        Try

            intParaPadre = intPadre
            If intStatus > 0 Then
                intParaStatus = intStatus
            End If
            If dblValNum > 0 Then
                intParaValorNumero = dblValNum
            End If

            ObtenInfoParametros = ManejaParametros(1)
            strErrorParam = ErrorParametro
        Catch ex As Exception
            strErrorParam = ex.Message
        End Try
    End Function
    'BUG-PD-16 MPUESTO 08/03/2016 Corrección del elemento inicial de los dropdown 
    Public Sub LlenaCombos(ByVal dtsSource As DataSet, _
                          ByVal strCol As String, _
                          ByVal strVal As String, _
                          ByRef objCmb As Object, _
                          Optional ByVal blnAgregaBlanco As Boolean = False, _
                          Optional ByVal blnSeleccionaDefault As Boolean = False, _
                          Optional ByVal strDefault As String = "")
        Try
            Dim intR As Integer = 0
            Dim intVal As Integer = 0
            Dim objRow As DataRow

            objCmb.Items.Clear()
            If blnAgregaBlanco Then
                'BUG-PD-16 MPUESTO 08/03/2016 Corrección del elemento inicial de los dropdown 
                objCmb.Items.Insert(0, New System.Web.UI.WebControls.ListItem("< SELECCIONAR >", 0))
                objCmb.AppendDataBoundItems = True
            End If


            If blnSeleccionaDefault Then
                If Trim(strDefault) <> "" Then
                    For Each objRow In dtsSource.Tables(0).Rows
                        If objRow.Item(strDefault) = 1 Then
                            intVal = objRow.Item(strVal)
                            Exit For
                        End If
                    Next
                End If
            End If

            objCmb.DataSource = dtsSource.Tables(0)
            objCmb.DataTextField = strCol
            objCmb.DataValueField = strVal
            objCmb.DataBind()

            If blnAgregaBlanco Then
                objCmb.SelectedValue = ""
            End If

            If blnSeleccionaDefault And intVal > 0 Then
                objCmb.SelectedValue = intVal
            End If

        Catch ex As Exception
            strErrorParam = ex.Message
        End Try
    End Sub
#End Region

End Class
