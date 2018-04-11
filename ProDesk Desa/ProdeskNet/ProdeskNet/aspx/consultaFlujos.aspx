<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaFlujos.aspx.vb" Inherits="consultaFlujos" %>
<%@ MasterType VirtualPath ="~/aspx/Home.Master" %>

<%--BUG-PD-340: DJUAREZ: 16/01/2018: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen. --%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">

    <script>
        function postBackByObject() {
            var o = window.event.srcElement;
            if (o.tagName == 'INPUT' && o.type == 'checkbox') {
                __doPostBack('<%= btnBuscar.ClientID %>', '');
            }
        }
    </script>
    
    <div class ="divPantConsul">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td colspan="2" style="width:30%;">Flujos</td>
                </tr>
            </table>
        </div>
        <div class="divCuerpoConsul">
            <table width = "100%">                           
                <tr>
                        <td style="width:5%;"><label id ="lblEmpresa" style="font-size: 8pt;font-family: Arial;color: #666666;">Empresa:</label></td>
                        <td style="width:20%;" ><asp:DropDownList runat="server" Width="100%"   ID="ddlEmpresa" CssClass="selectBBVA" AutoPostBack="true" ></asp:DropDownList>    </td>
                        <td style="width:5%;"><label id ="lblProducto" style="font-size: 8pt;font-family: Arial;color: #666666;">Producto:</label></td>
                        <td style="width:20%;" ><asp:DropDownList runat="server" ID="ddlProducto" Width="100%"  CssClass="selectBBVA" Autopostback="True"></asp:DropDownList></td>
                        <td style="width:20%;" align="right" valign="middle">
                            <asp:Button runat="server" id="btnBuscar" text="Buscar" CssClass="buttonBBVA2" visible="false" />
                        </td>
                    </tr>
                <tr>
                    <asp:GridView runat="server" id="grvConsulta" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" Width="100%"
                HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowSorting="True">
                <Columns>
                    <asp:TemplateField HeaderText="Id" ShowHeader="False" SortExpression="PDK_ID_FLUJOS">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catIdFlujos" CommandArgument='<%# Eval("PDK_ID_FLUJOS") %>'><%#Eval("PDK_ID_FLUJOS")%></asp:LinkButton>
                        </ItemTemplate>                                            
                    </asp:TemplateField>
                    <asp:BoundField DataField="PDK_FLU_NOMBRE" HeaderText="Flujo" 
                        SortExpression="PDK_FLU_NOMBRE" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_PROD_NOMBRE" HeaderText="Producto" 
                        SortExpression="PDK_PROD_NOMBRE" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_FLU_ORDEN" HeaderText="Orden" 
                        SortExpression="PDK_FLU_ORDEN" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_PER_NOMBRE" HeaderText="Personalidad Juridica" 
                        SortExpression="PDK_PER_NOMBRE" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_PAR_SIS_PARAMETRO" HeaderText="Status" 
                        SortExpression="PDK_PAR_SIS_PARAMETRO" ItemStyle-HorizontalAlign="Center"/>
                </Columns>      
            <PagerStyle HorizontalAlign="Right"></PagerStyle>
            </asp:GridView>
                </tr>
            </table>                          
    </div>
        <div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td align="center" valign="middle">
                        <asp:Button runat ="server" ID="btnAgregar" Text ="Agregar" CssClass="buttonBBVA2" /> 
                    </td>
                </tr>
            </table>
        </div>    
    </div>

</asp:Content>
