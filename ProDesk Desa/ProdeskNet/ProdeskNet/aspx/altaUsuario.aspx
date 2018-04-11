<%--BUG-PD-364 GVARGAS 21/02/2018 Correccion panel avoid Ajax Tool Kit--%>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="altaUsuario.aspx.vb" Inherits="altaUsuario" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix ="cc1" %>--%>
<%@ MasterType VirtualPath="~/aspx/Home.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">
<script type="text/javascript" language ="javascript" >
    $(document).ready(function () {
        $('#search').keyup(function () {
            searchTable($(this).val(), $("[id$=grvDistribu]"));
        });

        var ApPaterno = $('[id$=txtPaterno]');
        var Nombre = $('[id$=txtNombre]');
        var Usuario = $('[id$=txtUsuario]');

        /*  JDRA:Genera usuario    */

        ApPaterno.keypress(function (e) {
            letraInicial = String.fromCharCode(e.which);
            if (ApPaterno.val().length == 0) {
                Usuario.val(generaUsuario(Nombre.val(), letraInicial)); //
            }
            else {
                Usuario.val(generaUsuario(Nombre.val(), ApPaterno.val() + letraInicial));//
            }
        });
        Nombre.keypress(function (e) {
            letraInicial = String.fromCharCode(e.which);
            if (Nombre.val().length == 0) {
                Usuario.val(generaUsuario(letraInicial, ApPaterno.val())); //
            }
            else {
                Usuario.val(generaUsuario(Nombre.val(), ApPaterno.val()));//
            }            
        });

        /*  JDRA:Genera usuario    */

    });

    function funCheTodo(check) {
        var checkdis = $('[id$=chkDistribuir]')
        if (check.checked == true) {
            $.each(checkdis, function () {
                $(this).prop('checked',true)});
        }
        else {
            $.each(checkdis, function () { $(this).prop('checked', false) });
        }          
    }

    /*  JDRA:Genera usuario    */

    function generaUsuario(nombre, apellido) {
        var regresaUsuario;
        if (nombre == undefined) {
            regresaUsuario = apellido.substring(0, 7);
        }

        else if (apellido == undefined) {
            regresaUsuario = nombre.substring(0, 1);
        }

        else {
            regresaUsuario = nombre.substring(0, 1) + apellido.substring(0, 7);
        }

        return regresaUsuario;
    }

    /*  JDRA:Genera usuario    */

</script> 
    <div class="divAdminCat">
    <div class="divAdminCatTitulo">
       <table>
         <tr>
           <td class="tituloConsul">Usuarios</td>
         </tr>
       </table>
    </div>
    <div class="divAdminCatCuerpo">
      <div style ="position:absolute; top:0%; left:0%; width:100%;" >
         <table>
          <tr>
             <td class="campos" style="width:20%">Perfil:</td>
             <td style="width:30%"><asp:DropDownList runat="server" ID="cmbPerfil" Width="195px" SkinID="cmbGeneral"></asp:DropDownList> </td>

              <td class ="campos" style="width:20%" >Clave Usuario:</td>
             <td style="width:30%"><asp:Label runat ="server" Width="150px" SkinID="lblCampos" ID="lblCveUsu"></asp:Label></td>
            
          </tr>
           <tr>
             <td class="campos" >*Nombre:</td>
             <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" Width="150px" ID ="txtNombre"></asp:TextBox> </td>
            
             <td class="campos" >Apellido Paterno:</td>
              <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" Width="150px" ID="txtPaterno"></asp:TextBox></td>
           </tr>
 
          <tr>
               <td class="campos">Apellido Materno:</td>
              <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde" Width="150px" ID="txtMaterno"></asp:TextBox></td> 
              
               <td class="campos" >*Usuario:</td>
             <td><asp:TextBox runat ="server" SkinID="txtAlfaMayGde1"  MaxLength="20"  Width="150px" ID="txtUsuario"></asp:TextBox> </td> 

          </tr>
      
           <tr>
     
             <td class="campos" >*Contraseña:</td>
             <td><asp:TextBox runat="server" SkinID="txtAlfaMayGde1" MaxLength="10"  Width="150px" ID="txtContraseña" TextMode ="Password" ></asp:TextBox> </td>
      
             <td class="campos">Activo:</td>
             <td> <asp:CheckBox runat="server" ID="chkActivo" SkinID="cmbGeneral" /></td>     
           </tr>                
           <tr>
             <td class="campos">Correo Electronico:</td>
             <td><asp:TextBox ID="txtcorreo" runat="server" SkinID="txtMailGde" Width="150px"></asp:TextBox>   </td>
           </tr>
           <tr>
               <td class="campos" >Buscar:</td> 
               <td colspan ="4"><input type="text" class="resul" style="width:100%"   onkeypress ="ManejaCar('A',1,this.value,this);" id="search"/>  </td>  
               
             </tr>
           
           </table>
      </div> 


      <div style="position :absolute; top:35%; width:100%; height:50%;   overflow:auto;" >
         
     
        <asp:GridView ID="grvDistribu" runat="server" AutoGenerateColumns="False" 
              HeaderStyle-CssClass="encabezados"      
              Width="100%" PagerStyle-HorizontalAlign="Right" CssClass="resulGrid" 
              AllowSorting="True" >
              <Columns >
              <asp:BoundField DataField="PDK_ID_DISTRIBUIDOR" ItemStyle-CssClass="oculta" HeaderStyle-CssClass ="oculta " HeaderStyle-Width="10%" HeaderText="ID" FooterStyle-CssClass="oculta" />
              <asp:BoundField DataField="PDK_DIST_CLAVE" ItemStyle-CssClass ="resul" HeaderStyle-Width="10%" HeaderText="Clave" />
              <asp:BoundField DataField="PDK_DIST_NOMBRE" ItemStyle-CssClass="resul"  HeaderStyle-Width="60%" HeaderText="Nombre" />
              <asp:TemplateField HeaderStyle-Width="10%">
                 <HeaderTemplate>
                   <%--<asp:CheckBox ID="chkTodos" runat="server" Text="Todos" OnCheckedChanged="chkTodos_CheckedChanged" AutoPostBack="true" />--%>
                   Todos<input  id="checkTodo"   type="checkbox" onclick="funCheTodo(this)"  />    
                 </HeaderTemplate>
                 <ItemTemplate>
                  <asp:CheckBox ID="chkDistribuir" runat ="server"  /> 
                 </ItemTemplate> 
                 <ItemStyle CssClass="itemCentrar" />  
              </asp:TemplateField>
              </Columns>
        </asp:GridView>
      </div>
    </div>
    <div class="divAdminCatPie">
            <table width="100%" style="height:100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" id="btnRegresar" text="Regresar" SkinID="btnGeneral" />
                         <asp:Button runat="server" id="btnGuardar" text="Guardar" SkinID="btnGeneral" />
                    </td>
                </tr>
            </table>
        </div>
  </div>

</asp:Content>
