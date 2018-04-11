Imports System.Data.Common
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient


Public Class clsManejaBD
#Region "Manejador"
#Region "Atributos"
    Private objFactory As DbProviderFactory = Nothing
    Private objConex As DbConnection = Nothing
    Private objCommand As DbCommand = Nothing
    Private objAdapter As DbDataAdapter = Nothing

    Private strErrBase As String = ""
#End Region
#Region "Propiedad"
    Sub New()
        Try
            'Dim strProv As String = ConfigurationSettings.AppSettings.Item("Provedor")
            Dim strProv As String = ConfigurationManager.AppSettings("Provedor").ToString
            objFactory = DbProviderFactories.GetFactory(strProv)

            objConex = objFactory.CreateConnection
            objCommand = objFactory.CreateCommand
            objAdapter = objFactory.CreateDataAdapter

        Catch ex As Exception
            strErrBase = ex.Message
        End Try
    End Sub
    Public ReadOnly Property ErrorBD() As String
        Get
            Return strErrBase
        End Get
    End Property


#End Region
#Region "Metodo"
    Public Function AbreConex(Optional ByVal bd As Integer = 0, Optional ByVal dTime As Integer = 0) As SqlClient.SqlConnection
        Dim strConex As String
        'strConex = ConfigurationSettings.AppSettings.Item("Conexion")



        Select Case bd
            Case 0
                'strConex = ConfigurationSettings.AppSettings.Item("Conexion") 'My.Resources.BDProdeskNet
                strConex = ConfigurationManager.AppSettings("Conexion").ToString
            Case 1
                strConex = ConfigurationManager.AppSettings("ConexProcotiza").ToString 'My.Resources.BDProdeskNet 'strConex = My.Resources.BDProcotiza
            Case Else
                strConex = ConfigurationManager.AppSettings("Conexion").ToString
        End Select

        AbreConex = New SqlClient.SqlConnection(strConex)

    End Function

    Public Sub AbreConexion(Optional ByVal BD As Integer = 0)
        Try
            Dim strConex As String
            '= ConfigurationSettings.AppSettings.Item("Conexion")

            Select Case BD
                Case 0
                    strConex = ConfigurationManager.AppSettings("Conexion").ToString 'My.Resources.BDProdeskNetMy.Resources.BDProdeskNet
                Case 1
                    strConex = ConfigurationManager.AppSettings("ConexProcotiza").ToString 'My.Resources.BDProcotiza
                Case Else
                    strConex = ConfigurationManager.AppSettings("Conexion").ToString 'My.Resources.BDProdeskNet
            End Select

            objConex.ConnectionString = strConex
            objConex.Open()

        Catch ex As Exception
            strErrBase = ex.Message
        End Try
    End Sub
    Public Function EjecutarQuery(ByVal strSQL As String, Optional ByVal bd As Integer = 0) As DataSet
        Dim dtsRes = New DataSet
        Dim objSqlCnn As SqlClient.SqlConnection
        Dim objSqlAdap As SqlClient.SqlDataAdapter
        Dim ObjEx As Exception

        objSqlCnn = AbreConex(bd)
        Try
            objSqlAdap = New SqlClient.SqlDataAdapter(strSQL, objSqlCnn)
            objSqlAdap.SelectCommand.CommandTimeout = 0
            objSqlAdap.Fill(dtsRes, "REGISTRO")
            Return dtsRes
        Catch ex As Exception
            ObjEx = New Exception("Error en la comunicación a la base de datos")
            objSqlCnn.Close()
            objSqlCnn = Nothing
            Throw ObjEx
        End Try
    End Function

    Public Function EjecutaStoredProcedure(ByVal strStored As String) As DataSet

        EjecutaStoredProcedure = New DataSet
        AbreConexion()

        If Trim(strErrBase) = "" Then
            Try
                objCommand.CommandText = strStored
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.Connection = objConex

                objAdapter = objFactory.CreateDataAdapter
                objAdapter.SelectCommand = objCommand
                objAdapter.Fill(EjecutaStoredProcedure, "RESULTADO")
                objCommand.Parameters.Clear()

                objConex.Close()
            Catch ex As Exception
                objConex.Close()
                objCommand.Parameters.Clear()
                strErrBase = ex.Message
            End Try
        End If
    End Function

    Public Function EjecutaStoredProcedureCP(ByVal strStored As String, Optional ByVal ParametrosArray() As String = Nothing, Optional ByVal ParametrosValores() As String = Nothing) As DataSet

        'EjecutaStoredProcedure = New DataSet
        'AbreConexion()

        If Trim(strErrBase) = "" Then
            Try
                'objCommand.CommandText = strStored
                'objCommand.CommandType = CommandType.StoredProcedure
                'objCommand.Connection = objConex                

                Dim cmd As New SqlCommand(strStored)
                Dim sqlAdp As New SqlDataAdapter
                Dim ds As New DataSet
                cmd.Connection = AbreConex()
                cmd.Connection.Open()
                'objConex.Open()

                cmd.CommandType = CommandType.StoredProcedure

                If Not ParametrosArray Is Nothing Then
                    Dim parametro As String = ""
                    Dim valor As String = ""
                    Dim count As Integer = 0
                    For Each parametro In ParametrosArray
                        parametro = "@" & RTrim(LTrim(parametro))
                        cmd.Parameters.AddWithValue(parametro, RTrim(LTrim(ParametrosValores(count))))
                        count = count + 1
                    Next
                End If

                sqlAdp.SelectCommand = cmd
                sqlAdp.Fill(ds)

                cmd.Connection.Close()

                Return ds

                'objAdapter = objFactory.CreateDataAdapter
                'objAdapter.SelectCommand = objCommand
                'objAdapter.Fill(EjecutaStoredProcedure, "RESULTADO")
                'objCommand.Parameters.Clear()

            Catch ex As Exception
                objConex.Close()
                objCommand.Parameters.Clear()
                strErrBase = ex.Message
            End Try
        End If
    End Function

    Public Sub AgregaParametro(ByVal nombreParametro As String, _
                          ByVal tipoDato As TipoDato, _
                          ByVal valor As String, _
                          Optional ByVal parametroDeSalida As Boolean = False)
        Try
            Dim objParam As DbParameter

            'creamos el parámetro y le asignamos sus valores
            objParam = objFactory.CreateParameter
            objParam.ParameterName = nombreParametro 'nombre

            objParam.Value = valor

            Select Case tipoDato ' tipo
                Case ProdeskNet.BD.TipoDato.Cadena 'cadena
                    objParam.DbType = DbType.String
                Case ProdeskNet.BD.TipoDato.Entero 'entero
                    objParam.DbType = DbType.Int32
                Case ProdeskNet.BD.TipoDato.Flotante 'flotante
                    objParam.DbType = DbType.Double
                Case ProdeskNet.BD.TipoDato.Fecha 'fecha
                    objParam.DbType = DbType.Date
                Case ProdeskNet.BD.TipoDato.FechaHora 'fecha y hora
                    objParam.DbType = DbType.DateTime
                Case ProdeskNet.BD.TipoDato.FechaHora 'Boolean
                    objParam.DbType = DbType.Boolean
            End Select

            If parametroDeSalida Then
                'dirección
                '    objParam.Direction = ParameterDirection.Output                
                'Else
                objParam.Direction = ParameterDirection.InputOutput
            End If

            objCommand.Parameters.Add(objParam)

        Catch ex As Exception
            strErrBase = ex.Message
        End Try
    End Sub

    Public Function ObtenConsecutivo( _
                ByVal strCampo As String, _
                ByVal strTabla As String, _
                ByRef objTran As SqlClient.SqlTransaction) As Integer

        Dim objSqlCnn As SqlClient.SqlConnection = Nothing
        Dim objSqlCmd As New SqlClient.SqlCommand

        Try
            If objTran Is Nothing Then
                objSqlCnn = AbreConex()
                objSqlCmd.CommandTimeout = 0
                objSqlCmd.Connection = objSqlCnn
            Else
                objSqlCnn = objTran.Connection
                objSqlCmd.CommandTimeout = 0
                objSqlCmd.Connection = objSqlCnn
                objSqlCmd.Transaction = objTran
            End If
            objSqlCmd.CommandType = CommandType.Text
            objSqlCmd.CommandText = "SELECT ISNULL(MAX(" & strCampo & "),0) + 1 FROM " & strTabla & " "

            If objTran Is Nothing Then objSqlCnn.Open()
            Dim valor As Object = objSqlCmd.ExecuteScalar()
            If objTran Is Nothing Then objSqlCnn.Close()

            Return IIf(valor Is DBNull.Value OrElse valor Is Nothing, Nothing, valor)
        Catch ex As Exception
            strErrBase = ex.Message
        End Try

    End Function

    Public Function ExInsUpd(ByVal strSQL As String) As Integer
        Dim objSqlCnn As New SqlClient.SqlConnection
        Dim objSqlCmd As New SqlClient.SqlCommand
        Dim ObjEx As Exception
        Dim objTran As SqlTransaction
        Try

            objSqlCnn = AbreConex()

            objSqlCnn.Open()
            objTran = objSqlCnn.BeginTransaction
            objSqlCmd.Connection = objSqlCnn
            objSqlCmd.CommandType = CommandType.Text
            objSqlCmd.CommandText = strSQL
            objSqlCmd.Transaction = objTran

            Dim intRows As Integer = objSqlCmd.ExecuteNonQuery()
            objSqlCmd.Parameters.Clear()
            objTran.Commit()
            objSqlCnn.Close()

            Return intRows

        Catch ex As Exception
            objTran.Rollback()
            ObjEx = New Exception("Error en la comunicación a la base de datos")
            objSqlCnn.Close()
            objSqlCnn = Nothing
            Return 0
            Throw ObjEx
        End Try
    End Function


#End Region

#End Region
End Class
Public Enum TipoDato
    Cadena = 1
    Entero = 2
    Flotante = 3
    Fecha = 4
    FechaHora = 5
    Booleano = 6    
End Enum


