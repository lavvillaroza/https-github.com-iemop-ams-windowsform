'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 25, 2011
'Development Group:      Software Development and Support Division
'Description:            List of all enumeration
'Arguments/Parameters:  
'Files/Database Tables:       
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   August 25, 2011         Vladimir E. Espiritu            Added EnumReportType
'   September 15, 2011      Vladimir E. Espiritu            Added EnumChargeType
'   December 27, 2011       Vladimir E. Espiritu            Added EnumCollectionStatus
'   January 04, 2012        Vladimir E. Espiritu            Added EnumCollectionType
'   February 02, 2012       Vladimir E. Espiritu            Added EnumFileType
'   March 28, 2012          Vladimir E. Espiritu            Added EnumIsPosted
'   June 19, 2012           Vladimir E. Espiritu            Added EnumDMCMTransactionType
'   May 6, 2015             Lance Arjay V. Villaroza        Added EnumClassificationForFitType

Public Enum EnumEnergyWithholdStatus
    NotApplicable = 0
    UnpaidEWT = 1
    PaidEWT = 2
End Enum

Public Enum EnumIsAllocated
    NotAllocated = 1
    Allocated = 2
End Enum

Public Enum EnumChargeType
    MF = 0
    MFV = 1
    E = 2
    EV = 3
End Enum

Public Enum EnumWESMTransDetailsSummaryStatus
    CURRENT
    ADDED
    UPDATED
End Enum

Public Enum EnumReportType
    None
    WESMBillReport
    WESMBillSummaryOnly
    WESMBillSummaryDetails
End Enum

Public Enum EnumImportFileType
    WESMInvoice
    WESMSalesAndPurchases
End Enum

Public Enum EnumCollectionStatus
    Cancelled
    NotAllocated
    Allocated
    PreAllocated
End Enum

Public Enum EnumCollectionType
    WithholdingTaxOnMF = 0
    WithholdingVatOnMF = 1
    WithholdingTaxOnDefaultInterest = 2
    WithholdingVatOnDefaultInterest = 3
    DefaultInterestOnMF = 4
    DefaultInterestOnVatOnMF = 5
    MarketFees = 6
    VatOnMarketFees = 7
    DefaultInterestOnEnergy = 8
    Energy = 9
    DefaultInterestOnVatOnEnergy = 10
    VatOnEnergy = 11
    TransferToReplenishment = 12
    TransferToExcessCollection = 13
    TransferToHeldCollection = 14
    TransferToPEMCAccount = 15
    AppliedHeldCollection = 16
    WithholdingTaxonEnergy = 17
    TransferToPRDrawDown = 18
    NoTransaction = 19
    TransferToFinPen = 20
    EnergyAREndingBalanceAdjustment = 21
End Enum

Public Enum EnumAllocationType
    Manual
    Automatic
End Enum

Public Enum EnumPaymentNewType
    WithholdingTaxOnMF = 0
    WithholdingVatOnMF = 1
    WithholdingTaxOnDefaultInterest = 2
    WithholdingVatOnDefaultInterest = 3
    DefaultInterestOnMF = 4
    DefaultInterestOnVatOnMF = 5
    MarketFees = 6
    VatOnMarketFees = 7
    DefaultInterestOnEnergy = 8
    Energy = 9
    DefaultInterestOnVatOnEnergy = 10
    VatOnEnergy = 11
    TransferToReplenishment = 12
    WithholdingTaxOnEnergy = 17
    NoTransaction = 18
    FitEnergy = 19
    FitMF = 20
    DeferredAppliedEnergy = 21
    DeferredAppliedVAT = 22
    EnergyAPEndingBalanceAdjustment = 23
End Enum

Public Enum EnumDMCMCode
    MFMFV = 1
    MFMFVDI = 2
    MFEWTWVAT = 3
    MFEWTWVATDI = 4
    E = 5
    EDI = 6
    EV = 7
End Enum

