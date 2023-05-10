Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports System.Threading
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib


Public Class frmBIRAccessToRecord
    Private WbillHelper As WESMBillHelper
    Private objBIRAccessHelper As New BIRAccessToRecordHelper

    Private Sub frmBIRAccessToRecord_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
            WbillHelper = WESMBillHelper.GetInstance
            WbillHelper.ConnectionString = AMModule.ConnectionString
            WbillHelper.UserName = AMModule.UserName
            Me.LoadYear()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try        
    End Sub

    Private Sub LoadYear()
        Me.ddlYear_cmb.Items.Clear()
        If Me.objBIRAccessHelper.objTransactionYearList.Count = 0 Then
            MessageBox.Show("No available record to access.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        For Each item In Me.objBIRAccessHelper.objTransactionYearList
            Me.ddlYear_cmb.Items.Add(item)
        Next
    End Sub

    Private Sub ddlYear_cmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear_cmb.SelectedIndexChanged
        Try
            If Me.ddlYear_cmb.SelectedIndex = -1 Then
                Exit Sub
            End If
            Me.chkLB_Participants.Items.Clear()

            ProgressThread.Show("Please wait while processing.")
            Dim InvolvedParticipantList As List(Of AMParticipants) = Me.objBIRAccessHelper.GetParticipantsInvolvedInSelectedYear(Me.ddlYear_cmb.Text)

            If InvolvedParticipantList.Count = 0 Then
                MessageBox.Show("No available record to access. Please contact your administrator!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim GetInvolvedParticipantList As List(Of String) = (From x In InvolvedParticipantList Select x.ParticipantID).ToList()
            For Each item In GetInvolvedParticipantList
                Me.chkLB_Participants.Items.Add(item)
            Next
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try        
    End Sub

    Private Sub chkbox_SelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkbox_SelectAll.CheckedChanged
        For i As Integer = 0 To Me.chkLB_Participants.Items.Count - 1
            If chkbox_SelectAll.Checked = True Then
                Me.chkLB_Participants.SetItemChecked(i, True)
            Else
                Me.chkLB_Participants.SetItemChecked(i, False)
            End If
        Next
    End Sub

    Private Sub btn_ExportToExcel_Click(sender As Object, e As EventArgs) Handles btn_ExportToExcel.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim TargetPath As String = ""
        Dim SelectedYear As String = Me.ddlYear_cmb.Text
        Try
            If chkLB_Participants.CheckedItems.Count = 0 Then
                MessageBox.Show("No participants selected", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
            ProgressThread.Show("Please wait while processing.")

            For Each ParticipantID In Me.chkLB_Participants.CheckedItems                
                objBIRAccessHelper.GenerateBIRAcessToRecordReport(TargetPath, ParticipantID, SelectedYear)
            Next
            If objBIRAccessHelper.ReportCreatedCount = 0 Then
                ProgressThread.Close()
                MessageBox.Show("No generated report.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ProgressThread.Close()
                MessageBox.Show("Generating Reports are successfully.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try    
    End Sub
End Class