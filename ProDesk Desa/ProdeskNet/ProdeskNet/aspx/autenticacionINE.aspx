<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="autenticacionINE.aspx.vb" Inherits="aspx_autenticacionINE" %>

<%--BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)--%>
<%--BUG-PD-149:MPUESTO:11/07/2017:Mejoras en la interfaz y decodificacion de imagenes, correcciones de botones--%>
<%--BUG-PD-222 GVARGAS 05/10/2017 Mejoras captura INE--%>
<%--BBV-P-423 RQ-PD-17 1 GVARGAS 22/12/2017 Mejoras carga huella --%>
<%--BBV-P-423 RQ-PD-17 2 GVARGAS 02/01/2018 Mejoras carga huella --%>
<%--BBV-P-423 RQ-PD-17 3 GVARGAS 08/01/2018 Mejoras carga huella y cambio payload --%>
<%--BBV-P-423 RQ-PD-17 8 GVARGAS 29/01/2018 cambio payload envio --%>
<%--BBV-P-423 RQ-PD-17 13 GVARGAS 12/02/2018 Correciones --%>
<%--BBV-P-423 RQ-PD-17 15 GVARGAS 16/02/2018 New reader methotd --%>
<%--BBV-P-423 RQ-PD-17 16 GVARGAS 26/02/2018 Cambios correcion AJAX Tool Kit --%>
<%--URGENTE GVARGAS 07/03/2018  Oculto boton --%>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
    <script type="text/javascript">
        var llave_ = '';

        function btnGeneraArchivoCliente_Click() {
            $('#<%=btnGeneraArchivo.ClientID%>').click();
        }

        function btnConsultarHuella_Click() {
            $('#<%=btnConsultaHuella.ClientID%>').click();
        }

        function SaveFinger(WQS, IMG) {
            var settings = { type: "POST", url: "", async: false, data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
            settings.url = "autenticacionINE.aspx/saveFinger";
            settings.data = "{'WQS': '" + WQS.toString() + "', 'IMG': '" + IMG.toString() + "', 'idPant': '" + $.urlParam("idPantalla").toString() + "', 'nSol': '" + $.urlParam("sol").toString() + "'}";
            settings.success = OnSuccessSaveFinger;
            settings.failure = function (response) { alert("Error al cargar generar archivo."); }
            $.ajax(settings);
        }

        function OnSuccessSaveFinger(response) {
            //var items = $.parseJSON(response.d.toString());
        }

        function INEInformation() {
            $('#legendCarrusel').html('');
            $('#ventanaContain').hide();
            $('#divDetails').hide();
            $("#tableFields tbody").empty();

            alert("Carga completa.");

            var settings = { type: "POST", url: "", async: false, data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
            settings.url = "autenticacionINE.aspx/getInformationINE";
            settings.data = "{'nSol': " + $.urlParam("sol").toString() + ", 'idPant': " + $.urlParam("idPantalla").toString() + ", 'key': '" + llave_ + "'}";
            settings.success = OnSuccessINEInformation;
            settings.failure = function (response) { alert("Error al cargar generar archivo."); }
            $.ajax(settings);
        }

        function OnSuccessINEInformation(response) {
            var items = $.parseJSON(response.d.toString());

            if (items.opStatus == "OK") {
                $('#legendCarrusel').html('');
                $('#ventanaContain').hide();
                $('#divDetails').hide();
                $("#tableFields tbody").empty();
                $("#ctl00_ctl00_cphCuerpo_cphPantallas_imgFingerprint").attr("src", items.Fingerprint);
                $('#ctl00_ctl00_cphCuerpo_cphPantallas_btnbtnProcesar_Nuevo').prop("disabled", false);
            } else {
                $("#mensaje_").html(items.message.toString());
            }
        }

        function ReadFinger() {
            $.support.cors = true;
            //$.blockUI();
            Procesando();
            $.ajax({
                cache: false,
                url: 'https://localhost:3448/cid/Captura',
                type: 'GET',
                contentType: "application/json; charset=UTF-8",
                dataType: 'json',
                crossDomain: true,
                error: function (request, status, error) {
                    var message_ = error.toString()
                    if (request.readyState == 0) { message_ = "ERR_CONNECTION_REFUSED"; }
                    ProcesandoHide(message_);
                },
                success: function (datatWSQ) {
                    $("#bfWsq").val(datatWSQ.imagenWSQ64);
                    $("#bfImagen64").val(datatWSQ.imagenDispositivo64);
                    if (datatWSQ.imagenDispositivo64 != "") {
                        var imgSrc = "data:image/bmp;base64," + datatWSQ.imagenDispositivo64;

                        $("#ctl00_ctl00_cphCuerpo_cphPantallas_imgFingerprint").show().attr("src", imgSrc);
                        $('#ctl00_ctl00_cphCuerpo_cphPantallas_btnbtnProcesar_Nuevo').prop("disabled", false);
                                               
                        SaveFinger(datatWSQ.imagenWSQ64.toString(), datatWSQ.imagenDispositivo64.toString());
                    }

                    if (datatWSQ.codigoRespuesta >= 1) {
                        $("#errEncabezado").text(datatWSQ.codigoRespuesta + " : " + datatWSQ.mensajeDispositivo);
                    }
                    ProcesandoHide("Lectura Completa.");
                }/*,
                complete: function (data) { $.unblockUI(); ProcesandoHide("Lectura Completa.");
                }*/
            });
        }

        function FNGenerator() {
            // Firefox 1.0+
            var isFirefox = typeof InstallTrigger !== 'undefined';
            // Internet Explorer 6-11
            var isIE = /*@cc_on!@*/false || !!document.documentMode;
            // Chrome 1+
            var isChrome = !!window.chrome && !!window.chrome.webstore;

            var browser = "N/A";

            var link = window.location.href.toString().split("?");
            var link_ = link[0].toString().replace("autenticacionINE", "inicio");

            if (isFirefox) { browser = "FF"; }
            else if (isIE) { browser = "IE"; }
            else if (isChrome) { browser = "CR"; }

            var nameLocation = $(location).attr('pathname').split("/");
            var url = $(location).attr('href').split(nameLocation[nameLocation.length - 2]);

            var settings = { type: "POST", url: "", async: false, data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
            settings.url = "autenticacionINE.aspx/FNGenerator";
            settings.data = "{'idPant': '" + $.urlParam("idPantalla").toString() + "', 'nSol': '" + $.urlParam("sol").toString() + "', 'url': '" + link_ + "', 'Browser': '" + browser + "'}";
            settings.success = OnSuccessGeneratorFNG;
            settings.failure = function (response) { alert("Error al cargar generar archivo."); }
            $.ajax(settings);
        }

        function OnSuccessGeneratorFNG(response) {
            var items = $.parseJSON(response.d.toString());

            if (items.messageCode == "OK") {
                llave_ = items.name;

                var element = document.createElement('a');
                element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(items.information.toString()));
                element.setAttribute('download', 'Folio_' + $.urlParam("sol").toString() + '.fng');

                element.style.display = 'none';
                document.body.appendChild(element);

                element.click();

                document.body.removeChild(element);

                $('#legendCarrusel').html(items.messageTitle.toString());
                $('#ventanaContain').show();
                $('#divDetails').show();
                $("#mensaje_").html(items.message.toString());
            } else if (items.messageCode == "EF") {
                PopUpLetreroRedirect(items.message, "../aspx/consultaPanelControl.aspx")
            } else if (items.messageCode == "EC") {                
                $("[id$=lblMensaje]").text(items.message.toString());
                centraVentana($('#ventanaconfirm'));
                
                $('#ventanaContain').show();
                $('#ventanaconfirm').show();

                setTimeout(function () {
                    $('#ventanaContain').hide();
                    $('#ventanaconfirm').hide();
                }, 5 * 1000);
            }
        }

        function Procesando() {
            var mesaje = "Procesando información.";
            $('#ventanaContain').show();
            $('#procesandoINE').show();

            $("[id$=lblMensaje]").text(mesaje);
            centraVentana($('#ventanaconfirm'));

            $('#ventanaContain').show();
            $('#ventanaconfirm').show();
        }

        function ProcesandoHide(mesaje) {
            $("[id$=lblMensaje]").text(mesaje);

            setTimeout(function () {
                $('#ventanaContain').hide();
                $('#procesandoINE').hide();

                $("[id$=lblMensaje]").text(mesaje);
                centraVentana($('#ventanaconfirm'));

                $('#ventanaContain').hide();
                $('#ventanaconfirm').hide();
            }, 4000);
        }

        $(document).ready(function () {
            $.urlParam = function (name) {
                var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
                if (results == null) { return null; } else { return results[1] || 0; }
            }
        
            $("#CloseDetail_INE").on("click", function (e) {
                $('#legendCarrusel').html('');
                $('#ventanaContain').hide();
                $('#divDetails').hide();
                $("#tableFields tbody").empty();
            });

            if ($.urlParam("Enable").toString() == "1") {
                $('#btnReadFinger').prop("disabled", true);
                $('#ctl00_ctl00_cphCuerpo_cphPantallas_btnbtnProcesar_Nuevo').hide();
            }
        });
    </script>
    <style>
        #btnReadFinger:disabled {
            background-color: #9ec7fa !important;
        }

        .panel {
            height: 110px;
        }

        #ctl00_ctl00_cphCuerpo_cphPantallas_tbxNoIdentificacion {
            width: 80px!important;
        }

        #ctl00_ctl00_cphCuerpo_cphPantallas_tbxTipoIdentificacion {
            width: 100px!important;
        }

        #ctl00_ctl00_cphCuerpo_cphPantallas_tbxClaveElector {
            width: 120px!important;
        }
    </style>
    <div class="divPantConsul">
        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width: 70%;">
                                    <legend>Autenticación Biometría</legend>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

        <div>
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
        </div>

        <br />

        <div id="divDatosHuella" class="resulbbvaCenter" runat="server">
            <table class="resulbbvaCenter">
                <tr>
                    <td style="width: 70% !important">
                        <table width="100% !important">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Label runat="server" ID="lblTipoIdentificacion" Text="Tipo de Identificación"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="tbxTipoIdentificacion" CssClass="txt3BBVA" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Label runat="server" ID="lblNoIdentificacion" Text="Número de Identificación"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="tbxNoIdentificacion" CssClass="txt3BBVA" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Label runat="server" ID="lblClaveElector" Text="Clave de Elector"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="tbxClaveElector" CssClass="txt3BBVA" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnGeneraArchivoCliente" runat="server" Text="2122121" Style="display: none;" />
                                                <input id="btnReadFinger" type="button" value="Capturar huella." onclick="ReadFinger();/*FNGenerator()*/" class="buttonBBVA" Style="width: 180px !important">
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <div id="divResultAuthentication" runat="server" class="resulbbva">
                                        <table id="tableResult" style="font-size: 12px;" cellpadding="0" cellspacing="0" border="0">
                                            <thead>
                                                <tr class="GridviewScrollHeaderBBVA">
                                                    <th>Primer Nombre</th>
                                                    <th>Segundo Nombre</th>
                                                    <th>Apellido Paterno</th>
                                                    <th>Apellido Materno</th>
                                                    <th>OCR (No. Identificacion)</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="repINEResults" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label0" runat="server" Text='<%# Eval("NumeroSolicitud")%>'> </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("NombreCliente")%>'> </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("RFC")%>'> </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("NombreAgencia")%>'> </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="label4" runat="server" Text='<%# Eval("Alianza")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td>
                                    <div>
                                        <br />
                                        <br />
                                        Resultados de la autenticacion ante el INE
                                        <br />
                                        <br />
                                    </div>
                                    <div id="divNoAuthentication" runat="server" class="resulbbva">
                                        <br />
                                        <br />
                                        <br />
                                        -  No existen datos para mostrar  -
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 30% !important;">
                        <img runat="server" id="imgFingerprint" src="../img/blanco.png"  alt="Huella dactilar capturada" width="220" height="340" style="background-color: gray; max-height: 420px; max-width: 300px;" border="3" />
                    </td>
                </tr>
            </table>
        </div>

        <div id="divEsperaHuella" runat="server">
            <%--<act:ModalPopupExtender ID="mpuEsperaHuella" runat="server"
                TargetControlID="btnGeneraArchivoCliente"
                PopupControlID="popEsperaHuella"
                BackgroundCssClass="modalBackground">
            </act:ModalPopupExtender>--%>
            <%--<asp:Panel ID="popEsperaHuella" runat="server" CssClass="cajadialogo " Width="500px">
            </asp:Panel>--%>
        <div class="tituloConsul">
            <asp:Label ID="lblEsperaHuellaTitulo" runat="server" />
        </div>
        <table width="100%">
            <tr>
                <td class="campos">
                    <br />
                    <br />
                    <asp:Label ID="lblPopUpEsperaHuella" runat="server"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
            <tr style="width: 100%">
                <td align="center" valign="middle" style="display: none;">
                    <input id="btnConsultarHuella" type="button" runat="server" value="Consultar" class="Text " onclick="btnConsultarHuella_Click();" style="display: none; align-content: center; width: 100px;" />
                </td>
            </tr>
        </table>
        </div>
        <div id="divDetails" class = "panel" style="z-index: 1001; display: none; top: 148px; left: 140px; height: 150px;">
            <div style="position:absolute; top:10px; left:10px; width:97%; height:90px;text-align:left !important;">
                <table style="width:100%;">
                    <tr class="tituloConsul">
                        <td width="100%">
                            <table width="100%">
                                <tr>
                                    <td colspan="2" style="width: 95%;">
                                        <legend id="legendCarrusel"></legend>
                                    </td>
                                    <td colspan="2" style="width: 5%;">
                                        <legend id="CloseDetail_INE">Cerrar</legend>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div class="pane">
                    <table class="fieldsetBBVA" style="width: 100%">
                        <tr>
                            <th class="campos" style="width: 100%">
                                <asp:Label ID="mensaje_" Font-Bold="true" Font-Underline="true" />
                            </th>
                        </tr>
                        <tr style="display: none;">
                            <td align="center">
                                <input id="btnDetails" type="button" value="Consultar INE" onclick="INEInformation()" class="Text">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 100%">
                                <div>
                                    <iframe id="UploadFrame" frameborder="0" src="Uploader.aspx"></iframe>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <div class="resulbbvaCenter divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td valign="middle">
                        <asp:Button runat="server" disabled ID="btnbtnProcesar_Nuevo" Text="Procesar" CssClass="buttonBBVA2" OnClick="btnProcesar_Click" OnClientClick="Procesando();"/>
                        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" />
                        <asp:Button runat="server" ID="btnProcesar" Text="Validar ante INE" CssClass="buttonBBVA2" Visible="false" OnClick="btnProcesar_Click" />
                    </td>
                </tr>
            </table>
        </div>

        <div style="visibility: collapse">
            <asp:Button ID="btnGeneraArchivo" runat="server" OnClick="btnGeneraArchivo_Click" CssClass="buttonBBVA" Text="Generar Archivo de Lector para Cliente" Style="width: 180px !important" />
            <asp:Button ID="btnConsultaHuella" runat="server" OnClick="btnConsultaHuella_Click" CssClass="buttonBBVA" Text="Consultar Exitencia de huella en base" Style="width: 180px !important" />
        </div>
    </div>
</asp:Content>

