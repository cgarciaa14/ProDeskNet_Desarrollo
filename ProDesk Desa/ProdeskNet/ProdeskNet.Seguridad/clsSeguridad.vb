' BUG-PD-13  GVARGAS  28/02/2017 AutoLogIn

Imports System.Data.Common
Imports System.Data
Imports System.Configuration
Imports ProdeskNet.BD
Public Class clsSeguridad
    Private strErrSeguridad As String = ""
    Private objFactory As DbProviderFactory = Nothing
    Private objCommand As DbCommand = Nothing

    Public ReadOnly Property ErrorSeguridad() As String
        Get
            Return strErrSeguridad
        End Get
    End Property

    Public Function EncriptaDesencriptaCadena(ByVal strCadena As String, _
                                              Optional ByVal blnDesencripta As Boolean = False) As String
        Dim dtsResp As New DataSet
        strErrSeguridad = ""
        EncriptaDesencriptaCadena = ""

        If Trim(strCadena) <> "" Then
            Try
                Dim objSD As New ProdeskNet.BD.clsManejaBD
                strErrSeguridad = objSD.ErrorBD
                If Trim(strErrSeguridad) <> "" Then Exit Function

                With objSD
                    .AgregaParametro("@sCad", ProdeskNet.BD.TipoDato.Cadena, Trim(strCadena))
                    strErrSeguridad = objSD.ErrorBD
                    If Trim(strErrSeguridad) <> "" Then Exit Function

                    If blnDesencripta Then
                        dtsResp = .EjecutaStoredProcedure("procDesencripta")
                    Else
                        dtsResp = .EjecutaStoredProcedure("procEncripta")
                    End If

                    strErrSeguridad = .ErrorBD
                    If Trim(strErrSeguridad) <> "" Then Exit Function

                    EncriptaDesencriptaCadena = dtsResp.Tables(0).Rows(0).Item("RESULTADO")
                End With
            Catch ex As Exception
                strErrSeguridad = ex.Message
            End Try
        End If
    End Function
   

    Public Function ValidaUsuario(ByVal strUsuario As String, _
                                  ByVal strPwd As String) As ProdeskNet.Seguridad.clsUsuario

        'Función que valida el acceso al sistema
        Dim objUsu As New clsUsuario
        Dim strEncripta As String = ""
        Dim strBD As New DataSet

        strErrSeguridad = ""
        Try
            'se obtiene la información del usuario
            objUsu = New clsUsuario(strUsuario)
            ''strBD = objUsu.ObtenerUsuario(strUsuario, strErrSeguridad)
            'objUsu = New clsUsuarios(strUsuario, strErrSeguridad)
            strErrSeguridad = objUsu.ErrorUsuario

            If Trim$(strErrSeguridad) = "" Then
                If Trim(objUsu.PDK_USU_CLAVE) <> "" Then
                    If objUsu.PDK_USU_ACTIVO = 3 Then
                        strErrSeguridad = "Usuario Inactivo"
                    Else

                        If (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("ValidarPassword").ToString()) = 1) Then
                            strEncripta = EncriptaDesencriptaCadena(Trim$(strPwd), False)
                            If Trim$(strEncripta) <> Trim(objUsu.PDK_USU_CONTRASENA) Then
                                strErrSeguridad = "Contraseña Incorrecta"
                            End If
                        End If
                    End If
                Else
                        strErrSeguridad = "El usuario NO existe"
                End If

            End If
        Catch e As Exception
            strErrSeguridad = e.Message
        End Try
        Return objUsu
    End Function
End Class

