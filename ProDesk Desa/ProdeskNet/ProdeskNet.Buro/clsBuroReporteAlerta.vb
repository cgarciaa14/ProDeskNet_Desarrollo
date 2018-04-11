Imports System.Text

Public Class clsBuroReporteAlerta
'-------------------------- INICIO PDK_BURO_REPORTE_ALERTA-------------------------- 
#Region "Variables"
    Private intPDK_ID_BURO_REPORTE_ALERTA As Integer = 0
    Private intPDK_ID_BURO As Integer = 0
    Private strPDK_BUR_REP_ALE_FEC_HR As String = String.Empty
    Private strPDK_BUR_REP_ALE_HR_COD_MEN As String = String.Empty
    Private strPDK_BUR_REP_ALE_HR_NOM_CLAVE As String = String.Empty
    Private strPDK_BUR_REP_ALE_HR2_DESCRIPCION As String = String.Empty
    Private strPDK_BUR_REP_ALE_HR_HI As String = String.Empty
    Private strPDK_BUR_REP_ALE_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
#End Region
#Region "Propiedades"
    Public Property PDK_ID_BURO_REPORTE_ALERTA() As Integer
        Get
            Return intPDK_ID_BURO_REPORTE_ALERTA
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO_REPORTE_ALERTA = value
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
    Public Property PDK_BUR_REP_ALE_FEC_HR() As String
        Get
            Return strPDK_BUR_REP_ALE_FEC_HR
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_ALE_FEC_HR = value
        End Set
    End Property
    Public Property PDK_BUR_REP_ALE_HR_COD_MEN() As String
        Get
            Return strPDK_BUR_REP_ALE_HR_COD_MEN
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_ALE_HR_COD_MEN = value
        End Set
    End Property
    Public Property PDK_BUR_REP_ALE_HR_NOM_CLAVE() As String
        Get
            Return strPDK_BUR_REP_ALE_HR_NOM_CLAVE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_ALE_HR_NOM_CLAVE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_ALE_HR2_DESCRIPCION() As String
        Get
            Return strPDK_BUR_REP_ALE_HR2_DESCRIPCION
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_ALE_HR2_DESCRIPCION = value
        End Set
    End Property
    Public Property PDK_BUR_REP_ALE_HR_HI() As String
        Get
            Return strPDK_BUR_REP_ALE_HR_HI
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_ALE_HR_HI = value
        End Set
    End Property
    Public Property PDK_BUR_REP_ALE_MODIF() As String
        Get
            Return strPDK_BUR_REP_ALE_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_ALE_MODIF = value
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
            strSQL.Append(" PDK_ID_BURO_REPORTE_ALERTA,")
            strSQL.Append(" PDK_ID_BURO,")
            strSQL.Append(" PDK_BUR_REP_ALE_FEC_HR,")
            strSQL.Append(" PDK_BUR_REP_ALE_HR_COD_MEN,")
            strSQL.Append(" PDK_BUR_REP_ALE_HR_NOM_CLAVE,")
            strSQL.Append(" PDK_BUR_REP_ALE_HR2_DESCRIPCION,")
            strSQL.Append(" PDK_BUR_REP_ALE_HR_HI,")
            strSQL.Append(" PDK_BUR_REP_ALE_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_ALERTA")
            strSQL.Append(" WHERE PDK_ID_BURO_REPORTE_ALERTA = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_BURO_REPORTE_ALERTA = .Item("PDK_ID_BURO_REPORTE_ALERTA")
                Me.intPDK_ID_BURO = .Item("PDK_ID_BURO")
                Me.strPDK_BUR_REP_ALE_FEC_HR = .Item("PDK_BUR_REP_ALE_FEC_HR")
                Me.strPDK_BUR_REP_ALE_HR_COD_MEN = .Item("PDK_BUR_REP_ALE_HR_COD_MEN")
                Me.strPDK_BUR_REP_ALE_HR_NOM_CLAVE = .Item("PDK_BUR_REP_ALE_HR_NOM_CLAVE")
                Me.strPDK_BUR_REP_ALE_HR2_DESCRIPCION = .Item("PDK_BUR_REP_ALE_HR2_DESCRIPCION")
                Me.strPDK_BUR_REP_ALE_HR_HI = .Item("PDK_BUR_REP_ALE_HR_HI")
                Me.strPDK_BUR_REP_ALE_MODIF = .Item("PDK_BUR_REP_ALE_MODIF")
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
            strSQL.Append(" A.PDK_ID_BURO_REPORTE_ALERTA,")
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" A.PDK_BUR_REP_ALE_FEC_HR,")
            strSQL.Append(" A.PDK_BUR_REP_ALE_HR_COD_MEN,")
            strSQL.Append(" A.PDK_BUR_REP_ALE_HR_NOM_CLAVE,")
            strSQL.Append(" A.PDK_BUR_REP_ALE_HR2_DESCRIPCION,")
            strSQL.Append(" A.PDK_BUR_REP_ALE_HR_HI,")
            strSQL.Append(" A.PDK_BUR_REP_ALE_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_ALERTA A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_BURO_REPORTE_ALERTA")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_BURO_REPORTE_ALERTA = 0 Then
                Me.intPDK_ID_BURO_REPORTE_ALERTA = BD.ObtenConsecutivo("", "PDK_BURO_REPORTE_ALERTA", Nothing)
                strSql = "INSERT INTO PDK_BURO_REPORTE_ALERTA " & _
                                "(" & _
