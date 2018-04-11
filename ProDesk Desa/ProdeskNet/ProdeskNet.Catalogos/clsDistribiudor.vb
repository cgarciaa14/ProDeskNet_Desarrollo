Imports System.Text

Public Class clsDistribuidor
    '-------------------------- INICIO PDK_CAT_DISTRIBUIDOR-------------------------- 
#Region "Trackers"
    'TEL-B-1988:JDRA:No inserta Distribuidor se quita el consecutivo
#End Region

#Region "Variables"
    Private intPDK_ID_DISTRIBUIDOR As Integer = 0
    Private strPDK_DIST_NOMBRE As String = String.Empty
    Private intPDK_ID_DIST_DISTRIBUIDOR As Integer = 0
    Private intPDK_DIST_ACTIVO As Integer = 0
    Private strPDK_DIST_CLAVE As String = String.Empty
    Private strPDK_DIST_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private strPDK_DIST_STATUS As String = String.Empty
    'Private intPDK_ID_EMRPESA As Integer = 0
    Private strPDK_EMP_NOMBRE As String = String.Empty

    Private strPDK_DIST_DISTRIBUIDOR As String = String.Empty
    'Private strDistribuidorStatus As String = String.Empty
    Private boolPDK_FN_DIST_DISTRIBUIDOR_ACTIVO As Boolean = False
    'Private strPDK_STR_DIST_DISTRIBUIDOR_ACTIVO As String = String.Empty
    'Private intPDK_DIST_CLAVE As Integer = 0
    Private intPDK_ID_EMPRESA As Integer = 0
    Private intPDK_ID_PLAZA As Integer = 0

    Private dsDistribuidores As New DataSet
    Private dsAgencias As New DataSet

#End Region
#Region "Propiedades"

    'Public ReadOnly Property PDK_STR_DIST_DISTRIBUIDOR_ACTIVO() As String
    '    Get
    '        Return strPDK_STR_DIST_DISTRIBUIDOR_ACTIVO
    '    End Get
    'End Property
    Public Property dsAgenciasDS() As DataSet
        Get
            Return dsAgencias
        End Get
        Set(value As DataSet)
            dsAgencias = value
        End Set
    End Property

    Public Property PDK_DIST_DISTRIBUIDOR() As String
        Get
            Return strPDK_DIST_DISTRIBUIDOR
        End Get
        Set(value As String)
            strPDK_DIST_DISTRIBUIDOR = value
        End Set
    End Property
    Public Property PDK_FN_DIST_DISTRIBUIDOR_ACTIVO() As Boolean
        Get
            Return boolPDK_FN_DIST_DISTRIBUIDOR_ACTIVO
        End Get
        Set(value As Boolean)
            boolPDK_FN_DIST_DISTRIBUIDOR_ACTIVO = value
        End Set
    End Property

    Public Property DistribuidoresDS() As DataSet
        Get
            Return dsDistribuidores
        End Get
        Set(value As DataSet)
            dsDistribuidores = value
        End Set
    End Property

    Public Property PDK_ID_DISTRIBUIDOR() As Integer
        Get
            Return intPDK_ID_DISTRIBUIDOR
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_DISTRIBUIDOR = value
        End Set
    End Property
    Public Property PDK_DIST_NOMBRE() As String
        Get
            Return strPDK_DIST_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_DIST_NOMBRE = value
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
    Public Property PDK_DIST_ACTIVO() As Integer
        Get
            Return intPDK_DIST_ACTIVO
        End Get
        Set(ByVal value As Integer)
            intPDK_DIST_ACTIVO = value
        End Set
    End Property
    Public Property PDK_DIST_CLAVE() As String
        Get
            Return strPDK_DIST_CLAVE
        End Get
        Set(ByVal value As String)
            strPDK_DIST_CLAVE = value
        End Set
    End Property
    Public Property PDK_DIST_MODIF() As String
        Get
            Return strPDK_DIST_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_DIST_MODIF = value
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

    Public Property PDK_DIST_STATUS() As String
        Get
            Return strPDK_DIST_STATUS
        End Get
        Set(ByVal value As String)
            strPDK_DIST_STATUS = value
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

    Public Property PDK_ID_PLAZA() As Integer
        Get
            Return intPDK_ID_PLAZA
        End Get
        Set(value As Integer)
            intPDK_ID_PLAZA = value
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


