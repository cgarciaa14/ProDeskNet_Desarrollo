<%@ Page Language="vb" AutoEventWireup="false" CodeFile="consultaasigregladnegocio.aspx.vb" Inherits="consultaasigregladnegocio" MasterPageFile="~/aspx/Home.Master" EnableEventValidation = "false"%>
<%@ MasterType VirtualPath="~/aspx/home.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPantallas" runat="server">

    <script type = "text/javascript" language = "javascript">
        function fnBtnActualiza(idregla, idobjeto) {
            actua = $("[id$=" + idregla + "]").val();            
            ddlTarea = $("[id$=ddlTarea]").val();
            ddlPantalla = $("[id$=ddlPantalla]").val();
            if (actua == '') {
                actua = 0;
            }
            btnInsertar(" exec spActualizaRelPantObj " + idobjeto + "," + actua + "," + ddlTarea + "," + ddlPantalla + ";", "");
        }

        function insertaRN() {
            ddlPantalla = $('#ddlPantalla').val();
            ddlRN = $('#ddlRN').val();
            btnInsUpdReload("exec spActualizaRelPantObj " + ddlPantalla + ", " + ddlRN + ";", 'tbObjetos', '');
        }

        function fnEliminaRN(RelObjPan) {
            btnInsUpdReload("delete RN_REL_OBJ_RN Where pdk_id_rel_obj_pantalla = " + RelObjPan, 'tbObjetos', '');
        }

        $(document).ready(function () {
            fillgv('tbObjetos', '');
        });
    </script>


<div class="divPantConsul">
    <div class="divFiltrosConsul">
            <table class="tabFiltrosConsul">
                <tr class="tituloConsul">
                    <td colspan="2" style="width:70%;">Asignacion de regla de negocio.</td>
                </tr>
            </table>
        </div>

    <div id = "Principal" class="divCuerpoConsul">
                    <div style = "width:100%">
                        <table id = "tbObjetos" width = "98%" class = "resulGridsh">                                                                                
                        </table>
                    </div>
    </div>        
</div>

</asp:Content>
