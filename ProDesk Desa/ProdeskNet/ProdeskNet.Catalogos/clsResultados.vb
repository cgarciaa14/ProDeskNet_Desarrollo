Imports ProdeskNet.BD
Imports System.Text

Public Class clsResultados


    '-------------------------- INICIO PDK_CAT_RESULTADO-------------------------- 
#Region "Variables"
    Private intPDK_ID_CAT_RESULTADO As Integer = 0
    Private strPDK_RES_NOMBRE As String = String.Empty
    Private intPDK_RES_ACTIVO As Integer = 0
    Private strPDK_RES_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
#End Region
#Region "Propiedades"
    Public Property PDK_ID_CAT_RESULTADO() As Integer
        Get
            Return intPDK_ID_CAT_RESULTADO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_CAT_RESULTADO = value
        End Set
    End Property
    Public Property PDK_RES_NOMBRE() As String
        Get
            Return strPDK_RES_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_RES_NOMBRE = value
        End Set
    End Property
    Public Property PDK_RES_ACTIVO() As Integer
        Get
            Return intPDK_RES_ACTIVO
        End Get
        Set(ByVal value As Integer)
            intPDK_RES_ACTIVO = value
        End Set
    End Property
    Public Property PDK_RES_MODIF() As String
        Get
            Return strPDK_RES_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_RES_MODIF = value
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
            strSQL.Append(" PDK_ID_CAT_RESULTADO,")
            strSQL.Append(" PDK_RES_NOMBRE,")
            strSQL.Append(" PDK_RES_ACTIVO,")
            strSQL.Append(" PDK_RES_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO")
            strSQL.Append(" FROM PDK_CAT_RESULTADO")
            strSQL.Append(" WHERE PDK_ID_CAT_RESULTADO = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_CAT_RESULTADO = .Item("PDK_ID_CAT_RESULTADO")
                Me.strPDK_RES_NOMBRE = .Item("PDK_RES_NOMBRE")
                Me.intPDK_RES_ACTIVO = .Item("PDK_RES_ACTIVO")
                Me.strPDK_RES_MODIF = .Item("PDK_RES_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
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
            strSQL.Append(" A.PDK_ID_CAT_RESULTADO,")
            strSQL.Append(" A.PDK_RES_NOMBRE,")
            strSQL.Append(" A.PDK_RES_ACTIVO,")
            strSQL.Append(" A.PDK_RES_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO")
            strSQL.Append(" FROM PDK_CAT_RESULTADO A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_RESULTADO")
            Throw objException
        End Try
    End Function


    Public Function ObtenRechazos() As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_CAT_RESULTADO,")
            strSQL.Append(" A.PDK_RES_NOMBRE,")
            strSQL.Append(" A.PDK_RES_ACTIVO,")
            strSQL.Append(" A.PDK_RES_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" B.PDK_PAR_SIS_PARAMETRO")
            strSQL.Append(" FROM PDK_CAT_RESULTADO A ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA B ON B.PDK_ID_PARAMETROS_SISTEMA = A.PDK_RES_ACTIVO ")

            If Me.intPDK_RES_ACTIVO > 0 Then
                strSQL.Append(" WHERE A.PDK_RES_ACTIVO = " & Me.intPDK_RES_ACTIVO & " ")
            End If


            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_RESULTADO")
            Throw objException
        End Try
    End Function

    Public Sub Guarda()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Try
            If intPDK_ID_CAT_RESULTADO = 0 Then
                Me.intPDK_ID_CAT_RESULTADO = BD.ObtenConsecutivo("PDK_ID_CAT_RESULTADO", "PDK_CAT_RESULTADO", Nothing)
                strSql = "INSERT INTO PDK_CAT_RESULTADO " & _
                        "(" & _
                        "PDK_ID_CAT_RESULTADO,PDK_RES_NOMBRE,PDK_RES_ACTIVO,PDK_RES_MODIF,PDK_CLAVE_USUARIO)" & _
                        " VALUES ( " & intPDK_ID_CAT_RESULTADO & ", '" & strPDK_RES_NOMBRE & "', " & intPDK_RES_ACTIVO & ", '" & strPDK_RES_MODIF & "', " & intPDK_CLAVE_USUARIO & "" & _
                        ")"
            Else
                ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
            PDK_ID_CAT_RESULTADO = Me.intPDK_ID_CAT_RESULTADO
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_CAT_RESULTADO ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_CAT_RESULTADO " & _
               "SET " & _
                " PDK_RES_NOMBRE = '" & strPDK_RES_NOMBRE & "'," & _
                " PDK_RES_ACTIVO = " & intPDK_RES_ACTIVO & ", " & _
                " PDK_RES_MODIF = '" & strPDK_RES_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & " " & _
             " WHERE PDK_ID_CAT_RESULTADO=  " & intPDK_ID_CAT_RESULTADO
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_RESULTADO")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_CAT_RESULTADO-------------------------- 


End Class
