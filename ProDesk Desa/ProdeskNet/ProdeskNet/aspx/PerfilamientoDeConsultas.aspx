<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="PerfilamientoDeConsultas.aspx.vb" Inherits="aspx_PerfilamientoDeConsultas" %>

<%-- --BBVA-P-423 RQADM-28 17/05/2017 CGARCIA nuevo formulario de perfilamiento de consultas--%>
<%-- BBVA-P-423 RQXLS1 25/05/2017 CGARCIA agregar div al gridview--%>
<%--BBVA BUG-PD-84 10/06/2017 CGARCIA SE AGREGARON PROPIEDADES AL GRIDVIEW--%>
<%--BBVA BUG-PD-112: 28/06/2017: CGARCIA: SE CAMBIARON LOS CONTROLES DE LA MAYORIA DE LA PANTALLA--%>
<%--BUG-PD-162:MPUESTO:24/07/2017:REEMPLAZO DE PAGINA ANTERIOR POR LA ACTUAL, CORRECCIONES DEL TMO DEL MODULO Y TMO ACUMULADO--%>
<%--BUG-PD-185:JBEJAR:07/08/2017:CORRECIONES EN LA PAGINA AL MOMENTO DE REALIZAR  BUSQUEDA Y VALIDACION A LA BUSQUEDAD DE FECHAS--%>

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
                display: block;
                overflow: auto;
                height: 340px;
                overflow-x: hidden;
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
    <script type="text/javascript">
        function pageLoad() {
            var settingsDate = {
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
            $('#<%=txtFechaInicio.ClientID%>').datepicker(settingsDate).attr('readonly', 'true').attr('onkeydown', 'return false');
            $('#<%=txtFechafin.ClientID%>').datepicker(settingsDate).attr('readonly', 'true').attr('onkeydown', 'return false');
            $('input').on("change paste keyup dragend", function () {
                var dict = { "á": "a", "é": "e", "í": "i", "ó": "o", "ú": "u", "Á": "A", "É": "E", "Í": "I", "Ó": "O", "Ú": "U" };
                $(this).val($(this).val().toString().replace(/[^\w ]/g, function (char) { return dict[char] || char; }));
            });
        }
        function validafecha() {
            fecha1 = $('#<%=txtFechaInicio.ClientID%>').val();  
            var dia1 = fecha1.substr(0, 2);
            var mes1 = fecha1.substr(3, 2);
            var anyo1 = fecha1.substr(6);
            fecha2 = $('#<%=txtFechaFin.ClientID%>').val();
            var dia2 = fecha2.substr(0, 2);
            var mes2 = fecha2.substr(3, 2);
            var anyo2 = fecha2.substr(6);
            var nuevafecha1 = new Date(mes1 + "/" + dia1 + "/" + anyo1);
            var nuevafecha2 = new Date(mes2 + "/" + dia2 + "/" + anyo2);
            var Dif = nuevafecha2.getTime()  - nuevafecha1.getTime() ;
            var dias = Math.floor(Dif / (1000 * 24 * 60 * 60));
            var boton = $('#<%=btnConsultar.ClientID%>');
           
            if (dias < 0 ) {

                alert('La Fecha Inicio no puede ser mayor a la Fecha Fin');

            } else {

                boton.click();
                
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
                                <td colspan="2" style="width: 70%;">
                                    <legend>
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Módulo de Consulta"></asp:Label>
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
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNumeroSolicitud" Text="Número de Solicitud"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbxNumeroSolicitud" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,7);" MaxLength="10"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblNombreSolicitante" Text="Nombre del Solicitante"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbxNombreSolicitante" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,25);"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblRFCSolicitante" Text="RFC"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbxRFCSolicitante" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,22);"></asp:TextBox>
                    </td>
                    <td></td>
                    <td>


                          <input type="button" value="Buscar" class="buttonBBVA2" onclick="validafecha()" runat="server" id="btnBuscar" />
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblFechaInicio" Text="Fecha Inicio"></asp:Label>
                    </td>
                    <td>
                        <input runat="server" type="text" id="txtFechaInicio" class="txt3BBVA" />
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFechaFin" Text="Fecha Fin"></asp:Label>
                    </td>
                    <td>
                        <input runat="server" type="text" id="txtFechaFin" class="txt3BBVA" />
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblAsesor" Text="Asesor"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbxAsesor" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnLimpiar" runat="server" CssClass="buttonSecBBVA2" Text="Limpiar" OnClick="btnLimpiar_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <div class="resulbbvaCenter_local" id="divTableResult" runat="server" style="height: 70%;">
            <table id="tableResult" style="font-size: 12px;" cellpadding="0" cellspacing="0" border="0">
                <thead>
                    <tr class="GridviewScrollHeaderBBVA_local">
                        <th>Número de solicitud</th>
                        <th>Nombre del solicitante</th>
                        <th>RFC del solicitante</th>
                        <th>Nombre del Asesor</th>
                        <th>Fecha de ingreso</th>
                        <th>Hora de ingreso</th>
                        <th>Agencia</th>
                        <th>Canal</th>
                        <th>Marca</th>
                        <th>Submarca</th>
                        <th>Monto total solicitado</th>
                        <th>Módulo actual</th>
                        <th>Estatus del caso</th>
                        <th>TMO del módulo actual</th>
                        <th>TMO acumulado</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repResult" runat="server">
                        <ItemTemplate>
                            <tr class="GridviewScrollItemBBVA_local">
                                <td>
                                    <asp:Label ID="lblNoSolicitud" runat="server" Text='<%# CutText(Eval("No Solicitud"))%>' ToolTip='<%# Eval("No Solicitud")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNombreSolicitante" runat="server" Text='<%# CutText(Eval("Solicitante"))%>' ToolTip='<%# Eval("Solicitante")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblRFC" runat="server" Text='<%# CutText(Eval("RFC"))%>' ToolTip='<%# Eval("RFC")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAsesor" runat="server" Text='<%# CutText(Eval("Asesor"))%>' ToolTip='<%# Eval("Asesor")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFechaIngreso" runat="server" Text='<%# GetPartOfTime(Eval("Fecha Inicio"), True)%>' ToolTip='<%# GetPartOfTime(Eval("Fecha Inicio"), True)%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblHoraIngreso" runat="server" Text='<%# GetPartOfTime(Eval("Fecha Inicio"), False)%>' ToolTip='<%# GetPartOfTime(Eval("Fecha Inicio"), False)%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAgencia" runat="server" Text='<%# CutText(Eval("Agencia"))%>' ToolTip='<%# Eval("Agencia")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCanal" runat="server" Text='<%# CutText(Eval("Canal"))%>' ToolTip='<%# Eval("Canal")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMarca" runat="server" Text='<%# CutText(Eval("Marca"))%>' ToolTip='<%# Eval("Marca")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSubmarca" runat="server" Text='<%# CutText(Eval("Submarca"))%>' ToolTip='<%# Eval("Submarca")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblMontoSolicitado" runat="server" Text='<%# CutText(Eval("Monto Solicitado"))%>' ToolTip='<%# Eval("Monto Solicitado")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblModuloActual" runat="server" Text='<%# CutText(Eval("Estatus Tarea"))%>' ToolTip='<%# Eval("Estatus Tarea")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEstatusModulo" runat="server" Text='<%# CutText(Eval("Estatus Credito"))%>' ToolTip='<%# Eval("Estatus Credito")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTMOModulo" runat="server" Text='<%# CutText(Eval("TMO Modulo"))%>' ToolTip='<%# Eval("TMO Modulo")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTMOAcuulado" runat="server" Text='<%# CutText(Eval("TMO Acumulado"))%>' ToolTip='<%# Eval("TMO Acumulado")%>'> </asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

        <div style="visibility: collapse">  
            <asp:Button ID="btnConsultar" runat="server" class="buttonBBVA2" Text="Consultar"  OnClick="btnConsultar_Click" />
        </div>

        <div class="resulbbvaCenter" id="divEmptyTableResult" runat="server" style="text-align: center">
            <br />
            <br />
            - No existen datos para Mostrar -
        </div>

    </div>
</asp:Content>

