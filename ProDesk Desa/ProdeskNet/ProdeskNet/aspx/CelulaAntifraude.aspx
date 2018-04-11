<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="CelulaAntifraude.aspx.vb" Inherits="aspx_CelulaAntifraude" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%--BBVA-P-423 RQADM-11 GVARGAS 10/03/2017 Antifraude Básico – Carruseles (Celulares, RFC, ID’s, Contador de Teléfonos) 31--%>
<%----BBVA-P-423 13 Check HB GVARGAS 23/03/2017 Check Carruseles y Visor Documental--%>
<%--<%BBV-P-423:RQAMD-26 JBEJAR 11/05/2017 SE PROGRAMA  CEDULA ANTIFRAUDE.-- %>--%>
<%--BUG-PD-50 JBEJAR 19/05/2017 CORRECIONES BUG-PD-50 --%>
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN--%>
<%--BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS PARA ABRIR VISOR TELEPRO--%>
<%--BBV-P-423 RQ-PD-17 6 GVARGAS 16/01/2018 details INE--%>
<%--BBV-P-423 RQ-PD-17 7 GVARGAS 22/01/2018 Correciones generales--%>
<%--BUG-PD-347 GVARGAS 23/01/2018 Correcion cierre detalle INE Celula Antifraude--%>
<%--BBV-P-423 RQ-PD-17 8 GVARGAS 29/01/2018 Correcion detalle y turnar--%>
<%--BBV-P-423 RQ-PD-17 10 GVARGAS 31/01/2018 Correcion flujo--%>
<%--BBV-P-423 RQ-PD-17 14 GVARGAS 13/02/2018 Ajustes flujo 5--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
    <style type="text/css">
        html, body
        {
            overflow: hidden;
        }

        #divDetails {
            height: 110px;
        }
    </style>
    <script src="../js/jquery.ui.widget.js"></script>
    <script src="../js/jquery.iframe-transport.js"></script>
    <script src="../js/jquery.fileupload.js"></script>
    <script src="../js/Funciones.js"></script>
    <script type="text/javascript">

        function mostrarCanceDiv() {
            div = document.getElementById('divcancela');
            div.style.display = '';

        }
        function fnChequea() {
            fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');
            $('#ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1C').attr('disabled', false);
                PopUpLetrero("Documento procesado exitosamente");
            }


        

        function btnProcesarCliente_click() {
            //document.getElementById('btnProcesarCliente').disabled = false;
            $('#<%= btnProcesarCliente.ClientID%>').prop('disabled', true);
            var buttonProcesar = document.getElementById('<%= btnProcesar.ClientID%>');
            buttonProcesar.click();
            return;
        }

        function btnProcesarCliente_Enable() {
            $('#<%= btnProcesarCliente.ClientID%>').prop('disabled', false);
            // $('#btnProcesarCliente').prop('disabled', false);
        }

        function btnProcesarCliente_Enable1() {
            $('#<%= btnProcesarCliente.ClientID%>').prop('disabled', true);
               // $('#btnProcesarCliente').prop('disabled', false);
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

        $(document).ready(function () {
            $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddlTurnar").on("change", function () {
                var opcion = $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddlTurnar").val().toString();

                if (opcion == "0") { $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcess").prop("disabled", true); }
                else { $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcess").prop("disabled", false); }
            });

            $("#CloseDetail").on("click", function (e) {
                $('#legendCarrusel').html('');
                $('#ventanaContain').hide();
                $('#divDetails').hide();
                $("#tableFields tbody").empty();
            });

            $.urlParam = function (name) {
                var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
                if (results == null) { return null; } else { return results[1] || 0; }
            }

            detallesINE(0);
        });

        function turnarBtn() {
            $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcess").prop("disabled", true);
            var opcion = $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddlTurnar").val();

            turnarFunction(opcion, $.urlParam("sol").toString(), $.urlParam("usu").toString())
        }

        function pageLoad() {
            detallesINE(0);

            var opcTurnar = $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddlTurnar").val();

            if (opcTurnar != null) {
                if (opcTurnar == "0") { $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcess").prop("disabled", true); }
                else { $("#ctl00_ctl00_cphCuerpo_cphPantallas_btnProcess").prop("disabled", false); }
            }
        }

        function turnarFunction(opc, sol, usu) {
            var settings = { type: "POST", url: "", async: false, data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
            settings.url = "CelulaAntifraude.aspx/btnProcessINE";
            settings.success = OnSuccessturnarFunction;
            settings.data = "{'turnarOpc' : " + opc + ", 'sol': '" + sol + "', 'usr': " + usu + "}";
            settings.failure = function (response) { alert("Error al cargar : " + errorLabel.toString()); }
            $.ajax(settings);
        }

        function OnSuccessturnarFunction(response) {
            var items = $.parseJSON(response.d.toString());
            PopUpLetreroRedirect(items.msg, items.path);
        }

        function openVisor() {
            var settings = { type: "POST", url: "", async: false, data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
            settings.url = "CelulaAntifraude.aspx/OpenVisor";
            settings.success = OnSuccessOpenVisor;
            settings.data = "{'folio' : " + $.urlParam("sol").toString() + "}";
            settings.failure = function (response) { alert("Error al cargar el visor."); }
            $.ajax(settings);
        }

        function OnSuccessOpenVisor(response) {
            if ($.urlParam("sol").toString() != 177) { btnProcesarCliente_Enable(); }
            window.open(response.d.toString());
        }

        function detallesINE(opc) {
            if ($.urlParam("sol").toString() == 177) {
                $("#btnDetails").hide();
            } else {
                var settings = { type: "POST", url: "", async: false, data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
                settings.url = "CelulaAntifraude.aspx/detailsINE";
                settings.success = OnSuccessDetailsINE;
                settings.data = "{'folio' : '" + $.urlParam("sol").toString() + "', 'opc': '" + opc + "'}";
                settings.failure = function (response) { alert("Error al cargar : " + errorLabel.toString()); }
                $.ajax(settings);
            }
        }

        function OnSuccessDetailsINE(response) {
            var items = $.parseJSON(response.d.toString());
            if (items.mensaje == "ERROR") {
                alert(items.mensaje.toString());
                return;
            }
            else if (items.mensaje == "NO") {
                $("#btnDetails").hide();
                return;
            }
            else {
                var itemsINE = $.parseJSON(items.mensaje.toString());

                $('#legendCarrusel').html('Detalles INE');
                $('#ventanaContain').show();
                $('#divDetails').show();

                var nombre_ = "No coincide.";
                var ap_pat = "No coincide.";
                var ap_mat = "No coincide.";
                var ocr = "No coincide.";
                var INE_ = "El biométrico no coincide.";

                if (itemsINE.name.toString() == "true") { nombre_ = "Coincide."; }

                if (itemsINE.lastName.toString() == "true") { ap_pat = "Coincide."; }

                if (itemsINE.mothersLastName.toString() == "true") { ap_mat = "Coincide."; }

                if (itemsINE.ocr.toString() == "true") { ocr = "OCR coincide, " + itemsINE.descripcion.toString() + "."; }

                var print2 = parseInt(itemsINE.pawPrint2);
                var print7 = parseInt(itemsINE.pawPrint7);

                if ((print2 > 9099) || (print7 > 9099)) { INE_ = "El biométrico coincide."; }

                var porc = 0;

                if (print2 == print7) { porc = print7 / 100; }
                else if (print2 > print7) { porc = print2 / 100; }
                else { porc = print7 / 100; }

                $("#Nombre_INE").html(nombre_.toString());
                $("#Ap_INE").html(ap_pat.toString());
                $("#Am_INE").html(ap_mat.toString());
                $("#Ocd_INE").html(ocr.toString());
                $("#Biometrico").html(INE_);
                $("#porcBiometrico").html(porc + '%');
            }
        }
    </script>
    <div class="divPantConsul">
        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <legend>
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Célula Antifraude"></asp:Label></legend>
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

        <asp:GridView ID="gvCelula" runat="server" AutoGenerateColumns="false"
            HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowPaging="true" PageSize="5"
            Width="100%" PagerStyle-HorizontalAlign="Right"
            EmptyDataText="No se encontró información con los parámetros proporcionados.">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="Número de Solicitud" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre del Solicitante" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="RFC" HeaderText="RFC del Solicitante" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="FECHA" HeaderText="Fecha de Ingreso" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:g}" />
                <asp:BoundField DataField="HORA" HeaderText="Hora" ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </asp:GridView>
        <table class="fieldsetBBVA" width="100%" style="height: 100%;" >
            <tr>
                <td align="center"><asp:Label runat="server" ID="lblTextTip"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlTipificaciones" OnSelectedIndexChanged="ddlTipoTipificacion_SelectedIndexChanged" AutoPostBack="true" CssClass="select2BBVA">
                            <asp:ListItem Value="0" Text="<Seleccionar>" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Procesable"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Rechazado"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="ddlTurnar" CssClass="select2BBVA">
                            <asp:ListItem Value="0" Text="<Seleccionar>" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Precalificación"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Declinado"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Procesable"></asp:ListItem>
                        </asp:DropDownList>
                    <input id="btnDetails" type="button" value="Detalle INE" onclick="detallesINE(1)" class="buttonSecBBVA2">
                    <asp:Button runat="server" ID="btnVisor" Text="Visor Documental" CssClass="buttonSecBBVA2" OnClick="btnVisor_Click" Visible="false" />
                    <input id="btnVisorNew" type="button" value="Visor Documental" onclick="openVisor()" class="buttonSecBBVA2">
                    <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" OnClick="btnRegresar_Click" />
                    <input type="button" runat="server" id="btnProcesarCliente" value="Procesar" onclick="btnProcesarCliente_click();" class="buttonBBVA2" disabled="disabled" />
                    <input type="button" runat="server" id="btnProcess" value="Procesar" class="buttonBBVA2" onclick="turnarBtn();" disabled="disabled" />
                </td>
            </tr>
        </table>
        <br />
    </div>
    <div style="visibility: collapse">
        <asp:Button ID="btnProcesar" runat="server" Text="Procesar" CssClass="buttonBBVA2" OnClick="btnProcesar_Click" />
    </div>
    <div id="divDetails" class = "panel" style="z-index: 1001; display: none; top: 148px; left: 140px;">
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
                                    <legend id="CloseDetail">Cerrar</legend>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div class="pane">
                <table class="fieldsetBBVA" style="width: 100%">
                    <tr>
                        <th class="campos" style="width: 25%">Nombre:                    
                        </th>
                        <th class="campos" style="width: 25%">
                            <asp:Label ID="Nombre_INE" Font-Bold="true" Font-Underline="true" />
                        </th>
                        <th class="campos" style="width: 25%">Apellido Paterno:                    
                        </th>
                        <th class="campos" style="width: 25%">
                            <asp:Label ID="Ap_INE" Font-Bold="true" Font-Underline="true" />
                        </th>
                    </tr>
                    <tr>
                        <th class="campos">Apellido Materno:                    
                        </th>
                        <th class="campos">
                            <asp:Label ID="Am_INE" Font-Bold="true" Font-Underline="true" />
                        </th>
                        <th class="campos">OCR:                    
                        </th>
                        <th class="campos">
                            <asp:Label ID="Ocd_INE" Font-Bold="true" Font-Underline="true" /> 
                        </th>
                    </tr>
                    <tr>
                        <th class="campos">Biometrico:                    
                        </th>
                        <th class="campos">
                            <asp:Label ID="Biometrico" Font-Bold="true" Font-Underline="true" />
                        </th>
                        <th class="campos">% Biometrico:                    
                        </th>
                        <th class="campos">
                            <asp:Label ID="porcBiometrico" Font-Bold="true" Font-Underline="true" />
                        </th>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />
    <asp:HiddenField ID="hdnResultado" runat="server" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
</asp:Content>
