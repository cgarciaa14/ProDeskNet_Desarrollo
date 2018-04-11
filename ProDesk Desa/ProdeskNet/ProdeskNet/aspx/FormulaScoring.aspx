<%@ Page Language="vb" MasterPageFile="~/aspx/Home.Master"   AutoEventWireup="false" CodeFile="FormulaScoring.aspx.vb" Inherits="FormulaScoring" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID ="cphPantallas" runat="server" >

<script type ="text/javascript" language ="javascript" >

    function Operador(objeto) {
        var campo = $("#" + objeto)[0]
        var campo1 = $("[id$=txtOperacion]").val();
        var campo3 = $("[id$=txtOperanum]").val();
         
        if (campo1 == '') {
            campo1 = '[' + campo.innerHTML + ']'
            campo3 ='['+ campo.id.replace('txt_','') + ']'
        } else {
            campo1 += '[' + campo.innerHTML + ']'
            campo3 += '[' + campo.id.replace('txt_','') + ']'
        }

        $("[id$=txtOperacion]").val(campo1);
       $("[id$=txtOperanum]").val(campo3);
       fnValidaForMatematica($("[id$=txtOperanum]").val());  
    }

    
    function Operadores(objeto) {
        var campo = $("#" + objeto)
        var campo2 = $("[id$=txtOperanum]").val();
        var campo1 = $("[id$=txtOperacion]").val();
        if (campo1 == '') {
            campo1 = '[' + campo.val() + ']'
            campo2 = '[' + campo.val() + ']'
        } else {
            campo1 += '[' + campo.val() + ']'
            campo2 += '[' + campo.val() + ']'
        }
        $("[id$=txtOperacion]").val(campo1);
        $("[id$=txtOperanum]").val(campo2);
        fnValidaForMatematica($("[id$=txtOperanum]").val()); 
    }    

    function fnBtnBorrar() {
        var texto = $("[id$=txtOperacion]").val();
        var texto1 = $("[id$=txtOperanum]").val();
        texto = texto.replace(/]/g, "]|");
        texto1 = texto1.replace(/]/g, "]|");
        arraytext = texto.split("|");
        arraytext1 = texto1.split("|");
        tamanio = arraytext.length - 2;
        tamanio2 = arraytext1.length - 2;
        arraytext1.splice(tamanio2, 1)
        arraytext.splice(tamanio, 1)
        $("#txtOperacion").val(arraytext.join().replace(/,/g, ""));
        $("#txtOperanum").val(arraytext1.join().replace(/,/g, ""));


    }

    function ReplaceAll(text, busca, remplaza) {
        var idx = text.toString().indexOf(busca);
        while (idx != -1) {
            text = text.toString().replace(busca, remplaza);
            idx = text.toString().indexOf(busca, idx);
        }
        return text;

    }

    function fnmostrar() {
        var valor = $("#blbformula")[0].innerHTML;
        if (valor == "+") {
            $("#trFormula").show("fast");
            $("#blbformula")[0].innerHTML = "-";

           
        }
        else {
            $("#trFormula").hide("fast");
            $("#blbformula")[0].innerHTML = "+";
        }

    }

    function fnActualiza(td, ident,texto,texto1,nombre) {
        var control = $(td)[0]
        $("[id$=lblcve]").val(ident);
        $("[id$=txtOperanum]").val(texto1)
        $("[id$=txtOperacion]").val(texto);
        $("[id$=txtNomFormula]").val(nombre);    
    }

    function bntGuardar() {
        var texto = $("#txtOperanum");
        var texto3 = $("[id=txtOperacion]").val()
        var texto1 = $("[id$=txtNomFormula]").val();
        var texto2 = ''
        var usuario = $("[id$=hdnIdRegistro]").val();
        var tipofor = $("[id$=ddlformula]").val();
        var guardar = $("[id$=lblcve]").val();

        if (texto.val() == '') {
            alert('El campo de la formula no puede ir vacia');
            return;
        }
        if (texto1 == '') {
            alert('El nombre de la formula no puede ir vacia');
            return;
        }

        if (fnValidaForMatematica(texto.val())) {

            texto2 = ReplaceAll(texto.val(), '[(]', '(');
            texto2 = ReplaceAll(texto2, '[+]', '+');
            texto2 = ReplaceAll(texto2, '[-]', '-');
            texto2 = ReplaceAll(texto2, '[*]', '*');
            texto2 = ReplaceAll(texto2, '[/]', '/');
            texto2 = ReplaceAll(texto2, '[)]', ')');

            if (guardar == 0) {
                insertas = "insert PDK_FORMULA values('" + texto1 + "','" + texto2 + "',getdate()," + usuario + ",'" + texto3 + "'," + tipofor + ")"
               
            } else {
                insertas = " update PDK_FORMULA set PDK_FORMULA_GENERADA= '" + texto2 + "',PDK_FORMULA_DATOS= '" + texto3 + "'  WHERE PDK_ID_FORMULA =" + guardar
              
            }

            btnInsUpdReload(insertas, "TbseccionDat", "ddlformula");
            //btnInsertar("insert PDK_FORMULA values('" + texto1 + "','" + texto2 + "',getdate()," + usuario + ",'"+ texto3 + "'," + tipofor + ")")
            $("[id=txtOperanum]").val('');
            $("[id=txtOperacion]").val('');
            $("[id=txtNomFormula]").val('');

        } else {
            PopUpLetrero('Valida tu formula')
        }

        fillgv('TbseccionDat', 'ddlformula');
         
           
    }


    $(document).ready(function () {
        fillddl('formula', '');
    });  

</script>
  <div class ="divPantConsul">
    <div class="divFiltrosConsul">
      <table class ="tabFiltrosConsul">
        <tr  class ="tituloConsul">
          <td colspan="2" style="width:70%;">Formula Scoring </td>
        </tr> 
      </table>
    </div>
    <div class="divCuerpoConsul">
    <table class="resulGridsh" width="100%">
    <tr>
     <td class="campos">Tipo Formula:</td>
     <td><asp:DropDownList ID="ddlformula" runat="server" CssClass="Text" Width="150px" onChange="fillgv('TbseccionDat','ddlformula');"></asp:DropDownList> </td>
     </tr>
     <tr>
       <td colspan="4">
         <table id="TbseccionDat" class="resulGridsh">
         </table>             
       </td>     
     </tr>        
    </table>     
    </div> 
  </div>
    <asp:HiddenField runat="server" ID="hdnIdRegistro" />
</asp:Content>
