<%@ Page Language="VB" MasterPageFile ="~/aspx/Home.Master" AutoEventWireup="false" CodeFile="Blanco.aspx.vb" Inherits="aspx_Blanco" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>



<asp:Content ID="content1" ContentPlaceHolderID="cphPantallas" runat="server" >

    <!--  YAM-P-208  egonzalez 07/08/2015 Se agregó una función para la validación de los teléfonos -->
    <!--  YAM-P-208  egonzalez 12/08/2015 Se agregó una función para la limpieza de las tablas que no contenían campos -->
    <!--                                  Se editó el formato de los nombres de las secciones en el área de solicitud -->
    <!--  YAM-P-208  egonzalez 01/09/2015   Se agregó una función para habilitar los campos de una solicitud siempre y cuando se encuentre en la tarea "solicitud solicitante" con estatus activo. -->
    <!--                                    Se implementó el uso de una nueva función para obtener la tarea actual de la solicitud y se almacena en el campo oculto "hdnTareaActual". -->
    <!--  YAM-P-208  egonzalez 10/08/2015 Se bloquearon las 3 primeras condiciones en la página de dictamen -->
    <!--  YAM-P-208  egonzalez 02/10/2015 Se agregó una función para deshabilitar campos (rfc, homoclave) -->
    <!--  YAM-P-208  egonzalez 29/10/2015 Se agregó el campo de apellido paterno a los excluidos como obligatorios y en su lugar se hizo una validación que revise que exista uno de los 2 apellidos forzosamente -->
    <!--  YAM-P-208  egonzalez 29/12/2015 Se agregó a la validación de teléfonos, un punto extra que es no permitir repetir teléfonos -->
	<!--  BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75) -->
    <!--  BBV-P-423  gvargas   15/12/2016 Se agregaron CALLBACKS ajax a Web Method para validar y actualziar secciones de la pagina -->
    <!--                                  Se mejoraron vañidaciones telefono, agregados metodos delegados para captura de cambios en los selects-->
    <!--  BUG-PD-03: GVARGAS: 05/01/2017: Correcciones pantalla Precalificacion.-->
    <!--  BUG-PD-04: GVARGAS: 11/01/2017: Correcciones pantalla Precalificacion agregados nuevos campos y validacion campos obligatorios, val Num Cot a BDD Procotiza-->
    <!--  BUG-PD-05: GVARGAS: 21/01/2017: Correcciones pantalla Solicitud de Credito.-->
    <!--  BUG-PD-06: GVARGAS: 26/01/2017: Correcciones pantalla Solicitud de Credito & Precalificacion.-->
    <!--  BUG-PD-08: GVARGAS: 08/02/2017: Validación formato RFC y doble captura RFC.-->
    <!--  BUG-PD-09: GVARGAS: 15/02/2017: Revision general Bugs.-->
    <!--  BUG-PD-10: GVARGAS: 20/02/2017: Enexo BUGS.-->
    <!--  BUG-PD-13  GVARGAS  27/02/2017  Cambio focus doble val.-->
    <!--  BUG-PD-18  GVARGAS  06/03/2017  Cambios PEP & N Identificacion.-->
    <!--  BUG-PD-17 JRHM 16/03/17 Se corrigen errores en pagina de blanco (>:) -->
    <!--  BUG-PD-21  GVARGAS  27/03/2017 Bugs Campos Obligatorios-->
    <!--  BUG-PD-24  GVARGAS  29/03/2017 Bugs Campos Desabilitados en Pre si la SOL se regresa-->
    <!--  BUG-PD-28  GVARGAS  10/04/2017 Cambio valida correo-->
    <!--  BBV-P-423  RQADM-02 GVARGAS  12/04/2017 Actualización de Datos en Solicitud 38,39-->
    <!--  BUG-PD-34  GVARGAS  10/04/2017 Correccion validaciones Generales-->
	<!-- BUG-PD-39  erodriguez 26/04/2017 Cambios de usabilidad y estilos-->
    <!--  BUG-PD-53  GVARGAS 16/05/2017 Cambios Concuctor Recurrente-->
    <!--  BBVA-P-423 RQSOLBCOM-01 GVARGAS 16/04/2017 Cierra ventana al cancelar-->
    <!--  BUG-PD-67  GVARGAS 01/06/2017 Cambios ciudades-->
    <!--  BUG-PD-72  GVARGAS 08/06/2017 Recuperar Ciudad Guardada-->
    <!--  BUG-PD-83  GVARGAS 20/06/2017 Campos Precalificacion-->
    <!--  BUG-PD-31  GVARGAS 20/06/2017 Crear opcion "< SELECCIONAR >"-->
    <!--  BUG-PD-113  GVARGAS 21/06/2017 Cambios Extras-->
    <!--  BUG-PD-115  GVARGAS 23/06/2017 Valores Default-->
    <!--  BUG-PD-120  GVARGAS 26/06/2017 Correcion error pantalla-->	
	<!--  BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar-->
    <!--  BUG-PD-129  GVARGAS 29/06/2017 Correcion PEPs y Ñs-->
    <!--  BBV-P-423 RQACTPRE-01 GVARGAS 03/07/2017 Validar años residencia-->
    <!--  BUG-PD-139  GVARGAS 04/07/2017 Correcion return focus-->
    <!--  BUG-PD-154  GVARGAS 15/07/2017 Correcion Extencion empleo ant-->
    <!--  BUG-PD-180  GVARGAS 07/08/2017 RFC automatico-->
    <!--  BUG-PD-188  GVARGAS 31/08/2017 set Attr to inputs elements -->
    <!--  BBVA-P-423 RQ-MN2-1 GVARGAS 07/09/2017 CI Precalificaciòn -->
    <!--RQADM2-02: CGARCIA: 25/09/2017: IMPLEMENTACION DE ESPACIO EN BLANCOS.-->
    <!--  BBVA-P-423 RQ-PI7-PD6 Quitar Obligatoriedad telefono fijo-->
    <!--  BUG-PD-234 12/10/2017 GVARGAS Cambio urgente Blanco-->
    <!--  BBVA-P-423 RQ-PI7-PD1 GVARGAS 23/10/2017 Mejoras CI Precalificaciòn & Preforma-->
    <!-- BUG-PD-261 EGonzalez 06/11/2017 Se agrega llamado a la validación general al guardar, previniendo la falta de validación en un copy/paste -->
    <!-- BUG-PD-323 DJUAREZ 03/01/2018: Se coloca el RFC en mayusculas al quitar el focus del cuadro de texto de RFC -->
    <!-- BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit-->
    <!-- BUG-PD-393 GVARGAS 12/03/2018 Validar campos obligatorios sin ERROR-->
    <!-- BUG-PD-412 DJUAREZ 05/04/2018 Validar que la antiguedad de trabajo se mayor o igual a 14 años-->
    <!-- BUG-PD-445 GVARGAS 17/05/2018 Add usuario query String-->

       <script language="javascript" type ="text/javascript" >
           //--- INC-B-1922 
           //<!-- YAM-P-208  egonzalez 07/08/2015 Se implementó una función para validación de los teléfonos -->
           // YAM-P-208 egonzalez Se implementó el uso de la variable "timestamp" usada en otro desarrollo para evitar el caché de los navegadores

           //funcion que valida la impresion del documento de buro  
            
           function fnIsEneable() {

               if (QueryString.Enable == 1) {
                   btnProcesar.hide();
                   btnCancelarNew.hide();
//                   btnAutoriza.hide();
                   btnImprimir.show();
//                   fnValidaBuro();
               }
               else {
                   btnProcesar.show();
                   btnCancelarNew.show();
                   //btnAutoriza.show();
                   btnImprimir.hide();
               }
           }

           function fnValidaBuro() {               
               chkBuro.attr('checked', true);

               if (chkBuro.is(":checked")) {
                   btnProcesar.show();
               }
               else {
                   btnProcesar.hide();
               }

               chkBuro.click(function () {
                   if (chkBuro.is(":checked")) {
                       var c = confirm('Se requiere imprimir el contrato de aceptacion de la consulta a buro de credito.');
                       if (c == true) {
                           window.location.href = '../Documentos/AUTORIZACION_DE_INFORMACION_CREDITICIA.docx' + timestamp;
                           btnProcesar.show();
                       }
                       else {
                           chkBuro.attr('checked', false);
                           btnProcesar.hide();
                       }
                   }
                   else { btnProcesar.hide(); }
               });

           }

           function getval(ibj) { }

           function jsonBack(errorLabel, destino, successfully, datos) {
               var settings = { type: "POST", url: "", async: false, data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
               settings.url = destino
               settings.success = successfully
               settings.data = datos
               settings.failure = function (response) { alert("Error al cargar : " + errorLabel.toString()); }
               $.ajax(settings);
           }

           function limiteUpdate(folio) {
               var result = "";
               $.ajax({ type: "POST", url: "Blanco.aspx/limiteUpdate", async: false, contentType: "application/json; charset=utf-8", dataType: "json",
                        data: '{"folio": "' + folio.toString() + '", "tarea": "7"}',
                        success: function (response) { result = response.d; }
               });
               return result;
           }

           function JSONProccess(jsonTable, finalizar) {
               var destino = "Blanco.aspx/Proccess";
               var successfully = OnSuccessProccess;
               var datos = jsonTable;
               jsonBack('No fue posible guardar los cambios', destino, successfully, datos);
           }

           function OnSuccessProccess(response) {
               if (response.d.toString() == "OK") {
                   var folio = $.urlParam("idFolio").toString();
                   nextTarea(folio.toString() + ',64,' + "1");
               } else {
                   var url = window.location.href;
                   PopUpLetreroRedirect(response.d.toString(), url.toString());
               }
           }

           function nextTarea(exec) {
               var destino = "Blanco.aspx/nextTarea";
               var successfully = OnSuccessNextTarea;
               var datos = '{"exec":"' + exec.toString() + '"}';
               jsonBack('No fue posible guardar los cambios', destino, successfully, datos);
           }

           function OnSuccessNextTarea(response) {
               if (response.d.indexOf("Tarea Exitosa.") >= 0) {
                   var item = $.parseJSON(response.d.toString());

                   var msg = item.mensaje.toString();
                   var mostrar = item.mostrar.toString();
                   var id_pantalla = item.id_pantalla.toString();
                   var url = item.link.toString();

                   if (mostrar != "0") { url = "consultaPanelControl.aspx"; }
                   else {
                       var usu = $("#ctl00_ctl00_cphCuerpo_hdidUsuario").val();
                       var sol = $.urlParam("idFolio").toString();
                       url = url + "?idPantalla=" + id_pantalla + "&sol=" + sol + "&Enable=0&usu=" + usu + "&usuario=" + usu;//nuevobug
                   }
                 
                   PopUpLetreroRedirect(msg, "../aspx/" + url);
               } else {
                   PopUpLetreroRedirect(response.d.toString(), "../aspx/consultaPanelControl.aspx");
               }
           }

           function JSONTipoPersona(folio) {
               var destino = "Blanco.aspx/jsonTipoPersona";
               var successfully = OnSuccessTP;
               var datos = '{"folio_id":"' + folio.toString() + '"}';
               jsonBack('Tipo de persona ', destino, successfully, datos);
           }

           function OnSuccessTP(response) {
               if (response.d.toString() == "ERROR") { return; }

               var returnVars = response.d.toString().split('&$&');
               var fech = returnVars[0].split('/');
               var dia = fech[0];
               var mes = fech[1];
               if (dia.length == 1) { fech[0] = '0' + fech[0]; }
               if (mes.length == 1) { fech[1] = '0' + fech[1]; }
              
               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24').val(fech[0] + "/" + fech[1] + "/" + fech[2]).prop("disabled", true);
               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTIPO_PERSONA532').val(returnVars[1]).prop("disabled", true);
           }


           function jsonDestinoUnidad(folio) {
               var destino = "Blanco.aspx/jsonDestinoUnidad";
               var successfully = OnSuccessDU;
               var datos = '{"folio_id":"' + folio.toString() + '"}';
               jsonBack('Destino Unidad', destino, successfully, datos);
           }

           function OnSuccessDU(response) {
               if (response.d.toString() == "1") { $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtDESTINO_UNIDAD529').val(1474).prop("disabled", true); }
               else if (response.d.toString() == "0") { $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtDESTINO_UNIDAD529').val(1473).prop("disabled", true); }
           }

           function jsonHondaAcura(folio) {
               var destino = "Blanco.aspx/jsonHondaAcura";
               var successfully = OnSuccessHA;
               var datos = '{"folio_id":"' + folio.toString() + '"}';
               jsonBack('Conductor Recurrente', destino, successfully, datos);
           }

           function OnSuccessHA(response) {
               var HA = response.d.toString();
               var HAArray = HA.split(";");

               if ((HAArray[0]) != "3") {
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_CONDUCTOR_RECURRENTE").fadeOut();
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_CONDUCTOR_RECURRENTE").fadeOut();
               } else {
                   if (HAArray[1] == "1") { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCONDUCTOR_RECURRENTE576").prop("checked", true); }
                   else { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCONDUCTOR_RECURRENTE576").prop("checked", false); }
               }
           }

           function JSONCompraInteligente(folio) {
               var destino = "Blanco.aspx/jsonCompraInteligente";
               var successfully = OnSuccessCI;
               var datos = '{"folio_id":"' + folio.toString() + '"}';
               jsonBack('Compra Inteligente', destino, successfully, datos);
           }

           function OnSuccessCI(response) {
               if (response.d.toString() == "CI") {
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtTIPO_CREDITO533').val(1479).prop("disabled", true);
               }
               else if (response.d.toString() == "TRADICIONAL") {
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtTIPO_CREDITO533').val(1478).prop("disabled", true);
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_FIEL').fadeOut();
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_FIEL').fadeOut();
               }
           }

           function JSONCities(SelectedValor) {
               var destino = "Blanco.aspx/jsonCiudades";
               var successfully = OnSuccessCities;
               var datos = '{"city_id":"' + SelectedValor.toString() + '"}';
               jsonBack('Ciudades', destino, successfully, datos);
           }

           function JSONGetCity(folio) {
               var destino = "Blanco.aspx/getCity";
               var successfully = OnSuccessCity;
               var datos = '{"folio_id":"' + folio.toString() + '"}';
               jsonBack('Ciudad', destino, successfully, datos);
           }

           function OnSuccessCities(response) {
               var items = $.parseJSON(response.d);
               $.each(items, function (i, item) { $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCIUDAD547').append($('<option>', { value: item.id, text: item.c })); });
           }

           function OnSuccessCity(response) { $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCIUDAD547').append($('<option>', { value: 0, text: response.d.toString() })); }

           function JSONGetCI(folio) {
               var destino = "Blanco.aspx/InfoCI";
               var successfully = OnSuccessCI_;
               var datos = '{"folio":"' + folio.toString() + '"}';
               jsonBack('Compra inteligente', destino, successfully, datos);
           }

           function OnSuccessCI_(response) {
               var items = $.parseJSON(response.d);
               var count_ = items.length;
               if (count_ == 3) {
                   if (items[1].toString() == "1") {
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtRENOVACION_CI589").prop("checked", true);
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl590\\|NUM_CONTRATO_CI").html("<div align='Right'>Núm. Contrato *</div>");
                   }
                   else {
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtRENOVACION_CI589").prop("checked", false);
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_CONTRATO_CI590').val("").prop("disabled", true);
                   }
                   if (items[2].toString() == "1") { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtAUTORIZO_INE591").prop("checked", true); }
                   else { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtAUTORIZO_INE591").prop("checked", false); }
               } else {
                   alert(items[0].toString());
               }
           }

           function contratoCI(contrato, nombre) {
               var message = "";
               var destino = "Blanco.aspx/validCI";
               var successfully = function OnSuccessContratoCI(response) {
                   var items = $.parseJSON(response.d);
                   message = items["mensaje"].toString();
               };
               var datos = '{"NContrato":"' + contrato.toString() + '", "Nombre": "' + nombre.toString() + '"}';
               jsonBack('Compra inteligente', destino, successfully, datos);
               return message;
           }

           function WScontratoCI(contrato, Etapa, Nombre) {
               var message = "";
               var destino = "Blanco.aspx/validCIWS";
               var successfully = function OnSuccessContratoCI(response) {
                   var items = $.parseJSON(response.d);
                   message = items["mensaje"].toString();
               };
               var datos = '{"NContrato":"' + contrato.toString() + '", "Etapa": ' + Etapa + ', "Nombre": "' + Nombre.toString() + '" }';
               jsonBack('Compra inteligente', destino, successfully, datos);
               return message;
           }

           //function calculateAge(birthMonth, birthDay, birthYear) {
           //    todayDate = new Date();
           //    todayYear = todayDate.getFullYear();
           //    todayMonth = todayDate.getMonth();
           //    todayDay = todayDate.getDate();
           //    age = todayYear - birthYear;

           //    if (todayMonth < birthMonth - 1) {
           //        age--;
           //    }

           //    if (birthMonth - 1 == todayMonth && todayDay < birthDay) {
           //        age--;
           //    }
           //    return age;
           //}

           function ValidaRFC($rfc) { //Quité el '$(' de aquí...
               var strCorrecta;
               strCorrecta = $rfc;
               var longitudCorrecta = false;
               var valid;

               if ($("#TipoPersona :selected").text() == "Moral") {
                   longitudCorrecta = (strCorrecta.length === 12);
                   valid = '^(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))';
               }
               if ($("#TipoPersona :selected").text() == "Fisica") {
                   longitudCorrecta = (strCorrecta.length === 13);
                   valid = '^(([A-Z]|[a-z]|\s){1})(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))';
               }
               if (!longitudCorrecta) {
                   alert('el rfc es incorrecto');
                   return false;
               }
               var validRfc = new RegExp(valid);
               var matchArray = strCorrecta.match(validRfc);
               if (matchArray == null) {
                   alert('el rfc es incorrecto');
                   return false;
               }
           };

           jQuery(window).load(function () {
               $(".divAdminCatPie").show();
           });

           $(document).ready(function () {
               //FADE IN-OUT PANTALLA SOLICITUD DE CREDITO
               
               $(".divAdminCatPie").hide();

               var dateSF = "";
               var countHOMO = "0";
               var countRFC = "0";
               var countNumIdent = "0";
               var countTEL = "";
               var badWords = ["CACA", "FETO", "KOJE", "MULA", "CACO", "GUEI", "KOJI", "PEDA", "CAKA", "GUEY", "KOJO", "PEDO", "CAKO", "JOTO", "KULO", "PENE", "COGE", "KACA", "MAME", "PITO", "COGI", "KACO", "MAMO", "PUTA", "COJA", "KAKA", "MEAR", "PUTO", "COJE", "KAKO", "MEAS", "QULO", "COJI", "KOGE", "MEON", "RATA", "COJO", "KOGI", "MION", "RUIN", "CULO", "KOJA", "MOCO", "CAGO"];

               $.urlParam = function (name) {
                   var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
                   if (results == null) {
                       return null;
                   }
                   else {
                       return results[1] || 0;
                   }
               }

               //$("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl546|ESTADO").val("Estado ***");

               var pantalla = $.urlParam("idPantalla").toString();
               if (pantalla == "1") { //BUG PD 03
                   JSONGetCI($.urlParam("idFolio").toString());
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_EXT21").removeAttr("onkeypress").attr('onkeypress', 'return ManejaCar("A",1,this.value,this,event);');
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_INT22").removeAttr("onkeypress").attr('onkeypress', 'return ManejaCar("A",1,this.value,this,event);');

                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC8").prop("disabled", "");

                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_DATOS_CREDITO").hide("");
                   //$("#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_EMPLEO").hide("");

                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_DATOS_CREDITO").hide("");
                   //$("#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_EMPLEO").hide("");

                   //$("#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_DATOS_CREDITO").html("");
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_EMPLEO").html("");

                   //$("#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_DATOS_CREDITO").html("");
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_EMPLEO").html("");

                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl543\\|CLAVE_ELECTOR div span").html("Clave de Elector <span class='tooltiptext'><img src='../App_Themes/Imagenes/ine.png'></span>");

                   //alert("Entra y oculta la seccion datos empleo pantalla 1");
               }

               var folio = $.urlParam("idFolio").toString();
               var user = "1";

               var tel = "";
               var tel_intent = 1;
               var estadoOn = $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtESTADO546").val(); //Verificar estado seleccionado
               if (((estadoOn > 1510) && (estadoOn < 1543)) || ((estadoOn > 9392) && (estadoOn < 9418))) {
                   JSONGetCity(folio);
               }


               $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtTIPO_OPERACION530').val(1475).prop("disabled", true);
               
               //$('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtVIVEN_EN27').val(107).prop("disabled", true);

               //alert("Entro pagina");
               if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtVIVEN_EN27").val() != 35471) {
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_REFERE_PERSONALES').fadeOut();
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_REFERE_PERSONALES').fadeOut();
               }

               if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtNACIONALIDAD25").val() == 126) {
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_LUGAR_NACIMIENTO').fadeOut();
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_LUGAR_NACIMIENTO').fadeOut();
               } else {
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_RESIDENTES_EXTRANJEROS').fadeOut();
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_RESIDENTES_EXTRANJEROS').fadeOut();
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_IDENTIFICACION_FISCAL').fadeOut();
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_IDENTIFICACION_FISCAL').fadeOut();
               }
               //
               //$('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24').datepicker({ dateFormat: 'yy-mm-dd' });
               //$('#ctl00_ctl00_cphCuerpo_cphPantallas_txtPDK_FECHA_MODIF531').datepicker({ dateFormat: 'yy-mm-dd' });

               var settingsDate = {                    
                   //dateFormat: "yy-mm-dd",
                   dateFormat: "dd/mm/yy",
                   showAnim: "slide",
                   changeMonth: true,
                   changeYear: true,
                   showOtherMonths: true,
                   selectOtherMonths: true,
                   autoSize: true,
                   yearRange: "-100:-18",
                   maxDate: '-18Y'
               };

               $.datepicker.setDefaults($.datepicker.regional["es"]);
               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24').datepicker(settingsDate).attr('readonly', 'true').attr('onkeydown', 'return false');
               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtPDK_FECHA_MODIF531').datepicker(settingsDate).attr('readonly', 'true').attr('onkeydown', 'return false');
               $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtTIPO_OPERACION530').val(1475).prop("disabled", true);
               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO586').datepicker(settingsDate).attr('readonly', 'true').attr('onkeydown', 'return false');

               if (($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtEDAD13').val() == "0") && ($.urlParam("idPantalla").toString() == "1")) {
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO586').val('');
               }

               JSONCompraInteligente(folio);
               jsonHondaAcura(folio);
               jsonDestinoUnidad(folio);
               JSONTipoPersona(folio);

               $("input").on("change", function () {
                   if ("ctl00_ctl00_cphCuerpo_cphPantallas_txtRENOVACION_CI589" == this.id.toString()) {
                       if ($(this).is(':checked')) {
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl590\\|NUM_CONTRATO_CI").html("<div align='Right'>Núm. Contrato *</div>");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_CONTRATO_CI590").prop("disabled", false);
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_CONTRATO_CI590").val("");
                       }
                       else {
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl590\\|NUM_CONTRATO_CI").html("<div align='Right'>Núm. Contrato</div>");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_CONTRATO_CI590").prop("disabled", true);
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_CONTRATO_CI590").val("");
                       }
                   }

                   if ("ctl00_ctl00_cphCuerpo_cphPantallas_txtCONDUCTOR_RECURRENTE576" == this.id.toString()) {
                       if ($(this).is(':checked')) {
                           var a1 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE14").val();
                           var a2 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE25").val();
                           var a3 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtAPELLIDO_PATERNO6").val();
                           var a4 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtAPELLIDO_MATERNO7").val();
                           var RFC1 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC8").val();
                           var edad1 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24").val();
                           var genero1 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtSEXO23").val();
                           var genero = "";

                           if (genero1 == "81") { genero = "1647"; }
                           else { genero = "1648"; }

                           if (a2 != "") { a2 = a2 + " "; }
                           if (a4 != "") { a4 = " " + a4; } //Arregla los espacios

                           var edad2 = edad1.split("/");

                           calculaEdadSF(edad2[0], edad2[1], edad2[2]);

                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_COMPLETO577").prop("disabled", true).val(a1 + " " + a2 + a3 + a4);
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC578").prop("disabled", true).val(RFC1);
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtGENERO580").prop("disabled", true).val(genero);
                       }
                       else {
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_COMPLETO577").prop("disabled", false).val("");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC578").prop("disabled", false).val("");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtEDAD579").prop("disabled", false).val("");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtGENERO580").prop("disabled", false).val("1647");
                       }
                   }
               });

               $("select").on("change", function () {
                   var SelectedValor = this.value;

                   if (SelectedValor == 1510) { $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCIUDAD547').find('option').remove().end(); }
                   if (SelectedValor == 1545) { $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtPAIS_DOMICILIO550').find('option').remove().end(); }

                   if (((SelectedValor > 1481) && (SelectedValor < 1491)) || (SelectedValor == 33050))
                   {
                       if ((SelectedValor == 1484) || (SelectedValor == 1485)) {
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl158\\|INGRESO_VARIABLE_COMPROBABLE div span").html("Ingresos Variables *<span class='tooltiptext'>Si es asalariado, jubilado, pensionado, Indica si tienes ingresos adicionales</span>");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl157\\|INGRESO_FIJO_COMPROBABLE div span").html("Ingresos Fijos<span class='tooltiptext'>Capture los ingresos totales brutos del cliente</span>");
                       }
                       else {
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl158\\|INGRESO_VARIABLE_COMPROBABLE div span").html("Ingresos Variables<span class='tooltiptext'>Si es asalariado, jubilado, pensionado, Indica si tienes ingresos adicionales</span>");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl157\\|INGRESO_FIJO_COMPROBABLE div span").html("Ingresos Fijos * <span class='tooltiptext'>Capture los ingresos totales brutos del cliente</span>");
                       }
                   }

                   if ((SelectedValor > 1616) && (SelectedValor < 1619)) { //PEP BUG PD 18
                       if (SelectedValor == 1617) {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtFUNCION_PEPS570').prop("disabled", false);
                           //$('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_PEPS_OTRA583').prop("disabled", false);
                       }
                       else {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtFUNCION_PEPS570').prop("disabled", true).val('229');
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_PEPS_OTRA583').prop("disabled", true).val('-');
                       }
                   }

                   if ((SelectedValor > 1510) && (SelectedValor < 1543)) { //Ciudades
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCIUDAD547').find('option').remove().end();
                       JSONCities(SelectedValor);
                   }

                   if ((SelectedValor > 9392) && (SelectedValor < 9418)) { //Ciudades
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCIUDAD547').find('option').remove().end();
                       JSONCities(SelectedValor);
                   }


                   if (SelectedValor == 125) {
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_LUGAR_NACIMIENTO").fadeIn();
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_LUGAR_NACIMIENTO").fadeIn();
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_IDENTIFICACION_FISCAL").fadeOut();
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_IDENTIFICACION_FISCAL").fadeOut();
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_RESIDENTES_EXTRANJEROS").fadeOut();
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_RESIDENTES_EXTRANJEROS").fadeOut();
                   }
                   if (SelectedValor == 126) {
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_LUGAR_NACIMIENTO").fadeOut();
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_LUGAR_NACIMIENTO").fadeOut();
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_IDENTIFICACION_FISCAL").fadeIn();
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_IDENTIFICACION_FISCAL").fadeIn();
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_RESIDENTES_EXTRANJEROS").fadeIn();
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_RESIDENTES_EXTRANJEROS").fadeIn();
                   }

                   if (((SelectedValor > 1490) && (SelectedValor < 1495)) || (SelectedValor == 11) || ((SelectedValor > 1798) && (SelectedValor < 1821))) {

                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl543\\|CLAVE_ELECTOR div span").html("Clave de Elector<span class='tooltiptext'><img src='../App_Themes/Imagenes/ine.png'></span>");
                       
                       if (SelectedValor == 1493) {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCLAVE_ELECTOR543').prop("disabled", false).val('');
                           $("#img_dinamic").attr("src", "../App_Themes/Imagenes/n_iden.png");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl543\\|CLAVE_ELECTOR div span").html("Clave de Elector<span class='tooltiptext'><img src='../App_Themes/Imagenes/ine.png'></span>");
                       }
                       else {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCLAVE_ELECTOR543').prop("disabled", true).val("");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl543\\|CLAVE_ELECTOR div span").html("Clave de Elector <span class='tooltiptext'><img src='../App_Themes/Imagenes/ine.png'></span>");
                           if (SelectedValor == 1491) { $("#img_dinamic").attr("src", "../App_Themes/Imagenes/cp.png"); }
                           else if (SelectedValor == 1492) { $("#img_dinamic").attr("src", "../App_Themes/Imagenes/passport.png"); }
                           else if (SelectedValor == 1494) { $("#img_dinamic").attr("src", "../App_Themes/Imagenes/fm.png"); }
                       }
                   }

                   if ((SelectedValor > 1618) && (SelectedValor < 1621)) {
                       if (SelectedValor == 1620) {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtPARENTESCO_PEPS571').prop("disabled", true).val(225);
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_PEPS572').prop("disabled", true).val('-');
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtFUNCION_REL_PEPS573').prop("disabled", true).val(226);
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_REL_PEPS_OTRA584').prop("disabled", true).val('-');

                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl571\\|PARENTESCO_PEPS").html("<div align='Right'>¿Qué parentesco tiene con el PEP?</div>");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl573\\|FUNCION_REL_PEPS").html("<div align='Right'>¿Qué función desempeñó o ha desempeñado la PEP con la que tiene parentesco?</div>");
                       }
                       else {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtPARENTESCO_PEPS571').prop("disabled", false).val(225);
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_PEPS572').prop("disabled", false).val('');
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtFUNCION_REL_PEPS573').prop("disabled", false).val(226);
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_REL_PEPS_OTRA584').prop("disabled", true).val('-');

                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl571\\|PARENTESCO_PEPS").html("<div align='Right'>¿Qué parentesco tiene con el PEP? *</div>");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl573\\|FUNCION_REL_PEPS").html("<div align='Right'>¿Qué función desempeñó o ha desempeñado la PEP con la que tiene parentesco? *</div>");
                       }
                   }

                   if ((SelectedValor > 35468) && (SelectedValor < 35474)) {
                       if (SelectedValor == 35471) {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_REFERE_PERSONALES').fadeIn();
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_REFERE_PERSONALES').fadeIn();
                       } else {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_REFERE_PERSONALES').fadeOut();
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_REFERE_PERSONALES').fadeOut();
                       }
                   }

                   if ((SelectedValor > 82) && (SelectedValor < 88)) {
                       if (SelectedValor == 85) {
                           //$("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtREGIMEN_CONYUGAL535 option[value='']").attr('selected', true);
                           //$("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtREGIMEN_CONYUGAL535").prepend("<option value=''></option>").val('');
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtREGIMEN_CONYUGAL535').val('').prop("disabled", false);
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl535\\|REGIMEN_CONYUGAL").html("Régimen Conyugal *");
                       }
                       else {
                           //$("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtREGIMEN_CONYUGAL535 option[value='']").attr('selected', true);
                           //$("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtREGIMEN_CONYUGAL535").prepend("<option value=''></option>").val('');
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtREGIMEN_CONYUGAL535').val('').prop("disabled", true);
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl535\\|REGIMEN_CONYUGAL").html("Régimen Conyugal");
                       }
                   }
                   if (SelectedValor == 1654) { $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtREGIMEN_CONYUGAL535').val('').prop("disabled", true); } //Arregla regimen conyugal

                   if ((SelectedValor > 1620) && (SelectedValor < 1634)) {
                       if (SelectedValor == 1633) { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_PEPS_OTRA583').prop("disabled", false); }
                       else { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_PEPS_OTRA583').prop("disabled", true).val('-'); }
                   }

                   if ((SelectedValor > 1676) && (SelectedValor < 1690)) {
                       if (SelectedValor == 1689) { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_REL_PEPS_OTRA584').prop("disabled", false); }
                       else { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_REL_PEPS_OTRA584').prop("disabled", true).val('-'); }
                   }

                   if ((SelectedValor > 1644) && (SelectedValor < 1647)) {
                       if (SelectedValor == 1645) { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_TERCERO575').prop("disabled", false); }
                       else { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_TERCERO575').prop("disabled", true).val('-'); }
                   }
               });

               $("input").on("keyup", function (e) {
                   var id_select = e.currentTarget.id.toString();
                   var regNUM = new RegExp('^[0-9]+$');
                   /*if (id_select == "ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_PARTI9") {
                       if ((e.currentTarget.value.toString().length == 10) && (regNUM.test(e.key.toString()))) {
                           if (tel != "") {
                               if (tel_intent == 0) {
                                   if (e.currentTarget.value.toString() != tel) {
                                       tel = "";
                                       e.currentTarget.value = "";
                                       tel_intent = 1;
                                       alert("Los numeros ingresados no coinciden, favor de ingresarlos nuevamente.");
                                       //$("body").focus();
                                   }
                                   if (e.currentTarget.value.toString() == tel) {
                                       tel = "";
                                       tel_intent = 1;
                                   }
                               } else {
                                   tel = e.currentTarget.value.toString();
                                   e.currentTarget.value = "";
                                   tel_intent = 0;
                                   alert("Ingresar nuevamente el telefono particular.");
                                   //$("body").focus();
                               }
                           } else {
                               tel = e.currentTarget.value.toString();
                               e.currentTarget.value = "";
                               tel_intent = 0;
                               alert("Ingresar nuevamente el telefono particular.");
                               //$("body").focus();
                           }
                       }
                   }*/

                   if (id_select == "ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESOS_VARIABLES540") {
                       //ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24
                       //alert("entro ing var");
                   }

                   //if (id_select == "ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24") {
                   //    if (e.currentTarget.value.toString().length == 10) {
                   //        var fecha = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24").val().split("-")
                   //        var edad = calculateAge(fecha[1], fecha[2], fecha[0]);
                   //        if (edad < 18) {
                   //            alert("Debe ser mayor de edad.");
                   //        }
                   //    }
                   //}
               });

               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO586').on("change", function (e) {
                   var fNac = $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO586').val();
                   var fNacArray = fNac.split("/");
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtANO_NACIMIENTO412').val(fNacArray[2]);
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtMES_NACIMIENTO413').val(fNacArray[1]);
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtDIA_NACIMIENTO414').val(fNacArray[0]);
                   calculaEdad();
                   CalcularRFC();   
               });

               $('input').focusout(function (e) {
                   var id_select = e.currentTarget.id.toString();
                   if ($("#" + id_select).val() == "") { return; }
                   
                   $("#" + id_select).val($.trim($("#" + id_select).val()));
                   /*val
                   if ((id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC8') || (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC578')) {
                       var worsRFC = 
                       if ($.inArray('specialword', badWords) > -1) {
                           // the value is in the array

                       }
                   }*/

                   //if (id_select == "ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_PARTI9") {
                   if (id_select == "ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10") {
                       if ($("#" + id_select).val() == "") { return; }

                       var telefone = "";
                       if ($.urlParam("idPantalla").toString() == '7') { telefone = "particular"; }
                       else { telefone = "fijo"; }
                       
                       var telParti = $("#" + id_select).val();
                       if (telParti.length != 10) {
                           alert('El Teléfono Móvil debe tener 10 dígitos.');
                           setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                           countTEL = "";
                       } else {
                           if (countTEL == "") {
                               telefone = "";
                               alert('Ingresar nuevamente el Teléfono Móvil' + telefone);
                               countTEL = $("#" + id_select).val();
                               setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                           } else {
                               if (countTEL != telParti) {
                                   countTEL = "";
                                   setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                                   alert('Los Teléfonos ingresados no coinciden.');
                               } else { countTEL = ""; } //BUG_PD_08
                           }
                       }
                   }

                   if (id_select == "ctl00_ctl00_cphCuerpo_cphPantallas_txtMAIL554") {
                       var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                       var valido = re.test(e.currentTarget.value.toString());
                       if (!valido) {
                           alert('Formato de correo invalido');
                           e.currentTarget.value = "";
                       }
                   }

                   if ($.urlParam("idPantalla").toString() == '1') {
                       if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtN_IDENTIFICACION542') {
                           if ($("#" + id_select).val() == "") { return; }

                           var numIdent = $("#" + id_select).val();
                           if (numIdent == '') {
                               alert('Debe llenar el campo Nº de Identificación.');
                               $("#" + id_select).val("");
                               countNumIdent = "0";
                           } else {
                               if (countNumIdent == "0") {
                                   alert('Ingresa nuevamente el Nº de Identificación.');
                                   countNumIdent = $("#" + id_select).val();
                                   //$("#" + id_select).val("").focus();
                                   setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                               } else {
                                   numIdent = numIdent.toLowerCase();
                                   countNumIdent = countNumIdent.toLowerCase();
                                   if (countNumIdent != numIdent) {
                                       countNumIdent = "0";
                                       //$("#" + id_select).val("").focus();
                                       setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                                       alert('Los Nº de Identificación ingresadas no coinciden.');
                                   } else { countNumIdent = "0"; } //BUG_PD_08
                               }
                           }

                       }

                       if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE14') {
                           if ($("#" + id_select).val() == "") { return; }

                           var nombre = $("#" + id_select).val();
                           if (nombre.length < 1) {
                               alert("Debe ingresar al menos 1 caracter.")
                               //$("#" + id_select).val("").focus();
                               setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                           }
                       }

                       if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtAPELLIDO_PATERNO6_CANCELADO') {
                           if ($("#" + id_select).val() == "") { return; }

                           var apellido = $("#" + id_select).val();
                           if (apellido.length < 3) {
                               alert("Debe ingresar al menos 3 caracteres.")
                               //$("#" + id_select).val("").focus();
                               setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                           }
                       }

                       if (id_select.toLowerCase().indexOf("telefono") >= 0) {
                           if ($("#" + id_select).val() == "") { return; }

                           var verifiTel = $("#" + id_select).val();
                           //if (id_select != "ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_PARTI9") {
                           if (id_select != "ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10") {
                               if (verifiTel.length < 10) {
                                   alert('El Teléfono Fijo debe contener 10 digitos.');
                                   //$("#" + id_select).val("").focus();
                                   setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                               } else {
                                   var telFijo = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_PARTI9").val();
                                   var telMov = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10").val();
                                   if ((telFijo == telMov) && (telFijo.length > 0)) {
                                       //$("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10").val("").focus();
                                       setTimeout(function () { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10").val("").focus(); }, 1);
                                       alert("El Teléfono Móvil y Fijo no pueden ser iguales.");
                                   }
                               }
                           }
                       }

                       if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_RESIDE_DOM29') {
                           var a = parseInt($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtEDAD13').val());
                           var b = parseInt($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_RESIDE_DOM29').val());
                           if (a < b) {
                               alert('Los años de residir no pueden ser mayores a la edad actual.');
                               $("#" + id_select).val("");
                           }
                       }

                       if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtHOMOCLAVE443') {
                           if ($("#" + id_select).val() == "") { return; }

                           var HCLAVE = $("#" + id_select).val();
                           if (HCLAVE.length != 3) {
                               alert('La HOMOCLAVE debe tener formato "XXX".');
                               //$("#" + id_select).val("").focus();
                               setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                               countHOMO = "0";
                           } else {
                               if (countHOMO == "0") {
                                   alert('Ingresa nuevamente la HOMOCLAVE.');
                                   countHOMO = $("#" + id_select).val();
                                   //$("#" + id_select).val("").focus();
                                   setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                               } else {
                                   HCLAVE = HCLAVE.toLowerCase();
                                   countHOMO = countHOMO.toLowerCase();
                                   if (countHOMO != HCLAVE) {
                                       countHOMO = "0";
                                       //$("#" + id_select).val("").focus();
                                       setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                                       alert('Las HOMOCLAVES ingresadas no coinciden.');
                                   } else { countHOMO = "0"; } //BUG_PD_08
                               }
                           }
                       }

                       if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtCURP19') {
                           if ($("#" + id_select).val() == "") { return; }

                           var curp = $("#" + id_select).val();
                           if (curp.length != 18) {
                               alert('El CURP debe ser de 18 digitos.');
                               //$("#" + id_select).val("");
                               setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                           } else {
                               if (!curp.match(/[a-zA-Z]{4,4}[0-9]{6}[a-zA-Z]{6,6}[0-9]{2}/)) {
                                   //return false
                                   alert('El CURP debe tener formato "AAAA999999AAAAAA99".');
                                   //$("#" + id_select).val("");
                                   setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                               }
                           }
                       }
                   }

                   if (($.urlParam("idPantalla").toString() == '7') || (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC8')) {
                       if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_RESIDE_DOM29') {
                           var d = new Date();
                           var n = d.getFullYear();

                           var year_ = $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24').val().toString().split("/");
                           var a = parseInt(year_[2]);
                           var residence = parseInt($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_RESIDE_DOM29').val());
                           var b = n - residence;

                           if (a > b ) {
                               alert('Los años de residir no pueden ser mayores a la edad actual.');
                               setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                           }
                       }

                       if (id_select.toLowerCase().indexOf("telefono") >= 0) {
                           if ($("#" + id_select).val() == "") { return; }

                          var verifiTel = $("#" + id_select).val();
                          if (id_select != "ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_PARTI9")  {
                              if (verifiTel.length < 10) {
                                  alert('El Teléfono debe contener 10 digitos.');
                                  //$("#" + id_select).val("").focus();
                                  setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                              }
                          }
                          var telFijo = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_PARTI9").val();
                          var telMov = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10").val();
                          if ((telFijo == telMov) && (telFijo.length > 0)) {
                              //$("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10").val("").focus();
                              setTimeout(function () { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10").val("").focus(); }, 1);
                              alert("El teléfono móvil y fijo no pueden ser iguales.");
                          }
                      }

                      if ((id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD553') || (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD_EMPLEO_ANT559')) {
                          if ($("#" + id_select).val() == "") { return; }

                          var meses = parseInt($("#" + id_select).val());
                          if (meses > 11) {
                              alert('El número máximo de meses es de 11.');
                              setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);

                              $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_EMPLEO_ANT555').prop("disabled", true).val("");
                              $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_EMPLEO_ANT556').prop("disabled", true).val("");
                              $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtEXT_EMPLEO_ANT557').prop("disabled", true).val("");
                              $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD_EMPLEO_ANT558').prop("disabled", true).val("");
                              $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD_EMPLEO_ANT559').prop("disabled", true).val("");

                              $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl555\\|NOMBRE_EMPLEO_ANT").html("Nombre de la Empresa del Empleo Anterior");
                              $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl556\\|TELEFONO_EMPLEO_ANT").html("Teléfono de la Empresa Empleo Anterior");
                              $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl557\\|EXT_EMPLEO_ANT").html("Ext de la Empresa Empleo Anterior");
                              $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl558\\|ANOS_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Años) Empleo Anterior");
                              $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl559\\|MESES_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Meses) Empleo Anterior");
                          }
                      }


                        if ((id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_RESIDE_DOM534') || (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_RESIDE_DOM29')) {
                            if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_RESIDE_DOM534").val() == "") { return; }
                            if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_RESIDE_DOM29").val() == "") { return; }

                            var anios = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_RESIDE_DOM29").val().toString();

                            if (anios == "0") {
                                var meses = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_RESIDE_DOM534").val().toString();
                                if (meses < 6 ) {
                                    alert('Mínimo 6 meses de residir en el domicilio.');
                                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_RESIDE_DOM534").val("");
                                    setTimeout(function () { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_RESIDE_DOM29").val("").focus(); }, 1);
                                }
                            }
                        }


                        if ((id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD42') || (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD553')) {
                            if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD42").val() == "") {
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_EMPLEO_ANT555').prop("disabled", true).val("");
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_EMPLEO_ANT556').prop("disabled", true).val("");
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtEXT_EMPLEO_ANT557').prop("disabled", true).val("");
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD_EMPLEO_ANT558').prop("disabled", true).val("");
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD_EMPLEO_ANT559').prop("disabled", true).val("");

                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl555\\|NOMBRE_EMPLEO_ANT").html("Nombre de la Empresa del Empleo Anterior");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl556\\|TELEFONO_EMPLEO_ANT").html("Teléfono de la Empresa Empleo Anterior");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl557\\|EXT_EMPLEO_ANT").html("Ext de la Empresa Empleo Anterior");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl558\\|ANOS_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Años) Empleo Anterior");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl559\\|MESES_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Meses) Empleo Anterior");
                                return;
                            }
                            if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD553").val() == "") {
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_EMPLEO_ANT555').prop("disabled", true).val("");
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_EMPLEO_ANT556').prop("disabled", true).val("");
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtEXT_EMPLEO_ANT557').prop("disabled", true).val("");
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD_EMPLEO_ANT558').prop("disabled", true).val("");
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD_EMPLEO_ANT559').prop("disabled", true).val("");

                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl555\\|NOMBRE_EMPLEO_ANT").html("Nombre de la Empresa del Empleo Anterior");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl556\\|TELEFONO_EMPLEO_ANT").html("Teléfono de la Empresa Empleo Anterior");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl557\\|EXT_EMPLEO_ANT").html("Ext de la Empresa Empleo Anterior");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl558\\|ANOS_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Años) Empleo Anterior");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl559\\|MESES_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Meses) Empleo Anterior");
                                return;
                            }

                            var anios = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD42").val().toString();
                            var meses = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD553").val().toString();

                            if (((anios == 0) && (meses < 3)) || (anios > 0)) {
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_EMPLEO_ANT555').prop("disabled", true).val("");
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_EMPLEO_ANT556').prop("disabled", true).val("");
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtEXT_EMPLEO_ANT557').prop("disabled", true).val("");
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD_EMPLEO_ANT558').prop("disabled", true).val("");
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD_EMPLEO_ANT559').prop("disabled", true).val("");

                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl555\\|NOMBRE_EMPLEO_ANT").html("Nombre de la Empresa del Empleo Anterior");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl556\\|TELEFONO_EMPLEO_ANT").html("Teléfono de la Empresa Empleo Anterior");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl557\\|EXT_EMPLEO_ANT").html("Ext de la Empresa Empleo Anterior");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl558\\|ANOS_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Años) Empleo Anterior");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl559\\|MESES_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Meses) Empleo Anterior");

                                if ((anios == 0) && (meses < 3)) {
                                    alert('Mínimo 3 mes de antigüedad en el empleo .');
                                    setTimeout(function () { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD553").val("").focus(); }, 1);
                                }
                                return;
                            }

                            if ((anios == 0) && (meses > 2)) {
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_EMPLEO_ANT555').prop("disabled", false).val();
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_EMPLEO_ANT556').prop("disabled", false).val();
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtEXT_EMPLEO_ANT557').prop("disabled", false).val();
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD_EMPLEO_ANT558').prop("disabled", false).val();
                                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD_EMPLEO_ANT559').prop("disabled", false).val();

                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl555\\|NOMBRE_EMPLEO_ANT").html("Nombre de la Empresa del Empleo Anterior *");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl556\\|TELEFONO_EMPLEO_ANT").html("Teléfono de la Empresa Empleo Anterior *");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl557\\|EXT_EMPLEO_ANT").html("Ext de la Empresa Empleo Anterior");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl558\\|ANOS_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Años) Empleo Anterior *");
                                $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl559\\|MESES_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Meses) Empleo Anterior *");
                                return;
                            }
                        } 


                      /*if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtCP14') {
                          if ($("#" + id_select).val() == "") { return; }

                          var verifiTel = $("#" + id_select).val();
                          if (verifiTel.length < 5) {
                              //$("#" + id_select).val("");
                              setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                              alert('El Código postal debe ser de 5 digitos.');                             
                          }
                      }*/

                      /*if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtCP50') {
                          if ($("#" + id_select).val() == "") { return; }

                          var verifiTel = $("#" + id_select).val();
                          if (verifiTel.length < 5) {
                              //$("#" + id_select).val("").focus();
                              setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                              alert('El Código postal debe ser de 5 digitos.');
                          }
                      }*/

                      if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_RESIDE_DOM534') {
                          if ($("#" + id_select).val() == "") { return; }

                          var MesesResi = $("#" + id_select).val();
                          if (MesesResi > 11) {
                              alert('El número máximo de meses es de 11.');
                              //$("#" + id_select).val("");
                              setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                          }
                      }

                      if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtCURP19') {
                          if ($("#" + id_select).val() == "") { return; }

                          var curp = $("#" + id_select).val();
                          if (curp.length != 18) {
                              alert('El CURP debe ser de 18 digitos.');
                              //$("#" + id_select).val("");
                              setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                          } else {
                              if (!curp.match(/[a-zA-Z]{4,4}[0-9]{6}[a-zA-Z]{6,6}[0-9]{2}/)) {
                                  //return false
                                  alert('El CURP debe tener formato "AAAA999999AAAAAA99".');
                                  //$("#" + id_select).val("");
                                  setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                              } 
                          }
                      }

                      if ((id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC8') || (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC578')) {
                          if ($("#" + id_select).val() == "") { return; }

                          //$("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", true);
                          var RFC = $("#" + id_select).val();
                          if (RFC.length != 10) {
                              alert('El RFC debe ser de 10 digitos.');
                              //$("#" + id_select).val("").focus();
                              setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                              countRFC = "0";
                          } else {
                              RFC = RFC.toUpperCase();
                              if (!RFC.match(/^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))((-)?([A-Z\d]{3}))?$/)) {
                                  alert('El RFC debe tener formato "AAAA999999".');
                                  //$("#" + id_select).val("").focus();
                                  setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                                  countRFC = "0";
                              } else {
                                  if (countRFC == "0") {
                                      alert('Ingresa nuevamente el RFC.');
                                      countRFC = $("#" + id_select).val();
                                      //$("#" + id_select).val("").focus();
                                      setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                                  } else {
                                      countRFC = countRFC.toLowerCase();
                                      RFC = RFC.toLowerCase();
                                      if (countRFC != RFC) {
                                          countRFC = "0";
                                          //$("#" + id_select).val("").focus();
                                          setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                                          alert('Los RFC ingresados no coinciden.');
                                      } else { countRFC = "0"; $("#" + id_select).val($("#" + id_select).val().toUpperCase()); } //BUG_PD_08
                                  }

                              }
                          }
                      }

                      if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtHOMOCLAVE443') {
                          if ($("#" + id_select).val() == "") { return; }

                          var HCLAVE = $("#" + id_select).val();
                          if (HCLAVE.length != 3) {
                              alert('La HOMOCLAVE debe tener formato "XXX".');
                              //$("#" + id_select).val("");
                              setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                              countHOMO = "0";
                          } else {
                              if (countHOMO == "0") {
                                  alert('Ingresa nuevamente la HOMOCLAVE.');
                                  countHOMO = $("#" + id_select).val();
                                  //$("#" + id_select).val("");
                                  setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                              } else {
                                  HCLAVE = HCLAVE.toLowerCase();
                                  countHOMO = countHOMO.toLowerCase();
                                  if (countHOMO != HCLAVE) {
                                      countHOMO = "0";
                                      //$("#" + id_select).val("");
                                      setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                                      alert('Las HOMOCLAVES ingresadas no coinciden.');
                                  } else { countHOMO = "0"; } //BUG_PD_08
                              }
                          }
                      }

                      if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtN_IDENTIFICACION542') {
                          if ($("#" + id_select).val() == "") { return; }

                          var numIdent = $("#" + id_select).val();
                          if (numIdent == '') {
                              alert('Debe llenar el campo Nº de Identificación.');
                              $("#" + id_select).val("");
                              countNumIdent = "0";
                          } else {
                              if (countNumIdent == "0") {
                                  alert('Ingresa nuevamente el Nº de Identificación.');
                                  countNumIdent = $("#" + id_select).val();
                                  //$("#" + id_select).val("").focus();
                                  setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                              } else {
                                  numIdent = numIdent.toLowerCase();
                                  countNumIdent = countNumIdent.toLowerCase();
                                  if (countNumIdent != numIdent) {
                                      countNumIdent = "0";
                                      //$("#" + id_select).val("").focus();
                                      setTimeout(function () { $("#" + id_select).val("").focus(); }, 1);
                                      alert('Los Nº de Identificación ingresadas no coinciden.');
                                  } else { countNumIdent = "0"; } //BUG_PD_08
                              }
                          }

                      }

                      if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtEDAD579') {
                          var Edad_ = $("#" + id_select).val();
                          if (Edad_.length > 2) {
                              alert('La edad máxima es 99');
                              $("#" + id_select).val("");
                          }
                      }

                      if (id_select == 'ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_RESIDE_DOM29') {
                          var año = $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24').val().toString().split('/');
                          var d = new Date();
                          var n = d.getFullYear();
                          año = año[2];
                          sumaYears =  parseInt(año) +  parseInt($("#" + id_select).val());
                          if (sumaYears > n) {
                              alert('Los años de residir no pueden ser mayores edad acutal.');
                              $("#" + id_select).val("");
                          }
                      }
                   }
               });

               var Enable = $.urlParam("Enable").toString();
               if (Enable = "0") {
                   if (($.urlParam("idPantalla").toString()) == "7") {
                       var tablas = $('table', '#ctl00_ctl00_cphCuerpo_cphPantallas_pantalla');
                       var u = "ctl00_ctl00_cphCuerpo_cphPantallas_U_";
                       var i = "ctl00_ctl00_cphCuerpo_cphPantallas_I_";
                       $.each(tablas, function (tabla) {
                           if ((tablas[tabla].id == u + "PDK_TAB_DATOS_SOLICITANTE") || (tablas[tabla].id == i + "PDK_TAB_DATOS_SOLICITANTE")) {

                           } else if ((tablas[tabla].id == u + "PDK_TAB_SOLICITANTE") || (tablas[tabla].id == i + "PDK_TAB_SOLICITANTE")) {
                               $('input', '#' + tablas[tabla].id + ' tr').prop("disabled", true);
                               $('select', '#' + tablas[tabla].id + ' tr').prop("disabled", true);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_PARTI9').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCORREO_ELECTRONICO11').prop("disabled", false);
                           } else if ((tablas[tabla].id == u + "PDK_TAB_DATOS_PERSONALES") || (tablas[tabla].id == i + "PDK_TAB_DATOS_PERSONALES")) {
                               $('input', '#' + tablas[tabla].id + ' tr').prop("disabled", true);
                               $('select', '#' + tablas[tabla].id + ' tr').prop("disabled", true);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCURP19').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_RESIDE_DOM534').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtREGIMEN_CONYUGAL535').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_DEPENDIENTES536').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtESTUDIOS537').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtSITU_LABORAL538').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtNIVEL_ACADEMICO36').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESOS_FIJOS539').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESOS_VARIABLES540').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtTIPO_IDENTIFICACION541').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtN_IDENTIFICACION542').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCLAVE_ELECTOR543').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtBANCO_DOM544').prop("disabled", false);
                           } else if ((tablas[tabla].id == u + "PDK_TAB_EMPLEO") || (tablas[tabla].id == i + "PDK_TAB_EMPLEO")) {
                               $('input', '#' + tablas[tabla].id + ' tr').prop("disabled", false);
                               $('select', '#' + tablas[tabla].id + ' tr').prop("disabled", false);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD42').prop("disabled", true);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtDELEGA_O_MUNICI54').prop("disabled", true);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCIUDAD53').prop("disabled", true);
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtESTADO51').prop("disabled", true);
                           } else {
                               $('input', '#' + tablas[tabla].id + ' tr').prop("disabled", false);
                               $('select', '#' + tablas[tabla].id + ' tr').prop("disabled", false);
                           }
                       });

                       var img_dinamic = $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtTIPO_IDENTIFICACION541").val();
                       if (img_dinamic != 1493) {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCLAVE_ELECTOR543').prop("disabled", true).val("");
                           if (img_dinamic == 1491) { $("#img_dinamic").attr("src", "../App_Themes/Imagenes/cp.png"); }
                           else if (img_dinamic == 1492) { $("#img_dinamic").attr("src", "../App_Themes/Imagenes/passport.png"); }
                           else if (img_dinamic == 1494) { $("#img_dinamic").attr("src", "../App_Themes/Imagenes/fm.png"); }
                       } else { $("#img_dinamic").attr("src", "../App_Themes/Imagenes/n_iden.png"); }


                       if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtTIENE_REL_PEPS569").val() == 1620) {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtPARENTESCO_PEPS571').prop("disabled", true).val(225);
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_PEPS572').prop("disabled", true).val('-');
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtFUNCION_REL_PEPS573').prop("disabled", true).val(226);
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_REL_PEPS_OTRA584').prop("disabled", true).val('-');

                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl571\\|PARENTESCO_PEPS").html("<div align='Right'>¿Qué parentesco tiene con el PEP?</div>");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl573\\|FUNCION_REL_PEPS").html("<div align='Right'>¿Qué función desempeñó o ha desempeñado la PEP con la que tiene parentesco?</div>");
                       }

                       /*if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD42").val() > 0) {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD553').prop("disabled", true).val("0");
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_EMPLEO_ANT555').prop("disabled", true).val("-");
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_EMPLEO_ANT556').prop("disabled", true).val("0");
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtEXT_EMPLEO_ANT557').prop("disabled", true).val("0");
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD_EMPLEO_ANT558').prop("disabled", true).val("0");
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD_EMPLEO_ANT559').prop("disabled", true).val("0");
                       }*/

                   }
               } else if (Enable = "1") {
                   var tablas = $('table', '#ctl00_ctl00_cphCuerpo_cphPantallas_pantalla');
                   $.each(tablas, function (tabla) {
                       $('input', '#' + tablas[tabla].id + ' tr').prop("disabled", true);
                       $('select', '#' + tablas[tabla].id + ' tr').prop("disabled", true);
                   });
               }

               //FIN FADE IN-OUT PANTALLA SOLICITUD DE CREDITO

               if ($("[id$=hdnIdRegistro]").val() != 7) {
                   $('[id*="GuardarTMP"]').hide();
               }
               if ($("[id$=hdnIdRegistro]").val() == 7) {
                   var enbl = $.urlParam("Enable").toString();
                   if (enbl == "1") {
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnGuardarTMP").hide();
                   }
               }
               chkBuro = $('[id*="VISITAR_BURO"]');
               chkBuro.prop("cheked", true);
               chkBuro.hide();
               
               btnProcesar = $("[id$=Button1]");
               btnCancelarNew = $("[id$=btnCancelarNew]");
               //               btnAutoriza = $("[id$=btnAutoriza]");
               btnImprimir = $("[id$=btnImprimir]");

               fnIsEneable()

               $('[id$=txtFECHA_CONSTITUCION470]').datepicker();
               $('[id$=txtFECHA_REGISTRO477]').datepicker();
               $('[id$=txtFECHA_OPERACION481]').datepicker();

               $('[name$=txtPREGUNTA1422]').click(function () {
                   var enable = $('[id$=txtNUMERO_CREDITO153]')
                   if ($(this).parent().text() == "SI") { enable.removeAttr('disabled'); $('[id$=hdvalidad]').val('NUMERO_CREDITO153') } else { enable.attr('disabled', 'disabled'); $('[id$=hdvalidad]').val('') }

               });

               $('[name$=txtPREGUNTA1425]').click(function () {
                   var enable = $('[id$=txtNUMERO_DE_CREDITO_COA176]')
                   if ($(this).parent().text() == "SI") { enable.removeAttr('disabled'); $('[id$=hdvalidadCoa]').val('NUMERO_DE_CREDITO_COA176'); } else { enable.attr('disabled', 'disabled'); $('[id$=hdvalidadCoa]').val(''); }
               });


               var pantalla = $("[id$=hdnIdRegistro]").val();
               if (pantalla == 1 || pantalla == 7 || pantalla == 23 || pantalla == 29 || pantalla == 35 || pantalla == 41 /*|| pantalla == 59*/) {
                   llenarfecha();
                   var anos = $("[id$=hdnano]").val();
                   var mes = $("[id$=hdnmes]").val();
                   var dia = $("[id$=hdndia]").val();
                   if (anos != "") { $('[id$=ddltxtANO_NACIMIENTO412]')[0].value = anos; }
                   if (mes != "") { if (mes < 10) { mes = '0' + mes; } $('[id$=ddltxtMES_NACIMIENTO413]')[0].value = mes; }
                   if (dia != "") { if (dia < 10) { dia = '0' + dia; } $('[id$=ddltxtDIA_NACIMIENTO414]')[0].value = dia; }

                   if (pantalla != 7 && pantalla != 41) {
                       //se cambia codigo por funcion que valida la impresion del documento de buro
                       if (QueryString.Enable != undefined) {
                           if (QueryString.Enable == 0) {
                               fnValidaBuro();
                           }         
                       }
                   }

               } else if (pantalla == 5 || pantalla == 15 || pantalla == 27 || pantalla == 39 || pantalla == 47) {
                   llenarfechaCoa();
                   if (pantalla == 5) {
                       if (QueryString.Enable != undefined) {
                           if (QueryString.Enable == 0) {
                               fnValidaBuro();
                           }
                       }
                   }
                   var anos = $("[id$=hdnanocoa]").val();
                   var mes = $("[id$=hdnmescoa]").val();
                   var dia = $("[id$=hdndiacoa]").val();
                   if (anos != "") { $('[id$=ddltxtANO_NACIMIENTO415]')[0].value = anos; }
                   if (mes != "") { if (mes < 10) { mes = '0' + mes; } $('[id$=ddltxtMES_NACIMIENTO416]')[0].value = mes; }
                   if (dia != "") { if (dia < 10) { dia = '0' + dia; } $('[id$=ddltxtDIA_NACIMIENTO417]')[0].value = dia; }

               } else if (pantalla == 6 || pantalla == 5 || pantalla == 27 || pantalla == 28 || pantalla == 39 || pantalla == 40) {
                   llenarfecha();
                   llenarfechaCoa();
                   var anos = $("[id$=hdnano]").val();
                   var mes = $("[id$=hdnmes]").val();
                   var dia = $("[id$=hdndia]").val();
                   if (anos != "") { $('[id$=ddltxtANO_NACIMIENTO412]')[0].value = anos; }
                   if (mes != "") { if (mes < 10) { mes = '0' + mes; } $('[id$=ddltxtMES_NACIMIENTO413]')[0].value = mes; }
                   if (dia != "") { if (dia < 10) { dia = '0' + dia; } $('[id$=ddltxtDIA_NACIMIENTO414]')[0].value = dia; }
                   var anoscoa = $("[id$=hdnanocoa]").val();
                   var mescoa = $("[id$=hdnmescoa]").val();
                   var diacoa = $("[id$=hdndiacoa]").val();
                   if (anoscoa != "") { $('[id$=ddltxtANO_NACIMIENTO415]')[0].value = anoscoa; }
                   if (mescoa != "") { if (mescoa < 10) { mescoa = '0' + mescoa; } $('[id$=ddltxtMES_NACIMIENTO416]')[0].value = mescoa; }
                   if (diacoa != "") { if (diacoa < 10) { diacoa = '0' + diacoa; } $('[id$=ddltxtDIA_NACIMIENTO417]')[0].value = diacoa; }


               }

               cleanFormTables();

               //            if (pantalla == 15) { $('[id$=txtFECHA_CONSTITUCION470]').datepicker(); }

               habilitarCampos();
               deshabilitarCampos();

               //BUG PD 05
               if (pantalla == "7") {
                   //BUG PD 34
                   if ($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO163').val() == "0") { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO163').val(''); }
                   if ($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO562').val() == "0") { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO562').val(''); }
                   if ($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO565').val() == "0") { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO565').val(''); }

                   dateSF = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtPDK_FECHA_MODIF531").val();

                   var dateFake = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtPDK_FECHA_MODIF531").val();
                   var dateFakeArray = dateFake.split("-");
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtPDK_FECHA_MODIF531").val(dateFakeArray[2] + "/" + dateFakeArray[1] + "/" + dateFakeArray[0]);

                   if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCOMPANY_TEL_PARTICULAR582").val() == 1667) { $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCOMPANY_TEL_PARTICULAR582").val(1668); }
                   //BUD PD 10
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC8").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtHOMOCLAVE443").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_PARTI9").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCOMPANY_TEL_PARTICULAR582").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCOMPANY_MOVIL581").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCURP19").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtSEXO23").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCALLE20").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_EXT21").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_INT22").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCP14").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCOLONIA15").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24").prop("disabled", true);

                   if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCONDUCTOR_RECURRENTE576").is(':checked')) {
                       var a1 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE14").val();
                       var a2 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE25").val();
                       var a3 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtAPELLIDO_PATERNO6").val();
                       var a4 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtAPELLIDO_MATERNO7").val();
                       var RFC1 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC8").val();
                       var edad1 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24").val();
                       var genero1 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtSEXO23").val();
                       var genero = "";

                       if (genero1 == "81") { genero = "1647"; }
                       else { genero = "1648"; }

                       if (a2 != "") { a2 = a2 + " "; }
                       if (a4 != "") { a4 = " " + a4; } //Arregla los espacios

                       var edad2 = edad1.split("/");

                       calculaEdadSF(edad2[0], edad2[1], edad2[2]);

                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_COMPLETO577").prop("disabled", true).val(a1 + " " + a2 + a3 + a4);
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC578").prop("disabled", true).val(RFC1);
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtGENERO580").prop("disabled", true).val(genero);
                   }/*
                   else {
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_COMPLETO577").prop("disabled", false);//.val("");
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC578").prop("disabled", false);//.val("");
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtEDAD579").prop("disabled", false);//.val("");
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtGENERO580").prop("disabled", false);//.val("1647");
                   }*/

                   if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtTIENE_REL_PEPS569").val() == 1620) { //cambio
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtPARENTESCO_PEPS571').prop("disabled", true).val(225);
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_PEPS572').prop("disabled", true).val('-');
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtFUNCION_REL_PEPS573').prop("disabled", true).val(226);
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_REL_PEPS_OTRA584').prop("disabled", true).val('-');
                   }/* else {
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_PEPS').find(':input').prop("disabled", false);
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_PEPS').find(':input').prop("disabled", false);
                       //$('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_TERCERO575').val('');
                       //$('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_PEPS572').val('');
                   }*/

                   if ($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCURP19').val() == "AAAA999999AAAAAA99") { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCURP19').val(''); }

                   if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtHOMOCLAVE443").val() == "999") { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtHOMOCLAVE443").val(""); }

                   if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10__").val() == "9999999999") { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10").val(""); } //Arregla validar Celular


                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE14').prop("disabled", true);
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE25').prop("disabled", true);
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtAPELLIDO_PATERNO6').prop("disabled", true);
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtAPELLIDO_MATERNO7').prop("disabled", true);

                   /*$("#ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC8").prop("disabled", false);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtHOMOCLAVE443").prop("disabled", false);*/

                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtDELEGA_O_MUNICI54").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCIUDAD53").prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtESTADO51").prop("disabled", true);

                   //$("#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24").datepicker("setDate", "26/01/2017");

                   if ($('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtFUNCION_PEPS570').val() == 1633) {
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_PEPS_OTRA583').prop("disabled", false);
                   }
                   else {
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_PEPS_OTRA583').prop("disabled", true).val('-');
                   }

                   if ($('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtFUNCION_REL_PEPS573').val() == 1689) {
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_REL_PEPS_OTRA584').prop("disabled", false);
                   }
                   else {
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_REL_PEPS_OTRA584').prop("disabled", true).val('-');
                   }

                   if ($('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtACTUA_TERCERO574').val() == 1645) {
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_TERCERO575').prop("disabled", false);
                   }
                   else {
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_TERCERO575').prop("disabled", true).val('-');
                   }

                   if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_DEPENDIENTES536").val() == 'NOFX') { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_DEPENDIENTES536").val('') }

                   if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESOS_VARIABLES540").val() == '0') { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESOS_VARIABLES540").val('') }


                   
                   if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD42").val() != "" && $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD553").val() != "")
                   {
                       var anios = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD42").val().toString();
                       var meses = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD553").val().toString();

                       if (((anios == 0) && (meses < 3)) || (anios > 0)) {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_EMPLEO_ANT555').prop("disabled", true).val("");
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_EMPLEO_ANT556').prop("disabled", true).val("");
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtEXT_EMPLEO_ANT557').prop("disabled", true).val("");
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD_EMPLEO_ANT558').prop("disabled", true).val("");
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD_EMPLEO_ANT559').prop("disabled", true).val("");

                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl555\\|NOMBRE_EMPLEO_ANT").html("Nombre de la Empresa del Empleo Anterior");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl556\\|TELEFONO_EMPLEO_ANT").html("Teléfono de la Empresa Empleo Anterior");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl557\\|EXT_EMPLEO_ANT").html("Ext de la Empresa Empleo Anterior");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl558\\|ANOS_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Años) Empleo Anterior");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl559\\|MESES_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Meses) Empleo Anterior");

                           i/*f ((anios == 0) && (meses < 3)) {
                               alert('Mínimo 3 mes de antigüedad en el empleo .');
                               setTimeout(function () { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD553").val("").focus(); }, 1);
                           }*/
                           //return;
                       }

                       if ((anios == 0) && (meses > 2)) {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_EMPLEO_ANT555').prop("disabled", false).val();
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_EMPLEO_ANT556').prop("disabled", false).val();
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtEXT_EMPLEO_ANT557').prop("disabled", false).val();
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD_EMPLEO_ANT558').prop("disabled", false).val();
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD_EMPLEO_ANT559').prop("disabled", false).val();

                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl555\\|NOMBRE_EMPLEO_ANT").html("Nombre de la Empresa del Empleo Anterior *");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl556\\|TELEFONO_EMPLEO_ANT").html("Teléfono de la Empresa Empleo Anterior *");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl557\\|EXT_EMPLEO_ANT").html("Ext de la Empresa Empleo Anterior *");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl558\\|ANOS_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Años) Empleo Anterior *");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl559\\|MESES_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Meses) Empleo Anterior *");
                           //return;
                       }
                   } else {
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_EMPLEO_ANT555').prop("disabled", true).val("");
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_EMPLEO_ANT556').prop("disabled", true).val("");
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtEXT_EMPLEO_ANT557').prop("disabled", true).val("");
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD_EMPLEO_ANT558').prop("disabled", true).val("");
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD_EMPLEO_ANT559').prop("disabled", true).val("");

                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl555\\|NOMBRE_EMPLEO_ANT").html("Nombre de la Empresa del Empleo Anterior");
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl556\\|TELEFONO_EMPLEO_ANT").html("Teléfono de la Empresa Empleo Anterior");
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl557\\|EXT_EMPLEO_ANT").html("Ext de la Empresa Empleo Anterior");
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl558\\|ANOS_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Años) Empleo Anterior");
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl559\\|MESES_ANTIGUEDAD_EMPLEO_ANT").html("Antigüedad Laboral (Meses) Empleo Anterior");
                   }



                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESO_FIJO_COMPROBABLE157').prop("disabled", true);
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESO_VARIABLE_COMPROBABLE158').prop("disabled", true);

                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtTIPO_IDENTIFICACION541').prop("disabled", true);
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtN_IDENTIFICACION542').prop("disabled", true);
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCLAVE_ELECTOR543').prop("disabled", true);


                   if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtES_PEPS568").val() == 1617) {
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtFUNCION_PEPS570').prop("disabled", false);
                   } else {
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtFUNCION_PEPS570').prop("disabled", true).val('229');
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtFUNCION_PEPS_OTRA583').prop("disabled", true).val('-');
                   }

                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtFAKE_1587").parent().hide();
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtFAKE_2588").parent().hide();
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl587\\|FAKE_1").hide();
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl588\\|FAKE_2").hide();

                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtSITU_LABORAL538').prop("disabled", true);

               }

               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtDELEGA_O_MUNI17').prop("disabled", true);
               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCIUDAD16').prop("disabled", true);
               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtESTADO18').prop("disabled", true);

               if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtTIPO_IDENTIFICACION541").val() == 1493) {
                   //$('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCLAVE_ELECTOR543').prop("disabled", false).val('');
                   //$('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCLAVE_ELECTOR543').prop("disabled", false);
               } else { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCLAVE_ELECTOR543').val("").prop("disabled", true); }


               if (($("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtESTADO_CIVIL26").val() == 85) && (pantalla != "1")) {
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtREGIMEN_CONYUGAL535').prop("disabled", false); //Arregla regimen conyugal
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl535\\|REGIMEN_CONYUGAL").html("Régimen Conyugal *");
               }
               else if (pantalla != "1") {
                   $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtREGIMEN_CONYUGAL535').val('').prop("disabled", true);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_lbl535\\|REGIMEN_CONYUGAL").html("Régimen Conyugal");
               }

               if (pantalla == "1") { //BUG PD 03 habilita en pantalla 1 RFC y HOMOCLAVE
                   if ($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10').val() == "0") { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10').val(''); }
                   if ($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_PARTI9').val() == "0") { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_PARTI9').val(''); }
                   if ($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_COTIZACION441').val() == "0") { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_COTIZACION441').val(''); }
                   if ($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCP14').val() == "0") { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCP14').val(''); }

                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnImprimir").hide();

                   if ($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCURP19').val() == "AAAA999999AAAAAA99") { $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCURP19').val(''); }
                   if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtHOMOCLAVE443").val() == "999") { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtHOMOCLAVE443").val(""); }


                   //$("#ctl00_ctl00_cphCuerpo_cphPantallas_I_PDK_TAB_DATOS_PERSONALES tr:last").hide();
                   //$("#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_DATOS_PERSONALES tr:last").hide();
                   //$("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtESTADO_CIVIL26").val(85);

                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC8").prop("disabled", false);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtHOMOCLAVE443").prop("disabled", false);
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtANO_NACIMIENTO412 option:last").remove();

                   
                   $($('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtANO_NACIMIENTO412').parent()).parent().hide();
                   $($('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtMES_NACIMIENTO413').parent()).parent().hide();
                   $($('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtDIA_NACIMIENTO414').parent()).parent().hide();

                   var ena = $.urlParam("Enable").toString();
                   if (ena == "0") {
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtANO_NACIMIENTO412").prop("disabled", false);
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtMES_NACIMIENTO413").prop("disabled", false);
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtDIA_NACIMIENTO414").prop("disabled", false);
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCOLONIA15").prop("disabled", false);
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCOMPANY_MOVIL581").prop("disabled", false); //BUG PD 24
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCOMPANY_TEL_PARTICULAR582").prop("disabled", false);
                       

                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtSEXO23").prop("disabled", false);
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtESTADO_CIVIL26").prop("disabled", false);
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtVIVEN_EN27").prop("disabled", false);
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtEDAD13").prop("disabled", true);

                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtTIPO_IDENTIFICACION541').prop("disabled", false);
                       $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtSITU_LABORAL538').prop("disabled", false);
                   } else {
                       var img_dinamic = $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtTIPO_IDENTIFICACION541").val();
                       if (img_dinamic != 1493) {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtCLAVE_ELECTOR543').prop("disabled", true).val("");
                           if (img_dinamic == 1491) { $("#img_dinamic").attr("src", "../App_Themes/Imagenes/cp.png"); }
                           else if (img_dinamic == 1492) { $("#img_dinamic").attr("src", "../App_Themes/Imagenes/passport.png"); }
                           else if (img_dinamic == 1494) { $("#img_dinamic").attr("src", "../App_Themes/Imagenes/fm.png"); }
                       } else {
                           $("#img_dinamic").attr("src", "../App_Themes/Imagenes/n_iden.png");
                       }

                   }
               }

               //BUG PD 04 Verifica Num cot valido y agencia valida
               $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_COTIZACION441').focusout(function () {
                   if (($.urlParam("idPantalla").toString() == '1') && ($('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_COTIZACION441').val() != '')) {
                       var num_cot = $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_COTIZACION441').val();
                       var fol = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_SOLICITUD418").val();
                       var destino = "Blanco.aspx/validaCot";
                       var successfully = validaCot;
                       var datos = '{"num_cot":"' + num_cot.toString() + '", "folio": "' + fol.toString() + '"}';
                       jsonBack('No es posible validar el numero de cotizacion ingresado.', destino, successfully, datos);
                   }
           });

               $('input').on("change paste keyup dragend", function () {
                   if (($.urlParam("idPantalla").toString() == '1') && ($(this).attr('id').toString() != "ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_COTIZACION441")) {
                       var dict = { "á": "a", "é": "e", "í": "i", "ó": "o", "ú": "u", "Á": "A", "É": "E", "Í": "I", "Ó": "O", "Ú": "U" };
                       $(this).val($(this).val().toString().replace(/[^\w ]/g, function (char) { return dict[char] || char; }));
                   }
                   if ($.urlParam("idPantalla").toString() == '7') {
                       var dict = { "á": "a", "é": "e", "í": "i", "ó": "o", "ú": "u", "Á": "A", "É": "E", "Í": "I", "Ó": "O", "Ú": "U" };
                       $(this).val($(this).val().toString().replace(/[^\w ]/g, function (char) { return dict[char] || char; }));
                   }

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

               $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD42").blur(function(){
                   ValidarAntiguedadLaboral();
               });
               $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD553").blur(function () {
                   ValidarAntiguedadLaboral();
               });
           });

           function validaCot(response) {
               var respuesta = $.parseJSON(response.d);
               if (respuesta.cod != '1') {
                   alert(respuesta.mensaje.toString());
                   //$('#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_COTIZACION441').val('').focus();
                   setTimeout(function () { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_COTIZACION441").val("").focus(); }, 1);
               }
           }

           function removeDiacritics(str) {
               var defaultDiacriticsRemovalMap = [
                 { 'base': 'A', 'letters': /[\u0041\u24B6\uFF21\u00C0\u00C1\u00C2\u1EA6\u1EA4\u1EAA\u1EA8\u00C3\u0100\u0102\u1EB0\u1EAE\u1EB4\u1EB2\u0226\u01E0\u00C4\u01DE\u1EA2\u00C5\u01FA\u01CD\u0200\u0202\u1EA0\u1EAC\u1EB6\u1E00\u0104\u023A\u2C6F]/g },
                 { 'base': 'AA', 'letters': /[\uA732]/g },
                 { 'base': 'AE', 'letters': /[\u00C6\u01FC\u01E2]/g },
                 { 'base': 'AO', 'letters': /[\uA734]/g },
                 { 'base': 'AU', 'letters': /[\uA736]/g },
                 { 'base': 'AV', 'letters': /[\uA738\uA73A]/g },
                 { 'base': 'AY', 'letters': /[\uA73C]/g },
                 { 'base': 'B', 'letters': /[\u0042\u24B7\uFF22\u1E02\u1E04\u1E06\u0243\u0182\u0181]/g },
                 { 'base': 'C', 'letters': /[\u0043\u24B8\uFF23\u0106\u0108\u010A\u010C\u00C7\u1E08\u0187\u023B\uA73E]/g },
                 { 'base': 'D', 'letters': /[\u0044\u24B9\uFF24\u1E0A\u010E\u1E0C\u1E10\u1E12\u1E0E\u0110\u018B\u018A\u0189\uA779]/g },
                 { 'base': 'DZ', 'letters': /[\u01F1\u01C4]/g },
                 { 'base': 'Dz', 'letters': /[\u01F2\u01C5]/g },
                 { 'base': 'E', 'letters': /[\u0045\u24BA\uFF25\u00C8\u00C9\u00CA\u1EC0\u1EBE\u1EC4\u1EC2\u1EBC\u0112\u1E14\u1E16\u0114\u0116\u00CB\u1EBA\u011A\u0204\u0206\u1EB8\u1EC6\u0228\u1E1C\u0118\u1E18\u1E1A\u0190\u018E]/g },
                 { 'base': 'F', 'letters': /[\u0046\u24BB\uFF26\u1E1E\u0191\uA77B]/g },
                 { 'base': 'G', 'letters': /[\u0047\u24BC\uFF27\u01F4\u011C\u1E20\u011E\u0120\u01E6\u0122\u01E4\u0193\uA7A0\uA77D\uA77E]/g },
                 { 'base': 'H', 'letters': /[\u0048\u24BD\uFF28\u0124\u1E22\u1E26\u021E\u1E24\u1E28\u1E2A\u0126\u2C67\u2C75\uA78D]/g },
                 { 'base': 'I', 'letters': /[\u0049\u24BE\uFF29\u00CC\u00CD\u00CE\u0128\u012A\u012C\u0130\u00CF\u1E2E\u1EC8\u01CF\u0208\u020A\u1ECA\u012E\u1E2C\u0197]/g },
                 { 'base': 'J', 'letters': /[\u004A\u24BF\uFF2A\u0134\u0248]/g },
                 { 'base': 'K', 'letters': /[\u004B\u24C0\uFF2B\u1E30\u01E8\u1E32\u0136\u1E34\u0198\u2C69\uA740\uA742\uA744\uA7A2]/g },
                 { 'base': 'L', 'letters': /[\u004C\u24C1\uFF2C\u013F\u0139\u013D\u1E36\u1E38\u013B\u1E3C\u1E3A\u0141\u023D\u2C62\u2C60\uA748\uA746\uA780]/g },
                 { 'base': 'LJ', 'letters': /[\u01C7]/g },
                 { 'base': 'Lj', 'letters': /[\u01C8]/g },
                 { 'base': 'M', 'letters': /[\u004D\u24C2\uFF2D\u1E3E\u1E40\u1E42\u2C6E\u019C]/g },
                 { 'base': 'N', 'letters': /[\u004E\u24C3\uFF2E\u01F8\u0143\u00D1\u1E44\u0147\u1E46\u0145\u1E4A\u1E48\u0220\u019D\uA790\uA7A4]/g },
                 { 'base': 'NJ', 'letters': /[\u01CA]/g },
                 { 'base': 'Nj', 'letters': /[\u01CB]/g },
                 { 'base': 'O', 'letters': /[\u004F\u24C4\uFF2F\u00D2\u00D3\u00D4\u1ED2\u1ED0\u1ED6\u1ED4\u00D5\u1E4C\u022C\u1E4E\u014C\u1E50\u1E52\u014E\u022E\u0230\u00D6\u022A\u1ECE\u0150\u01D1\u020C\u020E\u01A0\u1EDC\u1EDA\u1EE0\u1EDE\u1EE2\u1ECC\u1ED8\u01EA\u01EC\u00D8\u01FE\u0186\u019F\uA74A\uA74C]/g },
                 { 'base': 'OI', 'letters': /[\u01A2]/g },
                 { 'base': 'OO', 'letters': /[\uA74E]/g },
                 { 'base': 'OU', 'letters': /[\u0222]/g },
                 { 'base': 'P', 'letters': /[\u0050\u24C5\uFF30\u1E54\u1E56\u01A4\u2C63\uA750\uA752\uA754]/g },
                 { 'base': 'Q', 'letters': /[\u0051\u24C6\uFF31\uA756\uA758\u024A]/g },
                 { 'base': 'R', 'letters': /[\u0052\u24C7\uFF32\u0154\u1E58\u0158\u0210\u0212\u1E5A\u1E5C\u0156\u1E5E\u024C\u2C64\uA75A\uA7A6\uA782]/g },
                 { 'base': 'S', 'letters': /[\u0053\u24C8\uFF33\u1E9E\u015A\u1E64\u015C\u1E60\u0160\u1E66\u1E62\u1E68\u0218\u015E\u2C7E\uA7A8\uA784]/g },
                 { 'base': 'T', 'letters': /[\u0054\u24C9\uFF34\u1E6A\u0164\u1E6C\u021A\u0162\u1E70\u1E6E\u0166\u01AC\u01AE\u023E\uA786]/g },
                 { 'base': 'TZ', 'letters': /[\uA728]/g },
                 { 'base': 'U', 'letters': /[\u0055\u24CA\uFF35\u00D9\u00DA\u00DB\u0168\u1E78\u016A\u1E7A\u016C\u00DC\u01DB\u01D7\u01D5\u01D9\u1EE6\u016E\u0170\u01D3\u0214\u0216\u01AF\u1EEA\u1EE8\u1EEE\u1EEC\u1EF0\u1EE4\u1E72\u0172\u1E76\u1E74\u0244]/g },
                 { 'base': 'V', 'letters': /[\u0056\u24CB\uFF36\u1E7C\u1E7E\u01B2\uA75E\u0245]/g },
                 { 'base': 'VY', 'letters': /[\uA760]/g },
                 { 'base': 'W', 'letters': /[\u0057\u24CC\uFF37\u1E80\u1E82\u0174\u1E86\u1E84\u1E88\u2C72]/g },
                 { 'base': 'X', 'letters': /[\u0058\u24CD\uFF38\u1E8A\u1E8C]/g },
                 { 'base': 'Y', 'letters': /[\u0059\u24CE\uFF39\u1EF2\u00DD\u0176\u1EF8\u0232\u1E8E\u0178\u1EF6\u1EF4\u01B3\u024E\u1EFE]/g },
                 { 'base': 'Z', 'letters': /[\u005A\u24CF\uFF3A\u0179\u1E90\u017B\u017D\u1E92\u1E94\u01B5\u0224\u2C7F\u2C6B\uA762]/g },
                 { 'base': 'a', 'letters': /[\u0061\u24D0\uFF41\u1E9A\u00E0\u00E1\u00E2\u1EA7\u1EA5\u1EAB\u1EA9\u00E3\u0101\u0103\u1EB1\u1EAF\u1EB5\u1EB3\u0227\u01E1\u00E4\u01DF\u1EA3\u00E5\u01FB\u01CE\u0201\u0203\u1EA1\u1EAD\u1EB7\u1E01\u0105\u2C65\u0250]/g },
                 { 'base': 'aa', 'letters': /[\uA733]/g },
                 { 'base': 'ae', 'letters': /[\u00E6\u01FD\u01E3]/g },
                 { 'base': 'ao', 'letters': /[\uA735]/g },
                 { 'base': 'au', 'letters': /[\uA737]/g },
                 { 'base': 'av', 'letters': /[\uA739\uA73B]/g },
                 { 'base': 'ay', 'letters': /[\uA73D]/g },
                 { 'base': 'b', 'letters': /[\u0062\u24D1\uFF42\u1E03\u1E05\u1E07\u0180\u0183\u0253]/g },
                 { 'base': 'c', 'letters': /[\u0063\u24D2\uFF43\u0107\u0109\u010B\u010D\u00E7\u1E09\u0188\u023C\uA73F\u2184]/g },
                 { 'base': 'd', 'letters': /[\u0064\u24D3\uFF44\u1E0B\u010F\u1E0D\u1E11\u1E13\u1E0F\u0111\u018C\u0256\u0257\uA77A]/g },
                 { 'base': 'dz', 'letters': /[\u01F3\u01C6]/g },
                 { 'base': 'e', 'letters': /[\u0065\u24D4\uFF45\u00E8\u00E9\u00EA\u1EC1\u1EBF\u1EC5\u1EC3\u1EBD\u0113\u1E15\u1E17\u0115\u0117\u00EB\u1EBB\u011B\u0205\u0207\u1EB9\u1EC7\u0229\u1E1D\u0119\u1E19\u1E1B\u0247\u025B\u01DD]/g },
                 { 'base': 'f', 'letters': /[\u0066\u24D5\uFF46\u1E1F\u0192\uA77C]/g },
                 { 'base': 'g', 'letters': /[\u0067\u24D6\uFF47\u01F5\u011D\u1E21\u011F\u0121\u01E7\u0123\u01E5\u0260\uA7A1\u1D79\uA77F]/g },
                 { 'base': 'h', 'letters': /[\u0068\u24D7\uFF48\u0125\u1E23\u1E27\u021F\u1E25\u1E29\u1E2B\u1E96\u0127\u2C68\u2C76\u0265]/g },
                 { 'base': 'hv', 'letters': /[\u0195]/g },
                 { 'base': 'i', 'letters': /[\u0069\u24D8\uFF49\u00EC\u00ED\u00EE\u0129\u012B\u012D\u00EF\u1E2F\u1EC9\u01D0\u0209\u020B\u1ECB\u012F\u1E2D\u0268\u0131]/g },
                 { 'base': 'j', 'letters': /[\u006A\u24D9\uFF4A\u0135\u01F0\u0249]/g },
                 { 'base': 'k', 'letters': /[\u006B\u24DA\uFF4B\u1E31\u01E9\u1E33\u0137\u1E35\u0199\u2C6A\uA741\uA743\uA745\uA7A3]/g },
                 { 'base': 'l', 'letters': /[\u006C\u24DB\uFF4C\u0140\u013A\u013E\u1E37\u1E39\u013C\u1E3D\u1E3B\u017F\u0142\u019A\u026B\u2C61\uA749\uA781\uA747]/g },
                 { 'base': 'lj', 'letters': /[\u01C9]/g },
                 { 'base': 'm', 'letters': /[\u006D\u24DC\uFF4D\u1E3F\u1E41\u1E43\u0271\u026F]/g },
                 { 'base': 'n', 'letters': /[\u006E\u24DD\uFF4E\u01F9\u0144\u00F1\u1E45\u0148\u1E47\u0146\u1E4B\u1E49\u019E\u0272\u0149\uA791\uA7A5]/g },
                 { 'base': 'nj', 'letters': /[\u01CC]/g },
                 { 'base': 'o', 'letters': /[\u006F\u24DE\uFF4F\u00F2\u00F3\u00F4\u1ED3\u1ED1\u1ED7\u1ED5\u00F5\u1E4D\u022D\u1E4F\u014D\u1E51\u1E53\u014F\u022F\u0231\u00F6\u022B\u1ECF\u0151\u01D2\u020D\u020F\u01A1\u1EDD\u1EDB\u1EE1\u1EDF\u1EE3\u1ECD\u1ED9\u01EB\u01ED\u00F8\u01FF\u0254\uA74B\uA74D\u0275]/g },
                 { 'base': 'oi', 'letters': /[\u01A3]/g },
                 { 'base': 'ou', 'letters': /[\u0223]/g },
                 { 'base': 'oo', 'letters': /[\uA74F]/g },
                 { 'base': 'p', 'letters': /[\u0070\u24DF\uFF50\u1E55\u1E57\u01A5\u1D7D\uA751\uA753\uA755]/g },
                 { 'base': 'q', 'letters': /[\u0071\u24E0\uFF51\u024B\uA757\uA759]/g },
                 { 'base': 'r', 'letters': /[\u0072\u24E1\uFF52\u0155\u1E59\u0159\u0211\u0213\u1E5B\u1E5D\u0157\u1E5F\u024D\u027D\uA75B\uA7A7\uA783]/g },
                 { 'base': 's', 'letters': /[\u0073\u24E2\uFF53\u00DF\u015B\u1E65\u015D\u1E61\u0161\u1E67\u1E63\u1E69\u0219\u015F\u023F\uA7A9\uA785\u1E9B]/g },
                 { 'base': 't', 'letters': /[\u0074\u24E3\uFF54\u1E6B\u1E97\u0165\u1E6D\u021B\u0163\u1E71\u1E6F\u0167\u01AD\u0288\u2C66\uA787]/g },
                 { 'base': 'tz', 'letters': /[\uA729]/g },
                 { 'base': 'u', 'letters': /[\u0075\u24E4\uFF55\u00F9\u00FA\u00FB\u0169\u1E79\u016B\u1E7B\u016D\u00FC\u01DC\u01D8\u01D6\u01DA\u1EE7\u016F\u0171\u01D4\u0215\u0217\u01B0\u1EEB\u1EE9\u1EEF\u1EED\u1EF1\u1EE5\u1E73\u0173\u1E77\u1E75\u0289]/g },
                 { 'base': 'v', 'letters': /[\u0076\u24E5\uFF56\u1E7D\u1E7F\u028B\uA75F\u028C]/g },
                 { 'base': 'vy', 'letters': /[\uA761]/g },
                 { 'base': 'w', 'letters': /[\u0077\u24E6\uFF57\u1E81\u1E83\u0175\u1E87\u1E85\u1E98\u1E89\u2C73]/g },
                 { 'base': 'x', 'letters': /[\u0078\u24E7\uFF58\u1E8B\u1E8D]/g },
                 { 'base': 'y', 'letters': /[\u0079\u24E8\uFF59\u1EF3\u00FD\u0177\u1EF9\u0233\u1E8F\u00FF\u1EF7\u1E99\u1EF5\u01B4\u024F\u1EFF]/g },
                 { 'base': 'z', 'letters': /[\u007A\u24E9\uFF5A\u017A\u1E91\u017C\u017E\u1E93\u1E95\u01B6\u0225\u0240\u2C6C\uA763]/g }
               ];
               for (var i = 0; i < defaultDiacriticsRemovalMap.length; i++) { str = str.replace(defaultDiacriticsRemovalMap[i].letters, defaultDiacriticsRemovalMap[i].base); }
               return str;
           }

           function llenarfechaCoa() {
               var hoy = new Date();
               var mes = hoy.getMonth();
               var dia = hoy.getDay();
               var annio = hoy.getFullYear();
               // llena ddl MES
               var month = '1';
               var ddl = $("[id$=ddltxtMES_NACIMIENTO416]")[0];
               while (month <= 12) {
                   valorOpc = new Option;
                   if (month < 10) {
                       valorOpc.text = '0' + month;
                       valorOpc.value = '0' + month;
                   }
                   else {
                       valorOpc.text = month;
                       valorOpc.value = month;
                   }
                   insertaddl(month - 1, valorOpc, ddl)
                   month++
               }
               // llena ddl ANNIO
               var inicio = annio - 100;
               var i = 0;
               var ddl = $("[id$=ddltxtANO_NACIMIENTO415]")[0];
               while (inicio <= annio) {
                   valorOpc = new Option;
                   valorOpc.text = inicio;
                   valorOpc.value = inicio;
                   insertaddl(i, valorOpc, ddl)
                   i++
                   inicio++
               }
               llenaddlDiascoa();

           }

           function llenarfecha() {
               var hoy = new Date();
               var mes = hoy.getMonth();
               var dia = hoy.getDay();
               var annio = hoy.getFullYear();
               // llena ddl MES
               var month = '1';
               var ddl = $("[id$=ddltxtMES_NACIMIENTO413]")[0];
               while (month <= 12) {
                   valorOpc = new Option;
                   if (month < 10) {
                       valorOpc.text = '0' + month;
                       valorOpc.value = '0' + month;
                   }
                   else {
                       valorOpc.text = month;
                       valorOpc.value = month;
                   }
                   insertaddl(month - 1, valorOpc, ddl)
                   month++
               }
               // llena ddl ANNIO
               var inicio = annio - 100;
               var i = 0;
               var ddl = $("[id$=ddltxtANO_NACIMIENTO412]")[0];
               while (inicio <= annio) {
                   valorOpc = new Option;
                   valorOpc.text = inicio;
                   valorOpc.value = inicio;
                   insertaddl(i, valorOpc, ddl)
                   i++
                   inicio++
               }
               llenaddlDias();



           }

           function insertaddl(id, valorOpc, ddl) {
               try{
               ddl.options[id] = valorOpc;
               }
               catch (msg) {
                   var error = msg;
               }
           }

           function daysInMonth(humanMonth, year) {
               return new Date(year || new Date().getFullYear(), humanMonth, 0).getDate();
           }

           function llenaddlDias() {
               // llena ddl day
               i = 1;
               var ddl = $("[id$=ddltxtDIA_NACIMIENTO414]");
               var sb;
               var mes = $("[id$=ddltxtMES_NACIMIENTO413]").val();
               var annio = $("[id$=ddltxtANO_NACIMIENTO412]").val();
               var numdias = daysInMonth(mes, annio);
               ddl.find("option").remove();
               while (i <= numdias) {
                   if (i < 10) {
                       sb += "<option value='0" + i + "'>0" + i + "</option>";
                   }
                   else {
                       sb += "<option value='" + i + "'>" + i + "</option>";
                   }
                   i++
               }
               ddl.append(sb);

           }

           function llenaddlDiascoa() {
               // llena ddl day
               i = 1;
               var ddl = $("[id$=ddltxtDIA_NACIMIENTO417]");
               var sb;
               var mes = $("[id$=ddltxtMES_NACIMIENTO416]").val();
               var annio = $("[id$=ddltxtANO_NACIMIENTO415]").val();
               var numdias = daysInMonth(mes, annio);
               ddl.find("option").remove();
               while (i <= numdias) {
                   if (i < 10) {
                       sb += "<option value='0" + i + "'>0" + i + "</option>";
                   }
                   else {
                       sb += "<option value='" + i + "'>" + i + "</option>";
                   }
                   i++
               }
               ddl.append(sb);


           }

           function calculaEdadSF(dia, mes, anio) {
               var edad = 0;
               var hoy = new Date();
               var añoshoy = hoy.getFullYear();
               var meshoy = hoy.getMonth() + 1;
               var diahoy = hoy.getDate();

               var fecha = mes + '-' + dia + '-' + anio;
               var fechaNew = anio + "/" + mes + "/" + dia;
               fecha = new Date(fecha);
               var edad = parseInt((hoy - fecha) / 365.25 / 24 / 60 / 60 / 1000)

               var age = getAge(new Date(fechaNew));
               $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtEDAD579").prop("disabled", true).val(age);

           }

           function calculaEdad() {
               var edad = 0;
               var hoy = new Date();
               var añoshoy = hoy.getFullYear();
               var meshoy = hoy.getMonth() + 1;
               var diahoy = hoy.getDate() ;
               var annio = $("[id$=ddltxtANO_NACIMIENTO412]").val();
               var mes = $("[id$=ddltxtMES_NACIMIENTO413]").val();
               var dianacimiento = $("[id$=ddltxtDIA_NACIMIENTO414]").val()
               var fecha = mes + '-' + dianacimiento + '-' + annio
               var fechaNew = annio + "/" + mes + "/" + dianacimiento
               fecha = new Date(fecha);
               var edad = parseInt((hoy - fecha) / 365.25 / 24 / 60 / 60 / 1000)

               var age = getAge(new Date(fechaNew));
               $("[id$=txtEDAD13]").val(age);
               //alert(age.toString());
               //            var edad = (añoshoy + 1900) - annio;
               //            if (meshoy < (mes - 1)) {
               //                edad--;
               //            }

               //            if (((mes - 1) == meshoy) && (diahoy < dianacimiento)) {
               //                edad--;
               //            }
               //            if (edad > 1900) {
               //                edad -= 1900;

               //            }
               //$("[id$=txtEDAD13]").val(edad);


           }

           function getAge(dateString) {
               var today = new Date();
               var birthDate = new Date(dateString);
               var age = today.getFullYear() - birthDate.getFullYear();
               var m = today.getMonth() - birthDate.getMonth();
               if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                   age--;
               }
               return age;
           }

           function calculaEdadcoa() {
               var edad = 0;
               var hoy = new Date();
               var añoshoy = hoy.getFullYear();
               var meshoy = hoy.getMonth() + 1;
               var diahoy = hoy.getDate();
               var annio = $("[id$=ddltxtANO_NACIMIENTO415]").val();
               var mes = $("[id$=ddltxtMES_NACIMIENTO416]").val();
               var dianacimiento = $("[id$=ddltxtDIA_NACIMIENTO417]").val()
               var fecha = mes + '-' + dianacimiento + '-' + annio
               fecha = new Date(fecha);
               var edad = parseInt((hoy - fecha) / 365 / 24 / 60 / 60 / 1000)
               //            var edad = (añoshoy + 1900) - annio;
               //            if (meshoy < (mes - 1)) {
               //                edad--;
               //            }

               //            if (((mes - 1) == meshoy) && (diahoy < dianacimiento)) {
               //                edad--;
               //            }
               //            if (edad > 1900) {
               //                edad -= 1900;

               //            }
               $("[id$=txtEDAD_COAC105]").val(edad);


           }

           function llenado(id) {
               if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCP14").val() == "") { return; }
               var verifiTel = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCP14").val();
               if (verifiTel.length < 5) {
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtDELEGA_O_MUNI17").val("");
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCIUDAD16").val("");
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtESTADO18").val("");
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCOLONIA15").empty();
                   alert('El Código Postal debe ser de 5 digitos.');
                   setTimeout(function () { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCP14").val("").focus(); }, 1);
                   return;
               }

               var codi = $(id).val();
               if (codi == "") { codi = "0"; }
               $("[id$=hdnCp]").val(codi);
               fillddl('txtCOLONIA15', 'hdnCp');
               filltxt('DELEGA_O_MUNI17', 'hdnCp');
               filltxt('ESTADO18', 'hdnCp');
               filltxt('CIUDAD16', 'hdnCp');

               if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCP14").val() != "") {
                   var d1 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtDELEGA_O_MUNI17").val().toString();
                   var d2 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCIUDAD16").val().toString();
                   var d3 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtESTADO18").val().toString();

                   if ((d1 == "") || (d2 == "") || (d3 == "")) {
                       //alert("El Código Postal no existe.");
                       alert("Código Postal Invalido");
                       setTimeout(function () { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCP14").val("").focus(); }, 1);
                   }
               }
           }

           function llenadoEmp(id) {
               if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCP50").val() == "") { return; }
               var verifiTel = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCP50").val();
               if (verifiTel.length < 5) {
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtDELEGA_O_MUNICI54").val("");
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCIUDAD53").val("");
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtESTADO51").val("");
                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCOLONIA52").empty();
                   alert('El CP debe ser de 5 digitos.');
                   setTimeout(function () { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCP50").val("").focus(); }, 1);
                   return;
               }

               var codi = $(id).val();
               if (codi == "") { codi = "0"; }
               $("[id$=hdnCp1]").val(codi);
               fillddl('txtCOLONIA52', 'hdnCp1');
               filltxt('DELEGA_O_MUNICI54', 'hdnCp1');
               filltxt('ESTADO51', 'hdnCp1');
               filltxt('CIUDAD53', 'hdnCp1');

               if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCP50").val() != "") {
                   var d1 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtDELEGA_O_MUNICI54").val().toString();
                   var d2 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCIUDAD53").val().toString();
                   var d3 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtESTADO51").val().toString();

                   if ((d1 == "") || (d2 == "") || (d3 == "")) {
                       //alert("El CP no existe.");
                       alert("CP Invalido");
                       setTimeout(function () { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCP50").val("").focus(); }, 1);
                   }
               }
           }

           function llenadoCoa(id) {
               var codi = $(id).val();
               $("[id$=hdnCp2]").val(codi);
               fillddl('txtCOLONIA85', 'hdnCp2');
               filltxt('DELEGA_O_MUNI87', 'hdnCp2');
               filltxt('ESTADO88', 'hdnCp2');
               filltxt('CIUDAD86', 'hdnCp2');


           }

           function llenarcoaemp(id) {
               var codi = $(id).val();
               $("[id$=hdnCp3]").val(codi);
               fillddl('txtCOLONIA122', 'hdnCp3');
               filltxt('DELEGA_O_MUNI124', 'hdnCp3');
               filltxt('ESTADO120', 'hdnCp3');
               filltxt('CIUDAD123', 'hdnCp3');

           }

           function llenarpersonamoral(id) {
               var codi = $(id).val();
               $("[id$=hdnCp4]").val(codi);
               fillddl('txtCOLONIA458', 'hdnCp4');
               filltxt('DELEGA_O_MUNI459', 'hdnCp4');
               filltxt('ESTADO460', 'hdnCp4');
               filltxt('CIUDAD461', 'hdnCp4');

           }

           function validarcorreo(id) {
               var valorMail = $(id).val();

               if (valorMail == "") { return; }

               if (valorMail.indexOf("@") < 0) { alert('Formato de Correo Electrónico invalido.'); setTimeout(function () { $(id).val("").focus(); }, 1); return; }

               var mail = valorMail.split("@");

               if (mail.lenght < 2) { alert('Formato de Correo Electrónico invalido.'); setTimeout(function () { $(id).val("").focus(); }, 1); return; }

               var domain = mail[1].split(".");

               if (domain.lenght < 2) { alert('Formato de Correo Electrónico invalido.'); setTimeout(function () { $(id).val("").focus(); }, 1); return; }

               var domainname = domain[0].length;
               var local_part = mail[0].length;
               if ((local_part < 3) || (domainname < 3)) {
                   alert('Longitud de Correo Electrónico no valida.');
                   setTimeout(function () { $(id).val("").focus(); }, 1);
               } else if (valorMail != "") {
                   var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                   var valido = re.test($(id).val());
                   if (!valido) {
                       alert('Formato de Correo Electrónico invalido.');
                       setTimeout(function () { $(id).val("").focus(); }, 1);
                   }
               }
           }

           function armaJson(finalizar) {
               var folio = $.urlParam("idFolio").toString();
               var pantalla = $.urlParam("idPantalla").toString();

               var tables = $('table', '#ctl00_ctl00_cphCuerpo_cphPantallas_pantalla');
               var jsonTbls = {};
               jsonTbls["Tablas"] = [];
               $.each(tables, function (table) {
                   if ($('#' + tables[table].id).css('display') != 'none') {
                       var jsonTbl = {};
                       jsonTbl["tableName"] = tables[table].id;
                       jsonTbl["Folio"] = folio.toString();
                       jsonTbl["Finalizar"] = finalizar;
                       jsonTbl["field"] = [];
                       var tds = $('tr td', '#' + tables[table].id);
                       for (var i = 0; i < tds.length; i += 2) {
                           var item = {};
                           var span = tds[i].children;
                           var input = tds[i + 1].children;
                           var tipo = $('#' + input[0].id);
                           var tipoInput = "";
                           var valorInput = "";
                           if (tipo.is('input[type=text]')) {
                               tipoInput = "text";
                               valorInput = $('#' + input[0].id).val();
                           } else if (tipo.is('input[type=date]')) {
                               tipoInput = "date";
                               valorInput = $('#' + input[0].id).val();
                           } else if (tipo.is('select')) {
                               tipoInput = "select";
                               valorInput = $("#" + input[0].id + " option:selected").text();
                           } else if (tipo.is('input[type=checkbox]')) {
                               tipoInput = "check";
                               valorInput = $("#" + input[0].id).is(":checked");
                           }
                           item["nameField"] = input[0].id;
                           item["valueField"] = valorInput;
                           item["textField"] = span[0].innerText;
                           item["tipo"] = tipoInput
                           jsonTbl.field.push(item);
                       }
                       jsonTbls.Tablas.push(jsonTbl);
                   }
               });
               var jsonString = JSON.stringify(jsonTbls, 'Tablas');
               JSONProccess(jsonString, finalizar);
           }

           function validaPantalla() {
               var tables = $('table', '#ctl00_ctl00_cphCuerpo_cphPantallas_pantalla');
               var jsonValFields = {};
               jsonValFields["mensaje"] = [];

               $.each(tables, function (table) {
                   if ($('#' + tables[table].id).css('display') != 'none') {
                       var tds = $('tr td', '#' + tables[table].id);
                       var bandera = 0;
                       for (var i = 0; i < tds.length; i += 2) {
                           var item = {};
                           var span = tds[i].children;
                           var input = tds[i + 1].children;
                           var tipo = $('#' + input[0].id);
                           var valorInput = "";
                           if (tipo.is('input[type=text]')) { valorInput = $('#' + input[0].id).val(); }
                           else if (tipo.is('input[type=date]')) { valorInput = $('#' + input[0].id).val(); }
                           else if (tipo.is('select')) { valorInput = $("#" + input[0].id + " option:selected").text(); }
                           else if (tipo.is('input[type=checkbox]')) { valorInput = $("#" + input[0].id).is(":checked"); }
                           
                           var strr = span[0].innerText.toString()
                           if  ( ((valorInput == "") && (span[0].innerText.toString().indexOf('*') > -1))
                                 ||
                                 ((valorInput == "SELECCIONAR ESTADO") && (span[0].innerText.toString().indexOf('*') > -1))
                                 ||
                                 ((valorInput == "SELECCIONA UNA CIUDAD") && (span[0].innerText.toString().indexOf('*') > -1))
                                 ||
                                 ((valorInput == "< SELECCIONAR >") && (span[0].innerText.toString().indexOf('*') > -1))
                                 ||
                                 ((valorInput == "0-cancelado") && (span[0].innerText.toString().indexOf('*') > -1))
                                 ||
                                 ((valorInput.toString().toUpperCase() == "ERROR") && (span[0].innerText.toString().indexOf('*') > -1))) {
                               if (bandera == 0) {
                                   var myTable = $('#' + tables[table].id);
                                   var myCaptionText = myTable.find('caption').text();
                                   var myCaptionText1 = myTable.find('caption').html();

                                   if (myCaptionText1 != undefined) {
                                       item = "\n" + $.trim(myCaptionText) + "\n";
                                       jsonValFields.mensaje.push(item);
                                   }
                                   bandera = 1;
                               }

                               item = ' - El campo "' + span[0].innerText.replace('*', '') + '" es obligatorio';
                               jsonValFields.mensaje.push(item);
                           }
                       }

                   }
               });
               var jsonString = JSON.stringify(jsonValFields);
               return jsonString;
           }

           function btnGuardar(id) {
               if ($.inArray(idPantalla, permiteCopyPaste) >= 0) {
               if (!globalValidate()) {
                   alert('Algunos de los campos contienen errores, imposible continuar.');
                   $('input[type="button"]:disabled').prop('disabled', false);
                   return false;
               }
               }

               $("#divcancela").hide();
               if (($.urlParam("idPantalla").toString() == "7") && (id != 'ctl00_ctl00_cphCuerpo_cphPantallas_btnGuardarCancelar')) {
                   var limite = limiteUpdate($.urlParam("idFolio").toString());
                   if (limite == 1) {
                       alert("El límite de actualizaciones para esta solicitud se ha excedido.");
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", false);
                       return;
                   }

                    var msg = validaPantalla();
                    if (msg.length > 14) {
                        var msgs = $.parseJSON(msg);
                        var listaMsg = "";
                        $.each(msgs, function (i, m) { listaMsg = msgs[i].toString().split(',').join('\n'); });
                        alert(listaMsg.toString());
                        $("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", false);
                        return;
                    }

                    if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD42").val() == "0") {
                        $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD42").val("0");
                    }

                    //debugger;

                    /*var telefonoFijo = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_PARTI9").val();
                    if (telefonoFijo.length < 10) {
                        alert("El teléfono fijo debe tener 10 dígitos.");
                        $("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", false);
                        return;
                    }*/

                    var telefono1 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10").val();
                    var telefono2 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO40").val();
                    var telefono3 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO562").val();
                    var telefono4 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO565").val();

                    if ($('#ctl00_ctl00_cphCuerpo_cphPantallas_U_PDK_TAB_REFERE_PERSONALES:visible').length == 0) {
                        //alert("telefono oculto");
                        telefono5 = "9999999999";
                    } else {
                        //alert("Telefono no Oculto");
                        var telefono5 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO163").val();
                    }

                    if ((telefono1.length < 10) || (telefono2.length < 10) || (telefono3.length < 10) || (telefono4.length < 10) || (telefono5.length < 10)) {
                        var mensajetel = "Alguno de los teléfonos no tienen 10 caracteres.\n\n";
                        
                        if (telefono1.length < 10) { mensajetel = mensajetel + "El Teléfono Móvil\n"; }
                        if (telefono2.length < 10) { mensajetel = mensajetel + "El Teléfono en la sección Datos de Empleo\n"; }
                        if (telefono3.length < 10) { mensajetel = mensajetel + "El Teléfono (Conocido)\n"; }
                        if (telefono4.length < 10) { mensajetel = mensajetel + "El Teléfono (Familiar)\n"; }
                        if (telefono5.length < 10) { mensajetel = mensajetel + "El Teléfono (Arrendador)\n"; }

                        alert(mensajetel.toString());
                        $("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", false);
                        return;
                    }

                    var ciudad = $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCIUDAD547 option:selected").text();
                    if (ciudad.toString() == "< SELECCIONAR >") {
                        alert("Debes seleccionar una Ciudad.");
                        $("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", false);
                        return;
                    }

                    if (telefono5 == "9999999999") {
                        $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO163").val("0");
                    }

                    var dFake = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtPDK_FECHA_MODIF531").val();
                    var dFakeArray = dFake.split("/");
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtPDK_FECHA_MODIF531").val(dFakeArray[2] + "-" + dFakeArray[1] + "-" + dFakeArray[0]);

                    armaJson(1);
               } else {
                   if (($.urlParam("idPantalla").toString() == '1') && (id != 'ctl00_ctl00_cphCuerpo_cphPantallas_btnGuardarCancelar')) {
                       var msg = validaPantalla();
                       if (msg.length > 14) {
                           var msgs = $.parseJSON(msg);
                           var listaMsg = "";
                           $.each(msgs, function (i, m) { listaMsg = msgs[i].toString().split(',').join('\n'); });
                           alert(listaMsg.toString());
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", false);
                           return;
                       }

                       /*var telefonoFijo = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_PARTI9").val();
                       if (telefonoFijo.length < 10) {
                           alert("El teléfono fijo debe tener 10 dígitos.");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", false);
                           return;
                       }*/
                       var telFijo = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_PARTI9").val();
                       var telMov = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10").val();
                       if ((telFijo == telMov) && (telFijo.length > 0)) {
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10").val("");
                           //$("body").focus();
                           alert("El teléfono móvil y fijo no pueden ser iguales.");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", false);
                           return;
                       }

                       var fijos = parseInt($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESO_FIJO_COMPROBABLE157").val(), 10);
                       var variables = parseInt($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESO_VARIABLE_COMPROBABLE158").val(), 10);
                       if ((fijos > 99999999) || (variables > 99999999)) {
                           var ingresos_var = "";
                           if ((fijos > 99999999) && (variables > 99999999)) {
                               ingresos_var = "fijos y variables máximos.";
                           } else if (fijos > 99999999) {
                               ingresos_var = "fijos máximos.";
                           } else {
                               ingresos_var = "variables máximos.";
                           }
                           alert('Favor de verificar los ingresos ' + ingresos_var.toString());
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", false);
                           return;
                       }

                       if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESO_FIJO_COMPROBABLE157").val().toString() == "") {
                           alert("El campo Ingresos Fijos debe ser mínimo 0.");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", false);
                           return;
                       } else if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESO_VARIABLE_COMPROBABLE158").val().toString() == "") {
                           alert("El campo Ingresos Variables debe ser mínimo 0.");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", false);
                           return;
                       }

                       if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtRENOVACION_CI589").is(':checked')) {
                           var contrato = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNUM_CONTRATO_CI590").val();
                           var nombre = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_SOLICI419").val();
                           var resp = contratoCI(contrato, nombre);
                           if (resp != "OK") {
                               alert(resp.toString());
                               $("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", false);
                               return;
                           } else {
                               var respWS = WScontratoCI(contrato, 0, nombre);
                               if (respWS != "OK") {
                                   alert(respWS.toString());
                                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_Button1").prop("disabled", false);
                                   return;
                               }
                           }
                       }
                   }
                   
                   //if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCURP19").val() == "") { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtCURP19").val("AAAA999999AAAAAA99").attr('style', 'width: 170px !important; color: #ffffff !important;'); }
                   //if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtHOMOCLAVE443").val() == "") { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtHOMOCLAVE443").val("999").attr('style', 'width: 170px !important; color: #ffffff !important;'); }
                   //if ($("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10").val() == "") { $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtTELEFONO_MOVIL10").val("9999999999").attr('style', 'width: 170px !important; color: #ffffff !important;'); }

                   $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtMENSUALIDAD131").val(0)

                   var tabla;
                   var into = "";
                   var values = "";
                   var upda = "";
                   var folio = $("[id$=hdnFolio] ")
                   var usuario = $("[id$=hdnUsuario] ")
                   var pantalla = $("[id$=hdnPantalla] ")
                   var cadena = "";
                   var cadenaUp = "";
                   var tablas = "";
                   var f = folio.val();
                   var u = usuario.val();
                   var valida = "";
                   var mensaje = "";
                   var mensaje1 = "";
                   var cvepantalla = $("[id$=hdnIdRegistro]").val();
                   var pantalla = pantalla.val();
                   var botones = id.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
                   var validCREDITO = $('[id$=hdvalidad]').val();
                   var validadrenta = $('[id$=hdnValiRenta]').val();
                   var validadRenta2 = $('[id$=hdnValiRenta1]').val();
                   var validaCrediCoa = $('[id$=hdvalidadCoa]').val();
                   if (validCREDITO != '') { mensaje = validCREDITO }
                   if (validadrenta != '') { mensaje += validadrenta }
                   if (validadRenta2 != '') { mensaje += validadRenta2 }
                   if (validaCrediCoa != '') { mensaje += validaCrediCoa }


                   var mater = $("[id$=ctl00_ctl00_cphCuerpo_cphPantallas_pantalla] >table")
                   mater.each(function (index) {
                       tabla = $(this).attr('id')
                       valida = tabla
                       tabla = tabla.replace('ctl00_ctl00_cphCuerpo_cphPantallas_U_', '');
                       tabla = tabla.replace('ctl00_ctl00_cphCuerpo_cphPantallas_I_', '');
                       valida = valida.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
                       valida = valida.replace(tabla, '');
                       valida = valida.replace('_', '');

                       $(this).children("caption").each(function (index2) { tablas += $(this).text() })//traer el nombre de la tabla 
                       var newtb = $("[id$=" + tabla + "] tbody tr")

                       newtb.each(function (index) {


                           $(this).children("td").each(function (index3) {

                               $(this).children("table").each(
                                function (index8) {
                                    var tablahijo = $(this).attr('id')
                                    var newtabhijo = $("[id$=" + tablahijo + "] tbody tr")
                                    newtabhijo.each(function (index9) {
                                        $(this).children("td").each(function (index10) {
                                            $(this).children("input").each(function (index11) {
                                                var chek1 = $(this).attr("type");
                                                if (chek1 == "radio") {
                                                    var radio = $(this).is(':checked');
                                                    if (radio == true) {

                                                        if (valida == "I") {
                                                            values += "'" + $(this).attr("value") + "',"
                                                        } else {
                                                            upda += "'" + $(this).attr("value") + "',"
                                                        }


                                                    }
                                                }

                                            })

                                        })


                                    })

                                })

                               $(this).children("span").each(
                                   function (index4) {
                                       var txt1 = $(this)[0].id
                                       var matritxt = txt1.split("|");

                                       if (valida == "I") {
                                           //                                into += $(this).text() + ','
                                           into += matritxt[1] + ','
                                       }
                                       else {
                                           //                                upda += $(this).text() + '='
                                           upda += matritxt[1] + '='
                                       }
                                   })
                               $(this).children("input").each(
                                   function (index5) {
                                       var texto = $(this).val();
                                       var check = $(this).attr("type");


                                       //                           alert($(this).attr("value") + ' ' + $(this).attr("name"));


                                       if (check == "checkbox") {
                                           var chkbox = $(this).is(':checked');

                                           if (chkbox == true) { texto = "SI" } else { mensaje1 += $(this)[0].name; texto = "NO" }
                                           if (valida == "I") {
                                               values += "'" + texto + "',"
                                           } else {
                                               upda += "'" + texto + "',"
                                           }

                                       } else if (check == "text") {

                                           if (texto == '' || texto == 0) { mensaje += $(this)[0].name; }
                                           if (valida == "I") {
                                               values += "'" + texto + "',"
                                           } else {
                                               upda += "'" + texto + "',"
                                           }
                                       }
                                       //                            var chenk = $(this).is(':checked');

                                   })



                               $(this).children("select").each(function (index6) {
                                   var sele = $(this).attr('id')
                                   //                        sele = sele.repalce('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
                                   lista = $("[id$=" + sele + "] option:selected");
                                   //                        alert(lista.text());
                                   if (valida == "I") {
                                       values += "'" + lista.text() + "',"
                                   } else {
                                       upda += "'" + lista.text() + "',"
                                   }



                               })
                               $(this).children("textarea").each(function (index7) {

                                   var texto = $(this).val();
                                   if ((cvepantalla != 4) && (cvepantalla != 1)) { if (texto == '' || texto == 0) { mensaje += $(this)[0].name; } }

                                   if (valida == "I") {
                                       values += "'" + texto + "',"
                                   } else {
                                       upda += "'" + texto + "',"
                                   }


                               })

                               ////(this).attr("value")
                               //$(this).children("span").each(function (index4) { into += $(this).text() + ',' })
                               //$(this).children("input").each(function (index5) { values += "'" + $(this).attr("value") + "'," })

                           })


                       })

                       if (valida == "I") {
                           cadena += ' INSERT INTO ' + tabla + '(' + into + 'PDK_ID_SECCCERO' + ',' + 'PDK_CLAVE_USUARIO' + ',' + 'PDK_FECHA_MODIF) VALUES (' + values + f + ',' + u + ',' + 'getdate()' + ')'
                       } else {
                           cadenaUp += '  UPDATE ' + tabla + ' ' + 'SET' + ' ' + upda + ' PDK_CLAVE_USUARIO=' + u + ',' + ' PDK_FECHA_MODIF=' + 'getdate()' + ' WHERE PDK_ID_SECCCERO=' + f + ' '

                       }

                       //            remple = tablas.replace('TAB', 'ID');
                       //            remple = remple.replace(' ', '') + ',';            
                       //                        
                       //            into = into.replace(remple, '');
                       //            alert(into);

                       //cadena += ' INSERT INTO ' + tabla + '(' + into + 'PDK_ID_SECCCERO' + ',' + 'PDK_CLAVE_USUARIO' + ',' + 'PDK_FECHA_MODIF) VALUES (' + values + f + ',' + u + ',' + 'getdate()' + ')'
                       tablas = "";
                       into = "";
                       values = "";
                       upda = "";


                   })

                   var queryExtra = '';
                   if ($("[id$=hdnIdRegistro]").val() == 1) {
                       queryExtra = 'INSERT INTO PDK_TAB_REFERE_PERSONALES(PDK_ID_SECCCERO,PDK_CLAVE_USUARIO,PDK_FECHA_MODIF) VALUES (' + f + ',' + u + ',getdate())';
                   }
                   // console.log(cadena + ' ' + cadenaUp + ' ' + queryExtra + ' ' + 'go EXEC spValNegocio ' + f);
                   // return false;
                   if (botones == "btnGuardarAutoriza") {
                       var cadena2 = cadena + ' ' + cadenaUp
                       var txtUsu = $('[id$=txtUsuario]').val()
                       var txtpsswor = $('[id$=txtPassw]').val()
                       var idpantalla = $("[id$=hdnIdRegistro] ").val()
                       var txtmotivoOb = $("[id$=txtmotivo]").val()
                       //           var cadenastored = 'EXEC sp_ValidacionUsuario ' + '"' + cadenatodo + '",' + idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1'

                       $('#ventanaContain').hide();
                       if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                       if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
                       if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }
                       //            txtmotivoOb = "'" + txtmotivoOb + "'"


                       btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 46, 1, txtmotivoOb)
                       //               btnInsertarBoton('EXEC sp_ValidacionUsuario ' +  idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1'+'' , cadena2 +'')
                       //            btnInsertar('EXEC sp_ValidacionUsuario ' +  idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1',pantalla, '' + f + '', '1', '' + u + '',''+cadena2



                   } else if (botones == "btnGuardarCancelar") {
                       //debugger;
                       //"UPDATE PDK_TAB_DATOS_SOLICITANTE SET PDK_FECHA_MODIF = GETDATE() WHERE PDK_ID_SECCCERO = " + 

                       cadena = "UPDATE PDK_TAB_DATOS_SOLICITANTE SET PDK_FECHA_MODIF=getdate() WHERE PDK_ID_SECCCERO=" + f.toString();
                       cadenaUp = "UPDATE PDK_TAB_SOLICITANTE SET PDK_FECHA_MODIF=getdate() WHERE PDK_ID_SECCCERO=" + f.toString();
                       var cadena2 = cadena + ' ' + cadenaUp;

                       var txtUsu = $('[id$=txtusua]').val();
                       var txtpsswor = $('[id$=txtpass]').val();
                       var idpantalla = $("[id$=hdnIdRegistro] ").val()
                       var txtmotivoOb = $("[id$=TxtmotivoCan]").val()

                       $('#ventanaContain').hide();
                       if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                       if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
                       if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }
                       $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())
                       //            txtmotivoOb = "'" + txtmotivoOb + "'"

                       btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)

                       /*if (idpantalla == "1") {
                           btnInserteaBoton(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)
                       } else {
                           btnInserteaBoton('SELECT 1', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)
                       }*/
                   }

                   else if (pantalla == 'ASIGNA ENTREVISTA' || pantalla == 'VALIDACION ENTREVISTA EXTERNA') {
                       btnInsertarPanta(cadena + ' ' + cadenaUp + ' ' + '  EXEC spValNegocio ' + f + ',64,' + u, pantalla, '' + f + '', '1', '' + u + '');
                   }

                   else {
                       var idpantalla = $("[id$=hdnIdRegistro] ").val()
                       var strPantalla = $("[id$=hdnPantalla]").val()


                       mensaje1 = ReplaceAll(mensaje1, 'ctl00$ctl00$cphCuerpo$cphPantallas$txt', ' \n ')
                       mensaje = ReplaceAll(mensaje, 'ctl00$ctl00$cphCuerpo$cphPantallas$txt', ' \n ')
                       mensaje = mensaje.replace('NOMBRE25', '');
                       mensaje = mensaje.replace('NOMBRE277', '');
                       //mensaje = mensaje.replace('NUM_COTIZACION441', '');
                       mensaje = mensaje.replace('NUMERO_CLIENTE434', '');
                       mensaje = mensaje.replace('NUM_EXT21', '');
                       mensaje = mensaje.replace('NUM_EXT45', '');
                       mensaje = mensaje.replace('NUM_EXT90', '');
                       mensaje = mensaje.replace('NUM_INT22', '');
                       mensaje = mensaje.replace('NUM_INT46', '');
                       mensaje = mensaje.replace('NUM_INT91', '');
                       mensaje = mensaje.replace('NUM_INT114', '');
                       mensaje = mensaje.replace('NUMERO_CREDITO153', '');
                       mensaje = mensaje.replace('IMPORTE_RENTA436', '');
                       mensaje = mensaje.replace('IMPORTE_RENTA437', '');
                       mensaje = mensaje.replace('NUMERO_DE_CREDITO_COA176', '');
                       mensaje = mensaje.replace('INGRESO_VARIABLE_COMPROBABLE158', '');
                       mensaje = mensaje.replace('INGRESO_OTROS159', '');
                       mensaje = mensaje.replace('INGRESO_VARIABLE_COACREDITADO167', '');
                       mensaje = mensaje.replace('INGRESO_OTRO_COACREDITADO168', '');
                       mensaje = mensaje.replace('SERIE_FIRMA_ELECTRONICA438', '');
                       mensaje = mensaje.replace('SERIE_FIRMA_ELECTRONICA439', '');
                       mensaje = mensaje.replace('ORIGEN_ING_OTROS161', '');
                       mensaje = mensaje.replace('ORIGEN_ING_OTRO_COA170', '');
                       mensaje = mensaje.replace('OBSERVACION428', '');
                       mensaje = mensaje.replace('FIRMA_ELECTRONICA451', '');
                       mensaje = mensaje.replace('NUM_EXT455', '');
                       mensaje = mensaje.replace('NUM_INT456', '');
                       mensaje = mensaje.replace('PAGINA_INTERNET465', '');
                       mensaje = mensaje.replace('PAIS_OFICINA467', '');
                       mensaje = mensaje.replace('TIPO_ADMINISTACION480', '');
                       mensaje = mensaje.replace('IMPORTE_RENTA484', '');
                       mensaje = mensaje.replace('TELEFONO_MOVIL489', '');
                       mensaje = mensaje.replace('OTROS_GASTOS521', '');
                       mensaje = mensaje.replace('MENSUALIDAD131', '');
                       mensaje = mensaje.replace('OTROS_GASTOS_COACREDITADO522', '');
                       mensaje = mensaje.replace('APELLIDO_PATERNO6', '');
                       mensaje = mensaje.replace('APELLIDO_MATERNO7', '');
                       mensaje = mensaje.replace('APELLIDO_MATERNO79', '');
                       mensaje = mensaje.replace('APELLIDO_PATERNO78', '');
                       if (cvepantalla == 15 || cvepantalla == 47) {
                           mensaje = mensaje.replace('TIPO_EMPLEO116', '');
                           mensaje = mensaje.replace('COMPANIA106', '');
                           mensaje = mensaje.replace('ACTIVIDA_ECONOMICA118', '');
                           mensaje = mensaje.replace('DEPARTAMENTO108', '');
                           mensaje = mensaje.replace('PUESTO107', '');
                           mensaje = mensaje.replace('CALLE112', '');
                           mensaje = mensaje.replace('NUM_EXT113', '');
                           mensaje = mensaje.replace('NUM_INT114', '');
                           mensaje = mensaje.replace('CP121', '');
                           mensaje = mensaje.replace('COLONIA122', '');
                           mensaje = mensaje.replace('DELEGA_O_MUNI124', '');
                           mensaje = mensaje.replace('ESTADO120', '');
                           mensaje = mensaje.replace('CIUDAD123', '');
                           mensaje = mensaje.replace('TELEFONO_C_LADA109', '');
                       } else if (cvepantalla == 1) {
                           mensaje = mensaje.replace('HOMOCLAVE443', '');
                           mensaje = mensaje.replace('CURP19', '');
                           mensaje = mensaje.replace('INGRESO_FIJO_COMPROBABLE157', '');
                           mensaje1 = mensaje1.replace('AUTORIZO_INE591', '');
                           mensaje1 = mensaje1.replace('RENOVACION_CI589', '');
                           mensaje1 = mensaje1.replace('NUM_CONTRATO_CI590', '');
                           mensaje = mensaje.replace('AUTORIZO_INE591', '');
                           mensaje = mensaje.replace('RENOVACION_CI589', '');
                           mensaje = mensaje.replace('NUM_CONTRATO_CI590', '');
                           mensaje = mensaje.replace('TELEFONO_PARTI9', '');
                       }
                       mensaje = $.trim(mensaje)
                       mensaje1 = $.trim(mensaje1)

                       if (mensaje1.length > 0) { alert('Debes de selecionar el campo ' + mensaje1); $('#ctl00_ctl00_cphCuerpo_cphPantallas_Button1').removeAttr('disabled', ''); return; }
                       if ((mensaje != "CLAVE_ELECTOR543") || (mensaje.indexOf("589") >= 0) || (mensaje.indexOf("591") >= 0)) {
                           if (mensaje.length > 0) { alert('Los campos estan vacios ' + mensaje.replace("CLAVE_ELECTOR543")); $('#ctl00_ctl00_cphCuerpo_cphPantallas_Button1').removeAttr('disabled', ''); return; }
                       }
                       var camposTelefono = [];
                       $('[id*="txtTELEFONO"]').each(function (index, value) { var idSplit = $(value).attr('id').split('txt'); camposTelefono.push(idSplit[1]) });

                       var telTmp;
                       var telError = '';

                       $(camposTelefono).each(function (index, value) {
                           if ($('[id$=' + value + ']').val() != undefined) {
                               telTmp = $('[id$=' + value + ']').val();
                               if (telTmp != 0) {
                                   $(camposTelefono).each(function (index2, value2) {
                                       if (value != value2) {
                                           if (telTmp == $('[id$=' + value2 + ']').val()) {
                                               telError = 'Uno o más telefonos se repiten, verifique los campos para continuar';
                                           }
                                       }
                                   });
                               }
                           }
                       });

                       if (telError != '') {
                           alert(telError);
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_Button1').removeAttr('disabled', '');
                           return false;
                       }

                       if ($("[id$=hdnIdRegistro]").val() == 1) {
                           if ($.trim($('[id$=APELLIDO_PATERNO6]').val()) == '' && $.trim($('[id$=APELLIDO_MATERNO7]').val()) == '') {
                               alert('Debes proporcionar al menos un apellido para continuar');
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_Button1').removeAttr('disabled', '');
                               return false;
                           }
                           /*Está comentado porque es para el desarrollo EP2 EGonzale*/
                           /*Validación persona física*/
                           //if ($('[id$=RFC8]').val() != '') {
                           //    if (validaSolicitud($('[id$=RFC8]').val())) {
                           //        $('#ctl00_ctl00_cphCuerpo_cphPantallas_Button1').removeAttr('disabled', '');
                           //        return false;
                           //    }
                           //}
                       }

                       if ($("[id$=hdnIdRegistro]").val() == 5) {
                           if ($.trim($('[id$=APELLIDO_PATERNO78]').val()) == '' && $.trim($('[id$=APELLIDO_MATERNO79]').val()) == '') {
                               alert('Debes proporcionar al menos un apellido para continuar');
                               $('#ctl00_ctl00_cphCuerpo_cphPantallas_Button1').removeAttr('disabled', '');
                               return false;
                           }
                       }

                       /*Está comentado porque es para el desarrollo EP2 EGonzale*/
                       //if ($("[id$=hdnIdRegistro]").val() == 15) {
                       //    /*Validación persona moral*/
                       //    if ($('[id$=RFC449]').val() != '') {
                       //        if (validaSolicitud($('[id$=RFC449]').val())) {
                       //            $('#ctl00_ctl00_cphCuerpo_cphPantallas_Button1').removeAttr('disabled', '');
                       //return false;
                       //        }
                       //    }
                       //}

                       //console.log(cadena + ' ' + cadenaUp + ' ' + queryExtra + ' ', pantalla, '' + f + '', '1', '' + u + '');
                       //return;
                       //debugger;

                       if ((strPantalla == 'PRECALIFICACION') || (strPantalla == 'COACREDITADO')) {
                           btnInsertarPanta(cadena + ' ' + cadenaUp + ' ' + queryExtra + ' ', pantalla, '' + f + '', '1', '' + u + '');
                       } else {
                           btnInsertarPanta(cadena + ' ' + cadenaUp + ' ' + '  EXEC spValNegocio ' + f + ',64,' + u, pantalla, '' + f + '', '1', '' + u + '');
                       }
                   }
               }
           }//POR VERSE

               function ReplaceAll(text, busca, remplaza) {
                   var idx = text.toString().indexOf(busca);
                   while (idx != -1) {
                       text = text.toString().replace(busca, remplaza);
                       idx = text.toString().indexOf(busca, idx);
                   }
                   return text;
               }

               function mostrarDiv() {
                   div = document.getElementById('divautoriza');
                   div.style.display = '';

               }

               function mostrarCanceDiv() {
                   $('#ventanaContain').show();
                   $("#divcancela").show();

               }

               function ocultarCanceDiv() {
                   $('#ventanaContain').hide();
                   $("#divcancela").hide();

               }

               function ConcatenarNom() {
                   var nombre1 = $("[id$=txtNOMBRE14]").val()
                   var nombre2 = $("[id$=txtNOMBRE25]").val()
                   var apellidoPa = $("[id$=txtAPELLIDO_PATERNO6]").val();
                   var apellidoMa = $("[id$=txtAPELLIDO_MATERNO7]").val();
                   var nombreCom = $("[id$=txtNOMBRE_SOLICI419]");
                   var concatenar = nombre1 + ' ' + nombre2 + ' ' + apellidoPa + ' ' + apellidoMa
                   nombreCom.attr("value", concatenar)
               }

               function validarTelefono(tel, campo) {
                   if (tel != '0' && tel != '' && tel != undefined) {
                       if (tel.length > 9) {
                           return true;
                       } else {
                           if (campo != undefined) {
                               alert('El campo ' + campo + ' NO es un teléfono válido');
                           }
                           return false;
                       }
                   } else {
                       return true;
                   }
               }

               function cleanFormTables() {
                   $("[id$=ctl00_ctl00_cphCuerpo_cphPantallas_pantalla] >table").each(function (index) {
                       //$(this).children("caption").each(function (index2) { console.log($(this).text()); })
                       //console.log($(this).children('tbody').length);
                       if ($(this).children('tbody').length == 0) { $(this).remove() }
                   });
               }

               function replacer(key, value) {
                   // Filtrando propiedades 
                   if (typeof value === "string") { return undefined; }
                   return value;
               }

               function btnGuardarTMP() {
                   if (($.urlParam("idPantalla").toString()) == "7") {
                       var ciudad = $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCIUDAD547 option:selected").text();
                       if (ciudad.toString() == "< SELECCIONAR >") {
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtCIUDAD547').html('');
                           $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddltxtESTADO546').html('');
                       }

                       var dFake = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtPDK_FECHA_MODIF531").val();
                       var dFakeArray = dFake.split("/");
                       $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtPDK_FECHA_MODIF531").val(dFakeArray[2] + "-" + dFakeArray[1] + "-" + dFakeArray[0]);

                       armaJson(0);
                   } else {
                       var tabla;
                       var into = "";
                       var values = "";
                       var upda = "";
                       var folio = $("[id$=hdnFolio] ")
                       var usuario = $("[id$=hdnUsuario] ")
                       var pantalla = $("[id$=hdnPantalla] ")
                       var cadena = "";
                       var cadenaUp = "";
                       var tablas = "";
                       var f = folio.val();
                       var u = usuario.val();
                       var valida = "";
                       var cvepantalla = $("[id$=hdnIdRegistro]").val();
                       var pantalla = pantalla.val();

                       var mater = $("[id$=ctl00_ctl00_cphCuerpo_cphPantallas_pantalla] >table")
                       mater.each(function (index) {
                           tabla = $(this).attr('id')
                           valida = tabla
                           tabla = tabla.replace('ctl00_ctl00_cphCuerpo_cphPantallas_U_', '');
                           tabla = tabla.replace('ctl00_ctl00_cphCuerpo_cphPantallas_I_', '');
                           valida = valida.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
                           valida = valida.replace(tabla, '');
                           valida = valida.replace('_', '');

                           $(this).children("caption").each(function (index2) { tablas += $(this).text() })//traer el nombre de la tabla 
                           var newtb = $("[id$=" + tabla + "] tbody tr")

                           newtb.each(function (index) {
                               $(this).children("td").each(function (index3) {
                                   $(this).children("table").each(
                                       function (index8) {
                                           var tablahijo = $(this).attr('id')
                                           var newtabhijo = $("[id$=" + tablahijo + "] tbody tr")
                                           newtabhijo.each(function (index9) {
                                               $(this).children("td").each(function (index10) {
                                                   $(this).children("input").each(function (index11) {
                                                       var chek1 = $(this).attr("type");
                                                       if (chek1 == "radio") {
                                                           var radio = $(this).is(':checked');
                                                           if (radio == true) {
                                                               if (valida != "I") {
                                                                   upda += "'" + $(this).attr("value") + "',"
                                                               }
                                                           }
                                                       }
                                                   })
                                               })
                                           })
                                       })

                                   $(this).children("span").each(
                                           function (index4) {
                                               var txt1 = $(this)[0].id
                                               var matritxt = txt1.split("|");

                                               if (valida != "I") {
                                                   upda += matritxt[1] + '='
                                               }
                                           }
                                   )

                                   $(this).children("input").each(
                                       function (index5) {
                                           var texto = $(this).val();
                                           var check = $(this).attr("type");

                                           if (check == "checkbox") {
                                               var chkbox = $(this).is(':checked');

                                               if (valida != "I") {
                                                   upda += "'" + texto + "',"
                                               }

                                           } else if (check == "text") {

                                               if (valida != "I") {
                                                   upda += "'" + texto + "',"
                                               }
                                           }
                                       })



                                   $(this).children("select").each(function (index6) {
                                       var sele = $(this).attr('id')
                                       lista = $("[id$=" + sele + "] option:selected");
                                       if (valida == "I") {
                                           values += "'" + lista.text() + "',"
                                       } else {
                                           upda += "'" + lista.text() + "',"
                                       }
                                   })

                                   $(this).children("textarea").each(function (index7) {
                                       var texto = $(this).val();
                                       if (valida != "I") {
                                           upda += "'" + texto + "',"
                                       }
                                   })
                               })

                           })

                           if (valida != "I") {
                               cadenaUp += '  UPDATE ' + tabla + ' ' + 'SET' + ' ' + upda + ' PDK_CLAVE_USUARIO=' + u + ',' + ' PDK_FECHA_MODIF=' + 'getdate()' + ' WHERE PDK_ID_SECCCERO=' + f + ' '

                           }
                           tablas = "";
                           into = "";
                           values = "";
                           upda = "";


                       })
                       // console.log(cadena + ' ' + cadenaUp)
                       // return false;
                       btnInsertarPanta(cadena + ' ' + cadenaUp, pantalla, '' + f + '', '1', '' + u + '', false, 'Registro guardado satisfactoriamente.');
                   }
               }

               function habilitarCampos() {
                   if ($('[id*=hdnIdRegistro]').val() == 7) {
                       if ($('[id*=hdnTareaActual]').val() == $('[id*=hdnIdRegistro]').val()) {
                           $('input, select').not('[id*=PDK_TAB_DATOS_SOLICITANTE] input, [id*=PDK_TAB_DISTRIBUIDOR] input, [id*=PDK_TAB_DATOS_SOLICITANTE] select, [id*=PDK_TAB_DISTRIBUIDOR] select').attr('disabled', false);
                           $('[id*=CLIENTE_INCREDIT433], [id*=NOMBRE_VENDEDOR147]').attr('disabled', false);
                       } else {
                           $('[id*="GuardarTMP"]').hide();
                       }
                   }
               }

               function deshabilitarCampos() {
                   $('[id$=HOMOCLAVE443]').attr('disabled', true);
                   $('[id$=txtRFC8]').attr('disabled', true);
               }

               $(window).bind("load", function () {
                   cleanFormTables();
                   if ($("[id$=hdnIdRegistro]").val() == 4) {
                       $('[id$=CONDICION1140]').attr('disabled', true);
                       $('[id$=CONDICION2141]').attr('disabled', true);
                       $('[id$=CONDICION3142]').attr('disabled', true);
                   }
               }); // Función que espera la carga completa del documento incluyendo imágenes y Ajax*

               $("[id$=btnImprimir]").click(function (event) {
                   event.preventDefault();
               });

               $(document).ready(function () { $.urlParam = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } }; if ($.urlParam("Enable").toString() == "1") { $("#btnCancelarNew").hide(); }; });

               function ValidarAntiguedadLaboral() {
                   obtenerParamAntiguedadLaboral("446");
               }

               function obtenerParamAntiguedadLaboral(parametro) {
                   var destino = "Blanco.aspx/ParamAntiguedadLaboral";
                   var successfully = OnSuccesParamAntiguedadLaboral;
                   var datos = '{"param":"' + parametro.toString() + '", "opcion": "5"}';
                   jsonBack('No fue posible obtener el parametro', destino, successfully, datos);
               }

               function OnSuccesParamAntiguedadLaboral(response) {
                   var items = $.parseJSON(response.d);
                   if (items[0].toString() == "OK") {
                       var antiguedad = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD42").val();
                       var mesesAnt = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD553").val();
                       var edad1 = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtFECHA_NACIMIENTO24").val();
                       var edad2 = edad1.split("/");
                       var dia = edad2[0];
                       var mes = edad2[1];
                       var anio = edad2[2];
                       var fechaNew = anio + "/" + mes + "/" + dia;
                       var date = new Date(fechaNew);
                       if (mesesAnt != "") {
                           date.setMonth(date.getMonth() + parseInt(mesesAnt));
                       }
                       var age = getAge(date);
                       var diferencia = age - antiguedad;

                       if (diferencia < parseInt(items[1].toString())) {
                           alert("La edad minima para laboral debe ser mayor o igual a 14 años");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD42").val("");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtMESES_ANTIGUEDAD553").val("");
                           $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtANOS_ANTIGUEDAD42").focus();
                       }
                   }
                   else {
                       alert(items[0].toString());
                   }
               }
