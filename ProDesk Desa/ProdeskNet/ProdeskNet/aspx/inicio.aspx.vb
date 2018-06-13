' BBVA-P-423:RQADM-03 GVARGAS: 03/02/2017 Web methods para Visor de Documentos
' BUG-PD-13  GVARGAS  28/02/2017 WebMethod Out To Procotiza
' BUG-PD-24  GVARGAS  29/03/2017 Bugs Campos Desabilitados en Pre si la SOL se regresa
' BUG-PC-88 GVARGAS 06/07/2017  Switch To ProDesk final
' BUG-PD-151  GVARGAS  12/07/2017 Cambios iv_ticket
' BUG-PD-249 GVARGAS 26/10/2017 Cierre de session cambio general
' RQ-PD28: DJUAREZ: 27/02/2018 Se modifica el visor para mostrar varios documentos en una pantalla
' RQ-PD31: DJUAREZ: 08/03/2018: SE CREA POPUP PARA MODIFICAR LA COLONIA CUANDO SE GUARDE LA COLONIA "OTRO"
' RQ-PD34: JMENDIETA: 03/05/2018: Se agrega un token de acceso para la visualizacion de documentos. 
' BUG-PD-437: ERODRIGUEZ: 07/05/2018: Se realiza el cambio de estatus del usuario y la desasignacion de folios segun el perfil.
Imports System.Data.SqlClient
Imports System.Data
Imports ProdeskNet.WCF

