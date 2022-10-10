Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementDataAccess
Public Class WBSAdjOnWTAXHelper : Implements IDisposable

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

#Region "Variables and Initialization Methods"
    Public Sub New()
        Me.Initialize()
    End Sub
    Public Sub Initialize()
        'Get the current instance of the dal
        Me._DataAccess = DAL.GetInstance()
        Me._WBillHelper = WESMBillHelper.GetInstance
        Me._BFactory = BusinessFactory.GetInstance
        Me._ListOfWESMBillNo = Me.WBillHelper.GetListofBillingPeriodNo()
        Me._DMCMSign = WBillHelper.GetSignatories("DMCM").First()
        Me._AMParticipants = WBillHelper.GetAMParticipants()
        Me._WBSAmountAdjustmentWhTaxList = Me.getListWBSAmountAdjustmentInWHTAX()
        Me.ResetObject()
    End Sub
    Public Sub ResetObject()
        Me.DMCMSeqNo = 0
        Me.JVSeqNo = 0
        Me.FTFSeqNo = 0
        Me._FundTransferForm = New List(Of FundTransferFormMain)
        Me._ListofDMCM = New List(Of DebitCreditMemo)
        Me._FundTransferForm = New List(Of FundTransferFormMain)
        Me._JournalVoucherSetup = New JournalVoucher
    End Sub

    Public Sub ResetObjectForViewing()
        Me._WBSAmountAdjustmentWhTax = New WBSAmountAdjustmentInWHTAX
        Me._FundTransferForm = New List(Of FundTransferFormMain)
        Me._ListofDMCM = New List(Of DebitCreditMemo)
        Me._FundTransferForm = New List(Of FundTransferFormMain)
        Me._JournalVoucherSetup = New JournalVoucher
    End Sub

    Public Sub RefreshList()
        Me._WBSAmountAdjustmentWhTaxList = Me.getListWBSAmountAdjustmentInWHTAX()
    End Sub
    Private DMCMSeqNo As Long = 0
    Private JVSeqNo As Long = 0
    Private FTFSeqNo As Long = 0
#End Region

#Region "WESM Bill Summary"
    Private _ListOfWESMBillSummary As New List(Of WESMBillSummary)
    Public ReadOnly Property ListOfWESMBillSummary() As List(Of WESMBillSummary)
        Get
            Return _ListOfWESMBillSummary
        End Get
    End Property
#End Region

#Region "WESM BILL No List"
    Private _ListOfWESMBillNo As New List(Of Integer)
    Public ReadOnly Property ListOfWESMBillNo() As List(Of Integer)
        Get
            Return _ListOfWESMBillNo
        End Get
    End Property
#End Region

#Region "WESMBILLSummaryAmountAdjustment"
    Private _WBSAmountAdjustmentWhTax As New WBSAmountAdjustmentInWHTAX
    Public ReadOnly Property WBSAmountAdjustmentWhTax() As WBSAmountAdjustmentInWHTAX
        Get
            Return _WBSAmountAdjustmentWhTax
        End Get
    End Property
#End Region

#Region "WESMBILLSummaryAmountAdjustmentList"
    Private _WBSAmountAdjustmentWhTaxList As New List(Of WBSAmountAdjustmentInWHTAX)
    Public ReadOnly Property WBSAmountAdjustmentWhTaxList() As List(Of WBSAmountAdjustmentInWHTAX)
        Get
            Return _WBSAmountAdjustmentWhTaxList
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

#Region "DMCM Signatories"
    Private _DMCMSign As New DocSignatories
    Public Property DMCMSign() As DocSignatories
        Get
            Return _DMCMSign
        End Get
        Set(value As DocSignatories)
            _DMCMSign = value
        End Set
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

#Region "Property of FTF"
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

#Region "Property of Journal Voucher List "
    Private _JournalVoucherSetup As JournalVoucher
    Public Property JournalVoucherSetup() As JournalVoucher
        Get
            Return _JournalVoucherSetup
        End Get
        Set(ByVal value As JournalVoucher)
            _JournalVoucherSetup = value
        End Set
    End Property
#End Region

#Region "Get WESMBILLSUMMARY using BPNO"
    Public Sub GetWESMBillSummaryUsingBPNo(ByVal BPNo As Integer, ByVal chargetype As EnumChargeType)
        Me._ListOfWESMBillSummary = WBillHelper.GetWESMBillSummary(BPNo, chargetype)
    End Sub
#End Region

