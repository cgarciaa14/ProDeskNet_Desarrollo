'BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
'BUG-PD-01: AMR: 22/12/2016: HTTP Error 404 - Not Found al procesar Precalificación.
'BUG-PD-87 GVARGAS 15/06/2017 Correcion consulta

Imports System.Text
Public Class clsTabSeccionCero

    Private _strError As String = String.Empty
    Private _PDKIDSECCCERO As Integer = 0
    Private _FECHA As String = String.Empty
    Private _CLAVEUSUARIO As Integer = 0
    Private _PDKDISTCLAVE As Integer = 0
    Private _PDKCONTADOR As Integer = 0
    Private _PDKFECHAINICIO As String = String.Empty
    Private _PDKFECHAFINAL As String = String.Empty
    Private _PDKIDEMPRESA As Integer = 0
    Private _PDKIDPRODUCTO As Integer = 0
    Private _PDKIDPERJURIDICA As Integer = 0
    Private _PDKSOLIRFC As String = String.Empty
    Private _IDCOTIZACION As Integer = 0
    Private _PDKCONDITAR As Integer = 0
    Private _CTOFLCVE As String = String.Empty
    Private _PDKSTATUSDOCUMENTOS As Integer = 0
    Private _PDKSTATUSCREDITO As Integer = 0
    Private _PDKTAREAACTUAL As Integer = 0
    Private _PDKSTVALIDASOLICITUD As Integer = 0

    Sub New()

    End Sub

    Public Property StrError As String
        Get
            Return _strError
        End Get
        Set(value As String)
            _strError = value
        End Set
    End Property

    Public ReadOnly Property PDKIDSECCCERO As Integer
        Get
            Return _PDKIDSECCCERO
        End Get
    End Property

    Public Property FECHA As String
        Get
            Return _FECHA
        End Get
        Set(value As String)
            _FECHA = value
        End Set
    End Property

    Public Property CLAVEUSUARIO As Integer
        Get
            Return _CLAVEUSUARIO
        End Get
        Set(value As Integer)
            _CLAVEUSUARIO = value
        End Set
    End Property

    Public Property PDKDISTCLAVE As Integer
        Get
            Return _PDKDISTCLAVE
        End Get
        Set(value As Integer)
            _PDKDISTCLAVE = value
        End Set
    End Property

    Public Property PDKCONTADOR As Integer
        Get
            Return _PDKCONTADOR
        End Get
        Set(value As Integer)
            _PDKCONTADOR = value
        End Set
    End Property

    Public Property PDKFECHAINICIO As String
        Get
            Return _PDKFECHAINICIO
        End Get
        Set(value As String)
            _PDKFECHAINICIO = value
        End Set
    End Property

    Public Property PDKFECHAFINAL As String
        Get
            Return _PDKFECHAFINAL
        End Get
        Set(value As String)
            _PDKFECHAFINAL = value
        End Set
    End Property

    Public Property PDKIDEMPRESA As Integer
        Get
            Return _PDKIDEMPRESA
        End Get
        Set(value As Integer)
            _PDKIDEMPRESA = value
        End Set
    End Property

    Public Property PDKIDPRODUCTO As Integer
        Get
            Return _PDKIDPRODUCTO
        End Get
        Set(value As Integer)
            _PDKIDPRODUCTO = value
        End Set
    End Property

    Public Property PDKIDPERJURIDICA As Integer
        Get
            Return _PDKIDPERJURIDICA
        End Get
        Set(value As Integer)
            _PDKIDPERJURIDICA = value
        End Set
    End Property

    Public Property PDKSOLIRFC As String
        Get
            Return _PDKSOLIRFC
        End Get
        Set(value As String)
            _PDKSOLIRFC = value
        End Set
    End Property

    Public Property IDCOTIZACION As Integer
        Get
            Return _IDCOTIZACION
        End Get
        Set(value As Integer)
            _IDCOTIZACION = value
        End Set
    End Property

    Public Property PDKCONDITAR As Integer
        Get
            Return _PDKCONDITAR
        End Get
        Set(value As Integer)
            _PDKCONDITAR = value
        End Set
    End Property

    Public Property CTOFLCVE As String
        Get
            Return _CTOFLCVE
        End Get
        Set(value As String)
            _CTOFLCVE = value
        End Set
    End Property

    Public Property PDKSTATUSDOCUMENTOS As Integer
        Get
            Return _PDKSTATUSDOCUMENTOS
        End Get
        Set(value As Integer)
            _PDKSTATUSDOCUMENTOS = value
        End Set
    End Property

    Public Property PDKSTATUSCREDITO As Integer
        Get
            Return _PDKSTATUSCREDITO
        End Get
        Set(value As Integer)
            _PDKSTATUSCREDITO = value
        End Set
    End Property

    Public Property PDKTAREAACTUAL As Integer
        Get
            Return _PDKTAREAACTUAL
        End Get
        Set(value As Integer)
            _PDKTAREAACTUAL = value
        End Set
    End Property

    Public Function CargaSeccionCero(ByVal intsol As Integer) As DataSet

        Dim dts As DataSet = New DataSet()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder

        Try

            sql.AppendLine("SELECT PDK_ID_SECCCERO, FECHA, CLAVE_USUARIO, PDK_DIST_CLAVE,PDK_CONTADOR, PDK_FECHA_INICIO, PDK_FECHA_FINAL,")
            sql.AppendLine("PDK_ID_EMPRESA,PDK_ID_PRODUCTO,PDK_ID_PER_JURIDICA,PDK_SOLI_RFC,ID_COTIZACION, PDK_CONDI_TAR,")
            sql.AppendLine("CTO_FL_CVE,PDK_STATUS_DOCUMENTOS,PDK_STATUS_CREDITO,PDK_TAREA_ACTUAL, PDK_ST_VALIDA_SOLICITUD")
            sql.AppendLine("FROM PDK_TAB_SECCION_CERO")
            sql.AppendLine("WHERE PDK_ID_SECCCERO = " & intsol.ToString)

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

    Public Function CargaDatosCotiza(ByVal intsol As Integer) As DataSet
        Dim dtssol As New DataSet()
        Dim dts As DataSet = New DataSet()
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder

        Try

            dtssol = CargaSeccionCero(intsol)
            If dtssol.Tables.Count > 0 Then
                If dtssol.Tables(0).Rows.Count > 0 Then

                    sql.AppendLine("SELECT ID_COTIZACION, ID_TIPO_COT, ID_EMPRESA, ID_AGENCIA, ID_PROMOTOR, ID_VENDEDOR, ID_ASESOR, ID_PER_JURIDICA, ID_GIRO,")
                    sql.AppendLine("ID_TIPO_PRODUCTO, ID_MARCA, ID_CLASIFICACION, ID_PRODUCTO, MARCA_USADOS, PRODUCTO_USADOS, ANIO_MODELO_USADOS, PRECIO_LISTA,")
                    sql.AppendLine("PRECIO_PRODUCTO, MONTO_ACCESORIOS, MONTO_ACCESORIOS_CONTADO, ISNULL(MONTO_ACCESORIOS_NO_SEGURO, 0), UNIDADES_PRODUCTO, ID_MONEDA_FACTURA,")
                    sql.AppendLine("ID_PAQUETE, ID_TIPO_OPER, ID_MONEDA, VALOR_TIPO_CAMBIO, CONSIDERA_ENGANCHE_ACCESORIOS, PTJ_ENGANCHE, PTJ_ENGANCHE_REAL,")
                    sql.AppendLine("ENGANCHE, CALCULO_PAGO_INICIAL, PAGOS_GRACIA_CAPITAL, PAGOS_GRACIA_INTERES, RENTAS_DEPOSITO, MONTO_RENTAS_DEPOSITO,")
                    sql.AppendLine("VALOR_RESIDUAL, PTJ_SERV_FINAN, SERVICIOS_FINANCIEROS, PTJ_OPCION_COMPRA, MONTO_OPCION_COMPRA, USA_TASA_PCP, TASA_PCP,")
                    sql.AppendLine("PTJ_BLIND_DISCOUNT, ID_TASA_IVA, TASA_IVA, TASA_INTERES, TASA_INTERES_SEGURO, MANEJA_TASA_VARIABLE, ID_TASA_VARIABLE,")
                    sql.AppendLine("TASA_VARIABLE, ID_PLAZO, VALOR_PLAZO, ID_ESQUEMA_FINAN, ID_CALENDARIO, ID_PERIODICIDAD, DIA_PAGO, ID_TIPO_VENCIMIENTO,")
                    sql.AppendLine("PRIMER_PAGO_IRREG, ID_TIPO_SEGURO, ID_ASEGURADORA, ID_ESTADO, ID_PAQUETE_SEGURO, MONTO_SEGURO, MONTO_SEGURO_REGALADO,")
                    sql.AppendLine("ID_TIPO_CALCULO_SEGURO, CAPTURA_MANUAL_SEGURO, ID_PLAZO_SEGURO, VALOR_PLAZO_SEGURO, FACTOR_SEG_VIDA, CALCULAR_IVA_SEGURO_VIDA,")
                    sql.AppendLine("ID_TIPO_SEGURO_VIDA, PUNTOS_ADIC_TASA, PAGO_PERIODO, PAGO_PERIODO_SEGURO, PAGO_PERIODO_SEGURO_VIDA, MONTO_SUBSIDIO,")
                    sql.AppendLine("PTJ_SUBSIDIO, TASA_SUBSIDIO, INCENTIVO_VENTAS, VALOR_ADAPTACION, INCLUYE_RC, UID_EMPRESA, UID_ASEGURADORA, UID_PRODUCTO,")
                    sql.AppendLine("UID_ESTADO, UID_PAQ_SEG, UID_MONEDA, UID_TIPO_PAGO, UID_PLAZO, UID_FAMILIA, UID_TIPO_PROD, FEC_REG, MONTO_SEGURO1, MONTO_SEGURO2,")
                    sql.AppendLine("MONTO_SEGURO3, ID_FAMILIA, ID_ORIGEN, ID_COTIZADOR, PTJ_ENGANCHE_DOS, ID_TIPO_CALCULO_SEGURO_VIDA, UTILITARIO, LOANTOVALUE")
                    sql.AppendLine("FROM COTIZACIONES")
                    sql.AppendLine("WHERE ID_COTIZACION = " & dtssol.Tables(0).Rows(0).Item("ID_COTIZACION").ToString)

                    dts = BD.EjecutarQuery(sql.ToString, 1)

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

                End If
            Else

            End If

        Catch ex As Exception
            _strError = "Exception: Error al cargar la información."
            Return Nothing
        End Try

    End Function

    'Cliente Indeseable
    Public Function ActSeccionCero(ByVal intsol As Integer, ByVal intusr As Integer, ByVal estatus As Integer)
        Dim reg As Integer = 0
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder
        Dim dts As New DataSet()
        Dim tarea As Integer = Nothing

        Try

            dts = CargaSeccionCero(intsol)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    tarea = dts.Tables(0).Rows(0).Item("PDK_TAREA_ACTUAL")
                End If
            End If

            If estatus = 238 Then
                sql.AppendLine("UPDATE PDK_TAB_SECCION_CERO SET PDK_STATUS_CREDITO = " & estatus.ToString & ", " & "PDK_FECHA_FINAL = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "'")
                sql.AppendLine(" WHERE PDK_ID_SECCCERO = " & intsol.ToString)
            Else
                sql.AppendLine("UPDATE PDK_TAB_SECCION_CERO SET PDK_STATUS_CREDITO = " & estatus.ToString & ", " & "PDK_FECHA_FINAL = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "'")
                sql.AppendLine(" WHERE PDK_ID_SECCCERO = " & intsol.ToString)
                sql.AppendLine("")
                sql.AppendLine("UPDATE PDK_OPE_SOLICITUD SET PDK_OPE_STATUS_PROCESO = 42, PDK_CLAVE_USUARIO = " & intusr.ToString & ", PDK_OPE_MODIF = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "'")
                sql.AppendLine("WHERE PDK_ID_SOLICITUD = " & intsol.ToString)
                sql.AppendLine("")
                sql.AppendLine("UPDATE PDK_OPE_SOLICITUD SET PDK_OPE_STATUS_TAREA = 42, PDK_CLAVE_USUARIO = " & intusr.ToString & ", PDK_OPE_MODIF = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', PDK_OPE_FECHA_FINAL = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "'")
                sql.AppendLine("WHERE PDK_ID_TAREAS = " & tarea.ToString & " AND PDK_ID_SOLICITUD = " & intsol.ToString)

            End If


            reg = BD.ExInsUpd(sql.ToString)

            Return reg
        Catch ex As Exception
            Return reg
        End Try

    End Function

    ''Cliente Viable / No Viable
    Public Function ActSeccionCeroProsp(ByVal intsol As Integer, ByVal intusr As Integer, ByVal Status As Integer)
        Dim reg As Integer = 0
        Dim BD As New ProdeskNet.BD.clsManejaBD
        Dim sql As New StringBuilder
        Dim dts As New DataSet()
        Dim tarea As Integer = Nothing

        Try

            dts = CargaSeccionCero(intsol)

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    tarea = dts.Tables(0).Rows(0).Item("PDK_TAREA_ACTUAL")
                End If
            End If

            sql.AppendLine("UPDATE PDK_TAB_SECCION_CERO SET PDK_STATUS_CREDITO = " & Status.ToString & ", PDK_FECHA_FINAL = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "'")
            sql.AppendLine(" WHERE PDK_ID_SECCCERO = " & intsol.ToString)
            sql.AppendLine("")
            If Status = 231 Then
                sql.AppendLine("UPDATE PDK_TAB_DATOS_SOLICITANTE SET STATUS_CREDITO = 'VIABLE', PDK_FECHA_MODIF = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', PDK_CLAVE_USUARIO = " & intusr.ToString)
                sql.AppendLine(" WHERE PDK_ID_SECCCERO = " & intsol.ToString)
            ElseIf 232 Then
                sql.AppendLine("UPDATE PDK_TAB_DATOS_SOLICITANTE SET STATUS_CREDITO = 'NO VIABLE', PDK_FECHA_MODIF = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', PDK_CLAVE_USUARIO = " & intusr.ToString)
                sql.AppendLine(" WHERE PDK_ID_SECCCERO = " & intsol.ToString)
                sql.AppendLine("")
                sql.AppendLine("UPDATE PDK_OPE_SOLICITUD SET PDK_OPE_STATUS_PROCESO = 42, PDK_CLAVE_USUARIO = " & intusr.ToString & ", PDK_OPE_MODIF = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "'")
                sql.AppendLine("WHERE PDK_ID_SOLICITUD = " & intsol.ToString)
                sql.AppendLine("")
                sql.AppendLine("UPDATE PDK_OPE_SOLICITUD SET PDK_OPE_STATUS_TAREA = 42, PDK_CLAVE_USUARIO = " & intusr.ToString & ", PDK_OPE_MODIF = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', PDK_OPE_FECHA_FINAL = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "'")
                sql.AppendLine("WHERE PDK_ID_TAREAS = " & tarea.ToString & " AND PDK_ID_SOLICITUD = " & intsol.ToString)
            End If

            reg = BD.ExInsUpd(sql.ToString)

            Return reg
        Catch ex As Exception
            Return reg
        End Try

    End Function
End Class
