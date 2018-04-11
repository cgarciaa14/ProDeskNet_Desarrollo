<%@ Page Title ="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master"   CodeFile="SeccionDatos.aspx.vb" Inherits="SeccionDatos" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath ="~/aspx/Home.Master"  %>


<asp:Content ID="Content1" runat ="server" ContentPlaceHolderID="cphPantallas" >

    <!--  BBV-P-423  RQSOL-02  gvargas   15/12/2016 Se agregó TextBox "txtTool" -->
    <!--  BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit-->
   
   <div class="divAdminCat">
     <div class="divFiltrosConsul">
       <table >
        <tr>
          <td class="tituloConsul">Sección de Datos</td>  
        </tr>
       </table>
     </div>
     <div class="divAdminCatCuerpo">
       <div style="position:absolute; top:0%; left:0%; width:40%;" >
         <table>
           <tr>
             <td class="campos" style ="width:33%">ID Seccion:</td>
             <td><asp:Label ID="lblCveSeccion" runat="server" SkinID="lblGeneral"></asp:Label>   </td>   
           </tr>
           <tr>
              <td class="campos" style="width:33%">ID Datos:</td>
              <td><asp:Label ID="lblCveDatos"  runat="server" SkinID="lblGeneral" ></asp:Label>  </td>   
           </tr>
           <tr>
             <td class="campos">* Nombre:</td>
             <td><asp:TextBox ID="txtnombre" runat="server" SkinID ="txtAlfaMayGde1"></asp:TextBox></td>  
           </tr>
           <tr>
             <td class ="campos">* Tipo Objeto:</td>
             <td><asp:DropDownList ID="cmbObjeto" runat="server" Width="195px"  CssClass="resul"></asp:DropDownList>  </td> 
           </tr>
           <tr>
             <td class="campos">* Campo:</td> 
             <td><asp:DropDownList ID="cmbCampo" runat ="server" width="195px" CssClass="resul"></asp:DropDownList>  </td> 
           </tr>
           <tr>
             <td class="campos">* Longuitud:</td>
             <td><asp:TextBox ID="txtlongu" runat="server" SkinID="txtNumerosGde" ></asp:TextBox>  </td>
           </tr>
           <tr>
             <td class="campos" >Muestra Pantalla:</td>
             <td><asp:TextBox ID="txtMostrar" runat="server" SkinID="txtGeneralGde"></asp:TextBox></td>
           </tr>
           <tr>
             <td class="campos" >ToolTip:</td>
             <td><asp:TextBox ID="txtTool" runat="server" SkinID="txtGeneralGde"></asp:TextBox></td>
           </tr>
           <tr>
             <td class="campos"> Activo:</td>
             <td><asp:CheckBox runat="server" CssClass="resul" ID="chkStatus" /></td>
           </tr>
           <tr>
             <td class="campos"></td>
              <td><asp:DropDownList ID="cmbLlave" runat ="server" CssClass="resul" AutoPostBack="true" Visible="false"    ></asp:DropDownList></td>
           </tr>
         </table>
       </div> 
       <div  style="position:absolute; top:50%; height:60%; left:0%; width:100%; ">
         <asp:GridView runat="server" id="grvConsulta" AutoGenerateColumns="False" 
                  HeaderStyle-CssClass="encabezados" AllowPaging="True" PageSize="10" 
                  Width="100%" PagerStyle-HorizontalAlign="Right" CssClass="resulGrid" 
                AllowSorting="True">
                <Columns>
                    <asp:TemplateField HeaderText="Id" ShowHeader="False" SortExpression="PDK_ID_SECCION_DATO">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catIDSeccDato" CommandArgument='<%# Eval("PDK_ID_SECCION_DATO") %>'><%# Eval("PDK_ID_SECCION_DATO")%></asp:LinkButton>
                        </ItemTemplate>                                            
                    </asp:TemplateField>
                    <asp:BoundField DataField="PDK_SEC_DAT_NOMBRE" HeaderText="Nombre" 
                        SortExpression="PDK_SEC_DAT_NOMBRE" />
                        <asp:BoundField DataField="PDK_TIP_OBJ_NOMBRE" HeaderText="Nombre Objeto" SortExpression="PDK_TIP_OBJ_NOMBRE" /> 
                        <asp:BoundField DataField ="PDK_SEC_DAT_LONGUITUD" HeaderText="Longuitud" SortExpression="PDK_SEC_DAT_LONGUITUD" /> 
                        <asp:BoundField DataField="PDK_PAR_SIS_PARAMETRO" HeaderText ="Status" SortExpression="PDK_PAR_SIS_PARAMETRO" />   
                </Columns>      
            <HeaderStyle CssClass="encabezados" ForeColor="White"></HeaderStyle>
            <PagerStyle HorizontalAlign="Right"></PagerStyle>
            </asp:GridView>
       
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
   
