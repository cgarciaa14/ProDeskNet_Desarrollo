<%@ Page Language="vb" AutoEventWireup="false" CodeFile="consultaReglasNegocio.aspx.vb" Inherits="consultaReglasNegocio" MasterPageFile="~/aspx/Home.Master"%>
<%@ MasterType VirtualPath="~/aspx/home.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">

    <script type = "text/javascript">

        function muestraPantalla() {
            esconde = $('.divPantConsul');
            muestra = $('#ventanaPopup1');
            //esconde.css({ "opacity": ".20" });
            //muestra.removeClass("oculta");
            muestra.addClass("ventanaAgregar");
            centraVentana(muestra);
            //muestra.css({ "opacity": "1" });
            muestraVentana(esconde, muestra);
            $('#tb1').removeClass('allGv'); limpiatxt()
        }
  
    function insertaCrear() {

        var RN = $("[id$=txtRN]").val();
        var Val1 = $("[id$=txtValorNva1]").val();
        var Val2 = $("[id$=txtValorNva2]").val();
        var cond = $("[id$=ddlCondicionNva]").val();
        var rechazo = $("[id$=ddlRechazo]").val(); 

        if (cond == 2) {
            if (Val2 == "") {
                alert('Necesita ingresar datos en el campo VALOR 2');
                return;
            }
            if (parseInt(Val1) > parseInt(Val2)) {
                alert('El campo VALOR 1 "' + parseInt(Val1) + '" NO puede ser MAYOR que el campo VALOR 2 "' + parseInt(Val2) + '"');
                return;
            }
        }
        else {
            if (RN == "") {
                alert("El campo Regla de Negocio no puede ir vacio");
                return;
            }
            if (Val1 == "") {
                alert("El campo Valor1 no puede ir vacio");
                return;
            }
            if (Val2 != "") {
                alert('El campo VALOR 2 NO sera tomado en cuenta');
                Val2 = "0"
            }
            else {
                Val2 = "0";
            }
        }

        if (rechazo == "") {
            alert("Se tiene que seleccionar un motivo de rechazo");
            return;
        }

        btnInsUpd("insert reglas_negocio values ('" + RN + "'," + cond + ",'" + Val1 + "','" + Val2 + "', " + rechazo + ")", "tb2");
        ocultaVentana($(".divPantConsul"), $("#ventanaPopup1"));
        $("#tb1").addClass("allGv");
    }

    function EditaTXT(td, ident) {        
        var tdControl = $(td)[0];
        $(td).attr("editable", "True");
        if (tdControl != null && tdControl.innerText != "" && $(td).attr("editable") != null && $(td).attr("editable") == "True") {
            var oldValue = tdControl.innerHTML.replace("<label></label>", "");
            tdControl.innerHTML = '<input id="txtEditPrecios" onkeydown="FiltradoKey(event, this, ' + ident + ');" style ="text-align:center;" onmouseout = "OcultarTxtEdit(this, ' + ident + ');"'
                                   + 'class="Text" type="Text" value="' + oldValue + '" />';
            $(".txtEditPrecios").focus().select();
        }
    }

    function OcultarTxtEdit(val, ident) {
        if (val != null) {
            var txt = $(val);
            var td = txt[0].parentNode;
            if (txt[0].defaultValue != txt[0].value) {
                td.setAttribute("EditVal", txt[0].value);
                td.className = "txtEdit";
                fnActualizar(td, txt[0].value, ident)
            }
            td.innerHTML = txt[0].value;
        }
    }

    function FiltradoKey(e, txt, ident) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 27 || code == 13 || code == 9) {
            OcultarTxtEdit(txt, ident);
        }
    }

    function fnActualizar(objetoAct, ValorNuevo, ident) {
        etiqueta = objetoAct.id.replace(ident, '');
        var campo;
        switch (etiqueta) {
            case 'lblVal1':
                campo = 'RN_NO_VALOR1';
                break;
            case 'lblVal2':
                campo = 'RN_NO_VALOR2';
                break;
            case 'lblRNNom':
                campo = 'RN_NOM_REGLADNEGOCIO';
                break;
            default:
                alert('el campo no existe en la bd');
                return;
        }
        if (ValorNuevo == '') {
            ValorNuevo = 0;
        }
        update = "update reglas_negocio set " + campo + " = '" + ValorNuevo + "' where rn_id = " + ident;
        btnInsUpd(update);
        cargaPantallaReglasNegocio();
    }

    function btnActualizar(objetoAct, val) {

        var objjq = "";
        var valorNvo = "";
        objjq = $("[id$=" + objetoAct.id + "]").val();
        valorNvo = objetoAct.value;
        var objeto = objetoAct.id.replace(val, '');
        var valorNvo = valorNvo.replace('"', '');               

        switch (objeto) {
            case 'ddlRN':
                campo = 'RN_ID_CONDICION';
                break;
            case 'ddlRechazos':
                campo = 'PDK_ID_CAT_RECHAZOS'
                break;
            default:
                alert('el campo no se encuentra');
                return;
        }

        if (valorNvo == '') {
            valorNvo = 0;
        }
        btnInsUpd("update reglas_negocio set " + campo + " = " + valorNvo + " where RN_ID = " + val + ";");
        cargaPantallaReglasNegocio();
    }

    function limpiatxt() {
        $("[id$=txtRN]").val('');
        $("[id$=txtValorNva1]").val('');
        $("[id$=txtValorNva2]").val('');
    }

    function cargaPantallaReglasNegocio() {
        fillddl('RN', '');
        fillddl('CondicionNva', '');
        fillddl('Rechazo', '');
        fillgv('tb2', '');
        $("#ventanaPopup1Cerrar").click(function () { ocultaVentanaFast($(".divPantConsul"), $("#ventanaPopup1")); $("#tb1").addClass("allGv"); });
    }

    $(document).ready(function () {
        cargaPantallaReglasNegocio();
    });
