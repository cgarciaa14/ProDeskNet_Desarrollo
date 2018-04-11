'BBV-P-423: RQSOL-03: AVH: 10/11/2016 SE AGREGA FUNCION Maneja_Empresa

Imports System.Text
Public Class clsEmpresa

    Dim BD As New ProdeskNet.BD.clsManejaBD
    '-------------------------- INICIO PDK_CAT_EMPRESAS-------------------------- 
#Region "Variables"

    Private intPDK_ID_EMPRESA As Integer = 0
    Private strPDK_EMP_NOMBRE As String = String.Empty
    Private strPDK_EMP_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_EMP_ACTIVO As Integer = 0
    Private intPDK_ID_DIST_DISTRIBUIDOR As Integer = 0
    Private intPDK_ID_DISTRIBUIDOR As Integer = 0
    Private dsPlazaDS As New DataSet

    Private intPDK_EMP_DEFAULT As Integer = 0
    Private strErrEmpresa As String = ""

#End Region
#Region "Propiedades"
    Public ReadOnly Property ErrorEmpresa() As String
        Get
            Return strErrEmpresa
        End Get
    End Property
    Public Property dsPlaza() As DataSet
        Get
            Return dsPlazaDS
        End Get
        Set(value As DataSet)
            value = dsPlazaDS
        End Set
    End Property
    Public Property PDK_ID_DISTRIBUIDOR() As Integer
        Get
            Return intPDK_ID_DISTRIBUIDOR
        End Get
        Set(value As Integer)
            intPDK_ID_DISTRIBUIDOR = value
        End Set
    End Property
    Public Property PDK_ID_DIST_DISTRIBUIDOR() As Integer
        Get
            Return intPDK_ID_DIST_DISTRIBUIDOR
        End Get
        Set(value As Integer)
            intPDK_ID_DIST_DISTRIBUIDOR = value
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
    Public Property PDK_EMP_NOMBRE() As String
        Get
            Return strPDK_EMP_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_EMP_NOMBRE = value
        End Set
    End Property
    Public Property PDK_EMP_MODIF() As String
        Get
            Return strPDK_EMP_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_EMP_MODIF = value
        End Set
    End Property
    Public Property PDK_EMP_ACTIVO() As Integer
        Get
            Return intPDK_EMP_ACTIVO
        End Get
        Set(value As Integer)
            intPDK_EMP_ACTIVO = value
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
    Public Property PDK_EMP_DEFAULT() As Integer
        Get
            Return intPDK_EMP_DEFAULT
        End Get
        Set(value As Integer)
            intPDK_EMP_DEFAULT = value
        End Set
    End Property
