'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BUG-PD-01: AMR: 22/12/2016: HTTP Error 404 - Not Found al procesar Precalificación.

Imports System.Text

Public Class clsCteIndeseable

    Private _strError As String = String.Empty
    Private _IDCTEINDESEABLE As Integer = 0
    Private _CTEINDESEABLECVE As String = String.Empty
    Private _NOMBRE As String = String.Empty
    Private _ACTIVO As Integer = 0
    Private _FECHAMODIF As String = String.Empty
    Private _CVEUSUARIO As Integer = 0

    Sub New()

    End Sub


    Public ReadOnly Property StrError As String
        Get
            Return _strError
        End Get
    End Property

    Public ReadOnly Property IDCTEINDESEABLE As Integer
        Get
            Return _IDCTEINDESEABLE
        End Get
    End Property

    Public Property CTEINDESEABLECVE As String
        Get
            Return _CTEINDESEABLECVE
        End Get
        Set(value As String)
            _CTEINDESEABLECVE = value
        End Set
    End Property

    Public Property NOMBRE As String
        Get
            Return _NOMBRE
        End Get
        Set(value As String)
            _NOMBRE = value
        End Set
    End Property

    Public Property ACTIVO As Integer
        Get
            Return _ACTIVO
        End Get
        Set(value As Integer)
            _ACTIVO = value
        End Set
    End Property

    Public Property FECHAMODIF As String
        Get
            Return _FECHAMODIF
        End Get
        Set(value As String)
            _FECHAMODIF = value
        End Set
    End Property

    Public Property CVEUSUARIO As Integer
        Get
            Return _CVEUSUARIO
        End Get
        Set(value As Integer)
            _CVEUSUARIO = value
        End Set
    End Property

    Public Function CatalogoCteIndeseable(ByVal strCve As String) As String
        Dim strindeseable As String = String.Empty
        Dim dts As DataSet = New DataSet()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder
        Try
            sql.AppendLine("SELECT PDK_ID_CTEINDESEABLE, PDK_CTEINDESEABLE_CVE,  PDK_CTEINDESEABLE_NOMBRE,")
            sql.AppendLine("PDK_TAR_ACTIVO, PDK_FECHA_MODIF, PDK_CLAVE_USUARIO")
            sql.AppendLine("FROM PDK_CAT_CTEINDEASEABLE")
            sql.AppendLine("WHERE PDK_CTEINDESEABLE_CVE = '" & strCve & "'")

            dts = BD.EjecutarQuery(sql.ToString)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    strindeseable = dts.Tables(0).Rows(0).Item("PDK_CTEINDESEABLE_CVE").ToString
                    Return strindeseable
                Else
                    Return strindeseable
                End If
            Else
                _strError = "Error al cargar la información."
                Return strindeseable
            End If

        Catch ex As Exception
            _strError = "Exception: Error al cargar la información."
            Return strindeseable
        End Try
    End Function
    Public Function ConsultaGetCustomer(ByVal intSol As Integer) As DataSet

        Dim dts As DataSet = New DataSet()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder

        sql.AppendLine("SELECT	A.NOMBRE1 + CASE WHEN ISNULL(A.NOMBRE2, '') = '' THEN '' ELSE ' ' + NOMBRE2 END NOMBRE1, A.APELLIDO_PATERNO, A.APELLIDO_MATERNO,")
        sql.AppendLine("A.RFC, CONVERT(VARCHAR(10), A.ANO_NACIMIENTO) + '-' +")
        sql.AppendLine("CASE WHEN A.MES_NACIMIENTO < 10 THEN '0' + CONVERT(VARCHAR(10), A.MES_NACIMIENTO) ELSE CONVERT(VARCHAR(10), A.MES_NACIMIENTO) END + '-' +")
        sql.AppendLine("CASE WHEN A.DIA_NACIMIENTO < 10 THEN '0' + CONVERT(VARCHAR(10), A.DIA_NACIMIENTO) ELSE CONVERT(VARCHAR(10), A.DIA_NACIMIENTO) END FECHA_NAC,")
        sql.AppendLine("ISNULL(A.HOMOCLAVE, '') AS HOMOCLAVE, B.CP,  SUBSTRING(B.COLONIA,0,31)AS COLONIA, LTRIM(RTRIM(B.CALLE)) + ' ' +  LTRIM(RTRIM(B.NUM_EXT)) + ' ' + LTRIM(RTRIM(B.NUM_INT)) AS CALLE, B.CIUDAD,")
        sql.AppendLine("RTRIM(LTRIM(B.ESTADO)) AS ESTADO, CASE WHEN B.SEXO = 'MASCULINO' THEN 'M' WHEN B.SEXO = 'FEMENINO' THEN 'F' ELSE 'M' END SEXO, A.RFC + ISNULL(A.HOMOCLAVE, '') AS RFC2, RTRIM(LTRIM(C.EFD_CL_BNC)) AS EFD_CL_BNC,DELEGA_O_MUNI")
        sql.AppendLine("FROM	PDK_TAB_SOLICITANTE A")
        sql.AppendLine("INNER JOIN PDK_TAB_DATOS_PERSONALES B ON B.PDK_ID_SECCCERO = A.PDK_ID_SECCCERO")
        sql.AppendLine("INNER JOIN PDK_CAT_EFEDERATIVA C ON C.EFD_DS_ENTIDAD = B.ESTADO")
        sql.AppendLine("WHERE	A.PDK_ID_SECCCERO = " & intSol.ToString)

        Try
            dts = BD.EjecutarQuery(sql.ToString)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    Return dts
                Else
                    _strError = "Error al consultar la información."
                    Return Nothing
                End If
            Else
                _strError = "Error al consultar la información."
                Return Nothing
            End If

        Catch ex As Exception
            _strError = "Exception: Error al cargar la información."
            Return Nothing
        End Try
    End Function
End Class