</script>
    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnCancela {
            display: none;
        }
    </style>
    <!--Add CSS to ToolTip -->
    <style>
        .tooltip {
            position: relative;
            display: inline-block;
            text-decoration: none;
            border-bottom: 1px dashed #999;
        }

        .tooltip .tooltiptext {
            visibility: hidden;
            min-width: 127px;
            max-width: 350px;
            min-height: 13px;
            max-height: 100px;
            background-color: #d9e6f2;
            color: black;
            text-align: center;
            border-radius: 6px;
            padding: 5px 0;

            position: absolute;
            z-index: 1;
            top: 150%;
            left: 50%;
            margin-left: -60px;
        }

        .tooltip .tooltiptext::after {
            content: "";
            position: absolute;
            bottom: 100%;
            left: 50%;
            margin-left: -5px;
            border-width: 5px;
            border-style: solid;
            border-color: transparent transparent #d9e6f2 transparent;
        }

        .tooltip:hover .tooltiptext {
            visibility: visible;
        }

        select[disabled="disabled"], select[disabled]{
            color: #979797;
            background-color: rgb(235, 235, 228);
        }

        input[type="button"][disabled="disabled"], input[type="button"][disabled]{
            color: #979797;
            background-color: rgb(235, 235, 228);
        }

        input[type="date"] {
            border-width: 1px;
            border-color: #000000;
            height: 18px;
        }

        input[type="date"][disabled="disabled"], input[type="date"][disabled]{
            color: #979797;
            background-color: rgb(235, 235, 228);
        }

        .txt2BBVA {
            width:170px !important;
        }

        .select2BBVA {
            width: 176px!important;
        }

        .campos {
            width: 220px !important;
        }

        #ctl00_ctl00_cphCuerpo_cphPantallas_lbl535\|REGIMEN_CONYUGAL {
            text-align: right!important;
        }
    </style> 
     
