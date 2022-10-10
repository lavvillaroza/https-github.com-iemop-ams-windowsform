'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmBillingCalendar
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     December 08, 2011
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
'   December 08, 2011       Vladimir E. Espiritu            GUI initialization
'

Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmBillingCalendar
    Private WBillHelper As WESMBillHelper


    Private Sub frmBillingCalendar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MdiParent = MainForm

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        Me.LoadBillingPeriod()
    End Sub

    Private Sub LoadBillingPeriod()
        Try
            Dim listBPs = WBillHelper.GetCalendarBP()

            Dim listBPsDesc = (From x In listBPs Select x Order By x.BillingPeriod Descending).ToList()


            Me.DGridViewMain.DataSource = listBPsDesc

            With Me.DGridViewMain
                .Columns(0).Width = 100
                .Columns(1).Width = 150
                .Columns(2).Width = 150
                .Columns(3).Width = 150
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.LoadBillingPeriod()

        MsgBox("Successfully Refreshed!", MsgBoxStyle.Information, "Done")
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim frm As New frmBillingCalendarMgt

        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.LoadBillingPeriod()
        End If
    End Sub
End Class