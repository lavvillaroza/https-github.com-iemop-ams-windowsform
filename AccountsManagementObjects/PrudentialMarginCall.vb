'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             PrudentialMarginCall
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     October 03, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Margin Call
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

Public Class PrudentialMarginCall

#Region "Initialization/Constructor"
    Public Sub New()
        Me._ReferenceNumber = 0
        Me._ListOfWESMBills = New List(Of WESMBill)
        Me._ListOfPreviousWESMBill = New List(Of WESMBill)
        Me._TransactionDate = Nothing
        Me._IDNumber = New AMParticipants()
        Me._NoOfMonths = 12
        Me._Total = 0
        Me._MonthlyAverage = 0
        Me._DailyAverage = 0
        Me._MaximumNetExposure = 0
        Me._AdvancePayment = 0
        Me._OutstandingBalance = 0
        Me._ActualNetExposure = 0
        Me._PrudentialDeposit = 0
        Me._PrudentialInterest = 0
        Me._TradingLimit = 0
        Me._MarginCallAmount = 0
        Me._Status = EnumMarginCallStatus.NotFinal
        Me._MarginCallDate = Nothing
    End Sub
#End Region


#Region "ReferenceNumber"
    Private _ReferenceNumber As Long
    Public Property ReferenceNumber() As Long
        Get
            Return _ReferenceNumber
        End Get
        Set(ByVal value As Long)
            _ReferenceNumber = value
        End Set
    End Property

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
    Public ReadOnly Property NoOfMonths() As Integer
        Get
            _NoOfMonths = (From x In Me.ListOfPreviousWESMBill _
                           Select x.BillingPeriod Distinct).Count()

            If Me.ListOfWESMBills.Count > 0 Then
                _NoOfMonths += 1
            End If
            Return _NoOfMonths
        End Get
    End Property


#End Region

#Region "Total"
    Private _Total As Decimal
    Public ReadOnly Property Total() As Decimal
        Get
            _Total = 0
            For Each item In Me.ListOfPreviousWESMBill
                _Total += item.Amount
            Next

            For Each item In Me.ListOfWESMBills
                _Total += item.Amount
            Next
            Return Math.Round(_Total, 2)
        End Get
    End Property

#End Region

#Region "TotalCurrentBill"
    Private _TotalCurrentBill As Decimal
    Public ReadOnly Property TotalCurrentBill() As Decimal
        Get
            _TotalCurrentBill = 0
            For Each item In Me.ListOfWESMBills
                _TotalCurrentBill += item.Amount
            Next
            Return Math.Round(_TotalCurrentBill, 2)

            Return _TotalCurrentBill
        End Get
    End Property


#End Region

#Region "MonthlyAverage"
    Private _MonthlyAverage As Decimal
    Public ReadOnly Property MonthlyAverage() As Decimal
        Get
            If Me.NoOfMonths <> 0 And Me.Total <> 0 Then
                _MonthlyAverage = Me.Total / Me.NoOfMonths
            End If

            Return Math.Round(_MonthlyAverage, 2)
        End Get
    End Property

#End Region

#Region "DailyAverage"
    Private _DailyAverage As Decimal
    Public ReadOnly Property DailyAverage() As Decimal
        Get
            If Me.MonthlyAverage <> 0 Then
                _DailyAverage = Me.MonthlyAverage / 30
            End If

            Return Math.Round(_DailyAverage, 2)
        End Get
    End Property

#End Region

#Region "MaximumNetExposure"
    Private _MaximumNetExposure As Decimal
    Public ReadOnly Property MaximumNetExposure() As Decimal
        Get
            _MaximumNetExposure = Me.DailyAverage * AMModule.MNEMultiplier
            Return Math.Round(_MaximumNetExposure, 2)
        End Get
    End Property

#End Region

#Region "OutstandingBalance"
    Private _OutstandingBalance As Decimal
    Public Property OutstandingBalance() As Decimal
        Get
            Return _OutstandingBalance
        End Get
        Set(ByVal value As Decimal)
            _OutstandingBalance = value
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
    Public ReadOnly Property ActualNetExposure() As Decimal
        Get
            _ActualNetExposure = (Me.MaximumNetExposure + Me.AdvancePayment + Me.OutstandingBalance) * -1D
            Return Math.Round(_ActualNetExposure, 2)
        End Get
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
    Public ReadOnly Property TotalPrudential() As Decimal
        Get
            _TotalPrudential = Me.PrudentialDeposit + Me.PrudentialInterest
            Return _TotalPrudential
        End Get
    End Property

#End Region

#Region "TradingLimit"
    Private _TradingLimit As Decimal
    Public ReadOnly Property TradingLimit() As Decimal
        Get
            _TradingLimit = Me.TotalPrudential * (AMModule.TradingLimitMultiplier / 100D)
            Return Math.Round(_TradingLimit, 2)
        End Get
    End Property
#End Region

#Region "MarginCallAmount"
    Private _MarginCallAmount As Decimal
    Public ReadOnly Property MarginCallAmount() As Decimal
        Get
            _MarginCallAmount = Me.ActualNetExposure - TradingLimit
            Return Math.Round(_MarginCallAmount, 2)
        End Get
    End Property

#End Region

#Region "ListOfWESMBill"
    Private _ListOfWESMBills As List(Of WESMBill)
    Public Property ListOfWESMBills() As List(Of WESMBill)
        Get
            Return _ListOfWESMBills
        End Get
        Set(ByVal value As List(Of WESMBill))
            _ListOfWESMBills = value
        End Set
    End Property

#End Region

#Region "ListOfPreviousWESMBill"
    Private _ListOfPreviousWESMBill As List(Of WESMBill)
    Public Property ListOfPreviousWESMBill() As List(Of WESMBill)
        Get
            Return _ListOfPreviousWESMBill
        End Get
        Set(ByVal value As List(Of WESMBill))
            _ListOfPreviousWESMBill = value
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