<div class="divAdminCat">

    <div class="divFiltrosConsul">
    <table>
     <tr>
       <td class="tituloConsul"><asp:Label ID="lbltitulo" Text="" runat="server"></asp:Label>    </td>
     </tr>
    </table> 
    </div> 

    <div class ="divAdminCatCuerpo">

        <div id="pantalla" runat="server"  style="position:absolute ; top:0%; left:0%; width:100%; height:100%;  overflow:auto;">
        </div>

        <%--<div id ="divautoriza" style="display:none">
            <cc1:ModalPopupExtender ID="mpoAutorizar" runat="server" TargetControlID="btnAutoriza" PopupControlID="popAutoriza" CancelControlID="btnCancelAutoriza" BackgroundCssClass ="modalBackground" >
            </cc1:ModalPopupExtender>
            <asp:Panel ID="popAutoriza" runat="server" Text="Autorización" CssClass ="cajadialogo" >
                <div class="tituloConsul"  >
                    <asp:Label ID="Label4" runat="server" Text="Autorización" />
                </div>
                <table width="100%"> 
                      <tr>
                        <td class="campos" >Usuario:</td>
                        <td><asp:TextBox ID="txtUsuario" SkinID ="txtGeneral" MaxLength="12"  runat="server" ></asp:TextBox> </td>
                      </tr>
                      <tr>
                        <td class="campos"  >Password:</td>
                        <td><asp:TextBox ID="txtPassw" runat="server" SkinID="txtGeneral" MaxLength ="12" TextMode="Password" EnableTheming ="true"   ></asp:TextBox> </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="campos" >Descripción:</td>
                      </tr>
                      <tr>
                        <td colspan="2"><textarea id="txtmotivo" runat="server" onkeypress="ManejaCar('A',1,this.value,this)" class = "Text" rows = "5" cols = "1" style = "width:100%"></textarea> </td>
                     </tr>
                      <tr style="width:100%">
                       <td><asp:HiddenField runat="server" ID="hdnidAutoriza" /></td>
                       <td align="left" valign="middle">
                            <input id="btnGuardarAutoriza" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar(id)"  /> 
                            <asp:Button ID="btnCancelAutoriza" runat="server" Text="Cancelar" SkinID="btnGeneral"/>              
                       </td>
                      </tr>
                </table>
            </asp:Panel> 
        </div>--%>

        <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top:15%; left:31%; width: 220px;">
            <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID ="btnCancela" CancelControlID="btnCancelCancela"  PopupControlID="popCancela" BackgroundCssClass ="modalBackground">
            </cc1:ModalPopupExtender> --%>
            <asp:Panel ID="popCancela" runat ="server" CssClass ="cajadialogo">
                <div class="tituloConsul"  >
                    <asp:Label ID="Label1" runat="server" Text="Cancelación" />
                </div>
                <table width="100%">
                 <tr>
                    <td class="campos" >Usuario:</td>
                    <td><asp:TextBox ID="txtusua" SkinID ="txtGeneral" MaxLength="12"  runat="server" style="width:120px !important;"></asp:TextBox> </td>
                  </tr>
                  <tr>
                    <td class="campos"  >Password:</td>
                    <td><asp:TextBox ID="txtpass" runat="server" SkinID="txtGeneral" MaxLength ="12" TextMode="Password" EnableTheming ="true" style="width:120px !important;"></asp:TextBox> </td>
                  </tr>
                   <tr>
                    <td colspan="2" class="campos" >Descripción:</td>
                  </tr>
                  <tr>
                    <td colspan="2"><textarea id="TxtmotivoCan" runat="server" onkeypress="ManejaCar('A',1,this.value,this)" class = "Text" rows = "5" cols = "1" style = "width:95% !important;"></textarea> </td>
                 </tr>
                  <tr style="width:100%">
                   <td><asp:HiddenField runat="server" ID="HiddenField1" /></td>
                   <td align="left" valign="middle">
                        <input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar(id)"  /> 
                        <input ID="btnCancelCancela" type="button" runat="server" value="Cancelar" class="Text " onclick="ocultarCanceDiv()"/>              
                   </td>
                  </tr>
                </table> 
            </asp:Panel>
        </div>
    </div>   
    
    <div class="divAdminCatPie">
		  <div style="background-color: #F0F0F0;">
            <asp:Image runat="server" ID="Image2" Width="30" Height="30" ImageUrl="~/App_Themes/Imagenes/Alert.png" />
            <label class="cintafooter"> Todos los campo marcados con asterisco(*) son obligatorios.</label>
        </div>	
			<table width="100%" style="height:100%;">
                <tr>
                    <td align="right" valign="middle" class = "cssCuerpo">
                         <input id="txtXML" type="text" runat="server"  style="display:none"   />
                         <input id="txtXSL"  type="text" runat="server"   style="display:none" />
                        <asp:Button runat="server" ID="btnGuardar" text="Guardar" Visible="false"/> 
                        <asp:Button runat="server" ID="btnRegresar" text="Regresar" CssClass="buttonSecBBVA2"/>
                        <asp:Button ID="btnImprimir" Text="Imprimir" runat="server" CssClass="buttonSecBBVA2"/>                        
                        <input id="Button1" value="Procesar" type="button" class="buttonBBVA2" onclick="$(this).prop('disabled', true); btnGuardar(id)" runat="server" />
                        <%--<input id="btnAutoriza" value="Autorizar" type="button" onclick="mostrarDiv()"   class ="oculta" runat="server"   />--%>
                        <input id="btnCancela" value="Cancelar" type="button" onclick="mostrarCanceDiv()"  class="buttonSecBBVA2" runat="server" />   
                        <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                        <input id="btnGuardarTMP" value="Guardar" type="button"  onclick="btnGuardarTMP()"  class="buttonSecBBVA2" runat="server" />
                     </td>
                </tr>
            </table>
      </div>
      
