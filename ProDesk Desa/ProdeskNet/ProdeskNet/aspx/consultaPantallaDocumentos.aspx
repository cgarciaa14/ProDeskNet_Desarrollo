<%@ Page Language="vb" AutoEventWireup="false" CodeFile="consultaPantallaDocumentos.aspx.vb" Inherits="consultaPantallaDocumentos" MasterPageFile="~/aspx/Home.Master" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ Register TagPrefix="ACP" TagName="ActualizaCP" Src="~/UserControl/ActualizaCP.ascx" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">
    <!-- YAM-P-208 egonzalez 11/08/2015 Se agregó una validación en caso de que al subir un documento, este nos envíe como parámetro un valor vacío -->
    <!--  YAM-P-208  egonzalez 28/08/2015 Se modificó la funsión 'fnCambiaStatus' para aprovecharla y actualizar el status de cancelado a aprovado y eliminar el registro en la tabla 'PDK_NOTIFICACIONES' -->
    <!--  YAM-P-208  egonzalez 08/10/2015 Se complementó el query de actualización de documento, ya que al ejecutarse, se obtenía más de un resultado en el subquery -->
    <!--  BBVA-P-423:RQSOL-05 23/11/16 Se cambio las referencias .js para llamar al nuevo fileupload-->
    <!--  BBVA-P-423:RQCONYFOR-05 29/11/2016 Se cambiaron eventos y acciones para el correcto funcionamiento de los motivos de rechazo.-->
    <!--  BBV-P-423: RQ H - CSS JRHM 09/12/2016 Se modificaron clases de estilos para asemejarce a el cotizador-->
    <!--  BBV-P-423:RQ I -WS ARCHIVING 14/12/16 JRHM Se agrego el control de id documento con el servicio ingestadocumentos y version del documento-->
    <!--  BBV-P-423: 04/01/17: JRHM RQCONYFOR-06 Se modifico evento procesar de pagina para mejora de proceso -->
    <!--  BBV-P-423 20/01/2017: JRHM RQCONYFOR-02: Se crearon nuevos controles y metodos javascript para la tarea de "Documentación de Pólizas de Seguros y Desembolso"-->
    <!--  BUG-PD-06: GVARGAS: 31/01/2017: Corrección cancelación.-->
    <!--  BUG-PD-12: GAPM: 21/02/2017: se cambian mensaje de comparacion Faltan.-->
    <!--  BUG-PD-15: JBB:  27/02/2017: se agrega mensaje  de verificación al subir documento.-->
    <!--  BUG-PD-17 JRHM  16/03/17 Se cambia forma en que la de carga poliza de seguro avanza de tarea-->
    <!--BUG-PD-25  JRHM 04/04/17 SE AGREGAN FUNCIONES JAVASCRIPT PARA LA CANCELACION DE SOLICITUDES Y LA VISIBILIDAD DE MOTIVOS DE RECHAZOS COMO TOOLTIP-->
    <!--BUG-PD-33 JRHM 24/04/17 SE MODIFICO ASPX PARA LA CORRECCTA FUNCIONALIDAD DE LOS TAB INDEX Y LA INCLUSION DEL VALOR SELECCIONAR EN LOS COMBOS DE VALIDACION -->
    <%--BUG-PD-68: RHERNANDEZ: 02/06/17:  SE MOFIFICO CARGA DE CALENDARIOS AL CARGAR LA PANTALLA DE DOCUMENTACION DE POLIZA DE SEGUROS--%>
    <%--BUG-PD-81: RHERNANDEZ: 10/06/17: SE AGREGA EMISION DE SEGUROS DE VIDA Y DAÑOS BBVA--%>
    <%--BUG-PD-98: RHERNANDEZ: 20/06/17: SE MUEVE FILL UPLOAD DE CODIGO BACK A FRONT--%>
    <%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
    <%--RQ-INB206: ERODRIGUEZ: 07/07/2017:Se Modifico para agregar  etiqueta de monto aprobado usando regla si es menor la capacidad de la solicitud asi se deja si es mayor se toma la cantidad de la cotizacion--%>
    <%--BBV-P-423:RQ-INB215: AVEGA: 24/07/2017 SE AGREGA BOTON IMPRIMIR--%>
    <%--BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN--%>
    <%--BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS DE POLIZAS PARA EL VISOR DE TELEPRO--%>
    <%--BUG-PD-236: CGARCIA: 17/10/2017: SE AGREGA OPCION DE PANTALLA 105 PARA QUE EL ENVIO DE EMAIL SE HAGA EFECTIVO--%>
    <%--BUG-PD-305: CCHAVEZ: 13/12/2017: SE AGREGA TABLA EN APARTADO NO PROCESABLE PARA DOCUMENTCION  --%>
    <%--BUG-PD-312: RHERNANDEZ: 18/12/17: sE CONFIGURA LA OPCION NO PROCESABLE PARA LA PANTALLA DE INS CHECK DOCUMENTAL--%>
    <%-- BUG-PD-321 : EGONZALEZ : 28/12/2017 : Se corrige espacio al mostrar las opciones al "turnar" a "No procesable" --%>
    <%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
    <%--RQ-PD31: DJUAREZ: 08/03/2018: SE CREA POPUP PARA MODIFICAR LA COLONIA CUANDO SE GUARDE LA COLONIA "OTRO" --%>


    <script src="../js/jquery.ui.widget.js"></script>
    <script src="../js/jquery.iframe-transport.js"></script>
    <script src="../js/jquery.fileupload.js"></script>
    <script src="../js/datepicker-es.js"></script>
    <script src="../js/Funciones.js"></script>


    <script language="javascript" type="text/javascript">

        // Tabla pedir documentos 
        $(document).on('change', '#ctl00_ctl00_cphCuerpo_cphPantallas_ddlTurnar', function () {
            var Procesable = $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddlTurnar option:selected").val();


            if (Procesable == -3) {
                $("#Tabla_Documentos").show();
            }

            else {
                $("#Tabla_Documentos").hide();

            }
        });

        function pageLoad() {
            fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');
            var idpantalla = $("[id$=hdPantalla] ").val()


            if (idpantalla == 63) {
                $.datepicker.setDefaults($.datepicker.regional["es"]);
                var settingsDate = {
                    dateFormat: "dd/mm/yy",
                    showAnim: "slide",
                    changeMonth: true,
                    changeYear: true,
                    showOtherMonths: true,
                    selectOtherMonths: true,
                    autoSize: true,
                    onSelect: function () {
                        this.focus();
                    }
                };
                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtVigSegDanios').datepicker(settingsDate).attr('readonly', 'true')
                $('#ctl00_ctl00_cphCuerpo_cphPantallas_txtVigSegVida').datepicker(settingsDate).attr('readonly', 'true')
            }


        }

        function btnGuardar(id) {
            var boton = id.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            var f = $('[id$=hdSolicitud]').val();
            var u = $('[id$=hdusuario]').val();
            var pantalla = $('[id$=hdNomPantalla]').val();
            var idpantalla = $("[id$=hdPantalla] ").val()
            var cadena = 'UPDATE PDK_CAT_TAREAS SET PDK_TAR_MODIF=GETDATE() WHERE PDK_ID_TAREAS=' + idpantalla.toString();
            var cadenaUp = '';



            if (boton == "btnGuardarAutoriza") {
                cambiaVisibilidadDiv('divautoriza', false);
                var txtUsu = $('[id$=txtUsuario]').val()
                var txtpsswor = $('[id$=txtPassw]').val()
                var idpantalla = $("[id$=hdPantalla] ").val()
                var txtmotivoOb = $("[id$=txtmotivo]").val()
                //           var cadenastored = 'EXEC sp_ValidacionUsuario ' + '"' + cadenatodo + '",' + idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1'

                $('#ventanaContain').hide();
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del passwird esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 46, 1, txtmotivoOb)

            } else if (boton == "btnGuardarCancelar") {
                cambiaVisibilidadDiv('divcancela', false);
                var txtUsu = $('[id$=txtusua]').val();
                var txtpsswor = $('[id$=txtpass]').val();
                var idpantalla = $("[id$=hdPantalla] ").val()
                var txtmotivoOb = $("[id$=TxtmotivoCan]").val()
                $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())

                $('#ventanaContain').hide();
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del passwird esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)

            }
        }

        function fnChequea() {
            fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');
            PopUpLetrero("Documento procesado exitosamente");
        }

        function fnMuestraChecks() {
            var cheks = $("[name^=chkbxRec]");
            var count = 0;

            cheks.each(function () {
                if ($(this).is(':checked')) {
                    var arrChk = new Array()
                    arrChk = $(this)[0].name.split('_');
                    var hedrec = $('#selMR' + arrChk[1]);
                    hedrec.show('fast', "swing");
                    hedrec.addClass('Text');

                    count++;
                }
                else {
                    var arrChk = new Array()
                    arrChk = $(this)[0].name.split('_');
                    var hedrec = $('#selMR' + arrChk[1]);
                    hedrec.hide('fast', "swing");
                    hedrec.addClass('Text');
                }
            })

            if (count > 0) {
                $('[id$=MovRec]').show('fast');
            }
            else {
                $('[id$=MovRec]').hide('fast');
            }
        }

        function fnCambiaStatus(check, id) {
            var docType = $(check).parent().parent().find('td:nth-child(2):first').text();
            var folio = $('[id$=hdSolicitud]').val();

            fnMuestraChecks()

            if (check.name.indexOf('Val') != -1) {
                if (check.checked == true) {
                    btnEntrevista('update PDK_REL_PAN_DOC_SOL set PDK_ST_VALIDADO = 1, PDK_ST_RECHAZADO = 0, PDK_PAR_SIS_PARAMETRO_DIG = 0 where PDK_ID_DOC_SOLICITUD = ' + id + '; exec spValidaEstatusDoc ' + id + '; DELETE FROM PDK_NOTIFICACIONES WHERE PDK_ID_DOCUMENTOS IN ( SELECT PDK_ID_DOCUMENTOS FROM PDK_CAT_DOCUMENTOS WHERE PDK_DOC_NOMBRE = \'' + docType + '\' ) AND PDK_ID_SECCCERO = ' + folio + ';');
                    $('[name=chkbxRec_' + id + ']').attr('checked', false);
                    fnMuestraChecks()
                }
                else {
                    btnEntrevista('update PDK_REL_PAN_DOC_SOL set PDK_ST_VALIDADO = 0 where PDK_ID_DOC_SOLICITUD = ' + id + '; exec spValidaEstatusDoc ' + id);
                }
            }
            if (check.name.indexOf('Rec') != -1) {
                if (check.checked == false) {
                    btnEntrevista("UPDATE PDK_REL_PAN_DOC_SOL SET PDK_ST_RECHAZADO = 0 WHERE PDK_ID_DOC_SOLICITUD = " + id + '; exec spValidaEstatusDoc ' + id);
                }
                else {
                    $('[name=chkbxVal' + id + ']').attr('checked', false);
                    btnEntrevista("UPDATE PDK_REL_PAN_DOC_SOL SET PDK_ST_RECHAZADO = 1, PDK_ST_VALIDADO = 0, fcNotificacion='', PDK_PAR_SIS_PARAMETRO_DIG=0 WHERE PDK_ID_DOC_SOLICITUD = " + id + '; exec spValidaEstatusDoc ' + id);

                }
            }
        }
        function Guardar() {
            debugger;
            inputfile = $("[id^=yourID]");
            $(inputfile).attr("disabled", true);
            var folio = $('[id$=hdSolicitud]').val();
            var usu = $('[id$=hdusuario]').val();
            var cve = $('[id$=hntipoPantalla]').val();
            var idPant = $('[id$=hdPantalla]').val();

            if (cve == 26) {
                if (idPant == 8) {
                    jsonValidaAgencia();
                }
                else if (idPant == 63) {
                    var ddlvalue = $('#<%=ddlTurnar.ClientID%>').val();
                    if (ddlvalue == "-1") {
                        jsonGuardaDatosSeguro();
                    }
                    else {
                        var botonp = document.getElementById('<%= btnproc.ClientID%>');
                        botonp.click();
                    }
                }
                else {
                    btnManejoMensaje('exec sp_validarEntrevista ' + folio + ',2,' + idPant, 'exec spValNegocio ' + folio + ',64,' + usu + '; select dbo.fnGetMensajeTarea (' + folio + ', ' + idPant + ')')
                }

        }
        else if (cve == 68) {
            if (idPant == 74 || idPant == 89 || idPant == 9) {
                var botonp = document.getElementById('<%= btnproc.ClientID%>');
                botonp.click();
            }
            else {
                if (idPant == 105) {
                    btnInsertDocumento('exec spValNegocio ' + folio + ',64,' + usu);
                    var botonp = document.getElementById('<%= btnproc.ClientID%>');
                    botonp.click();
                }
                else {
                    btnInsertDocumento('exec spValNegocio ' + folio + ',64,' + usu);
                }

            }

        }

}

