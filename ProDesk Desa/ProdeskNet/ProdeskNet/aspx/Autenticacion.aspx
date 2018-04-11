<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.master" CodeFile="Autenticacion.aspx.vb" Inherits="aspx_Autenticacion" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%--RQADM-20: RHERNANDEZ: 19/05/17: SE CREA LA PAGINA PARA GESTIONAR LAS TAREAS DE CUESTIONARIO AUTENTICACION Y CUESTIONARIO IVR--%> 
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BUG-PD-174: RHERNANDEZ: 28/07/17 SE  AGREGO FUNCION "cambiaVisibilidadDiv" POR QUE NO PERMITIA CANCELAR SOLICITUDES--%> 
<%--BUG-PD-336: DJUAREZ: 15/01/2018: Se bloquea la pantalla cuando se esta ejecutando una pantalla automatica.--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
<%--BUG-PD-395: DJUAREZ: 13/03/2018 Se quita bloqueo de pantalla ya que no es pantalla automatica--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">
    <script src="../js/Funciones.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            
        });

        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();
        }

        function btnGuardar(id) {
            var boton = id.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            var f = $('[id$=hdSolicitud]').val();
            var u = $('[id$=hdusuario]').val();
            var pantalla = $('[id$=hdNomPantalla]').val();
            var idpantalla = $("[id$=hdPantalla] ").val()
            var cadena = 'UPDATE PDK_CAT_TAREAS SET PDK_TAR_MODIF=GETDATE() WHERE PDK_ID_TAREAS=' + idpantalla.toString();
            var cadenaUp = '';



            if (boton == "btnGuardarAutoriza") {
                cambiaVisibilidadDiv('divautoriza', false);
                var txtUsu = $('[id$=txtUsuario]').val()
                var txtpsswor = $('[id$=txtPassw]').val()
                var idpantalla = $("[id$=hdPantalla] ").val()
                var txtmotivoOb = $("[id$=txtmotivo]").val()
                //           var cadenastored = 'EXEC sp_ValidacionUsuario ' + '"' + cadenatodo + '",' + idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1'

                $('#ventanaContain').hide();
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 46, 1, txtmotivoOb)

            } else if (boton == "btnGuardarCancelar") {
                cambiaVisibilidadDiv('divcancela', false);
                var txtUsu = $('[id$=txtusua]').val();
                var txtpsswor = $('[id$=txtpass]').val();
                var idpantalla = $("[id$=hdPantalla] ").val()
                var txtmotivoOb = $("[id$=TxtmotivoCan]").val()
                $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())

                $('#ventanaContain').hide();
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)

            }
        }
        function cambiaVisibilidadDiv(id, visible) {
            div = document.getElementById(id);
            if (visible) {
                div.style.display = '';
            }
            else {
                div.style.display = 'none'
            }
        }
        function Guardar()
        {
            var botonp = document.getElementById('<%= btnproc.ClientID%>');
            botonp.click();
        }

        $(document).ready(function () { $.urlParam = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } }; if ($.urlParam("Enable").toString() == "1") { $("#btnCancelarNew").hide(); }; });
    </script>
    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnCancelar {
            display: none;
        }
    </style>
    <div class="divPantConsul">
        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <legend>
                                        <asp:Label ID="lblNomPantalla" runat="server"></asp:Label></legend>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divCuerpoConsul" id="dvCuerpo">
            <table class="fieldsetBBVA" style="width: 100%">
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
            <br />            
            <br />
            <table class="resulbbvaCenter" id="CuestionarioIVR" runat="server">
                <tr>
                    <td>&nbsp;</td>
                    <td>¿Autenticación IVR exitosa? *</td>
                    <td>
                        <asp:DropDownList ID="ddlOKIVR" runat="server" CssClass="select2BBVA">
                            <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        </asp:DropDownList></td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <div id="CuestionarioAut" runat="server">
                    <asp:GridView ID="gvCuestionario" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvCuestionario_RowDataBound"
                        HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowPaging="false"
                        Width="80%" PagerStyle-HorizontalAlign="Right"
                        EmptyDataText="No se pudo recuperar el cuestionario.">
                        <Columns>
                            <asp:BoundField DataField="PDK_ID_PREGUNTA" HeaderText="ID" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="PDK_PREGUNTA" HeaderText="Pregunta" ItemStyle-HorizontalAlign="Left" >
                                <ItemStyle Width="120px" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Respuesta">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlRespuesta" runat="server" CssClass="select4BBVA" ></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:BoundField DataField="PDK_TIPO_PREGUNTA" HeaderText="PDK_TIPO_PREGUNTA" ItemStyle-HorizontalAlign="Left"/> 
                            <asp:BoundField DataField="PDK_NO_RESPUESTAS" HeaderText="PDK_NO_RESPUESTAS" ItemStyle-HorizontalAlign="Left"/> 
                            <asp:BoundField DataField="PDK_RESPUESTA_CORRECTA" HeaderText="PDK_RESPUESTA_CORRECTA" ItemStyle-HorizontalAlign="Left" />  
                            <asp:BoundField DataField="PDK_AYUDA_PREGUNTA" HeaderText="PDK_AYUDA_PREGUNTA" ItemStyle-HorizontalAlign="Left"/>                  
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
    <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top:15%; left:31%; width: 220px;">
        <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID="btnCancelar" PopupControlID="popCancela" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>--%>
<%--        <asp:Panel ID="popCancela" runat="server" CssClass="cajadialogo ">
        </asp:Panel>--%>
            <div class="tituloConsul">
                <asp:Label ID="Label1" runat="server" Text="Cancelación" />
            </div>
            <table width="100%">
                <tr>
                    <td class="campos">Usuario:</td>
                    <td>
                        <asp:TextBox ID="txtusua" SkinID="txtGeneral" MaxLength="12" runat="server" Style="width: 120px !important;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="campos">Password:</td>
                    <td>
                        <asp:TextBox ID="txtpass" runat="server" SkinID="txtGeneral" MaxLength="12" TextMode="Password" EnableTheming="true" Style="width: 120px !important;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="campos">Descripción:</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="TxtmotivoCan" runat="server" onkeypress="return ValCarac(this.event, 6);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" class="Text" rows="5" cols="1" style="width: 95% ! important"></textarea>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td>
                        <asp:HiddenField runat="server" ID="HiddenField1" />
                    </td>
                    <td align="left" valign="middle">
                        <input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar(id)" />
                        <br />
                        <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral" />
                    </td>
                </tr>

            </table>
    </div>
    <div class="resulbbvaCenter divAdminCatPie">
        <table width="100%" style="height: 100%;">
            <tr>
                <td>
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2" OnClick="btnRegresar_Click" />
                     <input id="cmbguardar1" runat="server" value="Procesar" type="button" onclick="$(this).prop('disabled', true); Guardar();" class="buttonBBVA2" />
                    <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClientClick="mostrarCanceDiv()" CssClass="buttonSecBBVA2" />
                    <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divbtncollap" style="visibility: collapse">
        <asp:Button runat="server" ID="btnproc" OnClick="btnproc_Click"/>
    </div>
    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />
    <asp:HiddenField ID="hdnResultado" runat="server" />
    <asp:HiddenField ID="hdnResultado1" runat="server" />
    <asp:HiddenField ID="hdnResultado2" runat="server" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
    </div>
    </div>
</asp:Content>


