<%@ Page Title="" Language="VB" MasterPageFile="~/aspx/Home.master" AutoEventWireup="false" CodeFile="ManejaPoolCredito.aspx.vb" Inherits="aspx_ManejaPoolCredito" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %>

<%--BBV-P-423: RQADM-25: erodriguez: 13/05/2017 Funcionalidad para Mesa pool de credito--%>
<%--BBV-P-423: RQXLS1: CGARCIA: 23/05/2017 Creacion de consultas de alianzas--%>
<%--BBV-P-423 - BUG-PD-63: erodriguez: 26/05/2017  se agrego una actualizacion del grid y permitir guardar el usuario asignado--%>
<%--BBV-P-423 - BUG-PD-79: erodriguez: 09/06/2017 se agrego validacion para ocultar boton imprimir cuando no tenga usuario asignado--%>
<%--BBV-P-423 - BUG-PD-126: erodriguez: 30/06/2017 Se ordeno el grid para poner primero los asignados--%>
<%--BUG-PD-193: RHERNANDEZ: 21/08/17 SE MODIFICA EVALUACION DE CARGA DE ARCHIVOS PARA PANTALLAS QUE NO REQUERIRAN VER EL COMPARADOR DE ARCAIVIN--%>
<%--BUG-PD-199: RHERNANDEZ: 24/08/17 SE MODIFICA CARGA DE ARCHIVOS PARA ABRIR VISOR TELEPRO--%>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" Runat="Server">

    <script type="text/javascript" src="../js/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="../js/jquery.iframe-transport.js"></script>
    <script type="text/javascript" src="../js/jquery.fileupload.js"></script>
    <script type="text/javascript" src="../js/Funciones.js"></script>
    <script type="text/javascript">

       $(function () {
            $('#txtruta').on('change', function () {
                var filePath = $(this).val();
                alert(filePath);
            });
        });


        //function mostrarCanceDiv() {
        //    div = document.getElementById('divcancela');
        //    div.style.display = '';
        //}

        function Replica() {
            //var fechaIni = document.getElementById('#);
            var fechaIni = $('#<%= hfFeIni.ClientID()%>');
            var fech1S = ($("#feIni").val() == ""
                && $('#<%= txtNombre.ClientID%>').val() == null
                && $('#<%= txtNoSol.ClientID%>').val() == null
                && $('#<%= txtRFC.ClientID%>').val() == null)
                ?
                    RestarDias().toString().split('/')
                :
                    ($("#feIni").val() == null || $("#feIni").val() == ""
                        ?
                            ''
                        :
                            $("#feIni").val().split('/'));

            var fechaFin = $('#<%= hfFeFin.ClientID()%>');
            var fech2S = ($("#feFin").val() == ""
                && $('#<%= txtNombre.ClientID%>').val() == null
                && $('#<%= txtNoSol.ClientID%>').val() == null
                && $('#<%= txtRFC.ClientID%>').val() == null)
                ?
                    new Date().yyyymmdd().toString().split('/')
                :
                    ($("#feFin").val() == null || $("#feFin").val() == ""
                        ?
                            ''
                        :
                            $("#feFin").val().split('/'));

            fechaIni.val(fech1S == '' ? '' : fech1S[2] + '/' + fech1S[1] + '/' + fech1S[0]);
            fechaFin.val(fech2S == '' ? '' : fech2S[2] + '/' + fech2S[1] + '/' + fech2S[0]);

          

            var botonBuscar = document.getElementById('<%= btnBuscar.ClientID%>');
            botonBuscar.click();
            

        }
        Date.prototype.yyyymmdd = function () {
            var mm = this.getMonth() + 1; // getMonth() is zero-based
            var dd = this.getDate();

            return [(dd > 9 ? '' : '0') + dd,
                    (mm > 9 ? '' : '0') + mm,
                    this.getFullYear()
            ].join('/');
        };

        function RestarDias() {
            var someDate = new Date();
            var numberOfDaysToAdd = 7;
            someDate.setDate(someDate.getDate() - numberOfDaysToAdd);
            return someDate.yyyymmdd();
        }

        function pageLoad() {
            var settingsDate1 = {
                dateFormat: "dd/mm/yy",
                showAnim: "slide",
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                selectOtherMonths: true,
                autoSize: true,
                yearRange: "-99:+0",
                maxDate: "+0m +0d"

            };

            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $('#feIni').datepicker(settingsDate1).attr('readonly', 'true').attr('onkeydown', 'return false');
            $('#feFin').datepicker(settingsDate1).attr('readonly', 'true').attr('onkeydown', 'return false');

            $('input').on("change paste keyup dragend", function () {
                var dict = { "á": "a", "é": "e", "í": "i", "ó": "o", "ú": "u", "Á": "A", "É": "E", "Í": "I", "Ó": "O", "Ú": "U" };
                $(this).val($(this).val().toString().replace(/[^\w ]/g, function (char) { return dict[char] || char; }));
            });

        }

        function fnChequea() {
            fillUpload('tbValidarObjetos', 'hdPantalla, hdSolicitud, hdusuario', 'per1', '');
                PopUpLetrero("Documento procesado exitosamente");
        }
    


        $(window).on("load", function () { Replica() });
    </script>
