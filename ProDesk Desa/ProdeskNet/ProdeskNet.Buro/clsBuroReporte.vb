Imports System.Text

Public Class clsBuroReporte
'-------------------------- INICIO PDK_BURO_REPORTE-------------------------- 
#Region "Variables"
    Private intPDK_ID_BURO_REPORTE As Integer = 0
    Private strPDK_BUR_CRED_SOLICITUD As String = String.Empty
    Private strPDK_BUR_CRED_PERSONA As String = String.Empty
    Private strPDK_BUR_REP_ENVIO As String = String.Empty
    Private strPDK_BUR_REP_RESPUESTA As String = String.Empty
    Private strPDK_BUR_REP_RESULTADO As String = String.Empty
    Private strPDK_BUR_REP_FEC_RESPUESTA As String = String.Empty
    Private strPDK_BUR_REP_ACTIVO As String = String.Empty
    Private strPDK_BUR_REP_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
    Private intPDK_ID_BURO As Integer = 0
    Private strPDK_BUR_RESPUESTA As String = String.Empty

#End Region
#Region "Propiedades"
    Public Property PDK_ID_BURO_REPORTE() As Integer
        Get
            Return intPDK_ID_BURO_REPORTE
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO_REPORTE = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_SOLICITUD() As String
        Get
            Return strPDK_BUR_CRED_SOLICITUD
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_SOLICITUD = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_PERSONA() As String
        Get
            Return strPDK_BUR_CRED_PERSONA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_PERSONA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_ENVIO() As String
        Get
            Return strPDK_BUR_REP_ENVIO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_ENVIO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RESPUESTA() As String
        Get
            Return strPDK_BUR_REP_RESPUESTA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RESPUESTA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_RESULTADO() As String
        Get
            Return strPDK_BUR_REP_RESULTADO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_RESULTADO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_FEC_RESPUESTA() As String
        Get
            Return strPDK_BUR_REP_FEC_RESPUESTA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_FEC_RESPUESTA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_ACTIVO() As String
        Get
            Return strPDK_BUR_REP_ACTIVO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_ACTIVO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_MODIF() As String
        Get
            Return strPDK_BUR_REP_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_MODIF = value
        End Set
    End Property
    Public Property PDK_CLAVE_USUARIO() As String
        Get
            Return strPDK_CLAVE_USUARIO
        End Get
        Set(ByVal value As String)
            strPDK_CLAVE_USUARIO = value
        End Set
    End Property
    Public Property PDK_ID_BURO() As Integer
        Get
            Return intPDK_ID_BURO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO = value
        End Set
    End Property

    Public Property PDK_BUR_RESPUESTA() As String
        Get
            Return strPDK_BUR_RESPUESTA
        End Get
        Set(value As String)
            strPDK_BUR_RESPUESTA = value
        End Set
    End Property

