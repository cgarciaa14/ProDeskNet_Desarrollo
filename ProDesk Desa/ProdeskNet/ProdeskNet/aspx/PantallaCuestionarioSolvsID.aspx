<%@ Page Language="VB" MasterPageFile="~/aspx/Home.Master" AutoEventWireup="false" CodeFile="PantallaCuestionarioSolvsID.aspx.vb" Inherits="aspx_PantallaCuestionarioSolvsID" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<script runat="server">

    Protected Sub ddlTipoINE_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
</script>

<%--BBV-P-423: RQADM-37: JRHM: 17/03/2017 Se crea pantalla "Solicitud vs ID"--%>
<%--BBV-P-423: RQADM-22: JRHM: 24/03/17 SE CAMBIA PRESENTACION DE LA PAGINA Y SE AGEGA TABLA COMENTADA PARA AGREGAR FUNCIONALIDAD FUTURA"--%>
<%--BUG-PD-25 JRHM 04/04/17 SE MODIFICO APARIENCIA Y FUNCIONALIDAD DE LA TABLA DE VALIDACION DE DOCUMENTOS--%>
<%--BUG-PD-45:RHERNANDEZ:15/05/17 DE AGREGA SIMBOLO * A OPCION DE TURNAR PARA OBLIGAR A SELECCIONAR ACCION QUE REALIZARA LA VENTANA--%>
<%--BBV-P-423. RQXLS2: CGARCIA: 26/05/2017 VLIDACIONES DICIONALES A DROPDOWNLIST--%>
<%-- BBV-p-423, RQXLS3: CGARCIA: 09/06/2017 SE AGREGAN VALIDACIONES--%>
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BUG-PD-135: CGARCIA: 04/07/2017: SE AGREGO VISOR DOCUMENTAL--%>
<%--BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN--%>
<%--BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS PARA ABRIR VISOR TELEPRO--%>
<%--BUG-PD-204 GVARGAS 19/09/2017 WS biometrico--%>
<%--BUG-PD-211 GVARGAS 27/09/2017 Cambios mostrar info--%>
<%--BUG-PD-221 GVARGAS 03/10/2017 Cambios validacion INE--%>
<%--BUG-PD-222 GVARGAS 05/10/2017 Mejoras captura INE--%>
<%--BBV-P-423 RQ-PD-17 6 GVARGAS 16/01/2018 details INE--%>
<%--BUG-PD-348 GVARGAS 25/01/2017 Correcion regreso muetar detalle INE--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
<%--BUG-PD-423: CGARCIA: 23/04/2018: SE MANDA A BACK LA ACTUALIZACION DE STATUS DE LOS DOCUMENTOS.--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">
    <script src="../js/jquery.ui.widget.js"></script>
    <script src="../js/jquery.iframe-transport.js"></script>
    <script src="../js/jquery.fileupload.js"></script>
    <script src="../js/Funciones.js"></script>
    <script type="text/javascript" language="javascript">
        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();

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
        function fnChequea() {
            fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario, hdBandera', 'per1', '');
                PopUpLetrero("Documento procesado exitosamente");
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
            if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
            if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

            btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)

            }
        }

        function fnCambiaStatus(check, id) {
            //BUG-PD-423: CGARCIA: 23/04/2018: SE MANDA A BACK LA ACTUALIZACION DE STATUS DE LOS DOCUMENTOS.
            var docType = $(check).parent().parent().find('td:nth-child(2):first').text();
            var folio = $('[id$=hdSolicitud]').val();
            var Check_VAL = check.name.indexOf('Val');
            var Check_REC = check.name.indexOf('Rec');
            var checked = check.checked

            fnMuestraChecks()

            if (check.name.indexOf('Val') != -1) {
                if (check.checked == true) {
                    var settings = {
                        type: "POST", url: "consultaPantallaDocumentos.aspx/getBack", async: false,
                        data: "{'id': '" + id + "', 'docType': '" + docType + "', 'folio': '" + folio + "', 'Check_VAL': '" + Check_VAL + "', 'Check_REC': '" + Check_REC + "', 'Checked': '" + 1 + "'}",
                        contentType: "application/json; charset=utf-8", dataType: "json",
                        success: function OnSuccess_showDetails(response) {
                            $('[name=chkbxRec_' + id + ']').attr('checked', false);
                            fnMuestraChecks()
                        }
                    };
                    $.ajax(settings);
                }
                else {
                    var settings = {
                        type: "POST", url: "consultaPantallaDocumentos.aspx/getBack", async: false,
                        data: "{'id': '" + id + "', 'docType': '" + docType + "', 'folio': '" + folio + "', 'Check_VAL': '" + Check_VAL + "', 'Check_REC': '" + Check_REC + "', 'Checked': '" + 2 + "'}",
                        contentType: "application/json; charset=utf-8", dataType: "json",
                        success: function OnSuccess_showDetails(response) { }
                    };
                    $.ajax(settings);
                }
            }

            if (check.name.indexOf('Rec') != -1) {
                if (check.checked == false) {
                    var settings = {
                        type: "POST", url: "consultaPantallaDocumentos.aspx/getBack", async: false,
                        data: "{'id': '" + id + "', 'docType': '" + docType + "', 'folio': '" + folio + "', 'Check_VAL': '" + Check_VAL + "', 'Check_REC': '" + Check_REC + "', 'Checked': '" + 3 + "'}",
                        contentType: "application/json; charset=utf-8", dataType: "json",
                        success: function OnSuccess_showDetails(response) { }
                    };
                    $.ajax(settings);
                }
                else {
                    $('[name=chkbxVal' + id + ']').attr('checked', false);
                    var settings = {
                        type: "POST", url: "consultaPantallaDocumentos.aspx/getBack", async: false,
                        data: "{'id': '" + id + "', 'docType': '" + docType + "', 'folio': '" + folio + "', 'Check_VAL': '" + Check_VAL + "', 'Check_REC': '" + Check_REC + "', 'Checked': '" + 4 + "'}",
                        contentType: "application/json; charset=utf-8", dataType: "json",
                        success: function OnSuccess_showDetails(response) { }
                    };
                    $.ajax(settings);
                }
            }
        }

        //function fnCambiaStatus(check, id) {
        //    var docType = $(check).parent().parent().find('td:nth-child(2):first').text();
        //    var folio = $('[id$=hdSolicitud]').val();

        //    fnMuestraChecks()

        //    if (check.name.indexOf('Val') != -1) {
        //        if (check.checked == true) {
        //            btnEntrevista('update PDK_REL_PAN_DOC_SOL set PDK_ST_VALIDADO = 1, PDK_ST_RECHAZADO = 0, PDK_PAR_SIS_PARAMETRO_DIG = 0 where PDK_ID_DOC_SOLICITUD = ' + id + '; exec spValidaEstatusDoc ' + id + '; DELETE FROM PDK_NOTIFICACIONES WHERE PDK_ID_DOCUMENTOS IN ( SELECT PDK_ID_DOCUMENTOS FROM PDK_CAT_DOCUMENTOS WHERE PDK_DOC_NOMBRE = \'' + docType + '\' ) AND PDK_ID_SECCCERO = ' + folio + ';');
        //            $('[name=chkbxRec_' + id + ']').attr('checked', false);
        //            fnMuestraChecks()
        //        }
        //        else {
        //            btnEntrevista('update PDK_REL_PAN_DOC_SOL set PDK_ST_VALIDADO = 0 where PDK_ID_DOC_SOLICITUD = ' + id + '; exec spValidaEstatusDoc ' + id);
        //        }
        //    }
        //    if (check.name.indexOf('Rec') != -1) {
        //        if (check.checked == false) {
        //            btnEntrevista("UPDATE PDK_REL_PAN_DOC_SOL SET PDK_ST_RECHAZADO = 0 WHERE PDK_ID_DOC_SOLICITUD = " + id + '; exec spValidaEstatusDoc ' + id);
        //        }
        //        else {

        //            $('[name=chkbxVal' + id + ']').attr('checked', false);
        //        }
        //    }
        //}
        function fnInsertaRechazo(elemento, DOC_SOLICITUD) {
            var chkMR = $('[name $= chkbxRec_' + elemento.id + ']');
            var ddlMR = $('[name $= ddlMR' + elemento.id + '] option:selected');
            var txtMR = $('[name $= txtNotRec' + elemento.id + ']');
            var u = $('[id$=hdusuario]').val();
            var sol = $('[id$=hdSolicitud]').val();
            var valchkMR = 0;

            if (chkMR.val() == 'on') { valchkMR = 1 } else { valchkMR = 0 }

            if (ddlMR.val() == '') {
                ddlMR.val(0);
            }

            if (txtMR.val() == '') {
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
        function cambiaVisibilidadDiv(id, visible) {
            div = document.getElementById(id);
            if (visible) {
                div.style.display = '';
            }
            else {
                div.style.display = 'none'
            }
        }

        function detallesINE(opc) {
            //var settings = { type: "POST", url: "", async: false, data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
            //settings.url = "PantallaCuestionarioSolvsID.aspx/detailsINE";
            //settings.success = OnSuccessDetailsINE;
            //settings.data = "{'folio' : '" + $.urlParam("sol").toString() + "', 'opc': '" + opc + "'}";
            //settings.failure = function (response) { alert("Error al cargar : " + errorLabel.toString()); }
            //$.ajax(settings);
        }

        function OnSuccessDetailsINE(response) {
            var items = $.parseJSON(response.d.toString());
            if (items.mensaje == "ERROR") {
                alert(items.mensaje.toString());
                return;
            }
            else if (items.mensaje == "NO") {
                $("#btnDetails").hide();
                return;
            }
            else {
                var itemsINE = $.parseJSON(items.mensaje.toString());

                $('#legendCarrusel').html('Detalles INE');
                $('#ventanaContain').show();
                $('#divDetails').show();

                var nombre_ = "El nombre no coincide.";
                var ap_pat = "El apellido paterno no coincide.";
                var ap_mat = "El apellido materno no coincide.";
                var ocr = "OCR no coincide.";
                var INE_ = "El biométrico no coincide.";

                if (itemsINE.name.toString() == "true") { nombre_ = "El nombre coincide."; }

                if (itemsINE.lastName.toString() == "true") { ap_pat = "El apellido paterno coincide."; }

                if (itemsINE.mothersLastName.toString() == "true") { ap_mat = "El apellido materno coincide."; }
              
                if (itemsINE.ocr.toString() == "true") { ocr = "OCR coincide, " + itemsINE.descripcion.toString() + "."; }

                var print2 = itemsINE.codePawPrint2;
                var print7 = itemsINE.codePawPrint7;

                if ((print2 > 90) || (print7 > 90)) { INE_ = "El biométrico coincide."; }

                $("#Nombre_INE").html(nombre_.toString());
                $("#Ap_INE").html(ap_pat.toString());
                $("#Am_INE").html(ap_mat.toString());
                $("#Ocd_INE").html(ocr.toString());
                $("#Biometrico").html(INE_);
            }

        }

        $(document).ready(function () {
            $("#CloseDetail").on("click", function (e) {
                $('#legendCarrusel').html('');
                $('#ventanaContain').hide();
                $('#divDetails').hide();
                $("#tableFields tbody").empty();
            });

            $.urlParam = function (name) {
                var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
                if (results == null) { return null; } else { return results[1] || 0; }
            }

            $("#btnDetails").hide();

            //detallesINE(0);
        });

        function pageLoad() {
            //detallesINE(0);
        }
    </script>
    <style>
        #divDetails {
            height: 130px;
        }  

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
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Validación ID"></asp:Label></legend>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divCuerpoConsul" id="dvCuerpo">
            <table class="fieldsetBBVA" style="width: 100%">
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
            </table>
            <br />
            <table class="resulbbvarigth">
                <tr>                    
                    <td>Tipo de Identificación * </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoID" runat="server" CssClass="selectBBVA" OnSelectedIndexChanged="ddlTipoID_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="-1" Text="<SELECCIONAR>" ></asp:ListItem>
                            <asp:ListItem Value="0" Text="Credencial de Elector"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Pasaporte"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Cedula Profesional"></asp:ListItem>
                            <asp:ListItem Value="3" Text="FM2/FM3 para extranjeros"></asp:ListItem>
                        </asp:DropDownList>
                    </td>   
                    <td>&nbsp;</td> 
                    <div id="divPortalNominal" runat="server" visible="false">
                        <td>Ingresar al Portal Lista Nominal: </td>
                        <td>
                            <asp:Button ID="btnLinkedin" runat="server" Text="Portal Lista Nominal" CssClass="buttonSecBBVA2" OnClick="btnLinkedin_Click" />
                        </td>
                    </div>
                    <div id="divPortalCedula" runat="server" visible="false">
                        <td>Portal Cédula Profesional:</td>
                        <td>
                            <asp:Button ID="btnLinkCel" runat="server" Text="Ir a Cédula Profesional" CssClass="buttonSecBBVA2" OnClick="btnLinkCel_Click"/>
                        </td>
                    </div>
                </tr>
                <tr>
                    <td>¿El nombre de la Identificación vs Solicitud es? * </td>
                    <td>
                        <asp:DropDownList ID="ddlNomvsSol" runat="server" CssClass="selectBBVA" AutoPostBack="true">
                            <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Iguales"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Similares"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Diferentes"></asp:ListItem>
                        </asp:DropDownList></td>
                <td>&nbsp;</td> 
                    <td>¿La firma de la Identificación vs Solicitud es idéntica? *</td>
                    <td>
                        <asp:DropDownList ID="ddlFirmaID" runat="server" CssClass="selectBBVA" OnSelectedIndexChanged="ddlFirmaID_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Iguales"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Similares"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Diferentes"></asp:ListItem>
                        </asp:DropDownList></td>
                    
                    </tr>
                <tr>
                    <td>¿La Identificación es vigente? *</td>
                    <td>
                        <asp:DropDownList ID="ddlIDVig" runat="server" CssClass="selectBBVA" AutoPostBack="true">
                            <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                        <div id="tdEstatusINE" runat="server" visible="false">
                        <td >Estatus actual de INE</td>
                        <td>
                            <asp:DropDownList ID="ddlEstatusINE" runat="server" CssClass="selectBBVA">
                                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="0" Text="No Vigente"></asp:ListItem>
                                <asp:ListItem Value="1" Text="No está Vigente(No concluiste tu trámite)"></asp:ListItem>
                                <asp:ListItem Value="2" Text="No está Vigente(No es tu última credencial)"></asp:ListItem>
                                <asp:ListItem Value="3" Text="No está Vigente(Tiene recuadro 03)"></asp:ListItem>
                                <asp:ListItem Value="4" Text="No está Vigente(Por Autoridad)"></asp:ListItem>
                                <asp:ListItem Value="5" Text="No está Vigente(Registro reincorporado por registro de autoridad)"></asp:ListItem>
                                <asp:ListItem Value="6" Text="OCR ingreso distinto al registrado"></asp:ListItem>
                                <asp:ListItem Value="7" Text="No está Vigente(Excluido de la lista Nominal)"></asp:ListItem>
                                <asp:ListItem Value="8" Text="No está Vigente(Por correción de datos)"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Datos incorrectos o inexistentes"></asp:ListItem>
                                <asp:ListItem Value="10" Text="No está Vigente(Suspensión de derechos)"></asp:ListItem>
                                <asp:ListItem Value="11" Text="No está Vigente(Por defunción)"></asp:ListItem>
                                <asp:ListItem Value="12" Text="No está Vigente(Existe otro registro a tu nombre)"></asp:ListItem>
                                <asp:ListItem Value="13" Text="No está Vigente(tu registro presenta datos irregulares)"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                   </div>
                    <div id="tdExisteCedula" runat="server" visible="false">
                        <td>¿Existe cédula profesional? * </td>
                        <td>
                            <asp:DropDownList ID="ddlCedula" runat="server" CssClass="selectBBVA" title="¿Existe cédula Profesional?" OnSelectedIndexChanged="ddlCedula_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Página no disponible"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </div>
                </tr>
                <tr>
                    <div id="trExisteINE" runat="server" visible="false">
                        <td>¿Existe el INE del solicitante? *</td>
                        <td>
                            <asp:DropDownList ID="ddlExisteINE" runat="server" CssClass="selectBBVA">
                                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Si"></asp:ListItem>
                                <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                <asp:ListItem Value="2" Text="No aplica"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Página no disponible"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </div>
                    <div id="trDatosCedula" runat="server" visible="false">
                        <td>¿El número de Cédula profesional es:?*</td>
                        <td>
                            <asp:DropDownList ID="ddlGeoloca" runat="server" CssClass="selectBBVA" title="¿El número de Cédula profesional es:?">
                                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Igual"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Diferente"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                        <td>¿El nombre de la Licenciatura coincide?*</td>
                        <td>
                            <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="selectBBVA" title="¿El nombre de la Licenciatura coincide?">
                                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Igual"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Diferente"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </div>
                </tr>

                <tr id="trTipoINE" runat="server" visible="false">                    
                    <td>¿Versión de Credencial de Elector es D o E?</td>
                    <td>
                        <asp:DropDownList ID="ddlTipoINE" runat="server" CssClass="selectBBVA" OnTextChanged="ddlTipoINE_TextChanged" AutoPostBack="true">
                            <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Si"></asp:ListItem>
                            <asp:ListItem Value="1" Text="No"></asp:ListItem>
                            <asp:ListItem Value="2" Text="No aplica"></asp:ListItem>
                        </asp:DropDownList>
                    </td>                   
                    <td>&nbsp;</td>                    
                        <td>¿Validación de Mica Procesable?</td>
                        <td>
                            <asp:DropDownList ID="ddlProcesable" runat="server" CssClass="selectBBVA" Enabled="false">
                                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Si"></asp:ListItem>
                                <asp:ListItem Value="1" Text="No"></asp:ListItem>
                                <asp:ListItem Value="2" Text="No contiene"></asp:ListItem>
                                <asp:ListItem Value="3" Text="No visible"></asp:ListItem>
                            </asp:DropDownList>
                        </td>                   
                </tr>                
            </table>
            <br />
            <div style="overflow:auto;height:40%;">
            <table id="tbValidarObjetos" class="resulGrid"></table>            
            </div>
        </div>
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
                        <textarea id="TxtmotivoCan" runat="server" onkeypress="return ValCarac(this.event, 6);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" class="Text" rows="5" cols="1" style="width: 95% ! important"></textarea>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td>
                        <asp:HiddenField runat="server" ID="HiddenField1" />
                    </td>
                    <td align="left" valign="middle">
                        <input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar(id)" />
                        <br />
                        <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral" />
                    </td>
                </tr>

            </table>
    </div>
    <div class="resulbbvaCenter divAdminCatPie">
        <table width="100%" style="height: 100%;">
            <tr>
                <td>
                    <input id="btnDetails" type="button" value="Detalle INE" onclick="detallesINE(1)" class="buttonSecBBVA2" style="display: none;">
                    <asp:Button ID="btnVisorDocumental" runat="server" Text="Visor Documental" CssClass="buttonSecBBVA2" OnClick="btnVisorDocumental_Click" />
                    <label id="lblturnar" runat="server">Turnar: *</label>
                    &nbsp;
                    <asp:DropDownList ID="ddlTurnar" runat="server" CssClass="selectBBVA">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2" OnClick="btnRegresar_Click" />
                    <asp:Button ID="cmbguardar1" runat="server" Text="Procesar" CssClass="buttonBBVA2" OnClick="cmbguardar1_Click" />
                    <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClientClick="mostrarCanceDiv()" CssClass="buttonSecBBVA2" />
                    <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                </td>
            </tr>
        </table>
    </div>   
    <div id="divDetails" class = "panel" style="z-index: 1001; display: none; top: 148px; left: 140px;">
        <div style="position:absolute; top:10px; left:10px; width:97%; height:90px;text-align:left !important;">
            <table style="width:100%;">
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width: 95%;">
                                    <legend id="legendCarrusel"></legend>
                                </td>
                                <td colspan="2" style="width: 5%;">
                                    <legend id="CloseDetail">Cerrar</legend>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div class="pane">
                <table class="fieldsetBBVA" style="width: 100%">
                    <tr>
                        <th class="campos" style="width: 25%">Nombre:                    
                        </th>
                        <th class="campos" style="width: 25%">
                            <asp:Label ID="Nombre_INE" Font-Bold="true" Font-Underline="true" />
                        </th>
                        <th class="campos" style="width: 25%">Apellido Paterno:                    
                        </th>
                        <th class="campos" style="width: 25%">
                            <asp:Label ID="Ap_INE" Font-Bold="true" Font-Underline="true" />
                        </th>
                    </tr>
                    <tr>
                        <th class="campos">Apellido Materno:                    
                        </th>
                        <th class="campos">
                            <asp:Label ID="Am_INE" Font-Bold="true" Font-Underline="true" />
                        </th>
                        <th class="campos">OCR:                    
                        </th>
                        <th class="campos">
                            <asp:Label ID="Ocd_INE" Font-Bold="true" Font-Underline="true" /> 
                        </th>
                    </tr>
                    <tr>
                        <th class="campos">Biometrico:                    
                        </th>
                        <th class="campos">
                            <asp:Label ID="Biometrico" Font-Bold="true" Font-Underline="true" />
                        </th>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />
    <asp:HiddenField ID="hdBandera" runat="server" />
    <asp:HiddenField ID="hdnResultado" runat="server" />    
    <asp:HiddenField ID="hdnResultado1" runat="server" />
    <asp:HiddenField ID="hdnResultado2" runat="server" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
</asp:Content>

