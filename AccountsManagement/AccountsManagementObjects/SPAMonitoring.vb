Public Class SPAMonitoring
    Private _WESMBillSummaryInfo As WESMBillSummary
    Public Property WESMBillSummaryInfo() As WESMBillSummary
        Get
            Return _WESMBillSummaryInfo
        End Get
        Set(ByVal value As WESMBillSummary)
            _WESMBillSummaryInfo = value
        End Set
    End Property

    Private _MonthlyDueDate As Date
    Public Property MonthlyDueDate() As Date
        Get
            Return _MonthlyDueDate
        End Get
        Set(ByVal value As Date)
            _MonthlyDueDate = value
        End Set
    End Property


    Private _MonthlyAmortization As Decimal
    Public Property MonthlyAmortization() As Decimal
        Get
            Return _MonthlyAmortization
        End Get
        Set(ByVal value As Decimal)
            _MonthlyAmortization = value
        End Set
    End Property


    Private _PrincipalAmount As Decimal
    Public Property PrincipalAmount() As Decimal
        Get
            Return _PrincipalAmount
        End Get
        Set(ByVal value As Decimal)
            _PrincipalAmount = value
        End Set
    End Property

    Private _InterestAmount As Decimal
    Public Property InterestAmount() As Decimal
        Get
            Return _InterestAmount
        End Get
        Set(ByVal value As Decimal)
            _InterestAmount = value
        End Set
    End Property

    Private _BalanceAmount As Decimal
    Public Property BalanceAmount() As Decimal
        Get
            Return _BalanceAmount
        End Get
        Set(ByVal value As Decimal)
            _BalanceAmount = value
        End Set
    End Property

#Region "DebitCreditMemo"
    Private _DMCMNumber As Long
    Public Property DMCMNumber() As Long
        Get
            Return _DMCMNumber
        End Get
        Set(ByVal value As Long)
            _DMCMNumber = value
        End Set
    End Property
#End Region
End Class
