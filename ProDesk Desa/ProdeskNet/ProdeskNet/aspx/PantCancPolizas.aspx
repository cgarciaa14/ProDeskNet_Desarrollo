<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.master" CodeFile="PantCancPolizas.aspx.vb" Inherits="aspx_PantCancPolizas" %>
<%@ MasterType VirtualPath="~/aspx/Home.master" %>
<%--BUG-PD-45: RHERNANDEZ: 15/05/17 SE CREA TAREA AUTOMATICA PARA LA CANCELACION DE POLIZA DE SEGURO DE DAÑOS Y DE VIDA(AUN SIN IMPLEMENTAR)--%>
<%--BUG-PD-395: DJUAREZ: 13/03/2018 Se agrega bloqueo de pantalla ya que es pantalla automatica--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
    <script type="text/javascript" language="javascript">
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
                    <td colspan="2" style="width:70%;">Cancelacion de polizas</td>
                    <td style="width:30%; text-align:right;"></td>                    
                </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
