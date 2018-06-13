//BBV-P-412:AVH:28/06/2016 RQ16: SE AGREGAN DOS PUNTOS FUNCION ValCarac OPCION 12
//BBVA-P-412 RQ WSA. WS GetLoan Products (RQ06, RQ09): AMR: 25/10/2016 WS GetLoan Products
//BUG-PC-02:  09/11/2016: AMR: Error en manejaProductos
//BUG-PC-09:AMR:16/11/2016 CUTARMA01:No pertime espacios en blanco al ingresar el nombre de la marca
//BUG-PC-13: AMR: 23/11/2016 Se quita valor por default en los combos.
//BUG-PC-14 2016-11-24 MAUT Se agregan opciones de validar caracteres
//BUG-PC-20: AVH: 30/11/2016 Se agrega opcion 18 ValCarac
//BUG-PC-21: AMR: 28/11/2016 El nombre de la Marca se muestra el texto en Mayúsculas.
//BBV-P-412:BUG-PC-23 JRHM: 30/11/2016 Se agrego opcion 19 a ValCarac para las tasas iva letras,numeros, espacios y '%'
//BBVA-P-423:RQ03 AVH: 15/12/2016 Se agrega a Prodesknet
//BUG-PD-16: MAPH: 10/03/2017 Se realiza merge con el Funciones.js para hommologar funcion valCarac y checkDecimals
//BBV-P-423 RQADM-30 MAPH 17/04/2017 Pantalla Referenciados SEVA 60 - Adición a valCarac de opción 23 para aceptar caracteres de correo electronico
//                        26/04/2017 Pantalla Referenciados SEVA 60 - Adición a valCarac de opción 24 para aceptar Letras sin caracteres especiales ni espacios
// BUG-PD-261 : Egonzalez : 06/11/2017 Se crea un arreglo para permitir copy/paste por ID de pantalla
//BUG-PD-438 : EGONZALEZ : 08/05/2018 : Se agrega la opción 26 para validar caracteres y se crea la función para decodificar html entities

/*Variables globales*/
var idPantalla;
var permiteCopyPaste = ['1'];

if (window.opener != 'undefined') {
    CierraPopup();
}

function CierraPopup() {
    setTimeout("window.close()", 600000);
}

function ManejaCar(tipo, b, val) {
    //N = solo numeros            , si b = 1 permite negativos
    //D = numeros decimales       , si b = 1 permite negativos
    //A = Alfanumericos Mayusculas, si b = 1 permite espacio en blanco, permite guion
    //a = Alfanumericos Minuaculas, si b = 1 permite espacio en blanco
    //C = Solo letras Mayusculas  , si b = 1 permite espacio en blanco
    //c = Solo letras Minusculas  , si b = 1 permite espacio en blanco
    //P = Alfanumericos May       , si b = 1 permite espacio en blanco. Permite punto, coma, comillas, arroba y guiones
    //p = Alfanumericos Min       , si b = 1 permite espacio en blanco. Permite punto, coma, comillas, arroba y guiones
    //Q = Alfanumericos May       , si b = 1 permite espacio en blanco. Permite coma y Diagonal
    //E = Alfanumericos May       , si b = 1 permite espacio en blanco. Permite coma, signos de interrocación y Diagonal
    //F = Fechas				  , solo numeros y "/" no se valida el valor de b
    //B = Cuentas de banco		  , enteros y "-", si b = 1 NO permite el guion
    //R = Direcciones de Internet , alfanumericos minusculas, punto y diagonal
    //m = Correo Electronico      , alfanumericos minusculas, arroba, punto, guion bajo y medio
    //i = Nombre imagenes         , alfanumericos minusculas, punto, guion bajo, dos puntos, diagonal y gatito
    //T = Alfanumericos Mayusculas, si b = 1 permite espacio en blanco. Permite letras con acentos, punto, coma y asterisco
    //t = Alfanumericos Minuaculas, si b = 1 permite espacio en blanco. Permite letras con acentos, punto, coma y asterisco

    var band = false;
    var c = event.keyCode;
    var str = new String(val);

    switch (tipo) {
        case 'N':
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 45 && str == '' && b == 1) { band = true; }
            break;
        case 'D':
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 46 && str.indexOf('.', 0) == -1) { band = true; }
            if (c == 45 && str == '' && b == 1) { band = true; }
            break;
        case 'A':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if ((c >= 97 && c <= 122) || (c == 241)) { event.keyCode = c - 32; }
            if (c == 32 && b == 1) { band = true; }
            if (c == 45) { band = true; }
            break;
        case 'a':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 32 && b == 1) { band = true; }
            break;
        case 'C':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if ((c >= 97 && c <= 122) || (c == 241)) { event.keyCode = c - 32; }
            if (c == 32 && b == 1) { band = true; }
            break;
        case 'c':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c == 32 && b == 1) { band = true; }
            break;
        case 'P':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 46) || (c == 44) || (c == 34) || (c == 45) || (c == 95) || (c == 64)) { band = true; }
            break;
        case 'Q':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if ((c >= 97 && c <= 122) || (c == 241)) { event.keyCode = c - 32; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 47) || (c == 44)) { band = true; }
            if (c == 46) { band = true; }
            break;
        case 'E':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if ((c >= 97 && c <= 122) || (c == 241)) { event.keyCode = c - 32; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 47) || (c == 44)) { band = true; }
            if (c == 46) { band = true; }
            if (c == 63) { band = true; }
            if (c == 191) { band = true; }
            break;
        case 'F':
            if ((c >= 48 && c <= 57) || (c == 47)) { band = true; }
            break;

        case 'B':
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 45 && b == 0) { band = true; }
            break;
        case 'p':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 46) || (c == 44) || (c == 34) || (c == 45) || (c == 95) || (c == 38) || (c == 64)) { band = true; }
            if ((c >= 65 && c <= 90) || (c == 241)) { event.keyCode = c + 32; }
            if (c == 165) { event.keyCode = c - 1; }
            if (c == 47) { band = true; }
            break;

        case 'R':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 46) || (c == 47) || (c == 58) || (c == 45) || (c == 95)) { band = true; }
            //if((c>=65 && c<=90)||(c==241)){event.keyCode = c + 32;}
            break;

        case 'm':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 46) || (c == 45) || (c == 95) || (c == 64) || (c == 54)) { band = true; }
            if ((c >= 65 && c <= 90) || (c == 241)) { event.keyCode = c + 32; }
            if (c == 165) { event.keyCode = c - 1; }
            break;
        case 'i':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if ((c == 46) || (c == 95) || (c == 58) || (c == 47) || (c == 35)) { band = true; }
            break;
        case 'T':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if ((c >= 97 && c <= 122) || (c == 241)) { event.keyCode = c - 32; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 46) || (c == 44) || (c == 42)) { band = true; }

            //los siguientes valores son letras con acentos (Á,É,Í,Ó,Ú)

            if (c == 193) { band = true; }
            if (c == 201) { band = true; }
            if (c == 205) { band = true; }
            if (c == 211) { band = true; }
            if (c == 218) { band = true; }
            if ((c == 225) || (c == 233) || (c == 237) || (c == 243) || (c == 250)) { event.keyCode = c - 32; }

            break;
        case 't':
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c == 241) || (c == 209)) { band = true; }
            if (c >= 48 && c <= 57) { band = true; }
            if (c == 32 && b == 1) { band = true; }
            if ((c == 46) || (c == 44) || (c == 42)) { band = true; }

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

            break;
    }
    event.returnValue = band;
}

