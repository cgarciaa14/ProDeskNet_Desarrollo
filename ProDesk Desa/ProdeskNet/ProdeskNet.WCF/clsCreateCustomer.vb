'BBV-P-423: AMR: 12/04/2017 RQADM-18 Evaluación

Public Class clsCreateCustomer

    Private _strerror As String = String.Empty

    Public ReadOnly Property Strerror As String
        Get
            Return _strerror
        End Get
    End Property


    Public Class JSON
        Public nombre As String = String.Empty
        Public priape As String = String.Empty
        Public segape As String = String.Empty
        Public rfc As String = String.Empty
        Public homonim As String = String.Empty
        Public fecnaci As String = String.Empty
        Public codpost As String = String.Empty
        Public numinte As String = String.Empty
        Public pecnaci As String = String.Empty
        Public fecingr As String = String.Empty
        Public codpais As String = String.Empty
        Public estado As String = String.Empty
        Public ocuphab As String = String.Empty
        Public sexo As String = String.Empty
        Public calle As String = String.Empty
        Public numexte As String = String.Empty
        Public ecalle2 As String = String.Empty
        Public dfecre As String = String.Empty
        Public nacion As String = String.Empty
        Public escolar As String = String.Empty
        Public centrab As String = String.Empty
        Public edocivi As String = String.Empty
        Public ecalle1 As String = String.Empty
        Public delmuni As String = String.Empty
        Public entnaci As String = String.Empty
        Public colonia As String = String.Empty
        Public puesto As String = String.Empty
        Public curp As String = String.Empty
        Public numescr As String = String.Empty
        Public perjuri As String = String.Empty
        Public cveiden As String = String.Empty
        Public numiden As String = String.Empty
        Public fvenide As String = String.Empty
        Public ingrcta As Integer = 0
        Public gtosfij As Integer = 0
        Public domirec As String = String.Empty
        Public actieco As String = String.Empty
        Public situlab As String = String.Empty
        Public ingrvar As Integer = 0
        Public sitvivi As Integer = 0
        Public actogir As String = String.Empty
        Public sdomedi As Integer = 0
        Public ingrnom As Integer = 0
        Public antempr As Integer = 0
        Public profesi As String = String.Empty
        Public depecon As Integer = 0
        Public gtosalq As Integer = 0
        Public ingrotr As Integer = 0
        Public domnomi As String = String.Empty
        Public ingrren As Integer = 0
        Public apoder1 As String = String.Empty
        Public valvivi As Integer = 0
        Public gtoship As Integer = 0
        Public gtospre As Integer = 0
    End Class

    Public Class msjerr
        Public message As String
        Public status As String
    End Class

    Public Class numcliente
        Public numclie As String = String.Empty
    End Class

    Sub New()
        End Sub


        Public Function CreateCustomer(ByVal intSolID As Integer) As String
            Dim strNuevoCte As String = String.Empty
            Dim json As New JSON()

            json.nombre = "KARINA"
            json.priape = "VILLORDO"
            json.segape = "AXA"
            json.rfc = "VIAK900812"
            json.homonim = "XYZ"
            json.fecnaci = "1990-08-12"
            json.codpost = "01000"
            json.numinte = ""
            json.pecnaci = "MEX"
            json.fecingr = "2016-12-30"
            json.codpais = "MEX"
            json.estado = "DF"
            json.ocuphab = ""
            json.sexo = "F"
            json.calle = "AV SANTA ANA"
            json.numexte = "327"
            json.ecalle2 = ""
            json.dfecre = "1991-08-12"
            json.nacion = "MEX"
            json.escolar = ""
            json.centrab = ""
            json.edocivi = "B"
            json.ecalle1 = ""
            json.delmuni = "ALVARO OBREGON"
            json.entnaci = "DF"
            json.colonia = "SAN ANGEL"
            json.puesto = ""
            json.curp = ""
            json.numescr = ""
            json.perjuri = "F32"
            json.cveiden = "P"
            json.numiden = "35712436"
            json.fvenide = "2019-12-30"
            json.ingrcta = 0
            json.gtosfij = 0
            json.domirec = "N"
            json.actieco = "2T"
            json.situlab = "4"
            json.ingrvar = 0
            json.sitvivi = 1
            json.actogir = ""
            json.sdomedi = 0
            json.ingrnom = 0
            json.antempr = 0
            json.profesi = "G"
            json.depecon = 2
            json.gtosalq = 0
            json.ingrotr = 0
            json.domnomi = "N"
            json.ingrren = 0
            json.apoder1 = ""
            json.valvivi = 1000000
            json.gtoship = 0
            json.gtospre = 1


            Dim restGT As RESTful = New RESTful()
            Dim jsonResult As String
            Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()

            Dim jsonBODY As String = serializer.Serialize(json)
            Dim userID As String = "mx.cpbast05"
            Dim iv_ticket1 = "v8pfYRp5PvMeNQ8zzIs7xWu1Ewqox9LShF7wn3DG5hXOGfm2KnTInwjhFHO1PdEE"

            'Dim userID As String = CType(System.Web.HttpContext.Current.Session.Item("userID"), String)
            'Dim iv_ticket1 As String = CType(System.Web.HttpContext.Current.Session.Item("iv_ticket"), String)

            restGT.Uri = System.Configuration.ConfigurationManager.AppSettings.Item("createCustomer")
            restGT.buscarHeader("ResponseWarningDescription")
            jsonResult = restGT.ConnectionPost(userID, iv_ticket1, jsonBODY)

            Dim srrSerialer As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim alert = srrSerialer.Deserialize(Of msjerr)(jsonResult)

            If restGT.IsError Then
                If restGT.MensajeError <> "" Then
                    _strerror = "Servicio Web: " & restGT.MensajeError
                    Return Nothing
                    Exit Function
                Else
                    If Not alert.message = Nothing Then
                        _strerror = "Servicio Web: " & alert.message
                        Return Nothing
                        Exit Function
                    End If
                End If
            End If

        Dim desserializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsoncte As numcliente = serializer.Deserialize(Of numcliente)(jsonResult)

        strNuevoCte = jsoncte.numclie

        Return strNuevoCte

        End Function

        Private Function ObtenerDatos(ByVal intSolID As Integer) As DataSet
            Dim dts = New DataSet()

            Return dts
        End Function

    End Class
