Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmPrudentialRefund
    Private prudentialRefundHelper As PrudentialRefundHelper
    Private Sub frmPrudentialRefundView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            prudentialRefundHelper = New PrudentialRefundHelper
            Me.DisableControls()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DisableControls()
        Me.btnJVSetup.Enabled = False
        Me.BtnJVClosing.Enabled = False
        Me.BtnJVEFT.Enabled = False        
        Me.btnGenerateFTF.Enabled = False
        Me.btnSave.Enabled = False        
    End Sub

    Private Sub EnableControls()
        Me.btnJVSetup.Enabled = True
        Me.BtnJVClosing.Enabled = True
        Me.BtnJVEFT.Enabled = True
        Me.btnGenerateFTF.Enabled = True
        Me.btnSave.Enabled = True
    End Sub

    Private Sub btnInputRefund_Click(sender As Object, e As EventArgs) Handles btnInputRefund.Click
        Dim transDate As Date, currdate As Date
        Dim frm As New frmPrudentialRefundTransDate
        Dim remittanceDateFromPayment As New AllocationDate

        remittanceDateFromPayment = Me.prudentialRefundHelper.GetMaxPaymentRemitDate()
        With frm
            If frm.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If
            transDate = CDate(FormatDateTime(frm.dtAllocationDate.Value, DateFormat.ShortDate))
            currdate = CDate(Me.prudentialRefundHelper.GetSystemDate())
            If transDate < remittanceDateFromPayment.RemittanceDate Then
                MessageBox.Show("Latest payment remittance date is " & remittanceDateFromPayment.RemittanceDate & ", please input a date that be equal or greater than the latest payment remittance date!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If transDate > currdate Then
                MessageBox.Show("Please choose transaction date not greater than the current date!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End With

        Try
            Me.prudentialRefundHelper.CreateTransaction(transDate)
            'Clear the DataGridView
            Me.DGridViewPrudential.Rows.Clear()
            For Each item In Me.prudentialRefundHelper.PrudentialDetailsList
                With Me.DGridViewPrudential
                    Me.DGridViewPrudential.Rows.Add(FormatDateTime(transDate, DateFormat.ShortDate), item.IDNumber, _
                                                item.ParticipantID, item.FullName, item.PRAmount.ToString("N"), item.InterestAmount.ToString("N"), "0.00")
                End With
            Next

            Me.EnableControls()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        
    End Sub

    Private Sub DGridViewPrudential_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGridViewPrudential.CellEndEdit
        Try
            Select Case e.ColumnIndex
                Case 6
                    If Not IsNumeric(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                        MessageBox.Show("Amount Refund is not numeric.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.DGridViewPrudential.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                    ElseIf CDec(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                        MessageBox.Show("Amount Refund shall be absolute value or shall not be equal to 0.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.DGridViewPrudential.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                    ElseIf CDec(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = 0 Then
                        Dim formatValue As Decimal = CDec(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                        Me.DGridViewPrudential.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = formatValue.ToString("N")
                        Using PRRefundDet As New PrudentialRefundDetails
                            With PRRefundDet
                                .IDNumber = CStr(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(1).Value)
                                .ParticipantID = CStr(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(2).Value)
                                .FullName = CStr(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(3).Value)
                                .PRAmount = CDec(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(4).Value)
                                .InterestAmount = CDec(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(5).Value)
                                .AmountRefund = CDec(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(6).Value)
                                Me.prudentialRefundHelper.UpdateTransactionDetails(PRRefundDet)
                            End With
                        End Using
                    ElseIf CDec(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                        Dim formatValue As Decimal = CDec(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                        Me.DGridViewPrudential.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = formatValue.ToString("N")
                        Using PRRefundDet As New PrudentialRefundDetails
                            With PRRefundDet
                                .IDNumber = CStr(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(1).Value)
                                .ParticipantID = CStr(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(2).Value)
                                .FullName = CStr(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(3).Value)
                                .PRAmount = CDec(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(4).Value)
                                .InterestAmount = CDec(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(5).Value)
                                .AmountRefund = CDec(Me.DGridViewPrudential.Rows(e.RowIndex).Cells(6).Value)
                                If .InterestAmount > 0 And .AmountRefund = .PRAmount Then
                                    MessageBox.Show("Interest Amount shall be transfered first before the full PR Refund!", "System Mesasge", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Me.DGridViewPrudential.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                                    Exit Sub
                                End If
                                If .AmountRefund > .PRAmount Then
                                    MessageBox.Show("Amount Refund shall be not greater than the PR Amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Me.DGridViewPrudential.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                                    Exit Sub
                                Else
                                    Me.prudentialRefundHelper.UpdateTransactionDetails(PRRefundDet)
                                End If
                            End With
                        End Using
                    End If
                Case Else
                    Me.DGridViewPrudential.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End Select
        Catch ex As Exception
            Me.DGridViewPrudential.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnJVSetup_Click(sender As Object, e As EventArgs) Handles btnJVSetup.Click
        Try
            Dim DS As New DataSet

            DS = Me.prudentialRefundHelper.GenerateJVReport(EnumPostedType.PRREFFTF)
            If DS.Tables.Count > 0 Then
                ProgressThread.Show("Please wait while preparing JV Setup Report.")
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucherDraft(DS)
                    ProgressThread.Close()
                    .ShowDialog()
                End With
            Else
                MessageBox.Show("No available JV Setup, no movement on PR Refund.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnJVClosing_Click(sender As Object, e As EventArgs) Handles BtnJVClosing.Click
        Try
            Dim DS As New DataSet

            DS = Me.prudentialRefundHelper.GenerateJVReport(EnumPostedType.PRREF)
            If DS.Tables.Count > 0 Then
                ProgressThread.Show("Please wait while preparing JV Closing Report.")
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucherDraft(DS)
                    ProgressThread.Close()
                    .ShowDialog()
                End With
            Else
                MessageBox.Show("No available JV Closing, no movement on PR Refund.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnJVEFT_Click(sender As Object, e As EventArgs) Handles BtnJVEFT.Click
        Try
            Dim DS As New DataSet

            DS = Me.prudentialRefundHelper.GenerateJVReport(EnumPostedType.PRREFEFT)
            If DS.Tables.Count > 0 Then
                ProgressThread.Show("Please wait while preparing JV EFT Report.")
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucherDraft(DS)
                    ProgressThread.Close()
                    .ShowDialog()
                End With
            Else
                MessageBox.Show("No available JV EFT, no movement on PR Refund.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function getChecPay(ByVal checkPay As String) As String()
        Dim arrCheckPay As String() = checkPay.Split(New Char() {" "c})
        Dim returnValue(1) As String
        Dim retVal As String = ""

        For Each word In arrCheckPay
            retVal &= " " & word
            If retVal.Length <= 35 Then
                returnValue(0) &= " " & word
            Else
                returnValue(1) &= " " & word
            End If
        Next
        Return returnValue
    End Function

    Private Sub btnGenerateFTF_Click(sender As Object, e As EventArgs) Handles btnGenerateFTF.Click
        If Me.prudentialRefundHelper.PrudentialRefund.PRRefundDetails.Count = 0 Then
            MessageBox.Show("No available FTF, please input PR Refund first.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Try
            Dim ds As New DataSet
            Dim dtMain As New DSReport.FTFMainDataTable

            If Not Me.prudentialRefundHelper.PRFTF Is Nothing Then
                Dim DTSet As DataSet = Me.GenerateFTFDS(Me.prudentialRefundHelper.PRFTF)
                Dim frmViewer As New frmReportViewer()
                With frmViewer
                    .LoadFTFDraft(DTSet)
                    .ShowDialog()
                End With
            Else
                MessageBox.Show("No available FTF report based on selected transaction.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function GenerateFTFDS(ByVal FTF As FundTransferFormMain) As DataSet
        Dim DS As New DataSet
        Dim dtMain As New DSReport.FTFMainDataTable
        Dim dtParticipant As New DSReport.FTFParticipantDataTable
        Dim dtDetails As New DSReport.FTFDetailsDataTable
        Dim WBillHelper As WESMBillHelper = WESMBillHelper.GetInstance
        Dim Signatory = WBillHelper.GetSignatories("FTF").First()
        With FTF
            Dim row = dtMain.NewRow()
            row("REF_NO") = .RefNo
            row("DR_DATE") = .DRDate.ToString("MMMM dd,yyyy")
            row("CR_DATE") = .CRDate.ToString("MMMM dd,yyyy")
            row("REMARKS") = "Prudential Security Refund      P" & FormatNumber(.TotalAmount, 2)

            row("TOTAL_AMOUNT") = .TotalAmount
            row("PREPARED_BY") = AMModule.FullName
            row("POSITION") = AMModule.Position

            row("REQUESTING_APPROVAL") = Signatory.Signatory_1
            row("POSITION1") = Signatory.Position_1
            row("APPROVED_BY") = Signatory.Signatory_2
            row("POSITION2") = Signatory.Position_2
            row("NOTED_BY") = Signatory.Signatory_3
            row("POSITION3") = Signatory.Position_3            
            dtMain.Rows.Add(row)
        End With
        dtMain.AcceptChanges()


        For Each item In FTF.ListOfFTFParticipants
            Dim row = dtParticipant.NewRow()

            With item
                row("REF_NO") = .RefNo
                row("ID_NUMBER") = .IDNumber.IDNumber
                row("PARTICPANT_ID") = .IDNumber.ParticipantID
                row("AMOUNT") = .Amount

                dtParticipant.Rows.Add(row)
            End With
        Next
        dtParticipant.AcceptChanges()


        For Each item In FTF.ListOfFTFDetails
            Dim row = dtDetails.NewRow()

            With item
                row("REF_NO") = .RefNo
                row("BANK_ACCT_NO") = .BankAccountNo
                If .BankAccountNo.Contains("Pru") Then
                    If .Debit <> 0 Then
                        row("RECEIVING_BANK") = "SCB Prudential"
                    ElseIf .Credit <> 0 Then
                        row("ISSUING_BANK") = "SCB Prudential"
                    End If
                ElseIf .BankAccountNo.Contains("PEM") Then
                    If .Debit <> 0 Then
                        row("RECEIVING_BANK") = "BPI"
                    ElseIf .Credit <> 0 Then
                        row("ISSUING_BANK") = "BPI"
                    End If
                ElseIf .BankAccountNo.Contains("Setl") Then
                    If .Debit <> 0 Then
                        row("RECEIVING_BANK") = "SCB Settlement"
                    ElseIf .Credit <> 0 Then
                        row("ISSUING_BANK") = "SCB Settlement"
                    End If
                End If
                row("AMOUNT") = .Debit - .Credit
                dtDetails.Rows.Add(row)
            End With
        Next
        dtDetails.AcceptChanges()


        With DS.Tables
            .Add(dtMain)
            .Add(dtParticipant)
            .Add(dtDetails)
        End With
        DS.AcceptChanges()

        Return DS
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim msgAns As New MsgBoxResult
            msgAns = MsgBox("Do you really want to save the Payment Allocations?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")

            If msgAns = MsgBoxResult.Yes Then
                Me.DisableControls()
                Me.btnInputRefund.Enabled = False
                Me.btnClose.Enabled = False
                ProgressThread.Show("Please wait while SAVING.")


                Me.prudentialRefundHelper.SaveRefund()

                'Disposing trash data in memory
                Me.prudentialRefundHelper.Dispose()
                prudentialRefundHelper = New PrudentialRefundHelper
                Me.DisableControls()
                'Clear the DataGridView
                Me.DGridViewPrudential.Rows.Clear()

                ProgressThread.Close()
                MessageBox.Show("The processed data have been successfully saved.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
        Catch ex As Exception
            ProgressThread.Close()            
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.EnableControls()
            Me.btnInputRefund.Enabled = True
            Me.btnClose.Enabled = True
        End Try
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class