function RevPermisos(command, usu) {
    oSelection = new Object();
    oSelection.Login = "";
    oSelection.Pwd = "";
    var sUrl = './Firma.aspx?usuario=' + usu;
    //Este string especifica el comportamiento del Dialog Modal
    Args = 'dialogHeight: 200px; dialogWidth: 300px; edge: Raised; center: Yes; help: No; resizable: No; status: No';
    // Mostramos el Dialog modal y enviamos el objeto oSelection
    if (window.showModalDialog(sUrl, oSelection, Args)) {
        //Si el usuario presiono el boton Aceptar, mostramos la Region y el Territorio devuelto desde el Modal Dialog Box
        document.forms[0].txtLogin.value = oSelection.Login;
        document.forms[0].txtPwd.value = oSelection.Pwd;
        if (oSelection.Login != '' && oSelection.Pwd != '') { __doPostBack(command, ''); }
    }
}

function AbreCotizador(emp) {
    var wcot;
    var strPro = 'toolbar=no,location=no,directories=no,status=no,scrollbars=no,resizable=no,width=910,height=475,left=50,top=50'
    wcot = window.open('./cotClienteTelepro.aspx?idEmp=' + emp, 'vmcot', strPro);
    wcot.focus();
}

function BuscaPersona() {
    var url;
    url = './buscaPersona.aspx';
    AbrePopup(url, 120, 55, 770, 380);
}

function AbreCorridaSeguros(idCot) {
    var url;
    url = './corridaSeguros.aspx?idCot=' + idCot;
    AbrePopup(url, 10, 55, 850, 400);
}

function AbreCorridaSegurosNav(idCot) {
    var url;
    url = './corridaSegurosNav.aspx?idCot=' + idCot;
    AbrePopup(url, 10, 55, 500, 400);
}

function RegresaDatos(obj, id) {
    window.close();
    //    if(window.opener.location!='[object]'){
    //	    window.opener.document.getElementById(obj).value = id;
    //	    window.opener.document.forms[0].method='post';
    //        window.opener.document.forms[0].submit();
    //    }          
    //    window.close();          
}

var wo;
function AbrePopup(url, pos1, pos2, tam1, tam2) {
    if (wo == '[object]') { wo.close(); }
    var strPro = 'toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,width=' + tam1 + ',height=' + tam2 + ',left=' + pos1 + ',top=' + pos2;
    wo = window.open(url, 'vmpopup', strPro);
    wo.focus();
}

function CambiaTab(ind) {
    var nom = "Nom" + ind;
    var tab = "Tab" + ind;
    var objN = document.getElementById(nom);
    var objT = document.getElementById(tab);

    LimpiaTabs();
    objN.style.background = "";
    objN.innerHTML = "&nbsp;<font style='font-size:16px'><b>" + objN.innerText + "</b></font>&nbsp;";
    objT.style.display = "block";

}

function LimpiaTabs() {
    var nom1 = document.getElementById('Nom1');
    var nom2 = document.getElementById('Nom2');
    var tab1 = document.getElementById('Tab1');
    var tab2 = document.getElementById('Tab2');

    nom1.innerHTML = "&nbsp;<font style='font-size:12px'>" + nom1.innerText + "</font>&nbsp;";
    nom1.style.background = "";
    nom2.innerHTML = "&nbsp;<font style='font-size:12px'>" + nom2.innerText + "</font>&nbsp;";
    nom2.style.background = "";
    tab1.style.display = "none";
    tab2.style.display = "none";
}

