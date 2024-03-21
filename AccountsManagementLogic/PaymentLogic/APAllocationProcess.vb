Imports System.Data.Linq.SqlClient
Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports System.Threading
Public Class APAllocationProcess
    Dim newProgress As ProgressClass
    Private boolAdjStatusFailed As Boolean
    Public Sub New()
        Me._WBillHelper = WESMBillHelper.GetInstance
    End Sub


#Region "WESMBillHelper"
    Public _WBillHelper As WESMBillHelper
    Private ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
        End Get
    End Property
#End Region

#Region "OffsettingSequence"
    Private _OffsettingSequence As Integer
    Public Property OffsettingSequence() As Integer
        Get
            Return _OffsettingSequence
        End Get
        Set(value As Integer)
            _OffsettingSequence = value
        End Set
    End Property
#End Region

#Region "Property of MFwithVATAllocationList"
    Private _MFWithVATAPAllocationList As New List(Of APAllocation)
    Public ReadOnly Property MFWithVATAPAllocationList() As List(Of APAllocation)
        Get
            Return _MFWithVATAPAllocationList
        End Get
    End Property
#End Region

#Region "Property of MFwithVATAllocationListDT"
    Private _MFWithVATAPAllocationListDT As New DataTable
    Public ReadOnly Property MFWithVATAPAllocationListDT() As DataTable
        Get
            Return _MFWithVATAPAllocationListDT
        End Get
    End Property
#End Region

#Region "Property of EnergyAllocationList"
    Private _EnergyAPAllocationList As New List(Of APAllocation)
    Public ReadOnly Property EnergyAPAllocationList() As List(Of APAllocation)
        Get
            Return _EnergyAPAllocationList
        End Get
    End Property
#End Region

#Region "Property of VATonEnergyAllocationList"
    Private _VATonEnergyAPAllocationList As New List(Of APAllocation)
    Public ReadOnly Property VATonEnergyAPAllocationList() As List(Of APAllocation)
        Get
            Return _VATonEnergyAPAllocationList
        End Get
    End Property
#End Region

#Region "Property of DMCMSummaryList"
    Private _ListOfDMCMSummary As New List(Of DebitCreditMemoSummaryNew)
    Public Property ListOfDMCMSummary() As List(Of DebitCreditMemoSummaryNew)
        Get
            Return _ListOfDMCMSummary
        End Get
        Set(value As List(Of DebitCreditMemoSummaryNew))
            _ListOfDMCMSummary = value
        End Set
    End Property
#End Region

#Region "Property of WESM Transaction Cover Summary"
    Private _WESMTransCoverSummaryList As New List(Of WESMBillAllocCoverSummary)
    Public ReadOnly Property WESMTransCoverSummaryList() As List(Of WESMBillAllocCoverSummary)
        Get
            Return _WESMTransCoverSummaryList
        End Get
    End Property
#End Region

#Region "Property of WESM Transaction Details Summary"
    Private _WESMTransDetailsSummaryList As New List(Of WESMTransDetailsSummary)
    Public ReadOnly Property WESMTransDetailsSummaryList() As List(Of WESMTransDetailsSummary)
        Get
            Return _WESMTransDetailsSummaryList
        End Get
    End Property
#End Region

#Region "Property of WESM Transaction Details Summary"
    'added by lance to speed up the process as of 07/28/2023
    Private _WESMTransDetailsSummaryHistoryDic As New Dictionary(Of String, WESMTransDetailsSummaryHistory)
    Private _WESMTransDetailsSummaryHistoryList As New List(Of WESMTransDetailsSummaryHistory)
    Public ReadOnly Property WESMTransDetailsSummaryHistoryList() As List(Of WESMTransDetailsSummaryHistory)
        Get
            Return _WESMTransDetailsSummaryHistoryList
        End Get
    End Property
#End Region

#Region "List Of DMCM for AP"
    Private _ListofDMCMAP As New List(Of DebitCreditMemo)
    Public Property ListofDMCMAP() As List(Of DebitCreditMemo)
        Get
            Return _ListofDMCMAP
        End Get
        Set(value As List(Of DebitCreditMemo))
            _ListofDMCMAP = value
        End Set
    End Property
#End Region

#Region "PaymentProformaEntries"
    Public _PaymentProformaEntries As New PaymentProformaEntries
    Private ReadOnly Property PaymentProformaEntries() As PaymentProformaEntries
        Get
            Return _PaymentProformaEntries
        End Get
    End Property
#End Region

#Region "AP Allocation Date"
    Private _APAllocationDate As New Date
    Public ReadOnly Property APAllocationDate() As Date
        Get
            Return _APAllocationDate
        End Get
    End Property
#End Region

#Region "DailyInterestRate"
    Public _DailyInterestRate As Decimal
    Private ReadOnly Property DailyInterestRate() As Decimal
        Get
            Return _DailyInterestRate
        End Get
    End Property
#End Region

#Region "AMParticipantsList"
    Public _AMParticipantsList As List(Of AMParticipants)
    Private ReadOnly Property AMParticipantsList() As List(Of AMParticipants)
        Get
            Return _AMParticipantsList
        End Get
    End Property
#End Region

#Region "Property of Fund Transfer Form"
    Private _FundTransferForm As New List(Of FundTransferFormMain)
    Public Property FundTransferform() As List(Of FundTransferFormMain)
        Get
            Return _FundTransferForm
        End Get
        Set(ByVal value As List(Of FundTransferFormMain))
            _FundTransferForm = value
        End Set
    End Property
#End Region

#Region "AP Allocation On MF"
    Public Sub ComputeMFAPAllocationList(ByVal AllocationDate As Date,
                                        ByRef WESMBillSummaryList As List(Of WESMBillSummary),
                                        ByVal progress As IProgress(Of ProgressClass))
        'Dim WESMBillSummaryListOnMFMFV = (From x In WESMBillSummaryList
        '                                  Where (x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV) _
        '                                  And x.EndingBalance > 0 And x.DueDate <= AllocationDate Select x).ToList()

        'Me._APAllocationDate = AllocationDate
        'Me.APMFAllocationProc(WESMBillSummaryListOnMFMFV, progress)
    End Sub
#End Region

#Region "AP Allocation On Energy"
    Public Sub ComputeEnergyAPAllocationList(ByVal AllocationDate As Date,
                                             ByRef WESMBillSummaryList As List(Of WESMBillSummary),
                                             ByVal listARCollectionEnergy As List(Of ARCollection),
                                             ByVal progress As IProgress(Of ProgressClass))
        Dim WESMBillSummaryListOnEnergy = (From x In WESMBillSummaryList
                                           Where x.ChargeType = EnumChargeType.E And x.DueDate <= AllocationDate
                                           Select x).ToList()
        Me._APAllocationDate = AllocationDate
        Me.APEnergyAllocationProcess(WESMBillSummaryListOnEnergy, listARCollectionEnergy, progress)
    End Sub
#End Region

#Region "AP Allocation On Energy VAT"
    Public Sub ComputeVATAPAllocationList(ByVal AllocationDate As Date,
                                          ByRef WESMBillSummaryList As List(Of WESMBillSummary),
                                          ByVal listARCollectionVAT As List(Of ARCollection),
                                          ByVal progress As IProgress(Of ProgressClass))

        Dim WESMBillSummaryListOnVAT = (From x In WESMBillSummaryList Where x.ChargeType = EnumChargeType.EV And x.EndingBalance > 0 Select x).ToList()
        Me._APAllocationDate = AllocationDate
        Me.APVATAllocationProcess(WESMBillSummaryListOnVAT, listARCollectionVAT, progress)
    End Sub
#End Region

#Region "APAllocation on Energy and VAT"
    Public Sub GetAPAllocationList(ByVal AllocationDate As Date,
                                   ByRef WESMBillSummaryList As List(Of WESMBillSummary),
                                   ByVal WESMBillSalesAndPurchasesList As List(Of WESMBillSalesAndPurchasedForWT),
                                   ByVal listARCollectionEnergy As List(Of ARCollection),
                                   ByVal listARCollectionVAT As List(Of ARCollection),
                                   ByVal listARCollectionMF As List(Of ARCollection),
                                   ByVal progress As IProgress(Of ProgressClass))

        Dim WESMBillSummaryListOnEnergy = (From x In WESMBillSummaryList Where x.ChargeType = EnumChargeType.E And x.DueDate <= AllocationDate Select x).ToList()
        Dim WESMBillSummaryListOnVAT = (From x In WESMBillSummaryList Where x.ChargeType = EnumChargeType.EV And x.DueDate <= AllocationDate Select x).ToList()
        Dim WESMBillSummaryListOnMFMF = (From x In WESMBillSummaryList
                                         Where (x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV) _
                                         And x.EndingBalance <> 0 And x.DueDate <= AllocationDate Select x).ToList()

        Dim getWBSForBRuling As List(Of Date) = WESMBillSummaryList.
                                                Where(Function(a) a.INVDMCMNo.StartsWith(AMModule.BIRRulingPrefix.ToString) And Not a.INVDMCMNo.EndsWith("-ADJ")).
                                                Select(Function(x) x.DueDate).Distinct.OrderBy(Function(y) y).ToList

        Dim getListOfWESMTransNoList As List(Of String) = listARCollectionEnergy.Select(Function(x) x.InvoiceNumber).
                                                          Distinct.Union(listARCollectionVAT.Select(Function(y) y.InvoiceNumber).Distinct.ToList).ToList

        newProgress = New ProgressClass
        newProgress.ProgressMsg = "Fetching WTA Summary Details for Payment Allocation..."
        progress.Report(newProgress)

        For Each item In getListOfWESMTransNoList.OrderBy(Function(x) x).ToList
            Me._WESMTransDetailsSummaryList.AddRange(Me.WBillHelper.GetListWESMTransDetailsSummary(item))
        Next

        Me._WESMTransCoverSummaryList.TrimExcess()
        Me._WESMTransDetailsSummaryList.TrimExcess()
        _APAllocationDate = AllocationDate

        Me.APEnergyAllocationProcess(WESMBillSummaryListOnEnergy, listARCollectionEnergy, progress)
        Me.APVATAllocationProcess(WESMBillSummaryListOnVAT, listARCollectionVAT, progress)
        Me.APMFAllocationProc(WESMBillSummaryListOnMFMF, listARCollectionMF, progress)
        Me._WESMTransDetailsSummaryHistoryList = _WESMTransDetailsSummaryHistoryDic.Select(Function(x) x.Value).ToList()
    End Sub
#End Region

#Region "Get APAllocation based on DB"
    Public Sub GetAPAllocationList(ByVal _APAllocationList As List(Of APAllocation))
        Dim APAllocationListonEnergy As List(Of APAllocation) = (From x In _APAllocationList
                                                                 Where x.ChargeType = EnumChargeType.E
                                                                 Select x).ToList()
        Dim APAllocationListonVATonEnergy As List(Of APAllocation) = (From x In _APAllocationList
                                                                      Where x.ChargeType = EnumChargeType.EV
                                                                      Select x).ToList()
        Dim APAllocationListonMFWithVAT As List(Of APAllocation) = (From x In _APAllocationList
                                                                    Where x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV
                                                                    Select x).ToList()
        Me._EnergyAPAllocationList = APAllocationListonEnergy
        Me._VATonEnergyAPAllocationList = APAllocationListonVATonEnergy
        Me._MFWithVATAPAllocationList = APAllocationListonMFWithVAT
    End Sub
#End Region

#Region "Method Step 1 for AP Allocation on MF"
    Private Sub APMFAllocationProc(ByRef WESMBillSummaryListOnMFMFV As List(Of WESMBillSummary),
                                   ByVal listARCollection As List(Of ARCollection),
                                   ByVal progress As IProgress(Of ProgressClass))

        Dim GetWESMBillSUmmaryListOnMFMFVAP As List(Of WESMBillSummary) = (From x In WESMBillSummaryListOnMFMFV
                                                                           Where x.EndingBalance > 0
                                                                           Select x).ToList

        Dim getTotalCollectedInARAmountDefInt As Decimal = listARCollection.
                                                           Where(Function(y) y.CollectionType = EnumCollectionType.DefaultInterestOnMF).
                                                           Select(Function(x) x.AllocationAmount).Sum()

        Dim getTotalCollectedInARAmountMF As Decimal = listARCollection.
                                                           Where(Function(y) y.CollectionType = EnumCollectionType.MarketFees).
                                                           Select(Function(x) x.AllocationAmount).Sum()

        Dim getTotalEndingBalance As Decimal = GetWESMBillSUmmaryListOnMFMFVAP.Select(Function(x) x.EndingBalance).Sum()

        If Math.Abs(getTotalCollectedInARAmountMF + getTotalCollectedInARAmountDefInt) >= Math.Abs(getTotalEndingBalance) And GetWESMBillSUmmaryListOnMFMFVAP.Count > 0 Then
            newProgress = New ProgressClass
            newProgress.ProgressMsg = "Processing Market Fees AP offsetting paid by IEMOP..."
            progress.Report(newProgress)
            Me.APPaidByPEMCAccProcess(GetWESMBillSUmmaryListOnMFMFVAP)
        End If

    End Sub

    Private Sub APPaidByPEMCAccProcess(ByRef WESMBillSummaryListOnMFMFV As List(Of WESMBillSummary))
        'Paid By PEMC
        For Each item In WESMBillSummaryListOnMFMFV
            Dim AMParticipantsInfo = (From x In Me.AMParticipantsList Where x.IDNumber = item.IDNumber.IDNumber Select x).FirstOrDefault
            Select Case item.ChargeType
                Case EnumChargeType.MF
                    Dim MFAmount As Decimal = item.EndingBalance
                    Dim MFWHTaxAmount As Decimal = 0 'Math.Round(item.BeginningBalance * AMParticipantsInfo.MarketFeesWHTax * -1, 2) 'Disabled as of 01/25/2024
                    Dim MFVWHVATAmount As Decimal = 0 'Math.Round(item.BeginningBalance * AMParticipantsInfo.MarketFeesWHVAT * -1, 2) 'Disabled as of 01/25/2024
                    Dim MFAmountTotal As Decimal = MFAmount - (MFWHTaxAmount + MFVWHVATAmount)
                    Dim APAllocationOnMFWHTax As New APAllocation
                    Dim APAllocationOnMF As New APAllocation

                    If Not MFAmount = Math.Round(MFWHTaxAmount, 2) Then
                        'WithHolding Tax Amount
                        If MFWHTaxAmount <> 0 Then
                            APAllocationOnMFWHTax = Me.CreateAPAllocationItem(item, MFWHTaxAmount, EnumPaymentNewType.WithholdingTaxOnMF, EnumCollectionCategory.Cash)
                            Me.MFWithVATAPAllocationList.Add(APAllocationOnMFWHTax)
                        End If

                        'WithHolding VAT Amount
                        If MFVWHVATAmount <> 0 Then
                            APAllocationOnMFWHTax = Me.CreateAPAllocationItem(item, MFVWHVATAmount, EnumPaymentNewType.WithholdingVatOnMF, EnumCollectionCategory.Cash)
                            Me.MFWithVATAPAllocationList.Add(APAllocationOnMFWHTax)
                        End If

                        'Pay the Outstanding Balance on MF
                        APAllocationOnMF = Me.CreateAPAllocationItem(item, MFAmount, EnumPaymentNewType.MarketFees, EnumCollectionCategory.Cash)
                        Me.MFWithVATAPAllocationList.Add(APAllocationOnMF)
                    End If

                Case EnumChargeType.MFV
                    Dim MFVAmount As Decimal = item.EndingBalance
                    Dim MFVAmountTotal As Decimal = MFVAmount
                    Dim APAllocationOnMFWHTax As New APAllocation
                    Dim APAllocationOnMF As New APAllocation

                    'Pay the Outstanding Balance on MFV
                    APAllocationOnMF = Me.CreateAPAllocationItem(item, MFVAmount, EnumPaymentNewType.VatOnMarketFees, EnumCollectionCategory.Cash)
                    Me.MFWithVATAPAllocationList.Add(APAllocationOnMF)
            End Select
        Next

        Dim getMFMFVInvoice = (From x In WESMBillSummaryListOnMFMFV Select x.INVDMCMNo).Distinct.ToList

        For Each inv In getMFMFVInvoice
            Dim getWBSOnMFMFV As List(Of APAllocation) = (From x In Me.MFWithVATAPAllocationList Where x.InvoiceNumber = inv Select x).ToList
            Dim _DMCM As New DebitCreditMemo
            _DMCM = Me.PaymentProformaEntries.CreateDMCM_APMF(getWBSOnMFMFV, APAllocationDate, Me.AMParticipantsList)
            Me.ListofDMCMAP.Add(_DMCM)
        Next

        If Me.MFWithVATAPAllocationList.Count > 0 Then
            Dim MFWESMBillSummary = (From x In WESMBillSummaryListOnMFMFV Where x.ChargeType = EnumChargeType.MF Select x).ToList
            Me.UpdateWESMBillSummary(MFWESMBillSummary, EnumPaymentNewType.MarketFees)

            Dim MFVWESMBillSummary = (From x In WESMBillSummaryListOnMFMFV Where x.ChargeType = EnumChargeType.MFV Select x).ToList
            Me.UpdateWESMBillSummary(MFVWESMBillSummary, EnumPaymentNewType.VatOnMarketFees)
        End If
    End Sub
