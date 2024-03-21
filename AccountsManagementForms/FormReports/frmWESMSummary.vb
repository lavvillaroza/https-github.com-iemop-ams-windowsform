'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmWESMBillSummary
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 19, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for the WESM Bills Summary and it's details if any
'Arguments/Parameters:  
'Files/Database Tables:  AM_WESM_BILL_SUMMARY, AM_OFFSET_P2PC2C_DETAILS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'   September 19, 2011      Juan Carlo L. Panopio           Finished form GUI and functionalities but no
'                                                           Generation of Report yet.(Viewing Only)
'   September 20, 2011      Juan Carlo L. Panopio           Completed Filtering for WESM Bill Summaries
'   September 21, 2011      Juan Carlo L. Panopio           Removed AM Code in Viewing
'   September 21, 2011      Juan Carlo L. Panopio           Completed Summary Report and Summary with Details Report

Option Strict On
Option Explicit On


Imports System.IO
Imports System.Data
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports Excel = Microsoft.Office.Interop.Excel
Imports WESMLib.Auth.Lib


Public Class frmWESMSummary

    Dim WBillHelper As WESMBillHelper
    Dim BillPeriodDueDate As New List(Of WESMBillSummary)
    Dim getWESMTransSummary As New List(Of WESMBillAllocCoverSummary)
    Dim getWESMInvoices As New List(Of WESMInvoice)
    Dim getWESMBillComputed As New List(Of WESMBill)
    Dim getWESMBillFromDB As New List(Of WESMBill)
    Dim getChargeCode As New List(Of ChargeId)
    Dim getAMParticipants As New List(Of AMParticipants)
    Private Enum EnumFilterType
        Participant
        Charge
        BillingPeriod
        DueDate
    End Enum

    Private _FilterChange As Boolean    

    Private Sub frmWESMSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName        
        Try                        
            getAMParticipants = Me.WBillHelper.GetAMParticipants()
            getChargeCode = Me.WBillHelper.GetChargeIDCodes()
            'Set Check Boxes
            Me.chkParticipantID.Checked = False
            Me.cbo_ParticipantId.Enabled = False

            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

#Region "Form Controls for enable/disable of combo boxes"
    Private Sub chkParticipantID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkParticipantID.CheckedChanged        
        If getAMParticipants.Count = 0 Then
            MsgBox("No participants found. Please check the selected invoice date then try again.", MsgBoxStyle.Exclamation, "System Message")
        End If

        Me.cbo_ParticipantId.Enabled = Me.chkParticipantID.Checked
        If Me.chkParticipantID.Checked Then
            For Each item In getAMParticipants
                Me.cbo_ParticipantId.Items.Add(item.ParticipantID)
            Next
            If Me.cbo_ParticipantId.Items.Count = 0 Then
                MsgBox("No participants found. Please check the master file then try again.", MsgBoxStyle.Exclamation, "Search")
                Me.chkParticipantID.Checked = False
                Me.cbo_ParticipantId.Enabled = False
                Exit Sub
            End If
            Me.cbo_ParticipantId.SelectedIndex = 0
        Else
            Me.cbo_ParticipantId.SelectedIndex = -1
        End If
    End Sub
