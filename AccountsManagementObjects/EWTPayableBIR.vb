Public Class EWTPayableBIR
    Private _BillingPeriodNo As Integer
    Public Property BillingPeriodNO() As Integer
        Get
            Return _BillingPeriodNo
        End Get
        Set(ByVal value As Integer)
            _BillingPeriodNo = value
        End Set
    End Property

    Private _SettlementRun As String
    Public Property SettlementRun() As String
        Get
            Return _SettlementRun
        End Get
        Set(ByVal value As String)
            _SettlementRun = value
        End Set
    End Property

    Private _IDNumber As String
    Public Property IDNumber() As String
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As String)
            _IDNumber = value
        End Set
    End Property

    Private _RegID As String
    Public Property RegID() As String
        Get
            Return _RegID
        End Get
        Set(ByVal value As String)
            _RegID = value
        End Set
    End Property

    Private _TaxBase As Decimal
    Public Property TaxBase() As Decimal
        Get
            Return _TaxBase
        End Get
        Set(ByVal value As Decimal)
            _TaxBase = value
        End Set
    End Property




End Class
