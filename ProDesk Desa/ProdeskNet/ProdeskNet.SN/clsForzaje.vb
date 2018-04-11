#Region "TRACKERS"
'BUG-PD-219:JBEJAR:03/10/2017 SE MEJORA EL LOG DE FORZJES
#End Region

Imports System.Text
Imports System.Data
Imports ProdeskNet.BD

Public Class clsForzaje
#Region "variables privadas"
    Private _Forzaje As Integer
    Private _Id_Solicitud As Integer = 0
    Private _Tipo_Forzaje As String = String.Empty
    Private _Tarea_Anterior As Integer = 0
    Private _Tarea_Siguiente As Integer = 0
    Private _Usuario_Forza As Integer = 0
    Private _strError As String = String.Empty
#End Region

#Region "Propiedades Publicas"

    Public Property Id_Solicitud As Integer
        Get
            Return _Id_Solicitud
        End Get
        Set(ByVal value As Integer)
            _Id_Solicitud = value
        End Set
    End Property

    Public Property Forzaje As String
        Get
            Return _Forzaje
        End Get
        Set(ByVal value As String)
            _Forzaje = value
        End Set
    End Property


    Public Property Tipo_Forzaje As String
        Get
            Return _Tipo_Forzaje
        End Get
        Set(ByVal value As String)
            _Tipo_Forzaje = value
        End Set
    End Property

    Public Property Tarea_Anterior As Integer
        Get
            Return _Tarea_Anterior
        End Get
        Set(value As Integer)
            _Tarea_Anterior = value
        End Set

    End Property

    Public Property Tarea_Siguiente As Integer
        Get
            Return _Tarea_Siguiente
        End Get
        Set(ByVal value As Integer)
            _Tarea_Siguiente = value
        End Set
    End Property

    Public Property Usuario_Forza As Integer
        Get
            Return _Usuario_Forza
        End Get
        Set(ByVal value As Integer)
            _Usuario_Forza = value
        End Set
    End Property


    Public Property strError As String
        Get
            Return _strError
        End Get
        Set(ByVal value As String)
            _strError = value
        End Set
    End Property
#End Region

#Region "Metodos y funciones"

    Public Function insertaForzaje() As Boolean
        insertaForzaje = False
        Dim dsres As DataSet = New DataSet
        Dim BD As New clsManejaBD
        Try
            BD.AgregaParametro("@id_solicitud", TipoDato.Entero, Id_Solicitud)
            BD.AgregaParametro("@tipo_forzaje", TipoDato.Cadena, Tipo_Forzaje)
            BD.AgregaParametro("@tarea_anterior", TipoDato.Entero, Tarea_Anterior)
            BD.AgregaParametro("@tarea_siguiente", TipoDato.Entero, Tarea_Siguiente)
            BD.AgregaParametro("@usuario_for", TipoDato.Entero, Usuario_Forza)
            dsres = BD.EjecutaStoredProcedure("spInsertaForzaje")
            If (BD.ErrorBD) <> "" Then
                _strError = BD.ErrorBD
            End If
            If (dsres.Tables.Count > 0) Then
                If (dsres.Tables(0).Rows.Count > 0) Then
                    If (dsres.Tables(0).Rows(0).Item("RESULT").ToString = "EXITOSO") Then
                        insertaForzaje = True
                    Else
                        insertaForzaje = False
                        Throw New Exception(dsres.Tables(0).Rows(0).Item("RESULT").ToString())
                    End If
                Else
                    Throw New Exception("Falla al guardar forzaje")
                End If
            Else
                Throw New Exception("Falla al guardar forzaje")
            End If
        Catch ex As Exception

        End Try
    End Function

#End Region

End Class
