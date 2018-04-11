<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="manejaFlujos.aspx.vb" Inherits="manejaFlujos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %> 

<%--BUG-PD-340: DJUAREZ: 16/01/2018: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen. --%>
<%--BUG-PD-355: DJUAREZ: 06/02/2018: Se modifican los redirect para que las pantallas no pierdan estilos. --%>

<asp:Content ID="Content1" ContentPlaceHolderID ="cphPantallas" runat ="server" >
    <style>
        .labelStyle
        {
            font-size: 8pt;
            font-family: Arial;
            color: #666666;
        }
    </style>
  <div class="divAdminCat">
    <div class ="divFiltrosConsul">
      <table>
        <tr>
          <td class ="tituloConsul">Flujo</td> 
        </tr>
      </table>
    </div> 
    <div class ="divAdminCatCuerpo">
        <div style="position:absolute; top:0%; left:0%; width:100%; border-style:ridge; border-color:#808080; border-width:2px;" >
          <table>
           <tr>
             <td style="width:100px"><label class="labelStyle">Id Flujo:</label></td>
             <td><asp:Label runat ="server" class="labelStyle" ID="lblIdFlujo"></asp:Label></td>
           </tr>
           <tr>
             <td style="width:100px" ><label class="labelStyle">*Empresa:&nbsp;</label></td>
             <td><asp:DropDownList runat="server" ID="ddlEmpresa" CssClass="selectBBVA" AutoPostBack="true" Width="200px"></asp:DropDownList> </td>
             <td ><label class="labelStyle">*Producto:</label></td>
             <td><asp:DropDownList runat="server" ID="ddlProducto" CssClass="selectBBVA" Width="200px"></asp:DropDownList> </td>
             <td ><label class="labelStyle">* Personalidad Jurídica:</label></td>
             <td><asp:DropDownList runat="server" ID="ddlPersonalidadJuridica" CssClass="selectBBVA" Width="200px"></asp:DropDownList> </td>
           </tr>
         
           <tr>
           <td ><label class="labelStyle">*Nombre Flujo:</label></td> 
             <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" id="txtNombreFlujo"></asp:TextBox> </td>
             <td ><label class="labelStyle">* Orden:</label></td> 
             <td><asp:TextBox runat="server" SkinID="txtNumerosGde"  id="txtOrden"></asp:TextBox> </td>
             
             <td></td>
             <td ><label class="labelStyle">* Activo:</label>
             <asp:CheckBox runat="server" CssClass="resul"  id="chkStatus" /></td>
           </tr>
                    
          </table>
        </div>   
        <div>
            <cc1:ModalPopupExtender ID="mpeAltaProcesos" 
                runat="server" 
                TargetControlID="BtnProcesos" 
                PopupControlID="popAltaProcesos"
                CancelControlID="BtnCancel" 
                BackgroundCssClass="modalBackground"/>  
            <asp:Panel id="popAltaProcesos" runat="server" style="display: none; border: 1px solid #ffffff; padding-top:20px; height:30%;"
                BackColor="#F2F2F2" 
                width="35%" 
                Height="25%">
                <table width="100%">
                    <tr>
                        <td ><label class="labelStyle">Procesos:</label></td>
                    </tr>
                   <tr>
                     <td ><label class="labelStyle">* Nombre Proceso:</label></td> 
                     <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" id="txtProcesoNombre"></asp:TextBox> </td>
                   </tr>
                   <tr>
                     <td ><label class="labelStyle">* Orden:</label></td> 
                     <td><asp:TextBox runat="server" SkinID="txtNumerosGde" id="txtOrdenProc"></asp:TextBox> </td>
                   </tr>
                   <tr>
                     <td ><label class="labelStyle">* Proceso Padre:</label></td> 
                     <td><asp:DropDownList ID="ddlProcesoPadre" runat="server" CssClass="selectBBVA" Width="200px"></asp:DropDownList></td>
                   </tr>
                   <tr>
                     <td ><label class="labelStyle">* Parallel:</label></td> 
                     <td><asp:CheckBox ID="chkParallelProc" runat="server" /> </td>
                   </tr>
                   <tr>
                     <td ><label class="labelStyle">* Activo:</label></td> 
                     <td><asp:CheckBox ID="chkActivoProc" runat="server" /> </td>
                   </tr>

                    <tr style="width:100%">
                    <td><asp:HiddenField runat="server" ID="hdnIdProcesos" /></td>
                        <td align="left" valign="middle">
                            <asp:Button ID="btnGuardarProcesos" runat="server" Text="Guardar" CssClass="buttonBBVA2"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="buttonBBVA2"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

        <div>
            <cc1:ModalPopupExtender ID="mpeAltaTareas" 
                runat="server" 
                TargetControlID="BtnTareas" 
                PopupControlID="popAltaTareas"
                CancelControlID="btnCancelaTareas" 
                BackgroundCssClass="modalBackground"/>  
            <asp:Panel id="popAltaTareas" runat="server" style="display: none; border: 1px solid #ffffff; padding-top:20px; height:48%;"
                BackColor="#F2F2F2" 
                width="35%" 
                Height="33%">
                <table width="100%">
                    <tr>
                        <td class="tituloConsul">Tareas:</td>
                    </tr>
                   <tr>
                     <td ><label class="labelStyle">* Proceso:</label></td> 
                     <td><asp:DropDownList ID="ddlProcesoTarea" runat="server" CssClass="selectBBVA" Width="200px" ></asp:DropDownList></td>
                   </tr>
                   <tr>
                     <td ><label class="labelStyle">* Nombre Tarea:</label></td> 
                     <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" id="txtTareaNombre"></asp:TextBox> </td>
                   </tr>
                   <tr>
                     <td ><label class="labelStyle">* Orden:</label></td> 
                     <td><asp:TextBox runat="server" SkinID="txtNumerosGde" id="txtTareaOrden"></asp:TextBox> </td>
                   </tr>
                   <tr>
                     <td ><label class="labelStyle">* Tarea Padre:</label></td> 
                     <td><asp:DropDownList ID="ddlTareaPadre" runat="server" CssClass="selectBBVA" Width="200px" ></asp:DropDownList></td>
                   </tr>
                   <tr>
                     <td ><label class="labelStyle">* Tarea Rechazo:</label></td> 
                     <td><asp:DropDownList ID="ddlTareaRechazo" runat="server" CssClass="selectBBVA" Width="200px" ></asp:DropDownList></td>
                   </tr>
                   <tr>
                     <td ><label class="labelStyle">* Tarea No Rechazo:</label></td> 
                     <td><asp:DropDownList ID="ddlTareaNoRechazo" runat="server" CssClass="selectBBVA" Width="200px" ></asp:DropDownList></td>
                   </tr>
                   <tr>
                     <td ><label class="labelStyle">* Perfil:</label></td>
                     <td><asp:DropDownList ID="ddlperdilTarea" runat="server" CssClass="selectBBVA" Width="200px"></asp:DropDownList>  </td> 
                   </tr>                
                   <tr>
                     <td ><label class="labelStyle">* Parallel:</label></td> 
                     <td><asp:CheckBox ID="chkTareaParallel" runat="server" /> </td>
                   </tr>
                   <tr>
                     <td ><label class="labelStyle">* Activo:</label></td> 
                     <td><asp:CheckBox ID="chkTareaActivo" runat="server" /> </td>
                   </tr>
                    <tr style="width:100%">
                        <td><asp:HiddenField runat="server" ID="hdnIdTarea" /></td>
                        <td align="left" valign="middle">
                            <asp:Button ID="btnGuardaTareas" runat="server" Text="Guardar" CssClass="buttonBBVA2"/>
                            <asp:Button ID="btnCancelaTareas" runat="server" Text="Cancelar" CssClass="buttonBBVA2"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>                       
        </div>

         <div>
            <cc1:ModalPopupExtender ID="mpePantallasAsociadas" 
                runat="server" 
                TargetControlID="BtnPantallas" 
                PopupControlID="popPantallasAsociadas"
                CancelControlID="btnRegresarPantalla" 
                BackgroundCssClass="modalBackground"/>  
            <asp:Panel id="popPantallasAsociadas" runat="server" style="display: none; border: 1px solid #ffffff; padding-top:20px;"
                BackColor="#F2F2F2" 
                width="35%" 
                Height="33%">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:GridView ID="grvPantallas" runat="server"
                                    AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False"
                                    Width="100%" BackColor="white" PagerStyle-HorizontalAlign="Right"
                                    HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA"
                                    EmptyDataText="No hay registros de Pantallas asociadas" 
                                    EmptyDataRowStyle-CssClass="resul" >
                                    <Columns>
                                        <asp:BoundField DataField="PDK_PANT_NOMBRE" ItemStyle-Width="20%" HeaderText="Pantalla(s) Asociada(s)"/>
                                    </Columns>
                                </asp:GridView>
                        </td>
                        <td align="left" valign="middle">
                            <asp:Button ID="btnRegresarPantalla" runat="server" Text="Regresar" SkinID="btnGeneral"/>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:HiddenField runat="server" ID="hdnIdTareaPantalla" /><asp:HiddenField runat="server" ID="hdnIdTareaPerfil" /></td>
                    </tr>
                </table>
            </asp:Panel>                       
        </div>

        <div style="position:absolute; height:79%; top:20%; left:0%; width:100%; border-style:ridge; border-color:#808080; border-width:2px; ">

                <cc1:Accordion ID="AccProcesos" runat="server" FadeTransitions="true" FramesPerSecond="50"
                    TransitionDuration="200" SelectedIndex= "2" 
                    HeaderCssClass="tituloConsul"
                    Width="100%" Visible="true" Height="130px" AutoSize="Fill" > 
                    <Panes>
                        <cc1:AccordionPane ID="AccPnlAct" runat="server"  >
                            <Header>
                                <asp:label ID="LblAct" runat="server" Text="Procesos" />
                            </Header>
                            <Content>
                               <table width="100%">
                                    <tr>
                                        <td><asp:Button ID="btnAltaProcesos" runat="server" Text="Alta Procesos" CssClass="buttonBBVA2" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdProcesos" runat="server"
                                                AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False"
                                                Width="100%" BackColor="white" PagerStyle-HorizontalAlign="Right"
                                                HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA"
                                                EmptyDataText="No hay registros de Procesos relacionados al Flujo" 
                                                EmptyDataRowStyle-CssClass="resul" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Id" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LkbProc" runat="server" CssClass="resul" CommandName="idProc"  OnClick="lkbProc_Click" CommandArgument='<%# Eval("PDK_ID_PROCESOS") %>'><%#Eval("PDK_ID_PROCESOS")%></asp:LinkButton>
                                                        </ItemTemplate>                                            
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PDK_PROC_NOMBRE" ItemStyle-Width="20%" HeaderText="Proceso" ItemStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="PDK_PROC_ORDEN" ItemStyle-Width="20%" HeaderText="Orden" ItemStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="PDK_PROC_PADRE_NOMBRE" ItemStyle-Width="20%" HeaderText="Proceso Padre" ItemStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="PDK_PAR_SIS_PARAMETRO_PARALEL" ItemStyle-Width="20%" HeaderText="Proceso Paralelo" ItemStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="PDK_PAR_SIS_PARAMETRO" ItemStyle-Width="20%" HeaderText="Status" ItemStyle-HorizontalAlign="Center"/>
                                                    <asp:TemplateField HeaderText="Mod." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnModP" runat="server" CssClass="botones" CommandName ="btnModP" CommandArgument ='<%# Eval("PDK_ID_PROCESOS")%>' Text="M" OnClick = "btnModP_Click" />
                                                        </ItemTemplate>                                            
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </cc1:AccordionPane>                        
                    </Panes>
                </cc1:Accordion>                
 
                <cc1:Accordion ID="AccTareas" runat="server" FadeTransitions="true" FramesPerSecond="50"
                    TransitionDuration="200" SelectedIndex= "2" 
                    HeaderCssClass="tituloConsul"
                    Width="100%" Visible="true" Height="180px" AutoSize = "Fill"> 
                    <Panes>
                        <cc1:AccordionPane ID="AccordionPane1" runat="server">
                            <Header>
                                <asp:label ID="lblTareas" runat="server" Text="Tareas" />
                            </Header>
                            <Content>
                               <table width="100%">
                                    <tr>
                                        <td><asp:Button ID="btnAltaTareas" runat="server" Text="Alta Tareas" CssClass="buttonBBVA2" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdTareas" runat="server"
                                                AllowSorting="true" AutoGenerateColumns="False"
                                                Width="100%" BackColor="white" PagerStyle-HorizontalAlign="Right"
                                                HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA"
                                                EmptyDataText="No hay registros de Tareas relacionadas al Proceso" EmptyDataRowStyle-CssClass="resul" >
                                                <Columns>
                                                    <asp:BoundField DataField="PDK_ID_TAREAS" ItemStyle-Width="5%" HeaderText="Id"/>
                                                    <asp:BoundField DataField="PDK_TAR_NOMBRE" ItemStyle-Width="15%" HeaderText="Tarea"/>
                                                    <asp:BoundField DataField="PDK_PROC_NOMBRE" ItemStyle-Width="15%" HeaderText="Proceso"/>
                                                    <asp:BoundField DataField="PDK_TAR_ORDEN" ItemStyle-Width="5%" HeaderText="Orden"/>
                                                    <asp:BoundField DataField="PDK_TAR_PADRE_NOMBRE" ItemStyle-Width="15%" HeaderText="Tarea Padre"/>
                                                    <asp:BoundField DataField="PDK_ID_TAREAS_RECHAZO" ItemStyle-Width="10%" HeaderText="Tarea Rechazo"/>
                                                    <asp:BoundField DataField="PDK_ID_TAREAS_NORECHAZO" ItemStyle-Width="10%" HeaderText="Tarea NoRechazo"/>
                                                    <asp:BoundField DataField="PDK_PAR_SIS_PARAMETRO_PARALEL" ItemStyle-Width="10%" HeaderText="Tarea Paralela"/>
                                                    <asp:BoundField DataField="PDK_PAR_SIS_PARAMETRO" ItemStyle-Width="10%" HeaderText="Status"/>
                                                    <asp:TemplateField HeaderText="Mod." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnMod" runat="server" CssClass="botones" CommandName ="btnMod" CommandArgument ='<%# Eval("PDK_ID_TAREAS")%>' Text="M" OnClick="btnMod_Click"  ToolTip ="Modificar Registro"/>
                                                        </ItemTemplate>                                            
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pant." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnPant" runat="server" CssClass="botones" CommandName ="btnPant" CommandArgument ='<%# Eval("PDK_ID_TAREAS")%>' Text="P"  OnClick="btnPant_Click" ToolTip="Pantallas Asociadas"/>
                                                        </ItemTemplate>                                            
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </Content>
                        </cc1:AccordionPane>                        
                    </Panes>
                </cc1:Accordion>
           </div>
               
      </div>
     <div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td align="center" valign="middle">
                        <asp:Button runat="server" ID="btnProcesos" text="Procesos" CssClass="buttonBBVA2" Style="display:none;"/>
                        <asp:Button runat="server" ID="btnTareas" text="Tareas" CssClass="buttonBBVA2" Style="display:none;"/>
                        <asp:Button runat="server" ID="btnPantallas" text="Tareas" CssClass="buttonBBVA2" Style="display:none;"/>
                        <asp:Button runat="server" ID="btnRegresar" text="Regresar" CssClass="buttonBBVA2" />
                        <asp:Button runat="server" ID="btnGuardar" text="Guardar" CssClass="buttonBBVA2" />
                        
                    </td>
                </tr>
            </table>
      </div>
  </div>

  <asp:HiddenField runat="server" ID="hdnIdRegistro" />
 </asp:Content>
