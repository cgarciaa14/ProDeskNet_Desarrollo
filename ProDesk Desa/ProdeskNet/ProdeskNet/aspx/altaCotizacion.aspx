<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="altaCotizacion.aspx.vb" Inherits="altaCotizacion" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>
<script runat="server">

    Protected Sub btnproc_Click(sender As Object, e As EventArgs)

    End Sub
</script>


<%--Tracker:INC-B-2019:JDRA:Regresar--%>
<%--BBV-P-423: RQADM-40: AMR: 28/03/2017 Ligue de Cotización con Solicitud--%>
<%--BBV-P-423: BUG-PD-52: ERODRIGUEZ: 17/05/2017 Modificaciòn para nos mostrar mensajes y deshabilitar botones validar y procesar--%>
<%--BUG-PD-57 GVARGAS 18/05/2017 Cambios faltas ortográficas --%>
<%--BUG-PD-75 ERODRIGUEZ 06/06/2017 se cambio formato para mostrar separacion de cantidades con coma --%>
<%--BUG-PD-107 ERODRIGUEZ 20/06/2017 Se realizaron validaciones para validar cuestionario en caso de tener uno guardado--%>
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BUG-PD-140: erodriguez: 05/07/2017:Se modifico regla si es menor la capacidad de la solicitud asi se deja si es mayor se toma la cantidad de la cotizacion --%>
<%--BUG-PD-224: RHERNANDEZ: 03/10/17: Se agrega redirect de tareas automaticas al ejecutar val negocio--%>
<%--BUG-PD-233: RHERNANDEZ: 12/10/2017: SE QUITA DEBUGGER DE FUNCION--%>  
<%--BUG-PD-235: RHERNANDEZ: 16/10/17: Se bloquea el boton al procesar la tarea, solo en caso de error lo habilita--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
<asp:Content ID="content1" ContentPlaceHolderID="cphPantallas" runat="server">
    <script language="javascript" type="text/javascript">

        function cambiaVisibilidadDiv(id, visible) {
            div = document.getElementById(id);
            if (visible) {
                div.style.display = '';
            }
            else {
                div.style.display = 'none'
            }
        }

        function btnGuardar(action) {
            var f = $('[id$=hdnIdFolio]').val();
            var u = $('[id$=hdnUsua]').val();
            var cadena = 'UPDATE PDK_OPE_SOLICITUD SET PDK_OPE_MODIF = GETDATE() WHERE PDK_ID_SOLICITUD = ' + f;
            var cadenaUp = '';
            var pantalla = $('[id$=hdNomPantalla]').val();


            if (action == "Autoriza") {
                cambiaVisibilidadDiv('divautoriza', false);
                var txtUsu = $('[id$=txtUsuario]').val();
                var txtpsswor = $('[id$=txtPassw]').val();
                var idpantalla = $("[id$=hdnIdPantalla] ").val();
                var txtmotivoOb = $("[id$=txtmotivo]").val();
                var hasError = false;
                var textResult = 'Información de la solicitud:\n'
                $('#ventanaContain').hide();

                if (txtUsu == '') {
                    hasError = true;
                    textResult += '\nEl campo usuario esta vacía';
                    return;
                }
                if (txtpsswor == '') {
                    hasError = true;
                    textResult += '\nEl campo del password esta vacía';
                    return;
                }
                if (txtmotivoOb == '') {
                    hasError = true;
                    textResult += '\nEl campo de motivo esta vacía';
                    return;
                }

                if (hasError) {
                    alert(textResult);
                    return;
                }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 46, 1, txtmotivoOb)

            } else if (action == "Cancelar") {
                cambiaVisibilidadDiv('divcancela', false);
                var txtUsu = $('[id$=txtusua]').val();
                var txtpsswor = $('[id$=txtpass]').val();
                var idpantalla = $("[id$=hdnIdPantalla] ").val();
                var txtmotivoOb = $("[id$=TxtmotivoCan]").val();
                $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())
                $('#ventanaContain').hide();
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)
            }
        }

        var respuesta;
        var descripcion;

        $(document).ready(function () {
            if ($('[id$=hdnCotizacion]').val() != "") {
                fnValidaenBD('exec spValidaCot ' + $('[id$=hdnCotizacion]').val() + ', ' + $('[id$=lblSolicitud]').text(), 1);
            }

            /*  valida que pantalla es, si es la pantalla 62 esconde el div de valida cotizacion    */

            if ($('[id$=hdnIdPantalla]').val() == 62) {
                $("#tbValidaCotiza").hide();
            }
            else {
                $("input[name = 'lblAceptaCotizacion']").prop('disabled', true);
            }

            /*  llena el texbox de comentarios anteriores*/

            fnLlenaComentariosAnteriores();

            $.fn.myFunction = function () {
                btnConsultaAObjeto("declare @mensaje varchar(5000) = ''; select @mensaje = @mensaje + convert(varchar(16), PDK_FEC_CAMBIO_OBS_COT, 120) + ' : ' + PDK_USU_NOMBRE + ' : ' + pdk_obsv_cot + CHAR(13)+CHAR(10) from PDK_OBSERVACIONES_COTIZACION a inner join PDK_USUARIO b on a.PDK_USU_CAMBIO_OBS_COT = b.PDK_ID_USUARIO where PDK_ID_SECCCERO =" + $('[id$=lblSolicitud]').text() + "; select @mensaje as mensaje", $('#ComentariosAnteriores'));
            }

            /*  Boton de Guarda observaciones   */

            $("#btnGuardaObservacionesCot").click(function () {
                var selected_val = $('form input[name=lblAceptaCotizacion]:checked').val();
                var strinserta = "insert PDK_OBSERVACIONES_COTIZACION(PDK_ID_SECCCERO, PDK_BIT_ACEPTA, PDK_OBSV_COT, PDK_USU_CAMBIO_OBS_COT)values (" + $('[id$=lblSolicitud]').text() + ", " + selected_val + ", '" + $('#txtAreaObservacionesCot').val() + "', " + $('[id$=hdnUsua]').val() + ")";
                if ($('#txtAreaObservacionesCot').val().length == 0) {
                    alert('Necesita introducir un comentario');
                }
                else {
                    btnInsUpd(strinserta, 'fnLlenaComentariosAnteriores()');
                }

            })

        });

        function fnLlenaComentariosAnteriores() {
            btnConsultaAObjeto("declare @mensaje varchar(5000) = ''; select @mensaje = @mensaje + convert(varchar(16), PDK_FEC_CAMBIO_OBS_COT, 120) + ' : ' + PDK_USU_NOMBRE + ' : ' + pdk_obsv_cot + CHAR(13)+CHAR(10) from PDK_OBSERVACIONES_COTIZACION a inner join PDK_USUARIO b on a.PDK_USU_CAMBIO_OBS_COT = b.PDK_ID_USUARIO where PDK_ID_SECCCERO =" + $('[id$=lblSolicitud]').text() + "; select @mensaje as mensaje", $('#ComentariosAnteriores'))
            $('#txtAreaObservacionesCot').val("");
        }

        function fnValidaCot() {
            var Cot = $('[id$=txtCotizacion]').val();
            var Sol = $('[id$=lblSolicitud]').text();

            if (Cot == '') { alert('Debes tener una cotización'); return; }
            fnValidaenBD("exec spValidaCot " + Cot + ", " + Sol);
        }

     

        function mostrarDiv() {
            div = document.getElementById('divautoriza');
            div.style.display = '';

        }


        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();
        }

        function closeCanceDiv() {
            $('#ventanaContain').hide();
            $("#divcancela").hide();
        }


        /*  funcion que sirve para realizar consulta a la base de datos y te regresa un objeto de respuesta    */

        function fnValidaenBD(cadena, primeraVez) {
            if (primeraVez == undefined) { primeraVez = 0; }
            cadena = cadena.replace(/'/g, "\\'");
            data = "{cadena: '" + cadena + "' }";
            var obj;
            var options = {};
            var request = $.ajax({
                type: "POST",
                url: "../fillobjetos.asmx/fnValida",
                data: data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    obj = $.parseJSON(msg.d)
                    $.each(obj.REGISTRO, function (key) {
                        respuesta = obj.REGISTRO[key].resp
                        descripcion = obj.REGISTRO[key].desc
                        capSol = obj.REGISTRO[key].CapacidadSol
                        PlzSol = obj.REGISTRO[key].PlazoSol.toFixed(2)
                        capCot = obj.REGISTRO[key].CapacidadCot.toFixed(2)
                        PlzCot = obj.REGISTRO[key].PlazoCot
                        AgCot = obj.REGISTRO[key].agenciaCot
                        AgSol = obj.REGISTRO[key].agenciaSol
                        //if (obj.REGISTRO[key].porcentajeMinimo != undefined) {
                        //    porcentajeMinimo = 'Datos Cotizacion.' + obj.REGISTRO[key].porcentajeMinimo.toFixed(2)
                        //} else {
                        porcentajeMinimo = '';
                        //}
                        capSolPor = obj.REGISTRO[key].capacidadPagoPorcentaje
                        stSol = obj.REGISTRO[key].StatusSol
                        stcot = obj.REGISTRO[key].StatusCot
                        // console.log('->' + respuesta + '<-');
                        btn = $("#<%=cmbGuardar.ClientID%>");
                    if (respuesta == 0) { btn.attr('disabled', true) }
                    else { btn.attr('disabled', false) }

                    if (respuesta == 0) {
                        PopUpLetrero(descripcion);
                        $('#dvRecCot').show('fold', options, 500, '');
                        //$('#dvFacturacion').hide('fold', options, 500, '');
                        if (!primeraVez || (primeraVez && $("#lblResultado").text() == '')) { $("#lblResultado").text(descripcion) }

                  
                            $("#lblCapacidadSol").text(new Intl.NumberFormat("es-MX", { minimumFractionDigits: 2 }).format(capSol))
                            $("#lblCapacidadCot").text(new Intl.NumberFormat("es-MX", { minimumFractionDigits: 2 }).format(capCot))
                      

                        //$("#lblCapacidadSol").text(new Intl.NumberFormat("es-MX", { minimumFractionDigits: 2 }).format(capSol))
                        $("#lblPlazoSol").text(PlzSol)   
                        //$("#lblCapacidadCot").text(new Intl.NumberFormat("es-MX", { minimumFractionDigits: 2 }).format(capCot))
                        $("#lblPlazoCot").text(PlzCot)
                        $("#lblAgenciaSol").text(AgSol)
                        $("#lblAgenciaCot").text(AgCot)
                        $("#lblporcentajeInfo").text(porcentajeMinimo)
                        $("#lblporcentajeDato").text(capSolPor)
                        $("#lblstatSol").text(stSol)
                        $("#lblstatCot").text(stcot)
                    }
                    else if (respuesta == 1) {
                        var estcot = $('[id$=hdnEst_Cot]').val();
                        if (estcot == "False") {
                            //PopUpLetrero(descripcion);
                            btn = $("#<%=cmbGuardar.ClientID%>");
                            btn.attr('disabled', false);
                            
                            btn_va = $("#<%=btn_validar.ClientID%>");
                            //btn_v.attr('disable', false);
                            btn_va.attr('visible', true);
                        } else {
                            btn = $("#<%=cmbGuardar.ClientID%>");
                            btn.attr('disabled', true);
                           
                            btn_va = $("#<%=btn_validar.ClientID%>");
                            //btn_v.attr('disable', true);
                            btn_va.attr('visible', false);
                        }



                        $('#lblCotizacion').text($('#txtCotizacion').val())
                        //$('#lblMonto').text(capCot)
                        //$('#lblPlazo').text(PlzCot)
                        if (!primeraVez || (primeraVez && $("#lblResultado").text() == '')) { $("#lblResultado").text(descripcion) }
                        $('#dvRecCot').show('fold', options, 500, '');
                      
                            $("#lblCapacidadSol").text(new Intl.NumberFormat("es-MX", { minimumFractionDigits: 2 }).format(capSol))
                            $("#lblCapacidadCot").text(new Intl.NumberFormat("es-MX", { minimumFractionDigits: 2 }).format(capCot))
                        
                       
                        $("#lblPlazoSol").text(PlzSol)                       
                        $("#lblPlazoCot").text(PlzCot)
                        $("#lblAgenciaSol").text(AgSol)
                        $("#lblAgenciaCot").text(AgCot)
                        $("#lblporcentajeInfo").text(porcentajeMinimo)
                        $("#lblporcentajeDato").text(capSolPor)
                        $("#lblstatSol").text(stSol)
                        $("#lblstatCot").text(stcot)
                    }

                    else if (respuesta == 2) {
                        PopUpLetrero(descripcion);
                        $('#lblCotizacion').text($('#txtCotizacion').val())
                        if (!primeraVez || (primeraVez && $("#lblResultadoMoral").text() == '')) { $("#lblResultadoMoral").text(descripcion) }
                        $('#dvRecCotmoral').show('fold', options, 500, '');
                        $("#lblagenciaSolMoral").text(AgSol)
                        $("#lblageciaCtoMoral").text(AgCot)
                        $("#lblporcentajeInfo").text(porcentajeMinimo)
                        $("#lblporcentajeDato").text(capSolPor)
                        $("#lblstatSol").text(stSol)
                        $("#lblstatCot").text(stcot)
                    }
                    else if (respuesta == 3) {
                        //$('#lblCotizacion').text($('#txtCotizacion').val())
                        //$("#lblResultadoMoral").text(descripcion);
                        //$('#dvRecCotmoral').show('fold', options, 500, '');
                        //$("#lblagenciaSolMoral").text(AgSol)
                        //$("#lblageciaCtoMoral").text(AgCot)
                        //$("#lblporcentajeInfo").text(porcentajeMinimo)
                        //$("#lblporcentajeDato").text(capSolPor)

                        if (!primeraVez || (primeraVez && $("#lblResultado").text() == '')) { $("#lblResultado").text(descripcion) }
                        PopUpLetrero(descripcion);
                        $('#dvRecCot').show('fold', options, 500, '');

                      
                            $("#lblCapacidadSol").text(new Intl.NumberFormat("es-MX", { minimumFractionDigits: 2 }).format(capSol))
                            $("#lblCapacidadCot").text(new Intl.NumberFormat("es-MX", { minimumFractionDigits: 2 }).format(capCot))
                      

                        //$("#lblCapacidadSol").text(new Intl.NumberFormat("es-MX", { minimumFractionDigits: 2 }).format(capSol))
                        $("#lblPlazoSol").text(PlzSol)
                        //$("#lblCapacidadCot").text(new Intl.NumberFormat("es-MX", { minimumFractionDigits: 2 }).format(capCot))
                        $("#lblPlazoCot").text(PlzCot)
                        $("#lblAgenciaSol").text(AgSol)
                        $("#lblAgenciaCot").text(AgCot)
                        $("#lblporcentajeInfo").text(porcentajeMinimo)
                        $("#lblporcentajeDato").text(capSolPor)
                        $("#lblstatSol").text(stSol)
                        $("#lblstatCot").text(stcot)
                    }
                })
            },
            error: function (msg) { PopUpLetrero("error"); }
        })
    request.onreadystatechange = null;
    request.abort = null;
    request = null;
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

    <div class="divPantConsul">
        <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width: 70%;">
                                    <legend>
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Cotizacion."></asp:Label>
                                    </legend>
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

        <div class="divAdminCatCuerpo">
            <div style="width: 100%; height: 100%; overflow: auto;">
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
                        <th class="campos">Status Documentos:                    
                        </th>
                        <th class="campos">
                            <asp:Label ID="lblStDocumento" Font-Underline="true" runat="server">
                            </asp:Label>
                        </th>
                        <th class="campos">Status Credito:                    
                        </th>
                        <th class="campos">
                            <asp:Label ID="lblStCredito" Font-Underline="true" runat="server">
                            </asp:Label>
                        </th>
                    </tr>
                </table>
                <br />
                <br />
                <table width="100%" class="cssCuerpo" id="tbValidaCotiza" style="background: white;">
                    <tr>
                        <td style="width: 40%; text-align: right; border-color: red;">Folio de Cotización *: 
                        </td>
                        <td style="width: 60%; text-align: left; border-color: red;">
                            <input onkeypress="return ManejaCar('N',0,this.value,this,event)" class="txt3BBVA" type="text" id="txtCotizacion" runat="server" maxlength="9" />
                        </td>
                    </tr>
                </table>
                <table width="95%" class="cssCuerpo">
                    <tr>
                        <td>    
                            <center>
                               <%-- <input runat="server" type="button" value="Validar" id="btnValidar" style="visibility:collapse" class="buttonBBVA2" onclick="fnValidaCot()" />--%>
                                <asp:Button runat="server" ID="btn_validar" CssClass="buttonBBVA2" Text="Validar"  OnClick="btn_validar_Click" />
                            </center>
                        </td>
                    </tr>
                </table>

                <br />
                <br />
            </div>
            <br />
            <br />
            <div id="dvRecCot"  style="top: 37%; left: 0%; width: 100%; height: 28%; display: none; position: absolute; font-family: Arial!important;  font-size: 8pt!important; color:#666666!important;">
                <%--background-color: #F3F7FB;--%>
                <table width="100%">
                    <%--            <tr>
                <td colspan = "3">
                    <label id = "lblResultado"></label>
                </td>
            </tr>--%>
                    <tr>
                        <th></th>
                        <th>Datos Autorizados.
                        </th>
                        <th><%--style="display:none;"--%>
                    Datos Cotizacion.
                        </th>
                        <th>
                            <span id="lblporcentajeInfo"></span>
                        </th>
                    </tr>
                    <tr>
                        <td>Capacidad.
                        </td>
                        <td style="text-align: center;">
                            <label id="lblCapacidadSol"></label>
                        </td>
                        <td style="text-align: center;">
                            <label id="lblCapacidadCot"></label>
                        </td>
                        <td style="text-align: center; display: none;">
                            <label id="lblporcentajeDato"></label>
                        </td>
                    </tr>
                    <tr>
                        <td>Plazo.
                        </td>
                        <td style="text-align: center;">
                            <label id="lblPlazoSol"></label>
                        </td>
                        <td style="text-align: center;">
                            <label id="lblPlazoCot"></label>
                        </td>
                        <td style="text-align: center;"></td>
                    </tr>
                    <tr>
                        <td>Agencia.
                        </td>
                        <td style="text-align: center;">
                            <label id="lblAgenciaSol"></label>
                        </td>
                        <td style="text-align: center;">
                            <label id="lblAgenciaCot"></label>
                        </td>
                        <td style="text-align: center;"></td>
                    </tr>
                    <tr>
                        <td>Estatus.
                        </td>
                        <td style="text-align: center;">
                            <label id="lblstatSol"></label>
                        </td>
                        <td style="text-align: center;">
                            <label id="lblstatCot"></label>
                        </td>
                        <td style="text-align: center;"></td>
                    </tr>
                </table>
            </div>
            <div id="dvRecCotmoral" style="top: 40%; left: 0%; width: 100%; height: 20%; display: none; position: absolute;">
                <%--background-color: #F3F7FB; --%>
                <table width="100%">
                    <tr>
                        <td colspan="3">
                            <label id="lblResultadoMoral"></label>
                        </td>
                    </tr>
                    <tr>
                        <th></th>
                        <th>Datos Autorizados.
                        </th>
                        <th>Datos Cotizacion.
                        </th>
                    </tr>
                    <tr>
                        <td>Agencia.
                        </td>
                        <td style="text-align: center;">
                            <label id="lblagenciaSolMoral"></label>
                        </td>
                        <td style="text-align: center;">
                            <label id="lblageciaCtoMoral"></label>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="divmessage" style="top: 68%; left: 0%; width: 100%; height: 10%; padding-top: 12px; padding-bottom: 10px; background-color: #F0F9FE; border: .5px solid #D8D8D8!important; position: absolute;">
                <table>
                    <tr>
                        <td>
                            <img src="../App_Themes/Imagenes/Alert.png" alt="alert" /></td>
                        <td class="CotizadotTagP" style="color: Gray; font-family: Arial; font-size: 10px;">
                            <label id="lblResultado" style="font-size: 12px;"></label>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="dvObservacionesCol" style="top: 60%; left: 0%; width: 100%; height: 40%; position: absolute; display: none;">
                <table>
                    <tr>
                        <td>
                            <label id="lblAceptaCotizacion">Acepta Cotización</label>
                            <input type="radio" name="lblAceptaCotizacion" value="1" checked="checked" />
                            SI
                    <input type="radio" name="lblAceptaCotizacion" value="0" />
                            NO
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <label id="lblObservacionesCot">Observaciones Cotizacion:</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <textarea id="txtAreaObservacionesCot" rows="3" cols="100"></textarea>
                        </td>
                        <td>
                            <input type="button" id="btnGuardaObservacionesCot" value="Guardar" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblComentariosAnteriores">Comentarios Anteriores:</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <textarea id="ComentariosAnteriores" rows="3" cols="100" disabled="disabled"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div id="divautoriza" style="display: none">
            <%--<cc1:ModalPopupExtender ID="mpoAutorizar" runat="server" PopupControlID="popAutoriza" TargetControlID="btnAutorizar" CancelControlID="btnCancelAutoriza" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>--%>
            <%--<asp:Panel ID="popAutoriza" runat="server" Text="Autorización" CssClass="cajadialogo">--%>
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
                            <input id="btnGuardarAutoriza" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar(id)" />
                            <asp:Button ID="btnCancelAutoriza" runat="server" Text="Cancelar" SkinID="btnGeneral" />
                        </td>
                    </tr>
                </table>
            <%--</asp:Panel>--%>
        </div>

        <div id="divcancela" class="cajadialogo" style="display:none; z-index: 1010 !important; position: absolute; background-color: white; top:15%; left:31%; width: 220px;">
            <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID="btnCancelar" PopupControlID="popCancela" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>--%>
            <%--<asp:Panel ID="popCancela" runat="server" CssClass="cajadialogo ">--%>
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
                            <input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" class="Text" onclick="btnGuardar('Cancelar')"/>
                            <input id="btnCancelCancelaClose" type ="button" value="Cancelar" class="Text" onclick="closeCanceDiv()" />
                            <%--<asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" class="Text" />--%>
                        </td>
                    </tr>
                </table>

            <%--</asp:Panel>--%>
        </div>

        <div class="divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle" class="fieldsetBBVA">
                        <center>
                            <asp:Button runat="server" ID="btnRegresar" Text="Regresar" CssClass="buttonSecBBVA2" />
                            <%--<input type="button" class="Text" value="Regresar" onclick="fnRegresar();"  />--%>
                            <input id="cmbGuardar" runat="server" type="button" value="Procesar" class="buttonBBVA2" onclick="$(this).prop('disabled', true); AvanzarTarea();" />
                            <asp:Button runat="server" ID="btnAutorizar" Visible="false" Text="Autorizar" />
                            <asp:Button runat="server" ID="btnCancelar" CssClass="buttonSecBBVA2" OnClientClick="mostrarCanceDiv()" Text="Cancelar" />
                            <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />
                        </center>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="divbtncollap" style="visibility: collapse">
            <asp:Button runat="server" ID="btnproc" OnClick="btnproc_Click"/>
        </div>
    <asp:HiddenField ID="hdnIdFolio" runat="server" />
    <asp:HiddenField ID="hdnIdPantalla" runat="server" />
    <asp:HiddenField ID="hdnUsua" runat="server" />
    <asp:HiddenField ID="hdnResultado" runat="server" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" />
    <asp:HiddenField ID="hdNomPantalla" runat="server" />
    <asp:HiddenField ID="hdnEnable" runat="server" />
    <asp:HiddenField ID="hdnMensualidad" runat="server" />
    <asp:HiddenField ID="hdnPlazo" runat="server" />
    <asp:HiddenField ID="hdnResultado1" runat="server" />
    <asp:HiddenField ID="hdnResultado2" runat="server" />
    <asp:HiddenField ID="hdnCotizacion" runat="server" />
    <asp:HiddenField ID="hdnEst_Cot" runat="server" />
</asp:Content>
