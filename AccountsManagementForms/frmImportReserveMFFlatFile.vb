Option Explicit On
Option Strict On

Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports CrystalDecisions.Shared
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmImportReserveMFFlatFile
    Private UtiImporter As ImporterUtility
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private listOfSignatories As List(Of DocSignatories)
    Private listOfChargeCode As List(Of ChargeId)
    Private listOfParticipants As List(Of AMParticipants)


    Private fileBillingPd As Integer = 0
    Private fileSettlementRun As String = ""
    Private ftype As String = ""
    Private fileChargeType As EnumChargeType
    Private fileType As EnumFileType
    Private listWesmBill As List(Of WESMBill)
    Private listWesmInvoice As List(Of WESMInvoice)
    Private calendarBP As List(Of CalendarBillingPeriod)

    Dim cts As CancellationTokenSource
    Private Sub frmImportReserveMFFlatFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UtiImporter = ImporterUtility.GetInstance()
        UtiImporter.ConnectionString = AMModule.ConnectionString

        Me.MdiParent = MainForm
        Me.WBillHelper = WESMBillHelper.GetInstance
        Me.BFactory = BusinessFactory.GetInstance()
        Me.btnDownload.Enabled = False
        listWesmBill = New List(Of WESMBill)
        listWesmInvoice = New List(Of WESMInvoice)
        calendarBP = New List(Of CalendarBillingPeriod)
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
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Try
            Dim pathFolder As String = ""
            Dim fileName As String = ""

            If Me.txtDirectory.Text.Trim.Length = 0 Then
                MsgBox("Please specify the file to be uploaded.!", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            ElseIf Not File.Exists(Me.txtDirectory.Text.Trim()) Then
                MsgBox("File does not exist!", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            pathFolder = Path.GetDirectoryName(Me.txtDirectory.Text.Trim())
            fileName = Path.GetFileName(Me.txtDirectory.Text.Trim())

            Dim fnameSplit = Split(fileName, "_")
            Dim fnameSplit2 = Split(fnameSplit(fnameSplit.Length - 1), ".")

            If UCase(fnameSplit(0).ToString) <> "BILLINGRUN" Then
                MsgBox("Invalid filename! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            If fnameSplit.Length <> 4 Or fnameSplit2.Length <> 2 Then
                MsgBox("Invalid file format! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            If UCase(fnameSplit2(1).ToString) <> "CSV" Then
                MsgBox("Invalid file type! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            If UCase(fnameSplit2(0).ToString) <> "MFR" Then
                MsgBox("Invalid Transaction Type found! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            If (Trim(Mid(fnameSplit(2).ToString, 1, 1))) <> "P" Or Len(fnameSplit(2).ToString) > 4 Then
                MsgBox("Invalid Settlement Run! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            fileBillingPd = CInt(fnameSplit(1).ToString)
            fileSettlementRun = CStr(fnameSplit(2).ToString)
            fileType = EnumFileType.MarketFees

            listWesmBill = UtiImporter.ImportWESMBill(fileBillingPd, fileSettlementRun, pathFolder, fileName)
            listWesmInvoice = UtiImporter.ImportWESMInvoice(fileBillingPd, fileSettlementRun, fileType,
                                                           pathFolder, fileName)

            If listWesmBill.Count = 0 Then
                MsgBox("The file to be uploaded is empty!", MsgBoxStyle.Critical, "Warning")
                Exit Sub
            End If


            Dim invoiceDic As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
            Dim billNo As Integer = 0
            For Each item In listWesmInvoice
                Dim invoiceKey As String = item.IDNumber & item.RegistrationID & item.InvoiceDate.ToString("MMddyyyy") & item.DueDate.ToString("MMddyyyy")
                If Not invoiceDic.ContainsKey(invoiceKey) Then
                    billNo += 1
                    Dim invoiceNo As String = "PS-R" & billNo.ToString("0000000")
                    invoiceDic.Add(invoiceKey, billNo)
                    item.InvoiceNumber = invoiceNo
                    item.InvoiceCode = billNo
                Else
                    Dim invoiceNo As String = "PS-R" & invoiceDic.Item(invoiceKey).ToString("0000000")
                    item.InvoiceNumber = invoiceNo
                    item.InvoiceCode = invoiceDic.Item(invoiceKey)
                End If
            Next

            listOfChargeCode = WBillHelper.GetChargeIDCodes()

            'Check Participants
            listOfParticipants = (From x In WBillHelper.GetAMParticipants() Where x.Status = EnumStatus.Active Select x).ToList()

            Dim NotInList As New List(Of String)
            For Each item In listWesmBill
                Dim ParentID = item.IDNumber
                Dim CheckParticipant = (From x In listOfParticipants Where x.IDNumber = ParentID)
                If CheckParticipant.Count = 0 Then
                    NotInList.Add(ParentID)
                End If

                Dim ChildID = item.RegistrationID
                CheckParticipant = (From x In listOfParticipants Where x.IDNumber = ChildID)
                If CheckParticipant.Count = 0 Then
                    NotInList.Add(ChildID)
                End If
            Next

            NotInList = (From x In NotInList Select x Distinct).ToList

            If NotInList.Count <> 0 Then
                MsgBox("There are participants not registered found in the file being uploaded.", MsgBoxStyle.Critical, "Invalid Participants")
                frmViewDetails.ShowNotExistingParticipants(NotInList)
                frmViewDetails.Show()
                Exit Sub
            End If
            'End check Participants

            If UCase(fnameSplit2(0).ToString) = "MFR" Then
                fileChargeType = EnumChargeType.MF
                Dim items = (From x In listWesmInvoice Join y In listOfChargeCode On x.ChargeID Equals y.ChargeId
                             Where (y.cIDType = EnumChargeType.E Or y.cIDType = EnumChargeType.EV)
                             Select y.ChargeId Distinct).ToList()

                If items.Count > 0 Then
                    MsgBox("Invalid Charge ID Type " & items(0), MsgBoxStyle.Exclamation, "Invalid")
                    Exit Sub
                End If
            End If

            'Check Billing Period
            calendarBP = WBillHelper.GetCalendarBP()
            Dim countBP = (From x In calendarBP
                           Where x.BillingPeriod = fileBillingPd
                           Select x).Count()
            If countBP = 0 Then
                MsgBox("Billing period " & fileBillingPd.ToString() & " does not exist in Calendar BP table", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            MessageBox.Show("Successfully uploaded", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.btnDownload.Enabled = True
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearControls()
        Me.btnDownload.Enabled = False
        Me.txtDirectory.Text = ""
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
            ans = MessageBox.Show("Do you really want to export the Reserve Market Fees?", "Exporting Reserve Market Fees", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            ProgressThread.Show("Please wait while preparing the file.")

            Dim getListParticipantsInMF = (From x In listWesmInvoice Select x.IDNumber Order By IDNumber).Distinct.ToList()
            Dim signatory = WBillHelper.GetSignatories("INV").First()
            Dim systemGeneratedMessage As String = "This is system generated invoice no signature required."
            Dim getMarketFeesRate As String = (From x In listWesmInvoice Select x.MarketFeesRate).FirstOrDefault.ToString

            'Get the dataset for WESM Invoice
            Dim ds = BFactory.GenerateWESMInvoicePrelim(New DSReport.WESMInvoiceDataTable, New DSReport.WESMInvoiceDetailsDataTable,
                                                  listWesmInvoice, listOfParticipants,
                                                  listOfChargeCode, calendarBP, getListParticipantsInMF, signatory, fileType)

            Parallel.ForEach(getListParticipantsInMF,
                             Sub(participantInfo)
                                 Dim participant = participantInfo

                                 Dim listInvoice = From x In ds.Tables("WESMInvoice").AsEnumerable()
                                                   Where x("PARTICIPANT_ID").ToString() = participant
                                                   Select x("BILL_NUMBER") Distinct

                                 For Each item In listInvoice
                                     Dim seletectedInvoice = item.ToString()

                                     Dim dtMain = (From x In ds.Tables("WESMInvoice").AsEnumerable()
                                                   Where x("BILL_NUMBER").ToString() = seletectedInvoice
                                                   Select x).CopyToDataTable()

                                     Dim BillNumber As String = CStr(dtMain.Rows(0)("BILL_NUMBER"))

                                     Dim dtDetails = (From x In ds.Tables("WESMInvoiceDetails").AsEnumerable()
                                                      Where x("BILL_NUMBER").ToString() = seletectedInvoice
                                                      Select x).CopyToDataTable()

                                     Dim dsReport = New DataSet()
                                     dtMain.TableName = "WESMInvoice"
                                     dtDetails.TableName = "WESMInvoiceDetails"
                                     dsReport.Tables.Add(dtMain)
                                     dsReport.Tables.Add(dtDetails)


                                     Dim expReport = New RPTWESMInvoicePrelim
                                     expReport.SetDataSource(dsReport)
                                     expReport.SetParameterValue("paramBIRDateIssued", AMModule.BIRDateIssued)
                                     expReport.SetParameterValue("paramBIRValidUntil", AMModule.BIRValidUntil)
                                     expReport.SetParameterValue("paramSeriesNo", AMModule.FSNumberPrefix.ToString)
                                     expReport.SetParameterValue("paramMarketFeeRate", getMarketFeesRate)
                                     expReport.SetParameterValue("paramSystemGeneratedMsg", systemGeneratedMessage)

                                     Dim fileName = fileBillingPd & "" & fileSettlementRun & "-"

                                     expReport.ExportToDisk(ExportFormatType.PortableDocFormat, filePath & "\" &
                                                            participant & "_TS-RP-" & fileName & seletectedInvoice.Replace("PS-R", "") & "_" & "RMF" & ".pdf")
                                     expReport.Close()
                                     expReport.Dispose()
                                 Next
                             End Sub
            )
            ProgressThread.Close()
            MessageBox.Show("Successfully exported!", "System Sucess Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_Clear_Click(sender As Object, e As EventArgs) Handles btn_Clear.Click
        Dim ans As DialogResult = New DialogResult
        Try
            ans = MessageBox.Show("Do you really want to clear this?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ans = DialogResult.No Then
                Exit Sub
            End If

            'Reset Variables
            listWesmBill = New List(Of WESMBill)
            listWesmInvoice = New List(Of WESMInvoice)
            fileBillingPd = 0
            fileSettlementRun = ""
            ftype = ""
            fileChargeType = New EnumChargeType
            fileType = New EnumFileType

            ClearControls()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class