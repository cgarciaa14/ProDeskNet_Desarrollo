<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile ="~/aspx/Home.Master" CodeFile="consultaProducto.aspx.vb" Inherits="consultaProducto" %>
<%@ MasterType VirtualPath ="~/aspx/Home.Master" %>

<%--BUG-PD-309: 28/12/2017: DJUAREZ: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen.--%>

<asp:Content ID="Content1" ContentPlaceHolderID ="cphPantallas" runat ="server" >
  <div class ="divPantConsul">
    <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td colspan="2" style ="width:20%;">Producto</td>
                <td style = "width:10%; text-align:right">
                    <asp:Button runat="server" ID="btnBuscar" Text="Buscar" SkinID ="btnGeneral" Visible="false"   />                 
                </td> 
                </tr>
            </table>
        </div>

    <div class ="divCuerpoConsul">
      <table width="100%">
          <tr class="resulbbvaCenter">
              <td  >
                  <asp:Label runat="server" ID="lblCompany" Text="Empresa: "></asp:Label>                    
              </td>              
              <td >
                  <asp:DropDownList runat="server" ID="cmbEmpresa" AutoPostBack="true" CssClass="selectBBVA" Width = "250px">
                  </asp:DropDownList>
              </td>
          </tr>
      </table>
      <table width = "100%">        
                
            <tr>
                <td>
                  
                        <asp:GridView runat ="server"  ID="grvConsulta" AutoGenerateColumns ="false" AllowPaging ="true" PageSize ="15" Width="100%"
                         HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowSorting ="true" >
                        <Columns >
                         <asp:TemplateField HeaderText ="Id" ShowHeader ="false" SortExpression ="PDK_ID_PRODUCTOS">
                           <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName ="catIdProd" CommandArgument='<%# Eval("PDK_ID_PRODUCTOS") %>'><%# Eval("PDK_ID_PRODUCTOS") %></asp:LinkButton> 
                           </ItemTemplate> 
                         </asp:TemplateField>
                            <asp:BoundField DataField ="PDK_PROD_NOMBRE" HeaderText ="Producto" SortExpression ="PDK_PROD_NOMBRE" ItemStyle-HorizontalAlign="Center"/>
                           <asp:BoundField DataField="PDK_EMP_NOMBRE" HeaderText ="Empresa" SortExpression ="PDK_EMP_NOMBRE" ItemStyle-HorizontalAlign="Center"/>
                           <asp:BoundField DataField="PDK_MON_NOMBRE" HeaderText ="Moneda" SortExpression="PDK_MON_NOMBRE" ItemStyle-HorizontalAlign="Center"/>
                           <asp:BoundField DataField="PDK_PAR_SIS_PARAMETRO" HeaderText ="Status" SortExpression="PDK_PAR_SIS_PARAMETRO" ItemStyle-HorizontalAlign="Center"/>
                         </Columns> 
                         <PagerStyle HorizontalAlign="Right"></PagerStyle>
           
                        </asp:GridView>
                   
                </td>
            </tr>
      </table> 
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
  </div>

</asp:Content>