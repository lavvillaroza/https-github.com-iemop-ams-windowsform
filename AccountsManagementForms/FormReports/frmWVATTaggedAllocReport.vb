Imports System.IO
Imports System.Threading.Tasks
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmWVATTaggedAllocReport
    Private _WVATTagAllocHelper As New WVATTaggedAllocReportHelper
    Private Sub frmWVATTaggedAllocReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
    End Sub

    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim TargetPath As String = ""
        Try

            Dim dateTo As Date = CDate(FormatDateTime(Me.dtTo.Value, DateFormat.ShortDate))
            Dim dateFrom As Date = CDate(FormatDateTime(Me.dtFrom.Value, DateFormat.ShortDate))

            With sFolderDialog
                .ShowDialog()
                If .SelectedPath.ToString.Trim.Length = 0 Then
                    Exit Sub
                Else
                    TargetPath = sFolderDialog.SelectedPath
                End If
            End With

            If TargetPath.Length = 0 Then
                MessageBox.Show("Please select valid file path!", "System Sucess Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            ProgressThread.Show("Please wait while generating.")
            _WVATTagAllocHelper.GetWVATTaggedAllocatedList(dateFrom, dateTo)
            CreateOutstandingSummaryReport(TargetPath)
            ProgressThread.Close()
            MessageBox.Show("Successfully exported in Excel!", "System Sucess Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CreateOutstandingSummaryReport(ByVal SavingPathName As String)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet1 As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlWVATTaggedAllocated As Excel.Range

        Dim getDateTo As Date = CDate(dtTo.Value)
        Dim getDateFrom As Date = CDate(dtFrom.Value)
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add()

        Dim WVATTaggedAllocatedHeadr As Object(,) = New Object(,) {}

        ReDim WVATTaggedAllocatedHeadr(1, 6)
        WVATTaggedAllocatedHeadr(1, 0) = "CERTIFICATE_NO"
        WVATTaggedAllocatedHeadr(1, 1) = "ID_NUMBER"
        WVATTaggedAllocatedHeadr(1, 2) = "INV_DM_CM"
        WVATTaggedAllocatedHeadr(1, 3) = "REMITTANCE_DATE"
        WVATTaggedAllocatedHeadr(1, 4) = "AMOUNT_TAGGED_ALLOC"
        WVATTaggedAllocatedHeadr(1, 5) = "BALANCE_TYPE"
        WVATTaggedAllocatedHeadr(1, 6) = "ALLOCATED_TO_AP"

        'WESM Bill Summary Table sheets
        xlWorkSheet1 = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet1.Name = "WVAT_ALLOCATION"
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(2, UBound(WVATTaggedAllocatedHeadr, 2) + 1), Excel.Range)
        xlWVATTaggedAllocated = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlWVATTaggedAllocated.Value = WVATTaggedAllocatedHeadr
        Dim lrow1 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
        If _WVATTagAllocHelper.WVATTaggedAllocatedList.Count <> 0 Then
            Dim WVATTaggedAllocatedArr As Object(,) = Me.GenerateWVATTaggedAllocatedArray()
            xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow1, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow1 + UBound(WVATTaggedAllocatedArr, 1), UBound(WVATTaggedAllocatedHeadr, 2) + 1), Excel.Range)
            xlWVATTaggedAllocated = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
            xlWVATTaggedAllocated.Value = WVATTaggedAllocatedArr
        End If
        'End  ****************************************************************************************************************************************************

        Dim FileName As String

        FileName = "WVAT_TAGALLOC_REPORT_" & getDateFrom.ToString("yyyyMMMdd") & "-" & getDateTo.ToString("yyyyMMMdd") & ".xlsx"

        xlApp.AlertBeforeOverwriting = False
        xlApp.DisplayAlerts = False

        xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        xlWorkBook.Close(False)
        xlApp.AlertBeforeOverwriting = True
        xlApp.DisplayAlerts = True
        xlApp.Quit()

        releaseObject(xlWVATTaggedAllocated)
        releaseObject(xlRowRange2)
        releaseObject(xlRowRange1)
        releaseObject(xlWorkSheet1)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)
    End Sub

    Private Function GenerateWVATTaggedAllocatedArray() As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim RowCount As Integer = _WVATTagAllocHelper.WVATTaggedAllocatedList.Count

        Dim EWTTAArr As Object(,) = New Object(,) {}
        ReDim EWTTAArr(RowCount, 8)

        For Each item In _WVATTagAllocHelper.WVATTaggedAllocatedList
            EWTTAArr(RowIndex, 0) = item.CertificateNo
            EWTTAArr(RowIndex, 1) = item.BillingIDNumber
            EWTTAArr(RowIndex, 2) = item.WESMTransNumber
            EWTTAArr(RowIndex, 3) = item.RemittanceDate.ToShortDateString
            EWTTAArr(RowIndex, 4) = item.AmountTaggedAlloc
            EWTTAArr(RowIndex, 5) = item.BalanceType.ToString
            EWTTAArr(RowIndex, 6) = item.AllocatedToAP.ToString
            RowIndex += 1
        Next

        ret = EWTTAArr

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

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged
        Try
            Dim dtFromSelectedDate As Date = CDate(dtFrom.Value)
            Dim dtToMinSelected As Date = New Date(dtFromSelectedDate.Year, dtFromSelectedDate.Month, DateTime.DaysInMonth(dtFromSelectedDate.Year, dtFromSelectedDate.Month))
            Dim dtToMaxSelected As Date = New Date(dtFromSelectedDate.Year, dtFromSelectedDate.Month, 1)

            If dtToMinSelected > dtToMaxSelected Then
                dtTo.MinDate = New Date(dtFromSelectedDate.Year, dtFromSelectedDate.Month, 1)
                dtTo.MaxDate = New Date(dtFromSelectedDate.Year, dtFromSelectedDate.Month, DateTime.DaysInMonth(dtFromSelectedDate.Year, dtFromSelectedDate.Month))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class