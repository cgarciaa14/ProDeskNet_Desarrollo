<!--BBVA-P-423:RQADM-03 GVARGAS: 03/02/2017 New visor Comparador de Documentos-->
<!--BUG-PD-24  GVARGAS  29/03/2017 Bugs Campos Desabilitados en Pre si la SOL se regresa-->
<!--BUG-PD-29 JRHM 10/04/17 Se modifica generacion de urls del visor-->
<!--RQ-PD28: DJUAREZ: 27/02/2018 Se modifica el visor para mostrar varios documentos en una pantalla-->

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Comparador.aspx.vb" Inherits="Comparadoraspx" %>

<!DOCTYPE HTML>
<html>
	<head runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
		<title>Comparador</title>
        <script type="text/javascript" src="js/jquery-3.1.1.min.js"></script>
        <style type="text/css">@import url(js/bootstrap.min.css); </style>
        <script type="text/javascript" src="js/bootstrap.min.js"></script>
		<script>
		    function jsonBack(errorLabel, destino, successfully, datos) {
		        var settings = { type: "POST", url: "", async: false, data: "", contentType: "application/json; charset=utf-8", dataType: "json", success: null, failure: null };
		        settings.url = destino
		        settings.success = successfully
		        settings.data = datos
		        settings.failure = function (response) { alert("Error al cargar : " + errorLabel.toString()); }
		        $.ajax(settings);
		    }

		    function JSONdocumento_x(pantalla, folio) {
		        //var destino = "../ProdeskNet/aspx/inicio.aspx/DocsProdesk";
		        //var destino = "../aspx/inicio.aspx/DocsProdesk";
		        var destino = window.location.origin.toString() + window.location.pathname.toString().replace("Comparador.aspx", "aspx/inicio.aspx/DocsProdesk");		        
		        var successfully = OnSuccessDocumento_1;
		        var datos = '{ "pantalla": "' + pantalla + '", "folio": "' + folio + '", "opcion": "0" }';
		        jsonBack('No fue posible cargar documentos.', destino, successfully, datos);
		    }

		    function JSONdocumento_1(folio) {
		        //var destino = "../ProdeskNet/aspx/inicio.aspx/DocsProdesk";
		        //var destino = "../aspx/inicio.aspx/DocsProdesk";
		        var destino = window.location.origin.toString() + window.location.pathname.toString().replace("Comparador.aspx", "aspx/inicio.aspx/DocsProdesk");
		        var successfully = OnSuccessDocumento_1;
		        var datos = '{ "pantalla": "", "folio": "' + folio + '", "opcion": "1" }';
		        jsonBack('No fue posible cargar documentos.', destino, successfully, datos);
		    }

		    function OnSuccessDocumento_1(response) {
		        var items = $.parseJSON(response.d);
		        if (items.contador.toString() == "0") { alert(items.mensaje.toString()); }
		        else { $.each(items.documentos, function (i, item) { $('#documento_1').append($('<option>', { value: item.id, text: item.nombre, folioDoc: item.folio })); }); }
		    }

		    function JSONdocumento_2(cliente) {
		        //var destino = "../ProdeskNet/aspx/inicio.aspx/CustomerDocumentData";
		        //var destino = "../aspx/inicio.aspx/CustomerDocumentData";
		        var destino = window.location.origin.toString() + window.location.pathname.toString().replace("Comparador.aspx", "aspx/inicio.aspx/CustomerDocumentData");
		        var successfully = OnSuccessDocumento_2;
		        var datos = '{ "cliente": "' + cliente + '" }';
		        jsonBack('No fue posible cargar documentos.', destino, successfully, datos);
		    }

		    function OnSuccessDocumento_2(response) {
		        var items = $.parseJSON(response.d);
		        if (items.counter.toString() == "0") {
		            alert(items.mensaje.toString());
		            $('#documento_2').prop('disabled', true);
		        }
		        else { $.each(items.options, function (i, item) { $('#documento_2').append($('<option>', { value: item.value, text: item.texto })); }); }
		    }

		    function getdocumento_1(id_document, folio) {
		        //var destino = "../ProdeskNet/aspx/inicio.aspx/getDocument_1";
		        //var destino = "../aspx/inicio.aspx/getDocument_1";
		        var destino = window.location.origin.toString() + window.location.pathname.toString().replace("Comparador.aspx", "aspx/inicio.aspx/getDocument_1");
		        var successfully = OnSuccessGetDocumento_1;
		        var datos = '{ "id_document": "' + id_document.toString() + '", "folio" : "' + folio.toString() + '" }';
		        jsonBack('No fue posible cargar el documento.', destino, successfully, datos);
		    }

		    function OnSuccessGetDocumento_1(response) {
		        var frame_1 = "<iframe style='width: 100%; height: 550px;' src='";
		        var frame_1_1 = "' name='frame1' id='frame1' frameborder='0' marginwidth='0' marginheight='0' scrolling='auto' allowtransparency='false'></iframe>";
		        $("#viewer_1").html("");
		        $("#viewer_1").html(frame_1.toString() + response.d.toString() + frame_1_1.toString());
		        $('iframe #frame1').contents().find("head").append($("<style type='text/css'> GERARDO my-class {display:none;}  </style>"));
		    }

		    function getdocumento_2(id_document) {
		        //var destino = "../ProdeskNet/aspx/inicio.aspx/getDocument_2";
		        //var destino = "../aspx/inicio.aspx/getDocument_2";
		        var destino = window.location.origin.toString() + window.location.pathname.toString().replace("Comparador.aspx", "aspx/inicio.aspx/getDocument_2");
		        var successfully = OnSuccessGetDocumento_2;
		        var datos = '{ "id_document": "' + id_document.toString() + '" }';
		        jsonBack('No fue posible cargar el documento.', destino, successfully, datos);
		    }

		    function OnSuccessGetDocumento_2(response) {
		        var frame_2 = "<iframe style='width: 100%; height: 550px;' src='";
		        var frame_2_1 = "' name='frame2' id='frame2' frameborder='0' marginwidth='0' marginheight='0' scrolling='auto' allowtransparency='false'></iframe>";
		        $("#viewer_2").html("");
		        $("#viewer_2").html(frame_2.toString() + response.d.toString() + frame_2_1.toString());
		    }

		    $(document).ready(function () {
		        $('#myModal').modal('show');
		        $.urlParam = function (name) {
		            var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
		            if (results == null) { return null;  } else { return results[1] || 0; }
		        }

		        var folio = $.urlParam("folio");
		        var pantalla = $.urlParam("pantalla");
		        var cliente = $.urlParam("cliente");
		        var id_doc = $.urlParam("id_doc");
		        var id_doc_exist = $.urlParam("id_doc_exist");
		        var msg = "";

		        if (folio == null) { msg = msg + "Imposible cargar documentos, del cliente. \n"; }
		        else {
		            if (pantalla == null) { JSONdocumento_1(folio); } else { JSONdocumento_x(pantalla, folio); }
		            id_doc = GetIdDocuments();
		            if (id_doc != null) {
		                getdocumento_1(id_doc, folio);
		                $("#documento_1").val(id_doc);
		            }
		        }

		        if (cliente == null) {
		            //msg = msg + "Imposible cargar documentos BBVA Bancomer.\n";
		            $("#hideDocument_2").prop("checked", true);
		            ocultarViewers();

		        }
		        else {
		            JSONdocumento_2(cliente);
		            if (id_doc_exist != null) {
		                var str = '#documento_2 option[value=' + id_doc_exist.toString() + ']';
		                if ($(str.toString()).length) {
		                    getdocumento_2(id_doc_exist);
		                    $("#documento_2").val(id_doc_exist);
		                }
		                else {
		                    msg = msg + 'El Documento seleccionado no existe.\n';
		                }
		            }
		        }

		        $('#myModal').modal('hide');

		        if (msg != "") { alert(msg.toString()); }

		        /*if (msg != "") {
		            $('#myModalMsg').modal('show');
		            $('#msgP').text(msg.toString());
		        }*/

		        $('select').on('change', function () {
		            $('#myModal').modal('show');
		            var SelectedValor = this.value;
		            var SelectedID = this.id;
		            if (SelectedID.toString() == 'documento_1') {
		                var id_documents_1 = GetIdDocuments();
		                if (SelectedValor.toString() == '0') { $("#viewer_1").html(""); } else { getdocumento_1(id_documents_1, folio); }
		            } else {
		                if (SelectedValor.toString() == '0') { $("#viewer_2").html(""); } else { getdocumento_2(SelectedValor); }
		            }
		            $('#myModal').modal('hide');
		        });

		        $('input[type=checkbox]').on('click', function () {
		            $('#myModal').modal('show');
		            var SelectedID = this.id;
		            var check = $('#' + SelectedID).prop('checked');
		            if (check) { if (SelectedID == 'hideDocument_1') { $('#viewer_1').hide(); } else { $('#viewer_2').hide(); } }
		            else { if (SelectedID == 'hideDocument_1') { $('#viewer_1').show(); } else { $('#viewer_2').show(); } }
		            ocultarViewers();
		            $('#myModal').modal('hide');
		        });
		    });

		    function ocultarViewers() {
		        if (($("#hideDocument_1").prop('checked')) && ($("#hideDocument_2").prop('checked'))) {
		            $('#viewer_1').attr('class', 'col-sm-6');
		            $('#viewer_2').attr('class', 'col-sm-6');
		        } else if (!($("#hideDocument_1").prop('checked')) && ($("#hideDocument_2").prop('checked'))) {
		            $('#viewer_1').attr('class', 'col-sm-12');
		            $('#viewer_2').attr('class', 'col-sm-6');
		        } else if (($("#hideDocument_1").prop('checked')) && !($("#hideDocument_2").prop('checked'))) {
		            $('#viewer_1').attr('class', 'col-sm-6');
		            $('#viewer_2').attr('class', 'col-sm-12');
		        } else {
		            $('#viewer_1').attr('class', 'col-sm-6');
		            $('#viewer_2').attr('class', 'col-sm-6');
		        }		        
		    }

		    function GetIdDocuments() {
		        var IdDocuments = "";
		        $("#documento_1 > option").each(function () {
		            if (this.attributes["folioDoc"].value != "0") {
		                if (IdDocuments.length > 0) {
		                    IdDocuments = IdDocuments + ",";
		                }
		                IdDocuments = IdDocuments + this.attributes["folioDoc"].value;
		            }
		        });
		        return IdDocuments;
		    }
		</script>
		<style>		
			body,html {
			  height: 100%;
			  overflow-y: hidden;
			  overflow-x: hidden;
			}
			
			.row {
				min-height: 90%;
				margin-right: -15px;
				margin-left: -15px; 
			}
			#viewer_1, #viewer_2{
				padding-left: 0;
				padding-right: 0;
			}
			
			div img {
				display: block;
				width: 100%;
			}

            .badge {
                padding-top: 1px;
            }

            input[type=checkbox], input[type=radio] {
                vertical-align: text-bottom;
                margin-top: 2px;
            }
			
            .label-default {
                 background-color: #08c;
            }

            .badge {
                background-color: #08c;
            }
		</style>
	</head>
	<body>
		<article class="container">
			<div class="page-header" style="padding-bottom: 0; margin: 0; ">
				<h3 style="margin-top: 0; margin-bottom: 0;">Comparador de documentos</h3>
				<div class="row">
				<form>
					<div class="form-group">
						<div class="col-sm-4">
                            <h5><span class="label label-default">Selecciona un documento:</span></h5>
                            <select class="form-control" id="documento_1" style="width: 50%;">
								<option value="0" folioDoc="0">Selecciona un documento</option>
							</select>
						</div>
						<div class="col-sm-2">
                            <span class="badge">Ocultar <input id="hideDocument_1" type="checkbox"></span>
						</div>
						<div class="col-sm-4">
							<h5><span class="label label-default">Selecciona un documento:</span></h5>
                            <select class="form-control" id="documento_2" style="width: 50%;">
								<option value="0">Selecciona un documento</option>
							</select>
						</div>
						<div class="col-sm-2">
                            <span class="badge">Ocultar <input id="hideDocument_2" type="checkbox"></span>
						</div>
					</div>
				</form>
				</div>
			</div>
		</article>		
		<article class="row" style="width: 95%; width: 95%; margin:auto;">
			<div id="viewer_1" class="col-sm-6" style="height: 550px;"></div> 
			<div id="viewer_2" class="col-sm-6" style="height: 550px;"></div> 
		</article>		
		<!-- Modal -->
		<div class="modal fade" id="myModal" role="dialog">
			<div class="modal-dialog modal-sm">
				<div class="modal-content">
					<div class="modal-body">
						<img src="App_Themes/img/load.gif" alt="" />
					</div>
				</div>
			</div>
		</div>
		<!-- Modal -->
		<div class="modal fade" id="myModalMsg" role="dialog">
			<div class="modal-dialog modal-sm">
				<div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Modal Header</h4>
                    </div>
					<div class="modal-body">
						<p id="msgP" />
					</div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
				</div>
			</div>
		</div>
	</body>
</html>