#End Region

#Region "Method Step 2 for AP Allocation on MF"
    Private Sub AllocatePaymentOnMF(ByRef WESMBillSummaryListPerBP As List(Of WESMBillSummary),
                                    ByVal TotalARAmountCollectedPerBP As ARCollectionPerBP)
        Dim BPTotalBalance As Decimal = (From x In WESMBillSummaryListPerBP Select x.EndingBalance).Sum() 'Total of Outstanding Balances per BillingPeriod based on WESMBillSummaryList
        Dim APAllocationListPerBP As New List(Of APAllocation)

        Dim ListofParticipants As List(Of String) = WESMBillSummaryListPerBP.Select(Function(x) x.IDNumber.IDNumber).Distinct.ToList()
        For Each Participant In ListofParticipants
            'Allocate AP on MF From Cash Collection
            Dim GetAMParticipantsInfo = (From x In Me.AMParticipantsList Where x.IDNumber = Participant Select x).FirstOrDefault
            Dim GetWESMBillSummaryAPMF As WESMBillSummary = (From x In WESMBillSummaryListPerBP
                                                             Where x.WESMBillBatchNo = TotalARAmountCollectedPerBP.WESMBillBatchNo _
                                                             And x.ChargeType = EnumChargeType.MF
                                                             Select x).FirstOrDefault

            Dim GetWESMBillSummaryAPMFV As WESMBillSummary = (From x In WESMBillSummaryListPerBP
                                                              Where x.WESMBillBatchNo = TotalARAmountCollectedPerBP.WESMBillBatchNo _
                                                              And x.ChargeType = EnumChargeType.MFV
                                                              Select x).FirstOrDefault

            Dim TotalWESMBillSummaryPerID As Decimal = GetWESMBillSummaryAPMF.EndingBalance + GetWESMBillSummaryAPMFV.EndingBalance

            'Allocate AP on MF From Cash Collection
            If TotalARAmountCollectedPerBP.CashAmount <> 0 Then
                Dim CashShare As Decimal = Me.ComputeAllocation(TotalWESMBillSummaryPerID, BPTotalBalance, TotalARAmountCollectedPerBP.CashAmount)

                Dim CashShareOnMF As Decimal = Math.Round((GetWESMBillSummaryAPMF.EndingBalance / TotalWESMBillSummaryPerID) * CashShare, 2)
                Dim CashShareOnMFV As Decimal = Math.Round((GetWESMBillSummaryAPMFV.EndingBalance / TotalWESMBillSummaryPerID) * CashShare, 2)
                Dim CheckTotalCashShare As Decimal = CashShareOnMF + CashShareOnMFV

                If CheckTotalCashShare <> CashShare Then
                    Dim AdjustCash As Decimal = CashShare - CheckTotalCashShare
                    CashShareOnMF += AdjustCash
                End If

                Dim CashShareOnMFWHTax As Decimal = (CashShareOnMF * GetAMParticipantsInfo.MarketFeesWHTax) * -1
                Dim CashShareOnMFWHVAT = (CashShareOnMF * GetAMParticipantsInfo.MarketFeesWHVAT) * -1
                Dim APAllocationMF As New APAllocation
                Dim APAllocationMFV As New APAllocation

                If CashShareOnMFWHTax <> 0 Then
                    Dim APAllocationMFWHTax As New APAllocation
                    APAllocationMFWHTax = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMF, CashShareOnMFWHTax,
                                                                    EnumPaymentNewType.WithholdingTaxOnMF,
                                                                    EnumCollectionCategory.Cash)
                    APAllocationListPerBP.Add(APAllocationMFWHTax)
                End If

                If CashShareOnMFWHVAT <> 0 Then
                    Dim APAllocationMFWHVAT As New APAllocation
                    APAllocationMFWHVAT = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMF, CashShareOnMFWHVAT,
                                                                    EnumPaymentNewType.WithholdingVatOnMF,
                                                                    EnumCollectionCategory.Cash)
                    APAllocationListPerBP.Add(APAllocationMFWHVAT)
                End If

                If CashShareOnMF <> 0 Then
                    APAllocationMF = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMF, CashShareOnMF + ((CashShareOnMFWHTax + CashShareOnMFWHVAT) * -1),
                                                           EnumPaymentNewType.MarketFees,
                                                           EnumCollectionCategory.Cash)
                    APAllocationListPerBP.Add(APAllocationMF)
                End If

                If CashShareOnMFV <> 0 Then
                    APAllocationMFV = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMFV, CashShareOnMFV,
                                                           EnumPaymentNewType.VatOnMarketFees,
                                                           EnumCollectionCategory.Cash)
                    APAllocationListPerBP.Add(APAllocationMFV)
                End If
            End If

            'Allocate AP Default Interest on MF from Cash Collection
            If TotalARAmountCollectedPerBP.CashAmountDI <> 0 Then
                Dim CashShareOnDI As Decimal = Me.ComputeAllocation(TotalWESMBillSummaryPerID, BPTotalBalance, TotalARAmountCollectedPerBP.CashAmount)

                Dim CashShareOnDIMF As Decimal = Math.Round((GetWESMBillSummaryAPMF.EndingBalance / TotalWESMBillSummaryPerID) * CashShareOnDI, 2)
                Dim CashShareOnDIMFV As Decimal = Math.Round((GetWESMBillSummaryAPMFV.EndingBalance / TotalWESMBillSummaryPerID) * CashShareOnDI, 2)
                Dim CheckTotalCashDIShare As Decimal = CashShareOnDIMF + CashShareOnDIMFV

                If CheckTotalCashDIShare <> CashShareOnDI Then
                    Dim AdjustCash As Decimal = CashShareOnDI - CheckTotalCashDIShare
                    CashShareOnDIMF += AdjustCash
                End If

                Dim CashShareOnMFWHTaxDI As Decimal = (CashShareOnDIMF * GetAMParticipantsInfo.MarketFeesWHTax) * -1
                Dim CashShareOnMFWHVATDI = (CashShareOnDIMF * GetAMParticipantsInfo.MarketFeesWHVAT) * -1
                Dim APAllocationDIMF As New APAllocation
                Dim APAllocationDIMFV As New APAllocation

                If CashShareOnMFWHTaxDI <> 0 Then
                    Dim APAllocationMFWHTaxDI As New APAllocation
                    APAllocationMFWHTaxDI = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMF, CashShareOnMFWHTaxDI,
                                                                    EnumPaymentNewType.WithholdingTaxOnMF,
                                                                    EnumCollectionCategory.Cash)
                    APAllocationListPerBP.Add(APAllocationMFWHTaxDI)
                End If

                If CashShareOnMFWHVATDI <> 0 Then
                    Dim APAllocationMFWHVATDI As New APAllocation
                    APAllocationMFWHVATDI = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMF, CashShareOnMFWHVATDI,
                                                                    EnumPaymentNewType.WithholdingVatOnMF,
                                                                    EnumCollectionCategory.Cash)
                    APAllocationListPerBP.Add(APAllocationMFWHVATDI)
                End If

                If CashShareOnDIMF <> 0 Then
                    APAllocationDIMF = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMF, CashShareOnDIMF + ((CashShareOnMFWHTaxDI + CashShareOnMFWHVATDI) * -1),
                                                           EnumPaymentNewType.MarketFees,
                                                           EnumCollectionCategory.Cash)
                    APAllocationListPerBP.Add(APAllocationDIMF)
                End If

                If CashShareOnDIMFV <> 0 Then
                    APAllocationDIMFV = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMFV, CashShareOnDIMFV,
                                                                  EnumPaymentNewType.VatOnMarketFees,
                                                                  EnumCollectionCategory.Cash)
                    APAllocationListPerBP.Add(APAllocationDIMFV)
                End If
            End If

            'Allocate AP on MF From offset Energy share
            If TotalARAmountCollectedPerBP.OffsetAmount <> 0 Then
                Dim OffsettingShare As Decimal = Me.ComputeAllocation(TotalWESMBillSummaryPerID, BPTotalBalance, TotalARAmountCollectedPerBP.OffsetAmount)

                Dim OffsettingShareOnMF As Decimal = Math.Round((GetWESMBillSummaryAPMF.EndingBalance / TotalWESMBillSummaryPerID) * OffsettingShare, 2)
                Dim OffsettingShareOnMFV As Decimal = Math.Round((GetWESMBillSummaryAPMFV.EndingBalance / TotalWESMBillSummaryPerID) * OffsettingShare, 2)
                Dim CheckTotalOffsetingShare As Decimal = OffsettingShareOnMF + OffsettingShareOnMFV

                If CheckTotalOffsetingShare <> OffsettingShare Then
                    Dim AdjustCash As Decimal = OffsettingShare - CheckTotalOffsetingShare
                    OffsettingShareOnMF += AdjustCash
                End If

                Dim OffsettingShareOnMFWHTax As Decimal = (OffsettingShareOnMF * GetAMParticipantsInfo.MarketFeesWHTax) * -1
                Dim OffsettingShareOnMFWHVAT = (OffsettingShareOnMF * GetAMParticipantsInfo.MarketFeesWHVAT) * -1
                Dim APAllocationMF As New APAllocation
                Dim APAllocationMFV As New APAllocation

                If OffsettingShareOnMFWHTax <> 0 Then
                    Dim APAllocationMFWHTax As New APAllocation
                    APAllocationMFWHTax = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMF, OffsettingShareOnMFWHTax,
                                                                    EnumPaymentNewType.WithholdingTaxOnMF,
                                                                    EnumCollectionCategory.Cash)
                    APAllocationListPerBP.Add(APAllocationMFWHTax)
                End If

                If OffsettingShareOnMFWHVAT <> 0 Then
                    Dim APAllocationMFWHVAT As New APAllocation
                    APAllocationMFWHVAT = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMF, OffsettingShareOnMFWHVAT,
                                                                    EnumPaymentNewType.WithholdingVatOnMF,
                                                                    EnumCollectionCategory.Cash)
                    APAllocationListPerBP.Add(APAllocationMFWHVAT)
                End If

                If OffsettingShareOnMF <> 0 Then
                    APAllocationMF = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMF, OffsettingShareOnMF + ((OffsettingShareOnMFWHTax + OffsettingShareOnMFWHVAT) * -1),
                                                           EnumPaymentNewType.MarketFees,
                                                           EnumCollectionCategory.Cash)
                    APAllocationListPerBP.Add(APAllocationMF)
                End If

                If OffsettingShareOnMFV <> 0 Then
                    APAllocationMFV = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMFV, OffsettingShareOnMFV,
                                                           EnumPaymentNewType.VatOnMarketFees,
                                                           EnumCollectionCategory.Cash)
                    APAllocationListPerBP.Add(APAllocationMFV)
                End If
            End If

            'Allocate AP on MF from offset DefaultInterestonEnergy Share
            If TotalARAmountCollectedPerBP.OffsetAmountDI <> 0 Then
                Dim OffsettingShareOnDI As Decimal = Me.ComputeAllocation(TotalWESMBillSummaryPerID, BPTotalBalance, TotalARAmountCollectedPerBP.OffsetAmountDI)

                Dim OffsettingShareOnDIMF As Decimal = Math.Round((GetWESMBillSummaryAPMF.EndingBalance / TotalWESMBillSummaryPerID) * OffsettingShareOnDI, 2)
                Dim OffsettingShareOnDIMFV As Decimal = Math.Round((GetWESMBillSummaryAPMFV.EndingBalance / TotalWESMBillSummaryPerID) * OffsettingShareOnDI, 2)
                Dim CheckTotalOffsettingDIShare As Decimal = OffsettingShareOnDIMF + OffsettingShareOnDIMFV

                If CheckTotalOffsettingDIShare <> OffsettingShareOnDI Then
                    Dim AdjustOffsetting As Decimal = OffsettingShareOnDI - CheckTotalOffsettingDIShare
                    OffsettingShareOnDIMF += AdjustOffsetting
                End If

                Dim OffsettingShareOnMFWHTaxDI As Decimal = (OffsettingShareOnDIMF * GetAMParticipantsInfo.MarketFeesWHTax) * -1
                Dim OffsettingShareOnMFWHVATDI = (OffsettingShareOnDIMF * GetAMParticipantsInfo.MarketFeesWHVAT) * -1
                Dim APAllocationDIMF As New APAllocation
                Dim APAllocationDIMFV As New APAllocation

                If OffsettingShareOnMFWHTaxDI <> 0 Then
                    Dim APAllocationMFWHTaxDI As New APAllocation
                    APAllocationMFWHTaxDI = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMF, OffsettingShareOnMFWHTaxDI,
                                                                    EnumPaymentNewType.WithholdingTaxOnMF,
                                                                    EnumCollectionCategory.Offset)
                    APAllocationListPerBP.Add(APAllocationMFWHTaxDI)
                End If

                If OffsettingShareOnMFWHVATDI <> 0 Then
                    Dim APAllocationMFWHVATDI As New APAllocation
                    APAllocationMFWHVATDI = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMF, OffsettingShareOnMFWHVATDI,
                                                                    EnumPaymentNewType.WithholdingVatOnMF,
                                                                    EnumCollectionCategory.Offset)
                    APAllocationListPerBP.Add(APAllocationMFWHVATDI)
                End If

                If OffsettingShareOnDIMF <> 0 Then
                    APAllocationDIMF = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMF, OffsettingShareOnDIMF + ((OffsettingShareOnMFWHTaxDI + OffsettingShareOnMFWHVATDI) * -1),
                                                           EnumPaymentNewType.MarketFees,
                                                           EnumCollectionCategory.Offset)
                    APAllocationListPerBP.Add(APAllocationDIMF)
                End If

                If OffsettingShareOnDIMFV <> 0 Then
                    APAllocationDIMFV = Me.CreateAPAllocationItem(GetWESMBillSummaryAPMFV, OffsettingShareOnDIMFV,
                                                                  EnumPaymentNewType.VatOnMarketFees,
                                                                  EnumCollectionCategory.Offset)
                    APAllocationListPerBP.Add(APAllocationDIMFV)
                End If
            End If

        Next


        Dim TotalAmountAllocatedCashMFWithVAT = (From x In APAllocationListPerBP
                                                 Where (x.PaymentType = EnumPaymentNewType.MarketFees _
                                                 Or x.PaymentType = EnumPaymentNewType.VatOnMarketFees) _
                                                 And x.PaymentCategory = EnumCollectionCategory.Cash
                                                 Select x.AllocationAmount).Sum()

        Dim TotalAmountAllocatedCashMFWithVATDI = (From x In APAllocationListPerBP
                                                   Where (x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                                          Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF) _
                                                   And x.PaymentCategory = EnumCollectionCategory.Cash
                                                   Select x.AllocationAmount).Sum()

        Dim TotalAmountAllocatedOffsetMFWithVAT = (From x In APAllocationListPerBP
                                                   Where (x.PaymentType = EnumPaymentNewType.MarketFees _
                                                   Or x.PaymentType = EnumPaymentNewType.VatOnMarketFees) _
                                                   And x.PaymentCategory = EnumCollectionCategory.Offset
                                                   Select x.AllocationAmount).Sum()

        Dim TotalAmountAllocatedOffsetMFWithVATDI = (From x In APAllocationListPerBP
                                                     Where (x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                                            Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF) _
                                                     And x.PaymentCategory = EnumCollectionCategory.Offset
                                                     Select x.AllocationAmount).Sum()



        Dim GetAmountDiffCashMFWithVAT As Decimal = TotalARAmountCollectedPerBP.CashAmount - TotalAmountAllocatedCashMFWithVAT
        Dim GetAmountDiffCashMFWithVATDI As Decimal = TotalARAmountCollectedPerBP.CashAmountDI - TotalAmountAllocatedCashMFWithVATDI
        Dim GetAmountDiffOffsetMFWithVAT As Decimal = TotalARAmountCollectedPerBP.OffsetAmount - TotalAmountAllocatedOffsetMFWithVAT
        Dim GetAmountDiffOffsetMFWithVATDI As Decimal = TotalARAmountCollectedPerBP.OffsetAmountDI - TotalAmountAllocatedOffsetMFWithVATDI

        If GetAmountDiffCashMFWithVAT <> 0 And TotalARAmountCollectedPerBP.CashAmount <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where (x.PaymentType = EnumPaymentNewType.MarketFees _
                                                 Or x.PaymentType = EnumPaymentNewType.VatOnMarketFees) _
                                          And x.PaymentCategory = EnumCollectionCategory.Cash
                                          Select x).ToList()

            Me.AdjustComputedAllocation(_APAllocationListPerBP, GetAmountDiffCashMFWithVAT) 'Function that will adjust the Allocated amount per invoice

            Me.AddAdjustedAPAllocationList(_APAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        ElseIf GetAmountDiffCashMFWithVAT = 0 And TotalARAmountCollectedPerBP.CashAmount <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where (x.PaymentType = EnumPaymentNewType.MarketFees _
                                                 Or x.PaymentType = EnumPaymentNewType.VatOnMarketFees) _
                                          And x.PaymentCategory = EnumCollectionCategory.Cash
                                          Select x).ToList()

            Me.AddAdjustedAPAllocationList(_APAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        End If

        If GetAmountDiffCashMFWithVATDI <> 0 And TotalARAmountCollectedPerBP.CashAmountDI <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where (x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                                 Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF) _
                                          And x.PaymentCategory = EnumCollectionCategory.Cash
                                          Select x).ToList()

            Me.AdjustComputedAllocation(_APAllocationListPerBP, GetAmountDiffCashMFWithVATDI) 'Function that will adjust the Allocated amount per invoice

            Me.AddAdjustedAPAllocationList(_APAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        ElseIf GetAmountDiffCashMFWithVATDI = 0 And TotalARAmountCollectedPerBP.CashAmountDI <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where (x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                                 Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF) _
                                          And x.PaymentCategory = EnumCollectionCategory.Cash
                                          Select x).ToList()

            Me.AddAdjustedAPAllocationList(_APAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        End If

        If GetAmountDiffOffsetMFWithVAT <> 0 And TotalARAmountCollectedPerBP.OffsetAmount <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where (x.PaymentType = EnumPaymentNewType.MarketFees _
                                                 Or x.PaymentType = EnumPaymentNewType.VatOnMarketFees) _
                                          And x.PaymentCategory = EnumCollectionCategory.Offset
                                          Select x).ToList()
            Me.AdjustComputedAllocation(_APAllocationListPerBP, GetAmountDiffOffsetMFWithVAT) 'Function that will adjust the Allocated amount per invoice
            Me.AddAdjustedAPAllocationList(_APAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        ElseIf GetAmountDiffOffsetMFWithVAT = 0 And TotalARAmountCollectedPerBP.OffsetAmount <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where (x.PaymentType = EnumPaymentNewType.MarketFees _
                                                 Or x.PaymentType = EnumPaymentNewType.VatOnMarketFees) _
                                          And x.PaymentCategory = EnumCollectionCategory.Offset
                                          Select x).ToList()

            Me.AddAdjustedAPAllocationList(_APAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        End If


        If GetAmountDiffOffsetMFWithVATDI <> 0 And TotalARAmountCollectedPerBP.OffsetAmountDI <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where (x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                                 Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF) _
                                          And x.PaymentCategory = EnumCollectionCategory.Offset
                                          Select x).ToList()
            Me.AdjustComputedAllocation(_APAllocationListPerBP, GetAmountDiffOffsetMFWithVATDI) 'Function that will adjust the Allocated amount per invoice
            Me.AddAdjustedAPAllocationList(_APAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        ElseIf GetAmountDiffOffsetMFWithVATDI = 0 And TotalARAmountCollectedPerBP.OffsetAmountDI <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where (x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                                 Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF) _
                                          And x.PaymentCategory = EnumCollectionCategory.Offset
                                          Select x).ToList()
            Me.AddAdjustedAPAllocationList(_APAllocationListPerBP) 'Method that will add new Item for APAllocationList Property
        End If

        Me.UpdateWESMBillSummaryMFMFV(WESMBillSummaryListPerBP)

    End Sub