<div class="divPantConsul">
<%--<div class="fieldsetBBVA">
    <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <legend>
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Mesa pool de credito."></asp:Label></legend>
                                </td>
                                <td>
                                    <asp:Label ID="lblIdPantalla" runat="server" CssClass="oculta"></asp:Label>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
</div>
<div class ="rTable">
     <div class="rTableRow">
         <div class="rTableRow">
             <div class="rTableCell" style="width:15%">
                 <asp:label ID="Label1" runat="server" Text="Número de solicitud"/>
             </div>
             <div class="rTableCell" style="width:8%">
                 <asp:TextBox ID="txtSol" SkinID="txtGeneral" MaxLength="12" runat="server" Style="width: 120px !important;"/>  
             </div>
              <div class="rTableCell" style="width:1%"></div>
             <div class="rTableCell" style="width:15%">
                 <asp:label ID="Label2"  runat="server" Text="Fecha Inicio"/>                
             </div>
             <div class="rTableCell" style="width:8%">
                  <input type="text" id="fi_date"/>
             </div>
              <div class="rTableCell" style="width:1%"></div>
             <div class="rTableCell" style="width:15%">
                 <asp:label ID="Label3"  runat="server" Text="Fecha fín"/>                
             </div>
             <div class="rTableCell" style="width:8%">
                  <input type="text" id="ff_Date"/>
              </div>

             <div class="rTableCell" style="width:20%"></div>
             
             <div class="rTableCell" style="width:8%">
                     <asp:Button runat="server" id="btnBuscar" text="Buscar" SkinID="btnGeneral" />
             </div>
             <div class="rTableCell" style="width:8%">
                      <asp:Button runat ="server" ID="btnLimpiar" Text ="Limpiar" SkinID="btnGeneral" />
             </div>
             
         </div>
         <div class="rTableRow">
             <div class="rTableCell">
                 <asp:label ID="Label4" runat="server" Text="Número del cliente"/>                 
             </div>
             <div class="rTableCell" >
                <asp:TextBox ID="TxtNoCte" SkinID="txtGeneral" MaxLength="12" runat="server" Style="width: 120px !important;"/>   
             </div>
             <div class="rTableCell" ></div>
             <div class="rTableCell" >
                 <asp:label ID="Label5" runat="server" Text="RFC"/>               
             </div>
             <div class="rTableCell" >
                   <asp:TextBox ID="TxtRFC" SkinID="txtGeneral" MaxLength="12" runat="server" Style="width: 120px !important;"/>    
             </div>
             <div class="rTableCell" ></div> 
              <div class="rTableCell" ></div> 
              <div class="rTableCell" ></div> 
              <div class="rTableCell" ></div> 
              <div class="rTableCell" ></div> 
              <div class="rTableCell" ></div> 
              <div class="rTableCell" ></div> 
              <div class="rTableCell" ></div>             
         </div>
     </div>
