Public Class BRCollectionReportPurchasesDetails
    Private _CRBatchNo As Integer
    Public Property CRBatchNo() As Integer
        Get
            Return _CRBatchNo
        End Get
        Set(ByVal value As Integer)
            _CRBatchNo = value
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

    Private _STLRun As String
    Public Property STLRun() As String
        Get
            Return _STLRun
        End Get
        Set(ByVal value As String)
            _STLRun = value
        End Set
    End Property

    Private _Particulars As String
    Public Property Particulars As String
        Get
            Return _Particulars
        End Get
        Set(ByVal value As String)
            _Particulars = value
        End Set
    End Property

    Private _CollectionDate As Date
    Public Property CollectionDate() As Date
        Get
            Return _CollectionDate
        End Get
        Set(ByVal value As Date)
            _CollectionDate = value
        End Set
    End Property

    Private _SellerParticipant As AMParticipants
    Public Property SellerParticipant() As AMParticipants
        Get
            Return _SellerParticipant
        End Get
        Set(ByVal value As AMParticipants)
            _SellerParticipant = value
        End Set
    End Property

    Private _SellerBillingID As String
    Public Property SellerBillingID() As String
        Get
            Return _SellerBillingID
        End Get
        Set(ByVal value As String)
            _SellerBillingID = value
        End Set
    End Property

    Private _InvoiceNo As String
    Public Property InvoiceNo() As String
        Get
            Return _InvoiceNo
        End Get
        Set(ByVal value As String)
            _InvoiceNo = value
        End Set
    End Property

    Private _SellerInvoiceNo As String
    Public Property SellerInvoiceNo() As String
        Get
            Return _SellerInvoiceNo
        End Get
        Set(ByVal value As String)
            _SellerInvoiceNo = value
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

    Private _WithholdingTax As Decimal
    Public Property WithholdingTax() As Decimal
        Get
            Return _WithholdingTax
        End Get
        Set(ByVal value As Decimal)
            _WithholdingTax = value
        End Set
    End Property

    Private _WithholdingVat As Decimal
    Public Property WithholdingVat() As Decimal
        Get
            Return _WithholdingVat
        End Get
        Set(ByVal value As Decimal)
            _WithholdingVat = value
        End Set
    End Property

    Public ReadOnly Property NetPurchase() As Decimal
        Get
            Return (_VatablePurchases + _ZeroRatedPurchases + _ZeroRatedEcoZonePurchases)
        End Get
    End Property

    Public ReadOnly Property Total() As Decimal
        Get
            Return CDec(_VatablePurchases + _ZeroRatedPurchases + _ZeroRatedEcoZonePurchases + _VatOnPurchases + _WithholdingTax + _WithholdingVat)
        End Get
    End Property
End Class