#End Region

#Region "Method Step 1 for AP Allocation on Energy"
    Private Sub APEnergyAllocationProcess(ByRef WESMBillSummaryListOnEnergy As List(Of WESMBillSummary),
                                          ByVal listARCollection As List(Of ARCollection),
                                          ByVal progress As IProgress(Of ProgressClass))
        Try
            Dim getDistinctWBatch As List(Of Long) = listARCollection.Select(Function(x) x.WESMBillBatchNo).OrderBy(Function(y) y).Distinct.ToList()
            Dim cnt As Integer = 0
            For Each wBatch In getDistinctWBatch
                newProgress = New ProgressClass
                newProgress.ProgressMsg = "Processing Payment Allocation in Energy with WESM Batch No " & cnt + 1 & "/" & getDistinctWBatch.Count & "..."
                progress.Report(newProgress)
                Dim itemListARColl As List(Of ARCollection) = listARCollection.Where(Function(x) x.WESMBillBatchNo = wBatch).ToList()
                Dim listWESMBillSummaryPerWBatch = WESMBillSummaryListOnEnergy.
                                                      Where(Function(x) x.WESMBillBatchNo = wBatch _
                                                                        And x.EndingBalance >= 0 _
                                                                        And x.BalanceType = EnumBalanceType.AP).ToList()
                Me.AllocatePaymentOnEnergy(listWESMBillSummaryPerWBatch, itemListARColl)
                cnt += 1
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "Method Step 2 for AP Allocation on Energy"
    Private Sub AllocatePaymentOnEnergy(ByRef listWESMBillSummaryPerWBatch As List(Of WESMBillSummary),
                                        ByVal listARCollection As List(Of ARCollection))

        Dim APAllocationListPerBPFinal As New List(Of APAllocation)
        Dim getDueDate As Date = listWESMBillSummaryPerWBatch.Select(Function(x) x.DueDate).Distinct.FirstOrDefault
        Dim getBPNo As Integer = listWESMBillSummaryPerWBatch.Select(Function(x) x.BillPeriod).Distinct.FirstOrDefault

        Dim getTotalEndingBalance As Decimal = listWESMBillSummaryPerWBatch.Select(Function(x) x.EndingBalance).Sum()

        Dim getBIRRulingInvoiceList As List(Of ARCollection) = listARCollection.Where(Function(x) x.InvoiceNumber.StartsWith("TS-") _
                                                                                      And Not x.InvoiceNumber.ToUpper Like "*-ADJ*") _
                                                                                      .OrderByDescending(Function(x) x.AllocationAmount) _
                                                                                      .OrderBy(Function(x) x.InvoiceNumber) _
                                                                                      .ThenBy(Function(x) x.CollectionType) _
                                                                                      .ToList

        Dim getNonBIRRulingInvoiceList As List(Of ARCollection) = listARCollection.Where(Function(x) (x.InvoiceNumber.StartsWith("TS-") And x.InvoiceNumber.ToUpper Like "*-ADJ*") _
                                                                                             Or Not x.InvoiceNumber.StartsWith("TS-")).ToList
        'Get the WTA Details Summary for current AR List
        Dim getWTDSummary As List(Of WESMTransDetailsSummary) = New List(Of WESMTransDetailsSummary)
        For Each itemAR In getBIRRulingInvoiceList
            Dim APAllocationListPerBP As New List(Of APAllocation)
            Dim ExcludeAPAllocationListPerBP As New List(Of String)
            getWTDSummary = Me._WESMTransDetailsSummaryList.Where(Function(x) x.BuyerTransNo = itemAR.InvoiceNumber).ToList
            For Each itemAP In listWESMBillSummaryPerWBatch
                Dim CashShareOnEnergy As Decimal = 0D
                If getWTDSummary.Count > 0 Then
                    Dim originalTotalAmountOfAP As Decimal = Math.Abs(getWTDSummary.Select(Function(x) x.OrigBalanceInEnergy).Sum)
                    Dim outstandingTotalAmountOfAP As Decimal = Math.Abs(getWTDSummary.Select(Function(x) x.OutstandingBalanceInEnergy).Sum)
                    Dim getWTDSummaryItemAP = getWTDSummary.Where(Function(x) x.BuyerTransNo = itemAR.InvoiceNumber _
                                                                              And x.SellerTransNo = itemAP.INVDMCMNo).FirstOrDefault
                    If Not getWTDSummaryItemAP Is Nothing Then
                        If itemAR.CollectionType = EnumCollectionType.Energy Then

                            Dim sumofTotalOBinEnergy As Decimal = Math.Abs(getWTDSummary.Select(Function(x) x.OutstandingBalanceInEnergy).Sum())
                            If Math.Abs(itemAR.AllocationAmount) = sumofTotalOBinEnergy Then
                                Dim outstandingBalanceOfAP As Decimal = Math.Abs(getWTDSummary.
                                                             Where(Function(x) x.BuyerTransNo = itemAR.InvoiceNumber _
                                                             And x.SellerTransNo = itemAP.INVDMCMNo).
                                                             Select(Function(y) y.OutstandingBalanceInEnergy).FirstOrDefault)
                                CashShareOnEnergy = outstandingBalanceOfAP
                            Else
                                CashShareOnEnergy = Me.ComputeAllocation(Math.Abs(getWTDSummaryItemAP.OrigBalanceInEnergy), originalTotalAmountOfAP, Math.Abs(itemAR.AllocationAmount))
                                Dim getAPAllocationItem As Decimal = APAllocationListPerBPFinal.Where(Function(x) x.InvoiceNumber.Equals(itemAP.INVDMCMNo) And x.PaymentType = EnumPaymentNewType.Energy).Select(Function(x) x.AllocationAmount).Sum()
                                If CashShareOnEnergy > Math.Abs(getWTDSummaryItemAP.OutstandingBalanceInEnergy) Then
                                    Dim amountChecker As Decimal = itemAP.EndingBalance - CashShareOnEnergy
                                    If amountChecker < 0 And Math.Abs(amountChecker) > AmountDiffValue Then
                                        Throw New Exception("Allocated share on energy is greater than the outstanding balance of InvoiceNo: " & itemAR.InvoiceNumber & "!" & vbNewLine & "Please contact the administrator.")
                                    Else
                                        Dim adjCashShareOnEnergy = Math.Abs(getWTDSummaryItemAP.OutstandingBalanceInEnergy) - CashShareOnEnergy
                                        If Math.Abs(adjCashShareOnEnergy) > AMModule.AmountDiffValue Then
                                            Throw New Exception("Adjusted Cash Share On Energy is greater than the amount that should be adjusted!" & vbNewLine _
                                                                & " BuyerInvoiceNo: " & itemAR.InvoiceNumber & ", SellerInvoiceNo: " & itemAP.INVDMCMNo & vbNewLine & "Please contact the administrator.")
                                        Else
                                            CashShareOnEnergy += adjCashShareOnEnergy
                                        End If
                                    End If
                                End If
                                'If itemAR.DueDate <= CDate("04/25/2022") Then
                                '    If (CashShareOnEnergy + getAPAllocationItem) > itemAP.EndingBalance Then
                                '        ExcludeAPAllocationListPerBP.Add(itemAP.INVDMCMNo)
                                '        Dim getdiff As Decimal = itemAP.EndingBalance - (CashShareOnEnergy + getAPAllocationItem)
                                '        CashShareOnEnergy += getdiff
                                '    End If
                                'End If
                                If (CashShareOnEnergy + getAPAllocationItem) > itemAP.EndingBalance Then
                                    ExcludeAPAllocationListPerBP.Add(itemAP.INVDMCMNo)
                                    Dim getdiff As Decimal = itemAP.EndingBalance - (CashShareOnEnergy + getAPAllocationItem)
                                    CashShareOnEnergy += getdiff
                                End If
                            End If
                        ElseIf itemAR.CollectionType = EnumCollectionType.DefaultInterestOnEnergy Then
                            CashShareOnEnergy = Me.ComputeAllocation(Math.Abs(getWTDSummaryItemAP.OrigBalanceInEnergy), originalTotalAmountOfAP, Math.Abs(itemAR.AllocationAmount))
                        End If
                    Else
                        CashShareOnEnergy = 0
                    End If
                Else
                    Throw New Exception("No available WTA Summary Details for InvoiceNo: " & itemAR.InvoiceNumber & "!" & vbNewLine & "Please contact the administrator.")
                End If
                Dim CreatedItem_Energy As New APAllocation
                Select Case itemAR.CollectionType
                    Case EnumCollectionType.Energy
                        CreatedItem_Energy = Me.CreateAPAllocationItem(itemAP, CashShareOnEnergy,
                                                            EnumPaymentNewType.Energy, itemAR.CollectionCategory)
                    Case EnumCollectionType.DefaultInterestOnEnergy
                        CreatedItem_Energy = Me.CreateAPAllocationItem(itemAP, CashShareOnEnergy,
                                                            EnumPaymentNewType.DefaultInterestOnEnergy, itemAR.CollectionCategory)
                End Select
                APAllocationListPerBP.Add(CreatedItem_Energy)
            Next
            Dim totalAPAllocationAmount As Decimal = APAllocationListPerBP.Select(Function(x) x.AllocationAmount).Sum()
            If Math.Abs(itemAR.AllocationAmount) <> totalAPAllocationAmount Then
                Dim getDiff As Decimal = Math.Abs(itemAR.AllocationAmount) - totalAPAllocationAmount
                If getDiff >= AMModule.AmountDiffValue Then
                    Throw New Exception("The amount difference between AR Tagged Amount and AP Allocated Amount is greater than the maximum value " & AMModule.AmountDiffValue.ToString("N2") & "!" & vbNewLine & "Please contact the administrator.")
                End If
                Me.AdjustComputedAllocationForWTA(APAllocationListPerBP, getWTDSummary, getDiff, ExcludeAPAllocationListPerBP)
                If boolAdjStatusFailed = True Then
                    Throw New Exception("Cannot adjust the allocated amount from AR Inv: " & itemAR.InvoiceNumber & "! Please contact the administrator.")
                End If
                'Me.AdjustComputedAllocation(APAllocationListPerBP, getDiff)
            End If

            For Each itemAP In APAllocationListPerBP
                Dim getWTDSummaryItem As WESMTransDetailsSummary = getWTDSummary.
                                                                   FirstOrDefault(Function(x) x.BuyerTransNo = itemAR.InvoiceNumber And x.SellerTransNo = itemAP.InvoiceNumber)
                If Not getWTDSummaryItem Is Nothing Then
                    Dim keyHis As String = itemAR.InvoiceNumber & "|" & itemAP.InvoiceNumber
                    With getWTDSummaryItem
                        Select Case itemAR.CollectionType
                            Case EnumCollectionType.Energy
                                If (.OutstandingBalanceInEnergy + itemAP.AllocationAmount) > 0 Then
                                    Throw New Exception("The allocated amount for " & itemAP.InvoiceNumber & " collected from " & itemAR.InvoiceNumber &
                                                         vbNewLine & " is greater than the outstanding balance of AP! Please contact the administrator.")
                                    '.OutstandingBalanceInEnergy = 0
                                Else
                                    .OutstandingBalanceInEnergy = .OutstandingBalanceInEnergy + itemAP.AllocationAmount
                                End If
                                If Not Me._WESMTransDetailsSummaryHistoryDic.ContainsKey(keyHis) Then
                                    Using WTDSummaryHistory As New WESMTransDetailsSummaryHistory
                                        With WTDSummaryHistory
                                            .BuyerTransNo = getWTDSummaryItem.BuyerTransNo
                                            .BuyerBillingID = getWTDSummaryItem.BuyerBillingID
                                            .SellerTransNo = getWTDSummaryItem.SellerTransNo
                                            .SellerBillingID = getWTDSummaryItem.SellerBillingID
                                            .DueDate = getWTDSummaryItem.DueDate
                                            .AllocationDate = itemAP.AllocationDate
                                            .AllocatedInEnergy = itemAP.AllocationAmount
                                        End With
                                        Me._WESMTransDetailsSummaryHistoryDic.Add(keyHis, WTDSummaryHistory)
                                    End Using
                                Else
                                    Me._WESMTransDetailsSummaryHistoryDic(keyHis).AllocatedInEnergy += itemAP.AllocationAmount
                                End If
                            Case EnumCollectionType.DefaultInterestOnEnergy
                                If Not Me._WESMTransDetailsSummaryHistoryDic.ContainsKey(keyHis) Then
                                    Using WTDSummaryHistory As New WESMTransDetailsSummaryHistory
                                        With WTDSummaryHistory
                                            .BuyerTransNo = getWTDSummaryItem.BuyerTransNo
                                            .BuyerBillingID = getWTDSummaryItem.BuyerBillingID
                                            .SellerTransNo = getWTDSummaryItem.SellerTransNo
                                            .SellerBillingID = getWTDSummaryItem.SellerBillingID
                                            .DueDate = getWTDSummaryItem.DueDate
                                            .AllocationDate = itemAP.AllocationDate
                                            .AllocatedInDefInt = itemAP.AllocationAmount
                                        End With
                                        Me._WESMTransDetailsSummaryHistoryDic.Add(keyHis, WTDSummaryHistory)
                                    End Using
                                Else
                                    Me._WESMTransDetailsSummaryHistoryDic(keyHis).AllocatedInDefInt += itemAP.AllocationAmount
                                End If
                        End Select
                        If .Status.Equals(EnumWESMTransDetailsSummaryStatus.CURRENT.ToString) Then
                            .Status = EnumWESMTransDetailsSummaryStatus.UPDATED.ToString
                        End If
                    End With
                    APAllocationListPerBPFinal.Add(itemAP)
                End If
            Next
            getWTDSummary = New List(Of WESMTransDetailsSummary)
        Next
        getWTDSummary = Nothing

        Dim getTotalCollectedInARAmountDefInt As Decimal = getNonBIRRulingInvoiceList.
                                                           Where(Function(y) y.CollectionType = EnumCollectionType.DefaultInterestOnEnergy).
                                                           Select(Function(x) x.AllocationAmount).Sum()

        Dim getTotalCollectedInARAmountEnergy As Decimal = getNonBIRRulingInvoiceList.
                                                           Where(Function(y) y.CollectionType = EnumCollectionType.Energy).
                                                           Select(Function(x) x.AllocationAmount).Sum()

        If getTotalCollectedInARAmountEnergy <> 0 Then
            Dim APAllocationListPerBP As New List(Of APAllocation)
            Dim getCollectionType As EnumCollectionType = getNonBIRRulingInvoiceList.Select(Function(x) x.CollectionType).First
            Dim getCollectionCategory As EnumCollectionCategory = getNonBIRRulingInvoiceList.Select(Function(x) x.CollectionCategory).First
            For Each itemAP In listWESMBillSummaryPerWBatch
                Dim CashShareOnEnergy As Decimal = Me.ComputeAllocation(itemAP.EndingBalance, getTotalEndingBalance, Math.Abs(getTotalCollectedInARAmountEnergy))
                Dim CreatedItem_Energy As New APAllocation

                CreatedItem_Energy = Me.CreateAPAllocationItem(itemAP, CashShareOnEnergy,
                                                            EnumPaymentNewType.Energy, getCollectionCategory)
                APAllocationListPerBP.Add(CreatedItem_Energy)
            Next
            Dim totalAPAllocationAmount As Decimal = APAllocationListPerBP.Select(Function(x) x.AllocationAmount).Sum()
            If Math.Abs(getTotalCollectedInARAmountEnergy) <> totalAPAllocationAmount Then
                Dim getDiff As Decimal = Math.Abs(getTotalCollectedInARAmountEnergy) - totalAPAllocationAmount
                If getDiff >= AMModule.AmountDiffValue Then
                    Throw New Exception("The amount difference between AR Tagged Amount and AP Allocated Amount is greater than the maximum value of PHP " & AMModule.AmountDiffValue.ToString("N2") & "!" & vbNewLine & "Please contact the administrator.")
                End If
                Me.AdjustComputedAllocation(APAllocationListPerBP, getDiff)
            End If
            Dim adjtotalAPAllocationAmount As Decimal = APAllocationListPerBP.Select(Function(x) x.AllocationAmount).Sum()
            For Each itemAP In APAllocationListPerBP
                APAllocationListPerBPFinal.Add(itemAP)
            Next
        End If

        If getTotalCollectedInARAmountDefInt <> 0 Then
            Dim APAllocationListPerBP As New List(Of APAllocation)
            Dim getCollectionType As EnumCollectionType = getNonBIRRulingInvoiceList.Select(Function(x) x.CollectionType).First
            Dim getCollectionCategory As EnumCollectionType = getNonBIRRulingInvoiceList.Select(Function(x) x.CollectionCategory).First

            For Each itemAP In listWESMBillSummaryPerWBatch
                Dim CashShareOnEnergy As Decimal = Me.ComputeAllocation(itemAP.EndingBalance, getTotalEndingBalance, Math.Abs(getTotalCollectedInARAmountDefInt))
                Dim CreatedItem_Energy As New APAllocation

                CreatedItem_Energy = Me.CreateAPAllocationItem(itemAP, CashShareOnEnergy,
                                                            EnumPaymentNewType.DefaultInterestOnEnergy, getCollectionCategory)
                APAllocationListPerBP.Add(CreatedItem_Energy)
            Next
            Dim totalAPAllocationAmount As Decimal = APAllocationListPerBP.Select(Function(x) x.AllocationAmount).Sum()
            If Math.Abs(getTotalCollectedInARAmountDefInt) <> totalAPAllocationAmount Then
                Dim getDiff As Decimal = Math.Abs(getTotalCollectedInARAmountDefInt) - totalAPAllocationAmount
                If getDiff >= AMModule.AmountDiffValue Then
                    Throw New Exception("The amount difference between AR Tagged Amount and AP Allocated Amount is greater than the maximum value PHP " & AMModule.AmountDiffValue.ToString("N2") & "!" & vbNewLine & "Please contact the administrator.")
                End If
                Me.AdjustComputedAllocation(APAllocationListPerBP, getDiff)
            End If
            For Each itemAP In APAllocationListPerBP
                APAllocationListPerBPFinal.Add(itemAP)
            Next
        End If

        Me.AddAdjustedAPAllocationList(APAllocationListPerBPFinal) 'Method that will add new Item for APAllocationList Property
        Me.UpdateWESMBillSummary(listWESMBillSummaryPerWBatch, EnumPaymentNewType.Energy)
    End Sub