#End Region

    Private Sub cmd_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Close.Click
        Me.Close()
    End Sub

    Private Sub cmd_ExportInExcel_Click(sender As Object, e As EventArgs) Handles cmd_ExportInExcel.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim TargetPath As String = ""
        'Dim SelectedYear As String = Me.ddlYear_cmb.Text
        Try
            getWESMBillComputed = New List(Of WESMBill)
            ProgressThread.Show("Please wait while gerating report.")
            If cbo_ParticipantId.SelectedIndex = -1 Then
                getWESMTransSummary = Me.WBillHelper.GetListWESMTransSummary(CDate(dtFrom.Value), CDate(dtTo.Value), "")
                getWESMInvoices = Me.WBillHelper.GetWESMInvoices(CDate(dtFrom.Value), CDate(dtTo.Value), "")
                getWESMBillFromDB = Me.WBillHelper.GetWESMBills(CDate(dtFrom.Value), CDate(dtTo.Value), "")
            Else
                Dim getIDNumber As String = (From x In getAMParticipants Where x.ParticipantID = CStr(cbo_ParticipantId.SelectedItem) Select x.IDNumber).FirstOrDefault
                getWESMTransSummary = Me.WBillHelper.GetListWESMTransSummary(CDate(dtFrom.Value), CDate(dtTo.Value), CStr(getIDNumber))
                getWESMInvoices = Me.WBillHelper.GetWESMInvoices(CDate(dtFrom.Value), CDate(dtTo.Value), CStr(getIDNumber))
                getWESMBillFromDB = Me.WBillHelper.GetWESMBills(CDate(dtFrom.Value), CDate(dtTo.Value), CStr(getIDNumber))
            End If

            If getWESMInvoices.Count = 0 And (getWESMTransSummary.Count = 0 And getWESMBillFromDB.Count = 0) Then
                ProgressThread.Close()
                MsgBox("No records found", MsgBoxStyle.Exclamation, "System Message")
                Exit Sub
            End If

            Dim listWESMBill As New List(Of WESMBill)

            For Each item In getWESMInvoices
                Dim newWESMBill As New WESMBill
                Dim chargeIdLib As String = item.ChargeID.ToUpper
                Dim ChargeId = (From x In getChargeCode Where Replace(x.ChargeId, " ", "") = Replace(chargeIdLib.ToUpper, " ", "")).First()
                With newWESMBill
                    .BillingPeriod = item.BillingPeriod
                    .IDNumber = item.IDNumber
                    .RegistrationID = item.RegistrationID
                    .InvoiceDate = item.InvoiceDate
                    .InvoiceNumber = item.InvoiceNumber
                    .Amount = item.Amount
                    .ChargeType = ChargeId.cIDType
                    .DueDate = item.DueDate
                    .MarketFeesRate = item.MarketFeesRate
                    .Remarks = item.Remarks
                    .SettlementRun = item.SettlementRun
                End With
                listWESMBill.Add(newWESMBill)
            Next

            Dim tmpList = From x In listWESMBill Group x By
                      x.BillingPeriod, x.SettlementRun, x.Remarks, x.ChargeType, x.IDNumber, x.RegistrationID, x.InvoiceNumber
                      Into Amount = Sum(x.Amount)
                          Select New With {.BillingPeriod = BillingPeriod,
                                       .SettlementRun = SettlementRun,
                                       .Remarks = Remarks,
                                       .ChargeType = ChargeType,
                                       .IDNumber = IDNumber,
                                       .RegistrationID = RegistrationID,
                                       .Amount = Amount,
                                       .InvoiceNumber = InvoiceNumber}
            For Each item In tmpList
                Dim selectedItem = item

                Dim itemWESMBill = (From x In listWESMBill
                                    Where x.IDNumber = selectedItem.IDNumber And
                                x.RegistrationID = selectedItem.RegistrationID And
                                x.ChargeType = selectedItem.ChargeType And
                                x.SettlementRun = selectedItem.SettlementRun And
                                x.BillingPeriod = selectedItem.BillingPeriod And
                                x.InvoiceNumber = selectedItem.InvoiceNumber
                                    Select x).First()

                Dim itemNewWESMBill As New WESMBill
                With itemNewWESMBill
                    .BillingPeriod = selectedItem.BillingPeriod
                    .IDNumber = selectedItem.IDNumber
                    .RegistrationID = selectedItem.RegistrationID
                    .ChargeType = selectedItem.ChargeType
                    .Amount = selectedItem.Amount
                    .SettlementRun = selectedItem.SettlementRun
                    .ForTheAccountOf = itemWESMBill.ForTheAccountOf
                    .FullName = itemWESMBill.FullName
                    .InvoiceDate = itemWESMBill.InvoiceDate
                    .InvoiceNumber = itemWESMBill.InvoiceNumber
                    .DueDate = itemWESMBill.DueDate
                    .MarketFeesRate = itemWESMBill.MarketFeesRate
                    .Remarks = itemWESMBill.Remarks
                End With
                getWESMBillComputed.Add(itemNewWESMBill)
                getWESMBillComputed.TrimExcess()
            Next

            For Each item In getWESMTransSummary
                Dim itemNewWESMBillEnergy As New WESMBill
                With itemNewWESMBillEnergy
                    .BillingPeriod = item.BillingPeriod
                    .IDNumber = item.StlID
                    .RegistrationID = item.BillingID
                    .ChargeType = EnumChargeType.E
                    .Amount = item.NetSale + item.NetPurchase
                    .SettlementRun = item.STLRun
                    .InvoiceDate = item.TransactionDate
                    .InvoiceNumber = item.TransactionNo
                    .DueDate = item.DueDate
                    .MarketFeesRate = item.MarketFeesRate
                    .Remarks = item.Remarks
                End With
                getWESMBillComputed.Add(itemNewWESMBillEnergy)
                getWESMBillComputed.TrimExcess()
                Dim itemNewWESMBillVAT As New WESMBill
                With itemNewWESMBillVAT
                    .BillingPeriod = item.BillingPeriod
                    .IDNumber = item.StlID
                    .RegistrationID = item.BillingID
                    .ChargeType = EnumChargeType.EV
                    .Amount = item.VatOnSales + item.VatOnPurchases
                    .SettlementRun = item.STLRun
                    .InvoiceDate = item.TransactionDate
                    .InvoiceNumber = item.TransactionNo
                    .DueDate = item.DueDate
                    .MarketFeesRate = item.MarketFeesRate
                    .Remarks = item.Remarks
                End With
                getWESMBillComputed.Add(itemNewWESMBillVAT)
                getWESMBillComputed.TrimExcess()
            Next


            ProgressThread.Close()
            With sFolderDialog
                .ShowDialog()
                If .SelectedPath.ToString.Trim.Length = 0 Then
                    Exit Sub
                Else
                    TargetPath = sFolderDialog.SelectedPath
                End If
            End With
            ProgressThread.Show("Please wait while processing.")

            If chkParticipantID.Checked = True Then
                CreateOutstandingSummaryReport(TargetPath, CStr(cbo_ParticipantId.SelectedItem), CDate(dtFrom.Value), CDate(dtTo.Value))
            Else
                CreateOutstandingSummaryReport(TargetPath, "", CDate(dtFrom.Value), CDate(dtTo.Value))
                ProgressThread.Close()
            End If
            ProgressThread.Close()
            MessageBox.Show("Generating Reports are successfully.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#Region "Method and Functions For Generation of Collection And Payment Report on MS-Excel"
    Public Sub CreateOutstandingSummaryReport(ByVal SavingPathName As String, ByVal selectedparticipant As String, ByVal selectedFrom As Nullable(Of Date), ByVal selectedTo As Nullable(Of Date))
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet1 As Excel.Worksheet
        Dim xlWorkSheet2 As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlWESMBill As Excel.Range
        Dim xlWESMInvoice As Excel.Range

        Dim GetDateNow As Date = CDate(AMModule.SystemDate.ToShortDateString)
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()

        Dim WESMBillSummaryHeader As Object(,) = New Object(,) {}
        ReDim WESMBillSummaryHeader(1, 13)
        WESMBillSummaryHeader(1, 0) = "Description"
        WESMBillSummaryHeader(1, 1) = "BillingPeriod"
        WESMBillSummaryHeader(1, 2) = "SettlmentRun"
        WESMBillSummaryHeader(1, 3) = "Document Number"
        WESMBillSummaryHeader(1, 4) = "Document Date"
        WESMBillSummaryHeader(1, 5) = "Customer ID"
        WESMBillSummaryHeader(1, 6) = "Billing ID"
        WESMBillSummaryHeader(1, 7) = "Short Name"
        WESMBillSummaryHeader(1, 8) = "Trading Participant Name"
        WESMBillSummaryHeader(1, 9) = "Energy/MarketFees Amount"
        WESMBillSummaryHeader(1, 10) = "VAT"
        WESMBillSummaryHeader(1, 11) = "EWT"
        WESMBillSummaryHeader(1, 12) = "Due Date"
        WESMBillSummaryHeader(1, 13) = "Charge Type"

        'WESM Invoice Table sheets
        xlWorkSheet1 = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet1.Name = "WESM Transactions For MRD"
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(2, UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
        xlWESMInvoice = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlWESMInvoice.Value = WESMBillSummaryHeader
        Dim lrow1 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1        
        If getWESMBillComputed.Count <> 0 Then
            Dim WESMBillSUmmaryArr As Object(,) = Me.GenerateWESMBillArray(getWESMBillComputed, getWESMTransSummary)
            xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow1, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow1 + UBound(WESMBillSUmmaryArr, 1), UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
            xlWESMInvoice = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
            xlWESMInvoice.Value = WESMBillSUmmaryArr
        End If
        'End  ****************************************************************************************************************************************************

        'WESM Bills Table sheets
        If xlWorkBook.Sheets.Count = 1 Then
            xlWorkBook.Sheets.Add(After:=xlWorkBook.Sheets(1))
        End If
        xlWorkSheet2 = CType(xlWorkBook.Sheets(2), Excel.Worksheet)
        xlWorkSheet2.Name = "WESM Transactions For AMU"
        xlRowRange1 = DirectCast(xlWorkSheet2.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet2.Cells(2, UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
        xlWESMBill = xlWorkSheet2.Range(xlRowRange1, xlRowRange2)
        xlWESMBill.Value = WESMBillSummaryHeader
        Dim lrow2 As Integer = xlWorkSheet2.Range("A" & xlWorkSheet2.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
        If getWESMBillFromDB.Count <> 0 Then
            Dim WESMBillSUmmaryArr As Object(,) = Me.GenerateWESMBillArray(getWESMBillFromDB, getWESMTransSummary)
            xlRowRange1 = DirectCast(xlWorkSheet2.Cells(lrow2, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet2.Cells(lrow2 + UBound(WESMBillSUmmaryArr, 1), UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
            xlWESMBill = xlWorkSheet2.Range(xlRowRange1, xlRowRange2)
            xlWESMBill.Value = WESMBillSUmmaryArr
        End If
        'End ****************************************************************************************************************************************************

        Dim FileName As String        
        If selectedparticipant.Length <> 0 And Not selectedFrom Is Nothing And Not selectedTo Is Nothing Then
            FileName = "WESM Transactions_" & Format(selectedFrom, "MMMddyyyy") & " To " & Format(selectedTo, "MMMddyyyy") & " (" & selectedparticipant & ")"
        Else
            FileName = "WESM Transactions_" & Format(selectedFrom, "MMMddyyyy") & " To " & Format(selectedTo, "MMMddyyyy")
        End If

        xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbookMacroEnabled, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        xlWorkBook.Close(False)
        xlApp.Quit()

        releaseObject(xlWESMBill)
        releaseObject(xlRowRange2)
        releaseObject(xlRowRange1)
        releaseObject(xlWorkSheet1)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)
    End Sub

    Private Function GenerateWESMBillArray(ByVal WESMBillList As List(Of WESMBill), ByVal WESMAllocTransList As List(Of WESMBillAllocCoverSummary)) As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim RowCount As Integer = WESMBillList.Count()

        Dim WBSArr As Object(,) = New Object(,) {}
        ReDim WBSArr(RowCount, 13)

        Dim getDistinctTransNo As List(Of String) = (From x In WESMBillList Select x.InvoiceNumber Distinct).ToList

        For Each item In getDistinctTransNo
            Dim getWESMBills As List(Of WESMBill) = (From x In WESMBillList Where x.InvoiceNumber = item Select x).ToList
            Dim getAMParticipantInfo = (From x In getAMParticipants Where x.IDNumber = getWESMBills.Select(Function(y) y.IDNumber).First Select x).FirstOrDefault

            Dim getWESMBillBaseAmount As WESMBill = (From x In getWESMBills Where x.ChargeType = EnumChargeType.E Or x.ChargeType = EnumChargeType.MF Select x).FirstOrDefault
            Dim getWESMBillVATAmount As WESMBill = (From x In getWESMBills Where x.ChargeType = EnumChargeType.EV Or x.ChargeType = EnumChargeType.MFV Select x).FirstOrDefault
            Dim getWESMAllocTransSummary As WESMBillAllocCoverSummary = (From x In WESMAllocTransList Where x.TransactionNo = item Select x).FirstOrDefault

            WBSArr(RowIndex, 0) = getWESMBills.Select(Function(x) x.Remarks).First
            WBSArr(RowIndex, 1) = getWESMBills.Select(Function(x) x.BillingPeriod).First
            WBSArr(RowIndex, 2) = getWESMBills.Select(Function(x) x.SettlementRun).First
            WBSArr(RowIndex, 3) = getWESMBills.Select(Function(x) x.InvoiceNumber).First
            WBSArr(RowIndex, 4) = getWESMBills.Select(Function(x) x.InvoiceDate).First
            WBSArr(RowIndex, 5) = getWESMBills.Select(Function(x) x.IDNumber).First
            WBSArr(RowIndex, 6) = getWESMBills.Select(Function(x) x.RegistrationID).First
            WBSArr(RowIndex, 7) = getAMParticipantInfo.ParticipantID
            WBSArr(RowIndex, 8) = getAMParticipantInfo.FullName
            WBSArr(RowIndex, 12) = getWESMBills.Select(Function(x) x.DueDate).First

            If Not getWESMBillBaseAmount Is Nothing Then
                WBSArr(RowIndex, 9) = getWESMBillBaseAmount.Amount
                If getWESMBills.Select(Function(x) x.SettlementRun).First.Contains("R") Then
                    WBSArr(RowIndex, 13) = If(getWESMBillBaseAmount.ChargeType = EnumChargeType.E, "RESERVE", "MFR")
                Else
                    WBSArr(RowIndex, 13) = If(getWESMBillBaseAmount.ChargeType = EnumChargeType.E, "ENERGY", "MF")
                End If
            Else
                WBSArr(RowIndex, 9) = 0
            End If

            If Not getWESMBillVATAmount Is Nothing Then
                WBSArr(RowIndex, 10) = getWESMBillVATAmount.Amount
                If getWESMBills.Select(Function(x) x.SettlementRun).First.Contains("R") Then
                    WBSArr(RowIndex, 13) = If(getWESMBillBaseAmount.ChargeType = EnumChargeType.E, "RESERVE", "MFR")
                Else
                    WBSArr(RowIndex, 13) = If(getWESMBillBaseAmount.ChargeType = EnumChargeType.E, "ENERGY", "MF")
                End If
            Else
                WBSArr(RowIndex, 10) = 0
            End If

            If Not getWESMAllocTransSummary Is Nothing Then
                WBSArr(RowIndex, 11) = getWESMAllocTransSummary.EWTSales + getWESMAllocTransSummary.EWTPurchases
            Else
                WBSArr(RowIndex, 11) = 0
            End If
            RowIndex += 1
        Next

        ret = WBSArr

        Return ret
    End Function

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
#End Region

    
End Class