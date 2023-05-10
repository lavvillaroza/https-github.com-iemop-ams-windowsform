Imports System.IO
Imports System.Data
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports Excel = Microsoft.Office.Interop.Excel
Imports WESMLib.Auth.Lib

Public Class frmExportWTASummary
    Dim WBillHelper As WESMBillHelper
    Private Sub frmWESMTransAllocSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        Try
            FillUpDropDownDueDate()
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub FillUpDropDownDueDate()
        Dim listOfDueDate As List(Of Date) = WBillHelper.GetWESMTransactionAllocationSummaryDueDate()
        Try
            ddlDueDate.Items.Clear()
            For Each item In listOfDueDate
                ddlDueDate.Items.Add(item)
            Next
            ddlDueDate.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmd_ExportInExcel_Click(sender As Object, e As EventArgs) Handles cmd_ExportInExcel.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim TargetPath As String = ""
        Try
            ProgressThread.Show("Please wait while gerating report.")
            ProgressThread.Close()
            With sFolderDialog
                .ShowDialog()
                If .SelectedPath.ToString.Trim.Length = 0 Then
                    Exit Sub
                Else
                    TargetPath = sFolderDialog.SelectedPath
                End If
            End With
            If ddlDueDate.SelectedIndex = -1 Then
                Exit Sub
            End If

            Dim dueDate As Date = CDate(ddlDueDate.Text)
            ProgressThread.Show("Please wait while processing.")
            CreateWTACoverSummaryReport(TargetPath, dueDate)
            ProgressThread.Close()
            MessageBox.Show("Generating Reports are successfully.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#Region "Method and Functions For Generation of Collection And Payment Report on MS-Excel"
    Public Sub CreateWTACoverSummaryReport(ByVal SavingPathName As String, ByVal dueDate As Date)
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

        Dim listOfWTACSummary As List(Of WESMBillAllocCoverSummary) = WBillHelper.GetListWESMTransCoverSummaryAllPerDueDate(dueDate)
        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()

        Dim WESMBillSummaryHeader1 As Object(,) = New Object(,) {}
        ReDim WESMBillSummaryHeader1(1, 27)
        WESMBillSummaryHeader1(1, 0) = "SUMMARY_ID"
        WESMBillSummaryHeader1(1, 1) = "STL_RUN"
        WESMBillSummaryHeader1(1, 2) = "BILLING_PERIOD"
        WESMBillSummaryHeader1(1, 3) = "STL_ID"
        WESMBillSummaryHeader1(1, 4) = "BILLING_ID"
        WESMBillSummaryHeader1(1, 5) = "NON_VATABLE_TAG"
        WESMBillSummaryHeader1(1, 6) = "ZERO_RATED_TAG"
        WESMBillSummaryHeader1(1, 7) = "WHT_TAG"
        WESMBillSummaryHeader1(1, 8) = "ITH_TAG"
        WESMBillSummaryHeader1(1, 9) = "NET_SELLER_BUYER_TAG"
        WESMBillSummaryHeader1(1, 10) = "TRANSACTION_NO"
        WESMBillSummaryHeader1(1, 11) = "TRANSACTION_DATE"
        WESMBillSummaryHeader1(1, 12) = "DUE_DATE"
        WESMBillSummaryHeader1(1, 13) = "VATABLE_SALES"
        WESMBillSummaryHeader1(1, 14) = "ZERO_RATED_SALES"
        WESMBillSummaryHeader1(1, 15) = "ZERO_RATED_ECOZONE_SALES"
        WESMBillSummaryHeader1(1, 16) = "VATABLE_PURCHASES"
        WESMBillSummaryHeader1(1, 17) = "ZERO_RATED_PURCHASES"
        WESMBillSummaryHeader1(1, 18) = "ZERO_RATED_ECOZONE_PURCHASES"
        WESMBillSummaryHeader1(1, 19) = "NSS_FLOWBACK"
        WESMBillSummaryHeader1(1, 20) = "VAT_ON_SALES"
        WESMBillSummaryHeader1(1, 21) = "VAT_ON_PURCHASES"
        WESMBillSummaryHeader1(1, 22) = "EWT_SALES"
        WESMBillSummaryHeader1(1, 23) = "EWT_PURCHASES"
        WESMBillSummaryHeader1(1, 24) = "SPOT_QTY"
        WESMBillSummaryHeader1(1, 25) = "GMR"
        WESMBillSummaryHeader1(1, 26) = "GENX_AMT"
        WESMBillSummaryHeader1(1, 27) = "REMARKS"

        'WESM Transaction Allocation Cover Summary sheets
        xlWorkSheet1 = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet1.Name = "WESMTransAllocCoverSummary"
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(2, UBound(WESMBillSummaryHeader1, 2) + 1), Excel.Range)
        xlWESMInvoice = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlWESMInvoice.Value = WESMBillSummaryHeader1
        Dim lrow1 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
        If listOfWTACSummary.Count <> 0 Then
            Dim WESMBillSUmmaryArr As Object(,) = Me.GenerateWTACSummaryArray(listOfWTACSummary)
            xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow1, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow1 + UBound(WESMBillSUmmaryArr, 1), UBound(WESMBillSummaryHeader1, 2) + 1), Excel.Range)
            xlWESMInvoice = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
            xlWESMInvoice.Value = WESMBillSUmmaryArr
        End If
        'End  ****************************************************************************************************************************************************

        Dim WESMBillSummaryHeader2 As Object(,) = New Object(,) {}
        ReDim WESMBillSummaryHeader2(1, 17)
        WESMBillSummaryHeader2(1, 0) = "SUMMARY_ID"
        WESMBillSummaryHeader2(1, 1) = "STL_ID"
        WESMBillSummaryHeader2(1, 2) = "BILLING_ID"
        WESMBillSummaryHeader2(1, 3) = "FACILITY_TYPE"
        WESMBillSummaryHeader2(1, 4) = "WHT"
        WESMBillSummaryHeader2(1, 5) = "ITH"
        WESMBillSummaryHeader2(1, 6) = "NON_VATABLE_TAG"
        WESMBillSummaryHeader2(1, 7) = "ZERO_RATED_TAG"
        WESMBillSummaryHeader2(1, 8) = "NET_SELLER_BUYER_TAG"
        WESMBillSummaryHeader2(1, 9) = "VATABLE_SALES"
        WESMBillSummaryHeader2(1, 10) = "ZERO_RATED_SALES"
        WESMBillSummaryHeader2(1, 11) = "ZERO_RATED_ECOZONE_SALES"
        WESMBillSummaryHeader2(1, 12) = "VAT_ON_SALES"
        WESMBillSummaryHeader2(1, 13) = "VATABLE_PURCHASES"
        WESMBillSummaryHeader2(1, 14) = "ZERO_RATED_PURCHASES"
        WESMBillSummaryHeader2(1, 15) = "ZERO_RATED_ECOZONE_PURCHASES"
        WESMBillSummaryHeader2(1, 16) = "VAT_ON_PURCHASES"
        WESMBillSummaryHeader2(1, 17) = "EWT"

        'WESM Transaction Allocation Details sheets
        If xlWorkBook.Sheets.Count = 1 Then
            xlWorkBook.Sheets.Add(After:=xlWorkBook.Sheets(1))
        End If
        xlWorkSheet2 = CType(xlWorkBook.Sheets(2), Excel.Worksheet)
        xlWorkSheet2.Name = "WESMTransAllocDetails"
        xlRowRange1 = DirectCast(xlWorkSheet2.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet2.Cells(2, UBound(WESMBillSummaryHeader2, 2) + 1), Excel.Range)
        xlWESMBill = xlWorkSheet2.Range(xlRowRange1, xlRowRange2)
        xlWESMBill.Value = WESMBillSummaryHeader2
        Dim lrow2 As Integer = xlWorkSheet2.Range("A" & xlWorkSheet2.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
        If listOfWTACSummary.Count <> 0 Then
            Dim WESMBillSUmmaryArr As Object(,) = Me.GenerateWTADetailsArray(listOfWTACSummary)
            xlRowRange1 = DirectCast(xlWorkSheet2.Cells(lrow2, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet2.Cells(lrow2 + UBound(WESMBillSUmmaryArr, 1), UBound(WESMBillSummaryHeader2, 2) + 1), Excel.Range)
            xlWESMBill = xlWorkSheet2.Range(xlRowRange1, xlRowRange2)
            xlWESMBill.Value = WESMBillSUmmaryArr
        End If
        'End ****************************************************************************************************************************************************

        Dim FileName As String = "WTASummaryList_" & Format(dueDate, "MMddyyyy") & ".xlsx"

        xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
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

    Private Function GenerateWTACSummaryArray(ByVal wESMTransAllocList As List(Of WESMBillAllocCoverSummary)) As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim RowCount As Integer = wESMTransAllocList.Count()

        Dim WBSArr As Object(,) = New Object(,) {}
        ReDim WBSArr(RowCount, 27)

        For Each item In wESMTransAllocList
            Dim getWTACSummary As WESMBillAllocCoverSummary = item
            WBSArr(RowIndex, 0) = getWTACSummary.SummaryId
            WBSArr(RowIndex, 1) = getWTACSummary.STLRun
            WBSArr(RowIndex, 2) = getWTACSummary.BillingPeriod
            WBSArr(RowIndex, 3) = getWTACSummary.StlID
            WBSArr(RowIndex, 4) = getWTACSummary.BillingID
            WBSArr(RowIndex, 5) = getWTACSummary.NonVatableTag
            WBSArr(RowIndex, 6) = getWTACSummary.ZeroRatedTag
            WBSArr(RowIndex, 7) = getWTACSummary.WHT
            WBSArr(RowIndex, 8) = getWTACSummary.ITH
            WBSArr(RowIndex, 9) = getWTACSummary.NetSellerBuyerTag
            WBSArr(RowIndex, 10) = getWTACSummary.TransactionNo
            WBSArr(RowIndex, 11) = getWTACSummary.TransactionDate.ToShortDateString
            WBSArr(RowIndex, 12) = getWTACSummary.DueDate.ToShortDateString
            WBSArr(RowIndex, 13) = getWTACSummary.VatableSales
            WBSArr(RowIndex, 14) = getWTACSummary.ZeroRatedSales
            WBSArr(RowIndex, 15) = getWTACSummary.ZeroRatedEcoZoneSales
            WBSArr(RowIndex, 16) = getWTACSummary.VatablePurchases
            WBSArr(RowIndex, 17) = getWTACSummary.ZeroRatedPurchases
            WBSArr(RowIndex, 18) = getWTACSummary.ZeroRatedEcoZonePurchases
            WBSArr(RowIndex, 19) = getWTACSummary.NSSFlowBack
            WBSArr(RowIndex, 20) = getWTACSummary.VatOnSales
            WBSArr(RowIndex, 21) = getWTACSummary.VatOnPurchases
            WBSArr(RowIndex, 22) = getWTACSummary.EWTSales
            WBSArr(RowIndex, 23) = getWTACSummary.EWTPurchases
            WBSArr(RowIndex, 24) = getWTACSummary.SpotQty
            WBSArr(RowIndex, 25) = getWTACSummary.GMR
            WBSArr(RowIndex, 26) = getWTACSummary.GenXAmount
            WBSArr(RowIndex, 27) = getWTACSummary.Remarks
            RowIndex += 1
        Next
        ret = WBSArr

        Return ret
    End Function


    Private Function GenerateWTADetailsArray(ByVal wESMTransAllocList As List(Of WESMBillAllocCoverSummary)) As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim RowCount As Integer = 0

        Dim WBSArr As Object(,) = New Object(,) {}
        For Each item In wESMTransAllocList
            RowCount += item.ListWBAllocDisDetails.Count
        Next
        ReDim WBSArr(RowCount, 17)

        For Each item In wESMTransAllocList
            Dim getWTADetailsSummary As WESMBillAllocCoverSummary = item
            For Each oitem In getWTADetailsSummary.ListWBAllocDisDetails
                WBSArr(RowIndex, 0) = getWTADetailsSummary.SummaryId
                WBSArr(RowIndex, 1) = oitem.STLID
                WBSArr(RowIndex, 2) = oitem.BillingID
                WBSArr(RowIndex, 3) = oitem.FacilityType
                WBSArr(RowIndex, 4) = oitem.WHTTag
                WBSArr(RowIndex, 5) = oitem.ITHTag
                WBSArr(RowIndex, 6) = oitem.NonVatableTag
                WBSArr(RowIndex, 7) = oitem.ZeroRatedTag
                WBSArr(RowIndex, 8) = oitem.NetSellerBuyerTag
                WBSArr(RowIndex, 9) = oitem.VatableSales
                WBSArr(RowIndex, 10) = oitem.ZeroRatedSales
                WBSArr(RowIndex, 11) = oitem.ZeroRatedEcoZoneSales
                WBSArr(RowIndex, 12) = oitem.VatOnSales
                WBSArr(RowIndex, 13) = oitem.VatablePurchases
                WBSArr(RowIndex, 14) = oitem.ZeroRatedPurchases
                WBSArr(RowIndex, 15) = oitem.ZeroRatedEcoZonePurchases
                WBSArr(RowIndex, 16) = oitem.VatOnPurchases
                WBSArr(RowIndex, 17) = oitem.EWT
                RowIndex += 1
            Next
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