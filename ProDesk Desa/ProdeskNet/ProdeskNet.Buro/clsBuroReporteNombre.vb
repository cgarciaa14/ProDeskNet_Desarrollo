Imports System.Text

Public Class clsBuroReporteNombre
'-------------------------- INICIO PDK_BURO_REPORTE_NOMBRE-------------------------- 
#Region "Variables"
    Private intPDK_ID_BURO_REPORTE_NOMBRE As Integer = 0
    Private intPDK_ID_BURO As Integer = 0
    Private strPDK_BUR_REP_NOM_PN_APE_PAT As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_APE_MAT As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_APE_ADIC As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_PRIMER_NOMBRE As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_SEGUNDO_NOMBRE As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_FEC_NAC As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_RFC As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_PRE_PERSONAL As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_SUFIJO As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_NACIONALIDAD As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_RESIDENCIA As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_LIC_CONDUCIR As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_EDO_CIVIL As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_SEXO As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_NUM_CED_PROF As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_REG_ELECTORAL As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_CVE_TAX_PAIS As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_CVE_OTRO_PAIS As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_DEPENDIENTES As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_EDAD_DEPENDIENTES As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_FEC_REC_INFO As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_FEC_DEFUNCION As String = String.Empty
    Private strPDK_BUR_REP_NOM_PN_MODIF As String = String.Empty
    Private strPDK_CLAVE_USUARIO As String = String.Empty
