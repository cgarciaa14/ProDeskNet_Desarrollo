<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="manejaProducto.aspx.vb" Inherits="manejaProducto" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"%>--%>
<%@ MasterType VirtualPath ="~/aspx/Home.Master" %>

<%--BBV-P-423: RQSOL-03: AVH: 10/11/2016 SE AGREGA CAMPO DEFAULT--%>
<%--BUG-PD-309: 28/12/2017: DJUAREZ: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen.--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="Content1" ContentPlaceHolderID ="cphPantallas" runat ="server" >
  <div class="divAdminCat">
    <div class ="divFiltrosConsul">
      <table>
        <tr>
          <td class ="tituloConsul">Producto</td> 
        </tr>
      </table>
    </div> 
    <div class ="divAdminCatCuerpo">
        <div style="position:absolute; top:0%; left:0%; width:40%;" >
          <table>
            <tr>
              <td style="width:33%" ><label style="font-size: 8pt;font-family: Arial;color: #666666;">ID:</label></td>
              <td><asp:Label runat="server" SkinID="lblGeneral" ID="lblID"></asp:Label> </td>     
            </tr>
            <tr>
              <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">* Nombre:</label></td>
              <td><asp:TextBox runat="server" SkinID ="txtAlfaMayGde" ID="txtNombreProd"></asp:TextBox></td> 
            </tr>
            <tr>
              <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Empresa:</label></td>
              <td><asp:DropDownList runat="server" id="cmbEmpresa" CssClass="selectBBVA"></asp:DropDownList></td>
            </tr>
            <tr>
              <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Moneda:</label></td>
              <td><asp:DropDownList runat="server" ID="cmbMoneda" CssClass="selectBBVA"></asp:DropDownList></td>
            </tr>
            <tr>
             <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Activo:</label></td>
             <td><asp:CheckBox runat="server" CssClass="resul" ID="chkStatus" /></td>
           </tr>
            <tr>
                <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Default:</label></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkDefault"/>
                </td>
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
    <asp:HiddenField runat="server" ID="hdnIdRegistro" />
</asp:Content>

