<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile ="~/aspx/MasterPageVacia.Master" CodeFile="VerificaDatSol.aspx.vb" Inherits="aspx_VerificaDatSol" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/MasterPageVacia.Master" %>

<asp:Content ID="content1" ContentPlaceHolderID="cphPantallas" runat="server" >         
 
    <!--  YAM-P-208  egonzalez 12/08/2015 Se agregó una función para la limpieza de las tablas que no contenían campos -->
    <!--  YAM-P-208  egonzalez 18/08/2015 Se agregó una validación en la función fnCargaPantalla() ya que estaban causando conflictos la declaración de unos datepicker  -->
    <!--  YAM-P-208  egonzalez 01/09/2015 implementó la función "deshabilitaCampos()" para bloquear los campos que no se deben modificar en ningún punto del flujo  -->
    <!--  YAM-P-208  egonzalez 03/09/2015 Se implementó la función "ocultarSolicitante()" para eliminar la información extra en la sección de "Coacreditado" -->
    <!--  YAM-P-208  egonzalez 06/10/2015 Se implementó la función validación de elementos y se agregó la librería de funciones para el RFC -->
    <!--  BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar-->
    <!--  BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit-->

        <script language ="javascript" type ="text/javascript" src ="../js/jquery.js"></script>
        <script language ="javascript" type ="text/javascript" src ="../js/rfc.js"></script>
         <script language="javascript" type ="text/javascript" >
           //--- INC-B-1922 
           //funcion que valida la impresion del documento de buro  

           function fnIsEneable() {

               if (QueryString.Enable == 1) {
                   btnProcesar.hide();
                   btnCancela.hide();
                   btnAutoriza.hide();
                   btnImprimir.show();
               }
               else {
                   btnProcesar.show();
                   btnCancela.show();
                   btnAutoriza.show();
                   btnImprimir.hide();
               }
           }

           function fnValidaBuro() {

               if (chkBuro.is(":checked")) {
                   btnProcesar.show();
               }
               else {
                   btnProcesar.hide();
               }

               chkBuro.click(function () {
                   if (chkBuro.is(":checked")) {
                       var c = confirm('Se requiere imprimir el contrato de aceptacion de la consulta a buro de credito.');
                       if (c == true) {
                           window.location.href = '../Documentos/Formatos de autorización-cambios SOFOM ENR.rtf';
                           btnProcesar.show();
                       }
                       else {
                           chkBuro.attr('checked', false);
                           btnProcesar.hide();
                       }
                   }
                   else { btnProcesar.hide(); }
               });
           }

           function fnCargaPantalla() {
               chkBuro = $('[id*="VISITAR_BURO"]');
               btnProcesar = $("[id$=Button1]");
               btnCancela = $("[id$=btnCancela]");
               btnAutoriza = $("[id$=btnAutoriza]");
               btnImprimir = $("[id$=btnImprimir]");

               fnIsEneable()

               if (pantalla == 15 || pantalla == 47) {
               $('[id$=txtFECHA_CONSTITUCION470]').datepicker();
               $('[id$=txtFECHA_REGISTRO477]').datepicker();
               $('[id$=txtFECHA_OPERACION481]').datepicker();
               }

               $('[name$=txtPREGUNTA1422]').click(function () {
                   var enable = $('[id$=txtNUMERO_CREDITO153]')
                   if ($(this).parent().text() == "SI") { enable.removeAttr('disabled'); $('[id$=hdvalidad]').val('NUMERO_CREDITO153') } else { enable.attr('disabled', 'disabled'); $('[id$=hdvalidad]').val('') }

               });

               $('[name$=txtPREGUNTA1425]').click(function () {
                   var enable = $('[id$=txtNUMERO_DE_CREDITO_COA176]')
                   if ($(this).parent().text() == "SI") { enable.removeAttr('disabled'); $('[id$=hdvalidadCoa]').val('NUMERO_DE_CREDITO_COA176'); } else { enable.attr('disabled', 'disabled'); $('[id$=hdvalidadCoa]').val(''); }
               });


               var pantalla = $("[id$=hdnIdRegistro]").val();
               if (pantalla == 1 || pantalla == 7 || pantalla == 23 || pantalla == 29 || pantalla == 35 || pantalla == 41 || pantalla == 59) {
                   llenarfecha();
                   var anos = $("[id$=hdnano]").val();
                   var mes = $("[id$=hdnmes]").val();
                   var dia = $("[id$=hdndia]").val();
                   if (anos != "") { $('[id$=ddltxtANO_NACIMIENTO412]')[0].value = anos; }
                   if (mes != "") { if (mes < 10) { mes = '0' + mes; } $('[id$=ddltxtMES_NACIMIENTO413]')[0].value = mes; }
                   if (dia != "") { if (dia < 10) { dia = '0' + dia; } $('[id$=ddltxtDIA_NACIMIENTO414]')[0].value = dia; }

                   if (pantalla != 7 && pantalla != 41) {
                       //se cambia codigo por funcion que valida la impresion del documento de buro
                       if (QueryString.Enable == undefined) {
                           fnValidaBuro();
                       }
                   }

               } else if (pantalla == 5 || pantalla == 15 || pantalla == 27 || pantalla == 39 || pantalla == 47) {
                   llenarfechaCoa();
                   if (pantalla == 5) {
                       if (QueryString.Enable == undefined) {
                           fnValidaBuro();
                       }
                   }
                   var anos = $("[id$=hdnanocoa]").val();
                   var mes = $("[id$=hdnmescoa]").val();
                   var dia = $("[id$=hdndiacoa]").val();
                   if (anos != "") { $('[id$=ddltxtANO_NACIMIENTO415]')[0].value = anos; }
                   if (mes != "") { if (mes < 10) { mes = '0' + mes; } $('[id$=ddltxtMES_NACIMIENTO416]')[0].value = mes; }
                   if (dia != "") { if (dia < 10) { dia = '0' + dia; } $('[id$=ddltxtDIA_NACIMIENTO417]')[0].value = dia; }

               } else if (pantalla == 6 || pantalla == 5 || pantalla == 27 || pantalla == 28 || pantalla == 39 || pantalla == 40) {
                   llenarfecha();
                   llenarfechaCoa();
                   var anos = $("[id$=hdnano]").val();
                   var mes = $("[id$=hdnmes]").val();
                   var dia = $("[id$=hdndia]").val();
                   if (anos != "") { $('[id$=ddltxtANO_NACIMIENTO412]')[0].value = anos; }
                   if (mes != "") { if (mes < 10) { mes = '0' + mes; } $('[id$=ddltxtMES_NACIMIENTO413]')[0].value = mes; }
                   if (dia != "") { if (dia < 10) { dia = '0' + dia; } $('[id$=ddltxtDIA_NACIMIENTO414]')[0].value = dia; }
                   var anoscoa = $("[id$=hdnanocoa]").val();
                   var mescoa = $("[id$=hdnmescoa]").val();
                   var diacoa = $("[id$=hdndiacoa]").val();
                   if (anoscoa != "") { $('[id$=ddltxtANO_NACIMIENTO415]')[0].value = anoscoa; }
                   if (mescoa != "") { if (mescoa < 10) { mescoa = '0' + mescoa; } $('[id$=ddltxtMES_NACIMIENTO416]')[0].value = mescoa; }
                   if (diacoa != "") { if (diacoa < 10) { diacoa = '0' + diacoa; } $('[id$=ddltxtDIA_NACIMIENTO417]')[0].value = diacoa; }
               }
           }

           $(document).ready(function () {
               $('[id$=Button1]').click(function () {
                   $(this).prop('disabled', true);
                   btnGuardar($(this).attr('id'));
               })
               fnCargaPantalla();
               deshabilitaCampos();
           });

           function llenarfechaCoa() {
               var hoy = new Date();
               var mes = hoy.getMonth();
               var dia = hoy.getDay();
               var annio = hoy.getFullYear();
               // llena ddl MES
               var month = '1';
               var ddl = $("[id$=ddltxtMES_NACIMIENTO416]")[0];
               while (month <= 12) {
                   valorOpc = new Option;
                   if (month < 10) {
                       valorOpc.text = '0' + month;
                       valorOpc.value = '0' + month;
                   }
                   else {
                       valorOpc.text = month;
                       valorOpc.value = month;
                   }
                   insertaddl(month - 1, valorOpc, ddl)
                   month++
               }
               // llena ddl ANNIO
               var inicio = annio - 100;
               var i = 0;
               var ddl = $("[id$=ddltxtANO_NACIMIENTO415]")[0];
               while (inicio <= annio) {
                   valorOpc = new Option;
                   valorOpc.text = inicio;
                   valorOpc.value = inicio;
                   insertaddl(i, valorOpc, ddl)
                   i++
                   inicio++
               }
               llenaddlDiascoa();

           }

           function llenarfecha() {
               var hoy = new Date();
               var mes = hoy.getMonth();
               var dia = hoy.getDay();
               var annio = hoy.getFullYear();
               // llena ddl MES
               var month = '1';
               var ddl = $("[id$=ddltxtMES_NACIMIENTO413]")[0];
               while (month <= 12) {
                   valorOpc = new Option;
                   if (month < 10) {
                       valorOpc.text = '0' + month;
                       valorOpc.value = '0' + month;
                   }
                   else {
                       valorOpc.text = month;
                       valorOpc.value = month;
                   }
                   insertaddl(month - 1, valorOpc, ddl)
                   month++
               }
               // llena ddl ANNIO
               var inicio = annio - 100;
               var i = 0;
               var ddl = $("[id$=ddltxtANO_NACIMIENTO412]")[0];
               while (inicio <= annio) {
                   valorOpc = new Option;
                   valorOpc.text = inicio;
                   valorOpc.value = inicio;
                   insertaddl(i, valorOpc, ddl)
                   i++
                   inicio++
               }
               llenaddlDias();



           }

           function insertaddl(id, valorOpc, ddl) {
               ddl.options[id] = valorOpc;
           }

           function daysInMonth(humanMonth, year) {
               return new Date(year || new Date().getFullYear(), humanMonth, 0).getDate();
           }

           function llenaddlDias() {
               // llena ddl day
               i = 1;
               var ddl = $("[id$=ddltxtDIA_NACIMIENTO414]");
               var sb;
               var mes = $("[id$=ddltxtMES_NACIMIENTO413]").val();
               var annio = $("[id$=ddltxtANO_NACIMIENTO412]").val();
               var numdias = daysInMonth(mes, annio);
               ddl.find("option").remove();
               while (i <= numdias) {
                   if (i < 10) {
                       sb += "<option value='0" + i + "'>0" + i + "</option>";
                   }
                   else {
                       sb += "<option value='" + i + "'>" + i + "</option>";
                   }
                   i++
               }
               ddl.append(sb);

           }

           function llenaddlDiascoa() {
               // llena ddl day
               i = 1;
               var ddl = $("[id$=ddltxtDIA_NACIMIENTO417]");
               var sb;
               var mes = $("[id$=ddltxtMES_NACIMIENTO416]").val();
               var annio = $("[id$=ddltxtANO_NACIMIENTO415]").val();
               var numdias = daysInMonth(mes, annio);
               ddl.find("option").remove();
               while (i <= numdias) {
                   if (i < 10) {
                       sb += "<option value='0" + i + "'>0" + i + "</option>";
                   }
                   else {
                       sb += "<option value='" + i + "'>" + i + "</option>";
                   }
                   i++
               }
               ddl.append(sb);


           }

           function calculaEdad() {
               var edad = 0;
               var hoy = new Date();
               var añoshoy = hoy.getFullYear();
               var meshoy = hoy.getMonth() + 1;
               var diahoy = hoy.getDate();
               var annio = $("[id$=ddltxtANO_NACIMIENTO412]").val();
               var mes = $("[id$=ddltxtMES_NACIMIENTO413]").val();
               var dianacimiento = $("[id$=ddltxtDIA_NACIMIENTO414]").val()
               var fecha = mes + '-' + dianacimiento + '-' + annio
               fecha = new Date(fecha);
               var edad = parseInt((hoy - fecha) / 365 / 24 / 60 / 60 / 1000)
               //            var edad = (añoshoy + 1900) - annio;
               //            if (meshoy < (mes - 1)) {
               //                edad--;
               //            }

               //            if (((mes - 1) == meshoy) && (diahoy < dianacimiento)) {
               //                edad--;
               //            }
               //            if (edad > 1900) {
               //                edad -= 1900;

               //            }
               $("[id$=txtEDAD13]").val(edad);


           }

           function calculaEdadcoa() {
               var edad = 0;
               var hoy = new Date();
               var añoshoy = hoy.getFullYear();
               var meshoy = hoy.getMonth() + 1;
               var diahoy = hoy.getDate();
               var annio = $("[id$=ddltxtANO_NACIMIENTO415]").val();
               var mes = $("[id$=ddltxtMES_NACIMIENTO416]").val();
               var dianacimiento = $("[id$=ddltxtDIA_NACIMIENTO417]").val()
               var fecha = mes + '-' + dianacimiento + '-' + annio
               fecha = new Date(fecha);
               var edad = parseInt((hoy - fecha) / 365 / 24 / 60 / 60 / 1000)
               //            var edad = (añoshoy + 1900) - annio;
               //            if (meshoy < (mes - 1)) {
               //                edad--;
               //            }

               //            if (((mes - 1) == meshoy) && (diahoy < dianacimiento)) {
               //                edad--;
               //            }
               //            if (edad > 1900) {
               //                edad -= 1900;

               //            }
               $("[id$=txtEDAD_COAC105]").val(edad);


           }

           function llenado(id) {
               var codi = $(id).val();
               $("[id$=hdnCp]").val(codi);
               fillddl('txtCOLONIA15', 'hdnCp');
               filltxt('DELEGA_O_MUNI17', 'hdnCp');
               filltxt('ESTADO18', 'hdnCp');
               filltxt('CIUDAD16', 'hdnCp');


           }

           function llenadoEmp(id) {
               var codi = $(id).val();
               $("[id$=hdnCp1]").val(codi);
               fillddl('txtCOLONIA52', 'hdnCp1');
               filltxt('DELEGA_O_MUNICI54', 'hdnCp1');
               filltxt('ESTADO51', 'hdnCp1');
               filltxt('CIUDAD53', 'hdnCp1');

           }

           function llenadoCoa(id) {
               var codi = $(id).val();
               $("[id$=hdnCp2]").val(codi);
               fillddl('txtCOLONIA85', 'hdnCp2');
               filltxt('DELEGA_O_MUNI87', 'hdnCp2');
               filltxt('ESTADO88', 'hdnCp2');
               filltxt('CIUDAD86', 'hdnCp2');


           }

           function llenarcoaemp(id) {
               var codi = $(id).val();
               $("[id$=hdnCp3]").val(codi);
               fillddl('txtCOLONIA122', 'hdnCp3');
               filltxt('DELEGA_O_MUNI124', 'hdnCp3');
               filltxt('ESTADO120', 'hdnCp3');
               filltxt('CIUDAD123', 'hdnCp3');

           }

           function llenarpersonamoral(id) {
               var codi = $(id).val();
               $("[id$=hdnCp4]").val(codi);
               fillddl('txtCOLONIA458', 'hdnCp4');
               filltxt('DELEGA_O_MUNI459', 'hdnCp4');
               filltxt('ESTADO460', 'hdnCp4');
               filltxt('CIUDAD461', 'hdnCp4');

           }

           function validarcorreo(id) {
               var correo = $(id).val();
               if (correo.indexOf('@') === -1) {
                   alert('Le falta el @');
                   $(id).val('');
               }




           }

           function btnGuardar(id) {
               var tabla;
               var into = "";
               var values = "";
               var upda = "";
               var folio = $("[id$=hdnFolio] ")
               var usuario = $("[id$=hdnUsuario] ")
               var pantalla = $("[id$=hdnPantalla] ")
               var cadena = "";
               var cadenaUp = "";
               var tablas = "";
               var f = folio.val();
               var u = usuario.val();
               var valida = "";
               var mensaje = "";
               var mensaje1 = "";
               var cvepantalla = $("[id$=hdnIdRegistro]").val();
               var pantalla = pantalla.val();
               var botones = id.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
               var validCREDITO = $('[id$=hdvalidad]').val();
               var validadrenta = $('[id$=hdnValiRenta]').val();
               var validadRenta2 = $('[id$=hdnValiRenta1]').val();
               var validaCrediCoa = $('[id$=hdvalidadCoa]').val();
               if (validCREDITO != '') { mensaje = validCREDITO }
               if (validadrenta != '') { mensaje += validadrenta }
               if (validadRenta2 != '') { mensaje += validadRenta2 }
               if (validaCrediCoa != '') { mensaje += validaCrediCoa }


               var mater = $("[id$=ctl00_ctl00_cphCuerpo_cphPantallas_pantalla] >table")
               mater.each(function (index) {
                   tabla = $(this).attr('id')
                   valida = tabla
                   tabla = tabla.replace('ctl00_ctl00_cphCuerpo_cphPantallas_U_', '');
                   tabla = tabla.replace('ctl00_ctl00_cphCuerpo_cphPantallas_I_', '');
                   valida = valida.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
                   valida = valida.replace(tabla, '');
                   valida = valida.replace('_', '');

                   $(this).children("caption").each(function (index2) { tablas += $(this).text() })//traer el nombre de la tabla 
                   console.log($(this).children("caption").each(function (index2) { tablas += $(this).text() }));
                   var newtb = $("[id$=" + tabla + "] tbody tr")

                   newtb.each(function (index) {


                       $(this).children("td").each(function (index3) {

                           $(this).children("table").each(
                            function (index8) {
                                var tablahijo = $(this).attr('id')
                                var newtabhijo = $("[id$=" + tablahijo + "] tbody tr")
                                newtabhijo.each(function (index9) {
                                    $(this).children("td").each(function (index10) {
                                        $(this).children("input").each(function (index11) {
                                            var chek1 = $(this).attr("type");
                                            if (chek1 == "radio") {
                                                var radio = $(this).is(':checked');
                                                if (radio == true) {

                                                    if (valida == "I") {
                                                        values += "'" + $(this).attr("value") + "',"
                                                    } else {
                                                        upda += "'" + $(this).attr("value") + "',"
                                                    }


                                                }
                                            }

                                        })

                                    })


                                })

                            })

                           $(this).children("span").each(
                               function (index4) {
                                   var txt1 = $(this)[0].id
                                   var matritxt = txt1.split("|");

                                   if (valida == "I") {
                                       //                                into += $(this).text() + ','
                                       into += matritxt[1] + ','
                                   }
                                   else {
                                       //                                upda += $(this).text() + '='
                                       upda += matritxt[1] + '='
                                   }
                               })
                           $(this).children("input").each(
                               function (index5) {
                                   var texto = $(this).val();
                                   var check = $(this).attr("type");


                                   //                           alert($(this).attr("value") + ' ' + $(this).attr("name"));


                                   if (check == "checkbox") {
                                       var chkbox = $(this).is(':checked');

                                       if (chkbox == true) { texto = "SI" } else { mensaje1 += $(this)[0].name; texto = "NO" }
                                       if (valida == "I") {
                                           values += "'" + texto + "',"
                                       } else {
                                           upda += "'" + texto + "',"
                                       }

                                   } else if (check == "text") {

                                       if (texto == '' || texto == 0) { mensaje += $(this)[0].name; }
                                       if (valida == "I") {
                                           values += "'" + texto + "',"
                                       } else {
                                           upda += "'" + texto + "',"
                                       }
                                   }
                                   //                            var chenk = $(this).is(':checked');

                               })



                           $(this).children("select").each(function (index6) {
                               var sele = $(this).attr('id')
                               //                        sele = sele.repalce('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
                               lista = $("[id$=" + sele + "] option:selected");
                               //                        alert(lista.text());
                               if (valida == "I") {
                                   values += "'" + lista.text() + "',"
                               } else {
                                   upda += "'" + lista.text() + "',"
                               }



                           })
                           $(this).children("textarea").each(function (index7) {

                               var texto = $(this).val();
                               if ((cvepantalla != 4) && (cvepantalla != 1)) { if (texto == '' || texto == 0) { mensaje += $(this)[0].name; } }

                               if (valida == "I") {
                                   values += "'" + texto + "',"
                               } else {
                                   upda += "'" + texto + "',"
                               }


                           })

                           ////(this).attr("value")
                           //$(this).children("span").each(function (index4) { into += $(this).text() + ',' })
                           //$(this).children("input").each(function (index5) { values += "'" + $(this).attr("value") + "'," })

                       })


                   })

                   if (valida == "I") {
                       cadena += ' INSERT INTO ' + tabla + '(' + into + 'PDK_ID_SECCCERO' + ',' + 'PDK_CLAVE_USUARIO' + ',' + 'PDK_FECHA_MODIF) VALUES (' + values + f + ',' + u + ',' + 'getdate()' + ')'
                   } else {
                       cadenaUp += '  UPDATE ' + tabla + ' ' + 'SET' + ' ' + upda + ' PDK_CLAVE_USUARIO=' + u + ',' + ' PDK_FECHA_MODIF=' + 'getdate()' + ' WHERE PDK_ID_SECCCERO=' + f + ' '

                   }

                   //            remple = tablas.replace('TAB', 'ID');
                   //            remple = remple.replace(' ', '') + ',';            
                   //                        
                   //            into = into.replace(remple, '');
                   //            alert(into);

                   //cadena += ' INSERT INTO ' + tabla + '(' + into + 'PDK_ID_SECCCERO' + ',' + 'PDK_CLAVE_USUARIO' + ',' + 'PDK_FECHA_MODIF) VALUES (' + values + f + ',' + u + ',' + 'getdate()' + ')'
                   tablas = "";
                   into = "";
                   values = "";
                   upda = "";


               })
               //        alert(cadena +' '+ cadenaUp +' ' + 'go EXEC spValNegocio ' + f)
               //return
               if (botones == "btnGuardarAutoriza") {
                   var cadena2 = cadena + ' ' + cadenaUp
                   var txtUsu = $('[id$=txtUsuario]').val()
                   var txtpsswor = $('[id$=txtPassw]').val()
                   var idpantalla = $("[id$=hdnIdRegistro] ").val()
                   var txtmotivoOb = $("[id$=txtmotivo]").val()
                   //           var cadenastored = 'EXEC sp_ValidacionUsuario ' + '"' + cadenatodo + '",' + idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1'
                   if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                   if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
                   if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }
                   //            txtmotivoOb = "'" + txtmotivoOb + "'"


                   btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 46, 1, txtmotivoOb)
                   //               btnInsertarBoton('EXEC sp_ValidacionUsuario ' +  idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1'+'' , cadena2 +'')
                   //            btnInsertar('EXEC sp_ValidacionUsuario ' +  idpantalla + ',' + txtUsu + ',' + txtpsswor + ',' + f + ',' + u + ',' + '46,' + '1',pantalla, '' + f + '', '1', '' + u + '',''+cadena2



               } else if (botones == "btnGuardarCancelar") {
                   var cadena2 = cadena + ' ' + cadenaUp
                   var txtUsu = $('[id$=txtusua]').val();
                   var txtpsswor = $('[id$=txtpass]').val();
                   var idpantalla = $("[id$=hdnIdRegistro] ").val()
                   var txtmotivoOb = $("[id$=TxtmotivoCan]").val()
                   if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                   if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
                   if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }
                   $('[id$=hdnResultado]').val($('[id$=hdRutaEntrada]').val())
                   //            txtmotivoOb = "'" + txtmotivoOb + "'"

                   btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)

               }

               else if (pantalla == 'ASIGNA ENTREVISTA' || pantalla == 'VALIDACION ENTREVISTA EXTERNA') {
                   btnInsertarPanta(cadena + ' ' + cadenaUp);
               }

               else {
                   var idpantalla = $("[id$=hdnIdRegistro] ").val()
                   var strPantalla = $("[id$=hdnPantalla]").val()


                   mensaje1 = ReplaceAll(mensaje1, 'ctl00$ctl00$cphCuerpo$cphPantallas$txt', ' \n ')
                   mensaje = ReplaceAll(mensaje, 'ctl00$ctl00$cphCuerpo$cphPantallas$txt', ' \n ')
                   mensaje = mensaje.replace('NOMBRE25', '');
                   mensaje = mensaje.replace('NOMBRE277', '');
                   //mensaje = mensaje.replace('NUM_COTIZACION441', '');
                   mensaje = mensaje.replace('NUMERO_CLIENTE434', '');
                   mensaje = mensaje.replace('NUM_INT22', '');
                   mensaje = mensaje.replace('NUM_INT46', '');
                   mensaje = mensaje.replace('NUM_INT91', '');
                   mensaje = mensaje.replace('NUM_INT114', '');
                   mensaje = mensaje.replace('NUMERO_CREDITO153', '');
                   mensaje = mensaje.replace('IMPORTE_RENTA436', '');
                   mensaje = mensaje.replace('IMPORTE_RENTA437', '');
                   mensaje = mensaje.replace('NUMERO_DE_CREDITO_COA176', '');
                   mensaje = mensaje.replace('INGRESO_VARIABLE_COMPROBABLE158', '');
                   mensaje = mensaje.replace('INGRESO_OTROS159', '');
                   mensaje = mensaje.replace('INGRESO_VARIABLE_COACREDITADO167', '');
                   mensaje = mensaje.replace('INGRESO_OTRO_COACREDITADO168', '');
                   mensaje = mensaje.replace('SERIE_FIRMA_ELECTRONICA438', '');
                   mensaje = mensaje.replace('SERIE_FIRMA_ELECTRONICA439', '');
                   mensaje = mensaje.replace('ORIGEN_ING_OTROS161', '');
                   mensaje = mensaje.replace('ORIGEN_ING_OTRO_COA170', '');
                   mensaje = mensaje.replace('OBSERVACION428', '');
                   mensaje = mensaje.replace('FIRMA_ELECTRONICA451', '');
                   mensaje = mensaje.replace('NUM_EXT455', '');
                   mensaje = mensaje.replace('NUM_INT456', '');
                   mensaje = mensaje.replace('PAGINA_INTERNET465', '');
                   mensaje = mensaje.replace('PAIS_OFICINA467', '');
                   mensaje = mensaje.replace('TIPO_ADMINISTACION480', '');
                   mensaje = mensaje.replace('IMPORTE_RENTA484', '');
                   mensaje = mensaje.replace('TELEFONO_MOVIL489', '');
                   mensaje = mensaje.replace('OTROS_GASTOS521', '');
                   mensaje = mensaje.replace('GASTOS_COACREDITADO522', '');
                   mensaje = mensaje.replace('OTROS_', '');
                   if (cvepantalla == 15 || cvepantalla == 47) {
                       mensaje = mensaje.replace('TIPO_EMPLEO116', '');
                       mensaje = mensaje.replace('COMPANIA106', '');
                       mensaje = mensaje.replace('ACTIVIDA_ECONOMICA118', '');
                       mensaje = mensaje.replace('DEPARTAMENTO108', '');
                       mensaje = mensaje.replace('PUESTO107', '');
                       mensaje = mensaje.replace('CALLE112', '');
                       mensaje = mensaje.replace('NUM_EXT113', '');
                       mensaje = mensaje.replace('NUM_INT114', '');
                       mensaje = mensaje.replace('CP121', '');
                       mensaje = mensaje.replace('COLONIA122', '');
                       mensaje = mensaje.replace('DELEGA_O_MUNI124', '');
                       mensaje = mensaje.replace('ESTADO120', '');
                       mensaje = mensaje.replace('CIUDAD123', '');
                       mensaje = mensaje.replace('TELEFONO_C_LADA109', '');
                   }
                   mensaje = $.trim(mensaje)
                   mensaje1 = $.trim(mensaje1)


                   //            alert(cadena);

                   if (mensaje1.length > 0) { alert('Debes de selecionar el campo ' + mensaje1); $('#ctl00_ctl00_cphCuerpo_cphPantallas_Button1').removeAttr('disabled', ''); return; }
                   if (mensaje.length > 0) { alert('Los campos estan vacios ' + mensaje); $('#Button1').removeAttr('disabled', ''); return; }
                   if ((strPantalla == 'PRECALIFICACION') || (strPantalla == 'COACREDITADO')) {
                       btnInsUpd(cadena + ' ' + cadenaUp + ' ', pantalla, '' + f + '', '1', '' + u + '');
                   } else {
                       btnInsUpd(cadena + ' ' + cadenaUp, 'alert("Registro actualizado")');
                   }
               }
           }

           function ReplaceAll(text, busca, remplaza) {
               var idx = text.toString().indexOf(busca);
               while (idx != -1) {
                   text = text.toString().replace(busca, remplaza);
                   idx = text.toString().indexOf(busca, idx);
               }
               return text;
           }

           function mostrarDiv() {
               div = document.getElementById('divautoriza');
               div.style.display = '';

           }

           function mostrarCanceDiv() {
               div = document.getElementById('divcancela');
               div.style.display = '';

           }

           function ConcatenarNom() {
               var nombre1 = $("[id$=txtNOMBRE14]").val()
               var nombre2 = $("[id$=txtNOMBRE25]").val()
               var apellidoPa = $("[id$=txtAPELLIDO_PATERNO6]").val();
               var apellidoMa = $("[id$=txtAPELLIDO_MATERNO7]").val();
               var nombreCom = $("[id$=txtNOMBRE_SOLICI419]");
               var concatenar = nombre1 + ' ' + nombre2 + ' ' + apellidoPa + ' ' + apellidoMa
               nombreCom.attr("value", concatenar)
           }

           function cleanFormTables() {
               $("[id$=ctl00_ctl00_cphCuerpo_cphPantallas_pantalla] >table").each(function (index) {
                   //$(this).children("caption").each(function (index2) { console.log($(this).text()); })
                   //console.log($(this).children('tbody').length);
                   if ($(this).children('tbody').length == 0) { $(this).remove() }
               });
           }

           function ocultarSolicitante() {
               if ($('[id$=OcultaSoli]').val() == 1) {
                   $('[id$=PDK_TAB_DATOS_SOLICITANTE], [id$=PDK_TAB_DISTRIBUIDOR], [id$=PDK_TAB_SOLICITANTE], [id$=PDK_TAB_DATOS_PERSONALES], [id$=PDK_TAB_EMPLEO], [id$=PDK_TAB_PERFIL_ECONOMICO], [id$=TAB_REFERE_PERSONALES], [id$=PDK_TAB_CARGO_DIRECTO]').remove();
               }
           }

           function deshabilitaCampos() {
               if ($("[id$=hdnIdRegistro]").val() == 7) {
                   $('[id*=PDK_TAB_DATOS_SOLICITANTE] input, [id*=PDK_TAB_DISTRIBUIDOR] input, [id*=PDK_TAB_DATOS_SOLICITANTE] select, [id*=PDK_TAB_DISTRIBUIDOR] select').attr('disabled', true);
                   $('[id*=CLIENTE_INCREDIT433], [id*=NOMBRE_VENDEDOR147]').attr('disabled', false);
               }
           }

           $(window).bind("load", function () {
               cleanFormTables();
               ocultarSolicitante();
           }); // Función que espera la carga comleta del documento incluyendo imágenes y Ajax*


