<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="SeleccionCliente.aspx.vb" Inherits="aspx_SeleccionCliente" %>

<%--BBV-P-423-RQADM01:MPUESTO:16/05/2017:Seleccion de Clientes--%>
<%--BUG-PD-69:MPUESTO:01/06/2017:Correcciones Seleccion de Clientes--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">

    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" language="javascript">

        function chkOption_click(idCheckBox, chBxSelectorClass) {
            chBxSelectorClass = '.' + chBxSelectorClass;
            idCheckBox = '#' + idCheckBox;
            var checked = $(idCheckBox).is(":checked");
            var checkBoxes = $(chBxSelectorClass);
            $.each(checkBoxes, function () {
                $(this).prop("checked", false);
            });
            $(idCheckBox).prop("checked", checked);
        }

        function btnProcesarCliente_click(){
            var arrayIds = [];
            var checkBoxes = $("input[type='checkbox']");
            $.each(checkBoxes, function () {
                if($(this).is(":checked")){
                    arrayIds.push($(this).attr("identifier"));
                }
            });
            if(arrayIds.length > 0){
                $('#<%=hdnArrayIds.ClientID%>').val(arrayIds);
                $('#<%=btnProcesar.ClientID%>').click();
            }
            else{
                alert('Debe seleccionar mínimo un registro');
            }
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
                                    <legend>Selección de Cliente
                                    </legend>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

        <br />

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

        <div class="resulbbvaCenter" id="divTableResult" runat="server" style="height: 70%;">
            <table id="tableResult" style="font-size: 12px;" cellpadding="0" cellspacing="0" border="0">
                <thead>
                    <tr class="GridviewScrollHeaderBBVA">
                        <th style="width: 250px">Nombre Completo</th>
                        <th style="width: 220px">Número de Cliente</th>
                        <th style="width: 350px">Dirección</th>
                        <th style="width: 200px">Selección</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomerAddress" runat="server">
                        <ItemTemplate>
                            <tr class="GridviewScrollItemBBVA">
                                <td style="width: 250px">
                                    <asp:Label ID="lblFullName" runat="server"
                                        Text='<%# Eval("FullName")%>'> </asp:Label>
                                </td>
                                <td style="width: 220px">
                                    <asp:Label ID="lblCustomerNumber" runat="server" Text='<%# Eval("ClientID")%>'> </asp:Label>
                                </td>
                                <td style="width: 350px">
                                    <asp:Label ID="lblCustomerAddress" runat="server" Text='<%# Eval("Address")%>'> </asp:Label>
                                </td>
                                <td style="width: 200px; text-align:center !important">
                                    <%--<asp:CheckBox ID="chkOption_" class="flat-checkbox" runat="server"/>--%>
                                    <input id="<%#String.Concat(Eval("ClientID"), "_", Eval("Identifier"))%>" type="checkbox" onclick='chkOption_click("<%#String.Concat(Eval("ClientID"), "_", Eval("Identifier"))%>    ", "<%#Eval("ClientID")%>    ");' class='<%#Eval("ClientID")%>' identifier='<%# Eval("Identifier")%>' />
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
        <div class="resulbbvaCenter divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" />
                        <input type="button" runat="server" id="btnProcesarCliente" value="Procesar" onclick="btnProcesarCliente_click();" class="buttonBBVA2" />
                        <%--                        <asp:Button runat="server" ID="btnAutorizar" Visible="false" Text="Autorizar" OnClientClick="cambiaVisibilidadDiv('divautoriza', true)" />
                        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="buttonSecBBVA2" OnClientClick="cambiaVisibilidadDiv('divcancela', true)" />--%>
                    </td>
                </tr>
            </table>
            <%-- BUG-PD-16 MAPH 07/03/2017 Validación de facturas necesarioas para procesamiento: Vehículo y por apoyo a la comercialización --%>
            <div id="divHiddenButton" style="visibility: collapse">
                <asp:Button runat="server" ID="btnProcesar" Text="Procesar" OnClick="btnProcesar_Click" />
                <asp:HiddenField runat="server" ID="hdnArrayIds" />
            </div>
        </div>

    </div>

</asp:Content>
