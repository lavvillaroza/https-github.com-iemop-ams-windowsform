Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports System.Threading
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmWBSAdjustmentPerBatchMain
    Dim WBillHelper As WESMBillHelper
    Dim WBSAdjOnWTaxHelper As New WBSAdjOnWTAXHelper
    Private Sub frmWESMBillSummaryAdjustmentPerBatchMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            Me.fillupDataGridViewMain()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub fillupDataGridViewMain()
        Me.dgv_WBSPerBatchMain.Rows.Clear()
        Me.WBSAdjOnWTaxHelper.RefreshList()
        For Each item In WBSAdjOnWTaxHelper.WBSAmountAdjustmentWhTaxList
            Me.dgv_WBSPerBatchMain.Rows.Add(item.ReferenceID.ToString, item.BillingPeriod.ToString, item.BillingBatchNo.ToString, item.ChargeType.ToString, _
                                            FormatNumber(item.TotalARAmountAdjusted, UseParensForNegativeNumbers:=TriState.True), FormatNumber(item.TotalAPAmountAdjusted, UseParensForNegativeNumbers:=TriState.True), _
                                            item.UpdatedBy.ToString, item.UpdatedDate, "VIEW")
        Next
    End Sub
    Private Sub btn_Add_Click(sender As Object, e As EventArgs) Handles btn_Add.Click
        Try
            ProgressThread.Show("Please wait while Opening..")
            Dim _frmWBSAdjForWTaxFormAdd As New frmWBSAdjustmentPerBatchAdd
            With _frmWBSAdjForWTaxFormAdd
                ProgressThread.Close()
                .mainForm = Me
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        Me.Close()
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        Me.fillupDataGridViewMain()
    End Sub

    Private Sub dgv_WBSPerBatchMain_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_WBSPerBatchMain.CellContentClick
        Try
            If e.ColumnIndex = 8 Then
                Dim refID = CLng(Me.dgv_WBSPerBatchMain.Rows(e.RowIndex).Cells("colRefID").Value)
                ProgressThread.Show("Please wait while Opening..")
                WBSAdjOnWTaxHelper.GetSelectedWBSAmountAdjustedWhTax(refID)
                With frmWBSAdjustmentPerBatchView
                    .WBSAdjOnWTaxHelper = Me.WBSAdjOnWTaxHelper
                    .isView = True
                    ProgressThread.Close()
                    .ShowDialog()
                End With
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class