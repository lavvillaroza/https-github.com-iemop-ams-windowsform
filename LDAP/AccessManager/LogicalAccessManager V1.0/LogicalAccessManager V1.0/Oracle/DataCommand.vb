Imports System.Collections.Generic
Imports System.Text

Namespace Library.OraDAL
    Public Class DataCommand
        Public Property Sql() As String
            Get
                Return m_Sql
            End Get
            Set(ByVal value As String)
                m_Sql = value
            End Set
        End Property
        Private m_Sql As String
        Public Property Args() As Object()
            Get
                Return m_Args
            End Get
            Set(ByVal value As Object())
                m_Args = value
            End Set
        End Property
        Private m_Args As Object()

        Public Sub New()
        End Sub
        Public Sub New(ByVal sql__1 As String)
            Sql = sql__1
        End Sub
        Public Sub New(ByVal sql__1 As String, ByVal ParamArray args__2 As Object())
            Sql = sql__1
            Args = args__2
            For i As Integer = 0 To Args.Length - 1
                If Args(i) Is Nothing Then
                    Args(i) = ""
                End If
            Next
        End Sub
    End Class
End Namespace