<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="altaSeccion.aspx.vb" Inherits="altaSeccion" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"  %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID ="cphPantallas" runat ="server" >
<script language="javascript" type="text/javascript">
    function ObtenerNombre(id) {
        var txtNombreTab
        txtNombreTab = document.getElementById(id).value;
        document.getElementById("ctl00_ctl00_cphCuerpo_cphPantallas_txtNombreTab").value = 'PDK_TAB_' + ReplaceAll(txtNombreTab,' ', '_');

    }

    function ReplaceAll(text, busca, remplaza) {
        var idx = text.toString().indexOf(busca);
        while (idx != -1) {
            text = text.toString().replace(busca, remplaza);
            idx = text.toString().indexOf(busca, idx);
        }
        return text;
    }

</script>   

  <div class="divAdminCat">
    <div class ="divAdminCatTitulo">
      <table >
        <tr>
          <td class="tituloConsul">Sección</td>
        </tr>
      </table>
    </div>
    <div class ="divAdminCatCuerpo">
      <div style ="position:absolute; top:0%; left:0%; width:70%;" >
        <table>
          <tr>
            <td class="campos">ID: </td>
            <td><asp:Label ID="lblCveSeccion" runat="server" SkinID="lblGeneral"></asp:Label> </td>
          </tr>
          <tr>
             <td class ="campos">* Nombre:</td>
             <td><asp:TextBox ID="txtNombre" runat="server" SkinID="txtAlfaMayGde" onBlur="ObtenerNombre(id)"></asp:TextBox>     </td> 
          </tr>
          <tr>
            <td class="campos">* Nombre Tabla:</td> 
            <td><asp:TextBox ID="txtNombreTab" runat="server" SkinID ="txtCustMayGde" Enabled="FALSE" ></asp:TextBox> </td>
          </tr>
          <tr>
            <td class="campos">* Mostrar Pantalla:</td>
            <td><asp:TextBox ID="txtmostraPantalla" runat="server" SkinID="txtGeneralGde" ></asp:TextBox>  </td> 
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