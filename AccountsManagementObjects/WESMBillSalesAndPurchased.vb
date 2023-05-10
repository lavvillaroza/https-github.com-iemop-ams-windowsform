'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             WESMBillSalesAndPurchased
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     October 14, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for WESM Bill Sales and Purchased
'Arguments/Parameters:  
'Files/Database Tables:  AM_WESM_BILL_SALES_PURCHASED
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   October 14, 2012        Vladimir E. Espiritu            Class initialization
'   June 27, 2013           Vladimir E. Espiritu            Added ReferenceNo property 
'   July 19, 2016           Lance Arjay Villaroza           Changed overall properties

Option Strict On
Option Explicit On

Public Class WESMBillSalesAndPurchased

#Region "Initialization/Constructor"
    Public Sub New()
        Me.New(0, "", New AMParticipants, "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
    End Sub

    Public Sub New(ByVal billingperiod As Integer, ByVal settlementrun As String, _
                   ByVal idnumber As AMParticipants, ByVal registrationid As String, ByVal invoiceno As String, ByVal sales As Decimal, _
                   ByVal zeroratedsales As Decimal, ByVal purchases As Decimal, ByVal zeroratedpurchases As Decimal, ByVal netsettlementamount As Decimal,
                   ByVal vatonsales As Decimal, ByVal vatonpurchases As Decimal, ByVal withholdingtax As Decimal, ByVal gmr As Decimal, ByVal nssra As Decimal)
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
    Private _BillingPeriod As Integer
    Public Property BillingPeriod() As Integer
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As Integer)
            _BillingPeriod = value
        End Set
    End Property
#End Region

#Region "SettlementRun"
    Private _SettlementRun As String
    Public Property SettlementRun() As String
        Get
            Return _SettlementRun
        End Get
        Set(ByVal value As String)
            _SettlementRun = value
        End Set
    End Property
#End Region

#Region "IDNumber"
    Private _IDNumber As AMParticipants
    Public Property IDNumber() As AMParticipants
        Get
            Return _IDNumber
        End Get
        Set(ByVal value As AMParticipants)
            _IDNumber = value
        End Set
    End Property
#End Region

#Region "RegistrationID"
    Private _RegistrationID As String
    Public Property RegistrationID() As String
        Get
            Return _RegistrationID
        End Get
        Set(ByVal value As String)
            _RegistrationID = value
        End Set
    End Property
#End Region

#Region "InvoiceNumber"
    Private _InvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return _InvoiceNumber
        End Get
        Set(ByVal value As String)
            _InvoiceNumber = value
        End Set
    End Property

#End Region

#Region "Vatable Sales"
    Private _VatableSales As Decimal
    Public Property VatableSales() As Decimal
        Get
            Return _VatableSales
        End Get
        Set(ByVal value As Decimal)
            _VatableSales = value
        End Set
    End Property
#End Region

#Region "ZeroRatedSales"
    Private _ZeroRatedSales As Decimal
    Public Property ZeroRatedSales() As Decimal
        Get
            Return _ZeroRatedSales
        End Get
        Set(ByVal value As Decimal)
            _ZeroRatedSales = value
        End Set
    End Property
#End Region

#Region "Vatable Purchases"
    Private _VatablePurchases As Decimal
    Public Property VatablePurchases() As Decimal
        Get
            Return _VatablePurchases
        End Get
        Set(ByVal value As Decimal)
            _VatablePurchases = value
        End Set
    End Property
#End Region

#Region "ZeroRatedPurchases"
    Private _ZeroRatedPurchases As Decimal
    Public Property ZeroRatedPurchases() As Decimal
        Get
            Return _ZeroRatedPurchases
        End Get
        Set(ByVal value As Decimal)
            _ZeroRatedPurchases = value
        End Set
    End Property
#End Region

#Region "NetSettlementAmount"
    Private _NetSettlementAmount As Decimal
    Public Property NetSettlementAmount() As Decimal
        Get
            Return _NetSettlementAmount
        End Get
        Set(ByVal value As Decimal)
            _NetSettlementAmount = value
        End Set
    End Property
#End Region

#Region "VATonSales"
    Private _VATonSales As Decimal
    Public Property VATonSales() As Decimal
        Get
            Return _VATonSales
        End Get
        Set(ByVal value As Decimal)
            _VATonSales = value
        End Set
    End Property

#End Region

#Region "VATonPurchases"
    Private _VATonPurchases As Decimal
    Public Property VATonPurchases() As Decimal
        Get
            Return _VATonPurchases
        End Get
        Set(ByVal value As Decimal)
            _VATonPurchases = value
        End Set
    End Property

#End Region

#Region "Withholding TAX"
    Private _WithholdingTAX As Decimal
    Public Property WithholdingTAX() As Decimal
        Get
            Return _WithholdingTAX
        End Get
        Set(ByVal value As Decimal)
            _WithholdingTAX = value
        End Set
    End Property
