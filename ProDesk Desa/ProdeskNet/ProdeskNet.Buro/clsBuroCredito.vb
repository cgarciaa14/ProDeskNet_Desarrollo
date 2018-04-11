Imports System.Text

Public Class clsBuroCredito

'-------------------------- INICIO PDK_BURO_CREDITO-------------------------- 
#Region "Variables"
    Private intPDK_ID_BURO_CREDITO As Integer = 0
    Private strPDK_BUR_CRED_SOLICITUD As String = String.Empty
    Private strPDK_BUR_CRED_FEC_ORIGEN As String = String.Empty
    Private strPDK_BUR_CRED_FEC_CONSULTA As String = String.Empty
    Private strPDK_BUR_CRED_CUENTAS As String = String.Empty
    Private strPDK_BUR_CRED_LIM_CRED As String = String.Empty
    Private strPDK_BUR_CRED_LIM_CRED_MAX As String = String.Empty
    Private strPDK_BUR_CRED_SDO_ACT As String = String.Empty
    Private strPDK_BUR_CRED_SDO_VEN As String = String.Empty
    Private strPDK_BUR_CRED_TOT_PAG As String = String.Empty
    Private strPDK_BUR_CRED_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_ID_BURO As Integer = 0
#End Region
#Region "Propiedades"
    Public Property PDK_ID_BURO_CREDITO() As Integer
        Get
            Return intPDK_ID_BURO_CREDITO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO_CREDITO = value
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
    Public Property PDK_BUR_CRED_FEC_ORIGEN() As String
        Get
            Return strPDK_BUR_CRED_FEC_ORIGEN
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_FEC_ORIGEN = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_FEC_CONSULTA() As String
        Get
            Return strPDK_BUR_CRED_FEC_CONSULTA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_FEC_CONSULTA = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_CUENTAS() As String
        Get
            Return strPDK_BUR_CRED_CUENTAS
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_CUENTAS = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_LIM_CRED() As String
        Get
            Return strPDK_BUR_CRED_LIM_CRED
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_LIM_CRED = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_LIM_CRED_MAX() As String
        Get
            Return strPDK_BUR_CRED_LIM_CRED_MAX
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_LIM_CRED_MAX = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_SDO_ACT() As String
        Get
            Return strPDK_BUR_CRED_SDO_ACT
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_SDO_ACT = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_SDO_VEN() As String
        Get
            Return strPDK_BUR_CRED_SDO_VEN
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_SDO_VEN = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_TOT_PAG() As String
        Get
            Return strPDK_BUR_CRED_TOT_PAG
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_TOT_PAG = value
        End Set
    End Property
    Public Property PDK_BUR_CRED_MODIF() As String
        Get
            Return strPDK_BUR_CRED_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_CRED_MODIF = value
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
    Public Property PDK_ID_BURO() As Integer
        Get
            Return intPDK_ID_BURO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO = value
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
            strSQL.Append(" PDK_ID_BURO_CREDITO,")
            strSQL.Append(" PDK_BUR_CRED_SOLICITUD,")
            strSQL.Append(" PDK_BUR_CRED_FEC_ORIGEN,")
            strSQL.Append(" PDK_BUR_CRED_FEC_CONSULTA,")
            strSQL.Append(" PDK_BUR_CRED_CUENTAS,")
            strSQL.Append(" PDK_BUR_CRED_LIM_CRED,")
            strSQL.Append(" PDK_BUR_CRED_LIM_CRED_MAX,")
            strSQL.Append(" PDK_BUR_CRED_SDO_ACT,")
            strSQL.Append(" PDK_BUR_CRED_SDO_VEN,")
            strSQL.Append(" PDK_BUR_CRED_TOT_PAG,")
            strSQL.Append(" PDK_BUR_CRED_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" PDK_ID_BURO,")
            strSQL.Append(" FROM PDK_BURO_CREDITO")
            strSQL.Append(" WHERE PDK_ID_BURO_CREDITO = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_BURO_CREDITO = .Item("PDK_ID_BURO_CREDITO")
                Me.strPDK_BUR_CRED_SOLICITUD = .Item("PDK_BUR_CRED_SOLICITUD")
                Me.strPDK_BUR_CRED_FEC_ORIGEN = .Item("PDK_BUR_CRED_FEC_ORIGEN")
                Me.strPDK_BUR_CRED_FEC_CONSULTA = .Item("PDK_BUR_CRED_FEC_CONSULTA")
                Me.strPDK_BUR_CRED_CUENTAS = .Item("PDK_BUR_CRED_CUENTAS")
                Me.strPDK_BUR_CRED_LIM_CRED = .Item("PDK_BUR_CRED_LIM_CRED")
                Me.strPDK_BUR_CRED_LIM_CRED_MAX = .Item("PDK_BUR_CRED_LIM_CRED_MAX")
                Me.strPDK_BUR_CRED_SDO_ACT = .Item("PDK_BUR_CRED_SDO_ACT")
                Me.strPDK_BUR_CRED_SDO_VEN = .Item("PDK_BUR_CRED_SDO_VEN")
                Me.strPDK_BUR_CRED_TOT_PAG = .Item("PDK_BUR_CRED_TOT_PAG")
                Me.strPDK_BUR_CRED_MODIF = .Item("PDK_BUR_CRED_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_ID_BURO = .Item("PDK_ID_BURO")
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
            strSQL.Append(" A.PDK_ID_BURO_CREDITO,")
            strSQL.Append(" A.PDK_BUR_CRED_SOLICITUD,")
            strSQL.Append(" A.PDK_BUR_CRED_FEC_ORIGEN,")
            strSQL.Append(" A.PDK_BUR_CRED_FEC_CONSULTA,")
            strSQL.Append(" A.PDK_BUR_CRED_CUENTAS,")
            strSQL.Append(" A.PDK_BUR_CRED_LIM_CRED,")
            strSQL.Append(" A.PDK_BUR_CRED_LIM_CRED_MAX,")
            strSQL.Append(" A.PDK_BUR_CRED_SDO_ACT,")
            strSQL.Append(" A.PDK_BUR_CRED_SDO_VEN,")
            strSQL.Append(" A.PDK_BUR_CRED_TOT_PAG,")
            strSQL.Append(" A.PDK_BUR_CRED_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" FROM PDK_BURO_CREDITO A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_BURO_CREDITO")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_BURO_CREDITO = 0 Then
                Me.intPDK_ID_BURO_CREDITO = BD.ObtenConsecutivo("", "PDK_BURO_CREDITO", Nothing)
                strSql = "INSERT INTO PDK_BURO_CREDITO " & _
                                "(" & _
"PDK_ID_BURO_CREDITO,PDK_BUR_CRED_SOLICITUD,PDK_BUR_CRED_FEC_ORIGEN,PDK_BUR_CRED_FEC_CONSULTA,PDK_BUR_CRED_CUENTAS,PDK_BUR_CRED_LIM_CRED,PDK_BUR_CRED_LIM_CRED_MAX,PDK_BUR_CRED_SDO_ACT,PDK_BUR_CRED_SDO_VEN,PDK_BUR_CRED_TOT_PAG,PDK_BUR_CRED_MODIF,PDK_CLAVE_USUARIO,PDK_ID_BURO,)" & _
" VALUES ( " & intPDK_ID_BURO_CREDITO & ", '" & strPDK_BUR_CRED_SOLICITUD & "','" & strPDK_BUR_CRED_FEC_ORIGEN & "','" & strPDK_BUR_CRED_FEC_CONSULTA & "','" & strPDK_BUR_CRED_CUENTAS & "','" & strPDK_BUR_CRED_LIM_CRED & "','" & strPDK_BUR_CRED_LIM_CRED_MAX & "','" & strPDK_BUR_CRED_SDO_ACT & "','" & strPDK_BUR_CRED_SDO_VEN & "','" & strPDK_BUR_CRED_TOT_PAG & "','" & strPDK_BUR_CRED_MODIF & "', " & intPDK_CLAVE_USUARIO & ",  " & intPDK_ID_BURO & ",  " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_BURO_CREDITO ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_BURO_CREDITO " & _
               "SET " & _
             " PDK_ID_BURO_CREDITO = " & intPDK_ID_BURO_CREDITO & ", " & _
                " PDK_BUR_CRED_SOLICITUD = '" & strPDK_BUR_CRED_SOLICITUD & "'," & _
                " PDK_BUR_CRED_FEC_ORIGEN = '" & strPDK_BUR_CRED_FEC_ORIGEN & "'," & _
                " PDK_BUR_CRED_FEC_CONSULTA = '" & strPDK_BUR_CRED_FEC_CONSULTA & "'," & _
                " PDK_BUR_CRED_CUENTAS = '" & strPDK_BUR_CRED_CUENTAS & "'," & _
                " PDK_BUR_CRED_LIM_CRED = '" & strPDK_BUR_CRED_LIM_CRED & "'," & _
                " PDK_BUR_CRED_LIM_CRED_MAX = '" & strPDK_BUR_CRED_LIM_CRED_MAX & "'," & _
                " PDK_BUR_CRED_SDO_ACT = '" & strPDK_BUR_CRED_SDO_ACT & "'," & _
                " PDK_BUR_CRED_SDO_VEN = '" & strPDK_BUR_CRED_SDO_VEN & "'," & _
                " PDK_BUR_CRED_TOT_PAG = '" & strPDK_BUR_CRED_TOT_PAG & "'," & _
                " PDK_BUR_CRED_MODIF = '" & strPDK_BUR_CRED_MODIF & "'," & _
             " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
             " PDK_ID_BURO = " & intPDK_ID_BURO & ", " & _
             " WHERE PDK_ID_BURO_CREDITO=  " & intPDK_ID_BURO_CREDITO
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_BURO_CREDITO")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_BURO_CREDITO-------------------------- 

End Class