</script>
     
<div class="divAdminCat">

    <div class="divFiltrosConsul">
    <table>
     <tr>
       <td class="tituloConsul"><asp:Label ID="lbltitulo" Text="" runat="server"></asp:Label>    </td>
     </tr>
    </table> 
    </div> 

    <div class ="divAdminCatCuerpo">

        <div id="pantalla" runat="server"  style="position:absolute ; top:0%; left:0%; width:100%; height:100%;  overflow:auto;">
        </div>

        <div id ="divautoriza" style="display:none">
            <%--<cc1:ModalPopupExtender ID="mpoAutorizar" runat="server" TargetControlID="btnAutoriza" PopupControlID="popAutoriza" CancelControlID="btnCancelAutoriza" BackgroundCssClass ="modalBackground" >
            </cc1:ModalPopupExtender>--%>
            <asp:Panel ID="popAutoriza" runat="server" Text="Autorización" CssClass ="cajadialogo" >
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

        <div id="divcancela" style="display:none" >            
            <asp:Panel ID="popCancela" runat ="server" CssClass ="cajadialogo">
                <div class="tituloConsul"  >
                    <asp:Label ID="Label1" runat="server" Text="Cancelación" />
                </div>
                <table width="100%">
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
    </div>   
    
    <div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td align="right" valign="middle" class = "cssCuerpo">             
                        <input id="Button1" value="Procesar" type="button"/>                        
                        <%--onclick="$(this).prop('disabled', true);btnGuardar(id)"--%>
                     </td>
                </tr>
            </table>
      </div>
      
