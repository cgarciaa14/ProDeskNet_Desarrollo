<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="manejaDomiciliacion.aspx.vb" Inherits="aspx_manejaDomiciliacion" %>

<%@ MasterType VirtualPath="~/aspx/home.master" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>


<script runat="server">

    Protected Sub btnAlgo_Click(sender As Object, e As EventArgs)

    End Sub
</script>

<%--BBV-P-423: RQADM-23: AVH: 28/03/2017 Se crea pantalla--%>
<%--BBVA-P-423: RQCAINTBAC-02: CGARCIA: 21/04/2017 Se creo una funcion para traer datos de la clave interbancaria--%>
<%--BUG-PD-57 GVARGAS 18/05/2017 Cambios faltas ortográficas --%>
<%--RQCAINTBAC-03: ERODRIGUEZ: 05/06/17  se creo la clase para consulta del servicio TXEMDC--%>
<%--BUG-PD-75 ERODRIGUEZ 06/06/2017 cambios en la validacion --%>
<%--BUG-PD-79 : ERODRIGUEZ 08/06/2017 : se agrego funcion CargaDatosSolicitanteTodos--%>
<%--BUG-PD-85 : ERODRIGUEZ 10/06/2017 : se agrego vañidacion para homoclave--%>
<%--BUG-PD-89 : ERODRIGUEZ 10/06/2017 : se limito el número de cuenta a solo diez digitos y se añadieron 10 ceros al inicio para validar con el servicio--%>
<%--BUG-PD-91 : ERODRIGUEZ 12/06/2017 : Se cambio el orden de los campos, se limpian los datos de cfdi al cambiar de banco seleccionado.--%>
<%--BUG-PD-96 : ERODRIGUEZ 15/06/2017 : Se habilitaron guiones automaticos en cfdi y se permitio no validar cfdi para todos los bancos--%>
<%--BUG-PD-100 : ERODRIGUEZ 15/06/2017 : Se habilitaron guiones automaticos en cfdi y eliminacion de números--%>
<%--BUG-PD-114:MPUESTO:22/06/2017:Modificación de expresiones para validación de formato de CFDI--%>
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BUG-PD-137: ERODRIGUEZ: 03/07/2017: Se cambió validación de clabe interbancaria se validó para permitir 20 números en clabe interbancaria--%>
<%--BUG-PD-140: ERODRIGUEZ: 05/07/2017: Se cambió validación RFC para convertir a mayusculas--%>
<%--BUG-PD-272: MGARCIA: 23/11/2017: Se agrego pantalla DetalleImpagos y su funcionalidad--%>
<%--BUG-PD-290: MGARCIA: 05/12/2017: Detalle Impagos en Menu--%>
<%--'RQ-PD18: CGARCIA: 13/12/2017: SE AGREGA NUMERO DE CLIENTE DESDE WS Y BD EN ENCABEZADO DE INFO --%>
<%--BUG-PD-317: DCORNEJO: 21/12/2017:SE OCULTO EL BOTON "Detalle Impagos" --%> 
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit---%>
<%--BUG-PD-370: ERODRIGUEZ: 26/02/2018: Se cambio validación de RFC para que se compare sin homoclave, Se agrego validacion para no permitir procesar sin número de cliente.--%>



