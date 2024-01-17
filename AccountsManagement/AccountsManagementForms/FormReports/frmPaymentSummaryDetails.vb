﻿Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementLogic

Public Class frmPaymentSummaryDetails
    Private WbillHelper As WESMBillHelper
    Private _PSDHelper As New PaymentSummaryDetailsHelper

    Private Sub frmPaymentSummaryDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        WbillHelper = WESMBillHelper.GetInstance
        WbillHelper.ConnectionString = AMModule.ConnectionString
        WbillHelper.UserName = AMModule.UserName

        ToolStripProgressBar_STLNotice.Visible = False
        ToolStripStatusLabel_Text.Visible = False
        ToolStripStatusLabel_Percent.Visible = False

        Dim PSDDate As List(Of Date) = Me.WbillHelper.GetPaymentDetailsDates()

        If PSDDate.Count = 0 Then
            MessageBox.Show("No Payment Summary Details was found.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        For Each DateItem In PSDDate
            Me.cbo_date.Items.Add(DateItem)
        Next
    End Sub

    Private Sub cbo_date_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_date.SelectedIndexChanged
        Try
            If Me.cbo_date.SelectedIndex = -1 Then
                Exit Sub
            Else
                If Me.chkbox_SelectAll.Checked = True Then
                    Me.chkbox_SelectAll.Checked = False
                End If

                Me.chkLB_Participants.Items.Clear()
                Dim ListofAMParticipants As List(Of String) = Me.WbillHelper.GetPDAMParticipants(CDate(cbo_date.SelectedItem.ToString)) 'Populate ListBox with Participant ID

                For Each item In ListofAMParticipants
                    Me.chkLB_Participants.Items.Add(item)
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub chkbox_SelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkbox_SelectAll.CheckedChanged
        For i As Integer = 0 To Me.chkLB_Participants.Items.Count - 1
            If chkbox_SelectAll.Checked = True Then
                Me.chkLB_Participants.SetItemChecked(i, True)
            Else
                Me.chkLB_Participants.SetItemChecked(i, False)
            End If
        Next
    End Sub

    Private Sub chk_Note_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Note.CheckedChanged
        If Me.chk_Note.Checked Then
            Me.txt_Note.ReadOnly = False
        Else
            Me.txt_Note.ReadOnly = True
        End If
    End Sub

    Private Sub btn_ExportToExcel_Click(sender As Object, e As EventArgs) Handles btn_ExportToExcel.Click

        Dim sFolderDialog As New FolderBrowserDialog
        Dim TargetPath As String = ""
        If chkLB_Participants.CheckedItems.Count = 0 Then
            MessageBox.Show("No participants selected", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        With sFolderDialog
            .ShowDialog()
            If .SelectedPath.ToString.Trim.Length = 0 Then
                Exit Sub
            Else
                TargetPath = sFolderDialog.SelectedPath
            End If
        End With
        Me.ToolStripProgressBar_STLNotice.Visible = True
        Me.ToolStripStatusLabel_Text.Visible = True
        Me.ToolStripStatusLabel_Percent.Visible = True
        Me.TableLayoutPanel_Main.Enabled = False

        Me.ToolStripStatusLabel_Text.Text = "Please wait while processing..."
        Try
            _PSDHelper.PSDDate = CDate(Me.cbo_date.Text)
            _PSDHelper.FilePath = TargetPath
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            bgw_PaymentSummaryDetails.WorkerSupportsCancellation = True
            bgw_PaymentSummaryDetails.WorkerReportsProgress = True
            bgw_PaymentSummaryDetails.RunWorkerAsync()
        End Try
    End Sub

    Private Sub bgw_PaymentSummaryDetails_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgw_PaymentSummaryDetails.DoWork
        Try
            If Me.chk_Note.Checked Then
                _PSDHelper.PaymentRemarks = Me.txt_Note.Text
            End If
            Dim i As Integer = 0
            Dim j As Integer = Me.chkLB_Participants.CheckedItems.Count
            For Each item In Me.chkLB_Participants.CheckedItems
                i += 1
                _PSDHelper.GeneratePSDReport(item.ToString)
                bgw_PaymentSummaryDetails.ReportProgress(CInt((i / j) * 100), "Running...")
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            e.Cancel = True
        End Try
    End Sub

    Private Sub bgw_PaymentSummaryDetails_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgw_PaymentSummaryDetails.ProgressChanged
        Try
            Me.ToolStripProgressBar_STLNotice.Value = CType(e.ProgressPercentage, Integer)
            Me.ToolStripStatusLabel_Percent.Text = e.ProgressPercentage.ToString("0.00") & " %"
            Me.ToolStripStatusLabel_Text.Text = _PSDHelper.ProcessRemarks.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub bgw_STLNotice_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw_PaymentSummaryDetails.RunWorkerCompleted
        Try
            If e.Cancelled = True Then
                Me.ToolStripStatusLabel_Text.Text = "Process cancelled due to error!"
                Me.ToolStripProgressBar_STLNotice.Visible = False
                Me.ToolStripStatusLabel_Text.Visible = False
                Me.ToolStripStatusLabel_Percent.Visible = False
                Me.TableLayoutPanel_Main.Enabled = True
            ElseIf e.Error IsNot Nothing Then
                MessageBox.Show(e.Error.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                Me.ToolStripStatusLabel_Text.Text = "Done..."
                If _PSDHelper.FileCounter = 0 Then
                    MessageBox.Show("No generated file", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Generated file/s successfully", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Me.ToolStripProgressBar_STLNotice.Visible = False
                Me.ToolStripStatusLabel_Text.Visible = False
                Me.ToolStripStatusLabel_Percent.Visible = False
                Me.TableLayoutPanel_Main.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class


