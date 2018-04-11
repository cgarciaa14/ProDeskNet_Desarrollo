<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaDistribuidor.aspx.vb" Inherits="consultaDistribuidora" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<%--BUG-PD-309: 28/12/2017: DJUAREZ: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen.--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">
    <script language="javascript" type ="text/jscript">
        $(document).ready(function () {
            $('#search').keyup(function () {
                searchTable($(this).val(), $('[id$=grvConsulta]'));
            });
        });
    </script> 

    <div class="divPantConsul">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width:40%;">Distribuidor</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>        
        <p></p>
        <div style ="position:absolute; top:10%; left:0%; width:100%;height:5%;">
            <label id ="lblTituloBusqueda" style="font-size: 8pt;font-family: Arial;color: #666666;">Busca Distribuidor:</label>
            <input type="text" class="resul" style=" width:80%;"    onkeypress ="ManejaCar('A',1,this.value,this);" id="search" />
        </div>
        <div style ="position:absolute; top:15%; left:0%; width:100%;height:5%;">
            <table>
                <tr>
                    <td style="width:5%;" ><label id ="lblEmpresa" style="font-size: 8pt;font-family: Arial;color: #666666;">Empresa:</label></td>
                    <td style="width:20%"><asp:DropDownList ID="ddlempresa" Width="100%" CssClass="selectBBVA"    runat ="server"  AutoPostBack ="true"></asp:DropDownList>  </td>
                    <td style="width:5%;" ><label id ="lblPlaza" style="font-size: 8pt;font-family: Arial;color: #666666;">Plaza:</label></td>
                    <td style="width:20%"><asp:DropDownList ID="ddlPlaza" Width="100%" CssClass="selectBBVA"    runat ="server"  AutoPostBack ="true"></asp:DropDownList>  </td>
                </tr>
            </table>
        </div>
        <div style ="position:absolute; top:20%; left:0%; width:100%; height:40%; overflow:auto;">                                    
            <asp:GridView runat="server" id="grvConsulta" AutoGenerateColumns="False" Width="100%"
                HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowSorting="True">
                <%--AllowPaging="True" PageSize="15" --%>
                <Columns>
                    <asp:TemplateField HeaderText="Id" ShowHeader="False" SortExpression="PDK_ID_DIST_DISTRIBUIDOR">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catDisId" CommandArgument='<%# Eval("PDK_ID_DIST_DISTRIBUIDOR")%>'><%#Eval("PDK_ID_DIST_DISTRIBUIDOR")%></asp:LinkButton>
                        </ItemTemplate>                                            
                    </asp:TemplateField>                    
                    <asp:BoundField DataField="PDK_DIST_DISTRIBUIDOR" HeaderText="NOMBRE DISTRIBUIDOR" SortExpression="PDK_DIST_DISTRIBUIDOR" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_EMP_NOMBRE" HeaderText="EMPRESA" SortExpression="PDK_EMP_NOMBRE" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_STR_DIST_DISTRIBUIDOR_ACTIVO" HeaderText="STATUS" SortExpression="PDK_STR_DIST_DISTRIBUIDOR_ACTIVO" ItemStyle-HorizontalAlign="Center"/>                    
                </Columns>      
                <PagerStyle HorizontalAlign="Right"></PagerStyle>
            </asp:GridView>
        </div>
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

</asp:Content>
