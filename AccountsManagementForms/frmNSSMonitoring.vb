'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmNSSMonitoring
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 18, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the NSS Monitoring
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   Augist 18, 2012         Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On

Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmNSSMonitoring

    Private WBillHelper As WESMBillHelper
    Dim UtiImporter As ImporterUtility

#Region "Properties"
    Private _BillingPeriod As CalendarBillingPeriod
    Public Property BillingPeriod() As CalendarBillingPeriod
        Get
            Return _BillingPeriod
        End Get
        Set(ByVal value As CalendarBillingPeriod)
            _BillingPeriod = value
        End Set
    End Property

#End Region

    Private Sub frmNSSMonitoring_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            UtiImporter = ImporterUtility.GetInstance()
            UtiImporter.ConnectionString = AMModule.ConnectionString

            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName


            Me.btnNSSSummarySave.Enabled = False
            Me.btnNSSSummaryViewSummary.Enabled = False

            Me.TabControl1.TabPages.Remove(Me.TabPage1)
            Me.TabControl1.TabPages.Remove(Me.TabPage2)
            Me.TabControl1.TabPages.Remove(Me.TabPage3)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Updated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_NSS_InterestWindow.ToString, "Access Uploading NSS Interest Window", "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.NoAccess.ToString, AMModule.UserName)
        End Try
    End Sub

    Private Sub btnNSSSummaryUploadFlatFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSSSummaryUpload.Click
        Dim frm As New frmUploadFile

        Try

            With frm
                frm.Filtertype = 1
                If .ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If
            End With

            'Get list of Calendar BP
            Dim listBP = WBillHelper.GetCalendarBP()

            Dim fnameSplit = Split(frm.FileName, "_")

            If fnameSplit.Length <> 5 Then
                MsgBox("Invalid file format! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            If fnameSplit(0) <> "NSS" Or fnameSplit(1) <> "SUMMARY" Or _
                (fnameSplit(2) <> "1Q" And fnameSplit(2) <> "2Q" And fnameSplit(2) <> "3Q" And fnameSplit(2) <> "2Q") _
                And Not IsNumeric(fnameSplit(3)) Or fnameSplit(4).Length <> 12 Then
                MsgBox("Invalid file format! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            'Get the billing period of the file
            Dim fnameSplit2 = Split(fnameSplit(fnameSplit.Length - 1), ".")
            Dim quarterPeriod = Me.ConvertIntoQuarterPeriod(fnameSplit(2))
            Dim yearName = CInt(fnameSplit(3))
            Dim allocationDate = New Date(CInt(Mid(fnameSplit2(0), 5, 4)), CInt(Mid(fnameSplit2(0), 1, 2)), CInt(Mid(fnameSplit2(0), 3, 2)))

            'Check for existing data
            Dim batchCode = WBillHelper.GetNSSSummaryBatchCode(quarterPeriod, yearName)

            If batchCode.Length <> 0 Then
                MsgBox("CSV File already uploaded. Choose another CSV File!", MsgBoxStyle.Critical, "Denied")
                Exit Sub
            End If

            'Get the data in CSV
            Dim listNSSSummary = UtiImporter.ImportNSSSummary(allocationDate, quarterPeriod, yearName, frm.DirectoryName, frm.FileName)

            'Load the data in grid
            Me.LoadNSSSummary(listNSSSummary)

            MsgBox("Successfuly uploaded. Please poke the 'Save' button to save the records in database.", _
                    MsgBoxStyle.Information, "Uploaded")

            Me.btnNSSSummarySave.Enabled = True
            Me.btnNSSSummaryViewSummary.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Updated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_NSS_InterestWindow.ToString, ex.Message, frm.FileName, "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInUploading.ToString, AMModule.UserName)
        End Try
    End Sub

    Private Sub btnNSSSummarySave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSSSummarySave.Click
        If Me.DGridNSSSummary.RowCount = 0 Then
            MsgBox("Nothing to saved!", MsgBoxStyle.Critical, "No data")
            Exit Sub
        End If

        Dim ans As MsgBoxResult

        Try
            'Get the list of NSS Summary in grid
            Dim listNSSSummary = Me.GetNSSSummary()

            'Generate the DMCM
            Dim listDMCM = Me.GenerateDMCM(listNSSSummary)

            'Generate the Journal Voucher
            Dim itemJV = Me.GenerateJornalVoucher(listDMCM)

            'Generate the WESM GP Posted
            Dim itemGP = Me.GenerateGPPosted(itemJV)

            'Generate the EFT
            Dim listEFT = Me.GenerateEFT(listNSSSummary)

            Dim QuarterlyPeriod = listNSSSummary.First.QuarterlyPeriod
            Dim yearPeriod = listNSSSummary.First.YearPeriod

            ans = MsgBox("Do you really want to save the records?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            ProgressThread.Show("Please wait while saving.")

            Dim batchCode As String = ""

            'Save the NSS Summary
            WBillHelper.SaveNSSSummary(listNSSSummary, listDMCM, itemJV, itemGP, listEFT, batchCode, "")

            ProgressThread.Close()
            MsgBox("Successfully Saved", MsgBoxStyle.Information, "Saved")

            For index = 0 To Me.DGridNSSSummary.RowCount - 1
                Me.DGridNSSSummary.Rows(index).Cells("colNSSSummaryBatchCode").Value = batchCode
            Next

            Me.btnNSSSummarySave.Enabled = False
            Me.btnNSSSummaryViewSummary.Enabled = True

            'Updated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_NSS_InterestWindow.ToString, listNSSSummary.First.YearPeriod.ToString() & " " & listNSSSummary.First.QuarterlyPeriod.ToString(), "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Updated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_NSS_InterestWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInSaving.ToString, AMModule.UserName)
        
        End Try

    End Sub

    Private Sub btnAnvancedSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSSSummaryAdvanceSearch.Click
        Dim frm As New frmSearchTransDate
        frm.Text = "Advanced Search"

        'Get the Year Now
        frm.YearNow = SystemDate.Year

        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim YearPeriod = frm.YearSelected
            Dim QuarterPeriod = frm.QuarterSelected

            Dim listNSSSummary = WBillHelper.GetNSSSummary(frm.YearSelected, frm.QuarterSelected)

            If listNSSSummary.Count = 0 Then
                MsgBox("No records found!", MsgBoxStyle.Information, "No data")
                Me.DGridNSSSummary.Rows.Clear()
                Exit Sub
            End If

            Me.LoadNSSSummary(listNSSSummary)

            Me.btnNSSSummarySave.Enabled = False
            Me.btnNSSSummaryViewSummary.Enabled = True
        End If
    End Sub

    Private Sub btnNSSSummaryViewSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSSSummaryViewSummary.Click
        Try
            If Me.DGridNSSSummary.RowCount = 0 Then
                MsgBox("Nothing to print!", MsgBoxStyle.Critical, "No data")
                Exit Sub
            End If

            'Get the list in NSS
            Dim listNSSSummary = Me.GetNSSSummary()

            'Get the system date
            Dim dateNow = SystemDate.ToString("MMMM dd, yyyy")

            Dim dt As New DataTable
            With dt.Columns
                .Add("REMARKS1", GetType(String))
                .Add("REMARKS2", GetType(String))
                .Add("BATCH_CODE", GetType(String))
                .Add("QUARTERLY_PERIOD", GetType(Integer))
                .Add("YEAR_PERIOD", GetType(Integer))
                .Add("ID_NUMBER", GetType(String))
                .Add("PARTICIPANT_ID", GetType(String))
                .Add("TOTAL_NSS_INTEREST", GetType(Decimal))
                .Add("TOTAL_NSS_INTEREST_NET_WTAX", GetType(Decimal))
                .Add("TOTAL_STL_INTEREST", GetType(Decimal))
                .Add("TOTAL_STL_INTEREST_NET_WTAX", GetType(Decimal))
                .Add("TOTAL", GetType(Decimal))
            End With
            dt.AcceptChanges()

            For Each item In listNSSSummary
                With item
                    Dim row = dt.NewRow()

                    row("REMARKS1") = "Summary of Settlement and Net Settlement Surplus Interest"
                    row("REMARKS2") = "For " & .QuarterlyPeriod.ToString() & " " & .YearPeriod.ToString() '& " as of " & dateNow
                    row("BATCH_CODE") = .BatchCode
                    row("QUARTERLY_PERIOD") = .QuarterlyPeriod
                    row("YEAR_PERIOD") = .YearPeriod
                    row("ID_NUMBER") = .IDNumber.IDNumber
                    row("PARTICIPANT_ID") = .IDNumber.ParticipantID
                    row("TOTAL_NSS_INTEREST") = .TotalNSSInterest
                    row("TOTAL_NSS_INTEREST_NET_WTAX") = .TotalNSSInterestNetWTax
                    row("TOTAL_STL_INTEREST") = .TotalSTLInterest
                    row("TOTAL_STL_INTEREST_NET_WTAX") = .TotalSTLInterestNetWTax
                    row("TOTAL") = .Total
                    dt.Rows.Add(row)
                End With
            Next
            dt.AcceptChanges()

            ProgressThread.Show("Please while preparing the NSS Summary Report.")
            Dim frmViewer As New frmReportViewer()
            With frmViewer
                .LoadNSSSummary(dt)
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try        
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtNSSSummarySearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNSSSummarySearch.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.LoadParticipant()
        End If
    End Sub

    Private Sub txtNSSRASearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNSSRASearch.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.LoadParticipantNSSRA()
        End If
    End Sub

    Private Sub btnNSSSummarySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSSSummarySearch.Click
        Me.LoadParticipant()
    End Sub

    Private Sub btnNSSRASearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSSRASearch.Click
        Me.LoadParticipantNSSRA()
    End Sub

    Private Sub btnCompute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompute.Click
        Dim frm As New frmNSSMonitoringSearch
        Dim listCurrentNSSRA As New List(Of NetSettlementSurplusMain)
        Dim listBillingPeriod As New List(Of CalendarBillingPeriod)

        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            listBillingPeriod = frm.ListOfSelectedBillingPeriod
            listCurrentNSSRA = frm.ListOfNetSettlementSurplusMain
        End If

        For Each item In listCurrentNSSRA
            item.ListOfNetSettlementSurplusDetails = WBillHelper.GetNSSMonitoringDetails(item.BillingPeriod.BillingPeriod)
            item.NetSettlementSurplus = (From x In item.ListOfNetSettlementSurplusDetails _
                                         Select x.NSSAmount).Sum()
        Next

        Dim itemNSS As New FuncNetSettlementSurplus
        With itemNSS
            .ListOfBillingPeriod = listBillingPeriod
            .ListOfParticipants = WBillHelper.GetParticipantsWithNSSRA(listBillingPeriod.First.BillingPeriod, _
                                                                       listBillingPeriod.Last.BillingPeriod)
            .ListOfPreviousNetSettlementSurplusMain = WBillHelper.GetNSSMonitoringMain(listBillingPeriod.First.BillingPeriod - 3, _
                                                                                       listBillingPeriod.First.BillingPeriod - 1)
            .ListOfCurrentNetSettlmentSurplusMain = listCurrentNSSRA

            DGridViewNSSRA.DataSource = .GenerateDatatable()
        End With

    End Sub

    Private Sub btnNSSRAAdvanceSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSSRAAdvanceSearch.Click
        Try
            Dim itemNSSRA As New FuncNetSettlementSurplusAdjustment()
            Dim listBP As New List(Of CalendarBillingPeriod)

            Dim frm As New frmNSSMonitoringSearch
            If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                listBP = frm.ListOfSelectedBillingPeriod
            Else
                Exit Sub
            End If

            With itemNSSRA
                .ListOfBillingPeriod = listBP
                .ListOfNetSettlementSurplusDetails = WBillHelper.GetNSSMonitoringDetails(listBP.First.BillingPeriod, listBP.Last.BillingPeriod)
                .ListOfParticipants = WBillHelper.GetParticipantsWithNSSRA(listBP.First.BillingPeriod, listBP.Last.BillingPeriod)
            End With
            Me.DGridViewNSSRA.DataSource = itemNSSRA.GenerateDatatable()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Updated By Lance 08/19/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_NSS_InterestWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInRetrieving.ToString, AMModule.UserName)
        End Try
    End Sub

#Region "Functions/Methods"

    Private Sub LoadNSSSummary(ByVal listNSSSummary As List(Of NetSettlementSurplusSummary))
        Me.DGridNSSSummary.Rows.Clear()

        For Each item In listNSSSummary
            With item
                Me.DGridNSSSummary.Rows.Add(.BatchCode, .YearPeriod, .QuarterlyPeriod.ToString(), FormatDateTime(.AllocationDate, DateFormat.ShortDate), _
                                           .YearPeriod.ToString() & "-" & .QuarterlyPeriod.ToString(), .IDNumber.IDNumber, _
                                            .IDNumber.ParticipantID, FormatNumber(.TotalNSSInterest, 2), FormatNumber(.TotalNSSInterestNetWTax, 2), _
                                            FormatNumber(.TotalSTLInterest, 2), FormatNumber(.TotalSTLInterestNetWTax, 2), _
                                            FormatNumber(.Total, 2))
            End With
        Next
    End Sub

    Private Function GetNSSSummary() As List(Of NetSettlementSurplusSummary)
        Dim result As New List(Of NetSettlementSurplusSummary)

        For index As Integer = 0 To Me.DGridNSSSummary.RowCount - 1
            With Me.DGridNSSSummary.Rows(index)
                Dim item As New NetSettlementSurplusSummary

                item.BatchCode = CStr(.Cells("colNSSSummaryBatchCode").Value)
                item.YearPeriod = CInt(.Cells("colNSSSummaryYear").Value)
                item.QuarterlyPeriod = CType(System.Enum.Parse(GetType(EnumQuarterlyPeriod), CStr(.Cells("colNSSSummaryQuarterly").Value)), EnumQuarterlyPeriod)
                item.AllocationDate = CDate(.Cells("colNSSSummaryAllocationDate").Value)
                item.IDNumber = New AMParticipants(CStr(.Cells("colNSSSummaryIDNumber").Value), CStr(.Cells("colNSSSummaryParticipantID").Value))
                item.TotalNSSInterest = CDec(.Cells("colNSSSummaryTotalNSSInterest").Value)
                item.TotalNSSInterestNetWTax = CDec(.Cells("colNSSSummaryTotalNSSInterestNetWTax").Value)
                item.TotalSTLInterest = CDec(.Cells("colNSSSummaryTotalSTLInterest").Value)
                item.TotalSTLInterestNetWTax = CDec(.Cells("colNSSSummarySTLInterestNetWTax").Value)
                item.Total = CDec(.Cells("colNSSSummaryTotal").Value)

                result.Add(item)
            End With
        Next

        result.TrimExcess()
        Return result
    End Function

    Private Sub LoadParticipant()
        If Me.DGridNSSSummary.RowCount = 0 Then
            MsgBox("No current records!", MsgBoxStyle.Exclamation, "No data")
            Exit Sub
        End If

        If Me.txtNSSSummarySearch.Text.Trim.Length = 0 Then
            MsgBox("Specify first the participant id!", MsgBoxStyle.Critical, "Invalid")
            Me.txtNSSSummarySearch.Select()
            Exit Sub
        End If

        For Index As Integer = 0 To Me.DGridNSSSummary.RowCount - 1
            If CStr(Me.DGridNSSSummary.Rows(Index).Cells("colNSSSummaryParticipantID").Value).ToUpper() = Me.txtNSSSummarySearch.Text.Trim.ToUpper Then
                Me.DGridNSSSummary.Rows(Index).Selected = True
                Me.DGridNSSSummary.CurrentCell = Me.DGridNSSSummary.Rows(Index).Cells(3)
                Me.txtNSSSummarySearch.Text = ""
                Exit Sub
            End If
        Next

        MsgBox(Me.txtNSSSummarySearch.Text & " does not exist in grid!", MsgBoxStyle.Information, "No data")
    End Sub

    Private Function ConvertIntoQuarterPeriod(ByVal value As String) As EnumQuarterlyPeriod
        Dim result As EnumQuarterlyPeriod = Nothing

        Select Case value
            Case "1Q"
                result = EnumQuarterlyPeriod.FirstQuarter
            Case "2Q"
                result = EnumQuarterlyPeriod.SecondQuarter
            Case "3Q"
                result = EnumQuarterlyPeriod.ThirdQuarter
            Case "4Q"
                result = EnumQuarterlyPeriod.FourthQuarter
            Case Else
                Throw New ApplicationException("Invalid value for Quarterly!")
        End Select

        Return result
    End Function

    Private Function GenerateDMCM(ByVal listNSSSummary As List(Of NetSettlementSurplusSummary)) As List(Of DebitCreditMemo)
        Dim result As New List(Of DebitCreditMemo)
        Try
            Dim dmcmSignatories = WBillHelper.GetSignatories("DMCM").First()

            For Each item In listNSSSummary

                'For NSS Interest
                If item.TotalNSSInterestNetWTax <> 0 Then
                    Dim listDMCMDetails As New List(Of DebitCreditMemoDetails)

                    'For Cash in Setlement
                    listDMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.CashinBankAccountSurplusCode, Math.Abs(item.TotalNSSInterestNetWTax), _
                                                                   0, "", EnumSummaryType.INV, item.IDNumber))

                    'For Interest Payable NSS
                    listDMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.InterestPayableNSSCode, 0, _
                                                                  Math.Abs(item.TotalNSSInterestNetWTax), "", EnumSummaryType.INV, item.IDNumber))

                    Dim itemDMCM As New DebitCreditMemo
                    With itemDMCM
                        .DueDate = item.AllocationDate
                        .ChargeType = EnumChargeType.E
                        .IDNumber = item.IDNumber.IDNumber
                        .TotalAmountDue = Math.Abs(item.TotalNSSInterestNetWTax)
                        .TransType = EnumDMCMTransactionType.NSSInterest
                        .VATExempt = Math.Abs(item.TotalNSSInterestNetWTax)
                        .DMCMDetails = listDMCMDetails
                        .ApprovedBy = dmcmSignatories.Signatory_1
                        .CheckedBy = dmcmSignatories.Signatory_2
                        .Particulars = "Net Settlement Surplus Interest Allocation"
                    End With

                    result.Add(itemDMCM)
                    result.TrimExcess()
                End If

                'For Settlement Interest
                If item.TotalSTLInterestNetWTax <> 0 Then
                    Dim listDMCMDetails As New List(Of DebitCreditMemoDetails)

                    'For Cash in Setlement
                    listDMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.CashInbankSettlementcode, Math.Abs(item.TotalSTLInterestNetWTax), _
                                                                   0, "", EnumSummaryType.INV, item.IDNumber))

                    'For Interest Payable Settlement
                    listDMCMDetails.Add(New DebitCreditMemoDetails(0, AMModule.InterestPayableSTLCode, 0, _
                                                                   Math.Abs(item.TotalSTLInterestNetWTax), "", EnumSummaryType.INV, item.IDNumber))

                    Dim itemDMCM As New DebitCreditMemo
                    With itemDMCM
                        .DueDate = item.AllocationDate
                        .ChargeType = EnumChargeType.E
                        .IDNumber = item.IDNumber.IDNumber
                        .TotalAmountDue = Math.Abs(item.TotalSTLInterestNetWTax)
                        .TransType = EnumDMCMTransactionType.STLInterest
                        .VATExempt = Math.Abs(item.TotalSTLInterestNetWTax)
                        .DMCMDetails = listDMCMDetails
                        .ApprovedBy = dmcmSignatories.Signatory_1
                        .CheckedBy = dmcmSignatories.Signatory_2
                        .Particulars = "Settlement Interest Allocation"
                    End With

                    result.Add(itemDMCM)
                    result.TrimExcess()
                End If
            Next
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Private Function GenerateJornalVoucher(ByVal listDMCM As List(Of DebitCreditMemo)) As JournalVoucher
        Dim result As New JournalVoucher
        Try
            Dim jvSignatories = WBillHelper.GetSignatories("DMCM").First()

            Dim listDMCMDetails As New List(Of DebitCreditMemoDetails)

            'Get all the debit/credit details
            For Each item In listDMCM
                listDMCMDetails.AddRange(item.DMCMDetails)
                listDMCMDetails.TrimExcess()
            Next


            'For Cash in Bank NSS 
            Dim totalCashInBankNSS = (From x In listDMCMDetails _
                                      Where x.AccountCode = AMModule.CashinBankAccountSurplusCode _
                                      Select x.Debit).Sum()

            'For Interest Payable NSS
            Dim totalInterestPayableNSS = (From x In listDMCMDetails _
                                           Where x.AccountCode = AMModule.InterestPayableNSSCode _
                                           Select x.Credit).Sum()

            'For Cash in Bank Settlement 
            Dim totalCashInBankSTL = (From x In listDMCMDetails _
                                      Where x.AccountCode = AMModule.CashInbankSettlementcode _
                                      Select x.Debit).Sum()

            'For Interest Payable NSS
            Dim totalInterestPayableSTL = (From x In listDMCMDetails _
                                           Where x.AccountCode = AMModule.InterestPayableSTLCode _
                                           Select x.Credit).Sum()


            If totalCashInBankNSS + totalCashInBankSTL <> totalInterestPayableNSS + totalInterestPayableSTL Then
                Throw New ApplicationException("Debit and credit in journal voucher is not equal")
            End If

            Dim listJVDetails As New List(Of JournalVoucherDetails)

            If totalCashInBankNSS <> 0 Then
                listJVDetails.Add(New JournalVoucherDetails(0, AMModule.CashinBankAccountSurplusCode, totalCashInBankNSS, 0))
                listJVDetails.Add(New JournalVoucherDetails(0, AMModule.InterestPayableNSSCode, 0, totalInterestPayableNSS))
            End If

            If totalCashInBankSTL <> 0 Then
                listJVDetails.Add(New JournalVoucherDetails(0, AMModule.CashInbankSettlementcode, totalCashInBankSTL, 0))
                listJVDetails.Add(New JournalVoucherDetails(0, AMModule.InterestPayableSTLCode, 0, totalInterestPayableSTL))
            End If

            listJVDetails.TrimExcess()
            With result
                .ApprovedBy = jvSignatories.Signatory_1
                .CheckedBy = jvSignatories.Signatory_2
                .JVDetails = listJVDetails
                .PostedType = EnumPostedType.NSSI.ToString()
                .Remarks = "Net Settlement Surplus Interest Allocation and Settlement Interest Summary."
            End With

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Private Function GenerateGPPosted(ByVal jv As JournalVoucher) As WESMBillGPPosted
        Dim result As New WESMBillGPPosted

        Try
            Dim totalDebit = (From x In jv.JVDetails _
                          Select x.Debit).Sum()

            Dim totalCredit = (From x In jv.JVDetails _
                               Select x.Credit).Sum()

            If totalDebit <> totalCredit Then
                Throw New ApplicationException("Debit amount and credit amount are not equal!")
            End If

            With result
                .PostType = EnumPostedType.NSSI.ToString()
                .DocumentAmount = totalDebit
            End With

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Private Function GenerateEFT(ByVal listNSSSummary As List(Of NetSettlementSurplusSummary)) As List(Of EFT)
        Dim result As New List(Of EFT)

        For Each item In listNSSSummary
            Dim itemEFT As New EFT
            With itemEFT
                .AllocationDate = item.AllocationDate
                .Participant = item.IDNumber
                .PaymentType = item.IDNumber.PaymentType
                .NSSInterest = item.TotalNSSInterestNetWTax
                .STLInterest = item.TotalSTLInterestNetWTax
            End With
            result.Add(itemEFT)
        Next
        result.TrimExcess()

        Return result
    End Function

    Private Sub LoadParticipantNSSRA()
        If Me.DGridViewNSSRA.RowCount = 0 Then
            MsgBox("No current records!", MsgBoxStyle.Exclamation, "No data")
            Exit Sub
        End If

        If Me.txtNSSRASearch.Text.Trim.Length = 0 Then
            MsgBox("Specify first the participant id!", MsgBoxStyle.Critical, "Invalid")
            Me.txtNSSRASearch.Select()
            Exit Sub
        End If

        For Index As Integer = 0 To Me.DGridViewNSSRA.RowCount - 1
            If CStr(Me.DGridViewNSSRA.Rows(Index).Cells(1).Value.ToString()).ToUpper() = Me.txtNSSRASearch.Text.Trim.ToUpper Then
                Me.DGridViewNSSRA.Rows(Index).Selected = True
                Me.DGridViewNSSRA.CurrentCell = Me.DGridViewNSSRA.Rows(Index).Cells(1)
                Me.txtNSSRASearch.Text = ""
                Exit Sub
            End If
        Next

        MsgBox(Me.txtNSSRASearch.Text & " does not exist in grid!", MsgBoxStyle.Information, "No data")
    End Sub

#End Region


End Class