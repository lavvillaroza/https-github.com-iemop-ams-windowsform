Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports System.Threading
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib


Public Class frmBIRAccessToRecord
    Private _BIRAccessHelper As BIRAccessToRecordHelper

    Private Sub frmBIRAccessToRecord_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub cmd_Close_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Async Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim dateFrom As Date = dtp_TransFrom.Value()
        Dim dateTo As Date = dtp_TransTo.Value()
        Dim getStartDate As Date = New Date(dateFrom.Year, dateFrom.Month, 1)
        Dim getEndDate As Date = New Date(dateTo.Year, dateTo.Month, DateTime.DaysInMonth(dateTo.Year, dateTo.Month))
        Try
            TableLayoutPanel_Main.Enabled = False
            ToolStripStatusLabelCR.Text = "Please wait while fetching data!"
            ctrl_statusStrip.Refresh()
            ProgressThread.Show("Loading...")
            _BIRAccessHelper = New BIRAccessToRecordHelper
            Dim listofParticipants As List(Of AMParticipants) = Await _BIRAccessHelper.GetTPByDateRange(getStartDate, getEndDate)
            Me.chkLB_Participants.Items.Clear()
            For Each item In listofParticipants
                Me.chkLB_Participants.Items.Add(item.IDNumber)
            Next
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            TableLayoutPanel_Main.Enabled = True
            ToolStripStatusLabelCR.Text = "Ready ..."
        End Try
    End Sub

    Private Sub btn_ExportToExcel_Click(sender As Object, e As EventArgs) Handles btn_ExportToExcel.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim TargetPath As String = ""
        Dim dateFrom As Date = dtp_TransFrom.Value()
        Dim dateTo As Date = dtp_TransTo.Value()
        Dim getStartDate As Date = New Date(dateFrom.Year, dateFrom.Month, 1)
        Dim getEndDate As Date = New Date(dateTo.Year, dateTo.Month, DateTime.DaysInMonth(dateTo.Year, dateTo.Month))
        Try
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
            ProgressThread.Show("Please wait while processing.")
            TableLayoutPanel_Main.Enabled = False
            ToolStripStatusLabelCR.Text = "Please wait while extracting data!"
            ctrl_statusStrip.Refresh()
            For Each participantID In Me.chkLB_Participants.CheckedItems
                ToolStripStatusLabelCR.Text = "Generating BIR Access To Record report for " & participantID
                ctrl_statusStrip.Refresh()
                _BIRAccessHelper.GenerateBIRAcessToRecordReport(TargetPath, participantID, getStartDate, getEndDate)
            Next
            If _BIRAccessHelper.GetReportCreatedCount = 0 Then
                ProgressThread.Close()
                MessageBox.Show("No generated report.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ProgressThread.Close()
                MessageBox.Show("Generating Reports are successfully.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            TableLayoutPanel_Main.Enabled = True
            ToolStripStatusLabelCR.Text = "Ready ..."
        End Try
    End Sub
End Class