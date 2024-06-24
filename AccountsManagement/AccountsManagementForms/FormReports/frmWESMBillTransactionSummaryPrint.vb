Option Explicit On
Option Strict On

Imports AccountsManagementObjects
Imports AccountsManagementLogic
Imports CrystalDecisions.Shared
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Threading.Tasks

Public Class frmWESMBillTransactionSummaryPrint
    Private WBTransSummaryPrintHelper As New WESMBillTransSummaryPrintHelper
    Private WBHelper As WESMBillHelper
    Private Enum EChargeType
        ENERGY
        MARKETFEES
    End Enum
    Private Sub frmWESMBillTransactionSummaryPrint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        WBHelper = WESMBillHelper.GetInstance
        Me.LoadBillingPeriod()
        'AddHandler Me.rbEnergy.CheckedChanged, AddressOf Me.rbChargeTypeCheckedChange
        'AddHandler Me.rbMarketFees.CheckedChanged, AddressOf Me.rbChargeTypeCheckedChange
        'Me.rbEnergy.Checked = True
    End Sub

    Private Sub UpdateProgress(_ProgressMsg As ProgressClass)
        ToolStripStatus_LabelMsg.Text = _ProgressMsg.ProgressMsg
        ctrl_statusStrip.Refresh()
    End Sub

    Private Sub LoadBillingPeriod()
        Me.ddlBillingPeriod.Items.Clear()
        Try
            Dim listOfBPNo As List(Of Integer) = WBTransSummaryPrintHelper.GetListBillingperiod()
            If listOfBPNo.Count = 0 Then
                MessageBox.Show("No available records", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            For Each item In listOfBPNo.OrderByDescending(Function(x) x).ToList
                Me.ddlBillingPeriod.Items.Add(item)
            Next
            Me.ddlBillingPeriod.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ddlBillingPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBillingPeriod.SelectedIndexChanged
        Dim selectedBPNo As Integer = CInt(Me.ddlBillingPeriod.Text)
        Me.LoadSTLRun(selectedBPNo)
    End Sub

    Private Sub LoadSTLRun(ByVal selectedBPno As Integer)
        Me.ddlSTLRun.Items.Clear()
        Try
            For Each item In WBTransSummaryPrintHelper.GetListSTLRun(selectedBPno)
                Me.ddlSTLRun.Items.Add(item)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ddlSTLRun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSTLRun.SelectedIndexChanged
        Dim selectedBPNo As Integer = CInt(Me.ddlBillingPeriod.Text)
        Dim selectedSTLRun As String = CStr(Me.ddlSTLRun.Text)
        Me.LoadParticipant(selectedBPNo, selectedSTLRun)
    End Sub

    Private Sub LoadParticipant(ByVal bpNo As Integer, ByVal stlRun As String)
        Try
            Me.chckList.Items.Clear()
            For Each item In WBTransSummaryPrintHelper.GetListParticipant(bpNo, stlRun)
                Me.chckList.Items.Add(item)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chckAll_CheckedChanged(sender As Object, e As EventArgs) Handles chckAll.CheckedChanged
        If Me.chckAll.Checked Then
            For index As Integer = 0 To Me.chckList.Items.Count - 1
                Me.chckList.SetItemChecked(index, True)
            Next
        Else
            For index As Integer = 0 To Me.chckList.Items.Count - 1
                Me.chckList.SetItemChecked(index, False)
            Next
        End If
    End Sub

    Private Sub btnExportToPDF_Click(sender As Object, e As EventArgs) Handles btnExportToPDF.Click
        Try
            Dim fOpen As New FolderBrowserDialog
            Dim filePath As String = ""

            fOpen.ShowDialog()
            filePath = fOpen.SelectedPath

            If filePath.Length = 0 Then
                MessageBox.Show("Please select valid file path!", "System Sucess Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ans As DialogResult = New DialogResult
            ans = MessageBox.Show("Do you really want to export the WESM Transactions?", "Exporting WESM Transctions", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            ProgressThread.Show("Please wait while preparing the file.")

            Dim selectedBPNo As Integer = CInt(Me.ddlBillingPeriod.Text)
            Dim selectedSTLRun As String = CStr(Me.ddlSTLRun.Text)
            Dim ListCalendarBP As List(Of CalendarBillingPeriod) = WBHelper.GetCalendarBP()
            Dim getCalendarBP As CalendarBillingPeriod = (From x In ListCalendarBP Where x.BillingPeriod = selectedBPNo Select x).First
            Dim ListParticipantInfo As List(Of AMParticipants) = WBHelper.GetAMParticipants()

            Dim bpValue As String = getCalendarBP.EndDate.ToString("MMMyyyy").ToUpper

            If selectedSTLRun = "F" Then
                Dim fileName As String = "_WTA"
                For cnt As Integer = 0 To Me.chckList.CheckedItems.Count - 1
                    Dim participant = Me.chckList.CheckedItems(cnt).ToString()
                    Dim getParticpantInfo As AMParticipants = (From x In ListParticipantInfo Where x.IDNumber = participant Select x).First

                    Dim listWESMTransSummary As New List(Of WESMBillAllocCoverSummary)

                    listWESMTransSummary = WBTransSummaryPrintHelper.GetListWESMTransSummaryPerNo(selectedBPNo, selectedSTLRun, participant)
                    Dim newProgress As ProgressClass = New ProgressClass
                    newProgress.ProgressMsg = "Exporting PDF File/s for " & getParticpantInfo.IDNumber.ToString
                    UpdateProgress(newProgress)
                    Parallel.ForEach(listWESMTransSummary, Sub(WESMTransSummary)
                                                               Dim ds As DataSet = WBTransSummaryPrintHelper.PrintWESMTransactionSummary(New DSReport.WESMBillTransCoverSummaryDataTable, New DSReport.WESMBillTransAllocDetailsDataTable,
                                                                                                  selectedBPNo, selectedSTLRun, participant, getCalendarBP, getParticpantInfo, WESMTransSummary)
                                                               Dim expReport = New RPTWESMBillTransCoverSummary

                                                               expReport.SetDataSource(ds)

                                                               'expReport.ExportToDisk(ExportFormatType.PortableDocFormat, filePath & "\" &
                                                               '             participant & "_" & WESMTransSummary.TransactionNo & fileName & ".pdf")

                                                               Dim dest As New DiskFileDestinationOptions()
                                                               dest.DiskFileName = filePath & "\" & participant & "_" & WESMTransSummary.TransactionNo & fileName & ".pdf"

                                                               Dim formatOpt As New PdfFormatOptions()
                                                               formatOpt.FirstPageNumber = 0
                                                               formatOpt.LastPageNumber = 0
                                                               formatOpt.UsePageRange = False
                                                               formatOpt.CreateBookmarksFromGroupTree = False

                                                               Dim ex As New ExportOptions()
                                                               ex.ExportDestinationType = ExportDestinationType.DiskFile
                                                               ex.ExportDestinationOptions = dest
                                                               ex.ExportFormatType = ExportFormatType.PortableDocFormat
                                                               ex.ExportFormatOptions = formatOpt
                                                               expReport.Export(ex)

                                                               expReport.Close()
                                                               expReport.Dispose()
                                                           End Sub)

                Next
            ElseIf selectedSTLRun = "R" Then
                Dim fileName As String = "_RTA"
                For cnt As Integer = 0 To Me.chckList.CheckedItems.Count - 1
                    Dim participant = Me.chckList.CheckedItems(cnt).ToString()
                    Dim getParticpantInfo As AMParticipants = (From x In ListParticipantInfo Where x.IDNumber = participant Select x).First

                    Dim listWESMTransSummary As New List(Of WESMBillAllocCoverSummary)

                    listWESMTransSummary = WBTransSummaryPrintHelper.GetListWESMTransSummaryPerNo(selectedBPNo, selectedSTLRun, participant)
                    Dim newProgress As ProgressClass = New ProgressClass
                    newProgress.ProgressMsg = "Exporting PDF File/s for " & getParticpantInfo.IDNumber.ToString
                    UpdateProgress(newProgress)
                    Parallel.ForEach(listWESMTransSummary, Sub(WESMTransSummary)
                                                               Dim ds As DataSet = WBTransSummaryPrintHelper.PrintWESMTransactionSummary(New DSReport.WESMBillTransCoverSummaryDataTable, New DSReport.WESMBillTransAllocDetailsDataTable,
                                                                                                  selectedBPNo, selectedSTLRun, participant, getCalendarBP, getParticpantInfo, WESMTransSummary)
                                                               Dim expReport = New RPTReserveTransAllocationSummary

                                                               expReport.SetDataSource(ds)

                                                               'expReport.ExportToDisk(ExportFormatType.PortableDocFormat, filePath & "\" &
                                                               '             participant & "_" & WESMTransSummary.TransactionNo & fileName & ".pdf")

                                                               Dim dest As New DiskFileDestinationOptions()
                                                               dest.DiskFileName = filePath & "\" & participant & "_" & WESMTransSummary.TransactionNo & fileName & ".pdf"

                                                               Dim formatOpt As New PdfFormatOptions()
                                                               formatOpt.FirstPageNumber = 0
                                                               formatOpt.LastPageNumber = 0
                                                               formatOpt.UsePageRange = False
                                                               formatOpt.CreateBookmarksFromGroupTree = False

                                                               Dim ex As New ExportOptions()
                                                               ex.ExportDestinationType = ExportDestinationType.DiskFile
                                                               ex.ExportDestinationOptions = dest
                                                               ex.ExportFormatType = ExportFormatType.PortableDocFormat
                                                               ex.ExportFormatOptions = formatOpt
                                                               expReport.Export(ex)

                                                               expReport.Close()
                                                               expReport.Dispose()
                                                           End Sub)

                Next
            End If


            ProgressThread.Close()
            MessageBox.Show("Successfully exported!", "System Sucess Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        Try
            Dim fOpen As New FolderBrowserDialog
            Dim filePath As String = ""

            fOpen.ShowDialog()
            filePath = fOpen.SelectedPath

            If filePath.Length = 0 Then
                MessageBox.Show("Please select valid file path!", "System Sucess Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ans As DialogResult = New DialogResult
            ans = MessageBox.Show("Do you really want to export the WESM Transactions?", "Exporting WESM Transctions", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            ProgressThread.Show("Please wait while preparing the file.")

            Dim selectedBPNo As Integer = CInt(Me.ddlBillingPeriod.Text)
            Dim selectedSTLRun As String = CStr(Me.ddlSTLRun.Text)
            Dim ListCalendarBP As List(Of CalendarBillingPeriod) = WBHelper.GetCalendarBP()
            Dim getCalendarBP As CalendarBillingPeriod = (From x In ListCalendarBP Where x.BillingPeriod = selectedBPNo Select x).First
            Dim ListParticipantInfo As List(Of AMParticipants) = WBHelper.GetAMParticipants()

            Dim bpValue As String = getCalendarBP.EndDate.ToString("MMMyyyy").ToUpper
            Dim fileName As String = "_WTA"

            If selectedSTLRun.Contains("F") Then
                For cnt As Integer = 0 To Me.chckList.CheckedItems.Count - 1
                    Dim participant = Me.chckList.CheckedItems(cnt).ToString()
                    Dim getParticpantInfo As AMParticipants = (From x In ListParticipantInfo Where x.IDNumber = participant Select x).First

                    Dim listWESMTransSummary As New List(Of WESMBillAllocCoverSummary)

                    listWESMTransSummary = WBTransSummaryPrintHelper.GetListWESMTransSummaryPerNo(selectedBPNo, selectedSTLRun, participant)

                    Dim newProgress As ProgressClass = New ProgressClass
                    newProgress.ProgressMsg = "Exporting Excel File for " & getParticpantInfo.IDNumber.ToString
                    UpdateProgress(newProgress)
                    Parallel.ForEach(listWESMTransSummary, Sub(WESMTransSummary)
                                                               CreateWTAperParticipants(filePath, getParticpantInfo, WESMTransSummary)
                                                           End Sub)
                Next
            ElseIf selectedSTLRun.Contains("R") Then
                For cnt As Integer = 0 To Me.chckList.CheckedItems.Count - 1
                    Dim participant = Me.chckList.CheckedItems(cnt).ToString()
                    Dim getParticpantInfo As AMParticipants = (From x In ListParticipantInfo Where x.IDNumber = participant Select x).First
                    Dim listReserveTransSummary As New List(Of WESMBillAllocCoverSummary)

                    listReserveTransSummary = WBTransSummaryPrintHelper.GetListWESMTransSummaryPerNo(selectedBPNo, selectedSTLRun, participant)

                    Dim newProgress As ProgressClass = New ProgressClass
                    newProgress.ProgressMsg = "Exporting Excel File for " & getParticpantInfo.IDNumber.ToString
                    UpdateProgress(newProgress)
                    Parallel.ForEach(listReserveTransSummary, Sub(ReserveTransSummary)
                                                                  CreateRTAperParticipants(filePath, getParticpantInfo, ReserveTransSummary)
                                                              End Sub)
                Next
            End If
            ProgressThread.Close()
            MessageBox.Show("Successfully exported!", "System Sucess Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub CreateWTAperParticipants(ByVal SavingPathName As String, ByVal stlID As AMParticipants, ByVal WTASummaryItem As WESMBillAllocCoverSummary)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet1 As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlWESMDetails As Excel.Range

        Dim GetDateNow As Date = CDate(AMModule.SystemDate.ToShortDateString)
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()

        Dim WESMBillSummaryHeader As Object(,) = New Object(,) {}
        ReDim WESMBillSummaryHeader(1, 15)
        WESMBillSummaryHeader(0, 0) = "TRANSACTION_NO"
        WESMBillSummaryHeader(0, 1) = WTASummaryItem.TransactionNo.ToString()
        WESMBillSummaryHeader(1, 0) = "STL ID"
        WESMBillSummaryHeader(1, 1) = "Billing ID"
        WESMBillSummaryHeader(1, 2) = "Facility ID"
        WESMBillSummaryHeader(1, 3) = "WHT Tag"
        WESMBillSummaryHeader(1, 4) = "NonVatable Tag"
        WESMBillSummaryHeader(1, 5) = "ZeroRated Tag"
        WESMBillSummaryHeader(1, 6) = "Vatable Sales"
        WESMBillSummaryHeader(1, 7) = "ZeroRated Sales"
        WESMBillSummaryHeader(1, 8) = "ZeroRated Econzone Sales"
        WESMBillSummaryHeader(1, 9) = "Vat On Sales"
        WESMBillSummaryHeader(1, 10) = "Vatable Purchases"
        WESMBillSummaryHeader(1, 11) = "ZeroRated Purchases"
        WESMBillSummaryHeader(1, 12) = "ZeroRated Econzone Purchases"
        WESMBillSummaryHeader(1, 13) = "VAT On Purchases"
        WESMBillSummaryHeader(1, 14) = "EWT"
        WESMBillSummaryHeader(1, 15) = "Remarks"

        xlWorkSheet1 = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet1.Name = WTASummaryItem.TransactionNo.ToString()
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(2, UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
        xlWESMDetails = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlWESMDetails.Value = WESMBillSummaryHeader
        Dim lrow3 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
        If WTASummaryItem.ListWBAllocDisDetails.Count <> 0 Then
            Dim WESMBillSUmmaryArr As Object(,) = Me.GenerateWESMTransDetailsArray(stlID, WTASummaryItem)
            xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow3, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow3 + UBound(WESMBillSUmmaryArr, 1), UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
            xlWESMDetails = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
            xlWESMDetails.Value = WESMBillSUmmaryArr
        End If
        'End ****************************************************************************************************************************************************

        Dim FileName As String
        FileName = stlID.IDNumber.ToUpper.ToString & "_" & WTASummaryItem.TransactionNo.ToString() & ".xlsx"
        xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        xlWorkBook.Close(False)
        xlApp.Quit()

        releaseObject(xlWESMDetails)
        releaseObject(xlRowRange2)
        releaseObject(xlRowRange1)
        releaseObject(xlWorkSheet1)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)
    End Sub

    Public Sub CreateRTAperParticipants(ByVal SavingPathName As String, ByVal stlID As AMParticipants, ByVal RTASummaryItem As WESMBillAllocCoverSummary)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet1 As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlWESMDetails As Excel.Range

        Dim GetDateNow As Date = CDate(AMModule.SystemDate.ToShortDateString)
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()

        Dim WESMBillSummaryHeader As Object(,) = New Object(,) {}
        ReDim WESMBillSummaryHeader(2, 17)

        WESMBillSummaryHeader(0, 0) = "BILLING_ID"
        WESMBillSummaryHeader(0, 1) = RTASummaryItem.BillingID.ToString()

        WESMBillSummaryHeader(1, 0) = "TRANSACTION_NO"
        WESMBillSummaryHeader(1, 1) = RTASummaryItem.TransactionNo.ToString()

        WESMBillSummaryHeader(0, 3) = "AS_NONPAYMENT"
        WESMBillSummaryHeader(0, 4) = RTASummaryItem.ASNonPayment

        WESMBillSummaryHeader(1, 3) = "RESERVE_AMOUNT_ADJ"
        WESMBillSummaryHeader(1, 4) = RTASummaryItem.ReserveAmountAdj

        WESMBillSummaryHeader(2, 0) = "STL ID"
        WESMBillSummaryHeader(2, 1) = "Billing ID"
        WESMBillSummaryHeader(2, 2) = "Facility ID"
        WESMBillSummaryHeader(2, 3) = "WHT Tag"
        WESMBillSummaryHeader(2, 4) = "NonVatable Tag"
        WESMBillSummaryHeader(2, 5) = "ZeroRated Tag"
        WESMBillSummaryHeader(2, 6) = "Vatable Sales"
        WESMBillSummaryHeader(2, 7) = "ZeroRated Sales"
        WESMBillSummaryHeader(2, 8) = "ZeroRated Econzone Sales"
        WESMBillSummaryHeader(2, 9) = "Vat On Sales"
        WESMBillSummaryHeader(2, 10) = "Vatable Purchases"
        WESMBillSummaryHeader(2, 11) = "ZeroRated Purchases"
        WESMBillSummaryHeader(2, 12) = "ZeroRated Econzone Purchases"
        WESMBillSummaryHeader(2, 13) = "VAT On Purchases"
        WESMBillSummaryHeader(2, 14) = "EWT"
        WESMBillSummaryHeader(2, 15) = "Remarks"
        WESMBillSummaryHeader(2, 16) = "Reserve Category"
        WESMBillSummaryHeader(2, 17) = "Region"

        xlWorkSheet1 = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet1.Name = RTASummaryItem.TransactionNo.ToString()
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(3, UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
        xlWESMDetails = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlWESMDetails.Value = WESMBillSummaryHeader
        Dim lrow3 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
        If RTASummaryItem.ListWBAllocDisDetails.Count <> 0 Then
            Dim WESMBillSUmmaryArr As Object(,) = Me.GenerateReserveTransDetailsArray(stlID, RTASummaryItem)
            xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow3, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow3 + UBound(WESMBillSUmmaryArr, 1), UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
            xlWESMDetails = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
            xlWESMDetails.Value = WESMBillSUmmaryArr
        End If
        'End ****************************************************************************************************************************************************

        Dim FileName As String
        FileName = stlID.IDNumber.ToUpper.ToString & "_" & RTASummaryItem.TransactionNo.ToString() & ".xlsx"
        xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        xlWorkBook.Close(False)
        xlApp.Quit()

        releaseObject(xlWESMDetails)
        releaseObject(xlRowRange2)
        releaseObject(xlRowRange1)
        releaseObject(xlWorkSheet1)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)
    End Sub

    Private Function GenerateWESMTransDetailsArray(ByVal stlID As AMParticipants, ByVal WTASummaryItem As WESMBillAllocCoverSummary) As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim RowCount As Integer = 0

        Dim WBSArr As Object(,) = New Object(,) {}

        RowCount = WTASummaryItem.ListWBAllocDisDetails.Count

        ReDim WBSArr(RowCount, 15)

        For Each item In WTASummaryItem.ListWBAllocDisDetails
            WBSArr(RowIndex, 0) = item.STLID
            WBSArr(RowIndex, 1) = item.BillingID
            WBSArr(RowIndex, 2) = item.FacilityType
            WBSArr(RowIndex, 3) = item.WHTTag
            WBSArr(RowIndex, 4) = item.NonVatableTag
            WBSArr(RowIndex, 5) = item.ZeroRatedTag
            WBSArr(RowIndex, 6) = item.VatableSales
            WBSArr(RowIndex, 7) = item.ZeroRatedSales
            WBSArr(RowIndex, 8) = item.ZeroRatedEcoZoneSales
            WBSArr(RowIndex, 9) = item.VatOnSales
            WBSArr(RowIndex, 10) = item.VatablePurchases
            WBSArr(RowIndex, 11) = item.ZeroRatedPurchases
            WBSArr(RowIndex, 12) = item.ZeroRatedEcoZonePurchases
            WBSArr(RowIndex, 13) = item.VatOnPurchases
            WBSArr(RowIndex, 14) = item.EWT
            WBSArr(RowIndex, 15) = WTASummaryItem.Remarks

            RowIndex += 1
        Next
        ret = WBSArr
        Return ret
    End Function

    Private Function GenerateReserveTransDetailsArray(ByVal stlID As AMParticipants, ByVal RTASummaryItem As WESMBillAllocCoverSummary) As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim RowCount As Integer = 0

        Dim WBSArr As Object(,) = New Object(,) {}

        RowCount = RTASummaryItem.ListWBAllocDisDetails.Count

        ReDim WBSArr(RowCount, 17)

        For Each item In RTASummaryItem.ListWBAllocDisDetails
            WBSArr(RowIndex, 0) = item.STLID
            WBSArr(RowIndex, 1) = item.BillingID
            WBSArr(RowIndex, 2) = item.FacilityType
            WBSArr(RowIndex, 3) = item.WHTTag
            WBSArr(RowIndex, 4) = item.NonVatableTag
            WBSArr(RowIndex, 5) = item.ZeroRatedTag
            WBSArr(RowIndex, 6) = item.VatableSales
            WBSArr(RowIndex, 7) = item.ZeroRatedSales
            WBSArr(RowIndex, 8) = item.ZeroRatedEcoZoneSales
            WBSArr(RowIndex, 9) = item.VatOnSales
            WBSArr(RowIndex, 10) = item.VatablePurchases
            WBSArr(RowIndex, 11) = item.ZeroRatedPurchases
            WBSArr(RowIndex, 12) = item.ZeroRatedEcoZonePurchases
            WBSArr(RowIndex, 13) = item.VatOnPurchases
            WBSArr(RowIndex, 14) = item.EWT
            WBSArr(RowIndex, 15) = RTASummaryItem.Remarks
            WBSArr(RowIndex, 16) = RTASummaryItem.ReserveCategory
            WBSArr(RowIndex, 17) = RTASummaryItem.Region
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

End Class