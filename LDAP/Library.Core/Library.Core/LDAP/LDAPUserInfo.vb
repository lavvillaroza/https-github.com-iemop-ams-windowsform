Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Library.Core.LDAP
    Public Class LdapUserInfo
        Private m_Name As String
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Set(value As String)
                m_Name = value
            End Set
        End Property

        Private m_Position As String
        Public Property Position() As String
            Get
                Return m_Position
            End Get
            Set(value As String)
                m_Position = value
            End Set
        End Property
    End Class
End Namespace