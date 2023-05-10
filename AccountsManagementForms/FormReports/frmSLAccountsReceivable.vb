'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmSLAccountsReceivable
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     August 28, 2015
'Development Group:      Software Development and Support Division
'Description:            GUI for the generation of SL-AR
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib
Imports System.Text


Public Class frmSLAccountsReceivable
    Private BFactory As BusinessFactory
    Private WBillHelper As WESMBillHelper
    Private dicParticipants As Dictionary(Of String, AMParticipants)

    Private Sub frmSLAccountsReceivable_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.MdiParent = MainForm

            BFactory = BusinessFactory.GetInstance()
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString

            Me.ddlParticipantID.Items.Clear()

            'Get the participants
            dicParticipants = WBillHelper.GetDicAMParticipantsByParticipantID()

            'Add selection for All Participants
            Me.ddlParticipantID.Items.Add("---ALL---")

            'Populate the list of Participants in dorpdown control
            For Each item In dicParticipants.Keys
                Me.ddlParticipantID.Items.Add(item)
            Next
        Catch ex As Exception            
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try        
    End Sub

    Private Sub cmd_Close_Click(sender As Object, e As EventArgs) Handles cmd_Close.Click
        Me.Close()
    End Sub

    Private Sub ddlParticipantID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParticipantID.SelectedIndexChanged
        Me.txtFullName.Text = ""
        If Me.ddlParticipantID.SelectedIndex = -1 Or Me.ddlParticipantID.SelectedIndex = 0 Then
            Exit Sub
        End If

        Dim itemParticipant As AMParticipants

        itemParticipant = Me.dicParticipants(Me.ddlParticipantID.Text)
        Me.txtFullName.Text = itemParticipant.FullName
    End Sub

    Private Sub cmd_Generate_Click(sender As Object, e As EventArgs) Handles cmd_Generate.Click
        Dim listWESMBillSummaryFromCollection As List(Of WESMBillSummaryTransaction)
        Dim listWESMBillSummaryFromPayment As List(Of WESMBillSummaryTransaction)
        Dim listWESMBillSummaryFromParentChildOffsetting As List(Of WESMBillSummaryTransaction)
        Dim listWESMBillSummaryFromSPA As List(Of WESMBillSummaryTransaction)
        Dim listWESMBillSummaryWithInvoice As List(Of WESMBillSummaryWithInvoice)
        Dim listSubsidiaryLedgerAR As New List(Of SubsidiaryLedgerAR)
        Dim itemGenerate As New FuncGenerateWESMBillSummaryTransactionAR
        Dim result As New DataTable, dt As New DSReport.SLAccountsReceivablePerParticipantDataTable
        Dim itemParticipant As AMParticipants
        Dim sysDate As Date, transactionDate As Date

        Try
            sysDate = CDate(WBillHelper.GetSystemDate().ToString("MM/dd/yyyy"))

            If CDate(Me.dtTransaction.Value.ToString("MM/dd/yyyy")) > sysDate Then
                MessageBox.Show("Transaction date must be equal or less than current date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            ElseIf Me.ddlParticipantID.SelectedIndex = -1 Then
                MessageBox.Show("Please select the participant", "Specify the inputs", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ProgressThread.Show("Please wait while preparing SL Accounts Receivable Report.")

            If Me.ddlParticipantID.SelectedIndex = 0 Then
                For Each item In Me.dicParticipants.Keys
                    'Get the selected participant
                    itemParticipant = Me.dicParticipants(item)
                    'Add 1 day because the updated date of WESM Bill Summary History format is MM/DD/YYYY HH:MI:SS
                    transactionDate = CDate(Me.dtTransaction.Value.ToString("MM/dd/yyy")).AddDays(1)

                    listWESMBillSummaryFromCollection = WBillHelper.GetWESMBillSummaryFromCollection(itemParticipant.IDNumber, transactionDate)
                    listWESMBillSummaryFromPayment = WBillHelper.GetWESMBillSummaryFromPaymentAllocationForAR(itemParticipant.IDNumber, transactionDate)
                    listWESMBillSummaryFromParentChildOffsetting = WBillHelper.GetWESMBillSummaryFromParentChildOffsetting(itemParticipant.IDNumber, transactionDate)
                    listWESMBillSummaryFromSPA = WBillHelper.GetWESMBillSummaryFromSPA(itemParticipant.IDNumber, transactionDate, EnumBalanceType.AR)
                    listWESMBillSummaryWithInvoice = WBillHelper.GetWESMBillSummaryWithInvoice(itemParticipant.IDNumber, transactionDate, EnumBalanceType.AR)

                    itemGenerate.TransactionDate = transactionDate
                    itemGenerate.ListOfWESMBillTransactionFromCollection = listWESMBillSummaryFromCollection
                    itemGenerate.ListOfWESMBillTransactionFromPayment = listWESMBillSummaryFromPayment
                    itemGenerate.ListOfWESMBillTransactionFromParentChildOffsetting = listWESMBillSummaryFromParentChildOffsetting
                    itemGenerate.ListOfWESMBillTransactionFromSPA = listWESMBillSummaryFromSPA
                    itemGenerate.ListOfWESMBillSummaryInvoice = listWESMBillSummaryWithInvoice

                    'Generate the List of Subsidiary Ledger
                    listSubsidiaryLedgerAR = itemGenerate.PopulateSubsidiaryLedger()

                    'Generate the Datatable
                    BFactory.GenerateSubsidiaryLedgerAccountsReceivablePerParticipant(itemParticipant, listSubsidiaryLedgerAR, dt)
                Next
            Else
                'Get the selected participant
                itemParticipant = Me.dicParticipants(Me.ddlParticipantID.Text)

                'Add 1 day because the updated date of WESM Bill Summary History format is MM/DD/YYYY HH:MI:SS
                transactionDate = CDate(Me.dtTransaction.Value.ToString("MM/dd/yyy")).AddDays(1)

                listWESMBillSummaryFromCollection = WBillHelper.GetWESMBillSummaryFromCollection(itemParticipant.IDNumber, transactionDate)
                listWESMBillSummaryFromPayment = WBillHelper.GetWESMBillSummaryFromPaymentAllocationForAR(itemParticipant.IDNumber, transactionDate)
                listWESMBillSummaryFromParentChildOffsetting = WBillHelper.GetWESMBillSummaryFromParentChildOffsetting(itemParticipant.IDNumber, transactionDate)
                listWESMBillSummaryFromSPA = WBillHelper.GetWESMBillSummaryFromSPA(itemParticipant.IDNumber, transactionDate, EnumBalanceType.AR)
                listWESMBillSummaryWithInvoice = WBillHelper.GetWESMBillSummaryWithInvoice(itemParticipant.IDNumber, transactionDate, EnumBalanceType.AR)

                itemGenerate.TransactionDate = transactionDate
                itemGenerate.ListOfWESMBillTransactionFromCollection = listWESMBillSummaryFromCollection
                itemGenerate.ListOfWESMBillTransactionFromPayment = listWESMBillSummaryFromPayment
                itemGenerate.ListOfWESMBillTransactionFromParentChildOffsetting = listWESMBillSummaryFromParentChildOffsetting
                itemGenerate.ListOfWESMBillTransactionFromSPA = listWESMBillSummaryFromSPA
                itemGenerate.ListOfWESMBillSummaryInvoice = listWESMBillSummaryWithInvoice

                'Generate the List of Subsidiary Ledger
                listSubsidiaryLedgerAR = itemGenerate.PopulateSubsidiaryLedger()

                'Generate the Datatable
                BFactory.GenerateSubsidiaryLedgerAccountsReceivablePerParticipant(itemParticipant, listSubsidiaryLedgerAR, dt)

            End If

            If dt.Rows.Count = 0 AndAlso dt Is Nothing Then
                ProgressThread.Close()
                MessageBox.Show("Nothing to generate!", "No data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            result = dt

            Dim rptView As New frmReportViewer
            With rptView
                .LoadSLAccountsReceivablePerParticipant(result, transactionDate.AddDays(-1))
                ProgressThread.Close()
                .ShowDialog()
            End With

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_ExportToCSV_Click(sender As Object, e As EventArgs) Handles btn_ExportToCSV.Click
        Dim listWESMBillSummaryFromCollection As List(Of WESMBillSummaryTransaction)
        Dim listWESMBillSummaryFromPayment As List(Of WESMBillSummaryTransaction)
        Dim listWESMBillSummaryFromParentChildOffsetting As List(Of WESMBillSummaryTransaction)
        Dim listWESMBillSummaryFromSPA As List(Of WESMBillSummaryTransaction)
        Dim listWESMBillSummaryWithInvoice As List(Of WESMBillSummaryWithInvoice)
        Dim listSubsidiaryLedgerAR As New List(Of SubsidiaryLedgerAR)
        Dim itemGenerate As New FuncGenerateWESMBillSummaryTransactionAR
        Dim result As New DataTable, dt As New DSReport.SLAccountsReceivablePerParticipantDataTable
        Dim itemParticipant As AMParticipants
        Dim sysDate As Date, transactionDate As Date

        Try
            sysDate = CDate(WBillHelper.GetSystemDate().ToString("MM/dd/yyyy"))

            If CDate(Me.dtTransaction.Value.ToString("MM/dd/yyyy")) > sysDate Then
                MessageBox.Show("Transaction date must be equal or less than current date", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            ElseIf Me.ddlParticipantID.SelectedIndex = -1 Then
                MessageBox.Show("Please select the participant", "Specify the inputs", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim ans As New MsgBoxResult
            Dim _fldrSelect As New FolderBrowserDialog
            Dim fPathName As String = ""

            ans = MsgBox("Do you really want to export the records as CSV Format?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Export records")
            If ans <> MsgBoxResult.Yes Then
                Exit Sub
            End If


            With _fldrSelect
                .ShowDialog()
                If .SelectedPath = "" Then
                    Exit Sub
                End If
                fPathName = .SelectedPath & "\SL_AccountReceivable_" & Format(Me.dtTransaction.Value, "MMddyyyy") & ".csv"
            End With

            ProgressThread.Show("Please wait while preparing SL Accounts Receivable Report.")

            If Me.ddlParticipantID.SelectedIndex = 0 Then
                For Each item In Me.dicParticipants.Keys
                    'Get the selected participant
                    itemParticipant = Me.dicParticipants(item)

                    'Add 1 day because the updated date of WESM Bill Summary History format is MM/DD/YYYY HH:MI:SS
                    transactionDate = CDate(Me.dtTransaction.Value.ToString("MM/dd/yyy")).AddDays(1)

                    listWESMBillSummaryFromCollection = WBillHelper.GetWESMBillSummaryFromCollection(itemParticipant.IDNumber, transactionDate)
                    listWESMBillSummaryFromPayment = WBillHelper.GetWESMBillSummaryFromPaymentAllocationForAR(itemParticipant.IDNumber, transactionDate)
                    listWESMBillSummaryFromParentChildOffsetting = WBillHelper.GetWESMBillSummaryFromParentChildOffsetting(itemParticipant.IDNumber, transactionDate)
                    listWESMBillSummaryFromSPA = WBillHelper.GetWESMBillSummaryFromSPA(itemParticipant.IDNumber, transactionDate, EnumBalanceType.AR)
                    listWESMBillSummaryWithInvoice = WBillHelper.GetWESMBillSummaryWithInvoice(itemParticipant.IDNumber, transactionDate, EnumBalanceType.AR)

                    itemGenerate.TransactionDate = transactionDate
                    itemGenerate.ListOfWESMBillTransactionFromCollection = listWESMBillSummaryFromCollection
                    itemGenerate.ListOfWESMBillTransactionFromPayment = listWESMBillSummaryFromPayment
                    itemGenerate.ListOfWESMBillTransactionFromParentChildOffsetting = listWESMBillSummaryFromParentChildOffsetting
                    itemGenerate.ListOfWESMBillTransactionFromSPA = listWESMBillSummaryFromSPA
                    itemGenerate.ListOfWESMBillSummaryInvoice = listWESMBillSummaryWithInvoice

                    'Generate the List of Subsidiary Ledger
                    listSubsidiaryLedgerAR = itemGenerate.PopulateSubsidiaryLedger()

                    'Generate the Datatable
                    BFactory.GenerateSubsidiaryLedgerAccountsReceivablePerParticipant(itemParticipant, listSubsidiaryLedgerAR, dt)
                Next
            Else
                'Get the selected participant
                itemParticipant = Me.dicParticipants(Me.ddlParticipantID.Text)

                'Add 1 day because the updated date of WESM Bill Summary History format is MM/DD/YYYY HH:MI:SS
                transactionDate = CDate(Me.dtTransaction.Value.ToString("MM/dd/yyy")).AddDays(1)

                listWESMBillSummaryFromCollection = WBillHelper.GetWESMBillSummaryFromCollection(itemParticipant.IDNumber, transactionDate)
                listWESMBillSummaryFromPayment = WBillHelper.GetWESMBillSummaryFromPaymentAllocationForAR(itemParticipant.IDNumber, transactionDate)
                listWESMBillSummaryFromParentChildOffsetting = WBillHelper.GetWESMBillSummaryFromParentChildOffsetting(itemParticipant.IDNumber, transactionDate)
                listWESMBillSummaryFromSPA = WBillHelper.GetWESMBillSummaryFromSPA(itemParticipant.IDNumber, transactionDate, EnumBalanceType.AR)
                listWESMBillSummaryWithInvoice = WBillHelper.GetWESMBillSummaryWithInvoice(itemParticipant.IDNumber, transactionDate, EnumBalanceType.AR)

                itemGenerate.TransactionDate = transactionDate
                itemGenerate.ListOfWESMBillTransactionFromCollection = listWESMBillSummaryFromCollection
                itemGenerate.ListOfWESMBillTransactionFromPayment = listWESMBillSummaryFromPayment
                itemGenerate.ListOfWESMBillTransactionFromParentChildOffsetting = listWESMBillSummaryFromParentChildOffsetting
                itemGenerate.ListOfWESMBillTransactionFromSPA = listWESMBillSummaryFromSPA
                itemGenerate.ListOfWESMBillSummaryInvoice = listWESMBillSummaryWithInvoice

                'Generate the List of Subsidiary Ledger
                listSubsidiaryLedgerAR = itemGenerate.PopulateSubsidiaryLedger()

                'Generate the Datatable
                BFactory.GenerateSubsidiaryLedgerAccountsReceivablePerParticipant(itemParticipant, listSubsidiaryLedgerAR, dt)

            End If

            result = dt
            
            If result.Rows.Count = 0 AndAlso dt Is Nothing Then
                ProgressThread.Close()
                MessageBox.Show("Nothing to generate!", "No data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Me.CreateCSV(result, fPathName)

            ProgressThread.Close()
            MessageBox.Show("Successfully exported records to " & fPathName, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub CreateCSV(dt As DataTable, filename As String)

        Dim strBldr As New StringBuilder()
        'Header
        strBldr.Append("ID_NUMBER,REFERENCE_NUMBER,CHARGE_TYPE,TRANS_DATE,AMOUNT,CURRENT_AMOUNT,AMOUNT31TO60,AMOUNT61TO90,AMOUNT91OVER")
        strBldr.AppendLine()

        'Data
        Dim sumTotalCurrAmount As Decimal = 0, sumTotalAmount31To60 As Decimal = 0, sumTotalAmount61To90 As Decimal = 0, sumTotalAmount91Over As Decimal = 0
        For Each item In (From x In dt.AsEnumerable() Select x.Field(Of String)("ID_NUMBER")).Distinct.ToList()
            Dim totalCurrAmount As Decimal = 0, totalAmount31To60 As Decimal = 0, totalAmount61To90 As Decimal = 0, totalAmount91Over As Decimal = 0

            For Each perIDNumber In (From x In dt.AsEnumerable() Where x.Field(Of String)("ID_NUMBER") = item Select x).ToList
                strBldr.Append(perIDNumber.Field(Of String)("ID_NUMBER") & Chr(44))
                strBldr.Append(perIDNumber.Field(Of String)("REFERENCE_NUMBER") & Chr(44))
                strBldr.Append(perIDNumber.Field(Of String)("CHARGE_TYPE") & Chr(44))
                strBldr.Append(perIDNumber.Field(Of Date)("TRANS_DATE") & Chr(44))
                strBldr.Append(perIDNumber.Field(Of Decimal)("AMOUNT") & Chr(44))
                strBldr.Append(perIDNumber.Field(Of Decimal)("CURRENT_AMOUNT") & Chr(44))
                strBldr.Append(perIDNumber.Field(Of Decimal)("AMOUNT31TO60") & Chr(44))
                strBldr.Append(perIDNumber.Field(Of Decimal)("AMOUNT61TO90") & Chr(44))
                strBldr.Append(perIDNumber.Field(Of Decimal)("AMOUNT91OVER"))
                strBldr.AppendLine()
                totalCurrAmount += perIDNumber.Field(Of Decimal)("CURRENT_AMOUNT")
                totalAmount31To60 += perIDNumber.Field(Of Decimal)("AMOUNT31TO60")
                totalAmount61To90 += perIDNumber.Field(Of Decimal)("AMOUNT61TO90")
                totalAmount91Over += perIDNumber.Field(Of Decimal)("AMOUNT91OVER")
            Next

            strBldr.Append("Aged Totals:" & Chr(44))
            strBldr.Append(totalCurrAmount & Chr(44))
            strBldr.Append(totalAmount31To60 & Chr(44))
            strBldr.Append(totalAmount61To90 & Chr(44))
            strBldr.Append(totalAmount91Over & Chr(44))
            strBldr.Append("Due:" & Chr(44))
            strBldr.Append(totalCurrAmount + totalAmount31To60 + totalAmount61To90 + totalAmount91Over)
            strBldr.AppendLine()

            sumTotalCurrAmount += totalCurrAmount
            sumTotalAmount31To60 += totalAmount31To60
            sumTotalAmount61To90 += totalAmount61To90
            sumTotalAmount91Over += totalAmount91Over

        Next

        strBldr.Append("Sum of Aged Totals:" & Chr(44))
        strBldr.Append(sumTotalCurrAmount & Chr(44))
        strBldr.Append(sumTotalAmount31To60 & Chr(44))
        strBldr.Append(sumTotalAmount61To90 & Chr(44))
        strBldr.Append(sumTotalAmount91Over & Chr(44))
        strBldr.Append("Sum of Due:" & Chr(44))
        strBldr.Append(sumTotalCurrAmount + sumTotalAmount31To60 + sumTotalAmount61To90 + sumTotalAmount91Over)
        strBldr.AppendLine()

        Using fs As FileStream = File.Create(filename)
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(strBldr.ToString)
            fs.Write(info, 0, info.Length)
        End Using
    End Sub
End Class