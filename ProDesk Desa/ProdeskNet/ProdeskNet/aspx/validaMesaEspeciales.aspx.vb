'BUG-PD-50 JBEJAR 18/05/2017 TAREA AUTOMATICA Valida mesas especiales 
'BUG-PD-71 JBEJAR 02/06/2017 Correciones en la comparacion de tarea automatica. 
'BUG-PD-94 JBEJAR 16/06/2017 Validaciones al no obtener datos.    
'BUG-PD-103 JBEJAR 16/06/2017 Validacion al obtener el riesgo ya que puede venir en "". 
'BUG-PD-134 JBEJAR 03/07/2017 Correcion al consultar  mas de un vehiculo.     
'BUG-PD-159 JBEJAR 24/07/2017 SE CAMBIA RESPONSE.
'BUG-PD-332 JBEJAR 11/01/2018 LOG TAREAS AUTOMATICAS. 
Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.Configurcion
Imports ProdeskNet.SN
Imports ProdeskNet.Catalogos
Partial Class aspx_validaMesaEspeciales
    Inherits System.Web.UI.Page
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim strError As String = String.Empty
    Dim _mensaje_ingreso As String = "Inicio_Tarea"
    Dim ds_tarea_log As DataSet
    Dim _Parametro As Integer
    Dim solicitud As Integer = 0
    Dim objespeciales As New clsEspeciales()
    Public Pantalla As Integer
    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpant As New ProdeskNet.SN.clsPantallas()
            Dim dts As New DataSet()
            Dim Bandera As Integer = 0
            Dim Bandera1 As Integer = 0
            Dim Bandera2 As Integer = 0
            Dim dsParametros As New DataSet
            Dim dsStopara As New DataSet
            Dim dsStopara2 As New DataSet
            Dim dsStorpara3 As New DataSet
            Dim dsStoparam As New DataSet
            Dim clien As New clsDatosCliente
            Dim objTarea As New clsSolicitudes(0)
            Dim objespeciales As New clsEspeciales()
            Dim solicitud As Integer = 0
            Dim ValTareas As Integer = 0
            Dim VehiP As Integer = 0
            Dim Vehi As Integer = 0
            Dim Mes As Integer = 0
            Dim MontoP As String = ""
            Dim MontoP1 As Decimal
            Dim MontoM As String = ""
            Dim MontoA As String = ""
            Dim MontoM1 As Decimal
            Dim MontoA1 As Decimal
            Dim RiesgoP As Decimal
            Dim Riesgo As String = ""
            Dim MontoD As String = ""
            Dim MarcB As Boolean
            Dim RFC As String = String.Empty
            Dim objFlujos As New clsSolicitudes(0)
            Dim ds_siguienteTarea As DataSet


            Pantalla = Convert.ToInt32(Val(Request("idPantalla")).ToString())
            objTarea.PDK_ID_SOLICITUD = Request("Sol")
            objTarea.PDK_ID_PANTALLA = Pantalla
            solicitud = Request("Sol")
            clien.GetDatosCliente(Request("sol"))

            RFC = clien.propRFC

            ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & Pantalla.ToString())

            'CONSULTA PARAMETROS DE CONFIGURACIONES ESPECIALES 
            dsParametros = BD.EjecutarQuery("SELECT PDK_NUM_VEHICULOS,PDK_PERIODO_AUTORIZACION,PDK_RIESGO,PDK_MONTO_ALIANZA,PDK_MONTO_MULTIMARCA  FROM PDK_CONFIG_ESPECIALES WHERE PDK_ID_SECCCERO=1")

            objespeciales.ID_SOLICITUD = Request("Sol")
            objespeciales.RFC = RFC

            dsStopara = objespeciales.GetEspeciales(1)
            dsStopara2 = objespeciales.GetEspeciales(2)
            dsStorpara3 = objespeciales.GetEspeciales(3)

            If dsStorpara3.Tables(0).Rows.Count > 0 Then 'BUG-PD-94 
                Riesgo = IIf(dsStorpara3.Tables(0).Rows(0).Item("Riesgo").ToString() = String.Empty, "0", dsStorpara3.Tables(0).Rows(0).Item("Riesgo").ToString()) 'BUG-PD-103 
            Else
                Riesgo = "0" 'BUG-PD-103  
            End If
            If dsStopara.Tables(0).Rows.Count() > 0 Then 'BUG-PD-94  
                Vehi = CInt(dsStopara.Tables(0).Rows(0).Item("NUMERO DE DATOS"))
            End If

            If dsParametros.Tables.Count > 0 Then
                If dsParametros.Tables(0).Rows.Count > 0 Then
                    VehiP = CInt(dsParametros.Tables(0).Rows(0).Item("PDK_NUM_VEHICULOS"))
                    MontoP = Replace(dsStopara2.Tables(0).Rows(0).Item("Monto Solicitado").ToString, ",", "")
                    MontoP1 = Val(MontoP)
                    RiesgoP = CInt(dsParametros.Tables(0).Rows(0).Item("PDK_RIESGO"))
                    MontoM = CDbl(dsParametros.Tables(0).Rows(0).Item("PDK_MONTO_MULTIMARCA"))
                    MontoA = CDbl(dsParametros.Tables(0).Rows(0).Item("PDK_MONTO_ALIANZA"))
                    MontoM1 = Val(MontoM)
                    MontoA1 = Val(MontoA)
                    MarcB = dsStopara2.Tables(0).Rows(0).Item("ES_MULTIMARCA")

                    If MarcB = True Then

                        If MontoP1 > MontoM1 Then
                            Bandera = 1
                        End If
                    ElseIf MontoP1 > MontoA1 Then

                        Bandera = 1

                    End If

                    If CType(Riesgo, Double) > RiesgoP Then 'BUG-PD-103 
                        Bandera1 = 1
                    End If

                    If Vehi >= VehiP Then
                        Bandera2 = 1
                    End If
                End If
            End If

            dts = objpant.CargaPantallas(Pantalla)

            ds_tarea_log = objespeciales.ConsultaParametro()

            If (Not IsNothing(ds_tarea_log) AndAlso ds_tarea_log.Tables.Count > 0 AndAlso ds_tarea_log.Tables(0).Rows.Count() > 0) Then

                _Parametro = ds_tarea_log.Tables(0).Rows(0).Item("PARAMETRO")

                If _Parametro = 2 Then
                    objespeciales.Tarea_Actual = Pantalla
                    objespeciales.ID_SOLICITUD() = solicitud
                    objespeciales.Mensaje() = _mensaje_ingreso
                    objespeciales.InsertLog()

                End If

            End If

            If dts.Tables.Count > 0 Then
                If dts.Tables(0).Rows.Count > 0 Then
                    If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Or (Bandera = 0) Then
                        If Bandera = 1 Or Bandera1 = 1 Or Bandera2 = 1 Then

                            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))

                        Else

                            asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))

                        End If

                    End If
                End If
            End If
        End If
    End Sub
    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Try
            'SE LLENAN LOS PARAMETROS PARA EJECUTAR METODOS DE LA CLASE clsSolicitudes
            Solicitudes.BOTON = 64                                  'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_SOLICITUD = Request("Sol")      'PARAMETRO PARA ValNegocio() Y ManejaTarea()
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")          'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_PANTALLA = Pantalla    'PARAMETRO PARA ManejaTarea()
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla    'PARAMETRO PARA ManejaTarea()

            dsresult = Solicitudes.ValNegocio(1)                    'SE ADELANTA LA TAREA ESPECIFICADA(RECHAZO O NORECHAZO) Y OBTENEMOS MENSAJE
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje  ' SE ASIGNA MENSAJE COMO PARAMETRO PARA ManejaTarea()
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "AbreError", "PopUpLetreroRedirect('" & mensaje & "', '../aspx/consultaPanelControl.aspx');", True)
                Exit Sub
            End If

            dslink = objtarea.SiguienteTarea(Solicitudes.PDK_ID_SOLICITUD)

            muestrapant = objpantalla.SiguientePantalla(Solicitudes.PDK_ID_SOLICITUD)

            Dim strLocation As String = String.Empty

            If (Not IsNothing(ds_tarea_log) AndAlso ds_tarea_log.Tables.Count > 0 AndAlso ds_tarea_log.Tables(0).Rows.Count() > 0) Then

                _Parametro = ds_tarea_log.Tables(0).Rows(0).Item("PARAMETRO")

                If _Parametro = 2 Then
                    objespeciales.Tarea_Actual = Pantalla
                    objespeciales.ID_SOLICITUD = solicitud
                    objespeciales.Mensaje = mensaje
                    objespeciales.Tarea_siguiente = idAsignarPantalla
                    objespeciales.InsertLog()
                End If

            End If

            If muestrapant = 0 Then
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString)
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            ElseIf muestrapant = 2 Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            End If

        Catch ex As Exception
            Master.MensajeError(mensaje)
        End Try
    End Sub

End Class
