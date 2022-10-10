Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace LogicalAccessManager.BO
    Public Class LaUserModule
        Private m_UserName As String
        Public Property UserName() As String
            Get
                Return m_UserName
            End Get
            Set(ByVal value As String)
                m_UserName = value
            End Set
        End Property
        Private m_FullName As String
        Public Property FullName() As String
            Get
                Return m_FullName
            End Get
            Set(ByVal value As String)
                m_FullName = value
            End Set
        End Property

        Private m_AppName As String
        Public Property ApplicationName() As String
            Get
                Return m_AppName
            End Get
            Set(ByVal value As String)
                m_AppName = value
            End Set
        End Property

        Private m_ModuleName As String
        Public Property ModuleName() As String
            Get
                Return m_ModuleName
            End Get
            Set(ByVal value As String)
                m_ModuleName = value
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