function ConsultaSinSubmit(Url) {
    var resultado = '';
    var objHttp;
    try {
        objHttp = new ActiveXObject("Msxml2.XMLHTTP");
    } catch (e) {
        try {
            objHttp = new ActiveXObject("Microsoft.XMLHTTP");
        } catch (e) {
            try {
                objHttp = new XMLHttpRequest();
            } catch (e) {
                resultado = 'Error, no se pudo crear el objeto HTTP';
            }
        }
    }

    if (resultado == '') {
        try {
            objHttp.open('POST', Url, false);
            objHttp.send();
            resultado = objHttp.responseText;
        } catch (E) {
            resultado = 'Error en tiempo de ejecución: ' + E.toString();
        }
    }
    return resultado;
}

function RevisaPermisos(acc, obj) {
    var url;
    var objMod = new Object();

    objMod.idAcc = acc;
    objMod.idObj = obj;
    objMod.res = '';

    var wo = window.showModalDialog('revisaPermisos.aspx', objMod, 'center=yes; help=no; dialogWidth=340px; dialogHeight=180px; status=no;');
    document.getElementById('permisos').value = objMod.res;
    return true;
}

function ValidaFecha(e) {
    var fec = e.value;
    if (fec == '') return;
    var re = /^(3[01]|0[1-9]|[12]\d)\/(0[1-9]|1[012])\/(19|20)\d\d/;
    if (re.test(fec) == true) {
        var seccion = fec.split('/');
        var dia = seccion[0];
        var mes = seccion[1];
        var anio = seccion[2];

        if (dia == '31' && (mes == '04' || mes == '06' || mes == '09' || mes == '11')) {
            alert('Fecha Incorrecta, el mes no tiene 31 dias');
            e.focus();
            e.select();
        }
        else if (dia >= 30 && mes == '02') {
            alert('Fecha incorrecta, Febrero no tiene mas de 28 o 29 dias');
            e.focus();
            e.select();
        }
        else if ((mes == '02' && dia == '29') && !(anio % 4 == 0 && (anio % 100 != 0 || anio % 400 == 0))) {
            alert('Fecha incorrecta, no es Bisiesto');
            e.focus();
            e.select();
        }
        else {
            return 1;
        }
    }
    else {
        alert('Fecha Incorrecta, el formato es dd/mm/yyyy');
        e.focus();
        e.select();
    }
}

function MuestraReporte() {
    var sUrl = './VisorReportes.aspx';
    var strPro = 'toolbar=no,location=no,directories=no,status=no,scrollbars=no,resizable=yes,width=1000px,height=700px,left=150,top=100';
    pup = window.open(sUrl, '_blank', strPro);
    pup.focus();
}

function Pesos(e) {
    var num = e.value;
    num = num.toString().replace(/\$|\,/g, '');
    e.value = num;
}

function FormatoPesosInterno(num) {
    var numero = 0;
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
    numero = (((sign) ? '' : '-') + num + '.' + cents);
    return numero;
}

function MuestraTextoCombo(combo) {
    combo.title = combo.text;
}

