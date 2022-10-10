'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmPrudentialDrawdown
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 08, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for the generation of Drawdown Report
'Arguments/Parameters:  
'Files/Database Tables:  frmPrudentialDrawdown
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   February 07, 2013       Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmPrudentialDrawdown
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private _ListDrawdown As New List(Of PrudentialDrawdown)

    Private Sub frmPrudentialDrawdown_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        BFactory = BusinessFactory.GetInstance()

        Me._ListDrawdown = New List(Of PrudentialDrawdown)
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim listDates = WBillHelper.GetPrudentialDrawdownDates(False)
        Dim transDate As Date

        If listDates.Count = 0 Then
            MsgBox("No drawdown to generate!", MsgBoxStyle.Critical, "No data")
            Exit Sub
        End If

        Dim frm As New frmPrudentialDrawdownSearch
        With frm
            .ListOfTransDate = listDates
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                transDate = CDate(.ddlTransDate.Text)
            Else
                Exit Sub
            End If
        End With

        Dim ans As MsgBoxResult

        Dim cnt = WBillHelper.GetPrudentialDrawdown(transDate, True).Count()
        If cnt = 0 Then
            ans = MsgBox("Do you really want generate?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Save")
        Else
            ans = MsgBox("There are already existing records, " & vbCrLf _
                         & "Do you want to replace the existing data?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
        End If

        If ans = MsgBoxResult.No Then
            Exit Sub
        End If

        Me._ListDrawdown = WBillHelper.GetPrudentialDrawdown(transDate, False)
        Me.LoadRecords()

        WBillHelper.SavePrudentialDrawdown(transDate, Me._ListDrawdown)

        MsgBox("Successfully generated and saved!", MsgBoxStyle.Information, "Saved and Generated")

    End Sub

    Private Sub LoadRecords()
        Me.DGridView.Rows.Clear()

        For Each item In Me._ListDrawdown
            With item
                Me.DGridView.Rows.Add(.TransactionDate.ToString("MM/dd/yyyy"), .DueDate.ToString("MM/dd/yyyy"), _
                                      .IDNumber.IDNumber, .IDNumber.ParticipantID, .IDNumber.FullName, _
                                       FormatNumber(.DrawdownAmount * -1D, 2), FormatNumber(.RemaningPrudential, 2))
            End With
        Next
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim listDates = WBillHelper.GetPrudentialDrawdownDates(True)
        Dim transDate As Date

        If listDates.Count = 0 Then
            MsgBox("No existing records!", MsgBoxStyle.Critical, "No data")
            Exit Sub
        End If

        Dim frm As New frmPrudentialDrawdownSearch
        With frm
            .ListOfTransDate = listDates
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                transDate = CDate(.ddlTransDate.Text)
            Else
                Exit Sub
            End If
        End With

        Me._ListDrawdown = WBillHelper.GetPrudentialDrawdown(transDate, True)
        Me.LoadRecords()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Me.DGridView.RowCount = 0 Then
            MsgBox("Nothing to print!", MsgBoxStyle.Critical, "No data")
            Exit Sub
        End If
        'Get the signatories for Margin Call
        Dim signatory = WBillHelper.GetSignatories("PR_DN").First()

        Dim dt = BFactory.GeneratePrudentialDrawdownReport(Me._ListDrawdown, signatory, _
                                                           New DSReport.DrawdownDataTable)

        Dim frmViewer As New frmReportViewer()
        With frmViewer
            .LoadDrawdown(dt)
            .ShowDialog()
        End With
    End Sub
End Class