Public Enum EnumPaymentType
    UnpaidMFWHTax = 0
    UnpaidMFWHVAT = 1
    UnpaidMFDefault = 2
    UnpaidMFVDefault = 3
    UnpaidMF = 4
    UnpaidMFV = 5
    PaymentEnergy = 6
    PaymentEnergyVAT = 7
    PaymentMF = 8
    PaymentMFV = 9
    OffsetAmountEnergy = 10
    OffsetAmountVATEnergy = 11
    OffsetAllocatedEnergy = 12
    OffsetAllocatedVAT = 13
    OffsetSpecialCase = 14
    OffsetToCurrentReceivable = 15
    OffsetToCurrentReceivableEnergy = 16
    OffsetToCurrentReceivableEnergyDefault = 17
    OffsetToCurrentReceivableVAT = 18
    OffsetToCurrentReceivableMF = 19
    OffsetToCurrentReceivableMFV = 20
    OffsetOfPreviousReceivable = 21
    OffsetOfPreviousReceivableEnergy = 22
    OffsetOfPreviousReceivableEnergyDefault = 23
    OffsetOfPreviousReceivableVAT = 24
    OffsetFromPrudentialRequirement = 25
    SetAsDeferredForEnergy = 26
    SetAsDeferredForVAT = 27
    ReturnToParticipant = 28
    WESMBillOffsetting = 29
    WHTaxDefault = 30
    WHVATDefault = 31
    DefaultAllocation = 32
    DeferredAppliedEnergy = 33
    DeferredAppliedVAT = 34
End Enum

Public Enum EnumParticipantPaymentType
    Check = 0
    EFT = 1
    None = 2
    SetAsDeferred = 3
End Enum

Public Enum EnumFileType
    Energy = 0
    MarketFees = 1
End Enum

Public Enum EnumStatus
    InActive = 0
    Active = 1
End Enum

Public Enum EnumGenLoad
    G
    L
    DCC
End Enum

Public Enum EnumPostType
    Uploaded
    Offset
    Collection
    Payment
    Adjustment
    EarnedInterest
End Enum

Public Enum EnumSaveType
    SaveNew
    SaveUpdate
    SaveDeleted
End Enum

Public Enum EnumPostedType
    U 'Uploading
    O 'P2P/C2C Offsetting
    C 'Collection Process
    P  ' Payment Process
    PA   ' Initial Allocation
    PEFT ' For EFT/Check/FTF
    PD ' Payment Default Interest
    PRR ' Prudential Replenishment
    PRI ' Prudential Interest
    PRTI 'Prudential Transfer Interest
    NSSI 'NSS Interest
    DC ' Daily Collection
    DCC ' Daily Cancelled Collection
    MDMCM 'Manual DMCM
    IES ' Interest Earned Settlement
    BCS ' Bank Charges Settlement
    SPAC 'WESM Bill Summary Invoices Closing for SPA
    SPASU 'SPA Setup
    FPA ' Financial Penatly
    ADJWHTAX 'Amount adjusted in withholding tax
    PRREF 'PR Refund Setup
    PRREFFTF ' PR REfund FTF
    PRREFEFT 'PR EFT
End Enum

Public Enum EnumSummaryType
    INV = 0
    DMCM = 1
    SPA = 2
End Enum

Public Enum EnumJVType
    DMCM = 1
    EFT = 2
    CHECKS = 3
End Enum

Public Enum EnumDocCode
    CN
    DMCM
    JV
End Enum

Public Enum EnumIDType
    P
    C
End Enum

Public Enum EnumPrudentialTransType
    Drawdown = 1
    Replenishment = 2
    InterestAmount = 3
    TransferInterestAmount = 4
    PRBeginningBalance = 5
    PRIntBegginingBalance = 6
    PRRefund = 7
End Enum

Public Enum EnumCollectionCategory    
    Cash = 1
    Drawdown = 2
    Offset = 3
    NoTrans = 4
    DeferredEnergy = 5
    DeferredVatonEnergy = 6
End Enum

Public Enum EnumIsPosted
    NotPost = 0
    Posted = 1
End Enum

Public Enum EnumParticipantClassification
    NonGovernment
    Government
End Enum

Public Enum EnumFTFTransType
    DrawDown = 1
    Replenishment = 2
    TransferPEMCAccount = 3
    TransferNSSToSTL = 4
    TransferSTLToNSS = 5
    TransferMarketFeesToPEMC = 6
    TransferMarketFeesToSTL = 7
    EnergyWithholdingTax = 8
    PRRefund = 9
End Enum

