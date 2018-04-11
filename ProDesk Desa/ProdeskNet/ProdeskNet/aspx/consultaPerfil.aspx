<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaPerfil.aspx.vb" Inherits="consultaPerfil" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">

    <%--BBV-P-412:AVH:18/07/2016 RQ B: SE OCULTA EL BOTON AGREGAR YA QUE EL ALTA SE REALIZA DESDE PROCOTIZA--%>
    <%--BUG-PD-309: 28/12/2017: DJUAREZ: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen.--%>
    <div class="divPantConsul">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width:70%;">Perfil</td>
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
                    <asp:TemplateField HeaderText="Id" ShowHeader="False" SortExpression="PDK_ID_PERFIL">
                        <ItemTemplate>
                            <div style="text-align:center">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catPerfilId" CommandArgument='<%# Eval("PDK_ID_PERFIL") %>'><%#Eval("PDK_ID_PERFIL")%></asp:LinkButton>
                            </div>
                        </ItemTemplate>                                            
                    </asp:TemplateField>
                    <asp:BoundField DataField="PDK_PER_NOMBRE" HeaderText="Nombre" 
                        SortExpression="PDK_MON_NOMBRE" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_PAR_STATUS" HeaderText="Status" 
                        SortExpression="PDK_PAR_STATUS" ItemStyle-HorizontalAlign="Center"/>
                </Columns>      
            <PagerStyle HorizontalAlign="Right"></PagerStyle>
            </asp:GridView>
        </div>
        <div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td align="center" valign="middle">
                        <asp:Button runat="server" id="btnAgregar" text="Agregar" CssClass="buttonBBVA2" Visible="false" />
                    </td>
                </tr>
            </table>
        </div>
    </div>    

</asp:Content>
