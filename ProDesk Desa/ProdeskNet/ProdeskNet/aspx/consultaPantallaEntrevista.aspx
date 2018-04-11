<%@ Page Language="vb" MasterPageFile="~/aspx/Home.Master"   AutoEventWireup="false" CodeFile="consultaPantallaEntrevista.aspx.vb" Inherits="consultaPantallaEntrevista" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace ="AjaxControlToolkit" TagPrefix ="cc1"  %>--%>
<%@ MasterType VirtualPath ="~/aspx/Home.Master"   %>

<asp:Content ID ="content" ContentPlaceHolderID ="cphPantallas" runat="server"  >
<!--  YAM-P-208  egonzalez 25/08/2015 Se agregó una función para concatenar en el título de la pantalla el número de solicitud, así como el nombre del cliente -->
<!--  YAM-P-208  egonzalez 26/08/2015 Se agregó un campo oculto el cuál servirá para determinar el porcentaje mínimo de aciertos en la encuesta -->
<!--  BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar-->
<!--  BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit-->
<script language="javascript" type ="text/javascript" >

    function btnGuardar(id) {
        var boton = id.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
        var f = $('[id$=hdnFolio]').val();
        var u = $('[id$=hdnIdUsuario]').val();
        var cadena = '';
        var cadenaUp = '';
        var pantalla = $('[id$=hdnNomPantalla]').val();


        if (boton == "btnGuardarAutoriza") {
            var txtUsu = $('[id$=txtUsuario]').val();
            var txtpsswor = $('[id$=txtPassw]').val();
            var idpantalla = $("[id$=HdnPantalla] ").val();
            var txtmotivoOb = $("[id$=txtmotivo]").val();
            //           var cadenastored = 'EXEC sp_ValidacionUsuario ' + '"' + cadenatodo + '",' + idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1'
            if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
            if (txtpsswor == '') { alert('El campo del passwird esta vacía'); return; }
            if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

            btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 46, 1, txtmotivoOb)
        } else if (boton == "btnGuardarCancelar") {
            var txtUsu = $('[id$=txtusua]').val();
            var txtpsswor = $('[id$=txtpass]').val();
            var idpantalla = $("[id$=HdnPantalla] ").val();
            var txtmotivoOb = $("[id$=TxtmotivoCan]").val();
            $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())
            if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
            if (txtpsswor == '') { alert('El campo del passwird esta vacía'); return; }
            if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }
            btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)

        }
    }

    function chkS(check,checkno,cve,folio,usu,text) {

        if (check.checked == true) {
            $(checkno).attr('checked', false);
            var status = 66;
            var tex = $(text).val();
            var texto = "'" + tex + "'";
            btnEntrevista('IF NOT EXISTS (SELECT * FROM PDK_REPORTE_ENTREVISTA WHERE PDK_ID_DATENTREVISTA=' + cve + ' AND PDK_ID_SECCCERO =' + folio + ') BEGIN INSERT INTO PDK_REPORTE_ENTREVISTA(PDK_ID_DATENTREVISTA,PDK_ID_SECCCERO,PDK_REPORT_DAT_CORRECTOS,PDK_REPORT_OBSERVACION,PDK_CLAVE_USUARIO) VALUES(' + cve + ',' + folio + ',' + status + ',' + texto + ',' + usu + ') END ELSE BEGIN   UPDATE PDK_REPORTE_ENTREVISTA SET PDK_REPORT_DAT_CORRECTOS=' + status + ',PDK_REPORT_OBSERVACION=' + texto + ',PDK_CLAVE_USUARIO=' + usu + ' WHERE PDK_ID_DATENTREVISTA=' + cve + ' AND PDK_ID_SECCCERO =' + folio + ' END')              
         }
               

    }

    function chkN(ckeck1, checksi,cve,folio,usu,text) {

        if (ckeck1.checked == true) {
            $(checksi).attr('checked', false);
            var status = 67;
            var tex = $(text).val();
            var texto = "'" + tex + "'";
            btnEntrevista('IF NOT EXISTS (SELECT * FROM PDK_REPORTE_ENTREVISTA WHERE PDK_ID_DATENTREVISTA=' + cve + ' AND PDK_ID_SECCCERO =' + folio + ') BEGIN INSERT INTO PDK_REPORTE_ENTREVISTA(PDK_ID_DATENTREVISTA,PDK_ID_SECCCERO,PDK_REPORT_DAT_CORRECTOS,PDK_REPORT_OBSERVACION,PDK_CLAVE_USUARIO) VALUES(' + cve + ',' + folio + ',' + status + ',' + texto + ',' + usu + ') END ELSE BEGIN   UPDATE PDK_REPORTE_ENTREVISTA SET PDK_REPORT_DAT_CORRECTOS=' + status + ',PDK_REPORT_OBSERVACION=' + texto + ',PDK_CLAVE_USUARIO=' + usu + ' WHERE PDK_ID_DATENTREVISTA=' + cve + ' AND PDK_ID_SECCCERO =' + folio + ' END')
                     
          }

    }

    function escondeAgregar() {
        //        dvPrin = $('#dvRecCot');
        //        dvSol = $('#divModificar');
        //        ocultaVentanaFast(dvPrin, dvSol);

        dvSol.hide("puff", options, 10, '');
    }
    function funMostraCap() {
        dvSol = $('#divModificar');
        dvSol.draggable();
        dvSol.show('slide', options, 1000, '');


    }
    function bntGuardarEntre(id, cve, folio, checksi, ckeckno,usu) {
        var si=0;

        var c = $(id).val();
        var texto = "'" + c + "'";
        


        if ((checksi.checked == false) && (ckeckno.checked == false)) { alert('Debes selecionar una opción'); return; }

        if (checksi.checked == true) { si = 66; } else if (ckeckno.checked == true) { si = 67 }


        btnEntrevista('IF NOT EXISTS (SELECT * FROM PDK_REPORTE_ENTREVISTA WHERE PDK_ID_DATENTREVISTA=' + cve + ' AND PDK_ID_SECCCERO =' + folio + ') BEGIN INSERT INTO PDK_REPORTE_ENTREVISTA(PDK_ID_DATENTREVISTA,PDK_ID_SECCCERO,PDK_REPORT_DAT_CORRECTOS,PDK_REPORT_OBSERVACION,PDK_CLAVE_USUARIO) VALUES(' + cve + ',' + folio + ',' + si + ',' + texto + ',' + usu + ') END ELSE BEGIN   UPDATE PDK_REPORTE_ENTREVISTA SET PDK_REPORT_DAT_CORRECTOS=' + si + ',PDK_REPORT_OBSERVACION=' + texto + ',PDK_CLAVE_USUARIO=' + usu + ' WHERE PDK_ID_DATENTREVISTA=' + cve + ' AND PDK_ID_SECCCERO =' + folio + ' END')
//        fillgv('TabEntrevista', 'HdnPantalla,hdnFolio,hdnIdUsuario');


    }

    function bntGuardarObser(id, cve, folio, usu) {
        var c = $(id).val();
        var texto = "'" + c + "'";

        btnEntrevista('IF NOT EXISTS (SELECT * FROM PDK_REPORTE_ENTREVISTA WHERE PDK_ID_DATENTREVISTA=' + cve + ' AND PDK_ID_SECCCERO =' + folio + ') BEGIN INSERT INTO PDK_REPORTE_ENTREVISTA(PDK_ID_DATENTREVISTA,PDK_ID_SECCCERO,PDK_REPORT_DAT_CORRECTOS,PDK_REPORT_OBSERVACION,PDK_CLAVE_USUARIO) VALUES(' + cve + ',' + folio + ',0,' + texto + ',' + usu + ') END ELSE BEGIN   UPDATE PDK_REPORTE_ENTREVISTA SET PDK_REPORT_OBSERVACION=' + texto + ',PDK_CLAVE_USUARIO=' + usu + ' WHERE PDK_ID_DATENTREVISTA=' + cve + ' AND PDK_ID_SECCCERO =' + folio + ' END')

    }

    function buscarEntre() {
        fillgv('TabEntrevista', 'HdnPantalla,hdnFolio,hdnIdUsuario,hdHabilitar');
    }
    function btnGurdarProceso(id) {
        /* Validación mínimo porcentaje requerido se saca de la columna 'PDK_PAR_SIS_VALOR_NUMERO' en la tabla 'PDK_PARAMETROS_SISTEMA' */
        if ($("[id$=hdnValPorcentaje] ").val() != undefined && $("[id$=hdnValPorcentaje] ").val() > 0) {
            var pTotal = $('input[type="checkbox"][id^="chksi"]').length * ($("[id$=hdnValPorcentaje] ").val() / 100);
            var pCorrectos = ($('input[type="checkbox"][id^="chksi"]:checked').length * 100) / $('input[type="checkbox"][id^="chksi"]').length;
            if ($('input[type="checkbox"][id^="chksi"]:checked').length < pTotal) {
                alert('El porcentaje de datos correctos es: ' + pCorrectos.toFixed(1) + '% se necesita un mínimo del ' + $("[id$=hdnValPorcentaje] ").val() + '% para continuar.');
                $('#ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1').attr('disabled',false);
                return false;
            }
        }

       var folio=$('[id$=hdnFolio]').val();
       var usu = $('[id$=hdnIdUsuario]').val();
       var pantalla = $("[id$=HdnPantalla]").val()
       btnManejoMensaje('exec sp_validarEntrevista ' + folio + ',1,' + pantalla, 'exec spValNegocio ' + folio + ',64,' + usu)
   }
   function mostrarDiv() {
       div = document.getElementById('divautoriza');
       div.style.display = '';

   }

   function mostrarCanceDiv() {
       div = document.getElementById('divcancela');
       div.style.display = '';

   }

   function funtValidaPag() {
       var usu = $('[id$=inpUsuario]').val();
       var pass = $('[id$=inpuPasswor]').val();
       var cap = $('[id$=intCapasidad]').val();

//       btnManejoMensaje('exec sp_validarUsuEntravisa'+ usu +','+ pass+ ',' + cap)
   
   }

    $(document).ready(function () {
        var folio = $("[id$=hdnFolio]").val()
        var pantalla=$("[id$=HdnPantalla]").val()
        fillgv('TabEntrevista', 'HdnPantalla,hdnFolio,hdnIdUsuario,hdHabilitar');       
    });  


