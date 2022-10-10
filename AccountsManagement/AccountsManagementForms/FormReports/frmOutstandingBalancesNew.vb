
Option Explicit On
Option Strict On

Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports System.IO
Imports System.Windows.Forms

Public Class frmOutstandingBalancesNew
    Private SummofOBHelper As New SummaryofOutstandingBalancesHelper
    Private WBillHelper As WESMBillHelper
    Private Sub frmOutstandingBalancesNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try            
            Me.MdiParent = MainForm
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString

            For Each DateItem In SummofOBHelper.AMDueDateList
                Me.ddl_DueDateList.Items.Add(DateItem)
            Next
        Catch ex As Exception            
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")        
        End Try
    End Sub

    Private Sub cmd_browse_Click(sender As Object, e As EventArgs) Handles cmd_browse.Click
        Dim sfDialog As New FolderBrowserDialog
        With sfDialog
            .ShowNewFolderButton = True
            '.RootFolder = Environment.SpecialFolder.MyComputer
            .ShowDialog()
            Me.txt_DestFile.Text = .SelectedPath
        End With
    End Sub

    Private Sub cmd_GenerateReportToExcel_Click(sender As Object, e As EventArgs) Handles cmd_GenerateReportToExcel.Click
        Dim FileName As String = Me.txt_DestFile.Text
        Dim SelectedDate As Date = CDate(Me.ddl_DueDateList.SelectedItem)
        Dim CheckBool As Boolean = If(Me.cbSelectAll.Checked = True, True, False)
        Try
            If FileName.Length <> 0 Then
                ProgressThread.Show("Please wait while exporting to '" & FileName & "'.")
                Me.SummofOBHelper.CreateOutstandingSummaryReport(FileName, SelectedDate, CheckBool)
                ProgressThread.Close()
                MessageBox.Show("Generated file/s successfully", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Please select folder.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ddl_DueDateList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_DueDateList.SelectedIndexChanged
        ProgressThread.Show("Please wait while fetching...")
        If Me.ddl_DueDateList.SelectedIndex = -1 Then
            Exit Sub
        End If
        Try
            Dim selectedDueDate As Date = CDate(Me.ddl_DueDateList.SelectedItem)
            Me.SummofOBHelper.InitializedBasedOnSelectedDate(CDate(selectedDueDate.ToShortDateString), False)
            ProgressThread.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ProgressThread.Close()
        End Try
    End Sub


    Private Sub cbSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbSelectAll.CheckedChanged
        Try
            If Me.cbSelectAll.Checked = False Then
                Me.ddl_DueDateList.Enabled = True
                Me.SummofOBHelper.ResetWESMBillSummaryList()
            Else
                Me.ddl_DueDateList.Enabled = False
                Me.ddl_DueDateList.SelectedIndex = -1
                ProgressThread.Close()
                ProgressThread.Show("Please wait while fetching...")
                Me.SummofOBHelper.InitializedBasedOnSelectedDate(New Date, True)
                ProgressThread.Close()
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try        
    End Sub

    Private Sub cmd_close_Click(sender As Object, e As EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub
End Class