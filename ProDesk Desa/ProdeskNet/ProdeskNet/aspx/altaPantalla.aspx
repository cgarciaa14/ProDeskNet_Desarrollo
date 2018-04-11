<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="altaPantalla.aspx.vb" Inherits="altaPantalla" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace ="AjaxControlToolkit" TagPrefix ="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID ="cphPantallas" runat="server" >
    <script language="javascript" type="text/javascript" >

    function ObtenerNombre(id) {
        var txtNombre
        txtNombre = document.getElementById(id).value;
        document.getElementById("ctl00_ctl00_cphCuerpo_cphPantallas_txtaspx").value = 'PANT_' + ReplaceAll(txtNombre, ' ', '_')+'.aspx';

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
    <div class="divFiltrosConsul">
      <table>
       <tr>
         <td class="tituloConsul">Pantalla</td>  
       </tr>
      </table>
    </div>
    <div class="divAdminCatCuerpo">
      <div style="position:absolute ; top:0%; left:0%; width:40%;" >
        <table>
         <tr>
           <td class="campos">Id Pantalla:</td> 
           <td><asp:Label runat="server" SkinID="lblCampos" ID="lblidPantalla"></asp:Label></td> 
         </tr>
         <tr>
           <td class="campos">Empresa:</td>
           <td><asp:DropDownList runat ="server" ID="ddlEmpresa" CssClass="Text" SkinID="cmbGeneral" AutoPostBack="true" Width="200px" ></asp:DropDownList></td>  
         </tr>
         <tr>
           <td class="campos">Producto:</td>
           <td><asp:DropDownList runat="server" ID="ddlProducto" CssClass="Text" SkinID="cmbGeneral" AutoPostBack="true" Width="200px"></asp:DropDownList></td>  
         </tr>
         <tr>
           <td class="campos">Flujo:</td>
           <td><asp:DropDownList runat="server" ID="ddlFlujo" CssClass="Text" SkinID="cmbGeneral" AutoPostBack="true" Width="200px"  ></asp:DropDownList></td>  
         </tr>
         <tr>
           <td class="campos">Proceso:</td>
           <td><asp:DropDownList runat="server" ID="ddlProceso" CssClass="Text" SkinID="cmbGeneral" AutoPostBack="true" Width="200px" ></asp:DropDownList>      </td>  
         </tr>
         <tr>
           <td class="campos">Tarea:</td>  
           <td><asp:DropDownList runat ="server" ID="ddlTarea" CssClass="Text" SkinID="cmbGeneral" AutoPostBack="true" Width="200px" ></asp:DropDownList>    </td>
         </tr>
         <tr>
            <td class="campos">Tipo Pantalla:</td>
            <td><asp:DropDownList ID="ddldocumento" runat="server" CssClass ="Text" SkinID="cmbGeneral" whidth="200px"></asp:DropDownList>  </td>
         </tr>
         <tr>
          <td class="campos">* Nombre:</td>
          <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" ID="txtNombre" onBlur="ObtenerNombre(id)"></asp:TextBox></td>  
         </tr>
         <tr>
           <td class="campos">* aspx:</td>
           <td><asp:TextBox runat="server" SkinID="txtAlfaMinGde" ID="txtaspx" Enabled="false"></asp:TextBox>       </td>
         </tr>
         <tr>
             <td class="campos">* Orden:</td> 
             <td><asp:TextBox runat="server" SkinID="txtNumerosGde"  id="txtOrden"></asp:TextBox> </td>
         </tr>
            <tr>
                <td class="campos">* Activo:</td>
                <td><asp:CheckBox runat="server" CssClass="resul"  id="chkStatus" /></td>
          </tr>
        </table> 
      </div>
      <div style="position:absolute; top:0%; left:35%; width:65; height:70%; display:none " >
        <table>
          <tr>
            <td class="campos">Seccion:</td>
            <td><asp:DropDownList runat="server" ID="ddlSeccion" CssClass="Text" SkinID="cmbGeneral" AutoPostBack="true" ></asp:DropDownList>       </td>  
          </tr>
                
        </table> 
      </div> 
      <div style ="position:absolute; top:12%; left:35%; width:100%; height:75%; display:none" >
      <table>
        <tr>
          <td class="campos">Seccion de Datos:</td> 
          <td>
          </td> 
          <td></td>
           <td><asp:Button runat="server" ID="btnGuardar1" text="Agregar" SkinID="btnGeneral"  /> </td>
        </tr> 
      </table> 
      <div class="divCuerpoConsultaPantalla" >  
        <asp:GridView ID="grdSeccDat" runat="server" AutoGenerateColumns="False" 
              HeaderStyle-CssClass="encabezados"  
              Width="100%" PagerStyle-HorizontalAlign="Right" CssClass="resulGrid" 
              AllowSorting="True" >
          <Columns>
             <asp:BoundField DataField="PDK_ID_SECCION_DATO" ItemStyle-CssClass="resul" ItemStyle-Width="5%" HeaderText="ID"/>
             <asp:BoundField DataField="PDK_SEC_DAT_NOMBRE" ItemStyle-CssClass="resul" ItemStyle-Width="19%" HeaderText="Nombre"/>
             <asp:BoundField DataField="PDK_ID_TIPO_OBJETO" ItemStyle-CssClass="oculta" HeaderStyle-CssClass="oculta" FooterStyle-CssClass="oculta" />
             <asp:BoundField DataField="PDK_TIP_OBJ_NOMBRE_COD" ItemStyle-CssClass="resul" ItemStyle-Width="19%" HeaderText="Tipo"/>
             <asp:BoundField DataField="PDK_SEC_DAT_LONGUITUD" ItemStyle-CssClass="resul" ItemStyle-Width="19%" HeaderText="Longuitud"/>
             <asp:TemplateField ItemStyle-Width="10%">
               <HeaderTemplate>
                 <asp:CheckBox ID="chkTodos" runat="server"  Text="Todo"  OnCheckedChanged="chkTodos_CheckedChanged"  AutoPostBack="true"  />
               </HeaderTemplate>
                <ItemStyle CssClass="itemCentrar" />
               <ItemTemplate>
                  <asp:CheckBox ID="chkSeccDta" runat="server" OnCheckedChanged="chkSeccDta_CheckedChanged" AutoPostBack="true"   />
               </ItemTemplate>
               <ItemStyle CssClass="itemCentrar" />
               </asp:TemplateField> 
               <asp:TemplateField HeaderText="Orden" ItemStyle-Width="10%">
                 <ItemTemplate>
                   <asp:Label ID="lblOrden" runat="server"></asp:Label>
                 </ItemTemplate>
                 <ItemStyle CssClass="itemIzquierda" />
                 </asp:TemplateField>
                 <asp:TemplateField ItemStyle-Width="10%">
                  <HeaderTemplate >
                   <asp:CheckBox ID="chkTodoMostrar" runat="server" Text="Mostrar" OnCheckedChanged="chkTodoMostrar_CheckedChanged"   AutoPostBack="true" />   
                  </HeaderTemplate>
                  <ItemStyle CssClass="itemCentrar" />
                   <ItemTemplate>
                  <asp:CheckBox ID="chkMostrar" runat="server"  AutoPostBack="true"   />
                 </ItemTemplate>
                 <ItemStyle CssClass="itemCentrar" />
                 </asp:TemplateField> 
                                                                 
         </Columns>
        </asp:GridView> 
         
     </div> 
          
     </div> 
     
           
    </div> 
    <div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" ID="btnRegresar" text="Regresar" SkinID="btnGeneral" />
                        <asp:Button runat="server" ID="btnGuardar" text="Guardar" SkinID="btnGeneral" />
                    </td>
                </tr>
            </table>
      </div>
  </div>

</asp:Content>