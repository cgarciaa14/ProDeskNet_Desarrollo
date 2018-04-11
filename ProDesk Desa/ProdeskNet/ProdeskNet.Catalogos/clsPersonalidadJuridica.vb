'BBV-P-423: RQSOL-03: AVH: 10/11/2016 Se agrega funcion ManejaPerJuridica
' BUG-PD-327: DJUAREZ: 04/01/2018: Se modifican los estilos de la pagina u orden de los datos

Imports System.Text

Public Class clsPersonalidadJuridica

    '-------------------------- INICIO PDK_CAT_PER_JURIDICA-------------------------- 
#Region "Variables"
    Private intPDK_ID_PER_JURIDICA As Integer = 0
    Private strPDK_PER_NOMBRE As String = String.Empty
    Private intPDK_PER_ACTIVO As Integer = 0
    Private strPDK_PER_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
    Private strPDK_PER_STATUS As String = String.Empty

    Private intPDK_PER_DEFAULT As Integer = 0
    Private strErrorPerJuridica As String = ""

#End Region
#Region "Propiedades"
    Public Property PDK_ID_PER_JURIDICA() As Integer
        Get
            Return intPDK_ID_PER_JURIDICA
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_PER_JURIDICA = value
        End Set
    End Property
    Public Property PDK_PER_NOMBRE() As String
        Get
            Return strPDK_PER_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_PER_NOMBRE = value
        End Set
    End Property
    Public Property PDK_PER_ACTIVO() As Integer
        Get
            Return intPDK_PER_ACTIVO
        End Get
        Set(ByVal value As Integer)
            intPDK_PER_ACTIVO = value
        End Set
    End Property
    Public Property PDK_PER_MODIF() As String
        Get
            Return strPDK_PER_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_PER_MODIF = value
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

    Public Property PDK_PER_STATUS() As String
        Get
            Return strPDK_PER_STATUS
        End Get
        Set(ByVal value As String)
            strPDK_PER_STATUS = value
        End Set
    End Property
    Public Property PDK_PER_DEFAULT() As Integer
        Get
            Return intPDK_PER_DEFAULT
        End Get
        Set(value As Integer)
            intPDK_PER_DEFAULT = value
        End Set
    End Property
    Public ReadOnly Property ErrorPerJuridica As String
        Get
            Return strErrorPerJuridica
        End Get
    End Property