#End Region
#Region "Metodos"
    Public Sub obtenAgencia()
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try

            strSQL.Append(" select * from PDK_CAT_DISTRIBUIDOR where PDK_ID_DISTRIBUIDOR = " & intPDK_ID_DISTRIBUIDOR)
            dsAgencias = BD.EjecutarQuery(strSQL.ToString)

            With dsAgencias.Tables(0).Rows(0)
                'Me.intPDK_ID_DIST_DISTRIBUIDOR = .Item("PDK_ID_DIST_DISTRIBUIDOR")
                Me.strPDK_DIST_NOMBRE = .Item("PDK_DIST_NOMBRE")
                Me.intPDK_DIST_ACTIVO = .Item("PDK_DIST_ACTIVO")
                Me.intPDK_ID_DIST_DISTRIBUIDOR = .Item("PDK_ID_DIST_DISTRIBUIDOR")
                'Me.strDistribuidorStatus = .Item("STATUS")
                'Me.intPDK_ID_EMPRESA = .Item("PDK_ID_EMPRESA")
                'Me.intPDK_ID_PLAZA = .Item("PDK_ID_PLAZA")
            End With

        Catch ex As Exception
            objException = New Exception("Error al buscar Agencia")
            Throw objException
        End Try
    End Sub

    Public Sub ObtenDistribuidores()
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try

            strSQL.Append(" declare @empresa int =  " & intPDK_ID_EMPRESA & ",  @distribuidor int = " & intPDK_ID_DIST_DISTRIBUIDOR & ", @plaza int = " & intPDK_ID_PLAZA)
            strSQL.Append(" SELECT  PDK_ID_DIST_DISTRIBUIDOR, dist.PDK_DIST_DISTRIBUIDOR, dist.PDK_ID_PLAZA, PDK_FN_DIST_DISTRIBUIDOR_ACTIVO,  D.PDK_EMP_NOMBRE, D.PDK_ID_EMPRESA, case when PDK_FN_DIST_DISTRIBUIDOR_ACTIVO = 1 then 'ACTIVO' else 'INACTIVO' end PDK_STR_DIST_DISTRIBUIDOR_ACTIVO ")
            strSQL.Append(" FROM PDK_CAT_DISTRIBUIDORES DIST ")
            strSQL.Append(" inner join PDK_CAT_PLAZA p ")
            strSQL.Append(" on dist.PDK_ID_PLAZA = p.PDK_ID_PLAZA ")
            strSQL.Append(" INNER JOIN PDK_CAT_EMPRESAS D ")
            strSQL.Append(" ON D.PDK_ID_EMPRESA = p.PDK_ID_EMPRESA ")
            strSQL.Append(" WHERE case when @empresa = 0  then 0  else D.PDK_ID_EMPRESA  end = @empresa ")
            strSQL.Append(" and case when @distribuidor = 0  then 0  else PDK_ID_DIST_DISTRIBUIDOR  end = @distribuidor ")
            strSQL.Append(" and case when @plaza = 0  then 0  else dist.PDK_ID_PLAZA  end = @plaza ")

            dsDistribuidores = BD.EjecutarQuery(strSQL.ToString)

            With dsDistribuidores.Tables(0).Rows(0)
                'Me.intPDK_ID_DIST_DISTRIBUIDOR = .Item("PDK_ID_DIST_DISTRIBUIDOR")
                Me.strPDK_DIST_DISTRIBUIDOR = .Item("PDK_DIST_DISTRIBUIDOR")
                Me.boolPDK_FN_DIST_DISTRIBUIDOR_ACTIVO = .Item("PDK_FN_DIST_DISTRIBUIDOR_ACTIVO")
                'Me.strDistribuidorStatus = .Item("STATUS")
                Me.intPDK_ID_EMPRESA = .Item("PDK_ID_EMPRESA")
                Me.intPDK_ID_PLAZA = .Item("PDK_ID_PLAZA")                

            End With

        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_DISTRIBUIDOR")
            Throw objException
        End Try
    End Sub
    Public Shared Function ObtenTodos(ByVal intEmpresa As Integer, Optional ByVal intDistribuidor As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_DISTRIBUIDOR,")
            strSQL.Append(" dist.PDK_DIST_DISTRIBUIDOR, ")
            strSQL.Append(" A.PDK_DIST_NOMBRE,")
            strSQL.Append(" A.PDK_DIST_ACTIVO,")
            strSQL.Append(" A.PDK_DIST_CLAVE,")
            strSQL.Append(" A.PDK_DIST_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" B.PDK_PAR_SIS_PARAMETRO 'PDK_DIST_STATUS', ")
            strSQL.Append(" D.PDK_ID_EMPRESA, ")
            strSQL.Append(" D.PDK_EMP_NOMBRE ")
            strSQL.Append(" FROM PDK_CAT_DISTRIBUIDOR A ")
            strSQL.Append(" inner join PDK_CAT_DISTRIBUIDORES DIST ")
            strSQL.Append(" on a.PDK_ID_DIST_DISTRIBUIDOR = dist.PDK_ID_DIST_DISTRIBUIDOR ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA B ON B.PDK_ID_PARAMETROS_SISTEMA = A.PDK_DIST_ACTIVO ")
            strSQL.Append(" INNER JOIN PDK_REL_DIST_EMP C ON C.PDK_ID_DISTRIBUIDOR = A.PDK_ID_DISTRIBUIDOR")
            strSQL.Append(" INNER JOIN PDK_CAT_EMPRESAS D ON D.PDK_ID_EMPRESA = C.PDK_ID_EMPRESA")
            strSQL.Append(" WHERE D.PDK_ID_EMPRESA =" & intEmpresa & "")

            If intDistribuidor > 0 Then
                strSQL.Append(" AND A.PDK_ID_DISTRIBUIDOR = " & intDistribuidor)

            End If

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds

        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_DISTRIBUIDOR")
            Throw objException
        End Try
    End Function

    Public Sub GuardaDistribuidor()
        Dim strSql As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD

        Try
            If intPDK_ID_DIST_DISTRIBUIDOR = 0 Then
                Me.intPDK_ID_DISTRIBUIDOR = BD.ObtenConsecutivo("PDK_ID_DISTRIBUIDOR", "PDK_CAT_DISTRIBUIDOR", Nothing)
                strSql = "INSERT INTO PDK_CAT_DISTRIBUIDORES " & _
                        "(" & _
                        "PDK_DIST_DISTRIBUIDOR,PDK_ID_PLAZA,PDK_FN_DIST_DISTRIBUIDOR_ACTIVO,PDK_FEC_DIST_DISTRIBUIDOR_ALTA)" & _
                        " VALUES ( '" & strPDK_DIST_DISTRIBUIDOR & "', " & intPDK_ID_PLAZA & ", '" & boolPDK_FN_DIST_DISTRIBUIDOR_ACTIVO & "', getdate())"
                BD.EjecutarQuery(strSql)


                'strSql = String.Empty

                'TEL-B-1988:JDRA:No inserta Distribuidor se quita el consecutivo

                'intConsecutivo = BD.ObtenConsecutivo("PDK_ID_REL_DIST_EMP", "PDK_REL_DIST_EMP", Nothing)
                'strSql = " INSERT INTO PDK_REL_DIST_EMP  (PDK_ID_EMPRESA, PDK_ID_DISTRIBUIDOR, PDK_REL_DIST_EMP_MODIF, PDK_CLAVE_USUARIO)" & _
                '        " VALUES (" & Me.intPDK_ID_EMPRESA & "," & Me.intPDK_ID_DISTRIBUIDOR & ",'" & Me.PDK_DIST_MODIF & "'," & Me.intPDK_CLAVE_USUARIO & ")"

                'TEL-B-1988:JDRA:No inserta Distribuidor se quita el consecutivo

                'BD.EjecutarQuery(strSql)

            Else
                ActualizaDistribuidor()
                Exit Sub
            End If
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_CAT_DISTRIBUIDOR ")
        End Try

    End Sub

    Public Sub ActualizaDistribuidor()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try

            strSql = "UPDATE PDK_CAT_DISTRIBUIDORES " & _
               "SET " & _
             " PDK_DIST_DISTRIBUIDOR = '" & strPDK_DIST_DISTRIBUIDOR & "', " & _
             " PDK_ID_PLAZA = " & intPDK_ID_PLAZA & ", " & _
             " PDK_FN_DIST_DISTRIBUIDOR_ACTIVO = " & IIf(boolPDK_FN_DIST_DISTRIBUIDOR_ACTIVO, 1, 0) & _
             " WHERE PDK_ID_DIST_DISTRIBUIDOR=  " & intPDK_ID_DIST_DISTRIBUIDOR
            BD.EjecutarQuery(strSql)

            'strSql = String.Empty
            'strSql = "UPDATE PDK_REL_DIST_EMP " & _
            '       "SET " & _
            '     " PDK_ID_EMPRESA = '" & intPDK_ID_EMPRESA & "'," & _
            '     " PDK_REL_DIST_EMP_MODIF = '" & strPDK_DIST_MODIF & "'," & _
            '     " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & " " & _
            '     " WHERE PDK_ID_DISTRIBUIDOR=  " & intPDK_ID_DISTRIBUIDOR
            'BD.EjecutarQuery(strSql)

        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_DISTRIBUIDOR")
        End Try
    End Sub

    Public Sub Guarda()

        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD

        Dim intConsecutivo As Integer = 0
        Try
            If intPDK_ID_DISTRIBUIDOR = 0 Then
                Me.intPDK_ID_DISTRIBUIDOR = BD.ObtenConsecutivo("PDK_ID_DISTRIBUIDOR", "PDK_CAT_DISTRIBUIDOR", Nothing)
                strSql = "INSERT INTO PDK_CAT_DISTRIBUIDOR " & _
                        "(" & _
                        "PDK_DIST_NOMBRE,PDK_DIST_ACTIVO,PDK_DIST_CLAVE,PDK_DIST_MODIF,PDK_CLAVE_USUARIO, PDK_ID_DIST_DISTRIBUIDOR)" & _
                        " VALUES ( '" & strPDK_DIST_NOMBRE & "', " & intPDK_DIST_ACTIVO & ", '" & Me.intPDK_ID_DISTRIBUIDOR & "','" & strPDK_DIST_MODIF & "', " & intPDK_CLAVE_USUARIO & ", " & intPDK_ID_DIST_DISTRIBUIDOR & _
                        ")"
                BD.EjecutarQuery(strSql)


                strSql = String.Empty

                'TEL-B-1988:JDRA:No inserta Distribuidor se quita el consecutivo

                'intConsecutivo = BD.ObtenConsecutivo("PDK_ID_REL_DIST_EMP", "PDK_REL_DIST_EMP", Nothing)
                strSql = " INSERT INTO PDK_REL_DIST_EMP  (PDK_ID_EMPRESA, PDK_ID_DISTRIBUIDOR, PDK_REL_DIST_EMP_MODIF, PDK_CLAVE_USUARIO)" & _
                        " VALUES (" & Me.intPDK_ID_EMPRESA & "," & Me.intPDK_ID_DISTRIBUIDOR & ",'" & Me.PDK_DIST_MODIF & "'," & Me.intPDK_CLAVE_USUARIO & ")"

                'TEL-B-1988:JDRA:No inserta Distribuidor se quita el consecutivo

                BD.EjecutarQuery(strSql)

            Else
                ActualizaRegistro()
                Exit Sub
            End If

        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_CAT_DISTRIBUIDOR ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_CAT_DISTRIBUIDOR " & _
               "SET " & _
             " PDK_DIST_NOMBRE = '" & strPDK_DIST_NOMBRE & "'," & _
             " PDK_DIST_ACTIVO = " & intPDK_DIST_ACTIVO & ", " & _
             " PDK_DIST_CLAVE = '" & strPDK_DIST_CLAVE & "'," & _
             " PDK_DIST_MODIF = '" & strPDK_DIST_MODIF & "'," & _
             " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
             " PDK_ID_DIST_DISTRIBUIDOR = " & intPDK_ID_DIST_DISTRIBUIDOR & " " & _
             " WHERE PDK_ID_DISTRIBUIDOR=  " & intPDK_ID_DISTRIBUIDOR
            BD.EjecutarQuery(strSql)

            strSql = String.Empty
            strSql = "UPDATE PDK_REL_DIST_EMP " & _
                   "SET " & _
                 " PDK_ID_EMPRESA = '" & intPDK_ID_EMPRESA & "'," & _
                 " PDK_REL_DIST_EMP_MODIF = '" & strPDK_DIST_MODIF & "'," & _
                 " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & " " & _
                 " WHERE PDK_ID_DISTRIBUIDOR=  " & intPDK_ID_DISTRIBUIDOR
            BD.EjecutarQuery(strSql)

        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_DISTRIBUIDOR")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_CAT_DISTRIBUIDOR-------------------------- 


End Class