Public Enum EnumCollectionMonitoringType
    AppliedHeldCollection = 1
    TransferToHeldCollection = 2
    TransferToExcessCollection = 3
    TransferToPRReplenishment = 4
    TransferToPRDrawdown = 5
    TransferToPEMCAccount = 6
End Enum

Public Enum EnumRFPDetailsType
    Payment = 1
    Collection = 2
End Enum

Public Enum EnumBankAccountType
    Savings = 1
    Current = 2
    Combo = 3
End Enum

Public Enum EnumDMCMTransactionType
    WESMBillP2PC2COffsetting = 1
    WESMBillP2COffsetting = 2
    CollectionDrawdown = 3
    CollectionSetupWithholding = 4
    CollectionSetupWithholdingDefaultInterest = 5
    CollectionSetupMFandMFVatDefaultInterest = 6
    CollectionSetupEnergyDefaultInterest = 7
    PaymentSetupWithholding = 8
    PaymentMFandMFVATDefaultInterest = 9
    PaymentSetupEnergyDefaultInterest = 10
    PaymentOffsettingOfReceivableEnergy = 11
    PaymentOffsettingOfReceivableVATOnEnergy = 12
    PaymentOffsettingOfReceivableMF = 13
    PaymentOffsettingOfReceivableMFV = 14
    PaymentChildToParentOffsetting = 15
    PaymentDefaultInterestAllocation = 16
    PaymentSetupOfDefaultInterestMFMFV = 17
    PaymentDefaultInterestAllocation2 = 18
    PaymentOffsettingOfReceivableEnergyDefault = 19
    PaymentOffsettingOfReceivableEnergyClearing = 20
    PaymentOffsettingOfReceivableVATEnergyClearing = 21
    PrudentialInterest = 22
    NSSInterest = 23
    ManualDMCM = 24
    PaymentPrudentialReplenishment = 25
    STLInterest = 26
    PaymentSetupClearingOnMFAndMFV = 27
    PaymentSetupClearingOnEWTAndWVAT = 28
    PaymentSetupClearingOnMFAndMFVDefaultInterest = 29
    PaymentSetupClearingOnEWTAndWVATDefaultInterest = 30
    PaymentSetupClearingAROnEnergyAndDefaultInterest = 31
    PaymentSetupClearingAROnVATonEnergy = 32
    PaymentSetupOffsettingAROfDefaultInterestOnEnergy = 33
    PaymentSetupClearingAPOfEnergyAndDefaultInterest = 34
    PaymentSetupClearingAPOfVATonEnergy = 35
    PaymentSetupOffsettingAPOfDefaultInterestOnEnergy = 36
    PaymentSetupClearingEnergyWithholdingTax = 37
    SPAClosingAccountReceivable = 38
    SPAClosingAccountPayable = 39
    SPASetupAccountReceivable = 40
    SPASetupAccountPayable = 41
    WHTAXAdjustmentSetupClearingAPOnEnergy = 42
    WHTAXAdjustmentSetupClearingAROnEnergy = 43    
    WHTAXAdjustmentSetupClearingAPOnMarketFees = 44
    WHTAXAdjustmentSetupClearingAROnEnergyAddBack = 45
    WHTAXAdjustmentSetupClearingAPOnEnergyAddBack = 46
    PaymentSetupForMFAPPaidByIEMOPAccount = 47

End Enum

Public Enum EnumPaymentAllocationType
    Energy = 1
    DefaultInterestOnEnergy = 2
    VATonEnergy = 3
End Enum

Public Enum EnumDMCMSummaryType
    Offsetting = 1
    DMCMSummary = 2
    TotalOffsettingCM = 3
    TotalOffsettingDA = 4
    TotalNoWBDM = 5
    TotalNoWBAPV = 6
    TotalDefIntDM = 7
    TotalDefIntAPV = 8
End Enum

Public Enum EnumQuarterlyPeriod
    FirstQuarter = 1
    SecondQuarter = 2
    ThirdQuarter = 3
    FourthQuarter = 4
End Enum

Public Enum EnumCNFlag
    BeforeCollection = 1
    AfterCollection = 2
End Enum

Public Enum EnumCollectionPostType
    PostDaily = 1
    PostManual = 2
End Enum

Public Enum EnumCollectionTypeCode
    WTMF
    WVMF
    WTDI
    WVDI
    DIMF
    DIVMF
    MF
    MFV
    DIE
    E
    VE
