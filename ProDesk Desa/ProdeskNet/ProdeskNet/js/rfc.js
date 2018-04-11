/*  YAM-P-208 egonzalez 02/10/2015
 *  Se implementó la generación de la homoclave con el dígito verificador tanto en solicitante como en coacreditado.
 *  YAM-P-208 egonzalez 29/10/2015
 *  Se agregó el caso en que no exista el apellido paterno y sólo el materno en la creación del RFC.
 *  BUG-PD-08: GVARGAS: 08/02/2017: Deshabilitado el calculo de RFC, para habilitar comentar el return;-->
 *  BUG-PD-180  GVARGAS 07/08/2017 RFC automatico
 *  BUG-PD-323 DJUAREZ 03/01/2018: Se coloca el RFC en mayusculas al quitar el focus del cuadro de texto de RFC
 */

function CalcularRFC() {

    //return; //Para habilitar comentar el return

    var nombre = $("[id$=txtNOMBRE14]").val() + ' ' + $("[id$=txtNOMBRE25]").val()
    var apellidoPaterno = $("[id$=txtAPELLIDO_PATERNO6]").val();
    var apellidoMaterno = $("[id$=txtAPELLIDO_MATERNO7]").val();
    var dia = $("[id$=ddltxtDIA_NACIMIENTO414]").val();
    var mes = $("[id$=ddltxtMES_NACIMIENTO413]").val();
    var annio = $("[id$=ddltxtANO_NACIMIENTO412]").val();
    var fecha = dia + '-' + mes + '-' + annio;
    var RFC = $("[id$=txtRFC8]");

//    nombre = nombre.toUpperCase();
//    apellidoPaterno = apellidoPaterno.toUpperCase();
//    apellidoMaterno = apellidoMaterno.toUpperCase();
    rfc = "";
    nombre = Trim(nombre);
    apellidoPaterno = Trim(apellidoPaterno);
    apellidoMaterno = Trim(apellidoMaterno);

    nombre = SinNombresComunes(nombre);
    apellidoPaterno = SinPrefijos(apellidoPaterno);
    apellidoMaterno = SinPrefijos(apellidoMaterno);

    if (apellidoMaterno == '') {
        apellidoMaterno = 'X'
    }

    // Primera letra del apellido paterno
    rfc = apellidoPaterno.substring(0, 1);

    //Buscamos y agregamos al rfc la primera vocal del primer apellido que encontremos       
    for (i = 1; i < apellidoPaterno.length; i++) {
        c = apellidoPaterno.substring(i, i + 1);
        if (EsVocal(c)) {
            rfc += SinAcento(c);
            break;

        }
    }

    //Si no hay vocal después de la primera letra ponemos una X
    if (rfc.length < 2)
        var PaternoCompleto = false;
    else
        var PaternoCompleto = true;
    //Agregamos el primer caracter del apellido materno
    rfc += SinAcento(apellidoMaterno.substring(0, 1));

    /* En caso de que no exista el apellido paterno */
    if (apellidoPaterno == '') {
        //Buscamos y agregamos al rfc la primera vocal del primer apellido que encontremos       
        for (i = 1; i < apellidoMaterno.length; i++) {
            c = apellidoMaterno.substring(i, i + 1);
            if (EsVocal(c)) {
                rfc += SinAcento(c);
                break;

            }
        }
    }

    //Agregamos el primer caracter del primer nombre o los dos primeros si es que el apellido paterno
    //no aportó los 2 caracteres que se requerían       
    if (PaternoCompleto)
        rfc += SinAcento(nombre.substring(0, 1));
    else
        rfc += SinAcento(nombre.substring(0, 2));

    // Si la cadena forma cualquier palabra altisonante, destruimos la altisonancia con una X en lugar de la ultima letra.
    rfc = SinAltisonantes(rfc);

    //Agregamos la fecha yymmdd (por ejemplo: 680825, 25 de agosto de 1968 )
    // 25-08-2006  => 060825

    rfc += fecha.substring(8, 10) + fecha.substring(3, 5) + fecha.substring(0, 2)

    var nombre = $('[id$=NOMBRE14]').val();

    if ($('[id$=NOMBRE25]').val() != '') {
        nombre += ' ' + $('[id$=NOMBRE25]').val()
    }


    //var homoclave = CalcularHomoclave(nombre, $('[id$=APELLIDO_PATERNO6]').val(), $('[id$=APELLIDO_MATERNO7]').val());

    //$('[id$=HOMOCLAVE443]').val(homoclave + DigitoVerificador(rfc + homoclave));
    RFC.attr("value", rfc.toString().toUpperCase())

    //$('#ctl00_ctl00_cphCuerpo_cphPantallas_txtRFC8').val(rfc.toString());

    return rfc.toString().toUpperCase();
}