#End Region
#Region "Metodos"
    Public Sub New(ByVal intRegistro As Integer)
        Dim strSQL As New StringBuilder
        'Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intRegistro = 0 Then Exit Sub
            strSQL.Append(" SELECT ")
            strSQL.Append(" PDK_ID_EMPRESA,")
            strSQL.Append(" PDK_EMP_NOMBRE,")
            strSQL.Append(" PDK_EMP_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO, ")
            strSQL.Append(" PDK_EMP_ACTIVO, ")
            strSQL.Append(" PDK_EMP_DEFAULT ")
            strSQL.Append(" FROM PDK_CAT_EMPRESAS")
            strSQL.Append(" WHERE PDK_ID_EMPRESA = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_EMPRESA = .Item("PDK_ID_EMPRESA")
                Me.strPDK_EMP_NOMBRE = .Item("PDK_EMP_NOMBRE")
                Me.strPDK_EMP_MODIF = .Item("PDK_EMP_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_EMP_ACTIVO = .Item("PDK_EMP_ACTIVO")
                Me.intPDK_EMP_DEFAULT = .Item("PDK_EMP_DEFAULT")
            End With
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try
    End Sub

    Public Sub getPlaza()

        Dim strSQL As New StringBuilder
        strSQL.Append("select * from pdk_cat_plaza where PDK_ID_EMPRESA = " & intPDK_ID_EMPRESA & " and PDK_FN_PLAZA_ACTIVO = 1")
        dsPlazaDS = BD.EjecutarQuery(strSQL.ToString)

    End Sub
    Public Shared Function obtenDistribuidor(Optional idDist As Integer = 1) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try

            'Si el idDist es 0 obtiene todos.

            strSQL.Append(" declare @distribuidor int = " & idDist)
            strSQL.Append(" select *")
            strSQL.Append(" from PDK_CAT_DISTRIBUIDORES")
            strSQL.Append(" where PDK_FN_DIST_DISTRIBUIDOR_ACTIVO = 1")
            strSQL.Append(" and case when @distribuidor = 0")
            strSQL.Append(" then @distribuidor")
            strSQL.Append(" else PDK_ID_DIST_DISTRIBUIDOR")
            strSQL.Append(" end = @distribuidor")

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar las agencias.")
            Throw objException
        End Try
    End Function

    Public Sub getDistribuidorActual()
        Dim strSQL As New StringBuilder
        'Dim BD As New ProdeskNet.BD.clsManejaBD
        strSQL.Append(" select PDK_ID_DIST_DISTRIBUIDOR ")
        strSQL.Append(" from PDK_CAT_DISTRIBUIDOR ")
        strSQL.Append(" where PDK_ID_DISTRIBUIDOR = " & intPDK_ID_DISTRIBUIDOR)
        Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
        intPDK_ID_DIST_DISTRIBUIDOR = ds.Tables(0).Rows(0).Item("PDK_ID_DIST_DISTRIBUIDOR")
    End Sub
    Public Shared Function ObtenTodos(Optional intBandera As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_EMPRESA,")
            strSQL.Append(" A.PDK_EMP_NOMBRE,")
            strSQL.Append(" A.PDK_EMP_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO ,")
            strSQL.Append(" P.PDK_PAR_SIS_PARAMETRO ,")
            strSQL.Append(" A.PDK_EMP_DEFAULT  ")
            strSQL.Append(" FROM PDK_CAT_EMPRESAS A INNER JOIN PDK_PARAMETROS_SISTEMA P ON P.PDK_ID_PARAMETROS_SISTEMA=A.PDK_EMP_ACTIVO AND P.PDK_PAR_SIS_ID_PADRE=1  ")
            If intBandera > 0 Then
                strSQL.Append(" WHERE A.PDK_EMP_ACTIVO=2")
            End If
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_EMPRESAS")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        'Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_EMPRESA = 0 Then
                Me.intPDK_ID_EMPRESA = BD.ObtenConsecutivo("PDK_ID_EMPRESA", "PDK_CAT_EMPRESAS", Nothing)
                strSql = "INSERT INTO PDK_CAT_EMPRESAS " & _
                        "(" & _
                        "PDK_ID_EMPRESA,PDK_EMP_NOMBRE,PDK_EMP_MODIF,PDK_CLAVE_USUARIO,PDK_EMP_ACTIVO,PDK_EMP_DEFAULT)" & _
                        " VALUES ( " & intPDK_ID_EMPRESA & ", '" & strPDK_EMP_NOMBRE & "','" & strPDK_EMP_MODIF & "', " & intPDK_CLAVE_USUARIO & ", " & intPDK_EMP_ACTIVO & ", " & intPDK_EMP_DEFAULT & "" & _
                        ")"
            Else
                ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_CAT_EMPRESAS ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        'Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_CAT_EMPRESAS " & _
               "SET " & _
                " PDK_EMP_NOMBRE = '" & strPDK_EMP_NOMBRE & "'," & _
                " PDK_EMP_MODIF = '" & strPDK_EMP_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
                "PDK_EMP_ACTIVO = " & intPDK_EMP_ACTIVO & " " & _
             " WHERE PDK_ID_EMPRESA=  " & intPDK_ID_EMPRESA
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_EMPRESAS")
        End Try
    End Sub

    Public Function ManejaEmpresa(ByVal intOper As Integer) As DataSet
        Dim MB As New ProdeskNet.BD.clsManejaBD
        Try
            strErrEmpresa = ""
        
        Select Case intOper
                Case 1 'CONSULTA
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    MB.AgregaParametro("@idEmp", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_EMPRESA)
                    MB.AgregaParametro("@nom", ProdeskNet.BD.TipoDato.Cadena, strPDK_EMP_NOMBRE)
                    MB.AgregaParametro("@estat", ProdeskNet.BD.TipoDato.Entero, intPDK_EMP_ACTIVO)
                Case 2 'INSERTA
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    MB.AgregaParametro("@idEmp", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_EMPRESA)
                    MB.AgregaParametro("@nom", ProdeskNet.BD.TipoDato.Cadena, strPDK_EMP_NOMBRE)
                    MB.AgregaParametro("@estat", ProdeskNet.BD.TipoDato.Entero, intPDK_EMP_ACTIVO)
                    MB.AgregaParametro("@default", ProdeskNet.BD.TipoDato.Entero, intPDK_EMP_DEFAULT)
                    MB.AgregaParametro("@usu_reg", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
                Case 3 'EDITA
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    MB.AgregaParametro("@idEmp", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_EMPRESA)
                    MB.AgregaParametro("@nom", ProdeskNet.BD.TipoDato.Cadena, strPDK_EMP_NOMBRE)
                    MB.AgregaParametro("@estat", ProdeskNet.BD.TipoDato.Entero, intPDK_EMP_ACTIVO)
                    MB.AgregaParametro("@default", ProdeskNet.BD.TipoDato.Entero, intPDK_EMP_DEFAULT)
                    MB.AgregaParametro("@usu_reg", ProdeskNet.BD.TipoDato.Entero, intPDK_CLAVE_USUARIO)
            End Select

        ManejaEmpresa = MB.EjecutaStoredProcedure("maneja_Empresa")
	If intOper = 2 Then
            intPDK_ID_EMPRESA = ManejaEmpresa.Tables(0).Rows(0).Item("PDK_ID_EMPRESA")
	End If
        Catch ex As Exception
            strErrEmpresa = ex.Message
        End Try

    End Function
#End Region
    '-------------------------- FIN PDK_CAT_EMPRESAS-------------------------- 

End Class
