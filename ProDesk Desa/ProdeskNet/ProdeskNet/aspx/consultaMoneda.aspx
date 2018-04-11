<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaMoneda.aspx.vb" Inherits="consultaMoneda" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<%--BUG-PD-309: 28/12/2017: DJUAREZ: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen.--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">

    <div class="divPantConsul">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width:70%;">Moneda</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>        
        <p></p>
        <div class="divCuerpoConsul">
            <asp:GridView runat="server" id="grvConsulta" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" Width="100%"
                HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowSorting="True">
                <Columns>
                    <asp:TemplateField HeaderText="Id" ShowHeader="False" SortExpression="PDK_ID_MONEDA">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catMonId" CommandArgument='<%# Eval("PDK_ID_MONEDA") %>'><%#Eval("PDK_ID_MONEDA")%></asp:LinkButton>
                        </ItemTemplate>                                            
                    </asp:TemplateField>
                    <asp:BoundField DataField="PDK_MON_NOMBRE" HeaderText="Nombre" 
                        SortExpression="PDK_MON_NOMBRE" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_MON_TIPO_CAMBIO" HeaderText="Tipo de Cambio" 
                        SortExpression="PDK_MON_TIPO_CAMBIO" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_PAR_SIS_PARAMETRO" HeaderText ="Status" 
                        SortExpression="PDK_PAR_SIS_PARAMETRO" ItemStyle-HorizontalAlign="Center"/>


                </Columns>      
                <PagerStyle HorizontalAlign="Right"></PagerStyle>
            </asp:GridView>
            <div class="divAdminCatPie">
                <table width="100%" style="height:100%;">
                    <tr>
                        <td align="center" valign="middle">
                            <asp:Button runat="server" id="btnAgregar" text="Agregar" CssClass="buttonBBVA2" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>    

</asp:Content>
