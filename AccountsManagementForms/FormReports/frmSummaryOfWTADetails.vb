Imports System.Threading
Imports System.Threading.Tasks
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmSummaryOfWTADetails
    Private _SOWTADHelper As New SummaryofWTADetailsHelper
    Private cts As CancellationTokenSource
    Private stopWatch As New Diagnostics.Stopwatch
    Private newProgress As New ProgressClass
    Private Sub frmSummaryOfWTADetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Me.ddlYear_cmb.Items.Clear()
        For Each item In _SOWTADHelper.ListofYear().OrderByDescending(Function(x) x)
            Me.ddlYear_cmb.Items.Add(item)
        Next
    End Sub
    Private Sub UpdateProgress(_ProgressMsg As ProgressClass)
        ToolStripStatusLabelCR.Text = _ProgressMsg.ProgressMsg
        ctrl_statusStrip.Refresh()
    End Sub

    Private Sub ddlYear_cmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear_cmb.SelectedIndexChanged
        Try
            If Me.ddlYear_cmb.SelectedIndex = -1 Then
                Exit Sub
            End If

            _SOWTADHelper.WTAListOfMPByYear(Me.ddlYear_cmb.Text)
            Dim listofParticipants As New List(Of String)
            For Each item In _SOWTADHelper.ListofSellerTransNoPerMP
                listofParticipants.Add(item.Key)
            Next
            For Each item In _SOWTADHelper.ListofBuyerTransNoPerMP
                listofParticipants.Add(item.Key)
            Next

            Me.chkLB_Participants.Items.Clear()
            For Each item In listofParticipants.Distinct.OrderBy(Function(x) x).Distinct.ToList()
                Me.chkLB_Participants.Items.Add(item)
            Next

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

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Async Sub btn_ExportToExcel_Click(sender As Object, e As EventArgs) Handles btn_ExportToExcel.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim TargetPath As String = ""
        Dim SelectedYear As String = Me.ddlYear_cmb.Text
        Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)
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

            Dim getTimeStart As New DateTime
            Dim getTimeEnd As New DateTime
            getTimeStart = DateTime.Now()

            cts = New CancellationTokenSource
            Me.Timer1.Start()
            Me.stopWatch.Start()

            newProgress = New ProgressClass With {.ProgressMsg = "Please wait while preparing."}
            UpdateProgress(newProgress)

            For Each ParticipantID In Me.chkLB_Participants.CheckedItems
                Await Task.Run(Sub()
                                   _SOWTADHelper.GenerateWTADetailsSummaryReport(TargetPath, ParticipantID, SelectedYear, progressIndicator, cts.Token)
                               End Sub)
            Next

            getTimeEnd = DateTime.Now()
            cts = Nothing

            MessageBox.Show("Generating Reports are successfully.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As OperationCanceledException
            newProgress = New ProgressClass With {.ProgressMsg = "Process has been canceled as requested."}
            UpdateProgress(newProgress)
            cts = Nothing
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            newProgress = New ProgressClass With {.ProgressMsg = "Process has been canceled due to error."}
            UpdateProgress(newProgress)
            cts = Nothing
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Timer1.Stop()
            Me.stopWatch.Stop()
            Me.stopWatch.Reset()
            Me.ToolStripStatusLabelCR.Text = "Ready..."
        End Try
    End Sub

End Class