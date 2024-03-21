Public Class ImportRTAFromCRSS
    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
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

    Private _GroupID As Long
    Public Property GroupID() As Long
        Get
            Return _GroupID
        End Get
        Set(ByVal value As Long)
            _GroupID = value
        End Set
    End Property

    Private _TotalNetSales As Decimal
    Public Property TotalNetSales() As Decimal
        Get
            Return _TotalNetSales
        End Get
        Set(ByVal value As Decimal)
            _TotalNetSales = value
        End Set
    End Property

    Private _TotalNetPurchases As Decimal
    Public Property TotalNetPurchases() As Decimal
        Get
            Return _TotalNetPurchases
        End Get
        Set(ByVal value As Decimal)
            _TotalNetPurchases = value
        End Set
    End Property

    Private _TotalEWTSales As Decimal
    Public Property TotalEWTSales() As Decimal
        Get
            Return _TotalEWTSales
        End Get
        Set(ByVal value As Decimal)
            _TotalEWTSales = value
        End Set
    End Property

    Private _TotalEWTPurchases As Decimal
    Public Property TotalEWTPurchases() As Decimal
        Get
            Return _TotalEWTPurchases
        End Get
        Set(ByVal value As Decimal)
            _TotalEWTPurchases = value
        End Set
    End Property

    Private _ListOfError As List(Of String)
    Public Property ListOfError As List(Of String)
        Get
            Return _ListOfError
        End Get
        Set(ByVal value As List(Of String))
            _ListOfError = value
        End Set
    End Property

    Private _Remarks As String
    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
        End Set
    End Property

    Private _RTACoverSummaryList As List(Of WESMBillAllocCoverSummary)
    Public Property RTACoverSummaryList() As List(Of WESMBillAllocCoverSummary)
        Get
            Return _RTACoverSummaryList
        End Get
        Set(ByVal value As List(Of WESMBillAllocCoverSummary))
            _RTACoverSummaryList = value
        End Set
    End Property

    Private _WESMBillList As List(Of WESMBill)
    Public Property WESMBillList() As List(Of WESMBill)
        Get
            Return _WESMBillList
        End Get
        Set(ByVal value As List(Of WESMBill))
            _WESMBillList = value
        End Set
    End Property

End Class
