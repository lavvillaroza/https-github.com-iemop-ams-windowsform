Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Public Class frmPaymentTaggingDetails
    Public _PaymentTagHelper As PaymentTaggingHelper
    Public _ViewPaymentTag As Boolean = False
    Public _PaymentTagNo As Long = 0

    Private Sub frmPaymentTaggingDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _ViewPaymentTag = True Then
            Me.FillDataForViewing()
        Else
            Me.FillParticipantsSelection()
        End If

        Me.btn_Allocate.Enabled = False
        Me.btnSave.Enabled = False
    End Sub

    Private Sub FillDataForViewing()
        Try
            ProgressThread.Show("Please wait while preparing the view of WESM Transaction.")

            Dim getSelectedPaymentTag As PaymentTag = _PaymentTagHelper.GetPaymentTag(_PaymentTagNo)

            Me.dtRemittanceDate.Value = getSelectedPaymentTag.RemittanceDate
            Me.dtRemittanceDate.Enabled = False

            Me.ddlParticipantID.Items.Clear()
            Me.ddlParticipantID.Items.Add(getSelectedPaymentTag.BillingIDNumber.IDNumber)
            Me.ddlParticipantID.SelectedIndex = 0
            Me.ddlParticipantID.Enabled = False

            Me.btn_Allocate.Hide()
            Me.btnSave.Hide()

            Me.dgTagging.Rows.Clear()
            Me.dgTagging.Columns(10).ReadOnly = True
            Me.dgTagging.Columns("colFullyPaid").Visible = False
            For Each item In getSelectedPaymentTag.TagDetails
                Me.dgTagging.Rows.Add(item.WESMBillSummary.WESMBillSummaryNo, item.WESMBillSummary.WESMBillBatchNo.ToString("d5"), item.WESMBillSummary.BillPeriod.ToString,
                                  item.WESMBillSummary.IDNumber.IDNumber.ToString, item.WESMBillSummary.INVDMCMNo, item.WESMBillSummary.ChargeType.ToString, item.WESMBillSummary.DueDate.ToString("MM/dd/yyyy"),
                                  item.WESMBillSummary.OrigNewDueDate.ToString("MM/dd/yyyy"), FormatNumber(item.EndingBalance, UseParensForNegativeNumbers:=TriState.True),
                                  False, FormatNumber(item.AmountTagAlloc, UseParensForNegativeNumbers:=TriState.True),
                                  FormatNumber(item.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True))
            Next

            Me.FormatTextBoxForDGVTagging()

            Me.dgAllocation.Rows.Clear()
            For Each item In getSelectedPaymentTag.AllocDetails
                Me.dgAllocation.Rows.Add(item.WESMBillSummary.WESMBillSummaryNo, item.WESMBillSummary.WESMBillBatchNo.ToString("d5"), item.WESMBillSummary.BillPeriod.ToString,
                                  item.WESMBillSummary.IDNumber.IDNumber.ToString, item.WESMBillSummary.INVDMCMNo, item.WESMBillSummary.ChargeType.ToString, item.WESMBillSummary.DueDate.ToString("MM/dd/yyyy"),
                                  item.WESMBillSummary.OrigNewDueDate.ToString("MM/dd/yyyy"), FormatNumber(item.AmountTagAlloc, UseParensForNegativeNumbers:=TriState.True))
            Next
            ProgressThread.Close()
            Me.FormatTextBoxForDGVAllocation()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillParticipantsSelection()
        Try
            Dim listOfParticipant As List(Of String) = Me._PaymentTagHelper.GetListOfParticipants()

            ddlParticipantID.Items.Clear()
            For Each item In listOfParticipant
                ddlParticipantID.Items.Add(item)
            Next
            ddlParticipantID.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FormatTextBoxForDGVTagging()
        Dim sumOfTaggedAmount As Decimal = 0D
        Dim sumOfNewOutstandingBalance As Decimal = 0D
        For Each row As DataGridViewRow In dgTagging.Rows
            If CDec(row.Cells("colTagAmountAP").Value) > 0 Then
                sumOfTaggedAmount += CDec(row.Cells("colTagAmountAP").Value)
            End If

            sumOfNewOutstandingBalance += CDec(row.Cells("colNewEndingBalanceAP").Value)
        Next
        Me.txtTotalTaggedAmount.Text = FormatNumber(sumOfTaggedAmount, UseParensForNegativeNumbers:=TriState.True)
        Me.txtTotalAROutstandingBalance.Text = FormatNumber(sumOfNewOutstandingBalance, UseParensForNegativeNumbers:=TriState.True)
    End Sub

    Private Sub FormatTextBoxForDGVAllocation()
        Dim sumOfAllocAmount As Decimal = 0D
        Dim sumOfNewOutstandingBalance As Decimal = 0D
        For Each row As DataGridViewRow In dgAllocation.Rows
            If CDec(row.Cells("colAllocAmountAR").Value) > 0 Then
                sumOfAllocAmount += CDec(row.Cells("colAllocAmountAR").Value)
            End If
        Next
        Me.txtTotalAllocAmount.Text = FormatNumber(sumOfAllocAmount, UseParensForNegativeNumbers:=TriState.True)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ProgressThread.Show("Please wait while saving...")
            _PaymentTagHelper.SavePaymentTag()
            ProgressThread.Close()
            MessageBox.Show("Successfully saved!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Close()
        End Try
    End Sub

    Private Sub ddlParticipantID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParticipantID.SelectedIndexChanged
        Try
            If ddlParticipantID.SelectedIndex = -1 Then
                Exit Sub
            End If

            If _ViewPaymentTag = True Then
                Exit Sub
            End If

            Dim selectedIDNumber As String = CStr(ddlParticipantID.Text)
            Dim selectedRemittanceDate As Date = CDate(dtRemittanceDate.Text)
            ProgressThread.Show("Please wait while preparing the list of WESM Transaction.")
            _PaymentTagHelper.GetWESMBillSummaryAP(selectedIDNumber)

            Dim getWESMBillSummaryForSelectedID As List(Of PaymentTagDetails) = (From x In _PaymentTagHelper.FetchListPaymentTagDetails Where x.WESMBillSummary.IDNumber.IDNumber = selectedIDNumber Select x).ToList
            Me.dgTagging.Rows.Clear()

            For Each item In getWESMBillSummaryForSelectedID
                Me.dgTagging.Rows.Add(item.WESMBillSummary.WESMBillSummaryNo, item.WESMBillSummary.WESMBillBatchNo.ToString("d5"), item.WESMBillSummary.BillPeriod.ToString,
                                      item.WESMBillSummary.IDNumber.IDNumber.ToString, item.WESMBillSummary.INVDMCMNo, item.WESMBillSummary.ChargeType.ToString, item.WESMBillSummary.DueDate.ToString("MM/dd/yyyy"),
                                      item.WESMBillSummary.OrigNewDueDate.ToString("MM/dd/yyyy"), FormatNumber(item.WESMBillSummary.OrigEndingBalance, UseParensForNegativeNumbers:=TriState.True),
                                      False, "0.00", FormatNumber(item.WESMBillSummary.OrigEndingBalance, UseParensForNegativeNumbers:=TriState.True))
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


            Dim ListPaymentTaggedDetails As New List(Of PaymentTagDetails)
            ProgressThread.Show("Please wait while processing the allocation...")
            For Each row As DataGridViewRow In dgTagging.Rows
                If CDate(Me.dtRemittanceDate.Value) <= CDate(row.Cells("colNewDueDateAP").Value) Then
                    ProgressThread.Close()
                    MessageBox.Show("Invalid Remittance Date!", "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If CDec(row.Cells("colTagAmountAP").Value) > 0 Then
                    Dim itemPaymentTaggedDetails As New PaymentTagDetails
                    Dim wbSummaryNo As Long = CLng(row.Cells("colWBSummaryNoAP").Value)
                    itemPaymentTaggedDetails = (From x In _PaymentTagHelper.FetchListPaymentTagDetails Where x.WESMBillSummary.WESMBillSummaryNo = wbSummaryNo Select x).First
                    itemPaymentTaggedDetails.AmountTagAlloc = CDec(row.Cells("colTagAmountAP").Value)
                    itemPaymentTaggedDetails.NewEndingBalance = CDec(row.Cells("colNewEndingBalanceAP").Value)
                    ListPaymentTaggedDetails.Add(itemPaymentTaggedDetails)
                End If
            Next

            ListPaymentTaggedDetails.TrimExcess()

            Dim collectionDate As Date = CDate(FormatDateTime(CDate(Me.dtRemittanceDate.Value), DateFormat.ShortDate))
            Dim participantID As String = CStr(Me.ddlParticipantID.SelectedItem.ToString())
            _PaymentTagHelper.GenerateNewPaymentTagging(collectionDate, participantID, ListPaymentTaggedDetails)

            Me.FillDGViews()
            Me.btnSave.Enabled = True
            Me.btn_Allocate.Enabled = False
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub FillDGViews()
        Me.dgTagging.Rows.Clear()
        Me.dgTagging.Columns(10).ReadOnly = True
        For Each item In _PaymentTagHelper.NewPaymentTag.TagDetails
            Me.dgTagging.Columns("colFullyPaid").Visible = False
            Me.dgTagging.Rows.Add(item.WESMBillSummary.WESMBillSummaryNo, item.WESMBillSummary.WESMBillBatchNo.ToString("d5"), item.WESMBillSummary.BillPeriod.ToString,
                                  item.WESMBillSummary.IDNumber.IDNumber.ToString, item.WESMBillSummary.INVDMCMNo, item.WESMBillSummary.ChargeType.ToString, item.WESMBillSummary.DueDate.ToString("MM/dd/yyyy"),
                                  item.WESMBillSummary.OrigNewDueDate.ToString("MM/dd/yyyy"), FormatNumber(item.WESMBillSummary.OrigEndingBalance, UseParensForNegativeNumbers:=TriState.True),
                                  False, FormatNumber(item.AmountTagAlloc, UseParensForNegativeNumbers:=TriState.True),
                                  FormatNumber(item.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True))
        Next

        Me.FormatTextBoxForDGVTagging()

        Me.dgAllocation.Rows.Clear()
        Me.dgAllocation.Columns(8).ReadOnly = True
        For Each item In _PaymentTagHelper.NewPaymentTag.AllocDetails
            Me.dgAllocation.Rows.Add(item.WESMBillSummary.WESMBillSummaryNo, item.WESMBillSummary.WESMBillBatchNo.ToString("d5"), item.WESMBillSummary.BillPeriod.ToString,
                                  item.WESMBillSummary.IDNumber.IDNumber.ToString, item.WESMBillSummary.INVDMCMNo, item.WESMBillSummary.ChargeType.ToString, item.WESMBillSummary.DueDate.ToString("MM/dd/yyyy"),
                                  item.WESMBillSummary.OrigNewDueDate.ToString("MM/dd/yyyy"),
                                  FormatNumber(item.AmountTagAlloc, UseParensForNegativeNumbers:=TriState.True))
        Next

        Me.FormatTextBoxForDGVAllocation()
    End Sub

    Private Sub dgTagging_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgTagging.CellValueChanged
        Try
            If Me.dgTagging.Rows.Count > 0 Then
                Select Case e.ColumnIndex
                    Case 9
                        If Not CBool(Me.dgTagging.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False Then
                            Me.dgTagging.Rows(e.RowIndex).Cells(10).Value = FormatNumber(Me.dgTagging.Rows(e.RowIndex).Cells(8).Value, UseParensForNegativeNumbers:=TriState.True)
                            Me.dgTagging.Rows(e.RowIndex).Cells(11).Value = FormatNumber(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(8).Value) - CDec(Me.dgTagging.Rows(e.RowIndex).Cells(10).Value), UseParensForNegativeNumbers:=TriState.True)
                        Else
                            Me.dgTagging.Rows(e.RowIndex).Cells(10).Value = FormatNumber(0, UseParensForNegativeNumbers:=TriState.True)
                            Me.dgTagging.Rows(e.RowIndex).Cells(11).Value = FormatNumber(CDec(Me.dgTagging.Rows(e.RowIndex).Cells(8).Value) - CDec(Me.dgTagging.Rows(e.RowIndex).Cells(10).Value), UseParensForNegativeNumbers:=TriState.True)
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

End Class