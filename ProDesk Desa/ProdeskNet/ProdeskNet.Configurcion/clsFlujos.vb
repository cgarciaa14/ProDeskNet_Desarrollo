Imports System.Text
'RQ-PD25: DCORNEJO: 22/02/2018: OBTENTAREAS Y OBTENSOLICITUD PARA MAPEO TAREAS
Public Class clsFlujos
    '-------------------------- INICIO PDK_CAT_FLUJOS-------------------------- 
#Region "Variables"
    Private intPDK_ID_FLUJOS As Integer = 0
    Private intPDK_ID_PRODUCTOS As Integer = 0
    Private strPDK_FLU_NOMBRE As String = String.Empty
    Private strPDK_FLU_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_FLU_ACTIVO As Integer = 0
    Private intPDK_FLU_ORDEN As Integer = 0
    Private intPDK_ID_PER_JURIDICA As Integer = 0

    Private arrProcesos() As clsProcesos

#End Region
#Region "Propiedades"

    Public Property procesos() As clsProcesos()
        Get
            Return arrProcesos
        End Get
        Set(value As clsProcesos())
            arrProcesos = value
        End Set
    End Property


    Public Property PDK_ID_FLUJOS() As Integer
        Get
            Return intPDK_ID_FLUJOS
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_FLUJOS = value
        End Set
    End Property
    Public Property PDK_ID_PRODUCTOS() As Integer
        Get
            Return intPDK_ID_PRODUCTOS
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_PRODUCTOS = value
        End Set
    End Property
    Public Property PDK_FLU_NOMBRE() As String
        Get
            Return strPDK_FLU_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_FLU_NOMBRE = value
        End Set
    End Property
    Public Property PDK_FLU_MODIF() As String
        Get
            Return strPDK_FLU_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_FLU_MODIF = value
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
    Public Property PDK_FLU_ACTIVO() As Integer
        Get
            Return intPDK_FLU_ACTIVO
        End Get
        Set(ByVal value As Integer)
            intPDK_FLU_ACTIVO = value
        End Set
    End Property
    Public Property PDK_FLU_ORDEN() As Integer
        Get
            Return intPDK_FLU_ORDEN
        End Get
        Set(ByVal value As Integer)
            intPDK_FLU_ORDEN = value
        End Set
    End Property
    Public Property PDK_ID_PER_JURIDICA() As Integer
        Get
            Return intPDK_ID_PER_JURIDICA
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_PER_JURIDICA = value
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
            strSQL.Append(" PDK_ID_FLUJOS,")
            strSQL.Append(" PDK_ID_PRODUCTOS,")
            strSQL.Append(" PDK_FLU_NOMBRE,")
            strSQL.Append(" PDK_FLU_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" PDK_FLU_ACTIVO,")
            strSQL.Append(" PDK_FLU_ORDEN,")
            strSQL.Append(" PDK_ID_PER_JURIDICA")
            strSQL.Append(" FROM PDK_CAT_FLUJOS")
            strSQL.Append(" WHERE PDK_ID_FLUJOS = " & intRegistro)

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_FLUJOS = .Item("PDK_ID_FLUJOS")
                Me.intPDK_ID_PRODUCTOS = .Item("PDK_ID_PRODUCTOS")
                Me.strPDK_FLU_NOMBRE = .Item("PDK_FLU_NOMBRE")
                Me.strPDK_FLU_MODIF = .Item("PDK_FLU_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_FLU_ACTIVO = .Item("PDK_FLU_ACTIVO")
                Me.intPDK_FLU_ORDEN = .Item("PDK_FLU_ORDEN")
                Me.intPDK_ID_PER_JURIDICA = .Item("PDK_ID_PER_JURIDICA")
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
            strSQL.Append("     A.PDK_ID_FLUJOS,")
            strSQL.Append("     A.PDK_ID_PRODUCTOS,")
            strSQL.Append("     A.PDK_FLU_NOMBRE,")
            strSQL.Append("     A.PDK_FLU_MODIF,")
            strSQL.Append("     A.PDK_CLAVE_USUARIO,")
            strSQL.Append("     A.PDK_FLU_ACTIVO,")
            strSQL.Append("     A.PDK_FLU_ORDEN,")
            strSQL.Append("     A.PDK_ID_PER_JURIDICA")
            strSQL.Append(" FROM PDK_CAT_FLUJOS A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_FLUJOS")
            Throw objException
        End Try
    End Function
    Public Shared Function ObtenerFlujoProd(ByVal intProducto As Integer) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append("     A.PDK_ID_FLUJOS,")
            strSQL.Append("     A.PDK_ID_PRODUCTOS,")
            strSQL.Append("     A.PDK_FLU_NOMBRE,")
            strSQL.Append("     A.PDK_FLU_MODIF,")
            strSQL.Append("     A.PDK_CLAVE_USUARIO,")
            strSQL.Append("     A.PDK_FLU_ACTIVO,")
            strSQL.Append("     A.PDK_FLU_ORDEN,")
            strSQL.Append("     A.PDK_ID_PER_JURIDICA, ")
            strSQL.Append("     B.PDK_PROD_NOMBRE, ")
            strSQL.Append("     C.PDK_PER_NOMBRE, ")
            strSQL.Append("     D.PDK_PAR_SIS_PARAMETRO ")
            strSQL.Append(" FROM PDK_CAT_FLUJOS A ")
            strSQL.Append(" INNER JOIN PDK_CAT_PRODUCTOS B ON B.PDK_ID_PRODUCTOS = A.PDK_ID_PRODUCTOS ")
            strSQL.Append(" INNER JOIN PDK_CAT_PER_JURIDICA C ON C.PDK_ID_PER_JURIDICA = A.PDK_ID_PER_JURIDICA ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA D ON D.PDK_ID_PARAMETROS_SISTEMA = A.PDK_FLU_ACTIVO")
            strSQL.Append(" WHERE A.PDK_ID_PRODUCTOS = " & intProducto)
            strSQL.Append(" AND  A.PDK_FLU_ACTIVO=2 ")

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)

            Return ds


        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_FLUJOS")
            Throw objException
        End Try

    End Function
    Public Function ObtenFlujos() As DataSet

        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append("     A.PDK_ID_FLUJOS,")
            strSQL.Append("     A.PDK_ID_PRODUCTOS,")
            strSQL.Append("     A.PDK_FLU_NOMBRE,")
            strSQL.Append("     A.PDK_FLU_MODIF,")
            strSQL.Append("     A.PDK_CLAVE_USUARIO,")
            strSQL.Append("     A.PDK_FLU_ACTIVO,")
            strSQL.Append("     A.PDK_FLU_ORDEN,")
            strSQL.Append("     A.PDK_ID_PER_JURIDICA, ")
            strSQL.Append("     B.PDK_PROD_NOMBRE, ")
            strSQL.Append("     C.PDK_PER_NOMBRE, ")
            strSQL.Append("     D.PDK_PAR_SIS_PARAMETRO ")
            strSQL.Append(" FROM PDK_CAT_FLUJOS A ")
            strSQL.Append(" INNER JOIN PDK_CAT_PRODUCTOS B ON B.PDK_ID_PRODUCTOS = A.PDK_ID_PRODUCTOS ")
            strSQL.Append(" INNER JOIN PDK_CAT_PER_JURIDICA C ON C.PDK_ID_PER_JURIDICA = A.PDK_ID_PER_JURIDICA ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA D ON D.PDK_ID_PARAMETROS_SISTEMA = A.PDK_FLU_ACTIVO")
            strSQL.Append(" WHERE 1=1 ")


            If Me.intPDK_ID_FLUJOS > 0 Then
                strSQL.Append(" AND A.PDK_ID_FLUJOS = " & Me.intPDK_ID_FLUJOS)
            End If


            If Me.intPDK_ID_PER_JURIDICA > 0 Then
                strSQL.Append(" AND A.PDK_ID_PER_JURIDICA = " & Me.intPDK_ID_PER_JURIDICA)
            End If


            If Me.intPDK_ID_PRODUCTOS > 0 Then
                strSQL.Append(" AND A.PDK_ID_PRODUCTOS = " & Me.intPDK_ID_PRODUCTOS)
            End If


            strSQL.Append(" ORDER BY PDK_FLU_ORDEN, PDK_ID_FLUJOS")

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)

            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_FLUJOS")
            Throw objException
        End Try
    End Function

    Public Function ObtenLosFlujos() As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_FLUJOS,")
            strSQL.Append(" A.PDK_ID_PRODUCTOS,")
            strSQL.Append(" A.PDK_FLU_NOMBRE,")
            strSQL.Append(" A.PDK_FLU_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_FLU_ACTIVO,")
            strSQL.Append(" A.PDK_FLU_ORDEN,")
            strSQL.Append(" A.PDK_ID_PER_JURIDICA, ")
            strSQL.Append(" B.PDK_PROC_NOMBRE,")
            strSQL.Append(" B.PDK_PROC_ACTIVO,")
            strSQL.Append(" D.PDK_PAR_SIS_PARAMETRO ")
            strSQL.Append(" FROM PDK_CAT_FLUJOS A")
            strSQL.Append(" INNER JOIN PDK_CAT_PROCESOS B ON B.PDK_ID_PROCESOS = A.PDK_ID_FLUJOS")
            strSQL.Append(" INNER JOIN PDK_CAT_PER_JURIDICA C ON C.PDK_ID_PER_JURIDICA = A.PDK_ID_PER_JURIDICA ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA D ON D.PDK_ID_PARAMETROS_SISTEMA = A.PDK_FLU_ACTIVO")
            strSQL.Append(" WHERE A.PDK_FLU_ACTIVO = 2")

            If Me.intPDK_ID_FLUJOS > 0 Then
                strSQL.Append(" AND A.PDK_ID_FLUJOS = " & Me.intPDK_ID_FLUJOS)
            End If


            If Me.intPDK_ID_PER_JURIDICA > 0 Then
                strSQL.Append(" AND A.PDK_ID_PER_JURIDICA = " & Me.intPDK_ID_PER_JURIDICA)
            End If


            If Me.intPDK_ID_PRODUCTOS > 0 Then
                strSQL.Append(" AND A.PDK_ID_PRODUCTOS = " & Me.intPDK_ID_PRODUCTOS)
            End If


            strSQL.Append(" ORDER BY PDK_FLU_ORDEN, PDK_ID_FLUJOS")

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds

        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_FLUJOS")
            Throw objException
        End Try
    End Function

    Public Sub Guarda()

        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim strSql As String = String.Empty
        Dim objException As Exception = Nothing
        Try
            If intPDK_ID_FLUJOS = 0 Then

                Me.intPDK_ID_FLUJOS = BD.ObtenConsecutivo("PDK_ID_FLUJOS", "PDK_CAT_FLUJOS", Nothing)
                If Me.intPDK_ID_FLUJOS = 0 Then Throw New Exception("Error al obtener el consecutivo de PDK_CAT_FLUJOS") : Exit Sub

                strSql = "INSERT INTO PDK_CAT_FLUJOS " & _
                        "(" & _
                        "PDK_ID_FLUJOS,PDK_ID_PRODUCTOS,PDK_FLU_NOMBRE,PDK_FLU_MODIF,PDK_CLAVE_USUARIO,PDK_FLU_ACTIVO,PDK_FLU_ORDEN,PDK_ID_PER_JURIDICA)" & _
                        " VALUES ( " & intPDK_ID_FLUJOS & ",  " & intPDK_ID_PRODUCTOS & ", '" & strPDK_FLU_NOMBRE & "','" & strPDK_FLU_MODIF & "', " & intPDK_CLAVE_USUARIO & ",  " & intPDK_FLU_ACTIVO & ",  " & intPDK_FLU_ORDEN & ",  " & intPDK_ID_PER_JURIDICA & "  " & _
                        ")"

            Else
                ActualizaRegistro()
                Exit Sub

            End If

            BD.EjecutarQuery(strSql)
            PDK_ID_FLUJOS = Me.intPDK_ID_FLUJOS

        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_CAT_FLUJOS ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_CAT_FLUJOS " & _
               "SET " & _
                " PDK_ID_PRODUCTOS = " & intPDK_ID_PRODUCTOS & ", " & _
                " PDK_FLU_NOMBRE = '" & strPDK_FLU_NOMBRE & "'," & _
                " PDK_FLU_MODIF = '" & strPDK_FLU_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
                " PDK_FLU_ACTIVO = " & intPDK_FLU_ACTIVO & ", " & _
                " PDK_FLU_ORDEN = " & intPDK_FLU_ORDEN & ", " & _
                " PDK_ID_PER_JURIDICA = " & intPDK_ID_PER_JURIDICA & " " & _
                " WHERE PDK_ID_FLUJOS=  " & intPDK_ID_FLUJOS
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_FLUJOS")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_CAT_FLUJOS-------------------------- 
End Class
