'BUG-PD-50 JBEJAR 19/05/2017 TAREA AUTOMATICA ESPECIALES
'BUG-PD-332 JBEJAR 11/01/2018 SE AGREGAN PROPIEDADES Y METODOS PARA LOG AUTOMATICAS. 
Imports System.Data
Public Class clsEspeciales
    Private _strErrSolicitud As String = ""
    Private _ID_SOLICITUD As Integer = 0
    Private _RFC As String = String.Empty
    Private _Mensaje As String = String.Empty
    Private _Tarea_actual As Integer = 0
    Private _Tarea_siguiente As Integer = 0
    Public Property Tarea_siguiente As Integer
        Get
            Return _Tarea_siguiente
        End Get
        Set(ByVal value As Integer)
            _Tarea_siguiente = value
        End Set
    End Property
    Public Property Tarea_Actual As Integer
        Get
            Return _Tarea_actual
        End Get
        Set(ByVal value As Integer)
            _Tarea_actual = value
        End Set
    End Property
    Public Property Mensaje As String
        Get
            Return _Mensaje
        End Get
        Set(ByVal value As String)
            _Mensaje = value
        End Set
    End Property

    Public Property strErrSolicitud As Integer
        Get
            Return _strErrSolicitud
        End Get
        Set(ByVal value As Integer)
            _strErrSolicitud = value
        End Set
    End Property

    Public Property ID_SOLICITUD As Integer
        Get
            Return _ID_SOLICITUD
        End Get
        Set(ByVal value As Integer)
            _ID_SOLICITUD = value
        End Set
    End Property
    Public Property RFC As String
        Get
            Return _RFC
        End Get
        Set(ByVal value As String)
            _RFC = value
        End Set
    End Property

    Public Function GetEspeciales(ByVal intOper) As DataSet

        Dim BD As New ProdeskNet.BD.clsManejaBD

        Try

            Select Case intOper

                Case 1
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    BD.AgregaParametro("@IdSolicitud", ProdeskNet.BD.TipoDato.Entero, ID_SOLICITUD)
                    BD.AgregaParametro("@RFC", ProdeskNet.BD.TipoDato.Cadena, RFC)
                Case 2
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    BD.AgregaParametro("@IdSolicitud", ProdeskNet.BD.TipoDato.Entero, ID_SOLICITUD)
                    BD.AgregaParametro("@RFC", ProdeskNet.BD.TipoDato.Cadena, RFC)
                Case 3
                    BD.AgregaParametro("@opcion", ProdeskNet.BD.TipoDato.Entero, intOper)
                    BD.AgregaParametro("@IdSolicitud", ProdeskNet.BD.TipoDato.Entero, ID_SOLICITUD)
                    BD.AgregaParametro("@RFC", ProdeskNet.BD.TipoDato.Cadena, RFC)

            End Select



            GetEspeciales = BD.EjecutaStoredProcedure("SpGetEspeciales")

            If (BD.ErrorBD) <> "" Then
                strErrSolicitud = BD.ErrorBD
            End If

        Catch ex As Exception
            strErrSolicitud = ex.Message
        End Try

    End Function
    Public Function ConsultaParametro() As DataSet

        Dim _BD As New ProdeskNet.BD.clsManejaBD
        Dim _Opcion As Integer
        _Opcion = 22

        Try

            _BD.AgregaParametro("@opcion", BD.TipoDato.Entero, _Opcion)
            ConsultaParametro = _BD.EjecutaStoredProcedure("spCatalogos")


            If (_BD.ErrorBD) <> "" Then
                strErrSolicitud = _BD.ErrorBD
            End If
        Catch ex As Exception
            strErrSolicitud = ex.Message
        End Try
    End Function
    Public Sub InsertLog()

        Dim _BD As New ProdeskNet.BD.clsManejaBD
        Dim _dataset As DataSet
        Dim _Opcion As Integer
        _Opcion = 23

        Try
            _BD.AgregaParametro("@opcion", BD.TipoDato.Entero, _Opcion)
            _BD.AgregaParametro("@idsolicitud", BD.TipoDato.Entero, _ID_SOLICITUD)
            _BD.AgregaParametro("@mensaje_val_negocio", BD.TipoDato.Cadena, _Mensaje)
            _BD.AgregaParametro("@tarea_actual", BD.TipoDato.Entero, _Tarea_actual)
            _BD.AgregaParametro("@tarea_siguiente", BD.TipoDato.Entero, _Tarea_siguiente)
            _dataset = _BD.EjecutaStoredProcedure("spCatalogos")

        Catch ex As Exception

        End Try
    End Sub
End Class
