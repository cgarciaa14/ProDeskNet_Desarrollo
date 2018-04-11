<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="CalculoDeIngresos.aspx.vb" maintainScrollPositionOnPostBack="true"  Inherits="aspx_CalculoDeIngresos" %>

<%@ MasterType VirtualPath="~/aspx/home.master" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%--BBV-P-423: RQADM-14:AVH: 17/03/2017 CALCULO DE INGRESOS--%>
<%--BBV-P-423: RQADM-22 JRHM: 24/03/2017 SE MODIFICA CODIGO ASP--%>
<%--BUG-PD-22: JBB 28/03/2017 se agregan validaciones a la pantalla calculo de ingresos y se regresa el boton en el div collapse--%>
<%--BBV-P-423-RQADM-09 JBB 04/04/2017 Correciones a la pantalla de calculo de ingresos.--%>
<%--BBVA-P-423: RQCONYFOR-07_2: AVH: 11/04/2017 modificaciones JBEJAR--%>
<%--BUG-PD-41 JBB:  27/04/2017 Se amplian los digitos de 6 a 8 mantiendo los 2 digitos--%>
<%--BUG-PD-55 JBEJAR:  25/05/2017 Correciones al guardar el total del calculo --%>
<%--BUG-PD-64 JBEJAR:  26/05/2017 validaciones en el calculo de ingreso  --%>
<%--BUG-PD-71 JBEJAR:  05/06/2017 correciones calculo de ingresos  --%>
<%--BUG-PD-80 JBEJAR:  09/06/2017 Todas las percepciones nullas se iran a 0--%> 
<%--BUG-PD-97 JBEJAR:  22/06/2017 Correciones en la matriz de calculo de ingresos--%>
<%--BUG-PD-108 JBEJAR: 29/06/2017 Rework calculo de ingresos.  --%>
<%--BUG-PD-125 JBEJAR: 01/07/2017 Calculo de ingresos cfdi.--%>
<%--BUG-PD-141 GVARGAS: 05/07/2017 Correcion Overflow-Y.--%>
<%--BUG-PD-124 ERODRIGUEZ 06/07/2017 Se agregó botón para reporte. --%>
<%--BUG-PD-186:MUESTO:09/08/2017: Homologación de etiquetas a mayúsculas con minúsculas--%>
<%--BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN--%>
<%--BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS PARA ABRIR VISOR TELEPRO--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
    <style>
        .cajadialogo_local {
            background-color: #F2F2F2;
            border-width: 4px;
            border-style: outset;
            border-color: #0080FF;
            padding: 0px;
            width: 500px;
            font-weight: bold;
            font-style: italic;
            margin: 5px;
            text-align: center;
        }

        .divAdminCatPie {
            top: 95% !important;
        }

        .divPantConsul {
            height: 90% !important;
            max-height: 94% !important;
            overflow-y: auto;
        }
    </style>
    <script src="../js/jquery.ui.widget.js"></script>
    <script src="../js/jquery.iframe-transport.js"></script>
    <script src="../js/jquery.fileupload.js"></script>
    <script src="../js/Funciones.js"></script>
    <script type="text/javascript" src="../js/Funciones.js"></script>

    <script type="text/javascript" language="javascript">

        function oprimeboton() {
            var boton = document.getElementById('<%= btnCancelar.ClientID%>');
            boton.click();
        }
        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();
        }



        function CFDIformat(evt, objSource) {
            var result = false;
            var charCode = (evt.which) ? evt.which : evt.KeyCode
            result = ValCarac(evt, 22);
            if (result) {
                if ($('#' + objSource.id).val().length == 8 ||
                    $('#' + objSource.id).val().length == 13 ||
                    $('#' + objSource.id).val().length == 18 ||
                    $('#' + objSource.id).val().length == 23) {
                    if (charCode != 8 && charCode != 46) {
                        $('#' + objSource.id).val($('#' + objSource.id).val() + "-");
                    }
                }
                else {
                }
            }
            return result;
        }
        /*       function ValidaRfc(rfcStr) {
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

        function btnValidar_Click() {


            var hasError = false;
            var textResult = 'Información validación cfdi:';
            var longitud = 0
            var valorI = $(".cfdicampo");
            $.each(valorI, function () {
                if ($(this)[0].title != "") {
                    if ($(this).val() == "") {



                        textResult += "\nFalta información en el campo " + $(this)[0].title;

                        hasError = true;
                    }

                }
            });
            longitud = $('#<%=txtFolioFiscal1.ClientID%>').val();

            $('#ventanaContain').hide();
            $("#divcancela").hide();

            if (longitud.length < 36) {

                textResult += "\nLa longitud del folio fiscal no puede ser menor a 36";
                hasError = true;
            }

            if (hasError == true) {

                alert(textResult);
            }

            if (hasError == false) {

                var botonvalidar = $('#<%= btnValidar1.ClientID%>');
                botonvalidar.click();
            }


        }

        function ValidaRfc(rfcStr) {
            var strCorrecta;
            strCorrecta = rfcStr;
            if (rfcStr.length != 10 && rfcStr.length != 13) {
                return '\n + El RFC debe ser de 10 o 13 caracteres.';
            } else {
                if (!RFC.match(/^([A-ZÑ\x26]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))((-)?([A-Z\d]{3}))?$/)) {
                    if (RFC.length == 10) {
                        return '\n + El RFC debe tener formato "AAAA999999".';
                    }
                    else {
                        return '\n + El RFC debe tener formato "AAAA999999***".';
                    }
                }
            }
            return '';
        }
        ////function pageLoad() {


        ////    function getVScroll() {
        ////        document.getElementById('tamaño').value = document.getElementById('Div2').scrollTop;
        ////    }

        ////    function setVScroll() {
        ////        document.getElementById('tamaño').scrollTop = document.getElementById('scroll').value;
        ////    }

        //}
        function btnguardarCliente_click() {

            var guardar = $('#<%=btnGuardar.ClientID%>');

            guardar.click();
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

        function btncalcular() {

            var depende = '';
            var hasError = false;
            var textResult = 'Información de la solicitud:';
            var longitud = 0
            var valorI = $(".txt2");
            $.each(valorI, function () {
                if ($(this)[0].title != "") {
                    if ($(this).val() == "") {


                        //longitud = valorI.length
                        //textResult += "\nFalta información en el campo " + $(this)[0].id.replace("txt", "");
                        hasError = false;
                    }
                }
            });

            if (hasError == true) {

                //$(this).val(0); BUG-PD-97  evita enviar los valores en 0 de la matriz de calculo de ingresos. 


            }


            if (hasError == false) {
                var botonCalcular = $('#<%= btnCalcularIngresos.ClientID%>');
                    botonCalcular.click();
                }

            }
        function fnChequea() {
            fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');
                    PopUpLetrero("Documento procesado exitosamente");
        }

        $(document).ready(function () { $.urlParam = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } }; if ($.urlParam("Enable").toString() == "1") { $("#btnCancelarNew").hide(); }; });


    </script>
    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnCancelar {
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
                                <td>
                                    <legend>
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Cálculo de Ingresos"></asp:Label></legend>
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

        <br />

        <table width="100%" class="fieldsetBBVA">
            <tr>
                <th class="campos" style="width: 25%">Solicitud:                    
                </th>
                <th class="campos" style="width: 25%">
                    <asp:Label ID="lblSolicitud" Font-Underline="true" runat="server">
                    </asp:Label>
                </th>
                <th class="campos" style="width: 25%">Cliente:                    
                </th>
                <th class="campos" style="width: 25%">
                    <asp:Label ID="lblCliente" Font-Underline="true" runat="server">
                    </asp:Label>
                </th>
            </tr>
            <tr>
                <th class="campos">Status Credito:                    
                </th>
                <th class="campos">
                    <asp:Label ID="lblStCredito" Font-Underline="true" runat="server">
                    </asp:Label>
                </th>
                <th class="campos">Status Documentos:                    
                </th>
                <th class="campos">
                    <asp:Label ID="lblStDocumento" Font-Underline="true" runat="server">
                    </asp:Label>
                </th>
            </tr>
        </table>

        <div id="Div1" class="resulbbvaCenter" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblTipoActividad" Text="Tipo de Actividad Económica *"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlTipoActividad" CssClass="select2BBVA" OnSelectedIndexChanged="ddlTipoActividad_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblTipoReciboAsalariado" Text="Tipo de Recibo Asalariado *"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlTipoReciboAsalariado" CssClass="select2BBVA"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblRecibosAsalariado" Text="Recibos Asalariado*"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlRecibosAsalariado" CssClass="select2BBVA" OnSelectedIndexChanged="ddlRecibosAsalariado_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0" Selected="True"><SELECCIONAR></asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                            <asp:ListItem>13</asp:ListItem>
                            <asp:ListItem>14</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblPeriodoPago" Text="Periodo de Pago *"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlPeriodoPago" CssClass="select2BBVA" OnSelectedIndexChanged="ddlPeriodoPago_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <asp:Label runat="server" ID="lblDiasPeriodo" Text="0" Visible="False"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblTolalIngresos" Text="Total Ingresos*"></asp:Label></td>

                    <td>
                        <asp:TextBox runat="server" ID="txtTotalIngresos" CssClass="txt3BBVA" Enabled="false"></asp:TextBox>
                        <%--<asp:TextBox runat="server" ID="txtTotalIngresos" CssClass="txt3BBVA" Enabled="True" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox></td>--%>
                        <td>
                            <asp:Label runat="server" ID="lblTipoReciboNoAsalariado" Text="Tipo de Recibo No Asalariado *"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlTipoReciboNoAsalariado" CssClass="select2BBVA"></asp:DropDownList>
                        </td>

                        <td>
                            <asp:Label runat="server" ID="lblRecibosNoAsalariado" Text="Recibos No Asalariado *"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlRecibosNoAsalariado" CssClass="select2BBVA" OnSelectedIndexChanged="ddlRecibosNoAsalariado_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0" Selected="True"><SELECCIONAR></asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <%--<asp:Label runat="server" ID="lblDias" Text="Días que Comprende el Periódo de Pago *"></asp:Label>--%>
                        </td>
                        <td>
                            <%--<asp:TextBox runat="server" ID="txtDiasPeriodoPago" Text="0" CssClass="txt3BBVA" onkeypress="return ValCarac(event,7);" MaxLength="3" AutoPostBack="true"></asp:TextBox>--%>
                            <asp:Label runat="server" ID="lblTotalPercepcionesAsalariado" Text="0" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblTotalPercepcionesNoAsalariado" Text="0" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblMenor" Text="0" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblMayor" Text="0" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblRegla" Text="0" Visible="false"></asp:Label>
                            <asp:Button runat="server" ID="btnVisor" Text="Visor Documental" CssClass="buttonSecBBVA2" OnClick="btnVisor_Click" />
                        </td>
                </tr>
                <tr>

                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
         <div style="visibility: collapse">
              <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClientClick="mostrarCanceDiv()" CssClass="buttonSecBBVA2" />
             <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
         </div>
         <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top:15%; left:31%; width: 297px;">
         <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID="btnCancelar" PopupControlID="validacfdi" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>--%>
           <%--<asp:Panel ID="validacfdi" runat="server" CssClass="cajadialogo_local">
       </asp:Panel>--%>
            <div class="tituloConsul">
            </div>
                <asp:Label ID="Label11" runat="server" Text="Validación CFDI" />
            <table width="100%">
                <tr>
                    <%--BUG-PD-186:MUESTO:09/08/2017: Homologación de etiquetas a mayúsculas con minúsculas--%>
                    <td class="campos"><asp:Label runat="server" ID="Label21" Text="RFC Emisor *"></asp:Label></td>
                    <td>
                      <input type="text" runat="server" onpaste="return false" oncut="return false" oncopy="return false" class="txt3BBVA cfdicampo" id="txtRFCE1"  onkeypress="return ValCarac(event,22);" onblur="upperCaseString(this);" maxlength="13" Style="width: 220px !important;" Title="RFC Emisor *" />
                    </td>
                </tr>
                <tr>
                    <td class="campos"><asp:Label runat="server" ID="Label31" Text="RFC Receptor *"></asp:Label></td>
                    <td>
                    <%--BUG-PD-186:MUESTO:09/08/2017: Homologación de etiquetas a mayúsculas con minúsculas--%>
                         <input type="text" runat="server" id="txtRFCR1" class="txt3BBVA cfdicampo" onpaste="return false" oncut="return false" oncopy="return false" onkeypress="return ValCarac(event,22);" onblur="upperCaseString(this);" maxlength="13" Style="width: 220px !important;" Title="RFC Receptor *"/>
                    </td>
                </tr>
                <tr>
                    <td class="campos"> <asp:Label runat="server" ID="lblFolioFiscal" Text="Folio Fiscal *"></asp:Label></td>
                    <td>
                       <input type="text" runat="server" id="txtFolioFiscal1" onkeypress="return CFDIformat(event, this);" onkeyup="setNewText(event, this);" class="txt3BBVA cfdicampo" onblur="upperCaseString(this);" maxlength="36" Style="width: 220px !important;" Title="Folio Fiscal *"/>
                    </td>
                </tr>
                <tr>
                    <td class="campos"<asp:Label runat="server" ID="Label4" Text="Cantidad Total *"></asp:Label></td>
                    <td>
                          <input type="text" runat="server" onpaste="return false" oncut="return false" oncopy="return false" id="txtTotal1"  onkeypress="return ValCarac(event,9);" class="txt3BBVA cfdicampo"  maxlength="13" Style="width: 220px !important;" Title="Cantidad Total *"/>
                    </td>
                </tr>
            
                <tr style="width: 100%">
                    <td>
                        <asp:HiddenField runat="server" ID="HiddenField11" />
                        <br />
                    </td>
                    <td align="center" valign="middle">
                       <input type="button" runat="server" id="btnAdelantarCliente" value="Procesar" onclick="btnValidar_Click();" />
                      <br/>
                      <br />
                        <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral" />
                    </td>
                </tr>
            </table>
    </div>
        <asp:Label ID="lblAsalariados" runat="server" Text="Asalariados" class="tituloConsul"></asp:Label>
        <div id="Div2" class="resulbbvaCenter" runat="server" visible="true" style="height: 20%; overflow: auto">


            <asp:GridView ID="grvAsalariados" runat="server"
                AutoGenerateColumns="false"
                OnRowCreated="grvAsalariados_RowCreated" ShowFooter="true">
                <HeaderStyle Height="20px" Font-Size="X-Small" Font-Names="Verdana" HorizontalAlign="Left" Font-Bold="True"
                    Font-Underline="True" ForeColor="White" BackColor="#A55129" />
                <RowStyle CssClass="GridviewScrollItem" />
                <PagerStyle CssClass="GridviewScrollPager" />
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="No Percepción" />

                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 1</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtComprobante1" CssClass="txt2" Text='<%#Bind("Column1")%>' Title="Comprobante1" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 2</a> " ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtComprobante2" CssClass="txt2" Text='<%#Bind("Column2")%>' Title="Comprobante2" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 3</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtComprobante3" CssClass="txt2" Text='<%#Bind("Column3")%>' Title="Comprobante3" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 4</a> " ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtComprobante4" CssClass="txt2" Text='<%#Bind("Column4")%>' Title="Comprobante4" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 5</a> " ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtComprobante5" CssClass="txt2" Text='<%#Bind("Column5")%>' Title="Comprobante5" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 6</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                             <asp:TextBox runat="server" ID="txtComprobante6" CssClass="txt2" Text='<%#Bind("Column6")%>' Title="Comprobante5" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 7</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                             <asp:TextBox runat="server" ID="txtComprobante7" CssClass="txt2" Text='<%#Bind("Column7")%>' Title="Comprobante5" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 8</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                             <asp:TextBox runat="server" ID="txtComprobante8" CssClass="txt2" Text='<%#Bind("Column8")%>' Title="Comprobante5" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 9</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                             <asp:TextBox runat="server" ID="txtComprobante9" CssClass="txt2" Text='<%#Bind("Column9")%>' Title="Comprobante5" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 10</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                             <asp:TextBox runat="server" ID="txtComprobante10" CssClass="txt2" Text='<%#Bind("Column10")%>' Title="Comprobante5" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 11</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                             <asp:TextBox runat="server" ID="txtComprobante11" CssClass="txt2" Text='<%#Bind("Column11")%>' Title="Comprobante5" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 12</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                             <asp:TextBox runat="server" ID="txtComprobante12" CssClass="txt2" Text='<%#Bind("Column12")%>' Title="Comprobante5" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 13</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                             <asp:TextBox runat="server" ID="txtComprobante13" CssClass="txt2" Text='<%#Bind("Column13")%>' Title="Comprobante5" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    
                     <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 14</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                             <asp:TextBox runat="server" ID="txtComprobante14" CssClass="txt2" Text='<%#Bind("Column14")%>' Title="Comprobante5" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                   
                     <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 15</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                             <asp:TextBox runat="server" ID="txtComprobante15" CssClass="txt2" Text='<%#Bind("Column15")%>' Title="Comprobante5" Onkeypress="return checkDecimals(event, this.value, 7);"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="16px" ControlStyle-Width="16px">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../App_Themes/img/delete.png" CommandArgument='<%#Eval("Column4")%>' OnClick="ImageButton1_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div id="divAgregarAsalariados" runat="server" visible="false">
                <table class="resul2" width="100%">
                    <tr>
                        <%--<td style="background-color:White; width:135px;">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="padding:0px; width:30px;">Agregar Percepción:</td>--%>
                        <td>
                            <asp:ImageButton runat="server" ID="cmdAgregaAsalariados" ImageUrl="~/App_Themes/img/add.png" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <div style="width: 100%">
                                <center>
                                    <asp:Label runat="server" ID="lblIngresosAsalariados" Text="Ingresos:"></asp:Label>
                                    <asp:Label runat="server" ID="lblTotalIngresosAsalariados" Text="0.00"></asp:Label>
                                    <asp:Button runat="server" ID="btnAgregarPercepcionAsalariados" Text="Agregar Percepción" CssClass="buttonSecBBVA2" OnClick="btnAgregarPercepcionAsalariados_Click" />
                                </center>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <asp:Label ID="lblNoAsalariados" runat="server" Text="No Asalariados" class="tituloConsul"></asp:Label>
        <div id="Div3" class="resulbbvaCenter" runat="server" visible="true" style="height: 20%; overflow: auto">


            <asp:GridView ID="grvNoAsalariados" runat="server"
                AutoGenerateColumns="false"
                OnRowCreated="grvNoAsalariados_RowCreated" ShowFooter="true">
                <HeaderStyle CssClass="GridviewScrollHeader" />
                <RowStyle CssClass="GridviewScrollItem" />
                <PagerStyle CssClass="GridviewScrollPager" />
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="No Percepción" />
<%--                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 1</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">--%>
                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 1</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtComprobante1" CssClass="txt2" Text='<%#Bind("Column1")%>' Onkeypress="return checkDecimals(event, this.value, 7);" Title="ComprobanteNo1"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 2</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtComprobante2" CssClass="txt2" Text='<%#Bind("Column2")%>' Onkeypress="return checkDecimals(event, this.value, 7);" Title="ComprobanteNo2"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 3</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtComprobante3" CssClass="txt2" Text='<%#Bind("Column3")%>' Onkeypress="return checkDecimals(event, this.value, 7);" Title="ComprobanteNo3"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 4</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtComprobante4" CssClass="txt2" Text='<%#Bind("Column4")%>' Onkeypress="return checkDecimals(event, this.value, 7);" Title="ComprobanteNo4"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<a href='#'  onclick='oprimeboton();return false;'>Comprobante 5</a>" ItemStyle-Width="150px" ControlStyle-Width="150px">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtComprobante5" CssClass="txt2" Text='<%#Bind("Column5")%>' Onkeypress="return checkDecimals(event, this.value, 7);" Title="ComprobanteNo5"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cadena" ItemStyle-Width="90px" ControlStyle-Width="90px" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCadena" runat="server" CssClass="txt3" Text='<%#Bind("Column4")%>'></asp:TextBox>
                            <asp:Label ID="lblCadena" runat="server" Text='<%#Bind("Column4")%>' Visible="False"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-Width="16px" ControlStyle-Width="16px">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/App_Themes/img/delete.png" CommandArgument='<%#Eval("Column4")%>' OnClick="ImageButton1_Click1" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div id="divAgregarNoAsalariados" runat="server" visible="false">
                <table class="resul2" width="100%">
                    <tr>
                        <%--<td style="background-color:White; width:135px;">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="padding:0px; width:30px;">Agregar Percepción:</td>--%>
                        <td>
                            <asp:ImageButton runat="server" ID="cmdAgregaNoAsalariados" ImageUrl="~/App_Themes/img/add.png" Visible="false" />
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <div style="width: 100%">
                                <center>
                                    <asp:Label runat="server" ID="lblIngresosNoAsalariados" Text="Ingresos:"></asp:Label>
                                    <asp:Label runat="server" ID="lblTotalIngresosNoAsalariados" Text="0.00"></asp:Label>
                                    <asp:Button runat="server" ID="btnAgregarPercepcionNoAsalariado" Text="Agregar Percepción" CssClass="buttonSecBBVA2" OnClick="btnAgregarPercepcionNoAsalariado_Click" />
                                </center>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
       
        </div>

        <table id="tbValidarObjetos" class="resulGrid">
        
        </table>

        <div id="divHiddenButton" style="visibility: collapse">
            <asp:Button runat="server" ID="btnProcesar" Text="Procesar" OnClick="btnProcesar_Click" />
            <asp:Button runat="server" ID="btnGuardar" Text="Guardar" CssClass="buttonSecBBVA2" OnClick="btnGuardar_Click" />
        </div>
        <div style="visibility: collapse">
            <asp:Button runat="server" ID="btnCalcularIngresos" Text="Calcular Ingresos" CssClass="buttonSecBBVA2" OnClick="btnCalcularIngresos_Click" />
            <asp:Button runat="server" ID="btnValidar1" Text="Validar CFDI"  OnClick="btnValidar1_Click" />
        </div>

    </div>

    <div class="resulbbvaCenter divAdminCatPie">            
        <table width="100%" style="height: 100%;">
            <tr>
                <td>Tipificaciones *
                    <asp:DropDownList runat="server" ID="ddlTipificaciones" OnSelectedIndexChanged="ddlTipoTipificacion_SelectedIndexChanged" AutoPostBack="true" CssClass="select2BBVA">
                        <asp:ListItem Value="0" Text="<Seleccionar>" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Procesable"></asp:ListItem>
                        <asp:ListItem Value="2" Text="No Procesable: Comprobantes incompletos"></asp:ListItem>
                        <asp:ListItem Value="3" Text="No Procesable: Comprobantes ilegibles"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Rechazado: Estados de cuenta a nombre de otra persona"></asp:ListItem>
                        <asp:ListItem Value="5" Text="Rechazado: Caracteres inválidos"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Rechazado: Fecha de Certificación no coincide"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Rechazado: Monto total CFDI vs Neto en recibos de nómina (Lector de QR)"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Rechazado: No Registrado"></asp:ListItem>
                    </asp:DropDownList>
                    <%--<asp:Button runat="server" ID="btnRegresar" text="Regresar"   CssClass="buttonSecBBVA2" />--%>
                    <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" OnClick="btnRegresar_Click" />
                    <asp:Button runat="server" ID="btnLimpiar" Text="Limpiar Valores" CssClass="buttonSecBBVA2" OnClick="btnLimpiar_Click" />
                    <input type="button" runat="server" id="Button1" value="Guardar" onclick="btnguardarCliente_click();" class="buttonBBVA2" />
                    <%--<input type="button" runat="server" id="CalcularI" value="Calcular Ingresos" class="buttonBBVA2" />--%>
                    <input type="button" runat="server" id="CalcularI" value="Calcular Ingresos" onclick="btncalcular();" class="buttonBBVA2" />
                    <%--<asp:Button runat="server" ID="btnProcesar" Text="Procesar" OnClientClick="btnEnable()" OnClick="btnProcesar_Click" CssClass="buttonBBVA2" Enabled="false"  />--%>
                    <input type="button" runat="server" id="btnProcesarCliente" value="Procesar" onclick="btnProcesarCliente_click();" class="buttonBBVA2" disabled="disabled" />
					<asp:Button runat="server" ID="btnPrint" Text="Imprimir"   OnClick="btnPrint_Click" CssClass="buttonSecBBVA2"/>
                </td>
            </tr>
        </table>
    </div>

    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />
    <asp:HiddenField ID="hdnResultado1" runat="server" />


</asp:Content>

