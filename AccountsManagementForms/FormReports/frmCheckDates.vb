'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmCheckDates
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     February 4, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for Updating the Check's Date released and date Cleared
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

Public Class frmCheckDates
    Private WbillHelper As WESMBillHelper
    Public _lstCheckForUpdate As List(Of Check)
    Public _isRelease As Boolean = False

    Private Sub cmd_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_OK.Click
        Try

        
            For Each itmCheck In _lstCheckForUpdate

                If CDate(FormatDateTime(itmCheck.TransactionDate, DateFormat.ShortDate)) > CDate(FormatDateTime(dtp_DateUpdate.Value, DateFormat.ShortDate)) Then
                    'Value being set to Date Released/Date Cleared of Check is Less than the transaction date
                    If _isRelease = True Then
                        MsgBox("Invalid Entry found, Transaction date should be less than or equal to Release Date", MsgBoxStyle.Critical, "Error!")
                    Else
                        MsgBox("Invalid Entry found, Transaction date should be less than or equal to Cleared Date", MsgBoxStyle.Critical, "Error!")
                    End If
                    Exit Sub
                End If

                If itmCheck.DateReleased <> "" Then
                    If CDate(FormatDateTime(CDate(itmCheck.DateReleased), DateFormat.ShortDate)) > CDate(FormatDateTime(dtp_DateUpdate.Value, DateFormat.ShortDate)) Then
                        If _isRelease = False Then
                            MsgBox("Invalid Entry found, Release date date should be less than or equal to Cleared Date", MsgBoxStyle.Critical, "Error!")
                            Exit Sub
                        End If
                    End If
                End If


                If _isRelease = True Then
                    itmCheck.DateReleased = FormatDateTime(dtp_DateUpdate.Value, DateFormat.ShortDate)
                Else
                    itmCheck.DateCleared = FormatDateTime(dtp_DateUpdate.Value, DateFormat.ShortDate)
                End If
            Next

            'Confirm Update of Dates
            Dim ans As New MsgBoxResult
            If _isRelease = True Then
                ans = MsgBox("Do you really want to change the Date Released to " & FormatDateTime(dtp_DateUpdate.Value, DateFormat.ShortDate) & "?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Information, MsgBoxStyle), "Update Dates")
            Else
                ans = MsgBox("Do you really want to change the Date Cleared to " & FormatDateTime(dtp_DateUpdate.Value, DateFormat.ShortDate) & "?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Information, MsgBoxStyle), "Update Dates")
            End If

            If ans = MsgBoxResult.Yes Then
                Me.WbillHelper.UpdateCheckReleaseCleared(_lstCheckForUpdate, _isRelease)
                For Each itmCheck In _lstCheckForUpdate
                    With itmCheck
                        If _isRelease = True Then
                            .Status = EnumCheckStatus.Released
                        Else
                            .Status = EnumCheckStatus.Cleared
                        End If
                    End With
                Next
                frmChecks.isCancelled = False
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Cancel.Click
        frmChecks.isCancelled = True
        Me.Close()
    End Sub

    Private Sub frmCheckDates_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WbillHelper = WESMBillHelper.GetInstance()

        WbillHelper.ConnectionString = AMModule.ConnectionString
        WbillHelper.UserName = AMModule.UserName
    End Sub
End Class