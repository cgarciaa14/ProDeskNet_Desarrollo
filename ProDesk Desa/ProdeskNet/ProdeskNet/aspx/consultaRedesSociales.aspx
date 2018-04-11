<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.master" CodeFile="consultaRedesSociales.aspx.vb" Inherits="aspx_consultaRedesSociales" %>

<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--RQ-PD19 DCORNEJO: 12/02/2018: SE AGREGA NUEVA TAREA 'CONSULTA REDES SOCIALES'--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
    <script type="text/javascript" src="../js/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="../js/jquery.iframe-transport.js"></script>
    <script type="text/javascript" src="../js/jquery.fileupload.js"></script>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript">
        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();
        }
        function fnChequea() {
            fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');
            $('#ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1C').attr('disabled', false);
            PopUpLetrero("Documento procesado exitosamente");
        }

        function btnGuardar(id) {
            var boton = id.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            var f = $('[id$=hdSolicitud]').val();
            var u = $('[id$=hdusuario]').val();
            var pantalla = $('[id$=hdNomPantalla]').val();
            var idpantalla = $("[id$=hdPantalla] ").val()
            var cadena = 'UPDATE PDK_CAT_TAREAS SET PDK_TAR_MODIF=GETDATE() WHERE PDK_ID_TAREAS=' + idpantalla.toString();
            var cadenaUp = 'UPDATE PDK_CAT_TAREAS SET PDK_TAR_MODIF=GETDATE() WHERE PDK_ID_TAREAS=' + idpantalla.toString();
            var txtUsu = $('[id$=txtusua]').val();
            var txtpsswor = $('[id$=txtpass]').val();
            var idpantalla = $("[id$=hdPantalla] ").val()
            var txtmotivoOb = $("[id$=TxtmotivoCan]").val()

            if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
            if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
            if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

            btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)
            $('#divcancela').hide()
        }

        function pageLoad() {

            var settingsDate1 = {
                dateFormat: "dd/mm/yy",
                showAnim: "slide",
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                selectOtherMonths: true,
                autoSize: true,
                yearRange: "-99:+0",
                maxDate: "+0m +0d"
            };

            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $('#feIni').datepicker(settingsDate1).attr('readonly', 'true').attr('onkeydown', 'return false');

            var habilitado = getParameterByName('Enable');

            if (habilitado == 1) {

                var fecha, fecha2
                if ($('#<%=hfFeIni.ClientID%>').val() != "") {
                    fecha = $('#<%=hfFeIni.ClientID%>').val().split('/')

                    fecha2 = fecha[0] + '/' + fecha[1] + '/' + fecha[2]
                    $('#feIni').val(fecha2);
                }

                $('#feIni').attr('disabled', true);
                $('#ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1C').hide();
            } else {

                $('#feIni').datepicker(settingsDate1).attr('readonly', 'true').attr('onkeydown', 'return false');
                $('#ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1C').show();

            }
        }

        function Replica() {
            var depende = '';
            var hasError = false;
            var textResult = 'Información de la solicitud:';
            var valorI = true;
            var Select = $('#<%= ddlPerfil.ClientID%>')
            var Select1 = $('#<%= ddlPerfilLinkedin.ClientID%>')
            var red = $('#<%= ddlRedSocial.ClientID%>')
            var redLinkedin = $('#<%= ddlRedSocialLinkedin.ClientID%>')

            if (valorI == true) {
                if (red.val() == -1 || red.val() == 1) {
                    $.each(red, function () {
                        if ($(this).val() == -1) {
                            textResult += "\n + Falta información en el campo " + $(this)[0].title;
                            hasError = true;
                        }
                    });
                    $.each(Select, function () {
                        if ($(this).val() == -1) {
                            textResult += "\n + Falta información en el campo " + $(this)[0].title;
                            hasError = true;
                        }
                    });
                }
                if (redLinkedin.val() == -1 || redLinkedin.val() == 1) {
                    $.each(redLinkedin, function () {
                        if ($(this).val() == -1) {
                            textResult += "\n + Falta información en el campo " + $(this)[0].title;
                            hasError = true;
                        }
                    });
                    $.each(Select1, function () {
                        if ($(this).val() == -1) {
                            textResult += "\n + Falta información en el campo " + $(this)[0].title;
                            hasError = true;
                        }
                        else {
                            depende += $(this)[0].id + ',';
                        }
                    });
                }
                if (hasError == true) {
                    alert(textResult);
                } else {

                    var botonBuscar = document.getElementById('<%= btnProcesar.ClientID%>');

                    botonBuscar.click();

                }

            } else {

                var botonBuscar = document.getElementById('<%= btnProcesar.ClientID%>');

                botonBuscar.click();
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
        <div class="fieldsetBBVA">
            <fieldset class="fieldsetBBVA">
                <legend>Redes Sociales</legend>
            </fieldset>
        </div>

        <br />
        <table class="fieldsetBBVA" style="width: 100%">
            <tr>
                <td class="campos" style="width: 25%">Solicitud:                    
                </td>
                <td class="campos" style="width: 25%">
                    <asp:Label ID="lblSolicitud" Font-Bold="true" Font-Underline="true" runat="server">
                    </asp:Label>
                </td>
                <td class="campos" style="width: 25%">Cliente:                    
                </td>
                <td class="campos" style="width: 25%">
                    <asp:Label ID="lblCliente" Font-Bold="true" Font-Underline="true" runat="server">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="campos">Status Credito:                    
                </td>
                <td class="campos">
                    <asp:Label ID="lblStCredito" Font-Bold="true" Font-Underline="true" runat="server">
                    </asp:Label>
                </td>
                <td class="campos">Status Documentos:                    
                </td>
                <td class="campos">
                    <asp:Label ID="lblStDocumento" Font-Bold="true" Font-Underline="true" runat="server">
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td class="campos">Correo Electrónico:                    
                </td>
                <td class="campos">
                    <asp:Label ID="LblSCorreo" Font-Bold="true" Font-Underline="true" runat="server">
                    </asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <fieldset class="fieldsetBBVA">
            <legend>Encuensta Facebook</legend>
            <div>
                <table class="resulbbvarigth">
                    <tr>
                        <td colspan="5" align="center">Portal Facebook:&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnFacebook" runat="server" Text="Ir a Facebook" CssClass="buttonSecBBVA2" OnClick="btnFacebook_click" /></td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>¿Existe Red social Facebook? *</td>
                        <td>
                            <asp:DropDownList ID="ddlRedSocial" runat="server" CssClass="selectBBVA" OnSelectedIndexChanged="ddlRedSocial_select_index" AutoPostBack="true" title="¿Existe Red social Facebook?*">
                                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Página no disponible"></asp:ListItem>

                            </asp:DropDownList></td>
                        <td>&nbsp;</td>

                        <td>¿Coincide foto del perfil con ID? *</td>
                        <td>
                            <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="selectBBVA" title="¿Coincide foto del perfil con ID?*">
                                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                <asp:ListItem Value="3" Text="No Publico"></asp:ListItem>
                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <br />
        <fieldset class="fieldsetBBVA">
            <legend>Encuensta Linkedin</legend>
            <div>
                <table class="resulbbvarigth">
                    <tr>
                        <td colspan="5" align="center">Portal Linkedin:&nbsp;&nbsp;&nbsp;<asp:Button ID="btnLinkedin" runat="server" Text="Ir a Linkedin" CssClass="buttonSecBBVA2" OnClick="btnLinkedin_click" /></td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>

                    <tr>
                        <td>¿Existe Red Social Linkedin? * </td>
                        <td>
                            <asp:DropDownList ID="ddlRedSocialLinkedin" runat="server" CssClass="selectBBVA" OnSelectedIndexChanged="ddlRedSocialLinkedin_SelectedIndexChanged" AutoPostBack="true" title="¿Existe Red Social Linkedin? *">
                                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Página no disponible"></asp:ListItem>
                            </asp:DropDownList></td>
                        <td>&nbsp;</td>
                        <td>¿Coincide Foto perfil (Empleo vs IFE/INE)? *</td>
                        <td>
                            <asp:DropDownList ID="ddlPerfilLinkedin" runat="server" CssClass="selectBBVA" title="¿Coincide Foto perfil (Empleo vs IFE/INE)? *">
                                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                <asp:ListItem Value="3" Text="No Publico"></asp:ListItem>
                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <br />
        <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top:15%; left:31%; width: 220px;">
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
                            <textarea id="TxtmotivoCan" runat="server" onkeypress="return ValCarac(this.event, 6);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" class="Text" rows="5" cols="1" style="width: 95%"></textarea>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td>
                            <asp:HiddenField runat="server" ID="HiddenField1" />
                        </td>
                        <td align="left" valign="middle">
                            <input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar(id)"/>
                        <br />
                        <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral" />
                        </td>
                    </tr>
                </table>
        </div>
        <div class="tituloConsul">
            <fieldset style="overflow: scroll; height: 120px; overflow-x: hidden">
                <table id="tbValidarObjetos" class="resulGrid"></table>
            </fieldset>
        </div>
    </div>
    <div class="resulbbvaCenter divAdminCatPie">
        <table width="100%" style="height: 100%;">
            <tr>
                <td align="right" valign="middle">
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2" OnClick="btnRegresar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="buttonSecBBVA2" OnClick="btnLimpiar_Click" />
                    <asp:Button runat="server" ID="btnVisor" Text="Visor Documental" CssClass="buttonSecBBVA2" OnClick="btnVisor_Click" />
                    <input type="button" value="Procesar" class="buttonBBVA2" onclick="Replica()" runat="server" id="cmbguardar1C" />
                    <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClientClick="mostrarCanceDiv()" CssClass="buttonSecBBVA2" />
                    <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                </td>
            </tr>
        </table>
    </div>
    <div style="visibility: collapse">
        <asp:Button ID="btnProcesar" runat="server" Text="Procesar" CssClass="buttonBBVA2" OnClick="btnProcesar_Click" />
        <table>
            <tr>
                <td>¿Año de apertura? * </td>
                <asp:HiddenField runat="server" ID="hfFeIni" />
                <td>
                    <input type="text" id="feIni" title="Año de apertura" />

                </td>
            </tr>
        </table>

    </div>
    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdPantalla1" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />
    <asp:HiddenField ID="hdnResultado" runat="server" />
    <asp:HiddenField ID="hdnResultado2" runat="server" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
</asp:Content>