</div>

    

<asp:HiddenField runat="server" ID="hdnIdRegistro" />
<asp:HiddenField runat="server" ID="hdnFolio" />
<asp:HiddenField runat="server" ID="hdnUsuario" />
<asp:HiddenField runat="server" ID="hdnPantalla" />
<asp:HiddenField runat="server" ID="hdnResultado" />
<asp:HiddenField runat="server" ID="hdnResultado1" /> 
<asp:HiddenField runat="server" ID="hdnResultado2" /> 
<asp:HiddenField runat="server" ID="hdnCp" />
<asp:HiddenField runat="server" ID="hdnCp1" />
<asp:HiddenField runat="server" ID="hdnCp2" />
<asp:HiddenField runat="server" ID="hdnCp3" />
<asp:HiddenField runat="server" ID="hdnCp4" />
<asp:HiddenField ID="hdRutaEntrada" runat="server" /> 
<asp:HiddenField runat="server" ID="hdnano" />
<asp:HiddenField runat="server" ID="hdnmes" />
<asp:HiddenField runat="server" ID="hdndia" />
<asp:HiddenField runat="server" ID="hdnanocoa" />
<asp:HiddenField runat="server" ID="hdnmescoa" />
<asp:HiddenField runat="server" ID="hdndiacoa" />  
<asp:HiddenField runat="server" ID="hdvalidad" /> 
<asp:HiddenField runat="server" ID="hdvalidadCoa" /> 
<asp:HiddenField runat="server" ID="hdnValiRenta" />
<asp:HiddenField runat="server" ID="hdnValiRenta1" />
<asp:HiddenField runat="server" ID="hdnOcultaSoli" />
</asp:Content>