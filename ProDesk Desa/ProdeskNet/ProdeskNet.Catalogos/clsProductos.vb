'BBV-P-423: RQSOL-03: AVH: 10/11/2016 se agrega funcion ManejaProducto

Imports System.Text

Public Class clsProductos
    '-------------------------- INICIO PDK_CAT_PRODUCTOS-------------------------- 
#Region "Variables"
    Private intPDK_ID_PRODUCTOS As Integer = 0
    Private strPDK_PROD_NOMBRE As String = String.Empty
    Private strPDK_PROD_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_ID_EMPRESA As Integer = 0
    Private intPDK_ID_MONEDA As Integer = 0
    Private intPDK_PROD_ACTIVO As Integer = 0
    Private strErrorProducto As String = ""

    Private intPDK_PROD_DEFAULT As Integer = 0

#End Region
#Region "Propiedades"
    Public Property PDK_ID_PRODUCTOS() As Integer
        Get
            Return intPDK_ID_PRODUCTOS
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_PRODUCTOS = value
        End Set
    End Property
    Public Property PDK_PROD_NOMBRE() As String
        Get
            Return strPDK_PROD_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_PROD_NOMBRE = value
        End Set
    End Property
    Public Property PDK_PROD_MODIF() As String
        Get
            Return strPDK_PROD_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_PROD_MODIF = value
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
    Public Property PDK_ID_EMPRESA() As Integer
        Get
            Return intPDK_ID_EMPRESA
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_EMPRESA = value
        End Set
    End Property
    Public Property PDK_ID_MONEDA() As Integer
        Get
            Return intPDK_ID_MONEDA
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_MONEDA = value
        End Set
    End Property
    Public Property PDK_PROD_ACTIVO() As Integer
        Get
            Return intPDK_PROD_ACTIVO
        End Get
        Set(ByVal value As Integer)
            intPDK_PROD_ACTIVO = value
        End Set
    End Property
    
    Public ReadOnly Property ErrorProducto As String
        Get
            Return strErrorProducto
        End Get
    End Property
    Public Property PDK_PROD_DEFAULT() As Integer
        Get
            Return intPDK_PROD_DEFAULT
        End Get
        Set(value As Integer)
            intPDK_PROD_DEFAULT = value
        End Set
    End Property

