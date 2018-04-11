<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaCrearFormulaRN.aspx.vb" Inherits="consultaCrearFormulaRN" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">

<script language = "javascript" type= "text/javascript">
    function validaFormula(id) {
        boton = $("#" + id)[0];
        var valor;
        var valor1;
        if (boton.tagName == "LABEL") {
            valor = "[" + boton.innerHTML + "]";
            valor1 = "[" + boton.id.replace("lbl", "") + "]";
        }
        else if (boton.type == "button") {
            valor = "[" + boton.value + "]";
            if (boton.value == "Y") {
                valor1 = "[AND]";
            }
            else if (boton.value == "O") {
                valor1 = "[OR]"
            }
            else {
                valor1 = "[" + boton.value + "]";
            }            
        }
        texto = $("#txtFormula");
        texto1 = $("#txtFormulaEsc");
        texto.val(texto.val() + valor);
        texto1.val(texto1.val() + valor1);
        fnValidaForMatematica(texto1.val());
    }    

    function fnBtnBorrar() {
        texto = $("#txtFormula").val();
        texto1 = $("#txtFormulaEsc").val();
        texto = texto.replace(/]/g, "]|");
        texto1 = texto1.replace(/]/g, "]|");
        arraytext = texto.split("|");
        arraytext1 = texto1.split("|");
        tamannio = arraytext.length-2;
        arraytext.splice(tamannio, 1)
        arraytext1.splice(tamannio, 1)     
        $("#txtFormula").val(arraytext.join().replace(/,/g, ""));
        $("#txtFormulaEsc").val(arraytext1.join().replace(/,/g, ""));
    }    

    function fnGuardaFormula() {
        texto1 = $("#txtFormulaEsc");
        texto2 = $("#txtNombreFor");
        if (fnValidaForMatematica(texto1.val())) {
            if (texto1.val() == '' || texto2.val() == '') {
                PopUpLetrero('La informacion * es requerida');
                return;
            }
            btnInsertar("exec sp_insFormula '" + texto1.val() + "', '" + texto2.val() + "';", "tbFormulaRN");
            $("#txtFormula").val('');
            texto1.val('');
            texto2.val('');
        }
        else {
            PopUpLetrero('Valida tu formula');
        }
    }

    function fnMuestraBtn(id) {
        if (id == "btnlogicas") {
            $("#trl").show("slow");
            $("#tra").hide("fast");
        }
        else {
            $("#trl").hide("fast");
            $("#tra").show("slow");
        }
    }

    function fnMuestraTablaRN() {
        var valor = $("#plus")[0].innerHTML;
        if (valor == "+") {
            $("#trFormulaRN").show("fast");
            $("#plus")[0].innerHTML = "-";
        }
        else {
            $("#trFormulaRN").hide("fast");
            $("#plus")[0].innerHTML = "+";
        }
    }

    function btnBorrarFromula(id) {
        var nombre = id.replace("img", "")
        btnInsertar("delete pdk_tb_Formularn where idFRN = " + nombre + ";", "CrearFormula");        
    }

    $(document).ready(function () {
        fillgv('CrearFormula', '');            
    });
</script>

<div class="divPantConsul">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td>Crear formula regla de negocios</td>                                
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    <div class="divCuerpoConsul">
        <table id = "CrearFormula" class = "resulGridsh">
        </table>
    </div>
</div>

</asp:Content>