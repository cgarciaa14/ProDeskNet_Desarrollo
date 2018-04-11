Imports System.Text

Public Class clsParametrosSistema

    '-------------------------- INICIO PDK_PARAMETROS_SISTEMA-------------------------- 
#Region "Variables"
    Private strPDK_ID_PARAMETROS_SISTEMA As Integer = 0
    Private intPDK_PAR_SIS_ID_PADRE As Integer = 0
    Private strPDK_PAR_SIS_PARAMETRO As String = String.Empty
    Private strPDK_PAR_SIS_VALOR_TEXTO As String = String.Empty
    Private strPDK_PAR_SIS_VALOR_FECHA As String = String.Empty
    Private intPDK_PAR_SIS_ORDEN As Integer = 0
    Private intPDK_PAR_SIS_STATUS As Integer = 0
    Private strPDK_PAR_SIS_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_PAR_SIS_VALOR_NUMERO As Integer = 0
#End Region
#Region "Propiedades"
    Public Property PDK_ID_PARAMETROS_SISTEMA() As Integer
        Get
            Return strPDK_ID_PARAMETROS_SISTEMA
        End Get
        Set(ByVal value As Integer)
            strPDK_ID_PARAMETROS_SISTEMA = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_ID_PADRE() As Integer
        Get
            Return intPDK_PAR_SIS_ID_PADRE
        End Get
        Set(ByVal value As Integer)
            intPDK_PAR_SIS_ID_PADRE = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_PARAMETRO() As String
        Get
            Return strPDK_PAR_SIS_PARAMETRO
        End Get
        Set(ByVal value As String)
            strPDK_PAR_SIS_PARAMETRO = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_VALOR_TEXTO() As String
        Get
            Return strPDK_PAR_SIS_VALOR_TEXTO
        End Get
        Set(ByVal value As String)
            strPDK_PAR_SIS_VALOR_TEXTO = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_VALOR_FECHA() As String
        Get
            Return strPDK_PAR_SIS_VALOR_FECHA
        End Get
        Set(ByVal value As String)
            strPDK_PAR_SIS_VALOR_FECHA = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_ORDEN() As Integer
        Get
            Return intPDK_PAR_SIS_ORDEN
        End Get
        Set(ByVal value As Integer)
            intPDK_PAR_SIS_ORDEN = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_STATUS() As Integer
        Get
            Return intPDK_PAR_SIS_STATUS
        End Get
        Set(ByVal value As Integer)
            intPDK_PAR_SIS_STATUS = value
        End Set
    End Property
    Public Property PDK_PAR_SIS_MODIF() As String
        Get
            Return strPDK_PAR_SIS_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_PAR_SIS_MODIF = value
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
    Public Property PDK_PAR_SIS_VALOR_NUMERO() As Integer
        Get
            Return intPDK_PAR_SIS_VALOR_NUMERO
        End Get
        Set(ByVal value As Integer)
            intPDK_PAR_SIS_VALOR_NUMERO = value
        End Set
    End Property
