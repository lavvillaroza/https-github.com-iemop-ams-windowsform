Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace LogicalAccessManager.BO
    Public Class LaUser
        Private m_UserName As String
        Public Property UserName() As String
            Get
                Return m_UserName
            End Get
            Set(ByVal value As String)
                m_UserName = value
            End Set
        End Property

        Private m_FirstName As String
        Public Property FirstName() As String
            Get
                Return m_FirstName
            End Get
            Set(ByVal value As String)
                m_FirstName = value
            End Set
        End Property

        Private m_MI As String
        Public Property MI() As String
            Get
                Return m_MI
            End Get
            Set(ByVal value As String)
                m_MI = value
            End Set
        End Property

        Private m_LastName As String
        Public Property LastName() As String
            Get
                Return m_LastName
            End Get
            Set(ByVal value As String)
                m_LastName = value
            End Set
        End Property

        Private m_Position As String
        Public Property Position() As String
            Get
                Return m_Position
            End Get
            Set(ByVal value As String)
                m_Position = value
            End Set
        End Property

        Private m_UpdatedBy As String
        Public Property UpdatedBy() As String
            Get
                Return m_UpdatedBy
            End Get
            Set(ByVal value As String)
                m_UpdatedBy = value
            End Set
        End Property

        Private m_UpdatedDate As DateTime
        Public Property UpdatedDate() As DateTime
            Get
                Return m_UpdatedDate
            End Get
            Set(ByVal value As DateTime)
                m_UpdatedDate = value
            End Set
        End Property
    End Class
End Namespace