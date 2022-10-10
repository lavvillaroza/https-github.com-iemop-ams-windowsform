'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             PrudentialMarginCallSummary
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 07, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for Margin Call Summary Report
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   October 03, 2012        Vladimir E. Espiritu            Class initialization
'   January 10, 2013        Vladimir E. Espiritu            Added ReferenceNumber, BillingPeriod and Status properties
'

Option Explicit On
Option Strict On

Public Class PrudentialMarginCallSummary

#Region "Initialization/Constructor"
    Public Sub New()
        Me._TransactionDate = Nothing
        Me._IDNumber = New AMParticipants()
        Me._NoOfMonths = 12
        Me._Total = 0
        Me._MonthlyAverage = 0
        Me._DailyAverage = 0
        Me._MaximumNetExposure = 0
        Me._AdvancePayment = 0
        Me._ActualNetExposure = 0
        Me._PrudentialDeposit = 0
        Me._PrudentialInterest = 0
        Me._TradingLimit = 0
        Me._MarginCallAmount = 0
        Me._Status = EnumMarginCallStatus.NotFinal
        Me._MarginCallDate = Nothing
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

#Region "NoOfMonths"
    Private _NoOfMonths As Integer
    Public Property NoOfMonths() As Integer
        Get
            Return _NoOfMonths
        End Get
        Set(ByVal value As Integer)
            _NoOfMonths = value
        End Set
    End Property
#End Region

#Region "Total"
    Private _Total As Decimal
    Public Property Total() As Decimal
        Get
            Return _Total
        End Get
        Set(ByVal value As Decimal)
            _Total = value
        End Set
    End Property

#End Region

#Region "MonthlyAverage"
    Private _MonthlyAverage As Decimal
    Public Property MonthlyAverage() As Decimal
        Get
            Return _MonthlyAverage
        End Get
        Set(ByVal value As Decimal)
            _MonthlyAverage = value
        End Set
    End Property

#End Region

#Region "DailyAverage"
    Private _DailyAverage As Decimal
    Public Property DailyAverage() As Decimal
        Get
            Return _DailyAverage
        End Get
        Set(ByVal value As Decimal)
            _DailyAverage = value
        End Set
    End Property


#End Region

#Region "MaximumNetExposure"
    Private _MaximumNetExposure As Decimal
    Public Property MaximumNetExposure() As Decimal
        Get
            Return _MaximumNetExposure
        End Get
        Set(ByVal value As Decimal)
            _MaximumNetExposure = value
        End Set
    End Property

#End Region

#Region "AdvancePayment"
    Private _AdvancePayment As Decimal
    Public Property AdvancePayment() As Decimal
        Get
            Return _AdvancePayment
        End Get
        Set(ByVal value As Decimal)
            _AdvancePayment = value
        End Set
    End Property

#End Region

#Region "ActualNetExposure"
    Private _ActualNetExposure As Decimal
    Public Property ActualNetExposure() As Decimal
        Get
            Return _ActualNetExposure
        End Get
        Set(ByVal value As Decimal)
            _ActualNetExposure = value
        End Set
    End Property

#End Region

#Region "PrudentialDeposit"
    Private _PrudentialDeposit As Decimal
    Public Property PrudentialDeposit() As Decimal
        Get
            Return _PrudentialDeposit
        End Get
        Set(ByVal value As Decimal)
            _PrudentialDeposit = value
        End Set
    End Property

#End Region

#Region "PrudentialInterest"
    Private _PrudentialInterest As Decimal
    Public Property PrudentialInterest() As Decimal
        Get
            Return _PrudentialInterest
        End Get
        Set(ByVal value As Decimal)
            _PrudentialInterest = value
        End Set
    End Property

#End Region

#Region "TotalPrudential"
    Private _TotalPrudential As Decimal
    Public Property TotalPrudential() As Decimal
        Get
            Return _TotalPrudential
        End Get
        Set(ByVal value As Decimal)
            _TotalPrudential = value
        End Set
    End Property

#End Region

#Region "TradingLimit"
    Private _TradingLimit As Decimal
    Public Property TradingLimit() As Decimal
        Get
            Return _TradingLimit
        End Get
        Set(ByVal value As Decimal)
            _TradingLimit = value
        End Set
    End Property

#End Region

#Region "MarginCallAmount"
    Private _MarginCallAmount As Decimal
    Public Property MarginCallAmount() As Decimal
        Get
            Return _MarginCallAmount
        End Get
        Set(ByVal value As Decimal)
            _MarginCallAmount = value
        End Set
    End Property

#End Region

#Region "Status"
    Private _Status As EnumMarginCallStatus
    Public Property Status() As EnumMarginCallStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As EnumMarginCallStatus)
            _Status = value
        End Set
    End Property

#End Region

#Region "MarginCallDate"
    Private _MarginCallDate As Date
    Public Property MarginCallDate() As Date
        Get
            Return _MarginCallDate
        End Get
        Set(ByVal value As Date)
            _MarginCallDate = value
        End Set
    End Property

#End Region

End Class
