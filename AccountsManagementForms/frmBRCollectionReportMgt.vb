Option Explicit On
Option Strict On
Imports System
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports System.Threading
Imports System.Threading.Tasks
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports CrystalDecisions.Shared
Imports WESMLib.Auth.Lib
Public Class frmBRCollectionReportMgt
    Private WBillHelper As WESMBillHelper
    Private BRCollReportHelper As BRCollectionReportHelper
    Private listOfBRCollectionReport As List(Of BRCollectionReport)
    Private cts As CancellationTokenSource
    Private Sub frmBRCollectionReportMgt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        WBillHelper = WESMBillHelper.GetInstance()
        BRCollReportHelper = New BRCollectionReportHelper()
        PutDataInDisplayGrid()
    End Sub

    Private Sub PutDataInDisplayGrid()
        Try
            Me.DGVBRCollectionReport.Rows.Clear()
            For Each item In BRCollReportHelper.GetListOfBRCollectionReport()
                With DGVBRCollectionReport
                    .Rows.Add(item.BIRRNo.ToString("d7"), item.RemittanceDateFrom.ToString("MM/dd/yyyy"),
                              item.RemittanceDateTo.ToString("MM/dd/yyyy"),
                              item.GeneratedBy, item.GeneratedDate.ToString("MM/dd/yyyy h:mm"))
                End With
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'Selection of allocation date
        Dim frm As New frmBRCollectionReportAdd
        With frm
            If frm.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If
        End With

        Dim getRemittanceDateFrom As Date = CDate(FormatDateTime(frm.dtFrom.Value, DateFormat.ShortDate))
        Dim getRemittanceDateTo As Date = CDate(FormatDateTime(frm.dtTo.Value, DateFormat.ShortDate))
        'Dim getDueDateSelected As Date = CDate(FormatDateTime(CDate(frm.cbo_date.SelectedItem), DateFormat.ShortDate))
        Try
            MainPanel.Enabled = False
            Dim msgAns As New MsgBoxResult
            msgAns = MsgBox("Do you really want to generate and save the New Collection Report?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")
            If msgAns = MsgBoxResult.Yes Then
                If BRCollReportHelper.CheckTransactionDateRange(getRemittanceDateFrom, getRemittanceDateTo) = True Then
                    MessageBox.Show("The selected dates are already exist! Please try again.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
                Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)
                cts = New CancellationTokenSource
                ProgressThread.Show("Please wait while processing.")
                Await Task.Run(Sub() BRCollReportHelper.GenerateBRCollectionReport(getRemittanceDateFrom, getRemittanceDateTo, progressIndicator, cts.Token))

                Dim getNewCreatedBRCollectionReport As BRCollectionReport = BRCollReportHelper.BIRRulingCR
                With DGVBRCollectionReport
                    .Rows.Add(getNewCreatedBRCollectionReport.BIRRNo.ToString("d7"), getNewCreatedBRCollectionReport.RemittanceDateFrom.ToString("MM/dd/yyyy"),
                              getNewCreatedBRCollectionReport.RemittanceDateTo.ToString("MM/dd/yyyy"), getNewCreatedBRCollectionReport.GeneratedBy.ToString,
                              getNewCreatedBRCollectionReport.GeneratedDate.ToString("MM/dd/yyyy h:mm"))
                    'Scroll to the last row.
                    .FirstDisplayedScrollingRowIndex = .RowCount - 1

                    'Select the last row.
                    .Rows(.RowCount - 1).Selected = True
                End With
                ProgressThread.Close()
                MessageBox.Show("Successfully created! Please select view button to print or export.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ToolStripStatusLabelCR.Text = "Ready"
                cts = Nothing
            End If
        Catch ex As Exception
            cts = Nothing
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            MainPanel.Enabled = True
        End Try
    End Sub

    Private Sub UpdateProgress(_ProgressMsg As ProgressClass)
        ToolStripStatusLabelCR.Text = _ProgressMsg.ProgressMsg
        ctrl_statusStrip.Refresh()
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnViewBySeller.Click
        Try
            If Me.DGVBRCollectionReport.Rows.Count = 0 Then
                MessageBox.Show("No available data!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.DGVBRCollectionReport.CurrentRow.Index = -1 Then
                MessageBox.Show("No selected data!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            ProgressThread.Show("Please wait while loading...")
            Dim i As Integer = Me.DGVBRCollectionReport.CurrentRow.Index

            Dim _BIRRNo As Long = CLng(Me.DGVBRCollectionReport.Rows(i).Cells(0).Value)
            BRCollReportHelper.SetBIRRulingData(_BIRRNo)
            Dim frmBRCRDetails As New frmBRCollectionReportDetails
            With frmBRCRDetails
                ._BRCollReportHelper = BRCollReportHelper
                .forSellerOrBuyer = EnumCollectionReportType.Seller
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmBRCollectionReportMgt_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If cts IsNot Nothing Then
            ProgressThread.Close()
            If MessageBox.Show("Are you sure to cancel this process?", "Close",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                cts.Cancel()
                e.Cancel = True
                MessageBox.Show("Please try to close again!", "Close", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ProgressThread.Show("Please wait while continuing the process.")
                e.Cancel = True
                Me.Show()
            End If
        End If
    End Sub

    Private Sub btnViewByBuyer_Click(sender As Object, e As EventArgs) Handles btnViewByBuyer.Click
        Try
            If Me.DGVBRCollectionReport.Rows.Count = 0 Then
                MessageBox.Show("No available data!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.DGVBRCollectionReport.CurrentRow.Index = -1 Then
                MessageBox.Show("No selected data!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            ProgressThread.Show("Please wait while loading...")
            Dim i As Integer = Me.DGVBRCollectionReport.CurrentRow.Index

            Dim _BIRRNo As Long = CLng(Me.DGVBRCollectionReport.Rows(i).Cells(0).Value)
            BRCollReportHelper.SetBIRRulingData(_BIRRNo)
            BRCollReportHelper.SetBIRRulingDataForBuyers()

            Dim frmBRCRDetails As New frmBRCollectionReportDetails
            With frmBRCRDetails
                ._BRCollReportHelper = BRCollReportHelper
                .forSellerOrBuyer = EnumCollectionReportType.Buyer
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class