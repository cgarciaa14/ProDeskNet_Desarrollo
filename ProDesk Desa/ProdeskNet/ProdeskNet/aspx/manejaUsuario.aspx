<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="manejaUsuario.aspx.vb" Inherits="manejaUsuario" %>
<%--<%@ Register Assembly ="AjaxControlToolkit" Namespace ="AjaxControlToolkit" TagPrefix ="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>

<%--BBV-P-412:AVH:18/07/2016 RQ B: SE OCULTA EL BOTON GUARDAR YA QUE EL ALTA SE REALIZA DESDE PROCOTIZA--%>
<%--BUG-PD-107: ERODRIGUEZ: 19/06/2017 Se oculto el boton resetear contrazeña--%>
<%--BUG-PD-176: ERODRIGUEZ: 26/07/2017 Se habilito el boton guardar--%>
<%--BUG-PD-309: 28/12/2017: DJUAREZ: Se modifican los estilos de la pagina para homologarlos con los que ya se tienen.--%>
<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">
<script type ="text/javascript" language ="javascript" >
    $(document).ready(function () {
        $('#search').keyup(function () {
            searchTable($(this).val(), $("[id$=grvDistribu]"));
        });
    });

    function funCheTodo(check) {
        var checkdis = $('[id$=chkDistribuir]')
        if (check.checked == true) {
            $.each(checkdis, function () {
                $(this).prop('checked', true)
            });
        } else { $.each(checkdis, function () { $(this).prop('checked', false) }); }


    }
    function funResetear() {
        var datos = $('[id$=hdhEncrip]').val();
        var mostra = $('[id$=hdnResetiar]').val();
        var usu =$('[id$=hdnIdRegistro]').val();
        $('[id$=inpContras]').val(mostra);
        btnInsUpd("UPDATE PDK_USUARIO SET PDK_USU_CONTRASENA='" + datos + "', PDK_USU_ULTINGRESO = getdate() WHERE PDK_ID_USUARIO<>1 and PDK_ID_USUARIO=" + usu)

        dvPrin = $('#divAdminCatCuerpo');
        dvSol = $('#divMostra');
        dvSol.draggable();
////        centraVentana(dvSol, dvPrin);
        dvSol.show('slide', options, 1000, '');

    }
    function escoder() {
        dvPrin = $('#divAdminCatCuerpo');
        dvsol = $('#divMostra');
//////        ocultaVentana(dvsol, dvPrin);
        dvSol.hide("puff", options, 10, '');
        
    }

