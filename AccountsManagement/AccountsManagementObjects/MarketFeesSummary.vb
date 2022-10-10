'***************************************************************************
'Copyright 2012: Philippine Electricity Market Corporation(PEMC)
'Class Name:             MarketFeesSummary
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     November 09, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Market Fees Summary Report
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   November 09, 2012       Vladimir E. Espiritu            Class initialization
'

Option Explicit On
Option Strict On

Public Class MarketFeesSummary

#Region "Initialization/Constructor"
    Public Sub New()
        Me._IDNumber = New AMParticipants()
        Me._INVDMCMNo = Nothing
        Me._SummaryType = Nothing
        Me._MarketFees = 0
        Me._VATOnMarketFees = 0
        Me._WithholdingVATOnMarketFees = 0
        Me._WithholdingTAXOnMarketFees = 0
        Me._Totals = 0
        Me._DefaultInterestOnMarketFees = 0
        Me._DefaultInterestOnVATOnMarketFees = 0
        Me._DefaultInterestOnWithholdingVAT = 0
        Me._DefaultInterestOnWithholdingTAX = 0
        Me._TotalDefaultInterest = 0
        Me._GrandTotal = 0
        Me._CollectionPaymentTransactionType = Nothing
    End Sub
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

#Region "INVDMCMNo"
    Private _INVDMCMNo As String
    Public Property INVDMCMNo() As String
        Get
            Return _INVDMCMNo
        End Get
        Set(ByVal value As String)
            _INVDMCMNo = value
        End Set
    End Property

#End Region

#Region "Summary Type"
    Private _SummaryType As EnumSummaryType
    Public Property SummaryType() As EnumSummaryType
        Get
            Return _SummaryType
        End Get
        Set(ByVal value As EnumSummaryType)
            _SummaryType = value
        End Set
    End Property

#End Region

#Region "MarketFees"
    Private _MarketFees As Decimal
    Public Property MarketFees() As Decimal
        Get
            Return _MarketFees
        End Get
        Set(ByVal value As Decimal)
            _MarketFees = value
        End Set
    End Property

#End Region

#Region "VATOnMarketFees"
    Private _VATOnMarketFees As Decimal
    Public Property VATOnMarketFees() As Decimal
        Get
            Return _VATOnMarketFees
        End Get
        Set(ByVal value As Decimal)
            _VATOnMarketFees = value
        End Set
    End Property

#End Region

#Region "WithholdingVATOnMarketFees"
    Private _WithholdingVATOnMarketFees As Decimal
    Public Property WithholdingVATOnMarketFees() As Decimal
        Get
            Return _WithholdingVATOnMarketFees
        End Get
        Set(ByVal value As Decimal)
            _WithholdingVATOnMarketFees = value
        End Set
    End Property

#End Region

#Region "WithholdingTAXOnMarketFees"
    Private _WithholdingTAXOnMarketFees As Decimal
    Public Property WithholdingTAXOnMarketFees() As Decimal
        Get
            Return _WithholdingTAXOnMarketFees
        End Get
        Set(ByVal value As Decimal)
            _WithholdingTAXOnMarketFees = value
        End Set
    End Property

#End Region

#Region "Totals"
    Private _Totals As Decimal
    Public ReadOnly Property Totals() As Decimal
        Get
            Me._Totals = 0
            Me._Totals = Me.MarketFees + Me.VATOnMarketFees + Me.WithholdingVATOnMarketFees + Me.WithholdingTAXOnMarketFees
            Return _Totals
        End Get
    End Property

#End Region

#Region "DefaultInterestOnMarketFees"
    Private _DefaultInterestOnMarketFees As Decimal
    Public Property DefaultInterestOnMarketFees() As Decimal
        Get
            Return _DefaultInterestOnMarketFees
        End Get
        Set(ByVal value As Decimal)
            _DefaultInterestOnMarketFees = value
        End Set
    End Property

#End Region

#Region "DefaultInterestOnVATOnMarketFees"
    Private _DefaultInterestOnVATOnMarketFees As Decimal
    Public Property DefaultInterestOnVATOnMarketFees() As Decimal
        Get
            Return _DefaultInterestOnVATOnMarketFees
        End Get
        Set(ByVal value As Decimal)
            _DefaultInterestOnVATOnMarketFees = value
        End Set
    End Property

#End Region

#Region "DefaultInterestOnWithholdingVAT"
    Private _DefaultInterestOnWithholdingVAT As Decimal
    Public Property DefaultInterestOnWithholdingVAT() As Decimal
        Get
            Return _DefaultInterestOnWithholdingVAT
        End Get
        Set(ByVal value As Decimal)
            _DefaultInterestOnWithholdingVAT = value
        End Set
    End Property

#End Region

#Region "DefaultInterestOnWithholdingTAX"
    Private _DefaultInterestOnWithholdingTAX As Decimal
    Public Property DefaultInterestOnWithholdingTAX() As Decimal
        Get
            Return _DefaultInterestOnWithholdingTAX
        End Get
        Set(ByVal value As Decimal)
            _DefaultInterestOnWithholdingTAX = value
        End Set
    End Property

#End Region

#Region "TotalDefaultInterest"
    Private _TotalDefaultInterest As Decimal
    Public ReadOnly Property TotalDefaultInterest() As Decimal
        Get
            _TotalDefaultInterest = 0
            _TotalDefaultInterest = Me.DefaultInterestOnMarketFees + Me.DefaultInterestOnVATOnMarketFees + _
                                    Me.DefaultInterestOnWithholdingVAT + Me.DefaultInterestOnWithholdingTAX
            Return _TotalDefaultInterest
        End Get
    End Property

#End Region

#Region "GrandTotal"
    Private _GrandTotal As Decimal
    Public ReadOnly Property GrandTotal() As Decimal
        Get
            Me._GrandTotal = Me.Totals + Me.TotalDefaultInterest
            Return _GrandTotal
        End Get
    End Property

#End Region

#Region "CollectionPaymentTransactionType"
    Private _CollectionPaymentTransactionType As EnumCollectionPaymentTransactionType
    Public Property CollectionPaymentTransactionType() As EnumCollectionPaymentTransactionType
        Get
            Return _CollectionPaymentTransactionType
        End Get
        Set(ByVal value As EnumCollectionPaymentTransactionType)
            _CollectionPaymentTransactionType = value
        End Set
    End Property

#End Region

End Class