function validarNro(tipo, e) {//alert('validarNro');
    //N = solo numeros , si N con 1 permite negativos
    //D = numeros decimales , si D con 1 permite negativos
    //F = Fechas, solo numeros y "/"
    //A = Alfanumericos Mayusculas, si A con 1 permite espacio en blanco / (diagonal) - (guion) ' (comilla simple)
    //a = Alfanumericos Minusculas, si a con 1 permite espacio en blanco
    //C = Solo letras Mayusculas  , si C con 1 permite espacio en blanco ' (apostrofe)
    //c = Solo letras Minusculas  , si c con 1 permite espacio en blanco ' (apostrofe)
    //P = Alfanumericos Mayusculas, (/)Diagonal (&)Ampersand (.) punto (,) coma ("") comillas (-) guion y (_) guion bajo. 
    //Si P con 1 permite espacio en blanco ' (comilla simple) y signo "+"
    //p = Alfanumericos Minusculas, (/)Diagonal (&)Ampersand (.) punto (,) coma ("") comillas (-) guion y (_) guion bajo. 
    //si p con 1 permite espacio en blanco ' (comilla simple)
    //B = Cuentas de banco, enteros y "-", si B con 1 NO permite el guion
    var nodo = NodoEvento(e);
    var b = 0;
    if (tipo.length > 1) {
        b = tipo.substring(1);
        tipo = tipo.substring(0, 1);
    }
    //alert(e);
    tecla = (document.all) ? e.keyCode : e.which;
    if (((tecla >= 65 && tecla <= 90) || (tecla >= 97 && tecla <= 122) || (tecla == 241)) && (tipo == 'A' || tipo == 'C' || tipo == 'P')) {//evt = uCase(e); 
        if (nodo.readOnly == false)//(nodo.style.backgroundColor==''||nodo.style.backgroundColor=='rgb(255, 255, 255)')
        { return uCase(e); }
    } //alert(e);
    if (((tecla >= 65 && tecla <= 90) || (tecla >= 97 && tecla <= 122) || (tecla == 241)) && (tipo == 'a' || tipo == 'c' || tipo == 'p') && (e.charCode == 0 || typeof (e.charCode) == 'undefined')) {//evt = lCase(e);
        if (nodo.readOnly == false)//(nodo.style.backgroundColor==""||nodo.style.backgroundColor=='rgb(255, 255, 255)')
        { return lCase(e); }
    }
    if ((tecla == 46 || tecla == 190) && (tipo == 'D'))//Punto Decimal
    {
        if (nodo.readOnly == false) { return valPunto(e); }
    }
    if ((tecla == 45 || tecla == 173 || tecla == 189) && (tipo == 'N' || tipo == 'D') && (b == 1))//Negativos
    {
        if (nodo.readOnly == false) { return valNegativo(e); }
    }

    var key = (document.all) ? e.keyCode : e.which;

    if (e.keyCode == 9) // tab (Tabulador)
    { return true; }
    if (e.keyCode == 37 || e.keyCode == 39) // flechas de desplazamiento
    { return true; }
    if (e.keyCode == 8)  // BS (back space)
    { return true; }
    if (key == 13)
    { return true; } // (Enter)	 
    if (key == 0)
    { return false; }

    switch (tipo) {
        case 'N':
            if (key >= 48 && key <= 57)  // numeros (0 - 9)
            { return true; }
            if ((key == 45 && b == 1) || (key == 189 && b == 1)) // - (guion)
            { return true; }
            return false;
            break;
        case 'D':
            if (key >= 48 && key <= 57)  // numeros (0 - 9)
            { return true; }
            if ((key == 45 && b == 1) || (key == 189 && b == 1) || (key == 46)) // Detectar . (PUNTO) y - (guion)
            { return true; }
            return false;
            break;
        case 'F':
            if (key >= 48 && key <= 57)  // numeros (0 - 9)
            { return true; }
            if ((key == 47)) //  / (Diagonal)
            { return true; }
            return false;
            break;
        case 'B':
            if (key >= 48 && key <= 57)  // numeros (0 - 9)
            { return true; }
            if (((key == 45 || tecla == 173 || tecla == 189) && b == 0) || (key == 189 && b == 0)) // - (guion)
            { return true; }
            return false;
            break;
        case 'A':
            if ((key >= 65 && key <= 90) || (key == 241) || (key == 209))  // letras (A-Z) 
            { return true; }
            if (key >= 48 && key <= 57)  // numeros (0 - 9)
            { return true; }
            if (key == 32 && b == 1)  // espacio
            { return true; }
            if (key == 47 && b == 1)  // / (diagonal)
            { return true; }
            if ((key == 45 || tecla == 173 || tecla == 189) && b == 1) // - (guion)
            { return true; }
            if (key == 39 && b == 1)  // ' (comilla simple)
            { return true; }
            return false;
            break;
        case 'a':
            if ((key >= 97 && key <= 122) || (key == 241) || (key == 209)) // letras (a-z) 
            { return true; }
            if (key >= 48 && key <= 57) // numeros (0 - 9)
            { return true; }
            if (key == 32 && b == 1) // espacio
            { return true; }
            return false;
            break;
        case 'C':
            if ((key >= 65 && key <= 90) || (key == 241) || (key == 209)) // letras (A-Z) 
            { return true; }
            if (key == 32 && b == 1)  // espacio
            { return true; }
            if (key == 39 && b == 1)  // ' (comilla simple)
            { return true; }
            return false;
            break;
        case 'c':
            if ((key >= 97 && key <= 122) || (key == 241) || (key == 209)) // letras (a-z) 
            { return true; }
            if (key == 32 && b == 1) // espacio
            { return true; }
            if (key == 39 && b == 1)  // ' (comilla simple)
            { return true; }
            return false;
            break;
        case 'P':
            if ((key >= 65 && key <= 90) || (key == 241) || (key == 38) || (key == 209) || (key == 47)) //letras (A-Z) & Ampersand / Diagonal
            { return true; }
            if (key >= 48 && key <= 57) // numeros (0 - 9)
            { return true; }
            if (key == 32 && b == 1)  // espacio
            { return true; }
            if ((key == 46) || (key == 44) || (key == 34) || (key == 45 || tecla == 173 || tecla == 189) || (key == 95))  // . (punto) y , (coma) y " (comillas) y - (guion) y _ (guion bajo)
            { return true; }
            if (key == 43 & b == 1)  // + (signo positivo)
            { return true; }
            if (key == 39 && b == 1)  // ' (comilla simple)
            { return true; }
            return false;
            break;
        case 'p':
            if ((key >= 97 && key <= 122) || (key == 241) || (key == 38) || (key == 209) || (key == 47)) //letras (A-Z) & Ampersand / Diagonal
            { return true; }
            if (key >= 48 && key <= 57)  // 0 al 9
            { return true; }
            if (key >= 32 && b == 1)  // espacio
            { return true; }
            if ((key == 46) || (key == 44) || (key == 34) || (key == 45 || tecla == 173 || tecla == 189) || (key == 95))  // , (coma) . (punto) " (comillas) - (guion) _ (guion bajo)
            { return true; }
            if (key == 39 && b == 1)  // ' (comilla simple)
            { return true; }
            return false;
            break;
    }

}

function valNegativo(e) {
    if (e.which) {
        var target = e.target;
        var str = new String(target.value);
        if (str.indexOf('-', 0) == -1) {
            return true;
        }
    }
    else if (window.event) {
        var target = window.event.srcElement;
        var str = new String(target.value);
        if (str.indexOf('-', 0) == -1) {
            return true;
        }
    } return false; // else alert('No event information');
}

function NodoEvento(event) {

    if (event.srcElement) return event.srcElement; // Internet explorer
    if (event.target) return event.target; // Navegador Estandard

} //fin funcion recoger elemento que provoca un evento

