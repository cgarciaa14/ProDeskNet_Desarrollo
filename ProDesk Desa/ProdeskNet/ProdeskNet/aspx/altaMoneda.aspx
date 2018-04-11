﻿<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="altaMoneda.aspx.vb" Inherits="altaMoneda" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/home.master" %>

<%--BUG-PD-309: 28/12/2017: DJUAREZ: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen.--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">

    <div class="divAdminCat">        
        <div class="divAdminCatTitulo">
            <table>
                <tr>
                    <td class="tituloConsul">Moneda</td>
                </tr>
            </table>
        </div>
        <div class="divAdminCatCuerpo">
            <div style="position:absolute; top:0%; left:0%; width:70%;">
                <table>                    
                    <tr>
                        <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">* Nombre:</label></td>
                        <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" id="txtNombreMoneda"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">* Tipo de Cambio:</label></td>
                        <td><asp:TextBox runat="server" SkinID="txtMontos" id="txtTipoCambio"></asp:TextBox></td>
                    </tr>
                    <tr>
                      <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Activo:</label></td>
                      <td><asp:CheckBox runat="server" ID="chkActivo" SkinID="cmbGeneral" /></td>  
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
