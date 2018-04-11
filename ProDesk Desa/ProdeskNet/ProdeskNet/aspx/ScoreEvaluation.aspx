<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="ScoreEvaluation.aspx.vb" Inherits="aspx_ScoreEvaluation" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %>

<%--'BBV-P-423: AMR: 12/04/2017 RQADM-18 Evaluación--%>
<%-- BBV-P-423 RQ-PI7-PD8 GVARGAS 18/10/2017 Cambios generales mensajes--%>
<%-- BUG-PD-274 GVARGAS 23/11/2017 Cambio al cerrar Detalle Score.--%>
<%--BUG-PD-336: DJUAREZ: 15/01/2018: Se bloquea la pantalla cuando se esta ejecutando una pantalla automatica.--%>
<%--BUG-PD-397: ERODRIGUEZ: 13/03/2018: Se oculta/muestra animacion procesando.--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
    <script type="text/javascript" language="javascript">
        function PopUpLetreroRedirectSpecial(mesajeRe, redirect) {
            $("#divBlockPage").hide();
            $('#legendCarrusel').html('Respuesta Score');
            $('#ventanaContain').show();
            $('#divDetails').show();
            $("#mensaje_").html(mesajeRe);
            $('#Redirect_').val(redirect);
           
            
        }

        $(document).ready(function () {
            $("#CloseDetail").on("click", function (e) {
                $('#legendCarrusel').html('');
                $('#ventanaContain').hide();
                $('#divDetails').hide();
                $("#tableFields tbody").empty();

                window.location.replace($('#Redirect_').val());
            });
            var idPantalla = getUrlValue("idPantalla");
                       
            var moddiv = document.getElementById("divBlockPage");
            if (moddiv.style.display == 'none') {
                blockScreen(idPantalla);
            }
            else {
                $("#divBlockPage").hide();
            }
                           
                
            
        });
    </script>
    <style>
        .panel {
            height: 75px !Important;
        }
    </style>
    <div class="divPantConsul">
        <div class ="divFiltrosConsul" style="padding-right:5px;" >
            <table class="tabFiltrosConsul">
                <tbody>
                <tr class="tituloConsul">
                    <td colspan="2" style="width:70%;">Hermes</td>
                    <td style="width:30%; text-align:right;"></td>
                    <td>
                        <asp:Button runat="server" ID="btnprocesar" Text="Procesar" CssClass="cssLetras" Visible="false"/>
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
        </div>
    </div>
</asp:Content>

