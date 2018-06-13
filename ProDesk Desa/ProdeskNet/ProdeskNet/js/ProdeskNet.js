    
//INC-B-2019:JDRA:Regresar

//Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
//Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

/*  YAM-P-208 egonzalez 11/08/2015
 *  Se agregó una validación en caso de que al subir un documento, este nos regrese un valor vacío
 *  así como el respectivo mensaje de error para el usuario.
 *  YAM-P-208 egonzalez 19/08/2015
 *  Se creó una validación para forzar a la función fillUpload() haciendola creer que se encuentra en la pantalla de carga de documentos
 *  así se aprovecha la funcionalidad y creamos la tabla de documentos
 *  A-87:AMR:No pide aval en caso de vivienda no propia.
 *  YAM-P-208 egonzalez 01/09/2015
 *  Se modificó la función "btnInsertarPanta()" agregandole 2 parámetros opcionales para no alterar el llamado a la misma desde otro punto
 *      los parámetros son: "redirect" y "mensaje" con ello evitamos o permitimos el redireccionamiento y recibe un mensaje y lo muestra al no redireccionar al final
 *  YAM-P-208 egonzalez 08/09/2015
 *  Se modificó la función de "fnCargaPaginacion" Para poder manejar los resultados de la carga de registros por llamada al store
 *  YAM-P-208 egonzalez 12/10/2015
 *  Se redujo el número de páginas donde se hará la paginación
 *  YAM-P-208 egonzalez 28/10/2015
 *  Se modificó la función ManejaCar() para que sólo regrese la bandera, evitando el lío de conflicto de cross browser, así no se necesita modificar el evento del navegador el cuál era el causante de los errores en los diferentes navegadores
 * BBVA-P-423:RQCONYFOR-05 JRHM 23/11/16 Se modifico metodo fnUpload() para crear los eventos para el nuevo fileupload.
 * BBVA-P-423:RQ I -WS ARCHIVING 14/12/16 JRHM Se agrego el control de id documento con el servicio ingestadocumentos
* BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
* BBVA-P-423:RQSOL-02 gvargas 15/12/16 Se modifico manejacar para ingreso de textos pantalla 7.
* BUG-PD-01: AMR: 22/12/2016: HTTP Error 404 - Not Found al procesar Precalificación.
* BBVA-P-423 JRHM: RQ J-REPORTE SOLICITUD DE CREDITO 30/12/16.- Se arreglo metodo ManejaCar para evitar error en pagina blanco.aspx.
* BUG-PD-03: GVARGAS: 05/01/2017: Correcciones pantalla Precalificacion.
* BUG-PD-04: GVARGAS: 11/01/2017: Correcciones pantalla Precalificacion permite ingresar etras con acentos.
* BUG-PD-05: GVARGAS: 24/01/2017: Correcciones pantalla Solicitud de Credito permite ingresar etras con acentos.
* BUG-PD-06: GVARGAS: 26/01/2017: Correcciones Acentos campos direccion.
* BBV-P-423 RQADM-36: AVH: 02/02/2017 Se modifica la carga de archivos
* BUG-PD-09: GVARGAS: 15/02/2017: Correccion filldll() sincrono
* BUG-PD-10: GVARGAS: 21/02/2017: Redirect	 
* BUG-PD-15: JBB:  28/02/2017: Se desahilita el asincrona de fillgv
* BUG-PD-13  GVARGAS  01/03/2017 Cambios en validacion homologar Procotiza
* BBV-P-423_RQADM-17 : JBB 27/03/2017 se desactiva boton procesar de los formularios de redes sociales
* BUG-PD-21  GVARGAS  27/03/2017 Bugs Campos Obligatorios
* BUG-PD-23 MAPH 30/03/2017 Corrección del paginador para que cargue las secciones mayores a 8
* BUG-PD-24  GVARGAS  29/03/2017 Bugs Campos Desabilitados en Pre si la SOL se regresa permit "-" & "." en direcciones referencias
*BUG-PD-25 JRHM 04/04/17 SE MODIFICO FUNCION ERROR DE LA CARGA DE ARCHIVOS PARA QUE MANDE UN MENSAJE DE ERROR
* BBVA-P-423: RQCONYFOR-07_2: AVH: 11/04/2017 modificaciones JBEJAR	
* BUG-PD-26 MAPH 06/04/2017 Inclusión de función para borrar inputs y seleccionar valor por defecto en DropDownLists
* BUG-PD-32: AVH: 18/04/2017 Correcciones de JHERNANDEZ		 
* BBV-P-423  RQADM-02 GVARGAS  12/04/2017 Actualización de Datos en Solicitud 38,39
* BUG-PD-33 JRHM 24/04/17 SE MODIFICO OPCION DE FUNCION PARA QUE VALIDACION DE TIPIFICACION DE DOCUMENTOS ACEPTE MENSAJE "FALTA"
* BBVA-P-423: RQXLS3 CGARCIA 09/06/2017 SE AGREGARON PARAMETROS PARA FILLUPLOAD
* BUG-PD-72 GVARGAS 07/06/2017 Añadir Ñs
* BUG-PD-99:MPUESTO:15/06/2017:CORRECCION PARA MOSTRAR O ELIMINAR LOS BOTONES DE ELIMINAR EN LAS TABLAS DE FACTURAS		 
* BUG-PD-98: RHERNANDEZ: 20/06/17: SE HABILITAN LOS BOTONES DE CARGA DE DOCUMENTOS AL DAR CLICK EN PROCESAR Y MOSTRAR UN MENSAJE DE ERROR
* BUG-PD-113  GVARGAS  21/06/2017 Modificacion servicio
* BUG-PD-123 ERODRIGUEZ 29/06/2017 Se creo la funcion btnInserteaBoton_dos para permitir cancelar al estar equivocados los parametros que se envian, en tipo y orden
* BUG-PD-145 RHERNANDEZ: 10/07/2017: Se quita funcionalidad drageable a todas las pantallas de adjuntar documentos.
* BUG-PD-143 GVARGAS 25/07/2017 PopUpRedirect Nuevo	  
* JBEJAR CAMBIO URGENTE FUNCION CANCELAR ADELANTABA A LAS AUTOMATICAS SI TENIA ESE VALOR.
* BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN
* BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS PARA ABRIR VISOR TELEPRO
* BUG-PD-224: RHERNANDEZ: 06/10/17: Se comenta mensaje de error para la funcion de fillUpload
* RQ-PI7-PD2: ERODRIGUEZ: 19/10/2017: Se agrego validacion para abrir caja de notas en pestaña nueva, llevando id de solicitud de pantalla donde se este trabajando.
* BUG-PD-237: RHERNANDEZ: 23/10/17: Al realizar un redirect en las tareas se bloquean todos los botones de los aspx
* BUG-PD-254: ERODRIGUEZ: 27/10/2017: Se creo ventana para el historial de caja de notas externas e internas.
* BUG-PD-261 : EGonzalez 06/11/2017 : Se crea la función "globalValidate" para realizar una validación completa de los campos al guardar.
* BUG-PD-274 GVARGAS 23/11/2017 Cambio "globalValidate" no valide inputs deshabilitados.
* BUG-PD-290: MGARCIA: 05/12/2017: Detalle Impagos en Menu
* BUG-PD-336: DJUAREZ: 15/01/2018: Se bloquea la pantalla cuando se esta ejecutando una pantalla automatica.
* BUG-PD-435: DCORNEJO: 07/05/2018: Se agrega Lineas en function btnEntrevista-->Roberto Hernandez.
* BUG-PD-441: DCORNEJO: 09/05/2018: SE CAMBIA MENSAJE DE ERROR EN btnManejoMensaje
 */

var options = {};

function beginReq(sender, args) {
    $find(ModalProgress).show();
}

function endReq(sender, args) {
    $find(ModalProgress).hide();
}

function alertJQUERY(msg) {
    $("[id$=lblMensaje]").text(msg); $('#ventanaconfirm').show(); $('#ventanaconfirm').draggable(); centraVentana($('#ventanaconfirm'));
}

