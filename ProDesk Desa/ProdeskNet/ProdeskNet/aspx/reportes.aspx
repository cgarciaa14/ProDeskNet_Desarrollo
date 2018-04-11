<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="reportes.aspx.vb" Inherits="aspx_reportes" %>

<%--
TRACKERS
BBV-P-423-RQ-INB214:MPUESTO:18/07/2017:Reporte de Proceso de Admision
BBV-P-423-RQ-INB213:MPUESTO:25/07/2017:Reporte de Aspectos Especiales - Mejora en interfaz
BUG-PD-186:MPUESTO:09/08/2017:Correccion de formato de fechas
BUG-PD-202:ERODRIGUEZ:29/08/2017:Se modificaron los parametros enviados para fechas.
--%>


<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">

    <style type="text/css">
        .resulbbvaCenter_local {
            font-family: Arial;
            font-size: 8pt;
            width: 98%;
        }

            .resulbbvaCenter_local table {
                display: block;
                overflow: auto;
                overflow-x: hidden;
            }


            .resulbbvaCenter_local tbody {
                /*display: block;*/
                overflow: auto;
                 /*Se agrego esto*/
                max-height: 120px;
                min-height:80px;
                 /*Se agrego esto*/
                /*overflow-x: hidden;*/
            }

            .resulbbvaCenter_local tr {
                padding-bottom: 3px;
            }

            .resulbbvaCenter_local th {
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

                .resulbbvaCenter_local th:last-child {
                    width: 140px !important;
                }

            .resulbbvaCenter_local td {
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

                .resulbbvaCenter_local td:first-child {
                    width: 60px !important;
                    padding-right: 5px;
                    background-color: #EFEFEF!important;
                }

            .resulbbvaCenter_local th:first-child {
                width: 60px !important;
                padding-right: 5px;
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
                    if ($(this)[0].className.indexOf("dateClass") > -1 || $(this)[0].className.indexOf("hasDatepicker") > -1)
                    {
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

    <div class="divPantConsul">
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
                            <asp:Button ID="btnExportar" runat="server" Text="Exportar" OnClick="btnExportar_Click" CssClass="buttonSecBBVA2" />
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
            <table>
                <asp:Repeater ID="repResult" runat="server">
                    <ItemTemplate>
                        <%# Container.DataItem.ToString() %>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div style="visibility: collapse">
        <asp:HiddenField ID="hdnParams" runat="server" />
        <asp:Button ID="btnConsultar" CssClass="buttonBBVA" runat="server" OnClick="btnConsultar_Click" Text="Consultar" />
    </div>
</asp:Content>