Public Class inicio
    Inherits System.Web.UI.Page

    <System.Web.Services.WebMethod()> _
    Public Shared Function getDocument_1(ByVal id_document As String, ByVal folio As String) As String
     'RQ-PD34 INI
        Dim uri As String = String.Empty
        Dim mensajeError As String = String.Empty
        Dim clsGenerarToken As New clsGenerarToken
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        serializer.MaxJsonLength = Int32.MaxValue


        Try
            uri = System.Configuration.ConfigurationManager.AppSettings("Archiving").ToString()
            uri = uri.Replace("FOLIOS", id_document)
            uri = uri.Replace("VALUE_TOKEN", clsGenerarToken.ObtenerToken())

            If Not String.IsNullOrEmpty(clsGenerarToken.StrError) Then
                mensajeError = clsGenerarToken.StrError
            End If

        Catch ex As Exception
            uri = uri.Replace("VALUE_TOKEN", String.Empty)
            mensajeError = ex.Message
        End Try


        Dim objectResult = New With
            {
                Key .mensajeError = mensajeError,
                Key .uri = uri
            }

        Return serializer.Serialize(objectResult)
        'RQ-PD34 FIN
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function getDocument_2(ByVal id_document As String) As String
        Dim uri As String = System.Configuration.ConfigurationManager.AppSettings("ArchivosExistentes").ToString() + id_document
        Return uri
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function CustomerDocumentData(ByVal cliente As String) As String
        Dim jsonDocs As String = ""

        Dim getCustomerDocumentData As getCustomerDocumentData = New getCustomerDocumentData()

        getCustomerDocumentData.option1 = "CO" 'hardcode
        getCustomerDocumentData.customer.id = cliente 'num cliente if null no cliente bancomer
        getCustomerDocumentData.matrix.matrixType = "OFFICIAL" 'hardcode
        getCustomerDocumentData.referenceNumberProcedure = ""

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonBODY As String = serializer.Serialize(getCustomerDocumentData)

        jsonBODY = jsonBODY.Replace("option1", "option")

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
        Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
        Dim rest As ProdeskNet.WCF.RESTful = New ProdeskNet.WCF.RESTful()
        'rest.consumerID = "10000056"
        rest.Uri = System.Configuration.ConfigurationManager.AppSettings("getCustomerDocumentData").ToString()

        Dim jsonResult As String = rest.ConnectionPost(userID, iv_ticket1, jsonBODY)
        Dim resp As jsonDocs = New jsonDocs()

        If rest.IsError Then
            resp.mensaje = "Servicio de visor de Documentos no disponible."
            resp.counter = 0
        Else
            Dim JSONDocs_raw As customerDocument = serializer.Deserialize(Of customerDocument)(jsonResult)
            If (JSONDocs_raw.customerDocument.Count > 0) Then
                Dim contador As Integer = 0

                Dim cls As inicio = New inicio()

                For Each listObj As getCustomerDocumentDataReturn In JSONDocs_raw.customerDocument
                    Dim docValido As Boolean = cls.docValido(listObj.matrix.document.id)
                    If docValido Then
                        Dim options As options = New options()
                        options.texto = listObj.matrix.document.file.extendedData.family.name
                        options.value = listObj.matrix.document.file.extendedData.referenceNumberDigitalization
                        resp.options.Add(options)
                        contador = contador + 1
                    End If
                Next

                resp.counter = contador

                If (contador = 0) Then
                    resp.mensaje = "No existen Documentos validos que mostrar."
                Else
                    resp.mensaje = "OK"
                End If

            Else
                resp.mensaje = "No existen Documentos."
                resp.counter = 0
            End If
        End If

        jsonDocs = serializer.Serialize(resp)
        Return jsonDocs
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function DocsProdesk(ByVal pantalla As String, ByVal folio As String, ByVal opcion As String) As String
        Dim jsonDocs As String = ""

        Dim documentos As listaDocumentos = New listaDocumentos()
        Dim Docs As Docs = New Docs()

        Docs.mensaje = ""
        Docs.contador = 0

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        'Dim sqlConnection1 As New SqlConnection("Data Source=LTELMXGV\SQL2014;Initial Catalog=bmnpad02_rr;User ID=sa;Password=telepro")
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_Docs_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@OPCION", opcion)
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", folio)
            cmd.Parameters.AddWithValue("@PDK_ID_PANTALLAS", pantalla)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Dim contador As Integer = 0
            Do While reader.Read()
                Dim Doc As documentos = New documentos()
                Doc.nombre = reader(0).ToString() 'Nombre Doc
                Dim strIdDoc = reader(1).ToString() 'ID Doc

                strIdDoc = strIdDoc.Replace("..", "")
                'Dim strIdDocArray = strIdDoc.Split(".")

                'Doc.id = strIdDocArray(1)
                Doc.id = reader(1).ToString() 'ID Doc
                Doc.folio = reader(2).ToString() 'Folio Doc
                Docs.documentos.Add(Doc)
                contador = contador + 1
            Loop

            If (contador = 0) Then
                Docs.mensaje = "No existen Documentos disponibles para esta pantalla."
            Else
                Docs.mensaje = "OK"
                Docs.contador = contador
            End If

        Catch ex As Exception
            Docs.mensaje = "Error al cargar los Documentos (ProDesk)."
        End Try

        sqlConnection1.Close()
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        jsonDocs = serializer.Serialize(Docs)

        Return jsonDocs
    End Function

    Public Class getCustomerDocumentData
        Public option1 As String
        Public customer As customer1 = New customer1()
        Public matrix As matrix = New matrix()
        Public referenceNumberProcedure As String
    End Class

    Public Class customer1
        Public id As String
    End Class

    Public Class matrix
        Public matrixType As String
    End Class

    Public Class customerDocument
        Public customerDocument As List(Of getCustomerDocumentDataReturn) = New List(Of getCustomerDocumentDataReturn)
    End Class

    Public Class getCustomerDocumentDataReturn
        Public matrix As matrixReturn = New matrixReturn()
    End Class

    Public Class matrixReturn
        Public document As documentReturn = New documentReturn()
    End Class

    Public Class documentReturn
        Public id As String
        Public file As fileReturn
    End Class

    Public Class fileReturn
        Public extendedData As extendedDataReturn = New extendedDataReturn()
    End Class

    Public Class extendedDataReturn
        Public referenceNumberDigitalization As String
        Public family As familyReturn = New familyReturn()
    End Class

    Public Class familyReturn
        Public id As String
        Public name As String
    End Class

    Public Class jsonDocs
        Public mensaje As String
        Public counter As Integer
        Public options As List(Of options) = New List(Of options)
    End Class

    Public Class options
        Public value As String
        Public texto As String
    End Class

    Public Class listaDocumentos
        Public mensaje As String
        Public documentos As List(Of documentos) = New List(Of documentos)
    End Class

    Public Class documentos
        Public nombre As String
        Public id As String
        Public folio As String
    End Class

    Public Class Docs
        Public mensaje As String
        Public contador As Integer
        Public documentos As List(Of documentos) = New List(Of documentos)
    End Class


    Private Function docValido(ByVal idDocBBVA As String) As Boolean
        Dim valido As Boolean = False
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "get_DocsValidoBBVA_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@ID_DOC_BBVA", idDocBBVA)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Dim Doc As String = ""

            Do While reader.Read()
                Doc = reader(0)
            Loop

            If (Doc = "") Then
                valido = False
            Else
                If (Doc = "True") Then
                    valido = True
                Else
                    valido = False
                End If
            End If


        Catch ex As Exception
            valido = False
        End Try
        sqlConnection1.Close()
        Return valido
    End Function

    Private Function datosDoc(ByVal id_Doc As String) As String
        Dim valido As Boolean = False
        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Dim Doc As String = ""
        Dim Ver As String = ""
        Dim salida As String = ""
        Try
            cmd.CommandText = "get_Doc_SP"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PDK_ID_DOC_SOLICITUD", id_Doc)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                Doc = reader(0)
                Ver = reader(1)
            Loop

            If ((Doc = "") Or (Ver = "")) Then
                salida = ""
            Else
                salida = Doc + "_" + Ver
            End If
        Catch ex As Exception
            salida = ""
        End Try
        sqlConnection1.Close()
        Return salida
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function OutToProcotiza(ByVal url As String) As String
        Dim Link As String = ""
        Dim vars As String = ""

        If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString()) = 1) Then
            Link = System.Configuration.ConfigurationManager.AppSettings("urlProCotiza").ToString()
            Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)
            Dim css As String = CType(System.Web.HttpContext.Current.Session.Item("css"), String)

            vars = "?userID=" + userID + "&iv_ticket=" + iv_ticket1.Replace("+", "_encode_1").Replace("/", "_encode_2") + "&css=" + css
        Else
            Link = System.Configuration.ConfigurationManager.AppSettings("urlProCotiza").ToString()
            vars = ""
        End If

        Return url + Link + vars
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function OutToProDesk(ByVal url As String) As String
        Dim vars As String = ""
        Dim Link As String = ""
        If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString()) = 1) Then
            Link = System.Configuration.ConfigurationManager.AppSettings("urlProCotiza").ToString()
            vars = "?out=true"
        Else
            Link = "salir.aspx"
            vars = ""
        End If

        Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("cveUsuAcc"), String)
        Dim datauser As DataSet = ProdeskNet.Seguridad.clsUsuario.obtenerValidadUsuario(userID, 1, 0)
        If Not IsNothing(datauser) AndAlso datauser.Tables.Count > 0 AndAlso datauser.Tables(0).Rows.Count() > 0 Then
         
            If Not IsNothing(datauser.Tables(0).Rows(0).Item("PDK_ID_USUARIO")) Then
                Dim datacierre As DataSet = ProdeskNet.Seguridad.clsUsuario.CierraSesionEstatus(datauser.Tables(0).Rows(0).Item("PDK_ID_USUARIO"))
            End If
    
        End If
       
        Return url + Link + vars
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function OutToProDeskMsg(ByVal url As String, ByVal msg As String) As String
        Dim vars As String = ""
        Dim Link As String = ""
        If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("LoginEXTERNO").ToString()) = 1) Then
            Link = System.Configuration.ConfigurationManager.AppSettings("urlProCotiza").ToString()
            vars = "?out=true&msg=" + msg
        Else
            Link = "salir.aspx"
            vars = ""
        End If
        Return url + Link + vars
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function JSONCodigoPostal(ByVal id_sol As String, ByVal opcion As String) As String
        Dim jsonCP As String = ""

        Dim codPostal As CodigoPostal = New CodigoPostal()

        codPostal.codigoPostal = ""

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("Conexion").ToString())
        'Dim sqlConnection1 As New SqlConnection("Data Source=LTELMXGV\SQL2014;Initial Catalog=bmnpad02_rr;User ID=sa;Password=telepro")
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "Sp_GetDatosSol"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@opcion", opcion)
            cmd.Parameters.AddWithValue("@IdSolicitud", id_sol)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Dim contador As Integer = 0
            Do While reader.Read()
                codPostal.codigoPostal = reader(6).ToString()
                codPostal.Colonia = reader(8).ToString()
            Loop

        Catch ex As Exception
            codPostal.codigoPostal = ""
        End Try

        sqlConnection1.Close()
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        jsonCP = serializer.Serialize(codPostal)

        Return jsonCP
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function UpdateColonia(ByVal id_sol As String, ByVal opcion As String, ByVal colonia As String) As String
        Dim MsgResponse As String = ""

        Dim sqlConnection1 As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ConexProcotiza").ToString())
        'Dim sqlConnection1 As New SqlConnection("Data Source=LTELMXGV\SQL2014;Initial Catalog=bmnpad02_rr;User ID=sa;Password=telepro")
        Dim cmd As New SqlCommand
        Dim reader As SqlDataReader
        Try
            cmd.CommandText = "spManejaCodPostal"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Opcion", opcion)
            cmd.Parameters.AddWithValue("@CPO_DS_COLONIA", colonia)
            cmd.Parameters.AddWithValue("@PDK_ID_SECCCERO", id_sol)
            cmd.Connection = sqlConnection1
            sqlConnection1.Open()
            reader = cmd.ExecuteReader()

            Do While reader.Read()
                If (reader(0).ToString() = "1") Then
                    MsgResponse = "Registro actualizado exitosamente"
                Else
                    MsgResponse = "Existio un error al actualizar la Información"
                End If
            Loop

        Catch ex As Exception
            MsgResponse = "Existio un error al actualizar la Información"
        End Try

        sqlConnection1.Close()

        Return MsgResponse
    End Function

    Public Class CodigoPostal
        Public codigoPostal As String
        Public Colonia As String
    End Class
End Class