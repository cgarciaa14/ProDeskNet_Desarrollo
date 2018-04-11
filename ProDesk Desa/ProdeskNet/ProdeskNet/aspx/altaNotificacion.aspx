<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="altaNotificacion.aspx.vb" Inherits="altaNotificacion" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %> 

<%--Tracker:INC-B-2019:JDRA:Regresar--%>
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="content1" ContentPlaceHolderID ="cphPantallas" runat="server" >
<script language="javascript" type="text/javascript" >

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

    function mostrarDiv() {     
       div = document.getElementById('divautoriza'); 
       div.style.display ='';

   }


   function mostrarCanceDiv() {
       div = document.getElementById('divcancela');
       div.style.display = '';
   }

    function btnguardarObse(id) {
        var observa = $(id).val();
        var texto = "'" + observa + "'";
        var folio = $('[id$=hdnIdFolio]').val();
        var usuario = $('[id$=hdnUsua]').val();
        var valor1 = $("#ddlcalifica  option:selected").val();
        if (valor1 == 72) { var valor = $("#ddlcondic option:selected").val(); } else {var valor = 0; }

        //        btnInsUpd('IF NOT EXISTS (SELECT * FROM PDK_RESULTADO_NOTIFICA WHERE PDK_ID_SECCCERO=' + folio + ' ) BEGIN   INSERT INTO PDK_RESULTADO_NOTIFICA(PDK_ID_SECCCERO,PDK_RESULNOTI_CALIFICACION,PDK_RESULNOTI_CONDICIONA,PDK_RESULNOTI_OBSERVACION,PDK_CLAVE_USUARIO) VALUES(' + folio + ',' + valor1 + ',' + valor + ',' + texto + ',' + usuario + ') END ELSE BEGIN    UPDATE PDK_RESULTADO_NOTIFICA SET PDK_RESULNOTI_CALIFICACION=' + valor1 + ',PDK_RESULNOTI_CONDICIONA=' + valor + ',PDK_RESULNOTI_OBSERVACION=' + texto + ',PDK_CLAVE_USUARIO=' + usuario + ' WHERE PDK_ID_SECCCERO=' + folio + '  END')
        btnInsUpd('IF NOT EXISTS (SELECT * FROM PDK_RESULTADO_NOTIFICA WHERE PDK_ID_SECCCERO=' + folio + ' ) BEGIN   INSERT INTO PDK_RESULTADO_NOTIFICA(PDK_ID_SECCCERO,PDK_RESULNOTI_CALIFICACION,PDK_RESULNOTI_CONDICIONA,PDK_RESULNOTI_OBSERVACION,PDK_CLAVE_USUARIO,PDK_RESULACEPTADO,PDK_RESULRECHAZADO,PDK_RESULCOMITE,PDK_RESULCOMACEP,PDK_RESULCOMRECH) VALUES(' + folio + ',' + 72 + ',' + 0 + ',' + texto + ',' + usuario + ',0,0,0,0,0) END ELSE BEGIN    UPDATE PDK_RESULTADO_NOTIFICA SET PDK_RESULNOTI_OBSERVACION=' + texto + ',PDK_CLAVE_USUARIO=' + usuario + ' WHERE PDK_ID_SECCCERO=' + folio + '  END')

    }

    function Activiar(id) {
//        var indice = id.selectedIndex;
        //        var valor = id.options[indice].value;
       
       var indice= $("#" + id.id + " option:selected").text();
       var valor = $("#" + id.id + " option:selected").val();
       if (valor == 72) {
           $('[id$=ddlcondic]').attr('style', 'display:block');
           $('[id$=lblcondi]').attr('style', 'display:block');
       } else {
           $('[id$=ddlcondic]').attr('style', 'display:none'); $('[id$=lblcondi]').attr('style', 'display:none');
           var valor1 = 0;
           var observa = $('[id$=txtObserva]').val();
           var texto = "'" + observa + "'";
           var folio = $('[id$=hdnIdFolio]').val();
           var usuario = $('[id$=hdnUsua]').val();

           btnInsUpd('IF NOT EXISTS (SELECT * FROM PDK_RESULTADO_NOTIFICA WHERE PDK_ID_SECCCERO=' + folio + ' ) BEGIN   INSERT INTO PDK_RESULTADO_NOTIFICA(PDK_ID_SECCCERO,PDK_RESULNOTI_CALIFICACION,PDK_RESULNOTI_CONDICIONA,PDK_RESULNOTI_OBSERVACION,PDK_CLAVE_USUARIO) VALUES(' + folio + ',' + valor + ',' + valor1 + ',' + texto + ',' + usuario + ') END ELSE BEGIN    UPDATE PDK_RESULTADO_NOTIFICA SET PDK_RESULNOTI_CALIFICACION=' + valor + ',PDK_RESULNOTI_CONDICIONA=' + valor1 + ',PDK_RESULNOTI_OBSERVACION=' + texto + ',PDK_CLAVE_USUARIO=' + usuario + ' WHERE PDK_ID_SECCCERO=' + folio + '  END')

         }

   }

   function funtValidarchech() {
       var cheAceptar1 = $('#chkAcepAnali')[0]
       var cheRechazado1=$('#chkRezaAnalis')[0]
       var cheComite=$('#chkcomiteAnalista')[0]
       var cheAceptar2=$('#chkAcepComite')[0]
       var cheRecha2 = $('#chkRezaComite')[0]
       var folio = $('[id$=hdnIdFolio]').val();
       var usuario = $('[id$=hdnUsua]').val();
       if ((cheAceptar1.checked == false) && (cheRechazado1.checked == false) && (cheComite.checked == false)) {
           alert('Debes selecionar un Dictamen Analista'); $('#ctl00_ctl00_cphCuerpo_cphPantallas_btnGuardarCom').removeAttr('disabled', ''); return;
       }else if (cheComite.checked==true) {
           if ((cheAceptar2.checked == false) && (cheRecha2.checked == false)) {
               alert('Debes selecionar un Dictamen Comite'); $('#ctl00_ctl00_cphCuerpo_cphPantallas_btnGuardarCom').removeAttr('disabled', '');  return;
          }
       }
       //       btnManejoMensaje('', 'exec spValNegocio ' + folio + ',64,' + usuario)
       btnInsertDocumento('exec spValNegocio ' + folio + ',64,' + usuario)
   }
   function btnGuardarChenk(check, chkRe,chkCom,clave) {
       if (check.checked == true) {
           var chequeado1 = 0
           var chequeado2 = 0
           var chequeado3 = 0
           var observa = $('[id$=txtObserva]').val();
           var texto = "'" + observa + "'";
           var folio = $('[id$=hdnIdFolio]').val();
           var usuario = $('[id$=hdnUsua]').val();

           if (clave == 1) {
               chequeado1 = 1
               btnInsUpd('IF NOT EXISTS (SELECT * FROM PDK_RESULTADO_NOTIFICA WHERE PDK_ID_SECCCERO=' + folio + ' ) BEGIN   INSERT INTO PDK_RESULTADO_NOTIFICA(PDK_ID_SECCCERO,PDK_RESULNOTI_CALIFICACION,PDK_RESULNOTI_CONDICIONA,PDK_RESULNOTI_OBSERVACION,PDK_CLAVE_USUARIO,PDK_RESULACEPTADO,PDK_RESULRECHAZADO,PDK_RESULCOMITE,PDK_RESULCOMACEP,PDK_RESULCOMRECH) VALUES(' + folio + ',' + 72 + ',' + 0 + ',' + texto + ',' + usuario + ',' + chequeado1 + ',' + chequeado2 + ',' + chequeado3 + ',0,0) END ELSE BEGIN    UPDATE PDK_RESULTADO_NOTIFICA SET PDK_RESULACEPTADO=' + chequeado1 + ',PDK_RESULRECHAZADO=' + chequeado2 + ',PDK_RESULCOMITE=' + chequeado3 + ',PDK_RESULNOTI_OBSERVACION=' + texto + ',PDK_CLAVE_USUARIO=' + usuario + ' WHERE PDK_ID_SECCCERO=' + folio + '  END')
           } else if (clave == 2) {
               chequeado2 = 1
               btnInsUpd('IF NOT EXISTS (SELECT * FROM PDK_RESULTADO_NOTIFICA WHERE PDK_ID_SECCCERO=' + folio + ' ) BEGIN   INSERT INTO PDK_RESULTADO_NOTIFICA(PDK_ID_SECCCERO,PDK_RESULNOTI_CALIFICACION,PDK_RESULNOTI_CONDICIONA,PDK_RESULNOTI_OBSERVACION,PDK_CLAVE_USUARIO,PDK_RESULACEPTADO,PDK_RESULRECHAZADO,PDK_RESULCOMITE,PDK_RESULCOMACEP,PDK_RESULCOMRECH) VALUES(' + folio + ',' + 72 + ',' + 0 + ',' + texto + ',' + usuario + ',' + chequeado1 + ',' + chequeado2 + ',' + chequeado3 + ',0,0) END ELSE BEGIN    UPDATE PDK_RESULTADO_NOTIFICA SET PDK_RESULACEPTADO=' + chequeado1 + ',PDK_RESULRECHAZADO=' + chequeado2 + ',PDK_RESULCOMITE=' + chequeado3 + ',PDK_RESULNOTI_OBSERVACION=' + texto + ',PDK_CLAVE_USUARIO=' + usuario + ' WHERE PDK_ID_SECCCERO=' + folio + '  END')

           } else {
               chequeado3 = 1
               $('[id$=lbldictame]').attr('style', 'display:block');
               $('[id$=chkAcepComite]').attr('style', 'display:block');
               $('[id$=chkRezaComite]').attr('style', 'display:block');                
               $(chkRe).attr('disabled', 'disabled');
               $(chkCom).attr('disabled', 'disabled');
               btnInsUpd('IF NOT EXISTS (SELECT * FROM PDK_RESULTADO_NOTIFICA WHERE PDK_ID_SECCCERO=' + folio + ' ) BEGIN   INSERT INTO PDK_RESULTADO_NOTIFICA(PDK_ID_SECCCERO,PDK_RESULNOTI_CALIFICACION,PDK_RESULNOTI_CONDICIONA,PDK_RESULNOTI_OBSERVACION,PDK_CLAVE_USUARIO,PDK_RESULACEPTADO,PDK_RESULRECHAZADO,PDK_RESULCOMITE,PDK_RESULCOMACEP,PDK_RESULCOMRECH) VALUES(' + folio + ',' + 72 + ',' + 0 + ',' + texto + ',' + usuario + ',' + chequeado1 + ',' + chequeado2 + ',' + chequeado3 + ',0,0) END ELSE BEGIN    UPDATE PDK_RESULTADO_NOTIFICA SET PDK_RESULACEPTADO=' + chequeado1 + ',PDK_RESULRECHAZADO=' + chequeado2 + ',PDK_RESULCOMITE=' + chequeado3 + ',PDK_RESULNOTI_OBSERVACION=' + texto + ',PDK_CLAVE_USUARIO=' + usuario + ' WHERE PDK_ID_SECCCERO=' + folio + '  END')

           }

           $(chkRe).attr('checked', false); $(chkCom).attr('checked', false);
       } else {
           $('[id$=lbldictame]').attr('style', 'display:none');
           $('[id$=chkAcepComite]').attr('style', 'display:none');
           $('[id$=chkRezaComite]').attr('style', 'display:none');
           $(chkRe).removeAttr('disabled', 'disabled');
           $(chkCom).removeAttr('disabled', 'disabled');
           $('[id$=chkAcepComite]').attr('checked', false); $('[id$=chkRezaComite]').attr('checked', false);

       
       }


   }

   function btnGuardarChenkComm(check,checkRecha,val) {
       if (check.checked == true) {
           var chequeado = 0
           var chequeado1 = 0
           var observa = $('[id$=txtObserva]').val();
           var texto = "'" + observa + "'";
           var folio = $('[id$=hdnIdFolio]').val();
           var usuario = $('[id$=hdnUsua]').val();
           if (val == 1) { chequeado = 1 } else { chequeado1 }

           btnInsUpd('IF NOT EXISTS (SELECT * FROM PDK_RESULTADO_NOTIFICA WHERE PDK_ID_SECCCERO=' + folio + ' ) BEGIN   INSERT INTO PDK_RESULTADO_NOTIFICA(PDK_ID_SECCCERO,PDK_RESULNOTI_CALIFICACION,PDK_RESULNOTI_CONDICIONA,PDK_RESULNOTI_OBSERVACION,PDK_CLAVE_USUARIO,PDK_RESULACEPTADO,PDK_RESULRECHAZADO,PDK_RESULCOMITE,PDK_RESULCOMACEP,PDK_RESULCOMRECH) VALUES(' + folio + ',' + 72 + ',' + 0 + ',' + texto + ',' + usuario + ',0,0,1,' + chequeado + ',' + chequeado1 + ') END ELSE BEGIN    UPDATE PDK_RESULTADO_NOTIFICA SET PDK_RESULCOMACEP=' + chequeado + ',PDK_RESULCOMRECH=' + chequeado1 + ',PDK_RESULNOTI_OBSERVACION=' + texto + ',PDK_CLAVE_USUARIO=' + usuario + ' WHERE PDK_ID_SECCCERO=' + folio + '  END')
           $(checkRecha).attr('checked', false);

       } 
     
   
   }
   function btnGuardaCondi(combo) {
       var indice = $("#" + combo.id + " option:selected").text();
       var valor = $("#" + combo.id + " option:selected").val();
       var valor1 = $("#ddlcalifica  option:selected").val();
       var observa=$('[id$=txtObserva]').val();
       var texto="'"+ observa +"'";
        var folio = $('[id$=hdnIdFolio]').val();
       var usuario = $('[id$=hdnUsua]').val();
       btnInsUpd('IF NOT EXISTS (SELECT * FROM PDK_RESULTADO_NOTIFICA WHERE PDK_ID_SECCCERO=' + folio + ' ) BEGIN   INSERT INTO PDK_RESULTADO_NOTIFICA(PDK_ID_SECCCERO,PDK_RESULNOTI_CALIFICACION,PDK_RESULNOTI_CONDICIONA,PDK_RESULNOTI_OBSERVACION,PDK_CLAVE_USUARIO) VALUES(' + folio + ',' + valor1 + ',' + valor + ',' + texto + ',' + usuario + ') END ELSE BEGIN    UPDATE PDK_RESULTADO_NOTIFICA SET PDK_RESULNOTI_CALIFICACION=' + valor1 + ',PDK_RESULNOTI_CONDICIONA=' + valor + ',PDK_RESULNOTI_OBSERVACION=' + texto + ',PDK_CLAVE_USUARIO=' + usuario + ' WHERE PDK_ID_SECCCERO=' + folio + '  END')


   }

   function muestraMoral() {
       fillgv('divValorScore', 'hdnIdFolio');

       dvValorScore = $('#divValorScore');
       dvValorScore.draggable();
       dvValorScore.show('slide', options, 1000, '');

   
   }
   function muestraAgregar() {
       dvSol = $('#btnInformativo');
       dvSol.draggable();
       dvSol.show('slide', options, 1000, '');
       $('.divAdminCatPie').hide('fast')
   }

   function escondeAgregar() {
       dvValorScore.hide("puff", options, 10, '');      

   }

   function escoder() {
       dvSol.hide("puff", options, 10, '');
       $('.divAdminCatPie').show('slide')
   }

   $(document).ready(function () {
       $("#tabs").tabs();
       fillgv('tabNotificacion', 'hdnIdFolio,hdnEnable');
       var tipoJudica = $('[id$=hdnIdTipoJuridica]').val();
       if (tipoJudica == 1 || tipoJudica == 2) { $('#lblMoral').hide(); $('#lbliforma').show(); } else if (tipoJudica == 3) { $('#lblMoral').show(); $('#lbliforma').hide(); }


   }); 

