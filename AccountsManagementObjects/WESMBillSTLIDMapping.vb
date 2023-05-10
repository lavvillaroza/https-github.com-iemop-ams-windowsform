Public Class WESMBillSTLIDMapping
    Sub New()
        Me.New(0, "", "", "")
    End Sub
    Sub New(ByVal billingPeriodNo As Long, ByVal billingId As String, ByVal invoiceId As String, ByVal stlId As String)
        Me._BillingPeriodNo = billingPeriodNo
        Me._BillingID = billingId
        Me._InvoiceID = invoiceId
        Me._STLID = stlId
    End Sub

    Private _BillingPeriodNo As Long
    Public Property BillingPeriodNo() As Long
        Get
            Return _BillingPeriodNo
        End Get
        Set(ByVal value As Long)
            _BillingPeriodNo = value
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

    Private _InvoiceID As String
    Public Property InvoiceID() As String
        Get
            Return _InvoiceID
        End Get
        Set(ByVal value As String)
            _InvoiceID = value
        End Set
    End Property

    Private _STLID As String
    Public Property STLID() As String
        Get
            Return _STLID
        End Get
        Set(ByVal value As String)
            _STLID = value
        End Set
    End Property
End Class
