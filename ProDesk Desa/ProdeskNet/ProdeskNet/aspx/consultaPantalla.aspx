<%@ Page Title="" Language="vb" AutoEventWireup="false"  MasterPageFile="~/aspx/Home.Master" CodeFile="consultaPantalla.aspx.vb" Inherits="consultaPantalla" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat ="server" >
  <div class ="divPantConsul">

  <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td colspan ="2" style="width:15%;" >Pantalla</td>
                   <td class="campos" style="width:5%;">Empresa:</td>
               <td style="width:20%;">
                    <asp:DropDownList runat="server" ID="ddlEmpresa" CssClass="Text" Width="100%"  AutoPostBack="true"></asp:DropDownList>
               </td>
               <td class ="campos" style="width:5%;">Producto:</td>
               <td style="width:20%;">
                    <asp:DropDownList runat="server" ID="ddlProducto" CssClass="Text" Width="100%" AutoPostBack="true"></asp:DropDownList>
               </td>
               <td class ="campos" style="width:5%;">Flujo:</td>
               <td style="width:20%;">
                    <asp:DropDownList runat="server" ID="ddlFlujo" CssClass="Text" Width="100%" AutoPostBack="true"></asp:DropDownList>
               </td>
               <td style="width:25%; text-align:right;   ">
                    <asp:Button runat="server" ID="btnAgregar" Text="Agregar" SkinID="btnGeneral" />
               </td>  
                </tr>
            </table>
        </div>

   <div class="divCuerpoConsul">
     <table width="100%" >       
            
              <tr>
                 <td >
                    <asp:GridView runat="server" id="grvConsulta" AutoGenerateColumns="False" 
                              HeaderStyle-CssClass="encabezados" AllowPaging="True" PageSize="15" 
                              PagerStyle-HorizontalAlign="Right" CssClass="resulGrid" 
                            AllowSorting="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Id" ShowHeader="False" SortExpression="PDK_ID_PANTALLAS">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catPantalla" CommandArgument='<%# Eval("PDK_ID_PANTALLAS") %>'><%# Eval("PDK_ID_PANTALLAS")%></asp:LinkButton>
                                    </ItemTemplate>                                            
                                </asp:TemplateField>
                                <asp:BoundField DataField="PDK_PANT_NOMBRE" HeaderText="Nombre" SortExpression="PDK_PANT_NOMBRE" />
                                <asp:BoundField DataField="PDK_PANT_LINK" HeaderText="aspx"  SortExpression="PDK_PANT_LINK" />
                                <asp:BoundField DataField ="PDK_PROC_NOMBRE" HeaderText="Proceso" SortExpression="PDK_PROC_NOMBRE" />
                                <asp:BoundField DataField ="PDK_TAR_NOMBRE" HeaderText="Tarea" SortExpression ="PDK_TAR_NOMBRE" />
                                <asp:BoundField DataField ="PDK_PANT_ORDEN" HeaderText="Orden" SortExpression="PDK_PANT_ORDEN" />
                                <asp:BoundField DataField="PDK_PAR_SIS_PARAMETRO" HeaderText ="Status" SortExpression ="PDK_PAR_SIS_PARAMETRO" />
                                <asp:TemplateField HeaderText ="Crear" SortExpression ="false" >
                                   <ItemTemplate>
                                     <asp:Button ID="cmbCrear" runat="server" Text="P" CssClass="resul"  CommandName="catIdPantalla" CommandArgument='<%# Eval("PDK_ID_PANTALLAS") %>' />     
                                   </ItemTemplate>
                                </asp:TemplateField>
                         </Columns>      
                        <HeaderStyle CssClass="encabezados" ForeColor="White"></HeaderStyle>
                        <PagerStyle HorizontalAlign="Right"></PagerStyle>
                        </asp:GridView>
                 </td>            
              </tr>       
            </table>  
        </div>  
  </div>   
</asp:Content>