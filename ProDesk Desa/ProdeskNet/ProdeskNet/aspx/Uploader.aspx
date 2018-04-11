<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Uploader.aspx.vb" Inherits="aspx_Uploader" %>

<%--BBV-P-423 RQ-PD-17 1 GVARGAS 22/12/2017 Mejoras carga huella --%>
<%--BBV-P-423 RQ-PD-17 2 GVARGAS 02/01/2018 Mejoras carga huella --%>

<!DOCTYPE HTML>
<html>
	<head runat="server">
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
		<title>Upload</title>
		<script src="../FileUpload/jquery-3.2.1.min.js"></script>
		<link rel="stylesheet" href="../FileUpload/bootstrap.min.css">
		<link rel="stylesheet" href="../FileUpload/bootstrap-theme.min.css">
		<script src="../FileUpload/bootstrap.min.js"></script>		

        <!-- Generic page styles -->
        <link rel="stylesheet" href="../FileUpload/style.css">
        <!-- CSS to style the file input field as button and adjust the Bootstrap progress bars -->
        <link rel="stylesheet" href="../FileUpload/jquery.fileupload.css">

        <style>
            body {
                padding-top: 0; 
            }
        </style>
	</head>
	<body style="background:rgba(0,0,0,0);">
        <div class="container">
            <span class="btn btn-primary fileinput-button">
                <i class="glyphicon glyphicon-plus"></i>
                <span>Seleccionar archivo .json</span>
                <!-- The fileupload input field used as target for the file upload widget -->
                <input id="fileupload" type="file" name="files[]" multiple>
            </span>
            <br>
            <br>
            <!-- The global progress bar -->
            <div id="progress" class="progress">
                <div class="progress-bar progress-bar-primary active"></div>
            </div>
            <!-- The container for the uploaded files -->
            <div id="files" class="files"></div>
        </div>

        <script src="../FileUpload/vendor/jquery.ui.widget.js"></script>
        <!-- The Iframe Transport is required for browsers without support for XHR file uploads -->
        <script src="../FileUpload/jquery.iframe-transport.js"></script>
        <!-- The basic File Upload plugin -->
        <script src="../FileUpload/jquery.fileupload.js"></script>
        <script>
            $(function () {
                'use strict';

                $('#fileupload').fileupload({
                    maxFileSize: 1024 * 1024,
                    url: 'fileUploadIne.ashx?upload=start',
                    dataType: 'json',
                    done: function (e, data) {
                        $.each(data.result.files, function (index, file) { $('<p/>').text(file.name).appendTo('#files'); });
                        window.parent.INEInformation();
                    },
                    progressall: function (e, data) {
                        var progress = parseInt(data.loaded / data.total * 100, 10);
                        $('#progress .progress-bar').css('width', progress + '%');
                        window.parent.INEInformation();
                    }
                }).prop('disabled', !$.support.fileInput).parent().addClass($.support.fileInput ? undefined : 'disabled');
            });

            function callFrame() {
                alert("llamada desde el iframe");
            }
        </script>
	</body>
</html>