#End Region
#Region "Metodos"
    Public Sub New(ByVal intRegistro As Integer)
        Dim strSQL As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intRegistro = 0 Then Exit Sub
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_PER_JURIDICA,")
            strSQL.Append(" A.PDK_PER_NOMBRE,")
            strSQL.Append(" A.PDK_PER_ACTIVO,")
            strSQL.Append(" A.PDK_PER_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO, ")
            strSQL.Append(" B.PDK_PAR_SIS_PARAMETRO AS 'PDK_PAR_STATUS', ")
            strSQL.Append(" PDK_PER_DEFAULT ")
            strSQL.Append(" FROM PDK_CAT_PER_JURIDICA A")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA B ON B.PDK_ID_PARAMETROS_SISTEMA = A.PDK_PER_ACTIVO ")
            strSQL.Append(" WHERE A.PDK_ID_PER_JURIDICA = " & intRegistro)

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_PER_JURIDICA = .Item("PDK_ID_PER_JURIDICA")
                Me.strPDK_PER_NOMBRE = .Item("PDK_PER_NOMBRE")
                Me.intPDK_PER_ACTIVO = .Item("PDK_PER_ACTIVO")
                Me.strPDK_PER_MODIF = .Item("PDK_PER_MODIF")
                Me.strPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.strPDK_PER_STATUS = .Item("PDK_PAR_STATUS")
                Me.intPDK_PER_DEFAULT = .Item("PDK_PER_DEFAULT")
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
            strSQL.Append(" A.PDK_ID_PER_JURIDICA,")
            strSQL.Append(" A.PDK_PER_NOMBRE,")
            strSQL.Append(" A.PDK_PER_ACTIVO,")
            strSQL.Append(" A.PDK_PER_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO, ")
            strSQL.Append(" B.PDK_PAR_SIS_PARAMETRO 'PDK_PAR_STATUS', ")
            strSQL.Append(" A.PDK_PER_DEFAULT ")
            strSQL.Append(" FROM PDK_CAT_PER_JURIDICA A ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA B ON B.PDK_ID_PARAMETROS_SISTEMA = A.PDK_PER_ACTIVO ")
            strSQL.Append(" ORDER BY  A.PDK_ID_PER_JURIDICA ASC")

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_PER_JURIDICA")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_PER_JURIDICA = 0 Then
                Me.intPDK_ID_PER_JURIDICA = BD.ObtenConsecutivo("PDK_ID_PER_JURIDICA", "PDK_CAT_PER_JURIDICA", Nothing)
                strSql = "INSERT INTO PDK_CAT_PER_JURIDICA " & _
                            "(" & _
                            " PDK_ID_PER_JURIDICA,PDK_PER_NOMBRE,PDK_PER_ACTIVO,PDK_PER_MODIF,PDK_CLAVE_USUARIO)" & _
                            " VALUES ( " & intPDK_ID_PER_JURIDICA & ", '" & strPDK_PER_NOMBRE & "', " & intPDK_PER_ACTIVO & ", '" & strPDK_PER_MODIF & "','" & strPDK_CLAVE_USUARIO & "' " & _
                            ")"
            Else
                ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_CAT_PER_JURIDICA ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_CAT_PER_JURIDICA " & _
               "SET " & _
                " PDK_PER_NOMBRE = '" & strPDK_PER_NOMBRE & "'," & _
                " PDK_PER_ACTIVO = " & intPDK_PER_ACTIVO & ", " & _
                " PDK_PER_MODIF = '" & strPDK_PER_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "' " & _
             " WHERE PDK_ID_PER_JURIDICA=  " & intPDK_ID_PER_JURIDICA
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_PER_JURIDICA")
        End Try
    End Sub
    Public Function ManejaPerJuridica(ByVal intOper As Integer) As DataSet
        Dim MB As New ProdeskNet.BD.clsManejaBD
        Try
            strErrorPerJuridica = ""

            Select Case intOper
                Case 1 'CONSULTA
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    MB.AgregaParametro("@idProd", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PER_JURIDICA)
                Case 2 'INSERTA
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    MB.AgregaParametro("@idPerJ", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PER_JURIDICA)
                    MB.AgregaParametro("@nom", ProdeskNet.BD.TipoDato.Cadena, strPDK_PER_NOMBRE)
                    MB.AgregaParametro("@feMod", ProdeskNet.BD.TipoDato.Cadena, strPDK_PER_MODIF)
                    MB.AgregaParametro("@estat", ProdeskNet.BD.TipoDato.Entero, intPDK_PER_ACTIVO)
                    MB.AgregaParametro("@usu_reg", ProdeskNet.BD.TipoDato.Entero, strPDK_CLAVE_USUARIO)
                    MB.AgregaParametro("@default", ProdeskNet.BD.TipoDato.Entero, intPDK_PER_DEFAULT)
                Case 3 'EDITA
                    MB.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    MB.AgregaParametro("@idPerJ", ProdeskNet.BD.TipoDato.Entero, intPDK_ID_PER_JURIDICA)
                    MB.AgregaParametro("@nom", ProdeskNet.BD.TipoDato.Cadena, strPDK_PER_NOMBRE)
                    MB.AgregaParametro("@feMod", ProdeskNet.BD.TipoDato.Cadena, strPDK_PER_MODIF)
                    MB.AgregaParametro("@estat", ProdeskNet.BD.TipoDato.Entero, intPDK_PER_ACTIVO)
                    MB.AgregaParametro("@usu_reg", ProdeskNet.BD.TipoDato.Entero, strPDK_CLAVE_USUARIO)
                    MB.AgregaParametro("@default", ProdeskNet.BD.TipoDato.Entero, intPDK_PER_DEFAULT)
            End Select

            ManejaPerJuridica = MB.EjecutaStoredProcedure("maneja_PerJuridica")
	If intOper = 2 Then
            intPDK_ID_PER_JURIDICA = ManejaPerJuridica.Tables(0).Rows(0).Item("PDK_ID_PER_JURIDICA")
	End If
        Catch ex As Exception
            strErrorPerJuridica = ex.Message
        End Try

    End Function

#End Region
    '-------------------------- FIN PDK_CAT_PER_JURIDICA-------------------------- 
    
End Class
