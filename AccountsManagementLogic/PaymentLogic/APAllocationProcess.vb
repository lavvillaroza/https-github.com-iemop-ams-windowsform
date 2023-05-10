Imports System.Linq
Imports AccountsManagementObjects
Imports AccountsManagementDataAccess

Public Class APAllocationProcess
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

#Region "Property of EnergyAllocationListDT"
    Private _EnergyAPAllocationListDT As New DataTable
    Public ReadOnly Property EnergyAPAllocationListDT() As DataTable
        Get
            Return _EnergyAPAllocationListDT
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

#Region "Property of EnergyAllocationListDT"
    Private _VATonEnergyAPAllocationListDT As New DataTable
    Public ReadOnly Property VATonEnergyAPAllocationListDT() As DataTable
        Get
            Return _VATonEnergyAPAllocationListDT
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
                                        ByVal TotalAPEnergyPerBP As List(Of ARCollectionPerBP))
        Dim WESMBillSummaryListOnMFMFV = (From x In WESMBillSummaryList
                                          Where (x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV) _
                                          And x.EndingBalance > 0 And x.DueDate <= AllocationDate Select x).ToList()

        Me._APAllocationDate = AllocationDate
        Me.APMFAllocationProc(WESMBillSummaryListOnMFMFV, TotalAPEnergyPerBP)
    End Sub
#End Region

#Region "AP Allocation On Energy"
    Public Sub ComputeEnergyAPAllocationList(ByVal allocationDate As Date,
                                             ByRef _WESMBillSummaryList As List(Of WESMBillSummary),
                                             ByVal TotalAPEnergyPerBP As List(Of ARCollectionPerBP))
        Dim WESMBillSummaryListOnEnergy = (From x In _WESMBillSummaryList
                                           Where x.ChargeType = EnumChargeType.E And x.DueDate <= allocationDate
                                           Select x).ToList()
        Me._APAllocationDate = allocationDate
        Me.APEnergyAllocationProcess(WESMBillSummaryListOnEnergy, TotalAPEnergyPerBP, New List(Of WESMBillSalesAndPurchasedForWT))
    End Sub
#End Region

#Region "AP Allocation On Energy VAT"
    Public Sub ComputeVATAPAllocationList(ByVal AllocationDate As Date,
                                          ByRef WESMBillSummaryList As List(Of WESMBillSummary),
                                          ByVal TotalAPVATPerBP As List(Of ARCollectionPerBP))

        Dim WESMBillSummaryListOnVAT = (From x In WESMBillSummaryList Where x.ChargeType = EnumChargeType.EV And x.EndingBalance > 0 Select x).ToList()
        Me._APAllocationDate = AllocationDate
        Me.APVATAllocationProcess(WESMBillSummaryListOnVAT, TotalAPVATPerBP)
    End Sub
#End Region

#Region "APAllocation on Energy and VAT"
    Public Sub GetAPAllocationList(ByVal AllocationDate As Date,
                                   ByRef WESMBillSummaryList As List(Of WESMBillSummary),
                                   ByVal WESMBillSalesAndPurchasesList As List(Of WESMBillSalesAndPurchasedForWT),
                                   ByVal TotalAPEnergyPerBP As List(Of ARCollectionPerBP),
                                   ByVal TotalAPVATPerBP As List(Of ARCollectionPerBP),
                                   ByVal TotalAPMFPerBP As List(Of ARCollectionPerBP))

        Dim WESMBillSummaryListOnEnergy = (From x In WESMBillSummaryList Where x.ChargeType = EnumChargeType.E And x.DueDate <= AllocationDate Select x).ToList()
        Dim WESMBillSummaryListOnVAT = (From x In WESMBillSummaryList Where x.ChargeType = EnumChargeType.EV And x.DueDate <= AllocationDate Select x).ToList()
        Dim WESMBillSummaryListOnMFMF = (From x In WESMBillSummaryList
                                         Where (x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV) _
                                         And x.EndingBalance <> 0 And x.DueDate <= AllocationDate Select x).ToList()


        _APAllocationDate = AllocationDate

        Me.APEnergyAllocationProcess(WESMBillSummaryListOnEnergy, TotalAPEnergyPerBP, WESMBillSalesAndPurchasesList)
        Me.APVATAllocationProcess(WESMBillSummaryListOnVAT, TotalAPVATPerBP)
        Me.APMFAllocationProc(WESMBillSummaryListOnMFMF, TotalAPMFPerBP)

        'Me._MFWithVATAPAllocationListDT = Me.CreateAPMFDT(Me.MFWithVATAPAllocationList)
        'Me._EnergyAPAllocationListDT = Me.CreateAPEnergyDT(Me.EnergyAPAllocationList)
        'Me._VATonEnergyAPAllocationListDT = Me.CreateAPVATDT(Me.VATonEnergyAPAllocationList)

    End Sub

    Public Sub GetAPAllocationList2(ByVal AllocationDate As Date,
                                   ByRef WESMBillSummaryList As List(Of WESMBillSummary),
                                   ByVal WESMBillSalesAndPurchasesList As List(Of WESMBillSalesAndPurchasedForWT),
                                   ByVal TotalAPEnergyPerBP As List(Of ARCollectionPerBP),
                                   ByVal TotalAPVATPerBP As List(Of ARCollectionPerBP),
                                   ByVal TotalAPMFPerBP As List(Of ARCollectionPerBP))

        Dim WESMBillSummaryListOnEnergy = (From x In WESMBillSummaryList Where x.ChargeType = EnumChargeType.E And x.DueDate <= AllocationDate Select x).ToList()
        Dim WESMBillSummaryListOnVAT = (From x In WESMBillSummaryList Where x.ChargeType = EnumChargeType.EV And x.DueDate <= AllocationDate Select x).ToList()
        Dim WESMBillSummaryListOnMFMF = (From x In WESMBillSummaryList
                                         Where (x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV) _
                                         And x.EndingBalance <> 0 And x.DueDate <= AllocationDate Select x).ToList()


        _APAllocationDate = AllocationDate

        Me.APEnergyAllocationProcess(WESMBillSummaryListOnEnergy, TotalAPEnergyPerBP, WESMBillSalesAndPurchasesList)
        Me.APVATAllocationProcess(WESMBillSummaryListOnVAT, TotalAPVATPerBP)
        Me.APMFAllocationProc(WESMBillSummaryListOnMFMF, TotalAPMFPerBP)

        'Me._MFWithVATAPAllocationListDT = Me.CreateAPMFDT(Me.MFWithVATAPAllocationList)
        'Me._EnergyAPAllocationListDT = Me.CreateAPEnergyDT(Me.EnergyAPAllocationList)
        'Me._VATonEnergyAPAllocationListDT = Me.CreateAPVATDT(Me.VATonEnergyAPAllocationList)

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

        'Me._EnergyAPAllocationListDT = Me.CreateAPEnergyDT(Me.EnergyAPAllocationList)
        'Me._VATonEnergyAPAllocationListDT = Me.CreateAPVATDT(Me.VATonEnergyAPAllocationList)
        'Me._MFWithVATAPAllocationListDT = Me.CreateAPMFDT(Me.MFWithVATAPAllocationList)
    End Sub
