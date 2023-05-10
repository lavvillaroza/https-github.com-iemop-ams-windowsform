Public Class WESMBillFileOutput
    Inherits SellerBuyerInformation

    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
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

    Private _VatOnSales As Decimal
    Public Property VatOnSales() As Decimal
        Get
            Return _VatOnSales
        End Get
        Set(ByVal value As Decimal)
            _VatOnSales = value
        End Set
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

    Private _VatOnPurchases As Decimal
    Public Property VatOnPurchases() As Decimal
        Get
            Return _VatOnPurchases
        End Get
        Set(ByVal value As Decimal)
            _VatOnPurchases = value
        End Set
    End Property

    Private _EWT As Decimal
    Public Property EWT() As Decimal
        Get
            Return _EWT
        End Get
        Set(ByVal value As Decimal)
            _EWT = value
        End Set
    End Property

    Private _NSS As Decimal
    Public Property NSS() As Decimal
        Get
            Return _NSS
        End Get
        Set(ByVal value As Decimal)
            _NSS = value
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

    Private _SpotQty As Decimal
    Public Property SpotQty() As Decimal
        Get
            Return _SpotQty
        End Get
        Set(ByVal value As Decimal)
            _SpotQty = value
        End Set
    End Property

    Private _SellerOrBuyer As String
    Public Property SellerOrBuyer() As String
        Get
            Return _SellerOrBuyer
        End Get
        Set(ByVal value As String)
            _SellerOrBuyer = value
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

    Private _GenXAmount As Decimal
    Public Property GenXAmount() As Decimal
        Get
            Return _GenXAmount
        End Get
        Set(ByVal value As Decimal)
            _GenXAmount = value
        End Set
    End Property

    Private _TotalSales As Decimal
    Public ReadOnly Property TotalSales() As Decimal
        Get
            Me._TotalSales = Me.VatableSales + Me.ZeroRatedSales + Me._ZeroRatedEcoZoneSales
            Return _TotalSales
        End Get
    End Property

    Private _TotalPurchases As Decimal
    Public ReadOnly Property NetTotalPurchases() As Decimal
        Get
            Me._TotalPurchases = Me.VatablePurchases + Me.ZeroRatedPurchases + Me.ZeroRatedEcoZonePurchases
            Return _TotalPurchases
        End Get
    End Property

End Class
