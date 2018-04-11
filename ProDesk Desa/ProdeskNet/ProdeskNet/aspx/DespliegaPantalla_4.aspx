<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="DespliegaPantalla_4.aspx.vb" Inherits="aspx_DespliegaPantalla_4" %>
<%--RQ-PD29 JMENDIETA: 28/02/2018 Se agrega pantalla automatica que redirigira automaticamente a la siguiente tarea.  --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var idPantalla = getUrlValue("idPantalla");
            blockScreen(idPantalla);
        });
    </script>

    <div class="divPantConsul">
        <div class="divFiltrosConsul" style="padding-right: 5px;">
            <table class="tabFiltrosConsul">
                <tbody>
                    <tr class="tituloConsul">
                        <td colspan="2" style="width: 70%;">Despliega Pantalla 4</td>
                        <td style="width: 30%; text-align: right;"></td>
                    </tr>
                </tbody>
            </table>
            1                                                                                                   
        </div>
    </div>
</asp:Content>