<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">

    <%--<asp:RegularExpressionValidator ID="RegExpVal1" runat="server" ControlToValidate="txtFolioFiscal" ErrorMessage="error"  ValidationExpression="^[0-9]+$" />--%>

    <script type="text/javascript" src="../js/Funciones.js"></script>

    <script type="text/javascript" language="javascript">

        function pageLoad() {

            var bbva = $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddlBanco').val();

            //if (bbva == "017") { $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddlTipoCuenta').val(285).prop("disabled", true); }
            //else { $('#ctl00_ctl00_cphCuerpo_cphPantallas_ddlTipoCuenta').val(0).prop("disabled", false); }


            $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddlBanco").on("change", function () {
                var SelectedValor = this.value;
            });
        }
        ////funcion para agregar guiones de acuerdo a formato de cfdi

        //BUG-PD-114:MPUESTO:22/06/2017:TAG INICIO
        function CFDIformat(evt, objSource) {
            var result = false;
            var charCode = (evt.which) ? evt.which : evt.KeyCode
            result = ValCarac(evt, 22);
            if (result) {
                if ($('#' + objSource.id).val().length == 8 ||
                    $('#' + objSource.id).val().length == 13 ||
                    $('#' + objSource.id).val().length == 18 ||
                    $('#' + objSource.id).val().length == 23) {
                    if (charCode != 8 && charCode != 46) {
                        $('#' + objSource.id).val($('#' + objSource.id).val() + "-");
                    }
                }
                else {
                }
            }
            return result;
        }
        //BUG-PD-114:MPUESTO:22/06/2017:TAG FINAL

        $(document).ready(function () {
            $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddlBanco").on("change", function () {
                var SelectedValor = this.value;
            });

        });

        function FunClave() {

            var txtOrigen = $('#<%=txtClaveInterbancaria.ClientID%>').val();

            var res = txtOrigen.slice(0, 3);

            var Cach = $('#<%= hiddenCach.ClientID%>');

            Cach.val(res)
        }

        function btnProcesarCliente_click() {

            //$("#<%= btnValidar.ClientID%>").click()
            //btnValidar_Click();

            //btnProcesar_Click();
            $('#<%= btnProcesar.ClientID%>').click()
            //btnProcesarCliente_click();


            $('#<%= btnProcesarCliente.ClientID%>').prop('disabled', true);

            return;
        }

        function btnProcesarCliente_Enable() {
            $('#<%= btnProcesarCliente.ClientID%>').prop('disabled', false);
        }

        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();
        }

        function btnValidar_Click() {

            var hasErrorSol = false;
            var hasErrorEje = false;
            var textResult = 'Información de la solicitud:';
            var textErrSol = ['\n\nSección Datos CFDI:', '', '', '', '', ''];
            var textErrEje = ['\n\nSección Datos Bancarios:', '', '', '', ''];

            cfdiInputs = $(".txt3BBVA");
            bankInputs = $(".select2BBVA");

            $.each(txtInputs, function () {
                if ($(this).val() == "" && $(this).attr('extraProperty') != '') {
                    if ($(this).hasClass('cfdiClass')) {
                        hasErrorSol = true;
                        textErrSol[$(this).attr('arrayPosition')] = "\n + Falta información en el campo " + $(this).attr('extraProperty').toString();
                    }
                    else if ($(this).hasClass('bancoClass')) {
                        hasErrorEje = true;
                        textErrEje[$(this).attr('arrayPosition')] = "\n + Falta información en el campo " + $(this).attr('extraProperty').toString();
                    }
                }
                if ($(this)[0].id == 'txtRFCE') {
                    textErrSol[$(this).attr('arrayPosition')] = ValidaRfc($(this).val());
                }
                if ($(this)[0].id == 'txtRFCR') {
                    textErrSol[$(this).attr('arrayPosition')] = ValidaRfc($(this).val());
                }
            });

            $.each(ddlInputs, function () {
                if ($(this).val() == 0) {
                    if ($(this).hasClass('cfdiClass')) {
                        hasErrorSol = true;
                        textErrSol[$(this).attr('arrayPosition')] = "\n + Debe seleccionar un elemento válido en " + $(this).attr('extraProperty').toString();
                    }
                    //else if ($(this).hasClass('ejecutivoClass')) {
                    //    hasErrorEje = true;
                    //    textErrEje[$(this).attr('arrayPosition')] = "\n + Debe seleccionar un elemento válido en " + $(this).attr('extraProperty').toString();
                    //}
                }
            });

            if (hasErrorSol) {
                var textSol = textErrSol.join()
                textResult += textSol;
            }
            if (hasErrorEje) {
                var textEje = textErrEje.join()
                textResult += textEje;
            }
            if (!hasErrorSol && !hasErrorEje) {

                //aspButtonAltaSEVA.click();
            }
            else {
                alert(textResult);
            }


        }

        function ValidaRfc(rfcStr) {
            var strCorrecta;
            strCorrecta = rfcStr;
            if (rfcStr.length != 10 && rfcStr.length != 13) {
                return '\n + El RFC debe ser de 10 o 13 caracteres.';
            } else {
                if (!RFC.match(/^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))((-)?([A-Z\d]{3}))?$/)) {
                    if (RFC.length == 10) {
                        return '\n + El RFC debe tener formato "AAAA999999".';
                    }
                    else {
                        return '\n + El RFC debe tener formato "AAAA999999***".';
                    }
                }
            }
            return '';
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
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 46, 1, txtmotivoOb)

            } else if (boton == "btnGuardarCancelar") {
                //debugger;
                var txtUsu = $('[id$=txtusua]').val();
                var txtpsswor = $('[id$=txtpass]').val();
                var idpantalla = $("[id$=hdPantalla] ").val()
                var txtmotivoOb = $("[id$=TxtmotivoCan]").val()
                $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)

            }
        }

        //BUG-PD-114:MPUESTO:22/06/2017:TAG INICIO
        function upperCaseString(objSource) {
            $('#' + objSource.id).val($('#' + objSource.id).val().toUpperCase());
            var charArr = [];
            var strText = $('#' + objSource.id).val();
            for (index = 0; index < strText.length; index++) {
                if (strText.charAt(index) != '-') {
                    charArr.push(strText.charAt(index));
                    if (charArr.length == 8 || charArr.length == 13 || charArr.length == 18 || charArr.length == 23)
                        charArr.push('-');
                }
            }
            if (charArr[charArr.length - 1] == '-') {
                charArr.splice(charArr.length - 1, 1)
            }
            $('#' + objSource.id).val(charArr.join(''));        
        }
        //BUG-PD-114:MPUESTO:22/06/2017:TAG FINAL

        function upperCaseS(objSource) {
            $('#' + objSource.id).val($('#' + objSource.id).val().toUpperCase());
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
                                    <asp:Label ID="lblNomPantalla" runat="server" Text="Mesa Ins. Edo. Cta "></asp:Label>
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
                <%--'RQ-PD18: CGARCIA: 13/12/2017: SE AGREGA NUMERO DE CLIENTE DESDE WS Y BD EN ENCABEZADO DE INFO --%>
                <th class="campos" style="width: 25%">Número de cliente:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </th>
                <th class="campos" style="width: 25%">
                    <asp:Label ID="lblNumCliente" runat="server" Font-Underline="true"></asp:Label>
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

        <div id="Div1" class="resulbbvaCenter" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblBanco" Text="Banco*"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlBanco" CssClass="select2BBVA" OnSelectedIndexChanged="ddlBanco_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblTipoCuenta" Text="Tipo de Cuenta*"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlTipoCuenta" CssClass="select2BBVA" OnSelectedIndexChanged="ddlTipoCuenta_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnLigarCuenta" Text="Validar Cuenta" CssClass="buttonSecBBVA2" OnClick="btnLigarCuenta_Click" Visible="false" />
                    </td>
                </tr>
                <tr runat="server" id="trTitular" visible="false">
                    <td>
                        <asp:Label runat="server" ID="lblTitular" Visible="false" Text="Titular Cuenta"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblTitularCuenta" Text=""></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="trRFC" visible="false">
                    <td>
                        <asp:Label runat="server" ID="Lbl1" Visible="false" Text="RFC"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label runat="server" ID="LblRFC" Text=""></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td>
                        <%--'RQ-PD18: CGARCIA: 13/12/2017: SE AGREGA NUMERO DE CLIENTE DESDE WS Y BD EN ENCABEZADO DE INFO --%>
                        <asp:Label runat="server" ID="lblNumeroCuenta" Text="Número de Cuenta"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNumeroCuenta" onPaste="return false" onCut="return false" onCopy="return false" onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);" CssClass="txt3BBVA" Enabled="false" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNumClienteWS" Text="Número de Cliente"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNumClienteWS" CssClass="txt3BBVA" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNumeroTarjeta" Text="Tarjeta de Debito"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNumeroTarjeta" onPaste="return false" onCut="return false" onCopy="return false" onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);" CssClass="txt3BBVA" Enabled="false" MaxLength="16"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblClaveInterbancaria" Text="Clabe Interbancaria"></asp:Label>
                    </td>
                    <td>
                        <%-- <asp:TextBox runat="server" ID="txtClaveInterbancaria" CssClass="txt3BBVA" Enabled="true" MaxLength="18"  AutoPostBack="true" ></asp:textbox>    --%>
                        <asp:TextBox runat="server" ID="txtClaveInterbancaria" onPaste="return false" onCut="return false" onCopy="return false" onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);" CssClass="txt3BBVA" Enabled="false" MaxLength="20" onblur="return FunClave()" AutoPostBack="true"></asp:TextBox>

                    </td>
                    <td></td>
                    <td></td>

                    <td>
                        <asp:Button runat="server" ID="btnVisor" Text="Visor Documental" CssClass="buttonSecBBVA2" OnClick="btnVisor_Click" />
                    </td>
                </tr>
            </table>

            <div style="height: 50px"></div>
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label2" Text="RFC EMISOR *"></asp:Label>
                    </td>
                    <td style="width: 30% !important">
                        <input type="text" runat="server" onpaste="return false" oncut="return false" oncopy="return false" class="txt3BBVA cfdiClass" id="txtRFCE" onkeypress="return ValCarac(event,22);" onblur="upperCaseS(this);" maxlength="13" />
                    </td>
                    <td></td>
                    <td>
                        <asp:Label runat="server" ID="Label3" Text="RFC RECEPTOR *"></asp:Label>
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtRFCR" class="txt3BBVA cfdiClass" onpaste="return false" oncut="return false" oncopy="return false" onkeypress="return ValCarac(event,22);" onblur="upperCaseS(this);" maxlength="13" />
                    </td>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnValidar" Text="Validar CFDI" CssClass="buttonSecBBVA2" OnClick="btnValidar_Click" />
                    </td>

                </tr>

                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblFolioFiscal" Text="Folio Fiscal *"></asp:Label>
                    </td>
                    <td colspan="2">
                        <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">   </asp:ToolkitScriptManager>--%>
                        <input type="text" runat="server" id="txtFolioFiscal" style='width: 90% !important;' class="txt3BBVA cfdiClass" onkeypress="return CFDIformat(event, this);" onkeyup="setNewText(event, this);" onblur="upperCaseString(this);" maxlength="36" />
                        <%--<asp:TextBox runat="server" onPaste="return false" onCut="return false" onCopy="return false" ID="txtFolioFiscal"  style="width:90% !important;" CssClass="txt3BBVA" MaxLength="36"></asp:TextBox>--%>
                        <%-- <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox1" Mask="99/99/9999" MaskType="Date" AcceptNegative="None" />--%>
                    
                    </td>

                    <td>
                        <asp:Label runat="server" ID="Label4" Text="Cantidad Total *"></asp:Label>
                    </td>
                    <td>
                        <input type="text" runat="server" onpaste="return false" oncut="return false" oncopy="return false" id="txtTotal" class="txt3BBVA cfdiClass" onkeypress="return ValCarac(event,9);" maxlength="13" />
                    </td>

                </tr>


                </table>
        </div>









        <table id="tbValidarObjetos" class="resulGrid">
        </table>

        <div class="resulbbvaCenter divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td>
                        <%--<asp:Button runat="server" ID="btnRegresar" text="Regresar"   CssClass="buttonSecBBVA2" />--%>
                        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" OnClick="btnRegresar_Click" />
                        <input type="button" runat="server" id="btnProcesarCliente" value="Procesar" onclick="btnProcesarCliente_click();" class="buttonBBVA2" />
                        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClientClick="mostrarCanceDiv()" CssClass="buttonSecBBVA2" />
                        <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                        <asp:Button runat="server" ID="btnDetalleImpagos" Text="Detalle Impagos" CssClass="buttonSecBBVA2" Visible="false" Style="position: relative; top: 0px; left: 12px; width: 127px" />
                        <%-- <asp:Button runat="server" ID="Button1" Text="Detalle Impagos" CssClass="buttonSecBBVA2" OnClick="btnDetalleImpagos_Click" style="position: relative; top: 0px; left: 12px; width: 127px" /> --%>
                    </td>
                </tr>
            </table>
        </div>

        <div id="divHiddenButton" style="visibility: collapse">
            <asp:Button runat="server" ID="btnProcesar" Text="Procesar" OnClick="btnProcesar_Click" />
        </div>

        <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top: 15%; left: 31%; width: 220px;">
            <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID="btnCancelar" PopupControlID="popCancela" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
            <asp:Panel ID="popCancela" runat="server" CssClass="cajadialogo ">--%>
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
                        <input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" onclick="btnGuardar(id)" />
                            <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral" />
                        </td>
                    </tr>
                </table>
            <%--</asp:Panel>--%>
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
    <asp:HiddenField ID="hiddenCach" runat="server" />
    <asp:HiddenField ID="HiddenRFC" runat="server" />
    <asp:HiddenField ID="HiddenHomoclave" runat="server" />
    <asp:HiddenField ID="HiddenRFCCompleto" runat="server" />
    <asp:HiddenField ID="hdnResultado3" runat="server" />



</asp:Content>

