<%@ Page Title=""  Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master"   CodeFile="manejaTipoObjeto.aspx.vb" Inherits="manejaTipoObjeto" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"  %>--%>
<%@ MasterType VirtualPath ="~/aspx/Home.Master" %>

<%--BUG-PD-309: 28/12/2017: DJUAREZ: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen.--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="Content1" runat ="server" ContentPlaceHolderID ="cphPantallas" >
  <div class="divAdminCat">
    <div class="divFiltrosConsul">
      <table >
        <tr>
          <td class ="tituloConsul">Tipo de Objeto</td> 
        </tr>
      </table>
    </div>
    <div class ="divAdminCatCuerpo">
      <div style="position:absolute; top:0%; left:0%; width:40%;" >
        <table >
          <tr>
            <td style="width:33%"><label style="font-size: 8pt;font-family: Arial;color: #666666;">ID:</label></td>
            <td><asp:Label ID="lblID" runat="server" SkinID="lblGeneral" ></asp:Label>  </td>   
          </tr>
          <tr>
            <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">* Nombre:</label></td>
            <td><asp:TextBox ID="txtNombre" runat="server" SkinID ="txtAlfaMayGde" ></asp:TextBox> </td> 
          </tr>
          <tr>
            <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Nombre Codigo:</label></td> 
            <td><asp:TextBox ID="txtNomberCod" runat ="server" SkinID ="txtAlfaMayGde" ></asp:TextBox></td>
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
