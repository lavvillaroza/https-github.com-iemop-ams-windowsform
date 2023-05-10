'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmPrudentialInterest
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     November 14, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the Prudential Interest
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   November 14, 2012       Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
'Imports LDAPLib
'Imports LDAPLogin

Public Class frmPrudentialInterest
    Private _ListParticipants As List(Of AMParticipants)
    Private _TransactionType As EnumTransactionType
    Private _ListPRHistory As List(Of PrudentialHistory)
    Private _TransDate As Date

    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory

    Private Enum EnumTransactionType
        Interest
        Edit
        Save
        JournalVoucher
        SummaryReport
        Search
        PostToGP
    End Enum

    Private Sub frmPrudentialInterest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            BFactory = BusinessFactory.GetInstance()

            Me._ListParticipants = New List(Of AMParticipants)
            Me._ListPRHistory = New List(Of PrudentialHistory)

            AddHandler Me.DGridViewPrudential.RowsAdded, AddressOf Me.DGridViewPrudential_RowsAdded

            Me.EnableControls(True, False, False, False, False, True, False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
            Exit Sub
        End Try
    End Sub

    Private Sub btnInterest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInterest.Click
        Dim frm As New frmCollectionSearch
        Try
            With frm
                Dim valSize As New System.Drawing.Size
                valSize.Width = 343
                valSize.Height = 130

                .Size = valSize
                .LoadType = frmCollectionSearch.EnumFunctionType.InterestSearch

                If frm.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If

                Me._TransDate = CDate(FormatDateTime(frm.dtAllocationDate.Value, DateFormat.ShortDate))
            End With

            If Me.DGridViewPrudential.RowCount <> 0 Then
                Dim ans As MsgBoxResult
                ans = MsgBox("This will cancel the current transaction. Do you really want to input interest?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Interest")

                If ans = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            Me._TransactionType = EnumTransactionType.Interest

            Me._ListPRHistory = New List(Of PrudentialHistory)

            'Clear the DataGridView
            Me.DGridViewPrudential.Rows.Clear()

            'Get the Participants
            Me._ListParticipants = WBillHelper.GetAMParticipants()

            For Each item In Me._ListParticipants
                Me.DGridViewPrudential.Rows.Add(FormatDateTime(Me._TransDate, DateFormat.ShortDate), item.IDNumber, _
                                                item.ParticipantID, item.FullName, "0.00", "View DMCM", "0")

            Next

            Me.EnableControls(True, False, True, False, False, True, False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
            Exit Sub
        End Try
    End Sub

    Private Sub DGridViewPrudential_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs)
        Select Case Me._TransactionType
            Case EnumTransactionType.Interest, EnumTransactionType.Edit
                Me.DGridViewPrudential.Rows(e.RowIndex).Cells("colAmount").ReadOnly = False

            Case EnumTransactionType.Save, EnumTransactionType.Search, EnumTransactionType.PostToGP
                Me.DGridViewPrudential.Rows(e.RowIndex).Cells("colAmount").ReadOnly = True

        End Select
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Me._ListPRHistory = New List(Of PrudentialHistory)
            For index As Integer = 0 To Me.DGridViewPrudential.RowCount - 1
                Dim amount = CDec(Me.DGridViewPrudential.Rows(index).Cells("colAmount").Value)

                If amount <> 0 Then
                    Dim item As New PrudentialHistory
                    With item
                        .IDNumber = New AMParticipants(CStr(Me.DGridViewPrudential.Rows(index).Cells("colIDNumber").Value), _
                                                       CStr(Me.DGridViewPrudential.Rows(index).Cells("colParticipantID").Value), _
                                                       CStr(Me.DGridViewPrudential.Rows(index).Cells("colParticipantName").Value))
                        .BatchCode = EnumPostedType.PRI.ToString() & "-XXXX"
                        .TransDate = Me._TransDate
                        .TransType = EnumPrudentialTransType.InterestAmount
                        .Amount = CDec(Me.DGridViewPrudential.Rows(index).Cells("colAmount").Value)
                    End With
                    Me._ListPRHistory.Add(item)
                    Me._ListPRHistory.TrimExcess()
                End If
            Next

            If Me._ListPRHistory.Count = 0 Then
                MsgBox("Nothing to Save!", MsgBoxStyle.Critical, "No records")
                Exit Sub
            End If

            Dim ans = MsgBox("Do you really want to save this record/s temporarily?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            Me._TransactionType = EnumTransactionType.Save

            'Save records
            WBillHelper.SavePrudentialInterestTemporary(Me._TransDate, Me._ListPRHistory)

            MsgBox("Successfully Saved!", MsgBoxStyle.Information, "Save")

            For index As Integer = 0 To Me.DGridViewPrudential.RowCount - 1
                Me.DGridViewPrudential.Rows(index).Cells("colAmount").ReadOnly = True
            Next

            Me.EnableControls(True, True, False, True, True, True, True)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
            Exit Sub
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            For index As Integer = 0 To Me.DGridViewPrudential.RowCount - 1
                Me.DGridViewPrudential.Rows(index).Cells("colAmount").ReadOnly = False
            Next

            Me.EnableControls(True, False, True, False, False, True, False)

            Me._TransactionType = EnumTransactionType.Edit
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
            Exit Sub
        End Try
    End Sub

    Private Sub btnJV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJV.Click        
        Try
            If Me._ListPRHistory.Count = 0 Then
                MsgBox("Nothing to view!", MsgBoxStyle.Critical, "No record")
                Exit Sub
            End If

            Dim remarks As String = ""
            Do While remarks.Trim.Length = 0
                remarks = InputBox("", "Input the Remarks for Journal Voucher")
            Loop

            ProgressThread.Show("Please wait while preparing the report.")
            'Get the signatories for JV
            Dim itemSignatories = WBillHelper.GetSignatories("JV").First()

            'Get the accounting codes
            Dim listAccountingCodes = WBillHelper.GetAccountingCodes()

            Dim itemJV As New JournalVoucher
            Dim totalAmount = (From x In Me._ListPRHistory _
                               Select x.Amount).Sum()

            Dim jvDetails As New List(Of JournalVoucherDetails)

            jvDetails.Add(New JournalVoucherDetails(0, AMModule.CashinBankPrudentialCode, totalAmount, 0))
            jvDetails.Add(New JournalVoucherDetails(0, AMModule.InterestPayablePRCode, 0, totalAmount))
            jvDetails.TrimExcess()

            'For Journal Voucher
            With itemJV
                .Status = 1
                .ApprovedBy = itemSignatories.Signatory_2
                .CheckedBy = itemSignatories.Signatory_3
                .PostedType = EnumPostedType.PRI.ToString()
                .Remarks = remarks
                .JVDate = Me._TransDate
                .JVDetails = jvDetails
            End With

            'Generate dataset for Journal voucher
            Dim ds = BFactory.GenerateJournalVoucherReport(listAccountingCodes, itemJV, New DSReport.JournalVoucherDataTable, _
                                                           New DSReport.JournalVoucherDetailsDataTable, New DSReport.AccountingCodeDataTable)

            Dim frmViewer As New frmReportViewer()
            With frmViewer
                .LoadJournalVoucher(ds)
                ProgressThread.Close()
                .ShowDialog()
            End With

            Me._TransactionType = EnumTransactionType.JournalVoucher

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSearchInterest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchInterest.Click
        Dim frm As New frmCollectionSearch
        Try
            With frm
                Dim valSize As New System.Drawing.Size
                valSize.Width = 343
                valSize.Height = 130

                .Size = valSize
                .LoadType = frmCollectionSearch.EnumFunctionType.InterestSearch

                If frm.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If

                Me._TransDate = CDate(FormatDateTime(frm.dtAllocationDate.Value, DateFormat.ShortDate))
            End With

            Me._TransactionType = EnumTransactionType.Search

            Me.DGridViewPrudential.Rows.Clear()

            'Get the Participants
            Me._ListParticipants = WBillHelper.GetAMParticipants()

            'Get the data
            Me._ListPRHistory = WBillHelper.GetParticipantsPrudentialHistoryNotPosted(Me._TransDate, EnumPrudentialTransType.InterestAmount)

            If Me._ListPRHistory.Count = 0 Then
                MsgBox("No record found!", MsgBoxStyle.Information, "No data")
                Me.EnableControls(True, False, False, False, False, True, False)
                Exit Sub
            Else
                Me.EnableControls(True, True, False, True, True, True, True)
            End If

            Dim listItems = From x In Me._ListParticipants Group Join y In Me._ListPRHistory _
                             On x.IDNumber Equals y.IDNumber.IDNumber Into Group From y In Group.DefaultIfEmpty(New PrudentialHistory()) _
                             Select x.IDNumber, x.ParticipantID, x.FullName, y.Amount


            For Each item In listItems
                Me.DGridViewPrudential.Rows.Add(FormatDateTime(Me._TransDate, DateFormat.ShortDate), item.IDNumber, _
                                                item.ParticipantID, item.FullName, FormatNumber(item.Amount, 2), "View DMCM", "0")

            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
            Exit Sub
        End Try
    End Sub

    Private Sub btnPostToGP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPostToGP.Click
        Try
            Dim listDMCM As New List(Of DebitCreditMemo)
            Dim itemJV As New JournalVoucher
            Dim itemGP As New WESMBillGPPosted

            Dim remarks As String = ""
            Do While remarks.Trim.Length = 0
                remarks = InputBox("", "Input the Remarks for Journal Voucher")
            Loop

            'Generate Entries
            Me.GenerateInterestEntries(listDMCM, itemJV, itemGP, remarks)

            Dim ans As MsgBoxResult
            ans = MsgBox("Do you really want to post this record/s?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")

            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            Me._TransactionType = EnumTransactionType.PostToGP

            'Save
            WBillHelper.SavePrudentialInterest(Me._ListPRHistory, listDMCM, itemJV, itemGP, Me._TransDate)

            MsgBox("Successfully saved!", MsgBoxStyle.Information, "Save")

            Me.DGridViewPrudential.Rows.Clear()

            'Get the Participants
            Me._ListParticipants = WBillHelper.GetAMParticipants()

            Dim listItems = From x In Me._ListParticipants Group Join y In Me._ListPRHistory _
                             On x.IDNumber Equals y.IDNumber.IDNumber Into Group From y In Group.DefaultIfEmpty(New PrudentialHistory()) _
                             Select x.IDNumber, x.ParticipantID, x.FullName, y.Amount, y.DMCMNumber

            For Each item In listItems
                Me.DGridViewPrudential.Rows.Add(FormatDateTime(Me._TransDate, DateFormat.ShortDate), item.IDNumber, _
                                                item.ParticipantID, item.FullName, FormatNumber(item.Amount, 2), "View DMCM", item.DMCMNumber)

            Next

            Me.EnableControls(True, False, False, False, False, True, False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
            Exit Sub
        End Try
    End Sub

    Private Sub DGridViewPrudential_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridViewPrudential.CellContentClick
        If e.ColumnIndex = 5 Then
            If CDec(Me.DGridViewPrudential.CurrentRow.Cells("colAmount").Value) = 0 Then
                Exit Sub
            End If

            Try
                ProgressThread.Show("Please wait while preparing report.")
                'Get the Signatories
                Dim SignatoriesDMCM = WBillHelper.GetSignatories("DMCM").First()

                'Get the DMCM No
                Dim DMCMNo = CLng(Me.DGridViewPrudential.CurrentRow.Cells("colDMCMNo").Value)

                'Load the DMCM
                Dim itemDMCM As DebitCreditMemo

                If DMCMNo = 0 Then
                    'Get the Amount
                    Dim Amount = CDec(Me.DGridViewPrudential.CurrentRow.Cells("colAmount").Value)

                    'Get the Trans Date
                    Dim TransDate = CDate(Me.DGridViewPrudential.CurrentRow.Cells("colTransDate").Value)

                    'Get the ID Number
                    Dim IDNumber = CStr(Me.DGridViewPrudential.CurrentRow.Cells("colIDNumber").Value)

                    Dim listDMCMDetails As New List(Of DebitCreditMemoDetails)
                    listDMCMDetails.Add(New DebitCreditMemoDetails(DMCMNo, AMModule.CashinBankPrudentialCode, Amount, 0, _
                                                                   "", EnumSummaryType.INV, New AMParticipants(IDNumber)))
                    listDMCMDetails.Add(New DebitCreditMemoDetails(DMCMNo, AMModule.InterestPayablePRCode, 0, Amount, _
                                                                   "", EnumSummaryType.INV, New AMParticipants(IDNumber)))

                    itemDMCM = New DebitCreditMemo
                    With itemDMCM
                        .IDNumber = IDNumber
                        .ChargeType = EnumChargeType.E
                        .Particulars = "Prudential Interest"
                        .TotalAmountDue = Amount
                        .TransType = EnumDMCMTransactionType.PrudentialInterest
                        .VATExempt = Amount
                        .TotalAmountDue = Amount
                        .DMCMDetails = listDMCMDetails
                        .CheckedBy = SignatoriesDMCM.Position_2
                        .ApprovedBy = SignatoriesDMCM.Position_3
                        .UpdatedDate = TransDate
                    End With
                Else
                    Dim listDMCM = WBillHelper.GetDebitCreditMemoMain(DMCMNo)

                    If listDMCM.Count = 0 Then
                        MsgBox("DMCM No. " & DMCMNo.ToString() & " does not found or already cancelled!", MsgBoxStyle.Information, "No record")
                        Exit Sub
                    End If

                    itemDMCM = listDMCM.First()
                End If

                Dim listDMCMNo As New List(Of Long)
                Dim listDMCMMain As New List(Of DebitCreditMemo)


                'Get the Accounting Codes
                Dim listAccountCodes = WBillHelper.GetAccountingCodes()

                'Get the Participants
                Dim listParticipants = WBillHelper.GetAMParticipantsAll()

                'Get the Signatory
                Dim signatory = WBillHelper.GetSignatories("DMCM").First()

                'Get the DMCM
                listDMCMMain.Add(itemDMCM)

                'Get the WESM Bill Sales and Purchase
                Dim listWESMBillSalesAndPurchased As New List(Of WESMBillSalesAndPurchased)

                'Add the DMCM Number
                listDMCMNo.Add(itemDMCM.DMCMNumber)

                Dim dt = BFactory.GenerateDMCMReport1(listDMCMNo, New DSReport.DebitCreditMemoDataTable, listDMCMMain, _
                                                      listAccountCodes, listParticipants, signatory, listWESMBillSalesAndPurchased)

                Dim frmViewer As New frmReportViewer()
                With frmViewer
                    .LoadDebitCreditMemo(dt)
                    ProgressThread.Close()
                    .ShowDialog()
                End With

            Catch ex As Exception
                ProgressThread.Close()
                MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub DGridViewPrudential_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridViewPrudential.CellEndEdit
        If e.ColumnIndex = 4 Then
            If Not IsNumeric(Me.DGridViewPrudential.CurrentRow.Cells("colAmount").Value) Then
                MsgBox("Amount value must be numeric!", MsgBoxStyle.Critical, "Specify the amount")
                Me.DGridViewPrudential.CurrentRow.Cells("colAmount").Value = "0.00"
                Exit Sub

            ElseIf Not BFactory.CheckPrecisionAndScale(12, 2, Me.DGridViewPrudential.CurrentRow.Cells("colAmount").Value.ToString()) Then
                MsgBox("Amount value should have a maximum of 12 whole numbers and 2 decimal places!", MsgBoxStyle.Critical, "Specify the amount")
                Me.DGridViewPrudential.CurrentRow.Cells("colAmount").Value = "0.00"

            ElseIf CDec(Me.DGridViewPrudential.CurrentRow.Cells("colAmount").Value) < 0 Then
                MsgBox("Amount value must greater than zero!", MsgBoxStyle.Critical, "Specify the amount")
                Me.DGridViewPrudential.CurrentRow.Cells("colAmount").Value = "0.00"
                Exit Sub
            End If

            Me.DGridViewPrudential.CurrentRow.Cells("colAmount").Value = FormatNumber(Me.DGridViewPrudential.CurrentRow.Cells("colAmount").Value, 2)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.LoadRecord()
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.LoadRecord()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub btnSummaryReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSummaryReport.Click        
        Try
            ProgressThread.Show("Please wait while preparing Summary Report.")
            'Get the Signatories
            Dim Signatory = WBillHelper.GetSignatories("PR_SUMMARY").First()

            Dim dt As New DSReport.PRSummaryReportDataTable

            'Generate dataset for Journal voucher
            Dim ds = BFactory.GeneratePrudentialReport(Me._ListPRHistory, 0, Signatory, dt)

            Dim frmViewer As New frmReportViewer()
            With frmViewer
                .LoadPrudentialSummaryReport(dt, EnumPrudentialTransType.InterestAmount)
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Methods/Functions"

    Private Sub GenerateInterestEntries(ByRef listDMCM As List(Of DebitCreditMemo), _
                                        ByRef itemJV As JournalVoucher, ByVal itemGP As WESMBillGPPosted, _
                                        ByVal remarks As String)

        'Get the Signatories
        Dim SignatoriesJV = WBillHelper.GetSignatories("PI").First()

        'Get the Signatories
        Dim SignatoriesDMCM = WBillHelper.GetSignatories("DMCM").First()

        Dim totalInterest As Decimal = 0
        Dim DMCMNo As Long = 0

        For Each item In Me._ListPRHistory
            If item.Amount <> 0 Then
                'For DMCM 
                DMCMNo += 1

                'Add the total amount
                totalInterest += item.Amount

                Dim listDMCMDetails As New List(Of DebitCreditMemoDetails)
                listDMCMDetails.Add(New DebitCreditMemoDetails(DMCMNo, AMModule.CashinBankPrudentialCode, item.Amount, 0, _
                                                               "", EnumSummaryType.INV, New AMParticipants(item.IDNumber.IDNumber)))
                listDMCMDetails.Add(New DebitCreditMemoDetails(DMCMNo, AMModule.InterestPayablePRCode, 0, item.Amount, _
                                                               "", EnumSummaryType.INV, New AMParticipants(item.IDNumber.IDNumber)))

                Dim itemDMCM As New DebitCreditMemo
                With itemDMCM
                    .IDNumber = item.IDNumber.IDNumber
                    .ChargeType = EnumChargeType.E
                    .Particulars = "Prudential Interest"
                    .TotalAmountDue = item.Amount
                    .TransType = EnumDMCMTransactionType.PrudentialInterest
                    .VATExempt = item.Amount
                    .TotalAmountDue = item.Amount
                    .DMCMDetails = listDMCMDetails
                    .CheckedBy = SignatoriesDMCM.Signatory_1
                    .ApprovedBy = SignatoriesDMCM.Signatory_2
                    .UpdatedDate = Me._TransDate
                End With

                listDMCM.Add(itemDMCM)
                listDMCM.TrimExcess()
            End If
        Next

        Dim jvDetails As New List(Of JournalVoucherDetails)

        jvDetails.Add(New JournalVoucherDetails(0, AMModule.CashinBankPrudentialCode, totalInterest, 0))
        jvDetails.Add(New JournalVoucherDetails(0, AMModule.InterestPayablePRCode, 0, totalInterest))
        jvDetails.TrimExcess()

        'For Journal Voucher
        With itemJV
            .Status = 1
            .CheckedBy = SignatoriesJV.Signatory_1
            .ApprovedBy = SignatoriesJV.Signatory_2
            .PostedType = EnumPostedType.PRI.ToString()
            .JVDetails = jvDetails
            .JVDate = Me._TransDate
        End With

        'For WESM GP Posted
        With itemGP
            .PostType = EnumPostedType.PRI.ToString()
            .DocumentAmount = totalInterest
            .Remarks = remarks
        End With
    End Sub

    Private Sub EnableControls(ByVal valInterest As Boolean, ByVal valEdit As Boolean, ByVal valSave As Boolean, ByVal valJV As Boolean, _
                               ByVal valSummary As Boolean, ByVal valSearch As Boolean, ByVal valPostToGP As Boolean)
        Me.btnInterest.Enabled = valInterest
        Me.btnEdit.Enabled = valEdit
        Me.btnSave.Enabled = valSave
        Me.btnJV.Enabled = valJV
        Me.btnSummaryReport.Enabled = valSummary
        Me.btnSearch.Enabled = valSearch
        Me.btnPostToGP.Enabled = valPostToGP
    End Sub

    Private Sub LoadRecord()
        If Me.DGridViewPrudential.RowCount = 0 Then
            MsgBox("No current records!", MsgBoxStyle.Exclamation, "No data")
            Exit Sub
        End If

        If Me.txtSearch.Text.Trim.Length = 0 Then
            MsgBox("Specify first the participant id!", MsgBoxStyle.Critical, "Invalid")
            Me.txtSearch.Select()
            Exit Sub
        End If

        For Index As Integer = 0 To Me.DGridViewPrudential.RowCount - 1
            If CStr(Me.DGridViewPrudential.Rows(Index).Cells("colParticipantID").Value).ToUpper() = Me.txtSearch.Text.Trim.ToUpper Then
                Me.DGridViewPrudential.Rows(Index).Selected = True
                Me.DGridViewPrudential.CurrentCell = Me.DGridViewPrudential.Rows(Index).Cells("colAmount")
                Me.txtSearch.Text = ""
                Exit Sub
            End If
        Next

        MsgBox(Me.txtSearch.Text & " does not exist in grid!", MsgBoxStyle.Information, "No data")
    End Sub

#End Region

End Class