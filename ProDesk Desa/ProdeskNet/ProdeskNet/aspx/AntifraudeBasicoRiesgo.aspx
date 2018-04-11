<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="AntifraudeBasicoRiesgo.aspx.vb" Inherits="aspx_AntifraudeBasicoRiesgo" %>

<%--BBVA-P-423 GVARGAS 05/05/2017 RQADM-07 Antifraude Básico Cliente, Empresa, Riesgos y Pre-Aprobados 40,76 --%>
<%--BUG-PD-57 GVARGAS 18/05/2017 Cambios al cancelar tarea --%>
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BBVA-P-423 RQ-INB225 GVARGAS 07/07/2017 Pantalla antifraude riesgos - ProDesk--%>
<%--RQ-PD24 DJUAREZ 19/02/2018 Se agrega la columna impagos a antifraude y se une en una columna el cliente --%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
<%--BUG-PD-377 DJUAREZ 01/03/2017 Se coloca la columna de impagos --%>
<%--BUG-PD-384 DCORNEJO 08/03/2018: Se agrega Monto Financiero--%>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
    <script type="text/javascript" src="../js/Funciones.js"></script>

    <script type="text/javascript" language="javascript">
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

        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();
        }

        function ocultarCanceDiv() {
            $('#ventanaContain').hide();
            $("#divcancela").hide();
        }

        function btnProcesarCliente_click() {

            //document.getElementById('btnProcesarCliente').disabled = false;
            $('#<%= btnProcesarCliente.ClientID%>').prop('disabled', true);
            var buttonProcesar = document.getElementById('<%= btnProcesar.ClientID%>');
            buttonProcesar.click();
            return;
        }

        function btnProcesarCliente_Enable() {

            $('#<%= btnProcesarCliente.ClientID%>').prop('disabled', false);
            // $('#btnProcesarCliente').prop('disabled', false);
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
        });

        $(document).ready(function () { $.urlParam = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } }; if ($.urlParam("Enable").toString() == "1") { $("#btnCancelarNew").hide(); }; });
    </script>
    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnCancelar {
            display: none;
        }
    </style>
    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_grd {
            font-family: arial, sans-serif;
            border-collapse: collapse;
            width: 100%;
            top: 22%;
            /*position: absolute;*/
            position: inherit;
        }

        #info {
            position: absolute;
            top: 11%;
        }

        #visor {
            position: absolute;
            top: 21%;
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

        .resulbbvaCenter td {
            text-align: center!important;
        }
    </style>
    <div class="divPantConsul">
        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width: 70%;">
                                    <legend>Antifraude Básico Riesgo.</legend>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
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
        </table>
        <div id="divHiddenButton" style="visibility: collapse">
            <asp:Button runat="server" ID="btnProcesar" Text="Procesar" OnClick="btnProcesar_Click" />
            <input type="text" runat="server" id="opc" />
        </div>

        <div id="popup" class="resulbbvaCenter" style="max-height: 310px; overflow-y: scroll; position: absolute; top: 22%; width: 100% !important;">
            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontro información.">
                <Columns>
                    <asp:BoundField HeaderText="Núm Cliente" DataField="Cta_BBVA"></asp:BoundField>
                    <asp:BoundField HeaderText="Nombre Cliente" DataField="Nombre_Cliente"></asp:BoundField>
                    <asp:BoundField HeaderText="RFC" DataField="RFC"></asp:BoundField>
                    <asp:BoundField HeaderText="Fecha de Nacimiento" DataField="FECHA_NAC"></asp:BoundField>
                    <asp:BoundField HeaderText="Dirección Completa" DataField="Domicilio"></asp:BoundField>
                    <asp:BoundField HeaderText="Score de Riesgo" DataField="Riesgo"></asp:BoundField>
                    <asp:BoundField HeaderText="Impagos" DataField="Impago"></asp:BoundField>
                </Columns>
                <EmptyDataTemplate>No se encontro información.</EmptyDataTemplate>
            </asp:GridView>

            <!--<table id="tblClientes" style="font-size: 12px; width: 100%" cellpadding="0" cellspacing="0" border="0">
                <thead>
                    <th>Núm Cliente</th>
                    <th>Nombre</th>
                    <th>Apellido Paterno</th>
                    <th>Apellido Materno</th>
                    <th>RFC</th>
                    <th>Fecha de Nacimiento</th>
                    <th>Dirección Completa</th>
                    <th>Score de Riesgo</th>
                </thead>
                <tbody>
                    <asp:Repeater ID="repClientes" runat="server">
                        <ItemTemplate>
                            <tr class="GridviewScrollItemBBVA">
                                <td>
                                    <asp:Label runat="server" id="lblNumCliente"> <%# Eval("Cta_BBVA")%> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" id="Label2"> <%# Eval("Nombre")%> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" id="Label3"> <%# Eval("Ape_Pat")%> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" id="Label4"> <%# Eval("Ape_Mat")%> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" id="Label5"> <%# Eval("RFC")%> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" id="Label6"> <%# Eval("FECHA_NAC")%> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" id="Label7"> <%# Eval("Domicilio")%> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" id="Label8"> <%# Eval("Riesgo")%> </asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>-->
        </div>

        <div style="position: absolute; top: 82%; width: 100%;">
            <table id="Table2" width="100%" class="fieldsetBBVA">
                <tbody>
                    <tr>
                        <th class="campos" style="width: 56%;"></th>
                        <th class="campos" style="width: 12%;">Monto Financiero:</th>
                        <th class="campos" style="width: 6%;">
                            <asp:TextBox ID="txtMonto" runat="server" CssClass="txt2BBVA" ReadOnly="true" />
                        </th>
                        
                        <th class="campos" style="width: 30%;"><asp:Label runat="server" ID="lbl_CVEBBVA" /></th>
                        <th class="campos" style="width: 8%;">Riesgo Total:</th>
                        <th class="campos" style="width: 6%;">
                            <asp:TextBox ID="txtRiesgo" runat="server" CssClass="txt2BBVA" ReadOnly="true" />
                        </th>
                        <th class="campos" style="width: 8%;">Total Impagos:</th>
                        <th class="campos" style="width: 6%;">
                            <asp:TextBox ID="txtImpago" runat="server" CssClass="txt2BBVA" ReadOnly="true" />
                        </th>
                    </tr>
                </tbody>
            </table>
        </div>

        <div class="resulbbvaCenter divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" />
                        <input type="button" runat="server" id="btnProcesarCliente" value="Procesar" onclick="btnProcesarCliente_click();" class="buttonBBVA2" />
                        <asp:Button runat="server" ID="btnAutorizar" Visible="false" Text="Autorizar" OnClientClick="cambiaVisibilidadDiv('divautoriza', true)" />
                        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="buttonSecBBVA2" OnClientClick="cambiaVisibilidadDiv('divcancela', true)" />
                        <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                    </td>
                </tr>
            </table>
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
                    <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral" OnClientClick="ocultarCanceDiv();" />
                    </td>
                </tr>

            </table>
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

