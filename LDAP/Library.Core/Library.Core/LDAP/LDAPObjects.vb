Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Library.Core.LDAP
    Public Class LdapObject
        Private m_Attribute As String
        Public Property Attribute() As String
            Get
                Return m_Attribute
            End Get
            Set(value As String)
                m_Attribute = value
            End Set
        End Property
        Private m_Value As String
        Public Property Value() As String
            Get
                Return m_Value
            End Get
            Set(value As String)
                m_Value = value
            End Set
        End Property

    End Class
End Namespace