#End Region

#Region "Method Step 1 for AP Allocation on Energy VAT"
    Private Sub APVATAllocationProcess(ByVal WESMBillSummaryListOnVAT As List(Of WESMBillSummary),
                                       ByVal listARCollection As List(Of ARCollection),
                                       ByVal progress As IProgress(Of ProgressClass))
        Dim getDistinctWBatch As List(Of Long) = listARCollection.Select(Function(x) x.WESMBillBatchNo).OrderBy(Function(y) y).Distinct.ToList()
        Dim cnt As Integer = 0
        For Each wBatch In getDistinctWBatch
            newProgress = New ProgressClass
            newProgress.ProgressMsg = "Processing Payment Allocation in VAT On Energy with WESM Batch No " & cnt + 1 & "/" & getDistinctWBatch.Count & "..."
            progress.Report(newProgress)
            Dim itemListARColl As List(Of ARCollection) = listARCollection.Where(Function(x) x.WESMBillBatchNo = wBatch).ToList()
            Dim listWESMBillSummaryPerWBatch = WESMBillSummaryListOnVAT.
                                                  Where(Function(x) x.WESMBillBatchNo = wBatch _
                                                                    And x.EndingBalance >= 0 _
                                                                    And x.BalanceType = EnumBalanceType.AP).ToList()
            Me.AllocatePaymentOnVAT(listWESMBillSummaryPerWBatch, itemListARColl)
            cnt += 1
        Next
    End Sub
#End Region

