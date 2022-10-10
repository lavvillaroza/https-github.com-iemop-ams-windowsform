Public Class StatementOfAccount

    Private SOANumber As Long
    Public Property _SOANumber() As Long
        Get
            Return SOANumber
        End Get
        Set(ByVal value As Long)
            SOANumber = value
        End Set
    End Property

    Private SOADate As Date
    Public Property _SOADate() As Date
        Get
            Return SOADate
        End Get
        Set(ByVal value As Date)
            SOADate = value
        End Set
    End Property

    Private DueDate As Date
    Public Property _DueDate() As Date
        Get
            Return DueDate
        End Get
        Set(ByVal value As Date)
            DueDate = value
        End Set
    End Property

    Private lstDetails As List(Of StatementOfAccountMainDetails)
    Public Property _lstDetails() As List(Of StatementOfAccountMainDetails)
        Get
            Return lstDetails
        End Get
        Set(ByVal value As List(Of StatementOfAccountMainDetails))
            lstDetails = value
        End Set
    End Property

    Private IDNumber As String
    Public Property _IDNumber() As String
        Get
            Return IDNumber
        End Get
        Set(ByVal value As String)
            IDNumber = value
        End Set
    End Property


    Public Sub New(SOANumber As Long, SOADate As Date, DueDate As Date, Details As List(Of StatementOfAccountMainDetails), IDNumber As String)
        SOANumber = _SOANumber
        SOADate = _SOADate
        DueDate = _DueDate
        Details = _lstDetails
        IDNumber = _IDNumber
    End Sub

    Public Sub New()
        _SOANumber = 0
        _SOADate = Nothing
        _DueDate = Nothing
        _lstDetails = New List(Of StatementOfAccountMainDetails)
        _IDNumber = ""
    End Sub
End Class