#Region "Main Methods of Helper"
    Public Sub GetSelectedWBSAmountAdjustedWhTax(ByVal refid As Long)
        Me.ResetObjectForViewing()
        Dim getWBSAmountAdjustedWhTax As WBSAmountAdjustmentInWHTAX = (From x In Me.WBSAmountAdjustmentWhTaxList Where x.ReferenceID = refid Select x).FirstOrDefault
        Me._WBSAmountAdjustmentWhTax = getWBSAmountAdjustedWhTax
        Me._WBSAmountAdjustmentWhTax.WBSAmountAdjWHTAXDetailsList = Me.getListWBSAmountAdjustmentInWHTAXDetails(refid)
        Me._WBSAmountAdjustmentWhTax.WBSDummiesList = Me.getListWBSAmountAdjustmentInWHTAXMon(refid)
        Me._JournalVoucherSetup = WBillHelper.GetJournalVoucher(Me.WBSAmountAdjustmentWhTax.JVSetupNo).FirstOrDefault
        Me._ListofDMCM = WBillHelper.GetDebitCreditMemoMainFromJV(Me.JournalVoucherSetup.JVNumber)
        Me._FundTransferForm = WBillHelper.GetFundTransferForm(Me.JournalVoucherSetup.BatchCode)
    End Sub

    Public Sub GetWESMBillSummaryAdjustment(ByVal iniWBSAmountAdj As WBSAmountAdjustmentInWHTAX, ByVal getListofInvoices As Dictionary(Of String, Decimal))
        Me.DMCMSeqNo = 0
        Me.JVSeqNo = 0
        Me.FTFSeqNo = 0
        Me._FundTransferForm = New List(Of FundTransferFormMain)
        Me._JournalVoucherSetup = New JournalVoucher
        Me._ListofDMCM = New List(Of DebitCreditMemo)

        Me._WBSAmountAdjustmentWhTax = iniWBSAmountAdj
        Dim getListofWESMBillSummary As List(Of WESMBillSummary) = (From x In Me.ListOfWESMBillSummary _
                                                                    Where x.WESMBillBatchNo = Me.WBSAmountAdjustmentWhTax.BillingBatchNo And x.ChargeType = Me.WBSAmountAdjustmentWhTax.ChargeType _
                                                                    Select x).ToList()
        Dim finalListofWBSInvolved As New List(Of WBSAmountAdjustmentWHTAXDetails)
        If Me.WBSAmountAdjustmentWhTax.ChargeType = EnumChargeType.E Then
            For Each item In getListofInvoices
                Dim newWBSAmountAdjDetails As New WBSAmountAdjustmentWHTAXDetails
                Dim getWBSummaryitem As WESMBillSummary = (From x In getListofWESMBillSummary Where x.INVDMCMNo = item.Key And x.ChargeType = iniWBSAmountAdj.ChargeType Select x).FirstOrDefault
                Dim createDMCM As New DebitCreditMemo
                If iniWBSAmountAdj.ChargeType = EnumChargeType.E And iniWBSAmountAdj.BillingType = EnumBalanceType.AR Then
                    createDMCM = CreateDMCM_AP(getWBSummaryitem, Math.Abs(item.Value))
                ElseIf iniWBSAmountAdj.ChargeType = EnumChargeType.E And iniWBSAmountAdj.BillingType = EnumBalanceType.AP Then
                    createDMCM = CreateDMCM_AR(getWBSummaryitem, Math.Abs(item.Value))
                End If
                Me.ListofDMCM.Add(createDMCM)
                With newWBSAmountAdjDetails
                    .WBSDetails = getWBSummaryitem
                    .AmountAdjusted = item.Value
                    .FTFDMCMDoc = BFactory.GenerateBIRDocumentNumber(createDMCM.DMCMNumber, BIRDocumentsType.DMCM)
                    .CreatedDocumentNo = createDMCM.DMCMNumber
                    .CreatedDocumentType = BIRDocumentsType.DMCM
                End With
                finalListofWBSInvolved.Add(newWBSAmountAdjDetails)
            Next
        Else
            Dim initialMFARListofWBSInvolved As New List(Of WBSAmountAdjustmentWHTAXDetails)
            Dim initialMFAPListofWBSInvolved As New List(Of WBSAmountAdjustmentWHTAXDetails)

            For Each item In getListofInvoices
                Dim newWBSAmountAdjDetails As New WBSAmountAdjustmentWHTAXDetails
                Dim getWBSummaryitem As WESMBillSummary = (From x In getListofWESMBillSummary Where x.INVDMCMNo = item.Key And x.ChargeType = iniWBSAmountAdj.ChargeType Select x).FirstOrDefault
                With newWBSAmountAdjDetails
                    .WBSDetails = getWBSummaryitem
                    .AmountAdjusted = item.Value
                    .FTFDMCMDoc = "N/A"
                End With
                If item.Value < 0 Then
                    initialMFARListofWBSInvolved.Add(newWBSAmountAdjDetails)
                Else
                    initialMFAPListofWBSInvolved.Add(newWBSAmountAdjDetails)
                End If
            Next
            If initialMFARListofWBSInvolved.Count <> 0 Then
                Dim getFTFNo As Long = Me.CreateFTFForMFAR(initialMFARListofWBSInvolved)
                For Each item In initialMFARListofWBSInvolved
                    With item
                        .FTFDMCMDoc = BFactory.GenerateBIRDocumentNumber(getFTFNo, BIRDocumentsType.FTF)
                        .CreatedDocumentNo = getFTFNo
                        .CreatedDocumentType = BIRDocumentsType.FTF
                    End With
                    finalListofWBSInvolved.Add(item)
                Next
            End If
            If initialMFAPListofWBSInvolved.Count <> 0 Then
                Dim getFTFNo As Long = Me.CreateFTFForMFAR(initialMFAPListofWBSInvolved)
                For Each item In initialMFAPListofWBSInvolved
                    With item
                        .FTFDMCMDoc = BFactory.GenerateBIRDocumentNumber(getFTFNo, BIRDocumentsType.FTF)
                        .CreatedDocumentNo = getFTFNo
                        .CreatedDocumentType = BIRDocumentsType.FTF
                    End With
                    finalListofWBSInvolved.Add(item)
                Next
            End If
        End If

        Dim getCountePartBill As New List(Of WESMBillSummary)

        If Me.WBSAmountAdjustmentWhTax.BillingType = EnumBalanceType.AR Then
            getCountePartBill = (From x In getListofWESMBillSummary Where x.BeginningBalance > 0 Select x).ToList
        ElseIf Me.WBSAmountAdjustmentWhTax.BillingType = EnumBalanceType.AP Then
            getCountePartBill = (From x In getListofWESMBillSummary Where x.BeginningBalance < 0 Select x).ToList
        End If

        Dim initialListofWBSInvolved As New List(Of WBSAmountAdjustmentWHTAXDetails)
        If getCountePartBill.Count <> 0 Then
            Dim BPTotalBalance As Decimal = (From x In getCountePartBill Select x.BeginningBalance).Sum()
            Dim getTotalAdjustment As Decimal = 0D
            For Each item In getCountePartBill
                Dim ShareOnAmountAdj = Me.ComputeAllocation(item.BeginningBalance, BPTotalBalance, Me.WBSAmountAdjustmentWhTax.TotalAPAmountAdjusted) * -1
                Dim newWBSAmountAdjDetails As New WBSAmountAdjustmentWHTAXDetails
                With newWBSAmountAdjDetails
                    .WBSDetails = item
                    .AmountAdjusted = ShareOnAmountAdj
                End With
                initialListofWBSInvolved.Add(newWBSAmountAdjDetails)
            Next
        End If

        Dim sumofAmountAdjustedFromAP As Decimal = (From x In initialListofWBSInvolved Select x.AmountAdjusted).Sum()
        Dim diffAmountAdjusted As Decimal = Me.WBSAmountAdjustmentWhTax.TotalAPAmountAdjusted + sumofAmountAdjustedFromAP
        'addjust if has a difference in allocation
        If diffAmountAdjusted <> 0 And sumofAmountAdjustedFromAP <> 0 Then

            Dim updateInitialListFromAP As WBSAmountAdjustmentWHTAXDetails = (From x In initialListofWBSInvolved Select x Order By x.WBSDetails.BeginningBalance Descending).FirstOrDefault
            Dim updateInitialListFromAPList As List(Of WBSAmountAdjustmentWHTAXDetails) = (From x In initialListofWBSInvolved Select x Order By x.WBSDetails.BeginningBalance Ascending).ToList

            updateInitialListFromAP.AmountAdjusted -= diffAmountAdjusted
        End If
        'add in final list of wesmbillsummaryamountadjustmentdetails
        For Each item In initialListofWBSInvolved
            Dim createDMCM As New DebitCreditMemo
            If Not item.AmountAdjusted = 0 Then
                If iniWBSAmountAdj.ChargeType = EnumChargeType.E And iniWBSAmountAdj.BillingType = EnumBalanceType.AR Then
                    createDMCM = CreateDMCM_AR(item.WBSDetails, Math.Abs(item.AmountAdjusted))
                ElseIf iniWBSAmountAdj.ChargeType = EnumChargeType.E And iniWBSAmountAdj.BillingType = EnumBalanceType.AP Then
                    createDMCM = CreateDMCM_AP(item.WBSDetails, Math.Abs(item.AmountAdjusted))
                End If
                Me.ListofDMCM.Add(createDMCM)
                item.FTFDMCMDoc = BFactory.GenerateBIRDocumentNumber(createDMCM.DMCMNumber, BIRDocumentsType.DMCM)
                item.CreatedDocumentNo = createDMCM.DMCMNumber
                item.CreatedDocumentType = BIRDocumentsType.DMCM
                finalListofWBSInvolved.Add(item)
            End If
        Next

        'Create Dummy invoices
        Dim finalListofWBSAdjMonDetails As New List(Of WESMBillSummary)
        For Each item In finalListofWBSInvolved
            Dim dummyInvoices As New WESMBillSummary
            dummyInvoices = CType(item.WBSDetails.Clone, WESMBillSummary)
            dummyInvoices.BeginningBalance = item.AmountAdjusted
            dummyInvoices.EndingBalance = item.AmountAdjusted
            dummyInvoices.EnergyWithhold = 0
            dummyInvoices.INVDMCMNo = item.FTFDMCMDoc & "(" & item.WBSDetails.INVDMCMNo & ")"
            dummyInvoices.SummaryType = EnumSummaryType.DMCM
            finalListofWBSAdjMonDetails.Add(dummyInvoices)
        Next
        Dim createJV As New JournalVoucher

        Me.JournalVoucherSetup = Me.GenerateJVFromWHTaxAdj(Me.FundTransferform, Me.ListofDMCM)
        Me._WBSAmountAdjustmentWhTax.WBSAmountAdjWHTAXDetailsList = finalListofWBSInvolved
        Me._WBSAmountAdjustmentWhTax.WBSDummiesList = finalListofWBSAdjMonDetails

    End Sub

    Public Function GetWESMBIllSummaryList(ByVal BatchNo As Integer, ByVal BillingType As EnumBalanceType, ByVal ChargeType As EnumChargeType) As List(Of WESMBillSummary)
        Dim RetWBS As New List(Of WESMBillSummary)
        If ChargeType = EnumChargeType.E Then
            If BillingType = EnumBalanceType.AR Then
                RetWBS = (From x In Me.ListOfWESMBillSummary Where x.WESMBillBatchNo = BatchNo And x.ChargeType = EnumChargeType.E And x.EndingBalance = 0 Select x).ToList
            Else
                RetWBS = (From x In Me.ListOfWESMBillSummary Where x.WESMBillBatchNo = BatchNo And x.ChargeType = EnumChargeType.E And x.EndingBalance = 0 Select x).ToList
            End If
        Else
            If BillingType = EnumBalanceType.AR Then
                RetWBS = (From x In Me.ListOfWESMBillSummary Where x.WESMBillBatchNo = BatchNo And x.ChargeType <> EnumChargeType.E And x.EndingBalance = 0 Select x).ToList
            Else
                RetWBS = (From x In Me.ListOfWESMBillSummary Where x.WESMBillBatchNo = BatchNo And x.ChargeType <> EnumChargeType.E And x.EndingBalance = 0 Select x).ToList
            End If

        End If
        Return RetWBS
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
#End Region

