'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmAgingReport
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     January 14, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for generation of Aging Reports
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'
'

Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmAgingReport
    Private WBillHelper As WESMBillHelper
    Private lstParticipants As List(Of AMParticipants)
    Private BFactory As BusinessFactory
    Private Sub chbox_AllParticipants_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbox_AllParticipants.CheckedChanged
        If Me.chbox_AllParticipants.Checked Then
            'Me.cbo_lstParticipants.Enabled = False
            Me.lstChkBox.Enabled = False
        Else
            'Me.cbo_lstParticipants.Enabled = True
            Me.lstChkBox.Enabled = True
        End If
    End Sub

    Private Sub cmd_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Close.Click
        Me.Close()
    End Sub

    Private Sub frmAgingReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
            WBillHelper = WESMBillHelper.GetInstance
            WBillHelper.UserName = AMModule.UserName
            BFactory = BusinessFactory.GetInstance()
            lstParticipants = Me.WBillHelper.GetAMParticipants()
            Me.FillParticipants()
            Me.FillTransactionDate()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error!")
            Exit Sub
        End Try
    End Sub

    Private Sub FillParticipants()
        'Me.cbo_lstParticipants.DataSource = (From x In Me.lstParticipants _
        'Select x.ParticipantID Distinct).ToList

        Me.lstChkBox.DataSource = (From x In Me.lstParticipants _
                                             Select x.ParticipantID Distinct).ToList
    End Sub

    Private Sub FillTransactionDate()
        Me.cbo_TransactionDate.DataSource = WBillHelper.GetSTLDates()
    End Sub

    Private Sub cmd_Generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Generate.Click
        Dim _selParticipant As New List(Of AMParticipants)

        Try
            If Me.cbo_TransactionDate.SelectedIndex = -1 Then
                MsgBox("No available data to generate for Aging Report", MsgBoxStyle.Critical, "No Data")
                Exit Sub
            End If

            'Get Participant
            If chbox_AllParticipants.Checked Then
                _selParticipant = lstParticipants
            Else
                If Me.lstChkBox.CheckedItems.Count = 0 Then
                    MsgBox("No participant/s selected, please check selection and try again.", MsgBoxStyle.Exclamation, "No Selected Participant")
                    Exit Sub
                End If

                For Each itmRec In Me.lstChkBox.CheckedItems
                    Dim _Participant = itmRec.ToString
                    _selParticipant.Add((From x In lstParticipants _
                                         Where x.ParticipantID = _Participant _
                                         Select x).FirstOrDefault)
                Next
            End If

            Dim TransactionDate As Date = CDate(FormatDateTime(CDate(Me.cbo_TransactionDate.SelectedItem.ToString), DateFormat.ShortDate))

            '1st day of Transaction
            Dim TransactionEnd As Date = CDate(FormatDateTime(TransactionDate, DateFormat.ShortDate))

            'As of Last Day of Month of selected date
            'Get Last day of month
            Dim TransactionBegin As New Date(TransactionDate.Year, TransactionDate.Month, 1) ' Now.Day)

            'Get All outstanding Balances
            Dim AllWESMBills As New List(Of WESMBillSummary)
            AllWESMBills = Me.WBillHelper.GetWESMBillSummary()

            Dim _OutstandingBal = New List(Of WESMBillSummary) 'Me.WBillHelper.GetWESMBillSummaryPerParticipant(_selParticipant)

            For Each itmParticipant In _selParticipant
                Dim _itmParticipant = itmParticipant
                _OutstandingBal.AddRange((From x In AllWESMBills _
                                   Where x.IDNumber.IDNumber = _itmParticipant.IDNumber _
                                   Select x).ToList)
                'Removed (And x.EndingBalance <> 0 _) in LinQ to accommodate all invoices
            Next

            'Changed x.EndingBalance -> x.BeginningBalance
            If rb_APBills.Checked Then
                _OutstandingBal = (From x In _OutstandingBal _
                                   Where x.BeginningBalance > 0 _
                                   Select x Order By x.INVDMCMNo Ascending).ToList 'And x.TransactionDate <= TransactionEnd _ , And x.TransactionDate >= TransactionBegin _
            Else
                _OutstandingBal = (From x In _OutstandingBal _
                                   Where x.BeginningBalance < 0 _
                                   Select x Order By x.INVDMCMNo Ascending).ToList 'And x.TransactionDate <= TransactionEnd _ , And x.TransactionDate >= TransactionBegin _
            End If

            If _OutstandingBal.Count = 0 Then
                MsgBox("No Records found.", MsgBoxStyle.Exclamation, "Aging Report")
                Exit Sub
            End If

            'Get Distinct WESMBill Summary Number
            Dim _DistinctBillNumber = (From x In _OutstandingBal _
                                       Select x.WESMBillSummaryNo Distinct).ToList

            Dim _Collections As New List(Of Collection)
            Dim _Payments As New List(Of Payment)



            Dim AllWESMHistoryTransaction As New List(Of WESMBillSummaryHistory)
            AllWESMHistoryTransaction = Me.WBillHelper.GetWESMBillSummaryHistory(TransactionEnd)

            'Get Collections and Payments
            If _DistinctBillNumber.Count <> 0 Then
                Dim distinctCollections = (From x In AllWESMHistoryTransaction _
                                           Where x.CollectionNumber <> 0 _
                                           Select x.CollectionNumber Distinct).ToList

                Dim distinctPayments = (From x In AllWESMHistoryTransaction _
                                        Where x.CollectionNumber = 0 _
                                        Select x.PaymentBatchCode Distinct).ToList

                If distinctCollections.Count <> 0 Then
                    _Collections = Me.WBillHelper.GetCollections(distinctCollections)
                End If


                If distinctPayments.Count <> 0 Then
                    _Payments = Me.WBillHelper.GetPayment(distinctPayments)
                End If

            End If

            ProgressThread.Show("Please wait while preparing Aging Report.")
            Dim rptView As New frmReportViewer            
            With rptView
                .LoadAgingReport(Me.GenerateTableReport(_OutstandingBal, _Collections, _Payments, AllWESMHistoryTransaction), AMModule.UserName, DateTime.Now)
                .ShowDialog()
            End With

        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error!")
            'Updated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepAgingReportWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInPrinting.ToString, AMModule.UserName)
            Exit Sub
        Finally
            ProgressThread.Close()
        End Try
    End Sub

    Private Function GenerateTableReport(ByVal lstWESMbills As List(Of WESMBillSummary), ByVal lstCollection As List(Of Collection), ByVal lstPayment As List(Of Payment), ByVal lstSummaryHistory As List(Of WESMBillSummaryHistory)) As DSReport.HistoricalAgedTrialDataTable
        Dim dt As New DSReport.HistoricalAgedTrialDataTable
        Dim AllParticipants As New List(Of AMParticipants)
        AllParticipants = Me.WBillHelper.GetAMParticipants()
        Dim Signatory = Me.WBillHelper.GetSignatories("AGE").FirstOrDefault

        Dim dFrom As New Date
        Dim dTo As New Date

        Dim _GetRFP = Me.WBillHelper.GetRequestForPayment()
        Dim _tstTotal As Decimal = 0
        For Each itmWESMBill In lstWESMbills
            Dim _itmWESMBill = itmWESMBill

            Dim _Collections = (From x In lstSummaryHistory _
                    Where x.CollectionNumber <> 0 _
                    And x.WESMBillSummaryNo = _itmWESMBill.WESMBillSummaryNo _
                    Select x).ToList

            Dim _Payment = (From x In lstSummaryHistory _
                            Where x.CollectionNumber = 0 _
                            And x.WESMBillSummaryNo = _itmWESMBill.WESMBillSummaryNo _
                            Select x.PaymentBatchCode Distinct).ToList

            'Get Participant Information 
            Dim _Participant = (From x In AllParticipants _
                                Where x.IDNumber = _itmWESMBill.IDNumber.IDNumber _
                                Select x).FirstOrDefault

            'Place WESM Bill Summary as Header
            Dim WBillRow As DataRow
            WBillRow = dt.NewRow
            With itmWESMBill
                WBillRow(dt.AMOUNTColumn) = .BeginningBalance
                WBillRow(dt.DATEColumn) = FormatDateTime(.DueDate, DateFormat.ShortDate)
                WBillRow(dt.DOC_NOColumn) = .INVDMCMNo

                WBillRow(dt.GEN_LOADColumn) = _Participant.GenLoad.ToString
                WBillRow(dt.CONTACT_PERSONColumn) = _Participant.Representative.GetFullName
                WBillRow(dt.CONTACT_NOColumn) = _Participant.Representative.Contact

                WBillRow(dt.ID_NUMBERColumn) = .IDNumber.IDNumber
                WBillRow(dt.PARTICIPANT_FULLNAMEColumn) = _Participant.FullName
                WBillRow(dt.PARTICIPANT_IDColumn) = .IDNumber.ParticipantID

                WBillRow(dt.TYPEColumn) = itmWESMBill.ChargeType.ToString

                Dim dDiff = Math.Abs(DateAndTime.DateDiff(DateInterval.Day, CDate(FormatDateTime(SystemDate, DateFormat.ShortDate)), _
                                        .DueDate))

                If .EndingBalance <> 0 Then
                    If dDiff <= 30 Then
                        WBillRow(dt.CURRENTColumn) = .EndingBalance
                    ElseIf dDiff <= 60 Then
                        WBillRow(dt._3160DAYSColumn) = .EndingBalance
                    ElseIf dDiff <= 90 Then
                        WBillRow(dt._6190DAYSColumn) = .EndingBalance
                    Else
                        WBillRow(dt._91OVERDAYSColumn) = .EndingBalance
                    End If
                End If

                WBillRow(dt.PREPARED_BYColumn) = Me.WBillHelper.UserName
                WBillRow(dt.PREPARED_POSITIONColumn) = AMModule.Position
                WBillRow(dt.CHECKED_BYColumn) = Signatory.Signatory_1
                WBillRow(dt.CHECKED_POSITIONColumn) = Signatory.Position_1
                WBillRow(dt.APPROVED_BYColumn) = Signatory.Signatory_2
                WBillRow(dt.APPROVED_POSITIONColumn) = Signatory.Position_2
            End With
            dt.Rows.Add(WBillRow)
            dt.AcceptChanges()

            'Place Collection Row
            For Each itmCollection In _Collections
                Dim colRow As DataRow
                Dim _itmCollection = itmCollection
                colRow = dt.NewRow

                With itmCollection

                    If .CollectionType = EnumCollectionType.DefaultInterestOnEnergy Or .CollectionType = EnumCollectionType.DefaultInterestOnMF Or _
                        .CollectionType = EnumCollectionType.DefaultInterestOnVatOnEnergy Or .CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF Then
                        Continue For
                    End If

                    Dim itmDetails = (From x In lstCollection _
                                      Where x.CollectionNumber = _itmCollection.CollectionNumber _
                                      Select x).FirstOrDefault

                    colRow(dt.DATEColumn) = FormatDateTime(itmDetails.CollectionDate, DateFormat.ShortDate)
                    Dim _chkCollectionType = itmDetails.CollectionCategory

                    If _chkCollectionType = EnumCollectionCategory.Cash Then
                        colRow(dt.DOC_NOColumn) = "       " & Me.BFactory.GenerateBIRDocumentNumber(itmDetails.ORNo, BIRDocumentsType.OfficialReceipt)
                    Else
                        colRow(dt.DOC_NOColumn) = "       " & Me.BFactory.GenerateBIRDocumentNumber(itmDetails.DMCMNumber, BIRDocumentsType.DMCM)
                    End If

                    colRow(dt.GEN_LOADColumn) = itmWESMBill.IDNumber.GenLoad.ToString
                    colRow(dt.ID_NUMBERColumn) = itmWESMBill.IDNumber.IDNumber
                    colRow(dt.PARTICIPANT_FULLNAMEColumn) = itmWESMBill.IDNumber.FullName
                    colRow(dt.PARTICIPANT_IDColumn) = itmWESMBill.IDNumber.ParticipantID

                    Dim dDiff = Math.Abs(DateAndTime.DateDiff(DateInterval.Day, CDate(FormatDateTime(SystemDate, DateFormat.ShortDate)), _
                                        _itmWESMBill.DueDate))

                    colRow(dt.AMOUNTColumn) = Math.Abs(.Amount)

                    'If dDiff <= 30 Then
                    '    If .CollectionType = EnumCollectionType.WithholdingTaxOnMF Or .CollectionType = EnumCollectionType.WithholdingVatonMF Then
                    '        colRow(dt.CURRENTColumn) = .Amount
                    '    Else
                    '        colRow(dt.CURRENTColumn) = .Amount * -1
                    '    End If
                    'ElseIf dDiff <= 60 Then
                    '    If .CollectionType = EnumCollectionType.WithholdingTaxOnMF Or .CollectionType = EnumCollectionType.WithholdingVatonMF Then
                    '        colRow(dt._3160DAYSColumn) = .Amount
                    '    Else
                    '        colRow(dt._3160DAYSColumn) = .Amount * -1
                    '    End If
                    'ElseIf dDiff <= 90 Then
                    '    If .CollectionType = EnumCollectionType.WithholdingTaxOnMF Or .CollectionType = EnumCollectionType.WithholdingVatonMF Then
                    '        colRow(dt._6190DAYSColumn) = .Amount
                    '    Else
                    '        colRow(dt._6190DAYSColumn) = .Amount * -1
                    '    End If
                    'Else
                    '    If .CollectionType = EnumCollectionType.WithholdingTaxOnMF Or .CollectionType = EnumCollectionType.WithholdingVatonMF Then
                    '        colRow(dt._91OVERDAYSColumn) = .Amount
                    '    Else
                    '        colRow(dt._91OVERDAYSColumn) = .Amount * -1
                    '    End If
                    'End If
                End With

                dt.Rows.Add(colRow)
                dt.AcceptChanges()
            Next

            'Payment Transactions
            For Each itmPaymentBatch In _Payment
                Dim _itmPaymentBatch = itmPaymentBatch
                Dim _tst As Decimal = 0
                'Loop payment types
                Dim PayTypes = (From x In lstSummaryHistory _
                                Where x.WESMBillSummaryNo = _itmWESMBill.WESMBillSummaryNo _
                                And x.PaymentBatchCode = _itmPaymentBatch _
                                Select x.PaymentType Distinct).ToList

                For Each itmPayType In PayTypes
                    If itmPayType = EnumPaymentType.OffsetOfPreviousReceivableEnergyDefault _
                        Or itmPayType = EnumPaymentType.OffsetToCurrentReceivableEnergyDefault _
                        Or itmPayType = EnumPaymentType.UnpaidMFDefault _
                        Or itmPayType = EnumPaymentType.UnpaidMFVDefault _
                        Or itmPayType = EnumPaymentType.WHTaxDefault _
                        Or itmPayType = EnumPaymentType.WHVATDefault _
                        Or itmPayType = EnumPaymentType.UnpaidMFWHTax _
                        Or itmPayType = EnumPaymentType.UnpaidMFWHVAT _
                        Then

                        Continue For
                    End If

                    Dim PayRow As DataRow
                    PayRow = dt.NewRow
                    Dim _itmPayType = itmPayType
                    Dim _SumAmount = (From x In lstSummaryHistory _
                                                      Where x.WESMBillSummaryNo = _itmWESMBill.WESMBillSummaryNo _
                                                      And x.PaymentBatchCode = _itmPaymentBatch _
                                                      And x.PaymentType = _itmPayType _
                                                      Select x.Amount).Sum

                    Dim _AllocationDate = (From x In lstPayment _
                                           Where x.PaymentBatchCode = _itmPaymentBatch _
                                           Select x.CollectionAllocationDate).FirstOrDefault

                    PayRow(dt.DATEColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)

                    Dim RFPNumber = (From x In _GetRFP _
                                     Where x.AllocationDate = _AllocationDate _
                                     Select x.ReferenceNo).FirstOrDefault
                    PayRow(dt.DOC_NOColumn) = "        RFP-" & RFPNumber

                    PayRow(dt.GEN_LOADColumn) = itmWESMBill.IDNumber.GenLoad.ToString
                    PayRow(dt.ID_NUMBERColumn) = itmWESMBill.IDNumber.IDNumber
                    PayRow(dt.PARTICIPANT_FULLNAMEColumn) = itmWESMBill.IDNumber.FullName
                    PayRow(dt.PARTICIPANT_IDColumn) = itmWESMBill.IDNumber.ParticipantID

                    Dim dDiff = Math.Abs(DateAndTime.DateDiff(DateInterval.Day, CDate(FormatDateTime(SystemDate, DateFormat.ShortDate)), _
                                        _itmWESMBill.DueDate))

                    _SumAmount *= -1

                    PayRow(dt.AMOUNTColumn) = _SumAmount

                    _tst += _SumAmount
                    _tstTotal += _SumAmount
                    dt.Rows.Add(PayRow)
                    dt.AcceptChanges()
                Next                
            Next

        Next        
        Return dt
    End Function
End Class