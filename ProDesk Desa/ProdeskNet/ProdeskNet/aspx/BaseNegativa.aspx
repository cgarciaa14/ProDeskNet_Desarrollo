<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="BaseNegativa.aspx.vb" Inherits="aspx_BaseNegativa" MasterPageFile="~/aspx/Home.master" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"   TagPrefix ="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<%--BBVA-P-423:RQSOL-01: AMR:14/12/2016 Precalificación Brechas (31, 49, 75)--%>
<%--BUG-PD-13  GVARGAS  28/02/2017 Corrige caracterer ingresado por error--%>
<%--BUG-PD-16: MAPH: 09/03/2016 Correcciones en la pantalla --%>
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
                    <td colspan="2" style="width:70%;">Precalificación 1</td>
                    <%--<td colspan="2" style="width:70%;">Base Negativa</td>--%>
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

