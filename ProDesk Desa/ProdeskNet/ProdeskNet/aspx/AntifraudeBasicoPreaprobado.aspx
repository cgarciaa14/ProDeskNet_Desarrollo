<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="AntifraudeBasicoPreaprobado.aspx.vb" Inherits="aspx_AntifraudeBasicoPreaprobado" %>

<%--BBVA-P-423 GVARGAS 12/05/2017 RQADM-07 Antifraude Básico Cliente, Empresa, Riesgos y Pre-Aprobados 40,76--%>
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
                    <td colspan="2" style="width:70%;">Antifraude Básico PreAprobado</td>
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

