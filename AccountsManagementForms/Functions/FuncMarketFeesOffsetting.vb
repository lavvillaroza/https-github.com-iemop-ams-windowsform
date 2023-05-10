'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              FuncMarketFeesOffsetting
'Orginal Author:         Juan Carlo Panopio
'File Creation Date:     July 29, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for the maintenance of Collection
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   July 29, 2013       Juan Carlo L. Panopio           Class Initialization
'

Option Explicit On
Option Strict On
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects


Public Class FuncMarketFeesOffsetting

#Region "Initialization"
    Public Sub New()
        Me._BFactory = BusinessFactory.GetInstance()
        Me._MFWESMBill = New List(Of WESMBillSummary)
        Me._CreatedOR = New OfficialReceiptMain
        Me._CreatedDMCM = New List(Of DebitCreditMemo)
        Me._Participant = New AMParticipants
        Me._AmountForApplication = 0
        Me._WHTaxAmount = 0
        Me._WHTaxDefault = 0
        Me._WHVATAmount = 0
        Me._WHVATDefault = 0
        Me._ReturnAmount = 0
        Me._AppliedBaseMF = 0
        Me._AppliedVATMF = 0
        Me._AppliedBaseDefaultMF = 0
        Me._AppliedBaseDefaultMFV = 0
        Me._DateComputationForDefault = SystemDate
        Me._InterestRate = 0
        Me._DMCMCtr = 0
        Me._ORCounter = 0
        Me._dicDMCMTrans = New Dictionary(Of Integer, EnumDMCMTransactionType)
        Me._isDefaultGreater = False
    End Sub
#End Region