function ManejaCar(tipo, b, val, obj, evt) {
	var id_ = obj.id;
	if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtMAIL554") { tipo = "m"; }
	if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtDIRECCION560") { tipo = "Apointguion"; }
	if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtDIRECCION563") { tipo = "Apointguion"; }
	if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtDIRECCION567") { tipo = "Apointguion"; }
    if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtN_IDENTIFICACION542") { tipo = "A"; }
    if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtCLAVE_ELECTOR543") { tipo = "A"; }
    if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESO_FIJO_COMPROBABLE157") { tipo = "DD"; }
    if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESO_VARIABLE_COMPROBABLE158") { tipo = "DD"; }
    if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESOS_FIJOS539") { tipo = "DD"; }
    if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtINGRESOS_VARIABLES540") { tipo = "DD"; }
    
    if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE14") { tipo = "Cdot"; }
    if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtAPELLIDO_MATERNO7") { tipo = "Cdot"; }
    if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtAPELLIDO_PATERNO6") { tipo = "Cdot"; }
    if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE25") { tipo = "Cdot"; }
    if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtNOMBRE_EMPLEO_ANT555") { tipo = "Apoint"; }
    if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtCOMPANIA37") { tipo = "Apoint"; }
    if (id_ == "ctl00_ctl00_cphCuerpo_cphPantallas_txtCALLE20") { tipo = "Apointguion"; }

    //DD = enteros con 2 digitos  
    //N = solo numeros            , si b = 1 permite negativos
    //D = numeros decimales       , si b = 1 permite negativos
    //A = Alfanumericos Mayusculas, si b = 1 permite espacio en blanco
    //a = Alfanumericos Minusculas, si b = 1 permite espacio en blanco
    //C = Solo letras Mayusculas  , si b = 1 permite espacio en blanco
    //c = Solo letras Minusculas  , si b = 1 permite espacio en blanco
    //T = Alfanumericos May       , si b = 1 permite espacio en blanco. Permite coma, punto, guin bajo, guion medio, diagonal, diagonal invertida y signos de interrogacion
    //t = Alfanumericos Min       , si b = 1 permite espacio en blanco. Permite coma, punto, guin bajo, guion medio, diagonal, diagonal invertida y signos de interrogacion
    //m = Correo Electronico      , alfanumericos minusculas, arroba, punto, guion bajo y medio, punto y coma
    //L = Direcciones de Internet , alfanumericos minusculas, punto y diagonal
    //K= Alfanumerico May          , alfanumericos mayuscula y arroba
    //p= Numeros Letras may
    //k= Numeros decimales         ,permite punto borrar y numero de pagos
    //j=numero y letras diagonal
    var c = (evt.which) ? evt.which : event.keyCode;
    //console.log(c);
    //return false;
    var band = false;
    //var c = event.keyCode;
    var str = new String(val);

    switch (tipo) {
        case 'Apointguion':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if ((c >= 97 && c <= 122) || (c == 241)) { evt.which = c - 32; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 241) || (c == 209)) { band = false; }
            if ((c == 193) || (c == 201) || (c == 205) || (c == 211) || (c == 218)) { band = true; } //Acentos mayosculas/ BUG PD 06
            if ((c == 225) || (c == 233) || (c == 237) || (c == 243) || (c == 250)) { band = true; } //Acentos minusculas/ BUG PD 06
            if (c == 8) { band = true; }
            if (c == 46) { band = true; }
            if (c == 44) { band = true; }
            if ((c == 209) || (c == 241)) { band = true; } //ñ, Ñ
            break;
        case 'Apoint':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if ((c >= 97 && c <= 122) || (c == 241)) { evt.which = c - 32; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 241) || (c == 209)) { band = false; }
            if ((c == 193) || (c == 201) || (c == 205) || (c == 211) || (c == 218)) { band = true; } //Acentos mayosculas/ BUG PD 06
            if ((c == 225) || (c == 233) || (c == 237) || (c == 243) || (c == 250)) { band = true; } //Acentos minusculas/ BUG PD 06
            if (c == 8) { band = true; }
            //if (c == 46 && str.indexOf('.', 0) == -1) { band = true; }
            if (c == 46) { band = true; }
            if ((c == 209) || (c == 241)) { band = true; } //ñ, Ñ
            break;
        case 'DD':
            if (str.indexOf('.', 0) != -1) {
                var strArray = str.split(".");
                if ((strArray[1].length < 2) && (c >= 48 && c <= 57)) { band = true; }
            }
            else {
                if (c >= 48 && c <= 57) { band = true; }
                if (c == 46 && str.indexOf('.', 0) == -1) { band = true; }
            }
            if (c == 8) { band = true; }
            break;
        case 'N':
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 45 && str == '' && b == 1) { band = true; }
            if (c == 8) { band = true; }
            break;
        case 'D':
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 46 && str.indexOf('.', 0) == -1) { band = true; }
            if (c == 45 && str == '' && b == 1) { band = true; }
            if (c == 8) { band = true; }
            break;
        case 'A':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if ((c >= 97 && c <= 122) || (c == 241)) { evt.which = c - 32; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 241) || (c == 209)) { band = false; }
            if ((c == 193) || (c == 201) || (c == 205) || (c == 211) || (c == 218)) { band = true; } //Acentos mayosculas/ BUG PD 06
            if ((c == 225) || (c == 233) || (c == 237) || (c == 243) || (c == 250)) { band = true; } //Acentos minusculas/ BUG PD 06
            if ((c == 209) || (c == 241)) { band = true; } //ñ, Ñ
            if (c == 8) { band = true; }
            break;
        case 'a':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 32 && b == 1) { band = true; }
            if (c == 8) { band = true; }
            break;
        case 'C':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if ((c >= 97 && c <= 122) || (c == 241)) { evt.which = c - 32; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 193) || (c == 201) || (c == 205) || (c == 211) || (c == 218)) { band = true; } //Acentos mayosculas
            if ((c == 225) || (c == 233) || (c == 237) || (c == 243) || (c == 250)) { band = true; } //Acentos minusculas
            if (c == 8) { band = true; }
            break;
        case 'Cdot':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if ((c >= 97 && c <= 122) || (c == 241)) { evt.which = c - 32; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 193) || (c == 201) || (c == 205) || (c == 211) || (c == 218)) { band = true; } //Acentos mayosculas
            if ((c == 225) || (c == 233) || (c == 237) || (c == 243) || (c == 250)) { band = true; } //Acentos minusculas
            if (c == 8) { band = true; }
            if (c == 46) { band = true; }
            break;
        case 'c':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c == 32 && b == 1) { band = true; }
            if (c == 8) { band = true; }
            break;
        case 'T':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if ((c >= 97 && c <= 122) || (c == 241)) { evt.which = c - 32; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 44) || (c == 45) || (c == 46) || (c == 47) || (c == 92) || (c == 95)) { band = true; }

            //signos de interrogacion
            if (c == 63) { band = true; }
            if (c == 191) { band = true; }

            //los siguientes valores son letras con acentos (Á,É,Í,Ó,Ú)
            if (c == 193) { band = true; }
            if (c == 201) { band = true; }
            if (c == 205) { band = true; }
            if (c == 211) { band = true; }
            if (c == 218) { band = true; }
            if ((c == 225) || (c == 233) || (c == 237) || (c == 243) || (c == 250)) { evt.which = c - 32; }
            if ((c  == 241)|| (c==209)) { band = false; }
            if (c == 8) { band = true; }
            break;
        case 't':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 44) || (c == 45) || (c == 46) || (c == 47) || (c == 92) || (c == 95)) { band = true; }

            //signos de interrogacion
            if (c == 63) { band = true; }
            if (c == 191) { band = true; }

            //los siguientes valores son letras con acentos (á,é,í,ó,ú  Á,É,Í,Ó,Ú)
            if (c == 225) { band = true; }
            if (c == 233) { band = true; }
            if (c == 237) { band = true; }
            if (c == 243) { band = true; }
            if (c == 250) { band = true; }

            if (c == 193) { band = true; }
            if (c == 201) { band = true; }
            if (c == 205) { band = true; }
            if (c == 211) { band = true; }
            if (c == 218) { band = true; }

            if (c == 8) { band = true; }

            break;
        case 'm':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 32 && b == 1) { band = true; }
            if (c == 59) { band = true; }
            if ((c == 46) || (c == 45) || (c == 95) || (c == 64) || (c == 54)) { band = true; }
            if ((c >= 65 && c <= 90) || (c == 241)) { evt.which = c + 32; }
            if (c == 165) { evt.which = c - 1; }
            if (c == 8) { band = true; }
            break;
        case 'L':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 46) || (c == 47) || (c == 58) || (c == 45) || (c == 95)) { band = true; }
            if (c == 8) { band = true; }
            break;
        case 'Z':
            if (val.length <= 250) { band = true; }
            if (c == 8) { band = true; }
            break;
        case 'K':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if ((c == 46) || (c == 45) || (c == 95) || (c == 64) || (c == 54)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if ((c >= 97 && c <= 122) || (c == 241)) { evt.which = c - 32; }
            if (c == 32 && b == 1) { band = true; }
            if (c == 8) { band = true; }
            break;
        case 'k':
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 46 && str.indexOf('.', 0) == -1) { band = true; }
            if ((c == 8) || (c==37)||(c==39)) { band = true; }
            if (c>=96 && c<=122){band=true;}
            if (c == 45 && str == '' && b == 1) { band = true; }
            if (c == 8) { band = true; }
            break;
        case 'p':
            if (c == 45 && str == '' && b == 1) { band = true; }
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if ((c >= 97 && c <= 122) || (c == 241)) { evt.which = c - 32; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 241) || (c == 209)) { band = false; }
            if (c == 8) { band = true; }
        case 'j':
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 45) { band = true; }
            if (c == 8) { band = true; }
            break;
     


        case 'z':
            val = window.clipboardData.getData('Text');
            if (val.length <= 250) { band = true; }
            else {
                b.value = val.substr(0, 251);
            }
            if (c == 8) { band = true; }
            break;
       
    }

    //ISGN - REMPLAZAMOS EL VALOR DE LA LETRA CUANDO EL keyCode ORIGINAL CAMBIO PORQUE IE 9 NO RESPETA EL CAMBIO
    if (band) {
        if (c != evt.which) {
            band = false;
            obj.value = obj.value + String.fromCharCode(evt.which);
        }
    }

    return band;
    evt.returnValue = band;
}

