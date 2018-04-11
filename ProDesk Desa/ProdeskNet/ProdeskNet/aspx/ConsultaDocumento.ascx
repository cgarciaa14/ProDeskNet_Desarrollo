<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ConsultaDocumento.ascx.vb" Inherits="aspx_ConsultaDocumento" %>

<%--BBV-P-423: RQSOL-04: AVH: 06/12/2016 SE CREA VENTANA FORZAJES--%>
<%--BBV-P-423: RQ H - CSS JRHM 09/12/2016 Se modificaron clases de estilos para asemejarce a el cotizador--%>
          <script src="../js/jquery.ui.widget.js"></script>   
    <script src="../js/jquery.iframe-transport.js"></script>
    <script src="../js/jquery.fileupload.js"></script>
     <script  type="text/javascript"> 
         
         function ColumnaOculta () {display:none;}

         //$("#ctl00_ctl00_cphCuerpo_cphPantallas_MuestraDocum_txtSolicitud").val(idsol);
    </script>


<br />
<asp:GridView ID="gridprueba" runat="server" HeaderStyle-CssClass="GridviewScrollHeaderBBVA" RowStyle-CssClass="GridviewScrollItemBBVA" AutoGenerateColumns="false" Width="100%">
                <Columns>
                    <asp:BoundField DataField="idDocumento" HeaderText="idDocumento" ItemStyle-HorizontalAlign="left"/>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre del Documento" ItemStyle-HorizontalAlign="Center"/>
                    

                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField DataField="RELDOC" HeaderText="Doc" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta"/>

                    <asp:templatefield HeaderText="Cargado" ItemStyle-HorizontalAlign="Center">
                    <itemtemplate>
                        <asp:checkbox ID="cbSelect" CssClass="gridCB" Enabled="false" runat="server"></asp:checkbox>
                    </itemtemplate>
                    </asp:templatefield>

                    <asp:BoundField DataField="PDK_LNK_IMAGEN" HeaderText="LINK" ItemStyle-HorizontalAlign="Center" Visible="false"  />
                    
                    <asp:templatefield HeaderText="Ver Imagen" ItemStyle-HorizontalAlign="Center">
                    <itemtemplate>
                        <%--<asp:LinkButton runat="server" ID="lbImagen" Text='<%# Eval("Nombre") %>'></asp:LinkButton>--%>
                        <asp:HyperLink ID="HyperLink1" runat="server"  NavigateUrl='<%# Eval("PDK_LNK_IMAGEN")%>' Target="_blank"   Text='<%# Eval("Nombre") %>'>
                            
                        </asp:HyperLink>
                    </itemtemplate>
                    </asp:templatefield>
                  
                    <asp:templatefield HeaderText="Cargado" ItemStyle-HorizontalAlign="Center">
                    <itemtemplate>
                            <span class="btnfubbva btn-successfubbva fileinput-buttonfubbva">
                            <span> Selecciona Archivo...</span>
                            <input id="fileupload" type="file" name="files[]" multiple runat="server"/>
                            </span>
                    </itemtemplate>
                    </asp:templatefield>
            
                </Columns>

            </asp:GridView>

<br />




<div style="text-align:center">
    <asp:Label runat="server" ID="lblSol"></asp:Label>

</div>



