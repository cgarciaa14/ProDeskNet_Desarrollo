<%@ Page Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="CajadeNotasExternas.aspx.vb" Inherits="aspx_CajadeNotasExternas" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>
<%--BBV-P-423:BUG-PD-91: ERODRIGUEZ: 12/06/2017 Funcionalidad Caja de Notas externas--%>
<%--BUG-PD-140: erodriguez: 05/07/2017: Se modifio para permitir visualizar mejor los mensajes--%>
<%--BUG-PD-148: erodriguez: 11/07/2017 Se agrego boton regresar--%>
<%--BUG-PD-198: CGARCIA: 23/08/2017: SE ELEBORO MERGE ENTRE TEST Y DESARROLLO--%>
<%--RQ-PI7-PD2: ERODRIGUEZ: 19/10/2017: Se agrego validacion para abrir caja de notas en pestaña nueva, llevando id de solicitud de pantalla donde se este trabajando.--%>
<%--RQ-PI7-PD13-3: ERODRIGUEZ: 21/12/2017: Se oculto Estatus de actividad--%>
<%--BUG-PD-438 : egonzalez : 08/05/2018 Se agrega el botón de "Limpiar"--%>
<script runat="server">

    
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
    <script type="text/javascript" src="../js/Funciones.js"></script>

    <script language="javascript" type="text/javascript">
        
        function confirmaAccion(msg) {
            var r = confirm(msg);
            if (r == true) {
                return true;
            } else {
                return false;
            }
        }
            
        function borrarMensaje(id) {
            if (confirmaAccion('¿Está seguro de querer borrar este mensaje?')) {
                btnInsUpd('UPDATE PDK_CAJA_NOTAS_EXT SET estatus = 0, fe_borrado = GETDATE(), usu_borrado = ' + $('[id$=hdnUsuario]').val() + ' WHERE id = ' + id, '');
                $('.bor[dataID=' + id + ']').parent().parent().hide();
            }
        }

        function pageLoad(e) {
            $('.bor').click(function () {
                borrarMensaje($(this).attr('dataID'));
            })


            $('.lee').click(function () {
                marcarMensajeLeido($(this).attr('dataID'));
            })
            
            $('#divMenuPricipal').hide();
            $('#DivActiva').hide();
            $('#dvBarraNot').hide();
            $('#DivSalir_sup_der').hide();

            var sol = $('[id$=Text1]').val();
            if (sol != '') {
              
                //$('#dvImagenesl').hide();
                $('[id$=Text1]').attr('disabled', true);
                $('[id$=txtTarea]').attr('disabled', true);
                $('[id$=LabelTarea]').attr('visible', false);
                
            }
        }


        function marcarMensajeLeido(id) {
            if (confirmaAccion('¿Desea marcar como leido este mensaje?')) {
                btnInsUpd('UPDATE PDK_CAJA_NOTAS_EXT SET leido = 1 WHERE id = ' + id, '');
                $(".lee").attr('disabled');
                $('[id$=btnActualizar]').trigger('click');
            }
        }


    </script>

    <div class="divAdminCat">

        <div class="divFiltrosConsul">
            <table cellpadding="2px">
                <tr>
                    <td class="tituloConsul">
                        <asp:Label ID="lbltitulo" Text="Caja de notas externas" runat="server"></asp:Label>
                    </td>
                </tr>

            </table>
        </div>
        <%--  --%>
    </div>


    <div class="divAdminCatCuerpo">
        <div id="debug" runat="server" style="position: absolute; top: 0%; left: 0%; width: 100%; height: 100%; overflow: auto;">
        </div>
        <div id="pantalla" runat="server" style="position: absolute; top: 0%; left: 0%; width: 100%; height: 100%; overflow: auto;">

            <table id="tableContent" border="0">
                <thead>
                    <tr>
                        <td class="campos" style="width: 50%;" align="right" valign="middle">
                            <asp:Label ID="Label1" runat="server" Text="Solicitud:"></asp:Label>
                            <input id="Text1" maxlength="19" style="width: 30%;" onkeypress="return ValCarac(event,7);" runat="server" />
                            <asp:Label ID="LabelTarea" runat="server" ></asp:Label>
                             
                           <%-- <input id="txtusu" runat="server" style="visibility:hidden"/>--%>
                        </td>

                        <td style="width: 10%;" align="left">
                            <%--<input id="btnBuscaCliente" type="button" value="Buscar" onclick="Buscar();" class="buttonBBVA2" runat="server" />--%>
                            <asp:Button runat="server" id="btnBuscaCliente" Text="Buscar" CssClass="buttonBBVA2" OnClick="btnBuscaCliente_Click"/>        
                        </td>
                         <td style="width: 10%;" align="left">                           
                            <asp:Button runat="server" id="btnRegresar" Text="Regresar" Visible="false" CssClass="buttonBBVA2" OnClick="btnRegresar_Click"/>        
                        </td>
                        <td style="width: 40%" align="left"><input id="btnCleanClient" class="buttonBBVA2" onclick="location.reload(true);" value="Limpiar" type="button"></td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="right">
                            <textarea style="width: 360px; height: 80px;" runat="server" id="textEditor" onkeypress="return ValCarac(event,26);"></textarea>
                            <asp:Button runat="server" CssClass="buttonSecBBVA2" ID="btnGuardar" Text="Guardar" />
                        </td>

                    </tr>
                    <tr>
                        <td class="left">
                            <asp:Button runat="server" ID="btnActualizar" OnClick="btnBuscaCliente_Click" Style="visibility: hidden" Text="Actualizar" /></td>
                        <asp:GridView ID="GridViewGral" runat="server" AutoGenerateColumns="false" 
                            HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowPaging="true" PageSize="10"
                            Width="100%" PagerStyle-HorizontalAlign="Center" OnPageIndexChanging="GridViewGral_PageIndexChanging"
                            EmptyDataText="No hay mensajes.">
                            <Columns>
                                <asp:BoundField DataField="PDK_ID_SOLICITUD" HeaderText="Folio Solicitud" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="fe_creacion" HeaderText="Fecha" DataFormatString="{0:dd-MM-yyyy HH:mm:ss}" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Tarea" HeaderText="Tarea" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                <%--<asp:BoundField DataField="mensaje" HeaderText="Mensaje"  ItemStyle-HorizontalAlign="Center" />--%>                                
                                <asp:TemplateField HeaderText="Mensaje">
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtbxMensaje" enabled="false" SkinID="txtGeneral" style="resize:none;min-width:98%!important;max-width:100%!important;min-height:50px;max-height:50px" runat="server" TextMode="MultiLine" Text='<%# Bind("[mensaje]") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>

                    </tr>
                    <tr>
                        <asp:GridView ID="gvMensajes" runat="server" AutoGenerateColumns="false" OnPageIndexChanging="gvMensajes_PageIndexChanging"
                            HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowPaging="true" PageSize="10"
                            Width="100%" PagerStyle-HorizontalAlign="Center"
                            EmptyDataText="No hay mensajes.">
                            <Columns>
                                <asp:BoundField DataField="PDK_ID_SOLICITUD" HeaderText="Folio Solicitud" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="fe_creacion" HeaderText="Fecha"  DataFormatString="{0:dd-MM-yyyy HH:mm:ss}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                 <asp:BoundField DataField="Tarea" HeaderText="Tarea" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                               <%-- <asp:BoundField DataField="mensaje" HeaderText="Mensajes" ItemStyle-HorizontalAlign="Center" />--%>
                                 <asp:TemplateField HeaderText="Mensaje">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbxMensajeg" enabled="false" SkinID="txtGeneral" style="resize:none;min-width:88%!important;max-width:90%!important;min-height:50px;max-height:50px" runat="server" TextMode="MultiLine" Text='<%# Bind("[mensaje]") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="id_nota" ItemStyle-CssClass="oculta" HeaderStyle-CssClass="oculta" FooterStyle-CssClass="oculta" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="leido" ItemStyle-CssClass="oculta" HeaderStyle-CssClass="oculta" FooterStyle-CssClass="oculta" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Leido" ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="Chk_leer" class="lee"  Enabled='<%#Eval("leido").ToString().Equals("0")%>' Checked='<%#Eval("leido").ToString().Equals("1")%>' dataID='<%#Eval("id_nota")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <input type="button" value="Borrar" class="bor" id="btnBorrar" style="visibility:hidden" dataid='<%#Eval("id_nota")%>'>
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

    <asp:HiddenField runat="server" ID="hdnFolio" />
    <asp:HiddenField runat="server" ID="hdnUsuario" />
    <asp:HiddenField runat="server" ID="tareaActual" />
    <asp:Label ID="lblNomUsuario" runat="server" Style="display: none;"></asp:Label>
</asp:Content>
