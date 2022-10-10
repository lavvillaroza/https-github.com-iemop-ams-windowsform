Public Class DALCommand
    Private _sql As String
    Public Property Sql() As String
        Get
            Return _sql
        End Get
        Set(value As String)
            _sql = value
        End Set
    End Property

    Private _args As Object()
    Public Property Args() As Object()
        Get
            Return _args
        End Get
        Set(value As Object())
            _args = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(sql__1 As String)
        Sql = sql__1
    End Sub

    Public Sub New(sql__1 As String, ParamArray args__2 As Object())
        Sql = sql__1
        Args = args__2
    End Sub
End Class
