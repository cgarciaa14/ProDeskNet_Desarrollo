<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="manejaRechazos.aspx.vb" Inherits="manejaRechazos" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/home.master" %>

<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">

    <div class="divAdminCat">
        <div class="divFiltrosConsul">
            <table>
                <tr>
                    <td class="tituloConsul">Rechazos</td>
                </tr>
            </table>
        </div>
        <div class="divAdminCatCuerpo">
            <div style="position:absolute; top:0%; left:0%; width:40%;">
                <table>
                    <tr>
                        <td class="campos">* Id:</td>
                        <td>
                            <asp:Label ID="lblIdRechazo" runat="server" skinid="lblCampos" ></asp:Label>
                        </td>
                    </tr>
                                   
                    <tr>
                        <td class="campos">* Descripcion:</td>
                        <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" id="txtDescripcion"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="campos">* Activo:</td>
                        <td><asp:CheckBox ID="chkActivo" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class = "campos">
                        * Condicionado:
                        </td>
                        <td>
                            <asp:DropDownList ID = "ddlcondicion" runat = "server" Width="195px" SkinID="cmbGeneral"></asp:DropDownList>                            
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