#Region "GenerateDMCM"
    Public Function GenerateDMCM(ByVal dt As DataTable, ByVal dmcmno As Long) As DataTable
        Dim _DMCM As New DebitCreditMemo
        Dim Signatory As DocSignatories = WBillHelper.GetSignatories("DMCM").First
        Dim ListofAccountingCodes As List(Of AccountingCode) = WBillHelper.GetAccountingCodes()
        Dim getDMCM As DebitCreditMemo = (From x In Me.ListofDMCM Where x.DMCMNumber = dmcmno Select x).FirstOrDefault
        Dim _ParticipantInfo As AMParticipants = (From x In Me.AMParticipants Where x.IDNumber = getDMCM.IDNumber Select x).FirstOrDefault
        If Not getDMCM Is Nothing Then
            With getDMCM
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
        dt.AcceptChanges()
        Return dt
    End Function
#End Region

#Region "CreateDMCMAP"
    Public Function CreateDMCM_AP(ByVal WBSummaryitem As WESMBillSummary,
                                  ByVal AmountAdjusted As Decimal) As DebitCreditMemo
        Dim ret As New DebitCreditMemo
        Dim _Particulars As String = ""
        Me.DMCMSeqNo += 1
        Dim getAMparticipantInfo As AMParticipants = (From x In Me.AMParticipants Where x.IDNumber = WBSummaryitem.IDNumber.IDNumber Select x).FirstOrDefault()
        With ret
            .BillingPeriod = WBSummaryitem.BillPeriod
            .DueDate = WBSummaryitem.DueDate
            .IDNumber = WBSummaryitem.IDNumber.IDNumber
            .DMCMNumber = Me.DMCMSeqNo
            .JVNumber = Me.JVSeqNo
            .VATExempt = AmountAdjusted
            .TotalAmountDue = AmountAdjusted
            .Particulars = _Particulars
            .ChargeType = EnumChargeType.E
            .TransType = EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAPOnEnergy
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = DMCMSign.Signatory_1
            .ApprovedBy = DMCMSign.Signatory_2
            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, AmountAdjusted, 0, WBSummaryitem.INVDMCMNo, EnumSummaryType.INV, getAMparticipantInfo, EnumDMCMComputed.Compute))
            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.DebitCode, 0, AmountAdjusted, WBSummaryitem.INVDMCMNo, EnumSummaryType.INV, getAMparticipantInfo, EnumDMCMComputed.Compute))
        End With

        Return ret
    End Function
#End Region

#Region "CreateDMCMAR"
    Public Function CreateDMCM_AR(ByVal WBSummaryitem As WESMBillSummary,
                                  ByVal AmountAdjusted As Decimal) As DebitCreditMemo
        Dim ODays As Integer = 0
        Dim ret As New DebitCreditMemo
        Dim _Particulars As String = ""
        Me.DMCMSeqNo += 1
        Dim getAMparticipantInfo As AMParticipants = (From x In Me.AMParticipants Where x.IDNumber = WBSummaryitem.IDNumber.IDNumber Select x).FirstOrDefault()
        With ret
            .BillingPeriod = WBSummaryitem.BillPeriod
            .DueDate = WBSummaryitem.DueDate
            .IDNumber = WBSummaryitem.IDNumber.IDNumber
            .DMCMNumber = Me.DMCMSeqNo
            .JVNumber = Me.JVSeqNo
            .VATExempt = AmountAdjusted
            .TotalAmountDue = AmountAdjusted
            .Particulars = _Particulars
            .ChargeType = EnumChargeType.E
            .TransType = EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAROnEnergy
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = DMCMSign.Signatory_1
            .ApprovedBy = DMCMSign.Signatory_2
            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.CreditCode, AmountAdjusted, 0, WBSummaryitem.INVDMCMNo, EnumSummaryType.INV, getAMparticipantInfo, EnumDMCMComputed.Compute))
            .DMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.ClearingAccountCode, 0, AmountAdjusted, WBSummaryitem.INVDMCMNo, EnumSummaryType.INV, getAMparticipantInfo, EnumDMCMComputed.Compute))
        End With

        Return ret
    End Function
#End Region

#Region "FTF For MARKET FEES Withholding TAX Adjustment"
    Public Function CreateFTFForMFAPPaidByPEMCAcc(ByVal WBSummaryAmnAdj As List(Of WBSAmountAdjustmentWHTAXDetails)) As Long
        Dim ret As New FundTransferFormMain
        Dim Signatory = WBillHelper.GetSignatories("FTF").First()
        Dim TotalMFAmount As Decimal = 0
        Me.FTFSeqNo += 1
        Using _FTFMain As New FundTransferFormMain
            With _FTFMain
                .DRDate = CDate(AMModule.SystemDate.ToShortDateString)
                .CRDate = CDate(AMModule.SystemDate.ToShortDateString)
                .RefNo = Me.FTFSeqNo
                .TransType = EnumFTFTransType.TransferMarketFeesToSTL
                .AllocationDate = CDate(AMModule.SystemDate.ToShortDateString)
                For Each item In WBSummaryAmnAdj
                    Dim AMParticipantsInfo = (From x In Me.AMParticipants _
                                              Where x.IDNumber = item.WBSDetails.IDNumber.IDNumber _
                                              And x.ParticipantID = item.WBSDetails.IDNumber.ParticipantID _
                                              Select x).FirstOrDefault
                    .ListOfFTFParticipants.Add(New FundTransferFormParticipant(.RefNo, AMParticipantsInfo, Math.Abs(item.AmountAdjusted)))
                    TotalMFAmount += Math.Abs(item.AmountAdjusted)
                Next
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, DebitCode, AMModule.SettlementBankAccountNo, TotalMFAmount, 0))
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, CreditCode, AMModule.CashinBankPEMCCode, 0, TotalMFAmount))
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, DebitCode, AMModule.EWTReceivable, TotalMFAmount, 0))
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, CreditCode, AMModule.ClearingAccountCode, 0, TotalMFAmount))
                .PreparedBy = AMModule.FullName
                .RequestingApproval = Signatory.Signatory_1
                .ApprovedBy = Signatory.Signatory_2
                .TotalAmount = TotalMFAmount
                .Status = EnumStatus.Active
            End With
            Me.FundTransferform.Add(_FTFMain)
        End Using
        Return Me.FTFSeqNo
    End Function

    Public Function CreateFTFForMFAR(ByVal WBSummaryAmnAdj As List(Of WBSAmountAdjustmentWHTAXDetails)) As Long
        Dim ret As New FundTransferFormMain
        Dim Signatory = WBillHelper.GetSignatories("FTF").First()
        Dim TotalMFAmount As Decimal = 0
        Me.FTFSeqNo += 1
        Using _FTFMain As New FundTransferFormMain
            With _FTFMain
                .DRDate = CDate(AMModule.SystemDate.ToShortDateString)
                .CRDate = CDate(AMModule.SystemDate.ToShortDateString)
                .RefNo = Me.FTFSeqNo
                .TransType = EnumFTFTransType.TransferMarketFeesToPEMC
                .AllocationDate = CDate(AMModule.SystemDate.ToShortDateString)
                For Each item In WBSummaryAmnAdj
                    Dim AMParticipantsInfo = (From x In Me.AMParticipants _
                                              Where x.IDNumber = item.WBSDetails.IDNumber.IDNumber _
                                              And x.ParticipantID = item.WBSDetails.IDNumber.ParticipantID _
                                              Select x).FirstOrDefault
                    .ListOfFTFParticipants.Add(New FundTransferFormParticipant(.RefNo, AMParticipantsInfo, Math.Abs(item.AmountAdjusted)))
                    TotalMFAmount += Math.Abs(item.AmountAdjusted)
                Next
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, DebitCode, AMModule.CreditCode, TotalMFAmount, 0))
                .ListOfFTFDetails.Add(New FundTransferFormDetails(.RefNo, CreditCode, AMModule.EWTReceivable, 0, TotalMFAmount))
                .PreparedBy = AMModule.FullName
                .RequestingApproval = Signatory.Signatory_1
                .ApprovedBy = Signatory.Signatory_2
                .TotalAmount = TotalMFAmount
                .Status = EnumStatus.Active
            End With
            Me.FundTransferform.Add(_FTFMain)
        End Using
        Return Me.FTFSeqNo
    End Function
