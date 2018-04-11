<%@ Page Title=""  Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master"   CodeFile="consultaTipoObjeto.aspx.vb" Inherits="consultaTipoObjeto" %>
<%@ MasterType VirtualPath ="~/aspx/Home.Master"  %>

<%--BUG-PD-309: 28/12/2017: DJUAREZ: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen.--%>

<asp:Content ID="Content1" runat ="server" ContentPlaceHolderID ="cphPantallas" >
   <div class ="divPantConsul">
     <div class ="divFiltrosConsul">
       <table class="tabFiltrosConsul">
         <tr class ="tituloConsul">
          <td width="100%">
            <table width ="100%">
              <tr>
                <td colspan ="2" style="width:70%;">Tipo de Objeto</td>  
              </tr>
            </table>
          </td>
         </tr> 
       </table>  
     </div>
      <div class="divCuerpoConsul" style="position:absolute; top:10%; left:0%; width:100%;">
                   <asp:GridView runat="server" id="grvConsulta" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" Width="100%"
                HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowSorting="True" >
                <Columns>
                    <asp:TemplateField HeaderText="Id" ShowHeader="False" SortExpression="PDK_ID_TIPO_OBJETO">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catIdObj" CommandArgument='<%# Eval("PDK_ID_TIPO_OBJETO") %>'><%# Eval("PDK_ID_TIPO_OBJETO")%></asp:LinkButton>
                        </ItemTemplate>                                            
                    </asp:TemplateField>                   
                        <asp:BoundField DataField="PDK_TIP_OBJ_NOMBRE" HeaderText="Nombre" SortExpression ="PDK_TIP_OBJ_NOMBRE" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField ="PDK_TIP_OBJ_NOMBRE_COD" HeaderText="Tipo Objeto" SortExpression ="PDK_TIP_OBJ_NOMBRE_COD" ItemStyle-HorizontalAlign="Center"/>
                        
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

