<%@ Page Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="ventanaBiometrico.aspx.vb" Inherits="aspx_ventanaBiometrico" %>

<%--RQ-PD33: DJUAREZ: 03/05/2018: Se crea nueva pantalla para visualizar biometría --%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

        });
        function autSolicitud() {
            $("#btnTurnar").prop("disabled", true);
            var numCheck = 0;
            var numUpd = 0;
            $(".checkActive").each(function () {
                if ($(this).find('[id$=chkOption]')[0].checked) {
                    numCheck = numCheck + 1;
                }
            });
            if (numCheck > 0) {
                $(".checkActive").each(function () {
                    var check = this;
                    if ($(this).find('[id$=chkOption]')[0].checked) {
                        numUpd = numUpd + 1;
                        $('[id$=txtSolAutenticar]').val($(this).find('[id$=hdnId]').val());
                        if (numCheck = numUpd) {
                            $('[id$=txtCompleto]').val("COMPLETO");
                        }
                        $('[id$=btnAutenticar]').click();
                    }
                });
            }
            else {
                alert("Selecciones al menos una solicitud.");
            }
            $("#btnTurnar").prop("disabled", false);
        }

        function Clear() {
            $('[id$=tbxNumeroSolicitud]').val("");
            //$(".checkActive").each(function () {
            //    $(this).find('[id$=chkOption]')[0].checked = false;
            //});
        }

        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();
        }

        function ocultarCanceDiv() {
            $('#ventanaContain').hide();
            $("#divcancela").hide();
        }

        function btnGuardar(action) {
            $("#btnCancelarNew").prop("disabled", true);
            var numCheck = 0;
            var numUpd = 0;
            $(".checkActive").each(function () {
                if ($(this).find('[id$=chkOption]')[0].checked) {
                    numCheck = numCheck + 1;
                }
            });

            if (numCheck > 0) {
                $(".checkActive").each(function () {
                    var check = this;
                    if ($(this).find('[id$=chkOption]')[0].checked) {
                        numUpd = numUpd + 1;

                        var f = $(this).find('[id$=hdnId]').val();
                        var u = $('[id$=hdnUsua]').val();
                        var cadena = 'UPDATE PDK_OPE_SOLICITUD SET PDK_OPE_MODIF = GETDATE() WHERE PDK_ID_SOLICITUD = ' + f;
                        var cadenaUp = '';
                        var pantalla = $('[id$=hdNomPantalla]').val();


                        if (action == "Autoriza") {
                            //cambiaVisibilidadDiv('divautoriza', false);
                            var txtUsu = $('[id$=txtUsuario]').val();
                            var txtpsswor = $('[id$=txtPassw]').val();
                            var idpantalla = $("[id$=hdnIdPantalla] ").val();
                            var txtmotivoOb = $("[id$=txtmotivo]").val();
                            var hasError = false;
                            
                            var textResult = 'Información de la solicitud:\n'
                            //           var cadenastored = 'EXEC sp_ValidacionUsuario ' + '"' + cadenatodo + '",' + idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1'
                            $('#ventanaContain').hide();

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

                        } else if (action == "Cancelar") {
                            ocultarCanceDiv();
                            var txtUsu = $('[id$=txtusua]').val();
                            var txtpsswor = $('[id$=txtpass]').val();
                            var idpantalla = 69;
                            var txtmotivoOb = $("[id$=TxtmotivoCan]").val();
                            $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())
                            $('#ventanaContain').hide();
                            if (txtUsu == '') { alert('El campo usuario esta vacío'); return; }
                            if (txtpsswor == '') { alert('El campo del password esta vacío'); return; }
                            if (txtmotivoOb == '') { alert('El campo de motivo esta vacío'); return; }

                            btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)
                        }
                    }
                });
            }
            else {
                alert("Selecciones al menos una solicitud.");
            }
            $("#btnCancelarNew").prop("disabled", false);
        }
    </script>
    <style type="text/css">
        .resulbbvaCenter_local {
            font-family: Arial;
            font-size: 8pt;
            width: 98%;
        }

        .resulbbvaCenter_local table {
            display: block;
            overflow: auto;
            /*overflow-x: hidden;*/
        }


        .resulbbvaCenter_local tbody {
            overflow: auto;
            height: 340px;
            /*overflow-x: hidden;*/
        }

        .resulbbvaCenter_local tr {
            padding-bottom: 3px;
        }

        .resulbbvaCenter_local th {
            /*width: 25%;*/
            font-family: Arial;
            font-size: 8pt;
            font-weight: bold!important;
            background-color: White;
            color: #666666;
            border-top: 1px solid White;
            border-left: 1px solid White;
            border-right: 1px solid White;
            border-bottom: 1px solid #D8D8D8;
            text-align: center;
        }

        .resulbbvaCenter_local th:last-child {
            /*width: 140px !important;*/
        }

        .resulbbvaCenter_local td {
            width: 115px !important;
            font-family: Arial;
            font-size: 8pt;
            background-color: White;
            color: #666666;
            border-top: 1px solid White;
            border-bottom: 1px solid White;
            border-left: 1px solid White;
            border-right: 1px solid White;
            text-align: center;
        }

        .resulbbvaCenter_local td:first-child {
            width: 60px !important;
            padding-right: 5px;
            background-color: #EFEFEF!important;
        }

        .resulbbvaCenter_local th:first-child {
            /*width: 60px !important;*/
            padding-right: 5px;
        }

 
    </style>

    <script type="text/javascript" language="javascript">
        function btnBuscarCliente_Click() {
            $('#<%=btnBuscar.ClientID%>').click()
        }
    </script>

    <div class="divPantConsul">
        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width: 70%;">
                                    <legend>Ventana Biometrico
                                    </legend>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

        <div id="divFiltros" class="resulbbvaCenter" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNumeroSolicitud" Text="Número de Solicitud"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbxNumeroSolicitud" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,7);" MaxLength="10"></asp:TextBox>
                    </td>
                    <td>
                        <input type="button" id="btnBuscarCliente" class="buttonBBVA2" onclick="btnBuscarCliente_Click();" value="Buscar">
                    </td>
                    <td>
                        <input type="button" id="btnLimpiarResultados" class="buttonSecBBVA2" onclick="Clear();" value="Limpiar">
                    </td>
                </tr>
            </table>
        </div>

        <div class="resulbbvaCenter_local" id="divTableResult" runat="server" style="height: 70%; overflow:auto;">
            <table id="tableResult" style="font-size: 12px;" cellpadding="0" cellspacing="0" border="0">
                <thead>
                    <tr class="GridviewScrollHeaderBBVA_local">
                        <th style="width:12%;">Solicitud</th>
                        <th style="width:40%;">Mensaje</th>
                        <th style="width:17%;">Pantalla Autentica</th>
                        <th style="width:17%;">Pantalla Error</th>
                        <th style="width:10%;">Intentos</th>
                        <th style="width:20%;">Seleccionar</th>
                    </tr>
                </thead>
                <tbody id="bodyRepSol" runat="server">
                    <asp:Repeater ID="repSolicitudes" runat="server">
                        <ItemTemplate>
                            <tr class="GridviewScrollItemBBVA_local" style="width:100%;">
                                <td style="width:12%;">
                                    <asp:Label ID="Label0" runat="server" Text='<%# Eval("NumeroSolicitud")%>' ToolTip='<%# Eval("NumeroSolicitud")%>'> </asp:Label>
                                </td>
                                <td style="width:40%;">
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Mensaje")%>' ToolTip='<%# Eval("NombreCliente")%>'> </asp:Label>
                                </td>
                                <td style="width:17%;">
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("PantallaAut")%>' ToolTip='<%# Eval("NombreCliente")%>'> </asp:Label>
                                </td>
                                <td style="width:17%;">
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("PantallaError")%>' ToolTip='<%# Eval("NombreCliente")%>'> </asp:Label>
                                </td>
                                <td style="width:10%;">
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("NumError")%>' ToolTip='<%# Eval("NombreCliente")%>'> </asp:Label>
                                </td>
                                <td style="width:20%;" class="checkActive">
                                    <asp:CheckBox ID="chkOption" class="flat-checkbox" runat="server" />
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("NumeroSolicitud") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <div class="resulbbvaCenter" id="divEmptyTableResult" runat="server" style="text-align: center; ">
                <br />
                <br />
                <br />
                <br />
                - No existen datos para Mostrar -
            </div>
        </div>

        <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top:15%; left:31%; width: 220px;">
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
                        <textarea id="TxtmotivoCan" runat="server"  class="Text" rows="5" cols="1" style="width: 95% !important;"></textarea>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td>
                        <asp:HiddenField runat="server" ID="HiddenField1" />
                    </td>
                    <td align="left" valign="middle">
                        <input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar('Cancelar')" />
                        <input id="btnCancelCancela" type="button" runat="server" value="Cancelar" class="Text " onclick="ocultarCanceDiv()" />
                    </td>
                </tr>

            </table>
        </div>

        <div id="divTurnar" runat="server" class="resulbbvaCenter">
            <table>
                <tr>
                    <td>
                        <input type="button" id="btnTurnar"  style="background-color:#094FA4" Class="buttonBBVA2"  value="Autenticar" onclick="autSolicitud()"/>
                    </td>
                    <td>
                        <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv()" id="btnCancelarNew" />
                    </td>
                </tr>
            </table>
        </div>

        <div style="visibility: collapse">
            <asp:HiddenField ID="hdnNumeroSolicitud" runat="server" />
            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="buttonBBVA2" OnClick="btnBuscar_Click" />
            <asp:Button runat="server" ID="btnAutenticar" OnClick="btnAutenticar_Click" Text="Autenticar" />
            <asp:TextBox runat="server" ID="txtSolAutenticar" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,7);" MaxLength="10"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtCompleto" CssClass="txt3BBVA" ></asp:TextBox>
            <asp:HiddenField ID="hdnUsua" runat="server" />
            <asp:HiddenField ID="hdnResultado" runat="server" />
            <asp:HiddenField ID="hdRutaEntrada" runat="server" />
            <asp:HiddenField ID="hdNomPantalla" runat="server" />
        </div>
    </div>
</asp:Content>

