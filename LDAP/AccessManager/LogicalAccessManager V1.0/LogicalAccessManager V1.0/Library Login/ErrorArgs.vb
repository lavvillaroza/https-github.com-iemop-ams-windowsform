
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace LogicalAccess.LDAP.Lib
    Public Class ErrorArgs : Inherits EventArgs
        Private m_Date As DateTime
        Private m_Exception As Exception
        Private m_ColorCode As ColorCode
        Private m_Message As String
        Public Property [Date]() As DateTime
            Get
                Return m_Date
            End Get
            Set(value As DateTime)
                m_Date = value
            End Set
        End Property

        Public Property Exception() As Exception
            Get
                Return m_Exception
            End Get
            Set(value As Exception)
                m_Exception = value
            End Set
        End Property

        Public Property ColorCode() As ColorCode
            Get
                Return m_ColorCode
            End Get
            Set(value As ColorCode)
                m_ColorCode = value
            End Set
        End Property

        Public Property Message() As String
            Get
                Return m_Message
            End Get
            Set(value As String)
                m_Message = value
            End Set
        End Property

    End Class
End Namespace
