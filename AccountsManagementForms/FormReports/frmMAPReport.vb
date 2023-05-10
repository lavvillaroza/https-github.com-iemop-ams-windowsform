
Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmMAPReport
    Private WbillHelper As WESMBillHelper
    Private _MAPReportHelper As MAPReportHelper = New MAPReportHelper
    Private Sub frmEWTPayableBIR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        WbillHelper = WESMBillHelper.GetInstance
        WbillHelper.ConnectionString = AMModule.ConnectionString
        WbillHelper.UserName = AMModule.UserName

        Me.LoadItem()
    End Sub

    Private Sub LoadItem()
        Dim DueDateList As List(Of Date) = _MAPReportHelper.GetDueDate()        
        Me.DueDate_CB.Items.Clear()
        For Each item In DueDateList
            Me.DueDate_CB.Items.Add(item.ToShortDateString)
        Next
    End Sub

    Private Sub GenerateMAPRaw_Button_Click(sender As Object, e As EventArgs) Handles GenerateMAPRaw_Button.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim TargetPath As String = ""

        If Me.DueDate_CB.SelectedIndex = -1 Then
            MessageBox.Show("No duedate selected", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        With sFolderDialog
            .ShowDialog()
            If .SelectedPath.ToString.Trim.Length = 0 Then
                Exit Sub
            Else
                TargetPath = sFolderDialog.SelectedPath
            End If
        End With
        Try
            ProgressThread.Show("Please wait while processing.")
            With _MAPReportHelper
                .TargetedPath = TargetPath
                .GenerateMAPRawReport()
            End With            
            ProgressThread.Close()
            MsgBox("MAP has been generated successfully.", MsgBoxStyle.Information, "System Message")
        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            chkbox_SelectAll.Checked = False
            Exit Sub
        End Try
    End Sub

    Private Sub GenerateBIR2307_Button_Click(sender As Object, e As EventArgs) Handles GenerateBIR2307_Button.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim TargetPath As String = ""

        If ParticipantList_CLB.CheckedItems.Count = 0 Then
            MessageBox.Show("No participant/s selected", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        With sFolderDialog
            .ShowDialog()
            If .SelectedPath.ToString.Trim.Length = 0 Then
                Exit Sub
            Else
                TargetPath = sFolderDialog.SelectedPath
            End If
        End With
        Try
            ProgressThread.Show("Please wait while processing.")
            Dim CountItemGenerated As Integer = 0
            For Each Item In ParticipantList_CLB.CheckedItems
                With _MAPReportHelper
                    .TargetedPath = TargetPath
                    .GenerateBIR2307(Item.ToString)
                End With
                CountItemGenerated += 1
            Next
            ProgressThread.Close()
            MsgBox("Total of " & CountItemGenerated & " has been generated successfully.", MsgBoxStyle.Information, "System Message")
        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            chkbox_SelectAll.Checked = False
            Exit Sub
        End Try
    End Sub

    Private Sub DueDate_CB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DueDate_CB.SelectedIndexChanged
        If Me.DueDate_CB.SelectedIndex = -1 Then
            Exit Sub
        End If

        Try
            With _MAPReportHelper                
                .SelectedDueDate = CDate(Me.DueDate_CB.SelectedItem)
                .GetWBSalesAndPurchased()
                Me.ParticipantList_CLB.Items.Clear()
                For Each item In .GetParticipantList(CDate(Me.DueDate_CB.SelectedItem))
                    Me.ParticipantList_CLB.Items.Add(item)
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub chkbox_SelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkbox_SelectAll.CheckedChanged
        For i As Integer = 0 To Me.ParticipantList_CLB.Items.Count - 1
            If chkbox_SelectAll.Checked = True Then
                Me.ParticipantList_CLB.SetItemChecked(i, True)
            Else
                Me.ParticipantList_CLB.SetItemChecked(i, False)
            End If
        Next
    End Sub
End Class