<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="altaDistribuidor.aspx.vb" Inherits="altaDistribuidora" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/home.master" %>

<%--INC-B-1988:JDRA Cambio de Skin en clave.--%>
<%--BUG-PD-309: 28/12/2017: DJUAREZ: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen.--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">

    <div class="divAdminCat">
        <div class="divAdminCatTitulo">
            <table>
                <tr>
                    <td class="tituloConsul">Distribuidor</td>
                </tr>
            </table>
        </div>
        <div class="divAdminCatCuerpo">
            <div style="position:absolute; top:0%; left:0%; width:70%;">
                <table>                    
                    <tr>
                        <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">* Nombre Distribuidor:</label></td>
                        <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" id="txtNombre"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">* Empresa:</label></td>
                        <td><asp:DropDownList ID="cmdEmpresa" runat="server" CssClass="selectBBVA"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">* Plaza:</label></td>
                        <td><asp:DropDownList ID="ddlPlaza" runat="server" CssClass="selectBBVA"></asp:DropDownList></td>
                    </tr>
                    <%--<tr>
                        <td class="campos">* Clave:</td>
                        <td><asp:TextBox runat="server" SkinID="txtNumeros" id="txtClaveDistribuidor"></asp:TextBox></td>
                    </tr>--%>
                    <tr>
                        <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">* Activo:</label></td>
                        <td><asp:CheckBox ID="chkStatus" runat="server" /></td>
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
