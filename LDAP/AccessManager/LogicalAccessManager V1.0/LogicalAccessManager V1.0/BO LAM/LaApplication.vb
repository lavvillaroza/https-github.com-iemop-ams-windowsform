Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace LogicalAccessManager.BO
    Public Class LaApplication
        Private m_ApplicationId As String
        Public Property ApplicationId() As String
            Get
                Return m_ApplicationId
            End Get
            Set(ByVal value As String)
                m_ApplicationId = value
            End Set
        End Property
        Private m_ApplicationName As String
        Public Property ApplicationName() As String
            Get
                Return m_ApplicationName
            End Get
            Set(ByVal value As String)
                m_ApplicationName = value
            End Set
        End Property
        Private m_Status As Integer
        Public Property Status() As Integer
            Get
                Return m_Status
            End Get
            Set(ByVal value As Integer)
                m_Status = value
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