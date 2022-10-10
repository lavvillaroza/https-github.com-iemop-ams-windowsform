Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace LogicalAccessManager.BO
    Public Class LaModule
        Private m_ModuleId As Integer
        Public Property ModuleId() As Integer
            Get
                Return m_ModuleId
            End Get
            Set(ByVal value As Integer)
                m_ModuleId = value
            End Set
        End Property
        Private m_ApplicationId As String
        Public Property ApplicationId() As String
            Get
                Return m_ApplicationId
            End Get
            Set(ByVal value As String)
                m_ApplicationId = value
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