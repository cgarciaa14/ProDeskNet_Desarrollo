<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaSeccion.aspx.vb" Inherits="consultaSeccion" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPantallas">
<script language="javascript" type="text/javascript" >
    function rtbDatos(f, id) {
            
        var padre = document.getElementById(f)
        var RadioButton = document.getElementById(id)
        var strRuta;

        if (RadioButton) { RadioButton.checked = true; }
        strRuta = objLink.title;
        strRuta = './altaSeccionDatos.aspx?&idSeccion=' + padre;

    }

</script>    
  <div class="divPantConsul">
    <div class="divFiltrosConsul">
      <table class ="tabFiltrosConsul">
        <tr class="tituloConsul">
          <td width="100%">
            <table width="100%">
              <tr>
                <td colspan ="2" style ="width:70%;">Sección</td>
                <td style="width:30%;" align="right" valign ="middle">
                   <asp:Button runat="server" id="btnAgregar" text="Agregar" SkinID="btnGeneral" />
                </td>   
              </tr>              
            </table>
          </td> 
        </tr>
       </table>
    </div>
     <div class="divCuerpoConsul">
            <asp:GridView runat="server" id="grvConsulta" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" CssClass="resulGrid" 
                AllowSorting="True" >
                <Columns>
                
                    <asp:TemplateField HeaderText="Id" ShowHeader="False" SortExpression="PDK_ID_SECCION" >
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catIdSeccio" CommandArgument='<%# Eval("PDK_ID_SECCION") %>'><%# Eval("PDK_ID_SECCION")%></asp:LinkButton>
                        </ItemTemplate>                                            
                    </asp:TemplateField>
                    <asp:BoundField DataField="PDK_SEC_NOMBRE" HeaderText="Nombre" SortExpression="PDK_SEC_NOMBRE" />
                        <asp:BoundField DataField="PDK_SEC_NOMBRE_TABLA" HeaderText="Nombre Tabla" SortExpression="PDK_SEC_NOMBRE_TABLA" />
                        <asp:BoundField DataField="PDK_SEC_CREACION"  ItemStyle-CssClass="oculta" HeaderStyle-CssClass="oculta" FooterStyle-CssClass="oculta" />                 
                     <asp:TemplateField HeaderText="Dato"  SortExpression ="PDK_SEC_CREACION" >
                        <ItemTemplate>
                          <asp:Button ID="cmbLlenar" runat="server" Text="D" CssClass="resul"   CommandName="catidSeccDatos" CommandArgument='<%# Eval("PDK_ID_SECCION") %>' />  
                        </ItemTemplate> 
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText ="Crear" SortExpression ="false" >
                       <ItemTemplate>
                         <asp:Button ID="cmbCrear" runat="server" Text="T" CssClass="resul"  CommandName="catIdSeccTable" CommandArgument='<%# Eval("PDK_ID_SECCION") %>' />     
                       </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Eliminar" SortExpression ="false">
                        <ItemTemplate>
                          <asp:Button ID="cmbElimina" runat="server" Text ="T" CssClass="resul" CommandName="catIdSeccElimina" CommandArgument = '<%# Eval("PDK_ID_SECCION") %>' />  
                        </ItemTemplate>                      
                     </asp:TemplateField>
                </Columns>      
            <HeaderStyle CssClass="encabezados" ForeColor="White"></HeaderStyle>
            <PagerStyle HorizontalAlign="Right"></PagerStyle>
            </asp:GridView>
        </div> 
  </div>

</asp:Content> 