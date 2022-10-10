Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Library.Core.LDAP
    Public Class LdapInfo
        'Properties
        Private m_Server As String
        Private m_Port As Integer
        Private m_BaseDn As String
        Private m_UserId As String
        Private m_Password As String
        Public Property Server() As String
            Get
                Return m_Server
            End Get
            Set(value As String)
                m_Server = value
            End Set
        End Property

        Public Property Port() As Integer
            Get
                Return m_Port
            End Get
            Set(value As Integer)
                m_Port = value
            End Set
        End Property

        Public Property BaseDn() As String
            Get
                Return m_BaseDn
            End Get
            Set(value As String)
                m_BaseDn = value
            End Set
        End Property

        Public Property UserId() As String
            Get
                Return m_UserId
            End Get
            Set(value As String)
                m_UserId = value
            End Set
        End Property

        Public Property Password() As String
            Get
                Return m_Password
            End Get
            Set(value As String)
                m_Password = value
            End Set
        End Property


        'Functions
        Public Function GetUserCn() As String
            Return String.Format("iemop\{0}", UserId)
        End Function

        Public Function GetUserDn() As String            
            Return String.Format("{0},{1}", BaseDn, GetUserCn())
        End Function
    End Class
End Namespace