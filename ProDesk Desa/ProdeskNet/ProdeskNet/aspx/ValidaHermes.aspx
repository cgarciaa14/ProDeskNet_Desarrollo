<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="ValidaHermes.aspx.vb" Inherits="aspx_ValidaHermes" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%--BUG-PD-103  JBEJAR 17/06/2017 TAREA AUTOMATICA VALIDA RESPUESTA HERMES. --%>
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
                    <td colspan="2" style="width:70%;">EMAIL Y SMS AUTOMATICO</td>
                    <td style="width:30%; text-align:right;"></td>      
                </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
