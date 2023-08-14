Public Class BRCollectionReportSellerParticipant
    Private _BRCollReportNumber As Long
    Public Property BRCollReportNumber() As Long
        Get
            Return _BRCollReportNumber
        End Get
        Set(ByVal value As Long)
            _BRCollReportNumber = value
        End Set
    End Property

    Private _SellerParticipantInfo As AMParticipants
    Public Property SellerParticipantInfo() As AMParticipants
        Get
            Return _SellerParticipantInfo
        End Get
        Set(ByVal value As AMParticipants)
            _SellerParticipantInfo = value
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

    Private _BRCollReportDetails As List(Of BRCollectionReportSalesDetails)
    Public Property BRCollReportDetails() As List(Of BRCollectionReportSalesDetails)
        Get
            Return _BRCollReportDetails
        End Get
        Set(ByVal value As List(Of BRCollectionReportSalesDetails))
            _BRCollReportDetails = value
        End Set
    End Property
End Class