#End Region
#Region "Metodos"
    Sub New()
    End Sub
    Public Sub New(ByVal intRegistro As Integer)
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intRegistro = 0 Then Exit Sub
            strSQL.Append(" SELECT ")
            strSQL.Append(" PDK_ID_PRODUCTOS,")
            strSQL.Append(" PDK_PROD_NOMBRE,")
            strSQL.Append(" PDK_PROD_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" PDK_ID_EMPRESA,")
            strSQL.Append(" PDK_ID_MONEDA,")
            strSQL.Append(" PDK_PROD_ACTIVO,")
            strSQL.Append(" PDK_PROD_DEFAULT ")
            strSQL.Append(" FROM PDK_CAT_PRODUCTOS")
            strSQL.Append(" WHERE PDK_ID_PRODUCTOS = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_PRODUCTOS = .Item("PDK_ID_PRODUCTOS")
                Me.strPDK_PROD_NOMBRE = .Item("PDK_PROD_NOMBRE")
                Me.strPDK_PROD_MODIF = .Item("PDK_PROD_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_ID_EMPRESA = .Item("PDK_ID_EMPRESA")
                Me.intPDK_ID_MONEDA = .Item("PDK_ID_MONEDA")
                Me.intPDK_PROD_ACTIVO = .Item("PDK_PROD_ACTIVO")
                Me.intPDK_PROD_DEFAULT = .Item("PDK_PROD_DEFAULT")
            End With
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try
    End Sub
    Public Shared Function ObtenTodos() As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_PRODUCTOS,")
            strSQL.Append(" A.PDK_PROD_NOMBRE,")
            strSQL.Append(" A.PDK_PROD_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_ID_EMPRESA,")
            strSQL.Append(" A.PDK_ID_MONEDA,")
            strSQL.Append(" A.PDK_PROD_ACTIVO,")
            strSQL.Append(" A.PDK_PROD_DEFAULT ")
            strSQL.Append(" FROM PDK_CAT_PRODUCTOS A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_PRODUCTOS")
            Throw objException
        End Try
    End Function
    Public Shared Function ObtenerProductoEmp(ByVal intEmpr As Integer, Optional ByVal intBandera As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        ObtenerProductoEmp = New DataSet
        Dim sqlSQL As String = ""
        Dim ManejaBD As New ProdeskNet.BD.clsManejaBD
        Try
            sqlSQL = "SELECT  A.PDK_ID_PRODUCTOS,A.PDK_PROD_NOMBRE, A.PDK_PROD_MODIF,A.PDK_CLAVE_USUARIO, A.PDK_ID_EMPRESA, A.PDK_ID_MONEDA,B.PDK_EMP_NOMBRE,C.PDK_MON_NOMBRE,P.PDK_PAR_SIS_PARAMETRO " & _
                     " FROM PDK_CAT_PRODUCTOS A INNER JOIN PDK_CAT_EMPRESAS B ON A.PDK_ID_EMPRESA=B.PDK_ID_EMPRESA  INNER JOIN PDK_CAT_MONEDA C ON C.PDK_ID_MONEDA=A.PDK_ID_MONEDA " & _
                     "  INNER JOIN PDK_PARAMETROS_SISTEMA P ON A.PDK_PROD_ACTIVO= P.PDK_ID_PARAMETROS_SISTEMA AND P.PDK_PAR_SIS_ID_PADRE=1 " & _
                     "WHERE A.PDK_ID_EMPRESA =" & intEmpr & " "

            If intBandera > 0 Then
                sqlSQL &= " AND A.PDK_PROD_ACTIVO=2 "
            End If
            sqlSQL &= " ORDER BY A.PDK_ID_PRODUCTOS"

            ObtenerProductoEmp = ManejaBD.EjecutarQuery(sqlSQL)
            Return ObtenerProductoEmp

        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_PRODUCTOS")
            Throw objException
        End Try

    End Function

    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_PRODUCTOS = 0 Then
                Me.intPDK_ID_PRODUCTOS = BD.ObtenConsecutivo("PDK_ID_PRODUCTOS", "PDK_CAT_PRODUCTOS", Nothing)
                strSql = "INSERT INTO PDK_CAT_PRODUCTOS " & _
                                "(" & _
"PDK_ID_PRODUCTOS,PDK_PROD_NOMBRE,PDK_PROD_MODIF,PDK_CLAVE_USUARIO,PDK_ID_EMPRESA,PDK_ID_MONEDA,PDK_PROD_ACTIVO)" & _
" VALUES ( " & intPDK_ID_PRODUCTOS & ", '" & strPDK_PROD_NOMBRE & "','" & strPDK_PROD_MODIF & "', " & intPDK_CLAVE_USUARIO & ",  " & intPDK_ID_EMPRESA & ",  " & intPDK_ID_MONEDA & ", " & intPDK_PROD_ACTIVO & " " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
            PDK_ID_PRODUCTOS = Me.intPDK_ID_PRODUCTOS
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_CAT_PRODUCTOS ")
        End Try
    End Sub
    Public Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_CAT_PRODUCTOS " & _
               "SET " & _
                " PDK_PROD_NOMBRE = '" & strPDK_PROD_NOMBRE & "'," & _
                " PDK_PROD_MODIF = '" & strPDK_PROD_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
                " PDK_ID_EMPRESA = " & intPDK_ID_EMPRESA & ", " & _
                " PDK_ID_MONEDA = " & intPDK_ID_MONEDA & ", " & _
                " PDK_PROD_ACTIVO= " & intPDK_PROD_ACTIVO & " " & _
             " WHERE PDK_ID_PRODUCTOS=  " & intPDK_ID_PRODUCTOS
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_PRODUCTOS")
        End Try
    End Sub
    Public Function ManejaProducto(ByVal intOper As Integer) As DataSet
        Dim MB As New ProdeskNet.BD.clsManejaBD
        Try
            strErrorProducto = ""

            Select Case intOper
                Case 1 'CONSULTA
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    MB.AgregaParametro("@idProd", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PRODUCTOS)
                Case 2 'INSERTA
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    MB.AgregaParametro("@idProd", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PRODUCTOS)
                    MB.AgregaParametro("@nom", ProdeskNet.BD.TipoDato.Cadena, strPDK_PROD_NOMBRE)
                    MB.AgregaParametro("@idEmp", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_EMPRESA)
                    MB.AgregaParametro("@idMon", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_MONEDA)
                    MB.AgregaParametro("@feMod", ProdeskNet.BD.TipoDato.Cadena, strPDK_PROD_MODIF)
                    MB.AgregaParametro("@estat", ProdeskNet.BD.TipoDato.Entero, intPDK_PROD_ACTIVO)
                    MB.AgregaParametro("@usu_reg", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                    MB.AgregaParametro("@default", ProdeskNet.BD.TipoDato.Entero, intPDK_PROD_DEFAULT)
                Case 3 'EDITA
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    MB.AgregaParametro("@idProd", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PRODUCTOS)
                    MB.AgregaParametro("@nom", ProdeskNet.BD.TipoDato.Cadena, strPDK_PROD_NOMBRE)
                    MB.AgregaParametro("@idEmp", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_EMPRESA)
                    MB.AgregaParametro("@idMon", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_MONEDA)
                    MB.AgregaParametro("@feMod", ProdeskNet.BD.TipoDato.Cadena, strPDK_PROD_MODIF)
                    MB.AgregaParametro("@estat", ProdeskNet.BD.TipoDato.Entero, intPDK_PROD_ACTIVO)
                    MB.AgregaParametro("@usu_reg", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                    MB.AgregaParametro("@default", ProdeskNet.BD.TipoDato.Entero, intPDK_PROD_DEFAULT)
            End Select

            ManejaProducto = MB.EjecutaStoredProcedure("maneja_Producto")
	If intOper = 2 Then
            intPDK_ID_PRODUCTOS = ManejaProducto.Tables(0).Rows(0).Item("PDK_ID_PRODUCTOS")
	End If
        Catch ex As Exception
            strErrorProducto = ex.Message
        End Try

    End Function
#End Region
    '-------------------------- FIN PDK_CAT_PRODUCTOS-------------------------- 
   
End Class
