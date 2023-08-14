Imports System.IO
Imports System.Data
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmWBSParentIdChange
    Private WBSChangeParentIdHelper As New WBSChangeParentIdHelper

    Private Sub frmWESMBillSummaryParentIDChange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            Me.InitializeCmbID()
            Me.btn_Add.Enabled = False
            Me.btn_Import.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub InitializeCmbID()
        Me.cmb_BillingPeriodNo.Items.Clear()
        For Each item In WBSChangeParentIdHelper.objBillingPeriod
            Me.cmb_BillingPeriodNo.Items.Add(item.BillingPeriod)
        Next
    End Sub


    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        Me.Close()
    End Sub

    Private Sub btn_Add_Click(sender As Object, e As EventArgs) Handles btn_Add.Click
        If Me.cmb_BillingPeriodNo.SelectedIndex = -1 Then
            MessageBox.Show("Please choose billing period.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        With frmWBSParentIDChangeMgt
            .WBSChangeParentIDHelpr = WBSChangeParentIdHelper
            .BillingPeriod = (From x In WBSChangeParentIdHelper.objBillingPeriod Where x.BillingPeriod = Me.cmb_BillingPeriodNo.SelectedItem Select x).FirstOrDefault
            .isForUPdate = False
            .ShowDialog()
        End With
    End Sub

    Private Sub cmb_BillingPeriodNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_BillingPeriodNo.SelectedIndexChanged
        Try
            If cmb_BillingPeriodNo.SelectedIndex = -1 Then
                Exit Sub
            End If
            Me.getUpdatedWBSChangeParentID(cmb_BillingPeriodNo.SelectedItem.ToString)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub getUpdatedWBSChangeParentID(ByVal selectedbp As String)
        Dim WBSChangeParticipantIDList As List(Of WESMBillSummaryChangeParentId) = WBSChangeParentIdHelper.GetobjWESMBillSummaryChangeParent(selectedbp)

        If Not WBSChangeParticipantIDList.Count = 0 Then
            Dim GetBPNo As Integer = WBSChangeParticipantIDList.Select(Function(x) x.BillingPeriod).Distinct.FirstOrDefault
            Dim DateRange As CalendarBillingPeriod = (From x In WBSChangeParentIdHelper.objBillingPeriod Where x.BillingPeriod = GetBPNo Select x).FirstOrDefault

            Me.dtp_DateFrom.Enabled = True
            Me.dtp_DateTo.Enabled = True

            Me.dtp_DateFrom.Value = DateRange.StartDate
            Me.dtp_DateFrom.Enabled = False
            Me.dtp_DateTo.Value = DateRange.EndDate
            Me.dtp_DateTo.Enabled = False

            Me.dgv_wbsChangeParentId.Rows.Clear()
            For Each item In WBSChangeParticipantIDList
                Me.dgv_wbsChangeParentId.Rows.Add(item.ParentParticipants.IDNumber, item.ParentParticipants.ParticipantID, item.ChildParticipants.IDNumber,
                                                     item.ChildParticipants.ParticipantID, item.NewParentParticipants.IDNumber, item.NewParentParticipants.ParticipantID,
                                                     item.Status.ToString, item.UpdatedBy, item.UpdatedDate, "UPDATE")

            Next
            DateRange = Nothing
        Else
            Dim DateRange As CalendarBillingPeriod = (From x In WBSChangeParentIdHelper.objBillingPeriod Where x.BillingPeriod = cmb_BillingPeriodNo.SelectedItem Select x).FirstOrDefault

            Me.dtp_DateFrom.Enabled = True
            Me.dtp_DateTo.Enabled = True

            Me.dtp_DateFrom.Value = DateRange.StartDate
            Me.dtp_DateFrom.Enabled = False
            Me.dtp_DateTo.Value = DateRange.EndDate
            Me.dtp_DateTo.Enabled = False
            Me.dgv_wbsChangeParentId.Rows.Clear()
            DateRange = Nothing
        End If
        Me.btn_Add.Enabled = True
        Me.btn_Import.Enabled = True
    End Sub
    Private Sub dgv_wbsChangeParentId_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_wbsChangeParentId.CellContentClick
        Try
            If e.RowIndex = -1 Then
                Exit Sub
            End If
            Select Case e.ColumnIndex
                Case 9
                    Dim parentId = CStr(Me.dgv_wbsChangeParentId.Rows(e.RowIndex).Cells("ParentIDNo").Value)
                    Dim childId = CStr(Me.dgv_wbsChangeParentId.Rows(e.RowIndex).Cells("ChildIDNo").Value)
                    Dim newParentId = CStr(Me.dgv_wbsChangeParentId.Rows(e.RowIndex).Cells("NewParentIdNo").Value)
                    Dim status = CStr(Me.dgv_wbsChangeParentId.Rows(e.RowIndex).Cells("Status").Value)
                    With frmWBSParentIDChangeMgt
                        .WBSChangeParentIDHelpr = WBSChangeParentIdHelper
                        .BillingPeriod = (From x In WBSChangeParentIdHelper.objBillingPeriod Where x.BillingPeriod = Me.cmb_BillingPeriodNo.SelectedItem Select x).FirstOrDefault
                        .isForUPdate = True
                        .UpdateParent(parentId, childId, newParentId, status)
                        .ShowDialog()
                    End With
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub btn_Import_Click(sender As Object, e As EventArgs) Handles btn_Import.Click
        Try
            With frmWBSParentIDChangeImport
                .WBSChangeParentIdHelper = Me.WBSChangeParentIdHelper
                .SelectedBillingPeriod = Me.cmb_BillingPeriodNo.SelectedItem
                .ShowDialog()
            End With

            Me.getUpdatedWBSChangeParentID(cmb_BillingPeriodNo.SelectedItem.ToString)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        InitializeCmbID()
    End Sub
End Class