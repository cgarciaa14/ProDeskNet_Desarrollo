<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaBuroCreditoINTL.aspx.vb" Inherits="consultaBuroCreditoINTL" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">

    <div class="divPantConsul">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td style="width:20%;">Consulta Buro de Credito INTL</td> 
                    <td class="campos" style = "width:10%"></td>                              
                               <td style = "width:50%">
                                <td style="width:10%; text-align:right;">
                                  <asp:Button runat ="server" ID="btnConsultar" Text="Consulta Buro" SkinID="btnGeneral" />                                  
                                </td>                                   
                                <td style="width:10%; text-align:right;">
                                </td>                
                </tr>                                                                                                                                                                 
            </table>
        </div>
        <div class="divCuerpoConsul">
        </div>                        
    </div>    


</asp:Content>
