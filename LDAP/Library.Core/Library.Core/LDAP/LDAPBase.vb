Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.DirectoryServices.Protocols
Imports System.Net

Namespace Library.Core.LDAP
    Public MustInherit Class LdapBase
        Implements IDisposable
        Private Shared _connInfo As LdapInfo
        Private Shared _ldapConn As LdapConnection
        Private Shared _errorException As Exception
        Private _isAuth As Boolean

        Public ReadOnly Property LdapInfo() As LdapInfo
            Get
                Return _connInfo
            End Get
        End Property

        Public ReadOnly Property ErrorException() As Exception
            Get
                Return _errorException
            End Get
        End Property

        Public ReadOnly Property IsAuth() As Boolean
            Get
                Return _isAuth
            End Get
        End Property

        Public Function EstablishConnection(connInfo As LdapInfo) As Boolean
            _connInfo = connInfo

            _errorException = Nothing

            Try
                If String.IsNullOrEmpty(_connInfo.UserId) Then
                    _errorException = New Exception("Empty User ID.")
                    _isAuth = False
                    Return False
                End If

                If String.IsNullOrEmpty(_connInfo.Password) Then
                    _errorException = New Exception("Empty Password.")
                    _isAuth = False
                    Return False
                End If

                '_ldapConn = New LdapConnection(New LdapDirectoryIdentifier(_connInfo.Server, _connInfo.Port))
                '_ldapConn.SessionOptions.SecureSocketLayer = False
                '_ldapConn.SessionOptions.ProtocolVersion = 3
                '_ldapConn.Credential = New NetworkCredential(_connInfo.GetUserDn(), _connInfo.Password)
                '_ldapConn.AuthType = AuthType.Basic
                '_ldapConn.Bind()

                _ldapConn = New LdapConnection(New LdapDirectoryIdentifier(_connInfo.Server, _connInfo.Port))
                _ldapConn.SessionOptions.SecureSocketLayer = False
                _ldapConn.SessionOptions.ProtocolVersion = 3
                Debug.Print(_connInfo.GetUserCn)
                _ldapConn.Credential = New NetworkCredential(_connInfo.GetUserCn, _connInfo.Password)
                _ldapConn.AuthType = AuthType.Basic
                _ldapConn.Bind()

            Catch ex1 As LdapException
                _errorException = New Exception("Ldap error occured: " + ex1.Message, ex1.InnerException)
                _isAuth = False
                Return False
            Catch ex2 As Exception
                _errorException = New Exception("Ldap error occured: " + ex2.Message, ex2.InnerException)
                _isAuth = False
                Return False
            End Try
            _isAuth = True
            Return True
        End Function

        Protected Function LdapQuery(dn As String, filter As String) As List(Of LdapObject)
            _errorException = Nothing

            If _isAuth Then
                _errorException = New Exception("Ldap error occured: Must establish valid connection.")
                Return Nothing
            End If

            Dim ret = New List(Of LdapObject)()

            Try
                Dim sReq = New SearchRequest()
                sReq.DistinguishedName = dn
                sReq.Filter = filter
                sReq.Scope = SearchScope.Subtree

                Dim sRes = DirectCast(_ldapConn.SendRequest(sReq), SearchResponse)

                For i = 0 To sRes.Entries.Count - 1
                    Dim attObj = sRes.Entries(i)
                    For Each s As String In attObj.Attributes.AttributeNames
                        For j = 0 To attObj.Attributes(s).Count - 1
                            ret.Add(New LdapObject() With { _
                                .Attribute = s, _
                                .Value = attObj.Attributes(s)(j).ToString() _
                            })
                        Next
                    Next
                Next
            Catch e As Exception
                _errorException = New Exception("Ldap error occured: " + e.Message, e.InnerException)
            End Try

            Return ret
        End Function


#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            _connInfo = Nothing
            _ldapConn = Nothing
            _errorException = Nothing
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
End Namespace