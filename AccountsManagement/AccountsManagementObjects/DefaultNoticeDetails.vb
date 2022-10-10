Public Class DefaultNoticeDetails
    Public Sub New()
        Me._BillingPeriod = Nothing
        Me._Particulars = Nothing
        Me._Amount = 0
        Me._DNNumber = 0
    End Sub

    Private _BillingPeriod As String
    Public Property BillingPeriod() As String
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As String)
            _BillingPeriod = value
        End Set
    End Property

    Private _Particulars As String
    Public Property Particulars() As String
        Get
            Return _Particulars
        End Get
        Set(ByVal value As String)
            _Particulars = value
        End Set
    End Property

    Private _Amount As Decimal
    Public Property Amount() As Decimal
        Get
            Return _Amount
        End Get
        Set(ByVal value As Decimal)
            _Amount = value
        End Set
    End Property

    Private _DNNumber As Decimal
    Public Property DNNumber() As Decimal
        Get
            Return _DNNumber
        End Get
        Set(ByVal value As Decimal)
            _DNNumber = value
        End Set
    End Property

End Class
