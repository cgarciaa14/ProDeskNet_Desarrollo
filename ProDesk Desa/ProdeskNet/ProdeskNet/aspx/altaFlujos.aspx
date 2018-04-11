<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="altaFlujos.aspx.vb" Inherits="altaFlujos" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<%--BUG-PD-340: DJUAREZ: 16/01/2018: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen. --%>

<asp:Content ID="Content1" ContentPlaceHolderID ="cphPantallas" runat ="server" >
  <style>
     .labelStyle
     {
         font-size: 8pt;
         font-family: Arial;
         color: #666666;
     }
  </style>
  <div class ="divAdminCat">
    <div class ="divAdminCatTitulo">
      <table >
        <tr>
          <td class ="tituloConsul">Flujos</td> 
        </tr>
      </table>
     </div> 
     <div class ="divAdminCatCuerpo ">
       <div style ="position:absolute; top:0%; left:0%; width:70%;" >
         <table>
           <tr>
             <td ><label class="labelStyle">Id Flujo:</label></td>
             <td><asp:Label runat ="server" SkinID ="lblCampos" ID="lblIdFlujo"></asp:Label></td>
           </tr>
           <tr>
             <td ><label class="labelStyle">* Empresa:</label></td>
             <td><asp:DropDownList runat="server" ID="ddlEmpresa"  CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList> </td>
           </tr>

           <tr>
             <td ><label class="labelStyle">* Producto:</label></td>
             <td><asp:DropDownList runat="server" ID="ddlProducto"  CssClass="selectBBVA"></asp:DropDownList> </td>
           </tr>
           <tr>
             <td ><label class="labelStyle">* Nombre Flujo:</label></td> 
             <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" id="txtNombreFlujo"></asp:TextBox> </td>
           </tr>
           <tr>
             <td ><label class="labelStyle">* Orden:</label></td> 
             <td><asp:TextBox runat="server" SkinID="txtNumerosGde"  id="txtOrden"></asp:TextBox> </td>
           </tr>

           <tr>
             <td ><label class="labelStyle">* Personalidad Jurídica:</label></td>
             <td><asp:DropDownList runat="server" ID="ddlPersonalidadJuridica"  CssClass="selectBBVA"></asp:DropDownList> </td>
           </tr>
            <tr>
                <td ><label class="labelStyle">* Activo:</label></td>
                <td><asp:CheckBox runat="server" CssClass="resul"  id="chkStatus" /></td>
            </tr>
         </table>
       </div>          
     </div>
     <div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td align="center" valign="middle">
                        <asp:Button runat="server" id="btnRegresar" text="Regresar" CssClass="buttonBBVA2" />
                        <asp:Button runat="server" id="btnGuardar" text="Guardar" CssClass="buttonBBVA2" />
                    </td>
                </tr>
            </table>
        </div>
  </div> 
</asp:Content>

