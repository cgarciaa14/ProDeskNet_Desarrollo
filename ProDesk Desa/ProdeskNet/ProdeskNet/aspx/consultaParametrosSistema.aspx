<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaParametrosSistema.aspx.vb" Inherits="consultaParametrosSistema" %>
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
                                <td colspan="2" style="width:70%;">Parametros Sistema</td>
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
                    <asp:TemplateField HeaderText="Id" ShowHeader="False" SortExpression="PDK_ID_PARAMETROS_SISTEMA">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catParId" CommandArgument='<%# Eval("PDK_ID_PARAMETROS_SISTEMA") %>'><%#Eval("PDK_ID_PARAMETROS_SISTEMA")%></asp:LinkButton>
                        </ItemTemplate>                                            
                    </asp:TemplateField>
                    <asp:BoundField DataField="PDK_PAR_SIS_ID_PADRE" HeaderText="Id Padre" 
                        SortExpression="PDK_PAR_SIS_ID_PADRE" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_PAR_SIS_PARAMETRO" HeaderText="Descripcion" 
                        SortExpression="PDK_PAR_SIS_PARAMETRO" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_PAR_SIS_VALOR_TEXTO" HeaderText="Valor Texto" 
                        SortExpression="PDK_PAR_SIS_VALOR_TEXTO" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_PAR_SIS_VALOR_FECHA" HeaderText="Valor Fecha" 
                        SortExpression="PDK_PAR_SIS_VALOR_FECHA" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_PAR_SIS_VALOR_NUMERO" HeaderText="Valor Numero" 
                        SortExpression="PDK_PAR_SIS_VALOR_NUMERO" ItemStyle-HorizontalAlign="Center"/>
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
