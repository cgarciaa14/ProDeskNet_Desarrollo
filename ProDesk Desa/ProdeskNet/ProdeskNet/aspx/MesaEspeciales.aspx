<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="MesaEspeciales.aspx.vb" Inherits="aspx_MesaEspeciales" %>

<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>


<%--BBV-P-423: RQADM-19: AVEGA: 10/05/2017 Mesa Especial, Mesa Especiales Saldos Vencidos
    BBV-P-423: RQXLS1: CGARCIA: 23/05/2017 Se agrego la consulta de alianza por registtro--%>
    <%--BUG-PD-74: JBEJAR 07/06/2017 visor documental imprimir caratula--%>
    <%--BUG-PD-80: JBEJAR 09/06/2017 se elimina el boton html no es necesario la funcion en javascript ya que no se evalua nada en ella solo se oprime el boton runnat server.--%>
    <%--BUG-PD-86: AVEGA: 10/06/2017 cambios JBEJAR--%>
    <%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
    <%--BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN--%>
    <%--BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS PARA ABRIR VISOR TELEPRO--%>
    <%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
    <%--BUG-PD-416: RHERNANDEZ: 12/04/2018: Se ocultan los botones de cancelar cuando la tarea ya fue procesada--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
     <style>
         
.divAdminCatPie_local
{
	position:relative;
	top:30%;
	left:1%;
	width:50%;
	height:8%;
    background-color: white;/*#F3F7FB*/
}
     </style>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script src="../js/jquery.fileupload.js"></script>
    <script type="text/javascript" language="javascript">

        function pageLoad() {


            var habilitado = getParameterByName('Enable');

            if (habilitado == 1) {

                $('#ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1C').hide();
                $('#btnCancelar').hide();
                $('#btnCancelarNew').hide();
            } else {


                $('#ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1C').show();


            }

        }

        function fnChequea() {
            fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');
                PopUpLetrero("Documento procesado exitosamente");
        }

        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();
        }

        function btnGuardar(id) {
            //debugger;
            var boton = id.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            var f = $('[id$=hdSolicitud]').val();
            var u = $('[id$=hdusuario]').val();
            var cadena = "UPDATE PDK_CAT_TAREAS SET PDK_TAR_MODIF=GETDATE() WHERE PDK_ID_TAREAS=75";
            var cadenaUp = '';
            var pantalla = $('[id$=hdNomPantalla]').val();


            if (boton == "btnGuardarAutoriza") {
                var txtUsu = $('[id$=txtUsuario]').val()
                var txtpsswor = $('[id$=txtPassw]').val()
                var idpantalla = $("[id$=hdPantalla] ").val()
                var txtmotivoOb = $("[id$=txtmotivo]").val()
                //           var cadenastored = 'EXEC sp_ValidacionUsuario ' + '"' + cadenatodo + '",' + idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1'

                $('#ventanaContain').hide();
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del passwird esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 46, 1, txtmotivoOb)

            } else if (boton == "btnGuardarCancelar") {
                //debugger;
                var txtUsu = $('[id$=txtusua]').val();
                var txtpsswor = $('[id$=txtpass]').val();
                var idpantalla = $("[id$=hdPantalla] ").val()
                var txtmotivoOb = $("[id$=TxtmotivoCan]").val()
                $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())

                $('#ventanaContain').hide();
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del passwird esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)

            }
        }
        function getParameterByName(name, url) {
            if (!url) {
                url = window.location.href;
            }
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }

        $(document).ready(function () { $.urlParam = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } }; if ($.urlParam("Enable").toString() == "1") { $("#btnCancelarNew").hide(); }; });
    </script>
    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnCancelar {
            display: none;
        }
    </style>


    <div class="divPantConsul">

        <script type="text/javascript" src="../js/Funciones.js"></script>
        <script type="text/javascript" src="../js/ProdeskNet.js"></script>
        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <legend>
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Mesa Especiales"></asp:Label></legend>
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
            <tr>
                <th class="campos">Alianza
                </th>
                <th class="campos">
                    <asp:Label ID="lblAlianza" Font-Underline="true" runat="server">
                    </asp:Label>
                </th>
            </tr>
        </table>


        <div style="height: 20px"></div>

        <div id="Div1" class="resulbbvaCenter" runat="server">
            <asp:Label ID="lblDetalle" runat="server" Text="Información de la Solicitud" class="tituloConsul"></asp:Label>

            <asp:GridView runat="server" ID="gvMesaEspeciales" AutoGenerateColumns="true"
                HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowPaging="true" PageSize="10"
                Width="100%" PagerStyle-HorizontalAlign="Center" HorizontalAlign="center" RowStyle-HorizontalAlign="Center"
                EmptyDataText="No se encontró información con los parámetros proporcionados.">
            </asp:GridView>

        </div>

        <div style="height: 50px"></div>

        <div id="Div2" class="resulbbvaCenter" runat="server">
            <asp:Label ID="lblSolicitudes" runat="server" Text="Consulta por más de un vehículo " class="tituloConsul"></asp:Label>

            <asp:GridView runat="server" ID="gvMesaAutos" AutoGenerateColumns="false"
                ShowHeaderWhenEmpty="true"
                HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowPaging="true" PageSize="10"
                Width="100%" PagerStyle-HorizontalAlign="Center" HorizontalAlign="center" RowStyle-HorizontalAlign="Center"
                EmptyDataText="No se encontró información con los parámetros proporcionados.">
                <Columns>
                    <asp:BoundField DataField="NoSolicitud" HeaderText="Número de Solicitud" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre del Cliente" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="RFC" HeaderText="RFC" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="CP" HeaderText="CP" ItemStyle-HorizontalAlign="Center" />

                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="tooltip">
                                <asp:LinkButton runat="server" ID="lbDetalle" Text="Ver Detalle" OnClick="lbDetalle_Click" CommandArgument='<%# Eval("NoSolicitud")%>'></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>
        <br />

        <div>
            <table id="tbValidarObjetos" class="resulGrid"></table>
        </div>

        <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top:15%; left:31%; width: 220px;">
            <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID="btnCancelar" PopupControlID="popCancela" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>--%>
            <%--<asp:Panel ID="popCancela" runat="server" CssClass="cajadialogo ">
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
                            <textarea id="TxtmotivoCan" runat="server" class="Text" rows="5" cols="1" style="width: 100%"></textarea>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td>
                            <asp:HiddenField runat="server" ID="HiddenField1" />
                        </td>
                        <td align="left" valign="middle">
                            <%--<input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" onclick="btnGuardar(id)"  />--%>
                            <asp:Button runat="server" ID="btnGuardarCancelar" Text="Guardar" OnClientClick="btnGuardar(id)" />
                            <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral" />
                        </td>
                    </tr>

                </table>
        </div>
        <div class="resulbbvaCenter divAdminCatPie_local">
            <table width="100%" style="height: 50%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Label runat="server" ID="lblTurnar" Text="Turnar *"></asp:Label>
                        <asp:DropDownList runat="server" CssClass="select2BBVA" ID="ddlTurnar">
                        </asp:DropDownList>
                        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" OnClick="btnRegresar_Click" />
                        <%--<input id="cmbguardar1"   runat="server"  type ="button" value="Procesar" Class="buttonBBVA2" onclick="" /> --%>
                        <%--<asp:Button runat="server" ID="btnProcesar" Text="Procesar" CssClass="buttonBBVA2" OnClick="btnProcesar_Click" />--%>
                        <%--<asp:Button runat="server" ID="btnGuardar" text="Procesar" SkinID="btnGeneral" />--%>
                        <asp:Button runat="server" ID="btnAutorizar" Text="Autorizar" Visible="false" CssClass="buttonSecBBVA2" />
                       <%-- <input type="button" runat="server" id="btnProcesarCliente" value="Procesar" onclick="btnProcesarCliente_click();" class="buttonBBVA2" />--%>
                        <asp:Button runat="server" ID="btnProcesar" Text="Procesar"  CssClass="buttonBBVA2" OnClick="btnProcesar_Click" />
                        <asp:button runat="server" ID="btnPrint" Text="Imprimir" AutoPostBack="true" onclick="btnPrint_Click"    CssClass="buttonSecBBVA2"/>
                        <asp:Button runat="server" ID="btnVisor" Text="Visor Documental" CssClass="buttonSecBBVA2" OnClick="btnVisor_Click" />
                        <asp:Button runat="server" ID="btnCancelar" OnClientClick="mostrarCanceDiv()" Text="Cancelar" CssClass="buttonSecBBVA2" />
                        <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                        <asp:Button runat="server" ID="btnImprimir" Text="Imprimir" OnClick="btnImprimir_Click" CssClass="buttonSecBBVA2" Visible="false" />
                        <asp:Button runat="server" ID="btnadelanta" Text="Adelantar" Visible="false" OnClick="btnadelanta_Click" />



                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div id="divHiddenButton" style="visibility: collapse">
            
        </div>
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