function valPunto(e) {
    if (e.which) {
        var target = e.target;
        var str = new String(target.value);
        if (str.indexOf('.', 0) == -1) {
            return true;
        }
    }
    else if (window.event) {
        var target = window.event.srcElement;
        var str = new String(target.value);
        if (str.indexOf('.', 0) == -1) {
            return true;
        }
    } return false; // else alert('No event information');
}

function uCase(e) {
    tecla = (document.all) ? e.keyCode : e.charCode; //e.which; 
    if (tecla == 9 || tecla == 0) return true;
    if (tecla == 8) return true;
    if (e.target)//if(window.Event)
    {
        var pst = e.currentTarget.selectionStart;
        var string_start = e.currentTarget.value.substring(0, pst);
        var string_end = e.currentTarget.value.substring(pst, e.currentTarget.value.length);
        //alert(e.currentTarget.value);
        e.currentTarget.value = string_start + String.fromCharCode(tecla).toUpperCase() + string_end;
        //alert(e.currentTarget.value);
        e.currentTarget.selectionStart = pst + 1;
        e.currentTarget.selectionEnd = pst + 1;
        e.stopPropagation();
        return false;
    }
    else {
        te = String.fromCharCode(tecla);
        te = te.toUpperCase();
        num = te.charCodeAt(0);
        e.keyCode = num;
    }
}


function PermiteNEnteros(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if (((charCode >= 48 && charCode <= 57))) // 0 - 9
    {
        return true;
    }

    else {
        return false;
    }
}

function PermiteNDecimales(evt, opc) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if (((charCode == 46)
        || (charCode >= 48 && charCode <= 57))) {
        return true;
    }

    else {
        return false;
    }
}

function PermiteCaracteres(evt, opc) {

    if (opc == 1) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (((charCode == 32)
        || (charCode >= 65 && charCode <= 90) // A - Z
        || (charCode >= 97 && charCode <= 122 // a - z
        || (charCode == 209) //Ñ
        || (charCode == 241) //ñ
        ))) {
            return true;
        }

        else {
            return false;
        }
    }

    else {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (((charCode >= 65 && charCode <= 90) // A - Z
        || (charCode >= 97 && charCode <= 122 // a - z
        || (charCode == 209) //Ñ
        || (charCode == 241) //ñ
        ))) {
            return true;
        }

        else {
            return false;
        }
    }

}

