Public Class ProgressClass
    Private _ProgressIndicator As Integer
    Public Property ProgressIndicator() As Integer
        Get
            Return _ProgressIndicator
        End Get
        Set(ByVal value As Integer)
            _ProgressIndicator = value
        End Set
    End Property

    Private _ProgressMsg As String
    Public Property ProgressMsg() As String
        Get
            Return _ProgressMsg
        End Get
        Set(ByVal value As String)
            _ProgressMsg = value
        End Set
    End Property
End Class
