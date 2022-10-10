'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmNSSRASummary
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 08, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for the generation of NSSRA Report
'Arguments/Parameters:  
'Files/Database Tables:  frmNSSRASummary
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   March 03, 2013          Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmNSSRASummary
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private _ListBillingPeriod As List(Of CalendarBillingPeriod)
    Private _ListOfParticipants As List(Of AMParticipants)

    Private Sub frmNSSRASummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MdiParent = MainForm

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        BFactory = BusinessFactory.GetInstance()

        Me._ListBillingPeriod = WBillHelper.GetBillingPeriodsWithNSS()
        Me._ListOfParticipants = WBillHelper.GetAMParticipantsAll()

        For Each item In Me._ListBillingPeriod
            Me.ddlMonth.Items.Add(item.BillingPeriod.ToString() & "-(" & item.StartDate.ToString("MM/dd/yyyy") & " to " & _
                                   item.EndDate.ToString("MM/dd/yyyy") & ")")
        Next

    End Sub

    Private Sub btnShowReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowReport.Click
        If Me.ddlMonth.SelectedIndex = -1 Then
            MsgBox("Please specify the billing period!", MsgBoxStyle.Critical, "Specify the inputs")
            Exit Sub
        End If
        Try            
            Dim result As New List(Of NetSettlementSurplusAdjustmentReport)

            Dim selectedBP = Me._ListBillingPeriod(Me.ddlMonth.SelectedIndex)

            'Get the WESM Invoices
            Dim listWESMInvoices = WBillHelper.GetWESMInvoicesForNSSAndNSSRA(selectedBP.BillingPeriod)

            If listWESMInvoices.Count = 0 Then
                MsgBox("No record found!", MsgBoxStyle.Information, "No data")
                Exit Sub
            End If

            '08/27/2013 Vloody
            Dim listItems = (From x In Me._ListOfParticipants Join y In listWESMInvoices _
                             On x.IDNumber Equals CStr(y.IDNumber) _
                             Select y, x.ParticipantID, x.FullName Order By ParticipantID).ToList()


            Dim dt As New DSReport.NSSRASummaryDataTable
            For Each item In listItems
                Dim row = dt.NewRow()
                With item
                    row("BILLING_PERIOD_VALUE") = Me.ddlMonth.Text
                    row("ID_NUMBER") = CLng(.y.IDNumber)
                    row("PARTICIPANT_ID") = .ParticipantID
                    row("FULL_NAME") = .FullName
                    row("INVOICE_NO") = .y.InvoiceNumber
                    row("INVOICE_DATE") = .y.InvoiceDate.ToString("MM/dd/yyyy")
                    row("AMOUNT") = .y.Amount
                End With
                dt.Rows.Add(row)
            Next

            Dim frmViewer As New frmReportViewer()
            With frmViewer
                frmViewer.LoadNSSRASummary(dt)                
                .ShowDialog()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error found.")        
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ddlMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlMonth.SelectedIndexChanged

    End Sub
End Class