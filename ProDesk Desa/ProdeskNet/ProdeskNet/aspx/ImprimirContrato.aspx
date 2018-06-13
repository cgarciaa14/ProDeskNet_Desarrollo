<%@ Page Language="vb" MasterPageFile="~/aspx/Home.Master" AutoEventWireup="false" CodeFile="ImprimirContrato.aspx.vb" Inherits="ImprimirContrato" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPantallas">
    <%--INC-B-2019:JDRA:Regresar--%>
    <!-- YAM-P-208 egonzalez 19/08/2015 Se agregó la tabla de archivos con id="tbValidarObjetos" donde se cargarán los archivos de la solicitud
                                        Se agregó el llamado a la función fillUpload() para la carga de documentos y se utilizó el método bind de jquery
                                        para eliminar el input file para evitar la carga de archivos.
    -->
    <%--BBVA-P-423: RQCONYFOR-07_2: AVH: 11/04/2017 modificaciones--%>
    <%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
    <%--BUG-PD-233: RHERNANDEZ: 12/10/2017: SE QUITA OPCION DE DAR DOBLE CLICK EN PANTALLA DE CONTRATOS--%>
    <%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
    <%-- BUG-PD-417: DCORNEJO: 17/04/2018: SE AGREGA LA OPCION DE TURNAR EN ADJUNTAR DOCUMENTOS PARA DESEMBOLSO--%>

    <script language="javascript" type="text/javascript">

         //Tabla pedir documentos 
        $(document).on('change', '#ctl00_ctl00_cphCuerpo_cphPantallas_ddlTurnar', function () {
            var Procesable = $("#ctl00_ctl00_cphCuerpo_cphPantallas_ddlTurnar option:selected").val();


            if (Procesable == -3) {
                $("#Tabla_Documentos").show();
            }

            else {
                $("#Tabla_Documentos").hide();

            }
        });

        function pageLoad() {
            fillgv('gvformato', 'hdnIdFolio');
            fillUpload('tbValidarObjetos', 'hdnIdPantalla, hdnIdFolio, hdnUsua', 'per1', '');
            var idpantalla = $("[id$=hdPantalla] ").val()
        }

        function fnImprimeContrato() {
            var count = 0;
            cheks = $('[id*=ctl00_ctl00_cphCuerpo_cphPantallas_chek]')
            $.each(cheks, function () {
                if ($(this)[0].checked == false) {
                    count++;
                }
            })
            if (cheks.length == count) {
                alert('Se debe seleccionar por lo menos un contrato');
                return;
            }
            var local = window.location.hostname;
            window.open("../uploads/01Contrato.rtf", "download");
        }

        function fnCmbImprimirCto() {
            var cadena = ''
            var check = $('[id*=chk]')
            var contrato = $('[id$=hdnIdFolio]').val();
            var cliente = $('[id$=botClien]').val();
            var moneda = $('[id$=botMod]').val();
            var empresa = $('[id$=botEmp]').val();
            var operacion = $('[id$=botOpera]').val();
            var pjuridica = $('[id$=botJuri]').val();

            $.each(check, function (key) {

                if ($(this)[0].checked == true) {
                    cadena += $(this)[0].id + ','
                    cadena = cadena.replace('chk', '');
                }


            })

            cadena = cadena.substring(0, cadena.length - 1);
            var coucadena = cadena.length
            if (coucadena == 0) { alert('Debes selecionar una opción'); return; }

            btnImprimirCto(cadena, contrato, cliente, empresa, moneda, operacion, pjuridica)


        }

        function mostrarDiv() {
            div = document.getElementById('divautoriza');
            div.style.display = '';

        }


        $(document).ready(function () {
            fillgv('gvformato', 'hdnIdFolio');
            fillUpload('tbValidarObjetos', 'hdnIdPantalla, hdnIdFolio, hdnUsua', 'per1', '');

        });

        $(window).bind("load", function () {
            $('#tbValidarObjetos').find('input[type="file"]').remove();
            setTimeout(function () {
                $('#tbValidarObjetos').find('input[type="file"]').remove();
            }, 500);
        });

        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();
        }

        function btnGuardar(id) {
            var boton = id.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            var f = $('[id$=hdnIdFolio]').val();
            var u = $('[id$=hdnUsua]').val();
            var cadena = '';
            var cadenaUp = '';
            var pantalla = $('[id$=hdNomPantalla]').val();


            if (boton == "btnGuardarAutoriza") {
                var txtUsu = $('[id$=txtUsuario]').val();
                var txtpsswor = $('[id$=txtPassw]').val();
                var idpantalla = $("[id$=hdnIdPantalla] ").val();
                var txtmotivoOb = $("[id$=txtmotivo]").val();
                //           var cadenastored = 'EXEC sp_ValidacionUsuario ' + '"' + cadenatodo + '",' + idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1'
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 46, 1, txtmotivoOb)

            } else if (boton == "btnGuardarCancelar") {
                var txtUsu = $('[id$=txtusua]').val();
                var txtpsswor = $('[id$=txtpass]').val();
                var idpantalla = $("[id$=hdnIdPantalla] ").val();
                var txtmotivoOb = $("[id$=TxtmotivoCan]").val();
                $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)

            }
        }
        function AvanzarTarea() {
            var botonp = document.getElementById('<%= btnproc.ClientID%>');
             botonp.click();
         }
        $(document).ready(function () { $.urlParam = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } }; if ($.urlParam("Enable").toString() == "1") { $("#btnCancelarNew").hide(); }; });
    </script>
    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnCancelar {
            display: none;
        }
    </style>
    <div class="divAdminCat">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width: 70%;">Impresion de contrato y anexo</td>
                            </tr>
                        </table>

                    </td>

                </tr>
            </table>
        </div>
        <div class="divAdminCatCuerpo">
            <div>
                <table class="cssEncabezado">
                    <tr>
                        <th style="width: 15%;" class="campos">NUMERO SOLICITUD:</th>
                        <th style="width: 20%;">
                            <asp:Label ID="lblSoli" runat="server"></asp:Label>
                        </th>
                        <th style="width: 15%;" class="campos">NOMBRE:</th>
                        <th style="width: 20%;">
                            <asp:Label ID="lblNombre" runat="server"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <th class="campos">STATUS DOCUMENTO:</th>
                        <th>
                            <asp:Label ID="lblstatusDoc" runat="server">
                            </asp:Label>
                        </th>
                        <th class="campos">STATUS CREDITO:
                        </th>
                        <th>
                            <asp:Label ID="ldlstatusCre" runat="server"></asp:Label>
                        </th>
                    </tr>

                </table>
                <div id="gvformato" style="width: 100%; border-style: solid; border-color: #808080; border-width: 1px; height: 50%;">
                </div>
            </div>
            <table id="tbValidarObjetos" class="resulGrid"></table>
        </div>
        <div id="divautoriza" style="display: none">
            <%--<cc1:ModalPopupExtender ID="mpoAutorizar" runat="server" PopupControlID="popAutoriza" TargetControlID="btnAutorizar" CancelControlID="btnCancelAutoriza" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>--%>
            <asp:Panel ID="popAutoriza" runat="server" Text="Autorización" CssClass="cajadialogo">
                <div class="tituloConsul">
                    <asp:Label ID="Label4" runat="server" Text="Autorización" />
                </div>
                <table width="100%">
                    <tr>
                        <td class="campos">Usuario:</td>
                        <td>
                            <asp:TextBox ID="txtUsuario" SkinID="txtGeneral" MaxLength="12" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="campos">Password:</td>
                        <td>
                            <asp:TextBox ID="txtPassw" runat="server" SkinID="txtGeneral" MaxLength="12" TextMode="Password" EnableTheming="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="campos">Descripción:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea id="txtmotivo" runat="server" onkeypress="ManejaCar('A',1,this.value,this)" class="Text" rows="5" cols="1" style="width: 100%"></textarea>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td>
                            <asp:HiddenField runat="server" ID="hdnidAutoriza" />
                        </td>
                        <td align="left" valign="middle">
                            <input id="btnGuardarAutoriza" type="button" runat="server" value="Guardar" class="buttonBBVA" onclick="btnGuardar(id)" />
                            <asp:Button ID="btnCancelAutoriza" runat="server" class="buttonSecBBVA2" Text="Cancelar" SkinID="btnGeneral" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top: 15%; left: 31%; width: 220px;">
            <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID="btnCancelar" PopupControlID="popCancela" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
            <asp:Panel ID="popCancela" runat="server" CssClass="cajadialogo ">--%>
                <div class="tituloConsul">
                    <asp:Label ID="Label1" runat="server" Text="Cancelación" />
                </div>
            <center>
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
                            <asp:TextBox ID="txtpass" runat="server" SkinID="txtGeneral" MaxLength="12" TextMode="Password" EnableTheming="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="campos">Descripción:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea id="TxtmotivoCan" runat="server" onkeypress="ManejaCar('A',1,this.value,this)" class="Text" rows="5" cols="1" style="width: 100%"></textarea>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td>
                            <asp:HiddenField runat="server" ID="HiddenField1" />
                        </td>
                        <td align="left" valign="middle">
                            <input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" class="button" onclick="btnGuardar(id)" />
                            <br />
                            <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral" />
                        </td>
                    </tr>

                </table>
            </center>

            <%--</asp:Panel>--%>
        </div>
        <div class="resulbbvaCenter divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">

                         <label id="lblturnar" runat="server" visible="false">Turnar: </label>
                        &nbsp;
                        <asp:DropDownList ID="ddlTurnar" runat="server" CssClass="selectBBVA" Visible="false" OnSelectedIndexChanged="ddlTurnar_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" />
                        <%--<input type="button" class="Text" value="Regresar" onclick="fnRegresar();"  />--%>
                        <input id="cmbguardar" runat="server" type="button" value="Procesar" onclick="$(this).prop('disabled', true); AvanzarTarea();" class="buttonBBVA" />
                        <%--<asp:Button runat="server" ID="btnGuardar" text="Procesar" SkinID="btnGeneral" />--%>
                        <asp:Button runat="server" ID="btnAutorizar" Visible="false" Text="Autorizar" />
                        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClientClick="mostrarCanceDiv()" CssClass="buttonSecBBVA2" />
                        <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />

                    </td>
                </tr>
            </table>
        </div>
         <div id="divbtncollap" style="visibility: collapse">
            <asp:Button runat="server" ID="btnproc" OnClick="btnproc_Click"/>
        </div>




    </div>
    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdnIdFolio" runat="server" />
    <asp:HiddenField ID="hdnIdPantalla" runat="server" />
    <asp:HiddenField ID="hdnUsua" runat="server" />
    <asp:HiddenField ID="hdnResultado" runat="server" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
    <asp:HiddenField ID="hdNomPantalla" runat="server" />
    <asp:HiddenField ID="hdnEnable" runat="server" />
    <asp:HiddenField ID="hdhContrato" runat="server" />
    <asp:HiddenField ID="hdnResultado1" runat="server" />
    <asp:HiddenField ID="hdnResultado2" runat="server" />



</asp:Content>