#Region "Method Step 2 for AP Allocation On Energy VAT"
    Private Sub AllocatePaymentOnVAT(ByRef listWESMBillSummaryPerWBatch As List(Of WESMBillSummary),
                                     ByVal listARCollection As List(Of ARCollection))

        Dim APAllocationListPerBPFinal As New List(Of APAllocation)
        Dim getDueDate As Date = listWESMBillSummaryPerWBatch.Select(Function(x) x.DueDate).Distinct.FirstOrDefault
        Dim getBPNo As Integer = listWESMBillSummaryPerWBatch.Select(Function(x) x.BillPeriod).Distinct.FirstOrDefault

        Dim getTotalEndingBalance As Decimal = listWESMBillSummaryPerWBatch.Select(Function(x) x.EndingBalance).Sum()
        Dim getBIRRulingInvoiceList As List(Of ARCollection) = listARCollection.Where(Function(x) x.InvoiceNumber.StartsWith("TS-") And Not x.InvoiceNumber.ToUpper Like "*-ADJ*").ToList
        Dim getNonBIRRulingInvoiceList As List(Of ARCollection) = listARCollection.Where(Function(x) (x.InvoiceNumber.StartsWith("TS-") And x.InvoiceNumber.ToUpper Like "*-ADJ*") Or Not x.InvoiceNumber.StartsWith("TS-")).ToList
        Dim cListWESMBillSummaryPerWBatch As List(Of WESMBillSummary) = listWESMBillSummaryPerWBatch

        Dim getWTDSummary As List(Of WESMTransDetailsSummary) = New List(Of WESMTransDetailsSummary)
        For Each itemAR In getBIRRulingInvoiceList
            Dim APAllocationListPerBP As New List(Of APAllocation)
            Dim ExcludeAPAllocationListPerBP As New List(Of String)
            getWTDSummary = Me._WESMTransDetailsSummaryList.Where(Function(x) x.BuyerTransNo = itemAR.InvoiceNumber).ToList
            For Each itemAP In cListWESMBillSummaryPerWBatch
                Dim CashShareOnVAT As Decimal = 0D
                If getWTDSummary.Count > 0 Then
                    Dim originalTotalAmountOfAP As Decimal = Math.Abs(getWTDSummary.Select(Function(x) x.OrigBalanceInVAT).Sum)
                    Dim getWTDSummaryItemAP = getWTDSummary.Where(Function(x) x.BuyerTransNo = itemAR.InvoiceNumber _
                                                                              And x.SellerTransNo = itemAP.INVDMCMNo).FirstOrDefault
                    If Not getWTDSummaryItemAP Is Nothing Then
                        If Math.Abs(itemAR.AllocationAmount) = Math.Abs(getWTDSummary.Select(Function(x) x.OutstandingBalanceInVAT).Sum()) Then
                            Dim outstandingBalanceOfAP As Decimal = Math.Abs(getWTDSummary.
                                                         Where(Function(x) x.BuyerTransNo = itemAR.InvoiceNumber _
                                                         And x.SellerTransNo = itemAP.INVDMCMNo).
                                                         Select(Function(y) y.OutstandingBalanceInVAT).FirstOrDefault)
                            CashShareOnVAT = outstandingBalanceOfAP
                        Else
                            CashShareOnVAT = Me.ComputeAllocation(Math.Abs(getWTDSummaryItemAP.OrigBalanceInVAT), originalTotalAmountOfAP, Math.Abs(itemAR.AllocationAmount))
                            If CashShareOnVAT > Math.Abs(getWTDSummaryItemAP.OutstandingBalanceInVAT) Then
                                Dim amountChecker As Decimal = itemAP.EndingBalance - CashShareOnVAT
                                If amountChecker < 0 And Math.Abs(amountChecker) > AmountDiffValue Then
                                    Throw New Exception("Allocated share on vat is greater than the outstanding balance of InvoiceNo: " & itemAR.InvoiceNumber & "!" & vbNewLine & "Please contact the administrator.")
                                Else
                                    Dim adjCashShareOnVat = Math.Abs(getWTDSummaryItemAP.OutstandingBalanceInVAT) - CashShareOnVAT

                                    If Math.Abs(adjCashShareOnVat) > AMModule.AmountDiffValue Then
                                        Throw New Exception("Adjusted Cash Share On VAT is greater than the amount that should be adjusted!" & vbNewLine _
                                                                & " BuyerInvoiceNo: " & itemAR.InvoiceNumber & ", SellerInvoiceNo: " & itemAP.INVDMCMNo & vbNewLine & "Please contact the administrator.")
                                    Else
                                        CashShareOnVAT += adjCashShareOnVat
                                    End If
                                End If
                            End If
                        End If
                    Else
                        CashShareOnVAT = 0
                    End If
                Else
                    Throw New Exception("No available WTA Summary Details for InvoiceNo: " & itemAR.InvoiceNumber & "!" & vbNewLine & "Please contact the administrator.")
                End If

                Dim CreatedItem_Energy As New APAllocation
                CreatedItem_Energy = Me.CreateAPAllocationItem(itemAP, CashShareOnVAT, EnumPaymentNewType.VatOnEnergy, itemAR.CollectionCategory)
                APAllocationListPerBP.Add(CreatedItem_Energy)
            Next

            Dim totalAPAllocationAmount As Decimal = APAllocationListPerBP.Select(Function(x) x.AllocationAmount).Sum()
            If Math.Abs(itemAR.AllocationAmount) <> totalAPAllocationAmount Then
                Dim getDiff As Decimal = Math.Abs(itemAR.AllocationAmount) - totalAPAllocationAmount
                If getDiff >= AMModule.AmountDiffValue Then
                    Throw New Exception("The amount difference between AR Tagged Amount and AP Allocated Amount is greater than the maximum value " & AMModule.AmountDiffValue.ToString("N2") & "!" & vbNewLine & "Please contact the administrator.")
                End If
                Me.AdjustComputedAllocationForWTA(APAllocationListPerBP, getWTDSummary, getDiff, ExcludeAPAllocationListPerBP)
                If boolAdjStatusFailed = True Then
                    Throw New Exception("Cannot adjust the allocated amount from AR Inv: " & itemAR.InvoiceNumber & "! Please contact the administrator.")
                End If
                'Me.AdjustComputedAllocation(APAllocationListPerBP, getDiff)
            End If

            For Each itemAP In APAllocationListPerBP
                Dim getWTDSummaryItem As WESMTransDetailsSummary = getWTDSummary.
                                                                   Where(Function(x) x.BuyerTransNo = itemAR.InvoiceNumber And x.SellerTransNo = itemAP.InvoiceNumber).FirstOrDefault
                If Not getWTDSummaryItem Is Nothing Then
                    Dim keyHis As String = itemAR.InvoiceNumber & "|" & itemAP.InvoiceNumber
                    With getWTDSummaryItem
                        If (.OutstandingBalanceInVAT + itemAP.AllocationAmount) > 0 Then
                            Throw New Exception("The allocated amount for " & itemAP.InvoiceNumber & " collected from " & itemAR.InvoiceNumber & "!" &
                                                         vbNewLine & " is greater than the outstanding balance of AP! Please contact the administrator.")
                            .OutstandingBalanceInVAT = 0
                        Else
                            .OutstandingBalanceInVAT = .OutstandingBalanceInVAT + itemAP.AllocationAmount
                        End If
                        If Not Me._WESMTransDetailsSummaryHistoryDic.ContainsKey(keyHis) Then
                            Using WTDSummaryHistory As New WESMTransDetailsSummaryHistory
                                With WTDSummaryHistory
                                    .BuyerTransNo = getWTDSummaryItem.BuyerTransNo
                                    .BuyerBillingID = getWTDSummaryItem.BuyerBillingID
                                    .SellerTransNo = getWTDSummaryItem.SellerTransNo
                                    .SellerBillingID = getWTDSummaryItem.SellerBillingID
                                    .DueDate = getWTDSummaryItem.DueDate
                                    .AllocationDate = itemAP.AllocationDate
                                    .AllocatedInVAT = itemAP.AllocationAmount
                                    Me._WESMTransDetailsSummaryHistoryDic.Add(keyHis, WTDSummaryHistory)
                                End With
                            End Using
                        Else
                            Me._WESMTransDetailsSummaryHistoryDic(keyHis).AllocatedInVAT += itemAP.AllocationAmount
                        End If
                        If .Status.Equals(EnumWESMTransDetailsSummaryStatus.CURRENT.ToString) Then
                            .Status = EnumWESMTransDetailsSummaryStatus.UPDATED.ToString
                        End If
                    End With
                    APAllocationListPerBPFinal.Add(itemAP)
                End If
            Next
        Next

        Dim getTotalCollectedInARAmount As Decimal = getNonBIRRulingInvoiceList.Select(Function(x) x.AllocationAmount).Sum()
        If getTotalCollectedInARAmount <> 0 Then
            Dim APAllocationListPerBP As New List(Of APAllocation)
            Dim getCollectionCategory As EnumCollectionType = getNonBIRRulingInvoiceList.Select(Function(x) x.CollectionCategory).First
            Dim cListWESMBillSummaryPerWBatchNonBIR_EV As List(Of WESMBillSummary) = listWESMBillSummaryPerWBatch

            For Each itemAP In cListWESMBillSummaryPerWBatchNonBIR_EV
                Dim CashShareOnVAT As Decimal = 0D
                CashShareOnVAT = Me.ComputeAllocation(itemAP.EndingBalance, getTotalEndingBalance, Math.Abs(getTotalCollectedInARAmount))

                Dim CreatedItem_VAT As New APAllocation
                CreatedItem_VAT = Me.CreateAPAllocationItem(itemAP, CashShareOnVAT, EnumPaymentNewType.VatOnEnergy, getCollectionCategory)
                APAllocationListPerBP.Add(CreatedItem_VAT)
            Next

            Dim totalAPAllocationAmount As Decimal = APAllocationListPerBP.Select(Function(x) x.AllocationAmount).Sum()
            If Math.Abs(getTotalCollectedInARAmount) <> totalAPAllocationAmount Then
                Dim getDiff As Decimal = Math.Abs(getTotalCollectedInARAmount) - totalAPAllocationAmount
                If getDiff >= AMModule.AmountDiffValue Then
                    Throw New Exception("The amount difference between AR Tagged Amount and AP Allocated Amount is greater than the maximum value " & AMModule.AmountDiffValue.ToString("N2") & "!" & vbNewLine & "Please contact the administrator.")
                End If
                Me.AdjustComputedAllocation(APAllocationListPerBP, getDiff)
            End If

            For Each itemAP In APAllocationListPerBP
                APAllocationListPerBPFinal.Add(itemAP)
            Next
        End If

        Me.AddAdjustedAPAllocationList(APAllocationListPerBPFinal) 'Method that will add new Item for APAllocationList Property
        Me.UpdateWESMBillSummary(listWESMBillSummaryPerWBatch, EnumPaymentNewType.VatOnEnergy)

    End Sub
#End Region