End Enum

Public Enum EnumCollectionPaymentTransactionType
    Collection = 1
    Payment = 2
End Enum

Public Enum EnumSTLCategory
    BeginningBalances = 1
    CurrentTransaction = 2
    Offsetting = 3
    Collection = 4
    Payment = 5
    Withholding = 6
    EndingBalances = 7
End Enum

Public Enum EnumSTLTransactionType
    AREnergy = 1
    ARVAT = 2
    ARDefaultEnergy = 3
    ARMF = 4 'MF + MFV
    ARDefaultMF = 5
    AROthers = 6
    APEnergy = 7
    APVAT = 8
    APDefaultEnergy = 9
    APMF = 10 'MF + MFV
    APDefaultMF = 11
    APOthers = 12
    WTax = 13
    WVAT = 14
    DefaultWTax = 15
    DefaultWVAT = 16
    PRReplenishment = 17
End Enum

Public Enum EnumCheckType
    BPI = 1
    SCB = 2
End Enum

Public Enum EnumCheckStatus
    SystemGenerated = 1
    GeneratedCheck = 2
    Released = 3
    Cleared = 4
    Cancelled = 5
    ReplacementForCancelled = 6
End Enum

Public Enum EnumMarginCallStatus
    NotFinal = 0
    Final = 1
End Enum

Public Enum EnumSummaryDefaultColumns
    BillNo = 1
    InterestRate = 2
    DefaultPeriod = 3
    NoOfDaysDefault = 4
    AmountDefaulted = 5
    WHTaxWHVat = 6
    DefaultAmount = 7
End Enum

Public Enum EnumBankReconType
    Settlement = 1
    Prudential = 2
    NSS = 3
End Enum

Public Enum EnumBankReconTransactionType
    Collection = 1
    PaymentEFT = 2
    PaymentCheck = 3
    NSS = 4
    PR = 5
    MarketFees = 6
    Interest = 7
    BankCharges = 8
    OutstandingCheck = 9
End Enum

Public Enum EnumAMSModulesFinal
    LibAccountCodeWindow = 1
    LibBATCodeWindow = 2
    LibCalendarBillingWindow = 3
    LibChargeIDWindow = 4
    LibDailyInterestRateWindow = 5
    LibSignatoriesMainteWindow = 6
    LibParticipantInfoWindow = 7
    LibParticipantPACEOWindow = 8
    LibWESMBillChangeParentIDWindow = 9
    SystemParamWindow = 10
    SAPSOAWindow = 11
    SAPUploadWESMBillWindow = 12
    SAPUploadWESMBillSAPWindow = 13
    SAPUploadWESMBillFetchFromCRSSDBWindow = 14
    SAPWESMBillOffsetingWindow = 15
    SAPWESMBillJVPostingWindow = 16
    CollEntryAllocationWindow = 17
    CollEditCollectionDefaultInterest = 18
    CollPostDailyCollectionsWindow = 19
    CollPostManualCollectionsWindow = 20
    PayAllocateCollectionsWindow = 21
    PayViewAllocatedCollectionsWindow = 22
    MonEarnedInterestWindow = 23
    MonNSSWindow = 24
    MonPrudentialRepWindow = 25
    MonPrudentialIntWindow = 26
    MonPrudentialTransIntWindow = 27
    MonSPAWindow = 28
    MonWhTaxAdjPerInvoiceWindow = 29
    MonWhTaxWhtaxAdjustmentWindow = 30
    MonWESMBillsExemptionWindow = 31
    InqRepAgingReportWindow = 32
    InqRepBankReconStmntWindow = 33
    InqRepBIRAccessToRecordWindow = 34
    InqRepCashSummaryReportWindow = 35
    InqRepChecksWindow = 36
    InqRepCollectionSummaryWindow = 37
    InqRepDCashCollSummaryWindow = 38
    InqRepDebitCreditMemoWindow = 39
    InqRepDefaultNoticeWindow = 40
    InqRepDeferredPaymentWindow = 41
    InqRepEFTWindow = 42
    InqRepFundTransferFormWindow = 43
    InqRepGeneralLedgerWindow = 44
    InqRepJVWindow = 45
    InqRepMFSummaryWindow = 46
    InqRepMAPEWTCertifWindow = 47
    InqRepNoticeofDrawDownWindow = 48
    InqRepOfficialReceiptWindow = 49
    InqRepPaymentDetailsWindow = 50
    InqRepPrudentialWindow = 51
    InqRepRequestForPaymentWindow = 52
    InqRepSettleNoticeWindow = 53
    InqRepSOAWindow = 54
    InqRepSubsidiaryLedgerWindow = 55
    InqRepSummaryForABWindow = 56
    InqRepSummaryofDefaultWindow = 57
    InqRepSummaryofOBWindow = 58
    InqRepWESMInvWindow = 59
    InqRepWESMBillsWindow = 60
    InqRepWESMBillsSummary = 61
    Logs = 62
    MonRefundPRWindow = 63
    ChangeWESMBillBP = 64
    SAPWESMBillAggregateMapping = 65
