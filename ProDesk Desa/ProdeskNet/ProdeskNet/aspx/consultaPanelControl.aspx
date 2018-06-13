<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="consultaPanelControl.aspx.vb" Inherits="aspx_consultaPanelSeguimiento" EnableEventValidation="false" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>--%>

<%--BUG-PD-70:MAPH:29/08/2017:SEGUNDA VERSION DEL PANEL DE SEGUIMIENTO--%>
<%--RQADM2-01:MPUESTO:11/09/2017:Mejoras de Panel de Seguimiento--%>
<%--BUG-PD-226:MPUESTO:04/11/2017:Correcciones de Panel de Seguimiento--%>
<%--BUG-PD-252:MPUESTO:27/10/2017:Permitir busqueda al realizar Enter en campos de texto del cuadro de Busqueda--%>
<%--BUG-PD-322:ERODRIGUEZ: 29/12/2017: Se agrego boton para actualizar el panel--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
<%--BUG-PD-369 GVARGAS 23/02/2018 Correccion panel avoid Ajax Tool Kit Paginar--%>
<%--BUG-PD-403: RHERNANDEZ: 20/03/2018: Se oculta div de tareas para evitar doble click de creacion de tareas --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">

    <script type="text/javascript" language="javascript">
        function pageLoad() {
            var tableAux = $('#tblTasksAuxiliar');
            var tableObjective = $('#<%=tblTasks.ClientID%>');

            tableObjective[0].innerHTML = tableAux[0].innerHTML;

            var settingsDate = {
                dateFormat: "dd/mm/yy",
                showAnim: "slide",
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                selectOtherMonths: true,
                autoSize: true
            };

            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $('#<%= tbxSearchInitialDate.ClientID%>').datepicker(settingsDate).attr('readonly', 'true').attr('onkeydown', 'return false');
            $('#<%= tbxSearchFinalDate.ClientID%>').datepicker(settingsDate).attr('readonly', 'true').attr('onkeydown', 'return false');

            fillWithBlanks();

            insertFirstOption();

            $('input').on("change paste keyup dragend", function () {
                var dict = { "á": "a", "é": "e", "í": "i", "ó": "o", "ú": "u", "Á": "A", "É": "E", "Í": "I", "Ó": "O", "Ú": "U" };
                $(this).val($(this).val().toString().replace(/[^\w ]/g, function (char) { return dict[char] || char; }));
            });

            $(".FindRequestClass").bind('keypress', function (e) {
                if (e.keyCode == 13) {
                    $("#<%= btnFindRequestsClient.ClientID%>").click();
                    //$('#<%= popSearch.ClientID%>').hide();
                }
            });

            $("#btnNewFind").on("click", function (e) {
                $('#newFindPanel').show();
                $('#ventanaContain').show();
                $('#divDetails').show();
            });

            $("#closeSearch").on("click", function (e) {
                $('#newFindPanel').hide();
                $('#ventanaContain').hide();
                $('#divDetails').hide();
            });

            $("#btnAddNew").on("click", function (e) {
                $('#divAddRequest').show();
                $('#ventanaContain').show();
                $('#divDetails').show();
            });

            $("#closeAdd").on("click", function (e) {
                $('#divAddRequest').hide();
                $('#ventanaContain').hide();
                $('#divDetails').hide();
            });

            $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddlPaginador").on("change", function (e) {
                var SelectedValor = this.value;
                addPaginator(SelectedValor.toString());
            });
        }

        function addPaginator(Paginador) {
            var settings = { type: "POST", url: "", async: false, data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
            settings.url = "consultaPanelControl.aspx/addPaginador";
            settings.success = OnSuccessAddPaginator;
            settings.data = "{'id_Paginador' : '" + Paginador.toString() + "'}";
            settings.failure = function (response) { alert("Error al cargar : " + errorLabel.toString()); }
            $.ajax(settings);
        }

        function OnSuccessAddPaginator(response) { $('#ctl00_ctl00_cphCuerpo_cphPantallas_hdnPostbackSearch').val(""); }

        function btnFindTasksClient_click(taskValue, colNumber) {
            $('#<%= hdnTask.ClientID%>').val(taskValue);
            $('#<%= hdnColNumber.ClientID%>').val(colNumber);
            $('#<%= btnFindTasks.ClientID%>').click();
        }

        function ShowTasks() {
            //$('#<%= btnShowTasks.ClientID%>').click();
            $('#ctl00_ctl00_cphCuerpo_cphPantallas_divTasks').show();
            $('#ventanaContain').show();
            $('#divDetails').show();
        }
        function HideTasks() {
            $('#<%= divTasks.ClientID%>').hide();
            var idPantalla = getUrlValue("idPantalla");
            blockScreen(idPantalla);
        }

        function btnHideModalsClient_click() {
            resetDropDowns();
            $('#<%= btnHideModals.ClientID%>').click();
        }

        function btnCleanClient_Click() {
            $('#<%= tbxSearchRequestNumber.ClientID%>').val('');
            $('#<%= tbxSearchClient.ClientID%>').val('');
            $('#<%= tbxSearchDealer.ClientID%>').val('');
            $('#<%= tbxSearchProdeskUser.ClientID%>').val('');
            $('#<%= tbxSearchInitialDate.ClientID%>').val('');
            $('#<%= tbxSearchFinalDate.ClientID%>').val('');
            $('#<%= btnClean.ClientID%>').click();
        }

        function invokeFillDdl(ddlObjective, ddlSender) {
            switch (ddlSender) {
                case 'addProduct':
                    ddlObjective = 'AddProduct';
                    ddlSender = 'ddlAddCompany';
                    break;
                case 'addCompany':
                    ddlObjective = 'AddCompany';
                    ddlSender = 'ddlAddLegalPersonality';
                    break;
                default:

            }
            fillddl(ddlObjective, ddlSender);
            insertFirstOption();
        }

        function insertFirstOption() {
            $("#<%= ddlAddCompany.ClientID%> :first").text("Seleccionar Empresa");
            $("#<%= ddlAddProduct.ClientID%> :first").text("Seleccionar Producto");
            $("#<%= ddlAddLegalPersonality.ClientID%> :first").text("Seleccionar Persona");
            $("#<%= ddlAddDealer.ClientID%> :first").text("Seleccionar Distribuidor");
        }

        function resetDropDowns() {
            $("#<%= ddlAddCompany.ClientID%>").val($("#<%= ddlAddCompany.ClientID%> option:first").val());
            $("#<%= ddlAddProduct.ClientID%>").val($("#<%= ddlAddProduct.ClientID%> option:first").val());
            $("#<%= ddlAddLegalPersonality.ClientID%>").val($("#<%= ddlAddLegalPersonality.ClientID%> option:first").val());
            $("#<%= ddlAddDealer.ClientID%>").val($("#<%= ddlAddDealer.ClientID%> option:first").val());
        }

        function btnExcecuteAddRequestClient_Click() {
            var mensajeError = 'Seleccione un elemento válido en la(s) siguiente(s) opcion(es):';
            var withError = false;
            if ($('#<%=ddlAddCompany.ClientID%>').val() <= 0) {
                withError = true;
                mensajeError += '\n     + Empresa.';
            }
            if ($('#<%=ddlAddProduct.ClientID%>').val() <= 0) {
                withError = true;
                mensajeError += '\n     + Producto.';
            }
            if ($('#<%=ddlAddLegalPersonality.ClientID%>').val() <= 0) {
                withError = true;
                mensajeError += '\n     + Tipo de Persona.';
            }
            if ($('#<%=ddlAddDealer.ClientID%>').val() <= 0) {
                withError = true;
                mensajeError += '\n     + Distribuidor.';
            }

            if (withError) {
                alert(mensajeError);
            }
            else {
                $('#<%= hdnAddDealer.ClientID%>').val($('#<%=ddlAddDealer.ClientID%>').val());
                $('#<%= hdnAddProduct.ClientID%>').val($('#<%= ddlAddProduct.ClientID%>').val());
                $('#<%= hdnAddLegalPersonality.ClientID%>').val($('#<%= ddlAddLegalPersonality.ClientID%>').val());
                $('#<%= hdnAddCompany.ClientID%>').val($('#<%= ddlAddCompany.ClientID%>').val());
                $('#<%= btnExcecuteAddRequest.ClientID%>').click();
            }
        }

        function btnFindRequestsClient_Click() {
            var iniDate = $('#ctl00_ctl00_cphCuerpo_cphPantallas_tbxSearchInitialDate').val();
            $('#ctl00_ctl00_cphCuerpo_cphPantallas_hdnSearchInitialDate').val(iniDate);
            if (iniDate != '') {
                iniDate = iniDate.split('/');
                iniDate = iniDate[1] + '/' + iniDate[0] + '/' + iniDate[2];
            }
            var finDate = $('#ctl00_ctl00_cphCuerpo_cphPantallas_tbxSearchFinalDate').val();
            $('#ctl00_ctl00_cphCuerpo_cphPantallas_hdnSearchFinalDate').val(finDate);
            if (finDate != '') {
                finDate = finDate.split('/');
                finDate = finDate[1] + '/' + finDate[0] + '/' + finDate[2];
            }

            $('#ctl00_ctl00_cphCuerpo_cphPantallas_hdnSearchRequestNumber').val($('#ctl00_ctl00_cphCuerpo_cphPantallas_tbxSearchRequestNumber').val());
            $('#ctl00_ctl00_cphCuerpo_cphPantallas_hdnSearchClient').val($('#ctl00_ctl00_cphCuerpo_cphPantallas_tbxSearchClient').val());
            $('#ctl00_ctl00_cphCuerpo_cphPantallas_hdnSearchDealer').val($('#ctl00_ctl00_cphCuerpo_cphPantallas_tbxSearchDealer').val());
            $('#ctl00_ctl00_cphCuerpo_cphPantallas_hdnSearchProdeskUser').val($('#ctl00_ctl00_cphCuerpo_cphPantallas_tbxSearchProdeskUser').val());

            if ((new Date(iniDate).getTime() > new Date(finDate).getTime())) {
                alert('La fecha de inicio no puede ser mayor a la final');
            }
            else {
                $('#ctl00_ctl00_cphCuerpo_cphPantallas_hdnPostbackSearch').val("Busqueda");
                $('#ctl00_ctl00_cphCuerpo_cphPantallas_btnFinNewAJAX').click();
            }
        }

        function fillWithBlanks()
        {
            var divSpaces = $('#divSpaces');
            var rowCount = $('#tableResult tr').length;
            var innerHTML = '';
            rowCount = 13 - rowCount;
            
            for (var counter = 0; counter < rowCount; counter++)
            {
                innerHTML += '<br/><br/><br/>'
            }

            divSpaces[0].innerHTML = innerHTML;
        }

       
    </script>
    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnFinNewAJAX {
            display: none;
        }

        .noBTN {
            display: none;
        }
    </style>

    <div class="divPantConsul">
        <div class="fieldsetBBVA">
            <table width="100%">
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width: 70%;"><legend>Seguimiento de Status por Solicitante</legend></td>
                                <td style="width: 30%;" align="right" valign="middle">&nbsp;&nbsp;&nbsp;
                                </td>
                                 <td>  <%--BUG-PD-322--%>
                                    <asp:Button ID="btnRefresh" runat="server" Text="Actualizar" onclick="Page_Load" CssClass="buttonBBVA2" />
                                </td>
                                <td>
                                    <asp:Button ID="btnShowSearch" runat="server" Text="Buscar" CssClass="buttonBBVA2" Visible="false" />
                                    <input type="button" id="btnNewFind" value="Buscar" class="buttonBBVA2" />
                                </td>
                                <td>
                                    <asp:Button ID="btnAddRequest" runat="server" Text="Agregar" CssClass="buttonBBVA2 noBTN" />
                                    <input type="button" id="btnAddNew" value="Agregar" class="buttonBBVA2" onclick="btnAddNew();"/>
                                </td>
                                <td>
                                    <input type="button" id="btnCleanClient" class="buttonBBVA2" onclick="btnCleanClient_Click();" value="Limpiar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <div id="divFiltros" class="resulbbvaCenter" runat="server">
            <table width="100%" id="tblFindNew">
                <tr>
                    <td></td>
                    <td>
                        <asp:Label runat="server" ID="lblCompany" Text="Empresa: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label runat="server" ID="lblProduct" Text="Producto: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlProduct" AutoPostBack="true" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label runat="server" ID="lblLegalPersonality" Text="Tipo de Persona: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlLegalPersonality" AutoPostBack="true" CssClass="selectBBVA"></asp:DropDownList>
                    </td>
                    <td></td>
                </tr>
            </table>
            <br />
            <br />
        </div>
        <div style="height: 80% !important; vertical-align: top !important">
            <table id="tableResult" class="resulGridPag">
                <thead>
                    <tr class="GridviewScrollHeaderBBVA">
                        <th>SOLICITUD</th>
                        <th>AGENCIA</th>
                        <th>ASESOR</th>
                        <th>CLIENTE</th>
                        <th>STATUS DE CREDITO</th>
                        <th>TAREA ACTUAL</th>
                        <th>INFORMACIÓN</th>
                        <th>PRE</th>
                        <th>CAL</th>
                        <th>VAL</th>
                        <th>FOR</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repSolicitudes" runat="server">
                        <ItemTemplate>
                            <tr class="GridviewScrollItemBBVA" style="display: table-row;">
                                <td>
                                    <asp:Label ID="lblSolicitud" runat="server" Text='<%# CutText(Eval("SOLICITUD"))%>' ToolTip='<%# Eval("SOLICITUD")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAgencia" runat="server" Text='<%# CutText(Eval("AGENCIA"))%>' ToolTip='<%# Eval("AGENCIA")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAsesor" runat="server" Text='<%# CutText(Eval("ASESOR"))%>' ToolTip='<%# Eval("ASESOR")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCliente" runat="server" Text='<%# CutText(Eval("CLIENTE"))%>' ToolTip='<%# Eval("CLIENTE")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblStatusCredito" runat="server" Text='<%# CutText(Eval("STATUS_CREDITO"))%>' ToolTip='<%# Eval("STATUS_CREDITO")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTareaActual" runat="server" Text='<%# CutText(Eval("TAREA_ACTUAL"))%>' ToolTip='<%# Eval("TAREA_ACTUAL")%>'> </asp:Label>
                                </td>
                                <td>
                                    <input id="imgInfo" type="image" src="..\App_Themes\Imagenes\Alert.png" title='<%# Eval("FORZAJE_TOOLTIP")%>' />
                                </td>
                                <td <%# GetStyle(Eval("PRE"))%> <%# GetEvent(Eval("PRE"), Eval("SOLICITUD"), "1")%>></td>
                                <td <%# GetStyle(Eval("CAL"))%> <%# GetEvent(Eval("CAL"), Eval("SOLICITUD"), "2")%>></td>
                                <td <%# GetStyle(Eval("VAL"))%> <%# GetEvent(Eval("VAL"), Eval("SOLICITUD"), "3")%>></td>
                                <td <%# GetStyle(Eval("FOR"))%> <%# GetEvent(Eval("FOR"), Eval("SOLICITUD"), "4")%>></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="3"></td>
                        <td>
                            <br />
                            <asp:Label ID="lblPaginador" runat="server" Text="Número de página" Font-Size="Small"></asp:Label>
                            <br />
                        </td>
                        <td>
                            <br />
                            <asp:DropDownList ID="ddlPaginador" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaginador_SelectedIndexChanged"></asp:DropDownList>
                            <br />
                        </td>
                        <td colspan="6"></td>
                    </tr>
                </tbody>
            </table>
        <div id="divSpaces">

        </div>            
            <table rules="all" id="tbCodigoColores" style="width: 100%; border-collapse: collapse;" cellspacing="0" border="1">
                <tbody>
                    <tr style="border-color: White; font-size: Larger; font-weight: bold;" align="center">
                        <td style="color: Black; background-color: #B5E5F9;">En proceso</td>
                        <td style="color: Black; background-color: #52BCEC;">Proceso Concluido Satisfactoriamente</td>
                        <td style="color: Black; background-color: #F6891E;">Proceso Cancelado</td>
                        <td style="color: Black; background-color: #006EC1;">Proceso Activado</td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>

    <div id="divTasks" runat="server" style="display: none; position: absolute; background-color: white; z-index: 1010; top: 17%; left: 7%; width: auto !important;">
        <%--<act:ModalPopupExtender ID="mpuTasks" runat="server"
            TargetControlID="btnDummie"
            PopupControlID="popShowTasks"
            BackgroundCssClass="modalBackground">
        </act:ModalPopupExtender>--%>
        <asp:Panel ID="popShowTasks" runat="server" style="max-height:400px; width:900px; overflow-x:auto; overflow-y:auto">
            <table id="tblTasks" runat="server" class="resulGridPag">
                <thead>
                    <tr>
                        <th>CABECERA</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>CUERPO</td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
    </div>
    <div id="newFindPanel" class="resulbbvaCenter" style="display: none; position: absolute; background-color: white; z-index: 1010; top: 17%; left: 29%; width: auto !important;">
                <table>
                    <tr>
                        <th colspan="2" style="text-align: right; border-bottom: hidden !important">
                            <b>
                        <a id="closeSearch" class="link" style="font-size: medium"><strong>X</strong></a>
                            </b>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2">
                            <asp:Label ID="lblTitleSearch" runat="server" Text="Búsqueda"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td style="text-align: left !important">
                            <asp:Label ID="lblSearchRequestNumber" runat="server" Text="Número de Solicitud: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbxSearchRequestNumber" runat="server" Style="width: 200px" MaxLength="9" onkeypress="return ValCarac(event,7);" CssClass="FindRequestClass" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left !important">
                            <asp:Label ID="lblSearchClient" runat="server" Text="Nombre de Cliente: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbxSearchClient" runat="server" onkeypress="return ValCarac(event,25);" Style="width: 200px" MaxLength="500" CssClass="FindRequestClass"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left !important">
                            <asp:Label ID="lblSearchDealer" runat="server" Text="Distribuidor: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbxSearchDealer" runat="server" onkeypress="return ValCarac(event,21);" Style="width: 200px" CssClass="FindRequestClass"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left !important">
                            <asp:Label ID="lblSearchProdeskUser" runat="server" Text="Usuario Asignado: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbxSearchProdeskUser" runat="server" Style="width: 200px" onkeypress="return ValCarac(event,12);" CssClass="FindRequestClass"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left !important">
                            <asp:Label ID="lblSearchInitialDate" runat="server" Text="Fecha Inicial: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSearchFinalDate" runat="server" Text="Fecha Final: "></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left !important">
                            <asp:TextBox ID="tbxSearchInitialDate" runat="server" Style="width: 200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tbxSearchFinalDate" runat="server" Style="width: 200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="align-content: flex-end">
                            <input type="button" id="btnFindRequestsClient" runat="server" onclick="btnFindRequestsClient_Click();" value="Buscar Solicitudes" class="buttonSecBBVA2" />
                    <asp:Button runat="server" id="btnFinNewAJAX" OnClick="btnFinNewAJAX_Click" />
                        </td>
                    </tr>
                </table>
            </div>

    <div id="divSearch" runat="server">
<%--        <act:ModalPopupExtender ID="mpuShowSearch" runat="server"
            TargetControlID="btnShowSearch"
            PopupControlID="popSearch"
            BackgroundCssClass="modalBackground">
        </act:ModalPopupExtender>--%>
        <asp:Panel ID="popSearch" runat="server" Style="background-color: white" >

        </asp:Panel>
    </div>

    <%--<div class="resulbbvaCenter" runat="server" style="display: none;">--%>
<%--        <act:ModalPopupExtender ID="mpuAddRequest" runat="server"
            TargetControlID="btnAddRequest"
            PopupControlID="popAdd"
            BackgroundCssClass="modalBackground">
        </act:ModalPopupExtender>--%>
        <%--<asp:Panel ID="popAdd" runat="server" Style="background-color: white">--%>
            <div id="divAddRequest" class="resulbbvaCenter" style="border-radius: 3px; position: absolute; display: none; background-color: white; z-index: 1010; top: 17%; left: 29%; width: auto !important;">
                <table>
                    <tr>
                        <th colspan="2" style="text-align: right; border-bottom: hidden !important">
                            <b>
                                <a id="closeAdd" class="link" onclick="btnHideModalsClient_click();" style="font-size: medium"><strong>X</strong></a>
                            </b>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2">
                            <asp:Label ID="lblTitleAdd" runat="server" Text="Alta Solicitud"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <td style="text-align: left !important">
                            <asp:Label ID="lblAddCompany" runat="server" Text="Empresa: "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAddCompany" runat="server" Style="width: 300px" onchange="invokeFillDdl('addCompany', 'addProduct');"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left !important">
                            <asp:Label ID="lblAddProduct" runat="server" Text="Producto: "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAddProduct" runat="server" Style="width: 300px" onchange="invokeFillDdl('addProduct', 'addLegalPersonality');"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left !important">
                            <asp:Label ID="lblAddLegalPersonality" runat="server" Text="Tipo de Persona: "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAddLegalPersonality" runat="server" Style="width: 300px"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left !important">
                            <asp:Label ID="lblAddDealer" runat="server" Text="Distribuidor: "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAddDealer" runat="server" Style="width: 300px"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <input id="btnExcecuteAddRequestClient" type="button" class="buttonSecBBVA2" value="Agregar" onclick="btnExcecuteAddRequestClient_Click();" style="align-content: center !important" />
                        </td>
                    </tr>
                </table>
            </div>
        <%--</asp:Panel>--%>
    <%--</div>--%>

    <div style="visibility: collapse">
        <table id="tblTasksAuxiliar" class="resulGridPag">
            <thead>
                <tr class="GridviewScrollHeaderBBVA">
                    <th>Sol: <%=hdnTask.Value%> </th>
                    <th colspan="2"></th>
                    <th style="text-align: right;">
                        <b>
                            <a id="btncloseModal" class="link" onclick="btnHideModalsClient_click();" style="font-size: medium"><strong>X</strong></a>
                        </b>
                    </th>
                </tr>
                <tr class="GridviewScrollHeaderBBVA">
                    <th>TAREA</th>
                    <th>STATUS</th>
                    <th>FECHA INICIO</th>
                    <th>FECHA FIN</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repTareas" runat="server">
                    <ItemTemplate>
                        <tr class="GridviewScrollItemBBVA" style="display: table-row;">
                            <td>
                                <asp:Label ID="lblSolicitud" runat="server" Text='<%# Eval("TAREA")%>'> </asp:Label>
                            </td>
                            <td>
                                <input id="imgStatusTarea" type="image" <%# GetImage(Eval("STATUS"))%> onmousedown='<%# GetImageEvent(hdnTask.Value, Eval("LINK"), Eval("STATUS"), Eval("ID_PANTALLA"))%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblAsesor" runat="server" Text='<%# Eval("FECHA_INICIO")%>'> </asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCliente" runat="server" Text='<%# Eval("FECHA_FINAL")%>'> </asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

    <div style="visibility: collapse">
        <asp:Button ID="btnFindTasks" runat="server" OnClick="btnFindTasks_Click" />
        <asp:Button ID="btnHideModals" runat="server" OnClick="btnHideModals_Click" />
        <asp:Button ID="btnShowTasks" runat="server" OnClick="btnShowTasks_Click" />
        <asp:Button ID="btnClean" runat="server" OnClick="btnClean_Click" />
        <asp:Button ID="btnExcecuteAddRequest" runat="server" OnClick="btnExcecuteAddRequest_Click" />
        <asp:Button ID="btnFindRequests" runat="server" OnClick="btnFindRequests_Click" />
        <asp:Button ID="btnDummie" runat="server" />
        <asp:HiddenField ID="hdnTask" runat="server" />
        <asp:HiddenField ID="hdnColNumber" runat="server" />

        <asp:HiddenField ID="hdnAddCompany" runat="server" />
        <asp:HiddenField ID="hdnAddProduct" runat="server" />
        <asp:HiddenField ID="hdnAddLegalPersonality" runat="server" />
        <asp:HiddenField ID="hdnAddDealer" runat="server" />
        <asp:HiddenField ID="hdnPostbackSearch" runat="server" />

        <asp:HiddenField ID="hdnSearchRequestNumber" runat="server" />
        <asp:HiddenField ID="hdnSearchClient" runat="server" />
        <asp:HiddenField ID="hdnSearchInitialDate" runat="server" />
        <asp:HiddenField ID="hdnSearchFinalDate" runat="server" />
        <asp:HiddenField ID="hdnSearchDealer" runat="server" />
        <asp:HiddenField ID="hdnSearchProdeskUser" runat="server" />
    </div>

</asp:Content>