#End Region
#Region "Metodos"
    Public Sub New(ByVal intRegistro As Integer)
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intRegistro = 0 Then Exit Sub
            strSQL.Append(" SELECT ")
            strSQL.Append(" PDK_ID_PARAMETROS_SISTEMA,")
            strSQL.Append(" PDK_PAR_SIS_ID_PADRE,")
            strSQL.Append(" PDK_PAR_SIS_PARAMETRO,")
            strSQL.Append(" ISNULL(PDK_PAR_SIS_VALOR_TEXTO,'') PDK_PAR_SIS_VALOR_TEXTO,")
            strSQL.Append(" ISNULL(PDK_PAR_SIS_VALOR_FECHA, '') PDK_PAR_SIS_VALOR_FECHA,")
            strSQL.Append(" ISNULL(PDK_PAR_SIS_ORDEN,0) PDK_PAR_SIS_ORDEN,")
            strSQL.Append(" PDK_PAR_SIS_STATUS,")
            strSQL.Append(" PDK_PAR_SIS_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" ISNULL(PDK_PAR_SIS_VALOR_NUMERO,'0') PDK_PAR_SIS_VALOR_NUMERO ")
            strSQL.Append(" FROM PDK_PARAMETROS_SISTEMA")
            strSQL.Append(" WHERE PDK_ID_PARAMETROS_SISTEMA = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.strPDK_ID_PARAMETROS_SISTEMA = .Item("PDK_ID_PARAMETROS_SISTEMA")
                Me.intPDK_PAR_SIS_ID_PADRE = .Item("PDK_PAR_SIS_ID_PADRE")
                Me.strPDK_PAR_SIS_PARAMETRO = .Item("PDK_PAR_SIS_PARAMETRO")
                Me.strPDK_PAR_SIS_VALOR_TEXTO = .Item("PDK_PAR_SIS_VALOR_TEXTO")
                Me.strPDK_PAR_SIS_VALOR_FECHA = .Item("PDK_PAR_SIS_VALOR_FECHA")
                Me.intPDK_PAR_SIS_ORDEN = .Item("PDK_PAR_SIS_ORDEN")
                Me.intPDK_PAR_SIS_STATUS = .Item("PDK_PAR_SIS_STATUS")
                Me.strPDK_PAR_SIS_MODIF = .Item("PDK_PAR_SIS_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_PAR_SIS_VALOR_NUMERO = .Item("PDK_PAR_SIS_VALOR_NUMERO")
            End With
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try
    End Sub
    Public Shared Function ObtenTodos(Optional ByVal intPadre As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_PARAMETROS_SISTEMA,")
            strSQL.Append(" A.PDK_PAR_SIS_ID_PADRE,")
            strSQL.Append(" A.PDK_PAR_SIS_PARAMETRO,")
            strSQL.Append(" A.PDK_PAR_SIS_VALOR_TEXTO,")
            strSQL.Append(" A.PDK_PAR_SIS_VALOR_FECHA,")
            strSQL.Append(" A.PDK_PAR_SIS_ORDEN,")
            strSQL.Append(" A.PDK_PAR_SIS_STATUS,")
            strSQL.Append(" A.PDK_PAR_SIS_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_PAR_SIS_VALOR_NUMERO")
            strSQL.Append(" FROM PDK_PARAMETROS_SISTEMA A ")

            If intPadre > 0 Then
                strSQL.Append(" WHERE A.PDK_PAR_SIS_ID_PADRE = " & intPadre & " ")
            End If

            If intPadre = -1 Then
                strSQL.Append(" WHERE A.PDK_PAR_SIS_ID_PADRE = 0 ")
            End If

            strSQL.Append(" ORDER BY A.PDK_ID_PARAMETROS_SISTEMA ")

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_PARAMETROS_SISTEMA")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If strPDK_ID_PARAMETROS_SISTEMA = 0 Then
                Me.strPDK_ID_PARAMETROS_SISTEMA = BD.ObtenConsecutivo("PDK_ID_PARAMETROS_SISTEMA", "PDK_PARAMETROS_SISTEMA", Nothing)
                strSql = "INSERT INTO PDK_PARAMETROS_SISTEMA " & _
                        "(" & _
                        "PDK_ID_PARAMETROS_SISTEMA,PDK_PAR_SIS_ID_PADRE,PDK_PAR_SIS_PARAMETRO,PDK_PAR_SIS_VALOR_TEXTO,PDK_PAR_SIS_VALOR_FECHA,PDK_PAR_SIS_ORDEN,PDK_PAR_SIS_STATUS,PDK_PAR_SIS_MODIF,PDK_CLAVE_USUARIO,PDK_PAR_SIS_VALOR_NUMERO)" & _
                        " VALUES ('" & strPDK_ID_PARAMETROS_SISTEMA & "', " & intPDK_PAR_SIS_ID_PADRE & ", '" & strPDK_PAR_SIS_PARAMETRO & "','" & strPDK_PAR_SIS_VALOR_TEXTO & "','" & strPDK_PAR_SIS_VALOR_FECHA & "', " & intPDK_PAR_SIS_ORDEN & ",  " & intPDK_PAR_SIS_STATUS & ", '" & strPDK_PAR_SIS_MODIF & "', " & intPDK_CLAVE_USUARIO & ",  " & intPDK_PAR_SIS_VALOR_NUMERO & " " & _
                        ")"
            Else
                ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_PARAMETROS_SISTEMA ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_PARAMETROS_SISTEMA " & _
               "SET " & _
                " PDK_PAR_SIS_ID_PADRE = " & intPDK_PAR_SIS_ID_PADRE & ", " & _
                " PDK_PAR_SIS_PARAMETRO = '" & strPDK_PAR_SIS_PARAMETRO & "'," & _
                " PDK_PAR_SIS_VALOR_TEXTO = '" & strPDK_PAR_SIS_VALOR_TEXTO & "'," & _
                " PDK_PAR_SIS_VALOR_FECHA = '" & strPDK_PAR_SIS_VALOR_FECHA & "'," & _
                " PDK_PAR_SIS_ORDEN = " & intPDK_PAR_SIS_ORDEN & ", " & _
                " PDK_PAR_SIS_STATUS = " & intPDK_PAR_SIS_STATUS & ", " & _
                " PDK_PAR_SIS_MODIF = '" & strPDK_PAR_SIS_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
                " PDK_PAR_SIS_VALOR_NUMERO = " & intPDK_PAR_SIS_VALOR_NUMERO & " " & _
                " WHERE PDK_ID_PARAMETROS_SISTEMA=  " & strPDK_ID_PARAMETROS_SISTEMA
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_PARAMETROS_SISTEMA")
        End Try
    End Sub

#End Region
    '-------------------------- FIN PDK_PARAMETROS_SISTEMA-------------------------- 


End Class
