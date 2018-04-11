<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="ValidaCI.aspx.vb" Inherits="aspx_ValidaCI" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
    <!--BUG-PD-22: JBB 2017/03/30 aspx de valida calculo de ingresos.-->
    <!--BUG-PD-336: DJUAREZ: 15/01/2018: Se bloquea la pantalla cuando se esta ejecutando una pantalla automatica.-->
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
                    <td colspan="2" style="width:70%;">Valida Calculo de Ingresos</td>
                    <td style="width:30%; text-align:right;"></td>
                    
                </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

