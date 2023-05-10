'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmSummaryOfMarginCall
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     April 04, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for Summary Of MarginCall
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   April 04, 2012          Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects


Public Class frmSummaryOfMarginCall
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private _ListPrudentialMarginCall As List(Of PrudentialMarginCallSummary)

    Private Sub frmPrudentialMarginCall_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        BFactory = BusinessFactory.GetInstance()

        Me.MdiParent = MainForm

        Me._ListPrudentialMarginCall = New List(Of PrudentialMarginCallSummary)

        Dim frmProg As New frmProgress
        Try

            ProgressThread.Show("Please wait while processing PR Margin Call Report.")
            Dim listDates = WBillHelper.GetPrudentialMarginCallTransactionDates()
            Me.ddlTransactionDate.DataSource = listDates

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ProgressThread.Close()
        End Try

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Me._ListPrudentialMarginCall.Count = 0 Then
            MsgBox("Nothing to print!", MsgBoxStyle.Critical, "No data")
            Exit Sub
        End If

        ProgressThread.Show("Please wait while printing the report.")
        'Get the signatories for Margin Call
        Dim signatory = WBillHelper.GetSignatories("PR_MCS").First()

        Dim dt = BFactory.GeneratePrudentialMarginCallSummaryReport(Me._ListPrudentialMarginCall, signatory, _
                                                                    New DSReport.MarginCallSummaryDataTable)

        Dim frmViewer As New frmReportViewer()
        With frmViewer
            .LoadMarginCallSummary(dt)
            ProgressThread.Close()
            .ShowDialog()
        End With
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.DGridView.Rows.Clear()

        If Me.ddlTransactionDate.Items.Count = 0 Then
            MsgBox("No existing margin call stored in database!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Sub
        End If

        If Me.ddlTransactionDate.SelectedIndex = -1 Then
            MsgBox("Please select first the transaction date!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Sub
        End If

        Me._ListPrudentialMarginCall = WBillHelper.GetPrudentialMarginCallMainSummary(CDate(Me.ddlTransactionDate.Text))

        Dim items = From x In Me._ListPrudentialMarginCall _
                        Select x Order By x.IDNumber.ParticipantID

        For Each item In items
            With item
                Me.DGridView.Rows.Add(.IDNumber.IDNumber, .IDNumber.ParticipantID, .IDNumber.FullName, _
                                      .MaximumNetExposure * -1D, .TradingLimit, .MarginCallAmount)
            End With
        Next
    End Sub

End Class