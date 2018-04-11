<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaPanelControl_OLD.aspx.vb" Inherits="consultaPanelControl" %>

<%@ MasterType VirtualPath="~/aspx/Home.Master" %>
<%@ Register TagPrefix="pie" TagName="StatusColor" Src="~/aspx/StatusColor.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">

    <%--Tracker:INC-B-2019:JDRA:Regresar--%>
    <!--  YAM-P-208  egonzalez 08/09/2015   Se agregó un campo oculto para la permanencia de la paginación y poder llamar al store para generar los registros siguientes -->
    <%--BBV-P-423: RQSOL-03: AVH: 10/11/2016 SE MUESTRA TABLA DE COLORES Y BOTON BUSCAR--%>
    <%--BBV-P-423: RQ H - CSS JRHM 09/12/2016 Se modificaron clases de estilos para asemejarce a el cotizador--%>
    <%--BUG-PD-03: GVARGAS: 05/01/2017: Correcciones pantalla Precalificacion.--%>
    <%--BUG-PD-04: GVARGAS: 11/01/2017: Correccion formato de fechas.--%>
    <%--BUG-PD-09: GVARGAS: 15/02/2017: Cambios combos alta solicitud.--%>
    <%--BUG-PD-15: JBB: 27/02/2017: Se agrega validación al no tener resultado en la busquedad.--%>
    <%--BUG-PD-13  GVARGAS  28/02/2017: Cambios Datepicker.--%>		  
    <%--BUG-PD-23 29/03/2017 MAPH Cambios en la búsqueda para utilizarla con o sin autocompletar.--%>
	<%--BUG-PD-39  erodriguez 26/04/2017 Cambios de usabilidad y estilos--%>
    <script type="text/javascript" src="../js/Funciones.js"></script>


