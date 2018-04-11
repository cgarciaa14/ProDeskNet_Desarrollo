<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="manejaRecotizacion.aspx.vb" Inherits="aspx_manejaRecotizacion" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%--'RQ-PI7-PD9: CGARCIA: 15/11/2017: CREACION DE LA RECOTIZACION--%>
<%--BUG-PD-336: DJUAREZ: 15/01/2018: Se bloquea la pantalla cuando se esta ejecutando una pantalla automatica.--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var idPantalla = getUrlValue("idPantalla");
            blockScreen(idPantalla);
        });
    </script>
    <div class="divPantConsul">
        <div class="divFiltrosConsul" style="padding-right:5px;">
            <table class="tabFiltrosConsul">
                <tbody>
                    <tr class="tituloConsul">
                        <td colspan="2" style="width:70%;">RE-COTIZACION</td>
                        <td style="width:30%; text-align:right;"></td>
                    </tr>
                </tbody>
            </table>1                                                                                                   
        </div>
    </div>
</asp:Content>

