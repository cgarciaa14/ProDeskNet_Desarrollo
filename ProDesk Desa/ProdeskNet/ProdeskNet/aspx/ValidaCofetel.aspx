<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="ValidaCofetel.aspx.vb" Inherits="aspx_ValidaCofetel" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
<!--'BBV-P-423-RQADM-10 JBEJAR 19/04/2017 Pantalla Consulta cofetel automatica. -->
<!--BUG-PD-336: DJUAREZ: 15/01/2018: Se bloquea la pantalla cuando se esta ejecutando una pantalla automatica. -->
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
                    <td colspan="2" style="width:70%;">Valida Cofetel</td>
                    <td style="width:30%; text-align:right;"></td>    
                </tr>
                </tbody>
            </table>
           </div>
    </div>
</asp:Content>