function globalValidate() {
    $('.inputError').removeClass('inputError');
    var b = 1;
    var tmpArr = [];
    var regExp = /^[w]/;
    var bandValida = true;
    var camposErroneos = [];
    var valGeneral = 0;

    $('input[type=text]').each(function () {
        tmpArr = [];

        if (($(this).attr('onkeypress') != undefined) && (!($(this).prop('disabled')))) {
            tmpArr = $(this).attr('onkeypress').split('(')[1].split(')')[0].split(',');
            tmpArr[0] = tmpArr[0].replace(/\'/g, '').replace(/\"/g, '');
            /*console.log(tmpArr[0]);*/
            switch (tmpArr[0]) {
                case 'DD':
                    regExp = /^\d*\.?\d{0,2}$/;
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).attr('id'));
                    }
                    break;
                case 'N':
                    regExp = (b == 1 ? /^(\d|-)?(\d)*$/ : /^(\d)*$/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'D':
                    regExp = (b == 1 ? /^(\d|-)?(\d)*\.?(\d)*$/ : /^(\d)?(\d)*\.?(\d)*$/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'A':
                    regExp = (b == 1 ? /^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\/\s]*$/ : /^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\/]*$/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'a':
                    regExp = (b == 1 ? /^[a-z0-9áéíóúñ\s]*$/ : /^[a-z0-9áéíóúñ]*$/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'Apointguion':
                    regExp = (b == 1 ? /^[a-zA-Z0-9áéíóúÁÉÍÓÚ.,ñÑ\s]*$/ : /^[a-zA-Z0-9áéíóúÁÉÍÓÚ.,ñÑ]*$/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'Apoint':
                    regExp = (b == 1 ? /^[a-zA-Z0-9áéíóúÁÉÍÓÚ.ñÑ\s]*$/ : /^[a-zA-Z0-9áéíóúÁÉÍÓÚ.ñÑ]*$/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'C':
                    regExp = (b == 1 ? /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]*$/ : /^[a-zA-ZáéíóúÁÉÍÓÚñÑ]*$/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'c':
                    regExp = (b == 1 ? /^[a-záéíóúñ\s]*$/ : /^[a-záéíóúñ]*$/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'Cdot':
                    regExp = (b == 1 ? /^[a-zA-ZáéíóúÁÉÍÓÚ.ñÑ\s]*$/ : /^[a-zA-ZáéíóúÁÉÍÓÚ.ñÑ]*$/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'T':
                    regExp = (b == 1 ? /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]*$/ : /^([\s,._\-/\\¿?]*[a-zA-ZáéíóúÁÉÍÓÚñÑ]*)*$/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 't':
                    regExp = (b == 1 ? /^[a-záéíóúñ\s]*$/ : /^([\s,._\-/\\¿?]*[a-záéíóúu00f1]*)*$/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'm':
                    regExp = (valGeneral == 0 ? /^([\w.\-@]*[a-zA-Z]*)*$/ : /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'L':
                    regExp = (valGeneral == 0 ? /^([\w.\-/:]*[a-z]*)*$/ : /https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'K':
                    regExp = (b == 1 ? /^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]*$/ : /^[a-zA-Z0-9áéíóúÁÉÍÓÚ@ñÑ]*$/);
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'k':
                    regExp = /^[0-9\.]*$/;
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'p':
                    regExp = /^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ]*$/;
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
                case 'j':
                    regExp = /^[a-zA-Z0-9áéíóúÁÉÍÓÚ\/ñÑ]*$/;
                    if (!$(this).val().match(regExp)) {
                        bandValida = false;
                        camposErroneos.push($(this).prop('id').toString());
                    }
                    break;
            }
        }
        if ($.inArray($(this).prop('id'), camposErroneos) >= 0) {
            $(this).change(function () { globalValidate(); });
        }
    });
    console.log(camposErroneos);

    $.each(camposErroneos, function (index, value) {
        $('#' + value + '').addClass('inputError');
    });

    return bandValida;
}

//Funciones para menu principal		
function MuestraMenuTodo(sTagName) {
    var options = {};
    var objMenu = $('tr[class *= submenu]');
    objMenu.show('fast');
}

function OcultaMenuTodo(sTagName) {
    var subMenu = $('tr[class *= submenu]')
    var options = {};
    subMenu.hide("clip", options, 200);
}

function MuestraMenu(idObj) {
    objMenu = $("[id$=Menu" + idObj + "]");
    if (objMenu[0].style.display == "none") {
        objMenu.show('fast');
    }
    else {
        var options = {};
        objMenu.hide("clip", options, 200);
    }
    var menu = $(".allGv");

    var links = $("td", menu);

    $.each(links, function (key) {

        if (links[key].innerHTML == "Notas Externas") {

            //links[key].attributes.onclick.textContent = "window.open('CajadeNotasExternas.aspx','CajadeNotas','OtherPage');"

            links[key].attributes.onclick.textContent = "window.open('CajadeNotasExternas.aspx','CajadeNotas','width=800,height=550,left=750,top=0,resizable');"

        }

        if (links[key].innerHTML == "Notas Internas") {

            links[key].attributes.onclick.textContent = "window.open('CajadeNotas.aspx','CajadeNotas','width=800,height=550,left=750,top=0,resizable');"

            //links[key].attributes.onclick.textContent = "window.open('CajadeNotas.aspx', 'OtherPage');"

        }

        if (links[key].innerHTML == "Historial") {

            links[key].attributes.onclick.textContent = "window.open('HistorialNotas.aspx','HistorialNotas','width=800,height=550,left=750,top=0,resizable');"



        }
        if (links[key].innerHTML == "Detalle Impagos") {

            //links[key].attributes.onclick.textContent = "window.open('CajadeNotasExternas.aspx','CajadeNotas','OtherPage');"

            links[key].attributes.onclick.textContent = "window.open('DetalleImpagos.aspx','DetalleImpagos','width=800,height=550,left=750,top=0,resizable');"
        }



    });

}

function CargaPantalla(url) {
    document.location = url;
}

function confirmacion(msjCnfm) {
    if (confirm(msjCnfm)) {
        event.returnValue = true;
    }
    else {
        event.returnValue = false;
    }
}

function CierraModal(id) {
    parent.$find(id).hide();
    parent.document.location.reload();
    event.returnValue = true;
}

function CierraCalendario(Calendario) {
    Calendario.hide();
    Calendario.get_element().blur();
}

function Toggle(item) {
    obj = document.getElementById(item);
    visible = (obj.style.display != "none")
    key = document.getElementById("x" + item);
    if (visible) {
        obj.style.display = "none";

    } else {
        obj.style.display = "block";

    }
}

function Expand() {
    divs = document.getElementsByTagName("DIV");
    for (i = 0; i < divs.length; i++) {
        divs[i].style.display = "block";
        key = document.getElementById("x" + divs[i].id);
        key.innerHTML = "<img src='textfolder.gif' width='16' height='16' hspace='0' vspace='0' border='0'>";
    }
}

function Collapse() {
    divs = document.getElementsByTagName("DIV");
    for (i = 0; i < divs.length; i++) {
        divs[i].style.display = "none";
        key = document.getElementById("x" + divs[i].id);
        key.innerHTML = "<img src='folder.gif' width='16' height='16' hspace='0' vspace='0' border='0'>";
    }
}



function ValidaFecha() {

    var fechaIni = document.getElementById("ctl00_ctl00_cphCuerpo_cphPantallas_TxtFIniEstimada");
    var fechaFin = document.getElementById("ctl00_ctl00_cphCuerpo_cphPantallas_TxtFFinEstimada");
    var horas = document.getElementById("ctl00_ctl00_cphCuerpo_cphPantallas_TxtHrsHEstimada")
    if ((fechaIni != null) || (fechaFin != null)) {
        if (fechaIni.value == "") {
            alert("El valor de la fecha Inicio Estimada es requerido");
            return false;
        }
        else {
            if (fechaFin.value == "") {
                alert("El valor de la fecha Fin Estimada es requerido");
                return false;
            }
            else {
                if (horas.value == "") {
                    alert("El valor de las Horas estimadas es requerido");
                    return false;
                }
                else { return true; }
            }
        }
    }
}

function centraVentana(muestra, base) {
    //Obtenemos ancho y alto del navegador, y alto y ancho de la popUp

    base = (typeof base === 'undefined') ? '' : base;

    var windowWidth;
    var windowHeight;

    if (base != '') {
        windowWidth = base.width();
        windowHeight = base.height();
    }
    else {
        windowWidth = $(window).width();
        windowHeight = $(window).height();
    }
                
    muestra.width(windowWidth * .4);
    muestra.height(windowHeight * .25);
    
    popupWidth = muestra.width();
    popupHeight = muestra.height();
    
    muestra.css({ "position": "absolute", "top": (windowHeight / 2) - (popupHeight / 2) + "px", "left": (windowWidth / 2) - (popupWidth / 2) + "px" });
}

function ocultaVentana(oculta, muestra) {    
    oculta.css({ "opacity": "100" });
    muestra.hide("puff", options, 1000, '');
}

/*      oculta ventana rapido       */

function ocultaVentanaFast(oculta, muestra) {
    oculta.css({ "opacity": "100" });
    muestra.hide("puff", options, 10, '');
}

function ocultaVentanaWP(oculta, muestra) {
    oculta.css({ "opacity": "100" });
    muestra.fadeOut(100000, "linear", __doPostBack('', ''));
}

function muestraVentana(oculta, muestra) {    
    oculta.css({ "opacity": ".20" });      
    muestra.show("slow");
}


/*  funcion limpiar */
function fnlimpiar(r) {
    r.onreadystatechange = null;
    r.abort = null;
    r = null;
}
/*  funcion limpiar */

/*  funcion para el llenado de un dropdownlist  por medio de ajax   */

function fillddl(ddl, depende) {
    var ddlobj = $("[id$=ddl" + ddl + "]");

    var dlls = depende.split(',');
    var i = dlls.length;
    var ddldependeval = new Array();
    while (i > 0) {
        i--;
        ddldependeval[i] = $("[id$=" + dlls[i] + "]").val();
    }
    var totval = ddldependeval.join();

    var data = "{ddl: '" + ddl + "', depende: '" + totval + "'}"

    var request = $.ajax({
        async: false,
        type: "POST",
        url: "../fillobjetos.asmx/llenaddl",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            ddlobj.empty();
            $.each(msg.d, function () {
                ddlobj.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
            },
        error: function (msg) { PopUpLetreroWP('Se ha sobrepasado el tiempo de espera'); }
    })
   fnlimpiar(request);
}

function validaScoring(cadena, depende,depende1) {
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "'}"

   var request= $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/validaScoring",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { $("[id$=lblMensaje]").text(msg.d); muestraVentana($('#Principal'), $('#ventanaconfirm')); centraVentana($('#ventanaconfirm')); ocultaVentana($("#Principal"), $("#ventanaconfirm")); fillgv(depende, depende1); },
        error: function (msg) { alert(msg.d); }
    })
   fnlimpiar(request);
}

function btnInsertarWP(cadena, pantalla, solicitud, persona, usuario) {
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "', pantalla: '" + pantalla + "', solicitud: '" + solicitud + "', persona: '" + persona + "', usuario: '" + usuario + "'}";
    $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnInsertar",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { PopUpLetreroWP(msg.d); },
        error: function (msg) { PopUpLetrero(msg.d); }
    })
    fnlimpiar(request);
}

function btnInserteaBoton(cadena, pantalla, solicitud, persona, usuario, idusuario, passwor, idpantalla, strStatus, intBandera, strMotivo) {
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "', pantalla: '" + pantalla + "', solicitud: '" + solicitud + "', persona: '" + persona + "', usuario: '" + usuario + "', cveusuario: '" + idusuario + "', paswword: '" + passwor + "', idpantalla: '" + idpantalla + "', strStatus: '" + strStatus + "', intbandera: '" + intBandera + "',strMotivo:'" + strMotivo + "' }";
    var boton = $("[id$= btnGuardarAutoriza]")[0]
    var ruta = $("[id$=hdnResultado]").val();
    if (intBandera == 1) {
        var modalAutoriza = $find('ctl00_ctl00_cphCuerpo_cphPantallas_mpoAutorizar')   // $("[id$=mpoAutorizar]")[0]
    } else { var modalAutoriza = $find('ctl00_ctl00_cphCuerpo_cphPantallas_mpoCancela') }


   var request= $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnInsertar",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { if (msg.d.indexOf("error") === -1) { PopUpLetreroRedirect(msg.d, ruta) } else {  modalAutoriza.hide();  PopUpLetrero(msg.d); }; },
        error: function (msg) { PopUpLetrero(msg.d); }
    })
   fnlimpiar(request);
}

