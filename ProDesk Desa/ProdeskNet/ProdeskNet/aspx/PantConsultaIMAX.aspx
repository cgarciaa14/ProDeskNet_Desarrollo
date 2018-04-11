<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.master" CodeFile="PantConsultaIMAX.aspx.vb" Inherits="aspx_PantConsultaIMAX" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.master" %>
<%--RQADM-38:RHERNANDEZ:05/05/17: Se crea pantalla para la tarea de Consulta IMAX--%>
<%--BUG-PD-54: RHERNANDEZ: 22/05/17:SE QUITA AUTOPOSTBACK DE COMBOS DEBIDO A QUE AHORA LA PREGUNTA NO REALIZA NINGUN RECHAZO DE DOCUMENTOS--%>
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BUG-PD-178:JBEJAR 31/07/2017 Se agrega opción a preguntas por petición del usuario--%>
<%--BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN--%>
<%--BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS PARA ABRIR VISOR TELEPRO--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

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
            fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');
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
                }
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
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Consulta IMAX"></asp:Label></legend>
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
                    <td>
                        Numero de Cliente:
                    </td>
                    <td>
                        <asp:Textbox ID="lblnocliente" runat="server" CssClass="txt2BBVA" Enabled="false"></asp:Textbox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>Tipo de Identificación * </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoID" runat="server" CssClass="selectBBVA">
                            <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Credencial de Elector"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Pasaporte"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Cedula Profesional"></asp:ListItem>
                            <asp:ListItem Value="3" Text="FM2/FM3 para extranjeros"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Cliente no disponible"></asp:ListItem> 
                        </asp:DropDownList></td>
                    <td>&nbsp;</td>
                     <td>Foto y firmas iguales *</td>
                    <td>
                        <asp:DropDownList ID="ddlfotoyfirma" runat="server" CssClass="selectBBVA">
                            <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
            </table>
            <br />


            <table id="tbValidarObjetos" class="resulGrid"></table>
        </div>
    </div>
    <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top:15%; left:31%; width: 220px;">
        <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID="btnCancelar" PopupControlID="popCancela" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>--%>
<%--        <asp:Panel ID="popCancela" runat="server" CssClass="cajadialogo ">
        </asp:Panel>--%>
            <div class="tituloConsul">
                <asp:Label ID="Label1" runat="server" Text="Cancelación" />
            </div>
            <table width="100%">
                <tr>
                    <td class="campos">Usuario:</td>
                    <td>
                        <asp:TextBox ID="txtusua" SkinID="txtGeneral" MaxLength="12" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="campos">Password:</td>
                    <td>
                        <asp:TextBox ID="txtpass" runat="server" SkinID="txtGeneral" MaxLength="12" TextMode="Password" EnableTheming="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="campos">Descripción:</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="TxtmotivoCan" runat="server" onkeypress="return ValCarac(this.event, 6);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" class="Text" rows="5" cols="1" style="width: 95%"></textarea>
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
                <td align="right" valign="middle">
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2" />
                    <asp:Button ID="cmbguardar1" runat="server" Text="Procesar" CssClass="buttonBBVA2" />
                    <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClientClick="mostrarCanceDiv()" CssClass="buttonSecBBVA2" />
                    <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />
    <asp:HiddenField ID="hdnResultado" runat="server" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
</asp:Content>
