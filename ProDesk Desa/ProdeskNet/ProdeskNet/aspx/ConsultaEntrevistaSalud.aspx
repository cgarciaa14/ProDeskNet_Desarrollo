<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="ConsultaEntrevistaSalud.aspx.vb" Inherits="aspx_ConsultaEntrevistaSalud" %>

<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<%--BBVA-P-423:RQ03 AVH: 15/12/2016 Se crea ventana para cuestionario de salud--%>
<%--BUG-PD-04: 12/01/2017 Gvargas: Se quitan degugger;--%>
<%--BUG-PD-07: AVH: 27/01/2017 SE AGREGA AUTOPOSTBACK EN LOS CAMPOS PESO Y ESTATURA--%>
<%--BUG-PD-09: AVH: 14/02/2017 SE MODIFICA TAMAÑO DE LA PANTALLA--%>
<%--BUG-PD-32: AVH: 18/04/2017 Se agregan WS con la secuencia 1 getQuestionnaire, 2 createQuestionnaire, 3 validateQuestionnaire--%>
<%--BUG-PD-35: AVH: 25/04/2017 Modificaciones pantalla--%>
<%--BUG-PD-61 JBEJAR 25/5/2017 Validaciones en los campos que son requeridos--%>
<%--BUG-PD-71 JBEJAR 05/06/2017 cambio de etiqueta cm a m--%>
<%--BUG-PD-80 JBEJAR 09/06/2017 Se limita a un entero y dos decimales el campo estatura.--%>
<%--BUG-PD-90 JBEJAR 12/06/2017 Se valida estatura maxima de 3 metros --%>
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BUG-PD-161: ERODRIGUEZ:18/07/2017:JBEJAR:SE BLOQUEA BOTON PROCESAR AL ENCONTRAR LA TAREA --%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">
   
    <script type="text/javascript">

        function btnProcesarCliente_click() {
            //document.getElementById('btnProcesarCliente').disabled = false;
            //alert("ENTRO")
            var hasError = false;
            var textResult = 'Información de la solicitud:';
            var valorB = $(".customFakeClass")
            var valorE = document.getElementById('<%=txtEstatura.ClientID%>').value
            if (valorE > 3.00) { // BUG-PD-90 Se valida el campo estatura mayor a 3.00 es incorrecto. 
                textResult += "\n + El campo estatura(m)* no puede ser mayor a los 3.00 metros";
                hasError = true;
            } 
                $.each(valorB, function () {
                    if ($(this).attr('extraProperty') != "") {
                        if ($(this).val() == "") {

                            textResult += "\n + Falta información en el campo " + $(this).attr('extraProperty');

                            hasError = true;
                        }
                    }
                }
                    );
            

            if (hasError == true) {
                alert(textResult);
            } else {
                var radioButtons = $('input[type="radio"]');
                var counter = 0;
                var counterSelected = 0;
                $.each(radioButtons, function () {
                    if ($(this).is(":visible")) {
                        counter++;
                        if ($(this).is(":checked")) {
                            counterSelected++;
                        }
                    }
                });
                // alert("counter: " + counter + "counterSelected: " + counterSelected);
                if ((counter / 2) != counterSelected ) {
                    alert('Se deben de responder todas las preguntas');
                } else {
                    //alert('OK'); 
                    var buttonProcesar = document.getElementById('<%= btnProcesar.ClientID%>');
                    $('#<%=btnProcesarCliente.ClientID%>').prop('disabled', true);              
                    buttonProcesar.click();
                }
            }
        }

        function ColumnaOculta() { display: none; }

        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();
        }

        function btnGuardar(id) {
            //debugger;
            var boton = id.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            var f = $('[id$=hdSolicitud]').val();
            var u = $('[id$=hdusuario]').val();
            var cadena = "UPDATE PDK_CAT_TAREAS SET PDK_TAR_MODIF=GETDATE() WHERE PDK_ID_TAREAS=75";
            var cadenaUp = '';
            var pantalla = $('[id$=hdNomPantalla]').val();


            if (boton == "btnGuardarAutoriza") {
                var txtUsu = $('[id$=txtUsuario]').val()
                var txtpsswor = $('[id$=txtPassw]').val()
                var idpantalla = $("[id$=hdPantalla] ").val()
                var txtmotivoOb = $("[id$=txtmotivo]").val()
                //           var cadenastored = 'EXEC sp_ValidacionUsuario ' + '"' + cadenatodo + '",' + idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1'

                $('#ventanaContain').hide();
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del passwird esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 46, 1, txtmotivoOb)

            } else if (boton == "btnGuardarCancelar") {
                //debugger;
                var txtUsu = $('[id$=txtusua]').val();
                var txtpsswor = $('[id$=txtpass]').val();
                var idpantalla = $("[id$=hdPantalla] ").val()
                var txtmotivoOb = $("[id$=TxtmotivoCan]").val()
                $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())

                $('#ventanaContain').hide();
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del passwird esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)

            }
        }

        $(document).ready(function () { $.urlParam = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } }; if ($.urlParam("Enable").toString() == "1") { $("#btnCancelarNew").hide(); }; });

    </script>
    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnCancelar {
            display: none;
        }
    </style>

    <div class="divPantConsul">

        <script type="text/javascript" src="../js/Funciones.js"></script>
        <script type="text/javascript" src="../js/ProdeskNet.js"></script>
        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <legend>
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Consulta Entrevista de Salud"></asp:Label></legend>
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
                    <asp:Label ID="lblSolicitud2" Font-Underline="true" runat="server">
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
        <br />

        <div class="fieldsetBBVA" runat="server">


            <table>
                <tr style="visibility: collapse">
                    <td>
                        <asp:Label runat="server" ID="lblSol" Text="Solicitud:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblSolicitud"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNombre" Text="Nombre de la persona a asegurar:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNombre" Text="" Width="100%" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblRFC" Text="RFC:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtRFC" Text="" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblOcupacion" Text="Ocupación:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtOcupacion" Text="" Width="98%" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblSexo" Text="Género:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSexo" Text="" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" Text="*"></asp:Label><asp:Label runat="server" ID="lblPeso" Text="Peso (kg):"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPeso" Onkeypress="return checkDecimals(event, this.value, 2)" CssClass="txt3BBVA customFakeClass" AutoPostBack="true" extraProperty="Peso (kg):*"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="*"></asp:Label><asp:Label runat="server" ID="lblEstatura" Text="Estatura (m):"></asp:Label>
                    </td>
                    <td><%--BUG-PD-80--%>
                        <asp:TextBox runat="server" ID="txtEstatura" Onkeypress="return checkDecimals(event, this.value, 0)" CssClass="txt3BBVA customFakeClass" AutoPostBack="true" extraProperty="Estatura (m):*"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFeNac" Text="Fecha de Nacimiento:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFeNac" Text="" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblEstadoCivil" Text="Estado Civil:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEstadoCivil" Text="" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblRegimen" Text="Régimen (Casados):"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtRegimen" Text="" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblMonto" Text="Monto del Crédito:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMonto" Text="" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblPlazo" Text="Plazo del Crédito:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPlazo" Text="" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblDomicilio" Text="Domicilio:"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtDomicilio" Text="" Width="98%" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblTel" Text="Teléfono:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTel" Text="" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblColonia" Text="Colonia:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtColonia" Text="" Width="98%" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblCiudad" Text="Ciudad:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCiudad" Text="" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblEstado" Text="Estado:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEstado" Text="" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblCP" Text="Código Postal:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCP" Text="" Enabled="false" CssClass="txt3BBVA"></asp:TextBox>
                    </td>
                </tr>
            </table>



        </div>
        <br />
        <div id="Div1" runat="server" style="height: 30%; overflow: auto">
            <asp:GridView ID="gvCuestionario" runat="server" AutoGenerateColumns="false"
                HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowPaging="false"
                Width="80%" PagerStyle-HorizontalAlign="Right"
                EmptyDataText="No se encontró información con los parámetros proporcionados.">
                <Columns>
                    <asp:BoundField DataField="PDK_ID_PREGUNTA" HeaderText="ID" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="PDK_PREGUNTA" HeaderText="Pregunta" ItemStyle-HorizontalAlign="Left">
                        <ItemStyle Width="120px" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Si">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:RadioButton runat="server" GroupName="venc" ID="rbSI" name="si" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="No">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:RadioButton runat="server" GroupName="venc" ID="rbNO" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor" HeaderStyle-CssClass="ColumnaOculta">
                        <ItemTemplate>
                            <itemstyle horizontalalign="Center" />
                            <asp:TextBox runat="server" ID="txtValor" Width="110px" Onkeypress="return checkDecimals(event, this.value, 2)"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Res" HeaderText="Respuestas" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta" />

                </Columns>
            </asp:GridView>
        </div>
        <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top:15%; left:31%; width: 220px;">
            <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID="btnCancelar" PopupControlID="popCancela" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>--%>
            <%--<asp:Panel ID="popCancela" runat="server" CssClass="cajadialogo ">
            </asp:Panel>--%>
                <div class="tituloConsul">
                    <asp:Label ID="Label1" runat="server" Text="Cancelación" />
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
                            <asp:TextBox ID="txtpass" runat="server" MaxLength="12" TextMode="Password" EnableTheming="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="campos">Descripción:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea id="TxtmotivoCan" runat="server" class="Text" rows="5" cols="1" style="width: 100%"></textarea>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td>
                            <asp:HiddenField runat="server" ID="HiddenField1" />
                        </td>
                        <td align="left" valign="middle">
                            <%--<input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" onclick="btnGuardar(id)"  />--%>
                            <asp:Button runat="server" ID="btnGuardarCancelar" Text="Guardar" OnClientClick="btnGuardar(id)" />
                            <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral" />
                        </td>
                    </tr>

                </table>
        </div>
        <div class="resulbbvaCenter divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" />
                        <%--<input id="cmbguardar1"   runat="server"  type ="button" value="Procesar" Class="buttonBBVA2" onclick="" /> --%>
                        <%--<asp:Button runat="server" ID="btnProcesar" Text="Procesar" CssClass="buttonBBVA2" OnClick="btnProcesar_Click" />--%>
                        <%--<asp:Button runat="server" ID="btnGuardar" text="Procesar" SkinID="btnGeneral" />--%>
                        <asp:Button runat="server" ID="btnAutorizar" Text="Autorizar" Visible="false" CssClass="buttonSecBBVA2" />
                        <input type="button" runat="server" id="btnProcesarCliente" value="Procesar" onclick="btnProcesarCliente_click();" class="buttonBBVA2" />
                        <asp:Button runat="server" ID="btnCancelar" OnClientClick="mostrarCanceDiv()" Text="Cancelar" CssClass="buttonSecBBVA2" />
                        <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                        <asp:Button runat="server" ID="btnImprimir" Text="Imprimir" OnClick="btnImprimir_Click" CssClass="buttonSecBBVA2" />
                        <asp:Button runat="server" ID="btnadelanta" Text="Adelantar" Visible="false" OnClick="btnadelanta_Click" />



                    </td>
                </tr>
            </table>
        </div>

        <div id="divHiddenButton" style="visibility: collapse">
            <asp:Button runat="server" ID="btnProcesar" Text="Procesar" OnClick="btnProcesar_Click" />
        </div>
    </div>

    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />
    <asp:HiddenField runat="server" ID="hdnResultado" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
    <asp:HiddenField ID="hdNomPantalla" runat="server" />
    <asp:HiddenField ID="hntipoPantalla" runat="server" />
    <asp:HiddenField ID="hdnResultado1" runat="server" />
    <asp:HiddenField ID="hdnResultado2" runat="server" />
</asp:Content>

