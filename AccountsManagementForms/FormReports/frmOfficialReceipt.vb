'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmOfficialReceipt
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     March 08, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI for the generation of OR
'Arguments/Parameters:  
'Files/Database Tables:  frmOfficialReceipt
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   March 08, 2013          Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmOfficialReceipt
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private _ListParticipants As List(Of AMParticipants)

    Private Sub frmOfficialReceipt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        BFactory = BusinessFactory.GetInstance()

        Me._ListParticipants = New List(Of AMParticipants)
        Me.chkboxHeader.Checked = True
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim startDate = CDate(FormatDateTime(Me.dtFrom.Value, DateFormat.ShortDate))
        Dim EndDate = CDate(FormatDateTime(Me.dtTo.Value, DateFormat.ShortDate))

        If startDate > EndDate Then
            MsgBox("Invalid date range!", MsgBoxStyle.Critical, "Specify input")
            Me.dtTo.Select()
            Exit Sub
        End If

        Dim listOR = WBillHelper.GetOfficialReceipts(startDate, EndDate)
        Me._ListParticipants = WBillHelper.GetAMParticipantsAll()

        Dim listItems = (From x In listOR Join y In Me._ListParticipants _
                         On x.IDNumber Equals y.IDNumber _
                         Select x, y.ParticipantID Order By x.ORNo).ToList()

        If listOR.Count = 0 Then
            MsgBox("No record found!", MsgBoxStyle.Information, "No data")
            Exit Sub
        End If

        Me.DGridView.Rows.Clear()

        For Each item In listItems
            With item
                Me.DGridView.Rows.Add(.x.ORNo, BFactory.GenerateBIRDocumentNumber(.x.ORNo, BIRDocumentsType.OfficialReceipt), .x.ORDate.ToString("MM/dd/yyyy"), _
                                      .x.IDNumber.ToString(), .ParticipantID, FormatNumber(.x.Amount, 2), _
                                      .x.TransactionType, .x.TransactionType.ToString())

            End With
        Next
    End Sub

    Private Sub DGridView_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridView.CellContentClick
        If e.ColumnIndex = 1 Then

            Try
                Dim ORNo = CLng(Me.DGridView.CurrentRow.Cells("colORNo").Value)

                Dim result As New DataTable

                Dim itemORReportMain As New OfficialReceiptReportMain
                 With itemORReportMain
                    .ItemOfficialReceipt = WBillHelper.GetOfficialReceipt(ORNo)
                    .ListOfficialReceiptReportRawDetails = WBillHelper.GetOfficialReceiptRawDetails(ORNo)
                    .ItemParticipant = WBillHelper.GetAMParticipants(.ItemOfficialReceipt.IDNumber).First()
                    .BIRPermitNumber = AMModule.BIRPermitNumber
                    .TotalPaymentInWords = BFactory.NumberConvert(.TotalPayment)

                    'Viewing OR from Replenishment Window
                    If .ItemOfficialReceipt.TransactionType = EnumORTransactionType.Replenishment Then
                        Dim item As New OfficialReceiptReportRawDetailsNew
                        item.TransactionType = EnumORTransactionType.Replenishment
                        item.Amount = itemORReportMain.ItemOfficialReceipt.Amount
                        item.DocumentNo = "Replenishment"
                        .ListOfficialReceiptReportRawDetails.Add(item)
                    End If

                    If .ItemOfficialReceipt.TransactionType = EnumORTransactionType.PaymentEnergy Or .ItemOfficialReceipt.TransactionType = EnumORTransactionType.PaymentEnergyAndVAT Or _
                        .ItemOfficialReceipt.TransactionType = EnumORTransactionType.PaymentMarketFees Or .ItemOfficialReceipt.TransactionType = EnumORTransactionType.PaymentVAT Then

                        'Get the WESM Bill Sales and Purchases
                        Dim listInvoiceNo As New List(Of String)
                        For Each item In .ListOfficialReceiptReportRawDetails
                            If item.DocumentType = EnumDocumentType.INV Then
                                listInvoiceNo.Add(item.DocumentNo)
                            End If
                        Next
                        listInvoiceNo.TrimExcess()

                        Dim listWESMBillSalesAndPurchases As New List(Of WESMBillSalesAndPurchased)
                        If listInvoiceNo.Count > 0 Then
                            listWESMBillSalesAndPurchases = WBillHelper.GetWESMInvoiceSalesAndPurchased(listInvoiceNo)
                        End If

                        'Update the Official Receipt Main Category Total
                        BFactory.GeneratePaymentORMain(.ItemOfficialReceipt, .ListOfficialReceiptReportRawDetails, listWESMBillSalesAndPurchases)
                    End If

                    Try
                        .DefaultInterestRate = WBillHelper.GetDailyInterestRate()(itemORReportMain.ItemOfficialReceipt.ORDate)
                    Catch ex1 As KeyNotFoundException
                        .DefaultInterestRate = 0
                    Catch ex As Exception
                        Throw New ApplicationException(ex.Message)
                    End Try
                End With

                Dim itemParticipant = (From x In Me._ListParticipants Where x.IDNumber = itemORReportMain.ItemParticipant.IDNumber _
                                       Select x).First()

                Dim PEMCHeader As Integer = EnumHeaderType.No

                If chkboxHeader.Checked = True Then
                    PEMCHeader = EnumHeaderType.Yes
                End If

                result = BFactory.GenerateOfficialReceiptReport(PEMCHeader, itemORReportMain, itemParticipant, New DSReport.OfficialReceiptMainNewDataTable)
                If itemORReportMain.ItemOfficialReceipt.Status = 0 Then
                    Dim frmViewer As New frmReportViewer()
                    With frmViewer
                        .LoadORCancelled(result, WBillHelper.GetSystemDateTime)
                        .printingOR = True
                        .ShowDialog()
                    End With
                Else
                    Dim frmViewer As New frmReportViewer()
                    With frmViewer
                        .LoadOR(result)
                        .printingOR = True
                        .ShowDialog()
                    End With
                End If                
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                'Updated By Lance 08/19/2014
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepOfficialReceiptWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInPrinting.ToString, AMModule.UserName)
            End Try
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class