'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmPrudentialComputeMarginCall
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     January 07, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for the generation of margin call
'Arguments/Parameters:  
'Files/Database Tables:  frmPrudentialComputeMarginCall
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   January 07, 2013        Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmPrudentialComputeMarginCall
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory

    Private _ListPrudentialMarginCall As List(Of PrudentialMarginCall)

    Private Sub frmPrudentialComputeMarginCall_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        BFactory = BusinessFactory.GetInstance()

        Me._ListPrudentialMarginCall = New List(Of PrudentialMarginCall)
    End Sub

    Private Sub btnMarginCall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarginCall.Click

        Dim ans = MsgBox("Do you really want to generate margin call?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Generate")
        If ans = MsgBoxResult.No Then
            Exit Sub
        End If

        Try
            Me.DGridView.Rows.Clear()

            Dim selectedBP As New CalendarBillingPeriod
            Dim MarginCallDate As Date

            Dim TransactionDate As Date = CDate(FormatDateTime(SystemDate, DateFormat.ShortDate))
            'Dim TransactionDate As Date = CDate(FormatDateTime(Now(), DateFormat.ShortDate))

            'Search the billing period for the billing date
            Dim listBP = WBillHelper.GetCalendarBP(TransactionDate)

            If listBP.Count = 0 Then
                Throw New ApplicationException("No billing period for " & TransactionDate.ToString("MM/dd/yyyy"))
            End If

            'Get the WESM Bills
            selectedBP = listBP.First()

            'Get the DueDate
            Dim frmDueDate As New frmPrudentialComputeMarginCallSearch
            With frmDueDate
                .LoadType = frmPrudentialComputeMarginCallSearch.EnumMarginCallSearch.SelectMarginCallDate
                If .ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    Exit Sub
                Else
                    MarginCallDate = CDate(FormatDateTime(.dtMarginCallDate.Value, DateFormat.ShortDate))
                End If
            End With

            'Check if Transaction Date < Margin Call Date
            If TransactionDate >= MarginCallDate Then
                MsgBox("Transaction date should be less than margin call date!", MsgBoxStyle.Critical, "Invalid Margin Call Date")
                Exit Sub
            End If

            'Compute the Margin Call
            Me.GenerateMarginCall(TransactionDate, MarginCallDate, selectedBP)

            If Me._ListPrudentialMarginCall.Count = 0 Then
                MsgBox("No records that were generated!", MsgBoxStyle.Information, "No data")
                Exit Sub
            End If

            Dim items = From x In Me._ListPrudentialMarginCall _
                        Select x Order By x.IDNumber.ParticipantID

            For Each item In items
                With item
                    Me.DGridView.Rows.Add(FormatDateTime(.TransactionDate, DateFormat.ShortDate), _
                                          FormatDateTime(.MarginCallDate, DateFormat.ShortDate), .IDNumber.ParticipantID, _
                                          FormatNumber(.MaximumNetExposure * -1D, 2), _
                                          FormatNumber(.TradingLimit, 2), FormatNumber(.MarginCallAmount, 2), _
                                          .Status.ToString())
                End With
            Next

            ProgressThread.Show("Please wait while saving Margin Call.")
            WBillHelper.SavePrudentialMarginCallMain(TransactionDate, selectedBP.StartDate, _
                                                     selectedBP.EndDate, Me._ListPrudentialMarginCall)
            ProgressThread.Close()
            MsgBox("Successfully computed the margin call!", MsgBoxStyle.Information, "Completed")
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Me._ListPrudentialMarginCall.Count = 0 Then
            MsgBox("Nothing to print!", MsgBoxStyle.Critical, "No data")
            Exit Sub
        End If

        Try
            ProgressThread.Show("Please wait while pringting.")
            'Get the signatories for Margin Call
            Dim signatory = WBillHelper.GetSignatories("PR_MC").First()

            'Get the list of Calendar Billing Period
            Dim listBP = WBillHelper.GetCalendarBP()

            Dim dt = BFactory.GeneratePrudentialMarginCall(Me._ListPrudentialMarginCall, listBP, signatory, _
                                                           New DSReport.MarginCallDataTable)

            Dim frmViewer As New frmReportViewer()
            With frmViewer
                .LoadMarginCall(dt)
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click        
        Try
            'Get the DueDate
            Dim frmSearch As New frmPrudentialComputeMarginCallSearch
            With frmSearch
                .LoadType = frmPrudentialComputeMarginCallSearch.EnumMarginCallSearch.SearchMarginCall
                If .ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If
                ProgressThread.Show("Please wait while processing.")
                If .IDNumber = "0" Then
                    Me._ListPrudentialMarginCall = WBillHelper.GetPrudentialMarginCallMain(CDate(.ddlTransDate.Text))
                Else
                    Me._ListPrudentialMarginCall = WBillHelper.GetPrudentialMarginCallMain(.IDNumber, CDate(.ddlTransDate.Text))
                End If
            End With

            Me.DGridView.Rows.Clear()
            Dim items = From x In Me._ListPrudentialMarginCall _
                            Select x Order By x.IDNumber.ParticipantID

            For Each item In items
                With item
                    Me.DGridView.Rows.Add(FormatDateTime(.TransactionDate, DateFormat.ShortDate), _
                                          FormatDateTime(.MarginCallDate, DateFormat.ShortDate), .IDNumber.ParticipantID, _
                                          FormatNumber(.MaximumNetExposure * -1D, 2), _
                                          FormatNumber(.TradingLimit, 2), FormatNumber(.MarginCallAmount, 2), _
                                          .Status.ToString())
                End With
            Next
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Methods/Functions"
    Private Sub GenerateMarginCall(ByVal TransactionDate As Date, ByVal MarginCallDate As Date, _
                                   ByVal itemBillingPeriod As CalendarBillingPeriod)

        Try
            'Get the prudentials
            Dim listPrudentials = WBillHelper.GetParticipantsPrudential()

            'Get the parent and child mapping
            Dim listMapping = WBillHelper.GetParentChildMappingPerBillingPeriodForParent(itemBillingPeriod.BillingPeriod - 1)

            If listMapping.Count = 0 Then
                Throw New ApplicationException("No records for parent and child mapping for " & itemBillingPeriod.BillingPeriod - 1 & " " & _
                                               "billing period.")
            End If

            'Get the participants
            Dim listParticipants = WBillHelper.GetAMParticipants()
            If listParticipants.Count = 0 Then
                Throw New ApplicationException("No records for participants.")
            End If

            'Get the unallocated collections
            Dim listCollections = WBillHelper.GetCollections(itemBillingPeriod.StartDate, _
                                                             itemBillingPeriod.EndDate, True)

            Dim listMappingResult = From x In listMapping Join y In listParticipants _
                                    On x.PCNumber Equals y.IDNumber _
                                    Select y

            'Reset the list
            Me._ListPrudentialMarginCall = New List(Of PrudentialMarginCall)

            For Each item In listMappingResult
                Dim selectedItem = item

                Dim itemMarginCall As New PrudentialMarginCall
                With itemMarginCall
                    .IDNumber = selectedItem

                    .TransactionDate = TransactionDate
                    .MarginCallDate = MarginCallDate
                    .Status = EnumMarginCallStatus.Final

                    .AdvancePayment = (From x In listCollections _
                                       Where x.IDNumber = selectedItem.IDNumber _
                                       Select x.CollectedAmount * -1D).Sum()

                    .OutstandingBalance = WBillHelper.GetOutstandingBalance(itemBillingPeriod.StartDate, selectedItem.IDNumber)


                    .PrudentialDeposit = (From x In listPrudentials _
                                          Where x.IDNumber = selectedItem.IDNumber _
                                          Select x.PrudentialAmount).Sum()

                    .PrudentialInterest = (From x In listPrudentials _
                                           Where x.IDNumber = selectedItem.IDNumber _
                                           Select x.InterestAmount).Sum()

                    'Get all the current invoice
                    .ListOfWESMBills = WBillHelper.GetPrudentialMarginCallWESMBills(selectedItem.IDNumber, itemBillingPeriod.BillingPeriod - 1, _
                                                                                    itemBillingPeriod.StartDate, itemBillingPeriod.EndDate)

                    'Get all the previous margin call
                    .ListOfPreviousWESMBill = WBillHelper.GetPrudentialMarginCallWESMBills(selectedItem.IDNumber, _
                                                                                           itemBillingPeriod.StartDate.AddMonths(-11), _
                                                                                           itemBillingPeriod.EndDate.AddMonths(-1))

                    If .MarginCallAmount > 0 Then
                        Me._ListPrudentialMarginCall.Add(itemMarginCall)
                    End If

                    Dim cnt = (From x In Me._ListPrudentialMarginCall _
                               Where x.ListOfPreviousWESMBill.Count = 0 And x.ListOfWESMBills.Count = 0 _
                               Select x).Count

                    If cnt = Me._ListPrudentialMarginCall.Count Then
                        Throw New ApplicationException("There is no WESM Bill uploaded for the current billing period and no computed margin call for the previous month!")
                    End If

                End With
            Next
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub
#End Region

End Class