"PDK_ID_BURO_REPORTE_ALERTA,PDK_ID_BURO,PDK_BUR_REP_ALE_FEC_HR,PDK_BUR_REP_ALE_HR_COD_MEN,PDK_BUR_REP_ALE_HR_NOM_CLAVE,PDK_BUR_REP_ALE_HR2_DESCRIPCION,PDK_BUR_REP_ALE_HR_HI,PDK_BUR_REP_ALE_MODIF,PDK_CLAVE_USUARIO,)" & _
" VALUES ( " & intPDK_ID_BURO_REPORTE_ALERTA & ",  " & intPDK_ID_BURO & ", '" & strPDK_BUR_REP_ALE_FEC_HR & "','" & strPDK_BUR_REP_ALE_HR_COD_MEN & "','" & strPDK_BUR_REP_ALE_HR_NOM_CLAVE & "','" & strPDK_BUR_REP_ALE_HR2_DESCRIPCION & "','" & strPDK_BUR_REP_ALE_HR_HI & "','" & strPDK_BUR_REP_ALE_MODIF & "','" & strPDK_CLAVE_USUARIO & "', " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_BURO_REPORTE_ALERTA ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_BURO_REPORTE_ALERTA " & _
               "SET " & _
             " PDK_ID_BURO_REPORTE_ALERTA = " & intPDK_ID_BURO_REPORTE_ALERTA & ", " & _
             " PDK_ID_BURO = " & intPDK_ID_BURO & ", " & _
                " PDK_BUR_REP_ALE_FEC_HR = '" & strPDK_BUR_REP_ALE_FEC_HR & "'," & _
                " PDK_BUR_REP_ALE_HR_COD_MEN = '" & strPDK_BUR_REP_ALE_HR_COD_MEN & "'," & _
                " PDK_BUR_REP_ALE_HR_NOM_CLAVE = '" & strPDK_BUR_REP_ALE_HR_NOM_CLAVE & "'," & _
                " PDK_BUR_REP_ALE_HR2_DESCRIPCION = '" & strPDK_BUR_REP_ALE_HR2_DESCRIPCION & "'," & _
                " PDK_BUR_REP_ALE_HR_HI = '" & strPDK_BUR_REP_ALE_HR_HI & "'," & _
                " PDK_BUR_REP_ALE_MODIF = '" & strPDK_BUR_REP_ALE_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "'," & _
             " WHERE PDK_ID_BURO_REPORTE_ALERTA=  " & intPDK_ID_BURO_REPORTE_ALERTA
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_BURO_REPORTE_ALERTA")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_BURO_REPORTE_ALERTA-------------------------- 

End Class
