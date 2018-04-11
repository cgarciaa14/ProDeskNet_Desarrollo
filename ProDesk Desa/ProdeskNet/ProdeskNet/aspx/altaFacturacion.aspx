<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="altaFacturacion.aspx.vb" Inherits="altaFacturacion" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<%--Tracker:INC-B-2019:JRDA:Regresar--%>

<asp:Content ID="content1" ContentPlaceHolderID="cphPantallas" runat="server">

    <!--  YAM-P-208  egonzalez 17/08/2015 Se agregó una validación para comprobar que existe al menos una factura declarada y no generar problemas en el paso siguiente del flujo -->
    <!--  YAM-P-208  egonzalez 17/08/2015 Se agregó a la validación de busqueda de factura un disable para el botón de "Procesar" ya que permitía enviár la petición más de una vez -->
    <%--  BBV-P-412  RQ D: AVH: 2016-10-13: Se agregan campos en la ventana Facturacion--%>
    <%--  BBV-P-423 RQCONYFOR-01: AVH: 02/01/2017 Se agregan Columnas--%>
    <%--  BBV-P-423 RQCONYFOR-07: AVH: 18/01/2017 SE MODIFICAN CAMPOS OBLIGATORIOS--%>
    <%--  BUG-PD-16 MAPH 03/03/2017        
            + Cambio de título sección Factura Intangibles
            + Cambio de orden y formato en el encabezado del detalle
            + Eliminación del evento que replica el número de factura en los demás textbox de números de facturas
            + Corrección de Validación del Número de serie a 17 caracteres
            + Corrección en Tipo de carrocería, ahora se muestra como obligatorio, adicionalmente se muestran solo las opciones 1 y 2.
            + El campo País solo muestra la opción México, ya se muestra como obligatorio
            + Corrección de formatos de fecha en Fecha de Factura y Fecha Firma de Contrato desde aaaa-mm-dd hacia dd/mm/aaaa
            + Adición de propiedad title para mostrar info en la alerta.
            + Adición de funcion ReemplazaAcentos(event, this.id, this.value) para quitar acentos en Descripción de las secciones Accesorios y Factura Intangibles (Garantia Extendida, Servicios).
        07/03/2017
            + Modificación para deshabilitar el input de suma de facturas
            + Validación de facturas necesarioas para procesamiento: Vehículo y por apoyo a la comercialización
            + Modificación para ocultar el Div de Cancelación
            + Homologación de encabezado
    --%>
    <%--BUG-PD-17 JRHM 16/03/17 Correccion a la pagina de alta factura en funciones javascript.--%>
    <%--BUG-PD-20 MAPH 23/03/2017 Correcciones de los siguientes puntos:
            + Permitir letras en el campo Número de Factura
            + Mensaje 'No existe factura de apoyo a la comercialización' aparece doble
            + Ocultar campo Registro Vehicular
            + Permitir colocar espacios para el campo "Número de Motor".
            + Cambiar leyenda del campo "Importe" a "Precio Vehículo", adicional traer el valor de la cotización a dicho campo.
            + Cuando las comisiones por Agencia y Vendedor sean cero no deberá solicitar agregar la factura de Comisiones.
            + Para el campo "Descripción Auto", no se alcanza a visualizar todo lo descrito en dicho campo.
            + Sólo permitir una factura en cada sección
            + Si los valores de la sección Comisión de Agencia y comisión de Vendedor son cero no validar factura obligatoria en esa sección
            + Cargar valor del importe de vehículo desde la cotización y ponerlo sólo informativo
    --%>
    <%--BUG-PD-23 MAPH 30/03/2017 Corrección de los siguientes puntos:
            + Validaciones de facturas de vehículo y apoyo a la comercialzación
            + Visibilidad del botón procesar.
    --%>
    <%-- BUG-PD-26 MAPH 30/03/2017 Cambio del campo ¿A quien se paga? por Nombre del Vendedor --%>
    <%-- BUG-PD-27 MAPH 17/04/2017 Cambio de caracteres a alfanuméricos para las tres últimas secciones de facturas en su número de factura y máximo de 15 caracteres--%>
    <%-- BUG-PD-44:MPUESTO:10/05/2017:Ajustes en validaciones por cambio de especificación:
            + Número de serie: 17 caracteres necesarios
            + Número de Motor: Mínimo 2 caracteres y no permitir espacios. Máximo 17 caracteres
            + Número de cilindros: Maxlength a 2 mínimo 1
            + Número de puertas: 1 carácter necesario
            + Fecha firma de contrato a partir del día actual
            + Número de factura máximo 17 caracteres mínimo 1--%>
    <%--BUG-PD-59:MPUESTO:22/05/2017:Recuperación del importe del vehículo después de agregar una factura en su sección correspondiente.--%>
    <%--BUG-PD-69:MPUESTO:22/05/2017:Corrección de validación en sección de factura de apoyo a la comercialización, procesos asíncronos de Mozilla llevados a lineal.--%>
    <%--BUG-PD-78:MPUESTO:07/06/2017:Fecha de firma de contrato delimitada al día actual o 2 días más como máximo--%>
    <%--BUG-PD-95: CGARCIA: 15/06/2017: Se agrego funcion para todos los inputs sobre no recivir un espacio inicial o dos espacios seguidos--%>
	<%--BUG-PD-99:MPUESTO:15/06/2017:CORRECCION PARA MOSTRAR O ELIMINAR LOS BOTONES DE ELIMINAR EN LAS TABLAS DE FACTURAS--%>
    <%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
	<%--BUG-PD-136:MPUESTO:03/07/2017:CORRECCION PARA EJECUTAR ACCION DE PROCESAR CON SOLICITUD NO PROCESABLE SIN VALIDACIONES EN FACTURAS--%>
    <%--BUG-PD-266: CGARCIA: 08/11/2017: SE CAMBIA LA FECHA FIRMA CONTRATO POR LA FERCHA DE COTIZACION--%>
    <%--BUG-PD-341: JMENDIETA: 16/01/2018: Para el td que contiene Fecha Firma de Contrato se le agrego el runat igual a server y el id lblFirmaContrato--%>
    <%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>


    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            fnCargaTablasInicio();
            var settingsDate = {
                // BUG-PD-16 MAPH 03/03/2017 Cambio de formatos de fecha en Fecha de Factura * y Fecha Firma de Contrato desde aaaa-mm-dd hacia dd/mm/aaaa
                dateFormat: "dd/mm/yy",
                showAnim: "slide",
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                selectOtherMonths: true,
                autoSize: true

            };
            var settingsDateFeFirma = {
                dateFormat: "dd/mm/yy",
                showAnim: "slide",
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                selectOtherMonths: true,
                autoSize: true,
                yearRange: "-99:+0",
                maxDate: "+0m +2d",
                minDate: "+0m +0d"
            };

            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $('#feFactura').datepicker(settingsDate).attr('readonly', 'true').attr('onkeydown', 'return false');
            $('#<%= feFirma.ClientID %>').datepicker(settingsDateFeFirma).attr('readonly', 'true').attr('onkeydown', 'return false');


            $('input').on("change paste keyup dragend", function () {
                var dict = { "á": "a", "é": "e", "í": "i", "ó": "o", "ú": "u", "Á": "A", "É": "E", "Í": "I", "Ó": "O", "Ú": "U" };
                $(this).val($(this).val().toString().replace(/[^\w ]/g, function (char) { return dict[char] || char; }));

                var texto = $("#" + $(this).attr('id').toString()).val();
                var tam = texto.length;

                if (tam == 1) { if (texto == " ") { $("#" + $(this).attr('id').toString()).val(""); } }
                else if (tam > 1) {
                    var penultimo = texto.substring(tam - 2, tam - 1);
                    var ultimo = texto.substring(tam - 1);
                    var newTEXT = texto.slice(0, -1);
                    if ((ultimo == " ") && (penultimo == " ")) { $("#" + $(this).attr('id').toString()).val(newTEXT.toString()); }
                }

            });

            deshabilitaComisiones();

            $('#<%=desAuto.ClientID %>').attr('title', $('#<%=desAuto.ClientID %>').val());

            //$("#feFirma").datepicker(
            //    {
            //        dateForma: 'dd/mm/yy',
            //        fistDay: 1,
            //    }).datepicker("setDate", new Date());
            
                                            
        })



        function deshabilitaComisiones() {
            var comisionVendedor = document.getElementById('<%= ComAgencia.ClientID()%>');
            var comisionAgencia = document.getElementById('<%= ComVendedor.ClientID()%>');
            comisionVendedor.disabled = (comisionVendedor.value > 0) ? '' : 'disabled';
            comisionAgencia.disabled = (comisionAgencia.value > 0) ? '' : 'disabled';


        }

        function fnActualizar(Tabla, solicitud, idFactura) {
            btnInsUpdMultiTab("exec SP_BorraTablaFacturas '" + Tabla + "'," + solicitud + ",  '" + idFactura + "'; ", "#tbFacturaVehiculo, #tbFacturaAccesorios, #tbFacturaIntangibles, #tbFacturaComer", "hdnIdFolio", "SumaMontos", "");
        }



        var GlobalIdButton;
        function fnAgregar(id) {
            GlobalIdButton = id;
            var depende = '';
            var hasError = false;
            var textResult = 'Información de la solicitud:';
            var textResultArray = ['', '', '', '', '', '', '', '', '', '', '', '', '', ''];

            switch (id) {
                case 'btnAgregar':
                    factV = $(".txt3BBVA");
                    factV2 = $(".select2BBVA");
                    tabla = 'tbFacturaVehiculo';
                    textResult += '\n\nSección Factura Vehículo:';
                    break;
                case 'btnAgregarAccesorios':
                    factV = $(".accesorio");
                    tabla = "tbFacturaAccesorios";
                    textResult += '\n\nSección Accesorios:';
                    break;
                case 'btnAgregarInt':
                    factV = $(".intangible");
                    tabla = "tbFacturaIntangibles";
                    textResult += '\n\nSección Factura Intangibles(Garantía Extendida):';
                    break;
                case 'btnAgregarComer':
                    factV = $(".comer");
                    tabla = "tbFacturaComer";
                    textResult += '\n\nSección Factura Apoyo a la Comercialización (Comisiones):';
                    break;
            }

            if (document.getElementById(tabla).tBodies[0].rows.length > 1) {
                hasError = true;
                textResult += '\n + Ya cuenta con una factura en esta sección.';
                alert(textResult);
                return;
            }

            $.each(factV, function () {
                if ($(this).attr('extraProperty') != "") {
                    if ($.trim($(this).val()) === "") {
                        //BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.
                        var position = $(this).attr('extraProperty').split('||');
                        textResultArray[position[position.length - 1]] = "\n + Falta información en el campo " + $(this).attr('extraProperty').split('||')[0];
                        hasError = true;
                    }
                    else if ($(this)[0].id == 'txtSerie' && $(this).val().length != 17) {
                        var position = $(this).attr('extraProperty').split('||');
                        textResultArray[position[position.length - 1]] = '\n + El número de serie debe tener 17 caracteres';
                        hasError = true;
                    }
                    else if ($(this)[0].id == 'txtMotor' && $.trim($(this).val()).length < 2) {
                        var position = $(this).attr('extraProperty').split('||');
                        textResultArray[position[position.length - 1]] = '\n + El número de motor debe tener mínimo 2 caracteres';
                        hasError = true;
                    }
                    else {
                        depende += $(this)[0].id + ',';
                    }
                }
            });

            if (id == 'btnAgregar') {
                //if (document.getElementById('feFirma').value != '') {
                //    depende += 'feFirma,';
                //}
                $.each(factV2, function () {
                    if ($(this).attr('extraProperty') != "" && $(this).val() <= 0) {
                        var position = $(this).attr('extraProperty').split('||');
                        textResultArray[position[position.length - 1]] = "\n + Debe seleccionar un elemento en " + $(this).attr('extraProperty').split('||')[0];
                        hasError = true;
                    }
                    else {
                        depende += $(this)[0].id + ',';
                    }
                });
            }

            //var n = str.indexOf("e", factV.length());
            //depende = depende.substring(0, n) + ')';

            depende = depende.replace(/,([^,]*)$/, '$1').replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            depende = depende.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            depende = depende.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            depende = depende.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            depende = depende.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            depende = depende.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            //BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.

            if (!hasError) {
                //$("#txtFacturaInt").val("'" + $("#txtFacturaInt").val() + "'");
                //$("#txtFacturaAccesorios").val("'" + $("#txtFacturaAccesorios").val() + "'");
                var feFacturaDate = document.getElementById("feFactura").value;
                var feFirmaDate = document.getElementById("<%= feFirma.ClientID %>").value;
                //fillgv(tabla, 'hdnIdFolio, ' + depende);

                document.getElementById("feFactura").value = feFacturaDate.split('/')[2] + '/' + feFacturaDate.split('/')[1] + '/' + feFacturaDate.split('/')[0];
                document.getElementById("<%= feFirma.ClientID %>").value = feFirmaDate.split('/')[2] + '/' + feFirmaDate.split('/')[1] + '/' + feFirmaDate.split('/')[0];
                fillUpload(tabla, 'hdnIdFolio,' + depende, 'SumaMontos', '');
                document.getElementById("feFactura").value = feFacturaDate;
                document.getElementById("<%= feFirma.ClientID %>").value = feFirmaDate;
                $.each(factV, function () {
                    if ($(this)[0].id != '<%= desAuto.ClientID()%>'
                                && $(this)[0].id != '<%= qPaga.ClientID()%>'
                            && $(this)[0].id != '<%= CueVendedor.ClientID()%>'
                            && $(this)[0].id != '<%= ComVendedor.ClientID()%>'
                            && $(this)[0].id != '<%= ComAgencia.ClientID()%>'
                            && $(this)[0].id != 'txtFacturaComer') {
                        $(this).val("");
                    }
                });
                if (id == 'btnAgregar') {
                    $.each(factV2, function () {
                        $(this).val(0);
                    });
                }
            }
            else {
                var index = 0;
                for (index = 0; index < textResultArray.length; index++) {
                    textResult += textResultArray[index];
                }
                alert(textResult);
            }
        }

        //BUG-PD-26 MAPH 06/04/2017 Inclusión de función para borrar inputs y seleccionar valor por defecto en DropDownLists
        function fnLimpiaSeccion() {
            $.each(factV, function () {
                if ($(this)[0].id != '<%= desAuto.ClientID()%>'
                            && $(this)[0].id != '<%= txtImporte.ClientID()%>'
                            && $(this)[0].id != '<%= qPaga.ClientID()%>'
                            && $(this)[0].id != '<%= CueVendedor.ClientID()%>'
                            && $(this)[0].id != '<%= ComVendedor.ClientID()%>'
                            && $(this)[0].id != '<%= ComAgencia.ClientID()%>'
                            && $(this)[0].id != 'txtFacturaComer') {
                    $(this).val("");
                }
            });
            if (GlobalIdButton == 'btnAgregar') {
                $.each(factV2, function () {
                    $(this).val(0);
                });
                //BUG-PD-59:MPUESTO:22/05/2017:Recuperación del importe del vehículo después de agregar una factura en su sección correspondiente.
                location.reload(true);
            }
            location.reload(true);
        }

        function fnCargaTablasInicio() {

            var dependeV = '';
            var dependeA = '';
            var dependeI = '';
            var dependeC = '';

            factV = $(".vehiculo");
            factA = $(".accesorio");
            factI = $(".intangible");
            factC = $(".comer");

            $.each(factV, function () {
                dependeV += $(this)[0].id + ', ';
            })
            $.each(factA, function () {
                dependeA += $(this)[0].id + ', ';
            })
            $.each(factI, function () {
                dependeI += $(this)[0].id + ', ';
            })
            $.each(factC, function () {
                dependeC += $(this)[0].id + ', ';
            })

            //BUG-PD-99:MPUESTO:15/06/2017
            var enableScreen = get('Enable') || "0";
            fillMultiTab("#tbFacturaVehiculo, #tbFacturaAccesorios, #tbFacturaIntangibles, #tbFacturaComer", "hdnIdFolio", undefined, undefined, enableScreen);

        }


        function get(name) {
            if (name = (new RegExp('[?&]' + encodeURIComponent(name) + '=([^&]*)')).exec(location.search))
                return decodeURIComponent(name[1]);
        }


        function fnFactura(valor) {
            $('[id^=txtFactura]').val(valor);
        }

        //BUG-PD-16 MAPH 07/03/2017 Modificación para ocultar el Div de Cancelación
        function cambiaVisibilidadDiv(id, visible) {
            div = document.getElementById(id);
            if (visible) {
                div.style.display = '';
            }
            else {
                div.style.display = 'none'
            }
        }


        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();
        }

        //BUG-PD-16 MAPH 07/03/2017 Modificación para ocultar el Div de Cancelación
        function btnGuardar(action) {
            var f = $('[id$=hdnIdFolio]').val();
            var u = $('[id$=hdnUsua]').val();
            //BUG-PD-16 MAPH 08/03/2016 Modificación para mostrar mensaje exitoso al guardar o cancelar registros
            var cadena = 'UPDATE PDK_OPE_SOLICITUD SET PDK_OPE_MODIF = GETDATE() WHERE PDK_ID_SOLICITUD = ' + f;
            var cadenaUp = '';
            var pantalla = $('[id$=hdNomPantalla]').val();


            if (action == "Autoriza") {
                cambiaVisibilidadDiv('divautoriza', false);
                var txtUsu = $('[id$=txtUsuario]').val();
                var txtpsswor = $('[id$=txtPassw]').val();
                var idpantalla = $("[id$=hdnIdPantalla] ").val();
                var txtmotivoOb = $("[id$=txtmotivo]").val();
                var hasError = false;
                // BUG-PD-16 MAPH 03/03/2017 Resúmen de errores
                var textResult = 'Información de la solicitud:\n'
                //           var cadenastored = 'EXEC sp_ValidacionUsuario ' + '"' + cadenatodo + '",' + idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1'
                $('#ventanaContain').hide();

                // BUG-PD-16 MAPH 03/03/2017 Resúmen de errores
                if (txtUsu == '') {
                    hasError = true;
                    textResult += '\nEl campo usuario esta vacía';
                    return;
                }
                if (txtpsswor == '') {
                    hasError = true;
                    textResult += '\nEl campo del password esta vacía';
                    return;
                }
                if (txtmotivoOb == '') {
                    hasError = true;
                    textResult += '\nEl campo de motivo esta vacía';
                    return;
                }

                if (hasError) {
                    alert(textResult);
                    return;
                }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 46, 1, txtmotivoOb)

                //BUG-PD-16 MAPH 07/03/2017 Modificación para ocultar el Div de Cancelación
            } else if (action == "Cancelar") {
                cambiaVisibilidadDiv('divcancela', false);
                var txtUsu = $('[id$=txtusua]').val();
                var txtpsswor = $('[id$=txtpass]').val();
                var idpantalla = $("[id$=hdnIdPantalla] ").val();
                var txtmotivoOb = $("[id$=TxtmotivoCan]").val();
                $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())
                $('#ventanaContain').hide();
                if (txtUsu == '') { alert('El campo usuario esta vacío'); return; }
                if (txtpsswor == '') { alert('El campo del password esta vacío'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacío'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)
            }
        }

        //BUG-PD-16 MAPH 07/03/2017 Validación de facturas necesarias para procesamiento: Vehículo y por apoyo a la comercialización
        function btnProcesarCliente_click() {
            var textError = "Información del procesamiento:\n\n";
            var hasError = false;
            var count = 0;
            var ComAgenciaValue = $('#<%= ComAgencia.ClientID%> ').val();
            var ComVendedorValue = $('#<%= ComVendedor.ClientID%> ').val();
            //BUG-PD-136:MPUESTO:03/07/2017:TAG INICIO
            var buttonProcesar = document.getElementById('<%= btnProcesar.ClientID%>');
            if ($('#<%= ddlSeleccionTurnar.ClientID%>').val() == "-3") {
                $('#<%=btnProcesarCliente.ClientID%>').prop('disabled', true);
                buttonProcesar.click();
            }
            //BUG-PD-136:MPUESTO:03/07/2017:TAG FINAL
            else {

                if (document.getElementById('tbFacturaVehiculo').tBodies[0].rows.length <= 1) {
                    hasError = true;
                    textError += " + No existe factura de vehículo\n";
                }
                //BUG-PD-23 MAPH 30/03/2017 Corrección de validaciones de facturas de vehículo y apoyo a la comercialzación
                if (document.getElementById('tbFacturaComer').tBodies[0].rows.length <= 1 && ((ComAgenciaValue != null && ComAgenciaValue > 0) || (ComVendedorValue != null && ComVendedorValue > 0))) {
                    hasError = true;
                    textError += " + No existe factura de apoyo a la comercialización\n";
                }
                if (hasError) {
                    alert(textError);
                }
                else {
                    //document.getElementById('btnProcesarCliente').disabled = 'disabled';
                    //BUG-PD-23 MAPH 30/03/2017 Corrección de visibilidad del botón procesar.
                    $('#<%=btnProcesarCliente.ClientID%>').prop('disabled', true);                  
                    buttonProcesar.click();
                }
            }
        }

        function btnProcesarCliente_Enable() {
            //BUG-PD-23 MAPH 30/03/2017 Corrección de visibilidad del botón procesar.
            $('#<%=btnProcesarCliente.ClientID%>').attr('disabled', false);
            //document.getElementById('btnProcesarCliente').disabled = '';
        }

        function disableElements() {
            $('#btnAgregar').css("display", "none");
            $('#btnAgregarAccesorios').css("display", "none");
            $('#btnAgregarInt').css("display", "none");
            $('#btnAgregarComer').css("display", "none");
            var table = document.getElementById('tbFacturaVehiculo');
        }

        //BUG-PD-69:MPUESTO:22/05/2017:Corrección de validación en sección de factura de apoyo a la comercialización, procesos asíncronos de Mozilla llevados a lineal.
        function btnAgregarComer_click() {
            var tabla = "tbFacturaComer";
            var textError = '';
            if (document.getElementById(tabla).tBodies[0].rows.length > 1) {
                textError += '\n + Ya cuenta con una factura en esta sección.';
                alert(textError);
                return;
            }
            textError = validaComisiones("Agencia");
            textError += validaComisiones("Vendedor");
            if (textError.length == 0) {
                fnAgregar('btnAgregarComer');
            }
            else {
                alert(textError);
            }
        }
        //BUG-PD-69:MPUESTO:22/05/2017:Corrección de validación en sección de factura de apoyo a la comercialización, procesos asíncronos de Mozilla llevados a lineal.
        function validaComisiones(id) {
            var comision = (id == 'Agencia') ? document.getElementById('<%= ComAgencia.ClientID()%>') : document.getElementById('<%= ComVendedor.ClientID()%>');
            var textError = "Información del cambio de valor en " + $(comision).attr('extraProperty').split('||')[0] + " :";
            var hidden = (id == 'Agencia') ? document.getElementById('<%= hdnComAgencia.ClientID()%>') : document.getElementById('<%= hdnComVendedor.ClientID()%>');
            if (comision.value > hidden.value) {
                comision.value = hidden.value;
                textError += "\n\n + El valor no puede ser mayor a " + hidden.value.toString() + "\n\n";
            }
            else {
                textError = '';
            }
            return textError;
        }


        $(document).ready(function () { $.urlParam = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } }; if ($.urlParam("Enable").toString() == "1") { $("#btnCancelarNew").hide(); }; });

    </script>

    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnCancelar {
            display: none;
        }
    </style>

    <%-- BUG-PD-16 08/03/2017 MAPH Homologación de encabezado--%>
    <%--<div class="divAdminCat ">--%>
    <%--<div class="divFiltrosConsul">--%>
    <%--<table class="tabFiltrosConsul">--%>
    <div class="divPantConsul">
        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width: 70%;">
                                    <legend>Pre-Formalización.
                                    </legend>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divAdminCatCuerpo ">
            <div style="width: 100%; height: 100%; overflow: auto;">
                <%-- BUG-PD-16 MAPH 03/03/2017 Cambio de orden y formato en el encabezado del detalle, invierten orden en Solicitudes --%>
                <table width="100%" class="fieldsetBBVA">
                    <tr>
                        <th class="campos" style="width: 25%">Solicitud:                    
                        </th>
                        <th class="campos" style="width: 25%">
                            <asp:Label ID="lblSolicitud" Font-Underline="true" runat="server">
                            </asp:Label>
                        </th>
                        <th class="campos" style="width: 25%">Cliente:                    
                        </th>
                        <th class="campos" style="width: 25%">
                            <asp:Label ID="lblCliente" Font-Underline="true" runat="server">
                            </asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <th class="campos">Status Credito:                    
                        </th>
                        <th class="campos">
                            <asp:Label ID="lblStCredito" Font-Underline="true" runat="server">
                            </asp:Label>
                        </th>
                        <th class="campos">Status Documentos:                    
                        </th>
                        <th class="campos">
                            <asp:Label ID="lblStDocumento" Font-Underline="true" runat="server">
                            </asp:Label>
                        </th>
                    </tr>
                </table>
                <table width="98%" id="tbFacturacion">
                    <tr>
                        <td colspan="4" style="font-size: larger">Datos de las Facturas.
                        </td>
                    </tr>
                    <tr>
                        <th colspan="4">Factura Vehiculo
                        </th>
                    </tr>
                    <tr>
                        <td>Número de Factura *
                        </td>
                        <td>
                            <%-- BUG-PD-16: : MAPH: Eliminación del evento que replica el número de factura en los demás textbox de números de facturas --%>
                            <%--<input type = "text" id = "txtFactura" onkeyup = "fnFactura(this.value)"  Class="txt3BBVA" Onkeypress="return ValCarac(event,7);" />--%>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <%-- BUG-PD-20 Omisión de Tooltips.--%>
                            <%-- BUG-PD-27 MAPH 17/04/2017 Máximo de 15 caracteres --%>
                            <input type="text" id="txtFactura" class="txt3BBVA" onkeypress="return ValCarac(event,16);" extraproperty="Número de Factura||0" maxlength="15" />
                        </td>
                        <td>Número de Serie *
                        </td>

                        <%-- BUG-PD-16  MAPH 03/03/2017 Referencia a función de validación del Número de Serie a 17 caracteres--%>
                        <%-- BUG-PD-16 MAPH 03/03/2017 Adición de propiedad extraProperty para mostrar info en la alerta.--%>
                        <%-- BUG-PD-16 MAPH 08/03/2017 Establecimiento de máximo de carcteres a 17.--%>
                        <td>
                            <input type="text" id="txtSerie" class="txt3BBVA" onkeypress="return ValCarac(event,22);" extraproperty="Número de Serie||1" maxlength="17"/>
                        </td>
                    </tr>
                    <tr>
                        <%--            <td   >
                Linea
            </td>
            <td   >
                <input type = "text" id = "txtLinea" Class="txt3BBVA"/>
            </td>--%>
                        <td>Motor *
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <%-- BUG-PD-20 MAPH 23/03/2017 Adición de propiedad extraProperty para mostrar info en la alerta.--%>
                            <input type="text" id="txtMotor" class="txt3BBVA" onkeypress="return ValCarac(event,16);" extraproperty="Motor||2" maxlength="17" />
                        </td>
                        <td>Precio Vehículo *
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <input type="text" runat="server" id="txtImporte" onkeypress="return checkDecimals(event,this.value,10)" disabled="disabled" class="txt3BBVA" extraproperty="Importe||3" />
                        </td>
                    </tr>
                    <tr>
                        <td>Descripción Auto *
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <input type="text" id="desAuto" onkeypress="return ValCarac(event,16);" disabled="disabled" class="txt3BBVA" runat="server" extraproperty="Descripción Auto||4" />
                        </td>
                        <%-- BUG-PD-16  MAPH 03/03/2017 Corrección en Tipo de carrocería, ahora se muestra como obligatorio.--%>
                        <td>Tipo de Carrocería *
                        </td>
                        <td>
                            <%--<input type="text" id="tipoCarr" onkeypress="ManejaCar('D',0,this.value,this)"   Class="txt3BBVA" />--%>
                            <asp:DropDownList runat="server" ID="ddlTipoCarroceria" Class="select2BBVA" Width="160px" extraProperty="Tipo de Carrocería||5">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Número de Cilindros *
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <input type="text" id="noCilindros" onkeypress="return ValCarac(event,7);" class="txt3BBVA" extraproperty="Número de Cilindros||6" maxlength="2" />
                        </td>
                        <td>Número de Puertas *
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <input type="text" id="noPuertas" onkeypress="return ValCarac(event,7);" class="txt3BBVA" extraproperty="Número de Puertas||7" maxlength="1" />
                        </td>
                    </tr>
                    <tr>
                        <td>Transmisión *
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlTransmision" Class="select2BBVA" extraProperty="Transmisión||8"></asp:DropDownList>
                        </td>
                        <td>País de Origen *
                        </td>
                        <td>
                            <%--<input type="text" id="noPais" onkeypress="ManejaCar('D',0,this.value,this)"   Class="txt3BBVA" />--%>

                            <asp:DropDownList runat="server" ID="ddlNumPais" Class="select2BBVA" extraProperty="País de Origen||9">
                                <%--                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <%--<td>Registro Vehicular *
                        </td>
                        <td>--%>
                        <%-- BUG-PD-16 Adición de propiedad extraProperty para mostrar info en la alerta.--%>
                        <%--<input type="text" id="regVehicular" maxlength="20" onkeypress="return ValCarac(event,7);" class="txt3BBVA" extraProperty="Registro Vehicular||10"/>
                        </td>--%>
                        <td>Fecha de Factura *
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <input type="text" id="feFactura" class="txt3BBVA" extraproperty="Fecha de Factura||11" />
                        </td>
                        <%--</tr>
                    <tr>--%>

                        <td runat="server" id="lblFirmaContrato">Fecha Firma de Contrato *
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <input type="text" id="feFirma" runat="server" disabled="disabled" class="txt3BBVA" extraproperty="Fecha Firma de Contrato||12" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table id="tbFacturaVehiculo" class="resulGrid">
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right;">
                            <input type="button" id="btnAgregar" value="Agregar" onclick="fnAgregar(this.id);" class="buttonSecBBVA2" />
                        </td>
                    </tr>
                    <tr>
                        <th colspan="4">Accesorios
                        </th>
                    </tr>
                    <tr>
                        <td>No.Factura
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <%-- BUG-PD-27 MAPH 17/04/2017 Cambio de caracteres a alfanuméricos para las tres últimas secciones de facturas en su número de factura y máximo de 15 caracteres --%>
                            <input type="text" id="txtFacturaAccesorios" class="accesorio" onkeypress="return ValCarac(event,16);" extraproperty="Número de Factura||0" maxlength="15" />
                        </td>
                        <td colspan="2" rowspan="3">
                            <table id="tbFacturaAccesorios" class="resulGrid">
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>Descripción
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <%-- BUG-PD-16 Adición de funcion ReemplazaAcentos(event, this.id, this.value) para quitar acentos.--%>
                            <input type="text" id="txtDescripcionAccesorios" class="accesorio" onkeypress="return ValCarac(event,16);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" extraproperty="Descripción||1" />
                        </td>
                    </tr>
                    <tr>
                        <td>Importe
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <input type="text" id="txtImporteAccesorios" onkeypress="return checkDecimals(event, this.value, 12)" class="accesorio" extraproperty="Importe||2" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right;">
                            <input type="button" id="btnAgregarAccesorios" value="Agregar" onclick="fnAgregar(this.id);" class="buttonSecBBVA2" />
                        </td>
                    </tr>
                    <tr>
                        <th colspan="4">
                            <%--BUG-PD-16 MAPH 03/03/2017 Modificación del título--%>
                            <%--Factura Intangibles(Garantia Extendida, Servicios)--%>
                Factura Intangibles(Garantía Extendida)
                        </th>
                    </tr>
                    <tr>
                        <td>No.Factura
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <%-- BUG-PD-27 MAPH 17/04/2017 Cambio de caracteres a alfanuméricos para las tres últimas secciones de facturas en su número de factura y máximo de 15 caracteres --%>
                            <input type="text" id="txtFacturaInt" class="intangible" onkeypress="return ValCarac(event,16);" extraproperty="Número de Factura||0" maxlength="15" />
                        </td>
                        <td colspan="2" rowspan="3">
                            <table id="tbFacturaIntangibles" class="resulGrid">
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>Descripción
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <%-- BUG-PD-16 Adición de funcion ReemplazaAcentos(event, this.id, this.value) para quitar acentos.--%>
                            <input type="text" id="txtDescripcionInt" class="intangible" onkeypress="return ValCarac(event,16);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" extraproperty="Descripción||1" />
                        </td>
                    </tr>
                    <tr>
                        <td>Importe
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <input type="text" id="ImporteInt" onkeypress="return checkDecimals(event, this.value, 12)" class="intangible" extraproperty="Importe||2" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right;">
                            <input type="button" id="btnAgregarInt" value="Agregar" onclick="fnAgregar(this.id);" class="buttonSecBBVA2" />
                        </td>
                    </tr>
                    <tr>
                        <%-- BUG-PD-16 Cambio de título desde Factura Apoyo a la Comercialización a Sección Factura Apoyo a la Comercialización (Comisiones)--%>
                        <th colspan="4">Factura Apoyo a la Comercialización (Comisiones)
                        </th>
                    </tr>
                    <tr>
                        <td style="visibility: collapse">No.Factura
                        </td>
                        <td style="visibility: collapse">
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <%-- BUG-PD-27 MAPH 17/04/2017 Cambio de caracteres a alfanuméricos para las tres últimas secciones de facturas en su número de factura y máximo de 15 caracteres --%>
                            <input type="text" value="1" id="txtFacturaComer" class="comer" onkeypress="return ValCarac(event,16);" extraproperty="Número de Factura||0" maxlength="15" />
                        </td>
                        <td colspan="2" rowspan="3">
                            <table id="tbFacturaComer" class="resulGrid">
                            </table>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>Descripción
                        </td>
                        <td>
                            <%--class = "comer"--%>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <input type="text" id="txtDescComer" onkeypress="return ValCarac(event,16);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" extraproperty="Descripción||1" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>Importe
                        </td>
                        <td>
                            <%--class = "comer"--%>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <input type="text" id="txtImporteComer" onkeypress="return checkDecimals(event, this.value, 12)" extraproperty="Importe||2" />
                        </td>
                    </tr>
                    <tr>
                        <td>% Comisión de Venta del Automóvil Vendedor
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <input runat="server" type="text" id="ComVendedor" onkeypress="return ValCarac(event, 9)" class="comer" onblur="validaComisiones('Vendedor')" extraproperty="% Comisión de Venta del Automóvil Vendedor||3" />
                        </td>
                    </tr>
                    <tr>
                        <td>% Comisión de Venta del Automóvil Agencia
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <input runat="server" type="text" id="ComAgencia" onkeypress="return ValCarac(event, 9)" class="comer" extraproperty="% Comisión de Venta del Automóvil Agencia||4" />
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:Label ID="lblqPaga" runat="server" Text="Nombre del Vendedor"></asp:Label>
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <%-- BUG-PD-16 Campo ¿A quién se le Paga? deshabilitado.--%>
                            <input runat="server" type="text" id="qPaga" maxlength="100" onkeypress="return ValCarac(event,16);" class="comer" disabled="disabled" extraproperty="¿A quién se le Paga?||5" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCueVendedor" runat="server" Text="Cuenta del Vendedor"></asp:Label>
                        </td>
                        <td>
                            <%-- BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.--%>
                            <%-- BUG-PD-16 Campo Cuenta del Vendedor deshabilitado.--%>
                            <input runat="server" type="text" id="CueVendedor" maxlength="100" onkeypress="return ValCarac(event,7);" class="comer" disabled="disabled" extraproperty="Cuenta del Vendedor||6" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right;">
                            <input type="button" id="btnAgregarComer" onclick="btnAgregarComer_click()" value="Agregar" class="buttonSecBBVA2" />
                        </td>
                    </tr>
                    <tr>
                        <th colspan="4">Importe Suma de Facturas
                            <%--BUG-PD-16 MAPH 07/03/2017 Modificación para deshabilitar el input de suma de facturas--%>
                            <input type="text" id="txtSumaFacturas" runat="server" disabled="disabled" />
                        </th>
                    </tr>
                    <%--        <tr>
            <td    colspan = "4" style = "text-align:right;">
                <input type = "button" value = "Guardar" id = "btnGuardarFac" onclick = "fnProcesar('')" class = "botones" />
            </td>
        </tr> --%>
                </table>
            </div>
        </div>

        <div id="divautoriza" style="display: none">
            <%--<cc1:ModalPopupExtender ID="mpoAutorizar" runat="server" PopupControlID="popAutoriza" TargetControlID="btnAutorizar" CancelControlID="btnCancelAutoriza" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>--%>
            <%--<asp:Panel ID="popAutoriza" runat="server" Text="Autorización" CssClass="cajadialogo">--%>
                <div class="tituloConsul">
                    <asp:Label ID="Label4" runat="server" Text="Autorización" />
                </div>
                <table width="100%">
                    <tr>
                        <td class="campos">Usuario:</td>
                        <td>
                            <asp:TextBox ID="txtUsuario" SkinID="txtGeneral" MaxLength="12" runat="server" Style="width: 120px !important;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="campos">Password:</td>
                        <td>
                            <asp:TextBox ID="txtPassw" runat="server" SkinID="txtGeneral" MaxLength="12" TextMode="Password" EnableTheming="true" Style="width: 120px !important;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="campos">Descripción:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea id="txtmotivo" runat="server" onkeypress="ManejaCar('A',1,this.value,this)" class="Text" rows="5" cols="1" style="width: 95% !important;"></textarea>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td>
                            <asp:HiddenField runat="server" ID="hdnidAutoriza" />
                        </td>
                        <td align="left" valign="middle">
                            <%--BUG-PD-16 MAPH 07/03/2017 Modificación para ocultar el Div de Cancelación--%>
                            <input id="btnGuardarAutoriza" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar('Autoriza')" />
                            <asp:Button ID="btnCancelAutoriza" runat="server" Text="Cancelar" SkinID="btnGeneral" />
                        </td>
                    </tr>
                </table>
            <%--</asp:Panel>--%>
        </div>
        <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top:15%; left:31%; width: 220px;">
            <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID="btnCancelar" PopupControlID="popCancela" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>--%>
            <%--<asp:Panel ID="popCancela" runat="server" CssClass="cajadialogo ">--%>
                <div class="tituloConsul">
                    <asp:Label ID="Label1" runat="server" Text="Cancelación" />
                </div>
                <table width="100%">
                    <tr>
                        <td class="campos">Usuario:</td>
                        <td>
                            <asp:TextBox ID="txtusua" SkinID="txtGeneral" MaxLength="12" runat="server" Style="width: 120px !important;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="campos">Password:</td>
                        <td>
                            <asp:TextBox ID="txtpass" runat="server" SkinID="txtGeneral" MaxLength="12" TextMode="Password" EnableTheming="true" Style="width: 120px !important;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="campos">Descripción:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                           <%-- <textarea id="TxtmotivoCan" runat="server" onkeypress="ManejaCar('A',1,this.value,this)" class="Text" rows="5" cols="1" style="width: 95% !important;"></textarea>--%>
                             <textarea id="TxtmotivoCan" runat="server"  class="Text" rows="5" cols="1" style="width: 95% !important;"></textarea>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td>
                            <asp:HiddenField runat="server" ID="HiddenField1" />
                        </td>
                        <td align="left" valign="middle">
                            <%--BUG-PD-16 MAPH 07/03/2017 Modificación para ocultar el Div de Cancelación--%>
                            <input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar('Cancelar')" />
                            <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral" />
                        </td>
                    </tr>

                </table>

            <%--</asp:Panel>--%>
        </div>
        <div class="resulbbvaCenter divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td valign="middle">
                        <%--BUG-PD-78:MPUESTO:07/06/2017:ADICION DE DROPDOWNLIST PARA PROCESO DE TURNAR--%>
                        <asp:Label runat="server" ID="lblSeleccionTurnar" Text="Elija una opción: "></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlSeleccionTurnar"></asp:DropDownList>
                        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" />
                        <%--<input type="button" class="Text" value="Regresar" onclick="fnRegresar();"  />--%>
                        <%--<input id="btnGuardarFac" runat="server" type = "button" class="buttonBBVA2" value = "Procesar" onclick = "if ($('table#tbFacturaVehiculo tbody').find('tr').length > 1) { $(this).prop('disabled', true); fnProcesar(''); } else { alert('Es necesario agregar una factura para continuar'); } jsonValidaWS()"/>--%>

                        <%-- BUG-PD-16 MAPH 07/03/2017 Validación de facturas necesarioas para procesamiento: Vehículo y por apoyo a la comercialización --%>
                        <input type="button" runat="server" id="btnProcesarCliente" value="Procesar" onclick="btnProcesarCliente_click();" class="buttonBBVA2" />
                        <asp:Button runat="server" ID="btnAutorizar" Visible="false" Text="Autorizar" OnClientClick="cambiaVisibilidadDiv('divautoriza', true)" />
                        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="buttonSecBBVA2" OnClientClick="cambiaVisibilidadDiv('divcancela', true)" />
                        <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv()" id="btnCancelarNew" />
                    </td>
                </tr>
            </table>
            <%-- BUG-PD-16 MAPH 07/03/2017 Validación de facturas necesarioas para procesamiento: Vehículo y por apoyo a la comercialización --%>
            <div id="divHiddenButton" style="visibility: collapse">
                <asp:Button runat="server" ID="btnProcesar" Text="Procesar" OnClick="btnProcesar_Click" />
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdnIdFolio" runat="server" />
    <asp:HiddenField ID="hdnIdPantalla" runat="server" />
    <asp:HiddenField ID="hdnUsua" runat="server" />
    <asp:HiddenField ID="hdnResultado" runat="server" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
    <asp:HiddenField ID="hdNomPantalla" runat="server" />
    <asp:HiddenField ID="hdnEnable" runat="server" />
    <asp:HiddenField ID="hdnMensualidad" runat="server" />
    <asp:HiddenField ID="hdnPlazo" runat="server" />
    <asp:HiddenField ID="hdnResultado1" runat="server" />
    <asp:HiddenField ID="hdnResultado2" runat="server" />
    <asp:HiddenField ID="hdnComAgencia" runat="server" />
    <asp:HiddenField ID="hdnComVendedor" runat="server" />
</asp:Content>
