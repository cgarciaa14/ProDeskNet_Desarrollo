<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="ValidacionImpagos.aspx.vb" Inherits="aspx_ValidacionImpagos" %>
<%--BBVA-P-423 RQ-INB226 GVARGAS 04/07/2017 Validación de Impagos (RV02)--%>
<%--BBV-P-423 RQ-PI-7-PD-14 GVARGAS 22/11/2017 Detalle Impagos--%>
<%--BUG-PD-292 CChavez 05/12/2017 Modificación mensaje de Impagos--%>



<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
        <script type="text/javascript" language="javascript">
            function PopUpLetreroRedirectSpecial(mesajeRe, redirect, JSON_Impagos, regs) {
                var title_ = 'Resumen Impagos.';

                $('#legendCarrusel').html(title_.toString());
                $('#ventanaContain').show();
                $('#divDetails').show();
                $("#mensaje_").html(mesajeRe);
                $('#Redirect_').val(redirect);

                if (regs < 0) {  // Se cambia comparacion para nunca entrar al ciclo que muestra cambios,si desea  entrar al codigo  solo poner >0
                    var items = $.parseJSON(JSON_Impagos);
                    var headers_ = items["ImpagoHeaders"];
                    var impagos_ = items["ImpagoAmounts"];

                    var $table = $("<table>").addClass("fieldsetBBVA");
                    var $tbody = $("<tbody>").appendTo($table);

                    var $tr = $("<tr>");

                    $.each(headers_, function (i, item) { $("<th>").html(item.toString()).appendTo($tr); });

                    $tr.appendTo($tbody);

                    $.each(impagos_, function (i, item) {
                        var $tr_ = $("<tr>");
                        
                        $("<td>").html(item.Detalle.toString()).appendTo($tr_);
                        $("<td>").html(item.Monto.toString()).appendTo($tr_);
                        $tr_.appendTo($tbody);
                    });

                    $("#imagosTBL").append($table);
                    $("#divDetails").css({ "height" : "125px" });
                }
            }

            $(document).ready(function () {
                $("#CloseDetail").on("click", function (e) {
                    $('#legendCarrusel').html('');
                    $('#ventanaContain').hide();
                    $('#divDetails').hide();
                    $("#tableFields tbody").empty();

                    window.location.replace($('#Redirect_').val());
                });
            });
    </script>
    <style>
        .panel {
            height: 75px;
        }

        .fieldsetBBVA {
            width: 100% !Important;
        }
    </style>
    <div class="divPantConsul">
        <div class ="divFiltrosConsul" style="padding-right:5px;" >
            <table class="tabFiltrosConsul">
                <tbody>
                    <tr class="tituloConsul">
                        <td colspan="2" style="width:70%;">VALIDACIÓN IMPAGOS</td>
                        <td style="width:30%; text-align:right;"></td>
                        <td>
                            <asp:Button runat="server" ID="btnprocesar" Text="Procesar" CssClass="cssLetras"  Visible="false"/>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div id="divDetails" class = "panel" style="z-index: 1001; display: none; top: 148px; left: 140px;">
        <input type="hidden" id="Redirect_" />
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
                        <th class="campos" style="width: 100%">
                            <asp:Label ID="mensaje_" Font-Bold="true" Font-Underline="true" />
                        </th>
                    </tr>
                </table>
            </div>
            <div id="imagosTBL" class="pane" style="width: 50%;">
            </div>
        </div>
    </div>
</asp:Content>
