Imports System
Imports System.Web
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Library.Core.Cryptography
Imports System.IO
Imports System.Reflection

Namespace Library.Core.Configuration
    Public Class ConfigDetails
        Implements IDisposable
        Private _config As Config
        Private _configPath As String

        'public Config Config { get; set; }

        Private _cryptSett As New CryptoSettings() With { _
          .Salt = "PEMCPassword", _
          .PasswordPhrase = "PEMCCrypto", _
          .PasswordIteration = 2, _
          .InitVector = "@1B2c3D4e5F6g7H8", _
          .Size = 128 _
        }

        Sub New()
            ' TODO: Complete member initialization 
        End Sub

        Private Shared Function IsFileinUse(ByVal file As FileInfo) As Boolean
            Dim stream As FileStream = Nothing

            Try
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            Catch generatedExceptionName As IOException
                Return True
            Finally
                If stream IsNot Nothing Then
                    stream.Close()
                End If
            End Try
            Return False
        End Function

        Public Sub Reload()
            Dim reload__1 As Boolean = False

            If File.Exists(_configPath) Then
                _config = ConfigSerializer.DeserializeConfig(_configPath)

                For Each r As RouteElem In _config.Routes
                    If r.MustCrypt Then
                        If r.Crypt Then
                            'True
                            For Each s In r.StringElems
                                s.Value = New Crypto(_cryptSett).Decrypt(s.Value)
                            Next
                            r.Crypt = False
                        Else
                            For Each s In r.StringElems
                                s.Value = New Crypto(_cryptSett).Encrypt(s.Value)
                            Next
                            r.Crypt = True
                            reload__1 = True
                        End If
                    End If

                    For Each g In r.GroupElems
                        If g.MustCrypt Then
                            If g.Crypt Then
                                'True
                                For Each s In g.StringElems
                                    s.Value = New Crypto(_cryptSett).Decrypt(s.Value)
                                Next
                                g.Crypt = False
                            Else
                                For Each s In g.StringElems
                                    s.Value = New Crypto(_cryptSett).Encrypt(s.Value)
                                Next
                                g.Crypt = True
                                reload__1 = True
                            End If
                        End If
                    Next
                Next

                If reload__1 Then
                    SaveConfig()
                    Reload()
                End If
            End If
        End Sub

        'Save Configuration
        Public Sub SaveConfig()
            ConfigSerializer.SerializeConfig(_config, _configPath)
        End Sub

        Public Sub SaveConfig(ByVal config As Config)
            ConfigSerializer.SerializeConfig(config, _configPath)
        End Sub

        Public Sub SaveConfig(ByVal config As Config, ByVal configPath As String)
            ConfigSerializer.SerializeConfig(config, configPath)
        End Sub

        '
        Public Function GetRouteElems() As List(Of RouteElem)
            'Get List of Routes
            Return _config.Routes()
        End Function

        Public Function GetRouteElem(ByVal routeName As String) As RouteElem
            'Get Specific Route
            Return GetRouteElems().Find(Function(x) x.Name = routeName)
        End Function

        Public Function GetRouteStringElems(ByVal routeName As String) As List(Of StringElem)
            'Get List of Strings in Route Element
            Return GetRouteElem(routeName).StringElems
        End Function

        Public Function GetRouteStringElem(ByVal routeName As String, ByVal routeStringName As String) As StringElem
            'Get String 
            Return GetRouteElem(routeName).StringElems.Find(Function(x) x.Name = routeStringName)
        End Function

        Public Function GetRouteGroupElems(ByVal routeName As String) As List(Of GroupElem)
            Return GetRouteElem(routeName).GroupElems
        End Function

        Public Function GetRouteGroupElem(ByVal routeName As String, ByVal routeGroupName As String) As GroupElem
            Return GetRouteGroupElems(routeName).Find(Function(x) x.Name = routeGroupName)
        End Function

        Public Function GetRouteGroupStringElems(ByVal routeName As String, ByVal routeGroupName As String) As List(Of StringElem)
            Return GetRouteGroupElem(routeName, routeGroupName).StringElems
        End Function

        Public Function GetRouteGroupStringElem(ByVal routeName As String, ByVal routeGroupName As String, ByVal routeGroupStringName As String) As StringElem            
            Return GetRouteGroupStringElems(routeName, routeGroupName).Find(Function(x) x.Name = routeGroupStringName)
        End Function

        Public Sub New(ByVal configPath As String)
            _configPath = configPath
            'If File.Exists(configPath) Then
            '    _configPath = configPath
            'Else
            '    _configPath = configPath 'HttpContext.Current.Server.MapPath("\" + configPath) 'configPath 'HttpContext.Current.Server.MapPath("\" + configPath)
            'End If
            Reload()
        End Sub

#Region "IDisposable Members"

        Protected Overridable Sub Dispose(ByVal dispose__1 As Boolean)
            If dispose__1 Then
            End If
            _config = Nothing
            _configPath = Nothing
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

#End Region
    End Class
End Namespace