#Region "Methods and Functions that used by Energy And VAT"
    Private Function CreateAPAllocationItem(ByRef WESMBillSummaryItem As WESMBillSummary,
                                            ByVal AmountShare As Decimal,
                                            ByVal PaymentNewType As EnumPaymentNewType,
                                            ByVal Category As EnumCollectionCategory,
                                            Optional ByVal ForceToCreateDMCM As Boolean = False) As APAllocation
        Dim _APAllocation As New APAllocation
        Dim _DMCM As New DebitCreditMemo
        Dim _DMCMSummary As New DebitCreditMemoSummaryNew
        Select Case PaymentNewType
            Case EnumPaymentNewType.MarketFees, EnumPaymentNewType.VatOnMarketFees, EnumPaymentNewType.WithholdingTaxOnMF, EnumPaymentNewType.WithholdingVatOnMF
                With _APAllocation
                    .WESMBillBatchNo = WESMBillSummaryItem.WESMBillBatchNo
                    .WESMBillSummaryNo = WESMBillSummaryItem.WESMBillSummaryNo
                    .BillingPeriod = WESMBillSummaryItem.BillPeriod
                    .IDNumber = WESMBillSummaryItem.IDNumber.IDNumber
                    .ParticipantID = WESMBillSummaryItem.IDNumber.ParticipantID
                    .InvoiceNumber = WESMBillSummaryItem.INVDMCMNo
                    .DueDate = WESMBillSummaryItem.DueDate
                    .NewDueDate = WESMBillSummaryItem.OrigNewDueDate
                    .EndingBalance = WESMBillSummaryItem.EndingBalance
                    .EnergyWithHold = WESMBillSummaryItem.EnergyWithhold
                    .AllocationDate = APAllocationDate
                    .PaymentType = PaymentNewType
                    .PaymentCategory = Category
                    .AllocationAmount = AmountShare
                    .NewEndingBalance = WESMBillSummaryItem.EndingBalance - AmountShare
                    .ChargeType = WESMBillSummaryItem.ChargeType
                    .OffsettingSequence = OffsettingSequence
                    .BillingRemarks = WESMBillSummaryItem.BillingRemarks
                End With
            Case EnumPaymentNewType.Energy
                With _APAllocation
                    .WESMBillBatchNo = WESMBillSummaryItem.WESMBillBatchNo
                    .WESMBillSummaryNo = WESMBillSummaryItem.WESMBillSummaryNo
                    .BillingPeriod = WESMBillSummaryItem.BillPeriod
                    .IDNumber = WESMBillSummaryItem.IDNumber.IDNumber
                    .ParticipantID = WESMBillSummaryItem.IDNumber.ParticipantID
                    .InvoiceNumber = WESMBillSummaryItem.INVDMCMNo
                    .DueDate = WESMBillSummaryItem.DueDate
                    .NewDueDate = WESMBillSummaryItem.OrigNewDueDate
                    .EndingBalance = WESMBillSummaryItem.EndingBalance
                    .EnergyWithHold = WESMBillSummaryItem.EnergyWithhold
                    .AllocationDate = APAllocationDate
                    .AllocationAmount = AmountShare
                    .PaymentType = PaymentNewType
                    .PaymentCategory = Category
                    If AmountShare > WESMBillSummaryItem.EndingBalance Then
                        .AllocationAmount = WESMBillSummaryItem.EndingBalance
                        .NewEndingBalance = WESMBillSummaryItem.EndingBalance - WESMBillSummaryItem.EndingBalance
                    Else
                        .AllocationAmount = AmountShare
                        .NewEndingBalance = WESMBillSummaryItem.EndingBalance - AmountShare
                    End If
                    .ChargeType = WESMBillSummaryItem.ChargeType
                    .OffsettingSequence = OffsettingSequence
                    If Category = EnumCollectionCategory.Offset Then
                        If AmountShare > 0 Or ForceToCreateDMCM = True Then
                            _DMCM = Me.PaymentProformaEntries.CreateDMCM_AP(_APAllocation, _APAllocation.AllocationDate, DailyInterestRate, Me.AMParticipantsList)
                            Me.ListofDMCMAP.Add(_DMCM)
                            .GeneratedDMCM = _DMCM.DMCMNumber
                        End If
                    End If
                    .BillingRemarks = WESMBillSummaryItem.BillingRemarks
                End With
            Case EnumPaymentNewType.WithholdingTaxOnEnergy
                With _APAllocation
                    .WESMBillBatchNo = WESMBillSummaryItem.WESMBillBatchNo
                    .WESMBillSummaryNo = WESMBillSummaryItem.WESMBillSummaryNo
                    .BillingPeriod = WESMBillSummaryItem.BillPeriod
                    .IDNumber = WESMBillSummaryItem.IDNumber.IDNumber
                    .ParticipantID = WESMBillSummaryItem.IDNumber.ParticipantID
                    .InvoiceNumber = WESMBillSummaryItem.INVDMCMNo
                    .DueDate = WESMBillSummaryItem.DueDate
                    .NewDueDate = WESMBillSummaryItem.OrigNewDueDate
                    .EndingBalance = WESMBillSummaryItem.EndingBalance
                    .EnergyWithHold = WESMBillSummaryItem.EnergyWithhold
                    .AllocationDate = APAllocationDate
                    .AllocationAmount = AmountShare
                    .PaymentType = PaymentNewType
                    .PaymentCategory = Category
                    .NewEndingBalance = WESMBillSummaryItem.EndingBalance
                    .ChargeType = WESMBillSummaryItem.ChargeType
                    .OffsettingSequence = OffsettingSequence
                    .BillingRemarks = WESMBillSummaryItem.BillingRemarks
                End With
            Case EnumPaymentNewType.VatOnEnergy
                With _APAllocation
                    .WESMBillBatchNo = WESMBillSummaryItem.WESMBillBatchNo
                    .WESMBillSummaryNo = WESMBillSummaryItem.WESMBillSummaryNo
                    .BillingPeriod = WESMBillSummaryItem.BillPeriod
                    .IDNumber = WESMBillSummaryItem.IDNumber.IDNumber
                    .ParticipantID = WESMBillSummaryItem.IDNumber.ParticipantID
                    .InvoiceNumber = WESMBillSummaryItem.INVDMCMNo
                    .DueDate = WESMBillSummaryItem.DueDate
                    .NewDueDate = WESMBillSummaryItem.OrigNewDueDate
                    .EndingBalance = WESMBillSummaryItem.EndingBalance
                    .EnergyWithHold = WESMBillSummaryItem.EnergyWithhold
                    .AllocationDate = APAllocationDate
                    .PaymentType = PaymentNewType
                    .PaymentCategory = Category
                    If AmountShare > WESMBillSummaryItem.EndingBalance Then
                        .AllocationAmount = WESMBillSummaryItem.EndingBalance
                        .NewEndingBalance = WESMBillSummaryItem.EndingBalance - WESMBillSummaryItem.EndingBalance
                    Else
                        .AllocationAmount = AmountShare
                        .NewEndingBalance = WESMBillSummaryItem.EndingBalance - AmountShare
                    End If
                    .ChargeType = WESMBillSummaryItem.ChargeType
                    .OffsettingSequence = OffsettingSequence
                    If Category = EnumCollectionCategory.Offset Then
                        If AmountShare > 0 Then
                            _DMCM = Me.PaymentProformaEntries.CreateDMCM_AP(_APAllocation, _APAllocation.AllocationDate, DailyInterestRate, Me.AMParticipantsList)
                            Dim _DMCMSummaryNew As New DebitCreditMemoSummaryNew
                            Me.ListofDMCMAP.Add(_DMCM)
                            .GeneratedDMCM = _DMCM.DMCMNumber
                        End If
                    End If
                    .BillingRemarks = WESMBillSummaryItem.BillingRemarks
                End With
            Case EnumPaymentNewType.DefaultInterestOnEnergy
                With _APAllocation
                    .WESMBillBatchNo = WESMBillSummaryItem.WESMBillBatchNo
                    .WESMBillSummaryNo = WESMBillSummaryItem.WESMBillSummaryNo
                    .BillingPeriod = WESMBillSummaryItem.BillPeriod
                    .IDNumber = WESMBillSummaryItem.IDNumber.IDNumber
                    .ParticipantID = WESMBillSummaryItem.IDNumber.ParticipantID
                    .InvoiceNumber = WESMBillSummaryItem.INVDMCMNo
                    .DueDate = WESMBillSummaryItem.DueDate
                    .NewDueDate = WESMBillSummaryItem.OrigNewDueDate
                    .EndingBalance = WESMBillSummaryItem.EndingBalance
                    .EnergyWithHold = WESMBillSummaryItem.EnergyWithhold
                    .AllocationDate = APAllocationDate
                    .AllocationAmount = AmountShare
                    .PaymentType = PaymentNewType
                    .PaymentCategory = Category
                    .NewEndingBalance = WESMBillSummaryItem.EndingBalance
                    .ChargeType = WESMBillSummaryItem.ChargeType
                    .OffsettingSequence = OffsettingSequence
                    If AmountShare > 0 Then
                        _DMCM = Me.PaymentProformaEntries.CreateDMCM_AP(_APAllocation, _APAllocation.AllocationDate, DailyInterestRate, Me.AMParticipantsList)
                        Dim _DMCMSummaryNew As New DebitCreditMemoSummaryNew
                        Me.ListofDMCMAP.Add(_DMCM)
                        .GeneratedDMCM = _DMCM.DMCMNumber
                    End If
                    .BillingRemarks = WESMBillSummaryItem.BillingRemarks
                End With
        End Select

        Return _APAllocation

    End Function

    Private Function ComputeAllocation(ByVal InvOutstandingBalance As Decimal, ByVal CurrentBP_TotalBalance As Decimal, ByVal TotalPaymentForBP As Decimal) As Decimal
        Dim returnAmntAlloc As Decimal
        If InvOutstandingBalance <> 0 Then
            returnAmntAlloc = CDec(InvOutstandingBalance / CurrentBP_TotalBalance) * TotalPaymentForBP
        Else
            returnAmntAlloc = 0
        End If
        returnAmntAlloc = Math.Round(returnAmntAlloc, 2, MidpointRounding.AwayFromZero)
        Return returnAmntAlloc
    End Function

    Private Sub AdjustComputedAllocationForWTA(ByRef UpdateAPAllocationList As List(Of APAllocation),
                                               ByVal WTDSummaryList As List(Of WESMTransDetailsSummary),
                                               ByVal AmountDiff As Decimal,
                                               ByVal ExcludedAPAllocation As List(Of String))

        Dim UpdatedAPAllocationListPerBP As New List(Of APAllocation)
        Dim totalAllocDiffAmount As Decimal = 0D
        Dim countofListOfInvoiceOB As Integer = 0
        Do While totalAllocDiffAmount <> AmountDiff
            Dim GetListOfInvoiceWithEOAmount = (From x In UpdateAPAllocationList Select x Where Not ExcludedAPAllocation.Contains(x.InvoiceNumber) And x.NewEndingBalance > 0 Order By x.AllocationAmount Descending, x.EndingBalance Descending).ToList
            If GetListOfInvoiceWithEOAmount.Count = 0 And AmountDiff < 0 Then
                GetListOfInvoiceWithEOAmount = (From x In UpdateAPAllocationList Select x Where Not ExcludedAPAllocation.Contains(x.InvoiceNumber) And x.EndingBalance > 0 Order By x.AllocationAmount Descending, x.EndingBalance Descending).ToList
            ElseIf GetListOfInvoiceWithEOAmount.Count = 0 Then
                Exit Do
            End If
            If countofListOfInvoiceOB = GetListOfInvoiceWithEOAmount.Count() And totalAllocDiffAmount = 0 Then
                boolAdjStatusFailed = True
                Exit Do
            End If
            For Each item In GetListOfInvoiceWithEOAmount
                countofListOfInvoiceOB += 1
                Dim computedShareDiffAmount As Decimal = If(AmountDiff > 0D, 0.01D, -0.01D)
                If totalAllocDiffAmount <> AmountDiff Then
                    Dim getWTDSummary = WTDSummaryList.FirstOrDefault(Function(x) x.SellerTransNo = item.InvoiceNumber)
                    Dim getOutstandingWTD As Decimal = 0
                    If item.PaymentType = EnumPaymentNewType.Energy Then
                        getOutstandingWTD = Math.Abs(getWTDSummary.OutstandingBalanceInEnergy)
                        Dim negativeAmountChecker As Decimal = getOutstandingWTD - (item.AllocationAmount + computedShareDiffAmount)
                        If Not negativeAmountChecker < 0 Then
                            Dim UpdateAPAllocation As APAllocation = (From x In UpdateAPAllocationList Where x.WESMBillSummaryNo = item.WESMBillSummaryNo Select x).FirstOrDefault
                            With UpdateAPAllocation
                                Dim DMCMItem = (From x In Me.ListofDMCMAP Where x.DMCMNumber = .GeneratedDMCM Select x).FirstOrDefault
                                If Not DMCMItem Is Nothing Then
                                    DMCMItem.VATExempt += computedShareDiffAmount
                                    DMCMItem.TotalAmountDue += computedShareDiffAmount
                                    For Each ItemDetail In DMCMItem.DMCMDetails
                                        If ItemDetail.Credit = 0 Then
                                            ItemDetail.Debit += computedShareDiffAmount
                                        Else
                                            ItemDetail.Credit += computedShareDiffAmount
                                        End If
                                    Next
                                    .AllocationAmount += computedShareDiffAmount
                                Else
                                    If .AllocationAmount = 0 Then
                                        Dim _DMCM As New DebitCreditMemo
                                        Dim MustBeAllocationAmount As Decimal = .AllocationAmount + computedShareDiffAmount
                                        UpdateAPAllocation.AllocationAmount = MustBeAllocationAmount
                                        _DMCM = Me.PaymentProformaEntries.CreateDMCM_AP(UpdateAPAllocation, .AllocationDate, DailyInterestRate, Me.AMParticipantsList)
                                        If Not _DMCM Is Nothing And _DMCM.DMCMNumber > 0 Then
                                            Me.ListofDMCMAP.Add(_DMCM)
                                            .GeneratedDMCM = _DMCM.DMCMNumber
                                        End If
                                    Else
                                        .AllocationAmount += computedShareDiffAmount
                                    End If
                                End If
                                .NewEndingBalance = .EndingBalance - .AllocationAmount
                            End With
                            totalAllocDiffAmount += computedShareDiffAmount
                        End If
                    ElseIf item.PaymentType = EnumPaymentNewType.VatOnEnergy Then
                        getOutstandingWTD = Math.Abs(getWTDSummary.OutstandingBalanceInVAT)
                        Dim negativeAmountChecker As Decimal = getOutstandingWTD - (item.AllocationAmount + computedShareDiffAmount)
                        If Not negativeAmountChecker < 0 Then
                            Dim UpdateAPAllocation As APAllocation = (From x In UpdateAPAllocationList Where x.WESMBillSummaryNo = item.WESMBillSummaryNo Select x).FirstOrDefault
                            With UpdateAPAllocation
                                Dim DMCMItem = (From x In Me.ListofDMCMAP Where x.DMCMNumber = .GeneratedDMCM Select x).FirstOrDefault
                                If Not DMCMItem Is Nothing Then
                                    DMCMItem.VATExempt += computedShareDiffAmount
                                    DMCMItem.TotalAmountDue += computedShareDiffAmount
                                    For Each ItemDetail In DMCMItem.DMCMDetails
                                        If ItemDetail.Credit = 0 Then
                                            ItemDetail.Debit += computedShareDiffAmount
                                        Else
                                            ItemDetail.Credit += computedShareDiffAmount
                                        End If
                                    Next
                                    .AllocationAmount += computedShareDiffAmount
                                Else
                                    If .AllocationAmount = 0 Then
                                        Dim _DMCM As New DebitCreditMemo
                                        Dim MustBeAllocationAmount As Decimal = .AllocationAmount + computedShareDiffAmount
                                        UpdateAPAllocation.AllocationAmount = MustBeAllocationAmount
                                        _DMCM = Me.PaymentProformaEntries.CreateDMCM_AP(UpdateAPAllocation, .AllocationDate, DailyInterestRate, Me.AMParticipantsList)
                                        If Not _DMCM Is Nothing And _DMCM.DMCMNumber > 0 Then
                                            Me.ListofDMCMAP.Add(_DMCM)
                                            .GeneratedDMCM = _DMCM.DMCMNumber
                                        End If
                                    Else
                                        .AllocationAmount += computedShareDiffAmount
                                    End If
                                End If
                                .NewEndingBalance = .EndingBalance - .AllocationAmount
                            End With
                            totalAllocDiffAmount += computedShareDiffAmount
                        End If
                    ElseIf item.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy Then
                        Dim UpdateAPAllocation As APAllocation = (From x In UpdateAPAllocationList Where x.WESMBillSummaryNo = item.WESMBillSummaryNo Select x).FirstOrDefault
                        With UpdateAPAllocation
                            Dim DMCMItem = (From x In Me.ListofDMCMAP Where x.DMCMNumber = .GeneratedDMCM Select x).FirstOrDefault
                            If Not DMCMItem Is Nothing Then
                                DMCMItem.VATExempt += computedShareDiffAmount
                                DMCMItem.TotalAmountDue += computedShareDiffAmount
                                For Each ItemDetail In DMCMItem.DMCMDetails
                                    If ItemDetail.Credit = 0 Then
                                        ItemDetail.Debit += computedShareDiffAmount
                                    Else
                                        ItemDetail.Credit += computedShareDiffAmount
                                    End If
                                Next
                                .AllocationAmount += computedShareDiffAmount
                            Else
                                If .AllocationAmount = 0 Then
                                    Dim _DMCM As New DebitCreditMemo
                                    Dim MustBeAllocationAmount As Decimal = .AllocationAmount + computedShareDiffAmount
                                    UpdateAPAllocation.AllocationAmount = MustBeAllocationAmount
                                    _DMCM = Me.PaymentProformaEntries.CreateDMCM_AP(UpdateAPAllocation, .AllocationDate, DailyInterestRate, Me.AMParticipantsList)
                                    If Not _DMCM Is Nothing And _DMCM.DMCMNumber > 0 Then
                                        Me.ListofDMCMAP.Add(_DMCM)
                                        .GeneratedDMCM = _DMCM.DMCMNumber
                                    End If
                                Else
                                    .AllocationAmount += computedShareDiffAmount
                                End If
                            End If
                            totalAllocDiffAmount += computedShareDiffAmount
                        End With
                    End If
                Else
                    Exit For
                End If
            Next
        Loop
    End Sub

    Private Sub AdjustComputedAllocation(ByRef UpdateAPAllocationList As List(Of APAllocation),
                                         ByVal AmountDiff As Decimal)
        Dim UpdatedAPAllocationListPerBP As New List(Of APAllocation)
        Dim totalAllocDiffAmount As Decimal = 0D
        Do While totalAllocDiffAmount <> AmountDiff
            Dim GetListOfInvoiceWithEOAmount = (From x In UpdateAPAllocationList Select x Where x.NewEndingBalance > 0 Order By x.AllocationAmount Descending, x.EndingBalance Descending).ToList
            If GetListOfInvoiceWithEOAmount.Count = 0 And AmountDiff < 0 Then
                GetListOfInvoiceWithEOAmount = (From x In UpdateAPAllocationList Select x Where x.EndingBalance > 0 Order By x.AllocationAmount Descending, x.EndingBalance Descending).ToList
            ElseIf GetListOfInvoiceWithEOAmount.Count = 0 Then
                Exit Do
            End If
            For Each item In GetListOfInvoiceWithEOAmount
                Dim computedShareDiffAmount As Decimal = If(AmountDiff > 0D, 0.01D, -0.01D)
                If totalAllocDiffAmount <> AmountDiff Then
                    Dim UpdateAPAllocation As APAllocation = (From x In UpdateAPAllocationList Where x.WESMBillSummaryNo = item.WESMBillSummaryNo Select x).FirstOrDefault
                    With UpdateAPAllocation
                        If Not .PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy Then
                            Dim negativeAmountChecker As Decimal = item.EndingBalance - (item.AllocationAmount + computedShareDiffAmount)
                            If Not negativeAmountChecker < 0 Then
                                Dim DMCMItem = (From x In Me.ListofDMCMAP Where x.DMCMNumber = .GeneratedDMCM Select x).FirstOrDefault
                                If Not DMCMItem Is Nothing Then
                                    DMCMItem.VATExempt += computedShareDiffAmount
                                    DMCMItem.TotalAmountDue += computedShareDiffAmount
                                    For Each ItemDetail In DMCMItem.DMCMDetails
                                        If ItemDetail.Credit = 0 Then
                                            ItemDetail.Debit += computedShareDiffAmount
                                        Else
                                            ItemDetail.Credit += computedShareDiffAmount
                                        End If
                                    Next
                                    .AllocationAmount += computedShareDiffAmount
                                Else
                                    If .AllocationAmount = 0 Then
                                        Dim _DMCM As New DebitCreditMemo
                                        Dim MustBeAllocationAmount As Decimal = .AllocationAmount + computedShareDiffAmount
                                        UpdateAPAllocation.AllocationAmount = MustBeAllocationAmount
                                        _DMCM = Me.PaymentProformaEntries.CreateDMCM_AP(UpdateAPAllocation, .AllocationDate, DailyInterestRate, Me.AMParticipantsList)
                                        If Not _DMCM Is Nothing And _DMCM.DMCMNumber > 0 Then
                                            Me.ListofDMCMAP.Add(_DMCM)
                                            .GeneratedDMCM = _DMCM.DMCMNumber
                                        End If
                                    Else
                                        .AllocationAmount += computedShareDiffAmount
                                    End If
                                End If
                                .NewEndingBalance = .EndingBalance - .AllocationAmount
                            End If
                        Else
                            Dim DMCMItem = (From x In Me.ListofDMCMAP Where x.DMCMNumber = .GeneratedDMCM Select x).FirstOrDefault
                            If Not DMCMItem Is Nothing Then
                                DMCMItem.VATExempt += computedShareDiffAmount
                                DMCMItem.TotalAmountDue += computedShareDiffAmount
                                For Each ItemDetail In DMCMItem.DMCMDetails
                                    If ItemDetail.Credit = 0 Then
                                        ItemDetail.Debit += computedShareDiffAmount
                                    Else
                                        ItemDetail.Credit += computedShareDiffAmount
                                    End If
                                Next
                                .AllocationAmount += computedShareDiffAmount
                            Else
                                If .AllocationAmount = 0 Then
                                    Dim _DMCM As New DebitCreditMemo
                                    Dim MustBeAllocationAmount As Decimal = .AllocationAmount + computedShareDiffAmount
                                    UpdateAPAllocation.AllocationAmount = MustBeAllocationAmount
                                    _DMCM = Me.PaymentProformaEntries.CreateDMCM_AP(UpdateAPAllocation, .AllocationDate, DailyInterestRate, Me.AMParticipantsList)
                                    If Not _DMCM Is Nothing And _DMCM.DMCMNumber > 0 Then
                                        Me.ListofDMCMAP.Add(_DMCM)
                                        .GeneratedDMCM = _DMCM.DMCMNumber
                                    End If
                                Else
                                    .AllocationAmount += computedShareDiffAmount
                                End If
                            End If
                        End If
                    End With
                Else
                    Exit For
                End If
                totalAllocDiffAmount += computedShareDiffAmount
            Next
        Loop
    End Sub

    Private Sub AddAdjustedAPAllocationList(ByRef AdjustedAPAllocationList As List(Of APAllocation))
        For Each AdjustedAPAllocation In AdjustedAPAllocationList
            Select Case AdjustedAPAllocation.ChargeType
                Case EnumChargeType.E
                    Dim UpdateDMCMOfEnergyWithDI = (From x In Me._EnergyAPAllocationList
                                                    Where x.WESMBillSummaryNo = AdjustedAPAllocation.WESMBillSummaryNo
                                                    Select x).FirstOrDefault

                    If Not UpdateDMCMOfEnergyWithDI Is Nothing Then
                        Dim UpdateDMCM = (From x In Me.ListofDMCMAP
                                          Where x.DMCMNumber = UpdateDMCMOfEnergyWithDI.GeneratedDMCM
                                          Select x).FirstOrDefault

                        If Not UpdateDMCM Is Nothing Then
                            UpdateDMCM.VATExempt += AdjustedAPAllocation.AllocationAmount
                            UpdateDMCM.TotalAmountDue += AdjustedAPAllocation.AllocationAmount
                            For Each DMCMDetail In UpdateDMCM.DMCMDetails
                                Select Case DMCMDetail.AccountCode
                                    Case AMModule.DebitCode
                                        DMCMDetail.Debit += AdjustedAPAllocation.AllocationAmount
                                    Case AMModule.ClearingAccountCode
                                        DMCMDetail.Credit += AdjustedAPAllocation.AllocationAmount
                                End Select
                            Next
                        End If
                    End If
                    Dim getItemInEnergyAPAllocationList = Me._EnergyAPAllocationList.
                                                            Where(Function(x) x.InvoiceNumber = AdjustedAPAllocation.InvoiceNumber _
                                                                          And x.PaymentType = AdjustedAPAllocation.PaymentType _
                                                                          And x.PaymentCategory = AdjustedAPAllocation.PaymentCategory _
                                                                          And x.OffsettingSequence = AdjustedAPAllocation.OffsettingSequence).FirstOrDefault

                    If getItemInEnergyAPAllocationList Is Nothing Then
                        Me._EnergyAPAllocationList.Add(AdjustedAPAllocation)
                    Else
                        With getItemInEnergyAPAllocationList
                            .AllocationAmount += AdjustedAPAllocation.AllocationAmount
                            If .PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy Then
                                .NewEndingBalance = .EndingBalance
                            Else
                                .NewEndingBalance = .EndingBalance - .AllocationAmount
                            End If
                        End With
                    End If
                Case EnumChargeType.EV

                    Dim getItemInVATAPAllocationList = Me._VATonEnergyAPAllocationList.
                                                            Where(Function(x) x.InvoiceNumber = AdjustedAPAllocation.InvoiceNumber _
                                                                          And x.PaymentType = AdjustedAPAllocation.PaymentType _
                                                                          And x.PaymentCategory = AdjustedAPAllocation.PaymentCategory _
                                                                          And x.OffsettingSequence = AdjustedAPAllocation.OffsettingSequence).FirstOrDefault

                    If getItemInVATAPAllocationList Is Nothing Then
                        Me._VATonEnergyAPAllocationList.Add(AdjustedAPAllocation)
                    Else
                        With getItemInVATAPAllocationList
                            .AllocationAmount += AdjustedAPAllocation.AllocationAmount
                            .NewEndingBalance = .EndingBalance - .AllocationAmount
                        End With
                    End If
                Case EnumChargeType.MF, EnumChargeType.MFV
                    Me._MFWithVATAPAllocationList.Add(AdjustedAPAllocation)
            End Select
        Next
    End Sub

    'Update WESMBill for MarketFees including VAT
    Private Sub UpdateWESMBillSummaryMFMFV(ByRef WESMBillSummaryListPerBP As List(Of WESMBillSummary))
        For Each WBSItem In WESMBillSummaryListPerBP
            Dim GetAllocatedAmountonAP As Decimal = 0
            Dim PaymentType As New EnumPaymentNewType
            If WBSItem.ChargeType = EnumChargeType.MF Then
                PaymentType = EnumPaymentNewType.MarketFees
            Else
                PaymentType = EnumPaymentNewType.VatOnMarketFees
            End If
            GetAllocatedAmountonAP = (From x In Me.MFWithVATAPAllocationList
                                      Where x.WESMBillSummaryNo = WBSItem.WESMBillSummaryNo And x.PaymentType = PaymentType
                                      Select x.AllocationAmount).Sum()
            With WBSItem
                .NewDueDate = APAllocationDate
                .TransactionDate = APAllocationDate
                .EndingBalance = .EndingBalance - GetAllocatedAmountonAP
                .StatusUpdate = True
            End With
        Next
    End Sub

    'Updating WESMBillSummary based on the Allocated Amount from Collections
    Private Sub UpdateWESMBillSummary(ByRef WESMBillSummaryListPerBP As List(Of WESMBillSummary),
                                      ByVal PaymentType As EnumPaymentNewType)
        For Each WBSItem In WESMBillSummaryListPerBP
            Dim GetAllocatedAmountonAP As Decimal = 0
            Dim GetAllocatedAmountonAPEWT As Decimal = 0
            With WBSItem
                Select Case PaymentType
                    Case EnumPaymentNewType.Energy
                        GetAllocatedAmountonAP = (From x In Me.EnergyAPAllocationList
                                                  Where x.WESMBillSummaryNo = WBSItem.WESMBillSummaryNo And x.PaymentType = PaymentType
                                                  Select x.AllocationAmount).Sum()
                        Dim getAllocatedAmountonAPList = (From x In Me.EnergyAPAllocationList
                                                          Where x.WESMBillSummaryNo = WBSItem.WESMBillSummaryNo And x.PaymentType = PaymentType
                                                          Select x).ToList
                        GetAllocatedAmountonAPEWT = (From x In Me.EnergyAPAllocationList
                                                     Where x.WESMBillSummaryNo = WBSItem.WESMBillSummaryNo And x.PaymentType = EnumPaymentNewType.WithholdingTaxOnEnergy
                                                     Select x.AllocationAmount).Sum()
                    Case EnumPaymentNewType.VatOnEnergy
                        GetAllocatedAmountonAP = (From x In Me.VATonEnergyAPAllocationList
                                                  Where x.WESMBillSummaryNo = WBSItem.WESMBillSummaryNo And x.PaymentType = PaymentType
                                                  Select x.AllocationAmount).Sum()
                    Case EnumPaymentNewType.MarketFees
                        GetAllocatedAmountonAP = (From x In Me.MFWithVATAPAllocationList
                                                  Where x.WESMBillSummaryNo = WBSItem.WESMBillSummaryNo And x.PaymentType = PaymentType
                                                  Select x.AllocationAmount).Sum()
                    Case EnumPaymentNewType.VatOnMarketFees
                        GetAllocatedAmountonAP = (From x In Me.MFWithVATAPAllocationList
                                                  Where x.WESMBillSummaryNo = WBSItem.WESMBillSummaryNo And x.PaymentType = PaymentType
                                                  Select x.AllocationAmount).Sum()
                End Select
                .NewDueDate = APAllocationDate
                .TransactionDate = APAllocationDate
                .EndingBalance = .EndingBalance - GetAllocatedAmountonAP
                If .EnergyWithholdStatus = EnumEnergyWithholdStatus.UnpaidEWT And GetAllocatedAmountonAPEWT <> 0 Then
                    .EnergyWithholdStatus = EnumEnergyWithholdStatus.PaidEWT
                End If
                .StatusUpdate = True
            End With
        Next
    End Sub
