<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaDocumentos.aspx.vb" Inherits="consultaDocumentos" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">

    <div class="divPantConsul">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">                    
                                <td style="width:40%;">Documentos</td>                                
                                <td class="campos" style="width:20%;">Personalidad Jurídica :
                                </td>
                                <td style="width:20%;">
                                    <asp:DropDownList ID="ddlPersonalidadJur" runat="server" SkinId="cmbGeneral"  AutoPostBack="true" Width= "100%"></asp:DropDownList>
                                </td>
                                <td style="width:20%;" align="right" valign="middle">
                                    <asp:Button runat="server" id="btnAgregar" text="Agregar" SkinID="btnGeneral" />
                                </td>
                            </tr>                
            </table>
        </div>   
             
        <p></p>

        <div class="divCuerpoConsul" style ="position:absolute ; top:10%; left:0%; width:100%;">
            <asp:GridView runat="server" id="grvConsulta" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" CssClass="resulGrid" 
                AllowSorting="True">
                <Columns>
                    <asp:TemplateField HeaderText="Id" ShowHeader="False" SortExpression="PDK_ID_DOCUMENTOS">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="resul" CommandName="catRelDoc" CommandArgument='<%# Eval("PDK_ID_DOCUMENTOS") %>'><%#Eval("PDK_ID_DOCUMENTOS")%></asp:LinkButton>
                        </ItemTemplate>                                            
                    </asp:TemplateField>
                    <asp:BoundField DataField="PDK_DOC_NOMBRE" HeaderText="Nombre Documento" 
                        SortExpression="PDK_DOC_NOMBRE" />
                    <asp:BoundField DataField="PDK_PER_NOMBRE" HeaderText="Personalidad Jurídica" 
                        SortExpression="PDK_PER_NOMBRE" />
                    <asp:BoundField DataField="PDK_PAR_SIS_PARAMETRO" HeaderText="Status" 
                        SortExpression="PDK_PAR_SIS_PARAMETRO" />
                     <%--   <asp:TemplateField>
                        <HeaderStyle Font-Underline = "true"/>
                        <HeaderTemplate>Documento en ProDoc</HeaderTemplate>
                        <ItemStyle CssClass = "resul" />
                        <ItemTemplate>                           
                            <asp:DropDownList ID = "ddlDescProdoc" runat = "server">
                            </asp:DropDownList>
                        </ItemTemplate>
                        </asp:TemplateField>          --%>              
                </Columns>      
            <HeaderStyle CssClass="encabezados" ForeColor="White"></HeaderStyle>
            <PagerStyle HorizontalAlign="Right"></PagerStyle>
            </asp:GridView>
        </div>
    </div>    

</asp:Content>
