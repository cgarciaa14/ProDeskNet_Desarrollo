<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="ValidaEmails.aspx.vb" Inherits="aspx_ValidaEmails" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%---- RQMSG  JBEJAR 26/05/2017  Script tarea automamtica emails automaticos. --%>
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

