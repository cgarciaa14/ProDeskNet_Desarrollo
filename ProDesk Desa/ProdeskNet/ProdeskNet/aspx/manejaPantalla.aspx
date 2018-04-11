<%@ Page Title=""  Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master"   CodeFile="manejaPantalla.aspx.vb" Inherits="manejaPantalla" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"  %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="Content1" ContentPlaceHolderID ="cphPantallas" runat="server" >  
    <div class="divAdminCat">
   <div class="divFiltrosConsul">
     <table>
       <tr class="tituloConsul" >
        <td width="100%">
         <table width="100%">
         <tr>
         <td colspan="2" style ="width:70%;" >Pantalla</td> 
  
         </tr> 
         </table>
         </td>
        </tr>
     </table> 
   </div> 

   <div class="divAdminCatCuerpo">
   <div style="border-style:ridge; border-color:#808080; border-width:2px; ">
           <table  width="100%" >      
            
             <tr>
              <td class="campos" >Id pantalla:</td>
              <td><asp:Label ID="lblPantalla" runat="server" SkinID="lblCampos" ></asp:Label></td> 
            
           </tr>
           <tr>
             <td class="campos">Empresa:</td>
              <td><asp:DropDownList runat="server" ID="ddlEmpresa" CssClass="Text" SkinID="cmbGeneral" AutoPostBack="true" Width="200px"></asp:DropDownList></td> 
               <td class="campos">Producto:</td> 
              <td><asp:DropDownList ID="ddlProducto" CssClass="Text" runat="server" SkinID="cmbGeneral" AutoPostBack="true" Width="200px"></asp:DropDownList></td>
           </tr>
           <tr>              
              <td class="campos">Flujo:</td> 
              <td><asp:DropDownList runat="server" ID="ddlFlujo" CssClass="Text" SkinID="cmbGeneral" AutoPostBack="true" Width="200px"></asp:DropDownList></td>  
              <td class="campos">Proceso:</td>
              <td><asp:DropDownList runat="server" ID="ddlProceso" CssClass="Text" SkinID="cmbGeneral" AutoPostBack="true" Width="200px"></asp:DropDownList></td>
    
           </tr>
           <tr>
              <td class="campos">Tarea:</td>
              <td><asp:DropDownList runat ="server" ID="ddlTarea" CssClass="Text" SkinID="cmbGeneral" AutoPostBack="true" Width="200px"></asp:DropDownList></td>
              <td class="campos">Pantalla:</td>
              <td> 
                <asp:DropDownList ID="ddlDocumento" runat="server" CssClass="Text" SkinID="cmbGeneral" Width="200px" AutoPostBack ="true" >
                </asp:DropDownList> 
              </td>
           </tr>
           <tr>
               
              <td class="campos">Nombre:</td>
              <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" ID="txtNombre"   ></asp:TextBox></td>
              <td class="campos">aspx:</td>
              <td><asp:TextBox runat="server" SkinID ="txtAlfaMinGde" ID="txtaspx" Enabled="false"  ></asp:TextBox></td>
       
           </tr>
           <tr>
              <td class="campos">* Orden:</td> 
              <td><asp:TextBox runat="server" SkinID="txtNumerosGde"  id="txtOrden" ></asp:TextBox> </td>  
              <td class="campos" style="width: 86px">* Mostrar  
              <asp:CheckBox runat="server" CssClass="resul" id="chkMostrar" />  </td>
              <td class="campos">* Activo:
              <asp:CheckBox runat="server" CssClass="resul"  id="chkStatus" /></td>
              <td align="right" valign="middle">
              <asp:Button runat="server" id="btnGuardar" text="Guardar" SkinID="btnGeneral" />
             </td>        
           </tr>
        
           </table>   
   
     
     </div>
     <div id = "dvidpantalla" style="border-style:ridge; border-color:#808080; border-width:2px; height:271px; " runat="server" >
     <table style="width: 169px">
         <tr>
         <td class="campos">Seccion: </td>
         <td><asp:DropDownList runat="server" ID="ddlSeccion" CssClass="Text" SkinID="cmbGeneral" AutoPostBack="true"  ></asp:DropDownList>       </td> 
        </tr>
     </table>
     <table width="100%" >
      
       <tr>
         <td class="campos">Seccion de Datos:</td> 
         <td></td>    
         <td class="campos">Objeto de Pantalla:</td>              
       </tr>       
       <tr>
       <td style="width:45%;">
       <div  style=" height:220px; overflow:auto">
       <asp:GridView ID="grdSeccDat" runat="server" AutoGenerateColumns="False" 
              HeaderStyle-CssClass="encabezados"      
              Width="100%" PagerStyle-HorizontalAlign="Right" CssClass="resulGrid" 
              AllowSorting="True"  >
          <Columns>
             <asp:BoundField DataField="PDK_ID_SECCION_DATO" ItemStyle-CssClass="oculta" HeaderStyle-CssClass="oculta" HeaderText="ID" FooterStyle-CssClass="oculta"/>
             <asp:BoundField DataField="PDK_SEC_DAT_NOMBRE" ItemStyle-CssClass="resul" ItemStyle-Width="19%" HeaderText="Nombre"/>
             <asp:BoundField DataField="PDK_ID_TIPO_OBJETO" ItemStyle-CssClass="oculta" HeaderStyle-CssClass="oculta" FooterStyle-CssClass="oculta" />
             <asp:BoundField DataField="PDK_TIP_OBJ_NOMBRE_COD" ItemStyle-CssClass="resul" ItemStyle-Width="19%" HeaderText="Tipo"/>
             <asp:BoundField DataField="PDK_SEC_DAT_LONGUITUD" ItemStyle-CssClass="oculta" HeaderStyle-CssClass="oculta" FooterStyle-CssClass="oculta" ItemStyle-Width="10%" HeaderText="Longuitud"/>
             <asp:TemplateField ItemStyle-Width="10%">
               <HeaderTemplate>
                 <asp:CheckBox ID="chkTodos" runat="server"  Text="Todo"  OnCheckedChanged="chkTodos_CheckedChanged"  AutoPostBack="true"  />
               </HeaderTemplate>
               <ItemTemplate >
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
                             
                                                               
         </Columns>
        </asp:GridView> 
        </div>
        </td>
        <td align="center"   style="width:10%; "><table><tr><td><asp:Button runat="server" ID="btnGuardar1" text=">>" SkinID="btnGeneral"  /></td></tr><tr><td><asp:Button runat="server" ID="ButtElimivar" text="<<" SkinID="btnGeneral"  /> </td></tr></table></td>
       <td style="width:45%;">
       <div style="height:219px; overflow:auto;">
       <asp:GridView ID="grvObjetoPant"  runat="server" AutoGenerateColumns="False" 
              HeaderStyle-CssClass="encabezados" 
              Width="80%" PagerStyle-HorizontalAlign="Right" CssClass="resulGrid" 
              AllowSorting="True">
              <Columns>
                <asp:BoundField DataField="PDK_SEC_NOMBRE_TABLA" ItemStyle-CssClass="oculta" HeaderStyle-CssClass="oculta" FooterStyle-CssClass="oculta" ItemStyle-Width="41%" HeaderText="Nombre Tabla" />
                <asp:BoundField DataField="PDK_REL_PANT_OBJ_NOMBRE" ItemStyle-CssClass="resul" ItemStyle-Width="30%" HeaderText="Nombre Objeto" />
                <asp:BoundField DataField ="PDK_REL_PANT_OBJ_ORDEN" ItemStyle-CssClass="resul" ItemStyle-Width="9%" HeaderText="Orden" />                  
              </Columns> 
                    
       </asp:GridView>
       </div>
       </td> 
       </tr>
      </table>
    </div>

    <div id = "dvdocumentos" style="border-style:ridge; border-color:#808080; border-width:2px; height:271px;" runat = "server">
        <table id = "tbdocumentos" width = "100%">
            <tr>
                <td style = "width:20%">
                    Tipo de Persona:
                </td>
                <td style = "width:80%">
                    <asp:Label ID = "lblTpersona" runat = "server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan = "2">
                  <table width="90%">
                   <tr>
                   <td>
                   <div style="height:200px; overflow:auto">
                    <asp:GridView ID="gvDocumentos"  runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="encabezados" Width="100%" PagerStyle-HorizontalAlign="Right" CssClass="resulGrid" AllowSorting="True">
                      <Columns>                     
                        <asp:BoundField DataField="PDK_ID_DOCUMENTOS" ItemStyle-CssClass="oculta" HeaderStyle-CssClass="oculta" FooterStyle-CssClass="oculta" ItemStyle-Width="41%" HeaderText="Nombre Tabla" />
                        <asp:BoundField DataField="PDK_DOC_NOMBRE" ItemStyle-CssClass="resul" ItemStyle-Width="50%" HeaderText="Nombre Documento" />
                        <asp:TemplateField HeaderText="STATUS"  >
                          <ItemTemplate >
                          <asp:Label ID="lbloblig" runat="server" Text='<%# Eval("PDK_ID_PARAMETROS_SISTEMA") %>' Visible ="false"></asp:Label>   
                            <asp:DropDownList ID="ddlobliga" runat="server" CssClass="Text" Width="200px"   ></asp:DropDownList> 
                          </ItemTemplate>
                        </asp:TemplateField>
                     <%--   <asp:TemplateField>
                            <ItemTemplate>
                              <asp:CheckBox ID="chkSeccDta" runat="server"/>
                           </ItemTemplate>
                        </asp:TemplateField>--%>
                      </Columns>                      
                     </asp:GridView>
                     </div>
                     </td>
                     </tr>
                     </table>
                </td>
            </tr>
            <tr>
                <td colspan = "2" style = "text-align:right">
                    <asp:Button ID = "btnDocumentos" CssClass = "Text" Text = "Guardar" runat = "server"/>
                </td>
            </tr>
        </table>
    </div>
                
   </div>
      <div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" id="btnRegresar" text="Regresar" SkinID="btnGeneral" />
                     </td>
                </tr>
            </table>
        </div>   
 </div>
   <asp:HiddenField runat="server" ID="hdnIdRegistro" />
</asp:Content> 