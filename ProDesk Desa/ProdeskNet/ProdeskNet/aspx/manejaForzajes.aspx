<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.Master" AutoEventWireup="false" CodeFile="manejaForzajes.aspx.vb" Inherits="aspx_manejaForzajes" %>

<%@ MasterType VirtualPath="~/aspx/home.master" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<%--BBV-P-423: RQSOL-04: AVH: 06/12/2016 SE CREA VENTANA FORZAJES--%>
<%--BBV-P-423: RQ H - CSS JRHM 09/12/2016 Se modificaron clases de estilos para asemejarce a el cotizador--%>
<%--BUG-PD-02: AVH:22/12/16 SE QUITA UC Y SE AGREGA EN LA VENTANA --%>
<%--BUG-PD-04: 12/01/2017 Gvargas: Se quitan degugger;--%>
<%--BBV-P-423 RQADM-36: AVH: 31/01/2017  Se modifica pantalla para cargar documentos--%>
<%--BUG-PD-14: JBB 2017/03/08 Se hacen validaciones en el campo , rfc , y formatos de fechas.--%>
<%--BUG-PD-17 JRHM 16/03/17 Correccion a busqueda por fecha y rfc--%>
<%--BUG PD-30 CGARCIA 10/04/2017 Se agrego una propiedad runat a la tabla de tbValidaDocumentos--%>
<%--BUG-PD-62 ERODRIGUEZ 05/06/2017 : Se agrego paginacion para grid--%>
<%--BUG-PD-90 JBEJAR 12/06/2017 SE ELIMINA DIV absoluto en el pie de pagina para no esconder el paginador del gv.--%>
<%--BUG-PD-125 JBEJAR 29/06/2017 SE AGREGA VALIDACION PARA EL BOTON PROCESAR DE LA VENTANA MODAL.--%>
<%--BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN--%>
<%--BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS PARA ABRIR VISOR TELEPRO--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
<%--BUG-PD-372 GVARGAS 26/02/2018 Doble forzar--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">
    <style> /*//BUG-PD-90*/ 
        .divAdminCatPie_local {
            position: relative;
            left: 1%;
            width: 50%;
            height: 8%;
            background-color: white; /*#F3F7FB*/
        }
    </style>
    <script type="text/javascript" src="../js/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="../js/jquery.iframe-transport.js"></script>
    <script type="text/javascript" src="../js/jquery.fileupload.js"></script>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript">

        function hideDivCancela() {
            $('#ventanaContain').hide();
            $("#divcancela").hide();
        }


        $(function () {
            $('#txtruta').on('change', function () {
                var filePath = $(this).val();
                alert(filePath);
            });
        });


        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();
        }

        function Replica() {
            //var fechaIni = document.getElementById('#);
            var fechaIni = $('#<%= hfFeIni.ClientID()%>');
            var fech1S = ($("#feIni").val() == ""
                && $('#<%= txtNombre.ClientID%>').val() == null
                && $('#<%= txtNoSol.ClientID%>').val() == null
                && $('#<%= txtRFC.ClientID%>').val() == null)
                ?
                    RestarDias().toString().split('/')
                :
                    ($("#feIni").val() == null || $("#feIni").val() == ""
                        ?
                            ''
                        :
                            $("#feIni").val().split('/'));

            var fechaFin = $('#<%= hfFeFin.ClientID()%>');
            var fech2S = ($("#feFin").val() == ""
                && $('#<%= txtNombre.ClientID%>').val() == null
                && $('#<%= txtNoSol.ClientID%>').val() == null
                && $('#<%= txtRFC.ClientID%>').val() == null)
                ?
                    new Date().yyyymmdd().toString().split('/')
                :
                    ($("#feFin").val() == null || $("#feFin").val() == ""
                        ?
                            ''
                        :
                            $("#feFin").val().split('/'));

            fechaIni.val(fech1S == '' ? '' : fech1S[2] + '/' + fech1S[1] + '/' + fech1S[0]);
            fechaFin.val(fech2S == '' ? '' : fech2S[2] + '/' + fech2S[1] + '/' + fech2S[0]);
            var botonBuscar = document.getElementById('<%= btnBuscar.ClientID%>');
            botonBuscar.click();

        }
        Date.prototype.yyyymmdd = function () {
            var mm = this.getMonth() + 1; // getMonth() is zero-based
            var dd = this.getDate();

            return [(dd > 9 ? '' : '0') + dd,
                    (mm > 9 ? '' : '0') + mm,
                    this.getFullYear()
            ].join('/');
        };

        function btnAdelantarCliente_click() {
            var usr = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtusua").val();
            var pass = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtpass").val();
            var comm = $("#ctl00_ctl00_cphCuerpo_cphPantallas_txtComentarios").val();

            var msgShow = "";

            if (usr == "") { msgShow = "Debe ingresar Usuario."; }
            else if (pass == "") { msgShow = "Debe ingresar Password."; }
            else if (comm == "") { msgShow = "Debe ingresar Comentarios."; }

            if (msgShow != "") {
                $("#divcancela").hide();
                PopUpLetrero(msgShow.toString());
            }
            else {
                $("#divcancela").hide();
                $("[id$=lblMensaje]").text("Forzando...");
                centraVentana($('#ventanaconfirm'));
                $('#ventanaContain').show();
                $('#ventanaconfirm').show();
                var boton = $(<%=btnAdelantar.ClientID%>)
                boton.click();
            }
        }

        function RestarDias() {
            var someDate = new Date();
            var numberOfDaysToAdd = 7;
            someDate.setDate(someDate.getDate() - numberOfDaysToAdd);
            return someDate.yyyymmdd();
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
            $('#feFin').datepicker(settingsDate1).attr('readonly', 'true').attr('onkeydown', 'return false');



        }

        function fnChequea() {
            fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');
                PopUpLetrero("Documento procesado exitosamente");
        }

        /*           function ValidaRfc(rfcStr) {
               var strCorrecta;
               strCorrecta = rfcStr;
               if (rfcStr.length != 10) {
                   alert('El RFC debe ser de 10 digitos.');
                   return false;
               } else {
                   RFC = rfcStr.toUpperCase();
                   if (!RFC.match(/^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))((-)?([A-Z\d]{3}))?$/)) {
                       alert('El RFC debe tener formato "AAAA999999".');
                       return false;
                   }
               }
   
           }
                    */
        $(window).on("load", function () { Replica() });
    </script>
    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnProcesar {
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
                                <td colspan="2" style="width: 70%;">
                                    <legend>
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Forzajes"></asp:Label></legend>
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
        <div id="Div1" class="resulbbvaCenter" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNoSol" Text="Número de Solicitud"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNoSol" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,7);" Text=""></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFechaIni" Text="Fecha Inicio"></asp:Label>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtFechaIni" CssClass="txt3BBVA" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                        <table id="tbFechaInicio" runat="server">
                            <tr>
                                <td>
                                    <asp:HiddenField runat="server" ID="hfFeIni" />
                                    <input type="text" id="feIni" class="txt3BBVA" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFechaFin" Text="Fecha Fin"></asp:Label>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtFechaFin" CssClass="txt3BBVA" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table id="tbFechaFin" runat="server">
                            <tr>
                                <td>
                                    <asp:HiddenField runat="server" ID="hfFeFin" />
                                    <input type="text" id="feFin" class="txt3BBVA" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <input id="btnBuscaCliente" type="button" value="Buscar" onclick="Replica();" class="buttonBBVA2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNombre" Text="Nombre del Cliente"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtNombre" Width="98%" CssClass="txt3BBVA" MaxLength="50" onkeyup="ReemplazaAcentos(event, this.id, this.value);" Text=""></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblRFC" Text="RFC"></asp:Label>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtRFC" CssClass="txt3BBVA" MaxLength="10" Text=""></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnLimpiar" Text="Limpiar" CssClass="buttonSecBBVA2" OnClick="btnLimpiar_Click1" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="gvForzajes" runat="server" AutoGenerateColumns="false"
                HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowPaging="true" PageSize="10"
                Width="100%" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="gvForzajes_PageIndexChanging"
                EmptyDataText="No se encontró información con los parámetros proporcionados.">
                <Columns>
                    <asp:BoundField DataField="FolioSolicitud" HeaderText="Folio Solicitud" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre del Cliente" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Agencia" HeaderText="Agencia" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Canal" HeaderText="Canal" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Submarca" HeaderText="Submarca" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MontoT" HeaderText="Monto Total Solicitado" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="ValorLTV" HeaderText="Valor LTV" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="ValorCCI" HeaderText="Valor CCI" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="ValorBSCORE" HeaderText="Valor BSCORE" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                        <ItemTemplate>
                            <div class="tooltip">
                                <asp:CheckBox runat="server" ID="chkSelecciona" AutoPostBack="true" OnCheckedChanged="chkSelecciona_CheckedChanged" Checked="false" CommandArgument='<%# Eval("FolioSolicitud") %>' />

                            </div>
                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
        <div style="height: 100px"></div>
        <table id="tbValidarObjetos" class="resulGrid" runat="server">
        </table>
        <div class="resulbbvaCenter divAdminCatPie_local">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">
                        <%--<asp:Button runat="server" ID="btnRegresar" text="Regresar"   CssClass="buttonSecBBVA2" />--%>
                        <asp:Button runat="server" ID="btnProcesar" Text="Procesar" CssClass="buttonBBVA2" OnClientClick="mostrarCanceDiv()" />
                        <input type="button" value="Procesar" class="buttonBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                    </td>
                </tr>
            </table>
        </div>

        <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top:15%; left:31%; width: 220px;">
            <%--<cc1:ModalPopupExtender ID="mpoForzaje" runat="server" TargetControlID="btnProcesar" PopupControlID="popForzaje" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>--%>
                <div class="tituloConsul">
                    <asp:Label ID="Label1" runat="server" Text="Permisos para Forzar" />
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
                        <td colspan="2" class="campos">Comentarios:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox runat="server" ID="txtComentarios" TextMode="MultiLine" Height="50px" Width="200px" MaxLength="200" Columns="50" Rows="12"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td align="center" valign="middle" style="width: 50%">
                            <input type="button" runat="server" id="btnAdelantarCliente" value="Procesar" onclick="btnAdelantarCliente_click();"  ondblclick="disabled=true" />    <%--BUG-PD-125--%>
                        </td>
                        <td align="center" valign="middle" style="width: 50%">
                            <asp:Button ID="Button1" runat="server" Text="Cancelar" OnClick="Button1_Click" />
                        </td>
                    </tr>
                </table>
<%--            <asp:Panel ID="popForzaje" runat="server" CssClass="cajadialogo" Width="250px">
            </asp:Panel>--%>
        </div>
    </div>
    <div style="visibility: collapse">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="buttonBBVA2" OnClick="btnBuscar_Click" />
        <asp:Button runat="server" ID="btnAdelantar" Text="Procesar" OnClick="btnAdelantar_Click" />
    </div>
    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />

</asp:Content>

