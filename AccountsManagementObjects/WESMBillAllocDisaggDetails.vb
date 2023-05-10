Public Class WESMBillAllocDisaggDetails
    Private _STLID As String
    Public Property STLID() As String
        Get
            Return _STLID
        End Get
        Set(ByVal value As String)
            _STLID = value
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

    Private _FacilityType As String
    Public Property FacilityType() As String
        Get
            Return _FacilityType
        End Get
        Set(ByVal value As String)
            _FacilityType = value
        End Set
    End Property

    Private _WHTTag As String
    Public Property WHTTag() As String
        Get
            Return _WHTTag
        End Get
        Set(ByVal value As String)
            _WHTTag = value
        End Set
    End Property

    Private _ITHTag As String
    Public Property ITHTag() As String
        Get
            Return _ITHTag
        End Get
        Set(ByVal value As String)
            _ITHTag = value
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

    Public ReadOnly Property GrossSale() As Decimal
        Get
            Return Me._VatableSales + Me._ZeroRatedSales + Me._ZeroRatedEcoZoneSales
        End Get
    End Property

    Public ReadOnly Property NetSale() As Decimal
        Get
            Return Me._VatableSales + Me._ZeroRatedSales + Me._ZeroRatedEcoZoneSales + If(Me._EWT < 0, Me._EWT, 0)
        End Get
    End Property

    Public ReadOnly Property GrossPurchase() As Decimal
        Get
            Return Me._VatablePurchases + Me._ZeroRatedPurchases + Me._ZeroRatedEcoZonePurchases
        End Get
    End Property

    Public ReadOnly Property NetPurchase() As Decimal
        Get
            Return Me._VatablePurchases + Me._ZeroRatedPurchases + Me._ZeroRatedEcoZonePurchases + If(Me._EWT > 0, Me._EWT, 0)
        End Get
    End Property

    Private _SummaryID As String
    Public Property SummaryId() As Long
        Get
            Return _SummaryID
        End Get
        Set(ByVal value As Long)
            _SummaryID = value
        End Set
    End Property

End Class
