Public Class EWTTaggedAllocated
    Private _CertificateNo As Long
    Public Property CertificateNo() As Long
        Get
            Return _CertificateNo
        End Get
        Set(ByVal value As Long)
            _CertificateNo = value
        End Set
    End Property

    Private _BillingIDNumber As String
    Public Property BillingIDNumber() As String
        Get
            Return _BillingIDNumber
        End Get
        Set(ByVal value As String)
            _BillingIDNumber = value
        End Set
    End Property

    Private _WESMTransNumber As String
    Public Property WESMTransNumber() As String
        Get
            Return _WESMTransNumber
        End Get
        Set(ByVal value As String)
            _WESMTransNumber = value
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

    Private _WithholdingTaxAmount As Decimal
    Public Property WithholdingTaxAmount() As Decimal
        Get
            Return _WithholdingTaxAmount
        End Get
        Set(ByVal value As Decimal)
            _WithholdingTaxAmount = value
        End Set
    End Property

    Private _EWTEndingBalance As Decimal
    Public Property EWTEndingBalance() As Decimal
        Get
            Return _EWTEndingBalance
        End Get
        Set(ByVal value As Decimal)
            _EWTEndingBalance = value
        End Set
    End Property

    Private _AmountTaggedAlloc As Decimal
    Public Property AmountTaggedAlloc() As Decimal
        Get
            Return _AmountTaggedAlloc
        End Get
        Set(ByVal value As Decimal)
            _AmountTaggedAlloc = value
        End Set
    End Property

    Private _BalanceType As String
    Public Property BalanceType() As String
        Get
            Return _BalanceType
        End Get
        Set(ByVal value As String)
            _BalanceType = value
        End Set
    End Property

    Private _AllocatedToAP As String
    Public Property AllocatedToAP() As String
        Get
            Return _AllocatedToAP
        End Get
        Set(ByVal value As String)
            _AllocatedToAP = value
        End Set
    End Property
End Class