function CalcularRFCcoa() {

 var nombre = $("[id$=txtNOMBRE176]").val() + ' ' + $("[id$=txtNOMBRE277]").val()
    var apellidoPaterno = $("[id$=txtAPELLIDO_PATERNO78]").val();
    var apellidoMaterno = $("[id$=txtAPELLIDO_MATERNO79]").val();
    var dia = $("[id$=ddltxtDIA_NACIMIENTO417]").val();
    var mes = $("[id$=ddltxtMES_NACIMIENTO416]").val();
    var annio = $("[id$=ddltxtANO_NACIMIENTO415]").val();
    var fecha = dia + '-' + mes + '-' + annio;
    var RFC = $("[id$=txtRFC80]");

//    nombre = nombre.toUpperCase();
//    apellidoPaterno = apellidoPaterno.toUpperCase();
//    apellidoMaterno = apellidoMaterno.toUpperCase();
    rfc = "";
    nombre = Trim(nombre);
    apellidoPaterno = Trim(apellidoPaterno);
    apellidoMaterno = Trim(apellidoMaterno);

    nombre = SinNombresComunes(nombre);
    apellidoPaterno = SinPrefijos(apellidoPaterno);
    apellidoMaterno = SinPrefijos(apellidoMaterno);

    // Primera letra del apellido paterno
    rfc = apellidoPaterno.substring(0, 1);

    //Buscamos y agregamos al rfc la primera vocal del primer apellido que encontremos       
    for (i = 1; i < apellidoPaterno.length; i++) {
        c = apellidoPaterno.substring(i, i + 1);
        if (EsVocal(c)) {
            rfc += SinAcento(c);
            break;

        }
    }

    //Si no hay vocal después de la primera letra ponemos una X
    if (rfc.length < 2)
        var PaternoCompleto = false;
    else
        var PaternoCompleto = true;
    //Agregamos el primer caracter del apellido materno
    rfc += SinAcento(apellidoMaterno.substring(0, 1));

    /* En caso de que no exista el apellido paterno */
    if (apellidoPaterno == '') {
        //Buscamos y agregamos al rfc la primera vocal del primer apellido que encontremos       
        for (i = 1; i < apellidoMaterno.length; i++) {
            c = apellidoMaterno.substring(i, i + 1);
            if (EsVocal(c)) {
                rfc += SinAcento(c);
                break;

            }
        }
    }

    //Agregamos el primer caracter del primer nombre o los dos primeros si es que el apellido paterno
    //no aportó los 2 caracteres que se requerían       
    if (PaternoCompleto)
        rfc += SinAcento(nombre.substring(0, 1));
    else
        rfc += SinAcento(nombre.substring(0, 2));

    // Si la cadena forma cualquier palabra altisonante, destruimos la altisonancia con una X en lugar de la ultima letra.
    rfc = SinAltisonantes(rfc);

    //Agregamos la fecha yymmdd (por ejemplo: 680825, 25 de agosto de 1968 )
    // 25-08-2006  => 060825

    rfc += fecha.substring(8, 10) + fecha.substring(3, 5) + fecha.substring(0, 2)

    var nombre = $('[id$=NOMBRE176]').val();

    if ($('[id$=NOMBRE277]').val() != '') {
        nombre += ' ' + $('[id$=NOMBRE277]').val()
    }


    var homoclave = CalcularHomoclave(nombre, $('[id$=APELLIDO_PATERNO78]').val(), $('[id$=APELLIDO_MATERNO79]').val());

    $('[id$=HOMOCLAVE80]').val(homoclave + DigitoVerificador(rfc + homoclave));
    RFC.attr("value", rfc)

    return rfc;
}



function Trim(STRING) {
    STRING = LTrim(STRING);
    return RTrim(STRING);
}

