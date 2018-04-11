Imports System.Text

Public Class clsReportes
    '-------------------------- INICIO PDK_REPORTES-------------------------- 
#Region "Variables"

    Private intPDK_ID_REPORTES As Integer = 0
    Private strPDK_REP_NOMBRE_REPORTE As String = String.Empty
    Private strPDK_REP_NOMBRE_PROCEDIMIENTO As String = String.Empty
    Private strPDK_REP_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_REP_ACTIVO As Integer = 0

#End Region
#Region "Propiedades"
    Public Property PDK_ID_REPORTES() As Integer
        Get
            Return intPDK_ID_REPORTES
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_REPORTES = value
        End Set
    End Property
    Public Property PDK_REP_NOMBRE_REPORTE() As String
        Get
            Return strPDK_REP_NOMBRE_REPORTE
        End Get
        Set(ByVal value As String)
            strPDK_REP_NOMBRE_REPORTE = value
        End Set
    End Property
    Public Property PDK_REP_NOMBRE_PROCEDIMIENTO() As String
        Get
            Return strPDK_REP_NOMBRE_PROCEDIMIENTO
        End Get
        Set(ByVal value As String)
            strPDK_REP_NOMBRE_PROCEDIMIENTO = value
        End Set
    End Property
    Public Property PDK_REP_MODIF() As String
        Get
            Return strPDK_REP_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_REP_MODIF = value
        End Set
    End Property
    Public Property PDK_REP_ACTIVO() As Integer
        Get
            Return intPDK_REP_ACTIVO
        End Get
        Set(value As Integer)
            intPDK_REP_ACTIVO = value
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
            strSQL.Append(" PDK_ID_REPORTES,")
            strSQL.Append(" PDK_REP_NOMBRE_REPORTE,")
            strSQL.Append(" PDK_REP_NOMBRE_PROCEDIMIENTO,")
            strSQL.Append(" PDK_REP_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO, ")
            strSQL.Append(" PDK_REP_ACTIVO ")
            strSQL.Append(" FROM PDK_REPORTES")
            strSQL.Append(" WHERE PDK_ID_REPORTES = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_REPORTES = .Item("PDK_ID_REPORTES")
                Me.strPDK_REP_NOMBRE_REPORTE = .Item("PDK_REP_NOMBRE_REPORTE")
                Me.strPDK_REP_NOMBRE_PROCEDIMIENTO = .Item("PDK_REP_NOMBRE_PROCEDIMIENTO")
                Me.strPDK_REP_MODIF = .Item("PDK_REP_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_REP_ACTIVO = .Item("PDK_REP_ACTIVO")
            End With
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try
    End Sub
    Public Shared Function ObtenTodos(Optional ByVal intBandera As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD

        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_REPORTES,")
            strSQL.Append(" A.PDK_REP_NOMBRE_REPORTE,")
            strSQL.Append(" A.PDK_REP_NOMBRE_PROCEDIMIENTO,")
            strSQL.Append(" A.PDK_REP_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append("  P.PDK_PAR_SIS_PARAMETRO ")
            strSQL.Append(" FROM PDK_REPORTES A INNER JOIN PDK_PARAMETROS_SISTEMA P ON P.PDK_ID_PARAMETROS_SISTEMA=A.PDK_REP_ACTIVO AND P.PDK_PAR_SIS_ID_PADRE=1 ")
            If intBandera > 0 Then
                strSQL.Append(" WHERE A.PDK_REP_ACTIVO=2")
            End If

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_REPORTES")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_REPORTES = 0 Then
                Me.intPDK_ID_REPORTES = BD.ObtenConsecutivo("PDK_ID_REPORTES", "PDK_REPORTES", Nothing)
                strSql = "INSERT INTO PDK_REPORTES " & _
                                "(" & _
                            "PDK_ID_REPORTES,PDK_REP_NOMBRE_REPORTE,PDK_REP_NOMBRE_PROCEDIMIENTO,PDK_REP_MODIF,PDK_CLAVE_USUARIO,PDK_REP_ACTIVO)" & _
                            " VALUES ( " & intPDK_ID_REPORTES & ", '" & strPDK_REP_NOMBRE_REPORTE & "','" & strPDK_REP_NOMBRE_PROCEDIMIENTO & "','" & strPDK_REP_MODIF & "', " & intPDK_CLAVE_USUARIO & ", " & intPDK_REP_ACTIVO & " " & _
                            ")"
            Else
                ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
            Me.PDK_ID_REPORTES = intPDK_ID_REPORTES
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_REPORTES ")
        End Try
    End Sub

    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_REPORTES " & _
               "SET " & _
                " PDK_REP_NOMBRE_REPORTE = '" & strPDK_REP_NOMBRE_REPORTE & "'," & _
                " PDK_REP_NOMBRE_PROCEDIMIENTO = '" & strPDK_REP_NOMBRE_PROCEDIMIENTO & "'," & _
                " PDK_REP_MODIF = '" & strPDK_REP_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
                " PDK_REP_ACTIVO= " & intPDK_REP_ACTIVO & " " & _
                " WHERE PDK_ID_REPORTES=  " & intPDK_ID_REPORTES
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_REPORTES")
        End Try
    End Sub

    Public Function ObtenDatosProcedimiento(ByVal intReporte As Integer)

        Dim dsDataset As New DataSet
        Dim strSql As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim strProcedimiento As String


        Try
            strSql = "SELECT PDK_REP_NOMBRE_PROCEDIMIENTO FROM PDK_REPORTES WHERE PDK_ID_REPORTES = " & intReporte & " AND PDK_REP_ACTIVO=2"
            dsDataset = BD.EjecutarQuery(strSql)
            strProcedimiento = dsDataset.Tables(0).Rows(0).Item(0).ToString


            strSql = String.Empty
            strSql = "SELECT "
            strSql &= " CASE a.usertype "
            strSql &= " WHEN 10 THEN a.name + ' ' + b.name + '(' + CONVERT(VARCHAR,a.prec) + ',' + CONVERT(VARCHAR,a.scale) + ')'"
            strSql &= " WHEN 26 THEN a.name + ' ' + b.name + '(' + CONVERT(VARCHAR,a.prec) + ',' + CONVERT(VARCHAR,a.scale) + ')'"
            strSql &= " WHEN 2 THEN a.name + ' ' + b.name + '(' + CONVERT(VARCHAR,a.length) +  '),' "
            strSql &= " ELSE"
            strSql &= " a.name + ' ' + b.name + ''"
            strSql &= " End AS PDK_REP_NOMBRE_PARAMETRO, "
            strSql &= " REPLACE(a.name,'@','') 'PDK_REP_NOMBRE_DATO', "
            strSql &= " b.name 'PDK_REP_NOMBRE_TDATO' "
            strSql &= " FROM syscolumns a"
            strSql &= " inner join systypes b on a.usertype = b.usertype"
            strSql &= " where a.id in ("
            strSql &= " SELECT id FROM sysobjects where name = '" & strProcedimiento.Trim & "')"
            strSql &= " order by a.colid"

            dsDataset = BD.EjecutarQuery(strSql)

        Catch ex As Exception
            dsDataset = Nothing
        End Try
        Return dsDataset

    End Function


    Public Function ObtenDatosReporte(ByVal intReporte As Integer, ByVal arrParametros As Object) As DataSet
        Dim dsDataset As New DataSet
        Dim strSql As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim strProcedimiento As String
        Dim intRegistros As Integer = 0


        Try
            strSql = "SELECT PDK_REP_NOMBRE_PROCEDIMIENTO FROM PDK_REPORTES WHERE PDK_ID_REPORTES = " & intReporte & " AND PDK_REP_ACTIVO=2"
            dsDataset = BD.EjecutarQuery(strSql)
            strProcedimiento = dsDataset.Tables(0).Rows(0).Item(0).ToString


            For intRegistros = 0 To UBound(arrParametros) - 1
                If arrParametros(intRegistros, 0) Is Nothing Then Exit For
                BD.AgregaParametro("@" & arrParametros(intRegistros, 0), ObtenTipoDato(arrParametros(intRegistros, 2)), arrParametros(intRegistros, 1), False)
            Next

            dsDataset = BD.EjecutaStoredProcedure(strProcedimiento)


        Catch ex As Exception

            dsDataset = Nothing
        End Try

        Return dsDataset
    End Function


    Private Function ObtenTipoDato(ByVal strTipoDato As String) As ProdeskNet.BD.TipoDato
        Try
            If strTipoDato = "int" Or strTipoDato = "integer" Then
                Return BD.TipoDato.Entero
            ElseIf strTipoDato = "char" Or strTipoDato = "varchar" Then
                Return BD.TipoDato.Cadena
            ElseIf strTipoDato = "datetime" Then
                Return BD.TipoDato.FechaHora
            ElseIf strTipoDato = "date" Then
                Return BD.TipoDato.Fecha
            ElseIf strTipoDato = "numeric" Then
                Return BD.TipoDato.Flotante
            End If
        Catch ex As Exception

        End Try
    End Function


#End Region
    '-------------------------- FIN PDK_REPORTES-------------------------- 


End Class
