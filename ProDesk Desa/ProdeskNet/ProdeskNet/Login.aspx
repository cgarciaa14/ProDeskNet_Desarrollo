<%@ Page Language="VB" MasterPageFile="~/Principal.Master"  AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" Title="PRODESKNET 3.0" %>
<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphCuerpo">
    <script language = "javascript" type = "text/javascript" src = "js/jquery.js"></script>
<script language = "javascript" type = "text/javascript" src = "js/jquery-ui.js"></script>
<script language = "javascript" type = "text/javascript">

    var options = {};

    function centraVentana(muestra, base) {
        //Obtenemos ancho y alto del navegador, y alto y ancho de la popUp

        if (base != undefined) {
            var windowWidth = base.width();
            var windowHeight = base.height();
        }
        else {
            var windowWidth = screen.width;
            var windowHeight = screen.height;
        }


        var popupWidth = muestra.width();
        var popupHeight = muestra.height();
        //centering
        muestra.css({ "position": "absolute", "top": (windowHeight / 2) - (popupHeight / 2) + "px", "left": (windowWidth / 2) - (popupWidth / 2) + "px" });
    }

    function PopUpLetreroLogin(mesaje) {
        $("[id$=lblMensaje]").text(mesaje);
        centraVentana($('#ventanaconfirmlog'));
        $('#dvLogo').hide('fold', options, 1500);
        $('#dvLogin').hide('fold', options, 1500);
        $('#ventanaconfirmlog').show('blind', options, 2500, '');
        $('#ventanaconfirmlog').hide('blind', options, 2500, '');
        $('#dvLogo').show('fold', options, 4000);
        $('#dvLogin').show('fold', options, 4000);
    }

    top.window.moveTo(0, 0);
    if (document.all) {
        top.window.resizeTo(screen.availWidth, screen.availHeight);
    }
    else if (document.layers || document.getElementById) {
        if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
            top.window.outerHeight = screen.availHeight;
            top.window.outerWidth = screen.availWidth;
        }
    }

</script>

<div id = "dvLogo" style = "position:absolute; top:0px; left:0px; width:50%; height:100%; text-align:center; padding:3%; ">
    <img id="imgLogo" src="App_Themes/Imagenes/logo_incredi3.png"  alt="incredit" style = "background-position:center center;"/>
</div>

<div id = "dvLogin" style = "position:absolute; top:40%; left:50%; width:40%; height:20%; border-radius:15px; box-shadow: 10px 10px 5px #888888; border-color: #5089c3; text-align:center;">
   <table width="98%" cellpadding = "8px" cellspacing = "4px" >
      <tr>
        <td  style="color:#434E87; font-family:Verdana, Helvetica, Arial, sans-serif; font-size:9pt; font-weight:bolder;">Usuario:</td>
         <td align="right">
            <asp:TextBox ID="txtUsu" runat="server" SkinID="txtGeneral" MaxLength="12"></asp:TextBox>
         </td>
      </tr>
      <tr>
         <td style="color:#434E87; font-family:Verdana, Helvetica, Arial, sans-serif; font-size:9pt; font-weight:bolder;">Contraseña:</td>
         <td align="right">
            <asp:TextBox ID="txtPwd" runat="server" SkinID="txtGeneral" TextMode="Password" MaxLength="12" EnableTheming="true" >
         </asp:TextBox></td>
      </tr>
      <tr>
        <td colspan="2" align="right">         
          <asp:Button runat="server" ID="btnAceptar" Text="Entrar" SkinID="btbGeneral" OnClick="btnAceptar_Click"  />
        </td>
      </tr>
   </table>
</div>

   <asp:HiddenField runat ="server" ID="usuAcceso" Value ="" />
   <asp:HiddenField runat="server" ID ="intentosAcceso" Value="0" />


</asp:Content>