#End Region

#Region "Function For Generation of Journal Voucher From Withholding TAX adjustment"
    Public Function GenerateJVReport() As DataSet
        Dim ret As New DataSet
        Dim Accountingcode = WBillHelper.GetAccountingCodes()
        Dim JV_Signatories = WBillHelper.GetSignatories("JV").First
        Dim AllGPPosted = WBillHelper.GetWESMBillGPPosted()
        Dim JVData = Me.JournalVoucherSetup

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

#Region "JV Entry From Withholding Tax Adjustment"
    Public Function GenerateJVFromWHTaxAdj(ByVal wHTaxAdjFTFList As List(Of FundTransferFormMain),
                                           ByVal DMCMList As List(Of DebitCreditMemo)) As JournalVoucher
        Me.JVSeqNo += 1
        Dim JVBatchcode As String = EnumPostedType.ADJWHTAX.ToString() & "-1"
        Dim JVWHTaxAdj As New JournalVoucher
        Dim SumOfWhTaxAdjFTFListEWTRefund As Decimal = (From x In wHTaxAdjFTFList Where x.TransType = EnumFTFTransType.TransferMarketFeesToSTL Select x.TotalAmount).Sum
        Dim SumOfWhTaxAdjFTFListEWT As Decimal = (From x In wHTaxAdjFTFList Where x.TransType = EnumFTFTransType.TransferMarketFeesToPEMC Select x.TotalAmount).Sum
        Dim JVWHTaxAdjDic As New Dictionary(Of String, Integer)
        Dim JVSign As DocSignatories = WBillHelper.GetSignatories("JV").First
        With JVWHTaxAdj
            .JVDate = AMModule.SystemDate
            .JVNumber = Me.JVSeqNo
            .PostedType = EnumPostedType.ADJWHTAX.ToString
            .Status = EnumPostedTypeStatus.NotPosted
            .BatchCode = JVBatchcode
            .Remarks = "Withholding Tax Adjustment from previous Invoice/s " & AMModule.SystemDate & "."

            If SumOfWhTaxAdjFTFListEWTRefund <> 0 Then
                JVWHTaxAdj.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashInbankSettlementcode, SumOfWhTaxAdjFTFListEWTRefund, 0))
                JVWHTaxAdj.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashinBankPEMCCode, 0, SumOfWhTaxAdjFTFListEWTRefund))
            End If
            If SumOfWhTaxAdjFTFListEWT <> 0 Then
                Dim getCashInBankSTL = (From x In JVWHTaxAdj.JVDetails Where x.AccountCode = AMModule.CashInbankSettlementcode Select x).FirstOrDefault
                If Not getCashInBankSTL Is Nothing Then
                    getCashInBankSTL.Debit += SumOfWhTaxAdjFTFListEWT
                Else
                    JVWHTaxAdj.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashInbankSettlementcode, SumOfWhTaxAdjFTFListEWT, 0))
                End If

                Dim getCashinBankPEMCCode = (From x In JVWHTaxAdj.JVDetails Where x.AccountCode = AMModule.CashinBankPEMCCode Select x).FirstOrDefault
                If Not getCashinBankPEMCCode Is Nothing Then
                    getCashinBankPEMCCode.Credit += SumOfWhTaxAdjFTFListEWT
                Else
                    JVWHTaxAdj.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.CashinBankPEMCCode, 0, SumOfWhTaxAdjFTFListEWT))
                End If
                JVWHTaxAdj.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.EWTReceivable, SumOfWhTaxAdjFTFListEWT, 0))
                JVWHTaxAdj.JVDetails.Add(New JournalVoucherDetails(Me.JVSeqNo, AMModule.ClearingAccountCode, 0, SumOfWhTaxAdjFTFListEWT))
            End If
            Dim ctrl As Integer = 0
            For Each DMCMItem In DMCMList.OrderBy(Function(x) x.DMCMNumber)
                Select Case DMCMItem.TransType
                    Case EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAROnEnergy
                        For Each _ItemDetails In DMCMItem.DMCMDetails
                            Dim Key As String = _ItemDetails.AccountCode & EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAROnEnergy.ToString()
                            If Not JVWHTaxAdjDic.ContainsKey(Key) Then
                                If _ItemDetails.Credit = 0 Then
                                    JVWHTaxAdj.JVDetails.Add(New JournalVoucherDetails(JVWHTaxAdj.JVNumber, _ItemDetails.AccountCode, _ItemDetails.Debit, 0))
                                    JVWHTaxAdjDic.Add(Key, ctrl)
                                    ctrl += 1
                                ElseIf _ItemDetails.Debit = 0 Then
                                    JVWHTaxAdj.JVDetails.Add(New JournalVoucherDetails(JVWHTaxAdj.JVNumber, _ItemDetails.AccountCode, 0, _ItemDetails.Credit))
                                    JVWHTaxAdjDic.Add(Key, ctrl)
                                    ctrl += 1
                                End If
                            Else
                                Dim DicItem As Integer = JVWHTaxAdjDic(Key)
                                If _ItemDetails.Credit = 0 Then
                                    JVWHTaxAdj.JVDetails(DicItem).Debit += _ItemDetails.Debit

                                ElseIf _ItemDetails.Debit = 0 Then
                                    JVWHTaxAdj.JVDetails(DicItem).Credit += _ItemDetails.Credit
                                End If
                            End If
                        Next
                    Case EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAPOnEnergy
                        For Each _ItemDetails In DMCMItem.DMCMDetails
                            Dim Key As String = _ItemDetails.AccountCode & EnumDMCMTransactionType.WHTAXAdjustmentSetupClearingAPOnEnergy.ToString()
                            If Not JVWHTaxAdjDic.ContainsKey(Key) Then
                                If _ItemDetails.Credit = 0 Then
                                    JVWHTaxAdj.JVDetails.Add(New JournalVoucherDetails(JVWHTaxAdj.JVNumber, _ItemDetails.AccountCode, _ItemDetails.Debit, 0))
                                    JVWHTaxAdjDic.Add(Key, ctrl)
                                    ctrl += 1
                                ElseIf _ItemDetails.Debit = 0 Then
                                    JVWHTaxAdj.JVDetails.Add(New JournalVoucherDetails(JVWHTaxAdj.JVNumber, _ItemDetails.AccountCode, 0, _ItemDetails.Credit))
                                    JVWHTaxAdjDic.Add(Key, ctrl)
                                    ctrl += 1
                                End If
                            Else
                                Dim DicItem As Integer = JVWHTaxAdjDic(Key)
                                If _ItemDetails.Credit = 0 Then
                                    JVWHTaxAdj.JVDetails(DicItem).Debit += _ItemDetails.Debit

                                ElseIf _ItemDetails.Debit = 0 Then
                                    JVWHTaxAdj.JVDetails(DicItem).Credit += _ItemDetails.Credit
                                End If
                            End If
                        Next
                End Select
            Next
            .UpdatedBy = AMModule.UserName
            .PreparedBy = AMModule.FullName
            .CheckedBy = JVSign.Signatory_1
            .ApprovedBy = JVSign.Signatory_2
        End With

        'Dim WESMBillGPPostedItem As New WESMBillGPPosted

        'With WESMBillGPPostedItem
        '    .Remarks = JVWHTaxAdj.Remarks
        '    .Posted = 0
        '    .BatchCode = JVWHTaxAdj.BatchCode
        '    .PostType = JVWHTaxAdj.PostedType
        '    .DocumentAmount = Math.Round((From x In JVWHTaxAdj.JVDetails Select x.Debit).Sum, 2)
        '    .JVNumber = JVWHTaxAdj.JVNumber
        'End With
        'Me._WESMBillGPPostedList.Add(WESMBillGPPostedItem)
        Return JVWHTaxAdj
    End Function
#End Region