#End Region
#Region "Propiedades"
    Public Property PDK_ID_BURO_REPORTE_NOMBRE() As Integer
        Get
            Return intPDK_ID_BURO_REPORTE_NOMBRE
        End Get
        Set(ByVal value As Integer)
            intPDK_ID_BURO_REPORTE_NOMBRE = value
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
    Public Property PDK_BUR_REP_NOM_PN_APE_PAT() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_APE_PAT
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_APE_PAT = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_APE_MAT() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_APE_MAT
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_APE_MAT = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_APE_ADIC() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_APE_ADIC
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_APE_ADIC = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_PRIMER_NOMBRE() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_PRIMER_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_PRIMER_NOMBRE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_SEGUNDO_NOMBRE() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_SEGUNDO_NOMBRE
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_SEGUNDO_NOMBRE = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_FEC_NAC() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_FEC_NAC
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_FEC_NAC = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_RFC() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_RFC
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_RFC = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_PRE_PERSONAL() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_PRE_PERSONAL
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_PRE_PERSONAL = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_SUFIJO() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_SUFIJO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_SUFIJO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_NACIONALIDAD() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_NACIONALIDAD
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_NACIONALIDAD = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_RESIDENCIA() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_RESIDENCIA
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_RESIDENCIA = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_LIC_CONDUCIR() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_LIC_CONDUCIR
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_LIC_CONDUCIR = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_EDO_CIVIL() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_EDO_CIVIL
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_EDO_CIVIL = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_SEXO() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_SEXO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_SEXO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_NUM_CED_PROF() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_NUM_CED_PROF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_NUM_CED_PROF = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_REG_ELECTORAL() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_REG_ELECTORAL
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_REG_ELECTORAL = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_CVE_TAX_PAIS() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_CVE_TAX_PAIS
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_CVE_TAX_PAIS = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_CVE_OTRO_PAIS() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_CVE_OTRO_PAIS
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_CVE_OTRO_PAIS = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_DEPENDIENTES() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_DEPENDIENTES
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_DEPENDIENTES = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_EDAD_DEPENDIENTES() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_EDAD_DEPENDIENTES
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_EDAD_DEPENDIENTES = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_FEC_REC_INFO() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_FEC_REC_INFO
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_FEC_REC_INFO = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_FEC_DEFUNCION() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_FEC_DEFUNCION
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_FEC_DEFUNCION = value
        End Set
    End Property
    Public Property PDK_BUR_REP_NOM_PN_MODIF() As String
        Get
            Return strPDK_BUR_REP_NOM_PN_MODIF
        End Get
        Set(ByVal value As String)
            strPDK_BUR_REP_NOM_PN_MODIF = value
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
            strSQL.Append(" PDK_ID_BURO_REPORTE_NOMBRE,")
            strSQL.Append(" PDK_ID_BURO,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_APE_PAT,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_APE_MAT,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_APE_ADIC,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_PRIMER_NOMBRE,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_SEGUNDO_NOMBRE,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_FEC_NAC,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_RFC,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_PRE_PERSONAL,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_SUFIJO,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_NACIONALIDAD,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_RESIDENCIA,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_LIC_CONDUCIR,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_EDO_CIVIL,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_SEXO,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_NUM_CED_PROF,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_REG_ELECTORAL,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_CVE_TAX_PAIS,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_CVE_OTRO_PAIS,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_DEPENDIENTES,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_EDAD_DEPENDIENTES,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_FEC_REC_INFO,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_FEC_DEFUNCION,")
            strSQL.Append(" PDK_BUR_REP_NOM_PN_MODIF,")
            strSQL.Append(" PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_NOMBRE")
            strSQL.Append(" WHERE PDK_ID_BURO_REPORTE_NOMBRE = " & intRegistro)
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            If ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No se encontro informacion con esa clave")
            End If
            With ds.Tables(0).Rows(0)
                Me.intPDK_ID_BURO_REPORTE_NOMBRE = .Item("PDK_ID_BURO_REPORTE_NOMBRE")
                Me.intPDK_ID_BURO = .Item("PDK_ID_BURO")
                Me.strPDK_BUR_REP_NOM_PN_APE_PAT = .Item("PDK_BUR_REP_NOM_PN_APE_PAT")
                Me.strPDK_BUR_REP_NOM_PN_APE_MAT = .Item("PDK_BUR_REP_NOM_PN_APE_MAT")
                Me.strPDK_BUR_REP_NOM_PN_APE_ADIC = .Item("PDK_BUR_REP_NOM_PN_APE_ADIC")
                Me.strPDK_BUR_REP_NOM_PN_PRIMER_NOMBRE = .Item("PDK_BUR_REP_NOM_PN_PRIMER_NOMBRE")
                Me.strPDK_BUR_REP_NOM_PN_SEGUNDO_NOMBRE = .Item("PDK_BUR_REP_NOM_PN_SEGUNDO_NOMBRE")
                Me.strPDK_BUR_REP_NOM_PN_FEC_NAC = .Item("PDK_BUR_REP_NOM_PN_FEC_NAC")
                Me.strPDK_BUR_REP_NOM_PN_RFC = .Item("PDK_BUR_REP_NOM_PN_RFC")
                Me.strPDK_BUR_REP_NOM_PN_PRE_PERSONAL = .Item("PDK_BUR_REP_NOM_PN_PRE_PERSONAL")
                Me.strPDK_BUR_REP_NOM_PN_SUFIJO = .Item("PDK_BUR_REP_NOM_PN_SUFIJO")
                Me.strPDK_BUR_REP_NOM_PN_NACIONALIDAD = .Item("PDK_BUR_REP_NOM_PN_NACIONALIDAD")
                Me.strPDK_BUR_REP_NOM_PN_RESIDENCIA = .Item("PDK_BUR_REP_NOM_PN_RESIDENCIA")
                Me.strPDK_BUR_REP_NOM_PN_LIC_CONDUCIR = .Item("PDK_BUR_REP_NOM_PN_LIC_CONDUCIR")
                Me.strPDK_BUR_REP_NOM_PN_EDO_CIVIL = .Item("PDK_BUR_REP_NOM_PN_EDO_CIVIL")
                Me.strPDK_BUR_REP_NOM_PN_SEXO = .Item("PDK_BUR_REP_NOM_PN_SEXO")
                Me.strPDK_BUR_REP_NOM_PN_NUM_CED_PROF = .Item("PDK_BUR_REP_NOM_PN_NUM_CED_PROF")
                Me.strPDK_BUR_REP_NOM_PN_REG_ELECTORAL = .Item("PDK_BUR_REP_NOM_PN_REG_ELECTORAL")
                Me.strPDK_BUR_REP_NOM_PN_CVE_TAX_PAIS = .Item("PDK_BUR_REP_NOM_PN_CVE_TAX_PAIS")
                Me.strPDK_BUR_REP_NOM_PN_CVE_OTRO_PAIS = .Item("PDK_BUR_REP_NOM_PN_CVE_OTRO_PAIS")
                Me.strPDK_BUR_REP_NOM_PN_DEPENDIENTES = .Item("PDK_BUR_REP_NOM_PN_DEPENDIENTES")
                Me.strPDK_BUR_REP_NOM_PN_EDAD_DEPENDIENTES = .Item("PDK_BUR_REP_NOM_PN_EDAD_DEPENDIENTES")
                Me.strPDK_BUR_REP_NOM_PN_FEC_REC_INFO = .Item("PDK_BUR_REP_NOM_PN_FEC_REC_INFO")
                Me.strPDK_BUR_REP_NOM_PN_FEC_DEFUNCION = .Item("PDK_BUR_REP_NOM_PN_FEC_DEFUNCION")
                Me.strPDK_BUR_REP_NOM_PN_MODIF = .Item("PDK_BUR_REP_NOM_PN_MODIF")
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
            strSQL.Append(" A.PDK_ID_BURO_REPORTE_NOMBRE,")
            strSQL.Append(" A.PDK_ID_BURO,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_APE_PAT,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_APE_MAT,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_APE_ADIC,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_PRIMER_NOMBRE,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_SEGUNDO_NOMBRE,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_FEC_NAC,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_RFC,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_PRE_PERSONAL,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_SUFIJO,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_NACIONALIDAD,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_RESIDENCIA,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_LIC_CONDUCIR,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_EDO_CIVIL,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_SEXO,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_NUM_CED_PROF,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_REG_ELECTORAL,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_CVE_TAX_PAIS,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_CVE_OTRO_PAIS,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_DEPENDIENTES,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_EDAD_DEPENDIENTES,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_FEC_REC_INFO,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_FEC_DEFUNCION,")
            strSQL.Append(" A.PDK_BUR_REP_NOM_PN_MODIF,")
            strSQL.Append(" A.PDK_CLAVE_USUARIO,")
            strSQL.Append(" FROM PDK_BURO_REPORTE_NOMBRE A ")
            Dim ds As DataSet = BD.EjecutarQuery(strSQL.ToString)
            Return ds
        Catch ex As Exception
            objException = New Exception("Error al buscar los registros de PDK_BURO_REPORTE_NOMBRE")
            Throw objException
        End Try
    End Function
    Public Sub Guarda()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim objException As Exception = Nothing
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            If intPDK_ID_BURO_REPORTE_NOMBRE = 0 Then
                Me.intPDK_ID_BURO_REPORTE_NOMBRE = BD.ObtenConsecutivo("", "PDK_BURO_REPORTE_NOMBRE", Nothing)
                strSql = "INSERT INTO PDK_BURO_REPORTE_NOMBRE " & _
                                "(" & _
