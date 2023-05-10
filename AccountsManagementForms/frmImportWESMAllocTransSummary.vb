Option Explicit On
Option Strict On

Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmImportWBTransactionSummaryBIRR
    Private _WBTSummaryHelper As New ImportWESMTransSummaryHelper
    Private _WBillHelper As WESMBillHelper
    Private cts As CancellationTokenSource
    Private Sub frmImportWBTransactionSummaryBIRR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Me.MdiParent = MainForm
            Me.txt_BillingRemarks.MaxLength = 300
        _WBillHelper = WESMBillHelper.GetInstance
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

    Private Async Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Try
            cts = New CancellationTokenSource
            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)

            Dim ask = MessageBox.Show("Do you really want to upload the file?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ask = DialogResult.No Then
                Exit Sub
            End If

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

            If Not fnameSplit(1).ToUpper.Contains("F") Then
                MessageBox.Show("Invalid filename! Please choose another file.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If Not fnameSplit(2).ToUpper.Equals("WF") And Not fnameSplit(2).ToUpper.Equals("WAC") And Not fnameSplit(2).ToUpper.Equals("WAD") Then
                MessageBox.Show("Invalid filename! Please choose another file.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If Not fnameSplit2(0).ToUpper.Equals("INV") Then
                MessageBox.Show("Invalid filename! Please choose another file.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim FileBillingPd As Integer = 0
            Dim FileSettlementRun As String = ""
            Dim BillType As String = ""
            Dim fileType As EnumFileType
            Dim wesmBilltransType As String = ""
            Dim fileChargeType As EnumChargeType

            FileBillingPd = CInt(fnameSplit(0).Replace("BP", "").Trim().ToString)
            FileSettlementRun = CStr(fnameSplit(1).Trim().ToString)
            wesmBilltransType = CStr(fnameSplit(2).Trim().ToUpper)

            fileChargeType = EnumChargeType.E
            fileType = EnumFileType.Energy

            Dim transDate As Date = CDate(FormatDateTime(Me.dtTransDate.Value, DateFormat.ShortDate))
            Dim dueDate As Date = CDate(FormatDateTime(Me.dtDueDate.Value, DateFormat.ShortDate))
            Dim remarks As String = CStr(Me.txt_BillingRemarks.Text)
            Dim currDate As Date = _WBillHelper.GetSystemDate()

            Dim getCalendarBP As CalendarBillingPeriod = (From x In _WBillHelper.GetCalendarBP() Where x.BillingPeriod = FileBillingPd Select x).FirstOrDefault

            If getCalendarBP Is Nothing Then
                MessageBox.Show("Billing Period" & FileBillingPd.ToString() & " does not exist in Calendar BP table", "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If transDate.Month <> currDate.Month Or transDate.Year <> currDate.Year Then
                MessageBox.Show("Invalid selection for Transaction Date ", "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If dueDate.Month <> currDate.Month Or dueDate.Year <> currDate.Year Or dueDate.Day <> 25 Then
                MessageBox.Show("Invalid selection for Due Date ", "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If txt_BillingRemarks.Text.Length = 0 Then
                MessageBox.Show("Please input Billing Remarks ", "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            'ProgressThread.Show("Please wait while saving.")
            Await Task.Run(Sub() Me._WBTSummaryHelper.ReadFlatFileNew(FileBillingPd, FileSettlementRun, wesmBilltransType, fileType, pathFolder, fileName, remarks, transDate, dueDate, progressIndicator))
            'ProgressThread.Close()

            'Check Participants
            Dim AllParticipants = (From x In _WBillHelper.GetAMParticipants() Where x.Status = EnumStatus.Active Select x).ToList()

            Dim NotInList As New List(Of String)
            For Each item In Me._WBTSummaryHelper.NewListWESMBill
                Dim ParentID = item.IDNumber
                Dim CheckParticipant = (From x In AllParticipants Where x.IDNumber = ParentID)
                If CheckParticipant.Count = 0 Then
                    NotInList.Add(ParentID)
                End If

                'Dim ChildID = item.RegistrationID
                'CheckParticipant = (From x In AllParticipants Where x.IDNumber = ChildID)
                'If CheckParticipant.Count = 0 Then
                '    NotInList.Add(ChildID)
                'End If
            Next

            NotInList = (From x In NotInList Select x Distinct).ToList

            If NotInList.Count <> 0 Then
                MessageBox.Show("There are participants not registered found in the file being uploaded.", "Invalid Participants", MessageBoxButtons.OK, MessageBoxIcon.Error)
                frmViewDetails.ShowNotExistingParticipants(NotInList)
                frmViewDetails.Show()
                Exit Sub
            End If
            'End check Participants

            Dim tmpCharge = From x In _WBTSummaryHelper.NewListWESMBill Select x.ChargeType Distinct
            For Each item In tmpCharge
                Dim isBillPosted = _WBillHelper.IsWESMBillPosted(FileBillingPd, FileSettlementRun, item)
                If isBillPosted = True Then
                    MessageBox.Show("Cannot upload the records, data are already posted!", "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Next

            If UCase(fnameSplit2(0).ToString) = "INV" Then

                'Check if the summation of VAT on Energy is equal to zero
                Dim Vat = (From x In _WBTSummaryHelper.NewListWESMBill
                           Where x.ChargeType = EnumChargeType.EV
                           Select x.Amount).Sum()

                If Vat <> 0 Then
                    If MessageBox.Show("Summation of VAT on Energy is not equal to zero. Do you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If

                Dim AP As Decimal = (From x In _WBTSummaryHelper.NewListWESMBill
                                     Where x.Amount > 0
                                     Select x.Amount).Sum()

                Dim AR As Decimal = (From x In _WBTSummaryHelper.NewListWESMBill
                                     Where x.Amount < 0
                                     Select x.Amount).Sum()

                Dim TotalAmount As Decimal = AP + AR

                'Compare NSSRA line item from AP minus AR.
                If TotalAmount <> 0 Then
                    MessageBox.Show("Total AP and AR:" & vbTab & vbTab & FormatNumber(TotalAmount, 2) & vbNewLine &
                                    "Total AP and AR line item are not equal! ", "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                Else
                    MessageBox.Show("Total AP and AR:" & vbTab & vbTab & FormatNumber(TotalAmount, 2), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                'Check if there is existing records
                Dim CountWESMBillExist As Integer = _WBillHelper.GetWESMBillCount(FileBillingPd, FileSettlementRun, fileChargeType)
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

                    Dim itemJV As JournalVoucher
                    If fileChargeType = EnumChargeType.E Then
                        itemJV = Me.GenerateJournalVoucherForEnergy(_WBTSummaryHelper.NewListWESMBill, 0)
                    Else
                        itemJV = Me.GenerateJournalVoucherForMarketFees(_WBTSummaryHelper.NewListWESMBill)
                    End If

                    Dim itemGP = Me.GenerateGPPosted(getCalendarBP, FileSettlementRun, dueDate,
                                                         fileChargeType, itemJV)

                    ProgressThread.Show("Please wait while saving.")

                    Await Task.Run(Sub() _WBTSummaryHelper.SaveUplodedWESMBill(getCalendarBP, FileSettlementRun, fileChargeType, fileType, itemJV, itemGP, CountWESMBillExist, progressIndicator, cts.Token))

                    ProgressThread.Close()
                    MessageBox.Show("Successfully uploaded to Database", "System Sucess Message!", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Me.ClearControls()
                    'Updated By Lance 08/17/2014
                    '_Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.SAPUploadWESMBillWindow.ToString, fileName, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyUploaded.ToString, AMModule.UserName)

                Else
                    ProgressThread.Close()
                    MessageBox.Show("Transaction Cancelled", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.panel_ImportBIRRTransSummary.Enabled = True
        End Try
    End Sub

#Region "Functions/Methods"
    Private Sub ClearControls()
        Me.txt_BillingRemarks.Text = ""
        Me.txtDirectory.Text = ""
    End Sub

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

        Private Function GenerateJournalVoucherForMarketFees(ByVal listItems As List(Of WESMBill)) As JournalVoucher

            Dim result As New JournalVoucher
            Dim jvDetails As New List(Of JournalVoucherDetails)
            Dim jvDetail As JournalVoucherDetails

            Try
                'Get the signatories
                Dim signatories = (From x In _WBillHelper.GetSignatories()
                                   Where x.DocCode = EnumDocCode.JV.ToString()
                                   Select x).First()

                'Get the Total AR Market Fees
                Dim totalMFAR = (From x In listItems
                                 Where x.ChargeType = EnumChargeType.MF And x.Amount < 0
                                 Select x.Amount).Sum()

                'Get the Total AP Market Fees
                Dim totalMFAP = (From x In listItems
                                 Where x.ChargeType = EnumChargeType.MF And x.Amount > 0
                                 Select x.Amount).Sum()

                'Get the Total AR Vat on Market Fees
                Dim totalMFVAR = (From x In listItems
                                  Where x.ChargeType = EnumChargeType.MFV And x.Amount < 0
                                  Select x.Amount).Sum()

                'Get the Total AP Vat on Market Fees
                Dim totalMFVAP = (From x In listItems
                                  Where x.ChargeType = EnumChargeType.MFV And x.Amount > 0
                                  Select x.Amount).Sum()


                'Comment by Vloody 03/25/2018
                'Since the Summary of Accounting Books is per WESM Bill
                'The negative and positive should have a separate entries

                If totalMFAR <> 0 Then
                    jvDetail = New JournalVoucherDetails
                    With jvDetail
                        .AccountCode = AMModule.MarketTransFeesCode
                        .Debit = 0
                        .Credit = Math.Abs(totalMFAR)
                    End With
                    jvDetails.Add(jvDetail)
                End If

                If totalMFVAR <> 0 Then
                    jvDetail = New JournalVoucherDetails
                    With jvDetail
                        .AccountCode = AMModule.MarketFeesOutputTaxCode
                        .Debit = 0
                        .Credit = Math.Abs(totalMFVAR)
                    End With
                    jvDetails.Add(jvDetail)
                End If

                If totalMFAR <> 0 Or totalMFVAR <> 0 Then
                    jvDetail = New JournalVoucherDetails
                    With jvDetail
                        .AccountCode = AMModule.CreditCode
                        .Debit = Math.Abs(totalMFAR + totalMFVAR)
                        .Credit = 0
                    End With
                    jvDetails.Add(jvDetail)
                End If

                If totalMFAP <> 0 Then
                    jvDetail = New JournalVoucherDetails
                    With jvDetail
                        .AccountCode = AMModule.MarketTransFeesCode
                        .Debit = totalMFAP
                        .Credit = 0
                    End With
                    jvDetails.Add(jvDetail)
                End If

                If totalMFVAP <> 0 Then
                    jvDetail = New JournalVoucherDetails
                    With jvDetail
                        .AccountCode = AMModule.MarketFeesOutputTaxCode
                        .Debit = totalMFVAP
                        .Credit = 0
                    End With
                    jvDetails.Add(jvDetail)
                End If

                If totalMFAP <> 0 Or totalMFVAP <> 0 Then
                    jvDetail = New JournalVoucherDetails
                    With jvDetail
                        .AccountCode = AMModule.DebitCode
                        .Debit = 0
                        .Credit = totalMFAP + totalMFVAP
                    End With
                    jvDetails.Add(jvDetail)
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

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Me.Close()
        End Sub

#End Region

    End Class