<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="altaProducto.aspx.vb" Inherits="altaProducto" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace ="AjaxControlToolkit" TagPrefix ="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID ="cphPantallas" runat ="server" >
  <div class ="divAdminCat">
    <div class ="divAdminCatTitulo">
      <table >
        <tr>
          <td class ="tituloConsul">Producto</td> 
        </tr>
      </table>
     </div> 
     <div class ="divAdminCatCuerpo ">
       <div style ="position:absolute; top:0%; left:0%; width:70%;" >
         <table>
           <tr>
             <td class ="campos ">Id Producto:</td>
             <td><asp:Label runat ="server" SkinID ="lblCampos" ID="lblCveProd"></asp:Label></td>
           </tr>
           <tr>
             <td class="campos">*Nombre Producto:</td> 
             <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" id="txtNombreProd"></asp:TextBox> </td>
           </tr>
           <tr>
             <td class ="campos" >Empresa:</td>
             <td><asp:DropDownList runat="server" ID="cmbEmpresa"  class="Text" SkinID="cmbGeneral"></asp:DropDownList> </td>
           </tr>
           <tr>
             <td class="campos">Moneda:</td>
             <td><asp:DropDownList runat="server" ID="cmbMoneda"  class="Text" SkinID="cmbGeneral"></asp:DropDownList> </td> 
           </tr>
           <tr>
              <td class="campos">Activo:</td>
              <td><asp:CheckBox runat="server" ID="chkActivo" SkinID="cmbGeneral" /></td>         
                        
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
</asp:Content>

