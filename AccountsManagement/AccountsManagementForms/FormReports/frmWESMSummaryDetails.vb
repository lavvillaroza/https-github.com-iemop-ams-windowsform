'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmWESMBillSummaryDetails
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 6, 2013
'Development Group:      Software Development and Support Division
'Description:            
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description


Option Strict On
Option Explicit On

Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmWESMSummaryDetails

    Dim WbillHelper As WESMBillHelper

    Private _WESMSummary As WESMBillSummary
    Public Property WESMSummary() As WESMBillSummary
        Get
            Return _WESMSummary
        End Get
        Set(ByVal value As WESMBillSummary)
            _WESMSummary = value
        End Set
    End Property


    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub frmWESMSummaryDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            WbillHelper = WESMBillHelper.GetInstance()
            WbillHelper.ConnectionString = AMModule.ConnectionString

            'Add Table Columns for Offset table
            Dim srcOffsetTable As New DataTable
            With srcOffsetTable.Columns
                .Add("Transaction Date")
                .Add("DMCM Number")
                .Add("Invoice Number")
                .Add("Participant ID")
                .Add("Debit")
                .Add("Credit")
            End With
            srcOffsetTable.AcceptChanges()

            'Add Table Columns for Collection table
            Dim srcCollectionTable As New DataTable
            With srcCollectionTable.Columns
                .Add("OR Number")
                .Add("Allocation Date")
                '.Add("Invoice Number")
                .Add("DMCM Number")
                .Add("Transaction Type")
                .Add("Amount")
            End With
            srcCollectionTable.AcceptChanges()

            Dim srcPaymentTable As New DataTable
            With srcPaymentTable.Columns
                .Add("Payment Batch Code")
                .Add("Allocation Date")
                .Add("OR Number")
                .Add("DMCM Number")
                .Add("Transaction Type")
                .Add("Amount")
            End With
            srcPaymentTable.AcceptChanges()

            Me.LoadSummaryDetails()
            Me.dgv_OffsettingTransaction.DataSource = Me.DisplayOffsettingDetails(srcOffsetTable)
            Me.dgv_CollectionTransactions.DataSource = Me.DisplayCollectionDetails(srcCollectionTable)
            Me.dgv_PaymentTransactions.DataSource = Me.DisplayPaymentDetails(srcPaymentTable)

            If Me.dgv_OffsettingTransaction.Rows.Count = 0 Then
                Me.cmd_GenOffsetRPT.Enabled = False
            Else
                Me.cmd_GenOffsetRPT.Enabled = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

#Region "Load WESM BILL SUMMARY Details to text boxes"
    Private Sub LoadSummaryDetails()
        With WESMSummary
            Me.txt_BegBalance.Text = FormatNumber(.BeginningBalance, 2, TriState.True, TriState.True, TriState.True)
            Me.txt_BillPeriod.Text = .BillPeriod.ToString
            Me.txt_ChargeType.Text = .ChargeType.ToString
            Me.txt_DueDate.Text = FormatDateTime(.DueDate, DateFormat.ShortDate)
            Me.txt_EWtax.Text = FormatNumber(.EnergyWithhold, 2, TriState.True, TriState.True)
            Me.txt_IDNumber.Text = .IDNumber.IDNumber.ToString
            Me.txt_NewDueDate.Text = FormatDateTime(.NewDueDate, DateFormat.ShortDate)
            Me.txt_OutstandingBal.Text = FormatNumber(.EndingBalance, 2, TriState.True, TriState.True)
            Me.txt_ParticipantID.Text = .IDNumber.ParticipantID
            Me.txt_RefNo.Text = .INVDMCMNo
            Me.txt_TransDate.Text = FormatDateTime(.TransactionDate, DateFormat.ShortDate)

            Dim IsAdjustment As String = "No"
            If .Adjustment = 1 Then
                IsAdjustment = "Yes"
            End If
            Me.txt_isAdjustment.Text = IsAdjustment

        End With
    End Sub
