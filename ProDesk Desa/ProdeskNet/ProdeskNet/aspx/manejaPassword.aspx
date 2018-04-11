<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master"   CodeFile="manejaPassword.aspx.vb" Inherits="manejaPassword" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat ="server" >
  <div class="divAdminCat">
    <div class ="divFiltrosConsul">
      <table>
        <tr>
          <td class="tituloConsul">Cambio de Contraseña</td>  
        </tr>
      </table> 
    </div> 
    <div class="divAdminCatCuerpo">
      <div style="position:absolute; top:0%; left:0%; width:40%;" >
        <table>
          <tr>
            <td class="campos">* Usuario:</td>
            <td><asp:Label ID="lblUsu" runat ="server" SkinID="lblGeneral"></asp:Label>  </td>  
          </tr>
          <tr>
            <td class="campos">* Contraseña Anterior:</td>
            <td><asp:TextBox ID="txtPwd" runat="server" SkinID="txtAlfaMayGde1" TextMode ="Password"></asp:TextBox>     </td>  
          </tr>
          <tr>
            <td class="campos">* Nueva Contraseña:</td>
            <td><asp:TextBox ID="txtNvaPwd" runat="server" SkinID ="txtAlfaMayGde1" TextMode ="Password"></asp:TextBox>  </td>  
          </tr>
          <tr>
            <td class ="campos">* Confirma Nueva Contraseña:</td>
            <td><asp:TextBox ID="txtCNvaPwd" runat="server" SkinID ="txtAlfaMayGde1" TextMode ="Password"></asp:TextBox>   </td>
          </tr>
        </table> 
      </div>     
    </div> 
       <div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" id="btnRegresar" text="Regresar" SkinID="btnGeneral" />
                        <asp:Button runat="server" id="btnGuardar" text="Guardar" SkinID="btnGeneral" />
                    </td>
                </tr>
            </table>
        </div> 
  </div> 
  <asp:HiddenField runat="server" ID="hdnIdRegistro" />
</asp:Content> 