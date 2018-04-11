<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="metodoAutenticacion.aspx.vb" Inherits="aspx_metodoAutenticacion" %>

<%--BBV-P-423 RQ-PD-17 9 GVARGAS 30/01/2018 Ajustes flujo--%>
<%--BBV-P-423 RQ-PD-17 12 GVARGAS 06/02/2018 Ajustes flujos 3--%>
<%--BBV-P-423 RQ-PD-17 15 GVARGAS 16/02/2018 New reader methotd --%>
<%--BUG-PD-380 GVARGAS 03/03/2018 Cambio cerrar divDetail--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
    <script type="text/javascript">
        function PopUpLetreroProccess() {
            $('#btnprocesar').prop("disabled", true);
            var mesajeRe = "Procesando";
            $("[id$=lblMensaje]").text(mesajeRe);
            centraVentana($('#ventanaconfirm'));

            $('#ventanaContain').show();
            $('#ventanaconfirm').show();
        }

        function btnGuardar(action) {
            var f = $.urlParam("sol").toString(); //$('[id$=hdnIdFolio]').val();
            var u = $.urlParam("usu").toString(); //$('[id$=hdnUsua]').val();
            //BUG-PD-16 MAPH 08/03/2016 Modificación para mostrar mensaje exitoso al guardar o cancelar registros
            var cadena = 'UPDATE PDK_OPE_SOLICITUD SET PDK_OPE_MODIF = GETDATE() WHERE PDK_ID_SOLICITUD = ' + f;
            var cadenaUp = '';
            var pantalla = $.urlParam("idPantalla").toString(); //$('[id$=hdNomPantalla]').val();

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
                var idpantalla = $.urlParam("idPantalla").toString(); //$("[id$=hdnIdPantalla] ").val();
                var txtmotivoOb = $("[id$=TxtmotivoCan]").val();
                $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)
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

        function btnProcesar() {
            PopUpLetreroProccess();
            var auth = 0;
            if ($('#chkIneAuth').is(':checked')) { auth = 1; }
            INEInformation(auth);
        }

        function INEInformation(auth) {
            var settings = { type: "POST", url: "", async: false, data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
            settings.url = "metodoAutenticacion.aspx/getInformationINE";
            settings.data = "{'sol': " + $.urlParam("sol").toString() + ", 'idPant': " + $.urlParam("idPantalla").toString() + ", 'auth': " + auth + ", 'usu': '" + $.urlParam("usuario").toString() + "'}";
            settings.success = OnSuccessINEInformation;
            settings.failure = function (response) {
                $('#btnprocesar').prop("disabled", false);
                alert("Error al validar Autenticación.");
            }
            $.ajax(settings);
        }

        function OnSuccessINEInformation(response) {
            var items = $.parseJSON(response.d.toString());
            PopUpLetreroRedirect(items.message.toString(), items.url.toString());
        }

        $(document).ready(function () {
            $.urlParam = function (name) {
                var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
                if (results == null) {
                    return null;
                }
                else {
                    return results[1] || 0;
                }
            }

            if ($.urlParam("Enable").toString() == "1") {
                $('#btnprocesar').prop("disabled", true);
                $('#chkIneAuth').prop("disabled", true);
                $('#btnprocesar').hide();
                $('#ctl00_ctl00_cphCuerpo_cphPantallas_btnCancelar').hide();
            }
        });
    </script>
    <style>
        #tbl {
            font-family: arial, sans-serif;
            border-collapse: collapse;
            width: 100%;
            top: 30%;
            position: absolute;
        }

        #info {
            position: absolute;
            top: 11%;
        }

        .th {
            FONT-SIZE: 11pt;
            FONT-WEIGHT: bold;
            background-color: #5089c3;
            color: #FFFFFF;
        }

        .td {
            FONT-SIZE: 9pt;
        }

        .thSol {
            FONT-SIZE: 9pt;
            FONT-WEIGHT: bold;
            padding-bottom: 2%;
        }

        .td, .th {
            border: 1px solid #dddddd;
            text-align: left;
            padding: 8px;
        }

        .tr:nth-child(even) {
            background-color: #dddddd;
        }
    </style>
    <div class="divPantConsul">
        <div class ="divFiltrosConsul" style="padding-right:5px;" >
            <table class="tabFiltrosConsul">
                <tbody>
                    <tr class="tituloConsul">
                        <td colspan="2" style="width:50%;">Metodo Autenticación</td>
                    </tr>
                </tbody>
            </table>
        </div>

        <table id="info" width="100%" class="fieldsetBBVA">
            <tr>
                <th class="campos" style="width: 25%">Solicitud: </th>
                <th class="campos" style="width: 25%">
                    <asp:Label ID="lblSolicitud" Font-Underline="true" runat="server"></asp:Label>
                </th>
                <th class="campos" style="width: 25%">Cliente: </th>
                <th class="campos" style="width: 25%">
                    <asp:Label ID="lblCliente" Font-Underline="true" runat="server"></asp:Label>
                </th>
            </tr>
            <tr>
                <th class="campos">Status Credito: </th>
                <th class="campos">
                    <asp:Label ID="lblStCredito" Font-Underline="true" runat="server"></asp:Label>
                </th>
                <th class="campos">Status Documentos: </th>
                <th class="campos">
                    <asp:Label ID="lblStDocumento" Font-Underline="true" runat="server"></asp:Label>
                </th>
            </tr>
<%--            <tr>
                <th class="campos" style="/*width: 40%;*/"></th>
                <th align="center" class="campos" style="/*width: 40%;*/ text-align: right"></th>
                <th class="campos" style="/*width: 40%;*/"></th>
            </tr>--%>
        </table>
        <div style="top: 35%; position:absolute; width: 100%; font-size: 10pt;" class="campos">
            <div style="margin:0 auto; text-align: center;">
                <input type="checkbox" id="chkIneAuth">Autoriza Captura Huella
            </div>
        </div>
        <div class="resulbbvaCenter divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" />
                        <input type="button" id="btnprocesar" value="Procesar" onclick="btnProcesar();" class="buttonBBVA2" />
                        <asp:Button runat="server" ID="btnAutorizar" Visible="false" Text="Autorizar" OnClientClick="cambiaVisibilidadDiv('divautoriza', true)" />
                        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="buttonSecBBVA2" OnClientClick="cambiaVisibilidadDiv('divcancela', true)" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div id="divcancela" style="display: none">
        <cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID="btnCancelar" PopupControlID="popCancela" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
        <asp:Panel ID="popCancela" runat="server" CssClass="cajadialogo ">
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
                        <textarea id="TxtmotivoCan" runat="server" onkeypress="ManejaCar('A',1,this.value,this)" class="Text" rows="5" cols="1" style="width: 95% !important;"></textarea>
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

        </asp:Panel>
    </div>
</asp:Content>

