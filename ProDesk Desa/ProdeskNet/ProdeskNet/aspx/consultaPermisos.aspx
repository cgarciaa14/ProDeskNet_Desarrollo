<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaPermisos.aspx.vb" Inherits="consultaPermisos" %>
<%--BUG-PD-246: RHERNANDEZ: 25/10/17: SE COLOCA PAGINA DE PERMISOS A EN MANTENIMIENTO POR SEGURIDAD (TEMPORAL)--%>
<%--BUG-PD-327: DJUAREZ: 04/01/2018: Se modifican los estilos de la pagina u orden de los datos--%>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
    <script>
        function postBackByObject() {
            var o = window.event.srcElement;
            if (o.tagName == 'INPUT' && o.type == 'checkbox') {
                __doPostBack('<%= btnBuscar.ClientID %>', '');
            }
        }
    </script>
    <div class="divPantConsul" style="display: none">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td>Sitio en Mantenimiento.</td>                    
                </tr>
            </table>
        </div>
        <div class="divCuerpoArbol">
            <img src="../img/mantenimiento.png" style = "background-position:center center;"/>
        </div>
    </div>


    <div class="divPantConsul" >
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td style="width: 20%;">Permisos</td>
                </tr>
            </table>
        </div>
        <div style ="position:absolute; top:10%; left:0%; width:100%; height:40%; overflow:auto;">
            <table>
                <tr>
                    <td style="width: 5%;" class="campos">Perfil:</td>
                    <td style="width: 30%;">
                        <asp:DropDownList runat="server" ID="ddlPerfil" CssClass="selectBBVA" AutoPostBack="True"></asp:DropDownList></td>
                    <td style="width: 5%;" class="campos">Menú:</td>
                    <td style="width: 30%;">
                        <asp:DropDownList runat="server" ID="ddlMenu" CssClass="selectBBVA" AutoPostBack="True"></asp:DropDownList></td>
                    <td style="width: 10%; text-align: right;">
                        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="buttonBBVA2" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="divCuerpoArbol" style ="top:20%;"">
            <table>
                <tr>
                    <td>
                        <asp:TreeView ID="trvObj" runat="server" ShowCheckBoxes="Leaf" onclick="postBackByObject();" Autopostback="True" ShowLines="True" Height="400px">
                            <NodeStyle CssClass="link" />
                        </asp:TreeView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