</script>  

<div class="divAdminCat">
  <div class="divFiltrosConsul">
    <table class="tabFiltrosConsul">
      <tr class="tituloConsul">
        <td width="100%">
          <table width="100%">
            <tr>
             <td>
               <asp:Label id = "lblNomNotificacion" runat = "server"></asp:Label>                                                                          
             </td>
             <td>
              <asp:Label id = "lblIdNotificacion" runat = "server" CssClass = "oculta"></asp:Label>
             </td>                                
            </tr>                            
          </table>
        </td>
      
      </tr>  
    </table>  
  </div>
  <div class="divAdminCatCuerpo">
    <table id="tabNotificacion" width="100%"  >    
    </table> 
    <table>
      <tr>
        <td><label id="lbliforma"  class="link" onclick="muestraAgregar();">INFORMACION DE LA SOLICITUD</label>  </td>
        
      </tr>
      <tr></tr>
      <tr>
        <td align="right" valign="middle" ><label id="lblMoral" class="link" onclick="muestraMoral();">CLIC LA CALIFICACION DE SCORING </label> </td>
      </tr>
    </table> 

  </div> 
  <div id="btnInformativo" class="dvInformar">
   <table width = "100%">
     <tr>
       <th style = "text-align: right">
           <label id = "lblX" onclick = "escoder();" class = "link">  X  </label>
        </th>
     </tr>
   </table>
   <div id="tabs">
          <ul>
            <li><a href ="#tabs-1">Cotización</a></li>
            <li><a href ="#tabs-2">Buro</a> </li>
            <li><a href ="#tabs-3">Solicitud</a> </li>
            <li><a href ="#tabs-4">Documentación</a></li>
          </ul>
          <div id="tabs-1">
              <table width="100%">
                 <tr>
                  <td class="campos">Num Cotización: 
                    <asp:Label ID="lblNumCotiza" runat="server" ></asp:Label> </td>
                 </tr>
                 <tr>
                 <td colspan ="7">
                 <table width="100%">
                   <tr>
                   <td>
                     <div style="height:350px; width:100%;  overflow:auto">
                      <asp:GridView ID="grvCaratula" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="encabezados" Width="100%" PagerStyle-HorizontalAlign="Right" CssClass="resulGrid" AllowSorting="True"   >
                      <Columns>
                        <asp:BoundField DataField="NO_PAGO" ItemStyle-CssClass="resul" HeaderText="Num Pago" />
                        <asp:BoundField DataField ="FEC_PAGO" ItemStyle-CssClass ="resul" HeaderText="Fecha Pago" />
                        <asp:BoundField DataField="BASE_CALC" ItemStyle-CssClass="resul" HeaderText="Base de Calculo" />
                        <asp:BoundField DataField="INSOLUTO_PROD" ItemStyle-CssClass="resul" HeaderText ="Insoluto" />  
                        <asp:BoundField DataField="INTERES_PROD" ItemStyle-CssClass="resul" HeaderText ="Interese" />
                        <asp:BoundField DataField="CAPITAL_PROD" ItemStyle-CssClass ="resul" headerText="Capital" />
                        <asp:BoundField DataField="PAGO_PROD" ItemStyle-CssClass="resul" HeaderText ="Pago Total" />         
      
                      </Columns> 
                      </asp:GridView>
                     </div>
                   </td>
                  </tr>
                 </table>  
                 </td>
                 </tr>
              </table>            
          </div>
          <div id="tabs-2">
            <div>
              <table width ="100%">
               <tr>
                <td class="tituloConsul" >Dictamen Precalificación</td>   
               </tr>
              </table> 
            </div>
            <div>
              <table width="100%">
               <tr>
                 <td style="text-align:center" class="campos"> Dictamen Final: 
                 <asp:Label ID="lbldicPre" runat="server"></asp:Label></td>
               </tr>
               <tr>
               <td colspan="4">
                  <table width="100%" style="border-style:ridge; border-color:#808080; border-width:2px;"  >
                   <tr>
                     <td class="campos">Condición 1:</td>
                     <td><asp:Label ID="lblcondi1" runat="server" ></asp:Label></td>
                     <td class="campos" >Condición 2:</td>
                     <td ><asp:Label ID="lblCondi2" runat="server"></asp:Label></td>    
                   </tr> 
                   <tr>
                    <td class="campos">Condición 3:</td>
                    <td><asp:Label ID="lblCondi3" runat="server" ></asp:Label></td>
                    <td class="campos">Condición 4:</td> 
                    <td ><asp:Label ID="lblCondi4" runat="server" ></asp:Label></td>             
                   </tr>
                  </table>        
               </td>               
              </tr>
              <tr>
               <td  class="campos">Resultado Buró de Crédito</td>
              </tr>
              <tr>
               <td colspan="6">
                 <table width="100%" style="border-style:ridge; border-color:#808080; border-width:2px;">
                   <tr>
                     <td style="text-align:center"   colspan="3" class="campos">Solicitante</td>
                     <td colspan="3" style="text-align:center"    class="campos">Coacreditado</td>  
                   </tr>
                   <tr>
                    <td class="campos">BC Score:</td>
                    <td><asp:Label ID="lblbc" runat="server"  Width="30%"></asp:Label></td>
                    <td><asp:ImageButton ID="imagSolicita" ImageUrl="../App_Themes/Imagenes/Logo Buro.png"  runat="server" /></td> 
                    <td class="campos">BC Score:</td>
                    <td><asp:Label ID="lblbc_coa" runat="server"  Width="30%"></asp:Label></td> 
                    <td><asp:ImageButton ID="imgCoacre" ImageUrl="../App_Themes/Imagenes/Logo Buro.png"  runat="server"   /></td>  
                   </tr>
                   <tr>
                    <td class="campos">ICC:</td> 
                    <td><asp:Label ID="lblicc" runat="server"  Width="30%"></asp:Label></td>
                    <td></td>
                    <td class="campos">ICC:</td>
                    <td><asp:Label ID="lblicc_coa" runat="server"  Width="30%"></asp:Label></td> 
                    <td></td> 
                   </tr>
                 </table>  
               </td>
               </tr> 
               <tr>
                 <td class="campos">Resultado Capacidad de pago</td>
               </tr>
               <tr>
                 <td colspan="4">
                    <table width="100%" style="border-style:ridge; border-color:#808080; border-width:2px;" >
                     <tr>
                       <td style="width:25%"   class="campos">Ration cap de Pago:</td>
                       <td style="width:25%"><asp:Label ID="lblration" runat="server" ></asp:Label></td>
                       <td style="width:25%" ></td>
                       <td style="width:25%"></td> 
                     </tr>         
                   </table>        
                 </td>
                </tr>
                <tr>
                 <td  class="campos">Resultado de Score</td>
                </tr>
                <tr>
                  <td colspan="4">
                    <table width="100%" style="border-style:ridge; border-color:#808080; border-width:2px;" >
                    <tr>
                     <td class="campos">Score:</td>
                     <td><asp:Label ID="lblresulscore" runat ="server" ></asp:Label></td>
                     <td class="campos">Score Coacreditado:</td>
                     <td></td> 
                    </tr>           
                   </table>       
                 </td> 
                </tr>   
             </table>  
         </div>
         </div>
         <div id="tabs-3">
            <div>
              <table width="100%">
               <tr>
                 <td class="tituloConsul">DATOS PERSONALES </td>
               </tr>  
              </table>  
            </div>
            <div style="border-style:ridge; border-color:#808080; border-width:2px;">
               <table width="100%">
                <tr>
                  <td class="campos">Nombre Completo:</td>
                  <td><asp:Label ID="lblnomCom" runat="server"></asp:Label></td>
                  <td class="campos">RFC:</td>
                  <td><asp:Label ID="lblRFC" runat="server"></asp:Label></td>
                  <td class="campos">Teléfono Particular:</td>
                  <td><asp:Label ID="lbltelpar" runat="server"></asp:Label></td>        
                </tr>
                <tr>
                  <td class="campos">Teléfono Movil:</td>
                  <td><asp:Label ID="lbltelmov" runat="server"></asp:Label></td>
                  <td class="campos">Correo Electrónico:</td> 
                  <td><asp:Label ID="lblcorreoe" runat="server" ></asp:Label> </td>
                  <td class="campos">Edad:</td> 
                  <td><asp:Label ID="lbledad" runat="server"></asp:Label></td>
                </tr>
                <tr>
                  <td class="campos">Domicilio:</td>
                  <td><asp:Label ID="lbldomi" runat="server"></asp:Label></td>
                  <td class="campos">CP:</td>
                  <td><asp:Label ID="lblcp" runat="server"></asp:Label>  </td>
                  <td class="campos">Colonia:</td>
                  <td><asp:Label ID="lblcolonia" runat="server"></asp:Label>  </td>
                </tr>
                <tr>
                  <td class="campos">Delegación/Municipio:</td>
                  <td><asp:Label ID="lbldelega" runat ="server"></asp:Label> </td>
                  <td class="campos">Estado:</td>
                  <td><asp:Label ID="lblestado" runat="server"></asp:Label>  </td>  
                  <td class="campos">Ciudad:</td>
                  <td><asp:Label ID="lblciudad" runat="server"></asp:Label> </td>
                </tr>
                <tr>
                  <td class="campos">Sexo:</td>
                  <td><asp:Label ID="lblsexo" runat ="server" ></asp:Label></td>
                  <td class="campos">Fecha Nacimiento:</td>
                  <td><asp:Label ID="lblfechana" runat="server"></asp:Label></td>
                  <td class="campos">Nacionalidad:</td>
                  <td><asp:Label ID="lblnaciona" runat="server"></asp:Label>  </td>   
                </tr>
                <tr>
                   <td class="campos">CURP:</td>
                   <td><asp:Label ID="lblcrup" runat ="server"></asp:Label> </td>   
                   <td class="campos">Estado Civil:</td>
                   <td><asp:Label ID="lblestadocivil" runat ="server" ></asp:Label></td>
                   <td class="campos">Viven en:</td>
                   <td><asp:Label ID="lblvive" runat="server"></asp:Label> </td>          
                </tr>
                <tr>
                   <td class="campos">Tiene Propiedades a su Nombre:</td>
                   <td><asp:Label ID="lblpropiedadasu" runat="server"></asp:Label>   </td>
                </tr>
               </table>  
            </div>
            <div>
              <table width="100%">
                 <tr>
                   <td class="tituloConsul">EMPLEO</td>       
                 </tr>    
              </table>      
            </div>
               <div style="border-style:ridge; border-color:#808080; border-width:2px;">
                 <table width="100%">
                  <tr>
                    <td class="campos">Compañia:</td>
                    <td><asp:Label ID="lblcompania" runat="server"></asp:Label></td>
                    <td class="campos">Puesto:</td>
                    <td><asp:Label ID="lblpuesto" runat="server"></asp:Label> </td>
                    <td class="campos">Departamento:</td>
                    <td><asp:Label ID="lbldepartamen" runat="server"></asp:Label> </td>  
                  </tr>
                  <tr>
                    <td class="campos">Teléfono:</td>
                    <td><asp:Label ID="lbltelemp" runat ="server" ></asp:Label></td>
                    <td class="campos">Ext:</td>
                    <td><asp:Label ID="lblextemp" runat="server"></asp:Label> </td> 
                    <td class="campos">Años Antiguedad:</td>
                    <td><asp:Label ID="lblanoantiguedad" runat="server" ></asp:Label></td>             
                  </tr>
                  <tr>
                    <td class="campos">Sueldo Mensual:</td>
                    <td><asp:Label ID="lblsuelmensu" runat="server" ></asp:Label></td>
                    <td class="campos">Domicilio:</td> 
                    <td><asp:Label ID="lblDomEmp" runat="server"></asp:Label> </td>
                    <td class="campos">CP:</td>
                    <td><asp:Label ID="lblcpemp" runat="server"></asp:Label>  </td>
                  </tr>
                  <tr>
                    <td class="campos">Colonia:</td>
                    <td><asp:Label ID="lblcoloniemp" runat="server"></asp:Label> </td>
                    <td class="campos">Delegación/Municipio:</td>
                    <td><asp:Label ID="lbldelegaemp" runat="server"></asp:Label>  </td>
                    <td class="campos" >Estado:</td>
                    <td><asp:Label ID="lblestadoemp" runat="server"></asp:Label> </td> 
                  </tr>
                  <tr>
                     <td class="campos">Ciudad:</td>
                     <td><asp:Label ID="lblciudademp" runat="server" ></asp:Label></td> 
                   </tr>
                </table> 
               </div>
               <div>
                 <table width="100%">
                   <tr>
                     <td class="tituloConsul">COACREDITADO</td>  
                    </tr>
                 </table> 
               </div>
               <div style="border-style:ridge; border-color:#808080; border-width:2px;">
                 <table width="100%">
                   <tr>
                     <td class="campos">Nombre Completo:</td>
                     <td><asp:Label ID="lblnombreComcoa" runat="server" ></asp:Label> </td>
                     <td class="campos">RFC:</td>
                     <td><asp:Label ID="lblrfccoa" runat="server" ></asp:Label> </td>
                     <td class="campos">Teléfono Particular:</td>
                     <td><asp:Label ID="lbltelparcoa" runat="server"></asp:Label> </td>   
                   </tr>
                   <tr>
                     <td class="campos">Teléfono Movil:</td>
                     <td><asp:Label ID="lbltelemovicoa" runat="server"></asp:Label>  </td>
                     <td class="campos">Correo Electrónico:</td>
                     <td><asp:Label ID="lblcorroelecoa" runat="server"></asp:Label>  </td>
                     <td class="campos">Edad:</td>
                     <td><asp:Label ID="lbledadcoa" runat="server"></asp:Label>  </td>        
                   </tr>
                   <tr>
                     <td class="campos">Domicilio:</td>
                     <td><asp:Label ID="lbldomicicoa" runat="server"></asp:Label>  </td>
                     <td class="campos">CP:</td>
                     <td><asp:Label ID="lblcpcoa" runat="server"></asp:Label>  </td>
                     <td class="campos">Colonia:</td>
                     <td><asp:Label ID="lblcoloniacoa" runat="server" ></asp:Label> </td>       
                   </tr>
                   <tr>
                     <td class="campos">Delegación/Municipio:</td>
                     <td><asp:Label ID="lbldelegacoa" runat="server"></asp:Label>  </td>
                     <td class="campos">Estado:</td>
                     <td><asp:Label ID="lblestadocoa" runat="server"></asp:Label>  </td> 
                     <td class="campos">Ciudad:</td>
                     <td><asp:Label ID="lblciudadcoa" runat="server"></asp:Label> </td>    
                   </tr>
                   <tr>
                     <td class="campos">Sexo:</td>
                     <td><asp:Label ID="lblsexocoa" runat="server"></asp:Label>  </td>
                     <td class="campos">Fecha Nacimiento:</td>
                     <td><asp:Label ID="lblfechanacimicoa" runat="server" ></asp:Label> </td>
                     <td class="campos">Nacionalidad:</td>
                     <td><asp:Label ID="lblnacionalicoa" runat="server"></asp:Label>  </td>      
                   </tr>
                   <tr>
                     <td class="campos">CURP:</td>
                     <td><asp:Label ID="lblcrpcoa" runat="server"></asp:Label>  </td>
                     <td class="campos">Estado Civil:</td>
                     <td><asp:Label ID="lblestadocivicoa" runat="server" ></asp:Label> </td>
                     <td class="campos">Viven en:</td>
                     <td><asp:Label ID="lblvivencoa" runat="server" ></asp:Label> </td>          
                   </tr>
                   <tr>
                     <td class="campos">Tiene Propiedades a su Nombre:</td>
                     <td><asp:Label ID="lblpropiedadcoa" runat="server"></asp:Label>   </td>  
                   </tr>
             </table> 
             </div>

         </div>
          <div id="tabs-4">
            <asp:GridView ID="grvDocumento" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="encabezados" Width="100%" PagerStyle-HorizontalAlign="Right" CssClass="resulGrid" AllowSorting="True" >
            <Columns>
                <asp:BoundField DataField="PDk_ID_DOCUMENTOS" ItemStyle-CssClass ="resul" HeaderText ="Clave" />
                <asp:HyperLinkField DataNavigateUrlFields="URL" DataTextField="PDK_DOC_NOMBRE" HeaderText="Nombre" />  
                <asp:BoundField DataField="PDK_ST_ENTREGADO" ItemStyle-CssClass ="resul" HeaderText ="Entregado" /> 
                <asp:BoundField  DataField="PDK_ST_VALIDADO" ItemStyle-CssClass="resul" HeaderText="Validado" />
                <asp:BoundField DataField="PDK_ST_RECHAZADO" ItemStyle-CssClass="resul" HeaderText="Rechazado" />   
   
           </Columns> 
           </asp:GridView>
        </div>
        
  </div>
