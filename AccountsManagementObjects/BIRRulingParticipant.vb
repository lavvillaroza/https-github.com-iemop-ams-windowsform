Public Class BIRRulingParticipant
    Private _CollReportNumber As Long
    Public Property CollReportNumber() As Long
        Get
            Return _CollReportNumber
        End Get
        Set(ByVal value As Long)
            _CollReportNumber = value
        End Set
    End Property

    Private _Participant As AMParticipants
    Public Property Participant() As AMParticipants
        Get
            Return _Participant
        End Get
        Set(ByVal value As AMParticipants)
            _Participant = value
        End Set
    End Property

    Private _BIRRDetails As List(Of BIRRulingDetails)
    Public Property BIRRDetails() As List(Of BIRRulingDetails)
        Get
            Return _BIRRDetails
        End Get
        Set(ByVal value As List(Of BIRRulingDetails))
            _BIRRDetails = value
        End Set
    End Property
End Class
