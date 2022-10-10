'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmChecks
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     January 4, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for Viewing and/or Printing of Check, Check Voucher, Check Register
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementObjects
Imports AccountsManagementLogic
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmChecks
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private lstParticipants As List(Of AMParticipants)
    Public lstChecks As New List(Of Check)
    Public lstUpdateChecks As New List(Of Check)
    Public ForCancelCheck As New Check
    Public isCancelled As New Boolean
    Public DateUpdate As New Date
    Public MaxDate As New Date
    Public MinDate As New Date


    Private Sub frmChecks_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
            WBillHelper = WESMBillHelper.GetInstance()

            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            BFactory = BusinessFactory.GetInstance()

            Me.lstParticipants = WBillHelper.GetAMParticipants()

            Me.FillParticipants()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Udpated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepChecksWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
            Exit Sub
        End Try
    End Sub

    Private Sub chkbox_Status_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_Status.CheckedChanged
        If Me.chkbox_Status.Checked Then
            gBox_Status.Enabled = True
        Else
            gBox_Status.Enabled = False
        End If
    End Sub

    Private Sub chkbox_EnableNumber_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_EnableNumber.CheckedChanged
        If Me.chkbox_EnableNumber.Checked Then
            gBox_Number.Enabled = True
            Me.txt_Number.Text = ""
        Else
            gBox_Number.Enabled = False
        End If
    End Sub

    Private Sub chkbox_EnableDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_EnableDate.CheckedChanged
        If Me.chkbox_EnableDate.Checked Then
            gBox_TransactionDate.Enabled = True
        Else
            gBox_TransactionDate.Enabled = False
        End If
    End Sub

    Private Sub chkbox_EnablePID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_EnablePID.CheckedChanged
        If Me.chkbox_EnablePID.Checked Then
            gBox_ParticipantID.Enabled = True
        Else
            gBox_ParticipantID.Enabled = False
        End If
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub FillParticipants()
        Me.cbo_Participants.DataSource = (From x In lstParticipants _
                                          Select x.ParticipantID).ToList
    End Sub

    Private Sub cmd_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Search.Click
        frmCheckSearch.Show()
    End Sub

    Public Sub FillDataTable(ByVal lstCheck As List(Of Check))
        Try
            If lstCheck.Count > 0 Then
                dgv_ViewDetails.Rows.Clear()
            Else
                MsgBox("No Records Found!", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, MsgBoxStyle), "No records found")
                dgv_ViewDetails.Rows.Clear()
                Exit Sub
            End If

            For Each itmCheck In lstCheck
                With itmCheck

                    Dim ReleaseDate As String = ""
                    Dim ClearedDate As String = ""

                    If itmCheck.DateReleased <> "" Then
                        ReleaseDate = FormatDateTime(CDate(itmCheck.DateReleased), DateFormat.ShortDate)
                    End If

                    If itmCheck.DateCleared <> "" Then
                        ClearedDate = FormatDateTime(CDate(itmCheck.DateCleared), DateFormat.ShortDate)
                    End If


                    dgv_ViewDetails.Rows.Add(.BatchCode, FormatDateTime(.TransactionDate, DateFormat.ShortDate), _
                                             .Participant.ParticipantID, _
                                             .Status.ToString, _
                                             False, _
                                             .CheckNumber, _
                                             .VoucherNumber, _
                                             FormatNumber(.Amount, 2, TriState.True, TriState.True), _
                                             ReleaseDate, _
                                             ClearedDate, _
                                             .Remarks)
                End With
            Next

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Udpated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepChecksWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInRetrieving.ToString, AMModule.UserName)
            Exit Sub
        End Try

    End Sub

    Private Sub dgv_ViewDetails_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgv_ViewDetails.CellBeginEdit
        If dgv_ViewDetails.Rows(e.RowIndex).Cells("status").Value.ToString = EnumCheckStatus.Cancelled.ToString Then
            e.Cancel = True
        End If
    End Sub

    'Sub dgv_ViewDetails_CellContentClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv_ViewDetails.CurrentCellDirtyStateChanged
    'If dgv_ViewDetails.IsCurrentCellDirty Then
    '    If Me.dgv_ViewDetails.CurrentRow.Cells("status").Value.ToString <> EnumCheckStatus.Cancelled.ToString Then
    '        dgv_ViewDetails.CommitEdit(DataGridViewDataErrorContexts.Commit)
    '    End If

    '    End If
    'End Sub

    Private Sub dgv_ViewDetails_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_ViewDetails.CellContentClick
        Select Case e.ColumnIndex
            Case 4
                If dgv_ViewDetails.Rows(e.RowIndex).Cells("status").Value.ToString = EnumCheckStatus.Cancelled.ToString Then

                    dgv_ViewDetails.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False
                    'MsgBox("Cannot print Cancelled checks, please select another record.", MsgBoxStyle.Exclamation, "Error!")
                    'Exit Sub
                End If

                '        If CBool(dgv_ViewDetails.Rows(e.RowIndex).Cells("colChkBox").Value) = True Then
                '            'No Check Number / Check Voucher yet
                '            Dim _chkInitialization = Me.WBillHelper.GetInitializationTable("CHECKS")
                '            Dim ans As New MsgBoxResult
                '            ans = MsgBox("Do you want to Generate the check for " & dgv_ViewDetails.Rows(e.RowIndex).Cells("participantID").Value.ToString & _
                '                         " with the Check No: " & _chkInitialization.LastSeqUsed + 1 & "?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Cofirm Check Number Generation")

                '            If ans = MsgBoxResult.Yes Then
                '                Dim updateCheck = (From x In lstChecks _
                '                                   Where x.BatchCode = dgv_ViewDetails.Rows(e.RowIndex).Cells("batchCode").Value.ToString _
                '                                   And x.Participant.ParticipantID = CStr(dgv_ViewDetails.Rows(e.RowIndex).Cells("participantID").Value.ToString) _
                '                                   Select x).FirstOrDefault

                '                updateCheck.CheckNumber = CStr(_chkInitialization.LastSeqUsed + 1)
                '                updateCheck.Status = EnumCheckStatus.FinalPrinted
                '                updateCheck.VoucherNumber = Me.WBillHelper.GetSequenceID("SEQ_AM_CHECK_VOUCHER_NO").ToString

                '                updateCheck = Me.CreateCheckVoucher(updateCheck)
                '                WBillHelper.UpdateChecksAndVoucher(updateCheck, EnumCheckStatus.FinalPrinted)

                '                dgv_ViewDetails.Rows(e.RowIndex).Cells("chNumber").Value = updateCheck.CheckNumber
                '                dgv_ViewDetails.Rows(e.RowIndex).Cells("cvNumber").Value = updateCheck.VoucherNumber
                '                dgv_ViewDetails.Rows(e.RowIndex).Cells("Status").Value = EnumCheckStatus.FinalPrinted.ToString
                '                dgv_ViewDetails.Rows(e.RowIndex).Cells("colBtnGenerate").Value = "Print Check"
                '            End If
                '        Else
                '            'With Check Number and Check Voucher
                '            Dim ans As New MsgBoxResult
                '            ans = MsgBox("Do you want to Print the check for " & dgv_ViewDetails.Rows(e.RowIndex).Cells("participantID").Value.ToString & _
                '                         " with the Check No: " & dgv_ViewDetails.Rows(e.RowIndex).Cells("chNumber").Value.ToString & "?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Cofirm Check Number Generation")

                '            If ans = MsgBoxResult.Yes Then

                '            End If
                '        End If


        End Select
    End Sub

    Private Function CreateCheckVoucher(ByVal CheckForSaving As Check) As Check
        Dim _CheckVoucherSignatory = Me.WBillHelper.GetSignatories("CV").FirstOrDefault

        With CheckForSaving.CheckVoucher
            .Participant = CheckForSaving.Participant
            .VoucherNumber = CheckForSaving.VoucherNumber
            .TransactionDate = CheckForSaving.TransactionDate
            .VoucherDetails.Add(New CheckVoucherDetails(CheckForSaving.VoucherNumber, CashInbankSettlementcode, 0, CheckForSaving.Amount))
            .VoucherDetails.Add(New CheckVoucherDetails(CheckForSaving.VoucherNumber, DebitCode, CheckForSaving.Amount, 0))
            .ApprovedBy = _CheckVoucherSignatory.Signatory_2
            .ApprovedPosition = _CheckVoucherSignatory.Position_2
            .CheckedBy = _CheckVoucherSignatory.Signatory_1
            .CheckedPosition = _CheckVoucherSignatory.Position_1
            .PreparedBy = AMModule.FullName
            .PreparedPosition = AMModule.Position


            'Check for Particulars
            'Get JV Number
            Dim _AMJV As JournalVoucher = (WBillHelper.GetJournalVoucherItem(CheckForSaving.BatchCode))            

            'Get Particulars using JVNumber
            Dim _RFPMain As RequestForPayment = WBillHelper.GetAMRFPMainForChecks(_AMJV.JVNumber)

            Dim _Particulars As String = (From x In _RFPMain.RFPDetails Where x.Participant = CheckForSaving.Participant.IDNumber Select x.Particulars).FirstOrDefault

            If _Particulars.Length <> 0 Then
                .Particulars = _Particulars
            End If
        End With

        Return CheckForSaving
    End Function

    Private Sub cmd_GenerateCV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenerateCV.Click
        Try
            If dgv_ViewDetails.RowCount = 0 Then
                MsgBox("No record Selected.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If dgv_ViewDetails.Rows(dgv_ViewDetails.CurrentRow.Index).Cells("Status").Value.ToString = EnumCheckStatus.Cancelled.ToString Then
                MsgBox("Cannot Generate Check Voucher. The selected entry is already cancelled.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If dgv_ViewDetails.Rows(dgv_ViewDetails.CurrentRow.Index).Cells("Status").Value.ToString = EnumCheckStatus.SystemGenerated.ToString Then
                MsgBox("Cannot Generate Check Voucher. The selected entry does not have any Checks/Check Voucher Generated.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            Dim dtVoucher As New DSReport.CheckVoucherDataTable

            Dim AcctCodes = Me.WBillHelper.GetAccountingCodes()
            Dim listOfParticipant = Me.WBillHelper.GetAMParticipants()

            Dim withCheck As Boolean = False
            Dim GenCheck As New List(Of Check)
            For rCtr = 0 To Me.dgv_ViewDetails.RowCount - 1
                If CBool(Me.dgv_ViewDetails.Rows(rCtr).Cells("colChkbox").Value) = True Then
                    'Get Currently Selected Item
                    withCheck = True
                    Dim _rCtr = rCtr
                    GenCheck.Add((From x In lstChecks _
                                    Where x.BatchCode = dgv_ViewDetails.Rows(_rCtr).Cells("batchCode").Value.ToString _
                                    And x.Participant.ParticipantID = dgv_ViewDetails.Rows(_rCtr).Cells("participantID").Value.ToString _
                                    And x.Status <> EnumCheckStatus.Cancelled _
                                    Select x).FirstOrDefault)
                End If
            Next

            If withCheck = False Then
                MsgBox("No Records selected", MsgBoxStyle.Critical, "Error Encountered")
                Exit Sub
            End If

            For Each itmGenCheck In GenCheck
                For Each itmCheck In itmGenCheck.CheckVoucher.VoucherDetails.Distinct()
                    Dim dr As DataRow
                    dr = dtVoucher.NewRow
                    With itmCheck
                        dr(dtVoucher.ACCOUNT_CODEColumn) = .AccountCode & " - " & (From x In AcctCodes _
                                                                                   Where x.AccountCode = .AccountCode _
                                                                                   Select x.Description).FirstOrDefault.ToString
                        dr(dtVoucher.AMOUNT_WORDSColumn) = Me.BFactory.NumberConvert(itmGenCheck.Amount)
                        dr(dtVoucher.AMOUNTColumn) = itmGenCheck.Amount
                        dr(dtVoucher.PARTICULARSColumn) = itmGenCheck.CheckVoucher.Particulars
                        dr(dtVoucher.APPROVED_BYColumn) = itmGenCheck.CheckVoucher.ApprovedBy
                        dr(dtVoucher.APPROVED_POSITIONColumn) = itmGenCheck.CheckVoucher.ApprovedPosition
                        dr(dtVoucher.PREPARED_BYColumn) = AMModule.FullName
                        dr(dtVoucher.PREPARED_POSITIONColumn) = AMModule.Position
                        dr(dtVoucher.CHECK_NUMBERColumn) = itmGenCheck.CheckNumber
                        dr(dtVoucher.CHECKED_BYColumn) = itmGenCheck.CheckVoucher.CheckedBy
                        dr(dtVoucher.CHECKED_POSITIONColumn) = itmGenCheck.CheckVoucher.CheckedPosition
                        dr(dtVoucher.CREDIT_AMOUNTColumn) = .Credit
                        dr(dtVoucher.DEBIT_AMOUNTColumn) = .Debit
                        dr(dtVoucher.DATEColumn) = FormatDateTime(itmGenCheck.TransactionDate, DateFormat.ShortDate)
                        dr(dtVoucher.CV_NUMBERColumn) = BFactory.GenerateBIRDocumentNumber(CLng(.VoucherNumber), BIRDocumentsType.CV)
                        dr(dtVoucher.ID_NUMBERColumn) = itmGenCheck.Participant.IDNumber
                        dr(dtVoucher.PARTICIPANT_IDColumn) = itmGenCheck.Participant.ParticipantID
                        dr(dtVoucher.BATCH_CODEColumn) = itmGenCheck.BatchCode
                        dr(dtVoucher.BUSINESS_STYLEColumn) = itmGenCheck.Participant.BusinessStyle
                        dr(dtVoucher.FULL_NAMEColumn) = itmGenCheck.Participant.FullName
                        dr(dtVoucher.BIR_PERMITColumn) = AMModule.BIRPermitNumber

                    End With
                    dtVoucher.Rows.Add(dr)
                    dtVoucher.AcceptChanges()
                Next
            Next
            ProgressThread.Show("Please wait while preparing CV.")

            Dim rptView As New frmReportViewer            
            With rptView
                .LoadCheckVoucher(dtVoucher)
                .Show()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Udpated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepChecksWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInGeneratingReport.ToString, AMModule.UserName)
            Exit Sub
        Finally
            ProgressThread.Close()
        End Try
    End Sub

    Private Sub cmd_GenerateRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenerateRegister.Click
        Try
            If dgv_ViewDetails.RowCount = 0 Then
                MsgBox("No records available.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            Dim cRegister As New DSReport.CheckRegisterDataTable

            Dim lstChkRegister = (From x In lstChecks _
                                  Where x.CheckNumber <> "0" _
                                  And x.VoucherNumber <> "" _
                                  And x.Status <> EnumCheckStatus.Cancelled _
                                  And x.Status <> EnumCheckStatus.Cleared _
                                  And x.Status <> EnumCheckStatus.Released _
                                  Select x).ToList

            If lstChkRegister.Count = 0 Then
                MsgBox("No records available.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            For Each itmCheck In lstChkRegister
                Dim dr As DataRow
                dr = cRegister.NewRow

                With itmCheck
                    dr(cRegister.AMOUNTColumn) = .Amount
                    dr(cRegister.CHECK_NUMBERColumn) = .CheckNumber
                    dr(cRegister.DATEColumn) = FormatDateTime(.TransactionDate, DateFormat.ShortDate)
                    dr(cRegister.PAYEEColumn) = .Participant.FullName
                    dr(cRegister.VOUCHER_NOColumn) = .VoucherNumber
                End With
                cRegister.Rows.Add(dr)
                cRegister.AcceptChanges()
            Next


            Dim dFrom As New Date
            Dim dTo As New Date

            If MaxDate <> New Date And MinDate <> New Date Then
                dFrom = CDate(FormatDateTime(MinDate, DateFormat.ShortDate))
                dTo = CDate(FormatDateTime(MaxDate, DateFormat.ShortDate))
            Else
                dFrom = (From x In lstChecks _
                     Select x.TransactionDate).Min

                dTo = (From x In lstChecks _
                       Select x.TransactionDate).Max
            End If
            ProgressThread.Show("Please wait while preparing Register.")
            Dim rptView As New frmReportViewer            
            With rptView                
                .LoadCheckRegister(cRegister, dFrom, dTo)
                ProgressThread.Close()
                .ShowDialog()
            End With

        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Udpated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepChecksWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInGeneratingReport.ToString, AMModule.UserName)
            Exit Sub
        Finally
            ProgressThread.Close()
        End Try
    End Sub

    Private Sub cmd_ChangeCheckNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ChangeCheckNo.Click
        Try
            If dgv_ViewDetails.RowCount = 0 Then
                MsgBox("No record Selected.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If dgv_ViewDetails.Rows(dgv_ViewDetails.CurrentRow.Index).Cells("Status").Value.ToString = EnumCheckStatus.Cancelled.ToString Then
                MsgBox("Cannot cancel Check. The selected entry is already cancelled.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If dgv_ViewDetails.Rows(dgv_ViewDetails.CurrentRow.Index).Cells("Status").Value.ToString = EnumCheckStatus.SystemGenerated.ToString Then
                MsgBox("Cannot cancel Check. No check is found for the entry.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If dgv_ViewDetails.Rows(dgv_ViewDetails.CurrentRow.Index).Cells("Status").Value.ToString = EnumCheckStatus.Cleared.ToString Then
                MsgBox("Cannot cancel Check. The selected entry is already Cleared.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            Dim ans As New MsgBoxResult

            'Check to Cancel
            Dim CancelCheck = (From x In lstChecks _
                               Where x.BatchCode = dgv_ViewDetails.Rows(dgv_ViewDetails.CurrentRow.Index).Cells("batchCode").Value.ToString _
                               And x.Participant.ParticipantID = dgv_ViewDetails.Rows(dgv_ViewDetails.CurrentRow.Index).Cells("participantID").Value.ToString _
                               And x.Status.ToString = dgv_ViewDetails.Rows(dgv_ViewDetails.CurrentRow.Index).Cells("Status").Value.ToString _
                               Select x).FirstOrDefault

            ans = MsgBox("Do you really want to cancel Check No: " & CancelCheck.CheckNumber & " of Participant: " & CancelCheck.Participant.ParticipantID & " and generate a new Check?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Cancel Check")

            If ans = MsgBoxResult.Yes Then
                Dim _chkInitialization = Me.WBillHelper.GetInitializationTable("CHECKS")

                Dim frmRemarks As New frmChecksAddRemarks
                With frmRemarks
                    ._CheckForUpdate = CancelCheck
                    .ShowDialog()
                End With

                If isCancelled = False Then
                    CancelCheck.Status = EnumCheckStatus.Cancelled

                    'Check to Insert
                    Dim NewCheck As New Check
                    With CancelCheck
                        NewCheck.Status = EnumCheckStatus.ReplacementForCancelled
                        NewCheck.Amount = CancelCheck.Amount
                        NewCheck.BatchCode = CancelCheck.BatchCode
                        NewCheck.CheckNumber = CStr(_chkInitialization.LastSeqUsed + 1)
                        NewCheck.CheckType = EnumCheckType.SCB
                        NewCheck.Participant = .Participant
                        NewCheck.TransactionDate = .TransactionDate
                        NewCheck.VoucherNumber = CancelCheck.CheckVoucher.VoucherNumber
                    End With
                    'NewCheck = Me.CreateCheckVoucher(NewCheck)
                    NewCheck.CheckVoucher = CancelCheck.CheckVoucher

                    WBillHelper.UpdateCancelledChecksAndVoucher(ForCancelCheck, NewCheck, EnumCheckStatus.ReplacementForCancelled)
                    MsgBox("Successfully saved!", MsgBoxStyle.Exclamation, "Success!")
                    'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_CheckWindow.ToString, eLogsStatus.Successful, "Successfully cancelled check " & CancelCheck.CheckNumber & ".")
                    dgv_ViewDetails.Rows(dgv_ViewDetails.CurrentRow.Index).Cells("status").Value = EnumCheckStatus.Cancelled.ToString
                    dgv_ViewDetails.Rows(dgv_ViewDetails.CurrentRow.Index).Cells("colChkBox").Value = False
                    dgv_ViewDetails.Rows(dgv_ViewDetails.CurrentRow.Index).Cells("Remarks").Value = ForCancelCheck.Remarks
                    With NewCheck
                        dgv_ViewDetails.Rows.Add(.BatchCode, FormatDateTime(.TransactionDate, DateFormat.ShortDate), _
                             .Participant.ParticipantID, _
                             .Status.ToString, _
                             False, _
                             .CheckNumber, _
                             .VoucherNumber, _
                             FormatNumber(.Amount, 2, TriState.True, TriState.True), _
                             .Remarks)
                    End With
                    lstChecks.Add(NewCheck)
                Else
                    ForCancelCheck = New Check
                    isCancelled = False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Udpated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepChecksWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInSaving.ToString, AMModule.UserName)
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_CreateCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_CreateCheck.Click
        Try
            Dim _lstUpdateChecks As New List(Of Check)
            Dim ctr As Integer = 1
            Dim withCheck As Boolean = False

            Dim chkCtr As Integer = 0

            For rCtr = 0 To Me.dgv_ViewDetails.RowCount - 1
                If CBool(Me.dgv_ViewDetails.Rows(rCtr).Cells("colChkBox").Value) = True Then
                    If Me.dgv_ViewDetails.Rows(rCtr).Cells("status").Value.ToString = EnumCheckStatus.SystemGenerated.ToString Then
                        chkCtr += 1
                    Else
                        MsgBox("Invalid selection found, Checks can only be generated for records with the status of SystemGenerated.", MsgBoxStyle.Critical, "Error!")
                        Exit Sub
                    End If
                End If
            Next

            If chkCtr = 0 Then
                MsgBox("No records selected. Please select at least one.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            Dim _GetCheckNumber = WBillHelper.GetInitializationTable("CHECKS")
            Dim _chkAvailable As Integer = 0
            'Check Sequence of Check number
            If _GetCheckNumber.LastSeqUsed = _GetCheckNumber.LastNo Then
                MsgBox("No Available check number to be used, please enter a new sequence.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            Else
                _chkAvailable = CInt(_GetCheckNumber.LastNo - _GetCheckNumber.LastSeqUsed)
                If _chkAvailable < chkCtr Then
                    MsgBox("The " & _chkAvailable & " selected participants will be generated with Checks, Please enter a new sequence after the process", MsgBoxStyle.Exclamation, "Warning!")
                End If
            End If

            Dim ans As New MsgBoxResult
            ans = MsgBox("Do you really want to Create Check/Check Voucher for the selected Participant/s?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Save Records")

            If ans = MsgBoxResult.Yes Then

                For rCtr = 0 To Me.dgv_ViewDetails.RowCount - 1
                    If CBool(Me.dgv_ViewDetails.Rows(rCtr).Cells("colChkBox").Value) = True Then
                        If Me.dgv_ViewDetails.Rows(rCtr).Cells("status").Value.ToString <> EnumCheckStatus.SystemGenerated.ToString Then
                            MsgBox("One or more selected record is already Released/Cleared, Please check your selection and try again.", MsgBoxStyle.Critical, "Error")
                            Exit Sub
                        End If
                        Dim _rCtr = rCtr

                        If _chkAvailable = _lstUpdateChecks.Count Then
                            Exit For
                        End If

                        withCheck = True
                        Dim UpdateCheck As New Check

                        UpdateCheck = (From x In lstChecks _
                                            Where x.BatchCode = Me.dgv_ViewDetails.Rows(_rCtr).Cells("batchCode").Value.ToString _
                                            And x.Participant.ParticipantID = Me.dgv_ViewDetails.Rows(_rCtr).Cells("participantID").Value.ToString _
                                            And x.Status.ToString = Me.dgv_ViewDetails.Rows(_rCtr).Cells("status").Value.ToString _
                                            Select x).FirstOrDefault

                        UpdateCheck.CheckNumber = ctr.ToString
                        UpdateCheck.Status = EnumCheckStatus.GeneratedCheck
                        UpdateCheck.VoucherNumber = Me.WBillHelper.GetSequenceID("SEQ_AM_CHECK_VOUCHER_NO").ToString
                        UpdateCheck = Me.CreateCheckVoucher(UpdateCheck)

                        _lstUpdateChecks.Add(UpdateCheck)

                        ctr += 1
                    End If
                Next


                WBillHelper.UpdateChecksAndVoucher(_lstUpdateChecks, EnumCheckStatus.GeneratedCheck)
                Me.dgv_ViewDetails.Rows.Clear()
                Me.FillDataTable(lstChecks)
                MsgBox("Successfully updated Checks and Check Voucher for Selected Participants.", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Success!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Udpated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepChecksWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInEditing.ToString, AMModule.UserName)
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_DateReleased_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_DateReleased.Click
        Try
            lstUpdateChecks.Clear()
            Dim withCheck As Boolean = False
            For rCtr = 0 To Me.dgv_ViewDetails.RowCount - 1
                If CBool(dgv_ViewDetails.Rows(rCtr).Cells("colChkBox").Value) = True Then
                    withCheck = True
                    If Me.dgv_ViewDetails.Rows(rCtr).Cells("status").Value.ToString = EnumCheckStatus.Cancelled.ToString Then
                        MsgBox("Cannot release cancelled checks. Please check the selection and try again.", MsgBoxStyle.Critical, "Error")
                        Exit Sub
                    End If

                    If Me.dgv_ViewDetails.Rows(rCtr).Cells("status").Value.ToString = EnumCheckStatus.SystemGenerated.ToString Then
                        MsgBox("Cannot Release Checks for Participants w/o Generated Checks. Please check the selection and try again.", MsgBoxStyle.Critical, "Error")
                        Exit Sub
                    End If

                    If Me.dgv_ViewDetails.Rows(rCtr).Cells("status").Value.ToString = EnumCheckStatus.Released.ToString Then
                        MsgBox("Cannot update date released for participants with Released Checks. Please check the selection and try again.", MsgBoxStyle.Critical, "Error")
                        Exit Sub
                    End If

                    If Me.dgv_ViewDetails.Rows(rCtr).Cells("status").Value.ToString = EnumCheckStatus.Cleared.ToString Then
                        MsgBox("Cannot update date released for participants with Cleared Checks. Please check the selection and try again.", MsgBoxStyle.Critical, "Error")
                        Exit Sub
                    End If

                    Dim _rctr = rCtr
                    Dim UpdateCheck = (From x In lstChecks _
                                       Where x.BatchCode = Me.dgv_ViewDetails.Rows(_rctr).Cells("batchCode").Value.ToString _
                                       And x.Participant.ParticipantID = Me.dgv_ViewDetails.Rows(_rctr).Cells("participantID").Value.ToString _
                                       And x.Status.ToString = Me.dgv_ViewDetails.Rows(_rctr).Cells("status").Value.ToString _
                                       Select x).FirstOrDefault
                    lstUpdateChecks.Add(UpdateCheck)
                End If
            Next

            If withCheck = False Then
                MsgBox("No Records selected", MsgBoxStyle.Critical, "Error Encountered")
                Exit Sub
            End If

            Dim UpdateDate As New frmCheckDates
            With UpdateDate
                .Text = "Update: Date Released"
                .grp_SelectDate.Text = "Please Select Date Released"
                ._isRelease = True
                ._lstCheckForUpdate = lstUpdateChecks
                .ShowDialog()
            End With

            If isCancelled = False Then
                For Each itmUpdate In lstUpdateChecks
                    Dim _itmUpdate = itmUpdate

                    With _itmUpdate
                        Dim _ForUpdate = (From x In lstChecks _
                                          Where x.BatchCode = .BatchCode _
                                          And x.Participant.IDNumber = .Participant.IDNumber _
                                          And x.Status = .Status _
                                          And x.VoucherNumber = .VoucherNumber _
                                          Select x).FirstOrDefault

                        _ForUpdate.DateReleased = .DateReleased
                    End With
                Next
                Me.lstUpdateChecks.Clear()
                Me.FillDataTable(lstChecks)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Udpated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepChecksWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInEditing.ToString, AMModule.UserName)
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_DateCleared_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_DateCleared.Click
        Try
            lstUpdateChecks.Clear()
            Dim withCheck As Boolean = False
            For rCtr = 0 To Me.dgv_ViewDetails.RowCount - 1
                If CBool(dgv_ViewDetails.Rows(rCtr).Cells("colChkBox").Value) = True Then
                    withCheck = True
                    If Me.dgv_ViewDetails.Rows(rCtr).Cells("dteReleased").Value.ToString = "" Then
                        MsgBox("Cannot clear checks that are not yet released. Please check the selection and try again.", MsgBoxStyle.Critical, "Error")
                        Exit Sub
                    End If

                    If Me.dgv_ViewDetails.Rows(rCtr).Cells("status").Value.ToString = EnumCheckStatus.Cancelled.ToString Then
                        MsgBox("Cannot clear cancelled checks. Please check the selection and try again.", MsgBoxStyle.Critical, "Error")
                        Exit Sub
                    End If

                    If Me.dgv_ViewDetails.Rows(rCtr).Cells("status").Value.ToString = EnumCheckStatus.SystemGenerated.ToString Then
                        MsgBox("Cannot clear Participants without checks. Please check the selection and try again.", MsgBoxStyle.Critical, "Error")
                        Exit Sub
                    End If

                    If Me.dgv_ViewDetails.Rows(rCtr).Cells("status").Value.ToString = EnumCheckStatus.Cleared.ToString Then
                        MsgBox("Cannot update date released for participants with Cleared Checks. Please check the selection and try again.", MsgBoxStyle.Critical, "Error")
                        Exit Sub
                    End If

                    Dim _rctr = rCtr

                    Dim UpdateCheck = (From x In lstChecks _
                                       Where x.BatchCode = Me.dgv_ViewDetails.Rows(_rctr).Cells("batchCode").Value.ToString _
                                       And x.Participant.ParticipantID = Me.dgv_ViewDetails.Rows(_rctr).Cells("participantID").Value.ToString _
                                       And x.Status.ToString = Me.dgv_ViewDetails.Rows(_rctr).Cells("status").Value.ToString _
                                       Select x).FirstOrDefault
                    lstUpdateChecks.Add(UpdateCheck)
                End If
            Next

            If withCheck = False Then
                MsgBox("No Records selected", MsgBoxStyle.Critical, "Error Encountered")
                Exit Sub
            End If

            Dim UpdateDate As New frmCheckDates
            With UpdateDate
                .Text = "Update: Date Cleared"
                .grp_SelectDate.Text = "Please Select Date Cleared"
                ._isRelease = False
                ._lstCheckForUpdate = lstUpdateChecks
                .ShowDialog()
            End With

            If isCancelled = False Then
                For Each itmUpdate In lstUpdateChecks
                    Dim _itmUpdate = itmUpdate

                    With _itmUpdate
                        Dim _ForUpdate = (From x In lstChecks _
                                          Where x.BatchCode = .BatchCode _
                                          And x.Participant.IDNumber = .Participant.IDNumber _
                                          And x.Status = .Status _
                                          And x.VoucherNumber = .VoucherNumber _
                                          Select x).FirstOrDefault

                        _ForUpdate.DateCleared = .DateCleared
                    End With
                Next
                Me.lstUpdateChecks.Clear()
                Me.FillDataTable(lstChecks)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Udpated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepChecksWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInEditing.ToString, AMModule.UserName)
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_OutstandingSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_OutstandingSummary.Click
        Try
            Dim lstOutstanding As New List(Of Check)
            Dim _Checks = Me.WBillHelper.GetChecksOutstanding()
            For Each itmOutstanding In _Checks

                If itmOutstanding.Status = EnumCheckStatus.Cancelled Or itmOutstanding.Status = EnumCheckStatus.SystemGenerated Or _
                itmOutstanding.Status = EnumCheckStatus.ReplacementForCancelled Then
                    Continue For
                End If

                If itmOutstanding.Status <> EnumCheckStatus.Cleared Then
                    lstOutstanding.Add(itmOutstanding)
                    Continue For
                End If

                If CDate(FormatDateTime(CDate(itmOutstanding.DateCleared), DateFormat.ShortDate)) > MaxDate Then
                    lstOutstanding.Add(itmOutstanding)
                    Continue For
                End If
            Next

            Dim cRegister As New DSReport.CheckRegisterDataTable

            If lstOutstanding.Count = 0 Then
                MsgBox("No Outstanding Checks found.", MsgBoxStyle.Critical, "Outstanding Checks Generation")
                Exit Sub
            End If

            For Each itmCheck In lstOutstanding
                Dim dr As DataRow
                dr = cRegister.NewRow

                With itmCheck
                    dr(cRegister.AMOUNTColumn) = .Amount
                    dr(cRegister.CHECK_NUMBERColumn) = .CheckNumber

                    Dim _tmpDate As String
                    If .DateReleased.Length = 0 Then
                        _tmpDate = "Not Yet Released"
                    Else
                        _tmpDate = FormatDateTime(CDate(.DateReleased), DateFormat.ShortDate)
                    End If

                    dr(cRegister.DATEColumn) = _tmpDate
                    dr(cRegister.PAYEEColumn) = .Participant.FullName
                    dr(cRegister.VOUCHER_NOColumn) = .VoucherNumber
                End With
                cRegister.Rows.Add(dr)
                cRegister.AcceptChanges()
            Next


            Dim dFrom As New Date
            Dim dTo As New Date

            'If MaxDate <> New Date And MinDate <> New Date Then
            'dFrom = CDate(FormatDateTime(MinDate, DateFormat.ShortDate))
            dTo = CDate(FormatDateTime(SystemDate, DateFormat.ShortDate))
            'Else
            '    dFrom = (From x In lstChecks _
            '         Select x.TransactionDate).Min

            '    dTo = (From x In lstChecks _
            '           Select x.TransactionDate).Max
            'End If
            ProgressThread.Show("Please wait while preparing Outstanding Summary Report.")
            Dim rptView As New frmReportViewer            
            With rptView
                .LoadOutstandingCheck(cRegister, dFrom, dTo)
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Udpated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepChecksWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInEditing.ToString, AMModule.UserName)
            Exit Sub
        Finally
            ProgressThread.Close()
        End Try
    End Sub

    Private Sub chk_allparticipants_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_allparticipants.CheckedChanged
        For rCtr = 0 To Me.dgv_ViewDetails.RowCount - 1
            If Me.dgv_ViewDetails.Rows(rCtr).Cells("status").Value.ToString <> EnumCheckStatus.Cancelled.ToString Then
                Me.dgv_ViewDetails.Rows(rCtr).Cells("colChkBox").Value = Me.chk_allparticipants.Checked
            End If
        Next
    End Sub

    Private Sub cmd_PrintCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_PrintCheck.Click

        If dgv_ViewDetails.RowCount = 0 Then
            MsgBox("No records available.", MsgBoxStyle.Critical, "Error!")
            Exit Sub
        End If

        If dgv_ViewDetails.CurrentRow.Cells("status").Value.ToString = EnumCheckStatus.Cancelled.ToString Or _
           dgv_ViewDetails.CurrentRow.Cells("status").Value.ToString = EnumCheckStatus.SystemGenerated.ToString Then
            MsgBox("No Check Available for printing.", MsgBoxStyle.Critical, "Error!")
            Exit Sub
        End If

        Dim _CheckDetails = (From x In lstChecks _
                             Where x.CheckNumber = Me.dgv_ViewDetails.CurrentRow.Cells("chNumber").Value.ToString _
                             Select x).FirstOrDefault

        'Dim GetDate As String = AMModule.SystemDate.ToString("MMMM dd, yyyy")
        If _CheckDetails.Status = EnumCheckStatus.Cleared Then
            MessageBox.Show("Already printed! Please contact the administrator.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Try            
            Dim chkView As New frmReportViewer
            With chkView
                .LoadCheckParams(_CheckDetails.Participant.CheckPay, BFactory.NumberConvert(_CheckDetails.Amount), _CheckDetails.Amount, CStr(_CheckDetails.TransactionDate.ToString("MMMM dd, yyyy")))
                .ShowDialog()
            End With
        Catch ex As Exception
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepChecksWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInPrinting.ToString, AMModule.UserName)
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub cmd_NewSequence_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_NewSequence.Click
        frmCheckNumber.Show()
    End Sub
End Class