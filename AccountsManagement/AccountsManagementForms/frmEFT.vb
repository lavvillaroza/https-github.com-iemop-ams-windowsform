'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmEFT
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     May 16, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for Viewing of Current and Previous EFT's Created by the system
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'
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

Public Class frmEFT

    Private WBillHelper As WESMBillHelper
    Private EFTList As List(Of EFT)    
    Private isViewOnly As Boolean = False
    Private ExportH2H As ExcelHandler
    Private tmpAllocDate As Date

    Private Sub frmEFT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm

            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            ExportH2H = ExcelHandler.GetInstance()

            If isViewOnly = False Then
                Me.cbo_AllocationDate.DataSource = Me.getCBOItems()

                If cbo_AllocationDate.Items.Count = 0 Then
                    MsgBox("No EFT Records were found.", MsgBoxStyle.Critical, "EFT")
                    Exit Sub
                End If
            Else
                Me.cbo_AllocationDate.Enabled = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function getCBOItems() As List(Of Date)
        Dim retCBOItems As New List(Of Date)
        Dim _EFTList = WBillHelper.GetAMPaymentForEFT()

        retCBOItems = (From x In _EFTList _
                       Select x.AllocationDate _
                       Order By AllocationDate Descending).Distinct.ToList
        Return retCBOItems
    End Function

    Private Sub cmd_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Search.Click
        Try
            If isViewOnly = False Then
                If cbo_AllocationDate.SelectedIndex = -1 Then
                    Exit Sub
                End If
                EFTList = WBillHelper.GetAMPaymentForEFT(CDate(cbo_AllocationDate.SelectedItem.ToString))
            End If

            If EFTList.Count = 0 Then
                MsgBox("No EFT Records were found.", MsgBoxStyle.Critical, "EFT")
                Exit Sub
            End If

            dgv_dataView.Rows.Clear()
            For Each itmEFT In EFTList
                With itmEFT
                    dgv_dataView.Rows.Add(.AllocationDate.ToString("MM/dd/yyyy"), .Participant.IDNumber, .Participant.ParticipantID, .PaymentType.ToString, .CheckNumber, _
                                          FormatNumber(.ExcessCollection, 2, TriState.True, TriState.True), _
                                          FormatNumber(.DeferredEnergy, 2, TriState.True, TriState.True), _
                                          FormatNumber(.DeferredVAT, 2, TriState.True, TriState.True), _
                                          FormatNumber(.OffsetOnDeferredEnergy, 2, TriState.True, TriState.True), _
                                          FormatNumber(.OffsetOnDeferredVAT, 2, TriState.True, TriState.True), _
                                          FormatNumber(.Energy, 2, TriState.True, TriState.True), _
                                          FormatNumber(.VAT, 2, TriState.True, TriState.True), _
                                          FormatNumber(.MarketFees, 2, TriState.True, TriState.True), _
                                          FormatNumber(.TransferPrudential * -1D, 2, TriState.True, TriState.True), _
                                          FormatNumber(.TransferFinPen * -1D, 2, TriState.True, TriState.True), _
                                          FormatNumber(.NSSInterest, 2, TriState.True, TriState.True, ), _
                                          FormatNumber(.STLInterest, 2, TriState.True, TriState.True), _
                                          FormatNumber(.TotalPayment, 2, TriState.True, TriState.True))
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub cmd_ExportToCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ExportToCSV.Click
        'subExportDGVToCSV
        Me.MdiParent = MainForm
        Try

            Dim sfDialog As New SaveFileDialog

            With sfDialog
                .Filter = "CSV Files|*.csv"
                .ShowDialog()
            End With

            If Me.dgv_dataView.RowCount = 0 Then
                MsgBox("No Available data for export.", MsgBoxStyle.Critical, "No Records Found")
                Exit Sub
            End If

            If sfDialog.FileName.Trim.Length = 0 Then
                MsgBox("Export H2H file cancelled.", MsgBoxStyle.Critical, "Export Cancelled")
                Exit Sub
            End If

            Dim ds As New DataSet
            Dim lstAllParticipants = WBillHelper.GetAMParticipants()

            'Get For EFT Participants to Datatable
            Dim dt As New System.Data.DataTable
            With dt.Columns
                .Add("Payment Type [ BT / ACH / RTGS / TT]")
                .Add("Payment/Invoice Reference")
                .Add("13-digit Debit A/C No.")
                .Add("Payee/Beneficiary Name (max 35 characters)")
                .Add("Payee/Beneficiary Bank Code")
                .Add("Payee/Beneficiary A/C No.")
                .Add("Payment Details 1 (35 characters)")
                .Add("Payment Details 2 (35 characters)")
                .Add("CCY")
                .Add("Payment Amount")
                .Add("Payee Email Address")
                .Add("Payee Name 2 or Payee Address 1 (max 35 characters)")
                .Add("Payee Address 2 (max 35 characters)")
                .Add("Payment Date (MM/DD/YYYY)")
            End With

            For x = 0 To dgv_dataView.Rows.Count - 1
                Dim dr As DataRow
                dr = dt.NewRow
                'Get Participant Details

                Dim _IDNumber = CStr(dgv_dataView.Rows(x).Cells("IDNumber").Value.ToString)
                Dim _itmParticipant = (From y In lstAllParticipants _
                                       Where y.IDNumber = _IDNumber _
                                       Select y).FirstOrDefault
                If _itmParticipant.Bank = "SCB" Then
                    dr("Payment Type [ BT / ACH / RTGS / TT]") = "BT"
                Else
                    dr("Payment Type [ BT / ACH / RTGS / TT]") = "RTGS"
                End If

                Dim limitCheckPay As String() = getChecPay(_itmParticipant.CheckPay)
                dr("Payment/Invoice Reference") = "'" & _IDNumber & CDate(dgv_dataView.Rows(x).Cells("AllocDate").Value.ToString()).ToString("MMddyyyy")
                dr("13-digit Debit A/C No.") = "'" & AMModule.t13DigitDebitACNo.ToString()
                dr("Payee/Beneficiary Name (max 35 characters)") = """" & limitCheckPay(0) & """" 'Replace(Trim(Mid(_itmParticipant.CheckPay, 1, 35)), ",", "")
                dr("Payee/Beneficiary Bank Code") = _itmParticipant.BankTransactionCode
                dr("Payee/Beneficiary A/C No.") = "'" & _itmParticipant.BankAccountNo
                dr("Payment Details 1 (35 characters)") = "Payment of WESM Transactions"
                dr("Payment Details 2 (35 characters)") = If(InStr(1, _itmParticipant.BankTransactionCode.ToString, "PNB") > 0, "WESM Participant", "")
                dr("CCY") = "PHP"
                dr("Payment Amount") = FormatNumber(CDec(dgv_dataView.Rows(x).Cells("TotalPayment").Value.ToString), 2, TriState.True, TriState.True, TriState.False)
                dr("Payee Email Address") = """" & _itmParticipant.Representative.EmailAddress & """"
                dr("Payee Name 2 or Payee Address 1 (max 35 characters)") = """" & limitCheckPay(1) & """" 'If(Len(_itmParticipant.CheckPay) <= 35, "", Microsoft.VisualBasic.Mid(Trim(_itmParticipant.CheckPay), 35 + 1, 70))
                dr("Payee Address 2 (max 35 characters)") = If(Len(_itmParticipant.CheckPay) <= 70, "", Microsoft.VisualBasic.Mid(Trim(_itmParticipant.CheckPay), 71))
                dr("Payment Date (MM/DD/YYYY)") = "'" & dgv_dataView.Rows(x).Cells("AllocDate").Value.ToString()

                dt.Rows.Add(dr)
                dt.AcceptChanges()
            Next

            ds.Tables.Add(dt)

            'Dim dtForReport As New DataTable
            'dtForReport = Me.WBillHelper.BFactory.RemoveCommaForCSVExport(dt)
            Me.WBillHelper.DataTable2CSV(dt, sfDialog.FileName)
            MsgBox("Successfully exported H2H file to " & sfDialog.FileName & ".", MsgBoxStyle.Information, "Success!")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Function getChecPay(ByVal checkPay As String) As String()
        Dim arrCheckPay As String() = checkPay.Split(New Char() {" "c})
        Dim returnValue(1) As String
        Dim retVal As String = ""

        For Each word In arrCheckPay
            retVal &= " " & word
            If retVal.Length <= 35 Then
                returnValue(0) &= " " & word
            Else
                returnValue(1) &= " " & word
            End If
        Next
        Return returnValue
    End Function


    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub cmd_refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_refresh.Click
        dgv_dataView.Rows.Clear()
        Me.getCBOItems()
    End Sub

    Public Sub LoadList(ByVal lstForEFT As List(Of EFT), Optional ByVal AllocationDate As Date = Nothing)
        EFTList = lstForEFT
        tmpAllocDate = AllocationDate
        isViewOnly = True
    End Sub

End Class