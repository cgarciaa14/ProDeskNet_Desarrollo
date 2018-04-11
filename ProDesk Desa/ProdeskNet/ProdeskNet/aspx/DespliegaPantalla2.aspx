<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.master" CodeFile="DespliegaPantalla2.aspx.vb" Inherits="aspx_DespliegaPantalla2" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%--RQ-PD: DCORNEJO: 23/02/2018 SE CREA NUEVA TAREA AUTOMATICA PARA REDIRECCIONAR A INS CHECK DOCUMENTAL--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
    <div class="divPantConsul">
        <div class ="divFiltrosConsul" style="padding-right:5px;" >
            <table class="tabFiltrosConsul">
                <tbody>
                <tr class="tituloConsul">
                    <td colspan="2" style="width:70%;">DespliegaPantalla3</td>
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
