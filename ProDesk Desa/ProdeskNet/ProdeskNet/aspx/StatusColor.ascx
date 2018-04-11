<%@ Control Language="VB" AutoEventWireup="false" CodeFile="StatusColor.ascx.vb" Inherits="aspx_StatusColor" %>

<%--BBV-P-423: RQSOL-03: AVH: 10/11/2016 SE CREA GRID DE COLORES--%>
<%--BUG PD-30 CGARCIA 10/04/2017 Se cambio el contorno blanco del GridView gvColor   --%>
<p style="bottom: auto">    
    <asp:GridView runat="server" AutoGenerateColumns="true" ID="gvColor" Width="98%" OnRowCreated="gvColor_RowCreated" >    
        <RowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Larger" BorderColor="White"/>
    </asp:GridView>
</p>