#End Region

#Region "Method Step 1 for AP Allocation on MF"
    Private Sub APMFAllocationProc(ByRef WESMBillSummaryListOnMFMFV As List(Of WESMBillSummary),
                                   ByVal TotalARMFAmountCollectedPerBP As List(Of ARCollectionPerBP))

        Dim GetWESMBillSUmmaryListOnMFMFVAP As List(Of WESMBillSummary) = (From x In WESMBillSummaryListOnMFMFV
                                                                           Where x.EndingBalance > 0
                                                                           Select x).ToList
        If GetWESMBillSUmmaryListOnMFMFVAP.Count > 0 Then
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
                    Dim MFWHTaxAmount As Decimal = Math.Round(item.BeginningBalance * AMParticipantsInfo.MarketFeesWHTax * -1, 2)
                    Dim MFVWHVATAmount As Decimal = Math.Round(item.BeginningBalance * AMParticipantsInfo.MarketFeesWHVAT * -1, 2)
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

#Region "Method Step 2 for AP Allocation On MF"
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
                                          ByVal TotalAPEnergyPerBP As List(Of ARCollectionPerBP),
                                          Optional ByVal WESMBillSalesAndPuchasesList As List(Of WESMBillSalesAndPurchasedForWT) = Nothing)

        For Each BPItem As ARCollectionPerBP In TotalAPEnergyPerBP

            Dim getWESMTransCoverSummaryList As List(Of WESMBillAllocCoverSummary) = (From x In Me.WBillHelper.GetListWESMTransCoverSummaryPerWBatch(BPItem.WESMBillBatchNo) Select x).ToList


            Dim getWESMBillSummaryListPerBP = (From x In WESMBillSummaryListOnEnergy
                                               Where x.WESMBillBatchNo = BPItem.WESMBillBatchNo _
                                               And x.DueDate = BPItem.OrigDueDate _
                                               And x.EndingBalance >= 0
                                               Select x).ToList()
            Dim getTotalWithHoldingTax As Decimal = 0D
            If WESMBillSalesAndPuchasesList.Count > 0 Then
                getTotalWithHoldingTax = (From x In WESMBillSalesAndPuchasesList Where x.BillingBatchNo = BPItem.WESMBillBatchNo Select x.WithholdingTAX).Sum()
            End If

            Me.AllocatePaymentOnEnergy(getWESMBillSummaryListPerBP, BPItem, getTotalWithHoldingTax, getWESMTransCoverSummaryList)
        Next
    End Sub
#End Region

#Region "Method Step 2 for AP Allocation on Energy"
    Private Sub AllocatePaymentOnEnergy(ByRef WESMBillSummaryListPerBP As List(Of WESMBillSummary),
                                        ByVal TotalARAmountCollectedPerBP As ARCollectionPerBP,
                                        ByVal totalWithholdingTax As Decimal,
                                        ByVal listWESMTransCoverSummary As List(Of WESMBillAllocCoverSummary))

        Dim APAllocationListPerBP As New List(Of APAllocation)

        Dim BPTotalBalance As Decimal = (From x In WESMBillSummaryListPerBP Where x.EndingBalance > 0 Select x.EndingBalance).Sum() 'Total of Outstanding Balances per BillingPeriod based on WESMBillSummaryList
        Dim BPTotalBalance2 As Decimal = listWESMTransCoverSummary.Where(Function(y) y.NetSale > 0).Select(Function(x) x.NetSale).Sum()

        'Deleted due to duplicate allocation
        TotalARAmountCollectedPerBP.CashAmount += totalWithholdingTax
        For Each WESMBillSummaryItem In WESMBillSummaryListPerBP
            'Allocate AP on Energy that came from Cash  and Add to List of APAllocation
            If TotalARAmountCollectedPerBP.CashAmount <> 0 Then
                'Dim CashShareOnEnergy = Me.ComputeAllocation(WESMBillSummaryItem.EndingBalance, BPTotalBalance, TotalARAmountCollectedPerBP.CashAmount)

                Dim CashShareOnEnergy As Decimal = 0D

                If BPTotalBalance2 <> 0 Then
                    Dim getWESMTransCoverSummaryItem As WESMBillAllocCoverSummary = listWESMTransCoverSummary.Where(Function(x) x.TransactionNo = WESMBillSummaryItem.INVDMCMNo).First
                    CashShareOnEnergy = Me.ComputeAllocation(getWESMTransCoverSummaryItem.NetSale, BPTotalBalance2, TotalARAmountCollectedPerBP.CashAmount)
                Else
                    CashShareOnEnergy = Me.ComputeAllocation(WESMBillSummaryItem.EndingBalance, BPTotalBalance, TotalARAmountCollectedPerBP.CashAmount)
                End If

                Dim CreatedItem_Energy As New APAllocation
                CreatedItem_Energy = Me.CreateAPAllocationItem(WESMBillSummaryItem, CashShareOnEnergy,
                                                            EnumPaymentNewType.Energy, EnumCollectionCategory.Cash)
                APAllocationListPerBP.Add(CreatedItem_Energy)

            End If

            'Allocate AP on Energy Default Interest that came from Cash and Add to List of APAllocation
            If TotalARAmountCollectedPerBP.CashAmountDI <> 0 Then

                Dim CashShareOnEnergyDI As Decimal = 0D

                If BPTotalBalance2 <> 0 Then
                    Dim getWESMTransCoverSummaryItem As WESMBillAllocCoverSummary = listWESMTransCoverSummary.Where(Function(x) x.TransactionNo = WESMBillSummaryItem.INVDMCMNo).First
                    CashShareOnEnergyDI = Me.ComputeAllocation(getWESMTransCoverSummaryItem.NetSale, BPTotalBalance2, TotalARAmountCollectedPerBP.CashAmountDI)
                Else
                    CashShareOnEnergyDI = Me.ComputeAllocation(WESMBillSummaryItem.EndingBalance, BPTotalBalance, TotalARAmountCollectedPerBP.CashAmountDI)
                End If

                Dim CreatedItem_EnergyDefault As New APAllocation
                CreatedItem_EnergyDefault = Me.CreateAPAllocationItem(WESMBillSummaryItem, CashShareOnEnergyDI,
                                                                           EnumPaymentNewType.DefaultInterestOnEnergy, EnumCollectionCategory.Cash)
                APAllocationListPerBP.Add(CreatedItem_EnergyDefault)
            End If

            'Allocate AP on Energy that came from DrawDownand Add to List of APAllocation
            If TotalARAmountCollectedPerBP.DrawDownAmount <> 0 Then

                Dim DrawDownShareOnEnergy As Decimal = 0D

                If BPTotalBalance2 <> 0 Then
                    Dim getWESMTransCoverSummaryItem As WESMBillAllocCoverSummary = listWESMTransCoverSummary.Where(Function(x) x.TransactionNo = WESMBillSummaryItem.INVDMCMNo).First
                    DrawDownShareOnEnergy = Me.ComputeAllocation(getWESMTransCoverSummaryItem.NetSale, BPTotalBalance2, TotalARAmountCollectedPerBP.DrawDownAmount)
                Else
                    DrawDownShareOnEnergy = Me.ComputeAllocation(WESMBillSummaryItem.EndingBalance, BPTotalBalance, TotalARAmountCollectedPerBP.DrawDownAmount)
                End If

                Dim CreatedItem_DDEnergy = Me.CreateAPAllocationItem(WESMBillSummaryItem, DrawDownShareOnEnergy,
                                                                     EnumPaymentNewType.Energy, EnumCollectionCategory.Drawdown)
                APAllocationListPerBP.Add(CreatedItem_DDEnergy)
            End If

            'Allocate AP on Energy that came from DrawDown Defaul interest
            If TotalARAmountCollectedPerBP.DrawDownAmountDI <> 0 Then
                Dim DrawDownShareOnEnergyDI As Decimal = 0D

                If BPTotalBalance2 <> 0 Then
                    Dim getWESMTransCoverSummaryItem As WESMBillAllocCoverSummary = listWESMTransCoverSummary.Where(Function(x) x.TransactionNo = WESMBillSummaryItem.INVDMCMNo).First
                    DrawDownShareOnEnergyDI = Me.ComputeAllocation(getWESMTransCoverSummaryItem.NetSale, BPTotalBalance2, TotalARAmountCollectedPerBP.DrawDownAmountDI)
                Else
                    DrawDownShareOnEnergyDI = Me.ComputeAllocation(WESMBillSummaryItem.EndingBalance, BPTotalBalance, TotalARAmountCollectedPerBP.DrawDownAmountDI)
                End If

                Dim CreatedItem_DDEnergyDefault = Me.CreateAPAllocationItem(WESMBillSummaryItem, DrawDownShareOnEnergyDI,
                                                                               EnumPaymentNewType.DefaultInterestOnEnergy, EnumCollectionCategory.Drawdown)
                APAllocationListPerBP.Add(CreatedItem_DDEnergyDefault)
            End If

            'Allocate AP on Energy that came from offset Energy
            If TotalARAmountCollectedPerBP.OffsetAmount <> 0 Then
                Dim OffsetShareOnEnergy As Decimal = 0D

                If BPTotalBalance2 <> 0 Then
                    Dim getWESMTransCoverSummaryItem As WESMBillAllocCoverSummary = listWESMTransCoverSummary.Where(Function(x) x.TransactionNo = WESMBillSummaryItem.INVDMCMNo).First
                    OffsetShareOnEnergy = Me.ComputeAllocation(getWESMTransCoverSummaryItem.NetSale, BPTotalBalance2, TotalARAmountCollectedPerBP.OffsetAmount)
                Else
                    OffsetShareOnEnergy = Me.ComputeAllocation(WESMBillSummaryItem.EndingBalance, BPTotalBalance, TotalARAmountCollectedPerBP.OffsetAmount)
                End If

                Dim CreatedItem_OffsetEnergy = Me.CreateAPAllocationItem(WESMBillSummaryItem, OffsetShareOnEnergy,
                                                                               EnumPaymentNewType.Energy, EnumCollectionCategory.Offset)
                APAllocationListPerBP.Add(CreatedItem_OffsetEnergy)
            End If

            'Allocate AP on Energy that came from offset Energy Default Interest
            'If TotalARAmountCollectedPerBP.OffsetAmountDI <> 0 Then
            If TotalARAmountCollectedPerBP.OffsetAmount = 0 Then
                'If invoice had a default interest but no base amount in energy the system shall force to create DMCM for energy even if is 0. added on 9/14/2017 by lance due to error in JV Payment Alloc
                Dim CreatedItem_OffsetEnergy = Me.CreateAPAllocationItem(WESMBillSummaryItem, 0,
                                                                         EnumPaymentNewType.Energy, EnumCollectionCategory.Offset,
                                                                         True)
                APAllocationListPerBP.Add(CreatedItem_OffsetEnergy)

                Dim OffsetShareOnEnergyDI As Decimal = 0D

                If BPTotalBalance2 <> 0 Then
                    Dim getWESMTransCoverSummaryItem As WESMBillAllocCoverSummary = listWESMTransCoverSummary.Where(Function(x) x.TransactionNo = WESMBillSummaryItem.INVDMCMNo).First
                    OffsetShareOnEnergyDI = Me.ComputeAllocation(getWESMTransCoverSummaryItem.NetSale, BPTotalBalance2, TotalARAmountCollectedPerBP.OffsetAmountDI)
                Else
                    OffsetShareOnEnergyDI = Me.ComputeAllocation(WESMBillSummaryItem.EndingBalance, BPTotalBalance, TotalARAmountCollectedPerBP.OffsetAmountDI)
                End If

                Dim CreatedItem_OffsetEnergyDefault = Me.CreateAPAllocationItem(WESMBillSummaryItem, OffsetShareOnEnergyDI,
                                                                               EnumPaymentNewType.DefaultInterestOnEnergy, EnumCollectionCategory.Offset)
                APAllocationListPerBP.Add(CreatedItem_OffsetEnergyDefault)

            End If
        Next

        'Dim TotalAmountAllocatedCashEnergy = (From x In APAllocationListPerBP _
        '                                      Where x.PaymentType = EnumPaymentNewType.Energy _
        '                                      And x.PaymentCategory = EnumCollectionCategory.Cash _
        '                                      Select x.AllocationAmount).Sum()

        Dim TotalAmountAllocatedCashEnergy = (From x In APAllocationListPerBP
                                              Where x.PaymentType = EnumPaymentNewType.Energy _
                                              And x.PaymentCategory = EnumCollectionCategory.Cash _
                                              And x.PaymentType <> EnumPaymentNewType.WithholdingTaxOnEnergy
                                              Select x.AllocationAmount).Sum()

        Dim TotalAmountAllocatedCashEnergyDI = (From x In APAllocationListPerBP
                                                Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy _
                                                And x.PaymentCategory = EnumCollectionCategory.Cash
                                                Select x.AllocationAmount).Sum()


        Dim TotalAmountAllocatedDrawDownEnergy = (From x In APAllocationListPerBP
                                                  Where x.PaymentType = EnumPaymentNewType.Energy _
                                                  And x.PaymentCategory = EnumCollectionCategory.Drawdown
                                                  Select x.AllocationAmount).Sum()

        Dim TotalAmountAllocatedDrawDownEnergyDI = (From x In APAllocationListPerBP
                                                    Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy _
                                                    And x.PaymentCategory = EnumCollectionCategory.Drawdown
                                                    Select x.AllocationAmount).Sum()

        Dim TotalAmountAllocatedOffsetEnergy = (From x In APAllocationListPerBP
                                                Where x.PaymentType = EnumPaymentNewType.Energy _
                                                  And x.PaymentCategory = EnumCollectionCategory.Offset
                                                Select x.AllocationAmount).Sum()

        Dim TotalAmountAllocatedOffsetEnergyDI = (From x In APAllocationListPerBP
                                                  Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy _
                                                    And x.PaymentCategory = EnumCollectionCategory.Offset
                                                  Select x.AllocationAmount).Sum()


        Dim GetAmountDiffCashEnergy As Decimal = TotalARAmountCollectedPerBP.CashAmount - TotalAmountAllocatedCashEnergy
        Dim GetAmountDiffCashEnergyDI As Decimal = TotalARAmountCollectedPerBP.CashAmountDI - TotalAmountAllocatedCashEnergyDI
        Dim GetAmountDiffDrawDownEnergy As Decimal = TotalARAmountCollectedPerBP.DrawDownAmount - TotalAmountAllocatedDrawDownEnergy
        Dim GetAmountDiffDrawDownEnergyDI As Decimal = TotalARAmountCollectedPerBP.DrawDownAmountDI - TotalAmountAllocatedDrawDownEnergyDI
        Dim GetAmountDiffOffsetEnergy As Decimal = TotalARAmountCollectedPerBP.OffsetAmount - TotalAmountAllocatedOffsetEnergy
        Dim GetAmountDiffOffsetEnergyDI As Decimal = TotalARAmountCollectedPerBP.OffsetAmountDI - TotalAmountAllocatedOffsetEnergyDI

        If GetAmountDiffCashEnergy <> 0 And TotalARAmountCollectedPerBP.CashAmount <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where x.PaymentType = EnumPaymentNewType.Energy And x.PaymentCategory = EnumCollectionCategory.Cash
                                          Select x).ToList()

            Me.AdjustComputedAllocation(_APAllocationListPerBP, GetAmountDiffCashEnergy) 'Function that will adjust the Allocated amount per 

            'remove due to withholding Tax agent
            'Dim UpdatedAPAllocationListPerBP As List(Of APAllocation) = (From x In _APAllocationListPerBP Where x.AllocationAmount <> 0 Select x).ToList
            Me.AddAdjustedAPAllocationList(_APAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        ElseIf GetAmountDiffCashEnergy = 0 And TotalARAmountCollectedPerBP.CashAmount <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where x.PaymentType = EnumPaymentNewType.Energy And x.PaymentCategory = EnumCollectionCategory.Cash
                                          Select x).ToList()

            Dim UpdatedAPAllocationListPerBP As List(Of APAllocation) = (From x In _APAllocationListPerBP Where x.AllocationAmount <> 0 Select x).ToList
            Me.AddAdjustedAPAllocationList(UpdatedAPAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        End If

        If GetAmountDiffCashEnergyDI <> 0 And TotalARAmountCollectedPerBP.CashAmountDI <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy And x.PaymentCategory = EnumCollectionCategory.Cash
                                          Select x).ToList()

            Me.AdjustComputedAllocation(_APAllocationListPerBP, GetAmountDiffCashEnergyDI) 'Function that will adjust the Allocated amount per invoice

            Dim UpdatedAPAllocationListPerBP As List(Of APAllocation) = (From x In _APAllocationListPerBP Where x.AllocationAmount <> 0 Select x).ToList
            Me.AddAdjustedAPAllocationList(UpdatedAPAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        ElseIf GetAmountDiffCashEnergyDI = 0 And TotalARAmountCollectedPerBP.CashAmountDI <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy And x.PaymentCategory = EnumCollectionCategory.Cash
                                          Select x).ToList()

            Dim UpdatedAPAllocationListPerBP As List(Of APAllocation) = (From x In _APAllocationListPerBP Where x.AllocationAmount <> 0 Select x).ToList
            Me.AddAdjustedAPAllocationList(UpdatedAPAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        End If

        If GetAmountDiffDrawDownEnergy <> 0 And TotalARAmountCollectedPerBP.DrawDownAmount <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where x.PaymentType = EnumPaymentNewType.Energy And x.PaymentCategory = EnumCollectionCategory.Drawdown
                                          Select x).ToList()
            Me.AdjustComputedAllocation(_APAllocationListPerBP, GetAmountDiffDrawDownEnergy) 'Function that will adjust the Allocated amount per invoice

            Dim UpdatedAPAllocationListPerBP As List(Of APAllocation) = (From x In _APAllocationListPerBP Where x.AllocationAmount <> 0 Select x).ToList
            Me.AddAdjustedAPAllocationList(UpdatedAPAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        ElseIf GetAmountDiffDrawDownEnergy = 0 And TotalARAmountCollectedPerBP.DrawDownAmount <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where x.PaymentType = EnumPaymentNewType.Energy And x.PaymentCategory = EnumCollectionCategory.Drawdown
                                          Select x).ToList()

            Dim UpdatedAPAllocationListPerBP As List(Of APAllocation) = (From x In _APAllocationListPerBP Where x.AllocationAmount <> 0 Select x).ToList
            Me.AddAdjustedAPAllocationList(UpdatedAPAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        End If

        If GetAmountDiffDrawDownEnergyDI <> 0 And TotalARAmountCollectedPerBP.DrawDownAmountDI <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy And x.PaymentCategory = EnumCollectionCategory.Drawdown
                                          Select x).ToList()
            Me.AdjustComputedAllocation(_APAllocationListPerBP, GetAmountDiffDrawDownEnergyDI) 'Function that will adjust the Allocated amount per invoice
            Dim UpdatedAPAllocationListPerBP As List(Of APAllocation) = (From x In _APAllocationListPerBP Where x.AllocationAmount <> 0 Select x).ToList
            Me.AddAdjustedAPAllocationList(UpdatedAPAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        ElseIf GetAmountDiffDrawDownEnergyDI = 0 And TotalARAmountCollectedPerBP.DrawDownAmountDI <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy And x.PaymentCategory = EnumCollectionCategory.Drawdown
                                          Select x).ToList()
            Dim UpdatedAPAllocationListPerBP As List(Of APAllocation) = (From x In _APAllocationListPerBP Where x.AllocationAmount <> 0 Select x).ToList
            Me.AddAdjustedAPAllocationList(UpdatedAPAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        End If


        If GetAmountDiffOffsetEnergy <> 0 And TotalARAmountCollectedPerBP.OffsetAmount <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where x.PaymentType = EnumPaymentNewType.Energy And x.PaymentCategory = EnumCollectionCategory.Offset
                                          Select x).ToList()
            Me.AdjustComputedAllocation(_APAllocationListPerBP, GetAmountDiffOffsetEnergy) 'Function that will adjust the Allocated amount per invoice
            Dim UpdatedAPAllocationListPerBP As List(Of APAllocation) = (From x In _APAllocationListPerBP Where x.AllocationAmount <> 0 Select x).ToList
            Me.AddAdjustedAPAllocationList(UpdatedAPAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        ElseIf GetAmountDiffOffsetEnergy = 0 And TotalARAmountCollectedPerBP.OffsetAmount <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where x.PaymentType = EnumPaymentNewType.Energy And x.PaymentCategory = EnumCollectionCategory.Offset
                                          Select x).ToList()

            Dim UpdatedAPAllocationListPerBP As List(Of APAllocation) = (From x In _APAllocationListPerBP Where x.AllocationAmount <> 0 Select x).ToList
            Me.AddAdjustedAPAllocationList(UpdatedAPAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        End If


        If GetAmountDiffOffsetEnergyDI <> 0 And TotalARAmountCollectedPerBP.OffsetAmountDI <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy And x.PaymentCategory = EnumCollectionCategory.Offset
                                          Select x).ToList()
            Me.AdjustComputedAllocation(_APAllocationListPerBP, GetAmountDiffOffsetEnergyDI) 'Function that will adjust the Allocated amount per invoice
            Dim UpdatedAPAllocationListPerBP As List(Of APAllocation) = (From x In _APAllocationListPerBP Where x.AllocationAmount <> 0 Select x).ToList
            Me.AddAdjustedAPAllocationList(UpdatedAPAllocationListPerBP) 'Method that will add new Item for APAllocationList Property

        ElseIf GetAmountDiffOffsetEnergyDI = 0 And TotalARAmountCollectedPerBP.OffsetAmountDI <> 0 Then
            Dim _APAllocationListPerBP = (From x In APAllocationListPerBP
                                          Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy And x.PaymentCategory = EnumCollectionCategory.Offset
                                          Select x).ToList()
            Dim UpdatedAPAllocationListPerBP As List(Of APAllocation) = (From x In _APAllocationListPerBP Where x.AllocationAmount <> 0 Select x).ToList
            Me.AddAdjustedAPAllocationList(UpdatedAPAllocationListPerBP) 'Method that will add new Item for APAllocationList Property
        End If

        Me.UpdateWESMBillSummary(WESMBillSummaryListPerBP, EnumPaymentNewType.Energy)

    End Sub