</script>

<div class="divPantConsul">
    <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td colspan="2" style="width:70%;">Reglas de Negocio</td>
                </tr>
            </table>
        </div>
    <div id = "Principal" class="divCuerpoConsul">        
            <table id = "tb2" class = "resulGrid">            
                <tr>
                    <td>
                        <table id = "resulGrid" width = "100%">            
                        </table>
                    </td>
                </tr>                        
            </table>             
            <table class = "resulGrid">
                <tr>                                
                    <td style = "text-align:right">
                        <input id = "btn" onclick = "muestraPantalla()" value = "Nuevo" type = "button" class = "Text"/>
                    </td>
                </tr>
            </table>
  </div>    
</div>
<div id="ventanaPopup1" class = "oculta" >
        <table width = "100%"> <%--class = "vPopP1"--%>
            <tr>                
                <th colspan = "10" style = "text-align: right;">
                    <b><a id="ventanaPopup1Cerrar" class = "link" style = "color:#000000">X</a></b>
                </th>
            </tr>
            <tr>
                <th colspan = "10">
                    Nueva Regla de Negocio
                </th>                
            </tr>                
            <tr>
                <td>
                    Regla de Negocio.
                </td>
                <td>
                    <asp:TextBox ID = "txtRN" runat = "server" CssClass = "Text" OnKeyPress= "ManejaCar('A',1,this.value,this)">
                    </asp:TextBox>
                </td>
                <td>
                    Condicion.
                </td>
                <td>
                    <asp:DropDownList ID = "ddlCondicionNva" runat = "server" Width = "100px" CssClass = "Text">
                    </asp:DropDownList>                    
                </td>
                <td>
                    Valor 1
                </td>
                <td>
                    <asp:TextBox ID = "txtValorNva1" runat = "server" CssClass = "Text" OnKeyPress="ManejaCar('D',1,this.value,this)"  >
                    </asp:TextBox>
                </td>
                <td>
                    Valor 2
                </td>
                <td>
                    <asp:TextBox ID = "txtValorNva2" runat = "server" CssClass = "Text" OnKeyPress="ManejaCar('D',1,this.value,this)"  >
                    </asp:TextBox>
                </td>
                <td>
                    Rechazo.
                </td>
                <td>
                    <asp:DropDownList ID = "ddlRechazo" runat = "server" Width = "100px" CssClass = "Text">
                    </asp:DropDownList>
                </td>
            </tr>            
            <tr>                
                <td colspan = "10" style = "text-align: right">
                    <input id = "btnCrear" onclick = "insertaCrear();" value = "Crear" type = "button" class = "Text"/>
                </td>
            </tr>
        </table>
    </div> 

    
</asp:Content>