<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImprimirSolicitud.aspx.vb" Inherits="aspx_ImprimirSolicitud" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
            
      	 	
   

              <script src="../js/jquery.js" type="text/javascript"></script>
            <script type="text/javascript" src="../js/jquery.xslt.js"></script>
           
        <script type="text/javascript" language="javascript">

            //            $(function () {
            //                var data;
            //                var pantalla = $("[id$=hdnPantalla]")
            //                var solicitud = $("[id$=hdnFolio]")
            //                var cadena;
            //      

            //                filltxt('XML', 'hdnPantalla,hdnFolio');
            //                filltxt('XSL', 'hdnPantalla,hdnFolio');

            //               

            //                ////                $.ajax({
            //                ////                    url: 'xslt-test.xml',
            //                ////                    dataType: 'html',
            //                ////                    success: function (dataXML) {
            //                ////                        $('#xmldata').text(dataXML);
            //                ////                    }
            //                ////                });

            //                ////                $.ajax({
            //                ////                    url: 'xslt-test.xsl',
            //                ////                    dataType: 'html',
            //                ////                    success: function (dataXSL) {
            //                ////                        $('#xsldata').text(dataXSL);
            //                ////                    }
            //                ////                });


            //
            //            });


            function fnTransformaXSL() {
                var dataXML = $("[id$=hdnXML]").val();
                var dataXSL = $("[id$=hdnXSL]").val();
                alert(dataXML);
                alert(dataXSL);
                $('#output').xslt({ xml: dataXML, xsl: dataXSL });
            }

            function fnForzap() {

                location.reload()
            }

            $(document).ready(function () {
                var dataXML = $("[id$=hdnXML]").val();
                var dataXSL = $("[id$=hdnXSL]").val();
                $('#output').xslt({ xml: dataXML, xsl: dataXSL });
                var entrar = $("[id$=txtentrada]").val();

                //                if (entrar==2) {
                //                    window.setInterval("reFresh()", 30);
                //                    entrar=3 
                //                }


            });

            function reFresh() {
                location.reload(true)
                ////               $("[id$=txtentrada]").val(2);


            }
            /* Establece el tiempo 1 minuto = 60000 milliseconds. */
            ////          window.setInterval("reFresh()",300000);





        </script> 
<head id="Head1" runat="server">
    <title></title>


</head>


<body class="bodyBC" >

    <form id="form1" runat="server">
    <asp:ScriptManager runat ="server" ID="ScriptManager"></asp:ScriptManager>
<div style="height: 80%; border: 1px solid #000000; overflow: scroll;"> 
<div id="output" style="padding: 2px;"> 
</div> 
</div> 
<div>
  <input id="txtentrada" type="text" runat="server" value="1"  style ="display:none" /> 
</div>

<%--     <div>
      <input id="txtXML" type="text" />
      <input id="txtXSL"  type="text" />
      <input id="btnBoton"  type="button" onclick="funButon()"  />  
     
    </div>--%>

    <asp:HiddenField runat="server" ID="hdnFolio" />
    <asp:HiddenField runat="server" ID="hdnPantalla" />
    <asp:HiddenField runat="server" ID="hdnXML" />
    <asp:HiddenField runat="server" ID="hdnXSL" />
    <asp:HiddenField runat="server" ID="hdnUsuar" />   

    </form>
</body>
</html>
