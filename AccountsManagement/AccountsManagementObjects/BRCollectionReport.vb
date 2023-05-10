Public Class BRCollectionReport
    Private _BIRRNo As Long
    Public Property BIRRNo() As Long
        Get
            Return _BIRRNo
        End Get
        Set(ByVal value As Long)
            _BIRRNo = value
        End Set
    End Property

    Private _RemittanceDateFrom As Date
    Public Property RemittanceDateFrom() As Date
        Get
            Return _RemittanceDateFrom
        End Get
        Set(ByVal value As Date)
            _RemittanceDateFrom = value
        End Set
    End Property

    Private _RemittanceDateTo As Date
    Public Property RemittanceDateTo() As Date
        Get
            Return _RemittanceDateTo
        End Get
        Set(ByVal value As Date)
            _RemittanceDateTo = value
        End Set
    End Property

    Private _GeneratedDate As Date
    Public Property GeneratedDate() As Date
        Get
            Return _GeneratedDate
        End Get
        Set(ByVal value As Date)
            _GeneratedDate = value
        End Set
    End Property

    Private _GeneratedBy As String
    Public Property GeneratedBy() As String
        Get
            Return _GeneratedBy
        End Get
        Set(ByVal value As String)
            _GeneratedBy = value
        End Set
    End Property

    Private _SellerDetails As List(Of BRCollectionReportSellerParticipant)
    Public Property SellerDetails() As List(Of BRCollectionReportSellerParticipant)
        Get
            Return _SellerDetails
        End Get
        Set(ByVal value As List(Of BRCollectionReportSellerParticipant))
            _SellerDetails = value
        End Set
    End Property

    Private _BuyerDetails As List(Of BRCollectionReportBuyerParticipant)
    Public Property BuyerDetails() As List(Of BRCollectionReportBuyerParticipant)
        Get
            Return _BuyerDetails
        End Get
        Set(ByVal value As List(Of BRCollectionReportBuyerParticipant))
            _BuyerDetails = value
        End Set
    End Property
End Class
