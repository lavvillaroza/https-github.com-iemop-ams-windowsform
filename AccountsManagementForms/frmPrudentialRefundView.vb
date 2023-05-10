Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib
Public Class frmPrudentialRefundView
    Private prudentialRefundHelper As PrudentialRefundHelper
    Private Sub frmPrudentialRefundView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            prudentialRefundHelper = New PrudentialRefundHelper
            prudentialRefundHelper.initializeView()
            Me.FillDDTransDateList()

            Me.DisableControls()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillDDTransDateList()
        For Each item In Me.prudentialRefundHelper.TransDateList
            Me.cbo_TransDate.Items.Add(item.ToShortDateString)
        Next
    End Sub
    Private Sub DisableControls()
        Me.btnJVSetup.Enabled = False
        Me.BtnJVClosing.Enabled = False
        Me.BtnJVEFT.Enabled = False
        Me.btnGenerateFTF.Enabled = False        
    End Sub

    Private Sub EnableControls()
        Me.btnJVSetup.Enabled = True
        Me.BtnJVClosing.Enabled = True
        Me.BtnJVEFT.Enabled = True
        Me.btnGenerateFTF.Enabled = True        
    End Sub

    Private Sub btn_ViewTrans_Click(sender As Object, e As EventArgs) Handles btn_ViewTrans.Click
        Try
            Dim selectedTransDate As Date = CDate(Me.cbo_TransDate.Text)
            prudentialRefundHelper.GetPrudentialByTransDate(selectedTransDate)

            'Clear the DataGridView
            Me.DGridViewPrudential.Rows.Clear()
            For Each item In Me.prudentialRefundHelper.PrudentialRefund.PRRefundDetails
                With Me.DGridViewPrudential
                    Me.DGridViewPrudential.Rows.Add(FormatDateTime(Me.prudentialRefundHelper.PrudentialRefund.TransactionDate, DateFormat.ShortDate), item.IDNumber, _
                                                item.ParticipantID, item.FullName, item.PRAmount.ToString("N"), item.InterestAmount.ToString("N"), item.AmountRefund.ToString("N"))
                End With
            Next

            Me.EnableControls()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cbo_TransDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_TransDate.SelectedIndexChanged
        btn_ViewTrans.Enabled = True
    End Sub

    Private Sub btnJVSetup_Click(sender As Object, e As EventArgs) Handles btnJVSetup.Click
        Try
            Dim DS As New DataSet

            DS = Me.prudentialRefundHelper.GenerateJVReport(EnumPostedType.PRREFFTF)
            If DS.Tables.Count > 0 Then
                ProgressThread.Show("Please wait while preparing JV Setup Report.")
                Dim frmReport As New frmReportViewer
                With frmReport
                    .LoadJournalVoucher(DS)
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
                    .LoadJournalVoucher(DS)
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
                    .LoadJournalVoucher(DS)
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
                    .LoadFTF(DTSet)
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

End Class