</div>

    

<asp:HiddenField runat="server" ID="hdnIdRegistro" />
<asp:HiddenField runat="server" ID="hdnFolio" />
<asp:HiddenField runat="server" ID="hdnUsuario" />
<asp:HiddenField runat="server" ID="hdnPantalla" />
<asp:HiddenField runat="server" ID="hdnResultado" />
<asp:HiddenField runat="server" ID="hdnResultado1" /> 
<asp:HiddenField runat="server" ID="hdnResultado2" /> 
<asp:HiddenField runat="server" ID="hdnCp" />
<asp:HiddenField runat="server" ID="hdnCp1" />
<asp:HiddenField runat="server" ID="hdnCp2" />
<asp:HiddenField runat="server" ID="hdnCp3" />
<asp:HiddenField runat="server" ID="hdnCp4" />
<asp:HiddenField ID="hdRutaEntrada" runat="server" /> 
<asp:HiddenField runat="server" ID="hdnano" />
<asp:HiddenField runat="server" ID="hdnmes" />
<asp:HiddenField runat="server" ID="hdndia" />
<asp:HiddenField runat="server" ID="hdnanocoa" />
<asp:HiddenField runat="server" ID="hdnmescoa" />
<asp:HiddenField runat="server" ID="hdndiacoa" />  
<asp:HiddenField runat="server" ID="hdvalidad" /> 
<asp:HiddenField runat="server" ID="hdvalidadCoa" /> 
<asp:HiddenField runat="server" ID="hdnValiRenta" />
<asp:HiddenField runat="server" ID="hdnValiRenta1" />
<asp:HiddenField runat="server" ID="hdnTareaActual" />

</asp:Content>


