<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/MasterPageVacia.master" AutoEventWireup="false" CodeFile="caratulaFacturacion.aspx.vb" Inherits="aspx_caratulaFacturacion" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/MasterPageVacia.Master" %>
<%--tracker--%>
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
<asp:Content ID="content1" ContentPlaceHolderID="cphPantallas" runat="server">

<script type="text/javascript" language="javascript" >

    $(document).ready(function () {
        fnCargaTablasInicio();
    })

    function fnActualizar(Tabla, solicitud, idFactura) {
        btnInsUpdMultiTab("exec SP_BorraTablaFacturas '" + Tabla + "'," + solicitud + ",  '" + idFactura + "'; ", "#tbFacturaVehiculo, #tbFacturaAccesorios, #tbFacturaIntangibles, #tbFacturaComer", "hdnIdFolio", "SumaMontos", "");
    }

    function fnAgregar(id) {
        var depende = '';
        var salida = 0;
        switch (id) {
            case 'btnAgregar':
                factV = $(".vehiculo");
                tabla = 'tbFacturaVehiculo';
                break;
            case 'btnAgregarAccesorios':
                factV = $(".accesorio");
                tabla = "tbFacturaAccesorios";
                break;
            case 'btnAgregarInt':
                factV = $(".intangible");
                tabla = "tbFacturaIntangibles";
                break;
            case 'btnAgregarComer':
                factV = $(".comer");
                tabla = "tbFacturaComer";
                break;
        }

        $.each(factV, function () {
            if ($(this).val() == "") {
                alert("Falta infromación en el campo " + $(this)[0].id.replace("txt", ""));
                salida = 1;
                return;
            }
            else {
                depende += $(this)[0].id + ',';
            }
        })

        //var n = str.indexOf("e", factV.length());
        //depende = depende.substring(0, n) + ')';

        depende = depende.replace(/,([^,]*)$/, '$1');

        if (salida == 0) {
            //fillgv(tabla, 'hdnIdFolio, ' + depende);
            fillUpload(tabla, 'hdnIdFolio,' + depende, 'SumaMontos', '');
            factV.val("");
        }
    }

    function fnCargaTablasInicio() {

        var dependeV = '';
        var dependeA = '';
        var dependeI = '';
        var dependeC = '';

        factV = $(".vehiculo");
        factA = $(".accesorio");
        factI = $(".intangible");
        factC = $(".comer");

        $.each(factV, function () {
            dependeV += $(this)[0].id + ', ';
        })
        $.each(factA, function () {
            dependeA += $(this)[0].id + ', ';
        })
        $.each(factI, function () {
            dependeI += $(this)[0].id + ', ';
        })
        $.each(factC, function () {
            dependeC += $(this)[0].id + ', ';
        })

        fillMultiTab("#tbFacturaVehiculo, #tbFacturaAccesorios, #tbFacturaIntangibles, #tbFacturaComer", "hdnIdFolio");

    }

    function fnFactura(valor) {
        $('[id^=txtFactura]').val(valor);
    }

    function mostrarDiv() {
        div = document.getElementById('divautoriza');
        div.style.display = '';

    }

    function mostrarCanceDiv() {
        div = document.getElementById('divcancela');
        div.style.display = '';
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

    $(window).bind("load", function () {
        if ($('[id$=statusTarea]').val() != "ACTIVA") {
            $('[type="button"]').remove();
            setTimeout(function () {
                $('.link').remove();
            }, 100);
        }
    }); // Función que espera la carga completa del documento incluyendo imágenes y Ajax*

</script> 

<div class="divAdminCat ">
    <div class ="divFiltrosConsul">
       <table class="tabFiltrosConsul">
            <tr class="tituloConsul">
                <td width="100%">
                    <table width="100%">
                        <tr>
                            <td colspan="2" style="width:70%;">Facturacion.</td>                                                                                    
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
     </div>
     <div class="divAdminCatCuerpo ">
        <div style = "width:100%; height:100%; overflow:auto;">
            <table width = "100%" class = "cssEncabezado">
            <tr>
                <th  style = "width:25%">
                    Solicitud:                    
                </th>
                <th    style = "width:25%">
                    <asp:label id = "lblSolicitud" Font-Underline = "true" runat = "server">
                    </asp:label>
                </th>
                <th   style = "width:25%">
                    Cliente:                    
                </th>
                <th   style = "width:25%">
                    <asp:label id = "lblCliente" Font-Underline = "true" runat = "server">
                    </asp:label>
                </th>
            </tr>    
            <tr>
                <th   >
                    Status Documentos:                    
                </th>
                <th   >
                    <asp:label id = "lblStDocumento" Font-Underline = "true" runat = "server">
                    </asp:label>
                </th>
                <th   >
                    Status Credito:                    
                </th>
                <th   >
                    <asp:label id = "lblStCredito" Font-Underline = "true" runat = "server">
                    </asp:label>
                </th>
            </tr>            
        </table>
        <table width = "98%" id = "tbFacturacion" class = "cssCuerpo">
        <tr>
            <td colspan = "4" style = "font-size:larger">
                Datos de las Facturas.
            </td>
        </tr>       
        <tr>
            <th colspan = "4"   >
                Factura Vehiculo
            </th>
        </tr>
        <tr>
            <td>
                No. Factura
            </td>
            <td>
                <input type = "text" id = "txtFactura" onkeyup = "fnFactura(this.value)"  class = "vehiculo" />
            </td>
            <td   >
                No.Serie
            </td>
            <td   >
                <input type = "text" id = "txtSerie" class = "vehiculo"/>
            </td>
        </tr>
        <tr>
<%--            <td   >
                Linea
            </td>
            <td   >
                <input type = "text" id = "txtLinea" class = "vehiculo"/>
            </td>--%>
            <td   >
                Motor
            </td>
            <td   >
                <input type = "text" id = "txtMotor" class = "vehiculo"/>
            </td>
            <td>
                Importe
            </td>
            <td   >
                <input type = "text" id = "txtImporte" onkeypress="ManejaCar('D',0,this.value,this)"   class = "vehiculo" />
            </td>
        </tr>        
        <tr>
            <td colspan = "4">
                <table id = "tbFacturaVehiculo" class = "resulGrid">
                </table>
            </td>
        </tr>                
        <tr>
            <td colspan = "4" style = "text-align: right;">
                <input type = "button" id = "btnAgregar" value = "Agregar" onclick = "fnAgregar(this.id);" class = "botones" />
            </td>
        </tr>
        <tr>
            <th colspan = "4" >
                Accesorios
            </th>            
        </tr>
        <tr>
            <td>
                No.Factura
            </td>
            <td>
                <input type = "text" id = "txtFacturaAccesorios" class = "accesorio" />
            </td>
            <td colspan = "2" rowspan = "3">
                <table id = "tbFacturaAccesorios"  class = "resulGrid">
                </table>
            </td>            
        </tr>
        <tr>
            <td>
                Descripción
            </td>
            <td>
                <input type = "text" id = "txtDescripcionAccesorios" class = "accesorio" />
            </td>
        </tr>
        <tr>
            <td>
                Importe
            </td>
            <td>
                <input type = "text" id = "txtImporteAccesorios" onkeypress="ManejaCar('D',0,this.value,this)" class = "accesorio"/>
            </td>
        </tr>
        <tr>
            <td colspan = "4" style = "text-align:right;">
                <input type = "button" id = "btnAgregarAccesorios" value = "Agregar" onclick = "fnAgregar(this.id);" class = "botones" />
            </td>
        </tr>
        <tr>
            <th    colspan = "4">
                Factura Intangibles(Garantia Extendida, Servicios)
            </th>
        </tr>
        <tr>
            <td>
                No.Factura
            </td>
            <td>
                <input type = "text" id = "txtFacturaInt" class = "intangible" />
            </td>            
            <td colspan = "2" rowspan = "3">
                <table id = "tbFacturaIntangibles"  class = "resulGrid">
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Descripción
            </td>
            <td>
                <input type = "text" id = "txtDescripcionInt" class = "intangible" />
            </td>
        </tr>
        <tr>
            <td>
                Importe
            </td>
            <td>
                <input type = "text" id = "ImporteInt" onkeypress="ManejaCar('D',0,this.value,this)" class = "intangible" />
            </td>
        </tr>
        <tr>
            <td colspan = "4" style = "text-align:right;">
                <input type = "button" id = "btnAgregarInt"  value = "Agregar" onclick = "fnAgregar(this.id);" class = "botones"/>
            </td>
        </tr>
        <tr>
            <th colspan = "4">
                Factura Apoyo a la Comercialización
            </th>
        </tr>
        <tr>
            <td>
                No.Factura
            </td>
            <td>
                <input type = "text" id = "txtFacturaComer" class = "comer"/>
            </td>
            <td colspan = "2" rowspan = "3">
                <table id = "tbFacturaComer" class = "resulGrid">
                </table>
            </td>            
        </tr>
        <tr>
            <td>
                Descripción
            </td>
            <td>
                <input type = "text" id = "txtDescComer" class = "comer"/>
            </td>
        </tr>
        <tr>
            <td>
                Importe
            </td>
            <td>
                <input type = "text" id = "txtImporteComer" onkeypress="ManejaCar('D',0,this.value,this)" class = "comer"/>
            </td>
        </tr>                
        <tr>
            <td colspan = "4" style = "text-align:right;">
                <input type = "button" id = "btnAgregarComer"  value = "Agregar" onclick = "fnAgregar(this.id);" class = "botones"/>
            </td>
        </tr>
        <tr>
            <th colspan = "4"   >
                Importe Suma de Facturas <input type = "text" id = "txtSumaFacturas" runat = "server" />
            </th>
        </tr>
<%--        <tr>
            <td    colspan = "4" style = "text-align:right;">
                <input type = "button" value = "Guardar" id = "btnGuardarFac" onclick = "fnProcesar('')" class = "botones" />
            </td>
        </tr> --%>       
    </table>
        </div>        
     </div>

     <div id ="divautoriza" style="display:none">
               <%--<cc1:ModalPopupExtender id="mpoAutorizar"  runat="server"  PopupControlID ="popAutoriza" TargetControlID="btnAutorizar"  CancelControlID ="btnCancelAutoriza" BackgroundCssClass ="modalBackground"></cc1:ModalPopupExtender>--%>
               <asp:Panel ID="popAutoriza" runat ="server" Text="Autorización" CssClass ="cajadialogo">
                 <div class="tituloConsul"  >
                    <asp:Label ID="Label4" runat="server" Text="Autorización" />
                 </div>
                 <table width="100%">
                    <tr>
                      <td class="campos" >Usuario:</td>
                      <td><asp:TextBox ID="txtUsuario" SkinID ="txtGeneral" MaxLength="12"  runat="server" ></asp:TextBox> </td>
                    </tr>
                    <tr>
                      <td class="campos"  >Password:</td>
                      <td><asp:TextBox ID="txtPassw" runat="server" SkinID="txtGeneral" MaxLength ="12" TextMode="Password" EnableTheming ="true"   ></asp:TextBox> </td>
                    </tr>
                    <tr>
                     <td colspan="2" class="campos" >Descripción:</td>
                    </tr>
                    <tr>
                     <td colspan="2"><textarea id="txtmotivo" runat="server" onkeypress="ManejaCar('A',1,this.value,this)" class = "Text" rows = "5" cols = "1" style = "width:100%"></textarea> </td>
                    </tr>
                    <tr style="width:100%">
                     <td><asp:HiddenField runat="server" ID="hdnidAutoriza" /></td>
                     <td align="left" valign="middle">
                      <input id="btnGuardarAutoriza" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar(id)"  /> 
                      <asp:Button ID="btnCancelAutoriza" runat="server" Text="Cancelar" SkinID="btnGeneral"/>              
                     </td>
                    </tr>
                 </table>                
               </asp:Panel> 
            </div>
            <div id="divcancela" style="display:none">
              <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID ="btnCancelar" PopupControlID="popCancela" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground" ></cc1:ModalPopupExtender>  --%>
              <asp:Panel ID="popCancela" runat="server" CssClass ="cajadialogo ">
               <div class ="tituloConsul">
                  <asp:Label ID="Label1" runat="server" Text="Cancelación" />
               </div>
               <table width="100%" >
                 <tr>
                   <td class="campos" >Usuario:</td>
                   <td><asp:TextBox ID="txtusua" SkinID ="txtGeneral" MaxLength="12"  runat="server" ></asp:TextBox> </td>
                 </tr>
                 <tr>
                   <td class="campos"  >Password:</td>
                    <td><asp:TextBox ID="txtpass" runat="server" SkinID="txtGeneral" MaxLength ="12" TextMode="Password" EnableTheming ="true"   ></asp:TextBox> </td>
                 </tr>
                 <tr>
                   <td colspan="2" class="campos" >Descripción:</td>
                 </tr>
                 <tr>
                  <td colspan="2"><textarea id="TxtmotivoCan" runat="server" onkeypress="ManejaCar('A',1,this.value,this)" class = "Text" rows = "5" cols = "1" style = "width:100%"></textarea> </td>
                 </tr>
                 <tr style="width:100%">
                   <td><asp:HiddenField runat="server" ID="HiddenField1" /></td>
                   <td align="left" valign="middle">
                     <input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar(id)"  /> 
                     <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral"/>              
                   </td>
                 </tr>
               
               </table> 
              
              </asp:Panel>            
            </div>
   <div class="divAdminCatPie">
    <table width="100%" style="height:100%;">
      <tr>
        <td align="right" valign="middle">                 
           <%--<asp:Button runat="server" ID="btnRegresar" text="Regresar" SkinID="btnGeneral" />--%>
           <%--<input type="button" class="Text" value="Regresar" onclick="fnRegresar();"  />--%>    
           <%--<input id="btnGuardarFac" runat="server" type = "button" value = "Procesar" onclick = "if ($('table#tbFacturaVehiculo tbody').find('tr').length > 1) { fnProcesar(''); } else { alert('Es necesario agregar una factura para continuar'); }"/>--%>
           <asp:Button runat="server" ID ="btnAutorizar" Visible="false" Text ="Autorizar"/>
           <%--<asp:Button runat="server" ID ="btnCancelar" OnClientClick="mostrarCanceDiv()" Text="Cancelar"/>--%>                                                                                                                                       
        </td>
      </tr>
    </table>
  </div> 



 </div>

 <asp:HiddenField ID = "hdnIdFolio" runat="server" />
<asp:HiddenField ID = "hdnIdPantalla" runat="server" />
<asp:HiddenField ID = "hdnUsua" runat="server" />
<asp:HiddenField ID = "hdnResultado" runat="server" />
<asp:HiddenField ID = "hdRutaEntrada" runat="server" />
<asp:HiddenField ID = "hdNomPantalla" runat="server" /> 
<asp:HiddenField ID = "hdnEnable" runat="server" /> 
<asp:HiddenField ID = "hdnMensualidad" runat = "server" />
<asp:HiddenField ID = "hdnPlazo" runat = "server" />
<asp:HiddenField ID="hdnResultado1" runat="server" />
<asp:HiddenField ID="hdnResultado2" runat ="server" />
<asp:HiddenField ID="statusTarea" runat ="server" />

</asp:Content>