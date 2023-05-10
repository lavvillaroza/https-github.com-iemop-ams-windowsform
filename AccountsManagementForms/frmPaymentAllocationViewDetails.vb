'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmPaymentAllocationMgt
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     April 13, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for viewing of Payment Allocation > BP > Participant > Summary Details
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'

Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects


Public Class frmPaymentAllocationViewDetails
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private _lstParticipantSummary As List(Of PaymentAllocationAccount)
    Private _lstParticipantAllocation As List(Of PaymentAllocationParticipant)
    Private _lstParticipantDMCM As List(Of DebitCreditMemo)
    Private _lstAccountingCodes As List(Of AccountingCode)
    Private _lstOfficialReceipt As List(Of OfficialReceiptMain)
    Private _ParticipantInfo As AMParticipants
    Private _dicTransactionType As Dictionary(Of EnumPaymentType, String)
    Private _AllocationDate As Date
    Private _ParticipantPR As Decimal
    Private _ExcessCollection As Decimal
    Private _PaymentBatch As String


    Public Sub ShowPaymentDetails(ByVal ParticipantAllocation As List(Of PaymentAllocationParticipant), ByVal ParticipantSummary As List(Of PaymentAllocationAccount), _
                                  ByVal DMCMList As List(Of DebitCreditMemo), ByVal AccountCodes As List(Of AccountingCode), ByVal ListOfficialReceipt As List(Of OfficialReceiptMain), _
                                  ByVal Participant As AMParticipants, ByVal AllocationDate As Date, ByVal dicPaymentTransaction As Dictionary(Of EnumPaymentType, String), _
                                  ByVal ParticipantPR As Decimal, ByVal ExcessCollection As Decimal, ByVal IDNumber As String, ByVal ParticipantID As String, _
                                  ByVal PaymentBatchCode As String)

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.UserName = AMModule.UserName
        BFactory = BusinessFactory.GetInstance()

        'Me.dgridView.DataSource = ParticipantSummary
        Me.Text &= IDNumber & " - " & ParticipantID

        _lstParticipantSummary = ParticipantSummary
        _lstParticipantAllocation = ParticipantAllocation
        _lstParticipantDMCM = DMCMList
        _lstAccountingCodes = AccountCodes
        _lstOfficialReceipt = ListOfficialReceipt
        _ParticipantInfo = Participant
        _AllocationDate = AllocationDate
        _dicTransactionType = dicPaymentTransaction
        _ParticipantPR = ParticipantPR
        _ExcessCollection = ExcessCollection
        _PaymentBatch = PaymentBatchCode

        Dim dt As New DataTable

        dt.TableName = "PaymentAllocationParticipant"

        With dt.Columns
            .Add("INV/DMCMNo")
            .Add("BillingPeriod")
            .Add("DueDate")
            .Add("ChargeType")
            .Add("Created OR")
            .Add("Created DMCM")
            .Add("PaymentType")
            ' .Add("BeginningBalance")
            .Add("PaymentAmount")
            '  .Add("EndingBalance")
        End With

        ParticipantSummary = (From x In ParticipantSummary _
                             Select x).ToList

        For Each item In ParticipantSummary
            Dim dr As DataRow
            dr = dt.NewRow
            With item
                If .WESMBillSummary.INVDMCMNo <> "" Then
                    If .WESMBillSummary.SummaryType = EnumSummaryType.INV Then
                        dr("INV/DMCMNo") = .WESMBillSummary.INVDMCMNo
                    Else
                        dr("INV/DMCMNo") = Me.BFactory.GenerateBIRDocumentNumber(CLng(.WESMBillSummary.INVDMCMNo), BIRDocumentsType.DMCM)
                    End If

                    dr("BillingPeriod") = .WESMBillSummary.BillPeriod
                    dr("DueDate") = .WESMBillSummary.DueDate.ToString("M/dd/yyyy")
                    dr("ChargeType") = IIf(.WESMBillSummary.ChargeType = EnumChargeType.E, "Energy", _
                                       IIf(.WESMBillSummary.ChargeType = EnumChargeType.EV, "VAT on Energy", _
                                          IIf(.WESMBillSummary.ChargeType = EnumChargeType.MF, "Market Fees", "VAT on Market Fees")))
                Else
                    dr("BillingPeriod") = "- None -"
                    dr("INV/DMCMNo") = "- None -"
                    dr("DueDate") = SystemDate.ToString("M/dd/yyyy")
                    dr("ChargeType") = "- None -"
                End If

                'Created Document
                If .DMCMNo = 0 Then
                    dr("Created DMCM") = "None"
                Else
                    dr("Created DMCM") = Me.BFactory.GenerateBIRDocumentNumber(.DMCMNo, BIRDocumentsType.DMCM)
                End If

                If .ORNumber = 0 Then
                    dr("Created OR") = "None"
                Else
                    dr("Created OR") = Me.BFactory.GenerateBIRDocumentNumber(.ORNumber, BIRDocumentsType.OfficialReceipt)
                End If

                dr("PaymentType") = .PaymentType
                '  dr("BeginningBalance") = FormatNumber(.BeginningBalance, 2, TriState.True, TriState.True)
                dr("PaymentAmount") = FormatNumber(.PaymentAmount, 2, TriState.True, TriState.True)
                '  dr("EndingBalance") = FormatNumber(.EndingBalance, 2, TriState.True, TriState.True)
            End With
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next

        If _ExcessCollection <> 0 Then
            Dim dr As DataRow
            dr = dt.NewRow
            dr("BillingPeriod") = "- None -"
            dr("INV/DMCMNo") = "- None -"
            dr("DueDate") = SystemDate.ToString("M/dd/yyyy")
            dr("ChargeType") = "- None -"
            dr("Created DMCM") = "None"
            dr("Created OR") = "None"
            dr("PaymentType") = EnumPaymentType.ReturnToParticipant.ToString
            dr("PaymentAmount") = FormatNumber(_ExcessCollection, 2, TriState.True, TriState.True)
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        End If

        With dgridView
            .DataSource = dt
            .EditMode = DataGridViewEditMode.EditProgrammatically

            .Columns("PaymentAmount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            ' .Columns("BeginningBalance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            ' .Columns("EndingBalance").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        End With

        Me.ComputeTotals()

    End Sub

    Private Sub cmd_DownloadCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_DownloadCSV.Click
        'subExportDGVToCSV
        'Me.MdiParent = MainForm
        Try
            Dim saveFileDialogBox As New SaveFileDialog
            With saveFileDialogBox
                .Title = "Save WESM Bill"
                .Filter = "CSV Files (*.csv)|*.csv"
                If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                    WBillHelper.subExportDGVToCSV(.FileName, dgridView, True)
                    MsgBox("Successfully Downloaded!", MsgBoxStyle.Information, "Done")
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub ComputeTotals()

        '*****************************************************
        '               Compute for AP
        '*****************************************************

        txt_APTotalMF.Text = (From x In _lstParticipantSummary _
                         Where (x.PaymentType = EnumPaymentType.PaymentMF _
                         Or x.PaymentType = EnumPaymentType.PaymentMFV _
                         Or x.PaymentType = EnumPaymentType.UnpaidMF _
                         Or x.PaymentType = EnumPaymentType.UnpaidMFV _
                         Or x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                         Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault) _
                         And x.PaymentAmount > 0 _
                         Select x.PaymentAmount).Sum.ToString("#,##0.00")

        txt_APMF.Text = (From x In _lstParticipantSummary _
                         Where (x.PaymentType = EnumPaymentType.PaymentMF _
                         Or x.PaymentType = EnumPaymentType.PaymentMFV _
                         Or x.PaymentType = EnumPaymentType.UnpaidMF _
                         Or x.PaymentType = EnumPaymentType.UnpaidMFV) _
                         And x.PaymentAmount > 0 _
                         Select x.PaymentAmount).Sum.ToString("#,##0.00")

        txt_APDefaultInterestMF.Text = (From x In _lstParticipantSummary _
                                        Where (x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                                        Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault) _
                                        And x.PaymentAmount > 0 _
                                        Select x.PaymentAmount).Sum.ToString("#,##0.00")

        txt_DeferredApplied.Text = (From x In _lstParticipantSummary _
                                    Where x.PaymentType = EnumPaymentType.DeferredAppliedVAT _
                                    Or x.PaymentType = EnumPaymentType.DeferredAppliedEnergy _
                                    Select x.PaymentAmount).Sum.ToString("#,##0.00")

        txt_APTotalEnergy.Text = (From x In _lstParticipantSummary _
                             Where x.PaymentType = EnumPaymentType.PaymentEnergy _
                             Or x.PaymentType = EnumPaymentType.DefaultAllocation _
                             Select x.PaymentAmount).Sum.ToString("#,##0.00") 'Or x.PaymentType = EnumPaymentType.OffsetAllocatedEnergy _

        txt_APEnergy.Text = (From x In _lstParticipantSummary _
                             Where x.PaymentType = EnumPaymentType.PaymentEnergy _
                             Select x.PaymentAmount).Sum.ToString("#,##0.00")

        txt_APTotalVATEnergy.Text = (From x In _lstParticipantSummary _
                                     Where x.PaymentType = EnumPaymentType.PaymentEnergyVAT _
                                     Select x.PaymentAmount).Sum.ToString("#,##0.00") 'Or x.PaymentType = EnumPaymentType.OffsetAllocatedVAT _

        txt_APOffsetAllocatedEnergy.Text = (From x In _lstParticipantSummary _
                                            Where x.PaymentType = EnumPaymentType.OffsetAllocatedEnergy _
                                            Select x.PaymentAmount).Sum.ToString("#,##0.00")

        txt_APOffsetAllocatedVATEnergy.Text = (From x In _lstParticipantSummary _
                                              Where x.PaymentType = EnumPaymentType.OffsetAllocatedVAT _
                                              Select x.PaymentAmount).Sum.ToString("#,##0.00")

        txt_APTotalDefaultEnergy.Text = (From x In _lstParticipantSummary _
                                         Where x.PaymentType = EnumPaymentType.DefaultAllocation _
                                         Select x.PaymentAmount).Sum.ToString("#,##0.00")

        If txt_APEnergy.Text = txt_APOffsetAllocatedEnergy.Text Then
            txt_TotalAP.Text = FormatNumber(CStr(CDec(txt_TotalAP.Text) - CDec(txt_APOffsetAllocatedEnergy.Text)), 2, TriState.True, TriState.True)
            txt_APTotalEnergy.Text = FormatNumber(CStr((CDec(txt_APTotalEnergy.Text) - CDec(txt_APOffsetAllocatedEnergy.Text)).ToString("#,##0.00")), 2, TriState.True, TriState.True)
        End If

        If txt_APOffsetAllocatedVATEnergy.Text = txt_APTotalVATEnergy.Text Then
            txt_TotalAP.Text = FormatNumber(CStr(CDec(txt_TotalAP.Text) - CDec(txt_APOffsetAllocatedVATEnergy.Text)), 2, TriState.True, TriState.True)
            txt_APTotalVATEnergy.Text = FormatNumber(CStr(CDec(txt_APTotalVATEnergy.Text) - CDec(txt_APOffsetAllocatedVATEnergy.Text)), 2, TriState.True, TriState.True)
        End If

        Dim _Total = (From x In _lstParticipantSummary _
                            Where (x.PaymentType = EnumPaymentType.PaymentMF _
                            Or x.PaymentType = EnumPaymentType.PaymentMFV _
                            Or x.PaymentType = EnumPaymentType.UnpaidMF _
                            Or x.PaymentType = EnumPaymentType.UnpaidMFV _
                            Or x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                            Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault) _
                            And x.PaymentAmount > 0 _
                            Or x.PaymentType = EnumPaymentType.PaymentEnergy _
                            Or x.PaymentType = EnumPaymentType.PaymentEnergyVAT _
                            Or x.PaymentType = EnumPaymentType.DefaultAllocation _
                            Or x.PaymentType = EnumPaymentType.DeferredAppliedEnergy _
                            Or x.PaymentType = EnumPaymentType.DeferredAppliedVAT _
                            Select x.PaymentAmount).Sum

        txt_TotalAP.Text = FormatNumber(CStr((CDec(txt_TotalAP.Text) + _Total).ToString("#,##0.00")), 2, TriState.True, TriState.True)

        '*****************************************************
        '               Compute for AR
        '*****************************************************

        txt_ARWHTax.Text = FormatNumber((From x In _lstParticipantSummary _
                            Where x.PaymentType = EnumPaymentType.UnpaidMFWHTax _
                            Or x.PaymentType = EnumPaymentType.UnpaidMFWHVAT _
                            Or x.PaymentType = EnumPaymentType.WHTaxDefault _
                            Or x.PaymentType = EnumPaymentType.WHVATDefault _
                            Select x.PaymentAmount).Sum.ToString("#,##0.00"), 2, TriState.True, TriState.True)

        txt_ARtotalMF.Text = FormatNumber((From x In _lstParticipantSummary _
                              Where (x.PaymentType = EnumPaymentType.UnpaidMF _
                              Or x.PaymentType = EnumPaymentType.UnpaidMFV _
                              Or x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                              Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault) _
                              And x.PaymentAmount < 0 _
                              Select x.PaymentAmount).Sum.ToString("#,##0.00"), 2, TriState.True, TriState.True)

        txt_ARTotalDefaultMF.Text = FormatNumber((From x In _lstParticipantSummary _
                                     Where (x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                                     Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault) _
                                     And x.PaymentAmount < 0 _
                                     Select x.PaymentAmount).Sum.ToString("#,##0.00"), 2, TriState.True, TriState.True)

        txt_ARMF.Text = FormatNumber((From x In _lstParticipantSummary _
                         Where (x.PaymentType = EnumPaymentType.UnpaidMF _
                         Or x.PaymentType = EnumPaymentType.UnpaidMFV) _
                         And x.PaymentAmount < 0 _
                         Select x.PaymentAmount).Sum.ToString("#,##0.00"), 2, TriState.True, TriState.True)

        txt_AREnergy.Text = FormatNumber((From x In _lstParticipantSummary _
                                  Where x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergy _
                                  Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergy _
                                  Select x.PaymentAmount).Sum.ToString("#,##0.00"), 2, TriState.True, TriState.True)

        txt_ARTotalDIEnergy.Text = FormatNumber((From x In _lstParticipantSummary _
                                    Where x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergyDefault _
                                    Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergyDefault _
                                    Select x.PaymentAmount).Sum.ToString("#,##0.00"), 2, TriState.True, TriState.True)

        txt_ARVATEnergy.Text = FormatNumber((From x In _lstParticipantSummary _
                                Where x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableVAT _
                                Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableVAT _
                                Select x.PaymentAmount).Sum.ToString("#,##0.00"), 2, TriState.True, TriState.True)

        txt_AROffsetAMTEnergy.Text = FormatNumber((From x In _lstParticipantSummary _
                                 Where x.PaymentType = EnumPaymentType.OffsetAmountEnergy _
                                 Select x.PaymentAmount).Sum.ToString("#,##0.00"), 2, TriState.True, TriState.True)

        txt_AROffsetAMTVAT.Text = FormatNumber((From x In _lstParticipantSummary _
                                   Where x.PaymentType = EnumPaymentType.OffsetAmountVATEnergy _
                                   Select x.PaymentAmount).Sum.ToString("#,##0.00"), 2, TriState.True, TriState.True)

        txt_TotalAREnergy.Text = FormatNumber((From x In _lstParticipantSummary _
                                  Where x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergy _
                                  Or x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergyDefault _
                                  Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergy _
                                  Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergyDefault _
                                  Or x.PaymentType = EnumPaymentType.OffsetAmountEnergy _
                                  Select x.PaymentAmount).Sum.ToString("#,##0.00"), 2, TriState.True, TriState.True)
        Dim TotalAmount As Decimal = 0
        TotalAmount = (From x In _lstParticipantSummary _
                          Where (x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergy _
                          Or x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableEnergyDefault _
                          Or x.PaymentType = EnumPaymentType.OffsetOfPreviousReceivableVAT _
                          Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergy _
                          Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableEnergyDefault _
                          Or x.PaymentType = EnumPaymentType.OffsetToCurrentReceivableVAT _
                          Or x.PaymentType = EnumPaymentType.OffsetAmountEnergy _
                          Or x.PaymentType = EnumPaymentType.OffsetAmountVATEnergy _
                          Or x.PaymentType = EnumPaymentType.UnpaidMF _
                          Or x.PaymentType = EnumPaymentType.UnpaidMFV _
                          Or x.PaymentType = EnumPaymentType.UnpaidMFDefault _
                          Or x.PaymentType = EnumPaymentType.UnpaidMFVDefault) _
                          And x.PaymentAmount < 0 _
                          Select x.PaymentAmount).Sum

        txt_totalAR.Text = FormatNumber(CDec(TotalAmount) + CDec(Me.txt_ARWHTax.Text), 2, TriState.True, TriState.True)
    End Sub

    Private Sub cmd_viewDMCM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_viewDMCM.Click

        If _lstParticipantDMCM.Count = 0 Then
            MsgBox("No DMCM Found for Participant!", MsgBoxStyle.Exclamation, "No DMCM found")
            Exit Sub
        End If

        Dim frmProg As New frmProgress
        frmProg.Show()

        Dim dt As New DSReport.DebitCreditMemoDataTable
        Dim docSig As New DocSignatories
        docSig = WBillHelper.GetSignatories("DMCM").FirstOrDefault

        For Each itmDMCMHeader In _lstParticipantDMCM
            For Each itmDetails In itmDMCMHeader.DMCMDetails
                Dim dr As DataRow
                dr = dt.NewRow

                Dim _itmDetail = itmDetails

                'get Participant Details
                Dim getParticipant = (From x In _lstParticipantSummary _
                                      Select x.WESMBillSummary.IDNumber).FirstOrDefault

                Dim getDescription = (From x In _lstAccountingCodes _
                                      Where x.AccountCode = _itmDetail.AccountCode _
                                      Select x.Description).FirstOrDefault

                With itmDetails
                    dr("ID_NUMBER") = getParticipant.IDNumber & " / " & getParticipant.ParticipantID
                    dr("PARTICIPANT_TIN") = getParticipant.TIN
                    dr("ADDRESS") = getParticipant.ParticipantAddress
                    dr("PARTICULARS") = itmDMCMHeader.Particulars
                    dr("DMCM_NO") = Me.BFactory.GenerateBIRDocumentNumber(.DMCMNumber, BIRDocumentsType.DMCM)
                    dr("ACCOUNT_CODE") = .AccountCode

                    If .InvDMCMNo <> "0" Then
                        dr("DESCRIPTION") = "(" & .InvDMCMNo & ") " & getDescription
                    Else
                        dr("DESCRIPTION") = getDescription
                    End If

                    dr("VAT") = itmDMCMHeader.VAT
                    dr("VATABLE") = itmDMCMHeader.Vatable
                    dr("VAT_EXEMPT_SALE") = itmDMCMHeader.VATExempt
                    dr("VAT_ZERO") = itmDMCMHeader.VatZeroRated
                    dr("TOTAL_AMOUNT_DUE") = itmDMCMHeader.TotalAmountDue

                    dr("EWT") = itmDMCMHeader.EWT
                    dr("EWV") = itmDMCMHeader.EWV

                    dr("PREPARED_BY") = itmDMCMHeader.PreparedBy
                    If Len(Trim(itmDMCMHeader.PreparedBy.ToString)) <> 0 Then
                        dr("POSITION1") = docSig.Position_1
                    End If

                    dr("CHECKED_BY") = itmDMCMHeader.CheckedBy
                    If Len(Trim(itmDMCMHeader.CheckedBy.ToString)) <> 0 Then
                        dr("POSITION2") = docSig.Position_2
                    End If

                    dr("APPROVED_BY") = itmDMCMHeader.ApprovedBy
                    If Len(Trim(itmDMCMHeader.ApprovedBy.ToString)) <> 0 Then
                        dr("POSITION3") = docSig.Position_3
                    End If

                    dr("PARTICIPANT_NAME") = getParticipant.FullName
                    dr("DR_AMOUNT") = .Debit
                    dr("CR_AMOUNT") = .Credit
                    dr("JV_NO") = itmDMCMHeader.JVNumber
                    dr("TOTAL_AMOUNT_DUE") = itmDMCMHeader.TotalAmountDue
                    dr("PARTICIPANT_ID") = getParticipant.ParticipantID
                End With
                dt.Rows.Add(dr)
                dt.AcceptChanges()
            Next
        Next

        Dim frmViewer As New frmReportViewer()
        With frmViewer
            .LoadDebitCreditMemo(dt)
            frmProg.Close()
            .ShowDialog()
        End With
    End Sub

    Private Sub cmd_viewOR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_viewOR.Click
        Dim dt As New DSReport.OfficialReceiptMainNewDataTable
        Dim _BusinessFactory As New BusinessFactory
        _BusinessFactory = BusinessFactory.GetInstance()
        Dim listParseValues As Integer() = {80, 80, 80, 80}

        If _lstOfficialReceipt.Count = 0 Then
            MsgBox("No OR found for participant.", MsgBoxStyle.Exclamation, "No OR found")
            Exit Sub
        End If

        For Each itmOr In Me._lstOfficialReceipt

            Dim itemORReportMain As New OfficialReceiptReportMain
            With itemORReportMain
                .ItemOfficialReceipt = itmOr
                If frmPayment.isViewing = True Then
                    .ListOfficialReceiptReportRawDetails = WBillHelper.GetOfficialReceiptRawDetails(itmOr.ORNo)
                End If
                .ItemParticipant = WBillHelper.GetAMParticipants(.ItemOfficialReceipt.IDNumber).First()
                .DefaultInterestRate = WBillHelper.GetDailyInterestRate()(Me._AllocationDate)
                .BIRPermitNumber = AMModule.ReportBIR
                .TotalPaymentInWords = _BusinessFactory.NumberConvert(.TotalPayment)
            End With
            Dim ORPEMC_Header As Integer = EnumHeaderType.Yes
            dt.Merge(_BusinessFactory.GenerateOfficialReceiptReport(ORPEMC_Header, itemORReportMain, itemORReportMain.ItemParticipant, New DSReport.OfficialReceiptMainNewDataTable))
        Next

        Dim frmViewer As New frmReportViewer
        With frmViewer
            .LoadOR(dt)
            frmProgress.Close()
            .ShowDialog()
        End With
    End Sub

    Private Sub cmd_ToPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ToPDF.Click
        Dim PaySummaryReport As New RPTPaymentSummary
        Dim ds As New DataSet
        Dim dtreport As New DSReport.CollectionSummaryDataTable

        Dim _Signatories As New DocSignatories
        _Signatories = WBillHelper.GetSignatories("PaymentAlloc").FirstOrDefault


        Dim curTransaction = (From x In _lstParticipantSummary _
                              Select x Order By x.WESMBillSummary.WESMBillSummaryNo Ascending).ToList

        'Get WESM Invoice Numbers
        Dim InvoiceTransaction = (From x In curTransaction _
                                  Select x.WESMBillSummary.INVDMCMNo Distinct).ToList

        For Each itmInvoice In InvoiceTransaction
            Dim _itmInvoice = itmInvoice
            Dim lstTransaction = (From x In curTransaction _
                               Where x.WESMBillSummary.INVDMCMNo = _itmInvoice _
                               Select x).ToList

            Dim lstDMCMNo = (From x In lstTransaction _
                             Where x.DMCMNo <> 0 _
                             Select x.DMCMNo Distinct).ToList

            Dim lstPayments = (From x In lstTransaction _
                               Where x.DMCMNo = 0 _
                               Select x.WESMBillSummary.INVDMCMNo Distinct).ToList

            'For Transactions without DMCM
            For Each itmPayment In lstPayments
                Dim dtRow = dtreport.NewRow
                Dim _itmPayment = itmPayment
                Dim TransactionType = (From x In lstTransaction _
                                       Where x.DMCMNo = 0 _
                                       And x.WESMBillSummary.INVDMCMNo = _itmPayment _
                                       Select x.PaymentType).FirstOrDefault

                dtRow(dtreport.AllocationDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)

                Dim PayAmount As Decimal = 0
                PayAmount = (From x In lstTransaction _
                             Where x.DMCMNo = 0 _
                             Select x.PaymentAmount).Sum
                dtRow(dtreport.AmountAppliedColumn) = PayAmount
                dtRow(dtreport.AmountColumn) = PayAmount

                Dim BaseAmount = (From x In lstTransaction _
                                  Where (x.WESMBillSummary.ChargeType = EnumChargeType.E _
                                  Or x.WESMBillSummary.ChargeType = EnumChargeType.MF) _
                                  And x.DMCMNo = 0 _
                                  Select x.PaymentAmount).Sum

                Dim VATAmount = (From x In lstTransaction _
                                  Where (x.WESMBillSummary.ChargeType = EnumChargeType.EV _
                                  Or x.WESMBillSummary.ChargeType = EnumChargeType.MFV) _
                                  And x.DMCMNo = 0 _
                                  Select x.PaymentAmount).Sum

                If TransactionType = EnumPaymentType.DeferredAppliedEnergy Or TransactionType = EnumPaymentType.DeferredAppliedVAT Then
                    BaseAmount = (From x In lstTransaction _
                                  Where x.PaymentType = EnumPaymentType.DeferredAppliedEnergy _
                                  Select x.PaymentAmount).Sum

                    VATAmount = (From x In lstTransaction _
                                 Where x.PaymentType = EnumPaymentType.DeferredAppliedVAT _
                                 Select x.PaymentAmount).Sum
                End If


                dtRow(dtreport.BASE_AMOUNTColumn) = BaseAmount
                dtRow(dtreport.VAT_AMOUNTColumn) = VATAmount

                dtRow(dtreport.BatchCodeColumn) = curTransaction.FirstOrDefault.PaymentBatchCode
                dtRow(dtreport.CollectionDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)


                dtRow(dtreport.TypeColumn) = _dicTransactionType(TransactionType)

                dtRow(dtreport.Position1Column) = _Signatories.Position_1
                dtRow(dtreport.Position2Column) = _Signatories.Position_2
                dtRow(dtreport.Position3Column) = _Signatories.Position_3

                dtRow(dtreport.Signatory1Column) = _Signatories.Signatory_1
                dtRow(dtreport.Signatory2Column) = _Signatories.Signatory_2
                dtRow(dtreport.Signatory3Column) = _Signatories.Signatory_3

                dtRow(dtreport.DocDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)
                dtRow(dtreport.IDNumberColumn) = _ParticipantInfo.IDNumber
                dtRow(dtreport.ParticipantIDColumn) = _ParticipantInfo.ParticipantID

                'Reference Document
                If itmPayment = "" Then
                    dtRow(dtreport.DocumentNoColumn) = ""
                    dtRow(dtreport.DueDateColumn) = ""
                Else
                    'get Summary Type
                    Dim Summary As New WESMBillSummary
                    Summary = (From x In curTransaction _
                                Where x.WESMBillSummary.INVDMCMNo = _itmInvoice _
                                Select x.WESMBillSummary).FirstOrDefault
                    If Summary.SummaryType = EnumSummaryType.INV Then
                        dtRow(dtreport.DocumentNoColumn) = Summary.INVDMCMNo
                    Else
                        dtRow(dtreport.DocumentNoColumn) = Summary.SummaryType.ToString & "-" & Summary.INVDMCMNo
                    End If

                    dtRow(dtreport.DueDateColumn) = FormatDateTime(Summary.DueDate, DateFormat.ShortDate)
                End If

                dtreport.Rows.Add(dtRow)
                dtreport.AcceptChanges()
            Next

            'For Transactions with DMCM
            For Each itmDMCM In lstDMCMNo
                Dim dtRow = dtreport.NewRow
                Dim _itmDMCM = itmDMCM
                Dim TransactionType = (From x In lstTransaction _
                                       Where x.DMCMNo = _itmDMCM _
                                       Select x.PaymentType).FirstOrDefault

                dtRow(dtreport.AllocationDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)


                Dim PayAmount As Decimal = 0
                PayAmount = (From x In lstTransaction _
                             Where x.DMCMNo = _itmDMCM _
                             Select x.PaymentAmount).Sum
                dtRow(dtreport.AmountAppliedColumn) = PayAmount
                dtRow(dtreport.AmountColumn) = PayAmount

                Dim BaseAmount = (From x In lstTransaction _
                                  Where (x.WESMBillSummary.ChargeType = EnumChargeType.E _
                                  Or x.WESMBillSummary.ChargeType = EnumChargeType.MF) _
                                   And x.DMCMNo = _itmDMCM _
                                  Select x.PaymentAmount).Sum

                Dim VATAmount = (From x In lstTransaction _
                                  Where (x.WESMBillSummary.ChargeType = EnumChargeType.EV _
                                  Or x.WESMBillSummary.ChargeType = EnumChargeType.MFV) _
                                  And x.DMCMNo = _itmDMCM _
                                  Select x.PaymentAmount).Sum

                dtRow(dtreport.BASE_AMOUNTColumn) = BaseAmount
                dtRow(dtreport.VAT_AMOUNTColumn) = VATAmount

                dtRow(dtreport.BatchCodeColumn) = curTransaction.FirstOrDefault.PaymentBatchCode
                dtRow(dtreport.CollectionDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)


                dtRow(dtreport.TypeColumn) = _dicTransactionType(TransactionType)

                dtRow(dtreport.Position1Column) = _Signatories.Position_1
                dtRow(dtreport.Position2Column) = _Signatories.Position_2
                dtRow(dtreport.Position3Column) = _Signatories.Position_3

                dtRow(dtreport.Signatory1Column) = _Signatories.Signatory_1
                dtRow(dtreport.Signatory2Column) = _Signatories.Signatory_2
                dtRow(dtreport.Signatory3Column) = _Signatories.Signatory_3

                dtRow(dtreport.DocDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)
                dtRow(dtreport.IDNumberColumn) = _ParticipantInfo.IDNumber
                dtRow(dtreport.ParticipantIDColumn) = _ParticipantInfo.ParticipantID

                dtRow(dtreport.CreatedDocumentNoColumn) = "DMCM-" & _itmDMCM.ToString

                'Get OR Number
                Dim _ORNumber = (From x In curTransaction _
                                 Where x.DMCMNo = _itmDMCM _
                                 Select x.ORNumber).FirstOrDefault.ToString

                If _ORNumber <> "0" Then
                    dtRow(dtreport.RefNoColumn) = "OR-" & _ORNumber
                End If

                'Reference Document
                If _itmDMCM = 0 Then
                    dtRow(dtreport.DocumentNoColumn) = ""
                    dtRow(dtreport.DueDateColumn) = ""
                Else
                    'get Summary Type
                    Dim Summary As New WESMBillSummary
                    Summary = (From x In curTransaction _
                                Where x.WESMBillSummary.INVDMCMNo = _itmInvoice _
                                Select x.WESMBillSummary).FirstOrDefault
                    If Summary.SummaryType = EnumSummaryType.INV Then
                        dtRow(dtreport.DocumentNoColumn) = WESMBillNumberPrefix.ToString & "-" & Summary.INVDMCMNo
                    Else
                        dtRow(dtreport.DocumentNoColumn) = Summary.SummaryType.ToString & "-" & Summary.INVDMCMNo
                    End If

                    dtRow(dtreport.DueDateColumn) = FormatDateTime(Summary.DueDate, DateFormat.ShortDate)
                End If

                dtreport.Rows.Add(dtRow)
                dtreport.AcceptChanges()
            Next

        Next

        'Check participant for Replenishment
        If _ParticipantPR <> 0 Then
            Dim dtRow = dtreport.NewRow
            dtRow(dtreport.AllocationDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)
            dtRow(dtreport.AmountAppliedColumn) = _ParticipantPR * -1
            dtRow(dtreport.AmountColumn) = _ParticipantPR
            dtRow(dtreport.BatchCodeColumn) = curTransaction.FirstOrDefault.PaymentBatchCode
            dtRow(dtreport.CollectionDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)

            dtRow(dtreport.DocDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)
            dtRow(dtreport.IDNumberColumn) = _ParticipantInfo.IDNumber
            dtRow(dtreport.ParticipantIDColumn) = _ParticipantInfo.ParticipantID
            dtRow(dtreport.DocumentNoColumn) = "PR Replenishment"

            dtRow(dtreport.Position1Column) = _Signatories.Position_1
            dtRow(dtreport.Position2Column) = _Signatories.Position_2
            dtRow(dtreport.Position3Column) = _Signatories.Position_3

            dtRow(dtreport.Signatory1Column) = _Signatories.Signatory_1
            dtRow(dtreport.Signatory2Column) = _Signatories.Signatory_2
            dtRow(dtreport.Signatory3Column) = _Signatories.Signatory_3

            dtreport.Rows.Add(dtRow)
            dtreport.AcceptChanges()
        End If


        If _ExcessCollection <> 0 Then
            Dim dtRow = dtreport.NewRow
            dtRow(dtreport.AllocationDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)

            dtRow(dtreport.AmountAppliedColumn) = _ExcessCollection
            dtRow(dtreport.AmountColumn) = _ExcessCollection
            dtRow(dtreport.BASE_AMOUNTColumn) = _ExcessCollection
            dtRow(dtreport.VAT_AMOUNTColumn) = 0

            dtRow(dtreport.BatchCodeColumn) = _PaymentBatch
            dtRow(dtreport.CollectionDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)

            dtRow(dtreport.TypeColumn) = _dicTransactionType(EnumPaymentType.ReturnToParticipant)

            dtRow(dtreport.Position1Column) = _Signatories.Position_1
            dtRow(dtreport.Position2Column) = _Signatories.Position_2
            dtRow(dtreport.Position3Column) = _Signatories.Position_3

            dtRow(dtreport.Signatory1Column) = _Signatories.Signatory_1
            dtRow(dtreport.Signatory2Column) = _Signatories.Signatory_2
            dtRow(dtreport.Signatory3Column) = _Signatories.Signatory_3

            dtRow(dtreport.DocDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)
            dtRow(dtreport.IDNumberColumn) = _ParticipantInfo.IDNumber
            dtRow(dtreport.ParticipantIDColumn) = _ParticipantInfo.ParticipantID
            dtRow(dtreport.DocumentNoColumn) = ""
            dtRow(dtreport.DueDateColumn) = ""

            dtreport.Rows.Add(dtRow)
            dtreport.AcceptChanges()
        End If


        'Dim curTransaction = (From x In _lstParticipantSummary _
        '                      Select x Order By x.WESMBillSummary.WESMBillSummaryNo Ascending).ToList

        'For Each itmTrans In curTransaction
        '    Dim dtRow As DataRow
        '    dtRow = dtreport.NewRow
        '    dtRow(dtreport.AllocationDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)
        '    dtRow(dtReport.AmountAppliedColumn) = itmTrans.PaymentAmount
        '    dtRow(dtReport.AmountColumn) = itmTrans.PaymentAmount
        '    dtRow(dtreport.BatchCodeColumn) = itmTrans.PaymentBatchCode
        '    dtRow(dtreport.CollectionDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)

        '    dtRow(dtreport.TypeColumn) = _dicTransactionType(itmTrans.PaymentType)

        '    If itmTrans.DMCMNo <> 0 Then
        '        dtRow(dtreport.CreatedDocumentNoColumn) = "DMCM-" & itmTrans.DMCMNo
        '    End If

        '    If itmTrans.ORNumber <> 0 Then
        '        dtRow(dtReport.RefNoColumn) = "OR-" & itmTrans.ORNumber
        '    End If

        '    dtRow(dtreport.Position1Column) = _Signatories.Position_1
        '    dtRow(dtreport.Position2Column) = _Signatories.Position_2
        '    dtRow(dtreport.Position3Column) = _Signatories.Position_3

        '    dtRow(dtreport.Signatory1Column) = _Signatories.Signatory_1
        '    dtRow(dtreport.Signatory2Column) = _Signatories.Signatory_2
        '    dtRow(dtreport.Signatory3Column) = _Signatories.Signatory_3

        '    dtRow(dtreport.DocDateColumn) = FormatDateTime(_AllocationDate, DateFormat.ShortDate)
        '    dtRow(dtreport.IDNumberColumn) = itmTrans.WESMBillSummary.IDNumber.IDNumber
        '    dtRow(dtreport.ParticipantIDColumn) = itmTrans.WESMBillSummary.IDNumber.ParticipantID
        '    'Reference Document
        '    If itmTrans.WESMBillSummary.INVDMCMNo = 0 Then
        '        dtRow(dtReport.DocumentNoColumn) = ""
        '        dtRow(dtReport.DueDateColumn) = ""
        '    Else
        '        dtRow(dtReport.DocumentNoColumn) = itmTrans.WESMBillSummary.SummaryType.ToString & "-" & itmTrans.WESMBillSummary.INVDMCMNo
        '        dtRow(dtReport.DueDateColumn) = FormatDateTime(itmTrans.WESMBillSummary.DueDate, DateFormat.ShortDate)
        '    End If

        '    dtReport.Rows.Add(dtRow)
        '    dtReport.AcceptChanges()
        'Next

        ds.Tables.Add(dtreport)

        Dim sfDialog As New FolderBrowserDialog
        Dim sfText As String = ""

        sfDialog.ShowNewFolderButton = True
        sfDialog.ShowDialog()
        sfText = sfDialog.SelectedPath

        If sfText.Trim.Length = 0 Then
            MsgBox("Cancelled export to PDF", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, MsgBoxStyle), "Cancelled!")
            Exit Sub
        End If
        With PaySummaryReport
            .SetDataSource(ds)
            .ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, sfText & "\" & _ParticipantInfo.IDNumber & "_" & "PaymentDetails_" & Replace(FormatDateTime(_AllocationDate, DateFormat.ShortDate), "/", "") & ".pdf")
            MsgBox("Successfully exported to " & sfText & "\" & _ParticipantInfo.IDNumber & "_" & "PaymentDetails_" & Replace(FormatDateTime(_AllocationDate, DateFormat.ShortDate), "/", "") & ".pdf", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, MsgBoxStyle), "Success")
        End With
    End Sub

End Class