Imports System.Text
Public Class clsBuroReporteDireccion
'-------------------------- INICIO PDK_BURO_REPORTE_DIRECCION-------------------------- 
#Region "Variables"
    Private intPDK_ID_BURO_REPORTE_DIRECCION As Integer = 0
    Private intPDK_ID_BURO As Integer = 0
    Private strPDK_BUR_REP_DIR_PA_DIRECCION As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_DIRECCION2 As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_COL_POB As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_DEL_MUN As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_CIUDAD As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_ESTADO As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_COD_POST As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_FEC_RESIDENCIA As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_NUM_TEL As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_EXT_TEL As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_NUM_FAX As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_TIPO_DOMICILIO As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_IND_EXP_DOM As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_FEC_REPORTE As String = String.Empty
    Private strPDK_BUR_REP_DIR_PA_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
#End Region
#Region "Propiedades"
    Public Property PDK_ID_BURO_REPORTE_DIRECCION() As Integer
        Get
            Return intPDK_ID_BURO_REPORTE_DIRECCION
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO_REPORTE_DIRECCION = value
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
    Public Property PDK_BUR_REP_DIR_PA_DIRECCION() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_DIRECCION
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_DIRECCION = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_DIRECCION2() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_DIRECCION2
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_DIRECCION2 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_COL_POB() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_COL_POB
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_COL_POB = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_DEL_MUN() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_DEL_MUN
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_DEL_MUN = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_CIUDAD() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_CIUDAD
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_CIUDAD = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_ESTADO() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_ESTADO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_ESTADO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_COD_POST() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_COD_POST
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_COD_POST = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_FEC_RESIDENCIA() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_FEC_RESIDENCIA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_FEC_RESIDENCIA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_NUM_TEL() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_NUM_TEL
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_NUM_TEL = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_EXT_TEL() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_EXT_TEL
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_EXT_TEL = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_NUM_FAX() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_NUM_FAX
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_NUM_FAX = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_TIPO_DOMICILIO() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_TIPO_DOMICILIO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_TIPO_DOMICILIO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_IND_EXP_DOM() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_IND_EXP_DOM
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_IND_EXP_DOM = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_FEC_REPORTE() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_FEC_REPORTE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_FEC_REPORTE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_DIR_PA_MODIF() As String
        Get
            Return strPDK_BUR_REP_DIR_PA_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_DIR_PA_MODIF = value
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
#End Region
#Region "Metodos"
    Public Sub New(ByVal intRegistro As Integer)
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intRegistro = 0 Then Exit Sub
            strSQL.Append(" SELECT ")
            strSQL.Append(" PDK_ID_BURO_REPORTE_DIRECCION,")
            strSQL.Append(" PDK_ID_BURO,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_DIRECCION,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_DIRECCION2,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_COL_POB,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_DEL_MUN,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_CIUDAD,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_ESTADO,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_COD_POST,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_FEC_RESIDENCIA,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_NUM_TEL,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_EXT_TEL,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_NUM_FAX,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_TIPO_DOMICILIO,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_IND_EXP_DOM,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_FEC_REPORTE,")
            strSQL.Append(" PDK_BUR_REP_DIR_PA_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_DIRECCION")
            strSQL.Append(" WHERE PDK_ID_BURO_REPORTE_DIRECCION = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_BURO_REPORTE_DIRECCION = .Item("PDK_ID_BURO_REPORTE_DIRECCION")
                Me.intPDK_ID_BURO = .Item("PDK_ID_BURO")
                Me.strPDK_BUR_REP_DIR_PA_DIRECCION = .Item("PDK_BUR_REP_DIR_PA_DIRECCION")
                Me.strPDK_BUR_REP_DIR_PA_DIRECCION2 = .Item("PDK_BUR_REP_DIR_PA_DIRECCION2")
                Me.strPDK_BUR_REP_DIR_PA_COL_POB = .Item("PDK_BUR_REP_DIR_PA_COL_POB")
                Me.strPDK_BUR_REP_DIR_PA_DEL_MUN = .Item("PDK_BUR_REP_DIR_PA_DEL_MUN")
                Me.strPDK_BUR_REP_DIR_PA_CIUDAD = .Item("PDK_BUR_REP_DIR_PA_CIUDAD")
                Me.strPDK_BUR_REP_DIR_PA_ESTADO = .Item("PDK_BUR_REP_DIR_PA_ESTADO")
                Me.strPDK_BUR_REP_DIR_PA_COD_POST = .Item("PDK_BUR_REP_DIR_PA_COD_POST")
                Me.strPDK_BUR_REP_DIR_PA_FEC_RESIDENCIA = .Item("PDK_BUR_REP_DIR_PA_FEC_RESIDENCIA")
                Me.strPDK_BUR_REP_DIR_PA_NUM_TEL = .Item("PDK_BUR_REP_DIR_PA_NUM_TEL")
                Me.strPDK_BUR_REP_DIR_PA_EXT_TEL = .Item("PDK_BUR_REP_DIR_PA_EXT_TEL")
                Me.strPDK_BUR_REP_DIR_PA_NUM_FAX = .Item("PDK_BUR_REP_DIR_PA_NUM_FAX")
                Me.strPDK_BUR_REP_DIR_PA_TIPO_DOMICILIO = .Item("PDK_BUR_REP_DIR_PA_TIPO_DOMICILIO")
                Me.strPDK_BUR_REP_DIR_PA_IND_EXP_DOM = .Item("PDK_BUR_REP_DIR_PA_IND_EXP_DOM")
                Me.strPDK_BUR_REP_DIR_PA_FEC_REPORTE = .Item("PDK_BUR_REP_DIR_PA_FEC_REPORTE")
                Me.strPDK_BUR_REP_DIR_PA_MODIF = .Item("PDK_BUR_REP_DIR_PA_MODIF")
                Me.strPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
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
            strSQL.Append(" A.PDK_ID_BURO_REPORTE_DIRECCION,")
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_DIRECCION,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_DIRECCION2,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_COL_POB,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_DEL_MUN,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_CIUDAD,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_ESTADO,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_COD_POST,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_FEC_RESIDENCIA,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_NUM_TEL,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_EXT_TEL,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_NUM_FAX,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_TIPO_DOMICILIO,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_IND_EXP_DOM,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_FEC_REPORTE,")
            strSQL.Append(" A.PDK_BUR_REP_DIR_PA_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_DIRECCION A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_BURO_REPORTE_DIRECCION")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_BURO_REPORTE_DIRECCION = 0 Then
                Me.intPDK_ID_BURO_REPORTE_DIRECCION = BD.ObtenConsecutivo("", "PDK_BURO_REPORTE_DIRECCION", Nothing)
                strSql = "INSERT INTO PDK_BURO_REPORTE_DIRECCION " & _
                                "(" & _
"PDK_ID_BURO_REPORTE_DIRECCION,PDK_ID_BURO,PDK_BUR_REP_DIR_PA_DIRECCION,PDK_BUR_REP_DIR_PA_DIRECCION2,PDK_BUR_REP_DIR_PA_COL_POB,PDK_BUR_REP_DIR_PA_DEL_MUN,PDK_BUR_REP_DIR_PA_CIUDAD,PDK_BUR_REP_DIR_PA_ESTADO,PDK_BUR_REP_DIR_PA_COD_POST,PDK_BUR_REP_DIR_PA_FEC_RESIDENCIA,PDK_BUR_REP_DIR_PA_NUM_TEL,PDK_BUR_REP_DIR_PA_EXT_TEL,PDK_BUR_REP_DIR_PA_NUM_FAX,PDK_BUR_REP_DIR_PA_TIPO_DOMICILIO,PDK_BUR_REP_DIR_PA_IND_EXP_DOM,PDK_BUR_REP_DIR_PA_FEC_REPORTE,PDK_BUR_REP_DIR_PA_MODIF,PDK_CLAVE_USUARIO,)" & _
" VALUES ( " & intPDK_ID_BURO_REPORTE_DIRECCION & ",  " & intPDK_ID_BURO & ", '" & strPDK_BUR_REP_DIR_PA_DIRECCION & "','" & strPDK_BUR_REP_DIR_PA_DIRECCION2 & "','" & strPDK_BUR_REP_DIR_PA_COL_POB & "','" & strPDK_BUR_REP_DIR_PA_DEL_MUN & "','" & strPDK_BUR_REP_DIR_PA_CIUDAD & "','" & strPDK_BUR_REP_DIR_PA_ESTADO & "','" & strPDK_BUR_REP_DIR_PA_COD_POST & "','" & strPDK_BUR_REP_DIR_PA_FEC_RESIDENCIA & "','" & strPDK_BUR_REP_DIR_PA_NUM_TEL & "','" & strPDK_BUR_REP_DIR_PA_EXT_TEL & "','" & strPDK_BUR_REP_DIR_PA_NUM_FAX & "','" & strPDK_BUR_REP_DIR_PA_TIPO_DOMICILIO & "','" & strPDK_BUR_REP_DIR_PA_IND_EXP_DOM & "','" & strPDK_BUR_REP_DIR_PA_FEC_REPORTE & "','" & strPDK_BUR_REP_DIR_PA_MODIF & "','" & strPDK_CLAVE_USUARIO & "', " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_BURO_REPORTE_DIRECCION ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_BURO_REPORTE_DIRECCION " & _
               "SET " & _
             " PDK_ID_BURO_REPORTE_DIRECCION = " & intPDK_ID_BURO_REPORTE_DIRECCION & ", " & _
             " PDK_ID_BURO = " & intPDK_ID_BURO & ", " & _
                " PDK_BUR_REP_DIR_PA_DIRECCION = '" & strPDK_BUR_REP_DIR_PA_DIRECCION & "'," & _
                " PDK_BUR_REP_DIR_PA_DIRECCION2 = '" & strPDK_BUR_REP_DIR_PA_DIRECCION2 & "'," & _
                " PDK_BUR_REP_DIR_PA_COL_POB = '" & strPDK_BUR_REP_DIR_PA_COL_POB & "'," & _
                " PDK_BUR_REP_DIR_PA_DEL_MUN = '" & strPDK_BUR_REP_DIR_PA_DEL_MUN & "'," & _
                " PDK_BUR_REP_DIR_PA_CIUDAD = '" & strPDK_BUR_REP_DIR_PA_CIUDAD & "'," & _
                " PDK_BUR_REP_DIR_PA_ESTADO = '" & strPDK_BUR_REP_DIR_PA_ESTADO & "'," & _
                " PDK_BUR_REP_DIR_PA_COD_POST = '" & strPDK_BUR_REP_DIR_PA_COD_POST & "'," & _
                " PDK_BUR_REP_DIR_PA_FEC_RESIDENCIA = '" & strPDK_BUR_REP_DIR_PA_FEC_RESIDENCIA & "'," & _
                " PDK_BUR_REP_DIR_PA_NUM_TEL = '" & strPDK_BUR_REP_DIR_PA_NUM_TEL & "'," & _
                " PDK_BUR_REP_DIR_PA_EXT_TEL = '" & strPDK_BUR_REP_DIR_PA_EXT_TEL & "'," & _
                " PDK_BUR_REP_DIR_PA_NUM_FAX = '" & strPDK_BUR_REP_DIR_PA_NUM_FAX & "'," & _
                " PDK_BUR_REP_DIR_PA_TIPO_DOMICILIO = '" & strPDK_BUR_REP_DIR_PA_TIPO_DOMICILIO & "'," & _
                " PDK_BUR_REP_DIR_PA_IND_EXP_DOM = '" & strPDK_BUR_REP_DIR_PA_IND_EXP_DOM & "'," & _
                " PDK_BUR_REP_DIR_PA_FEC_REPORTE = '" & strPDK_BUR_REP_DIR_PA_FEC_REPORTE & "'," & _
                " PDK_BUR_REP_DIR_PA_MODIF = '" & strPDK_BUR_REP_DIR_PA_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "'," & _
             " WHERE PDK_ID_BURO_REPORTE_DIRECCION=  " & intPDK_ID_BURO_REPORTE_DIRECCION
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_BURO_REPORTE_DIRECCION")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_BURO_REPORTE_DIRECCION-------------------------- 

End Class