"PDK_ID_BURO_REPORTE_NOMBRE,PDK_ID_BURO,PDK_BUR_REP_NOM_PN_APE_PAT,PDK_BUR_REP_NOM_PN_APE_MAT,PDK_BUR_REP_NOM_PN_APE_ADIC,PDK_BUR_REP_NOM_PN_PRIMER_NOMBRE,PDK_BUR_REP_NOM_PN_SEGUNDO_NOMBRE,PDK_BUR_REP_NOM_PN_FEC_NAC,PDK_BUR_REP_NOM_PN_RFC,PDK_BUR_REP_NOM_PN_PRE_PERSONAL,PDK_BUR_REP_NOM_PN_SUFIJO,PDK_BUR_REP_NOM_PN_NACIONALIDAD,PDK_BUR_REP_NOM_PN_RESIDENCIA,PDK_BUR_REP_NOM_PN_LIC_CONDUCIR,PDK_BUR_REP_NOM_PN_EDO_CIVIL,PDK_BUR_REP_NOM_PN_SEXO,PDK_BUR_REP_NOM_PN_NUM_CED_PROF,PDK_BUR_REP_NOM_PN_REG_ELECTORAL,PDK_BUR_REP_NOM_PN_CVE_TAX_PAIS,PDK_BUR_REP_NOM_PN_CVE_OTRO_PAIS,PDK_BUR_REP_NOM_PN_DEPENDIENTES,PDK_BUR_REP_NOM_PN_EDAD_DEPENDIENTES,PDK_BUR_REP_NOM_PN_FEC_REC_INFO,PDK_BUR_REP_NOM_PN_FEC_DEFUNCION,PDK_BUR_REP_NOM_PN_MODIF,PDK_CLAVE_USUARIO,)" & _
" VALUES ( " & intPDK_ID_BURO_REPORTE_NOMBRE & ",  " & intPDK_ID_BURO & ", '" & strPDK_BUR_REP_NOM_PN_APE_PAT & "','" & strPDK_BUR_REP_NOM_PN_APE_MAT & "','" & strPDK_BUR_REP_NOM_PN_APE_ADIC & "','" & strPDK_BUR_REP_NOM_PN_PRIMER_NOMBRE & "','" & strPDK_BUR_REP_NOM_PN_SEGUNDO_NOMBRE & "','" & strPDK_BUR_REP_NOM_PN_FEC_NAC & "','" & strPDK_BUR_REP_NOM_PN_RFC & "','" & strPDK_BUR_REP_NOM_PN_PRE_PERSONAL & "','" & strPDK_BUR_REP_NOM_PN_SUFIJO & "','" & strPDK_BUR_REP_NOM_PN_NACIONALIDAD & "','" & strPDK_BUR_REP_NOM_PN_RESIDENCIA & "','" & strPDK_BUR_REP_NOM_PN_LIC_CONDUCIR & "','" & strPDK_BUR_REP_NOM_PN_EDO_CIVIL & "','" & strPDK_BUR_REP_NOM_PN_SEXO & "','" & strPDK_BUR_REP_NOM_PN_NUM_CED_PROF & "','" & strPDK_BUR_REP_NOM_PN_REG_ELECTORAL & "','" & strPDK_BUR_REP_NOM_PN_CVE_TAX_PAIS & "','" & strPDK_BUR_REP_NOM_PN_CVE_OTRO_PAIS & "','" & strPDK_BUR_REP_NOM_PN_DEPENDIENTES & "','" & strPDK_BUR_REP_NOM_PN_EDAD_DEPENDIENTES & "','" & strPDK_BUR_REP_NOM_PN_FEC_REC_INFO & "','" & strPDK_BUR_REP_NOM_PN_FEC_DEFUNCION & "','" & strPDK_BUR_REP_NOM_PN_MODIF & "','" & strPDK_CLAVE_USUARIO & "', " & _
")"
            Else
                'ActualizaRegistro()
                Exit Sub
            End If
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al guardar PDK_BURO_REPORTE_NOMBRE ")
        End Try
    End Sub
    Private Sub ActualizaRegistro()
        Dim strSql As String = String.Empty
        Dim strFechaOp As String = String.Empty
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Try
            strSql = "UPDATE PDK_BURO_REPORTE_NOMBRE " & _
               "SET " & _
             " PDK_ID_BURO_REPORTE_NOMBRE = " & intPDK_ID_BURO_REPORTE_NOMBRE & ", " & _
             " PDK_ID_BURO = " & intPDK_ID_BURO & ", " & _
                " PDK_BUR_REP_NOM_PN_APE_PAT = '" & strPDK_BUR_REP_NOM_PN_APE_PAT & "'," & _
                " PDK_BUR_REP_NOM_PN_APE_MAT = '" & strPDK_BUR_REP_NOM_PN_APE_MAT & "'," & _
                " PDK_BUR_REP_NOM_PN_APE_ADIC = '" & strPDK_BUR_REP_NOM_PN_APE_ADIC & "'," & _
                " PDK_BUR_REP_NOM_PN_PRIMER_NOMBRE = '" & strPDK_BUR_REP_NOM_PN_PRIMER_NOMBRE & "'," & _
                " PDK_BUR_REP_NOM_PN_SEGUNDO_NOMBRE = '" & strPDK_BUR_REP_NOM_PN_SEGUNDO_NOMBRE & "'," & _
                " PDK_BUR_REP_NOM_PN_FEC_NAC = '" & strPDK_BUR_REP_NOM_PN_FEC_NAC & "'," & _
                " PDK_BUR_REP_NOM_PN_RFC = '" & strPDK_BUR_REP_NOM_PN_RFC & "'," & _
                " PDK_BUR_REP_NOM_PN_PRE_PERSONAL = '" & strPDK_BUR_REP_NOM_PN_PRE_PERSONAL & "'," & _
                " PDK_BUR_REP_NOM_PN_SUFIJO = '" & strPDK_BUR_REP_NOM_PN_SUFIJO & "'," & _
                " PDK_BUR_REP_NOM_PN_NACIONALIDAD = '" & strPDK_BUR_REP_NOM_PN_NACIONALIDAD & "'," & _
                " PDK_BUR_REP_NOM_PN_RESIDENCIA = '" & strPDK_BUR_REP_NOM_PN_RESIDENCIA & "'," & _
                " PDK_BUR_REP_NOM_PN_LIC_CONDUCIR = '" & strPDK_BUR_REP_NOM_PN_LIC_CONDUCIR & "'," & _
                " PDK_BUR_REP_NOM_PN_EDO_CIVIL = '" & strPDK_BUR_REP_NOM_PN_EDO_CIVIL & "'," & _
                " PDK_BUR_REP_NOM_PN_SEXO = '" & strPDK_BUR_REP_NOM_PN_SEXO & "'," & _
                " PDK_BUR_REP_NOM_PN_NUM_CED_PROF = '" & strPDK_BUR_REP_NOM_PN_NUM_CED_PROF & "'," & _
                " PDK_BUR_REP_NOM_PN_REG_ELECTORAL = '" & strPDK_BUR_REP_NOM_PN_REG_ELECTORAL & "'," & _
                " PDK_BUR_REP_NOM_PN_CVE_TAX_PAIS = '" & strPDK_BUR_REP_NOM_PN_CVE_TAX_PAIS & "'," & _
                " PDK_BUR_REP_NOM_PN_CVE_OTRO_PAIS = '" & strPDK_BUR_REP_NOM_PN_CVE_OTRO_PAIS & "'," & _
                " PDK_BUR_REP_NOM_PN_DEPENDIENTES = '" & strPDK_BUR_REP_NOM_PN_DEPENDIENTES & "'," & _
                " PDK_BUR_REP_NOM_PN_EDAD_DEPENDIENTES = '" & strPDK_BUR_REP_NOM_PN_EDAD_DEPENDIENTES & "'," & _
                " PDK_BUR_REP_NOM_PN_FEC_REC_INFO = '" & strPDK_BUR_REP_NOM_PN_FEC_REC_INFO & "'," & _
                " PDK_BUR_REP_NOM_PN_FEC_DEFUNCION = '" & strPDK_BUR_REP_NOM_PN_FEC_DEFUNCION & "'," & _
                " PDK_BUR_REP_NOM_PN_MODIF = '" & strPDK_BUR_REP_NOM_PN_MODIF & "'," & _
                " PDK_CLAVE_USUARIO = '" & strPDK_CLAVE_USUARIO & "'," & _
             " WHERE PDK_ID_BURO_REPORTE_NOMBRE=  " & intPDK_ID_BURO_REPORTE_NOMBRE
            BD.EjecutarQuery(strSql)
        Catch ex As Exception
            Throw New Exception("Error al actualizar PDK_BURO_REPORTE_NOMBRE")
        End Try
    End Sub
#End Region
    '-------------------------- FIN PDK_BURO_REPORTE_NOMBRE-------------------------- 

End Class
