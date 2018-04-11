<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaBurodeCredito.aspx.vb" Inherits="consultaBuroCredito" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">
   <div class ="divAdminCat">
    <div class ="divFiltrosConsul">
      <table class="tabFiltrosConsul">
        <tr class="tituloConsul">
          <td class ="tituloConsul">Buro de Credito</td> 
        </tr>
      </table>
     </div> 
     <div class ="divAdminCatCuerpo ">
       <div style ="position:absolute; top:0%; left:0%; width:70%;" >
         <table>
           <tr>
             <td class ="campos ">Id Configuracion:</td>
             <td><asp:Label runat ="server" SkinID ="lblCampos" ID="lblIdConfiguracion"></asp:Label></td>
           </tr>
           <tr>
             <td class="campos">* PF Servidor:</td> 
             <td><asp:TextBox runat="server" SkinID="txtGeneralGde"  id="txtPFServidor"></asp:TextBox> </td>
           </tr>
           <tr>
             <td class="campos">* PF Puerto:</td> 
             <td><asp:TextBox runat="server" SkinID="txtGeneralGde" id="txtPFPuerto"></asp:TextBox> </td>
           </tr>
           <tr>
             <td class="campos">* PF Usuario:</td> 
             <td><asp:TextBox runat="server" SkinID="txtGeneralGde" id="txtPFUsuario"></asp:TextBox> </td>
           </tr>
           <tr>
             <td class="campos">* PF Contraseña:</td> 
             <td><asp:TextBox runat="server" SkinID="txtGeneralGde" id="txtPFContrasena"></asp:TextBox> </td>
           </tr>
           <tr>
             <td class="campos">* PM Servidor:</td> 
             <td><asp:TextBox runat="server" SkinID="txtGeneralGde" id="txtPMServidor"></asp:TextBox> </td>
           </tr>
           <tr>
             <td class="campos">* PM Puerto:</td> 
             <td><asp:TextBox runat="server" SkinID="txtGeneralGde" id="txtPMPuerto"></asp:TextBox> </td>
           </tr>
           <tr>
             <td class="campos">* PM Usuario:</td> 
             <td><asp:TextBox runat="server" SkinID="txtGeneralGde" id="txtPMUsuario"></asp:TextBox> </td>
           </tr>
           <tr>
             <td class="campos">* PM Contraseña:</td> 
             <td><asp:TextBox runat="server" SkinID="txtGeneralGde" id="txtPMContrasena"></asp:TextBox> </td>
           </tr>
           <tr>
             <td class="campos">* Frecuencia:</td> 
             <td><asp:TextBox runat="server" SkinID="txtGeneralGde" id="txtFrecuencia"></asp:TextBox> </td>
           </tr>
         </table>
       </div>          
     </div>
     <div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" id="btnGuardar" text="Guardar" SkinID="btnGeneral" />
                    </td>
                </tr>
            </table>
        </div>
  </div> 
  

</asp:Content>
