<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="altaProductoPorPlazo.aspx.vb" Inherits="altaProductoPorPlazo" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPantallas" >
<script language="javascript" type ="text/javascript" >
    $(document).ready(function () {
        fillddl('ProductoPlaz', '')
        fillddl('seccionDatos', '')
        fillddl('tipoobjeto', '')
    
    });

    function fnvalidaNum() {
        var obje = $("[id$=ddltipoobjeto]").val();
        var valor1 = $("#txtValor")[0]

        $("#txtValor").unbind("keypress");
        if ((obje == 2) || (obje == 4) || (obje == 5)) {
            $("[id$=txtValor]").val('');
            $("#txtValor").keypress(function () { ManejaCar('N', 1, this.value, this) });

        } else if (obje == 3) {
            $("[id$=txtValor]").val('');
            $("#txtValor").keypress(function () { ManejaCar('j', 1, this.value, this) });


        } else {
            $("[id$=txtValor]").val('');
            $("#txtValor").keypress(function () { ManejaCar('C', 1, this.value, this) });

        }



    }

    function EditaTXT(td, ident, cve) {
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
                $("#txtEditPrecios").on("keypress", function () { ManejaCar('D', 1, this.value, this) })
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
        etiqueta = etiqueta.substring(0, 9);
        var campo;
        switch (etiqueta) {
            case 'lbl_valor':
                campo = 'PDK_PRODPLAZO_VALOR';
                break;
          
            default:
                alert('el campo no existe en la base de datos');
                return;
        }

        update = "update PDK_PRODUCTO_PLAZO set " + campo + " = '" + ValorNuevo + "' where PDK_ID_PRODPLAZO = " + ident;
        btnInsUpdReload(update, "grvSeccionPlazo", "ddlProductoPlaz, ddlseccionDatos");
    }

    function funchek(check,cve) {
        var chequete = 0
        if (check.checked == true) { chequete = 2 } else { chequete = 3 }
        update = "update PDK_PRODUCTO_PLAZO set  PDK_PRODPLAZO_STATUS =" + chequete + " where PDK_ID_PRODPLAZO = " + cve;
        btnInsUpdReload(update, "grvSeccionPlazo", "ddlProductoPlaz, ddlseccionDatos");
    
    
    }
    function inserta() {
        var valor1 = $("[id$=txtValor]").val();
        var usua = $("[id$=hdnIdRegistro]").val();
        var producto = $("[id$=ddlProductoPlaz]").val();
        var seccion = $("[id$=ddlseccionDatos]").val();
        var obje = $("[id$=ddltipoobjeto]").val();
        var cadena = "";
        if (valor1 == '') {
            alert('El valor 1 no puede ir vacia');
            return;
        }
        if (producto == '') {
            alert('El producto no puede ir vacia');
            return;
            
        }

        if (seccion == '') {
            alert('La seccion de datos no puede ir vacia');
            return;
        }
        if (obje == '') {
            alert('El objeto no puede ir vacia');
            return;
        }


        //        cadena = "IF NOT EXISTS (SELECT * FROM PDK_PRODUCTO_PLAZO WHERE PDK_ID_PRODUCTOS=" + producto + " AND PDK_ID_SECCION_DATO=" + seccion + " AND PDK_PRODPLAZO_VALOR='" + valor1 + "') BEGIN INSERT INTO PDK_PRODUCTO_PLAZO (PDK_ID_PRODUCTOS,PDK_ID_SECCION_DATO,PDK_ID_TIPO_OBJETO,PDK_PRODPLAZO_VALOR,PDK_PRODPLAZO_MODIFI,PDK_CLAVE_USUARIO) VALUES(" + producto + "," + seccion + "," + obje + ",'" + valor1 + "',GETDATE()," + usua + ") END"
        cadena = "EXEC sp_ValidarScoring" + " " + seccion + ",'" + valor1 + "',''," + usua + ",'" + producto + "'," + obje + ",2"
        validaScoring(cadena, "grvSeccionPlazo", "ddlProductoPlaz,ddlseccionDatos");




        $("[id$=txtValor]").val('');



    }

</script>

<div class="divPantConsul">
  <div class="divFiltrosConsul">
    <table class="tabFiltrosConsul">
      <tr class ="tituloConsul">
        <td colspan="2" style="width :70%;" >Producto por Plazo</td> 
      </tr> 
    </table>  
  </div> 
  <div class="divCuerpoConsul">
  <table class="resulGridsh" width="100%">
   <tr>
     <td class="campos" style="text-align:center;">Producto:</td>
     <td class="campos" style="text-align:center;">Seccion Datos:</td>
     <td class="campos" style="text-align:center;">Tipo Objetos:</td>
     <td class="campos" style="text-align:center;">Valor:</td>
     <td></td>
   
   </tr>
   <tr>
    <td><asp:DropDownList ID="ddlProductoPlaz" runat="server" CssClass="Text" Width="150px"></asp:DropDownList></td>
    <td><asp:DropDownList ID="ddlseccionDatos" runat="server" CssClass="Text" Width="150px" onChange="fillgv('grvSeccionPlazo','ddlProductoPlaz,ddlseccionDatos');"></asp:DropDownList></td>
    <td><asp:DropDownList ID="ddltipoobjeto" runat="server" CssClass="Text" Width="150px" onChange="fnvalidaNum()"></asp:DropDownList></td>
    <td><input id="txtValor" type="text" class="resul" style="width:140px;" /></td>
    <td><input id="cmbInsertar" value="Insertar" class="TextLink" type="button" onclick="inserta()" /> </td>
   </tr>
  </table> 
  <table class="resulGridsh" width="100%">
  <tr>
    <td >
     <div style="height:500px; overflow:auto;">
       <table class="resulGrid"  id="grvSeccionPlazo">
       
       </table>
       
     </div>
    </td>
   </tr>
  </table> 
  </div>  

</div>  




 <asp:HiddenField runat="server" ID="hdnIdRegistro" />

</asp:Content> 
