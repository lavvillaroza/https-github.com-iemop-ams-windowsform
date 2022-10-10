'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmCheckSearch
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     January 31, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for Search filters on Checks
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmCheckSearch
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private lstParticipants As List(Of AMParticipants)
    Private lstChecks As List(Of Check)
    Public ForCancelCheck As Check
    Public isCancelled As Boolean

    Private Sub chkbox_Status_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_Status.CheckedChanged
        If Me.chkbox_Status.Checked Then
            gBox_Status.Enabled = True
        Else
            gBox_Status.Enabled = False
        End If
    End Sub

    Private Sub chkbox_EnableNumber_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_EnableNumber.CheckedChanged
        If Me.chkbox_EnableNumber.Checked Then
            gBox_Number.Enabled = True
            Me.txt_Number.Text = ""
        Else
            gBox_Number.Enabled = False
        End If
    End Sub

    Private Sub chkbox_EnableDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_EnableDate.CheckedChanged
        If Me.chkbox_EnableDate.Checked Then
            gBox_TransactionDate.Enabled = True
        Else
            gBox_TransactionDate.Enabled = False
        End If
    End Sub

    Private Sub chkbox_EnablePID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_EnablePID.CheckedChanged
        If Me.chkbox_EnablePID.Checked Then
            gBox_ParticipantID.Enabled = True
        Else
            gBox_ParticipantID.Enabled = False
        End If
    End Sub

    Private Sub cmd_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Search.Click
        Try
            If chkbox_EnableDate.Checked Then
                If dtp_From.Value > dtp_To.Value Then
                    MsgBox("Invalid Date Range.", MsgBoxStyle.Critical, "Error!")
                    Exit Sub
                End If

                'Get By Date
                lstChecks = Me.WBillHelper.GetCheck(dtp_From.Value, dtp_To.Value)
            ElseIf chkbox_EnablePID.Checked Then
                'Get By Participant
                Dim selParticipant = (From x In lstParticipants _
                                      Where x.ParticipantID = cbo_Participants.SelectedItem.ToString _
                                      Select x).FirstOrDefault

                lstChecks = Me.WBillHelper.GetCheck(selParticipant.IDNumber)
            ElseIf chkbox_EnableNumber.Checked Then
                'Get by Check No or Check Voucher No
                lstChecks = Me.WBillHelper.GetCheck(Me.txt_Number.Text.ToString, rb_CheckNo.Checked)
            ElseIf chkbox_Status.Checked Then
                'Get By Status of Check
                Dim _lstStatus As New List(Of EnumCheckStatus)

                If chk_Status1.Checked Then
                    _lstStatus.Add(EnumCheckStatus.SystemGenerated)
                End If

                If chk_Status2.Checked Then
                    _lstStatus.Add(EnumCheckStatus.GeneratedCheck)
                End If

                'If chk_Status3.Checked Then
                '    _lstStatus.Add(EnumCheckStatus.Released)
                'End If

                If chk_Status4.Checked Then
                    _lstStatus.Add(EnumCheckStatus.Cancelled)
                End If

                If chk_Status5.Checked Then
                    _lstStatus.Add(EnumCheckStatus.ReplacementForCancelled)
                End If

                'If chk_Status6.Checked Then
                '    _lstStatus.Add(EnumCheckStatus.Cleared)
                'End If

                lstChecks = Me.WBillHelper.GetCheck(_lstStatus)

            ElseIf chk_Cleared.Checked Then
                lstChecks = Me.WBillHelper.GetCheckCleared(rb_UnCleared.Checked)
                If rb_UnCleared.Checked = False Then
                    lstChecks = (From x In lstChecks _
                             Where x.Status = EnumCheckStatus.Cleared _
                             Select x).ToList
                End If
            ElseIf chk_Released.Checked Then
                lstChecks = Me.WBillHelper.GetCheckReleased(rb_Released.Checked)
                If rb_Released.Checked Then
                    lstChecks = (From x In lstChecks _
                             Where x.Status = EnumCheckStatus.Released _
                             Select x).ToList
                End If
            Else
                MsgBox("No Search filters set! Please select a filter and try again", CType(MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, MsgBoxStyle), "No Search Filter")
                Exit Sub
            End If

            lstChecks = Me.FilterChecks(lstChecks)
            frmChecks.lstChecks = lstChecks
            frmChecks.MaxDate = CDate(FormatDateTime(dtp_To.Value, DateFormat.ShortDate))
            frmChecks.MinDate = CDate(FormatDateTime(dtp_From.Value, DateFormat.ShortDate))
            frmChecks.FillDataTable(lstChecks)
            If Me.lstChecks.Count = 0 Then
            Else
                Me.Close()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Function FilterChecks(ByVal lstCheck As List(Of Check)) As List(Of Check)
        Dim fltrList As New List(Of Check)
        'Filter By Date
        If chkbox_EnableDate.Checked Then
            lstCheck = ((From x In lstCheck _
                        Where x.TransactionDate >= CDate(FormatDateTime(dtp_From.Value, DateFormat.ShortDate)) _
                        And x.TransactionDate <= CDate(FormatDateTime(dtp_To.Value, DateFormat.ShortDate)) _
                        Select x).ToList)
        End If

        If chkbox_EnableNumber.Checked Then
            If rb_VoucherNo.Checked Then
                lstCheck = ((From x In lstCheck _
                            Where x.VoucherNumber = Me.txt_Number.Text.ToString _
                            Select x).ToList)
            ElseIf rb_CheckNo.Checked Then
                lstCheck = ((From x In lstCheck _
                            Where x.CheckNumber = Me.txt_Number.Text.ToString _
                            Select x).ToList)
            End If
        End If

        If chkbox_EnablePID.Checked Then
            lstCheck = ((From x In lstCheck _
                        Where x.Participant.ParticipantID = cbo_Participants.SelectedItem.ToString _
                        Select x).ToList)
        End If

        If chkbox_Status.Checked Then
            Dim _tmpList As New List(Of Check)

            If chk_Status1.Checked Then
                _tmpList.AddRange((From x In lstCheck _
                            Where x.Status = EnumCheckStatus.SystemGenerated _
                            Select x).ToList)
            End If

            If chk_Status2.Checked Then
                _tmpList.AddRange((From x In lstCheck _
                            Where x.Status = EnumCheckStatus.GeneratedCheck _
                            Select x).ToList)
            End If

            'If chk_Status3.Checked Then
            '    _tmpList.AddRange((From x In lstCheck _
            '                Where x.Status = EnumCheckStatus.Released _
            '                Select x).ToList)
            'End If

            If chk_Status4.Checked Then
                _tmpList.AddRange((From x In lstCheck _
                            Where x.Status = EnumCheckStatus.Cancelled _
                            Select x).ToList)
            End If

            If chk_Status5.Checked Then
                _tmpList.AddRange((From x In lstCheck _
                            Where x.Status = EnumCheckStatus.ReplacementForCancelled _
                            Select x).ToList)
            End If

            'If chk_Status6.Checked Then
            '    _tmpList.AddRange((From x In lstCheck _
            '                       Where x.Status = EnumCheckStatus.ReplacementForCancelled _
            '                       Select x).ToList)
            'End If

            _tmpList = (From x In _tmpList _
                        Select x Distinct).ToList


            lstCheck = _tmpList

        End If

        If chk_Released.Checked Then
            If rb_Released.Checked Then
                lstCheck = ((From x In lstCheck _
                                  Where x.DateReleased <> "" _
                                  Select x).ToList)
            Else
                lstCheck = ((From x In lstCheck _
                                  Where x.DateReleased = "" _
                                  Select x).ToList)
            End If
        End If
        
        If chk_Cleared.Checked Then
            If rb_Cleared.Checked Then
                lstCheck = ((From x In lstCheck _
                                   Where x.DateCleared <> "" _
                                   Select x).ToList)
            Else
                lstCheck = ((From x In lstCheck _
                                   Where x.DateCleared = "" _
                                   Select x).ToList)
            End If
        End If

        lstCheck = (From x In lstCheck _
                    Select x Distinct).ToList

        Return lstCheck
    End Function


    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub frmCheckSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            WBillHelper = WESMBillHelper.GetInstance()

            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            lstParticipants = (From x In Me.WBillHelper.GetAMParticipantsAll Where x.PaymentType = EnumParticipantPaymentType.Check).ToList

            Me.cbo_Participants.DataSource = lstParticipants.Select(Function(x) x.ParticipantID).ToList
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub chk_Cleared_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_Cleared.CheckedChanged
        If Me.chk_Cleared.Checked Then
            gbox_Cleared.Enabled = True
        Else
            gbox_Cleared.Enabled = False
        End If
    End Sub

    Private Sub chk_Released_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_Released.CheckedChanged
        If Me.chk_Released.Checked Then
            gBox_Released.Enabled = True
        Else
            gBox_Released.Enabled = False
        End If
    End Sub

    Private Sub chk_Status5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_Status5.CheckedChanged

    End Sub
End Class