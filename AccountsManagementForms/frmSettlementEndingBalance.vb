'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmSettlementEndingBalance
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 16, 2014
'Development Group:      Software Development and Support Division
'Description:            GUI for generating the settlement ending balance
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   August 16, 2014         Vladimir E. Espiritu            GUI initialization
'

Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
'Imports LDAPLib
'Imports LDAPLogin

Public Class frmSettlementEndingBalance

    Private WBillHelper As WESMBillHelper
    Private DateNow As Date
    Private Sub frmSettlementEndingBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        DateNow = SystemDate
        'DateNow = Now()

        'If SystemDate.Day < 25 Then
        '    DateNow = DateNow.AddDays((DateNow.Day + 1) * -1)
        'End If

        Dim TransactionDate As Date = New Date(DateNow.Year, DateNow.Month, 1)
        Me.txtInfo.Text = Me.txtInfo.Text & " " & DateNow.ToString("MMMM") & " " & TransactionDate.Year

    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Dim ans As MsgBoxResult
        Dim TransactionDate As Date = New Date(DateNow.Year, DateNow.Month, Date.DaysInMonth(DateNow.Year, DateNow.Month))
        ans = MsgBox("Do you really want to generate the Settment Ending Balance for the month of " & TransactionDate.ToString("MMMM") & " " & TransactionDate.ToString("yyyy") & "?", vbQuestion Or vbYesNo, "Generate")

        If ans = vbNo Then
            Exit Sub
        End If

        If WBillHelper.GetSettlementNoticeEndingBalanceCount(TransactionDate) = 0 Then
            Dim listWESMBillSummary = WBillHelper.GetWESMBillSummaryWithEndingBalance()
            ' If listWESMBillSummary.Count = 0 Then
            'Edited - 20140826 - MsgBox("No Settlement Ending Balance for " & TransactionDate.ToString("MMMM") & " " & TransactionDate.ToString("yyyy") & " were saved!", MsgBoxStyle.Information, "No Data")
            'Get all participants registered in the AM_PARTICIPANTS TABLE per id number
            Dim _allParticipants As New List(Of AMParticipants)
            Dim _distinctIDNumber As New List(Of String)

            _allParticipants = WBillHelper.GetAMParticipants()

            _distinctIDNumber = (From x In _allParticipants _
                                 Select x.IDNumber Distinct _
                                 Order By IDNumber Ascending).ToList

            'Clear the list
            'listWESMBillSummary.Clear()

            For Each itmIDNumber In _distinctIDNumber
                Dim _itmIDNumber = itmIDNumber

                'Get outstanding invocies of participant
                Dim _lstInvoices = (From x In listWESMBillSummary _
                                    Where x.IDNumber.IDNumber = _itmIDNumber _
                                    And x.EndingBalance <> 0 _
                                    Select x).ToList

                If _lstInvoices.Count <> 0 Then
                    ' listWESMBillSummary.AddRange(_lstInvoices)
                Else
                    Dim _tmpWESMSummary As New WESMBillSummary
                    With _tmpWESMSummary
                        .BillPeriod = 0

                        .DueDate = Date.Now
                        .NewDueDate = Date.Now

                        .IDNumber.IDNumber = itmIDNumber
                        .ChargeType = EnumChargeType.E
                        .BeginningBalance = 0
                        .EndingBalance = 0
                        .GroupNo = 0
                        .IDType = "P"
                        .IsMFWTaxDeducted = 1
                        .INVDMCMNo = ""
                        .SummaryType = EnumSummaryType.INV
                        .WESMBillSummaryNo = 0
                        .TransactionDate = TransactionDate
                        .Adjustment = 0

                        listWESMBillSummary.Add(_tmpWESMSummary)
                    End With

                End If
            Next
            'End If

            'Save the invoices to the database for settlement notice
            WBillHelper.SaveSettlementNoticeBeginningBalance(TransactionDate, listWESMBillSummary)
            'Edited - 20140826 - Else
            'Edited - 20140826 - WBillHelper.SaveSettlementNoticeBeginningBalance(TransactionDate, listWESMBillSummary)
            MsgBox("Settlement Ending Balance for " & TransactionDate.ToString("MMMM") & " " & TransactionDate.ToString("yyyy") & " was succesfully saved!", MsgBoxStyle.Information, "Saved!")
            'Edited - 20140826 - End If
        Else
            ans = MsgBox("There are existing records found for " & TransactionDate.ToString("MMMM") & " " & TransactionDate.ToString("yyyy") & ", do you really want to continue?", vbQuestion Or vbYesNo, "Replace")
            If ans = vbYes Then
                Dim listWESMBillSummary = WBillHelper.GetWESMBillSummaryWithEndingBalance()
                'If listWESMBillSummary.Count = 0 Then
                'Edited - 20140826 - MsgBox("No Settlement Ending Balance for " & TransactionDate.ToString("MMMM") & " " & TransactionDate.ToString("yyyy") & " were saved!", MsgBoxStyle.Information, "No Data")

                'Get all participants registered in the AM_PARTICIPANTS TABLE per id number
                Dim _allParticipants As New List(Of AMParticipants)
                Dim _distinctIDNumber As New List(Of String)

                _allParticipants = WBillHelper.GetAMParticipants()

                _distinctIDNumber = (From x In _allParticipants _
                                     Select x.IDNumber Distinct _
                                     Order By IDNumber Ascending).ToList

                'Clear the list
                'listWESMBillSummary.Clear()

                For Each itmIDNumber In _distinctIDNumber
                    Dim _itmIDNumber = itmIDNumber

                    'Get outstanding invocies of participant
                    Dim _lstInvoices = (From x In listWESMBillSummary _
                                        Where x.IDNumber.IDNumber = _itmIDNumber _
                                        And x.EndingBalance <> 0 _
                                        Select x).ToList

                    If _lstInvoices.Count <> 0 Then
                        '  listWESMBillSummary.AddRange(_lstInvoices)
                    Else
                        Dim _tmpWESMSummary As New WESMBillSummary
                        With _tmpWESMSummary
                            .BillPeriod = 0

                            .DueDate = Date.Now
                            .NewDueDate = Date.Now

                            .IDNumber.IDNumber = itmIDNumber
                            .ChargeType = EnumChargeType.E
                            .BeginningBalance = 0
                            .EndingBalance = 0
                            .GroupNo = 0
                            .IDType = "P"
                            .IsMFWTaxDeducted = 1
                            .INVDMCMNo = ""
                            .SummaryType = EnumSummaryType.INV
                            .WESMBillSummaryNo = 0
                            .TransactionDate = TransactionDate
                            .Adjustment = 0

                            listWESMBillSummary.Add(_tmpWESMSummary)
                        End With

                    End If
                Next
                'End If
                'Edited - 20140826 - Else
                'Save the invoices to the database for settlement notice
                WBillHelper.SaveSettlementNoticeBeginningBalance(TransactionDate, listWESMBillSummary)
                MsgBox("Settlement Ending Balance for " & TransactionDate.ToString("MMMM") & " " & TransactionDate.ToString("yyyy") & " was succesfully saved!", MsgBoxStyle.Information, "Saved!")
                'Edited - 20140826 - End If
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class