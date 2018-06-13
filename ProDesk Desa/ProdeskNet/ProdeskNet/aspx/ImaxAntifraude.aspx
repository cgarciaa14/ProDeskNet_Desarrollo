<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="ImaxAntifraude.aspx.vb" Inherits="aspx_ImaxAntifraude" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<%--RQ-PD21: JMENDIETA: 13/02/2018: Se crea el aspx con la fusión de Consulta Imax y Antifraude Basico Riesgo--%>
<%--RQ-PD21-2: JMENDIETA: 27/02/2018: Para el grid de antifraude se agrega columna de impagos y se fusiona el nombre del cliente en una sola--%>
<%--RQ-PD21-3: JMENDIETA: 05/03/2018: Modificacion para no sobreponer los controles de operación (Regresar,Procesar,Cancelar) sobre el resultado de antifraude--%>
<%--BUG-PD-384 DCORNEJO 08/03/2018: Se agrega Monto Financiero--%>
<%--BUG-PD-423: CGARCIA: 23/04/2018: SE MANDA A BACK LA ACTUALIZACION DE STATUS DE LOS DOCUMENTOS.--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
    <script type="text/javascript" src="../js/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="../js/jquery.iframe-transport.js"></script>
    <script type="text/javascript" src="../js/jquery.fileupload.js"></script>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" language="javascript">
        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();

        }
        function fnMuestraChecks() {
            var cheks = $("[name^=chkbxRec]");
            var count = 0;

            cheks.each(function () {
                if ($(this).is(':checked')) {
                    var arrChk = new Array()
                    arrChk = $(this)[0].name.split('_');
                    var hedrec = $('#selMR' + arrChk[1]);
                    hedrec.show('fast', "swing");
                    hedrec.addClass('Text');

                    count++;
                }
                else {
                    var arrChk = new Array()
                    arrChk = $(this)[0].name.split('_');
                    var hedrec = $('#selMR' + arrChk[1]);
                    hedrec.hide('fast', "swing");
                    hedrec.addClass('Text');
                }
            })

            if (count > 0) {
                $('[id$=MovRec]').show('fast');
            }
            else {
                $('[id$=MovRec]').hide('fast');
            }
        }
        function fnChequea() {
            fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');
            PopUpLetrero("Documento procesado exitosamente");
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
                if (txtpsswor == '') { alert('El campo del passwird esta vacía'); return; }
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

        function fnCambiaStatus(check, id) {
            //BUG-PD-423: CGARCIA: 23/04/2018: SE MANDA A BACK LA ACTUALIZACION DE STATUS DE LOS DOCUMENTOS.
            var docType = $(check).parent().parent().find('td:nth-child(2):first').text();
            var folio = $('[id$=hdSolicitud]').val();
            var Check_VAL = check.name.indexOf('Val');
            var Check_REC = check.name.indexOf('Rec');
            var checked = check.checked

            fnMuestraChecks()

            if (check.name.indexOf('Val') != -1) {
                if (check.checked == true) {
                    var settings = {
                        type: "POST", url: "consultaPantallaDocumentos.aspx/getBack", async: false,
                        data: "{'id': '" + id + "', 'docType': '" + docType + "', 'folio': '" + folio + "', 'Check_VAL': '" + Check_VAL + "', 'Check_REC': '" + Check_REC + "', 'Checked': '" + 1 + "'}",
                        contentType: "application/json; charset=utf-8", dataType: "json",
                        success: function OnSuccess_showDetails(response) {
                            $('[name=chkbxRec_' + id + ']').attr('checked', false);
                            fnMuestraChecks()
                        }
                    };
                    $.ajax(settings);
                }
                else {
                    var settings = {
                        type: "POST", url: "consultaPantallaDocumentos.aspx/getBack", async: false,
                        data: "{'id': '" + id + "', 'docType': '" + docType + "', 'folio': '" + folio + "', 'Check_VAL': '" + Check_VAL + "', 'Check_REC': '" + Check_REC + "', 'Checked': '" + 2 + "'}",
                        contentType: "application/json; charset=utf-8", dataType: "json",
                        success: function OnSuccess_showDetails(response) { }
                    };
                    $.ajax(settings);
                }
            }

            if (check.name.indexOf('Rec') != -1) {
                if (check.checked == false) {
                    var settings = {
                        type: "POST", url: "consultaPantallaDocumentos.aspx/getBack", async: false,
                        data: "{'id': '" + id + "', 'docType': '" + docType + "', 'folio': '" + folio + "', 'Check_VAL': '" + Check_VAL + "', 'Check_REC': '" + Check_REC + "', 'Checked': '" + 3 + "'}",
                        contentType: "application/json; charset=utf-8", dataType: "json",
                        success: function OnSuccess_showDetails(response) { }
                    };
                    $.ajax(settings);
                }
                else {
                    $('[name=chkbxVal' + id + ']').attr('checked', false);
                    var settings = {
                        type: "POST", url: "consultaPantallaDocumentos.aspx/getBack", async: false,
                        data: "{'id': '" + id + "', 'docType': '" + docType + "', 'folio': '" + folio + "', 'Check_VAL': '" + Check_VAL + "', 'Check_REC': '" + Check_REC + "', 'Checked': '" + 4 + "'}",
                        contentType: "application/json; charset=utf-8", dataType: "json",
                        success: function OnSuccess_showDetails(response) { }
                    };
                    $.ajax(settings);
                }
            }
        }
        //function fnCambiaStatus(check, id) {
        //    var docType = $(check).parent().parent().find('td:nth-child(2):first').text();
        //    var folio = $('[id$=hdSolicitud]').val();
        //    debugger;
        //    fnMuestraChecks()

        //    if (check.name.indexOf('Val') != -1) {
        //        if (check.checked == true) {
        //            btnEntrevista('update PDK_REL_PAN_DOC_SOL set PDK_ST_VALIDADO = 1, PDK_ST_RECHAZADO = 0, PDK_PAR_SIS_PARAMETRO_DIG = 0 where PDK_ID_DOC_SOLICITUD = ' + id + '; exec spValidaEstatusDoc ' + id + '; DELETE FROM PDK_NOTIFICACIONES WHERE PDK_ID_DOCUMENTOS IN ( SELECT PDK_ID_DOCUMENTOS FROM PDK_CAT_DOCUMENTOS WHERE PDK_DOC_NOMBRE = \'' + docType + '\' ) AND PDK_ID_SECCCERO = ' + folio + ';');
        //            $('[name=chkbxRec_' + id + ']').attr('checked', false);
        //            fnMuestraChecks()
        //        }
        //        else {
        //            btnEntrevista('update PDK_REL_PAN_DOC_SOL set PDK_ST_VALIDADO = 0 where PDK_ID_DOC_SOLICITUD = ' + id + '; exec spValidaEstatusDoc ' + id);
        //        }
        //    }
        //    if (check.name.indexOf('Rec') != -1) {
        //        if (check.checked == false) {
        //            btnEntrevista("UPDATE PDK_REL_PAN_DOC_SOL SET PDK_ST_RECHAZADO = 0 WHERE PDK_ID_DOC_SOLICITUD = " + id + '; exec spValidaEstatusDoc ' + id);
        //        }
        //        else {

        //            $('[name=chkbxVal' + id + ']').attr('checked', false);
        //        }
        //    }
        //}
        function fnInsertaRechazo(elemento, DOC_SOLICITUD) {
            var chkMR = $('[name $= chkbxRec_' + elemento.id + ']');
            var ddlMR = $('[name $= ddlMR' + elemento.id + '] option:selected');
            var txtMR = $('[name $= txtNotRec' + elemento.id + ']');
            var u = $('[id$=hdusuario]').val();
            var sol = $('[id$=hdSolicitud]').val();
            var valchkMR = 0;

            if (chkMR.val() == 'on') { valchkMR = 1 } else { valchkMR = 0 }

            if (ddlMR.val() == '') {
                ddlMR.val(0);
            }

            if (txtMR.val() == '') {
                btnEntrevista("UPDATE PDK_REL_PAN_DOC_SOL SET PDK_ST_RECHAZADO = " + valchkMR + ",	PDK_ST_VALIDADO = 0, PDK_PAR_SIS_PARAMETRO_DIG = " + ddlMR.val() + ", fcNotificacion='' WHERE PDK_ID_DOC_SOLICITUD = " + DOC_SOLICITUD + "; exec sp_InsNotDig " + sol + ", " + u + ", ''; exec spValidaEstatusDoc " + DOC_SOLICITUD + ";");
            }
            else {
                btnEntrevista("UPDATE PDK_REL_PAN_DOC_SOL SET PDK_ST_RECHAZADO = " + valchkMR + ",	PDK_ST_VALIDADO = 0, PDK_PAR_SIS_PARAMETRO_DIG = " + ddlMR.val() + ",fcNotificacion='" + txtMR.val() + "' WHERE PDK_ID_DOC_SOLICITUD = " + DOC_SOLICITUD + "; exec sp_InsNotDig " + sol + ", " + u + ", '" + txtMR.val() + "'; exec spValidaEstatusDoc " + DOC_SOLICITUD + ";");
            }
        }

        function ddlMRover(elemento) {
            drop = document.getElementById(elemento.id);
            if (drop.options[drop.selectedIndex].text.val != "") {
                drop.title = drop.options[drop.selectedIndex].text;
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

        $(document).ready(function () { $.urlParam = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } }; if ($.urlParam("Enable").toString() == "1") { $("#btnCancelarNew").hide(); }; });

    </script>

    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnCancelar
        {
            display: none;
        }
    </style>


    <div class="divPantConsul">


        <div style="display: table; width: 100%;">
            <div style="display: table-row; width: 100%;">
                <!-- CLIENTE-->
                <div>
                    <div class="fieldsetBBVA">
                        <table>
                            <tr class="tituloConsul">
                                <td width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="width: 70%;">
                                                <legend>IMAX - Antifraude Básico Riesgo</legend>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
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
                </div>
            </div>
        </div>



        <div style="display: table; width: 100%;">
            <div style="display: table-row; width: 100%;">
                <!-- Consulta IMAX-->
                <div>
                    <!-- <legend>Consulta IMAX</legend>
                    <div class="fieldsetBBVA">
                        <table>
                            <tr class="tituloConsul">
                                <td width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="width: 70%;">
                                                <legend>Consulta IMAX</legend>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                         -->
                    <div id="dvCuerpo">

                        <br />
                        <table class="resulbbvarigth">
                            <tr>
                                <td>Numero de Cliente:
                                </td>
                                <td>
                                    <asp:TextBox ID="lblnocliente" runat="server" CssClass="txt2BBVA" Enabled="false"></asp:TextBox>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>Tipo de Identificación * </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoID" runat="server" CssClass="selectBBVA">
                                        <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Credencial de Elector"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Pasaporte"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Cedula Profesional"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="FM2/FM3 para extranjeros"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Cliente no disponible"></asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>&nbsp;</td>
                                <td>Foto y firmas iguales *</td>
                                <td>
                                    <asp:DropDownList ID="ddlfotoyfirma" runat="server" CssClass="selectBBVA">
                                        <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                        </table>
                        <br />
                        <div style="margin-top: 2%">
                            <table id="tbValidarObjetos" class="resulGrid"></table>
                        </div>
                    </div>
                </div>
                <!-- Antifraude Basico Riesgo-->
                <div style="margin-top: 2%">
                    <div class="fieldsetBBVA">
                        <table>
                            <tr class="tituloConsul">
                                <td width="100%">
                                    <!--
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" style="width: 70%;">
                                                <legend>Antifraude Básico Riesgo.</legend>
                                            </td>
                                        </tr>
                                    </table>
                                    -->
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="popup" class="resulbbvaCenter" style="max-height: 225px; overflow-y: scroll; top: 22%; text-align: center">
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataText="No se encontro información." HorizontalAlign="Center" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="Núm Cliente" DataField="Cta_BBVA"></asp:BoundField>
                                <asp:BoundField HeaderText="Nombre Cliente" DataField="Nombre_Cliente"></asp:BoundField>
                                <asp:BoundField HeaderText="RFC" DataField="RFC"></asp:BoundField>
                                <asp:BoundField HeaderText="Fecha de Nacimiento" DataField="FECHA_NAC"></asp:BoundField>
                                <asp:BoundField HeaderText="Dirección Completa" DataField="Domicilio"></asp:BoundField>
                                <asp:BoundField HeaderText="Score de Riesgo" DataField="Riesgo"></asp:BoundField>
                                <asp:BoundField HeaderText="Impagos" DataField="Impago"></asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>No se encontro información.</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <div style="margin-top: 2%">
                        <table id="Table2" width="100%" class="fieldsetBBVA">
                            <tbody>
                                <tr>
                                    <th class="campos" style="width: 56%;"></th>
                                    <th class="campos" style="width: 12%;">Monto Financiero:</th>
                                    <th class="campos" style="width: 6%;">
                                        <asp:TextBox ID="txtMonto" runat="server" CssClass="txt2BBVA" ReadOnly="true" />
                                    </th>
                                    <th class="campos" style="width: 30%;">
                                        <asp:Label runat="server" ID="lbl_CVEBBVA" /></th>
                                    <th class="campos" style="width: 8%;">Riesgo Total:</th>
                                    <th class="campos" style="width: 6%;">
                                        <asp:TextBox ID="txtRiesgo" runat="server" CssClass="txt2BBVA" ReadOnly="true" />
                                    </th>
                                    <th class="campos" style="width: 8%;">Total Impagos:</th>
                                    <th class="campos" style="width: 6%;">
                                        <asp:TextBox ID="txtImpago" runat="server" CssClass="txt2BBVA" ReadOnly="true" />
                                    </th>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="resulbbvaCenter">
                    <table width="100%" style="height: 100%;">
                        <tr>
                            <td align="right" valign="middle">
                                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2" />
                                <asp:Button ID="btnProcesar" runat="server" Text="Procesar" CssClass="buttonBBVA2" />
                                <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClientClick="mostrarCanceDiv()" CssClass="buttonSecBBVA2" />
                                <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                            </td>
                        </tr>
                    </table>
            </div>
            </div>
        </div>



    </div>


    <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top: 15%; left: 31%; width: 220px;">
        <div class="tituloConsul">
            <asp:Label ID="Label1" runat="server" Text="Cancelación" />
        </div>
        <table width="100%">
            <tr>
                <td class="campos">Usuario:</td>
                <td>
                    <asp:TextBox ID="txtusua" SkinID="txtGeneral" MaxLength="12" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="campos">Password:</td>
                <td>
                    <asp:TextBox ID="txtpass" runat="server" SkinID="txtGeneral" MaxLength="12" TextMode="Password" EnableTheming="true"></asp:TextBox>
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



    <%--    <div class="resulbbvaCenter divAdminCatPie">
        <table width="100%" style="height: 100%;">
            <tr>
                <td align="right" valign="middle">
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2" />
                    <asp:Button ID="btnProcesar" runat="server" Text="Procesar" CssClass="buttonBBVA2" />
                    <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClientClick="mostrarCanceDiv()" CssClass="buttonSecBBVA2" />
                    <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                </td>
            </tr>
        </table>
    </div>--%>
    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />
    <asp:HiddenField ID="hdnResultado" runat="server" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />




</asp:Content>

