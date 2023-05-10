'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmCashSummaryReport
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     March 6, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for the Generation of Cash Sumamry Report Per Bank Account
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'
'




Option Strict On
Option Explicit On

Imports AccountsManagementObjects
Imports AccountsManagementForms
Imports AccountsManagementLogic
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmCashSummaryReport
    Private WBillHelper As WESMBillHelper
    Private dicAccountCode As New Dictionary(Of Integer, String)

    Private Sub frmCashSummaryReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        WBillHelper = WESMBillHelper.GetInstance
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        'For Text box 
        dicAccountCode.Add(0, CashInbankSettlementcode)
        dicAccountCode.Add(1, CashinBankPrudentialCode)
        dicAccountCode.Add(2, CashinBankAccountSurplusCode)

        'Cash Accounts to Sumamrize
        Me.cbo_AccountToGenerate.Items.Add("Cash in Bank (Settlement Account)")
        Me.cbo_AccountToGenerate.Items.Add("Cash in Bank (PR Account)")
        Me.cbo_AccountToGenerate.Items.Add("Cash in Bank (NSS Account)")

        Me.cbo_AccountToGenerate.SelectedIndex = 0

        'Column to Order
        Me.cbo_ColumnSort.Items.Add("Transaction Date")
        Me.cbo_ColumnSort.Items.Add("Journal Voucher Number")
        Me.cbo_ColumnSort.Items.Add("Participant ID Number")
        Me.cbo_ColumnSort.Items.Add("Participant Fullname")

        Me.cbo_ColumnSort.SelectedIndex = 0

    End Sub

    Private Sub cbo_AccountToGenerate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_AccountToGenerate.SelectedIndexChanged
        If Me.cbo_AccountToGenerate.SelectedIndex <> -1 Then
            Me.txt_AccountCode.Text = dicAccountCode(Me.cbo_AccountToGenerate.SelectedIndex)
        End If
    End Sub

    Private Sub cmd_Generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Generate.Click
        Dim dt As New DSReport.CashSummaryReportDataTable
        Try
            Dim _lstCashSummary As New List(Of CashSummary)
            Dim _Participants = Me.WBillHelper.GetAMParticipants()
            Dim _GetJournalVoucher = Me.WBillHelper.GetJournalVoucher()

            Dim _GetBeginningBalance As New List(Of BankReconMain)
            _GetBeginningBalance = WBillHelper.GetBankRecon(dtp_From.Value.Month, dtp_From.Value.Year)
            Dim BeginBalance As Decimal = 0
            If Me.cbo_AccountToGenerate.SelectedIndex = 0 Then
                BeginBalance = (From x In _GetBeginningBalance _
                                Where x.BankReconType = EnumBankReconType.Settlement _
                                And x.BeginningBalance <> 0 _
                                Select x.BeginningBalance).FirstOrDefault

                'Get All Payments for with EFT/Check Payments
                Dim _PaymentEFT As New List(Of EFT)
                _PaymentEFT = WBillHelper.GetAMPaymentForEFT(dtp_From.Value, dtp_to.Value)
                'For EFT
                _PaymentEFT = (From x In _PaymentEFT _
                               Where x.PaymentType = EnumParticipantPaymentType.EFT _
                               Select x).ToList

                Dim _PaymentChecks As New List(Of Check)
                _PaymentChecks = WBillHelper.GetCheckCleared(dtp_From.Value, dtp_to.Value)

                'Get All Cleared checks
                _PaymentChecks = (From x In _PaymentChecks _
                                  Where x.Status = EnumCheckStatus.Cleared _
                                  Select x).ToList

                'Get Collections
                Dim _GetCollections As New List(Of Collection)
                _GetCollections = WBillHelper.GetCollections(dtp_From.Value, dtp_to.Value, True)

                For Each _itmCollections In _GetCollections
                    Dim _itmCashsummary As New CashSummary
                    With _itmCollections

                        If .CollectionCategory = EnumCollectionCategory.Drawdown Then
                            Continue For
                        End If

                        _itmCashsummary.Participant.IDNumber = .IDNumber
                        _itmCashsummary.Participant.FullName = (From x In _Participants _
                                                    Where x.IDNumber = .IDNumber _
                                                    Select x.FullName).FirstOrDefault
                        _itmCashsummary.TransactionDate = .CollectionDate
                        _itmCashsummary.Debit = .CollectedAmount
                        If .CollectionCategory = EnumCollectionCategory.Cash Then
                            _itmCashsummary.DocumentNo = "OR-" & .ORNo
                        Else
                            _itmCashsummary.DocumentNo = "DMCM-" & .DMCMNumber
                        End If

                        _itmCashsummary.JournalVoucherNo = (From x In _GetJournalVoucher _
                                                            Where x.BatchCode = (From z In _GetCollections _
                                                                   Where z.CollectionNumber = .CollectionNumber _
                                                                   Select z.BatchCode).FirstOrDefault.ToString _
                                                            Select x.JVNumber).FirstOrDefault.ToString
                    End With
                    _lstCashSummary.Add(_itmCashsummary)
                Next

                Dim _lstPayment As New List(Of Payment)
                _lstPayment = Me.WBillHelper.GetPayment(dtp_From.Value, dtp_to.Value)
                For Each _itmEFT In _PaymentEFT
                    Dim _itmCashsummary As New CashSummary
                    With _itmEFT
                        _itmCashsummary.Participant.IDNumber = .Participant.IDNumber
                        _itmCashsummary.Participant.FullName = (From x In _Participants _
                                                    Where x.IDNumber = .Participant.IDNumber _
                                                    Select x.FullName).FirstOrDefault
                        _itmCashsummary.TransactionDate = .AllocationDate
                        _itmCashsummary.Credit = .TotalPayment
                        'dr(dt.MASTER_NUMBERColumn) = "OR-" & 

                        Dim _pBatchCode = (From x In _lstPayment _
                                           Where x.CollectionAllocationDate = .AllocationDate _
                                           Select x.PaymentBatchCode).FirstOrDefault

                        _itmCashsummary.JournalVoucherNo = (From x In _GetJournalVoucher _
                                                            Where x.BatchCode = _pBatchCode _
                                                            And x.PostedType = "PA" _
                                                            Select x.JVNumber).FirstOrDefault.ToString
                    End With
                    _lstCashSummary.Add(_itmCashsummary)
                Next

                Dim _lstChecks As List(Of Check)
                _lstChecks = Me.WBillHelper.GetCheck(dtp_From.Value, dtp_to.Value)

                For Each _itmChecks In _PaymentChecks
                    Dim _itmCashsummary As New CashSummary
                    With _itmChecks
                        _itmCashsummary.Participant.IDNumber = .Participant.IDNumber
                        _itmCashsummary.Participant.FullName = (From x In _Participants _
                                                    Where x.IDNumber = .Participant.IDNumber _
                                                    Select x.FullName).FirstOrDefault
                        _itmCashsummary.TransactionDate = .TransactionDate
                        _itmCashsummary.Credit = .Amount
                        _itmCashsummary.DocumentNo = "BPI-" & (From x In _lstChecks _
                                                               Where x.TransactionDate = .TransactionDate _
                                                               And x.Participant.IDNumber = .Participant.IDNumber _
                                                               Select x.CheckNumber).FirstOrDefault

                        Dim _pBatchCode = (From z In _lstPayment _
                                           Where z.CollectionAllocationDate = .TransactionDate _
                                           Select z.PaymentBatchCode).FirstOrDefault.ToString

                        _itmCashsummary.JournalVoucherNo = (From x In _GetJournalVoucher _
                                          Where x.BatchCode = _pBatchCode _
                                          And x.PostedType = "PA" _
                                          Select x.JVNumber).FirstOrDefault.ToString
                    End With
                    _lstCashSummary.Add(_itmCashsummary)
                Next
            ElseIf Me.cbo_AccountToGenerate.SelectedIndex = 1 Then

                BeginBalance = (From x In _GetBeginningBalance _
                                Where x.BankReconType = EnumBankReconType.Prudential _
                                Select x.BeginningBalance).FirstOrDefault

                'Get Fund Transfer Form
                Dim _FTFDrawdown As New List(Of FundTransferFormMain)
                _FTFDrawdown = Me.WBillHelper.GetFundTransferForm(CDate(FormatDateTime(dtp_From.Value, DateFormat.ShortDate)), CDate(FormatDateTime(dtp_to.Value, DateFormat.ShortDate)), EnumFTFTransType.DrawDown)
                Dim _FTFReplenishment As New List(Of FundTransferFormMain)
                _FTFReplenishment = Me.WBillHelper.GetFundTransferForm(CDate(FormatDateTime(dtp_From.Value, DateFormat.ShortDate)), CDate(FormatDateTime(dtp_to.Value, DateFormat.ShortDate)), EnumFTFTransType.Replenishment)

                For Each itmDrawdown In _FTFDrawdown
                    For Each itmPerParticipant In itmDrawdown.ListOfFTFParticipants
                        Dim _itmPerParticipant = itmPerParticipant
                        Dim itmCashSummary As New CashSummary
                        With itmDrawdown
                            itmCashSummary.TransactionDate = .AllocationDate
                            itmCashSummary.Participant.IDNumber = itmPerParticipant.IDNumber.IDNumber
                            itmCashSummary.Participant.FullName = (From x In _Participants _
                                                                   Where x.IDNumber = _itmPerParticipant.IDNumber.IDNumber _
                                                                   Select x.FullName).FirstOrDefault
                            itmCashSummary.JournalVoucherNo = (From x In _GetJournalVoucher _
                                                               Where x.BatchCode = .BatchCode _
                                                               Select x.JVNumber).FirstOrDefault.ToString
                            itmCashSummary.DocumentNo = "FTF-" & .RefNo.ToString
                            itmCashSummary.Credit = Math.Abs(_itmPerParticipant.Amount)
                        End With
                        _lstCashSummary.Add(itmCashSummary)
                    Next
                Next

                For Each itmReplenishment In _FTFReplenishment
                    For Each itmPerParticipant In itmReplenishment.ListOfFTFParticipants
                        Dim _itmPerParticipant = itmPerParticipant
                        Dim itmCashSummary As New CashSummary
                        With itmReplenishment
                            itmCashSummary.TransactionDate = .AllocationDate
                            itmCashSummary.Participant.IDNumber = itmPerParticipant.IDNumber.IDNumber
                            itmCashSummary.Participant.FullName = (From x In _Participants _
                                                                   Where x.IDNumber = _itmPerParticipant.IDNumber.IDNumber _
                                                                   Select x.FullName).FirstOrDefault
                            itmCashSummary.JournalVoucherNo = (From x In _GetJournalVoucher _
                                                               Where x.BatchCode = .BatchCode _
                                                               Select x.JVNumber).FirstOrDefault.ToString
                            itmCashSummary.DocumentNo = "FTF-" & .RefNo.ToString
                            itmCashSummary.Debit = Math.Abs(_itmPerParticipant.Amount)
                        End With
                        _lstCashSummary.Add(itmCashSummary)
                    Next
                Next
            Else
                BeginBalance = (From x In _GetBeginningBalance _
                                Where x.BankReconType = EnumBankReconType.NSS _
                                Select x.BeginningBalance).FirstOrDefault

                'Get Fund Transfer Form
                Dim _TransferNSSToSTL As New List(Of FundTransferFormMain)
                _TransferNSSToSTL = Me.WBillHelper.GetFundTransferForm(CDate(FormatDateTime(dtp_From.Value, DateFormat.ShortDate)), CDate(FormatDateTime(dtp_to.Value, DateFormat.ShortDate)), EnumFTFTransType.TransferNSSToSTL)
                Dim _TransferSTLToNSS As New List(Of FundTransferFormMain)
                _TransferSTLToNSS = Me.WBillHelper.GetFundTransferForm(CDate(FormatDateTime(dtp_From.Value, DateFormat.ShortDate)), CDate(FormatDateTime(dtp_to.Value, DateFormat.ShortDate)), EnumFTFTransType.TransferSTLToNSS)

                For Each itmReplenishment In _TransferNSSToSTL
                    If itmReplenishment.ListOfFTFParticipants.Count = 0 Then
                        Dim itmCashSummary As New CashSummary
                        With itmReplenishment
                            itmCashSummary.TransactionDate = .AllocationDate
                            itmCashSummary.JournalVoucherNo = (From x In _GetJournalVoucher _
                                                               Where x.BatchCode = .BatchCode _
                                                               Select x.JVNumber).FirstOrDefault.ToString
                            itmCashSummary.DocumentNo = "FTF-" & .RefNo.ToString
                            itmCashSummary.Credit = itmReplenishment.TotalAmount
                        End With
                        _lstCashSummary.Add(itmCashSummary)
                    Else
                        For Each itmPerParticipant In itmReplenishment.ListOfFTFParticipants
                            Dim _itmPerParticipant = itmPerParticipant
                            Dim itmCashSummary As New CashSummary
                            With itmReplenishment
                                itmCashSummary.TransactionDate = .AllocationDate
                                itmCashSummary.Participant.IDNumber = itmPerParticipant.IDNumber.IDNumber
                                itmCashSummary.Participant.FullName = (From x In _Participants _
                                                                       Where x.IDNumber = _itmPerParticipant.IDNumber.IDNumber _
                                                                       Select x.FullName).FirstOrDefault
                                itmCashSummary.JournalVoucherNo = (From x In _GetJournalVoucher _
                                                                   Where x.BatchCode = .BatchCode _
                                                                   Select x.JVNumber).FirstOrDefault.ToString
                                itmCashSummary.DocumentNo = "FTF-" & .RefNo.ToString
                                itmCashSummary.Credit = Math.Abs(_itmPerParticipant.Amount)
                            End With
                            _lstCashSummary.Add(itmCashSummary)
                        Next
                    End If
                Next

                For Each itmReplenishment In _TransferSTLToNSS
                    If itmReplenishment.ListOfFTFParticipants.Count = 0 Then
                        Dim itmCashSummary As New CashSummary
                        With itmReplenishment
                            itmCashSummary.TransactionDate = .AllocationDate
                            itmCashSummary.JournalVoucherNo = (From x In _GetJournalVoucher _
                                                               Where x.BatchCode = .BatchCode _
                                                               Select x.JVNumber).FirstOrDefault.ToString
                            itmCashSummary.DocumentNo = "FTF-" & .RefNo.ToString
                            itmCashSummary.Debit = itmReplenishment.TotalAmount
                        End With
                        _lstCashSummary.Add(itmCashSummary)
                    Else
                        For Each itmPerParticipant In itmReplenishment.ListOfFTFParticipants
                            Dim _itmPerParticipant = itmPerParticipant
                            Dim itmCashSummary As New CashSummary
                            With itmReplenishment
                                itmCashSummary.TransactionDate = .AllocationDate
                                itmCashSummary.Participant.IDNumber = itmPerParticipant.IDNumber.IDNumber
                                itmCashSummary.Participant.FullName = (From x In _Participants _
                                                                       Where x.IDNumber = _itmPerParticipant.IDNumber.IDNumber _
                                                                       Select x.FullName).FirstOrDefault
                                itmCashSummary.JournalVoucherNo = (From x In _GetJournalVoucher _
                                                                   Where x.BatchCode = .BatchCode _
                                                                   Select x.JVNumber).FirstOrDefault.ToString
                                itmCashSummary.DocumentNo = "FTF-" & .RefNo.ToString
                                itmCashSummary.Debit = Math.Abs(_itmPerParticipant.Amount)
                            End With
                            _lstCashSummary.Add(itmCashSummary)
                        Next
                    End If
                Next

            End If

            Select Case cbo_ColumnSort.SelectedIndex
                Case 0
                    If rb_Ascending.Checked Then
                        _lstCashSummary = (From x In _lstCashSummary _
                                       Select x Order By x.TransactionDate Ascending).ToList
                    Else
                        _lstCashSummary = (From x In _lstCashSummary _
                                       Select x Order By x.TransactionDate Descending).ToList
                    End If
                Case 1
                    If rb_Ascending.Checked Then
                        _lstCashSummary = (From x In _lstCashSummary _
                                       Select x Order By x.JournalVoucherNo Ascending).ToList
                    Else
                        _lstCashSummary = (From x In _lstCashSummary _
                                       Select x Order By x.JournalVoucherNo Descending).ToList
                    End If
                Case 2
                    If rb_Ascending.Checked Then
                        _lstCashSummary = (From x In _lstCashSummary _
                                       Select x Order By x.Participant.IDNumber Ascending).ToList
                    Else
                        _lstCashSummary = (From x In _lstCashSummary _
                                       Select x Order By x.Participant.IDNumber Descending).ToList
                    End If
                Case 3
                    If rb_Ascending.Checked Then
                        _lstCashSummary = (From x In _lstCashSummary _
                                       Select x Order By x.Participant.FullName Ascending).ToList
                    Else
                        _lstCashSummary = (From x In _lstCashSummary _
                                       Select x Order By x.Participant.FullName Descending).ToList
                    End If
            End Select

            If _lstCashSummary.Count = 0 Then
                MsgBox("No records found.", MsgBoxStyle.Exclamation, "Error")
                Exit Sub
            End If

            Dim Signatory = WBillHelper.GetSignatories("CSR").FirstOrDefault

            For Each itmCashSummary In _lstCashSummary
                Dim dr As DataRow
                dr = dt.NewRow
                With itmCashSummary
                    dr(dt.CREDITColumn) = Math.Abs(.Credit)
                    dr(dt.DEBITColumn) = Math.Abs(.Debit)
                    dr(dt.DISTRIBUTION_REFERENCEColumn) = .DistributionRef
                    dr(dt.JV_NOColumn) = .JournalVoucherNo
                    dr(dt.MASTER_NUMBERColumn) = .DocumentNo
                    dr(dt.MP_FULLNAMEColumn) = .Participant.FullName
                    dr(dt.MP_IDColumn) = .Participant.IDNumber
                    dr(dt.TRANSACTION_DATEColumn) = .TransactionDate
                    dr(dt.PREPARED_BYColumn) = AMModule.FullName
                    dr(dt.PREPARED_POSITIONColumn) = AMModule.Position
                    dr(dt.CHECKED_BYColumn) = Signatory.Signatory_1
                    dr(dt.CHECKED_POSITIONColumn) = Signatory.Position_1
                    dr(dt.APPROVED_BYColumn) = Signatory.Signatory_2
                    dr(dt.APPROVED_POSITIONColumn) = Signatory.Position_2
                End With
                dt.Rows.Add(dr)
                dt.AcceptChanges()
            Next

            ProgressThread.Show("Please wait while preparing Cash Summary Report.")
            Dim rpViewer As New frmReportViewer
            With rpViewer
                .LoadCashSummaryReport(dt, dtp_From.Value, dtp_to.Value, Me.txt_AccountCode.Text, Me.cbo_AccountToGenerate.SelectedItem.ToString, _
                                       BeginBalance, Me.cbo_ColumnSort.SelectedItem.ToString, Me.WBillHelper.UserName, SystemDate)
                .Show()
            End With

        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Updated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepCashSummaryReportWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInGeneratingReport.ToString, AMModule.UserName)
            Exit Sub
        Finally
            ProgressThread.Close()
        End Try
    End Sub

    Private Sub cmd_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Close.Click
        Me.Close()
    End Sub
End Class