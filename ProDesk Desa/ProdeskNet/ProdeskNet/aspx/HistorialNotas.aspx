<%@ Page Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="HistorialNotas.aspx.vb" Inherits="aspx_HistorialNotas" %>

<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<%--BUG-PD-254: ERODRIGUEZ: 27/10/2017: Se creo ventana para el historial de caja de notas externas e internas.--%>
<%--BUG-PD-268: RIGLESIAS: 10/11/2017: Se agrego el filtro de buscar solicitud  y boton de limpiar.--%>
<%--BUG-PD-303: DCORNEJO: 13/12/2017: Se movio de seccion la parte del filtro que constan de la caja de texto y los botones Buscar y Limpiar--%>
<%--RQ-PI7-PD13-3: ERODRIGUEZ: 21/12/2017: Se oculto Estatus de actividad--%>


<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
    <script type="text/javascript" src="../js/Funciones.js"></script>

     <script language="javascript" type="text/javascript">

        function pageLoad(e) {
           
                $('#divMenuPricipal').hide();
                $('#DivActiva').hide();
                $('#dvBarraNot').hide();
                $('#DivSalir_sup_der').hide();
            }
        
        var sol = $('[id$=Text1]').val();
        if (sol != '') {
            $('#divMenuPricipal').hide();
                        
            $('[id$=Text1]').attr('disabled', true);
            $('[id$=txtTarea]').attr('disabled', true);
            $('[id$=LabelTarea]').attr('visible', false);
        }
        
    </script>

    <div class="divAdminCat">

        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td class="tituloConsul">
                    <%--<td colspan="2" style="width: 30%;">Historial de Notas </td>--%>
                     <asp:Label ID="lbltitulo" Text="Historial de Notas" runat="server"></asp:Label>
                    <%--<td style="width: 5%;" class="campos">Solicitud</td>
                    <td style="width: 20%;">--%>
                        </td>
                </tr>
                        <%--<input id="txtIdnota" maxlength="19" style="width: 30%;" onkeypress="return ValCarac(event,7);"
                            runat="server" /></td>
                    <td style="width: 20%;" align="right" valign="middle">
                        <asp:Button runat="server" ID="btnBuscaNota" Text="Buscar" CssClass="buttonBBVA2"
                            OnClick="btnBuscaNota_Click" />
                    </td>
                     <td>
                     <td style="width: 20%;" align="left" valign="middle">
                        <asp:Button runat="server" ID="Button1" Text="Limpiar" CssClass="buttonBBVA2" OnClick=Button2_Click />  
                     </td>
                </tr>--%>

            </table>
            <%--<br />--%>
        </div>
        <%--  --%>
    </div>

   
    <div class="divAdminCatCuerpo">
        <div id="debug" runat="server" style="position: absolute; top: 0%; left: 0%; width: 100%;
            height: 100%; overflow: auto;">
        </div>
        <div id="pantalla" runat="server" style="position: absolute; top: 0%; left: 0%; width: 100%;
            height: 100%; overflow: auto;">

            <table id="tableContent" border="0">
                <thead>
                    <tr>
                        <td class="campos" style="width: 50%;" align="right" valign="middle">
                            <asp:Label runat="server" Text="Solicitud:"></asp:Label>
                            <input id="txtIdnota" maxlength="19" style="width: 30%;" onkeypress="return ValCarac(event,7);"
                            runat="server" />
                             <%--<asp:Label ID="LabelTarea" runat="server" ></asp:Label>--%>
                        </td>
                         <td style="width: 10%;" align="left">
                            <%--<asp:Button runat="server" id="btnBuscaCliente" Text="Buscar" CssClass="buttonBBVA2" OnClick="btnBuscaCliente_Click"/>--%>
                             <asp:Button runat="server" ID="btnBuscaNota" Text="Buscar" CssClass="buttonBBVA2"
                            OnClick="btnBuscaNota_Click" />        
                        </td>
                        <td style="width: 20%;" align="left" valign="middle">
                        <asp:Button runat="server" ID="Button1" Text="Limpiar" CssClass="buttonBBVA2" OnClick=Button2_Click />  
                     </td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <asp:GridView ID="GridViewGral" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GridViewGral_PageIndexChanging"
                            HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA"
                            AllowPaging="true" PageSize="10"
                            Width="100%" PagerStyle-HorizontalAlign="Center"
                            EmptyDataText="No hay mensajes.">
                            <Columns>
                                <asp:BoundField DataField="PDK_ID_SOLICITUD"  HeaderText="Folio Solicitud" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="fe_creacion" HeaderText="Fecha" DataFormatString="{0:dd-MM-yyyy HH:mm:ss}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Caja" HeaderText="Tipo" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Tarea" HeaderText="Tarea" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Mensaje">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxMensaje" Enabled="false" SkinID="txtGeneral" Style="resize: none;
                                            min-width: 98%!important; max-width: 100%!important; min-height: 50px; max-height: 50px"
                                            runat="server" TextMode="MultiLine" Text='<%# Bind("[mensaje]") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="leido" ItemStyle-CssClass="oculta" HeaderStyle-CssClass="oculta"
                                    FooterStyle-CssClass="oculta" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Leido" ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="Chk_leer" class="lee" Enabled="False" Checked='<%#Eval("leido").ToString().Equals("1")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </tr>

                </tbody>
            </table>

            <div>
            </div>
            <div>
            </div>


        </div>

    </div>


 </asp:Content>