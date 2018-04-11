<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="ConsultaINE.aspx.vb" Inherits="aspx_ConsultaINE" %>

<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server"> 
<!--BBV-P-423-RQADM-08 JBEJAR 04/04/2017 Pantalla Consulta INE-->
<!--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar-->
<%--BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN--%>
<%--BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS PARA ABRIR VISOR TELEPRO--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
    <script src="../js/jquery.ui.widget.js"></script>
    <script src="../js/jquery.iframe-transport.js"></script>
    <script src="../js/jquery.fileupload.js"></script>
    <script src="../js/Funciones.js"></script>
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

            $('#ventanaContain').hide();
            $('#divcancela').hide();

            if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
            if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
            if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

            btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)
        }

        function pageLoad() {


            var habilitado = getParameterByName('Enable');

            if (habilitado == 1) {

                $('#ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1C').hide();
            } else {


                $('#ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1C').show();


            }

        }

        function Replica() {
            //var fechaIni = document.getElementById('#);
            var depende = '';
            var hasError = false;
            var textResult = 'Información de la solicitud:';
            var valorI = $(".txtBBVA");
            var Select = $(".selectBBVA");
            var Red = $('#<%=ddlRedSocial.ClientID%>').val()
            if (Red == 1 || Red == -1) {
                $.each(Select, function () {
                    if ($(this)[0].title != "" && $(this).val() == -1) {
                        //BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.
                        textResult += "\n + Debe seleccionar un elemento en " + $(this)[0].title;
                        //textResult += "\nFalta infromación en el campo " + $(this)[0].id.replace("txt", "");
                        hasError = true;
                    }
                    else {
                        depende += $(this)[0].id + ',';
                    }
                });
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


        /*  Date.prototype.yyyymmdd = function () {
              var mm = this.getMonth() + 1; // getMonth() is zero-based
              var dd = this.getDate();
    
              return [(dd > 9 ? '' : '0') + dd,
                      (mm > 9 ? '' : '0') + mm,
                      this.getFullYear()
              ].join('/');
          };
          */

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
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Consulta INE"></asp:Label></legend>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

        <br />
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
        <table class="resulbbvarigth">
            <tr>
                <td colspan="5" align="center">Ingresar al Portal Lista Nominal:&nbsp;&nbsp;&nbsp;<asp:Button ID="btnLinkedin" runat="server" Text="Portal Lista Nominal" CssClass="buttonSecBBVA2" OnClick="btnLinkedin_click" /></td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>

            <tr>
                <td>¿Existe el INE del solicitante? * </td>
                <td>
                    <asp:DropDownList ID="ddlRedSocial" runat="server" CssClass="selectBBVA" OnSelectedIndexChanged="ddlRedSocial_select_index" AutoPostBack="true" title="¿Existe el INE del solicitante?*">
                        <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        <asp:ListItem Value="2" Text ="No aplica"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Página no disponible"></asp:ListItem>
                    </asp:DropDownList></td>
                <td>&nbsp;</td>
                <td>Estatus Actual de INE</td>
                <td>
                    <asp:DropDownList ID="ddlGeoloca" runat="server" CssClass="selectBBVA" title="Estatus Actual de INE">
                        <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Vigente"></asp:ListItem>
                        <asp:ListItem Value="2" Text="No Vigente"></asp:ListItem>
                        <asp:ListItem Value="3" Text="No está Vigente (No concluiste tu tramite)"></asp:ListItem>
                        <asp:ListItem Value="4" Text="No está Vigente (No es tu última credencial)"></asp:ListItem>
                        <asp:ListItem Value ="5" Text="No está Vigente (Tiene recuadro 03)"></asp:ListItem>
                        <asp:ListItem Value ="6" Text="No está Vigente (Por Autoridad)"></asp:ListItem>
                        <asp:ListItem Value ="7" Text="No está Vigente (Registro reincorporado por registro de autoridad)"></asp:ListItem>
                        <asp:ListItem Value ="8" Text="OCR ingresado distintito al registrado"></asp:ListItem>
                        <asp:ListItem Value ="9" Text="No está Vigente (excluido de la Lista Nominal)"></asp:ListItem>
                        <asp:ListItem Value ="10" Text="No está vigente (por corrección de datos)"></asp:ListItem>
                        <asp:ListItem Value="11" Text ="Datos incorrectos o inexistentes"></asp:ListItem>
                        <asp:ListItem Value="12" Text="No está vigente (suspensión de derechos)"></asp:ListItem>
                        <asp:ListItem Value="13" Text="No está Vigente (por defunción)"></asp:ListItem>
                        <asp:ListItem Value="14" Text="No está Vigente (Existe otro registro a tu nombre)"></asp:ListItem>
                        <asp:ListItem Value="15" Text="No está vigente (tu registro presenta datos irregulares)"></asp:ListItem> 
                    </asp:DropDownList></td>

            </tr>
           

        </table>
        <br />
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
                        <textarea id="TxtmotivoCan" runat="server" onkeypress="return ValCarac(this.event, 6);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" class="Text" rows="5" cols="1" style="width: 95%"></textarea>
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
                <td align="right" valign="middle">
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2" OnClick="btnRegresar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="buttonSecBBVA2" OnClick="btnLimpiar_Click" />
                    <input type="button" value="Procesar" class="buttonBBVA2" onclick="Replica()" runat="server" id="cmbguardar1C" />
                    <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClientClick="mostrarCanceDiv()" CssClass="buttonSecBBVA2" />
                    <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                </td>
            </tr>
        </table>
    </div>
    <div style="visibility: collapse">
        <asp:Button ID="btnProcesar" runat="server" Text="Procesar" CssClass="buttonBBVA2" OnClick="btnProcesar_Click" />
    </div>
    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />
    <asp:HiddenField ID="hdnResultado" runat="server" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
</asp:Content>