</div>

<div id="divValorScore" class="dvScoreMoral">

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
            <asp:Button runat="server" ID="btnRegresar" text="Regresar" SkinID="btnGeneral" />
           <%--<input type="button" class="Text" value="Regresar" onclick="fnRegresar();"  />--%>     
           <input id="btnGuardarCom" value="Procesar" type="button" runat="server"   class="Text" onclick="$('#ctl00_ctl00_cphCuerpo_cphPantallas_btnGuardarCom').attr('disabled', 'disabled'); funtValidarchech();" />  
           <%--<asp:Button runat="server" ID="btnGuardar" text="Procesar" SkinID="btnGeneral" />--%>
           <asp:Button runat="server" ID ="btnAutorizar" Visible="false"     Text ="Autorizar" SkinID ="btnGeneral" />
           <asp:Button runat="server" ID ="btnCancelar" Text="Cancelar" SkinID ="btnGeneral" />
           
                                                                                                                                       
        </td>
      </tr>
    </table>
  </div>     
</div>   
<asp:HiddenField ID="hdnIdFolio" runat="server" />
<asp:HiddenField  ID="hdnIdPantalla" runat="server" />
<asp:HiddenField ID="hdnUsua" runat="server" />
<asp:HiddenField ID="hdnResultado" runat="server" />
<asp:HiddenField ID="hdRutaEntrada" runat="server" />
<asp:HiddenField ID="hdNomPantalla" runat="server" /> 
<asp:HiddenField ID="hdnEnable" runat="server" /> 
<asp:HiddenField ID="hdnResultado1" runat="server" />
<asp:HiddenField ID="hdnResultado2" runat ="server" />
<asp:HiddenField ID="hdnIdTipoJuridica" runat="server" />
  


</asp:Content>  