</script>

<div class="divPantConsul">
  <div class="divFiltrosConsul">
    <table class="tabFiltrosConsul">
      <tr class="tituloConsul">
       <td width="100%">
         <table width="100%">
           <tr>
             <td>
               <asp:Label id = "lblNomPantalla" runat = "server"></asp:Label>                                                                       
             </td>
             <td>
               <asp:Label id = "lblIdPantalla" runat = "server" CssClass = "oculta"></asp:Label>
                
             </td> 
             <td style = "width:30%; text-align:right;  ">
                    <input type="button" id="txtCambioCot" runat="server"   value="Modificar" onclick ="funMostraCap();" />
                </td>
                   
           
           </tr>
         
         </table>
       
       </td>
      </tr>  
    </table> 
  
  </div>  
  <div class="divCuerpoConsul">
    <table id="TabEntrevista" width="100%"  class ="resulGrid"> 
      
    </table>

  </div> 
  <div id ="divautoriza" style="display:none">
               <%--<cc1:ModalPopupExtender id="mpoAutorizar"  runat="server"  PopupControlID ="popAutoriza" TargetControlID="btnAutorizar"  CancelControlID ="btnCancelAutoriza" BackgroundCssClass ="modalBackground"></cc1:ModalPopupExtender>--%>
               <asp:Panel ID="popAutoriza" runat ="server" Text="Autorización" CssClass ="cajadialogo">
                 <div class="tituloConsul"  >
                    <asp:Label ID="Label4" runat="server" Text="Autorización"/>
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
            <div id="divcancela" style="display:none" >
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
            <div id="divModificar" class="dvAgregar" >
               <table width = "100%">
                 <tr>
                    <th style = "text-align: right">
                        <label id = "lblX" onclick = "escondeAgregar()" class = "cssCerrar">X</label>
                    </th>
                 </tr>
               </table>
               <table width="100%">
                 <tr>
                    <th colspan = "2" style = "font-size:15px" >
                        Modificar Capacidad de Pago.
                    </th>
                 </tr>
                 <tr>
                   <td class="cssLetras" >Usuario: </td>
                   <td><asp:TextBox ID="inpUsuario" SkinID ="txtGeneral" MaxLength="12"  runat="server" ></asp:TextBox> </td>
                 </tr>
                 <tr>
                   <td class="cssLetras">Password:</td>
                   <td><asp:TextBox ID="inpuPasswor" runat="server" SkinID="txtGeneral" MaxLength ="12" TextMode="Password" EnableTheming ="true"   ></asp:TextBox>  </td>  
                 </tr>
                 <tr>
                   <td class="cssLetras">Capacidad:     </td>
                   <td><input type = "text" id = "intCapasidad" style = "width:45%" onkeypress="ManejaCar('D',0,this.value,this)"/></td>
                 </tr>
                 <tr>
                   <td colspan="2"  align="center" valign ="middle" >
                     <input id="cmbCambioPag" type="button" value="Actualizar" class="Text" onclick="funtValidaPag()" />     
                   </td>
                 </tr>
               </table> 
            
            </div>