function futGuardarPro(response) {
    debugger;
    var folio = $('[id$=hdSolicitud]').val();
    var usu = $('[id$=hdusuario]').val();
    var cve = $('[id$=hntipoPantalla]').val();
    var idPant = $('[id$=hdPantalla]').val();
    var statusagencia = response.d.toString()
    if (statusagencia != "AGENCIA NO BLOQUEADA") {
        PopUpLetrero("Servicio: " + statusagencia.toString());
        $("[id$= cmbguardar1]").prop('disabled', false);
        inputfile = $("[id^=yourID]");
        $(inputfile).attr("disabled", false);
    }
    else {
        btnManejoMensaje('exec sp_validarEntrevista ' + folio + ',2,' + idPant, 'exec spValNegocio ' + folio + ',64,' + usu + '; select dbo.fnGetMensajeTarea (' + folio + ', ' + idPant + ')')
    }
}

function jsonValidaAgencia() {
    var folio = $('[id$=hdSolicitud]').val();
    var destino = "consultaPantallaDocumentos.aspx/jsonValidaAgencia";
    var successfully = futGuardarPro;
    var datos = '{"id_sol":"' + folio.toString() + '"}';
    jsonBack('Valida Agencia', destino, successfully, datos);
}

function jsonBack(errorLabel, destino, successfully, datos) {
    debugger;
    var settings = { type: "POST", url: "", data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
    settings.url = destino
    settings.success = successfully
    settings.data = datos
    settings.failure = function (response) { PopUpLetrero("Error al cargar : " + response.d.toString); $("[id$= cmbguardar1]").prop('disabled', false); }
    $.ajax(settings);
}
function jsonGuardaDatosSeguro() {
    var folio = $('[id$=hdSolicitud]').val();
    var usu = $('[id$=hdusuario]').val();
    var nomsegdanios = $('[id$=txtNomSegDanios]').val();
    var numsegdanios = $('[id$=txtNumSegDanios]').val();
    var vigsegdanios = $('[id$=txtVigSegDanios]').val();
    var nomsegvida = $('[id$=txtNomSegVida]').val();
    var numsegvida = $('[id$=txtNumSegVida]').val();
    var vigsegvida = $('[id$=txtVigSegVida]').val();
    var quotevida = $('[id$=idquotelife]').val();
    if (nomsegdanios != "" && numsegdanios != "" && vigsegdanios != "" && nomsegvida != "" && numsegvida != "" && vigsegvida != "") {
        var destino = "consultaPantallaDocumentos.aspx/jsonGuardaDatosSeguro";
        var successfully = GuardadoExitoso;
        var datos = '{"id_sol":"' + folio.toString() + '","id_usu":"' + usu.toString() + '","nomsegdanios":"' + nomsegdanios + '" , "numsegdanios":"' + numsegdanios + '" , "vigsegdanios":"' + vigsegdanios + '" , "nomsegvida":"' + nomsegvida + '" , "numsegvida":"' + numsegvida + '", "vigsegvida":"' + vigsegvida + '", "quotevida":"' + quotevida + '"}';
        jsonBack('GuardaDatSeg', destino, successfully, datos);
    }
    else {
        debugger;
        PopUpLetrero("Error: Falta cargar datos con marca *");
        $("[id$= cmbguardar1]").prop('disabled', false);
        inputfile = $("[id^=yourID]");
        $(inputfile).attr("disabled", false);
    }
}
function GuardadoExitoso(response) {
    debugger;
    var mensaje = response.d.toString()
    var ruta2 = $("[id$=hdnResultado2]").val();
    var boton = $("[id$= cmbguardar1]")
    if (response.d.indexOf("error") != -1 || response.d.indexOf("ERROR") != -1) {
        PopUpLetrero(response.d.toString());
        boton.removeAttr('disabled', '');
        inputfile = $("[id^=yourID]");
        $(inputfile).attr("disabled", false);
    }
    else {
        var botonp = document.getElementById('<%= btnproc.ClientID%>');
        botonp.click();
    }
}

function mostrarDiv() {
    div = document.getElementById('divautoriza');
    div.style.display = '';

}

function mostrarCanceDiv() {
    $('#ventanaContain').show();
    $("#divcancela").show();
}
function cambiaVisibilidadDiv(id, visible) {
    div = document.getElementById(id);
    if (visible) {
        div.style.display = '';
    }
    else {
        div.style.display = 'none'
    }
}
function fnInsertaRechazo(elemento, DOC_SOLICITUD) {
    var chkMR = $('[name $= chkbxRec_' + elemento.id + ']');
    var ddlMR = $('[name $= ddlMR' + elemento.id + '] option:selected');
    var txtMR = $('[name $= txtNotRec' + elemento.id + ']');
    var u = $('[id$=hdusuario]').val();
    var sol = $('[id$=hdSolicitud]').val();
    var valchkMR = 0;
    if (chkMR.val() == 'on') { valchkMR = 1 } else { valchkMR = 0 }
    if (ddlMR.val() == "< SELECIONAR >") {
        ddlMR.val(0);
    }

    if (txtMR.val() == "") {
        btnEntrevista("UPDATE PDK_REL_PAN_DOC_SOL SET PDK_ST_RECHAZADO = " + valchkMR + ",	PDK_ST_VALIDADO = 0, PDK_PAR_SIS_PARAMETRO_DIG = " + ddlMR.val() + ", fcNotificacion='' WHERE PDK_ID_DOC_SOLICITUD = " + DOC_SOLICITUD + "; exec sp_InsNotDig " + sol + ", " + u + ", ''; exec spValidaEstatusDoc " + DOC_SOLICITUD + ";");
    }
    else {
        btnEntrevista("UPDATE PDK_REL_PAN_DOC_SOL SET PDK_ST_RECHAZADO = " + valchkMR + ",	PDK_ST_VALIDADO = 0, PDK_PAR_SIS_PARAMETRO_DIG = " + ddlMR.val() + ",fcNotificacion='" + txtMR.val() + "' WHERE PDK_ID_DOC_SOLICITUD = " + DOC_SOLICITUD + "; exec sp_InsNotDig " + sol + ", " + u + ", '" + txtMR.val() + "'; exec spValidaEstatusDoc " + DOC_SOLICITUD + ";");
    }
}

function ddlMRover(elemento) {
    drop = document.getElementById(elemento.id);
    if (drop.options[drop.selectedIndex].text.val != "") {
        drop.title = drop.options[drop.selectedIndex].text;
    }
}

$(document).ready(function () { $.urlParam = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } }; if ($.urlParam("Enable").toString() == "1") { $("#btnCancelarNew").hide(); }; });
    </script>
    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnCancelar {
            display: none;
        }
    </style>
    <div class="divPantConsul">
        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <legend>
                                        <asp:Label ID="lblNomPantalla" runat="server"></asp:Label></legend>
                                </td>
                                <td>
                                    <asp:Label ID="lblIdPantalla" runat="server" CssClass="oculta"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divCuerpoConsul" id="dvCuerpo">
            <table class="fieldsetBBVA" style="width: 100%">
                <!--style="margin-left: 0px"-->
                <tr>
                    <th class="campos" style="width: 25%">Solicitud:                    
                    </th>
                    <th class="campos" style="width: 25%">
                        <asp:Label ID="lblSolicitud" Font-Bold="true" Font-Underline="true" runat="server">
                        </asp:Label>
                    </th>
                    <th class="campos" style="width: 25%">Cliente:                    
                    </th>
                    <th class="campos" style="width: 25%">
                        <asp:Label ID="lblCliente" Font-Bold="true" Font-Underline="true" runat="server">
                        </asp:Label>
                    </th>
                </tr>
                <tr>
                    <th class="campos">Status Credito:                    
                    </th>
                    <th class="campos">
                        <asp:Label ID="lblStCredito" Font-Bold="true" Font-Underline="true" runat="server">
                        </asp:Label>
                    </th>
                    <th class="campos">Status Documentos:                    
                    </th>
                    <th class="campos">
                        <asp:Label ID="lblStDocumento" Font-Bold="true" Font-Underline="true" runat="server">
                        </asp:Label>
                    </th>
                </tr>
                <tr>
                    <th class="campos">Monto aprobado:                    
                    </th>
                    <th class="campos">
                        <asp:Label ID="LblCS" Font-Bold="true" Font-Underline="true" runat="server">
                        </asp:Label>
                    </th>

                    <%--     <th class="campos">Capacidad cotizada:                    
                    </th>
                    <th class="campos">
                        <asp:Label ID="LblCC" Font-Bold="true" Font-Underline="true" runat="server">
                        </asp:Label>
                    </th>--%>
                </tr>
            </table>
            <br />
            <div id="DatosBrokers" runat="server" style="display: none">
                <table class="resulbbvarigth" style="text-align: left" width="100%">
                    <tr>
                        <th colspan="6">Datos Seguro de Daños:</th>
                    </tr>
                    <tr>
                        <td style="width: 17%">Nombre de la Aseguradora Daños: * </td>
                        <td>
                            <asp:TextBox ID="txtNomSegDanios" CssClass="txt3BBVA" runat="server" Onkeypress="return ValCarac(event,11);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" TabIndex="1" /></td>
                        <td style="width: 16%">Número de Póliza Daños: * </td>
                        <td>
                            <asp:TextBox ID="txtNumSegDanios" CssClass="txt3BBVA" MaxLength="10" runat="server" Onkeypress="return ValCarac(event,12);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" TabIndex="2" /></td>
                        <td style="width: 16%">Vigencia de la póliza de Daños: * </td>
                        <td>
                            <asp:TextBox ID="txtVigSegDanios" CssClass="txt3BBVA" runat="server" TabIndex="3" /></td>
                    </tr>
                    <tr>
                        <th colspan="6">Datos Seguro de Vida:</th>
                    </tr>
                    <tr>
                        <td style="width: 17%">Nombre de la Aseguradora Vida: * </td>
                        <td>
                            <asp:TextBox ID="txtNomSegVida" CssClass="txt3BBVA" runat="server" Onkeypress="return ValCarac(event,11);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" TabIndex="4" /></td>
                        <td style="width: 16%">Número de Póliza Vida: * </td>
                        <td>
                            <asp:TextBox ID="txtNumSegVida" CssClass="txt3BBVA" runat="server" MaxLength="10" Onkeypress="return ValCarac(event,12);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" TabIndex="5" /></td>
                        <td style="width: 16%">Vigencia de la póliza de Vida: * </td>
                        <td>
                            <asp:TextBox ID="txtVigSegVida" CssClass="txt3BBVA" runat="server" TabIndex="6" /></td>
                    </tr>
                </table>
            </div>
            <br />
            <fieldset style="overflow: scroll; height: 300px; overflow-x: hidden; border: none">
                <table id="tbValidarObjetos" class="resulGrid"></table>
            </fieldset>
            <br />
            <%-- Tabla documentos opcionales--%>
            <div id="divDocumentos_opcionales" runat="server" visible="false" style="align-content: center; padding-bottom: 40px;">
                <table>
                    <asp:GridView ID="gvDocumentos_opc" runat="server" AutoGenerateColumns="false" Width="310px" HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA"
                        Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0"><tr class="GridviewScrollHeaderBBVA"><th>Solicitar Documentos</th></tr></table>' CaptionAlign="Top">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Left"  Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblid_doc" runat="server" Text='<%# Eval("PDK_ID_DOCUMENTOS")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PDK_DOC_NOMBRE" HeaderText="Nombre" ItemStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderText="No Procesable" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDocopc" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </table>
            </div>


            <%--<div id="Tabla_Documentos" style="display: none" >
            <div >
                <table style="width:25%;margin-bottom:25px">
                    <tbody>
                       <th  class="TituloDoc" style="width: 15% ;text-align:left"> Pedir Documentos </th>
 
                    </tbody>
                </table>   
        </div>


            <div>
                <table class="GridviewScrollItemBBVA td" style="width:25%">
                    <tbody>
                        
                    <tr>
                        <th class="GridviewScrollItemBBVA td" style=" width: 15% "><b>Nombre del Documento</b></th>
                        
                        <th class="TableDoc" style=" width: 15%;text-align:left">Seleccionar</th> 
                        

                 <tr>
                         <td  class="GridviewScrollItemBBVA td" style="width: 15% ;text-align:center">Comprobante de Domicilio</td> 
                        <td>
                            <input id="Comprobante"  checked="checked" type="checkbox">
                      
                         </td>
                     </tr>

                   <tr>
                         <td  class="GridviewScrollItemBBVA td";style="width:15%">Estado de Cuenta</td> 
                        <td>
                            <input id="Estado"  checked="checked" type="checkbox">
                      
                         </td>
                     </tr>

                          </tr>

                    </tbody>
                </table>   
            </div>
        </div>--%>
        </div>





        <div id="divautoriza" style="display: none">
            <%--<cc1:ModalPopupExtender ID="mpoAutorizar" runat="server" PopupControlID="popAutoriza" TargetControlID="btnAutorizar" CancelControlID="btnCancelAutoriza" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>--%>
            <asp:Panel ID="popAutoriza" runat="server" Text="Autorización" CssClass="cajadialogo">
                <div class="tituloConsul">
                    <asp:Label ID="Label4" runat="server" Text="Autorización" />
                </div>
                <table width="100%">
                    <tr>
                        <td class="campos">Usuario:</td>
                        <td>
                            <asp:TextBox ID="txtUsuario" SkinID="txtGeneral" MaxLength="12" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="campos">Password:</td>
                        <td>
                            <asp:TextBox ID="txtPassw" runat="server" SkinID="txtGeneral" MaxLength="12" TextMode="Password" EnableTheming="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="campos">Descripción:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea id="txtmotivo" runat="server" onkeypress="ManejaCar('A',1,this.value,this)" class="Text" rows="5" cols="1" style="width: 100%"></textarea>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td>
                            <asp:HiddenField runat="server" ID="hdnidAutoriza" />
                        </td>
                        <td align="left" valign="middle">
                            <input id="btnGuardarAutoriza" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar(id)" />
                            <asp:Button ID="btnCancelAutoriza" runat="server" Text="Cancelar" SkinID="btnGeneral" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top:15%; left:31%; width: 220px;">
            <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID="btnCancelar" PopupControlID="popCancela" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>--%>
            <%--<asp:Panel ID="popCancela" runat="server" CssClass="cajadialogo ">
            </asp:Panel>--%>
                <div class="tituloConsul">
                    <asp:Label ID="Label1" runat="server" Text="Cancelación" />
                </div>
                <table width="100%">
                    <tr>
                        <td class="campos">Usuario:</td>
                        <td>
                            <asp:TextBox ID="txtusua" SkinID="txtGeneral" MaxLength="12" Style="width: 120px !important;" runat="server"></asp:TextBox>
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
                            <textarea id="TxtmotivoCan" runat="server" onkeypress="ManejaCar('A',1,this.value,this)" class="Text" rows="5" cols="1" style="width: 95% ! important"></textarea>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td>
                            <asp:HiddenField runat="server" ID="HiddenField1" />
                        </td>
                        <td align="left" valign="middle">
                            <input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar(id)" />
                            <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral" />
                        </td>
                    </tr>

                </table>
        </div>
        <div id="divActualizaColonia" runat="server" style="display:none;">
            <ACP:ActualizaCP id="ActualizaCP" runat="server"/>
        </div>
        <div class="resulbbva divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">
                        <label id="lblturnar" runat="server" visible="false">Turnar: </label>
                        &nbsp;
                        <asp:DropDownList ID="ddlTurnar" runat="server" CssClass="selectBBVA" Visible="false" OnSelectedIndexChanged="ddlTurnar_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        &nbsp;
                        <%--<ACP:ActualizaCP id="ActualizaCP1" runat="server"/>--%>
                       <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" />
                        <%-- <asp:Button runat="server" ID="btnGuardar" text="Procesar"  SkinID="btnGeneral" />--%>
                        <asp:Button runat="server" ID="btnImpAuto" Visible="false" Text="Imp. Poliza Daños" CssClass="buttonBBVA2" OnClick="btnImpAuto_Click" />
                        <asp:Button runat="server" ID="btnImpVida" Visible="false" Text="Imp. Poliza  Vida" CssClass="buttonBBVA2" OnClick="btnImpVida_Click" />
                        <asp:Button runat="server" ID="btnEmisionDanRetry" Text="Reintentar Emision" class="buttonBBVA2" Visible="false" OnClientClick="this.disabled = true;" OnClick="btnEmisionDanRetry_Click" UseSubmitBehavior="false" />
                        <asp:Button runat="server" ID="btnImpresionDanRetry" Text="Reintentar Impresion" class="buttonBBVA2" Visible="false" OnClientClick="this.disabled = true;" OnClick="btnImpresionDanRetry_Click" UseSubmitBehavior="false" />
                        <asp:Button runat="server" ID="btnEmisionVidRetry" Text="Reintentar Emision" class="buttonBBVA2" Visible="false" OnClientClick="this.disabled = true;" OnClick="btnEmisionVidRetry_Click" UseSubmitBehavior="false" />
                        <asp:Button runat="server" ID="btnImpresionVidRetry" Text="Reintentar Impresion" class="buttonBBVA2" Visible="false" OnClientClick="this.disabled = true;" OnClick="btnImpresionVidRetry_Click" UseSubmitBehavior="false" />
                        <input id="cmbguardar1" runat="server" value="Procesar" type="button" onclick="$(this).prop('disabled', true); Guardar();" class="buttonBBVA2" />
                        <asp:Button runat="server" ID="btnAutorizar" Visible="false" Text="Autorizar" CssClass="buttonBBVA2" />
                        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClientClick="mostrarCanceDiv()" CssClass="buttonSecBBVA2" />
                        <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                        <asp:Button runat="server" ID="btnImprimir" Text="Imprimir" CssClass="buttonSecBBVA2" OnClick="btnImprimir_Click" />

                    </td>
                </tr>
            </table>
        </div>
        <div id="divbtncollap" style="visibility: collapse">
            <asp:Button runat="server" ID="btnproc" OnClick="btnproc_Click" />
        </div>
    </div>
    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />
    <asp:HiddenField runat="server" ID="hdnResultado" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
    <asp:HiddenField ID="hdNomPantalla" runat="server" />
    <asp:HiddenField ID="hntipoPantalla" runat="server" />
    <asp:HiddenField ID="hdnResultado1" runat="server" />
    <asp:HiddenField ID="hdnResultado2" runat="server" />
    <asp:HiddenField ID="idquote" runat="server" />
    <asp:HiddenField ID="idquotelife" runat="server" />
</asp:Content>
