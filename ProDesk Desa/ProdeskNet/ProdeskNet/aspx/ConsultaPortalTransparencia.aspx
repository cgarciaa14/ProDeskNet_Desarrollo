<%@ Page Language="VB" MasterPageFile="~/aspx/Home.Master" AutoEventWireup="false" CodeFile="ConsultaPortalTransparencia.aspx.vb" Inherits="aspx_ConsultaPortalTransparecia" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"   TagPrefix ="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<%--BBV-P-423: RQADM-15: JRHM: 17/03/17 SE CREA PANTALLA PARA TAREA DE "CONSULTA INGRESOS EN PORTAL DE TRANSPARENCIA"--%> 
<%--BBV-P-423: RQADM-22: JRHM: 24/03/17 SE AGREGA MENSAJE DE CARGA EXITOSA DE EVIDENCIA DE CONSULTA EN IFAI"--%> 
<%--BUG-PD-123: ERODRIGUEZ 29/06/2017 Se cambio el llamado a función para cancelar / autorizar--%>
<%--BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN--%>
<%--BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS PARA ABRIR VISOR TELEPRO--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server" >
    <script src="../js/jquery.ui.widget.js"></script>
    <script src="../js/jquery.iframe-transport.js"></script>
    <script src="../js/jquery.fileupload.js"></script>    
    <script src="../js/Funciones.js"></script>
    <script type="text/javascript" language="javascript">
        function mostrarCanceDiv() {
            $('#ventanaContain').show();
            $("#divcancela").show();

        }
        function fnChequea() {
            fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');
                PopUpLetrero("Documento procesado exitosamente");
            }


        function btnGuardar(id) {
            var boton = id.replace('ctl00_ctl00_cphCuerpo_cphPantallas_', '');
            var f = $('[id$=hdSolicitud]').val();
            var u = $('[id$=hdusuario]').val();
            var pantalla = $('[id$=hdNomPantalla]').val();
            var idpantalla = $("[id$=hdPantalla] ").val()
            var cadena = 'UPDATE PDK_CAT_TAREAS SET PDK_TAR_MODIF=GETDATE() WHERE PDK_ID_TAREAS=' + idpantalla.toString();
            var cadenaUp = 'UPDATE PDK_CAT_TAREAS SET PDK_TAR_MODIF=GETDATE() WHERE PDK_ID_TAREAS=' + idpantalla.toString();
                var txtUsu = $('[id$=txtusua]').val();
                var txtpsswor = $('[id$=txtpass]').val();
                var idpantalla = $("[id$=hdPantalla] ").val()
                var txtmotivoOb = $("[id$=TxtmotivoCan]").val()
              
                if (txtUsu == '') { alert('El campo usuario esta vacía'); return; }
                if (txtpsswor == '') { alert('El campo del password esta vacía'); return; }
                if (txtmotivoOb == '') { alert('El campo de motivo esta vacía'); return; }

                btnInserteaBoton_dos(cadena + ' ' + cadenaUp + ' ', pantalla, f, 1, u, txtUsu, txtpsswor, idpantalla, 47, 2, txtmotivoOb)            
        }
        $(document).ready(function () { $.urlParam = function (name) { var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href); if (results == null) { return null; } else { return results[1] || 0; } }; if ($.urlParam("Enable").toString() == "1") { $("#btnCancelarNew").hide(); }; });
    </script>
    <style>
        #ctl00_ctl00_cphCuerpo_cphPantallas_btnCancelar {
            display: none;
        }
    </style>
<div class="divPantConsul">
   <div class="fieldsetBBVA">
    <table>
      <tr class="tituloConsul">
       <td width="100%">
         <table width="100%">
           <tr>
             <td>
               <legend><asp:Label id = "lblNomPantalla" runat = "server" Text="Consulta de Ingresos en el Portal de Transparencia "></asp:Label></legend>                                                                       
             </td>            
           </tr>
         </table>       
       </td>
      </tr>  
    </table> 
  </div> 
 <div class="divCuerpoConsul" id = "dvCuerpo">
            <table class="fieldsetBBVA"style="width:100%"> 
                <tr>
                    <th class = "campos" style = "width:25%">
                        Solicitud:                    
                    </th>
                    <th  class = "campos"  style = "width:25%">
                        <asp:label id = "lblSolicitud" Font-Bold = "true" Font-Underline = "true" runat = "server">
                        </asp:label>
                    </th>
                    <th  class = "campos" style = "width:25%">
                        Cliente:                    
                    </th>
                    <th  class = "campos" style = "width:25%">
                        <asp:label id = "lblCliente" Font-Bold = "true" Font-Underline = "true" runat = "server">
                        </asp:label>
                    </th>
                </tr>    
                <tr>
                    <th  class = "campos" >
                        Status Credito:                    
                    </th>
                    <th  class = "campos" >
                        <asp:label id = "lblStCredito" Font-Bold = "true" Font-Underline = "true" runat = "server">
                        </asp:label>
                    </th>
                    <th  class = "campos" >
                        Status Documentos:                    
                    </th>
                    <th  class = "campos" >
                        <asp:label id = "lblStDocumento" Font-Bold = "true" Font-Underline = "true" runat = "server">
                        </asp:label>
                    </th>                    
                </tr>            
            </table>     
