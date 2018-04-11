Imports ProdeskNet.Configurcion
Imports ProdeskNet.Catalogos
Imports System.Data

#Region "trackers"
'INC-B-2019:JDRA:Regresar.
#End Region

Public Class consultaPanelControl
    Inherits System.Web.UI.Page

    Public table2 As DataTable
    Public dsTable2 As DataSet
    Dim BD As New ProdeskNet.BD.clsManejaBD
    Dim dsfill As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim objFlujos As New clsSolicitudes(0)
            Dim dsDataset As New DataSet

            'Response.AddHeader("Refresh", "5")

            'objFlujos.PDK_CLAVE_USUARIO = Session("IdUsua")
            Dim solid As Integer = 0

            objFlujos.PDK_OPE_USU_ASIGNADO = Session("IdUsua")
            hdnClienteNo.Value = Session("IdUsua")
            'Me.lblUsuario.Value = Session("IdUsua")
            'Me.lblUsuarioNombre.Text = filllblUsuNombre(Session("IdUsua"))  'prueba daver.
            hdnUsuario.Value = Session("cveUsuAcc")
            dsDataset = objFlujos.ObtenStatusSolicitud(0)
            Session("dsPanelControl") = dsDataset
            'grvConsulta.DataSource = dsDataset
            'grvConsulta.DataBind()
            Session("Regresar") = Nothing
            'LlenaCombo(1)
            'LlenaCombo(2)
            'LlenaCombo(3)
            'LlenaCombo(4)
            'LlenaCombo(5)
            'LlenaCombo(6)
            'LlenaCombo(7)

            hdnParametro1.Value = Request("NoSolicitud")
            hdnEmpresa.Value = Request("Empresa")
            hdnProducto.Value = Request("Producto")
            hdnPersona.Value = Request("Persona")

            'Else

            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "script", "fnMuestraBuscar();", True)
            'ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "script", "btnBuscarCliente();", True)

        End If

        'MyMaster = Master

    End Sub
    'Private Sub LlenaCombo(ByVal intCombo As Integer)
    '    Dim dsDataset As New DataSet
    '    Dim intEmpresa As Integer = 0
    '    Dim i As Integer = 0

    '    Try
    '        If intCombo = 1 Then
    '            dsDataset = clsEmpresa.ObtenTodos(1)
    '            ddlEmpresa.DataSource = dsDataset
    '            ddlEmpresa.DataTextField = "PDK_EMP_NOMBRE"
    '            ddlEmpresa.DataValueField = "PDK_ID_EMPRESA"
    '            ddlEmpresa.DataBind()
    '        End If

    '        If intCombo = 2 Then
    '            intEmpresa = Val(ddlEmpresa.SelectedValue.ToString)
    '            dsDataset = clsProductos.ObtenerProductoEmp(intEmpresa, 1)
    '            ddlconproducto.DataSource = dsDataset
    '            ddlconproducto.DataTextField = "PDK_PROD_NOMBRE"
    '            ddlconproducto.DataValueField = "PDK_ID_PRODUCTOS"
    '            ddlconproducto.DataBind()

    '        End If

    '        If intCombo = 3 Then
    '            dsDataset = clsPersonalidadJuridica.ObtenTodos
    '            ddltipoper.DataSource = dsDataset
    '            ddltipoper.DataTextField = "PDK_PER_NOMBRE"
    '            ddltipoper.DataValueField = "PDK_ID_PER_JURIDICA"
    '            ddltipoper.DataBind()
    '        End If

    '        'If intCombo = 4 Then
    '        '    ddlEmpresa1.DataSource = filldEmpresas().Tables(0)
    '        '    ddlEmpresa1.DataTextField = "text"
    '        '    ddlEmpresa1.DataValueField = "val"
    '        '    ddlEmpresa1.DataBind()

    '        'End If
    '        'If intCombo = 5 Then
    '        '    ddlDistribuidor.DataSource = filldllDistribuidor(hdnClienteNo.Value).Tables(0)
    '        '    ddlDistribuidor.DataTextField = "text"
    '        '    ddlDistribuidor.DataValueField = "val"
    '        '    ddlDistribuidor.DataBind()
    '        'End If
    '        'If intCombo = 6 Then
    '        '    ddlPersona.DataSource = filldllPerJur().Tables(0)
    '        '    ddlPersona.DataTextField = "text"
    '        '    ddlPersona.DataValueField = "val"
    '        '    ddlPersona.DataBind()
    '        'End If
    '        'If intCombo = 7 Then
    '        '    ddlProducto.DataSource = fillddlProducto(ddlEmpresa1.SelectedValue).Tables(0)
    '        '    ddlProducto.DataTextField = "text"
    '        '    ddlProducto.DataValueField = "val"
    '        '    ddlProducto.DataBind()
    '        'End If


    '    Catch ex As Exception

    '    End Try
    'End Sub
    Function filldEmpresas() As DataSet
        Return BD.EjecutarQuery("SELECT  PDK_ID_EMPRESA val, PDK_EMP_NOMBRE text from PDK_CAT_EMPRESAS WHERE PDK_EMP_ACTIVO=2;")
    End Function

    Function filldllDistribuidor(usuario As Integer) As DataSet        
        Return BD.EjecutarQuery("select cd.PDK_ID_DISTRIBUIDOR val,  PDK_DIST_NOMBRE text from PDK_CAT_DISTRIBUIDOR cd inner join PDK_REL_USU_DIST rud on cd.PDK_ID_DISTRIBUIDOR = rud.PDK_ID_DISTRIBUIDOR where rud.PDK_ID_USUARIO = " & usuario)
    End Function

    Function filllblUsuNombre(usuario As Integer) As String
        dsfill = BD.EjecutarQuery("select isnull(PDK_USU_NOMBRE, '') + ' ' + isnull(PDK_USU_APE_PAT, '') + ' ' + isnull(PDK_USU_APE_MAT, '') as nombre from PDK_USUARIO where PDK_ID_USUARIO = " & usuario)
        Return dsfill.Tables(0).Rows(0)(0)
    End Function

    Function fillddlProducto(ByVal intEmpresa As Integer) As DataSet
        Return BD.EjecutarQuery("select PDK_ID_PRODUCTOS val, PDK_PROD_NOMBRE text from PDK_CAT_PRODUCTOS where PDK_ID_EMPRESA=" & intEmpresa & ";")
    End Function

    Function filldllPerJur() As DataSet
        Return BD.EjecutarQuery("select PDK_ID_PER_JURIDICA val, PDK_PER_NOMBRE text from PDK_CAT_PER_JURIDICA;")
    End Function

    Function filldll(dsfill As DataSet) As DropDownList
        Dim objn As New DropDownList
        objn.DataSource = dsfill.Tables(0)
        objn.DataTextField = "text"
        objn.DataValueField = "val"
        objn.DataBind()
        Return objn
    End Function

End Class