<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/aspx/Home.Master" CodeFile="consultaTareaPerfil.aspx.vb" Inherits="consultaTareaPerfil" %>
<%@ MasterType VirtualPath="~/aspx/home.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">
    <!--  BBV-P-423  RQSOL-02  gvargas   15/12/2016 Se agregó CSS (display none) A los tags TD de "Perfil" para que no se muestren en pantalla -->
    <!--                                            Se Cambio el text de TD de "Tareas - Perfil" a "Tareas" -->

    <script type = "text/javascript">       
        //fillgv("tbTareaPerfil", "ddlEmpresa, ddlProducto, ddlPerfil, ddlProceso, ddlFlujos");

        function fnChequea(update, obj, where) {
            if (obj.checked == false) {
                chk = 0
            }
            else { 
                chk = 1
            }
            qry = update + ' ' + chk + ' ' + where;
            btnInsUpd(qry);
        }        

        $(document).ready(function () {
            fillddl('Empresa', '');            
            fillddl('Perfil', '');

            //$('[id$=Empresa]').change(function () {
            //    fillddl('Producto1', 'Empresa');
            //})
        })
    </script>

    <div class="divPantConsul">
        <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td style="width:20%;">Tareas</td>                        
                    <td class="campos" style="width:5%;">Empresa:</td>
                    <td style="width:15%;">
                        <select id = "ddlEmpresa" class = "Text" style="width:100%;" onchange = "fillddl('Producto', 'Empresa');fillgv('tbTareaPerfil', 'ddlEmpresa, ddlProducto, ddlPerfil');">
                            <option value = "0"></option>
                        </select>                                    
                    </td>                                
                    <td class="campos" style="width:5%;">Producto:</td>
                    <td style="width:15%;">
                        <select id="ddlProducto" class="Text" style="width:100%;"   onchange = "fillgv('tbTareaPerfil', 'ddlEmpresa, ddlProducto, ddlPerfil');">
                            <option value = "0"></option>
                        </select>
                    </td>                                
                    <td class="campos" style="width:5%; display:none;">Perfil:</td>
                    <td style="width:15%; display:none;">
                        <select id="ddlPerfil" class="Text" style="width:100%;"  onchange = "fillgv('tbTareaPerfil', 'ddlEmpresa, ddlProducto, ddlPerfil');">
                            <option value = "0"></option>
                        </select>
                    </td>                                
                </tr>
            </table>
        </div>
        <div class="divCuerpoConsul">                   
            <table id = "tbTareaPerfil" class = "resulGridsh">
            </table>
        </div>
    </div>    
</asp:Content>
