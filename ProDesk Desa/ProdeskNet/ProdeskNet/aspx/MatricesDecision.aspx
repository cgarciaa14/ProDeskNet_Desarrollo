<%@ Page Language="vb" MasterPageFile ="~/aspx/Home.Master"  AutoEventWireup="false" CodeFile="MatricesDecision.aspx.vb" Inherits="MatricesDecision" %>
<%@ MasterType VirtualPath="~/aspx/Home.Master"%>

<asp:Content ID="content1" ContentPlaceHolderID="cphPantallas" runat ="server" >
<script type ="text/javascript" language ="javascript" >

    function EditaTXT(td, ident) {
        var tdControl = $(td)[0];
        $(td).attr("editable", "True");
        if (tdControl != null && tdControl.innerText != "" && $(td).attr("editable") != null && $(td).attr("editable") == "True") {
            var oldValue = tdControl.innerHTML.replace("<label></label>", "");
            tdControl.innerHTML = '<input id="txtEditPrecios"  onkeydown="FiltradoKey(event, this, ' + ident + ');" style ="text-align:center;" onmouseout = "OcultarTxtEdit(this, ' + ident + ');"'
                                   + 'class="Text" type="Text"  value="' + oldValue + '" />';

            //$("#txtEditPrecios").select();
            $("#txtEditPrecios").on("keypress", function () { ManejaCar('k', 1, this.value, this) })
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
        var usua = $("[id$=hdnIdRegistro]").val();
        var tipotab = $("[id$=ddlmatriz]").val();
        var update;
        var campo;
        if (tipotab == 20) {
            switch (etiqueta) {
                case 'lblinf':
                    campo = 'PDK_MATRIZ_SCORIN_LIMINF';
                    break;
                case 'lblsup':
                    campo = 'PDK_MATRIZ_SCORIN_LIMSUP';
                    break;
            }

            if (ValorNuevo == '') {
                ValorNuevo = 0;
            }

            update = "update PDK_MATRIZ_DICTAMENSCORIGN set " + campo + "=" + ValorNuevo + ", PDK_MATRIZ_SCORING_MODIF=GETDATE(), PDK_CLAVE_USUARIO=" + usua + " where PDK_ID_MATRIZ_DICSCORING =" + ident;

        } else if (tipotab == 21) {
            switch (etiqueta) {
                case 'lblinf':
                    campo = 'PDK_MATRIZ_BC_LIMINF';
                    break;
                case 'lblsup':
                    campo = 'PDK_MATRIZ_BC_LIMSUP';
                    break;
            }

            if (ValorNuevo == '') {
                ValorNuevo = 0;
            }

            update = "update PDK_MATRIZ_BCSCORE set " + campo + "=" + ValorNuevo + ", PDK_MATRIZ_BC_MODIF=GETDATE(), PDK_CLAVE_USUARIO=" + usua + " where PDK_ID_MATRIZ_BC =" + ident;

        } else if (tipotab == 22) {
            switch (etiqueta) {
                case 'lblinf':
                    campo = 'PDK_MATRIZ_ICC_LIMINF';
                    break;
                case 'lblsup':
                    campo = 'PDK_MATRIZ_ICC_LIMSUP';
                    break;
            }

            if (ValorNuevo == '') {
                ValorNuevo = 0;
            }

            update = "update PDK_MATRIZ_ICC set " + campo + "=" + ValorNuevo + ", PDK_MATRIZ_ICC_MODIF=GETDATE(), PDK_CLAVE_USUARIO=" + usua + " where PDK_ID_MATRIZ_ICC =" + ident;


        } else if (tipotab == 23) {

            switch (etiqueta) {
                case 'lblinf':
                    campo = 'PDK_MATRIZ_DICTACAPPAGO_LIMINF';
                    break;
                case 'lblsup':
                    campo = 'PDK_MATRIZ_DICTACAPPAGO_LIMSUP';
                    break;
            }

            if (ValorNuevo == '') {
                ValorNuevo = 0;
            }

            update = "update PDK_MATRIZ_DICTAMENCAPPAGO set " + campo + "=" + ValorNuevo + ", PDK_MATRIZ_DICTACAPPAGO_MADIF=GETDATE(), PDK_CLAVE_USUARIO=" + usua + " where PDK_ID_MATRIZ_DICTACAPPAGO =" + ident;

        } else if (tipotab == 28) {

            switch (etiqueta) {
                case 'lblpo_':
                    campo = 'PDK_VIVIENDA_PUNTOPORCENTAJE';
                    
                    break;
       
            }

            if (ValorNuevo == '') {
                ValorNuevo = 0;
            }

            update = "update PDK_MATRIZ_TIPOVIVIENDA set " + campo + "=" + "." + ValorNuevo + "," + " PDK_VIVIENDA_PORCENTAJE=" + ValorNuevo + ", PDK_VIVIENDA_MODIF=GETDATE(), PDK_CLAVE_USUARIO=" + usua + " where PDK_ID_MATRIZ_TIPOVIVIENDA =" + ident;
          
        }

        btnInsUpd(update);

        fillgv('grvMatriz', 'ddlmatriz');

    }


    $(document).ready(function () {
        fillddl('matriz', '');        
    });




</script>

<div class="divPantConsul">
  <div class="divFiltrosConsul">
    <table class="tabFiltrosConsul">
      <tr class ="tituloConsul">
        <td colspan="2" style="width:70%">Matriz de Decision</td>
      </tr> 
    </table> 
  </div> 
  <div class="divCuerpoConsul" >
  <table class="resulGridsh" width="100%">
   <tr>
     <td class="campos">Tipo de Matriz:</td>
     <td><asp:DropDownList ID="ddlmatriz" CssClass="Text" Width="150px" onChange="fillgv('grvMatriz','ddlmatriz');"  runat="server"></asp:DropDownList> </td>
     <td></td><td></td>
   </tr> 
   <tr>
   <td colspan="6">
   <table id="grvMatriz" class="resulGrid">
   
     
   </table>
   </td>
   </tr> 
  
  
  </table> 
  </div> 
</div>  
<asp:HiddenField runat="server" ID="hdnIdRegistro" />
</asp:Content> 

