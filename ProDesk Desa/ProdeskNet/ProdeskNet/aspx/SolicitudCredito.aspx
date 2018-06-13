<%@ Page Language="VB" MasterPageFile="~/aspx/Home.Master" AutoEventWireup="false" CodeFile="SolicitudCredito.aspx.vb" Inherits="aspx_SolicitudCredito" %>

<%@ MasterType VirtualPath="~/aspx/Home.Master" %>
<%-- BUG-PD-408 17/04/2018 EGONZALEZ: PANTALLA DE SOLICITUD DE CREDITO --%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">

    <%--<link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css" crossorigin="anonymous">--%>

    <script type="text/javascript" src="../js/pdkBlanco.js"></script>

    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnCancela
        {
            display: none;
        }
    </style>
    <!--Add CSS to ToolTip -->
    <style>
        .tooltip
        {
            position: relative;
            display: inline-block;
            text-decoration: none;
            border-bottom: 1px dashed #999;
        }

            .tooltip .tooltiptext
            {
                visibility: hidden;
                min-width: 127px;
                max-width: 350px;
                min-height: 13px;
                max-height: 100px;
                background-color: #d9e6f2;
                color: black;
                text-align: center;
                border-radius: 6px;
                padding: 5px 0;
                position: absolute;
                z-index: 1;
                top: 150%;
                left: 50%;
                margin-left: -60px;
            }

                .tooltip .tooltiptext::after
                {
                    content: "";
                    position: absolute;
                    bottom: 100%;
                    left: 50%;
                    margin-left: -5px;
                    border-width: 5px;
                    border-style: solid;
                    border-color: transparent transparent #d9e6f2 transparent;
                }

            .tooltip:hover .tooltiptext
            {
                visibility: visible;
            }

        select[disabled="disabled"], select[disabled]
        {
            color: #979797;
            background-color: rgb(235, 235, 228);
        }

        input[type="button"][disabled="disabled"], input[type="button"][disabled]
        {
            color: #979797;
            background-color: rgb(235, 235, 228);
        }

        input[type="date"]
        {
            border-width: 1px;
            border-color: #000000;
            height: 18px;
        }

            input[type="date"][disabled="disabled"], input[type="date"][disabled]
            {
                color: #979797;
                background-color: rgb(235, 235, 228);
            }

        .txt2BBVA
        {
            width: 170px !important;
        }

        .select2BBVA
        {
            width: 176px!important;
        }

        .campos
        {
            width: 220px !important;
        }

        #ctl00_ctl00_cphCuerpo_cphPantallas_lbl535\|REGIMEN_CONYUGAL
        {
            text-align: right!important;
        }
    </style>
    <%--    <div class="col-md-12 container">
        <div id="pantallaBTSP" runat="server" class="alert alert-info">
        </div>
    </div>--%>

    <div class="divAdminCat">

        <div class="divFiltrosConsul">
            <table>
                <tr>
                    <td class="tituloConsul">
                        <asp:Label ID="lbltitulo" Text="" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

        <div class="divAdminCatCuerpo">

            <div id="pantalla" runat="server" style="position: absolute; top: 0%; left: 0%; width: 100%; height: 100%; overflow: auto;">
            </div>


            <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top: 15%; left: 31%; width: 220px;">
                <asp:Panel ID="popCancela" runat="server" CssClass="cajadialogo">
                    <div class="tituloConsul">
                        <asp:Label ID="Label1" runat="server" Text="Cancelación" />
                    </div>
                    <table width="100%">
                        <tr>
                            <td class="campos">Usuario:</td>
                            <td>
                                <asp:TextBox ID="txtusua" SkinID="txtGeneral" MaxLength="12" runat="server" Style="width: 120px !important;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="campos">Password:</td>
                            <td>
                                <asp:TextBox ID="txtpass" runat="server" SkinID="txtGeneral" MaxLength="12" TextMode="Password" EnableTheming="true" Style="width: 120px !important;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="campos">Descripción:</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <textarea id="TxtmotivoCan" runat="server" onkeypress="ManejaCar('A',1,this.value,this)" class="Text" rows="5" cols="1" style="width: 95% !important;"></textarea>
                            </td>
                        </tr>
                        <tr style="width: 100%">
                            <td>
                                <asp:HiddenField runat="server" ID="HiddenField1" />
                            </td>
                            <td align="left" valign="middle">
                                <input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar(id)" />
                                <input id="btnCancelCancela" type="button" runat="server" value="Cancelar" class="Text " onclick="ocultarCanceDiv()" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>

        <div class="divAdminCatPie">
            <div style="background-color: #F0F0F0;">
                <asp:Image runat="server" ID="Image2" Width="30" Height="30" ImageUrl="~/App_Themes/Imagenes/Alert.png" />
                <label class="cintafooter">Todos los campo marcados con asterisco(*) son obligatorios.</label>
            </div>
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle" class="cssCuerpo">
                        <input id="txtXML" type="text" runat="server" style="display: none" />
                        <input id="txtXSL" type="text" runat="server" style="display: none" />
                        <asp:Button runat="server" ID="btnGuardar" Text="Guardar" Visible="false" />
                        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" />
                        <asp:Button ID="btnImprimir" Text="Imprimir" runat="server" CssClass="buttonSecBBVA2" />
                        <input id="Button1" value="Procesar" type="button" class="buttonBBVA2" onclick="$(this).prop('disabled', true); btnGuardar(id)" runat="server" />
                        <input id="btnCancela" value="Cancelar" type="button" onclick="mostrarCanceDiv()" class="buttonSecBBVA2" runat="server" />
                        <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                        <input id="btnGuardarTMP" value="Guardar" type="button" onclick="btnGuardarTMP()" class="buttonSecBBVA2" runat="server" />
                    </td>
                </tr>
            </table>
        </div>

    </div>



    <asp:HiddenField runat="server" ID="hdnIdRegistro" />
    <asp:HiddenField runat="server" ID="hdnFolio" />
    <asp:HiddenField runat="server" ID="hdnUsuario" />
    <asp:HiddenField runat="server" ID="hdnPantalla" />
    <asp:HiddenField runat="server" ID="hdnResultado" />
    <asp:HiddenField runat="server" ID="hdnResultado1" />
    <asp:HiddenField runat="server" ID="hdnResultado2" />
    <asp:HiddenField runat="server" ID="hdnCp" />
    <asp:HiddenField runat="server" ID="hdnCp1" />
    <asp:HiddenField runat="server" ID="hdnCp2" />
    <asp:HiddenField runat="server" ID="hdnCp3" />
    <asp:HiddenField runat="server" ID="hdnCp4" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
    <asp:HiddenField runat="server" ID="hdnano" />
    <asp:HiddenField runat="server" ID="hdnmes" />
    <asp:HiddenField runat="server" ID="hdndia" />
    <asp:HiddenField runat="server" ID="hdnanocoa" />
    <asp:HiddenField runat="server" ID="hdnmescoa" />
    <asp:HiddenField runat="server" ID="hdndiacoa" />
    <asp:HiddenField runat="server" ID="hdvalidad" />
    <asp:HiddenField runat="server" ID="hdvalidadCoa" />
    <asp:HiddenField runat="server" ID="hdnValiRenta" />
    <asp:HiddenField runat="server" ID="hdnValiRenta1" />
    <asp:HiddenField runat="server" ID="hdnTareaActual" />

</asp:Content>


