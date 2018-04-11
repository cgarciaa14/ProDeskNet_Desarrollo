Imports ProdeskNet.BD
Imports System.Text

#Region "Trackers"
'BBV-P-423-RQADM-01:MPUESTO:16/05/2017:SELECCION DE CLIENTES
'BUG-PD-69:MPUESTO:01/06/2017:Correcciones
'BUG-PD-79 : ERODRIGUEZ 08/06/2017 : se agrego funcion CargaDatosSolicitanteTodos
#End Region


Public Class clsSolicitante
#Region "Variables"
    Private _ID_SOLICITUD As Int64
    Private _CUSTOMER_ID As String
    Private _NAME As String
    Private _LAST_NAME As String
    Private _MOTHERS_LAST_NAME As String
    Private _BIRTHDAY As Date
    Private _RFC As String
    Private _ADDRESS As String
    Private _strError As String = String.Empty
#End Region

#Region "Properties"
    Public Property ID_SOLICITUD() As Int64
        Get
            Return _ID_SOLICITUD
        End Get
        Set(ByVal value As Int64)
            _ID_SOLICITUD = value
        End Set
    End Property

    Public Property CUSTOMER_ID() As String
        Get
            Return _CUSTOMER_ID
        End Get
        Set(ByVal value As String)
            _CUSTOMER_ID = value
        End Set
    End Property

    Public Property NAME() As String
        Get
            Return _NAME
        End Get
        Set(ByVal value As String)
            _NAME = value
        End Set
    End Property

    Public Property LAST_NAME() As String
        Get
            Return _LAST_NAME
        End Get
        Set(ByVal value As String)
            _LAST_NAME = value
        End Set
    End Property

    Public Property MOTHERS_LAST_NAME() As String
        Get
            Return _MOTHERS_LAST_NAME
        End Get
        Set(ByVal value As String)
            _MOTHERS_LAST_NAME = value
        End Set
    End Property

    Public Property BIRTHDAY() As Date
        Get
            Return _BIRTHDAY
        End Get
        Set(ByVal value As Date)
            _BIRTHDAY = value
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

    Public Property ADDRESS() As String
        Get
            Return _ADDRESS
        End Get
        Set(ByVal value As String)
            _ADDRESS = value
        End Set
    End Property
    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property
#End Region
#Region "Methods"
    Public Function CargaDatosSolicitanteTodos(ByVal intSol As Integer) As DataSet
        Dim dts As DataSet = New DataSet()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder

        Try
            sql.AppendLine("SELECT * ")
            sql.AppendLine("FROM PDK_TAB_SOLICITANTE")
            sql.AppendLine("WHERE PDK_ID_SECCCERO = " & intSol.ToString)

            dts = BD.EjecutarQuery(sql.ToString)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    Return dts
                Else
                    _strError = "Error al consultar la información."
                    Return Nothing
                End If
            Else
                _strError = "Error al consultar la información."
                Return Nothing
            End If

        Catch ex As Exception
            _strError = "Exception: Error al cargar la información."
            Return Nothing
        End Try
    End Function
#End Region
End Class

Public Class clsSolicitantes
    Inherits List(Of clsSolicitante)

#Region "Variables"
    Dim dataManager As New clsManejaBD
    Dim tempResult As New DataSet
#End Region

    Public Function getSolicitante(ByVal ID_SOLICITUD As Int64) As clsSolicitantes
        dataManager = New clsManejaBD()
        dataManager.AgregaParametro("@PDK_ID_SECCCERO", ProdeskNet.BD.TipoDato.Entero, ID_SOLICITUD)
        tempResult = dataManager.EjecutaStoredProcedure("getDatosSolicitante")
        If Not tempResult Is Nothing And tempResult.Tables.Count > 0 Then
            For Each singleRow As DataRow In tempResult.Tables(0).Rows
                Dim strDateTime As String

                If IsDBNull(singleRow("BIRTHDAY")) Or String.IsNullOrWhiteSpace(Convert.ToString(singleRow("BIRTHDAY"))) Then
                    strDateTime = Date.MinValue.ToString()
                Else
                    strDateTime = Convert.ToString(singleRow("BIRTHDAY"))
                End If
                Me.Add(New clsSolicitante() With {.ID_SOLICITUD = Convert.ToInt64(singleRow("ID_SOLICITUD")), _
                                                   .NAME = Convert.ToString(singleRow("NAME")), _
                                                   .LAST_NAME = Convert.ToString(singleRow("LAST_NAME")), _
                                                   .MOTHERS_LAST_NAME = Convert.ToString(singleRow("MOTHERS_LAST_NAME")), _
                                                   .BIRTHDAY = Convert.ToDateTime(strDateTime), _
                                                   .RFC = Convert.ToString(singleRow("RFC")), _
                                                   .ADDRESS = String.Empty
                                                })
            Next
        End If
        Return Me
    End Function

    Public Sub insertaDireccionAlter(ByVal PDK_ID_SECCCERO As Integer, ByVal objSolicitante As clsSolicitante)
        dataManager = New clsManejaBD()
        Try
            dataManager.AgregaParametro("@PDK_ID_SECCCERO", ProdeskNet.BD.TipoDato.Entero, PDK_ID_SECCCERO)
            dataManager.AgregaParametro("@ID_CLIENTE", ProdeskNet.BD.TipoDato.Cadena, IIf(objSolicitante.CUSTOMER_ID Is Nothing, String.Empty, objSolicitante.CUSTOMER_ID))
            dataManager.AgregaParametro("@NOMBRE", ProdeskNet.BD.TipoDato.Cadena, IIf(objSolicitante.NAME Is Nothing, String.Empty, objSolicitante.NAME))
            dataManager.AgregaParametro("@AP_PATERNO", ProdeskNet.BD.TipoDato.Cadena, IIf(objSolicitante.LAST_NAME Is Nothing, String.Empty, objSolicitante.LAST_NAME))
            dataManager.AgregaParametro("@AP_MATERNO", ProdeskNet.BD.TipoDato.Cadena, IIf(objSolicitante.MOTHERS_LAST_NAME Is Nothing, String.Empty, objSolicitante.MOTHERS_LAST_NAME))
            dataManager.AgregaParametro("@RFC", ProdeskNet.BD.TipoDato.Cadena, IIf(objSolicitante.RFC Is Nothing, String.Empty, objSolicitante.RFC))
            dataManager.AgregaParametro("@FECHA_NACIMIENTO", ProdeskNet.BD.TipoDato.Fecha, IIf(objSolicitante.BIRTHDAY = Nothing, String.Empty, objSolicitante.BIRTHDAY))
            dataManager.AgregaParametro("@DIRECCION", ProdeskNet.BD.TipoDato.Cadena, IIf(objSolicitante.ADDRESS Is Nothing, String.Empty, objSolicitante.ADDRESS))

            dataManager.EjecutaStoredProcedure("sp_insertaDirAlternativa")
        Catch ex As Exception
        End Try
    End Sub


  

End Class