Imports System.Text

Public Class clsBuroReporteEmpleo
'-------------------------- INICIO PDK_BURO_REPORTE_EMPLEO-------------------------- 
#Region "Variables"
    Private intPDK_ID_BURO_REPORTE_EMPLEO As Integer = 0
    Private strPDK_BUR_REP_EMP_PE_EMPRESA As String = String.Empty
    Private intPDK_ID_BURO As Integer = 0
    Private strPDK_BUR_REP_EMP_PE_DIRECCION As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_DIRECCION2 As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_COL_POB As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_DEL_MUN As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_CIUDAD As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_ESTADO As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_COD_POST As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_TELEFONO As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_EXT_TELEFONO As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_NUM_FAX As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_CARGO As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_FEC_CONTRATACION As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_MONEDA As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_SALARIO As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_BASE_SALARIAL As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_NUM_EMPLEADO As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_FEC_ULT_DIA_EMPLEO As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_FEC_REPORTE_EMPLEO As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_FEC_VERIFICACION As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_MOD_VERIFICACION As String = String.Empty
    Private strPDK_BUR_REP_EMP_PE_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
#End Region
#Region "Propiedades"
    Public Property PDK_ID_BURO_REPORTE_EMPLEO() As Integer
        Get
            Return intPDK_ID_BURO_REPORTE_EMPLEO
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO_REPORTE_EMPLEO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_EMPRESA() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_EMPRESA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_EMPRESA = value
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
    Public Property PDK_BUR_REP_EMP_PE_DIRECCION() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_DIRECCION
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_DIRECCION = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_DIRECCION2() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_DIRECCION2
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_DIRECCION2 = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_COL_POB() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_COL_POB
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_COL_POB = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_DEL_MUN() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_DEL_MUN
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_DEL_MUN = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_CIUDAD() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_CIUDAD
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_CIUDAD = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_ESTADO() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_ESTADO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_ESTADO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_COD_POST() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_COD_POST
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_COD_POST = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_TELEFONO() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_TELEFONO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_TELEFONO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_EXT_TELEFONO() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_EXT_TELEFONO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_EXT_TELEFONO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_NUM_FAX() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_NUM_FAX
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_NUM_FAX = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_CARGO() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_CARGO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_CARGO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_FEC_CONTRATACION() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_FEC_CONTRATACION
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_FEC_CONTRATACION = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_MONEDA() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_MONEDA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_MONEDA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_SALARIO() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_SALARIO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_SALARIO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_BASE_SALARIAL() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_BASE_SALARIAL
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_BASE_SALARIAL = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_NUM_EMPLEADO() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_NUM_EMPLEADO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_NUM_EMPLEADO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_FEC_ULT_DIA_EMPLEO() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_FEC_ULT_DIA_EMPLEO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_FEC_ULT_DIA_EMPLEO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_FEC_REPORTE_EMPLEO() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_FEC_REPORTE_EMPLEO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_FEC_REPORTE_EMPLEO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_FEC_VERIFICACION() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_FEC_VERIFICACION
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_FEC_VERIFICACION = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_MOD_VERIFICACION() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_MOD_VERIFICACION
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_MOD_VERIFICACION = value
        End Set
    End Property
    Public Property PDK_BUR_REP_EMP_PE_MODIF() As String
        Get
            Return strPDK_BUR_REP_EMP_PE_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_EMP_PE_MODIF = value
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
            strSQL.Append(" PDK_ID_BURO_REPORTE_EMPLEO,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_EMPRESA,")
            strSQL.Append(" PDK_ID_BURO,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_DIRECCION,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_DIRECCION2,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_COL_POB,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_DEL_MUN,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_CIUDAD,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_ESTADO,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_COD_POST,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_TELEFONO,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_EXT_TELEFONO,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_NUM_FAX,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_CARGO,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_FEC_CONTRATACION,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_MONEDA,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_SALARIO,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_BASE_SALARIAL,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_NUM_EMPLEADO,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_FEC_ULT_DIA_EMPLEO,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_FEC_REPORTE_EMPLEO,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_FEC_VERIFICACION,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_MOD_VERIFICACION,")
            strSQL.Append(" PDK_BUR_REP_EMP_PE_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_EMPLEO")
            strSQL.Append(" WHERE PDK_ID_BURO_REPORTE_EMPLEO = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_BURO_REPORTE_EMPLEO = .Item("PDK_ID_BURO_REPORTE_EMPLEO")
                Me.strPDK_BUR_REP_EMP_PE_EMPRESA = .Item("PDK_BUR_REP_EMP_PE_EMPRESA")
                Me.intPDK_ID_BURO = .Item("PDK_ID_BURO")
                Me.strPDK_BUR_REP_EMP_PE_DIRECCION = .Item("PDK_BUR_REP_EMP_PE_DIRECCION")
                Me.strPDK_BUR_REP_EMP_PE_DIRECCION2 = .Item("PDK_BUR_REP_EMP_PE_DIRECCION2")
                Me.strPDK_BUR_REP_EMP_PE_COL_POB = .Item("PDK_BUR_REP_EMP_PE_COL_POB")
                Me.strPDK_BUR_REP_EMP_PE_DEL_MUN = .Item("PDK_BUR_REP_EMP_PE_DEL_MUN")
                Me.strPDK_BUR_REP_EMP_PE_CIUDAD = .Item("PDK_BUR_REP_EMP_PE_CIUDAD")
                Me.strPDK_BUR_REP_EMP_PE_ESTADO = .Item("PDK_BUR_REP_EMP_PE_ESTADO")
                Me.strPDK_BUR_REP_EMP_PE_COD_POST = .Item("PDK_BUR_REP_EMP_PE_COD_POST")
                Me.strPDK_BUR_REP_EMP_PE_TELEFONO = .Item("PDK_BUR_REP_EMP_PE_TELEFONO")
                Me.strPDK_BUR_REP_EMP_PE_EXT_TELEFONO = .Item("PDK_BUR_REP_EMP_PE_EXT_TELEFONO")
                Me.strPDK_BUR_REP_EMP_PE_NUM_FAX = .Item("PDK_BUR_REP_EMP_PE_NUM_FAX")
                Me.strPDK_BUR_REP_EMP_PE_CARGO = .Item("PDK_BUR_REP_EMP_PE_CARGO")
                Me.strPDK_BUR_REP_EMP_PE_FEC_CONTRATACION = .Item("PDK_BUR_REP_EMP_PE_FEC_CONTRATACION")
                Me.strPDK_BUR_REP_EMP_PE_MONEDA = .Item("PDK_BUR_REP_EMP_PE_MONEDA")
                Me.strPDK_BUR_REP_EMP_PE_SALARIO = .Item("PDK_BUR_REP_EMP_PE_SALARIO")
                Me.strPDK_BUR_REP_EMP_PE_BASE_SALARIAL = .Item("PDK_BUR_REP_EMP_PE_BASE_SALARIAL")
                Me.strPDK_BUR_REP_EMP_PE_NUM_EMPLEADO = .Item("PDK_BUR_REP_EMP_PE_NUM_EMPLEADO")
                Me.strPDK_BUR_REP_EMP_PE_FEC_ULT_DIA_EMPLEO = .Item("PDK_BUR_REP_EMP_PE_FEC_ULT_DIA_EMPLEO")
                Me.strPDK_BUR_REP_EMP_PE_FEC_REPORTE_EMPLEO = .Item("PDK_BUR_REP_EMP_PE_FEC_REPORTE_EMPLEO")
                Me.strPDK_BUR_REP_EMP_PE_FEC_VERIFICACION = .Item("PDK_BUR_REP_EMP_PE_FEC_VERIFICACION")
                Me.strPDK_BUR_REP_EMP_PE_MOD_VERIFICACION = .Item("PDK_BUR_REP_EMP_PE_MOD_VERIFICACION")
                Me.strPDK_BUR_REP_EMP_PE_MODIF = .Item("PDK_BUR_REP_EMP_PE_MODIF")
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
            strSQL.Append(" A.PDK_ID_BURO_REPORTE_EMPLEO,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_EMPRESA,")
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_DIRECCION,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_DIRECCION2,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_COL_POB,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_DEL_MUN,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_CIUDAD,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_ESTADO,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_COD_POST,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_TELEFONO,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_EXT_TELEFONO,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_NUM_FAX,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_CARGO,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_FEC_CONTRATACION,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_MONEDA,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_SALARIO,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_BASE_SALARIAL,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_NUM_EMPLEADO,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_FEC_ULT_DIA_EMPLEO,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_FEC_REPORTE_EMPLEO,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_FEC_VERIFICACION,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_MOD_VERIFICACION,")
            strSQL.Append(" A.PDK_BUR_REP_EMP_PE_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_EMPLEO A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_BURO_REPORTE_EMPLEO")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_BURO_REPORTE_EMPLEO = 0 Then
                Me.intPDK_ID_BURO_REPORTE_EMPLEO = BD.ObtenConsecutivo("", "PDK_BURO_REPORTE_EMPLEO", Nothing)
                strSql = "INSERT INTO PDK_BURO_REPORTE_EMPLEO " & _
                                "(" & _
"PDK_ID_BURO_REPORTE_EMPLEO,PDK_BUR_REP_EMP_PE_EMPRESA,PDK_ID_BURO,PDK_BUR_REP_EMP_PE_DIRECCION,PDK_BUR_REP_EMP_PE_DIRECCION2,PDK_BUR_REP_EMP_PE_COL_POB,PDK_BUR_REP_EMP_PE_DEL_MUN,PDK_BUR_REP_EMP_PE_CIUDAD,PDK_BUR_REP_EMP_PE_ESTADO,PDK_BUR_REP_EMP_PE_COD_POST,PDK_BUR_REP_EMP_PE_TELEFONO,PDK_BUR_REP_EMP_PE_EXT_TELEFONO,PDK_BUR_REP_EMP_PE_NUM_FAX,PDK_BUR_REP_EMP_PE_CARGO,PDK_BUR_REP_EMP_PE_FEC_CONTRATACION,PDK_BUR_REP_EMP_PE_MONEDA,PDK_BUR_REP_EMP_PE_SALARIO,PDK_BUR_REP_EMP_PE_BASE_SALARIAL,PDK_BUR_REP_EMP_PE_NUM_EMPLEADO,PDK_BUR_REP_EMP_PE_FEC_ULT_DIA_EMPLEO,PDK_BUR_REP_EMP_PE_FEC_REPORTE_EMPLEO,PDK_BUR_REP_EMP_PE_FEC_VERIFICACION,PDK_BUR_REP_EMP_PE_MOD_VERIFICACION,PDK_BUR_REP_EMP_PE_MODIF,PDK_CLAVE_USUARIO,)" & _
" VALUES ( " & intPDK_ID_BURO_REPORTE_EMPLEO & ", '" & strPDK_BUR_REP_EMP_PE_EMPRESA & "', " & intPDK_ID_BURO & ", '" & strPDK_BUR_REP_EMP_PE_DIRECCION & "','" & strPDK_BUR_REP_EMP_PE_DIRECCION2 & "','" & strPDK_BUR_REP_EMP_PE_COL_POB & "','" & strPDK_BUR_REP_EMP_PE_DEL_MUN & "','" & strPDK_BUR_REP_EMP_PE_CIUDAD & "','" & strPDK_BUR_REP_EMP_PE_ESTADO & "','" & strPDK_BUR_REP_EMP_PE_COD_POST & "','" & strPDK_BUR_REP_EMP_PE_TELEFONO & "','" & strPDK_BUR_REP_EMP_PE_EXT_TELEFONO & "','" & strPDK_BUR_REP_EMP_PE_NUM_FAX & "','" & strPDK_BUR_REP_EMP_PE_CARGO & "','" & strPDK_BUR_REP_EMP_PE_FEC_CONTRATACION & "','" & strPDK_BUR_REP_EMP_PE_MONEDA & "','" & strPDK_BUR_REP_EMP_PE_SALARIO & "','" & strPDK_BUR_REP_EMP_PE_BASE_SALARIAL & "','" & strPDK_BUR_REP_EMP_PE_NUM_EMPLEADO & "','" & strPDK_BUR_REP_EMP_PE_FEC_ULT_DIA_EMPLEO & "','" & strPDK_BUR_REP_EMP_PE_FEC_REPORTE_EMPLEO & "','" & strPDK_BUR_REP_EMP_PE_FEC_VERIFICACION & "','" & strPDK_BUR_REP_EMP_PE_MOD_VERIFICACION & "','" & strPDK_BUR_REP_EMP_PE_MODIF & "','" & strPDK_CLAVE_USUARIO & "', " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_BURO_REPORTE_EMPLEO ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_BURO_REPORTE_EMPLEO " & _
               "SET " & _
             " PDK_ID_BURO_REPORTE_EMPLEO = " & intPDK_ID_BURO_REPORTE_EMPLEO & ", " & _
                " PDK_BUR_REP_EMP_PE_EMPRESA = '" & strPDK_BUR_REP_EMP_PE_EMPRESA & "'," & _
             " PDK_ID_BURO = " & intPDK_ID_BURO & ", " & _
                " PDK_BUR_REP_EMP_PE_DIRECCION = '" & strPDK_BUR_REP_EMP_PE_DIRECCION & "'," & _
                " PDK_BUR_REP_EMP_PE_DIRECCION2 = '" & strPDK_BUR_REP_EMP_PE_DIRECCION2 & "'," & _
                " PDK_BUR_REP_EMP_PE_COL_POB = '" & strPDK_BUR_REP_EMP_PE_COL_POB & "'," & _
                " PDK_BUR_REP_EMP_PE_DEL_MUN = '" & strPDK_BUR_REP_EMP_PE_DEL_MUN & "'," & _
                " PDK_BUR_REP_EMP_PE_CIUDAD = '" & strPDK_BUR_REP_EMP_PE_CIUDAD & "'," & _
                " PDK_BUR_REP_EMP_PE_ESTADO = '" & strPDK_BUR_REP_EMP_PE_ESTADO & "'," & _
                " PDK_BUR_REP_EMP_PE_COD_POST = '" & strPDK_BUR_REP_EMP_PE_COD_POST & "'," & _
                " PDK_BUR_REP_EMP_PE_TELEFONO = '" & strPDK_BUR_REP_EMP_PE_TELEFONO & "'," & _
                " PDK_BUR_REP_EMP_PE_EXT_TELEFONO = '" & strPDK_BUR_REP_EMP_PE_EXT_TELEFONO & "'," & _
                " PDK_BUR_REP_EMP_PE_NUM_FAX = '" & strPDK_BUR_REP_EMP_PE_NUM_FAX & "'," & _
                " PDK_BUR_REP_EMP_PE_CARGO = '" & strPDK_BUR_REP_EMP_PE_CARGO & "'," & _
                " PDK_BUR_REP_EMP_PE_FEC_CONTRATACION = '" & strPDK_BUR_REP_EMP_PE_FEC_CONTRATACION & "'," & _
                " PDK_BUR_REP_EMP_PE_MONEDA = '" & strPDK_BUR_REP_EMP_PE_MONEDA & "'," & _
                " PDK_BUR_REP_EMP_PE_SALARIO = '" & strPDK_BUR_REP_EMP_PE_SALARIO & "'," & _
                " PDK_BUR_REP_EMP_PE_BASE_SALARIAL = '" & strPDK_BUR_REP_EMP_PE_BASE_SALARIAL & "'," & _
                " PDK_BUR_REP_EMP_PE_NUM_EMPLEADO = '" & strPDK_BUR_REP_EMP_PE_NUM_EMPLEADO & "'," & _
                " PDK_BUR_REP_EMP_PE_FEC_ULT_DIA_EMPLEO = '" & strPDK_BUR_REP_EMP_PE_FEC_ULT_DIA_EMPLEO & "'," & _
                " PDK_BUR_REP_EMP_PE_FEC_REPORTE_EMPLEO = '" & strPDK_BUR_REP_EMP_PE_FEC_REPORTE_EMPLEO & "'," & _
                " PDK_BUR_REP_EMP_PE_FEC_VERIFICACION = '" & strPDK_BUR_REP_EMP_PE_FEC_VERIFICACION & "'," & _
                " PDK_BUR_REP_EMP_PE_MOD_VERIFICACION = '" & strPDK_BUR_REP_EMP_PE_MOD_VERIFICACION & "'," & _
                " PDK_BUR_REP_EMP_PE_MODIF = '" & strPDK_BUR_REP_EMP_PE_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "'," & _
             " WHERE PDK_ID_BURO_REPORTE_EMPLEO=  " & intPDK_ID_BURO_REPORTE_EMPLEO
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_BURO_REPORTE_EMPLEO")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_BURO_REPORTE_EMPLEO-------------------------- 

End Class
