<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PantRespHERMESAtent.aspx.vb" Inherits="aspx_PantRespHERMESAtent" MasterPageFile ="~/aspx/Home.master" %>
<%@ MasterType VirtualPath="~/aspx/Home.master" %>
<%--BUG-PD-145: RHERNANDEZ: 10/07/17 SE CREA TAREA AUTOMATICA PARA CONSULTA SI LA RESPUESTA DE SCORING ES NECESARIA LA AUTENTICACION VIA CUESTIONARIO--%>
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
                    <td colspan="2" style="width:70%;">Validacion Respuesta Scoring para autenticacion</td>
                    <td style="width:30%; text-align:right;"></td>
                    
                </tr>
                </tbody>6
            </table>
        </div>
    </div>
</asp:Content>