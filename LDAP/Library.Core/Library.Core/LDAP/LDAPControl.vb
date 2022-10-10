Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Library.Core.Configuration

Namespace Library.Core.LDAP
    Public Class LdapControl
        Inherits LdapBase
        Public Function GetAccessRights() As List(Of String)
            Dim ret = New List(Of String)()

            For Each o In (From x In New LdapModule().GetObjects() Select x)
                Dim str = o.Value
                ret.Add((str.Split(",")(0)).Split("=")(1).Trim().ToString())
            Next

            ret = ret.Distinct().ToList()
            Return ret
        End Function

        Public Function HasAccessRight(value As String) As Boolean            
            If Not String.IsNullOrEmpty((From x In GetAccessRights() Where x = value Select x).FirstOrDefault()) Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace