<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="reportes.aspx.vb" Inherits="aspx_reportes" %>

<%--
TRACKERS
BBV-P-423-RQ-INB214:MPUESTO:18/07/2017:Reporte de Proceso de Admision
BBV-P-423-RQ-INB213:MPUESTO:25/07/2017:Reporte de Aspectos Especiales - Mejora en interfaz
BUG-PD-186:MPUESTO:09/08/2017:Correccion de formato de fechas
BUG-PD-202:ERODRIGUEZ:29/08/2017:Se modificaron los parametros enviados para fechas.
BUG-PD-410: DJUAREZ: 03/04/2018: Se realiza paginado para el reporte Proceso de admision
BUG-PD-433: JBEJAR:  08/05/2018: Optimizacion reporte de admision
RQ-PC9: CGARCIA: 02/05/2018: SE MODIFICA REPORTE DE ADMISION , NOTAS EXTERNAS EN CADA SOLICITUD
--%>


<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">

    <style type="text/css">
        .resulbbvaCenter_local
        {
            font-family: Arial;
            font-size: 8pt;
            width: 98%;
        }

            .resulbbvaCenter_local table
            {
                display: block;
                overflow: auto;
                overflow-x: hidden;
            }


            .resulbbvaCenter_local tbody
            {
                /*display: block;*/
                overflow: auto;
                /*Se agrego esto*/
                max-height: 120px;
                min-height: 80px;
                /*Se agrego esto*/
                /*overflow-x: hidden;*/
            }

            .resulbbvaCenter_local tr
            {
                padding-bottom: 3px;
            }

            .resulbbvaCenter_local th
            {
                width: 107px;
                font-family: Arial;
                font-size: 8pt;
                font-weight: bold!important;
                background-color: White;
                color: #666666;
                border-top: 1px solid White;
                border-left: 1px solid White;
                border-right: 1px solid White;
                border-bottom: 1px solid #D8D8D8;
                text-align: center;
            }

                .resulbbvaCenter_local th:last-child
                {
                    width: 140px !important;
                }

            .resulbbvaCenter_local td
            {
                width: 115px !important;
                font-family: Arial;
                font-size: 8pt;
                background-color: White;
                color: #666666;
                border-top: 1px solid White;
                border-bottom: 1px solid White;
                border-left: 1px solid White;
                border-right: 1px solid White;
                text-align: center;
            }

                .resulbbvaCenter_local td:first-child
                {
                    width: 60px !important;
                    padding-right: 5px;
                    background-color: #EFEFEF!important;
                }

            .resulbbvaCenter_local th:first-child
            {
                width: 60px !important;
                padding-right: 5px;
            }

        .page_enabled, .page_disabled
        {
            display: inline-block;
            height: 20px;
            min-width: 20px;
            line-height: 20px;
            text-align: center;
            text-decoration: none;
            border: 1px solid #ccc;
        }

        .page_enabled
        {
            background-color: #eee;
            color: #000;
        }

        .page_disabled
        {
            background-color: #6C6C6C;
            color: #fff !important;
        }

        a {
            color: blue;
            font-style: italic;
        }

        /* visited link */
        a:visited {
            color: green;
        }

        /* mouse over link */
        a:hover {
            color: hotpink;
        }

        /* selected link */
        a:active {
            color: blue;
        }
    </style>


    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" language="javascript">
        function pageLoad() {
            var settingsDate = {
                //BUG-PD-186
                dateFormat: "dd/mm/yy",
                showAnim: "slide",
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                selectOtherMonths: true,
                autoSize: true

            };
            $.datepicker.setDefaults($.datepicker.regional["es"]);

            var myDatePickers = $(".dateClass");
            $.each(myDatePickers, function () {
                $(this).datepicker(settingsDate).attr('readonly', 'true').attr('onkeydown', 'return false');
            });

            $('input').on("change paste keyup dragend", function () {
                var dict = { "á": "a", "é": "e", "í": "i", "ó": "o", "ú": "u", "Á": "A", "É": "E", "Í": "I", "Ó": "O", "Ú": "U" };
                $(this).val($(this).val().toString().replace(/[^\w ]/g, function (char) { return dict[char] || char; }));
            });
        }

        function btnConsultarCliente_Click() {
            var strParams = '';
            var myInputs = $(".inputParams");
            $.each(myInputs, function () {
                if ($(this)[0].value.toString() != "") {
                    strParams += $(this)[0].id.toString();
                    if ($(this)[0].className.indexOf("dateClass") > -1 || $(this)[0].className.indexOf("hasDatepicker") > -1) {
                        var dd = $(this)[0].value.toString().substring(0, 2);
                        var mm = $(this)[0].value.toString().substring(3, 5);
                        var yy = $(this)[0].value.toString().substring(6, 10);
                        strParams += '=' + mm + "/" + dd + "/" + yy;
                    } else {
                        strParams += '=' + $(this)[0].value.toString();
                    }

                    strParams += '|';
                }
            })
            strParams = strParams == "" ? "" : strParams.substring(0, strParams.length - 1);
            $('#<%=hdnParams.ClientID%>').val(strParams);
            $('#<%=btnConsultar.ClientID%>').click();

        }

    </script>
    <script type="text/javascript">
        function fnProcesar_() {
            var moddiv = document.getElementById("divBlockPage");
            if (moddiv.style.display == 'none') {
                blockScreen(1);
            }
            else {
                $("#divBlockPage").hide();
            }
        }

        function fnCierraProcesar() {
            $("#divBlockPage").hide();
        }

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            if (args.get_error() && args.get_error().name === 'Sys.WebForms.PageRequestManagerTimeoutException') {
                args.set_errorHandled(true);
            }
        });
    </script>
    

    <div class="divPantConsul" style="overflow-y: scroll">
        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width: 70%;">
                                    <legend>Generación de Reportes
                                    </legend>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divFiltros" class="resulbbvaCenter" runat="server">
            <table>
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblSeleccionReporte" runat="server" Text="Elija un reporte" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSeleccionReporte" runat="server" Width="350" OnSelectedIndexChanged="ddlSeleccionReporte_SelectedIndexChanged" AutoPostBack="true" />
                        </td>
                        <td></td>
                        <td>
                            <input type="button" runat="server" id="btnConsultarCliente" value="Consultar" onclick="btnConsultarCliente_Click();" class="buttonBBVA" />
                        </td>
                        <td>
                            <asp:Button ID="btnExportar" runat="server" Text="Exportar" OnClick="btnExportar_Click" CssClass="buttonSecBBVA2" OnClientClick="fnProcesar_();" />
                        </td>
                        <td>
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" CssClass="buttonSecBBVA2" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <br />
            <table>
                <tbody>
                    <asp:Repeater ID="repFiltros" runat="server">
                        <ItemTemplate>
                            <tr>
                                <%# Container.DataItem.ToString() %>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <br />
        <br />
        <br />
        <div id="divResult" class="resulbbvaCenter_local" runat="server" style="height: 80%; overflow-x: scroll">
            <asp:GridView ID="gvAdmision" runat="server" AutoGenerateColumns="false"
                HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowPaging="true" PageSize="100"
                Width="100%" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="gvAdmision_PageIndexChanging"
                EmptyDataText="No se encontró información">
                <Columns>
                    <asp:TemplateField HeaderText="No Solicitud" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate> 
                            <asp:LinkButton ID="lnkSolicitud" runat="server" CssClass="resul" CommandName="SolicitudID" CommandArgument='<%# Eval("No Solicitud")%>'><%#Eval("No Solicitud") %></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="No Solicitud" HeaderText="No Solicitud" ItemStyle-HorizontalAlign="Center" />--%>
                    <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="RFC" HeaderText="RFC" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Fecha Inicio" HeaderText="Fecha Inicio" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Fecha Final" HeaderText="Fecha Final" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Agencia" HeaderText="Agencia" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Alianza" HeaderText="Alianza" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Canal" HeaderText="Canal" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Submarca" HeaderText="Submarca" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Monto Solicitado" HeaderText="Monto Solicitado" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Usuario Mod" HeaderText="Usuario Mod" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Nombre Usuario Mod" HeaderText="Nombre Usuario Mod" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Tarea" HeaderText="Tarea" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Estatus Tarea" HeaderText="Estatus Tarea" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Estatus Credito" HeaderText="Estatus Credito" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="TMO Modulo" HeaderText="TMO Modulo" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="TMO Acumulado" HeaderText="TMO Acumulado" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Num Credito" HeaderText="Número de crédito" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <div id="divResultExport" class="resulbbvaCenter_local" runat="server" style="display: none;">

            <table>
                <asp:Repeater ID="repResultExport" runat="server">
                    <ItemTemplate>
                        <%# Container.DataItem.ToString() %>
                    </ItemTemplate>
                </asp:Repeater>
            </table>

        </div>
        <div id="dvPag">
            <%--  <asp:Repeater ID="rptPager" runat="server">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                        CssClass='<%# If(Convert.ToBoolean(Eval("Enabled")), "page_enabled", "page_disabled")%>'
                        OnClick="Page_Changed" OnClientClick='<%# If(Not Convert.ToBoolean(Eval("Enabled")), "return false;", "") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>--%>
        </div>
    </div>
    <div style="visibility: collapse">
        <asp:HiddenField ID="hdnParams" runat="server" />
        <asp:Button ID="btnConsultar" CssClass="buttonBBVA" runat="server" OnClick="btnConsultar_Click" Text="Consultar" OnClientClick="fnProcesar_();" />
    </div>
</asp:Content>


