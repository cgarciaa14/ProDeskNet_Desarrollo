Imports System.Text
Imports ProdeskNet.BD

Public Class clsRechazos

    '-------------------------- INICIO PDK_CAT_RECHAZOS-------------------------- 
#Region "Variables"

    Private intPDK_ID_CAT_RECHAZOS As Integer = 0
    Private strPDK_REC_NOMBRE As String = String.Empty
    Private intPDK_REC_ACTIVO As Integer = 0
    Private strPDK_REC_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private conR As DataSet
    Private RechazoCondicion As Integer

#End Region
#Region "Propiedades"

    Public Property rechcond As Integer
        Get
            Return RechazoCondicion
        End Get
        Set(value As Integer)
            RechazoCondicion = value
        End Set
    End Property

    Public Property CondicionRechazo As DataSet
        Get
            Return conR
        End Get
        Set(value As DataSet)
            conR = value
        End Set
    End Property
    Public Property PDK_ID_CAT_RECHAZOS() As Integer
        Get
            Return intPDK_ID_CAT_RECHAZOS
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_CAT_RECHAZOS = value
        End Set
    End Property
    Public Property PDK_REC_NOMBRE() As String
        Get
            Return strPDK_REC_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_REC_NOMBRE = value
        End Set
    End Property
    Public Property PDK_REC_ACTIVO() As Integer
        Get
            Return intPDK_REC_ACTIVO
        End Get
        Set(ByVal value As Integer)
            intPDK_REC_ACTIVO = value
        End Set
    End Property
    Public Property PDK_REC_MODIF() As String
        Get
            Return strPDK_REC_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_REC_MODIF = value
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

    Public Sub returnRechazo()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        conR = BD.EjecutarQuery("select pdk_id_parametros_sistema id, pdk_par_sis_parametro value from PDK_PARAMETROS_SISTEMA where pdk_par_sis_id_padre = 80")
    End Sub

    Public Sub New(ByVal intRegistro As Integer)
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intRegistro = 0 Then Exit Sub
            strSQL.Append(" SELECT ")
            strSQL.Append(" PDK_ID_CAT_RECHAZOS,")
            strSQL.Append(" PDK_REC_NOMBRE,")
            strSQL.Append(" PDK_REC_ACTIVO,")
            strSQL.Append(" PDK_REC_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO")
            strSQL.Append(" FROM PDK_CAT_RECHAZOS")
            strSQL.Append(" WHERE PDK_ID_CAT_RECHAZOS = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_CAT_RECHAZOS = .Item("PDK_ID_CAT_RECHAZOS")
                Me.strPDK_REC_NOMBRE = .Item("PDK_REC_NOMBRE")
                Me.intPDK_REC_ACTIVO = .Item("PDK_REC_ACTIVO")
                Me.strPDK_REC_MODIF = .Item("PDK_REC_MODIF")
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
            strSQL.Append(" A.PDK_ID_CAT_RECHAZOS,")
            strSQL.Append(" A.PDK_REC_NOMBRE,")
            strSQL.Append(" A.PDK_REC_ACTIVO,")
            strSQL.Append(" A.PDK_REC_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO")
            strSQL.Append(" FROM PDK_CAT_RECHAZOS A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_RECHAZOS")
            Throw objException
        End Try
    End Function

    Public Function ObtenRechazos() As DataSet

        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_CAT_RECHAZOS,")
            strSQL.Append(" A.PDK_REC_NOMBRE,")
            strSQL.Append(" A.PDK_REC_ACTIVO,")
            strSQL.Append(" A.PDK_REC_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" B.PDK_PAR_SIS_PARAMETRO,")
            strSQL.Append(" isnull(C.PDK_PAR_SIS_PARAMETRO, 'NO PARAMETRIZADA') [CONDICION]")
            strSQL.Append(" FROM PDK_CAT_RECHAZOS A ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA B ON B.PDK_ID_PARAMETROS_SISTEMA = PDK_REC_ACTIVO ")
            strSQL.Append(" left outer join PDK_PARAMETROS_SISTEMA c on A.PDK_REC_CONDICIONADO  = c.PDK_ID_PARAMETROS_SISTEMA and c.pdk_par_sis_id_padre = 80")


            If Me.intPDK_REC_ACTIVO > 0 Then
                strSQL.Append(" WHERE A.PDK_REC_ACTIVO = " & Me.intPDK_REC_ACTIVO & " ")
            End If


            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_RECHAZOS")
            Throw objException
        End Try
    End Function

    Public Sub Guarda()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Try
            If intPDK_ID_CAT_RECHAZOS = 0 Then
                Me.intPDK_ID_CAT_RECHAZOS = BD.ObtenConsecutivo("PDK_ID_CAT_RECHAZOS", "PDK_CAT_RECHAZOS", Nothing)
                strSql = "INSERT INTO PDK_CAT_RECHAZOS " & _
                        "(" & _
                        "PDK_ID_CAT_RECHAZOS,PDK_REC_NOMBRE,PDK_REC_ACTIVO,PDK_REC_MODIF,PDK_CLAVE_USUARIO, PDK_REC_CONDICIONADO)" & _
                        " VALUES ( " & intPDK_ID_CAT_RECHAZOS & ", '" & strPDK_REC_NOMBRE & "', " & intPDK_REC_ACTIVO & ", '" & strPDK_REC_MODIF & "', " & intPDK_CLAVE_USUARIO & ", " & RechazoCondicion & _
                        ")"
            Else
                ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
            PDK_ID_CAT_RECHAZOS = Me.intPDK_ID_CAT_RECHAZOS

        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_CAT_RECHAZOS ")
        End Try
    End Sub

    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_CAT_RECHAZOS " & _
               "SET " & _
                " PDK_REC_NOMBRE = '" & strPDK_REC_NOMBRE & "'," & _
                " PDK_REC_ACTIVO = " & intPDK_REC_ACTIVO & ", " & _
                " PDK_REC_MODIF = '" & strPDK_REC_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
                " PDK_REC_CONDICIONADO = " & RechazoCondicion & " " & _
             " WHERE PDK_ID_CAT_RECHAZOS=  " & intPDK_ID_CAT_RECHAZOS
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_RECHAZOS")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_CAT_RECHAZOS-------------------------- 


End Class