#End Region

#Region "Function of AP Energy Data Table"
    Public Function CreateAPEnergyDT(ByVal _APAllocation As List(Of APAllocation)) As DataTable
        Dim EnergyAPDT As New DataTable
        Dim APDictionary As New Dictionary(Of String, Integer)

        EnergyAPDT.TableName = "AccountPayablesEnergy"
        With EnergyAPDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(Date))
            .Add("NewDueDate", GetType(Date))
            .Add("OutstandingBalance", GetType(Decimal))
            .Add("EnergyWithHold", GetType(String))
            .Add("AllocationDate", GetType(Date))
            .Add("EnergyAmount", GetType(Decimal))
            .Add("EnergyWithHoldAmount", GetType(Decimal))
            .Add("EnergyAmountDI", GetType(Decimal))
            .Add("EnergyDrawDown", GetType(Decimal))
            .Add("EnergyDrawDownDI", GetType(Decimal))
            .Add("NewOutstandingBalance", GetType(Decimal))
        End With
        Dim Counter As Long = 0

        For Each APItem As APAllocation In _APAllocation
            Dim row As DataRow
            If Not APDictionary.ContainsKey(APItem.InvoiceNumber) Then
                APDictionary.Add(APItem.InvoiceNumber, Counter)
                row = EnergyAPDT.NewRow()

                row("BillingPeriod") = APItem.BillingPeriod
                row("IDNumber") = APItem.IDNumber
                row("ParticipantID") = APItem.ParticipantID
                row("InvoiceNumber") = APItem.InvoiceNumber
                row("DueDate") = APItem.DueDate
                row("OutstandingBalance") = APItem.EndingBalance
                row("EnergyWithHold") = If(APItem.EnergyWithHold <> 0, FormatNumber(APItem.EnergyWithHold, UseParensForNegativeNumbers:=TriState.True), "")
                row("NewDueDate") = APItem.NewDueDate
                row("AllocationDate") = APItem.AllocationDate
                If APItem.PaymentCategory = EnumCollectionCategory.Cash Then
                    Select Case APItem.PaymentType
                        Case EnumPaymentNewType.Energy
                            row("EnergyAmount") = APItem.AllocationAmount
                            row("NewOutstandingBalance") = APItem.NewEndingBalance
                        Case EnumPaymentNewType.WithholdingTaxOnEnergy
                            row("EnergyWithHoldAmount") = APItem.AllocationAmount
                            row("NewOutstandingBalance") = APItem.NewEndingBalance
                        Case EnumPaymentNewType.DefaultInterestOnEnergy
                            row("EnergyAmountDI") = APItem.AllocationAmount
                            row("NewOutstandingBalance") = APItem.EndingBalance
                    End Select
                Else
                    Select Case APItem.PaymentType
                        Case EnumPaymentNewType.Energy
                            row("EnergyDrawDown") = APItem.AllocationAmount
                            row("NewOutstandingBalance") = APItem.NewEndingBalance
                        Case EnumPaymentNewType.WithholdingTaxOnEnergy
                            row("EnergyWithHoldAmount") = APItem.AllocationAmount
                            row("NewOutstandingBalance") = APItem.NewEndingBalance
                        Case EnumPaymentNewType.DefaultInterestOnEnergy
                            row("EnergyDrawDownDI") = APItem.AllocationAmount
                            row("NewOutstandingBalance") = APItem.EndingBalance
                    End Select
                End If
                Counter += 1
                EnergyAPDT.Rows.Add(row)
            Else
                row = EnergyAPDT(APDictionary(APItem.InvoiceNumber))

                If APItem.PaymentCategory = EnumCollectionCategory.Cash Then
                    Select Case APItem.PaymentType
                        Case EnumPaymentNewType.Energy
                            row("EnergyAmount") = APItem.AllocationAmount
                            If row("NewOutstandingBalance") > APItem.NewEndingBalance Then
                                row("NewOutstandingBalance") = APItem.NewEndingBalance
                            End If
                        Case EnumPaymentNewType.WithholdingTaxOnEnergy
                            row("EnergyWithHoldAmount") = APItem.AllocationAmount
                            If row("NewOutstandingBalance") > APItem.NewEndingBalance Then
                                row("NewOutstandingBalance") = APItem.NewEndingBalance
                            End If
                        Case EnumPaymentNewType.DefaultInterestOnEnergy
                            row("EnergyAmountDI") = APItem.AllocationAmount
                            If row("NewOutstandingBalance") > APItem.NewEndingBalance Then
                                row("NewOutstandingBalance") = APItem.NewEndingBalance
                            End If
                    End Select

                Else
                    Select Case APItem.PaymentType
                        Case EnumPaymentNewType.Energy
                            row("EnergyDrawDown") = APItem.AllocationAmount
                            If row("NewOutstandingBalance") > APItem.NewEndingBalance Then
                                row("NewOutstandingBalance") = APItem.NewEndingBalance
                            End If
                        Case EnumPaymentNewType.WithholdingTaxOnEnergy
                            row("EnergyWithHoldAmount") = APItem.AllocationAmount
                            If row("NewOutstandingBalance") > APItem.NewEndingBalance Then
                                row("NewOutstandingBalance") = APItem.NewEndingBalance
                            End If
                        Case EnumPaymentNewType.DefaultInterestOnEnergy
                            row("EnergyDrawDownDI") = APItem.AllocationAmount
                            If row("NewOutstandingBalance") > APItem.NewEndingBalance Then
                                row("NewOutstandingBalance") = APItem.NewEndingBalance
                            End If
                    End Select
                End If
            End If
        Next
        Return EnergyAPDT
    End Function

    Public Function CreateOffsetAPEnergyDT(ByVal _APAllocation As List(Of APAllocation)) As DataTable
        Dim EnergyAPDT As New DataTable
        Dim APDictionary As New Dictionary(Of String, Integer)
        Dim APDicOS As New Dictionary(Of String, String)

        EnergyAPDT.TableName = "AccountPayablesEnergy"
        With EnergyAPDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(Date))
            .Add("NewDueDate", GetType(Date))
            .Add("OutstandingBalance", GetType(Decimal))
            .Add("EnergyWithHold", GetType(String))
            .Add("AllocationDate", GetType(Date))
            .Add("EnergyOffsetAmount", GetType(Decimal))
            .Add("EnergyOffsetAmountDI", GetType(Decimal))
            .Add("NewOutstandingBalance", GetType(Decimal))
            .Add("OffsetSeqNo", GetType(String))
        End With
        Dim Counter As Long = 0
        For Each APItem As APAllocation In _APAllocation
            Dim row As DataRow
            If Not APDictionary.ContainsKey(APItem.InvoiceNumber) Then
                APDictionary.Add(APItem.InvoiceNumber, Counter)
                row = EnergyAPDT.NewRow()

                row("BillingPeriod") = APItem.BillingPeriod
                row("IDNumber") = APItem.IDNumber
                row("ParticipantID") = APItem.ParticipantID
                row("InvoiceNumber") = APItem.InvoiceNumber
                row("DueDate") = APItem.DueDate
                row("OutstandingBalance") = APItem.EndingBalance
                row("EnergyWithHold") = If(APItem.EnergyWithHold <> 0, FormatNumber(APItem.EnergyWithHold, UseParensForNegativeNumbers:=TriState.True), "")
                row("NewDueDate") = APItem.NewDueDate
                row("AllocationDate") = APItem.AllocationDate

                If Not APDicOS.ContainsKey(APItem.InvoiceNumber & APItem.OffsettingSequence) Then
                    APDicOS.Add(APItem.InvoiceNumber & APItem.OffsettingSequence, APItem.OffsettingSequence.ToString)
                    row("OffsetSeqNo") = APItem.OffsettingSequence
                End If

                If APItem.PaymentCategory = EnumCollectionCategory.Offset Then
                    Select Case APItem.PaymentType
                        Case EnumPaymentNewType.Energy
                            row("EnergyOffsetAmount") = APItem.AllocationAmount
                            row("NewOutstandingBalance") = APItem.NewEndingBalance
                        Case EnumPaymentNewType.DefaultInterestOnEnergy
                            row("EnergyOffsetAmountDI") = APItem.AllocationAmount
                            row("NewOutstandingBalance") = APItem.EndingBalance
                    End Select
                End If
                Counter += 1
                EnergyAPDT.Rows.Add(row)
            Else
                row = EnergyAPDT(APDictionary(APItem.InvoiceNumber))
                If Not APDicOS.ContainsKey(APItem.InvoiceNumber & APItem.OffsettingSequence) Then
                    APDicOS.Add(APItem.InvoiceNumber & APItem.OffsettingSequence, APItem.OffsettingSequence.ToString)
                    row("OffsetSeqNo") &= ";" & APItem.OffsettingSequence
                End If

                If APItem.PaymentCategory = EnumCollectionCategory.Offset Then
                    Select Case APItem.PaymentType
                        Case EnumPaymentNewType.Energy
                            row("EnergyOffsetAmount") = If(IsDBNull(row("EnergyOffsetAmount")), 0, CDec(row("EnergyOffsetAmount"))) + APItem.AllocationAmount
                            If IsDBNull(row("NewOutstandingBalance")) Then
                                row("NewOutstandingBalance") = 0
                            End If
                            If row("NewOutstandingBalance") > APItem.NewEndingBalance Then
                                row("NewOutstandingBalance") = APItem.NewEndingBalance
                            End If

                        Case EnumPaymentNewType.DefaultInterestOnEnergy
                            row("EnergyOffsetAmountDI") = If(IsDBNull(row("EnergyOffsetAmountDI")), 0, CDec(row("EnergyOffsetAmountDI"))) + APItem.AllocationAmount
                            If IsDBNull(row("NewOutstandingBalance")) Then
                                row("NewOutstandingBalance") = 0
                            End If
                            If row("NewOutstandingBalance") > APItem.NewEndingBalance Then
                                row("NewOutstandingBalance") = APItem.NewEndingBalance
                            End If
                    End Select
                End If
            End If
        Next
        Return EnergyAPDT
    End Function