#End Region

    Private Function DisplayOffsettingDetails(ByVal OffsetTable As DataTable) As DataTable
        'Get Offsetting details of WESMBill Summary
        Dim OffsetDetails As New List(Of OffsetP2PC2CDetails)

        OffsetDetails = WbillHelper.GetWESMBillOffsetDetailsPerItem(WESMSummary.WESMBillSummaryNo.ToString)

        If OffsetDetails.Count = 0 Then
            Return OffsetTable
        End If

        For Each itmDetails In OffsetDetails
            Dim dr As DataRow
            dr = OffsetTable.NewRow

            dr("Transaction Date") = FormatDateTime(itmDetails.UpdatedDate, DateFormat.ShortDate)
            dr("DMCM Number") = EnumSummaryType.DMCM.ToString & "-" & itmDetails.DMCMNumber
            dr("Invoice Number") =  itmDetails.InvoiceNumber
            dr("Debit") = itmDetails.Debit
            dr("Credit") = itmDetails.Credit

            OffsetTable.Rows.Add(dr)
            OffsetTable.AcceptChanges()
        Next

        Return OffsetTable
    End Function

    Private Function DisplayCollectionDetails(ByVal retCollection As DataTable) As DataTable
        Dim CollectionDetails As New List(Of CollectionAllocation)
        CollectionDetails = WbillHelper.GetCollectionAllocationPerWESMBillNo(WESMSummary.WESMBillSummaryNo)

        If CollectionDetails.Count = 0 Then
            Return retCollection
        End If

        'Get Distinct Collection Number/s
        Dim lstCollection As New List(Of Collection)
        Dim lstCollectionNumber = (From x In CollectionDetails _
                                   Select x.CollectionNumber Distinct).ToList
        lstCollection = WbillHelper.GetCollections(lstCollectionNumber)

        For Each itmCollectionDetails In CollectionDetails
            Dim dr As DataRow

            Dim DMCMNo As String = "-None-"
            Dim OrNumber As String = "-None-"
            Dim _itmCollectionDetails = itmCollectionDetails
            Dim _itmColDetails = (From x In lstCollection _
                                  Where x.CollectionNumber = _itmCollectionDetails.CollectionNumber _
                                  Select x).FirstOrDefault
            dr = retCollection.NewRow

            If _itmColDetails.ORNo <> 0 Then
                OrNumber = "OR-" & _itmColDetails.ORNo
            Else
                dr("DMCM Number") = "DMCM-" & _itmColDetails.DMCMNumber
            End If
            dr("OR Number") = OrNumber

            dr("Allocation Date") = FormatDateTime(_itmColDetails.AllocationDate, DateFormat.ShortDate)
            'dr("Invoice Number") = WESMSummary.SummaryType.ToString & "-" & WESMSummary.INVDMCMNo

            'If _itmCollectionDetails.DMCMNumber <> 0 Then
            '    DMCMNo = EnumSummaryType.DMCM.ToString & "-" & _itmCollectionDetails.DMCMNumber
            'End If
            'dr("DMCM Number") = DMCMNo
            dr("Transaction Type") = _itmCollectionDetails.CollectionType.ToString

            dr("Amount") = FormatNumber(_itmCollectionDetails.Amount, 2, TriState.True, TriState.True)

            retCollection.Rows.Add(dr)
            retCollection.AcceptChanges()
        Next

        Return retCollection
    End Function

    Private Function DisplayPaymentDetails(ByVal retPaymentDetails As DataTable) As DataTable
        Dim PaymentPerAccount As New List(Of PaymentAllocationAccount)
        PaymentPerAccount = WbillHelper.GetPaymentAllocationAccountPerWESMNo(WESMSummary.WESMBillSummaryNo)

        If PaymentPerAccount.Count = 0 Then
            Return retPaymentDetails
        End If

        'Get Per Participant Allocation
        Dim PaymentPerParticipant As New List(Of PaymentAllocationParticipant)
        PaymentPerParticipant = WbillHelper.GetPaymentAllocationParticipant((From x In PaymentPerAccount _
                                                                             Select x.PaymentBatchCode Distinct).ToList)

        'Get BatchCode for Whole Transaction
        Dim PaymentPerBillPeriod As New List(Of Payment)
        PaymentPerBillPeriod = WbillHelper.GetPayment((From x In PaymentPerAccount _
                                                       Select x.PaymentBatchCode Distinct).ToList)

        For Each itmPayment In PaymentPerAccount
            Dim dr As DataRow
            dr = retPaymentDetails.NewRow
            
            Dim _itmPayment = itmPayment
            Dim _PerBPBatchCode As String = ""
            _PerBPBatchCode = _itmPayment.PaymentBatchCode
            Dim BatchCodeAllocDate = (From x In PaymentPerBillPeriod _
                                      Where x.PaymentPerBPNo = _PerBPBatchCode _
                                      Select x.PaymentBatchCode, x.PaymentAllocationDate).FirstOrDefault

            dr("Payment Batch Code") = BatchCodeAllocDate.PaymentBatchCode
            dr("Allocation Date") = FormatDateTime(BatchCodeAllocDate.PaymentAllocationDate, DateFormat.ShortDate)
            If _itmPayment.DMCMNo = 0 Then
                dr("DMCM Number") = "-None-"
            Else
                dr("DMCM Number") = EnumSummaryType.DMCM.ToString & "-" & _itmPayment.DMCMNo
            End If

            If _itmPayment.ORNumber = 0 Then
                dr("OR Number") = "-None-"
            Else
                dr("OR Number") = "OR-" & _itmPayment.ORNumber
            End If

            dr("Transaction Type") = _itmPayment.PaymentType.ToString
            dr("Amount") = FormatNumber(_itmPayment.PaymentAmount, 2, TriState.True, TriState.True)

            retPaymentDetails.Rows.Add(dr)
            retPaymentDetails.AcceptChanges()
        Next


        Return retPaymentDetails
    End Function

    Private Sub cmd_GenOffsetRPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenOffsetRPT.Click
        
        Dim frmReport As New frmReportViewer

        Dim OffsettingDetails As List(Of OffsetP2PC2CDetails)
        OffsettingDetails = WbillHelper.GetWESMBillOffsetDetailsPerItem(WESMSummary.WESMBillSummaryNo.ToString)

        Dim GetlstInvoices As List(Of WESMBill)
        GetlstInvoices = WbillHelper.GetWESMBills((From x In OffsettingDetails _
                                                   Select x.InvoiceNumber).ToList)

        Dim ForReport As New DataSet
        ForReport = GenerateSummaryReport(WESMSummary, GetlstInvoices, OffsettingDetails, True)

        With frmReport
            .LoadWESMBillReportSummary(ForReport, ConvertChargeType(WESMSummary.ChargeType), WESMSummary.IDNumber.ParticipantID, FormatDateTime(WESMSummary.DueDate, DateFormat.ShortDate), WESMSummary.BillPeriod.ToString)
            .ShowDialog()
        End With

    End Sub
End Class