'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmSTLNotice
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     November 15, 2012
'Development Group:      Software Development and Support Division
'Description:            Graphical Interface to display Settlement Notice Report
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description

Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementLogic

Public Class frmSTLNotice
    Private WbillHelper As WESMBillHelper
    Private STLNoticeHeader As New List(Of STLNotice)
    Private _lstParticipants As New List(Of AMParticipants)
    Private _lstBillPeriod As New List(Of CalendarBillingPeriod)

    Private dicCollectionNoToDate As New Dictionary(Of Long, Date)
    Private dicPaymentNoToDate As New Dictionary(Of String, Date)

    Private _isViewing As Boolean
    Public Property isViewing() As Boolean
        Get
            Return _isViewing
        End Get
        Set(ByVal value As Boolean)
            _isViewing = value
        End Set
    End Property

    Private Sub frmSTLNotice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        WbillHelper = WESMBillHelper.GetInstance
        WbillHelper.ConnectionString = AMModule.ConnectionString
        WbillHelper.UserName = AMModule.UserName

        dicCollectionNoToDate = Me.WbillHelper.GetCollectionNumberAllocDate()
        dicPaymentNoToDate = Me.WbillHelper.GetPaymentAllocDate()

        'Populate ComboBox with Dates
        _lstParticipants = Me.WbillHelper.GetAMParticipants()
        Me.cbo_date.DataSource = Me.WbillHelper.GetSTLDates()
        _lstBillPeriod = Me.WbillHelper.GetCalendarBP()

    End Sub

    Private Sub cmd_Generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Generate.Click
        Try
            If Me.cbo_date.SelectedIndex = -1 Then
                Throw New ApplicationException("No Available data for Settlement Notice.")
                Exit Sub
            End If

            'Get STL Notice for the selected transaction date
            STLNoticeHeader = Me.WbillHelper.GetSTLNotice(CDate(cbo_date.SelectedItem.ToString))

            'Populate ListBox with Participant ID
            Me.lstbx_Participants.DataSource = (From x In STLNoticeHeader _
                                                Select x.Participant.ParticipantID Distinct _
                                                Order By ParticipantID).ToList
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub lstbx_Participants_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstbx_Participants.SelectedIndexChanged
        Try
            If Me.lstbx_Participants.SelectedIndex = -1 Then
                'Throw Error Here
                Exit Sub
            End If

            'Transaction Date (Date the records are saved on STL Notice Table)
            'Dim TransactionDate As Date = CDate(FormatDateTime(DateAdd(DateInterval.Day, 1, CDate(Me.cbo_date.SelectedItem.ToString)), DateFormat.ShortDate))
            Dim TransactionDate As Date = CDate(FormatDateTime(CDate(Me.cbo_date.SelectedItem.ToString), DateFormat.ShortDate))

            '1st day of Transaction
            'Dim TransactionEnd As Date = CDate(FormatDateTime(TransactionDate, DateFormat.ShortDate)).AddMonths(1).AddDays(-1)
            Dim TransactionEnd As Date = CDate(FormatDateTime(CDate(Me.cbo_date.SelectedItem.ToString), DateFormat.ShortDate))

            'As of Last Day of Month of selected date
            'Get Last day of month
            Dim FirstDayOfMonth As New Long

            Dim TransactionBegin As New Date(TransactionDate.Year, TransactionDate.Month, 1) ' Now.Day)

            Me.gbox_Details.Text = "Settlement Notice Details for " & Me.lstbx_Participants.SelectedItem.ToString & " for (" & TransactionDate & ")"

            Dim _STLSelected = (From x In STLNoticeHeader _
                                Where x.Participant.ParticipantID = Me.lstbx_Participants.SelectedItem.ToString _
                                Select x).FirstOrDefault

            ' Reset Notice Details to Nothing
            _STLSelected.STLNoticeDetails.Clear()

            '**********************************
            '   To Get WESM Bills
            '**********************************

            '   1. Get Current BP first from WESM Invoices
            Dim _CurrentWESMBills = Me.WbillHelper.GetWESMBills(TransactionBegin, TransactionEnd, _STLSelected.Participant.IDNumber)

            '   2. Check Child to Parent Offsetting for the Selected Billing Period
            Dim _C2POffsetting = Me.WbillHelper.GetParentChildMapping(_STLSelected.Participant.IDNumber.ToString)
            '       - Get Offsetting of Participant for Current Billing Period (List Child Participants)
            Dim _WESMBillToList As New List(Of WESMBill)

            Dim _GetBP = (From x In _CurrentWESMBills _
                          Select x.BillingPeriod Distinct).ToList

            For Each itmBP In _GetBP
                Dim _itmBP = itmBP
                Dim _lstChild = (From x In _C2POffsetting _
                             Where x.Status = 1 _
                                   And x.ParentFlag = 0 _
                                   And x.BillPeriod = _itmBP _
                             Select x.IDNumber Distinct).ToList

                If _lstChild.Count <> 0 Then
                    For Each itmChild In _lstChild
                        _WESMBillToList.AddRange(Me.WbillHelper.GetWESMBills(TransactionBegin, TransactionEnd, itmChild))
                    Next
                    'If Participant has Child, Add Child WESM Bills to List
                Else
                    _WESMBillToList.AddRange(_CurrentWESMBills)
                End If
            Next

            'Add Current WESM Bills to the STL Notice Details
            _STLSelected.STLNoticeDetails.AddRange(Me.AddCurrentTransaction(_WESMBillToList))

            '**********************************
            '   To Get Offsetting
            '**********************************
            'Get WESM Bill Offsetting
            'Get Participant's List of WESM Bills
            Dim _WESMBillsSummary = Me.WbillHelper.GetWESMBillSummaryPerParticipant(_STLSelected.Participant.IDNumber)

            _WESMBillsSummary = (From x In _WESMBillsSummary _
                                 Select x).ToList

            'Get Distinct Group Numbers
            Dim _lstGroupNo = (From x In _WESMBillsSummary _
                               Select x.WESMBillSummaryNo Distinct).ToList

            If _lstGroupNo.Count <> 0 Then
                Dim _GetOffsetting = Me.WbillHelper.GetWESMBillOffsetDetails(_lstGroupNo)
                ' Removed dates by JCLP 20140826
                _GetOffsetting = (From x In _GetOffsetting _
                                    Where x.TransDate >= TransactionBegin _
                                    And x.TransDate <= TransactionEnd _
                                    Select x).ToList

                _STLSelected.STLNoticeDetails.AddRange(Me.AddOffsetting(_GetOffsetting))
            End If

            '**********************************
            '   To Get Settlements
            '**********************************

            '**********************************
            '      Get Collections
            '**********************************
            Dim _MonthCollection = Me.WbillHelper.GetCollections(TransactionBegin, TransactionEnd, _STLSelected.Participant.IDNumber, False)
            Dim _MonthCollectionMonitoring = Me.WbillHelper.GetCollectionMonitoring(TransactionBegin, TransactionEnd, _STLSelected.Participant.IDNumber)
            _STLSelected.STLNoticeDetails.AddRange(Me.AddCollectionTransactions(_MonthCollection, _MonthCollectionMonitoring))

            '**********************************
            '       Get Payments
            '   With Values:
            '       - Per Billing Period Allocation
            '       - Allocation per Participant
            '       - Per Summary Allocation
            '**********************************
            Dim _MonthPayment = Me.WbillHelper.GetPaymentAllocation(TransactionBegin, TransactionEnd, _STLSelected.Participant.IDNumber.ToString)
            If _MonthPayment IsNot Nothing Then
                _STLSelected.STLNoticeDetails.AddRange(Me.AddPaymentTransactions(_MonthPayment))
            End If

            Dim _ForEnding As New List(Of WESMBillSummary)

            If Me.cbo_date.SelectedIndex = Me.cbo_date.Items.Count - 1 And Me.cbo_date.Items.Count <> 1 Then
                Dim endingbalance As New List(Of STLNotice)
                endingbalance = WbillHelper.GetSTLNotice(CDate(Me.cbo_date.Items(Me.cbo_date.SelectedIndex - 1).ToString))

                Dim _tmpTransactionDate As Date = CDate(FormatDateTime(CDate(Me.cbo_date.Items(Me.cbo_date.SelectedIndex - 1).ToString), DateFormat.ShortDate))

                'Dim ParticipantEndingBalance As New List(Of STLBeginningBalance)
                Dim ParticipantEndingBalance As New STLNotice
                ParticipantEndingBalance = (From x In endingbalance _
                                            Where x.Participant.IDNumber = _STLSelected.Participant.IDNumber _
                                            And x.TransactionDate = _tmpTransactionDate _
                                            Select x).FirstOrDefault

                If ParticipantEndingBalance IsNot Nothing Then
                    For Each itmEndBalance In ParticipantEndingBalance.BalanceDetails
                        Dim WESMSummary As New WESMBillSummary
                        With WESMSummary
                            .BillPeriod = itmEndBalance.BillPeriod
                            .DueDate = itmEndBalance.DueDate
                            .NewDueDate = itmEndBalance.NewDueDate
                            .SummaryType = itmEndBalance.SummaryType
                            .BeginningBalance = itmEndBalance.BeginningBalance
                            .EndingBalance = itmEndBalance.EndingBalance
                            .INVDMCMNo = itmEndBalance.INVDMCMNo
                            .ChargeType = itmEndBalance.ChargeType
                        End With
                        _ForEnding.Add(WESMSummary)
                    Next
                Else
                    Dim WESMSummary As New WESMBillSummary
                    With WESMSummary
                        .BillPeriod = 0
                        .DueDate = Date.Now
                        .NewDueDate = Date.Now
                        .SummaryType = EnumSummaryType.INV
                        .BeginningBalance = 0
                        .EndingBalance = 0
                        .INVDMCMNo = ""
                        .ChargeType = EnumChargeType.E
                    End With
                    _ForEnding.Add(WESMSummary)
                End If
            Else

                _ForEnding = (From x In _WESMBillsSummary _
              Where x.EndingBalance <> 0 _
              Select x).ToList

                For Each itmBP In _GetBP
                    Dim _itmBP = itmBP
                    _ForEnding.AddRange((From x In _WESMBillsSummary _
                                  Where x.BillPeriod = _itmBP _
                                  And x.EndingBalance = 0 _
                                  Select x).ToList)
                Next
            End If




            '***********************************************************************
            '   Ending Balances _EndingBalances declared on the offsetting set
            '***********************************************************************
            _STLSelected.STLNoticeDetails.AddRange(Me.AddEndingBalances(_ForEnding))

            'Display STL Notice to DataTable
            Me.dgView.DataSource = GenerateSTLDataTable(_STLSelected)
            For x = 0 To Me.dgView.Columns.Count - 1
                dgView.Columns(x).SortMode = DataGridViewColumnSortMode.NotSortable
                'If x >= 4 Then
                '    dgView.Columns(x).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'End If
            Next

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    'Get Current Transactions
    Private Function AddCurrentTransaction(ByVal lstCurrentBills As List(Of WESMBill)) As List(Of STLNoticeDetails)
        Dim retWESMBill As New List(Of STLNoticeDetails)

        'Get Distinct Invoice Numbers
        Dim dInvoiceNumber = (From x In lstCurrentBills _
                              Select x.InvoiceNumber Distinct).ToList

        For Each itmdInvoiceNo In dInvoiceNumber
            Dim _itmdInvoiceNo = itmdInvoiceNo


            'Get invoices for the Invoice No
            Dim _WESMBills = (From x In lstCurrentBills _
                              Where x.InvoiceNumber = _itmdInvoiceNo _
                              Select x).ToList

            For Each itmWESMBill In _WESMBills
                Dim _itmWESMbill = itmWESMBill

                Dim itmSTLDetails As New STLNoticeDetails
                With itmSTLDetails
                    .BillPeriod = _WESMBills.FirstOrDefault.BillingPeriod
                    .CategoryType = EnumSTLCategory.CurrentTransaction
                    .INVDMCMNo = _WESMBills.FirstOrDefault.InvoiceNumber
                    .InvoiceDate = _WESMBills.FirstOrDefault.InvoiceDate
                    .Participant.IDNumber = CStr(_WESMBills.FirstOrDefault.IDNumber)  '08/27/2013

                    If itmWESMBill.Amount < 0 Then
                        'Go for AR Set of Transaction Type
                        Select Case itmWESMBill.ChargeType
                            Case EnumChargeType.E
                                .STLTransactionType = EnumSTLTransactionType.AREnergy
                                .Amount = itmWESMBill.Amount
                                .Remarks_1 = "Energy"
                            Case EnumChargeType.EV
                                .STLTransactionType = EnumSTLTransactionType.ARVAT
                                .Amount = itmWESMBill.Amount
                                .Remarks_1 = "Energy"
                            Case EnumChargeType.MF, EnumChargeType.MFV
                                .STLTransactionType = EnumSTLTransactionType.ARMF
                                .Amount += itmWESMBill.Amount
                                .Remarks_1 = "Market Fees"
                        End Select
                    ElseIf itmWESMBill.Amount > 0 Then
                        'Go for AP Set of Transaction Type
                        Select Case itmWESMBill.ChargeType
                            Case EnumChargeType.E
                                .STLTransactionType = EnumSTLTransactionType.APEnergy
                                .Amount = itmWESMBill.Amount
                                .Remarks_1 = "Energy"
                            Case EnumChargeType.EV
                                .STLTransactionType = EnumSTLTransactionType.APVAT
                                .Amount = itmWESMBill.Amount
                                .Remarks_1 = "Energy"
                            Case EnumChargeType.MF, EnumChargeType.MFV
                                .STLTransactionType = EnumSTLTransactionType.APMF
                                .Amount += itmWESMBill.Amount
                                .Remarks_1 = "Market Fees"
                        End Select
                    Else
                        'If Amount is Zero
                        Continue For
                    End If

                    '08/27/2013 Vloody
                    .Remarks_2 = (From x In _lstParticipants _
                                  Where x.IDNumber = CStr(_itmWESMbill.RegistrationID) _
                                  Select x.ParticipantID).FirstOrDefault
                    .SummaryType = EnumSummaryType.INV
                End With
                'Add to List
                retWESMBill.Add(itmSTLDetails)
            Next



        Next


        Return retWESMBill
    End Function

    Private Function AddOffsetting(ByVal lstOffsetDetails As List(Of OffsetP2PC2CDetails)) As List(Of STLNoticeDetails)
        Dim retSTLOffsetDetails As New List(Of STLNoticeDetails)

        'Get Distinct DMCM Numbers for the Offsetting Details
        Dim _lstDMCMNumber = (From x In lstOffsetDetails _
                              Select x.DMCMNumber Distinct).ToList

        If _lstDMCMNumber.Count > 0 Then

            'Get DMCM list
            Dim _lstDMCMMain = WbillHelper.GetDebitCreditMemoMain(_lstDMCMNumber)

            'get Invoices
            Dim _lstInvoices = WbillHelper.GetWESMBills()

            Dim grpCtr As Integer = 0

            For Each itmDMCMMain In _lstDMCMMain
                'Get Details
                Dim _itmDetails = itmDMCMMain.DMCMDetails
                Dim _itmDMCMMain = itmDMCMMain

                Dim InvAmountPositive As Boolean = True

                'Get WESM Bill Summary No of the DMCM
                Dim _WESMSummaryNo = (From x In lstOffsetDetails _
                                      Where x.DMCMNumber = _itmDMCMMain.DMCMNumber _
                                      Select x.WESMBillSummaryNo).ToList

                Dim WESMInv = WbillHelper.GetWESMBillSummary(_WESMSummaryNo, False).FirstOrDefault
                Dim _InvoiceNumber = WESMInv.INVDMCMNo

                If WESMInv.BeginningBalance > 0 Then
                    InvAmountPositive = True
                Else
                    InvAmountPositive = False
                End If

                'Dim _chkAmount = (From x In _itmDetails _
                '                  Where x.InvDMCMNo = _InvoiceNumber _
                '                  Select x.AccountCode).FirstOrDefault

                'If _chkAmount = DebitCode Then
                '    InvAmountPositive = True
                'Else
                '    InvAmountPositive = False
                'End If

                Dim chargeGroup As EnumChargeType
                chargeGroup = itmDMCMMain.ChargeType
                grpCtr += 1
                For Each itmDMCMDetails In _itmDetails.OrderByDescending(Function(x) x.InvDMCMNo)
                    Dim _itmDMCMDetails = itmDMCMDetails
                    Dim stlDetails As New STLNoticeDetails

                    Dim invAmount As Decimal = 0
                    'get Invoice

                    Dim WESMInvoice As New WESMBill

                    If _itmDMCMDetails.InvDMCMNo <> _InvoiceNumber Then
                        WESMInvoice = (From x In _lstInvoices _
                                       Where x.InvoiceNumber = _itmDMCMDetails.InvDMCMNo _
                                       Select x).FirstOrDefault
                    End If


                    Dim cParticipant = (From x In _lstParticipants _
                                        Where x.IDNumber = _itmDMCMDetails.IDNumber.IDNumber _
                                        Select x).FirstOrDefault

                    stlDetails.CategoryType = EnumSTLCategory.Offsetting

                    stlDetails.Amount = itmDMCMDetails.Debit + itmDMCMDetails.Credit 'WESMInvoice.Amount

                    'If InvAmountPositive = True Then
                    stlDetails.Amount *= -1D
                    'End If

                    If _itmDMCMDetails.InvDMCMNo <> _InvoiceNumber Then
                        If WESMInvoice IsNot Nothing Then
                            stlDetails.BillPeriod = WESMInvoice.BillingPeriod
                            stlDetails.INVDMCMNo = WESMInvoice.InvoiceNumber
                            stlDetails.InvoiceDate = WESMInvoice.InvoiceDate
                            stlDetails.TransactionDate = WESMInvoice.InvoiceDate

                            If WESMInvoice.Amount > 0 Then
                                InvAmountPositive = True
                            Else
                                InvAmountPositive = False
                            End If

                        Else
                            Dim DMCM = WbillHelper.GetDebitCreditMemoMain(CLng(_itmDMCMDetails.InvDMCMNo)).FirstOrDefault
                            stlDetails.BillPeriod = DMCM.BillingPeriod
                            stlDetails.INVDMCMNo = CStr(DMCM.DMCMNumber)
                            stlDetails.InvoiceDate = DMCM.UpdatedDate
                            stlDetails.TransactionDate = DMCM.UpdatedDate
                        End If

                    Else
                        stlDetails.TransactionDate = _itmDMCMDetails.UpdatedDate
                    End If



                    stlDetails.Participant = cParticipant
                    stlDetails.SummaryType = _itmDMCMDetails.SummaryType

                    stlDetails.Remarks_2 = cParticipant.ParticipantID
                    stlDetails.Remarks_3 = itmDMCMMain.DMCMNumber.ToString

                    If InvAmountPositive = True Then
                        Select Case chargeGroup
                            Case EnumChargeType.E
                                stlDetails.STLTransactionType = EnumSTLTransactionType.APEnergy
                                stlDetails.Remarks_1 = "Energy"
                            Case EnumChargeType.EV
                                stlDetails.STLTransactionType = EnumSTLTransactionType.APVAT
                                stlDetails.Remarks_1 = "Energy"
                            Case EnumChargeType.MF, EnumChargeType.MFV
                                stlDetails.STLTransactionType = EnumSTLTransactionType.APMF
                                stlDetails.Remarks_1 = "Market Fees"
                        End Select

                        If _itmDMCMDetails.AccountCode = DebitCode Then
                            If _itmDMCMDetails.InvDMCMNo = _InvoiceNumber Then
                                stlDetails.Amount *= 1D
                            Else
                                stlDetails.Amount *= -1D
                            End If
                        End If

                        'If _itmDMCMDetails.AccountCode = DebitCode And _itmDMCMDetails.InvDMCMNo <> _InvoiceNumber Then
                        '    If _itmDMCMDetails.Debit <> _InvoiceNumber Then
                        '    Else
                        '        stlDetails.Amount *= -1
                        '    End If
                        'Else
                        '    stlDetails.Amount *= -1
                        'End If
                    Else
                        'Invoice Charge Types
                        Select Case chargeGroup
                            Case EnumChargeType.E
                                stlDetails.STLTransactionType = EnumSTLTransactionType.AREnergy
                                stlDetails.Remarks_1 = "Energy"
                            Case EnumChargeType.EV
                                stlDetails.STLTransactionType = EnumSTLTransactionType.ARVAT
                                stlDetails.Remarks_1 = "Energy"
                            Case EnumChargeType.MF, EnumChargeType.MFV
                                stlDetails.STLTransactionType = EnumSTLTransactionType.ARMF
                                stlDetails.Remarks_1 = "Market Fees"
                        End Select

                        If _itmDMCMDetails.AccountCode = CreditCode Then
                            If _itmDMCMDetails.InvDMCMNo = _InvoiceNumber Then
                                stlDetails.Amount *= 1D
                            Else
                                stlDetails.Amount *= -1D
                            End If
                        End If

                        'If _itmDMCMDetails.AccountCode = CreditCode And _itmDMCMDetails.InvDMCMNo <> _InvoiceNumber Then
                        '    If _itmDMCMDetails.Debit <> _InvoiceNumber Then
                        '    Else
                        '        stlDetails.Amount *= -1
                        '    End If
                        'End If
                    End If

                    'If WESMInvoice.Amount > 0 Then
                    '    'Invoice Charge Types
                    '    Select Case WESMInvoice.ChargeType
                    '        Case EnumChargeType.E
                    '            stlDetails.STLTransactionType = EnumSTLTransactionType.APEnergy
                    '            stlDetails.Remarks_1 = "Energy"
                    '        Case EnumChargeType.EV
                    '            stlDetails.STLTransactionType = EnumSTLTransactionType.APVAT
                    '            stlDetails.Remarks_1 = "Energy"
                    '        Case EnumChargeType.MF, EnumChargeType.MFV
                    '            stlDetails.STLTransactionType = EnumSTLTransactionType.APMF
                    '            stlDetails.Remarks_1 = "Market Fees"
                    '    End Select
                    'Else
                    '    'Invoice Charge Types
                    '    Select Case WESMInvoice.ChargeType
                    '        Case EnumChargeType.E
                    '            stlDetails.STLTransactionType = EnumSTLTransactionType.AREnergy
                    '            stlDetails.Remarks_1 = "Energy"
                    '        Case EnumChargeType.EV
                    '            stlDetails.STLTransactionType = EnumSTLTransactionType.ARVAT
                    '            stlDetails.Remarks_1 = "Energy"
                    '        Case EnumChargeType.MF, EnumChargeType.MFV
                    '            stlDetails.STLTransactionType = EnumSTLTransactionType.ARMF
                    '            stlDetails.Remarks_1 = "Market Fees"
                    '    End Select
                    'End If
                    retSTLOffsetDetails.Add(stlDetails)






                Next


                'For Each itmDMCMNo In _lstDMCMNumber
                '    Dim _itmDMCMNo = itmDMCMNo
                '    'get Details of DMCM Number 

                '    '02062013
                '    'Dim _itmDetails = (From x In _lstDMCMDetails _
                '    '                   Where x.DMCMNumber = _itmDMCMNo _
                '    '                   And x.InvDMCMNo <> 0 _
                '    '                   Select x).ToList

                '    'Dim _itmDetails = (From x In _lstDMCMMain _
                '    '                   Where x.DMCMNumber = _itmDMCMNo _
                '    '                   Select x.DMCMDetails).ToList

                '    'Dim InvAmountPositive As Boolean = True

                '    'Dim _chkAmount = (From x In _itmDetails _
                '    '                  Where x.InvDMCMNo = 0 _
                '    '                  Select x.AccountCode).FirstOrDefault

                '    'If _chkAmount = DebitCode Then
                '    '    InvAmountPositive = True
                '    'Else
                '    '    InvAmountPositive = False
                '    'End If

                '    'Dim chargeGroup As EnumChargeType
                '    'grpCtr += 1
                '    'For Each itmDMCMDetails In _itmDetails
                '    '    Dim _itmDMCMDetails = itmDMCMDetails
                '    '    Dim stlDetails As New STLNoticeDetails

                '    '    Dim invAmount As Decimal = 0
                '    '    'get Invoice

                '    '    Dim WESMInvoice As New WESMBill

                '    '    If _itmDMCMDetails.InvDMCMNo <> 0 Then
                '    '        WESMInvoice = (From x In _lstInvoices _
                '    '                       Where x.InvoiceNumber = _itmDMCMDetails.InvDMCMNo _
                '    '                       Select x).FirstOrDefault
                '    '        chargeGroup = WESMInvoice.ChargeType
                '    '    End If


                '    '    Dim cParticipant = (From x In _lstParticipants _
                '    '                        Where x.IDNumber = _itmDMCMDetails.IDNumber.IDNumber _
                '    '                        Select x).FirstOrDefault

                '    '    stlDetails.CategoryType = EnumSTLCategory.Offsetting

                '    '    stlDetails.Amount = itmDMCMDetails.Debit + itmDMCMDetails.Credit 'WESMInvoice.Amount

                '    '    If WESMInvoice.Amount < 0 Then
                '    '        stlDetails.Amount *= -1D
                '    '    End If

                '    '    If _itmDMCMDetails.InvDMCMNo <> 0 Then
                '    '        stlDetails.BillPeriod = WESMInvoice.BillingPeriod
                '    '        stlDetails.INVDMCMNo = WESMInvoice.InvoiceNumber
                '    '        stlDetails.InvoiceDate = WESMInvoice.InvoiceDate
                '    '        stlDetails.TransactionDate = WESMInvoice.InvoiceDate
                '    '    Else
                '    '        stlDetails.TransactionDate = _itmDMCMDetails.UpdatedDate
                '    '    End If

                '    '    stlDetails.Participant = cParticipant
                '    '    stlDetails.SummaryType = EnumSummaryType.INV

                '    '    stlDetails.Remarks_2 = cParticipant.ParticipantID
                '    '    stlDetails.Remarks_3 = _itmDMCMNo.ToString

                '    '    If InvAmountPositive = True Then
                '    '        Select Case chargeGroup
                '    '            Case EnumChargeType.E
                '    '                stlDetails.STLTransactionType = EnumSTLTransactionType.APEnergy
                '    '                stlDetails.Remarks_1 = "Energy"
                '    '            Case EnumChargeType.EV
                '    '                stlDetails.STLTransactionType = EnumSTLTransactionType.APVAT
                '    '                stlDetails.Remarks_1 = "Energy"
                '    '            Case EnumChargeType.MF, EnumChargeType.MFV
                '    '                stlDetails.STLTransactionType = EnumSTLTransactionType.APMF
                '    '                stlDetails.Remarks_1 = "Market Fees"
                '    '        End Select
                '    '        If _itmDMCMDetails.AccountCode = DebitCode And _itmDMCMDetails.InvDMCMNo <> 0 Then
                '    '            If _itmDMCMDetails.Debit <> 0 Then
                '    '            Else
                '    '                stlDetails.Amount *= -1
                '    '            End If

                '    '        End If
                '    '    Else
                '    '        'Invoice Charge Types
                '    '        Select Case chargeGroup
                '    '            Case EnumChargeType.E
                '    '                stlDetails.STLTransactionType = EnumSTLTransactionType.AREnergy
                '    '                stlDetails.Remarks_1 = "Energy"
                '    '            Case EnumChargeType.EV
                '    '                stlDetails.STLTransactionType = EnumSTLTransactionType.ARVAT
                '    '                stlDetails.Remarks_1 = "Energy"
                '    '            Case EnumChargeType.MF, EnumChargeType.MFV
                '    '                stlDetails.STLTransactionType = EnumSTLTransactionType.ARMF
                '    '                stlDetails.Remarks_1 = "Market Fees"
                '    '        End Select
                '    '        If _itmDMCMDetails.AccountCode = CreditCode And _itmDMCMDetails.InvDMCMNo <> 0 Then
                '    '            If _itmDMCMDetails.Debit <> 0 Then
                '    '            Else
                '    '                stlDetails.Amount *= -1
                '    '            End If
                '    '        End If
                '    '    End If

                '    '    'If WESMInvoice.Amount > 0 Then
                '    '    '    'Invoice Charge Types
                '    '    '    Select Case WESMInvoice.ChargeType
                '    '    '        Case EnumChargeType.E
                '    '    '            stlDetails.STLTransactionType = EnumSTLTransactionType.APEnergy
                '    '    '            stlDetails.Remarks_1 = "Energy"
                '    '    '        Case EnumChargeType.EV
                '    '    '            stlDetails.STLTransactionType = EnumSTLTransactionType.APVAT
                '    '    '            stlDetails.Remarks_1 = "Energy"
                '    '    '        Case EnumChargeType.MF, EnumChargeType.MFV
                '    '    '            stlDetails.STLTransactionType = EnumSTLTransactionType.APMF
                '    '    '            stlDetails.Remarks_1 = "Market Fees"
                '    '    '    End Select
                '    '    'Else
                '    '    '    'Invoice Charge Types
                '    '    '    Select Case WESMInvoice.ChargeType
                '    '    '        Case EnumChargeType.E
                '    '    '            stlDetails.STLTransactionType = EnumSTLTransactionType.AREnergy
                '    '    '            stlDetails.Remarks_1 = "Energy"
                '    '    '        Case EnumChargeType.EV
                '    '    '            stlDetails.STLTransactionType = EnumSTLTransactionType.ARVAT
                '    '    '            stlDetails.Remarks_1 = "Energy"
                '    '    '        Case EnumChargeType.MF, EnumChargeType.MFV
                '    '    '            stlDetails.STLTransactionType = EnumSTLTransactionType.ARMF
                '    '    '            stlDetails.Remarks_1 = "Market Fees"
                '    '    '    End Select
                '    '    'End If
                '    '    retSTLOffsetDetails.Add(stlDetails)
                'Next
            Next
        End If
        Return retSTLOffsetDetails
    End Function

    'Get Collections for the Month
    Private Function AddCollectionTransactions(ByVal lstCollections As List(Of Collection), ByVal lstColMonitoring As List(Of CollectionMonitoring)) As List(Of STLNoticeDetails)
        Dim retSTLCollection As New List(Of STLNoticeDetails)

        'Groupings by Allocation Date then Per invoice number

        'Get Distinct Allocation Date
        Dim lstAllocationDate = (From x In lstCollections _
                                 Select x.AllocationDate Distinct).ToList

        'Loop Allocation Date
        For Each itmAllocationDate In lstAllocationDate
            Dim _itmAllocationDate = itmAllocationDate

            'Get Collection Number per Allocation Date
            Dim lstCollectionAllocation = (From x In lstCollections _
                                  Where x.AllocationDate = _itmAllocationDate _
                                  Select x).ToList

            'Loop Collections
            For Each itmCollectionAllocation In lstCollectionAllocation
                Dim _itmCollectionAllocation = itmCollectionAllocation

                'Get distinct Reference Numbers
                Dim dRefNumber = (From x In _itmCollectionAllocation.ListOfCollectionAllocation _
                                  Select x.WESMBillSummaryNo.INVDMCMNo, x.WESMBillSummaryNo.SummaryType Distinct).ToList

                'Loop Reference Numbers
                For Each itmRefNumber In dRefNumber
                    Dim _itmRefNumber = itmRefNumber

                    'Get Distinct Reference Numbers (Invoice Numbers)
                    Dim _lstRefTransaction = (From x In _itmCollectionAllocation.ListOfCollectionAllocation _
                                              Where x.WESMBillSummaryNo.INVDMCMNo = _itmRefNumber.INVDMCMNo _
                                                    And x.WESMBillSummaryNo.SummaryType = _itmRefNumber.SummaryType _
                                              Select x _
                                              Order By x.BillingPeriod Ascending).ToList


                    'Loop Collection Details per Reference Number
                    For Each itmRefTransaction In _lstRefTransaction
                        Dim _colSTLDetails As New STLNoticeDetails

                        With _colSTLDetails
                            .Participant.IDNumber = _itmCollectionAllocation.IDNumber
                            .AllocationDate = _itmAllocationDate

                            .CategoryType = EnumSTLCategory.Collection
                            .Participant.IDNumber = itmCollectionAllocation.IDNumber

                            .BillPeriod = itmRefTransaction.BillingPeriod
                            .INVDMCMNo = itmRefTransaction.WESMBillSummaryNo.INVDMCMNo
                            .SummaryType = itmRefTransaction.WESMBillSummaryNo.SummaryType
                            .Participant.IDNumber = _itmCollectionAllocation.IDNumber
                            .Remarks_2 = (From x In _lstParticipants _
                                         Where x.IDNumber = _itmCollectionAllocation.IDNumber _
                                         Select x.ParticipantID).FirstOrDefault

                            Select Case itmRefTransaction.CollectionType
                                Case EnumCollectionType.Energy
                                    .STLTransactionType = EnumSTLTransactionType.AREnergy
                                    .Amount += itmRefTransaction.Amount
                                    .Remarks_1 = "Energy"
                                Case EnumCollectionType.DefaultInterestOnEnergy
                                    .STLTransactionType = EnumSTLTransactionType.ARDefaultEnergy
                                    .Amount += itmRefTransaction.Amount
                                    .Remarks_1 = "Energy"
                                Case EnumCollectionType.VatOnEnergy
                                    .STLTransactionType = EnumSTLTransactionType.ARVAT
                                    .Amount += itmRefTransaction.Amount
                                    .Remarks_1 = "Energy"
                                Case EnumCollectionType.VatOnMarketFees, EnumCollectionType.MarketFees
                                    .STLTransactionType = EnumSTLTransactionType.ARMF
                                    .Amount += itmRefTransaction.Amount
                                    .Remarks_1 = "Market Fees"
                                Case EnumCollectionType.DefaultInterestOnMF, EnumCollectionType.DefaultInterestOnVatOnMF
                                    .STLTransactionType = EnumSTLTransactionType.ARDefaultMF
                                    .Amount += itmRefTransaction.Amount
                                    .Remarks_1 = "Market Fees"
                                Case EnumCollectionType.WithholdingTaxOnMF
                                    .STLTransactionType = EnumSTLTransactionType.WTax
                                    .Amount += itmRefTransaction.Amount
                                    .Remarks_1 = "Market Fees"
                                Case EnumCollectionType.WithholdingVatonMF
                                    .STLTransactionType = EnumSTLTransactionType.WVAT
                                    .Amount += itmRefTransaction.Amount
                                    .Remarks_1 = "Market Fees"
                                Case EnumCollectionType.WithholdingTaxOnDefaultInterest
                                    .STLTransactionType = EnumSTLTransactionType.DefaultWTax
                                    .Amount += itmRefTransaction.Amount
                                    .Remarks_1 = "Market Fees"
                                Case EnumCollectionType.WithholdingVatonDefaultInterest
                                    .STLTransactionType = EnumSTLTransactionType.DefaultWVAT
                                    .Amount += itmRefTransaction.Amount
                                    .Remarks_1 = "Market Fees"
                            End Select

                            If _itmCollectionAllocation.CollectionCategory = EnumCollectionCategory.Drawdown Then
                                .Remarks_1 = "PR - Drawdown"
                            End If

                            retSTLCollection.Add(_colSTLDetails)
                        End With
                    Next
                    'End Loop Collection Details per Reference Number

                Next
                'End Loop Reference Number

            Next
            'End Loop Collections

            'Get Replenishment
            Dim _lstReplenishment = (From x In lstColMonitoring _
                                     Where x.AllocationDate = _itmAllocationDate _
                                     And x.TransType = EnumCollectionMonitoringType.TransferToPRReplenishment _
                                     Select x).ToList

            If _lstReplenishment.Count <> 0 Then
                Dim stlReplenishDetails As New STLNoticeDetails
                With stlReplenishDetails
                    .AllocationDate = _itmAllocationDate
                    .Amount = _lstReplenishment.Sum(Function(x As CollectionMonitoring) x.Amount)
                    .STLTransactionType = EnumSTLTransactionType.PRReplenishment
                    .Participant = _lstReplenishment.FirstOrDefault.IDNumber
                    .Remarks_1 = "PR - Replenishment"
                    .BillPeriod = 0
                End With
                retSTLCollection.Add(stlReplenishDetails)
            End If

        Next
        'End Loop Allocation Date

        Return retSTLCollection
    End Function

    'Get Payments for the Month
    Private Function AddPaymentTransactions(ByVal lstPayments As FuncAutoPaymentAllocationResult) As List(Of STLNoticeDetails)
        Dim retSTLPayments As New List(Of STLNoticeDetails)

        'Get Allocation date per payment
        Dim _lstAllocationDate = (From x In lstPayments.lstPerBillPeriodAllocation _
                                  Select x.CollectionAllocationDate Distinct).ToList

        'Loop Allocation date
        For Each itmAllocationDate In _lstAllocationDate
            Dim _itmAllocationDate = itmAllocationDate

            'Get Group code/s of participant
            Dim lstGroupCode = (From hdr In lstPayments.lstPerBillPeriodAllocation _
                                 Join dtls In lstPayments.lstPerParticipantAllocationViewing _
                                 On hdr.PaymentPerBPNo Equals dtls.PaymentBatchCode _
                                 Where hdr.CollectionAllocationDate = _itmAllocationDate _
                                 Select dtls.PaymentGroupCode Distinct).ToList

            'Loop Group Code to Get Participant's Allocation per summary
            For Each itmGroupcode In lstGroupCode
                Dim _itmGroupCode = itmGroupcode

                'Get invoice numbers for the group code
                Dim lstPerInvoice = (From x In lstPayments.lstPerSummaryAllocation _
                                     Where x.PaymentGroupCode = _itmGroupCode _
                                     Select x.WESMBillSummary.INVDMCMNo, x.WESMBillSummary.SummaryType Distinct).ToList

                'Create Per Account For List
                For Each itmPerInvoice In lstPerInvoice
                    Dim _itmPerInvoice = itmPerInvoice

                    'Get Invoice Transactions 
                    Dim lstInvTransaction = (From x In lstPayments.lstPerSummaryAllocation, y In lstPayments.lstPerBillPeriodAllocation _
                                             Where x.WESMBillSummary.INVDMCMNo = _itmPerInvoice.INVDMCMNo _
                                             And x.WESMBillSummary.SummaryType = _itmPerInvoice.SummaryType _
                                             And x.PaymentBatchCode = y.PaymentPerBPNo _
                                             And y.CollectionAllocationDate = _itmAllocationDate _
                                             Select x).ToList

                    'Create Entry for STL Payment List
                    For Each itmTransaction In lstInvTransaction
                        Dim STLPayment As New STLNoticeDetails
                        Dim AmountToSTL As Decimal = 0
                        With STLPayment
                            .BillPeriod = itmTransaction.WESMBillSummary.BillPeriod
                            .AllocationDate = _itmAllocationDate
                            .CategoryType = EnumSTLCategory.Payment
                            .Participant = itmTransaction.WESMBillSummary.IDNumber
                            .INVDMCMNo = itmTransaction.WESMBillSummary.INVDMCMNo
                            .SummaryType = itmTransaction.WESMBillSummary.SummaryType
                            .Remarks_2 = itmTransaction.WESMBillSummary.IDNumber.ParticipantID

                            AmountToSTL = itmTransaction.PaymentAmount

                            Select Case itmTransaction.PaymentType
                                'AR Market Fees and VAT on MF
                                Case EnumPaymentType.UnpaidMF, EnumPaymentType.UnpaidMFV
                                    If itmTransaction.PaymentAmount < 0 Then
                                        .STLTransactionType = EnumSTLTransactionType.ARMF
                                        .Remarks_1 = "Market Fees"
                                        'Add WHTax/WHVat to Get Gross
                                        If itmTransaction.PaymentType = EnumPaymentType.UnpaidMF Then
                                            Dim _WHTax = (From x In lstInvTransaction _
                                                          Where x.PaymentType = EnumPaymentType.UnpaidMF _
                                                          Select x.PaymentAmount).Sum

                                            AmountToSTL = _WHTax
                                        Else
                                            Dim _WHVAT = (From x In lstInvTransaction _
                                                          Where x.PaymentType = EnumPaymentType.UnpaidMFV _
                                                          Select x.PaymentAmount).Sum

                                            AmountToSTL = _WHVAT
                                        End If
                                    Else
                                        .STLTransactionType = EnumSTLTransactionType.APMF
                                        .Remarks_1 = "Market Fees"
                                    End If

                                    'AR Default On Market Fees
                                Case EnumPaymentType.UnpaidMFDefault, EnumPaymentType.UnpaidMFVDefault
                                    If itmTransaction.PaymentAmount < 0 Then
                                        .STLTransactionType = EnumSTLTransactionType.ARDefaultMF
                                        .Remarks_1 = "Market Fees"

                                        'Add DefaultWHTax/DefaultWHVat to Get Gross
                                        If itmTransaction.PaymentType = EnumPaymentType.UnpaidMFDefault Then
                                            Dim _WHTax = (From x In lstInvTransaction _
                                                          Where x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                                                          Select x.PaymentAmount).Sum

                                            AmountToSTL = _WHTax
                                        Else
                                            Dim _WHVAT = (From x In lstInvTransaction _
                                                          Where x.PaymentType = EnumPaymentType.UnpaidMFVDefault _
                                                          Select x.PaymentAmount).Sum

                                            AmountToSTL = _WHVAT
                                        End If
                                    Else
                                        .STLTransactionType = EnumSTLTransactionType.APDefaultMF
                                        .Remarks_1 = "Market Fees"
                                    End If

                                    'AR WHTax/WHVAT
                                Case EnumPaymentType.UnpaidMFWHTax
                                    .STLTransactionType = EnumSTLTransactionType.WTax
                                    .Remarks_1 = "Market Fees"
                                Case EnumPaymentType.WHTaxDefault
                                    .STLTransactionType = EnumSTLTransactionType.DefaultWTax
                                    .Remarks_1 = "Market Fees"
                                Case EnumPaymentType.WHVATDefault
                                    .STLTransactionType = EnumSTLTransactionType.DefaultWVAT
                                    .Remarks_1 = "Market Fees"
                                Case EnumPaymentType.UnpaidMFWHVAT
                                    .STLTransactionType = EnumSTLTransactionType.WVAT
                                    .Remarks_1 = "Market Fees"
                                    'AR Energy
                                Case EnumPaymentType.OffsetToCurrentReceivableEnergy, EnumPaymentType.OffsetOfPreviousReceivableEnergy
                                    .STLTransactionType = EnumSTLTransactionType.AREnergy
                                    .Remarks_1 = "Energy"
                                    'AR Default Interest
                                Case EnumPaymentType.OffsetOfPreviousReceivableEnergyDefault, EnumPaymentType.OffsetToCurrentReceivableEnergyDefault
                                    .STLTransactionType = EnumSTLTransactionType.ARDefaultEnergy
                                    .Remarks_1 = "Energy"
                                    'AR VAT
                                Case EnumPaymentType.OffsetOfPreviousReceivableVAT, EnumPaymentType.OffsetToCurrentReceivableVAT
                                    .STLTransactionType = EnumSTLTransactionType.ARVAT
                                    .Remarks_1 = "Energy"
                                    'AP Market Fees and VAT on MF
                                Case EnumPaymentType.PaymentMF, EnumPaymentType.PaymentMFV
                                    .STLTransactionType = EnumSTLTransactionType.APMF
                                    .Remarks_1 = "Market Fees"
                                    'AP Energy
                                Case EnumPaymentType.PaymentEnergy
                                    .STLTransactionType = EnumSTLTransactionType.APEnergy
                                    .Remarks_1 = "Energy"
                                    'AP VAT
                                Case EnumPaymentType.PaymentEnergyVAT
                                    .STLTransactionType = EnumSTLTransactionType.APVAT
                                    .Remarks_1 = "Energy"
                                    'AP Default Interest
                                Case EnumPaymentType.DefaultAllocation
                                    .STLTransactionType = EnumSTLTransactionType.APDefaultEnergy
                                    .Remarks_1 = "Energy"
                            End Select


                            .Amount += AmountToSTL
                        End With

                        If STLPayment.STLTransactionType <> 0 Then
                            retSTLPayments.Add(STLPayment)
                        End If

                    Next
                    'End Per Invoice Transaction

                Next
                'End Create per Account

            Next
            'End GroupCode Loop

            'Get PR Replenishment from Payment
            Dim _lstPaymentEFT = (From x In lstPayments.ParticipantForEFT _
                                  Where x.AllocationDate = _itmAllocationDate _
                                  And x.TransferPrudential <> 0 _
                                  Select x).ToList

            If _lstPaymentEFT.Count <> 0 Then
                Dim eftSTLDetails As New STLNoticeDetails
                With eftSTLDetails
                    '.BillPeriod = 0
                    .AllocationDate = _itmAllocationDate
                    .CategoryType = EnumSTLCategory.Payment
                    .STLTransactionType = EnumSTLTransactionType.PRReplenishment
                    .Amount = _lstPaymentEFT.Sum(Function(x As EFT) x.TransferPrudential) * -1D
                    .Participant.IDNumber = _lstPaymentEFT.FirstOrDefault.Participant.IDNumber
                    .Remarks_1 = "PR Replenishment"
                    .Remarks_2 = _lstPaymentEFT.FirstOrDefault.Participant.ParticipantID
                End With
                retSTLPayments.Add(eftSTLDetails)
            End If

            'Get Interest NSS
            Dim _lstNSSInterest = (From x In lstPayments.ParticipantForEFT _
                                  Where x.AllocationDate = _itmAllocationDate _
                                  And x.NSSInterest <> 0 _
                                  Select x).ToList

            If _lstNSSInterest.Count <> 0 Then
                Dim eftSTLDetails As New STLNoticeDetails
                With eftSTLDetails
                    '.BillPeriod = 0
                    .AllocationDate = _itmAllocationDate
                    .CategoryType = EnumSTLCategory.Payment
                    .STLTransactionType = EnumSTLTransactionType.APOthers
                    .Amount = _lstNSSInterest.Sum(Function(x As EFT) x.NSSInterest)
                    .Participant.IDNumber = _lstNSSInterest.FirstOrDefault.Participant.IDNumber
                    .Remarks_1 = "NSS Interest"
                    .Remarks_2 = _lstNSSInterest.FirstOrDefault.Participant.ParticipantID
                End With
                retSTLPayments.Add(eftSTLDetails)
            End If

            'Get Interest STL
            Dim _lstSTLInterest = (From x In lstPayments.ParticipantForEFT _
                                  Where x.AllocationDate = _itmAllocationDate _
                                  And x.STLInterest <> 0 _
                                  Select x).ToList

            If _lstSTLInterest.Count <> 0 Then
                Dim eftSTLDetails As New STLNoticeDetails
                With eftSTLDetails
                    '.BillPeriod = 0
                    .AllocationDate = _itmAllocationDate
                    .CategoryType = EnumSTLCategory.Payment
                    .STLTransactionType = EnumSTLTransactionType.APOthers
                    .Amount = _lstSTLInterest.Sum(Function(x As EFT) x.STLInterest)
                    .Participant.IDNumber = _lstSTLInterest.FirstOrDefault.Participant.IDNumber
                    .Remarks_1 = "Settlement Interest"
                    .Remarks_2 = _lstSTLInterest.FirstOrDefault.Participant.ParticipantID
                End With
                retSTLPayments.Add(eftSTLDetails)
            End If
        Next
        'End Loop Allocation Date
        Return retSTLPayments
    End Function

    'Ending Balances
    Private Function AddEndingBalances(ByVal lstEndTransactions As List(Of WESMBillSummary)) As List(Of STLNoticeDetails)
        Dim retEndingSTL As New List(Of STLNoticeDetails)

        'If Me.cbo_date.SelectedIndex <> 0 Then
        '    'Get WESM Bill Summary History
        '    'Get WESMBill Summary Number
        '    Dim _lstWESMNumber = (From x In lstEndTransactions _
        '                          Select x.WESMBillSummaryNo).ToList

        '    Dim _WESMHistory As New List(Of WESMBillSummaryHistory)
        '    _WESMHistory = Me.WbillHelper.GetWESMBillSummaryHistoryLstWESMSummaryNo(_lstWESMNumber)


        '    For Each itmWESMSummary In lstEndTransactions.OrderBy(Function(x) x.BillPeriod)
        '        Dim EndingSTL As New STLNoticeDetails
        '        Dim _itmWESMSummary = itmWESMSummary
        '        Dim EndingBalance As Decimal = 0
        '        'Get WESM History for transaction
        '        Dim _TransactionHistory As New List(Of WESMBillSummaryHistory)
        '        _TransactionHistory = (From x In _WESMHistory _
        '                               Where x.WESMBillSummaryNo = _itmWESMSummary.WESMBillSummaryNo _
        '                               Select x).ToList

        '        'Get Beginning Balance of Bill
        '        Dim _BeginningBalance As Decimal = 0
        '        _BeginningBalance = (From x In lstEndTransactions _
        '                             Where x.WESMBillSummaryNo = _itmWESMSummary.WESMBillSummaryNo _
        '                             Select x.BeginningBalance).FirstOrDefault

        '        For Each itmHistory In _TransactionHistory
        '            If itmHistory.CollectionType = EnumCollectionType.Energy Or _
        '            itmHistory.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergy Or _
        '            itmHistory.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergy Then

        '                _BeginningBalance -= itmHistory.Amount

        '            End If
        '        Next



        '        With EndingSTL
        '            .BillPeriod = itmWESMSummary.BillPeriod
        '            .Amount = itmWESMSummary.EndingBalance
        '            .Amount = _BeginningBalance
        '            .INVDMCMNo = itmWESMSummary.INVDMCMNo
        '            .SummaryType = itmWESMSummary.SummaryType
        '            .Participant = itmWESMSummary.IDNumber
        '            .Remarks_2 = itmWESMSummary.IDNumber.ParticipantID
        '            '.InvoiceDate
        '            If itmWESMSummary.EndingBalance < 0 Then
        '                Select Case itmWESMSummary.ChargeType
        '                    Case EnumChargeType.E
        '                        .STLTransactionType = EnumSTLTransactionType.AREnergy
        '                        .Remarks_1 = "Energy"
        '                    Case EnumChargeType.EV
        '                        .STLTransactionType = EnumSTLTransactionType.ARVAT
        '                        .Remarks_1 = "Energy"
        '                    Case EnumChargeType.MF, EnumChargeType.MFV
        '                        .STLTransactionType = EnumSTLTransactionType.ARMF
        '                        .Remarks_1 = "Market Fees"
        '                End Select
        '            Else
        '                Select Case itmWESMSummary.ChargeType
        '                    Case EnumChargeType.E
        '                        .STLTransactionType = EnumSTLTransactionType.APEnergy
        '                        .Remarks_1 = "Energy"
        '                    Case EnumChargeType.EV
        '                        .STLTransactionType = EnumSTLTransactionType.APVAT
        '                        .Remarks_1 = "Energy"
        '                    Case EnumChargeType.MF, EnumChargeType.MFV
        '                        .STLTransactionType = EnumSTLTransactionType.APMF
        '                        .Remarks_1 = "Market Fees"
        '                End Select
        '            End If

        '            .CategoryType = EnumSTLCategory.EndingBalances
        '        End With
        '        retEndingSTL.Add(EndingSTL)
        '    Next

        'Else
        For Each itmWESMSummary In lstEndTransactions.OrderBy(Function(x) x.BillPeriod)
            Dim EndingSTL As New STLNoticeDetails
            With EndingSTL
                .BillPeriod = itmWESMSummary.BillPeriod
                .Amount = itmWESMSummary.EndingBalance
                .INVDMCMNo = itmWESMSummary.INVDMCMNo
                .SummaryType = itmWESMSummary.SummaryType
                .Participant = itmWESMSummary.IDNumber
                .Remarks_2 = itmWESMSummary.IDNumber.ParticipantID
                '.InvoiceDate
                If itmWESMSummary.EndingBalance < 0 Then
                    Select Case itmWESMSummary.ChargeType
                        Case EnumChargeType.E
                            .STLTransactionType = EnumSTLTransactionType.AREnergy
                            .Remarks_1 = "Energy"
                        Case EnumChargeType.EV
                            .STLTransactionType = EnumSTLTransactionType.ARVAT
                            .Remarks_1 = "Energy"
                        Case EnumChargeType.MF, EnumChargeType.MFV
                            .STLTransactionType = EnumSTLTransactionType.ARMF
                            .Remarks_1 = "Market Fees"
                    End Select
                Else
                    Select Case itmWESMSummary.ChargeType
                        Case EnumChargeType.E
                            .STLTransactionType = EnumSTLTransactionType.APEnergy
                            .Remarks_1 = "Energy"
                        Case EnumChargeType.EV
                            .STLTransactionType = EnumSTLTransactionType.APVAT
                            .Remarks_1 = "Energy"
                        Case EnumChargeType.MF, EnumChargeType.MFV
                            .STLTransactionType = EnumSTLTransactionType.APMF
                            .Remarks_1 = "Market Fees"
                    End Select
                End If

                .CategoryType = EnumSTLCategory.EndingBalances
            End With
            retEndingSTL.Add(EndingSTL)
        Next
        'End If
        Return retEndingSTL
    End Function

    'Add to Data Table
    Private Function GenerateSTLDataTable(ByVal ParticipantSTL As STLNotice) As DataTable
        'Receive Value of:
        '   Participant's Header of Settlement Notice

        Dim dt As New DataTable
        Dim dicTotalAR As New Dictionary(Of Integer, Decimal)
        Dim dicTotalAP As New Dictionary(Of Integer, Decimal)

        'Input Columns of DataTable
        With dt.Columns
            .Add("Billing Period")
            .Add("Remarks_1") ' Charge Type
            .Add("Remarks_2") ' For Participant Id
            .Add("Remarks_3") ' For Particulars (PSM, MRU adj etc)
            .Add("Invoice Number")
            .Add(EnumSTLTransactionType.AREnergy.ToString)
            .Add(EnumSTLTransactionType.ARDefaultEnergy.ToString)
            .Add(EnumSTLTransactionType.ARVAT.ToString)
            .Add(EnumSTLTransactionType.ARMF.ToString)
            .Add(EnumSTLTransactionType.ARDefaultMF.ToString)
            .Add(EnumSTLTransactionType.AROthers.ToString)
            .Add("AR - TOTAL")
            .Add(EnumSTLTransactionType.APEnergy.ToString)
            .Add(EnumSTLTransactionType.APDefaultEnergy.ToString)
            .Add(EnumSTLTransactionType.APVAT.ToString)
            .Add(EnumSTLTransactionType.APMF.ToString)
            .Add(EnumSTLTransactionType.APDefaultMF.ToString)
            .Add(EnumSTLTransactionType.APOthers.ToString)
            .Add("AP - TOTAL")
            .Add("GRAND TOTAL")
        End With

        'For beginning balance
        Dim dr As DataRow
        dr = dt.NewRow

        Dim BeginningBalance As New List(Of STLNoticeDetails)
        Dim DistinctInvoice As New List(Of String)
        DistinctInvoice = (From x In ParticipantSTL.BalanceDetails _
                           Select x.INVDMCMNo Distinct).ToList

        For Each itmInvoice In DistinctInvoice
            Dim _itmInvoice = itmInvoice
            Dim ForDetails As New List(Of STLBeginningBalance)
            Dim BegBalance As New STLNoticeDetails
            ForDetails = (From x In ParticipantSTL.BalanceDetails _
                          Where x.INVDMCMNo = _itmInvoice _
                          Select x).ToList

            Dim TotalMFAmount As Decimal = 0

            'Loop Invoices
            For Each itmDetails In ForDetails
                BegBalance.BillPeriod = itmDetails.BillPeriod
                BegBalance.CategoryType = EnumSTLCategory.BeginningBalances
                BegBalance.INVDMCMNo = itmDetails.INVDMCMNo
                BegBalance.Participant = itmDetails.IDNumber
                BegBalance.SummaryType = itmDetails.SummaryType
                BegBalance.TransactionDate = itmDetails.TransactionDate

                If itmDetails.ChargeType = EnumChargeType.MF Or itmDetails.ChargeType = EnumChargeType.MFV Then
                    TotalMFAmount += itmDetails.EndingBalance
                Else
                    If itmDetails.BeginningBalance < 0 Then
                        If itmDetails.ChargeType = EnumChargeType.E Then
                            BegBalance.STLTransactionType = EnumSTLTransactionType.AREnergy
                            BegBalance.Amount = itmDetails.EndingBalance
                            BeginningBalance.Add(BegBalance)
                            BegBalance = New STLNoticeDetails
                        Else
                            BegBalance.STLTransactionType = EnumSTLTransactionType.ARVAT
                            BegBalance.Amount = itmDetails.EndingBalance
                            BeginningBalance.Add(BegBalance)
                            BegBalance = New STLNoticeDetails
                        End If
                    Else
                        If itmDetails.ChargeType = EnumChargeType.E Then
                            BegBalance.STLTransactionType = EnumSTLTransactionType.APEnergy
                            BegBalance.Amount = itmDetails.EndingBalance
                            BeginningBalance.Add(BegBalance)
                            BegBalance = New STLNoticeDetails
                        Else
                            BegBalance.STLTransactionType = EnumSTLTransactionType.APVAT
                            BegBalance.Amount = itmDetails.EndingBalance
                            BeginningBalance.Add(BegBalance)
                            BegBalance = New STLNoticeDetails
                        End If
                    End If
                End If
            Next

            If TotalMFAmount <> 0 Then
                If TotalMFAmount < 0 Then
                    BegBalance.STLTransactionType = EnumSTLTransactionType.ARMF
                Else
                    BegBalance.STLTransactionType = EnumSTLTransactionType.APMF
                End If
                BegBalance.Amount = TotalMFAmount
                BeginningBalance.Add(BegBalance)
            End If
        Next


        If BeginningBalance.Count <> 0 Then
            dr = dt.NewRow
            dr("Billing Period") = "Beginning Balances:"
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        End If

        'Add Beginning Balances
        dt.Merge(Me.AddDetailsDataRow(dt, BeginningBalance, EnumSTLCategory.BeginningBalances))

        dt.Rows.Add(dt.NewRow)
        dr = dt.NewRow


        Dim _ARSum = (From x In ParticipantSTL.BalanceDetails _
                      Where x.EndingBalance < 0 _
                      Select x.EndingBalance).Sum

        Dim _APSum = (From x In ParticipantSTL.BalanceDetails _
                      Where x.EndingBalance > 0 _
                      Select x.EndingBalance).Sum

        'dr("Billing Period") = "Total Beginning Balances:"

        ''get Charge Types
        'Dim _dChrageType = (From x In ParticipantSTL.BalanceDetails _
        '                    Select x.ChargeType Distinct).ToList

        'For Each itmCharge In _dChrageType
        '    Dim _itmCharge = itmCharge

        '    Select Case _itmCharge
        '        Case EnumChargeType.E
        '            Dim _SumAR = (From x In ParticipantSTL.BalanceDetails _
        '                          Where x.ChargeType = EnumChargeType.E _
        '                          And x.EndingBalance < 0 _
        '                          Select x.EndingBalance).Sum

        '            Dim _SumAP = (From x In ParticipantSTL.BalanceDetails _
        '                          Where x.ChargeType = EnumChargeType.E _
        '                          And x.EndingBalance > 0 _
        '                          Select x.EndingBalance).Sum

        '            dr(EnumSTLTransactionType.AREnergy.ToString) = FormatNumber(_SumAR, 2, TriState.True, TriState.True)
        '            dr(EnumSTLTransactionType.APEnergy.ToString) = FormatNumber(_SumAP, 2, TriState.True, TriState.True)

        '        Case EnumChargeType.EV
        '            Dim _SumAR = (From x In ParticipantSTL.BalanceDetails _
        '                          Where x.ChargeType = EnumChargeType.EV _
        '                          And x.EndingBalance < 0 _
        '                          Select x.EndingBalance).Sum

        '            Dim _SumAP = (From x In ParticipantSTL.BalanceDetails _
        '                          Where x.ChargeType = EnumChargeType.EV _
        '                          And x.EndingBalance > 0 _
        '                          Select x.EndingBalance).Sum

        '            dr(EnumSTLTransactionType.ARVAT.ToString) = FormatNumber(_SumAR, 2, TriState.True, TriState.True)
        '            dr(EnumSTLTransactionType.APVAT.ToString) = FormatNumber(_SumAP, 2, TriState.True, TriState.True)

        '        Case EnumChargeType.MF, EnumChargeType.MFV
        '            Dim _SumAR = (From x In ParticipantSTL.BalanceDetails _
        '                          Where (x.ChargeType = EnumChargeType.MF _
        '                                 Or x.ChargeType = EnumChargeType.MFV) _
        '                          And x.EndingBalance < 0 _
        '                          Select x.EndingBalance).Sum

        '            Dim _SumAP = (From x In ParticipantSTL.BalanceDetails _
        '                          Where (x.ChargeType = EnumChargeType.MF _
        '                                 Or x.ChargeType = EnumChargeType.MFV) _
        '                          And x.EndingBalance > 0 _
        '                          Select x.EndingBalance).Sum

        '            dr(EnumSTLTransactionType.ARMF.ToString) = FormatNumber(_SumAR, 2, TriState.True, TriState.True)
        '            dr(EnumSTLTransactionType.APMF.ToString) = FormatNumber(_SumAP, 2, TriState.True, TriState.True)
        ''    End Select
        ''Next

        'dr("AR - TOTAL") = FormatNumber(_ARSum, 2, TriState.True, TriState.True)
        'dr("AP - TOTAL") = FormatNumber(_APSum, 2, TriState.True, TriState.True)

        'dr("GRAND TOTAL") = FormatNumber(_APSum + _ARSum, 2, TriState.True, TriState.True)
        'dt.Rows.Add(dr)
        'dt.AcceptChanges()

        'For Current Transactions
        Dim CurrentTransactions = ParticipantSTL.STLNoticeDetails.Where(Function(x) x.CategoryType = EnumSTLCategory.CurrentTransaction).ToList

        If CurrentTransactions.Count <> 0 Then
            dt.Rows.Add(dt.NewRow)
            dr = dt.NewRow
            dr("Billing Period") = "Current Transactions:"
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        End If

        'Add Current Transactions
        dt.Merge(Me.AddDetailsDataRow(dt, CurrentTransactions, EnumSTLCategory.CurrentTransaction))

        dt.Rows.Add(dt.NewRow)
        dr = dt.NewRow

        'For Offsetting
        Dim OffsetTransactions = ParticipantSTL.STLNoticeDetails.Where(Function(x) x.CategoryType = EnumSTLCategory.Offsetting).ToList
        If OffsetTransactions.Count <> 0 Then
            dr("Billing Period") = "Offsetting"
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        End If

        dt.Merge(Me.AddOffsettingDataRow(dt, OffsetTransactions))
        dt.Rows.Add(dt.NewRow)
        dr = dt.NewRow
        'End Offsetting

        dr("Billing Period") = "Settlements:"
        dt.Rows.Add(dr)
        dt.AcceptChanges()

        'Get Distinct Allocation Date
        For Each itmAllocationDate In ParticipantSTL.STLNoticeDetails
            itmAllocationDate.AllocationDate = CDate(FormatDateTime(itmAllocationDate.AllocationDate, DateFormat.ShortDate))
        Next

        Dim _lstAllocationDate = (From x In ParticipantSTL.STLNoticeDetails _
                             Select x.AllocationDate Distinct Order By AllocationDate Ascending).ToList

        For Each itmAllocationDate In _lstAllocationDate
            Dim _itmAllocationDate = itmAllocationDate

            'After Create Allocation date Loop
            Dim WTaxVAT As Decimal = 0
            Dim WTaxVATDefault As Decimal = 0

            'For Collection Transactions
            Dim CollectionTransaction = ParticipantSTL.STLNoticeDetails.Where(Function(x) x.CategoryType = EnumSTLCategory.Collection And x.AllocationDate = _itmAllocationDate).ToList

            'Get Distinct Invoices
            Dim colInvoice = (From x In CollectionTransaction _
                              Select x.INVDMCMNo, x.SummaryType, x.BillPeriod Distinct Order By BillPeriod Ascending).ToList

            If colInvoice.Count <> 0 Then
                dr = dt.NewRow
                dr("Billing Period") = "Collections for " & FormatDateTime(_itmAllocationDate, DateFormat.ShortDate)
                dt.Rows.Add(dr)
                dt.AcceptChanges()
            End If

            'Add collection Allocations
            dt.Merge(Me.AddDetailsDataRow(dt, CollectionTransaction, EnumSTLCategory.Collection, _itmAllocationDate))

            'For Payment Transactions
            Dim PaymentTransaction = (From x In ParticipantSTL.STLNoticeDetails Where _
                                      x.AllocationDate = _itmAllocationDate _
                                      And x.CategoryType = EnumSTLCategory.Payment _
                                      Select x).ToList

            'Get Distinct Invoices
            Dim lstPaymentInvoice = (From x In PaymentTransaction _
                                     Select x.INVDMCMNo, x.SummaryType, x.BillPeriod Distinct Order By BillPeriod Ascending).ToList

            If lstPaymentInvoice.Count <> 0 Then
                dr = dt.NewRow
                dr("Billing Period") = "Payments for " & FormatDateTime(_itmAllocationDate, DateFormat.ShortDate)
                dt.Rows.Add(dr)
                dt.AcceptChanges()
            End If

            'add Payment Allocation
            dt.Merge(Me.AddDetailsDataRow(dt, PaymentTransaction, EnumSTLCategory.Payment, _itmAllocationDate))
        Next

        '******************************
        '       Ending Balances
        '******************************
        dt.Rows.Add(dt.NewRow)
        dr = dt.NewRow
        dr("Billing Period") = "Ending Transactions:"
        dt.Rows.Add(dr)
        dt.AcceptChanges()

        Dim EndingTransaction = ParticipantSTL.STLNoticeDetails.Where(Function(x) x.CategoryType = EnumSTLCategory.EndingBalances).ToList

        dt.Merge(Me.AddDetailsDataRow(dt, EndingTransaction, EnumSTLCategory.EndingBalances))

        Dim _tmpDt As New DataTable

        For x = 0 To dt.Columns.Count - 1
            'Get Column Names from original
            Dim _colName As String = dt.Columns(x).ColumnName.ToString

            If x < 5 Then
                _tmpDt.Columns.Add(_colName)
            Else
                If x < 12 Then
                    _tmpDt.Columns.Add(Replace(_colName, "AR", "AP", 1, 2))
                Else
                    _tmpDt.Columns.Add(Replace(_colName, "AP", "AR", 1, 2))
                End If
            End If
            _tmpDt.AcceptChanges()
        Next

        For x = 0 To dt.Rows.Count - 1
            Dim dRow As DataRow
            dRow = _tmpDt.NewRow
            For colCtr = 0 To _tmpDt.Columns.Count - 1
                dRow(colCtr) = dt.Rows(x).Item(colCtr).ToString
            Next
            _tmpDt.Rows.Add(dRow)
            _tmpDt.AcceptChanges()
        Next

        Return _tmpDt
    End Function

    'Function to Add datarow to the table
    Private Function AddDetailsDataRow(ByVal dt As DataTable, ByVal SettlementDetails As List(Of STLNoticeDetails), ByVal Category As EnumSTLCategory, _
                                       Optional ByVal AllocationDate As Date = Nothing) As DataTable

        Dim dEndInvoices = (From x In SettlementDetails _
                            Select x.INVDMCMNo, x.SummaryType, x.BillPeriod Distinct Order By BillPeriod Ascending).ToList

        If Category = EnumSTLCategory.Offsetting Then
            dEndInvoices = (From x In SettlementDetails _
                            Select x.INVDMCMNo, x.SummaryType, x.BillPeriod).ToList
        End If

        Dim dr As DataRow

        For Each itmInvoice In dEndInvoices
            Dim _itmInvoice = itmInvoice

            'Get Invoice Details
            Dim invTransaction = (From x In SettlementDetails _
                                  Where x.INVDMCMNo = _itmInvoice.INVDMCMNo _
                                  And x.SummaryType = _itmInvoice.SummaryType _
                                  And x.STLTransactionType <> EnumSTLTransactionType.WTax _
                                    And x.STLTransactionType <> EnumSTLTransactionType.WVAT _
                                    And x.STLTransactionType <> EnumSTLTransactionType.DefaultWTax _
                                    And x.STLTransactionType <> EnumSTLTransactionType.DefaultWVAT _
                                    And x.STLTransactionType <> EnumSTLTransactionType.PRReplenishment _
                                  Select x).ToList

            Dim GrandTotal As Decimal = 0
            Dim ARTotal As Decimal = 0
            Dim APTotal As Decimal = 0

            Dim MarketFeetotal As Decimal = 0

            dr = dt.NewRow

            For Each itmTransaction In invTransaction
                With itmTransaction

                    'Get billing period 
                    Dim BillPeriod = (From x In _lstBillPeriod _
                                      Where x.BillingPeriod = .BillPeriod _
                                      Select x).FirstOrDefault

                    If itmTransaction.INVDMCMNo <> "" Then
                        dr("Billing Period") = .BillPeriod & " (" & BillPeriod.StartDate & " - " & BillPeriod.EndDate & ")"
                    End If
                    dr("Remarks_1") = .Remarks_1
                    dr("Remarks_2") = .Remarks_2
                    dr("Remarks_3") = .Remarks_3

                    If .INVDMCMNo <> "" Then
                        dr("Invoice Number") = .INVDMCMNo
                    End If

                    If .STLTransactionType <> EnumSTLTransactionType.ARMF And .STLTransactionType <> EnumSTLTransactionType.APMF Then
                        Dim AddAmount As Decimal = 0
                        If dr(.STLTransactionType.ToString).ToString <> "" Then
                            AddAmount = CDec(dr(.STLTransactionType.ToString).ToString)
                        End If

                        dr(.STLTransactionType.ToString) = FormatNumber(.Amount + AddAmount, 2, TriState.True, TriState.True)
                    Else
                        MarketFeetotal += .Amount
                    End If

                    If itmTransaction.CategoryType = EnumSTLCategory.Collection Then
                        If itmTransaction.Amount < 0 Then
                            APTotal += .Amount
                        Else
                            ARTotal += .Amount
                        End If
                    Else
                        If itmTransaction.Amount < 0 Then
                            ARTotal += .Amount
                        Else
                            APTotal += .Amount
                        End If
                    End If

                    GrandTotal += .Amount

                    If .STLTransactionType = EnumSTLTransactionType.ARMF Or .STLTransactionType = EnumSTLTransactionType.APMF Then
                        dr(.STLTransactionType.ToString) = FormatNumber(MarketFeetotal, 2, TriState.True, TriState.True)
                    End If

                End With
            Next
            If invTransaction.Count <> 0 Then

                If ARTotal <> 0 Then
                    dr("AR - TOTAL") = FormatNumber(ARTotal, 2, TriState.True, TriState.True)
                End If

                If APTotal <> 0 Then
                    dr("AP - TOTAL") = FormatNumber(APTotal, 2, TriState.True, TriState.True)
                End If

                If GrandTotal <> 0 Then
                    dr("GRAND TOTAL") = FormatNumber(GrandTotal, 2, TriState.True, TriState.True)
                End If
                dt.Rows.Add(dr)
                dt.AcceptChanges()
            End If
        Next

        If SettlementDetails.Count <> 0 Then
            dt.Merge(Me.AddWithHoldingTaxVAT(dt, SettlementDetails))

            Dim TotalWTax As Decimal = (From x In SettlementDetails _
                                        Where x.STLTransactionType = EnumSTLTransactionType.WTax Or _
                                        x.STLTransactionType = EnumSTLTransactionType.WVAT _
                                        Select x.Amount).Sum

            Dim TotalDefaultWTax As Decimal = (From x In SettlementDetails _
                                               Where x.STLTransactionType = EnumSTLTransactionType.DefaultWTax Or _
                                               x.STLTransactionType = EnumSTLTransactionType.DefaultWVAT _
                                               Select x.Amount).Sum


            Dim _chkReplenishment = (From x In SettlementDetails _
                                         Where x.STLTransactionType = EnumSTLTransactionType.PRReplenishment _
                                         And x.CategoryType = Category _
                                         Select x).ToList
            If _chkReplenishment.Count <> 0 Then
                'Check for replenishment
                dr = dt.NewRow
                dr(EnumSTLTransactionType.APOthers.ToString) = FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.PRReplenishment).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
                dr("Remarks_1") = _chkReplenishment.FirstOrDefault.Remarks_1
                dr("AP - TOTAL") = FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.PRReplenishment).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
                dr("GRAND TOTAL") = FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.PRReplenishment).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
                dt.Rows.Add(dr)
                dt.AcceptChanges()
            End If

            dr = dt.NewRow
            If Category = EnumSTLCategory.Collection Then
                dr("Billing Period") = "TOTAL COLLECTION ON " & FormatDateTime(AllocationDate, DateFormat.ShortDate)
            ElseIf Category = EnumSTLCategory.Payment Then
                dr("Billing Period") = "TOTAL REMITTANCE ON " & FormatDateTime(AllocationDate, DateFormat.ShortDate)
            Else
                dr("Billing Period") = "Total"
            End If

            'get total per row

            'For AR
            dr(EnumSTLTransactionType.AREnergy.ToString) = FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.AREnergy).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
            dr(EnumSTLTransactionType.ARDefaultEnergy.ToString) = FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.ARDefaultEnergy).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
            dr(EnumSTLTransactionType.ARVAT.ToString) = FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.ARVAT).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
            dr(EnumSTLTransactionType.ARMF.ToString) = FormatNumber(TotalWTax + SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.ARMF).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
            dr(EnumSTLTransactionType.ARDefaultMF.ToString) = FormatNumber(TotalDefaultWTax + SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.ARDefaultMF).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
            dr(EnumSTLTransactionType.AROthers.ToString) = FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.AROthers).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
            'MsgBox(FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType.ToString.Contains("AR") = True).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True))
            dr("AR - TOTAL") = FormatNumber(TotalWTax + TotalDefaultWTax + SettlementDetails.Where(Function(x) x.STLTransactionType.ToString.Contains("AR") = True).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)

            'For AP
            dr(EnumSTLTransactionType.APEnergy.ToString) = FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.APEnergy).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
            dr(EnumSTLTransactionType.APDefaultEnergy.ToString) = FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.APDefaultEnergy).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
            dr(EnumSTLTransactionType.APVAT.ToString) = FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.APVAT).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
            dr(EnumSTLTransactionType.APMF.ToString) = FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.APMF).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
            dr(EnumSTLTransactionType.APOthers.ToString) = FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType = EnumSTLTransactionType.APOthers).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)
            'MsgBox(FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType.ToString.Contains("AP") = True).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True))
            dr("AP - TOTAL") = FormatNumber(SettlementDetails.Where(Function(x) x.STLTransactionType.ToString.Contains("AP") = True).Sum(Function(x As STLNoticeDetails) x.Amount) + SettlementDetails.Where(Function(x) x.STLTransactionType.ToString.Contains("Replenishment") = True).Sum(Function(x As STLNoticeDetails) x.Amount), 2, TriState.True, TriState.True)

            dr("GRAND TOTAL") = FormatNumber(CDec(dr("AR - TOTAL")) + CDec(dr("AP - TOTAL")), 2, TriState.True, TriState.True)

            dt.Rows.Add(dr)
            dt.AcceptChanges()
        End If
        Return dt
    End Function

    'Function to Add datarow to the table
    Private Function AddOffsettingDataRow(ByVal dt As DataTable, ByVal SettlementDetails As List(Of STLNoticeDetails)) As DataTable

        'Get Distinct DMCM Number on Offsetting (Remarks 3)
        Dim DMCMNumber = (From x In SettlementDetails _
                         Select CLng(x.Remarks_3) Distinct).ToList

        For Each itmDMCMNumber In DMCMNumber
            'Get Settlement Details for the DMCM
            'STL Details
            Dim _itmDMCMNumber = itmDMCMNumber
            Dim _stlDetails = (From x In SettlementDetails _
                               Where x.Remarks_3 = CStr(_itmDMCMNumber) _
                               Select x).ToList

            For Each itmDetails In _stlDetails
                Dim dr As DataRow
                dr = dt.NewRow

                With itmDetails

                    'Get billing period 
                    Dim BillPeriod = (From x In _lstBillPeriod _
                                      Where x.BillingPeriod = .BillPeriod _
                                      Select x).FirstOrDefault

                    If .BillPeriod <> 0 Then
                        dr("Billing Period") = .BillPeriod & " (" & BillPeriod.StartDate & " - " & BillPeriod.EndDate & ")"
                        dr("Invoice Number") = .INVDMCMNo
                    End If

                    dr("Remarks_1") = .Remarks_1
                    dr("Remarks_2") = .Remarks_2
                    dr("Remarks_3") = .Remarks_3

                    dr(.STLTransactionType.ToString) = FormatNumber(.Amount, 2, TriState.True, TriState.True)

                    If .STLTransactionType = EnumSTLTransactionType.AREnergy Or .STLTransactionType = EnumSTLTransactionType.ARVAT Or .STLTransactionType = EnumSTLTransactionType.ARMF Then
                        If .Amount <> 0 Then
                            dr("AR - TOTAL") = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                            dr("AP - TOTAL") = FormatNumber(0, 2, TriState.True, TriState.True)
                        End If
                    Else
                        If .Amount <> 0 Then
                            dr("AP - TOTAL") = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                            dr("AR - TOTAL") = FormatNumber(0, 2, TriState.True, TriState.True)
                        End If
                    End If

                    If .Amount <> 0 Then
                        dr("GRAND TOTAL") = FormatNumber(.Amount, 2, TriState.True, TriState.True)
                    End If
                End With
                dt.Rows.Add(dr)
            Next
        Next

        Return dt
    End Function

    'Function to Add WHTax/WHVat to the table
    Private Function AddWithHoldingTaxVAT(ByVal dt As DataTable, ByVal SettlementDetails As List(Of STLNoticeDetails)) As DataTable
        Dim WTaxVAT As Decimal = 0
        Dim WTaxVATDefault As Decimal = 0

        WTaxVAT = (From x In SettlementDetails _
                           Where x.STLTransactionType = EnumSTLTransactionType.WTax _
                           Or x.STLTransactionType = EnumSTLTransactionType.WVAT _
                           Select x.Amount).Sum
        WTaxVATDefault = (From x In SettlementDetails _
                       Where x.STLTransactionType = EnumSTLTransactionType.DefaultWVAT _
                       Or x.STLTransactionType = EnumSTLTransactionType.DefaultWTax _
                       Select x.Amount).Sum

        If WTaxVAT <> 0 And WTaxVATDefault <> 0 Then
            Dim dr As DataRow
            dr = dt.NewRow
            dr("Billing Period") = SettlementDetails.FirstOrDefault.BillPeriod
            dr("Remarks_1") = "MF - WTax/WVAT"
            dr(EnumSTLTransactionType.ARMF.ToString) = FormatNumber(WTaxVAT, 2, TriState.True, TriState.True)
            dr(EnumSTLTransactionType.ARDefaultMF.ToString) = FormatNumber(WTaxVATDefault, 2, TriState.True, TriState.True)
            dr("AR - TOTAL") = FormatNumber(WTaxVAT + WTaxVATDefault, 2, TriState.True, TriState.True)
            dr("GRAND TOTAL") = FormatNumber(WTaxVAT + WTaxVATDefault, 2, TriState.True, TriState.True)
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        End If

        Return dt
    End Function

    Private Sub cmd_gen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_gen.Click
        If Me.dgView.RowCount = 0 Then
            MsgBox("No Records to export.", MsgBoxStyle.Exclamation, "No Records found")
            Exit Sub
        End If

        Dim sFolderDialog As New FolderBrowserDialog
        Dim fPath As String = ""
        With sFolderDialog
            If sFolderDialog.ShowDialog(Me) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            .ShowDialog()
            If .SelectedPath.ToString.Trim.Length = 0 Then
                Exit Sub
            Else
                fPath = sFolderDialog.SelectedPath
            End If
            fPath &= "\SettlementNoticeFor" & Me.lstbx_Participants.SelectedItem.ToString & "-" & Replace(FormatDateTime(CDate(Me.cbo_date.SelectedItem.ToString), DateFormat.ShortDate), "/", "") & ".csv"
        End With

        Dim dt As New DataTable

        If Me.chk_Note.Checked Then
            dt = CType(dgView.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr("Billing Period") = "NOTE: "
            dr("Remarks_1") = Me.txt_Note.Text
            dt.Rows.Add(dr)
            dt.AcceptChanges()
            Me.dgView.DataSource = dt
        Else
            dt = CType(dgView.DataSource, DataTable)
        End If

        Me.WbillHelper.BFactory.RemoveCommaForCSVExport(dt)
        Me.WbillHelper.DataTable2CSV(dt, fPath)

        MsgBox("Successfully exported file to " & fPath, MsgBoxStyle.Exclamation, "Success")
    End Sub

    Private Sub chk_Note_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_Note.CheckedChanged
        If Me.chk_Note.Checked Then
            Me.txt_Note.ReadOnly = False
        Else
            Me.txt_Note.ReadOnly = True
        End If
    End Sub

    Private Sub cbo_date_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_date.SelectedIndexChanged

    End Sub
End Class