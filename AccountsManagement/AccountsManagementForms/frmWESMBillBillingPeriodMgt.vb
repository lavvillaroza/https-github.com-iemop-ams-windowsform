Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmWESMBillBillingPeriodMgt
    Private WBillHelper As WESMBillHelper
    Private ListOfWESMBillsGP As New List(Of WESMBillGPPosted)
    Private Sub frmWESMBillsChangeBP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            Me.getListOfWESMBillsForBPChange()
            Me.loadComboBoxDuedDate()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.ChangeWESMBillBP.ToString,
                                    ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInAccessing.ToString, AMModule.UserName)
        End Try
    End Sub

    Private Sub loadComboBoxDuedDate()
        With ddlDueDate
            .Items.Clear()
            For Each item In ListOfWESMBillsGP.Select(Function(x) x.DueDate).Distinct.OrderByDescending(Function(x) x)
                .Items.Add(item.ToString("MM/dd/yyyy"))
            Next
            .SelectedIndex = -1
        End With
    End Sub

    Private Sub getListOfWESMBillsForBPChange()
        ListOfWESMBillsGP = Me.WBillHelper.GetWBBPChange()
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        DGridView.Rows.Clear()
        If ddlDueDate.SelectedIndex = -1 Then
            MessageBox.Show("Please choose due date.", "No selected due date!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        For Each item In ListOfWESMBillsGP.OrderBy(Function(x) x.BillingPeriod And x.Charge).Where(Function(z) z.DueDate.ToString("MM/dd/yyyy") = ddlDueDate.SelectedItem)
            With DGridView
                .Rows.Add(item.BillingPeriod, item.SettlementRun, item.Charge, item.BatchCode, "Change BP")
            End With
        Next
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.getListOfWESMBillsForBPChange()
        Me.loadComboBoxDuedDate()
        DGridView.Rows.Clear()
    End Sub

    Public Sub refereshDGV()
        Me.getListOfWESMBillsForBPChange()
        DGridView.Rows.Clear()
        If ddlDueDate.SelectedIndex = -1 Then
            MessageBox.Show("Please choose due date.", "No selected due date!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        For Each item In ListOfWESMBillsGP.OrderBy(Function(x) x.BillingPeriod And x.Charge).Where(Function(z) z.DueDate.ToString("MM/dd/yyyy") = ddlDueDate.SelectedItem)
            With DGridView
                .Rows.Add(item.BillingPeriod, item.SettlementRun, item.Charge, item.BatchCode, "Change BP")
            End With
        Next
    End Sub
    Private Sub DGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGridView.CellClick
        With DGridView
            Dim senderGrid = DirectCast(sender, DataGridView)
            If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewLinkColumn AndAlso e.RowIndex >= 0 Then
                Dim currentRow As Integer = .CurrentCell.RowIndex

                Dim batchCode As String = .Rows(currentRow).Cells(3).Value
                Dim getWESMBillGPPosted = (From x In ListOfWESMBillsGP Where x.BatchCode = batchCode).FirstOrDefault

                If getWESMBillGPPosted IsNot Nothing Then
                    With frmWESMBillBillingPeriodChanger
                        .GetWESMBillGPPosted = getWESMBillGPPosted
                        .Show()
                    End With                    
                End If
            End If
        End With
    End Sub
End Class