</script>
   <div class="divAdminCat">
     <div class ="divFiltrosConsul">
      <table>
        <tr>
          <td class ="tituloConsul ">Usuarios</td>
        </tr>
      </table>
     </div>
     <div class ="divAdminCatCuerpo">
      <div style="position:absolute; top:0%; left:0%; width:100%;">
        <table width="100%"  >
        <tr>
             <td style="width:15%"><label id ="lblPerfil" style="font-size: 8pt;font-family: Arial;color: #666666;">Perfil:</label></td>
             <td style="width:30%"><asp:DropDownList ID="cmbPerfil" runat="server" Width="80%" CssClass="selectBBVA"></asp:DropDownList></td>
              <td style="width:15%"><label style="font-size: 8pt;font-family: Arial;color: #666666;">Clave Usuario:</label></td>

             <td style="width:30%"><asp:Label ID="lblID" runat ="server" Width="80%"   SkinID="lblGeneral"></asp:Label></td>
           </tr>
           <tr>
             <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">* Nombre:</label></td>
             <td><asp:TextBox runat ="server" SkinID="txtAlfaMayGde1" Width="100%" ID="txtNombre"></asp:TextBox></td>
              <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Apellido Paterno:</label></td>   
             <td><asp:TextBox runat ="server" SkinID ="txtAlfaMayGde"   ID="txtPaterno"></asp:TextBox></td>
           </tr>
          <tr>
             <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Apellido Materno:</label></td>
             <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde"  ID="txtMaterno"></asp:TextBox></td>
             <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Usuario:</label></td>
             <td><asp:TextBox runat ="server" SkinID="txtAlfaMayGde1" MaxLength ="20"   ID="txtUsuario"></asp:TextBox></td> 
             
           </tr>
           <tr>
             <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Correo Electronico:</label></td>
             <td><asp:TextBox ID="txtcorreo" runat="server" SkinID="txtMailGde" ></asp:TextBox>   </td>
              <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Activo:</label></td>
             <td><asp:CheckBox runat="server" CssClass="resul" ID="chkStatus" /></td>
               <%--Se oculto el boton resetear contrazeña--%>
            <td style="width:15%; visibility:collapse"><input id="btnRese" type="button"  class="Text" value="Resetear Contraseña" onclick="funResetear()"  /></td>
           </tr>
         
             <tr>
               <td ><label style="font-size: 8pt;font-family: Arial;color: #666666;">Buscar:</label></td> 
               <td colspan ="4"><input type="text" class="resul" style="width:100%"   onkeypress ="ManejaCar('A',1,this.value,this);" id="search"/>  </td>  
               <td></td>
             </tr>

           
        </table>
      </div>
      <div style="position :absolute; top:35%; width:100%; height:70%;   overflow:auto;" >
            <asp:GridView ID="grvDistribu" runat ="server" AutoGenerateColumns="False" 
              Width="100%" PagerStyle-HorizontalAlign="Right" 
              HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AllowSorting="True" >
              <Columns >
              <asp:BoundField DataField="PDK_ID_DISTRIBUIDOR" ItemStyle-CssClass="oculta" HeaderStyle-CssClass ="oculta " HeaderStyle-Width="10%" HeaderText="ID" FooterStyle-CssClass="oculta" />
              <asp:BoundField DataField="PDK_DIST_CLAVE" ItemStyle-CssClass ="resul" HeaderStyle-Width="10%" HeaderText="Clave" ItemStyle-HorizontalAlign="Center"/>
              <asp:BoundField DataField="PDK_DIST_NOMBRE" ItemStyle-CssClass="resul"  HeaderStyle-Width="60%" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center"/>
              <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                 <HeaderTemplate>
                   <%--<asp:CheckBox ID="chkTodos" runat="server" Text="Todos" OnCheckedChanged="chkTodos_CheckedChanged" AutoPostBack="true" />--%>
                   Todos<input id="chkTodos" type="checkbox" onclick="funCheTodo(this)" /> 
                 </HeaderTemplate>
                 <ItemTemplate>
                  <asp:CheckBox ID="chkDistribuir" runat ="server"  /> 
                 </ItemTemplate> 
              </asp:TemplateField>
              </Columns>
            
            </asp:GridView> 
    
      </div>
      <div id="divMostra" class="dvPequeContraseña"   >
        <table width="100%"  >
         <tr>
           <th style = "text-align: right">
             <label id = "lblX" onclick = "escoder();" class = "link">  X  </label>
           </th>
        </tr>
        </table>
         <div>
        <table style="text-align:center;"> 
           <tr>
            <td colspan="2" ></td>
             <td  class="cssLetras">Contraseña es: </td>
            </tr>
            <tr>
            <td colspan="2" ></td>
             <td   ><input id="inpContras" type="text" disabled="disabled" /> </td> 
           </tr>
        </table>
        </div> 
      
      </div>
     </div>
      <div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td align="center" valign="middle">
                        <asp:Button runat="server" id="btnRegresar" text="Regresar" CssClass="buttonBBVA2" />
                        <asp:Button runat="server" id="btnGuardar" text="Guardar" CssClass="buttonBBVA2" />
                    </td>
                </tr>
            </table>
        </div>
   </div>
   <asp:HiddenField runat="server" ID="hdnIdRegistro" />
  <asp:HiddenField runat="server" ID="hdnResetiar" />
  <asp:HiddenField runat="server" ID="hdhEncrip" />  
</asp:Content>