#Region "PROPERTIES"

    'Business Factory - Computation of Default Interest
    Private _BFactory As BusinessFactory
    Public Property BFactory() As BusinessFactory
        Get
            Return _BFactory
        End Get
        Set(ByVal value As BusinessFactory)
            _BFactory = value
        End Set
    End Property

    'To handle MF/MFV Invoices - Update Ending Balances
    Private _MFWESMBill As List(Of WESMBillSummary)
    Public Property MFWESMBill() As List(Of WESMBillSummary)
        Get
            Return _MFWESMBill
        End Get
        Set(ByVal value As List(Of WESMBillSummary))
            _MFWESMBill = value
        End Set
    End Property

    'Return of Created OR
    Private _CreatedOR As OfficialReceiptMain
    Public Property CreatedOR() As OfficialReceiptMain
        Get
            Return _CreatedOR
        End Get
        Set(ByVal value As OfficialReceiptMain)
            _CreatedOR = value
        End Set
    End Property

    'Return of Created DMCM
    Private _CreatedDMCM As List(Of DebitCreditMemo)
    Public Property CreatedDMCM() As List(Of DebitCreditMemo)
        Get
            Return _CreatedDMCM
        End Get
        Set(ByVal value As List(Of DebitCreditMemo))
            _CreatedDMCM = value
        End Set
    End Property

    'Amount For Application (Collection/Payment - Full/Partial Payment to the invoices)
    Private _AmountForApplication As Decimal
    Public Property AmountForApplication() As Decimal
        Get
            Return _AmountForApplication
        End Get
        Set(ByVal value As Decimal)
            _AmountForApplication = value
        End Set
    End Property

    'Amount Applied to Base MF 
    Private _AppliedBaseMF As Decimal
    Public Property AppliedBaseMF() As Decimal
        Get
            Return _AppliedBaseMF
        End Get
        Set(ByVal value As Decimal)
            _AppliedBaseMF = value
        End Set
    End Property

    'Amount Applied to MFVat 
    Private _AppliedVATMF As Decimal
    Public Property AppliedVATMF() As Decimal
        Get
            Return _AppliedVATMF
        End Get
        Set(ByVal value As Decimal)
            _AppliedVATMF = value
        End Set
    End Property

    'Amount Applied to DefaultMF
    Private _AppliedBaseDefaultMF As Decimal
    Public Property AppliedBaseDefaultMF() As Decimal
        Get
            Return _AppliedBaseDefaultMF
        End Get
        Set(ByVal value As Decimal)
            _AppliedBaseDefaultMF = value
        End Set
    End Property

    'Amount Applied to Default MFV
    Private _AppliedBaseDefaultMFV As Decimal
    Public Property AppliedBaseDefaultMFV() As Decimal
        Get
            Return _AppliedBaseDefaultMFV
        End Get
        Set(ByVal value As Decimal)
            _AppliedBaseDefaultMFV = value
        End Set
    End Property

    'Computed WHTax
    Private _WHTaxAmount As Decimal
    Public Property WHTaxAmount() As Decimal
        Get
            Return _WHTaxAmount
        End Get
        Set(ByVal value As Decimal)
            _WHTaxAmount = value
        End Set
    End Property

    'Computed WHVAT
    Private _WHVATAmount As Decimal
    Public Property WHVATAmount() As Decimal
        Get
            Return _WHVATAmount
        End Get
        Set(ByVal value As Decimal)
            _WHVATAmount = value
        End Set
    End Property

    'Computed WTax on Default
    Private _WHTaxDefault As Decimal
    Public Property WHTaxDefault() As Decimal
        Get
            Return _WHTaxDefault
        End Get
        Set(ByVal value As Decimal)
            _WHTaxDefault = value
        End Set
    End Property

    'Computed WVAT on Default
    Private _WHVATDefault As Decimal
    Public Property WHVATDefault() As Decimal
        Get
            Return _WHVATDefault
        End Get
        Set(ByVal value As Decimal)
            _WHVATDefault = value
        End Set
    End Property

    'Return Amount
    Private _ReturnAmount As Decimal
    Public Property ReturnAmount() As Decimal
        Get
            Return _ReturnAmount
        End Get
        Set(ByVal value As Decimal)
            _ReturnAmount = value
        End Set
    End Property

    'Participant Information for WHTax/WHVAT Rates
    Private _Participant As AMParticipants
    Public Property Participant() As AMParticipants
        Get
            Return _Participant
        End Get
        Set(ByVal value As AMParticipants)
            _Participant = value
        End Set
    End Property

    'Date for computation of Default interest
    Private _DateComputationForDefault As Date
    Public Property DateComputationforDefault() As Date
        Get
            Return _DateComputationForDefault
        End Get
        Set(ByVal value As Date)
            _DateComputationForDefault = value
        End Set
    End Property

    'Rate for computation of Default Interest
    Private _InterestRate As Decimal
    Public Property InterestRate() As Decimal
        Get
            Return _InterestRate
        End Get
        Set(ByVal value As Decimal)
            _InterestRate = value
        End Set
    End Property

    'Counter for numbering of DMCM
    Private _DMCMCtr As Integer
    Public Property DMCMCtr() As Integer
        Get
            Return _DMCMCtr
        End Get
        Set(ByVal value As Integer)
            _DMCMCtr = value
        End Set
    End Property

    'Counter for numbering of OR
    Private _ORCounter As Integer
    Public Property ORCounter() As Integer
        Get
            Return _ORCounter
        End Get
        Set(ByVal value As Integer)
            _ORCounter = value
        End Set
    End Property

    'DMCM Transaction Summary
    Private _dicDMCMTrans As Dictionary(Of Integer, EnumDMCMTransactionType)
    Public Property dicDMCMTrans() As Dictionary(Of Integer, EnumDMCMTransactionType)
        Get
            Return _dicDMCMTrans
        End Get
        Set(ByVal value As Dictionary(Of Integer, EnumDMCMTransactionType))
            _dicDMCMTrans = value
        End Set
    End Property

    'Check if default > amount for application
    Private _isDefaultGreater As Boolean
    Public Property isDefaultGreater() As Boolean
        Get
            Return _isDefaultGreater
        End Get
        Set(ByVal value As Boolean)
            _isDefaultGreater = value
        End Set
    End Property

