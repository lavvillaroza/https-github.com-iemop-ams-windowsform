Imports System.IO
Imports System.Data
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmWBSParentIDChangeImport
    Public WBSChangeParentIdHelper As WBSChangeParentIdHelper
    Public SelectedBillingPeriod As Integer
    Private Sub frmWESMBillSummaryParentIDChangeImport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.InitializeCmbID()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub InitializeCmbID()
        For Each item In WBSChangeParentIdHelper.objBillingPeriod
            If Not item.BillingPeriod = SelectedBillingPeriod Then
                Me.cmb_BillingPeriodNo.Items.Add(item.BillingPeriod)
            End If
        Next
    End Sub
    Private Sub cmb_BillingPeriodNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_BillingPeriodNo.SelectedIndexChanged
        Try
            If cmb_BillingPeriodNo.SelectedIndex = -1 Then
                Exit Sub
            End If

            Dim WBSChangeParticipantIDList As List(Of WESMBillSummaryChangeParentId) = WBSChangeParentIdHelper.GetobjWESMBillSummaryChangeParent(cmb_BillingPeriodNo.SelectedItem.ToString)

            If Not WBSChangeParticipantIDList.Count = 0 Then
                Dim GetBPNo As Integer = WBSChangeParticipantIDList.Select(Function(x) x.BillingPeriod).Distinct.FirstOrDefault
                Dim DateRange As CalendarBillingPeriod = (From x In WBSChangeParentIdHelper.objBillingPeriod Where x.BillingPeriod = GetBPNo Select x).FirstOrDefault

                Me.dtp_DateFrom.Enabled = True
                Me.dtp_DateTo.Enabled = True

                Me.dtp_DateFrom.Value = DateRange.StartDate
                Me.dtp_DateFrom.Enabled = False
                Me.dtp_DateTo.Value = DateRange.EndDate
                Me.dtp_DateTo.Enabled = False

            Else
                Dim DateRange As CalendarBillingPeriod = (From x In WBSChangeParentIdHelper.objBillingPeriod Where x.BillingPeriod = cmb_BillingPeriodNo.SelectedItem Select x).FirstOrDefault

                Me.dtp_DateFrom.Enabled = True
                Me.dtp_DateTo.Enabled = True

                Me.dtp_DateFrom.Value = DateRange.StartDate
                Me.dtp_DateFrom.Enabled = False
                Me.dtp_DateTo.Value = DateRange.EndDate
                Me.dtp_DateTo.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        Try
            Dim msgAns As New MsgBoxResult
            msgAns = MsgBox("Do you really want to save?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")
            If msgAns = MsgBoxResult.Yes Then
                ProgressThread.Show("Please wait while saving...")
                WBSChangeParentIdHelper.ImportSave(CInt(Me.cmb_BillingPeriodNo.SelectedItem), SelectedBillingPeriod)
                ProgressThread.Close()
                MessageBox.Show("The processed data have been successfully saved.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibWESMBillChangeParentIDWindow.ToString,
                                    "The processed data  has been successfully saved", "Adding", "", CType(EnumColorCode.Green, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)
                Me.Close()
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        Me.Close()
    End Sub
End Class