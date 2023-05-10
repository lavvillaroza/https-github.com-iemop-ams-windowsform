'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             FuncAutoCollectionAllocationResult
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     April 2, 2012
'Development Group:      Software Development and Support Division
'Description:            Class for re-usable functions
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
Public Class FuncAutoPaymentAllocationResult

#Region "Initialization/Constructor"
    Public Sub New()
        Me._PaymentAllocPerBillPeriod = New List(Of Payment)
        Me._PaymentAllocPerParticipant = New List(Of PaymentAllocationParticipant)
        Me._PaymentPerAccount = New List(Of PaymentAllocationAccount)
        Me._UpdateWESMSummary = New List(Of WESMBillSummary)
        Me._NewWESMSummary = New List(Of WESMBillSummary)
        Me._DebitCreditMemo = New List(Of DebitCreditMemo)
        Me._OffsettingDetails = New List(Of OffsetP2PC2CDetails)
        Me._WESMGPPosted = New List(Of WESMBillGPPosted)
        Me._JournalVoucher = New List(Of JournalVoucher)
        Me._DeferredPayments = New List(Of AMParticipants)
        Me._lstAccountCodes = New List(Of AccountingCode)
        Me._CalendarBP = New List(Of CalendarBillingPeriod)
        Me._AllocationDate = DateTime.Now
        Me._ParticipantForEFT = New List(Of EFT)
        Me._ForEFTNew = New List(Of EFT)
        Me._ForEFTUpdate = New List(Of EFT)
        Me._RequestForPayment = New List(Of RequestForPayment)
        Me._RFPDetails = New List(Of RequestForPaymentDetails)
        Me._FundTransferForm = New List(Of FundTransferFormMain)
        Me._CollectionAllocDetails = New List(Of CollectionAllocation)
        Me._lstCollection = New List(Of Collection)
        Me._PrudentialHistory = New List(Of PrudentialHistory)
        Me._PrudentialReq = New List(Of Prudential)
        Me._lstAllocationSummary = New List(Of PaymentAllocation)
        Me._DMCMSummary = New List(Of DebitCreditMemoSummary)
        Me._lstOfficialReceipt = New List(Of OfficialReceiptMain)
        Me._dicPaymentTransType = New Dictionary(Of EnumPaymentType, String)
    End Sub
#End Region

#Region "Payment Allocation > BP"

    Private _PaymentAllocPerBillPeriod As List(Of Payment)
    Public Property lstPerBillPeriodAllocation() As List(Of Payment)
        Get
            Return _PaymentAllocPerBillPeriod
        End Get
        Set(ByVal value As List(Of Payment))
            _PaymentAllocPerBillPeriod = value
        End Set
    End Property

#End Region

#Region "Payment Allocation > BP > Participant"

    Private _PaymentAllocPerParticipant As List(Of PaymentAllocationParticipant)
    Public Property lstPerParticipantAllocation() As List(Of PaymentAllocationParticipant)
        Get
            Return _PaymentAllocPerParticipant
        End Get
        Set(ByVal value As List(Of PaymentAllocationParticipant))
            _PaymentAllocPerParticipant = value
        End Set
    End Property

#End Region

#Region "Payment Allocation > BP > Participant - Viewing"

    Private _PaymentAllocPerParticipantViewing As List(Of PaymentAllocationParticipant)
    Public Property lstPerParticipantAllocationViewing() As List(Of PaymentAllocationParticipant)
        Get
            Return _PaymentAllocPerParticipantViewing
        End Get
        Set(ByVal value As List(Of PaymentAllocationParticipant))
            _PaymentAllocPerParticipantViewing = value
        End Set
    End Property

#End Region

#Region "Payment Allocation > BP > Participant > Summary Details"

    Private _PaymentPerAccount As List(Of PaymentAllocationAccount)
    Public Property lstPerSummaryAllocation() As List(Of PaymentAllocationAccount)
        Get
            Return _PaymentPerAccount
        End Get
        Set(ByVal value As List(Of PaymentAllocationAccount))
            _PaymentPerAccount = value
        End Set
    End Property

#End Region