#End Region

#Region "Method Step 1 for AP Allocation on Energy VAT"
    Private Sub APVATAllocationProcess(ByVal WESMBillSummaryListOnVAT As List(Of WESMBillSummary),
                                  ByVal TotalAPVATPerBP As List(Of ARCollectionPerBP))

        For Each Item As ARCollectionPerBP In TotalAPVATPerBP
            'Added By LAVV 2022
            Dim getWESMTransCoverSummaryList As List(Of WESMBillAllocCoverSummary) = (From x In Me.WBillHelper.GetListWESMTransCoverSummaryPerWBatch(Item.WESMBillBatchNo) Select x).ToList

            Dim APAllocationListperBP = (From x In WESMBillSummaryListOnVAT
                                         Where x.WESMBillBatchNo = Item.WESMBillBatchNo _
                                         And x.DueDate = Item.OrigDueDate
                                         Select x).ToList()
            Me.AllocatePaymentOnVAT(APAllocationListperBP, Item, getWESMTransCoverSummaryList)
        Next

    End Sub
#End Region

#Region "Method Step 2 for AP Allocation On Energy VAT"
    Private Sub AllocatePaymentOnVAT(ByRef WESMBillSummaryListPerBP As List(Of WESMBillSummary),
                                     ByVal TotalARAmountCollectedPerBP As ARCollectionPerBP,
                                     ByVal listWESMTransCoverSummary As List(Of WESMBillAllocCoverSummary))

        Dim APAllocationListPerBP As New List(Of APAllocation)

        Dim BPTotalBalance As Decimal = (From x In WESMBillSummaryListPerBP Where x.EndingBalance > 0 Select x.EndingBalance).Sum() 'Total of Outstanding Balances per BillingPeriod based on WESMBillSummaryList        
        Dim BPTotalBalance2 As Decimal = listWESMTransCoverSummary.Where(Function(y) y.VatOnSales > 0).Select(Function(x) x.VatOnSales).Sum()

        For Each WESMBillSummaryItem In WESMBillSummaryListPerBP
            'Allocate AP on VAT that came from Cash
            If TotalARAmountCollectedPerBP.CashAmount <> 0 Then
                Dim CashShareOnVAT As Decimal = 0D

                If BPTotalBalance2 <> 0 Then
                    Dim getWESMTransCoverSummaryItem As WESMBillAllocCoverSummary = listWESMTransCoverSummary.Where(Function(x) x.TransactionNo = WESMBillSummaryItem.INVDMCMNo).First
                    CashShareOnVAT = Me.ComputeAllocation(getWESMTransCoverSummaryItem.VatOnSales, BPTotalBalance2, TotalARAmountCollectedPerBP.CashAmount)
                Else
                    CashShareOnVAT = Me.ComputeAllocation(WESMBillSummaryItem.EndingBalance, BPTotalBalance, TotalARAmountCollectedPerBP.CashAmount)
                End If

                Dim APAllocationOnCashAmount = Me.CreateAPAllocationItem(WESMBillSummaryItem, CashShareOnVAT,
                                                                         EnumPaymentNewType.VatOnEnergy, EnumCollectionCategory.Cash)
                APAllocationListPerBP.Add(APAllocationOnCashAmount)
            End If
            'Allocate AP on Energy that came from offset Energy
            If TotalARAmountCollectedPerBP.OffsetAmount <> 0 Then
                Dim OffsetShareOnVAT = Me.ComputeAllocation(WESMBillSummaryItem.EndingBalance, BPTotalBalance, TotalARAmountCollectedPerBP.OffsetAmount)
                Dim CreatedItem_OffsetVAT = Me.CreateAPAllocationItem(WESMBillSummaryItem, OffsetShareOnVAT,
                                                                      EnumPaymentNewType.VatOnEnergy, EnumCollectionCategory.Offset)
                APAllocationListPerBP.Add(CreatedItem_OffsetVAT)
            End If
        Next
        Dim TotalAmountAllocatedCashEnergyVAT = (From x In APAllocationListPerBP
                                                 Where x.PaymentType = EnumPaymentNewType.VatOnEnergy And x.PaymentCategory = EnumCollectionCategory.Cash
                                                 Select x.AllocationAmount).Sum()
        Dim GetAmountDiffCashEnergyVAT As Decimal = TotalARAmountCollectedPerBP.CashAmount - TotalAmountAllocatedCashEnergyVAT

        Dim TotalAmountAllocatedOffsetEnergyVAT = (From x In APAllocationListPerBP
                                                   Where x.PaymentType = EnumPaymentNewType.VatOnEnergy And x.PaymentCategory = EnumCollectionCategory.Offset
                                                   Select x.AllocationAmount).Sum()
        Dim GetAmountDiffOffsetEnergyVAT As Decimal = TotalARAmountCollectedPerBP.OffsetAmount - TotalAmountAllocatedOffsetEnergyVAT

        If GetAmountDiffCashEnergyVAT <> 0 And TotalARAmountCollectedPerBP.CashAmount <> 0 Then
            Dim _iAPAllocationList = (From x In APAllocationListPerBP
                                      Where x.PaymentType = EnumPaymentNewType.VatOnEnergy And x.PaymentCategory = EnumCollectionCategory.Cash
                                      Select x).ToList()
            Me.AdjustComputedAllocation(_iAPAllocationList, GetAmountDiffCashEnergyVAT) 'Function that will adjust the Allocated amount per invoice
            Me.AddAdjustedAPAllocationList(_iAPAllocationList) 'Method that will add new Item for APAllocationList Property

        ElseIf GetAmountDiffCashEnergyVAT = 0 And TotalARAmountCollectedPerBP.CashAmount <> 0 Then
            Dim _iAPAllocationList = (From x In APAllocationListPerBP
                                      Where x.PaymentType = EnumPaymentNewType.VatOnEnergy And x.PaymentCategory = EnumCollectionCategory.Cash
                                      Select x).ToList()
            Me.AddAdjustedAPAllocationList(_iAPAllocationList) 'Method that will add new Item for APAllocationList Property
        End If

        If GetAmountDiffOffsetEnergyVAT <> 0 And TotalARAmountCollectedPerBP.OffsetAmount <> 0 Then
            Dim _iAPAllocationList = (From x In APAllocationListPerBP
                                      Where x.PaymentType = EnumPaymentNewType.VatOnEnergy And x.PaymentCategory = EnumCollectionCategory.Offset
                                      Select x).ToList()
            Me.AdjustComputedAllocation(_iAPAllocationList, GetAmountDiffOffsetEnergyVAT) 'Function that will adjust the Allocated amount per invoice
            Me.AddAdjustedAPAllocationList(_iAPAllocationList) 'Method that will add new Item for APAllocationList Property

        ElseIf GetAmountDiffOffsetEnergyVAT = 0 And TotalARAmountCollectedPerBP.OffsetAmount <> 0 Then
            Dim _iAPAllocationList = (From x In APAllocationListPerBP
                                      Where x.PaymentType = EnumPaymentNewType.VatOnEnergy And x.PaymentCategory = EnumCollectionCategory.Offset
                                      Select x).ToList()
            Me.AddAdjustedAPAllocationList(_iAPAllocationList) 'Method that will add new Item for APAllocationList Property
        End If

        Me.UpdateWESMBillSummary(WESMBillSummaryListPerBP, EnumPaymentNewType.VatOnEnergy)
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
            returnAmntAlloc = (InvOutstandingBalance / CurrentBP_TotalBalance) * TotalPaymentForBP
        Else
            returnAmntAlloc = 0
        End If
        returnAmntAlloc = Math.Round(returnAmntAlloc, 2)
        Return returnAmntAlloc
    End Function

    Private Sub AdjustComputedAllocation(ByRef UpdateAPAllocationList As List(Of APAllocation),
                                              ByVal AmountDiff As Decimal)
        Dim GetHighiestAmount = (From x In UpdateAPAllocationList Select x Order By x.AllocationAmount Descending, x.EndingBalance Descending).FirstOrDefault
        Dim UpdatedAPAllocationListPerBP As New List(Of APAllocation)

        If Not GetHighiestAmount Is Nothing Then
            If (GetHighiestAmount.NewEndingBalance = 0 And AmountDiff > 0) Or (GetHighiestAmount.NewEndingBalance < AmountDiff And AmountDiff > 0) Then
                Dim GetListOfInvoiceWithEOAmount = (From x In UpdateAPAllocationList Select x Where x.NewEndingBalance > 0 Order By x.AllocationAmount Descending, x.EndingBalance Descending).ToList
                Dim multiplyBy As Integer = AmountDiff * 100
                Dim totalAllocDiffAmount As Decimal = 0D

                For Each item In GetListOfInvoiceWithEOAmount
                    Dim computedRatio As Decimal = item.NewEndingBalance / GetListOfInvoiceWithEOAmount.Select(Function(x) x.NewEndingBalance).Sum
                    Dim computedShareDiffAmount As Decimal = Math.Round((multiplyBy * computedRatio) / 100, 2)
                    totalAllocDiffAmount += computedShareDiffAmount

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
                            'lance
                            If AmountDiff <> 0 And .AllocationAmount = 0 Then
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
                Next
                If AmountDiff <> totalAllocDiffAmount Then
                    Dim getAmountDiff As Decimal = AmountDiff - totalAllocDiffAmount
                    Dim getHighestValue = (From x In GetListOfInvoiceWithEOAmount Select x Order By x.NewEndingBalance Descending).FirstOrDefault
                    If getHighestValue Is Nothing Then
                        getHighestValue = (From x In GetListOfInvoiceWithEOAmount Select x Order By x.EndingBalance Descending).FirstOrDefault
                    End If
                    Dim UpdateAPAllocation As APAllocation = (From x In UpdateAPAllocationList Where x.WESMBillSummaryNo = getHighestValue.WESMBillSummaryNo Select x).FirstOrDefault
                    With UpdateAPAllocation
                        Dim DMCMItem = (From x In Me.ListofDMCMAP Where x.DMCMNumber = .GeneratedDMCM Select x).FirstOrDefault
                        If Not DMCMItem Is Nothing Then
                            DMCMItem.VATExempt += getAmountDiff
                            DMCMItem.TotalAmountDue += getAmountDiff
                            For Each ItemDetail In DMCMItem.DMCMDetails
                                If ItemDetail.Credit = 0 Then
                                    ItemDetail.Debit += getAmountDiff
                                Else
                                    ItemDetail.Credit += getAmountDiff
                                End If
                            Next
                            .AllocationAmount += getAmountDiff
                        Else
                            'lance
                            If getAmountDiff <> 0 And .AllocationAmount = 0 Then
                                Dim _DMCM As New DebitCreditMemo
                                Dim MustBeAllocationAmount As Decimal = .AllocationAmount + getAmountDiff
                                UpdateAPAllocation.AllocationAmount = MustBeAllocationAmount
                                _DMCM = Me.PaymentProformaEntries.CreateDMCM_AP(UpdateAPAllocation, .AllocationDate, DailyInterestRate, Me.AMParticipantsList)
                                If Not _DMCM Is Nothing And _DMCM.DMCMNumber > 0 Then
                                    Me.ListofDMCMAP.Add(_DMCM)
                                    .GeneratedDMCM = _DMCM.DMCMNumber
                                End If
                            Else
                                .AllocationAmount += getAmountDiff
                            End If
                        End If
                        .NewEndingBalance = .EndingBalance - .AllocationAmount
                    End With
                End If
            Else
                Dim UpdateAPAllocation As APAllocation = (From x In UpdateAPAllocationList Where x.WESMBillSummaryNo = GetHighiestAmount.WESMBillSummaryNo Select x).FirstOrDefault
                With UpdateAPAllocation
                    Dim DMCMItem = (From x In Me.ListofDMCMAP Where x.DMCMNumber = .GeneratedDMCM Select x).FirstOrDefault
                    If Not DMCMItem Is Nothing Then
                        DMCMItem.VATExempt += AmountDiff
                        DMCMItem.TotalAmountDue += AmountDiff
                        For Each ItemDetail In DMCMItem.DMCMDetails
                            If ItemDetail.Credit = 0 Then
                                ItemDetail.Debit += AmountDiff
                            Else
                                ItemDetail.Credit += AmountDiff
                            End If
                        Next
                        .AllocationAmount += AmountDiff
                    Else
                        'lance
                        If AmountDiff <> 0 And .AllocationAmount = 0 Then
                            Dim _DMCM As New DebitCreditMemo
                            Dim MustBeAllocationAmount As Decimal = .AllocationAmount + AmountDiff
                            UpdateAPAllocation.AllocationAmount = MustBeAllocationAmount
                            _DMCM = Me.PaymentProformaEntries.CreateDMCM_AP(UpdateAPAllocation, .AllocationDate, DailyInterestRate, Me.AMParticipantsList)
                            If Not _DMCM Is Nothing And _DMCM.DMCMNumber > 0 Then
                                Me.ListofDMCMAP.Add(_DMCM)
                                .GeneratedDMCM = _DMCM.DMCMNumber
                            End If
                        Else
                            .AllocationAmount += AmountDiff
                        End If
                    End If
                    .NewEndingBalance = .EndingBalance - .AllocationAmount
                End With
            End If
        End If
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
                    Me._EnergyAPAllocationList.Add(AdjustedAPAllocation)
                Case EnumChargeType.EV
                    Me._VATonEnergyAPAllocationList.Add(AdjustedAPAllocation)
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
