'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmSummary
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     January 30, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI the Generation of Summary of Invoices, DMCM and Check vouchers
'Arguments/Parameters:  
'Files/Database Tables:  AM_ACCOUNTING_CODE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description



Option Explicit On
Option Strict On
Imports System
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib


'Imports LDAPLib
'Imports LDAPLogin

Public Class frmSummary
    Private WbillHelper As WESMBillHelper
    Private lstParticipants As List(Of AMParticipants)
    Private lstPostingToGP As List(Of WESMBillGPPosted)

    Private Sub cmd_GenReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenReport.Click
        Try
            Dim SummaryToReport As New List(Of SummaryObj)

            If FormatDateTime(dtp_To.Value, DateFormat.ShortDate) < FormatDateTime(dtp_From.Value, DateFormat.ShortDate) Then
                MsgBox("Invalid Date Range!", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            lstParticipants = Me.WbillHelper.GetAMParticipants()
            lstPostingToGP = Me.WbillHelper.GetWESMBillGPPosted(CDate(FormatDateTime(dtp_From.Value, DateFormat.ShortDate)), CDate(FormatDateTime(dtp_To.Value, DateFormat.ShortDate)), 2)

            If rb_Invoice.Checked Then
                Dim WESMInvoice = Me.WbillHelper.GetWESMBills(CDate(FormatDateTime(dtp_From.Value, DateFormat.ShortDate)), CDate(FormatDateTime(dtp_To.Value, DateFormat.ShortDate)))
                SummaryToReport = Me.WESMInvoiceToSummary(WESMInvoice)
            ElseIf rb_CheckVoucher.Checked Then
                Dim lstChecks = Me.WbillHelper.GetCheck(CDate(FormatDateTime(dtp_From.Value, DateFormat.ShortDate)), CDate(FormatDateTime(dtp_To.Value, DateFormat.ShortDate)))
                lstChecks = (From x In lstChecks _
                             Where x.CheckNumber <> "0" _
                             Select x Order By x.VoucherNumber Ascending).ToList
                SummaryToReport = Me.CheckVoucherToSummary(lstChecks)
            ElseIf rb_DMCM.Checked Then
                Dim DMCMlst = Me.WbillHelper.GetDebitCreditMemoMain(CDate(FormatDateTime(dtp_From.Value, DateFormat.ShortDate)), CDate(FormatDateTime(dtp_To.Value, DateFormat.ShortDate)))
                DMCMlst = (From x In DMCMlst _
                           Select x Order By x.DMCMNumber Ascending).ToList
                SummaryToReport = Me.DMCMToSummary(DMCMlst)
            End If

            If SummaryToReport.Count = 0 Then
                MsgBox("No Records Found", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            Else
                Me.dgv_dataView.DataSource = Me.SummaryToTable(SummaryToReport)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Updated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepSummaryForABWindow.ToString, "Accessing Summary For Accounting Books Window", "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInGeneratingReport.ToString, AMModule.UserName)
            Exit Sub
        End Try
    End Sub


    Private Function WESMInvoiceToSummary(ByVal lstWESMInvoice As List(Of WESMBill)) As List(Of SummaryObj)
        Dim retSummary As New List(Of SummaryObj)

        Dim _lstInvoiceNo = (From x In lstWESMInvoice _
                             Select x.InvoiceNumber Distinct).ToList

        For Each itmInvoiceNo In _lstInvoiceNo
            Dim _InvoiceNo = itmInvoiceNo
            Dim cSummary As New SummaryObj

            Dim _lstWESMInvoice = (From x In lstWESMInvoice _
                                   Where x.InvoiceNumber = _InvoiceNo _
                                   Select x).ToList

            For Each itmInvoice In _lstWESMInvoice
                With cSummary
                    .AMSBatchCode = itmInvoice.BatchCode
                    .CustomerNumber = CStr(itmInvoice.IDNumber) '08/27/2013 Vloody
                    .CustomerName = (From x In lstParticipants _
                                     Where x.IDNumber = CStr(.CustomerNumber) _
                                     Select x.ParticipantID).FirstOrDefault
                    .DocumentAmount += itmInvoice.Amount
                    If itmInvoice.ChargeType = EnumChargeType.E Or itmInvoice.ChargeType = EnumChargeType.MF Then
                        .BaseAmount = itmInvoice.Amount
                    Else
                        .VATAmount = itmInvoice.Amount
                    End If
                    .DocumentDate = itmInvoice.InvoiceDate
                    .DocumentNumber = "INV" & itmInvoice.InvoiceNumber
                    .GPReferenceNo = (From x In lstPostingToGP _
                                      Where x.BatchCode = .AMSBatchCode _
                                      Select x.GPRefNo).FirstOrDefault
                End With
            Next
            retSummary.Add(cSummary)
        Next

        Return retSummary
    End Function

    Private Function DMCMToSummary(ByVal lstDMCM As List(Of DebitCreditMemo)) As List(Of SummaryObj)
        Dim retSummary As New List(Of SummaryObj)

        For Each itmDMCM In lstDMCM
            Dim itmSummary As New SummaryObj
            Dim _itmDMCM = itmDMCM

            With itmSummary
                .DocumentAmount = itmDMCM.TotalAmountDue
                .DocumentNumber = "DMCM" & itmDMCM.DMCMNumber
                .DocumentDate = CDate(FormatDateTime(itmDMCM.UpdatedDate, DateFormat.ShortDate))
                .CustomerNumber = CStr(itmDMCM.IDNumber)
                .CustomerName = (From X In lstParticipants _
                                 Where X.IDNumber = CStr(.CustomerNumber) _
                                 Select X.ParticipantID).FirstOrDefault
                .GPReferenceNo = (From x In lstPostingToGP _
                                  Where x.JVNumber = _itmDMCM.JVNumber _
                                  Select x.GPRefNo).FirstOrDefault
                .AMSBatchCode = (From x In lstPostingToGP _
                                 Where x.JVNumber = _itmDMCM.JVNumber _
                                 Select x.BatchCode).FirstOrDefault

                If .GPReferenceNo = "" Then
                    .GPReferenceNo = "- Journal Voucher Not Posted -"
                End If

                .ChargeType = itmDMCM.ChargeType
            End With

            retSummary.Add(itmSummary)
        Next

        Return retSummary
    End Function

    Private Function CheckVoucherToSummary(ByVal lstCheckVoucher As List(Of Check)) As List(Of SummaryObj)
        Dim retSummary As New List(Of SummaryObj)

        For Each itmCheck In lstCheckVoucher
            Dim itmSummary As New SummaryObj
            With itmSummary
                .DocumentNumber = itmCheck.VoucherNumber
                '.DocumentDate = itmCheck.UpdatedDate
                .CheckNumber = itmCheck.CheckNumber
                .DocumentDate = itmCheck.TransactionDate
                .CustomerNumber = CStr(itmCheck.Participant.IDNumber)
                .CustomerName = itmCheck.Participant.ParticipantID
                .DocumentAmount = itmCheck.Amount
                .AMSBatchCode = itmCheck.BatchCode
                .GPReferenceNo = (From x In lstPostingToGP _
                                  Where x.BatchCode = .AMSBatchCode _
                                  Select x.GPRefNo).FirstOrDefault
            End With
            retSummary.Add(itmSummary)
        Next

        Return retSummary
    End Function

    Private Function SummaryToTable(ByVal lstSummary As List(Of SummaryObj)) As DataTable
        Dim dt As New DataTable

        With dt.Columns
            .Add("Document Number")
            .Add("Document Date")

            .Add("Customer Number")
            .Add("Customer Name")

            If rb_CheckVoucher.Checked Then
                .Add("Check Number")
            End If

            If rb_Invoice.Checked Then
                .Add("Base Amount")
                .Add("VAT")
                .Add("Total")
            Else
                .Add("Document Amount")
            End If

            If rb_DMCM.Checked Then
                .Add("Charge Type")
            End If

            .Add("AMS Batch Code")
            .Add("GP Reference Number")
        End With
        dt.AcceptChanges()

        For Each itmSummary In lstSummary
            Dim dr As DataRow
            dr = dt.NewRow

            With itmSummary
                dr("Document Number") = .DocumentNumber
                dr("Document Date") = FormatDateTime(.DocumentDate, DateFormat.ShortDate)

                dr("Customer Number") = .CustomerNumber
                dr("Customer Name") = .CustomerName

                If rb_CheckVoucher.Checked Then
                    dr("Check Number") = .CheckNumber
                End If

                If rb_Invoice.Checked Then
                    dr("Base Amount") = FormatNumber(.BaseAmount, 2, TriState.True, TriState.True)
                    dr("VAT") = FormatNumber(.VATAmount, 2, TriState.True, TriState.True)
                    dr("Total") = FormatNumber(.DocumentAmount, 2, TriState.True, TriState.True)
                Else
                    dr("Document Amount") = FormatNumber(.DocumentAmount, 2, TriState.True, TriState.True)
                End If

                If rb_DMCM.Checked Then
                    dr("Charge Type") = .ChargeType
                End If

                dr("AMS Batch code") = .AMSBatchCode
                dr("GP Reference Number") = .GPReferenceNo
            End With

            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next

        Return dt
    End Function

    Private Sub frmSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
            WbillHelper = WESMBillHelper.GetInstance()
            WbillHelper.ConnectionString = AMModule.ConnectionString
            WbillHelper.UserName = AMModule.UserName
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Export.Click
        Dim listSummaryAccountingBooks As New List(Of SummaryAccountingBooks)
        Dim _fldrSelect As New FolderBrowserDialog
        Dim fPathName As String = ""
        Dim header() As String = {AMModule.CompanyFullName, Chr(34) & AMModule.CompanyAddress & Chr(34), "VAT REG TIN " & AMModule.CompanyTinNumber, "",
                  "CAS PERMIT NO." & AMModule.BIRCASPermit, "SOFTWARE SYSTEM: ACCOUNTS MANAGEMENT SYSTEM", "VERSION NO. " & AMModule.SystemVersion}
        Dim ans As New MsgBoxResult

        ans = MsgBox("Do you really want to export the records as Excel Format?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Export records")
        If ans <> MsgBoxResult.Yes Then
            Exit Sub
        End If


        With _fldrSelect
            .ShowDialog()
            If .SelectedPath = "" Then
                Exit Sub
            End If
            fPathName = .SelectedPath & "\SummaryOf_AccountingBooks_" & Format(Me.dtp_From.Value, "MMddyyyy") & "-" & _
                        Format(Me.dtp_To.Value, "MMddyyyy") & ".xlsx"

            'fPathName = .SelectedPath & "\SummaryOfAccountingBooks.xlsx"
        End With

        Try
            ProgressThread.Show("Please wait generating the report")

            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksSettlementInterest(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBill(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBillEWT(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBillJV(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBillOffsetting(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksDailyCollection(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            'added by lance to get the deleted OR in Colleciton 10/17/2019
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksDeletedDailyCollection(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksCollectionTagging(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksCollectionTaggingDMCM(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksCollectionTaggingFTF(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRReplenishment(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRInterest(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRTransferInterest(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocation(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationOffsetting(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWithholdingTAXAdjustmentSetup(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            'added by lance 03/01/2019
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksSPASetup(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksSPAClosing(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))


            'listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationFTF(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            'listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationCheck(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationEFT(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            'added by lance for PR Refund 05/01/2019
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRREfund(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            listSummaryAccountingBooks.TrimExcess()

            'Me.WbillHelper.CreateCSVSummaryOfAccountingBooks(listSummaryAccountingBooks, fPathName, ",")
            Me.WbillHelper.CreateExcelSummaryOfAccountingBooks(listSummaryAccountingBooks, fPathName, header)

            ProgressThread.Close()
            MsgBox("Successfully exported records to " & fPathName, MsgBoxStyle.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error found.")
        Finally
            listSummaryAccountingBooks = New List(Of SummaryAccountingBooks)
        End Try
        
    End Sub


    Private Sub btn_ExportToText_Click(sender As Object, e As EventArgs) Handles btn_ExportToText.Click
        Dim listSummaryAccountingBooks As New List(Of SummaryAccountingBooks)
        Dim _fldrSelect As New FolderBrowserDialog
        Dim fPathName As String = ""
        Dim header() As String = {AMModule.CompanyFullName, Chr(34) & AMModule.CompanyAddress & Chr(34), "VAT REG TIN " & AMModule.CompanyTinNumber, "",
                  "CAS PERMIT NO." & AMModule.BIRCASPermit, "SOFTWARE SYSTEM: ACCOUNTS MANAGEMENT SYSTEM", "VERSION NO. " & AMModule.SystemVersion}
        Dim ans As New MsgBoxResult

        ans = MsgBox("Do you really want to export the records as txt Format?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Export records")
        If ans <> MsgBoxResult.Yes Then
            Exit Sub
        End If


        With _fldrSelect
            .ShowDialog()
            If .SelectedPath = "" Then
                Exit Sub
            End If
            fPathName = .SelectedPath & "\SummaryOf_AccountingBooks_" & Format(Me.dtp_From.Value, "MMddyyyy") & "-" & _
                        Format(Me.dtp_To.Value, "MMddyyyy") & ".txt"

            'fPathName = .SelectedPath & "\SummaryOfAccountingBooks.xlsx"
        End With
        Try
            ProgressThread.Show("Please wait generating the report")

            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksSettlementInterest(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBill(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBillEWT(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBillJV(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBillOffsetting(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksDailyCollection(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            'added by lance to get the deleted OR in Colleciton 10/17/2019
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksDeletedDailyCollection(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksCollectionTagging(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksCollectionTaggingDMCM(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksCollectionTaggingFTF(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRReplenishment(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRInterest(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRTransferInterest(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocation(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationOffsetting(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWithholdingTAXAdjustmentSetup(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            'added by lance 03/01/2019
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksSPASetup(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksSPAClosing(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))


            'listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationFTF(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            'listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationCheck(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationEFT(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            'added by lance for PR Refund 05/01/2019
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRREfund(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            listSummaryAccountingBooks.TrimExcess()

            Dim sDate As Date = CDate(Me.dtp_From.Value)
            Dim eDate As Date = CDate(Me.dtp_To.Value)

            'Me.WbillHelper.CreateCSVSummaryOfAccountingBooks(listSummaryAccountingBooks, fPathName, ",")
            Me.CreateTxTFile(sDate, eDate, listSummaryAccountingBooks, fPathName, header)

            ProgressThread.Close()
            MsgBox("Successfully exported records to " & fPathName, MsgBoxStyle.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error found.")
        Finally
            listSummaryAccountingBooks = New List(Of SummaryAccountingBooks)
        End Try
        
    End Sub

    Private Sub CreateTxTFile(ByVal startdate As Date, ByVal enddate As Date, ByVal ListOfAccoutingBooks As List(Of SummaryAccountingBooks), ByVal filename As String, header() As String)

        Dim strBldr As New StringBuilder()
        For Each itm In header
            strBldr.Append(itm)
            strBldr.AppendLine()
        Next

        strBldr.AppendLine()
        strBldr.Append("Accounting Books / File Attributes / Layout Difference")
        strBldr.AppendLine()

        strBldr.Append("File Name: General Ledger")
        strBldr.AppendLine()

        strBldr.Append("File Type: Text File")
        strBldr.AppendLine()

        strBldr.Append("Number of Records: " & (ListOfAccoutingBooks.Count).ToString("N0"))
        strBldr.AppendLine()

        Dim getTotal As Decimal = (From x In ListOfAccoutingBooks Select Math.Abs(x.Debit)).Sum
        strBldr.Append("Amount Field Control Total: " & (getTotal).ToString("n"))
        strBldr.AppendLine()

        strBldr.Append("Period Covered: " & startdate.ToString("MMMM") & " to " & enddate.ToString("MMMM") & " " & enddate.ToString("yyyy"))
        strBldr.AppendLine()

        strBldr.Append("Transaction cut-off Date/Time: " & enddate.ToString("MMMM") & " " & System.DateTime.DaysInMonth(enddate.Year, enddate.Month) & ", " & enddate.Year & "/06:00 PM")
        strBldr.AppendLine()

        strBldr.Append("Extracted By: " & AMModule.UserName)
        strBldr.AppendLine()

        strBldr.Append("Extracted Date/Time: " & WbillHelper.GetSystemDateTime.ToString("MMM-dd-yyyy hh:mm tt"))
        strBldr.AppendLine()

        strBldr.Append("File Layout:")
        strBldr.AppendLine()

        strBldr.Append(LengthLoop("    Field Name", ("     Field Name").Length, 20))
        strBldr.Append(LengthLoop("From", ("From").Length, 10))
        strBldr.Append(LengthLoop("To", ("To").Length, 10))
        strBldr.Append("Length")
        strBldr.AppendLine()

        strBldr.Append(LengthLoop("    JV No.", ("     JV No.").Length, 20))
        strBldr.Append(LengthLoop("1", ("1").Length, 10))
        strBldr.Append(LengthLoop("20", ("20").Length, 10))
        strBldr.Append("20")
        strBldr.AppendLine()

        strBldr.Append(LengthLoop("    Date", ("     Date").Length, 20))
        strBldr.Append(LengthLoop("21", ("21").Length, 10))
        strBldr.Append(LengthLoop("40", ("40").Length, 10))
        strBldr.Append("20")
        strBldr.AppendLine()

        strBldr.Append(LengthLoop("    Reference", ("     Reference").Length, 20))
        strBldr.Append(LengthLoop("41", ("41").Length, 10))
        strBldr.Append(LengthLoop("60", ("60").Length, 10))
        strBldr.Append("20")
        strBldr.AppendLine()

        strBldr.Append(LengthLoop("    Description", ("     Description").Length, 20))
        strBldr.Append(LengthLoop("61", ("61").Length, 10))
        strBldr.Append(LengthLoop("105", ("105").Length, 10))
        strBldr.Append("45")
        strBldr.AppendLine()

        strBldr.Append(LengthLoop("    Participant", ("     Participant").Length, 20))
        strBldr.Append(LengthLoop("106", ("106").Length, 10))
        strBldr.Append(LengthLoop("180", ("180").Length, 10))
        strBldr.Append("75")
        strBldr.AppendLine()

        strBldr.Append(LengthLoop("    Account Code", ("     Account Code").Length, 20))
        strBldr.Append(LengthLoop("181", ("181").Length, 10))
        strBldr.Append(LengthLoop("205", ("205").Length, 10))
        strBldr.Append("25")
        strBldr.AppendLine()

        strBldr.Append(LengthLoop("    Account Title", ("     Account Title").Length, 20))
        strBldr.Append(LengthLoop("206", ("206").Length, 10))
        strBldr.Append(LengthLoop("270", ("270").Length, 10))
        strBldr.Append("65")
        strBldr.AppendLine()

        strBldr.Append(LengthLoop("    Debit", ("     Debit").Length, 20))
        strBldr.Append(LengthLoop("271", ("271").Length, 10))
        strBldr.Append(LengthLoop("295", ("295").Length, 10))
        strBldr.Append("25")
        strBldr.AppendLine()

        strBldr.Append(LengthLoop("    Credit", ("     Credit").Length, 20))
        strBldr.Append(LengthLoop("296", ("296").Length, 10))
        strBldr.Append(LengthLoop("320", ("320").Length, 10))
        strBldr.Append("25")
        strBldr.AppendLine()
        strBldr.AppendLine()
        strBldr.AppendLine()
        strBldr.AppendLine()

        'Header
        strBldr.Append(LengthLoop("JV No.", ("JV No.").Length, 20))
        strBldr.Append(LengthLoop("Date", ("Date").Length, 20))
        strBldr.Append(LengthLoop("Reference", ("Reference").Length, 20))
        strBldr.Append(LengthLoop("Description", ("Description").Length, 45))
        strBldr.Append(LengthLoop("Participant ID", ("Participant ID").Length, 75))
        strBldr.Append(LengthLoop("Account Code", ("Account Code").Length, 25))
        strBldr.Append(LengthLoop("Account Title", ("Account Title").Length, 65))
        strBldr.Append(LengthLoop("Debit", ("Debit").Length, 25))
        strBldr.Append(LengthLoop("Credit", ("Credit").Length, 25))
   
        strBldr.AppendLine()

        'Data
        For Each item In ListOfAccoutingBooks
            strBldr.Append(LengthLoop(item.JVNo, item.JVNo.Length, 20)) 'JV Number
            strBldr.Append(LengthLoop(Format(item.JVDate, "MM/dd/yyyy"), Format(item.JVDate, "MM/dd/yyyy").ToString.Length, 20)) 'Transaction Date
            strBldr.Append(LengthLoop(item.DocumentNo, item.DocumentNo.Length, 20)) 'Reference Nuber
            strBldr.Append(LengthLoop(item.TransactionType, item.TransactionType.Length, 45))

            If item.ItemParticipant.ParticipantID Is Nothing Then
                strBldr.Append(LengthLoop("N/A", ("N/A").Length, 75))
            Else
                strBldr.Append(LengthLoop(item.ItemParticipant.ParticipantID, item.ItemParticipant.ParticipantID.Length, 75))
            End If

            strBldr.Append(LengthLoop(item.ItemAccountCode.AccountCode, item.ItemAccountCode.AccountCode.Length, 25))
            strBldr.Append(LengthLoop(item.ItemAccountCode.Description, item.ItemAccountCode.Description.Length, 65))
            strBldr.Append(LengthLoop(Math.Abs(item.Debit).ToString, Math.Abs(item.Debit).ToString.Length, 25))
            strBldr.Append(LengthLoop(Math.Abs(item.Credit).ToString, Math.Abs(item.Credit).ToString.Length, 25))
            strBldr.AppendLine()
        Next

        Using fs As FileStream = File.Create(filename)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(strBldr.ToString)
            fs.Write(info, 0, info.Length)
        End Using

    End Sub

    Private Sub CreateCSVDatFile(ByVal startdate As Date, ByVal enddate As Date, ByVal ListOfAccoutingBooks As List(Of SummaryAccountingBooks), ByVal filename As String, header() As String, ByVal filetype As String)

        Dim strBldr As New StringBuilder()
        For Each itm In header
            strBldr.Append(itm)
            strBldr.AppendLine()
        Next

        strBldr.AppendLine()
        strBldr.Append("Accounting Books / File Attributes / Layout Difference")
        strBldr.AppendLine()

        strBldr.Append("File Name: General Ledger")
        strBldr.AppendLine()

        strBldr.Append("File Type: " & filetype & " File")
        strBldr.AppendLine()

        strBldr.Append(Chr(34) & "Number of Records: " & (ListOfAccoutingBooks.Count).ToString("N0") & Chr(34))
        strBldr.AppendLine()

        Dim getTotal As Decimal = (From x In ListOfAccoutingBooks Select Math.Abs(x.Debit)).Sum

        If (getTotal).ToString("N0").Length > 4 Then
            strBldr.Append(Chr(34) & "Amount Field Control Total: ")
            strBldr.Append((getTotal).ToString("n") & Chr(34))
        Else
            strBldr.Append("Amount Field Control Total: ")
            strBldr.Append((getTotal).ToString("n"))
        End If
        strBldr.AppendLine()

        strBldr.Append("Period Covered: " & startdate.ToString("MMMM") & " to " & enddate.ToString("MMMM") & " " & enddate.ToString("yyyy"))
        strBldr.AppendLine()

        strBldr.Append(Chr(34) & "Transaction cut-off Date/Time: " & enddate.ToString("MMMM") & " " & System.DateTime.DaysInMonth(enddate.Year, enddate.Month) & ", " & enddate.Year & "/06:00 PM" & Chr(34))
        strBldr.AppendLine()

        strBldr.Append("Extracted By: " & AMModule.UserName)
        strBldr.AppendLine()

        strBldr.Append("Extracted Date/Time: " & WbillHelper.GetSystemDateTime.ToString("MMM-dd-yyyy hh:mm tt"))
        strBldr.AppendLine()

        strBldr.Append("File Layout:")
        strBldr.AppendLine()

        strBldr.Append("    Field Name,")
        strBldr.Append("From,")
        strBldr.Append("To,")
        strBldr.Append("Length,")
        strBldr.AppendLine()

        strBldr.Append("    JV No.,")
        strBldr.Append("1,")
        strBldr.Append("20,")
        strBldr.Append("20")
        strBldr.AppendLine()

        strBldr.Append("    Date,")
        strBldr.Append("21,")
        strBldr.Append("40,")
        strBldr.Append("20")
        strBldr.AppendLine()

        strBldr.Append("    Reference,")
        strBldr.Append("41,")
        strBldr.Append("60,")
        strBldr.Append("20")
        strBldr.AppendLine()

        strBldr.Append("    Description,")
        strBldr.Append("61,")
        strBldr.Append("105,")
        strBldr.Append("45")
        strBldr.AppendLine()

        strBldr.Append("    Participant,")
        strBldr.Append("106,")
        strBldr.Append("180,")
        strBldr.Append("75")
        strBldr.AppendLine()

        strBldr.Append("    Account Code,")
        strBldr.Append("181,")
        strBldr.Append("205,")
        strBldr.Append("25")
        strBldr.AppendLine()

        strBldr.Append("    Account Title,")
        strBldr.Append("206,")
        strBldr.Append("270,")
        strBldr.Append("65")
        strBldr.AppendLine()

        strBldr.Append("    Debit,")
        strBldr.Append("271,")
        strBldr.Append("271,")
        strBldr.Append("25")
        strBldr.AppendLine()

        strBldr.Append("    Credit,")
        strBldr.Append("296,")
        strBldr.Append("320,")
        strBldr.Append("25")
        strBldr.AppendLine()
        strBldr.AppendLine()
        strBldr.AppendLine()
        strBldr.AppendLine()

        'Header
        strBldr.Append("JV No.,Date,Reference,Description,Participant ID,Account Code,Account Title,Debit,Credit")
        strBldr.AppendLine()

        'Data
        For Each item In ListOfAccoutingBooks
            strBldr.Append(item.JVNo & Chr(44)) 'JV Number
            strBldr.Append(Format(item.JVDate, "MM/dd/yyyy") & Chr(44)) 'Transaction Date
            strBldr.Append(item.DocumentNo & Chr(44)) 'Reference Nuber
            strBldr.Append(item.TransactionType & Chr(44)) 'Description

            If item.ItemParticipant.ParticipantID Is Nothing Then
                strBldr.Append("N/A" & Chr(44))
            Else
                strBldr.Append(Chr(34) & item.ItemParticipant.ParticipantID & Chr(34) & Chr(44))
            End If

            strBldr.Append(item.ItemAccountCode.AccountCode & Chr(44))   'Accounting Code
            strBldr.Append(item.ItemAccountCode.Description & Chr(44))   'Accounting Description
            strBldr.Append(Math.Abs(item.Debit).ToString & Chr(44)) 'Debit
            strBldr.Append(Math.Abs(item.Credit).ToString) 'Credit

            strBldr.AppendLine()
        Next

        Using fs As FileStream = File.Create(filename)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(strBldr.ToString)
            fs.Write(info, 0, info.Length)
        End Using

    End Sub


    Private Function LengthLoop(ByVal strVal As String, ByVal startIndex As Integer, ByVal endIndex As Integer) As String
        Dim ret As New StringBuilder

        ret.Append(strVal)
        For index As Integer = startIndex To endIndex
            ret.Append(" ")
        Next

        Return ret.ToString
    End Function

    Private Sub btn_ExportToCSV_Click(sender As Object, e As EventArgs) Handles btn_ExportToCSV.Click
        Dim listSummaryAccountingBooks As New List(Of SummaryAccountingBooks)
        Dim _fldrSelect As New FolderBrowserDialog
        Dim fPathName As String = ""
        Dim header() As String = {AMModule.CompanyFullName, Chr(34) & AMModule.CompanyAddress & Chr(34), "VAT REG TIN " & AMModule.CompanyTinNumber, "",
                  "CAS PERMIT NO." & AMModule.BIRCASPermit, "SOFTWARE SYSTEM: ACCOUNTS MANAGEMENT SYSTEM", "VERSION NO. " & AMModule.SystemVersion}
        Dim ans As New MsgBoxResult

        ans = MsgBox("Do you really want to export the records as csv Format?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Export records")
        If ans <> MsgBoxResult.Yes Then
            Exit Sub
        End If


        With _fldrSelect
            .ShowDialog()
            If .SelectedPath = "" Then
                Exit Sub
            End If
            fPathName = .SelectedPath & "\SummaryOf_AccountingBooks_" & Format(Me.dtp_From.Value, "MMddyyyy") & "-" & _
                        Format(Me.dtp_To.Value, "MMddyyyy") & ".csv"
        End With
        Try
            ProgressThread.Show("Please wait generating the report")

            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksSettlementInterest(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBill(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBillEWT(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBillJV(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBillOffsetting(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksDailyCollection(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            'added by lance to get the deleted OR in Colleciton 10/17/2019
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksDeletedDailyCollection(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksCollectionTagging(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksCollectionTaggingDMCM(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksCollectionTaggingFTF(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRReplenishment(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRInterest(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRTransferInterest(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocation(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationOffsetting(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWithholdingTAXAdjustmentSetup(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            'added by lance 03/01/2019
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksSPASetup(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksSPAClosing(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))


            'listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationFTF(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            'listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationCheck(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationEFT(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            'added by lance for PR Refund 05/01/2019
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRREfund(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            listSummaryAccountingBooks.TrimExcess()

            Dim sDate As Date = CDate(Me.dtp_From.Value)
            Dim eDate As Date = CDate(Me.dtp_To.Value)

            'Me.WbillHelper.CreateCSVSummaryOfAccountingBooks(listSummaryAccountingBooks, fPathName, ",")
            Me.CreateCSVDatFile(sDate, eDate, listSummaryAccountingBooks, fPathName, header, "CSV")

            ProgressThread.Close()
            MsgBox("Successfully exported records to " & fPathName, MsgBoxStyle.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error found.")
        Finally
            listSummaryAccountingBooks = New List(Of SummaryAccountingBooks)
        End Try
    End Sub

    Private Sub btn_ExportToDat_Click(sender As Object, e As EventArgs) Handles btn_ExportToDat.Click
        Dim listSummaryAccountingBooks As New List(Of SummaryAccountingBooks)
        Dim _fldrSelect As New FolderBrowserDialog
        Dim fPathName As String = ""
        Dim header() As String = {AMModule.CompanyFullName, Chr(34) & AMModule.CompanyAddress & Chr(34), "VAT REG TIN " & AMModule.CompanyTinNumber, "",
                  "CAS PERMIT NO." & AMModule.BIRCASPermit, "SOFTWARE SYSTEM: ACCOUNTS MANAGEMENT SYSTEM", "VERSION NO. " & AMModule.SystemVersion}
        Dim ans As New MsgBoxResult

        ans = MsgBox("Do you really want to export the records as dat Format?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Export records")
        If ans <> MsgBoxResult.Yes Then
            Exit Sub
        End If


        With _fldrSelect
            .ShowDialog()
            If .SelectedPath = "" Then
                Exit Sub
            End If
            fPathName = .SelectedPath & "\SummaryOf_AccountingBooks_" & Format(Me.dtp_From.Value, "MMddyyyy") & "-" & _
                        Format(Me.dtp_To.Value, "MMddyyyy") & ".dat"
        End With
        Try
            ProgressThread.Show("Please wait generating the report")

            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksSettlementInterest(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBill(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBillEWT(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBillJV(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWESMBillOffsetting(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksDailyCollection(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            'added by lance to get the deleted OR in Colleciton 10/17/2019
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksDeletedDailyCollection(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksCollectionTagging(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksCollectionTaggingDMCM(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksCollectionTaggingFTF(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRReplenishment(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRInterest(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRTransferInterest(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocation(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationOffsetting(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksWithholdingTAXAdjustmentSetup(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            'added by lance 03/01/2019
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksSPASetup(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksSPAClosing(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))


            'listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationFTF(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            'listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationCheck(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPaymentAllocationEFT(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            'added by lance for PR Refund 05/01/2019
            listSummaryAccountingBooks.AddRange(WbillHelper.GetSummaryAccountingBooksPRREfund(CDate(Format(Me.dtp_From.Value, "MM/dd/yyyy")), DateAdd(DateInterval.Day, 1, CDate(Format(Me.dtp_To.Value, "MM/dd/yyyy")))))

            listSummaryAccountingBooks.TrimExcess()

            Dim sDate As Date = CDate(Me.dtp_From.Value)
            Dim eDate As Date = CDate(Me.dtp_To.Value)

            Me.CreateCSVDatFile(sDate, eDate, listSummaryAccountingBooks, fPathName, header, "DAT")

            ProgressThread.Close()
            MsgBox("Successfully exported records to " & fPathName, MsgBoxStyle.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error found.")
        Finally
            listSummaryAccountingBooks = New List(Of SummaryAccountingBooks)
        End Try
    End Sub
End Class
