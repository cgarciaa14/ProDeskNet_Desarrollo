<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="ExpedienteDoc.aspx.vb" Inherits="aspx_ExpedienteDoc" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
   <%-- -- RQ-INB203INB204INB209 JBEJAR: 18/07/2017  SPRINT 15 DOCUMENTOS ADICIONALES AGENCIA. --%>
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

    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" language="javascript">

        function pageLoad() {
            var settingsDateNetMessages = {
                dateFormat: "dd/mm/yy",
                showAnim: "slide",
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                selectOtherMonths: true,
                autoSize: true,
                yearRange: "-99:+0",
                maxDate: "+0m +0d"
            }

            $.datepicker.setDefaults($.datepicker.regional["es"]);

            $('#<%=txtFechaInicio.ClientID %>').datepicker(settingsDateNetMessages).attr('readonly', 'true').attr('onkeydown', 'return false');

            $('#<%=txtFechaFin.ClientID%>').datepicker(settingsDateNetMessages).attr('readonly', 'true').attr('onkeydown', 'return false');

            $('input').on("change paste keyup dragend", function () {
                var dict = { "á": "a", "é": "e", "í": "i", "ó": "o", "ú": "u", "Á": "A", "É": "E", "Í": "I", "Ó": "O", "Ú": "U" };
                $(this).val($(this).val().toString().replace(/[^\w ]/g, function (char) { return dict[char] || char; }));
            });
        }

        function btnVisorDocumentalCliente_click(NumeroSolicitud){
            $('#<%=hdnNumeroSolicitud.ClientID%>').val(NumeroSolicitud);
            $('#<%=btnVisorDocumental.ClientID%>').click();
        }

        function btnBuscarCliente_Click()
        {
            var dateIni = $('#<%= txtFechaInicio.ClientID%>').val().split('/');
            var dateFin = $('#<%= txtFechaFin.ClientID%>').val().split('/');
            dateIni = dateIni[2] + '/' + dateIni[1] + '/' + dateIni[0];
            dateFin = dateFin[2] + '/' + dateFin[1] + '/' + dateFin[0];
            dateIni = new Date(dateIni);
            dateFin = new Date(dateFin);
            if(dateIni>dateFin)
            {
                alert('La fecha de inicio no puede ser mayor a la fecha de fin');
            }
            else
            {
                $('#<%=btnBuscar.ClientID%>').click()
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
                                    <legend>Expediente documental 
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
                    <td></td>

                    <td>
                        <input type="button" id="btnBuscarCliente" class="buttonBBVA2" onclick="btnBuscarCliente_Click();" value="Buscar">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNombreCliente" Text="Nombre del Cliente"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbxNombreCliente" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,25);"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblRFCCliente" Text="RFC"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="tbxRFCCliente" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,22);"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnLimpiarResultados" CssClass="buttonSecBBVA2" OnClick="btnLimpiarResultados_Click" Text="Limpiar" />
                    </td>

                </tr>
            </table>
        </div>

        <div class="resulbbvaCenter_local" id="divTableResult" runat="server" style="height: 70%;">
            <table id="tableResult" style="font-size: 12px;" cellpadding="0" cellspacing="0" border="0">
                <thead>
                    <tr class="GridviewScrollHeaderBBVA_local">
                        <th>Solicitud</th>
                        <th>Nombre Cliente</th>
                        <th>RFC</th>
                        <th>Agencia</th>
                        <th>Alianza</th>
                        <th>Canal</th>
                        <th>Marca</th>
                        <th>Submarca</th>
                        <th>Monto Solicitado</th>
                        <th>Tarea Actual</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repSolicitudes" runat="server">
                        <ItemTemplate>
                            <tr class="GridviewScrollItemBBVA_local">
                                <td>
                                    <asp:Label ID="Label0" runat="server" Text='<%# CutText(Eval("NumeroSolicitud"))%>' ToolTip='<%# Eval("NumeroSolicitud")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text='<%# CutText(Eval("NombreCliente"))%>' ToolTip='<%# Eval("NombreCliente")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text='<%# CutText(Eval("RFC"))%>' ToolTip='<%# Eval("RFC")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text='<%# CutText(Eval("NombreAgencia"))%>' ToolTip='<%# Eval("NombreAgencia")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="label4" runat="server" Text='<%# CutText(Eval("Alianza"))%>' ToolTip='<%# Eval("Alianza")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text='<%# CutText(Eval("NombreCanal"))%>' ToolTip='<%# Eval("NombreCanal")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text='<%# CutText(Eval("NombreMarca"))%>' ToolTip='<%# Eval("NombreMarca")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text='<%# CutText(Eval("NombreSubmarca"))%>' ToolTip='<%# Eval("NombreSubmarca")%>'> </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text='<%# CutText(Eval("MontoSolicitado"))%>' ToolTip='<%# Eval("MontoSolicitado")%>'> </asp:Label>
                                </td>
                                <td>
                                    <a  title='<%# Eval("TareaActual") + ", Estado: " + Eval("StatusTareaActual")%>' >
                                        <%# CutText(Eval("TareaActual"))%></a>
                                </td>
                                <td>
                                    <input type="button" id="btnVisorDocumentalCliente" onclick="btnVisorDocumentalCliente_click(<%# Eval("NumeroSolicitud")%>);" class="buttonSecBBVA2" value="Visor Documental" />
                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("NumeroSolicitud") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

        <div class="resulbbvaCenter" id="divEmptyTableResult" runat="server" style="text-align: center">
            <br />
            <br />
            <br />
            <br />
            - No existen datos para Mostrar -
        </div>

      


        <div style="visibility: collapse">
            <asp:Button runat="server" ID="btnVisorDocumental" OnClick="btnVisorDocumental_Click" />
            <asp:HiddenField ID="hdnNumeroSolicitud" runat="server" />
            <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="buttonBBVA2" OnClick="btnBuscar_Click" />
        </div>

    </div>

</asp:Content>
 