//'ERODRIGUEZ: Se duplico la funcion btnInserteaBoton para permitir cancelar al estar equivocados los parametros que se envian en tipo y orden
//JBEJAR CAMBIO URGENTE FUNCION CANCELAR ADELANTABA A LAS AUTOMATICAS SI TENIA ESE VALOR.
function btnInserteaBoton_dos(cadena, pantalla, solicitud, persona, usuario, idusuario, passwor, idpantalla, strStatus, intBandera, strMotivo) {
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "', pantalla: '" + pantalla + "', solicitud: '" + solicitud + "', persona: '" + persona + "', usuario: '" + usuario + "', cveusuario: '" + idusuario + "', paswword: '" + passwor + "', idpantalla: '" + idpantalla + "', strStatus: '" + strStatus + "', intbandera: '" + intBandera + "',strMotivo:'" + strMotivo + "' }";
    var boton = $("[id$= btnGuardarAutoriza]")[0]
    var ruta = '../aspx/consultaPanelControl.aspx'
    if (intBandera == 1) {
        var modalAutoriza = $find('ctl00_ctl00_cphCuerpo_cphPantallas_mpoAutorizar')   // $("[id$=mpoAutorizar]")[0]
    } else { var modalAutoriza = $find('ctl00_ctl00_cphCuerpo_cphPantallas_mpoCancela') }


    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnInsertar_dos",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { if (msg.d.indexOf("error") === -1) { PopUpLetreroRedirect(msg.d, ruta) } else { modalAutoriza.hide(); PopUpLetrero(msg.d); }; },
        error: function (msg) { PopUpLetrero(msg.d); }
    })
    fnlimpiar(request);
}

function btnInsertar(cadena, pantalla, solicitud, persona, usuario, idusuario, passwor, idpantalla, strStatus, intBandera ) {
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "', pantalla: '" + pantalla + "', solicitud: '" + solicitud + "', persona: '" + persona + "', usuario: '" + usuario + "', cveusuario: '" + idusuario + "', paswword: '"+ passwor + "', idpantalla: '" + idpantalla + "', strStatus: '"+ strStatus+ "', intbandera: '" + intBandera + "' }";   
    $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnInsertar",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { if (msg.d.indexOf("error") === -1) { PopUpLetrero(msg.d);  } else { PopUpLetrero(msg.d);  }; },
        error: function (msg) { PopUpLetrero(msg.d); }
    })
    fnlimpiar(request);
}

/*  funcion que sirve para actualizar, insertar o borrar un registro regresa el mensaje se actualizo correctamente.. o existio un error si no actualiza nada */

function btnInsUpd(cadena, Opcionalfuncion) {
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "' }";
    Opcionalfuncion = (typeof Opcionalfuncion === "undefined") ? "" : Opcionalfuncion;    
    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnInsUpd",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (Opcionalfuncion != "undefined") { eval(Opcionalfuncion) }
            else if (msg.d.indexOf("error") === -1) { PopUpLetrero(msg.d); }
            else { PopUpLetrero(msg.d); }
        },
        error: function (msg) { if (msg.d.length > 0) { PopUpLetrero(msg.d); } else { PopUpLetrero("Se excedio el tiempo de respuesta") } }
    })
    fnlimpiar(request);
}

/*  Ejecuta la consulta, el resultado de la primera fila, la primera columna es devuelta e ingresada en el objeto que se selecciono. */

function btnConsultaAObjeto(cadena, objeto) {
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "' }";
    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnInsertDocumento",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { objeto.val(msg.d) },
        error: function (msg) { if (msg.d.length > 0) { PopUpLetrero(msg.d); } else { PopUpLetrero("Se excedio el tiempo de respuesta") } }
    })
    fnlimpiar(request);
}

function btnInsUpdMultiTab(cadena, valor, depende, funcion, fnValorDep) {
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "' }";
    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnInsUpd",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { PopUpLetreroMultiTab(msg.d, valor, depende, funcion, fnValorDep) },
        error: function (msg) { PopUpLetrero(msg.d) }
    })
    fnlimpiar(request);
}

function btnInsUpdRapido(cadena) {
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "' }";
    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnInsUpd",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { if (msg.d.indexOf("error") === -1) { PopUpLetreroRapido(msg.d); } else { PopUpLetreroRapido(msg.d); } },
        error: function (msg) { if (msg.d.length > 0) { PopUpLetrero(msg.d); } else { PopUpLetrero("Se excedio el tiempo de respuesta") } }
    })
    fnlimpiar(request);
}


function btnEntrevista(cadena) {
    $("[id$=lblMensaje]").text('Espere un momento...');
    centraVentana($('#ventanaconfirm'));

    $('#ventanaContain').show();
    $('#ventanaconfirm').show();
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "' }";
    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnInsUpd",
        data: data,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d.indexOf("error") != -1) { PopUpLetreroRapido(msg.d); }
            $('#ventanaContain').hide();
            $('#ventanaconfirm').hide();
        },
        error: function (msg) {
            if (msg.d.length > 0) { PopUpLetrero(msg.d); } else { PopUpLetrero("Se excedio el tiempo de respuesta") }
            $('#ventanaContain').hide();
            $('#ventanaconfirm').hide();
        }
    })
    fnlimpiar(request);
}

/*  funcion que sirve para actualizar, insertar o borrar un registro y posteriormente te redirecciona a la pagina deseada */

