<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaAgencia.aspx.vb" Inherits="aspx_consultaAgencia" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>
<%--BBV-P-423: RQSOL-03: AVH: 10/11/2016 SE OCULTA BOTON AGREGAR--%>
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
                                <td colspan="2" style="width:40%;">Agencia</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>        
        <p></p>
        <div style ="position:absolute; top:10%; left:0%; width:100%;height:5%;">
            <label id ="lblTituloBusqueda" style="font-size: 8pt;font-family: Arial;color: #666666;">Busca Agencia:</label>
            <input type="text" class="resul" style=" width:80%;"    onkeypress ="ManejaCar('A',1,this.value,this);" id="search" />
        </div>
        <div style ="position:absolute; top:15%; left:0%; width:100%;height:5%;">
            <table>
                <tr>
                    <td style="width:5%;"><label id ="lblEmpresa" style="font-size: 8pt;font-family: Arial;color: #666666;">Empresa:</label></td>
                    <td style="width:20%"><asp:DropDownList ID="ddlempresa" Width="100%" CssClass="selectBBVA"    runat ="server"  AutoPostBack ="true"></asp:DropDownList>  </td>
                </tr>
            </table>
        </div>
        <div style ="position:absolute; top:20%; left:0%; width:100%; height:40%; overflow:auto;">
            <asp:GridView runat="server" id="grvConsulta" AutoGenerateColumns="False" CssClass="resulGrid" 
                HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowSorting="True">
                <%--AllowPaging="True" PageSize="15"--%> 
                <Columns>
                    <asp:TemplateField HeaderText="Id" ShowHeader="False" SortExpression="PDK_ID_DISTRIBUIDOR">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catDisId" CommandArgument='<%# Eval("PDK_ID_DISTRIBUIDOR") %>'><%#Eval("PDK_ID_DISTRIBUIDOR")%></asp:LinkButton>
                        </ItemTemplate>                                            
                    </asp:TemplateField>
                    <asp:BoundField DataField="PDK_DIST_NOMBRE" HeaderText="Agencia" SortExpression="PDK_DIST_NOMBRE" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_DIST_DISTRIBUIDOR" HeaderText="Distribuidor" SortExpression="PDK_DIST_NOMBRE" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_EMP_NOMBRE" HeaderText="Empresa" SortExpression="PDK_EMP_NOMBRE" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_DIST_CLAVE" HeaderText="Clave Distribuidor" SortExpression="PDK_DIST_CLAVE" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="PDK_DIST_STATUS" HeaderText="Status" SortExpression="PDK_DIST_STATUS" ItemStyle-HorizontalAlign="Center"/>
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
