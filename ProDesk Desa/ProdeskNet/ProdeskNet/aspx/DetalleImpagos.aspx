<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="DetalleImpagos.aspx.vb" Inherits="aspx_DetalleImpagos" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>
<%--BUG-PD-272: MGARCIA: 23/11/2017: Se agrego pantalla DetalleImpagos y su funcionalidad--%>
<%--BUG-PD-290: MGARCIA: 05/12/2017: Detalle Impagos en Menu--%>
<%--BUG-PD-302: DJUAREZ: 13/12/2017: Se quitaron los filtros del encabezado dejandolos abajo del mismo--%>
<%--BUG-PD-327: DJUAREZ: 04/01/2018: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen--%>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphPantallas">
    <%--<style>
        table {
            width: 50% !important;
            margin-top: 5%;
            margin-left: 5%;
        }

        #tblTotal
{
           
            margin-top: 1%;
         
        }

    </style--%>

     <script type="text/javascript" src="../js/Funciones.js"></script>
    <script language="javascript" type="text/javascript">


       
        function pageLoad(e) {
           
                $('#divMenuPricipal').hide();
                
            }
        

    </script>


   <div class="divAdminCat">

        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td colspan="2" style="width: 30%;">Detalle Impagos </td>
                </tr>

            </table>
            <br />
        </div>
        <%--  --%>
    </div>
    <br />
    <div class="divAdminCatCuerpo">
        <table>
            <tr>
                <td style="width: 5%;" ><label id ="lblSolicitud" style="font-size: 8pt;font-family: Arial;color: #666666;">Solicitud:</label></td>
                <td style="width: 20%;">
                    <input id="txtDetalle" maxlength="19" style="width: 30%;" SkinID="txtAlfaMinGde" onkeypress="return ValCarac(event,7);"
                        runat="server" /></td>
                <td style="width: 20%;" align="right" valign="middle">
                    <asp:Button runat="server" ID="btnBuscaDetalle" Text="Buscar" CssClass="buttonBBVA2"
                        OnClick="btnBuscaDetalle_Click" />
                </td>
                    
                <td style="width: 20%;" align="left" valign="middle">
                    <asp:Button runat="server" ID="Button1" Text="Limpiar" CssClass="buttonBBVA2" OnClick=Button2_Click />
                           
                </td>
            </tr>
        </table>
        <asp:GridView ID="GrdDetalleImpagos"
            runat="server"
            Width="100%"
            AutoGenerateColumns="false"
            ShowHeaderWhenEmpty="True"
            EmptyDataText="No se encontro información."
            HeaderStyle-CssClass="GridviewScrollHeaderBBVA" 
            RowStyle-CssClass="GridviewScrollItemBBVA"
            Font-Size= "Small">
            <Columns>
                <asp:BoundField HeaderText="DETALLE IMPAGO" DataField="Detalle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                <asp:BoundField HeaderText="IMPORTE" DataField="Cantidad" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
            </Columns>
            <EmptyDataRowStyle HorizontalAlign="Center"/>
            <EmptyDataTemplate>No se encontro información.</EmptyDataTemplate>
        </asp:GridView>
        <br />
<br />
        <table id="tblTotal" align="center">
        <tbody>
            <tr>
                <th style="width:100%"><label id ="lblTotal" style="font-size: 8pt;font-family: Arial;color: #666666;">TOTAL:</label></th> 
                <th style="width:47%"> 
                    <asp:label  runat="server" id="txtTotalMonto"  
                                onkeyup="setNewText(event, this);"
                                class="txt3BBVA cfdicampo"
                                onblur="upperCaseString(this);" maxlength="36" Style="width: 420px !important;" Title="Folio Fiscal *"/>                                      
                </th>
            </tr>
        </tbody>
    </table>
    </div>



</asp:Content>

     


