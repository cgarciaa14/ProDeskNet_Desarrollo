<%@ Page Title="" Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.master" CodeFile="PantBusqClient.aspx.vb" Inherits="aspx_PantBusqClientIncredit"  %>
<%@ MasterType VirtualPath="~/aspx/Home.master" %>
<%--RQADM-38:RHERNANDEZ:05/05/17: Se crea pantalla para buscar si el cliente de la solicitud esta dado de alta en expediente unico--%>
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
                    <td colspan="2" style="width:70%;">Busqueda de Datos Cliente</td>
                    <td style="width:30%; text-align:right;"></td>
                    
                </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
