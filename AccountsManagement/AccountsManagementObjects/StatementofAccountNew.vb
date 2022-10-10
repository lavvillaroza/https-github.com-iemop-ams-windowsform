Public Class StatementofAccountNew
    Private _SOANumber As Long
    Public Property SOANumber() As Long
        Get
            Return _SOANumber
        End Get
        Set(ByVal value As Long)
            _SOANumber = value
        End Set
    End Property

    Private _SOADate As Date
    Public Property SOADate() As Date
        Get
            Return _SOADate
        End Get
        Set(ByVal value As Date)
            _SOADate = value
        End Set
    End Property

    Private _DueDate As Date
    Public Property DueDate() As Date
        Get
            Return _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
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

    Private _PreviousBalanceDueOnEnergy As Decimal
    Public Property PreviousBalanceDueOnEnergy() As Decimal
        Get
            Return _PreviousBalanceDueOnEnergy
        End Get
        Set(ByVal value As Decimal)
            _PreviousBalanceDueOnEnergy = value
        End Set
    End Property

    Private _PreviousBalanceDueOnVAT As Decimal
    Public Property PreviousBalanceDueOnVAT() As Decimal
        Get
            Return _PreviousBalanceDueOnVAT
        End Get
        Set(ByVal value As Decimal)
            _PreviousBalanceDueOnVAT = value
        End Set
    End Property

    Private _PreviousBalanceDueOnMF As Decimal
    Public Property PreviousBalanceDueOnMF() As Decimal
        Get
            Return _PreviousBalanceDueOnMF
        End Get
        Set(ByVal value As Decimal)
            _PreviousBalanceDueOnMF = value
        End Set
    End Property

    Private _PaymentReceivedOnEnergy As Decimal
    Public Property PaymentReceivedOnEnergy() As Decimal
        Get
            Return _PaymentReceivedOnEnergy
        End Get
        Set(ByVal value As Decimal)
            _PaymentReceivedOnEnergy = value
        End Set
    End Property

    Private _PaymentReceivedOnVAT As Decimal
    Public Property PaymentReceivedOnVAT() As Decimal
        Get
            Return _PaymentReceivedOnVAT
        End Get
        Set(ByVal value As Decimal)
            _PaymentReceivedOnVAT = value
        End Set
    End Property

    Private _PaymentReceivedOnMF As Decimal
    Public Property PaymentReceivedOnMF() As Decimal
        Get
            Return _PaymentReceivedOnMF
        End Get
        Set(ByVal value As Decimal)
            _PaymentReceivedOnMF = value
        End Set
    End Property

    Private _PaymentReceivedOnDefaultInterest As Decimal
    Public Property PaymentReceivedOnDefaultInterest() As Decimal
        Get
            Return _PaymentReceivedOnDefaultInterest
        End Get
        Set(ByVal value As Decimal)
            _PaymentReceivedOnDefaultInterest = value
        End Set
    End Property

    Private _SettledThruOffsettingOnEnergy As Decimal
    Public Property SettledThruOffsettingOnEnergy() As Decimal
        Get
            Return _SettledThruOffsettingOnEnergy
        End Get
        Set(ByVal value As Decimal)
            _SettledThruOffsettingOnEnergy = value
        End Set
    End Property

    Private _SettledThruOffsettingOnVAT As Decimal
    Public Property SettledThruOffsettingOnVAT() As Decimal
        Get
            Return _SettledThruOffsettingOnVAT
        End Get
        Set(ByVal value As Decimal)
            _SettledThruOffsettingOnVAT = value
        End Set
    End Property

    Private _SettledThruOffsettingOnMF As Decimal
    Public Property SettledThruOffsettingOnMF() As Decimal
        Get
            Return _SettledThruOffsettingOnMF
        End Get
        Set(ByVal value As Decimal)
            _SettledThruOffsettingOnMF = value
        End Set
    End Property

    Private _SettledThruOffsettingOnDefaultInterest As Decimal
    Public Property SettledThruOffsettingOnDefaultInterest() As Decimal
        Get
            Return _SettledThruOffsettingOnDefaultInterest
        End Get
        Set(ByVal value As Decimal)
            _SettledThruOffsettingOnDefaultInterest = value
        End Set
    End Property

    Private _WESMBillSummaryList As List(Of WESMBillSummary)
    Public Property WESMBillSummaryList() As List(Of WESMBillSummary)
        Get
            Return _WESMBillSummaryList
        End Get
        Set(ByVal value As List(Of WESMBillSummary))
            _WESMBillSummaryList = value
        End Set
    End Property

End Class
