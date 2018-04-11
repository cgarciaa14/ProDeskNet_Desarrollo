<%@ Page Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="PantallaEmiteBBVA.aspx.vb" Inherits="aspx_PantallaEmiteBBVA" %>
<%--BUG-PD-116: RHERNANDEZ: 26/06/17: SE CREA PANTALLA PARA SEPARAR LA EMISION DE SEGUROS BANCOMER CON LA PANTALLA DE DESEMBOLSO--%>
<%--BUG-PD-128: RHERNANDEZ: 01/07/17: SE BLOQUEAN BOTONES DE REGRESAR Y PROCESAR PARA EVITAR PROBLEMAS--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">

    <script language="javascript">
        function pageLoad() {
            fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');
        }
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
                                        Emision de Polizas BBVA</legend>
                                </td>                                
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
         <div class="divCuerpoConsul" id="dvCuerpo">
            <table class="fieldsetBBVA" style="width: 100%">
                <!--style="margin-left: 0px"-->
                <tr>
                    <th class="campos" style="width: 25%">Solicitud:                    
                    </th>
                    <th class="campos" style="width: 25%">
                        <asp:Label ID="lblSolicitud" Font-Bold="true" Font-Underline="true" runat="server">
                        </asp:Label>
                    </th>
                    <th class="campos" style="width: 25%">Cliente:                    
                    </th>
                    <th class="campos" style="width: 25%">
                        <asp:Label ID="lblCliente" Font-Bold="true" Font-Underline="true" runat="server">
                        </asp:Label>
                    </th>
                </tr>
                <tr>
                    <th class="campos">Status Credito:                    
                    </th>
                    <th class="campos">
                        <asp:Label ID="lblStCredito" Font-Bold="true" Font-Underline="true" runat="server">
                        </asp:Label>
                    </th>
                    <th class="campos">Status Documentos:                    
                    </th>
                    <th class="campos">
                        <asp:Label ID="lblStDocumento" Font-Bold="true" Font-Underline="true" runat="server">
                        </asp:Label>
                    </th>
                </tr>
            </table>
            <br />    
             <fieldset style="overflow: scroll; height: 300px; overflow-x: hidden; border: none">
                <table id="tbValidarObjetos" class="resulGrid"></table>
            </fieldset>       
        </div>

       <div class="resulbbva divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">                        
                       <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" OnClick="btnRegresar_Click"  Enabled="false" />                      
                        <input id="cmbguardar1" runat="server" value="Procesar" type="button" onclick="$(this).prop('disabled', true); Guardar();" class="buttonBBVA2" disabled="disabled" />                  

                    </td>
                </tr>
            </table>
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
    </div>
</asp:Content>