</div>--%>

     <div class="fieldsetBBVA">
            <table>
                <tr class="tituloConsul">
                    <td width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="2" style="width: 70%;">
                                    <legend>
                                        <asp:Label ID="lblNomPantalla" runat="server" Text="Mesa pool de credito"></asp:Label></legend>
                                </td>
                                <td>
                                    <asp:Label ID="lblIdPantalla" runat="server" CssClass="oculta"></asp:Label>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>


        <div id="Div1" class="resulbbvaCenter" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNoSol" Text="Número de Solicitud"></asp:Label>
                    </td>
                    <td>                       
                        <input type="text" runat="server" ID="txtNoSol" MaxLength="20" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,7);" ></input>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFechaIni" Text="Fecha Inicio"></asp:Label>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtFechaIni" CssClass="txt3BBVA" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                        <table id="tbFechaInicio" runat="server">
                            <tr>
                                <td>
                                    <asp:HiddenField runat="server" ID="hfFeIni" />
                                    <input type="text" id="feIni" class="txt3BBVA" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblFechaFin" Text="Fecha Fin"></asp:Label>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtFechaFin" CssClass="txt3BBVA" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table id="tbFechaFin" runat="server">
                            <tr>
                                <td>
                                    <asp:HiddenField runat="server" ID="hfFeFin" />
                                    <input type="text" id="feFin" class="txt3BBVA" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <input id="btnBuscaCliente" type="button" value="Buscar" onclick="Replica();" class="buttonBBVA2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNombre" Text="Nombre del Cliente"></asp:Label>
                    </td>
                    <td colspan="3"> 
                        <input type="text" runat="server" ID="txtNombre" Width="98%" CssClass="txt3BBVA" Onkeypress="return ValCarac(event,6);" onkeyup="ReemplazaAcentos(event, this.id, this.value);" MaxLength="50" ></input>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblRFC" Text="RFC"></asp:Label>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <input type="text" runat="server" ID="txtRFC" CssClass="txt3BBVA" onkeypress="return ValCarac(event,22)" class="txt3BBVA solicitanteClass" maxlength="13" extraProperty="RFC"></input>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                         <%--<div style="visibility: collapse">--%>
                        <asp:Button runat="server" ID="btnLimpiar" Text="Limpiar" CssClass="buttonSecBBVA2" OnClick="btnLimpiar_Click1" />
                         <%--</div>--%>
                    </td>
                </tr>
            </table>
            <br />

    <div class="resulbbvaCenter" style="max-height: 310px; overflow-y: scroll; position: absolute; top: 35%; width: 100% !important;">
         <%--   OnPageIndexChanging="gvMesaPoolCredito_PageIndexChanging" AllowPaging="true" PageSize="10" PagerStyle-HorizontalAlign="Center"--%>
            <asp:GridView ID="gvMesaPoolCredito" runat="server" AutoGenerateColumns="false" 
                HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" 
                Width="100%"  
                EmptyDataText="No se encontró información con los parámetros proporcionados.">
                <Columns>
                    <asp:BoundField DataField="NO_SOLICITUD" HeaderText="Folio Solicitud" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombre del Cliente" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="RFC" HeaderText="RFC" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="AGENCIA" HeaderText="Agencia" ItemStyle-HorizontalAlign="Center" />
					<asp:BoundField DataField="ALIANZA" HeaderText="Alianza" ItemStyle-HorizontalAlign="Center" />																							  
                    <asp:BoundField DataField="CANAL" HeaderText="Canal" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Submarca" HeaderText="Submarca" ItemStyle-HorizontalAlign="Center" />                   
                    <asp:BoundField DataField="MONTO_SOLICITADO" HeaderText="Monto Solicitado" ItemStyle-HorizontalAlign="Center" />                  
                    <asp:TemplateField HeaderText="Atender" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                        <ItemTemplate>
                            <div class="tooltip">
                                <%--<asp:CheckBox runat="server" ID="chkSelecciona" AutoPostBack="true" OnCheckedChanged="chkSelecciona_CheckedChanged" Checked="false" CommandArgument='<%# Eval("NO_SOLICITUD")%>' />--%>
                                <asp:CheckBox runat="server" ID="chkSelecciona" AutoPostBack="true" OnCheckedChanged="chkSelecciona_CheckedChanged"  />

                            </div>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1px">
                        <ItemTemplate>
                            <div class="tooltip">
                                <%--<asp:CheckBox runat="server" ID="chkSelecciona" AutoPostBack="true" OnCheckedChanged="chkSelecciona_CheckedChanged" Checked="false" CommandArgument='<%# Eval("NO_SOLICITUD")%>' />--%>
                                <asp:label visible="false" runat="server" ID="ID_PDK_ID_REL_COT_POOL" AutoPostBack="true"  text='<%# Eval("PDK_ID_REL_COT_POOL")%>'  CommandArgument='<%# Eval("PDK_ID_REL_COT_POOL")%>' />

                            </div>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                        <ItemTemplate>
                            <div class="tooltip">
                                <%--<asp:CheckBox runat="server" ID="chkSelecciona" AutoPostBack="true" OnCheckedChanged="chkSelecciona_CheckedChanged" Checked="false" CommandArgument='<%# Eval("NO_SOLICITUD")%>' />--%>
                                <asp:button runat="server" ID="btnPrint" Text="Imprimir" CssClass="buttonSecBBVA2" AutoPostBack="true" onclick="btnPrint_Click"  />

                            </div>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:BoundField DataField="PDK_ID_REL_COT_POOL" ItemStyle-CssClass="oculta" HeaderStyle-CssClass="oculta" FooterStyle-CssClass="oculta" ItemStyle-HorizontalAlign="Center" />

                </Columns>
            </asp:GridView>
       </div>
     <%--   <div>
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" ID="Button1" text="Forzar"  onclick="Button1_Click1" CssClass="buttonSecBBVA2" />
                    
                    </td>
                </tr>
            </table>
        </div>--%>

             </div>


        <div style="height: 100px"></div>
        <table id="tbValidarObjetos" class="resulGrid" runat="server">
        </table>

    <%--    <div class="resulbbvaCenter divAdminCatPie">
            <table width="100%" style="height: 100%;">
                <tr>
                    <td align="right" valign="middle">
                        <asp:Button runat="server" ID="btnRegresar" text="Regresar"  onclick="btnRegresar_Click" CssClass="buttonSecBBVA2" />
                     
                    </td>
                </tr>
            </table>
        </div>--%>

    <%--    <div id="divcancela" style="display: none">
            
            <asp:Panel ID="popForzaje" runat="server" CssClass="cajadialogo" Width="250px">
                <div class="tituloConsul">
                    <asp:Label ID="Label1" runat="server" Text="Permisos para Forzar" />
                </div>
                <table width="100%">
                    <tr>
                        <td class="campos">Usuario:</td>
                        <td>
                            <asp:TextBox ID="txtusua" SkinID="txtGeneral" MaxLength="12" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="campos">Password:</td>
                        <td>
                            <asp:TextBox ID="txtpass" runat="server" SkinID="txtGeneral" MaxLength="12" TextMode="Password" EnableTheming="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="campos">Comentarios:</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox runat="server" ID="txtComentarios" TextMode="MultiLine" Height="50px" Width="200px" MaxLength="200" Columns="50" Rows="12"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td align="center" valign="middle" style="width: 50%">
                         
                        </td>
                        <td align="center" valign="middle" style="width: 50%">
                            <asp:Button ID="Button1" runat="server" Text="Cancelar" OnClick="Button1_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>--%>

</div>

 <div style="visibility: collapse">
        <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="buttonBBVA2" OnClick="btnBuscar_Click" />
 </div>
    <asp:HiddenField ID="hdPantalla" runat="server" />
    <asp:HiddenField ID="hdSolicitud" runat="server" />
    <asp:HiddenField ID="hdusuario" runat="server" />

</asp:Content>

