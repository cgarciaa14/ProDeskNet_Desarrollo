<%@ Page Language="vb" MasterPageFile="~/aspx/Home.Master" AutoEventWireup="false" CodeFile="NivelPerfil.aspx.vb" Inherits="NivelPerfil" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace ="AjaxControlToolkit" TagPrefix ="cc1"   %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>
<%--BUG-PD-210: 20/09/2017: CGARCIA: ADICION DE SCROLL A NIVELES DE PERFIL--%>
<%--BUG-PC-242: 20/10/2017: RIGLESIAS: SE COMENTO TODA LA TABLA tituloConsul PARA NO MOSTRAR OBJETOS YA QUE SE PUSO EN MANTENIMIENTO LA PAGINA --%>
<%--BUG-PD-333: 10/01/2018: JMENDIETA: SE AGREGA UN DIV QUE CONTIENE LA IMAGEN DE MANTENIMIENTO PARA PODER MOSTRAR U OCULTAR SU CONTENIDO --%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPantallas">

    <div class="divAdminCat">
        <div class="divAdminCatTitulo">
            <table>
                <tr>
                    <td class="tituloConsul">Nivel de Perfil</td>
                </tr>
            </table>
        </div>
        <div class="divAdminCatCuerpo">
            <div style="position: absolute; top: 0%; left: 0%; width: 70%; height: 90%; overflow: auto;">
                <table width="100%">
                    <tr>
                        <td>
                            <h1>Sitio en mantenimiento</h1>
                            <img id="img1" src="../img/mantenimiento.png" style="background-position: center center;"/>
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>
    <div class="divAdminCat" style="display: none">
        <div class="divAdminCatTitulo">
            <table>
                <tr>
                    <td class="tituloConsul">Nivel de Perfil</td>
                </tr>
            </table>
        </div>
        <div class="divAdminCatCuerpo">
            <div style="position: absolute; top: 0%; left: 0%; width: 70%; height: 90%; overflow: auto;">
                <table width="100%">
                    <tr>
                        <!--<td class="campos">NIVEL: </td> -->
                        <td>
                            <asp:DropDownList ID="lblNiveles" runat="server" Width="150px" AutoPostBack="true" CssClass="Text"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div>
                                            <asp:GridView ID="grvNivel" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="encabezados" Width="100%" PagerStyle-HorizontalAlign="Center" CssClass="resulGrid" AllowSorting="true">
                                                <Columns>
                                                    <asp:BoundField DataField="PDK_ID_PERFIL" ItemStyle-CssClass="oculta" HeaderStyle-CssClass="oculta" FooterStyle-CssClass="oculta" ItemStyle-Width="30%" HeaderText="CLAVE" />
                                                    <asp:BoundField DataField="PDK_PER_NOMBRE" ItemStyle-CssClass="resul" ItemStyle-Width="60%" HeaderText="Nombre Perfil" />
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblNombre" runat="server" Text="Mostrar"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cnkstatus" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
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


    <asp:HiddenField runat="server" ID="hdnIdRegistro" />


</asp:Content>