function btnInsUpdRedirect(cadena) {
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "' }";
    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/validaScoring",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { if (msg.d.indexOf("error") === -1) { window.location.replace(msg.d); } else { PopUpLetrero(msg.d); } },
        error: function (msg) { if (msg.d != undefined) { PopUpLetrero(msg.d); } else { PopUpLetrero("Se excedio el tiempo de respuesta") } }
    })
    fnlimpiar(request);
}
/* funcion para manejo de errores. */

function btnManejoMensaje(cadena, cadena2) {
    /*BBV-P-423_RQADM-17*/
    $('#ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1C').attr('disabled', true);
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "', cadena2: ' " + cadena2 + "'}";
    var ruta = $('[id$=hdnResultado]').val();
    var boton = $("[id$= cmbguardar1]")
    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnManejoMensaje",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if ((msg.d.indexOf("pregunta") === -1) && (msg.d.indexOf("Falta") === -1) && (msg.d.indexOf("error") === -1))
            {
                PopUpLetreroRedirect(msg.d, ruta)
            } else {
                PopUpLetrero(msg.d); boton.removeAttr('disabled', ''); inputfile = $("[id^=yourID]"); $(inputfile).attr("disabled", false);
            }
        },
        error: function (msg) {
            if (msg.d == null)
            {
                PopUpLetrero("Error En Conexion Intentar Nuevamente"); return;
            }
            if (msg.d.length > 0)
            {
                PopUpLetrero(msg.d);
            } else {
                PopUpLetrero("Se excedio el tiempo de respuesta")
            }
        }
    })
    fnlimpiar(request);
}
/*  funcion que sirve para actualizar, insertar o borrar un registro, posteriormente recarga la tabla deseada. */

function btnInsUpdReload(cadena, tabla, depende, upload, valorupl) {
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "' }";
    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnInsUpd",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { fillUpload(tabla, depende, upload, valorupl); /*PopUpLetreroUpload(msg.d, tabla, depende, upload, valorupl) */ },
        error: function (msg) { if (msg.d.length > 0) { PopUpLetrero(msg.d); } else { PopUpLetrero("Se excedio el tiempo de respuesta") } }
    })
    fnlimpiar(request);
}

/*  funcion que sirve para actualizar, insertar o borrar un registro, posteriormente realiza postback. */

function btnInsUpdWP(cadena) {
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "' }";
    $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnInsUpd",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { if (msg.d.indexOf("error") === -1) { PopUpLetreroWP(msg.d); } else { PopUpLetrero(msg.d); } },
        error: function (msg) { PopUpLetrero(msg.d); }
    })
    fnlimpiar(request);
}


/*  funcion que inserta y regresa el mensaje en el campo de mensaje */

function btnInsertDocumento(cadena) {
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "' }";
    var ruta = $("[id$=hdnResultado]").val();
    var ruta1 = $("[id$=hdnResultado1]").val();
    var ruta2 = $("[id$=hdnResultado2]").val();
    var boton = $("[id$= cmbguardar1]")
    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnInsertDocumento",      
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d.indexOf("error") != -1 || msg.d.indexOf("ERROR") != -1) { PopUpLetrero(msg.d); } else if (msg.d.indexOf("RECHAZO") != -1) { PopUpLetreroRedirect(msg.d, ruta1) } else if (msg.d.indexOf("Faltan") != -1 || msg.d.indexOf("Falta") != -1) { PopUpLetrero(msg.d); boton.removeAttr('disabled', ''); inputfile = $("[id^=yourID]"); $(inputfile).attr("disabled", false); } else { PopUpLetreroRedirect(msg.d, ruta2) }
        },
        error: function (msg) { PopUpLetrero(msg.d); }
    })
    fnlimpiar(request);
}

function btnImprimirCto(cadena, contrato, cliente, empresa, moneda, toperacion, pjuridica) {
   cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "', contrato: '" + contrato + "', cliente: '" + cliente + "', empresa: '" + empresa + "', moneda: '" + moneda + "', toperacion:'" + toperacion + "', pjuridica: '" + pjuridica +  "'}";

    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnImprimirCto",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { var ruta = msg.d.split("|"); var i = ruta.length; while (i > 0) { i--; window.open('..' + ruta[i]) } },
        //window.open("/Documentos/01 CONTRATO1_Contrato 645CA.rtf")
////        window.location.replace(ruta[i])
        error: function (msg) { PopUpLetrero(msg.d); }
    })
    fnlimpiar(request);
}
function btnInsertarPanta(cadena, pantalla, solicitud, persona, usuario, redirect, mensaje) {
    if (redirect == undefined) {
        redirect = true;
    } else {
        redirect = redirect;
    }
    if (mensaje == undefined) {
        mensaje = 'Registro guardado exitosamente.';
    } else {
        mensaje = mensaje;
    }
    cadena = cadena.replace(/'/g, "\\'");
    data = "{cadena: '" + cadena + "', pantalla: '" + pantalla + "', solicitud: '" + solicitud + "', persona: '" + persona + "', usuario: '" + usuario + "'}";
    var boton = $("[id$=Button1]")[0]
    var ruta = $("[id$=hdnResultado]").val();
    if (pantalla == "PRECALIFICACION" || pantalla == "COACREDITADO") { var ruta1 = $("[id$=hdnResultado1]").val(); var ruta2 = $("[id$=hdnResultado2]").val(); }

    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/btnInsertarPanta",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == null) {
                PopUpLetreroRedirect("Registro guardado exitosamente.", ruta);
                return;
            }

            if (ruta2 == "") { ruta2 = '../aspx/consultaPanelControl.aspx'; }

            if (pantalla == "PRECALIFICACION" || pantalla == "COACREDITADO") {
                if (msg.d.indexOf("error") != -1 || msg.d.indexOf("ERROR") != -1)
                { alertJQUERY(msg.d) }
                else if (msg.d.indexOf("RECHAZO") != -1)
                { PopUpLetreroRedirect(msg.d, ruta2) }
                else if (msg.d.indexOf("CONDICIONADA") != -1)
                { PopUpLetreroRedirect(msg.d, ruta1) }
                else
                {
                    if (msg.d.indexOf("Error") != -1 || msg.d.indexOf("ERROR") != -1) {
                        PopUpLetreroRedirect(msg.d, ruta);
                    }
                    else {cambiaurl(solicitud, msg.d);}
                }
            }
            else if (msg.d.indexOf("error") != -1 || msg.d.indexOf("ERROR") != -1)
            { PopUpLetrero(msg.d); }

            else if (!redirect) { PopUpLetrero(mensaje); }

            else { PopUpLetreroRedirect(msg.d, ruta) };
        },
        error: function (msg) {PopUpLetrero(msg.d);}
    })
    fnlimpiar(request);
}

/*Funcion para cambiar la url*/
function cambiaurl(solicitud, msj) {
    var resul;
    data = "{solicitud:" + solicitud + "}";

    var request = $.ajax({
        type: 'POST',
        url: "../fillobjetos.asmx/urlnueva",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            ruta = msg.d
            if (ruta == "") {
                ruta = '"./consultaPanelControl.aspx"'
            }

            PopUpLetreroRedirect("", ruta)
        },
        error: function (msg) { PopUpLetrero(msg.d); }
    });
    fnlimpiar(request);
}

/*  funcion que realiza el llenado de un input type texbox  */

function filltxt(txt, depende) {
    var prueba = $("[id$=RN]").val()
    var txtnom = 'txt' + txt;
    // objeto cuadro de texto
    var txtobj = $("[id$=" + txtnom + "]");
    // valore(s) de donde depende
    var dlls = depende.split(',');
    var i = dlls.length;
    var ddldependeval = new Array();
    while (i > 0) {
        i--;
        ddldependeval[i] = $("[id$=" + dlls[i] + "]").val();
    }
    var totval = ddldependeval.join();

    //val de entrada a ws

    var data = "{valor: '" + txt + "', depende: '" + totval + "'}";

   var request= $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/filltxt",
        data: data,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            txtobj.val(msg.d);
        },
        error: function (msg) { PopUpLetreroWP('Se ha sobrepasado el tiempo de espera'); }
    })
   fnlimpiar(request);
}


/*  funcion que realiza el llenado de un label  */

function filllbl(label, depende) {    
    // objeto cuadro de texto
    var txtobj = $("[id$=" + label + "]");

    var dlls = depende.split(',');
    var i = dlls.length;
    var ddldependeval = new Array();
    while (i > 0) {
        i--;
        ddldependeval[i] = $("[id$=" + dlls[i] + "]").val();
    }
    var totval = ddldependeval.join();

    //val de entrada a ws

    var data = "{valor: '" + label + "', depende: '" + totval + "'}";

    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/filltxt",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            txtobj.text(msg.d);
        },
        error: function (msg) { PopUpLetreroWP('Se ha sobrepasado el tiempo de espera'); }
    })
    fnlimpiar(request);
}

/*     funcion que realiza el autocomplete de jquery    */

