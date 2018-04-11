<%@ Page Language="vb" MasterPageFile="~/aspx/Home.Master"  AutoEventWireup="false" CodeFile="CaratulaAutorizacion.aspx.vb" Inherits="CaratulaAutorizacion" %>
<%--<%@ Register Assembly ="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix ="cc1"   %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master"    %>

<asp:Content ID="content1" ContentPlaceHolderID="cphPantallas" runat ="server" >

    <!--  YAM-P-208  egonzalez 12/08/2015 Se agregaron estilos para la implementación del iframe de coacreditado -->
    <!-- BBV-P-423: RQSOL-04: AVH: 06/12/2016 Editar el Estado de la Tarea-->
    <!-- BBV-P-423: RQSOL-04: ERODRIGUEZ: 17/05/2017 Modificacion para enviar numero de solicitud para la caja de mensajes-->
	<!-- BBV-P-423: BUG-PD-60: ERODRIGUEZ: 22/05/2017 Mensaje de error al introducir caracteres no validos ó números demasiado grandes en la busqueda por solicitud-->
	<!-- BBV-P-423: BUG-PD-62: ERODRIGUEZ: 24/05/2017 Permite marcar mensajes como leidos y vista de mensajes sin leer-->
    <!-- BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit-->

    <script language="javascript" type="text/javascript" >

        $(function () {
            $("#tabs").tabs();            
            $("[id$='inSolicitud']").autocomplete({
                source: $('[id$="hdACNombre"]').val().split(',')
            });
            $("#divsol").css("display", "none");
            fnOcultaObjetos(document.location.href.match(/[^\/]+$/)[0], $('[id$="hdPerfilUsuario"]').val());
        })
      function fnBuscaSoli() {
          //fillAutocomplete('tags', 'lblNomUsuario');
          $("#divsol").css("display", "none");

      }
      function finOculta() {
          $("#divsol").css("display", "");
      }

      function CargaMensajes() {
         
      }

</script>  

<div class ="divAdminCat">
 <div class="divAdminCatTitulo">
  <table>
    <tr>
     <td  class="tituloConsul">Caratula de Autorización</td>      
    </tr>
       
  </table> 
  <div id = "dvBuscaCliente" style = "position:absolute; top:10%; height:5%; left:50%; width:60%; background-color:#F3F7FB; ">
    <table width="100%">
      <tr>
        <td class="campos" style="width:5%;" align="right" valign="middle">Solicitud:</td>
        <td style="width:20%;"  valign="middle" >
          <input id="inSolicitud" maxlength="19" style="width:80%;" runat="server"  />
        </td> 
        <td style="width:10%;" align="left"  >
          <asp:ImageButton ID = "btnBuscarCliente" runat = "server" ImageUrl = "~/App_Themes/Imagenes/search.png" />                
        </td>
       </tr>     
     </table> 
     </div>
 </div>

 <div class="divAdminCatCuerpo">
 <div id="divsol"  >
   <table width="100%">
     <tr>
        <td style="width:5%;"  class="campos">Solicitud:</td>
       <td style="width:25%;"><asp:Label ID="lblsolicitud" runat="server"></asp:Label> </td>
       <td style="width:5%;" class="campos">Nombre:</td>
       <td style="width:35%;"><asp:Label ID="lblnombresol" runat="server"></asp:Label> </td>
       <td style="width:5%;" class="campos">Fecha:</td>
       <td style="width:25%;"><asp:Label ID="lblfechasol" runat="server" ></asp:Label> </td>
     </tr>
   </table>
 </div>
           

<div id="tabs" style="top:0;left:0;width:100%;min-height:100%;overflow:auto;">
<ul>
    <li id ="CajaNotas"><a href ="#tabs-1">Caja de Notas</a></li>
   <%-- <li id ="Cotizacion"><a href ="#tabs-1">Cotización</a></li>--%>
    <li id ="Buro"><a href ="#tabs-2">Buro</a> </li>
    <%--<li><a href ="#tabs-3">Solicitud</a> </li>--%>
    <li id ="Documentacion"><a href ="#tabs-4">Documentación</a></li>
    <li id ="ActualizarSolicitud"><a href ="#tabs-5">Actualizar Solicitud</a></li>
    <li id ="ActualizarFactura"><a href ="#tabs-6">Factura</a></li>
    <li id ="EditarEstadoTarea"><a href ="#tabs-7">Editar el Estado de la Tarea</a></li>
    <%--<li id ="CajaNotas"><a href ="#tabs-8">Caja de Notas</a></li>--%>
     <li id ="Cotizacion"><a href ="#tabs-8">Cotización</a></li>
