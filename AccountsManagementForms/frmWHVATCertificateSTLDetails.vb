Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports System.Threading
Imports System.Threading.Tasks

Public Class frmWHVATCertificateSTLDetails
    Public _WHVatCertSTLHelper As WHVATCertificateSTLHelper
    Public _ViewCertificate As Boolean = False
    Public _CertificateNo As Long = 0
    Private cts As CancellationTokenSource
    Private Sub frmWHVATCertificateSTLDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            Dim getSelectedCertificateNo As WHVATCertificateSTL = _WHVatCertSTLHelper.GetWHVATCertStl(_CertificateNo)

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
                                  FormatNumber(item.AmountTagged, UseParensForNegativeNumbers:=TriState.True),
                                  FormatNumber(item.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True))
            Next

            Me.FormatTextBoxForDGVTagging()

            Me.dgAllocation.Rows.Clear()
            For Each item In getSelectedCertificateNo.AllocationDetails
                Me.dgAllocation.Rows.Add(item.WESMBillSummary.WESMBillSummaryNo, item.WESMBillSummary.WESMBillBatchNo.ToString("d5"), item.WESMBillSummary.BillPeriod.ToString,
                                  item.WESMBillSummary.IDNumber.IDNumber.ToString, item.WESMBillSummary.INVDMCMNo, item.WESMBillSummary.DueDate.ToString("MM/dd/yyyy"),
                                  item.WESMBillSummary.OrigNewDueDate.ToString("MM/dd/yyyy"),
                                  FormatNumber(item.AmountTagged, UseParensForNegativeNumbers:=TriState.True))
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
            For Each item In Me._WHVatCertSTLHelper.InitializeListOfRemittanceDate()
                ddlRemittanceDate.Items.Add(item.RemittanceDate.ToShortDateString)
            Next
            ddlRemittanceDate.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_Allocate_Click(sender As Object, e As EventArgs) Handles btn_Allocate.Click
        Try
            Dim ListWHTCertCollectionTagged As New List(Of WHVATCertificateDetails)
            ProgressThread.Show("Please wait while processing the allocation...")

            For Each row As DataGridViewRow In dgTagging.Rows
                If CDec(row.Cells("colTagAmountAR").Value) > 0 Then
                    Dim itemWHVCertCollectionTagged As New WHVATCertificateDetails
                    Dim invoiceNo As String = CStr(row.Cells("colTransactionNoAR").Value)
                    itemWHVCertCollectionTagged = (From x In _WHVatCertSTLHelper.FetchListWHVCertDetails Where x.WESMBillSummary.INVDMCMNo = invoiceNo Select x).First
                    itemWHVCertCollectionTagged.AmountTagged = CDec(row.Cells("colTagAmountAR").Value)
                    itemWHVCertCollectionTagged.NewEndingBalance = CDec(row.Cells("colNewEndingBalanceAR").Value)
                    ListWHTCertCollectionTagged.Add(itemWHVCertCollectionTagged)
                End If
            Next

            ListWHTCertCollectionTagged.TrimExcess()
            Dim collectionDate As New Date
            collectionDate = CDate(FormatDateTime(CDate(Me.ddlRemittanceDate.SelectedItem.ToString()), DateFormat.ShortDate))

            Dim participantID As String = CStr(Me.ddlParticipantID.SelectedItem.ToString())
            _WHVatCertSTLHelper.GenerateNewWHVATCertTagAlloc(collectionDate, participantID, ListWHTCertCollectionTagged)

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
        For Each item In _WHVatCertSTLHelper.NewWHVatCertSTL.TagDetails
            Me.dgTagging.Rows.Add(item.WESMBillSummary.WESMBillSummaryNo, item.WESMBillSummary.WESMBillBatchNo.ToString("d5"), item.WESMBillSummary.BillPeriod.ToString,
                                  item.WESMBillSummary.IDNumber.IDNumber.ToString, item.WESMBillSummary.INVDMCMNo, item.WESMBillSummary.DueDate.ToString("MM/dd/yyyy"),
                                  item.WESMBillSummary.OrigNewDueDate.ToString("MM/dd/yyyy"), FormatNumber(item.WESMBillSummary.OrigEndingBalance, UseParensForNegativeNumbers:=TriState.True),
                                  FormatNumber(item.AmountTagged, UseParensForNegativeNumbers:=TriState.True), FormatNumber(item.NewEndingBalance, UseParensForNegativeNumbers:=TriState.True))
        Next

        Me.FormatTextBoxForDGVTagging()

        Me.dgAllocation.Rows.Clear()
        Me.dgAllocation.Columns(8).ReadOnly = True
        For Each item In _WHVatCertSTLHelper.NewWHVatCertSTL.AllocationDetails
            Me.dgAllocation.Rows.Add(item.WESMBillSummary.WESMBillSummaryNo, item.WESMBillSummary.WESMBillBatchNo.ToString("d5"), item.WESMBillSummary.BillPeriod.ToString,
                                  item.WESMBillSummary.IDNumber.IDNumber.ToString, item.WESMBillSummary.INVDMCMNo, item.WESMBillSummary.DueDate.ToString("MM/dd/yyyy"),
                                  item.WESMBillSummary.OrigNewDueDate.ToString("MM/dd/yyyy"),
                                  FormatNumber(item.AmountTagged, UseParensForNegativeNumbers:=TriState.True))
        Next

        Me.FormatTextBoxForDGVAllocation()
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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
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
            Await Task.Run(Sub() _WHVatCertSTLHelper.SaveWHVATTransaction(progressIndicator, cts.Token))

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
End Class