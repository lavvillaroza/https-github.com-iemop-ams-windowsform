'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             NetSettlementSurplusMain
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 10, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for Net Settlement Surplus (NSS)
'Arguments/Parameters:  
'Files/Database Tables:  AM_NSS_MAIN
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   Febraury 10, 2013       Vladimir E. Espiritu            Class initialization

Public Class NetSettlementSurplusMain

#Region "Initialization/Constructor"
    Public Sub New()
        Me._TransactionDate = Nothing
        Me._BillingPeriod = New CalendarBillingPeriod()
        Me._NetSettlementSurplus = 0
        Me._NetSettlementSurplusAdjustment = 0
        Me._NSSRetentionBalance = 0
        Me._Interest = 0
        Me._ReturnOfInterest = 0
        Me._InterestOnInterest = 0
        Me._NSSRABankStatement = 0
        Me._BankRate = 0
        Me._IsPosted = Nothing
        Me._ListOfNetSettlementSurplusDetails = New List(Of NetSettlementSurplusDetails)
    End Sub
#End Region


#Region "TransactionDate"
    Private _TransactionDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            _TransactionDate = value
        End Set
    End Property

#End Region

#Region "BillingPeriod"
    Private _BillingPeriod As CalendarBillingPeriod
    Public Property BillingPeriod() As CalendarBillingPeriod
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As CalendarBillingPeriod)
            _BillingPeriod = value
        End Set
    End Property


#End Region

#Region "NetSettlementSurplus"
    Private _NetSettlementSurplus As Decimal
    Public Property NetSettlementSurplus() As Decimal
        Get
            Return _NetSettlementSurplus
        End Get
        Set(ByVal value As Decimal)
            _NetSettlementSurplus = value
        End Set
    End Property

#End Region

#Region "NetSettlementSurplusAdjustment"
    Private _NetSettlementSurplusAdjustment As Decimal
    Public Property NetSettlementSurplusAdjustment() As Decimal
        Get
            Return _NetSettlementSurplusAdjustment
        End Get
        Set(ByVal value As Decimal)
            _NetSettlementSurplusAdjustment = value
        End Set
    End Property

#End Region

#Region "NSSRetentionBalance"
    Private _NSSRetentionBalance As Decimal
    Public Property NSSRetentionBalance() As Decimal
        Get
            Return _NSSRetentionBalance
        End Get
        Set(ByVal value As Decimal)
            _NSSRetentionBalance = value
        End Set
    End Property


#End Region

#Region "Interest"
    Private _Interest As Decimal
    Public Property Interest() As Decimal
        Get
            Return _Interest
        End Get
        Set(ByVal value As Decimal)
            _Interest = value
        End Set
    End Property

#End Region

#Region "ReturnOfInterest"
    Private _ReturnOfInterest As Decimal
    Public Property ReturnOfInterest() As Decimal
        Get
            Return _ReturnOfInterest
        End Get
        Set(ByVal value As Decimal)
            _ReturnOfInterest = value
        End Set
    End Property

#End Region

#Region "InterestOnInterest"
    Private _InterestOnInterest As Decimal
    Public Property InterestOnInterest() As Decimal
        Get
            Return _InterestOnInterest
        End Get
        Set(ByVal value As Decimal)
            _InterestOnInterest = value
        End Set
    End Property

#End Region

#Region "NSSRABankStatement"
    Private _NSSRABankStatement As Decimal
    Public Property NSSRABankStatement() As Decimal
        Get
            Return _NSSRABankStatement
        End Get
        Set(ByVal value As Decimal)
            _NSSRABankStatement = value
        End Set
    End Property

#End Region

#Region "BankRate"
    Private _BankRate As Decimal
    Public Property BankRate() As Decimal
        Get
            Return _BankRate
        End Get
        Set(ByVal value As Decimal)
            _BankRate = value
        End Set
    End Property

#End Region

#Region "IsPosted"
    Private _IsPosted As EnumIsPosted
    Public Property IsPosted() As EnumIsPosted
        Get
            Return _IsPosted
        End Get
        Set(ByVal value As EnumIsPosted)
            _IsPosted = value
        End Set
    End Property


#End Region

#Region "ListOfNetSettlementSurplusDetails"
    Private _ListOfNetSettlementSurplusDetails As List(Of NetSettlementSurplusDetails)
    Public Property ListOfNetSettlementSurplusDetails() As List(Of NetSettlementSurplusDetails)
        Get
            Return _ListOfNetSettlementSurplusDetails
        End Get
        Set(ByVal value As List(Of NetSettlementSurplusDetails))
            _ListOfNetSettlementSurplusDetails = value
        End Set
    End Property

#End Region

End Class