</ul>
    <div id="tabs-8">
 <table width="100%">
   <tr>
     <td class="campos">Num Cotización: <asp:Label ID="lblNumCotiza" runat="server" ></asp:Label> </td>
    <td class ="campos">Unidad: <asp:Label ID="lblUnidad" runat="server" ></asp:Label> </td>
    <td class ="campos">Paquete: <asp:Label ID="lblPaquete" runat="server" ></asp:Label> </td>
   </tr>
   <tr>
   <td colspan ="7">
   <table width="100%">
     <tr>
      <td>
        <div style="height:350px; width:100%;  overflow:auto">
         <asp:GridView ID="grvCaratula" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="encabezados" Width="100%" PagerStyle-HorizontalAlign="Right" CssClass="resulGrid" AllowSorting="True"   >
          <Columns>
               <asp:BoundField DataField="No." ItemStyle-CssClass="resul" HeaderText="No." />
               <asp:BoundField DataField ="Saldo Capital" ItemStyle-CssClass ="resul" HeaderText="Saldo Capital" />
               <asp:BoundField DataField="Amortizacion Capital" ItemStyle-CssClass="resul" HeaderText="Amortizacion Capital"/>
               <asp:BoundField DataField="Interes" ItemStyle-CssClass="resul" HeaderText ="Interes"/>
               <asp:BoundField DataField="IVA" ItemStyle-CssClass="resul" HeaderText ="IVA"/>
               <asp:BoundField DataField="Pago Mensual" ItemStyle-CssClass ="resul" headerText="Pago Mensual"/>
               <asp:BoundField DataField="Pago Mensual Seguro" ItemStyle-CssClass="resul" HeaderText ="Pago Mensual Seguro"/>
               <asp:BoundField DataField="Pago Mensual Total" ItemStyle-CssClass="resul" HeaderText ="Pago Mensual Total"/>      
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
        <table width="100%" style="border-style:ridge; border-color:#808080; border-width:2px;"    >
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
             <td style="width:25%" class="campos">Capacidad de Pago:</td>
             <td style="width:25%"><asp:Label ID="lblCapacidadPago" runat="server"></asp:Label></td>
             <td style="width:25%" class="campos">Ratio:</td>
             <td style="width:25%"><asp:Label ID="lblration" runat="server" ></asp:Label></td>           
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
    <%--<div id="tabs-3">
  <div>
    <table width="100%">
      <tr>
        <td class="tituloConsul"  >DATOS PERSONALES </td>
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
       <td class="tituloConsul" ">EMPLEO</td>       
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
</div>--%>
    <div id="tabs-4">
  <asp:GridView ID="grvDocumento" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="encabezados" Width="100%" PagerStyle-HorizontalAlign="Right" CssClass="resulGrid" AllowSorting="True" >
   <Columns>
     <asp:BoundField DataField="PDk_ID_DOCUMENTOS" ItemStyle-CssClass ="resul" HeaderText ="Clave" />
     <asp:HyperLinkField DataTextField="PDK_DOC_NOMBRE" Target ="_blank"  DataNavigateUrlFields ="URL" ItemStyle-CssClass="resul " HeaderText="Nombre" /> 
     <asp:BoundField DataField="PDK_ST_ENTREGADO" ItemStyle-CssClass ="resul" HeaderText ="Entregado" /> 
     <asp:BoundField  DataField="PDK_ST_VALIDADO" ItemStyle-CssClass="resul" HeaderText="Validado" />
     <asp:BoundField DataField="PDK_ST_RECHAZADO" ItemStyle-CssClass="resul" HeaderText="Rechazado" />   
   
   </Columns> 
  </asp:GridView>
  
 
 </div>
    <div id ="tabs-5" style ="top:10%;left:0;width:96%;height:80%;position:absolute;">      
        <iframe id ="ifSol" style ="width:100%; height:100%;" runat ="server"></iframe>  
        <iframe id ="ifsol2" style ="width:100%; height:100%;" runat ="server"></iframe>
    </div>
    <div id ="tabs-6" style ="top:10%;left:0;width:96%;height:80%;position:absolute;">      
        <iframe id ="ifFact" style ="width:100%; height:100%;" runat ="server"></iframe>
    </div>
    <div id ="tabs-7" style ="top:10%;left:0;width:96%;height:80%;position:absolute;">      
        <iframe id ="MTareas" style ="width:100%; height:100%;" runat ="server"></iframe>
    </div>
    
    <div id ="tabs-1" style ="top:10%;left:0;width:96%;height:80%;position:absolute;">      
        <iframe id ="CNotas" onclick="CargaMensajes()" src="CajaNotas.aspx" style ="width:100%; height:100%;" runat ="server"></iframe>
    </div>
</div>  
</div>


</div> 
<asp:HiddenField ID="hndtexto" runat ="server" />
<asp:HiddenField ID="hdnFolio" runat ="server" />
<asp:Label ID="lblNomUsuario" runat="server" style="display:none;"  ></asp:Label> 
</asp:Content> 