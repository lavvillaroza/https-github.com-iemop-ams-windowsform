'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             FuncBankReconStatement
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     February 19, 2013
'Development Group:      Software Development and Support Division
'Description:            Class for Creating entries in Bank Recon Statement
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description
'   April 2, 2012          Juan Carlo L. Panopio         Class initialization
'

Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementLogic

Public Class FuncBankReconStatement
    Private _WBillHelper As WESMBillHelper

    Public Sub New()
        Me._WBillHelper = WESMBillHelper.GetInstance()
    End Sub


    Private _Collection As List(Of Collection)
    Public Property Collection() As List(Of Collection)
        Get
            Return _Collection
        End Get
        Set(ByVal value As List(Of Collection))
            _Collection = value
        End Set
    End Property

    Private _CollectionMonitoring As List(Of CollectionMonitoring)
    Public Property CollectionMonitoring() As List(Of CollectionMonitoring)
        Get
            Return _CollectionMonitoring
        End Get
        Set(ByVal value As List(Of CollectionMonitoring))
            _CollectionMonitoring = value
        End Set
    End Property


    Private _PaymentAllocation As FuncAutoPaymentAllocationResult
    Public Property PaymentAllocation() As FuncAutoPaymentAllocationResult
        Get
            Return _PaymentAllocation
        End Get
        Set(ByVal value As FuncAutoPaymentAllocationResult)
            _PaymentAllocation = value
        End Set
    End Property

    Private _lstChecks As List(Of Check)
    Public Property lstChecks() As List(Of Check)
        Get
            Return _lstChecks
        End Get
        Set(ByVal value As List(Of Check))
            _lstChecks = value
        End Set
    End Property

    Private _lstFundTransfer As List(Of FundTransferFormMain)
    Public Property lstFundTransfer() As List(Of FundTransferFormMain)
        Get
            Return _lstFundTransfer
        End Get
        Set(ByVal value As List(Of FundTransferFormMain))
            _lstFundTransfer = value
        End Set
    End Property

    Private _PRHistory As List(Of PrudentialHistory)
    Public Property PRHistory() As List(Of PrudentialHistory)
        Get
            Return _PRHistory
        End Get
        Set(ByVal value As List(Of PrudentialHistory))
            _PRHistory = value
        End Set
    End Property

    Public Function GenerateBankRecon() As List(Of BankReconMain)
        Dim lstBankRecon As New List(Of BankReconMain)

        '***********************************
        ' Create Bank recon for Settlement
        '***********************************
        Dim bnkReconSettlement As New BankReconMain
        With bnkReconSettlement
            .BankReconType = EnumBankReconType.Settlement
            .BatchCode = "STL-1"
            '.BeginningBalance = 'To get from previous bank recon (Ending)
            .Status = EnumStatus.Active
            .TransactionDate = SystemDate
            .UpdatedBy = Me._WBillHelper.UserName
            .UpdatedDate = SystemDate
        End With
        lstBankRecon.Add(GenerateBankRecon(bnkReconSettlement))

        '***********************************
        ' Create Bank recon for Prudential
        '***********************************
        Dim bnkReconPrudential As New BankReconMain
        With bnkReconPrudential
            .BankReconType = EnumBankReconType.Prudential
            .BatchCode = "PR-1"
            '.BeginningBalance = 'To get from previous bank recon (Ending)
            .Status = EnumStatus.Active
            .TransactionDate = SystemDate
            .UpdatedBy = Me._WBillHelper.UserName
            .UpdatedDate = SystemDate
        End With
        lstBankRecon.Add(GenerateBankRecon(bnkReconPrudential))

        '***********************************
        ' Create Bank recon for NSS
        '***********************************
        Dim bnkReconNSS As New BankReconMain
        With bnkReconNSS
            .BankReconType = EnumBankReconType.NSS
            .BatchCode = "NSS-1"
            '.BeginningBalance = 'To get from previous bank recon (Ending)
            .Status = EnumStatus.Active
            .TransactionDate = SystemDate
            .UpdatedBy = Me._WBillHelper.UserName
            .UpdatedDate = SystemDate
        End With
        lstBankRecon.Add(GenerateBankRecon(bnkReconNSS))

        Return lstBankRecon
    End Function

    Private Function GenerateBankRecon(ByVal bnkRecon As BankReconMain) As BankReconMain

        Select Case bnkRecon.BankReconType
            Case EnumBankReconType.Settlement

                'For Collection(All collections including PR)
                Dim bnkReconDetails As New BankReconDetails
                With bnkReconDetails
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.Collection
                    .Amount = (From x In _Collection _
                               Select x.CollectedAmount).Sum * -1D
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconDetails)

                'For PRDrawdown in Collection
                Dim bnkReconDDown As New BankReconDetails
                With bnkReconDDown
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.PR
                    .Amount = (From x In _CollectionMonitoring _
                               Where x.TransType = EnumCollectionMonitoringType.TransferToPRDrawdown _
                               Select x.Amount).Sum
                    If .Amount > 0 Then
                        .Amount *= -1
                    End If
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconDDown)

                'For FTF from NSS to STL
                Dim bnkReconFromNSS As New BankReconDetails
                With bnkReconFromNSS
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.NSS
                    .Amount = (From x In _lstFundTransfer _
                               Where x.TransType = EnumFTFTransType.TransferNSSToSTL _
                               Select x.TotalAmount).Sum
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconFromNSS)

                'For FTF from STL - NSS
                Dim bnkReconToNSS As New BankReconDetails
                With bnkReconToNSS
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.NSS
                    .Amount = (From x In _lstFundTransfer _
                               Where x.TransType = EnumFTFTransType.TransferSTLToNSS _
                               Select x.TotalAmount).Sum
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconToNSS)

                'For Interest Earned from STL
                Dim bnkReconInterest As New BankReconDetails
                With bnkReconInterest
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.Interest
                    .Amount = (From x In _PaymentAllocation.ParticipantForEFT _
                              Where x.STLInterest <> 0 _
                              Select x.STLInterest).Sum

                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconInterest)

                'For Payment EFT
                Dim bnkReconPaymentEFT As New BankReconDetails
                With bnkReconPaymentEFT
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.PaymentEFT
                    .Amount = (From x In _PaymentAllocation.ParticipantForEFT _
                               Where x.PaymentType = EnumParticipantPaymentType.EFT _
                               Select x.TotalPayment).Sum * -1D
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconPaymentEFT)

                'For Payment LBC/Checks
                Dim bnkReconPaymentCheck As New BankReconDetails
                With bnkReconPaymentCheck
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.PaymentCheck
                    .Amount = (From x In _lstChecks _
                               Where x.Status = EnumCheckStatus.Cleared _
                               Select x.Amount).Sum * -1D
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconPaymentCheck)

                'For Payment Market Transaction Fees
                Dim bnkReconMTF As New BankReconDetails
                With bnkReconMTF
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.MarketFees
                    .Amount = (From x In _lstFundTransfer _
                               Where x.TransType = EnumFTFTransType.TransferMarketFeesToPEMC _
                               Select x.TotalAmount).Sum * -1D
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconMTF)

                'For Payment PR Replenishment
                Dim bnkreconPRRep As New BankReconDetails
                With bnkreconPRRep
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.PR
                    .Amount = (From x In _lstFundTransfer _
                               Where x.TransType = EnumFTFTransType.Replenishment _
                               And (x.BatchCode.Contains("P-") Or x.BatchCode.Contains("C-")) _
                               Select x.TotalAmount).Sum
                End With
                bnkRecon.lstBankReconDetails.Add(bnkreconPRRep)

                'For outstanding Checks
                Dim bnkReconOutstanding As New BankReconDetails
                With bnkReconOutstanding
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.OutstandingCheck
                    .Amount = (From x In _lstChecks _
                               Where x.Status <> EnumCheckStatus.Cleared _
                               Select x.Amount).Sum
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconOutstanding)

            Case EnumBankReconType.Prudential

                'For PR Replenishment from Collection
                Dim bnkReconColReplenish As New BankReconDetails
                With bnkReconColReplenish
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.Collection
                    .Amount = (From x In _PRHistory _
                               Where x.TransType = EnumFTFTransType.Replenishment _
                               And x.BatchCode.Contains("C-") _
                               Select x.Amount).Sum
                    .Amount = Math.Abs(.Amount)
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconColReplenish)

                'For PR Replenishment Direct Update
                Dim bnkReconReplenish As New BankReconDetails
                With bnkReconReplenish
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.PR
                    .Amount = (From x In _PRHistory _
                               Where x.TransType = EnumPrudentialTransType.Replenishment _
                               And x.BatchCode.Contains("PRR") _
                               Select x.Amount).Sum
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconReplenish)

                'For PR Replenishment from Payment Allocation
                Dim bnkReconPaymentReplenish As New BankReconDetails
                With bnkReconPaymentReplenish
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.PaymentCheck
                    .Amount = (From x In _PRHistory _
                               Where x.TransType = EnumPrudentialTransType.Replenishment _
                               And x.BatchCode.Contains("P-") _
                               Select x.Amount).Sum
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconPaymentReplenish)

                'For Interest Earned
                Dim bnkReconInterestEarned As New BankReconDetails
                With bnkReconInterestEarned
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.Interest
                    .Amount = (From x In _PRHistory _
                               Where x.TransType = EnumPrudentialTransType.InterestAmount _
                               Select x.Amount).Sum
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconInterestEarned)

                'For Drawdown
                Dim bnkReconDrawdown As New BankReconDetails
                With bnkReconDrawdown
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.Collection
                    .Amount = Math.Abs((From x In _lstFundTransfer _
                                       Where x.TransType = EnumFTFTransType.DrawDown _
                                       Select x.TotalAmount).Sum)
                    .Amount = Math.Abs(.Amount) * -1D
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconDrawdown)

                'For outstanding Checks
                Dim bnkReconOutstanding As New BankReconDetails
                With bnkReconOutstanding
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.OutstandingCheck
                    .Amount = 0
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconOutstanding)

            Case EnumBankReconType.NSS

                'For NSS to STL
                Dim bnkReconNSStoSTL As New BankReconDetails
                With bnkReconNSStoSTL
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.Collection
                    .Amount = (From x In _lstFundTransfer _
                               Where x.TransType = EnumFTFTransType.TransferNSSToSTL _
                               Select x.TotalAmount).Sum
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconNSStoSTL)

                'Interest uploaded from NSS Module
                Dim bnkReconInterestEarned As New BankReconDetails
                With bnkReconInterestEarned
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.Interest
                    .Amount = (From x In _PaymentAllocation.ParticipantForEFT _
                               Where x.NSSInterest <> 0 _
                               Select x.NSSInterest).Sum

                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconInterestEarned)

                'For STL to NSS
                Dim bnkReconSTLtoNSS As New BankReconDetails
                With bnkReconSTLtoNSS
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.PaymentCheck
                    .Amount = Math.Abs((From x In _lstFundTransfer _
                               Where x.TransType = EnumFTFTransType.TransferSTLToNSS _
                               Select x.TotalAmount).Sum) * -1D
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconSTLtoNSS)

                'Interest to be paid to Participants
                Dim bnkReconInterestPayment As New BankReconDetails
                With bnkReconInterestPayment
                    .BatchCode = bnkRecon.BatchCode
                    .TransactionType = EnumBankReconTransactionType.Interest
                    .Amount = (From x In _PaymentAllocation.ParticipantForEFT _
                               Where x.NSSInterest <> 0 _
                               Select x.NSSInterest).Sum
                    If .Amount > 0 Then
                        .Amount *= -1D
                    End If
                End With
                bnkRecon.lstBankReconDetails.Add(bnkReconInterestPayment)
        End Select
        
        Return bnkRecon
    End Function


End Class