function fillAutocomplete(txt,depende) {
    
    // objeto cuadro de texto
    var txtobj = $("[id$=" + txt + "]");
    var totval = "";
    var matdepende = depende.split(",")
    var longmatdepende = matdepende.length;
    var mattotal = new Array();

    while (longmatdepende > 0) {
        longmatdepende--;
        mattotal[longmatdepende] = $("[id$=" + matdepende[longmatdepende] + "]").text();
    }

    totval = mattotal.join();

    //val de entrada a ws

    var data = "{valor: '" + txt + "', depende: '" + totval + "'}";

    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/filltxt",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            txtobj.autocomplete({
                source: msg.d.split(",")
            });
            $('#txtUsuAsign').focus();
        },
        error: function (msg) {
            alert('Ocurrio un error.');
            //PopUpLetreroWP('Se ha sobrepasado el tiempo de espera');
        }
    })
    fnlimpiar(request);
}

/*  funcion que realiza el llenado de una tabla por medio de html ....  */

function fillgv(valor, depende) {

    depende = (typeof depende === 'undefined') ? '' : depende;

    tabla = $("[id$=" + valor + "]")
    depende = depende.replace(/'/g, "\\'");

    var dlls = depende.split(',');
    var i = dlls.length;
    var ddldependeval = new Array();
    while (i > 0) {
        i--;
        ddldependeval[i] = $("[id$=" + dlls[i].trim() + "]").val();
    }

    var totval = ddldependeval.join();

    data = "{valor: '" + valor + "', depende: '" + totval + "'}"

    var request = $.ajax({
        async: false,
        url: "../fillobjetos.asmx/fillgv",
        data: data,
        type: "POST",
        timeout: 60000,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { tabla.html(msg.d); if (valor == "grvConsulta") { fnCargaPaginacion(); } },
        error: function (msg) { PopUpLetrero(msg.d) }
    });
    fnlimpiar(request);
}

function fillJSON(valor, depende) {

    tabla = $("[id$=" + valor + "]")
    depende = depende.replace(/'/g, "\\'");

    var dlls = depende.split(',');
    var i = dlls.length;
    var ddldependeval = new Array();
    while (i > 0) {
        i--;
        ddldependeval[i] = $("[id$=" + dlls[i].trim() + "]").val();
    }

    var totval = ddldependeval.join();

    data = "{valor: '" + valor + "', depende: '" + totval + "'}"

        var request = $.ajax({
            type: "POST",
            url: "../fillobjetos.asmx/fnFillJSON",
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                obj = $.parseJSON(msg.d)
                var respuesta = "";
                $.each(obj.REGISTRO, function (key) {
                    respuesta += obj.REGISTRO[key].resp;                    
                })
                tabla.html(respuesta);
                //alert('OK')
            },
            error: function (msg) { PopUpLetrero("error"); }
        })
        fnlimpiar(request);
}

function fillMultiTab(valor, depende, funcion, fnValorDep, enable) {
    enable = enable || "";
    tabla = $(valor);    
    depende = depende.replace(/'/g, "\\'");

    var dlls = depende.split(',');
    var i = dlls.length;
    var ddldependeval = new Array();
    while (i > 0) {
        i--;
        ddldependeval[i] = $("[id$=" + dlls[i].trim() + "]").val();
    }

    var totval = ddldependeval.join();

    //BUG-PD-99:MPUESTO:15/06/2017
    data = "{valor: '" + valor + "', depende: '" + totval + "', enable: '" + enable + "'}";

    var request = $.ajax({
        async: true,
        url: "../fillobjetos.asmx/fillMultiTab",
        data: data,
        type: "POST",
        timeout: 60000,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            obj = $.parseJSON(msg.d)
            $.each(obj.REGISTRO, function (key) {
                respuesta = obj.REGISTRO[key].resp
                tb = $('#' + tabla[key].id);
                tb.html(respuesta);
            })
            fnUploadtab(funcion, fnValorDep); 
        },
        error: function (msg) { if (msg.d.length > 0) { PopUpLetrero(msg.d); } else { PopUpLetrero("Se excedio el tiempo de respuesta") } }
    });

    fnlimpiar(request);

    function fnUploadtab(uploadUpl, valoruplUpl) {
        switch (uploadUpl) {
            case "SumaMontos":
                var importe = $('.importe');
                var sumaTotal = 0;
                $.each(importe, function (key) {
                    sumaTotal += parseFloat(importe[key].innerHTML, 2);
                })
                $('[id$=txtSumaFacturas]').val(sumaTotal);
                break;
        }
    }
}

/*  realiza el llenado de una tabla, y posteriormente realiza la tarea encomendada....  */

function fillUpload(valorUpl, dependeUpl, uploadUpl, valoruplUpl) {

    tablaUpl = $("[id$=" + valorUpl + "]")
    dependeUpl = dependeUpl.replace(/'/g, "\\'");    

    var dependeUplOr = dependeUpl;

    var dllsUpl = dependeUpl.split(',');
    var i = dllsUpl.length;
    var ddldependevalUpl = new Array();
    while (i > 0) {
        i--;

        if (dllsUpl[i].trim() == 'hdnIdPantalla') {
            dependeUpl = dependeUpl.replace('hdnIdPantalla', 'hdPantalla');
            ddldependevalUpl[i] = 63;
        } else {
        ddldependevalUpl[i] = $("[id$=" + dllsUpl[i].trim() + "]").val();
            if (dependeUplOr.match('hdnIdPantalla')) {
            dependeUpl = dependeUpl.replace('hdnIdFolio', 'hdSolicitud');
            dependeUpl = dependeUpl.replace('hdnUsua', 'hdusuario');
            //dependeUpl = 'hdBandera'
            }
        }
    }

    var totvalUpl = ddldependevalUpl.join();

    dataUpl = "{objeto: '" + valorUpl + "', dependeObjeto: '" + dependeUpl + "', dependeValor: '" + totvalUpl + "'}"

    var requestUpl = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/newFillgv",
        data: dataUpl,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //BUG-PD-26 MAPH 06/04/2017 Inclusión de función para borrar inputs y seleccionar valor por defecto en DropDownLists
        success: function (msg) {tablaUpl[0].innerHTML = ""; tablaUpl.html(msg.d); fnUploadtab(uploadUpl, valoruplUpl); try { fnLimpiaSeccion(); } catch (err) { } },
        error: function (msg) { /*alert(msg.d);*/ }
    })

    fnlimpiar(requestUpl);

    /*  function reload de fillupload   */

    function fnUploadtab(uploadUpl, valoruplUpl) {
        if (uploadUpl == 'muestra') {
            $('#' + valoruplUpl).show('fast');
        }
        else if (uploadUpl == 'oculta') {
            $('#' + valoruplUpl).hide('fast');
        }
        else if (uploadUpl == 'SumaMontos') {
            var importe = $('.importe');
            var sumaTotal = 0;
            $.each(importe, function (key) {                
                sumaTotal += parseFloat(importe[key].innerHTML, 2);
            })
            $('[id$=txtSumaFacturas]').val(sumaTotal);
        }
        else if (uploadUpl == "per1") {

            fnUpload();

            var cheksUpl = $("[name^=chkbxRec]");
            var countUpl = 0;


            cheksUpl.each(function () {
                if ($(this).is(':checked')) {
                    var arrChk = new Array()
                    arrChk = $(this)[0].name.split('_');
                    var hedrec = $('#selMR' + arrChk[1]);
                    hedrec.show('fast', "swing");
                    hedrec.addClass('Text');

                    countUpl++;
                }
                else {
                    var arrChk = new Array()
                    arrChk = $(this)[0].name.split('_');
                    var hedrec = $('#selMR' + arrChk[1]);
                    hedrec.hide('fast', "swing");
                    hedrec.addClass('Text');
                }
            })

            if (countUpl > 0) {
                $('[id$=MovRec]').show('fast');
            }
            else {
                $('[id$=MovRec]').hide('fast');
            }
        }
    }
}

