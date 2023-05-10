Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports System.Windows.Forms.VisualStyles
Imports System.Threading.Tasks
Imports System.Threading

Public Class frmWHTaxCertificateSTLMgt
    Private _WHTaxCertSTLHelper As New WHTaxCertificateSTLHelper
    Private cts As CancellationTokenSource
    Private stopWatch As New Diagnostics.Stopwatch
    Private newProgress As New ProgressClass
    Private Sub frmWHTaxCertificateCollection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Me.DGridViewCollection.Rows.Clear()
    End Sub

    Private Sub UpdateProgress(_ProgressMsg As ProgressClass)
        tsslbl_Msg.Text = _ProgressMsg.ProgressMsg
        ctrl_statusStrip.Refresh()
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try

            newProgress = New ProgressClass With {.ProgressMsg = "Adding New Certificate."}
            UpdateProgress(newProgress)

            Dim frmWTCertColTag As New frmWHTaxCertificateSTLDetails
            With frmWTCertColTag
                ._WHTaxCertSTLHelper = _WHTaxCertSTLHelper
                .TabControl1.TabPages(1).Hide()
                .ShowDialog()
            End With

            If _WHTaxCertSTLHelper.NewWHTaxCertSTL Is Nothing Then
                MessageBox.Show("No added data!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If _WHTaxCertSTLHelper.NewWHTaxCertSTL.CertificateNo = 0 Then
                MessageBox.Show("Data is not save!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            With DGridViewCollection
                .Rows.Add(_WHTaxCertSTLHelper.NewWHTaxCertSTL.CertificateNo.ToString("d7"),
                          _WHTaxCertSTLHelper.NewWHTaxCertSTL.RemittanceDate.ToString("MM/dd/yyyy"),
                          _WHTaxCertSTLHelper.NewWHTaxCertSTL.BillingIDNumber.IDNumber,
                          FormatNumber(_WHTaxCertSTLHelper.NewWHTaxCertSTL.CollectedAmount.ToString("0.00"), UseParensForNegativeNumbers:=TriState.True))
            End With
            _WHTaxCertSTLHelper.NewWHTaxCertSTL = New WHTaxCertificateSTL
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            newProgress = New ProgressClass With {.ProgressMsg = "Ready"}
            UpdateProgress(newProgress)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            MainPanel.Enabled = False
            newProgress = New ProgressClass With {.ProgressMsg = "Please wait while searching..."}
            UpdateProgress(newProgress)
            Dim dateFrom As Date = CDate(FormatDateTime(Me.dtFrom.Value, DateFormat.ShortDate))
            Dim dateTo As Date = CDate(FormatDateTime(Me.dtTo.Value, DateFormat.ShortDate))
            Dim notAllocated As Boolean = CBool(chkbox_Allocated.Checked)
            Dim notUntagged As Boolean = CBool(chkbox_Untagged.Checked)

            _WHTaxCertSTLHelper.SearchByDateRangeForWTCertCollection(dateFrom, dateTo, notAllocated, notUntagged)
            If _WHTaxCertSTLHelper.ViewListOfWHTCertSTL.Count <> 0 Then
                PutDataInDisplayGrid(_WHTaxCertSTLHelper.ViewListOfWHTCertSTL)
            Else
                MessageBox.Show("No available data found!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            chkbox_SelectAll.Checked = False
        Catch ex As Exception
            MainPanel.Enabled = True
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            MainPanel.Enabled = True
            newProgress = New ProgressClass With {.ProgressMsg = "Ready"}
            UpdateProgress(newProgress)
        End Try
    End Sub

    Private Sub PutDataInDisplayGrid(ByVal whTaxCertifCollectionList As List(Of WHTaxCertificateSTL))
        DGridViewCollection.Rows.Clear()
        For Each item In whTaxCertifCollectionList
            With DGridViewCollection
                .Rows.Add(item.CertificateNo.ToString("d7"), item.RemittanceDate.ToString("MM/dd/yyyy"), item.BillingIDNumber.IDNumber,
                          FormatNumber(item.CollectedAmount.ToString("0.00"), UseParensForNegativeNumbers:=TriState.True),
                          If(item.AllocatedToAP.ToUpper.Equals("YES"), True, False), If(item.UntagEWT.ToUpper.Equals("YES"), True, False))
            End With
        Next
        Me.PutColorRedInAllocatedToAPRow()
    End Sub

    Private Sub PutColorRedInAllocatedToAPRow()
        For Each row As DataGridViewRow In DGridViewCollection.Rows
            If CBool(row.Cells("colAllocatedToAP").Value) = True Or CBool(row.Cells("colUntag").Value) = True Then
                row.DefaultCellStyle.ForeColor = Drawing.Color.Red
                row.ReadOnly = True
            Else
                row.DefaultCellStyle.ForeColor = Drawing.Color.Black
                row.Cells("colUntag").ReadOnly = True
            End If
        Next
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try

            If Me.DGridViewCollection.Rows.Count = 0 Then
                MessageBox.Show("No available data!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.DGridViewCollection.CurrentRow.Index = -1 Then
                MessageBox.Show("No selected data!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim i As Integer = Me.DGridViewCollection.CurrentRow.Index
            Dim certifNo As Long = CLng(Me.DGridViewCollection.Rows(i).Cells(0).Value)

            newProgress = New ProgressClass With {.ProgressMsg = "Viewing CertificateNo:" & certifNo.ToString("N0")}
            UpdateProgress(newProgress)

            Dim frmWTCertColTag As New frmWHTaxCertificateSTLDetails
            With frmWTCertColTag
                ._WHTaxCertSTLHelper = _WHTaxCertSTLHelper
                ._ViewCertificate = True
                ._CertificateNo = certifNo
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub btnAllocateToAP_Click(sender As Object, e As EventArgs) Handles btnAllocateToAP.Click
        Try
            Me.gbMenu1.Enabled = False
            Me.gbMenu2.Enabled = False
            Me.chkbox_SelectAll.Enabled = False
            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)

            Dim rowCounter As Integer = 0
            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colAllocatedToAP").Value) = True Then
                    rowCounter += 1
                End If
            Next

            If rowCounter = 0 Then
                MessageBox.Show("No selected row!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ask = MessageBox.Show("Do you really want to allocate to AP?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ask = DialogResult.No Then
                Exit Sub
            End If

            Dim getListOfRemittanceDate As New Date
            Dim getTimeStart As New DateTime
            Dim getTimeEnd As New DateTime
            getTimeStart = DateTime.Now()

            cts = New CancellationTokenSource
            Me.Timer1.Start()
            Me.stopWatch.Start()

            newProgress = New ProgressClass With {.ProgressMsg = "Please wait while preparing."}
            UpdateProgress(newProgress)

            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colAllocatedToAP").Value) = True Then
                    Dim certifNo As Long = CLng(row.Cells("colCertificateNo").Value)
                    getListOfRemittanceDate = CDate(row.Cells("colRemittanceDate").Value)

                    newProgress = New ProgressClass With {.ProgressMsg = "Allocating Certificate No:" & certifNo.ToString("N0")}
                    UpdateProgress(newProgress)

                    Await Task.Run(Sub() _WHTaxCertSTLHelper.SaveAllocatedToAp(certifNo, progressIndicator, cts.Token))
                    row.DefaultCellStyle.ForeColor = Drawing.Color.Red
                    row.ReadOnly = True
                End If
            Next

            Await Task.Run(Sub() _WHTaxCertSTLHelper.SaveSTLNoticeNew(getListOfRemittanceDate, progressIndicator, cts.Token))
            getTimeEnd = DateTime.Now()
            cts = Nothing

            ProgressThread.Close()
            MessageBox.Show("The processed data have been successfully allocated and saved." & vbNewLine _
                                & "Date Time Start: " & getTimeStart.ToString("MM/dd/yyyy hh:mm") & vbNewLine _
                                & "Date Time End: " & getTimeEnd.ToString("MM/dd/yyyy hh:mm"), "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

            Me.tsslbl_Msg.Text = "Ready"
            Me.tsslbl_Timer.Text = Me.DGridViewCollection.RowCount.ToString("N0") & " records"

            Me.gbMenu1.Enabled = True
            Me.gbMenu2.Enabled = True
            Me.chkbox_SelectAll.Enabled = True
        End Try
    End Sub

    Private Sub chkbox_SelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkbox_SelectAll.CheckedChanged

        If chkbox_SelectAll.Checked = True Then
            Dim rowCounter As Integer = 0
            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colAllocatedToAP").Value) = False Then
                    rowCounter += 1
                End If
            Next

            If rowCounter = 0 Then
                MessageBox.Show("No available row to check!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ask = MessageBox.Show("Do you really want to check all?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ask = DialogResult.No Then
                Exit Sub
            End If

            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colAllocatedToAP").Value) = False Then
                    row.Cells("colAllocatedToAP").Value = True
                End If
            Next
        Else
            Dim rowCounter As Integer = 0
            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colAllocatedToAP").Value) = True Then
                    rowCounter += 1
                End If
            Next

            If rowCounter = 0 Then
                MessageBox.Show("No available row to uncheck!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colAllocatedToAP").Value) = True Then
                    row.Cells("colAllocatedToAP").Value = False
                End If
            Next
        End If

    End Sub

    Private Sub DGridViewCollection_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGridViewCollection.CellContentClick
        Try
            If DGridViewCollection.Columns(e.ColumnIndex).Name = "colUntag" Then
                If Not DGridViewCollection.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Drawing.Color.Red Then
                    If CBool(DGridViewCollection.Rows(e.RowIndex).Cells("colUntag").Value) = True Then
                        DGridViewCollection.Rows(e.RowIndex).Cells("colUntag").Value = False
                        DGridViewCollection.Rows(e.RowIndex).Cells("colAllocatedToAP").ReadOnly = False
                    Else
                        DGridViewCollection.Rows(e.RowIndex).Cells("colUntag").Value = True
                        DGridViewCollection.Rows(e.RowIndex).Cells("colAllocatedToAP").ReadOnly = True
                    End If
                End If
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub btnUntag_Click(sender As Object, e As EventArgs) Handles btnUntag.Click
        Try
            Me.gbMenu1.Enabled = False
            Me.gbMenu2.Enabled = False
            Me.chkbox_SelectAll.Enabled = False

            Dim rowCounter As Integer = 0
            cts = New CancellationTokenSource
            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colUntag").Value) = True Then
                    rowCounter += 1
                End If
            Next

            If rowCounter = 0 Then
                MessageBox.Show("No selected row to execute untagging.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ask = MessageBox.Show("Do you really want to untag the selected rows?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ask = DialogResult.No Then
                Exit Sub
            End If

            ProgressThread.Show("Please wait while untagging...")
            Dim getListOfRemittanceDate As New Date
            Dim getTimeStart As New DateTime
            Dim getTimeEnd As New DateTime
            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)
            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colUntag").Value) = True Then
                    Dim certifNo As Long = CLng(row.Cells("colCertificateNo").Value)
                    getListOfRemittanceDate = CDate(row.Cells("colRemittanceDate").Value)
                    Await Task.Run(Sub() _WHTaxCertSTLHelper.UntagEWTSelected(certifNo, progressIndicator, cts.Token))
                    row.DefaultCellStyle.ForeColor = Drawing.Color.Red
                    row.ReadOnly = True
                End If
            Next
            cts = Nothing
            ProgressThread.Close()
            MessageBox.Show("Untagging successfully executed.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.gbMenu1.Enabled = True
            Me.gbMenu2.Enabled = True
            Me.chkbox_SelectAll.Enabled = True
        End Try
    End Sub
    Private Sub frmWHTaxCertificateSTLMgt_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
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
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim elapsed As TimeSpan = Me.StopWatch.Elapsed
        tsslbl_Timer.Text = "Timer: " & String.Format("{0:00}:{1:00}:{2:00}", Math.Floor(elapsed.TotalHours), elapsed.Minutes, elapsed.Seconds)
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        If cts IsNot Nothing Then
            cts.Cancel()
            newProgress = New ProgressClass With {.ProgressIndicator = 0, .ProgressMsg = "Please wait while canceling..."}
            Me.UpdateProgress(newProgress)
            btn_Close.Enabled = False
        Else
            Me.Close()
        End If
    End Sub
End Class