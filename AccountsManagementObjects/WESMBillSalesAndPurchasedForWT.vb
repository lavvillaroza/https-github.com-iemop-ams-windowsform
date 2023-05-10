Option Strict On
Option Explicit On
Public Class WESMBillSalesAndPurchasedForWT
    Inherits WESMBillSalesAndPurchased
#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(0, 0, "", New AMParticipants, "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
    End Sub

    Public Sub New(ByVal billingbatchno As Integer, ByVal billingperiod As Integer, ByVal settlementrun As String, _
                   ByVal idnumber As AMParticipants, ByVal registrationid As String, ByVal invoiceno As String, ByVal sales As Decimal, _
                   ByVal zeroratedsales As Decimal, ByVal purchases As Decimal, ByVal zeroratedpurchases As Decimal, ByVal netsettlementamount As Decimal,
                   ByVal vatonsales As Decimal, ByVal vatonpurchases As Decimal, ByVal withholdingtax As Decimal, ByVal gmr As Decimal, ByVal nssra As Decimal)
        Me.BillingBatchNo = billingbatchno
        Me.BillingPeriod = billingperiod
        Me.SettlementRun = settlementrun
        Me.IDNumber = idnumber
        Me.RegistrationID = registrationid
        Me.InvoiceNumber = invoiceno
        Me.VatableSales = sales
        Me.ZeroRatedSales = zeroratedsales
        Me.VatablePurchases = purchases
        Me.ZeroRatedPurchases = zeroratedpurchases
        Me.NetSettlementAmount = netsettlementamount
        Me.VATonSales = vatonsales
        Me.VATonPurchases = vatonpurchases
        Me.WithholdingTAX = withholdingtax
        Me.GMR = gmr
        Me.NSSRA = nssra
    End Sub
#End Region
#Region "BillingPeriod"
    Private _BillingBatchNo As Integer
    Public Property BillingBatchNo() As Integer
        Get
            Return _BillingBatchNo
        End Get
        Set(ByVal value As Integer)
            _BillingBatchNo = value
        End Set
    End Property
#End Region

End Class
