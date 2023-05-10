'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmMarketFeesSummary
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     May 23, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for Market Fees Summary report
'Arguments/Parameters:  
'Files/Database Tables:  AM_COLLECTION_ALLOCATION, AM_WESMBILL_SUMMARY
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   May 23, 2012            Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmMarketFeesSummary
    Private WBillHelper As WESMBillHelper
    Private Itemsignatories As DocSignatories
    Private ListMFSummary As List(Of MarketFeesSummary)

    Private Sub frmMarketFeesSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        'Get the signatories for Summary of Market Fees
        Me.Itemsignatories = WBillHelper.GetSignatories("MF").First()

        Me.ListMFSummary = New List(Of MarketFeesSummary)
        Me.rbAllocation.Select()
    End Sub


    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Dim dateFrom = CDate(FormatDateTime(Me.dtFrom.Value, DateFormat.ShortDate))
        Dim dateTo = CDate(FormatDateTime(Me.dtTo.Value, DateFormat.ShortDate))

        Me.DGridView.Rows.Clear()

        If dateFrom > dateTo Then
            MsgBox("Invalid date range!", MsgBoxStyle.Critical, "Invalid")

            Me.DGridView.Rows.Clear()
            Me.ListMFSummary = New List(Of MarketFeesSummary)
            Exit Sub
        End If

        Try
            'Get the MF Summary in Collection
            Me.ListMFSummary = WBillHelper.GetMarketFeesSummary(dateFrom, dateTo, If(Me.rbAllocation.Checked, 1, 2))

            If ListMFSummary.Count = 0 Then
                MsgBox("No records found!", MsgBoxStyle.Information, "No data")
                Exit Sub
            End If

            For Each item In ListMFSummary
                With item
                    Me.DGridView.Rows.Insert(0, .IDNumber.IDNumber, .IDNumber.ParticipantID, .INVDMCMNo.ToString(), _
                                             .MarketFees, .VATOnMarketFees, .WithholdingVATOnMarketFees, _
                                             .WithholdingTAXOnMarketFees, .Totals, _
                                             .DefaultInterestOnMarketFees, .DefaultInterestOnVATOnMarketFees, _
                                             .DefaultInterestOnWithholdingTAX + .DefaultInterestOnWithholdingVAT, _
                                             .TotalDefaultInterest, .GrandTotal, .CollectionPaymentTransactionType)
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "System Error Message")            
        End Try        
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim dt As DataTable

        dt = Me.GetSummary()

        If dt.Rows.Count = 0 Then
            MsgBox("Nothing to generate!", MsgBoxStyle.Exclamation, "No data")
            Exit Sub
        End If
        Dim frmViewer As New frmReportViewer()
        With frmViewer
            .LoadMarketFeesSummary(dt)            
            .ShowDialog()
        End With
    End Sub

    Private Sub DGridView_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DGridView.RowsAdded
        If CInt(Me.DGridView.Rows(e.RowIndex).Cells("colTransType").Value) = EnumCollectionPaymentTransactionType.Payment Then
            Me.DGridView.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Yellow
            Application.DoEvents()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#Region "Methods/Functions"
    Private Function GetSummary() As DataTable
        Dim result As New DSReport.MarketFeesSummaryDataTable

        For Each item In Me.ListMFSummary
            With item
                Dim row = result.NewRow()
                row("ID_NUMBER") = .IDNumber.IDNumber
                row("PARTICIPANT_ID") = .IDNumber.ParticipantID
                row("INVOICE_NO") = .INVDMCMNo.ToString()
                row("MARKET_FEES") = .MarketFees
                row("VAT_ON_MF") = .VATOnMarketFees
                row("WVAT_ON_MF") = .WithholdingVATOnMarketFees
                row("WTAX_ON_MF") = .WithholdingTAXOnMarketFees
                row("TOTAL") = .Totals
                row("DI_ON_MF") = .DefaultInterestOnMarketFees
                row("DI_ON_VAT") = .DefaultInterestOnVATOnMarketFees
                row("DI_ON_WITHHOLD") = .DefaultInterestOnWithholdingVAT + .DefaultInterestOnWithholdingTAX
                row("TOTAL_DI") = .TotalDefaultInterest
                row("GRAND_TOTAL") = .GrandTotal
                row("REMARKS") = ""
                row("SIGNATORY1") = AMModule.FullName
                row("POSITION1") = AMModule.Position
                row("SIGNATORY2") = Me.Itemsignatories.Signatory_1
                row("POSITION2") = Me.Itemsignatories.Position_1
                row("SIGNATORY3") = Me.Itemsignatories.Signatory_2
                row("POSITION3") = Me.Itemsignatories.Position_2

                result.Rows.Add(row)
            End With
        Next
        result.AcceptChanges()

        Return result
    End Function
#End Region

End Class