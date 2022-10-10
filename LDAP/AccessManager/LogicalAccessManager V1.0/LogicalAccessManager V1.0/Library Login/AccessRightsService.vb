Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports LogicalAccess.LDAP.BO

Namespace LogicalAccess.LDAP.Lib
    Public Class AccessRightsService
        Private _ud As UserDetails

        Public Function GetUserModules(username As String, appName As String) As List(Of String)
            Return _ud.GetUserModules(username, appName)
        End Function

        Public Sub New(datasource As String, userId As String, password As String)
            _ud = New UserDetails(datasource, userId, password)
        End Sub
    End Class
End Namespace