Imports System.Text

Public Class clsMoneda
    '-------------------------- INICIO PDK_CAT_MONEDA-------------------------- 
#Region "Variables"
    Private intPDK_ID_MONEDA As Integer = 0
    Private strPDK_MON_NOMBRE As String = String.Empty
    Private decPDK_MON_TIPO_CAMBIO As Decimal = 0
    Private strPDK_MON_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_MON_ACTIVO As Integer = 0
#End Region
#Region "Propiedades"
    Public Property PDK_ID_MONEDA() As Integer
        Get
            Return intPDK_ID_MONEDA
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_MONEDA = value
        End Set
    End Property
    Public Property PDK_MON_NOMBRE() As String
        Get
            Return strPDK_MON_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_MON_NOMBRE = value
        End Set
    End Property
    Public Property PDK_MON_TIPO_CAMBIO() As Decimal
        Get
            Return decPDK_MON_TIPO_CAMBIO
        End Get
        Set(ByVal value As Decimal)
            decPDK_MON_TIPO_CAMBIO = value
        End Set
    End Property
    Public Property PDK_MON_MODIF() As String
        Get
            Return strPDK_MON_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_MON_MODIF = value
        End Set
    End Property
    Public Property PDK_MON_ACTIVO() As Integer
        Get
            Return intPDK_MON_ACTIVO
        End Get
        Set(value As Integer)
            intPDK_MON_ACTIVO = value
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
    Public Sub New(ByVal intRegistro As Integer)
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intRegistro = 0 Then Exit Sub
            strSQL.Append(" SELECT ")
            strSQL.Append(" PDK_ID_MONEDA,")
            strSQL.Append(" PDK_MON_NOMBRE,")
            strSQL.Append(" PDK_MON_TIPO_CAMBIO,")
            strSQL.Append(" PDK_MON_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO, ")
            strSQL.Append(" PDK_MON_ACTIVO ")
            strSQL.Append(" FROM PDK_CAT_MONEDA")
            strSQL.Append(" WHERE PDK_ID_MONEDA = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_MONEDA = .Item("PDK_ID_MONEDA")
                Me.strPDK_MON_NOMBRE = .Item("PDK_MON_NOMBRE")
                Me.decPDK_MON_TIPO_CAMBIO = .Item("PDK_MON_TIPO_CAMBIO")
                Me.strPDK_MON_MODIF = .Item("PDK_MON_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_MON_ACTIVO = .Item("PDK_MON_ACTIVO")
            End With
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try
    End Sub
    Public Shared Function ObtenTodos(Optional ByVal intBaldera As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_MONEDA,")
            strSQL.Append(" A.PDK_MON_NOMBRE,")
            strSQL.Append(" A.PDK_MON_TIPO_CAMBIO,")
            strSQL.Append(" A.PDK_MON_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO, ")
            strSQL.Append(" P.PDK_PAR_SIS_PARAMETRO ")
            strSQL.Append(" FROM PDK_CAT_MONEDA A INNER JOIN PDK_PARAMETROS_SISTEMA P ON P.PDK_ID_PARAMETROS_SISTEMA=A.PDK_MON_ACTIVO AND P.PDK_PAR_SIS_ID_PADRE=1 ")
            If intBaldera > 0 Then
                strSQL.Append(" WHERE A.PDK_MON_ACTIVO=2 ")
            End If
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_MONEDA")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_MONEDA = 0 Then
                Me.intPDK_ID_MONEDA = BD.ObtenConsecutivo("PDK_ID_MONEDA", "PDK_CAT_MONEDA", Nothing)
                strSql = "INSERT INTO PDK_CAT_MONEDA " & _
                                "(" & _
                            "PDK_ID_MONEDA,PDK_MON_NOMBRE,PDK_MON_TIPO_CAMBIO,PDK_MON_MODIF,PDK_CLAVE_USUARIO,PDK_MON_ACTIVO)" & _
                            " VALUES ( " & intPDK_ID_MONEDA & ", '" & strPDK_MON_NOMBRE & "', " & decPDK_MON_TIPO_CAMBIO & ", '" & strPDK_MON_MODIF & "', " & intPDK_CLAVE_USUARIO & ", " & intPDK_MON_ACTIVO & "  " & _
                            ")"
            Else
                ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_CAT_MONEDA ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_CAT_MONEDA " & _
               "SET " & _
             " PDK_MON_NOMBRE = '" & strPDK_MON_NOMBRE & "'," & _
             " PDK_MON_TIPO_CAMBIO= " & decPDK_MON_TIPO_CAMBIO & ", " & _
             " PDK_MON_MODIF = '" & strPDK_MON_MODIF & "'," & _
             " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
             " PDK_MON_ACTIVO= " & intPDK_MON_ACTIVO & " " & _
             " WHERE PDK_ID_MONEDA=  " & intPDK_ID_MONEDA

            BD.EjecutarQuery(strSql)

        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_MONEDA")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_CAT_MONEDA-------------------------- 


End Class
