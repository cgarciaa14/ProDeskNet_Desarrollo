<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaResultados.aspx.vb" Inherits="consultaResultados" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">


    <div class="divPantConsul">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width:70%;">Resultados</td>
                                <td style="width:30%;" align="right" valign="middle">
                                    <asp:Button runat="server" id="btnAgregar" text="Agregar" SkinID="btnGeneral" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>        
        <p></p>
        <div class="divCuerpoConsul">
            <asp:GridView runat="server" id="grvConsulta" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" CssClass="resulGrid" 
                AllowSorting="True">
                <Columns>
                    <asp:TemplateField HeaderText="Id" ShowHeader="False" SortExpression="PDK_ID_CAT_RESULTADO">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catResId" CommandArgument='<%# Eval("PDK_ID_CAT_RESULTADO") %>'><%#Eval("PDK_ID_CAT_RESULTADO")%></asp:LinkButton>
                        </ItemTemplate>                                            
                    </asp:TemplateField>
                    <asp:BoundField DataField="PDK_RES_NOMBRE" HeaderText="Nombre" 
                        SortExpression="PDK_RES_NOMBRE" />
                    <asp:BoundField DataField="PDK_PAR_SIS_PARAMETRO" HeaderText="Status" 
                        SortExpression="PDK_PAR_SIS_PARAMETRO" />
                </Columns>      
            <HeaderStyle CssClass="encabezados" ForeColor="White"></HeaderStyle>
            <PagerStyle HorizontalAlign="Right"></PagerStyle>
            </asp:GridView>
        </div>
    </div>    


</asp:Content>