#End Region

    Public Function ApplicationToMarketFees() As FuncMarketFeesOffsetting
        Dim MarketFeesOffsetting As New FuncMarketFeesOffsetting
        Dim WithholdingRatio As Decimal = 0
        Dim MFMFVAppliedAmount As Decimal = 0
        Dim NetWTaxWVATAmount As Decimal = 0
        Dim MFDefaultComputed As Decimal = 0
        Dim MFVDefaultComputed As Decimal = 0


        Dim DateDue = (From x In Me._MFWESMBill _
                   Select x.DueDate, x.NewDueDate).FirstOrDefault

        'Get Market Fees Original Amount
        Dim MFAmount = (From x In Me._MFWESMBill _
                               Where x.ChargeType = EnumChargeType.MF _
                               Select x.BeginningBalance).Sum

        Dim MFVAmount = (From x In Me._MFWESMBill _
                         Where x.ChargeType = EnumChargeType.MFV _
                         Select x.BeginningBalance).Sum

        'Get Market Fees Ending Balance
        Dim MFEndingBalance = (From x In Me._MFWESMBill _
                               Where x.ChargeType = EnumChargeType.MF _
                               Select x.EndingBalance).Sum

        Dim MFVEndingBalance = (From x In Me._MFWESMBill _
                               Where x.ChargeType = EnumChargeType.MFV _
                               Select x.EndingBalance).Sum

        '**************************      Computation for Default Interest     *************************************
        'Computed Default interest should be negative
        MFDefaultComputed = Me._BFactory.ComputeDefaultInterest(DateDue.DueDate, DateDue.NewDueDate, Me._DateComputationForDefault, MFEndingBalance, Me._InterestRate)
        MFVDefaultComputed = Me._BFactory.ComputeDefaultInterest(DateDue.DueDate, DateDue.NewDueDate, Me._DateComputationForDefault, MFVEndingBalance, Me._InterestRate)

        'Computed Withholding Tax/VAT should be positive
        WHTaxDefault = Math.Round(Math.Abs(MFDefaultComputed * Me._Participant.MarketFeesTax), 3)
        WHVATDefault = Math.Round(Math.Abs(MFDefaultComputed * Me._Participant.VatOnMarketFeesTax), 3)

        'Get total application of default interest on market fees (net of withholding)
        Dim CheckDefaultAmount As Decimal = 0
        CheckDefaultAmount = MFDefaultComputed + MFVDefaultComputed + WHTaxDefault + WHVATDefault

        'Check if Amount for Application is Enough for Default Interest on Market Fees
        If CheckDefaultAmount <> 0 Then
            If Math.Abs(CheckDefaultAmount) <= Me._AmountForApplication Then
                'Assign Values for the Class
                Me._AppliedBaseDefaultMF = MFDefaultComputed
                Me._AppliedBaseDefaultMFV = MFVDefaultComputed

                Me._WHTaxDefault = WHTaxDefault
                Me._WHVATDefault = WHVATDefault

                'Create DMCM For Setup of Withholding Tax/Withholding VAT on Default Interest
                Me._CreatedDMCM.Add(Me.CreateDebitCreditMemo(EnumDMCMTransactionType.PaymentSetupWithholding, WHTaxDefault, WHVATDefault, Me._MFWESMBill.Where(Function(x) x.ChargeType = EnumChargeType.MF).FirstOrDefault, _
                                         Me._MFWESMBill.Where(Function(x) x.ChargeType = EnumChargeType.MFV).FirstOrDefault, True))

                'Create DMCM For Setup of Default Interest - Gross amount (Withholding not deducted)
                Me._CreatedDMCM.Add(Me.CreateDebitCreditMemo(EnumDMCMTransactionType.PaymentSetupOfDefaultInterestMFMFV, Math.Abs(MFDefaultComputed), Math.Abs(MFVDefaultComputed), Me._MFWESMBill.Where(Function(x) x.ChargeType = EnumChargeType.MF).FirstOrDefault, _
                                         Me._MFWESMBill.Where(Function(x) x.ChargeType = EnumChargeType.MFV).FirstOrDefault, True))

                'Create DMCM For Application of Default Interest - Gross amount (Withholding not deducted)
                Me._CreatedDMCM.Add(Me.CreateDebitCreditMemo(EnumDMCMTransactionType.PaymentMFandMFVATDefaultInterest, Math.Abs(_AppliedBaseDefaultMF), Math.Abs(_AppliedBaseDefaultMFV), Me._MFWESMBill.Where(Function(x) x.ChargeType = EnumChargeType.MF).FirstOrDefault, _
                                         Me._MFWESMBill.Where(Function(x) x.ChargeType = EnumChargeType.MFV).FirstOrDefault))

                'Adjust amount for application (deduct default amount)
                Me._AmountForApplication += CheckDefaultAmount
                Me._isDefaultGreater = False
            Else
                'Don't Apply if amount is not enough for default interest
                Me._ReturnAmount = Me._AmountForApplication
                Me._isDefaultGreater = True
                Return MarketFeesOffsetting
            End If
        End If

        '**************************     End Computation for Default Interest     *************************************
        If Me._AmountForApplication = 0 Then
            Return MarketFeesOffsetting
        End If

        If Me._AmountForApplication < 0 Then
            MsgBox("Error in Default Interest Application - Market Fees)")
        End If

        '**************************     Application to Base Market Fees and VAT on Market Fees     *************************************
        'Computation for Base Market Fees / Market Fees Application
        Dim WtaxMF As New Decimal
        Dim WVATMF As New Decimal

        WtaxMF = Math.Abs(MFAmount * Me._Participant.MarketFeesTax)
        WVATMF = Math.Abs(MFAmount * Me._Participant.VatOnMarketFeesTax)

        Dim TotalMFOffsetting As New Decimal
        If MFVEndingBalance = 0 Then
            TotalMFOffsetting = MFAmount + WtaxMF
        Else
            TotalMFOffsetting = MFAmount + MFVAmount + WtaxMF + WVATMF
        End If

        'For computation of total application
        Dim TotalApplied As New Decimal

        'Check Total MF For Offsetting vs. Amount Allocated
        If Me._AmountForApplication >= Math.Abs(TotalMFOffsetting) Then
            'If amount allocated is greater or Equal MF Offsetting
            Me._AppliedBaseMF = MFEndingBalance            
            Me._WHTaxAmount = Math.Abs(Me._AppliedBaseMF * Me._Participant.MarketFeesTax)
            If MFVAmount <> 0 Then
                Me._AppliedVATMF = MFVEndingBalance
                Me._WHVATAmount = Math.Abs(Me._AppliedBaseMF * Me._Participant.VatOnMarketFeesTax)
            End If

        Else
            'if amount allocated is less than total market fees
            'Compute for ratio per item
            Dim BaseMFRatio As New Decimal
            Dim VATMFRatio As New Decimal
            Dim WTaxRatio As New Decimal
            Dim WVATRatio As New Decimal

            BaseMFRatio = MFAmount / TotalMFOffsetting
            VATMFRatio = MFVAmount / TotalMFOffsetting
            WTaxRatio = WtaxMF / TotalMFOffsetting
            WVATRatio = WVATMF / TotalMFOffsetting

            Debug.Print("Partial " & BaseMFRatio & ", " & VATMFRatio & ", " & WTaxRatio & ", " & WVATRatio)

            Me._AppliedBaseMF = _AmountForApplication * BaseMFRatio * -1D
            Me._WHTaxAmount = Math.Abs(AmountForApplication * WTaxRatio)

            If MFVAmount <> 0 Then
                Me._AppliedVATMF = _AmountForApplication * VATMFRatio * -1D
                Me._WHVATAmount = Math.Abs(_AmountForApplication * WVATRatio)
            End If

        End If

        'Total Applied
        TotalApplied = Me._AppliedBaseMF + Me._AppliedVATMF + Me._WHTaxAmount + Me._WHVATAmount
        Debug.Print("Available share " & _AmountForApplication)
        'Adjust participant allocation based on the application
        Me._AmountForApplication += Math.Round(TotalApplied, 4)
        If _AmountForApplication < 0 Then
            MsgBox("MF Application error (Negative share)")
        End If

        If WHTaxAmount <> 0 Or WHVATAmount <> 0 Then
            Me.CreatedDMCM.Add(Me.CreateDebitCreditMemo(EnumDMCMTransactionType.PaymentSetupWithholding, WHTaxAmount, WHVATAmount, Me._MFWESMBill.Where(Function(x) x.ChargeType = EnumChargeType.MF).FirstOrDefault, _
                                                    Me._MFWESMBill.Where(Function(x) x.ChargeType = EnumChargeType.MFV).FirstOrDefault))
        End If
        
        If Me._AppliedBaseMF <> 0 Or Me._AppliedVATMF <> 0 Then
            Me.CreatedDMCM.Add(Me.CreateDebitCreditMemo(EnumDMCMTransactionType.PaymentOffsettingOfReceivableMF, Math.Abs(Me._AppliedBaseMF), Math.Abs(Me._AppliedVATMF), Me._MFWESMBill.Where(Function(x) x.ChargeType = EnumChargeType.MF).FirstOrDefault, _
                                                    Me._MFWESMBill.Where(Function(x) x.ChargeType = EnumChargeType.MFV).FirstOrDefault))
        End If
        '**************************     End Application to Base Market Fees and VAT on Market Fees     *************************************

        ' - Create OR For the transaction -
        Dim _chkBaseInvoice = (From x In _MFWESMBill _
                               Where x.ChargeType = EnumChargeType.MF _
                               Select x).FirstOrDefault

        Dim _chkVATInvoice = (From x In _MFWESMBill _
                              Where x.ChargeType = EnumChargeType.MFV _
                              Select x).FirstOrDefault

        Dim _ForORInvoice As New WESMBillSummary

        If _chkBaseInvoice Is Nothing Then
            _ForORInvoice = _chkVATInvoice
        Else
            _ForORInvoice = _chkBaseInvoice
        End If

        Me.CreatedOR = (Me.CreateOfficialReceipt(_ForORInvoice, Me.CreatedDMCM, MFDefaultComputed, AppliedBaseMF, _
                                                 WHTaxDefault, WHVATDefault, WHTaxAmount, MFVDefaultComputed, AppliedVATMF, WHVATAmount))

        With MarketFeesOffsetting
            .MFWESMBill = Me._MFWESMBill
            .AppliedBaseMF = Me._AppliedBaseMF 'Gross of WHTax
            .AppliedVATMF = Me._AppliedVATMF 'Gross of WHVAT
            .WHTaxAmount = Me._WHTaxAmount
            .WHVATAmount = Me._WHVATAmount
            .ReturnAmount = Me._ReturnAmount
            .Participant = Me._Participant
            .AmountForApplication = Me._AmountForApplication
            .CreatedDMCM = Me._CreatedDMCM
            .CreatedOR = Me._CreatedOR
            .WHTaxDefault = Me._WHTaxDefault
            .WHVATDefault = Me._WHVATDefault
            .AppliedBaseDefaultMF = Me._AppliedBaseDefaultMF ' Gross of WHTax
            .AppliedBaseDefaultMFV = Me._AppliedBaseDefaultMFV ' Gross of WVAT
            .DateComputationforDefault = Me._DateComputationForDefault
            .DMCMCtr = Me._DMCMCtr
            .dicDMCMTrans = Me._dicDMCMTrans
        End With

        Return MarketFeesOffsetting
    End Function

    Private Function CreateDebitCreditMemo(ByVal DMCMTransactionType As EnumDMCMTransactionType, ByVal BaseAmount As Decimal, ByVal VATAmount As Decimal, _
                                           ByVal BaseInvoice As WESMBillSummary, ByVal VATInvoice As WESMBillSummary, Optional ByVal isDefault As Boolean = False) As DebitCreditMemo
        Dim _itmDMCM As New DebitCreditMemo

        With _itmDMCM

            If BaseInvoice Is Nothing Then
                .BillingPeriod = VATInvoice.BillPeriod
                .DueDate = VATInvoice.DueDate
            Else
                .BillingPeriod = BaseInvoice.BillPeriod
                .DueDate = BaseInvoice.DueDate
            End If

            .ChargeType = EnumChargeType.MF
            .IDNumber = Me._Participant.IDNumber
            .TransType = DMCMTransactionType
            .DMCMNumber = _DMCMCtr
            .TotalAmountDue = BaseAmount + VATAmount


            Select Case DMCMTransactionType
                Case EnumDMCMTransactionType.PaymentSetupWithholding
                    If BaseAmount <> 0 Then
                        .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, EWTReceivable, BaseAmount, 0))
                    End If

                    If VATAmount <> 0 Then
                        .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, EWVReceivableCode, VATAmount, 0))
                    End If

                    If BaseAmount + VATAmount <> 0 Then
                        .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, ClearingAccountCode, 0, BaseAmount + VATAmount))
                    End If

                    .EWT = BaseAmount
                    .EWV = VATAmount

                    If isDefault = True Then
                        .Particulars = "Default"
                    End If
                Case EnumDMCMTransactionType.PaymentSetupOfDefaultInterestMFMFV 'Gross of Withholding Tax/Withholding VAT for Default Interest
                    '*************************************************************
                    ' Check here for missing amount in dmcm details 08/18/2014
                    '*************************************************************

                    .Others = BaseAmount + VATAmount

                    .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, DefaultInterestMarketFees, 0, BaseAmount))
                    If VATAmount <> 0 Then
                        .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, MarketFeesOutputTaxCode, 0, VATAmount))
                    End If
                    .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, CreditCode, BaseAmount + VATAmount, 0))
                Case EnumDMCMTransactionType.PaymentMFandMFVATDefaultInterest ' Application of Default Interest to Clearing Account (Net of Withholding Tax/Withholding Vat)
                    .Others = BaseAmount + VATAmount

                    .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, ClearingAccountCode, BaseAmount + VATAmount, 0, BaseInvoice.INVDMCMNo, BaseInvoice.SummaryType, _
                                                                Me._Participant))
                    .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, CreditCode, 0, BaseAmount + VATAmount, BaseInvoice.INVDMCMNo, BaseInvoice.SummaryType, _
                                                                Me._Participant))
                Case EnumDMCMTransactionType.PaymentOffsettingOfReceivableMF ' Application of Market Fees (Base Amount) To Clearing amount (Net of Withholding Tax)
                    .Vatable = BaseAmount
                    .VAT = VATAmount

                    .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, CreditCode, 0, BaseAmount, BaseInvoice.INVDMCMNo, BaseInvoice.SummaryType, _
                                                                Me._Participant))

                    If VATAmount <> 0 Then
                        .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, CreditCode, 0, VATAmount, VATInvoice.INVDMCMNo, VATInvoice.SummaryType, _
                                                                Me._Participant))
                    End If

                    .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, ClearingAccountCode, BaseAmount + VATAmount, 0, BaseInvoice.INVDMCMNo, BaseInvoice.SummaryType, _
                                                                Me._Participant))
                    'Case EnumDMCMTransactionType.PaymentOffsettingOfReceivableMFV ' Application of VAT on Market Fees To Clearing amount (Net of Withholding VAT)
                    '    .VAT = VATAmount
                    '    .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, CreditCode, 0, VATAmount, VATInvoice.INVDMCMNo, VATInvoice.SummaryType, _
                    '                                                Me._Participant))
                    '    .DMCMDetails.Add(New DebitCreditMemoDetails(.DMCMNumber, ClearingAccountCode, VATAmount, 0, VATInvoice.INVDMCMNo, VATInvoice.SummaryType, _
                    '                                                Me._Participant))
            End Select
        End With

        Me._dicDMCMTrans.Add(_DMCMCtr, DMCMTransactionType)
        _DMCMCtr += 1
        Return _itmDMCM
    End Function

    Private Function CreateOfficialReceipt(ByVal baseInvoice As WESMBillSummary, ByVal lstDebitCredit As List(Of DebitCreditMemo), _
                                           ByVal BaseDefault As Decimal, ByVal BaseAmount As Decimal, _
                                           ByVal BaseDefaultWTax As Decimal, ByVal BaseDefaultWVAT As Decimal, _
                                           ByVal BaseAmountWTax As Decimal, _
                                           ByVal VATDefault As Decimal, ByVal VATAmount As Decimal, ByVal VATWVAT As Decimal) As OfficialReceiptMain
        Dim _itmOR As New OfficialReceiptMain
        With _itmOR
            .ORDate = CDate(FormatDateTime(SystemDate, DateFormat.ShortDate))
            .ORNo = _ORCounter
            .TransactionType = EnumORTransactionType.PaymentMarketFees

            .Remarks = baseInvoice.INVDMCMNo.ToString & " through offsetting with the documents "
            .IDNumber = baseInvoice.IDNumber.IDNumber

            For Each _itmDMCM In lstDebitCredit
                .Remarks &= EnumSummaryType.DMCM.ToString & "-" & _itmDMCM.DMCMNumber & " "
            Next

            'Entries for OR
            .Amount = Math.Abs(BaseDefault + BaseAmount + VATDefault + VATAmount + BaseDefaultWTax + BaseDefaultWVAT + VATWVAT + BaseAmountWTax)

            .ListORDetails.Add(New OfficialReceiptDetails(.ORNo, ClearingAccountCode, "ClearingAccount", .Amount, 0))
            If BaseAmount + BaseDefault <> 0 Then
                .ListORDetails.Add(New OfficialReceiptDetails(.ORNo, MarketTransFeesCode, "BaseAmountAndDefault", 0, BaseAmount + BaseDefault))
            End If

            If BaseAmountWTax + BaseDefaultWTax <> 0 Then
                .ListORDetails.Add(New OfficialReceiptDetails(.ORNo, EWTReceivable, "BaseAmountAndDefaultWTax", 0, BaseAmountWTax + BaseDefaultWTax))
            End If

            If VATAmount + VATDefault <> 0 Then
                .ListORDetails.Add(New OfficialReceiptDetails(.ORNo, MarketFeesOutputTaxCode, "VATAmountAndVATDefault", 0, VATAmount + VATDefault))
            End If

            If VATWVAT + BaseDefaultWVAT <> 0 Then
                .ListORDetails.Add(New OfficialReceiptDetails(.ORNo, EWVReceivableCode, "BaseAmountAndDefaultWVAT", 0, VATWVAT + BaseDefaultWVAT))
            End If

            'invoice number tracking in OR
            .WESMSummary = baseInvoice
        End With
        Return _itmOR
    End Function
End Class
