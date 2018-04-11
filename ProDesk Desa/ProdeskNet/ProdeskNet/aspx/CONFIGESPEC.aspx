<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="CONFIGESPEC.aspx.vb" Inherits="aspx_CONFIGESPEC" %>

<%@ MasterType VirtualPath="~/aspx/Home.Master" %>
<%-- BUG-PD-50 JBEJAR 13/05/2017 SE CREA PANTALLA DE CONFIGURACION  --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
    <script src="../js/jquery.ui.widget.js"></script>
    <script src="../js/jquery.iframe-transport.js"></script>
    <script src="../js/jquery.fileupload.js"></script>
    <script src="../js/Funciones.js"></script>

    <div class="divAdminCat">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td class="tituloConsul">Pantalla Config. Especiales</td>
                </tr>
            </table>
        </div>
        <div class="divAdminCatCuerpo "  >
            <div style="position: absolute; top: 0%; left: 0%; width: 100%;" align="center">
                <table>
                    <tr>
                          <td class ="campos">Id Configuracion:</td>
                          <td><asp:Label runat ="server" SkinID ="lblCampos" ID="lblIdConfiguracion"></asp:Label></td>
                        </tr>
                </table>
                <table>
                    <tr>
                        <td class="campos">Número de vehículos</td>
                        <td>
                            <asp:TextBox runat="server" SkinID="txtGeneralGde" ID="txtnumeve" Width="30px" Onkeypress="return ValCarac(event,7);" MaxLength="8"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="campos">Periodo para solicitar autorización por más unidades:</td>
                        <td>
                            <asp:TextBox runat="server" SkinID="txtGeneralGde" ID="txtPeriodo" Onkeypress="return ValCarac(event,7);" MaxLength="4"></asp:TextBox>
                        </td>
                        <td class="campos">Meses</td>
                    </tr>
                    <tr>
                       <td class="campos">Autorizar por montos a financiar:</td>
                    </tr>
                    <tr>

                    </tr>
                    <tr>
                        <td class="campos">Alianza</td>
                        <td class="campos">Multimarca</td>
                    </tr>
                    <tr>
                        <td class="campos"><asp:TextBox runat="server" SkinID="txtGeneralGde" ID="TextAli" Onkeypress="return ValCarac(event,7);" MaxLength="8"></asp:TextBox></td>
                        <td class="campos"><asp:TextBox runat="server" SkinID="txtGeneralGde" ID="TextMulti" Onkeypress="return ValCarac(event,7);" MaxLength="8"></asp:TextBox></td>
                        <td class="campos">
                         Pesos
                        </td>
                    </tr>
                    <tr>
                        <td class="campos">Autorizar por un riesgo potencial:</td>
                    </tr>
                    <tr>

                        <td class="campos">Riesgo potencial mayor a</td>
                         <td>
                            <asp:TextBox runat="server" SkinID="txtGeneralGde" ID="txtRiesgo" Onkeypress="return ValCarac(event,7);" MaxLength="8"></asp:TextBox>
                        </td>
                        <td class="campos">
                         Pesos
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" SkinID="btnGeneral" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>