</div>  
<div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td align="right" valign="middle">      
                        <asp:Button runat="server" ID="btnRegresar" text="Regresar"   SkinID="btnGeneral" />
                        <input id="cmbguardar1"   runat="server"  type ="button" value="Procesar" class="Text" onclick="$('#ctl00_ctl00_cphCuerpo_cphPantallas_cmbguardar1').attr('disabled', 'disabled');btnGurdarProceso(id)" />   
                        <%--<asp:Button runat="server" ID="btnGuardar" text="Procesar" SkinID="btnGeneral" />--%>   
                        <asp:Button runat="server" ID ="btnAutorizar" Text ="Autorizar" Visible="false"    SkinID ="btnGeneral" />
                        <asp:Button runat="server" ID ="btnCancelar" OnClientClick="mostrarCanceDiv()" Text="Cancelar" SkinID ="btnGeneral" />
                         <asp:Button runat="server" ID="btnImprimir" Text="Imprimir"    SkinID="btnGeneral"  /> 
                                                                                 
                     </td>
                </tr>
            </table>
</div>


<asp:HiddenField ID="hdnFolio" runat ="server" />
<asp:HiddenField ID="HdnPantalla" runat ="server" />
<asp:HiddenField ID="hdnResultado" runat ="server" />
<asp:HiddenField ID="hdnIdUsuario" runat="server" />
<asp:HiddenField ID="hdnNomPantalla" runat="server" />
<asp:HiddenField ID="hdRutaEntrada" runat="server" /> 
<asp:HiddenField ID="hdHabilitar" runat="server" />

<asp:HiddenField ID="hdnValPorcentaje" runat="server" />

</asp:Content>