function RTrim(STRING) {
    while (STRING.charAt((STRING.length - 1)) == " ") {
        STRING = STRING.substring(0, STRING.length - 1);
    }
    return STRING;
}
function LTrim(STRING) {
    while (STRING.charAt(0) == " ") {
        STRING = STRING.replace(STRING.charAt(0), "");
    }
    return STRING;
}

function EsVocal(letra) {
    //Aunque para el caso del RFC cambié todas las letras a mayúsculas
    //igual agregé las minúsculas.
    if (letra == 'A' || letra == 'E' || letra == 'I' || letra == 'O' || letra == 'U' ||
            letra == 'a' || letra == 'e' || letra == 'i' || letra == 'o' || letra == 'u' ||
            letra == 'Á' || letra == 'É' || letra == 'Í' || letra == 'Ó' || letra == 'Ú' ||
            letra == 'á' || letra == 'é' || letra == 'í' || letra == 'ó' || letra == 'ú')
        return true;
    else
        return false;
}

function inArray(elemento, arreglo) {
    for (var keyVar in arreglo) {

        if (keyVar == elemento) {
            return arreglo[keyVar];
        }
    }
    return false;

}


function SinPrefijos(nombres) {

    if (nombres.substring(0, 5) == 'DE LA')
        return (Trim(nombres.substring(5, nombres.length)));

    if (nombres.substring(0, 5) == 'DE EL')
        return (Trim(nombres.substring(5, nombres.length)));

    if (nombres.substring(0, 6) == 'DE LOS')
        return (Trim(nombres.substring(6, nombres.length)));

    if (nombres.substring(0, 6) == 'DE LAS')
        return (Trim(nombres.substring(6, nombres.length)));

    if (nombres.substring(0, 3) == 'DEL ')
        return (Trim(nombres.substring(3, nombres.length)));

    if (nombres.substring(0, 3) == 'LOS ')
        return (Trim(nombres.substring(3, nombres.length)));

    if (nombres.substring(0, 2) == 'EL ')
        return (Trim(nombres.substring(2, nombres.length)));

    if (nombres.substring(0, 2) == 'DE ')
        return (Trim(nombres.substring(2, nombres.length)));

    if (nombres.substring(0, 2) == 'LA ')
        return (Trim(nombres.substring(2, nombres.length)));


    return (nombres);
}



function SinNombresComunes(nombres) {

    // Cuantas palabras conforman el nombre?
    var num = 1;

    for (i = 0; i < nombres.length; i++) {
        c = nombres.substring(i, i + 1);
        if (c == ' ') {
            num++;

            //Si hay más de un espacio vacío consecutivo, recorremos hasta el siguiente caracter no vacío.
            while (nombres.substring((i + 1), (i + 2)) == ' ') {
                i++;
            }
        }
    }


    if (num == 1)
        return (nombres);

    //Si hay más de un nombre, verificamos que el primero no sea de los de la lista, y si lo és, lo reovemos.

    if (nombres.substring(0, 5) == 'MARIA')
        return (Trim(nombres.substring(5, nombres.length)));

    if (nombres.substring(0, 4) == 'JOSE')
        return (Trim(nombres.substring(4, nombres.length)));

    return (nombres);
}


function SinAcento(vocal) {

    //Minúsculas
    if (vocal == 'á')
        return ('a');

    if (vocal == 'é')
        return ('e');

    if (vocal == 'í')
        return ('i');

    if (vocal == 'ó')
        return ('o');

    if (vocal == 'ú')
        return ('u');

    // Mayusculas
    if (vocal == 'Á')
        return ('A');

    if (vocal == 'É')
        return ('E');

    if (vocal == 'Í')
        return ('I');

    if (vocal == 'Ó')
        return ('O');

    if (vocal == 'Ú')
        return ('U');

    return (vocal);
}