#Region "Update WESM Bills Summary"

    Private _UpdateWESMSummary As List(Of WESMBillSummary)
    Public Property UpdateWESMSummary() As List(Of WESMBillSummary)
        Get
            Return _UpdateWESMSummary
        End Get
        Set(ByVal value As List(Of WESMBillSummary))
            _UpdateWESMSummary = value
        End Set
    End Property

#End Region

#Region "New WESM Bills Summary"

    Private _NewWESMSummary As List(Of WESMBillSummary)
    Public Property NewWESMSummary() As List(Of WESMBillSummary)
        Get
            Return _NewWESMSummary
        End Get
        Set(ByVal value As List(Of WESMBillSummary))
            _NewWESMSummary = value
        End Set
    End Property

#End Region

#Region "DebitCreditMemo"

    Private _DebitCreditMemo As List(Of DebitCreditMemo)
    Public Property lstDebitCreditMemo() As List(Of DebitCreditMemo)
        Get
            Return _DebitCreditMemo
        End Get
        Set(ByVal value As List(Of DebitCreditMemo))
            _DebitCreditMemo = value
        End Set
    End Property

#End Region

#Region "Offsetting Table"

    Private _OffsettingDetails As List(Of OffsetP2PC2CDetails)
    Public Property lstOffsetDetails() As List(Of OffsetP2PC2CDetails)
        Get
            Return _OffsettingDetails
        End Get
        Set(ByVal value As List(Of OffsetP2PC2CDetails))
            _OffsettingDetails = value
        End Set
    End Property

#End Region

#Region "For Posting in GreatPlains Interface"

    Private _WESMGPPosted As List(Of WESMBillGPPosted)
    Public Property lstForPostingGP() As List(Of WESMBillGPPosted)
        Get
            Return _WESMGPPosted
        End Get
        Set(ByVal value As List(Of WESMBillGPPosted))
            _WESMGPPosted = value
        End Set
    End Property

#End Region

#Region "Journal Voucher"

    Private _JournalVoucher As List(Of JournalVoucher)
    Public Property lstJournalVoucher() As List(Of JournalVoucher)
        Get
            Return _JournalVoucher
        End Get
        Set(ByVal value As List(Of JournalVoucher))
            _JournalVoucher = value
        End Set
    End Property

#End Region

#Region "List for Deferred"

    Private _DeferredPayments As List(Of AMParticipants)
    Public Property lstDeferredPayment() As List(Of AMParticipants)
        Get
            Return _DeferredPayments
        End Get
        Set(ByVal value As List(Of AMParticipants))
            _DeferredPayments = value
        End Set
    End Property

#End Region

#Region "Collection Allocation Details"

    Private _CollectionAllocDetails As List(Of CollectionAllocation)
    Public Property CollectionAllocationDetails() As List(Of CollectionAllocation)
        Get
            Return _CollectionAllocDetails
        End Get
        Set(ByVal value As List(Of CollectionAllocation))
            _CollectionAllocDetails = value
        End Set
    End Property

#End Region

#Region "Collections"

    Private _lstCollection As List(Of Collection)
    Public Property lstCollection() As List(Of Collection)
        Get
            Return _lstCollection
        End Get
        Set(ByVal value As List(Of Collection))
            _lstCollection = value
        End Set
    End Property

#End Region

#Region "Account Codes"

    Private _lstAccountCodes As List(Of AccountingCode)
    Public Property lstAccountCodes() As List(Of AccountingCode)
        Get
            Return _lstAccountCodes
        End Get
        Set(ByVal value As List(Of AccountingCode))
            _lstAccountCodes = value
        End Set
    End Property

#End Region

#Region "Calendar Bill Period"

    Private _CalendarBP As List(Of CalendarBillingPeriod)
    Public Property CalendarBillPeriod() As List(Of CalendarBillingPeriod)
        Get
            Return _CalendarBP
        End Get
        Set(ByVal value As List(Of CalendarBillingPeriod))
            _CalendarBP = value
        End Set
    End Property

#End Region

#Region "Collection Allocation Date"

    Private _AllocationDate As Date
    Public Property AllocationDate() As Date
        Get
            Return _AllocationDate
        End Get
        Set(ByVal value As Date)
            _AllocationDate = value
        End Set
    End Property

#End Region

