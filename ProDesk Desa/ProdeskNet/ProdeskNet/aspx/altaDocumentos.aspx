<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="altaDocumentos.aspx.vb" Inherits="altaDocumentos" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">
    <div class="divAdminCat">
        <div class="divAdminCatTitulo">
            <table>
                <tr>
                    <td class="tituloConsul">Documentos</td>
                </tr>
            </table>
        </div>
        <div class="divAdminCatCuerpo">
            <div style="position:absolute; top:0%; left:0%; width:70%;">
                <table>                    
                    <tr>
                        <td class="campos">* Id:</td>
                        <td>
                            <asp:Label ID="lblIdRelacion" runat="server" skinid="lblCampos" ></asp:Label>
                            <asp:Label ID="lblIdDocumento" runat="server" SkinID ="lblCampos" visible="false" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="campos">* Personalidad Juridica:</td>
                        <td><asp:DropDownList ID="ddlPersonalidadJuridica" runat="server" SkinID="cmbGeneral" ></asp:DropDownList></td>
                    </tr>

                    <tr>
                        <td class="campos">* Nombre Documento:</td>
                        <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" id="txtNombreDocumento"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="campos">* Activo:</td>
                        <td><asp:CheckBox ID="chkActivo" runat="server" SkinID="Texto" /> </td>
                    </tr>
                    <tr>
                        <td class="campos">* ID ProdDesk:</td>
                        <td><asp:DropDownList ID = "ddlIDProdDesk" runat = "server" SkinID="cmbGeneral"></asp:DropDownList></td>
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
