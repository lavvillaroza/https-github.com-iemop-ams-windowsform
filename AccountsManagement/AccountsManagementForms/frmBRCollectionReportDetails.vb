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

Public Class frmBRCollectionReportDetails
    Public _BRCollReportHelper As BRCollectionReportHelper
    Private cts As CancellationTokenSource
    Private Sub frmBRCollectionReportDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.rbDaily.Checked = True
        FillUpCheckBoxList()
    End Sub

    Private Sub FillUpCheckBoxList()
        Me.chkLB_Participants.Items.Clear()
        For Each item In _BRCollReportHelper.BIRRulingCR.SellerDetails
            Me.chkLB_Participants.Items.Add(item.Participant.IDNumber)
        Next
    End Sub
    Private Sub UpdateProgress(_ProgressMsg As ProgressClass)
        ToolStripStatusLabelCR.Text = _ProgressMsg.ProgressMsg
        ctrl_statusStrip.Refresh()
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
    Private Sub btn_GeneratePDF_Click(sender As Object, e As EventArgs) Handles btn_GeneratePDF.Click
        Try
            Me.TableLayoutPanel_Main.Enabled = False
            Dim _fldrSelect As New FolderBrowserDialog
            Dim ans As New MsgBoxResult
            Dim fPathName As String = ""
            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)

            ans = MsgBox("Do you really want to export the records as pdf Format?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Export records")
            If ans <> MsgBoxResult.Yes Then
                Exit Sub
            End If
            With _fldrSelect
                .ShowDialog()
                If .SelectedPath = "" Then
                    Exit Sub
                End If

                ProgressThread.Show("Please wait while preparing the file.")

                fPathName = .SelectedPath

                'Get the selected participants
                For cnt As Integer = 0 To Me.chkLB_Participants.CheckedItems.Count - 1
                    Dim selectedParticipant As String = Me.chkLB_Participants.CheckedItems(cnt).ToString()
                    Dim dataSource As New DataTable

                    If Me.rbDaily.Checked = True Then
                        dataSource = _BRCollReportHelper.GenerateCollectionReportDatatableDaily(New DSReport.BIRRulingMainDataTable, selectedParticipant)
                    Else
                        dataSource = _BRCollReportHelper.GenerateCollectionReportDatatableMonthly(New DSReport.BIRRulingMainDataTable, selectedParticipant)
                    End If

                    Dim getCRNumber As String = (From x In dataSource.AsEnumerable() Select x.Field(Of String)("CR_NUMBER") Distinct).First()

                    Dim expReport = New RPTBIRCollectionReportSummary
                    expReport.SetDataSource(dataSource)
                    expReport.ExportToDisk(ExportFormatType.PortableDocFormat, fPathName & "\" &
                                           "CSR_IEMOP_" & selectedParticipant.Replace("_FIT", "").ToString & "_" & getCRNumber & "_" & _BRCollReportHelper.BIRRulingCR.RemittanceDateTo.ToString("yyyyMMdd") & ".pdf")
                    expReport.Close()
                    expReport.Dispose()
                Next

                ProgressThread.Close()
                MsgBox("Successfully downloaded!", MsgBoxStyle.Information, "Downloaded")
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.TableLayoutPanel_Main.Enabled = True
        End Try
    End Sub

    Private Sub btnShowReport_Click(sender As Object, e As EventArgs) Handles btnShowReport.Click
        Try
            Dim listOfSelectedParticipant As New List(Of String)
            ProgressThread.Show("Please wait while loading.")
            'Get the selected participants
            For cnt As Integer = 0 To Me.chkLB_Participants.CheckedItems.Count - 1
                listOfSelectedParticipant.Add(Me.chkLB_Participants.CheckedItems(cnt).ToString())
            Next
            Dim collReporType As String = ""

            If Me.rbDaily.Checked = True Then
                collReporType = "DAILY"
            Else
                collReporType = "MONTHLY"
            End If
            ProgressThread.Close()
            Dim rptView As New frmReportViewer
            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)

            With rptView
                If collReporType.Equals("DAILY") Then
                    .LoadBIRCollectionReport(_BRCollReportHelper.GenerateCollectionReportDatatableDaily(New DSReport.BIRRulingMainDataTable, listOfSelectedParticipant), collReporType)
                ElseIf collReporType.Equals("MONTHLY") Then
                    .LoadBIRCollectionReport(_BRCollReportHelper.GenerateCollectionReportDatatableMonthly(New DSReport.BIRRulingMainDataTable, listOfSelectedParticipant), collReporType)
                End If
                .ShowDialog()
            End With

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub btn_ExportToExcel_Click(sender As Object, e As EventArgs) Handles btn_ExportToExcel.Click
        Try
            TableLayoutPanel_Main.Enabled = False
            Dim _fldrSelect As New FolderBrowserDialog
            Dim ans As New MsgBoxResult
            Dim fPathName As String = ""
            Dim shortCounter As Integer = 0
            Dim collReporType As String = ""

            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)
            cts = New CancellationTokenSource

            If Me.rbDaily.Checked = True Then
                collReporType = "DAILY"
            Else
                collReporType = "MONTHLY"
            End If

            If Me.chkLB_Participants.Items.Count = Me.chkLB_Participants.CheckedItems.Count Then
                ans = MsgBox("Do you really want to export the Collection Report per participant?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Export records in Excel")
                If ans = MsgBoxResult.Yes Then
                    With _fldrSelect
                        .ShowDialog()
                        If .SelectedPath = "" Then
                            Exit Sub
                        End If
                        ProgressThread.Show("Please wait while loading.")
                        fPathName = .SelectedPath
                        'Get the selected participants
                        For cnt As Integer = 0 To Me.chkLB_Participants.CheckedItems.Count - 1
                            Dim selectedParticipant As String = Me.chkLB_Participants.CheckedItems(cnt).ToString()
                            Await Task.Run(Sub() _BRCollReportHelper.GenerateBIRCollectionReportExcel(selectedParticipant, fPathName, False, collReporType, progressIndicator))
                            shortCounter += 1
                        Next
                        ProgressThread.Close()
                        If shortCounter = 0 Then
                            MessageBox.Show("No selected participant/s! Please select first.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                        MessageBox.Show("Successfully downloaded!", "Downloaded", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End With
                Else
                    With _fldrSelect
                        .ShowDialog()
                        If .SelectedPath = "" Then
                            Exit Sub
                        End If
                        ProgressThread.Show("Please wait while loading.")
                        fPathName = .SelectedPath
                        Await Task.Run(Sub() _BRCollReportHelper.GenerateBIRCollectionReportExcel("", fPathName, True, collReporType, progressIndicator))
                        ProgressThread.Close()
                        MessageBox.Show("Successfully downloaded!", "Downloaded", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End With
                End If
            ElseIf Me.chkLB_Participants.CheckedItems.Count > 0 Then
                With _fldrSelect
                    .ShowDialog()
                    If .SelectedPath = "" Then
                        Exit Sub
                    End If
                    ProgressThread.Show("Please wait while loading.")
                    fPathName = .SelectedPath
                    'Get the selected participants
                    For cnt As Integer = 0 To Me.chkLB_Participants.CheckedItems.Count - 1
                        Dim selectedParticipant As String = Me.chkLB_Participants.CheckedItems(cnt).ToString()
                        Await Task.Run(Sub() _BRCollReportHelper.GenerateBIRCollectionReportExcel(selectedParticipant, fPathName, False, collReporType, progressIndicator))
                    Next
                    ProgressThread.Close()
                    MessageBox.Show("Successfully downloaded!", "Downloaded", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End With
            Else
                ProgressThread.Close()
                MessageBox.Show("No selected participant/s! Please select first.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            cts = Nothing
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            TableLayoutPanel_Main.Enabled = True
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If cts IsNot Nothing Then
            If MessageBox.Show("Are you sure to cancel the current process?", "Close",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                cts.Cancel()
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub frmBRCollectionReportDetails_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
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
End Class