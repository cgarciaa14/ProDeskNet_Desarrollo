<%@ Page Title=""  Language="vb" MasterPageFile="~/aspx/Home.Master"   AutoEventWireup="false" CodeFile="Scoring.aspx.vb" Inherits="Scoring" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID ="cphPantallas" runat="server" >
<script type ="text/javascript" language ="javascript" >

    function Insertar() {
        var valor1 = $("[id$=txtvalor1]").val();
        var valor2 = $("[id$=txtvalor2]").val();
        var peso = $("[id$=txtpeso]").val();
        var usua = $("[id$=hdnIdRegistro]").val();
        var seccion = $("[id$=ddlseccionDatos]").val();
        var obje = $("[id$=ddltipoobjeto]").val();
        var cadena = "";
         if (valor1 == '') {
            alert('El valor 1 no puede ir vacia');
            return;
        }
        if (valor2 == '') {
            $("[id$=txtvalor2]").val('0');
            valor2 = $("[id$=txtvalor2]").val();
        }
                  
        if (peso == '') {
            alert('El valor no puede ir vacia');
            return;
        }

        cadena = "EXEC sp_ValidarScoring" + " " + seccion + ",'" + valor1 + "','" + valor2 + "'," + usua + ",'" + peso + "'," + obje +",1"

        validaScoring(cadena, "grvseccion", "ddlseccionDatos");
       



        $("[id$=txtvalor1]").val('');
        $("[id$=txtvalor2]").val('');
        $("[id$=txtpeso]").val('');


    }

    function fnvalidaNum() {
        var obje = $("[id$=ddltipoobjeto]").val();
        var valor1 = $("#txtvalor1")[0]

        $("#txtvalor1, #txtvalor2").unbind("keypress");
        if ((obje == 2) || (obje == 4) || (obje == 5)) {
            $("[id$=txtvalor1], [id$=txtvalor2], [id$=txtpeso]").val('');
           $("#txtvalor1, #txtvalor2").keypress(function () { ManejaCar('N', 1, this.value, this) });

       } else if (obje == 3) {
           $("[id$=txtvalor1], [id$=txtvalor2], [id$=txtpeso]").val('');
           $("#txtvalor1, #txtvalor2").keypress(function () { ManejaCar('j', 1, this.value, this) });
          
           
       } else {
           $("[id$=txtvalor1], [id$=txtvalor2], [id$=txtpeso]").val('');
           $("#txtvalor1, #txtvalor2").keypress(function () { ManejaCar('C', 1, this.value, this) });

       }



   }
     
    function EditaTXT(td, ident,cve) {
        var tdControl = $(td)[0];
        $(td).attr("editable", "True");
        if (tdControl != null && tdControl.innerText != "" && $(td).attr("editable") != null && $(td).attr("editable") == "True") {
            var oldValue = tdControl.innerHTML.replace("<label></label>", "");
            tdControl.innerHTML = '<input id="txtEditPrecios" onkeydown="FiltradoKey(event, this, ' + ident + ');" style ="text-align:center;" onmouseout = "OcultarTxtEdit(this, ' + ident + ');"'
                                   + 'class="Text" type="Text" value="' + oldValue + '" />';
            if (cve == 3) {
                $("#txtEditPrecios").on("keypress", function () { ManejaCar('j', 1, this.value, this) })

            } else if ((cve == 2) || (cve == 4) || (cve == 5)) {
                $("#txtEditPrecios").on("keypress", function () { ManejaCar('N', 1, this.value, this) })
            } else if (cve == 30) {
                $("#txtEditPrecios").on("keypress", function () { ManejaCar('D',1,this.value,this) })
            } else {
                $("#txtEditPrecios").on("keypress", function () { ManejaCar('C', 1, this.value, this) })
            }

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
        etiqueta = etiqueta.substring(0, 6);
        var campo;
        switch (etiqueta) {
            case 'lbl_v1':
                campo = 'PDK_SCORING_VALOR1';
                break;
            case 'lbl_v2':
                campo = 'PDK_SCORING_VALOR2';
                break;
            case 'lbl_pe':
                campo = 'PDK_SCORING_PESO';
                break;
            default:
                alert('el campo no existe en la base de datos');
                return;
        }

        if (ValorNuevo == '') {
            ValorNuevo = 'N/A';
        }

        update = "update PDK_SCORING set " + campo + " = '" + ValorNuevo + "' where PDK_ID_SCORING = " + ident;
        btnInsUpdReload(update, "grvseccion", 'ddlseccionDatos');
    }




    $(document).ready(function () {
        fillddl('seccionDatos', '');
        fillddl('tipoobjeto', '');
        //fillgv('grvseccion', '');
       // fillddl('datosvalor', '');
    });  

</script>
  <div class ="divPantConsul">
    <div class="divFiltrosConsul">
      <table class ="tabFiltrosConsul">
        <tr class ="tituloConsul">
          <td colspan="2" style="width:70%;">Scoring </td>
        </tr> 
      </table>
    </div>
    <div class="divCuerpoConsul">
     <table  class="resulGridsh" width="100%">
      <tr>
        <td  class="campos">Seccion Datos:</td>
        <td><asp:DropDownList ID="ddlseccionDatos" CssClass="Text" Width="150px" onChange="fillgv('grvseccion','ddlseccionDatos');" runat="server" ></asp:DropDownList> </td>
         <td class="campos">Tipo Objeto:</td>
         <td><asp:DropDownList ID="ddltipoobjeto" CssClass="Text" width="150px" onChange="fnvalidaNum()"  runat ="server"></asp:DropDownList>  </td>
         <td></td>
         <td></td>
        <td></td>
      </tr>
      <tr> 
         <td class="campos">Valor1:</td>
        <td><input id="txtvalor1" type="text" style="width:140px;"   /> </td>        
         <td class="campos">Valor2:</td>
        <td><input id="txtvalor2" type="text" Style="width:140px;"  /> </td>
        <td class="campos">Peso:</td>
        <td><input id="txtpeso" type="text"onkeypress="ManejaCar('D',1,this.value,this)"  style="width:140px" /> </td>
        
       <td><input id="cmbinsertar" value="Insertar"  class="TextLink" type="button" onclick="Insertar()"   /> </td>      
      </tr> 
     </table>    
     <table class="resulGridsh" width="100%">
     <tr>
         <td colspan="12">
           <div style=" height:500px;   overflow:auto">
             <table id="grvseccion" class="resulGrid">
                 
             </table>
           </div>
         
         </td>
       </tr>
   
     </table>
    </div> 
    
  
  </div>
    <asp:HiddenField runat="server" ID="hdnIdRegistro" />
</asp:Content>