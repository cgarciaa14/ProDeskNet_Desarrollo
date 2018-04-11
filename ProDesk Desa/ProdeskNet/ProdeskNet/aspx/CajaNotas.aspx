<%@ Page Language="VB" MasterPageFile="~/aspx/MasterPageVacia.master" AutoEventWireup="false" CodeFile="CajaNotas.aspx.vb" Inherits="aspx_CajaNotas" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/MasterPageVacia.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">
    <!-- BBV-P-423: BUG-PD-58: ERODRIGUEZ: 17/05/2017 Caja de mensajes por numero de solicitud-->	
	<!--'BBV-P-423: BUG-PD-62: ERODRIGUEZ: 24/05/2017 Permite marcar mensajes como leidos y vista de mensajes sin leer-->
    <!--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit-->

    <style>
        .negrita { font-weight: bold; }
        #tableContent
        {
            width: 100%;
            /*outline: 1px solid black;*/
        }
        #tableContent td
        {
            width: 80%;
            padding: 10px;
            /*outline: 1px solid red;*/
        }
        #tableMessajes
        {
            font-family: Verdana, Helvetica, Arial, sans-serif;
            font-size: 7pt;
            color: Black;
            font-weight: bold;
            text-align: left;
            background: white;
            width: 100%;
        }
        #tableMessajes tr
        {
            outline: 1px solid black;
        }
        textarea#textEditor
        {
            width: 99%;
            height: 80px;
        }
        .left
        {
            text-align: left;
        }
        .right
        {
            text-align: right;
        }
        .center
        {
            text-align: center;
        }
        .centerBold
        {
            text-align: center;
            font-size: 15px;
            font-weight: bold;
        }
        td.fecha
        {
            width: 15%!important;
        }
    </style>
    <script language ="javascript" type ="text/javascript" src ="../js/jquery.js"></script>

<div class="divAdminCat">

	    <div class="divFiltrosConsul">
		    <table>
			    <tr>
				    <td class="tituloConsul"><asp:Label ID="lbltitulo" Text="" runat="server">Mensajes</asp:Label>    </td>
			    </tr>
		    </table> 
	    </div>

    <div class ="divAdminCatCuerpo">
        <div id="debug" runat="server"  style="position:absolute ; top:0%; left:0%; width:100%; height:100%;  overflow:auto;">
        </div>

        <div id="pantalla" runat="server"  style="position:absolute ; top:0%; left:0%; width:100%; height:100%;  overflow:auto;">
            <table id="tableContent" border="0">
                <tbody>
                    <tr>
                        <td class="right"><textarea style="width:99%; height: 80px;" runat="server" id="textEditor"></textarea></td>
                        <td class="left"><asp:Button runat="server" ID="btnGuardar" Text="Guardar" SkinID="btbGeneral" /></td>
                        <td class="left"><asp:Button runat="server" ID="btnActualizar" style="visibility:hidden" Text="Actualizar" SkinID="btbGeneral" /></td>
                    </tr>
                    <tr>
                        <td class="right">
                            <table id="tableMessajes">
                                <tbody id="messages" runat="server">
                                    <tr><th class="centerBold">No hay mensajes para mostrar</th></tr>
                                </tbody>
                            </table>
                        </td>
                        <td class="left"></td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div id="divcancela" style="display:none" >
            <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID ="btnCancela" CancelControlID="btnCancelCancela"  PopupControlID="popCancela" BackgroundCssClass ="modalBackground">--%>
            <%--</cc1:ModalPopupExtender> --%>
            <asp:Panel ID="popCancela" runat ="server" CssClass ="cajadialogo">
                <div class="tituloConsul"  >
                    <asp:Label ID="Label1" runat="server" Text="Autorización" />
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
                   <%--<tr>
                    <td colspan="2" class="campos" >Descripción:</td>
                  </tr>
                  <tr>
                    <td colspan="2"><textarea id="TxtmotivoCan" runat="server" onkeypress="ManejaCar('A',1,this.value,this)" class = "Text" rows = "5" cols = "1" style = "width:100%"></textarea> </td>
                 </tr>--%>
                  <tr style="width:100%">
                   <td><asp:HiddenField runat="server" ID="HiddenField1" /></td>
                   <td align="left" valign="middle">
                        <input id="btnGuardarCancelar" type="button" runat="server" value="Adelantar" class="Text " onclick="cancela();"  /> 
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
                         <input id="txtXML" type="text" runat="server"  style="display:none"   />
                         <input id="txtXSL"  type="text" runat="server"   style="display:none" />
                        <input id="btnCancela" value="Cancelar" type="button" onclick="mostrarCanceDiv()"  class="Text" runat="server" style="display: none;"/>
                     </td>
                </tr>
            </table>
      </div>
      
