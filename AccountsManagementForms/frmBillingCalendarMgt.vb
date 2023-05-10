'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmBillingCalendarMgt
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 11, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for viewing of Billing Calendar
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   August 11, 2012         Vladimir E. Espiritu            GUI initialization
'

Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmBillingCalendarMgt
    Private WBillHelper As WESMBillHelper
    Dim dicMonth As Dictionary(Of Integer, String)

    Private Sub frmBillingCalendarMgt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        dicMonth = New Dictionary(Of Integer, String)
        For index As Integer = 1 To 12
            dicMonth.Add(index, New Date(Now.Year, index, 1).ToString("MMMM"))
        Next

        'Get the billing period
        Dim listBP = WBillHelper.GetCalendarBP()

        'Display in From fields the latest billing period.
        'In case there is no records, get the year for today and display in From fields
        If listBP.Count = 0 Then
            Me.txtYearFrom.Text = "2006"
            Me.txtMonthFrom.Text = "May"
        Else
            Dim lastBP = (From x In listBP Select x Order By x.BillingDate Descending).First()

            Me.txtYearFrom.Text = lastBP.BillingDate.Year.ToString()
            Me.txtMonthFrom.Text = dicMonth(lastBP.BillingDate.Month)
        End If

        For index As Integer = 0 To 10
            'If Month From is December, display the next year
            If Me.txtMonthFrom.Text = dicMonth(12) Then
                Me.ddlYearTo.Items.Add(New Date(CInt(Me.txtYearFrom.Text), 1, 1).AddYears(index + 1).Year)
            Else
                Me.ddlYearTo.Items.Add(New Date(CInt(Me.txtYearFrom.Text), 1, 1).AddYears(index).Year)
            End If
        Next

        For Each item In dicMonth.Values
            Me.ddlMonthTo.Items.Add(item)
        Next

        'Set the default values of To fields
        For index As Integer = 1 To 12
            If dicMonth(index) = Me.txtMonthFrom.Text Then
                Dim dateDefault = New Date(CInt(Me.txtYearFrom.Text), index, 1)
                dateDefault = dateDefault.AddMonths(1)

                Me.ddlYearTo.Text = dateDefault.Year.ToString()
                Me.ddlMonthTo.Text = dicMonth(dateDefault.Month)
                Exit For
            End If
        Next

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'Get the billing period
            Dim listBP = WBillHelper.GetCalendarBP()
            Dim dateTo As Date
            Dim lastBP As CalendarBillingPeriod

            'Display in From fields the latest billing period.
            'In case there is no records, get the year for today and display in From fields
            If listBP.Count = 0 Then
                lastBP = New CalendarBillingPeriod(-1, New Date(2006, 4, 26), New Date(2006, 5, 25), New Date(2006, 5, 1))
            Else
                lastBP = (From x In listBP Select x Order By x.BillingDate Descending).First()
            End If

            For index As Integer = 1 To 12
                If dicMonth(index) = Me.ddlMonthTo.Text Then
                    dateTo = New Date(CInt(Me.ddlYearTo.Text), index, 1)
                    Exit For
                End If
            Next

            If dateTo <= lastBP.BillingDate Then
                MsgBox("Invalid date range!", MsgBoxStyle.Critical, "Invalid")
                Me.ddlMonthTo.Select()
                Exit Sub
            End If

            Dim listNewBP As New List(Of CalendarBillingPeriod)
            Dim cnt As Integer = 1
            Do While dateTo >= lastBP.BillingDate.AddMonths(cnt)
                listNewBP.Add(New CalendarBillingPeriod(lastBP.BillingPeriod + cnt, lastBP.StartDate.AddMonths(cnt), _
                                                    lastBP.EndDate.AddMonths(cnt), lastBP.BillingDate.AddMonths(cnt)))
                cnt += 1
            Loop

            Dim ans = MsgBox("Do you really want to save the records?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            'Saved
            WBillHelper.SaveCalendarBPs(listNewBP)

            'Updated By Lance 08/17/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibCalendarBillingWindow.ToString, "Added Billing Period From: " & listNewBP.Min(Function(x As CalendarBillingPeriod) x.BillingPeriod) & " to " & listNewBP.Max(Function(x As CalendarBillingPeriod) x.BillingPeriod), "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)

            MsgBox("Successfully Saved", MsgBoxStyle.Information, "Saved")

            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class