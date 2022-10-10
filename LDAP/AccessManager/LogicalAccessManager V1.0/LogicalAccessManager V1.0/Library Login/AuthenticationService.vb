Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Library.Core.Configuration
Imports Library.Core.LDAP
Imports LogicalAccess.LDAP.BO

Namespace LogicalAccess.LDAP.Lib
    Public Class AuthenticationService
                
        Private _ldapInfo As LdapInfo
        Private _ldapControl As LdapControl
        Private _cd As ConfigDetails
        Private _isConfigValid As Boolean = False
        Private _errMsg As String = ""
        Private m_AccessWindow As String

        Sub New()
            ' TODO: Complete member initialization             
        End Sub

        Public Property AccessWindow() As String
            Get
                Return m_AccessWindow
            End Get
            Set(value As String)
                m_AccessWindow = value
            End Set
        End Property

        Public Property DbDatasource() As String
            Get
                Return If(_cd.GetRouteGroupStringElem("LAM Route", "Db", "Datasource") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "Db", "Datasource").Value, "")
            End Get
            Set(value As String)
                _cd.GetRouteGroupStringElem("LAM Route", "Db", "Datasource").Value = value
            End Set
        End Property

        Public Property DbUserId() As String
            Get
                Return If(_cd.GetRouteGroupStringElem("LAM Route", "Db", "UserId") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "Db", "UserId").Value, "")
            End Get
            Set(value As String)
                _cd.GetRouteGroupStringElem("LAM Route", "Db", "UserId").Value = value
            End Set
        End Property

        Public Property DbPassword() As String
            Get
                Return If(_cd.GetRouteGroupStringElem("LAM Route", "Db", "Password") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "Db", "Password").Value, "")
            End Get
            Set(value As String)
                _cd.GetRouteGroupStringElem("LAM Route", "Db", "Password").Value = value
            End Set
        End Property

        Public Property AdServer() As String
            Get                
                Return If(_cd.GetRouteGroupStringElem("LAM Route", "Ad", "Server") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "Ad", "Server").Value, "")
            End Get
            Set(value As String)
                _cd.GetRouteGroupStringElem("LAM Route", "Ad", "Server").Value = value
            End Set
        End Property

        Public Property AdPort() As String
            Get
                Return If(_cd.GetRouteGroupStringElem("LAM Route", "Ad", "Port") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "Ad", "Port").Value, "")
            End Get
            Set(value As String)                
                _cd.GetRouteGroupStringElem("LAM Route", "Ad", "Port").Value = value
            End Set
        End Property
        Public Property AdBaseDn() As String
            Get
                Return If(_cd.GetRouteGroupStringElem("LAM Route", "Ad", "BaseDn") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "Ad", "BaseDn").Value, "")
            End Get
            Set(value As String)                
                _cd.GetRouteGroupStringElem("LAM Route", "Ad", "BaseDn").Value = value
            End Set
        End Property
        Public ReadOnly Property IsConfigValid() As Boolean
            Get
                Return _isConfigValid
            End Get
        End Property
        Public ReadOnly Property GetErrMessage() As String
            Get
                Return _errMsg
            End Get
        End Property

        Public Sub SaveConfig()
            _cd.SaveConfig()
        End Sub

        Private _userLogged As String
        Public ReadOnly Property UserLogged() As String
            Get
                Return _userLogged
            End Get
        End Property

        Private _isAuth As Boolean
        Public ReadOnly Property IsAuth() As Boolean
            Get
                Return _isAuth
            End Get
        End Property

        Private _accessList As List(Of String)
        Private Sub LoadAccessList(userId As String, appName As String)
            _accessList = New List(Of String)()
            Dim ars = New AccessRightsService(DbDatasource, DbUserId, DbPassword)

            For Each mStr As String In ars.GetUserModules(userId, appName)
                _accessList.Add(mStr)
            Next
        End Sub

        'Validate Access String
        Public Function HasAccess(access As String) As Boolean
            Dim ret = False

            Dim temp = _accessList.Find(Function(x) x = access)
            If Not String.IsNullOrEmpty(temp) Then
                ret = True
            End If

            Return ret
        End Function

        'Authenticate User's Credential
        Public Function AuthUserCredentials(userId As String, password As String, appName As String) As Boolean
            _ldapInfo.UserId = userId
            _ldapInfo.Password = password

            _ldapControl = New LdapControl()
            If _ldapControl.EstablishConnection(_ldapInfo) Then
                LoadAccessList(userId, appName)
                If HasAccess(AccessWindow) Then
                    _userLogged = userId
                    _isAuth = True
                    Return True
                Else
                    _userLogged = ""
                    _isAuth = False
                    Return False
                End If
            End If
            Return False
        End Function

        'Constructor
        Public Sub New(configPath As String)
            Try
            _cd = New ConfigDetails(configPath)

            _errMsg = String.Empty

            If _cd.GetRouteGroupStringElem("LAM Route", "Ad", "Server") Is Nothing Then
                _errMsg += "Cannot find config element (LAM Route/Ad/Server)." & vbLf
            Else
                If String.IsNullOrEmpty(_cd.GetRouteGroupStringElem("LAM Route", "Ad", "Server").Value) Then
                    _errMsg += "LAM Route/Ad/Server, empty value not valid." & vbLf
                End If
            End If

            If _cd.GetRouteGroupStringElem("LAM Route", "Ad", "Port") Is Nothing Then
                _errMsg += "Cannot find config element (LAM Route/Ad/Port)." & vbLf
            Else
                If String.IsNullOrEmpty(_cd.GetRouteGroupStringElem("LAM Route", "Ad", "Port").Value) Then
                    _errMsg += "LAM Route/Ad/Port, empty value not valid." & vbLf
                End If
            End If

            If _cd.GetRouteGroupStringElem("LAM Route", "Ad", "BaseDn") Is Nothing Then
                _errMsg += "Cannot find config element (LAM Route/Ad/BaseDn)." & vbLf
            Else
                If String.IsNullOrEmpty(_cd.GetRouteGroupStringElem("LAM Route", "Ad", "BaseDn").Value) Then
                    _errMsg += "LAM Route/Ad/BaseDn, empty value not valid." & vbLf
                End If
            End If

            If _cd.GetRouteGroupStringElem("LAM Route", "Db", "Datasource") Is Nothing Then
                _errMsg += "Cannot find config element (LAM Route/Db/Datasource)." & vbLf
            Else
                If String.IsNullOrEmpty(_cd.GetRouteGroupStringElem("LAM Route", "Db", "Datasource").Value) Then
                    _errMsg += "LAM Route/Db/Datasource, empty value not valid." & vbLf
                End If
            End If

            If _cd.GetRouteGroupStringElem("LAM Route", "Db", "UserId") Is Nothing Then
                _errMsg += "Cannot find config element (LAM Route/Db/UserId)." & vbLf
            Else
                If String.IsNullOrEmpty(_cd.GetRouteGroupStringElem("LAM Route", "Db", "UserId").Value) Then
                    _errMsg += "LAM Route/Db/UserId, empty value not valid." & vbLf
                End If
            End If

            If _cd.GetRouteGroupStringElem("LAM Route", "Db", "Password") Is Nothing Then
                _errMsg += "Cannot find config element (LAM Route/Db/Password)." & vbLf
            Else
                If String.IsNullOrEmpty(_cd.GetRouteGroupStringElem("LAM Route", "Db", "Password").Value) Then
                    _errMsg += "LAM Route/Db/Password, empty value not valid." & vbLf
                End If
            End If

                If String.IsNullOrEmpty(_errMsg) Then
                _isConfigValid = True

                'Setup LDAP Parameters
                _ldapInfo = New LdapInfo()
                _ldapInfo.Server = AdServer
                _ldapInfo.Port = Convert.ToInt32(AdPort)
                _ldapInfo.BaseDn = AdBaseDn
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub
    End Class
End Namespace