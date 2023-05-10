Public Class BIRRuling

    Private _TransactionDateFrom As Date
    Public Property TransactionDateFrom() As Date
        Get
            Return _TransactionDateFrom
        End Get
        Set(ByVal value As Date)
            _TransactionDateFrom = value
        End Set
    End Property

    Private _TransactionDateTo As Date
    Public Property TransactionDateTo() As Date
        Get
            Return _TransactionDateTo
        End Get
        Set(ByVal value As Date)
            _TransactionDateTo = value
        End Set
    End Property


    Private _SellerParticipantInfo As List(Of BIRRulingParticipant)
    Public Property SellerDetails() As List(Of BIRRulingParticipant)
        Get
            Return _SellerParticipantInfo
        End Get
        Set(ByVal value As List(Of BIRRulingParticipant))
            _SellerParticipantInfo = value
        End Set
    End Property
End Class
