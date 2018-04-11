<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="Prospector.aspx.vb" Inherits="aspx_Prospector" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %>

<%--BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)--%>
<%--BUG-PD-16: MAPH: 09/03/2017 Correcciones en la pantalla --%>
<%--BUG-PD-336: DJUAREZ: 15/01/2018: Se bloquea la pantalla cuando se esta ejecutando una pantalla automatica.--%>

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
                    <td colspan="2" style="width:70%;">Precalificación 4</td>
                    <%--<td colspan="2" style="width:70%;">Prospector</td>--%>
                    <td style="width:30%; text-align:right;"></td>
                    <td>
                        <asp:Button runat="server" ID="btnprocesar" Text="Procesar" CssClass="cssLetras" Visible="false"/>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

