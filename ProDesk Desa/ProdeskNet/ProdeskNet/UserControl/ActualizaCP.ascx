<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ActualizaCP.ascx.vb" Inherits="UserControl_ActualizaCP" %>

<%--RQ-PD31: DJUAREZ: 08/03/2018: SE CREA POPUP PARA MODIFICAR LA COLONIA CUANDO SE GUARDE LA COLONIA "OTRO" --%>

<script type="text/javascript">
    $(document).ready(function () {
        var id_sol = $.urlParam("solicitud");
        JSONCodigoPostal(id_sol);
    });

    function mostrarDivActualizaCP() {
        $('#ventanaContain').show();
        $("#divActualizaCP").show();
    }

    function ocultarGuardarCPDiv() {
        $('#ventanaContain').hide();
        $("#divActualizaCP").hide();
    }

    function btnGuardarCP() {
        var solicitud = $.urlParam("sol");
        var codigoPostal = $('[id$=txtcodigoPostal]').val();
        var colonia = $('[id$=txtcolonia]').val();
        alert(solicitud + codigoPostal + colonia)
    }

    function jsonBack(errorLabel, destino, successfully, datos) {
        var settings = { type: "POST", url: "", async: false, data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
        settings.url = destino
        settings.success = successfully
        settings.data = datos
        settings.failure = function (response) { alert("Error al cargar : " + errorLabel.toString()); }
        $.ajax(settings);
    }

    function JSONCodigoPostal(folio) {
        var destino = "inicio.aspx/JSONCodigoPostal";
        var successfully = OnSuccessCodigoPostal;
        var datos = '{"id_sol":"' + folio.toString() + '", "opcion": "1"}';
        jsonBack('No fue posible cargar el C.P.', destino, successfully, datos);
    }

    function OnSuccessCodigoPostal(response) {
        var items = $.parseJSON(response.d);
        if (items.Colonia.toString() == "-1" || items.Colonia.toString().toUpperCase() == "OTRO") {
            $("#divBtnActualizaCP").show();
            if (items.codigoPostal.toString() != "") {
                $('[id$=txtcodigoPostal]').val(items.codigoPostal.toString());
                $('[id$=txtcodigoPostal]').prop('disabled', true);
                fillddl('txtCOLONIA15', 'txtcodigoPostal');
                $("[id$=ddltxtCOLONIA15] option[value='-1']").remove();
            }
        }
        //else { $.each(items.documentos, function (i, item) { $('#documento_1').append($('<option>', { value: item.id, text: item.nombre, folioDoc: item.folio })); }); }
    }

    function btnGuardarColonia() {
        var id_sol = $.urlParam("solicitud");
        var colonia = $("[id$=ddltxtCOLONIA15] option:selected").text();
        var opcion = 9;
        if (colonia != "") {
            GuardarColonia(id_sol, colonia, opcion);
        }
        else {
            alert("Debe seleccionar una Colonia");
        }
    }

    function GuardarColonia(solicitud, colonia, intBandera) {
        data = "{id_sol: '" + solicitud + "', opcion: '" + intBandera + "', colonia: '" + colonia + "' }";
        $.ajax({
            type: "POST",
            url: "inicio.aspx/UpdateColonia",
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) { if (msg.d.indexOf("error") === -1) { ocultarGuardarCPDiv(); PopUpLetrero(msg.d); $("[id$=divBtnActualizaCP]").hide(); } else { PopUpLetrero(msg.d); }; },
            error: function (msg) { PopUpLetrero(msg.d); }
        })
    }

</script>

<div id="divActualizaCP" class="cajadialogo" style="display: none; z-index: 1000 !important; position: absolute; background-color: white; top:15%; left:31%; width: 220px;">
    <div class="tituloConsul">
        <asp:Label ID="Label1" runat="server" Text="Actualiza Colonia" />
    </div>
    <table style="width:100%;">
        <tr>
            <td class="campos">Codigo Postal:</td>
            <td>
                <input type="text" ID="txtcodigoPostal" class="txt2BBVA " runat="server" Style="width: 95% !important;"/>
            </td>
        </tr>
        <tr>
            <td class="campos">Colonia:</td>
            <td>
                <select ID="ddltxtCOLONIA15" runat="server" class="select2BBVA" Style="width: 100% !important;">
                    <option value="0">Selecciona una colonia</option>
                </select>
            </td>
        </tr>
        <tr style="width: 100%">
            <td>
                <asp:HiddenField runat="server" ID="HiddenField1" />
            </td>
            <td align="left" valign="middle">
                <input id="btnGuardarCP" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardarColonia();" />
                <input ID="btnCancelaCP" type="button" runat="server" value="Cancelar" class="Text " onclick="ocultarGuardarCPDiv();" />
            </td>
        </tr>

    </table>
</div>
<div id="divBtnActualizaCP" style="display: none; position: absolute; top: 91%; z-index: 1; left: 1%; height: 8%;">
    <input type="button" value="Actualizar C.P" class="buttonSecBBVA2" onclick="mostrarDivActualizaCP();" id="btnActualizaCP" />
</div>
<%--<div>
    <input type="button" value="Actualizar C.P" class="buttonSecBBVA2" onclick="mostrarDivActualizaCP();" id="btnCancelarNew"  />
</div>--%>