#Region "Participants for EFT"

    Private _ParticipantForEFT As List(Of EFT)
    Public Property ParticipantForEFT() As List(Of EFT)
        Get
            Return _ParticipantForEFT
        End Get
        Set(ByVal value As List(Of EFT))
            _ParticipantForEFT = value
        End Set
    End Property

#End Region

#Region "Participants for EFT - Updating"

    Private _ForEFTUpdate As List(Of EFT)
    Public Property ForEFTUpdate() As List(Of EFT)
        Get
            Return _ForEFTUpdate
        End Get
        Set(ByVal value As List(Of EFT))
            _ForEFTUpdate = value
        End Set
    End Property

#End Region

#Region "Participants for EFT - New"

    Private _ForEFTNew As List(Of EFT)
    Public Property ForEFTNew() As List(Of EFT)
        Get
            Return _ForEFTNew
        End Get
        Set(ByVal value As List(Of EFT))
            _ForEFTNew = value
        End Set
    End Property

#End Region

#Region "Request for Payment"
    Private _RequestForPayment As List(Of RequestForPayment)
    Public Property RequestForPayment() As List(Of RequestForPayment)
        Get
            Return _RequestForPayment
        End Get
        Set(ByVal value As List(Of RequestForPayment))
            _RequestForPayment = value
        End Set
    End Property

#End Region

#Region "Request for Payment - details"
    Private _RFPDetails As List(Of RequestForPaymentDetails)
    Public Property RFPDetails() As List(Of RequestForPaymentDetails)
        Get
            Return _RFPDetails
        End Get
        Set(ByVal value As List(Of RequestForPaymentDetails))
            _RFPDetails = value
        End Set
    End Property

#End Region

#Region "Fund Transfer Form - Main"

    Private _FundTransferForm As List(Of FundTransferFormMain)
    Public Property FundTransferform() As List(Of FundTransferFormMain)
        Get
            Return _FundTransferForm
        End Get
        Set(ByVal value As List(Of FundTransferFormMain))
            _FundTransferForm = value
        End Set
    End Property

#End Region

#Region "Prudential Requirements"

    Private _PrudentialReq As List(Of Prudential)
    Public Property PrudentialReq() As List(Of Prudential)
        Get
            Return _PrudentialReq
        End Get
        Set(ByVal value As List(Of Prudential))
            _PrudentialReq = value
        End Set
    End Property

#End Region

#Region "Prudential History"

    Private _PrudentialHistory As List(Of PrudentialHistory)
    Public Property PrudentialHistory() As List(Of PrudentialHistory)
        Get
            Return _PrudentialHistory
        End Get
        Set(ByVal value As List(Of PrudentialHistory))
            _PrudentialHistory = value
        End Set
    End Property

#End Region

#Region "Allocation Summary"

    Private _lstAllocationSummary As List(Of PaymentAllocation)
    Public Property lstAllocationSummary() As List(Of PaymentAllocation)
        Get
            Return _lstAllocationSummary
        End Get
        Set(ByVal value As List(Of PaymentAllocation))
            _lstAllocationSummary = value
        End Set
    End Property

#End Region

#Region "DebitCredit Memo Summary"

    Private _DMCMSummary As List(Of DebitCreditMemoSummary)
    Public Property DMCMSummary() As List(Of DebitCreditMemoSummary)
        Get
            Return _DMCMSummary
        End Get
        Set(ByVal value As List(Of DebitCreditMemoSummary))
            _DMCMSummary = value
        End Set
    End Property

#End Region

#Region "Payment Batch Code"
    Private _PaymentBatchCode As String
    Public Property PaymentbatchCode() As String
        Get
            Return _PaymentBatchCode
        End Get
        Set(ByVal value As String)
            _PaymentBatchCode = value
        End Set
    End Property

#End Region

#Region "Official Receipt"
    Private _lstOfficialReceipt As List(Of OfficialReceiptMain)
    Public Property lstOfficialReceipt() As List(Of OfficialReceiptMain)
        Get
            Return _lstOfficialReceipt
        End Get
        Set(ByVal value As List(Of OfficialReceiptMain))
            _lstOfficialReceipt = value
        End Set
    End Property
