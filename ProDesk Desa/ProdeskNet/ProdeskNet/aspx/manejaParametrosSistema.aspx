<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="manejaParametrosSistema.aspx.vb" Inherits="manejoParametrosSistema" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/home.master" %>

<%--BUG-PD-309: 28/12/2017: DJUAREZ: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen.--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">
    <script type ="text/javascript">
        $(function () {
            $('[id$=txtValorFecha]').datepicker();
        })
    </script>
    <div class="divAdminCat">
        <div class="divFiltrosConsul">
            <table>
                <tr>
                    <td class="tituloConsul">Parametros Sistema</td>
                </tr>
            </table>
        </div>
        <div class="divAdminCatCuerpo">
            <div style="position:absolute; top:0%; left:0%; width:40%;">
                <table>
                    <tr>
                        <td style="width:33%"><label style="font-size: 8pt;font-family: Arial;color: #666666;">ID:</label></td>
                        <td><asp:label id="lblID" runat="server" SkinID="lblGeneral"></asp:label></td>
                    </tr>
                    <tr>
                        <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">* Padre:</label></td>
                        <td><asp:DropDownList ID="ddlPadre" runat="server" CssClass="selectBBVA"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">* Nombre:</label></td>
                        <td>
                            <asp:TextBox runat="server" SkinID="txtAlfaMayGde" id="txtNombre"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Valor Texto:</label></td>
                        <td>
                            <asp:TextBox runat="server" SkinID="txtGeneral" id="txtValorTexto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Valor Fecha:</label></td>
                        <td>
                            <asp:TextBox runat="server" SkinID="txtGeneral" id="txtValorFecha"></asp:TextBox>                            
                        </td>
                    </tr>
                    <tr>
                        <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Valor Numero:</label></td>
                        <td>
                            <asp:TextBox runat="server" SkinID="txtGeneral" id="txtValorNumero"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">* Activo:</label></td>
                        <td>
                            <asp:CheckBox runat="server" CssClass="resul"  id="chkStatus" />
                        </td>
                    </tr>

                </table>

            </div>            
        </div>
        <div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td align="center" valign="middle">
                        <asp:Button runat="server" id="btnRegresar" text="Regresar" CssClass="buttonBBVA2" />
                        <asp:Button runat="server" id="btnGuardar" text="Guardar" CssClass="buttonBBVA2" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdnIdRegistro" />


</asp:Content>
