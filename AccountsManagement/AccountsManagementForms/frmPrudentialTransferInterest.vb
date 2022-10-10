'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmPrudentialTransferInterest
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     November 14, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the Transfer of Prudential Interest
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
Imports WESMLib.Auth.Lib

Public Class frmPrudentialTransferInterest
    Private _ListParticipants As List(Of AMParticipants)
    Private _TransactionType As EnumTransactionType
    Private _ListPRHistory As List(Of PrudentialHistory)
    Private _TransDate As Date

    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory

    Private Enum EnumTransactionType
        TransferOfInterest
        Edit
        Save
        JournalVoucher
        SummaryReport
        Search
        PostToGP
    End Enum


    Private Sub frmPrudentialTransferInterest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        BFactory = BusinessFactory.GetInstance()

        Me._ListParticipants = New List(Of AMParticipants)
        Me._ListPRHistory = New List(Of PrudentialHistory)
        AddHandler Me.DGridViewPrudential.RowsAdded, AddressOf Me.DGridViewPrudential_RowsAdded

        Me.EnableControls(True, False, False, False, False, True, False)
    End Sub

    Private Sub DGridViewPrudential_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs)
        Select Case Me._TransactionType
            Case EnumTransactionType.TransferOfInterest, EnumTransactionType.Edit
                Me.DGridViewPrudential.Rows(e.RowIndex).Cells("colCheck").ReadOnly = False

            Case EnumTransactionType.Save, EnumTransactionType.Search, EnumTransactionType.PostToGP
                Me.DGridViewPrudential.Rows(e.RowIndex).Cells("colCheck").ReadOnly = True

        End Select
    End Sub

    Private Sub btnLoadInterest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadInterest.Click
        'Get the prudential
        Dim listPR = WBillHelper.GetParticipantsPrudential()

        Dim cnt = (From x In listPR _
                   Where x.InterestAmount <> 0 _
                   Select x).Count()

        If cnt = 0 Then
            MsgBox("No existing prudential interest!", MsgBoxStyle.Exclamation, "No Interest Amount")
            Exit Sub
        End If

        If Me.DGridViewPrudential.RowCount <> 0 Then
            Dim ans As MsgBoxResult
            ans = MsgBox("This will cancel the current transaction. Do you really want to load the prudential interest?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Replenish")

            If ans = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

        Me._ListPRHistory = New List(Of PrudentialHistory)

        Me._TransactionType = EnumTransactionType.TransferOfInterest

        'Clear the DataGridView
        Me.DGridViewPrudential.Rows.Clear()

        'Get the Participants
        Me._ListParticipants = WBillHelper.GetAMParticipantsAll()

        Dim listInterest = From x In listPR Join y In Me._ListParticipants _
                           On x.IDNumber Equals y.IDNumber _
                           Where x.InterestAmount <> 0 _
                           Select y.IDNumber, y.ParticipantID, y.FullName, x.InterestAmount

        For Each item In listInterest
            Me.DGridViewPrudential.Rows.Add(item.IDNumber, item.ParticipantID, item.FullName, FormatNumber(item.InterestAmount, 2))

        Next

        Me.EnableControls(True, False, True, False, False, True, False)
    End Sub

    Private Sub btnTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Try
            If Me.DGridViewPrudential.RowCount = 0 Then
                MsgBox("Nothing to transfer!", MsgBoxStyle.Critical, "No record")
                Exit Sub
            End If

            Dim chck As Boolean = False
            For index As Integer = 0 To Me.DGridViewPrudential.RowCount - 1
                If CBool(Me.DGridViewPrudential.Rows(index).Cells("colCheck").Value) Then
                    chck = True
                    Exit For
                End If
            Next

            If chck = False Then
                MsgBox("Please tick the interest to transfer!", MsgBoxStyle.Critical, "Specify the inputs")
                Exit Sub
            End If

            Dim ans = MsgBox("Do you really want to save this record/s temporarily?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            Dim frm As New frmCollectionSearch
            With frm
                Dim valSize As New System.Drawing.Size
                valSize.Width = 343
                valSize.Height = 130

                .Size = valSize
                .LoadType = frmCollectionSearch.EnumFunctionType.TransferOfInterestSearch

                If frm.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If

                Me._TransDate = CDate(FormatDateTime(frm.dtAllocationDate.Value, DateFormat.ShortDate))
            End With

            Me._ListPRHistory = New List(Of PrudentialHistory)
            For index As Integer = 0 To Me.DGridViewPrudential.RowCount - 1
                If CBool(Me.DGridViewPrudential.Rows(index).Cells("colCheck").Value) Then
                    Dim amount = CDec(Me.DGridViewPrudential.Rows(index).Cells("colAmount").Value)

                    Dim item As New PrudentialHistory
                    With item
                        .TransDate = Me._TransDate
                        .BatchCode = EnumPostedType.PRTI.ToString() & "-XXXX"
                        .IDNumber = New AMParticipants(CStr(Me.DGridViewPrudential.Rows(index).Cells("colIDNumber").Value), _
                                                       CStr(Me.DGridViewPrudential.Rows(index).Cells("colParticipantID").Value), _
                                                       CStr(Me.DGridViewPrudential.Rows(index).Cells("colParticipantName").Value))
                        .TransType = EnumPrudentialTransType.TransferInterestAmount
                        .Amount = CDec(Me.DGridViewPrudential.Rows(index).Cells("colAmount").Value)

                    End With
                    Me._ListPRHistory.Add(item)
                    Me._ListPRHistory.TrimExcess()

                    'Updated By Lance 08/19/2014
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.MonPrudentialTransIntWindow.ToString, "colIDNumber: " & CStr(Me.DGridViewPrudential.Rows(index).Cells("colIDNumber").Value), "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)
                    
                End If
            Next

            Me._TransactionType = EnumTransactionType.Save

            'Save records
            WBillHelper.SavePrudentialTransferInterestTemporary(Me._ListPRHistory, Me._TransDate)

            MsgBox("Successfully Saved!", MsgBoxStyle.Information, "Save")

            For index As Integer = 0 To Me.DGridViewPrudential.RowCount - 1
                Me.DGridViewPrudential.Rows(index).Cells("colCheck").ReadOnly = True
            Next

            Me.EnableControls(True, True, False, True, True, True, True)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error found")            
        End Try
    End Sub

    Private Sub btnSearchReplenishment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchReplenishment.Click
        Dim frm As New frmCollectionSearch
        With frm
            Dim valSize As New System.Drawing.Size
            valSize.Width = 343
            valSize.Height = 130

            .Size = valSize
            .LoadType = frmCollectionSearch.EnumFunctionType.ReplenishmentSearch

            If frm.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If

            Me._TransDate = CDate(FormatDateTime(frm.dtAllocationDate.Value, DateFormat.ShortDate))
        End With

        Me._TransactionType = EnumTransactionType.Search

        Me.DGridViewPrudential.Rows.Clear()

        'Get the prudential
        Dim listPR = WBillHelper.GetParticipantsPrudential()

        'Get the Participants
        Me._ListParticipants = WBillHelper.GetAMParticipantsAll()

        'Get the data
        Me._ListPRHistory = WBillHelper.GetParticipantsPrudentialHistoryNotPosted(Me._TransDate, EnumPrudentialTransType.TransferInterestAmount)

        If Me._ListPRHistory.Count = 0 Then
            MsgBox("No record found!", MsgBoxStyle.Information, "No data")
            Me.EnableControls(True, False, False, False, False, True, False)
            Exit Sub
        Else
            Me.EnableControls(True, True, False, True, True, True, True)
        End If

        Dim listInterest = From x In listPR Group Join y In Me._ListPRHistory _
                           On x.IDNumber Equals y.IDNumber.IDNumber _
                           Into Group From y In Group.DefaultIfEmpty(New PrudentialHistory()) _
                           Where x.InterestAmount <> 0 _
                           Select x.IDNumber, x.InterestAmount, y.Amount

        Dim listItems = From x In listInterest Join y In Me._ListParticipants _
                        On x.IDNumber Equals y.IDNumber _
                        Select x.IDNumber, y.ParticipantID, y.FullName, x.Amount, x.InterestAmount

        For Each item In listItems
            Me.DGridViewPrudential.Rows.Add(item.IDNumber, item.ParticipantID, item.FullName, _
                                            FormatNumber(If(item.Amount <> 0, item.Amount, item.InterestAmount), 2), _
                                            If(item.Amount <> 0, True, False))

        Next

    End Sub

    Private Sub btnPostToGP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPostToGP.Click
        Try
            Dim itemJV As New JournalVoucher
            Dim itemGP As New WESMBillGPPosted

            'Generate Entries
            Me.GenerateTransferInterestIntoPrudentialEntries(itemJV, itemGP)

            Dim ans As MsgBoxResult
            ans = MsgBox("Do you really want to post this record/s?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")

            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            Me._TransactionType = EnumTransactionType.PostToGP

            'Save
            WBillHelper.SavePrudentialTransferInterest(Me._ListPRHistory, itemJV, itemGP, Me._TransDate)

            MsgBox("Successfully saved!", MsgBoxStyle.Information, "Save")

            Me._ListPRHistory = New List(Of PrudentialHistory)
            Me.DGridViewPrudential.Rows.Clear()

            Me.EnableControls(True, False, False, False, False, True, False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error found")            
        End Try
    End Sub

    Private Sub btnJV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJV.Click        
        Try
            ProgressThread.Show("Please wait while preparing JV Report.")
            If Me._ListPRHistory.Count = 0 Then
                MsgBox("Nothing to view!", MsgBoxStyle.Critical, "No record")
                Exit Sub
            End If

            'Get the signatories for JV
            Dim itemSignatories = WBillHelper.GetSignatories("JV").First()

            'Get the accounting codes
            Dim listAccountingCodes = WBillHelper.GetAccountingCodes()

            Dim itemJV As New JournalVoucher
            Dim totalAmount = (From x In Me._ListPRHistory _
                               Select x.Amount).Sum()

            'For JV
            Dim jvDetails As New List(Of JournalVoucherDetails)
            jvDetails.Add(New JournalVoucherDetails(0, AMModule.InterestPayablePRCode, totalAmount, 0))
            jvDetails.Add(New JournalVoucherDetails(0, AMModule.PRWESMCode, 0, totalAmount))

            'For Journal Voucher
            With itemJV
                .Status = 1
                .ApprovedBy = itemSignatories.Signatory_2
                .CheckedBy = itemSignatories.Signatory_3
                .PostedType = EnumPostedType.PRTI.ToString()
                .Remarks = "Transfer of Prudential Interest"
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

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.LoadRecord()
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.LoadRecord()
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        For index As Integer = 0 To Me.DGridViewPrudential.RowCount - 1
            Me.DGridViewPrudential.Rows(index).Cells("colCheck").ReadOnly = False
        Next

        Me.EnableControls(True, False, True, False, False, True, False)

        Me._TransactionType = EnumTransactionType.Edit
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
                .LoadPrudentialSummaryReport(dt, EnumPrudentialTransType.TransferInterestAmount)
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Methods/Functions"

    Private Sub GenerateTransferInterestIntoPrudentialEntries(ByRef itemJV As JournalVoucher, ByRef itemGP As WESMBillGPPosted)

        'Get the Signatories
        Dim SignatoriesJV = WBillHelper.GetSignatories("PI").First()
        Dim totalInterest As Decimal = 0

        For Each item In Me._ListPRHistory
            totalInterest += item.Amount
        Next

        Dim jvDetails As New List(Of JournalVoucherDetails)

        jvDetails.Add(New JournalVoucherDetails(0, AMModule.InterestPayablePRCode, totalInterest, 0))
        jvDetails.Add(New JournalVoucherDetails(0, AMModule.PRWESMCode, 0, totalInterest))
        jvDetails.TrimExcess()

        'For Journal Voucher
        With itemJV
            .Status = 1
            .CheckedBy = SignatoriesJV.Signatory_1
            .ApprovedBy = SignatoriesJV.Signatory_2
            .PostedType = EnumPostedType.PRTI.ToString()
            .JVDetails = jvDetails
            .JVDate = Me._TransDate
        End With

        'For WESM GP Posted
        With itemGP
            .PostType = EnumPostedType.PRTI.ToString()
            .DocumentAmount = totalInterest
        End With
    End Sub

    Private Sub EnableControls(ByVal valInterest As Boolean, ByVal valEdit As Boolean, ByVal valTransfer As Boolean, ByVal valJV As Boolean, _
                               ByVal valSummary As Boolean, ByVal valSearch As Boolean, ByVal valPostToGP As Boolean)
        Me.btnLoadInterest.Enabled = valInterest
        Me.btnEdit.Enabled = valEdit
        Me.btnTransfer.Enabled = valTransfer
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