/**********************************************replica*/
function fillUpload2(valorUpl, dependeUpl, uploadUpl, valoruplUpl) {

    tablaUpl = $("[id$=" + valorUpl + "]")
    dependeUpl = dependeUpl.replace(/'/g, "\\'");

    var dependeUplOr = dependeUpl;

    var dllsUpl = dependeUpl.split(',');
    var i = dllsUpl.length;
    var ddldependevalUpl = new Array();
    while (i > 0) {
        i--;

        if (dllsUpl[i].trim() == 'hdnIdPantalla') {
            dependeUpl = dependeUpl.replace('hdnIdPantalla', 'hdPantalla');
            ddldependevalUpl[i] = 63;
        } else {
            ddldependevalUpl[i] = $("[id$=" + dllsUpl[i].trim() + "]").val();
            if (dependeUplOr.match('hdnIdPantalla')) {
                dependeUpl = dependeUpl.replace('hdnIdFolio', 'hdSolicitud');
                dependeUpl = dependeUpl.replace('hdnUsua', 'hdusuario');
                dependeUpl = 'hdBandera'
            }
        }
    }

    var totvalUpl = ddldependevalUpl.join();

    dataUpl = "{objeto: '" + valorUpl + "', dependeObjeto: '" + dependeUpl + "', dependeValor: '" + totvalUpl + "'}"

    var requestUpl = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/newFillgv",
        data: dataUpl,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //BUG-PD-26 MAPH 06/04/2017 Inclusión de función para borrar inputs y seleccionar valor por defecto en DropDownLists
        success: function (msg) {tablaUpl[0].innerHTML = ""; tablaUpl.html(msg.d); fnUploadtab(uploadUpl, valoruplUpl); try { fnLimpiaSeccion(); } catch (err) { } },
        error: function (msg) { alert(msg.d); }
    })

    fnlimpiar(requestUpl);

    /*  function reload de fillupload   */

    function fnUploadtab(uploadUpl, valoruplUpl) {
        if (uploadUpl == 'muestra') {
            $('#' + valoruplUpl).show('fast');
        }
        else if (uploadUpl == 'oculta') {
            $('#' + valoruplUpl).hide('fast');
        }
        else if (uploadUpl == 'SumaMontos') {
            var importe = $('.importe');
            var sumaTotal = 0;
            $.each(importe, function (key) {                
                sumaTotal += parseFloat(importe[key].innerHTML, 2);
            })
            $('[id$=txtSumaFacturas]').val(sumaTotal);
        }
        else if (uploadUpl == "per1") {

            fnUpload();

            var cheksUpl = $("[name^=chkbxRec]");
            var countUpl = 0;


            cheksUpl.each(function () {
                if ($(this).is(':checked')) {
                    var arrChk = new Array()
                    arrChk = $(this)[0].name.split('_');
                    var hedrec = $('#selMR' + arrChk[1]);
                    hedrec.show('fast', "swing");
                    hedrec.addClass('Text');

                    countUpl++;
                }
                else {
                    var arrChk = new Array()
                    arrChk = $(this)[0].name.split('_');
                    var hedrec = $('#selMR' + arrChk[1]);
                    hedrec.hide('fast', "swing");
                    hedrec.addClass('Text');
                }
            })

            if (countUpl > 0) {
                $('[id$=MovRec]').show('fast');
            }
            else {
                $('[id$=MovRec]').hide('fast');
            }
        }
    }
}

/*  llena la marquesina */

function fillmarquee(valor, depende) {

    tabla = $("[id$=" + valor + "]")
    depende = depende.replace(/'/g, "\\'");

    var dlls = depende.split(',');
    var i = dlls.length;
    var ddldependeval = new Array();
    while (i > 0) {
        i--;
        ddldependeval[i] = $("[id$=" + dlls[i] + "]").val();
    }

    var totval = ddldependeval.join();

    data = "{valor: '" + valor + "', depende: '" + totval + "'}"

    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/fillgv",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) { tabla.html(msg.d); tabla.webTicker(); },
        error: function (msg) { alert(msg.d); }
    })
    fnlimpiar(request);
}

function fnChangeColor(td) {
    $(td).addClass('changecolortd')
}


/*  envia mensaje a pantalla por medio de un div    */

function PopUpLetrero(mesaje) {
    $("[id$=lblMensaje]").text(mesaje);
    centraVentana($('#ventanaconfirm'));
    //$('#ventanaconfirm').show('blind', options, 10000, '');
    //$('#ventanaconfirm').hide('blind', options, 10000, '');

    $('#ventanaContain').show();
    $('#ventanaconfirm').show();
   
    setTimeout(function () {
        $('#ventanaContain').hide();
        $('#ventanaconfirm').hide();
    }, 4000);
}

function PopUpLetreroMultiTab(mesajeM, valorM, dependeM, funcion, fnValorDep) {
    $("[id$=lblMensaje]").text(mesajeM);
    centraVentana($('#ventanaconfirm'));
    /*$('#ventanaconfirm').show('blind', options, 4000, '');
    $('#ventanaconfirm').hide('blind', options, 4000, function () { fillMultiTab(valorM, dependeM, funcion, fnValorDep) });*/

    $('#ventanaContain').show();
    $('#ventanaconfirm').show();

    setTimeout(function () {
        $('#ventanaContain').hide();
        $('#ventanaconfirm').hide();
        fillMultiTab(valorM, dependeM, funcion, fnValorDep);
    }, 4000);
}

function PopUpLetreroUpload(mensajeUp, tablaUp, dependeUp, uploadUp, valoruplUp) {
    $("[id$=lblMensaje]").text(mensajeUp);
    centraVentana($('#ventanaconfirm'));
    //$('#ventanaconfirm').show('blind', options, 4000, '');
    //$('#ventanaconfirm').hide('blind', options, 4000, function () { fillUpload(tablaUp, dependeUp, uploadUp, valoruplUp); });    

    $('#ventanaContain').show();
    $('#ventanaconfirm').show();

    setTimeout(function () {
        $('#ventanaContain').hide();
        $('#ventanaconfirm').hide();
        fillUpload(tablaUp, dependeUp, uploadUp, valoruplUp);
    }, 4000);
}

function PopUpLetreroRedirect(mesajeRe, redirect) {
    $('input[type=button]').prop('disabled', true);
    if (mesajeRe.length > 0) {
        $("[id$=lblMensaje]").text(mesajeRe);
        centraVentana($('#ventanaconfirm'));
        //$('#ventanaconfirm').show('blind', options, 4000, '');
        //$('#ventanaconfirm').hide('blind', options, 4000, function () {window.location.replace(redirect)});

        $('#ventanaContain').show();
        $('#ventanaconfirm').show();

        setTimeout(function () {
            //$('#ventanaContain').hide();
            //$('#ventanaconfirm').hide();
            window.location.replace(redirect);
        }, 4000);

    } else { window.location.replace(redirect); /*$('#ventanaconfirm').hide('blind', options, 4000, function () { window.location.replace(redirect) }); */ }
}

function PopUpLetreroRapido(mesajeRa) {
    $("[id$=lblMensaje]").text(mesajeRa);
    centraVentana($('#ventanaconfirm'));
    //$('#ventanaconfirm').show('blind', options, 500, '');
    //$('#ventanaconfirm').hide('blind', options, 500, '');

    $('#ventanaContain').show();
    $('#ventanaconfirm').show();

    setTimeout(function () {
        $('#ventanaContain').hide();
        $('#ventanaconfirm').hide();
    }, 500);
}

/*  envia mensaje y realiza postback    */

function PopUpLetreroWP(mesaje) {
    $("[id$=lblMensaje]").text(mesaje);
    //muestraVentana($('#Principal'), $('#ventanaconfirm'));
    centraVentana($('#ventanaconfirm'));
    //$('#ventanaconfirm').show('blind', options, 4000, '');
    //$('#ventanaconfirm').hide('blind', options, 4000, function () { __doPostBack('', '') });    
    //ocultaVentanaWP($("#Principal"), $("#ventanaconfirm"));    

    $('#ventanaContain').show();
    $('#ventanaconfirm').show();

    setTimeout(function () {
        $('#ventanaContain').hide();
        $('#ventanaconfirm').hide();
        __doPostBack('', '');
    }, 4000);
}

function fnValidaForMatematica(formula) {
    var operadores = new Array("[+]", "[-]", "[*]", "[/]", "[(]", "[)]", "[AND]", "[OR]");
    var operadoresNF = new Array("[+]", "[-]", "[*]", "[/]", "[(]", "[AND]", "[OR]");
    var operadoresNI = new Array("[+]", "[-]", "[*]", "[/]", "[)]", "[AND]", "[OR]");
    var countA = 0;
    var countC = 0;
    var valida = true

    formula = formula.replace(/]/g, "]|");
    arrayformula = formula.split("|");
    for (var i = 0; i < operadoresNI.length; i++) {
        if (arrayformula[0] == operadoresNI[i]) {
            PopUpLetrero('Operador de inicio incorrecto');
            fnBtnBorrar();
            return;
        }
        else if (arrayformula[arrayformula.length - 2] == operadoresNF[i]) {
            valida = false;
        }
    }

    for (var i = 0; i < arrayformula.length - 1; i++) {
        for (var ai = 0; ai < operadores.length; ai++) {
            if (arrayformula[i] == operadoresNI[ai]) {
                for (var ai = 0; ai < operadores.length; ai++) {
                    if (arrayformula[i - 1] == operadoresNF[ai]) {
                        PopUpLetrero('No se puede tener dos operadores seguidos');
                        fnBtnBorrar();
                        return;
                    }
                }
            }
        }
        if (arrayformula[i].replace(/]/g, "").replace("[", "") == "(") {
            countA++;
        }
        if (arrayformula[i].replace(/]/g, "").replace("[", "") == ")") {
            countC++;
        }
        if (i >= 1) {
            if (isNaN(parseInt(arrayformula[i].replace(/]/g, "").replace("[", ""))) == true || arrayformula[i].replace(/]/g, "").replace("[", "") == "(" || arrayformula[i].replace(/]/g, "").replace("[", "") == ")") {
                if (arrayformula[i].replace(/]/g, "").replace("[", "") == ")" && countA < countC) {
                    fnBtnBorrar();
                }
            }
            else {
                if (isNaN(parseInt(arrayformula[i - 1].replace(/]/g, "").replace("[", "")))) {
                    if (arrayformula[i].replace(/]/g, "").replace("[", "") == "(" || arrayformula[i].replace(/]/g, "").replace("[", "") == ")") {
                        fnBtnBorrar();
                    }
                }
                else {
                    PopUpLetrero('Se necesita un operador');
                    fnBtnBorrar();
                    return;
                }
            }
        }
    }
    if (countA != countC || i == 1) {
        valida = false;
    }
    return valida;
}