End Enum

Public Enum EnumHeaderType
    Yes = 1
    No = 2
End Enum

Public Enum EnumNSSRAReportCategory
    Previous = 1
    Current = 2
    Total = 3
End Enum

Public Enum EnumORTransactionType
    Collection = 0
    Replenishment = 1
    PaymentEnergy = 2
    PaymentMarketFees = 3
    PaymentVAT = 4
    PaymentEnergyAndVAT = 5
    Energy = 6
    MarketFees = 7
    Excess = 8
    Held = 9
    FinPenAmount = 10
End Enum

Public Enum EnumWESMBillSalesAndPurchasedTransType
    Uploaded = 1
    Computed = 2
End Enum

Public Enum EnumDocumentType
    NONE = 0
    INV = 1
    DMCM = 2
End Enum

Public Enum EnumDMCMComputed
    NotCompute = 0
    Compute = 1
End Enum

Public Enum EnumLogType
    Loading
    Saving
    Editing
    Viewing
    Retrieving
    Printing
    Exporting
    Downloading
    Searching
    SuccesffullySaved
    SuccessfullyEdited
    SuccessfullyDownloaded
    SuccessfullyUploaded
    SuccessfullyPrinted
    SuccessfullyAccessed
    SuccssfullyDeleted
    SuccessfullyPosted
    SuccessfullyGeneratedReport
    SuccessfullyExported
    SuccessfullySearched
    SuccessfullyUntag
    SuccessfullyAllocated
    SuccessfullyRollBack
    ErrorInAllocation
    ErrorInDeleting
    ErrorInSaving
    ErrorInEditing
    ErrorInRetrieving
    ErrorInExporting
    ErrorInDownloading
    ErrorInPrinting
    ErrorInAccessing
    ErrorInAddingNewRecord
    ErrorInRollBack
    ErrorInUnTagging
    ErrorInPosting
    ErrorInUploading
    ErrorInRefreshing
    ErrorInGeneratingReport
    ErrorInSearching
    ErrorInViewing
    ErrorInLoading
    NoAccess
End Enum

Public Enum EnumColorCode
    Red
    Green
    Blue
End Enum

Public Enum BIRDocumentsType
    FinalStatement
    DMCM
    OfficialReceipt
    JournalVoucher
    FTF
    EFT
    RFP
    CHECK
    SOA
    CV
End Enum

Public Enum EnumSOAMainType
    CollectionEnergy
    CollectionVAT
    CollectionMF
    CollectionDefault
    PaymentEnergy
    PaymentVAT
    PaymentMF
    PaymentDefault
    OutstandingE
    OutstandingVAT
    OutstandingMF
    OutstandingDefault
End Enum

Public Enum EnumPostedTypeStatus
    Cancelled = 0
    NotPosted = 1
    Posted = 2
End Enum

Public Enum EnumDeferredType
    OutstandingBalance = 0
    Deferral = 1
    Remitted = 2
    offset = 3
End Enum

Public Enum EnumBalanceType
    AR = 1
    AP = 2
End Enum

Public Enum EnumSPAStatus
    Settled = 1
    OnGoing = 2
End Enum

Public Enum EnumBRTransactionType
    CASH = 1
    DRAWDOWN = 2
End Enum

Public Enum EnumWESMBillImportType
    WF = 1
    WAD = 2
    WAC = 3
End Enum

Public Enum EnumCollectionReportType
    Seller = 1
    Buyer = 2
End Enum

