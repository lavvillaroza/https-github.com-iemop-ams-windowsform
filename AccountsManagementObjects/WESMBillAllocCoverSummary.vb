Public Class WESMBillAllocCoverSummary
    Private _SummaryID As String
    Public Property SummaryId() As Long
        Get
            Return _SummaryID
        End Get
        Set(ByVal value As Long)
            _SummaryID = value
        End Set
    End Property

    Private _STLRun As String
    Public Property STLRun() As String
        Get
            Return _STLRun
        End Get
        Set(ByVal value As String)
            _STLRun = value
        End Set
    End Property

    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
        End Set
    End Property

    Private _StlID As String
    Public Property StlID() As String
        Get
            Return _StlID
        End Get
        Set(ByVal value As String)
            _StlID = value
        End Set
    End Property

    Private _BillingID As String
    Public Property BillingID() As String
        Get
            Return _BillingID
        End Get
        Set(ByVal value As String)
            _BillingID = value
        End Set
    End Property

    Private _WHT As String
    Public Property WHT() As String
        Get
            Return _WHT
        End Get
        Set(ByVal value As String)
            _WHT = value
        End Set
    End Property

    Private _ITH As String
    Public Property ITH() As String
        Get
            Return _ITH
        End Get
        Set(ByVal value As String)
            _ITH = value
        End Set
    End Property

    Private _NonVatableTag As String
    Public Property NonVatableTag() As String
        Get
            Return _NonVatableTag
        End Get
        Set(ByVal value As String)
            _NonVatableTag = value
        End Set
    End Property

    Private _ZeroRatedTag As String
    Public Property ZeroRatedTag() As String
        Get
            Return _ZeroRatedTag
        End Get
        Set(ByVal value As String)
            _ZeroRatedTag = value
        End Set
    End Property

    Private _NetSellerBuyerTag As String
    Public Property NetSellerBuyerTag() As String
        Get
            Return _NetSellerBuyerTag
        End Get
        Set(ByVal value As String)
            _NetSellerBuyerTag = value
        End Set
    End Property

    Private _TransactionNo As String
    Public Property TransactionNo() As String
        Get
            Return _TransactionNo
        End Get
        Set(ByVal value As String)
            _TransactionNo = value
        End Set
    End Property

    Private _TransactionDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            _TransactionDate = value
        End Set
    End Property

    Private _VatableSales As Decimal
    Public Property VatableSales() As Decimal
        Get
            Return _VatableSales
        End Get
        Set(ByVal value As Decimal)
            _VatableSales = value
        End Set
    End Property

    Private _ZeroRatedSales As Decimal
    Public Property ZeroRatedSales() As Decimal
        Get
            Return _ZeroRatedSales
        End Get
        Set(ByVal value As Decimal)
            _ZeroRatedSales = value
        End Set
    End Property

    Private _ZeroRatedEcoZoneSales As Decimal
    Public Property ZeroRatedEcoZoneSales() As Decimal
        Get
            Return _ZeroRatedEcoZoneSales
        End Get
        Set(ByVal value As Decimal)
            _ZeroRatedEcoZoneSales = value
        End Set
    End Property

    Public ReadOnly Property GrossSale() As Decimal
        Get
            Return Me._VatableSales + Me._ZeroRatedSales + Me._ZeroRatedEcoZoneSales
        End Get
    End Property

    Public ReadOnly Property NetSale() As Decimal
        Get
            Return Me._VatableSales + Me._ZeroRatedSales + Me._ZeroRatedEcoZoneSales + Me._EWTSales
        End Get
    End Property

    Private _VatablePurchases As Decimal
    Public Property VatablePurchases() As Decimal
        Get
            Return _VatablePurchases
        End Get
        Set(ByVal value As Decimal)
            _VatablePurchases = value
        End Set
    End Property

    Private _ZeroRatedPurchases As Decimal
    Public Property ZeroRatedPurchases() As Decimal
        Get
            Return _ZeroRatedPurchases
        End Get
        Set(ByVal value As Decimal)
            _ZeroRatedPurchases = value
        End Set
    End Property

    Private _ZeroRatedEcoZonePurchases As Decimal
    Public Property ZeroRatedEcoZonePurchases() As Decimal
        Get
            Return _ZeroRatedEcoZonePurchases
        End Get
        Set(ByVal value As Decimal)
            _ZeroRatedEcoZonePurchases = value
        End Set
    End Property

    Public ReadOnly Property GrossPurchase() As Decimal
        Get
            Return Me._VatablePurchases + Me._ZeroRatedPurchases + Me._ZeroRatedEcoZonePurchases
        End Get
    End Property

    Public ReadOnly Property NetPurchase() As Decimal
        Get
            Return Me._VatablePurchases + Me._ZeroRatedPurchases + Me._ZeroRatedEcoZonePurchases + Me._EWTPurchases
        End Get
    End Property

    Private _NSSFlowBack As Decimal
    Public Property NSSFlowBack() As Decimal
        Get
            Return _NSSFlowBack
        End Get
        Set(ByVal value As Decimal)
            _NSSFlowBack = value
        End Set
    End Property

    Private _VatOnSales As Decimal
    Public Property VatOnSales() As Decimal
        Get
            Return _VatOnSales
        End Get
        Set(ByVal value As Decimal)
            _VatOnSales = value
        End Set
    End Property

    Private _VatOnPurchases As Decimal
    Public Property VatOnPurchases() As Decimal
        Get
            Return _VatOnPurchases
        End Get
        Set(ByVal value As Decimal)
            _VatOnPurchases = value
        End Set
    End Property

    Private _EWTSales As Decimal
    Public Property EWTSales() As Decimal
        Get
            Return _EWTSales
        End Get
        Set(ByVal value As Decimal)
            _EWTSales = value
        End Set
    End Property

    Private _EWTPurchases As Decimal
    Public Property EWTPurchases() As Decimal
        Get
            Return _EWTPurchases
        End Get
        Set(ByVal value As Decimal)
            _EWTPurchases = value
        End Set
    End Property

    Private _GMR As Decimal
    Public Property GMR() As Decimal
        Get
            Return _GMR
        End Get
        Set(ByVal value As Decimal)
            _GMR = value
        End Set
    End Property

    Private _MarketFeesRate As Decimal
    Public Property MarketFeesRate() As Decimal
        Get
            Return _MarketFeesRate
        End Get
        Set(ByVal value As Decimal)
            _MarketFeesRate = value
        End Set
    End Property

    Private _SpotQty As Decimal
    Public Property SpotQty() As Decimal
        Get
            Return _SpotQty
        End Get
        Set(ByVal value As Decimal)
            _SpotQty = value
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

    Private _Remakrs As String
    Public Property Remarks() As String
        Get
            Return _Remakrs
        End Get
        Set(ByVal value As String)
            _Remakrs = value
        End Set
    End Property

    Private _GenXAmount As Decimal
    Public Property GenXAmount() As Decimal
        Get
            Return _GenXAmount
        End Get
        Set(ByVal value As Decimal)
            _GenXAmount = value
        End Set
    End Property

    Private _UploadFrom As String
    Public Property UploadFrom() As String
        Get
            Return _UploadFrom
        End Get
        Set(ByVal value As String)
            _UploadFrom = value
        End Set
    End Property

    Private _ListWBAllocDisDetails As List(Of WESMBillAllocDisaggDetails)
    Public Property ListWBAllocDisDetails() As List(Of WESMBillAllocDisaggDetails)
        Get
            Return _ListWBAllocDisDetails
        End Get
        Set(ByVal value As List(Of WESMBillAllocDisaggDetails))
            _ListWBAllocDisDetails = value
        End Set
    End Property

End Class
