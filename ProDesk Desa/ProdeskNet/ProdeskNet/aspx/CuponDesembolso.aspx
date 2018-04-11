<%@ Page Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="CuponDesembolso.aspx.vb" Inherits="aspx_CuponDesembolso" %>
<%@ MasterType VirtualPath="~/aspx/Home.master" %>

<%--RQADM2-03 RHERNANDEZ: 14/09/17: SE CREA PANTALLA PARA LA IMPRESION DEL COMPROBANTE DE DESEMBOLSO--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
    <script language="javascript">
        function Guardar() {
            var botonp = document.getElementById('<%= btnproc.ClientID%>');
            botonp.click();
        }
    </script>
    <div class="divPantConsul">

        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <legend>
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Comprobante de Desembolso."></asp:Label></legend>
                                </td>
                                <td>
                                    <asp:Label ID="lblIdPantalla" runat="server" CssClass="oculta"></asp:Label>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <table width="100%" class="fieldsetBBVA">
            <tr>
                <th class="campos" style="width: 25%">Solicitud:                    
                </th>
                <th class="campos" style="width: 25%">
                    <asp:Label ID="lblSolicitud" Font-Underline="true" runat="server">
                    </asp:Label>
                </th>
                <th class="campos" style="width: 25%">Cliente:                    
                </th>
                <th class="campos" style="width: 25%">
                    <asp:Label ID="lblCliente" Font-Underline="true" runat="server">
                    </asp:Label>
                </th>
            </tr>
            <tr>
                <th class="campos">Status Credito:                    
                </th>
                <th class="campos">
                    <asp:Label ID="lblStCredito" Font-Underline="true" runat="server">
                    </asp:Label>
                </th>
                <th class="campos">Status Documentos:                    
                </th>
                <th class="campos">
                    <asp:Label ID="lblStDocumento" Font-Underline="true" runat="server">
                    </asp:Label>
                </th>
            </tr>
        </table>
        
        <div class="resulbbvaCenter divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" id="btnRegresar" Text ="Regresar" CssClass ="buttonSecBBVA2" OnClick="btnRegresar_Click" />     
                        <input id="cmbguardar1" runat="server" value="Procesar" type="button" onclick="$(this).prop('disabled', true); Guardar();" class="buttonBBVA2" /> 
                        <asp:Button runat="server" ID="btnImprimir" Text="Imprimir" CssClass="buttonSecBBVA2" OnClick="btnImprimir_Click" />                                           
                    </td>
                </tr>
            </table>
        </div>
    </div>
     <div id="divbtncollap" style="visibility: collapse">
            <asp:Button runat="server" ID="btnproc" OnClick="btnproc_Click" />
        </div>
    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />
    <asp:HiddenField runat="server" ID="hdnResultado" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
    <asp:HiddenField ID="hdNomPantalla" runat="server" />
    <asp:HiddenField ID="hntipoPantalla" runat="server" />
    <asp:HiddenField ID="hdnResultado1" runat="server" />
    <asp:HiddenField ID="hdnResultado2" runat="server" />
</asp:Content>
