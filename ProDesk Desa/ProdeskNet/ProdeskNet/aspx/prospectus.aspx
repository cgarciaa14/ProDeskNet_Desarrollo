<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="prospectus.aspx.vb" Inherits="aspx_prospectus" %>

<%--BBV-P-423 RQ-PD-17 3 GVARGAS 08/01/2018 Mejoras carga huella y cambio payload --%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
    <div class="divPantConsul">
        <div class ="divFiltrosConsul" style="padding-right:5px;" >
            <table class="tabFiltrosConsul">
                <tbody>
                <tr class="tituloConsul">
                    <td colspan="2" style="width:70%;">Precalificación 4</td>
                    <%--<td colspan="2" style="width:70%;">Prospector</td>--%>
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

