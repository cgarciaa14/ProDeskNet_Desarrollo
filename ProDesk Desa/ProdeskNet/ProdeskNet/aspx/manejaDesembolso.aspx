<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="manejaDesembolso.aspx.vb" Inherits="aspx_manejaDesembolso" %>

<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%--BBV-P-423 RQCONYFOR-07: AVH: 18/01/2017 SE CREA VENTANA DE DESEMBOLSO--%>
<%--BBVA-P-423: RQCONYFOR-07_2: AVH: 11/04/2017 modificaciones--%>
<%--BUG-PD-86: AVEGA: 10/06/2017 Se bloquea boton Procesar--%>
<%--BUG-PD-88: CGARCIA: 13/06/2017: SE AGREGAN LOS CONDICIONES PARA TURNAR LA TAREA --%>
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BUG-PD-259: RIGLESIAS 09/11/2017 Se OCULTO EL CONTENIDO QUE MOSTRABA LA PAGINA--%>
<%--BUG-PC-282: RHERNANDEZ: 01/12/17: SE MODIFICO LA PANTALLA DE DESEMBOLSO PARA ADAPTARSE DEPENDIENDO SI ES AUTOMATICA O NO Y SI YA FUE EMITIDA EXTERNAMENTE DEJAR AVANZAR NORMALMENTE--%>
<%--BUG-PD-336: DJUAREZ: 15/01/2018: Se bloquea la pantalla cuando se esta ejecutando una pantalla automatica.--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            var idPantalla = getUrlValue("idPantalla");
            blockScreen(idPantalla);
        });

        function ColumnaOculta() { display: none; }

        function mostrarCanceDiv() {
            div = document.getElementById('divcancela');
            div.style.display = '';
        }

        function btnGuardar(id) {

            var boton = id.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            var f = $('[id$=hdSolicitud]').val();
            var u = $('[id$=hdusuario]').val();
            var cadena = "UPDATE PDK_CAT_TAREAS SET PDK_TAR_MODIF=GETDATE() WHERE PDK_ID_TAREAS=" + $("[id$=hdPantalla] ").val()
            var cadenaUp = '';
            var pantalla = $('[id$=hdNomPantalla]').val();


            if (boton == "btnGuardarAutoriza") {
                var txtUsu = $('[id$=txtUsuario]').val()
                var txtpsswor = $('[id$=txtPassw]').val()
                var idpantalla = $("[id$=hdPantalla] ").val()
                var txtmotivoOb = $("[id$=txtmotivo]").val()
                //           var cadenastored = 'EXEC sp_ValidacionUsuario ' + '"' + cadenatodo + '",' + idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1'
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del passwird esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 46, 1, txtmotivoOb)

            } else if (boton == "btnGuardarCancelar") {

                var txtUsu = $('[id$=txtusua]').val();
                var txtpsswor = $('[id$=txtpass]').val();
                var idpantalla = $("[id$=hdPantalla] ").val()
                var txtmotivoOb = $("[id$=TxtmotivoCan]").val()
                $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del passwird esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)

            }
        }

        function btnProcesarCliente_click() {


            $('#<%=btnProcesarCliente.ClientID%>').prop('disabled', true)
            var buttonProcesar = document.getElementById('<%= btnProcesar.ClientID%>');
            buttonProcesar.click();

        }


    </script>



    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" src="../js/ProdeskNet.js"></script>

    <div class="divPantConsul">

        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <legend>
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Desembolso."></asp:Label></legend>
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
        <div id="divcuerpodesem" runat="server">
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

            <br />

            <div style="height: 300px; overflow: auto">
                <asp:Label ID="lblClientes" runat="server" Text="CLIENTE" class="tituloConsul"></asp:Label>
                <div class="resulbbvarigth">
                    <asp:GridView runat="server" ID="gvCupon" AutoGenerateColumns="true" ShowHeader="false" Width="100%">
                    </asp:GridView>
                </div>

                <br />
                <asp:Label ID="lblUnidad" runat="server" Text="UNIDAD A FINANCIAR" class="tituloConsul"></asp:Label>
                <div class="resulbbvarigth">
                    <asp:GridView runat="server" ID="gvCupon2" AutoGenerateColumns="true" ShowHeader="false" Width="100%">
                    </asp:GridView>
                </div>

                <br />
                <asp:Label ID="lblAgencia" runat="server" Text="AGENCIA" class="tituloConsul"></asp:Label>
                <div class="resulbbvarigth">
                    <asp:GridView runat="server" ID="gvAgencia" AutoGenerateColumns="true" HeaderStyle-HorizontalAlign="Left" Width="100%" HorizontalAlign="Left">
                    </asp:GridView>
                </div>

                <div style="height: 50px">
                </div>
                <asp:Label ID="lblDetalle" runat="server" Text="DETALLE DE DESEMBOLSO" class="tituloConsul"></asp:Label>
                <div class="resulbbvarigth">
                    <asp:GridView runat="server" ID="gvDetalle" AutoGenerateColumns="true" HeaderStyle-HorizontalAlign="Left" Width="100%" HorizontalAlign="Left">
                    </asp:GridView>
                </div>
            </div>
        </div>


    </div>


    <br />



    <div id="divcancela" style="display: none">
        <cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID="btnCancelar" PopupControlID="popCancela" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
        <asp:Panel ID="popCancela" runat="server" CssClass="cajadialogo ">
            <div class="tituloConsul">
                <asp:Label ID="Label3" runat="server" Text="Cancelación" />
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
                        <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" />
                    </td>
                </tr>

            </table>

        </asp:Panel>
    </div>
    <div class="resulbbvaCenter divAdminCatPie" id="divbotones" runat="server">
        <table width="100%" style="height: 100%;">
            <tr>
                <td align="right" valign="middle">
                    <label id="lblTurnar" runat="server" visible="true">Turnar:</label>
                    <asp:DropDownList ID="ddlTurnar" runat="server" Visible="true" CssClass="selectBBVA"></asp:DropDownList>
                    <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" />
                    <%--<input id="cmbguardar1"   runat="server"  type ="button" value="Procesar" Class="buttonBBVA2" onclick="" /> --%>
                    <input type="button" runat="server" id="btnProcesarCliente" value="Procesar" onclick="btnProcesarCliente_click();" class="buttonBBVA2" />
                    <%--<asp:Button runat="server" ID="btnProcesar" Text="Procesar" CssClass="buttonBBVA2" OnClick="btnProcesar_Click" />--%>
                    <%--<asp:Button runat="server" ID="btnGuardar" text="Procesar" SkinID="btnGeneral" />--%>
                    <asp:Button runat="server" ID="btnAutorizar" Text="Autorizar" Visible="false" CssClass="buttonSecBBVA2" />
                    <asp:Button runat="server" ID="btnCancelar" OnClientClick="mostrarCanceDiv()" Text="Cancelar" CssClass="buttonSecBBVA2" />
                    <asp:Button runat="server" ID="btnImprimir" Text="Imprimir" CssClass="buttonSecBBVA2" OnClick="btnImprimir_Click1" />
                    <asp:Button runat="server" ID="btnadelanta" Text="Adelantar" Visible="false" OnClick="btnadelanta_Click" />

                </td>
            </tr>
        </table>
    </div>

    <div id="divHiddenButton" style="visibility: collapse">
        <asp:Button runat="server" ID="btnProcesar" Text="Procesar" OnClick="btnProcesar_Click" />
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

