'BBV-P-423-RQADM-08 JBEJAR 04/04/2017 Pantalla Consulta INE. 
Imports ProdeskNet.BD
Public Class clsCuestionarioINE
    Private _strError As String = String.Empty
    Private _ID_SOLICITUD As Integer = 0
    Private _EXISTE_INE As Integer = 0
    Private _ESTATUS_INE As Integer = 0

    Sub New()

    End Sub
    Public Property strError As String
        Get
            Return _strError
        End Get
        Set(value As String)
            _strError = value
        End Set
    End Property
    Public Property ID_SOLICITUD As Integer
        Get
            Return _ID_SOLICITUD
        End Get
        Set(value As Integer)
            _ID_SOLICITUD = value
        End Set

    End Property
    Public Property EXISTE_INE As Integer
        Get
            Return _EXISTE_INE
        End Get
        Set(value As Integer)
            _EXISTE_INE = value
        End Set
    End Property
    Public Property ESTATUS_INE As Integer
        Get
            Return _ESTATUS_INE
        End Get
        Set(value As Integer)
            _ESTATUS_INE = value
        End Set
    End Property
    Public Function insertaDatosINE() As Boolean
        insertaDatosINE = False
        Dim dsres As DataSet = New DataSet
        Try
            Dim BD As New clsManejaBD
            BD.AgregaParametro("@id_solicitud", TipoDato.Entero, ID_SOLICITUD)
            BD.AgregaParametro("@existeine", TipoDato.Entero, EXISTE_INE)
            BD.AgregaParametro("@estatusine", TipoDato.Entero, ESTATUS_INE)


            dsres = BD.EjecutaStoredProcedure("spInsertaDatosINE")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        insertaDatosINE = True
                    Else
                        insertaDatosINE = False
                        Throw New Exception(dsres.Tables(0).Rows(0).Item("RESULT").ToString())
                    End If
                Else
                    Throw New Exception("Falla al guardar Cuestionario")
                End If
            Else
                Throw New Exception("Falla al guardar Cuestionario")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return False
        End Try
    End Function
    Public Function GetCuestionarioINE() As DataSet
        Try
            Dim BD As New clsManejaBD
            Dim dsres As New DataSet
            BD.AgregaParametro("@IdSolicitud", TipoDato.Entero, ID_SOLICITUD)

            dsres = BD.EjecutaStoredProcedure("SpGetCuestionarioINE")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If dsres.Tables.Count > 0 Then
                If dsres.Tables(0).Rows.Count > 0 Then
                    Return dsres
                Else
                    Throw New Exception("No se encontraron datos del cuestionario INE")
                End If
            Else
                Throw New Exception("Falla al consultar cuestionario INE")
            End If
        Catch ex As Exception
            _strError = ex.Message.ToString()
            Return Nothing
        End Try
    End Function
End Class