</div>


    <script language ="javascript" type ="text/javascript" >
        function manejaTarea(obj, opt, tarea) {
            $(obj).attr('disabled', true);
            if (opt == 1) {
                if (!confirmaAccion('Está seguro de querer regresar a la tarea: "' + tarea + '"')) {
                    $(obj).attr('disabled', false);
                } else {
                    $('button').attr('disabled', true);
                    btnInsUpd('INSERT INTO PDK_CARATULA_TAR_MOV (id_solicitud,id_tar_actual,id_tar_anterior,id_tar_siguiente,tipo_movimiento,id_usuario) VALUES (' + $('[id$=hdnFolio]').val() + ',' + $('[name="idTarActual"]').val() + ',' + $('[name="idTarAnterior"]').val() + ',' + $('[name="idTarSiguiente"]').val() + ',1,' + $('[id$=hdnUsuario]').val() + ');', '');
                    btnInsUpd('EXEC sp_regresarTarea ' + $('[id$=hdnFolio]').val(), 'alert("Se regresó satisfactoriamente a la tarea: ' + tarea + '"); location.reload();');
                }
            } else if (opt == 2) {
                if (!confirmaAccion('Está seguro de querer adelantar a la tarea: "' + tarea + '"')) {
                    $(obj).attr('disabled', false);
                } else {
                    $('button').attr('disabled', true);
                    $('[id$=btnCancela]').trigger('click');
                }
            }
        }

        function confirmaAccion(msg) {
            var r = confirm(msg);
            if (r == true) {
                return true;
            } else {
                return false;
            }
        }

        function mostrarCanceDiv() {
            div = document.getElementById('divcancela');
            div.style.display = '';

        }

        function cancela() {
            btnInsUpd('INSERT INTO PDK_CARATULA_TAR_MOV (id_solicitud,id_tar_actual,id_tar_anterior,id_tar_siguiente,tipo_movimiento,id_usuario) VALUES (' + $('[id$=hdnFolio]').val() + ',' + $('[name="idTarActual"]').val() + ',' + $('[name="idTarAnterior"]').val() + ',' + $('[name="idTarSiguiente"]').val() + ',2,' + $('[id$=hdnUsuario]').val() + ');', '');
            btnInsUpd("EXEC sp_ValidacionUsuario " + $('[id$=tareaActual]').val() + ",'" + $('[id$=txtusua]').val() + "','" + $('[id$=txtpass]').val() + "'," + $('[id$=hdnFolio]').val() + "," + $('[id$=hdnUsuario]').val() + ",64,1,''");
            $('[id$=btnCancelCancela]').trigger('click');
            alert("Se autorizó satisfactoriamente la tarea.");
            location.reload();
        }

        function fnTerminarSolicitud(obj, solicitud, boton, usuario) {
            if (!confirmaAccion('Está seguro de querer terminar la solicitud número: "' + solicitud + '"')) {
                $(obj).attr('disabled', false);
            } else {
                $('button').attr('disabled', true);
                btnInsUpd('INSERT INTO PDK_CARATULA_TAR_MOV (id_solicitud,id_tar_actual,id_tar_anterior,id_tar_siguiente,tipo_movimiento,id_usuario) VALUES (' + $('[id$=hdnFolio]').val() + ',' + $('[name="idTarActual"]').val() + ',' + $('[name="idTarAnterior"]').val() + ',' + $('[name="idTarSiguiente"]').val() + ',1,' + $('[id$=hdnUsuario]').val() + ');', '');
                btnInsUpd('EXEC spValNegocio ' + solicitud + ', ' + boton + ', ' + usuario, 'alert("Se procesó satisfactoriamente a la solicitud número: ' + solicitud + '"); location.reload();');
            }
        }

        $('.btnBorrar').click(function () { borrarMensaje($(this).attr('dataID')) })

        function borrarMensaje(id) {
            if (confirmaAccion('¿Está seguro de querer borrar este mensaje?')) {
                btnInsUpd('UPDATE PDK_CAJA_NOTAS SET estatus = 0, fe_borrado = GETDATE(), usu_borrado = ' + $('[id$=hdnUsuario]').val() + ' WHERE id = ' + id, '');
                $('.btnBorrar[dataid=' + id + ']').parent().parent().hide()
            }
        }

        $('.btnLeer').click(function () { marcarMensajeLeido($(this).attr('dataID')) })

        function marcarMensajeLeido(id) {
            if (confirmaAccion('¿Desea marcar como leido este mensaje?')) {
                btnInsUpd('UPDATE PDK_CAJA_NOTAS SET leido = 1 WHERE id = ' + id, '');
                $('[id$=btnActualizar]').trigger('click');               
            }
        }


        $(document).ready(function () {
            $("[id$=btnGuardar]").click(function () { $(this).attr('disabled'); })
            $("[id$=btnActualizar]").click(function () { $(this).attr('disabled'); })
        })
    </script>

    <asp:HiddenField runat="server" ID="hdnFolio" />
    <asp:HiddenField runat="server" ID="hdnUsuario" />
    <asp:HiddenField runat="server" ID="tareaActual" />
</asp:Content>