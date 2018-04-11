<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaMenu.aspx.vb" Inherits="consultaMenu" %>
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
                                <td colspan="2" style="width:70%;">Menu</td>
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
                    <asp:TemplateField HeaderText="Id" ShowHeader="False" SortExpression="PDK_ID_MENU">
                        <ItemTemplate>
                            <div style="text-align:center">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catMenId" CommandArgument='<%# Eval("PDK_ID_MENU") %>'><%#Eval("PDK_ID_MENU")%></asp:LinkButton>
                            </div>
                        </ItemTemplate>                                            
                    </asp:TemplateField>
                    <asp:BoundField DataField="PDK_MEN_DESCRIPCION" HeaderText="Descripcion" 
                        SortExpression="PDK_MEN_DESCRIPCION" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_MEN_LINK" HeaderText="Liga" 
                        SortExpression="PDK_MEN_LINK" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_PAR_SIS_PARAMETRO" HeaderText="Tipo" 
                        SortExpression="PDK_PAR_SIS_PARAMETRO" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_PAD_DESCRIPCION" HeaderText="Padre" 
                        SortExpression="PDK_PAD_DESCRIPCION" ItemStyle-HorizontalAlign="Center"/>
                </Columns>      
            <PagerStyle HorizontalAlign="Right"></PagerStyle>
            </asp:GridView>
        </div>
        <div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td style="width:30%;" align="center" valign="middle">
                        <asp:Button runat="server" id="btnAgregar" text="Agregar" CssClass="buttonBBVA2" />
                    </td>
                </tr>
            </table>
        </div>
    </div>    

</asp:Content>