#Region "SAVING Adjustment"
    Private Function CreateSQLStatement() As List(Of String)
        Dim ListofSQL As New List(Of String)
        Dim newREfID As Long = getMaxRefNo() + 1
        Dim JVBatchCode As String = EnumPostedType.ADJWHTAX.ToString & "-" & CStr(WBillHelper.GetSequenceID("SEQ_AM_BATCH_CODE").ToString)
        Dim JournalVoucherNo As Long = WBillHelper.GetSequenceID("SEQ_AM_JV_NO")
        Dim WESMBillBatchNo As Long = WBillHelper.GetSequenceID("SEQ_AM_WESMBILL_BATCH_NO")
        Dim dicDocument As New Dictionary(Of Long, Long)
        Dim dicDocumentNo As New Dictionary(Of Long, Long)
        Dim SysDateTime As Date = WBillHelper.GetSystemDateTime()
        Dim SQL As String = ""
        With Me.WBSAmountAdjustmentWhTax
            With Me.JournalVoucherSetup
                SQL = "INSERT INTO AM_WESM_BILL_GP_POSTED (BILLING_PERIOD, STL_RUN, CHARGE_TYPE, DUE_DATE, REMARKS, POSTED, UPDATED_BY, BATCH_CODE, GP_REFNO, POSTED_TYPE, DOCUMENT_AMOUNT, AM_JV_NO) " & _
                           "VALUES ('','','','','" & .Remarks & "', '0', '" & _
                                   .UpdatedBy & "', '" & _
                                   JVBatchCode & "', '', '" & _
                                   EnumPostedType.ADJWHTAX.ToString & "', '" & _
                                   Math.Round((From x In .JVDetails Select x.Debit).Sum, 2) & "', '" & _
                                   JournalVoucherNo & "')"
                ListofSQL.Add(SQL)

                SQL = "INSERT INTO AM_JV(AM_JV_NO, AM_JV_DATE, BATCH_CODE, STATUS, PREPARED_BY, CHECKED_BY, APPROVED_BY, UPDATED_BY, UPDATED_DATE, POSTED_TYPE) " _
                            & "VALUES (" & JournalVoucherNo & ", TO_DATE('" & .JVDate.ToShortDateString & "','mm/dd/yyyy'), '" & JVBatchCode _
                                     & "', '" & .Status & "', '" & .PreparedBy & "', '" & .CheckedBy & "', '" & .ApprovedBy & "', '" & .UpdatedBy _
                                     & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & EnumPostedType.ADJWHTAX.ToString() & "')"
                ListofSQL.Add(SQL)
                'Insert AM_JV_DETAILS
                For Each detail In .JVDetails
                    SQL = "INSERT INTO AM_JV_DETAILS (AM_JV_NO, ACCT_CODE, DEBIT, CREDIT, UPDATED_BY, UPDATED_DATE) " _
                        & "VALUES(" & JournalVoucherNo & ", '" & detail.AccountCode & "', " & detail.Debit & ", " & detail.Credit _
                                & ", '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'))"
                    ListofSQL.Add(SQL)
                Next
            End With

            SQL = "INSERT INTO AM_WESM_BILL_WHTAX_ADJ (REF_ID, BILLING_PERIOD,BILLING_BATCH, BILLING_TYPE, CHARGE_TYPE, REMARKS, AM_JV_NO, UPDATED_BY, UPDATED_DATE, AR_ADJUSTED_WHTAX, AP_ADJUSTED_WHTAX) " & vbNewLine & _
                 "VALUES(" & newREfID & ", " & .BillingPeriod & ", " & .BillingBatchNo & ", '" & .BillingType.ToString & "', '" & .ChargeType.ToString & "','" & .Remarks & "', " & JournalVoucherNo & vbNewLine & _
                        ", '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss')," & .TotalARAmountAdjusted & "," & .TotalAPAmountAdjusted & ")"
            ListofSQL.Add(SQL)

            For Each getitem In .WBSAmountAdjWHTAXDetailsList
                Dim item As New WBSAmountAdjustmentWHTAXDetails
                item = getitem
                If item.CreatedDocumentType = BIRDocumentsType.FTF Then
                    Dim ReferenceNo As Long
                    Dim getFTF As FundTransferFormMain = (From x In Me.FundTransferform Where x.RefNo = item.CreatedDocumentNo Select x).FirstOrDefault
                    If Not dicDocument.ContainsKey(item.CreatedDocumentNo) Then
                        ReferenceNo = WBillHelper.GetSequenceID("SEQ_AM_FTF_REF_NO")
                        dicDocument.Add(item.CreatedDocumentNo, ReferenceNo)
                        dicDocumentNo.Add(item.WBSDetails.WESMBillSummaryNo, ReferenceNo)

                        SQL = "INSERT INTO AM_WESM_BILL_WHTAX_ADJ_DTLS (REF_ID, WESMBILL_SUMMARY_NO, CREATED_DOCUMENT_NO, CREATED_DOCUMENT_TYPE, AMOUNT_ADJUSTED) " & vbNewLine & _
                            "VALUES(" & newREfID & ", " & item.WBSDetails.WESMBillSummaryNo & ", " & ReferenceNo & ", '" & item.CreatedDocumentType.ToString & "'," & item.AmountAdjusted & ")"
                        ListofSQL.Add(SQL)

                        If .ChargeType = EnumChargeType.E Then
                            SQL = "UPDATE AM_WESM_BILL_SUMMARY SET ENDING_BALANCE = ENDING_BALANCE + " & item.AmountAdjusted & ", ENERGY_WITHHOLD = ENERGY_WITHHOLD + " & item.AmountAdjusted & vbNewLine & _
                             " WHERE WESMBILL_SUMMARY_NO = " & item.WBSDetails.WESMBillSummaryNo
                            ListofSQL.Add(SQL)
                        End If

                        SQL = "INSERT INTO AM_FTF_MAIN (ALLOCATION_DATE, BATCH_CODE, REF_NO, DR_DATE, CR_DATE, TOTAL_AMOUNT, " & _
                              "TRANS_TYPE, STATUS, IS_POSTED, REQUESTING_APPROVAL, APPROVED_BY, UPDATED_BY, UPDATED_DATE) " & _
                              "VALUES (TO_DATE('" & SysDateTime.ToShortDateString & "','MM/DD/YYYY'), '" & JVBatchCode & "', '" & ReferenceNo & "', " & _
                              "TO_DATE('" & getFTF.DRDate.ToString("MM/dd/yyyy") & "','MM/DD/YYYY'), TO_DATE('" & getFTF.CRDate.ToString("MM/dd/yyyy") & "','MM/DD/YYYY'), '" & getFTF.TotalAmount & "', " & _
                              "'" & getFTF.TransType & "', '" & getFTF.Status & "', '" & getFTF.IsPosted & "', '" & getFTF.RequestingApproval & "', '" & getFTF.ApprovedBy & "', " & _
                              "'" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'))"
                        ListofSQL.Add(SQL)

                        For Each ItemFTF In getFTF.ListOfFTFDetails
                            SQL = "INSERT INTO AM_FTF_DETAILS (REF_NO, ACCT_CODE, BANK_ACCNT_NO, ISSUING_BANK, RECEIVING_BANK, DEBIT, CREDIT, PARTICULARS, UPDATED_BY, UPDATED_DATE) " & _
                                  "VALUES('" & ReferenceNo & "', '" & ItemFTF.AccountCode & "', '" & ItemFTF.BankAccountNo & "', '" & ItemFTF.IssuingBank & "', '" & ItemFTF.ReceivingBank & "', " & _
                                  "'" & ItemFTF.Debit & "', '" & ItemFTF.Credit & "', '" & ItemFTF.Particulars & "', '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'))"
                            ListofSQL.Add(SQL)
                        Next

                        For Each ItemParticipants In getFTF.ListOfFTFParticipants
                            SQL = "INSERT INTO AM_FTF_PARTICIPANT (REF_NO, ID_NUMBER, AMOUNT, UPDATED_BY, UPDATED_DATE) " & _
                                    "VALUES ('" & ReferenceNo & "', '" & ItemParticipants.IDNumber.IDNumber & "', " & _
                                    "'" & ItemParticipants.Amount & "', '" & AMModule.UserName & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'))"
                            ListofSQL.Add(SQL)
                        Next
                    Else
                        ReferenceNo = dicDocument(item.CreatedDocumentNo)
                        dicDocumentNo.Add(item.WBSDetails.WESMBillSummaryNo, ReferenceNo)
                        SQL = "INSERT INTO AM_WESM_BILL_WHTAX_ADJ_DTLS (REF_ID, WESMBILL_SUMMARY_NO, CREATED_DOCUMENT_NO, CREATED_DOCUMENT_TYPE, AMOUNT_ADJUSTED) " & vbNewLine & _
                           "VALUES(" & newREfID & ", " & item.WBSDetails.WESMBillSummaryNo & ", " & ReferenceNo & ", '" & item.CreatedDocumentType.ToString & "'," & item.WBSDetails.BeginningBalance & ")"
                        ListofSQL.Add(SQL)
                    End If
                ElseIf item.CreatedDocumentType = BIRDocumentsType.DMCM Then
                    Dim getDMCM As DebitCreditMemo = (From x In Me.ListofDMCM Where x.DMCMNumber = item.CreatedDocumentNo Select x).FirstOrDefault
                    Dim DMCMNo As Long = 0
                    If Not dicDocument.ContainsKey(item.CreatedDocumentNo) Then
                        DMCMNo = WBillHelper.GetSequenceID("SEQ_AM_DMCM_NO")
                        dicDocument.Add(item.CreatedDocumentNo, DMCMNo)
                    Else
                        DMCMNo = dicDocument(item.CreatedDocumentNo)
                    End If

                    SQL = "INSERT INTO AM_WESM_BILL_WHTAX_ADJ_DTLS (REF_ID, WESMBILL_SUMMARY_NO, CREATED_DOCUMENT_NO, CREATED_DOCUMENT_TYPE, AMOUNT_ADJUSTED) " & vbNewLine & _
                            "VALUES(" & newREfID & ", " & item.WBSDetails.WESMBillSummaryNo & ", " & DMCMNo & ", '" & item.CreatedDocumentType.ToString & "'," & item.AmountAdjusted & ")"
                    ListofSQL.Add(SQL)

                    'If .ChargeType = EnumChargeType.E Then
                    '    SQL = "UPDATE AM_WESM_BILL_SUMMARY SET ENDING_BALANCE = ENDING_BALANCE + " & item.AmountAdjusted & ", ENERGY_WITHHOLD = ENERGY_WITHHOLD + " & item.AmountAdjusted & vbNewLine & _
                    '     " WHERE WESMBILL_SUMMARY_NO = " & item.WBSDetails.WESMBillSummaryNo
                    '    ListofSQL.Add(SQL)
                    'End If

                    dicDocumentNo.Add(item.WBSDetails.WESMBillSummaryNo, DMCMNo)
                    SQL = "INSERT INTO AM_DMCM (AM_DMCM_NO, AM_JV_NO, ID_NUMBER, PARTICULARS, CHARGE_TYPE, PREPARED_BY, CHECKED_BY, APPROVED_BY, " & _
                    "UPDATED_BY, UPDATED_DATE, TRANS_TYPE, VATABLE, VAT, VAT_EXEMPT, VAT_ZERO_RATED, TOTAL_AMOUNT_DUE, EWT, EWV, STATUS, OTHERS, BILLING_PERIOD, DUE_DATE) " & _
                    "VALUES ('" & DMCMNo & "', '" & JournalVoucherNo & "', '" & getDMCM.IDNumber & "', '" & getDMCM.Particulars & "', '" & getDMCM.ChargeType.ToString & "', '" & getDMCM.PreparedBy & "', '" & getDMCM.CheckedBy & "', '" & getDMCM.ApprovedBy & "', " & _
                    "'" & getDMCM.UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & getDMCM.TransType & "', '" & getDMCM.Vatable & "', '" & getDMCM.VAT & "', '" & getDMCM.VATExempt & "', '" & getDMCM.VatZeroRated & "', " & _
                    "'" & getDMCM.TotalAmountDue & "', '" & getDMCM.EWT & "','" & getDMCM.EWV & "', '1', '" & getDMCM.Others & "', '" & getDMCM.BillingPeriod & "', TO_DATE('" & getDMCM.DueDate & "','MM/DD/YYYY'))"
                    ListofSQL.Add(SQL)

                    'Insert AM_DMCM_DETAILS query database
                    For Each itemDMCM In getDMCM.DMCMDetails
                        SQL = "INSERT INTO AM_DMCM_DETAILS (AM_DMCM_NO, ACCT_CODE, DEBIT, CREDIT, UPDATED_BY, UPDATED_DATE, INV_DM_CM, SUMMARY_TYPE, ID_NUMBER, IS_COMPUTE) " & _
                                "VALUES ('" & DMCMNo & "', '" & itemDMCM.AccountCode & "', '" & itemDMCM.Debit & "', '" & itemDMCM.Credit & "', '" & itemDMCM.UpdatedBy & "', TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), " & _
                                "'" & itemDMCM.InvDMCMNo & "', '" & itemDMCM.SummaryType & "', '" & itemDMCM.IDNumber.IDNumber & "', '" & itemDMCM.IsComputed & "')"
                        ListofSQL.Add(SQL)
                    Next
                End If
            Next

            For Each item In .WBSDummiesList
                Dim getWESMBillSummaryNo As Long = WBillHelper.GetSequenceID("SEQ_AM_WESMBILL_SUMMARY_NO")
                SQL = "INSERT INTO AM_WESM_BILL_WHTAX_ADJ_MON(REF_ID, WESMBILL_SUMMARY_NO) " & vbNewLine & _
                      "VALUES(" & newREfID & ", " & getWESMBillSummaryNo & ")"
                ListofSQL.Add(SQL)
                Dim getDocumentNo As Long = dicDocumentNo(item.WESMBillSummaryNo)
                If .ChargeType = EnumChargeType.MF Then
                    item.INVDMCMNo = item.INVDMCMNo.Replace(Left(item.INVDMCMNo, 12), BFactory.GenerateBIRDocumentNumber(getDocumentNo, BIRDocumentsType.FTF))
                Else
                    item.INVDMCMNo = item.INVDMCMNo.Replace(Left(item.INVDMCMNo, 12), BFactory.GenerateBIRDocumentNumber(getDocumentNo, BIRDocumentsType.DMCM))
                End If

                SQL = "INSERT INTO AM_WESM_BILL (BATCH_CODE,AM_CODE,BILLING_PERIOD,STL_RUN,ID_NUMBER,REG_ID,FOR_ACCOUNT_OF,FULL_NAME, " & vbNewLine & _
                      "INVOICE_NO,INVOICE_DATE,AMOUNT,CHARGE_TYPE,DUE_DATE,MARKET_FEES_RATE,REMARKS) " & vbNewLine & _
                      "(SELECT 'U-XX' AS BATCH_CODE, 'U-CODE-XXX' AS AM_CODE, " & item.BillPeriod & ", 'FX' AS STL_RUN, '" & item.IDNumber.IDNumber & "', '" & item.IDNumber.IDNumber & "' AS REG_ID, '" & vbNewLine & _
                      "FOR THE ACCOUNT OF' AS FOR_ACCOUNT_OF, 'FULL_NAME' AS FULL_NAME, '" & item.INVDMCMNo & "', TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), " & item.BeginningBalance & ", '" & item.ChargeType.ToString & "', " & vbNewLine & _
                      "TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), 0 AS MARKET_FEES_RATE, '" & item.BillingRemarks & "' AS REMARKS FROM DUAL)"
                ListofSQL.Add(SQL)
                If item.BeginningBalance < 0 Then
                    SQL = "INSERT INTO AM_WESM_BILL_SUMMARY(BILLING_PERIOD,ID_NUMBER,CHARGE_TYPE,DUE_DATE,BEGINNING_BALANCE,UPDATED_DATE,UPDATED_BY,ENDING_BALANCE,GROUP_NO,ID_TYPE,NEW_DUEDATE,IS_MFWTAX_DEDUCTED,INV_DM_CM,SUMMARY_TYPE,WESMBILL_SUMMARY_NO,ADJUSTMENT,TRANSACTION_DATE,ENERGY_WITHHOLD,SPA_NO,WESMBILL_BATCH_NO,ENERGY_WITHHOLD_STATUS,NO_OFFSET, NO_SOA, BALANCE_TYPE) " & vbNewLine & _
                      "VALUES(" & item.BillPeriod & ", '" & item.IDNumber.IDNumber & "', '" & item.ChargeType.ToString & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), " & item.BeginningBalance & ", TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & AMModule.UserName & "', " & item.EndingBalance & ", " & vbNewLine & _
                              item.GroupNo & ", '" & item.IDType & "', TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), " & item.IsMFWTaxDeducted & ", '" & item.INVDMCMNo & "', '" & item.SummaryType.ToString & "', " & getWESMBillSummaryNo & ", " & item.Adjustment & ", TO_DATE('" & item.TransactionDate & "','MM/DD/YYYY hh:mi:ss AM'), " & item.EnergyWithhold & ", " & item.SPANo & ", " & vbNewLine & _
                              WESMBillBatchNo & ", " & item.EnergyWithholdStatus & ", " & If(item.NoOffset = True, 1, 0) & ", 0, 'AR')"
                    ListofSQL.Add(SQL)
                ElseIf item.BeginningBalance > 0 Then
                    SQL = "INSERT INTO AM_WESM_BILL_SUMMARY(BILLING_PERIOD,ID_NUMBER,CHARGE_TYPE,DUE_DATE,BEGINNING_BALANCE,UPDATED_DATE,UPDATED_BY,ENDING_BALANCE,GROUP_NO,ID_TYPE,NEW_DUEDATE,IS_MFWTAX_DEDUCTED,INV_DM_CM,SUMMARY_TYPE,WESMBILL_SUMMARY_NO,ADJUSTMENT,TRANSACTION_DATE,ENERGY_WITHHOLD,SPA_NO,WESMBILL_BATCH_NO,ENERGY_WITHHOLD_STATUS,NO_OFFSET, NO_SOA, BALANCE_TYPE) " & vbNewLine & _
                      "VALUES(" & item.BillPeriod & ", '" & item.IDNumber.IDNumber & "', '" & item.ChargeType.ToString & "', TO_DATE('" & item.DueDate & "','MM/DD/YYYY'), " & item.BeginningBalance & ", TO_DATE('" & SysDateTime.ToString("MM/dd/yyyy HH:mm:ss") & "','mm/dd/yyyy hh24:mi:ss'), '" & AMModule.UserName & "', " & item.EndingBalance & ", " & vbNewLine & _
                              item.GroupNo & ", '" & item.IDType & "', TO_DATE('" & item.NewDueDate & "','MM/DD/YYYY'), " & item.IsMFWTaxDeducted & ", '" & item.INVDMCMNo & "', '" & item.SummaryType.ToString & "', " & getWESMBillSummaryNo & ", " & item.Adjustment & ", TO_DATE('" & item.TransactionDate & "','MM/DD/YYYY hh:mi:ss AM'), " & item.EnergyWithhold & ", " & item.SPANo & ", " & vbNewLine & _
                              WESMBillBatchNo & ", " & item.EnergyWithholdStatus & ", " & If(item.NoOffset = True, 1, 0) & ", 0, 'AP')"
                    ListofSQL.Add(SQL)
                End If

                
            Next
        End With
        Return ListofSQL
    End Function

    Public Sub Save()
        Try
            Dim report As New DataReport
            Dim ListofSQL As New List(Of String)

            ListofSQL = Me.CreateSQLStatement
            report = Me.DataAccess.ExecuteSaveQuery(ListofSQL, New DataSet)

            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            ListofSQL = Nothing
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub
#End Region



    Private Function getMaxRefNo() As Long
        Dim report As New DataReport
        Dim ret As Long = 0
        Try
            Dim SQL As String = "SELECT MAX(REF_ID) AS CUR_REF_ID FROM AM_WESM_BILL_WHTAX_ADJ"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            While report.ReturnedIDatareader.Read()
                With report.ReturnedIDatareader
                    If IsDBNull(.Item("CUR_REF_ID")) Then
                        ret = 0
                    Else
                        ret = CLng(.Item("CUR_REF_ID"))
                    End If
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function getListWBSAmountAdjustmentInWHTAX() As List(Of WBSAmountAdjustmentInWHTAX)
        Dim report As New DataReport
        Dim ret As New List(Of WBSAmountAdjustmentInWHTAX)
        Try
            Dim SQL As String = "SELECT * FROM AM_WESM_BILL_WHTAX_ADJ"
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            While report.ReturnedIDatareader.Read()
                With report.ReturnedIDatareader
                    Dim item As New WBSAmountAdjustmentInWHTAX
                    item.ReferenceID = CInt(.Item("REF_ID"))
                    item.BillingPeriod = CInt(.Item("BILLING_PERIOD"))
                    item.BillingBatchNo = CInt(.Item("BILLING_BATCH"))
                    item.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE"))), EnumChargeType)
                    item.BillingType = CType(System.Enum.Parse(GetType(EnumBalanceType), CStr(.Item("BILLING_TYPE"))), EnumBalanceType)
                    item.TotalARAmountAdjusted = CDec(.Item("AR_ADJUSTED_WHTAX"))
                    item.TotalAPAmountAdjusted = CDec(.Item("AP_ADJUSTED_WHTAX"))
                    item.Remarks = If(IsDBNull(.Item("REMARKS").ToString()), "", .Item("REMARKS").ToString())
                    item.JVSetupNo = CLng(.Item("AM_JV_NO"))
                    item.UpdatedBy = CStr(.Item("UPDATED_BY"))
                    item.UpdatedDate = CDate(.Item("UPDATED_DATE"))
                    ret.Add(item)
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function getListWBSAmountAdjustmentInWHTAXDetails(ByVal refID As Long) As List(Of WBSAmountAdjustmentWHTAXDetails)
        Dim report As New DataReport
        Dim ret As New List(Of WBSAmountAdjustmentWHTAXDetails)
        Try
            Dim SQL As String = "SELECT A.CREATED_DOCUMENT_NO, A.CREATED_DOCUMENT_TYPE, A.AMOUNT_ADJUSTED, B.* FROM AM_WESM_BILL_WHTAX_ADJ_DTLS A " & vbNewLine & _
                                "LEFT JOIN (SELECT T1.*, T2.PARTICIPANT_ID, T2.PARTICIPANT_ADDRESS, T2.CITY, T2.PROVINCE, T2.ZIP_CODE, T2.ZERO_RATED_MARKET_FEES, T2.ZERO_RATED_ENERGY, T3.REMARKS " & vbNewLine & _
                                          "FROM AM_WESM_BILL_SUMMARY T1 " & vbNewLine & vbNewLine & _
                                          "LEFT JOIN AM_PARTICIPANTS T2 ON T1.ID_NUMBER = T2.ID_NUMBER " & vbNewLine & _
                                          "LEFT JOIN AM_WESM_BILL T3 ON T3.INVOICE_NO = T1.INV_DM_CM AND T3.CHARGE_TYPE = T1.CHARGE_TYPE) B " & vbNewLine & _
                                "ON B.WESMBILL_SUMMARY_NO = A.WESMBILL_SUMMARY_NO " & vbNewLine & _
                                "WHERE A.REF_ID = " & refID
            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If

            While report.ReturnedIDatareader.Read()
                With report.ReturnedIDatareader
                    Dim item As New WBSAmountAdjustmentWHTAXDetails
                    item.CreatedDocumentNo = CLng(.Item("CREATED_DOCUMENT_NO").ToString)
                    item.CreatedDocumentType = CType(System.Enum.Parse(GetType(BIRDocumentsType), CStr(.Item("CREATED_DOCUMENT_TYPE").ToString())), BIRDocumentsType)
                    item.FTFDMCMDoc = BFactory.GenerateBIRDocumentNumber(item.CreatedDocumentNo, item.CreatedDocumentType)
                    item.AmountAdjusted = CDec(.Item("AMOUNT_ADJUSTED").ToString)
                    Dim newWBSItem As New WESMBillSummary

                    newWBSItem.IDNumber = New AMParticipants(CStr(.Item("ID_NUMBER").ToString()), CStr(.Item("PARTICIPANT_ID").ToString()), _
                                                       CStr(.Item("PARTICIPANT_ADDRESS").ToString()), CStr(.Item("CITY").ToString()), CStr(.Item("PROVINCE").ToString()), _
                                                       CStr(.Item("ZIP_CODE").ToString()), CBool(IIf(CInt(.Item("ZERO_RATED_MARKET_FEES")) = 1, True, False)), _
                                                       CBool(IIf(CInt(.Item("ZERO_RATED_ENERGY")) = 1, True, False)))

                    newWBSItem.BillPeriod = CInt(.Item("BILLING_PERIOD").ToString())
                    newWBSItem.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE").ToString())), EnumChargeType)
                    newWBSItem.DueDate = CDate(.Item("DUE_DATE").ToString())
                    newWBSItem.BeginningBalance = Math.Round(CDec(.Item("BEGINNING_BALANCE").ToString()), 2)
                    If newWBSItem.BeginningBalance < 0 Then
                        newWBSItem.BalanceType = EnumBalanceType.AR
                    Else
                        newWBSItem.BalanceType = EnumBalanceType.AP
                    End If
                    newWBSItem.EndingBalance = Math.Round(CDec(.Item("ENDING_BALANCE").ToString()), 2)
                    newWBSItem.OrigEndingBalance = Math.Round(CDec(.Item("ENDING_BALANCE").ToString()), 2)
                    newWBSItem.IDType = CStr(.Item("ID_TYPE").ToString())
                    newWBSItem.NewDueDate = CDate(.Item("NEW_DUEDATE").ToString())
                    newWBSItem.OrigNewDueDate = CDate(.Item("NEW_DUEDATE").ToString())
                    newWBSItem.IsMFWTaxDeducted = CInt(.Item("IS_MFWTAX_DEDUCTED").ToString())
                    newWBSItem.INVDMCMNo = CStr(.Item("INV_DM_CM").ToString())
                    If .Item("SUMMARY_TYPE") IsNot DBNull.Value Then
                        newWBSItem.SummaryType = CType(System.Enum.Parse(GetType(EnumSummaryType), CStr(.Item("SUMMARY_TYPE").ToString())), EnumSummaryType)
                    End If
                    newWBSItem.WESMBillSummaryNo = CLng(.Item("WESMBILL_SUMMARY_NO"))
                    newWBSItem.Adjustment = CInt(.Item("ADJUSTMENT").ToString())
                    newWBSItem.TransactionDate = CDate(.Item("TRANSACTION_DATE"))
                    newWBSItem.EnergyWithhold = CDec(.Item("ENERGY_WITHHOLD"))
                    newWBSItem.WESMBillBatchNo = CLng(.Item("WESMBILL_BATCH_NO"))
                    If .Item("ENERGY_WITHHOLD_STATUS") IsNot DBNull.Value Then
                        newWBSItem.EnergyWithholdStatus = CType(CInt(.Item("ENERGY_WITHHOLD_STATUS")), EnumEnergyWithholdStatus)
                    Else
                        newWBSItem.EnergyWithholdStatus = EnumEnergyWithholdStatus.NotApplicable
                    End If
                    newWBSItem.NoOffset = CBool(IIf(CInt(.Item("NO_OFFSET")) = 1, True, False))
                    newWBSItem.BillingRemarks = CStr(.Item("REMARKS").ToString())
                    item.WBSDetails = newWBSItem
                    ret.Add(item)
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

    Private Function getListWBSAmountAdjustmentInWHTAXMon(ByVal refID As Long) As List(Of WESMBillSummary)
        Dim report As New DataReport
        Dim ret As New List(Of WESMBillSummary)
        Try
            Dim SQL As String = "SELECT B.* FROM AM_WESM_BILL_WHTAX_ADJ_MON A " & vbNewLine & _
               "LEFT JOIN (SELECT T1.*, T2.PARTICIPANT_ID, T2.PARTICIPANT_ADDRESS, T2.CITY, T2.PROVINCE, T2.ZIP_CODE, T2.ZERO_RATED_MARKET_FEES, T2.ZERO_RATED_ENERGY, T3.REMARKS " & vbNewLine & _
                                          "FROM AM_WESM_BILL_SUMMARY T1 " & vbNewLine & vbNewLine & _
                                          "LEFT JOIN AM_PARTICIPANTS T2 ON T1.ID_NUMBER = T2.ID_NUMBER " & vbNewLine & _
                                          "LEFT JOIN AM_WESM_BILL T3 ON T3.INVOICE_NO = T1.INV_DM_CM AND T3.CHARGE_TYPE = T1.CHARGE_TYPE) B " & vbNewLine & _
                "ON B.WESMBILL_SUMMARY_NO = A.WESMBILL_SUMMARY_NO " & vbNewLine & _
                "WHERE A.REF_ID = " & refID

            report = Me.DataAccess.ExecuteSelectQueryReturningDataReader(SQL)
            If report.ErrorMessage.Length <> 0 Then
                Throw New ApplicationException(report.ErrorMessage)
            End If
            While report.ReturnedIDatareader.Read()
                With report.ReturnedIDatareader
                    Dim item As New WESMBillSummary

                    item.IDNumber = New AMParticipants(CStr(.Item("ID_NUMBER").ToString()), CStr(.Item("PARTICIPANT_ID").ToString()), _
                                                       CStr(.Item("PARTICIPANT_ADDRESS").ToString()), CStr(.Item("CITY").ToString()), CStr(.Item("PROVINCE").ToString()), _
                                                       CStr(.Item("ZIP_CODE").ToString()), CBool(IIf(CInt(.Item("ZERO_RATED_MARKET_FEES")) = 1, True, False)), _
                                                       CBool(IIf(CInt(.Item("ZERO_RATED_ENERGY")) = 1, True, False)))

                    item.BillPeriod = CInt(.Item("BILLING_PERIOD").ToString())
                    item.ChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(.Item("CHARGE_TYPE").ToString())), EnumChargeType)
                    item.DueDate = CDate(.Item("DUE_DATE").ToString())
                    item.BeginningBalance = Math.Round(CDec(.Item("BEGINNING_BALANCE").ToString()), 2)
                    If item.BeginningBalance < 0 Then
                        item.BalanceType = EnumBalanceType.AR
                    Else
                        item.BalanceType = EnumBalanceType.AP
                    End If
                    item.EndingBalance = Math.Round(CDec(.Item("ENDING_BALANCE").ToString()), 2)
                    item.OrigEndingBalance = Math.Round(CDec(.Item("ENDING_BALANCE").ToString()), 2)
                    item.IDType = CStr(.Item("ID_TYPE").ToString())
                    item.NewDueDate = CDate(.Item("NEW_DUEDATE").ToString())
                    item.OrigNewDueDate = CDate(.Item("NEW_DUEDATE").ToString())
                    item.IsMFWTaxDeducted = CInt(.Item("IS_MFWTAX_DEDUCTED").ToString())
                    item.INVDMCMNo = CStr(.Item("INV_DM_CM").ToString())
                    If .Item("SUMMARY_TYPE") IsNot DBNull.Value Then
                        item.SummaryType = CType(System.Enum.Parse(GetType(EnumSummaryType), CStr(.Item("SUMMARY_TYPE").ToString())), EnumSummaryType)
                    End If
                    item.WESMBillSummaryNo = CLng(.Item("WESMBILL_SUMMARY_NO"))
                    item.Adjustment = CInt(.Item("ADJUSTMENT").ToString())
                    item.TransactionDate = CDate(.Item("TRANSACTION_DATE"))
                    item.EnergyWithhold = CDec(.Item("ENERGY_WITHHOLD"))
                    item.WESMBillBatchNo = CLng(.Item("WESMBILL_BATCH_NO"))
                    If .Item("ENERGY_WITHHOLD_STATUS") IsNot DBNull.Value Then
                        item.EnergyWithholdStatus = CType(CInt(.Item("ENERGY_WITHHOLD_STATUS")), EnumEnergyWithholdStatus)
                    Else
                        item.EnergyWithholdStatus = EnumEnergyWithholdStatus.NotApplicable
                    End If
                    item.NoOffset = CBool(IIf(CInt(.Item("NO_OFFSET")) = 1, True, False))
                    item.BillingRemarks = CStr(.Item("REMARKS").ToString())
                    ret.Add(item)
                End With
            End While
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
        Return ret
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
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
