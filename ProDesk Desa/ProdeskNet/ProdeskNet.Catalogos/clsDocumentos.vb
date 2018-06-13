Imports System.Text
Imports System.Data

Public Class clsDocumentos
    '-------------------------- INICIO PDK_CAT_DOCUMENTOS-------------------------- 
#Region "Trakers"
    'BUG-PD-423: CGARCIA: 23/04/2018: SE AGREGA METODO PARA ACTUALIZAR INFO DE DOCUMENTOS 
#End Region
#Region "Variables"
    Private intPDK_ID_DOCUMENTOS As Integer = 0
    Private strPDK_DOC_NOMBRE As String = String.Empty
    Private strPDK_DOC_MODIF As String = String.Empty
    Private intPDK_CLAVE_USUARIO As Integer = 0
    Private intPDK_DOC_ACTIVO As Integer = 0
    Private intPDK_ID_PER_JURIDICA As Integer = 0
    Private intPDK_ID_REL_DOC_PER_JUR As Integer = 0
    Private intPDK_CAT_DOCPRODOC As Integer = 0

    Private _id As Integer = 0
    Private _doctType As String = String.Empty
    Private _folio As Integer = 0
    Private _Check_VAL As Integer = 0
    Private _Check_REC As Integer = 0
    Private _Checked As Boolean = False

    Private strErrCatalogos As String
             

