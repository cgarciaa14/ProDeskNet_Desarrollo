<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="altaSEVA.aspx.vb" Inherits="aspx_altaSEVA" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<%--BBV-P-423 RQADM-30 MAPH 17/04/2017 Pantalla Referenciados SEVA 60--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="Server">

    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript" language="javascript">
        function pageLoad() {
            $('#<%= ddlPersonalidadJuridica.ClientID%>').change(function () {
                if ($(this).val() == "3") {
                    $('#<%= txtSegundoNombre.ClientID %>').prop('disabled', true);
                    $('#<%= txtApPaterno.ClientID %>').prop('disabled', true);
                    $('#<%= txtApMaterno%>').prop('disabled', true);
                    $('#<%= txtSegundoNombre.ClientID %>').val('');
                    $('#<%= txtApPaterno.ClientID %>').val('');
                    $('#<%= txtApMaterno.ClientID %>').val('');
                    $('#<%= txtApPaterno.ClientID %>').attr('extraProperty', '');
                    $('#<%= txtApMaterno.ClientID %>').attr('extraProperty', '');
                    $('#<%= txtNombre.ClientID %>').prop('onkeypress', 'return ValCarac(event,6);');
                } else {
                    $('#<%= txtSegundoNombre.ClientID %>').prop('disabled', false);
                    $('#<%= txtApPaterno.ClientID %>').prop('disabled', false);
                    $('#<%= txtApMaterno.ClientID %>').prop('disabled', false);
                    $('#<%= txtApPaterno.ClientID %>').attr('extraProperty', 'Apellido Paterno');
                    $('#<%= txtApMaterno.ClientID %>').attr('extraProperty', 'Apellido Materno');
                    $('#<%= txtNombre.ClientID %>').prop('onkeypress', 'return ValCarac(event,5);');
                }
            });
            $('#<%= txtFolioSEVA.ClientID%>').prop('title', $('#<%= txtFolioSEVA.ClientID%>').val());
        }

        function btnAltaSEVA_click() {
            var hasErrorSol = false;
            var hasErrorEje = false;
            var textResult = 'Información de la solicitud:';
            var textErrSol = ['\n\nSección Datos del Solicitante:', '', '', '', '', ''];
            var textErrEje = ['\n\nSección Datos del Ejecutivo:', '', '', '', ''];

            txtInputs = $(".txt3BBVA");
            ddlInputs = $(".select2BBVA");

            $.each(txtInputs, function () {
                if ($(this).val() == "" && $(this).attr('extraProperty') != '') {
                    if ($(this).hasClass('solicitanteClass')) {
                        hasErrorSol = true;
                        textErrSol[$(this).attr('arrayPosition')] = "\n + Falta información en el campo " + $(this).attr('extraProperty').toString();
                    }
                    else if ($(this).hasClass('ejecutivoClass')) {
                        hasErrorEje = true;
                        textErrEje[$(this).attr('arrayPosition')] = "\n + Falta información en el campo " + $(this).attr('extraProperty').toString();
                    }
                }
                if ($(this)[0].id == 'txtRFC') {
                    textErrSol[$(this).attr('arrayPosition')] = ValidaRfc($(this).val());
                }
            });

            $.each(ddlInputs, function () {
                if ($(this).val() == 0) {
                    if ($(this).hasClass('solicitanteClass')) {
                        hasErrorSol = true;
                        textErrSol[$(this).attr('arrayPosition')] = "\n + Debe seleccionar un elemento válido en " + $(this).attr('extraProperty').toString();
                    }
                    //else if ($(this).hasClass('ejecutivoClass')) {
                    //    hasErrorEje = true;
                    //    textErrEje[$(this).attr('arrayPosition')] = "\n + Debe seleccionar un elemento válido en " + $(this).attr('extraProperty').toString();
                    //}
                }
            });

            if (hasErrorSol) {
                var textSol = textErrSol.join()
                textResult += textSol;
            }
            if (hasErrorEje) {
                var textEje = textErrEje.join()
                textResult += textEje;
            }
            if (!hasErrorSol && !hasErrorEje) {
                var aspButtonAltaSEVA = $("#<%=btnAspAltaSEVA.ClientID%>");
                aspButtonAltaSEVA.click();
            }
            else {
                alert(textResult);
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

        function btnRegresar_click() {
            window.location = "../aspx/consultaPanelControl.aspx"
        }

        function validarEmail() {
            var email = $("input[id$=txtmail]").val();
            if (!email == '') {
                expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!expr.test(email))
                    alert("El formato de la dirección de correo no es valido.");
            }
        }

    </script>

    <div class="divAdminCat ">
        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width: 70%;">
                                    <legend>Alta SEVA
                                    </legend>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divAdminCatCuerpo ">
            <div style="width: 100%; height: 100%; overflow: auto;">
                <table width="98%" id="tbFacturacion">
                    <tr>
                        <td colspan="4" style="font-size: larger">Datos del Solicitante.
                        </td>
                    </tr>
                    <tr>
                        <td>Personalidad Jurídica *
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlPersonalidadJuridica" Class="select2BBVA solicitanteClass" Width="160px" extraProperty="Personalidad Jurídica" arrayPosition="1">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <label id="lblNombre">Razón Social/Primer Nombre *</label>
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtNombre" class="txt3BBVA solicitanteClass" onkeypress="return ValCarac(event,5);" extraProperty="Razón Social/Primer Nombre" title="Nombre tal y como aparece en la identificación oficial" arrayposition="2" />
                        </td>
                    </tr>
                    <tr>
                        <td>Segundo Nombre 
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtSegundoNombre" class="txt3BBVA solicitanteClass" onkeypress="return ValCarac(event,5);" extraProperty="" />
                        </td>
                        <td>Apellido Paterno *
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtApPaterno" onkeypress="return ValCarac(event,6);" class="txt3BBVA solicitanteClass" extraProperty="Apellido Paterno" arrayposition="3" />
                        </td>
                    </tr>
                    <tr>
                        <td>Apellido Materno *
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtApMaterno" onkeypress="return ValCarac(event,6);" class="txt3BBVA solicitanteClass" extraProperty="Apellido Materno" arrayposition="4" />
                        </td>
                        <td>RFC *
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtRFC" name="txtRFC" onkeypress="return ValCarac(event,12)" class="txt3BBVA solicitanteClass" maxlength="13" extraProperty="RFC" arrayposition="5" />
                        </td>
                    </tr>
                    <tr>
                        <td>Teléfono Casa/Oficina
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtTelParticular" onkeypress="return ValCarac(event,7);" class="txt3BBVA solicitanteClass" extraProperty="" />
                        </td>
                        <td>Teléfono Movil
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtTelMovil" onkeypress="return ValCarac(event,7);" class="txt3BBVA solicitanteClass" extraProperty="" />
                        </td>
                    </tr>
                    <tr>
                        <td>E-mail
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtEmail" onkeypress="return ValCarac(event,23);" class="txt3BBVA solicitanteClass" extraProperty="" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="font-size: larger">Datos del Ejecutivo.
                        </td>
                    </tr>
                    <tr>
                        <td>Nombre Completo *
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtNombreCompletoEjec" onkeypress="return ValCarac(event,6);" class="txt3BBVA ejecutivoClass" extraProperty="Nombre Completo" arrayposition="1" title="Nombre completo del Ejecutivo" />
                        </td>
                        <td>Usuario Corporativo *
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtUsuarioCorpoEjec" onkeypress="return ValCarac(event,7);" class="txt3BBVA ejecutivoClass" extraProperty="Usuario Corporativo" arrayposition="2" />
                        </td>
                    </tr>
                    <tr>
                        <td>CR Sucursal *
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtSucursalEjec" onkeypress="return ValCarac(event,7);" class="txt3BBVA ejecutivoClass" extraProperty="CR Sucursal" arrayposition="3" />
                        </td>
                        <td>E-mail *
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtEmailEjec" onkeypress="return ValCarac(event,23);" class="txt3BBVA ejecutivoClass" extraProperty="E-mail" arrayposition="4" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="4" style="text-align: right;"></td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="Label1">Folio SEVA</label>
                        </td>
                        <td>
                            <input type="text" runat="server" id="txtFolioSEVA" class="txt3BBVA solicitanteClass" disabled="disabled" extraProperty=""/>
                        </td>
                    </tr>

                </table>
            </div>
        </div>
        <div class="divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">
                        <input type="button" id="btnRegresar" value="Regresar" onclick="btnRegresar_click();" class="buttonSecBBVA2" />
                        <input type="button" id="btnAltaSEVA" value="Alta SEVA" onclick="btnAltaSEVA_click();" class="buttonBBVA2" />
                    </td>
                </tr>
            </table>
        </div>

        <div style="visibility: collapse">
            <asp:Button ID="btnAspAltaSEVA" runat="server" OnClick="btnAspAltaSEVA_Click" />
        </div>

    </div>

</asp:Content>
