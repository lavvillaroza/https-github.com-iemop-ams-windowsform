'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             STLBeginningBalance
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     November 14, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for Beginning Balances for STL Notice
'Arguments/Parameters:  
'Files/Database Tables:  AM_STL_NOTICE_BEGINNING_BAL
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description

Public Class STLBeginningBalance
    Inherits WESMBillSummary

    Public Sub New()
        Me.STLTransactionDate = SystemDate
        Me.Adjustment = 0
        Me.AMCode = ""
        Me.BeginningBalance = 0
        Me.BillPeriod = 0
        Me.ChargeType = Nothing
        Me.DueDate = Nothing
        Me.EndingBalance = 0
        Me.Energy = 0
        Me.GroupNo = 0
        Me.IDNumber = New AMParticipants
        Me.IDType = Nothing
        Me.INVDMCMNo = 0
        Me.ListOfWESMBill = New List(Of WESMBill)
        Me.ListOfWESMBillSummaryDetails = New List(Of OffsetP2PC2CDetails)
        Me.MarketFees = 0
        Me.NewDueDate = Nothing
        Me.SummaryType = Nothing
        Me.VATEnergy = 0
        Me.VATMarketFees = 0
        Me.WESMBillSummaryNo = 0
    End Sub

    Public Sub New(ByVal TransactionDate As Date, ByVal SummaryBill As WESMBillSummary)
        Me.STLTransactionDate = TransactionDate
        With SummaryBill
            Me.Adjustment = .Adjustment
            Me.AMCode = .AMCode
            Me.BeginningBalance = .BeginningBalance
            Me.BillPeriod = .BillPeriod
            Me.ChargeType = .ChargeType
            Me.DueDate = .DueDate
            Me.EndingBalance = .EndingBalance
            Me.Energy = .Energy
            Me.GroupNo = .GroupNo
            Me.IDNumber = .IDNumber
            Me.IDType = .IDType
            Me.INVDMCMNo = .INVDMCMNo
            Me.ListOfWESMBill = .ListOfWESMBill
            Me.ListOfWESMBillSummaryDetails = .ListOfWESMBillSummaryDetails
            Me.MarketFees = .MarketFees
            Me.NewDueDate = .NewDueDate
            Me.SummaryType = .SummaryType
            Me.VATEnergy = .VATEnergy
            Me.VATMarketFees = .VATMarketFees
            Me.WESMBillSummaryNo = .WESMBillSummaryNo
        End With
    End Sub

    Private _STLTransactionDate As Date
    Public Property STLTransactionDate() As Date
        Get
            Return _STLTransactionDate
        End Get
        Set(ByVal value As Date)
            _STLTransactionDate = value
        End Set
    End Property

End Class
