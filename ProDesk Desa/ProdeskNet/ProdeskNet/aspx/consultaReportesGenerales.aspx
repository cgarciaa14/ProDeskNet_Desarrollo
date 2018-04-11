<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaReportesGenerales.aspx.vb" Inherits="aspx_consultaReportesGenerales" %>

<%@ MasterType VirtualPath="~/aspx/home.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
    <div style="position:absolute; top:1%; left:1%; width:90%; height:95%">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width:70%;">Reportes Generales</td>
                                <td style="width:30%;" align="right" valign="middle">
                                    <asp:Button runat="server" id="btnBuscar" text="Buscar" SkinID="btnGeneral" Visible ="false"  />
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td class="campos">Reporte:</td>
                                <td><asp:DropDownList runat="server" ID="ddlReporte" class="Text" Autopostback="True"></asp:DropDownList></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>        
        <div class="divCuerpoConsulReporte">
            <table>
                <tr>
                    <td>
                        <asp:GridView  DataKeyNames ="PDK_REP_NOMBRE_TDATO" runat="server" id="grvConsulta" AutoGenerateColumns="False" CssClass="resulGrid">
                            <Columns>
                                <asp:BoundField DataField="PDK_REP_NOMBRE_DATO" HeaderText="Nombre" />
                                <asp:TemplateField HeaderText="Dato" ShowHeader="False"  >
                                    <ItemTemplate>
                                        <asp:TextBox ID="lnkTexto" runat="server" CssClass="resul"></asp:TextBox>
                                        <asp:TextBox ID="txtFecha" runat="server" CssClass="resul" Visible="false"></asp:TextBox>
                                    </ItemTemplate>                                            
                                </asp:TemplateField>
                                <asp:BoundField DataField="PDK_REP_NOMBRE_TDATO" HeaderText="" ControlStyle-CssClass="ColumnaOculta"  ItemStyle-CssClass ="ColumnaOculta"  HeaderStyle-CssClass="ColumnaOculta"/>
                            </Columns>      
                            
                        <HeaderStyle CssClass="encabezados" ForeColor="White"></HeaderStyle>
                        <PagerStyle HorizontalAlign="Right"></PagerStyle>
                        </asp:GridView>
                   </td>
                   <td> <asp:Button id="btnConsultar" runat="server" Text="Consultar" Visible=false /> &nbsp;&nbsp;&nbsp;
                        <asp:Button id="btnExportarPDF" runat="server" Text="PDF" Visible =false />
                        <asp:Button id="btnExportarExcel" runat="server" Text="Excel" Visible =false />
                   </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td runat="server" id="tdReporte" class="campos" >Reporte: </td>
                    <td><asp:Label ID="lblReporte" runat="server" Visible = "false" CssClass="campos"></asp:Label></td>
                </tr>
            </table>
            <div style"overflow:auto;width:60%;height:100px;" >

                            <asp:Table id="tblResultado" runat="server">
                            </asp:Table>
            </div>
        </div>
    </div>    
</asp:Content>