#End Region

#Region "ZeroRated Econzone"
    Private _ZeroRatedEcozone As Decimal
    Public Property ZeroRatedEcozone() As Decimal
        Get
            Return _ZeroRatedEcozone
        End Get
        Set(ByVal value As Decimal)
            _ZeroRatedEcozone = value
        End Set
    End Property
#End Region

#Region "GMR"
    Private _GMR As Decimal
    Public Property GMR() As Decimal
        Get
            Return _GMR
        End Get
        Set(ByVal value As Decimal)
            _GMR = value
        End Set
    End Property

#End Region

#Region "NSSRA"
    Private _NSSRA As Decimal
    Public Property NSSRA() As Decimal
        Get
            Return _NSSRA
        End Get
        Set(ByVal value As Decimal)
            _NSSRA = value
        End Set
    End Property

#End Region

#Region "TransactionType"
    Private _TransactionType As EnumWESMBillSalesAndPurchasedTransType
    Public Property TransactionType() As EnumWESMBillSalesAndPurchasedTransType
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As EnumWESMBillSalesAndPurchasedTransType)
            _TransactionType = value
        End Set
    End Property

#End Region

#Region "Total TTA of Sales and Purchases with VAT EcoZone"
    Private _TotalVATSalesAndPurchases As Decimal
    Public ReadOnly Property TotalVATSalesAndPurchases() As Decimal
        Get
            '_TotalVATSalesAndPurchases = Math.Abs(Me.VatablePurchases) - Math.Abs(Me.VatableSales)
            'If _TotalVATSalesAndPurchases < 0 Then
            '    Return 0
            'Else
            '    Return _TotalVATSalesAndPurchases
            'End If

            _TotalVATSalesAndPurchases = Me.VatablePurchases + Me.VatableSales

            If _TotalVATSalesAndPurchases > 0 Then
                Return 0
            Else
                Return _TotalVATSalesAndPurchases
            End If
        End Get
    End Property
#End Region

#Region "Total TTA of Sales and Purchases with VAT EcoZone"
    Private _TotalZeroRatedSalesAndPurchases As Decimal
    Public ReadOnly Property TotalZeroRatedSalesAndPurchases() As Decimal
        Get
            '_TotalZeroRatedSalesAndPurchases = Math.Abs(Me.ZeroRatedPurchases) - Math.Abs(Me.ZeroRatedSales) - Math.Abs(Me.ZeroRatedEcozone)
            'If _TotalZeroRatedSalesAndPurchases < 0 Then
            '    Return 0
            'Else
            '    Return _TotalZeroRatedSalesAndPurchases
            'End If

            _TotalZeroRatedSalesAndPurchases = Me.ZeroRatedPurchases + Me.ZeroRatedSales + Me.ZeroRatedEcozone
            If _TotalZeroRatedSalesAndPurchases > 0 Then
                Return 0
            Else
                Return _TotalZeroRatedSalesAndPurchases
            End If
        End Get
    End Property
#End Region

#Region "VatableRatio"
    Private _VatableRatio As Decimal
    Public ReadOnly Property VatableRatio() As Decimal
        Get
            'Dim TotalTTA As Decimal = Me.TotalVATSalesAndPurchases + Me.TotalZeroRatedSalesAndPurchases
            'If TotalTTA = 0 Then
            '    _VatableRatio = 0
            'Else                
            '    _VatableRatio = Me.TotalVATSalesAndPurchases / TotalTTA
            'End If
            'Return _VatableRatio

            If Me.NetSettlementAmount > 0 Or Me.TotalVATSalesAndPurchases = 0 Then
                _VatableRatio = 0
            Else
                _VatableRatio = Math.Abs(Me.TotalVATSalesAndPurchases) / Math.Abs(Me.NetSettlementAmount)
            End If

            Return _VatableRatio

        End Get
    End Property

#End Region

#Region "ZeroRatedRatio"
    Private _ZeroRatedRatio As Decimal
    Public ReadOnly Property ZeroRatedRatio() As Decimal
        Get
            'Dim TotalTTA As Decimal = Me.TotalVATSalesAndPurchases + Me.TotalZeroRatedSalesAndPurchases
            'If TotalTTA = 0 Then
            '    _ZeroRatedRatio = 0
            'Else              
            '    _ZeroRatedRatio = Me.TotalZeroRatedSalesAndPurchases / TotalTTA
            'End If
            'Return _ZeroRatedRatio

            If Me.NetSettlementAmount > 0 Or Me.TotalZeroRatedSalesAndPurchases = 0 Then
                _ZeroRatedRatio = 0
            Else
                _ZeroRatedRatio = Math.Abs(Me.TotalZeroRatedSalesAndPurchases) / Math.Abs(Me.NetSettlementAmount)
            End If

            Return _ZeroRatedRatio

        End Get
    End Property
#End Region

End Class
