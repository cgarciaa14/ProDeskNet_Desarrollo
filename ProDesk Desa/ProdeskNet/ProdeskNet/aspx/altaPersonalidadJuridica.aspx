<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="altaPersonalidadJuridica.aspx.vb" Inherits="altaPersonalidadJuridica" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/home.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">

    <div class="divAdminCat">
        <div class="divAdminCatTitulo">
            <table>
                <tr>
                    <td class="tituloConsul">Personalidad Jurídica</td>
                </tr>
            </table>
        </div>
        <div class="divAdminCatCuerpo">
            <div style="position:absolute; top:0%; left:0%; width:70%;">
                <table>                    
                    <tr>
                        <td class="campos">* Nombre:</td>
                        <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" id="txtNombre"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="campos">* Activo:</td>
                        <td>
                            <%--<asp:TextBox runat="server" SkinID="txtAlfaMayGde" id="txtTipoCambio"></asp:TextBox>--%>
                            <asp:CheckBox runat="server" CssClass="resul"  id="chkStatus" />
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
                        <asp:Button runat="server" id="btnGuardar" text="Guardar" SkinID="btnGeneral" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdnIdRegistro" />


</asp:Content>