<script type="text/javascript">

    function fndll() {
        $("[id$='hdnPaginacion']").val(1)        
        fillgv('grvConsulta', 'hdnClienteNo,ddlEmpresa,ddlProducto,ddlTPersona,hdnPaginacion');
    }

    function fnPag(id) {
        $("[id$='hdnPaginacion']").val(id)        
        fillgv('grvConsulta', 'hdnClienteNo,ddlEmpresa,ddlProducto,ddlTPersona,hdnPaginacion');        
    }

    function fnReasigna(obj) {
        datosR = obj.id.replace('lblReasigna', '').split("-");
        obj.outerHTML = obj.outerHTML + '<input type = "text" id = "txtUsuAsign" onblur = "fnReasigUpd(this, ' + datosR[0] + ',' + datosR[1] + ')"/>'
        $('#' + obj.id).hide('fast', '');
        fillAutocomplete('txtUsuAsign', 'lblNomUsu,lbldistri' + datosR[0] + '-' + datosR[1]);        
    }

    function fnReasigUpd(obj, solicitudRU, usuarioRU) {
        usuarioAsig = obj.value.split('.');
        if (obj.value == '') {
            $('#lblReasigna' + solicitudRU + '-' + usuarioRU).show('fast', '');
            obj.outerHTML = '';
            return;
        }
        if (usuarioAsig.length < 2) {
            $('#lblReasigna' + solicitudRU + '-' + usuarioRU).show('fast', '');
            obj.outerHTML = '';
            return;
        }
        cadena = 'update PDK_OPE_SOLICITUD set PDK_OPE_USU_ASIGNADO = ' + usuarioAsig[0] + ' where pdk_id_solicitud = ' + solicitudRU + 'and PDK_OPE_USU_ASIGNADO = ' + usuarioRU
        btnInsUpdReload(cadena, 'grvConsulta', 'hdnClienteNo,ddlEmpresa,ddlProducto,ddlTPersona', '', '');
    }

    function fnDistribuidor(idl, texto) {
        debugger;
        var divDistri = $('#divDistri');
        var etiqueta = $('#' + idl).offset();
        altoB = screen.height * .13;
        anchoB = screen.width * .15;
        anchoL = $('#' + idl).width() + 5;
        altoL = $('#' + idl).height()/2;
        $('#divDistri').css('top', etiqueta.top - altoB + altoL);
        $('#divDistri').css('left', etiqueta.left - anchoB + anchoL);
        $('#lblnombre').text(texto);
        divDistri.show(1000);
    }

    function fnDisOculta() {        
        var divDistri = $('#divDistri');
        divDistri.hide(1000);
    }

    function filltbTareas(solicitud, procesos, usuario) {
        dvPrin = $('#Principal');
        dvPTar = $('#dvTareas');
        $('#inSol').val(solicitud);
        $('#inPro').val(procesos);
        $('#inUsu').val(usuario);
        $('#tbTareas')[0].innerHTML = '';
        fillgv('tbTareas', 'inSol, inPro, inUsu');
        centraVentana(dvPTar, dvPrin);
        muestraVentana(dvPrin, dvPTar);
        dvPTar.draggable();
    }


        var autocompletePanelControl = true;
    function btnBuscarCliente() {
        fnMuestraBuscar();
            //BUG-PD-23 29/03/2017 MAPH Cambios para realizar la búsqueda con o sin autocompletar
            if (autocompletePanelControl) {
        fillgv('grvConsulta', 'hdnCriterio, hdnClienteNo, hdnParametro1, hdnParametro2,ddlEmpresa,ddlProducto,ddlTPersona');
            }
            else {
                fillgv('grvConsulta', 'hdnCriterio, hdnClienteNo, hdnNombreCliente, hdnParametro2,ddlEmpresa,ddlProducto,ddlTPersona');
            }

            //var tabla = document.getElementsByClassName('resulGridPag').rows.length;
            //var tabla = $(".resulGridPag");

            var tabla2 = document.getElementsByClassName('resulGridPag')[0].rows.length;

            if (tabla2 == 1) {
                PopUpLetrero("No se encontró información con los parámetros proporcionados.");

            } 


            //var td = tabla.getElementsByTagName('TD');

            //var i = td.length;

             
           
           
        
               
        }
       
    

    function muestraAgregar() {
        dvPrin = $('#Principal');
        dvSol = $('#dvAgregarSolicitud');
        dvSol.draggable();
        centraVentana(dvSol);
        dvSol.show('slide', options, 1000, '');
    }

    function escondeAgregar() {
        dvPrin = $('#Principal');
        dvSol = $('#dvAgregarSolicitud');
        ocultaVentanaFast(dvPrin, dvSol);
    }

    function fnCargaPantalla() {
        fnMuestraBuscar();

        if ($("[id$=hdnParametro1]").val() != "") {
            /*  cuando existe valor en el parametro1 solicitud, se realiza la carga por solicitud   */
            $("[id$=hdnCriterio]").val("rdSolCli");

            
            //regresa a la ultima solicitud
            fillgv('grvConsulta', 'hdnCriterio, hdnClienteNo, hdnParametro1, hdnParametro2,hdnEmpresa,hdnProducto,hdnPersona');
            

            //$('[id$=hdnPaginacion]').val("1");
            //fillgv('grvConsulta', 'hdnClienteNo,hdnEmpresa,hdnProducto,hdnPersona,hdnPaginacion');

        }
        else {
            /*  muestra todo el panel   */
            fillgv('grvConsulta', 'hdnClienteNo, paginacion');
        }
        
        //$('#lblColores').text('-');
        $('#dvColores').show("slow");
    }

    function fnUsuAsig(obj) {
        arrayObj = obj.split('|')
        
        //$.each(arrayObj, function (key, value) {
        //    alert(key + ' - ' + value)
        //})

        fnDistribuidor(arrayObj[0], arrayObj[1])
        
    }

    $(document).ready(function () {
        fnCargaPantalla();
        fillddl('Empresa1', '');
        fillddl('Producto1', 'Empresa1');
        fillddl('Persona', '');
        fillddl('Distribuidor', 'hdnClienteNo');


        var settingsDate = {
            dateFormat: "yy-mm-dd",
            showAnim: "slide",
            changeMonth: true,
            changeYear: true,
            showOtherMonths: true,
            selectOtherMonths: true,
            autoSize: true,
            maxDate: '0'
            /*yearRange: "-100: 0",
            maxDate: '-18Y'*/
        };

        $.datepicker.setDefaults($.datepicker.regional["es"]);
        $('#txtFecIni').datepicker(settingsDate).attr('readonly', 'true').attr('onkeydown', 'return false');
        $('#txtFecFin').datepicker(settingsDate).attr('readonly', 'true').attr('onkeydown', 'return false');

        $('#btnAgregar').on('click', function () {
            $("#ddlEmpresa1 :first").text("Seleccionar Empresa");
            $("#ddlProducto1 :first").text("Seleccionar Producto").prop("Disabled", true);
            $("#ddlPersona :first").text("Seleccionar Persona");
            $("#ddlDistribuidor :first").text("Seleccionar Distribuidor");
        });

        $("select").on("change", function (e) {
            var id_select = e.currentTarget.id.toString();
            if ((id_select == "ddlEmpresa1") || (id_select == "ddlProducto1") || (id_select == "ddlPersona") || (id_select == "ddlDistribuidor")) {
                $("#ddlEmpresa1 :first").text("Seleccionar Empresa");
                $("#ddlProducto1 :first").text("Seleccionar Producto");
                $("#ddlPersona :first").text("Seleccionar Persona");
                $("#ddlDistribuidor :first").text("Seleccionar Distribuidor");
            }
        });

        /*$("#ddlEmpresa1").change(function () {
            $("#ddlProducto1 :first").text("Seleccionar Producto");
            $("#ddlProducto1").focus();
        });

        $("#ddlProducto1 valu['']").select(function () {
            $("#ddlProducto1 :first").text("Seleccionar Producto");
        });*/

        /*$("#ddlProducto1").bind("DOMSubtreeModified", function () {
            $("#ddlProducto1 :first").text("Seleccionar Producto");
            alert("tree changed");
        });

        $("#ddlProducto1").bind("DOMSubtreeModified", function () {
            alert("tree changed");
        });*/
    });
    

    function fnBuscar() {
        $("#txtFecIni").datepicker();
        $("#txtFecFin").datepicker();

        $("#txtFecIni").datepicker("option", "dateFormat", "dd/mm/yy");
        $("#txtFecFin").datepicker("option", "dateFormat", "dd/mm/yy");

        centraVentana($('#dvBusqueda'));
        $("#dvBusqueda").draggable();
        $('#dvBusqueda').show('slide', options, 1000, '');
        //fillAutocomplete('tags', 'lblNomUsu');
        //fillAutocomplete('txtDistribuidor', '');
        $("#tags").autocomplete({
            source: $('[id$="hdACNombre"]').val().split(',')
        });
        $("#txtDistribuidor").autocomplete({
            source: $('[id$="hdACDistribuidor"]').val().split(',')
        });

        fillAutocomplete('txtUsuAsign', 'lblNomUsu, txtDistribuidor')
    }