function ValCarac(evt, opc) {
    //opc=1  --> Letras Mayúsculas
    //opc=2  --> Letras Mayúsculas + Espacios
    //opc=3  --> Letras Minúsculas
    //opc=4  --> Letras Minúsculas + Espacios
    //opc=5  --> Letras Mayúsculas y Minúsculas
    //opc=6  --> Letras Mayúsculas y Minúsculas + Espacios
    //opc=7  --> Números Enteros
    //opc=8  --> Números Enteros Negativos
    //opc=9  --> Números Decimales
    //opc=10 --> Números Decimales Negativos
    //opc=11 --> Razón Social
    //opc=12 --> Alfanuméricos
    //opc=13 --> Fechas
    //opc=14 --> Submarcas-Versiones
    //opc=15 --> Alfanuméricos sin caracteres especiales
    //opc=16 --> Clave Lada
    //opc=18 --> Unicamente Letras Mayúsculas y Minúsculas + Espacios
    //opc=19 --> Unicamente Letras Mayúsculas y Minúsculas + Espacios y un simbolo '%' (Tasas IVA)
    //opc=20 --> Tasas IVA
    //opc=20 --> Nombres de Agencias
    //opc=22 --> Letras Mayúsculas, Minúsculas y punto
    //opc=23 --> Caracteres de correo electrónico
    //opc=24 --> Letras sin caracteres especiales ni espacios
    switch (opc) {
        case 1: //Letras Mayúsculas
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
                || (charCode == 209) //Ñ
                || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
                ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
        case 2: //Letras Mayúsculas + Espacios
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
                || (charCode == 209) //Ñ
                || (charCode == 32) //Espacio
                || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
                ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
        case 3: //Letras Minúsculas
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 97 && charCode <= 122) // a - z
               || (charCode == 241) //ñ
               || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
               ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
        case 4: //Letras Minúsculas + Espacios
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 97 && charCode <= 122) // a - z
               || (charCode == 241) //ñ
               || (charCode == 32) //Espacio
               || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
               ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
        case 5: //Mayúsculas y Minúsculas
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
               || (charCode == 209) //Ñ
               || (charCode >= 97 && charCode <= 122) // a - z
               || (charCode == 241) //ñ
              || (charCode >= 48 && charCode <= 57) // 0-9
              || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
               ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
        case 6: //Letras Mayúsculas y Minúsculas + Espacios
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
               || (charCode == 209) //Ñ
               || (charCode >= 97 && charCode <= 122) // a - z
               || (charCode == 241) //ñ
               || (charCode == 32) //Espacio
                //|| (charCode == 34) //"
                //|| (charCode == 39) //'
                //|| (charCode == 43) // +
             || (charCode == 45) // -
                //|| (charCode == 46) // .
             || (charCode == 193) // Á
             || (charCode == 201) // É
             || (charCode == 205) // Í
             || (charCode == 211) // Ó
             || (charCode == 218) // Ú
             || (charCode == 225) // á
             || (charCode == 233) // é
             || (charCode == 237) // í
             || (charCode == 243) // ó
             || (charCode == 250) // ú
             || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
               ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
        case 7: //Números Enteros
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 48 && charCode <= 57) || (charCode == 08)) // 0 - 9 //BackSpace BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
            {
                return true;
                break;
            }

            else {
                return false;
                break;
            }
        case 8: //Números Enteros Negativos
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 48 && charCode <= 57) // 0 - 9
               || (charCode == 45) // - (Guión)
               || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
                ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
        case 9: //Números Decimales
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 48 && charCode <= 57) || (charCode == 08)) // 0 - 9 BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
            {
                return true;
                break;
            }

            if ((charCode == 46))
            { return valPunto(evt); }

            //BUG-PC-34 MAUT 16/01/2017 Se agrega el backspace para que lo reconozca Firefox
            if ((charCode == 8)) {
                return true;
                break;
            }

            else {
                return false;
                break;
            }
        case 10: //Números Decimales Negativos
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 48 && charCode <= 57) || (charCode == 45) || (charCode == 08)) // 0 - 9  //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
            {
                return true;
                break;
            }

            if ((charCode == 46))
            { return valPunto(evt); }

            else {
                return false;
                break;
            }
        case 11: //Razón Social
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
               || (charCode == 209) //Ñ
               || (charCode >= 97 && charCode <= 122) // a - z
               || (charCode == 241) //ñ
              || (charCode == 64) //@
              || (charCode == 39) //'
                //|| (charCode == 37) //%
              || (charCode == 35) //#
              || (charCode == 161) //¡
               || (charCode == 32) //Espacio
              || (charCode == 46) //.
               || (charCode == 38) //Ampersand
              || (charCode == 43) //+
              || (charCode == 193) // Á
              || (charCode == 201) // É
              || (charCode == 205) // Í
              || (charCode == 211) // Ó
              || (charCode == 218) // Ú
              || (charCode == 225) // á
              || (charCode == 233) // é
              || (charCode == 237) // í
              || (charCode == 243) // ó
              || (charCode == 250) // ú
              || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
               ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
        case 12: //Alfanuméricos
            var charCode = (evt.which) ? evt.which : event.keyCode

            if ((charCode >= 65 && charCode <= 90) // A - Z
               || (charCode == 209) //Ñ
               || (charCode >= 97 && charCode <= 122) // a - z
               || (charCode == 241) //ñ
               || (charCode >= 32 && charCode <= 47) //Especiales
               || (charCode >= 48 && charCode <= 57) // 0-9
              || (charCode == 193) // Á
              || (charCode == 201) // É
              || (charCode == 205) // Í
              || (charCode == 211) // Ó
              || (charCode == 218) // Ú
              || (charCode == 225) // á
              || (charCode == 233) // é
              || (charCode == 237) // í
              || (charCode == 243) // ó
              || (charCode == 250) // ú
              || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
               ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
        case 13: //Fecha
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 48 && charCode <= 57) // 0 - 9
               || (charCode == 45) //Guion
               || (charCode == 08)) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
            {
                return true;
                break;
            }
            else {
                return false;
                break;
            }

        case 14: //Submarcas-Versiones
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
               || (charCode == 209) //Ñ
               || (charCode >= 97 && charCode <= 122) // a - z
               || (charCode == 241) //ñ
               || (charCode >= 48 && charCode <= 57) // 0-9					
               || (charCode == 32) //Espacio
               || (charCode == 34) //"
               || (charCode == 39) //'
               || (charCode == 43) // +
               || (charCode == 45) // -
               || (charCode == 46) // .
               || (charCode == 193) // Á
               || (charCode == 201) // É
               || (charCode == 205) // Í
               || (charCode == 211) // Ó
               || (charCode == 218) // Ú
               || (charCode == 225) // á
               || (charCode == 233) // é
               || (charCode == 237) // í
               || (charCode == 243) // ó
               || (charCode == 250) // ú
               || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
               ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }

        case 15: //Link
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
               || (charCode >= 97 && charCode <= 122) // a - z
               || (charCode >= 48 && charCode <= 57) // 0-9	
               || (charCode == 39) // '
               || (charCode == 37) // %
                //|| (charCode == 35) // #
               || (charCode == 46) // .
               || (charCode == 38) // Ampersand
               || (charCode == 58) // :
               || (charCode == 63) // ?
               || (charCode == 47)// /
               || (charCode == 95)// _
               || (charCode == 45) // -
               || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
               ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
            //BUG-PC-14 2016-11-24 MAUT Se agrega opcion 16
        case 16: //Alfanuméricos sin Especiales
            var charCode = (evt.which) ? evt.which : event.keyCode

            if ((charCode >= 65 && charCode <= 90) // A - Z
                || (charCode == 209) //Ñ
                || (charCode >= 97 && charCode <= 122) // a - z
                || (charCode == 241) //ñ
                || (charCode >= 48 && charCode <= 57) // 0-9
                || (charCode == 32) //Espacio
               || (charCode == 193) // Á
               || (charCode == 201) // É
               || (charCode == 205) // Í
               || (charCode == 211) // Ó
               || (charCode == 218) // Ú
               || (charCode == 225) // á
               || (charCode == 233) // é
               || (charCode == 237) // í
               || (charCode == 243) // ó
               || (charCode == 250) // ú
               || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
                ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
            //BUG-PC-14 2016-11-24 MAUT Se agrega opcion 16
        case 17: //Clave Lada
            var charCode = (evt.which) ? evt.which : event.keyCode

            if ((charCode >= 48 && charCode <= 57) // 0-9
               || (charCode == 43) // +
               ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
            //BUG-PC-20: AVH
        case 18: //Letras Mayúsculas y Minúsculas + Espacios
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
                || (charCode == 209) //Ñ
                || (charCode >= 97 && charCode <= 122) // a - z
                || (charCode == 241) //ñ
                || (charCode == 32) //Espacio
                || (charCode == 46) // .
                || (charCode == 193) // Á
                || (charCode == 201) // É
                || (charCode == 205) // Í
                || (charCode == 211) // Ó
                || (charCode == 218) // Ú
                || (charCode == 225) // á
                || (charCode == 233) // é
                || (charCode == 237) // í
                || (charCode == 243) // ó
                || (charCode == 250) // ú
                || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
                ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
        case 19: // Tasas IVA
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
               || (charCode == 209) //Ñ
               || (charCode >= 97 && charCode <= 122) // a - z
               || (charCode == 241) //ñ
               || (charCode >= 48 && charCode <= 57) // 0-9					
               || (charCode == 32) //Espacio
               || (charCode == 193) // Á
               || (charCode == 201) // É
               || (charCode == 205) // Í
               || (charCode == 211) // Ó
               || (charCode == 218) // Ú
               || (charCode == 225) // á
               || (charCode == 233) // é
               || (charCode == 237) // í
               || (charCode == 243) // ó
               || (charCode == 250) // ú
               || (charCode == 37) //%
               || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
               ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
        case 20: //Mayúsculas, Minúsculas y punto
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
                || (charCode == 209) //Ñ
                || (charCode >= 97 && charCode <= 122) // a - z
                || (charCode == 241) //ñ
                || (charCode >= 48 && charCode <= 57) // 0-9
                || (charCode == 46) // .
                || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
                ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
        case 21: //Nombres de Agencias
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
               || (charCode == 209) //Ñ
               || (charCode >= 97 && charCode <= 122) // a - z
               || (charCode >= 48 && charCode <= 57) // 0-9
               || (charCode == 241) //ñ
              || (charCode == 64) //@
              || (charCode == 39) //'
                //|| (charCode == 37) //%
              || (charCode == 35) //#
              || (charCode == 161) //¡
               || (charCode == 32) //Espacio
              || (charCode == 46) //.
               || (charCode == 38) //Ampersand
              || (charCode == 43) //+
              || (charCode == 193) // Á
              || (charCode == 201) // É
              || (charCode == 205) // Í
              || (charCode == 211) // Ó
              || (charCode == 218) // Ú
              || (charCode == 225) // á
              || (charCode == 233) // é
              || (charCode == 237) // í
              || (charCode == 243) // ó
              || (charCode == 250) // ú
              || (charCode == 08) //BackSpace 
                || (charCode != 209) //Ñ
                || (charCode != 241) //ñ
                ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
            //BUG-PD-16: MAPH: 10/03/2017 Se agrega el backspace.
        case 22: // Alfanuméricos sin caracteres especiales y sin espacios
            var charCode = (evt.which) ? evt.which : event.keyCode

            if ((charCode >= 65 && charCode <= 90) // A - Z
                || (charCode >= 97 && charCode <= 122) // a - z
                || (charCode >= 48 && charCode <= 57) // 0-9
               || (charCode == 193) // Á
               || (charCode == 201) // É
               || (charCode == 205) // Í
               || (charCode == 211) // Ó
               || (charCode == 218) // Ú
               || (charCode == 225) // á
               || (charCode == 233) // é
               || (charCode == 237) // í
               || (charCode == 243) // ó
               || (charCode == 250) // ú
               || (charCode == 08) //BackSpace 
                ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }

            //BBV-P-423 RQADM-30 MAPH 17/04/2017 Pantalla Referenciados SEVA 60
        case 23: // Caracteres de correo electronico
            var charCode = (evt.which) ? evt.which : event.keyCode

            if ((charCode >= 65 && charCode <= 90) // A - Z
                || (charCode >= 97 && charCode <= 122) // a - z
                || (charCode >= 48 && charCode <= 57) // 0-9
                || (charCode == 46) // .
                || (charCode == 95) // _
                || (charCode == 45) // -
                || (charCode == 64) // @
                || (charCode == 08) // BackSpace 
                ) {
                return true;
            }
            else {
                return false;
            }
            break;

            //BBV-P-423 RQADM-30: MAPH: 26/04/2017 Pantalla Referenciados SEVA 60
        case 24: // Letras sin caracteres especiales y sin espacios
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
                || (charCode >= 97 && charCode <= 122) // a - z
               || (charCode == 193) // Á
               || (charCode == 201) // É
               || (charCode == 205) // Í
               || (charCode == 211) // Ó
               || (charCode == 218) // Ú
               || (charCode == 225) // á
               || (charCode == 233) // é
               || (charCode == 237) // í
               || (charCode == 243) // ó
               || (charCode == 250) // ú
               || (charCode == 08) //BackSpace 
                ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
            //BUG-PD-16: MAPH: 10/03/2017 Corrección para aceptar sólo letras + Espacios en Cambio de Agencia
        case 25: //Letras Mayúsculas y Minúsculas sin acentos + Espacios
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
                || (charCode == 209) //Ñ
                || (charCode >= 97 && charCode <= 122) // a - z
                || (charCode == 241) //ñ
                || (charCode == 32) //Espacio
                || (charCode == 193) // Á
                || (charCode == 201) // É
                || (charCode == 205) // Í
                || (charCode == 211) // Ó
                || (charCode == 218) // Ú
                || (charCode == 225) // á
                || (charCode == 233) // é
                || (charCode == 237) // í
                || (charCode == 243) // ó
                || (charCode == 250) // ú
                || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
               ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
        case 26: //Letras Mayúsculas y Minúsculas + Espacios
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode >= 65 && charCode <= 90) // A - Z
                || (charCode == 13) //retorno de carro
                || (charCode == 209) //Ñ
                || (charCode >= 97 && charCode <= 122) // a - z
                || (charCode == 241) //ñ
                || (charCode == 32) //Espacio
                || (charCode == 46) // .
                || (charCode == 193) // Á
                || (charCode == 201) // É
                || (charCode == 205) // Í
                || (charCode == 211) // Ó
                || (charCode == 218) // Ú
                || (charCode == 225) // á
                || (charCode == 233) // é
                || (charCode == 237) // í
                || (charCode == 243) // ó
                || (charCode == 250) // ú
                || (charCode == 08) //BackSpace //BUG-PC-41:PVARGAS:27/01/2017:SE AGREGA EL BACKSPACE PARA QUE PERMITA BORRAR.
                || (charCode >= 48 && charCode <= 57) // 0-9
                ) {
                return true;
                break;
            }
            else {
                return false;
                break;
            }
    }
}

