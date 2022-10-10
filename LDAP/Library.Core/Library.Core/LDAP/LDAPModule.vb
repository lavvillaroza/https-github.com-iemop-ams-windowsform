Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Namespace Library.Core.LDAP
    Public Class LdapModule
        Inherits LdapControl
        Public Function GetObjects() As List(Of LdapObject)
            Dim ret = New List(Of LdapObject)()
            Dim titleFilter = String.Format("(&(objectClass=user)({0}))", LdapInfo.GetUserCn())
            Return ret
        End Function

        Public Function GetObject(attribute As String) As LdapObject
            Return GetObjects().Find(Function(x) x.Attribute = attribute)
        End Function
    End Class
End Namespace