<br />
    <table class="resulbbvarigth">
        <tr>
            <td colspan="5" align="center">Portal de Transparencia IFAI:
             <br /><br /><asp:Button ID="btnlinkIFAI" runat="server" Text="Ir a IFAI" CssClass="buttonSecBBVA2" OnClick="btnlinkIFAI_Click" /></td>
        </tr>
        <tr><td colspan="5">&nbsp;</td></tr>
        <tr>
            <td>¿Portal de Transparencia Disponible? * </td>
            <td><asp:DropDownList ID="ddlIFAIisAvaible" runat="server" CssClass="selectBBVA" AutoPostBack="true">
                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                </asp:DropDownList></td>
            <td>&nbsp;</td>  
            <td>¿Existe Solicitante en Página? * </td>
            <td><asp:DropDownList ID="ddlExistSolIFAI" runat="server" CssClass="selectBBVA" AutoPostBack="True">
                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                </asp:DropDownList></td>           
                    
        </tr>        
        <tr>            
            <td>¿Existe Posición de trabajo (Puesto) dentro del Portal? *</td>
            <td><asp:DropDownList ID="ddlExistJobIFAI" runat="server" CssClass="selectBBVA">
                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                </asp:DropDownList></td>      
            <td>&nbsp;</td>  
            <td>¿La persona se encuentra adscrita al puesto Buscado? * </td>
            <td><asp:DropDownList ID="ddlPerJobIFAI" runat="server" CssClass="selectBBVA">
                <asp:ListItem Value="-1" Text="<SELECCIONAR>" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>            
            <td>Monto de Sueldo Registrado *</td>
            <td><asp:TextBox ID="txtMontoReg" runat="server" CssClass="txtBBVA" MaxLength="9" onkeypress="return checkDecimals(event, this.value,5)" /></td>           
            <td>&nbsp;</td>  
            <td>&nbsp;</td>  
            <td>&nbsp;</td>          
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
            <td align="right"></td>         
        </tr>
    </table>
   <br />

     
    <table id = "tbValidarObjetos" class = "resulGrid"></table>         
 </div>
</div>
        <div id="divcancela" class="cajadialogo" style="display: none; z-index: 1010 !important; position: absolute; background-color: white; top:15%; left:31%; width: 220px;">
              <%--<cc1:ModalPopupExtender ID="mpoCancela" runat="server" TargetControlID ="btnCancelar" PopupControlID="popCancela" CancelControlID="btnCancelCancela" BackgroundCssClass="modalBackground" ></cc1:ModalPopupExtender>  
              <asp:Panel ID="popCancela" runat="server" CssClass ="cajadialogo ">--%>
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
                   <td colspan="2"><textarea id="TxtmotivoCan" runat="server" onkeypress="return ValCarac(this.event, 6);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" class = "Text" rows = "5" cols = "1" style = "width:95%"></textarea> </td>
                 </tr>
                 <tr style="width:100%">
                   <td><asp:HiddenField runat="server" ID="HiddenField1" /></td>
                   <td align="left" valign="middle">
                     <input id="btnGuardarCancelar" type="button" runat="server" value="Guardar" class="Text " onclick="btnGuardar(id)"  /> 
                       <br />
                     <asp:Button ID="btnCancelCancela" runat="server" Text="Cancelar" SkinID="btnGeneral"/>              
                   </td>
                 </tr>
               
               </table> 
              <%--</asp:Panel>  --%>          
            </div>
    <div class="resulbbvaCenter divAdminCatPie" >
        <table width="100%" style="height:100%;">
            <tr>
                <td align="right" valign="middle" >
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="buttonSecBBVA2" OnClick="btnRegresar_Click"/>
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="buttonSecBBVA2" OnClick="btnLimpiar_Click" />
                    <asp:Button ID="cmbguardar1" runat="server" Text="Procesar" CssClass="buttonBBVA2" OnClick="cmbguardar1_Click"/>   
                    <asp:Button runat="server" ID ="btnCancelar" Text="Cancelar" OnClientClick="mostrarCanceDiv()" CssClass="buttonSecBBVA2"/>            
                    <input type="button" value="Cancelar" class="buttonSecBBVA2" onclick="mostrarCanceDiv();" id="btnCancelarNew" />            
                </td>
            </tr>
        </table>        
     </div>       
    <asp:HiddenField ID = "hdPantalla" runat = "server" />
    <asp:HiddenField ID = "hdSolicitud" runat = "server" />
    <asp:HiddenField ID = "hdusuario" runat = "server" /> 
    <asp:HiddenField ID="hdnResultado" runat="server" />
    <asp:HiddenField ID="hdRutaEntrada" runat="server" /> 
</asp:Content>
