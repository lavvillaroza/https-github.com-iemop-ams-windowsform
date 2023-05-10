Public Class PaymentTag
    Private _PaymentTagNo As Long
    Public Property PaymentTagNo() As Long
        Get
            Return _PaymentTagNo
        End Get
        Set(ByVal value As Long)
            _PaymentTagNo = value
        End Set
    End Property

    Private _RemittanceDate As Date
    Public Property RemittanceDate() As Date
        Get
            Return _RemittanceDate
        End Get
        Set(ByVal value As Date)
            _RemittanceDate = value
        End Set
    End Property

    Private _BillingIDNumber As AMParticipants
    Public Property BillingIDNumber() As AMParticipants
        Get
            Return _BillingIDNumber
        End Get
        Set(ByVal value As AMParticipants)
            _BillingIDNumber = value
        End Set
    End Property

    Private _TotalAmountTagged As Decimal
    Public Property TotalAmountTagged() As Decimal
        Get
            Return _TotalAmountTagged
        End Get
        Set(ByVal value As Decimal)
            _TotalAmountTagged = value
        End Set
    End Property

    Private _TagDetails As List(Of PaymentTagDetails)
    Public Property TagDetails() As List(Of PaymentTagDetails)
        Get
            Return _TagDetails
        End Get
        Set(ByVal value As List(Of PaymentTagDetails))
            _TagDetails = value
        End Set
    End Property

    Private _AllocDetails As List(Of PaymentTagDetails)
    Public Property AllocDetails() As List(Of PaymentTagDetails)
        Get
            Return _AllocDetails
        End Get
        Set(ByVal value As List(Of PaymentTagDetails))
            _AllocDetails = value
        End Set
    End Property
End Class
