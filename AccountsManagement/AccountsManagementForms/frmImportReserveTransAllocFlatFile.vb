Option Explicit On
Option Strict On

Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports CrystalDecisions.Shared
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmImportReserveTransAllocFlatFile
    Private _RTAFFHelper As New ImportReserveTransAllocFlatFileHelper
    Private _WBillHelper As WESMBillHelper

    Dim fileBillingPd As Integer = 0
    Dim fileSettlementRun As String = ""
    Dim fileChargeType As EnumChargeType
    Dim getCalendarBP As New CalendarBillingPeriod
    Dim listParticipantInfo As New List(Of AMParticipants)
    Dim listWESMBill As New List(Of WESMBill)
    Dim wesmBilltransType As String = ""

    Dim cts As CancellationTokenSource
    Private Sub frmImportWESMAllocTransSummaryPrelimvb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Me.txt_BillingRemarks.MaxLength = 300
        _WBillHelper = WESMBillHelper.GetInstance
        Me.btnExportWTASummary.Enabled = False
        Me.btnDownload.Enabled = False
        Me.btnSave.Enabled = False
        Me.rb_Prelim.Checked = True
        Me.panel_CheckList.Enabled = False
    End Sub

    Private Sub UpdateProgress(_ProgressMsg As ProgressClass)
        ToolStripStatus_LabelMsg.Text = _ProgressMsg.ProgressMsg
        ctrl_statusStrip.Refresh()
    End Sub

    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        Dim openFD As New OpenFileDialog
        With openFD
            .Filter = "CSV Files (*.csv)|*.csv"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtDirectory.Text = .FileName
                Me.txtDirectory.SelectionStart = Me.txtDirectory.Text.Length + 1
            End If
        End With
        Me.btnSave.Enabled = False
    End Sub

    Private Async Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Try
            Me.chckList.Items.Clear()
            Me.chckAll.Checked = False

            _RTAFFHelper.NewListWBAllocCoverSummary = New List(Of WESMBillAllocCoverSummary)
            _RTAFFHelper.NewListWESMBill = New List(Of WESMBill)
            listWESMBill = New List(Of WESMBill)

            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)

            Dim ask = MessageBox.Show("Do you really want to upload the file?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ask = DialogResult.No Then
                Exit Sub
            End If
            btn_Clear.Enabled = False
            panel_ImportBIRRTransSummary.Enabled = False

            If Me.txtDirectory.Text.Trim.Length = 0 Then
                MessageBox.Show("Please specify the file to be uploaded.!", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Exit Sub
            ElseIf Not File.Exists(Me.txtDirectory.Text.Trim()) Then
                MessageBox.Show("File does not exist!", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Exit Sub
            End If

            Dim pathFolder As String = ""
            Dim fileName As String = ""

            pathFolder = Path.GetDirectoryName(Me.txtDirectory.Text.Trim())
            fileName = Path.GetFileName(Me.txtDirectory.Text.Trim())

            Dim fnameSplit = Split(fileName, "_")
            Dim fnameSplit2 = Split(fnameSplit(fnameSplit.Length - 1), ".")

            If Not fnameSplit2(1).ToUpper.Equals("CSV") Then
                MessageBox.Show("Invalid file type! Please choose another file.", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Exit Sub
            End If

            If Not fnameSplit(0).ToUpper.Contains("BP") Then
                MessageBox.Show("Invalid filename! Please choose another file.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If rb_Prelim.Checked = True Then
                If Not fnameSplit(1).ToUpper.Contains("P") Then
                    MessageBox.Show("Invalid filename! Please check the STL Run if started choose another file.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                If Not fnameSplit(2).ToUpper.Equals("RP") Then
                    MessageBox.Show("Invalid filename! Please choose another file.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Else
                If Not fnameSplit(1).ToUpper.Contains("F") Then
                    MessageBox.Show("Invalid filename! Please choose another file.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                If Not fnameSplit(2).ToUpper.Equals("RF") Then
                    MessageBox.Show("Invalid filename! Please choose another file.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If

            If Not fnameSplit2(0).ToUpper.Equals("INV") Then
                MessageBox.Show("Invalid filename! Please choose another file.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            fileBillingPd = CInt(fnameSplit(0).Replace("BP", "").Trim().ToString)
            fileSettlementRun = CStr(fnameSplit(1).Trim().ToString)
            wesmBilltransType = CStr(fnameSplit(2).Trim().ToUpper)

            fileChargeType = EnumChargeType.E

            Dim transDate As Date = CDate(FormatDateTime(Me.dtTransDate.Value, DateFormat.ShortDate))
            Dim dueDate As Date = CDate(FormatDateTime(Me.dtDueDate.Value, DateFormat.ShortDate))
            Dim remarks As String = CStr(Me.txt_BillingRemarks.Text)
            Dim currDate As Date = _WBillHelper.GetSystemDate()

            getCalendarBP = (From x In _WBillHelper.GetCalendarBP() Where x.BillingPeriod = fileBillingPd Select x).FirstOrDefault

            If getCalendarBP Is Nothing Then
                MessageBox.Show("Billing Period" & fileBillingPd.ToString() & " does not exist in Calendar BP table", "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            'If transDate.Month <> currDate.Month Or transDate.Year <> currDate.Year Then
            '    MessageBox.Show("Invalid selection for Transaction Date ", "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'End If

            'If dueDate.Month <> currDate.Month Or dueDate.Year <> currDate.Year Or dueDate.Day <> 25 Then
            '    MessageBox.Show("Invalid selection for Due Date ", "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'End If

            If txt_BillingRemarks.Text.Length = 0 Then
                MessageBox.Show("Please input Billing Remarks ", "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            'ProgressThread.Show("Please wait while saving.")
            Await Task.Run(Sub() Me._RTAFFHelper.ReadFlatFileNew(fileBillingPd, fileSettlementRun, wesmBilltransType, EnumFileType.Energy, pathFolder, fileName, remarks, transDate, dueDate, progressIndicator))

            'ProgressThread.Close()

            'Check Participants
            listParticipantInfo = (From x In _WBillHelper.GetAMParticipants() Where x.Status = EnumStatus.Active Select x).ToList()

            Dim NotInList As New List(Of String)
            For Each item In Me._RTAFFHelper.NewListWBAllocCoverSummary.Select(Function(x) x.StlID).Distinct.ToList()
                Dim ParentID = item
                Dim CheckParticipant = (From x In listParticipantInfo Where x.IDNumber = ParentID)
                If CheckParticipant.Count = 0 Then
                    NotInList.Add(ParentID)
                End If
            Next

            NotInList = (From x In NotInList Select x Distinct).ToList

            If NotInList.Count <> 0 Then
                MessageBox.Show("There are participants not registered found in the file being uploaded.", "Invalid Participants", MessageBoxButtons.OK, MessageBoxIcon.Error)
                frmViewDetails.ShowNotExistingParticipants(NotInList)
                frmViewDetails.Show()
                Exit Sub
            End If
            'End check Participants

            If UCase(fnameSplit2(0).ToString) = "INV" Then

                'Check if the summation of VAT on Energy is equal to zero
                Dim Vat = (From x In _RTAFFHelper.NewListWBAllocCoverSummary
                           Select x.VatOnSales + x.VatOnPurchases).Sum()

                If Vat <> 0 Then
                    If MessageBox.Show("Summation of VAT on Energy is not equal to zero. Do you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If

                Dim AP As Decimal = (From x In _RTAFFHelper.NewListWBAllocCoverSummary
                                     Where x.NetSale > 0
                                     Select x.NetSale).Sum()

                Dim AR As Decimal = (From x In _RTAFFHelper.NewListWBAllocCoverSummary
                                     Where x.NetPurchase < 0
                                     Select x.NetPurchase).Sum()

                Dim TotalAmount As Decimal = AP + AR

                'Compare NSSRA line item from AP minus AR.
                If TotalAmount <> 0 Then
                    'Dim conf = MessageBox.Show("Total AP and AR:" & vbTab & vbTab & FormatNumber(TotalAmount, 2) & vbNewLine &
                    '                           "Total AP and AR are not equal! Do you want to continue?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    'If conf = DialogResult.No Then
                    '    Exit Sub
                    'End If
                    MessageBox.Show("Total AP and AR:" & vbTab & vbTab & FormatNumber(TotalAmount, 2), "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                Else
                    MessageBox.Show("Total AP and AR:" & vbTab & vbTab & FormatNumber(TotalAmount, 2), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                If Me.rb_Final.Checked = True Then
                    Me.btnSave.Enabled = True
                End If

                ProgressThread.Show("Please wait while preparing.")
                Me.chckList.Items.Clear()
                For Each item In _RTAFFHelper.NewListWBAllocCoverSummary.Select(Function(y) y.StlID).OrderBy(Function(x) x).Distinct.ToList
                    Me.chckList.Items.Add(item)
                Next

                ProgressThread.Close()
                MessageBox.Show("Ready for exporting to PDF!", "System Sucess Message!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.btnExportWTASummary.Enabled = True
                Me.btnDownload.Enabled = True
                Me.panel_CheckList.Enabled = True
                Me.btn_Clear.Enabled = True
                Me.ToolStripStatus_LabelMsg.Text = "Ready"

            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.panel_ImportBIRRTransSummary.Enabled = True
        End Try
    End Sub

    Private Sub ClearControls()
        Me.txt_BillingRemarks.Text = ""
        Me.txtDirectory.Text = ""
        Me.chckList.Items.Clear()
        Me.chckAll.Checked = False
        Me.btnExportWTASummary.Enabled = False
        Me.btnDownload.Enabled = False
        Me.ToolStripStatus_LabelMsg.Text = "Ready"
        Me.ctrl_statusStrip.Refresh()
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
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

            Dim bpValue As String = getCalendarBP.EndDate.ToString("MMMyyyy").ToUpper
            Dim fileName As String = bpValue & "_" & fileSettlementRun & "_INV"

            'Check Participants
            listParticipantInfo = (From x In _WBillHelper.GetAMParticipants() Where x.Status = EnumStatus.Active Select x).ToList()
            getCalendarBP = (From x In _WBillHelper.GetCalendarBP() Where x.BillingPeriod = fileBillingPd Select x).FirstOrDefault

            For cnt As Integer = 0 To Me.chckList.CheckedItems.Count - 1

                Dim participant = Me.chckList.CheckedItems(cnt).ToString()
                Dim getParticpantInfo As AMParticipants = (From x In listParticipantInfo Where x.IDNumber = participant Select x).First
                Dim listWESMTransSummary As New List(Of WESMBillAllocCoverSummary)
                listWESMTransSummary = _RTAFFHelper.NewListWBAllocCoverSummary.Where(Function(x) x.StlID = getParticpantInfo.IDNumber).Select(Function(z) z).ToList

                Dim newProgress As ProgressClass = New ProgressClass
                newProgress.ProgressMsg = "Exporting PDF File for " & getParticpantInfo.IDNumber.ToString
                UpdateProgress(newProgress)

                Parallel.ForEach(listWESMTransSummary, Sub(WESMTransSummary)

                                                           Dim ds As DataSet = _RTAFFHelper.PrintReserveTransactionSummary(New DSReport.WESMBillTransCoverSummaryDataTable, New DSReport.WESMBillTransAllocDetailsDataTable,
                                                                                              fileBillingPd, fileSettlementRun, participant, getCalendarBP, getParticpantInfo, WESMTransSummary)

                                                           'expReport.ExportToDisk(ExportFormatType.PortableDocFormat, filePath & "\" &
                                                           '                         participant & "_" & WESMTransSummary.TransactionNo & "_WTA.pdf")
                                                           Dim dest As New DiskFileDestinationOptions()
                                                           dest.DiskFileName = filePath & "\" & participant & "_" & WESMTransSummary.TransactionNo & "_RTA.pdf"

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
                                                           If Me.rb_Prelim.Checked = True Then
                                                               Dim expReport = New RPTReserveTransAllocationSummaryPrelim
                                                               expReport.SetDataSource(ds)
                                                               expReport.Export(ex)

                                                               expReport.Close()
                                                               expReport.Dispose()
                                                           Else
                                                               Dim expReport = New RPTReserveTransAllocationSummary
                                                               expReport.SetDataSource(ds)
                                                               expReport.Export(ex)

                                                               expReport.Close()
                                                               expReport.Dispose()
                                                           End If
                                                       End Sub)
            Next
            ProgressThread.Close()
            If Me.chckList.CheckedItems.Count = 0 Then
                MessageBox.Show("No selected participants!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Successfully exported!", "System Sucess Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Async Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Panel_Main.Enabled = False
            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)
            'Check if there is existing records

            'alter STL RUN From F to R for Reserve
            fileSettlementRun = fileSettlementRun.Replace("F", "R")
            Dim CountWESMBillExist As Integer = _WBillHelper.GetWESMBillCount(fileBillingPd, fileSettlementRun, fileChargeType)
            Dim ans As DialogResult = New DialogResult

            If CountWESMBillExist <> 0 Then
                ans = MessageBox.Show("There are already existing records, " & vbNewLine _
                                    & "Do you want to replace the existing data?", "Please Confirm Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If ans = DialogResult.No Then
                    Exit Sub
                End If
            Else
                ans = MessageBox.Show("Do you really want to save the records?", "Please Confirm Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If ans = DialogResult.No Then
                    Exit Sub
                End If
            End If

            If ans = MsgBoxResult.Yes Then
                cts = New CancellationTokenSource
                _RTAFFHelper.CreateWESMBill()
                Dim dueDate As Date = CDate(FormatDateTime(Me.dtDueDate.Value, DateFormat.ShortDate))
                Dim itemJV As JournalVoucher
                itemJV = Me.GenerateJournalVoucherForEnergy(_RTAFFHelper.NewListWESMBill, 0)

                Dim itemGP = Me.GenerateGPPosted(getCalendarBP, fileSettlementRun, dueDate,
                                                     fileChargeType, itemJV)

                ProgressThread.Show("Please wait while saving.")
                Await Task.Run(Sub() _RTAFFHelper.SaveUplodedWESMBill(getCalendarBP, fileSettlementRun, fileChargeType, EnumFileType.Energy, itemJV, itemGP, CountWESMBillExist, progressIndicator, cts.Token))
                ProgressThread.Close()
                MessageBox.Show("Successfully uploaded to Database", "System Sucess Message!", MessageBoxButtons.OK, MessageBoxIcon.Information)

                _RTAFFHelper.NewListWBAllocCoverSummary = New List(Of WESMBillAllocCoverSummary)
                _RTAFFHelper.NewListWESMBill = New List(Of WESMBill)
                listWESMBill = New List(Of WESMBill)
                Me.ClearControls()
                cts = Nothing
            Else
                ProgressThread.Close()
                MessageBox.Show("Transaction Cancelled", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Panel_Main.Enabled = True
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

    Private Sub btn_Clear_Click(sender As Object, e As EventArgs) Handles btn_Clear.Click
        Dim ans As DialogResult = New DialogResult
        Try
            ans = MessageBox.Show("Do you really want to clear this?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ans = DialogResult.No Then
                Exit Sub
            End If
            _RTAFFHelper.NewListWBAllocCoverSummary = New List(Of WESMBillAllocCoverSummary)
            _RTAFFHelper.NewListWESMBill = New List(Of WESMBill)
            listWESMBill = New List(Of WESMBill)
            ClearControls()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmd_ExportInExcel_Click(sender As Object, e As EventArgs) Handles btnExportWTASummary.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim TargetPath As String = ""
        'Dim SelectedYear As String = Me.ddlYear_cmb.Text
        Try
            For Each item In _RTAFFHelper.NewListWBAllocCoverSummary
                Dim itemNewWESMBillEnergy As New WESMBill
                With itemNewWESMBillEnergy
                    .BillingPeriod = item.BillingPeriod
                    .IDNumber = item.StlID
                    .RegistrationID = item.BillingID
                    .ChargeType = EnumChargeType.E
                    .Amount = Math.Round(item.VatableSales + item.ZeroRatedSales + item.ZeroRatedEcoZoneSales + item.VatablePurchases + item.ZeroRatedPurchases + item.ZeroRatedEcoZonePurchases, 2)
                    .SettlementRun = item.STLRun
                    .InvoiceDate = item.TransactionDate
                    .InvoiceNumber = item.TransactionNo
                    .DueDate = item.DueDate
                    .MarketFeesRate = item.MarketFeesRate
                    .Remarks = item.Remarks
                End With
                listWESMBill.Add(itemNewWESMBillEnergy)
                listWESMBill.TrimExcess()
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
                listWESMBill.Add(itemNewWESMBillVAT)
                listWESMBill.TrimExcess()
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

            If TargetPath.Length = 0 Then
                MessageBox.Show("Please select valid file path!", "System Sucess Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ProgressThread.Show("Please wait while processing.")
            Dim newProgress As ProgressClass = New ProgressClass
            newProgress.ProgressMsg = "Exporting Excel File for Uploaded WTA Summary"
            UpdateProgress(newProgress)

            CreateOutstandingSummaryReport(TargetPath)
            ProgressThread.Close()

            MessageBox.Show("Generating Reports are successfully.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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


    Public Sub CreateOutstandingSummaryReport(ByVal SavingPathName As String)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet1 As Excel.Worksheet
        Dim xlWorkSheet2 As Excel.Worksheet
        Dim xlWorkSheet3 As Excel.Worksheet
        Dim xlRowRange1 As Excel.Range
        Dim xlRowRange2 As Excel.Range
        Dim xlWESMBills As Excel.Range
        Dim xlWESMTrans As Excel.Range
        Dim xlWESMDetails As Excel.Range

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
        WESMBillSummaryHeader(1, 9) = "Reserve Amount"
        WESMBillSummaryHeader(1, 10) = "VAT"
        WESMBillSummaryHeader(1, 11) = "EWT"
        WESMBillSummaryHeader(1, 12) = "Due Date"
        WESMBillSummaryHeader(1, 13) = "Charge Type"


        'WESM Bill Summary Table sheets
        xlWorkSheet1 = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet1.Name = "WESM Bills Summary"
        xlRowRange1 = DirectCast(xlWorkSheet1.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet1.Cells(2, UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
        xlWESMBills = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
        xlWESMBills.Value = WESMBillSummaryHeader
        Dim lrow1 As Integer = xlWorkSheet1.Range("A" & xlWorkSheet1.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
        If listWESMBill.Count <> 0 Then
            Dim WESMBillSUmmaryArr As Object(,) = Me.GenerateWESMBillArray()
            xlRowRange1 = DirectCast(xlWorkSheet1.Cells(lrow1, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet1.Cells(lrow1 + UBound(WESMBillSUmmaryArr, 1), UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
            xlWESMBills = xlWorkSheet1.Range(xlRowRange1, xlRowRange2)
            xlWESMBills.Value = WESMBillSUmmaryArr
        End If
        'End  ****************************************************************************************************************************************************

        'WESM Transaction Covery Summary Table sheets
        WESMBillSummaryHeader = New Object(,) {}
        ReDim WESMBillSummaryHeader(1, 23)
        WESMBillSummaryHeader(1, 0) = "Remarks"
        WESMBillSummaryHeader(1, 1) = "BillingPeriod"
        WESMBillSummaryHeader(1, 2) = "SettlmentRun"
        WESMBillSummaryHeader(1, 3) = "STL ID"
        WESMBillSummaryHeader(1, 4) = "Billing ID"
        WESMBillSummaryHeader(1, 5) = "ZeroRated Tag"
        WESMBillSummaryHeader(1, 6) = "NonVatable Tag"
        WESMBillSummaryHeader(1, 7) = "ITH"
        WESMBillSummaryHeader(1, 8) = "WHT"
        WESMBillSummaryHeader(1, 9) = "Vatable Sales"
        WESMBillSummaryHeader(1, 10) = "ZeroRated Sales"
        WESMBillSummaryHeader(1, 11) = "ZeroRated Econzone Sales"
        WESMBillSummaryHeader(1, 12) = "EWT Sales"
        WESMBillSummaryHeader(1, 13) = "Vat On Sales"
        WESMBillSummaryHeader(1, 14) = "Vatable Purchases"
        WESMBillSummaryHeader(1, 15) = "ZeroRated Purchases"
        WESMBillSummaryHeader(1, 16) = "ZeroRated Econzone Purchases"
        WESMBillSummaryHeader(1, 17) = "VAT On Purchases"
        WESMBillSummaryHeader(1, 18) = "EWT Purchases"
        WESMBillSummaryHeader(1, 19) = "Transaction No"
        WESMBillSummaryHeader(1, 20) = "AS NonPayment"
        WESMBillSummaryHeader(1, 21) = "Reserve Amount Adj"
        WESMBillSummaryHeader(1, 22) = "Reserve Category"
        WESMBillSummaryHeader(1, 23) = "Region"


        If xlWorkBook.Sheets.Count = 1 Then
            xlWorkBook.Sheets.Add(After:=xlWorkBook.Sheets(1))
        End If
        xlWorkSheet2 = CType(xlWorkBook.Sheets(2), Excel.Worksheet)
        xlWorkSheet2.Name = "WESM Transactions Cover Summary"
        xlRowRange1 = DirectCast(xlWorkSheet2.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet2.Cells(2, UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
        xlWESMTrans = xlWorkSheet2.Range(xlRowRange1, xlRowRange2)
        xlWESMTrans.Value = WESMBillSummaryHeader
        Dim lrow2 As Integer = xlWorkSheet2.Range("A" & xlWorkSheet2.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
        If _RTAFFHelper.NewListWBAllocCoverSummary.Count <> 0 Then
            Dim WESMBillSUmmaryArr As Object(,) = Me.GenerateWESMTransSummaryArray()
            xlRowRange1 = DirectCast(xlWorkSheet2.Cells(lrow2, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet2.Cells(lrow2 + UBound(WESMBillSUmmaryArr, 1), UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
            xlWESMTrans = xlWorkSheet2.Range(xlRowRange1, xlRowRange2)
            xlWESMTrans.Value = WESMBillSUmmaryArr
        End If
        'End ****************************************************************************************************************************************************

        'WESM Transaction Details Table sheets
        WESMBillSummaryHeader = New Object(,) {}
        ReDim WESMBillSummaryHeader(1, 15)
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
        WESMBillSummaryHeader(1, 15) = "Transaction No"

        If xlWorkBook.Sheets.Count = 2 Then
            xlWorkBook.Sheets.Add(After:=xlWorkBook.Sheets(2))
        End If
        xlWorkSheet3 = CType(xlWorkBook.Sheets(3), Excel.Worksheet)
        xlWorkSheet3.Name = "WESM Transactions Details"
        xlRowRange1 = DirectCast(xlWorkSheet3.Cells(1, 1), Excel.Range)
        xlRowRange2 = DirectCast(xlWorkSheet3.Cells(2, UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
        xlWESMDetails = xlWorkSheet3.Range(xlRowRange1, xlRowRange2)
        xlWESMDetails.Value = WESMBillSummaryHeader
        Dim lrow3 As Integer = xlWorkSheet3.Range("A" & xlWorkSheet3.Rows.Count).End(Excel.XlDirection.xlUp).Row + 1
        If _RTAFFHelper.NewListWBAllocCoverSummary.Count <> 0 Then
            Dim WESMBillSUmmaryArr As Object(,) = Me.GenerateWESMTransDetailsArray()
            xlRowRange1 = DirectCast(xlWorkSheet3.Cells(lrow3, 1), Excel.Range)
            xlRowRange2 = DirectCast(xlWorkSheet3.Cells(lrow3 + UBound(WESMBillSUmmaryArr, 1), UBound(WESMBillSummaryHeader, 2) + 1), Excel.Range)
            xlWESMDetails = xlWorkSheet3.Range(xlRowRange1, xlRowRange2)
            xlWESMDetails.Value = WESMBillSUmmaryArr
        End If
        'End ****************************************************************************************************************************************************

        Dim FileName As String

        FileName = "Uploaded WTA Summary_" & fileBillingPd & "_" & fileSettlementRun & ".xlsx"

        xlWorkBook.SaveAs(SavingPathName & "\" & FileName, Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        xlWorkBook.Close(False)
        xlApp.Quit()

        releaseObject(xlWESMDetails)
        releaseObject(xlWESMTrans)
        releaseObject(xlWESMBills)
        releaseObject(xlRowRange2)
        releaseObject(xlRowRange1)
        releaseObject(xlWorkSheet3)
        releaseObject(xlWorkSheet2)
        releaseObject(xlWorkSheet1)
        releaseObject(xlWorkBook)
        releaseObject(xlApp)
    End Sub

    Private Function GenerateWESMBillArray() As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim WBSArr As Object(,) = New Object(,) {}
        Dim getDistinctTransNo As List(Of String) = (From x In listWESMBill Select x.InvoiceNumber Distinct).ToList
        Dim RowCount As Integer = getDistinctTransNo.Count()

        ReDim WBSArr(RowCount, 13)

        For Each item In getDistinctTransNo
            Dim getWESMBills As List(Of WESMBill) = (From x In listWESMBill Where x.InvoiceNumber = item Select x).ToList
            Dim getAMParticipantInfo = (From x In listParticipantInfo Where x.IDNumber = getWESMBills.Select(Function(y) y.IDNumber).First Select x).FirstOrDefault

            Dim getWESMBillBaseAmount As WESMBill = (From x In getWESMBills Where x.ChargeType = EnumChargeType.E Or x.ChargeType = EnumChargeType.MF Select x).FirstOrDefault
            Dim getWESMBillVATAmount As WESMBill = (From x In getWESMBills Where x.ChargeType = EnumChargeType.EV Or x.ChargeType = EnumChargeType.MFV Select x).FirstOrDefault
            Dim getWESMAllocTransSummary As WESMBillAllocCoverSummary = (From x In _RTAFFHelper.NewListWBAllocCoverSummary Where x.TransactionNo = item Select x).FirstOrDefault

            WBSArr(RowIndex, 0) = getWESMBills.Select(Function(x) x.Remarks).First
            WBSArr(RowIndex, 1) = getWESMBills.Select(Function(x) x.BillingPeriod).First
            WBSArr(RowIndex, 2) = getWESMBills.Select(Function(x) x.SettlementRun).First.Replace("R", "F")
            WBSArr(RowIndex, 3) = getWESMBills.Select(Function(x) x.InvoiceNumber).First
            WBSArr(RowIndex, 4) = getWESMBills.Select(Function(x) x.InvoiceDate).First
            WBSArr(RowIndex, 5) = getWESMBills.Select(Function(x) x.IDNumber).First
            WBSArr(RowIndex, 6) = getWESMBills.Select(Function(x) x.RegistrationID).First
            WBSArr(RowIndex, 7) = getAMParticipantInfo.ParticipantID
            WBSArr(RowIndex, 8) = getAMParticipantInfo.FullName
            WBSArr(RowIndex, 12) = getWESMBills.Select(Function(x) x.DueDate).First
            WBSArr(RowIndex, 13) = "RESERVE"

            If Not getWESMBillBaseAmount Is Nothing Then
                WBSArr(RowIndex, 9) = getWESMBillBaseAmount.Amount
            Else
                WBSArr(RowIndex, 9) = 0
            End If

            If Not getWESMBillVATAmount Is Nothing Then
                WBSArr(RowIndex, 10) = getWESMBillVATAmount.Amount
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

    Private Function GenerateWESMTransSummaryArray() As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim RowCount As Integer = listWESMBill.Count()

        Dim WBSArr As Object(,) = New Object(,) {}
        ReDim WBSArr(RowCount, 23)

        Dim getDistinctTransNo As List(Of String) = (From x In _RTAFFHelper.NewListWBAllocCoverSummary Select x.TransactionNo Distinct Order By TransactionNo).ToList

        For Each item In getDistinctTransNo
            Dim getWESMAllocTransSummary As WESMBillAllocCoverSummary = (From x In _RTAFFHelper.NewListWBAllocCoverSummary Where x.TransactionNo = item Select x).FirstOrDefault
            Dim getAMParticipantInfo = (From x In listParticipantInfo Where x.IDNumber = getWESMAllocTransSummary.StlID Select x).FirstOrDefault

            WBSArr(RowIndex, 0) = getWESMAllocTransSummary.Remarks
            WBSArr(RowIndex, 1) = getWESMAllocTransSummary.BillingPeriod
            WBSArr(RowIndex, 2) = getWESMAllocTransSummary.STLRun
            WBSArr(RowIndex, 3) = getWESMAllocTransSummary.StlID
            WBSArr(RowIndex, 4) = getWESMAllocTransSummary.BillingID
            WBSArr(RowIndex, 5) = getWESMAllocTransSummary.ZeroRatedTag
            WBSArr(RowIndex, 6) = getWESMAllocTransSummary.NonVatableTag
            WBSArr(RowIndex, 7) = getWESMAllocTransSummary.ITH
            WBSArr(RowIndex, 8) = getWESMAllocTransSummary.WHT
            WBSArr(RowIndex, 9) = getWESMAllocTransSummary.VatableSales
            WBSArr(RowIndex, 10) = getWESMAllocTransSummary.ZeroRatedSales
            WBSArr(RowIndex, 11) = getWESMAllocTransSummary.ZeroRatedEcoZoneSales
            WBSArr(RowIndex, 12) = getWESMAllocTransSummary.EWTSales
            WBSArr(RowIndex, 13) = getWESMAllocTransSummary.VatOnSales
            WBSArr(RowIndex, 14) = getWESMAllocTransSummary.VatablePurchases
            WBSArr(RowIndex, 15) = getWESMAllocTransSummary.ZeroRatedPurchases
            WBSArr(RowIndex, 16) = getWESMAllocTransSummary.ZeroRatedEcoZonePurchases
            WBSArr(RowIndex, 17) = getWESMAllocTransSummary.VatOnPurchases
            WBSArr(RowIndex, 18) = getWESMAllocTransSummary.EWTPurchases
            WBSArr(RowIndex, 19) = getWESMAllocTransSummary.TransactionNo
            WBSArr(RowIndex, 20) = getWESMAllocTransSummary.ASNonPayment
            WBSArr(RowIndex, 21) = getWESMAllocTransSummary.ReserveAmountAdj
            WBSArr(RowIndex, 22) = getWESMAllocTransSummary.ReserveCategory
            WBSArr(RowIndex, 23) = getWESMAllocTransSummary.Region

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

    Private Function GenerateWESMTransDetailsArray() As Object(,)
        Dim ret As Object(,) = New Object(,) {}
        Dim ObjDicInvNo As New Dictionary(Of String, Integer)
        Dim ObjDicSeq As New Dictionary(Of Integer, Integer)
        Dim RowIndex As Integer = 0
        Dim RowCount As Integer = 0

        Dim WBSArr As Object(,) = New Object(,) {}
        For Each item In _RTAFFHelper.NewListWBAllocCoverSummary
            RowCount += item.ListWBAllocDisDetails.Count
        Next

        ReDim WBSArr(RowCount, 15)

        For Each item In _RTAFFHelper.NewListWBAllocCoverSummary
            For Each itemDetails In item.ListWBAllocDisDetails
                WBSArr(RowIndex, 0) = itemDetails.STLID
                WBSArr(RowIndex, 1) = itemDetails.BillingID
                WBSArr(RowIndex, 2) = itemDetails.FacilityType
                WBSArr(RowIndex, 3) = itemDetails.WHTTag
                WBSArr(RowIndex, 4) = itemDetails.NonVatableTag
                WBSArr(RowIndex, 5) = itemDetails.ZeroRatedTag
                WBSArr(RowIndex, 6) = itemDetails.VatableSales
                WBSArr(RowIndex, 7) = itemDetails.ZeroRatedSales
                WBSArr(RowIndex, 8) = itemDetails.ZeroRatedEcoZoneSales
                WBSArr(RowIndex, 9) = itemDetails.VatOnSales
                WBSArr(RowIndex, 10) = itemDetails.VatablePurchases
                WBSArr(RowIndex, 11) = itemDetails.ZeroRatedPurchases
                WBSArr(RowIndex, 12) = itemDetails.ZeroRatedEcoZonePurchases
                WBSArr(RowIndex, 13) = itemDetails.VatOnPurchases
                WBSArr(RowIndex, 14) = itemDetails.EWT
                WBSArr(RowIndex, 15) = item.TransactionNo
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

    Private Function GenerateGPPosted(ByVal billingPeriod As CalendarBillingPeriod, ByVal settlementRun As String,
                                      ByVal dueDate As Date, ByVal chargeType As EnumChargeType, ByVal itemJV As JournalVoucher) _
                                      As WESMBillGPPosted
        Dim result As New WESMBillGPPosted
        Dim totalDocumentAmount As Decimal = 0, totalCredit As Decimal = 0

        Try
            For Each item In itemJV.JVDetails
                totalDocumentAmount += item.Debit
                totalCredit += item.Credit
            Next

            'For testing only
            If totalDocumentAmount <> totalCredit Then
                Throw New ApplicationException("Debit and credit are not equal!")
            End If

            With result
                .BillingPeriod = billingPeriod.BillingPeriod
                .SettlementRun = settlementRun
                .DueDate = dueDate
                .Charge = chargeType
                .DocumentAmount = totalDocumentAmount
                .Posted = 0
                .PostType = EnumPostedType.U.ToString()
            End With

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function
    Private Function GenerateJournalVoucherForEnergy(ByVal listItems As List(Of WESMBill), ByVal WithholdingTax As Decimal) As JournalVoucher
        Dim result As New JournalVoucher
        Try
            Dim jvDetails As New List(Of JournalVoucherDetails)
            Dim jvDetail As JournalVoucherDetails

            Dim signatories As New DocSignatories

            Try
                signatories = (From x In _WBillHelper.GetSignatories()
                               Where x.DocCode = EnumDocCode.JV.ToString()
                               Select x).First()
            Catch ex As Exception
                Throw New ApplicationException("No Signatories for Journal Voucher")
            End Try


            'Get the Total AR
            Dim totalAR = (From x In listItems
                           Where x.Amount < 0
                           Select x.Amount).Sum()

            'Get the Total AP
            Dim totalAP = (From x In listItems
                           Where x.Amount > 0
                           Select x.Amount).Sum()

            totalAP = Math.Round(totalAP, 2)
            totalAR = Math.Round(totalAR, 2)
            WithholdingTax = Math.Round(WithholdingTax, 2)

            'Get the Total NSS
            Dim totalNSS = Math.Abs(totalAP + totalAR - WithholdingTax)

            'Entry for Accounts Receivable
            If totalAR <> 0 Then
                jvDetail = New JournalVoucherDetails
                With jvDetail
                    .AccountCode = AMModule.CreditCode
                    .Debit = Math.Abs(totalAR)
                    .Credit = 0
                End With
                jvDetails.Add(jvDetail)
            End If

            If WithholdingTax <> 0 Then
                jvDetail = New JournalVoucherDetails
                With jvDetail
                    .AccountCode = AMModule.EWTPayable
                    .Debit = 0
                    .Credit = Math.Abs(WithholdingTax)
                End With
                jvDetails.Add(jvDetail)
            End If

            'Entry for Accounts Payable
            If totalAP <> 0 Then
                jvDetail = New JournalVoucherDetails
                With jvDetail
                    .AccountCode = AMModule.DebitCode
                    .Debit = 0
                    .Credit = totalAP
                End With
                jvDetails.Add(jvDetail)
            End If

            'Check where NSS should be place
            If totalNSS <> 0 Then
                If totalAP + Math.Abs(WithholdingTax) > Math.Abs(totalAR) Then
                    jvDetail = New JournalVoucherDetails
                    With jvDetail
                        .AccountCode = AMModule.NSSCode
                        .Debit = Math.Abs(totalNSS)
                        .Credit = 0
                    End With
                    jvDetails.Add(jvDetail)
                Else
                    jvDetail = New JournalVoucherDetails
                    With jvDetail
                        .AccountCode = AMModule.NSSCode
                        .Debit = 0
                        .Credit = Math.Abs(totalNSS)
                    End With
                    jvDetails.Add(jvDetail)
                End If
            End If

            'Create the JV Main
            With result
                .PostedType = EnumPostedType.U.ToString()
                .JVDetails = jvDetails
                .CheckedBy = signatories.Signatory_1
                .ApprovedBy = signatories.Signatory_2
            End With
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        Return result
    End Function

    Private Sub btnExportperSTLID_Click(sender As Object, e As EventArgs) Handles btnExportperSTLID.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim TargetPath As String = ""
        Try
            ProgressThread.Close()
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

            ProgressThread.Show("Please wait while processing.")
            Dim bpValue As String = getCalendarBP.EndDate.ToString("MMMyyyy").ToUpper
            Dim fileName As String = bpValue & "_" & fileSettlementRun & "_INV"

            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)

            For cnt As Integer = 0 To Me.chckList.CheckedItems.Count - 1
                Dim participant = Me.chckList.CheckedItems(cnt).ToString()
                Dim getParticpantInfo As AMParticipants = (From x In listParticipantInfo Where x.IDNumber = participant Select x).First
                Dim listWESMTransSummary As New List(Of WESMBillAllocCoverSummary)
                listWESMTransSummary = _RTAFFHelper.NewListWBAllocCoverSummary.Where(Function(x) x.StlID = getParticpantInfo.IDNumber).Select(Function(z) z).ToList

                Dim newProgress As ProgressClass = New ProgressClass
                newProgress.ProgressMsg = "Exporting Excel File for " & getParticpantInfo.IDNumber.ToString
                UpdateProgress(newProgress)
                Parallel.ForEach(listWESMTransSummary, Sub(WESMTransSummary)
                                                           CreateRTAperParticipants(TargetPath, getParticpantInfo, WESMTransSummary)
                                                       End Sub)
            Next

            ProgressThread.Close()
            If Me.chckList.CheckedItems.Count = 0 Then
                MessageBox.Show("No selected participants!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Successfully exported in Excel!", "System Sucess Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmImportWESMAllocTransSummaryPrelim_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If cts IsNot Nothing Then
            ProgressThread.Close()
            If MessageBox.Show("Are you sure to cancel this process?", "Close",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                cts.Cancel()
                e.Cancel = True
                MessageBox.Show("Please try to close again!", "Close", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ProgressThread.Show("Please wait while continuing the process.")
                e.Cancel = True
                Me.Show()
            End If
        End If
    End Sub
End Class