#End Region

    Private _dicPaymentTransType As Dictionary(Of EnumPaymentType, String)
    Public ReadOnly Property dicPaymentTransType() As Dictionary(Of EnumPaymentType, String)
        Get
            _dicPaymentTransType = New Dictionary(Of EnumPaymentType, String)
            If _dicPaymentTransType.Count = 0 Then
                'Withholding Tax/VAT
                Me._dicPaymentTransType.Add(EnumPaymentType.UnpaidMFWHTax, "MFW")
                Me._dicPaymentTransType.Add(EnumPaymentType.UnpaidMFWHVAT, "MFW")

                'Withholding Tax/VAT on Default Interest
                Me._dicPaymentTransType.Add(EnumPaymentType.WHTaxDefault, "MFWDI")
                Me._dicPaymentTransType.Add(EnumPaymentType.WHVATDefault, "MFWDI")

                'Default Interest in MF/MFV
                Me._dicPaymentTransType.Add(EnumPaymentType.UnpaidMFDefault, "MFDI")
                Me._dicPaymentTransType.Add(EnumPaymentType.UnpaidMFVDefault, "MFDI")

                'Base MF/MFV
                Me._dicPaymentTransType.Add(EnumPaymentType.UnpaidMF, "MFO")
                Me._dicPaymentTransType.Add(EnumPaymentType.UnpaidMFV, "MFO")

                'Payment Energy/VAT and Refund MF/MFV
                Me._dicPaymentTransType.Add(EnumPaymentType.PaymentEnergy, "EVEP")
                Me._dicPaymentTransType.Add(EnumPaymentType.PaymentEnergyVAT, "EVEP")
                Me._dicPaymentTransType.Add(EnumPaymentType.PaymentMF, "MFR")
                Me._dicPaymentTransType.Add(EnumPaymentType.PaymentMFV, "MFR")

                'Default Interest Allocation
                Me._dicPaymentTransType.Add(EnumPaymentType.DefaultAllocation, "EDI")

                'Applied Deferred Payments
                Me._dicPaymentTransType.Add(EnumPaymentType.DeferredAppliedEnergy, "EDEF")
                Me._dicPaymentTransType.Add(EnumPaymentType.DeferredAppliedVAT, "EVDEF")

                'Offsetting of Previous Receivable
                Me._dicPaymentTransType.Add(EnumPaymentType.OffsetOfPreviousReceivableEnergy, "EO")
                Me._dicPaymentTransType.Add(EnumPaymentType.OffsetOfPreviousReceivableEnergyDefault, "EODI")
                Me._dicPaymentTransType.Add(EnumPaymentType.OffsetOfPreviousReceivableVAT, "EVO")

                'Offsetting of Current Receivable
                Me._dicPaymentTransType.Add(EnumPaymentType.OffsetToCurrentReceivableEnergy, "EO")
                Me._dicPaymentTransType.Add(EnumPaymentType.OffsetToCurrentReceivableEnergyDefault, "EODI")
                Me._dicPaymentTransType.Add(EnumPaymentType.OffsetToCurrentReceivableVAT, "EVO")

                'Offsetting of WESM Bills
                'Me._dicPaymentTransType.Add(EnumPaymentType.OffsetAmountEnergy, "EWBOFF")
                'Me._dicPaymentTransType.Add(EnumPaymentType.OffsetAmountVATEnergy, "EVWBOFF")
                'Me._dicPaymentTransType.Add(EnumPaymentType.WESMBillOffsetting, "WBOFF")

                'Offset Allocated
                'Me._dicPaymentTransType.Add(EnumPaymentType.OffsetAllocatedEnergy, "EAOFF")
                'Me._dicPaymentTransType.Add(EnumPaymentType.OffsetAllocatedVAT, "EVAOFF")

                'Special Case Offsetting
                'Me._dicPaymentTransType.Add(EnumPaymentType.OffsetSpecialCase, "SCOFF")

                'Return To Participant
                Me._dicPaymentTransType.Add(EnumPaymentType.ReturnToParticipant, "XC")

                'Offset from PR
                'Me._dicPaymentTransType.Add(EnumPaymentType.OffsetFromPrudentialRequirement, "PROFF")
            End If

            Return Me._dicPaymentTransType
        End Get
    End Property

End Class
