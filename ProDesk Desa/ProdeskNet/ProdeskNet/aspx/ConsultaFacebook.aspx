<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="ConsultaFacebook.aspx.vb" Inherits="aspx_ConsultaFacebook" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%--BBV-P-423 RQADM-16 JBB 21/03/2017 Pantalla Consulta Portal Facebook.--%>
<%--BBV-PD-37 JBB 24/04/2017 Correciones en la pantalla de redes sociales.--%>
<%--BUG-PD-64 JBEJAR 26/05/2017 Correciones para la validacion de datos.--%>
<%--BUG-PD-74 JBEJAR 07/06/2017 Se agrega la opcion no publico --%>
<%--BUG-PD-80 JBEJAR 09/06/2017 Se elimina duda en la otra pregunta por solicitud del cliente. solo se queda en red social--%>
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BUG-PD-138: JBEJAR 04/07/2017 Se cambia la respuesta no publico a foto de perfil --%>
<%--BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN--%>
<%--BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS PARA ABRIR VISOR TELEPRO--%>
<%--BUG-PD-247 JBEJAR 27/10/2017 SE AGREGA CORREO ELECTRONICO Y VISOR DOCUMENTAL.--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
<%--BUG-PD-416: RHERNANDEZ: 12/04/2018: Se ocultan los botones de cancelar cuando la tarea ya fue procesada--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
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

                var fecha , fecha2 
                if ($('#<%=hfFeIni.ClientID%>').val() != "") {
                    fecha = $('#<%=hfFeIni.ClientID%>').val().split('/')

                    fecha2 = fecha[0] + '/' + fecha[1] + '/' + fecha[2]
                    $('#feIni').val(fecha2);
                }
             
                $('#feIni').attr('disabled', true);
                $('#ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1C').hide();
                $('#btnCancelar').hide();
                $('#btnCancelarNew').hide();
            } else {

                $('#feIni').datepicker(settingsDate1).attr('readonly', 'true').attr('onkeydown', 'return false');
                $('#ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1C').show();
                
                
            }

        }

      
        function Replica() {
            //var fechaIni = document.getElementById('#);
            
            var depende = '';
            var hasError = false;
            var textResult = 'Información de la solicitud:';
            var fech1S = $("#feIni").val();
            $('#<%=hfFeIni.ClientID%>').val(fech1S.toString());
            var red = $('#<%=ddlRedSocial.ClientID%>').val(); 
            var valorI = $(".txtBBVA");
            var Select = $(".selectBBVA");
           
            if (red == -1 || red == 1) {
                $.each(valorI, function () {
                    if ($(this)[0].title != "") {
                        if ($(this).val() == "") {
                            //BUG-PD-16 Adición de propiedad title para mostrar info en la alerta.
                            textResult += "\n + Falta información en el campo " + $(this)[0].title;

                            //textResult += "\nFalta información en el campo " + $(this)[0].id.replace("txt", "");
                            hasError = true;
                        }

                    }
                }
                );
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
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Redes Sociales – Facebook"></asp:Label></legend>
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
             <tr>
                <th class="campos">Correo Electrónico:                    
                </th>
                <th class="campos">
                    <asp:Label ID="LblSCorreo" Font-Bold="true" Font-Underline="true" runat="server">
                    </asp:Label>
                </th>
            </tr>
        </table>
        <br />
        <table class="resulbbvarigth">
            <tr>
                <td colspan="5" align="center">Portal Facebook:&nbsp;&nbsp;&nbsp;<asp:Button ID="btnFacebook" runat="server" Text="Ir a Facebook" CssClass="buttonSecBBVA2" OnClick="btnFacebook_click" /></td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;</td>
            </tr>
            
            <tr>
                <td>¿Existe Red social Facebook? * </td>
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
   
                    <asp:DropDownList ID="ddlEmpleo" runat="server"  title="¿Coincide empleo declarado en solicitud con la red social?">
                        <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                    </asp:DropDownList>
          
           <td>Amigos*</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" MaxLength="4" onkeypress="return ValCarac(event,7);" title="Amigos"/></td>
                <td>&nbsp;</td>
                <td>¿Publicaciones recientes?*</td>
                <td>
                    <asp:DropDownList ID="ddlPublicaciones" runat="server" title="¿Publicaciones recientes?">
                        <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                    </asp:DropDownList></td>

            <td>¿Coincide estado (localización geográfica)?*</td>
                <td>
                    <asp:DropDownList ID="ddlGeoloca" runat="server" title="¿Coincide estado (localización geográfica)?">
                        <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                    </asp:DropDownList></td>
                <td>&nbsp;</td>

          <td>¿Año de apertura? * </td>
                <asp:HiddenField runat="server" ID="hfFeIni" />
                <td>
                    <input type="text" id="feIni"  title="Año de apertura"/>

                </td>

    </div>
    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />
    <asp:HiddenField ID="hdnResultado" runat="server" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
</asp:Content>

