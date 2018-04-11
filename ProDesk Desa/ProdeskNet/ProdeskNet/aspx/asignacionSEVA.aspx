<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="asignacionSEVA.aspx.vb" Inherits="aspx_asignacionSEVA" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"   TagPrefix ="cc1" %>--%>

<%--BBV-P-423 RQADM-30 MAPH 17/04/2017 Pantalla Referenciados SEVA 60, asignación automática de folios SEVA a las solicitudes correspondientes--%>
<%--BUG-PD-336: DJUAREZ: 15/01/2018: Se bloquea la pantalla cuando se esta ejecutando una pantalla automatica.--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var idPantalla = getUrlValue("idPantalla");
            blockScreen(idPantalla);
        });
    </script>
    <div class="divPantConsul">
        <div class ="divFiltrosConsul" style="padding-right:5px;" >
            <table class="tabFiltrosConsul">
                <tbody>
                <tr class="tituloConsul">
                    <td colspan="2" style="width:70%;">Asignación SEVA</td>
                    <td style="width:30%; text-align:right;"></td>
                    <td>
                        <asp:Button runat="server" ID="btnProcesar" Text="Procesar" CssClass="cssLetras" Visible="false" OnClick="btnProcesar_Click"/>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