#End Region

#Region "Function of AP Energy VAT Data Table"
    Public Function CreateAPVATDT(ByVal _APAllocation As List(Of APAllocation)) As DataTable
        Dim VATAPDT As New DataTable
        Dim APDictionary As New Dictionary(Of String, Integer)

        VATAPDT.TableName = "AccountPayablesVAT"
        With VATAPDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(Date))
            .Add("OutstandingBalance", GetType(Decimal))
            .Add("NewDueDate", GetType(Date))
            .Add("AllocationDate", GetType(Date))
            .Add("VATAmount", GetType(Decimal))
            .Add("NewOutstandingBalance", GetType(Decimal))
        End With

        Dim Counter As Long = 0
        For Each APItem As APAllocation In _APAllocation
            Dim row As DataRow
            If Not APDictionary.ContainsKey(APItem.InvoiceNumber) Then
                APDictionary.Add(APItem.InvoiceNumber, Counter)
                row = VATAPDT.NewRow()

                row("BillingPeriod") = APItem.BillingPeriod
                row("IDNumber") = APItem.IDNumber
                row("ParticipantID") = APItem.ParticipantID
                row("InvoiceNumber") = APItem.InvoiceNumber
                row("DueDate") = APItem.DueDate
                row("OutstandingBalance") = APItem.EndingBalance
                row("NewDueDate") = APItem.NewDueDate
                row("AllocationDate") = APItem.AllocationDate
                row("VATAmount") = APItem.AllocationAmount
                row("NewOutstandingBalance") = APItem.NewEndingBalance
                Counter += 1
                VATAPDT.Rows.Add(row)
            Else
                row = VATAPDT(APDictionary(APItem.InvoiceNumber))
                row("VATAmount") = APItem.AllocationAmount
                If CDec(APItem.NewEndingBalance) < CDec(row("NewOutstandingBalance")) Then
                    row("NewOutstandingBalance") = APItem.NewEndingBalance
                End If
            End If
        Next
        VATAPDT.AcceptChanges()

        Return VATAPDT
    End Function

    Public Function CreateOffsetAPVATDT(ByVal _APAllocation As List(Of APAllocation)) As DataTable
        Dim VATAPDT As New DataTable
        Dim APDictionary As New Dictionary(Of String, Integer)
        Dim APDicOS As New Dictionary(Of String, String)

        VATAPDT.TableName = "AccountPayablesVAT"
        With VATAPDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(Date))
            .Add("OutstandingBalance", GetType(Decimal))
            .Add("NewDueDate", GetType(Date))
            .Add("AllocationDate", GetType(Date))
            .Add("VATOffsetAmount", GetType(Decimal))
            .Add("NewOutstandingBalance", GetType(Decimal))
            .Add("OffsettingSequence", GetType(String))
        End With

        Dim Counter As Long = 0
        For Each APItem As APAllocation In _APAllocation
            Dim row As DataRow
            If Not APDictionary.ContainsKey(APItem.InvoiceNumber) Then
                APDictionary.Add(APItem.InvoiceNumber, Counter)
                row = VATAPDT.NewRow()

                row("BillingPeriod") = APItem.BillingPeriod
                row("IDNumber") = APItem.IDNumber
                row("ParticipantID") = APItem.ParticipantID
                row("InvoiceNumber") = APItem.InvoiceNumber
                row("DueDate") = APItem.DueDate
                row("OutstandingBalance") = APItem.EndingBalance
                row("NewDueDate") = APItem.NewDueDate
                row("AllocationDate") = APItem.AllocationDate
                row("VATOffsetAmount") = APItem.AllocationAmount
                row("NewOutstandingBalance") = APItem.NewEndingBalance
                If Not APDicOS.ContainsKey(APItem.InvoiceNumber & APItem.OffsettingSequence) Then
                    APDicOS.Add(APItem.InvoiceNumber & APItem.OffsettingSequence, APItem.OffsettingSequence.ToString)
                    row("OffsettingSequence") = APItem.OffsettingSequence
                End If

                Counter += 1
                VATAPDT.Rows.Add(row)
            Else
                row = VATAPDT(APDictionary(APItem.InvoiceNumber))
                row("VATOffsetAmount") = CDec(row("VATOffsetAmount")) + APItem.AllocationAmount
                If CDec(APItem.NewEndingBalance) < CDec(row("NewOutstandingBalance")) Then
                    row("NewOutstandingBalance") = APItem.NewEndingBalance
                End If
                If Not APDicOS.ContainsKey(APItem.InvoiceNumber & APItem.OffsettingSequence) Then
                    APDicOS.Add(APItem.InvoiceNumber & APItem.OffsettingSequence, APItem.OffsettingSequence.ToString)
                    row("OffsettingSequence") &= ";" & APItem.OffsettingSequence
                End If

            End If
        Next
        VATAPDT.AcceptChanges()

        Return VATAPDT
    End Function
#End Region

#Region "Function of AP MF Data Table"
    Public Function CreateAPMFDT(ByVal _APAllocation As List(Of APAllocation)) As DataTable

        Dim APDicOS As New Dictionary(Of String, String)

        Dim MFARDT As New DataTable
        Dim APDictionary As New Dictionary(Of String, Integer)
        MFARDT.TableName = "AccountReceivablesMF"
        With MFARDT.Columns
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(Date))
            .Add("NewDueDate", GetType(Date))
            .Add("OutstandingBalanceonMF", GetType(String))
            .Add("OutstandingBalanceonMFV", GetType(String))
            .Add("CollectionDate", GetType(Date))
            .Add("AmountonMF", GetType(String))
            .Add("AmountonMFDI", GetType(String))
            .Add("WTAXonMF", GetType(String))
            .Add("WTAXDIonMF", GetType(String))
            .Add("AmountonMFV", GetType(String))
            .Add("AmountonMFVDI", GetType(String))
            .Add("WVATonMF", GetType(String))
            .Add("WVATDIonMF", GetType(String))
            .Add("NewOutstandingBalanceonMF", GetType(String))
            .Add("NewOutstandingBalanceonMFV", GetType(String))
            .Add("OffsettingSequence", GetType(String))
        End With

        Dim Counter As Long = 0
        For Each APItem As APAllocation In _APAllocation
            Dim row As DataRow
            If Not APDictionary.ContainsKey(APItem.InvoiceNumber) Then
                APDictionary.Add(APItem.InvoiceNumber, Counter)
                row = MFARDT.NewRow()
                row("BillingPeriod") = APItem.BillingPeriod
                row("ParticipantID") = APItem.ParticipantID
                row("IDNumber") = APItem.IDNumber
                row("DueDate") = APItem.DueDate
                row("NewDueDate") = APItem.NewDueDate
                row("InvoiceNumber") = APItem.InvoiceNumber
                row("CollectionDate") = APItem.AllocationDate
                If Not APDicOS.ContainsKey(APItem.InvoiceNumber & APItem.OffsettingSequence) Then
                    APDicOS.Add(APItem.InvoiceNumber & APItem.OffsettingSequence, APItem.OffsettingSequence.ToString)
                    row("OffsettingSequence") = APItem.OffsettingSequence
                End If
                Select Case APItem.PaymentType
                    Case EnumPaymentNewType.MarketFees
                        row("OutstandingBalanceonMF") = FormatNumber(APItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        row("AmountonMF") = FormatNumber(APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                        row("NewOutstandingBalanceonMF") = FormatNumber(APItem.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumPaymentNewType.DefaultInterestOnMF
                        row("AmountonMFDI") = FormatNumber(APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumPaymentNewType.VatOnMarketFees
                        row("OutstandingBalanceonMFV") = FormatNumber(APItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        row("AmountonMFV") = FormatNumber(APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                        row("NewOutstandingBalanceonMFV") = FormatNumber(APItem.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumPaymentNewType.DefaultInterestOnVatOnMF
                        row("AmountonMFVDI") = FormatNumber(APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumPaymentNewType.WithholdingVatOnMF
                        row("WVATonMF") = If(APItem.AllocationAmount <> 0, FormatNumber(APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True), "")
                    Case EnumPaymentNewType.WithholdingVatOnDefaultInterest
                        row("WVATDIonMF") = If(APItem.AllocationAmount <> 0, FormatNumber(APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True), "")
                    Case EnumPaymentNewType.WithholdingTaxOnMF
                        row("WTAXonMF") = If(APItem.AllocationAmount <> 0, FormatNumber(APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True), "")
                    Case EnumPaymentNewType.WithholdingTaxOnDefaultInterest
                        row("WTAXDIonMF") = If(APItem.AllocationAmount <> 0, FormatNumber(APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True), "")
                End Select
                Counter += 1
                MFARDT.Rows.Add(row)
            Else
                row = MFARDT(APDictionary(APItem.InvoiceNumber))
                If Not APDicOS.ContainsKey(APItem.InvoiceNumber & APItem.OffsettingSequence) Then
                    APDicOS.Add(APItem.InvoiceNumber & APItem.OffsettingSequence, APItem.OffsettingSequence.ToString)
                    row("OffsettingSequence") &= ";" & APItem.OffsettingSequence
                End If
                Select Case APItem.PaymentType
                    Case EnumPaymentNewType.MarketFees
                        If IsDBNull(row("OutstandingBalanceonMF")) Then
                            row("OutstandingBalanceonMF") = FormatNumber(APItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        ElseIf Math.Abs(APItem.EndingBalance) > Math.Abs(CDec(row("OutstandingBalanceonMF"))) Then
                            row("OutstandingBalanceonMF") = FormatNumber(APItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        End If
                        row("AmountonMF") = FormatNumber(CDec(If(IsDBNull(row("AmountonMF")), 0, row("AmountonMF"))) + APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                        row("NewOutstandingBalanceonMF") = FormatNumber(If(IsDBNull(row("OutstandingBalanceonMF")), 0, CDec(row("OutstandingBalanceonMF"))) - APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumPaymentNewType.DefaultInterestOnMF
                        row("AmountonMFDI") = FormatNumber(If(IsDBNull(row("AmountonMFDI")), 0, CDec(row("AmountonMFDI"))) + APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumPaymentNewType.VatOnMarketFees
                        If IsDBNull(row("OutstandingBalanceonMFV")) Then
                            row("OutstandingBalanceonMFV") = FormatNumber(APItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        ElseIf Math.Abs(APItem.EndingBalance) > Math.Abs(CDec(row("OutstandingBalanceonMFV"))) Then
                            row("OutstandingBalanceonMFV") = FormatNumber(APItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
                        End If
                        row("AmountonMFV") = FormatNumber(If(IsDBNull(row("AmountonMFV")), 0, CDec(row("AmountonMFV"))) + APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                        row("NewOutstandingBalanceonMFV") = FormatNumber(If(IsDBNull(row("OutstandingBalanceonMFV")), 0, CDec(row("OutstandingBalanceonMFV"))) - APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumPaymentNewType.DefaultInterestOnVatOnMF
                        row("AmountonMFVDI") = FormatNumber(If(IsDBNull(row("AmountonMFVDI")), 0, CDec(row("AmountonMFVDI"))) + APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumPaymentNewType.WithholdingVatOnMF
                        row("WVATonMF") = FormatNumber(If(IsDBNull(row("WVATonMF")), 0, CDec(row("WVATonMF"))) + APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumPaymentNewType.WithholdingVatOnDefaultInterest
                        row("WVATDIonMF") = FormatNumber(If(IsDBNull(row("WVATDIonMF")), 0, CDec(row("WVATDIonMF"))) + APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumPaymentNewType.WithholdingTaxOnMF
                        row("WTAXonMF") = FormatNumber(If(IsDBNull(row("WTAXonMF")), 0, CDec(row("WTAXonMF"))) + APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumPaymentNewType.WithholdingTaxOnDefaultInterest
                        row("WTAXDIonMF") = FormatNumber(If(IsDBNull(row("WTAXDIonMF")), 0, CDec(row("WTAXDIonMF"))) + APItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
                End Select
            End If
        Next
        MFARDT.AcceptChanges()
        Return MFARDT
    End Function

#End Region

End Class
