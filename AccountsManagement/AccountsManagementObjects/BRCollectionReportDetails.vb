Public Class BRCollectionReportDetails
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

    Private _BuyerParticipant As AMParticipants
    Public Property BuyerParticipant() As AMParticipants
        Get
            Return _BuyerParticipant
        End Get
        Set(ByVal value As AMParticipants)
            _BuyerParticipant = value
        End Set
    End Property

    Private _Renewable As String
    Public Property Renewable() As String
        Get
            Return _Renewable
        End Get
        Set(ByVal value As String)
            _Renewable = value
        End Set
    End Property

    Private _ZeroRated As String
    Public Property ZeroRated() As String
        Get
            Return _ZeroRated
        End Get
        Set(ByVal value As String)
            _ZeroRated = value
        End Set
    End Property

    Private _IncomeTaxHoliday As String
    Public Property IncomeTaxHoliday() As String
        Get
            Return _IncomeTaxHoliday
        End Get
        Set(ByVal value As String)
            _IncomeTaxHoliday = value
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

    Private _BuyerInvoiceNo As String
    Public Property BuyerInvoiceNo() As String
        Get
            Return _BuyerInvoiceNo
        End Get
        Set(ByVal value As String)
            _BuyerInvoiceNo = value
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

    Private _WithholdingTax As Decimal
    Public Property WithholdingTax() As Decimal
        Get
            Return _WithholdingTax
        End Get
        Set(ByVal value As Decimal)
            _WithholdingTax = value
        End Set
    End Property

    Public ReadOnly Property NetSale() As Decimal
        Get
            Return (_VatableSales + _ZeroRatedSales + _ZeroRatedEcoZoneSales)
        End Get
    End Property

    Public ReadOnly Property Total() As Decimal
        Get
            Return CDec(_VatableSales + _ZeroRatedSales + _ZeroRatedEcoZoneSales + _VatOnSales + _WithholdingTax)
        End Get
    End Property
End Class