function checkDecimals(evt, valor, tc) {
    var charCode = (evt.which) ? evt.which : event.keyCode

    //BUG-PC-34 MAUT 16/01/2017 Se agrega el backspace para que lo reconozca Firefox
    if ((charCode == 8)) {
        return true;
    }

    if ((charCode >= 48 && charCode <= 57)) {
        if (valor.length > tc) {
            if (valor.indexOf('.') == -1) {
                return false;
            }
            else {

                valor += ".";
                dectext = valor.substring(valor.indexOf('.') + 1, valor.length);

                if (dectext.length > 2) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }
        else {
            if (valor.indexOf('.') != -1) {

                valor += ".";
                dectext = valor.substring(valor.indexOf('.') + 1, valor.length);

                if (dectext.length > 2) {
                    return false;
                }
                else {
                    return true;
                }

            }
            else {
                return true;
            }

        }
    }

    if ((charCode == 46)) {
        return valPunto(evt);
    }

    else {
        return false;
    }
};

function ReemplazaAcentos(evt, obj, valor) {

    var valor = document.getElementById(obj).value;
    var kycd = [193, 201, 205, 211, 218, 225, 233, 237, 243, 250];
    var char = [];
    var char2 = ["A", "E", "I", "O", "U", "a", "e", "i", "o", "u"];


    for (i = 0; i <= 9; i++) {
        char.push(String.fromCharCode(kycd[i]));
    }

    if (char.length == char2.length) {
        for (i = 0; i < char.length; i++) {
            if (valor.indexOf(char[i]) != -1) { valor = valor.replace(char[i], char2[i]); }
        }
    } else { alert("Error al validar"); }

    document.getElementById(obj).value = valor;

}

$(document).ready(function () {
    /*Obtenemos el idPantalla*/
    idPantalla = $.urlParam('idPantalla');
    /*Revisamos permiso para copy/paste*/
    if ($.inArray(idPantalla,permiteCopyPaste) >= 0) {
        $('input[type=text]').each(function () {
            if ($(this).attr('onkeypress') != undefined) {
                $(this).removeAttr('onpaste');
            }
        });
    } else {
        $('input[type=text]').bind('paste', function (e) {
            e.preventDefault();
        });
    }
});

function newcheckDecimals(evt, valor, tce, tcd) {
    var charCode = (evt.which) ? evt.which : event.keyCode

    if ((charCode >= 48 && charCode <= 57) //0-9
        || (charCode == 46) // .
        || (charCode == 08)) { // Backspace

        if ((charCode == 46)) {
            return valPunto(evt);
        }

        if (valor.indexOf('.') == -1) {
            enttext = valor.substring(valor.indexOf('.'), valor.length);
            if (charCode == 08) {
                return true;
            }
            else {
                if (enttext.length > tce - 1) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }
        else {
            dectext = valor.substring(valor.indexOf('.') + 1, valor.length) + 1;
            if (charCode == 08) {
                return true;
            }
            else {
                if (dectext.length > tcd) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        if (charCode == 08) {
            return true;
        }
    }
    else {
        return false;
    }
};

$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results == null) {
        return null;
    }
    else {
        return decodeURI(results[1]) || 0;
    }
}

function decodeEntities(str) {
    var parser = new DOMParser;
    var dom = parser.parseFromString(
        '<!doctype html><body>' + str,
        'text/html');
    return dom.body.textContent;
}

function domHtmlEntities() {
    $('*').filter(function()
    {
        let regexp = /&#[0-9]{2,3};/g;
        if($(this).text().match(regexp) != null && $(this).html().match(/</) == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }).each(function()
    {
        var tmp = $(this);

        let regexp = /&#[0-9]{2,3};/g;
        var matches_array = $(this).text().match(regexp);

        console.log(matches_array);

        $(matches_array).each(function(i,val){
            console.log(val)
            console.log(decodeEntities(val));
            $(tmp).text($(tmp).text().replace(val,decodeEntities(val)));
        });
    });
};

$(window).bind("load", function () {
    domHtmlEntities();
}); // Función que espera la carga completa del documento incluyendo imágenes y Ajax*

var prm = Sys.WebForms.PageRequestManager.getInstance();
prm.add_endRequest(function (s, e) {
    domHtmlEntities();
});