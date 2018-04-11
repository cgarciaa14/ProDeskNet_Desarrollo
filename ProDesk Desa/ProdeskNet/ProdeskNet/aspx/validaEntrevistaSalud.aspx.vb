'BUG-PD-19 JBB 10/03/2017 CORRECIONES PARA FINALIZAR EL FLUJO CORRECTAMENTE. 
'BUG-PD-125 JBEJAR 29/06/2017 VALIDACION PARA REGRESAR A LA TAREA ANTERIOR MANUAL SE CAMBIA RESPONSE REDIRECT POR SCRIPT MANAGER. 
'BUG-PD-205 RHERNANDEZ 31/08/17 CONTROL DE EXCEPCIONES DEPENDIENDO DE QUE EL MENSAJE PROVENGA DE VALNEGOCIO
Imports System.Data
Imports System.Data.SqlClient
Imports ProdeskNet.Configurcion
Partial Class aspx_validaEntrevistaSalud
    Inherits System.Web.UI.Page
    Dim BD As ProdeskNet.BD.clsManejaBD = New ProdeskNet.BD.clsManejaBD()
    Dim strError As String = String.Empty


    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        btnProcesar_Click(sender, e)

    End Sub

    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs)
        Try
            If Not IsPostBack Then
                Dim objpant As New ProdeskNet.SN.clsPantallas()
                Dim dts As New DataSet()
                Dim Bandera As Integer = 0
                Dim dsParametros As New DataSet
                Dim objTarea As New clsSolicitudes(0)
                Dim dsValidaTarea As DataSet
                Dim ValTareas As Integer = 0
                Dim MontoMax As Decimal
                Dim EdadMeses As Integer
                Dim MontoD As String = ""
                Dim Monto As Decimal
                Dim Plazo As Integer
                Dim Fecha As Date = DateTime.Now.ToString()
                Dim FechaN As Date
                Dim dsDatosCliente As DataSet
                Dim objFlujos As New clsSolicitudes(0)
                Dim ds_siguienteTarea As DataSet
                Dim pantalla As Integer = 0


                objTarea.PDK_ID_SOLICITUD = Request("sol")
                objTarea.PDK_ID_PANTALLA = Request("idPantalla")
                pantalla = objTarea.PDK_ID_PANTALLA
                dsValidaTarea = objTarea.ManejaTarea(1)
                ds_siguienteTarea = BD.EjecutarQuery("SELECT PDK_ID_TAREAS,PDK_TAR_NOMBRE,PDK_ID_TAREAS_RECHAZO,PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS=" & pantalla)



                If dsValidaTarea.Tables.Count > 0 Then
                    If dsValidaTarea.Tables(0).Rows.Count > 0 Then
                        ValTareas = dsValidaTarea.Tables(0).Rows(0).Item("VALIDA_TAREA")
                    End If
                End If

                'CONSULTA PARA COMPARAR MONTO Y FECHA
                objFlujos.PDK_ID_SOLICITUD = Request("sol")
                dsDatosCliente = objFlujos.ConsultaCuestionarios(2)
                If dsDatosCliente.Tables.Count > 0 Then
                    If dsDatosCliente.Tables(0).Rows.Count > 0 Then
                        FechaN = dsDatosCliente.Tables(0).Rows(0).Item("ANO_NACIMIENTO").ToString + "-" + dsDatosCliente.Tables(0).Rows(0).Item("MES_NACIMIENTO").ToString + "-" + dsDatosCliente.Tables(0).Rows(0).Item("DIA_NACIMIENTO").ToString
                        MontoD = Replace(dsDatosCliente.Tables(0).Rows(0).Item("MONTO").ToString, ",", "")
                        Monto = Val(MontoD)
                        Plazo = dsDatosCliente.Tables(0).Rows(0).Item("PLAZO").ToString
                    End If
                End If

                'CONSULTA PARAMETROS PARA COMPARAR MONTO Y FECHA
                dsParametros = BD.EjecutarQuery("SELECT PDK_ID_PARAMETROS_SISTEMA,PDK_PAR_SIS_VALOR_TEXTO FROM PDK_PARAMETROS_SISTEMA WHERE PDK_PAR_SIS_ID_PADRE=233 ORDER BY 1")

                If dsParametros.Tables.Count > 0 Then
                    If dsParametros.Tables(0).Rows.Count > 0 Then
                        MontoMax = Val(dsParametros.Tables(0).Rows(0).Item("PDK_PAR_SIS_VALOR_TEXTO").ToString)
                        EdadMeses = Val(dsParametros.Tables(0).Rows(1).Item("PDK_PAR_SIS_VALOR_TEXTO").ToString)

                        'Monto = 700000 '------------------------------------------------------- <------------------BORRAME

                        If Monto > MontoMax Then
                            Bandera = 1
                        End If

                        Fecha = DateAdd("m", Val(Plazo), Fecha)
                        Dim DifMeses As Integer
                        DifMeses = DateDiff(DateInterval.Month, FechaN, Fecha)
                        ' DifMeses = 752 '------------------------------------------------------------ <---------------BORRAME
                        If DifMeses > EdadMeses Then
                            Bandera = 1
                        End If

                    End If
                End If

                dts = objpant.CargaPantallas(Val(Request("idPantalla")).ToString)

                If dts.Tables.Count > 0 Then
                    If dts.Tables(0).Rows.Count > 0 Then
                        If dts.Tables(0).Rows(0).Item("PDK_PANT_MOSTRAR") = 0 Or (Bandera = 1 And ValTareas = 1) Then
                            If Bandera = 1 Then

                                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_RECHAZO"))

                            Else

                                asignaTarea(ds_siguienteTarea.Tables(0).Rows(0).Item("PDK_ID_TAREAS_NORECHAZO"))

                            End If

                        End If
                    End If
                End If

            End If

        Catch ex As Exception
            Dim ds_tareaAnterior As DataSet 'BUG-PD-125 TAREA ANTERIOR MANUAL JBEJAR 
            Dim Tarea_anterior As Integer = 0
            BD.AgregaParametro("@IdSolicitud", ProdeskNet.BD.TipoDato.Entero, Request("sol"))
            ds_tareaAnterior = BD.EjecutaStoredProcedure("SpGetTareaAnterior")
            Tarea_anterior = ds_tareaAnterior.Tables(0).Rows(0).Item("PDK_ID_TAREAS")
            asignaTarea(Tarea_anterior) ' EN CASO DE FALLAR IRA A LA TAREA ANTERIOR MANUAL.  
        End Try
    End Sub


    Private Sub asignaTarea(ByVal idAsignarPantalla As Integer)
        Dim dsresult As DataSet = New DataSet()
        Dim dslink As DataSet = New DataSet()
        Dim muestrapant As Integer = Nothing
        Dim objtarea As New ProdeskNet.SN.clsCatTareas()
        Dim objpantalla As New ProdeskNet.SN.clsPantallas()
        Dim Solicitudes As New ProdeskNet.Configurcion.clsSolicitudes(Val(Request("Sol")))
        Dim mensaje As String = String.Empty
        Dim strLocat As String = String.Empty
        Try
            'SE LLENAN LOS PARAMETROS PARA EJECUTAR METODOS DE LA CLASE clsSolicitudes
            Solicitudes.BOTON = 64                                  'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_SOLICITUD = Val(Request("Sol"))      'PARAMETRO PARA ValNegocio() Y ManejaTarea()
            Solicitudes.PDK_CLAVE_USUARIO = Session("IdUsua")          'PARAMETRO PARA ValNegocio()
            Solicitudes.PDK_ID_PANTALLA = Request("idPantalla")     'PARAMETRO PARA ManejaTarea()
            Solicitudes.PDK_ID_CAT_RESULTADO = idAsignarPantalla    'PARAMETRO PARA ManejaTarea()

            dsresult = Solicitudes.ValNegocio(1)                    'SE ADELANTA LA TAREA ESPECIFICADA(RECHAZO O NORECHAZO) Y OBTENEMOS MENSAJE
            mensaje = dsresult.Tables(0).Rows(0).Item("MENSAJE").ToString
            Solicitudes.MENSAJE = mensaje  ' SE ASIGNA MENSAJE COMO PARAMETRO PARA ManejaTarea()
            Solicitudes.ManejaTarea(5)

            If mensaje <> "Tarea Exitosa" Then
                Throw New Exception(mensaje)
            End If

            'BD.EjecutarQuery("exec spvalNegocio " & Val(Request("Sol")).ToString & ",64," & Val(Request("usuario")).ToString)

            dslink = objtarea.SiguienteTarea(Val(Request("Sol")))

            muestrapant = objpantalla.SiguientePantalla(Val(Request("Sol")))

            Dim strLocation As String = String.Empty


            If muestrapant = 0 Then
                strLocation = ("../aspx/" & dslink.Tables(0).Rows(0).Item("PDK_PANT_LINK").ToString & "?idPantalla=" & dslink.Tables(0).Rows(0).Item("PDK_ID_PANTALLAS").ToString & "&sol=" & Val(Request("Sol")).ToString & "&usuario=" & Val(Request("usuario")).ToString) 'BUG-PD-125
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True)
            ElseIf muestrapant = 2 Then
                strLocation = ("../aspx/consultaPanelControl.aspx")
                ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "RedireccionaPagina", "window.location = '" & strLocation & "';", True) 'BUG-PD-125 
            End If

        Catch ex As Exception
            Master.MensajeError(ex.Message)
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