function SinAltisonantes(palabra) {

    var mal = new Array("BUEI",
                    "BUEY",
                    "CACA",
                    "CACO",
                    "CAGA",
                    "CAGO",
                    "CAKA",
                    "CAKO",
                    "COGE",
                    "COJA",
                    "COJE",
                    "COJI",
                    "COJO",
                    "CULO",
                    "FETO",
                    "GUEY",
                    "JOTO",
                    "KACA",
                    "KACO",
                    "KAGA",
                    "KAGO",
                    "KOGE",
                    "KOJO",
                    "KAKA",
                    "KULO",
                    "LOCA",
                    "LOCO",
                    "LOKA",
                    "LOKO",
                    "MAME",
                    "MAMO",
                    "MEAR",
                    "MEAS",
                    "MEON",
                    "MION",
                    "MOCO",
                    "MULA",
                    "PEDA",
                    "PEDO",
                    "PENE",
                    "PUTA",
                    "PUTO",
                    "QULO",
                    "RATA",
                    "RUIN");

    for (i = 0; i < mal.length; i++) {
        if (mal[i] == palabra) {
            nrfx = mal[i];
            nrfx = nrfx.substring(0, 3) + 'X';
            return (nrfx);

        }
    }

    /*    */
    return (palabra);
}

function CalcularHomoclave(nombre, pat, mat) {
    var strNombreComp = pat.toUpperCase() + " " + mat.toUpperCase() + " " + nombre.toUpperCase();
    var strCharsHc = '123456789ABCDEFGHIJKLMNPQRSTUVWXYZ';
    var strCadena = '0';
    var strChr;
    var intNum1;
    var intNum2;
    var intSum = 0;
    var int3;
    var intQuo;
    var intRem;

    for (i = 0; i <= strNombreComp.length; i++) {
        strChr = strNombreComp.substr(i, 1);

        if (strChr == ' ' || strChr == '-') {
            strCadena = strCadena + '00';
        }

        if (strChr == 'Ñ' || strChr == 'Ü') {
            strCadena = strCadena + '10';
        }

        if (strChr == 'A' || strChr == 'B' || strChr == 'C' || strChr == 'D' || strChr == 'E' ||
        strChr == 'F' || strChr == 'G' || strChr == 'H' || strChr == 'I') {
            strCadena = strCadena + ((strChr.charCodeAt()) - 54);
        }

        if (strChr == 'J' || strChr == 'K' || strChr == 'L' || strChr == 'M' || strChr == 'N' ||
        strChr == 'O' || strChr == 'P' || strChr == 'Q' || strChr == 'R') {
            strCadena = strCadena + ((strChr.charCodeAt()) - 53);
        }

        if (strChr == 'S' || strChr == 'T' || strChr == 'U' || strChr == 'V' || strChr == 'W' ||
        strChr == 'X' || strChr == 'Y' || strChr == 'Z') {
            strCadena = strCadena + ((strChr.charCodeAt()) - 51);
        }

    }

    for (i = 0; i < (strCadena.length) - 1; i++) {
        intNum1 = parseInt(strCadena.substr(i, 2));
        intNum2 = parseInt(strCadena.substr(i + 1, 1));
        intSum = intSum + intNum1 * intNum2;
    }

    intSum = intSum + ' ';
    intSum = Trim(intSum)
    int3 = intSum.substr(-3);
    intQuo = parseInt(int3 / 34);
    intRem = int3 % 34;

    return ((strCharsHc.substr(intQuo, 1)) + (strCharsHc.substr(intRem, 1)));
}

function strpos(haystack, needle, offset) {
    // http://kevin.vanzonneveld.net
    // +   original by: Kevin van Zonneveld (http://kevin.vanzonneveld.net)
    // *     example 1: strpos('Kevin van Zonneveld', 'e', 5);
    // *     returns 1: 14

    var i = haystack.indexOf(needle, offset); // returns -1
    return i >= 0 ? i : false;
}

function DigitoVerificador(rfc_homo) {
    var strChars = '0123456789ABCDEFGHIJKLMN&OPQRSTUVWXYZ*';
    var strDV;
    var intDV;
    intSumas = 0
    for (i = 0; i < rfc_homo.length; i++) {
        strCh = rfc_homo.substr(i, 1);
        strCh = ((strCh == ' ') ? '*' : strCh);
        intIdx = strpos(strChars, strCh);
        intSumas = intSumas + intIdx * (14 - (i + 1));
    }
    if ((intSumas % 11) == 0) {
        strDV = 0;
    }
    else {
        intDV = 11 - intSumas % 11;
        if (intDV > 9) {
            strDV = 'A';
        }
        else {
            strDV = intDV;
        }
    }
    return strDV;
}