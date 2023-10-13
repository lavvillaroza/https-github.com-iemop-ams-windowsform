'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             NewPaymentHelper
'Orginal Author:         Lance Arjay Villaroza
'File Creation Date:     July 10, 2015
'Development Group:      Software Development and Support Division
'Description:            Class Payment Helper
'Arguments/Parameters:   -
'Files/Database Tables:  -
'Return Value:           -
'Error codes/Exceptions: -
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		            Description


Option Explicit On
Option Strict On


Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Threading
Imports System.Threading.Tasks

Public Class PaymentHelper
    Implements IDisposable

#Region "Proforma Entries for Payment"
    Public _PaymentProformaEntries As PaymentProformaEntries
    Private ReadOnly Property PaymentProformaEntries() As PaymentProformaEntries
        Get
            Return _PaymentProformaEntries
        End Get
    End Property
#End Region

#Region "WESMBillHelper"
    Public _WBillHelper As WESMBillHelper
    Private ReadOnly Property WBillHelper() As WESMBillHelper
        Get
            Return _WBillHelper
        End Get
    End Property
#End Region

#Region "BFactory"
    Public _BFactory As New BusinessFactory
    Private ReadOnly Property BFactory() As BusinessFactory
        Get
            Return _BFactory
        End Get
    End Property
#End Region

#Region "DAL"
    Private _DataAccess As DAL
    Public ReadOnly Property DataAccess() As DAL
        Get
            Return Me._DataAccess
        End Get
    End Property
#End Region

#Region "ClassSetup"
    Private _ORNumber As Long = 0
    Private DicRFPPaymentTypePR As New Dictionary(Of String, EnumParticipantPaymentType)
    Private DicRFPParticularPR As New Dictionary(Of String, String)
    Private DicRFPPaymentTypeFP As New Dictionary(Of String, EnumParticipantPaymentType)
    Private DicRFPParticularFP As New Dictionary(Of String, String)

    Public offsettingIterationCount As Integer
    Private getWESMBillSalesAndPurchases As New List(Of WESMBillSalesAndPurchasedForWT)
    Private getWESMBillParticulars As New Dictionary(Of String, String)
    Private cts As CancellationToken
    Private progress As IProgress(Of ProgressClass)

    Public Sub New()
        'Get the current instance of the dal
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._BFactory = BusinessFactory.GetInstance
        Me._PaymentProformaEntries = New PaymentProformaEntries
        Me.ResetObject()
    End Sub

    Private Sub ResetObject()

        Me._WESMBillList = New List(Of WESMBill)
        Me._CalendarBP = New List(Of CalendarBillingPeriod)
        Me._SPAInvoiceList = New List(Of SPAInvoices)
        Me._PayAllocDate = New AllocationDate
        Me._AMParticipants = New List(Of AMParticipants)
        Me._CollectionMonitoring = New List(Of CollectionMonitoring)
        Me._AMCollectionList = New List(Of Collection)
        Me._AMCollectionAllocList = New List(Of CollectionAllocation)
        Me._EnergyListCollection = New List(Of ARCollection)
        Me._VATonEnergyCollectionList = New List(Of ARCollection)
        Me._MFwithVATCollectionList = New List(Of ARCollection)
        Me._OffsettingEnergyCollectionList = New List(Of ARCollection)
        Me._OffsettingVATonEnergyCollectionList = New List(Of ARCollection)
        Me._OffsettingMFwithVATCollectionList = New List(Of ARCollection)
        Me._EnergyNoTransactionARList = New List(Of ARCollection)
        Me._VATNoTransactionARList = New List(Of ARCollection)
        Me._MFwithVATNoTransARList = New List(Of ARCollection)
        Me._EnergyNoTransactionAPList = New List(Of APAllocation)
        Me._VATNoTransactionAPList = New List(Of APAllocation)
        Me._MFwithVATNoTransAPList = New List(Of APAllocation)
        Me._TotalARCollections = 0D
        Me._TotalEnergyCollectionPerBP = New List(Of ARCollectionPerBP)
        Me._TotalVATCollectionPerBP = New List(Of ARCollectionPerBP)
        Me._TotalMFCollectionPerBP = New List(Of ARCollectionPerBP)
        Me._TotalMFCollectionPerBP = New List(Of ARCollectionPerBP)
        Me._EnergyAllocationList = New List(Of APAllocation)
        Me._VATonEnergyAllocationList = New List(Of APAllocation)
        Me._MFwithVATAllocationList = New List(Of APAllocation)
        Me._OffsettingEnergyAllocationList = New List(Of APAllocation)
        Me._OffsettingVATonEnergyAllocationList = New List(Of APAllocation)
        Me._OffsettingMFwithVATAllocationList = New List(Of APAllocation)
        Me._MFwithVATShare = New List(Of PaymentShare)
        Me._EnergyShare = New List(Of PaymentShare)
        Me._VATonEnergyShare = New List(Of PaymentShare)
        Me._ListofDMCM = New List(Of DebitCreditMemo)
        Me._ListOfDMCMSummary = New List(Of DebitCreditMemoSummaryNew)
        Me._JournalVoucherList = New List(Of JournalVoucher)
        Me._DailyInterestRate = 0D
        Me._OffsettingMFCollectionListDT = Nothing
        Me._OffsettingMFAllocationListDT = Nothing
        Me._OffsettingEnergyCollectionListDT = Nothing
        Me._OffsettingEnergyAllocationListDT = Nothing
        Me._OffsettingVATonEnergyCollectionListDT = Nothing
        Me._OffsettingVATonEnergyAllocationListDT = Nothing
        Me._WESMBillSummaryList = New List(Of WESMBillSummary)
        Me._CollFundTransferForm = New List(Of FundTransferFormMain)
        Me._FundTransferForm = New List(Of FundTransferFormMain)
        Me._PaymentTrasferToPRList = New List(Of PaymentTransferToPR)
        Me._PrevDeferredPaymentList = New List(Of DeferredMain)
        Me._CurrentDeferredPaymentList = New List(Of DeferredMain)
        Me._EFTSummaryReportList = New List(Of EFT)
        Me._RequestForPayment = New RequestForPayment
        Me._ORofOffsettingMFwithVAT = New List(Of OfficialReceiptMain)
        Me._ORofFinPen = New List(Of OfficialReceiptMain)

    End Sub

    Private Sub ClearObjects()
        Me._WESMBillList = Nothing
        Me._CalendarBP = Nothing
        Me._SPAInvoiceList = Nothing
        Me._PayAllocDate = Nothing
        Me._AMParticipants = Nothing
        Me._CollectionMonitoring = Nothing
        Me._AMCollectionList = Nothing
        Me._AMCollectionAllocList = Nothing
        Me._EnergyListCollection = Nothing
        Me._VATonEnergyCollectionList = Nothing
        Me._MFwithVATCollectionList = Nothing
        Me._OffsettingEnergyCollectionList = Nothing
        Me._OffsettingVATonEnergyCollectionList = Nothing
        Me._OffsettingMFwithVATCollectionList = Nothing
        Me._EnergyNoTransactionARList = Nothing
        Me._VATNoTransactionARList = Nothing
        Me._MFwithVATNoTransARList = Nothing
        Me._EnergyNoTransactionAPList = Nothing
        Me._VATNoTransactionAPList = Nothing
        Me._MFwithVATNoTransAPList = Nothing
        Me._TotalARCollections = 0D
        Me._TotalEnergyCollectionPerBP = Nothing
        Me._TotalVATCollectionPerBP = Nothing
        Me._TotalMFCollectionPerBP = Nothing
        Me._TotalMFCollectionPerBP = Nothing
        Me._EnergyAllocationList = Nothing
        Me._VATonEnergyAllocationList = Nothing
        Me._MFwithVATAllocationList = Nothing
        Me._OffsettingEnergyAllocationList = Nothing
        Me._OffsettingVATonEnergyAllocationList = Nothing
        Me._OffsettingMFwithVATAllocationList = Nothing
        Me._MFwithVATShare = Nothing
        Me._EnergyShare = Nothing
        Me._VATonEnergyShare = Nothing
        Me._ListofDMCM = Nothing
        Me._ListOfDMCMSummary = Nothing
        Me._JournalVoucherList = Nothing
        Me._DailyInterestRate = 0D
        Me._OffsettingMFCollectionListDT = Nothing
        Me._OffsettingMFAllocationListDT = Nothing
        Me._OffsettingEnergyCollectionListDT = Nothing
        Me._OffsettingEnergyAllocationListDT = Nothing
        Me._OffsettingVATonEnergyCollectionListDT = Nothing
        Me._OffsettingVATonEnergyAllocationListDT = Nothing
        Me._WESMBillSummaryList = Nothing
        Me._CollFundTransferForm = Nothing
        Me._FundTransferForm = Nothing
        Me._PaymentTrasferToPRList = Nothing
        Me._PrevDeferredPaymentList = Nothing
        Me._CurrentDeferredPaymentList = Nothing
        Me._EFTSummaryReportList = Nothing
        Me._RequestForPayment = Nothing
        Me._ORofOffsettingMFwithVAT = Nothing
        Me._ORofFinPen = Nothing
    End Sub

    Public Sub InitializeObject(ByVal SelectedAllocDate As AllocationDate)

        Me.ResetObject()
        'Me.ProgressRemarks = "Fetching Data..."
        Me._PayAllocDate = SelectedAllocDate
        Me._PaymentProformaEntries = New PaymentProformaEntries
        Me._PaymentProformaEntries.JVSign = WBillHelper.GetSignatories("JV").First
        Me._PaymentProformaEntries.DMCMSign = WBillHelper.GetSignatories("DMCM").First

        'Me._SPAInvoiceList = Me.WBillHelper.GetSPAInvoiceList()
        Me._CollectionMonitoring = Me.WBillHelper.GetCollectionMonitoring(SelectedAllocDate.CollAllocationDate)
        Me._CalendarBP = Me.WBillHelper.GetCalendarBP
        Me._WESMBillList = Me.WBillHelper.GetWESMBillList
        'Me.InsertIntoDic()
        Me._AMCollectionList = Me.WBillHelper.GetCollections(Me.PayAllocDate.CollAllocationDate, Me.PayAllocDate.CollAllocationDate, False)

        Me._AMCollectionAllocList = Me.WBillHelper.GetCollectionAllocation(Me.PayAllocDate.CollAllocationDate, Me.PayAllocDate.CollAllocationDate, False)
        Me.PrevDeferredPaymentList = Me.WBillHelper.GetCurrentDeferredPayment()
        Me.CollFundTransferform = WBillHelper.GetFundTransferForm(Me.PayAllocDate.CollAllocationDate, Me.PayAllocDate.CollAllocationDate)

        Dim DailyInterestRateDic = Me.WBillHelper.GetDailyInterestRate()
        If DailyInterestRateDic.ContainsKey(SelectedAllocDate.CollAllocationDate) Then
            Me.DailyInterestRate = DailyInterestRateDic.Item(SelectedAllocDate.CollAllocationDate)
        Else
            Me.DailyInterestRate = 0
        End If

        Me._AMParticipants = WBillHelper.GetAMParticipants()
        Me.WESMBillSummaryList = (From x In WBillHelper.GetWESMBillSummaryAll(SelectedAllocDate.CollAllocationDate)
                                  Select x).ToList()

        If cts.IsCancellationRequested Then
            Throw New OperationCanceledException
        End If

    End Sub

    Public Sub InitializeObjectView(ByVal _BusinessFactory As BusinessFactory,
                                    ByVal _WESMBillHelper As WESMBillHelper,
                                    ByVal SelectedAllocDate As AllocationDate)
        Me.ResetObject()

        Me._WBillHelper = _WESMBillHelper
        Me._BFactory = _BusinessFactory
        Me._PayAllocDate = SelectedAllocDate
        Me._PaymentProformaEntries = New PaymentProformaEntries
        Me._PaymentProformaEntries.JVSign = WBillHelper.GetSignatories("JV").First
        Me._PaymentProformaEntries.DMCMSign = WBillHelper.GetSignatories("DMCM").First

        Me._CollectionMonitoring = Me.WBillHelper.GetCollectionMonitoring(SelectedAllocDate.CollAllocationDate)
        Me._AMParticipants = WBillHelper.GetAMParticipants()
        Me._CalendarBP = Me.WBillHelper.GetCalendarBP
        Me._AMCollectionList = Me.WBillHelper.GetCollections(Me.PayAllocDate.CollAllocationDate, Me.PayAllocDate.CollAllocationDate, False)

        Dim JVNoList As List(Of Long) = WBillHelper.GetAMPaymentNewDetails(Me.PayAllocDate.PaymentNo)

        Me.JournalVoucherList = WBillHelper.GetPaymentJV(JVNoList)
        Me.FundTransferform = WBillHelper.GetFTFforPayment((From x In Me.JournalVoucherList Select x.BatchCode).ToList())
        Me.ListofDMCM = WBillHelper.GetDMCMforPayment((From x In Me.JournalVoucherList Where x.PostedType = EnumPostedType.PA.ToString Select x.JVNumber).FirstOrDefault)
        Me.PaymentTransferToPRList = WBillHelper.GetAMPaymentNewTransToPR(Me.PayAllocDate.PaymentNo)
        Me.RequestForPayment = WBillHelper.GetAMRFPMain(Me.PayAllocDate.PaymentNo)
        Me.CurrentDeferredPaymentList = Me.WBillHelper.GetCurrentDeferredPayment(Me.PayAllocDate.PaymentNo)
        Me.PrevDeferredPaymentList = Me.WBillHelper.GetPrevDeferredPayment(Me.PayAllocDate.PaymentNo)
        Me.EFTSummaryReportList = Me.WBillHelper.GetAMPaymentEFT(Me.PayAllocDate.PaymentNo)
        Dim ORList = Me.WBillHelper.GetOfficialReceipt(Me.PayAllocDate.CollAllocationDate)
        Me.ORofOffsettingMFwithVAT = (From x In ORList
                                      Where x.TransactionType = EnumORTransactionType.MarketFees _
                                      Or x.TransactionType = EnumORTransactionType.PaymentMarketFees
                                      Select x).ToList

        getWESMBillSalesAndPurchases = (From x In Me.WBillHelper.GetWESMBillSalesAndPurchasedForEWT2(Me.PayAllocDate.CollAllocationDate) Select x).ToList

        Me.ORofFinPen = (From x In ORList
                         Where x.TransactionType = EnumORTransactionType.FinPenAmount
                         Select x).ToList()

        If cts.IsCancellationRequested Then
            Throw New OperationCanceledException
        End If

    End Sub

    Public Sub InitializeObjectOnPostedTypeP(ByVal _BusinessFactory As BusinessFactory,
                                            ByVal _WESMBillHelper As WESMBillHelper,
                                            ByVal SelectedAllocDate As AllocationDate)
        Me.ResetObject()

        Me._WBillHelper = _WESMBillHelper
        Me._BFactory = _BusinessFactory
        Me._PayAllocDate = SelectedAllocDate

        Me._CollectionMonitoring = Me.WBillHelper.GetCollectionMonitoring(SelectedAllocDate.CollAllocationDate)
        Me._AMParticipants = WBillHelper.GetAMParticipants()
        Me._CalendarBP = Me.WBillHelper.GetCalendarBP
        Me._WESMBillList = Me.WBillHelper.GetWESMBillList

        'Me.InsertIntoDic()
        'Dim JVNoList As List(Of Long) = WBillHelper.GetAMPaymentNewDetails(Me.PayAllocDate.PaymentNo)
        'Me.JournalVoucherList = WBillHelper.GetPaymentJV(JVNoList)
        'Me.FundTransferform = WBillHelper.GetFTFforPayment((From x In Me.JournalVoucherList Select x.BatchCode).ToList())
        'Me.RequestForPayment = WBillHelper.GetAMRFPMain(Me.PayAllocDate.PaymentNo)

        Me.CurrentDeferredPaymentList = Me.WBillHelper.GetCurrentDeferredPayment(Me.PayAllocDate.PaymentNo)
        Me.PrevDeferredPaymentList = Me.WBillHelper.GetPrevDeferredPayment(Me.PayAllocDate.PaymentNo)
        Me.EFTSummaryReportList = Me.WBillHelper.GetAMPaymentEFT(Me.PayAllocDate.PaymentNo)
        Me.GetCollections()
        Me.GetPayments()
        Me.GetPaymentOffsetting()
    End Sub

    Public Sub InitializeObjectOnPostedTypePA(ByVal _BusinessFactory As BusinessFactory,
                                              ByVal _WESMBillHelper As WESMBillHelper,
                                              ByVal SelectedAllocDate As AllocationDate)
        Me.ResetObject()

        Me._WBillHelper = _WESMBillHelper
        Me._BFactory = _BusinessFactory
        Me._PayAllocDate = SelectedAllocDate

        Me._AMParticipants = WBillHelper.GetAMParticipants()
        Dim JVNoList As List(Of Long) = WBillHelper.GetAMPaymentNewDetails(Me.PayAllocDate.PaymentNo)
        Me.JournalVoucherList = WBillHelper.GetPaymentJV(JVNoList)
        Me.ListofDMCM = WBillHelper.GetDMCMforPayment((From x In Me.JournalVoucherList Where x.PostedType = EnumPostedType.PA.ToString Select x.JVNumber).First)

    End Sub

    Public Sub InitializeObjectOnPostedTypePEFT(ByVal _BusinessFactory As BusinessFactory,
                                                  ByVal _WESMBillHelper As WESMBillHelper,
                                                  ByVal SelectedAllocDate As AllocationDate)
        Me._WBillHelper = _WESMBillHelper
        Me._BFactory = _BusinessFactory
        Me._PayAllocDate = SelectedAllocDate

        Me._AMParticipants = WBillHelper.GetAMParticipants()
        Dim JVNoList As List(Of Long) = WBillHelper.GetAMPaymentNewDetails(Me.PayAllocDate.PaymentNo)
        Me.JournalVoucherList = WBillHelper.GetPaymentJV(JVNoList)
        Me.RequestForPayment = WBillHelper.GetAMRFPMain(Me.PayAllocDate.PaymentNo)

    End Sub

    Public Sub InitializeObjectOnGP(ByVal _BusinessFactory As BusinessFactory,
                                    ByVal _WESMBillHelper As WESMBillHelper,
                                    ByVal SelectedAllocDate As AllocationDate,
                                    ByVal JVNo As Long)
        Me._WBillHelper = _WESMBillHelper
        Me._BFactory = _BusinessFactory
        Me._PayAllocDate = SelectedAllocDate
        Me._PaymentProformaEntries = New PaymentProformaEntries
        Me._PaymentProformaEntries.JVSign = WBillHelper.GetSignatories("JV").First
        Me._PaymentProformaEntries.DMCMSign = WBillHelper.GetSignatories("DMCM").First

        Me._CollectionMonitoring = Me.WBillHelper.GetCollectionMonitoring(SelectedAllocDate.CollAllocationDate)
        Me._AMParticipants = WBillHelper.GetAMParticipants()
        Me._CalendarBP = Me.WBillHelper.GetCalendarBP
        Me._WESMBillList = Me.WBillHelper.GetWESMBillList
        'Me.InsertIntoDic()
        Me._AMCollectionList = Me.WBillHelper.GetCollections(Me.PayAllocDate.CollAllocationDate, Me.PayAllocDate.CollAllocationDate, False)

        Dim JVNoList As List(Of Long) = WBillHelper.GetAMPaymentNewDetails(Me.PayAllocDate.PaymentNo)
        Me.JournalVoucherList = WBillHelper.GetPaymentJV(JVNoList)
        Me.FundTransferform = WBillHelper.GetFTFforPayment((From x In Me.JournalVoucherList Select x.BatchCode).ToList())
        Me.ListofDMCM = WBillHelper.GetDMCMforPayment((From x In Me.JournalVoucherList Where x.PostedType = EnumPostedType.PA.ToString Select x.JVNumber).First)
        Me.PaymentTransferToPRList = WBillHelper.GetAMPaymentNewTransToPR(Me.PayAllocDate.PaymentNo)
        Me.RequestForPayment = WBillHelper.GetAMRFPMain(Me.PayAllocDate.PaymentNo)
        Me.CurrentDeferredPaymentList = Me.WBillHelper.GetCurrentDeferredPayment(Me.PayAllocDate.PaymentNo)
        Me.PrevDeferredPaymentList = Me.WBillHelper.GetPrevDeferredPayment(Me.PayAllocDate.PaymentNo)
        Me.EFTSummaryReportList = Me.WBillHelper.GetAMPaymentEFT(Me.PayAllocDate.PaymentNo)

        Me.GetCollections()
        Me.GetPayments()
        Me.GetPaymentOffsetting()
    End Sub
#End Region

#Region "Set of Cancellation Token"
    Public Sub SetCTS(ByVal ct As CancellationToken)
        Me.cts = ct
    End Sub
#End Region

#Region "Set Progress Report"
    Public Sub SetProgress(ByVal progress As IProgress(Of ProgressClass))
        Me.progress = progress
    End Sub
#End Region

#Region "Property of WESM Bill"
    Private _WESMBillList As New List(Of WESMBill)
    Private ReadOnly Property WESMBillList() As List(Of WESMBill)
        Get
            Return _WESMBillList
        End Get
    End Property
    'Private Sub InsertIntoDic()
    '    For Each item In _WESMBillList
    '        If Not getWESMBillParticulars.ContainsKey(item.InvoiceNumber & "|" & item.ChargeType.ToString) Then
    '            getWESMBillParticulars.Add(item.InvoiceNumber & "|" & item.ChargeType.ToString, item.Remarks)
    '        End If
    '    Next
    'End Sub
#End Region

#Region "Property of Calendar Billing Period"
    Private _CalendarBP As New List(Of CalendarBillingPeriod)
    Private ReadOnly Property CalendarBP() As List(Of CalendarBillingPeriod)
        Get
            Return _CalendarBP
        End Get
    End Property
#End Region

#Region "Property of SPA Invoices List"
    Private _SPAInvoiceList As New List(Of SPAInvoices)
    Private ReadOnly Property SPAInvoiceList() As List(Of SPAInvoices)
        Get
            Return _SPAInvoiceList
        End Get
    End Property

#End Region

#Region "Property of AllocationDate for AM_PAYMENT_NEW"
    Public _PayAllocDate As New AllocationDate
    Private ReadOnly Property PayAllocDate() As AllocationDate
        Get
            Return _PayAllocDate
        End Get
    End Property
#End Region

#Region "Property of AMParticipants"
    Private _AMParticipants As New List(Of AMParticipants)
    Public ReadOnly Property AMParticipants() As List(Of AMParticipants)
        Get
            Return _AMParticipants
        End Get
    End Property
#End Region

#Region "Property of AM_COLLECTION_MONITORING Table"
    Private _CollectionMonitoring As List(Of CollectionMonitoring)
    Public ReadOnly Property CollectionMonitoring() As List(Of CollectionMonitoring)
        Get
            Return _CollectionMonitoring
        End Get
    End Property
#End Region

#Region "Property of AM_COLLECTION Table"
    Private _AMCollectionList As New List(Of Collection)
    Private ReadOnly Property AMCollectionList() As List(Of Collection)
        Get
            Return _AMCollectionList
        End Get
    End Property
#End Region

#Region "Property of AM_COLLECTION_ALLOCATION Table"
    Private _AMCollectionAllocList As New List(Of CollectionAllocation)
    Private ReadOnly Property AMCollectionAllocList() As List(Of CollectionAllocation)
        Get
            Return _AMCollectionAllocList
        End Get
    End Property
#End Region

#Region "Properites of CollectionList from AM_COLLECTION_ALLOCATION"
    Private _EnergyListCollection As New List(Of ARCollection)
    Public ReadOnly Property EnergyListCollection() As List(Of ARCollection)
        Get
            Return _EnergyListCollection
        End Get
    End Property

    Private _VATonEnergyCollectionList As New List(Of ARCollection)
    Public ReadOnly Property VATonEnergyCollectionList() As List(Of ARCollection)
        Get
            Return _VATonEnergyCollectionList
        End Get
    End Property

    Private _MFwithVATCollectionList As New List(Of ARCollection)
    Public ReadOnly Property MFwithVATCollectionList() As List(Of ARCollection)
        Get
            Return _MFwithVATCollectionList
        End Get
    End Property
#End Region

#Region "Properties of Offsetting Collection List for AM_PAYMENT_NEW_OFFSETTING_AR"
    Private _OffsettingEnergyCollectionList As New List(Of ARCollection)
    Public ReadOnly Property OffsettingEnergyCollectionList() As List(Of ARCollection)
        Get
            Return _OffsettingEnergyCollectionList
        End Get
    End Property

    Private _OffsettingVATonEnergyCollectionList As New List(Of ARCollection)
    Public ReadOnly Property OffsettingVATonEnergyCollectionList() As List(Of ARCollection)
        Get
            Return _OffsettingVATonEnergyCollectionList
        End Get
    End Property

    Private _OffsettingMFwithVATCollectionList As New List(Of ARCollection)
    Public ReadOnly Property OffsettingMFwithVATCollectionList() As List(Of ARCollection)
        Get
            Return _OffsettingMFwithVATCollectionList
        End Get
    End Property
#End Region

#Region "Properties of Offsetting Collection List for AM_PAYMENT_NEW_NOTRANS_AR"
    Private _EnergyNoTransactionARList As New List(Of ARCollection)
    Public ReadOnly Property EnergyNoTransactionARList() As List(Of ARCollection)
        Get
            Return _EnergyNoTransactionARList
        End Get
    End Property

    Private _VATNoTransactionARList As New List(Of ARCollection)
    Public ReadOnly Property VATNoTransactionARList() As List(Of ARCollection)
        Get
            Return _VATNoTransactionARList
        End Get
    End Property

    Private _MFwithVATNoTransARList As New List(Of ARCollection)
    Public ReadOnly Property MFwithVATNoTransARList() As List(Of ARCollection)
        Get
            Return _MFwithVATNoTransARList
        End Get
    End Property
#End Region

#Region "Properties of Offsetting Collection List for AM_PAYMENT_NEW_NOTRANS_AP"
    Private _EnergyNoTransactionAPList As New List(Of APAllocation)
    Public ReadOnly Property EnergyNoTransactionAPList() As List(Of APAllocation)
        Get
            Return _EnergyNoTransactionAPList
        End Get
    End Property

    Private _VATNoTransactionAPList As New List(Of APAllocation)
    Public ReadOnly Property VATNoTransactionAPList() As List(Of APAllocation)
        Get
            Return _VATNoTransactionAPList
        End Get
    End Property

    Private _MFwithVATNoTransAPList As New List(Of APAllocation)
    Public ReadOnly Property MFwithVATNoTransAPList() As List(Of APAllocation)
        Get
            Return _MFwithVATNoTransAPList
        End Get
    End Property
#End Region

#Region "Properties of TotalCollectionPerBillingPeriod For Energy and VATonEnergy Collections"
    Private _TotalARCollections As Decimal = 0
    Public ReadOnly Property TotalARCollections() As Decimal
        Get
            Return _TotalARCollections
        End Get
    End Property

    Private _TotalEnergyCollectionPerBP As New List(Of ARCollectionPerBP)
    Public ReadOnly Property TotalEnergyCollectionperBP() As List(Of ARCollectionPerBP)
        Get
            Return _TotalEnergyCollectionPerBP
        End Get
    End Property

    Private _TotalVATCollectionPerBP As New List(Of ARCollectionPerBP)
    Public ReadOnly Property TotalVATCollectionPerBP() As List(Of ARCollectionPerBP)
        Get
            Return _TotalVATCollectionPerBP
        End Get
    End Property

    Private _TotalMFCollectionPerBP As New List(Of ARCollectionPerBP)
    Public ReadOnly Property TotalMFCollectionPerBP() As List(Of ARCollectionPerBP)
        Get
            Return _TotalMFCollectionPerBP
        End Get
    End Property
#End Region

#Region "Properties of AllocationList For AM_PAYMENT_NEW_AP"
    Private _EnergyAllocationList As New List(Of APAllocation)
    Public ReadOnly Property EnergyAllocationList() As List(Of APAllocation)
        Get
            Return _EnergyAllocationList
        End Get
    End Property

    Private _VATonEnergyAllocationList As New List(Of APAllocation)
    Public ReadOnly Property VATonEnergyAllocationList() As List(Of APAllocation)
        Get
            Return _VATonEnergyAllocationList
        End Get
    End Property

    Private _MFwithVATAllocationList As New List(Of APAllocation)
    Public ReadOnly Property MFwithVATAllocationList() As List(Of APAllocation)
        Get
            Return _MFwithVATAllocationList
        End Get
    End Property
#End Region

#Region "Properties of Offsetting Allocation List For AM_PAYMENT_NEW_OFFSETTING_AP"
    Private _OffsettingEnergyAllocationList As New List(Of APAllocation)
    Public ReadOnly Property OffsettingEnergyAllocationList() As List(Of APAllocation)
        Get
            Return _OffsettingEnergyAllocationList
        End Get
    End Property

    Private _OffsettingVATonEnergyAllocationList As New List(Of APAllocation)
    Public ReadOnly Property OffsettingVATonEnergyAllocationList() As List(Of APAllocation)
        Get
            Return _OffsettingVATonEnergyAllocationList
        End Get
    End Property

    Private _OffsettingMFwithVATAllocationList As New List(Of APAllocation)
    Public ReadOnly Property OffsettingMFwithVATAllocationList() As List(Of APAllocation)
        Get
            Return _OffsettingMFwithVATAllocationList
        End Get
    End Property

#End Region

#Region "Properties of AllocationShare for MFwithVAT, Energy and VATonEnergy"
    Private _MFwithVATShare As New List(Of PaymentShare)
    Public ReadOnly Property MFwithVATShare() As List(Of PaymentShare)
        Get
            Return _MFwithVATShare
        End Get
    End Property

    Private _EnergyShare As New List(Of PaymentShare)
    Public ReadOnly Property EnergyShare() As List(Of PaymentShare)
        Get
            Return _EnergyShare
        End Get
    End Property

    Private _VATonEnergyShare As New List(Of PaymentShare)
    Public ReadOnly Property VATonEnergyShare() As List(Of PaymentShare)
        Get
            Return _VATonEnergyShare
        End Get
    End Property
#End Region

#Region "Property Of DMCM List"
    Private _ListofDMCM As New List(Of DebitCreditMemo)
    Public Property ListofDMCM() As List(Of DebitCreditMemo)
        Get
            Return _ListofDMCM
        End Get
        Set(value As List(Of DebitCreditMemo))
            _ListofDMCM = value
        End Set
    End Property
#End Region

#Region "Property of DMCM Summary List"
    Private _ListOfDMCMSummary As New List(Of DebitCreditMemoSummaryNew)
    Public ReadOnly Property ListOfDMCMSummary() As List(Of DebitCreditMemoSummaryNew)
        Get
            Return _ListOfDMCMSummary
        End Get
    End Property
#End Region

#Region "Property of Journal Voucher List For Payment, Payment Allocation and Payment EFT"
    Private _JournalVoucherList As List(Of JournalVoucher)
    Public Property JournalVoucherList() As List(Of JournalVoucher)
        Get
            Return _JournalVoucherList
        End Get
        Set(ByVal value As List(Of JournalVoucher))
            _JournalVoucherList = value
        End Set
    End Property
#End Region

#Region "Property of Daily Interest Rate"
    Private _DailyInterestRate As Decimal
    Public Property DailyInterestRate() As Decimal
        Get
            Return _DailyInterestRate
        End Get
        Set(value As Decimal)
            _DailyInterestRate = value
        End Set
    End Property

#End Region

#Region "Property of MFCollectionListDT"
    Private _MFwithVATCollectionListDT As New DataTable
    Public ReadOnly Property MFwithVATCollectionListDT() As DataTable
        Get
            Return _MFwithVATCollectionListDT
        End Get
    End Property
#End Region

#Region "Property of MFAllocationListDT"
    Private _MFwithVATAllocationListDT As New DataTable
    Public ReadOnly Property MFwithVATAllocationListDT() As DataTable
        Get
            Return _MFwithVATAllocationListDT
        End Get
    End Property
#End Region

#Region "Property of EnergyCollectionListDT"
    Private _EnergyCollectionListDT As New DataTable
    Public ReadOnly Property EnergyCollectionListDT() As DataTable
        Get
            Return _EnergyCollectionListDT
        End Get
    End Property
#End Region

#Region "Property of EnergyAllocationListDT"
    Private _EnergyAllocationListDT As New DataTable
    Public ReadOnly Property EnergyAllocationListDT() As DataTable
        Get
            Return _EnergyAllocationListDT
        End Get
    End Property
#End Region

#Region "Property of VATonEnergyCollectionListDT"
    Private _VATonEnergyCollectionListDT As New DataTable
    Public ReadOnly Property VATonEnergyCollectionListDT() As DataTable
        Get
            Return _VATonEnergyCollectionListDT
        End Get
    End Property
#End Region

#Region "Property of VATonEnergyAllocationListDT"
    Private _VATonEnergyAllocationListDT As New DataTable
    Public ReadOnly Property VATonEnergyAllocationListDT() As DataTable
        Get
            Return _VATonEnergyAllocationListDT
        End Get
    End Property
#End Region

#Region "Property OffsettingMFCollectionListDT"
    Private _OffsettingMFCollectionListDT As New DataTable
    Public ReadOnly Property OffsettingMFCollectionListDT() As DataTable
        Get
            Return _OffsettingMFCollectionListDT
        End Get
    End Property
#End Region

#Region "Property of OffsettingMFAllocationListDT"
    Private _OffsettingMFAllocationListDT As New DataTable
    Public ReadOnly Property OffsettingMFAllocationListDT() As DataTable
        Get
            Return _OffsettingMFAllocationListDT
        End Get
    End Property
#End Region

#Region "Property of OffsettingEnergyCollectionListDT"
    Private _OffsettingEnergyCollectionListDT As New DataTable
    Public ReadOnly Property OffsettingEnergyCollectionListDT() As DataTable
        Get
            Return _OffsettingEnergyCollectionListDT
        End Get
    End Property
#End Region

#Region "Property of OffsettingEnergyAllocationListDT"
    Private _OffsettingEnergyAllocationListDT As New DataTable
    Public ReadOnly Property OffsettingEnergyAllocationListDT() As DataTable
        Get
            Return _OffsettingEnergyAllocationListDT
        End Get
    End Property
#End Region

#Region "Property of OffsettingVATonEnergyCollectionListDT"
    Private _OffsettingVATonEnergyCollectionListDT As New DataTable
    Public ReadOnly Property OffsettingVATonEnergyCollectionListDT() As DataTable
        Get
            Return _OffsettingVATonEnergyCollectionListDT
        End Get
    End Property
#End Region

#Region "Property of OffsettingVATonEnergyAllocationListDT"
    Private _OffsettingVATonEnergyAllocationListDT As New DataTable
    Public ReadOnly Property OffsettingVATonEnergyAllocationListDT() As DataTable
        Get
            Return _OffsettingVATonEnergyAllocationListDT
        End Get
    End Property
#End Region

#Region "Property of WESMBillSummary"
    Private _WESMBillSummaryList As New List(Of WESMBillSummary)
    Public Property WESMBillSummaryList() As List(Of WESMBillSummary)
        Get
            Return _WESMBillSummaryList
        End Get
        Set(value As List(Of WESMBillSummary))
            _WESMBillSummaryList = value
        End Set
    End Property
#End Region

#Region "Property of PaymentWBSHistoryBalance"
    Private _PymntWBSHistoryBalance As New List(Of PaymentWBSHistoryBalance)
    Public Property PymntWBSHistoryBalance() As List(Of PaymentWBSHistoryBalance)
        Get
            Return _PymntWBSHistoryBalance
        End Get
        Set(value As List(Of PaymentWBSHistoryBalance))
            _PymntWBSHistoryBalance = value
        End Set
    End Property
#End Region

#Region "Property of Fund Transfer Form from Collection"
    Private _CollFundTransferForm As New List(Of FundTransferFormMain)
    Public Property CollFundTransferform() As List(Of FundTransferFormMain)
        Get
            Return _CollFundTransferForm
        End Get
        Set(ByVal value As List(Of FundTransferFormMain))
            _CollFundTransferForm = value
        End Set
    End Property
#End Region

#Region "Property of Fund Transfer Form from Payment"
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

#Region "Property of PaymentTransferToPR"
    Private _PaymentTrasferToPRList As New List(Of PaymentTransferToPR)
    Public Property PaymentTransferToPRList() As List(Of PaymentTransferToPR)
        Get
            Return _PaymentTrasferToPRList
        End Get
        Set(value As List(Of PaymentTransferToPR))
            _PaymentTrasferToPRList = value
        End Set
    End Property
#End Region

#Region "Property of Deferred Payment List"
    Private _PrevDeferredPaymentList As New List(Of DeferredMain)
    Public Property PrevDeferredPaymentList() As List(Of DeferredMain)
        Get
            Return _PrevDeferredPaymentList
        End Get
        Set(value As List(Of DeferredMain))
            _PrevDeferredPaymentList = value
        End Set
    End Property
#End Region

#Region "Property of New Deferred Payment List"
    Private _CurrentDeferredPaymentList As New List(Of DeferredMain)
    Public Property CurrentDeferredPaymentList() As List(Of DeferredMain)
        Get
            Return _CurrentDeferredPaymentList
        End Get
        Set(value As List(Of DeferredMain))
            _CurrentDeferredPaymentList = value
        End Set
    End Property
#End Region

#Region "Property of EFT List"
    Private _EFTSummaryReportList As New List(Of EFT)
    Public Property EFTSummaryReportList() As List(Of EFT)
        Get
            Return _EFTSummaryReportList
        End Get
        Set(value As List(Of EFT))
            _EFTSummaryReportList = value
        End Set
    End Property
#End Region

#Region "Property of RequestForPayment"
    Private _RequestForPayment As New RequestForPayment
    Public Property RequestForPayment() As RequestForPayment
        Get
            Return _RequestForPayment
        End Get
        Set(value As RequestForPayment)
            _RequestForPayment = value
        End Set
    End Property
#End Region

#Region "Property of Current Prudential"
    Private _CurrentPrudential As New List(Of Prudential)
    Public Property CurrentPrudential() As List(Of Prudential)
        Get
            Return _CurrentPrudential
        End Get
        Set(value As List(Of Prudential))
            _CurrentPrudential = value
        End Set
    End Property
#End Region

#Region "Property of Prudential History"
    Private _NewPrudentialHistory As New List(Of PrudentialHistory)
    Public Property NewPrudentialHistory() As List(Of PrudentialHistory)
        Get
            Return _NewPrudentialHistory
        End Get
        Set(value As List(Of PrudentialHistory))
            _NewPrudentialHistory = value
        End Set
    End Property
#End Region

#Region "Property of List of Officail Receipt for Offsetting MF"
    Private _ORofOffsettingMFwithVAT As New List(Of OfficialReceiptMain)
    Public Property ORofOffsettingMFwithVAT() As List(Of OfficialReceiptMain)
        Get
            Return _ORofOffsettingMFwithVAT
        End Get
        Set(value As List(Of OfficialReceiptMain))
            _ORofOffsettingMFwithVAT = value
        End Set
    End Property
#End Region

#Region "Property of List of Official Receipt for Financial Penalty"
    Private _ORofFinPen As New List(Of OfficialReceiptMain)
    Public Property ORofFinPen() As List(Of OfficialReceiptMain)
        Get
            Return _ORofFinPen
        End Get
        Set(value As List(Of OfficialReceiptMain))
            _ORofFinPen = value
        End Set
    End Property
#End Region

#Region "Property of Dictionary For FIT"
    Private _DicFIT As New Dictionary(Of String, Decimal)
    Public ReadOnly Property DicFIT() As Dictionary(Of String, Decimal)
        Get
            Return _DicFIT
        End Get
    End Property
#End Region

#Region "Property of WESMBillSummary with add back for withholding tax adjustment"
    Private _WESMBillSummaryListWithWHTAXAdj As New List(Of WESMBillSummary)
    Public Property WESMBillSummaryListWithWHTAXAdj() As List(Of WESMBillSummary)
        Get
            Return _WESMBillSummaryListWithWHTAXAdj
        End Get
        Set(value As List(Of WESMBillSummary))
            _WESMBillSummaryListWithWHTAXAdj = value
        End Set
    End Property
#End Region

#Region "Property of WESM Transaction Cover Summary List"
    Private _WESMTransCoverSummaryList As New List(Of WESMBillAllocCoverSummary)
    Public Property WESMTransCoverSummaryList() As List(Of WESMBillAllocCoverSummary)
        Get
            Return _WESMTransCoverSummaryList
        End Get
        Set(value As List(Of WESMBillAllocCoverSummary))
            _WESMTransCoverSummaryList = value
        End Set
    End Property
#End Region

#Region "Property of WESM Transaction Details Summary"
    Private _WESMTransDetailsSummaryList As New List(Of WESMTransDetailsSummary)
    Public Property WESMTransDetailsSummaryList() As List(Of WESMTransDetailsSummary)
        Get
            Return _WESMTransDetailsSummaryList
        End Get
        Set(value As List(Of WESMTransDetailsSummary))
            _WESMTransDetailsSummaryList = value
        End Set
    End Property
#End Region

#Region "Property of WESM Transaction Details Summary History"
    Private _WESMTransDetailsSummaryHistoryList As New List(Of WESMTransDetailsSummaryHistory)
    Public Property WESMTransDetailsSummaryHistoryList() As List(Of WESMTransDetailsSummaryHistory)
        Get
            Return _WESMTransDetailsSummaryHistoryList
        End Get
        Set(value As List(Of WESMTransDetailsSummaryHistory))
            _WESMTransDetailsSummaryHistoryList = value
        End Set
    End Property
#End Region

#Region "Methods For Payment Viewing"
    Public Sub GetCollections()
        Dim ARCollectionsProc As New ARCollectionProcess
        Dim ARCollectionList As New List(Of ARCollection)

        ARCollectionList = WBillHelper.GetCollectionAllocation(Me.PayAllocDate.CollAllocationDate, EnumIsAllocated.Allocated)

        ARCollectionsProc.GetARAllocationList(ARCollectionList)
        With ARCollectionsProc
            Me._EnergyCollectionListDT = .EnergyCollectionDT
            Me._VATonEnergyCollectionListDT = .VATonEnergyCollectionDT
            Me._MFwithVATCollectionListDT = .MFwithVATCollectionDT
            Me._EnergyListCollection = .EnergyCollection
            Me._VATonEnergyCollectionList = .VATCollection
            Me._MFwithVATCollectionList = .MFwithVATCollectionList

            Me._TotalEnergyCollectionPerBP = .TotalEnergyCollectionPerBP
            Me._TotalVATCollectionPerBP = .TotalVATCollectionPerBP
            Me._TotalMFCollectionPerBP = .TotalMFCollectionPerBP
        End With

        ARCollectionsProc = Nothing
        ARCollectionList = Nothing
    End Sub

    Public Sub GetPayments()
        Dim APAllocationProcess As New APAllocationProcess
        Dim APAllocationList As New List(Of APAllocation)

        APAllocationProcess._AMParticipantsList = Me.AMParticipants
        APAllocationList = WBillHelper.GetAMPaymentNewAP(Me.PayAllocDate.PaymentNo)

        APAllocationProcess.GetAPAllocationList(APAllocationList)

        Me._EnergyAllocationList = APAllocationProcess.EnergyAPAllocationList
        Me._VATonEnergyAllocationList = APAllocationProcess.VATonEnergyAPAllocationList
        Me._MFwithVATAllocationList = APAllocationProcess.MFWithVATAPAllocationList

        'Me._EnergyAllocationListDT = APAllocationProcess.EnergyAPAllocationListDT
        'Me._VATonEnergyAllocationListDT = APAllocationProcess.VATonEnergyAPAllocationListDT
        'Me._MFwithVATAllocationListDT = APAllocationProcess.MFWithVATAPAllocationListDT
        APAllocationProcess = Nothing
        APAllocationList = Nothing
    End Sub

    Public Sub GetPaymentOffsetting()
        Dim OffsetARCollProcess As New ARCollectionProcess
        Dim OffsetAPAllocProcess As New APAllocationProcess

        Dim OffsettingAPAllocation As New List(Of APAllocation)
        Dim OffsettingARCollection As New List(Of ARCollection)
        Dim PaymentShareList As New List(Of PaymentShare)

        OffsettingAPAllocation = WBillHelper.GetAMPaymentNewOffsetAP(Me.PayAllocDate.PaymentNo)
        OffsettingARCollection = WBillHelper.GetAMPaymentNewOffsetAR(Me.PayAllocDate.PaymentNo)
        PaymentShareList = WBillHelper.GetAMPaymentNewShare(Me.PayAllocDate.PaymentNo)


        Dim OffsettingARCollection_E = (From x In OffsettingARCollection
                                        Where x.CollectionType = EnumCollectionType.Energy _
                                        Or x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy
                                        Select x).ToList()

        Dim OffsettingARCollection_EV = (From x In OffsettingARCollection
                                         Where x.CollectionType = EnumCollectionType.VatOnEnergy
                                         Select x).ToList()

        Dim OffsettingARCollection_MFwV = (From x In OffsettingARCollection
                                           Where x.CollectionType = EnumCollectionType.MarketFees _
                                           Or x.CollectionType = EnumCollectionType.VatOnMarketFees _
                                           Or x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                                           Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF _
                                           Or x.CollectionType = EnumCollectionType.WithholdingTaxOnMF _
                                           Or x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest _
                                           Or x.CollectionType = EnumCollectionType.WithholdingVatOnMF _
                                           Or x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest
                                           Select x).ToList

        Dim OffsettingAPAllocation_E = (From x In OffsettingAPAllocation
                                        Where x.ChargeType = EnumChargeType.E Select x).ToList()

        Dim OffsettingAPAllocation_EV = (From x In OffsettingAPAllocation
                                         Where x.ChargeType = EnumChargeType.EV Select x).ToList()

        Dim OffsettingAPAllocation_MFwV = (From x In OffsettingAPAllocation
                                           Where x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV Select x).ToList()


        Me._OffsettingEnergyCollectionList = OffsettingARCollection_E
        'Me._OffsettingEnergyCollectionListDT = OffsetARCollProcess.Create_OffsettingAREnergyDT(OffsettingARCollection_E)

        Me._OffsettingEnergyAllocationList = OffsettingAPAllocation_E
        'Me._OffsettingEnergyAllocationListDT = OffsetAPAllocProcess.CreateOffsetAPEnergyDT(OffsettingAPAllocation_E)

        Me._OffsettingVATonEnergyCollectionList = OffsettingARCollection_EV
        'Me._OffsettingVATonEnergyCollectionListDT = OffsetARCollProcess.Create_OffsettingARVATDT(OffsettingARCollection_EV)

        Me._OffsettingVATonEnergyAllocationList = OffsettingAPAllocation_EV
        'Me._OffsettingVATonEnergyAllocationListDT = OffsetAPAllocProcess.CreateOffsetAPVATDT(OffsettingAPAllocation_EV)

        Me._OffsettingMFwithVATCollectionList = OffsettingARCollection_MFwV
        'Me._OffsettingMFCollectionListDT = OffsetARCollProcess.Create_ARMFDT(OffsettingARCollection_MFwV)

        Me._OffsettingMFwithVATAllocationList = OffsettingAPAllocation_MFwV
        'Me._OffsettingMFAllocationListDT = OffsetAPAllocProcess.CreateAPMFDT(OffsettingAPAllocation_MFwV)

        Me._EnergyShare = (From x In PaymentShareList Where x.ChargeType = EnumChargeType.E Select x).ToList()
        Me._VATonEnergyShare = (From x In PaymentShareList Where x.ChargeType = EnumChargeType.EV Select x).ToList()
        Me._MFwithVATShare = (From x In PaymentShareList Where x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV Select x).ToList()

        Me.PymntWBSHistoryBalance = WBillHelper.GetAMPaymentNewWBSBalance(Me.PayAllocDate.PaymentNo)


        Me._MFwithVATNoTransARList = (From x In WBillHelper.GetAMPaymentNewNoTransAR(Me.PayAllocDate.PaymentNo, EnumChargeType.MF) Select x).Union _
                                     (From x In WBillHelper.GetAMPaymentNewNoTransAR(Me.PayAllocDate.PaymentNo, EnumChargeType.MFV) Select x).ToList()

        Me._EnergyNoTransactionARList = (From x In WBillHelper.GetAMPaymentNewNoTransAR(Me.PayAllocDate.PaymentNo, EnumChargeType.E) Select x).ToList

        Me._VATNoTransactionARList = (From x In WBillHelper.GetAMPaymentNewNoTransAR(Me.PayAllocDate.PaymentNo, EnumChargeType.EV) Select x).ToList


        Me._MFwithVATNoTransAPList = (From x In WBillHelper.GetAMPaymentNewNoTransAP(Me.PayAllocDate.PaymentNo, EnumChargeType.MF) Select x).Union _
                                     (From x In WBillHelper.GetAMPaymentNewNoTransAP(Me.PayAllocDate.PaymentNo, EnumChargeType.MFV) Select x).ToList()

        Me._EnergyNoTransactionAPList = (From x In WBillHelper.GetAMPaymentNewNoTransAP(Me.PayAllocDate.PaymentNo, EnumChargeType.E) Select x).ToList

        Me._VATNoTransactionAPList = (From x In WBillHelper.GetAMPaymentNewNoTransAP(Me.PayAllocDate.PaymentNo, EnumChargeType.EV) Select x).ToList

        OffsetARCollProcess = Nothing
        OffsetAPAllocProcess = Nothing

        OffsettingAPAllocation = Nothing
        OffsettingARCollection = Nothing
        PaymentShareList = Nothing

    End Sub
#End Region

#Region "Function For ARCollections"
    Private SumOfCollection As Decimal
    Private SumOfExcess As Decimal

    Public Sub GetARCollections()
        Dim ARCollectionsProc As New ARCollectionProcess
        Dim ARCollectionList As New List(Of ARCollection)
        Dim SumofCollectionAllocation As Decimal = 0
        Dim SumOfExcessCollection As Decimal = 0



        ARCollectionList = WBillHelper.GetCollectionAllocation(Me.PayAllocDate.CollAllocationDate, EnumIsAllocated.NotAllocated)

        ARCollectionsProc.GetARAllocationList(ARCollectionList)

        SumofCollectionAllocation = Math.Abs((From x In ARCollectionsProc.EnergyCollection Select x.AllocationAmount).Sum +
                                             (From x In ARCollectionsProc.VATCollection Select x.AllocationAmount).Sum)

        Dim GetEnumCollMonType As New List(Of Integer)
        GetEnumCollMonType.Add(EnumCollectionMonitoringType.TransferToPRDrawdown)
        GetEnumCollMonType.Add(EnumCollectionMonitoringType.TransferToPRReplenishment)

        SumOfExcessCollection = (From x In Me.CollectionMonitoring
                                 Where Not GetEnumCollMonType.Contains(x.TransType)
                                 Select x.Amount).Sum

        SumOfCollection = SumofCollectionAllocation
        SumOfExcess = SumOfExcessCollection

        Me._EnergyListCollection = ARCollectionsProc.EnergyCollection
        Me._VATonEnergyCollectionList = ARCollectionsProc.VATCollection
        Me._MFwithVATCollectionList = ARCollectionsProc.MFwithVATCollectionList

        Me._TotalEnergyCollectionPerBP = ARCollectionsProc.TotalEnergyCollectionPerBP
        Me._TotalVATCollectionPerBP = ARCollectionsProc.TotalVATCollectionPerBP
        Me._TotalMFCollectionPerBP = ARCollectionsProc.TotalMFCollectionPerBP


        ARCollectionsProc = Nothing
        ARCollectionList = Nothing
        If cts.IsCancellationRequested Then
            Throw New OperationCanceledException
        End If
    End Sub
#End Region

#Region "Methods For GetAPAllocations"
    Public Sub GetAPAllocations()
        Dim JVFromCollection As New JournalVoucher
        Dim APAllocationProc As New APAllocationProcess 'APAllocationProcessNew
        Dim WBillSummaryList As New List(Of WESMBillSummary)

        WBillSummaryList = (From x In Me.WESMBillSummaryList
                            Where (x.EndingBalance > x.EnergyWithhold And x.EndingBalance > 0) _
                            Or (Math.Abs(x.BeginningBalance) = 0 And x.EnergyWithholdStatus = EnumEnergyWithholdStatus.UnpaidEWT And x.BalanceType = EnumBalanceType.AP)
                            Select x).ToList()
        With APAllocationProc
            .OffsettingSequence = 0
            ._PaymentProformaEntries = Me.PaymentProformaEntries
            ._DailyInterestRate = Me.DailyInterestRate
            ._AMParticipantsList = Me.AMParticipants
            .GetAPAllocationList(Me.PayAllocDate.CollAllocationDate,
                                WBillSummaryList,
                                getWESMBillSalesAndPurchases,
                                Me.EnergyListCollection,
                                Me.VATonEnergyCollectionList,
                                Me.MFwithVATCollectionList,
                                Me.progress)


            Me._WESMTransCoverSummaryList = .WESMTransCoverSummaryList
            Me._WESMTransDetailsSummaryList = .WESMTransDetailsSummaryList
            Me._WESMTransDetailsSummaryHistoryList = .WESMTransDetailsSummaryHistoryList
            Me.ListofDMCM = .ListofDMCMAP

            Me._EnergyAllocationList = .EnergyAPAllocationList
            Me._VATonEnergyAllocationList = .VATonEnergyAPAllocationList
            Me._MFwithVATAllocationList = .MFWithVATAPAllocationList
        End With
        'Dim SumOfEWT As Decimal = Math.Abs((From x In Me.EnergyAllocationList Where x.PaymentType = EnumPaymentNewType.WithholdingTaxOnEnergy Select x.AllocationAmount).Sum)
        Dim SumOfEWT As Decimal = Math.Abs((From x In getWESMBillSalesAndPurchases Select x.WithholdingTAX).Sum)

        If SumOfCollection > 0 Or SumOfExcess > 0 Or SumOfEWT > 0 Then
            JVFromCollection = Me.PaymentProformaEntries.GenerateJVFromCollection(Me.PayAllocDate.RemittanceDate,
                                                                              SumOfCollection,
                                                                              SumOfExcess,
                                                                              SumOfEWT)
            Me.JournalVoucherList.Add(JVFromCollection)
        End If

        'Set FTF for MF AP that paid by PEMC Account
        Me.CreateFTFForMFAPPaidByPEMCAcc()

        JVFromCollection = Nothing
        APAllocationProc = Nothing
        WBillSummaryList = Nothing

        If cts.IsCancellationRequested Then
            Throw New OperationCanceledException
        End If

    End Sub
#End Region

#Region "Function For GetOffsetting"
    Public Sub GetOffsetting()

        'Dim _OffsettingProcess As New OffsettingProcess
        Dim JVFromPaymentAlloc As New JournalVoucher
        Dim JVFromPaymentEFTCheck As New JournalVoucher

        'Updated by Lance on 2/19/2016
        Dim WBillSummaryList As New List(Of WESMBillSummary)

        WBillSummaryList = (From x In Me.WESMBillSummaryList
                            Where (Math.Abs(x.EndingBalance) > Math.Abs(x.EnergyWithhold) _
                                    Or (Math.Abs(x.EnergyWithhold) <> 0 And x.EnergyWithholdStatus <> EnumEnergyWithholdStatus.NotApplicable)) _
                            And x.EndingBalance <> 0
                            Select x).ToList()
        Using _OffsettingProcess As New OffsettingProcess
            With _OffsettingProcess
                ._PaymentProformaEntries = Me._PaymentProformaEntries
                ._AMParticipants = Me.AMParticipants
                ._SPAInvoiceList = Me.SPAInvoiceList
                .GetOffsettingList(Me.PayAllocDate.CollAllocationDate,
                                    DailyInterestRate,
                                    Me.EnergyAllocationList,
                                    Me.VATonEnergyAllocationList,
                                    Me.MFwithVATAllocationList,
                                    WBillSummaryList,
                                    Me.AMParticipants,
                                    Me.PrevDeferredPaymentList,
                                    Me.progress)
                offsettingIterationCount = .OffsettingSequence
                For Each item In .ListofDMCM
                    Me.ListofDMCM.Add(item)
                Next

                Me._OffsettingMFwithVATCollectionList = .OffsettingMFwithVATCollectionList
                Me._OffsettingMFwithVATAllocationList = .OffsettingMFwithVATAllocation
                Me._OffsettingEnergyCollectionList = .OffsettingEnergyCollectionList
                Me._OffsettingEnergyAllocationList = .OffsettingEnergyAllocationList
                Me._OffsettingVATonEnergyCollectionList = .OffsettingVATonEnergyCollectionList
                Me._OffsettingVATonEnergyAllocationList = .OffsettingVATonEnergyAllocationList

                Me._EnergyShare = .EnergyShare
                Me._VATonEnergyShare = .VATonEnergyShare
                Me._MFwithVATShare = .MFwithVATShare

                Me.CreateFTFForEWT()
                Me.CreateFTFForMFOffsetting()
                Me.CreatePaymentTransferToPR()
                Me.GeneratePaymentORFromList()

                If Me.EnergyListCollection.Count > 0 Or Me.EnergyAllocationList.Count > 0 Or Me.VATonEnergyCollectionList.Count > 0 _
                    Or Me.VATonEnergyAllocationList.Count > 0 Or Me.MFwithVATCollectionList.Count > 0 Or Me.MFwithVATAllocationList.Count > 0 _
                    Or Me.OffsettingEnergyCollectionList.Count > 0 Or Me.OffsettingEnergyAllocationList.Count > 0 Or Me.OffsettingVATonEnergyCollectionList.Count > 0 _
                    Or Me.OffsettingVATonEnergyAllocationList.Count > 0 Or Me.OffsettingMFwithVATCollectionList.Count > 0 Or Me.OffsettingMFwithVATAllocationList.Count > 0 Then
                    Me.CreateNoTransAR()
                End If

                Me.AdjustWESMBillSummaryForWHTAXADJ()

                'Creation of JV PaymentAlloc
                Dim GetMFRefund = (From x In Me.MFwithVATAllocationList Where x.PaymentCategory = EnumCollectionCategory.Cash Select x).ToList()

                If Me.ListofDMCM.Count > 0 Or GetMFRefund.Count > 0 Then
                    JVFromPaymentAlloc = Me._PaymentProformaEntries.GenerateJVFromPaymentAlloc(Me.PayAllocDate.CollAllocationDate, Me.ListofDMCM, GetMFRefund)
                    Me.JournalVoucherList.Add(JVFromPaymentAlloc)
                End If

                'Creation of JV EFT
                JVFromPaymentEFTCheck = Me.PaymentProformaEntries.GenerateJVFromPaymentEFTCheck(Me.PayAllocDate.RemittanceDate,
                                                                                                    Me.RequestForPayment,
                                                                                                    Me.FundTransferform,
                                                                                                    Me.CollFundTransferform,
                                                                                                    Me.PrevDeferredPaymentList,
                                                                                                    Me.CurrentDeferredPaymentList)
                If Not JVFromPaymentEFTCheck.JVNumber = 0 Then
                    Me.JournalVoucherList.Add(JVFromPaymentEFTCheck)
                End If

                Dim newProgress As New ProgressClass
                newProgress.ProgressMsg = "Creating WESMBillSummary History of Balances..."
                Me.progress.Report(newProgress)

                Me.CreateWESMBillSummaryBalance()

                Me.AdjustWESMBillSummaryForWHTAXADJ2()

                newProgress = New ProgressClass
                newProgress.ProgressMsg = "Allocation completed! Please check the result."
                Me.progress.Report(newProgress)

            End With
        End Using
        JVFromPaymentAlloc = Nothing
        JVFromPaymentEFTCheck = Nothing
        WBillSummaryList = Nothing

        If cts.IsCancellationRequested Then
            Throw New OperationCanceledException
        End If
    End Sub
#End Region

#Region "Method For Creating No transaction AR"
    Private Sub CreateNoTransAR()

        Dim GetWESMBillSummaryWithNoTrans As List(Of WESMBillSummary) = (From x In Me.WESMBillSummaryList
                                                                         Select x).ToList()

        'Update AR Invoices with Withhold Tax for Agent Process
        Dim getWESMBillARWtax As List(Of WESMBillSummary) = (From x In Me.WESMBillSummaryList Where x.BalanceType = EnumBalanceType.AR And x.EnergyWithholdStatus = EnumEnergyWithholdStatus.UnpaidEWT Select x).ToList

        For Each y In getWESMBillARWtax
            Dim updateWBSItem As WESMBillSummary = (From x In Me.WESMBillSummaryList Where x.WESMBillSummaryNo = y.WESMBillSummaryNo Select x).FirstOrDefault
            If Not updateWBSItem Is Nothing Then
                updateWBSItem.EnergyWithholdStatus = EnumEnergyWithholdStatus.PaidEWT
                updateWBSItem.StatusUpdate = True
            End If
        Next

        Dim GetEnergyAR As List(Of Long) = (From x In Me.EnergyListCollection Select x.WESMBillSummaryNo).Union _
                                             (From x In Me.OffsettingEnergyCollectionList Select x.WESMBillSummaryNo).Union _
                                             (From x In Me.EnergyAllocationList Select x.WESMBillSummaryNo).Union _
                                             (From x In Me.OffsettingEnergyAllocationList Select x.WESMBillSummaryNo).Distinct.ToList()

        Dim GetEnergyARByBatchNo As List(Of Long) = (From x In Me.EnergyListCollection Select x.WESMBillBatchNo).Union _
                                             (From x In Me.OffsettingEnergyCollectionList Select x.WESMBillBatchNo).Union _
                                             (From x In Me.EnergyAllocationList Select x.WESMBillBatchNo).Union _
                                             (From x In Me.OffsettingEnergyAllocationList Select x.WESMBillBatchNo).Distinct.ToList()

        Dim GetEnergyNoTransList As List(Of WESMBillSummary) = (From x In GetWESMBillSummaryWithNoTrans
                                                                Where (x.ChargeType = EnumChargeType.E And Not GetEnergyAR.Contains(x.WESMBillSummaryNo)) _
                                                                    Or (x.ChargeType = EnumChargeType.E And Not GetEnergyAR.Contains(x.WESMBillSummaryNo) _
                                                                        And GetEnergyARByBatchNo.Contains(x.WESMBillBatchNo))
                                                                Select x).ToList()

        Dim GetVATAR As List(Of Long) = (From x In Me.VATonEnergyCollectionList Select x.WESMBillSummaryNo).Union _
                                          (From x In Me.OffsettingVATonEnergyCollectionList Select x.WESMBillSummaryNo).Union _
                                          (From x In Me.VATonEnergyAllocationList Select x.WESMBillSummaryNo).Union _
                                          (From x In Me.OffsettingVATonEnergyAllocationList Select x.WESMBillSummaryNo).Distinct.ToList()

        Dim GetVATARByBatchNo As List(Of Long) = (From x In Me.VATonEnergyCollectionList Select x.WESMBillBatchNo).Union _
                                          (From x In Me.OffsettingVATonEnergyCollectionList Select x.WESMBillBatchNo).Union _
                                          (From x In Me.VATonEnergyAllocationList Select x.WESMBillBatchNo).Union _
                                          (From x In Me.OffsettingVATonEnergyAllocationList Select x.WESMBillBatchNo).Distinct.ToList()


        Dim GetVATNoTransList As List(Of WESMBillSummary) = (From x In GetWESMBillSummaryWithNoTrans
                                                             Where (x.ChargeType = EnumChargeType.EV And Not GetVATAR.Contains(x.WESMBillSummaryNo)) _
                                                             Or (x.ChargeType = EnumChargeType.EV And Not GetVATAR.Contains(x.WESMBillSummaryNo) _
                                                                 And GetVATARByBatchNo.Contains(x.WESMBillBatchNo))
                                                             Select x).ToList()

        Dim GetMFwitVATAR As List(Of Long) = (From x In Me.MFwithVATCollectionList Select x.WESMBillSummaryNo).Union _
                                               (From x In Me.OffsettingMFwithVATCollectionList Select x.WESMBillSummaryNo).Union _
                                               (From x In Me.MFwithVATAllocationList Select x.WESMBillSummaryNo).Union _
                                               (From x In Me.OffsettingMFwithVATAllocationList Select x.WESMBillSummaryNo).Distinct.ToList()

        Dim GetMFwitVATARByBatchNo As List(Of Long) = (From x In Me.MFwithVATCollectionList Select x.WESMBillBatchNo).Union _
                                               (From x In Me.OffsettingMFwithVATCollectionList Select x.WESMBillBatchNo).Union _
                                               (From x In Me.MFwithVATAllocationList Select x.WESMBillBatchNo).Union _
                                               (From x In Me.OffsettingMFwithVATAllocationList Select x.WESMBillBatchNo).Distinct.ToList()

        Dim GetMFwithVATNoTransList As List(Of WESMBillSummary) = (From x In GetWESMBillSummaryWithNoTrans
                                                                   Where ((x.ChargeType = EnumChargeType.MF And Not GetMFwitVATAR.Contains(x.WESMBillSummaryNo)) Or (x.ChargeType = EnumChargeType.MF And Not GetMFwitVATAR.Contains(x.WESMBillSummaryNo) And GetMFwitVATARByBatchNo.Contains(x.WESMBillBatchNo))) _
                                                                   Or ((x.ChargeType = EnumChargeType.MFV And Not GetMFwitVATAR.Contains(x.WESMBillSummaryNo)) Or (x.ChargeType = EnumChargeType.MFV And Not GetMFwitVATAR.Contains(x.WESMBillSummaryNo) And GetMFwitVATARByBatchNo.Contains(x.WESMBillBatchNo)))
                                                                   Select x).ToList()

        Dim checkBatchIsZeroEnergy = (From x In GetEnergyNoTransList
                                      Where x.BeginningBalance < 0
                                      Group By WESMBillBatchNo = x.WESMBillBatchNo
                                      Into AmountBalance = Sum(x.EndingBalance)
                                      Order By WESMBillBatchNo).ToList()



        Dim listofWBBatchNoNoBalEnergy As New List(Of Long)
        For Each item In checkBatchIsZeroEnergy
            If item.AmountBalance <> 0 Or GetEnergyARByBatchNo.Contains(item.WESMBillBatchNo) Then
                listofWBBatchNoNoBalEnergy.Add(item.WESMBillBatchNo)
            End If
        Next

        Dim updateGetEnergyNoTransList = (From x In GetEnergyNoTransList Where listofWBBatchNoNoBalEnergy.Contains(x.WESMBillBatchNo) Select x).ToList
        For Each item In updateGetEnergyNoTransList
            If item.BalanceType = EnumBalanceType.AR Then
                Me._EnergyNoTransactionARList.Add(New ARCollection(item.WESMBillBatchNo, item.BillPeriod, item.IDNumber.IDNumber.ToString, item.IDNumber.ParticipantID, item.INVDMCMNo,
                                                                 item.EndingBalance, item.EnergyWithhold, item.EndingBalance, item.DueDate, item.NewDueDate, item.WESMBillSummaryNo, EnumCollectionCategory.NoTrans, EnumCollectionType.Energy, item.BillingRemarks))
            Else
                Me._EnergyNoTransactionAPList.Add(New APAllocation(item.WESMBillBatchNo, item.BillPeriod, item.IDNumber.IDNumber.ToString, item.IDNumber.ParticipantID, item.INVDMCMNo,
                                                                   item.EndingBalance, item.EnergyWithhold, item.EndingBalance, item.DueDate, item.NewDueDate, item.WESMBillSummaryNo, EnumCollectionCategory.NoTrans, EnumPaymentNewType.Energy, item.ChargeType, item.BillingRemarks))
            End If
        Next

        Dim checkBatchIsZeroVAT = (From x In GetVATNoTransList
                                   Where x.BeginningBalance < 0
                                   Group By WESMBillBatchNo = x.WESMBillBatchNo
                                   Into AmountBalance = Sum(x.EndingBalance)
                                   Order By WESMBillBatchNo).ToList()

        Dim listofWBBatchNoNoBalVAT As New List(Of Long)
        For Each item In checkBatchIsZeroVAT
            If item.AmountBalance <> 0 Or GetVATARByBatchNo.Contains(item.WESMBillBatchNo) Then
                listofWBBatchNoNoBalVAT.Add(item.WESMBillBatchNo)
            End If
        Next

        Dim updateGetVATNoTransList = (From x In GetVATNoTransList Where listofWBBatchNoNoBalVAT.Contains(x.WESMBillBatchNo) Select x).ToList
        For Each item In updateGetVATNoTransList
            If item.BalanceType = EnumBalanceType.AR Then
                Me._VATNoTransactionARList.Add(New ARCollection(item.WESMBillBatchNo, item.BillPeriod, item.IDNumber.IDNumber.ToString, item.IDNumber.ParticipantID, item.INVDMCMNo,
                                                             item.EndingBalance, item.EnergyWithhold, item.EndingBalance, item.DueDate, item.NewDueDate, item.WESMBillSummaryNo, EnumCollectionCategory.NoTrans, EnumCollectionType.VatOnEnergy, item.BillingRemarks))
            Else
                Me._VATNoTransactionAPList.Add(New APAllocation(item.WESMBillBatchNo, item.BillPeriod, item.IDNumber.IDNumber.ToString, item.IDNumber.ParticipantID, item.INVDMCMNo,
                                                             item.EndingBalance, item.EnergyWithhold, item.EndingBalance, item.DueDate, item.NewDueDate, item.WESMBillSummaryNo, EnumCollectionCategory.NoTrans, EnumPaymentNewType.VatOnEnergy, item.ChargeType, item.BillingRemarks))
            End If

        Next

        Dim checkBatchIsZeroMF = (From x In GetMFwithVATNoTransList
                                  Where x.BeginningBalance < 0
                                  Group By WESMBillBatchNo = x.WESMBillBatchNo
                                  Into AmountBalance = Sum(x.EndingBalance)
                                  Order By WESMBillBatchNo).ToList()

        Dim listofWBBatchNoNoBalMF As New List(Of Long)
        For Each item In checkBatchIsZeroMF
            If item.AmountBalance <> 0 Or GetMFwitVATARByBatchNo.Contains(item.WESMBillBatchNo) Then
                listofWBBatchNoNoBalMF.Add(item.WESMBillBatchNo)
            End If
        Next
        Dim updateGetMFNoTransList = (From x In GetMFwithVATNoTransList Where listofWBBatchNoNoBalMF.Contains(x.WESMBillBatchNo) Select x).ToList
        For Each item In updateGetMFNoTransList
            If item.BalanceType = EnumBalanceType.AR Then
                Dim objEnumCollectionType As New EnumCollectionType
                If item.ChargeType = EnumChargeType.MF Then
                    objEnumCollectionType = EnumCollectionType.MarketFees
                Else
                    objEnumCollectionType = EnumCollectionType.VatOnMarketFees
                End If
                Me._MFwithVATNoTransARList.Add(New ARCollection(item.WESMBillBatchNo, item.BillPeriod, item.IDNumber.IDNumber.ToString, item.IDNumber.ParticipantID, item.INVDMCMNo,
                                                                item.EndingBalance, item.EnergyWithhold, item.EndingBalance, item.DueDate, item.NewDueDate, item.WESMBillSummaryNo, EnumCollectionCategory.NoTrans, objEnumCollectionType, item.BillingRemarks))
            Else
                Dim objEnumPaymentType As New EnumPaymentNewType
                If item.ChargeType = EnumChargeType.MF Then
                    objEnumPaymentType = EnumPaymentNewType.MarketFees
                Else
                    objEnumPaymentType = EnumPaymentNewType.VatOnMarketFees
                End If
                Me._MFwithVATNoTransAPList.Add(New APAllocation(item.WESMBillBatchNo, item.BillPeriod, item.IDNumber.IDNumber.ToString, item.IDNumber.ParticipantID, item.INVDMCMNo,
                                                                item.EndingBalance, item.EnergyWithhold, item.EndingBalance, item.DueDate, item.NewDueDate, item.WESMBillSummaryNo, EnumCollectionCategory.NoTrans, objEnumPaymentType, item.ChargeType, item.BillingRemarks))
            End If
        Next

        GetWESMBillSummaryWithNoTrans = Nothing
        getWESMBillARWtax = Nothing
        GetEnergyAR = Nothing
        GetEnergyARByBatchNo = Nothing
        GetEnergyNoTransList = Nothing
        GetVATAR = Nothing
        GetVATARByBatchNo = Nothing
        GetVATNoTransList = Nothing
        GetMFwitVATAR = Nothing
        GetMFwitVATARByBatchNo = Nothing
        GetMFwithVATNoTransList = Nothing
        checkBatchIsZeroEnergy = Nothing
        listofWBBatchNoNoBalEnergy = Nothing
        updateGetEnergyNoTransList = Nothing
        checkBatchIsZeroMF = Nothing
        listofWBBatchNoNoBalVAT = Nothing
        updateGetVATNoTransList = Nothing
        checkBatchIsZeroMF = Nothing
        listofWBBatchNoNoBalMF = Nothing
        updateGetMFNoTransList = Nothing
    End Sub
#End Region

#Region "Methods For WESMBillSummary Balance"
    Private Sub CreateWESMBillSummaryBalance()
        Dim PaymentWBSHBalance As New List(Of PaymentWBSHistoryBalance)
        Dim WESMBillSummaryReceivablesList = (From x In Me.WESMBillSummaryList
                                              Where x.BeginningBalance < 0 Or x.EndingBalance < 0
                                              Group By WESMBillBatchNo = x.WESMBillBatchNo,
                                                       BillingPeriod = x.BillPeriod,
                                                       OrigDueDate = x.DueDate,
                                                       IDNumber = x.IDNumber,
                                                       ChargeType = x.ChargeType,
                                                       Remarks = x.BillingRemarks
                                              Into AmountBalance = Sum(x.EndingBalance)
                                              Order By WESMBillBatchNo).ToList()

        Dim TotalAmountPerWESMBillAR = (From x In Me.WESMBillSummaryList
                                        Where x.BeginningBalance < 0 Or x.EndingBalance < 0
                                        Group By WESMBillBatchNo = x.WESMBillBatchNo,
                                                BillingPeriod = x.BillPeriod,
                                                OrigDueDate = x.DueDate,
                                                ChargeType = x.ChargeType,
                                                Remarks = x.BillingRemarks
                                        Into TotalBillAmount = Sum(x.BeginningBalance)
                                        Order By WESMBillBatchNo).ToList()

        Dim ObjARDictionary As New Dictionary(Of String, String)
        Dim counter As Integer = 0
        Dim newProgress As ProgressClass

        For Each item In WESMBillSummaryReceivablesList
            If cts.IsCancellationRequested Then
                Throw New OperationCanceledException
            End If

            counter += 1
            Dim _percent As Decimal = Math.Round(CDec(counter / WESMBillSummaryReceivablesList.Count), 2)
            If (counter Mod 1000) = 0 Then
                newProgress = New ProgressClass
                newProgress.ProgressMsg = "Creating WESMBillSummary History of Balances in AR " & _percent.ToString("P")
                Me.progress.Report(newProgress)
            End If
            Dim DicKey As String = item.WESMBillBatchNo.ToString & "|" _
                                 & item.BillingPeriod.ToString & "|" _
                                 & item.OrigDueDate.ToShortDateString & "|" _
                                 & item.ChargeType.ToString & "|" _
                                 & item.IDNumber.IDNumber.ToString

            If Not ObjARDictionary.ContainsKey(DicKey) Then
                ObjARDictionary.Add(DicKey, "NOTHING")
                Dim GetTotalBill = (From x In TotalAmountPerWESMBillAR
                                    Where x.BillingPeriod = item.BillingPeriod _
                                        And x.OrigDueDate = item.OrigDueDate _
                                        And x.ChargeType = item.ChargeType _
                                        And x.WESMBillBatchNo = item.WESMBillBatchNo
                                    Select x.TotalBillAmount).Distinct().FirstOrDefault


                Using _PymntWBSHistoryBalance As New PaymentWBSHistoryBalance
                    With _PymntWBSHistoryBalance
                        .WESMBillBatchNo = item.WESMBillBatchNo
                        .BillingPeriod = item.BillingPeriod
                        .BillingPeriodRemarks = item.Remarks
                        .OriginalDueDate = item.OrigDueDate
                        .TotalBillAmount = CDec(GetTotalBill)
                        .IDNumber = New AMParticipants(item.IDNumber.IDNumber, item.IDNumber.ParticipantID)
                        .AmountBalance = item.AmountBalance
                        .ChargeType = item.ChargeType
                        .BalanceType = EnumBalanceType.AR
                    End With
                    PaymentWBSHBalance.Add(_PymntWBSHistoryBalance)
                End Using
            Else
                If item.AmountBalance <> 0 Then
                    Dim EditPaymntWBSHBalance As PaymentWBSHistoryBalance = (From x In PaymentWBSHBalance
                                                                             Where x.IDNumber.IDNumber = item.IDNumber.IDNumber _
                                                                                 And x.ChargeType = item.ChargeType And x.OriginalDueDate = item.OrigDueDate _
                                                                                 And x.BillingPeriod = item.BillingPeriod And x.BalanceType = EnumBalanceType.AR _
                                                                                 And x.WESMBillBatchNo = item.WESMBillBatchNo
                                                                             Select x).FirstOrDefault
                    If Not EditPaymntWBSHBalance Is Nothing Then
                        EditPaymntWBSHBalance.AmountBalance += item.AmountBalance
                    End If
                End If
            End If
        Next

        Dim WESMBillSummaryPayablesList = (From x In Me.WESMBillSummaryList
                                           Where x.BeginningBalance > 0
                                           Group By WESMBillBatchNo = x.WESMBillBatchNo,
                                                   BillingPeriod = x.BillPeriod,
                                                   OrigDueDate = x.DueDate,
                                                   IDNumber = x.IDNumber,
                                                   ChargeType = x.ChargeType,
                                                   Remarks = x.BillingRemarks
                                           Into AmountBalance = Sum(x.EndingBalance)
                                           Order By WESMBillBatchNo).ToList()

        Dim TotalAmountPerWESMBillAP = (From x In Me.WESMBillSummaryList
                                        Where x.BeginningBalance > 0
                                        Group By WESMBillBatchNo = x.WESMBillBatchNo,
                                                BillingPeriod = x.BillPeriod,
                                                OrigDueDate = x.DueDate,
                                                ChargeType = x.ChargeType,
                                                Remarks = x.BillingRemarks
                                        Into TotalBillAmount = Sum(x.BeginningBalance)
                                        Order By WESMBillBatchNo).ToList()

        Dim WESMBillSummaryPayablesListDistinct = (From x In WESMBillSummaryPayablesList Select x.WESMBillBatchNo, x.BillingPeriod, x.OrigDueDate, x.ChargeType).Distinct.ToList()
        Dim ObjAPDictionary As New Dictionary(Of String, String)
        counter = 0
        For Each item In WESMBillSummaryPayablesList
            If cts.IsCancellationRequested Then
                Throw New OperationCanceledException
            End If

            counter += 1
            Dim _percent As Decimal = Math.Round(CDec(counter / WESMBillSummaryPayablesList.Count), 2)
            If (counter Mod 1000) = 0 Then
                newProgress = New ProgressClass
                newProgress.ProgressMsg = "Creating WESMBillSummary History of Balances in AP " & _percent.ToString("P")
                Me.progress.Report(newProgress)
            End If
            Dim DicKey As String = item.WESMBillBatchNo.ToString & "|" _
                                   & item.BillingPeriod.ToString & "|" _
                                   & item.OrigDueDate.ToShortDateString & "|" _
                                   & item.ChargeType.ToString & "|" _
                                   & item.IDNumber.IDNumber.ToString

            If Not ObjAPDictionary.ContainsKey(DicKey) Then
                ObjAPDictionary.Add(DicKey, "NOTHING")
                Dim GetTotalBill = (From x In TotalAmountPerWESMBillAP
                                    Where x.BillingPeriod = item.BillingPeriod _
                                    And x.OrigDueDate = item.OrigDueDate _
                                    And x.ChargeType = item.ChargeType _
                                    And x.WESMBillBatchNo = item.WESMBillBatchNo
                                    Select x.TotalBillAmount).Distinct().FirstOrDefault

                Using _PymntWBSHistoryBalance As New PaymentWBSHistoryBalance
                    With _PymntWBSHistoryBalance
                        .WESMBillBatchNo = item.WESMBillBatchNo
                        .BillingPeriod = item.BillingPeriod
                        .BillingPeriodRemarks = item.Remarks
                        .OriginalDueDate = item.OrigDueDate
                        .TotalBillAmount = CDec(GetTotalBill)
                        .IDNumber = New AMParticipants(item.IDNumber.IDNumber, item.IDNumber.ParticipantID)
                        .AmountBalance = item.AmountBalance
                        .ChargeType = item.ChargeType
                        .BalanceType = EnumBalanceType.AP
                    End With

                    PaymentWBSHBalance.Add(_PymntWBSHistoryBalance)
                End Using
            Else
                If item.AmountBalance <> 0 Then
                    Dim EditPaymntWBSHBalance As PaymentWBSHistoryBalance = (From x In PaymentWBSHBalance
                                                                             Where x.IDNumber.IDNumber = item.IDNumber.IDNumber _
                                                                             And x.ChargeType = item.ChargeType And x.OriginalDueDate = item.OrigDueDate _
                                                                             And x.WESMBillBatchNo = item.WESMBillBatchNo _
                                                                             And x.BillingPeriod = item.BillingPeriod And x.BalanceType = EnumBalanceType.AP
                                                                             Select x).FirstOrDefault

                    If Not EditPaymntWBSHBalance Is Nothing Then
                        EditPaymntWBSHBalance.AmountBalance += item.AmountBalance
                    End If
                End If
            End If
        Next

        Me.PymntWBSHistoryBalance = PaymentWBSHBalance
        PaymentWBSHBalance = Nothing
        WESMBillSummaryReceivablesList = Nothing
        TotalAmountPerWESMBillAR = Nothing
        ObjARDictionary = Nothing
        WESMBillSummaryPayablesList = Nothing
        TotalAmountPerWESMBillAP = Nothing
        WESMBillSummaryPayablesListDistinct = Nothing
        ObjAPDictionary = Nothing
        If cts.IsCancellationRequested Then
            Throw New OperationCanceledException
        End If
    End Sub
#End Region

#Region "Functions For Offsetting of AR and AP DataTable"
    Public Function GetShareDetailsARDT(ByVal OffsettingSeq As String,
                                        ByVal WESMBillIDNumber As String,
                                        ByVal BillingPeriod As String,
                                        ByVal ShareType As List(Of PaymentShare)) As DataTable
        Dim OffsetSeqArr() As String = OffsettingSeq.Split(CChar(";"))
        Dim OffsettingARBreakDown As New List(Of PaymentShare)

        OffsettingARBreakDown = (From x In ShareType
                                 Where x.IDNumber.ToString() = WESMBillIDNumber
                                 Select x).ToList()

        Dim OARDDT As New DataTable
        OARDDT.TableName = "GetOffsettingARDetails"

        With OARDDT.Columns
            .Add("OffsetSeqNo", GetType(String))
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("AmountShare", GetType(String))
            .Add("AmountOffset", GetType(String))
            .Add("AmountBalance", GetType(String))
            .Add("PaymentType", GetType(String))
        End With

        For Each OItem In OffsettingARBreakDown
            Dim row As DataRow
            Dim AmountToOffset As Decimal = 0
            For Each OSDItem In OItem.AmountOffset
                For Each Seq In OffsetSeqArr
                    If OSDItem.BatchGroupSequence = CInt(Seq) And OSDItem.BillingPeriod = CLng(BillingPeriod) Then
                        row = OARDDT.NewRow()
                        row("OffsetSeqNo") = OItem.OffsettingSequence
                        row("BillingPeriod") = OItem.BillingPeriod
                        row("IDNumber") = OItem.IDNumber
                        row("InvoiceNumber") = OItem.InvoiceNumber
                        row("AmountShare") = FormatNumber(OItem.AmountShare, UseParensForNegativeNumbers:=TriState.True)
                        row("AmountOffset") = FormatNumber(If(IsDBNull(row("AmountOffset")), 0, CDec(row("AmountOffset"))) + OSDItem.OffsetAmount, UseParensForNegativeNumbers:=TriState.True)
                        AmountToOffset += OSDItem.OffsetAmount
                        row("AmountBalance") = FormatNumber(OItem.AmountShare - AmountToOffset, UseParensForNegativeNumbers:=TriState.True)
                        row("PaymentType") = OItem.PaymentType.ToString
                        OARDDT.Rows.Add(row)
                    End If
                Next
            Next
        Next
        OARDDT.AcceptChanges()
        Return OARDDT
    End Function

    Public Function GetShareDetailsAPDT(ByVal OffsettingSeq As String,
                                        ByVal BillingPeriod As String,
                                        ByVal IDNumber As String,
                                        ByVal InvNumber As String,
                                        ByVal ShareType As List(Of PaymentShare)) As DataTable
        Dim OffsetSeqArr() As String = OffsettingSeq.Split(CChar(";"))
        Dim OffsettingARBreakDown As New List(Of PaymentShare)

        OffsettingARBreakDown = (From x In ShareType
                                 Where x.BillingPeriod.ToString = BillingPeriod _
                                 And x.IDNumber.ToString() = IDNumber _
                                 And x.InvoiceNumber.ToString = InvNumber _
                                 And OffsetSeqArr.Contains(x.OffsettingSequence.ToString)
                                 Select x).ToList()

        Dim ODAPDDT As New DataTable
        ODAPDDT.TableName = "ShareOnInvoice"

        With ODAPDDT.Columns
            .Add("OffsetSeqNo", GetType(String))
            .Add("BillingPeriod", GetType(Long))
            .Add("IDNumber", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("AmountShare", GetType(String))
            .Add("AmountOffset", GetType(String))
        End With

        For Each OItem In OffsettingARBreakDown
            Dim row As DataRow
            Dim AmountToOffset As Decimal = 0
            For Each OSDItem In OItem.AmountOffset
                row = ODAPDDT.NewRow()
                row("OffsetSeqNo") = OSDItem.BatchGroupSequence
                row("BillingPeriod") = OSDItem.BillingPeriod
                row("IDNumber") = OSDItem.IDNumber
                row("InvoiceNumber") = OSDItem.InvoiceNumber
                row("AmountShare") = FormatNumber(OItem.AmountShare, UseParensForNegativeNumbers:=TriState.True)
                row("AmountOffset") = FormatNumber(If(IsDBNull(row("AmountOffset")), 0, CDec(row("AmountOffset"))) + OSDItem.OffsetAmount, UseParensForNegativeNumbers:=TriState.True)
                ODAPDDT.Rows.Add(row)
            Next
        Next
        ODAPDDT.AcceptChanges()
        Return ODAPDDT
    End Function

    Public Function GetOffsettingARDetailsDT(ByVal OffsettingSeq As String,
                                             ByVal IDNumber As String,
                                             ByVal BillingPeriod As String,
                                             ByVal ARCollList As List(Of ARCollection)) As DataTable
        Dim OffsetSeqArr() As String = OffsettingSeq.Split(CChar(";"))
        Dim OffsettingARDetails As New List(Of ARCollection)

        OffsettingARDetails = (From x In ARCollList
                               Where OffsetSeqArr.Contains(x.OffsettingSequence.ToString) _
                               And x.BillingPeriod = CLng(BillingPeriod) _
                               And x.IDNumber = IDNumber Select x).ToList

        Dim OARDDT As New DataTable
        OARDDT.TableName = "GetOffsettingARDetails"

        With OARDDT.Columns
            .Add("OffsetSeqNo", GetType(String))
            .Add("BillingPeriod", GetType(Integer))
            .Add("IDNumber", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(String))
            .Add("EndingBalance", GetType(String))
            .Add("EnergyWithHolding", GetType(String))
            .Add("AllocationDate", GetType(String))
            .Add("AllocationAmount", GetType(String))
            .Add("NewDueDate", GetType(String))
            .Add("NewEndingBalance", GetType(String))
            .Add("CollectionType", GetType(String))
            .Add("Remarks", GetType(String))
        End With

        For Each OItem In OffsettingARDetails
            Dim row As DataRow
            row = OARDDT.NewRow()
            row("OffsetSeqNo") = OItem.OffsettingSequence
            row("BillingPeriod") = OItem.BillingPeriod
            row("IDNumber") = OItem.IDNumber
            row("InvoiceNumber") = OItem.InvoiceNumber
            row("DueDate") = OItem.DueDate.ToShortDateString()
            row("EndingBalance") = FormatNumber(OItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
            row("EnergyWithHolding") = FormatNumber(OItem.EnergyWithHold, UseParensForNegativeNumbers:=TriState.True)
            row("NewDueDate") = OItem.NewDueDate.ToShortDateString()
            row("AllocationDate") = OItem.AllocationDate.ToShortDateString()
            row("AllocationAmount") = FormatNumber(OItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
            row("NewEndingBalance") = FormatNumber(OItem.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True)
            row("CollectionType") = ([Enum].GetName(GetType(EnumCollectionType), OItem.CollectionType)).ToString
            'For Each DMCMNumber In OItem.ListofDMCM
            '    row("Remarks") = If(row("Remarks").ToString.Length = 0, Nothing, row("Remarks").ToString & ";") & DMCMNumber
            'Next
            row("Remarks") = OItem.GeneratedDMCM.ToString

            OARDDT.Rows.Add(row)
        Next
        OARDDT.AcceptChanges()
        Return OARDDT
    End Function

    Public Function GetOffsettingAPDetailsDT(ByVal OffsettingSeq As String,
                                            ByVal BillingPeriod As String,
                                            ByVal IDNumber As String,
                                            ByVal InvNumber As String,
                                            ByVal APAllocList As List(Of APAllocation)) As DataTable
        Dim OffsetSeqArr() As String = OffsettingSeq.Split(CChar(";"))
        Dim OffsettingAPDetails As New List(Of APAllocation)

        OffsettingAPDetails = (From x In APAllocList
                               Where x.BillingPeriod = CLng(BillingPeriod) _
                               And x.IDNumber = IDNumber _
                               And x.InvoiceNumber = InvNumber _
                               And OffsetSeqArr.Contains(x.OffsettingSequence.ToString)
                               Select x).ToList

        Dim OAPDDT As New DataTable
        OAPDDT.TableName = "GetOffsettingAPDetails"

        With OAPDDT.Columns
            .Add("OffsetSeqNo", GetType(String))
            .Add("BillingPeriod", GetType(Integer))
            .Add("IDNumber", GetType(String))
            .Add("InvoiceNumber", GetType(String))
            .Add("DueDate", GetType(String))
            .Add("EndingBalance", GetType(String))
            .Add("EnergyWithHolding", GetType(String))
            .Add("AllocationDate", GetType(String))
            .Add("AllocationAmount", GetType(String))
            .Add("NewDueDate", GetType(String))
            .Add("NewEndingBalance", GetType(String))
            .Add("PaymentType", GetType(String))
            .Add("Remarks", GetType(String))
        End With

        For Each OItem In OffsettingAPDetails
            Dim row As DataRow
            row = OAPDDT.NewRow()
            row("OffsetSeqNo") = OItem.OffsettingSequence
            row("BillingPeriod") = OItem.BillingPeriod
            row("IDNumber") = OItem.IDNumber
            row("InvoiceNumber") = OItem.InvoiceNumber
            row("DueDate") = OItem.DueDate.ToShortDateString()
            row("EndingBalance") = FormatNumber(OItem.EndingBalance, UseParensForNegativeNumbers:=TriState.True)
            row("EnergyWithHolding") = FormatNumber(OItem.EnergyWithHold, UseParensForNegativeNumbers:=TriState.True)
            row("NewDueDate") = OItem.NewDueDate.ToShortDateString()
            row("AllocationDate") = OItem.AllocationDate.ToShortDateString()
            row("AllocationAmount") = FormatNumber(OItem.AllocationAmount, UseParensForNegativeNumbers:=TriState.True)
            row("NewEndingBalance") = FormatNumber(OItem.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True)
            row("PaymentType") = ([Enum].GetName(GetType(EnumPaymentNewType), OItem.PaymentType)).ToString
            row("Remarks") = OItem.GeneratedDMCM
            'For Each dmcm In OItem.ListofDMCM
            '    row("Remarks") = If(row("Remarks").ToString.Length > 0, ";", "") & dmcm
            'Next
            OAPDDT.Rows.Add(row)
        Next
        OAPDDT.AcceptChanges()
        Return OAPDDT
    End Function


#End Region

#Region "Methods and Function For Generation of DMCM"
    Public Sub CreateDMCMSummaryDoc(ByVal SavingPathName As String)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet1 As Excel.Worksheet
        Dim xlWorkSheet2 As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlTotalOffsettingCM As Excel.Range
        Dim xlTotalOffsettingDA As Excel.Range
        Dim xlTotalDefIntDM As Excel.Range
        Dim xlTotalDefIntAPV As Excel.Range
        Dim xlDMCMSummary As Excel.Range

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        Dim TemplatePathFile = AppDomain.CurrentDomain.BaseDirectory & "Excel_Template\DMCMSummary.xltx"

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(TemplatePathFile)
        xlWorkSheet1 = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet2 = CType(xlWorkBook.Sheets(2), Excel.Worksheet)

        Dim TotalOffsettingCMArr As Object(,) = New Object(,) {}
        Dim TotalOffsettingDAArr As Object(,) = New Object(,) {}
        Dim TotalDefIntDMArr As Object(,) = New Object(,) {}
        Dim TotalDefIntAPVArr As Object(,) = New Object(,) {}
        Dim DMCMSummaryList As Object(,) = New Object(,) {}

        Dim iDMCMSummaryList As List(Of DebitCreditMemoSummaryNew) = Me.GetDMCMSummaryList()

        Dim ForTotalOffsettingCM = (From x In iDMCMSummaryList Where x.DMCMSummaryType = EnumDMCMSummaryType.TotalOffsettingCM Select x).ToList()
        ReDim TotalOffsettingCMArr(ForTotalOffsettingCM.Count - 1, 4)

        rowIndex = 0
        For Each item In ForTotalOffsettingCM
            TotalOffsettingCMArr(rowIndex, 0) = BFactory.GenerateBIRDocumentNumber(item.DMCMNumber, BIRDocumentsType.DMCM)
            TotalOffsettingCMArr(rowIndex, 1) = item.AMParticipantInfo.IDNumber
            TotalOffsettingCMArr(rowIndex, 2) = item.AMParticipantInfo.ParticipantID
            TotalOffsettingCMArr(rowIndex, 3) = item.AMParticipantInfo.FullName
            TotalOffsettingCMArr(rowIndex, 4) = FormatNumber(item.DMCMAmount, UseParensForNegativeNumbers:=TriState.True)
            rowIndex += 1
        Next

        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(5, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(rowIndex + 4, 5), Excel.Range)
        xlTotalOffsettingCM = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlTotalOffsettingCM.Value = TotalOffsettingCMArr
        xlTotalOffsettingCM.Range(Cell1:="W5", Cell2:="W" & rowIndex + 4).Style = "Comma"

        Dim ForTotalOffsettingDA = (From x In iDMCMSummaryList Where x.DMCMSummaryType = EnumDMCMSummaryType.TotalOffsettingDA Select x).ToList()
        ReDim TotalOffsettingDAArr(ForTotalOffsettingDA.Count - 1, 4)
        rowIndex = 0
        For Each item In ForTotalOffsettingDA
            TotalOffsettingDAArr(rowIndex, 0) = BFactory.GenerateBIRDocumentNumber(item.DMCMNumber, BIRDocumentsType.DMCM)
            TotalOffsettingDAArr(rowIndex, 1) = item.AMParticipantInfo.IDNumber
            TotalOffsettingDAArr(rowIndex, 2) = item.AMParticipantInfo.ParticipantID
            TotalOffsettingDAArr(rowIndex, 3) = item.AMParticipantInfo.FullName
            TotalOffsettingDAArr(rowIndex, 4) = FormatNumber(item.DMCMAmount, UseParensForNegativeNumbers:=TriState.True)
            rowIndex += 1
        Next

        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(5, 7), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(rowIndex + 4, 11), Excel.Range)
        xlTotalOffsettingDA = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlTotalOffsettingDA.Value = TotalOffsettingDAArr
        xlTotalOffsettingDA.Range(Cell1:="Q5", Cell2:="Q" & rowIndex + 4).Style = "Comma"

        Dim ForTotalDefIntDM = (From x In iDMCMSummaryList Where x.DMCMSummaryType = EnumDMCMSummaryType.TotalDefIntDM Select x).ToList()
        ReDim TotalDefIntDMArr(ForTotalDefIntDM.Count - 1, 4)
        rowIndex = 0
        For Each item In ForTotalDefIntDM
            TotalDefIntDMArr(rowIndex, 0) = BFactory.GenerateBIRDocumentNumber(item.DMCMNumber, BIRDocumentsType.DMCM)
            TotalDefIntDMArr(rowIndex, 1) = item.AMParticipantInfo.IDNumber
            TotalDefIntDMArr(rowIndex, 2) = item.AMParticipantInfo.ParticipantID
            TotalDefIntDMArr(rowIndex, 3) = item.AMParticipantInfo.FullName
            TotalDefIntDMArr(rowIndex, 4) = FormatNumber(item.DMCMAmount, UseParensForNegativeNumbers:=TriState.True)
            rowIndex += 1
        Next

        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(5, 13), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(rowIndex + 4, 17), Excel.Range)
        xlTotalDefIntDM = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlTotalDefIntDM.Value = TotalDefIntDMArr
        xlTotalDefIntDM.Range(Cell1:="K5", Cell2:="K" & rowIndex + 4).Style = "Comma"

        Dim ForTotalDefIntAPV = (From x In iDMCMSummaryList Where x.DMCMSummaryType = EnumDMCMSummaryType.TotalDefIntAPV Select x).ToList()
        ReDim TotalDefIntAPVArr(ForTotalDefIntAPV.Count - 1, 4)
        rowIndex = 0
        For Each item In ForTotalDefIntAPV
            TotalDefIntAPVArr(rowIndex, 0) = BFactory.GenerateBIRDocumentNumber(item.DMCMNumber, BIRDocumentsType.DMCM)
            TotalDefIntAPVArr(rowIndex, 1) = item.AMParticipantInfo.IDNumber
            TotalDefIntAPVArr(rowIndex, 2) = item.AMParticipantInfo.ParticipantID
            TotalDefIntAPVArr(rowIndex, 3) = item.AMParticipantInfo.FullName
            TotalDefIntAPVArr(rowIndex, 4) = FormatNumber(item.DMCMAmount, UseParensForNegativeNumbers:=TriState.True)
            rowIndex += 1
        Next

        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(5, 19), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(rowIndex + 4, 23), Excel.Range)
        xlTotalDefIntAPV = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlTotalDefIntAPV.Value = TotalDefIntAPVArr
        xlTotalDefIntAPV.Range(Cell1:="E5", Cell2:="E" & rowIndex + 4).Style = "Comma"

        Dim DistinctParticipants = (From x In iDMCMSummaryList Select x.AMParticipantInfo).Distinct.ToList()
        Dim DistinctParticipantDic As New Dictionary(Of String, Integer)
        ReDim DMCMSummaryList(DistinctParticipants.Count - 1, 6)
        rowIndex = 0
        For Each item In DistinctParticipants
            If Not DistinctParticipantDic.ContainsKey(item.IDNumber) Then
                DMCMSummaryList(rowIndex, 0) = item.IDNumber
                DMCMSummaryList(rowIndex, 1) = item.ParticipantID
                DMCMSummaryList(rowIndex, 2) = item.FullName
                DMCMSummaryList(rowIndex, 3) = "0.00"
                DMCMSummaryList(rowIndex, 4) = "0.00"
                DMCMSummaryList(rowIndex, 5) = "0.00"
                DMCMSummaryList(rowIndex, 6) = "0.00"
                DistinctParticipantDic.Add(item.IDNumber, rowIndex)
                rowIndex += 1
            End If
        Next

        For Each item In iDMCMSummaryList
            If DistinctParticipantDic.ContainsKey(item.AMParticipantInfo.IDNumber) Then
                Dim row As Integer = DistinctParticipantDic(item.AMParticipantInfo.IDNumber)
                Select Case item.DMCMSummaryType
                    Case EnumDMCMSummaryType.TotalOffsettingCM
                        DMCMSummaryList(row, 3) = FormatNumber(CDec(DMCMSummaryList(row, 3)) + item.DMCMAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumDMCMSummaryType.TotalOffsettingDA
                        DMCMSummaryList(row, 4) = FormatNumber(CDec(DMCMSummaryList(row, 4)) + item.DMCMAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumDMCMSummaryType.TotalDefIntDM
                        DMCMSummaryList(row, 5) = FormatNumber(CDec(DMCMSummaryList(row, 5)) + item.DMCMAmount, UseParensForNegativeNumbers:=TriState.True)
                    Case EnumDMCMSummaryType.TotalDefIntAPV
                        DMCMSummaryList(row, 6) = FormatNumber(CDec(DMCMSummaryList(row, 6)) + item.DMCMAmount, UseParensForNegativeNumbers:=TriState.True)
                End Select
            End If
        Next

        xlRowRange1 = DirectCast(xlWorkSheet2.Cells(5, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet2.Cells(rowIndex + 4, 7), Excel.Range)
        xlDMCMSummary = xlWorkSheet2.Range(xlRowRange1, xlRowRange2)
        xlDMCMSummary.Value = DMCMSummaryList
        xlDMCMSummary.Range(Cell1:="D5", Cell2:="G" & rowIndex + 4).Style = "Comma"


        Dim FileName As String = "DMCMSummaryReport " & Me._PayAllocDate.CollAllocationDate.ToString("dd-MMM-yyyy")
        xlApp.DisplayAlerts = False
        xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlExcel5, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        xlWorkBook.Close(False)
        xlApp.Quit()

        releaseObject(xlDMCMSummary)
        releaseObject(xlTotalDefIntAPV)
        releaseObject(xlTotalDefIntDM)
        releaseObject(xlTotalOffsettingDA)
        releaseObject(xlTotalOffsettingCM)
        releaseObject(xlRowRange2)
        releaseObject(xlRowRange1)
        releaseObject(xlWorkSheet2)
        releaseObject(xlWorkSheet1)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Public Function GetDMCMSummaryList() As List(Of DebitCreditMemoSummaryNew)
        Dim DMCMSummaryNewList As New List(Of DebitCreditMemoSummaryNew)
        For Each _DMCM In Me.ListofDMCM
            Dim _DMCMSummaryNewItem As New DebitCreditMemoSummaryNew
            Using _DMCMSummaryNewItem
                With _DMCMSummaryNewItem
                    .AMParticipantInfo = (From x In Me.AMParticipants Where x.IDNumber = _DMCM.IDNumber.ToString Select x).FirstOrDefault
                    .DMCMNumber = _DMCM.DMCMNumber
                    .UpdatedBy = AMModule.UserName
                    Select Case _DMCM.TransType
                        Case EnumDMCMTransactionType.PaymentSetupClearingOnMFAndMFV,
                            EnumDMCMTransactionType.PaymentSetupClearingOnEWTAndWVAT,
                            EnumDMCMTransactionType.PaymentSetupClearingAROnEnergyAndDefaultInterest,
                            EnumDMCMTransactionType.PaymentSetupClearingAROnVATonEnergy

                            .DMCMSummaryType = EnumDMCMSummaryType.TotalOffsettingCM
                            .DMCMAmount = (From x In _DMCM.DMCMDetails Select x.Credit).Sum()

                        Case EnumDMCMTransactionType.PaymentSetupOffsettingAROfDefaultInterestOnEnergy,
                            EnumDMCMTransactionType.PaymentSetupClearingOnMFAndMFVDefaultInterest,
                            EnumDMCMTransactionType.PaymentSetupClearingOnEWTAndWVATDefaultInterest

                            .DMCMSummaryType = EnumDMCMSummaryType.TotalDefIntDM
                            .DMCMAmount = (From x In _DMCM.DMCMDetails Select x.Credit).Sum()

                        Case EnumDMCMTransactionType.PaymentSetupClearingAPOfEnergyAndDefaultInterest,
                            EnumDMCMTransactionType.PaymentSetupClearingAPOfVATonEnergy

                            .DMCMSummaryType = EnumDMCMSummaryType.TotalOffsettingDA
                            .DMCMAmount = (From x In _DMCM.DMCMDetails Select x.Debit).Sum()

                        Case EnumDMCMTransactionType.PaymentSetupOffsettingAPOfDefaultInterestOnEnergy,
                            EnumDMCMTransactionType.PaymentSetupEnergyDefaultInterest
                            .DMCMSummaryType = EnumDMCMSummaryType.TotalDefIntAPV
                            .DMCMAmount = (From x In _DMCM.DMCMDetails Select x.Debit).Sum()
                        Case Else

                    End Select
                End With
                DMCMSummaryNewList.Add(_DMCMSummaryNewItem)
            End Using
        Next

        Me._ListOfDMCMSummary = DMCMSummaryNewList

        Return DMCMSummaryNewList
    End Function

    Public Function CreateDMCMList(ByVal dt As DataTable,
                                   ByVal DMCMList As List(Of Object),
                                   ByVal IDnumber As String) As DataTable

        Dim _DMCM As New DebitCreditMemo
        Dim Signatory As DocSignatories = WBillHelper.GetSignatories("DMCM").First
        Dim ListofAccountingCodes As List(Of AccountingCode) = WBillHelper.GetAccountingCodes()
        Dim _ParticipantInfo As AMParticipants = (From x In Me.AMParticipants Where x.IDNumber = IDnumber Select x).FirstOrDefault

        For Each DMCMItem In DMCMList
            Dim SelectedDMCM = (From x In Me.ListofDMCM Where x.DMCMNumber = CLng(DMCMItem) Select x).FirstOrDefault
            If Not SelectedDMCM Is Nothing Then
                With SelectedDMCM
                    For Each Item In .DMCMDetails
                        Dim _Description As String = (From x In ListofAccountingCodes Where x.AccountCode = Item.AccountCode Select x.Description).FirstOrDefault
                        Dim row As DataRow = dt.NewRow()
                        row("ID_NUMBER") = _ParticipantInfo.IDNumber & " / " & _ParticipantInfo.ParticipantID
                        row("BUSINESS_STYLE") = _ParticipantInfo.BusinessStyle
                        row("ADDRESS") = _ParticipantInfo.ParticipantAddress
                        row("PARTICIPANT_TIN") = _ParticipantInfo.TIN
                        row("DMCM_NO") = BFactory.GenerateBIRDocumentNumber(.DMCMNumber, BIRDocumentsType.DMCM)
                        row("JV_NO") = BFactory.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
                        row("PARTICULARS") = .Particulars
                        row("ACCOUNT_CODE") = Item.AccountCode
                        row("DESCRIPTION") = _Description
                        row("PREPARED_BY") = AMModule.FullName
                        row("CHECKED_BY") = Signatory.Signatory_1
                        row("APPROVED_BY") = Signatory.Signatory_2
                        row("POSITION1") = AMModule.Position
                        row("POSITION2") = Signatory.Position_1
                        row("POSITION3") = Signatory.Position_2
                        row("PARTICIPANT_NAME") = _ParticipantInfo.FullName
                        row("DR_AMOUNT") = Item.Debit
                        row("CR_AMOUNT") = Item.Credit
                        row("PARTICIPANT_ID") = _ParticipantInfo.ParticipantID
                        row("EWT") = .EWT
                        row("EWV") = .EWV
                        row("VATABLE") = .Vatable
                        row("VAT") = .VAT
                        row("VAT_EXEMPT_SALE") = .VATExempt
                        row("VAT_ZERO") = .VatZeroRated
                        row("OTHERS") = .Others
                        row("TOTAL_AMOUNT_DUE") = .TotalAmountDue
                        row("BIR_VALUE") = AMModule.BIRPermitNumber
                        dt.Rows.Add(row)
                    Next
                End With
            End If
        Next
        dt.AcceptChanges()
        Return dt

    End Function

    Public Function CreateDMCM(ByVal dt As DataTable,
                               ByVal SeqNo As Integer,
                               ByVal IDnumber As String,
                               ByVal BillPeriod As String,
                               ByVal InvNumber As String,
                               ByVal Signatory As DocSignatories,
                               ByVal ListofAccountingCodes As List(Of AccountingCode)) As DataTable

        Dim _DMCM As New DebitCreditMemo

        Dim _ParticipantInfo As New AMParticipants
        _ParticipantInfo = (From x In Me.AMParticipants Where x.IDNumber = IDnumber Select x).FirstOrDefault

        Dim ARCollectionLIst = (From x In Me.EnergyAllocationList
                                Where x.OffsettingSequence = SeqNo _
                                And x.BillingPeriod = CLng(BillPeriod) _
                                And x.IDNumber = IDnumber _
                                And x.InvoiceNumber = InvNumber _
                                And x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy
                                Select x).ToList

        For Each ARItem In ARCollectionLIst
            Dim SelectedDMCM = (From x In Me.ListofDMCM Where x.DMCMNumber = ARItem.GeneratedDMCM Select x).FirstOrDefault
            With SelectedDMCM
                For Each Item In .DMCMDetails
                    Dim _Description As String = (From x In ListofAccountingCodes Where x.AccountCode = Item.AccountCode Select x.Description).FirstOrDefault
                    Dim row As DataRow = dt.NewRow()
                    row("ID_NUMBER") = _ParticipantInfo.IDNumber & " / " & _ParticipantInfo.ParticipantID
                    row("ADDRESS") = _ParticipantInfo.ParticipantAddress
                    row("PARTICIPANT_TIN") = _ParticipantInfo.TIN
                    row("DMCM_NO") = BFactory.GenerateBIRDocumentNumber(.DMCMNumber, BIRDocumentsType.DMCM)
                    row("JV_NO") = BFactory.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
                    row("PARTICULARS") = .Particulars
                    row("ACCOUNT_CODE") = Item.AccountCode
                    row("DESCRIPTION") = _Description
                    row("PREPARED_BY") = AMModule.FullName
                    row("CHECKED_BY") = Signatory.Signatory_1
                    row("APPROVED_BY") = Signatory.Signatory_2
                    row("POSITION1") = AMModule.Position
                    row("POSITION2") = Signatory.Position_1
                    row("POSITION3") = Signatory.Position_2
                    row("PARTICIPANT_NAME") = _ParticipantInfo.FullName
                    row("DR_AMOUNT") = Item.Debit
                    row("CR_AMOUNT") = Item.Credit
                    row("PARTICIPANT_ID") = _ParticipantInfo.ParticipantID
                    row("EWT") = .EWT
                    row("EWV") = .EWV
                    row("VATABLE") = .Vatable
                    row("VAT") = .VAT
                    row("VAT_EXEMPT_SALE") = .VATExempt
                    row("VAT_ZERO") = .VatZeroRated
                    row("OTHERS") = .Others
                    row("TOTAL_AMOUNT_DUE") = .TotalAmountDue
                    row("BIR_VALUE") = AMModule.BIRPermitNumber
                    dt.Rows.Add(row)
                Next
            End With
        Next
        dt.AcceptChanges()
        Return dt
    End Function
#End Region

#Region "Method and Functions For Generation of Collection And Payment Report on MS-Excel"
    Public Sub CreateCollAndPaySummReport(ByVal SavingPathName As String)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet1 As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlEnergyAllocation As Excel.Range
        Dim xlEnergyBalance As Excel.Range
        Dim xlVATAllocation As Excel.Range
        Dim xlVATBalance As Excel.Range
        Dim xlMFAllocation As Excel.Range
        Dim xlCollAndPayment As Excel.Range
        Dim xlFrontPage As Excel.Range
        Dim xlEFTFormat As Excel.Range
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0
        Dim TemplatePathFile = AppDomain.CurrentDomain.BaseDirectory & "Excel_Template\CollectionPaymentSummary.xltm"

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(TemplatePathFile)

        'Start EnergyAllocation ****************************************************************************************************************************************************
        Dim ForEnergyCollection = (From x In Me.EnergyNoTransactionARList Select x).Union _
                                  (From x In Me.EnergyListCollection Select x).Union _
                                  (From x In Me.OffsettingEnergyCollectionList Select x).ToList()

        Dim ForEnergyAllocation = (From x In Me.EnergyNoTransactionAPList Select x).Union _
                                  (From x In Me.EnergyAllocationList Select x).Union _
                                  (From x In Me.OffsettingEnergyAllocationList Select x).ToList()

        Dim DistinctEBP = (From x In ForEnergyCollection
                           Order By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate
                           Group x By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate
                           Into y = Group Let Grp = New With {.WESMBillBatchNo = WESMBillBatchNo, .BillingPeriod = BillingPeriod, .DueDate = DueDate}
                           Select Grp).ToList()

        Dim DistinctSequenceE = (From x In ForEnergyCollection Select x.OffsettingSequence Order By OffsettingSequence).Distinct().ToList()

        'Energy Allocation Header Setup
        Dim EnergyAllocHeader As Object(,) = New Object(,) {}
        ReDim EnergyAllocHeader(1, 15)
        EnergyAllocHeader(1, 0) = "BillingParticulars"
        EnergyAllocHeader(1, 1) = "BillingBatchNo"
        EnergyAllocHeader(1, 2) = "IDNumber"
        EnergyAllocHeader(1, 3) = "ParticipantID"
        EnergyAllocHeader(1, 4) = "InvoiceNumber"
        EnergyAllocHeader(1, 5) = "OrigDueDate"
        EnergyAllocHeader(1, 6) = "NewDueDate"
        EnergyAllocHeader(1, 7) = "WithHoldingTax"
        EnergyAllocHeader(1, 8) = "OutstandingBalance"
        EnergyAllocHeader(1, 9) = Nothing

        EnergyAllocHeader(0, 10) = Me.PayAllocDate.RemittanceDate.ToShortDateString
        EnergyAllocHeader(1, 10) = "CollectionPayment"
        EnergyAllocHeader(0, 11) = Me.PayAllocDate.RemittanceDate.ToShortDateString
        EnergyAllocHeader(1, 11) = "WithholdingTax"
        EnergyAllocHeader(0, 12) = Me.PayAllocDate.RemittanceDate.ToShortDateString
        EnergyAllocHeader(1, 12) = "DrawDown"
        EnergyAllocHeader(0, 13) = Me.PayAllocDate.RemittanceDate.ToShortDateString
        EnergyAllocHeader(1, 13) = "Offsetting"
        EnergyAllocHeader(0, 14) = Me.PayAllocDate.RemittanceDate.ToShortDateString
        EnergyAllocHeader(1, 14) = "DefaultInterest"
        EnergyAllocHeader(0, 15) = Me.PayAllocDate.RemittanceDate.ToShortDateString
        EnergyAllocHeader(1, 15) = "NewOutstandingBalance"

        'EnergyAllocation sheets
        xlWorkSheet1 = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(3, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(4, UBound(EnergyAllocHeader, 2) + 1), Excel.Range)
        xlEnergyAllocation = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlEnergyAllocation.Value = EnergyAllocHeader

        For Each BP In DistinctEBP
            Dim lrow1 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
            Dim GetEnergyColl = (From x In ForEnergyCollection
                                 Where x.WESMBillBatchNo = BP.WESMBillBatchNo And x.BillingPeriod = BP.BillingPeriod And x.DueDate = BP.DueDate
                                 Select x Order By x.OffsettingSequence).ToList()
            If GetEnergyColl.Count <> 0 Then
                Dim EnergyCollArr As Object(,) = Me.GenerateCollectionArr(GetEnergyColl, EnumChargeType.E, DistinctSequenceE)
                Dim rIndex As Integer = CInt(UBound(EnergyCollArr, 1))
                For r As Integer = 0 To UBound(EnergyCollArr, 1) - 1
                    For c As Integer = 0 To UBound(EnergyCollArr, 2)
                        Select Case c
                            Case 7, 8, 10, 11, 12, 13, 14, 15
                                EnergyCollArr(rIndex, c) = CDec(EnergyCollArr(rIndex, c)) + CDec(EnergyCollArr(r, c))
                        End Select
                    Next
                Next
                xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow1 + 2, 1), Excel.Range)
                xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow1 + 2 + UBound(EnergyCollArr, 1), UBound(EnergyAllocHeader, 2) + 1), Excel.Range)
                xlEnergyBalance = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
                xlEnergyBalance.Value = EnergyCollArr
                EnergyCollArr = Nothing
            End If

            Dim lrow2 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
            Dim GetEnergyAlloc = (From x In ForEnergyAllocation
                                  Where x.WESMBillBatchNo = BP.WESMBillBatchNo And x.BillingPeriod = BP.BillingPeriod And x.DueDate = BP.DueDate
                                  Select x Order By x.OffsettingSequence).ToList()

            If GetEnergyAlloc.Count <> 0 Then
                Dim EnergyAllocArr As Object(,) = Me.GenerateAllocationArr(GetEnergyAlloc, EnumChargeType.E, DistinctSequenceE)
                Dim rIndex As Integer = CInt(UBound(EnergyAllocArr, 1))
                For r As Integer = 0 To UBound(EnergyAllocArr, 1) - 1
                    For c As Integer = 0 To UBound(EnergyAllocArr, 2)
                        Select Case c
                            Case 7, 8, 10, 11, 12, 13, 14, 15
                                EnergyAllocArr(rIndex, c) = CDec(EnergyAllocArr(rIndex, c)) + CDec(EnergyAllocArr(r, c))
                        End Select
                    Next
                Next
                xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow2 + 2, 1), Excel.Range)
                xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow2 + 2 + UBound(EnergyAllocArr, 1), UBound(EnergyAllocHeader, 2) + 1), Excel.Range)
                xlEnergyBalance = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
                xlEnergyBalance.Value = EnergyAllocArr
                EnergyAllocArr = Nothing
            End If

            GetEnergyColl = Nothing
            GetEnergyAlloc = Nothing
        Next

        ForEnergyCollection = Nothing
        ForEnergyAllocation = Nothing
        DistinctEBP = Nothing
        DistinctSequenceE = Nothing
        'End   ENERGYAllocation ****************************************************************************************************************************************************

        'Start EnergyBalance *******************************************************************************************************************************************************
        Dim ForEnergyBalance As List(Of PaymentWBSHistoryBalance) = (From x In Me.PymntWBSHistoryBalance Where x.ChargeType = EnumChargeType.E Select x).ToList()

        Dim ForEnergyBalanceHeader = (From x In ForEnergyBalance
                                      Where x.BalanceType = EnumBalanceType.AP
                                      Select x.BillingPeriodRemarks, x.BillingPeriod, x.WESMBillBatchNo, x.OriginalDueDate, x.TotalBillAmount
                                      Order By WESMBillBatchNo, OriginalDueDate).Distinct().ToList()

        'Energy Balance Header Setup
        Dim i As Integer = 2
        Dim EnergyBalanceHeader As Object(,) = New Object(,) {}
        Dim EColumnIndexDic As New Dictionary(Of String, Integer)
        Dim GrandTotalEBillAmount As Decimal = 0
        ReDim EnergyBalanceHeader(5, 2)
        EnergyBalanceHeader(0, 0) = "Outstanding Energy Balance"
        EnergyBalanceHeader(0, 1) = Nothing
        EnergyBalanceHeader(0, 2) = Nothing
        EnergyBalanceHeader(1, 0) = "Total Energy Billing"
        EnergyBalanceHeader(2, 0) = "Percentage of Collection"
        EnergyBalanceHeader(3, 0) = "Total Receivables"
        EnergyBalanceHeader(4, 0) = "Total Payables"
        EnergyBalanceHeader(5, 0) = "Difference"

        For Each item In ForEnergyBalanceHeader
            i += 1
            ReDim Preserve EnergyBalanceHeader(5, i)
            EnergyBalanceHeader(0, i) = item.WESMBillBatchNo & " [" _
                                      & item.BillingPeriod & " " _
                                      & item.OriginalDueDate.ToShortDateString & "]"
            EnergyBalanceHeader(1, i) = item.TotalBillAmount

            If Not EColumnIndexDic.ContainsKey(CStr(EnergyBalanceHeader(0, i))) Then
                EColumnIndexDic.Add(CStr(EnergyBalanceHeader(0, i)), i)
            End If
            GrandTotalEBillAmount += item.TotalBillAmount
        Next

        i += 2
        ReDim Preserve EnergyBalanceHeader(5, i)
        EnergyBalanceHeader(0, i - 1) = Nothing
        EnergyBalanceHeader(0, i) = "Total"
        EnergyBalanceHeader(1, i) = GrandTotalEBillAmount

        'Add Header into worksheet
        xlWorkSheet1 = CType(xlWorkBook.Sheets(2), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(2, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(7, UBound(EnergyBalanceHeader, 2) + 1), Excel.Range)
        xlEnergyBalance = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlEnergyBalance.Value = EnergyBalanceHeader
        EnergyBalanceHeader = Nothing

        'Add AR Content
        Dim ForEnergyBalAR = (From x In ForEnergyBalance Where x.BalanceType = EnumBalanceType.AR Select x).ToList()
        Dim EnergyBalConAR As Object(,) = Me.GenerateBalanceContent(EColumnIndexDic, i, ForEnergyBalAR, "RECEIVABLES:")
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(9, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(UBound(EnergyBalConAR, 1) + 9, UBound(EnergyBalConAR, 2) + 1), Excel.Range)
        xlEnergyBalance = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlEnergyBalance.Value = EnergyBalConAR

        'Add AP Content
        Dim ForEnergyBalAP = (From x In ForEnergyBalance Where x.BalanceType = EnumBalanceType.AP Select x).ToList()
        Dim EnergyBalConAP As Object(,) = Me.GenerateBalanceContent(EColumnIndexDic, i, ForEnergyBalAP, "PAYABLES:")
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(UBound(EnergyBalConAR, 1) + 11, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(UBound(EnergyBalConAR, 1) + 11 + UBound(EnergyBalConAP, 1), UBound(EnergyBalConAP, 2) + 1), Excel.Range)
        xlEnergyBalance = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlEnergyBalance.Value = EnergyBalConAP
        EnergyBalConAR = Nothing
        EnergyBalConAP = Nothing

        ForEnergyBalance = Nothing
        ForEnergyBalanceHeader = Nothing
        'End EnergyBalance *********************************************************************************************************************************************************

        'Start VATAllocation *******************************************************************************************************************************************************
        Dim ForVATCollection = (From x In Me.VATNoTransactionARList Select x).Union _
                               (From x In Me.VATonEnergyCollectionList Select x).Union _
                               (From x In Me.OffsettingVATonEnergyCollectionList Select x).ToList()


        Dim ForVATAllocation = (From x In Me.VATNoTransactionAPList Select x).Union _
                               (From x In Me.VATonEnergyAllocationList Where x.EndingBalance > 0 Select x).Union _
                               (From x In Me.OffsettingVATonEnergyAllocationList Select x).ToList()


        Dim DistinctEVBP = (From x In ForVATCollection
                            Order By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate
                            Group x By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate
                            Into y = Group Let Grp = New With {.WESMBillBatchNo = WESMBillBatchNo, .BillingPeriod = BillingPeriod, .DueDate = DueDate}
                            Select Grp).ToList()

        Dim DistinctSequenceEV = (From x In ForVATCollection Select x.OffsettingSequence Order By OffsettingSequence).Distinct().ToList()

        'VAT Allocation Header Setup
        Dim VATAllocHeader As Object(,) = New Object(,) {}
        ReDim VATAllocHeader(1, 11)
        VATAllocHeader(1, 0) = "BillingParticulars"
        VATAllocHeader(1, 1) = "BillingBatchNo"
        VATAllocHeader(1, 2) = "IDNumber"
        VATAllocHeader(1, 3) = "ParticipantID"
        VATAllocHeader(1, 4) = "InvoiceNumber"
        VATAllocHeader(1, 5) = "OrigDueDate"
        VATAllocHeader(1, 6) = "NewDueDate"
        VATAllocHeader(1, 7) = "OutstandingBalance"
        VATAllocHeader(1, 8) = Nothing

        VATAllocHeader(0, 9) = Me.PayAllocDate.RemittanceDate.ToShortDateString()
        VATAllocHeader(1, 9) = "CollectionPayment"
        VATAllocHeader(0, 10) = Me.PayAllocDate.RemittanceDate.ToShortDateString()
        VATAllocHeader(1, 10) = "Offsetting"
        VATAllocHeader(0, 11) = Me.PayAllocDate.RemittanceDate.ToShortDateString()
        VATAllocHeader(1, 11) = "NewOutstandingBalance"

        'VATAllocation sheets
        xlWorkSheet1 = CType(xlWorkBook.Sheets(3), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(3, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(4, UBound(VATAllocHeader, 2) + 1), Excel.Range)
        xlVATAllocation = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlVATAllocation.Value = VATAllocHeader

        For Each BP In DistinctEVBP
            Dim lrow1 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
            Dim GetVATColl = (From x In ForVATCollection
                              Where x.WESMBillBatchNo = BP.WESMBillBatchNo And x.BillingPeriod = BP.BillingPeriod And x.DueDate = BP.DueDate
                              Select x Order By x.OffsettingSequence).ToList()

            If GetVATColl.Count <> 0 Then
                Dim VATCollArr As Object(,) = Me.GenerateCollectionArr(GetVATColl, EnumChargeType.EV, DistinctSequenceEV)
                Dim rIndex As Integer = CInt(UBound(VATCollArr, 1))
                For r As Integer = 0 To UBound(VATCollArr, 1) - 1
                    For c As Integer = 0 To UBound(VATCollArr, 2)
                        Select Case c
                            Case 7, 9 To 11
                                VATCollArr(rIndex, c) = CDec(VATCollArr(rIndex, c)) + CDec(VATCollArr(r, c))
                        End Select
                    Next
                Next
                xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow1 + 2, 1), Excel.Range)
                xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow1 + 2 + UBound(VATCollArr, 1), UBound(VATAllocHeader, 2) + 1), Excel.Range)
                xlVATAllocation = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
                xlVATAllocation.Value = VATCollArr
                VATCollArr = Nothing
            End If

            Dim lrow2 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
            Dim GetVATAlloc = (From x In ForVATAllocation
                               Where x.WESMBillBatchNo = BP.WESMBillBatchNo And x.BillingPeriod = BP.BillingPeriod And x.DueDate = BP.DueDate
                               Select x Order By x.OffsettingSequence).ToList()
            If GetVATAlloc.Count <> 0 Then
                Dim VATAllocArr As Object(,) = Me.GenerateAllocationArr(GetVATAlloc, EnumChargeType.EV, DistinctSequenceEV)
                Dim rIndex As Integer = CInt(UBound(VATAllocArr, 1))
                For r As Integer = 0 To UBound(VATAllocArr, 1) - 1
                    For c As Integer = 0 To UBound(VATAllocArr, 2)
                        Select Case c
                            Case 7, 9 To 11
                                VATAllocArr(rIndex, c) = CDec(VATAllocArr(rIndex, c)) + CDec(VATAllocArr(r, c))
                        End Select
                    Next
                Next
                xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow2 + 2, 1), Excel.Range)
                xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow2 + 2 + UBound(VATAllocArr, 1), UBound(VATAllocHeader, 2) + 1), Excel.Range)
                xlEnergyAllocation = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
                xlEnergyAllocation.Value = VATAllocArr
                VATAllocArr = Nothing
            End If
            GetVATColl = Nothing
            GetVATAlloc = Nothing
        Next

        ForVATCollection = Nothing
        ForVATAllocation = Nothing
        DistinctEVBP = Nothing
        DistinctSequenceEV = Nothing
        'End   VATAllocation *******************************************************************************************************************************************************

        'Start VATBalance *******************************************************************************************************************************************************
        Dim ForVATBalance As List(Of PaymentWBSHistoryBalance) = (From x In Me.PymntWBSHistoryBalance Where x.ChargeType = EnumChargeType.EV Select x).ToList()

        Dim ForVATBalanceHeader = (From x In ForVATBalance
                                   Where x.BalanceType = EnumBalanceType.AP
                                   Select x.BillingPeriodRemarks, x.BillingPeriod, x.WESMBillBatchNo, x.OriginalDueDate, x.TotalBillAmount
                                   Order By WESMBillBatchNo, OriginalDueDate).Distinct().ToList()

        'VAT Balance Header Setup
        Dim j As Integer = 2
        Dim VATBalanceHeader As Object(,) = New Object(,) {}
        Dim VColumnIndexDic As New Dictionary(Of String, Integer)
        Dim GrandTotalVBillAmount As Decimal = 0
        ReDim VATBalanceHeader(5, 2)
        VATBalanceHeader(0, 0) = "Outstanding VAT Balance"
        VATBalanceHeader(0, 1) = Nothing
        VATBalanceHeader(0, 2) = Nothing
        VATBalanceHeader(1, 0) = "Total VAT Billing"
        VATBalanceHeader(2, 0) = "Percentage of Collection"
        VATBalanceHeader(3, 0) = "Total Receivables"
        VATBalanceHeader(4, 0) = "Total Payables"
        VATBalanceHeader(5, 0) = "Difference"

        For Each item In ForVATBalanceHeader
            j += 1
            ReDim Preserve VATBalanceHeader(5, j)
            VATBalanceHeader(0, j) = item.WESMBillBatchNo & " [" _
                                      & item.BillingPeriod & " " _
                                      & item.OriginalDueDate.ToShortDateString & "]"

            VATBalanceHeader(1, j) = item.TotalBillAmount
            If Not VColumnIndexDic.ContainsKey(CStr(VATBalanceHeader(0, j))) Then
                VColumnIndexDic.Add(CStr(VATBalanceHeader(0, j)), j)
            End If
            GrandTotalVBillAmount += item.TotalBillAmount
        Next

        j += 2
        ReDim Preserve VATBalanceHeader(5, j)
        VATBalanceHeader(0, j - 1) = Nothing
        VATBalanceHeader(0, j) = "Total"
        VATBalanceHeader(1, j) = GrandTotalVBillAmount

        'Add Header into worksheet
        xlWorkSheet1 = CType(xlWorkBook.Sheets(4), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(2, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(7, UBound(VATBalanceHeader, 2) + 1), Excel.Range)
        xlVATBalance = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlVATBalance.Value = VATBalanceHeader
        VATBalanceHeader = Nothing
        'Add AR Content
        Dim ForVATBalAR = (From x In ForVATBalance Where x.BalanceType = EnumBalanceType.AR Select x).ToList()
        Dim VATBalConAR As Object(,) = Me.GenerateBalanceContent(VColumnIndexDic, j, ForVATBalAR, "RECEIVABLES:")
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(9, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(UBound(VATBalConAR, 1) + 9, UBound(VATBalConAR, 2) + 1), Excel.Range)
        xlVATBalance = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlVATBalance.Value = VATBalConAR

        'Add AP Content
        Dim ForVATBalAP = (From x In ForVATBalance Where x.BalanceType = EnumBalanceType.AP Select x).ToList()
        Dim VATBalConAP As Object(,) = Me.GenerateBalanceContent(VColumnIndexDic, j, ForVATBalAP, "PAYABLES:")
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(UBound(VATBalConAR, 1) + 11, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(UBound(VATBalConAR, 1) + 11 + UBound(VATBalConAP, 1), UBound(VATBalConAP, 2) + 1), Excel.Range)
        xlVATBalance = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlVATBalance.Value = VATBalConAP
        VATBalConAR = Nothing
        VATBalConAP = Nothing

        ForVATBalance = Nothing
        ForVATBalanceHeader = Nothing
        VColumnIndexDic = Nothing
        'End VATBalance *********************************************************************************************************************************************************

        'Start MarketFeesAllocation ************************************************************************************************************************************************
        Dim ForMFMFVCollection = (From x In Me.MFwithVATNoTransARList Select x).Union _
                                 (From x In Me.MFwithVATCollectionList Select x).Union _
                                 (From x In Me.OffsettingMFwithVATCollectionList Select x).ToList()

        Dim ForMFMFVAlloc = (From x In Me.MFwithVATNoTransAPList Select x).Union _
                            (From x In Me.MFwithVATAllocationList Select x).Union _
                            (From x In Me.OffsettingMFwithVATAllocationList Select x).ToList()

        Dim DistinctMFMFVBPColl = (From x In ForMFMFVCollection
                                   Order By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate
                                   Group x By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate
                                    Into y = Group Let Grp = New With {.WESMBillBatchNo = WESMBillBatchNo, .BillingPeriod = BillingPeriod, .DueDate = DueDate}
                                   Select Grp).ToList()

        Dim DistinctMFMFVBPAlloc = (From x In ForMFMFVAlloc
                                    Order By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate
                                    Group x By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate
                                    Into y = Group Let Grp = New With {.WESMBillBatchNo = WESMBillBatchNo, .BillingPeriod = BillingPeriod, .DueDate = DueDate}
                                    Select Grp).ToList()

        Dim DistinctMFMFVBP = (From x In DistinctMFMFVBPColl Select x).Union _
                            (From x In DistinctMFMFVBPAlloc Select x).Distinct.ToList()

        Dim DistinctSequenceMFMFV = (From x In ForMFMFVCollection Select x.OffsettingSequence Order By OffsettingSequence).Distinct().ToList()

        'Energy Allocation Header Setup
        Dim MFAllocHeader As Object(,) = New Object(,) {}
        ReDim MFAllocHeader(1, 18)
        MFAllocHeader(1, 0) = "BillingParticulars"
        MFAllocHeader(1, 1) = "BillingBatchNo"
        MFAllocHeader(1, 2) = "IDNumber"
        MFAllocHeader(1, 3) = "ParticipantID"
        MFAllocHeader(1, 4) = "InvoiceNumber"
        MFAllocHeader(1, 5) = "OrigDueDate"
        MFAllocHeader(1, 6) = "NewDueDate"
        MFAllocHeader(1, 7) = "MFBase"
        MFAllocHeader(1, 8) = "MFVAT"
        MFAllocHeader(1, 9) = "OutstandingBalance"
        MFAllocHeader(1, 10) = Nothing
        MFAllocHeader(0, 11) = Me.PayAllocDate.RemittanceDate.ToShortDateString()
        MFAllocHeader(1, 11) = "Collection"
        MFAllocHeader(0, 12) = Me.PayAllocDate.RemittanceDate.ToShortDateString()
        MFAllocHeader(1, 12) = "Offsetting"
        MFAllocHeader(0, 13) = Me.PayAllocDate.RemittanceDate.ToShortDateString()
        MFAllocHeader(1, 13) = "DefaultInterest"
        MFAllocHeader(0, 14) = Me.PayAllocDate.RemittanceDate.ToShortDateString()
        MFAllocHeader(1, 14) = "WithholdingTAX"
        MFAllocHeader(0, 15) = Me.PayAllocDate.RemittanceDate.ToShortDateString()
        MFAllocHeader(1, 15) = "DefaultInterestonWHTAX"
        MFAllocHeader(0, 16) = Me.PayAllocDate.RemittanceDate.ToShortDateString()
        MFAllocHeader(1, 16) = "WithholdingVAT"
        MFAllocHeader(0, 17) = Me.PayAllocDate.RemittanceDate.ToShortDateString()
        MFAllocHeader(1, 17) = "DefaultInterstWHVAT"
        MFAllocHeader(0, 18) = Me.PayAllocDate.RemittanceDate.ToShortDateString()
        MFAllocHeader(1, 18) = "NewOutstandingBalance"

        'MFMFVAllocation sheets
        xlWorkSheet1 = CType(xlWorkBook.Sheets(5), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(3, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(4, UBound(MFAllocHeader, 2) + 1), Excel.Range)
        xlMFAllocation = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlMFAllocation.Value = MFAllocHeader

        For Each BP In DistinctMFMFVBP
            Dim lrow1 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
            Dim GetMFMFVColl = (From x In ForMFMFVCollection
                                Where x.WESMBillBatchNo = BP.WESMBillBatchNo And x.BillingPeriod = BP.BillingPeriod And x.DueDate = BP.DueDate
                                Select x Order By x.OffsettingSequence).ToList()
            If GetMFMFVColl.Count <> 0 Then
                Dim MFMFVCollArr As Object(,) = Me.GenerateCollectionArr(GetMFMFVColl, EnumChargeType.MF, DistinctSequenceMFMFV)
                Dim rIndex As Integer = CInt(UBound(MFMFVCollArr, 1))
                For r As Integer = 0 To UBound(MFMFVCollArr, 1) - 1
                    For c As Integer = 0 To UBound(MFMFVCollArr, 2)
                        Select Case c
                            Case 9, 11, 12 To 18
                                MFMFVCollArr(rIndex, c) = CDec(MFMFVCollArr(rIndex, c)) + CDec(MFMFVCollArr(r, c))
                        End Select
                    Next
                Next
                xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow1 + 2, 1), Excel.Range)
                xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow1 + 2 + UBound(MFMFVCollArr, 1), UBound(MFAllocHeader, 2) + 1), Excel.Range)
                xlMFAllocation = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
                xlMFAllocation.Value = MFMFVCollArr

            End If

            Dim lrow2 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
            Dim GetMFMFVAlloc = (From x In ForMFMFVAlloc
                                 Where x.WESMBillBatchNo = BP.WESMBillBatchNo And x.BillingPeriod = BP.BillingPeriod And x.DueDate = BP.DueDate
                                 Select x Order By x.OffsettingSequence).ToList()
            If GetMFMFVAlloc.Count <> 0 Then
                Dim MFMFVAllocArr As Object(,) = Me.GenerateAllocationArr(GetMFMFVAlloc, EnumChargeType.MF, DistinctSequenceMFMFV)
                Dim rIndex As Integer = CInt(UBound(MFMFVAllocArr, 1))
                For r As Integer = 0 To UBound(MFMFVAllocArr, 1) - 1
                    For c As Integer = 0 To UBound(MFMFVAllocArr, 2)
                        Select Case c
                            Case 9, 11, 12 To 18
                                MFMFVAllocArr(rIndex, c) = CDec(MFMFVAllocArr(rIndex, c)) + CDec(MFMFVAllocArr(r, c))
                        End Select
                    Next
                Next
                xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow2 + 2, 1), Excel.Range)
                xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow2 + 2 + UBound(MFMFVAllocArr, 1), UBound(MFAllocHeader, 2) + 1), Excel.Range)
                xlMFAllocation = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
                xlMFAllocation.Value = MFMFVAllocArr

            End If
            GetMFMFVColl = Nothing
            GetMFMFVAlloc = Nothing
        Next

        ForMFMFVCollection = Nothing
        ForMFMFVAlloc = Nothing
        DistinctMFMFVBP = Nothing
        DistinctSequenceMFMFV = Nothing
        'End   MarketFeesAllocation ************************************************************************************************************************************************

        'Start Collection and Payment Report ***************************************************************************************************************************************
        Dim CAPDicCol As New Dictionary(Of String, Integer)
        Dim CAPDicRow As New Dictionary(Of String, Integer)
        Dim CurrentRow As Integer = 0
        Dim CurrentCol As Integer = 0
        Dim CapHeader = Me.GenerateCollPymtHeader(CAPDicCol)

        'Coll-PymntReportHeader Header with Surplus and NSS Interest
        CurrentRow += UBound(CapHeader, 1) + 1
        CurrentCol += UBound(CapHeader, 2) + 1
        xlWorkSheet1 = CType(xlWorkBook.Sheets(6), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(CurrentRow, CurrentCol), Excel.Range)
        xlCollAndPayment = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlCollAndPayment.Value = CapHeader

        Dim CAPCollCount As Integer = UBound(CapHeader, 2)

        'Collection DrawDown        
        Dim CAPContentDrawDown = Me.GenerateCollDrawDown(CAPDicCol, CAPCollCount)
        CurrentRow += 1
        xlWorkSheet1 = CType(xlWorkBook.Sheets(6), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(CurrentRow, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(UBound(CAPContentDrawDown, 1) + CurrentRow, CurrentCol), Excel.Range)
        xlCollAndPayment = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlCollAndPayment.Value = CAPContentDrawDown

        'Collection Cash        
        Dim CAPContentCash = Me.GenerateCollCash(CAPDicCol, CAPCollCount)
        CurrentRow += UBound(CAPContentDrawDown, 1) + 2
        xlWorkSheet1 = CType(xlWorkBook.Sheets(6), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(CurrentRow, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(UBound(CAPContentCash, 1) + CurrentRow, CurrentCol), Excel.Range)
        xlCollAndPayment = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlCollAndPayment.Value = CAPContentCash


        'Pymt and Offsetting
        Dim CAPContentPymtWithOffset = Me.GeneratePymtIncludingOffsetting(CAPDicCol, CAPCollCount)
        CurrentRow += UBound(CAPContentCash, 1) + 2
        xlWorkSheet1 = CType(xlWorkBook.Sheets(6), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(CurrentRow, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(UBound(CAPContentPymtWithOffset, 1) + CurrentRow, CurrentCol), Excel.Range)
        xlCollAndPayment = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlCollAndPayment.Value = CAPContentPymtWithOffset

        'Footer 
        Dim CAPContentFooter = Me.GenerateFooter(CapHeader, CAPContentDrawDown, CAPContentCash, CAPContentPymtWithOffset, CAPCollCount)
        CurrentRow += UBound(CAPContentPymtWithOffset, 1) + 2
        xlWorkSheet1 = CType(xlWorkBook.Sheets(6), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(CurrentRow, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(UBound(CAPContentFooter, 1) + CurrentRow, CurrentCol), Excel.Range)
        xlCollAndPayment = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlCollAndPayment.Value = CAPContentFooter

        CapHeader = Nothing
        CAPContentDrawDown = Nothing
        CAPContentCash = Nothing
        CAPContentPymtWithOffset = Nothing
        CAPContentFooter = Nothing
        'End  Collection and Payment Report ****************************************************************************************************************************************

        'Start Front Page ***************************************************************************************************************************************
        Dim CAPFrontPage = Me.GenerateFrontPage()
        'CY Year
        xlWorkSheet1 = CType(xlWorkBook.Sheets(7), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(3, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(3, 1), Excel.Range)
        xlFrontPage = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlFrontPage.Value = "CY " & Me.PayAllocDate.RemittanceDate.Year

        'Date
        xlWorkSheet1 = CType(xlWorkBook.Sheets(7), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(4, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(4, 1), Excel.Range)
        xlFrontPage = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlFrontPage.Value = Me.PayAllocDate.RemittanceDate.ToShortDateString()

        'Collection
        xlWorkSheet1 = CType(xlWorkBook.Sheets(7), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(11, 5), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(UBound(CAPFrontPage(0)) + 11, 5), Excel.Range)
        xlFrontPage = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlFrontPage.Value = CAPFrontPage(0)

        'Payment
        xlWorkSheet1 = CType(xlWorkBook.Sheets(7), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(32, 5), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(UBound(CAPFrontPage(1)) + 32, 5), Excel.Range)
        xlFrontPage = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlFrontPage.Value = CAPFrontPage(1)

        CAPFrontPage = Nothing
        'End  Front Page ****************************************************************************************************************************************

        'Start EFT Format ***************************************************************************************************************************************
        Dim CAPEFTFormat = Me.GenerateEFTFormat()
        xlWorkSheet1 = CType(xlWorkBook.Sheets(8), Excel.Worksheet)
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(2, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(UBound(CAPEFTFormat, 1) + 1, 14), Excel.Range)
        xlEFTFormat = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlEFTFormat.Value = CAPEFTFormat
        CAPEFTFormat = Nothing
        'End  EFTFormat  ****************************************************************************************************************************************

        Dim FileName As String = "CAPSummary " & Me._PayAllocDate.CollAllocationDate.ToString("dd-MMM-yyyy")

        xlApp.DisplayAlerts = False
        xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbookMacroEnabled, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        xlWorkBook.Close(False)
        xlApp.Quit()

        releaseObject(xlEFTFormat)
        releaseObject(xlFrontPage)
        releaseObject(xlCollAndPayment)
        releaseObject(xlMFAllocation)
        releaseObject(xlVATBalance)
        releaseObject(xlVATAllocation)
        releaseObject(xlEnergyBalance)
        releaseObject(xlEnergyAllocation)
        releaseObject(xlRowRange2)
        releaseObject(xlRowRange1)
        releaseObject(xlWorkSheet1)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)
    End Sub
    Private Function GenerateBalanceContent(ByVal ColumnIndexDic As Dictionary(Of String, Integer),
                                            ByVal ColumnCount As Integer,
                                            ByVal BalanceContent As List(Of PaymentWBSHistoryBalance),
                                            ByVal ContentBalanceType As String) As Object(,)

        Dim BalContent As Object(,) = New Object(,) {}
        Dim ForDistinctParticipant = (From x In BalanceContent Select x.IDNumber Order By IDNumber.ParticipantID).ToList()
        Dim DistinctParticipant = (From x In ForDistinctParticipant Select x.IDNumber).Distinct.ToList()
        Dim rIndex As Integer = 0
        ReDim BalContent(DistinctParticipant.Count + 1, ColumnCount)
        BalContent(0, 0) = ContentBalanceType
        For Each item In DistinctParticipant
            rIndex += 1
            BalContent(rIndex, 0) = item.ToString
            BalContent(rIndex, 1) = (From x In Me.AMParticipants Where x.IDNumber = item Select x.ParticipantID).FirstOrDefault
            Dim GetBalancePerBP = (From x In BalanceContent Where x.IDNumber.IDNumber = item Select x).ToList()
            Dim TotalBalance As Decimal = 0
            For Each itemx In GetBalancePerBP
                Dim DicKey As String = itemx.WESMBillBatchNo.ToString & " [" _
                                     & itemx.BillingPeriod.ToString & " " _
                                     & itemx.OriginalDueDate.ToShortDateString() & "]"
                Dim ColumnIndexNo As Integer = ColumnIndexDic(DicKey.Trim.ToString)
                BalContent(rIndex, ColumnIndexNo) = itemx.AmountBalance
                BalContent(rIndex, UBound(BalContent, 2)) = CDec(BalContent(rIndex, UBound(BalContent, 2))) + itemx.AmountBalance
                TotalBalance += itemx.AmountBalance
            Next
            BalContent(rIndex, UBound(BalContent, 2)) = TotalBalance.ToString
        Next
        Return BalContent
    End Function

    Private Function GenerateEFTFormat() As Object(,)
        Dim EFTSummaryReportArr As Object(,) = New Object(,) {}
        ReDim Preserve EFTSummaryReportArr(Me.EFTSummaryReportList.Count, 13)

        Dim Row As Integer = 0
        For Each Item In Me.EFTSummaryReportList
            If Item.Participant.Bank.ToUpper.Equals("SCB") Or Item.Participant.Bank.ToUpper.Equals("STANDARD CHARTERED BANK") Then
                EFTSummaryReportArr(Row, 0) = "BT"
            Else
                EFTSummaryReportArr(Row, 0) = "RTGS"
            End If
            Dim limitCheckPay As String() = getChecPay(Item.Participant.CheckPay)
            EFTSummaryReportArr(Row, 1) = "'" & Item.Participant.IDNumber & Me.PayAllocDate.RemittanceDate.ToString("MMddyyyy")
            EFTSummaryReportArr(Row, 2) = "'" & AMModule.t13DigitDebitACNo.ToString()
            EFTSummaryReportArr(Row, 3) = limitCheckPay(0) 'Left(Item.Participant.CheckPay, 35)
            EFTSummaryReportArr(Row, 4) = Item.Participant.BankTransactionCode.ToString
            EFTSummaryReportArr(Row, 5) = "'" & Item.Participant.BankAccountNo.ToString
            EFTSummaryReportArr(Row, 6) = "Payment of WESM Transactions"
            EFTSummaryReportArr(Row, 7) = If(InStr(1, Item.Participant.BankTransactionCode.ToString, "PNB") > 0, "WESM Participant", "")
            EFTSummaryReportArr(Row, 8) = "PHP"
            EFTSummaryReportArr(Row, 9) = Item.TotalPayment
            EFTSummaryReportArr(Row, 10) = Item.Participant.Representative.EmailAddress
            EFTSummaryReportArr(Row, 11) = limitCheckPay(1)  'If(Len(Item.Participant.CheckPay) <= 35, "", Microsoft.VisualBasic.Mid(Trim(Item.Participant.CheckPay), 35 + 1, 70)) 'If(Len(Item.Participant.CheckPay) <= 35, "", Right(Item.Participant.CheckPay, Len(Item.Participant.CheckPay) - 35))
            EFTSummaryReportArr(Row, 12) = If(Len(Item.Participant.CheckPay) <= 70, "", Microsoft.VisualBasic.Mid(Trim(Item.Participant.CheckPay), 71))
            EFTSummaryReportArr(Row, 13) = "'" & Me.PayAllocDate.RemittanceDate.ToString("MM/dd/yyyy")
            Row += 1
        Next
        Return EFTSummaryReportArr
    End Function

    Private Function getChecPay(ByVal checkPay As String) As String()
        Dim arrCheckPay As String() = checkPay.Split(New Char() {" "c})
        Dim returnValue(1) As String
        Dim retVal As String = ""

        For Each word In arrCheckPay
            retVal &= " " & word
            If retVal.Length <= 35 Then
                returnValue(0) &= " " & word
            Else
                returnValue(1) &= " " & word
            End If
        Next
        Return returnValue
    End Function

    Private Function GenerateFrontPage() As List(Of Object(,))
        Dim ret As List(Of Object(,)) = New List(Of Object(,))

        Dim TotalDrawDown As Decimal = (From x In Me.EnergyListCollection Where x.CollectionCategory = EnumCollectionCategory.Drawdown Select x.AllocationAmount).Sum()
        Dim TotalDeferred As Decimal = (From x In Me.PrevDeferredPaymentList Select x.OutstandingBalanceDeferredPayment).Sum()

        Dim TotalCashApplied As Decimal = (From x In Me.EnergyListCollection Where x.CollectionCategory = EnumCollectionCategory.Cash Select x.AllocationAmount).Sum _
                                         + (From x In Me.VATonEnergyCollectionList Where x.CollectionCategory = EnumCollectionCategory.Cash Select x.AllocationAmount).Sum _
                                         + (From x In Me.MFwithVATCollectionList Where x.CollectionCategory = EnumCollectionCategory.Cash Select x.AllocationAmount).Sum



        Dim TotalCashUnApplied As Decimal = (From x In Me.CollectionMonitoring
                                             Where Not x.TransType = EnumCollectionMonitoringType.TransferToPRDrawdown
                                             Select x.Amount).Sum()


        Dim Collection As Object(,) = New Object(,) {}
        Dim Payment As Object(,) = New Object(,) {}
        ReDim Collection(7, 0)

        For i As Integer = 0 To 7
            Select Case i
                Case 0
                    Collection(i, 0) = 0 'NSS
                Case 3
                    Collection(i, 0) = Math.Round(Math.Abs(TotalDrawDown), 2)
                Case 5
                    Collection(i, 0) = Math.Round(TotalDeferred, 2)
                Case 7
                    Collection(i, 0) = Math.Round(Math.Abs(TotalCashApplied) + TotalCashUnApplied, 2)
            End Select
        Next
        ret.Add(Collection)

        Dim TotalLBC As Decimal = (From x In Me.RequestForPayment.RFPDetails
                                   Where x.RFPDetailsType = EnumRFPDetailsType.Payment _
                                   And x.PaymentType = EnumParticipantPaymentType.Check _
                                   And x.Amount >= 1000
                                   Select x.Amount).Sum

        Dim TotalEFT As Decimal = (From x In Me.RequestForPayment.RFPDetails
                                   Where x.RFPDetailsType = EnumRFPDetailsType.Payment _
                                   And x.PaymentType = EnumParticipantPaymentType.EFT _
                                   And x.Amount >= 1000
                                   Select x.Amount).Sum()

        Dim TotalDeferredNew As Decimal = (From x In Me.RequestForPayment.RFPDetails
                                           Where x.RFPDetailsType = EnumRFPDetailsType.Payment _
                                           And x.Amount < 1000
                                           Select x.Amount).Sum()

        Dim TotalMarketFees As Decimal = Me.RequestForPayment.MarketFees
        Dim TotalReplenishment As Decimal = Me.RequestForPayment.PRReplenishment
        Dim TotalWithholdingTax As Decimal = Math.Abs((From x In getWESMBillSalesAndPurchases Select x.WithholdingTAX).Sum)
        Dim TotalFinPen As Decimal = Math.Abs((From x In Me.PaymentTransferToPRList Where x.TransferToFinPen <> 0 Select x.TransferToFinPen).Sum)

        ReDim Payment(21, 0)
        For i As Integer = 0 To 21
            Select Case i
                Case 0
                    Payment(i, 0) = Math.Round(TotalEFT, 2)
                Case 2
                    Payment(i, 0) = Math.Round(TotalLBC, 2)
                Case 4
                    Payment(i, 0) = Math.Round(TotalDeferredNew, 2)
                Case 6
                    Payment(i, 0) = Math.Round(TotalMarketFees, 2)
                Case 11
                    Payment(i, 0) = Math.Round(TotalReplenishment, 2)
                Case 19
                    Payment(i, 0) = Math.Round(TotalWithholdingTax, 2)
                Case 21
                    Payment(i, 0) = Math.Round(TotalFinPen, 2)
            End Select
        Next
        ret.Add(Payment)
        Return ret
    End Function

    Private Function GenerateFooter(ByVal CapHeader As Object(,),
                                    ByVal CAPContentDrawDown As Object(,),
                                    ByVal CAPContentCash As Object(,),
                                    ByVal CAPContentPymtWithOffset As Object(,),
                                    ByVal CAPCollCount As Integer) As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim CAPContentFooter As Object(,) = New Object(,) {}
        ReDim CAPContentFooter(5, CAPCollCount)
        CAPContentFooter(0, 0) = "Market Fees with VAT"
        CAPContentFooter(1, 0) = "Settlement Account"
        CAPContentFooter(2, 0) = "Surplus"
        CAPContentFooter(3, 0) = "Prudential Requirement"
        CAPContentFooter(4, 0) = "Financial Penalty"
        CAPContentFooter(5, 0) = "Total"

        For c As Integer = 2 To UBound(CapHeader, 2) - 11
            Dim SplitHeader As String() = Split(CapHeader(1, c).ToString(), "-")
            Select Case DirectCast([Enum].Parse(GetType(EnumCollectionType), SplitHeader(0)), EnumCollectionType)
                Case EnumCollectionType.MarketFees, EnumCollectionType.VatOnMarketFees,
                    EnumCollectionType.DefaultInterestOnMF, EnumCollectionType.DefaultInterestOnVatOnMF,
                    EnumCollectionType.WithholdingTaxOnMF, EnumCollectionType.WithholdingVatOnMF,
                    EnumCollectionType.WithholdingTaxOnDefaultInterest, EnumCollectionType.WithholdingVatOnDefaultInterest
                    Dim ARTotal As Decimal = (CDec(CAPContentDrawDown(UBound(CAPContentDrawDown, 1), c)) + CDec(CAPContentCash(UBound(CAPContentCash, 1), c)))

                    If ARTotal = 0 Then
                        CAPContentFooter(0, c) = CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), c))
                    Else
                        CAPContentFooter(0, c) = ARTotal - CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), c))
                    End If

                Case EnumCollectionType.Energy, EnumCollectionType.DefaultInterestOnEnergy
                    Dim ARTotal As Decimal = (CDec(CAPContentDrawDown(UBound(CAPContentDrawDown, 1), c)) + CDec(CAPContentCash(UBound(CAPContentCash, 1), c)))
                    If ARTotal = 0 Then
                        CAPContentFooter(1, c) = CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), c))
                    Else
                        CAPContentFooter(1, c) = ARTotal - CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), c))
                    End If
                    CAPContentFooter(0, UBound(CAPContentFooter, 2) - 9) = CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 9)) + CDec(CAPContentFooter(1, c))
                Case EnumCollectionType.VatOnEnergy
                    Dim ARTotal As Decimal = (CDec(CAPContentDrawDown(UBound(CAPContentDrawDown, 1), c)) + CDec(CAPContentCash(UBound(CAPContentCash, 1), c)))
                    If ARTotal = 0 Then
                        CAPContentFooter(1, c) = CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), c))
                    Else
                        CAPContentFooter(1, c) = ARTotal - CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), c))
                    End If

                    CAPContentFooter(0, UBound(CAPContentFooter, 2) - 8) = CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 8)) + CDec(CAPContentFooter(1, c))
            End Select
            CAPContentFooter(5, c) = CDec(CAPContentFooter(0, c)) +
                                    CDec(CAPContentFooter(1, c)) +
                                    CDec(CAPContentFooter(2, c)) +
                                    CDec(CAPContentFooter(3, c)) +
                                    CDec(CAPContentFooter(4, c))

        Next

        Dim MFTotal As Decimal = CDec(CAPContentDrawDown(UBound(CAPContentDrawDown, 1), UBound(CAPContentDrawDown, 2) - 9)) + CDec(CAPContentCash(UBound(CAPContentCash, 1), UBound(CAPContentCash, 2) - 9))

        If MFTotal = 0 Then
            CAPContentFooter(0, UBound(CAPContentFooter, 2) - 9) = CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), UBound(CAPContentPymtWithOffset, 2) - 9))
        Else
            CAPContentFooter(0, UBound(CAPContentFooter, 2) - 9) = MFTotal - CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), UBound(CAPContentPymtWithOffset, 2) - 9))
        End If

        CAPContentFooter(0, UBound(CAPContentFooter, 2) - 7) = CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 9))
        CAPContentFooter(0, UBound(CAPContentFooter, 2)) = CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 7)) +
                                                           CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 6)) +
                                                           CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 5)) +
                                                           CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 4)) +
                                                           CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 3)) +
                                                           CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 2)) +
                                                           CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 1))

        CAPContentFooter(1, UBound(CAPContentFooter, 2) - 7) = CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 10)) +
                                                               CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 8))

        CAPContentFooter(1, UBound(CAPContentFooter, 2) - 4) = CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), UBound(CAPContentPymtWithOffset, 2) - 4))
        CAPContentFooter(1, UBound(CAPContentFooter, 2) - 3) = CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), UBound(CAPContentPymtWithOffset, 2) - 3))

        CAPContentFooter(1, UBound(CAPContentFooter, 2)) = CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 7)) +
                                                           CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 6)) +
                                                           CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 5)) +
                                                           CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 4)) +
                                                           CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 3)) +
                                                           CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 2)) +
                                                           CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 1))

        CAPContentFooter(3, UBound(CAPContentFooter, 2) - 6) = CDec(CAPContentCash(UBound(CAPContentCash, 1), UBound(CAPContentCash, 2) - 6)) +
                                                               Math.Abs(CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), UBound(CAPContentPymtWithOffset, 2) - 6)))

        CAPContentFooter(3, UBound(CAPContentFooter, 2)) = CDec(CAPContentCash(UBound(CAPContentCash, 1), UBound(CAPContentCash, 2) - 6)) +
                                                        Math.Abs(CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), UBound(CAPContentPymtWithOffset, 2) - 6)))

        CAPContentFooter(4, UBound(CAPContentFooter, 2) - 1) = CDec(CAPContentCash(UBound(CAPContentCash, 1), UBound(CAPContentCash, 2) - 1)) +
                                                               Math.Abs(CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), UBound(CAPContentPymtWithOffset, 2) - 1)))

        CAPContentFooter(4, UBound(CAPContentFooter, 2)) = CDec(CAPContentCash(UBound(CAPContentCash, 1), UBound(CAPContentCash, 2) - 1)) +
                                                        Math.Abs(CDec(CAPContentPymtWithOffset(UBound(CAPContentPymtWithOffset, 1), UBound(CAPContentPymtWithOffset, 2) - 1)))


        CAPContentFooter(5, UBound(CAPContentFooter, 2) - 7) = CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 7)) +
                                                                CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 7)) +
                                                                CDec(CAPContentFooter(2, UBound(CAPContentFooter, 2) - 7)) +
                                                                CDec(CAPContentFooter(3, UBound(CAPContentFooter, 2) - 7)) +
                                                                CDec(CAPContentFooter(4, UBound(CAPContentFooter, 2) - 7))

        CAPContentFooter(5, UBound(CAPContentFooter, 2) - 6) = CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 6)) +
                                                                CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 6)) +
                                                                CDec(CAPContentFooter(2, UBound(CAPContentFooter, 2) - 6)) +
                                                                CDec(CAPContentFooter(3, UBound(CAPContentFooter, 2) - 6)) +
                                                                CDec(CAPContentFooter(4, UBound(CAPContentFooter, 2) - 6))

        CAPContentFooter(5, UBound(CAPContentFooter, 2) - 5) = CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 5)) +
                                                                CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 5)) +
                                                                CDec(CAPContentFooter(2, UBound(CAPContentFooter, 2) - 5)) +
                                                                CDec(CAPContentFooter(3, UBound(CAPContentFooter, 2) - 5)) +
                                                                CDec(CAPContentFooter(4, UBound(CAPContentFooter, 2) - 5))

        CAPContentFooter(5, UBound(CAPContentFooter, 2) - 4) = CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 4)) +
                                                                CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 4)) +
                                                                CDec(CAPContentFooter(2, UBound(CAPContentFooter, 2) - 4)) +
                                                                CDec(CAPContentFooter(3, UBound(CAPContentFooter, 2) - 4)) +
                                                                CDec(CAPContentFooter(4, UBound(CAPContentFooter, 2) - 4))

        CAPContentFooter(5, UBound(CAPContentFooter, 2) - 3) = CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 3)) +
                                                                CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 3)) +
                                                                CDec(CAPContentFooter(2, UBound(CAPContentFooter, 2) - 3)) +
                                                                CDec(CAPContentFooter(3, UBound(CAPContentFooter, 2) - 3)) +
                                                                CDec(CAPContentFooter(4, UBound(CAPContentFooter, 2) - 3))

        CAPContentFooter(5, UBound(CAPContentFooter, 2) - 2) = CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 2)) +
                                                               CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 2)) +
                                                               CDec(CAPContentFooter(2, UBound(CAPContentFooter, 2) - 2)) +
                                                               CDec(CAPContentFooter(3, UBound(CAPContentFooter, 2) - 2)) +
                                                               CDec(CAPContentFooter(4, UBound(CAPContentFooter, 2) - 2))

        CAPContentFooter(5, UBound(CAPContentFooter, 2) - 1) = CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2) - 1)) +
                                                                CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2) - 1)) +
                                                                CDec(CAPContentFooter(2, UBound(CAPContentFooter, 2) - 1)) +
                                                                CDec(CAPContentFooter(3, UBound(CAPContentFooter, 2) - 1)) +
                                                                CDec(CAPContentFooter(4, UBound(CAPContentFooter, 2) - 1))

        CAPContentFooter(5, UBound(CAPContentFooter, 2)) = CDec(CAPContentFooter(0, UBound(CAPContentFooter, 2))) +
                                                           CDec(CAPContentFooter(1, UBound(CAPContentFooter, 2))) +
                                                           CDec(CAPContentFooter(2, UBound(CAPContentFooter, 2))) +
                                                           CDec(CAPContentFooter(3, UBound(CAPContentFooter, 2))) +
                                                           CDec(CAPContentFooter(4, UBound(CAPContentFooter, 2)))



        ret = CAPContentFooter
        Return ret
    End Function

    Private Sub GetFITOffsetting()
        Me._DicFIT = New Dictionary(Of String, Decimal)
        If Me._DicFIT.Count > 0 Then
            Exit Sub
        End If

        Dim ForCAPOffsettingFIt = (From x In Me.EnergyShare Where x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) And x.AmountOffset.Count > 0 Select x).Union _
                                  (From x In Me.VATonEnergyShare Where x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) And x.AmountOffset.Count > 0 Select x).Union _
                                  (From x In Me.MFwithVATShare Where x.IDNumber.EndsWith(AMModule.FITParticipantCode.ToString) And x.AmountOffset.Count > 0 Select x).ToList


        Dim SortForCapOffsettingFit = ForCAPOffsettingFIt.OrderBy(Function(x) x.IDNumber).ToList()

        '_DicFIT        
        For Each item In SortForCapOffsettingFit

            For Each oitem In item.AmountOffset
                If item.IDNumber <> oitem.IDNumber Then
                    If Not DicFIT.ContainsKey(item.IDNumber) Then
                        DicFIT.Add(item.IDNumber, (oitem.OffsetAmount * -1))
                    Else
                        DicFIT.Item(item.IDNumber) += (oitem.OffsetAmount * -1)
                    End If
                    If Not DicFIT.ContainsKey(oitem.IDNumber) Then
                        DicFIT.Add(oitem.IDNumber, oitem.OffsetAmount)
                    Else
                        DicFIT.Item(oitem.IDNumber) += oitem.OffsetAmount
                    End If
                End If
            Next
        Next

    End Sub

    Private Function GeneratePymtIncludingOffsetting(ByRef CapDicCol As Dictionary(Of String, Integer),
                                                ByVal CAPCollCount As Integer) As Object(,)
        Dim ret As Object(,) = New Object(,) {}

        Me.GetFITOffsetting()
        Dim ForCAPCashColl = (From x In Me.EnergyListCollection Where x.CollectionCategory = EnumCollectionCategory.Cash Select x).Union _
                            (From x In Me.VATonEnergyCollectionList Where x.CollectionCategory = EnumCollectionCategory.Cash Select x).Union _
                            (From x In Me.MFwithVATCollectionList Where x.CollectionCategory = EnumCollectionCategory.Cash Select x).ToList()

        Dim ForCAPCASHDrawDownPymt = (From x In Me.EnergyAllocationList Select x).Union _
                                     (From x In Me.VATonEnergyAllocationList Select x).Union _
                                     (From x In Me.MFwithVATAllocationList Select x).ToList()

        Dim ForCAPOffsettingColl = (From x In Me.OffsettingEnergyCollectionList Select x).Union _
                                   (From x In Me.OffsettingVATonEnergyCollectionList Select x).Union _
                                   (From x In Me.OffsettingMFwithVATCollectionList Select x).ToList()

        Dim ForCAPOffsettingPymt = (From x In Me.OffsettingEnergyAllocationList Select x).Union _
                                   (From x In Me.OffsettingVATonEnergyAllocationList Select x).ToList()

        Dim DistinctParticipants = (From x In ForCAPCASHDrawDownPymt Select x.IDNumber, x.ParticipantID).Union _
                                   (From x In ForCAPOffsettingColl Select x.IDNumber, x.ParticipantID).Union _
                                   (From x In ForCAPCashColl Select x.IDNumber, x.ParticipantID).Union _
                                   (From x In ForCAPOffsettingPymt Select x.IDNumber, x.ParticipantID).ToList

        Dim DistinctBillingPeriod = (From x In ForCAPCASHDrawDownPymt Select x.BillingPeriod).Union _
                                   (From x In ForCAPOffsettingColl Select x.BillingPeriod).Union _
                                   (From x In ForCAPCashColl Select x.BillingPeriod).Union _
                                   (From x In ForCAPOffsettingPymt Select x.BillingPeriod).ToList

        Dim SortDistinctParticipants = (From x In Me._AMParticipants Order By x.ParticipantID Select x.IDNumber, x.ParticipantID).Distinct().ToList

        Dim CAPContentPymt As Object(,) = New Object(,) {}
        ReDim CAPContentPymt(SortDistinctParticipants.Count + 1, CAPCollCount)
        CAPContentPymt(0, 0) = "Payments:"
        Dim r As Integer = 0

        For Each item In SortDistinctParticipants
            r += 1
            Dim GetPymnt = (From x In ForCAPCASHDrawDownPymt Where x.IDNumber = item.IDNumber And x.ParticipantID = item.ParticipantID Select x).ToList()

            Dim GetOffsettingColl = (From x In ForCAPOffsettingColl Where x.IDNumber = item.IDNumber And x.ParticipantID = item.ParticipantID Select x).ToList()

            Dim GetOffsettingPymt = (From x In ForCAPOffsettingPymt Where x.IDNumber = item.IDNumber And x.ParticipantID = item.ParticipantID Select x).ToList()

            Dim GetTransToPRperMP = (From x In Me.FundTransferform Where x.TransType = EnumFTFTransType.Replenishment Select x).FirstOrDefault()

            Dim GetCurrentDeferredPaymentOnEnergy = (From x In Me.PrevDeferredPaymentList
                                                     Where x.IDNumber = item.IDNumber And x.ChargeType = EnumChargeType.E
                                                     Select x.OutstandingBalanceDeferredPayment).FirstOrDefault

            If IsDBNull(GetCurrentDeferredPaymentOnEnergy) Then
                GetCurrentDeferredPaymentOnEnergy = Nothing
            End If


            Dim GetCurrentDeferredPaymentOnVAT = (From x In Me.PrevDeferredPaymentList
                                                  Where x.IDNumber = item.IDNumber And x.ChargeType = EnumChargeType.EV
                                                  Select x.OutstandingBalanceDeferredPayment).FirstOrDefault

            If IsDBNull(GetCurrentDeferredPaymentOnVAT) Then
                GetCurrentDeferredPaymentOnVAT = Nothing
            End If

            Dim ExcessCollList = (From x In Me.CollectionMonitoring
                                  Where x.IDNumber.IDNumber = item.IDNumber _
                                  And x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection
                                  Select x.Amount).Sum

            If ExcessCollList = 0 Then
                CAPContentPymt(r, UBound(CAPContentPymt, 2) - 5) = Nothing
            Else
                CAPContentPymt(r, UBound(CAPContentPymt, 2) - 5) = If(ExcessCollList = 0, Nothing, ExcessCollList)
            End If

            CAPContentPymt(r, 0) = item.IDNumber
            CAPContentPymt(r, 1) = item.ParticipantID
            CAPContentPymt(UBound(CAPContentPymt, 1), 0) = "Total Payment:"

            If Not IsNothing(GetTransToPRperMP) Then
                Dim ReplinishmentAmount As Decimal = (From x In GetTransToPRperMP.ListOfFTFParticipants Where x.IDNumber.IDNumber = item.IDNumber Select x.Amount).FirstOrDefault()

                If Not ReplinishmentAmount = 0D Then
                    CAPContentPymt(r, UBound(CAPContentPymt, 2) - 6) = ReplinishmentAmount * -1
                End If

            End If

            CAPContentPymt(r, UBound(CAPContentPymt, 2) - 4) = GetCurrentDeferredPaymentOnEnergy
            CAPContentPymt(r, UBound(CAPContentPymt, 2) - 3) = GetCurrentDeferredPaymentOnVAT
            If Me.DicFIT.ContainsKey(item.IDNumber) Then
                CAPContentPymt(r, UBound(CAPContentPymt, 2) - 2) = Me.DicFIT.Item(item.IDNumber)
            End If

            Dim getFinPin = (From x In Me.PaymentTransferToPRList Where x.IDNumber = item.IDNumber Select x).FirstOrDefault
            If Not getFinPin Is Nothing Then
                If getFinPin.TransferToFinPen <> 0 Then
                    CAPContentPymt(r, UBound(CAPContentPymt, 2) - 1) = getFinPin.TransferToFinPen * -1
                End If
            End If

            For Each PymtItem In GetPymnt
                Dim CAPHeader1 As String = PymtItem.BillingRemarks.ToString & vbNewLine _
                                           & PymtItem.WESMBillBatchNo _
                                           & " [BPNo." & PymtItem.BillingPeriod & " - " _
                                           & PymtItem.DueDate.ToShortDateString & "]"

                Dim PymtType = ([Enum].GetName(GetType(EnumPaymentNewType), PymtItem.PaymentType)).ToString
                Dim PymtCategory = ([Enum].GetName(GetType(EnumCollectionCategory), PymtItem.PaymentCategory)).ToString
                Dim CAPHeader2 As String = PymtType & " - " & PymtCategory
                Dim ColName = CAPHeader1.ToString & " " & CAPHeader2.ToString
                If CapDicCol.ContainsKey(ColName) Then
                    Dim c As Integer = CapDicCol(ColName)
                    CAPContentPymt(r, c) = CDec(CAPContentPymt(r, c)) + PymtItem.AllocationAmount
                    Select Case PymtItem.PaymentType
                        Case EnumPaymentNewType.Energy, EnumPaymentNewType.DefaultInterestOnEnergy
                            CAPContentPymt(r, UBound(CAPContentPymt, 2) - 10) = CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 10)) + PymtItem.AllocationAmount
                        Case EnumPaymentNewType.VatOnEnergy
                            CAPContentPymt(r, UBound(CAPContentPymt, 2) - 8) = CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 8)) + PymtItem.AllocationAmount
                        Case EnumPaymentNewType.MarketFees, EnumPaymentNewType.VatOnMarketFees,
                            EnumPaymentNewType.DefaultInterestOnMF, EnumPaymentNewType.DefaultInterestOnVatOnMF,
                            EnumPaymentNewType.WithholdingTaxOnMF, EnumPaymentNewType.WithholdingTaxOnDefaultInterest,
                            EnumPaymentNewType.WithholdingVatOnMF, EnumPaymentNewType.WithholdingVatOnDefaultInterest
                            CAPContentPymt(r, UBound(CAPContentPymt, 2) - 9) = CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 9)) + PymtItem.AllocationAmount
                    End Select

                    CAPContentPymt(r, UBound(CAPContentPymt, 2) - 7) = CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 10)) + CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 9)) + CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 8))
                End If
            Next

            For Each OffsetCollItem In GetOffsettingColl

                Dim CAPHeader1 As String = OffsetCollItem.BillingRemarks.ToString & vbNewLine _
                                           & OffsetCollItem.WESMBillBatchNo _
                                           & " [BPNo." & OffsetCollItem.BillingPeriod & " - " _
                                           & OffsetCollItem.DueDate.ToShortDateString & "]"

                Dim CollType = ([Enum].GetName(GetType(EnumCollectionType), OffsetCollItem.CollectionType)).ToString
                Dim CollCategory = ([Enum].GetName(GetType(EnumCollectionCategory), OffsetCollItem.CollectionCategory)).ToString
                Dim CAPHeader2 As String = CollType & " - " & CollCategory
                Dim ColName = CAPHeader1.ToString & " " & CAPHeader2.ToString
                If CapDicCol.ContainsKey(ColName) Then
                    Dim c As Integer = CapDicCol(ColName)
                    CAPContentPymt(r, c) = CDec(CAPContentPymt(r, c)) + OffsetCollItem.AllocationAmount
                    Select Case OffsetCollItem.CollectionType
                        Case EnumCollectionType.Energy, EnumCollectionType.DefaultInterestOnEnergy
                            CAPContentPymt(r, UBound(CAPContentPymt, 2) - 10) = CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 10)) + OffsetCollItem.AllocationAmount
                        Case EnumCollectionType.VatOnEnergy
                            CAPContentPymt(r, UBound(CAPContentPymt, 2) - 8) = CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 8)) + OffsetCollItem.AllocationAmount
                        Case EnumCollectionType.MarketFees, EnumCollectionType.VatOnMarketFees,
                            EnumCollectionType.DefaultInterestOnMF, EnumCollectionType.DefaultInterestOnVatOnMF,
                            EnumCollectionType.WithholdingTaxOnMF, EnumCollectionType.WithholdingTaxOnDefaultInterest,
                            EnumCollectionType.WithholdingVatOnMF, EnumCollectionType.WithholdingVatOnDefaultInterest
                            CAPContentPymt(r, UBound(CAPContentPymt, 2) - 9) = CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 9)) + OffsetCollItem.AllocationAmount
                    End Select
                    CAPContentPymt(r, UBound(CAPContentPymt, 2) - 7) = CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 10)) + CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 9)) + CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 8))
                End If
            Next

            For Each OffsetPymtItem In GetOffsettingPymt

                Dim CAPHeader1 As String = OffsetPymtItem.BillingRemarks.ToString & vbNewLine _
                                           & OffsetPymtItem.WESMBillBatchNo _
                                           & " [BPNo." & OffsetPymtItem.BillingPeriod & " - " _
                                           & OffsetPymtItem.DueDate.ToShortDateString & "]"

                Dim PymtType = ([Enum].GetName(GetType(EnumPaymentNewType), OffsetPymtItem.PaymentType)).ToString
                Dim PymtCategory = ([Enum].GetName(GetType(EnumCollectionCategory), OffsetPymtItem.PaymentCategory)).ToString
                Dim CAPHeader2 As String = PymtType & " - " & PymtCategory
                Dim ColName = CAPHeader1.ToString & " " & CAPHeader2.ToString
                If CapDicCol.ContainsKey(Trim(ColName)) Then
                    Dim c As Integer = CapDicCol(ColName)
                    CAPContentPymt(r, c) = CDec(CAPContentPymt(r, c)) + OffsetPymtItem.AllocationAmount
                    Select Case OffsetPymtItem.PaymentType
                        Case EnumPaymentNewType.Energy, EnumPaymentNewType.DefaultInterestOnEnergy
                            CAPContentPymt(r, UBound(CAPContentPymt, 2) - 10) = CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 10)) + OffsetPymtItem.AllocationAmount
                        Case EnumPaymentNewType.VatOnEnergy
                            CAPContentPymt(r, UBound(CAPContentPymt, 2) - 8) = CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 8)) + OffsetPymtItem.AllocationAmount
                        Case EnumPaymentNewType.MarketFees, EnumPaymentNewType.VatOnMarketFees,
                            EnumPaymentNewType.DefaultInterestOnMF, EnumPaymentNewType.DefaultInterestOnVatOnMF,
                            EnumPaymentNewType.WithholdingTaxOnMF, EnumPaymentNewType.WithholdingTaxOnDefaultInterest,
                            EnumPaymentNewType.WithholdingVatOnMF, EnumPaymentNewType.WithholdingVatOnDefaultInterest
                            CAPContentPymt(r, UBound(CAPContentPymt, 2) - 9) = CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 9)) + OffsetPymtItem.AllocationAmount
                    End Select
                    CAPContentPymt(r, UBound(CAPContentPymt, 2) - 7) = CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 10)) + CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 9)) + CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 8))
                End If
            Next

            CAPContentPymt(r, UBound(CAPContentPymt, 2)) = CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 7)) +
                                                           CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 6)) +
                                                           CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 5)) +
                                                           CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 4)) +
                                                           CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 3)) +
                                                           CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 2)) +
                                                           CDec(CAPContentPymt(r, UBound(CAPContentPymt, 2) - 1))

            For i As Integer = 2 To UBound(CAPContentPymt, 2)
                CAPContentPymt(UBound(CAPContentPymt, 1), i) = CDec(CAPContentPymt(UBound(CAPContentPymt, 1), i)) + CDec(CAPContentPymt(r, i))
            Next
        Next
        ret = CAPContentPymt
        Return ret
    End Function

    Private Function GenerateCollCash(ByRef CapDicCol As Dictionary(Of String, Integer),
                                      ByVal CAPCollCount As Integer) As Object(,)
        Dim ret As Object(,) = New Object(,) {}

        Dim ForCAPCashColl = (From x In Me.EnergyListCollection Where x.CollectionCategory = EnumCollectionCategory.Cash Select x).Union _
                             (From x In Me.VATonEnergyCollectionList Where x.CollectionCategory = EnumCollectionCategory.Cash Select x).Union _
                             (From x In Me.MFwithVATCollectionList Where x.CollectionCategory = EnumCollectionCategory.Cash Select x).ToList()

        Dim ParticipantwithCollection = (From x In Me.EnergyListCollection Where x.CollectionCategory = EnumCollectionCategory.Cash Select x.IDNumber, x.ParticipantID).Union _
                                        (From x In Me.VATonEnergyCollectionList Where x.CollectionCategory = EnumCollectionCategory.Cash Select x.IDNumber, x.ParticipantID).Union _
                                        (From x In Me.MFwithVATCollectionList Where x.CollectionCategory = EnumCollectionCategory.Cash Select x.IDNumber, x.ParticipantID).Union _
                                        (From x In Me.CollectionMonitoring Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection Select x.IDNumber.IDNumber, x.IDNumber.ParticipantID).ToList().Union _
                                        (From x In Me.PrevDeferredPaymentList Join y In Me.AMParticipants On x.IDNumber Equals y.IDNumber Select x.IDNumber, y.ParticipantID).Union _
                                        (From x In Me.CollectionMonitoring Where x.TransType = EnumCollectionMonitoringType.TransferToPRReplenishment Select x.IDNumber.IDNumber, x.IDNumber.ParticipantID).ToList()

        Dim DistinctParticipants = (From x In ParticipantwithCollection Select x).Distinct().ToList()

        Dim DistinctBillingPeriod = (From x In ForCAPCashColl Select x.BillingPeriod).Distinct.ToList

        Dim CAPContentColl As Object(,) = New Object(,) {}
        ReDim CAPContentColl(DistinctParticipants.Count + 1, CAPCollCount)
        CAPContentColl(0, 0) = "Collections:"
        Dim r As Integer = 0
        For Each item In DistinctParticipants
            Dim CASHCollList = (From x In ForCAPCashColl Where x.IDNumber = item.IDNumber And x.ParticipantID = item.ParticipantID Select x).ToList()
            r += 1
            CAPContentColl(r, 0) = item.IDNumber
            CAPContentColl(r, 1) = item.ParticipantID
            CAPContentColl(UBound(CAPContentColl, 1), 0) = "Total Collection:"

            Dim ExcessCollList = (From x In Me.CollectionMonitoring
                                  Where x.IDNumber.IDNumber = item.IDNumber _
                                  And x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection
                                  Select x.Amount).Sum

            If ExcessCollList = 0 Then
                CAPContentColl(r, UBound(CAPContentColl, 2) - 5) = Nothing
            Else
                CAPContentColl(r, UBound(CAPContentColl, 2) - 5) = If(ExcessCollList = 0, Nothing, ExcessCollList)
            End If


            Dim PRReplenishment = (From x In Me.CollectionMonitoring
                                   Where x.IDNumber.IDNumber = item.IDNumber _
                                   And x.TransType = EnumCollectionMonitoringType.TransferToPRReplenishment
                                   Select x.Amount).Sum
            If PRReplenishment = 0 Then
                CAPContentColl(r, UBound(CAPContentColl, 2) - 6) = Nothing
            Else
                CAPContentColl(r, UBound(CAPContentColl, 2) - 6) = If(PRReplenishment = 0, Nothing, PRReplenishment)
            End If

            For Each CollItem In CASHCollList
                Dim CAPHeader1 As String = CollItem.BillingRemarks.ToString & vbNewLine _
                                           & CollItem.WESMBillBatchNo _
                                           & " [BPNo." & CollItem.BillingPeriod & " - " _
                                           & CollItem.DueDate.ToShortDateString & "]"


                Dim CollType = ([Enum].GetName(GetType(EnumCollectionType), CollItem.CollectionType)).ToString
                Dim CollCategory = ([Enum].GetName(GetType(EnumCollectionCategory), CollItem.CollectionCategory)).ToString
                Dim CAPHeader2 As String = CollType & " - " & CollCategory
                Dim ColName = CAPHeader1.ToString & " " & CAPHeader2.ToString
                If CapDicCol.ContainsKey(ColName) Then
                    Dim c As Integer = CapDicCol(ColName)
                    CAPContentColl(r, c) = CDec(CAPContentColl(r, c)) + (CollItem.AllocationAmount * -1)
                    Select Case CollItem.CollectionType
                        Case EnumCollectionType.Energy, EnumCollectionType.DefaultInterestOnEnergy
                            CAPContentColl(r, UBound(CAPContentColl, 2) - 10) = CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 10)) + (CollItem.AllocationAmount * -1)
                        Case EnumCollectionType.VatOnEnergy
                            CAPContentColl(r, UBound(CAPContentColl, 2) - 8) = CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 8)) + (CollItem.AllocationAmount * -1)
                        Case EnumCollectionType.MarketFees, EnumCollectionType.VatOnMarketFees,
                            EnumCollectionType.DefaultInterestOnMF, EnumCollectionType.DefaultInterestOnVatOnMF,
                            EnumCollectionType.WithholdingTaxOnMF, EnumCollectionType.WithholdingTaxOnDefaultInterest,
                            EnumCollectionType.WithholdingVatOnMF, EnumCollectionType.WithholdingVatOnDefaultInterest
                            CAPContentColl(r, UBound(CAPContentColl, 2) - 9) = CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 9)) + (CollItem.AllocationAmount * -1)
                    End Select
                End If
            Next
            CAPContentColl(r, UBound(CAPContentColl, 2) - 7) = CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 10)) + CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 9)) + CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 8))

            CAPContentColl(r, UBound(CAPContentColl, 2)) = CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 7)) +
                                                            CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 6)) +
                                                            CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 5)) +
                                                            CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 4)) +
                                                            CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 3)) +
                                                            CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 2)) +
                                                            CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 1))

            For i As Integer = 2 To UBound(CAPContentColl, 2)
                CAPContentColl(UBound(CAPContentColl, 1), i) = CDec(CAPContentColl(UBound(CAPContentColl, 1), i)) + CDec(CAPContentColl(r, i))
            Next
        Next
        ret = CAPContentColl
        Return ret
    End Function

    Private Function GenerateCollDrawDown(ByRef CapDicCol As Dictionary(Of String, Integer),
                                          ByVal CAPCollCount As Integer) As Object(,)
        Dim ret As Object(,) = New Object(,) {}

        Dim ForEnergyCAPColl = (From x In Me.EnergyListCollection Where x.CollectionCategory = EnumCollectionCategory.Drawdown Select x).ToList()

        Dim DistinctParticipants = (From x In ForEnergyCAPColl Select x.IDNumber, x.ParticipantID).Distinct().ToList()

        Dim DistinctBillingPeriod = (From x In ForEnergyCAPColl Select x.BillingPeriod).Distinct.ToList

        Dim CAPContentColl As Object(,) = New Object(,) {}
        ReDim CAPContentColl(DistinctParticipants.Count + 1, CAPCollCount)
        CAPContentColl(0, 0) = "DrawDown:"
        Dim r As Integer = 0
        For Each item In DistinctParticipants
            Dim PRCollList = (From x In ForEnergyCAPColl Where x.IDNumber = item.IDNumber And x.ParticipantID = item.ParticipantID Select x).ToList()
            r += 1
            CAPContentColl(r, 0) = item.IDNumber
            CAPContentColl(r, 1) = item.ParticipantID
            CAPContentColl(UBound(CAPContentColl, 1), 0) = "Total DrawDown:"
            For Each DDItem In PRCollList
                Dim CAPHeader1 As String = DDItem.BillingRemarks.ToString & vbNewLine _
                                           & DDItem.WESMBillBatchNo _
                                           & " [BPNo." & DDItem.BillingPeriod & " - " _
                                           & DDItem.DueDate.ToShortDateString & "]"

                Dim CollType = ([Enum].GetName(GetType(EnumCollectionType), DDItem.CollectionType)).ToString
                Dim CollCategory = ([Enum].GetName(GetType(EnumCollectionCategory), DDItem.CollectionCategory)).ToString
                Dim CAPHeader2 As String = CollType & " - " & CollCategory
                Dim ColName = CAPHeader1.ToString & " " & CAPHeader2.ToString
                If CapDicCol.ContainsKey(ColName) Then
                    Dim c As Integer = CapDicCol(ColName)
                    CAPContentColl(r, c) = CDec(CAPContentColl(r, c)) + (DDItem.AllocationAmount * -1)
                    CAPContentColl(r, UBound(CAPContentColl, 2) - 7) = CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 7)) + (DDItem.AllocationAmount * -1)
                    CAPContentColl(r, UBound(CAPContentColl, 2) - 10) = CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 10)) + (DDItem.AllocationAmount * -1)
                    CAPContentColl(r, UBound(CAPContentColl, 2)) = CDec(CAPContentColl(r, UBound(CAPContentColl, 2) - 10))
                End If
            Next
            For i As Integer = 2 To UBound(CAPContentColl, 2)
                CAPContentColl(UBound(CAPContentColl, 1), i) = CDec(CAPContentColl(UBound(CAPContentColl, 1), i)) + CDec(CAPContentColl(r, i))
            Next
        Next

        ret = CAPContentColl
        Return ret
    End Function

    Private Function GenerateCollPymtHeader(ByRef CapDicCol As Dictionary(Of String, Integer)) As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ForEnergyCAP = (From x In Me.EnergyListCollection Select x.WESMBillBatchNo, x.BillingPeriod, x.DueDate, x.CollectionType, x.CollectionCategory, x.BillingRemarks).Union _
                           (From x In Me.OffsettingEnergyCollectionList Order By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate Select x.WESMBillBatchNo, x.BillingPeriod, x.DueDate, x.CollectionType, x.CollectionCategory, x.BillingRemarks).Distinct().ToList()

        Dim ForVATCAP = (From x In Me.VATonEnergyCollectionList Select x.WESMBillBatchNo, x.BillingPeriod, x.DueDate, x.CollectionType, x.CollectionCategory, x.BillingRemarks).Union _
                        (From x In Me.OffsettingVATonEnergyCollectionList Order By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate Select x.WESMBillBatchNo, x.BillingPeriod, x.DueDate, x.CollectionType, x.CollectionCategory, x.BillingRemarks).Distinct().ToList()

        Dim ForMFCAP = (From x In Me.MFwithVATCollectionList Select x.WESMBillBatchNo, x.BillingPeriod, x.DueDate, x.CollectionType, x.CollectionCategory, x.BillingRemarks).Union _
                       (From x In Me.OffsettingMFwithVATCollectionList Order By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate Select x.WESMBillBatchNo, x.BillingPeriod, x.DueDate, x.CollectionType, x.CollectionCategory, x.BillingRemarks).Distinct().ToList()

        Dim ForMFCAP2 = (From x In Me.MFwithVATAllocationList Select x.WESMBillBatchNo, x.BillingPeriod, x.DueDate, x.PaymentType, x.PaymentCategory, x.BillingRemarks).Union _
                        (From x In Me.OffsettingMFwithVATAllocationList Order By x.WESMBillBatchNo, x.BillingPeriod, x.DueDate Select x.WESMBillBatchNo, x.BillingPeriod, x.DueDate, x.PaymentType, x.PaymentCategory, x.BillingRemarks).Distinct().ToList()

        Dim CombinationOfBillingPeriod = (From x In ForEnergyCAP Select x).Union _
                                         (From x In ForVATCAP Select x).Union _
                                         (From x In ForMFCAP Select x).Distinct().ToList


        Dim SortCombOfBillingPeriod = (From x In CombinationOfBillingPeriod Select x.WESMBillBatchNo, x.BillingPeriod, x.DueDate Order By WESMBillBatchNo, BillingPeriod, DueDate).Distinct().ToList()
        Dim ForWESMBill = (From x In Me.WESMBillList Select x.BillingPeriod, x.DueDate, x.Remarks, x.ChargeType Order By BillingPeriod, DueDate).Distinct().ToList()

        Dim CAPHeader As Object(,) = New Object(,) {}
        ReDim CAPHeader(6, 1)
        CAPHeader(0, 0) = AMModule.CompanyFullName & vbNewLine _
                        & "Collection-Payment Summary"
        CAPHeader(1, 0) = Me.PayAllocDate.RemittanceDate
        CAPHeader(2, 1) = "Mode of Settlement:"
        CAPHeader(4, 0) = "Surplus"
        CAPHeader(5, 0) = "NSS Interest"

        For Each Item In SortCombOfBillingPeriod
            Dim CAPHeader1 As String = ""
            Dim EnergyCAP = (From x In ForEnergyCAP
                             Where x.WESMBillBatchNo = Item.WESMBillBatchNo _
                             And x.BillingPeriod = Item.BillingPeriod _
                             And x.DueDate = Item.DueDate
                             Select x).ToList()

            Dim ColName As String = ""
            If EnergyCAP.Count > 0 Then
                For Each Eitem In EnergyCAP
                    ReDim Preserve CAPHeader(6, UBound(CAPHeader, 2) + 1)
                    CAPHeader(0, UBound(CAPHeader, 2)) = Eitem.BillingRemarks & vbNewLine _
                                                    & Item.WESMBillBatchNo _
                                                    & " [BPNo." & Item.BillingPeriod & " - " _
                                                    & Item.DueDate.ToShortDateString & "]"

                    Dim CollType = ([Enum].GetName(GetType(EnumCollectionType), Eitem.CollectionType)).ToString
                    Dim CollCategory = ([Enum].GetName(GetType(EnumCollectionCategory), Eitem.CollectionCategory)).ToString
                    CAPHeader(1, UBound(CAPHeader, 2)) = CollType & " - " & CollCategory

                    ColName = CAPHeader(0, UBound(CAPHeader, 2)).ToString & " " & CAPHeader(1, UBound(CAPHeader, 2)).ToString
                    If Not CapDicCol.ContainsKey(ColName) Then
                        CapDicCol.Add(ColName, UBound(CAPHeader, 2))
                    End If
                Next
            End If

            Dim VATCAP = (From x In ForVATCAP
                          Where x.WESMBillBatchNo = Item.WESMBillBatchNo _
                          And x.BillingPeriod = Item.BillingPeriod _
                          And x.DueDate = Item.DueDate
                          Select x).ToList()

            If VATCAP.Count > 0 Then
                For Each VItem In VATCAP
                    ReDim Preserve CAPHeader(6, UBound(CAPHeader, 2) + 1)
                    CAPHeader(0, UBound(CAPHeader, 2)) = VItem.BillingRemarks & vbNewLine _
                                                    & Item.WESMBillBatchNo _
                                                    & " [BPNo." & Item.BillingPeriod & " - " _
                                                    & Item.DueDate.ToShortDateString & "]"

                    Dim CollTypeV = ([Enum].GetName(GetType(EnumCollectionType), VItem.CollectionType)).ToString
                    Dim CollCategoryV = ([Enum].GetName(GetType(EnumCollectionCategory), VItem.CollectionCategory)).ToString
                    CAPHeader(1, UBound(CAPHeader, 2)) = CollTypeV & " - " & CollCategoryV

                    ColName = CAPHeader(0, UBound(CAPHeader, 2)).ToString & " " & CAPHeader(1, UBound(CAPHeader, 2)).ToString
                    If Not CapDicCol.ContainsKey(ColName) Then
                        CapDicCol.Add(ColName, UBound(CAPHeader, 2))
                    End If
                Next
            End If

            Dim MFCAP = (From x In ForMFCAP
                         Where x.WESMBillBatchNo = Item.WESMBillBatchNo _
                         And x.BillingPeriod = Item.BillingPeriod _
                         And x.DueDate = Item.DueDate
                         Select x).ToList()

            If MFCAP.Count > 0 Then
                For Each MFItem In MFCAP
                    ReDim Preserve CAPHeader(6, UBound(CAPHeader, 2) + 1)
                    CAPHeader(0, UBound(CAPHeader, 2)) = MFItem.BillingRemarks & vbNewLine _
                                                     & Item.WESMBillBatchNo _
                                                     & " [BPNo." & Item.BillingPeriod & " - " _
                                                     & Item.DueDate.ToShortDateString & "]"

                    Dim CollTypeV = ([Enum].GetName(GetType(EnumCollectionType), MFItem.CollectionType)).ToString
                    Dim CollCategoryV = ([Enum].GetName(GetType(EnumCollectionCategory), MFItem.CollectionCategory)).ToString
                    CAPHeader(1, UBound(CAPHeader, 2)) = CollTypeV & " - " & CollCategoryV

                    ColName = CAPHeader(0, UBound(CAPHeader, 2)).ToString & " " & CAPHeader(1, UBound(CAPHeader, 2)).ToString
                    If Not CapDicCol.ContainsKey(ColName) Then
                        CapDicCol.Add(ColName, UBound(CAPHeader, 2))
                    End If
                Next
            End If

            Dim MFCAP2 = (From x In ForMFCAP2
                          Where x.WESMBillBatchNo = Item.WESMBillBatchNo _
                          And x.BillingPeriod = Item.BillingPeriod _
                          And x.DueDate = Item.DueDate
                          Select x).ToList()

            If MFCAP2.Count <> 0 Then
                For Each MFItem In MFCAP2
                    Dim Header As String = MFItem.BillingRemarks & vbNewLine _
                                                         & Item.WESMBillBatchNo _
                                                         & " [BPNo." & Item.BillingPeriod & " - " _
                                                         & Item.DueDate.ToShortDateString & "]"


                    Dim CollTypeV = ([Enum].GetName(GetType(EnumPaymentNewType), MFItem.PaymentType)).ToString
                    Dim CollCategoryV = ([Enum].GetName(GetType(EnumCollectionCategory), MFItem.PaymentCategory)).ToString
                    Dim Header2 As String = CollTypeV & " - " & CollCategoryV

                    Dim PayName = Header.ToString & " " & Header2.ToString
                    If Not CapDicCol.ContainsKey(PayName) Then
                        ReDim Preserve CAPHeader(6, UBound(CAPHeader, 2) + 1)
                        CapDicCol.Add(PayName, UBound(CAPHeader, 2))
                        CAPHeader(0, UBound(CAPHeader, 2)) = Header
                        CAPHeader(1, UBound(CAPHeader, 2)) = Header2
                    End If
                Next
            End If
        Next

        ReDim Preserve CAPHeader(6, UBound(CAPHeader, 2) + 11)
        CAPHeader(0, UBound(CAPHeader, 2) - 10) = "Total " & vbNewLine & "Energy And Default"
        CAPHeader(0, UBound(CAPHeader, 2) - 9) = "Total " & vbNewLine & "MF And Default"
        CAPHeader(0, UBound(CAPHeader, 2) - 8) = "Total " & vbNewLine & "VAT"
        CAPHeader(0, UBound(CAPHeader, 2) - 7) = "Total " & vbNewLine & "(Energy, MF And VAT)"
        CAPHeader(0, UBound(CAPHeader, 2) - 6) = "Replenishment of Prudential"
        CAPHeader(0, UBound(CAPHeader, 2) - 5) = "Excess Collection"
        CAPHeader(0, UBound(CAPHeader, 2) - 4) = "Deferred Payments - Energy"
        CAPHeader(0, UBound(CAPHeader, 2) - 3) = "Deferred Payments - VAT"
        CAPHeader(0, UBound(CAPHeader, 2) - 2) = "Offsetting on FIT"
        CAPHeader(0, UBound(CAPHeader, 2) - 1) = "Financial Penalty"
        CAPHeader(0, UBound(CAPHeader, 2)) = "GRAND TOTAL"

        ret = CAPHeader

        Return ret
    End Function

    Private Function GenerateCollectionArr(ByVal CollectionList As List(Of ARCollection),
                                           ByVal iChargeType As EnumChargeType,
                                           ByVal DistinctSequence As List(Of Integer)) As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim DistinctGroup = (From x In CollectionList Select x.InvoiceNumber, x.WESMBillBatchNo).Distinct.ToList
        Dim RowCount As Integer = DistinctGroup.Count()


        Select Case iChargeType
            Case EnumChargeType.E
                Dim EnergyArr As Object(,) = New Object(,) {}
                ReDim EnergyArr(RowCount, 15)
                For Each GroupItem In DistinctGroup
                    Dim GetCollectionList As List(Of ARCollection) = (From x In CollectionList
                                                                      Where x.InvoiceNumber = GroupItem.InvoiceNumber _
                                                                      And x.WESMBillBatchNo = GroupItem.WESMBillBatchNo
                                                                      Select x Order By x.CollectionCategory, x.CollectionType Descending).ToList()

                    Dim MinOffsetSeq As Integer = (From x In GetCollectionList
                                                   Where x.CollectionType = EnumCollectionType.Energy _
                                                   Or x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                                   Or x.CollectionType = EnumCollectionType.NoTransaction
                                                   Select x).DefaultIfEmpty.Min(Function(x) x.OffsettingSequence)

                    Dim MaxOffsetSeq As Integer = (From x In GetCollectionList
                                                   Where x.CollectionType = EnumCollectionType.Energy _
                                                   Or x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                                   Or x.CollectionType = EnumCollectionType.NoTransaction
                                                   Select x).DefaultIfEmpty.Max(Function(x) x.OffsettingSequence)

                    'Ending Balance before collection
                    Dim GetEndingBalanceEnergy As ARCollection = (From x In GetCollectionList
                                                                  Where (x.CollectionType = EnumCollectionType.Energy _
                                                                      Or x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                                                      Or x.CollectionType = EnumCollectionType.NoTransaction) _
                                                                  And x.OffsettingSequence = MinOffsetSeq
                                                                  Select x Order By x.EndingBalance, x.CollectionCategory).FirstOrDefault

                    'Total Energy Collection on Cash 
                    Dim SumCashColEnergy As Decimal = (From x In GetCollectionList
                                                       Where x.CollectionType = EnumCollectionType.Energy _
                                                       And x.CollectionCategory = EnumCollectionCategory.Cash
                                                       Select x.AllocationAmount).Sum()

                    'Total WithholdingTax Energy Collection on Cash 
                    Dim SumCashColEnergyWithholdingTax As Decimal = (From x In GetCollectionList
                                                                     Where x.CollectionType = EnumCollectionType.WithholdingTaxonEnergy _
                                                                     And x.CollectionCategory = EnumCollectionCategory.Cash
                                                                     Select x.AllocationAmount).Sum()


                    'Total Energy Collection on Offsetting                    
                    Dim SumDrawColEnergy As Decimal = (From x In GetCollectionList
                                                       Where x.CollectionType = EnumCollectionType.Energy _
                                                       And x.CollectionCategory = EnumCollectionCategory.Drawdown
                                                       Select x.AllocationAmount).Sum()

                    'Total Energy Collection on Offsetting                    
                    Dim SumOffsetColEnergy As Decimal = (From x In GetCollectionList
                                                         Where x.CollectionType = EnumCollectionType.Energy _
                                                         And x.CollectionCategory = EnumCollectionCategory.Offset
                                                         Select x.AllocationAmount).Sum()

                    'Total Default Interest on Energy Collection Cash or Offset
                    Dim SumDefaultInterest As Decimal = (From x In GetCollectionList
                                                         Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                                         And (x.CollectionCategory = EnumCollectionCategory.Cash _
                                                              Or x.CollectionCategory = EnumCollectionCategory.Offset _
                                                              Or x.CollectionCategory = EnumCollectionCategory.Drawdown)
                                                         Select x.AllocationAmount).Sum()


                    'New Ending Balance

                    Dim GetNewEndingBalanceEnergyOrig As ARCollection = (From x In GetCollectionList
                                                                         Where (x.CollectionType = EnumCollectionType.Energy _
                                                                                Or x.CollectionType = EnumCollectionType.NoTransaction) _
                                                                         And x.OffsettingSequence = MaxOffsetSeq
                                                                         Select x Order By x.NewEndingBalance Descending).FirstOrDefault

                    If GetNewEndingBalanceEnergyOrig Is Nothing Then
                        GetNewEndingBalanceEnergyOrig = New ARCollection
                        GetNewEndingBalanceEnergyOrig = (From x In GetCollectionList
                                                         Where (x.CollectionType = EnumCollectionType.Energy _
                                                             Or x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                                                             Or x.CollectionType = EnumCollectionType.NoTransaction) _
                                                         And x.OffsettingSequence = MaxOffsetSeq
                                                         Select x Order By x.NewEndingBalance Descending).FirstOrDefault
                    End If

                    EnergyArr(RowIndex, 0) = GetEndingBalanceEnergy.BillingRemarks
                    EnergyArr(RowIndex, 1) = GetEndingBalanceEnergy.WESMBillBatchNo
                    EnergyArr(RowIndex, 2) = GetEndingBalanceEnergy.IDNumber
                    EnergyArr(RowCount, 2) = "Total Accounts Receivables"
                    EnergyArr(RowIndex, 3) = GetEndingBalanceEnergy.ParticipantID
                    EnergyArr(RowIndex, 4) = GetEndingBalanceEnergy.InvoiceNumber
                    EnergyArr(RowIndex, 5) = GetEndingBalanceEnergy.DueDate
                    EnergyArr(RowIndex, 6) = GetEndingBalanceEnergy.NewDueDate
                    EnergyArr(RowIndex, 7) = GetEndingBalanceEnergy.EnergyWithHold
                    EnergyArr(RowIndex, 8) = GetEndingBalanceEnergy.EndingBalance
                    EnergyArr(RowIndex, 9) = Nothing
                    EnergyArr(RowIndex, 10) = IIf(SumCashColEnergy = 0D, Nothing, SumCashColEnergy)
                    EnergyArr(RowIndex, 11) = IIf(SumCashColEnergyWithholdingTax = 0D, Nothing, SumCashColEnergyWithholdingTax)
                    EnergyArr(RowIndex, 12) = IIf(SumDrawColEnergy = 0D, Nothing, SumDrawColEnergy)
                    EnergyArr(RowIndex, 13) = IIf(SumOffsetColEnergy = 0D, Nothing, SumOffsetColEnergy)
                    EnergyArr(RowIndex, 14) = IIf(SumDefaultInterest = 0D, Nothing, SumDefaultInterest)
                    EnergyArr(RowIndex, 15) = GetNewEndingBalanceEnergyOrig.NewEndingBalance 'oNewEndingBalance
                    RowIndex += 1
                Next
                ret = EnergyArr

            Case EnumChargeType.EV
                Dim VATArr As Object(,) = New Object(,) {}
                ReDim VATArr(RowCount, 11)
                For Each GroupItem In DistinctGroup

                    Dim GetCollectionList As List(Of ARCollection) = (From x In CollectionList
                                                                      Where x.InvoiceNumber = GroupItem.InvoiceNumber _
                                                                      And x.WESMBillBatchNo = GroupItem.WESMBillBatchNo
                                                                      Select x Order By x.CollectionCategory, x.CollectionType Descending).ToList()

                    Dim MinOffsetSeq As Integer = (From x In GetCollectionList
                                                   Where x.CollectionType = EnumCollectionType.VatOnEnergy _
                                                   Or x.CollectionType = EnumCollectionType.NoTransaction
                                                   Select x).DefaultIfEmpty.Min(Function(x) x.OffsettingSequence)

                    Dim MaxOffsetSeq As Integer = (From x In GetCollectionList
                                                   Where x.CollectionType = EnumCollectionType.VatOnEnergy _
                                                   Or x.CollectionType = EnumCollectionType.NoTransaction
                                                   Select x).DefaultIfEmpty.Max(Function(x) x.OffsettingSequence)

                    'Ending Balance before collection
                    Dim GetEndingBalanceVAT As ARCollection = (From x In GetCollectionList
                                                               Where (x.CollectionType = EnumCollectionType.VatOnEnergy _
                                                                   Or x.CollectionType = EnumCollectionType.NoTransaction) _
                                                               And x.OffsettingSequence = MinOffsetSeq
                                                               Select x Order By x.EndingBalance).FirstOrDefault

                    'Total VAT Collection on Cash 
                    Dim SumCashColVAT As Decimal = (From x In GetCollectionList
                                                    Where x.CollectionType = EnumCollectionType.VatOnEnergy _
                                                    And x.CollectionCategory = EnumCollectionCategory.Cash
                                                    Select x.AllocationAmount).Sum()
                    'Total VAT Collection on Offsetting                    
                    Dim SumOffsetColVAT As Decimal = (From x In GetCollectionList
                                                      Where x.CollectionType = EnumCollectionType.VatOnEnergy _
                                                      And x.CollectionCategory = EnumCollectionCategory.Offset
                                                      Select x.AllocationAmount).Sum()

                    'New Ending Balance
                    Dim GetNewEndingBalanceVAT As ARCollection = (From x In GetCollectionList
                                                                  Where (x.CollectionType = EnumCollectionType.VatOnEnergy _
                                                                     Or x.CollectionType = EnumCollectionType.NoTransaction) _
                                                                  And x.OffsettingSequence = MaxOffsetSeq
                                                                  Select x Order By x.NewEndingBalance Descending).FirstOrDefault


                    VATArr(RowIndex, 0) = GetEndingBalanceVAT.BillingRemarks
                    VATArr(RowIndex, 1) = GetEndingBalanceVAT.WESMBillBatchNo
                    VATArr(RowIndex, 2) = GetEndingBalanceVAT.IDNumber
                    VATArr(RowCount, 2) = "Total Accounts Receivables"
                    VATArr(RowIndex, 3) = GetEndingBalanceVAT.ParticipantID
                    VATArr(RowIndex, 4) = GetEndingBalanceVAT.InvoiceNumber
                    VATArr(RowIndex, 5) = GetEndingBalanceVAT.DueDate
                    VATArr(RowIndex, 6) = GetEndingBalanceVAT.NewDueDate
                    VATArr(RowIndex, 7) = GetEndingBalanceVAT.EndingBalance
                    VATArr(RowIndex, 8) = Nothing
                    VATArr(RowIndex, 9) = IIf(SumCashColVAT = 0D, Nothing, SumCashColVAT)
                    VATArr(RowIndex, 10) = IIf(SumOffsetColVAT = 0D, Nothing, SumOffsetColVAT)
                    VATArr(RowIndex, 11) = GetNewEndingBalanceVAT.NewEndingBalance
                    RowIndex += 1
                Next
                ret = VATArr

            Case EnumChargeType.MF, EnumChargeType.MFV
                Dim MFArr As Object(,) = New Object(,) {}
                ReDim MFArr(RowCount, 18)
                For Each GroupItem In DistinctGroup
                    Dim GetCollectionList As List(Of ARCollection) = (From x In CollectionList
                                                                      Where x.InvoiceNumber = GroupItem.InvoiceNumber _
                                                                      And x.WESMBillBatchNo = GroupItem.WESMBillBatchNo
                                                                      Select x Order By x.OffsettingSequence, x.CollectionCategory, x.CollectionType).ToList()

                    Dim MinoffsetSeq As Integer = (From x In GetCollectionList
                                                   Where (x.CollectionType = EnumCollectionType.MarketFees _
                                                          Or x.CollectionType = EnumCollectionType.VatOnMarketFees) _
                                                   Or x.CollectionType = EnumCollectionType.NoTransaction
                                                   Select x).DefaultIfEmpty.Min(Function(x) x.OffsettingSequence)

                    Dim MaxOffsetSeq As Integer = (From x In GetCollectionList
                                                   Where (x.CollectionType = EnumCollectionType.MarketFees _
                                                          Or x.CollectionType = EnumCollectionType.VatOnMarketFees) _
                                                   Or x.CollectionType = EnumCollectionType.NoTransaction
                                                   Select x).DefaultIfEmpty.Max(Function(x) x.OffsettingSequence)

                    'Ending Balance before collection
                    Dim GetEndingBalanceMF As ARCollection = (From x In GetCollectionList
                                                              Where (x.CollectionType = EnumCollectionType.MarketFees _
                                                                   Or x.CollectionType = EnumCollectionType.NoTransaction) _
                                                              And x.OffsettingSequence = MinoffsetSeq
                                                              Select x Order By x.EndingBalance).FirstOrDefault

                    Dim GetEndingBalanceMFV As ARCollection = (From x In GetCollectionList
                                                               Where (x.CollectionType = EnumCollectionType.VatOnMarketFees _
                                                                    Or x.CollectionType = EnumCollectionType.NoTransaction) _
                                                               And x.OffsettingSequence = MinoffsetSeq
                                                               Select x Order By x.EndingBalance).FirstOrDefault

                    'Total Collection on Cash MF and MFV 
                    Dim SumCashColMFMFV As Decimal = (From x In GetCollectionList
                                                      Where (x.CollectionType = EnumCollectionType.MarketFees _
                                                            Or x.CollectionType = EnumCollectionType.VatOnMarketFees _
                                                            Or x.CollectionType = EnumCollectionType.WithholdingTaxOnMF _
                                                            Or x.CollectionType = EnumCollectionType.WithholdingVatOnMF) _
                                                         And x.CollectionCategory = EnumCollectionCategory.Cash
                                                      Select x.AllocationAmount).Sum()

                    'Total Collection on Offsetting MF and MFV
                    Dim SumOffsetColMFMFV As Decimal = (From x In GetCollectionList
                                                        Where (x.CollectionType = EnumCollectionType.MarketFees _
                                                               Or x.CollectionType = EnumCollectionType.VatOnMarketFees _
                                                               Or x.CollectionType = EnumCollectionType.WithholdingTaxOnMF _
                                                               Or x.CollectionType = EnumCollectionType.WithholdingVatOnMF) _
                                                           And x.CollectionCategory = EnumCollectionCategory.Offset
                                                        Select x.AllocationAmount).Sum()

                    'Total Collection on Default Interest Cash or Offset
                    Dim SumDefault As Decimal = (From x In GetCollectionList
                                                 Where (x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                                                        Or x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF _
                                                        Or x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest _
                                                        Or x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest) _
                                                 And (x.CollectionCategory = EnumCollectionCategory.Cash _
                                                      Or x.CollectionCategory = EnumCollectionCategory.Offset)
                                                 Select x.AllocationAmount).Sum()


                    'Total Collection on Withholding Tax Cash or Offset
                    Dim SumWT As Decimal = (From x In GetCollectionList
                                            Where x.CollectionType = EnumCollectionType.WithholdingTaxOnMF _
                                            And (x.CollectionCategory = EnumCollectionCategory.Cash _
                                                Or x.CollectionCategory = EnumCollectionCategory.Offset)
                                            Select x.AllocationAmount).Sum()

                    'Total Collection on Withholding Tax Default Cash or Offset
                    Dim SumWTDI As Decimal = (From x In GetCollectionList
                                              Where x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest _
                                              And (x.CollectionCategory = EnumCollectionCategory.Cash _
                                                  Or x.CollectionCategory = EnumCollectionCategory.Offset)
                                              Select x.AllocationAmount).Sum()

                    'Total Collection on Withholding VAT Cash or Offset
                    Dim SumWV As Decimal = (From x In GetCollectionList
                                            Where x.CollectionType = EnumCollectionType.WithholdingVatOnMF _
                                            And (x.CollectionCategory = EnumCollectionCategory.Cash _
                                                Or x.CollectionCategory = EnumCollectionCategory.Offset)
                                            Select x.AllocationAmount).Sum()
                    'Total Collection on Withholding VAT Default Cash or Offset
                    Dim SumWVDI As Decimal = (From x In GetCollectionList
                                              Where x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest _
                                              And (x.CollectionCategory = EnumCollectionCategory.Cash _
                                                  Or x.CollectionCategory = EnumCollectionCategory.Offset)
                                              Select x.AllocationAmount).Sum()


                    'New Ending Balance
                    Dim GetNewEndingBalanceMF As ARCollection = (From x In GetCollectionList
                                                                 Where (x.CollectionType = EnumCollectionType.MarketFees _
                                                                     Or x.CollectionType = EnumCollectionType.NoTransaction) _
                                                                 And x.OffsettingSequence = MaxOffsetSeq
                                                                 Select x Order By x.NewEndingBalance Descending).FirstOrDefault

                    Dim GetNewEndingBalanceMFV As ARCollection = (From x In GetCollectionList
                                                                  Where (x.CollectionType = EnumCollectionType.VatOnMarketFees _
                                                                      Or x.CollectionType = EnumCollectionType.NoTransaction) _
                                                                  And x.OffsettingSequence = MaxOffsetSeq
                                                                  Select x Order By x.NewEndingBalance Descending).FirstOrDefault

                    If GetEndingBalanceMF Is Nothing Then
                        MFArr(RowIndex, 0) = GetEndingBalanceMFV.BillingRemarks
                        MFArr(RowIndex, 1) = GetEndingBalanceMFV.WESMBillBatchNo
                        MFArr(RowCount, 2) = "Total Accounts Receivables"
                        MFArr(RowIndex, 2) = GetEndingBalanceMFV.IDNumber
                        MFArr(RowIndex, 3) = GetEndingBalanceMFV.ParticipantID
                        MFArr(RowIndex, 4) = GetEndingBalanceMFV.InvoiceNumber
                        MFArr(RowIndex, 5) = GetEndingBalanceMFV.DueDate
                        MFArr(RowIndex, 6) = GetEndingBalanceMFV.NewDueDate
                        MFArr(RowIndex, 7) = 0

                    Else
                        MFArr(RowIndex, 0) = GetEndingBalanceMF.BillingRemarks
                        MFArr(RowIndex, 1) = GetEndingBalanceMF.WESMBillBatchNo
                        MFArr(RowCount, 2) = "Total Accounts Receivables"
                        MFArr(RowIndex, 2) = GetEndingBalanceMF.IDNumber
                        MFArr(RowIndex, 3) = GetEndingBalanceMF.ParticipantID
                        MFArr(RowIndex, 4) = GetEndingBalanceMF.InvoiceNumber
                        MFArr(RowIndex, 5) = GetEndingBalanceMF.DueDate
                        MFArr(RowIndex, 6) = GetEndingBalanceMF.NewDueDate
                        MFArr(RowIndex, 7) = GetEndingBalanceMF.EndingBalance

                    End If

                    If GetEndingBalanceMFV Is Nothing Then
                        MFArr(RowIndex, 8) = 0
                        MFArr(RowIndex, 9) = GetEndingBalanceMF.EndingBalance
                    ElseIf GetEndingBalanceMF Is Nothing Then
                        MFArr(RowIndex, 8) = GetEndingBalanceMFV.EndingBalance
                        MFArr(RowIndex, 9) = GetEndingBalanceMFV.EndingBalance
                    Else
                        MFArr(RowIndex, 8) = GetEndingBalanceMFV.EndingBalance
                        MFArr(RowIndex, 9) = GetEndingBalanceMF.EndingBalance + GetEndingBalanceMFV.EndingBalance
                    End If

                    MFArr(RowIndex, 10) = Nothing
                    MFArr(RowIndex, 11) = IIf(SumCashColMFMFV = 0D, Nothing, SumCashColMFMFV)
                    MFArr(RowIndex, 12) = IIf(SumOffsetColMFMFV = 0D, Nothing, SumOffsetColMFMFV)
                    MFArr(RowIndex, 13) = IIf(SumDefault = 0D, Nothing, SumDefault)
                    MFArr(RowIndex, 14) = IIf(SumWT = 0D, Nothing, SumWT * -1)
                    MFArr(RowIndex, 15) = IIf(SumWTDI = 0D, Nothing, SumWTDI * -1)
                    MFArr(RowIndex, 16) = IIf(SumWV = 0D, Nothing, SumWV * -1)
                    MFArr(RowIndex, 17) = IIf(SumWVDI = 0D, Nothing, SumWVDI * -1)
                    If GetNewEndingBalanceMFV Is Nothing Then
                        MFArr(RowIndex, 18) = GetNewEndingBalanceMF.NewEndingBalance
                    ElseIf GetNewEndingBalanceMF Is Nothing Then
                        MFArr(RowIndex, 18) = GetNewEndingBalanceMFV.NewEndingBalance
                    Else
                        MFArr(RowIndex, 18) = GetNewEndingBalanceMF.NewEndingBalance + GetNewEndingBalanceMFV.NewEndingBalance
                    End If

                    RowIndex += 1
                Next
                ret = MFArr
        End Select
        Return ret
    End Function

    Private Function GenerateAllocationArr(ByVal AllocationList As List(Of APAllocation),
                                           ByVal iChargeType As EnumChargeType,
                                           ByVal DistinctSequence As List(Of Integer)) As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicINV As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim DistinctGroup = (From x In AllocationList Select x.InvoiceNumber, x.WESMBillBatchNo).Distinct.ToList
        Dim RowCount As Integer = DistinctGroup.Count()

        Select Case iChargeType
            Case EnumChargeType.E
                Dim EnergyArr As Object(,) = New Object(,) {}
                ReDim EnergyArr(RowCount, 15)
                For Each GroupItem In DistinctGroup
                    Dim GetAllocationList As List(Of APAllocation) = (From x In AllocationList
                                                                      Where x.InvoiceNumber = GroupItem.InvoiceNumber _
                                                                      And x.WESMBillBatchNo = GroupItem.WESMBillBatchNo
                                                                      Select x Order By x.PaymentCategory, x.PaymentType Descending).ToList()

                    Dim MinOffsetSeq As Integer = (From x In GetAllocationList
                                                   Where x.PaymentType = EnumPaymentNewType.Energy _
                                                   Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy _
                                                   Or x.PaymentType = EnumPaymentNewType.NoTransaction
                                                   Select x).DefaultIfEmpty.Min(Function(x) x.OffsettingSequence)

                    Dim MaxOffsetSeq As Integer = (From x In GetAllocationList
                                                   Where x.PaymentType = EnumPaymentNewType.Energy _
                                                   Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy _
                                                   Or x.PaymentType = EnumPaymentNewType.NoTransaction
                                                   Select x).DefaultIfEmpty.Max(Function(x) x.OffsettingSequence)

                    'Ending Balance before Payment
                    Dim GetEndingBalanceEnergy As APAllocation = (From x In GetAllocationList
                                                                  Where (x.PaymentType = EnumPaymentNewType.Energy _
                                                                      Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy _
                                                                      Or x.PaymentType = EnumPaymentNewType.NoTransaction) _
                                                                  And x.OffsettingSequence = MinOffsetSeq
                                                                  Select x).FirstOrDefault

                    'Total Energy Payment on Cash 
                    Dim SumCashColEnergy As Decimal = (From x In GetAllocationList
                                                       Where x.PaymentType = EnumPaymentNewType.Energy _
                                                       And x.PaymentCategory = EnumCollectionCategory.Cash
                                                       Select x.AllocationAmount).Sum()

                    'Total WithholdingTax Energy Collection on Cash 
                    Dim SumCashColEnergyWithholdingTax As Decimal = (From x In GetAllocationList
                                                                     Where x.PaymentType = EnumPaymentNewType.WithholdingTaxOnEnergy _
                                                                     And x.PaymentCategory = EnumCollectionCategory.Cash
                                                                     Select x.AllocationAmount).Sum()

                    'Total Energy Payment on Drawdown
                    Dim SumDrawColEnergy As Decimal = (From x In GetAllocationList
                                                       Where x.PaymentType = EnumPaymentNewType.Energy _
                                                       And x.PaymentCategory = EnumCollectionCategory.Drawdown
                                                       Select x.AllocationAmount).Sum()

                    'Total Energy Payment on Offsetting                    
                    Dim SumOffsetColEnergy As Decimal = (From x In GetAllocationList
                                                         Where x.PaymentType = EnumPaymentNewType.Energy _
                                                         And x.PaymentCategory = EnumCollectionCategory.Offset
                                                         Select x.AllocationAmount).Sum()

                    'Total Default Interest on Energy Payment Cash or Offset
                    Dim SumDefaultInterest As Decimal = (From x In GetAllocationList
                                                         Where x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy _
                                                         And (x.PaymentCategory = EnumCollectionCategory.Cash _
                                                              Or x.PaymentCategory = EnumCollectionCategory.Offset _
                                                             Or x.PaymentCategory = EnumCollectionCategory.Drawdown)
                                                         Select x.AllocationAmount).Sum()

                    'New Ending Balance
                    Dim GetNewEndingBalanceEnergy As List(Of APAllocation) = (From x In GetAllocationList
                                                                              Where (x.PaymentType = EnumPaymentNewType.Energy _
                                                                                  Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy _
                                                                                  Or x.PaymentType = EnumPaymentNewType.NoTransaction) _
                                                                              And x.OffsettingSequence = MaxOffsetSeq
                                                                              Select x Order By x.NewEndingBalance).ToList


                    Dim GetNewEndingBalanceEnergyOrig As APAllocation = (From x In GetAllocationList
                                                                         Where (x.PaymentType = EnumPaymentNewType.Energy _
                                                                        Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy _
                                                                        Or x.PaymentType = EnumPaymentNewType.NoTransaction) _
                                                                    And x.OffsettingSequence = MaxOffsetSeq
                                                                         Select x Order By x.PaymentType Descending, x.NewEndingBalance Descending).FirstOrDefault
                    Dim oNewEndingBalance As Decimal = 0D
                    If GetNewEndingBalanceEnergy.Count > 1 Then
                        oNewEndingBalance = GetNewEndingBalanceEnergyOrig.EndingBalance
                        For Each oitem In GetNewEndingBalanceEnergy
                            If oitem.PaymentType = EnumPaymentNewType.Energy Then
                                oNewEndingBalance -= oitem.AllocationAmount
                            End If
                        Next
                    Else
                        oNewEndingBalance = GetNewEndingBalanceEnergyOrig.NewEndingBalance
                    End If

                    EnergyArr(RowIndex, 0) = GetEndingBalanceEnergy.BillingRemarks
                    EnergyArr(RowIndex, 1) = GetEndingBalanceEnergy.WESMBillBatchNo
                    EnergyArr(RowIndex, 2) = GetEndingBalanceEnergy.IDNumber
                    EnergyArr(RowCount, 2) = "Total Accounts Payables"
                    EnergyArr(RowIndex, 3) = GetEndingBalanceEnergy.ParticipantID
                    EnergyArr(RowIndex, 4) = GetEndingBalanceEnergy.InvoiceNumber
                    EnergyArr(RowIndex, 5) = GetEndingBalanceEnergy.DueDate
                    EnergyArr(RowIndex, 6) = GetEndingBalanceEnergy.NewDueDate
                    EnergyArr(RowIndex, 7) = GetEndingBalanceEnergy.EnergyWithHold
                    EnergyArr(RowIndex, 8) = GetEndingBalanceEnergy.EndingBalance
                    EnergyArr(RowIndex, 9) = Nothing
                    EnergyArr(RowIndex, 10) = IIf(SumCashColEnergy = 0D, Nothing, SumCashColEnergy)
                    EnergyArr(RowIndex, 11) = IIf(SumCashColEnergyWithholdingTax = 0D, Nothing, SumCashColEnergyWithholdingTax)
                    EnergyArr(RowIndex, 12) = IIf(SumDrawColEnergy = 0D, Nothing, SumDrawColEnergy)
                    EnergyArr(RowIndex, 13) = IIf(SumOffsetColEnergy = 0D, Nothing, SumOffsetColEnergy)
                    EnergyArr(RowIndex, 14) = IIf(SumDefaultInterest = 0D, Nothing, SumDefaultInterest)
                    EnergyArr(RowIndex, 15) = oNewEndingBalance

                    RowIndex += 1
                Next

                ret = EnergyArr

            Case EnumChargeType.EV
                Dim VATArr As Object(,) = New Object(,) {}
                ReDim VATArr(RowCount, 11)
                For Each GroupItem In DistinctGroup

                    Dim GetAllocationList As List(Of APAllocation) = (From x In AllocationList
                                                                      Where x.InvoiceNumber = GroupItem.InvoiceNumber _
                                                                      And x.WESMBillBatchNo = GroupItem.WESMBillBatchNo
                                                                      Select x Order By x.PaymentCategory, x.PaymentType Descending).ToList()

                    Dim MinOffsetSeq As Integer = (From x In GetAllocationList
                                                   Where x.PaymentType = EnumPaymentNewType.VatOnEnergy _
                                                   Or x.PaymentType = EnumPaymentNewType.NoTransaction
                                                   Select x).DefaultIfEmpty.Min(Function(x) x.OffsettingSequence)

                    Dim MaxOffsetSeq As Integer = (From x In GetAllocationList
                                                   Where x.PaymentType = EnumPaymentNewType.VatOnEnergy _
                                                   Or x.PaymentType = EnumPaymentNewType.NoTransaction
                                                   Select x).DefaultIfEmpty.Max(Function(x) x.OffsettingSequence)

                    'Ending Balance before Payment
                    Dim GetEndingBalanceVAT As APAllocation = (From x In GetAllocationList
                                                               Where (x.PaymentType = EnumPaymentNewType.VatOnEnergy _
                                                                   Or x.PaymentType = EnumPaymentNewType.NoTransaction) _
                                                               And x.OffsettingSequence = MinOffsetSeq
                                                               Select x Order By x.EndingBalance).FirstOrDefault

                    'Total VAT Payment on Cash 
                    Dim SumCashColVAT As Decimal = (From x In GetAllocationList
                                                    Where x.PaymentType = EnumPaymentNewType.VatOnEnergy _
                                                    And x.PaymentCategory = EnumCollectionCategory.Cash
                                                    Select x.AllocationAmount).Sum()

                    'Total VAT Payment on Offsetting                    
                    Dim SumOffsetColVAT As Decimal = (From x In GetAllocationList
                                                      Where x.PaymentType = EnumPaymentNewType.VatOnEnergy _
                                                      And x.PaymentCategory = EnumCollectionCategory.Offset
                                                      Select x.AllocationAmount).Sum()



                    'New Ending Balance
                    Dim GetNewEndingBalanceVAT As APAllocation = (From x In GetAllocationList
                                                                  Where (x.PaymentType = EnumPaymentNewType.VatOnEnergy _
                                                                      Or x.PaymentType = EnumPaymentNewType.NoTransaction) _
                                                                  And x.OffsettingSequence = MaxOffsetSeq
                                                                  Select x Order By x.NewEndingBalance).FirstOrDefault
                    VATArr(RowIndex, 0) = GetEndingBalanceVAT.BillingRemarks
                    VATArr(RowIndex, 1) = GetEndingBalanceVAT.WESMBillBatchNo
                    VATArr(RowIndex, 2) = GetEndingBalanceVAT.IDNumber
                    VATArr(RowCount, 2) = "Total Accounts Payables"
                    VATArr(RowIndex, 3) = GetEndingBalanceVAT.ParticipantID
                    VATArr(RowIndex, 4) = GetEndingBalanceVAT.InvoiceNumber
                    VATArr(RowIndex, 5) = GetEndingBalanceVAT.DueDate
                    VATArr(RowIndex, 6) = GetEndingBalanceVAT.NewDueDate
                    VATArr(RowIndex, 7) = GetEndingBalanceVAT.EndingBalance
                    VATArr(RowIndex, 8) = Nothing
                    VATArr(RowIndex, 9) = IIf(SumCashColVAT = 0D, Nothing, SumCashColVAT)
                    VATArr(RowIndex, 10) = IIf(SumOffsetColVAT = 0D, Nothing, SumOffsetColVAT)
                    VATArr(RowIndex, 11) = GetNewEndingBalanceVAT.NewEndingBalance
                    RowIndex += 1
                Next
                ret = VATArr
            Case EnumChargeType.MF, EnumChargeType.MFV
                Dim MFArr As Object(,) = New Object(,) {}
                ReDim MFArr(RowCount, 18)
                For Each GroupItem In DistinctGroup
                    Dim GetAllocationList = (From x In AllocationList
                                             Where x.InvoiceNumber = GroupItem.InvoiceNumber _
                                             And x.WESMBillBatchNo = GroupItem.WESMBillBatchNo
                                             Select x Order By x.OffsettingSequence, x.PaymentCategory, x.PaymentType).ToList()

                    Dim MinOffsetSeq As Integer = (From x In GetAllocationList
                                                   Where (x.PaymentType = EnumPaymentNewType.MarketFees Or x.PaymentType = EnumPaymentNewType.VatOnMarketFees) _
                                                   Or x.PaymentType = EnumPaymentNewType.NoTransaction
                                                   Select x).DefaultIfEmpty.Min(Function(x) x.OffsettingSequence)

                    Dim MaxOffsetSeq As Integer = (From x In GetAllocationList
                                                   Where (x.PaymentType = EnumPaymentNewType.MarketFees Or x.PaymentType = EnumPaymentNewType.VatOnMarketFees) _
                                                   Or x.PaymentType = EnumPaymentNewType.NoTransaction
                                                   Select x).DefaultIfEmpty.Max(Function(x) x.OffsettingSequence)

                    'Ending Balance
                    Dim GetEndingBalanceMF As APAllocation = (From x In GetAllocationList
                                                              Where (x.PaymentType = EnumPaymentNewType.MarketFees _
                                                                  Or x.PaymentType = EnumPaymentNewType.NoTransaction) _
                                                              And x.OffsettingSequence = MinOffsetSeq
                                                              Select x Order By x.EndingBalance Descending).FirstOrDefault


                    Dim GetEndingBalanceMFV As APAllocation = (From x In GetAllocationList
                                                               Where (x.PaymentType = EnumPaymentNewType.VatOnMarketFees _
                                                                   Or x.PaymentType = EnumPaymentNewType.NoTransaction) _
                                                               And x.OffsettingSequence = MinOffsetSeq
                                                               Select x Order By x.EndingBalance Descending).FirstOrDefault

                    'Total Collection on Cash MF and MFV 
                    Dim SumCashColMFMFV As Decimal = (From x In GetAllocationList
                                                      Where (x.PaymentType = EnumPaymentNewType.MarketFees _
                                                             Or x.PaymentType = EnumPaymentNewType.VatOnMarketFees _
                                                             Or x.PaymentType = EnumPaymentNewType.WithholdingTaxOnMF _
                                                             Or x.PaymentType = EnumPaymentNewType.WithholdingVatOnMF) _
                                                         And x.PaymentCategory = EnumCollectionCategory.Cash
                                                      Select x.AllocationAmount).Sum()

                    'Total Collection on Offsetting MF and MFV
                    Dim SumOffsetColMFMFV As Decimal = (From x In GetAllocationList
                                                        Where (x.PaymentType = EnumPaymentNewType.MarketFees _
                                                               Or x.PaymentType = EnumPaymentNewType.VatOnMarketFees _
                                                               Or x.PaymentType = EnumPaymentNewType.WithholdingTaxOnMF _
                                                               Or x.PaymentType = EnumPaymentNewType.WithholdingVatOnMF) _
                                                           And x.PaymentCategory = EnumCollectionCategory.Offset
                                                        Select x.AllocationAmount).Sum()

                    'Total Collection on Default Interest Cash or Offset
                    Dim SumDefault As Decimal = (From x In GetAllocationList
                                                 Where (x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                                        Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF _
                                                        Or x.PaymentType = EnumPaymentNewType.WithholdingTaxOnDefaultInterest _
                                                        Or x.PaymentType = EnumPaymentNewType.WithholdingVatOnDefaultInterest) _
                                                 And (x.PaymentCategory = EnumCollectionCategory.Cash _
                                                      Or x.PaymentCategory = EnumCollectionCategory.Offset)
                                                 Select x.AllocationAmount).Sum()

                    'Total Collection on Withholding Tax Cash or Offset
                    Dim SumWT As Decimal = (From x In GetAllocationList
                                            Where x.PaymentType = EnumPaymentNewType.WithholdingTaxOnMF _
                                            And (x.PaymentCategory = EnumCollectionCategory.Cash _
                                                Or x.PaymentCategory = EnumCollectionCategory.Offset)
                                            Select x.AllocationAmount).Sum()
                    'Total Collection on Withholding Tax Default Cash or Offset
                    Dim SumWTDI As Decimal = (From x In GetAllocationList
                                              Where x.PaymentType = EnumPaymentNewType.WithholdingTaxOnDefaultInterest _
                                              And (x.PaymentCategory = EnumCollectionCategory.Cash _
                                                  Or x.PaymentCategory = EnumCollectionCategory.Offset)
                                              Select x.AllocationAmount).Sum()

                    'Total Collection on Withholding VAT Cash or Offset
                    Dim SumWV As Decimal = (From x In GetAllocationList
                                            Where x.PaymentType = EnumPaymentNewType.WithholdingVatOnMF _
                                            And (x.PaymentCategory = EnumCollectionCategory.Cash _
                                                Or x.PaymentCategory = EnumCollectionCategory.Offset)
                                            Select x.AllocationAmount).Sum()

                    'Total Collection on Withholding VAT Default Cash or Offset
                    Dim SumWVDI As Decimal = (From x In GetAllocationList
                                              Where x.PaymentType = EnumPaymentNewType.WithholdingVatOnDefaultInterest _
                                              And (x.PaymentCategory = EnumCollectionCategory.Cash _
                                                  Or x.PaymentCategory = EnumCollectionCategory.Offset)
                                              Select x.AllocationAmount).Sum()



                    'New Ending Balance
                    Dim GetNewEndingBalanceMF As APAllocation = (From x In GetAllocationList
                                                                 Where (x.PaymentType = EnumPaymentNewType.MarketFees _
                                                                     Or x.PaymentType = EnumPaymentNewType.NoTransaction) _
                                                                 And x.OffsettingSequence = MaxOffsetSeq
                                                                 Select x Order By x.NewEndingBalance).FirstOrDefault

                    Dim GetNewEndingBalanceMFV As APAllocation = (From x In GetAllocationList
                                                                  Where (x.PaymentType = EnumPaymentNewType.VatOnMarketFees _
                                                                       Or x.PaymentType = EnumPaymentNewType.NoTransaction) _
                                                                  And x.OffsettingSequence = MaxOffsetSeq
                                                                  Select x Order By x.NewEndingBalance).FirstOrDefault


                    If GetNewEndingBalanceMF Is Nothing Then
                        MFArr(RowIndex, 0) = GetNewEndingBalanceMFV.BillingRemarks
                        MFArr(RowIndex, 1) = GetNewEndingBalanceMFV.WESMBillBatchNo
                        MFArr(RowCount, 2) = "Total Accounts Payables"
                        MFArr(RowIndex, 2) = GetNewEndingBalanceMFV.IDNumber
                        MFArr(RowIndex, 3) = GetNewEndingBalanceMFV.ParticipantID
                        MFArr(RowIndex, 4) = GetNewEndingBalanceMFV.InvoiceNumber
                        MFArr(RowIndex, 5) = GetNewEndingBalanceMFV.DueDate
                        MFArr(RowIndex, 6) = GetNewEndingBalanceMFV.NewDueDate
                        MFArr(RowIndex, 7) = 0
                    Else
                        MFArr(RowIndex, 0) = GetEndingBalanceMF.BillingRemarks
                        MFArr(RowIndex, 1) = GetEndingBalanceMF.WESMBillBatchNo
                        MFArr(RowCount, 2) = "Total Accounts Payables"
                        MFArr(RowIndex, 2) = GetEndingBalanceMF.IDNumber
                        MFArr(RowIndex, 3) = GetEndingBalanceMF.ParticipantID
                        MFArr(RowIndex, 4) = GetEndingBalanceMF.InvoiceNumber
                        MFArr(RowIndex, 5) = GetEndingBalanceMF.DueDate
                        MFArr(RowIndex, 6) = GetEndingBalanceMF.NewDueDate
                        MFArr(RowIndex, 7) = GetEndingBalanceMF.EndingBalance
                    End If

                    If GetEndingBalanceMFV Is Nothing Then
                        MFArr(RowIndex, 8) = 0
                        MFArr(RowIndex, 9) = GetEndingBalanceMF.EndingBalance
                    ElseIf GetEndingBalanceMF Is Nothing Then
                        MFArr(RowIndex, 8) = GetEndingBalanceMFV.EndingBalance
                        MFArr(RowIndex, 9) = GetEndingBalanceMFV.EndingBalance
                    Else
                        MFArr(RowIndex, 8) = GetEndingBalanceMFV.EndingBalance
                        MFArr(RowIndex, 9) = GetEndingBalanceMF.EndingBalance + GetEndingBalanceMFV.EndingBalance
                    End If

                    MFArr(RowIndex, 10) = Nothing
                    MFArr(RowIndex, 11) = IIf(SumCashColMFMFV = 0D, Nothing, SumCashColMFMFV)
                    MFArr(RowIndex, 12) = IIf(SumOffsetColMFMFV = 0D, Nothing, SumOffsetColMFMFV)
                    MFArr(RowIndex, 13) = IIf(SumDefault = 0D, Nothing, SumDefault)
                    MFArr(RowIndex, 14) = IIf(SumWT = 0D, Nothing, SumWT * -1)
                    MFArr(RowIndex, 15) = IIf(SumWTDI = 0D, Nothing, SumWTDI * -1)
                    MFArr(RowIndex, 16) = IIf(SumWV = 0D, Nothing, SumWV * -1)
                    MFArr(RowIndex, 17) = IIf(SumWVDI = 0D, Nothing, SumWVDI * -1)
                    If GetNewEndingBalanceMFV Is Nothing Then
                        MFArr(RowIndex, 18) = GetNewEndingBalanceMF.NewEndingBalance
                    ElseIf GetEndingBalanceMF Is Nothing Then
                        MFArr(RowIndex, 18) = GetEndingBalanceMFV.NewEndingBalance
                    Else
                        MFArr(RowIndex, 18) = GetNewEndingBalanceMF.NewEndingBalance + GetNewEndingBalanceMFV.NewEndingBalance
                    End If

                    RowIndex += 1
                Next
                ret = MFArr
        End Select
        Return ret
    End Function
#End Region

#Region "Methods for Generation of Check"

#End Region

#Region "Methods For Generation of EFT Summary Report on MS-Excel"
    Public Sub CreateEFTSummaryReport()

        Me.EFTSummaryReportList.Clear()


        Dim RFPPaymentListForNonFit = (From x In Me.RequestForPayment.RFPDetails
                                       Where x.RFPDetailsType = EnumRFPDetailsType.Payment _
                                       And x.PaymentType = EnumParticipantPaymentType.EFT _
                                       And Not x.Participant.EndsWith(AMModule.FITParticipantCode)
                                       Select x).ToList()

        For Each Item In RFPPaymentListForNonFit
            Dim GetAMParticipantInfo As AMParticipants = (From x In Me.AMParticipants Where x.IDNumber = Item.Participant Select x).FirstOrDefault()

            Dim GetPaymentTransToPR As PaymentTransferToPR = (From x In Me.PaymentTransferToPRList
                                                              Where x.IDNumber = Item.Participant
                                                              Select x).FirstOrDefault
            Dim TotalAmountForRemittance As Decimal = GetPaymentTransToPR.TotalAmountForRemittance
            If TotalAmountForRemittance >= 1000 Then
                Using EFTNew As New EFT
                    With EFTNew
                        .AllocationDate = Me.PayAllocDate.CollAllocationDate
                        .Participant = GetAMParticipantInfo
                        .PaymentType = GetAMParticipantInfo.PaymentType
                        .CheckNumber = 0
                        .ExcessCollection = GetPaymentTransToPR.PaymentOnExcessCollection
                        .DeferredEnergy = GetPaymentTransToPR.PaymentOnDeferredonEnergy
                        .OffsetOnDeferredEnergy = GetPaymentTransToPR.OffsetOnOffsetDeferredonEnergy
                        .DeferredVAT = GetPaymentTransToPR.PaymentOnDeferredonVATonEnergy
                        .OffsetOnDeferredVAT = GetPaymentTransToPR.OffsetOnOffsetDeferredonVATonEnergy
                        .Energy = GetPaymentTransToPR.PaymentOnEnergy
                        .VAT = GetPaymentTransToPR.PaymentOnVATonEnergy
                        .MarketFees = GetPaymentTransToPR.PaymentOnMFWithVAT
                        .ReturnAmount = 0
                        .TransferPrudential = GetPaymentTransToPR.TransferToPrudential
                        .TransferFinPen = GetPaymentTransToPR.TransferToFinPen
                        .TotalPayment = TotalAmountForRemittance
                        .NSSInterest = 0
                        .STLInterest = 0
                        .UpdatedBy = AMModule.UserName
                        .UpdatedDate = SystemDate
                    End With
                    EFTSummaryReportList.Add(EFTNew)
                End Using
            End If
        Next

        'GetInfo of Transco in AMParticipants
        Dim GetTranscoInfo As AMParticipants = (From x In Me.AMParticipants Where x.IDNumber = AMModule.ParentFitID Select x).FirstOrDefault
        Dim GetPaymentTransToPRForFit As List(Of PaymentTransferToPR) = (From x In Me.PaymentTransferToPRList
                                                                         Where x.IDNumber.EndsWith(AMModule.FITParticipantCode) _
                                                                         And x.TotalAmountForRemittance >= DeferredPayment
                                                                         Select x).ToList()

        Dim RFPPaymentListForFit = (From x In Me.RequestForPayment.RFPDetails
                                    Where x.RFPDetailsType = EnumRFPDetailsType.Payment _
                                    And x.PaymentType = EnumParticipantPaymentType.EFT _
                                    And x.Participant.EndsWith(AMModule.FITParticipantCode.ToString)
                                    Select x).ToList()

        Dim TotalAmountForFITRemittance As Decimal = GetPaymentTransToPRForFit.Where(Function(item) item.TotalAmountForRemittance >= 1000).Sum(Function(item) item.TotalAmountForRemittance)

        If TotalAmountForFITRemittance <> 0 Then
            Using EFTNew As New EFT
                With EFTNew
                    .AllocationDate = Me.PayAllocDate.CollAllocationDate
                    .Participant = GetTranscoInfo
                    .PaymentType = GetTranscoInfo.PaymentType
                    .CheckNumber = 0
                    .ExcessCollection = GetPaymentTransToPRForFit.Sum(Function(item) item.PaymentOnExcessCollection)
                    .DeferredEnergy = GetPaymentTransToPRForFit.Sum(Function(item) item.PaymentOnDeferredonEnergy)
                    .OffsetOnDeferredEnergy = GetPaymentTransToPRForFit.Sum(Function(item) item.OffsetOnOffsetDeferredonEnergy)
                    .DeferredVAT = GetPaymentTransToPRForFit.Sum(Function(item) item.PaymentOnDeferredonVATonEnergy)
                    .OffsetOnDeferredVAT = GetPaymentTransToPRForFit.Sum(Function(item) item.OffsetOnOffsetDeferredonVATonEnergy)
                    .Energy = GetPaymentTransToPRForFit.Sum(Function(item) item.PaymentOnEnergy)
                    .VAT = GetPaymentTransToPRForFit.Sum(Function(item) item.PaymentOnVATonEnergy)
                    .MarketFees = GetPaymentTransToPRForFit.Sum(Function(item) item.PaymentOnMFWithVAT)
                    .ReturnAmount = 0
                    .TransferPrudential = GetPaymentTransToPRForFit.Sum(Function(item) item.TransferToPrudential)
                    .TransferFinPen = GetPaymentTransToPRForFit.Sum(Function(item) item.TransferToFinPen)
                    .TotalPayment = TotalAmountForFITRemittance
                    .NSSInterest = 0
                    .STLInterest = 0
                    .UpdatedBy = AMModule.UserName
                    .UpdatedDate = SystemDate
                End With
                EFTSummaryReportList.Add(EFTNew)
            End Using
        End If
    End Sub

    Public Sub CreateEFTSummaryReport(ByVal SavingPathName As String)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet1 As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlEFTSummaryReport As Excel.Range

        Dim misValue As Object = System.Reflection.Missing.Value
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet1 = CType(xlWorkBook.Sheets(1), Excel.Worksheet)

        Dim EFTSummaryReportArr As Object(,) = New Object(,) {}
        ReDim Preserve EFTSummaryReportArr(Me.EFTSummaryReportList.Count, 9)

        'Column Headers
        EFTSummaryReportArr(0, 0) = "Record Number"
        EFTSummaryReportArr(0, 1) = "Payee/Beneficiary Name" & vbNewLine & "(max 35 characters)"
        EFTSummaryReportArr(0, 2) = "Payee/Beneficiary Account No."
        EFTSummaryReportArr(0, 3) = "Beneficiary Bank Clearing Code"
        EFTSummaryReportArr(0, 4) = "Invoice No."
        EFTSummaryReportArr(0, 5) = "Invoice Date"
        EFTSummaryReportArr(0, 6) = "Payment Date"
        EFTSummaryReportArr(0, 7) = "Amount"
        EFTSummaryReportArr(0, 8) = "Payment Details 1" & vbNewLine & "(max 35 characters)"
        EFTSummaryReportArr(0, 9) = "Payment Details IV" & vbNewLine & "(max 35 characters)"

        Dim Row As Integer = 1
        For Each Item In Me.EFTSummaryReportList
            EFTSummaryReportArr(Row, 0) = Item.Participant.IDNumber
            EFTSummaryReportArr(Row, 1) = Item.Participant.FullName
            EFTSummaryReportArr(Row, 2) = Item.Participant.BankAccountNo
            EFTSummaryReportArr(Row, 3) = ""
            EFTSummaryReportArr(Row, 4) = ""
            EFTSummaryReportArr(Row, 5) = ""
            EFTSummaryReportArr(Row, 6) = Item.AllocationDate
            EFTSummaryReportArr(Row, 7) = Item.TotalPayment
            EFTSummaryReportArr(Row, 8) = Item.Participant.ParticipantAddress
            EFTSummaryReportArr(Row, 9) = Item.Participant.City
            Row += 1
        Next

        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(Row, 10), Excel.Range)
        xlEFTSummaryReport = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlEFTSummaryReport.Value = EFTSummaryReportArr
        'xlDMCMSummary.Range(Cell1:="D5", Cell2:="G" & rowIndex + 4).Style = "Comma"

        Dim FileName As String = "EFTSummaryReport " & Me._PayAllocDate.CollAllocationDate.ToString("dd-MMM-yyyy")

        xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlCSV, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        xlWorkBook.Close(False)
        xlApp.Quit()

        releaseObject(xlEFTSummaryReport)
        releaseObject(xlRowRange2)
        releaseObject(xlRowRange1)
        releaseObject(xlWorkSheet1)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)
    End Sub
#End Region

#Region "Function For Generation of Journal Voucher From Collection"

    Public Function GenerateJVReport(ByVal PostedType As EnumPostedType) As DataSet
        Dim ret As New DataSet
        Dim Accountingcode = WBillHelper.GetAccountingCodes()
        Dim JV_Signatories = WBillHelper.GetSignatories("JV").First
        Dim AllGPPosted = WBillHelper.GetWESMBillGPPosted()
        Dim JVData = (From x In Me.JournalVoucherList Where x.PostedType = PostedType.ToString Select x).FirstOrDefault

        If JVData Is Nothing Then
            Return ret
        End If
        Dim JournalVoucherTable As New DataTable

        JournalVoucherTable.TableName = "JournalVoucher"
        With JournalVoucherTable.Columns
            .Add("JV_NO", GetType(String))
            .Add("JV_DATE", GetType(Date))
            .Add("BATCHCODE", GetType(String))
            .Add("OFFSETNUMBER", GetType(String))
            .Add("STATUS", GetType(Integer))
            .Add("PREPAREDBY", GetType(String))
            .Add("CHECKEDBY", GetType(String))
            .Add("APPROVEDBY", GetType(String))
            .Add("UPDATEDBY", GetType(String))
            .Add("UPDATEDDATE", GetType(Date))
            .Add("REMARKS", GetType(String))
            .Add("GPREF_NO", GetType(String))
            .Add("BIRPermit", GetType(String))
        End With

        Dim dr As DataRow
        dr = JournalVoucherTable.NewRow
        With JVData
            dr("JV_NO") = BFactory.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
            dr("JV_DATE") = .JVDate
            dr("BATCHCODE") = .BatchCode.ToString
            'dr("OFFSETNUMBER") = .OffsetNumber.ToString
            dr("STATUS") = .Status
            dr("PREPAREDBY") = .PreparedBy
            dr("CHECKEDBY") = JV_Signatories.Signatory_1
            dr("APPROVEDBY") = JV_Signatories.Signatory_2
            dr("UPDATEDBY") = AMModule.UserName
            dr("UPDATEDDATE") = AMModule.SystemDate
            dr("GPREF_NO") = .GPRefNo
            dr("REMARKS") = .Remarks
        End With
        JournalVoucherTable.Rows.Add(dr)

        Dim JournalVoucherDetailsTable As New DataTable
        JournalVoucherDetailsTable.TableName = "JournalVoucherDetails"
        With JournalVoucherDetailsTable.Columns
            .Add("JV_NO", GetType(String))
            .Add("ACCOUNTCODE", GetType(String))
            .Add("CREDIT", GetType(Decimal))
            .Add("DEBIT", GetType(Decimal))
            .Add("UPDATEDBY", GetType(String))
            .Add("UPDATEDDATE", GetType(Date))
        End With

        For Each rowJVDetails In JVData.JVDetails
            Dim jvRow As DataRow
            jvRow = JournalVoucherDetailsTable.NewRow
            With rowJVDetails
                jvRow("JV_NO") = Me.BFactory.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
                jvRow("ACCOUNTCODE") = .AccountCode
                jvRow("CREDIT") = .Credit
                jvRow("DEBIT") = .Debit
                jvRow("UPDATEDBY") = .UpdatedBy
                jvRow("UPDATEDDATE") = .UpdatedDate
            End With
            JournalVoucherDetailsTable.Rows.Add(jvRow)
        Next

        Dim AccountCodesTable As New DataTable
        AccountCodesTable.TableName = "AccountingCode"
        With AccountCodesTable.Columns
            .Add("ACCT_CODE", GetType(String))
            .Add("DESCRIPTION", GetType(String))
        End With

        For Each acctCodes In Accountingcode
            Dim acRow As DataRow
            acRow = AccountCodesTable.NewRow
            With acRow
                acRow("ACCT_CODE") = acctCodes.AccountCode
                acRow("DESCRIPTION") = acctCodes.Description
            End With
            AccountCodesTable.Rows.Add(acRow)
        Next

        ret.Tables.Add(JournalVoucherTable)
        ret.Tables.Add(JournalVoucherDetailsTable)
        ret.Tables.Add(AccountCodesTable)

        Return ret
    End Function
#End Region

#Region "Methods and Function for Financial Penalty"
    Dim FinPenORNumber As Long = 0
    Public Function GenerateFinPenOR(ByVal ItemOfTransToPR As PaymentTransferToPR) As OfficialReceiptMain
        Dim result As New OfficialReceiptMain

        Dim listORSummary As New List(Of OfficialReceiptSummary)
        Dim listORDetails As New List(Of OfficialReceiptDetails)
        Dim totalAR As Decimal = 0, totalPEMC As Decimal = 0
        Dim VatExempt As Decimal = 0, Vatable As Decimal = 0, VAT As Decimal = 0
        Dim VatZeroRated As Decimal = 0, Others As Decimal = 0, WithholdTax As Decimal = 0, WithholdVat As Decimal = 0
        Dim TotalTransAmountOnFinPen As Decimal = ItemOfTransToPR.TransferToFinPen

        Others = ItemOfTransToPR.TransferToFinPen
        FinPenORNumber += 1

        'For Offsetting Cash
        listORDetails.Add(New OfficialReceiptDetails(_ORNumber, AMModule.ClearingAccountCode,
                                                     "Clearing", TotalTransAmountOnFinPen, 0))

        'For Clearing AR-WESM
        listORDetails.Add(New OfficialReceiptDetails(_ORNumber, AMModule.CreditCode,
                                                     "AR-NonWESM", 0, TotalTransAmountOnFinPen))

        Dim GetJVEFTBatchCode As String = (From x In Me.JournalVoucherList
                                           Where x.PostedType = EnumPostedType.FPA.ToString
                                           Select x.BatchCode).FirstOrDefault
        If GetJVEFTBatchCode Is Nothing Then
            GetJVEFTBatchCode = ""
        End If

        Dim CreatedOR As OfficialReceiptMain = New OfficialReceiptMain(_ORNumber, Me.PayAllocDate.CollAllocationDate, ItemOfTransToPR.IDNumber, TotalTransAmountOnFinPen,
                                                                       1, listORDetails, listORSummary, GetJVEFTBatchCode, "", EnumORTransactionType.FinPenAmount, VatExempt, Vatable,
                                                                      VAT, VatZeroRated, Others, WithholdTax, WithholdVat)
        result = CreatedOR

        Return result
    End Function


    Public Sub UpdateTransferToFinPen(ByVal ItemOfTransToPR As PaymentTransferToPR)
        FinPenORNumber += _ORNumber
        Dim UdpateTransToFinPen = (From x In Me.PaymentTransferToPRList
                                   Where x.IDNumber = ItemOfTransToPR.IDNumber _
                                   And x.ParticipantID = ItemOfTransToPR.ParticipantID
                                   Select x).FirstOrDefault
        With UdpateTransToFinPen
            .FullyTransferToFinPen = ItemOfTransToPR.FullyTransferToFinPen
            .TransferToFinPen = ItemOfTransToPR.TransferToFinPen
            .TotalAmountForRemittance = ItemOfTransToPR.TotalAmountForRemittance
        End With

        Me.UpdateFTFForFinPen()
        Me.UpdateRFP(ItemOfTransToPR, EnumFTFTransType.TransferPEMCAccount)
        Me.CreateEFTSummaryReport()
        Me.CreateDeferredPayments()

        Dim GetJVEFTCheck = (From x In Me.JournalVoucherList Where x.PostedType = EnumPostedType.PEFT.ToString Select x).First

        If Not GetJVEFTCheck Is Nothing Then
            Me.PaymentProformaEntries.UpdateJVFromPaymentEFTCheck(GetJVEFTCheck,
                                                                  Me.RequestForPayment,
                                                                  Me.FundTransferform,
                                                                  Me.CollFundTransferform,
                                                                  Me.PrevDeferredPaymentList,
                                                                  Me.CurrentDeferredPaymentList)
        End If
    End Sub

    Public Function GenerateORFinPenReport(ByVal DT As DataTable, ByVal IDNumber As String) As DataTable
        Dim itemORReportMain As New OfficialReceiptReportMain

        Dim CheckIfHaveOr = (From x In Me.ORofFinPen
                             Where x.IDNumber = IDNumber
                             Select x).FirstOrDefault

        If Not CheckIfHaveOr Is Nothing Then
            With itemORReportMain

                .ItemOfficialReceipt = (From x In Me.ORofFinPen
                                        Where x.IDNumber = IDNumber
                                        Select x).FirstOrDefault

                .ItemParticipant = (From x In Me.AMParticipants Where x.IDNumber = IDNumber).FirstOrDefault
                .BIRPermitNumber = AMModule.BIRPermitNumber
                Dim TotalPayment As Decimal = (From x In .ListOfficialReceiptReportRawDetails Select x.Amount).Sum()
                .TotalPaymentInWords = BFactory.NumberConvert(TotalPayment)
            End With

            Dim ORPEMC_Header As Integer = EnumHeaderType.Yes

            DT = BFactory.GenerateOfficialReceiptReport(ORPEMC_Header, itemORReportMain, itemORReportMain.ItemParticipant, DT)
        End If
        Return DT
    End Function
#End Region

#Region "Methods For Creating Payment Transfer To Prudential Requirement"
    Public Sub UpdateTransferToPR(ByVal ItemOfTransToPR As PaymentTransferToPR)

        Dim UdpateTransToPR = (From x In Me.PaymentTransferToPRList
                               Where x.IDNumber = ItemOfTransToPR.IDNumber _
                               And x.ParticipantID = ItemOfTransToPR.ParticipantID
                               Select x).FirstOrDefault
        With UdpateTransToPR
            .FullyTransferToPR = ItemOfTransToPR.FullyTransferToPR
            .TransferToPrudential = ItemOfTransToPR.TransferToPrudential
            .TotalAmountForRemittance = ItemOfTransToPR.TotalAmountForRemittance
        End With

        Me.UpdateFTFForReplenishment()
        Me.UpdatePrudentialHistory()
        Me.UpdateRFP(ItemOfTransToPR, EnumFTFTransType.Replenishment)
        Me.CreateEFTSummaryReport()
        Me.CreateDeferredPayments()

        Dim GetJVEFTCheck = (From x In Me.JournalVoucherList Where x.PostedType = EnumPostedType.PEFT.ToString Select x).First

        If Not GetJVEFTCheck Is Nothing Then
            Me.PaymentProformaEntries.UpdateJVFromPaymentEFTCheck(GetJVEFTCheck,
                                                                  Me.RequestForPayment,
                                                                  Me.FundTransferform,
                                                                  Me.CollFundTransferform,
                                                                  Me.PrevDeferredPaymentList,
                                                                  Me.CurrentDeferredPaymentList)
        End If
    End Sub
    'For Testing only
    Public Shared Function ConvertToDataTable(Of t)(
                                                      ByVal list As IList(Of t)
                                                   ) As DataTable
        Dim table As New DataTable()
        If Not list.Any Then
            'don't know schema ....
            Return table
        End If
        Dim fields() = list.First.GetType.GetProperties
        For Each field In fields
            table.Columns.Add(field.Name, field.PropertyType)
        Next
        For Each item In list
            Dim row As DataRow = table.NewRow()
            For Each field In fields
                Dim p = item.GetType.GetProperty(field.Name)
                row(field.Name) = p.GetValue(item, Nothing)
            Next
            table.Rows.Add(row)
        Next
        Return table
    End Function

    Private Sub UpdatePrevDeferredDueToOffset()
        For Each item In Me.PrevDeferredPaymentList
            Select Case item.ChargeType
                Case EnumChargeType.E
                    Dim getDeductionfromEnergyShare As PaymentShare = (From x In Me.EnergyShare
                                                                       Where x.IDNumber = item.IDNumber And x.PaymentType = EnumPaymentNewType.DeferredAppliedEnergy
                                                                       Select x).FirstOrDefault
                    If Not getDeductionfromEnergyShare Is Nothing Then
                        item.OutstandingBalanceDeferredPayment = getDeductionfromEnergyShare.AmountBalance
                    End If
                Case EnumChargeType.EV
                    Dim getDeductionfromVATShare As PaymentShare = (From x In Me.EnergyShare
                                                                    Where x.IDNumber = item.IDNumber And x.PaymentType = EnumPaymentNewType.DeferredAppliedVAT
                                                                    Select x).FirstOrDefault
                    If Not getDeductionfromVATShare Is Nothing Then
                        item.OutstandingBalanceDeferredPayment = getDeductionfromVATShare.AmountBalance
                    End If
            End Select
        Next
    End Sub

    Public Sub CreatePaymentTransferToPR()
        Dim ListOfMPwithShare = (From x In Me.MFwithVATShare Select x.IDNumber).Union _
                                (From x In Me.EnergyShare Select x.IDNumber).Union _
                                (From x In Me.VATonEnergyShare Select x.IDNumber).Union _
                                (From x In Me.CollectionMonitoring Select x.IDNumber.IDNumber).Union _
                                (From x In Me.PrevDeferredPaymentList Select x.IDNumber).ToList()

        Dim DistinctListofMPwithShare = (From x In ListOfMPwithShare Select x Order By x).Distinct.ToList()
        Dim getListofPrudnetial As List(Of Prudential) = WBillHelper.GetParticipantsPrudential()

        Dim getWESMBillsSummaryForEnergy As List(Of WESMBillSummary) = Me.WESMBillSummaryList.Where(Function(x) (x.EndingBalance - x.EnergyWithhold) < 0 _
                                                                                                    And x.ChargeType = EnumChargeType.E _
                                                                                                    And x.BalanceType = EnumBalanceType.AR _
                                                                                                    And x.INVDMCMNo.StartsWith("TS-W") _
                                                                                                    And Not x.INVDMCMNo.ToUpper Like "*-ADJ*").ToList()
        For Each MP In DistinctListofMPwithShare
            Dim getOffsetOnDeferredEnergy As Decimal = 0D
            Dim getOffsetOnDeferredVAT As Decimal = 0D
            Dim AMParticipantInfo = (From x In Me.AMParticipants Where x.IDNumber = MP.ToString Select x).FirstOrDefault
            Dim getDrawDownCollection As Decimal = (From x In Me._EnergyListCollection
                                                    Where x.CollectionCategory = EnumCollectionCategory.Drawdown _
                                                    And x.IDNumber = MP.ToString()
                                                    Select x.AllocationAmount).Sum()
            Dim getMPAROutstandingBalances = getWESMBillsSummaryForEnergy.Where(Function(x) x.IDNumber.IDNumber.Equals(MP)).ToList()
            Dim getMPPrudential As Prudential = (From x In getListofPrudnetial Where x.IDNumber = MP Select x).FirstOrDefault
            If AMParticipantInfo Is Nothing Then
                Continue For
            End If
            Dim MPMFShareBalance = (From x In Me.MFwithVATShare Where x.IDNumber = MP.ToString Select x.AmountBalance).Sum

            Dim MPEnergyShareBalance As Decimal = (From x In Me.EnergyShare Where x.IDNumber = MP.ToString And x.PaymentType <> EnumPaymentNewType.DeferredAppliedEnergy Select x.AmountBalance).Sum
            Dim MPVATonEnergyShareBalance = (From x In Me.VATonEnergyShare Where x.IDNumber = MP.ToString And x.PaymentType <> EnumPaymentNewType.DeferredAppliedVAT Select x.AmountBalance).Sum
            Dim MPExcessCollection = (From x In Me.CollectionMonitoring
                                      Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                                      And x.IDNumber.IDNumber = MP.ToString
                                      Select x.Amount).Sum()

            'Remove due to update for Offsetting of Energy Deferred
            'Dim MPDeferredEnergy = (From x In Me.PrevDeferredPaymentList _
            '                        Where x.ChargeType = EnumChargeType.E _
            '                        And x.IDNumber = MP.ToString _
            '                        Select x.OutstandingBalanceDeferredPayment).Sum() _

            'Additional code for Offsetting on deferred Energy as of 06052018 - added by LAVV 
            'validate 06/10/2018
            Dim MPDeferredEnergyList = (From x In Me.EnergyShare
                                        Where x.IDNumber = MP.ToString _
                                        And x.PaymentType = EnumPaymentNewType.DeferredAppliedEnergy
                                        Select x).ToList
            For Each itemDefE In MPDeferredEnergyList
                For Each itemOffsetDefE In itemDefE.AmountOffset
                    getOffsetOnDeferredEnergy += Math.Abs(itemOffsetDefE.OffsetAmount)
                Next
            Next

            'Additional code for Offsetting on deferred VAT on Energy as of 06052018 - added by LAVV
            Dim MPDeferredVATonEnergyList = (From x In Me.VATonEnergyShare
                                             Where x.IDNumber = MP.ToString _
                                             And x.PaymentType = EnumPaymentNewType.DeferredAppliedVAT
                                             Select x).ToList

            For Each itemDefEV In MPDeferredVATonEnergyList
                For Each itemOffsetDefEV In itemDefEV.AmountOffset
                    getOffsetOnDeferredVAT += Math.Abs(itemOffsetDefEV.OffsetAmount)
                Next
            Next

            Dim MPDeferredEnergy = (From x In Me.EnergyShare
                                    Where x.IDNumber = MP.ToString _
                                    And x.PaymentType = EnumPaymentNewType.DeferredAppliedEnergy
                                    Select x.AmountShare).Sum()

            Dim MPDeferredEnergyBalance = (From x In Me.EnergyShare
                                           Where x.IDNumber = MP.ToString _
                                           And x.PaymentType = EnumPaymentNewType.DeferredAppliedEnergy
                                           Select x.AmountBalance).Sum()

            'Remove due to update for Offsetting of VAT Deferred
            'Dim MPDeferredVATonEnergy = (From x In Me.PrevDeferredPaymentList _
            '                            Where x.ChargeType = EnumChargeType.EV _
            '                            And x.IDNumber = MP.ToString _
            '                            Select x.OutstandingBalanceDeferredPayment).Sum()

            Dim MPDeferredVATonEnergy = (From x In Me.VATonEnergyShare
                                         Where x.IDNumber = MP.ToString _
                                         And x.PaymentType = EnumPaymentNewType.DeferredAppliedVAT
                                         Select x.AmountShare).Sum()

            Dim MMPDeferredVATonEnergyBalance = (From x In Me.VATonEnergyShare
                                                 Where x.IDNumber = MP.ToString _
                                                 And x.PaymentType = EnumPaymentNewType.DeferredAppliedVAT
                                                 Select x.AmountBalance).Sum()

            Using _PaymentTransferToPR As New PaymentTransferToPR
                Dim TotalPaymentAlloc As Decimal = MPExcessCollection +
                                                    MPDeferredEnergy +
                                                    MPDeferredVATonEnergy +
                                                    MPMFShareBalance +
                                                    MPEnergyShareBalance +
                                                    MPVATonEnergyShareBalance +
                                                    (getOffsetOnDeferredEnergy * -1) +
                                                    (getOffsetOnDeferredVAT * -1)

                With _PaymentTransferToPR
                    .IDNumber = AMParticipantInfo.IDNumber
                    .ParticipantID = AMParticipantInfo.ParticipantID
                    .PaymentOnExcessCollection = MPExcessCollection
                    .PaymentOnDeferredonEnergy = MPDeferredEnergy
                    .PaymentOnDeferredonVATonEnergy = MPDeferredVATonEnergy
                    .OffsetOnOffsetDeferredonEnergy = getOffsetOnDeferredEnergy * -1
                    .OffsetOnOffsetDeferredonVATonEnergy = getOffsetOnDeferredVAT * -1
                    .PaymentOnMFWithVAT = MPMFShareBalance
                    .PaymentOnEnergy = MPEnergyShareBalance
                    .PaymentOnVATonEnergy = MPVATonEnergyShareBalance
                    If getDrawDownCollection = 0 Then
                        .FullyTransferToPR = False
                        .TransferToPrudential = 0
                    Else
                        'Added by LAVV 09/05/2023 to check if the current MP have AR outstanding balances if true no auto replenishment shall be allocated
                        If getMPAROutstandingBalances.Count = 0 Then
                            'Added by LAVV 03/26/2022 to ensure that no PR shall be False if the ID_Number does not exist to the Prudential Monitoring Table 
                            If getMPPrudential Is Nothing Then
                                .FullyTransferToPR = False
                                .TransferToPrudential = 0
                            ElseIf getMPPrudential.IDNumber.Contains("_FIT") Then
                                .FullyTransferToPR = False
                                .TransferToPrudential = 0
                            Else
                                If MPEnergyShareBalance > Math.Abs(getDrawDownCollection) And MPEnergyShareBalance <> 0 Then
                                    .FullyTransferToPR = False
                                    .TransferToPrudential = Math.Abs(getDrawDownCollection)
                                ElseIf MPEnergyShareBalance <= Math.Abs(getDrawDownCollection) And MPEnergyShareBalance <> 0 Then
                                    .FullyTransferToPR = True
                                    .TransferToPrudential = MPEnergyShareBalance
                                Else
                                    .FullyTransferToPR = False
                                    .TransferToPrudential = 0
                                End If
                            End If
                        End If
                    End If
                    .FullyTransferToFinPen = False
                    .TransferToFinPen = 0
                    .TotalPaymentAllocated = TotalPaymentAlloc '- .TransferToPrudential
                    .TotalAmountForRemittance = TotalPaymentAlloc - .TransferToPrudential
                End With

                Me.PaymentTransferToPRList.Add(_PaymentTransferToPR)
            End Using
        Next

        Me.UpdateFTFForReplenishment()
        Me.UpdatePrudentialHistory()
        Me.CreateRFP()
        Me.CreateEFTSummaryReport()
        Me.CreateDeferredPayments()
    End Sub
#End Region

#Region "Function For Generating Payment Transfer To Prudential Requirements DataTable"
    Public Function PaymentTrasferToPRDT() As DataTable
        Dim PaymentShare As New DataTable
        PaymentShare.TableName = "PaymentTrasferToPR"
        With PaymentShare.Columns
            .Add("IDNumber", GetType(String))
            .Add("ParticipantID", GetType(String))
            .Add("ExcessCollection", GetType(String))
            .Add("DeferredOnEnergy", GetType(String))
            .Add("DeferredOnVATonEnergy", GetType(String))
            .Add("OffsetDeferredOnEnergy", GetType(String))
            .Add("OffsetDeferredOnVATonEnergy", GetType(String))
            .Add("PaymentOnMarketFees", GetType(String))
            .Add("PaymentOnEnergy", GetType(String))
            .Add("PaymentOnVATonEnergy", GetType(String))
            .Add("TotalPaymentAllocated", GetType(String))
            .Add("FullyTransferToPR", GetType(Boolean))
            .Add("TransferToPR", GetType(String))
            .Add("FullyTransferToFinPen", GetType(Boolean))
            .Add("TransferToFinPen", GetType(String))
            .Add("TotalAmountForRemittance", GetType(String))
        End With

        For Each MP In Me.PaymentTransferToPRList.OrderBy(Function(x) x.ParticipantID)
            Dim PSRow As DataRow
            PSRow = PaymentShare.NewRow
            PSRow("IDNumber") = MP.IDNumber
            PSRow("ParticipantID") = MP.ParticipantID
            PSRow("ExcessCollection") = FormatNumber(MP.PaymentOnExcessCollection, UseParensForNegativeNumbers:=TriState.True)
            PSRow("DeferredOnEnergy") = FormatNumber(MP.PaymentOnDeferredonEnergy, UseParensForNegativeNumbers:=TriState.True)
            PSRow("DeferredOnVATonEnergy") = FormatNumber(MP.PaymentOnDeferredonVATonEnergy, UseParensForNegativeNumbers:=TriState.True)
            PSRow("OffsetDeferredOnEnergy") = FormatNumber(MP.OffsetOnOffsetDeferredonEnergy, UseParensForNegativeNumbers:=TriState.True)
            PSRow("OffsetDeferredOnVATonEnergy") = FormatNumber(MP.OffsetOnOffsetDeferredonVATonEnergy, UseParensForNegativeNumbers:=TriState.True)
            PSRow("PaymentOnMarketFees") = FormatNumber(MP.PaymentOnMFWithVAT, UseParensForNegativeNumbers:=TriState.True)
            PSRow("PaymentOnEnergy") = FormatNumber(MP.PaymentOnEnergy, UseParensForNegativeNumbers:=TriState.True)
            PSRow("PaymentOnVATonEnergy") = FormatNumber(MP.PaymentOnVATonEnergy, UseParensForNegativeNumbers:=TriState.True)
            PSRow("TotalPaymentAllocated") = FormatNumber(MP.TotalPaymentAllocated, UseParensForNegativeNumbers:=TriState.True)
            PSRow("FullyTransferToPR") = MP.FullyTransferToPR
            PSRow("TransferToPR") = FormatNumber(MP.TransferToPrudential, UseParensForNegativeNumbers:=TriState.True)
            PSRow("FullyTransferToFinPen") = MP.FullyTransferToFinPen
            PSRow("TransferToFinPen") = FormatNumber(MP.TransferToFinPen, UseParensForNegativeNumbers:=TriState.True)
            PSRow("TotalAmountForRemittance") = FormatNumber(MP.TotalAmountForRemittance, UseParensForNegativeNumbers:=TriState.True)
            PaymentShare.Rows.Add(PSRow)
        Next
        Return PaymentShare
    End Function
#End Region

#Region "Function and Methods For Fund Transfer Form"
    Public Function GetLastRefNoUsed() As Long
        Dim ret As Long = 0
        Dim LastRefNoUsed As New FundTransferFormMain
        If Me.FundTransferform.Count = 0 Then
            ret = 1
        Else
            LastRefNoUsed = (From x In Me.FundTransferform Order By x.RefNo Descending Select x).FirstOrDefault
            If Not IsDBNull(LastRefNoUsed.RefNo) Then
                ret = LastRefNoUsed.RefNo
            End If
        End If
        Return ret
    End Function

    Public Sub CreateFTFForReplenishment()
        Dim RefNo As Long = 0
        Dim SumReplenishmentAmount = (From x In Me.PaymentTransferToPRList
                                      Select x.TransferToPrudential).Sum
        Dim Signatory = WBillHelper.GetSignatories("FTF").First()
        Dim TotalPRAmount As Decimal = 0
        If SumReplenishmentAmount > 0 Then
            Using _FTFMain As New FundTransferFormMain
                With _FTFMain
                    .DRDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                    .CRDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                    .RefNo = Me.GetLastRefNoUsed + 1
                    .TransType = EnumFTFTransType.Replenishment
                    .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                    For Each item In Me.PaymentTransferToPRList
                        Dim AMPartifipantsInfo = (From x In Me.AMParticipants
                                                  Where x.IDNumber = item.IDNumber _
                                                  And x.ParticipantID = item.ParticipantID
                                                  Select x).FirstOrDefault
                        If item.TransferToPrudential > 0 Then
                            .ListOfFTFParticipants.Add(New FundTransferFormParticipant(.RefNo, AMPartifipantsInfo, Math.Abs(item.TransferToPrudential)))
                            TotalPRAmount += Math.Abs(item.TransferToPrudential)
                        End If
                    Next
                    .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashinBankPrudentialCode, PrudentialBankAccountNo, TotalPRAmount, 0))
                    .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashInbankSettlementcode, SettlementBankAccountNo, 0, TotalPRAmount))
                    .PreparedBy = AMModule.FullName
                    .RequestingApproval = Signatory.Signatory_1
                    .ApprovedBy = Signatory.Signatory_2
                    .TotalAmount = TotalPRAmount
                    .Status = EnumStatus.Active
                End With
                Me.FundTransferform.Add(_FTFMain)
            End Using
        End If
    End Sub

    Public Sub CreateFTFForFinPen()
        Dim RefNo As Long = 0
        Dim SumFinPenAmount = (From x In Me.PaymentTransferToPRList
                               Select x.TransferToFinPen).Sum
        Dim Signatory = WBillHelper.GetSignatories("FTF2").First()
        If Signatory Is Nothing Then
            Throw New System.Exception("FTF2 not found in the signatories table, please contact the administrator!")
            Exit Sub
        End If
        Dim TotalFinPenAmount As Decimal = 0
        If SumFinPenAmount > 0 Then
            Me.ORofFinPen = New List(Of OfficialReceiptMain)
            Me.FinPenORNumber = 0
            Using _FTFMain As New FundTransferFormMain
                With _FTFMain
                    .DRDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                    .CRDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                    .RefNo = Me.GetLastRefNoUsed + 1
                    .TransType = EnumFTFTransType.TransferPEMCAccount
                    .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                    For Each item In Me.PaymentTransferToPRList
                        Dim AMPartifipantsInfo = (From x In Me.AMParticipants
                                                  Where x.IDNumber = item.IDNumber _
                                                  And x.ParticipantID = item.ParticipantID
                                                  Select x).FirstOrDefault
                        If item.TransferToFinPen > 0 Then
                            .ListOfFTFParticipants.Add(New FundTransferFormParticipant(.RefNo, AMPartifipantsInfo, Math.Abs(item.TransferToFinPen)))
                            Me.ORofFinPen.Add(Me.GenerateFinPenOR(item))
                            TotalFinPenAmount += Math.Abs(item.TransferToFinPen)
                        End If
                    Next
                    .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashinBankPEMCCode, AMModule.PemcBankAccountNo, TotalFinPenAmount, 0))
                    .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashInbankSettlementcode, AMModule.SettlementBankAccountNo, 0, TotalFinPenAmount))
                    .PreparedBy = AMModule.FullName
                    .RequestingApproval = Signatory.Signatory_1
                    .ApprovedBy = Signatory.Signatory_2
                    .TotalAmount = TotalFinPenAmount
                    .Status = EnumStatus.Active
                End With
                Me.FundTransferform.Add(_FTFMain)
            End Using
        End If
    End Sub

    Public Sub UpdateFTFForReplenishment()
        Dim UpdateFTFForRep = (From x In Me.FundTransferform Where x.TransType = EnumFTFTransType.Replenishment Select x).FirstOrDefault
        If Not UpdateFTFForRep Is Nothing Then
            UpdateFTFForRep.ListOfFTFParticipants = New List(Of FundTransferFormParticipant)
            UpdateFTFForRep.ListOfFTFDetails = New List(Of FundTransferFormDetails)

            Dim TotalPRAmount As Decimal = 0
            With UpdateFTFForRep
                For Each item In Me.PaymentTransferToPRList
                    Dim AMPartifipantsInfo = (From x In Me.AMParticipants
                                              Where x.IDNumber = item.IDNumber _
                                              And x.ParticipantID = item.ParticipantID
                                              Select x).FirstOrDefault
                    If item.TransferToPrudential > 0 Then
                        .ListOfFTFParticipants.Add(New FundTransferFormParticipant(.RefNo, AMPartifipantsInfo, Math.Abs(item.TransferToPrudential)))
                        TotalPRAmount += Math.Abs(item.TransferToPrudential)
                    End If
                Next
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashinBankPrudentialCode, PrudentialBankAccountNo, TotalPRAmount, 0))
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashInbankSettlementcode, SettlementBankAccountNo, 0, TotalPRAmount))
                .TotalAmount = TotalPRAmount
            End With
        Else
            Me.CreateFTFForReplenishment()
        End If
    End Sub

    Public Sub UpdateFTFForFinPen()
        Dim UpdateFTFForFinPenalty = (From x In Me.FundTransferform Where x.TransType = EnumFTFTransType.TransferPEMCAccount Select x).FirstOrDefault

        If Not UpdateFTFForFinPenalty Is Nothing Then
            UpdateFTFForFinPenalty.ListOfFTFParticipants = New List(Of FundTransferFormParticipant)
            UpdateFTFForFinPenalty.ListOfFTFDetails = New List(Of FundTransferFormDetails)

            Dim TotalFinPenAmount As Decimal = 0
            Me.FinPenORNumber = 0
            Me.ORofFinPen = New List(Of OfficialReceiptMain)
            With UpdateFTFForFinPenalty
                For Each item In Me.PaymentTransferToPRList
                    Dim AMPartifipantsInfo = (From x In Me.AMParticipants
                                              Where x.IDNumber = item.IDNumber _
                                              And x.ParticipantID = item.ParticipantID
                                              Select x).FirstOrDefault
                    If item.TransferToFinPen > 0 Then
                        .ListOfFTFParticipants.Add(New FundTransferFormParticipant(.RefNo, AMPartifipantsInfo, Math.Abs(item.TransferToFinPen)))
                        Me.ORofFinPen.Add(Me.GenerateFinPenOR(item))
                        TotalFinPenAmount += Math.Abs(item.TransferToFinPen)
                    End If
                Next
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashinBankPEMCCode, AMModule.PemcBankAccountNo, TotalFinPenAmount, 0))
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashInbankSettlementcode, AMModule.SettlementBankAccountNo, 0, TotalFinPenAmount))
                .TotalAmount = TotalFinPenAmount
            End With
        Else
            Me.CreateFTFForFinPen()
        End If
    End Sub

    Public Sub CreateFTFForMFOffsetting()
        Dim ret As New FundTransferFormMain
        Dim MFCollectionAndOffsetting = (From x In Me.MFwithVATCollectionList Select x).Union _
                                        (From x In Me.OffsettingMFwithVATCollectionList Select x).ToList

        Dim SumOfMPwithMFColl = (From x In MFCollectionAndOffsetting
                                 Group By x.IDNumber, x.ParticipantID
                                 Into MFAmount = Sum(x.AllocationAmount)
                                 Order By ParticipantID, IDNumber
                                 Select IDNumber, ParticipantID, MFAmount).ToList()

        If SumOfMPwithMFColl.Count = 0 Then Exit Sub

        Dim Signatory = WBillHelper.GetSignatories("FTF2").First()
        If Signatory Is Nothing Then
            Throw New System.Exception("FTF2 not found in the signatories table, please contact the administrator!")
            Exit Sub
        End If
        Dim TotalMFAmount As Decimal = 0
        Using _FTFMain As New FundTransferFormMain
            With _FTFMain
                .DRDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                .CRDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                .RefNo = Me.GetLastRefNoUsed + 1
                .TransType = EnumFTFTransType.TransferMarketFeesToPEMC
                .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                For Each item In SumOfMPwithMFColl
                    Dim AMParticipantsInfo = (From x In Me.AMParticipants
                                              Where x.IDNumber = item.IDNumber _
                                              And x.ParticipantID = item.ParticipantID
                                              Select x).FirstOrDefault
                    .ListOfFTFParticipants.Add(New FundTransferFormParticipant(.RefNo, AMParticipantsInfo, Math.Abs(item.MFAmount)))
                    TotalMFAmount += Math.Abs(item.MFAmount)
                Next
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashinBankPEMCCode, PemcBankAccountNo, TotalMFAmount, 0))
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashInbankSettlementcode, SettlementBankAccountNo, 0, TotalMFAmount))
                .PreparedBy = AMModule.FullName
                .RequestingApproval = Signatory.Signatory_1
                .ApprovedBy = Signatory.Signatory_2
                .TotalAmount = TotalMFAmount
                .Status = EnumStatus.Active
            End With
            Me.FundTransferform.Add(_FTFMain)
        End Using
    End Sub

    Public Sub CreateFTFForMFAPPaidByPEMCAcc()
        Dim ret As New FundTransferFormMain
        Dim SumOfMPwithMFAlloc = (From x In Me.MFwithVATAllocationList
                                  Group By x.IDNumber, x.ParticipantID
                                  Into MFAmount = Sum(x.AllocationAmount)
                                  Order By ParticipantID, IDNumber
                                  Select IDNumber, ParticipantID, MFAmount).ToList()

        If SumOfMPwithMFAlloc.Count = 0 Then Exit Sub

        Dim Signatory = WBillHelper.GetSignatories("FTF2").First()
        If Signatory Is Nothing Then
            Throw New System.Exception("FTF2 not found in the signatories table, please contact the administrator!")
            Exit Sub
        End If
        Dim TotalMFAmount As Decimal = 0
        Using _FTFMain As New FundTransferFormMain
            With _FTFMain
                .DRDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                .CRDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                .RefNo = Me.GetLastRefNoUsed + 1
                .TransType = EnumFTFTransType.TransferMarketFeesToSTL
                .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                For Each item In SumOfMPwithMFAlloc
                    Dim AMParticipantsInfo = (From x In Me.AMParticipants
                                              Where x.IDNumber = item.IDNumber _
                                              And x.ParticipantID = item.ParticipantID
                                              Select x).FirstOrDefault
                    .ListOfFTFParticipants.Add(New FundTransferFormParticipant(.RefNo, AMParticipantsInfo, Math.Abs(item.MFAmount)))
                    TotalMFAmount += Math.Abs(item.MFAmount)
                Next
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashInbankSettlementcode, AMModule.SettlementBankAccountNo, TotalMFAmount, 0))
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashinBankPEMCCode, AMModule.PemcBankAccountNo, 0, TotalMFAmount))
                .PreparedBy = AMModule.FullName
                .RequestingApproval = Signatory.Signatory_1
                .ApprovedBy = Signatory.Signatory_2
                .TotalAmount = TotalMFAmount
                .Status = EnumStatus.Active
            End With
            Me.FundTransferform.Add(_FTFMain)
        End Using
    End Sub

    Private Sub CreateFTFForEWT()
        Dim ret As New FundTransferFormMain

        If getWESMBillSalesAndPurchases.Count = 0 Then Exit Sub

        Dim Signatory = WBillHelper.GetSignatories("FTF").First()
        Dim TotalEWTAmount As Decimal = 0
        Using _FTFMain As New FundTransferFormMain
            With _FTFMain
                .DRDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                .CRDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                .RefNo = Me.GetLastRefNoUsed + 1
                .TransType = EnumFTFTransType.EnergyWithholdingTax
                .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                For Each item In getWESMBillSalesAndPurchases
                    Dim AMPartifipantsInfo = (From x In Me.AMParticipants
                                              Where x.IDNumber = item.IDNumber.IDNumber
                                              Select x).FirstOrDefault
                    .ListOfFTFParticipants.Add(New FundTransferFormParticipant(.RefNo, AMPartifipantsInfo, Math.Abs(item.WithholdingTAX)))
                    TotalEWTAmount += Math.Abs(item.WithholdingTAX)
                Next
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashinBankPEMCCode, AMModule.PemcBankAccountNo, TotalEWTAmount, 0))
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, AMModule.CashInbankSettlementcode, AMModule.SettlementBankAccountNo, 0, TotalEWTAmount))
                .PreparedBy = AMModule.FullName
                .RequestingApproval = Signatory.Signatory_1
                .ApprovedBy = Signatory.Signatory_2
                .TotalAmount = TotalEWTAmount
                .Status = EnumStatus.Active
            End With
            Me.FundTransferform.Add(_FTFMain)
        End Using

    End Sub

#End Region

#Region "Methods for Prudential and History"
    Private Sub UpdatePrudentialHistory()
        Me.NewPrudentialHistory.Clear()
        Dim PRList As New List(Of PrudentialHistory)
        Dim UpdateFTFForRep = (From x In Me.FundTransferform Where x.TransType = EnumFTFTransType.Replenishment Select x).FirstOrDefault
        If Not UpdateFTFForRep Is Nothing Then
            For Each item In UpdateFTFForRep.ListOfFTFParticipants
                Dim PR As New PrudentialHistory
                With PR
                    .IDNumber = New AMParticipants(item.IDNumber.IDNumber, item.IDNumber.ParticipantID)
                    .Amount = item.Amount
                    .TransType = EnumPrudentialTransType.Replenishment
                    .TransDate = Me.PayAllocDate.CollAllocationDate()
                    .FTFNumber = item.RefNo
                End With
                PRList.Add(PR)
            Next
            Me.NewPrudentialHistory = PRList
        End If
    End Sub
#End Region

#Region "Function For Generating Request For Payment Data Set"
    Public Function GenerateRFP(ByVal DTRFP As DataTable, ByVal DTRFPCollections As DataTable, ByVal DTRFPPayments As DataTable) As DataSet
        Dim ret As New DataSet

        Dim nRow = DTRFP.NewRow
        With Me.RequestForPayment
            Dim TotalPayment = (From x In .RFPDetails
                                Where x.RFPDetailsType = EnumRFPDetailsType.Payment
                                Select x.Amount).Sum()

            Dim TotalDeferred = (From x In .RFPDetails
                                 Where x.RFPDetailsType = EnumRFPDetailsType.Payment _
                                 And x.Amount < 1000
                                 Select x.Amount).Sum()

            Dim TotalLBC = (From x In .RFPDetails
                            Where x.RFPDetailsType = EnumRFPDetailsType.Payment _
                            And x.PaymentType = EnumParticipantPaymentType.Check _
                            And x.Amount >= 1000
                            Select x.Amount).Sum()

            Dim TotalEFT = (From x In .RFPDetails
                            Where x.RFPDetailsType = EnumRFPDetailsType.Payment _
                            And x.PaymentType = EnumParticipantPaymentType.EFT _
                            And x.Amount >= 1000
                            Select x.Amount).Sum()

            nRow("REF_NO") = CStr(CInt(.ReferenceNo).ToString("000"))
            nRow("RFP_TO") = .toRFP
            nRow("RFP_FROM") = .FromRFP
            nRow("PURPOSE") = .PurposeOfPayment
            nRow("NOTE") = ""
            nRow("TOTAL_PAYMENT") = TotalPayment
            nRow("DATE") = .AllocationDate.ToShortDateString
            nRow("TOTAL_DEFERRED") = TotalDeferred
            nRow("TOTAL_LBC") = TotalLBC
            nRow("TOTAL_RTGS") = TotalEFT
            nRow("TOTAL_NSS") = .NSSAmount
            nRow("TOTAL_PRUDENTIAL") = .PRReplenishment
            nRow("TOTAL_MF") = .MarketFees
            nRow("HELD_COLLECTION") = .HeldCollection
            nRow("TO_PEMC") = .TransferToPEMC
            nRow("ALLOCATION_DATE") = CDate(.AllocationDate.ToShortDateString)
            nRow("PREPARED_BY") = .PreparedBy
            nRow("PREPARED_BY_POS") = .PreparedByPosition
            nRow("REVIEWED_BY") = .ReviewedBy
            nRow("REVIEWED_BY_POS") = .ReviewedByPosition
            nRow("APPROVED_BY") = .ApprovedBy
            nRow("APPROVED_BY_POS") = .ApprovedByPosition
        End With
        DTRFP.Rows.Add(nRow)
        DTRFP.AcceptChanges()

        Dim GetCollinRFP = (From x In Me.RequestForPayment.RFPDetails
                            Where x.RFPDetailsType = EnumRFPDetailsType.Collection
                            Select x).ToList()
        For Each Coll In GetCollinRFP
            Dim nRowColl = DTRFPCollections.NewRow
            Dim AMParticipantsInfo = (From x In Me.AMParticipants Where x.IDNumber = Coll.Participant Select x).FirstOrDefault()
            nRowColl("PAYOR") = AMParticipantsInfo.FullName
            nRowColl("ID_NO") = Coll.Participant
            nRowColl("DATE_OF_DEPOSIT") = Coll.DateOfDeposit
            nRowColl("AMOUNT") = Coll.Amount
            nRowColl("PARTICULARS") = Coll.Particulars
            nRowColl("ALLOCATION_DATE") = Coll.AllocationDate
            DTRFPCollections.Rows.Add(nRowColl)
            DTRFPCollections.AcceptChanges()
        Next

        Dim GetPayinRFP = (From x In Me.RequestForPayment.RFPDetails
                           Where x.RFPDetailsType = EnumRFPDetailsType.Payment
                           Select x).ToList()

        For Each Pay In GetPayinRFP
            Dim nRowPay = DTRFPPayments.NewRow
            Dim AMParticipantsInfo = (From x In Me.AMParticipants Where x.IDNumber = Pay.Participant Select x).FirstOrDefault()
            nRowPay("PAYEE") = AMParticipantsInfo.FullName
            nRowPay("ID_NO") = Pay.Participant
            nRowPay("BANKBRANCH") = AMParticipantsInfo.Bank & "-" & AMParticipantsInfo.BankBranch
            nRowPay("ACCOUNT_NO") = AMParticipantsInfo.BankAccountNo
            nRowPay("PAYMENT_TYPE") = Pay.PaymentType
            nRowPay("AMOUNT") = Pay.Amount
            nRowPay("PARTICULARS") = Pay.Particulars
            nRowPay("ALLOCATION_DATE") = Pay.AllocationDate.ToShortDateString
            DTRFPPayments.Rows.Add(nRowPay)
            DTRFPPayments.AcceptChanges()
        Next

        ret.Tables.Add(DTRFP)
        ret.Tables.Add(DTRFPPayments)
        ret.Tables.Add(DTRFPCollections)
        ret.AcceptChanges()

        Return ret
    End Function
#End Region

#Region "Method For Generating Request For Payment Property"
    Private Sub UpdateRFP(ByVal ItemOfTransToPR As PaymentTransferToPR, ByVal eFTFTransType As EnumFTFTransType)

        If eFTFTransType = EnumFTFTransType.Replenishment Then
            Dim TotalPRReplenishPayment = (From x In Me.FundTransferform
                                           Where x.TransType = EnumFTFTransType.Replenishment
                                           Select x.TotalAmount).Sum
            Dim PRReplenishCollection = (From x In Me.CollFundTransferform Where x.TransType = EnumFTFTransType.Replenishment Select x).ToList()
            Dim TotalPRReplenishColl As Decimal = PRReplenishCollection.Sum(Function(x As FundTransferFormMain) x.TotalAmount)

            Me.RequestForPayment.PRReplenishment = TotalPRReplenishColl + TotalPRReplenishPayment
            Dim UpdateRFPofMP = (From x In Me.RequestForPayment.RFPDetails
                                 Where x.Participant = ItemOfTransToPR.IDNumber _
                                 And x.RFPDetailsType = EnumRFPDetailsType.Payment
                                 Select x).FirstOrDefault

            If Not DicRFPParticularPR.ContainsKey(ItemOfTransToPR.IDNumber.ToString()) Then
                DicRFPParticularPR.Add(ItemOfTransToPR.IDNumber.ToString(), UpdateRFPofMP.Particulars.ToString)
            End If

            If Not DicRFPPaymentTypePR.ContainsKey(ItemOfTransToPR.IDNumber.ToString) Then
                DicRFPPaymentTypePR.Add(ItemOfTransToPR.IDNumber.ToString, UpdateRFPofMP.PaymentType)
            End If
            If UpdateRFPofMP Is Nothing Then
                Dim ParticipantInfo = (From x In Me.AMParticipants Where x.IDNumber = ItemOfTransToPR.IDNumber Select x).FirstOrDefault

                Dim GetExcessCollection As Decimal = (From x In Me.CollectionMonitoring
                                                      Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                                                      And x.IDNumber.IDNumber = ItemOfTransToPR.IDNumber
                                                      Select x.Amount).Sum()

                Dim GetPrevDeferredEnergy As Decimal = (From x In Me.PrevDeferredPaymentList
                                                        Where x.IDNumber = ItemOfTransToPR.IDNumber _
                                                        And x.ChargeType = EnumChargeType.E
                                                        Select x.OutstandingBalanceDeferredPayment).Sum

                Dim GetPrevDeferredVAT As Decimal = (From x In Me.PrevDeferredPaymentList
                                                     Where x.IDNumber = ItemOfTransToPR.IDNumber _
                                                     And x.ChargeType = EnumChargeType.EV
                                                     Select x.OutstandingBalanceDeferredPayment).Sum
                Using _RFPDetails As New RequestForPaymentDetails
                    With _RFPDetails
                        .Amount = ItemOfTransToPR.TotalPaymentAllocated
                        .Participant = ParticipantInfo.IDNumber
                        .BankBranch = ParticipantInfo.BankBranch
                        .AccountNo = ParticipantInfo.BankAccountNo
                        .ReferenceNo = 1
                        .AllocationDate = Me.PayAllocDate.CollAllocationDate
                        .DateOfDeposit = FormatDateTime(WBillHelper.GetSystemDate(), DateFormat.ShortDate)
                        .RFPDetailsType = EnumRFPDetailsType.Payment
                        .PaymentType = ParticipantInfo.PaymentType

                        If .Amount < DeferredPayment Then
                            .Particulars = "Set as Deferred Payment"
                            .PaymentType = EnumParticipantPaymentType.SetAsDeferred
                        ElseIf .Amount > DeferredPayment Then
                            If ItemOfTransToPR.PaymentOnEnergy <> 0 Then
                                .Particulars &= "Energy; "
                            End If
                            Dim GetDefaultInterest = (From x In Me.EnergyAllocationList
                                                      Where x.IDNumber = .Participant And x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy
                                                      Select x.AllocationAmount).Sum() +
                                                     (From x In Me.OffsettingEnergyAllocationList
                                                      Where x.IDNumber = .Participant And x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy
                                                      Select x.AllocationAmount).Sum() +
                                                     (From x In Me.MFwithVATAllocationList
                                                      Where x.IDNumber = .Participant _
                                                      And (x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                                           Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF _
                                                           Or x.PaymentType = EnumPaymentNewType.WithholdingTaxOnDefaultInterest _
                                                           Or x.PaymentType = EnumPaymentNewType.WithholdingVatOnDefaultInterest)
                                                      Select x.AllocationAmount).Sum() +
                                                     (From x In Me.OffsettingMFwithVATAllocationList
                                                      Where x.IDNumber = .Participant _
                                                       And (x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                                           Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF _
                                                           Or x.PaymentType = EnumPaymentNewType.WithholdingTaxOnDefaultInterest _
                                                           Or x.PaymentType = EnumPaymentNewType.WithholdingVatOnDefaultInterest)
                                                      Select x.AllocationAmount).Sum()

                            If GetDefaultInterest <> 0 Then
                                .Particulars &= "Default Interest; "
                            End If

                            If ItemOfTransToPR.PaymentOnVATonEnergy <> 0 Then
                                .Particulars &= "VAT; "
                            End If

                            If ItemOfTransToPR.PaymentOnMFWithVAT > 0 Then
                                .Particulars &= "Market Fees; "
                            End If

                            If GetExcessCollection <> 0 Then
                                .Particulars &= "Excess Collection; "
                            End If

                            If .Particulars.Length <> 0 Then
                                .Particulars = Trim(Mid(.Particulars, 1, Len(.Particulars) - 2))
                            End If
                        End If
                    End With
                    If _RFPDetails.Amount <> 0 Then
                        Me.RequestForPayment.RFPDetails.Add(_RFPDetails)
                    End If
                End Using
            Else
                With UpdateRFPofMP
                    If ItemOfTransToPR.FullyTransferToPR = True Then
                        .Amount = ItemOfTransToPR.TotalAmountForRemittance

                        If .Amount < DeferredPayment Then
                            .Particulars = "Set as Deferred Payment"
                            .PaymentType = EnumParticipantPaymentType.SetAsDeferred
                        ElseIf .Amount = 0 Then
                            Me.RequestForPayment.RFPDetails.RemoveAll(Function(x) x.Participant = ItemOfTransToPR.IDNumber And x.RFPDetailsType = EnumRFPDetailsType.Payment)
                        Else
                            If InStrRev(.Particulars, "Energy; ") > 0 Then
                                .Particulars = .Particulars.Replace("Energy; ", "")
                            ElseIf InStrRev(.Particulars, "Energy") > 0 Then
                                .Particulars = .Particulars.Replace("Energy", "")
                            ElseIf InStrRev(.Particulars, "; Energy") > 0 Then
                                .Particulars = .Particulars.Replace("; Energy", "")
                            End If
                        End If
                    Else
                        .Amount = ItemOfTransToPR.TotalAmountForRemittance
                        If .Amount < DeferredPayment Then
                            .Particulars = "Set as Deferred Payment"
                            .PaymentType = EnumParticipantPaymentType.SetAsDeferred
                        ElseIf .Amount = 0 Then
                            Me.RequestForPayment.RFPDetails.RemoveAll(Function(x) x.Participant = ItemOfTransToPR.IDNumber And x.RFPDetailsType = EnumRFPDetailsType.Payment)
                        Else
                            .Particulars = DicRFPParticularPR.Item(ItemOfTransToPR.IDNumber)
                            .PaymentType = DicRFPPaymentTypePR.Item(ItemOfTransToPR.IDNumber)
                        End If
                    End If
                End With
            End If

        ElseIf eFTFTransType = EnumFTFTransType.TransferPEMCAccount Then
            Dim TotalFinPenPayment = (From x In Me.FundTransferform
                                      Where x.TransType = EnumFTFTransType.TransferPEMCAccount
                                      Select x.TotalAmount).Sum

            Dim FinPenCollection = (From x In Me.CollFundTransferform Where x.TransType = EnumFTFTransType.TransferPEMCAccount Select x).ToList()
            Dim TotalFinPenCollection As Decimal = FinPenCollection.Sum(Function(x As FundTransferFormMain) x.TotalAmount)

            Me.RequestForPayment.TransferToPEMC = TotalFinPenCollection + TotalFinPenPayment

            Dim UpdateRFPofMP = (From x In Me.RequestForPayment.RFPDetails
                                 Where x.Participant = ItemOfTransToPR.IDNumber _
                                 And x.RFPDetailsType = EnumRFPDetailsType.Payment
                                 Select x).FirstOrDefault

            If Not DicRFPParticularFP.ContainsKey(ItemOfTransToPR.IDNumber.ToString()) Then
                DicRFPParticularFP.Add(ItemOfTransToPR.IDNumber.ToString(), UpdateRFPofMP.Particulars.ToString)
            End If

            If Not DicRFPPaymentTypeFP.ContainsKey(ItemOfTransToPR.IDNumber.ToString) Then
                DicRFPPaymentTypeFP.Add(ItemOfTransToPR.IDNumber.ToString, UpdateRFPofMP.PaymentType)
            End If

            If UpdateRFPofMP Is Nothing Then
                Dim ParticipantInfo = (From x In Me.AMParticipants Where x.IDNumber = ItemOfTransToPR.IDNumber Select x).FirstOrDefault

                Dim GetExcessCollection As Decimal = (From x In Me.CollectionMonitoring
                                                      Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                                                      And x.IDNumber.IDNumber = ItemOfTransToPR.IDNumber
                                                      Select x.Amount).Sum()

                Dim GetPrevDeferredEnergy As Decimal = (From x In Me.PrevDeferredPaymentList
                                                        Where x.IDNumber = ItemOfTransToPR.IDNumber _
                                                        And x.ChargeType = EnumChargeType.E
                                                        Select x.OutstandingBalanceDeferredPayment).Sum

                Dim GetPrevDeferredVAT As Decimal = (From x In Me.PrevDeferredPaymentList
                                                     Where x.IDNumber = ItemOfTransToPR.IDNumber _
                                                     And x.ChargeType = EnumChargeType.EV
                                                     Select x.OutstandingBalanceDeferredPayment).Sum
                Using _RFPDetails As New RequestForPaymentDetails
                    With _RFPDetails
                        .Amount = ItemOfTransToPR.TotalPaymentAllocated
                        .Participant = ParticipantInfo.IDNumber
                        .BankBranch = ParticipantInfo.BankBranch
                        .AccountNo = ParticipantInfo.BankAccountNo
                        .ReferenceNo = 1
                        .AllocationDate = Me.PayAllocDate.CollAllocationDate
                        .DateOfDeposit = FormatDateTime(WBillHelper.GetSystemDate(), DateFormat.ShortDate)
                        .RFPDetailsType = EnumRFPDetailsType.Payment
                        .PaymentType = ParticipantInfo.PaymentType

                        If .Amount < DeferredPayment Then
                            .Particulars = "Set as Deferred Payment"
                            .PaymentType = EnumParticipantPaymentType.SetAsDeferred
                        ElseIf .Amount > DeferredPayment Then
                            If ItemOfTransToPR.PaymentOnEnergy <> 0 Then
                                .Particulars &= "Energy; "
                            End If
                            Dim GetDefaultInterest = (From x In Me.EnergyAllocationList
                                                      Where x.IDNumber = .Participant And x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy
                                                      Select x.AllocationAmount).Sum() +
                                                     (From x In Me.OffsettingEnergyAllocationList
                                                      Where x.IDNumber = .Participant And x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy
                                                      Select x.AllocationAmount).Sum() +
                                                     (From x In Me.MFwithVATAllocationList
                                                      Where x.IDNumber = .Participant _
                                                      And (x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                                           Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF _
                                                           Or x.PaymentType = EnumPaymentNewType.WithholdingTaxOnDefaultInterest _
                                                           Or x.PaymentType = EnumPaymentNewType.WithholdingVatOnDefaultInterest)
                                                      Select x.AllocationAmount).Sum() +
                                                     (From x In Me.OffsettingMFwithVATAllocationList
                                                      Where x.IDNumber = .Participant _
                                                       And (x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                                           Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF _
                                                           Or x.PaymentType = EnumPaymentNewType.WithholdingTaxOnDefaultInterest _
                                                           Or x.PaymentType = EnumPaymentNewType.WithholdingVatOnDefaultInterest)
                                                      Select x.AllocationAmount).Sum()

                            If GetDefaultInterest <> 0 Then
                                .Particulars &= "Default Interest; "
                            End If

                            If ItemOfTransToPR.PaymentOnVATonEnergy <> 0 Then
                                .Particulars &= "VAT; "
                            End If

                            If ItemOfTransToPR.PaymentOnMFWithVAT > 0 Then
                                .Particulars &= "Market Fees; "
                            End If

                            If GetExcessCollection <> 0 Then
                                .Particulars &= "Excess Collection; "
                            End If

                            If .Particulars.Length <> 0 Then
                                .Particulars = Trim(Mid(.Particulars, 1, Len(.Particulars) - 2))
                            End If
                        End If
                    End With
                    If _RFPDetails.Amount <> 0 Then
                        Me.RequestForPayment.RFPDetails.Add(_RFPDetails)
                    End If
                End Using
            Else
                With UpdateRFPofMP
                    If ItemOfTransToPR.FullyTransferToFinPen = True Then
                        .Amount = ItemOfTransToPR.TotalAmountForRemittance

                        If .Amount < DeferredPayment Then
                            .Particulars = "Set as Deferred Payment"
                            .PaymentType = EnumParticipantPaymentType.SetAsDeferred
                        ElseIf .Amount = 0 Then
                            Me.RequestForPayment.RFPDetails.RemoveAll(Function(x) x.Participant = ItemOfTransToPR.IDNumber And x.RFPDetailsType = EnumRFPDetailsType.Payment)
                        Else
                            If InStrRev(.Particulars, "Energy; ") > 0 Then
                                .Particulars = .Particulars.Replace("Energy; ", "")
                            ElseIf InStrRev(.Particulars, "Energy") > 0 Then
                                .Particulars = .Particulars.Replace("Energy", "")
                            ElseIf InStrRev(.Particulars, "; Energy") > 0 Then
                                .Particulars = .Particulars.Replace("; Energy", "")
                            End If
                        End If

                    Else
                        .Amount = ItemOfTransToPR.TotalAmountForRemittance

                        If .Amount < DeferredPayment Then
                            .Particulars = "Set as Deferred Payment"
                            .PaymentType = EnumParticipantPaymentType.SetAsDeferred
                        ElseIf .Amount = 0 Then
                            Me.RequestForPayment.RFPDetails.RemoveAll(Function(x) x.Participant = ItemOfTransToPR.IDNumber And x.RFPDetailsType = EnumRFPDetailsType.Payment)
                        Else
                            .Particulars = DicRFPParticularFP.Item(ItemOfTransToPR.IDNumber)
                            .PaymentType = DicRFPPaymentTypeFP.Item(ItemOfTransToPR.IDNumber)
                        End If
                    End If

                End With
            End If
        End If

    End Sub
    'TEST2
    Private Sub CreateRFP()
        Me.RequestForPayment = Nothing

        Dim _GetSignatories = WBillHelper.GetSignatories("RFP").FirstOrDefault
        Dim GetSumHeldCollection = (From x In Me.CollectionMonitoring
                                    Where x.TransType = EnumCollectionMonitoringType.TransferToHeldCollection _
                                    And x.CollectionNoTag = 0
                                    Select x.Amount).Sum
        Dim GetSumPEMCAccount = (From x In Me.CollectionMonitoring
                                 Where x.TransType = EnumCollectionMonitoringType.TransferToPEMCAccount
                                 Select x.Amount).Sum
        Dim GetSumExcessCollection = (From x In Me.CollectionMonitoring
                                      Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection
                                      Select x.Amount).Sum

        Dim ForRFPReport As New RequestForPayment
        Dim RFPReferenceNo As Long = 1

        With ForRFPReport
            .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
            .ReferenceNo = RFPReferenceNo
            .toRFP = "CFM - Finance Department"
            .FromRFP = "AM- Finance Department"
            .PurposeOfPayment = "Remittance of Energy, VAT and Default Interests"
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .PreparedByPosition = AMModule.Position
            .ReviewedBy = _GetSignatories.Signatory_1
            .ReviewedByPosition = _GetSignatories.Position_1
            .ApprovedBy = _GetSignatories.Signatory_2
            .ApprovedByPosition = _GetSignatories.Position_2
            .PaymentDate = CDate(FormatDateTime(Me.PayAllocDate.RemittanceDate, DateFormat.ShortDate))

            Dim MFwithVATColl = (From x In Me.MFwithVATCollectionList
                                 Where (x.CollectionType = EnumCollectionType.MarketFees Or
                                        x.CollectionType = EnumCollectionType.VatOnMarketFees Or
                                        x.CollectionType = EnumCollectionType.DefaultInterestOnMF Or
                                        x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF Or
                                        x.CollectionType = EnumCollectionType.WithholdingTaxOnMF Or
                                        x.CollectionType = EnumCollectionType.WithholdingVatOnMF Or
                                        x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest Or
                                        x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest)
                                 Select x.AllocationAmount).Sum

            Dim OffsetMFwithVATColl = (From x In Me.OffsettingMFwithVATCollectionList
                                       Where (x.CollectionType = EnumCollectionType.MarketFees Or
                                              x.CollectionType = EnumCollectionType.VatOnMarketFees Or
                                              x.CollectionType = EnumCollectionType.DefaultInterestOnMF Or
                                              x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF Or
                                              x.CollectionType = EnumCollectionType.WithholdingTaxOnMF Or
                                              x.CollectionType = EnumCollectionType.WithholdingVatOnMF Or
                                              x.CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest Or
                                              x.CollectionType = EnumCollectionType.WithholdingVatOnDefaultInterest)
                                       Select x.AllocationAmount).Sum

            Dim MFwithVATPay = (From x In Me.MFwithVATAllocationList
                                Where (x.PaymentType = EnumPaymentNewType.MarketFees Or
                                       x.PaymentType = EnumPaymentNewType.VatOnMarketFees Or
                                       x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF Or
                                       x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF Or
                                       x.PaymentType = EnumPaymentNewType.WithholdingTaxOnMF Or
                                       x.PaymentType = EnumPaymentNewType.WithholdingVatOnMF Or
                                       x.PaymentType = EnumPaymentNewType.WithholdingTaxOnDefaultInterest Or
                                       x.PaymentType = EnumPaymentNewType.WithholdingVatOnDefaultInterest)
                                Select x.AllocationAmount).Sum
            Dim OffsetMFwithVATPay = (From x In Me.OffsettingMFwithVATAllocationList
                                      Where (x.PaymentType = EnumPaymentNewType.MarketFees Or
                                             x.PaymentType = EnumPaymentNewType.VatOnMarketFees Or
                                             x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF Or
                                             x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF Or
                                             x.PaymentType = EnumPaymentNewType.WithholdingTaxOnMF Or
                                             x.PaymentType = EnumPaymentNewType.WithholdingVatOnMF Or
                                             x.PaymentType = EnumPaymentNewType.WithholdingTaxOnDefaultInterest Or
                                             x.PaymentType = EnumPaymentNewType.WithholdingVatOnDefaultInterest)
                                      Select x.AllocationAmount).Sum

            Dim TotalMarketFeesWithVAT As Decimal = (MFwithVATColl + OffsetMFwithVATColl) + (MFwithVATPay + OffsetMFwithVATPay)

            If TotalMarketFeesWithVAT <> 0 Then
                .MarketFees = TotalMarketFeesWithVAT * -1D
            End If

            .NSSAmount = 0D

            Dim TotalPRReplenishPayment = (From x In Me.FundTransferform
                                           Where x.TransType = EnumFTFTransType.Replenishment
                                           Select x.TotalAmount).Sum

            Dim PRReplenishCollection = (From x In Me.CollFundTransferform Where x.TransType = EnumFTFTransType.Replenishment Select x).ToList()
            Dim TotalPRReplenishColl As Decimal = PRReplenishCollection.Sum(Function(x As FundTransferFormMain) x.TotalAmount)

            If TotalPRReplenishPayment + TotalPRReplenishColl <> 0 Then
                .PRReplenishment = TotalPRReplenishPayment + TotalPRReplenishColl
            End If

            If GetSumHeldCollection <> 0 Then
                .HeldCollection = GetSumHeldCollection
            End If
            'include Financial Penalty            
            If GetSumPEMCAccount <> 0 Then
                .TransferToPEMC = GetSumPEMCAccount
            End If
        End With

        'Payment RFP        
        Dim IncludedInRFP As New List(Of String)
        For Each itmParticipant In Me.PaymentTransferToPRList

            Dim ParticipantInfo = (From x In Me.AMParticipants Where x.IDNumber = itmParticipant.IDNumber Select x).FirstOrDefault
            If ParticipantInfo Is Nothing Then
                Throw New Exception("Cannot find Settlement ID: " & itmParticipant.IDNumber & " in the database")
                Exit Sub
            End If

            Dim GetExcessCollection As Decimal = (From x In Me.CollectionMonitoring
                                                  Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                                                  And x.IDNumber.IDNumber = itmParticipant.IDNumber
                                                  Select x.Amount).Sum()

            'Dim GetPrevDeferredEnergy As Decimal = (From x In Me.PrevDeferredPaymentList _
            '                                         Where x.IDNumber = itmParticipant.IDNumber _
            '                                         And x.ChargeType = EnumChargeType.E _
            '                                         Select x.OutstandingBalanceDeferredPayment).Sum

            'Dim GetPrevDeferredVAT As Decimal = (From x In Me.PrevDeferredPaymentList _
            '                                      Where x.IDNumber = itmParticipant.IDNumber _
            '                                      And x.ChargeType = EnumChargeType.EV _
            '                                      Select x.OutstandingBalanceDeferredPayment).Sum

            IncludedInRFP.Add(itmParticipant.IDNumber)
            Using _RFPDetails As New RequestForPaymentDetails
                With _RFPDetails
                    .Amount = itmParticipant.TotalAmountForRemittance 'itmParticipant.TotalPaymentAllocated 'changed by lance as of 06/05/2018
                    .Participant = ParticipantInfo.IDNumber
                    .BankBranch = ParticipantInfo.BankBranch
                    .AccountNo = ParticipantInfo.BankAccountNo
                    .ReferenceNo = RFPReferenceNo
                    .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString)
                    .DateOfDeposit = FormatDateTime(WBillHelper.GetSystemDate(), DateFormat.ShortDate)
                    .RFPDetailsType = EnumRFPDetailsType.Payment
                    .PaymentType = ParticipantInfo.PaymentType

                    If .Amount < DeferredPayment Then
                        .Particulars = "Set as Deferred Payment"
                        .PaymentType = EnumParticipantPaymentType.SetAsDeferred
                    ElseIf .Amount > DeferredPayment Then
                        If itmParticipant.PaymentOnEnergy <> 0 Then
                            .Particulars &= "Energy; "
                        End If
                        Dim GetDefaultInterest = (From x In Me.EnergyAllocationList
                                                  Where x.IDNumber = .Participant And x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy
                                                  Select x.AllocationAmount).Sum() +
                                                 (From x In Me.OffsettingEnergyAllocationList
                                                  Where x.IDNumber = .Participant And x.PaymentType = EnumPaymentNewType.DefaultInterestOnEnergy
                                                  Select x.AllocationAmount).Sum() +
                                                 (From x In Me.MFwithVATAllocationList
                                                  Where x.IDNumber = .Participant _
                                                  And (x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                                       Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF _
                                                       Or x.PaymentType = EnumPaymentNewType.WithholdingTaxOnDefaultInterest _
                                                       Or x.PaymentType = EnumPaymentNewType.WithholdingVatOnDefaultInterest)
                                                  Select x.AllocationAmount).Sum() +
                                                 (From x In Me.OffsettingMFwithVATAllocationList
                                                  Where x.IDNumber = .Participant _
                                                   And (x.PaymentType = EnumPaymentNewType.DefaultInterestOnMF _
                                                       Or x.PaymentType = EnumPaymentNewType.DefaultInterestOnVatOnMF _
                                                       Or x.PaymentType = EnumPaymentNewType.WithholdingTaxOnDefaultInterest _
                                                       Or x.PaymentType = EnumPaymentNewType.WithholdingVatOnDefaultInterest)
                                                  Select x.AllocationAmount).Sum()

                        If GetDefaultInterest <> 0 Then
                            .Particulars &= "Default Interest; "
                        End If

                        If itmParticipant.PaymentOnVATonEnergy <> 0 Then
                            .Particulars &= "VAT; "
                        End If

                        If itmParticipant.PaymentOnMFWithVAT > 0 Then
                            .Particulars &= "Market Fees; "
                        End If

                        If GetExcessCollection <> 0 Then
                            .Particulars &= "Excess Collection; "
                        End If

                        If .Particulars.Length <> 0 Then
                            .Particulars = Trim(Mid(.Particulars, 1, Len(.Particulars) - 2))
                        End If
                    End If
                End With
                If _RFPDetails.Amount <> 0 Then
                    ForRFPReport.RFPDetails.Add(_RFPDetails)
                End If
            End Using
        Next

        If IncludedInRFP.Count <> 0 Then
            'Dim GetNotIncludedDeferredParticipants = (From x In Me.PrevDeferredPaymentList _
            '                                          Where Not IncludedInRFP.Contains(x.IDNumber) _
            '                                          Select x.IDNumber).ToList()

            'For Each MPItem In GetNotIncludedDeferredParticipants
            '    Dim ParticipantInfo = (From x In Me.AMParticipants Where x.IDNumber = MPItem Select x).FirstOrDefault
            '    If ParticipantInfo Is Nothing Then
            '        Continue For
            '    End If

            '    IncludedInRFP.Add(MPItem)
            '    Dim GetPrevDeferredEnergy = (From x In Me.PrevDeferredPaymentList _
            '                           Where x.IDNumber = MPItem _
            '                           And x.ChargeType = EnumChargeType.E _
            '                           Select x.OutstandingBalanceDeferredPayment).Sum()

            '    Dim GetPrevDeferredVAT = (From x In Me.PrevDeferredPaymentList _
            '                           Where x.IDNumber = MPItem _
            '                           And x.ChargeType = EnumChargeType.EV _
            '                           Select x.OutstandingBalanceDeferredPayment).Sum()

            '    Dim GetExcessCollection = (From x In Me.CollectionMonitoring _
            '                           Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
            '                           And x.IDNumber.IDNumber = MPItem _
            '                           Select x.Amount).Sum()


            '    If (GetPrevDeferredEnergy + GetPrevDeferredVAT + GetExcessCollection) < DeferredPayment Then
            '        Using _RFPDetails As New RequestForPaymentDetails
            '            With _RFPDetails
            '                .Amount = GetPrevDeferredEnergy + GetPrevDeferredVAT + GetExcessCollection
            '                .Participant = ParticipantInfo.IDNumber
            '                .BankBranch = ParticipantInfo.BankBranch
            '                .AccountNo = ParticipantInfo.BankAccountNo
            '                .ReferenceNo = RFPReferenceNo
            '                .AllocationDate = Me.PayAllocDate.CollAllocationDate
            '                .DateOfDeposit = FormatDateTime(WBillHelper.GetSystemDate(), DateFormat.ShortDate)
            '                .RFPDetailsType = EnumRFPDetailsType.Payment
            '                .PaymentType = EnumParticipantPaymentType.SetAsDeferred
            '                .Particulars = "Set as Deferred Payment"
            '            End With
            '            ForRFPReport.RFPDetails.Add(_RFPDetails)
            '        End Using
            '    Else
            '        Using _RFPDetails As New RequestForPaymentDetails
            '            With _RFPDetails
            '                .Amount = GetPrevDeferredEnergy + GetPrevDeferredVAT + GetExcessCollection
            '                .Participant = ParticipantInfo.IDNumber
            '                .BankBranch = ParticipantInfo.BankBranch
            '                .AccountNo = ParticipantInfo.BankAccountNo
            '                .ReferenceNo = RFPReferenceNo
            '                .AllocationDate = Me.PayAllocDate.CollAllocationDate
            '                .DateOfDeposit = FormatDateTime(WBillHelper.GetSystemDate(), DateFormat.ShortDate)
            '                .RFPDetailsType = EnumRFPDetailsType.Payment
            '                .PaymentType = ParticipantInfo.PaymentType
            '                If (GetPrevDeferredEnergy + GetPrevDeferredVAT) <> 0 Then
            '                    .Particulars &= "Previous Deferred Payment; "
            '                End If

            '                If GetExcessCollection <> 0 Then
            '                    .Particulars &= "Excess Collection; "
            '                End If

            '                If .Particulars.Length <> 0 Then
            '                    .Particulars = Trim(Mid(.Particulars, 1, Len(.Particulars) - 2))
            '                End If
            '            End With
            '            ForRFPReport.RFPDetails.Add(_RFPDetails)
            '        End Using
            '    End If
            'Next

            Dim GetNotIncludedMPInCollMonitoring = (From x In Me.CollectionMonitoring
                                                    Where Not IncludedInRFP.Contains(x.IDNumber.IDNumber)
                                                    Select x.IDNumber.IDNumber).ToList()
            For Each MPItem In GetNotIncludedMPInCollMonitoring
                Dim ParticipantInfo = (From x In Me.AMParticipants Where x.IDNumber = MPItem Select x).FirstOrDefault
                If ParticipantInfo Is Nothing Then
                    Continue For
                End If

                Dim GetExcessCollection = (From x In Me.CollectionMonitoring
                                           Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                                           And x.IDNumber.IDNumber = MPItem
                                           Select x.Amount).Sum()

                If (GetExcessCollection) < DeferredPayment Then
                    Using _RFPDetails As New RequestForPaymentDetails
                        With _RFPDetails
                            .Amount = GetExcessCollection
                            .Participant = ParticipantInfo.IDNumber
                            .BankBranch = ParticipantInfo.BankBranch
                            .AccountNo = ParticipantInfo.BankAccountNo
                            .ReferenceNo = RFPReferenceNo
                            .AllocationDate = Me.PayAllocDate.CollAllocationDate
                            .DateOfDeposit = FormatDateTime(WBillHelper.GetSystemDate(), DateFormat.ShortDate)
                            .RFPDetailsType = EnumRFPDetailsType.Payment
                            .PaymentType = EnumParticipantPaymentType.SetAsDeferred
                            .Particulars = "Set as Deferred Payment"
                        End With
                        ForRFPReport.RFPDetails.Add(_RFPDetails)
                    End Using
                Else
                    Using _RFPDetails As New RequestForPaymentDetails
                        With _RFPDetails
                            .Amount = GetExcessCollection
                            .Participant = ParticipantInfo.IDNumber
                            .BankBranch = ParticipantInfo.BankBranch
                            .AccountNo = ParticipantInfo.BankAccountNo
                            .ReferenceNo = RFPReferenceNo
                            .AllocationDate = Me.PayAllocDate.CollAllocationDate
                            .DateOfDeposit = FormatDateTime(WBillHelper.GetSystemDate(), DateFormat.ShortDate)
                            .RFPDetailsType = EnumRFPDetailsType.Payment
                            .PaymentType = ParticipantInfo.PaymentType
                            If GetExcessCollection <> 0 Then
                                .Particulars &= "Excess Collection; "
                            End If

                            If .Particulars.Length <> 0 Then
                                .Particulars = Trim(Mid(.Particulars, 1, Len(.Particulars) - 2))
                            End If
                        End With
                        ForRFPReport.RFPDetails.Add(_RFPDetails)
                    End Using
                End If
            Next
        End If

        'Check NSS/STL/Prudential Replenishment
        'Get Distinct Participants in Collection
        Dim GetParticipants = (From x In Me.AMCollectionList
                               Where x.CollectionCategory = EnumCollectionCategory.Cash
                               Select x.IDNumber).Distinct().ToList()

        Dim IncludedInRFPSourceOfFunds As New List(Of String)
        For Each ItmParticipant In GetParticipants.Distinct()
            Dim ParticipantInfo = (From x In Me.AMParticipants
                                   Where x.IDNumber = ItmParticipant
                                   Select x).FirstOrDefault
            If ParticipantInfo Is Nothing Then
                Throw New Exception("Cannot find Settlement ID: " & ItmParticipant & " in the database")
                Exit Sub
            End If

            Dim GetParticipantCollection = (From x In Me.AMCollectionList
                                            Where x.IDNumber = ItmParticipant _
                                            And x.CollectionCategory = EnumCollectionCategory.Cash
                                            Select x).ToList()

            For Each ItmCollection In GetParticipantCollection
                Dim GetCollectAlloc = (From x In Me.AMCollectionAllocList
                                       Where x.CollectionNumber = ItmCollection.CollectionNumber
                                       Select x).ToList()
                Dim GetCollectMon = (From x In Me.CollectionMonitoring
                                     Where x.CollectionNo = ItmCollection.CollectionNumber
                                     Select x).ToList()

                Dim Remarks As String = ""
                For Each Item In GetCollectAlloc
                    Select Case Item.CollectionType
                        Case EnumCollectionType.Energy
                            If Remarks.Contains("ENERGY") = False Then
                                Remarks &= "ENERGY; "
                            End If
                        Case EnumCollectionType.VatOnEnergy
                            If Remarks.Contains("VAT") = False Then
                                Remarks &= "VAT; "
                            End If
                        Case EnumCollectionType.MarketFees,
                            EnumCollectionType.VatOnMarketFees,
                            EnumCollectionType.WithholdingTaxOnMF,
                            EnumCollectionType.WithholdingVatOnMF
                            If Remarks.Contains("MF") = False Then
                                Remarks &= "MF; "
                            End If
                        Case EnumCollectionType.DefaultInterestOnEnergy,
                            EnumCollectionType.DefaultInterestOnMF,
                            EnumCollectionType.DefaultInterestOnVatOnMF,
                            EnumCollectionType.WithholdingTaxOnDefaultInterest,
                            EnumCollectionType.WithholdingVatOnDefaultInterest
                            If Remarks.Contains("Default Interest") = False Then
                                Remarks &= "Default Interest; "
                            End If
                    End Select
                    For Each _Item In GetCollectMon
                        Select Case _Item.TransType
                            Case EnumCollectionMonitoringType.AppliedHeldCollection
                                If Remarks.Contains("Held Collection") = False Then
                                    Remarks &= "Held Collection; "
                                End If
                            Case EnumCollectionMonitoringType.TransferToExcessCollection
                                If Remarks.Contains("Excess Collection") = False Then
                                    Remarks &= "Excess Collection; "
                                End If
                            Case EnumCollectionMonitoringType.TransferToHeldCollection
                                If Remarks.Contains("Held Collection") = False Then
                                    Remarks &= "Held Collection; "
                                End If
                            Case EnumCollectionMonitoringType.TransferToPEMCAccount
                                If Remarks.Contains("PEMC Account") = False Then
                                    Remarks &= "PEMC Account; "
                                End If
                            Case EnumCollectionMonitoringType.TransferToPRReplenishment
                                If Remarks.Contains("PR Replenishment") = False Then
                                    Remarks &= "PR Replenishment; "
                                End If
                        End Select
                    Next
                Next
                If GetCollectMon.Count <> 0 And GetCollectAlloc.Count = 0 Then
                    For Each _Item In GetCollectMon
                        Select Case _Item.TransType
                            Case EnumCollectionMonitoringType.AppliedHeldCollection
                                If Remarks.Contains("Held Collection") = False Then
                                    Remarks &= "Held Collection; "
                                End If
                            Case EnumCollectionMonitoringType.TransferToExcessCollection
                                If Remarks.Contains("Excess Collection") = False Then
                                    Remarks &= "Excess Collection; "
                                End If
                            Case EnumCollectionMonitoringType.TransferToHeldCollection
                                If Remarks.Contains("Held Collection") = False Then
                                    Remarks &= "Held Collection; "
                                End If
                            Case EnumCollectionMonitoringType.TransferToPEMCAccount
                                If Remarks.Contains("PEMC Account") = False Then
                                    Remarks &= "PEMC Account; "
                                End If
                            Case EnumCollectionMonitoringType.TransferToPRReplenishment
                                If Remarks.Contains("PR Replenishment") = False Then
                                    Remarks &= "PR Replenishment; "
                                End If
                        End Select
                    Next
                End If
                If Right(Trim(Remarks), 1) = ";" Then
                    Remarks = Remarks.Remove(Remarks.Length - 2)
                End If
                Using _RFPDetails As New RequestForPaymentDetails
                    With _RFPDetails
                        .ReferenceNo = RFPReferenceNo
                        .Participant = ParticipantInfo.IDNumber
                        .BankBranch = ParticipantInfo.BankBranch
                        .AccountNo = ParticipantInfo.BankAccountNo
                        .Amount = ItmCollection.CollectedAmount
                        .DateOfDeposit = ItmCollection.CollectionDate.ToShortDateString
                        .Particulars = Remarks
                        .AllocationDate = Me.PayAllocDate.CollAllocationDate
                        .PaymentType = ParticipantInfo.PaymentType
                        .RFPDetailsType = EnumRFPDetailsType.Collection
                    End With
                    ForRFPReport.RFPDetails.Add(_RFPDetails)
                End Using
            Next
        Next

        'Dim getParticipantInPreDeferredPaymentList As List(Of String) = (From x In Me.PrevDeferredPaymentList Select x.IDNumber).Distinct.ToList()

        'For Each item In getParticipantInPreDeferredPaymentList
        '    Dim ParticipantInfo = (From x In Me.AMParticipants _
        '                           Where x.IDNumber = item _
        '                           Select x).FirstOrDefault
        '    If ParticipantInfo Is Nothing Then
        '        Continue For
        '    End If
        '    Dim getDeferredEnergyOffset = (From x In Me.EnergyShare Where x.IDNumber = item And x.PaymentType = EnumPaymentNewType.DeferredAppliedEnergy _
        '                                   Select x).FirstOrDefault
        '    If Not getDeferredEnergyOffset Is Nothing Then
        '        Dim getOffsetAmountInEnergy As Decimal = getDeferredEnergyOffset.AmountShare - getDeferredEnergyOffset.AmountBalance
        '        Using _RFPDetails As New RequestForPaymentDetails
        '            With _RFPDetails
        '                .ReferenceNo = RFPReferenceNo
        '                .Participant = ParticipantInfo.IDNumber
        '                .BankBranch = ParticipantInfo.BankBranch
        '                .AccountNo = ParticipantInfo.BankAccountNo
        '                .Amount = If(getOffsetAmountInEnergy = 0, getDeferredEnergyOffset.AmountShare, getOffsetAmountInEnergy)
        '                .DateOfDeposit = FormatDateTime(WBillHelper.GetSystemDate(), DateFormat.ShortDate)
        '                .Particulars = "Deferred Payment on Energy"
        '                .AllocationDate = Me.PayAllocDate.CollAllocationDate
        '                .PaymentType = ParticipantInfo.PaymentType
        '                .RFPDetailsType = EnumRFPDetailsType.Collection
        '            End With
        '            ForRFPReport.RFPDetails.Add(_RFPDetails)
        '        End Using
        '    End If
        '    Dim getDeferredVATOffset = (From x In Me.EnergyShare Where x.IDNumber = item And x.PaymentType = EnumPaymentNewType.DeferredAppliedVAT _
        '                                Select x).FirstOrDefault
        '    If Not getDeferredVATOffset Is Nothing Then
        '        Dim getOffsetAmountInVAT As Decimal = getDeferredEnergyOffset.AmountShare - getDeferredEnergyOffset.AmountBalance
        '        Using _RFPDetails As New RequestForPaymentDetails
        '            With _RFPDetails
        '                .ReferenceNo = RFPReferenceNo
        '                .Participant = ParticipantInfo.IDNumber
        '                .BankBranch = ParticipantInfo.BankBranch
        '                .AccountNo = ParticipantInfo.BankAccountNo
        '                .Amount = If(getOffsetAmountInVAT = 0, getDeferredEnergyOffset.AmountShare, getOffsetAmountInVAT)
        '                .DateOfDeposit = FormatDateTime(WBillHelper.GetSystemDate(), DateFormat.ShortDate)
        '                .Particulars = "Deferred Payment on VAT"
        '                .AllocationDate = Me.PayAllocDate.CollAllocationDate
        '                .PaymentType = ParticipantInfo.PaymentType
        '                .RFPDetailsType = EnumRFPDetailsType.Collection
        '            End With
        '            ForRFPReport.RFPDetails.Add(_RFPDetails)
        '        End Using
        '    End If

        'Next


        Me.RequestForPayment = ForRFPReport
    End Sub
#End Region

#Region "Function For Generating Deferred Payments"
    Public Function GenerateDeferredPaymentsDT(ByVal DeferredDT As DataTable) As DataTable

        Dim CurEnergyDeferred = (From x In Me.CurrentDeferredPaymentList
                                 Where x.ChargeType = EnumChargeType.E
                                 Select x).ToList()

        Dim PrevEnergyDeferred = (From x In Me.PrevDeferredPaymentList
                                  Where x.ChargeType = EnumChargeType.E
                                  Select x).ToList()

        Dim CurVATDeferred = (From x In Me.CurrentDeferredPaymentList
                              Where x.ChargeType = EnumChargeType.EV
                              Select x).ToList()

        Dim PrevVATDeferred = (From x In Me.PrevDeferredPaymentList
                               Where x.ChargeType = EnumChargeType.EV
                               Select x).ToList()

        Dim MPIDsList = (From x In Me.CurrentDeferredPaymentList Select x.IDNumber).Union _
                       (From x In Me.PrevDeferredPaymentList Select x.IDNumber).ToList()

        Dim GetMPIDs = (From x In MPIDsList Select x).Distinct.ToList()

        For Each item In GetMPIDs
            Dim dRow = DeferredDT.NewRow
            Dim ParticipantsInfo As AMParticipants = (From x In Me.AMParticipants
                                                      Where x.IDNumber = item).FirstOrDefault
            Dim GetCurEneDef = (From x In CurEnergyDeferred Where x.IDNumber = item Select x).FirstOrDefault
            Dim GetCurVATDef = (From x In CurVATDeferred Where x.IDNumber = item Select x).FirstOrDefault
            Dim GetPrevEneDef = (From x In PrevEnergyDeferred Where x.IDNumber = item Select x).FirstOrDefault
            Dim GetPrevVATDef = (From x In PrevVATDeferred Where x.IDNumber = item Select x).FirstOrDefault
            dRow("MPID") = ParticipantsInfo.IDNumber
            dRow("MP_NAME") = ParticipantsInfo.FullName
            dRow("ALLOCATE_DATE") = Me.PayAllocDate.RemittanceDate.ToShortDateString

            If GetPrevEneDef Is Nothing Then
                dRow("OLD_OB_ENERGY") = 0
            Else
                dRow("OLD_OB_ENERGY") = GetPrevEneDef.OutstandingBalanceDeferredPayment
            End If

            If GetPrevVATDef Is Nothing Then
                dRow("OLD_OB_VAT") = 0
            Else
                dRow("OLD_OB_VAT") = GetPrevVATDef.OutstandingBalanceDeferredPayment
            End If
            dRow("OLD_OB_TOTAL") = CDec(dRow("OLD_OB_ENERGY")) + CDec(dRow("OLD_OB_VAT"))

            If Not GetCurEneDef Is Nothing Then
                Select Case GetCurEneDef.DeferredType
                    Case EnumDeferredType.Remitted
                        dRow("REMIT_ENERGY") = GetCurEneDef.DeferredAmount
                        dRow("DEFERRAL_ENERGY") = 0
                        dRow("NEW_OB_ENERGY") = GetCurEneDef.OutstandingBalanceDeferredPayment
                    Case EnumDeferredType.Deferral
                        dRow("REMIT_ENERGY") = 0
                        dRow("DEFERRAL_ENERGY") = GetCurEneDef.DeferredAmount
                        dRow("NEW_OB_ENERGY") = GetCurEneDef.OutstandingBalanceDeferredPayment
                    Case EnumDeferredType.OutstandingBalance
                        dRow("REMIT_ENERGY") = 0
                        dRow("DEFERRAL_ENERGY") = 0
                        dRow("NEW_OB_ENERGY") = GetCurEneDef.OutstandingBalanceDeferredPayment
                End Select
            Else
                dRow("REMIT_ENERGY") = 0
                dRow("DEFERRAL_ENERGY") = 0
                dRow("NEW_OB_ENERGY") = 0
            End If

            If Not GetCurVATDef Is Nothing Then
                Select Case GetCurVATDef.DeferredType
                    Case EnumDeferredType.Remitted
                        dRow("REMIT_VAT") = GetCurVATDef.DeferredAmount
                        dRow("DEFERRAL_VAT") = 0
                        dRow("NEW_OB_VAT") = GetCurVATDef.OutstandingBalanceDeferredPayment
                    Case EnumDeferredType.Deferral
                        dRow("REMIT_VAT") = 0
                        dRow("DEFERRAL_VAT") = GetCurVATDef.DeferredAmount
                        dRow("NEW_OB_VAT") = GetCurVATDef.OutstandingBalanceDeferredPayment
                    Case EnumDeferredType.OutstandingBalance
                        dRow("REMIT_VAT") = 0
                        dRow("DEFERRAL_VAT") = 0
                        dRow("NEW_OB_VAT") = GetCurVATDef.OutstandingBalanceDeferredPayment
                End Select
            Else
                dRow("REMIT_VAT") = 0
                dRow("DEFERRAL_VAT") = 0
                dRow("NEW_OB_VAT") = 0
            End If
            dRow("REMIT_TOTAL") = CDec(dRow("REMIT_ENERGY")) + CDec(dRow("REMIT_VAT"))
            dRow("DEFERRAL_TOTAL") = CDec(dRow("DEFERRAL_ENERGY")) + CDec(dRow("DEFERRAL_VAT"))
            dRow("NEW_OB_TOTAL") = CDec(dRow("NEW_OB_ENERGY")) + CDec(dRow("NEW_OB_VAT"))
            DeferredDT.Rows.Add(dRow)
            DeferredDT.AcceptChanges()
        Next
        Return DeferredDT
    End Function

    Public Sub CreateDeferredPayments()
        Me.CurrentDeferredPaymentList.Clear()
        Dim UpdatedDeferredPayment As New List(Of DeferredMain)
        Dim MPIDsList = (From x In Me.PrevDeferredPaymentList Select x.IDNumber).ToList()

        Dim GetRFPPayment = (From x In Me.RequestForPayment.RFPDetails
                             Where x.RFPDetailsType = EnumRFPDetailsType.Payment
                             Select x).ToList()

        Dim getRFPPaymentID = (From x In GetRFPPayment Select x.Participant).Distinct.ToList()

        Dim getIDNotInRFP = (From x In MPIDsList Where Not getRFPPaymentID.Contains(x) Select x).Distinct.ToList()

        Dim DPNo As Long = 0
        For Each Item In GetRFPPayment
            Dim GetCollMonitoring As Decimal = (From x In Me.CollectionMonitoring
                                                Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                                                And x.IDNumber.IDNumber = Item.Participant
                                                Select x.Amount).Sum()

            Dim GetTrasToPR As Decimal = (From x In Me.PaymentTransferToPRList
                                          Where x.IDNumber = Item.Participant
                                          Select x.TransferToPrudential).Sum

            Dim GetTrasToFinPen As Decimal = (From x In Me.PaymentTransferToPRList
                                              Where x.IDNumber = Item.Participant
                                              Select x.TransferToFinPen).Sum

            Dim GetShareOnMF As Decimal = (From x In Me.MFwithVATShare
                                           Where x.IDNumber = Item.Participant
                                           Select x.AmountBalance).Sum()

            Dim GetShareOnEnergy As Decimal = ((From x In Me.EnergyShare
                                                Where x.IDNumber = Item.Participant _
                                                And x.PaymentType <> EnumPaymentNewType.DeferredAppliedEnergy
                                                Select x.AmountBalance).Sum() - GetTrasToPR) - GetTrasToFinPen

            Dim GetShareVatOnEnergy As Decimal = (From x In Me.VATonEnergyShare
                                                  Where x.IDNumber = Item.Participant _
                                                  And x.PaymentType <> EnumPaymentNewType.DeferredAppliedVAT
                                                  Select x.AmountBalance).Sum()


            Dim PrevDeferredPayment = (From x In Me.PrevDeferredPaymentList
                                       Where x.IDNumber = Item.Participant
                                       Select x).ToList()

            Dim PrevEnergyDeferredPayment = (From x In Me.EnergyShare
                                             Where x.IDNumber = Item.Participant _
                                             And x.PaymentType = EnumPaymentNewType.DeferredAppliedEnergy
                                             Select x).ToList

            Dim PrevVATDeferredPayment = (From x In Me.VATonEnergyShare
                                          Where x.IDNumber = Item.Participant _
                                          And x.PaymentType = EnumPaymentNewType.DeferredAppliedVAT
                                          Select x).ToList
            If Not PrevDeferredPayment.Count = 0 Then 'This condition is for determine if have previous Deferred Payments
                DPNo += 1
                If Item.PaymentType = EnumParticipantPaymentType.Check _
                    Or Item.PaymentType = EnumParticipantPaymentType.EFT Then

                    Dim GetPreviousEnergyDeferred As PaymentShare = (From x In PrevEnergyDeferredPayment
                                                                     Where x.IDNumber = Item.Participant _
                                                                     And x.ChargeType = EnumChargeType.E
                                                                     Select x).FirstOrDefault

                    Dim GetPreviousEnergyDeferredOrigDate = (From x In PrevDeferredPayment
                                                             Where x.IDNumber = Item.Participant _
                                                             And x.ChargeType = EnumChargeType.E
                                                             Select x).FirstOrDefault

                    If Not GetPreviousEnergyDeferred Is Nothing Then
                        Using _DeferredEnergy As New DeferredMain
                            With _DeferredEnergy
                                .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                .DeferredPaymentNo = DPNo
                                .IDNumber = Item.Participant
                                .OutstandingBalanceDeferredPayment = 0
                                .PaymentNo = 0
                                .DeferredAmount = GetPreviousEnergyDeferred.AmountShare
                                .DeferredType = EnumDeferredType.Remitted
                                .ChargeType = EnumChargeType.E
                                If GetPreviousEnergyDeferredOrigDate Is Nothing Then
                                    .OriginalDate = .AllocationDate
                                Else
                                    .OriginalDate = GetPreviousEnergyDeferredOrigDate.OriginalDate
                                End If
                            End With
                            UpdatedDeferredPayment.Add(_DeferredEnergy)
                        End Using
                    End If


                    Dim GetPreviousVATDeferred As PaymentShare = (From x In PrevVATDeferredPayment
                                                                  Where x.IDNumber = Item.Participant _
                                                                  And x.ChargeType = EnumChargeType.EV
                                                                  Select x).FirstOrDefault

                    Dim GetPreviousVATDeferredOrigDate = (From x In PrevDeferredPayment
                                                          Where x.IDNumber = Item.Participant _
                                                          And x.ChargeType = EnumChargeType.EV
                                                          Select x).FirstOrDefault

                    If Not GetPreviousVATDeferred Is Nothing Then
                        Using _DeferredVAT As New DeferredMain
                            With _DeferredVAT
                                .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                .DeferredPaymentNo = DPNo
                                .IDNumber = Item.Participant
                                .OutstandingBalanceDeferredPayment = 0
                                .PaymentNo = 0
                                .DeferredAmount = GetPreviousVATDeferred.AmountShare
                                .DeferredType = EnumDeferredType.Remitted
                                .ChargeType = EnumChargeType.EV
                                If GetPreviousVATDeferredOrigDate Is Nothing Then
                                    .OriginalDate = .AllocationDate
                                Else
                                    .OriginalDate = GetPreviousVATDeferredOrigDate.OriginalDate
                                End If
                            End With
                            UpdatedDeferredPayment.Add(_DeferredVAT)
                        End Using
                    End If
                ElseIf Item.PaymentType = EnumParticipantPaymentType.SetAsDeferred Then
                    Dim GetPreviousEnergyDeferred As PaymentShare = (From x In PrevEnergyDeferredPayment
                                                                     Where x.IDNumber = Item.Participant _
                                                                     And x.ChargeType = EnumChargeType.E
                                                                     Select x).FirstOrDefault

                    Dim GetPreviousEnergyDeferredOrigDate = (From x In PrevDeferredPayment
                                                             Where x.IDNumber = Item.Participant _
                                                             And x.ChargeType = EnumChargeType.E
                                                             Select x).FirstOrDefault
                    If Not GetPreviousEnergyDeferred Is Nothing Then
                        Using _DeferredEnergy As New DeferredMain
                            Dim TotalEnergyDeferral As Decimal = Math.Round(GetPreviousEnergyDeferred.AmountBalance + GetShareOnEnergy + GetShareOnMF + GetCollMonitoring, 2)
                            Dim GetOffsetEnergy As Decimal = GetPreviousEnergyDeferred.AmountShare - GetPreviousEnergyDeferred.AmountBalance
                            If Math.Round(GetShareOnEnergy + GetShareOnMF + GetCollMonitoring, 2) = 0 Then
                                If GetOffsetEnergy = 0 Then
                                    With _DeferredEnergy
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = GetPreviousEnergyDeferred.AmountShare
                                        .PaymentNo = 0
                                        .DeferredAmount = 0
                                        .DeferredType = EnumDeferredType.OutstandingBalance
                                        .ChargeType = EnumChargeType.E
                                        If GetPreviousEnergyDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousEnergyDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                Else
                                    With _DeferredEnergy
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = GetPreviousEnergyDeferred.AmountBalance
                                        .PaymentNo = 0
                                        .DeferredAmount = GetOffsetEnergy
                                        .DeferredType = EnumDeferredType.Remitted
                                        .ChargeType = EnumChargeType.E
                                        If GetPreviousEnergyDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousEnergyDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                End If
                            Else
                                If TotalEnergyDeferral = 0D Then
                                    With _DeferredEnergy
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = 0
                                        .PaymentNo = 0
                                        .DeferredAmount = GetPreviousEnergyDeferred.AmountShare
                                        .DeferredType = EnumDeferredType.Remitted
                                        .ChargeType = EnumChargeType.E
                                        If GetPreviousEnergyDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousEnergyDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                ElseIf GetPreviousEnergyDeferred.AmountBalance = 0 And (GetShareOnEnergy + GetShareOnMF + GetCollMonitoring) > 0 Then
                                    With _DeferredEnergy
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = TotalEnergyDeferral
                                        .PaymentNo = 0
                                        .DeferredAmount = GetPreviousEnergyDeferred.AmountShare - TotalEnergyDeferral
                                        .DeferredType = EnumDeferredType.Remitted
                                        .ChargeType = EnumChargeType.E
                                        If GetPreviousEnergyDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousEnergyDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                ElseIf GetPreviousEnergyDeferred.AmountBalance > 0 And (GetShareOnEnergy + GetShareOnMF + GetCollMonitoring) > 0 And GetOffsetEnergy > 0 Then
                                    With _DeferredEnergy
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = TotalEnergyDeferral
                                        .PaymentNo = 0
                                        .DeferredAmount = GetOffsetEnergy - (GetShareOnEnergy + GetShareOnMF + GetCollMonitoring)
                                        .DeferredType = EnumDeferredType.Remitted
                                        .ChargeType = EnumChargeType.E
                                        If GetPreviousEnergyDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousEnergyDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                ElseIf GetPreviousEnergyDeferred.AmountBalance > 0 And (GetShareOnEnergy + GetShareOnMF + GetCollMonitoring) = 0 And GetOffsetEnergy = 0 Then
                                    With _DeferredEnergy
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = TotalEnergyDeferral
                                        .PaymentNo = 0
                                        .DeferredAmount = 0
                                        .DeferredType = EnumDeferredType.OutstandingBalance
                                        .ChargeType = EnumChargeType.E
                                        If GetPreviousEnergyDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousEnergyDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                ElseIf GetPreviousEnergyDeferred.AmountBalance > 0 And (GetShareOnEnergy + GetShareOnMF + GetCollMonitoring) > 0 And GetOffsetEnergy = 0 Then
                                    With _DeferredEnergy
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = TotalEnergyDeferral
                                        .PaymentNo = 0
                                        .DeferredAmount = (GetShareOnEnergy + GetShareOnMF + GetCollMonitoring)
                                        .DeferredType = EnumDeferredType.Deferral
                                        .ChargeType = EnumChargeType.E
                                        If GetPreviousEnergyDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousEnergyDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                End If
                            End If
                            UpdatedDeferredPayment.Add(_DeferredEnergy)
                        End Using
                    Else
                        Dim TotalEnergyDeferral As Decimal = Math.Round(GetShareOnEnergy + GetShareOnMF + GetCollMonitoring, 2)
                        If TotalEnergyDeferral <> 0 Then
                            Using _DeferredEnergy As New DeferredMain
                                With _DeferredEnergy
                                    .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                    .DeferredPaymentNo = DPNo
                                    .IDNumber = Item.Participant
                                    .OutstandingBalanceDeferredPayment = Math.Round(TotalEnergyDeferral, 2)
                                    .PaymentNo = 0
                                    .DeferredAmount = TotalEnergyDeferral
                                    .DeferredType = EnumDeferredType.Deferral
                                    .ChargeType = EnumChargeType.E
                                    .OriginalDate = .AllocationDate
                                    If GetShareOnEnergy <> 0 Then
                                        .Remarks = "Energy ;"
                                    End If
                                    If GetShareOnMF <> 0 Then
                                        .Remarks &= "MFShare ;"
                                    End If
                                    If GetCollMonitoring <> 0 Then
                                        .Remarks &= "Excess Collection ;"
                                    End If
                                    If .Remarks.Length <> 0 Then
                                        .Remarks = Trim(Mid(.Remarks, 1, Len(.Remarks) - 1))
                                    End If
                                End With
                                UpdatedDeferredPayment.Add(_DeferredEnergy)
                            End Using
                        End If
                    End If

                    Dim GetPreviousVATDeferred = (From x In PrevVATDeferredPayment
                                                  Where x.IDNumber = Item.Participant _
                                                  And x.ChargeType = EnumChargeType.EV
                                                  Select x).FirstOrDefault

                    Dim GetPreviousVATDeferredOrigDate = (From x In PrevDeferredPayment
                                                          Where x.IDNumber = Item.Participant _
                                                          And x.ChargeType = EnumChargeType.EV
                                                          Select x).FirstOrDefault
                    If Not GetPreviousVATDeferred Is Nothing Then
                        Using _DeferredVAT As New DeferredMain
                            Dim TotalVATDeferral As Decimal = Math.Round(GetPreviousVATDeferred.AmountBalance + GetShareVatOnEnergy, 2)
                            Dim GetOffsetVat As Decimal = GetPreviousVATDeferred.AmountShare - GetPreviousVATDeferred.AmountBalance
                            If Math.Round(GetShareVatOnEnergy, 2) = 0 Then
                                If GetOffsetVat = 0D Then
                                    With _DeferredVAT
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = GetPreviousVATDeferred.AmountShare
                                        .PaymentNo = 0
                                        .DeferredAmount = 0
                                        .DeferredType = EnumDeferredType.OutstandingBalance
                                        .ChargeType = EnumChargeType.EV
                                        If GetPreviousVATDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousVATDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                Else
                                    With _DeferredVAT
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = GetPreviousVATDeferred.AmountBalance
                                        .PaymentNo = 0
                                        .DeferredAmount = GetOffsetVat
                                        .DeferredType = EnumDeferredType.Remitted
                                        .ChargeType = EnumChargeType.EV
                                        If GetPreviousVATDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousVATDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                End If
                            Else
                                If TotalVATDeferral = 0D Then
                                    With _DeferredVAT
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = 0
                                        .PaymentNo = 0
                                        .DeferredAmount = GetPreviousVATDeferred.AmountShare
                                        .DeferredType = EnumDeferredType.Remitted
                                        .ChargeType = EnumChargeType.EV
                                        If GetPreviousVATDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousVATDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                ElseIf GetPreviousVATDeferred.AmountBalance = 0 And GetShareVatOnEnergy > 0 Then
                                    With _DeferredVAT
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = GetShareVatOnEnergy
                                        .PaymentNo = 0
                                        .DeferredAmount = GetPreviousVATDeferred.AmountShare - GetShareVatOnEnergy
                                        .DeferredType = EnumDeferredType.Remitted
                                        .ChargeType = EnumChargeType.EV
                                        If GetPreviousVATDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousVATDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                ElseIf GetPreviousVATDeferred.AmountBalance > 0 And GetShareVatOnEnergy > 0 And GetOffsetVat > 0 Then
                                    With _DeferredVAT
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = TotalVATDeferral
                                        .PaymentNo = 0
                                        .DeferredAmount = GetOffsetVat - GetShareVatOnEnergy
                                        .DeferredType = EnumDeferredType.Remitted
                                        .ChargeType = EnumChargeType.EV
                                        If GetPreviousVATDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousVATDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                ElseIf GetPreviousVATDeferred.AmountBalance > 0 And GetShareVatOnEnergy = 0 And GetOffsetVat = 0 Then
                                    With _DeferredVAT
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = TotalVATDeferral
                                        .PaymentNo = 0
                                        .DeferredAmount = 0
                                        .DeferredType = EnumDeferredType.OutstandingBalance
                                        .ChargeType = EnumChargeType.EV
                                        If GetPreviousVATDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousVATDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                ElseIf GetPreviousVATDeferred.AmountBalance > 0 And GetShareVatOnEnergy > 0 And GetOffsetVat = 0 Then
                                    With _DeferredVAT
                                        .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                        .DeferredPaymentNo = DPNo
                                        .IDNumber = Item.Participant
                                        .OutstandingBalanceDeferredPayment = TotalVATDeferral
                                        .PaymentNo = 0
                                        .DeferredAmount = GetShareVatOnEnergy
                                        .DeferredType = EnumDeferredType.Deferral
                                        .ChargeType = EnumChargeType.EV
                                        If GetPreviousVATDeferredOrigDate Is Nothing Then
                                            .OriginalDate = .AllocationDate
                                        Else
                                            .OriginalDate = GetPreviousVATDeferredOrigDate.OriginalDate
                                        End If
                                    End With
                                End If
                            End If
                            UpdatedDeferredPayment.Add(_DeferredVAT)
                        End Using
                    Else
                        Dim TotalVATDeferral As Decimal = Math.Round(GetShareVatOnEnergy, 2)
                        If TotalVATDeferral <> 0 Then
                            Using _DeferredVAT As New DeferredMain
                                With _DeferredVAT
                                    .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                    .DeferredPaymentNo = DPNo
                                    .IDNumber = Item.Participant
                                    .OutstandingBalanceDeferredPayment = TotalVATDeferral
                                    .PaymentNo = 0
                                    .DeferredAmount = TotalVATDeferral
                                    .DeferredType = EnumDeferredType.Deferral
                                    .ChargeType = EnumChargeType.EV
                                    .OriginalDate = .AllocationDate
                                    .Remarks = "VAT on Energy Share"
                                End With
                                UpdatedDeferredPayment.Add(_DeferredVAT)
                            End Using
                        End If
                    End If
                End If
            Else
                If Item.PaymentType = EnumParticipantPaymentType.SetAsDeferred Then
                    DPNo += 1
                    Dim TotalEnergyDeferral As Decimal = Math.Round(GetShareOnEnergy + GetShareOnMF + GetCollMonitoring, 2)
                    If TotalEnergyDeferral <> 0 Then
                        Using _DeferredEnergy As New DeferredMain
                            With _DeferredEnergy
                                .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                .DeferredPaymentNo = DPNo
                                .IDNumber = Item.Participant
                                .OutstandingBalanceDeferredPayment = Math.Round(TotalEnergyDeferral, 2)
                                .PaymentNo = 0
                                .DeferredAmount = TotalEnergyDeferral
                                .DeferredType = EnumDeferredType.Deferral
                                .ChargeType = EnumChargeType.E
                                .OriginalDate = .AllocationDate
                                If GetShareOnEnergy <> 0 Then
                                    .Remarks = "Energy ;"
                                End If
                                If GetShareOnMF <> 0 Then
                                    .Remarks &= "MFShare ;"
                                End If
                                If GetCollMonitoring <> 0 Then
                                    .Remarks &= "Excess Collection ;"
                                End If
                                If .Remarks.Length <> 0 Then
                                    .Remarks = Trim(Mid(.Remarks, 1, Len(.Remarks) - 1))
                                End If
                            End With
                            UpdatedDeferredPayment.Add(_DeferredEnergy)
                        End Using
                    End If

                    Dim TotalVATDeferral As Decimal = Math.Round(GetShareVatOnEnergy, 2)
                    If TotalVATDeferral <> 0 Then
                        Using _DeferredVAT As New DeferredMain
                            With _DeferredVAT
                                .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                .DeferredPaymentNo = DPNo
                                .IDNumber = Item.Participant
                                .OutstandingBalanceDeferredPayment = TotalVATDeferral
                                .PaymentNo = 0
                                .DeferredAmount = TotalVATDeferral
                                .DeferredType = EnumDeferredType.Deferral
                                .ChargeType = EnumChargeType.EV
                                .OriginalDate = .AllocationDate
                                .Remarks = "VAT on Energy Share"
                            End With
                            UpdatedDeferredPayment.Add(_DeferredVAT)
                        End Using
                    End If

                End If
            End If
        Next

        If getIDNotInRFP.Count <> 0 Then
            For Each item In getIDNotInRFP
                Dim PrevDeferredPayment = (From x In Me.PrevDeferredPaymentList
                                           Where x.IDNumber = item
                                           Select x).ToList()


                Dim PrevEnergyDeferredPayment = (From x In Me.EnergyShare
                                                 Where x.IDNumber = item _
                                                 And x.PaymentType = EnumPaymentNewType.DeferredAppliedEnergy
                                                 Select x).ToList

                Dim PrevVATDeferredPayment = (From x In Me.VATonEnergyShare
                                              Where x.IDNumber = item _
                                              And x.PaymentType = EnumPaymentNewType.DeferredAppliedVAT
                                              Select x).ToList

                Dim GetPreviousEnergyDeferred As PaymentShare = (From x In PrevEnergyDeferredPayment
                                                                 Where x.IDNumber = item _
                                                                 And x.ChargeType = EnumChargeType.E
                                                                 Select x).FirstOrDefault

                Dim GetPreviousEnergyDeferredOrigDate = (From x In PrevDeferredPayment
                                                         Where x.IDNumber = item _
                                                         And x.ChargeType = EnumChargeType.E
                                                         Select x).FirstOrDefault

                If Not GetPreviousEnergyDeferred Is Nothing Then
                    If GetPreviousEnergyDeferred.AmountBalance = 0 Then
                        Using _DeferredEnergy As New DeferredMain
                            With _DeferredEnergy
                                .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                .DeferredPaymentNo = DPNo
                                .IDNumber = item
                                .OutstandingBalanceDeferredPayment = 0
                                .PaymentNo = 0
                                .DeferredAmount = GetPreviousEnergyDeferred.AmountShare
                                .DeferredType = EnumDeferredType.Remitted
                                .ChargeType = EnumChargeType.E
                                If GetPreviousEnergyDeferredOrigDate Is Nothing Then
                                    .OriginalDate = .AllocationDate
                                Else
                                    .OriginalDate = GetPreviousEnergyDeferredOrigDate.OriginalDate
                                End If
                            End With
                            UpdatedDeferredPayment.Add(_DeferredEnergy)
                        End Using
                    End If
                End If

                Dim GetPreviousVATDeferred = (From x In PrevVATDeferredPayment
                                              Where x.IDNumber = item _
                                              And x.ChargeType = EnumChargeType.EV
                                              Select x).FirstOrDefault

                Dim GetPreviousVATDeferredOrigDate = (From x In PrevDeferredPayment
                                                      Where x.IDNumber = item _
                                                      And x.ChargeType = EnumChargeType.EV
                                                      Select x).FirstOrDefault

                If Not GetPreviousVATDeferred Is Nothing Then
                    If GetPreviousVATDeferred.AmountBalance = 0 Then
                        Using _DeferredVAT As New DeferredMain
                            With _DeferredVAT
                                .AllocationDate = CDate(Me.PayAllocDate.RemittanceDate.ToShortDateString) 'Item.AllocationDate
                                .DeferredPaymentNo = DPNo
                                .IDNumber = item
                                .OutstandingBalanceDeferredPayment = 0
                                .PaymentNo = 0
                                .DeferredAmount = GetPreviousVATDeferred.AmountShare
                                .DeferredType = EnumDeferredType.Remitted
                                .ChargeType = EnumChargeType.EV
                                If GetPreviousVATDeferredOrigDate Is Nothing Then
                                    .OriginalDate = .AllocationDate
                                Else
                                    .OriginalDate = GetPreviousVATDeferredOrigDate.OriginalDate
                                End If
                            End With
                            UpdatedDeferredPayment.Add(_DeferredVAT)
                        End Using
                    End If
                End If
            Next
        End If

        Me.CurrentDeferredPaymentList = UpdatedDeferredPayment
    End Sub
#End Region

#Region "Function for OR Summary Report"
    Public Function GenerateORSummary(ByVal DT As DataTable) As DataTable
        Dim ORListCreated As List(Of OfficialReceiptMain) = (From x In Me.ORofOffsettingMFwithVAT Select x).Union _
                                                            (From x In Me.ORofFinPen Select x).ToList()
        For Each itmOR In ORListCreated
            Dim dr As DataRow
            dr = DT.NewRow

            With itmOR
                dr("OR_NO") = Me.BFactory.GenerateBIRDocumentNumber(.ORNo, BIRDocumentsType.OfficialReceipt)
                dr("ID_NUMBER") = .IDNumber
                dr("PARTICIPANT_ID") = (From x In Me.AMParticipants
                                        Where x.IDNumber = .IDNumber
                                        Select x.ParticipantID).FirstOrDefault
                dr("AMOUNT") = Math.Abs(.Amount)
                dr("TYPE") = .TransactionType.ToString
                dr("DATE_ALLOCATED") = FormatDateTime(Me.PayAllocDate.CollAllocationDate, DateFormat.ShortDate)
            End With
            DT.Rows.Add(dr)
            DT.AcceptChanges()
        Next
        Return DT
    End Function
#End Region

#Region "Method and Function to Generate PaymentORForMarketFeesOffsetting"

    Public Function GenerateORReport(ByVal DT As DataTable, ByVal IDNumber As String) As DataTable
        Try
            Dim itemORReportMain As New OfficialReceiptReportMain
            With itemORReportMain

                .ItemOfficialReceipt = (From x In Me.ORofOffsettingMFwithVAT
                                        Where x.IDNumber = IDNumber
                                        Select x).FirstOrDefault

                For Each item In .ItemOfficialReceipt.ListORSummary
                    Dim selectedItem = item
                    Dim mfAmount As Decimal = 0, vmfAmount As Decimal = 0, wtaxAmount As Decimal = 0, wvatAmount As Decimal = 0

                    Select Case item.CollectionType
                        Case EnumCollectionType.WithholdingTaxOnMF, EnumCollectionType.WithholdingTaxOnDefaultInterest
                            wtaxAmount = item.Amount

                        Case EnumCollectionType.WithholdingVatOnMF, EnumCollectionType.WithholdingVatOnDefaultInterest
                            wvatAmount = item.Amount

                        Case EnumCollectionType.MarketFees, EnumCollectionType.DefaultInterestOnMF
                            mfAmount = item.Amount

                        Case EnumCollectionType.VatOnMarketFees, EnumCollectionType.DefaultInterestOnVatOnMF
                            vmfAmount = item.Amount

                    End Select

                    .ListOfficialReceiptReportRawDetails.AddRange((From w In Me.WESMBillSummaryList
                                                                   Join z In Me.WESMBillList On w.INVDMCMNo Equals z.InvoiceNumber And w.ChargeType Equals z.ChargeType
                                                                   Where w.WESMBillSummaryNo = selectedItem.WESMBillSummaryNo
                                                                   Select New OfficialReceiptReportRawDetailsNew _
                                                                          With {.ORNo = selectedItem.ORNo, .DocumentNo = w.INVDMCMNo, .DocumentType = EnumDocumentType.INV,
                                                                              .DocumentDate = z.InvoiceDate, .WESMBillSummaryNo = w.WESMBillSummaryNo,
                                                                              .DueDate = w.DueDate, .Amount = mfAmount, .Vat = vmfAmount,
                                                                              .DefaultInterest = 0,
                                                                              .WithHoldingTax = wtaxAmount,
                                                                              .WithHoldingVat = wvatAmount,
                                                                              .TransactionType = EnumORTransactionType.MarketFees}).ToList())
                Next

                .ItemParticipant = (From x In Me.AMParticipants Where x.IDNumber = IDNumber).FirstOrDefault

                .DefaultInterestRate = WBillHelper.GetDailyInterestRate()(itemORReportMain.ItemOfficialReceipt.ORDate)
                .BIRPermitNumber = AMModule.BIRPermitNumber
                Dim TotalPayment As Decimal = (From x In .ListOfficialReceiptReportRawDetails Select x.Amount).Sum()
                .TotalPaymentInWords = BFactory.NumberConvert(TotalPayment)
            End With

            Dim ORPEMC_Header As Integer = EnumHeaderType.Yes

            DT = BFactory.GenerateOfficialReceiptReport(ORPEMC_Header, itemORReportMain, itemORReportMain.ItemParticipant, DT)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return DT
    End Function

    Private Sub GeneratePaymentORFromList()
        Dim DistinctItemUsingIDNo = (From x In Me.OffsettingMFwithVATCollectionList Select x.IDNumber).Distinct.ToList()

        For Each item In DistinctItemUsingIDNo
            Dim GetItemList = (From x In Me.OffsettingMFwithVATCollectionList Where x.IDNumber = item Select x).ToList()
            Dim CreatedOR = Me.GeneratePaymentOR(GetItemList)
            Me.ORofOffsettingMFwithVAT.Add(CreatedOR)
        Next
        Me.ORofOffsettingMFwithVAT.TrimExcess()
    End Sub

    Public Function GeneratePaymentOR(ByVal ListOffsettingCollectionsOnMFwithVAT As List(Of ARCollection)) As OfficialReceiptMain
        Dim result As New OfficialReceiptMain

        Dim listORSummary As New List(Of OfficialReceiptSummary)
        Dim listORDetails As New List(Of OfficialReceiptDetails)
        Dim totalAR As Decimal = 0, totalPEMC As Decimal = 0
        Dim VatExempt As Decimal = 0, Vatable As Decimal = 0, VAT As Decimal = 0
        Dim VatZeroRated As Decimal = 0, Others As Decimal = 0, WithholdTax As Decimal = 0, WithholdVat As Decimal = 0
        Dim TotalOffsettingCollectedAmount As Decimal = 0

        _ORNumber += 1
        For Each item In ListOffsettingCollectionsOnMFwithVAT
            Dim Amount As Decimal = 0
            Select Case item.CollectionType
                Case EnumCollectionType.WithholdingTaxOnMF, EnumCollectionType.WithholdingTaxOnDefaultInterest
                    WithholdTax += item.AllocationAmount * -1D
                    Amount = item.AllocationAmount * -1D
                Case EnumCollectionType.WithholdingVatOnMF, EnumCollectionType.WithholdingVatOnDefaultInterest
                    WithholdVat += item.AllocationAmount * -1D
                    Amount = item.AllocationAmount * -1D
                Case EnumCollectionType.DefaultInterestOnEnergy
                    Others += Math.Abs(item.AllocationAmount)
                    Amount = Math.Abs(item.AllocationAmount)
                Case EnumCollectionType.MarketFees, EnumCollectionType.DefaultInterestOnMF
                    Dim getParticipantInfo = (From x In Me.AMParticipants Where x.IDNumber = item.IDNumber Select x).FirstOrDefault
                    If Not getParticipantInfo Is Nothing Then
                        If getParticipantInfo.ZeroRatedMarketFees = True Then
                            VatZeroRated += Math.Abs(item.AllocationAmount)
                            Amount = Math.Abs(item.AllocationAmount)
                        Else
                            Vatable += Math.Abs(item.AllocationAmount)
                            Amount = Math.Abs(item.AllocationAmount)
                        End If
                    End If
                Case EnumCollectionType.VatOnMarketFees, EnumCollectionType.DefaultInterestOnVatOnMF
                    VAT += Math.Abs(item.AllocationAmount)
                    Amount = Math.Abs(item.AllocationAmount)
            End Select

            Dim CheckListORSummary = (From x In listORSummary
                                      Where x.WESMBillSummaryNo = item.WESMBillSummaryNo _
                                      And x.CollectionType = item.CollectionType
                                      Select x).FirstOrDefault

            If CheckListORSummary Is Nothing Then
                listORSummary.Add(New OfficialReceiptSummary(_ORNumber, item.WESMBillSummaryNo, item.DueDate,
                                                             Amount, item.CollectionType))
            Else
                CheckListORSummary.Amount += Amount
            End If
            TotalOffsettingCollectedAmount += Amount
        Next

        'For Offsetting Cash
        listORDetails.Add(New OfficialReceiptDetails(_ORNumber, AMModule.ClearingAccountCode,
                                                     "Clearing", TotalOffsettingCollectedAmount, 0))

        'For Clearing AR-WESM
        listORDetails.Add(New OfficialReceiptDetails(_ORNumber, AMModule.CreditCode,
                                                     "AR-WESM", 0, TotalOffsettingCollectedAmount))

        Dim MPInfo = (From x In ListOffsettingCollectionsOnMFwithVAT Select x).FirstOrDefault
        Dim GetJVPaymentAllocBatchCode As String = (From x In Me.JournalVoucherList
                                                    Where x.PostedType = EnumPostedType.PA.ToString
                                                    Select x.BatchCode).FirstOrDefault
        If GetJVPaymentAllocBatchCode Is Nothing Then
            GetJVPaymentAllocBatchCode = ""
        End If

        Dim CreatedOR As OfficialReceiptMain = New OfficialReceiptMain(_ORNumber, MPInfo.AllocationDate, MPInfo.IDNumber, TotalOffsettingCollectedAmount,
                                                                       1, listORDetails, listORSummary, GetJVPaymentAllocBatchCode, "", EnumORTransactionType.MarketFees, VatExempt, Vatable,
                                                                       VAT, VatZeroRated, Others, WithholdTax, WithholdVat)
        result = CreatedOR

        Return result
    End Function
#End Region

#Region "Saving Function"
    Public Sub SavePaymentProcess(ByVal progress As IProgress(Of ProgressClass), ByVal ct As CancellationToken)
        Try
            Using PymtSaveNew As New PaymentSaveHelper
                With PymtSaveNew
                    ._objDAL = DataAccess
                    ._objWBillHelper = WBillHelper
                    ._AllocationDate = Me.PayAllocDate
                    ._AMPaymentNewAP = (From x In Me.EnergyAllocationList Select x).Union _
                                       (From x In Me.VATonEnergyAllocationList Select x).Union _
                                       (From x In Me.MFwithVATAllocationList Select x).ToList()

                    ._AMPaymentNewOffsettingAP = (From x In Me.OffsettingEnergyAllocationList Select x).Union _
                                                (From x In Me.OffsettingVATonEnergyAllocationList Select x).Union _
                                                (From x In Me.OffsettingMFwithVATAllocationList Select x).ToList()

                    ._AMPaymentNewOffsettingAR = (From x In Me.OffsettingEnergyCollectionList Select x).Union _
                                                 (From x In Me.OffsettingVATonEnergyCollectionList Select x).Union _
                                                 (From x In Me.OffsettingMFwithVATCollectionList Select x).ToList()
                    ._PaymentDMCMList = Me.ListofDMCM
                    ._UpdatedWESMBillSummaryList = (From x In Me.WESMBillSummaryList Where x.StatusUpdate = True Select x).ToList()
                    ._WESMBillSummaryBalance = Me.PymntWBSHistoryBalance
                    ._AMPaymentTransferToPR = Me.PaymentTransferToPRList
                    ._PaymentFTF = Me.FundTransferform
                    ._CollectionFTF = Me.CollFundTransferform
                    ._NewPaymentDeferred = Me.CurrentDeferredPaymentList
                    ._PaymentEFT = Me.EFTSummaryReportList
                    ._AMPaymentShare = (From x In Me.MFwithVATShare Select x).Union _
                                       (From x In Me.EnergyShare Select x).Union _
                                       (From x In Me.VATonEnergyShare Select x).ToList
                    ._JournalVoucherList = Me.JournalVoucherList
                    ._RFPList = Me.RequestForPayment
                    ._PrudentialHistoryList = Me.NewPrudentialHistory

                    ._ORList = (From x In Me.ORofOffsettingMFwithVAT Select x).Union _
                                (From x In Me.ORofFinPen Select x).ToList()

                    ._WESMBillSummaryNoTransAR = (From x In Me.MFwithVATNoTransARList Select x).Union _
                                                 (From x In Me.EnergyNoTransactionARList Select x).Union _
                                                 (From x In Me.VATNoTransactionARList Select x).ToList()

                    ._WESMBillSummaryNoTransAP = (From x In Me.MFwithVATNoTransAPList Select x).Union _
                                                 (From x In Me.EnergyNoTransactionAPList Select x).Union _
                                                 (From x In Me.VATNoTransactionAPList Select x).ToList()

                    ._AMCollectionAR = (From x In Me.EnergyListCollection Select x).Union _
                                       (From x In Me.VATonEnergyCollectionList Select x).Union _
                                       (From x In Me.MFwithVATCollectionList Select x).ToList()
                    ._WESMTransDetailsSummaryList = Me.WESMTransDetailsSummaryList
                    ._WESMTransDetailsSummaryHistoryList = Me.WESMTransDetailsSummaryHistoryList
                    ._UpdatedWESMBillSummaryWHTAXAdjList = WESMBillSummaryListWithWHTAXAdj
                    .SaveToDB(progress, ct)
                End With
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "Method for adjusting of Outstanding balance of WESM Bill Summaries that have a withholding Tax Adjustment"
    Private Sub AdjustWESMBillSummaryForWHTAXADJ()
        Dim getWBSummaryWithWHTaxAdj As List(Of WESMBillSummary) = (From x In Me.WESMBillSummaryList Where x.INVDMCMNo.Contains("DMCM") Select x).ToList
        For Each item In getWBSummaryWithWHTaxAdj
            Dim getParticipantInfo As AMParticipants = (From x In Me.AMParticipants Where x.IDNumber = item.IDNumber.IDNumber Select x).FirstOrDefault
            Dim lenCount As Integer = If(item.INVDMCMNo.IndexOf("INV") > 0, 10, 11)
            Dim extractInvNo As String = item.INVDMCMNo.Substring(item.INVDMCMNo.IndexOf(AMModule.FSNumberPrefix.ToString) + item.INVDMCMNo.IndexOf("INV") + 1, lenCount)
            Dim getTotalOffset As Decimal = 0D
            If item.BeginningBalance >= 0 Then
                getTotalOffset = (From x In Me.OffsettingEnergyAllocationList Where x.WESMBillSummaryNo = item.WESMBillSummaryNo Select x.AllocationAmount).Sum()
            Else
                getTotalOffset = (From x In Me.OffsettingEnergyCollectionList Where x.WESMBillSummaryNo = item.WESMBillSummaryNo Select x.AllocationAmount).Sum()
            End If
            Select Case item.ChargeType
                Case EnumChargeType.E
                    Dim GetActualInv As WESMBillSummary = (From x In Me.WESMBillSummaryList Where x.INVDMCMNo = extractInvNo And x.ChargeType = item.ChargeType Select x).FirstOrDefault
                    If GetActualInv Is Nothing Then
                        Throw New Exception("WESM Bill No." & extractInvNo & " not found in WESM Bill Summary List")
                    Else
                        WESMBillSummaryListWithWHTAXAdj.Add(GetActualInv)
                        GetActualInv.EnergyWithhold += (getTotalOffset * -1)
                        GetActualInv.EndingBalance += (getTotalOffset * -1)
                        GetActualInv.StatusUpdate = True
                        Dim CreateDMCMToAddBackBalance As New DebitCreditMemo
                        If getTotalOffset > 0 Then
                            CreateDMCMToAddBackBalance = Me.PaymentProformaEntries.CreateARDMCM_WHTax_Toaddback(item, getTotalOffset, getParticipantInfo, Me.PayAllocDate.CollAllocationDate)
                            Me.ListofDMCM.Add(CreateDMCMToAddBackBalance)
                        ElseIf getTotalOffset < 0 Then
                            CreateDMCMToAddBackBalance = Me.PaymentProformaEntries.CreateAPDMCM_WHTax_Toaddback(item, getTotalOffset, getParticipantInfo, Me.PayAllocDate.CollAllocationDate)
                            Me.ListofDMCM.Add(CreateDMCMToAddBackBalance)
                        End If
                    End If
            End Select
        Next
    End Sub
    Private Sub AdjustWESMBillSummaryForWHTAXADJ2()
        Dim getWBSummaryWithWHTaxAdj As List(Of WESMBillSummary) = (From x In Me.WESMBillSummaryList Where x.INVDMCMNo.Contains("DMCM") Select x).ToList
        For Each item In getWBSummaryWithWHTaxAdj
            Dim getParticipantInfo As AMParticipants = (From x In Me.AMParticipants Where x.IDNumber = item.IDNumber.IDNumber Select x).FirstOrDefault
            Dim lenCount As Integer = If(item.INVDMCMNo.IndexOf("INV") > 0, 10, 11)
            Dim extractInvNo As String = item.INVDMCMNo.Substring(item.INVDMCMNo.IndexOf(AMModule.FSNumberPrefix.ToString) + item.INVDMCMNo.IndexOf("INV") + 1, lenCount)
            Dim getTotalOffset As Decimal = 0D
            If item.BeginningBalance >= 0 Then
                getTotalOffset = (From x In Me.OffsettingEnergyAllocationList Where x.WESMBillSummaryNo = item.WESMBillSummaryNo Select x.AllocationAmount).Sum()
            Else
                getTotalOffset = (From x In Me.OffsettingEnergyCollectionList Where x.WESMBillSummaryNo = item.WESMBillSummaryNo Select x.AllocationAmount).Sum()
            End If
            Select Case item.ChargeType
                Case EnumChargeType.E
                    Dim GetARCollecNoTrans As ARCollection = (From x In Me._EnergyNoTransactionARList Where x.InvoiceNumber = extractInvNo Select x).FirstOrDefault
                    If Not GetARCollecNoTrans Is Nothing Then
                        GetARCollecNoTrans.EnergyWithHold += (getTotalOffset * -1)
                        GetARCollecNoTrans.NewEndingBalance += (getTotalOffset * -1)
                    End If

                    Dim GetAPAllocNoTrans As APAllocation = (From x In Me._EnergyNoTransactionAPList Where x.InvoiceNumber = extractInvNo Select x).FirstOrDefault
                    If Not GetAPAllocNoTrans Is Nothing Then
                        GetAPAllocNoTrans.EnergyWithHold += (getTotalOffset * -1)
                        GetAPAllocNoTrans.NewEndingBalance += (getTotalOffset * -1)
                    End If

                    Dim GetARCollec As ARCollection = (From x In Me._EnergyListCollection Where x.InvoiceNumber = extractInvNo Select x).FirstOrDefault
                    If Not GetARCollec Is Nothing Then
                        GetARCollec.EnergyWithHold += (getTotalOffset * -1)
                        GetARCollec.NewEndingBalance += (getTotalOffset * -1)
                    End If

                    Dim GetAPAlloc As APAllocation = (From x In Me._EnergyAllocationList Where x.InvoiceNumber = extractInvNo Select x).FirstOrDefault
                    If Not GetAPAlloc Is Nothing Then
                        GetAPAlloc.EnergyWithHold += (getTotalOffset * -1)
                        GetAPAlloc.NewEndingBalance += (getTotalOffset * -1)
                    End If

                    Dim GetOffsetAPAlloc As List(Of APAllocation) = (From x In Me._OffsettingEnergyAllocationList Where x.InvoiceNumber = extractInvNo Select x).ToList
                    For Each itemAP In GetOffsetAPAlloc
                        If Not itemAP Is Nothing Then
                            itemAP.EnergyWithHold += (getTotalOffset * -1)
                            itemAP.NewEndingBalance += (getTotalOffset * -1)
                        End If
                    Next
            End Select
        Next
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                Me.ClearObjects()
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
