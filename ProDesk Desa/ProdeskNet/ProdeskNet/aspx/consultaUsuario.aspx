<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaUsuario.aspx.vb" Inherits="consultaUsuario" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>
    <%--BBV-P-412:AVH:18/07/2016 RQ B: SE OCULTA EL BOTON AGREGAR YA QUE EL ALTA SE REALIZA DESDE PROCOTIZA--%>
	<%--BUG-PD-309: 28/12/2017: DJUAREZ: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen.--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">
<script language="javascript" type ="text/jscript">
    $(document).ready(function () {
        $('#search').keyup(function () {
            searchTable($(this).val(), $('[id$=grvConsulta]'));
        });
    });

</script> 
    <div class="divPantConsul">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td style="width:20%;">Usuarios</td> 
                </tr>                                                                                                                                                                 
            </table>
        </div>
        <div class="divCuerpoConsul">
            <table  width="100%" >
                    <tr>
                        <td style = "width:10%"><label id ="lblPerfil" style="font-size: 8pt;font-family: Arial;color: #666666;">Perfil:</label></td>                              
                        <td style = "width:50%">
                            <asp:DropDownList runat ="server" ID="cmbPerfil" CssClass="selectBBVA" AutoPostBack="true"></asp:DropDownList></td> 
                    <%--      <td style="width:10%; text-align:right;">
                            <asp:Button runat ="server" ID="btnBuscar" Text="Buscar" SkinID="btnGeneral" />                                  
                        </td> --%>                                  
                        <td style="width:10%; text-align:right;">
                            <asp:Button runat="server" id="btnAgregar" text="Agregar" SkinID="btnGeneral" Visible="false" />
                        </td>                
                    </tr>
                    <tr >
                      <td ><label id ="lblBusqueda" style="font-size: 8pt;font-family: Arial;color: #666666;">BUSQUEDA POR NOMBRE:</label></td>
                      <td style=" width:100%;" ><input type="text" class="resul" style=" width:80%;"    onkeypress ="ManejaCar('A',1,this.value,this);" id="search" /></td>
                    </tr>                
                    <tr>
                        <td colspan = "6">  
                                <div style="width:100%; height:410px; overflow:auto "    >                                             
                                 <asp:GridView runat="server" Width="100%" id="grvConsulta" AutoGenerateColumns="False"    
                                 HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowSorting="true" PagerStyle-HorizontalAlign="Right">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" ShowHeader="False" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catIdUsu" CommandArgument='<%# Eval("PDK_ID_USUARIO") %>'><%# Eval("PDK_ID_USUARIO")%></asp:LinkButton>
                                        </ItemTemplate>                                            
                                    </asp:TemplateField>                   
                                        <asp:BoundField DataField="PDK_USU_NOMBRE"   HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField ="PDK_USU_APE_PAT" HeaderText="Apellido Paterno" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField ="PDK_USU_APE_MAT" HeaderText="Apellido Materno" ItemStyle-HorizontalAlign="Center" />
                                         <asp:BoundField DataField="PDK_USU_CLAVE" HeaderText="Clave" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField ="PDK_PAR_SIS_PARAMETRO" HeaderText ="Status"  ItemStyle-HorizontalAlign="Center"/>
                                  </Columns>      
                     
                            </asp:GridView>  
                               </div>                         
                        </td>
                    </tr>
                  </table>                        
        </div>                        
    </div>    
</asp:Content>
