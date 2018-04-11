<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="CarruselID.aspx.vb" Inherits="aspx_CarruselID" %>
<%--BBVA-P-423 RQADM-11 GVARGAS 10/03/2017 Antifraude Básico – Carruseles (Celulares, RFC, ID’s, Contador de Teléfonos) 31--%>
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
                        <td colspan="2" style="width:70%;">CARRUSEL ID</td>
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