#End Region
#Region "Propiedades"

    Sub New()
        ' TODO: Complete member initialization 
    End Sub

    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    Public Property docType() As String
        Get
            Return _doctType
        End Get
        Set(value As String)
            _doctType = value
        End Set
    End Property

    Public Property folio() As Integer
        Get
            Return _folio
        End Get
        Set(value As Integer)
            _folio = value
        End Set
    End Property

    Public Property Check_VAL() As Integer
        Get
            Return _Check_VAL
        End Get
        Set(value As Integer)
            _Check_VAL = value
        End Set
    End Property

    Public Property Check_REC() As Integer
        Get
            Return _Check_REC
        End Get
        Set(value As Integer)
            _Check_REC = value
        End Set
    End Property

    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(value As Boolean)
            _Checked = value
        End Set
    End Property


    Public Property PDK_CAT_DOCPRODOC() As Integer
        Get
            Return intPDK_CAT_DOCPRODOC
        End Get
        Set(ByVal value As Integer)
            intPDK_CAT_DOCPRODOC = value
        End Set
    End Property

    Public Property PDK_ID_DOCUMENTOS() As Integer
        Get
            Return intPDK_ID_DOCUMENTOS
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_DOCUMENTOS = value
        End Set
    End Property
    Public Property PDK_DOC_NOMBRE() As String
        Get
            Return strPDK_DOC_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_DOC_NOMBRE = value
        End Set
    End Property
    Public Property PDK_DOC_MODIF() As String
        Get
            Return strPDK_DOC_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_DOC_MODIF = value
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
    Public Property PDK_DOC_ACTIVO() As Integer
        Get
            Return intPDK_DOC_ACTIVO
        End Get
        Set(ByVal value As Integer)
            intPDK_DOC_ACTIVO = value
        End Set
    End Property

    Public Property PDK_ID_PER_JURIDICA() As Integer
        Get
            Return intPDK_ID_PER_JURIDICA
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_PER_JURIDICA = value
        End Set
    End Property

    'intPDK_ID_REL_DOC_PER_JUR
    Public Property PDK_ID_REL_DOC_PER_JUR() As Integer
        Get
            Return intPDK_ID_REL_DOC_PER_JUR
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_REL_DOC_PER_JUR = value
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
            strSQL.Append(" PDK_ID_DOCUMENTOS,")
            strSQL.Append(" PDK_DOC_NOMBRE,")
            strSQL.Append(" PDK_DOC_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" PDK_DOC_ACTIVO,")
            strSQL.Append(" FROM PDK_CAT_DOCUMENTOS")
            strSQL.Append(" WHERE PDK_ID_DOCUMENTOS = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_DOCUMENTOS = .Item("PDK_ID_DOCUMENTOS")
                Me.strPDK_DOC_NOMBRE = .Item("PDK_DOC_NOMBRE")
                Me.strPDK_DOC_MODIF = .Item("PDK_DOC_MODIF")
                Me.intPDK_CLAVE_USUARIO = .Item("PDK_CLAVE_USUARIO")
                Me.intPDK_DOC_ACTIVO = .Item("PDK_DOC_ACTIVO")
            End With
        Catch ex As Exception
            Throw New Exception("Error al buscar los registros")
        End Try
    End Sub

    Public Shared Function ObtenTodos( _
            Optional ByVal intDocumento As Integer = 0, _
            Optional ByVal intPerJuridica As Integer = 0, _
            Optional ByVal intRelDocPer As Integer = 0) As DataSet
        Dim objException As Exception = Nothing
        Dim strSQL As New StringBuilder
        Dim strSQLWhere As New StringBuilder
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSQL.Append(" SELECT ")
            strSQL.Append(" A.PDK_ID_DOCUMENTOS,")
            strSQL.Append(" A.PDK_DOC_NOMBRE,")
            strSQL.Append(" A.PDK_DOC_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" A.PDK_DOC_ACTIVO,")
            strSQL.Append(" B.PDK_ID_PER_JURIDICA,")
            strSQL.Append(" B.PDK_ID_REL_DOC_PER_JUR,")
            strSQL.Append(" C.PDK_PER_NOMBRE,")
            strSQL.Append(" D.PDK_PAR_SIS_PARAMETRO")

            strSQL.Append(" FROM PDK_CAT_DOCUMENTOS A ")
            strSQL.Append(" INNER JOIN PDK_REL_DOC_PER_JUR B ON B.PDK_ID_DOCUMENTOS = A.PDK_ID_DOCUMENTOS ")
            strSQL.Append(" INNER JOIN PDK_CAT_PER_JURIDICA C ON C.PDK_ID_PER_JURIDICA = B.PDK_ID_PER_JURIDICA ")
            strSQL.Append(" INNER JOIN PDK_PARAMETROS_SISTEMA D ON D.PDK_ID_PARAMETROS_SISTEMA = A.PDK_DOC_ACTIVO")


            If intDocumento > 0 Then
                strSQLWhere.Append(" AND A.PDK_ID_DOCUMENTOS=" & intDocumento)
            End If

            If intPerJuridica > 0 Then
                strSQLWhere.Append(" AND B.PDK_ID_PER_JURIDICA=" & intPerJuridica)
            End If

            If intRelDocPer > 0 Then
                strSQLWhere.Append(" AND B.PDK_ID_REL_DOC_PER_JUR =" & intRelDocPer)
            End If

            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString & " WHERE 1=1 " & strSQLWhere.ToString.Trim)
            Return ds

        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_CAT_DOCUMENTOS")
            Throw objException
        End Try

    End Function

    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_DOCUMENTOS = 0 Then
                Me.intPDK_ID_DOCUMENTOS = BD.ObtenConsecutivo("PDK_ID_DOCUMENTOS", "PDK_CAT_DOCUMENTOS", Nothing)
                strSql = "INSERT INTO PDK_CAT_DOCUMENTOS " & _
                            "(" & _
                        "PDK_ID_DOCUMENTOS,PDK_DOC_NOMBRE,PDK_DOC_MODIF,PDK_CLAVE_USUARIO,PDK_DOC_ACTIVO, PDK_ID_PRODDOC)" & _
                        " VALUES ( " & intPDK_ID_DOCUMENTOS & ", '" & strPDK_DOC_NOMBRE & "','" & strPDK_DOC_MODIF & "', " & intPDK_CLAVE_USUARIO & ",  " & intPDK_DOC_ACTIVO & ",  " & intPDK_CAT_DOCPRODOC & _
                        ")"

                BD.EjecutarQuery(strSql)
            Else
                ActualizaRegistro()
            End If

            'Guardamos la relacion de documentos con personalidad jurídica
            If Me.intPDK_ID_DOCUMENTOS > 0 Then
                If Me.intPDK_ID_REL_DOC_PER_JUR = 0 Then
                    Me.intPDK_ID_REL_DOC_PER_JUR = BD.ObtenConsecutivo("PDK_ID_REL_DOC_PER_JUR", "PDK_REL_DOC_PER_JUR", Nothing)
                    strSql = "INSERT INTO PDK_REL_DOC_PER_JUR " & _
                                "(" & _
                            "PDK_ID_REL_DOC_PER_JUR,PDK_ID_PER_JURIDICA,PDK_ID_DOCUMENTOS)" & _
                            " VALUES ( " & intPDK_ID_REL_DOC_PER_JUR & ", " & intPDK_ID_PER_JURIDICA & ",  " & intPDK_ID_DOCUMENTOS & "  " & _
                            ")"
                    BD.EjecutarQuery(strSql)
                Else
                    ActualizaRelacion()
                End If
            End If
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_CAT_DOCUMENTOS ")
        End Try
    End Sub

    Private Sub ActualizaRelacion()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_REL_DOC_PER_JUR " & _
                "SET " & _
                " PDK_ID_PER_JURIDICA= " & PDK_ID_PER_JURIDICA & ", " & _
                " PDK_ID_DOCUMENTOS = " & intPDK_ID_DOCUMENTOS & " " & _
                " WHERE PDK_ID_REL_DOC_PER_JUR=  " & intPDK_ID_REL_DOC_PER_JUR
            BD.EjecutarQuery(strSql)

        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_DOCUMENTOS")
        End Try
    End Sub

    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_CAT_DOCUMENTOS " & _
               "SET " & _
                " PDK_DOC_NOMBRE = '" & strPDK_DOC_NOMBRE & "'," & _
                " PDK_DOC_MODIF = '" & strPDK_DOC_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = " & intPDK_CLAVE_USUARIO & ", " & _
                " PDK_DOC_ACTIVO = " & intPDK_DOC_ACTIVO & ", " & _
                " PDK_ID_PRODDOC = " & intPDK_CAT_DOCPRODOC & " " & _
                " WHERE PDK_ID_DOCUMENTOS=  " & intPDK_ID_DOCUMENTOS
            BD.EjecutarQuery(strSql)

        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_CAT_DOCUMENTOS")
        End Try
    End Sub

    'BUG-PD-423: CGARCIA: 23/04/2018: SE AGREGA METODO PARA ACTUALIZAR INFO DE DOCUMENTOS 
    Public Function getActualizaDatosDocumentos(ByVal opcion As Integer) As DataSet
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim ManejaDatosDoc As New DataSet
        Try
            BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, opcion)
            Select Case opcion
                Case 1
                    BD.AgregaParametro("@id", ProdeskNet.BD.TipoDato.Entero, id)
                    BD.AgregaParametro("@docType", ProdeskNet.BD.TipoDato.Cadena, docType)
                    BD.AgregaParametro("@folio", ProdeskNet.BD.TipoDato.Entero, folio)
                    BD.AgregaParametro("@Check_VAL", ProdeskNet.BD.TipoDato.Entero, Check_VAL)
                    BD.AgregaParametro("@Check_REC", ProdeskNet.BD.TipoDato.Entero, Check_REC)
                    BD.AgregaParametro("@Checked", ProdeskNet.BD.TipoDato.Booleano, Checked)

            End Select
            ManejaDatosDoc = BD.EjecutaStoredProcedure("SPD_PROCESO_DOCUMENTOS")

            If (BD.ErrorBD) <> "" Then
                strErrCatalogos = BD.ErrorBD            
            End If
            Return ManejaDatosDoc
        Catch ex As Exception
            strErrCatalogos = ex.Message
            Throw New Exception(strErrCatalogos)
        End Try
    End Function

#End Region
    '-------------------------- FIN PDK_CAT_DOCUMENTOS-------------------------- 

End Class
