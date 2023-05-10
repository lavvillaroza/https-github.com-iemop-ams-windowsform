Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports System.Threading
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib
Imports System.Runtime.InteropServices

Public Class frmUpdateWESMBillSummary
    Dim WBillHelper As WESMBillHelper
    Dim objUWBSummaryHelper As New UpdateWBillSummaryHelper
    Private IsSelected As Boolean = False
    Private IsEnergy As Boolean = False
    Private IsVAT As Boolean = False

    Private Sub frmUpdateWESMBillSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            Me.ParticipantID_CB.DataSource = Nothing
            Me.ParticipantID_CB.DataSource = objUWBSummaryHelper.GetListofParticipantID()
            Me.ParticipantID_CB.SelectedIndex = -1
            Me.IsSelected = True
            Me.Energy_RB.Checked = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub GridViewFormat(ByVal dgv As DataGridView)
        With dgv.Columns(0)
            .Visible = False
        End With

        With dgv.Columns(7).DefaultCellStyle
            .Format = "N2"
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        With dgv.Columns(8).DefaultCellStyle
            .Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    Private Sub ParticipantID_CB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ParticipantID_CB.SelectedIndexChanged
        If Me.ParticipantID_CB.SelectedIndex = -1 Then
            Exit Sub
        End If

        If IsSelected = True Then
            'objUWBSummaryHelper.InitializeObject()
            Me.GetWESMBillSummaryBasedOnSelectedData()
        End If
    End Sub

    Private Sub dgv_UWBSummaryList_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv_UWBSummaryList.CellMouseDoubleClick
        Try
            If e.RowIndex >= 0 Then
                Dim r As Integer = dgv_UWBSummaryList.CurrentRow.Index
                Dim WBSNo As Long = CLng(dgv_UWBSummaryList.Item(0, r).Value.ToString)
                Dim ObjfrmUWBSummaryMgt As New frmUpdateWESMBillSummaryMgt

                With ObjfrmUWBSummaryMgt
                    .objWBSHelper = objUWBSummaryHelper
                    .objWBSummaryNo = WBSNo
                    .ShowDialog()
                End With
                Me.GetWESMBillSummaryBasedOnSelectedData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Energy_RB_CheckedChanged(sender As Object, e As EventArgs) Handles Energy_RB.CheckedChanged
        If IsVAT = True Then
            IsVAT = False
            IsEnergy = True
            Me.GetWESMBillSummaryBasedOnSelectedData()
        End If
    End Sub

    Private Sub VATonEnergy_RB_CheckedChanged(sender As Object, e As EventArgs) Handles VATonEnergy_RB.CheckedChanged
        If IsEnergy = True Then
            IsEnergy = False
            IsVAT = True
            Me.GetWESMBillSummaryBasedOnSelectedData()
        End If
    End Sub

    Private Sub GetWESMBillSummaryBasedOnSelectedData()
        Try
            Dim GetParticipantID As String = ParticipantID_CB.SelectedItem.ToString()
            Dim GetChargeType As New EnumChargeType
            If Me.Energy_RB.Checked = True Then
                IsEnergy = True
                GetChargeType = EnumChargeType.E
            ElseIf Me.VATonEnergy_RB.Checked = True Then
                IsVAT = True
                GetChargeType = EnumChargeType.EV
            End If

            Me.dgv_UWBSummaryList.Columns.Clear()
            Me.dgv_UWBSummaryList.DataSource = Nothing
            Me.dgv_UWBSummaryList.DataSource = Me.objUWBSummaryHelper.GetWESMBillSummaryDT(GetParticipantID, GetChargeType)
            Me.GridViewFormat(Me.dgv_UWBSummaryList)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK)
        End Try        
    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub

End Class