Public Class PaymentTagDetails
    Private _WESMBillSummary As WESMBillSummary
    Public Property WESMBillSummary() As WESMBillSummary
        Get
            Return _WESMBillSummary
        End Get
        Set(ByVal value As WESMBillSummary)
            _WESMBillSummary = value
        End Set
    End Property

    Private _NewDueDate As Date
    Public Property NewDueDate() As Date
        Get
            Return _NewDueDate
        End Get
        Set(ByVal value As Date)
            _NewDueDate = value
        End Set
    End Property

    Private _EndingBalance As Decimal
    Public Property EndingBalance() As Decimal
        Get
            Return _EndingBalance
        End Get
        Set(ByVal value As Decimal)
            _EndingBalance = value
        End Set
    End Property

    Private _AmountTagAlloc As Decimal
    Public Property AmountTagAlloc() As Decimal
        Get
            Return _AmountTagAlloc
        End Get
        Set(ByVal value As Decimal)
            _AmountTagAlloc = value
        End Set
    End Property

    Private _NewEndingBalance As Decimal
    Public Property NewEndingBalance() As Decimal
        Get
            Return _NewEndingBalance
        End Get
        Set(ByVal value As Decimal)
            _NewEndingBalance = value
        End Set
    End Property
End Class
