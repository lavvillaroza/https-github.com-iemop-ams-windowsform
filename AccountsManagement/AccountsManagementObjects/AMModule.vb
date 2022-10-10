Public Module AMModule
    'Cash Accounting Codes
    Public CashinBankPEMCCode As String
    Public CashinBankPrudentialCode As String
    Public CashinBankAccountSurplusCode As String
    Public CashInbankSettlementcode As String

    'AR Accounting Codes
    Public CreditCode As String
    Public EWTReceivable As String
    Public EWVReceivableCode As String
    Public DefaultInterestCode As String

    'AP Accounting Codes
    Public DebitCodeNonWESM As String
    Public DebitCode As String
    Public APStaleChecksCode As String
    Public APUnreleasedChecksCode As String
    Public DeferredPaymentCode As String
    Public UnappliedcollectionCode As String
    Public EWTPayable As String    

    'PR/STL/NSS Accounting Codes
    Public PRWESMCode As String
    Public NSSCode As String
    Public NSSVATCode As String
    Public InterestPayableSTLCode As String
    Public InterestPayablePRCode As String
    Public InterestPayableNSSCode As String

    'Clearing Account
    Public ClearingAccountCode As String

    'MF Accounting Codes
    Public MarketTransFeesCode As String
    Public MarketFeesOutputTaxCode As String
    Public DefaultInterestMarketFees As String

    'Others
    Public ConnectionString As String
    Public ConnectionStringCRSS As String
    Public DeferredPayment As Decimal
    Public MinimumAmountToBeOffset As Decimal
    Public NSSRACode As String
    Public VatValue As Decimal
    Public VatCode As String
    Public WESMBillNumberPrefix As String

    'Bank Account Numbers
    Public PrudentialBankAccountNo As String
    Public SettlementBankAccountNo As String
    Public PemcBankAccountNo As String
    Public NSSBankAccountNo As String

    'Margin Call
    Public MNEMultiplier As Decimal
    Public TradingLimitMultiplier As Decimal

    'Users
    Public UserName As String
    Public FullName As String
    Public Position As String

    'ManualDMCM
    Public ManualDMCMMarketFees As String
    Public ManualDMCMVATonMarketFees As String
    Public ManualDMCMEnergy As String
    Public ManualDMCMVATonEnergy As String

    'BIR in Final Statement, OR, DMCM, JV
    Public BIRCASPermit As String
    Public BIRPermitNumber As String
    Public BIRDateIssued As String
    Public BIRValidUntil As String
    Public FSNumberPrefix As String
    Public DMCMNumberPrefix As String
    Public ORNumberPrefix As String
    Public JVNumberPrefix As String
    Public FTFNumberPrefix As String
    Public RFPNumberPrefix As String
    Public CheckNumberPrefix As String
    Public SOANumberPrefix As String
    Public CVNumberPrefix As String

    'Can Edit Default Interest in Collection Allocation
    Public IsAllowedToEditDefaultInterest As Boolean = True

    Public SystemDate As Date

    'BIR EWT Payable Report
    Public CompanyShortName As String
    Public CompanyFullName As String
    Public CompanyAddress As String
    Public CompanyTinNumber As String
    Public CompanyTinExt As String
    Public CompanyRDONumber As String
    Public AlphaTypeHeader As String
    Public AlphaTypeDetails As String
    Public AlphaTypeControl As String
    Public FormTypeCodeHeader As String
    Public FormTypeCodeDetails As String
    Public FormTypeCodeControl As String
    Public FormTypeCodeBaseNumber As String
    Public CompanyBDOAccountName As String
    Public CompanyAccountNumber As String

    'Transco
    Public ParentFitID As String

    '13-digit Debit A/C No.
    Public t13DigitDebitACNo As String

    'Selection of due Date in WESM Bill Offsetting
    Public WESMBillOffsetFromDate As String
    Public WESMBillOffsetToDate As String

    Public BIRWHTAgent As String

    Public OffsettingLimit As Integer

    Public DateOfNoSP As Date

    Public FITParticipantCode As String

    Public DefaultInterestDivisorValue As Decimal

    Public OldWESMBILL As Long

    Public SystemVersion As String

    Public PrintingMaxLimit As Integer

    Public RegionType As String

    Public BRImplementedBPNo As Long
    Public BIRRulingPrefix As String

    Public AmountDiffValue As Decimal

End Module