</script>

<style>
    #dvBusqueda {
        width: 570px !important;
        height: 180px !important;
    }

    #dvAgregarSolicitud {
        width: 570px !important;
        height: 180px !important;
        }
</style>

<div class="divPantConsul">
        <div class="fieldsetBBVA">
            <table width="100%">
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">                            
                            <tr>
                                <td colspan="2" style="width:70%;"><legend>Seguimiento de Status por Solicitante</legend></td>
                                <td style="width: 30%;" align="right" valign="middle">&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <input type="button" id="btnBuscar" value="Buscar" class="buttonBBVA2" onclick="fnBuscar()" /></td>
                                <td>                                   
                                    <asp:Label ID = "lblAgregar" runat = "server"></asp:Label>                                    
                                    <input type = "button" id = "btnAgregar" value = "Agregar" class = "buttonBBVA2" onclick = "muestraAgregar();"/>                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>        
        <p></p>
        <div class="divCuerpoConsul" id = "Principal">            
            <table id = "grvConsulta" width="100%">
            </table>      
            <asp:TextBox ID = "txtNomCliente" runat = "server" CssClass = "oculta"></asp:TextBox>
            <input type = "text" id = "inSol" class = "oculta" />
            <input type = "text" id = "inPro" class = "oculta" />
            <input type = "text" id = "inUsu" class = "oculta" />
        </div>        
    </div>    
    <div id = "dvTareas" class = "cssTareas">        
            <table id = "tbTareas" class = "resulGrid">   
            </table>
        </div>
        
        <div id="divDistri" class="dvLetrros" >
          <table width="100%">
            <tr>
              <th style="text-align:center;" >
                <label id="lblnombre" class ="cssLetras"></label>  
              </th>   
            </tr>
          </table>  
        </div>

        <asp:HiddenField ID = "hdnCriterio" runat = "server" />
        <asp:HiddenField ID = "hdnParametro1" runat = "server" />
        <asp:HiddenField ID = "hdnParametro2" runat = "server" />
        <asp:HiddenField ID= "hdnprimeraTare" runat="server" />  
        <asp:HiddenField ID="hdnClienteNo" runat="server" />
        <asp:HiddenField ID = "hdnUsuario" runat = "server" />
        <asp:HiddenField ID = "hdnPaginacion" runat = "server" />
        <asp:HiddenField ID = "hdnEmpresa" runat = "server" />
        <asp:HiddenField ID = "hdnProducto" runat = "server" />
        <asp:HiddenField ID = "hdnPersona" runat = "server" />

    <%--BUG-PD-23 29/03/2017 MAPH Cambios en la búsqueda para utilizarla con o sin autocompletar.--%>
    <asp:HiddenField ID="hdnNombreCliente" runat="server" />

    <input type="hidden" value="0" name="paginacion" id="paginacion">
    <br />

<%--    <asp:GridView runat="server" AutoGenerateColumns="true" ID="gvColor">       

    </asp:GridView>--%>    
    <div style="z-index:-1; top: 96%; width: 98%; height: 15px; position: absolute;">
        <pie:StatusColor ID="sc" runat="server" />        
    </div>
               
</asp:Content>
