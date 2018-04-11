<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="CheckHB.aspx.vb" Inherits="aspx_CheckHB" %>
<%--BBVA-P-423 RQADM-11 GVARGAS 21/03/2017 Antifraude Básico – Carruseles (Celulares, RFC, ID’s, Contador de Teléfonos) 31--%>
<%--BBVA-P-423 RQADM-13 Check HB GVARGAS 23/03/2017 Check Carruseles y Visor Documental--%>
<%--BUG-PD-28  GVARGAS  10/04/2017 Cambio label --%>
<%--BUG-PD-38 GVARGAS 24/04/2017 TAB Check HB sol turnada--%>
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BBVA-P-423 RQ-CHECKHB-ProDesk GVARGAS 24/07/2017 detalles HIT--%>
<%--BUG-PD-163 GVARGAS 31/07/2017 Cambios detalles--%>
<%--BUG-PD-183 GVARGAS 03/08/2017 Cambios detalles Carrusel TEL--%>
<%--'BUG-PD-250: CGARCIA: 31/10/2017: LINK DE REPORTES EN LA CONSULTA DE DETALLES DE CELULAR, TELEFONO Y RFC--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
    <script type="text/javascript" src="../js/Funciones.js"></script>

    <script type="text/javascript" language="javascript">
        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();
        }

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

        function pageLoad() {
            $("#ddlSelect").on("change", function () {
                var opcion = $("#ddlSelect").val().toString();

                if (opcion == "0") {
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_opc").val("");
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcesarCliente").prop("disabled", true);
                }
                else if (opcion == "1") {
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_opc").val(opcion);
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcesarCliente").prop("disabled", false);
                }
                else if (opcion == "2") {
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_opc").val(opcion);
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcesarCliente").prop("disabled", false);
                }
                else if (opcion == "3") {
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_opc").val(opcion);
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcesarCliente").prop("disabled", false);
                }
            });
        }

        function jsonBack(datos) {
            var settings = { type: "POST", url: "CheckHB.aspx/valueTurned", async: false, data: datos,
                             contentType: "application/json; charset=utf-8", dataType: "json", success: OnSuccessProccess };
            $.ajax(settings);
        }

        function OnSuccessProccess(response) { $("#ddlSelect").val(response.d.toString()); }

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
                var datos = '{"folio": "' + $.urlParam("sol").toString() + '", "pantalla": "' + $.urlParam("idPantalla").toString() + '" }';
                jsonBack(datos);
            }

            $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcesarCliente").prop("disabled", true);
            var id = $.urlParam("idPantalla").toString();

            if (id != null) {
                $('#c_r_1').html('NO');
                $('#c_c_1').html('NO');
                $('#c_t_1').html('NO');

                $('#c_r_2').html('NO');
                $('#c_c_2').html('NO');
                $('#c_t_2').html('NO');

                $('#c_r_3').html('NO');
                $('#c_c_3').html('NO');
                $('#c_t_3').html('NO');

                $('#c_r_4').html('NO');
                $('#c_c_4').html('NO');
                $('#c_t_4').html('NO');
            }

            $("#ddlSelect").on("change", function () {
                var opcion = $("#ddlSelect").val().toString();

                if (opcion == "0") {
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_opc").val("");
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcesarCliente").prop("disabled", true);
                }
                else if (opcion == "1") {
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_opc").val(opcion);
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcesarCliente").prop("disabled", false);
                }
                else if (opcion == "2") {
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_opc").val(opcion);
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcesarCliente").prop("disabled", false);
                }
                else if (opcion == "3") {
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_opc").val(opcion);
                    $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcesarCliente").prop("disabled", false);
                }
            });

            $("label").on("click", function (e) {
                var id_label = e.target.id.toString();
                if ((id_label == "c_cel_1") || (id_label == "c_tel_1") || (id_label == "c_tel_3") || (id_label == "c_tel_4") || (id_label == "c_rfc_1")) {
                    showDetails(id_label, $.urlParam("sol").toString());
                }
            });

            $("#CloseDetail").on("click", function (e) {
                $('#legendCarrusel').html('');
                $('#ventanaContain').hide();
                $('#divDetails').hide();
                $("#tableFields tbody").empty();
            });
            //'BUG-PD-: CGARCIA: 31/10/2017: LINK DE REPORTES EN LA CONSULTA DE DETALLES DE CELULAR, TELEFONO Y RFC
            function showDetails(id_label, sol) {
                
                var idPantalla = $.urlParam("idPantalla").toString();
                var settings = {
                    type: "POST", url: "CheckHB.aspx/getDetails", async: false, data: "{'id': '" + id_label + "', 'sol': '" + sol + "', 'Pantalla': '" + idPantalla + "'}",
                    contentType: "application/json; charset=utf-8", dataType: "json",
                    success: function OnSuccess_showDetails(response) {
                        $('#ventanaContain').show();
                        $('#divDetails').show();

                        var legend = "Detalles HIT";
                        var tipo = 0;

                        if (id_label == "c_cel_1") {
                            legend = "Detalles Carrusel Celular.";
                            tipo = 1;
                        } else if (id_label == "c_tel_1") {
                            legend = "Detalles Carrusel Teléfono Solicitante.";
                            tipo = 2;
                        } else if (id_label == "c_tel_3") {
                            legend = "Detalles Carrusel Teléfono Referencia 1.";
                            tipo = 2;
                        }
                        else if (id_label == "c_tel_4") {
                            legend = "Detalles Carrusel Teléfono Referencia 2.";
                            tipo = 2;
                        } else if (id_label == "c_rfc_1") {
                            legend = "Detalles Carrusel RFC.";
                            tipo = 3;
                        }
                        else { legend = "Detalles Carrusel."; }                        

                        $('#legendCarrusel').html(legend);

                        var items = $.parseJSON(response.d.toString());
                        var ban = 0;

                        $.each(items, function (i, item) {
                            $.each(item, function (ii, item_) {
                                if (ban == 0) {
                                    var tams = [];
                                    var Nombres = [];
                                    if (tipo == 1) {
                                        tams = ["33", "33", "34"];
                                        Nombres = ["FOLIO", "CELULAR", "RFC"];
                                    } else if (tipo == 2) {
                                        tams = ["33", "33", "34"];
                                        Nombres = ["FOLIO", "TELÉFONO", "APARECE COMO"];
                                    } else if (tipo == 3) {
                                        tams = ["20", "20", "60"];
                                        Nombres = ["FOLIO", "RFC", "NOMBRE"];
                                    }

                                    var $InitialRow = $('<tr id="headers_">' +
                                                            '<th class="HeaderDetail" style="width: ' + tams[0] + '%">' + Nombres[0] + '</th>' +
                                                            '<th class="HeaderDetail" style="width: ' + tams[1] + '%">' + Nombres[1] + '</th>' +
                                                            '<th class="HeaderDetail" style="width: ' + tams[2] + '%">' + Nombres[2] + '</th>' +
                                                        '</tr>');
                                    ban = 1;
                                    $('#tableFields tbody:last').append($InitialRow);
                                }
                                if (tipo == 1) {
                                    var $row = $('<tr>' +
                                                    '<td class="campos" style="width: 33%"><a href="' + item_.LIGA + '" target="_blank">' + item_.FOLIO + '</a></td>' +
                                                    '<td class="campos" style="width: 33%">' + item_.MOVIL + '</td>' +
                                                    '<td class="campos" style="width: 33%">' + item_.RFC + '</td>' +
                                                 '</tr>');
                                    $('#tableFields tbody:last').append($row);
                                } else if (tipo == 2) {
                                    var $row = $('<tr>' +
                                                    '<td class="campos" style="width: 33%"><a href="' + item_.LIGA + '" target="_blank">' + item_.FOLIO + '</a></td>' +
                                                    '<td class="campos" style="width: 33%">' + item_.TEL + '</td>' +
                                                    '<td class="campos" style="width: 33%">' + item_.TIPO_TEL + '</td>' +
                                                 '</tr>');
                                    $('#tableFields tbody:last').append($row);
                                } else if (tipo == 3) {
                                    var $row = $('<tr>' +
                                                    '<td class="campos" style="width: 33%"><a href="' + item_.LIGA + '" target="_blank">' + item_.FOLIO + '</a></td>' +
                                                    '<td class="campos" style="width: 33%">' + item_.RFC + '</td>' +
                                                    '<td class="campos" style="width: 33%">' + item_.NOMBRE + '</td>' +
                                                 '</tr>');
                                    $('#tableFields tbody:last').append($row);
                                }
                            });
                        });
                    }
                };
                $.ajax(settings);
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

        .tooltip {
            position: relative;
            display: inline-block;
            text-decoration: none;
            border-bottom: 1px dashed #999;
        }

        .tooltip .tooltiptext {
            visibility: hidden;
            min-width: 100px;
            max-width: 400px;
            min-height: 30px;
            max-height: 200px;
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
            overflow: auto;
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

        .pane {
            overflow-y: scroll;
            max-height: 170%;
        }

        #insertarCampos {
            text-align: center;
            margin: auto;
        }

        .HeaderDetail {
            font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;
            font-size: 14px;
            line-height: 1.428571429;
            color: #333;
        }
    </style>
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
                <table id="tableFields" width="100%" class="fieldsetBBVA">
                    <tbody />
                </table>
            </div>
        </div>
    </div>
    <div class="divPantConsul">
        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width: 70%;">
                                    <legend>Check HB.</legend>
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
        <div id="visor" class="resulbbvaCenter">
            <table align="center">
	            <tr>
                    <th>
                        <asp:Button runat="server" ID="btnVisor" Text="Visor Documental" CssClass="buttonSecBBVA2" OnClick="btnVisor_Click" />
                    </th>
                    <th>
                        <label for="ddlSelect"> Tipificaciones : </label>
                        <select id="ddlSelect" class="selectBBVA" style="width:80%;">
                            <option value="0">&lt Seleccionar &gt</option>
                            <option value="1">Procesable</option>
                            <option value="2">Rechazado</option>
                            <option value="3">Dudoso</option>
                        </select>
                    </th>
                </tr>
            </table>
        </div>
        <table id="tbl">
            <tr class="tr">
                <th class="th"></th>
                <th class="th">Carrusel RFC</th>
                <th class="th">Carrusel Celulares</th>
                <th class="th">Carrusel Contador Teléfonos</th>
            </tr>
            <tr class="tr">
                <td class="td">Solicitante</td>
                <td class="td" ID="c_r_1" runat="server" ></td>
                <td class="td" ID="c_c_1" runat="server" ></td>
                <td class="td" ID="c_t_1" runat="server" ></td>
            </tr>
            <tr class="tr">
                <td class="td">Empresa</td>
                <td class="td" ID="c_r_2" runat="server" ></td>
                <td class="td" ID="c_c_2" runat="server" ></td>
                <td class="td" ID="c_t_2" runat="server" ></td>
            </tr>
            <tr class="tr">
                <td class="td">Referencia 1</td>
                <td class="td" ID="c_r_3" runat="server" ></td>
                <td class="td" ID="c_c_3" runat="server" ></td>
                <td class="td" ID="c_t_3" runat="server" ></td>
            </tr>
            <tr class="tr">
                <td class="td">Referencia 2</td>
                <td class="td" ID="c_r_4" runat="server" ></td>
                <td class="td" ID="c_c_4" runat="server" ></td>
                <td class="td" ID="c_t_4" runat="server" ></td>
            </tr>
        </table>
        <div class="resulbbvaCenter divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" />
                        <input type="button" runat="server" id="btnProcesarCliente" value="Procesar" onclick="btnProcesarCliente_click();" class="buttonBBVA2"  disabled/>
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
                            <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral" />
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
