Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Library.Core.Configuration
Imports Library.Core.LDAP
Imports WESMLib.Auth.BO
Imports Oracle.ManagedDataAccess.Client
Imports Npgsql

Namespace WESMLib.Auth.Lib
    Public Class AuthenticationService
        Private _ldapInfo As LdapInfo
        Private _ldapControl As LdapControl
        Private _cd As ConfigDetails
        Private _isConfigValid As Boolean = False
        Private _errMsg As String = ""
        Private m_AccessWindow As String
        Private oraConnStr = New OracleConnectionStringBuilder()

        Public Property AccessWindow() As String
            Get
                Return m_AccessWindow
            End Get
            Set(value As String)
                m_AccessWindow = value
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


        Public Property CRSSDbServer() As String
            Get
                Return If(_cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Server") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Server").Value, "")
            End Get
            Set(value As String)
                _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Server").Value = value
            End Set
        End Property

        'Public Property CRSSPort() As String
        '    Get
        '        Return If(_cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Port") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Port").Value, "")
        '    End Get
        '    Set(value As String)
        '        _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Port").Value = value
        '    End Set
        'End Property

        Public Property CRSSDbDatabase() As String
            Get
                Return If(_cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Database") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Database").Value, "")
            End Get
            Set(value As String)
                _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Database").Value = value
            End Set
        End Property


        Public Property CRSSDbUserId() As String
            Get
                Return If(_cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "UserId") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "UserId").Value, "")
            End Get
            Set(value As String)
                _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "UserId").Value = value
            End Set
        End Property

        Public Property CRSSDbPassword() As String
            Get
                Return If(_cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Password") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Password").Value, "")
            End Get
            Set(value As String)
                _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Password").Value = value
            End Set
        End Property

        Private crss_connectionstring As String
        Public Property CRSSConnectionString() As String
            Get
                Dim postgreConnStr = New NpgsqlConnectionStringBuilder With {
                     .Host = CRSSDbServer,
                     .Database = CRSSDbDatabase,
                     .UserName = CRSSDbUserId,
                     .Password = CRSSDbPassword
                    }
                Return postgreConnStr.ConnectionString
            End Get
            Set(value As String)
                crss_connectionstring = value
            End Set
        End Property

        Public Property AMSDbDatasource() As String
            Get
                Return If(_cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "Datasource") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "Datasource").Value, "")
            End Get
            Set(value As String)
                _cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "Datasource").Value = value
            End Set
        End Property

        Public Property AMSDbUserId() As String
            Get
                Return If(_cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "UserId") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "UserId").Value, "")
            End Get
            Set(value As String)
                _cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "UserId").Value = value
            End Set
        End Property

        Public Property AMSDbPassword() As String
            Get
                Return If(_cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "Password") IsNot Nothing, _cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "Password").Value, "")
            End Get
            Set(value As String)
                _cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "Password").Value = value
            End Set
        End Property

        Private ams_connectionstring As String
        Public Property AMSConnectionString() As String
            Get
                oraConnStr.DataSource = AMSDbDatasource
                oraConnStr.UserID = AMSDbUserId
                oraConnStr.Password = AMSDbPassword
                Return oraConnStr.ConnectionString
            End Get
            Set(value As String)
                ams_connectionstring = value
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
        Private Sub LoadAccessList(userId As String)
            Try
                _accessList = New List(Of String)()
                Dim ars = New AccessRightsService(AMSDbDatasource, AMSDbUserId, AMSDbPassword)

                For Each mStr As String In ars.GetUserModules(userId)
                    _accessList.Add(mStr)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        'Validate Access String
        Public Function HasAccess(access As String) As Boolean
            Dim ret = False
            Dim countAccess As Integer = _accessList.Count
            If countAccess <> 0 Then
                ret = True
            End If
            Return ret
        End Function

        'Authenticate User's Credential
        Public Function AuthUserCredentials(userId As String, password As String, appName As String) As Boolean
            Try
                'If userId.ToUpper.Contains("@IEMOP.PH") Then
                '    userId = userId.Replace("@iemop.ph", "")
                'End If
                _ldapInfo.UserId = userId.ToLower
                _ldapInfo.Password = password
                _ldapControl = New LdapControl()
                If userId.Length = 0 Then
                    Return False
                End If
                'Using AD
                If _ldapControl.EstablishConnection(_ldapInfo) Then
                    'If Not userId.ToUpper.Contains("@IEMOP.PH") Then
                    '    userId = userId.ToLower & "@iemop.ph"
                    'End If
                    LoadAccessList(userId)
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

                If Not _ldapControl.ErrorException Is Nothing Then
                    Throw New Exception(_ldapControl.ErrorException.Message)
                End If

                'Direct Login   
                'If Not userId.ToUpper.Contains("@IEMOP.PH") Then
                '    userId = userId.ToLower & "@iemop.ph"
                'End If
                _userLogged = userId
                LoadAccessList(userId)
                Return True

                'Return False
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
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

                If _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Server") Is Nothing Then
                    _errMsg += "Cannot find config element (LAM Route/Db/Datasource)." & vbLf
                Else
                    If String.IsNullOrEmpty(_cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Server").Value) Then
                        _errMsg += "LAM Route/CRSSDb/Datasource, empty value not valid." & vbLf
                    End If
                End If

                If _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Database") Is Nothing Then
                    _errMsg += "Cannot find config element (LAM Route/CRSSDb/Database)." & vbLf
                Else
                    If String.IsNullOrEmpty(_cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Database").Value) Then
                        _errMsg += "LAM Route/CRSSDb/Database, empty value not valid." & vbLf
                    End If
                End If

                If _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "UserId") Is Nothing Then
                    _errMsg += "Cannot find config element (LAM Route/Db/UserId)." & vbLf
                Else
                    If String.IsNullOrEmpty(_cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "UserId").Value) Then
                        _errMsg += "LAM Route/Db/UserId, empty value not valid." & vbLf
                    End If
                End If

                If _cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Password") Is Nothing Then
                    _errMsg += "Cannot find config element (LAM Route/Db/Password)." & vbLf
                Else
                    If String.IsNullOrEmpty(_cd.GetRouteGroupStringElem("LAM Route", "CRSSDb", "Password").Value) Then
                        _errMsg += "LAM Route/Db/Password, empty value not valid." & vbLf
                    End If
                End If

                If _cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "Datasource") Is Nothing Then
                    _errMsg += "Cannot find config element (LAM Route/Db/Datasource)." & vbLf
                Else
                    If String.IsNullOrEmpty(_cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "Datasource").Value) Then
                        _errMsg += "LAM Route/Db/Datasource, empty value not valid." & vbLf
                    End If
                End If

                If _cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "UserId") Is Nothing Then
                    _errMsg += "Cannot find config element (LAM Route/Db/UserId)." & vbLf
                Else
                    If String.IsNullOrEmpty(_cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "UserId").Value) Then
                        _errMsg += "LAM Route/Db/UserId, empty value not valid." & vbLf
                    End If
                End If

                If _cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "Password") Is Nothing Then
                    _errMsg += "Cannot find config element (LAM Route/Db/Password)." & vbLf
                Else
                    If String.IsNullOrEmpty(_cd.GetRouteGroupStringElem("LAM Route", "AMSDb", "Password").Value) Then
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