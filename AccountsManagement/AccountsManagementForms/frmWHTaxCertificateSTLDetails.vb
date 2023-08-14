Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports System.Threading
Imports System.Threading.Tasks

Public Class frmWHTaxCertificateSTLDetails
    Public _WHTaxCertSTLHelper As WHTaxCertificateSTLHelper
    Public _ViewCertificate As Boolean = False
    Public _CertificateNo As Long = 0
    Private cts As CancellationTokenSource
    Private Sub frmWHTaxCertificateCollectionTag_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _ViewCertificate = True Then
            Me.FillDataForViewing()
        Else
            Me.FillRemittanceDateSelection()            
        End If

        Me.btn_Allocate.Enabled = False
        Me.btnSave.Enabled = False
    End Sub
    Private Sub UpdateProgress(_ProgressMsg As ProgressClass)
        ToolStripStatus_LabelMsg.Text = _ProgressMsg.ProgressMsg
        ctrl_statusStrip.Refresh()
    End Sub
    Private Sub FillDataForViewing()
        Try
            ProgressThread.Show("Please wait while preparing the view of WESM Transaction.")

            Dim getSelectedCertificateNo As WHTaxCertificateSTL = _WHTaxCertSTLHelper.GetWTCertStl(_CertificateNo)

            Me.ddlRemittanceDate.Items.Clear()
            Me.ddlRemittanceDate.Items.Add(getSelectedCertificateNo.RemittanceDate)
            Me.ddlRemittanceDate.SelectedIndex = 0
            Me.ddlRemittanceDate.Enabled = False

            Me.ddlParticipantID.Items.Clear()
            Me.ddlParticipantID.Items.Add(getSelectedCertificateNo.BillingIDNumber.IDNumber)
            Me.ddlParticipantID.SelectedIndex = 0
            Me.ddlParticipantID.Enabled = False

            Me.btn_Allocate.Hide()
            Me.btnSave.Hide()

            Me.dgTagging.Rows.Clear()
            Me.dgTagging.Columns(10).ReadOnly = True
            Me.dgTagging.Columns("colFullyPaid").Visible = False
            For Each item In getSelectedCertificateNo.TagDetails
                Me.dgTagging.Rows.Add(item.WESMBillSummary.WESMBillSummaryNo, item.WESMBillSummary.WESMBillBatchNo.ToString("d5"), item.WESMBillSummary.BillPeriod.ToString,
                                  item.WESMBillSummary.IDNumber.IDNumber.ToString, item.WESMBillSummary.INVDMCMNo, item.WESMBillSummary.DueDate.ToString("MM/dd/yyyy"),
                                  item.WESMBillSummary.OrigNewDueDate.ToString("MM/dd/yyyy"), FormatNumber(item.EndingBalance, UseParensForNegativeNumbers:=TriState.True),
                                  FormatNumber(item.WithholdingTaxAmount.ToString("N2"), UseParensForNegativeNumbers:=TriState.True), False, FormatNumber(item.Amount, UseParensForNegativeNumbers:=TriState.True),
                                  FormatNumber(item.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True))
            Next

            Me.FormatTextBoxForDGVTagging()

            Me.dgAllocation.Rows.Clear()
            For Each item In getSelectedCertificateNo.AllocationDetails
                Me.dgAllocation.Rows.Add(item.WESMBillSummary.WESMBillSummaryNo, item.WESMBillSummary.WESMBillBatchNo.ToString("d5"), item.WESMBillSummary.BillPeriod.ToString,
                                  item.WESMBillSummary.IDNumber.IDNumber.ToString, item.WESMBillSummary.INVDMCMNo, item.WESMBillSummary.DueDate.ToString("MM/dd/yyyy"),
                                  item.WESMBillSummary.OrigNewDueDate.ToString("MM/dd/yyyy"),
                                  FormatNumber(item.WithholdingTaxAmount.ToString("N2"), UseParensForNegativeNumbers:=TriState.True),
                                  FormatNumber(item.Amount, UseParensForNegativeNumbers:=TriState.True))
            Next
            ProgressThread.Close()
            Me.FormatTextBoxForDGVAllocation()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillRemittanceDateSelection()
        Try
            ddlRemittanceDate.Items.Clear()
            For Each item In Me._WHTaxCertSTLHelper.InitializeListOfRemittanceDate()
                ddlRemittanceDate.Items.Add(item.RemittanceDate.ToShortDateString)
            Next
            ddlRemittanceDate.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillParticipantsSelection()
        Try
            If ddlRemittanceDate.SelectedIndex = -1 Then
                Exit Sub
            End If

            Dim selectedRemittanceDate As Date = CDate(ddlRemittanceDate.Text)
            Dim listOfParticipant As List(Of String) = Me._WHTaxCertSTLHelper.GetListOfParticipants(selectedRemittanceDate)
            ddlParticipantID.Text = ""
            ddlParticipantID.Items.Clear()
            For Each item In listOfParticipant
                ddlParticipantID.Items.Add(item)
            Next
            ddlParticipantID.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FillParticipantsSelectionForAdvanceEWT()
        Try
            Dim listOfParticipant As List(Of String) = Me._WHTaxCertSTLHelper.GetListOfParticipantsForAdvanceEWT()
            ddlParticipantID.Text = ""
            ddlParticipantID.Items.Clear()
            For Each item In listOfParticipant
                ddlParticipantID.Items.Add(item)
            Next
            ddlParticipantID.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ddlParticipantID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParticipantID.SelectedIndexChanged
        Try
            If ddlParticipantID.SelectedIndex = -1 Then
                Exit Sub
            End If

            If _ViewCertificate = True Then
                Exit Sub
            End If

            Dim selectedIDNumber As String = CStr(ddlParticipantID.Text)
            Dim selectedRemittanceDate As Date = CDate(ddlRemittanceDate.Text)
            ProgressThread.Show("Please wait while preparing the list of WESM Transaction.")
            _WHTaxCertSTLHelper.GetWESMBillSummaryWithWTAX(selectedIDNumber, selectedRemittanceDate)

            Dim getWESMBillSummaryForSelectedID As List(Of WHTaxCertificateDetails) = (From x In _WHTaxCertSTLHelper.FetchListWHTCertDetails Where x.WESMBillSummary.IDNumber.IDNumber = selectedIDNumber Select x).ToList
            Me.dgTagging.Rows.Clear()
            If getWESMBillSummaryForSelectedID.Count = 0 Then
                ProgressThread.Close()
                MessageBox.Show("No available EWT to tag!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            For Each item In getWESMBillSummaryForSelectedID
                Me.dgTagging.Rows.Add(item.WESMBillSummary.WESMBillSummaryNo, item.WESMBillSummary.WESMBillBatchNo.ToString("d5"), item.WESMBillSummary.BillPeriod.ToString,
                                          item.WESMBillSummary.IDNumber.IDNumber.ToString, item.WESMBillSummary.INVDMCMNo, item.WESMBillSummary.DueDate.ToString("MM/dd/yyyy"),
                                          item.WESMBillSummary.OrigNewDueDate.ToString("MM/dd/yyyy"), FormatNumber(item.WESMBillSummary.OrigEndingBalance, UseParensForNegativeNumbers:=TriState.True),
                                          FormatNumber(item.WithholdingTaxAmount, UseParensForNegativeNumbers:=TriState.True), False, "0.00", "0.00", FormatNumber(item.WESMBillSummary.OrigEndingBalance, UseParensForNegativeNumbers:=TriState.True))
            Next

            Me.btn_Allocate.Enabled = True
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btn_Allocate_Click(sender As Object, e As EventArgs) Handles btn_Allocate.Click
        Try
            Dim ListWHTCertCollectionTagged As New List(Of WHTaxCertificateDetails)
            ProgressThread.Show("Please wait while processing the allocation...")

            For Each row As DataGridViewRow In dgTagging.Rows
                If CDec(row.Cells("colTagAmountAR").Value) > 0 Or CDec(row.Cells("colTagAmountDIAR").Value) > 0 Then
                    Dim itemWHTCertCollectionTagged As New WHTaxCertificateDetails
                    Dim invoiceNo As String = CStr(row.Cells("colTransactionNoAR").Value)
                    itemWHTCertCollectionTagged = (From x In _WHTaxCertSTLHelper.FetchListWHTCertDetails Where x.WESMBillSummary.INVDMCMNo = invoiceNo Select x).First
                    itemWHTCertCollectionTagged.Amount = CDec(row.Cells("colTagAmountAR").Value)
                    itemWHTCertCollectionTagged.NewEndingBalance = CDec(row.Cells("colNewEndingBalanceAR").Value)
                    ListWHTCertCollectionTagged.Add(itemWHTCertCollectionTagged)
                End If
            Next

            ListWHTCertCollectionTagged.TrimExcess()
            Dim collectionDate As New Date
            collectionDate = CDate(FormatDateTime(CDate(Me.ddlRemittanceDate.SelectedItem.ToString()), DateFormat.ShortDate))

            Dim participantID As String = CStr(Me.ddlParticipantID.SelectedItem.ToString())
            _WHTaxCertSTLHelper.GenerateNewWHTaxCertTagAlloc(collectionDate, participantID, ListWHTCertCollectionTagged)

            Me.FillDGViews()
            Me.btnSave.Enabled = True
            Me.btn_Allocate.Enabled = False
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FillDGViews()
        Me.dgTagging.Rows.Clear()
        Me.dgTagging.Columns(10).ReadOnly = True
        For Each item In _WHTaxCertSTLHelper.NewWHTaxCertSTL.TagDetails
            Me.dgTagging.Columns("colFullyPaid").Visible = False
            Me.dgTagging.Rows.Add(item.WESMBillSummary.WESMBillSummaryNo, item.WESMBillSummary.WESMBillBatchNo.ToString("d5"), item.WESMBillSummary.BillPeriod.ToString,
                                  item.WESMBillSummary.IDNumber.IDNumber.ToString, item.WESMBillSummary.INVDMCMNo, item.WESMBillSummary.DueDate.ToString("MM/dd/yyyy"),
                                  item.WESMBillSummary.OrigNewDueDate.ToString("MM/dd/yyyy"), FormatNumber(item.WESMBillSummary.OrigEndingBalance, UseParensForNegativeNumbers:=TriState.True),
                                  FormatNumber(item.WithholdingTaxAmount.ToString("N2"), UseParensForNegativeNumbers:=TriState.True), False, FormatNumber(item.Amount, UseParensForNegativeNumbers:=TriState.True),
                                  FormatNumber(item.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True))
        Next

        Me.FormatTextBoxForDGVTagging()

        Me.dgAllocation.Rows.Clear()
        Me.dgAllocation.Columns(8).ReadOnly = True
        For Each item In _WHTaxCertSTLHelper.NewWHTaxCertSTL.AllocationDetails
            Me.dgAllocation.Rows.Add(item.WESMBillSummary.WESMBillSummaryNo, item.WESMBillSummary.WESMBillBatchNo.ToString("d5"), item.WESMBillSummary.BillPeriod.ToString,
                                  item.WESMBillSummary.IDNumber.IDNumber.ToString, item.WESMBillSummary.INVDMCMNo, item.WESMBillSummary.DueDate.ToString("MM/dd/yyyy"),
                                  item.WESMBillSummary.OrigNewDueDate.ToString("MM/dd/yyyy"), FormatNumber(item.WithholdingTaxAmount.ToString("N2"), UseParensForNegativeNumbers:=TriState.True),
                                  FormatNumber(item.Amount, UseParensForNegativeNumbers:=TriState.True))
        Next

        Me.FormatTextBoxForDGVAllocation()
    End Sub

    Private Sub dgTagging_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgTagging.CellEndEdit
        Try
            Select Case e.ColumnIndex
                Case 10
                    If Not IsNumeric(Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                        MessageBox.Show("Tagged Amount is not numeric.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                    Else
                        If CDec(Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) <= 0 Then
                            MessageBox.Show("Tagged Amount should not be negative.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                        ElseIf CDec(Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > Math.Abs(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(8).Value)) Then
                            MessageBox.Show("Tagged Amount should not be greater than EWT.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                        Else
                            Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = FormatNumber(Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value, UseParensForNegativeNumbers:=TriState.True)
                            Me.dgTagging.Rows(e.RowIndex).Cells(11).Value = FormatNumber(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(7).Value) + CDec(Me.dgTagging.Rows(e.RowIndex).Cells(10).Value), UseParensForNegativeNumbers:=TriState.True)
                        End If
                        Me.FormatTextBoxForDGVTagging()
                    End If
                Case 11
                    If Not IsNumeric(Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                        MessageBox.Show("Tagged Amount DI is not numeric.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                    Else
                        If CDec(Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) <= 0 Then
                            MessageBox.Show("Tagged Amount DI should not be negative.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                        ElseIf CDec(Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > Math.Abs(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(8).Value)) Then
                            MessageBox.Show("Tagged Amount DI should not be greater than EWT.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                        Else
                            Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = FormatNumber(Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value, UseParensForNegativeNumbers:=TriState.True)
                            Me.dgTagging.Rows(e.RowIndex).Cells(11).Value = FormatNumber(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(7).Value) + CDec(Me.dgTagging.Rows(e.RowIndex).Cells(10).Value), UseParensForNegativeNumbers:=TriState.True)
                        End If
                        Me.FormatTextBoxForDGVTagging()
                    End If
                Case Else
                    Me.dgTagging.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormatTextBoxForDGVTagging()
        Dim sumOfTaggedAmount As Decimal = 0D
        Dim sumOfNewOutstandingBalance As Decimal = 0D
        For Each row As DataGridViewRow In dgTagging.Rows
            If CDec(row.Cells("colTagAmountAR").Value) > 0 Then
                sumOfTaggedAmount += CDec(row.Cells("colTagAmountAR").Value)
            End If

            sumOfNewOutstandingBalance += CDec(row.Cells("colNewEndingBalanceAR").Value)
        Next
        Me.txtTotalTaggedAmount.Text = FormatNumber(sumOfTaggedAmount, UseParensForNegativeNumbers:=TriState.True)
        Me.txtTotalAROutstandingBalance.Text = FormatNumber(sumOfNewOutstandingBalance, UseParensForNegativeNumbers:=TriState.True)
    End Sub

    Private Sub FormatTextBoxForDGVAllocation()
        Dim sumOfAllocAmount As Decimal = 0D
        Dim sumOfNewOutstandingBalance As Decimal = 0D
        For Each row As DataGridViewRow In dgAllocation.Rows
            If CDec(row.Cells("colAllocAmountAP").Value) < 0 Then
                sumOfAllocAmount += CDec(row.Cells("colAllocAmountAP").Value)
            End If
        Next
        Me.txtTotalAllocAmount.Text = FormatNumber(sumOfAllocAmount, UseParensForNegativeNumbers:=TriState.True)
    End Sub

    Private Async Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            mainTLP.Enabled = False
            cts = New CancellationTokenSource
            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)
            Dim ask = MessageBox.Show("Do you really want to save?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ask = DialogResult.No Then
                Exit Sub
            End If

            ProgressThread.Show("Please wait while saving...")
            Await Task.Run(Sub() _WHTaxCertSTLHelper.SaveWHTaxTransaction(progressIndicator, cts.Token))

            ProgressThread.Close()
            MessageBox.Show("Successfully saved!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cts = Nothing
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            mainTLP.Enabled = True
            Me.Close()
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub dgTagging_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgTagging.CellValueChanged
        Try
            If Me.dgTagging.Rows.Count > 0 Then
                Select Case e.ColumnIndex
                    Case 9
                        If Not CBool(Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False Then
                            If Math.Abs(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(7).Value)) >= Math.Abs(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(8).Value)) Then
                                Me.dgTagging.Rows(e.RowIndex).Cells(10).Value = FormatNumber(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(8).Value), UseParensForNegativeNumbers:=TriState.True)
                                Me.dgTagging.Rows(e.RowIndex).Cells(11).Value = FormatNumber(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(8).Value) + CDec(Me.dgTagging.Rows(e.RowIndex).Cells(7).Value), UseParensForNegativeNumbers:=TriState.True)
                            Else
                                Me.dgTagging.Rows(e.RowIndex).Cells(10).Value = FormatNumber(Math.Abs(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(7).Value)), UseParensForNegativeNumbers:=TriState.True)
                                Me.dgTagging.Rows(e.RowIndex).Cells(11).Value = FormatNumber(Math.Abs(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(7).Value)) - Math.Abs(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(7).Value)), UseParensForNegativeNumbers:=TriState.True)
                            End If
                        Else
                            Me.dgTagging.Rows(e.RowIndex).Cells(10).Value = FormatNumber(0, UseParensForNegativeNumbers:=TriState.True)
                            Me.dgTagging.Rows(e.RowIndex).Cells(11).Value = FormatNumber(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(7).Value), UseParensForNegativeNumbers:=TriState.True)
                        End If
                        Me.FormatTextBoxForDGVTagging()
                    Case Else
                        Me.dgTagging.CommitEdit(DataGridViewDataErrorContexts.Commit)
                End Select
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgTagging_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgTagging.CurrentCellDirtyStateChanged
        If Me.dgTagging.IsCurrentCellDirty Then
            Me.dgTagging.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub ddlRemittanceDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRemittanceDate.SelectedIndexChanged
        Try
            FillParticipantsSelection()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub checkBoxAll_CheckedChanged(sender As Object, e As EventArgs) Handles checkBoxAll.CheckedChanged
        If Me.checkBoxAll.Checked = False Then
            For Each Rw As DataGridViewRow In dgTagging.Rows
                dgTagging.Item(9, Rw.Index).Value = False
            Next
        Else
            For Each Rw As DataGridViewRow In dgTagging.Rows
                dgTagging.Item(9, Rw.Index).Value = True
            Next
        End If
    End Sub

    Private Sub dtRemittanceDate_ValueChanged(sender As Object, e As EventArgs)
        Try
            Me.FillParticipantsSelectionForAdvanceEWT()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmWHTaxCertificateSTLDetails_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
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