/*  abre una nueva ventana  */

function CallWindow(page) {
    window.open(page, "", "height=600,width=800,status=yes,location=no,toolbar=no,menubar=no,scrollbars=yes", "");
    return false;
}

function searchTable(inputVal, table) {
    //var table = $("[id$=grvDistribu]");
    table.find('tr').each(function (index, row) {
        var allCells = $(row).find('td');
        if (allCells.length > 0) {
            var found = false;
            allCells.each(function (index, td) {
                var regExp = new RegExp(inputVal, 'i');
                if (regExp.test($(td).text())) {
                    found = true;
                    return false;
                }
            });
            if (found == true) $(row).show(); else $(row).hide();
        }
    });
}


// obtener el valor de una variable.

function getVarUrl(name) {
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var tmpURL = window.location.href;
    var results = regex.exec(tmpURL);
    if (results == null)
        return "";
    else
        return results[1];
}

/*      function para realizar la subida de los documentos      */

function fnUpload() {
  
    inputfile = $("[id^=yourID]");
    var idsol = $("[id$=lblSolicitud]");
    

        $(inputfile).fileupload({
            url: '../upload.ashx',
            replaceFileInput: false,
            singleFileUploads: true,
            drop: function (e, data) {
                e.preventDefault();
                },

            beforeSend: function(xhr, data){
                
                xhr.setRequestHeader('iddoc', data.fileInput[0].id);
            },
            autoupload: false,
            change: function (e, data) {              
            
                if (data.files.length > 1) {
                    PopUpLetrero("Solo se puede cargar un archivo a la vez")
                    return false;
                }
                    if (data.files[0].size > 10000000) {
                        PopUpLetrero("El archivo no debe de exceder los 10MB")
                        return false;
                    }
                if (!(/\.(pdf)$/i).test(data.files[0].name)) {
                    PopUpLetrero('Solo se pueden cargar archivos pdf');
                    return false;
                }
                
            },
            add: function (e, data) {          
                data.submit();
            },
            success: function (e, data, result) {
                fnChequea();                
            },
            error: function (xhr, errorType, exception) {
                var responseText;
                try {
                    responseText = jQuery.parseJSON(xhr.responseText);
                    PopUpLetrero(responseText.Message);                 
                } catch (e) {
                    responseText = xhr.responseText;
                    PopUpLetrero(responseText);
                }
            }
        })
    }

    //inputfile.uploadify({
    //    "scriptData": { "id": inputfile.id, "foo": theString },
    //    "auto": "true",
    //    "buttonText": "Escoger Imagen",
    //    "buttonImage": "../App_Themes/Imagenes/upload-green.png",
    //    "cancelImg": "../App_Themes/Imagenes/uploadify-cancel.png",
    //    "fileDesc": "Imagen",
    //    "height": 30,
    //    "swf": "../js/uploadify.swf",
    //    "uploader": "../upload.ashx",
    //    "folder": "../uploads",
    //    "width": 30,
    //    "fileSizeLimit": "10MB",
    //    "onUploadSuccess": function (file, data, responce) { if (data != '') { fnChequea(file, data, idsol, $(this)) } else { PopUpLetrero("SE HA PRODUCIDO UN ERROR DURANTE EL GUARDADO DE LA INFORMACION...."); } },
    //    "onError": function (errorType) {
    //        PopUpLetrero("SE HA PRODUCIDO UN ERROR DE TIPO " + errorType + " DURANTE EL GUARDADO DE LA INFORMACION....");
    //    }
    //})


function fnRegresar() {
    window.location.replace("consultaPanelControl.aspx");
}

function fnProcesar(Pantalla) {
    nompantalla = Pantalla.split('/');
    tampantalla = nompantalla.length;
 //   switch (nompantalla[tampantalla - 1]) {
   //     case 'altaCotizacion.aspx':
            folio = $('[ID$=hdnIdFolio]').val();
            usuario = $('[ID$=hdnUsua]').val();
            btnInsertDocumento('exec spValNegocio ' + folio + ',64,' + usuario + ';')
     //       break;
    
}

var QueryString = function () {
    // This function is anonymous, is executed immediately and 
    // the return value is assigned to QueryString!
    var query_string = {};
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        // If first entry with this name
        if (typeof query_string[pair[0]] === "undefined") {
            query_string[pair[0]] = pair[1];
            // If second entry with this name
        } else if (typeof query_string[pair[0]] === "string") {
            var arr = [query_string[pair[0]], pair[1]];
            query_string[pair[0]] = arr;
            // If third or later entry with this name
        } else {
            query_string[pair[0]].push(pair[1]);
        }
    }
    return query_string;
}();

/*  carga paginacion    */

function fnCargaPaginacion() {
    $('table.resulGridPag').each(function () {
        var currentPage = 0;
        var numPerPage = 10;
        var numPaginas = 8;
        var $table = $(this);
        $table.bind('repaginate', function () {
            $table.find('tbody tr').hide().slice(currentPage * numPerPage, (currentPage + 1) * numPerPage).show();
        });
        $table.trigger('repaginate');
        var numRows = $table.find('tbody tr').length;
        var $pager = $('<div class="pager"></div>');
        if ($('[name=paginacion]').val() > 0) {
            $('<span class="page-number"></span>').text(' [ << ] ').bind('click', {
                newPage: page
            }, function (event) {
                $('[name=paginacion]').val(parseInt($('[name=paginacion]').val()) - 1);
                $('<div><img width="100px" height="100px" src="../App_Themes/Imagenes/ajax-loader.gif"></div>').css({'text-align' : 'center', height: '100px', padding: '30px'}).insertAfter($('table.resulGridPag'));
                $(this).unbind( "click" );
                $('table.resulGridPag tbody').remove();
                fillgv('grvConsulta', 'hdnClienteNo, paginacion');
            }).css( "width", "+=50" ).appendTo($pager).addClass('clickable');
        }
        var pageIndex = $('[name=paginacion]').val() * numPaginas;
        var npages = Math.ceil(numRows / numPerPage);
        for (var page = 0; page < npages; page++) {
            $('<span class="page-number"></span>').text(pageIndex + page + 1).bind('click', {
                newPage: page
            }, function (event) {
                currentPage = event.data['newPage'];
                $table.trigger('repaginate');
                $(this).addClass('active').siblings().removeClass('active');
            }).appendTo($pager).addClass('clickable');
        }

        if (npages == numPaginas) {
            $('<span class="page-number"></span>').text(' [ >> ] ').bind('click', {
                newPage: page
            }, function (event) {
                // BUG-PD-23 MAPH 30/03/2017 Corrección del paginador para que cargue las secciones mayores a 8
                $('[name=paginacion]').val(parseInt($('[name=paginacion]').val()) + 1);
                $('<div><img width="100px" height="100px" src="../App_Themes/Imagenes/ajax-loader.gif"></div>').css({'text-align' : 'center', height: '100px', padding: '30px'}).insertAfter($('table.resulGridPag'));
                $(this).unbind( "click" );
                $('table.resulGridPag tbody').remove();
                fillgv('grvConsulta', 'hdnClienteNo, paginacion');
            }).css( "width", "+=50" ).appendTo($pager).addClass('clickable');
        }

        if ($('[name=paginacion]').val() <= 0) {
        $pager.insertBefore($table).find('span.page-number:first').addClass('active');
        } else {
            $pager.insertBefore($table).find('span.page-number:nth-child(2)').addClass('active');
        }
    });
}

function fnOcultaObjetos(pantalla, perfil) {
        
    data = "{pantalla: '" + pantalla + "', perfil: '" + perfil + "'}";

    var request = $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/fnOcultaObjetos",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
                                    var arrObj = msg.d.split(',');
                                    $.each(arrObj, function (key, value) {
                                        //alert(key + ', ' + value);
                                        $('[id$=' + value + ']').hide();
                                    })
                                },
        error: function (msg) { if (msg.d.length > 0) { PopUpLetrero(msg.d); } else { PopUpLetrero("Se excedio el tiempo de respuesta") } }
    })
    fnlimpiar(request);    
}

function phoneValidate(phone) {
    if (phone != undefined) {
        if (phone.length < 10) {
            alert("El teléfono no puede ser menor a 10 dígitos");
        }
    }
}

function validaSolicitud(rfc) {
    var request= $.ajax({
        type: "POST",
        url: "../fillobjetos.asmx/fnValidaSolicitud",
        data: "{rfc: '" + rfc + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var res = JSON.parse(msg.d);
            console.log(res.REGISTRO.length);
            if (res.REGISTRO.length > 0) {
                alert('Se encontró una coincidencia exacta en la sucursal "' + res["REGISTRO"][0].NOMBRE_DISTRIBUIDOR + '" con fecha de: ' + res["REGISTRO"][0].PDK_FECHA_MODIF + ' imposible continuar');
                return false;
            } else {
                return true;
            }
        },
        error: function (msg) { PopUpLetreroWP('Error al validar solicitud en otras sucursales'); }
    })
}

function blockScreen(idScreen) {
    $("#divBlockPage").show();
}

function getUrlValue(name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results == null) {
        return null;
    }
    else {
        return results[1] || 0;
    }
}