<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="manejaCambioAgencia.aspx.vb" Inherits="aspx_manejaCambioAgencia" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxCT" %>--%>

<%--BBV-P-423 - RQADM-31 22/03/2017 MAPH Creación de aspx para cambio de Agencia --%>
<%--BUG-PD-36  25/04/2017 MAPH Validación del repeater en el front, inclusión de multiselect en el repeater, maxlength a 9 de número de solicitud. 
               27/04/2017 MAPH Establecimiento del maxlength a 10 del Número de Solicitud--%>
<%--BUG-PD-119  26/06/2017 ERODRIGUEZ Se agregaron validaciones y campos necesarios para guardar un historial de agencias cambiadas por solicitud--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">

    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" language="javascript">

        function pageLoad()
        {
            $('input').on("change paste keyup dragend", function () {
                var dict = { "á": "a", "é": "e", "í": "i", "ó": "o", "ú": "u", "Á": "A", "É": "E", "Í": "I", "Ó": "O", "Ú": "U" };
                $(this).val($(this).val().toString().replace(/[^\w ]/g, function (char) { return dict[char] || char; }));
            });
        }

        function btnCambiarCliente_click() {
            var textError = 'Debe seleccionar ';
            var checked = false;
            var checkBoxes = $("input[type='checkbox']");
            $.each(checkBoxes, function () {
                if ($(this).is(":checked")) {
                    checked = true;
                }
            });
            if (checked == false) {
                textError += 'como mínimo un registro'
            }

            if ($('#<%= ddlAgencia.ClientID%>').val() > 0 && $('#<%= ddlVendedor.ClientID%>').val() > 0 && checked == true) {
                if (confirm("¿Está seguro de realizar el cambio de Agencia?")) {
                    $('#<%= btnCambiar.ClientID%>').click();
                }
                else {
                    alert('No Actualizado');
                }
            }
            else {
                if((checked == false) && ($('#<%= ddlAgencia.ClientID%>').val() <= 0 && $('#<%= ddlVendedor.ClientID()%>').val() <= 0))
                {
                    textError += ', ';
                }
                else
                {
                    if((checked == false) && ($('#<%= ddlAgencia.ClientID%>').val() <= 0 || $('#<%= ddlVendedor.ClientID%>').val() <= 0))
                    {
                        textError += ' y ';
                    }
                }
                
                textError += $('#<%= ddlAgencia.ClientID%>').val() <= 0 ? 'una agencia' : '';
                textError += ($('#<%= ddlAgencia.ClientID%>').val() <= 0 && $('#<%= ddlAgencia.ClientID%>').val() <= 0) ? ' y ' : '';
                textError += $('#<%= ddlVendedor.ClientID%>').val() <= 0 ? 'un vendedor' : '';
                alert(textError);
            }
        }

        function btnLimpiarCliente_Click() {
            $('#<%=tbxNumeroSolicitud.ClientID%>').val('');
            $('#<%=tbxNombreSolicitante.ClientID%>').val('');
            $('#<%=tbxRFCSolicitante.ClientID%>').val('');
            $('#<%=btnBuscar.ClientID%>').click();
        }

        function chkSelectAll_click()
        {
            var checked = $('#chkSelectAll').is(":checked");
            var checkBoxes = $("input[type='checkbox']");
            $.each(checkBoxes, function () {
                $(this).prop("checked", checked);
            });
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
                                    <legend>Cambio de Agencia
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
                        <asp:Label runat="server" ID="lblNombreSolicitante" Text="Nombre del Solicitante"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbxNombreSolicitante" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,25);"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblRFCSolicitante" Text="RFC del Solicitante"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbxRFCSolicitante" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,22);"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="buttonBBVA2" OnClick="btnBuscar_Click" />
                    </td>
                    <td>
                        <input type="button" id="btnLimpiarCliente" value="Limpiar" onclick="btnLimpiarCliente_Click();" class="buttonSecBBVA2" />
                    </td>
                </tr>
            </table>
        </div>

        <div class="resulbbvaCenter" id="divTableHeader" runat="server" style="overflow: hidden;">
            <table style="font-size: 12px; width: 100%" cellpadding="0" cellspacing="0" border="0">
                <thead>
                    <tr class="GridviewScrollHeaderBBVA" style="width: 100%">
                        <th style="width: 20%; text-align:left">Número de Solicitud</th>
                        <th style="width: 20%; text-align:left">Nombre del solicitante</th>
                        <th style="width: 20%; text-align:left">RFC del solicitante</th>
                        <th style="width: 20%; text-align:left">Agencia</th>
                        <th style="width: 20%; text-align:center">Seleccionar todo <input id="chkSelectAll" type="checkbox" onclick="chkSelectAll_click();" /> </th>
                    </tr>
                </thead>
            </table>
        </div>

        <div class="resulbbvaCenter" id="divTableResult" runat="server" style="overflow: auto; height: 65%; width: 100%">
            <table style="font-size: 12px; width: 100%" cellpadding="0" cellspacing="0" border="0">
                <tbody>
                    <asp:Repeater ID="repAgenciaVendedor" runat="server">
                        <ItemTemplate>
                            <tr class="GridviewScrollItemBBVA" style="width: 80%">
                                <td style="width: 20%"><%# Eval("ID_SOLICITUD")%></td>
                                <td style="width: 20%"><%# Eval("NOMBRE")%></td>
                                <td style="width: 20%"><%# Eval("RFC")%></td>
                                <td style="width: 20%"><%# Eval("AGENCIA")%></td>
                                <td style="text-align: center; width: 20%">
                                    <asp:CheckBox ID="chkOption" class="flat-checkbox checkJOJOJO" runat="server" />
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("ID_SOLICITUD") %>' />
                                    <asp:HiddenField ID="hdnIdCot" runat="server" Value='<%#Eval("ID_COTIZACION")%>' />
                                    <asp:HiddenField ID="hdIdAgAnt" runat="server" Value='<%#Eval("ID_AGENCIA")%>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

        <div class="resulbbvaCenter" id="divEmptyTableResult" runat="server" style="text-align: center">
            <br />
            <br />
            <br />
            <br />
            - No existen datos para Mostrar -
        </div>

        <div id="div" class="divAdminCatPie" runat="server">
            <asp:UpdatePanel runat="server" ID="updDropDowns" UpdateMode="Conditional">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblAgencia" Text="Cambiar Agencia a"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlAgencia" OnSelectedIndexChanged="ddlAgencia_SelectedIndexChanged" AutoPostBack="true" Width="160px"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="Label2" Text="Cambiar Vendedor a"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlVendedor" Width="160px"></asp:DropDownList>
                            </td>
                            <td>
                                <input type="button" id="btnCambiarCliente" onclick="btnCambiarCliente_click();" class="buttonBBVA" value="Cambiar">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div style="visibility: collapse">
            <asp:Button runat="server" ID="btnCambiar" OnClick="btnCambiar_Click" />
        </div>
    </div>
</asp:Content>