#End Region
#Region "Metodos"
    Public Sub New(ByVal intRegistro As Integer, Optional ByVal intBuro As Integer = 0)
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intRegistro = 0 Then Exit Sub
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_BURO_REPORTE,")
            strSQL.Append(" A.PDK_BUR_CRED_SOLICITUD,")
            strSQL.Append(" A.PDK_BUR_CRED_PERSONA,")
            strSQL.Append(" A.PDK_BUR_REP_ENVIO,")
            strSQL.Append(" A.PDK_BUR_REP_RESPUESTA,")
            strSQL.Append(" A.PDK_BUR_REP_RESULTADO,")
            strSQL.Append(" A.PDK_BUR_REP_FEC_RESPUESTA,")
            strSQL.Append(" A.PDK_BUR_REP_ACTIVO,")
            strSQL.Append(" A.PDK_BUR_REP_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" B.PDK_BUR_RESPUESTA")
            strSQL.Append(" FROM PDK_BURO_REPORTE A")
            strSQL.Append(" INNER JOIN PDK_BURO_BITACORA B ON B.PDK_ID_BURO = A.PDK_ID_BURO ")
            strSQL.Append(" WHERE PDK_BUR_CRED_SOLICITUD = " & intRegistro)

            If intBuro > 0 Then
                strSQL.Append(" and A.PDK_ID_BURO=" & intBuro)
            End If

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_BURO_REPORTE = .Item("PDK_ID_BURO_REPORTE")
                Me.strPDK_BUR_CRED_SOLICITUD = .Item("PDK_BUR_CRED_SOLICITUD")
                Me.strPDK_BUR_CRED_PERSONA = .Item("PDK_BUR_CRED_PERSONA")
                Me.strPDK_BUR_REP_ENVIO = .Item("PDK_BUR_REP_ENVIO")
                Me.strPDK_BUR_REP_RESPUESTA = .Item("PDK_BUR_REP_RESPUESTA")
                Me.strPDK_BUR_REP_RESULTADO = .Item("PDK_BUR_REP_RESULTADO")
                Me.strPDK_BUR_REP_FEC_RESPUESTA = .Item("PDK_BUR_REP_FEC_RESPUESTA")
                Me.strPDK_BUR_REP_ACTIVO = .Item("PDK_BUR_REP_ACTIVO")
                Me.strPDK_BUR_REP_MODIF = .Item("PDK_BUR_REP_MODIF")
                Me.strPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_ID_BURO = .Item("PDK_ID_BURO")
                Me.strPDK_BUR_RESPUESTA = .Item("PDK_BUR_RESPUESTA")

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
            strSQL.Append(" A.PDK_ID_BURO_REPORTE,")
            strSQL.Append(" A.PDK_BUR_CRED_SOLICITUD,")
            strSQL.Append(" A.PDK_BUR_CRED_PERSONA,")
            strSQL.Append(" A.PDK_BUR_REP_ENVIO,")
            strSQL.Append(" A.PDK_BUR_REP_RESPUESTA,")
            strSQL.Append(" A.PDK_BUR_REP_RESULTADO,")
            strSQL.Append(" A.PDK_BUR_REP_FEC_RESPUESTA,")
            strSQL.Append(" A.PDK_BUR_REP_ACTIVO,")
            strSQL.Append(" A.PDK_BUR_REP_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_BURO_REPORTE")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_BURO_REPORTE = 0 Then
                Me.intPDK_ID_BURO_REPORTE = BD.ObtenConsecutivo("", "PDK_BURO_REPORTE", Nothing)
                strSql = "INSERT INTO PDK_BURO_REPORTE " & _
                                "(" & _
"PDK_ID_BURO_REPORTE,PDK_BUR_CRED_SOLICITUD,PDK_BUR_CRED_PERSONA,PDK_BUR_REP_ENVIO,PDK_BUR_REP_RESPUESTA,PDK_BUR_REP_RESULTADO,PDK_BUR_REP_FEC_RESPUESTA,PDK_BUR_REP_ACTIVO,PDK_BUR_REP_MODIF,PDK_CLAVE_USUARIO,PDK_ID_BURO,)" & _
" VALUES ( " & intPDK_ID_BURO_REPORTE & ", '" & strPDK_BUR_CRED_SOLICITUD & "','" & strPDK_BUR_CRED_PERSONA & "','" & strPDK_BUR_REP_ENVIO & "','" & strPDK_BUR_REP_RESPUESTA & "','" & strPDK_BUR_REP_RESULTADO & "','" & strPDK_BUR_REP_FEC_RESPUESTA & "','" & strPDK_BUR_REP_ACTIVO & "','" & strPDK_BUR_REP_MODIF & "','" & strPDK_CLAVE_USUARIO & "', " & intPDK_ID_BURO & ",  " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_BURO_REPORTE ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_BURO_REPORTE " & _
               "SET " & _
             " PDK_ID_BURO_REPORTE = " & intPDK_ID_BURO_REPORTE & ", " & _
                " PDK_BUR_CRED_SOLICITUD = '" & strPDK_BUR_CRED_SOLICITUD & "'," & _
                " PDK_BUR_CRED_PERSONA = '" & strPDK_BUR_CRED_PERSONA & "'," & _
                " PDK_BUR_REP_ENVIO = '" & strPDK_BUR_REP_ENVIO & "'," & _
                " PDK_BUR_REP_RESPUESTA = '" & strPDK_BUR_REP_RESPUESTA & "'," & _
                " PDK_BUR_REP_RESULTADO = '" & strPDK_BUR_REP_RESULTADO & "'," & _
                " PDK_BUR_REP_FEC_RESPUESTA = '" & strPDK_BUR_REP_FEC_RESPUESTA & "'," & _
                " PDK_BUR_REP_ACTIVO = '" & strPDK_BUR_REP_ACTIVO & "'," & _
                " PDK_BUR_REP_MODIF = '" & strPDK_BUR_REP_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "'," & _
             " PDK_ID_BURO = " & intPDK_ID_BURO & ", " & _
             " WHERE PDK_ID_BURO_REPORTE=  " & intPDK_ID_BURO_REPORTE
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_BURO_REPORTE")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_BURO_REPORTE-------------------------- 

End Class
