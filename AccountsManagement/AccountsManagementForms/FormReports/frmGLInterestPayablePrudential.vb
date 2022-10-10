'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmGLInterestPayablePrudential
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 13, 2015
'Development Group:      Software Development and Support Division
'Description:            GUI for the generation of Deferred Payment
'Arguments/Parameters:  
'Files/Database Tables:  frmGLInterestPayablePrudential
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'

Option Explicit On
Option Strict On
Imports System
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmGLInterestPayablePrudential
    Private BFactory As BusinessFactory
    Private WBillHelper As WESMBillHelper
    Private PrudentialMod As PrudentialModel

    Private Sub frmGLInterestPayablePrudential_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        BFactory = BusinessFactory.GetInstance()
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
    End Sub

    Private Sub cmd_Generate_Click(sender As Object, e As EventArgs) Handles cmd_Generate.Click
        Dim dt As New DSReport.GeneralLedgerInterestPayablePrudentialDataTable
        Dim result As New DataTable
        Dim listGLInterestPayablePrudential As New List(Of GeneralLedgerInterestPayablePrudential)
        Dim startDate As Date, endDate As Date, transactionDate As Date
        Dim beginningBalance As Decimal
        Dim itemAcctCode As AccountingCode

        Try
            startDate = CDate(Me.dtp_From.Value.ToString("MM/dd/yyyy"))
            endDate = CDate(Me.dtp_To.Value.ToString("MM/dd/yyyy"))

            If startDate > endDate Then
                MessageBox.Show("Invalid", "Invalid Date Range!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            'Get the Begin Prudential Interest
            listGLInterestPayablePrudential.AddRange(WBillHelper.GetGeneralLedgerInterestPayableBeginPrudentialInterest(startDate, endDate))

            'Get the Prudential Interest
            listGLInterestPayablePrudential.AddRange(WBillHelper.GetGeneralLedgerInterestPayablePrudentialInterest(startDate, endDate))

            'Get the Transfer of Prudential Interest
            listGLInterestPayablePrudential.AddRange(WBillHelper.GetGeneralLedgerInterestPayablePrudentialTransferInterest(startDate, endDate))

            listGLInterestPayablePrudential.TrimExcess()

            'If listGLInterestPayablePrudential.Count = 0 Then
            '    MessageBox.Show("No record found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End If

            'Generate the Datatable
            result = BFactory.GenerateGeneralLedgerInterestPayablePrudential(startDate, endDate, listGLInterestPayablePrudential, dt)

            'Get and Generate the Beginning Balance
            transactionDate = CDate(Me.dtp_From.Value.ToString("MM/dd/yyyy")).AddDays(-1)

            'beginningBalance = WBillHelper.GetAMBeginningBalance(AMModule.InterestPayablePRCode).Amount
            beginningBalance += WBillHelper.GetGeneralLedgerInterestPayablePrudentialTransferInterestBeginning(transactionDate).Debit
            beginningBalance -= WBillHelper.GetGeneralLedgerInterestPayablePrudentialInterestBeginning(transactionDate).Credit
            beginningBalance -= WBillHelper.GetGeneralLedgerInterestPayableBeginPrudentialInterestBeginning(transactionDate).Credit

            'beginningBalance = WBillHelper.GetAMBeginningBalance(AMModule.InterestPayablePRCode).Amount + _
            '                   WBillHelper.GetGeneralLedgerInterestPayablePrudentialTransferInterestBeginning(transactionDate).Debit - _
            '                   WBillHelper.GetGeneralLedgerInterestPayablePrudentialInterestBeginning(transactionDate).Credit - _
            '                   WBillHelper.GetGeneralLedgerInterestPayableBeginPrudentialInterestBeginning(transactionDate).Credit

            'Get the accounting code
            itemAcctCode = WBillHelper.GetAccountingCode(AMModule.InterestPayablePRCode)
            Dim rptView As New frmReportViewer
            With rptView
                .LoadGLInterestPayablePrudential(result, AMModule.InterestPayablePRCode, itemAcctCode.Description, beginningBalance)                
                .ShowDialog()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error found.")        
        End Try
    End Sub

    Private Sub btn_ExportToText_Click(sender As Object, e As EventArgs) Handles btn_ExportToText.Click
        Dim listGLInterestPayablePrudential As New List(Of GeneralLedgerInterestPayablePrudential)
        Dim startDate As Date, endDate As Date, transactionDate As Date
        Dim beginningBalance As Decimal
        Dim itemAcctCode As AccountingCode
        Dim _fldrSelect As New FolderBrowserDialog
        Dim fPathName As String = ""
        Dim header() As String = {AMModule.CompanyFullName, AMModule.CompanyAddress, "VAT REG TIN " & AMModule.CompanyTinNumber, "",
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
            fPathName = .SelectedPath & "\GL_InterestPayablePrudential_" & Format(Me.dtp_From.Value, "MMddyyyy") & "-" & _
                        Format(Me.dtp_To.Value, "MMddyyyy") & ".txt"

            'fPathName = .SelectedPath & "\SummaryOfAccountingBooks.xlsx"
        End With
        ProgressThread.Show("Please wait generating the report")

        Try
            startDate = CDate(Me.dtp_From.Value.ToString("MM/dd/yyyy"))
            endDate = CDate(Me.dtp_To.Value.ToString("MM/dd/yyyy"))

            If startDate > endDate Then
                ProgressThread.Close()
                MessageBox.Show("Invalid", "Invalid Date Range!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            'Get the Begin Prudential Interest
            listGLInterestPayablePrudential.AddRange(WBillHelper.GetGeneralLedgerInterestPayableBeginPrudentialInterest(startDate, endDate))

            'Get the Prudential Interest
            listGLInterestPayablePrudential.AddRange(WBillHelper.GetGeneralLedgerInterestPayablePrudentialInterest(startDate, endDate))

            'Get the Transfer of Prudential Interest
            listGLInterestPayablePrudential.AddRange(WBillHelper.GetGeneralLedgerInterestPayablePrudentialTransferInterest(startDate, endDate))

            listGLInterestPayablePrudential.TrimExcess()

            'Get and Generate the Beginning Balance
            transactionDate = CDate(Me.dtp_From.Value.ToString("MM/dd/yyyy")).AddDays(-1)

            beginningBalance += WBillHelper.GetGeneralLedgerInterestPayablePrudentialTransferInterestBeginning(transactionDate).Debit
            beginningBalance -= WBillHelper.GetGeneralLedgerInterestPayablePrudentialInterestBeginning(transactionDate).Credit
            beginningBalance -= WBillHelper.GetGeneralLedgerInterestPayableBeginPrudentialInterestBeginning(transactionDate).Credit

            'Get the accounting code
            itemAcctCode = WBillHelper.GetAccountingCode(AMModule.InterestPayablePRCode)


            If listGLInterestPayablePrudential.Count = 0 Then
                ProgressThread.Close()
                MessageBox.Show("No record found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            'Get the accounting code
            Me.CreateTxTFile(startDate, endDate, listGLInterestPayablePrudential, fPathName, header, itemAcctCode)

            ProgressThread.Close()
            MsgBox("Successfully exported records to " & fPathName, MsgBoxStyle.Information)

        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error found.")
        Finally
            listGLInterestPayablePrudential = New List(Of GeneralLedgerInterestPayablePrudential)
        End Try
    End Sub


    Private Sub CreateTxTFile(ByVal startdate As Date, ByVal enddate As Date, ByVal listGLInterestPayablePrudential As List(Of GeneralLedgerInterestPayablePrudential),
                              ByVal filename As String, header() As String, ByVal itemAcctCode As AccountingCode)

        Dim strBldr As New StringBuilder()
        For Each itm In header
            strBldr.Append(itm)
            strBldr.AppendLine()
        Next

        strBldr.AppendLine()
        strBldr.Append("Accounting Books / File Attributes / Layout Difference")
        strBldr.AppendLine()

        strBldr.Append("File Name: General Ledger - Interest Payable Prudential")
        strBldr.AppendLine()

        strBldr.Append("File Type: Text File")
        strBldr.AppendLine()

        strBldr.Append("Number of Records: " & (listGLInterestPayablePrudential.Count).ToString("N0"))
        strBldr.AppendLine()

        Dim getTotal As Decimal = Math.Abs((From x In listGLInterestPayablePrudential Select x.Debit).Sum - (From x In listGLInterestPayablePrudential Select x.Credit).Sum)
        strBldr.Append("Amount Field Control Total: " & (getTotal).ToString("n"))
        strBldr.AppendLine()

        strBldr.Append("Period Covered: " & startdate.ToString("MMMM") & " to " & enddate.ToString("MMMM") & " " & enddate.ToString("yyyy"))
        strBldr.AppendLine()

        strBldr.Append("Transaction cut-off Date/Time: " & enddate.ToString("MMMM") & " " & System.DateTime.DaysInMonth(enddate.Year, enddate.Month) & ", " & enddate.Year & "/11:59 PM")
        strBldr.AppendLine()

        strBldr.Append("Extracted By: " & AMModule.UserName)
        strBldr.AppendLine()

        strBldr.Append("Extracted Date/Time: " & WBillHelper.GetSystemDateTime.ToString("MMM-dd-yyyy hh:mm tt"))
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
        For Each item In listGLInterestPayablePrudential

            With item

                If .JournalNumber = 0 And .ReferenceNumber = 0 Then
                    strBldr.Append(LengthLoop(("None").ToString, ("None").ToString.Length, 20)) 'JV Number
                    strBldr.Append(LengthLoop(Format(.TransactionDate, "MM/dd/yyyy"),
                                      Format(.TransactionDate, "MM/dd/yyyy").ToString.Length, 20)) 'Transaction Date
                    strBldr.Append(LengthLoop(("None").ToString, ("None").ToString.Length, 20)) 'Reference Number
                Else
                    strBldr.Append(LengthLoop(BFactory.GenerateBIRDocumentNumber(.JournalNumber, BIRDocumentsType.JournalVoucher),
                                   BFactory.GenerateBIRDocumentNumber(.JournalNumber, BIRDocumentsType.JournalVoucher).ToString.Length, 20)) 'JV Number
                    strBldr.Append(LengthLoop(Format(.TransactionDate, "MM/dd/yyyy"),
                                      Format(.TransactionDate, "MM/dd/yyyy").ToString.Length, 20)) 'Transaction Date
                    strBldr.Append(LengthLoop(BFactory.GenerateBIRDocumentNumber(.ReferenceNumber, BIRDocumentsType.JournalVoucher),
                                   BFactory.GenerateBIRDocumentNumber(.ReferenceNumber, .TransactionType).ToString.Length, 20)) 'Reference Number
                End If

                strBldr.Append(LengthLoop(.Description.ToString, .Description.ToString.Length, 45))         'Description
                strBldr.Append(LengthLoop(.ParticipantName, .ParticipantName.Length, 75))                   'Participant
                strBldr.Append(LengthLoop(itemAcctCode.AccountCode, itemAcctCode.AccountCode.Length, 25))   'Accounting Code
                strBldr.Append(LengthLoop(itemAcctCode.Description, itemAcctCode.Description.Length, 65))   'Accounting Description
                strBldr.Append(LengthLoop(Math.Abs(.Debit).ToString, Math.Abs(.Debit).ToString.Length, 25)) 'Debit
                strBldr.Append(LengthLoop(Math.Abs(.Credit).ToString, Math.Abs(.Credit).ToString.Length, 25)) 'Credit
                strBldr.AppendLine()
            End With
        Next

        Using fs As FileStream = File.Create(filename)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(strBldr.ToString)
            fs.Write(info, 0, info.Length)
        End Using

    End Sub


    Private Sub CreateCSVDatFile(ByVal startdate As Date, ByVal enddate As Date, ByVal listGLInterestPayablePrudential As List(Of GeneralLedgerInterestPayablePrudential),
                              ByVal filename As String, header() As String, ByVal itemAcctCode As AccountingCode, ByVal filetype As String)

        Dim strBldr As New StringBuilder()
        For Each itm In header
            strBldr.Append(itm)
            strBldr.AppendLine()
        Next

        strBldr.AppendLine()
        strBldr.Append("Accounting Books / File Attributes / Layout Difference")
        strBldr.AppendLine()

        strBldr.Append("File Name: General Ledger - Interest Payable Prudential")
        strBldr.AppendLine()

        strBldr.Append("File Type: " & filetype & " File")
        strBldr.AppendLine()

        strBldr.Append(Chr(34) & "Number of Records: " & (listGLInterestPayablePrudential.Count).ToString("N0") & Chr(34))
        strBldr.AppendLine()

        Dim getTotal As Decimal = Math.Abs((From x In listGLInterestPayablePrudential Select x.Debit).Sum - (From x In listGLInterestPayablePrudential Select x.Credit).Sum)
        If (getTotal).ToString("N0").Length > 4 Then
            strBldr.Append(Chr(34) & "Amount Field Control Total: ")
            strBldr.Append((getTotal).ToString("n") & Chr(34))
        Else
            strBldr.Append("Amount Field Control Total: ")
            strBldr.Append((getTotal).ToString("n"))
        End If

        strBldr.Append("Period Covered: " & startdate.ToString("MMMM") & " to " & enddate.ToString("MMMM") & " " & enddate.ToString("yyyy"))
        strBldr.AppendLine()

        strBldr.Append(Chr(34) & "Transaction cut-off Date/Time: " & enddate.ToString("MMMM") & " " & System.DateTime.DaysInMonth(enddate.Year, enddate.Month) & ", " & enddate.Year & "/06:00 PM" & Chr(34))
        strBldr.AppendLine()

        strBldr.Append("Extracted By: " & AMModule.UserName)
        strBldr.AppendLine()

        strBldr.Append("Extracted Date/Time: " & WBillHelper.GetSystemDateTime.ToString("MMM-dd-yyyy hh:mm tt"))
        strBldr.AppendLine()
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
        strBldr.Append("295,")
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
        For Each item In listGLInterestPayablePrudential
            With item
                If .JournalNumber = 0 And .ReferenceNumber = 0 Then
                    strBldr.Append(("None").ToString & Chr(44)) 'JV Number
                    strBldr.Append(Format(.TransactionDate, "MM/dd/yyyy") & Chr(44)) 'Transaction Date
                    strBldr.Append(("None").ToString & Chr(44)) 'Reference Number
                Else
                    strBldr.Append(BFactory.GenerateBIRDocumentNumber(.JournalNumber, BIRDocumentsType.JournalVoucher) & Chr(44)) 'JV Number
                    strBldr.Append(Format(.TransactionDate, "MM/dd/yyyy") & Chr(44)) 'Transaction Date
                    strBldr.Append(BFactory.GenerateBIRDocumentNumber(.ReferenceNumber, .TransactionType) & Chr(44)) 'Reference Number
                End If

                strBldr.Append(.Description.ToString & Chr(44)) 'Description
                strBldr.Append(Chr(34) & .ParticipantName & Chr(34) & Chr(44))                   'Participant
                strBldr.Append(itemAcctCode.AccountCode & Chr(44))   'Accounting Code
                strBldr.Append(itemAcctCode.Description & Chr(44))   'Accounting Description
                strBldr.Append(Math.Abs(.Debit).ToString & Chr(44)) 'Debit
                strBldr.Append(Math.Abs(.Credit).ToString) 'Credit
                strBldr.AppendLine()
            End With
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
        Dim listGLInterestPayablePrudential As New List(Of GeneralLedgerInterestPayablePrudential)
        Dim startDate As Date, endDate As Date, transactionDate As Date
        Dim beginningBalance As Decimal
        Dim itemAcctCode As AccountingCode
        Dim _fldrSelect As New FolderBrowserDialog
        Dim fPathName As String = ""
        Dim header() As String = {AMModule.CompanyFullName, Chr(34) & AMModule.CompanyAddress & Chr(34), "VAT REG TIN " & AMModule.CompanyTinNumber, "",
                  "CAS PERMIT NO." & AMModule.BIRCASPermit, "SOFTWARE SYSTEM: ACCOUNTS MANAGEMENT SYSTEM", "VERSION NO. " & AMModule.SystemVersion}
        Dim ans As New MsgBoxResult

        ans = MsgBox("Do you really want to export the records as CSV Format?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Export records")
        If ans <> MsgBoxResult.Yes Then
            Exit Sub
        End If


        With _fldrSelect
            .ShowDialog()
            If .SelectedPath = "" Then
                Exit Sub
            End If
            fPathName = .SelectedPath & "\GL_InterestPayablePrudential_" & Format(Me.dtp_From.Value, "MMddyyyy") & "-" & _
                        Format(Me.dtp_To.Value, "MMddyyyy") & ".csv"

        End With
        ProgressThread.Show("Please wait generating the report")

        Try
            startDate = CDate(Me.dtp_From.Value.ToString("MM/dd/yyyy"))
            endDate = CDate(Me.dtp_To.Value.ToString("MM/dd/yyyy"))

            If startDate > endDate Then
                ProgressThread.Close()
                MessageBox.Show("Invalid", "Invalid Date Range!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            'Get the Begin Prudential Interest
            listGLInterestPayablePrudential.AddRange(WBillHelper.GetGeneralLedgerInterestPayableBeginPrudentialInterest(startDate, endDate))

            'Get the Prudential Interest
            listGLInterestPayablePrudential.AddRange(WBillHelper.GetGeneralLedgerInterestPayablePrudentialInterest(startDate, endDate))

            'Get the Transfer of Prudential Interest
            listGLInterestPayablePrudential.AddRange(WBillHelper.GetGeneralLedgerInterestPayablePrudentialTransferInterest(startDate, endDate))

            listGLInterestPayablePrudential.TrimExcess()

            'Get and Generate the Beginning Balance
            transactionDate = CDate(Me.dtp_From.Value.ToString("MM/dd/yyyy")).AddDays(-1)

            beginningBalance += WBillHelper.GetGeneralLedgerInterestPayablePrudentialTransferInterestBeginning(transactionDate).Debit
            beginningBalance -= WBillHelper.GetGeneralLedgerInterestPayablePrudentialInterestBeginning(transactionDate).Credit
            beginningBalance -= WBillHelper.GetGeneralLedgerInterestPayableBeginPrudentialInterestBeginning(transactionDate).Credit

            'Get the accounting code
            itemAcctCode = WBillHelper.GetAccountingCode(AMModule.InterestPayablePRCode)

            If listGLInterestPayablePrudential.Count = 0 Then
                ProgressThread.Close()
                MessageBox.Show("No record found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            'Get the accounting code
            Me.CreateCSVDatFile(startDate, endDate, listGLInterestPayablePrudential, fPathName, header, itemAcctCode, "CSV")

            ProgressThread.Close()
            MsgBox("Successfully exported records to " & fPathName, MsgBoxStyle.Information)

        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error found.")
        Finally
            listGLInterestPayablePrudential = New List(Of GeneralLedgerInterestPayablePrudential)
        End Try
    End Sub

    Private Sub btn_ExportToDat_Click(sender As Object, e As EventArgs) Handles btn_ExportToDat.Click
        Dim listGLInterestPayablePrudential As New List(Of GeneralLedgerInterestPayablePrudential)
        Dim startDate As Date, endDate As Date, transactionDate As Date
        Dim beginningBalance As Decimal
        Dim itemAcctCode As AccountingCode
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
            fPathName = .SelectedPath & "\GL_InterestPayablePrudential_" & Format(Me.dtp_From.Value, "MMddyyyy") & "-" & _
                        Format(Me.dtp_To.Value, "MMddyyyy") & ".dat"

        End With
        ProgressThread.Show("Please wait generating the report")

        Try
            startDate = CDate(Me.dtp_From.Value.ToString("MM/dd/yyyy"))
            endDate = CDate(Me.dtp_To.Value.ToString("MM/dd/yyyy"))

            If startDate > endDate Then
                ProgressThread.Close()
                MessageBox.Show("Invalid", "Invalid Date Range!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            'Get the Begin Prudential Interest
            listGLInterestPayablePrudential.AddRange(WBillHelper.GetGeneralLedgerInterestPayableBeginPrudentialInterest(startDate, endDate))

            'Get the Prudential Interest
            listGLInterestPayablePrudential.AddRange(WBillHelper.GetGeneralLedgerInterestPayablePrudentialInterest(startDate, endDate))

            'Get the Transfer of Prudential Interest
            listGLInterestPayablePrudential.AddRange(WBillHelper.GetGeneralLedgerInterestPayablePrudentialTransferInterest(startDate, endDate))

            listGLInterestPayablePrudential.TrimExcess()

            'Get and Generate the Beginning Balance
            transactionDate = CDate(Me.dtp_From.Value.ToString("MM/dd/yyyy")).AddDays(-1)

            beginningBalance += WBillHelper.GetGeneralLedgerInterestPayablePrudentialTransferInterestBeginning(transactionDate).Debit
            beginningBalance -= WBillHelper.GetGeneralLedgerInterestPayablePrudentialInterestBeginning(transactionDate).Credit
            beginningBalance -= WBillHelper.GetGeneralLedgerInterestPayableBeginPrudentialInterestBeginning(transactionDate).Credit

            'Get the accounting code
            itemAcctCode = WBillHelper.GetAccountingCode(AMModule.InterestPayablePRCode)


            If listGLInterestPayablePrudential.Count = 0 Then
                ProgressThread.Close()
                MessageBox.Show("No record found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            'Get the accounting code
            Me.CreateCSVDatFile(startDate, endDate, listGLInterestPayablePrudential, fPathName, header, itemAcctCode, "DAT")

            ProgressThread.Close()
            MsgBox("Successfully exported records to " & fPathName, MsgBoxStyle.Information)

        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error found.")
        Finally
            listGLInterestPayablePrudential = New List(Of GeneralLedgerInterestPayablePrudential)
        End Try
    End Sub
End Class